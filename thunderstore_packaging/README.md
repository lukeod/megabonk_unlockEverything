# Unlock Everything - Megabonk Mod

Instantly unlock all content in Megabonk.

## Features

This mod unlocks:
- All items
- All weapons
- All tomes
- All characters

## ⚠️ Important Warning

**BACK UP YOUR SAVE DATA FIRST!**

While this mod doesn't directly modify save files, it changes how the game interprets unlock data. There's a possibility it could cause unexpected issues.

Save data location: `%UserProfile%\AppData\LocalLow\Ved\`
(e.g., `C:\Users\YourName\AppData\LocalLow\Ved\`)

Make a backup of this entire folder before using the mod.

## Installation

### Via Thunderstore/R2ModMan (Recommended)
1. Install the mod through Thunderstore or R2ModMan
2. Launch the game through the mod manager
3. All content will be unlocked!

### Manual Installation
1. Ensure BepInEx IL2CPP (6.0.733) is installed
2. Copy `UnlockEverything.dll` to `BepInEx\plugins\`
3. Launch the game normally

## Configuration

The mod creates a configuration file in `BepInEx\config\UnlockEverything.cfg`:

- **UnlockAllEnabled** (default: true) - Toggle the mod on/off without uninstalling
- **DebugLogging** (default: false) - Enable to see what's being unlocked in the console

You can edit these settings while the game is running - changes take effect immediately!

## How It Works

The mod uses Harmony patches to intercept the game's unlock checking methods:
- `IsAvailable` - Makes all content appear available
- `IsPurchased` - Makes everything appear as already purchased
- `IsAchievementDone` - Marks all achievements as completed
- Character selection and challenge visibility checks

## Uninstalling

1. Remove `UnlockEverything.dll` from `BepInEx\plugins\`
2. Optionally delete the config file from `BepInEx\config\`

## Support

If you encounter issues:
1. Check that BepInEx is properly installed
2. Verify the mod is in the correct folder
3. Enable debug logging to see what's happening
4. Report issues on the GitHub repository

## Credits

- Built using the [MEGABONK_SIMPLE_MOD](https://github.com/Oksamies/MEGABONK_SIMPLE_MOD) template
- Thanks to the BepInEx and Harmony teams