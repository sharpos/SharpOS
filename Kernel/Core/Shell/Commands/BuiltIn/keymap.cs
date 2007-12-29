// 
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.Attributes;
using SharpOS.Foundation;
using SharpOS.ADC;

namespace SharpOS.Shell.Commands.BuiltIn
{
    public unsafe static class keymap
    {
        public const string name = "keymap";
        public const string shortDescription = "Lists keymaps and changes the active one";
        public const string lblExecute = "COMMANDS.keymap.Execute";
        public const string lblGetHelp = "COMMANDS.keymap.GetHelp";

        [Label( lblExecute )]
        public static void Execute( CommandExecutionContext* context )
        {

            if( context->parameters->Length == 0 )
                TextMode.WriteLine( "Current keymap command is not supported yet." );
            if( context->parameters->Compare( "--list" ) == 0 )
                TextMode.WriteLine( "--list command is not supported yet." );
            else
                KeyMap.SetKeyMap( context->parameters );

            return;
        }

        [Label( lblGetHelp )]
        public static void GetHelp( CommandExecutionContext* context )
        {
            TextMode.WriteLine( "Syntax: " );
            TextMode.WriteLine( "     keymap : shows the active keymap." );
            TextMode.WriteLine( "     keymap --list : shows all the installed keymaps." );
            TextMode.WriteLine( "     keymap <keymap> : sets the keymap to <keymap>." );
            TextMode.WriteLine( CommandTableHeader.inform_USE_HELP_COMMANDS );
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
