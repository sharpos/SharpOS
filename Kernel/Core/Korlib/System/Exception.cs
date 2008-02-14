//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;
using SharpOS.Korlib.Runtime;
using SharpOS.Kernel.ADC;

namespace InternalSystem {
	[TargetNamespace ("System")]
	public class Exception: InternalSystem.Object {
		internal StackFrame [] CallingStack = null;
		internal int IgnoreStackFramesCount = 0;
		internal int CurrentStackFrame = 0;
		private string message;

		public Exception ():
			this ("An exception was thrown.")
		{
		}

		public Exception (string message)
		{
			this.message = message;
		}

		public virtual string Message {
			get { return message; }
		}
	}
}
