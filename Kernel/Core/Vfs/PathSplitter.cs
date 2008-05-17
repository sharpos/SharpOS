using System;

namespace SharpOS.Kernel.Vfs
{
	class PathSplitter
	{
		protected string path;
		protected int[] seperators;

		public PathSplitter (string path)
		{
			this.path = path;
			MarkSeperators ();
		}

		protected void MarkSeperators ()
		{
			int count = 0;
			for (int i = 0; i < path.Length; i++)
				if ((path[i] == System.IO.Path.DirectorySeparatorChar) || (path[i] == System.IO.Path.AltDirectorySeparatorChar))
					count++;

			seperators = new int[count + 1];

			count = 0;
			for (int i = 0; i < path.Length; i++)
				if ((path[i] == System.IO.Path.DirectorySeparatorChar) || (path[i] == System.IO.Path.AltDirectorySeparatorChar))
					seperators[count++] = i;
		}

		public string GetPath (int index)
		{
			int start = (index == 0) ? 0 : seperators[index];
			int end = seperators[index + 1];

			return path.Substring (start, end - start);
		}

		public string this[int index]
		{
			get
			{
				return GetPath (index);
			}
		}

		public int Length // Seperators
		{
			get
			{
				return seperators.Length - 1;
			}
		}
	}

}
