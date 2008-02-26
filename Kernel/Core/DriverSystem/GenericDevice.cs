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

namespace SharpOS.Kernel.DriverSystem {

	/// <summary>
	/// Generic Device
	/// </summary>
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
		public virtual IDriver Driver
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
		public virtual string Signature { get { return signature; } }
		#endregion
		
		#region Vendor
		private string vendor;
		public virtual string Vendor { get { return vendor; } }
		#endregion
		
		#region Signature
		private string name;
		public virtual string Name { get { return name; } }
		#endregion
		
		#region Signature
		private string category;
		public virtual string Category { get { return category; } }
		#endregion

		#region Enabled
		private bool			enabled = false;
		public virtual bool		Enabled
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
