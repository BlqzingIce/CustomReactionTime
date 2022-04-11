using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using CustomReactionTime.Modes;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace CustomReactionTime.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual bool Enabled { get; set; } = true;
        public virtual Mode Mode { get; set; } = Mode.UseReactionTime;
        public virtual float ReactionTime { get; set; } = 500;
        public virtual float LowerThreshold { get; set; } = 400;
        public virtual float UpperThreshold { get; set; } = 1000;
        public virtual float SliderMin { get; set; } = 100;
        public virtual float SliderMax { get; set; } = 1500;
    }
}