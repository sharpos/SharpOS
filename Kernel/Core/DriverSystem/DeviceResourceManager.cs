using System;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.DriverSystem.Drivers.Block;

namespace SharpOS.Kernel.DriverSystem
{
	static class DeviceResourceManager
	{
		public const uint MaxDeviceResources = 128;

		private static DeviceResource[] deviceResources = null;
		private static SpinLock spinLock;
		private static DeviceResource NullResource;
		private static uint resourceCount = 0;
		private static uint resourceID = 0;
		private static SpinLock spinLock2;

		static public void Initialize ()
		{
			if (deviceResources == null) {
				spinLock.Enter ();

				if (deviceResources == null) {
					deviceResources = new DeviceResource[MaxDeviceResources];
					resourceCount = 0;

					NullResource.ID = 0;
					NullResource.Status = DeviceResourceStatus.UnableToLocated;
				}

				spinLock.Exit ();
			}
		}

		public static uint GetUniqueResourceID ()
		{
			try {
				spinLock2.Enter ();

				return ++resourceID;
			}
			finally {
				spinLock2.Exit ();
			}

		}

		static public DeviceResource Add (DeviceResource deviceResource)
		{
			Initialize ();	// okay to call multiple times

			spinLock.Enter ();

			try {
				uint slot = 0;

				if (resourceCount == MaxDeviceResources) {
					deviceResource.Status = DeviceResourceStatus.MaxedOut;
				}
				else {
					//TextMode.WriteLine ("Adding Resource");
					deviceResources[resourceCount] = deviceResource;
					resourceCount++;
				}

			}
			finally {
				spinLock.Exit ();
			}

			return deviceResource;
		}

		static public DeviceResource ResolveByName (string resourceName)
		{
			spinLock.Enter ();

			try {
				DeviceResource device = NullResource;

				// find an open slot
				for (uint slot = 0; slot < resourceCount; slot++) {
					if (deviceResources[slot].Name == resourceName) // ?? 
						return deviceResources[slot];
				}

				return device;

			}
			finally {
				spinLock.Exit ();
			}
		}

		static public DeviceResource ResolveByID (uint id)
		{
			spinLock.Enter ();

			try {
				DeviceResource device = NullResource;

				// find an open slot
				for (uint slot = 0; slot < resourceCount; slot++) {
					if (deviceResources[slot].ID == id)
						return deviceResources[slot];
				}

				return device;

			}
			finally {
				spinLock.Exit ();
			}
		}

		static public DeviceResource ResolveByBlockDevice (IBlockDevice device)
		{
			spinLock.Enter ();

			try {
				// find an open slot
				for (uint slot = 0; slot < resourceCount; slot++) {
					if (deviceResources[slot].BlockDevice == device)
						return deviceResources[slot];
				}

				return NullResource;

			}
			finally {
				spinLock.Exit ();
			}
		}

		// IEnumerable<T> is not yet implemented
		//static public IEnumerable<DeviceResource> GetAllItems ()
		//{
		//    for (uint slot = 0; slot < MaxDevices; slot++) {
		//        if (deviceResources[slot].Status != DeviceResourceStatus.None)
		//            yield return deviceResources[slot];
		//    } 
		//}

		static public DeviceResource GetBySlot (uint slot)
		{
			if (slot >= resourceCount)
				return NullResource;

			return deviceResources[slot];
		}

	}
}
