using SharpOS.AOT.X86;
using SharpOS.ADC;
using SharpOS.Memory;

namespace SharpOS {
	public enum KernelError: uint {
		Unknown = 0,
		
		Success,
		MultibootError,

		/// <summary>
		/// Scheduler Queue is empty and this was not expected
		/// </summary>
		SchedulerQueueEmpty
	}
}