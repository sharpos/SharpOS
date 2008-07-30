//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Kernel.DeviceSystem;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.ADC
{
	public static class Debug
	{
		static public DebugHandler COM1;
		static public DebugHandler COM2;

		static public void Setup ()
		{
			COM1 = new DebugHandler (null);
			COM2 = new DebugHandler (null);
		}

		static public void Setup2 ()
		{
			foreach (Device device in DeviceManager.GetOnlineDevicesWithName ("COM_0x3F8"))
				if (device is ISerialDevice) {
					COM1 = new DebugHandler ((ISerialDevice)device);
					TextMode.WriteLine ("Sending: Hello!");
					COM1.WriteLine ("Hello!");
					break;
				}

			foreach (Device device in DeviceManager.GetOnlineDevicesWithName ("COM_0x2F8"))
				if (device is ISerialDevice) {
					COM2 = new DebugHandler ((ISerialDevice)device);
					break;
				}

		}

	}

	public class DebugHandler
	{
		protected ISerialDevice com;

		public ISerialDevice COM
		{
			get { return com; }
		}

		public DebugHandler (ISerialDevice com)
		{
			this.com = com;
		}

		public byte Read ()
		{
			if (com == null)
				return 0;

			return (byte)com.ReadByte ();	// fixme
		}

		public void Write (char c)
		{
			//TextMode.WriteChar ((byte)c);
			if (com == null)
				return;

			com.Write ((byte)c);
		}

		public void Write (string s)
		{
			foreach (char c in s)
				Write (c);
		}

		public void WriteLine (string s)
		{
			Write (s);
			WriteLine ();
		}

		public void WriteLine ()
		{
			Write ('\n');
		}

		public unsafe void Write (CString8* cs)
		{
			for (int i = 0; i < cs->Length; i++)
				Write (cs->GetChar (i));
		}

		public unsafe void WriteLine (CString8* cs)
		{
			Write (cs);
			WriteLine ();
		}

		public void Write (int i)
		{
			if (com == null)
				return;

			Write (i.ToString ());
		}

		public void WriteLine (int i)
		{
			Write (i);
			WriteLine ();
		}

		public void Write (int i, bool hex)
		{
			if (com == null)
				return;

			if (hex)
				Write (i.ToString ("X"));
			else
				Write (i.ToString ());
		}

		public void WriteLine (int i, bool hex)
		{
			WriteLine (i, hex);
			WriteLine ();
		}

		public void WriteNumber (int i, bool hex)
		{
			Write (i, hex);
		}

		public void Write (uint i)
		{
			Write (i);
			WriteLine ();
		}

		public void WriteLine (uint i)
		{
			Write (i);
			WriteLine ();
		}

		public void Write (string s, uint i)
		{
			Write (s);
			Write (i);
		}

		public void WriteLine (string s, uint i)
		{
			Write (s);
			Write (i);
			WriteLine ();
		}

		public void Write (string s, int i)
		{
			Write (s);
			Write (i);
		}

		public void WriteLine (string s, int i)
		{
			Write (s);
			Write (i);
			WriteLine ();
		}
	}
}
