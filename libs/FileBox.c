#include "FileBox.h"

static char *GetRootDir(void)
{
	static char *dir;

	if(!dir)
		dir = combine(getEnvLine("TMP"), "{2c6563c5-796f-4067-9254-96d4d4d1bfeb}");

	return dir;
}
static void INIT(void)
{
	{
		static int passed;

		if(passed)
			return;

		passed = 1;
	}

	createDirIfNotExist(GetRootDir());
}
static char *GetNewPath(void)
{
	INIT();
	return combine_cx(GetRootDir(), MakeRandID());
}

void FB_Clear(void)
{
	INIT();
	coExecute_x(xcout("DEL /Q \"%s\\*\"", GetRootDir()));
}
char *FB_AddPath(void)
{
	return GetNewPath();
}
char *FB_AddFile_MvCr(char *rFile)
{
	char *wFile = FB_AddPath();

	moveFile(rFile, wFile);
	createFile(rFile);
	return wFile;
}
