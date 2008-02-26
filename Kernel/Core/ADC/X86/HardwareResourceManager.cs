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
using SharpOS.Kernel.DriverSystem;

namespace SharpOS.Kernel.ADC.X86 {

	/// <summary>
	/// This class is used for drivers to acquire (safe) access to and reserve 
	/// access to system wide hardware resources.
	/// </summary>
	/// <todo>
	/// Eventually we want to pass an attribute annotated interface to the
	/// manager and it'll generate a class which implements that interface and
	/// returns it.. untill we can implement something like that we'll 
	/// need to work around it
	/// </todo>
	/// <todo>
	/// We need to have some sort of context so that when a driver is cleaned up, 
	/// we can find back the resources it requested.
	/// </todo>	
	/// <TODO> add support for dma </TODO>
	/// <TODO> add support for interrupts </TODO>
	/// <TODO> keep track of which resources are used and which ones are available </TODO>
	internal class HardwareResourceManager : IHardwareResourceManager {
		
		public void Setup()
		{
		}

		private sealed class DriverContext : IDriverContext
		{
			public DriverContext(HardwareResourceManager _manager, IDevice _device)
			{
				if (_manager == null)
					throw new ArgumentNullException("manager");
				manager = _manager;
				device = _device;
			}

			/*
			~DriverContext()
			{
				Release();
			}*/

			private HardwareResourceManager manager;

			private IDevice					device;
			public IDevice					Device { get { return device; } }

			private DriverFlags				flags;
			public DriverFlags				Flags { get { return flags; } }
			
			private bool					isReleased = false;
			public bool						IsReleased { get { return isReleased; } }
			
			public void Release()
			{
				if (isReleased)
					return;
				try { manager.Release(this); }
				finally { isReleased = true; }
			}
			
			// TODO: this should eventually be done trough attributes
			public void Initialize(DriverFlags _flags)
			{
				flags = _flags;
			}

			public MemoryBlock CreateMemoryBuffer(uint address, uint length)
			{
				return manager.CreateMemoryBuffer(this, address, length);
			}

			public IOPortStream CreateIOPortStream(ushort port)
			{
				return manager.CreateIOPortStream(this, port);
			}
		}


		
		public IDriverContext		CreateDriverContext(IDevice device)
		{
			return new DriverContext(this, device);
		}

		internal void				Release(IDriverContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (context.IsReleased)
				throw new InvalidOperationException("Context has already been released.");
		}

		internal MemoryBlock		CreateMemoryBuffer(IDriverContext context, uint address, uint length)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (context.IsReleased)
				throw new InvalidOperationException("Context was used after it was released.");

			return new MemoryBlock(address, length);
		}

		internal IOPortStream		CreateIOPortStream(IDriverContext context, ushort port)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (context.IsReleased)
				throw new InvalidOperationException("Context was used after it was released.");

			switch ((DriverFlags)(context.Flags & DriverFlags.IOStreamMask))
			{
				default:
				case DriverFlags.IOStream8Bit:	return new IOPortStream8bit((IO.Port)port);
				case DriverFlags.IOStream16Bit:	throw new NotImplementedException();
				case DriverFlags.IOStream32Bit:	throw new NotImplementedException();
				case DriverFlags.IOStream64Bit:	throw new NotImplementedException();
			}
		}
	}
}