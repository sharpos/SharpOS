// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//  Phil Garcia <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.HAL
{
	public interface IRQHandler
	{
		void ClearInterrupt ();
		bool WaitForInterrupt (uint timeout);
		void AssignCallBack (IIRQCallBack callback);
	}

	//NOTE: Since delegates are not yet supported, an interface will be used instead
	//delegate bool OnInterrupt (uint irg); 
	public interface IIRQCallBack
	{
		bool OnInterrupt (uint irq);
	}

}
