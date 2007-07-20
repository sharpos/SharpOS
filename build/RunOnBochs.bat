@echo off
echo -------------------------------------
echo Running Image on Bochs
echo -------------------------------------
if exist "disk.img" goto run
echo Could not find disk.img
IF not "%1"=="nopause" pause
exit
:run
cmd /C "Bochs -f ..\util\bochsrc.txt -q"
IF not "%1"=="nopause" pause
exit