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
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace SharpOS.Tools.DiagnosticTool
{
	public partial class MainWindow : Form
	{
		static private MainWindow _this;
		static public Client Client;

		public MainWindow ()
		{
			InitializeComponent ();
			_this = this;
			Client = new Client ();

			dLog = new DelegateLog (Log);
			dUpdateGUI = new DelegateUpdateGUI (UpdateGUI);
		}

		private void userDispose ()
		{
			Client.Close();
		}

		public static void InvokeLog (string msg)
		{
			_this.Invoke (_this.dLog, msg);
		}

		private static void Log(string msg)
		{
			_this.textBox1.Text += DateTime.Now.ToLongTimeString() + "\t" + msg + "\r\n";
		}

		private void btnClear_Click (object sender, EventArgs e)
		{
			this.textBox1.Clear ();
		}

		private void btnConnect_Click (object sender, EventArgs e)
		{
			Client.Status s = Client.fn00_Connect ();
			if (s == Client.Status.Error)
			{
				Log("Not connected");
				return;
			}

			this.btnConnect.Enabled = false;
			this.btnHello.Enabled = true;
			this.btnMemory.Enabled = true;
			this.btnUnitTests.Enabled = true;
			Log ("Connected to SharpOS");
		}

		private void btnHello_Click (object sender, EventArgs e)
		{
			string result = string.Empty;
			Client.Status s = Client.fn01_HelloWorld(out result);
			if (s != Client.Status.Success)
			{
			  Log("Hello test failed");
			  return;
			}

			Log(result);
		}

		private void btnMemory_Click (object sender, EventArgs e)
		{
			(new MemoryView ()).Show ();
		}

		private void btnUnitTests_Click (object sender, EventArgs e)
		{
			(new UnitTestsView ()).Show ();
		}


		private delegate void DelegateLog (string msg);
		private delegate void DelegateUpdateGUI ();
		private DelegateLog dLog;
		private DelegateUpdateGUI dUpdateGUI;

		private void UpdateGUI()
		{
			this.btnConnect.Enabled = true;

			Client.Status s = Client.fn00_Connect ();
			if (s == Client.Status.Error)
			{
				Log ("Not connected");
				return;
			}

			this.btnConnect.Enabled = false;
			this.btnHello.Enabled = true;
			this.btnMemory.Enabled = true;
			this.btnUnitTests.Enabled = true;
			Log ("Connected to SharpOS");
		}

		private void WaitForVM ()
		{
			this.Invoke(this.dLog , "Waiting for VM to start...");
			while (Client.Open () != Client.Status.Success)
			{
				Thread.Sleep (1000);
			}

			Console.WriteLine ("Client connected successfully.");
			this.Invoke (this.dLog, "OK. Pipe opened.");
			this.Invoke (this.dUpdateGUI);
		}

		private Process vm = new Process ();

		private void MainWindow_Shown (object sender, EventArgs e)
		{
			new Thread (new ThreadStart (WaitForVM)).Start ();

			string distroPath = Path.Combine (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location), @"distro\");
			vm.StartInfo.FileName = Path.Combine (distroPath, @"qemu\qemu.exe");
			vm.StartInfo.Arguments = "-L " + Path.Combine (distroPath, @"qemu") + " -hda " + Path.Combine (distroPath, @"common\SharpOS.img") + " -serial pipe:SharpOS";
			vm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			vm.Start ();
		}

		private void MainWindow_FormClosing (object sender, FormClosingEventArgs e)
		{
			if(!vm.HasExited)
				vm.Kill ();
		}

	}
}
