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
    public class Class : IEnumerable<Method>
    {
        public Class(Engine engine, TypeDefinition classDefinition)
        {
            this.engine = engine;
            this.classDefinition = classDefinition;
        }

        private Engine engine = null;
        private TypeDefinition classDefinition = null;

        public TypeDefinition ClassDefinition
        {
            get { return this.classDefinition; }
        }

        public void Add(Method method)
        {
            this.methods.Add(method);
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
