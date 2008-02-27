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
	/// Defines methods to manipulate generic collections.
	/// </summary>
	/// <typeparam name="T">
	/// The type of the elements in the collection.
	/// </typeparam>
	[TargetNamespace ("System.Collections.Generic")]
	public interface ICollection<T> : IEnumerable<T>, IEnumerable
	{
		/// <summary>
		/// Gets the number of elements contained in the System.Collections.Generic.ICollection&lt;T&gt;.
		/// </summary>
		/// <returns>
		/// The number of elements contained in the System.Collections.Generic.ICollection&lt;T&gt;.
		/// </returns>
		int Count { get; }
		
		/// <summary>
		/// Gets a value indicating whether the System.Collections.Generic.ICollection&lt;T&gt;
		/// is read-only.
		/// </summary>
		/// <returns>
		/// true if the System.Collections.Generic.ICollection&lt;T&gt; is read-only; otherwise,
		/// false.
		/// </returns>
		bool IsReadOnly { get; }

		/// <summary>
		/// Adds an item to the System.Collections.Generic.ICollection&lt;T&gt;.
		/// </summary>
		/// <param name="item">
		/// The object to add to the System.Collections.Generic.ICollection&lt;T&gt;.
		/// </param>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.Generic.ICollection&lt;T&gt; is read-only.
		/// </exception>
		void Add(T item);
		
		/// <summary>
		/// Removes all items from the System.Collections.Generic.ICollection&lt;T&gt;.
		/// </summary>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.Generic.ICollection&lt;T&gt; is read-only.
		/// </exception>
		void Clear();
		
		/// <summary>
		/// Determines whether the System.Collections.Generic.ICollection&lt;T&gt; contains
		/// a specific value.
		/// </summary>
		/// <param name="item">
		/// The object to locate in the System.Collections.Generic.ICollection&lt;T&gt;.
		/// </param>
		///
		/// <returns>
		/// true if item is found in the System.Collections.Generic.ICollection&lt;T&gt;; otherwise,
		/// false.
		/// </returns>
		bool Contains(T item);
		
		/// <summary>
		/// Copies the elements of the System.Collections.Generic.ICollection&lt;T&gt; to an
		/// System.Array, starting at a particular System.Array index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional System.Array that is the destination of the elements
		/// copied from System.Collections.Generic.ICollection&lt;T&gt;. The System.Array must
		/// have zero-based indexing.
		/// </param>
		/// <param name="arrayIndex">
		/// The zero-based index in array at which copying begins.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// array is null.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// arrayIndex is less than 0.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// array is multidimensional.-or-arrayIndex is equal to or greater than the
		/// length of array.-or-The number of elements in the source System.Collections.Generic.ICollection&lt;T&gt;
		/// is greater than the available space from arrayIndex to the end of the destination
		/// array.-or-Type T cannot be cast automatically to the type of the destination
		/// array.
		/// </exception>
		void CopyTo(T[] array, int arrayIndex);
		
		/// <summary>
		/// Removes the first occurrence of a specific object from the System.Collections.Generic.ICollection&lt;T&gt;.
		/// </summary>
		/// <param name="item">
		/// The object to remove from the System.Collections.Generic.ICollection&lt;T&gt;.
		/// </param>
		/// <returns>
		/// true if item was successfully removed from the System.Collections.Generic.ICollection&lt;T&gt;;
		/// otherwise, false. This method also returns false if item is not found in
		/// the original System.Collections.Generic.ICollection&lt;T&gt;.
		/// </returns>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.Generic.ICollection&lt;T&gt; is read-only.
		/// </exception>
		bool Remove(T item);
	}
}
