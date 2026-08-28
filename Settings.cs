#nullable disable
using Il2CppNewtonsoft.Json.Linq;
using ModSettings;
using System.ComponentModel;

namespace BaltaTweaks
{
    internal class BaltaTweaksSettings : JsonModSettings
    {
        public static BaltaTweaksSettings Instance { get; private set; }

        [Section("Components")]
        [Name("Block Book Stacking")]
        [ModSettings.Description("If enabled, prevents books to merge into a stack and become a generic book.")]
        public bool PersistentBookEnabled = true;

        [Name("No Auto Close Doors")]
        [ModSettings.Description("If enabled, doors will no longer close themselfs.")]
        public bool NoAutoCloseDoorEnabled = true;

        protected override void OnConfirm()
        {
            base.OnConfirm();
            Save();
            MelonLoader.MelonLogger.Msg("[BaltaTweaks] Settings saved.");
        }

        public static void OnLoad()
        {
            Instance = new BaltaTweaksSettings();
            Instance.AddToModSettings("BaltaTweaks");
        }
    }
}