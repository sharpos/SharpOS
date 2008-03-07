// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using Mono.Cecil;

using SharpOS.AOT.IR.Instructions;

namespace SharpOS.AOT.IR {
	public enum DumpType : byte {
		Console = 1,
		File = 2,
		Buffer = 4,
		XML = 8
	}

	public enum DumpSection {
		Root,
		Dominance,
		DefineUse,
		SSATransform,
		Optimizations,
		LiveRanges,
		RegisterAllocation,
		MethodBlocks,

		Encoding,
		MethodEncode,
		DataEncode
	}

	public class DumpProcessor : IDisposable {
		private bool enabled = true;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="DumpProcessor"/> will write its content.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DumpProcessor"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="file">The file.</param>
		public DumpProcessor (byte type, string file)
		{
			this.type = type;
			this.file = file;

			if (file != null) {
				this.type |= (byte) DumpType.File;
				this.streamWriter = new StreamWriter (this.file);
				this.streamWriter.AutoFlush = true;
			}
		}

		/// <summary>
		/// Creates a new dump processor that will store it's 
		/// output in the given <see cref="StringBuilder" />.
		/// </summary>
		public DumpProcessor (byte type)
			:
			this (type, null)
		{
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="DumpProcessor"/> is reclaimed by garbage collection.
		/// </summary>
		~DumpProcessor ()
		{
			Dispose (false);
		}

		private byte type;
		private Stack<DumpElement> elements = new Stack<DumpElement> ();
		private string file = null;
		private StreamWriter streamWriter = null;
		private StringBuilder buffer = new StringBuilder ();

		/// <summary>
		/// This class is used to track the previous ElementStack
		/// and sections sent to this dump processor.
		/// </summary>
		private class DumpElement {
			/// <summary>
			/// Initializes a new instance of the <see cref="DumpElement"/> class.
			/// </summary>
			/// <param name="tag">The tag.</param>
			/// <param name="newLine">if set to <c>true</c> [new line].</param>
			public DumpElement (string tag, bool newLine, bool inline, bool property)
			{
				this.Tag = tag;
				this.NewLine = newLine;
				this.Inline = inline;
				this.Property = property;
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="DumpElement"/> class.
			/// </summary>
			/// <param name="tag">The tag.</param>
			public DumpElement (string tag)
				: this (tag, false, true, false)
			{
			}

			public string Tag;
			public bool NewLine;
			public bool Inline;
			public bool Property;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose ()
		{

		}

		/// <summary>
		/// Disposes the specified disposing.
		/// </summary>
		/// <param name="disposing">if set to <c>true</c> [disposing].</param>
		protected void Dispose (bool disposing)
		{
			try {
				if (disposing)
					GC.SuppressFinalize (this);

				if (streamWriter != null)
					streamWriter.Dispose ();
			} catch (Exception) {
				//Console.WriteLine (ex.ToString ());
			}
		}


		/// <summary>
		/// Gets the prefix.
		/// </summary>
		/// <value>The prefix.</value>
		private string Prefix
		{
			get
			{
				int count = this.elements.Count;				
				if (count == 0)
					return String.Empty;
				return new String(' ', (count - 1) * 4);
			}
		}

		/// <summary>
		/// Pushes the element.
		/// </summary>
		/// <param name="name">The name.</param>
		internal void PushElement (string name)
		{
			this.PushElement (name, true, false, false);
		}

		/// <summary>
		/// Pushes the element.
		/// </summary>
		/// <param name="name">The name.</param>
		internal void PushElement (string name, bool newLine, bool inline, bool property)
		{
			this.elements.Push (new DumpElement (name, newLine, inline, property));

			string value = string.Empty;

			if ((this.type & (byte) DumpType.XML) != 0)
				value += string.Format ("{0}<{1}>", this.Prefix, name);

			else {
				if (!inline || property)
					value += string.Format ("{0}{1}: ", this.Prefix, name);

				else if (newLine)
					value += this.Prefix;

				if (!inline)
					value += "\n";
			}

			if (this.enabled && (this.type & (byte) DumpType.Buffer) != 0)
				this.buffer.Append (value);

			if (this.enabled && (this.type & (byte) DumpType.File) != 0
					&& this.streamWriter != null)
				this.streamWriter.Write (value);

			if (this.enabled && (this.type & (byte) DumpType.Console) != 0)
				Console.Write (value);
		}

		/// <summary>
		/// Pops the element.
		/// </summary>
		internal void PopElement ()
		{
			if (this.elements.Count > 0) {
				string value = string.Empty;

				if ((this.type & (byte) DumpType.XML) != 0)
					value = string.Format ("{0}</{1}>", this.Prefix, this.elements.Peek ().Tag);

				if (this.elements.Peek ().NewLine || this.elements.Peek ().Property)
					value += "\n";

				if (this.enabled && (this.type & (byte) DumpType.Buffer) != 0)
					this.buffer.Append (value);

				if (this.enabled && (this.type & (byte) DumpType.File) != 0
						&& this.streamWriter != null)
					this.streamWriter.Write (value);

				if (this.enabled && (this.type & (byte) DumpType.Console) != 0)
					Console.Write (value);

				this.elements.Pop ();
			}
		}

		/// <summary>
		/// Flushes the elements.
		/// </summary>
		internal void FlushElements ()
		{
			while (this.elements.Count > 0)
				this.PopElement ();
		}

		/// <summary>
		/// Sets the content.
		/// </summary>
		/// <param name="value">The value.</param>
		private void SetContent (string value)
		{
			if (!this.elements.Peek ().Inline)
				value += this.Prefix;

			value = string.Format ("{1}", this.Prefix, value);

			if (!this.elements.Peek ().Inline)
				value += "\n";

			if (this.enabled && (this.type & (byte) DumpType.Buffer) != 0)
				this.buffer.Append (value);

			if (this.enabled && (this.type & (byte) DumpType.File) != 0
					&& this.streamWriter != null)
				this.streamWriter.Write (value);

			if (this.enabled && (this.type & (byte) DumpType.Console) != 0)
				Console.Write (value);
		}

		/// <summary>
		/// Adds the element.
		/// </summary>
		/// <param name="tag">The tag.</param>
		/// <param name="value">The value.</param>
		internal void AddElement (string tag, string value)
		{
			this.AddElement (tag, value, false, true, true);
		}

		internal void AddElement (string tag, string value, bool newLine, bool inline, bool property)
		{
			this.PushElement (tag, newLine, inline, property);
			this.SetContent (value);
			this.PopElement ();
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			return this.buffer.ToString ();
		}

		/// <summary>
		/// Items this instance.
		/// </summary>
		public void Item ()
		{
			this.PushElement ("item");
		}
		/*
				/// <summary>
				/// Elements the specified item.
				/// </summary>
				/// <param name="item">The item.</param>
				public void Element (Method.DefUseItem item)
				{
					this.PushElement ("item");

					this.PushElement ("definition", true, false, true);

					this.Element (item.Definition);

					this.PopElement ();

					this.PushElement ("uses");

					foreach (Instruction ins in item)
						this.Element (ins);

					this.PopElement ();

					this.PopElement ();
				}
		*/
		/// <summary>
		/// Elements the specified assembly definition.
		/// </summary>
		/// <param name="assemblyDefinition">The assembly definition.</param>
		/// <param name="filename">The filename.</param>
		public void Element (AssemblyDefinition assemblyDefinition, string filename)
		{
			this.PushElement ("assembly");

			this.AddElement ("name", assemblyDefinition.Name.Name);
			this.AddElement ("filename", filename);
		}


		/// <summary>
		/// Elements the specified type definition.
		/// </summary>
		/// <param name="typeDefinition">The type definition.</param>
		public void Element (TypeReference typeDefinition)
		{
			this.AddElement ("type", typeDefinition.ToString ());
		}

		/// <summary>
		/// Elements the specified method definition.
		/// </summary>
		/// <param name="methodDefinition">The method definition.</param>
		public void Element (MethodReference methodDefinition)
		{
			this.PushElement ("method");

			this.AddElement ("name", methodDefinition.ToString ());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="reason"></param>
		public void IgnoreMember (string name, string reason)
		{
			this.PushElement ("ignore-member");

			this.AddElement ("name", name);
			this.AddElement ("reason", reason);

			this.PopElement ();
		}

		/// <summary>
		/// Dominances the specified blocks.
		/// </summary>
		/// <param name="blocks">The blocks.</param>
		public void Dominance (List<Block> blocks)
		{
			List<int> idominates = new List<int> ();
			List<int> dominators = new List<int> ();
			List<int> frontiers = new List<int> ();
			uint x = 0;

			this.Section (DumpSection.Dominance);

			foreach (Block block in blocks) {
				int idominator = 0;

				// idominates

				foreach (Block dominates in block.ImmediateDominatorOf)
					idominates.Add (dominates.Index);

				// idominator

				if (block.ImmediateDominator != null)
					idominator = block.ImmediateDominator.Index;

				// dominators

				foreach (Block dominator in block.Dominators)
					dominators.Add (dominator.Index);

				// frontiers

				foreach (Block frontier in block.DominanceFrontiers)
					frontiers.Add (frontier.Index);

				BlockDominance (block, idominator, dominators, idominates,
							frontiers);

				idominates.Clear ();
				dominators.Clear ();
				frontiers.Clear ();

				++x;
			}

			this.PopElement ();
		}

		/// <summary>
		/// Dumps the dominators of a given block.
		/// </summary>
		/// <param name="b">The b.</param>
		/// <param name="idominator">The idominator.</param>
		/// <param name="dominators">The dominators.</param>
		/// <param name="dominates">The dominates.</param>
		/// <param name="frontiers">The frontiers.</param>
		private void BlockDominance (Block b, int idominator, List<int> dominators, List<int> dominates,
						List<int> frontiers)
		{
			this.PushElement ("block");

			this.AddElement ("index", b.Index.ToString ());
			this.AddElement ("idominator", idominator.ToString ());
			this.AddElement ("dominators", CombineInts (dominators, "#"));
			this.AddElement ("idominates", CombineInts (dominates, "#"));
			this.AddElement ("frontiers", CombineInts (frontiers, "#"));

			this.PopElement ();
		}

		/// <summary>
		/// Sections the specified sect.
		/// </summary>
		/// <param name="sect">The sect.</param>
		public void Section (DumpSection sect)
		{
			switch (sect) {
			case DumpSection.Root:
				this.PushElement ("aot-dump");
				this.AddElement ("appversion", "SharpOS.AOT/" + Engine.EngineVersion);
				break;
			case DumpSection.Dominance:
				this.PushElement ("dominance");
				break;
			case DumpSection.DefineUse:
				this.PushElement ("define-use");
				break;
			case DumpSection.SSATransform:
				this.PushElement ("ssa-transform");
				break;

			case DumpSection.RegisterAllocation:
				this.PushElement ("register-allocation");
				break;

			case DumpSection.Optimizations:
				this.PushElement ("optimizations");
				break;

			case DumpSection.LiveRanges:
				this.PushElement ("live-ranges");
				break;

			case DumpSection.MethodBlocks:
				this.PushElement ("blocks");
				break;

			case DumpSection.Encoding:
				this.PushElement ("encoding");
				break;

			case DumpSection.MethodEncode:
				this.PushElement ("method-encode");
				break;

			case DumpSection.DataEncode:
				this.PushElement ("data-encode");
				break;

			default:
				throw new EngineException ("dump: unknown section " + sect);
			}
		}

		/// <summary>
		/// Elements the specified block.
		/// </summary>
		/// <param name="block">The block.</param>
		/// <param name="ins">The ins.</param>
		/// <param name="outs">The outs.</param>
		public void Element (Block block, int [] ins, int [] outs)
		{
			string insStr = CombineInts (ins, "#");
			string outsStr = CombineInts (outs, "#");

			this.PushElement ("block");

			this.AddElement ("id", "#" + block.Index.ToString ());
			this.AddElement ("ins", insStr);
			this.AddElement ("outs", outsStr);
		}

		/// <summary>
		/// Adds the specified instruction as an element.
		/// </summary>
		/// <param name="instruction">The instruction.</param>
		public void Element (SharpOS.AOT.IR.Instructions.Instruction instruction)
		{
			instruction.Dump (this);
		}
		
		/*
		/// <summary>
		/// Elements the specified ins.
		/// </summary>
		/// <param name="ins">The ins.</param>
		/// <param name="lvalue">The lvalue.</param>
		/// <param name="attr">The attr.</param>
		/// <param name="lblock">The lblock.</param>
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

			this.PushElement ("instruction");

			this.AddElement ("index", ins.Index.ToString ());
			this.AddElement ("type", ins.GetType ().Name);
			this.AddElement ("block", block.ToString ());

			if (attr != null) {
				foreach (KeyValuePair<string, string> kvp in attr)
					this.AddElement (kvp.Key, kvp.Value);
			}

			this.SetContent (value == null ? "null" : value.ToString ());

			this.PopElement ();

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
				if (this.ConsoleDump) {
					Append ("{0}\t", Prefix);

					if (ins is Instructions.Return)
						Append ("Ret ");

					else if (ins is Instructions.Jump)
						Append ("Jmp ");

				} else
					Append ("{0}- Instruction type: {1}",
							Prefix, ins.GetType ().Name);
				
				if (attr != null) {
					foreach (KeyValuePair<string, string> kvp in attr) {
						if (this.ConsoleDump)
							Append ("{0}", kvp.Value);
						else
							Append (", {0}: {1}", kvp.Key, kvp.Value);
					}
				}

				if (this.ConsoleDump) {
					if (attr != null && attr.Count > 0)
						Append (" = ");

					Append ("{0}\n", (value == null ? "" : value));

				} else
					Append (" == {1}\n", 
						Prefix, (value == null ? "(null)" : value));
			}
		}*/

		/// <summary>
		/// Methods the encode.
		/// </summary>
		/// <param name="method">The method.</param>
		public void MethodEncode (Method method)
		{
			this.AddElement ("method", method.MethodFullName);
		}

		/// <summary>
		/// Cps the add key.
		/// </summary>
		/// <param name="key">The key.</param>
		public void CpAddKey (string key)
		{
			this.PushElement ("add-key");

			this.AddElement ("name", key);
		}

		/// <summary>
		/// Elements the specified live range.
		/// </summary>
		/// <param name="liveRange">The live range.</param>
		public void Element (SharpOS.AOT.IR.Method.LiveRange liveRange)
		{
			string register = null;

			if (liveRange.Identifier.Register != int.MinValue)
				register = "R" + liveRange.Identifier.Register;

			else if (liveRange.Identifier.Stack != int.MinValue)
				register = "M" + liveRange.Identifier.Stack;

			this.PushElement ("range");

			this.AddElement ("identifier", liveRange.Identifier.ToString ());

			if (register != null)
				this.AddElement ("register", register);

			this.AddElement ("start", liveRange.Start.Index.ToString ());
			this.AddElement ("end", liveRange.End.Index.ToString ());

			this.PopElement ();
		}

		/// <summary>
		/// Phis the specified identifier.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		public void PHI (string identifier)
		{
			this.PushElement ("phi");

			this.AddElement ("identifier", identifier);

			this.PopElement ();
		}

		///////////////////////////////////////

		/// <summary>
		/// Combines the ints.
		/// </summary>
		/// <param name="ints">The ints.</param>
		/// <param name="Prefix">The prefix.</param>
		/// <returns></returns>
		private static string CombineInts (int [] ints, string Prefix)
		{
			string str = "";

			for (int x = 0; x < ints.Length; ++x) {
				if (x != 0)
					str += ", ";

				if (Prefix == null)
					str += ints [x];
				else
					str += Prefix + ints [x];
			}

			return str;
		}

		/// <summary>
		/// Combines the ints.
		/// </summary>
		/// <param name="ints">The ints.</param>
		/// <param name="Prefix">The prefix.</param>
		/// <returns></returns>
		private static string CombineInts (List<int> ints, string Prefix)
		{
			string str = "";

			for (int x = 0; x < ints.Count; ++x) {
				if (x != 0)
					str += ", ";

				if (Prefix == null)
					str += ints [x];
				else
					str += Prefix + ints [x];
			}

			return str;
		}
	}
}
