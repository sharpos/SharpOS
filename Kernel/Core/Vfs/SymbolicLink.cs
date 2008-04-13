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
using System.IO;

namespace SharpOS.Kernel.Vfs {
	public sealed class SymbolicLink : NodeBase {
		#region Data members

		/// <summary>
		/// The target of the symbolic link.
		/// </summary>
		private char[] _target;

		#endregion // Data members

		#region Construction

		public SymbolicLink (char[] target)
			: base (VfsNodeType.SymbolicLink)
		{
			_target = target;
		}

		#endregion // Construction

		#region Properties

		/// <summary>
		/// Retrieves the target path of the symbolic link.
		/// </summary>
		public char[] Target
		{
			get
			{
				return _target;
			}
		}

		#endregion // Properties

		#region IVfsNode Members

		public override IVfsNode Create (char[] name, VfsNodeType type, object settings)
		{
			// Pass this request to the link target node.
			// FIXME: throw new NotImplementedException();
			return null;
		}

		public override object Open(FileAccess access, FileShare sharing)
		{
			// FIXME:
			// - Pass this request to the link target node?
			// - Do we really want this?
			// FIXME: throw new NotImplementedException();
			return null;
		}

		public override void Delete()
		{
			// FIXME: Delete the symbolic link from the filesystem, after all names have been dropped.
			// throw new NotImplementedException();
		}

		#endregion // IVfsNode Members
	}
}