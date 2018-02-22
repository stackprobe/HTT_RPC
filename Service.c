#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\FileTools.h"
#include "C:\Factory\Satellite\libs\Flowertact\Fortewave.h"
#include "libs\FileBox.h"

#define COMMON_ID "{7da01163-efa3-4941-a5a6-be0800720d8e}" // shared_uuid@g
#define W_MUTEX_ID COMMON_ID "_w"
#define MUTEX_ID COMMON_ID "_m"
#define HTT_ID COMMON_ID "_h"
#define HTT_SERVICE_ID COMMON_ID "_hs"

#define IP_FILE "IP.httdat"
#define RECV_FILE "Recv.httdat"
#define SEND_FILE "Send.httdat"
#define TIGHT_FILE "Tight.httdat"
#define WAIT_0_FILE "Wait_0.httdat"

#define HEADER_PART_FILE "HeaderPart.dat"
#define HEADER_INFO_FILE "HeaderInfo.dat"
#define BODY_SIZE_FILE "BodySize.dat"
#define CHUNKED_FLAG "Chunked.flg"
#define BODY_FILE "Body.dat"
#define ERROR_FLAG "Error.flg"
#define RES_INFO_FILE "ResInfo.dat"
#define DOWNLOAD_FLAG "Download.flg"
#define DOWNLOAD_INFO_FILE "DownloadInfo.dat"
#define WAIT_RESPONSE_FIRST_TIME_FLAG "WaitResponseFirstTime.flg"

// ---- config ----

/*
	.conf の内容は正しいものとして扱う。エラーチェックしない！！！
*/

static autoList_t *ConfigTokens;

static void INIT_Config(void)
{
	if(!ConfigTokens)
		ConfigTokens = tokenize_x(readText_x(changeExt(getSelfFile(), "conf")), ':');
}
static uint64 GetContentLengthMax(void)
{
	INIT_Config();
	return toValue64(getLine(ConfigTokens, 0)); // 値域: 0 ～ IMAX_64
}
static uint GetWaitResponseMillis(void)
{
	INIT_Config();
	return toValue(getLine(ConfigTokens, 1)); // 値域: 0 ～ IMAX
}
static uint GetWaitResponseTimeoutSec(void)
{
	INIT_Config();
	return toValue(getLine(ConfigTokens, 2)); // 値域: 0 ～ IMAX
}

// ----

static time_t SockTimeoutSec;
static char *SendFileFullPath;

static void DoDisconnect(void)
{
	LOGPOS();
	removeFileAtTermination(SendFileFullPath);
}

// ---- tools ----

static char *NextCRLF(char *p)
{
	for(; ; p++)
	{
		errorCase(!*p); // ? 見つからない。
		errorCase(*p == '\n'); // ? ! CR-LF

		if(*p == '\r')
		{
			errorCase(p[1] != '\n'); // ? ! CR-LF
			break;
		}
	}
	return p;
}
static char *ReadToCRLF_IgnoreTopCr(FILE *fp)
{
	autoBlock_t *buff = newBlock();

	for(; ; )
	{
		int chr = readChar(fp);

		if(chr == EOF)
		{
			releaseAutoBlock(buff);
			return NULL;
		}
		if(chr == '\r' && getSize(buff))
			break;

		errorCase(512000 < getSize(buff)); // ? 1行_でか過ぎ
		addByte(buff, chr);
	}
	errorCase(readChar(fp) != '\n'); // ? ! CR-LF
	return unbindBlock2Line(buff);
}
static char *Block2Line(autoBlock_t *block)
{
	addByte(block, '\0');
	return b(block);
}
static void CheckHttServerRunning(int stoppedAndError)
{
	{
		static int passed;

		if(passed)
			return;

		passed = 1;
	}

	{
		uint wHdl = mutexOpen(W_MUTEX_ID);
		uint hdl = mutexOpen(MUTEX_ID);
		int running = 1;

		handleWaitForever(wHdl); // WHTTR.exe もチェックを行うため、WHTTR.exe を排他する。

		if(handleWaitForMillis(hdl, 0)) // ? ロックできた。-> ロックされていなかった。-> HttServer 停止中
		{
			running = 0;

			// 停止中にすべきこと！
			{
				{
					Frtwv_t *i = Frtwv_Create(HTT_ID);

					Frtwv_Clear(i);
					Frtwv_Release(i);
				}

				{
					Frtwv_t *i = Frtwv_Create(HTT_SERVICE_ID);

					Frtwv_Clear(i);
					Frtwv_Release(i);
				}

				FB_Clear();
			}

			mutexRelease(hdl);
		}
		mutexRelease(wHdl);
		handleClose(hdl);

		errorCase(stoppedAndError && !running);
	}
}
static int HasUrlScheme(char *url)
{
	char *p = url;

	if(!m_isalpha(*p))
		return 0;

	while(m_isalpha(*++p));

	return !strcmp(p, "://");
}

// ---- DoParseHeader ----

static char *DPH_Text;
static autoList_t *DPH_Lines;
static char *DPH_FirstLine;
static char *DPH_Method;
static char *DPH_Url;
static char *DPH_HTTPVersion;
static autoList_t *DPH_Keys;
static autoList_t *DPH_Values;
static uint64 DPH_BodySize;
static int DPH_Chunked;
static char *DPH_Host;
static int DPH_Expect100Continue;

static void DPH_ParseLines(void)
{
	char *p = DPH_Text;

	DPH_Lines = newList();

	while(*p)
	{
		char *q = NextCRLF(p);
		char *line;

		*q = '\0'; // 破壊
		line = strx(p);
//		*q = '\r'; // 復元
		addElement(DPH_Lines, (uint)line);
		p = q + 2;
	}
}
static void DPH_ParseFirstLine(void)
{
	char *line = getLine(DPH_Lines, 0);
	autoList_t *tokens;

	tokens = tokenize(line, ' ');

	errorCase(getCount(tokens) != 3);

	DPH_Method      = getLine(tokens, 0);
	DPH_Url         = getLine(tokens, 1);
	DPH_HTTPVersion = getLine(tokens, 2);

	toAsciiLine(DPH_Method,      0, 0, 0);
	toAsciiLine(DPH_Url,         0, 0, 0);
	toAsciiLine(DPH_HTTPVersion, 0, 0, 0);
}
static void DPH_ParseFields(void)
{
	uint index;

	DPH_Keys = newList();
	DPH_Values = newList();

	for(index = 1; index + 1 < getCount(DPH_Lines); index++) // 最終行は空行
	{
		char *line = getLine(DPH_Lines, index);

		toAsciiLine(line, 0, 1, 1);

		if(line[0] <= ' ' && 1 <= getCount(DPH_Values)) // unfolding
		{
			char *value = (char *)unaddElement(DPH_Values);

			ucTrimEdge(line);

			value = addChar(value, ' ');
			value = addLine(value, line);

			addElement(DPH_Values, (uint)value);
		}
		else
		{
			char *p = strchr(line, ':');
			char *key;
			char *value;

			errorCase(!p);

			*p = '\0';
			key = strx(line);
			value = strx(p + 1);

			ucTrimEdge(key);
			ucTrimEdge(value);

			addElement(DPH_Keys, (uint)key);
			addElement(DPH_Values, (uint)value);
		}
	}
}
static void DPH_CheckFields(void)
{
	char *key;
	uint index;

	foreach(DPH_Keys, key, index)
	{
		if(!_stricmp(key, "Content-Length"))
		{
			DPH_BodySize = toValue64(getLine(DPH_Values, index));
		}
		else if(!_stricmp(key, "Transfer-Encoding") && !_stricmp(getLine(DPH_Values, index), "chunked"))
		{
			DPH_Chunked = 1;
		}
		else if(!_stricmp(key, "Host"))
		{
			DPH_Host = getLine(DPH_Values, index);
		}
		else if(!_stricmp(key, "Expect") && !_stricmp(getLine(DPH_Values, index), "100-continue"))
		{
			DPH_Expect100Continue = 1;
		}
	}
	if(!DPH_Host || !*DPH_Host)
		DPH_Host = "localhost";

	DPH_Host = strx(DPH_Host);
}
static void DoParseHeader(void)
{
	autoBlock_t *recvData;
	uint rPos;

	recvData = readBinary(RECV_FILE);

	for(rPos = 0; ; rPos++)
	{
		if(getSize(recvData) < rPos + 4) // ? 受信未完了
		{
			errorCase(512000 < getSize(recvData)); // ? ヘッダ_でか過ぎ
			errorCase(getFileWriteTime(IP_FILE) + SockTimeoutSec < time(NULL)); // ? 接続してから一定時間以上経過 -> 時間掛かり過ぎ
			termination(0);
		}
		if(
			b(recvData)[rPos + 0] == '\r' &&
			b(recvData)[rPos + 1] == '\n' &&
			b(recvData)[rPos + 2] == '\r' &&
			b(recvData)[rPos + 3] == '\n'
			)
			break;
	}
	rPos += 4;
	setSize(recvData, rPos);
	DeleteFileDataPart(RECV_FILE, 0, rPos);

	writeBinary(HEADER_PART_FILE, recvData);

	DPH_Text = unbindBlock2Line(recvData);
	DPH_ParseLines();
	DPH_ParseFirstLine();
	DPH_ParseFields();
	DPH_CheckFields();

	if(HasUrlScheme(DPH_Url))
//	if(startsWithICase(DPH_Url, "http://"))
	{
		// noop
	}
	else
	{
		if(*DPH_Url == '/')
			DPH_Url++;

		DPH_Url = xcout("http://%s/%s", DPH_Host, DPH_Url);
	}

	{
		FILE *fp = fileOpen(HEADER_INFO_FILE, "wb");

		writeLine(fp, DPH_Method);
		writeLine(fp, DPH_Url);
		writeLine(fp, DPH_HTTPVersion);
		writeValue(fp, getCount(DPH_Keys));
		writeLines2Stream(fp, DPH_Keys);
		writeLines2Stream(fp, DPH_Values);

		fileClose(fp);
	}

	if(DPH_Chunked)
		errorCase(1 <= DPH_BodySize); // ? chunked なのに Content-Length が指定されている。
	else
		errorCase(GetContentLengthMax() < DPH_BodySize); // ? Content-Length でか過ぎ

	writeOneValue64(BODY_SIZE_FILE, DPH_BodySize);

	if(DPH_Chunked)
		createFile(CHUNKED_FLAG);

	errorCase(DPH_Expect100Continue); // Expect: 100-continue 非対応
}

// ---- DoRecvBody ----

static void DRB_Request(void)
{
	autoList_t *ol = newList();
	uint kv_num;
	uint index;

	addElement(ol, (uint)ab_fromLine_x(getCwd()));
	addElement(ol, (uint)ab_fromLine_x(readFirstLine(IP_FILE)));

	{
		FILE *fp = fileOpen(HEADER_INFO_FILE, "rb");

		DPH_Method      = readLine(fp);
		DPH_Url         = readLine(fp);
		DPH_HTTPVersion = readLine(fp);

		kv_num = readValue(fp);

		DPH_Keys = newList();
		DPH_Values = newList();

		for(index = 0; index < kv_num; index++)
		{
			addElement(DPH_Keys, (uint)readLine(fp));
		}
		for(index = 0; index < kv_num; index++)
		{
			addElement(DPH_Values, (uint)readLine(fp));
		}
		fileClose(fp);
	}

	addElement(ol, (uint)ab_fromLine(DPH_Method));
	addElement(ol, (uint)ab_fromLine(DPH_Url));
	addElement(ol, (uint)ab_fromLine(DPH_HTTPVersion));
	addElement(ol, (uint)ab_fromLine_x(xcout("%u", getCount(DPH_Keys))));

	for(index = 0; index < kv_num; index++)
	{
		addElement(ol, (uint)ab_fromLine(getLine(DPH_Keys, index)));
		addElement(ol, (uint)ab_fromLine(getLine(DPH_Values, index)));
	}

	addElement(ol, (uint)ab_fromLine_x(FB_AddFile_MvCr(HEADER_PART_FILE)));
	addElement(ol, (uint)ab_fromLine_x(FB_AddFile_MvCr(BODY_FILE)));

	{
		Frtwv_t *i = Frtwv_Create(HTT_SERVICE_ID);

		Frtwv_SendOL(i, ol, 1);
		Frtwv_Release(i);
	}

	releaseDim_BR(ol, 1, releaseAutoBlock);
}
static void DoRecvBody(void)
{
	uint64 bodySz = readFirstValue64(BODY_SIZE_FILE);
	uint64 recvFSz;

restart:
	recvFSz = getFileSize(RECV_FILE);

	if(recvFSz < bodySz) // ? 受信未完了
	{
	continue_read:
		errorCase(getFileWriteTime(RECV_FILE) + SockTimeoutSec < time(NULL)); // ? 受信の無通信状態が一定時間以上続いている。-> 遅すぎ、切る。
		termination(0);
	}
	if(existFile(CHUNKED_FLAG))
	{
		FILE *fp = fileOpen(RECV_FILE, "rb");
		char *line;
		uint64 nextSz;

		fileSeek(fp, SEEK_SET, bodySz);
		line = ReadToCRLF_IgnoreTopCr(fp);
		fileClose(fp);

		if(!line) // ? 次のサイズ_受信未完了
			goto continue_read;

		DeleteFileDataPart(RECV_FILE, bodySz, strlen(line) + 2);

		// chunk-extension 削除
		{
			char *p = strchr(line, ';');

			if(p)
				*p = '\0';
		}

		ucTrimEdge(line);
		toAsciiLine(line, 0, 0, 0);

		nextSz = toValue64Digits(line, hexadecimal);

		memFree(line);

		if(1 <= nextSz) // ? chunk まだ続く
		{
			errorCase(IMAX_64 < nextSz); // ? 次のサイズ_でか過ぎ
			bodySz += nextSz;
			errorCase(GetContentLengthMax() < bodySz); // ? ボディ_でか過ぎ
			writeOneValue64(BODY_SIZE_FILE, bodySz);
			goto restart;
		}
		setFileSize(RECV_FILE, bodySz); // chunk footer 削除
	}
	else
	{
		errorCase(bodySz < recvFSz); // ? 送信データ Content-Length より長い。
	}
	moveFile(RECV_FILE, BODY_FILE);
	createFile(RECV_FILE);

	CheckHttServerRunning(1);

	DRB_Request();
}

// ---- DoWaitResponse ----

static autoList_t *DWR_OL;
static uint DWR_RPos;

static char *DWR_Next(int kind) // ret: c_, kind: "ABP"
{
	autoBlock_t *block;
	char *ret;

	block = (autoBlock_t *)getElement(DWR_OL, DWR_RPos);
	DWR_RPos++;

	switch(kind) // 書式エラーは矯正して継続！！！
	{
	case 'A':
		ret = Block2Line(block);
		toAsciiLine(ret, 0, 1, 1);
		break;

	case 'B':
		ret = (char *)block;
		break;

	case 'P':
		ret = Block2Line(block);
		line2JLine(ret, 1, 0, 0, 1);
		trimEdge(ret, ' ');
		break;

	default:
		error();
	}
	return ret;
}
static DWR_CheckTimeout(void)
{
	if(!existFile(WAIT_RESPONSE_FIRST_TIME_FLAG)) // ? 初回
	{
		createFile(WAIT_RESPONSE_FIRST_TIME_FLAG);
	}
	else // ? 2回目～
	{
		errorCase(getFileWriteTime(WAIT_RESPONSE_FIRST_TIME_FLAG) + GetWaitResponseTimeoutSec() < time(NULL)); // レスポンス待ちタイムアウト
	}
}

static char *DWR_HTTPVersion;
static char *DWR_StatusCode;
static char *DWR_ReasonPhrase;
static autoList_t *DWR_Keys; // "" == 直前のフィールドの folding
static autoList_t *DWR_Values;
static char *DWR_DLFile; // "" == ファイル無し。
static autoBlock_t *DWR_DLData;

static void DWR_LoadResInfo(void)
{
	FILE *fp = fileOpen(RES_INFO_FILE, "rb");
	uint kv_num;
	uint index;

	DWR_HTTPVersion  = readLine(fp);
	DWR_StatusCode   = readLine(fp);
	DWR_ReasonPhrase = readLine(fp);

	kv_num = readValue(fp);

	DWR_Keys = newList();
	DWR_Values = newList();

	for(index = 0; index < kv_num; index++)
	{
		addElement(DWR_Keys, (uint)readLine(fp));
		addElement(DWR_Values, (uint)readLine(fp));
	}

	DWR_DLFile = readLine(fp);
	DWR_DLData = readBinaryToEnd(fp, NULL);

	fileClose(fp);
}
static void DWR_StartDownload(void)
{
	FILE *fp = fileOpen(SEND_FILE, "wb");
	char *key;
	uint index;

	writeToken_x(fp, xcout("%s %s %s\r\n", DWR_HTTPVersion, DWR_StatusCode, DWR_ReasonPhrase));

	foreach(DWR_Keys, key, index)
	{
		if(*key)
			writeToken_x(fp, xcout("%s:", key));

		writeToken_x(fp, xcout(" %s\r\n", getLine(DWR_Values, index)));
	}

	{
		uint64 contentLength = getSize(DWR_DLData);

		if(*DWR_DLFile)
			contentLength += getFileSize(DWR_DLFile);

		writeToken_x(fp, xcout(
			"Content-Length: %I64u\r\n"
			"Connection: close\r\n"
			,contentLength
			));
	}

	writeToken(fp, "\r\n");
	writeBinaryBlock(fp, DWR_DLData);

	fileClose(fp);

	if(*DWR_DLFile)
	{
		fp = fileOpen(DOWNLOAD_INFO_FILE, "wb");

		writeLine(fp, DWR_DLFile);
		writeLine(fp, "0");

		fileClose(fp);
	}
}
static void DoWaitResponse(void)
{
	CheckHttServerRunning(1);

	errorCase(existFile(ERROR_FLAG)); // ? サービス・エラー -> 切断

	setFileSize(RECV_FILE, 0); // もう受信しない。念のため潰しておく。

	if(!existFile(RES_INFO_FILE)) // ? サービス・レスポンス_未受信
	{
		Frtwv_t *i = Frtwv_Create(HTT_ID);
		int quickFlag = 0;

		for(; ; )
		{
			autoList_t *ol = Frtwv_RecvOL(i, 1, quickFlag ? 0 : GetWaitResponseMillis());

			if(!ol)
				break;

			DWR_OL = ol;
			DWR_RPos = 0;

			switch(DWR_Next('A')[0]) // COMMAND
			{
			case 'E': // ERROR
				{
					char *conDir = DWR_Next('P'); // CONNECTION-DIR

					cout("E_conDir: %s\n", conDir);

					if(existDir(conDir)) // ? まだ切断していない。
					{
						char *file = combine(conDir, ERROR_FLAG);

						createFile(file);
						memFree(file);
					}
				}
				break;

			case 'R': // RESPONSE
				{
					char *conDir = DWR_Next('P'); // CONNECTION-DIR

					cout("R_conDir: %s\n", conDir);

					if(existDir(conDir)) // ? まだ切断していない。
					{
						char *file = combine(conDir, RES_INFO_FILE);
						FILE *fp;
						uint kv_num;
						uint index;

						fp = fileOpen(file, "wb");

						writeLine(fp, DWR_Next('A')); // HTTP-VERSION
						writeLine(fp, DWR_Next('A')); // STATUS-CODE
						writeLine(fp, DWR_Next('A')); // REASON-PHRASE

						kv_num = toValue(DWR_Next('A')); // KEY-VALUE-NUM

						writeValue(fp, kv_num);

						for(index = 0; index < kv_num; index++)
						{
							writeLine(fp, DWR_Next('A')); // KEY
							writeLine(fp, DWR_Next('A')); // VALUE
						}

						writeLine(fp, DWR_Next('P')); // DOWNLOAD-FILE
						writeBinaryBlock(fp, (autoBlock_t *)DWR_Next('B')); // DOWNLOAD-DATA

						fileClose(fp);
						memFree(file);
					}
				}
				break;

			default:
				error();
			}

			DWR_OL = NULL;
			DWR_RPos = 0;

			releaseDim_BR(ol, 1, releaseAutoBlock);

			if(!quickFlag)
				quickFlag = existFile(ERROR_FLAG) || existFile(RES_INFO_FILE);
		}
		Frtwv_Release(i);
	}
	errorCase(existFile(ERROR_FLAG)); // ? サービス・エラー -> 切断

	if(!existFile(RES_INFO_FILE)) // ? サービス・レスポンス_未受信
	{
		DWR_CheckTimeout();

		createFile(WAIT_0_FILE);
		termination(0);
	}
	DWR_LoadResInfo();
	DWR_StartDownload();

	createFile(DOWNLOAD_FLAG);
}

// ---- DoDownload ----

#define DD_MIN_SIZE 1000000
#define DD_ADD_SIZE 2000000

static char *DD_File;
static uint64 DD_RPos;
static uint64 DD_FileSize;

static void DoDownload(void)
{
	uint64 sendFSz = getFileSize(SEND_FILE);

	setFileSize(RECV_FILE, 0); // もう受信しない。念のため潰しておく。

	if(DD_MIN_SIZE < sendFSz)
	{
		errorCase(getFileWriteTime(SEND_FILE) + SockTimeoutSec < time(NULL)); // ? 送信の無通信状態が一定時間以上続いている。-> 遅すぎ、切る。
		termination(0);
	}
	if(existFile(DOWNLOAD_INFO_FILE))
	{
		FILE *fp = fileOpen(DOWNLOAD_INFO_FILE, "rb");

		DD_File = readLine(fp);
		DD_RPos = toValue64_x(readLine(fp));

		fileClose(fp);

		// ----

		errorCase(!existFile(DD_File));

		fp = fileOpen(DD_File, "rb");
		DD_FileSize = getFileSizeFP(fp);

		errorCase(DD_FileSize < DD_RPos);

		fileSeek(fp, SEEK_SET, DD_RPos);

		if(DD_ADD_SIZE < DD_FileSize - DD_RPos) // ? 残り DD_ADD_SIZE より多い。
		{
			writeJoinBinary_cx(SEND_FILE, neReadBinaryBlock(fp, DD_ADD_SIZE));
			DD_RPos += DD_ADD_SIZE;

			fileClose(fp);

			// ----

			fp = fileOpen(DOWNLOAD_INFO_FILE, "wb");

			writeLine(fp, DD_File);
			writeLine_x(fp, xcout("%I64u", DD_RPos));

			fileClose(fp);
		}
		else // ? 残り DD_ADD_SIZE 以下
		{
			writeJoinBinary_cx(SEND_FILE, readBinaryToEnd(fp, NULL));

			fileClose(fp);

			// ----

			removeFile(DOWNLOAD_INFO_FILE);
		}
		termination(0);
	}
	if(sendFSz == 0) // ダウンロード指示ファイル無し && SEND_FILE 空 -> 送信完了
	{
		DoDisconnect(); // 送信完了
	}
}

// ----

static void ErrorFnlz(void)
{
	if(errorOccurred)
		DoDisconnect();

	termination(1);
}
static void Main2(void)
{
	errorCase(!existFile(IP_FILE));
	errorCase(!existFile(RECV_FILE));
	errorCase(!existFile(SEND_FILE));

	if(getFileSize(IP_FILE) == 0)
		return;

	SockTimeoutSec = existFile(TIGHT_FILE) ? 60L : 300L;
	cout("SockTimeoutSec: %I64d\n", SockTimeoutSec);

	SendFileFullPath = makeFullPath(SEND_FILE);
	addFinalizer(ErrorFnlz);

	if(!existFile(HEADER_PART_FILE))
		DoParseHeader();

	if(!existFile(BODY_FILE))
		DoRecvBody();

	if(!existFile(DOWNLOAD_FLAG)) // 他の接続からも書き込まれるので RES_INFO_FILE で判定するのはマズい。
		DoWaitResponse();

	DoDownload();
}
int main(int argc, char **argv)
{
	if(argIs("/BEFORE-HTT"))
	{
		CheckHttServerRunning(0);
		return;
	}

	Main2();
	termination(0);
}
