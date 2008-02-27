//
// (C) 2006-2007 The SharpOS Project Team (http:///www.sharpos.org)
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
using SharpOS.Kernel;

namespace InternalSystem.Collections {
	/// <summary>
	/// Implements the System.Collections.IList interface using an array whose size
	/// is dynamically increased as required.
	/// </summary>
	[TargetNamespace ("System.Collections")]
	public class ArrayList :/*
		System.Collections.IList, 
		System.Collections.ICollection, 
		System.Collections.IEnumerable, 
		*/
		System.ICloneable
	{
		/// <summary>
		/// Initializes a new instance of the System.Collections.ArrayList class that
		/// is empty and has the default initial capacity.
		/// </summary>
		public ArrayList()
		{
			SetCapacity(0);
		}
		
		/// <summary>
		/// Initializes a new instance of the System.Collections.ArrayList class that
		/// contains elements copied from the specified collection and that has the same
		/// initial capacity as the number of elements copied.
		/// </summary>
		/// <param name="collection">
		/// The System.Collections.ICollection whose elements are copied to the new list.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// collection is null.
		/// </exception>
		public ArrayList(ICollection collection)
		{
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Initializes a new instance of the System.Collections.ArrayList class that
		/// is empty and has the specified initial capacity.
		/// </summary>
		/// <param name="capacity">
		/// The number of elements that the new list can initially store.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// capacity is less than zero.
		/// </exception>
		public ArrayList(int capacity)
		{
			if (capacity < 0)
				throw new System.ArgumentOutOfRangeException("capacity");
			SetCapacity(capacity);
		}

		public bool IsReadOnly { get { return false; } }

		private object[] internalArray = null;
		private int internalCount = 0;
		private int internalCapacity = 0;
		private unsafe void SetCapacity(int value)
		{
			if (internalCapacity < internalCount)
			{
				throw new System.ArgumentOutOfRangeException("value",
					"internalCapacity is set to a value that is than internalCount.");
			}
					
			
			if (value != 0)
			{				
				value = (int)MemoryUtil.NextPowerOf2((uint)value);
				if (value < 4)
					value = 4;				
			}
			
			internalCapacity = value;
			
			if (internalArray != null)
			{
				object[] oldArray = internalArray;
				object[] newArray = new object[internalCapacity];

				for (int i = 0; i < internalCount; i++)
					newArray[i] = oldArray[i];
				internalArray = newArray;
				
				// TODO: remove this and mark as deprecated when GC is working
				MemoryManager.Free (Stubs.GetPointerFromObject (oldArray));
			} else
			{
				if (internalCapacity > 0)
					internalArray = new object[internalCapacity];
				else
					internalArray = null;
				internalCount = 0;
			}			
		}

		private void EnsureAdditionalCapacity(int count)
		{			
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count",
					"count is less than 0.");
			
			int diff = internalCapacity - internalCount;
			if (diff >= count)
				return;
		
			SetCapacity(internalCapacity + count);
		}

		/// <summary>
		/// Gets or sets the number of elements that the System.Collections.ArrayList
		/// can contain.
		/// </summary>
		/// <returns>
		/// The number of elements that the System.Collections.ArrayList can contain.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// System.Collections.ArrayList.Capacity is set to a value that is less than
		/// System.Collections.ArrayList.Count.
		/// </exception>
		/// <exception cref="System.OutOfMemoryException">
		/// There is not enough memory available on the system.
		/// </exception>
		public virtual int Capacity 
		{ 
			get
			{
				return internalCapacity;
			}
			set
			{
				if (internalCapacity < Count)
					throw new System.ArgumentOutOfRangeException("Capacity",
						"ArrayList.Capacity is set to a value that is than ArrayList.Count.");
				SetCapacity(value);
			}
		}
		
		/// <summary>
		/// Gets the number of elements actually contained in the System.Collections.ArrayList.
		/// </summary>
		/// <returns>
		/// The number of elements actually contained in the System.Collections.ArrayList.
		/// </returns>
		public virtual int Count 
		{ 
			get
			{
				return internalCount;
			} 
		}
		
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
		/// index is less than zero.-or- index is equal to or greater than System.Collections.ArrayList.Count.
		/// </exception>
		public virtual object this[int index] 
		{ 
			get
			{
				if (index < 0 || index >= Count)
					throw new System.ArgumentOutOfRangeException("index",
						"index and count do not specify a valid section in the ArrayList.");
				return internalArray[index];
			} 
			set
			{
				if (index < 0 || index >= Count)
					throw new System.ArgumentOutOfRangeException("index",
						"index and count do not specify a valid section in the ArrayList.");
				internalArray[index] = value;
			}
		}

		/// <summary>
		/// Adds an object to the end of the System.Collections.ArrayList.
		/// </summary>
		/// <param name="item">
		/// The System.Object to be added to the end of the System.Collections.ArrayList.
		/// The item can be null.
		/// </param>
		/// <returns>
		/// The System.Collections.ArrayList index at which the item has been added.
		/// </returns>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.-or- The System.Collections.ArrayList
		/// has a fixed size.
		/// </exception>
		public virtual int Add(object item)
		{
			EnsureAdditionalCapacity(1);
			internalArray[internalCount] = item;
			internalCount++;
			return internalCount - 1;
		}
		
		/// <summary>
		/// Adds the elements of an System.Collections.ICollection to the end of the
		/// System.Collections.ArrayList.
		/// </summary>
		/// <param name="collection">
		/// The System.Collections.ICollection whose elements should be added to the
		/// end of the System.Collections.ArrayList. The collection itself cannot be
		/// null, but it can contain elements that are null.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// collection is null.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.-or- The System.Collections.ArrayList
		/// has a fixed size.
		/// </exception>
		public virtual void AddRange(ICollection collection)
		{
			if (collection == null)
				throw new System.ArgumentNullException("collection");
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Removes all elements from the System.Collections.ArrayList.
		/// </summary>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.-or- The System.Collections.ArrayList
		/// has a fixed size.
		/// </exception>
		public virtual void Clear()
		{
			for (int i = 0; i < internalCount; i++)
				internalArray[i] = null;
			internalCount = 0;
		}
		
		/// <summary>
		/// Determines whether an element is in the System.Collections.ArrayList.
		/// </summary>
		/// <param name="item">
		/// The System.Object to locate in the System.Collections.ArrayList. The item
		/// can be null.
		/// </param>
		/// <returns>
		/// true if item is found in the System.Collections.ArrayList; otherwise, false.
		/// </returns>
		public virtual bool Contains(object item)
		{
			for (int i = 0; i < internalCount; i++)
			{
				if (item.Equals(internalArray[i]))
					return true;
			}
			return false;
		}
		
		/// <summary>
		/// Copies the entire System.Collections.ArrayList to a compatible one-dimensional
		/// System.Array, starting at the beginning of the target array.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional System.Array that is the destination of the elements
		/// copied from System.Collections.ArrayList. The System.Array must have zero-based
		/// indexing.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// array is null.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// array is multidimensional.-or- The number of elements in the source System.Collections.ArrayList
		/// is greater than the number of elements that the destination array can contain.
		/// </exception>
		/// <exception cref="System.InvalidCastException">
		/// The type of the source System.Collections.ArrayList cannot be cast automatically
		/// to the type of the destination array.
		/// </exception>
		public virtual void CopyTo(Array array)
		{
			if (array == null)
				throw new System.ArgumentNullException("array");
			if (Count > array.Length)
				throw new System.ArgumentException("The number of elements in the source ArrayList is greater than the number of elements that the destination array can contain.");
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Copies the entire System.Collections.ArrayList to a compatible one-dimensional
		/// System.Array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional System.Array that is the destination of the elements
		/// copied from System.Collections.ArrayList. The System.Array must have zero-based
		/// indexing.
		/// </param>
		/// <param name="arrayIndex">
		/// The zero-based index in array at which copying begins.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// array is null.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// arrayIndex is less than zero.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// array is multidimensional.-or- arrayIndex is equal to or greater than the
		/// length of array.-or- The number of elements in the source System.Collections.ArrayList
		/// is greater than the available space from arrayIndex to the end of the destination
		/// array.
		/// </exception>
		/// <exception cref="System.InvalidCastException">
		/// The type of the source System.Collections.ArrayList cannot be cast automatically
		/// to the type of the destination array.
		/// </exception>
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
				throw new System.ArgumentNullException("array");
			if (arrayIndex < 0)
				throw new System.ArgumentOutOfRangeException("arrayIndex");
			if (arrayIndex >= array.Length)
				throw new System.ArgumentException("arrayIndex is equal to or greater than the length of array.");
			if (Count > array.Length - arrayIndex)
				throw new System.ArgumentException("The number of elements in the source ArrayList is greater than the available space from arrayIndex to the end of the destination array.");
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Copies a range of elements from the System.Collections.ArrayList to a compatible
		/// one-dimensional System.Array, starting at the specified index of the target
		/// array.
		/// </summary>
		/// <param name="index">
		/// The zero-based index in the source System.Collections.ArrayList at which
		/// copying begins.
		/// </param>
		/// <param name="array">
		/// The one-dimensional System.Array that is the destination of the elements
		/// copied from System.Collections.ArrayList. The System.Array must have zero-based
		/// indexing.
		/// </param>
		/// <param name="arrayIndex">
		/// The zero-based index in array at which copying begins.
		/// </param>
		/// <param name="count">
		/// The number of elements to copy.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// array is null.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than zero.-or- arrayIndex is less than zero.-or- count is less
		/// than zero.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// array is multidimensional.-or- index is equal to or greater than the System.Collections.ArrayList.Count
		/// of the source System.Collections.ArrayList.-or- arrayIndex is equal to or
		/// greater than the length of array.-or- The number of elements from index to
		/// the end of the source System.Collections.ArrayList is greater than the available
		/// space from arrayIndex to the end of the destination array.
		/// </exception>
		/// <exception cref="System.InvalidCastException">
		/// The type of the source System.Collections.ArrayList cannot be cast automatically
		/// to the type of the destination array.
		/// </exception>
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (array == null)
				throw new System.ArgumentNullException("array");
			
			if (index < 0)
				throw new System.ArgumentOutOfRangeException("index", "index is less than 0.");
			if (arrayIndex < 0)
				throw new System.ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", "count is less than 0.");
			
			if (index >= Count)
				throw new System.ArgumentException("index is equal to or greater than the Count of the source ArrayList.");
			if (arrayIndex >= array.Length)
				throw new System.ArgumentException("arrayIndex is equal to or greater than the length of array.");
			if (index + count > Count)
				throw new System.ArgumentException("index + count > Count.");
			if (arrayIndex + count > array.Length)
				throw new System.ArgumentException("arrayIndex + count > array.Length");

			//WTF?
			if (Count - index > array.Length - arrayIndex)
				throw new System.ArgumentException("The number of elements from index to the end of the source ArrayList is greater than the available space from arrayIndex to the end of the destination array.");
			
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Returns an enumerator for the entire System.Collections.ArrayList.
		/// </summary>
		/// <returns>
		/// An System.Collections.IEnumerator for the entire System.Collections.ArrayList.
		/// </returns>
		public virtual IEnumerator GetEnumerator()
		{
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Returns an enumerator for a range of elements in the System.Collections.ArrayList.
		/// </summary>
		/// <param name="index">
		/// The zero-based starting index of the System.Collections.ArrayList section
		/// that the enumerator should refer to.
		/// </param>
		/// <param name="count">
		/// The number of elements in the System.Collections.ArrayList section that the
		/// enumerator should refer to.
		/// </param>
		/// <returns>
		/// An System.Collections.IEnumerator for the specified range of elements in
		/// the System.Collections.ArrayList.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than zero.-or- count is less than zero.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// index and count do not specify a valid range in the System.Collections.ArrayList.
		/// </exception>
		public virtual IEnumerator GetEnumerator(int index, int count)
		{
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Returns an System.Collections.ArrayList which represents a subset of the
		/// elements in the source System.Collections.ArrayList.
		/// </summary>
		/// <param name="index">
		/// The zero-based System.Collections.ArrayList index at which the range starts.
		/// </param>
		/// <param name="count">
		/// The number of elements in the range.
		/// </param>
		/// <returns>
		/// An System.Collections.ArrayList which represents a subset of the elements
		/// in the source System.Collections.ArrayList.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than zero.-or- count is less than zero.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// index and count do not denote a valid range of elements in the System.Collections.ArrayList.
		/// </exception>
		public virtual ArrayList GetRange(int index, int count)
		{
			if (index < 0)
				throw new System.ArgumentOutOfRangeException("index", "index is less than 0.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", "count is less than 0.");
			if (index + count >= Count)
				throw new System.ArgumentException("index and count do not denote a valid range of elements in the ArrayList.");

			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Searches for the specified System.Object and returns the zero-based index
		/// of the first occurrence within the entire System.Collections.ArrayList.
		/// </summary>
		/// <param name="item">
		/// The System.Object to locate in the System.Collections.ArrayList. The item
		/// can be null.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of item within the entire System.Collections.ArrayList,
		/// if found; otherwise, -1.
		/// </returns>
		public virtual int IndexOf(object item)
		{
			for (int i = 0; i < internalCount; i++)
			{
				if (item.Equals(internalArray[i]))
					return i;
			}
			return -1;
		}
		
		/// <summary>
		/// Searches for the specified System.Object and returns the zero-based index
		/// of the first occurrence within the range of elements in the System.Collections.ArrayList
		/// that extends from the specified index to the last element.
		/// </summary>
		/// <param name="item">
		/// The System.Object to locate in the System.Collections.ArrayList. The item
		/// can be null.
		/// </param>
		/// <param name="startIndex">
		/// The zero-based starting index of the search.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of item within the range of
		/// elements in the System.Collections.ArrayList that extends from startIndex
		/// to the last element, if found; otherwise, -1.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// startIndex is outside the range of valid indexes for the System.Collections.ArrayList.
		/// </exception>
		public virtual int IndexOf(object item, int startIndex)
		{
			if (startIndex < 0 || startIndex >= Count)
				throw new System.ArgumentOutOfRangeException("index", "index is outside the range of valid indexes for the ArrayList.");
			
			for (int i = startIndex; i < internalCount; i++)
			{
				if (item.Equals(internalArray[i]))
					return i;
			}
			return -1;
		}
		
		/// <summary>
		/// Searches for the specified System.Object and returns the zero-based index
		/// of the first occurrence within the range of elements in the System.Collections.ArrayList
		/// that starts at the specified index and contains the specified number of elements.
		/// </summary>
		/// <param name="item">
		/// The System.Object to locate in the System.Collections.ArrayList. The item
		/// can be null.
		/// </param>
		/// <param name="startIndex">
		/// The zero-based starting index of the search.
		/// </param>
		/// <param name="count">
		/// The number of elements in the section to search.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of item within the range of
		/// elements in the System.Collections.ArrayList that starts at startIndex and
		/// contains count number of elements, if found; otherwise, -1.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// startIndex is outside the range of valid indexes for the System.Collections.ArrayList.-or-
		/// count is less than zero.-or- startIndex and count do not specify a valid
		/// section in the System.Collections.ArrayList.
		/// </exception>
		public virtual int IndexOf(object item, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex >= Count)
				throw new System.ArgumentOutOfRangeException("startIndex", 
					"startIndex is outside the range of valid indexes for the ArrayList.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", 
					"count is less than 0.");
			if (startIndex + count > Count)
				throw new System.ArgumentOutOfRangeException("startIndex", 
					"startIndex and count do not specify a valid section in the ArrayList.");
			
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (item.Equals(internalArray[i]))
					return i;
			}
			return -1;
		}
		
		/// <summary>
		/// Inserts an element into the System.Collections.ArrayList at the specified
		/// index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which item should be inserted.
		/// </param>
		/// <param name="item">
		/// The System.Object to insert. The item can be null.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than zero.-or- index is greater than System.Collections.ArrayList.Count.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.-or- The System.Collections.ArrayList
		/// has a fixed size.
		/// </exception>
		public virtual void Insert(int index, object item)
		{
			if (index < 0)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is less than 0.");
			
			if (index > Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is greater than Count.");

			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Inserts the elements of a collection into the System.Collections.ArrayList
		/// at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which the new elements should be inserted.
		/// </param>
		/// <param name="collection">
		/// The System.Collections.ICollection whose elements should be inserted into
		/// the System.Collections.ArrayList. The collection itself cannot be null, but
		/// it can contain elements that are null.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// collection is null.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than zero.-or- index is greater than System.Collections.ArrayList.Count.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.-or- The System.Collections.ArrayList
		/// has a fixed size.
		/// </exception>
		public virtual void InsertRange(int index, ICollection collection)
		{
			if (collection == null)
				throw new System.ArgumentNullException("collection");
			if (index < 0)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is less than 0.");			
			if (index > Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is greater than Count.");

			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Searches for the specified System.Object and returns the zero-based index
		/// of the last occurrence within the entire System.Collections.ArrayList.
		/// </summary>
		/// <param name="item">
		/// The System.Object to locate in the System.Collections.ArrayList. The item
		/// can be null.
		/// </param>
		/// <returns>
		/// The zero-based index of the last occurrence of item within the entire the
		/// System.Collections.ArrayList, if found; otherwise, -1.
		/// </returns>
		public virtual int LastIndexOf(object item)
		{
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Searches for the specified System.Object and returns the zero-based index
		/// of the last occurrence within the range of elements in the System.Collections.ArrayList
		/// that extends from the first element to the specified index.
		/// </summary>
		/// <param name="item">
		/// The System.Object to locate in the System.Collections.ArrayList. The item
		/// can be null.
		/// </param>
		/// <param name="startIndex">
		/// The zero-based starting index of the backward search.
		/// </param>
		/// <returns>
		/// The zero-based index of the last occurrence of item within the range of
		/// elements in the System.Collections.ArrayList that extends from the first
		/// element to startIndex, if found; otherwise, -1.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// startIndex is outside the range of valid indexes for the System.Collections.ArrayList.
		/// </exception>
		public virtual int LastIndexOf(object item, int startIndex)
		{
			if (startIndex < 0 || startIndex >= Count)
				throw new System.ArgumentOutOfRangeException("startIndex", 
					"startIndex is outside the range of valid indexes for the ArrayList.");

			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Searches for the specified System.Object and returns the zero-based index
		/// of the last occurrence within the range of elements in the System.Collections.ArrayList
		/// that contains the specified number of elements and ends at the specified
		/// index.
		/// </summary>
		/// <param name="item">
		/// The System.Object to locate in the System.Collections.ArrayList. The item
		/// can be null.
		/// </param>
		/// <param name="startIndex">
		/// The zero-based starting index of the backward search.
		/// </param>
		/// <param name="count">
		/// The number of elements in the section to search.
		/// </param>
		/// <returns>
		/// The zero-based index of the last occurrence of item within the range of
		/// elements in the System.Collections.ArrayList that contains count number of
		/// elements and ends at startIndex, if found; otherwise, -1.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// startIndex is outside the range of valid indexes for the System.Collections.ArrayList.-or-
		/// count is less than zero.-or- startIndex and count do not specify a valid
		/// section in the System.Collections.ArrayList.
		/// </exception>
		public virtual int LastIndexOf(object item, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex >= Count)
				throw new System.ArgumentOutOfRangeException("startIndex", 
					"startIndex is outside the range of valid indexes for the ArrayList.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", 
					"count is less than 0.");
			if (startIndex + count > Count)
				throw new System.ArgumentOutOfRangeException("startIndex", 
					"startIndex and count do not specify a valid section in the ArrayList.");

			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Removes the first occurrence of a specific object from the System.Collections.ArrayList.
		/// </summary>
		/// <param name="obj">
		/// The System.Object to remove from the System.Collections.ArrayList. The item
		/// can be null.
		/// </param>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.-or- The System.Collections.ArrayList
		/// has a fixed size.
		/// </exception>
		public virtual void Remove(object obj)
		{
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Removes the element at the specified index of the System.Collections.ArrayList.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the element to remove.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than zero.-or- index is equal to or greater than System.Collections.ArrayList.Count.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.-or- The System.Collections.ArrayList
		/// has a fixed size.
		/// </exception>
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is outside the range of valid indexes for the ArrayList.");

			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Removes a range of elements from the System.Collections.ArrayList.
		/// </summary>
		/// <param name="index">
		/// The zero-based starting index of the range of elements to remove.
		/// </param>
		/// <param name="count">
		/// The number of elements to remove.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than zero.-or- count is less than zero.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// index and count do not denote a valid range of elements in the System.Collections.ArrayList.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.-or- The System.Collections.ArrayList
		/// has a fixed size.
		/// </exception>
		public virtual void RemoveRange(int index, int count)
		{
			if (index < 0 || index >= Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is outside the range of valid indexes for the ArrayList.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", 
					"count is less than 0.");
			if (index + count > Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index and count do not specify a valid section in the ArrayList.");

			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Returns an System.Collections.ArrayList whose elements are copies of the
		/// specified item.
		/// </summary>
		/// <param name="item">
		/// The System.Object to copy multiple times in the new System.Collections.ArrayList.
		/// The item can be null.
		/// </param>
		/// <param name="count">
		/// The number of times item should be copied.
		/// </param>
		/// <returns>
		/// An System.Collections.ArrayList with count number of elements, all of which
		/// are copies of item.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// count is less than zero.
		/// </exception>
		public static ArrayList Repeat(object item, int count)
		{
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Reverses the order of the elements in the entire System.Collections.ArrayList.
		/// </summary>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.
		/// </exception>
		public virtual void Reverse()
		{
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Reverses the order of the elements in the specified range.
		/// </summary>
		/// <param name="index">
		/// The zero-based starting index of the range to reverse.
		/// </param>
		/// <param name="count">
		/// The number of elements in the range to reverse.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than zero.-or- count is less than zero.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// index and count do not denote a valid range of elements in the System.Collections.ArrayList.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.
		/// </exception>
		public virtual void Reverse(int index, int count)
		{
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Copies the elements of the System.Collections.ArrayList to a new System.Object
		/// array.
		/// </summary>
		/// <returns>
		/// An System.Object array containing copies of the elements of the System.Collections.ArrayList.
		/// </returns>
		public virtual object[] ToArray()
		{
			throw new System.NotImplementedException();
		}
		
		/// <summary>
		/// Sets the capacity to the actual number of elements in the System.Collections.ArrayList.
		/// </summary>
		/// <exception cref="System.NotSupportedException">
		/// The System.Collections.ArrayList is read-only.-or- The System.Collections.ArrayList
		/// has a fixed size.
		/// </exception>
		public virtual void TrimToSize()
		{
			SetCapacity(Count);
		}

		#region ICloneable Members

		public object Clone()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
