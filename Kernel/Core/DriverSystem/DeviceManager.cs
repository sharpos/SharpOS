// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.DriverSystem {

	// TODO: would be nice to be able to use List<IDevice> internally ...
	// TODO: eventually need events to notify subscribers when 
	//		devices have been changed/added/removed
	// TODO: ability to register root devices
	public class DeviceManager : IDeviceManager	{

		#region Constructor
		public DeviceManager(IHardwareResourceManager _manager)
		{
			manager = _manager;
		}
		#endregion

		#region HardwareResourceManager
		private IHardwareResourceManager manager;
		#endregion

		#region Devices
		public IDevice[]	Devices 
		{
			get
			{
				return Architecture.RootDevices;
			}
		}
		#endregion

		#region Setup
		public void Setup()
		{
			InitializeDevices(Architecture.RootDevices);
		}
		#endregion
		
		#region InitializeDriver (internal)
		internal bool InitializeDriver(IDevice device, IDriver driver)
		{
			if (device == null)
				throw new ArgumentNullException("device");
			if (driver == null)
				throw new ArgumentNullException("driver");

			IDriverContext context = null;
			try
			{
				context = manager.CreateDriverContext(device);
				if (driver.Initialize(context))
				{
					// eventually use a dictionary in the device manager instead
					driver.DriverContext = context;
					return true;
				}
			}
			catch { }
			if (context != null)
				context.Release();
			return false;
		}
		#endregion
		
		#region InitializeDevices (internal)
		internal void InitializeDevices(IDevice[] devices)
		{
			if (devices == null)
				throw new ArgumentNullException("devices");

			// ..initialize devices
			for (int i = 0; i < devices.Length; i++)
			{
				IDevice device = devices[i];
				if (device == null || // just in case...
					(	
						// *should* never happen.. 
						device.Driver != null &&
						device.Driver.IsInitialized
					))
					continue;

				device.Enabled = false;
				if (device.Driver != null)
				{
					if (InitializeDriver(device, device.Driver))
					{
						device.Enabled = true;
						continue;
					} else
						device.Driver = null;
				}

				IDriver[] foundDrivers = DriverManager.Find(device);
				for (int j = 0; j < foundDrivers.Length; j++)
				{
					IDriver driver = foundDrivers[j];					
					if (InitializeDriver(device, driver))
					{
						device.Driver = driver;
						device.Enabled = true;
						break;
					}
				}
			}

			// ..initialize child devices
			for (int i = 0; i < devices.Length; i++)
			{
				IDevice device = devices[i];
				if (device == null || // just in case...
					device.Driver == null ||
					!device.Enabled ||
					!device.Driver.IsInitialized ||
					!device.Driver.HasSubDevices)
					continue;

				try
				{
					IDevice[] childDevices;
					if (device.Driver.GetSubDevices(out childDevices))
					{
						InitializeDevices(childDevices);
						continue;
					}
				}
				catch
				{
					if (device.Driver != null &&
						device.Driver.DriverContext != null)
					{
						device.Driver.DriverContext.Release();
						device.Driver.DriverContext = null;
					}
					device.Driver = null;
					device.Enabled = false;
				}			
			}
		}
		#endregion
	}
}
