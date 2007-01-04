#!/bin/sh

#../AOTOS/bin/Debug/AOTOS.exe 
cp blank.img disk.img
dd if=../AOTOS/bin/Debug/SharpOS.bin of=disk.img conv=notrunc
./bochs.exe -q
