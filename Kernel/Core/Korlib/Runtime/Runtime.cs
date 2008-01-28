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
	public enum TokenType : uint {
		Module			= 0x00000000,
		TypeRef			= 0x01000000,
		TypeDef			= 0x02000000,
		Field			= 0x04000000,
		Method			= 0x06000000,
		Param			= 0x08000000,
		InterfaceImpl		= 0x09000000,
		MemberRef		= 0x0a000000,
		CustomAttribute		= 0x0c000000,
		Permission		= 0x0e000000,
		Signature		= 0x11000000,
		Event			= 0x14000000,
		Property		= 0x17000000,
		ModuleRef		= 0x1a000000,
		TypeSpec		= 0x1b000000,
		Assembly		= 0x20000000,
		AssemblyRef		= 0x23000000,
		File			= 0x26000000,
		ExportedType            = 0x27000000,
		ManifestResource	= 0x28000000,
		GenericParam		= 0x2a000000,
		MethodSpec		= 0x2b000000,
		String			= 0x70000000,
		Name			= 0x71000000,
		BaseType		= 0x72000000
	}

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
	}

	internal class Runtime {

		[AddressOf ("MetadataRoot")]
		private static MetadataRoot Root;

		public unsafe static CString8 *GetString (AssemblyMetadata assembly, uint str)
		{
			int length = 0;
			CString8 *buf;
			int index = 0;

			Diagnostics.Assert (str < assembly.StringsHeap.Length,
				"Runtime.ReadString(): parameter `str' is out of range");
			Diagnostics.Assert (assembly != null,
				"Runtime.ReadString(): parameter `assembly' is null");

			length = GetStringLength (assembly, str);
			buf = CString8.Create (length);

			for (int x = (int) str; index < length; ++x) {
				buf->SetChar (index, assembly.StringsHeap [x]);
				++index;
			}

			return buf;
		}

		public static TypeDefRow ResolveTypeRef (AssemblyMetadata assembly, TypeRefRow row)
		{
			AssemblyMetadata dest;
			return ResolveTypeRef (assembly, row, out dest);
		}

		public static unsafe TypeDefRow ResolveTypeRef (AssemblyMetadata assembly, TypeRefRow row,
			out AssemblyMetadata destAssembly)
		{
			CString8 *typeName = GetString (assembly, row.Name);
			CString8 *typeNamespace = GetString (assembly, row.Namespace);
			TokenType resScopeType;
			uint resScopeRID;
			TypeDefRow result = null;

			Diagnostics.Assert (assembly != null,
				"Runtime.ResolveTypeDef(): parameter `assembly' is null");
			Diagnostics.Assert (row != null,
				"Runtime.ResolveTypeDef(): parameter `row' is null");

			MetadataToken.Decode (row.ResolutionScope, out resScopeType, out resScopeRID);

			Diagnostics.Assert (resScopeType == TokenType.Assembly || resScopeType == TokenType.Module ||
					resScopeType == TokenType.AssemblyRef || resScopeType == TokenType.ModuleRef,
				"Runtime.ResolveTypeRef(): resolution scope token is of an invalid type");

			destAssembly = null;

			if (resScopeType == TokenType.Assembly || resScopeType == TokenType.Module || resScopeType == TokenType.ModuleRef) {
				Diagnostics.Assert (resScopeRID == 0,
					"Runtime.ResolveTypeRef(): resolution scope of Assembly must be zero!");
				destAssembly = assembly;
				return GetType (assembly, typeName, typeNamespace);
			} else if (resScopeType == TokenType.AssemblyRef) {
				Diagnostics.Assert (resScopeRID < assembly.AssemblyRef.Length,
					"Runtime.ResolveTypeRef(): AssemblyRef metadata token out of range");
				AssemblyMetadata assembly2 = ResolveAssemblyRef (assembly, assembly.AssemblyRef [resScopeRID]);

				destAssembly = assembly2;
				return GetType (assembly2, typeName, typeNamespace);
			}

			MemoryManager.Free (typeName);
			MemoryManager.Free (typeNamespace);

			return result;
		}

		public static unsafe AssemblyMetadata ResolveAssemblyRef (AssemblyMetadata assembly, AssemblyRefRow row)
		{
			// TODO: check more than just the name here

			Diagnostics.Assert (assembly != null,
				"Runtime.ResolveAssemblyRef(): parameter `assembly' is null");
			Diagnostics.Assert (row != null,
				"Runtime.ResolveAssemblyRef(): parameter `row' is null");

			CString8 *name = GetString (assembly, row.Name);
			AssemblyMetadata result = null;

			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				AssemblyMetadata assembly2 = Root.Assemblies [x];
				CString8 *name2;

				if (assembly2.Assembly.Length == 0)
					continue;	// unnamed assembly

				name2 = GetString (assembly2, assembly2.Assembly [0].Name);

				if (name->Compare (name2) == 0)
					result = assembly2;

				MemoryManager.Free (name2);

				if (result != null)
					break;
			}

			MemoryManager.Free (name);
			return result;
		}

		public static int GetStringLength (AssemblyMetadata assembly, uint str)
		{
			int len = 0;

			Diagnostics.Assert (assembly != null,
				"Runtime.GetStringLength(): parameter `assembly' is null");

			Diagnostics.Assert (str < assembly.StringsHeap.Length,
				"Runtime.GetStringLength(): invalid StringsHeap token");

			for (int z = (int) str; z < assembly.StringsHeap.Length; ++z) {
				if (assembly.StringsHeap [z] == (byte) '\0')
					break;
				++len;
			}

			return len;
		}

		public static ModuleRow ResolveModuleRef (AssemblyMetadata assembly, ModuleRefRow row)
		{
			AssemblyMetadata assem2;

			Diagnostics.Assert (assembly != null,
				"Runtime.ResolveModuleRef(): parameter `assembly' is null");

			Diagnostics.Assert (row != null,
				"Runtime.ResolveModuleRef(): parameter `row' is null");

			return ResolveModuleRef (assembly, row, out assem2);
		}

		public static unsafe ModuleRow ResolveModuleRef (AssemblyMetadata assembly, ModuleRefRow row, out AssemblyMetadata destAssembly)
		{
			CString8 *moduleName;
			ModuleRow result = null;

			Diagnostics.Assert (assembly != null,
				"Runtime.ResolveModuleRef(): parameter `assembly' is null");
			Diagnostics.Assert (row != null,
				"Runtime.ResolveModuleRef(): parameter `row' is null");

			moduleName = GetString (assembly, row.Name);
			destAssembly = null;

			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				AssemblyMetadata assembly2 = Root.Assemblies [x];

				for (int y = 0; y < assembly2.Module.Length; ++y) {
					ModuleRow mod = assembly2.Module [y];
					int len = GetStringLength (assembly2, mod.Name);

					if (moduleName->Compare (0, assembly2.StringsHeap, (int) mod.Name, len) == 0) {
						result = mod;
						destAssembly = assembly2;
						break;
					}
				}
			}

			return result;
		}

		public static unsafe ModuleRow GetModule (CString8 *name)
		{
			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				ModuleRow result = GetModule (Root.Assemblies [x], name);

				if (result != null)
					return result;
			}

			return null;
		}

		public static unsafe ModuleRow GetModule (AssemblyMetadata assembly, CString8 *name)
		{
			Diagnostics.Assert (assembly == null,
				"Runtime.GetModule(): parameter `assembly' is null");
			Diagnostics.Assert (name == null,
				"Runtime.GetModule(): parameter `name' is null");

			for (int x = 0; x < assembly.Module.Length; ++x) {
				ModuleRow row = assembly.Module [x];
				int len = GetStringLength (assembly, row.Name);

				if (name->Compare (0, assembly.StringsHeap, (int) row.Name, len) == 0)
					return row;
			}

			return null;
		}

		public static TypeDefRow GetType (MetadataToken token, out AssemblyMetadata dest)
		{
			dest = null;

			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				AssemblyMetadata def;
				TypeDefRow result = GetType (Root.Assemblies [x], token.Type, token.RID, out def);


				if (result != null) {
					return result;
					dest = def;
				}
			}

			return null;
		}

		public static TypeDefRow GetType (MetadataToken token)
		{
			AssemblyMetadata dest;
			return GetType (token, out dest);
		}

		public static TypeDefRow GetType (AssemblyMetadata assembly, MetadataToken token, out AssemblyMetadata dest)
		{
			return GetType (assembly, token.Type, token.RID, out dest);
		}

		public static TypeDefRow GetType (AssemblyMetadata assembly, MetadataToken token)
		{
			AssemblyMetadata dest;
			return GetType (assembly, token, out dest);
		}

		public static TypeDefRow GetType (AssemblyMetadata assembly, TokenType type, uint rid)
		{
			AssemblyMetadata dest;
			return GetType (assembly, type, rid, out dest);
		}

		public static TypeDefRow GetType (AssemblyMetadata assembly, TokenType type, uint rid,
			out AssemblyMetadata destAssembly)
		{
			Diagnostics.Assert (type == TokenType.TypeRef || type == TokenType.TypeDef,
				"Runtime.GetType(): token type must be either TypeRef or TypeDef");

			if (type == TokenType.TypeDef) {
				Diagnostics.Assert (rid < assembly.TypeDef.Length,
					"Runtime.GetType(): token out of range for TypeDef table");

				destAssembly = assembly;
				return assembly.TypeDef [rid];
			} else {
				TypeRefRow row = assembly.TypeRef [rid];

				return ResolveTypeRef (assembly, row, out destAssembly);
			}
		}

		public static unsafe TypeDefRow GetType (AssemblyMetadata assembly, CString8 *name, CString8 *ns)
		{
			TypeDefRow result = null;

			for (int x = 0; x < assembly.TypeDef.Length; ++x) {
				int nameLength = 0;
				int nsLength = 0;
				TypeDefRow inspect = assembly.TypeDef [x];

				nameLength = GetStringLength (assembly, inspect.Name);
				nsLength = GetStringLength (assembly, inspect.Namespace);

				if (name->Compare (0, assembly.StringsHeap, (int) inspect.Name, nameLength) != 0)
					continue;
				if (ns->Compare (0, assembly.StringsHeap, (int) inspect.Namespace, nsLength) != 0)
					continue;

				result = inspect;
				break;
			}

			return result;
		}

		public static unsafe bool IsTypeSystemObject (AssemblyMetadata assembly, TokenType type, uint rid)
		{
			CString8 *systemNS = CString8.Copy ("System");
			CString8 *objectName = CString8.Copy ("Object");
			int nameLen, nsLen;
			bool result = false;

			Diagnostics.Assert (type == TokenType.TypeRef || type == TokenType.TypeDef,
				"Runtime.IsTypeSystemObject(): invalid metadata token, must be of type `TypeDef' or `TypeRef' only");

			if (type == TokenType.TypeRef) {
				TypeRefRow row;

				// Handle this specially in case mscorlib is not included
				// (otherwise we will definitely get null from GetType())

				Diagnostics.Assert (rid < assembly.TypeRef.Length,
					"Runtime.IsTypeSystemObject(): invalid metadata token, too large for TypeRef table!");

				row = assembly.TypeRef [rid];
				nsLen = GetStringLength (assembly, row.Namespace);
				nameLen = GetStringLength (assembly, row.Name);

				if (systemNS->Compare (0, assembly.StringsHeap, (int) row.Namespace, nsLen) == 0 &&
				    objectName->Compare (0, assembly.StringsHeap, (int) row.Name, nameLen) == 0)
					result = true;
			} else if (type == TokenType.TypeDef) {
				TypeDefRow row;

				row = GetType (assembly, type, rid);
				nsLen = GetStringLength (assembly, row.Namespace);
				nameLen = GetStringLength (assembly, row.Name);

				if (systemNS->Compare (0, assembly.StringsHeap, (int) row.Namespace, nsLen) == 0 &&
				    objectName->Compare (0, assembly.StringsHeap, (int) row.Name, nameLen) == 0)
					result = true;
			}

			MemoryManager.Free (systemNS);
			MemoryManager.Free (objectName);

			return result;
		}

		public unsafe static void PrintTypeName (AssemblyMetadata assembly, TypeDefRow type)
		{
			CString8 *name, ns;

			name = GetString (assembly, type.Name);
			ns = GetString (assembly, type.Namespace);

			Serial.Write (ns);
			Serial.Write (".");
			Serial.Write (name);

			MemoryManager.Free (name);
			MemoryManager.Free (ns);
		}

		public unsafe static void DumpTypeDef (AssemblyMetadata assembly, TypeDefRow row, int index)
		{
			Serial.Write ("TypeDefRow#");
			Serial.Write (index);
			Serial.Write (" ");
			Serial.Write ((int)row.Flags);
			Serial.Write (" ");
			Serial.Write ((int)row.Name);
			Serial.Write (" ");
			Serial.Write ((int)row.Namespace);
			Serial.Write (" ");
			Serial.Write ((int)row.Extends);
			Serial.Write (" ");
			Serial.Write ((int)row.FieldList);
			Serial.Write (" ");
			Serial.Write ((int)row.MethodList);
			Serial.Write (" ");
			PrintTypeName (assembly, row);
			Serial.WriteLine ();
		}

		public static bool VerifyMetadata ()
		{
			bool result = true;

			//if (Root.Magic != MetadataRoot.MDMagic) {
			//	TextMode.Write ("Runtime: metadata root has invalid magic `", Root.Magic, true);
			//	TextMode.Write ("'");
			//}

			PrintTypeName (Root.Assemblies [0], Root.Assemblies [0].TypeDef [0]);

			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				bool skip = false;

				Serial.Write ("Assembly#");
				Serial.Write (x);

				if (Root.Assemblies [x].Magic != MetadataRoot.MDMagic) {
					Serial.Write (": invalid magic");
					result = false;
					skip = true;
				} else {
					Serial.Write (": valid magic");
				}

				Serial.Write (" `0x");
				Serial.Write ((int)Root.Assemblies [x].Magic, true);
				Serial.WriteLine ("'");

				if (skip)
					continue;

				int pass = 0;
				int total = 0;
				int startPass = -1;
				int firstFailure = -1;

				for (int y = 0; y < Root.Assemblies [x].TypeDef.Length; ++y) {
					TypeDefRow row = Root.Assemblies [x].TypeDef [y];

					if (row.Magic == MetadataRoot.MDMagic) {
						++pass;

						if (firstFailure == -1) {
							DumpTypeDef (Root.Assemblies [x], row, y);
						}
					} else {
						result = false;
						if (y < 3) {
							Serial.Write ("- TypeDef#");
							Serial.Write (y);
							Serial.Write (": invalid metadata `0x");
							Serial.Write ((int) row.Magic, true);
							Serial.WriteLine ("'");
						}

						if (startPass == -1)
							startPass = pass;
						if (firstFailure == -1)
							firstFailure = y;
					}

					++total;
				}

				if (pass != total) {
					Serial.Write (" - ");
					Serial.Write (Root.Assemblies [x].TypeDef.Length);
					Serial.Write (" total types, ");
					Serial.Write (total - pass);
					Serial.WriteLine (" with invalid magic signatures");
					Serial.Write (" - ");
					Serial.Write ((pass * 100 / total));
					Serial.Write ("% (");
					Serial.Write (pass);
					Serial.WriteLine (") of total types pass magic test");
					Serial.Write (" - ");
					Serial.Write ((startPass * 100 / total));
					Serial.Write ("% (");
					Serial.Write (startPass);
					Serial.WriteLine (") passed before the first failed magic test");
					Serial.Write (" - type #");
					Serial.Write (firstFailure);
					Serial.WriteLine (" was the first to fail");
				}
			}

			//if (Root.Assemblies [0].TypeDef [x].Magic != 200)
			//	Diagnostics.Error ("Runtime: type has invalid magic");

			return result;
		}

		public static void __RunTests ()
		{
			// Magic tests

			VerifyMetadata ();

			//__TestIsBaseClassOf ();
		}

		class TestA {
			public int Member1;
		}

		class TestB : TestA {
			public int Member2;
		}

		public static void __TestIsBaseClassOf ()
		{
			object o1 = new TestA ();
			object o2 = new TestB ();
			InternalSystem.Object io1, io2;
			uint ui = 1109;

			io1 = o1 as InternalSystem.Object;
			io2 = o2 as InternalSystem.Object;

			return;

			Testcase.Test (Runtime.IsBaseClassOf (io2.VTable.Type, io1.VTable.Type),
				"Runtime", "IsBaseClassOf where result should be true");
			Testcase.Test (Runtime.IsBaseClassOf (io1.VTable.Type, io2.VTable.Type) == false,
				"Runtime", "IsBaseClassOf where result should be false");
		}

		public static bool IsBaseClassOf (TypeInfo type, TypeInfo baseType)
		{
			TokenType typeTokType, baseTokType, interimTokType;
			uint typeRID, baseRID, interimRID;
			TypeDefRow typeDef, baseDef, interimDef;
			AssemblyMetadata interimAssembly;
			bool result = false;
			TypeDefRow lastInterimDef = null;

			MetadataToken.Decode (type.MetadataToken, out typeTokType, out typeRID);
			MetadataToken.Decode (baseType.MetadataToken, out baseTokType, out baseRID);

			//if (IsTypeSystemObject (baseType.Assembly, baseTokType, baseRID))
			//	return true;

			typeDef = GetType (type.Assembly, typeTokType, typeRID);
			baseDef = GetType (baseType.Assembly, baseTokType, baseRID);
			interimDef = typeDef;
			interimAssembly = type.Assembly;


			TextMode.Write ("IsBaseClassOf('");
			PrintTypeName (type.Assembly, typeDef);
			TextMode.Write ("', '");
			PrintTypeName (baseType.Assembly, baseDef);
			TextMode.WriteLine ("')");

			MetadataToken.Decode (typeDef.Extends, out interimTokType, out interimRID);

			for (int i = 0; i == 0;) {
				int nameLen;
				int nsLen;

				// If we've hit Object then the two types are unrelated

				if (IsTypeSystemObject (interimAssembly, interimTokType, interimRID))
					return false;

				lastInterimDef = interimDef;
				interimDef = GetType (interimAssembly, interimTokType, interimRID, out interimAssembly);

				Diagnostics.Assert (interimDef == null,
					"Runtime.IsBaseClassOf(): Failed to find TypeDef for the base class of a type");

				Diagnostics.Assert (lastInterimDef != interimDef,
					"Runtime.IsBaseClassOf(): ERROR: TypeDef's base class is itself!!!");

				if (interimDef == baseDef)
					return true;

				// Reiterate

				MetadataToken.Decode (interimDef.Extends, out interimTokType, out interimRID);
			}

			return false;
		}
	}
}
