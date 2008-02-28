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
	/// Defines size, enumerators, and synchronization methods for all nongeneric
	/// collections.
	/// </summary>
	[TargetNamespace ("System.Collections")]
	public interface ICollection : 
		System.Collections.IEnumerable
	{
		/// <summary>
		/// Gets the number of elements contained in the System.Collections.ICollection.
		/// </summary>
		/// <returns>
		/// The number of elements contained in the System.Collections.ICollection.
		/// </returns>
		int Count { get; }
		
		/// <summary>
		/// Gets a value indicating whether access to the System.Collections.ICollection
		/// is synchronized (thread safe).
		/// </summary>
		/// <returns>
		/// true if access to the System.Collections.ICollection is synchronized (thread
		/// safe); otherwise, false.
		/// </returns>
		bool IsSynchronized { get; }
		
		/// <summary>
		/// Gets an object that can be used to synchronize access to the System.Collections.ICollection.
		/// </summary>
		/// <returns>
		/// An object that can be used to synchronize access to the System.Collections.ICollection.
		/// </returns>
		object SyncRoot { get; }

		/// <summary>
		/// Copies the elements of the System.Collections.ICollection to an System.Array,
		/// starting at a particular System.Array index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional System.Array that is the destination of the elements
		/// copied from System.Collections.ICollection. The System.Array must have zero-based
		/// indexing.
		/// </param>
		/// <param name="index">
		/// The zero-based index in array at which copying begins.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// array is null.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than zero.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// array is multidimensional.-or- index is equal to or greater than the length
		/// of array.-or- The number of elements in the source System.Collections.ICollection
		/// is greater than the available space from index to the end of the destination
		/// array.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// The type of the source System.Collections.ICollection cannot be cast automatically
		/// to the type of the destination array.
		/// </exception>
		void CopyTo(System.Array array, int index);
	}
}
