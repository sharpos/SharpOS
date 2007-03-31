/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 */

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


namespace SharpOS.AOT.IR
{
    public partial class Engine : IEnumerable<Class>
    {
        public Engine()
		{
		}

        private IAssembly asm = null;

        public IAssembly Assembly
        {
            get { return asm; }
        }
	
        public void Run(IAssembly asm, string assembly, string target)
        {
            this.asm = asm;

            AssemblyDefinition library = AssemblyFactory.GetAssembly(assembly);
         
            foreach (TypeDefinition type in library.MainModule.Types)
            {
                Console.WriteLine(type.Name);

                if (type.Name.Equals("<Module>") == true)
                {
                    continue;
                }

                Console.WriteLine(type.FullName);

                Class _class = new Class(this, type);
                this.classes.Add(_class);

                foreach (MethodDefinition entry in type.Constructors)
                {
                    if (entry.Name.Equals(".cctor") == false)
                    {
                        continue;
                    }

                    Method method = new Method(this, entry);

                    method.Process();

                    _class.Add(method);

                    break;
                }

                foreach (MethodDefinition entry in type.Methods)
                {
                    if (entry.IsStatic == false || entry.ImplAttributes != MethodImplAttributes.Managed)
                    {
                        Console.WriteLine("Not processing '" + entry.DeclaringType.FullName + "." + entry.Name + "'");

                        continue;
                    }

                    Method method = new Method(this, entry);

                    method.Process();

                    _class.Add(method);
                }
            }

            asm.Encode(this, target);

            return;
        }

        private List<Class> classes = new List<Class>();

        IEnumerator<Class> IEnumerable<Class>.GetEnumerator()
        {
            foreach (Class _class in this.classes)
            {
                yield return _class;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Class>)this).GetEnumerator();
        }
    }
}
