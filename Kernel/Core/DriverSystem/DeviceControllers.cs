using System;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.DriverSystem
{
	static class DeviceControllers
	{
		public const uint MaxDeviceControllers = 128;

		private static DeviceController[] deviceControllers = null;
		private static uint count = 0;
		private static SpinLock spinLock;

		static public void Initialize ()
		{
			if (deviceControllers == null) {
				spinLock.Enter ();

				if (deviceControllers == null) {
					deviceControllers = new DeviceController[MaxDeviceControllers];
					count = 0;
				}

				spinLock.Exit ();
			}
		}

		static public bool Add (DeviceController deviceController)
		{
			Initialize ();	// okay to call multiple times

			spinLock.Enter ();

			try {
				uint slot = 0;

				if (count == MaxDeviceControllers)
					return false;

				//TextMode.WriteLine ("Adding Device Controller");
				deviceControllers[count++] = deviceController;

				return true;
			}
			finally {
				spinLock.Exit ();
			}
		}

		static public DeviceController ResolveByName (string controllerName)
		{
			spinLock.Enter ();

			try {

				// find an open slot
				for (uint slot = 0; slot < count; slot++)
					if (deviceControllers[slot].GetName () == controllerName)
						return deviceControllers[slot];

				return null;
			}
			finally {
				spinLock.Exit ();
			}
		}

		//IEnumerable<T> is not yet implemented
		//static public IEnumerable<DeviceControllers> GetAll ()
		//{
		//    for (uint slot = 0; slot < MaxDevices; slot++) {
		//        spinLock.Enter ();
		//        DeviceController deviceController = deviceControllers[slot];
		//        spinLock.Exit ();
		//        if (deviceController != null)
		//            yield return deviceController;
		//    }
		//}

	}
}
