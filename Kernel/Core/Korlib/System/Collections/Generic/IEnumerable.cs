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
	/// Exposes the enumerator, which supports a simple iteration over a collection
	/// of a specified type.
	/// </summary>
	/// <typeparam name="T">
	/// The type of objects to enumerate.
	/// </typeparam>
	[TargetNamespace ("System.Collections.Generic")]
	public interface IEnumerable<T> : IEnumerable
	{
		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A System.Collections.Generic.IEnumerator&lt;T^gt; that can be used to iterate through
		/// the collection.
		/// </returns>
		System.Collections.Generic.IEnumerator<T> GetEnumerator();
	}
}
