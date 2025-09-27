# Unlock Everything - Megabonk Mod

## Description
This mod unlocks all content in Megabonk, including:
- All items
- All weapons
- All tomes
- All characters
- All achievements

## Warning

⚠️ **BACK UP YOUR SAVE DATA FIRST!**

This has had very limited testing. While it doesn't directly modify save data, it may cause unexpected issues.

Save data location: `%UserProfile%\AppData\LocalLow\Ved\` (e.g., `C:\Users\YourName\AppData\LocalLow\Ved\`)

Make a backup of this entire folder before using the mod.

## Installation

### Via Thunderstore/R2ModMan (Recommended)
1. Install through Thunderstore or R2ModMan
2. Launch the game through the mod manager

### Manual Installation
1. Ensure BepInEx IL2CPP 6.0.733 is installed
2. Copy `UnlockEverything.dll` to `BepInEx\plugins\`
3. Launch the game

## Configuration
The mod creates a configuration file in `BepInEx\config\` on first run:
- `UnlockAllEnabled`: Toggle the mod on/off (default: true)
- `DebugLogging`: Enable debug logging to see what's being unlocked (default: false)

## How It Works
The mod uses Harmony to patch the game's unlock system:
- `IsAvailable`: Makes all content appear available
- `IsPurchased`: Makes everything appear as purchased
- `IsAchievementDone`: Marks all achievements as completed
- Character and challenge visibility checks

## Building from Source

### Prerequisites
1. .NET 6.0 SDK
2. Game files with BepInEx installed at `../gamefiles/Megabonk_bepin/`

### Build Steps
```bash
cd unlockEverything
dotnet build
```

The built DLL will be in `bin/Debug/net6.0/` and automatically copied to the game's plugins folder.

### Note on Dependencies
The project references game DLLs from the local game installation. These are not included in the repository to avoid redistributing game files. The `.gitignore` excludes the `Libs/` folder for this reason.

## Publishing to Thunderstore

1. Build the project: `dotnet build -c Release`
2. Copy `bin/Release/net6.0/UnlockEverything.dll` to `thunderstore_packaging/`
3. Add a 256x256 `icon.png` to `thunderstore_packaging/`
4. Create a ZIP containing:
   - `UnlockEverything.dll`
   - `manifest.json`
   - `README.md`
   - `icon.png`
5. Name the ZIP: `YourTeamName-UnlockEverything-1.0.0.zip`
6. Upload to Thunderstore

## Credits
- Built using the [MEGABONK_SIMPLE_MOD](https://github.com/Oksamies/MEGABONK_SIMPLE_MOD) template
- BepInEx and Harmony teams for the modding framework