using System;
using System.Text;

namespace SharpOS.Kernel.DeviceSystem
{
	///TODO: Add API to set Serial settings, like baud rate, parity, etc.
	public interface ISerialDevice
	{
		 void Write (byte ch);
		 int ReadByte ();
	}
}
