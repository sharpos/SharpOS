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
	///////////////////// Signature types (from the Blob heap)

	// WARNING: *not* for those with weak hearts and stomachs! You have been warned!

	public abstract class Signature : Metadata {
		protected Signature (AssemblyMetadata assembly):
			base (assembly)
		{
		}

		/// <summary>
		/// Signatures are compressed when in the blob heap. The compression acts
		/// on the integral values in the signature. Values could be 1, 2, or 4 bytes
		/// long, where either 7, 14, or 29 bits are used to hold the values respectively.
		/// For more information, see ECMA-335, fourth-edition, partition II, section 23.2,
		/// page 279.
		/// </summary>
		protected uint DecompressValue (byte [] blob, ref int pos)
		{
			// I can't believe I haven't drank any coffee in the last few hours, yet here I am.

			byte firstByte = blob [pos++];

			if (firstByte == 0xFF)
				return (uint)firstByte;		// null string. the easiest for sure.

			if ((firstByte & 0x80) == 0)
				return (uint)(firstByte & 0x7F);	// 1-byte with value in bits 6-0

			if ((firstByte & 0xC0) == 0x80) {
				short value = (short)((short) firstByte << 8 | (short) blob [pos++]);

				return (uint)(value & 0x3FFF);	// 2-byte with value in bits 13-0
			} else {
				return (uint) ((uint) firstByte << 24 | (uint) blob [pos++] << 16 | (uint) blob [pos++] << 8 |
					(uint) blob [pos++]);
			}
		}
	}

	public class FieldSignature : Signature {
		public FieldSignature (AssemblyMetadata assembly, FieldRow fieldRow):
			base (assembly)
		{
			this.Field = fieldRow;

			int pos = (int)fieldRow.Signature - 1;
			byte [] blob = assembly.BlobHeap;
			int cmodCount = 0;
			int savedPos = 0;
			int cmodIndex = 0;

			// The first byte must be FIELD (0x6)

			Diagnostics.Assert (blob [pos++] == 0x6,
				"FieldSignature.ctor(): blob signature is not FIELD (0x6)");

			//if (blob [pos] != 0x6) {
			//	throw new Exception ("Invalid signature type for field"); // explode
			//}

			// First determine the amount of cmods we have

			savedPos = pos;

			while (true) {
				ElementType type = (ElementType) DecompressValue (blob, ref pos);

				if (type == ElementType.CModReqD || type == ElementType.CModOpt)
					++cmodCount;
				else
					break;
			}

			this.CustomModifiers = new SigCustomModifier [cmodCount];
			pos = savedPos;

			while (true) {
				ElementType type = (ElementType) DecompressValue (blob, ref pos);

				if (type == ElementType.CModReqD || type == ElementType.CModOpt) {
					CustomModifiers [cmodIndex++] =
						new SigCustomModifier (type, DecompressValue (blob, ref pos));
				} else {
					// now for the "Type"

					switch (type) {
					case ElementType.Class:
						this.Type = new SigClassType (type, DecompressValue (blob, ref pos));
						break;
					case ElementType.ValueType:
						this.Type = new SigClassType (type, DecompressValue (blob, ref pos));
						break;
					case ElementType.Boolean:
					case ElementType.Char:
					case ElementType.I1:
					case ElementType.U1:
					case ElementType.I2:
					case ElementType.U2:
					case ElementType.I4:
					case ElementType.U4:
					case ElementType.I8:
					case ElementType.U8:
					case ElementType.R4:
					case ElementType.R8:
					case ElementType.I:
					case ElementType.U:
					case ElementType.Object:
					case ElementType.String:
						this.Type = new SigType (type);
						break;
					default:
						//throw new NotSupportedException (); // explode
						break;
					}

					// hereby ends this thousand-year journey
					break;
				}
			}

			++pos;
		}

		public AssemblyMetadata Assembly;
		public FieldRow Field;

		public SigCustomModifier [] CustomModifiers;
		public SigType Type;

		public override void Free ()
		{
			for (int x = 0; x < this.CustomModifiers.Length; ++x)
				Runtime.Free (this.CustomModifiers [x]);

			Runtime.Free (this.CustomModifiers);
			this.Type.Free ();
			Runtime.Free (this);
		}
	}

	/// <summary>
	/// Don't ask me what it's for, k? I don't know yet either!!
	/// </summary>
	public class SigCustomModifier {
		public SigCustomModifier (ElementType type, uint token)
		{
			Type = type;
			MetadataToken = token;
		}

		public ElementType Type;

		/// <summary>
		/// Can be "TypeDefEncoded" or "TypeRefEncoded" (straight from the spec)
		/// dunno why they didn't just say TypeDefOrRefEncoded.
		/// </summary>
		public uint MetadataToken;
	}

	/// <summary>
	/// This represents a "Type" value from a signature
	/// </summary>
	public class SigType {
		public SigType (ElementType type)
		{
			Type = type;
		}

		public ElementType Type;

		public void Free ()
		{
			// TODO: this is here because certain subclasses of SigType
			// will need to free the objects they create. Call the
			// subclass-specific static Free() for those types, else
			// use Runtime.Free();

			Runtime.Free (this);
		}
	}

	public class SigClassType : SigType {
		public SigClassType (ElementType type, uint token):
			base (type)
		{
			MetadataToken = token;
		}

		public uint MetadataToken;
	}

	public class SigValueType : SigType {
		public SigValueType (ElementType type, uint token):
			base (type)
		{
			MetadataToken = token;
		}

		public uint MetadataToken;
	}
}