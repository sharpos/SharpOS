@echo off

if exist "SharpOS.Kernel.dll" goto create
echo -------------------------------------
echo Could not find SharpOS.Kernel.dll
goto the_exit

:create
cmd /C "CreateImage.bat nopause"

if exist "disk.img" goto run
echo -------------------------------------
echo Could not find disk.img
goto the_exit

:run
cmd /C "RunOnBochs.bat nopause"

:the_exit
echo -------------------------------------
IF not "%1"=="nopause" pause
exit