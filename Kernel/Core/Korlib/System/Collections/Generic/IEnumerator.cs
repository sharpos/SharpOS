//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Runtime.InteropServices;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace InternalSystem.Collections.Generic {
	/// <summary>
	/// Supports a simple iteration over a generic collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of objects to enumerate.
	/// </typeparam>
	[TargetNamespace ("System.Collections.Generic")]
	public interface IEnumerator<T> : System.IDisposable, System.Collections.IEnumerator
	{
		/// <summary>
		/// Gets the element in the collection at the current position of the enumerator.
		/// </summary>
		/// <returns>
		/// The element in the collection at the current position of the enumerator.
		/// </returns>
		T Current { get; }
	}
}
