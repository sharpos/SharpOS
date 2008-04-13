//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Michael Ruck (aka grover) <sharpos@michaelruck.de>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using InternalSystem.Collections.Generic;

namespace SharpOS.Kernel.Vfs
{
	public sealed class DirectoryEntry
	{

		#region Static data members

		/// <summary>
		/// Holds the current directory of the current thread.
		/// </summary>
		[System.ThreadStatic]
		private static DirectoryEntry s_currentDirectory = null;

		#endregion // Static data members

		#region Data members

		/// <summary>
		/// References the inode that belongs to this name.
		/// </summary>
		private IVfsNode _inode;

		/// <summary>
		/// The name of this directory entry.
		/// </summary>
		private char[] _name;

		/// <summary>
		/// Ptr to the parent directory entry. 
		/// </summary>
		/// <remarks>
		/// If _parent == this, we're at the root directory entry.
		/// </remarks>
		private DirectoryEntry _parent;

		/// <summary>
		/// Sorted list of child directory entries of this name.
		/// </summary>
		private DirectoryEntry _child, _next;

		#endregion // Data members

		#region Construction

		public DirectoryEntry ()
		{
		}

		#endregion // Construction

		#region Properties

		public char[] Name
		{
			get
			{
				return _name;
			}
		}

		public IVfsNode Node
		{
			get
			{
				return _inode;
			}
		}

		public DirectoryEntry Parent
		{
			get
			{
				return _parent;
			}
		}

		#endregion // Properties

		#region Methods

		public DirectoryEntry Lookup (char[] name)
		{
			/*
				DirectoryEntry entry = null;
				int idx = 0, rmin = 0, rmax = _children.Count - 1;

				// Check for common names
				if (true == name.Equals("."))
					return this;
				if (true == name.Equals(".."))
					return _parent;

				// Iterative binary lookup into the _children list
				while (rmin <= rmax)
				{
					idx = (rmax + rmin) / 2;
					entry = _children[idx];
					if (name == entry.Name)
					{
						return entry;
					}

					if (0 < entry.Name.CompareTo(name))
					{
						rmax = idx - 1;
					}
					else
					{
						rmin = idx + 1;
					}
				}

				// FIXME: Maybe we don't have everything from the inode we're naming. Get all subentries from the inode.
			 */

			DirectoryEntry e = _child;
			while ((null != e) && (e._name != name))
				e = e._next;

			return e;
		}

		private void Setup (DirectoryEntry parent, char[] name, IVfsNode node)
		{			
			if (!Object.ReferenceEquals(this, parent))
				_parent.InsertChild(this);

			_parent = parent;
			_name = name;
			_inode = node;
		}

		/// <summary>
		/// Releases the DirectoryEntry from the parent DirectoryEntry. This is *not* a delete operation.
		/// </summary>
		/// <remarks>
		/// This function is used to remove a DirectoryEntry from the cache. Some (networked) file systems may want to use
		/// this function to remove "known" directories from the lookup cache when they loose server connection.
		/// </remarks>
		public void Release ()
		{
			// FIXME: Remove the entry from the parent and release it to the
			// entry cache in the vfs service.
			if (false == Object.ReferenceEquals(this, Parent))
				_parent.RemoveChild(this);

			_inode = null;
			_name = null;
			_parent = null;
		}

		#endregion // Methods

		#region Child list functions

		private void InsertChild (DirectoryEntry child)
		{
			/*
						DirectoryEntry entry = null;
						int idx = 0, rmin = 0, rmax = _children.Count - 1;

						// Iterative binary lookup into the _children list
						while (rmin <= rmax)
						{
							idx = (rmax + rmin) / 2;
							entry = _children[idx];
							if (true == child.Name.Equals(entry.Name))
							{
			#if VFS_NO_EXCEPTIONS
								throw new InvalidOperationException("Duplicate name.");
			#endif // #if VFS_NO_EXCEPTIONS
							}

							if (0 < entry.Name.CompareTo(child.Name))
							{
								rmax = idx - 1;
							}
							else
							{
								rmin = idx + 1;
							}
						}

						_children.Insert(rmin, child);
			 */
			// FIXME: Thread safety
			child._next = _child;
			_child = child;
		}

		private void RemoveChild (DirectoryEntry child)
		{
			// FIXME: Thread safety
			if (Object.ReferenceEquals(_child, child)) {
				_child = child._next;
			}
			else {
				DirectoryEntry e = _child;
				while (!Object.ReferenceEquals(e._next, child))
					e = e._next;
				e._next = child._next;
			}

			child._next = null;

			//			_children.Remove(child);
		}

		#endregion // Child list functions

		#region Static methods

		public static DirectoryEntry CurrentDirectoryEntry
		{
			get
			{
				if (null == s_currentDirectory) {
					// FIXME: Use the process root instead of this in order to put processes in a jail.
					s_currentDirectory = VirtualFileSystem.RootDirectoryEntry;
				}

				return s_currentDirectory;
			}

			set
			{
				if (null == value)
					throw new System.ArgumentNullException("value");

				s_currentDirectory = value;
			}
		}

		/// <summary>
		/// Allocates a new DirectoryEntry object for the given settings.
		/// </summary>
		/// <param name="parent">The parent directory entry.</param>
		/// <param name="name">The name of the entry to create.</param>
		/// <param name="node">The vfs node referenced by the directory entry.</param>
		/// <returns>The allocated directory entry.</returns>
		/// <exception cref="System.ArgumentNullException">If any one of the parameters is null.</exception>
		/// <exception cref="System.ArgumentException">If the name is zero-length.</exception>
		/// <remarks>
		/// This method is used to control the DirectoryEntry allocation and maintain a cache of them. Also used to
		/// prevent infinite name allocations.
		/// </remarks>
		public static DirectoryEntry Allocate (DirectoryEntry parent, char[] name, IVfsNode node)
		{
#if VFS_NO_EXCEPTIONS
			if (null == parent)
				throw new ArgumentNullException(@"parent");
			if (null == name)
				throw new ArgumentNullException(@"name");
			if (null == node)
				throw new ArgumentNullException(@"node");
			if (0 == name.Length)
				throw new ArgumentException(@"Invalid directory entry name.", @"name");
			// FIXME: Add precondition check for invalid characters
			// FIXME: Localize exception messages
#endif // #if VFS_NO_EXCEPTIONS

			DirectoryEntry d = new DirectoryEntry();
			d.Setup(parent, name, node);
			return d;
		}

		/// <summary>
		/// Allocates a vfs root directory entry.
		/// </summary>
		/// <param name="node">The vfs node, which corresponds to the root directory.</param>
		/// <returns>The created directory entry.</returns>
		/// <exception cref="System.ArgumentNullException">The specified node is invalid.</exception>
		/// <remarks>
		/// This method creates a directory entry, which has some special properties. The first one is, that
		/// its parent is itself. This provides for the ability to cd .. on the root to stay on the root.
		/// <para/>
		/// The next ability is to create specialized root directories to isolate processes from the remainder
		/// of the filesystem. Setting a root directory created using this method effectively limits the process
		/// to access inside of the newly created namespace.
		/// </remarks>
		public static DirectoryEntry AllocateRoot (IVfsNode node)
		{
#if VFS_NO_EXCEPTIONS
			if (null == node)
				throw new ArgumentNullException(@"node");
#endif // #if VFS_NO_EXCEPTIONS

			DirectoryEntry result = new DirectoryEntry();
			//result.Setup(result, String.Empty, node);	
			result.Setup (result, null, node);
			return result;
		}

		#endregion // Static methods
	}
}
