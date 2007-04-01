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
using System.Collections.ObjectModel;
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
    public class Method : IEnumerable<Block>
    {
        public Method(Engine engine, MethodDefinition methodDefinition)
        {
            this.engine = engine;
            this.methodDefinition = methodDefinition;
        }

        private Engine engine = null;
        private MethodDefinition methodDefinition = null;

        public MethodDefinition MethodDefinition
        {
            get { return this.methodDefinition; }
        }

        public string Dump()
        {
            return Dump(blocks);
        }

        private int stackSize = 0;

        public int StackSize
        {
            get { return stackSize; }
            set { stackSize = value; }
        }


        public Argument GetArgument(int i)
        {
            Argument argument = new Argument(i);

            argument.SetSizeType(this.methodDefinition.Parameters[i - 1].ParameterType.FullName);

            return argument;
        }

        public Local GetLocal(int i)
        {
            Local local = new Local(i);

            local.SetSizeType(this.methodDefinition.Body.Variables[i].VariableType.FullName);

            return local;
        }

        public string Dump(List<Block> list)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("===============================\n");
            stringBuilder.Append("->" + this.methodDefinition.Name + "\n");
            stringBuilder.Append("===============================\n");

            foreach (Block block in list)
            {
                block.Dump(string.Empty, stringBuilder);
            }

            return stringBuilder.ToString();
        }

        public bool IsBranch(Mono.Cecil.Cil.Instruction instruction, bool all)
        {
            if (all == true && instruction.OpCode == OpCodes.Ret)
            {
                return true;
            }

            if (instruction.OpCode == OpCodes.Br
                || instruction.OpCode == OpCodes.Br_S
                || instruction.OpCode == OpCodes.Brfalse
                || instruction.OpCode == OpCodes.Brfalse_S
                || instruction.OpCode == OpCodes.Brtrue
                || instruction.OpCode == OpCodes.Brtrue_S
                || instruction.OpCode == OpCodes.Beq
                || instruction.OpCode == OpCodes.Beq_S
                || instruction.OpCode == OpCodes.Bge
                || instruction.OpCode == OpCodes.Bge_S
                || instruction.OpCode == OpCodes.Bge_Un
                || instruction.OpCode == OpCodes.Bge_Un_S
                || instruction.OpCode == OpCodes.Bgt
                || instruction.OpCode == OpCodes.Bgt_S
                || instruction.OpCode == OpCodes.Bgt_Un
                || instruction.OpCode == OpCodes.Bgt_Un_S
                || instruction.OpCode == OpCodes.Ble
                || instruction.OpCode == OpCodes.Ble_S
                || instruction.OpCode == OpCodes.Ble_Un
                || instruction.OpCode == OpCodes.Ble_Un_S
                || instruction.OpCode == OpCodes.Blt
                || instruction.OpCode == OpCodes.Blt_S
                || instruction.OpCode == OpCodes.Blt_Un
                || instruction.OpCode == OpCodes.Blt_Un_S
                || instruction.OpCode == OpCodes.Bne_Un
                || instruction.OpCode == OpCodes.Bne_Un_S
                || instruction.OpCode == OpCodes.Switch
                || instruction.OpCode == OpCodes.Leave
                || instruction.OpCode == OpCodes.Leave_S
                || instruction.OpCode == OpCodes.Endfinally
                || instruction.OpCode == OpCodes.Throw
                || instruction.OpCode == OpCodes.Rethrow)
            {
                return true;
            }

            return false;
        }

        private void BuildBlocks()
        {
            blocks = new List<Block>();

            Block currentBlock = new Block(this);
            blocks.Add(currentBlock);

            // 1st Step: Split the code in blocks that branch at the end
            for (int i = 0; i < this.methodDefinition.Body.Instructions.Count; i++)
            {
                Mono.Cecil.Cil.Instruction instruction = this.methodDefinition.Body.Instructions[i];

                currentBlock.CIL.Add(instruction);

                if (i < this.methodDefinition.Body.Instructions.Count - 1 && IsBranch(instruction, true) == true)
                {
                    currentBlock = new Block(this);
                    blocks.Add(currentBlock);
                }
            }

            // 2nd Step: Split the blocks if their code is referenced by other branches
            bool found;

            do
            {
                found = false;

                foreach (Block source in blocks)
                {
                    if (this.IsBranch(source.CIL[source.CIL.Count - 1], false) == true
                        && (source.CIL[source.CIL.Count - 1].Operand is Mono.Cecil.Cil.Instruction
                        || source.CIL[source.CIL.Count - 1].Operand is Mono.Cecil.Cil.Instruction[]))
                    {
                        List<Mono.Cecil.Cil.Instruction> jumps = new List<Mono.Cecil.Cil.Instruction>();

                        if (source.CIL[source.CIL.Count - 1].Operand is Mono.Cecil.Cil.Instruction)
                        {
                            jumps.Add(source.CIL[source.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction);
                        }
                        else
                        {
                            jumps = new List<Mono.Cecil.Cil.Instruction>(source.CIL[source.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction[]);
                        }

                        foreach (Mono.Cecil.Cil.Instruction jump in jumps)
                        {
                            if (jump == source.CIL[source.CIL.Count - 1])
                            {
                                continue;
                            }

                            for (int destinationIndex = 0; destinationIndex < blocks.Count; destinationIndex++)
                            {
                                Block destination = blocks[destinationIndex];
                                Block newBlock = new Block(this);

                                for (int i = 0; i < destination.CIL.Count; i++)
                                {
                                    Mono.Cecil.Cil.Instruction instruction = destination.CIL[i];

                                    if (instruction == jump)
                                    {
                                        if (i == 0)
                                        {
                                            break;
                                        }

                                        found = true;
                                    }

                                    if (found == true)
                                    {
                                        newBlock.CIL.Add(destination.CIL[i]);
                                    }
                                }

                                if (found == true)
                                {
                                    for (int i = 0; i < newBlock.CIL.Count; i++)
                                    {
                                        destination.CIL.Remove(newBlock.CIL[i]);
                                    }

                                    blocks.Insert(destinationIndex + 1, newBlock);

                                    break;
                                }
                            }

                            if (found == true)
                            {
                                break;
                            }
                        }
                    }

                    if (found == true)
                    {
                        break;
                    }
                }
            }
            while (found == true);

            // 3rd step: split the try blocks in case they got mixed up with some other code
            do
            {
                found = false;

                foreach (ExceptionHandler exceptionHandler in this.methodDefinition.Body.ExceptionHandlers)
                {
                    for (int i = 0; i < this.blocks.Count; i++)
                    {
                        Block block = this.blocks[i];

                        if (exceptionHandler.TryStart.Offset > block.StartOffset
                            && exceptionHandler.TryStart.Offset <= block.EndOffset)
                        {
                            Block newBlock = new Block(this);

                            for (int j = 0; j < block.CIL.Count; j++)
                            {
                                Mono.Cecil.Cil.Instruction instruction = block.CIL[j];

                                if (instruction == exceptionHandler.TryStart)
                                {
                                    found = true;
                                }

                                if (found == true)
                                {
                                    newBlock.CIL.Add(block.CIL[j]);
                                }
                            }

                            for (int j = 0; j < newBlock.CIL.Count; j++)
                            {
                                block.CIL.Remove(newBlock.CIL[j]);
                            }

                            blocks.Insert(i + 1, newBlock);

                            break;
                        }

                        if (block.StartOffset > exceptionHandler.TryStart.Offset)
                        {
                            break;
                        }
                    }

                    if (found == true)
                    {
                        break;
                    }
                }

            }
            while (found == true);

            return;
        }

        private void FillOuts(Block destination, Mono.Cecil.Cil.Instruction[] instructions)
        {
            foreach (Mono.Cecil.Cil.Instruction instruction in instructions)
            {
                bool found = false;

                foreach (Block block in blocks)
                {
                    if (block.CIL[0] == instruction)
                    {
                        found = true;

                        destination.Outs.Add(block);

                        break;
                    }
                }

                if (found == false)
                {
                    throw new Exception("Could not find the block for the instruction at offset '" + instruction.Offset + "'.");
                }
            }

            return;
        }

        private void ClassifyAndLinkBlocks()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                Block block = blocks[i];

                if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Ret)
                {
                    block.Type = Block.BlockType.Return;
                }
                else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Switch)
                {
                    block.Type = Block.BlockType.NWay;

                    this.FillOuts(block, block.CIL[block.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction[]);
                }
                else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Br
                    || block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Br_S)
                {
                    block.Type = Block.BlockType.OneWay;

                    this.FillOuts(block, new Mono.Cecil.Cil.Instruction[] { block.CIL[block.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction });
                }
                else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Endfinally)
                {
                    block.Type = Block.BlockType.OneWay; // Block.BlockType.Finally;
                }
                else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Throw
                   || block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Rethrow)
                {
                    block.Type = Block.BlockType.Throw;
                }
                else if (block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Leave
                    || block.CIL[block.CIL.Count - 1].OpCode == OpCodes.Leave_S)
                {
                    Mono.Cecil.Cil.Instruction lastInstruction = block.CIL[block.CIL.Count - 1];

                    this.FillOuts(block, new Mono.Cecil.Cil.Instruction[] { block.CIL[block.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction });

                    bool found = false;

                    foreach (ExceptionHandler exceptionHandler in block.Method.MethodDefinition.Body.ExceptionHandlers)
                    {
                        if (exceptionHandler.TryEnd.Previous == lastInstruction)
                        {
                            found = true;
                            block.Type = Block.BlockType.OneWay; // Block.BlockType.Try;

                            break;
                        }
                        else if (exceptionHandler.HandlerEnd.Previous == lastInstruction)
                        {
                            found = true;
                            block.Type = Block.BlockType.OneWay; // Block.BlockType.Catch;

                            break;
                        }
                    }

                    if (found == false)
                    {
                        throw new Exception("Malformated Try/Catch block in '" + block.Method.MethodDefinition.Name + "'.");
                    }
                }
                else if (this.IsBranch(block.CIL[block.CIL.Count - 1], false) == true)
                {
                    block.Type = Block.BlockType.TwoWay;

                    this.FillOuts(block, new Mono.Cecil.Cil.Instruction[] { block.CIL[block.CIL.Count - 1].Operand as Mono.Cecil.Cil.Instruction });

                    block.Outs.Add(blocks[i + 1]);
                }
                else
                {
                    block.Type = Block.BlockType.Fall;
                    block.Outs.Add(blocks[i + 1]);
                }
            }

            // Fill The Ins
            for (int i = 0; i < blocks.Count; i++)
            {
                for (int j = 0; j < blocks.Count; j++)
                {
                    if (blocks[j].Outs.Contains(blocks[i]) == true)
                    {
                        blocks[i].Ins.Add(blocks[j]);
                    }
                }
            }

            return;
        }

        public void BlocksOptimization()
        {
            bool changed;
            do
            {
                changed = false;
                for (int i = 1; i < this.blocks.Count; i++)
                {
                    if (this.blocks[i].Ins.Count == 1
                        && this.blocks[i - 1].Outs.Count == 1
                        && this.blocks[i].Ins[0] == this.blocks[i - 1]
                        && this.blocks[i - 1].Outs[0] == this.blocks[i])
                    {
                        this.blocks[i - 1].Merge(this.blocks[i]);

                        this.blocks.Remove(this.blocks[i]);

                        changed = true;
                        break;
                    }
                }
            }
            while (changed == true);

            for (int i = 0; i < this.blocks.Count; i++)
            {
                this.blocks[i].Index = i;
            }

            return;
        }

        private void ConvertFromCIL()
        {
            // TODO don't move the instructions from one block to the other

            foreach (Block block in blocks)
            {
                block.ConvertFromCIL(false);
            }

            /*List<Block> removeFirstInstruction = new List<Block>();

            foreach (Block block in blocks)
            {
                if (block.Stack > 0 && removeFirstInstruction.Contains(block) == false && block.CIL[block.CIL.Count - 1].OpCode != OpCodes.Ret)
                {
                    if (block.Outs.Count != 1)
                    {
                        throw new Exception("Could not convert '" + block.Method.MethodDefinition.Name + "' from CIL.");
                    }

                    Block childBlock = block.Outs[0];

                    while (childBlock.Type == Block.BlockType.OneWay && childBlock.CIL.Count == 1)
                    {
                        childBlock = childBlock.Outs[0];
                    }

                    if (removeFirstInstruction.Contains(childBlock) == false)
                    {
                        removeFirstInstruction.Add(childBlock);
                    }

                    Mono.Cecil.Cil.Instruction instruction = childBlock.CIL[0];

                    if (block.CIL[block.CIL.Count - 1].OpCode.FlowControl == FlowControl.Branch)
                    {
                        block.CIL.Insert(block.CIL.Count - 1, instruction);
                    }
                    else
                    {
                        block.CIL.Add(instruction);
                    }
                }
            }

            if (removeFirstInstruction.Count > 0)
            {
                foreach (Block block in removeFirstInstruction)
                {
                    block.CIL.RemoveAt(0);
                }

                foreach (Block block in blocks)
                {
                    block.ConvertFromCIL(true);
                }
            }*/

            return;
        }

        private List<Block> Preorder()
        {
            List<Block> list = new List<Block>();
            List<Block> visited = new List<Block>();

            Preorder(visited, list, this.blocks[0]);

            return list;
        }

        private void Preorder(List<Block> visited, List<Block> list, Block current)
        {
            if (visited.Contains(current) == false)
            {
                visited.Add(current);

                list.Add(current);

                for (int i = 0; i < current.Outs.Count; i++)
                {
                    Preorder(visited, list, current.Outs[i]);
                }
            }

            return;
        }

        private List<Block> Postorder()
        {
            List<Block> list = new List<Block>();
            List<Block> visited = new List<Block>();

            Postorder(visited, list, this.blocks[0]);

            return list;
        }

        private void Postorder(List<Block> visited, List<Block> list, Block current)
        {
            if (visited.Contains(current) == false)
            {
                visited.Add(current);

                for (int i = 0; i < current.Outs.Count; i++)
                {
                    Postorder(visited, list, current.Outs[i]);
                }

                list.Add(current);
            }

            return;
        }

        private List<Block> ReversePostorder()
        {
            List<Block> list = new List<Block>();
            List<Block> visited = new List<Block>();
            List<Block> active = new List<Block>();

            visited.Add(this.blocks[0]);
            list.Add(this.blocks[0]);

            ReversePostorder(visited, active, list, this.blocks[0]);

            return list;
        }

        private void ReversePostorder(List<Block> visited, List<Block> active, List<Block> list, Block current)
        {
            if (active.Contains(current) == true)
            {
                return;
            }

            active.Add(current);

            for (int i = 0; i < current.Outs.Count; i++)
            {
                if (visited.Contains(current.Outs[i]) == false)
                {
                    visited.Add(current.Outs[i]);
                    list.Add(current.Outs[i]);
                }
            }

            for (int i = 0; i < current.Outs.Count; i++)
            {
                ReversePostorder(visited, active, list, current.Outs[i]);
            }

            active.Remove(current);

            return;
        }

        private void Dominators()
        {
            for (int i = 0; i < this.blocks.Count; i++)
            {
                foreach (Block block in blocks)
                {
                    this.blocks[i].Dominators.Add(block);
                }
            }

            List<Block> list = Preorder();

            bool changed = true;

            while (changed == true)
            {
                changed = false;

                for (int i = 0; i < list.Count; i++)
                {
                    List<Block> predecessorDoms = new List<Block>();
                    List<Block> doms = new List<Block>();

                    Block block = list[list.Count - 1 - i];

                    // Add the dominator blocks of the predecessors
                    foreach (Block predecessor in block.Ins)
                    {
                        foreach (Block dom in predecessor.Dominators)
                        {
                            if (predecessorDoms.Contains(dom) == false)
                            {
                                predecessorDoms.Add(dom);
                            }
                        }
                    }

                    // For each block in the predecessors' dominators build the intersection
                    foreach (Block predecessorDom in predecessorDoms)
                    {
                        bool include = true;

                        foreach (Block predecessor in block.Ins)
                        {
                            if (predecessor.Dominators.Contains(predecessorDom) == false)
                            {
                                include = false;
                                break;
                            }
                        }

                        if (include == true)
                        {
                            doms.Add(predecessorDom);
                        }
                    }

                    // Add the block itself to the dominators
                    doms.Add(block);

                    // Set the new dominators if there are any differences
                    if (block.Dominators.Count != doms.Count)
                    {
                        block.Dominators = doms;
                        changed = true;
                    }
                    else
                    {
                        foreach (Block dom in doms)
                        {
                            if (block.Dominators.Contains(dom) == false)
                            {
                                block.Dominators = doms;
                                changed = true;
                                break;
                            }
                        }
                    }
                }
            }

            // Compute the Immediate Dominator of each Block
            foreach (Block block in blocks)
            {
                foreach (Block immediateDominator in block.Dominators)
                {
                    // An Immediate Dominator can't be the block itself
                    if (immediateDominator == block)
                    {
                        continue;
                    }

                    bool found = false;

                    foreach (Block dominator in block.Dominators)
                    {
                        if (dominator == immediateDominator || dominator == block)
                        {
                            continue;
                        }

                        // An Immediate Dominator can't dominate another Dominator only the block itself
                        if (dominator.Dominators.Contains(immediateDominator) == true)
                        {
                            found = true;
                            break;
                        }
                    }

                    // We found the Immediate Dominator that does not dominate any other dominator but the block itself
                    if (found == false)
                    {
                        block.ImmediateDominator = immediateDominator;
                        break;
                    }
                }
            }

            // Build the Dominator Tree. The Parent of a Node is the Immediate Dominator of that block.
            foreach (Block parent in blocks)
            {
                foreach (Block possibleChild in blocks)
                {
                    if (parent == possibleChild.ImmediateDominator)
                    {
                        parent.ImmediateDominatorOf.Add(possibleChild);
                    }
                }
            }

            // Compute the Dominance Frontier
            foreach (Block block in blocks)
            {
                if (block.Ins.Count > 1)
                {
                    foreach (Block predecessor in block.Ins)
                    {
                        Block runner = predecessor;

                        while (runner != block.ImmediateDominator)
                        //&& runner != block) // In case we got back throu a Backwards link
                        {
                            runner.DominanceFrontiers.Add(block);
                            runner = runner.ImmediateDominator;
                        }
                    }
                }
            }

            Console.WriteLine("=======================================");
            Console.WriteLine("Dominator");
            Console.WriteLine("=======================================");

            foreach (Block block in this.blocks)
            {
                StringBuilder stringBuilder = new StringBuilder();

                if (block.ImmediateDominator == null)
                {
                    stringBuilder.Append("<>");
                }
                else
                {
                    stringBuilder.Append("<" + block.ImmediateDominator.Index + ">");
                }

                stringBuilder.Append(" " + block.Index + " -> [");

                foreach (Block dominator in block.Dominators)
                {
                    if (dominator != block.Dominators[0])
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(dominator.Index);
                }

                stringBuilder.Append("]");

                Console.WriteLine(stringBuilder.ToString());
            }

            Console.WriteLine("=======================================");
            Console.WriteLine("Dominator Tree");
            Console.WriteLine("=======================================");

            foreach (Block parent in blocks)
            {
                if (parent.ImmediateDominatorOf.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    stringBuilder.Append(parent.Index + " -> [");

                    foreach (Block child in parent.ImmediateDominatorOf)
                    {
                        if (child != parent.ImmediateDominatorOf[0])
                        {
                            stringBuilder.Append(", ");
                        }

                        stringBuilder.Append(child.Index);
                    }

                    stringBuilder.Append("]");

                    Console.WriteLine(stringBuilder.ToString());
                }
            }


            Console.WriteLine("=======================================");
            Console.WriteLine("Dominance Frontiers");
            Console.WriteLine("=======================================");

            foreach (Block parent in blocks)
            {
                if (parent.DominanceFrontiers.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    stringBuilder.Append(parent.Index + " -> [");

                    foreach (Block child in parent.DominanceFrontiers)
                    {
                        if (child != parent.DominanceFrontiers[0])
                        {
                            stringBuilder.Append(", ");
                        }

                        stringBuilder.Append(child.Index);
                    }

                    stringBuilder.Append("]");

                    Console.WriteLine(stringBuilder.ToString());
                }
            }

            return;
        }
       
        /*private class IdentifierBlockList
        {
            public IdentifierBlockList(SharpOS.AOT.IR.Operands.Identifier identifier, List<Block> list)
            {
                this.identifier = identifier;
                this.list = list;
            }

            public SharpOS.AOT.IR.Operands.Identifier identifier = null;
            public List<Block> list = null;
        }*/

        private class IdentifierBlocks : KeyedCollection<SharpOS.AOT.IR.Operands.Identifier, IdentifierBlocksItem>
        {
            internal IdentifierBlocks() : base() { }

            protected override SharpOS.AOT.IR.Operands.Identifier GetKeyForItem(IdentifierBlocksItem item)
            {
                return item.key;
            }

            internal List<SharpOS.AOT.IR.Operands.Identifier> GetKeys()
            {
                List<SharpOS.AOT.IR.Operands.Identifier> values = new List<SharpOS.AOT.IR.Operands.Identifier>();

                foreach (IdentifierBlocksItem item in this)
                {
                    values.Add(item.key);
                }

                return values;
            }

            internal void AddVariable(Identifier identifier, Block block)
            {
                /*if (identifier is SharpOS.AOT.IR.Operands.Register == true)
                {
                    return;
                }*/

                foreach (IdentifierBlocksItem item in this)
                {
                    if (item.key.ID.Equals(identifier.ID) == true)
                    {
                        if (item.values.Contains(block) == false)
                        {
                            item.values.Add(block);
                        }

                        return; ;
                    }
                }

                List<Block> list = new List<Block>();
                list.Add(block);
                
                IdentifierBlocksItem value = new IdentifierBlocksItem(identifier.Clone() as Identifier, list);
                this.Add(value);

                return;
            }
        }

        private class IdentifierBlocksItem
        {
            public IdentifierBlocksItem(SharpOS.AOT.IR.Operands.Identifier key, List<Block> values)
            {
                this.key = key;
                this.values = values;
            }

            public SharpOS.AOT.IR.Operands.Identifier key;
            public List<Block> values;
        }

        private void TransformationToSSA()
        {
            IdentifierBlocks identifierList = new IdentifierBlocks();

            // Find out in which blocks every variable gets defined
            foreach (Block block in blocks)
            {
                foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block)
                {
                    if (instruction is Assign)
                    {
                        Assign assign = instruction as Assign;

                        identifierList.AddVariable(assign.Asignee, block);
                    }
                }
            }

            // Insert PHI
            foreach (IdentifierBlocksItem item in identifierList)
            {
                Console.WriteLine("PHI Identifier: {0}", item.key);

                List<Block> list = item.values;
                List<Block> everProcessed = new List<Block>();

                foreach (Block block in list)
                {
                    everProcessed.Add(block);
                }

                do
                {
                    Block block = list[0];
                    list.RemoveAt(0);

                    foreach (Block dominanceFrontier in block.DominanceFrontiers)
                    {
                        bool found = false;

                        // Is the PHI for the current variable already in the block?
                        foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in dominanceFrontier)
                        {
                            if (instruction is PHI == false)
                            {
                                break;
                            }

                            Assign phi = instruction as Assign;
                            string id = phi.Asignee.Value;

                            if (id.Equals(item.key.Value) == true)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (found == false)
                        {
                            Operand[] operands = new Operand[dominanceFrontier.Ins.Count];

                            for (int i = 0; i < operands.Length; i++)
                            {
                                operands[i] = item.key.Clone();
                            }

                            PHI phi = new PHI(item.key.Clone() as Identifier, new Operands.Miscellaneous(new Operators.Miscellaneous(Operator.MiscellaneousType.InternalList), operands));
                            dominanceFrontier.InsertInstruction(0, phi);

                            if (everProcessed.Contains(dominanceFrontier) == false)
                            {
                                everProcessed.Add(dominanceFrontier);
                                list.Add(dominanceFrontier);
                            }
                        }
                    }
                }
                while (list.Count > 0);
            }

            // Rename the Variables
            foreach (Block block in blocks)
            {
                Dictionary<string, int> count = new Dictionary<string, int>();
                Dictionary<string, Stack<int>> stack = new Dictionary<string, Stack<int>>();

                foreach (IdentifierBlocksItem item in identifierList)
                {
                    count[item.key.Value] = 0;
                    stack[item.key.Value] = new Stack<int>();
                    stack[item.key.Value].Push(0);
                }

                this.SSARename(this.blocks[0], count, stack);
            }

            return;
        }

        private int GetSSAStackValue(Dictionary<string, Stack<int>> stack, string name)
        {
            if (stack.ContainsKey(name) == false)
            {
                return 0;
            }

            return stack[name].Peek();
        }

        private void SSARename(Block block, Dictionary<string, int> count, Dictionary<string, Stack<int>> stack)
        {
            foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block)
            {
                // Update the Operands of the instruction (A = B -> A = B5)
                if (instruction is PHI == false && instruction.Value != null)
                {
                    if (instruction.Value.Operands != null)
                    {
                        foreach (Operand operand in instruction.Value.Operands)
                        {
                            if (operand is Reference == true)
                            {
                                Identifier identifier = (operand as Reference).Value as Identifier;

                                identifier.Version = GetSSAStackValue(stack, identifier.Value);
                            }
                            else if (operand is Identifier == true)
                            {
                                Identifier identifier = operand as Identifier;

                                identifier.Version = GetSSAStackValue(stack, identifier.Value);
                            }
                        }
                    }
                    else if (instruction.Value is Identifier == true)
                    {
                        Identifier identifier = instruction.Value as Identifier;

                        identifier.Version = GetSSAStackValue(stack, identifier.Value);
                    }
                }

                // Update the Definition of a variaable (e.g. A = ... -> A3 = ...)
                if (instruction is Assign == true)
                //|| instruction is PHI == true)
                {
                    if ((instruction as Assign).Asignee is Reference == true)
                    {
                        Identifier identifier = ((instruction as Assign).Asignee as Reference).Value; 

                        identifier.Version = GetSSAStackValue(stack, identifier.Value);
                    }
                    else
                    {
                        string id = (instruction as Assign).Asignee.Value;

                        count[id]++;
                        stack[id].Push(count[id]);

                        (instruction as Assign).Asignee.Version = count[id];
                    }
                }
            }

            // Now update the PHI of the successors
            foreach (Block successor in block.Outs)
            {
                int j = 0;
                bool found = false;

                // Find the position of the link to the successor in the successor itself
                foreach (Block predecessor in successor.Ins)
                {
                    if (predecessor == block)
                    {
                        found = true;
                        break;
                    }

                    j++;
                }

                if (found == false)
                {
                    throw new Exception("Could not find the successor position.");
                }

                // The update the PHI Values
                foreach (Instructions.Instruction instruction in successor)
                {
                    if (instruction is PHI == false)
                    {
                        break;
                    }

                    PHI phi = instruction as PHI;
                    phi.Value.Operands[j].Version = stack[(phi as Assign).Asignee.Value].Peek();
                }
            }

            // Descend in the Dominator Tree and do the "SSA Thing"
            foreach (Block child in block.ImmediateDominatorOf)
            {
                this.SSARename(child, count, stack);
            }

            // Pull from the stack the variable versions of the current block 
            foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block)
            {
                if (instruction is Assign == true)
                {
                    Assign assign = instruction as Assign;

                    if (assign.Asignee is Reference == false)
                    {
                        stack[assign.Asignee.Value].Pop();
                    }
                }
            }

            return;
        }

        private class DefUse : KeyedCollection<string, DefUseItem>
        {
            public DefUse() : base() { }

            protected override string GetKeyForItem(DefUseItem item)
            {
                return item.key;
            }

            public List<string> GetKeys()
            {
                List<string> values = new List<string>();
                
                foreach (DefUseItem item in this)
                {
                    values.Add(item.key);
                }

                return values;
            }
        }

        private class DefUseItem
        {
            public DefUseItem(string key, List<Instructions.Instruction> values)
            {
                this.key = key;
                this.values = values;
            }

            public string key;
            public List<Instructions.Instruction> values;
        }

        DefUse defuse;

        // The first entry in every list of each variable is the definition instruction, the others are the instructions that use the variable.
        private void GetListOfDefUse()
        {
            defuse = new DefUse();

            foreach (Block block in this.blocks)
            {
                foreach (Instructions.Instruction instruction in block)
                {
                    if (instruction.Value != null)
                    {
                        foreach (Operand operand in instruction.Value.Operands)
                        {
                            if (operand is Identifier == true)
                            {
                                string id = operand.ID;

                                if (defuse.Contains(id) == false)
                                {
                                    DefUseItem item = new DefUseItem(id, new List<SharpOS.AOT.IR.Instructions.Instruction>());
                                    item.values.Add(null);
                                    defuse.Add(item);

                                    if (operand.Version == 0)
                                    {
                                        defuse[id].values[0] = new Instructions.System(new SharpOS.AOT.IR.Operands.Miscellaneous(new Operators.Miscellaneous(Operator.MiscellaneousType.Argument)));
                                        defuse[id].values[0].Block = this.blocks[0];
                                    }
                                }

                                if (defuse[id].values.Contains(instruction) == false)
                                {
                                    defuse[id].values.Add(instruction);
                                }
                            }
                        }
                    }

                    if (instruction is Assign == true)
                    {
                        string id = (instruction as Assign).Asignee.ID;

                        if ((instruction as Assign).Asignee is Reference == true)
                        {
                            if (defuse[id].values.Contains(instruction) == false)
                            {
                                defuse[id].values.Add(instruction);
                            }
                        }
                        else
                        {
                            if (defuse.Contains(id) == false)
                            {
                                DefUseItem item = new DefUseItem(id, new List<SharpOS.AOT.IR.Instructions.Instruction>());
                                item.values.Add(instruction);
                                defuse.Add(item);
                            }
                            else
                            {
                                if (defuse[id].values[0] != null)
                                {
                                    throw new Exception("SSA variable '" + id + "' in '" + this.MethodFullName + "' defined a second time.");
                                }

                                defuse[id].values[0] = instruction;
                            }
                        }
                    }
                }
            }

            int stamp = 0;

            foreach (DefUseItem item in defuse)
            {
                string key = item.key;
                List<Instructions.Instruction> list = defuse[key].values;

                if (list[0] == null)
                {
                    throw new Exception("Def statement for '" + key + "' in '" + this.MethodFullName + "' not found.");
                }

                Assign definition = list[0] as Assign;

                if (definition == null)
                {
                    continue;
                }

                definition.Asignee.Stamp = stamp++;

                for (int i = 1; i < list.Count; i++)
                {
                    Instructions.Instruction instruction = list[i];

                    if (instruction is Assign == true
                        && (instruction as Assign).Asignee is Reference == true)
                    {
                        Reference reference = (instruction as Assign).Asignee as Reference;

                        if (reference.Value.ID.Equals(definition.Asignee.ID) == true)
                        {
                            reference.Value = definition.Asignee;
                        }
                    }

                    if (instruction.Value != null)
                    {
                        for (int j = 0; j < instruction.Value.Operands.Length; j++)
                        {
                            Operand operand = instruction.Value.Operands[j];

                            if (operand is Identifier == true)
                            {
                                string id = operand.ID;

                                if (definition.Asignee.ID.Equals(id) == true)
                                {
                                    if (instruction.Value is Reference == true)
                                    {
                                        Reference reference = instruction.Value as Reference;

                                        reference.Value = definition.Asignee;
                                    }
                                    else if (instruction.Value is Identifier == true)
                                    {
                                        instruction.Value = definition.Asignee;
                                    }
                                    else
                                    {
                                        instruction.Value.Operands[j] = definition.Asignee;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("=======================================");
            Console.WriteLine("Def-Use");
            Console.WriteLine("=======================================");

            foreach (DefUseItem item in defuse)
            {
                string key = item.key;
                List<Instructions.Instruction> list = defuse[key].values;

                Console.WriteLine(list[0].Block.Index + " : " + list[0].ToString());

                for (int i = 1; i < list.Count; i++)
                {
                    Instructions.Instruction instruction = list[i];

                    Console.WriteLine("\t" + instruction.Block.Index + " : " + instruction);
                }
            }

            return;
        }

        private void DeadCodeElimination()
        {
            List<string> keys = this.defuse.GetKeys();

            Console.WriteLine("=======================================");
            Console.WriteLine("Dead Code Elimination");
            Console.WriteLine("=======================================");

            while (keys.Count > 0)
            {
                string key = keys[0];
                keys.RemoveAt(0);

                List<Instructions.Instruction> list = this.defuse[key].values;
                
                // This variable is only defined but not used
                if (list.Count == 1
                    && !(list[0] is Assign == true
                        && (list[0] as Assign).Asignee is Field == true))
                {
                    // A = B + C;
                    Instructions.Instruction definition = list[0];

                    Console.WriteLine(definition.Block.Index + " : " + definition.ToString());

                    // Remove the instruction from the block that it is containing it
                    definition.Block.RemoveInstruction(definition);

                    // Remove the variable from the defuse list
                    defuse.Remove(key);

                    if (definition.Value != null
                        && definition is Instructions.System == false)
                    {
                        // B & C used in "A = B + C"
                        foreach (Operand operand in definition.Value.Operands)
                        {
                            if (operand is Identifier == true)
                            {
                                string id = operand.ID;

                                // Remove "A = B + C" from B & C
                                this.defuse[id].values.Remove(definition);

                                // Add to the queue B & C to check them it they are used anywhere else
                                if (keys.Contains(id) == false)
                                {
                                    keys.Add(id);
                                }
                            }
                        }
                    }
                }
            }

            return;
        }

        private void SimpleConstantPropagation()
        {
            List<string> keys = this.defuse.GetKeys();

            Console.WriteLine("=======================================");
            Console.WriteLine("Simple Constant Propagation");
            Console.WriteLine("=======================================");

            keys.Sort();

            while (keys.Count > 0)
            {
                string key = keys[0];
                keys.RemoveAt(0);

                List<Instructions.Instruction> list = this.defuse[key].values;

                Instructions.Instruction definition = list[0];

                Console.WriteLine(definition.Block.Index + " : " + definition.ToString());

                // v2 = PHI(v1, v1)
                if (definition is PHI == true)
                {
                    Operand sample = definition.Value.Operands[0];

                    bool equal = true;

                    for (int i = 1; i < definition.Value.Operands.Length; i++)
                    {
                        if (sample.ID.Equals(definition.Value.Operands[i].ID) == false)
                        {
                            equal = false;
                            break;
                        }
                    }

                    if (equal == true)
                    {
                        Assign assign = new Assign((definition as Assign).Asignee, sample);

                        // Replace the PHI with a normal assignment
                        definition.Block.RemoveInstruction(definition);
                        definition.Block.InsertInstruction(0, assign);

                        defuse[key].values[0] = assign;
                    }
                }
                // A = 100
                else if (definition is Assign == true 
                    && (definition as Assign).Value is Constant == true 
                    && (definition as Assign).Asignee is Field == false)
                {
                    // Remove the instruction from the block that it is containing it
                    definition.Block.RemoveInstruction(definition);

                    // Remove the variable from the defuse list
                    defuse.Remove(key);

                    // "X = A" becomes "X = 100"
                    for (int i = 1; i < list.Count; i++)
                    {
                        Instructions.Instruction used = list[i];

                        if (used.Value != null)
                        {
                            for (int j = 0; j < used.Value.Operands.Length; j++)
                            {
                                Operand operand = used.Value.Operands[j];

                                // Replace A with 100
                                if (operand is Identifier == true
                                    && operand.ID.Equals(key) == true)
                                {
                                    if (used.Value is Identifier == true)
                                    {
                                        used.Value = definition.Value;
                                    }
                                    else
                                    {
                                        used.Value.Operands[j] = definition.Value;
                                    }
                                }
                            }

                            Console.WriteLine("\t" + definition.Block.Index + " : " + used.ToString());
                        }

                        if (used is Assign == true)
                        {
                            string id = (used as Assign).Asignee.ID;

                            // Add to the queue 
                            if (keys.Contains(id) == false)
                            {
                                keys.Add(id);
                            }
                        }
                    }
                }
            }

            return;
        }

        private void CopyPropagation()
        {
            List<string> keys = this.defuse.GetKeys();

            Console.WriteLine("=======================================");
            Console.WriteLine("Copy Propagation");
            Console.WriteLine("=======================================");

            while (keys.Count > 0)
            {
                //Console.Write("Before: "); foreach(string value in keys) Console.Write(" {0}", value); Console.WriteLine();

                string key = keys[0];
                keys.RemoveAt(0);

                //Console.Write("After: "); foreach (string value in keys) Console.Write(" {0}", value); Console.WriteLine();

                Console.WriteLine("[*]Remove Key: {0}", key);

                List<Instructions.Instruction> list = this.defuse[key].values;

                Instructions.Instruction definition = list[0];

                // A = B

                if (definition is Assign != true
                    || list.Count == 1
                    || (definition as Assign).Value.ConvertTo != SharpOS.AOT.IR.Operands.Operand.ConvertType.NotSet
                    || (definition as Assign).Value is Reference == true
                    || (definition as Assign).Value is Field == true
                    || (definition as Assign).Value is Arithmetic == true)
                {
                    continue;
                }

                if ((definition as Assign).Asignee is Register == true
                    && (definition as Assign).Value is Identifier == true
                    && this.engine.Assembly.IsRegister(((definition as Assign).Value as Identifier).Value) == true)
                {
                }
                else if ((definition as Assign).Asignee is Register == true
                    && (definition as Assign).Value is Operands.Call == true
                    && this.engine.Assembly.IsInstruction(((definition as Assign).Value as Operands.Call).Method.DeclaringType.FullName) == true)
                {
                }
                else if ((definition as Assign).Asignee is Identifier == true 
                    && (definition as Assign).Value is Identifier == true)
                {
                }
                else
                {
                    continue;
                }
                
                Console.WriteLine(definition.Block.Index + " : " + definition.ToString());

                // Remove the instruction from the block that it is containing it
                definition.Block.RemoveInstruction(definition);

                // Remove the variable from the defuse list
                defuse.Remove(key);

                // "X = A" becomes "X = B"
                for (int i = 1; i < list.Count; i++)
                {
                    Instructions.Instruction used = list[i];

                    if (used is Assign == true
                        && (used as Assign).Asignee is Reference == true)
                    {
                        Reference reference = (used as Assign).Asignee as Reference;

                        if (reference.ID.Equals(key) == true)
                        {
                            reference.Value = definition.Value as Identifier;
                        }
                    }

                    if (used.Value != null)
                    {
                        for (int j = 0; j < used.Value.Operands.Length; j++)
                        {
                            Operand operand = used.Value.Operands[j];

                            // Replace A with B
                            if (operand is Identifier == true
                                && operand.ID.Equals(key) == true)
                            {
                                if (used.Value is Identifier == true)
                                {
                                    used.Value = definition.Value;
                                }
                                else
                                {
                                    used.Value.Operands[j] = definition.Value;
                                }
                            }
                        }

                        Console.WriteLine("\t" + definition.Block.Index + " : " + used.ToString());
                    }

                    if (used is Assign == true
                        && (used as Assign).Asignee is Reference == false)
                    {
                        string id = (used as Assign).Asignee.ID;

                        // Add to the queue 
                        if (keys.Contains(id) == false)
                        {
                            Console.WriteLine("[*]Add Key: {0}", id);
                            keys.Add(id);
                        }
                    }
                }
            }

            return;
        }


        public string MethodFullName
        {
            get
            {
                return this.methodDefinition.DeclaringType.FullName + "." + this.methodDefinition.Name;
            }
        }

        // If a block that has many predecessors is linked to a block that has many successors 
        // then an empty edge is inserted. Its used later for the transformation out of SSA.
        public void EdgeSplit()
        {
            foreach (Block block in Preorder())
            {
                if (block.Ins.Count > 1)
                {
                    for (int i = 0; i < block.Ins.Count; i++)
                    {
                        Block predecessor = block.Ins[i];

                        if (predecessor.Outs.Count > 1)
                        {
                            int position = 0;

                            for (; position < predecessor.Outs.Count && predecessor.Outs[position] != block; position++) ;

                            if (position == predecessor.Outs.Count)
                            {
                                throw new Exception("In '" + this.MethodFullName + "' Block " + predecessor.Index + " is not linked to the Block " + block.Index + ".");
                            }

                            Block split = new Block();

                            split.Index = this.blocks[this.blocks.Count - 1].Index + 1;
                            split.Type = Block.BlockType.OneWay;
                            split.InsertInstruction(0, new Jump());
                            split.Ins.Add(predecessor);
                            split.Outs.Add(block);

                            predecessor.Outs[position] = split;
                            block.Ins[i] = split;

                            this.blocks.Add(split);
                        }
                    }
                }
            }
        }

        // Transformation out of SSA
        private void TransformationOutOfSSA()
        {
            foreach (Block block in this.blocks)
            {
                List<Instructions.Instruction> remove = new List<SharpOS.AOT.IR.Instructions.Instruction>();

                foreach (Instructions.Instruction instruction in block)
                {
                    if (instruction is PHI == false)
                    {
                        break;
                    }

                    PHI phi = instruction as PHI;

                    for (int i = 0; i < block.Ins.Count; i++)
                    {
                        Block predecessor = block.Ins[i];

                        // Skip uninitilized register assignments (Reg1_5=Reg2_0)
                        if (phi.Value.Operands[i] is Register == true
                            && (phi.Value.Operands[i] as Register).Version == 0)
                        {
                            continue;
                        }

                        Assign assign = new Assign(phi.Asignee, phi.Value.Operands[i]);

                        int position = predecessor.InstructionsCount;

                        if (predecessor.InstructionsCount > 0
                            && predecessor[predecessor.InstructionsCount - 1] is Jump == true)
                        {
                            position--;
                        }

                        predecessor.InsertInstruction(position, assign);

                        remove.Add(instruction);
                    }
                }

                foreach (Instructions.Instruction instruction in remove)
                {
                    block.RemoveInstruction(instruction);
                }
            }

            return;
        }

        private class LiveRange : IComparable
        {
            public LiveRange(string id, Instructions.Instruction start)
            {
                this.id = id;
                this.start = start;
                this.end = start;
            }

            private string id = string.Empty;

            public string ID
            {
                get { return id; }
                set { id = value; }
            }

            private Instructions.Instruction start = null;

            public Instructions.Instruction Start
            {
                get { return start; }
                set { start = value; }
            }

            private Instructions.Instruction end = null;

            public Instructions.Instruction End
            {
                get { return end; }
                set { end = value; }
            }

            private Operand identifier = null;

            public Operand Identifier
            {
                get { return identifier; }
                set { identifier = value; }
            }

            int IComparable.CompareTo(object obj)
            {
                LiveRange liveRange = (LiveRange)obj;

                return this.id.CompareTo(liveRange.id);
            }

            public override string ToString()
            {
                string register = string.Empty;

                if (this.Identifier.Register != int.MinValue)
                {
                    register = "R" + this.Identifier.Register;
                }
                else if (this.Identifier.Stack != int.MinValue)
                {
                    register = "M" + this.Identifier.Stack;
                }

                return this.identifier + " : " + register + " : " + this.Start.Index + " <-> " + this.End.Index;
            }

            public class SortByStart : IComparer<LiveRange>
            {
                int IComparer<LiveRange>.Compare(LiveRange liveRange1, LiveRange liveRange2)
                {
                    if (liveRange1.Start.Index > liveRange2.Start.Index)
                    {
                        return 1;
                    }

                    if (liveRange1.Start.Index < liveRange2.Start.Index)
                    {
                        return -1;
                    }

                    if (liveRange1.End.Index > liveRange2.End.Index)
                    {
                        return 1;
                    }

                    if (liveRange1.End.Index < liveRange2.End.Index)
                    {
                        return -1;
                    }

                    return 0;
                }
            }

            public class SortByEnd : IComparer<LiveRange>
            {
                int IComparer<LiveRange>.Compare(LiveRange liveRange1, LiveRange liveRange2)
                {
                    if (liveRange1.End.Index > liveRange2.End.Index)
                    {
                        return 1;
                    }

                    if (liveRange1.End.Index < liveRange2.End.Index)
                    {
                        return -1;
                    }

                    return 0;
                }
            }

            public class SortByRegisterStack : IComparer<LiveRange>
            {
                int IComparer<LiveRange>.Compare(LiveRange liveRange1, LiveRange liveRange2)
                {
                    if (liveRange1.Identifier.Register != int.MinValue
                        && liveRange2.Identifier.Stack != int.MinValue)
                    {
                        return -1;
                    }

                    if (liveRange1.Identifier.Stack != int.MinValue
                        && liveRange2.Identifier.Register != int.MinValue)
                    {
                        return 1;
                    }

                    if (liveRange1.Identifier.Register != int.MinValue
                        && liveRange2.Identifier.Register != int.MinValue)
                    {
                        if (liveRange1.Identifier.Register > liveRange2.Identifier.Register)
                        {
                            return 1;
                        }

                        if (liveRange1.Identifier.Register < liveRange2.Identifier.Register)
                        {
                            return -1;
                        }

                        return 0;
                    }

                    if (liveRange1.Identifier.Stack > liveRange2.Identifier.Stack)
                    {
                        return 1;
                    }

                    if (liveRange1.Identifier.Stack < liveRange2.Identifier.Stack)
                    {
                        return -1;
                    }

                    return 0;
                    //throw new Exception("No register allocated.");
                }
            }
        }

        private void AddLineScaneValue(Dictionary<string, LiveRange> values, Identifier identifier, Instructions.Instruction instruction)
        {
            if (identifier is Argument == true
                || identifier is Field == true
                || identifier is Reference == true)
            {
                return;
            }

            if (this.engine.Assembly.IsRegister(identifier.Value) == true)
            {
                return;
            }

            string id = identifier.ID;

            if (values.ContainsKey(id) == false)
            {
                LiveRange liveRange = new LiveRange(id, instruction);
                liveRange.Identifier = identifier;

                values[id] = liveRange;
            }
            else
            {
                values[id].End = instruction;
            }
        }

        private List<LiveRange> liveRanges;

        private void ComputeLiveRanges()
        {
            int index = 0;
            Dictionary<string, LiveRange> values = new Dictionary<string, LiveRange>();

            foreach (Block block in ReversePostorder())
            {
                foreach (Instructions.Instruction instruction in block)
                {
                    instruction.Index = index++;

                    if (instruction.Value != null)
                    {
                        foreach (Operand operand in instruction.Value.Operands)
                        {
                            if (operand is Identifier == true)
                            {
                                AddLineScaneValue(values, operand as Identifier, instruction);
                            }
                        }
                    }

                    if (instruction is Assign == true)
                    {
                        AddLineScaneValue(values, (instruction as Assign).Asignee, instruction);
                    }
                }
            }

            this.liveRanges = new List<LiveRange>();

            foreach (KeyValuePair<string, LiveRange> entry in values)
            {
                this.liveRanges.Add(entry.Value);
            }

            this.liveRanges.Sort(new LiveRange.SortByStart());

            Console.WriteLine("=======================================");
            Console.WriteLine("Live Ranges");
            Console.WriteLine("=======================================");

            foreach (LiveRange entry in this.liveRanges)
            {
                Console.WriteLine(entry);
            }

            return;
        }

        private void SetNextStackPosition(Operand operand)
        {
            operand.Stack = this.stackSize;

            this.stackSize++;

            if (operand.SizeType == Operand.InternalSizeType.I8
                || operand.SizeType == Operand.InternalSizeType.R8)
            {
                this.stackSize++;
            }
        }

        private void LinearScanRegisterAllocation()
        {
            List<LiveRange> active = new List<LiveRange>();
            List<int> registers = new List<int>();
            int stackPosition = 0;

            for (int i = 0; i < this.engine.Assembly.AvailableRegistersCount; i++)
            {
                registers.Add(i);
            }

            for (int i = 0; i < this.liveRanges.Count; i++)
            {
                ExpireOldIntervals(active, registers, this.liveRanges[i]);

                if (this.engine.Assembly.Spill(this.liveRanges[i].Identifier.SizeType) == true)
                {
                    SetNextStackPosition(this.liveRanges[i].Identifier);
                }
                else
                {
                    if (active.Count == this.engine.Assembly.AvailableRegistersCount)
                    {
                        SpillAtInterval(active, registers, ref stackPosition, this.liveRanges[i]);
                    }
                    else
                    {
                        int register = registers[0];
                        registers.RemoveAt(0);

                        this.liveRanges[i].Identifier.Register = register;

                        active.Add(this.liveRanges[i]);
                        active.Sort(new LiveRange.SortByEnd());
                    }
                }
            }

            this.liveRanges.Sort(new LiveRange.SortByRegisterStack());

            Console.WriteLine("=======================================");
            Console.WriteLine("Linear Scan Register Allocation");
            Console.WriteLine("=======================================");

            foreach (LiveRange entry in this.liveRanges)
            {
                Console.WriteLine(entry);
            }

            return;
        }

        private void ExpireOldIntervals(List<LiveRange> active, List<int> registers, LiveRange liveRange)
        {
            List<LiveRange> remove = new List<LiveRange>();

            foreach (LiveRange value in active)
            {
                if (value.End.Index >= liveRange.Start.Index)
                {
                    break;
                }

                remove.Add(value);
            }

            foreach (LiveRange value in remove)
            {
                registers.Add(value.Identifier.Register);

                active.Remove(value);
            }
        }

        private void SpillAtInterval(List<LiveRange> active, List<int> registers, ref int stackPosition, LiveRange liveRange)
        {
            LiveRange spill = active[active.Count - 1];

            if (spill.End.Index > liveRange.End.Index)
            {
                liveRange.Identifier.Register = spill.Identifier.Register;

                spill.Identifier.Register = int.MinValue;

                SetNextStackPosition(spill.Identifier);

                active.Remove(spill);

                active.Add(liveRange);
                active.Sort(new LiveRange.SortByEnd());
            }
            else
            {
                SetNextStackPosition(liveRange.Identifier);
            }
        }

        private void ComputeSizeType()
        {
            int unsolvedCounter = 0;
            int lastUnsolvedCounter = 0;

            do 
            {
                lastUnsolvedCounter = unsolvedCounter;
                unsolvedCounter = 0;

                foreach (Block block in this.ReversePostorder())
                {
                    foreach (Instructions.Instruction instruction in block)
                    {
                        if (instruction is Assign == true)
                        {
                            if ((instruction as Assign).Asignee.SizeType == Operand.InternalSizeType.NotSet)
                            {
                                bool found = false;
                                Assign assign = instruction as Assign;

                                if (assign.Value.ConvertTo != Operand.ConvertType.NotSet)
                                {
                                    found = true;
                                    assign.Asignee.SizeType = assign.Value.ConvertSizeType;
                                }
                                else if (assign.Value.SizeType != Operand.InternalSizeType.NotSet)
                                {
                                    found = true;
                                    assign.Asignee.SizeType = assign.Value.SizeType;
                                }
                                else if (assign.Value is Operands.Call == true)
                                {
                                    found = true;
                                    Operands.Call call = assign.Value as Operands.Call;
                                    assign.Asignee.SetSizeType(call.Method.ReturnType.ReturnType.FullName);
                                }
                                else if (assign.Value is Operands.Boolean == true)
                                {
                                    found = true;
                                    assign.Asignee.SizeType = Operand.InternalSizeType.I;
                                }
                                else if (assign.Value.Operands.Length > 0)
                                {
                                    foreach (Operand operand in assign.Value.Operands)
                                    {
                                        if (operand.ConvertTo != Operand.ConvertType.NotSet)
                                        {
                                            found = true;
                                            assign.Asignee.SizeType = operand.ConvertSizeType;
                                            break;
                                        }
                                        else if (operand.SizeType != Operand.InternalSizeType.NotSet)
                                        {
                                            found = true;
                                            assign.Asignee.SizeType = operand.SizeType;
                                            break;
                                        }
                                    }
                                }

                                if (found == false)
                                {
                                    unsolvedCounter++;
                                }
                            }
                        }
                    }
                }

                if (unsolvedCounter != 0 && lastUnsolvedCounter == unsolvedCounter)
                {
                    throw new Exception("Could not compute variable sizes in '" + this.MethodFullName + "'.");
                }
            }
            while (unsolvedCounter != 0);

            return;
        }

        public void Process()
        {
            if (this.methodDefinition.Body == null)
            {
                return;
            }

            this.BuildBlocks();
            this.ClassifyAndLinkBlocks();
            this.BlocksOptimization();
            this.ConvertFromCIL();
            this.Dominators();

            Console.WriteLine(this.Dump());

            this.TransformationToSSA();
            this.EdgeSplit();

            Console.WriteLine(this.Dump());

            this.GetListOfDefUse();
            this.DeadCodeElimination();
            this.SimpleConstantPropagation();

            Console.WriteLine(this.Dump());

            this.CopyPropagation();
            this.TransformationOutOfSSA();

            Console.WriteLine(this.Dump());

            this.ComputeSizeType();
            this.ComputeLiveRanges();
            this.LinearScanRegisterAllocation();

            Console.WriteLine(this.Dump()); //ReversePostorder()));

            return;
        }

        private List<Block> blocks;

        IEnumerator<Block> IEnumerable<Block>.GetEnumerator()
        {
            foreach (Block block in this.blocks)
            {
                yield return block;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Block>)this).GetEnumerator();
        }
    }
}
