// 
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Collections.Generic;
using System.Text;
using SharpOS.Foundation;
using System.Runtime.InteropServices;

namespace SharpOS.Shell.Commands
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CommandTableHeader
    {
        public int count;
        public CommandTableEntry* firstEntry;

        public CommandTableEntry* FindCommand(CString8* name)
        {
            Diagnostics.Assert(name != null, "CommandTableHeader::FindCommand(CString8*): Parameter 'name' is null");
            Diagnostics.Assert(firstEntry != null, "CommandTableHeader::FindCommand(CString8*): Cannot search through commands; command list is empty");
            CommandTableEntry* currentEntry;
            for (currentEntry = firstEntry;
                currentEntry != null;
                currentEntry = currentEntry->nextEntry)
            {
                //FIXME: This needs to be case insensitive!
                if (ByteString.Compare(name->Pointer, currentEntry->name->Pointer) == 0)
                    return currentEntry;
                //else
                //{
                //    ADC.TextMode.Write("CommandTableHeader::FindCommand(CString8*): Entered '");
                //    ADC.TextMode.Write(name);
                //    ADC.TextMode.Write("' != '");
                //    ADC.TextMode.Write(currentEntry->name);
                //    ADC.TextMode.WriteLine("'");
                //}
            }

            return null;
        }

        internal static void DisplayCommandEntryDebug(CommandTableEntry* entry)
        {
            const int tempBufferSIZE = 30;
            byte* tempBuffer = stackalloc byte[tempBufferSIZE];

            ADC.TextMode.WriteLine("Dumping CommandTableEntry:");
            if (entry == null)
            {
                ADC.TextMode.Write(" Pointer Address: ");
                Convert.ToString((int)entry, true, tempBuffer, tempBufferSIZE, 0);
                ADC.TextMode.Write(tempBuffer);
                ADC.TextMode.WriteLine(" (NULL!)");
            }
            else
            {
                ADC.TextMode.Write(" Pointer Address: ");
                Convert.ToString((int)entry, true, tempBuffer, tempBufferSIZE, 0);
                ADC.TextMode.WriteLine(tempBuffer);
            }

            ADC.TextMode.Write(" Name: \"");
            ADC.TextMode.Write(entry->name);
            ADC.TextMode.Write("\" @ ");
            Convert.ToString((int)entry->name, true, tempBuffer, tempBufferSIZE, 0);
            ADC.TextMode.WriteLine(tempBuffer);

            ADC.TextMode.Write(" Description: \"");
            ADC.TextMode.Write(entry->shortDescription);
            ADC.TextMode.Write("\" @ ");
            Convert.ToString((int)entry->shortDescription, true, tempBuffer, tempBufferSIZE, 0);
            ADC.TextMode.WriteLine(tempBuffer);

            ADC.TextMode.Write(" Execute() @ ");
            Convert.ToString((int)entry->func_Execute, true, tempBuffer, tempBufferSIZE, 0);
            ADC.TextMode.Write(tempBuffer);
            ADC.TextMode.Write("; GetHelp() @ ");
            Convert.ToString((int)entry->func_GetHelp, true, tempBuffer, tempBufferSIZE, 0);
            ADC.TextMode.WriteLine(tempBuffer);

            ADC.TextMode.Write(" nextEntry @ ");
            Convert.ToString((int)entry->nextEntry, true, tempBuffer, tempBufferSIZE, 0);
            ADC.TextMode.WriteLine(tempBuffer);
        }

        public void AddEntry(CommandTableEntry* entry)
        {
            if (entry == null)
                Diagnostics.Message("CommandTableHeader::AddEntry(CommandTableEntry*): Parameter 'entry' is null");

            if (firstEntry == null)
            {

                firstEntry = entry;
                entry->nextEntry = null;
                return;
            }
            else
            {
                CommandTableEntry* currentEntry = null;
                for (currentEntry = firstEntry;
                    currentEntry->nextEntry != null;
                    currentEntry = currentEntry->nextEntry)
                { }
                currentEntry->nextEntry = entry;
                entry->nextEntry = null;

                //CommandTableEntry* currentEntry = this.firstEntry;
                //for ( ;
                //    currentEntry->nextEntry != null || currentEntry->nextEntry->name->Compare(currentEntry->name) > 0;
                //    currentEntry = currentEntry->nextEntry)
                //{
                //}
                //if (currentEntry->nextEntry == null)
                //{
                //    currentEntry->nextEntry = entry;
                //    entry->nextEntry = null;
                //}
                //else
                //{
                //    CommandTableEntry* nextEntry = currentEntry->nextEntry;
                //    currentEntry->nextEntry = entry;
                //    entry->nextEntry = nextEntry;
                //}

                return;
            }
        }

        public void DISPOSE()
        {
            CommandTableEntry* curEntry = this.firstEntry;
            while (curEntry != null)
            {
                CommandTableEntry* nEntry = curEntry->nextEntry;

                curEntry->DISPOSE();
                SharpOS.ADC.MemoryManager.Free(curEntry);

                curEntry = nEntry;
            }
        }

        public static CommandTableHeader* GenerateDefault()
        {
            CommandTableHeader* header = (CommandTableHeader*)SharpOS.ADC.MemoryManager.Allocate((uint)sizeof(CommandTableHeader));

            //FIXME: For some reason, the first command on the list doesn't register properly
            //(NOTE: So don't put anything important there yet...)
            header->AddEntry(BuiltIn.halt.CREATE());
            header->AddEntry(BuiltIn.cls.CREATE());
            header->AddEntry(BuiltIn.commands.CREATE());
            header->AddEntry(BuiltIn.help.CREATE());
            header->AddEntry(BuiltIn.version.CREATE());

            return header;
        }
    }
}