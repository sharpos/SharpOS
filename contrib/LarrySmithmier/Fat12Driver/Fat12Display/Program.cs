/* Fat12 Console application
 * Copyright (C) 2006,2007 Larry Smithmier
 * email: larry@smithmier.com
 * 
 * This program is free software; you can redistribute it and/or modify it under 
 * the terms of the GNU General Public License as published by the Free Software 
 * Foundation; either version 2 of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY 
 * WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this 
 * program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, 
 * Suite 330, Boston, MA 02111-1307 USA
 * 
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Fat12Driver;

namespace Fat12Display
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"E:\SharpOS\Floppy1.IMA";
            if(args.Length > 0)
            {
                filePath = args[0];
            }
            Fat12Disk disk = new Fat12Disk(filePath);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(disk.ToString());
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
