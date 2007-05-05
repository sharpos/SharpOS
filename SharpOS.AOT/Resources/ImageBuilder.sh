#!/bin/sh

TEMP="/tmp/sharpos"
SUDO="sudo"

if [ -z "$1" -o -z "$2" ]; then
	echo "ImageBuilder.sh [kernel.bin] [kernel.img]"
	exit 1
fi

in="$1"
out="$2"

if [ ! -f "$out" ]; then
	echo "Error (ImageBuilder.sh): Image file does not exist"
fi

test "`whoami`" = "root" && unset SUDO

mkdir -p $TEMP
$SUDO mount "$out" "$TEMP" -o loop
$SUDO cp "$in" "$TEMP/SharpOS.bin"
$SUDO umount "$TEMP"
rmdir "$TEMP"
