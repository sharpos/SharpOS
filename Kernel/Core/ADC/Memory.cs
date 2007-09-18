using System;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.ADC
{
	public static class Memory
	{
		[AOTAttr.ADCStub]
		public static unsafe void MemSet32(uint value, uint dst, uint count)
		{
			TextMode.WriteLine("Unimplemented - Memory.MemSet32");
		}

		[AOTAttr.ADCStub]
		public static unsafe void MemCopy32(uint src, uint dst, uint count)
		{
			TextMode.WriteLine("Unimplemented - Memory.MemCopy32");
		}

		[AOTAttr.ADCStub]
		public unsafe static void Call(uint address, uint value)
		{
			TextMode.WriteLine("Unimplemented - Memory.Call");
		}
	}
}
