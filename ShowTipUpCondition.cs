#nullable disable
using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace BaltaTweaks
{
    internal static class TipupConditionPatches
    {
        private const string tipUpName = "GEAR_TipUp";
        private const float lowConditionThresholdPercent = 10f;

        private static UILabel conditionLabel;
        private static bool initialized = false;

        [HarmonyPatch(typeof(Panel_IceFishing), nameof(Panel_IceFishing.Initialize))]
        private static class Panel_IceFishing_Initialize_Patch
        {
            private static void Postfix(Panel_IceFishing __instance)
            {
                if (initialized)
                {
                    return;
                }
                initialized = true;

                GameObject sourceLabelObject = __instance.m_HoursToFishLabel.gameObject;
                GameObject conditionLabelObject = NGUITools.AddChild(__instance.m_TipupFishingParent, sourceLabelObject);
                conditionLabelObject.name = "TipupConditionLabel";

                conditionLabel = conditionLabelObject.GetComponent<UILabel>();

                conditionLabel.leftAnchor.target = null;
                conditionLabel.rightAnchor.target = null;
                conditionLabel.bottomAnchor.target = null;
                conditionLabel.topAnchor.target = null;

                conditionLabel.transform.localPosition = new Vector3(-140f, -101f, 0f);
                conditionLabel.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                conditionLabel.pivot = UIWidget.Pivot.Center;

                conditionLabel.width = 200;
                conditionLabel.height = 40;
                conditionLabel.depth = 50;
                conditionLabel.alpha = 1f;
                conditionLabel.color = Color.white;
                conditionLabel.text = string.Empty;
            }
        }

        private static void RefreshConditionLabel()
        {
            if (conditionLabel == null)
            {
                return;
            }

            if (!BaltaTweaksSettings.Instance.ShowTipupConditionEnabled)
            {
                conditionLabel.text = string.Empty;
                return;
            }

            GearItem bestTipup = GameManager.GetInventoryComponent().GetHighestConditionGearThatMatchesName(tipUpName);
            if (bestTipup == null)
            {
                conditionLabel.text = string.Empty;
                return;
            }

            int conditionPercent = bestTipup.GetRoundedCondition();

            conditionLabel.text = conditionPercent + "%";
            conditionLabel.color = (conditionPercent <= lowConditionThresholdPercent) ? Color.red : Color.white;
        }

        [HarmonyPatch(typeof(Panel_IceFishing), nameof(Panel_IceFishing.OnShowPlaceTipup))]
        private static class Panel_IceFishing_OnShowPlaceTipup_Patch
        {
            private static void Postfix()
            {
                RefreshConditionLabel();
            }
        }

        [HarmonyPatch(typeof(Panel_IceFishing), nameof(Panel_IceFishing.Enable), new Type[] { typeof(bool) })]
        private static class Panel_IceFishing_Enable_Patch
        {
            private static void Postfix(bool enable)
            {
                if (enable)
                {
                    RefreshConditionLabel();
                }
            }
        }
    }
}