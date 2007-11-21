//
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.ADC {

	public unsafe class Architecture {
		/// <summary>
		/// Checks for compatibility with the current system, using 
		/// the most well-supported method possible. 
		/// </summary>
		[AOTAttr.ADCStub]
		public static bool CheckCompatibility()
		{
			return false;
		}
		
		/// <summary>
		/// Does architecture-specific initialization. 
		/// </summary>
		[AOTAttr.ADCStub]
		public static void Setup ()
		{
		}
		
		/// <summary>
		/// Gets a string representing the CPU type which can be 
		/// displayed to the user.
		/// </summary>
		[AOTAttr.ADCStub]
		public static byte *GetCPU()
		{
			return null;
		}
		
		[AOTAttr.ADCStub]
		public static byte *GetAuthor()
		{
			return null;
		}
		
		[AOTAttr.ADCStub]
		public static byte *GetLayerName()
		{
			return null;
		}
		
		[AOTAttr.ADCStub]
		public static int GetProcessorCount ()
		{
			return 0;
		}
		
		[AOTAttr.ADCStub]
		public static Processor *GetProcessors ()
		{
			return null;
		}
		
		[AOTAttr.ADCStub]
		public static EventRegisterStatus RegisterTimerEvent (uint func)
		{
			return EventRegisterStatus.NotSupported;
		}
		

		/// <summary>
		///		Disable interrupts
		/// </summary>
		[AOTAttr.ADCStub] public static void		DisableInterrupts	() { }

		/// <summary>
		///		Enable interrupts
		/// </summary>
		[AOTAttr.ADCStub] public static void		EnableInterrupts	() { }
	}
}
