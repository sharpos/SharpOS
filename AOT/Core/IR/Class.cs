//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Stanislaw Pitucha <viraptor@gmail.com>
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
	/// <summary>
	///
	/// </summary>
	public class Class {
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

		public Class (Engine engine, TypeReference classDefinition, GenericInstanceType genericInstanceType)
		{
			this.engine = engine;
			this.classDefinition = classDefinition;
			this.genericInstanceType = genericInstanceType;
		}

		private int setupStep = -1;

		/// <summary>
		/// Setups this instance.
		/// </summary>
		public void Setup ()
		{
			Setup (0);
			Setup (1);
		}

		/// <summary>
		/// Setups this instance.
		/// </summary>
		public void Setup (int step)
		{
			if (setupStep >= step)
				return;

			setupStep = step;

			if (this.classDefinition is TypeDefinition) {
				TypeDefinition typeDefinition = this.classDefinition as TypeDefinition;

				if (step == 0) {
					if (typeDefinition.GenericParameters != null
							&& typeDefinition.GenericParameters.Count > 0
							&& this.genericInstanceType == null)
						this.isGenericType = true;

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

						this.internalType = this.engine.GetInternalType (this.TypeFullName);

						if (this.internalType == InternalType.NotSet)
							this.internalType = Operands.InternalType.ValueType;

					} else if (typeDefinition.IsClass) {
						this.isClass = true;

						this.internalType = Operands.InternalType.O;

					} else if (typeDefinition.IsInterface) {
						this.isInterface = true;

						this.internalType = Operands.InternalType.O;
					}

					if (this.TypeFullName != Mono.Cecil.Constants.Object 
							&& !this.isInterface
							&& !this.isGenericType) {

						GenericInstanceType genericBaseType = typeDefinition.BaseType as GenericInstanceType;

						if (this.genericInstanceType != null
								&& genericBaseType != null
								&& genericBaseType.GenericArguments [0] is GenericParameter) {
							GenericInstanceType _genericInstanceType = new GenericInstanceType (genericBaseType.ElementType);

							for (int i = 0; i < genericBaseType.GenericArguments.Count; i++)
								_genericInstanceType.GenericArguments.Add (this.genericInstanceType.GenericArguments [i]);

							this._base = this.GetClass (_genericInstanceType);
						} else
							this._base = this.GetClass (typeDefinition.BaseType);
					}

					// initialize base class before marking virtual methods
					if (this._base != null)
						this._base.Setup (step);

					// initialize base interfaces before marking interface methods
					foreach (TypeReference interfaceRef in (this.ClassDefinition as TypeDefinition).Interfaces) {
						this.GetClass(interfaceRef).Setup (step);
					}

					if (this.IsInterface)
						this.AssignInterfaceMethodNumbers();
					else {
						this.MarkInterfaceMethods ();
						if (this._base != null && this._base.ImplementsInterfaces)
							this.MergeBaseInterfaceMethods ();
					}

					this.AddVirtualMethods (this.virtualMethods);

				} else if (step == 1) {
					if (!this.IsGenericType && (!this.isInternal || (this.isInternal && !this.engine.Assembly.IgnoreTypeContent (this.TypeFullName)))) {
						foreach (FieldDefinition field in typeDefinition.Fields) {
							Class _class = this.GetClass (field.FieldType);
							InternalType _internalType = this.engine.GetInternalType (_class.TypeFullName);

							Field _field = new Field (field, _class, this, _internalType);

							fields.Add (_field);
							fieldsDictionary [field.Name] = _field;
						}
					}
				} else
					throw new NotImplementedEngineException ("Getting size of " + this);

			} else if (this.classDefinition is TypeSpecification) {
				if (step == 0) {
					this.isSpecialType = true;
					this.internalType = this.engine.GetInternalType (this.TypeFullName);

					if (this.classDefinition is ArrayType) {
						this._base = this.engine.ArrayClass;
						this.specialTypeElement = this.engine.GetClass ((this.classDefinition as ArrayType).ElementType);

					} else
						this._base = this.engine.GetClass (this.classDefinition.GetOriginalType ());

					// initialize base class
					if (this._base != null)
						this._base.Setup (step);

				} else if (step != 1)
					throw new NotImplementedEngineException ("Can't initialize " + this.TypeFullName + " to step " + step);

			} else
				throw new NotImplementedEngineException (this.TypeFullName + " not supported yet.");
		}

		public void AddMethods ()
		{
			AddMethods (false);
		}

		public void AddMethods (bool isAOTCore)
		{
			bool constructorsSkipProcessing = isAOTCore && this.engine.Assembly.IsMemoryAddress (this.classDefinition.FullName);

			TypeDefinition type = this.classDefinition as TypeDefinition;

			if (type == null)
				return;

			foreach (MethodDefinition entry in type.Constructors) {
				Method method = new Method (this.engine, this, entry);

				method.SkipProcessing = constructorsSkipProcessing;

				this.Add (method);
			}

			// We don't need the methods of the registers or memory addresses
			if (isAOTCore && !this.engine.Assembly.IsInstruction (this.classDefinition.FullName))
				return;

			foreach (MethodDefinition entry in type.Methods) {
				if (entry.ImplAttributes != MethodImplAttributes.Managed) {
					this.engine.Dump.IgnoreMember (entry.Name,
							"Method is unmanaged");

					continue;
				}

				Method method = new Method (this.engine, this, entry);

				this.Add (method);
			}
		}

		public void SetupMethods ()
		{
			if (this.IsGenericType)
				return;

			if (this.IsSpecialType)
				return;

			for (int j = 0; j < this.methods.Count; j++)
				this.methods [j].Setup ();
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
				return this.specialTypeElement;
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

		private bool isGenericType = false;

		/// <summary>
		/// Gets a value indicating whether this instance is generic type.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is generic type; otherwise, <c>false</c>.
		/// </value>
		public bool IsGenericType
		{
			get
			{
				return isGenericType;
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

		/// <summary>
		/// Gets a value indicating whether this instance is array.
		/// </summary>
		/// <value><c>true</c> if this instance is array; otherwise, <c>false</c>.</value>
		public bool IsArray
		{
			get
			{
				return this.isSpecialType && this._base.TypeFullName.Equals (Engine.SYSTEM_ARRAY);
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

		private bool isInterface = false;

		/// <summary>
		/// Gets a value indicating whether this instance is interface.
		/// </summary>
		/// <value><c>true</c> if this instance is interface; otherwise, <c>false</c>.</value>
		public bool IsInterface
		{
			get
			{
				return isInterface;
			}
		}

		private bool hasExplicitLayout = false;

		/// <summary>
		/// Gets a value indicating whether this instance has explicit layout.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has explicit layout; otherwise, <c>false</c>.
		/// </value>
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

		Dictionary<string, Field> fieldsDictionary = new Dictionary<string, Field> ();
		List<Field> fields = new List<Field> ();

		/// <summary>
		/// Gets the fields.
		/// </summary>
		/// <value>The fields.</value>
		public List<Field> Fields
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
			if (!fieldsDictionary.ContainsKey (value))
				throw new EngineException (string.Format ("Field '{0}' not found.", value));

			return fieldsDictionary [value];
		}

		/// <summary>
		/// Gets the name of the method by.
		/// </summary>
		/// <param name="methodReference">The method reference.</param>
		/// <returns></returns>
		public Method GetMethodByName (MethodReference methodReference)
		{
			string value = Method.GetLabel (this, methodReference);

			if (this.methodsDictionary.ContainsKey (value))
				return this.methodsDictionary [value];

			if (this.isSpecialType) {
				Method method = new Method (this.engine, this, methodReference);
				
				method.Setup ();

				method.SkipProcessing = true;

				this.Add (method);

				return method;
			}

			if (methodReference is GenericInstanceMethod) {
				GenericInstanceMethod genericInstanceMethod = methodReference as GenericInstanceMethod;

				methodReference = this.GetGenericMethod (genericInstanceMethod);

                /// FYI (Adam Stevenson)
                /// This constructor is being looked at for refactoring, as their are two information streams coming in here that are possibly 
                /// overlapping in information content.
				Method method = new Method (this.engine, this, methodReference, genericInstanceMethod);

				method.Setup ();

				method.Process ();

				this.Add (method);

				return method;
			}

			throw new EngineException (string.Format ("Method '{0}' not found.", value));
		}

		private MethodReference GetGenericMethod (GenericInstanceMethod genericInstanceMethod)
		{
			string value = Method.GetLabel (this, genericInstanceMethod);

			if (this.methodsDictionary.ContainsKey (value))
				return this.methodsDictionary [value].MethodDefinition;

			foreach (Method method in this.methods) {
				if (!method.IsGenericType)
					continue;

				if (method.Name != genericInstanceMethod.Name)
					continue;

				if (method.MethodDefinition.Parameters.Count != genericInstanceMethod.Parameters.Count)
					continue;

				bool ok = true;

				for (int i = 0; i < method.MethodDefinition.Parameters.Count; i++) {
					if (method.MethodDefinition.Parameters [i].ParameterType != genericInstanceMethod.Parameters [i].ParameterType) {
						if (!(method.MethodDefinition.Parameters [i].ParameterType is GenericParameter)
								|| !(genericInstanceMethod.Parameters [i].ParameterType is GenericParameter)) {
							ok = false;
							break;
						}
					}
				}

				if (ok) {
					this.methodsDictionary [value] = method;

					return method.MethodDefinition;
				}
			}

			throw new EngineException (string.Format ("Method '{0}' not found.", genericInstanceMethod.ToString ()));
		}

		internal Class GetClass (TypeReference type)
		{
			if (type is GenericParameter
					&& this.genericInstanceType != null) {
				GenericParameter genericParameter = type as GenericParameter;

				int i = 0;
				for (; i < genericParameter.Owner.GenericParameters.Count; i++) {
					if (genericParameter.Owner.GenericParameters [i].FullName == genericParameter.FullName)
						break;
				}

				if (i >= genericParameter.Owner.GenericParameters.Count)
					throw new EngineException (string.Format ("Type '{0}' was not found in the method '{1}'.", type.ToString (), this.TypeFullName));

				type = this.genericInstanceType.GenericArguments [i];

			} else if (type is GenericInstanceType
					&& (type as GenericInstanceType).GenericArguments [0] is GenericParameter) {
				GenericInstanceType _genericInstanceType = new GenericInstanceType ((type as GenericInstanceType).ElementType);

				for (int i = 0; i < (type as GenericInstanceType).GenericArguments.Count; i++)
					_genericInstanceType.GenericArguments.Add (this.genericInstanceType.GenericArguments [i]);

				return this.GetClass (_genericInstanceType);
			}

			return this.engine.GetClass (type);		
		}

		internal Field GetField (FieldReference fieldReference)
		{
			if (fieldReference.DeclaringType is GenericInstanceType
					&& (fieldReference.DeclaringType as TypeSpecification).ElementType == this.classDefinition) {
				return this.GetFieldByName (fieldReference.Name);
			}

			return this.engine.GetField (fieldReference);
		}

		private Engine engine = null;

		/// <summary>
		/// Gets the engine.
		/// </summary>
		/// <value>The engine.</value>
		public Engine Engine {
			get {
				return engine;
			}
		}

		private GenericInstanceType genericInstanceType = null;

		private TypeReference classDefinition = null;

		/// <summary>
		/// Gets the class definition.
		/// </summary>
		/// <value>The class definition.</value>
		public TypeReference ClassDefinition {
			get {
				return this.classDefinition;
			}
		}

		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		/// <value>The full name of the type.</value>
		public string TypeFullName {
			get {
				if (this.genericInstanceType != null)
					return Class.GetTypeFullName (this.genericInstanceType);

				return Class.GetTypeFullName (this.classDefinition);
			}
		}

		/// <summary>
		/// Gets the name of the type.
		/// </summary>
		/// <value>The name of the type.</value>
		public string TypeName {
			get {
				if (this.genericInstanceType != null)
					return this.genericInstanceType.Name;

				return this.classDefinition.Name;
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

			foreach (string label in method.Labels)
				this.methodsDictionary [label] = method;
		}

		private List<Method> methods = new List<Method> ();
		private Dictionary<string, Method> methodsDictionary = new Dictionary<string, Method> ();

		public List<Method> Methods {
			get {
				return this.methods;
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
			if (this.genericInstanceType != null)
				return this.genericInstanceType.FullName;

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
			foreach (Field field in this.fields) {
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
			foreach (Field field in this.fields) {
				if (field.IsStatic)
					continue;

				if (field.Name.Equals (fieldName)) {
					// An ExplicitLayout has already the offset defined
					if (this.IsValueType && this.hasExplicitLayout)
						result = (int) field.Offset;

					break;
				}

				result += this.engine.GetFieldSize (field);
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

			} else if (type is TypeSpecification) {
				TypeSpecification typeSpecification = type as TypeSpecification;

				foreach (CustomAttribute attribute in typeSpecification.ElementType.CustomAttributes) {
					if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.TargetNamespaceAttribute).ToString ()))
						continue;

					return attribute.ConstructorParameters [0].ToString () + "." + typeSpecification.Name;
				}
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
		/// <value>The size.</value>
		public int Size
		{
			get
			{
				return this.InternalSize;
			}
		}

		/// <summary>
		/// Gets the size of boxed valuetype or standard object size.
		/// </summary>
		/// <value>The size.</value>
		public int ObjectSize
		{
			get
			{
				if (IsValueType)
					return this.Size + engine.ObjectSize;
				else
					return this.Size;
			}
		}

		private int size = -1;

		/// <summary>
		/// Gets the size of the reference.
		/// </summary>
		/// <value>The size of the reference.</value>
		public int ReferenceSize
		{
			get
			{
				if (this.internalType == InternalType.O
						|| this.internalType == InternalType.I
						|| this.internalType == InternalType.U
						|| this.internalType == InternalType.M
						|| this.internalType == InternalType.SZArray
						|| this.internalType == InternalType.Array)
					return this.engine.Assembly.IntSize;

				return this.Size;
			}
		}

		/// <summary>
		/// Gets the size.
		/// </summary>
		/// <returns></returns>
		private int InternalSize
		{
			get
			{
				if (this.size != -1)
					return this.size;

				int result = 0;

				if (this.IsEnum) {
					foreach (Field field in this.fields) {
						if ((field.FieldDefinition.Attributes & FieldAttributes.RTSpecialName) != 0) {
							result = this.engine.GetTypeSize (field.FieldDefinition.FieldType.FullName);
							break;
						}
					}

				} else if (this.IsValueType
						|| this.IsClass
						|| this.IsInterface) {
					result = this.BaseSize;

					if (this.hasExplicitLayout) {
						foreach (Field field in this.fields) {
							if (field.IsStatic)
								continue;

							int value = (int) (field.Offset + this.engine.GetTypeSize (field.FieldDefinition.FieldType.FullName));

							if (value > result)
								result = value;
						}

					} else {
						foreach (Field field in this.fields) {
							if (field.IsStatic)
								continue;

							result += this.engine.GetFieldSize (field);
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
					throw new NotImplementedEngineException ("Can't retrieve InternalSize for type " + this.TypeFullName);

				this.size = result;

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
				if (method.IsNewSlot
						|| method.IsAbstract) {
					method.VirtualSlot = list.Count;
					list.Add (method);

				} else if (method.IsVirtual) {
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

		public bool ImplementsInterfaces
		{
			get {
				return (!IsInterface && interfaceMethodsEntries != null);
			}
		}

		public List<Method> GetInterfaceEntries (int key) {
			if (interfaceMethodsEntries == null)
				return null;
			return interfaceMethodsEntries[key];
		}

		private List<Method>[] interfaceMethodsEntries = null;

		private void AddMethodToIMT (Method method) {
			if (method.InterfaceMethodNumber == -1)
				throw new EngineException("Can't add "+method+" to IMT - Method number == -1");
			if (interfaceMethodsEntries == null)
				interfaceMethodsEntries = new List<Method>[Method.IMTSize];

			int key = method.InterfaceMethodKey;

			if (interfaceMethodsEntries [key] == null)
				interfaceMethodsEntries [key] = new List<Method> ();

			interfaceMethodsEntries[key].Add (method);
		}

		/// <summary>
		/// Marks all methods with correct IMT numbers if they are interface implementation
		/// </summary>
		private void MarkInterfaceMethods ()
		{
			foreach (Method method in this.methods) {
				if (!method.IsNewSlot)
					continue;
				
				// was implementation explicit?
				MethodDefinition methodDef = method.MethodDefinition as MethodDefinition;
				if (methodDef.Overrides.Count > 0) {
					foreach (MethodReference origReference in methodDef.Overrides) {
						Method orig = engine.GetClass (origReference.DeclaringType).GetMethodByName (origReference);
						method.InterfaceMethodNumber = orig.InterfaceMethodNumber;
						AddMethodToIMT(method);
					}
				} else { // was implementation implicit?
					foreach (TypeReference iface in (this.ClassDefinition as TypeDefinition).Interfaces) {
						Class ifaceClass = engine.GetClass (iface);
						Method orig = ifaceClass.methods.Find (delegate (Method m) { return m.ID==method.ID; });
						if (orig!=null) {
							method.InterfaceMethodNumber = orig.InterfaceMethodNumber;
							AddMethodToIMT(method);
							break;
						}
					}
				}
				// or it's not interface method
			}
		}

		/// <summary>
		/// Merges all not implemented interface methods from base classes
		/// </summary>
		private void MergeBaseInterfaceMethods ()
		{
			for (int i = 0; i < Method.IMTSize; i++) {
				if (_base.interfaceMethodsEntries[i] == null)
					continue;
				
				foreach (Method baseMethod in _base.interfaceMethodsEntries[i]) {
					bool hasLocalImpl = false;
				
					// lookup matching method from current class
					if (interfaceMethodsEntries != null && interfaceMethodsEntries[i] != null)
						hasLocalImpl = interfaceMethodsEntries[i].Exists (delegate (Method method) {
							return method.InterfaceMethodNumber == baseMethod.InterfaceMethodNumber;
						});

					if (!hasLocalImpl)
						AddMethodToIMT (baseMethod);
				}
			}
		}

		/// <summary>
		/// Assigns all methods a number for IMT
		/// </summary>
		/// <returns></returns>
		private void AssignInterfaceMethodNumbers ()
		{
			if (!this.IsInterface)
				return;

			methods.ForEach(delegate (Method _method) {
				_method.AssignInterfaceMethodNumber();
			});
		}
	}
}
