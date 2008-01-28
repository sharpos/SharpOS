using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.ADC.X86
{
	/// <summary>
	/// This class is used for drivers to acquire (safe) access to and reserve 
	/// access to system wide hardware resources.
	/// </summary>
	/// <todo>How to abstract away all the different types of hardware resources yet
	/// keep as much as possible platform independent.. 
	/// maybe have an interface for each different type of hardware resource?</todo>
	internal class HardwareResourceManager : IHardwareResourceManager {
		public void Setup()
		{
		}
	}
}