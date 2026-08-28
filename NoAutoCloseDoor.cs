#nullable disable
using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace BaltaTweaks
{
    [HarmonyPatch(typeof(OpenCloseTrigger), nameof(OpenCloseTrigger.OnTriggerExit), new Type[] { typeof(Collider) })]
    public static class OpenCloseTrigger_OnTriggerExit_Patch
    {
        static bool Prefix(OpenCloseTrigger __instance, Collider other)
        {
            if (!BaltaTweaksSettings.Instance.NoAutoCloseDoorEnabled)
                return true;

            return false;
        }
    }

}