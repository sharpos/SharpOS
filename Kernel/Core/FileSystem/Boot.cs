using System;
using SharpOS.Kernel.FileSystem;

namespace SharpOS.Kernel.FileSystem
{
	public static class Boot
	{
		public static void Start ()
		{
			Vfs.VirtualFileSystem.Setup ();
			FATFileSystem.FATDevice.Register ();
		}
	}
}
