// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using Mono.Cecil;

using SharpOS.AOT.IR.Instructions;

namespace SharpOS.AOT.IR {
	public enum DumpType {
		XML, Text
	}
	
	public enum DumpSection {
		Root, Dominance, DefineUse, SSATransform, ConstantFolding,
		ConstantPropagation, CopyPropagation, LiveRanges, 
		DeadCodeElimination, RegisterAllocation, MethodBlocks,
		
		Encoding, MethodEncode, DataEncode
	}
	
	public class DumpProcessor: IDisposable {
		public DumpProcessor (DumpType Type, bool consoleDump, string file)
		{
			this.Type = Type;
			this.ConsoleDump = consoleDump;
			this.File = file;
			
			if (file != null) {
				this.Output = new StreamWriter (file);
				this.Output.AutoFlush = true;
			}
		}
		
		/// <summary>
		/// Creates a new dump processor that will store it's 
		/// output in the given <see cref="StringBuilder" />.
		/// </summary>
		public DumpProcessor (DumpType type, StringBuilder sb):
			this (type, false, null)
		{
			OutputStore = sb;
		}
		
		~DumpProcessor ()
		{
			Dispose (false);
		}
		
		public string TextTab = "  ";
		
		protected DumpType Type;
		protected Stack<DumpElement> ElementStack = new Stack<DumpElement>();
		protected string Prefix = "";
		protected bool ConsoleDump = false;
		protected string File = null;
		protected StreamWriter Output = null;
		protected StringBuilder OutputStore = null;
		
		/**
			<summary>
				This class is used to track the previous ElementStack
				and sections sent to this dump processor.
			</summary>
		*/
		public class DumpElement {
			public DumpElement (string tag)
			{
				Tag = tag;
			}
			
			public string Tag;
		}
		
		public void Dispose ()
		{
		
		}
		
		protected void Dispose (bool disposing)
		{
			if (disposing)
				GC.SuppressFinalize (this);
			
			Output.Dispose ();
		}
		
		/**
			<summary>
				Adds <see cref=ElementextTab" /> to the Prefix member, 
				but only when we are outputting a text dump.
			</summary>
		*/
		protected void IncreasePrefix ()
		{
			if (this.Type == DumpType.Text)
				Prefix += TextTab;
		}
		
		protected void Append (string append, params object [] parms)
		{
			if (this.ConsoleDump)
				Console.Write (append, parms);
			
			if (Output != null)
				Output.Write (append, parms);
			
			if (OutputStore != null)
				OutputStore.AppendFormat (append, parms);
			
		}
		
		protected void AppendLine (string append, params object [] parms)
		{
			if (this.ConsoleDump)
				Console.WriteLine (append, parms);
			
			if (Output != null)
				Output.WriteLine (append, parms);
			
			if (OutputStore != null) {
				OutputStore.AppendFormat (append, parms);
				OutputStore.Append ("\n");
			}
		}
		
		protected void AppendLine ()
		{
			Append ("\n");
		}
		
		/**
			<summary>
				Removes one <see cref=ElementextTab" /> from the 
				Prefix member, but only when we are outputting 
				a text dump.
			</summary>
			<exception cref="InvalidOperationException">
				Thrown when the <see cref="Prefix" /> member is
				already empty or it's length is less than the size
				of <see cref=ElementextTab" />.
			</exception>
		*/
		protected void DecreasePrefix ()
		{
			if (this.Type == DumpType.Text) {
				if (Prefix.Length == 0)
					throw new InvalidOperationException ("Extraneous DecreasePrefix");
				else if (Prefix.Length < TextTab.Length)
					throw new Exception
						("Internal: Prefix variable is in an incorrect state");
					
				Prefix = Prefix.Substring (TextTab.Length);
			}
		}
		
		public void Item ()
		{
			if (this.Type == DumpType.XML)
				Append ("<item>");
			else if (this.Type == DumpType.Text)
				Append ("{0}- Item:\n", Prefix);
			
			ElementStack.Push (new DumpElement ("item"));
			IncreasePrefix ();
		}
		
		public void Element(Method.DefUseItem item)
		{
			if (this.Type == DumpType.XML) {
				Append("<item><definition>");
				Element(item.Definition);
				Append("</definition><uses>");
				
				foreach (Instruction ins in item)
					Element(ins);
					
				Append("</uses></item>");
			} else {
				Element(item.Definition);
				IncreasePrefix();
				foreach (Instruction ins in item)
					Element(ins);
				DecreasePrefix();
			}
		}
		
		/**
			<summary>
			
			</summary>
		*/
		public void Element(AssemblyDefinition assem, string filename)
		{
			if (this.Type == DumpType.XML)
				Append("<assembly name=\"{0}\" filename=\"{0}\">", 
						assem.Name.Name, filename);
			else if (this.Type == DumpType.Text)
				Append("{0}Assembly {1} (loaded from `{2}'):\n", 
						Prefix, assem.Name.Name, filename);
		
			ElementStack.Push(new DumpElement("assembly"));
			IncreasePrefix();
		}
		
		/**
			<summary>
			
			</summary>
		*/
		public void FinishElement()
		{
			DumpElement el = ElementStack.Pop();
			bool remPrefix = true;
			
			if (el.Tag == "aot-dump")
				remPrefix = false;
			
			if (Type == DumpType.XML)
				Append("</{0}>", el.Tag);
			
			if (remPrefix)
				DecreasePrefix();
		}
		
		/**
			<summary>
				
			</summary>
		*/
		public void Element(TypeDefinition klass)
		{
			if (this.Type == DumpType.XML)
				Append("<type name=\"{0}\">", klass);
			else
				Append("{0}Type {1}:\n", Prefix, klass);
		
			ElementStack.Push(new DumpElement("type"));
			IncreasePrefix();
		}
		
		/**
			<summary>
			
			</summary>
		*/
		public void Element(MethodDefinition mr)
		{
			if (this.Type == DumpType.XML)
				Append("<method name=\"{0}\">", mr);
			else if (this.Type == DumpType.Text)
				Append("{0}Method {1}:\n", Prefix, mr);
		
			ElementStack.Push(new DumpElement("method"));
			IncreasePrefix();
		}
		
		/**
			<summary>
			
			</summary>
		*/
		public void IgnoreMember(string name, string reason)
		{
			if (this.Type == DumpType.XML)
				Append("<ignore-member name=\"{0}\" reason=\"{1}\" />", 
							name, reason);
			else if (this.Type == DumpType.Text)
				Append("Ignoring member `{0}': {1}", name, reason);
		}
		
		public void Dominance(List<Block> blocks)
		{
			List<int> idominates = new List<int>();
			List<int> dominators = new List<int>();
			List<int> frontiers = new List<int>();
			uint x = 0;
			
			Section(DumpSection.Dominance);
			
			foreach (Block block in blocks) {
				int idominator = 0;
				
				// idominates
				
				foreach (Block dominates in block.ImmediateDominatorOf)
					idominates.Add(dominates.Index);
				
				// idominator
				
				if (block.ImmediateDominator != null)
					idominator = block.ImmediateDominator.Index;

				// dominators
				
				foreach (Block dominator in block.Dominators)
					dominators.Add(dominator.Index);
				
				// frontiers
				
				foreach (Block frontier in block.DominanceFrontiers)
					frontiers.Add(frontier.Index);
				
				BlockDominance(block, idominator, dominators, idominates,
							frontiers);
				
				idominates.Clear();
				dominators.Clear();
				frontiers.Clear();
				
				++x;
			}
			
			FinishElement();
		}
		
		/**
			<summary>
				Dumps the dominators of a given block.
			</summary>
		*/
		private void BlockDominance(Block b, int idominator, List<int> dominators, List<int> dominates, 
						List<int> frontiers)
		{
			if (this.Type == DumpType.XML)
				Append(
					"<block><index>{0}</index><idominator>{1}</idominator>" + 
					"<dominators>{2}</dominators><idominates>{3}</idominates>" +
					"<frontiers>{4}</frontiers></block>", 
					b.Index, idominator, CombineInts(dominators, null), 
					CombineInts(dominates, null), CombineInts(frontiers, null));
			else
				Append("{0}- Block #{1}, idominator: #{2}, dominators: {3}, " +
						"idominates: {4}, frontiers: {5}\n",
						Prefix, b.Index, idominator, CombineInts(dominators, "#"), 
						CombineInts(dominates, "#"), CombineInts(frontiers, "#"));
		}
		
		public void Section(DumpSection sect)
		{
			string tag = null;
			string text = null;
			string close_text = null;
			bool addPrefix = true;
			
			switch (sect) {
				case DumpSection.Root:
					tag = "aot-dump appversion=\"SharpOS.AOT/" + 
						Engine.EngineVersion + "\"";
					text = "SharpOS.AOT/" + Engine.EngineVersion; // this means `run!!'
					close_text = "aot-dump";
					addPrefix = false;
				break;
				case DumpSection.Dominance:
					tag = "dominance";
					text = "Dominance";
				break;
				case DumpSection.DefineUse:
					tag = "define-use";
					text = "Define-Use";
				break;
				case DumpSection.SSATransform:
					tag = "ssa-transform";
					text = "SSA Transform";
				break;
				case DumpSection.RegisterAllocation:
					tag = "register-alloc";
					text = "Register Allocation";
				break;
				case DumpSection.ConstantPropagation:
					tag = "const-propagation";
					text = "Constant Propagation";
				break;
				case DumpSection.CopyPropagation:
					tag = "copy-propagation";
					text = "Copy Propagation";
				break;
				case DumpSection.LiveRanges:
					tag = "live-ranges";
					text = "Live Ranges";
				break;
				case DumpSection.DeadCodeElimination:
					tag = "deadcode-elim";
					text = "Dead Code Elimination";
				break;
				case DumpSection.ConstantFolding:
					tag = "constant-folding";
					text = "Constant Folding";
				break;
				case DumpSection.MethodBlocks:
					tag = "blocks";
					text = "Blocks";
				break;
				case DumpSection.Encoding:
					tag = "encoding";
					text = "Encoding";
				break;
				case DumpSection.MethodEncode:
					tag = "method-encode";
					text = "Encoding methods";
				break;
				case DumpSection.DataEncode:
					tag = "data-encode";
					text = "Encoding static class fields";
				break;
				default:
					throw new Exception("dump: unknown section " + sect);
			}
			
			if (this.Type == DumpType.XML)
				Append("<{0}>", tag);
			else
				Append("{0}{1}:\n", Prefix, text);
			
			if (close_text != null)
				ElementStack.Push(new DumpElement(close_text));
			else
				ElementStack.Push(new DumpElement(tag));
			
			if (addPrefix)
				IncreasePrefix();
		}
		
		public void Element(Block block, int[] ins, int[] outs)
		{
			string insStr = CombineInts (ins, "#");
			string outsStr = CombineInts (outs, "#");
			
			if (this.Type == DumpType.XML)
				Append ("<block ins=\"{0}\" outs=\"{1}\">", insStr, outsStr);
			else if (this.Type == DumpType.Text) {
				Append ("{0}- Block #{1}", Prefix, 
						block.Index, insStr, outsStr);
			
				if (insStr != "")
					Append (", ins: {0}", insStr);
				if (outsStr != "")
					Append (", outs: {0}", outsStr);
				
				AppendLine ();
			}
			
			ElementStack.Push (new DumpElement ("block"));
			IncreasePrefix ();
		}
		
		public void Element (SharpOS.AOT.IR.Instructions.Instruction ins)
		{
			Element (ins, null, null, null);
		}
		
		public void Element (SharpOS.AOT.IR.Instructions.Instruction ins, object lvalue, 
					Dictionary<string, string> attr, int ?lblock)
		{
			object value = lvalue;
			int block = 0;
			
			if (value == null)
				value = ins.Value;
			
			if (lblock == null)
				block = ins.Block.Index;
			else
				block = (int)lblock;
			
			if (this.Type == DumpType.XML) {
				Append ("<instruction index=\"{0}\" type=\"{1}\" block=\"{2}\"", 
							ins.Index, ins.GetType().Name, block);
				
				if (attr != null) foreach (KeyValuePair<string,string> kvp in attr)
					Append (" {0}=\"{1}\"", kvp.Key, kvp.Value);
				
				if (value == null)
					Append (">null</instruction>");
				else
					Append (">{0}</instruction>", value);
			} else if (this.Type == DumpType.Text) {
				Append ("{0}- Instruction type: {1}", 
							Prefix, ins.GetType().Name);
				
				if (attr != null) {
					foreach (KeyValuePair<string,string> kvp in attr)
						Append (", {0}: {1}", kvp.Key, kvp.Value);
				}
				
				Append ("   == {1}\n", 
					Prefix, (value == null ? "(null)" : value));
			}
		}
		
		public void MethodEncode (Method m)
		{
			if (this.Type == DumpType.XML)
				Append ("<method name=\"{0}\" />", m.MethodFullName);
			else if (this.Type == DumpType.Text)
				Append ("{0}- Method {1}\n", Prefix, m.MethodFullName);
		}
		
		public void CpAddKey (string key)
		{
			if (this.Type == DumpType.XML)
				Append ("<add-key name=\"{0}\" />", key);
			else
				Append ("{0}- Add key {1}\n", Prefix, key);
		}
		
		public void Element (SharpOS.AOT.IR.Method.LiveRange r)
		{
			string register = null;
			
			if (r.Identifier.Register != int.MinValue)
				register = "R" + r.Identifier.Register;
			else if (r.Identifier.Stack != int.MinValue)
				register = "M" + r.Identifier.Stack;
			
			if (this.Type == DumpType.XML) {
				Append ("<range identifier=\"{0}\"", r.Identifier);
				
				if (register != null)
					Append (" register=\"{0}\"", register);
				
				Append (" start=\"{0}\" end=\"{1}\" />", r.Start.Index, r.End.Index);
			} else if (this.Type == DumpType.Text) {
				Append ("{0}- Range {1}, {2}start: {3}, end: {4}\n",
						Prefix, r.Identifier,
						(register != null ? "register: " + register + ", " : ""),
						r.Start.Index, r.End.Index);
			}
		}
		
		public void Phi (string ident)
		{
			if (this.Type == DumpType.XML)
				Append ("<phi identifier=\"{0}\" />", ident);
			else
				Append ("{0}- Phi {1}\n", Prefix, ident);
		}
		
		
		
		///////////////////////////////////////
		
		private string CombineInts (int[] ints, string Prefix)
		{
			string str = "";
			
			for (int x = 0; x < ints.Length; ++x) {
				if (x != 0)
					str += ", ";
				
				if (Prefix == null)
					str += ints[x];
				else
					str += Prefix + ints[x];
			}
			
			return str;
		}
		
		private string CombineInts (List<int> ints, string Prefix)
		{
			string str = "";
			
			for (int x = 0; x < ints.Count; ++x) {
				if (x != 0)
					str += ", ";
				
				if (Prefix == null)
					str += ints[x];
				else
					str += Prefix + ints[x];
			}
			
			return str;
		}
	}
}