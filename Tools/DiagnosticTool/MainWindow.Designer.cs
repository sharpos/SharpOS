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
			this.msgView = new System.Windows.Forms.TextBox ();
			this.btnMemory = new System.Windows.Forms.Button ();
			this.btnUnitTests = new System.Windows.Forms.Button ();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer ();
			this.debugOutput = new System.Windows.Forms.TextBox ();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip ();
			this.statusConnection = new System.Windows.Forms.ToolStripStatusLabel ();
			this.label1 = new System.Windows.Forms.Label ();
			btnClearLog = new System.Windows.Forms.Button ();
			this.splitContainer1.Panel1.SuspendLayout ();
			this.splitContainer1.Panel2.SuspendLayout ();
			this.splitContainer1.SuspendLayout ();
			this.statusStrip1.SuspendLayout ();
			this.SuspendLayout ();
			// 
			// btnClearLog
			// 
			btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			btnClearLog.Location = new System.Drawing.Point (437, 12);
			btnClearLog.Name = "btnClearLog";
			btnClearLog.Size = new System.Drawing.Size (60, 23);
			btnClearLog.TabIndex = 4;
			btnClearLog.Text = "Clear log";
			btnClearLog.UseVisualStyleBackColor = true;
			btnClearLog.Click += new System.EventHandler (this.btnClear_Click);
			// 
			// msgView
			// 
			this.msgView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.msgView.Location = new System.Drawing.Point (0, 0);
			this.msgView.Multiline = true;
			this.msgView.Name = "msgView";
			this.msgView.ReadOnly = true;
			this.msgView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.msgView.Size = new System.Drawing.Size (485, 75);
			this.msgView.TabIndex = 2;
			this.msgView.WordWrap = false;
			// 
			// btnMemory
			// 
			this.btnMemory.Enabled = false;
			this.btnMemory.Location = new System.Drawing.Point (12, 12);
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
			this.btnUnitTests.Location = new System.Drawing.Point (78, 12);
			this.btnUnitTests.Name = "btnUnitTests";
			this.btnUnitTests.Size = new System.Drawing.Size (60, 23);
			this.btnUnitTests.TabIndex = 5;
			this.btnUnitTests.Text = "Unit tests";
			this.btnUnitTests.UseVisualStyleBackColor = true;
			this.btnUnitTests.Click += new System.EventHandler (this.btnUnitTests_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point (12, 41);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add (this.label1);
			this.splitContainer1.Panel1.Controls.Add (this.debugOutput);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add (this.msgView);
			this.splitContainer1.Size = new System.Drawing.Size (485, 249);
			this.splitContainer1.SplitterDistance = 170;
			this.splitContainer1.TabIndex = 6;
			// 
			// debugOutput
			// 
			this.debugOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.debugOutput.Location = new System.Drawing.Point (0, 16);
			this.debugOutput.Multiline = true;
			this.debugOutput.Name = "debugOutput";
			this.debugOutput.ReadOnly = true;
			this.debugOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.debugOutput.Size = new System.Drawing.Size (485, 152);
			this.debugOutput.TabIndex = 0;
			this.debugOutput.WordWrap = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange (new System.Windows.Forms.ToolStripItem [] {
            this.statusConnection});
			this.statusStrip1.Location = new System.Drawing.Point (0, 293);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size (509, 22);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statusConnection
			// 
			this.statusConnection.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.statusConnection.Name = "statusConnection";
			this.statusConnection.Size = new System.Drawing.Size (46, 17);
			this.statusConnection.Text = "R: / W:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point (0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size (72, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Debug output";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (509, 315);
			this.Controls.Add (this.statusStrip1);
			this.Controls.Add (this.splitContainer1);
			this.Controls.Add (this.btnUnitTests);
			this.Controls.Add (btnClearLog);
			this.Controls.Add (this.btnMemory);
			this.Name = "MainWindow";
			this.Text = "SharpOS Diagnostic Tool";
			this.Shown += new System.EventHandler (this.MainWindow_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler (this.MainWindow_FormClosing);
			this.splitContainer1.Panel1.ResumeLayout (false);
			this.splitContainer1.Panel1.PerformLayout ();
			this.splitContainer1.Panel2.ResumeLayout (false);
			this.splitContainer1.Panel2.PerformLayout ();
			this.splitContainer1.ResumeLayout (false);
			this.statusStrip1.ResumeLayout (false);
			this.statusStrip1.PerformLayout ();
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private System.Windows.Forms.TextBox msgView;
		private System.Windows.Forms.Button btnMemory;
		private System.Windows.Forms.Button btnUnitTests;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox debugOutput;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusConnection;
		private System.Windows.Forms.Label label1;
	}
}

