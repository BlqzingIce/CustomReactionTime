namespace CustomReactionTime.Utils
{
    public class ReactionTimeUtils
    {
        public static float CalculateJumpDistance(BeatmapObjectSpawnMovementData.NoteJumpValueType offsetMode, float offsetValue, float mapNJS, float mapBPM)
        {
            switch (offsetMode)
            {
                case BeatmapObjectSpawnMovementData.NoteJumpValueType.JumpDuration:
                    return offsetValue * mapNJS * 2;

                case BeatmapObjectSpawnMovementData.NoteJumpValueType.BeatOffset:
                    float halfjump = 4f;
                    float num = 60f / mapBPM;

                    while (mapNJS * num * halfjump > 17.999)
                        halfjump /= 2;

                    halfjump += offsetValue;
                    if (halfjump < 0.25f)
                        halfjump = 0.25f;

                    float jumpdistance = mapNJS * num * halfjump * 2;
                    return jumpdistance;

                default:
                    Plugin.Log.Error("offsetMode error");
                    return 0;
            }
        }

        public static float CalculateReactionTime(BeatmapObjectSpawnMovementData.NoteJumpValueType offsetMode, float offsetValue, float mapNJS, float mapBPM)
        {
            switch (offsetMode)
            {
                case BeatmapObjectSpawnMovementData.NoteJumpValueType.JumpDuration:
                    return offsetValue;

                case BeatmapObjectSpawnMovementData.NoteJumpValueType.BeatOffset:
                    return CalculateJumpDistance(offsetMode, offsetValue, mapNJS, mapBPM) / (2 * mapNJS);

                default:
                    Plugin.Log.Error("offsetMode error");
                    return 0;
            }
        }
    }
}