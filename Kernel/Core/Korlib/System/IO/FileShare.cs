
using SharpOS.AOT.Attributes;

namespace InternalSystem.IO {
	[ TargetNamespace("System.IO") ]
	public enum FileShare {
		None,
		Read,
		Write,
		ReadWrite,
		Delete,
		Inheritable
	}
}
