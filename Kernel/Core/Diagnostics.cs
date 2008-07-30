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

using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;
using System.Globalization;

namespace SharpOS.Kernel {
	public unsafe static class Diagnostics {
		static byte* intermediateStringBuffer = Stubs.StaticAlloc (MaxMessageLength);

		/// <summary>
		/// Defines the maximum allowed length of diagnostic messages
		/// </summary>
		public const int MaxMessageLength = 60;



		#region Diagnostics

		public static void SetErrorTextAttributes ()
		{
			TextMode.SetAttributes (TextColor.BrightWhite, TextColor.Red);
		}

		public static void SetWarningTextAttributes ()
		{
			TextMode.SetAttributes (TextColor.Brown, TextColor.Black);
		}

		/// <summary>
		/// Induce a kernel panic. Prints the meessage, stage, and error code
		/// then halts the computer.
		/// <summary>
		public unsafe static void Panic (string msg, KernelStage stage, KernelError code)
		{
			TextMode.Write ("Panic: ");
			TextMode.WriteLine (msg);
#if false
			PString8* buf = PString8.Wrap (intermediateStringBuffer, MaxMessageLength);

			buf->Concat ("Stage: ");
			buf->Concat ((int) stage, false);
			buf->ConcatLine ();

			buf->Concat ("  Error: ");
			buf->Concat ((int) code, false);
			buf->ConcatLine ();

			TextMode.SaveAttributes ();
			SetErrorTextAttributes ();
			TextMode.ClearScreen ();
			TextMode.WriteLine ("SharpOS");
			TextMode.WriteLine ("Kernel Panic. Your system was halted to ensure your security.");
			TextMode.Write ("  Stage: ");
			TextMode.Write ((int) stage, false);
			TextMode.WriteLine ();
			TextMode.Write ("  Error: ");
			TextMode.Write ((int) code, false);
			TextMode.WriteLine ();

			TextMode.WriteLine ();
			TextMode.WriteLine ("              ,  ");
			TextMode.WriteLine ("      |\\   /\\/ \\/|   ,_");
			TextMode.WriteLine ("      ; \\/`     '; , \\_',");
			TextMode.WriteLine ("       \\        / ");
			TextMode.WriteLine ("        '.    .'    /`.");
			TextMode.WriteLine ("    jgs   `~~` , /\\ `\"`");
			TextMode.WriteLine ("              .  `\"");

			TextMode.WriteLine ();
			TextMode.WriteLine ("The SharpOS Project would appreciate your feedback on this bug.");

			TextMode.RestoreAttributes ();
#endif
			EntryModule.Halt ();
		}

		public static void Panic (string msg)
		{
			Panic (msg, KernelStage.Unknown, KernelError.Unknown);
		}

		public static void Assert (bool cond, string msg)
		{
			if (!cond) {
				Barrier.Enter();

				TextMode.Write ("Assertion Failed: ");
				TextMode.Write (msg);
				
				//if (Serial.Initialized)
				//{
					Debug.COM1.WriteLine ("");
					Debug.COM1.WriteLine ("----------------- ");
					Debug.COM1.WriteLine ("Assertion Failed: ");
					Debug.COM1.WriteLine (msg);
					
					Debug.COM1.WriteLine ("=============================================================");
					Debug.COM1.WriteLine ("Stack Trace:");
				
					ExceptionHandling.DumpCallingStack();
				//}			
				Panic (msg);
				
				Barrier.Exit();
			}
		}

		public static void AssertFalse (bool cond, string msg)
		{
			Assert (!cond, msg);
		}

		public static void AssertZero (uint err, string msg)
		{
			if (err != 0) {
				TextMode.Write ("Error: ");
				TextMode.Write ((int) err);

				Assert (false, msg);
			}
		}

		public static void AssertNonZero (uint err, string msg)
		{
			AssertZero (err == 0 ? 1U : 0U, msg);
		}

		public unsafe static void Warning (string msg)
		{
			TextMode.SaveAttributes ();
			PString8* buf = PString8.Wrap (intermediateStringBuffer, MaxMessageLength);

			SetWarningTextAttributes ();

			buf->Concat ("Warning: ");
			buf->Concat (msg);
			TextMode.WriteLine (buf);

			TextMode.RestoreAttributes ();
		}

		public static void Message (string msg)
		{
			TextMode.WriteLine (msg);
		}

		public static void Message(string msg, int value)
		{
			TextMode.Write(msg);
			TextMode.Write(value);
			TextMode.WriteLine();
		}

		public static void Error (string msg)
		{
			TextMode.SaveAttributes ();
			SetErrorTextAttributes ();
			TextMode.WriteLine (msg);
			TextMode.RestoreAttributes ();
		}

		public unsafe static void Error (PString8* msg)
		{
			TextMode.SaveAttributes ();
			SetErrorTextAttributes ();
			TextMode.WriteLine (msg);
			TextMode.RestoreAttributes ();
		}


		private static StringBuilder* formatDump_sb = (StringBuilder*)0; //StringBuilder.CREATE(4 * 4096);
		private static StringBuilder* formatDump_asc = (StringBuilder*)0; //StringBuilder.CREATE(16);
		private static StringBuilder* formatDump_tmp = (StringBuilder*)0; //StringBuilder.CREATE(1);
		public unsafe static CString8* FormatDump(byte* buffer, int count, int bytesPerRow)
		{
			if (formatDump_sb == (StringBuilder*)0) formatDump_sb = StringBuilder.CREATE(4 * 4096);
			if (formatDump_asc == (StringBuilder*)0) formatDump_asc = StringBuilder.CREATE(16);
			if (formatDump_tmp == (StringBuilder*)0) formatDump_tmp = StringBuilder.CREATE(1);
			formatDump_sb->Clear();
			formatDump_asc->Clear();
			formatDump_tmp->Clear();

			// Dump memory into 3 parts : page offset, Hexa dump, ASCII dump
			//
			// oooo | hh hh hh hh hh hh hh hh hh hh hh hh hh hh hh hh | aaaaaaaaaaaa\n  --   11 + 4n, where n is the number of column (16)
			//
			for (int i = 0; i < count; i++)
			{
				// First byte of the line, write the offset
				if (i % bytesPerRow == 0)
				{
					int offset = (int)buffer + i;
					// sb.AppendFormat("{0:X4} | ", offset);
					if (offset < 0x10) formatDump_sb->AppendNumber(0);
					if (offset < 0x100) formatDump_sb->AppendNumber(0);
					if (offset < 0x1000) formatDump_sb->AppendNumber(0);
					if (offset < 0x10000) formatDump_sb->AppendNumber(0);
					if (offset < 0x100000) formatDump_sb->AppendNumber(0);
					if (offset < 0x1000000) formatDump_sb->AppendNumber(0);
					if (offset < 0x10000000) formatDump_sb->AppendNumber(0);
					formatDump_sb->AppendNumber(offset, true);
					formatDump_sb->Append(" | ");
				}

				// Write the byte
				//sb.AppendFormat("{0:X2} ", buffer[i]);
				if (buffer[i] < 0x10) formatDump_sb->AppendNumber(0);
				formatDump_sb->AppendNumber(buffer[i], true);
				formatDump_sb->Append(" ");

				// Append char to secondary StringBuilder if printable.Otherwise, append a dot.
				//char c = Convert.ToChar(buffer[i]);
				//asc.Append(CharIsPrintable(c) ? c : '.');
				formatDump_tmp->Clear();
				formatDump_tmp->AppendChar(buffer[i]);
				if (buffer[i] != 0x0A && formatDump_tmp->buffer->Length > 0)
				{
					formatDump_asc->AppendChar(buffer[i]);
				}
				else
				{
					formatDump_asc->Append(".");
				}

				// Last byte of the line, write the ASCII equivalent
				if (i % bytesPerRow == bytesPerRow - 1)
				{
					//sb.Append("| ");
					//sb.Append(asc.ToString());
					//sb.AppendLine();
					//asc = new StringBuilder(bytesPerRow);
					formatDump_sb->Append("| ");
					formatDump_sb->Append(formatDump_asc->buffer);
					formatDump_sb->Append("\n");
					formatDump_asc->Clear();
				}
			}


			// Last line, fill with spaces
			if ((count - 1) % bytesPerRow != (bytesPerRow - 1))
			{
				for (int i = 0; i < (bytesPerRow - 1) - (count - 1) % bytesPerRow; i++)
					formatDump_sb->Append("   ");

				formatDump_sb->Append("| ");
				formatDump_sb->Append(formatDump_asc->buffer);
				formatDump_sb->Append("\n");
				formatDump_asc->Clear();
			}

			return formatDump_sb->buffer;
		}

		#endregion
	}
}
