@echo off
cmd /C "Clean.bat" nopause
if not errorlevel 0 exit
cmd /C "Build.bat" nopause
if not errorlevel 0 exit
IF not "%1"=="nopause" pause
exit