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
	partial class UnitTestsView
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
			System.Windows.Forms.ColumnHeader columnHeader1;
			System.Windows.Forms.ColumnHeader columnHeader2;
			this.comThread = new System.ComponentModel.BackgroundWorker ();
			this.listView1 = new System.Windows.Forms.ListView ();
			this.progressBar1 = new System.Windows.Forms.ProgressBar ();
			this.label1 = new System.Windows.Forms.Label ();
			this.label2 = new System.Windows.Forms.Label ();
			this.btnCancel = new System.Windows.Forms.Button ();
			columnHeader1 = new System.Windows.Forms.ColumnHeader ();
			columnHeader2 = new System.Windows.Forms.ColumnHeader ();
			this.SuspendLayout ();
			// 
			// columnHeader1
			// 
			columnHeader1.Text = "Name";
			columnHeader1.Width = 363;
			// 
			// columnHeader2
			// 
			columnHeader2.Text = "Result";
			columnHeader2.Width = 73;
			// 
			// comThread
			// 
			this.comThread.WorkerReportsProgress = true;
			this.comThread.WorkerSupportsCancellation = true;
			this.comThread.DoWork += new System.ComponentModel.DoWorkEventHandler (this.comThread_DoWork);
			this.comThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler (this.comThread_RunWorkerCompleted);
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange (new System.Windows.Forms.ColumnHeader [] {
            columnHeader1,
            columnHeader2});
			this.listView1.Location = new System.Drawing.Point (12, 41);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size (459, 291);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point (113, 17);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size (298, 13);
			this.progressBar1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point (12, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size (95, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Gathering results...";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point (12, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size (35, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "label2";
			this.label2.Visible = false;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point (417, 12);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size (54, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler (this.btnCancel_Click);
			// 
			// UnitTestsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (483, 344);
			this.Controls.Add (this.btnCancel);
			this.Controls.Add (this.label2);
			this.Controls.Add (this.label1);
			this.Controls.Add (this.progressBar1);
			this.Controls.Add (this.listView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "UnitTestsView";
			this.ShowInTaskbar = false;
			this.Text = "Unit Tests";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler (this.UnitTestsView_FormClosing);
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private System.ComponentModel.BackgroundWorker comThread;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCancel;
	}
}