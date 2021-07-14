using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace PermadeathEnforcementSuite
{
    [StaticConstructorOnStartup]
    public static class LetterSaver
    {

        [HarmonyPatch(typeof(LetterStack), nameof(LetterStack.ReceiveLetter), new Type[] { typeof(Letter), typeof(string)})]
        public static class Patch_LetterStack_ReceiveLetter {
            public static void Postfix(Letter let, string debugInfo = null) {
                if (Current.Game.Info.permadeathMode) {
                    Log.Message("[PES] Autosaving...");
                    Current.Game.autosaver.DoAutosave();
                    Current.Game.autosaver.DoAutosave();
                }
            }
        }

        static LetterSaver() {
            Log.Message("[PES] Okay, showtime!");          
            Harmony har = new Harmony("PermadeathEnforcementSuite");
            har.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
