using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.Korlib
{
	static public class Convert
	{
		static public string ToString (char[] val, int startIndex, int length)
		{
			return InternalSystem.String.CreateStringImpl (val, startIndex, length);
		}

		static public string ToString (char[] val)
		{
			return InternalSystem.String.CreateStringImpl (val);
		}
	}
}
