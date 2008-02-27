//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;
using SharpOS.Korlib.Runtime;

namespace InternalSystem {
	[TargetNamespace ("System")]
	public class OutOfMemoryException: InternalSystem.SystemException {
		public OutOfMemoryException () :
			base ("There was not enough memory to continue the execution of the program.")
		{
		}

		public OutOfMemoryException (string message):
			base (message)
		{
		}
	}
}
