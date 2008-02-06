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
using System.Windows.Forms;

namespace SharpOS.Tools.DiagnosticTool
{
	public partial class UnitTestsView : Form
	{
		Client.UnitTests ut;

		public UnitTestsView ()
		{
			InitializeComponent ();

			listView1.Items.Clear();
			listView1.Groups.Clear ();

			ut = new Client.UnitTests ();
			comThread.RunWorkerAsync ();
			comThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler (comThread_ProgressChanged);
		}

		void comThread_ProgressChanged (object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
		}

		private void comThread_DoWork (object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			ut.Clear ();
			e.Result = MainWindow.Client.fn03_UnitTests (ut, (System.ComponentModel.BackgroundWorker)sender);
		}

		private void comThread_RunWorkerCompleted (object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			label1.Visible = false;
			progressBar1.Visible = false;
			btnCancel.Visible = false;

			Client.Status result = (Client.Status)e.Result;
			if( result!=Client.Status.Success )
				return;

			int passed = 0;
			foreach (Client.UnitTestDataItem item in ut)
			{
				ListViewItem lvi = new ListViewItem (new string [] { item.Name, item.Result.ToString () });

				lvi.Group = listView1.Groups [item.Source];
				if (lvi.Group == null)
				{
					lvi.Group = new ListViewGroup (item.Source, item.Source);
					listView1.Groups.Add (lvi.Group);
				}

				listView1.Items.Add (lvi);

				if(item.Result) passed++;
			}

			label2.Text = string.Format ("{0} tests: {1} passed, {2} failed.", ut.Count, passed, ut.Count - passed);
			label2.Visible = true;
		}

		private void btnCancel_Click (object sender, EventArgs e)
		{
			comThread.CancelAsync ();
		}

		private void UnitTestsView_FormClosing (object sender, FormClosingEventArgs e)
		{
			comThread.CancelAsync ();
		}
	}
}
