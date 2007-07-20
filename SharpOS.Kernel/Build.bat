@echo off
echo -------------------------------------
echo Compiling Kernel
echo -------------------------------------
cmd /C "@nant.bat"
echo -------------------------------------
echo DONE
echo -------------------------------------
IF not "%1"=="nopause" (
pause
) ELSE (
if not errorlevel 0 pause
)
exit