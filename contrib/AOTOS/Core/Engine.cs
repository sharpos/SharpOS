/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
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
    public partial class Engine : IEnumerable<Method>
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

                foreach (MethodDefinition entry in type.Methods)
                {
                    Method method = new Method(this, entry);

                    method.Process();

                    this.methods.Add(method);
                }
            }

            asm.Encode(this, target);

            return;
        }

        private List<Method> methods = new List<Method>();

        IEnumerator<Method> IEnumerable<Method>.GetEnumerator()
        {
            foreach (Method method in this.methods)
            {
                yield return method;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Method>)this).GetEnumerator();
        }
    }
}
