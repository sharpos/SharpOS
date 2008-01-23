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
using SharpOS.AOT.Attributes;

namespace SharpOS.AOT.Metadata {
	[Include]
	[StructLayout (LayoutKind.Sequential)]
	public class AssemblyRow {
		public uint HashAlgId;
		public ushort MajorVersion;
		public ushort MinorVersion;
		public ushort BuildNumber;
		public ushort RevisionNumber;
		public uint Flags;
		public uint PublicKey;
		public uint Name;
		public uint Culture;
	}

	[Include]
	[StructLayout (LayoutKind.Sequential)]
	public class ModuleMetadata {
		public byte [] StringsHeap;
		public byte [] BlobHeap;
		public byte [] GuidHeap;
		public AssemblyRow [] Assembly;

		/*
		public AssemblyOSRow [] AssemblyOS;
		public AssemblyProcessorRow [] AssemblyProcessor;
		public AssemblyRefOSRow [] AssemblyRefOS;
		public AssemblyRefProcessorRow [] AssemblyRefProcessor;
		public AssemblyRefRow [] AssemblyRef;
		public ClassLayoutRow [] ClassLayout;
		public ConstantRow [] Constant;
		public CustomAttributeRow [] CustomAttribute;
		public DeclSecurityRow [] DeclSecurity;
		public EventMapRow [] EventMap;
		public EventPtrRow [] EventPtr;
		public EventRow [] Event;
		public ExportedTypeRow [] ExportedType;
		public FieldLayoutRow [] FieldLayout;
		public FieldMarshalRow [] FieldMarshal;
		public FieldPtrRow [] FieldPtr;
		public FieldRVARow [] FieldRVA;
		public FieldRow [] Field;
		public FileRow [] File;
		public GenericParamConstraintRow [] GenericParamConstraint;
		public GenericParamRow [] GenericParam;
		public IMetadataRow [] IMetadata;
		public ImplMapRow [] ImplMap;
		public InterfaceImplRow [] InterfaceImpl;
		public ManifestResourceRow [] ManifestResource;
		public MemberRefRow [] MemberRef;
		public MethodImplRow [] MethodImpl;
		public MethodPtrRow [] MethodPtr;
		public MethodSemanticsRow [] MethodSemantics;
		public MethodSpecRow [] MethodSpec;
		public MethodRow [] Method;
		public ModuleRefRow [] ModuleRef;
		public ModuleRow [] Module;
		public NestedClassRow [] NestedClass;
		public ParamPtrRow [] ParamPtr;
		public ParamRow [] Param;
		public PropertyMapRow [] PropertyMap;
		public PropertyPtrRow [] PropertyPtr;
		public PropertyRow [] Property;
		public StandAloneSigRow [] StandAloneSig;
		public TypeDefRow [] TypeDef;
		public TypeRefRow [] TypeRef;
		public TypeSpecRow [] TypeSpec;
		*/
	}
}