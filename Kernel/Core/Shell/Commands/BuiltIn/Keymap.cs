//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Jonathan Dickinson <jonathand.za@gmail.com>
//  William Lahti <xfurious@gmail.com>
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

namespace SharpOS.Shell.Commands.BuiltIn {
    public unsafe static class keymap {
        public const string name = "keymap";
        public const string shortDescription = "Lists keymaps and changes the active one";
        public const string lblExecute = "COMMANDS.keymap.Execute";
        public const string lblGetHelp = "COMMANDS.keymap.GetHelp";

        [Label (lblExecute)]
        public static void Execute(CommandExecutionContext* context) {
            byte* rawbuf = stackalloc byte[Kernel.MaxKeyMapNameLength];

            if (context->parameters->Compare ("--list") == 0) {
                ListKeyMaps ();
            } else if (context->parameters->Compare (0, "--set ", 0, 6) == 0 &&
                   context->parameters->Length > 6) {
                PString8* buf = PString8.Wrap (rawbuf,
                    Kernel.MaxKeyMapNameLength);
                buf->Clear ();
                TextMode.Write (context->parameters->Length);
                TextMode.WriteLine ();
                buf->Concat (context->parameters, 6, context->parameters->Length - 6);

                TextMode.Write ("Setting key map to `");
                TextMode.Write (buf);
                TextMode.Write ("'");
                KeyMap.SetKeyMap (buf);
            } else if (context->parameters->Length == 0) {
                TextMode.Write ("Current key map: ");
                TextMode.WriteLine (KeyMap.GetCurrentKeyMapName ());
            } else {
                TextMode.WriteLine ("Usage: keymap [--list|--set NAME]");
            }
        }

        public static void ListKeyMaps() {
            int count = KeyMap.GetBuiltinKeyMapsCount ();
            byte* str = stackalloc byte[Kernel.MaxKeyMapNameLength];
            int error = 0;
            byte* keymap = null;
            int len = 0;

            TextMode.Write (count);
            TextMode.WriteLine (" available keymaps:");

            for (int x = 0; x < count; ++x) {
                keymap = (byte*) KeyMap.GetBuiltinKeyMap (x);
                len = BinaryTool.ReadPrefixedString (keymap, str,
                        Kernel.MaxKeyMapNameLength, &error);

                TextMode.Write (" ");
                TextMode.WriteLine (str);
            }
        }

        [Label (lblGetHelp)]
        public static void GetHelp(CommandExecutionContext* context) {
            TextMode.WriteLine ("Syntax: ");
            TextMode.WriteLine ("     keymap : shows the active keymap.");
            TextMode.WriteLine ("     keymap --list : shows all the installed keymaps.");
            TextMode.WriteLine ("     keymap <keymap> : sets the keymap to <keymap>.");
            TextMode.WriteLine (CommandTableHeader.inform_USE_HELP_COMMANDS);
        }

        public static CommandTableEntry* CREATE() {
            CommandTableEntry* entry = (CommandTableEntry*) SharpOS.ADC.MemoryManager.Allocate ((uint) sizeof (CommandTableEntry));

            entry->name = (CString8*) SharpOS.Stubs.CString (name);
            entry->shortDescription = (CString8*) SharpOS.Stubs.CString (shortDescription);
            entry->func_Execute = (void*) SharpOS.Stubs.GetLabelAddress (lblExecute);
            entry->func_GetHelp = (void*) SharpOS.Stubs.GetLabelAddress (lblGetHelp);

            return entry;
        }
    }
}