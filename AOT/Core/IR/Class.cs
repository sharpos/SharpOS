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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
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

		public void Setup ()
		{
			if (this.classDefinition.IsEnum) {
				foreach (FieldDefinition field in this.classDefinition.Fields) {
					if ((field.Attributes & FieldAttributes.RTSpecialName) != 0) {
						this.internalType = this.engine.GetInternalType (field.FieldType.FullName);
						break;
					}
				}

			} else if (this.classDefinition.IsValueType)
				this.internalType = Operands.InternalType.ValueType;

			else if (this.classDefinition.IsClass)
				this.internalType = Operands.InternalType.O;


			foreach (FieldDefinition field in this.classDefinition.Fields)
				fields [field.Name] = new Field (field);

			if (this.TypeFullName != Mono.Cecil.Constants.Object)
				this._base = this.engine.GetClass (this.classDefinition.BaseType);
		}

		Class _base = null;

		Dictionary<string, Field> fields = new Dictionary<string, Field> ();

		public Dictionary<string, Field> Fields
		{
			get
			{
				return this.fields;
			}
		}

		/// <summary>
		/// Gets the name of the field by.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public Field GetFieldByName (string value)
		{
			if (!fields.ContainsKey (value))
				throw new EngineException (string.Format ("Field '{0}' not found.", value));

			return fields [value];
		}

		/// <summary>
		/// Gets the name of the method by.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public Method GetMethodByName (string value)
		{
			if (!this.methodsDictionary.ContainsKey (value))
				throw new EngineException (string.Format ("Method '{0}' not found.", value));

			return this.methodsDictionary [value];
		}

		private Engine engine = null;

		public Engine Engine
		{
			get
			{
				return engine;
			}
		}

		private TypeDefinition classDefinition = null;

		/// <summary>
		/// Gets the class definition.
		/// </summary>
		/// <value>The class definition.</value>
		public TypeDefinition ClassDefinition
		{
			get
			{
				return this.classDefinition;
			}
		}

		public string TypeFullName
		{
			get
			{
				return Class.GetTypeFullName (this.classDefinition);
			}
		}

		/// <summary>
		/// Adds the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		public void Add (Method method)
		{
			this.methods.Add (method);

			this.methodsDictionary [method.MethodFullName] = method;

			foreach (CustomAttribute customAttribute in method.MethodDefinition.CustomAttributes) {
				if (!customAttribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.LabelAttribute).ToString ()))
					continue;

				string name = customAttribute.ConstructorParameters [0].ToString ();

				this.methodsDictionary [name] = method;
			}
		}

		private List<Method> methods = new List<Method> ();
		private Dictionary<string, Method> methodsDictionary = new Dictionary<string, Method> ();

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

		/// <summary>
		/// Gets the type of the field.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public InternalType GetFieldType (string value)
		{
			foreach (FieldDefinition field in this.classDefinition.Fields) {
				if (field.Name.Equals (value))
					return this.engine.GetInternalType (field.FieldType.FullName);
			}

			return InternalType.NotSet;
		}

		/// <summary>
		/// Gets the field offset.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public int GetFieldOffset (string fieldName)
		{
			//this.engine.GetBaseTypeSize (this.classDefinition.BaseType as TypeDefinition);

			int result = this.BaseSize;

			foreach (FieldReference field in this.classDefinition.Fields) {
				if ((field as FieldDefinition).IsStatic)
					continue;

				if (field.Name.Equals (fieldName)) {
					// An ExplicitLayout has already the offset defined
					if (this.classDefinition.IsValueType
							&& (this.classDefinition.Attributes & TypeAttributes.ExplicitLayout) != 0)
						result = (int) (field as FieldDefinition).Offset;

					break;
				}

				result += this.engine.GetFieldSize (field.FieldType.FullName);
			}

			return result;
		}

		internal static string GetTypeFullName (MemberReference type)
		{
			if (type is TypeDefinition) {
				TypeDefinition typeDefinition = type as TypeDefinition;

				foreach (CustomAttribute attribute in typeDefinition.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.TargetNamespaceAttribute).ToString ()))
						continue;
					
					return attribute.ConstructorParameters [0].ToString () + "." + type.Name;
				}
			} else if (type is FieldReference) {
				FieldReference typeDefinition = type as FieldReference;

				foreach (CustomAttribute attribute in typeDefinition.DeclaringType.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.TargetNamespaceAttribute).ToString ()))
						continue;
					
					return attribute.ConstructorParameters [0].ToString () + "." + type.DeclaringType.Name;
				}

				return typeDefinition.DeclaringType.FullName;
			}

			return type.ToString ();
		}

		InternalType internalType = InternalType.NotSet;

		/// <summary>
		/// Gets the type of the internal type.
		/// </summary>
		/// <value>The type of the internal.</value>
		public InternalType InternalType
		{
			get
			{
				return internalType;
			}
		}

		private bool isInternal = false;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is internal.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is internal; otherwise, <c>false</c>.
		/// </value>
		public bool IsInternal
		{
			get
			{
				return this.isInternal;
			}
			set
			{
				this.isInternal = value;
			}
		}

		/// <summary>
		/// Gets the size of the base.
		/// </summary>
		/// <value>The size of the base.</value>
		public int BaseSize
		{
			get
			{
				if (!this.classDefinition.IsValueType
						&& this.classDefinition.IsClass
						&& this._base != null)
					return this._base.Size;

				return 0;
			}
		}

		/// <summary>
		/// Gets the size.
		/// </summary>
		/// <returns></returns>
		public int Size
		{
			get
			{
				int result = 0;

				if (this.classDefinition.IsEnum) {
					foreach (FieldDefinition field in this.classDefinition.Fields) {
						if ((field.Attributes & FieldAttributes.RTSpecialName) != 0) {
							result = this.engine.GetTypeSize (field.FieldType.FullName);
							break;
						}
					}

				} else if (this.classDefinition.IsValueType
						|| this.classDefinition.IsClass) {
					result = this.BaseSize;

					if ((this.classDefinition.Attributes & TypeAttributes.ExplicitLayout) != 0) {
						foreach (FieldDefinition field in this.classDefinition.Fields) {
							if ((field as FieldDefinition).IsStatic)
								continue;

							int value = (int) (field.Offset + this.engine.GetTypeSize (field.FieldType.FullName));

							if (value > result)
								result = value;
						}

					} else {
						foreach (FieldReference field in this.classDefinition.Fields) {
							if ((field as FieldDefinition).IsStatic)
								continue;

							result += this.engine.GetFieldSize (field.FieldType.FullName);
						}
					} 

				} else
					throw new NotImplementedEngineException ();

				return result;
			}
		}
	}
}
