using HarmonyLib;
using CustomReactionTime.Modes;

namespace CustomReactionTime.HarmonyPatches
{
    [HarmonyPatch(typeof(BeatmapObjectSpawnMovementData), "Init")]
    internal class SpawnMovementDataUpdatePatch
    {
        public static void Prefix(ref float startNoteJumpMovementSpeed, float startBpm, ref BeatmapObjectSpawnMovementData.NoteJumpValueType noteJumpValueType, ref float noteJumpValue)
        {
            if (Configuration.PluginConfig.Instance.Enabled == false)
            {
                return;
            }

            if(Configuration.PluginConfig.Instance.Mode == Mode.UseReactionTime)
            {
                Plugin.Log.Info("Mode: UseReactionTime");
                noteJumpValueType = BeatmapObjectSpawnMovementData.NoteJumpValueType.JumpDuration;
                noteJumpValue = Configuration.PluginConfig.Instance.ReactionTime / 1000f;
                Plugin.Log.Info("Offset set to: " + (noteJumpValue * 1000).ToString() + " ms");
            }

            else
            {
                //if offset mode is static, this is the reaction time
                float reactionTime = noteJumpValue * 1000;

                //Is offset mode set to dynamic?
                if (noteJumpValueType == BeatmapObjectSpawnMovementData.NoteJumpValueType.BeatOffset)
                {
                    Plugin.Log.Debug("Offset type: Dynamic");

                    float mapNJS = startNoteJumpMovementSpeed;
                    if (mapNJS <= 0.01) //Just in case?
                        mapNJS = 10;

                    //Calculate jump distance
                    float jumpdistance = 0f; //Also just in case
                    float halfjump = 4f;
                    float num = 60f / startBpm;

                    while (mapNJS * num * halfjump > 17.999)
                        halfjump /= 2;

                    halfjump += noteJumpValue;
                    if (halfjump < 0.25f)
                        halfjump = 0.25f;

                    jumpdistance = mapNJS * num * halfjump * 2;
                    Plugin.Log.Debug("jumpdistance: " + jumpdistance.ToString());

                    //Calculate set reaction time
                    reactionTime = jumpdistance / (2 * mapNJS) * 1000;
                }

                Plugin.Log.Info("ReactionTime: " + reactionTime.ToString());

                if (reactionTime < Configuration.PluginConfig.Instance.LowerThreshold)
                {
                    Plugin.Log.Info("Reaction time is below min");
                    noteJumpValueType = BeatmapObjectSpawnMovementData.NoteJumpValueType.JumpDuration;

                    if (Configuration.PluginConfig.Instance.Mode == Mode.SetToThreshold)
                    {
                        noteJumpValue = Configuration.PluginConfig.Instance.LowerThreshold / 1000f;
                    }

                    else noteJumpValue = Configuration.PluginConfig.Instance.ReactionTime / 1000f;

                    Plugin.Log.Info("Offset set to: " + (noteJumpValue * 1000).ToString() + " ms");
                }

                else if (reactionTime > Configuration.PluginConfig.Instance.UpperThreshold)
                {
                    Plugin.Log.Info("Reaction time is above max");
                    noteJumpValueType = BeatmapObjectSpawnMovementData.NoteJumpValueType.JumpDuration;

                    if (Configuration.PluginConfig.Instance.Mode == Mode.SetToThreshold)
                    {
                        noteJumpValue = Configuration.PluginConfig.Instance.UpperThreshold / 1000f;
                    }

                    else noteJumpValue = Configuration.PluginConfig.Instance.ReactionTime / 1000f;

                    Plugin.Log.Info("Offset set to: " + (noteJumpValue * 1000).ToString() + " ms");
                }

                else
                {
                    Plugin.Log.Info("Offset not changed, within limits");
                }
            }
        }
    }
}