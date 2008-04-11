
using SharpOS.AOT.Attributes;

namespace InternalSystem.IO {
	[TargetNamespace("System.IO")]
	public enum FileAccess {
		Read,
		Write,
		ReadWrite
	}
}
