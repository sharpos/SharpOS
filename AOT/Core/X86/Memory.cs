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
using SharpOS.AOT.IR;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	public class Memory {
		/// <summary>
		/// Check32s the values.
		/// </summary>
		private void Check32Values ()
		{
			if ((index == R32.ESP && scale > 0) || (index == R32.ESP && scale == 0 && _base == R32.ESP))
				throw new EngineException ("ESP can't be used as index.");

			if (scale > 3)
				throw new EngineException ("The Scale can be 0, 1, 2 or 3.");

			if (_base == null && index == null && !this.displacementSet)
				throw new EngineException ("No valid 32bit address.");
		}

		/// <summary>
		/// Check16s the values.
		/// </summary>
		private void Check16Values ()
		{
			if (_base != null) {
				if (_base != R16.BX && _base != R16.BP && _base != R16.SI && _base != R16.DI)
					throw new EngineException ("16bit Register '" + _base.Name + "' is not allowed.");

				if (index != null && index != R16.SI && index != R16.DI)
					throw new EngineException ("16bit Register '" + index.Name + "' is not allowed.");

			} else if (index != null)
				throw new EngineException ("16bit Index Register is defined and the Base Register is missing.");

			if (_base == null && index == null && !displacementSet)
				throw new EngineException ("No valid 16bit address.");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Memory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="label">The label.</param>
		public Memory (SegType segment, string label)
		{
			this.segment = segment;
			this.reference = label;
			this.Displacement = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Memory"/> class.
		/// </summary>
		protected Memory ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Memory"/> class.
		/// </summary>
		/// <param name="label">The label.</param>
		public Memory (string label)
		{
			this.reference = label;
			this.Displacement = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Memory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="index">The index.</param>
		/// <param name="displacement">The displacement.</param>
		public Memory (SegType segment, R16Type _base, R16Type index, Int16 displacement)
		{
			this.bits32Address = false;
			this.segment = segment;
			this._base = _base;
			this.index = index;
			this.Displacement = displacement;

			this.Check16Values ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Memory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="index">The index.</param>
		public Memory (SegType segment, R16Type _base, R16Type index)
		{
			this.bits32Address = false;
			this.segment = segment;
			this._base = _base;
			this.index = index;

			this.Check16Values ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Memory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="index">The index.</param>
		/// <param name="scale">The scale.</param>
		/// <param name="displacement">The displacement.</param>
		public Memory (SegType segment, R32Type _base, R32Type index, byte scale, Int32 displacement)
		{
			this.segment = segment;
			this._base = _base;
			this.index = index;
			this.scale = scale;
			this.Displacement = displacement;

			Check32Values ();
		}

		public Memory (Memory memory)
		{
			displacement = memory.displacement;
			displacementSet = memory.displacementSet;


			bits32Address = memory.bits32Address;
			scale = memory.scale;
			index = memory.index;
			_base = memory._base;

			reference = memory.reference;


			segment = memory.segment;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Memory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="index">The index.</param>
		/// <param name="scale">The scale.</param>
		public Memory (SegType segment, R32Type _base, R32Type index, byte scale)
		{
			this.segment = segment;
			this._base = _base;
			this.index = index;
			this.scale = scale;

			Check32Values ();
		}

		protected Int32 displacement = 0;
		protected bool displacementSet = false;

		/// <summary>
		/// Gets or sets the displacement.
		/// </summary>
		/// <value>The displacement.</value>
		internal Int32 Displacement
		{
			get
			{
				return this.displacement;
			}
			set
			{
				this.displacementSet = true;
				this.displacement = value;
			}
		}

		protected Int32 displacementDelta = 0;

		/// <summary>
		/// Gets or sets the displacement delta.
		/// </summary>
		/// <value>The displacement delta.</value>
		internal Int32 DisplacementDelta
		{
			get
			{
				return this.displacementDelta;
			}
			set
			{
				if (this.reference.Length == 0) {
					this.displacementSet = true;
					this.displacement += value;
				} else
					this.displacementDelta = value;
			}
		}

		protected bool bits32Address = true;
		protected byte scale = 0;
		protected Register index = null, _base = null;

		protected string reference = string.Empty;

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

		protected SegType segment;

		/// <summary>
		/// Gets the segment.
		/// </summary>
		/// <value>The segment.</value>
		public SegType Segment
		{
			get
			{
				return segment;
			}
		}

		/// <summary>
		/// Encodes the specified bits32.
		/// </summary>
		/// <param name="bits32">if set to <c>true</c> [bits32].</param>
		/// <param name="spareRegister">The spare register.</param>
		/// <param name="binaryWriter">The binary writer.</param>
		/// <returns></returns>
		public bool Encode (bool bits32, byte spareRegister, BinaryWriter binaryWriter)
		{
			byte value = (byte) (spareRegister * 8);

			if (bits32 != this.bits32Address && (this._base != null || this.index != null))
				throw new EngineException ("Wrong kind of address. (16bit/32bit mix not allowed)");

			if (bits32) {
				R32Type _base = (R32Type) this._base, index = (R32Type) this.index;
				Int32 displacement = this.displacement;
				bool displacementSet = this.displacementSet;
				byte scale = this.scale;

				if ((this._base == null && this.index != null && this.scale == 0)
						|| this.index == R32.ESP) {
					_base = (R32Type) this.index;
					index = (R32Type) this._base;
				}

				if (_base == null && index != null && scale == 1) {
					_base = index;
					scale = 0;
				}

				bool fixEBP = false;

				if (!displacementSet && _base == R32.EBP) // && index == null) || (_base == null && index == R32.EBP)))
				{
					fixEBP = true;
					displacementSet = true;
					displacement = 0;
				}

				bool shortDisplacement = false;

				if (displacementSet != false) {
					if (_base == null && index == null) {
						value += 5;

						binaryWriter.Write (value);

						binaryWriter.Write ((UInt32) displacement);

						return true;

					} else if (_base != null) {
						if (fixEBP || (displacement >= -0x80 && displacement <= 0x7f)) {
							shortDisplacement = true;
							value += 1 * 64; // 8bit

						} else {
							value += 2 * 64; // 32bit
						}
					}
				}

				if (_base != R32.ESP && index == null) {
					value += _base.Index;

				} else {
					value += 0x04;
				}

				binaryWriter.Write (value);

				if (_base == R32.ESP || index != null) {
					value = (byte) (scale * 64);

					if (index != null) {
						value += (byte) (index.Index * 8);

					} else {
						value += 0x20;
					}

					if (_base != null) {
						value += _base.Index;

					} else {
						value += 0x05;
					}

					binaryWriter.Write (value);

					if (!this.displacementSet && _base == null)
						binaryWriter.Write ((UInt32) 0);
				}

				if (displacementSet) {
					if (fixEBP || shortDisplacement) //(displacement >= -0x80 && displacement <= 0x7f))
						binaryWriter.Write ((byte) displacement);

					else
						binaryWriter.Write ((UInt32) displacement);
				}

			} else {
				byte rm;
				Int16 displacement = (Int16) this.displacement;

				if (this._base == R16.BX && this.index == R16.SI) {
					rm = 0;

				} else if (this._base == R16.BX && this.index == R16.DI) {
					rm = 1;

				} else if (this._base == R16.BP && this.index == R16.SI) {
					rm = 2;

				} else if (this._base == R16.BP && this.index == R16.DI) {
					rm = 3;

				} else if (this._base == R16.SI && this.index == null) {
					rm = 4;

				} else if (this._base == R16.DI && this.index == null) {
					rm = 5;

				} else if (this._base == R16.BP && this.index == null) {
					rm = 6;

					if (!displacementSet) {
						value += 0x46;
						binaryWriter.Write (value);
						binaryWriter.Write ((byte) 0);
						return true;
					}

				} else if (this._base == R16.BX && this.index == null) {
					rm = 7;

				} else {
					rm = 6;
				}

				value += rm;

				if (this._base != null || this.index != null) {
					if (displacementSet != false) {
						if (displacement >= -0x80 && displacement <= 0x7f) {
							value += 0x40; // 8Bit

						} else {
							value += 0x80; // 16Bit
						}
					}
				}

				binaryWriter.Write (value);

				if (displacementSet != false) {
					if (displacement >= -0x80 && displacement <= 0x7f) {
						binaryWriter.Write ((byte) displacement);

					} else {
						binaryWriter.Write ((UInt16) displacement);
					}
				}
			}

			return true;
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

			if (this._base != null)
				stringBuilder.Append (this._base.Name);

			if (this.index != null) {
				if (stringBuilder.Length > 0)
					stringBuilder.Append (" + ");

				stringBuilder.Append (this.index.Name);

				stringBuilder.Append ("*");
				stringBuilder.Append (System.Math.Pow (2, this.scale));
			}

			if (this.displacementSet) {
				if (this.displacement >= 0) {
					if (stringBuilder.Length > 0)
						stringBuilder.Append (" + ");

					stringBuilder.Append (string.Format ("0x{0:x}", this.displacement));
				} else {
					if (stringBuilder.Length > 0)
						stringBuilder.Append (" - ");

					stringBuilder.Append (string.Format ("0x{0:x}", 0 - this.displacement));
				}
			}

			if (this.segment != null)
				stringBuilder.Insert (0, this.segment.Name + ":");

			return "[" + stringBuilder.ToString () + "]";
		}
	}
}