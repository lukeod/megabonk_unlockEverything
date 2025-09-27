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
        private static ConfigEntry<bool> unlockAllEnabled;
        private static ConfigEntry<bool> debugLogging;

        public override void Load()
        {
            // Initialize logger
            Logger = Log;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loading!");

            // Configuration
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

            // Apply Harmony patches
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
            // Patch IsAvailable to always return true when mod is enabled
            [HarmonyPrefix]
            [HarmonyPatch(typeof(MyAchievements), nameof(MyAchievements.IsAvailable))]
            public static bool IsAvailable_Prefix(UnlockableBase unlockable, ref bool __result)
            {
                if (!unlockAllEnabled.Value)
                    return true; // Run original method

                if (debugLogging.Value && unlockable != null)
                {
                    try
                    {
                        var name = unlockable.GetName();
                        Logger.LogInfo($"Unlocking: {name}");
                    }
                    catch
                    {
                        // Ignore any errors getting the name
                    }
                }

                __result = true; // Everything is available!
                return false; // Skip original method
            }

            // Also patch IsPurchased to ensure everything appears as purchased
            [HarmonyPrefix]
            [HarmonyPatch(typeof(MyAchievements), nameof(MyAchievements.IsPurchased))]
            public static bool IsPurchased_Prefix(UnlockableBase unlockable, ref bool __result)
            {
                if (!unlockAllEnabled.Value)
                    return true; // Run original method

                __result = true; // Everything is purchased!
                return false; // Skip original method
            }

            // Also patch the achievement check method - it takes a string parameter
            [HarmonyPrefix]
            [HarmonyPatch(typeof(MyAchievements), "IsAchievementDone", typeof(string))]
            public static bool IsAchievementDone_Prefix(string achName, ref bool __result)
            {
                if (!unlockAllEnabled.Value)
                    return true; // Run original method

                if (debugLogging.Value && !string.IsNullOrEmpty(achName))
                {
                    Logger.LogInfo($"Unlocking achievement: {achName}");
                }

                __result = true; // All achievements are done!
                return false; // Skip original method
            }

            // Patch character selection UI to show all characters as unlocked
            [HarmonyPrefix]
            [HarmonyPatch(typeof(CharacterData), "IsBlackedOutInCharacterSelectionScreen")]
            public static bool IsBlackedOutInCharacterSelectionScreen_Prefix(ref bool __result)
            {
                if (!unlockAllEnabled.Value)
                    return true; // Run original method

                __result = false; // Nothing is blacked out!
                return false; // Skip original method
            }

            // Additional patches for other potential unlock checks
            [HarmonyPrefix]
            [HarmonyPatch(typeof(MyAchievements), "IsUnlocked", typeof(MyAchievement))]
            public static bool IsUnlocked_MyAchievement_Prefix(MyAchievement myAchievement, ref bool __result)
            {
                if (!unlockAllEnabled.Value)
                    return true; // Run original method

                __result = true; // Everything is unlocked!
                return false; // Skip original method
            }

            // Patch CanBuy to always allow purchases (for free unlocks)
            [HarmonyPrefix]
            [HarmonyPatch(typeof(UnlockableBase), "CanBuy")]
            public static bool CanBuy_Prefix(ref bool __result)
            {
                if (!unlockAllEnabled.Value)
                    return true; // Run original method

                __result = true; // Everything can be bought!
                return false; // Skip original method
            }

            // Patch challenge visibility check
            [HarmonyPrefix]
            [HarmonyPatch(typeof(ChallengeData), "CanShow")]
            public static bool CanShow_Prefix(ref bool __result)
            {
                if (!unlockAllEnabled.Value)
                    return true; // Run original method

                __result = true; // All challenges are visible!
                return false; // Skip original method
            }
        }
    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.megabonk.unlockeverything";
        public const string PLUGIN_NAME = "Unlock Everything";
        public const string PLUGIN_VERSION = "1.0.0";
    }
}