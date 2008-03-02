//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;

namespace SharpOS.Kernel.ADC
{
	public static class SpinLock
	{

		#region Lock
		[AOTAttr.ADCStub]
		public static unsafe void Lock(uint* location)
		{

		}
		#endregion

        #region Release
        [AOTAttr.ADCStub]
		public static unsafe void Release(uint* location)
		{

		}
		#endregion

	}
}
