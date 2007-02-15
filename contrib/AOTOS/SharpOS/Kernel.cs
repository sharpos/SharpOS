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
        /*public static void BootSectorBegin()
        {
            Asm.BITS32(false);
            Asm.ORG(0x7C00);

            Asm.CLI();

            Asm.MOV(R16.AX, 0x17);
            Asm.MOV(new WordMemory("GDT"), R16.AX);
            Asm.MOV(R16.AX, "GDTStart");
            Asm.MOV(new WordMemory("GDTAddress"), R16.AX);
            Asm.LGDT(new Memory("GDT"));
			
            Asm.MOV(R32.EAX, CR.CR0);
            Asm.OR(R8.AL, 1);
            Asm.MOV(CR.CR0, R32.EAX);

            // HACK
            Asm.JMP((UInt16)0x10, (UInt16)0x7c40);

            Asm.OFFSET(0x40);
            Asm.BITS32(true);

            Asm.XOR(R32.EDI, R32.EDI);
            Asm.XOR(R32.ESI, R32.ESI);
			Asm.MOV(R16.AX, 0x08);
			Asm.MOV(Seg.DS, R16.AX);
            Asm.MOV(Seg.ES, R16.AX);
            Asm.MOV(Seg.FS, R16.AX);
            Asm.MOV(Seg.GS, R16.AX);
            Asm.MOV(Seg.SS, R16.AX);
					
            Asm.MOV(R32.ESP, 0x10000 - 4);

            // A20
            Asm.CALL("SharpOS.SharpOS.KeyboardWait");
            Asm.MOV(R8.AL,0xD1);
            Asm.OUT__AL(0x64);
            Asm.CALL("SharpOS.SharpOS.KeyboardWait");
            Asm.MOV(R8.AL, 0xDF);
            Asm.OUT__AL(0x60);
            Asm.CALL("SharpOS.SharpOS.KeyboardWait");

            ClearScreen(0, 0);
        }*/

        /*public static void Data()
        {
            Asm.DATA("BootMessage", "Grub -> Booting [SharpOS] 2006 by Mircea-Cristian Racasan (\x01)\0");
        }*/

        public unsafe static byte* String(string value)
        {
            return null;
        }

        public unsafe static byte* BootMessage = String("Grub -> Booting [SharpOS] 2007 by Mircea-Cristian Racasan (\x01)");

        public unsafe static void Main()
        {
            WriteMessage(String("OK"));
            //WriteMessage(BootMessage);
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

        /*{
            Asm.MOV(R32.ESI, new DWordMemory(null, R32.EBP, null, 0, 0x08));
            Asm.MOV(R32.EDI, 0xB8000);
            Asm.MOV(R8.AH, 0x0F);

            Asm.LABEL("WriteMessage");
            Asm.LODSB();
            Asm.OR(R8.AL, R8.AL);
            Asm.JZ("Done");
            
            Asm.STOSW();
            Asm.JMP("WriteMessage");

            Asm.LABEL("Done");
        }*/

        /*public static void KeyboardWait()
        {
            Asm.LABEL("A20");
            Asm.IN_AL(0x64);
            Asm.TEST(R8.AL, 2);
            Asm.JNZ("A20");
        }

        public static int Test()
        {
            int x = 10;
            int y = 50;

            x = (x * 91 + y / 45) * (x + 5) + 15 * (y + 300) + (x * y / 500);

            return x / 50;
        }*/

        /*public static void ClearScreen(byte clear_to, byte attrib)
        {
            unsafe
            {
                byte* video = (byte*)0xB8000;
                int pos = 0;

                for (int y = 0; y < 25; y++)
                {
                    for (int x = 0; x < 80; x++)
                    {
                        video[(y * 80 + x) * 2] = (byte) (clear_to * 2);
                        
                        pos = (y * 80 + x) * 2 + 1;
                        video[pos] = (byte) (attrib * 2);
                    }
                }
            }
        }*/
        

        /*public static void BootSectorEnd()
        {
            Asm.DATA("GDT", (UInt16)0x17);
            Asm.DATA("GDTAddress", (UInt32)0);
            Asm.DATA("GDTStart", (byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);

            // Data
            Asm.DATA((byte)0xFF);
            Asm.DATA((byte)0xFF);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x92);
            Asm.DATA((byte)0xCF);
            Asm.DATA((byte)0x00);

            // Code
            Asm.DATA((byte)0xFF);
            Asm.DATA((byte)0xFF);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x00);
            Asm.DATA((byte)0x9A);
            Asm.DATA((byte)0xCF);
            Asm.DATA((byte)0x00);

            Asm.DATA("BootMessage", "Booting [SharpOS] 2006 by Mircea-Cristian Racasan (\x01)\0");

            Asm.OFFSET(510);
            Asm.DATA(0xAA55);
        }*/
    }
}

