//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

// TODO: break each class into separate files!

using System.Runtime.InteropServices;
using SharpOS.AOT.Attributes;

namespace SharpOS.AOT.Metadata {

	///////////////////// Miscellaneous types

	[Include]
	public enum SecurityAction : short {
		Request			= 1,
		Demand			= 2,
		Assert			= 3,
		Deny			= 4,
		PermitOnly		= 5,
		LinkDemand		= 6,
		InheritDemand		= 7,
		RequestMinimum		= 8,
		RequestOptional		= 9,
		RequestRefuse		= 10,
		PreJitGrant		= 11,
		PreJitDeny		= 12,
		NonCasDemand		= 13,
		NonCasLinkDemand	= 14,
		NonCasInheritance	= 15
	}

	///////////////////// Attribute enumerations (*Attributes)

	[Include]
	[System.Flags]
	public enum AssemblyFlags : uint {
		PublicKey			= 0x0001,
		SideBySideCompatible		= 0x0000,
		Retargetable			= 0x0100,
		EnableJITcompileTracking	= 0x8000,
		DisableJITcompileOptimizer	= 0x4000
	}

	[Include]
	[System.Flags]
	public enum EventAttributes : ushort {
		SpecialName	= 0x0200,
		RTSpecialName	= 0x0400,
	}

	/// <credits>
	/// from Mono.Cecil 0.6 by Jb Evain. (C) 2005 Jb Evain.
	/// </credits>
	[Include]
	public enum ExceptionHandlerType {
		Catch	= 0x0000,
		Filter	= 0x0001,
		Finally = 0x0002,
		Fault	= 0x0004
	}

	/// <credits>
	/// from Mono.Cecil 0.6 by Jb Evain. (C) 2005 Jb Evain.
	/// </credits>
	[Include]
	[System.Flags]
	public enum TypeAttributes : uint {
		// Visibility attributes
		VisibilityMask		= 0x00000007,	// Use this mask to retrieve visibility information
		NotPublic		= 0x00000000,	// Class has no public scope
		Public			= 0x00000001,	// Class has public scope
		NestedPublic		= 0x00000002,	// Class is nested with public visibility
		NestedPrivate		= 0x00000003,	// Class is nested with private visibility
		NestedFamily		= 0x00000004,	// Class is nested with family visibility
		NestedAssembly		= 0x00000005,	// Class is nested with assembly visibility
		NestedFamANDAssem	= 0x00000006,	// Class is nested with family and assembly visibility
		NestedFamORAssem	= 0x00000007,	// Class is nested with family or assembly visibility

		// Class layout attributes
		LayoutMask		= 0x00000018,	// Use this mask to retrieve class layout information
		AutoLayout		= 0x00000000,	// Class fields are auto-laid out
		SequentialLayout	= 0x00000008,	// Class fields are laid out sequentially
		ExplicitLayout		= 0x00000010,	// Layout is supplied explicitly

		// Class semantics attributes
		ClassSemanticMask	= 0x00000020,	// Use this mask to retrieve class semantics information
		Class			= 0x00000000,	// Type is a class
		Interface		= 0x00000020,	// Type is an interface

		// Special semantics in addition to class semantics
		Abstract		= 0x00000080,	// Class is abstract
		Sealed			= 0x00000100,	// Class cannot be extended
		SpecialName		= 0x00000400,	// Class name is special

		// Implementation attributes
		Import			= 0x00001000,	// Class/Interface is imported
		Serializable		= 0x00002000,	// Class is serializable

		// String formatting attributes
		StringFormatMask	= 0x00030000,	// Use this mask to retrieve string information for native interop
		AnsiClass		= 0x00000000,	// LPSTR is interpreted as ANSI
		UnicodeClass		= 0x00010000,	// LPSTR is interpreted as Unicode
		AutoClass		= 0x00020000,	// LPSTR is interpreted automatically

		// Class initialization attributes
		BeforeFieldInit		= 0x00100000,	// Initialize the class before first static field access

		// Additional flags
		RTSpecialName		= 0x00000800,	// CLI provides 'special' behavior, depending upon the name of the Type
		HasSecurity		= 0x00040000	 // Type has security associate with it
	}

	/// <credits>
	/// from Mono.Cecil 0.6 by Jb Evain. (C) 2005 Jb Evain.
	/// </credits>
	[Include]
	[System.Flags]
	public enum FieldAttributes : ushort {
		FieldAccessMask		= 0x0007,
		Compilercontrolled	= 0x0000,       // Member not referenceable
		Private			= 0x0001,       // Accessible only by the parent type
		FamANDAssem		= 0x0002,       // Accessible by sub-types only in this assembly
		Assembly		= 0x0003,       // Accessible by anyone in the Assembly
		Family			= 0x0004,       // Accessible only by type and sub-types
		FamORAssem		= 0x0005,       // Accessible by sub-types anywhere, plus anyone in the assembly
		Public			= 0x0006,       // Accessible by anyone who has visibility to this scope field contract attributes

		Static			= 0x0010,       // Defined on type, else per instance
		InitOnly		= 0x0020,       // Field may only be initialized, not written after init
		Literal			= 0x0040,       // Value is compile time constant
		NotSerialized		= 0x0080,       // Field does not have to be serialized when type is remoted
		SpecialName		= 0x0200,       // Field is special

		// Interop Attributes
		PInvokeImpl		= 0x2000,       // Implementation is forwarded through PInvoke

		// Additional flags
		RTSpecialName		= 0x0400,       // CLI provides 'special' behavior, depending upon the name of the field
		HasFieldMarshal		= 0x1000,       // Field has marshalling information
		HasDefault		= 0x8000,       // Field has default
		HasFieldRVA		= 0x0100         // Field has RVA
	}

	[Include]
	public enum FileAttributes : uint {
		ContainsMetaData	= 0,
		ContainsNoMetaData	= 1,
	}

	/// <credits>
	/// from Mono.Cecil 0.6 by Jb Evain. (C) 2005 Jb Evain.
	/// </credits>
	[Include]
	[System.Flags]
	public enum GenericParameterAttributes : ushort {
		VarianceMask	= 0x0003,
		NonVariant	= 0x0000,
		Covariant	= 0x0001,
		Contravariant   = 0x0002,

		SpecialConstraintMask		= 0x001c,
		ReferenceTypeConstraint		= 0x0004,
		NotNullableValueTypeConstraint	= 0x0008,
		DefaultConstructorConstraint	= 0x0010
	}

	/// <credits>
	/// from Mono.Cecil 0.6 by Jb Evain. (C) 2005 Jb Evain.
	/// </credits>
	[Include]
	[System.Flags]
	public enum PInvokeAttributes : ushort {
		NoMangle		= 0x0001,       // PInvoke is to use the member name as specified

		// Character set
		CharSetMask		= 0x0006,
		CharSetNotSpec          = 0x0000,
		CharSetAnsi		= 0x0002,
		CharSetUnicode		= 0x0004,
		CharSetAuto		= 0x0006,
		SupportsLastError	= 0x0040,       // Information about target function. Not relevant for fields

		// Calling convetion
		CallConvMask            = 0x0700,
		CallConvWinapi          = 0x0100,
		CallConvCdecl           = 0x0200,
		CallConvStdCall         = 0x0300,
		CallConvThiscall        = 0x0400,
		CallConvFastcall        = 0x0500
	}

	[Include]
	[System.Flags]
	public enum ManifestResourceAttributes : uint {
		VisibilityMask	= 7,
		Public		= 1,
		Private		= 2
	}

	/// <credits>
	/// from Mono.Cecil 0.6 by Jb Evain. (C) 2005 Jb Evain.
	/// </credits>
	[Include]
	[System.Flags]
	public enum MethodSemanticsAttributes : ushort {
		Setter          = 0x0001,       // Setter for property
		Getter          = 0x0002,       // Getter for property
		Other           = 0x0004,       // Other method for property or event
		AddOn           = 0x0008,       // AddOn method for event
		RemoveOn        = 0x0010,       // RemoveOn method for event
		Fire            = 0x0020         // Fire method for event
	}

	/// <credits>
	/// from Mono.Cecil 0.6 by Jb Evain. (C) 2005 Jb Evain.
	/// </credits>
	[Include]
	[System.Flags]
	public enum MethodImplAttributes : ushort {
		CodeTypeMask		= 0x0003,
		IL			= 0x0000,       // Method impl is CIL
		Native			= 0x0001,       // Method impl is native
		OPTIL			= 0x0002,       // Reserved: shall be zero in conforming implementations
		Runtime			= 0x0003,       // Method impl is provided by the runtime

		ManagedMask		= 0x0004,       // Flags specifying whether the code is managed or unmanaged
		Unmanaged		= 0x0004,       // Method impl is unmanaged, otherwise managed
		Managed			= 0x0000,       // Method impl is managed

		// Implementation info and interop
		ForwardRef		= 0x0010,       // Indicates method is defined; used primarily in merge scenarios
		PreserveSig		= 0x0080,       // Reserved: conforming implementations may ignore
		InternalCall		= 0x1000,       // Reserved: shall be zero in conforming implementations
		Synchronized		= 0x0020,       // Method is single threaded through the body
		NoInlining		= 0x0008,       // Method may not be inlined
		MaxMethodImplVal	= 0xffff         // Range check value
	}

	/// <credits>
	/// from Mono.Cecil 0.6 by Jb Evain. (C) 2005 Jb Evain.
	/// </credits>
	[Include]
	[System.Flags]
	public enum MethodAttributes : ushort {
		MemberAccessMask	= 0x0007,
		Compilercontrolled	= 0x0000,       // Member not referenceable
		Private			= 0x0001,       // Accessible only by the parent type
		FamANDAssem		= 0x0002,       // Accessible by sub-types only in this Assembly
		Assem			= 0x0003,       // Accessibly by anyone in the Assembly
		Family			= 0x0004,       // Accessible only by type and sub-types
		FamORAssem		= 0x0005,       // Accessibly by sub-types anywhere, plus anyone in assembly
		Public			= 0x0006,       // Accessibly by anyone who has visibility to this scope

		Static			= 0x0010,       // Defined on type, else per instance
		Final			= 0x0020,       // Method may not be overridden
		Virtual			= 0x0040,       // Method is virtual
		HideBySig		= 0x0080,       // Method hides by name+sig, else just by name

		VtableLayoutMask	= 0x0100,       // Use this mask to retrieve vtable attributes
		ReuseSlot		= 0x0000,       // Method reuses existing slot in vtable
		NewSlot			= 0x0100,       // Method always gets a new slot in the vtable

		Abstract		= 0x0400,       // Method does not provide an implementation
		SpecialName		= 0x0800,       // Method is special

		// Interop Attributes
		PInvokeImpl		= 0x2000,       // Implementation is forwarded through PInvoke
		UnmanagedExport		= 0x0008,       // Reserved: shall be zero for conforming implementations

		// Additional flags
		RTSpecialName		= 0x1000,       // CLI provides 'special' behavior, depending upon the name of the method
		HasSecurity		= 0x4000,       // Method has security associate with it
		RequireSecObject	= 0x8000         // Method calls another method containing security code
	}

	[Include]
	[System.Flags]
	public enum ParameterAttributes : ushort {
		None		= 0x0000,
		In		= 0x0001,       // Param is [In]
		Out		= 0x0002,       // Param is [Out]
		Optional	= 0x0010,       // Param is optional
		HasDefault	= 0x1000,       // Param has default value
		HasFieldMarshal	= 0x2000,       // Param has field marshal
		Unused		= 0xcfe0         // Reserved: shall be zero in a conforming implementation
	}

	[Include]
	[System.Flags]
	public enum PropertyAttributes : ushort {
		SpecialName	= 0x0200,
		RTSpecialName	= 0x0400,
		HasDefault	= 0x1000,
		Unused		= 0xe9ff,
	}

	/// <credits>
	/// from Mono.Cecil 0.6 by Jb Evain. (C) 2005 Jb Evain.
	/// </credits>
	[Include]
	public enum ElementType : byte {
		End		= 0x00,   // Marks end of a list
		Void		= 0x01,
		Boolean		= 0x02,
		Char		= 0x03,
		I1		= 0x04,
		U1		= 0x05,
		I2		= 0x06,
		U2		= 0x07,
		I4		= 0x08,
		U4		= 0x09,
		I8		= 0x0a,
		U8		= 0x0b,
		R4		= 0x0c,
		R8		= 0x0d,
		String		= 0x0e,
		Ptr		= 0x0f,   // Followed by <type> token
		ByRef		= 0x10,   // Followed by <type> token
		ValueType	= 0x11,   // Followed by <type> token
		Class		= 0x12,   // Followed by <type> token
		Var		= 0x13,   // Followed by generic parameter number
		Array		= 0x14,   // <type> <rank> <boundsCount> <bound1>  <loCount> <lo1>
		GenericInst	= 0x15,   // <type> <type-arg-count> <type-1> ... <type-n> */
		TypedByRef	= 0x16,
		I		= 0x18,   // System.IntPtr
		U		= 0x19,   // System.UIntPtr
		FnPtr		= 0x1b,   // Followed by full method signature
		Object		= 0x1c,   // System.Object
		SzArray		= 0x1d,   // Single-dim array with 0 lower bound
		MVar		= 0x1e,   // Followed by generic parameter number
		CModReqD	= 0x1f,   // Required modifier : followed by a TypeDef or TypeRef token
		CModOpt		= 0x20,   // Optional modifier : followed by a TypeDef or TypeRef token
		Internal	= 0x21,   // Implemented within the CLI
		Modifier	= 0x40,   // Or'd with following element types
		Sentinel	= 0x41,   // Sentinel for varargs method signature
		Pinned		= 0x45,   // Denotes a local variable that points at a pinned object

		// special undocumented constants
		Type		= 0x50,
		Boxed	   = 0x51,
		Enum		= 0x55
	}

	///////////////////// Table entry types (*Row)

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
	public class AssemblyRefRow {
		public ushort MajorVersion;
		public ushort MinorVersion;
		public ushort BuildNumber;
		public ushort RevisionNumber;
		public AssemblyFlags Flags;
		public uint PublicKeyOrToken;
		public uint Name;
		public uint Culture;
		public uint HashValue;
	}

	[Include]
	[StructLayout (LayoutKind.Sequential)]
	public class ClassLayoutRow {
		public ushort PackingSize;
		public uint Class;
		public uint Parent;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class ConstantRow {
		public ElementType Type;
		public uint Parent;
		public uint Value;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class CustomAttributeRow {
		public uint Parent;
		public uint Type;
		public uint Value;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class DeclSecurityRow {
		public SecurityAction Action;
		public uint Parent;
		public uint PermissionSet;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class EventMapRow {
		public uint Parent;
		public uint EventList;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class EventPtrRow {
		public uint Event;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class EventRow {
		public EventAttributes EventFlags;
		public uint Name;
		public uint EventType;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class ExportedTypeRow {
		public TypeAttributes Flags;
		public uint TypeDefId;
		public uint TypeName;
		public uint TypeNamespace;
		public uint Implementation;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class FieldLayoutRow {
		public uint Offset;
		public uint Field;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class FieldMarshalRow {
		public uint Parent;
		public uint NativeType;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class FieldPtrRow {
		public uint Field;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class FieldRVARow {
		public uint RVA;
		public uint Field;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class FieldRow {
		public FieldAttributes Flags;
		public uint Name;
		public uint Signature;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class FileRow {
		public FileAttributes Flags;
		public uint Name;
		public uint HashValue;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class GenericParamConstraintRow {
		public uint Owner;
		public uint Constraint;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class GenericParamRow {
		public ushort Number;
		public GenericParameterAttributes Flags;
		public uint Owner;
		public uint Name;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class ImplMapRow {
		public PInvokeAttributes MappingFlags;
		public uint MemberForwarded;
		public uint ImportName;
		public uint ImportScope;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class InterfaceImplRow {
		public uint Class;
		public uint Interface;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class ManifestResourceRow {
		public uint Offset;
		public ManifestResourceAttributes Flags;
		public uint Name;
		public uint Implementation;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class MemberRefRow {
		public uint Class;
		public uint Name;
		public uint Signature;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class MethodImplRow {
		public uint Class;
		public uint MethodBody;
		public uint MethodDeclaration;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class MethodPtrRow {
		public uint Method;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class MethodSemanticsRow {
		public MethodSemanticsAttributes Semantics;
		public uint Method;
		public uint Association;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class MethodSpecRow {
		public uint Method;
		public uint Instantiation;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class MethodRow {
		public uint RVA;
		public MethodImplAttributes ImplFlags;
		public MethodAttributes Flags;
		public uint Name;
		public uint Signature;
		public uint ParamList;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class ModuleRefRow {
		public uint Name;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class ModuleRow {
		public ushort Generation;
		public uint Name;
		public uint Mvid;
		public uint EncId;
		public uint EncBaseId;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class NestedClassRow {
		public uint NestedClass;
		public uint EnclosingClass;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class ParamPtrRow {
		public uint Param;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class ParamRow {
		public ParameterAttributes Flags;
		public ushort Sequence;
		public uint Name;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class PropertyMapRow {
		public uint Parent;
		public uint PropertyList;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class PropertyPtrRow {
		public uint Property;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class PropertyRow {
		public PropertyAttributes Flags;
		public uint Name;
		public uint Type;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class StandAloneSigRow {
		public uint Signature;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class TypeDefRow {
		public TypeAttributes Flags;
		public uint Name;
		public uint Namespace;
		public uint Extends;
		public uint FieldList;
		public uint MethodList;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class TypeRefRow {
		public uint ResolutionScope;
		public uint Name;
		public uint Namespace;
	}

	[Include]
	[StructLayout(LayoutKind.Sequential)]
	public class TypeSpecRow {
		public uint Signature;
	}

	[Include]
	[StructLayout (LayoutKind.Sequential)]
	public class AssemblyMetadata {
		public byte [] StringsHeap;
		public byte [] BlobHeap;
		public byte [] GuidHeap;
		public byte [] UserStringsHeap;
		public AssemblyRefRow [] AssemblyRef;
		public AssemblyRow [] Assembly;
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
	}

	[Include]
	[StructLayout (LayoutKind.Sequential)]
	public class MetadataRoot {
		public AssemblyMetadata [] Assemblies;
	}
}