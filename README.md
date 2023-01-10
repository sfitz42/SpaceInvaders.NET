# SpaceInvaders.NET
Space Invaders arcade emulator implemented using C# / .NET 6.

Core Intel 8080 CPU emulation has been built in [NET8080](https://github.com/sfitz42/NET8080.git) class libary.

## Building
### Requirements
- Git
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download)
- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- dotnet wasm-tools workload
- [NET8080](https://github.com/sfitz42/NET8080.git) (included as submodule)
- Space Invaders ROM files (not provided in repo):
    - invaders.h
    - invaders.g
    - invaders.f
    - invaders.e
- Space Invaders sound files (not provided in repo)

```ps
git clone https://github.com/sfitz42/SpaceInvaders.NET.git --recurse-submodules
dotnet workload install wasm-tools
cd SpaceInvaders.NET
dotnet build
```

## Providing Game Files
ROM files in the WASM port are selected by the user at runtime via upload dialog.

The following ROM files need to be placed inside SpaceInvaders.OpenTK/Roms:
- invaders.h
- invaders.g
- invaders.f
- invaders.e

The following sound files need to be placed inside SpaceInvaders.OpenTK/Sounds:
|File name|Sound Type|
|---------|----------|
|0.wav|UFO (looping)|
|1.wav|Fire|
|2.wav|Death|
|3.wav|Hit|
|4.wav|Fleet Movement 1|
|5.wav|Fleet Movement 2|
|6.wav|Fleet Movement 3|
|7.wav|Fleet Movement 4|
|8.wav|UFO Hit|

## Running Emulator
### OpenTK
The OpenTK frontend (OpenGL / OpenAL) can be ran on Windows / MacOS / Linux using the following commands:
```ps
cd SpaceInvaders.OpenTK
dotnet run
```

Change display scale (default 1x) by providing the scale argument:
```
dotnet run -s 2
dotnet run --displayScale 2
```

Users on Windows will also need to install OpenAL.

### Web Browser (WASM)
The web assembly port has been deployed to Netlify. This can be accessed on the following URL:

https://space-invaders-net.netlify.app

The port can also be ran on a local server by executing the following commands:

```ps
cd SpaceInvaders.Web
dotnet run
```

**Note**: Audio support has not yet been implemented

## Useful Links / Resources
- [Computer Archeology - Space Invaders](https://computerarcheology.com/Arcade/SpaceInvaders)
- [superzazu/invaders](https://github.com/superzazu/invaders) - Useful for colour mappings
- [OpenTK (OpenGL Examples)](https://github.com/opentk/LearnOpenTK)
- [OpenTK (OpenAL Example)](https://github.com/mono/opentk/blob/e5859900d3a41885e03be46b492bfd382442f130/Source/Examples/OpenAL/1.1/Playback.cs) - Useful WAV file method