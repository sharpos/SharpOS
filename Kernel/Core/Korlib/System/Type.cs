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
using SharpOS.AOT.Metadata;

namespace InternalSystem
{
    [TargetNamespace("System")]
    public abstract class Type
    {
        //protected TypeDefRow typeDefRow;

        protected Type()
        {
        }

        ///// <summary>
        /////   Returns the Attributes associated with the type.
        ///// </summary>
        //public TypeAttributes Attributes
        //{
        //    get
        //    {
        //        return typeDefRow.Flags;
        //    }
        //}

        //public Assembly Assembly
        //{
        //    get;
        //}

        //public string ToString()
        //{
        //    return String.CreateStringImpl(Runtime.GetString(Assembly, typeDef.Name));
        //}

        //public unsafe bool IsSubclassOf (InternalSystem.Type type)
        //{
        //    return Runtime.IsBaseClassOf (typeInfo, type.typeInfo); 			
        //}

        //public unsafe bool IsAssignableFrom (System.Type type)
        //{
        //    //return Runtime.IsBaseClassOf (this, type) || ;
        //    return false;
        //}

        //public static Type GetTypeFromHandle (System.RuntimeTypeHandle handle)
        //{
        //    if (handle.Value == System.IntPtr.Zero)	
        //        return null;

        //    return null; // TODO
        //}
    }
}
