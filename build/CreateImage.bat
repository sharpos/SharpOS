@echo off
if exist "SharpOS.Kernel.dll" goto create
echo -------------------------------------
echo Compiling and Creating image
echo -------------------------------------
echo Could not find SharpOS.Kernel.dll
IF not "%1"=="nopause" pause
exit
:create
if not exist "SharpOS.bin" goto disk
echo -------------------------------------
echo Deleting "SharpOS.bin"
echo -------------------------------------
del SharpOS.bin
echo.
:disk
if not exist "disk.img" goto create
echo -------------------------------------
echo Deleting "disk.img"
echo -------------------------------------
del disk.img
echo.
:create
echo -------------------------------------
echo Compiling and Creating image
echo -------------------------------------
SharpOS.AOT.exe SharpOS.Kernel.dll -v -image -bin-out:SharpOS.bin -out:.\distro\common\disk.img
IF not "%1"=="nopause" (
pause
) ELSE (
if not errorlevel 0 pause
)
exit