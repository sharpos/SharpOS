#!/bin/sh

#../AOTOS/bin/Debug/AOTOS.exe 
#cp blank.img disk.img
#dd if=../AOTOS/bin/Debug/SharpOS.bin of=disk.img conv=notrunc
/cygdrive/h/Downloads/Temp/vfd21-050404/vfd open disk.img
cp ../AOTOS/bin/Debug/SharpOS.bin /cygdrive/b/
#gzip -c -9 ../AOTOS/bin/Debug/SharpOS.bin > /cygdrive/b/SharpOS.bin
/cygdrive/h/Downloads/Temp/vfd21-050404/vfd close b:
./bochs.exe -q
