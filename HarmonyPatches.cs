using HarmonyLib;
using CustomReactionTime.Modes;
using CustomReactionTime.Utils;

namespace CustomReactionTime.HarmonyPatches
{
    [HarmonyPatch(typeof(BeatmapObjectSpawnMovementData), "Init")]
    internal class SpawnMovementDataUpdatePatch
    {
        public static void Prefix(ref float startNoteJumpMovementSpeed, float startBpm, ref BeatmapObjectSpawnMovementData.NoteJumpValueType noteJumpValueType, ref float noteJumpValue)
        {
            switch (Configuration.PluginConfig.Instance.Mode)
            {
                case Mode.Disabled:
                    Plugin.Log.Info("Mode: Disabled");
                    Plugin.Log.Info("Offset: " + (ReactionTimeUtils.CalculateReactionTime(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm) * 1000).ToString() + " ms");
                    Plugin.Log.Info("Jump Distance: " + ReactionTimeUtils.CalculateJumpDistance(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm).ToString() + " m");
                    break;

                case Mode.UseReactionTime:
                    Plugin.Log.Info("Mode: UseReactionTime");
                    Plugin.Log.Info("Offset: " + (ReactionTimeUtils.CalculateReactionTime(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm) * 1000).ToString() + " ms");
                    Plugin.Log.Info("Jump Distance: " + ReactionTimeUtils.CalculateJumpDistance(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm).ToString() + " m");

                    noteJumpValueType = BeatmapObjectSpawnMovementData.NoteJumpValueType.JumpDuration;
                    noteJumpValue = Configuration.PluginConfig.Instance.ReactionTime / 1000f;

                    Plugin.Log.Info("Offset set to: " + (noteJumpValue * 1000).ToString() + " ms");
                    Plugin.Log.Info("Jump Distance set to: " + ReactionTimeUtils.CalculateJumpDistance(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm).ToString() + " m");
                    break;

                case Mode.UseThresholds:
                    Plugin.Log.Info("Mode: UseThresholds");
                    float reactionTime = ReactionTimeUtils.CalculateReactionTime(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm) * 1000;
                    float jumpDistance = ReactionTimeUtils.CalculateJumpDistance(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm);
                    Plugin.Log.Info("Offset: " + reactionTime.ToString() + " ms");
                    Plugin.Log.Info("Jump Distance: " + jumpDistance.ToString() + " m");

                    if (reactionTime < Configuration.PluginConfig.Instance.LowerThreshold)
                    {
                        switch (Configuration.PluginConfig.Instance.LowerSetting)
                        {
                            case Threshold.ReactionTime:
                                reactionTime = Configuration.PluginConfig.Instance.ReactionTime;
                                break;

                            case Threshold.Threshold:
                                reactionTime = Configuration.PluginConfig.Instance.LowerThreshold;
                                break;

                            case Threshold.None:
                                break;

                            default:
                                Plugin.Log.Error("How did you manage to break your lower theshold setting??");
                                break;
                        }
                    }
                    else if (reactionTime > Configuration.PluginConfig.Instance.UpperThreshold)
                    {
                        switch (Configuration.PluginConfig.Instance.UpperSetting)
                        {
                            case Threshold.ReactionTime:
                                reactionTime = Configuration.PluginConfig.Instance.ReactionTime;
                                break;

                            case Threshold.Threshold:
                                reactionTime = Configuration.PluginConfig.Instance.UpperThreshold;
                                break;

                            case Threshold.None:
                                break;

                            default:
                                Plugin.Log.Error("How did you manage to break your upper theshold setting??");
                                break;
                        }
                    }

                    noteJumpValueType = BeatmapObjectSpawnMovementData.NoteJumpValueType.JumpDuration;
                    noteJumpValue = reactionTime / 1000f;

                    Plugin.Log.Info("Offset set to: " + (noteJumpValue * 1000).ToString() + " ms");
                    Plugin.Log.Info("Jump Distance set to: " + ReactionTimeUtils.CalculateJumpDistance(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm).ToString() + " m");
                    break;

                default:
                    Plugin.Log.Error("How did you manage to break your mode setting??");
                    break;
            }

            if (Configuration.PluginConfig.Instance.MinJDEnabled)
            {
                if (ReactionTimeUtils.CalculateJumpDistance(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm) < Configuration.PluginConfig.Instance.MinimumJumpDistance)
                {
                    noteJumpValueType = BeatmapObjectSpawnMovementData.NoteJumpValueType.BeatOffset;

                    //shamelessly stolen from jdfixer
                    float numCurr = 60f / startBpm;
                    float num2Curr = 4f;

                    while (startNoteJumpMovementSpeed * numCurr * num2Curr > 17.999)
                        num2Curr /= 2f;

                    if (num2Curr < 0.25f)
                        num2Curr = 0.25f;

                    float jumpDurCurr = num2Curr * numCurr * 2f;
                    float jumpDisCurr = startNoteJumpMovementSpeed * jumpDurCurr;

                    float desiredJumpDur = Configuration.PluginConfig.Instance.MinimumJumpDistance / startNoteJumpMovementSpeed;
                    float desiredHalfJumpDur = desiredJumpDur / 2f / num2Curr;
                    float jumpDurMul = desiredJumpDur / jumpDurCurr;

                    noteJumpValue = (num2Curr * jumpDurMul) - num2Curr;
                    Plugin.Log.Info("Jump Distance raised to: " + ReactionTimeUtils.CalculateJumpDistance(noteJumpValueType, noteJumpValue, startNoteJumpMovementSpeed, startBpm).ToString() + " m");
                }
            }
        }
    }
}