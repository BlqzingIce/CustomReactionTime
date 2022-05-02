using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPALogger = IPA.Logging.Logger;

namespace CustomReactionTime
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static Harmony harmony;

        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
        }


        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
        }

        [OnStart]
        public void OnApplicationStart()
        {
            harmony = new Harmony("com.BlqzingIce.BeatSaber.CustomReactionTime");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("CustomReactionTime", "CustomReactionTime.UI.settings.bsml", Settings.ModifierUI.instance);
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            harmony.UnpatchSelf();
        }
    }
}