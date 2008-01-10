
using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Mono.GetOptions;
using SharpOS.Tools;

namespace SharpOS.Tools.KeyCompiler {
	public enum SpecialKeys : int {
		Control = 65536,
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
		public UnexpectedEndOfFileException (Tokenizer t, string expectedToken)
		{
			this.Tokenizer = t;
			this.ExpectedToken = expectedToken;
		}

		public Tokenizer Tokenizer;
		public string ExpectedToken;
	}

	public class CompilerOptions : Options {
		public CompilerOptions (string [] args)
			:
			base (args)
		{

		}

		[Option ("Specify the output file", 'o', "out")]
		public string OutputFile = "keymap.skm";

		[Option ("Output encoding (ascii, unicode, xml)", 'x', "encoding", "enc")]
		public string OutputEncoding = "ascii";

		[Option ("Decode a compiled keymap", 'd', "decode", "dec")]
		public bool Decode;

		[Option ("Create a keymap archive (.ska)", 'a', "archive", "ar")]
		public bool Archive;

		[Option ("Force operation", 'f', "force")]
		public bool Force;

		[Option ("Maximum parse errors", "maxerrors", "maxerr")]
		public int MaxErrors = 5;

		[Option ("Change to directory", 'D', "dir")]
		public string WorkingDirectory = ".";
	}

	public class Keymap {
		public Keymap ()
		{

		}

		public Dictionary<int, int> Entries = new Dictionary<int, int> ();
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
			int max = 0;

			foreach (KeyValuePair<int, int> kvp in map.Entries) {
				if (kvp.Key > max)
					max = kvp.Key;
			}

			w.Write (max);

			for (int scancode = 0; scancode < max; ++scancode) {
				int value = map.Entries [scancode];
				int specConv = 129 - (int) SpecialKeys.Control;

				if (!map.Entries.ContainsKey (scancode)) {
					w.Write ((byte) 0);
					continue;
					;
				}

				if (value > 0xFFFF)
					value = value + specConv;

				if (value > 255)
					Console.Error.WriteLine ("Warning: key `{0}' is too large", value);

				w.Write ((byte) value);
			}
		}

		Keymap DecodeKeymap (BinaryReader r)
		{
			Keymap map = new Keymap ();
			int count = r.ReadInt32 ();
			int specConv = (int) SpecialKeys.Control - 129;

			for (int scancode = 0; scancode < count; ++scancode) {
				byte value = r.ReadByte ();

				if (value > 128)
					map.Entries [scancode] = (int) value + specConv;
				else
					map.Entries [scancode] = value;
			}

			return map;
		}

		public void Encode (int keymask, int statebit, Keymap defaultMap,
				    Keymap shiftedMap, Stream output)
		{
			using (BinaryWriter bw = new BinaryWriter (output)) {
				bw.Write ((byte) keymask);
				bw.Write ((byte) statebit);
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

	public class UnicodeEncoding : IEncoding {
		public UnicodeEncoding ()
		{
		}

		void EncodeKeymap (Keymap map, BinaryWriter w)
		{
			int max = 0;

			foreach (KeyValuePair<int, int> kvp in map.Entries) {
				if (kvp.Key > max)
					max = kvp.Key;
			}

			w.Write (max);

			for (int scancode = 0; scancode < max; ++scancode) {
				if (map.Entries.ContainsKey (scancode))
					w.Write ((char) map.Entries [scancode]);
				else
					w.Write ((char) 0);
			}
		}

		Keymap DecodeKeymap (BinaryReader r)
		{
			Keymap map = new Keymap ();
			int count = r.ReadInt32 ();

			for (int scancode = 0; scancode < count; ++scancode) {
				map.Entries [scancode] = r.ReadChar ();
			}

			return map;
		}

		public void Encode (int keymask, int statebit, Keymap defaultMap,
				    Keymap shiftedMap, Stream output)
		{
			using (BinaryWriter bw = new BinaryWriter (output)) {
				bw.Write ((char) keymask);
				bw.Write ((char) statebit);
				EncodeKeymap (defaultMap, bw);
				EncodeKeymap (shiftedMap, bw);
			}
		}

		public void Decode (out int keymask, out int statebit, out Keymap defaultMap,
				    out Keymap shiftedMap, Stream input)
		{
			using (BinaryReader br = new BinaryReader (input)) {
				keymask = br.ReadChar ();
				statebit = br.ReadChar ();
				defaultMap = DecodeKeymap (br);
				shiftedMap = DecodeKeymap (br);
			}
		}
	}

	public class Compiler {
		public Compiler (string [] args, string encoding)
		{
			this.args = args;

			switch (encoding) {
			case "ascii":
				this.encoding = new ASCIIEncoding ();
				break;
			case "unicode":
				this.encoding = new UnicodeEncoding ();
				break;
			default:
				throw new Exception ("Unknown encoding: " + encoding);
			}
		}

		public const string Version = "0.0.1";

		string [] args;
		string source;
		int keymask;
		int statebit;
		Keymap defaultMap;
		Keymap shiftedMap;
		IEncoding encoding;

		public string OutputFile = null;
		public bool Decode = false;
		public bool Archive = false;
		public bool Force = false;
		public int MaxErrors = 5;
		public string WorkingDirectory = ".";

		void Decompile (Stream s)
		{
			using (StreamWriter sw = new StreamWriter (s)) {
				sw.WriteLine ("// decompiled using KeyCompiler {0}", Version);
				sw.WriteLine ();
				sw.WriteLine ("keymask = 0x{0};", keymask.ToString ("x"));
				sw.WriteLine ("statebit = 0x{0};", statebit.ToString ("x"));
				sw.WriteLine ();
				sw.WriteLine ("default {");

				foreach (KeyValuePair<int, int> kvp in defaultMap.Entries) {
					if (kvp.Value <= 128)
						sw.WriteLine ("\t{0} = '{1}';", kvp.Key, (char) kvp.Value);
					else
						sw.WriteLine ("\t{0} = @{1};", kvp.Key, (SpecialKeys) kvp.Value);
				}

				sw.WriteLine ("}");
				sw.WriteLine ();
				sw.WriteLine ("shifted {");

				foreach (KeyValuePair<int, int> kvp in shiftedMap.Entries) {
					string val;

					if (kvp.Value == (int) '\n')
						val = "\\n";
					else if (kvp.Value == (int) '\b')
						val = "\\b";
					else if (kvp.Value == (int) '\r')
						val = "\\r";
					else if (kvp.Value == (int) '\t')
						val = "\\t";
					else if (kvp.Value <= 128)
						val = "'" + ((char) kvp.Value) + "'";
					else
						val = "@" + ((SpecialKeys) kvp.Value);

					sw.WriteLine ("\t{0} = '{1}';", kvp.Key, val);
				}

				sw.WriteLine ("}");
			}
		}

		public int UnpackArchive (Stream s, string wd)
		{
			using (BinaryReader r = new BinaryReader (s)) {
				int count = r.ReadInt32 ();

				for (int x = 0; x < count; ++x) {
					string keymapName = r.ReadString ();
					string fileName = Path.Combine (wd, keymapName + ".skm");
					Stream outp = null;
					byte [] buffer = new byte [2048];
					int read = 0;

					if (File.Exists (fileName) && !Force) {
						Console.Error.WriteLine ("{0}: file exists (-force to override)",
							fileName);

						continue;
					}

					Console.WriteLine ("Unpacking keymap `{0}' to {1}",
						keymapName, fileName);

					outp = File.Open (fileName, FileMode.Create,
						FileAccess.Write);

					while ((read = s.Read (buffer, 0, buffer.Length)) > 0)
						outp.Write (buffer, 0, read);

					outp.Close ();
				}
			}

			return 0;
		}

		public int CreateArchive (Stream s, string [] initArgs)
		{
			List<string> args = new List<string> ();

			foreach (string arg in initArgs) {
				if (arg.StartsWith ("@")) {
					using (StreamReader sr = new StreamReader (arg.Substring (1))) {
						foreach (string sa in sr.ReadToEnd ().
							 Split (' '))
							args.Add (sa);
					}
				} else
					args.Add (arg);
			}

			if (args.Count == 0) {
				Console.Error.WriteLine ("Not enough arguments");
				return 1;
			}

			using (BinaryWriter w = new BinaryWriter (s)) {
				w.Write (args.Count);

				foreach (string arg in args) {
					string name, file;
					Stream inp = null;
					byte [] buffer = new byte [2048];
					int read = 0;

					if (!arg.Contains (":")) {
						Console.Error.WriteLine ("Argument format: name:file");
						return 1;
					}

					name = arg.Substring (0, arg.IndexOf (":"));
					file = arg.Substring (arg.IndexOf (":") + 1);
					inp = File.Open (file, FileMode.Open, FileAccess.Read);

					w.Write (name);

					while ((read = inp.Read (buffer, 0, buffer.Length)) > 0)
						s.Write (buffer, 0, read);

					inp.Close ();
				}
			}

			return 0;
		}

		public int Compile ()
		{
			Stream s = null;

			OutputFile = Path.GetFullPath (OutputFile);
			string directory = Path.GetDirectoryName (OutputFile);
			if (!Directory.Exists (directory))
				Directory.CreateDirectory (directory);

			if (Decode && Archive) {
				if (!Directory.Exists (OutputFile) || args.Length != 1) {
					Console.Error.WriteLine ("Usage: KeyCompiler.exe -decode -archive -out <directory> <archive>");

					return 1;
				}

				s = File.Open (args [0], FileMode.Open, FileAccess.Read);

				try {
					return UnpackArchive (s, OutputFile);
				} finally {
					s.Close ();
				}

			} else if (Decode) {
				if (args.Length != 1) {
					Console.Error.WriteLine ("Usage: KeyCompiler.exe -decode -out <file.skm> <file.sk>");

					return 1;
				}

				s = File.Open (args [0], FileMode.Open, FileAccess.Read);
				encoding.Decode (out keymask, out statebit,
					out defaultMap, out shiftedMap, s);
				s.Close ();

				s = File.Open (OutputFile, FileMode.Create, FileAccess.Write);
				Decompile (s);
				s.Close ();

			} else if (Archive) {
				s = File.Open (OutputFile, FileMode.Create, FileAccess.Write);
				CreateArchive (s, args);
				s.Close ();

			} else {
				int parseResult;

				this.source = args [0];
				parseResult = Parse (this.source);

				if (parseResult != 0) {
					Console.Error.WriteLine ("Failed to parse file `{0}'", this.source);
					return parseResult;
				}

				Console.WriteLine ("Encoding to `{0}'...", OutputFile);

				s = File.Open (OutputFile, FileMode.Create, FileAccess.Write);
				encoding.Encode (keymask, statebit, defaultMap, shiftedMap, s);
			}

			return 0;
		}

		public int Parse (string file)
		{
			string content;
			Tokenizer t;
			int errors = 0;
			int line, col;

			if (file == null)
				throw new ArgumentNullException ("file");

			using (StreamReader sr = new StreamReader (file))
				content = sr.ReadToEnd ();

			t = new Tokenizer (content, true);

			t.CommentTokenPairs = new string [] {
				"//", "\n",
				"/*", "*/",
			};

			t.QuoteTokenPairs = new string [] { "'", "'" };
			t.SpecialTokens = new string [] { "0x" };

			while (true) {
				bool failed = false;

				try {
					if (!ParseStatement (t))
						break;
				} catch (UnexpectedTokenException ute) {

					failed = true;
					Console.Error.WriteLine (
						"Unexpected token '{0}' on line {1}, col {2} (wanted '{3}')",
						ute.Token.Text, ute.Token.Line, ute.Token.Column,
						ute.ExpectedToken);

				} catch (UnexpectedEndOfFileException ueof) {

					failed = true;
					ueof.Tokenizer.GetLineInfo (out line, out col);
					Console.Error.WriteLine (
						"Unexpected end of file at line {0}, col {1}; expected `{2}'",
						line, col, ueof.ExpectedToken);

					return 2;
				}

				if (failed) {
					Token tk;

					++errors;

					if (errors >= MaxErrors)
						break;

					while ((tk = t.Read ()) != null) {
						if (tk.Text == ";")
							break;
					}
				}
			}

			if (errors > 0)
				return 1;

			return 0;
		}

		public bool ParseStatement (Tokenizer t)
		{
			bool err = false;
			Token token = t.Read ();

			if (token == null)
				return false;

			if (token.Type == TokenType.Alphanumeric) {
				switch (token.Text) {
				case "keymask":
					keymask = ReadEqInt (t);
					break;
				case "statebit":
					statebit = ReadEqInt (t);
					break;
				case "default":
					Console.WriteLine ("Parsing `default' keymap...");
					defaultMap = ReadMap (t);
					break;
				case "shifted":
					Console.WriteLine ("Parsing `shifted' keymap...");
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

			return true;
		}

		public int ReadInt (Tokenizer t)
		{
			bool neg = false;
			int num;
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

			if (neg)
				return -num;
			else
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
				throw new UnexpectedEndOfFileException (t, "=");

			if (tok.Text != "=")
				throw new UnexpectedTokenException (tok, "=");
		}

		public void ReadEnd (Tokenizer t)
		{
			Token tok = t.Read ();

			if (tok == null)
				throw new UnexpectedEndOfFileException (t, ";");

			if (tok.Text != ";")
				throw new UnexpectedTokenException (tok, ";");
		}

		public string ReadString (Tokenizer t)
		{
			Token tok = t.Read ();
			string text;

			if (tok == null)
				throw new UnexpectedEndOfFileException (t, "<quote-start>");

			if (tok.Type != TokenType.StringQuoteStart)
				throw new UnexpectedTokenException (tok, "<quote-start>");

			tok = t.Read ();

			if (tok == null)
				throw new UnexpectedEndOfFileException (t, "<string>");

			if (tok.Type != TokenType.String)
				throw new UnexpectedTokenException (tok, "<string>");

			text = tok.Text;
			tok = t.Read ();

			if (tok == null)
				throw new UnexpectedEndOfFileException (t, "<quote-end>");

			if (tok.Type != TokenType.StringQuoteEnd)
				throw new UnexpectedTokenException (tok, "<quote-end>");

			return text;
		}

		public int ReadEqChar (Tokenizer t)
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
					throw new Exception (tok.PositionString + ": Expected one character, not string '" + text + "'");

				return text [0];
			} else if (tok.Text == "@") {
				tok = t.Read ();

				if (tok.Type != TokenType.Alphanumeric)
					throw new UnexpectedTokenException (tok, "<special-key>");

				return (int) Enum.Parse (typeof (SpecialKeys), tok.Text);

			} else {
				t.PutBack (tok);
				return ReadInt (t);
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
				throw new UnexpectedEndOfFileException (t, "{");

			if (tok.Text != "{")
				throw new UnexpectedTokenException (tok, "{");
		}

		public Keymap ReadMap (Tokenizer t)
		{
			Keymap map = new Keymap ();
			Token tok;

			ReadSectionOpen (t);

			do {
				int scancode, key;

				tok = t.Read ();

				if (tok == null)
					throw new UnexpectedEndOfFileException (t, "}");

				if (tok.Text == "}")
					break;
				else
					t.PutBack (tok);

				scancode = ReadInt (t);

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

			if (args.Length == 0 || opts.RemainingArguments.Length == 9) {
				Console.Error.WriteLine ("Usage: KeyCompiler [options] <input-file> ...");
				Console.Error.WriteLine ("Run `KeyCompiler -help` for more information.");
				return 1;
			}

			c = new Compiler (opts.RemainingArguments, opts.OutputEncoding);

			c.OutputFile = opts.OutputFile;
			c.Decode = opts.Decode;
			c.Force = opts.Force;
			c.Archive = opts.Archive;
			c.WorkingDirectory = opts.WorkingDirectory;

			Environment.CurrentDirectory = Path.GetFullPath (c.WorkingDirectory);

			return c.Compile ();
		}
	}
}
