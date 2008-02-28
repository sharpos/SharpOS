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
	/// Represents a collection of objects that can be individually accessed by index.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the list.
	/// </typeparam>
	[TargetNamespace ("System.Collections.Generic")]
	public interface IList<T> : 
		//System.Collections.Generic.ICollection<T>, 
		//System.Collections.Generic.IEnumerable<T>, 
		System.Collections.IEnumerable
	{
		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the element to get or set.
		/// </param>
		/// <returns>
		/// The element at the specified index.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is not a valid index in the System.Collections.Generic.IList&lt;T^gt;.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The property is set and the System.Collections.Generic.IList&lt;T^gt; is read-only.
		/// </exception>
		T this[int index] { get; set; }

		/// <summary>
		/// Determines the index of a specific item in the System.Collections.Generic.IList&lt;T^gt;.
		/// </summary>
		/// <param name="item">
		/// The object to locate in the System.Collections.Generic.IList&lt;T^gt;.
		/// </param>
		/// <returns>
		/// The index of item if found in the list; otherwise, -1.
		/// </returns>
		int IndexOf(T item);
		//
		/// <summary>
		/// Inserts an item to the System.Collections.Generic.IList&lt;T^gt; at the specified
		/// index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which item should be inserted.
		/// </param>
		/// <param name="item">
		/// The object to insert into the System.Collections.Generic.IList&lt;T^gt;.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is not a valid index in the System.Collections.Generic.IList&lt;T^gt;.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.Generic.IList&lt;T^gt; is read-only.
		/// </exception>
		void Insert(int index, T item);
		//
		/// <summary>
		/// Removes the System.Collections.Generic.IList&lt;T^gt; item at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the item to remove.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is not a valid index in the System.Collections.Generic.IList&lt;T^gt;.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.Generic.IList&lt;T^gt; is read-only.
		/// </exception>
		void RemoveAt(int index);
	}
}
