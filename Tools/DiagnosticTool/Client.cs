//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//
//#define LOG_IO

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace SharpOS.Tools.DiagnosticTool {
	public class Client {
		public Client ()
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix)
				pipe = new UnixNamedPipe ();
			else
				pipe = new WindowsNamedPipe ();

			if (Program.Options.SaveFilename != null) {
				using (StreamWriter sw = new StreamWriter (Program.Options.SaveFilename)) {
					sw.Write (pipe.FileName);
				}
			}
		}

		#region Private fields

		private INamedPipe pipe = null;

		#endregion

		#region Statistics

		public uint BytesWritten { get { return pipe.BytesWritten; } }
		public uint BytesRead { get { return pipe.BytesRead; } }

		#endregion

		#region Enums

		public enum Status
		{
			Success,
			Error,
			Incomplete
		}

		#endregion

		#region Connection management

		public Status Open ()
		{
			return pipe.Open ();
		}

		public void Close ()
		{
			pipe.Close ();
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Empty the pipe to remove any garbage
		/// </summary>
		/// <returns></returns>
		public Status Empty ()
		{
			uint count = 256;
			byte[] read = new byte[count];

			// Empty the pipe

			MainWindow.InvokeLog ("Emptying pipe...");
			while (Read (read, ref count) != Status.Error && count==256);

			return Status.Success;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="milliseconds"></param>
		public void Wait (int milliseconds)
		{
			Thread.Sleep (milliseconds);
		}

		/// <summary>
		/// Send through the pipe a ACK command
		/// </summary>
		/// <returns></returns>
		public Status Ack ()
		{
			return Write (0xAC);
		}

		/// <summary>
		/// Send through the pipe a RESEND command
		/// </summary>
		/// <returns></returns>
		public Status Resend ()
		{
			Wait (200);
			Empty ();
			return Write (0xAD);
		}

		/// <summary>
		/// Send through the pipe a RESEND command
		/// </summary>
		/// <returns></returns>
		public Status Cancel ()
		{
			Wait (200);
			Empty ();
			return Write (0xAE);
		}

		#endregion

		#region Write data

		private Status Write (params byte [] buffer)
		{
			return pipe.Write (buffer);
		}

		#endregion

		#region Read data

		private Status Read (byte [] buffer, ref uint count)
		{
			return pipe.Read (buffer, ref count);
		}

		// Read a result message consisting of a size N coded on two bytes (LittleEndian) and N bytes.
		// If an error occurs while reading, returns Status.Error
		// If the read is incomplete, wait for a lapse, empty the pipe, send RESEND and restart the complete read.
		// This could be optimized...
		private Status Read (out byte [] buffer)
		{
			Status s;
			byte[] localBuffer = null;
			buffer = null;

			while(true)
			{
				// Read the size of data to read
				uint count = 0;
				byte[] numReadWritten = new byte [2];
				s = Read (numReadWritten, ref count);
				if (s == Status.Error)
					return Status.Error;

				if (count != 2) {
					// Incomplete
					Resend();
					continue;
				}


				// Read data
				uint msgLength = (uint)numReadWritten [0] | ((uint)numReadWritten [1] << 8);
				localBuffer = new byte [msgLength];
#if LOG_IO
				MainWindow.InvokeLog ("---- Requesting " + msgLength + " bytes");
#endif
				s = Read (localBuffer, ref count);
#if LOG_IO
				MainWindow.InvokeLog ("---- Received " + count + " bytes");
#endif
				if (s == Status.Error)
					return Status.Error;

				if (count != msgLength)
				{
					// Incomplete
					Resend ();
					continue;
				}

				Ack();
				break;
			}

			buffer = localBuffer;
			return Status.Success;
		}

		#endregion

		#region Functions

		public Status fn00_Connect ()
		{

			// Send a connection message to SharpOS.
			MainWindow.InvokeLog ("Try to connect...");
			if (Write (0x00) != Status.Success)
			{
				return Status.Error;
			}

			byte[] read;
			if (Read (out read) != Status.Success)
			{
				return Status.Error;
			}
			if (read [0] == 0)
			{
				MainWindow.InvokeLog ("Connection refused by SharpOS");
				return Status.Error;
			}

			return Status.Success;
		}

		public Status fn01_HelloWorld (out string result)
		{
			result = string.Empty;

			MainWindow.InvokeLog ("Send test command...");
			if (Write (0x01) != Status.Success)
			{
				MainWindow.InvokeLog ("Send failed");
				return Status.Error;
			}

			byte [] read;
			if (Read (out read) != Status.Success)
			{
				MainWindow.InvokeLog ("Receive failed");
				return Status.Error;
			}

			result = System.Text.Encoding.ASCII.GetString (read);
			return Status.Success;
		}

		public Status fn02_MemoryDump (uint address, out byte [] result, System.ComponentModel.BackgroundWorker bgw)
		{
			MainWindow.InvokeLog ("Start memory dump command...");
			Status s = Write (0x02, (byte)(address & 0xFF), (byte)((address >> 8) & 0xFF), (byte)((address >> 16) & 0xFF), (byte)((address >> 24) & 0xFF));
			if (s != Status.Success)
			{
				result = null;
				return Status.Error;
			}

			result = new byte [4096];

			int currentOffset = 0;
			for (int i=0; i < 16; i++)
			{
				bgw.ReportProgress ((100 * i) / 16);

				byte[] read;
				if (Read (out read) != Status.Success)
					break;

				Array.Copy (read, 0, result, currentOffset, read.Length);
				currentOffset += read.Length;

				if (Ack () != Status.Success)
					break;
			}

			return s;
		}


		public class UnitTestDataItem
		{
			public string Source;
			public string Name;
			public bool Result;
		}

		public class UnitTests : List<UnitTestDataItem>
		{
		}

		public Status fn03_UnitTests (UnitTests ut, System.ComponentModel.BackgroundWorker bgw)
		{
			MainWindow.InvokeLog ("Start unit tests command...");
			Status s = Write (0x03);
			if (s != Status.Success)
				return s;

			byte[] read;
			if (Read (out read) != Status.Success)
				return Status.Error;

			int count = 0;
			count += read [0];
			count += (read [1] << 8);
			count += (read [2] << 16);
			count += (read [3] << 24);

			for (int i=0; i < count; i++)
			{
				if (bgw.CancellationPending)
				{
					Cancel ();
					break;
				}

				bgw.ReportProgress ((100 * i) / count);

				UnitTestDataItem item = new UnitTestDataItem ();
				if (Read (out read) != Status.Success)
					continue;
				item.Source = System.Text.Encoding.ASCII.GetString (read);

				if (Read (out read) != Status.Success)
					continue;
				item.Name = System.Text.Encoding.ASCII.GetString (read);

				if (Read (out read) != Status.Success)
					continue;
				item.Result = (read [0] != 0);

				ut.Add (item);

			}
			return Status.Success;
		}

		#endregion
	}
}
