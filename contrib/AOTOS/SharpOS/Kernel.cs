/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS
{
    public class Kernel
    {
        public unsafe static byte* String(string value)
        {
            return null;
        }

        public unsafe static byte* BootMessage = String("Grub -> Booting [SharpOS] 2007 by Mircea-Cristian Racasan (\x01)");

        public unsafe static void Main()
        {
            WriteMessage(BootMessage);
        }

        public unsafe static void WriteMessage(byte* message)
        {
            byte* video = (byte*)0xB8000;

            int i = 0;

            while (message[i] != 0)
            {
                *video++ = message[i++];
                *video++ = 7;
            }
        }
    }
}

