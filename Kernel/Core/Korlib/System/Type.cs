//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;
using SharpOS.Korlib.Runtime;

namespace InternalSystem
{
	[TargetNamespace("System")]
	public abstract class Type
	{

		protected Type ()
		{
		}

		public abstract Type BaseType
		{
			get;
		}

		public unsafe bool IsSubclassOf (System.Type type)
		{
			//return Runtime.IsBaseClassOf (this, type); 
			return false;
		}

		public unsafe bool IsAssignableFrom (System.Type type)
		{
			//return Runtime.IsBaseClassOf (this, type) || ;
			return false;
		}

		//public static Type GetTypeFromHandle (System.RuntimeTypeHandle handle)
		//{
		//    if (handle.Value == System.IntPtr.Zero)	
		//        return null;

		//    return null; // TODO
		//}

	}
}
