// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Sander van Rossen <sander.vanrossen@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.DriverSystem {
	
	public interface IDriverContext
	{
		DriverFlags					Flags				{ get; }

		void						Release				();
		bool						IsReleased			{ get; }
		
		// TODO: this should eventually be done trough attributes
		void						Initialize			(DriverFlags _flags);

		MemoryBlock					CreateMemoryBuffer	(uint address, uint length);
		IOPortStream				CreateIOPortStream	(ushort port);
	}
}
