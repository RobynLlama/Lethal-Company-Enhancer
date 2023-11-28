using HarmonyLib;

namespace Enhancer.Patches;

public static class ConfiguredValues
{
    [HarmonyPatch(typeof(StartOfRound), "Start")]
    [HarmonyPrefix]
    public static bool StartOfRoundShipStartPre()
    {
        Plugin.Log.LogInfo("StartOfRound Start");

        TimeOfDay.Instance.quotaVariables.deadlineDaysAmount = Plugin.Cfg.DaysPerQuota;

        //never skip
        return true;
    }

    [HarmonyPatch(typeof(TimeOfDay), "Start")]
    [HarmonyPostfix]
    public static void TimeOfDayOnStartPost(TimeOfDay __instance)
    {
        Plugin.Log.LogInfo("TimeOfDay Start");

        //Sets gamespeed
        __instance.globalTimeSpeedMultiplier = Plugin.Cfg.TimeScale;
    }

    [HarmonyPatch(typeof(HangarShipDoor), "Start")]
    [HarmonyPostfix]
    public static void HangarShipDoorPost(HangarShipDoor __instance)
    {
        Plugin.Log.LogInfo("HangarShipDoor Start");

        //Sets hangar door close timer
        __instance.doorPowerDuration = Plugin.Cfg.DoorTimer;
    }

    [HarmonyPatch(typeof(Terminal), "Update")]
    [HarmonyPostfix]
    public static void TerminalUpdatePost(Terminal __instance)
    {
        if (__instance.terminalUIScreen.gameObject.activeSelf && Plugin.Cfg.KeepConsoleEnabled)
            return;

        __instance.terminalUIScreen.gameObject.SetActive(true);
    }
}