@echo off
echo -------------------------------------
echo Cleaning SharpOS.AOT.Core
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