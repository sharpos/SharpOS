// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SharpOS.Tools.DiagnosticTool
{
	public partial class MemoryView : Form
	{
		#region Private fields

		private const int PAGE_SIZE = 4096;

		private bool _emptyPage = true;
		private byte[] _buffer = new byte [PAGE_SIZE];
		private uint _page = 0;
		private uint _pageAddress = 0;
		private int _bytesPerRow = 16;
		private bool _showPageOffset = true;

		#endregion

		public MemoryView ()
		{
			InitializeComponent ();

			_txtAddress.Text = string.Format ("{0:X8}", _pageAddress);
		}

		private static bool CharIsPrintable (char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory (c);
			if (((unicodeCategory == UnicodeCategory.Control) && (unicodeCategory != UnicodeCategory.Format)) && ((unicodeCategory != UnicodeCategory.LineSeparator) && (unicodeCategory != UnicodeCategory.ParagraphSeparator)))
			{
				return (unicodeCategory == UnicodeCategory.OtherNotAssigned);
			}
			return true;
		}

		private void ComputeBytePerRow ()
		{
			int charWidth = 1;

			using (Graphics g = _dumpHexa.CreateGraphics ())
			{
				string test = "oooo | hh hh hh hh hh hh hh hh hh hh hh hh hh hh hh hh | aaaaaaaaaaaaaaaa";
				Size size = g.MeasureString (test, _dumpHexa.Font).ToSize ();
				charWidth = size.Width / test.Length + 1;
			}

			int lineWidth = _dumpHexa.ClientSize.Width;
			int nbCharPerLine = lineWidth / charWidth;
			int constantSize = (_showPageOffset ? 4 : 8) + 3 + 2;

			_bytesPerRow = (nbCharPerLine - constantSize) / 4;
			if (_bytesPerRow < 1) _bytesPerRow = 1;
		}

		private void ReadPage ()
		{
			progressBar1.Value = 0;
			progressBar1.Visible = true;
			btnCancel.Visible = true;
			comThread.RunWorkerAsync ();
		}

		private void comThread_DoWork (object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			e.Result = MainWindow.Client.fn02_MemoryDump (_pageAddress, out _buffer, (System.ComponentModel.BackgroundWorker)sender);
		}

		private void comThread_ProgressChanged (object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
		}

		private void comThread_RunWorkerCompleted (object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			Client.Status s = (Client.Status)e.Result;
			_emptyPage = s != Client.Status.Success;
			progressBar1.Visible = false;
			btnCancel.Visible = false;
			FormatDump ();
		}

		private void btnCancel_Click (object sender, EventArgs e)
		{
			comThread.CancelAsync ();
		}

		private void MemoryView_FormClosing (object sender, FormClosingEventArgs e)
		{
			comThread.CancelAsync ();
		}

		private void FormatDump ()
		{
			if (_emptyPage)
			{
				_dumpHexa.Clear ();
				_dumpHexa.Text = "Unable to read memory. Please update.";
				return;
			}

			// Dump memory into 3 parts : page offset, Hexa dump, ASCII dump
			//
			// oooo | hh hh hh hh ... hh hh | aaaa ... aa   --   9 + 4n
			// oooooooo | hh hh hh hh ... hh hh | aaaa ... aa   --   13 + 4n
			//
			StringBuilder sb = new StringBuilder (4 * PAGE_SIZE);
			StringBuilder asc = new StringBuilder (_bytesPerRow);
			for (int i = 0; i < PAGE_SIZE; i++)
			{
				if (i % _bytesPerRow == 0)
				{
					if (_showPageOffset)
					{
						sb.AppendFormat ("{0:X4} | ", i);
					}
					else
					{
						sb.AppendFormat ("{0:X8} | ", _pageAddress + i);
					}
				}

				sb.AppendFormat ("{0:X2} ", _buffer [i]);

				char c = Convert.ToChar (_buffer [i]);

				if (CharIsPrintable (c))
				{
					asc.Append (c);
				}
				else
				{
					asc.Append ('.');
				}

				if (i % _bytesPerRow == _bytesPerRow - 1)
				{
					sb.Append ("| ");
					sb.Append (asc.ToString ());
					sb.AppendLine ();
					asc = new StringBuilder (_bytesPerRow);
				}
			}
			_dumpHexa.Text = sb.ToString ();
		}

		private void btnNext_Click (object sender, EventArgs e)
		{
			_page++;
			_pageAddress = _page * PAGE_SIZE;
			_txtAddress.Text = string.Format ("{0:X8}", _pageAddress);
			ReadPage ();
		}

		private void btnPrev_Click (object sender, EventArgs e)
		{
			_page--;
			_pageAddress = _page * PAGE_SIZE;
			_txtAddress.Text = string.Format ("{0:X8}", _pageAddress);
			ReadPage ();
		}

		private void _dumpHexa_SizeChanged (object sender, EventArgs e)
		{
			ComputeBytePerRow ();
			FormatDump ();
		}

		private void button1_Click (object sender, EventArgs e)
		{
			_pageAddress = UInt32.Parse (_txtAddress.Text, NumberStyles.HexNumber);
			_page = _pageAddress / PAGE_SIZE;
			ReadPage ();
		}

		private void MemoryView_Shown (object sender, EventArgs e)
		{
			ComputeBytePerRow ();
			ReadPage ();
		}

	}
}
