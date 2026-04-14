using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Assets.Scripts.Saves___Serialization.Progression.Achievements;
using Assets.Scripts.Saves___Serialization.Progression.Unlocks;

namespace UnlockEverything
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        internal static ManualLogSource Logger;
        internal static ConfigEntry<bool> unlockAllEnabled;
        internal static ConfigEntry<bool> debugLogging;

        public override void Load()
        {
            Logger = Log;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loading!");

            unlockAllEnabled = Config.Bind(
                "General",
                "UnlockAllEnabled",
                true,
                "Enable/disable unlocking all content");

            debugLogging = Config.Bind(
                "Debug",
                "DebugLogging",
                false,
                "Enable debug logging for unlock checks");

            try
            {
                Harmony.CreateAndPatchAll(typeof(UnlockPatches));
                Logger.LogInfo($"Successfully patched unlock methods!");
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"Failed to apply patches: {ex.Message}");
                Logger.LogError($"Stack trace: {ex.StackTrace}");
            }

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private static class UnlockPatches
        {
            // Patch IsPurchased
            [HarmonyPostfix]  // ✅ Postfix — same reasoning
            [HarmonyPatch(typeof(MyAchievements), nameof(MyAchievements.IsPurchased))]
            public static void IsPurchased_Postfix(UnlockableBase unlockable, ref bool __result)
            {
                if (!unlockAllEnabled.Value) return;
                __result = true;
            }

            // Patch IsAchievementDone
            [HarmonyPostfix]
            [HarmonyPatch(typeof(MyAchievements), "IsAchievementDone", typeof(string))]
            public static void IsAchievementDone_Postfix(string achName, ref bool __result)
            {
                if (!unlockAllEnabled.Value) return;

                if (debugLogging.Value && !string.IsNullOrEmpty(achName))
                    Logger.LogInfo($"Unlocking achievement: {achName}");

                __result = true;
            }

            // Patch character selection screen — only unblack, never re-black
            [HarmonyPostfix]
            [HarmonyPatch(typeof(CharacterData), "IsBlackedOutInCharacterSelectionScreen")]
            public static void IsBlackedOutInCharacterSelectionScreen_Postfix(ref bool __result)
            {
                if (!unlockAllEnabled.Value) return;
                __result = false;  // false = not blacked out = visible ✅
            }

            // Patch IsUnlocked
            [HarmonyPostfix]
            [HarmonyPatch(typeof(MyAchievements), "IsUnlocked", typeof(MyAchievement))]
            public static void IsUnlocked_MyAchievement_Postfix(MyAchievement myAchievement, ref bool __result)
            {
                if (!unlockAllEnabled.Value) return;
                __result = true;
            }

            // ✅ REMOVED: CanBuy and CanShow patches
            // These are likely what the in-game toggle hooks into to control
            // item pool inclusion during runs. By not patching them, the
            // in-game toggle can function normally.
        }
    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.megabonk.unlockeverything";
        public const string PLUGIN_NAME = "Unlock Everything";
        public const string PLUGIN_VERSION = "1.0.0";
    }
}