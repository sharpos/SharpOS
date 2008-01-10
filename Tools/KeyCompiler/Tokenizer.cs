
using System;
using System.IO;
using System.Collections.Generic;

namespace SharpOS.Tools {
	public enum TokenType {
		None,
		Whitespace,
		Alphanumeric,
		Symbolic,
		String,

		StringQuoteStart,
		StringQuoteEnd,
		CommentStart,
		CommentEnd,
		Special,
		MaybeSpecial,
	}

	public class Token {
		public Token (Tokenizer o, char c, int offset, int line, int ch)
		{
			Owner = o;
			Text = c.ToString ();
			Offset = offset;
			Line = line;
			Column = ch;
			Type = TokenType.None;

			if (o.PrimitiveDecider != null) {
				Type = o.PrimitiveDecider (c);
			}

			if (Type == TokenType.None) {
				if (char.IsLetterOrDigit (c) || c == '_')
					Type = TokenType.Alphanumeric;
				else {
					if (Owner.IsQuoteToken (c.ToString (), 0)) {
						Type = TokenType.StringQuoteStart;

					} else if (Owner.IsCommentToken (c.ToString (), 0)) {
						Type = TokenType.CommentStart;

					} else if (!Owner.IgnoreSpecial &&
						   (Owner.MaybeQuoteToken (c.ToString (), 0) ||
						   Owner.MaybeCommentToken (c.ToString (), 0))) {
						Type = TokenType.MaybeSpecial;
					}

					if (Type == TokenType.None)
						Type = TokenType.Symbolic;
				}

				foreach (char t in Owner.WhitespaceChars)
					if (c == t)
						Type = TokenType.Whitespace;
			}
		}

		public Token (Tokenizer t, string text, int offset, int line, int ch, TokenType type)
		{
			Owner = t;
			Text = text;
			Offset = offset;
			Line = line;
			Column = ch;
			Type = type;
		}

		public Token (Token t1, Token t2, TokenType type)
		{
			Owner = t1.Owner;
			Text = t1.Text + t2.Text;
			Offset = t1.Offset;
			Line = t1.Line;
			Column = t1.Column;
			Type = type;
		}

		public Tokenizer Owner;
		public string Text;
		public int Offset;
		public int Line;
		public int Column;
		public TokenType Type;

		public string PositionString {
			get {
				return string.Format ("({0}, {1})", Line, Column);
			}
		}

		public override string ToString ()
		{
			return string.Format ("[{0}] on line {1} col {2}", Text, Line, Column);
		}

		public static Token operator + (Token t1, Token t2)
		{
			// if the two tokens combined make a definite special token, return it
			if (t1.Owner.IsSpecialToken (t1.Text + t2.Text))
				return new Token (t1, t2, TokenType.Special);

			// if the two tokens combined make a possible special token and the owner
			// isn't ignoring potential special tokens, return it.
			if (t1.Owner.MaybeSpecialToken (t1.Text + t2.Text) && !t1.Owner.IgnoreSpecial) {
				return new Token (t1, t2, TokenType.MaybeSpecial);
			}

			// First, if the first token is MaybeSpecial and t1.Text + t2.Text is the precursor 
			// or entirety of a quote character, then return it.

			if (t1.Type == TokenType.MaybeSpecial) {
				if (t1.Owner.IsQuoteToken (t1.Text + t2.Text, 0))
					return new Token (t1, t2, TokenType.StringQuoteStart);
				if (t1.Owner.MaybeQuoteToken (t1.Text + t2.Text, 0))
					return new Token (t1, t2, t1.Type);

				if (t1.Owner.IsCommentToken (t1.Text + t2.Text, 0))
					return new Token (t1, t2, TokenType.CommentStart);
				if (t1.Owner.MaybeCommentToken (t1.Text + t2.Text, 0))
					return new Token (t1, t2, t1.Type);


				// False positive...	
				return null;
			}



			// If both types are the same type and the combined tokens result in a valid token
			// return it.

			if (t1.Type == t2.Type) {
				if (t1.Type == TokenType.Symbolic || t1.Type == TokenType.Special) {
					return null;	// these tokens cannot be simply added
				}

				if (t1.Type == TokenType.None) {
					throw new Exception ("Bug: Left side operand was not given a token type");
				}

				if (t1.Type == TokenType.Special) {
					return null;
				}

				return new Token (t1, t2, t1.Type);
			}

			return null;
		}
	}

	public class Tokenizer {
		public Tokenizer (string text, bool ignws)
		{
			Text = text;
			IgnoreWhitespace = ignws;
		}

		public delegate TokenType DPrimitiveDecider (char c);
		public DPrimitiveDecider PrimitiveDecider;
		public string Text;
		public bool IgnoreWhitespace = false;
		public char [] WhitespaceChars = new char [] { ' ', '\t', '\n', '\r' };
		public string [] SpecialTokens = new string [0];

		public bool StringMode = false;
		public string EndStringSequence = null;
		public TokenType EndStringSeqType;

		protected string [] _CommentTokenPairs = new string [0];
		protected string [] _QuoteTokenPairs = new string [0];

		public string [] CommentTokenPairs
		{
			get
			{
				return _CommentTokenPairs;
			}
			set
			{
				if ((double) value.Length / 2.0 == Math.Floor ((double) value.Length / 2.0))
					_CommentTokenPairs = value;
				else
					throw new Exception ("CommentTokenPairs.Length must be an even number (paired)");
			}
		}

		public string [] QuoteTokenPairs
		{
			get
			{
				return _QuoteTokenPairs;
			}
			set
			{
				if ((double) value.Length / 2.0 == Math.Floor ((double) value.Length / 2.0))
					_QuoteTokenPairs = value;
				else
					throw new Exception ("QuoteTokenPairs.Length must be an even number (paired)");
			}
		}

		public int Caret
		{
			get
			{
				return _Caret;
			}
		}

		protected int _Caret = 0;
		protected int _Line = 0, _Column = 0;

		public void GetLineInfo (out int line, out int col)
		{
			line = _Line;
			col = _Column;
		}

		public bool IsQuoteToken (string str)
		{
			return IsQuoteToken (str, -1);
		}

		public bool IsQuoteToken (string str, int cmpn)
		{
			for (int x = 0; x + 1 < QuoteTokenPairs.Length; x += 2) {
				if (str == QuoteTokenPairs [x] && cmpn <= 0)
					return true;
				else if (str == QuoteTokenPairs [x + 1] && (cmpn == -1 || cmpn == 1))
					return true;
			}

			return false;
		}

		public bool MaybeQuoteToken (string str)
		{
			return MaybeQuoteToken (str, -1);
		}

		public bool MaybeQuoteToken (string str, int cmpn)
		{
			for (int x = 0; x + 1 < QuoteTokenPairs.Length; x += 2) {
				if (QuoteTokenPairs [x].StartsWith (str) && cmpn <= 0)
					return true;
				else if (QuoteTokenPairs [x + 1].StartsWith (str) && (cmpn == -1 || cmpn == 1))
					return true;
			}

			return false;
		}

		public bool IsCommentToken (string str, int cmpn)
		{
			for (int x = 0; x + 1 < CommentTokenPairs.Length; x += 2) {
				if (str == CommentTokenPairs [x] && cmpn <= 0)
					return true;
				else if (str == CommentTokenPairs [x + 1] && (cmpn == -1 || cmpn == 1))
					return true;
			}

			return false;
		}

		public bool MaybeCommentToken (string str, int cmpn)
		{
			for (int x = 0; x + 1 < CommentTokenPairs.Length; x += 2) {
				if (CommentTokenPairs [x].StartsWith (str) && cmpn <= 0)
					return true;
				else if (CommentTokenPairs [x + 1].StartsWith (str) && (cmpn == -1 || cmpn == 1))
					return true;
			}

			return false;
		}

		public bool IsSpecialToken (string str)
		{
			foreach (string t in SpecialTokens) {
				if (str == t)
					return true;
			}

			return false;
		}

		public bool MaybeSpecialToken (string str)
		{
			foreach (string spec in SpecialTokens) {
				if (str.Length <= spec.Length) {
					if (str == spec.Substring (0, str.Length))
						return true;
				}
			}

			return false;
		}

		// When the parser thinks it is parsing a special symbol but upon further
		// examination finds it is not, this is set and tokenizing is restarted from
		// the position before the current Read () moved the caret with this set. 
		// This causes the parser to ignore the positive return value of MaybeSpecialToken ().
		private bool _IgnoreSpecial = false;

		public bool IgnoreSpecial
		{
			get
			{
				return _IgnoreSpecial;
			}
		}

		protected List<Token> _Queue = new List<Token> ();

		public void PutBack (Token t)
		{
			_Queue.Add (t);
		}

		public void PutBack (params Token [] t)
		{
			PutBack (false, t);
		}

		public void PutBack (bool reverse, params Token [] ts)
		{
			Token [] rev_ts = ts;

			if (reverse) {
				rev_ts = new Token [ts.Length];

				for (int x = 0; x < ts.Length; ++x)
					rev_ts [x] = ts [ts.Length - x - 1];
			}

			foreach (Token t in rev_ts)
				_Queue.Add (t);
		}

		public Token Read ()
		{
			if (_Queue.Count > 0) {
				Token tx = _Queue [0];
				_Queue.RemoveAt (0);

				return tx;
			}

			Token t = _Read ();

			if (IgnoreWhitespace) {
				while (t != null && t.Type == TokenType.Whitespace)
					t = _Read ();
			}

			if (t == null)
				return null;

			if (t.Type == TokenType.StringQuoteStart) {
				StringMode = true;

				_EndStringFlag = false;
				EndStringSequence = QuoteTokenPairs [Array.IndexOf (QuoteTokenPairs, t.Text) + 1];
				EndStringSeqType = TokenType.StringQuoteEnd;

			} else if (t.Type == TokenType.CommentStart) {
				string ends = CommentTokenPairs [Array.IndexOf (CommentTokenPairs, t.Text) + 1];

				StringMode = true;
				_EndStringFlag = false;
				EndStringSequence = ends;
				EndStringSeqType = TokenType.CommentEnd;

				do {
					t = _Read ();
				} while (t != null && t.Text != ends);

				StringMode = false;
				_EndStringFlag = false;
				EndStringSequence = null;
				EndStringSeqType = TokenType.None;

				t = Read ();
			} else if (t.Type == TokenType.StringQuoteEnd || t.Type == TokenType.CommentEnd) {
				StringMode = false;
				_EndStringFlag = false;
			}

			return t;
		}

		private bool _EndStringFlag = false;
		private string _EndStringTok = null;

		private Token _StringRead ()
		{
			if (_EndStringFlag) {
				return new Token (this, _EndStringTok, _Caret, _Line, _Column, EndStringSeqType);
			}

			char c = Text [_Caret];

			string str = "";

			int crt = _Caret, col = _Column, ln = _Line;
			bool skipCheck = false;

			if (!skipCheck && c == '\\') {
				skipCheck = true;
				c = Text [++_Caret];
			}

			while (_Caret < Text.Length) {
				if (skipCheck) {
					if (c == 'n')
						str += "\n";
					else if (c == 'b')
						str += "\b";
					else if (c == 't')
						str += "\t";
					else if (c == 'r')
						str += "\r";
					else
						str += c.ToString ();
				} else
					str += c.ToString ();

				if (!skipCheck && str.EndsWith (EndStringSequence)) {
					_EndStringFlag = true;
					_EndStringTok = EndStringSequence;

					++_Caret;


					return new Token (this, str.Substring (0, str.Length - EndStringSequence.Length),
							crt, ln, col, TokenType.String);
				}

				if (c == '\n') {
					++_Line;
					_Column = 0;
				} else
					++_Column;

				if (!skipCheck && Text [_Caret] == '\\') {
					str = str.Substring (0, str.Length - 1);
					skipCheck = true;
				} else {
					skipCheck = false;
				}

				c = Text [++_Caret];
			}

			return new Token (this, str, crt, ln, col, TokenType.String);
		}

		private Token _Read ()
		{
			if (Text.Length == 0)
				return null;

			if (StringMode) {
				return _StringRead ();
			}

			int startCaret = _Caret;
			int startLine = _Line;
			int startCol = _Column;

			if (_Caret >= Text.Length)
				return null;
			if (_Caret < 0)
				_Caret = 0;

			char c = Text [_Caret];

			Token t = new Token (this, c, _Caret, _Line, _Column);

			if (StringMode)
				return t;	// it has just switched to, so return this and await string parsing.

			if (c == '\n') {
				++_Line;
				_Column = 0;
			}

			++_Column;
			++_Caret;

			if (_Caret >= Text.Length) {
				return t;
			}

			c = Text [_Caret];

			// try to combine the tokens

			while (_Caret < Text.Length) {
				Token newt = t + new Token (this, c, _Caret, _Line, _Column);

				if (c == '\n') {
					++_Line;
					_Column = 0;
				}

				if (newt == null) {
					if (t.Type == TokenType.MaybeSpecial) {
						// We may have parsed into multiple tokens
						// in our search for what we thought was a 
						// special token. Now we must disable special
						// tokens and reparse from where we started

						bool old = _IgnoreSpecial;
						_IgnoreSpecial = true;
						_Caret = startCaret;
						_Line = startLine;
						_Column = startCol;
						try {
							return Read ();
						} finally {
							_IgnoreSpecial = old;
						}
					}

					return t;
				}

				t = newt;

				++_Column;
				++_Caret;

				if (_Caret < Text.Length)
					c = Text [_Caret];
			}

			return t;
		}
	}
}
