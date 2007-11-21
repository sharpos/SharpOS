// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.IR {
	public class Class : IEnumerable<Method> {
		/// <summary>
		/// Initializes a new instance of the <see cref="Class"/> class.
		/// </summary>
		/// <param name="engine">The engine.</param>
		/// <param name="classDefinition">The class definition.</param>
		public Class (Engine engine, TypeDefinition classDefinition)
		{
			this.engine = engine;
			this.classDefinition = classDefinition;
		}

		private Engine engine = null;
		private TypeDefinition classDefinition = null;

		/// <summary>
		/// Gets the class definition.
		/// </summary>
		/// <value>The class definition.</value>
		public TypeDefinition ClassDefinition {
			get {
				return this.classDefinition;
			}
		}

		/// <summary>
		/// Adds the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public void Add (Method method)
		{
			this.methods.Add (method);
		}

		private List<Method> methods = new List<Method> ();

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
		/// </returns>
		IEnumerator<Method> IEnumerable<Method>.GetEnumerator ()
		{
			foreach (Method method in this.methods) 
				yield return method;
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((IEnumerable<Method>) this).GetEnumerator ();
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			if (this.classDefinition != null)
				return this.classDefinition.FullName;

			return base.ToString ();
		}
	}
}
