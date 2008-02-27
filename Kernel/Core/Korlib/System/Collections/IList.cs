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
	/// Represents a non-generic collection of objects that can be individually accessed
	/// by index.
	/// </summary>
	[TargetNamespace ("System.Collections")]
	public interface IList : ICollection, IEnumerable
	{
		/// <summary>
		/// Gets a value indicating whether the System.Collections.IList has a fixed
		/// size.
		/// </summary>
		/// <returns>
		/// true if the System.Collections.IList has a fixed size; otherwise, false.
		/// </returns>
		bool IsFixedSize { get; }
		
		/// <summary>
		/// Gets a value indicating whether the System.Collections.IList is read-only.
		/// </summary>
		/// <returns>
		/// true if the System.Collections.IList is read-only; otherwise, false.
		/// </returns>
		bool IsReadOnly { get; }

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
		/// index is not a valid index in the System.Collections.IList.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The property is set and the System.Collections.IList is read-only.
		/// </exception>
		object this[int index] { get; set; }

		/// <summary>
		/// Adds an item to the System.Collections.IList.
		/// </summary>
		/// <param name="value">
		/// The System.Object to add to the System.Collections.IList.
		/// </param>
		/// <returns>
		/// The position into which the new element was inserted.
		/// </returns>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.IList is read-only.-or- The System.Collections.IList
		/// has a fixed size.
		/// </exception>
		int Add(object value);
		
		/// <summary>
		/// Removes all items from the System.Collections.IList.
		/// </summary>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.IList is read-only.
		/// </exception>
		void Clear();
		
		/// <summary>
		/// Determines whether the System.Collections.IList contains a specific value.
		/// </summary>
		/// <param name="value">
		/// The System.Object to locate in the System.Collections.IList.
		/// </param>
		/// <returns>
		/// true if the System.Object is found in the System.Collections.IList; otherwise,
		/// false.
		/// </returns>
		bool Contains(object value);
		
		/// <summary>
		/// Determines the index of a specific item in the System.Collections.IList.
		/// </summary>
		/// <param name="value">
		/// The System.Object to locate in the System.Collections.IList.
		/// </param>
		/// <returns>
		/// The index of value if found in the list; otherwise, -1.
		/// </returns>
		int IndexOf(object value);
		
		/// <summary>
		/// Inserts an item to the System.Collections.IList at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which value should be inserted.
		/// </param>
		/// <param name="value">
		/// The System.Object to insert into the System.Collections.IList.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is not a valid index in the System.Collections.IList.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.IList is read-only.-or- The System.Collections.IList
		/// has a fixed size.
		/// </exception>
		/// <exception cref="System.NullReferenceException">
		/// value is null reference in the System.Collections.IList.
		/// </exception>
		void Insert(int index, object value);
		
		/// <summary>
		/// Removes the first occurrence of a specific object from the System.Collections.IList.
		/// </summary>
		/// <param name="value">
		/// The System.Object to remove from the System.Collections.IList.
		/// </param>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.IList is read-only.-or- The System.Collections.IList
		/// has a fixed size.
		/// </exception>
		void Remove(object value);
		
		/// <summary>
		/// Removes the System.Collections.IList item at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the item to remove.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is not a valid index in the System.Collections.IList.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.IList is read-only.-or- The System.Collections.IList
		/// has a fixed size.
		/// </exception>
		void RemoveAt(int index);
	}
}
