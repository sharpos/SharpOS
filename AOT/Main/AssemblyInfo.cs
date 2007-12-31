using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// These attributes are used with Mono.GetOptions
//
// This is text that goes after " [options]" in help output.
[assembly: Mono.UsageComplement ("<assembly> ...")]

// Attributes visible in " -V"

[assembly: Mono.About ("The sharpos-aot compiler translates one or more IL bytecode assemblies into a\nraw platform-dependent kernel executable which may be run without operating\nsystem software. sharpos-aot is similar to the 'mono --aot' mode of\nthe Mono runtime, except that the resulting binary does not depend on any\nshared objects or operating system facilities.\n")]
[assembly: Mono.Author ("Mircea Cristian Racasan and the SharpOS Team (http://sharpos.org/)")]

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle ("SharpOS Ahead-Of-Time Compiler")]

[assembly: AssemblyDescription ("")]

[assembly: AssemblyConfiguration ("")]

[assembly: AssemblyCompany ("The SharpOS Project (http://sharpos.org/)")]

[assembly: AssemblyProduct ("SharpOS Ahead-Of-Time Compiler")]

[assembly: AssemblyCopyright ("Copyright © 2007, The SharpOS Project")]

[assembly: AssemblyTrademark ("")]

[assembly: AssemblyCulture ("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible (false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid ("eeb9a45d-2aef-47c8-8457-58ec8829a744")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//

[assembly: AssemblyVersion ("0.2.0.0")]

[assembly: AssemblyFileVersion ("0.2.0.0")]
