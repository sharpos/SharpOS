@echo off
echo -------------------------------------
echo Cleaning Kernel
echo -------------------------------------
cmd /C "nant.bat clean"
echo -------------------------------------
echo DONE
echo -------------------------------------
IF not "%1"=="nopause" (
pause
) ELSE (
if not errorlevel 0 pause
)
exit