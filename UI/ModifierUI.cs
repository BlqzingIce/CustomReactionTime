using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using CustomReactionTime.Modes;

namespace CustomReactionTime.Settings
{
    public class ModifierUI : NotifiableSingleton<ModifierUI>
    {
        //enable setting
        [UIValue("enabled")]
        private bool modEnabled
        {
            get => Configuration.PluginConfig.Instance.Enabled;
            set
            {
                Configuration.PluginConfig.Instance.Enabled = value;
            }
        }
        [UIAction("setEnabled")]
        void SetEnabled(bool value)
        {
            modEnabled = value;
        }

        //mode list setting
        [UIValue("modes")]
        public List<object> Modes = Enum.GetValues(typeof(Mode)).Cast<object>().ToList();

        [UIValue("mode_value")]
        public Mode SelectedHeightUnit
        {
            get => Configuration.PluginConfig.Instance.Mode;
            set
            {
                Configuration.PluginConfig.Instance.Mode = value;
            }
        }

        //slider min/max + format
        [UIValue("min_slider")]
        private float Min_Slider => Configuration.PluginConfig.Instance.SliderMin;

        [UIValue("max_slider")]
        private float Max_Slider => Configuration.PluginConfig.Instance.SliderMax;

        [UIAction("rt_slider_formatter")]
        private string RT_Slider_Formatter(float value) => value.ToString("0") + " ms";

        //rt slider setting
        [UIComponent("rt_slider")]
        private SliderSetting RT_Slider;

        [UIValue("rt_value")]
        private float RT_Value
        {
            get => Configuration.PluginConfig.Instance.ReactionTime;
            set
            {
                Configuration.PluginConfig.Instance.ReactionTime = value;
            }              
        }

        [UIAction("set_rt_value")]
        private void Set_RT_Value(float value)
        {
            RT_Value = value;
        }
        
        //lower rt slider setting
        [UIComponent("lower_slider")]
        private SliderSetting Lower_Slider;

        [UIValue("lower_value")]
        private float Lower_Value
        {
            get => Configuration.PluginConfig.Instance.LowerThreshold;
            set
            {
                Configuration.PluginConfig.Instance.LowerThreshold = value;
            }
        }

        [UIAction("set_lower_value")]
        private void Set_Lower_Value(float value)
        {
            Lower_Value = value;
        }

        //upper rt slider setting
        [UIComponent("upper_slider")]
        private SliderSetting Upper_Slider;

        [UIValue("upper_value")]
        private float Upper_Value
        {
            get => Configuration.PluginConfig.Instance.UpperThreshold;
            set
            {
                Configuration.PluginConfig.Instance.UpperThreshold = value;
            }
        }

        [UIAction("set_upper_value")]
        private void Set_Upper_Value(float value)
        {
            Upper_Value = value;
        }
    }
}