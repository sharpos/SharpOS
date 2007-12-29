

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.Attributes;
using SharpOS.Foundation;
using SharpOS.ADC;

namespace SharpOS.Shell.Commands.BuiltIn
{
    public unsafe static class reboot
    {
        public const string name = "reboot";
        public const string shortDescription = "Restarts the machine";
        public const string lblExecute = "COMMANDS.reboot.Execute";
        public const string lblGetHelp = "COMMANDS.reboot.GetHelp";

        [Label( lblExecute )]
        public static void Execute( CommandExecutionContext* context )
        {
            Kernel.Reboot( );
        }

        [Label( lblGetHelp )]
        public static void GetHelp( CommandExecutionContext* context )
        {
            TextMode.WriteLine( "Syntax: " );
            TextMode.WriteLine( "     reboot" );
            TextMode.WriteLine( "" );
            TextMode.WriteLine( "Restarts the machine." );
        }

        public static CommandTableEntry* CREATE( )
        {
            CommandTableEntry* entry = (CommandTableEntry*) SharpOS.ADC.MemoryManager.Allocate( (uint) sizeof( CommandTableEntry ) );

            entry->name = (CString8*) SharpOS.Stubs.CString( name );
            entry->shortDescription = (CString8*) SharpOS.Stubs.CString( shortDescription );
            entry->func_Execute = (void*) SharpOS.Stubs.GetLabelAddress( lblExecute );
            entry->func_GetHelp = (void*) SharpOS.Stubs.GetLabelAddress( lblGetHelp );
            entry->nextEntry = null;

            return entry;
        }
    }
}
