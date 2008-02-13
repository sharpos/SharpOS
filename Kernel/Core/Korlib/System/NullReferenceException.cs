//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;
using SharpOS.Korlib.Runtime;
using SharpOS.Kernel.ADC;

namespace InternalSystem {
	[TargetNamespace ("System")]
	public class NullReferenceException: InternalSystem.SystemException {
		public NullReferenceException ():
			this ("A null value was found where an object instance was required.")
		{
		}

		public NullReferenceException (string message):
			base (message)
		{
		}
	}
}
