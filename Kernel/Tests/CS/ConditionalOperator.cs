//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.Tests.CS
{
	public class ConditionalOperator 
	{

		public static uint CMPQuestionOperator1()
		{
			uint a = 0;
			uint b = 10;

			uint c = (a != 0) ? a : b;

			if (c != 10)
				return 0;

			if (((a != 0) ? a : b) != 10)
				return 0;

			return 1;
		}

		[Flags]
		enum TestType { Apple = 0x1, Blueberry = 0x2, Cherry = 0x4 };

		public static uint CMPQuestionOperator2()
		{
			TestType testtype;
			TestType value = (TestType)2;

			testtype = ((value & TestType.Blueberry) == TestType.Blueberry) ? TestType.Blueberry : TestType.Apple;

			if (testtype != TestType.Blueberry)
				return 0;

			return 1;
		}

		public static uint CMPQuestionOperator3()
		{
			uint value = 1;

			value++;

			if ((((((TestType)value) & TestType.Blueberry) == TestType.Blueberry) ? TestType.Blueberry : TestType.Apple) != TestType.Blueberry)
				return 0;

			return 1;
		}

		public static uint CMPQuestionOperator4()
		{
			uint c = 100;
			uint b = 12;

			int a = (int)((b == 12) ? (c + (c / 2)) : (b == 16) ? c * 2 : c * 4);

			if (a != 150)
				return 0;

			b = 16;
			a = (int)((b == 12) ? (c + (c / 2)) : (b == 16) ? c * 2 : c * 4);

			if (a != 200)
				return 0;

			return 1;
		}

	}
}
