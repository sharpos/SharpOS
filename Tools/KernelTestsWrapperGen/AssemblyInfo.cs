using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: Mono.UsageComplement ("-c <ilasm.exe>")]
[assembly: Mono.Author ("Mircea-Cristian Racasan (darx_kies@gmx.net)")]
[assembly: Mono.About ("")]

[assembly: AssemblyTitle ("KernelTestsWrapperGen")]

[assembly: AssemblyDescription ("It generates the Wrapper.cs for the SharpOS.Kernel.Tests project and the NUnit wrapper. It also compiles the SharpOS.Kernel.Tests.IL.dll if the -c parameter is specified with the name and location of ilasm.exe")]

[assembly: AssemblyConfiguration ("")]

[assembly: AssemblyCompany ("The SharpOS Project (http://sharpos.org/)")]

[assembly: AssemblyProduct ("SharpOS Kernel Tests Wrapper Generator")]

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

[assembly: AssemblyVersion ("1.0.0.0")]

[assembly: AssemblyFileVersion ("1.0.0.0")]
