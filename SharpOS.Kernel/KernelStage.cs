using SharpOS.AOT.X86;
using SharpOS.ADC;
using SharpOS.Memory;
using ADC = SharpOS.ADC;

namespace SharpOS {
	public enum KernelStage: uint {
		Init = 0,
		RuntimeInit,
		UserInit,
		Active,
		SingleUser,
		Stopping,
		Stop,
		Halt,
		
		Unknown = 0xFFFFFFFF
	}
}