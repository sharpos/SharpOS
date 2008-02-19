// 
// (C) 2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Stanisław Pitucha <viraptor@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using SharpOS.AOT.IR;

namespace SharpOS.AOT.X86 {
	internal class BlobDataInstruction : DataInstruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="BlobDataInstruction"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public BlobDataInstruction (byte[] value)
			: base (false, string.Empty, string.Empty, string.Empty, string.Empty, null, null, null, value, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BlobDataInstruction"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public BlobDataInstruction (UInt32[] value)
			: base (false, string.Empty, string.Empty, string.Empty, string.Empty, null, null, null, value, null)
		{
		}

		/// <summary>
		/// Pushes all values from data blob to supplied binary writer.
		/// </summary>
		/// <param name="bits32">unused</param>
		/// <param name="binaryWriter">Binary writer used for dumping contents.</param>
		public override bool Encode (bool bits32, BinaryWriter binaryWriter)
		{
			if (this.Value is byte[])
				binaryWriter.Write (this.Value as byte[]);
			else if (this.Value is UInt32[]) {
				UInt32[] arr = this.Value as UInt32[];
				int len = arr.Length;
				for (int i=0; i<len; i++)
					binaryWriter.Write (arr[i]);
			} else
				throw new EngineException ("Wrong data type.");
			return true;
		}

		public override string Parameters
		{
			get
			{
				StringBuilder sb = new StringBuilder();

				if (this.value is byte[]) {
					sb.Append ("DB ");

					byte[] arr = this.value as byte[];
					int len = arr.Length;
					if (len > 0)
						sb.AppendFormat ("0x{0:X2}", arr[0]);

					for (int i=1; i<len; i++)
						sb.AppendFormat (", 0x{0:X2}", arr[i]);
				} else if (this.value is UInt32[]) {
					sb.Append ("DD ");

					UInt32[] arr = this.value as UInt32[];
					int len = arr.Length;
					if (len > 0)
						sb.AppendFormat ("0x{0:X8}", arr[0]);

					for (int i=1; i<len; i++)
						sb.AppendFormat (", 0x{0:X8}", arr[i]);
				} else
					throw new EngineException ("Wrong data type.");

				return sb.ToString();
			}
		}

	}
}
