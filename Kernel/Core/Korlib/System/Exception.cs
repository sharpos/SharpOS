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
		/// <summary>
		/// It contains the calling stack, from the entry point to where the exception occured.
		/// </summary>
		internal StackFrame [] CallingStack = null;

		/// <summary>
		/// At the top of the calling stack there are a couple of stack frames that are irrelevant.
		/// </summary>
		internal int IgnoreStackFramesCount = 0;

		/// <summary>
		/// It keeps the last stack frame that has been processed so that when a rethrow occurs
		/// it continues with the next one.
		/// </summary>
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
