# CustomReactionTime

Like JDFixer, but worse! I made this cuz I couldn't figure out what a jump distance was.

Set a consistent reaction time, or use thresholds to set it based on the map's original reaction time.

### Important
This mod is affected by the base game setting for offset. "Static" offsets will set the reaction time to the value you have selected before the mod acts on it, which is pointless as you can set the reaction time to a constant more precisely with this mod. Therefore it is recommended that you use the "Dynamic Default" offset setting. 

## Modes
- **Disabled**  
The mod will not change anything when playing.

- **UseReactionTime**  
Changes every map to have the `ReactionTime` you set. Nice, simple, and consistent.

- **UseThresholds**  
If a map's base reaction time is below your `LowerThreshold` or above your `UpperThreshold`, it's reaction time is changed based on the setting you have selected for that threshold.

**Threshold Settings**  
`ReactionTime` - Will set the reaction time to your selected `ReactionTime`.  
`Threshold` - Will set the reaction time to the threshold it went past.  
`None` - Will not change the reaction time.

**Minimum Jump Distance Settings**  
`Use Minimum Jump Distance` - If the jump distance set by the map or this mod is below the value set for `Minimum Jump Distance`, it will raise the jump distance to `Minimum Jump Distance`.  
`Minimum Jump Distance` - Minimum jump distance value, in meters.

## How To Install
- Simply download CustomReactionTime.dll from [releases](https://github.com/BlqzingIce/CustomReactionTime/releases) and put it in your Plugins folder!
- Requires BSIPA and BSML (most likely already installed lol)
- Currently made for 1.24.1, will work with on 1.21.0+
- **Not compatible with JDFixer or NjsFixer** (or anything else that messes with njs, jd, or rt)

## Config File
- The config file can be found at Beat Saber/UserData/CustomReactionTime.json
- `Mode`: 0 = Disabled, 1 = UseReactionTime, 2 = UseThresholds
- `ReactionTime`, `LowerThreshold`, and`UpperThreshold`: Exactly what they sound like, all in milliseconds.
- `LowerSetting` and `UpperSetting`: Corresponding threshold settings, 0 = ReactionTime, 1 = Threshold, 2 = None
- `SliderMin` and `SliderMax`: Secret settings that control how much range the slider settings have, also in ms.
- `MinJDEnabled` and `MinimumJumpDistance`: Self explanatory.
- **It is highly recommended that you both: keep minimums below their associated maximums, and keep the settings measured in ms to increments of 5. Not doing so could cause unexpected things to happen.**
-  Also note that extreme values could cause things to function improperly. Please keep your values reasonable, and out of the negatives.

## Credits
A lot of this is based off of JDFixer, which itself was based off of NjsFixer, so big thanks to Zephyr and Kyle 1413, as well as the modding community as a whole.