using System;
using System.Text;

namespace SharpOS.Kernel.DeviceSystem
{
	public enum DeviceStatus : uint
	{
		Initializing,
		Online,
		Offline,
		NotFound,
		Error
	}

    public abstract class Device : IDevice
    {
        protected string name;
        protected Device parent;
		protected DeviceStatus deviceStatus;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Device Parent
        {
            get
            {
                return parent;
            }
        }

		public DeviceStatus Status
		{
			get
			{
				return deviceStatus;
			}
		}

        public Device()
        {
            name = string.Empty;            
            parent = null;
			deviceStatus = DeviceStatus.Error;
        }

        public Device(Device parent, string name, DeviceStatus deviceStatus)
        {
            this.parent = parent;
            this.name = name;
			this.deviceStatus = deviceStatus;           
        }


    }
}
