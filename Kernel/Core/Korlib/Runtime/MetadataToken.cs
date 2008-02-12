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

		public static unsafe string GetTokenTypeString (TokenType type)
		{
			switch (type) {
			case TokenType.Module:
				return "Module";
			case TokenType.TypeRef:
				return "TypeRef";
			case TokenType.TypeDef:
				return "TypeDef";
			case TokenType.Field:
				return "Field";
			case TokenType.Method:
				return "Method";
			case TokenType.Param:
				return "Param";
			case TokenType.InterfaceImpl:
				return "InterfaceImpl";
			case TokenType.MemberRef:
				return "MemberRef";
			case TokenType.CustomAttribute:
				return "CustomAttribute";
			case TokenType.Permission:
				return "Permission";
			case TokenType.Signature:
				return "Signature";
			case TokenType.Event:
				return "Event";
			case TokenType.Property:
				return "Property";
			case TokenType.ModuleRef:
				return "ModuleRef";
			case TokenType.TypeSpec:
				return "TypeSpec";
			case TokenType.Assembly:
				return "Assembly";
			case TokenType.AssemblyRef:
				return "AssemblyRef";
			case TokenType.File:
				return "File";
			case TokenType.ExportedType:
				return "ExportedType";
			case TokenType.ManifestResource:
				return "ManifestResource";
			case TokenType.GenericParam:
				return "GenericParam";
			case TokenType.MethodSpec:
				return "MethodSpec";
			case TokenType.String:
				return "String";
			case TokenType.Name:
				return "Name";
			case TokenType.BaseType:
				return "BaseType";
			}

			return "Unknown";
		}

		public uint ToUInt ()
		{
			return Combine (Type, RID);
		}
	}
}
