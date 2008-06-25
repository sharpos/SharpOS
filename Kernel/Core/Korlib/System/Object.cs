//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;
using SharpOS.Korlib.Runtime;

namespace InternalSystem
{
	[TargetNamespace ("System")]
	public class Object
	{
        /// <summary>
        /// Used to support runtime method binding / Dynamic Dispatch
        /// </summary>
		internal VTable VTable;

		internal uint Synchronisation = 0;

		public Object ()
		{
		}

#pragma warning disable 114
		public virtual string ToString ()
		{
			return this.VTable.Type.Name;
		}

		public virtual int GetHashCode ()
		{
			return 0; // TODO
		}

		public unsafe virtual bool Equals (object o)
		{
			void* p1 = Runtime.GetPointerFromObject (this);
			void* p2 = Runtime.GetPointerFromObject (o);

			return p1 == p2;
		}
		public static unsafe bool ReferenceEquals (object o1, object o2)
		{
			void* p1 = Runtime.GetPointerFromObject (o1);
			void* p2 = Runtime.GetPointerFromObject (o2);

			return p1 == p2;
		}
#pragma warning restore 114

		public unsafe Type GetType ()
		{
			return null; // TODO
		}

		
	}
}
