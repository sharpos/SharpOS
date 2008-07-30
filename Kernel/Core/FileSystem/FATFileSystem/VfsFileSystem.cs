using System;
using SharpOS.Kernel.Vfs;

namespace SharpOS.Kernel.FileSystem.FATFileSystem
{
	public class VfsFileSystem : IFileSystemService, IFileSystem
	{
		protected FAT fat;

		public FAT FAT
		{
			get { return fat; }
		}

		public VfsFileSystem (FAT fat)
		{
			this.fat = fat;
		}

		public object SettingsType
		{
			get
			{
				return fat.SettingsType;
			}
		}

		public bool Mount ()
		{
			return true;
		}

		public bool Format (SettingsBase settings)
		{
			return (fat.Format (((FATSettings)settings)));
		}

		public bool IsReadOnly
		{
			get
			{
				return fat.IsReadOnly;
			}
		}

		private VfsDirectory root;

		public IVfsNode Root
		{
			get
			{
				if (root == null)
					root = new VfsDirectory (this, 0);

				return root;
			}
		}

	}
}
