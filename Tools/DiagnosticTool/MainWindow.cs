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

			dMessage = new DelegateMessage (Message);
			dUpdateGUI = new DelegateUpdateGUI (UpdateGUI);
			dAddLogger = new DelegateAddLogger (AddLogger);
			dSetStatus = new DelegateSetStatus (SetStatus);
		}

		private void userDispose ()
		{
			Client.Close();
		}

		public static void InvokeLog (string msg)
		{
			_this.Invoke (_this.dMessage, msg);
		}

		private void btnClear_Click (object sender, EventArgs e)
		{
			this.msgView.Clear ();
		}

		private void btnMemory_Click (object sender, EventArgs e)
		{
			(new MemoryView ()).Show ();
		}

		private void btnUnitTests_Click (object sender, EventArgs e)
		{
			(new UnitTestsView ()).Show ();
		}


		private delegate void DelegateMessage (string msg);
		private delegate void DelegateUpdateGUI ();
		private delegate void DelegateAddLogger (string log);
		private delegate void DelegateSetStatus (string status);
		private DelegateMessage dMessage;
		private DelegateUpdateGUI dUpdateGUI;
		private DelegateAddLogger dAddLogger;
		private DelegateSetStatus dSetStatus;

		private static void Message (string msg)
		{
			_this.msgView.Text += DateTime.Now.ToLongTimeString () + "\t" + msg + "\r\n";
		}

		private void UpdateGUI ()
		{
			Client.Status s = Client.fn00_Connect ();
			if (s == Client.Status.Error)
			{
				Message ("Not connected");
				return;
			}

			this.btnMemory.Enabled = true;
			this.btnUnitTests.Enabled = true;
			Message ("Connected to SharpOS");
		}

		private void AddLogger (string log)
		{
			_this.debugOutput.Text += log;
		}

		private void SetStatus (string status)
		{
			_this.statusConnection.Text = status;
		}

		private void WaitForVM ()
		{
			this.Invoke(this.dMessage , "Waiting for VM to start...");
			while (Client.Open () != Client.Status.Success)
			{
				Thread.Sleep (1000);
			}

			Console.WriteLine ("Client connected successfully.");
			this.Invoke (this.dMessage, "OK. Pipe opened.");
			this.Invoke (this.dUpdateGUI);

			while (true)
			{
				string status = string.Format ("R: {0} / W: {1}", Client.BytesRead, Client.BytesWritten);
				this.Invoke (this.dSetStatus, status);

				string log = System.Text.Encoding.ASCII.GetString (Client.LogQueue.ToArray ());
				Client.LogQueue.Clear ();
				this.Invoke (this.dAddLogger, log.Replace ("\n", "\r\n")); 

				Thread.Sleep (100);
			}
		}

		private Thread waitForVMThread;
		private Process vm = new Process ();

		private void MainWindow_Shown (object sender, EventArgs e)
		{
      waitForVMThread = new Thread(new ThreadStart(WaitForVM));
			waitForVMThread.Start ();
			waitForVMThread.Name = "WaitForVM";

      string basePath = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location);
			string distroPath = Path.Combine (basePath, "distro");
      string qemuPath = Path.Combine (distroPath , "qemu");
      string commonPath = Path.Combine (distroPath, "common");

      vm.StartInfo.FileName = Path.Combine (qemuPath , "qemu.exe");
      vm.StartInfo.Arguments = "-L " + qemuPath + " -hda " + Path.Combine (commonPath , "SharpOS.img") + " -serial pipe:SharpOS-Log -serial pipe:SharpOS-Control";
			vm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			vm.Start ();
		}

		private void MainWindow_FormClosing (object sender, FormClosingEventArgs e)
		{
			if (waitForVMThread!=null && waitForVMThread.IsAlive)
        waitForVMThread.Abort ();

			if (!vm.HasExited)
				vm.Kill ();
		}

	}
}
