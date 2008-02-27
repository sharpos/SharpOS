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

namespace InternalSystem.Collections {
	/// <summary>
	/// Supports a simple iteration over a nongeneric collection.
	/// </summary>
	[TargetNamespace ("System.Collections")]
	public interface IEnumerator
	{
		/// <summary>
		/// Gets the current element in the collection.
		/// </summary>
		/// <returns>
		/// The current element in the collection.
		/// </returns>
		/// <exception cref="System.InvalidOperationException">
		/// The enumerator is positioned before the first element of the collection or
		/// after the last element.-or- The collection was modified after the enumerator
		/// was created.
		/// </exception>
		object Current { get; }

		/// <summary>
		/// Advances the enumerator to the next element of the collection.
		/// </summary>
		/// <returns>
		/// true if the enumerator was successfully advanced to the next element; false
		/// if the enumerator has passed the end of the collection.
		/// </returns>
		/// <exception cref="System.InvalidOperationException">
		/// The collection was modified after the enumerator was created.
		/// </exception>
		bool MoveNext();

		/// <summary>
		/// Sets the enumerator to its initial position, which is before the first element
		/// in the collection.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// The collection was modified after the enumerator was created.
		/// </exception>
		void Reset();
	}
}
