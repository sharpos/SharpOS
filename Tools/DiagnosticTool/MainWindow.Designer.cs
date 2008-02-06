// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

namespace SharpOS.Tools.DiagnosticTool
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose ();
			}
			userDispose ();
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.Windows.Forms.Button btnClearLog;
			this.btnConnect = new System.Windows.Forms.Button ();
			this.btnHello = new System.Windows.Forms.Button ();
			this.textBox1 = new System.Windows.Forms.TextBox ();
			this.btnMemory = new System.Windows.Forms.Button ();
			this.btnUnitTests = new System.Windows.Forms.Button ();
			btnClearLog = new System.Windows.Forms.Button ();
			this.SuspendLayout ();
			// 
			// btnClearLog
			// 
			btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			btnClearLog.Location = new System.Drawing.Point (12, 280);
			btnClearLog.Name = "btnClearLog";
			btnClearLog.Size = new System.Drawing.Size (60, 23);
			btnClearLog.TabIndex = 4;
			btnClearLog.Text = "Clear log";
			btnClearLog.UseVisualStyleBackColor = true;
			btnClearLog.Click += new System.EventHandler (this.btnClear_Click);
			// 
			// btnConnect
			// 
			this.btnConnect.Enabled = false;
			this.btnConnect.Location = new System.Drawing.Point (12, 12);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size (60, 23);
			this.btnConnect.TabIndex = 0;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler (this.btnConnect_Click);
			// 
			// btnHello
			// 
			this.btnHello.Enabled = false;
			this.btnHello.Location = new System.Drawing.Point (95, 12);
			this.btnHello.Name = "btnHello";
			this.btnHello.Size = new System.Drawing.Size (60, 23);
			this.btnHello.TabIndex = 0;
			this.btnHello.Text = "Hello";
			this.btnHello.UseVisualStyleBackColor = true;
			this.btnHello.Click += new System.EventHandler (this.btnHello_Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point (12, 41);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size (485, 233);
			this.textBox1.TabIndex = 2;
			this.textBox1.WordWrap = false;
			// 
			// btnMemory
			// 
			this.btnMemory.Enabled = false;
			this.btnMemory.Location = new System.Drawing.Point (161, 12);
			this.btnMemory.Name = "btnMemory";
			this.btnMemory.Size = new System.Drawing.Size (60, 23);
			this.btnMemory.TabIndex = 3;
			this.btnMemory.Text = "Memory";
			this.btnMemory.UseVisualStyleBackColor = true;
			this.btnMemory.Click += new System.EventHandler (this.btnMemory_Click);
			// 
			// btnUnitTests
			// 
			this.btnUnitTests.Enabled = false;
			this.btnUnitTests.Location = new System.Drawing.Point (227, 12);
			this.btnUnitTests.Name = "btnUnitTests";
			this.btnUnitTests.Size = new System.Drawing.Size (60, 23);
			this.btnUnitTests.TabIndex = 5;
			this.btnUnitTests.Text = "Unit tests";
			this.btnUnitTests.UseVisualStyleBackColor = true;
			this.btnUnitTests.Click += new System.EventHandler (this.btnUnitTests_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (509, 315);
			this.Controls.Add (this.btnUnitTests);
			this.Controls.Add (btnClearLog);
			this.Controls.Add (this.btnMemory);
			this.Controls.Add (this.textBox1);
			this.Controls.Add (this.btnHello);
			this.Controls.Add (this.btnConnect);
			this.Name = "MainWindow";
			this.Text = "SharpOS Diagnostic Tool";
			this.Shown += new System.EventHandler (this.MainWindow_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler (this.MainWindow_FormClosing);
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnHello;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnMemory;
		private System.Windows.Forms.Button btnUnitTests;
	}
}

