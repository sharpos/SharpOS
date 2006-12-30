using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpOS.AOT
{
	public class CompilerOptions
	{
		private string _AssemblyFileName;
		private string _OutputFileName;
		private bool _CompileAllAsEnvironmentIndependent;
		private List<Type> _EnvironmentIndependentTypes = new List<Type>();
		private Dictionary<MethodInfo, string> _NativeInjectionLocations = new Dictionary<MethodInfo, string>();
		
		/// <summary>
		/// The assembly file name to compile.
		/// </summary>
		public string AssemblyFileName
		{
			get { return _AssemblyFileName; }
			set { _AssemblyFileName = value; }
		}
		
		public string OutputFileName
		{
			get { return _OutputFileName; }
			set { _OutputFileName = value; }
		}
		
		public bool CompileAllAsEnvironmentIndependent
		{
			get { return _CompileAllAsEnvironmentIndependent; }
			set { _CompileAllAsEnvironmentIndependent = value; }
		}
		
		public List<Type> EnvironmentIndependentTypes
		{
			get { return _EnvironmentIndependentTypes; }
		}
		
		public Dictionary<MethodInfo, string> NativeInjectionLocations
		{
			get { return _NativeInjectionLocations; }
		}
	}
}
