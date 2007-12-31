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

namespace Ext2 {
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
	}

	class Program {
		static int Main (string [] args)
		{
			UpdaterOptions opts = new UpdaterOptions (args);

			if (opts.RemainingArguments.Length == 2) {
				Console.Error.WriteLine ("Usage: DiskImageUpdater -i <kernel> -o <disk image>");
				Console.Error.WriteLine ("Run `DiskImageUpdater -help` for more information.");
				return 1;
			}

			try {
				Ext2FS ext2fs = new Ext2FS ();
				ext2fs.UpdateKernel (opts.DiskImg, opts.SharpOSKernelBin);

				Console.WriteLine (string.Format ("'{0}' has been updated.", opts.DiskImg));

			} catch (FileNotFoundException exception) {
				Console.WriteLine (string.Format ("Could not find '{0}'.", exception.FileName));
				return 1;
			} catch (Exception exception) {
				Console.WriteLine (exception.Message);
				return 1;
			}

			return 0;
		}
	}
}
