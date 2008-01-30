//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Runtime.InteropServices;
using SharpOS.AOT.Metadata;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Korlib.Runtime {

	public struct MetadataToken {
		public MetadataToken (uint token)
		{
			Decode (token, out this.Type, out this.RID);
		}

		public MetadataToken (TokenType type, uint rid)
		{
			this.Type = type;
			this.RID = rid;
		}

		public TokenType Type;
		public uint RID;

		public static void Decode (uint token, out TokenType type, out uint rid)
		{
			type = (TokenType) (token & 0xff000000);
			rid = (uint) token & 0x00ffffff;
		}
	}
}
