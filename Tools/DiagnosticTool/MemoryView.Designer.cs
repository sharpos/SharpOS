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
	partial class MemoryView
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
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.Windows.Forms.Label label1;
			this._dumpHexa = new System.Windows.Forms.TextBox ();
			this._btnNext = new System.Windows.Forms.Button ();
			this._btnPrev = new System.Windows.Forms.Button ();
			this._txtAddress = new System.Windows.Forms.TextBox ();
			this._btnGo = new System.Windows.Forms.Button ();
			this.comThread = new System.ComponentModel.BackgroundWorker ();
			this.progressBar1 = new System.Windows.Forms.ProgressBar ();
			this.btnCancel = new System.Windows.Forms.Button ();
			label1 = new System.Windows.Forms.Label ();
			this.SuspendLayout ();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point (88, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size (62, 13);
			label1.TabIndex = 3;
			label1.Text = "Address: 0x";
			// 
			// _dumpHexa
			// 
			this._dumpHexa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this._dumpHexa.BackColor = System.Drawing.SystemColors.Control;
			this._dumpHexa.Font = new System.Drawing.Font ("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._dumpHexa.HideSelection = false;
			this._dumpHexa.Location = new System.Drawing.Point (12, 32);
			this._dumpHexa.Multiline = true;
			this._dumpHexa.Name = "_dumpHexa";
			this._dumpHexa.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this._dumpHexa.Size = new System.Drawing.Size (992, 442);
			this._dumpHexa.TabIndex = 0;
			this._dumpHexa.WordWrap = false;
			this._dumpHexa.SizeChanged += new System.EventHandler (this._dumpHexa_SizeChanged);
			// 
			// _btnNext
			// 
			this._btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._btnNext.Location = new System.Drawing.Point (41, 3);
			this._btnNext.Name = "_btnNext";
			this._btnNext.Size = new System.Drawing.Size (23, 23);
			this._btnNext.TabIndex = 2;
			this._btnNext.Text = ">";
			this._btnNext.UseVisualStyleBackColor = false;
			this._btnNext.Click += new System.EventHandler (this.btnNext_Click);
			// 
			// _btnPrev
			// 
			this._btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._btnPrev.Location = new System.Drawing.Point (12, 3);
			this._btnPrev.Name = "_btnPrev";
			this._btnPrev.Size = new System.Drawing.Size (23, 23);
			this._btnPrev.TabIndex = 1;
			this._btnPrev.Text = "<";
			this._btnPrev.UseVisualStyleBackColor = false;
			this._btnPrev.Click += new System.EventHandler (this.btnPrev_Click);
			// 
			// _txtAddress
			// 
			this._txtAddress.Location = new System.Drawing.Point (147, 6);
			this._txtAddress.Name = "_txtAddress";
			this._txtAddress.Size = new System.Drawing.Size (92, 20);
			this._txtAddress.TabIndex = 4;
			// 
			// _btnGo
			// 
			this._btnGo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._btnGo.Location = new System.Drawing.Point (245, 3);
			this._btnGo.Name = "_btnGo";
			this._btnGo.Size = new System.Drawing.Size (31, 23);
			this._btnGo.TabIndex = 5;
			this._btnGo.Text = "Go";
			this._btnGo.UseVisualStyleBackColor = false;
			this._btnGo.Click += new System.EventHandler (this.button1_Click);
			// 
			// comThread
			// 
			this.comThread.WorkerReportsProgress = true;
			this.comThread.WorkerSupportsCancellation = true;
			this.comThread.DoWork += new System.ComponentModel.DoWorkEventHandler (this.comThread_DoWork);
			this.comThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler (this.comThread_RunWorkerCompleted);
			this.comThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler (this.comThread_ProgressChanged);
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point (282, 9);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size (654, 13);
			this.progressBar1.TabIndex = 6;
			this.progressBar1.Visible = false;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point (942, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size (62, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Visible = false;
			this.btnCancel.Click += new System.EventHandler (this.btnCancel_Click);
			// 
			// MemoryView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (1016, 486);
			this.Controls.Add (this.btnCancel);
			this.Controls.Add (this.progressBar1);
			this.Controls.Add (this._btnGo);
			this.Controls.Add (this._txtAddress);
			this.Controls.Add (label1);
			this.Controls.Add (this._btnNext);
			this.Controls.Add (this._btnPrev);
			this.Controls.Add (this._dumpHexa);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "MemoryView";
			this.ShowInTaskbar = false;
			this.Text = "MemoryView";
			this.Shown += new System.EventHandler (this.MemoryView_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler (this.MemoryView_FormClosing);
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

    private System.Windows.Forms.TextBox _dumpHexa;
		private System.Windows.Forms.Button _btnNext;
		private System.Windows.Forms.Button _btnPrev;
		private System.Windows.Forms.TextBox _txtAddress;
		private System.Windows.Forms.Button _btnGo;
		private System.ComponentModel.BackgroundWorker comThread;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button btnCancel;
	}
}