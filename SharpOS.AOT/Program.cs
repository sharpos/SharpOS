/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using SharpOS;
using SharpOS.AOT.IR;
using SharpOS.AOT.X86;

namespace SharpOS
{
    public class AOTOS
    {
        static void Main(string[] args)
        {
            //try
            {
                Engine engine = new Engine();
                string filename = "SharpOS.dll";

                if (args.Length == 1)
                {
                    filename = args[0];
                }
                else if (args.Length > 0)
                {
                    Console.WriteLine("Usage: AOTOS [filename]");
                    
                    return;
                }

                if (System.IO.File.Exists(filename) == false)
                {
                    Console.WriteLine("File '" + filename + "' was not found.");

                    return;
                }

                engine.Run(new Assembly(), filename, "SharpOS.bin");
            }
            //catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
