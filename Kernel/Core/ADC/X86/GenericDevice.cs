using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.ADC.X86
{
	public class GenericDevice : IDevice {

		#region Constructors
		public GenericDevice(string		_signature,
							 string		_vendor,
							 string		_name,
							 string		_category)
		{
			signature	= _signature;
			vendor		= _vendor;
			name		= _name;
			category	= _category;
		}

		public GenericDevice(string		_signature,
							 string		_vendor,
							 string		_name)
		{
			signature	= _signature;
			vendor		= _vendor;
			name		= _name;
			category	= "";
		}

		public GenericDevice(string		_signature,
							 string		_vendor,
							 string		_name,
							 IDriver	_driver)
		{
			signature	= _signature;
			vendor		= _vendor;
			name		= _name;
			category	= "";
			driver		= _driver;
		}

		public GenericDevice(string		_signature,
							 string		_vendor,
							 string		_name,
							 string		_category,
							 IDriver	_driver)
		{
			signature	= _signature;
			vendor		= _vendor;
			name		= _name;
			category	= _category;
			driver		= _driver;
		}
		#endregion

		#region Driver
		private IDriver driver;
		public override IDriver Driver
		{
			get { return driver; }
			set 
			{ 
				driver = value;
				enabled = (driver != null);
			}
		}
		#endregion

		#region Signature
		private string signature;
		public override string Signature { get { return signature; } }
		#endregion
		
		#region Vendor
		private string vendor;
		public override string Vendor { get { return vendor; } }
		#endregion
		
		#region Signature
		private string name;
		public override string Name { get { return name; } }
		#endregion
		
		#region Signature
		private string category;
		public override string Category { get { return category; } }
		#endregion

		#region Enabled
		private bool			enabled = false;
		public override bool	Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = true;
			}
		}
		#endregion
	}
}
