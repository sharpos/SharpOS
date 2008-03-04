//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//  Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.ADC {
	public class Serial {

		[AOTAttr.ADCStub]
		public static void Setup ()
		{
		}

		public static bool Initialized 
		{ 
			[AOTAttr.ADCStub]
			get { return false; } 
		}

		public static SerialPort COM1 { [AOTAttr.ADCStub]get { return null; }}
		public static SerialPort COM2 { [AOTAttr.ADCStub]get { return null; }}
		public static SerialPort COM3 { [AOTAttr.ADCStub]get { return null; }}
		public static SerialPort COM4 { [AOTAttr.ADCStub]get { return null; }}
	}

	public unsafe abstract class SerialPort {

		#region Write family

		public abstract void Write (byte ch);

		public void Write (string str)
		{
			int strLength = str.Length;
			for (int x = 0; x < strLength; x++)
			{
				Write ((byte)str [x]);
			}
 		}

		public unsafe void Write (byte* str)
 		{
			int strLength = ByteString.Length (str);
			for (int x = 0; x < strLength; x++)
			{
				Write (str [x]);
			}
 		}

		public unsafe void Write (CString8* str)
		{
			int strLength = str->Length;
			for (int x = 0; x < strLength; x++)
			{
				Write (str->Pointer [x]);
			}
		}

		public void WriteLine ()
 		{
			Write ((byte) '\n');
 		}

		public void WriteLine (string str)
 		{
			Write (str);
			WriteLine ();
 		}

		public unsafe void WriteNumber (uint number, bool hex)
		{
			byte* buffer = stackalloc byte [32];
			int length;

			length = SharpOS.Kernel.Foundation.Convert.ToString (number, hex, buffer, 32, 0);

			for (int x = 0; x < length; ++x)
				Write (buffer [x]);


			/*
			// this doesn't play well when you're dumping something trough 
			//	serial when you ran out of memory...
			CString8* str = Foundation.Convert.ToString (number, hex);
			Write (str);
			ADC.MemoryManager.Free (str);
			*/
		}

		public unsafe void WriteNumber (int number, bool hex)
		{
			byte* buffer = stackalloc byte [32];
			int length;

			length = SharpOS.Kernel.Foundation.Convert.ToString (number, hex, buffer, 32, 0);

			for (int x = 0; x < length; ++x)
				Write (buffer [x]);


			/*
			// this doesn't play well when you're dumping something trough 
			//	serial when you ran out of memory...
			CString8* str = Foundation.Convert.ToString (number, hex);
			Write (str);
			ADC.MemoryManager.Free (str);
			*/
		}

		public unsafe void Write (uint number)
		{
			WriteNumber (number, false);
		}

		public unsafe void Write (int number)
		{
			WriteNumber (number, false);
		}

		public unsafe void Write (int number, bool hex)
		{
			WriteNumber (number, hex);
		}

		public unsafe void Write (string str, int number, bool hex)
		{
			Write (str);
			Write (number, hex);
		}

		public unsafe void Write (string str, int number)
		{
			Write (str, number, false);
		}

		public unsafe void WriteLine (string str, int number, bool hex)
		{
			Write (str, number, hex);
			WriteLine ();
		}

		public unsafe void WriteLine (string str, int number)
		{
			Write (str, number, false);
			WriteLine ();
		}

		#endregion

		#region Read family

		public abstract byte Read ();

		#endregion

		#region Data received event

		unsafe uint* dataReceivedEvent = (uint*)Stubs.StaticAlloc (sizeof (uint) * EntryModule.MaxEventHandlers);

		public unsafe EventRegisterStatus RegisterDataReceivedEvent (uint address)
		{
			int firstAvailableSlot = -1;

			for (int x = 0; x < EntryModule.MaxEventHandlers; ++x)
			{
				if (dataReceivedEvent [x] == address)
				{
					return EventRegisterStatus.AlreadySubscribed;
				}
				if (firstAvailableSlot == -1 && dataReceivedEvent [x] == 0)
				{
					firstAvailableSlot = x;
				}
			}

			if (firstAvailableSlot == -1)
			{
				return EventRegisterStatus.CapacityExceeded;
			}

			dataReceivedEvent [firstAvailableSlot] = address;
			return EventRegisterStatus.Success;
		}

		public unsafe EventRegisterStatus UnregisterDataReceivedEvent (uint address)
		{
			for (int x = 0; x < EntryModule.MaxEventHandlers; ++x)
			{
				if (dataReceivedEvent [x] == address)
				{
					dataReceivedEvent [x] = 0;
				}
			}
			return EventRegisterStatus.Success;
		}

		public unsafe void OnDataReceived ()
		{
			for (int x = 0; x < EntryModule.MaxEventHandlers; ++x)
			{
				if (dataReceivedEvent [x] == 0)
					continue;

				MemoryUtil.Call ((void*)dataReceivedEvent [x], (void*)0);
			}
		}

		public abstract void DisableDataReceivedInterrupt ();

		public abstract void EnableDataReceivedInterrupt ();

		#endregion
	}
}