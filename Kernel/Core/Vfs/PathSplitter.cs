using System;

namespace SharpOS.Kernel.Vfs
{
	class PathSplitter
	{
		protected const uint MaxSeperators = 25;

		protected char[] path;
		protected int[] seperators;

		//public PathSplitter (string path)
		//{
		//    for (int i = 0; i < path.Length; i++)
		//        this.path[i] = path[i];

		//    MarkSeperators ();
		//}

		public PathSplitter (char[] path)
		{
			for (int i = 0; i < path.Length; i++)
				this.path[i] = path[i];

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

		//public string GetPath (int index)
		//{
		//    int start = (index == 0) ? 0 : seperators[index];
		//    int end = seperators[index + 1];

		//    return path.Substring (start, end - start);
		//}

		public char[] GetPath (int index)
		{
			int start = (index == 0) ? 0 : seperators[index];
			int end = seperators[index + 1];
			int len = end - start;

			char[] section = new char[len];

			for (int i = 0; i < len; i++)
				section[i] = path[i];

			return section;
		}

		public char[] this[int index]
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

	static class Util
	{
		static public char[] ToChar (string s)
		{
			char[] c = new char[s.Length];

			for (int i = 0; i < s.Length; i++)
				c[i] = s[i];

			return c;
		}
	}

}
