@echo off
cmd /C "Rebuild.bat nopause"
if not errorlevel 0 exit
cd ..\build
cmd /C "CreateImageAndRunOnBochs.bat nopause"
if not errorlevel 0 exit
IF not "%1"=="nopause" pause
exit