using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using CustomReactionTime.Modes;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace CustomReactionTime.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual Mode Mode { get; set; } = Mode.Disabled;
        public virtual float ReactionTime { get; set; } = 500;
        public virtual float LowerThreshold { get; set; } = 400;
        public virtual float UpperThreshold { get; set; } = 1000;
        public virtual Threshold LowerSetting { get; set; } = Threshold.Threshold;
        public virtual Threshold UpperSetting { get; set; } = Threshold.ReactionTime;
        public virtual float SliderMin { get; set; } = 300;
        public virtual float SliderMax { get; set; } = 1000;
    }
}