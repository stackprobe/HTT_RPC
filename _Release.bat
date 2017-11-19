C:\Factory\Tools\RDMD.exe /RC out
C:\Factory\Tools\RDMD.exe /RC out\lib
C:\Factory\Tools\RDMD.exe /RC out\src

COPY /B ..\HTT\HTT\Release\HTT.exe out
COPY /B ..\HTT\doc\HTT.conf out

COPY /B Service.exe out
COPY /B Service.conf out
COPY /B Service.dat out
COPY /B WHTTR\WHTTR\bin\Release\WHTTR.exe out
COPY /B WHTTR\WHTTR\httr_16_00.ico out\Icon_00.dat
COPY /B WHTTR\WHTTR\httr_16_01.ico out\Icon_01.dat
COPY /B WHTTR\WHTTR\httr_16_10.ico out\Icon_10.dat
COPY /B WHTTR\WHTTR\httr_16_11.ico out\Icon_11.dat

C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\Service.exe

COPY /B C:\Factory\Resource\JIS0208.txt out

C:\Factory\Tools\xcp.exe doc out

COPY /B ..\..\Main2\Satellite\Satellite\Satellite\bin\Release\Satellite.dll out\lib

rem ---- satellite.jar ----

C:\Factory\Tools\zcp.exe C:\pleiades\workspace\Test02\bin\charlotte\flowertact out\lib\1\charlotte\flowertact
C:\Factory\Tools\zcp.exe C:\pleiades\workspace\Test02\bin\charlotte\htt        out\lib\1\charlotte\htt
C:\Factory\Tools\zcp.exe C:\pleiades\workspace\Test02\bin\charlotte\satellite  out\lib\1\charlotte\satellite
C:\Factory\Tools\zcp.exe C:\pleiades\workspace\Test02\bin\charlotte\tools      out\lib\1\charlotte\tools

MD out\lib\1\META-INF
> out\lib\1\META-INF\MANIFEST.MF ECHO Manifest-Version: 1.0

C:\Factory\SubTools\zip.exe /P out\lib\satellite.jar out\lib\1
C:\Factory\Tools\RDMD.exe /RD out\lib\1

rem ---- src ----

C:\Factory\Tools\zcp.exe C:\Dev\Main2\Satellite\Satellite\Satellite\Flowertact out\src\1\Flowertact
C:\Factory\Tools\zcp.exe C:\Dev\Main2\Satellite\Satellite\Satellite\Htt        out\src\1\Htt
C:\Factory\Tools\zcp.exe C:\Dev\Main2\Satellite\Satellite\Satellite\Satellite  out\src\1\Satellite

C:\Factory\SubTools\zip.exe /PK out\src\Satellite.dll.src.zip out\src\1 $
C:\Factory\Tools\RDMD.exe /RD out\src\1

C:\Factory\Tools\zcp.exe C:\pleiades\workspace\Test02\src\charlotte\flowertact out\src\1\charlotte\flowertact
C:\Factory\Tools\zcp.exe C:\pleiades\workspace\Test02\src\charlotte\htt        out\src\1\charlotte\htt
C:\Factory\Tools\zcp.exe C:\pleiades\workspace\Test02\src\charlotte\satellite  out\src\1\charlotte\satellite
C:\Factory\Tools\zcp.exe C:\pleiades\workspace\Test02\src\charlotte\tools      out\src\1\charlotte\tools

C:\Factory\SubTools\zip.exe /PK out\src\satellite.jar.src.zip out\src\1 $
C:\Factory\Tools\RDMD.exe /RD out\src\1

rem ----

C:\Factory\SubTools\zip.exe /O out HTT_RPC

PAUSE
