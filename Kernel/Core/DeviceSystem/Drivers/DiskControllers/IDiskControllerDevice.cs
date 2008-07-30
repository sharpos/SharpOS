using System;

namespace SharpOS.Kernel.DeviceSystem.DiskController
{
	public interface IDiskControllerDevice
	{
		void Initialize ();

		bool Open (uint driveNbr);
		bool Release (uint driveNbr);

		bool ReadBlock (uint driveNbr, uint block, uint count, byte[] data);
		bool WriteBlock (uint driveNbr, uint block, uint count, byte[] data);

		uint GetSectorSize (uint driveNbr);
		uint GetTotalSectors (uint driveNbr);
		bool CanWrite (uint driveNbr);
	}
}
