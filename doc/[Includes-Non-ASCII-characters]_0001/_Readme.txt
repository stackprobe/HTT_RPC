==================
サンプルプログラム
==================


---- DemoUploader ----


■概要

	ファイル・アップローダーのデモ


■起動方法

	1. WHTTR.exe を起動する。

	2. DemoUploader.exe を起動する。

	3. http://localhost/ にアクセスする。

		ポート番号を変更した場合、別ホストからアクセスする場合は http://<SERVER-HOST>:<PORT>/ として下さい。


■終了方法

	1. DemoUploader.exe を終了する。

		コンソールウィンドウにフォーカスを合わせてエスケープキーを押す。

	2. WHTTR.exe を終了する。

		タスクトレイ上のアイコンを右クリックして「終了」を選択する。


■チュートリアル

	1. 起動方法を実行して、ページを開いて下さい。

	2. "file:" に続く [ファイルを選択] または [参照] をクリックして、適当なファイル (画像、音楽、映像など) を選択して下さい。

	3. [Upload] ボタンを押して下さい。

	4. アップロードしたファイルに関するページが表示されます。


■ソースコード

	根っこ

		https://github.com/stackprobe/Satellite/tree/master/Test_Server/DemoUploader

	DemoUploaderService クラス (HttService を継承したクラス)

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/DemoUploader/DemoUploader/DemoUploaderService.cs

	HttServer を起動しているところ

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/DemoUploader/DemoUploader/Program.cs




---- EasyBBS ----


■概要

	簡易BBS


■起動方法

	1. WHTTR.exe を起動する。

	2. EasyBBS.exe を起動する。

	3. http://localhost/ にアクセスする。

		ポート番号を変更した場合、別ホストからアクセスする場合は http://<SERVER-HOST>:<PORT>/ として下さい。


■終了方法

	1. EasyBBS.exe を終了する。

		コンソールウィンドウにフォーカスを合わせてエスケープキーを押す。

	2. WHTTR.exe を終了する。

		タスクトレイ上のアイコンを右クリックして「終了」を選択する。


■チュートリアル

	1. 起動方法を実行して、ページを開いて下さい。

	2. "name:", "e-mail address:" を任意に変更し textarea に適当な文字列を入力して [Send] をクリックして下さい。

	3. ページのタイムラインが更新されます。


■ソースコード

	根っこ

		https://github.com/stackprobe/Satellite/tree/master/Test_Server/EasyBBS

	EasyBBSService クラス (HttService を継承したクラス)

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/EasyBBS/EasyBBS/EasyBBSService.cs

	HttServer を起動しているところ

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/EasyBBS/EasyBBS/Program.cs




---- LiteFiler ----


■概要

	簡易ファイラー


■起動方法

	1. WHTTR.exe を起動する。

	2. LiteFiler.exe を起動する。

	3. http://localhost/ にアクセスする。

		ポート番号を変更した場合、別ホストからアクセスする場合は http://<SERVER-HOST>:<PORT>/ として下さい。


■終了方法

	1. LiteFiler.exe を終了する。

		コンソールウィンドウにフォーカスを合わせてエスケープキーを押す。

	2. WHTTR.exe を終了する。

		タスクトレイ上のアイコンを右クリックして「終了」を選択する。


■チュートリアル

	1. 起動方法を実行して、ページを開いて下さい。

	2. サーバーのフォルダ・ファイル一覧が表示されます。

		フォルダのリンクをクリックすると、そのフォルダ内へ
		ファイルのリンクをクリックすると、そのファイルを開きます。


■ソースコード

	根っこ

		https://github.com/stackprobe/Satellite/tree/master/Test_Server/LiteFiler

	LiteFilerService クラス (HttService を継承したクラス)

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/LiteFiler/LiteFiler/LiteFilerService.cs

	HttServer を起動しているところ

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/LiteFiler/LiteFiler/Program.cs




---- SimpleHttService ----


■概要

	シンプル HTTP サーバー


■起動方法

	1. WHTTR.exe を起動する。

	2. SimpleHttService.exe を起動する。

	3. http://localhost/ にアクセスする。

		ポート番号を変更した場合、別ホストからアクセスする場合は http://<SERVER-HOST>:<PORT>/ として下さい。


■終了方法

	1. SimpleHttService.exe を終了する。

		Exit ボタンを押す。

	2. WHTTR.exe を終了する。

		タスクトレイ上のアイコンを右クリックして「終了」を選択する。


■チュートリアル

	1. 起動方法を実行して、ページを開いて下さい。

		リクエストの内容が簡易表示されます。


■ソースコード

	根っこ

		https://github.com/stackprobe/Satellite/tree/master/Test_Server/SimpleHttService

	Test01Service 内部クラス (HttService を継承したクラス)

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/SimpleHttService/SimpleHttService/Form1.cs ---> Test01Service 内部クラス

	HttServer を起動しているところ

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/SimpleHttService/SimpleHttService/Form1.cs ---> Form1_Shown メソッド




---- Test01 ----


■概要

	シンプル HTTP サーバー (2)


■起動方法

	1. WHTTR.exe を起動する。

	2. HTT_RPC_Test01.exe を起動する。

	3. http://localhost/ にアクセスする。

		ポート番号を変更した場合、別ホストからアクセスする場合は http://<SERVER-HOST>:<PORT>/ として下さい。


■終了方法

	1. HTT_RPC_Test01.exe を終了する。

		コンソールウィンドウにフォーカスを合わせてエスケープキーを押す。

	2. WHTTR.exe を終了する。

		タスクトレイ上のアイコンを右クリックして「終了」を選択する。


■チュートリアル

	1. 起動方法を実行して、ページを開いて下さい。

		リクエストの内容が簡易表示されます。


■ソースコード

	根っこ

		https://github.com/stackprobe/Satellite/tree/master/Test_Server/Test01

	Test01Service 内部クラス (HttService を継承したクラス)

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/Test01/HTT_RPC_Test01/Program.cs ---> Test01Service 内部クラス

	HttServer を起動しているところ

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/Test01/HTT_RPC_Test01/Program.cs ---> Main メソッド




---- TestHtmlJpegPngJsonXml ----


■概要

	画像ファイル, Xml, Json を表示するテスト


■起動方法

	1. WHTTR.exe を起動する。

	2. TestHtmlJpegPngJsonXml.exe を起動する。

	3. http://localhost/ にアクセスする。

		ポート番号を変更した場合、別ホストからアクセスする場合は http://<SERVER-HOST>:<PORT>/ として下さい。


■終了方法

	1. TestHtmlJpegPngJsonXml.exe を終了する。

		コンソールウィンドウにフォーカスを合わせてエスケープキーを押す。

	2. WHTTR.exe を終了する。

		タスクトレイ上のアイコンを右クリックして「終了」を選択する。


■チュートリアル

	1. 起動方法を実行して、ページを開いて下さい。

		/test0001.html

			固定 HTML
			https://github.com/stackprobe/Satellite/blob/master/Test_Server/TestHtmlJpegPngJsonXml/TestHtmlJpegPngJsonXml/Program.cs#L44-L56

		/test0002.jpeg

			固定 JPEG
			https://github.com/stackprobe/Satellite/blob/master/Test_Server/TestHtmlJpegPngJsonXml/TestHtmlJpegPngJsonXml/Program.cs#L60-L75

		/test0003.png

			アクセスする度に変わる PNG
			https://github.com/stackprobe/Satellite/blob/master/Test_Server/TestHtmlJpegPngJsonXml/TestHtmlJpegPngJsonXml/Program.cs#L79-L109

		/test0004.json

			固定 JSON
			https://github.com/stackprobe/Satellite/blob/master/Test_Server/TestHtmlJpegPngJsonXml/TestHtmlJpegPngJsonXml/Program.cs#L113-L122

		/test0005.xml

			固定 XML
			https://github.com/stackprobe/Satellite/blob/master/Test_Server/TestHtmlJpegPngJsonXml/TestHtmlJpegPngJsonXml/Program.cs#L126-L137


■ソースコード

	根っこ

		https://github.com/stackprobe/Satellite/tree/master/Test_Server/TestHtmlJpegPngJsonXml

	TestServer0001 内部クラス (HttService を継承したクラス)

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/TestHtmlJpegPngJsonXml/TestHtmlJpegPngJsonXml/Program.cs ---> TestServer0001 内部クラス

	HttServer を起動しているところ

		https://github.com/stackprobe/Satellite/blob/master/Test_Server/TestHtmlJpegPngJsonXml/TestHtmlJpegPngJsonXml/Program.cs ---> Main メソッド



