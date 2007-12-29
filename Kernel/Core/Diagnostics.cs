// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS.Foundation;
using SharpOS.ADC;

namespace SharpOS
{
    public unsafe static class Diagnostics
    {
        static byte* intermediateStringBuffer = Stubs.StaticAlloc(MaxMessageLength);

        /// <summary>
        /// Defines the maximum allowed length of diagnostic messages
        /// </summary>
        public const int MaxMessageLength = 60;



        #region Diagnostics

        public static void SetErrorTextAttributes()
        {
            TextMode.SetAttributes( TextColor.BrightWhite, TextColor.Red );
        }

        public static void SetWarningTextAttributes()
        {
            TextMode.SetAttributes(TextColor.Brown, TextColor.Black);
        }

        /// <summary>
        /// Induce a kernel panic. Prints the meessage, stage, and error code
        /// then halts the computer.
        /// <summary>
        public unsafe static void Panic(string msg, KernelStage stage, KernelError code)
        {
            PString8* buf = PString8.Wrap(intermediateStringBuffer, MaxMessageLength);

            buf->Concat("Stage: ");
            buf->Concat((int)stage, false);
            buf->ConcatLine();

            buf->Concat("  Error: ");
            buf->Concat((int)code, false);
            buf->ConcatLine();

            TextMode.SaveAttributes();
            SetErrorTextAttributes( );
            TextMode.ClearScreen( );
            TextMode.WriteLine( "SharpOS" );
            TextMode.WriteLine( "Kernel Panic. Your system was halted to ensure your security." );
            TextMode.Write( "  Stage: " ); TextMode.Write( (int) stage, false ); TextMode.WriteLine( );
            TextMode.Write( "  Error: " ); TextMode.Write( (int) code, false ); TextMode.WriteLine( );

            TextMode.WriteLine( );
            TextMode.WriteLine("              ,  ");
            TextMode.WriteLine("      |\\   /\\/ \\/|   ,_");
            TextMode.WriteLine("      ; \\/`     '; , \\_',");
            TextMode.WriteLine("       \\        / ");
            TextMode.WriteLine("        '.    .'    /`.");
            TextMode.WriteLine("    jgs   `~~` , /\\ `\"`");
            TextMode.WriteLine("              .  `\"");

            TextMode.WriteLine( );
            TextMode.WriteLine( "The SharpOS Project would appreciate your feedback on this bug." );

            TextMode.RestoreAttributes();

            Kernel.Halt( );
        }

        public static void Panic(string msg)
        {
            Panic(msg, KernelStage.Unknown, KernelError.Unknown);
        }

        public static void Assert(bool cond, string msg)
        {
            if (!cond)
            {
                TextMode.Write("Assertion Failed: ");
                Panic( msg );
            }
        }

        public static void AssertFalse(bool cond, string msg)
        {
            Assert(!cond, msg);
        }

        public static void AssertZero(uint err, string msg)
        {
            if (err != 0)
            {
                TextMode.Write("Error: ");
                TextMode.Write((int)err);

                Assert(false, msg);
            }
        }

        public static void AssertNonZero(uint err, string msg)
        {
            AssertZero(err == 0 ? 1U : 0U, msg);
        }

        public unsafe static void Warning(string msg)
        {
            TextMode.SaveAttributes();
            PString8* buf = PString8.Wrap(intermediateStringBuffer, MaxMessageLength);

            SetWarningTextAttributes();

            buf->Concat("Warning: ");
            buf->Concat(msg);
            TextMode.WriteLine(buf);

            TextMode.RestoreAttributes();
        }

        public static void Message(string msg)
        {
            TextMode.WriteLine(msg);
        }

        public static void Error(string msg)
        {
            TextMode.SaveAttributes();
            SetErrorTextAttributes();
            TextMode.WriteLine(msg);
            TextMode.RestoreAttributes();
        }

        public unsafe static void Error(PString8* msg)
        {
            TextMode.SaveAttributes();
            SetErrorTextAttributes();
            TextMode.WriteLine(msg);
            TextMode.RestoreAttributes();
        }

        #endregion
    }
}
