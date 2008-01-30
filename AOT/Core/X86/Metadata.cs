//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	public class MetadataVisitor : BaseMetadataVisitor {
		public MetadataVisitor (Assembly asm)
		{
			this.asm = asm;
		}

		Assembly asm;
		AssemblyDefinition currentModule = null;
		string moduleName = null;
		string [] potentiallyMissing = new string[] {
			"AssemblyRef",
			"Assembly",
			"ClassLayout",
			"Constant",
			"CustomAttribute",
			"DeclSecurity",
			"EventMap",
			"EventPtr",
			"Event",
			"ExportedType",
			"FieldLayout",
			"FieldMarshal",
			"FieldPtr",
			"FieldRVA",
			"Field",
			"File",
			"GenericParamConstraint",
			"GenericParam",
			"ImplMap",
			"InterfaceImpl",
			"ManifestResource",
			"MemberRef",
			"MethodImpl",
			"MethodPtr",
			"MethodSemantics",
			"MethodSpec",
			"Method",
			"ModuleRef",
			"Module",
			"NestedClass",
			"ParamPtr",
			"Param",
			"PropertyMap",
			"PropertyPtr",
			"Property",
			"StandAloneSig",
			"TypeDef",
			"TypeRef",
			"TypeSpec",
		};

		public void Encode (AssemblyDefinition def)
		{
			currentModule = def;
			moduleName = def.Name.FullName;
			currentModule.MainModule.Image.MetadataRoot.Accept (this);
		}

		public override void TerminateMetadataRoot (MetadataRoot root)
		{
			this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
			this.asm.LABEL (moduleName + " MetadataRoot");
			this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.AssemblyMetadata).ToString ());

			this.asm.ADDRESSOF (moduleName + " StringsHeap");
			this.asm.ADDRESSOF (moduleName + " BlobHeap");
			this.asm.ADDRESSOF (moduleName + " GuidHeap");
			this.asm.ADDRESSOF (moduleName + " UserStringsHeap");
			this.asm.ADDRESSOF (moduleName + " AssemblyRefArray");
			this.asm.ADDRESSOF (moduleName + " AssemblyArray");
			this.asm.ADDRESSOF (moduleName + " ClassLayoutArray");
			this.asm.ADDRESSOF (moduleName + " ConstantArray");
			this.asm.ADDRESSOF (moduleName + " CustomAttributeArray");
			this.asm.ADDRESSOF (moduleName + " DeclSecurityArray");
			this.asm.ADDRESSOF (moduleName + " EventMapArray");
			this.asm.ADDRESSOF (moduleName + " EventPtrArray");
			this.asm.ADDRESSOF (moduleName + " EventArray");
			this.asm.ADDRESSOF (moduleName + " ExportedTypeArray");
			this.asm.ADDRESSOF (moduleName + " FieldLayoutArray");
			this.asm.ADDRESSOF (moduleName + " FieldMarshalArray");
			this.asm.ADDRESSOF (moduleName + " FieldPtrArray");
			this.asm.ADDRESSOF (moduleName + " FieldRVAArray");
			this.asm.ADDRESSOF (moduleName + " FieldArray");
			this.asm.ADDRESSOF (moduleName + " FileArray");
			this.asm.ADDRESSOF (moduleName + " GenericParamConstraintArray");
			this.asm.ADDRESSOF (moduleName + " GenericParamArray");
			this.asm.ADDRESSOF (moduleName + " ImplMapArray");
			this.asm.ADDRESSOF (moduleName + " InterfaceImplArray");
			this.asm.ADDRESSOF (moduleName + " ManifestResourceArray");
			this.asm.ADDRESSOF (moduleName + " MemberRefArray");
			this.asm.ADDRESSOF (moduleName + " MethodImplArray");
			this.asm.ADDRESSOF (moduleName + " MethodPtrArray");
			this.asm.ADDRESSOF (moduleName + " MethodSemanticsArray");
			this.asm.ADDRESSOF (moduleName + " MethodSpecArray");
			this.asm.ADDRESSOF (moduleName + " MethodArray");
			this.asm.ADDRESSOF (moduleName + " ModuleRefArray");
			this.asm.ADDRESSOF (moduleName + " ModuleArray");
			this.asm.ADDRESSOF (moduleName + " NestedClassArray");
			this.asm.ADDRESSOF (moduleName + " ParamPtrArray");
			this.asm.ADDRESSOF (moduleName + " ParamArray");
			this.asm.ADDRESSOF (moduleName + " PropertyMapArray");
			this.asm.ADDRESSOF (moduleName + " PropertyPtrArray");
			this.asm.ADDRESSOF (moduleName + " PropertyArray");
			this.asm.ADDRESSOF (moduleName + " StandAloneSigArray");
			this.asm.ADDRESSOF (moduleName + " TypeDefArray");
			this.asm.ADDRESSOF (moduleName + " TypeRefArray");
			this.asm.ADDRESSOF (moduleName + " TypeSpecArray");
		}

		public override void VisitBlobHeap (BlobHeap heap)
		{
			this.asm.StaticArray (moduleName + " BlobHeap", heap.Data);
		}

		public override void VisitUserStringsHeap (UserStringsHeap heap)
		{
			this.asm.StaticArray (moduleName + " UserStringsHeap", heap.Data);
		}

		public override void VisitGuidHeap (GuidHeap heap)
		{
			this.asm.StaticArray (moduleName + " GuidHeap", heap.Data);
		}

		public override void VisitStringsHeap (StringsHeap heap)
		{
			this.asm.StaticArray (moduleName + " StringsHeap", heap.Data);
		}

		// Encoding

		void MetadataArray (string name, IMetadataTable table)
		{
			this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
			this.asm.LABEL (moduleName + " " + name + "Array");
			this.asm.AddArrayFields (table.Rows.Count);
			for (int x = 0; x < table.Rows.Count; ++x)
				this.asm.ADDRESSOF (moduleName + " " + name + "Row#" + x);
		}

		void EncodeAssemblyRefTable (AssemblyRefTable table)
		{
			int index = 0;

			foreach (AssemblyRefRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " AssemblyRefRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.AssemblyRefRow).ToString ());
				this.asm.DATA (row.MajorVersion);
				this.asm.DATA (row.MinorVersion);
				this.asm.DATA (row.BuildNumber);
				this.asm.DATA (row.RevisionNumber);
				this.asm.DATA ((uint)row.Flags);
				this.asm.DATA (row.PublicKeyOrToken);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Culture);
				this.asm.DATA (row.HashValue);
				++index;
			}

			this.MetadataArray ("AssemblyRef", table);
		}

		void EncodeAssemblyTable (AssemblyTable table)
		{
			int index = 0;

			foreach (AssemblyRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " AssemblyRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.AssemblyRow).ToString ());
				this.asm.DATA ((uint) row.HashAlgId);
				this.asm.DATA (row.MajorVersion);
				this.asm.DATA (row.MinorVersion);
				this.asm.DATA (row.BuildNumber);
				this.asm.DATA (row.RevisionNumber);
				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.PublicKey);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Culture);

				++index;
			}

			this.MetadataArray ("Assembly", table);
		}

		void EncodeClassLayoutTable (ClassLayoutTable table)
		{
			int index = 0;

			Console.WriteLine ("encoding class table for {0}", moduleName);
			foreach (ClassLayoutRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " ClassLayoutRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ClassLayoutRow).ToString ());
				this.asm.DATA (row.PackingSize);
				this.asm.DATA (row.ClassSize);
				this.asm.DATA (row.Parent);
				++index;
			}

			this.MetadataArray ("ClassLayout", table);
		}

		void EncodeConstantTable (ConstantTable table)
		{
			// TODO: shall we move constants here as well or not?
			// Also, the ElementType enum in Cecil does not specify
			// a base type.

			this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
			this.asm.LABEL (moduleName + " ConstantArray");
			this.asm.AddArrayFields (0);
		}

		void EncodeCustomAttributeTable (CustomAttributeTable table)
		{
			int index = 0;

			foreach (CustomAttributeRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " CustomAttributeRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.CustomAttributeRow).ToString ());
				this.asm.DATA (row.Parent.ToUInt ());
				this.asm.DATA (row.Type.ToUInt ());
				this.asm.DATA (row.Value);
				++index;
			}

			this.MetadataArray ("CustomAttribute", table);
		}

		void EncodeDeclSecurityTable (DeclSecurityTable table)
		{
			int index = 0;

			foreach (DeclSecurityRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " DeclSecurityRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.DeclSecurityRow).ToString ());
				this.asm.DATA ((ushort) row.Action);
				this.asm.DATA (row.Parent.ToUInt ());
				this.asm.DATA (row.PermissionSet);
				++index;
			}

			this.MetadataArray ("DeclSecurity", table);
		}

		void EncodeEventMapTable (EventMapTable table)
		{
			int index = 0;

			foreach (EventMapRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " EventMapRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.EventMapRow).ToString ());
				this.asm.DATA (row.Parent);
				this.asm.DATA (row.EventList);
				++index;
			}

			this.MetadataArray ("EventMap", table);
		}

		void EncodeEventPtrTable (EventPtrTable table)
		{
			int index = 0;

			foreach (EventPtrRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " EventPtrRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ClassLayoutRow).ToString ());
				this.asm.DATA (row.Event);
				++index;
			}

			this.MetadataArray ("EventPtr", table);
		}

		void EncodeEventTable (EventTable table)
		{
			int index = 0;

			foreach (EventRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " EventRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.EventRow).ToString ());
				this.asm.DATA ((ushort) row.EventFlags);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.EventType.ToUInt ());
				++index;
			}

			this.MetadataArray ("Event", table);
		}

		void EncodeExportedTypeTable (ExportedTypeTable table)
		{
			int index = 0;

			foreach (ExportedTypeRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " ExportedTypeRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ExportedTypeRow).ToString ());
				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.TypeDefId);
				this.asm.DATA (row.TypeName);
				this.asm.DATA (row.TypeNamespace);
				this.asm.DATA (row.Implementation.ToUInt ());
				++index;
			}

			this.MetadataArray ("ExportedType", table);
		}

		void EncodeFieldLayoutTable (FieldLayoutTable table)
		{
			int index = 0;

			foreach (FieldLayoutRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " FieldLayoutRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.FieldLayoutRow).ToString ());
				this.asm.DATA (row.Offset);
				this.asm.DATA (row.Field);
				++index;
			}

			this.MetadataArray ("FieldLayout", table);
		}

		void EncodeFieldMarshalTable (FieldMarshalTable table)
		{
			int index = 0;

			foreach (FieldMarshalRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " FieldMarshalRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.FieldMarshalRow).ToString ());
				this.asm.DATA (row.Parent.ToUInt ());
				this.asm.DATA (row.NativeType);
				++index;
			}

			this.MetadataArray ("FieldMarshal", table);
		}

		void EncodeFieldPtrTable (FieldPtrTable table)
		{
			int index = 0;

			foreach (FieldPtrRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " FieldPtrRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.FieldPtrRow).ToString ());

				this.asm.DATA (row.Field);
				++index;
			}

			this.MetadataArray ("FieldPtr", table);
		}

		void EncodeFieldRVATable (FieldRVATable table)
		{
			int index = 0;

			foreach (FieldRVARow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " FieldRVARow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.FieldRVARow).ToString ());
				this.asm.DATA (row.RVA.Value);
				this.asm.DATA (row.Field);
				++index;
			}

			this.MetadataArray ("FieldRVA", table);
		}

		void EncodeFieldTable (FieldTable table)
		{
			int index = 0;

			foreach (FieldRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " FieldRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.FieldRow).ToString ());

				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Signature);
				++index;
			}

			this.MetadataArray ("Field", table);
		}

		void EncodeFileTable (FileTable table)
		{
			int index = 0;

			foreach (FileRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " FileRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.FileRow).ToString ());
				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.HashValue);
				++index;
			}

			this.MetadataArray ("File", table);
		}

		void EncodeGenericParamConstraintTable (GenericParamConstraintTable table)
		{
			int index = 0;

			foreach (GenericParamConstraintRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " GenericParamConstraintRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.GenericParamConstraintRow).ToString ());
				this.asm.DATA (row.Owner);
				this.asm.DATA (row.Constraint.ToUInt ());
				++index;
			}

			this.MetadataArray ("GenericParamConstraint", table);
		}

		void EncodeGenericParamTable (GenericParamTable table)
		{
			int index = 0;

			foreach (GenericParamRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " GenericParamRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.GenericParamRow).ToString ());
				this.asm.DATA (row.Number);
				this.asm.DATA ((ushort) row.Flags);
				this.asm.DATA (row.Owner.ToUInt ());
				this.asm.DATA (row.Name);
				++index;
			}

			this.MetadataArray ("GenericParam", table);
		}

		void EncodeImplMapTable (ImplMapTable table)
		{
			int index = 0;

			foreach (ImplMapRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " ImplMapRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ImplMapRow).ToString ());
				this.asm.DATA ((uint) row.MappingFlags);
				this.asm.DATA (row.MemberForwarded.ToUInt ());
				this.asm.DATA (row.ImportName);
				this.asm.DATA (row.ImportScope);
				++index;
			}

			this.MetadataArray ("ImplMap", table);
		}

		void EncodeInterfaceImplTable (InterfaceImplTable table)
		{
			int index = 0;

			foreach (InterfaceImplRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " InterfaceImplRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.InterfaceImplRow).ToString ());
				this.asm.DATA (row.Class);
				this.asm.DATA (row.Interface.ToUInt ());
				++index;
			}

			this.MetadataArray ("InterfaceImpl", table);
		}

		void EncodeManifestResourceTable (ManifestResourceTable table)
		{
			int index = 0;

			foreach (ManifestResourceRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " ManifestResourceRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ManifestResourceRow).ToString ());
				this.asm.DATA (row.Offset);
				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Implementation.ToUInt ());
				++index;
			}

			this.MetadataArray ("ManifestResource", table);
		}

		void EncodeMemberRefTable (MemberRefTable table)
		{
			int index = 0;

			foreach (MemberRefRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " MemberRefRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.MemberRefRow).ToString ());
				this.asm.DATA (row.Class.ToUInt ());
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Signature);
				++index;
			}

			this.MetadataArray ("MemberRef", table);
		}

		void EncodeMethodImplTable (MethodImplTable table)
		{
			int index = 0;

			foreach (MethodImplRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " MethodImplRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.MethodImplRow).ToString ());
				this.asm.DATA (row.Class);
				this.asm.DATA (row.MethodBody.ToUInt ());
				this.asm.DATA (row.MethodDeclaration.ToUInt ());
				++index;
			}

			this.MetadataArray ("MethodImpl", table);
		}

		void EncodeMethodPtrTable (MethodPtrTable table)
		{
			int index = 0;

			foreach (MethodPtrRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " MethodPtrRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.MethodPtrRow).ToString ());

				this.asm.DATA (row.Method);
				++index;
			}

			this.MetadataArray ("MethodPtr", table);
		}

		void EncodeMethodSemanticsTable (MethodSemanticsTable table)
		{
			int index = 0;

			foreach (MethodSemanticsRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " MethodSemanticsRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.MethodSemanticsRow).ToString ());

				this.asm.DATA ((uint) row.Semantics);
				this.asm.DATA (row.Method);
				this.asm.DATA (row.Association.ToUInt ());
				++index;
			}

			this.MetadataArray ("MethodSemantics", table);
		}

		void EncodeMethodSpecTable (MethodSpecTable table)
		{
			int index = 0;

			foreach (MethodSpecRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " MethodSpecRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.MethodSpecRow).ToString ());

				this.asm.DATA (row.Method.ToUInt ());
				this.asm.DATA (row.Instantiation);
				++index;
			}

			this.MetadataArray ("MethodSpec", table);
		}

		void EncodeMethodTable (MethodTable table)
		{
			int index = 0;

			foreach (MethodRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " MethodRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.MethodRow).ToString ());

				this.asm.DATA (row.RVA.Value);
				this.asm.DATA ((uint) row.ImplFlags);
				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Signature);
				this.asm.DATA (row.ParamList);
				++index;
			}

			this.MetadataArray ("Method", table);
		}

		void EncodeModuleRefTable (ModuleRefTable table)
		{
			int index = 0;

			foreach (ModuleRefRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " ModuleRefRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ModuleRefRow).ToString ());

				this.asm.DATA (row.Name);
				++index;
			}

			this.MetadataArray ("ModuleRef", table);
		}

		void EncodeModuleTable (ModuleTable table)
		{
			int index = 0;

			foreach (ModuleRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " ModuleRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ModuleRow).ToString ());

				this.asm.DATA (row.Generation);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Mvid);
				this.asm.DATA (row.EncId);
				this.asm.DATA (row.EncBaseId);

				++index;
			}

			this.MetadataArray ("Module", table);
		}

		void EncodeNestedClassTable (NestedClassTable table)
		{
			int index = 0;

			foreach (NestedClassRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " NestedClassRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.NestedClassRow).ToString ());

				this.asm.DATA (row.NestedClass);
				this.asm.DATA (row.EnclosingClass);

				++index;
			}

			this.MetadataArray ("NestedClass", table);
		}

		void EncodeParamPtrTable (ParamPtrTable table)
		{
			int index = 0;

			foreach (ParamPtrRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " ParamPtrRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ParamPtrRow).ToString ());

				this.asm.DATA (row.Param);

				++index;
			}

			this.MetadataArray ("ParamPtr", table);
		}

		void EncodeParamTable (ParamTable table)
		{
			int index = 0;

			foreach (ParamRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " ParamRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ParamRow).ToString ());

				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.Sequence);
				this.asm.DATA (row.Name);

				++index;
			}

			this.MetadataArray ("Param", table);
		}

		void EncodePropertyMapTable (PropertyMapTable table)
		{
			int index = 0;

			foreach (PropertyMapRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " PropertyMapRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.PropertyMapRow).ToString ());

				this.asm.DATA (row.Parent);
				this.asm.DATA (row.PropertyList);

				++index;
			}

			this.MetadataArray ("PropertyMap", table);
		}

		void EncodePropertyPtrTable (PropertyPtrTable table)
		{
			int index = 0;

			foreach (PropertyPtrRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " PropertyPtrRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.PropertyPtrRow).ToString ());

				this.asm.DATA (row.Property);

				++index;
			}

			this.MetadataArray ("PropertyPtr", table);
		}

		void EncodePropertyTable (PropertyTable table)
		{
			int index = 0;

			foreach (PropertyRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " PropertyRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.PropertyRow).ToString ());

				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Type);

				++index;
			}

			this.MetadataArray ("Property", table);
		}

		void EncodeStandAloneSigTable (StandAloneSigTable table)
		{
			int index = 0;

			foreach (StandAloneSigRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " StandAloneSigRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.StandAloneSigRow).ToString ());

				this.asm.DATA (row.Signature);

				++index;
			}

			this.MetadataArray ("StandAloneSig", table);
		}

		void DumpTypeDef (TypeDefRow row, int index)
		{
			Console.WriteLine ("TypeDefRow#{0} {1} {2} {3} {4} {5} {6}",
				index,
				(uint)row.Flags,
				row.Name,
				row.Namespace,
				row.Extends.ToUInt (),
				row.FieldList,
				row.MethodList);
		}

		void EncodeTypeDefTable (TypeDefTable table)
		{
			int index = 0;

			foreach (TypeDefRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " TypeDefRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.TypeDefRow).ToString ());

				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Namespace);
				this.asm.DATA (row.Extends.ToUInt ());
				this.asm.DATA (row.FieldList);
				this.asm.DATA (row.MethodList);

				++index;
			}

			this.MetadataArray ("TypeDef", table);
		}

		void EncodeTypeRefTable (TypeRefTable table)
		{
			int index = 0;

			foreach (TypeRefRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " TypeRefRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.TypeRefRow).ToString ());

				this.asm.DATA (row.ResolutionScope.ToUInt ());
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Namespace);

				++index;
			}

			this.MetadataArray ("TypeRef", table);
		}

		void EncodeTypeSpecTable (TypeSpecTable table)
		{
			int index = 0;

			foreach (TypeSpecRow row in table.Rows) {
				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " TypeSpecRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.TypeSpecRow).ToString ());

				this.asm.DATA (row.Signature);

				++index;
			}

			this.MetadataArray ("TypeSpec", table);
		}

		public override void VisitTablesHeap (TablesHeap heap)
		{
			List <IMetadataTable> encodedTables = new List <IMetadataTable> ();

			foreach (IMetadataTable table in heap.Tables) {
				//this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				//this.asm.LABEL (moduleName + " " + table.GetType().Name);

				encodedTables.Add (table);

				if (table is AssemblyRefTable)
					EncodeAssemblyRefTable (table as AssemblyRefTable);
				if (table is AssemblyTable)
					EncodeAssemblyTable (table as AssemblyTable);
				if (table is ClassLayoutTable)
					EncodeClassLayoutTable (table as ClassLayoutTable);
				if (table is ConstantTable)
					EncodeConstantTable (table as ConstantTable);
				if (table is CustomAttributeTable)
					EncodeCustomAttributeTable (table as CustomAttributeTable);
				if (table is DeclSecurityTable)
					EncodeDeclSecurityTable (table as DeclSecurityTable);
				if (table is EventMapTable)
					EncodeEventMapTable (table as EventMapTable);
				if (table is EventPtrTable)
					EncodeEventPtrTable (table as EventPtrTable);
				if (table is EventTable)
					EncodeEventTable (table as EventTable);
				if (table is ExportedTypeTable)
					EncodeExportedTypeTable (table as ExportedTypeTable);
				if (table is FieldLayoutTable)
					EncodeFieldLayoutTable (table as FieldLayoutTable);
				if (table is FieldMarshalTable)
					EncodeFieldMarshalTable (table as FieldMarshalTable);
				if (table is FieldPtrTable)
					EncodeFieldPtrTable (table as FieldPtrTable);
				if (table is FieldRVATable)
					EncodeFieldRVATable (table as FieldRVATable);
				if (table is FieldTable)
					EncodeFieldTable (table as FieldTable);
				if (table is FileTable)
					EncodeFileTable (table as FileTable);
				if (table is GenericParamConstraintTable)
					EncodeGenericParamConstraintTable (table as GenericParamConstraintTable);
				if (table is GenericParamTable)
					EncodeGenericParamTable (table as GenericParamTable);
				if (table is ImplMapTable)
					EncodeImplMapTable (table as ImplMapTable);
				if (table is InterfaceImplTable)
					EncodeInterfaceImplTable (table as InterfaceImplTable);
				if (table is ManifestResourceTable)
					EncodeManifestResourceTable (table as ManifestResourceTable);
				if (table is MemberRefTable)
					EncodeMemberRefTable (table as MemberRefTable);
				if (table is MethodImplTable)
					EncodeMethodImplTable (table as MethodImplTable);
				if (table is MethodPtrTable)
					EncodeMethodPtrTable (table as MethodPtrTable);
				if (table is MethodSemanticsTable)
					EncodeMethodSemanticsTable (table as MethodSemanticsTable);
				if (table is MethodSpecTable)
					EncodeMethodSpecTable (table as MethodSpecTable);
				if (table is MethodTable)
					EncodeMethodTable (table as MethodTable);
				if (table is ModuleRefTable)
					EncodeModuleRefTable (table as ModuleRefTable);
				if (table is ModuleTable)
					EncodeModuleTable (table as ModuleTable);
				if (table is NestedClassTable)
					EncodeNestedClassTable (table as NestedClassTable);
				if (table is ParamPtrTable)
					EncodeParamPtrTable (table as ParamPtrTable);
				if (table is ParamTable)
					EncodeParamTable (table as ParamTable);
				if (table is PropertyMapTable)
					EncodePropertyMapTable (table as PropertyMapTable);
				if (table is PropertyPtrTable)
					EncodePropertyPtrTable (table as PropertyPtrTable);
				if (table is PropertyTable)
					EncodePropertyTable (table as PropertyTable);
				if (table is StandAloneSigTable)
					EncodeStandAloneSigTable (table as StandAloneSigTable);
				if (table is TypeDefTable)
					EncodeTypeDefTable (table as TypeDefTable);
				if (table is TypeRefTable)
					EncodeTypeRefTable (table as TypeRefTable);
				if (table is TypeSpecTable)
					EncodeTypeSpecTable (table as TypeSpecTable);
			}

			// Handle tables which were missing...

			foreach (string missing in potentiallyMissing) {
				bool found = false;

				foreach (IMetadataTable table in encodedTables) {
					if (table.GetType ().Name == missing + "Table") {
						found = true;
						break;
					}
				}

				if (found)
					continue;

				//Console.WriteLine ("Stubbing missing metadata table `{0} {1}Table'", moduleName, missing);

				this.asm.ALIGN (Assembly.OBJECT_ALIGNMENT);
				this.asm.LABEL (moduleName + " " + missing + "Array");
				this.asm.AddArrayFields (0);
			}
		}
	}
}