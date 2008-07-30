//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using SharpOS.Kernel.DeviceSystem;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC {

	// TODO: ..rename to enviroment?
	public unsafe class Architecture {
		
		#region Setup
		/// <summary>
		/// Does architecture-specific initialization. 
		/// </summary>
		[AOTAttr.ADCStub]
		public static void Setup ()
		{
		}
		#endregion
		
		// TODO: How usefull is this?
		#region CheckCompatibility
		/// <summary>
		/// Checks for compatibility with the current system, using 
		/// the most well-supported method possible. 
		/// </summary>
		[AOTAttr.ADCStub]
		public static bool CheckCompatibility ()
		{
			return false;
		}
		#endregion
		
		// TODO: should be put in attributes / seperate class?
		#region Implementation Information
		/// <summary>
		/// Gets a string representing the CPU type which can be 
		/// displayed to the user.
		/// </summary>
		[AOTAttr.ADCStub]
		public static string GetCPU()
		{
			return null;
		}

		[AOTAttr.ADCStub]
		public static byte* GetAuthor ()
		{
			return null;
		}

		[AOTAttr.ADCStub]
		public static byte* GetLayerName ()
		{
			return null;
		}
		#endregion
		
		#region Processors
		[AOTAttr.ADCStub]
		public static int GetProcessorCount ()
		{
			return 0;
		}

		[AOTAttr.ADCStub]
		public static IProcessor[] GetProcessors ()
		{
			return null;
		}
		#endregion				
	}
}
