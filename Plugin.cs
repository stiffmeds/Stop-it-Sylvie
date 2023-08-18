using BepInEx;
using HarmonyLib;
using System.Collections.Generic;

namespace Stop_it_Sylvie
{
    [BepInPlugin("com.meds.stopitsylvie", "Stop it, Sylvie!", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new("com.meds.stopitsylvie");
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Sylvie better stop it with those sounds.");
            harmony.PatchAll();
        }
    }
    [HarmonyPatch]
    public class StopHerPls
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Globals), "CreateGameContent")]
        public static void CreateGameContentPostfix()
        {
            Dictionary<string, SubClassData> medsSubClassesSource = Traverse.Create(Globals.Instance).Field("_SubClassSource").GetValue<Dictionary<string, SubClassData>>();
            Dictionary<string, SubClassData> medsSubClasses = Traverse.Create(Globals.Instance).Field("_SubClass").GetValue<Dictionary<string, SubClassData>>();
            medsSubClassesSource["archer"].HitSound = medsSubClassesSource["priest"].HitSound;
            medsSubClasses["archer"].HitSound = medsSubClasses["priest"].HitSound;
            Traverse.Create(Globals.Instance).Field("_SubClassSource").SetValue(medsSubClassesSource);
            Traverse.Create(Globals.Instance).Field("_SubClass").SetValue(medsSubClasses);
        }
    }
}
