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
}
