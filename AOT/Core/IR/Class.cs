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
		public Class (Engine engine, TypeReference classDefinition)
		{
			this.engine = engine;
			this.classDefinition = classDefinition;
		}

		public void Setup ()
		{
			if (this.classDefinition is TypeDefinition) {
				TypeDefinition typeDefinition = this.classDefinition as TypeDefinition;

				this.hasExplicitLayout = (typeDefinition.Attributes & TypeAttributes.ExplicitLayout) != 0;

				if (typeDefinition.IsEnum) {
					this.isEnum = true;

					foreach (FieldDefinition field in typeDefinition.Fields) {
						if ((field.Attributes & FieldAttributes.RTSpecialName) != 0) {
							this.internalType = this.engine.GetInternalType (field.FieldType.FullName);
							break;
						}
					}

				} else if (typeDefinition.IsValueType) {
					this.isValueType = true;

					this.internalType = Operands.InternalType.ValueType;

				} else if (typeDefinition.IsClass) {
					this.isClass = true;

					this.internalType = Operands.InternalType.O;
				}

				foreach (FieldDefinition field in typeDefinition.Fields)
					fields [field.Name] = new Field (field);

				if (this.TypeFullName != Mono.Cecil.Constants.Object)
					this._base = this.engine.GetClass (typeDefinition.BaseType);

				this.AddVirtualMethods (this.virtualMethods);

			} else if (this.classDefinition is TypeSpecification) {
				this.isSpecialType = true;

				if (this.classDefinition is ArrayType) {
					this._base = this.engine.ArrayClass;
					this.specialTypeElement = this.engine.GetClass (this.classDefinition.GetOriginalType ());

				} else
					this._base = this.engine.GetClass (this.classDefinition.GetOriginalType ());

			} else
				throw new NotImplementedEngineException ();
		}


		private Class specialTypeElement = null;

		/// <summary>
		/// Gets the special type element.
		/// </summary>
		/// <value>The special type element.</value>
		public Class SpecialTypeElement
		{
			get
			{
				return specialTypeElement;
			}
		}

		private bool isSpecialType = false;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is special type.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is special type; otherwise, <c>false</c>.
		/// </value>
		public bool IsSpecialType
		{
			get
			{
				return this.isSpecialType;
			}
			set
			{
				this.isSpecialType = value;
			}
		}

		private bool isValueType = false;

		/// <summary>
		/// Gets a value indicating whether this instance is value type.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is value type; otherwise, <c>false</c>.
		/// </value>
		public bool IsValueType
		{
			get
			{
				return isValueType;
			}
		}

		private bool isClass = false;

		/// <summary>
		/// Gets a value indicating whether this instance is class.
		/// </summary>
		/// <value><c>true</c> if this instance is class; otherwise, <c>false</c>.</value>
		public bool IsClass
		{
			get
			{
				return isClass;
			}
		}

		private bool isEnum = false;

		/// <summary>
		/// Gets a value indicating whether this instance is enum.
		/// </summary>
		/// <value><c>true</c> if this instance is enum; otherwise, <c>false</c>.</value>
		public bool IsEnum
		{
			get
			{
				return isEnum;
			}
		}

		private bool hasExplicitLayout = false;

		public bool HasExplicitLayout
		{
			get
			{
				return hasExplicitLayout;
			}
		}

		Class _base = null;

		/// <summary>
		/// Gets the base.
		/// </summary>
		/// <value>The base.</value>
		public Class Base
		{
			get
			{
				return this._base;
			}
		}

		Dictionary<string, Field> fields = new Dictionary<string, Field> ();

		/// <summary>
		/// Gets the fields.
		/// </summary>
		/// <value>The fields.</value>
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

		/// <summary>
		/// Gets the engine.
		/// </summary>
		/// <value>The engine.</value>
		public Engine Engine
		{
			get
			{
				return engine;
			}
		}

		private TypeReference classDefinition = null;

		/// <summary>
		/// Gets the class definition.
		/// </summary>
		/// <value>The class definition.</value>
		public TypeReference ClassDefinition
		{
			get
			{
				return this.classDefinition;
			}
		}

		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		/// <value>The full name of the type.</value>
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
			// TODO Refactoring
			foreach (Field field in this.fields.Values) {
				if (field.Name.Equals (value))
					return this.engine.GetInternalType (field.FieldDefinition.FieldType.FullName);
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
			int result = this.BaseSize;

			// TODO Refactoring
			foreach (Field field in this.fields.Values) {
				if (field.IsStatic)
					continue;

				if (field.Name.Equals (fieldName)) {
					// An ExplicitLayout has already the offset defined
					if (this.IsValueType && this.hasExplicitLayout)
						result = (int) field.Offset;

					break;
				}

				result += this.engine.GetFieldSize (field.FieldDefinition.FieldType.FullName);
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
				if (!this.IsValueType
						&& this.IsClass
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

				if (this.IsEnum) {
					foreach (Field field in this.fields.Values) {
						if ((field.FieldDefinition.Attributes & FieldAttributes.RTSpecialName) != 0) {
							result = this.engine.GetTypeSize (field.FieldDefinition.FieldType.FullName);
							break;
						}
					}

				} else if (this.IsValueType
						|| this.IsClass) {
					result = this.BaseSize;

					if (this.hasExplicitLayout) {
						foreach (Field field in this.fields.Values) {
							if (field.IsStatic)
								continue;

							int value = (int) (field.Offset + this.engine.GetTypeSize (field.FieldDefinition.FieldType.FullName));

							if (value > result)
								result = value;
						}

					} else {
						foreach (Field field in this.fields.Values) {
							if (field.IsStatic)
								continue;

							result += this.engine.GetFieldSize (field.FieldDefinition.FieldType.FullName);
						}
					}

				} else if (this.classDefinition is PointerType)
					result = this.engine.Assembly.IntSize;

				else if (this.classDefinition is ReferenceType)
					result = this.engine.Assembly.IntSize;

				else if (this.classDefinition is PinnedType)
					result = this.engine.Assembly.IntSize;

				else if (this.classDefinition is ArrayType)
					result = this.engine.ArrayClass.Size;

				else
					throw new NotImplementedEngineException ();

				return result;
			}
		}

		private List<Method> virtualMethods = new List<Method> ();

		/// <summary>
		/// Gets the virtual methods.
		/// </summary>
		/// <value>The virtual methods.</value>
		public List<Method> VirtualMethods
		{
			get
			{
				return virtualMethods;
			}
		}

		private void AddVirtualMethods (List<Method> list)
		{
			if (this._base != null)
				this._base.AddVirtualMethods (list);

			foreach (Method method in this.methods) {
				if (method.MethodDefinition.IsNewSlot) {
					method.VirtualSlot = list.Count;
					list.Add (method);

				} else if (method.MethodDefinition.IsVirtual) {
					for (int i = 0; i < list.Count; i++) {
						if (list [i].ID == method.ID) {
							method.VirtualSlot = i;
							list [i] = method;
							break;
						}
					}
				}
			}
		}
	}
}
