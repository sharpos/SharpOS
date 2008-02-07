//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

#define DEBUG_EXCEPTION_HANDLING

using System.Runtime.InteropServices;
using SharpOS.AOT.Metadata;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Korlib.Runtime {

	public class TypeMetadata : Metadata {
		public TypeMetadata (AssemblyMetadata assembly, uint token):
			base (assembly)
		{
			TokenType tokenType;
			uint tokenRID;
			uint fieldLen, methodLen;
			AssemblyMetadata result;

			SharpOS.Korlib.Runtime.MetadataToken.Decode (token, out tokenType, out tokenRID);

			this.typeDef = Runtime.GetType (assembly, tokenType, tokenRID, out result, out tokenRID);
			this.metadataToken = SharpOS.Korlib.Runtime.MetadataToken.Combine (TokenType.TypeDef, tokenRID);
			this.Assembly = result;

			//if (typeDef == null)
			//	throw new Exception ("Could not locate TypeDef for given type");

			// Fill the fields

			//if (this.Assembly.TypeDef [tokenRID].FieldList <= this.typeDef.FieldList)
			//	throw new Exception ("Invalid metadata: next type has a bad FieldList compared to current");

			//if (this.Assembly.TypeDef [tokenRID].MethodList <= this.typeDef.MethodList)
			//	throw new Exception ("Invalid metadata: next type has a bad FieldList compared to current");

			fieldLen = this.Assembly.TypeDef [tokenRID].FieldList - this.typeDef.FieldList;
			methodLen = this.Assembly.TypeDef [tokenRID].MethodList - this.typeDef.MethodList;

			this.fields = new FieldRow [fieldLen];
			this.methods = new MethodRow [methodLen];

			for (int x = 0; x < fieldLen; ++x)
				this.fields [x] = this.Assembly.Field [this.typeDef.FieldList + x - 1];

			for (int x = 0; x < methodLen; ++x)
				this.methods [x] = this.Assembly.Method [this.typeDef.MethodList + x - 1];
		}

		uint metadataToken;
		TypeDefRow typeDef;
		FieldRow [] fields;
		MethodRow [] methods;
		unsafe CString8 *name, ns, fullname;

		private unsafe void FillStrings ()
		{
			this.name = Runtime.GetString (Assembly, typeDef.Name);
			this.ns = Runtime.GetString (Assembly, typeDef.Namespace);
			this.fullname = null; // TODO: tiredness alert.
		}

		public unsafe override void Free ()
		{
			Runtime.Free (this.fields);
			Runtime.Free (this.methods);
			MemoryManager.Free (this.name);
			MemoryManager.Free (this.ns);
			MemoryManager.Free (this.fullname);
			Runtime.Free (this);
		}

		public uint MetadataToken {
			get { return this.metadataToken; }
		}

		public TypeDefRow TypeDef {
			get { return this.typeDef; }
		}

		public FieldRow [] Fields {
			get { return this.fields; }
		}

		public MethodRow [] Methods {
			get { return this.methods; }
		}

		public FieldMetadata ResolveField (int index)
		{
			//if (index < 0 || index > this.fields.Length)
			//	throw new IndexOutOfRangeException ();

			return ResolveField (this.fields [index]);
		}

		public FieldMetadata ResolveField (FieldRow row)
		{
			return new FieldMetadata (this.Assembly, row);
		}

		public unsafe CString8 *Name {
			get {
				return this.name;
			}
		}

		public unsafe CString8 *Namespace {
			get {
				return this.ns;
			}
		}

		public unsafe CString8 *FullName {
			get {
				return this.fullname;
			}
		}
	}
}
