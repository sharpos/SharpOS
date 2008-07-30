using System;
using System.Text;

namespace SharpOS.Kernel.DeviceSystem
{
	public interface IDevice
	{
		string Name { get; }
		Device Parent { get; }
		DeviceStatus Status { get; }
	}
}
