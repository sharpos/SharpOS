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
using SharpOS.Kernel;

namespace InternalSystem.Collections.Generic {
	/// <summary>
	/// Represents a strongly typed list of objects that can be accessed by index.
	/// Provides methods to search, sort, and manipulate lists.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the list.
	/// </typeparam>
	[TargetNamespace ("System.Collections.Generic")]
	public class List<T> /* : // ... "not supported" by AOT?
		System.Collections.Generic.IList<T>, 
		System.Collections.Generic.ICollection<T>,
		System.Collections.Generic.IEnumerable<T>,
		System.Collections.IList, 
		System.Collections.ICollection, 
		System.Collections.IEnumerable
		*/
	{
		/// <summary>
		/// Initializes a new instance of the System.Collections.Generic.List&lt;T&gt; class
		/// that is empty and has the default initial capacity.
		/// </summary>
		public List()
		{
			SetCapacity(0);
		} 
		
		/// <summary>
		/// Initializes a new instance of the System.Collections.Generic.List&lt;T&gt; class
		/// that contains elements copied from the specified collection and has sufficient
		/// capacity to accommodate the number of elements copied.
		/// </summary>
		/// <param name="collection">
		/// The collection whose elements are copied to the new list.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// collection is null.
		/// </exception>
		public List(IEnumerable<T> collection)
		{
			if (collection == null)
				throw new System.ArgumentNullException("collection");
			throw new System.NotImplementedException("List<T>(IEnumerable<T> collection) is not implemented");
		} 
		
		/// <summary>
		/// Initializes a new instance of the System.Collections.Generic.List&lt;T&gt; class
		/// that is empty and has the specified initial capacity.
		/// </summary>
		/// <param name="capacity">
		/// The number of elements that the new list can initially store.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// capacity is less than 0.
		/// </exception>
		public List(int capacity)
		{
			if (capacity < 0)
				throw new System.ArgumentOutOfRangeException("capacity");
			SetCapacity(capacity);
		} 

		public bool IsReadOnly { get { return false; } }

		private T[] internalArray;
		private int internalCapacity = 0;
		private unsafe void SetCapacity(int value)
		{
			if (internalCapacity < internalCount)
				throw new System.ArgumentOutOfRangeException(
					"internalCapacity is set to a value that is than internalCount.");

			if (value != 0)
			{
				value = (int)MemoryUtil.NextPowerOf2((uint)value);
				if (value < 4)
					value = 4;
			}

			internalCapacity = value;
			if (internalArray != null)
			{
				T[] oldArray = internalArray;
				T[] newArray = new T[internalCapacity];

				for (int i = 0; i < internalCount; i++)
					newArray[i] = oldArray[i];
				internalArray = newArray;
				
				// TODO: remove this and mark as deprecated when GC is working
				MemoryManager.Free (Stubs.GetPointerFromObject (oldArray));
			} else
			{
				internalArray = new T[internalCapacity];
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
		/// Gets or sets the total number of elements the internal data structure can
		/// hold without resizing.
		/// </summary>
		/// <returns>
		/// The number of elements that the System.Collections.Generic.List&lt;T&gt; can contain
		/// before resizing is required.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// System.Collections.Generic.List&lt;T&gt;.Capacity is set to a value that is less
		/// than System.Collections.Generic.List&lt;T&gt;.Count.
		/// </exception>
		/// <exception cref="System.OutOfMemoryException">
		/// There is not enough memory available on the system.
		/// </exception>
		public int Capacity 
		{ 
			get
			{
				return internalCapacity;
			} 
			set
			{
				if (internalCapacity < Count)
					throw new System.ArgumentOutOfRangeException(
						"List<T>.Capacity is set to a value that is than List<T>.Count.");
				SetCapacity(value);
			} 
		}

		private int internalCount = 0;
		/// <summary>
		/// Gets the number of elements actually contained in the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <returns>
		/// The number of elements actually contained in the System.Collections.Generic.List&lt;T&gt;.
		/// </returns>
		public int Count
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
		/// index is less than 0.-or-index is equal to or greater than System.Collections.Generic.List&lt;T&gt;.Count.
		/// </exception>
		public T this[int index] 
		{ 
			get
			{
				if (index < 0 || index >= Count)
					throw new System.ArgumentOutOfRangeException("index",
						"index and count do not specify a valid section in the List<T>.");
				return internalArray[index];
			}
			set
			{
				if (index < 0 || index >= Count)
					throw new System.ArgumentOutOfRangeException("index",
						"index and count do not specify a valid section in the List<T>.");
				internalArray[index] = value;
			}
		}

		/// <summary>
		/// Adds an object to the end of the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <param name="item">
		/// The object to be added to the end of the System.Collections.Generic.List&lt;T&gt;.
		/// The value can be null for reference types.
		/// </param>
		public void Add(T item)
		{
			EnsureAdditionalCapacity(1);
			internalArray[internalCount] = item;
			internalCount++;
		}
		
		/// <summary>
		/// Adds the elements of the specified collection to the end of the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <param name="collection">
		/// The collection whose elements should be added to the end of the System.Collections.Generic.List&lt;T&gt;.
		/// The collection itself cannot be null, but it can contain elements that are
		/// null, if type T is a reference type.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// collection is null.
		/// </exception>
		public void AddRange(IEnumerable<T> collection)
		{
			if (collection == null)
				throw new System.ArgumentNullException("collection");
			throw new System.NotImplementedException("List<T>.AddRange(IEnumerable<T> collection) is not implemented");
		}
				
		/// <summary>
		/// Removes all elements from the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		public void Clear()
		{
			for (int i = 0; i < internalCount; i++)
				internalArray[i] = default(T);
			internalCount = 0;
		}
		
		/// <summary>
		/// Determines whether an element is in the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		///
		/// <param name="item">
		/// The object to locate in the System.Collections.Generic.List&lt;T&gt;. The value
		/// can be null for reference types.
		/// </param>
		///
		/// <returns>
		/// true if item is found in the System.Collections.Generic.List&lt;T&gt;; otherwise,
		/// false.
		/// </returns>
		public bool Contains(T item)
		{
			for (int i = 0; i < internalCount; i++)
			{
				if (item.Equals(internalArray[i]))
					return true;
			}
			return false;
		}
		
		/// <summary>
		/// Copies the entire System.Collections.Generic.List&lt;T&gt; to a compatible one-dimensional
		/// array, starting at the beginning of the target array.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional System.Array that is the destination of the elements
		/// copied from System.Collections.Generic.List&lt;T&gt;. The System.Array must have
		/// zero-based indexing.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// array is null.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// The number of elements in the source System.Collections.Generic.List&lt;T&gt; is
		/// greater than the number of elements that the destination array can contain.
		/// </exception>
		public void CopyTo(T[] array)
		{
			if (array == null)
				throw new System.ArgumentNullException("array");
			if (Count > array.Length)
				throw new System.ArgumentException("The number of elements in the source List<T> is greater than the number of elements that the destination array can contain.");
			throw new System.NotImplementedException("List<T>.CopyTo(T[] array) is not implemented");
		}
		
		/// <summary>
		/// Copies the entire System.Collections.Generic.List&lt;T&gt; to a compatible one-dimensional
		/// array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional System.Array that is the destination of the elements
		/// copied from System.Collections.Generic.List&lt;T&gt;. The System.Array must have
		/// zero-based indexing.
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
		/// arrayIndex is equal to or greater than the length of array.-or-The number
		/// of elements in the source System.Collections.Generic.List&lt;T&gt; is greater than
		/// the available space from arrayIndex to the end of the destination array.
		/// </exception>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
				throw new System.ArgumentNullException("array");
			if (arrayIndex < 0)
				throw new System.ArgumentOutOfRangeException("arrayIndex");
			if (arrayIndex >= array.Length)
				throw new System.ArgumentException("arrayIndex is equal to or greater than the length of array.");
			if (Count > array.Length - arrayIndex)
				throw new System.ArgumentException("The number of elements in the source List<T> is greater than the available space from arrayIndex to the end of the destination array.");
			throw new System.NotImplementedException("List<T>.CopyTo(T[] array, int arrayIndex) is not implemented");
		}
		
		/// <summary>
		/// Copies a range of elements from the System.Collections.Generic.List&lt;T&gt; to
		/// a compatible one-dimensional array, starting at the specified index of the
		/// target array.
		/// </summary>
		/// <param name="index">
		/// The zero-based index in the source System.Collections.Generic.List&lt;T&gt; at
		/// which copying begins.
		/// </param>
		/// <param name="array">
		/// The one-dimensional System.Array that is the destination of the elements
		/// copied from System.Collections.Generic.List&lt;T&gt;. The System.Array must have
		/// zero-based indexing.
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
		/// index is less than 0.-or-arrayIndex is less than 0.-or-count is less than
		/// 0.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// index is equal to or greater than the System.Collections.Generic.List&lt;T&gt;.Count
		/// of the source System.Collections.Generic.List&lt;T&gt;.-or-arrayIndex is equal
		/// to or greater than the length of array.-or-The number of elements from index
		/// to the end of the source System.Collections.Generic.List&lt;T&gt; is greater than
		/// the available space from arrayIndex to the end of the destination array.
		/// </exception>
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
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
				throw new System.ArgumentException("index is equal to or greater than the Count of the source List<T>.");
			if (arrayIndex >= array.Length)
				throw new System.ArgumentException("arrayIndex is equal to or greater than the length of array.");
			if (index + count > Count)
				throw new System.ArgumentException("index + count > Count.");
			if (arrayIndex + count > array.Length)
				throw new System.ArgumentException("arrayIndex + count > array.Length");

			//WTF?
			if (Count - index > array.Length - arrayIndex)
				throw new System.ArgumentException("The number of elements from index to the end of the source List<T> is greater than the available space from arrayIndex to the end of the destination array.");
			
			throw new System.NotImplementedException("List<T>.CopyTo(int index, T[] array, int arrayIndex, int count) is not implemented");
		}
		
		/// <summary>
		/// Returns an enumerator that iterates through the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <returns>
		/// A System.Collections.Generic.IEnumerator&lt;T&gt; for the System.Collections.Generic.List&lt;T&gt;.
		/// </returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			throw new System.NotImplementedException("List<T>.GetEnumerator() is not implemented");
		}
		
		/*
		/// <summary>
		/// Returns an enumerator that iterates through the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <returns>
		/// A System.Collections.IEnumerator for the System.Collections.Generic.List&lt;T&gt;.
		/// </returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new System.NotImplementedException("List<T>.GetEnumerator() is not implemented");
		}
		*/
		
		/// <summary>
		/// Creates a shallow copy of a range of elements in the source System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <param name="index">
		/// The zero-based System.Collections.Generic.List&lt;T&gt; index at which the range
		/// starts.
		/// </param>
		/// <param name="count">
		/// The number of elements in the range.
		/// </param>
		/// <returns>
		/// A shallow copy of a range of elements in the source System.Collections.Generic.List&lt;T&gt;.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than 0.-or-count is less than 0.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// index and count do not denote a valid range of elements in the System.Collections.Generic.List&lt;T&gt;.
		/// </exception>
		public List<T> GetRange(int index, int count)
		{
			if (index < 0)
				throw new System.ArgumentOutOfRangeException("index", "index is less than 0.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", "count is less than 0.");
			if (index + count >= Count)
				throw new System.ArgumentException("index and count do not denote a valid range of elements in the List<T>.");

			throw new System.NotImplementedException("List<T>.GetRange(int index, int count) is not implemented");
		}
		
		/// <summary>
		/// Searches for the specified object and returns the zero-based index of the
		/// first occurrence within the entire System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <param name="item">
		/// The object to locate in the System.Collections.Generic.List&lt;T&gt;. The value
		/// can be null for reference types.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of item within the entire System.Collections.Generic.List&lt;T&gt;,
		/// if found; otherwise, –1.
		/// </returns>
		public int IndexOf(T item)
		{
			for (int i = 0; i < internalCount; i++)
			{
				if (item.Equals(internalArray[i]))
					return i;
			}
			return -1;
		}
		
		/// <summary>
		/// Searches for the specified object and returns the zero-based index of the
		/// first occurrence within the range of elements in the System.Collections.Generic.List&lt;T&gt;
		/// that extends from the specified index to the last element.
		/// </summary>
		/// <param name="item">
		/// The object to locate in the System.Collections.Generic.List&lt;T&gt;. The value
		/// can be null for reference types.
		/// </param>
		/// <param name="index">
		/// The zero-based starting index of the search.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of item within the range of
		/// elements in the System.Collections.Generic.List&lt;T&gt; that extends from index
		/// to the last element, if found; otherwise, –1.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is outside the range of valid indexes for the System.Collections.Generic.List&lt;T&gt;.
		/// </exception>
		public int IndexOf(T item, int index)
		{
			if (index < 0 || index >= Count)
				throw new System.ArgumentOutOfRangeException("index", "index is outside the range of valid indexes for the List<T>.");
			
			for (int i = index; i < internalCount; i++)
			{
				if (item.Equals(internalArray[i]))
					return i;
			}
			return -1;
		}
		
		/// <summary>
		/// Searches for the specified object and returns the zero-based index of the
		/// first occurrence within the range of elements in the System.Collections.Generic.List&lt;T&gt;
		/// that starts at the specified index and contains the specified number of elements.
		/// </summary>
		/// <param name="item">
		/// The object to locate in the System.Collections.Generic.List&lt;T&gt;. The value
		/// can be null for reference types.
		/// </param>
		/// <param name="index">
		/// The zero-based starting index of the search.
		/// </param>
		/// <param name="count">
		/// The number of elements in the section to search.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of item within the range of
		/// elements in the System.Collections.Generic.List&lt;T&gt; that starts at index and
		/// contains count number of elements, if found; otherwise, –1.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is outside the range of valid indexes for the System.Collections.Generic.List&lt;T&gt;.-or-count
		/// is less than 0.-or-index and count do not specify a valid section in the
		/// System.Collections.Generic.List&lt;T&gt;.
		/// </exception>
		public int IndexOf(T item, int index, int count)
		{
			if (index < 0 || index >= Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is outside the range of valid indexes for the List<T>.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", 
					"count is less than 0.");
			if (index + count > Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index and count do not specify a valid section in the List<T>.");
			
			for (int i = index; i < index + count; i++)
			{
				if (item.Equals(internalArray[i]))
					return i;
			}
			return -1;
		}
		
		/// <summary>
		/// Inserts an element into the System.Collections.Generic.List&lt;T&gt; at the specified
		/// index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which item should be inserted.
		/// </param>
		/// <param name="item">
		/// The object to insert. The value can be null for reference types.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than 0.-or-index is greater than System.Collections.Generic.List&lt;T&gt;.Count.
		/// </exception>
		public void Insert(int index, T item)
		{
			if (index < 0)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is less than 0.");
			
			if (index > Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is greater than Count.");

			throw new System.NotImplementedException("List<T>.Insert(int index, T item) is not implemented");
		}
		
		/// <summary>
		/// Inserts the elements of a collection into the System.Collections.Generic.List&lt;T&gt;
		/// at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which the new elements should be inserted.
		/// </param>
		/// <param name="collection">
		/// The collection whose elements should be inserted into the System.Collections.Generic.List&lt;T&gt;.
		/// The collection itself cannot be null, but it can contain elements that are
		/// null, if type T is a reference type.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// collection is null.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than 0.-or-index is greater than System.Collections.Generic.List&lt;T&gt;.Count.
		/// </exception>
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
				throw new System.ArgumentNullException("collection");
			if (index < 0)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is less than 0.");			
			if (index > Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is greater than Count.");

			throw new System.NotImplementedException("List<T>.InsertRange(int index, IEnumerable<T> collection) is not implemented");
		}
		
		/// <summary>
		/// Searches for the specified object and returns the zero-based index of the
		/// last occurrence within the entire System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <param name="item">
		/// The object to locate in the System.Collections.Generic.List&lt;T&gt;. The value
		/// can be null for reference types.
		/// </param>
		/// <returns>
		/// The zero-based index of the last occurrence of item within the entire the
		/// System.Collections.Generic.List&lt;T&gt;, if found; otherwise, –1.
		/// </returns>
		public int LastIndexOf(T item)
		{
			throw new System.NotImplementedException("List<T>.LastIndexOf(T item) is not implemented");
		}
		
		/// <summary>
		/// Searches for the specified object and returns the zero-based index of the
		/// last occurrence within the range of elements in the System.Collections.Generic.List&lt;T&gt;
		/// that extends from the first element to the specified index.
		/// </summary>
		/// <param name="item">
		/// The object to locate in the System.Collections.Generic.List&lt;T&gt;. The value
		/// can be null for reference types.
		/// </param>
		/// <param name="index">
		/// The zero-based starting index of the backward search.
		/// </param>
		/// <returns>
		/// The zero-based index of the last occurrence of item within the range of elements
		/// in the System.Collections.Generic.List&lt;T&gt; that extends from the first element
		/// to index, if found; otherwise, –1.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is outside the range of valid indexes for the System.Collections.Generic.List&lt;T&gt;.
		/// </exception>
		public int LastIndexOf(T item, int index)
		{
			if (index < 0 || index >= Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is outside the range of valid indexes for the List<T>.");

			throw new System.NotImplementedException("List<T>.LastIndexOf(T item, int index) is not implemented");
		}
		
		/// <summary>
		/// Searches for the specified object and returns the zero-based index of the
		/// last occurrence within the range of elements in the System.Collections.Generic.List&lt;T&gt;
		/// that contains the specified number of elements and ends at the specified
		/// index.
		/// </summary>
		/// <param name="item">
		/// The object to locate in the System.Collections.Generic.List&lt;T&gt;. The value
		/// can be null for reference types.
		/// </param>
		/// <param name="index">
		/// The zero-based starting index of the backward search.
		/// </param>
		/// <param name="count">
		/// The number of elements in the section to search.
		/// </param>
		/// <returns>
		/// The zero-based index of the last occurrence of item within the range of elements
		/// in the System.Collections.Generic.List&lt;T&gt; that contains count number of elements
		/// and ends at index, if found; otherwise, –1.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is outside the range of valid indexes for the System.Collections.Generic.List&lt;T&gt;.-or-count
		/// is less than 0.-or-index and count do not specify a valid section in the
		/// System.Collections.Generic.List&lt;T&gt;.
		/// </exception>
		public int LastIndexOf(T item, int index, int count)
		{
			if (index < 0 || index >= Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is outside the range of valid indexes for the List<T>.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", 
					"count is less than 0.");
			if (index + count > Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index and count do not specify a valid section in the List<T>.");

			throw new System.NotImplementedException("List<T>.LastIndexOf(T item, int index, int count) is not implemented");
		}
		
		/// <summary>
		/// Removes the first occurrence of a specific object from the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <param name="item">
		/// The object to remove from the System.Collections.Generic.List&lt;T&gt;. The value
		/// can be null for reference types.
		/// </param>
		/// <returns>
		/// true if item is successfully removed; otherwise, false. This method also
		/// returns false if item was not found in the System.Collections.Generic.List&lt;T&gt;.
		/// </returns>
		public bool Remove(T item)
		{
			throw new System.NotImplementedException("List<T>.Remove(T item) is not implemented");
		}
		
		/// <summary>
		/// Removes the element at the specified index of the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the element to remove.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than 0.-or-index is equal to or greater than System.Collections.Generic.List&lt;T&gt;.Count.
		/// </exception>
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is outside the range of valid indexes for the List<T>.");

			throw new System.NotImplementedException("List<T>.RemoveAt(int index) is not implemented");
		}
		
		/// <summary>
		/// Removes a range of elements from the System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		/// <param name="index">
		/// The zero-based starting index of the range of elements to remove.
		/// </param>
		/// <param name="count">
		/// The number of elements to remove.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// index is less than 0.-or-count is less than 0.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// index and count do not denote a valid range of elements in the System.Collections.Generic.List&lt;T&gt;.
		/// </exception>
		public void RemoveRange(int index, int count)
		{
			if (index < 0 || index >= Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is outside the range of valid indexes for the List<T>.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", 
					"count is less than 0.");
			if (index + count > Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index and count do not specify a valid section in the List<T>.");

			throw new System.NotImplementedException("List<T>.RemoveRange(int index, int count) is not implemented");
		}

		/// <summary>
		/// Reverses the order of the elements in the entire System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		public void Reverse()
		{
			throw new System.NotImplementedException("List<T>.Reverse() is not implemented");
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
		/// index is less than 0.-or-count is less than 0.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// index and count do not denote a valid range of elements in the System.Collections.Generic.List&lt;T&gt;.
		/// </exception>
		public void Reverse(int index, int count)
		{
			if (index < 0 || index >= Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index is outside the range of valid indexes for the List<T>.");
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count", 
					"count is less than 0.");
			if (index + count > Count)
				throw new System.ArgumentOutOfRangeException("index", 
					"index and count do not specify a valid section in the List<T>.");

			throw new System.NotImplementedException("List<T>.Reverse(int index, int count) is not implemented");
		}

		/// <summary>
		/// Copies the elements of the System.Collections.Generic.List&lt;T&gt; to a new array.
		/// </summary>
		/// <returns>
		/// An array containing copies of the elements of the System.Collections.Generic.List&lt;T&gt;.
		/// </returns>
		public T[] ToArray()
		{
			throw new System.NotImplementedException("List<T>.ToArray() is not implemented");
		}

		/// <summary>
		/// Sets the capacity to the actual number of elements in the System.Collections.Generic.List&lt;T&gt;,
		/// if that number is less than a threshold value.
		/// </summary>
		public void TrimExcess()
		{
			SetCapacity(Count);
		}

		/*
		// ... "not supported" by AOT?
		/// <summary>
		/// Enumerates the elements of a System.Collections.Generic.List&lt;T&gt;.
		/// </summary>
		public struct Enumerator : System.Collections.Generic.IEnumerator<T>, System.IDisposable, System.Collections.IEnumerator
		{
			/// <summary>
			/// Gets the element at the current position of the enumerator.
			/// </summary>
			/// <returns>
			/// The element in the System.Collections.Generic.List&lt;T&gt; at the current position
			/// of the enumerator.
			/// </returns>
			public T Current 
			{ 
				get
				{
					throw new System.NotImplementedException("Enumerator.Current is not implemented");
				}
			}

			/// <summary>
			/// Releases all resources used by the System.Collections.Generic.List&lt;T&gt;.Enumerator.
			/// </summary>
			public void Dispose()
			{
				throw new System.NotImplementedException("Enumerator.Dispose() is not implemented");
			}
			
			/// <summary>
			/// Advances the enumerator to the next element of the System.Collections.Generic.List&lt;T&gt;.
			/// </summary>
			/// <returns>
			/// true if the enumerator was successfully advanced to the next element; false
			/// if the enumerator has passed the end of the collection.
			/// </returns>
			/// <exception cref="System.InvalidOperationException">
			/// The collection was modified after the enumerator was created.
			/// </exception>
			public bool MoveNext()
			{
				throw new System.NotImplementedException("Enumerator.MoveNext() is not implemented");
			}

			object System.Collections.IEnumerator.Current
			{
				get 
				{ 
					throw new System.NotImplementedException("IEnumerator.Current is not implemented"); 
				}
			}

			public void Reset()
			{
				throw new System.NotImplementedException("IEnumerator.Reset is not implemented"); 
			}
		}
		*/
	}
}
