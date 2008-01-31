//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Runtime.InteropServices;
using SharpOS.AOT.Metadata;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Korlib.Runtime {

	public struct MetadataToken {
		public MetadataToken (uint token)
		{
			Decode (token, out this.Type, out this.RID);
		}

		public MetadataToken (TokenType type, uint rid)
		{
			this.Type = type;
			this.RID = rid;
		}

		public TokenType Type;
		public uint RID;

		public static void Decode (uint token, out TokenType type, out uint rid)
		{
			type = (TokenType) (token & 0xff000000);
			rid = (uint) token & 0x00ffffff;
		}

		public static uint Combine (TokenType type, uint rid)
		{
			return (uint)type | rid;
		}

		public static unsafe CString8 *GetTokenTypeString (TokenType type)
		{
			switch (type) {
			case TokenType.Module:
				return (CString8*)Stubs.CString ("Module");
			case TokenType.TypeRef:
				return (CString8*)Stubs.CString ("TypeRef");
			case TokenType.TypeDef:
				return (CString8*)Stubs.CString ("TypeDef");
			case TokenType.Field:
				return (CString8*)Stubs.CString ("Field");
			case TokenType.Method:
				return (CString8*)Stubs.CString ("Method");
			case TokenType.Param:
				return (CString8*)Stubs.CString ("Param");
			case TokenType.InterfaceImpl:
				return (CString8*)Stubs.CString ("InterfaceImpl");
			case TokenType.MemberRef:
				return (CString8*)Stubs.CString ("MemberRef");
			case TokenType.CustomAttribute:
				return (CString8*)Stubs.CString ("CustomAttribute");
			case TokenType.Permission:
				return (CString8*)Stubs.CString ("Permission");
			case TokenType.Signature:
				return (CString8*)Stubs.CString ("Signature");
			case TokenType.Event:
				return (CString8*)Stubs.CString ("Event");
			case TokenType.Property:
				return (CString8*)Stubs.CString ("Property");
			case TokenType.ModuleRef:
				return (CString8*)Stubs.CString ("ModuleRef");
			case TokenType.TypeSpec:
				return (CString8*)Stubs.CString ("TypeSpec");
			case TokenType.Assembly:
				return (CString8*)Stubs.CString ("Assembly");
			case TokenType.AssemblyRef:
				return (CString8*)Stubs.CString ("AssemblyRef");
			case TokenType.File:
				return (CString8*)Stubs.CString ("File");
			case TokenType.ExportedType:
				return (CString8*)Stubs.CString ("ExportedType");
			case TokenType.ManifestResource:
				return (CString8*)Stubs.CString ("ManifestResource");
			case TokenType.GenericParam:
				return (CString8*)Stubs.CString ("GenericParam");
			case TokenType.MethodSpec:
				return (CString8*)Stubs.CString ("MethodSpec");
			case TokenType.String:
				return (CString8*)Stubs.CString ("String");
			case TokenType.Name:
				return (CString8*)Stubs.CString ("Name");
			case TokenType.BaseType:
				return (CString8*)Stubs.CString ("BaseType");
			}

			return (CString8*)Stubs.CString ("Unknown");
		}

		public uint ToUInt ()
		{
			return Combine (Type, RID);
		}
	}
}
