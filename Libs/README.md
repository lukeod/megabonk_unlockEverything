# Required Libraries

This folder should contain the following DLL files from your BepInEx installation:

## From BepInEx Core (`BepInEx/core/`)
- `0Harmony.dll`
- `BepInEx.Core.dll`
- `BepInEx.Unity.IL2CPP.dll`
- `Il2CppInterop.Runtime.dll`

## From BepInEx Interop (`BepInEx/interop/`)
These are generated when BepInEx runs with the game:
- `Assembly-CSharp.dll`
- `UnityEngine.dll`
- `UnityEngine.CoreModule.dll`
- `Il2Cppmscorlib.dll`
- `Il2CppSystem.dll`
- `Il2CppSystem.Core.dll`

## How to obtain these files

1. Install BepInEx IL2CPP (version 6.0.733) for Megabonk
2. Run the game once with BepInEx installed
3. Copy the required DLLs from the locations above to this folder

**Note:** These game DLL files are not included in the repository to avoid redistributing copyrighted content. You must own the game and have BepInEx installed to build this mod from source.