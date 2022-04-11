# CustomReactionTime

Like JDFixer, but worse! I made this cuz I couldn't figure out what a jump distance was.

Set a consistent reaction time, or use thresholds for varied reaction times within your parameters.

## Modes
- **UseReactionTime**
Changes every map to have the `Reaction Time` you set. Nice, simple, and consistent.
- **SetIfOutside**
If a map's base reaction time is in between `Lower Threshold` and `Upper Threshold`, it's reaction time is left unchanged. But if the reaction time goes above or below these thresholds, it is set to your `Reaction Time` instead.
- **SetToThreshold**
Like `SetIfOutside`, but instead of changing the reaction time to `Reaction Time`, it changes it to the closest threshold. Ex: Map's RT is 812, your `Upper Threshold` is 600, the reaction time will be set to 600.

## How To Install
- Simply download CustomReactionTime.dll from [releases](https://github.com/BlqzingIce/CustomReactionTime/releases) and put it in your Plugins folder!
- Requires BSIPA and BSML (most likely already installed lol)
- Currently made for 1.21.0, may work with 1.20.x and future versions (not tested)
- **Not compatible with JDFixer or NjsFixer** (or anything else that messes with njs, jd, or rt)

## Config File
- The config file can be found at Beat Saber/UserData/CustomReactionTime.json
- `Enabled`: Whether or not the mod should change reaction times
- `Mode`: 0 = UseReactionTime, 1 = SetIfOutside, 2 = SetToThreshold
- `ReactionTime`, `LowerThreshold`, and`UpperThreshold`: Exactly what they sound like, all in milliseconds.
- `SliderMin` and `SliderMax`: Secret settings that control how much range the slider settings have, also in ms.
- **It is highly recommended that you both: keep minimums above their maximums, and the settings measured in ms to increments of 5. Not doing so could cause unexpected things to happen.**
-  Also note that extreme values could cause things to function improperly. Please keep your values reasonable, and out of the negatives.

## Credits
A lot of this is based off of JDFixer, which itself was based off of NjsFixer, so big thanks to Zephyr and Kyle 1413, as well as the modding community as a whole.
