using System;
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.Attributes;
using SharpOS.Foundation;
using SharpOS.ADC;

namespace SharpOS.Shell.Commands.BuiltIn
{
    public unsafe static class victim
    {
        private const string name = "victim";
        private const string shortDescription = "This is a placeholder for a bug.";
        private const string lblExecute = "COMMANDS.victim.Execute";
        private const string lblGetHelp = "COMMANDS.victim.GetHelp";

        [Label( lblExecute )]
        public static void Execute( CommandExecutionContext* context )
        {

        }

        [Label( lblGetHelp )]
        public static void GetHelp( CommandExecutionContext* context )
        {

        }

        public static CommandTableEntry* CREATE( )
        {
            CommandTableEntry* entry = (CommandTableEntry*) SharpOS.ADC.MemoryManager.Allocate( (uint) sizeof( CommandTableEntry ) );

            entry->name = (CString8*) SharpOS.Stubs.CString( name );
            entry->shortDescription = (CString8*) SharpOS.Stubs.CString( shortDescription );
            entry->func_Execute = (void*) SharpOS.Stubs.GetLabelAddress( lblExecute );
            entry->func_GetHelp = (void*) SharpOS.Stubs.GetLabelAddress( lblGetHelp );

            return entry;
        }
    }
}
