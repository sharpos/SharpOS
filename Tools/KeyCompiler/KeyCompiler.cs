
using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Mono.GetOptions;
using SharpOS.Tools;

namespace SharpOS.Tools.KeyCompiler
{
	public enum SpecialKeys : byte {
		Control = 129,
		LShift,
		RShift,
		Alt,
		CapsLock,
		F1,
		F2,
		F3,
		F4,
		F5,
		F6,
		F7,
		F8,
		F9,
		F10,
		F11,
		F12,
		NumLock,
		ScrollLock, 
		Home,
		UpArrow,
		PageUp,
		LeftArrow,
		RightArrow,
		End,
		DownArrow,
		PageDown,
		Insert,
		Delete
	}
	
	public class UnexpectedTokenException : Exception {
		public UnexpectedTokenException (Token t, string expectedToken)
		{
			this.Token = t;
			this.ExpectedToken = expectedToken;
		}
		
		public Token Token;
		public string ExpectedToken;
	}
	
	public class UnexpectedEndOfFileException : Exception {
		public UnexpectedEndOfFileException (Tokenizer t)
		{
			this.Tokenizer = t;
		}
		
		public Tokenizer Tokenizer;
	}
	
	public class CompilerOptions : Options {
		public CompilerOptions (string [] args):
			base (args)
		{
			
		}
		
		[Option ("Specify the output file", 'o', "out")]
		public string OutputFile = "keymap.skm";

		[Option ("Output encoding (ascii, unicode, xml)", 'x', "encoding", "enc")]
		public string OutputEncoding = "ascii";

		[Option ("Decode a compiled keymap", 'd', "decode", "dec")]
		public bool Decode;
	}
	
	public class Keymap {
		public Keymap ()
		{
		
		}
		
		public Dictionary <int,int> Entries = new Dictionary <int,int> ();	
	}
	
	public interface IEncoding {
		void Encode (int keymask, int statebit, Keymap defaultMap, Keymap shiftedMap,
			Stream output);
		void Decode (out int keymask, out int statebit, out Keymap defaultMap,
			out Keymap shiftedMap, Stream input);
	}

	public class ASCIIEncoding : IEncoding {
		public ASCIIEncoding ()
		{
		}

		void EncodeKeymap (Keymap map, BinaryWriter w)
		{
			int encodedItems = 0;
			int max = 0;

			for (KeyValuePair <int, int> kvp in map.Entries) {
				if (kvp.Key > max)
					max = kvp.Key;
			}
						
			w.Write (max);

			for (int scancode = 0; scancode < max; ++scancode) {
				if (map.Entries.ContainsKey (scancode))
					w.Write ((byte)map.Entries [scancode]);
				else
					w.Write ((byte)0);
			}
		}
		
		Keymap DecodeKeymap (BinaryReader r)
		{
			Keymap map = new Keymap ();
			int count = r.ReadInt32 ();

			for (int scancode = 0; scancode < count; ++scancode) {
				map.Entries [scancode] = r.ReadByte ();
			}

			return map;
		}
		
		public void Encode (int keymask, int statebit, Keymap defaultMap,
				    Keymap shiftedMap, Stream output)
		{
			using (BinaryWriter bw = new BinaryWriter (output)) {
				bw.Write ((byte)keymask);
				bw.Write ((byte)statebit);
				EncodeKeymap (defaultMap, bw);
				EncodeKeymap (shiftedMap, bw);
			}
		}

		public void Decode (out int keymask, out int statebit, out Keymap defaultMap,
				    out Keymap shiftedMap, Stream input)
		{
			using (BinaryReader br = new BinaryReader (input)) {
				keymask = br.ReadByte ();
				statebit = br.ReadByte ();
				defaultMap = DecodeKeymap (br);
				shiftedMap = DecodeKeymap (br);
			}
		}
	}
	
	public class Compiler {
		public Compiler (string skbfile, string encoding)
		{
			this.source = skbfile;

			switch (encoding) {
				case "ascii":
					this.encoding = new ASCIIEncoding ();
				break;
				default:
					throw new Exception ("Unknown encoding: " + encoding);
			}
		}

		public const string Version = "0.0.1";
		
		string source;
		int keymask;
		int statebit;
		Keymap defaultMap;
		Keymap shiftedMap;
		IEncoding encoding;

		public string OutputFile = "keymap.skm";
		public bool Decode = false;
		
		public int Compile ()
		{
			if (Decode) {
				Stream s = File.Open (source, FileMode.Open, FileAccess.Read);

				encoding.Decode (out keymask, out statebit, out defaultMap,
					out shiftedMap, s);

				using (StreamWriter sw = new StreamWriter (OutputFile)) {
					sw.WriteLine ("// decompiled using KeyCompiler {0}", Version);
					sw.WriteLine ();
					sw.WriteLine ("keymask = {0};", keymask);
					sw.WriteLine ("statebit = {0};", statebit);
					sw.WriteLine ();
					sw.WriteLine ("default {");

					foreach (KeyValuePair <int,int> kvp in defaultMap.Entries) {
						string val;

						if (kvp.Value <= 128)
							sw.WriteLine ("\t{0} = '{1}';", kvp.Key, (char)kvp.Value);
						else
							sw.WriteLine ("\t{0} = {1};", kvp.Key, kvp.Value);
					}
					
					sw.WriteLine ("}");
					sw.WriteLine ();
					sw.WriteLine ("shifted {");

					foreach (KeyValuePair <int,int> kvp in shiftedMap.Entries) {
						string val;

						if (kvp.Value <= 128)
							sw.WriteLine ("\t{0} = '{1}';", kvp.Key, (char)kvp.Value);
						else
							sw.WriteLine ("\t{0} = {1};", kvp.Key, kvp.Value);
					}
					
					sw.WriteLine ("}");
				}
				
				return 0;
			} else {
				
				try {
					Parse ();
				} catch (UnexpectedTokenException ute) {
					Console.Error.WriteLine ("Unexpected token '{0}' on line {1}, col {2} (wanted '{3}')",
						ute.Token.Text, ute.Token.Line, ute.Token.Column, ute.ExpectedToken);
					
				} catch (UnexpectedEndOfFileException ueof) {
					int line, col;
					
					ueof.Tokenizer.GetLineInfo (out line, out col);
					
					Console.Error.WriteLine ("Unexpected end of file at line {0}, col {1}",
						line, col);
				}

				Stream s = File.Open (OutputFile, FileMode.Create, FileAccess.Write);

				Console.WriteLine ("Encoding to `{0}'...", OutputFile);
				encoding.Encode (keymask, statebit, defaultMap, shiftedMap, s);
			}
			
			return 0;
		}
		
		public void Parse ()
		{
			string content;
			Tokenizer t;
			Token token;
			
			using (StreamReader sr = new StreamReader (source))
				content = sr.ReadToEnd ();
				
			t = new Tokenizer (content, true);
			
			t.CommentTokenPairs = new string [] {
				"//", "\n",
				"/*", "*/",
			};
			
			t.QuoteTokenPairs = new string [] { "'", "'" };
			t.SpecialTokens = new string [] { "0x" };
			
			while ((token = t.Read ()) != null) {
				bool err = false;
				
				if (token.Type == TokenType.Alphanumeric) {
					switch (token.Text) {
						case "keymask":
							keymask = ReadEqInt (t);
						break;
						case "statebit":
							statebit = ReadEqInt (t);
						break;
						case "default":
							defaultMap = ReadMap (t);
						break;
						case "shifted":
							shiftedMap = ReadMap (t);
						break;
						default:
							err = true;
						break;
					}
				} else
					err = true;
				
				if (err)
					throw new UnexpectedTokenException (token, "<unknown>");
			}
		}
		
		public int ReadInt (Tokenizer t)
		{
			bool neg = false, hex = false;
			int num = 0;
			Token tok = t.Read ();
			
			if (tok.Text == "-") {
				neg = true;
				tok = t.Read ();
			}
			
			if (tok.Text == "0x") {
				string tempNum = "";
				
				do {
					tok = t.Read ();
					
					if (tok.Type == TokenType.Alphanumeric)
						tempNum += tok.Text;
					else
						break;
				} while (true);
		
				try {
					num = int.Parse (tempNum, NumberStyles.HexNumber);
				} catch (FormatException) {
					throw new UnexpectedTokenException (tok, "<hex>");
				}
				
			} else if (tok.Type == TokenType.Alphanumeric) {
				try {
					num = int.Parse (tok.Text);
				} catch (FormatException) {
					throw new UnexpectedTokenException (tok, "<dec>");
				}
			} else
				throw new UnexpectedTokenException (tok, "<number>");
			
			return num;
		}
		
		public int ReadEqInt (Tokenizer t)
		{
			ReadEq (t);
			return ReadInt (t);
		}
		
		public void ReadEq (Tokenizer t)
		{
			Token tok = t.Read ();
			
			if (tok == null)
				throw new UnexpectedEndOfFileException (t);
			
			if (tok.Text != "=")
				throw new UnexpectedTokenException (tok, "=");
		}
		
		public void ReadEnd (Tokenizer t)
		{
			Token tok = t.Read ();
			
			if (tok == null)
				throw new UnexpectedEndOfFileException (t);
			
			if (tok.Text != ";")
				throw new UnexpectedTokenException (tok, ";");
		}
		
		public string ReadString (Tokenizer t)
		{
			Token tok = t.Read ();
			string text;

			if (tok == null)
				throw new UnexpectedEndOfFileException (t);

			if (tok.Type != TokenType.StringQuoteStart)
				throw new UnexpectedTokenException (tok, "<quote-start>");
			
			tok = t.Read ();

			if (tok == null)
				throw new UnexpectedEndOfFileException (t);
			
			if (tok.Type != TokenType.String)
				throw new UnexpectedTokenException (tok, "<string>");
			
			text = tok.Text;
			tok = t.Read ();
			
			if (tok == null)
				throw new UnexpectedEndOfFileException (t);
			
			if (tok.Type != TokenType.StringQuoteEnd)
				throw new UnexpectedTokenException (tok, "<quote-end>");
			
			return text;
		}
		
		public byte ReadEqChar (Tokenizer t)
		{
			Token tok;
			
			ReadEq (t);
			tok = t.Read ();
			
			if (tok.Type == TokenType.StringQuoteStart) {
				string text;
				
				t.PutBack (tok);
				text = ReadString (t);
				
				text = text.Replace ("\\b", "\b");
				text = text.Replace ("\\t", "\t");
				text = text.Replace ("\\n", "\n");
				text = text.Replace ("\\'", "'");
				text = text.Replace ("\\\"", "\"");
				text = text.Replace ("\\\\", "\\");
				
				if (text.Length != 1)
					throw new Exception ("Expected one character, not string '" + text + "'");
				
				return (byte) text [0];
			} else if (tok.Text == "@") {
				tok = t.Read ();
				
				if (tok.Type != TokenType.Alphanumeric)
					throw new UnexpectedTokenException (tok, "<special-key>");
				
				return (byte)Enum.Parse (typeof (SpecialKeys), tok.Text);
				
			} else {
				t.PutBack (tok);
				return (byte) ReadInt (t);
			}
		}
		
		public string ReadEqString (Tokenizer t)
		{
			ReadEq (t);
			return ReadString (t);
		}
		
		public void ReadSectionOpen (Tokenizer t)
		{
			Token tok = t.Read ();
			
			if (tok == null)
				throw new UnexpectedEndOfFileException (t);
			
			if (tok.Text != "{")
				throw new UnexpectedTokenException (tok, "{");
		}
		
		public Keymap ReadMap (Tokenizer t)
		{
			Keymap map = new Keymap ();
			Token tok;
			
			ReadSectionOpen (t);
			
			do {
				byte scancode, key;
				
				tok = t.Read ();
				
				if (tok == null)
					throw new UnexpectedEndOfFileException (t);
				
				if (tok.Text == "}")
					break;
				else
					t.PutBack (tok);
				
				scancode = (byte) ReadInt (t);
				
				key = ReadEqChar (t);
				ReadEnd (t);

				map.Entries [scancode] = key;
			} while (true);
			
			return map;
		}
		
		public static int Main (string [] args)
		{
			CompilerOptions opts = new CompilerOptions (args);
			Compiler c = null;
			
			if (opts.RemainingArguments.Length != 1) {
				Console.Error.WriteLine ("Usage: KeyCompiler [options] <input-file>");
				Console.Error.WriteLine ("Run `KeyCompiler -help` for more information.");
				return 1;
			}
			
			c = new Compiler (opts.RemainingArguments [0], opts.OutputEncoding);
			
			c.OutputFile = opts.OutputFile;
			c.Decode = opts.Decode;
			
			return c.Compile ();
		}
	}
}
