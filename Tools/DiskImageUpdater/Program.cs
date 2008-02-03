// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Mono.GetOptions;
using Ext2;

namespace DiskImageUpdater {
	public class UpdaterOptions : Options {
		public UpdaterOptions (string [] args)
			:
			base (args)
		{

		}

		[Option ("Specify the disk.img to update", 'o', "out")]
		public string DiskImg = "disk.img";

		[Option ("Specify the SharpOS.Kernel.bin to write to the disk.img", 'i', "in")]
		public string SharpOSKernelBin = "SharpOS.Kernel.bin";

		[Option ("Has MBR", 'm', "has-mbr")]
		public bool HasMBR = false;

		[Option ("Create a MS Virtual Hard Drive file", 'v', "vhd")]
		public string OutputVHD = string.Empty;
	}

	class Program {
		static int Main (string [] args)
		{
			UpdaterOptions opts = new UpdaterOptions (args);
			bool debuggerAttached = false;

			if (opts.RemainingArguments.Length == 2) {
				Console.Error.WriteLine ("Usage: DiskImageUpdater -i <kernel> -o <disk image>");
				Console.Error.WriteLine ("Run `DiskImageUpdater -help` for more information.");
				return 1;
			}

			// Prevent a Mono error if one occurs.
			try {
				debuggerAttached = System.Diagnostics.Debugger.IsAttached;
			} catch {
			}

			if (debuggerAttached) {
				Ext2FS ext2fs = new Ext2FS ();
				ext2fs.UpdateKernel (opts.DiskImg, opts.SharpOSKernelBin, opts.HasMBR);

			} else {
				try {
					Ext2FS ext2fs = new Ext2FS ();
					ext2fs.UpdateKernel (opts.DiskImg, opts.SharpOSKernelBin, opts.HasMBR);

					Console.WriteLine (string.Format ("'{0}' has been updated.", opts.DiskImg));

				} catch (FileNotFoundException exception) {
					Console.WriteLine (string.Format ("Could not find '{0}'.", exception.FileName));
					return 1;
				} catch (Exception exception) {
					Console.WriteLine (exception.Message);
					return 1;
				}
			}

			if (opts.OutputVHD!=string.Empty)
			{
				try {
					MsVhd.CreateFromImage (opts.DiskImg, opts.OutputVHD);

					Console.WriteLine (string.Format ("'{0}' virtual disk has been created.", opts.OutputVHD));

				} catch (Exception exception) {
					Console.WriteLine (exception.Message);
					return 1;
				}
			}

			return 0;
		}
	}
}
