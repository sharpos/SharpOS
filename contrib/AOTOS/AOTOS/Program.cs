/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
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
                engine.Run(new Assembly(), "SharpOS.dll", "SharpOS.bin");
            }
            //catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
