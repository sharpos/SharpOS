// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;

namespace SharpOS.AOT.IR {

	/// <summary>
	/// Defines an exception thrown by <see cref="SharpOS.AOT.IR.Engine" />
	/// and related classes when an unrecoverable error occurs while performing
	/// the AOT operation. This exception should be caught by the program
	/// embedding the AOT engine and displayed to the user. This exception is
	/// not thrown when an internal AOT error occurs.
	/// </summary>
	public class EngineException : Exception {
		public EngineException(string message):
			base(message)
		{
		}
	}
}