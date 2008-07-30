using System;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.DeviceSystem
{
	public static class DeviceManager
	{
		private const uint MaxDevices = 256;
		private static Device[] devices;
		private static uint deviceCount;
		private static SpinLock spinLock;

		static public void Initalize ()
		{
			devices = new Device[MaxDevices];
			deviceCount = 0;
		}

		static public void Add (Device device)
		{
			spinLock.Enter ();

			if (deviceCount < MaxDevices)
				devices[deviceCount++] = device;

			spinLock.Exit ();
		}

		static public Device[] GetChildrenOf (Device device)
		{
			spinLock.Enter ();

			uint count = 0;
			for (uint i = 0; i < deviceCount; i++)
				if (devices[i] == device.Parent)
					count++;

			Device[] children = new Device[count];

			count = 0;
			for (uint i = 0; i < deviceCount; i++)
				if (devices[i] == device.Parent)
					children[count++] = devices[i];

			spinLock.Exit ();

			return children;
		}

		//static public Device[] GetDevicesOf (Type type)
		//{
		//    spinLock.Enter ();

		//    uint count = 0;
		//    for (uint i = 0; i < deviceCount; i++) {
		//        Type a = devices[i].GetType ();

		//        if ((a.IsSubclassOf (type)) || (a == type) || (a.IsAssignableFrom (type)) || (type.IsAssignableFrom (a)))
		//            count++;
		//    }

		//    Device[] list = new Device[count];

		//    count = 0;
		//    for (uint i = 0; i < deviceCount; i++) {
		//        Type a = devices[i].GetType ();

		//        if ((a.IsSubclassOf (type)) || (a == type) || (a.IsAssignableFrom (type)) || (type.IsAssignableFrom (a)))
		//            list[count++] = devices[i];
		//    }

		//    spinLock.Exit ();

		//    return list;
		//}

		//static public Device[] GetDevicesOf (Type type, string name)
		//{
		//    spinLock.Enter ();

		//    uint count = 0;
		//    for (uint i = 0; i < deviceCount; i++)
		//        if (devices[i].Name == name) {
		//            Type a = devices[i].GetType ();

		//            if ((a.IsSubclassOf (type)) || (a == type) || (a.IsAssignableFrom (type)) || (type.IsAssignableFrom (a)))
		//                count++;
		//        }

		//    Device[] list = new Device[count];

		//    count = 0;
		//    for (uint i = 0; i < deviceCount; i++)
		//        if (devices[i].Name == name) {
		//            Type a = devices[i].GetType ();

		//            if ((a.IsSubclassOf (type)) || (a == type) || (a.IsAssignableFrom (type)) || (type.IsAssignableFrom (a)))
		//                list[count++] = devices[i];
		//        }

		//    spinLock.Exit ();

		//    return list;
		//}

		static public Device[] GetOnlineDevicesWithName (string name)
		{
			spinLock.Enter ();

			uint count = 0;
			for (uint i = 0; i < deviceCount; i++)
				if ((devices[i].Status == DeviceStatus.Online) && (devices[i].Name == name))
					count++;


			Device[] list = new Device[count];

			count = 0;
			for (uint i = 0; i < deviceCount; i++)
				if ((devices[i].Status == DeviceStatus.Online) && (devices[i].Name == name))
					list[count++] = devices[i];

			spinLock.Exit ();

			return list;
		}

		static public Device[] GetOnlineDevices ()
		{
			spinLock.Enter ();

			uint count = 0;
			for (uint i = 0; i < deviceCount; i++)
				if (devices[i].Status == DeviceStatus.Online)
					count++;

			Device[] children = new Device[count];

			count = 0;
			for (uint i = 0; i < deviceCount; i++)
				if (devices[i].Status == DeviceStatus.Online)
					children[count++] = devices[i];

			spinLock.Exit ();

			return children;
		}

		static public Device[] GetDevices ()
		{
			spinLock.Enter ();

			Device[] list = new Device[deviceCount];

			for (uint i = 0; i < deviceCount; i++)
				list[i] = devices[i];

			spinLock.Exit ();

			return list;
		}
	}
}
