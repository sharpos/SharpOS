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
	/// <summary>
	/// Supports cloning, which creates a new instance of a class with the same value
	/// as an existing instance.
	/// </summary>
	[TargetNamespace ("System")]
	public interface ICloneable
	{
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		object Clone();
	}
}
