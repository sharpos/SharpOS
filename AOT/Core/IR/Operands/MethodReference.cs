// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Operands
{
	[Serializable]
	public class MethodReference : Operand
	{
		public MethodReference(Mono.Cecil.MethodReference methodReference)
			: base(null, new Operand[] { })
		{
			if (methodReference == null)
				throw new ArgumentNullException("methodReference");

			this.method = methodReference;
			this.SizeType = InternalSizeType.U; // Internal size type should be "native unsigned int"
		}

		private Mono.Cecil.MethodReference method;

		public Mono.Cecil.MethodReference Method
		{
			get
			{
				return method;
			}
		}

		public override string ToString()
		{
			return method.ToString();
		}
	}
}
