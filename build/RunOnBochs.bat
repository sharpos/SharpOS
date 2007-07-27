@echo off
echo -------------------------------------
echo Running Image on Bochs
echo -------------------------------------
if exist ".\distro\common\disk.img" goto run
echo Could not find disk.img
IF not "%1"=="nopause" pause
exit
:run
cmd /C "bochs -f .\distro\bochs\bochsrc.bxrc -q"
IF not "%1"=="nopause" pause
exit