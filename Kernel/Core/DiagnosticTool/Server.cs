//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.DiagnosticTool
{
	unsafe class Server
	{
		#region Constants

		const string DATARECEIVED_HANDLER = "DATARECEIVED_HANDLER";

		#endregion

		public static void Setup ()
		{
			//Debug.COM2.RegisterDataReceivedEvent (Stubs.GetFunctionPointer (DATARECEIVED_HANDLER)); /// FIXME!!!
			Debug.COM2.Write ((byte)0xAC);
		}

		//private static void Send (byte [] data)
		//{
		//  int len = data.Length;
		//  TextMode.Write ("\nSending ", len);
		//  TextMode.Write (" bytes...");
		//  Debug.COM2.Write ((byte)(len & 0xFF));
		//  Debug.COM2.Write ((byte)((len >> 8) & 0xFF));
		//  for (int i = 0; i < len; i++)
		//  {
		//    Debug.COM2.Write (data [i]);
		//  }
		//  TextMode.Write ("Completed.");
		//}

		private static unsafe void Send (byte* data, int len)
		{
			//TextMode.Write ("Sending ", len);
			//TextMode.WriteLine (" bytes...");
			Debug.COM2.Write ((byte)(len & 0xFF));
			Debug.COM2.Write ((byte)((len >> 8) & 0xFF));
			for (int i = 0; i < len; i++) {
				Debug.COM2.Write (data[i]);
			}
			//TextMode.WriteLine ("Completed.");
		}

		private static unsafe bool SafeSend (byte* data, int len)
		{
			byte ack = 0;
			do {
				Send (data, len);
				ack = Debug.COM2.Read ();
			} while (ack == 0xAD); // resend the packet
			return (ack == 0xAC);
		}

		private static unsafe void Send (byte data)
		{
			byte* buffer = stackalloc byte[1];
			//byte[] buffer = new byte [1];
			buffer[0] = data;
			Send (buffer, 1);
		}

		private static unsafe void Send (int data)
		{
			byte* buffer = stackalloc byte[4];
			buffer[0] = (byte)(data & 0xFF);
			buffer[1] = (byte)((data >> 8) & 0xFF);
			buffer[2] = (byte)((data >> 16) & 0xFF);
			buffer[3] = (byte)((data >> 24) & 0xFF);
			Send (buffer, 4);
		}

		private static unsafe bool SafeSend (int data)
		{
			byte* buffer = stackalloc byte[4];
			buffer[0] = (byte)(data & 0xFF);
			buffer[1] = (byte)((data >> 8) & 0xFF);
			buffer[2] = (byte)((data >> 16) & 0xFF);
			buffer[3] = (byte)((data >> 24) & 0xFF);
			return SafeSend (buffer, 4);
		}

		private static unsafe void Send (string msg)
		{
			byte* buffer = stackalloc byte[msg.Length];
			//byte[] buffer = new byte [msg.Length];
			for (int i = 0; i < msg.Length; i++)
				buffer[i] = (byte)msg[i];
			Send (buffer, msg.Length);
		}


		static bool connected = false;

		[SharpOS.AOT.Attributes.Label (DATARECEIVED_HANDLER)]
		static unsafe void DataReceivedHandler (byte data)
		{
			//			Debug.COM2.DisableDataReceivedInterrupt ();

			byte fn = Debug.COM2.Read ();
			if (fn != 0 && !connected)
				return;

			switch (fn) {
				case 0x00: // ask for connection
					if (!connected) {
						//TextMode.WriteLine ("Sending OK...");
						Send ((byte)1);
						connected = true;
					}
					else {
						//TextMode.WriteLine ("Sending NOK...");
						Send ((byte)0);
					}
					break;

				case 0x01: // test
					Send ("Hello from SharpOS");
					break;

				case 0x02: // dump memory
					//TextMode.WriteLine ("Memory dump request...");

					// Memory dump needs one 32bits argument.
					int address = 0;
					address += Debug.COM2.Read ();
					address += (Debug.COM2.Read () << 8);
					address += (Debug.COM2.Read () << 16);
					address += (Debug.COM2.Read () << 24);

					// Byte dump is splitted into 16 packets of 256 bytes
					for (int counter = 0; counter < 16; counter++) {
						//TextMode.Write (address + 256 * counter, true);
						//TextMode.WriteLine (": dumping memory...");
						if (!SafeSend ((byte*)(address + 256 * counter), 256))
							break;
					}
					break;

				case 0x03:
					int count = Testcase.GetTestCount ();
					if (!SafeSend (count))
						break;

					Testcase.TestRecord* test = Testcase.GetFirstTest ();
					for (test = Testcase.GetFirstTest (); test != null; test = Testcase.GetNextTest (test)) {
						if (!SafeSend ((byte*)(test->Source->Pointer), test->Source->Length))
							break;

						if (!SafeSend ((byte*)(test->Name->Pointer), test->Name->Length))
							break;

						if (!SafeSend ((byte)(test->Result ? 0x01 : 0x00)))
							break;

					}
					break;

				default:
					break;

			}

			//			Debug.COM2.EnableDataReceivedInterrupt ();
		}

	}
}
