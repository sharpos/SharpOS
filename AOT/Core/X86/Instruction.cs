//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	public class Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Instruction"/> class, which represents an x86
		/// instruction which is to be encoded in the output file. The raw instruction emitting methods
		/// found in Instructions.cs create an instance of this class to represent each individual instruction.
		/// </summary>
		/// <param name="indent">if set to <c>true</c> [indent].</param>
		/// <param name="label">The label.</param>
		/// <param name="reference">The reference.</param>
		/// <param name="name">The name.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="rmMemory">The rm memory.</param>
		/// <param name="rmRegister">The rm register.</param>
		/// <param name="register">The register.</param>
		/// <param name="value">The value.</param>
		/// <param name="encoding">The encoding.</param>
		public Instruction (bool indent, string label, string reference, string name, string parameters, Memory rmMemory, Register rmRegister, Register register, object value, string [] encoding)
		{
			this.label = Assembly.FormatLabelName (label);
			this.reference = reference;
			this.name = name;
			this.parameters = parameters;
			this.indent = indent;
			this.encoding = encoding;
			this.value = value;
			this.register = register;
			this.rmRegister = rmRegister;
			this.rmMemory = rmMemory;
		}

		private Register register = null;
		private Register rmRegister = null;
		private string [] encoding = null;

		/// <summary>
		/// Gets the values of the individual bytes which represent the binary encoding for this kind of instruction.
		/// </summary>
		/// <value>The encoding.</value>
		public string [] Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		/// <summary>
		/// Changes the encoding of this instruction to that of the given instruction. This method
		/// only changes the kind of instruction which is emitted, but not any arguments.
		/// </summary>
		/// <param name="instruction">The instruction.</param>
		public void Set (Instruction instruction)
		{
			this.encoding = instruction.encoding;
		}

		private Memory rmMemory = null;

		/// <summary>
		/// Gets the RM memory.
		/// </summary>
		/// <value>The RM memory.</value>
		public Memory RMMemory
		{
			get
			{
				return this.rmMemory;
			}
		}

		protected object value = null;

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public object Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
			}
		}

		private uint offset = 0;

		/// <summary>
		/// Gets or sets the offset in the binary file.
		/// </summary>
		/// <value>The offset.</value>
		public uint Offset
		{
			get
			{
				return offset;
			}
			set
			{
				offset = value;
			}
		}

		private string reference = string.Empty;

		/// <summary>
		/// Gets the reference.
		/// </summary>
		/// <value>The reference.</value>
		public string Reference
		{
			get
			{
				return reference;
			}
		}

		protected string label = string.Empty;

		/// <summary>
		/// Gets the label.
		/// </summary>
		/// <value>The label.</value>
		public string Label
		{
			get
			{
				return label;
			}
		}

		private string name = string.Empty;

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get
			{
				return name;
			}
		}

		/// <summary>
		/// Gets the short name only if it is not a label name.
		/// </summary>
		/// <value>The name of the short.</value>
		public virtual string ShortName
		{
			get
			{
				string result = name;

				if (result.Length > 0
						&& result [result.Length - 1] != ':'
						&& result.IndexOf ("_") != -1) {
					string [] values = result.Split ('_');

					result = values [0];
				}

				return result;
			}
		}

		protected string parameters = string.Empty;

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>The parameters.</value>
		public virtual string Parameters
		{
			get
			{
				return parameters;
			}
		}

		private bool indent = true;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Instruction"/> is indent.
		/// </summary>
		/// <value><c>true</c> if indent; otherwise, <c>false</c>.</value>
		public bool Indent
		{
			get
			{
				return indent;
			}
		}

		private int index = int.MaxValue;

		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index
		{
			get
			{
				return index;
			}
			set
			{
				index = value;
			}
		}


		/// <summary>
		/// Gets a value indicating whether this <see cref="Instruction"/> is relative.
		/// </summary>
		/// <value><c>true</c> if relative; otherwise, <c>false</c>.</value>
		public bool Relative
		{
			get
			{
				bool relative = false;

				foreach (string encodingValue in this.encoding) {
					if (encodingValue.StartsWith ("r")) {
						relative = true;
						break;
					}
				}

				return relative;
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder ();

			int indentSize = 20;

			stringBuilder.Append (string.Format ("0x{0:x8}\t", this.Offset));

			if (this.indent) {
				stringBuilder.Append (string.Empty.PadRight (indentSize));

				stringBuilder.Append (this.ShortName + " ");

			} else
				stringBuilder.Append (this.ShortName.PadRight (indentSize));

			if (this.RMMemory != null
					&& this.RMMemory.Reference != null
					&& this.RMMemory.Reference.Length > 0) {
				stringBuilder.Append (this.Parameters.Replace ("[0x0]", "[" + this.RMMemory.Reference + "]"));

				stringBuilder.Append ("\t;" + string.Format ("0x{0:x}", this.RMMemory.Displacement));

			} else
				stringBuilder.Append (this.Parameters);


			return stringBuilder.ToString ();
		}

		class BlackholeStream : Stream {
			long position = 0;
			
			public override long Position {
				get { return position; }
				set { position = value; }
			}
			
			public override void Write (byte[] buffer, int offset, int count)
			{
				position += count;
			}

			public override bool CanRead {
				get { return false; }
			}

			public override bool CanWrite {
				get { return true; }
			}

			public override bool CanSeek {
				get { return false; }
			}

			public override long Length {
				get { return position; }
			}

			public override void Flush ()
			{
				return;
			}

			public override long Seek (long offset, SeekOrigin origin)
			{
				throw new NotImplementedException ();
			}

			public override int Read (byte[] buffer, int offset, int count)
			{
				throw new NotImplementedException ();
			}

			public override void SetLength (long value)
			{
				throw new NotImplementedException ();
			}

		}

		/// <summary>
		/// Sizes the specified bits32.
		/// </summary>
		/// <param name="bits32">if set to <c>true</c> [bits32].</param>
		/// <returns></returns>
		public UInt32 Size (bool bits32)
		{
			BlackholeStream bs = new BlackholeStream();
			BinaryWriter binaryWriter = new BinaryWriter (bs);

			this.Encode (bits32, binaryWriter);

			return (UInt32) bs.Length;
		}

		const string hexChars = "0123456789ABCDEF"; 

		/// <summary>
		/// Encodes the specified bits32.
		/// </summary>
		/// <param name="bits32">if set to <c>true</c> [bits32].</param>
		/// <param name="binaryWriter">The binary writer.</param>
		/// <returns></returns>
		public virtual bool Encode (bool bits32, BinaryWriter binaryWriter)
		{
			int i = 0;
			int valueIndex = 0;

			if (this.encoding == null || this.encoding.Length == 0)
				return true;

			if (this.rmMemory != null && this.rmMemory.Segment != null)
				binaryWriter.Write (this.rmMemory.Segment.Value);

			for (; i < this.encoding.Length; i++) {
				string token = this.encoding [i].ToUpper ();

				switch(token) {
				case "O16":
					if (bits32)
						binaryWriter.Write ((byte) 0x66);
					break;
				case "O32":
					if (!bits32)
						binaryWriter.Write ((byte) 0x66);
					break;
				case "A16":
					if (bits32)
						binaryWriter.Write ((byte) 0x67);
					break;
				case "A32":
					if (!bits32)
						binaryWriter.Write ((byte) 0x67);
					break;
				case "RW/RD":
					if (bits32)
						binaryWriter.Write ((UInt32) ((UInt32) ((UInt32 []) this.value) [valueIndex++] - binaryWriter.BaseStream.Length - 4));

					else
						binaryWriter.Write ((UInt16) ((UInt16) ((UInt32 []) this.value) [valueIndex++] - binaryWriter.BaseStream.Length - 2));
					break;
				case "OW/OD":
					if (bits32)
						binaryWriter.Write ((UInt32) ((UInt32) ((UInt32 []) this.value) [valueIndex++]));

					else
						binaryWriter.Write ((UInt16) ((UInt16) ((UInt32 []) this.value) [valueIndex++]));
					break;
				case "IB":
					binaryWriter.Write ((byte) ((UInt32 []) this.value) [valueIndex++]);
					break;
				case "IW":
					binaryWriter.Write ((UInt16) ((UInt32 []) this.value) [valueIndex++]);
					break;
				case "ID":
					binaryWriter.Write ((UInt32) ((UInt32 []) this.value) [valueIndex++]);
					break;
				case "RB":
					binaryWriter.Write ((byte) ((byte) ((UInt32 []) this.value) [valueIndex++] - binaryWriter.BaseStream.Length - 1));
					break;
				case "RW":
					binaryWriter.Write ((UInt16) ((UInt16) ((UInt32 []) this.value) [valueIndex++] - binaryWriter.BaseStream.Length - 2));
					break;
				case "RD":
					binaryWriter.Write ((UInt32) ((UInt32) ((UInt32 []) this.value) [valueIndex++] - binaryWriter.BaseStream.Length - 4));
					break;
				case "/R":
					if (this.register != null && this.rmRegister != null) {
						byte value = (byte) (0xC0 + this.register.Index * 8 + this.rmRegister.Index);

						binaryWriter.Write (value);

					} else if (this.register != null && this.rmMemory != null) {
						this.rmMemory.Encode (bits32, this.register.Index, binaryWriter);

					} else if (this.register != null) {
						byte value = (byte) (0xC0 + this.register.Index * 8 + this.register.Index);

						binaryWriter.Write (value);
					}
					break;
				default:
					int ha, hb;
					if (token.Length == 2
						&& (ha = hexChars.IndexOf (token [0])) != -1
						&& (hb = hexChars.IndexOf (token [1])) != -1) {
						byte value = (byte) (ha * 16 + hb);

						binaryWriter.Write (value);
					} else if (token.EndsWith ("+R")) {
						token = token.Substring (0, token.Length - 2);

						byte value = (byte) (hexChars.IndexOf (token [0]) * 16 + hexChars.IndexOf (token [1]));

						binaryWriter.Write ((byte) (value + this.register.Index));
					} else if (token[0] == '/') {
						token = token.Substring (1);

						if (this.value != null) {
							if (this.register != null || this.rmRegister != null) {
								byte value = (byte) (0xC0 + byte.Parse (token) * 8);

								if (this.register != null) {
									value += this.register.Index;
								} else if (this.rmRegister != null) {
									value += this.rmRegister.Index;
								}

								binaryWriter.Write (value);

							} else if (this.rmMemory != null) {
								this.rmMemory.Encode (bits32, byte.Parse (token), binaryWriter);
							}

						} else if (this.rmMemory != null) {
							this.rmMemory.Encode (bits32, byte.Parse (token), binaryWriter);

						} else if (this.rmRegister != null) {
							byte value = (byte) (0xC0 + byte.Parse (token) * 8);

							value += this.rmRegister.Index;

							binaryWriter.Write (value);

						} else {
							byte value = (byte) (0xC0 + byte.Parse (token) * 8);

							binaryWriter.Write (value);
						}
					}
					break;
				}
			}

			return true;
		}
	}
}