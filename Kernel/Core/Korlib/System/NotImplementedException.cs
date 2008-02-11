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
	public class NotImplementedException : InternalSystem.SystemException {
		public NotImplementedException ():
			this ("The requested feature is not implemented.")
		{
		}

		public NotImplementedException (string message):
			base (message)
		{
		}
	}
}
