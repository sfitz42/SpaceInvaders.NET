# SpaceInvaders.NET
Space Invaders arcade emulator implemented using C# / .NET 6.

Core Intel 8080 CPU emulation has been built in [NET8080](https://github.com/sfitz42/NET8080.git) class libary.

## Building
### Requirements
- Windows
- Git
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download)
- [NET8080](https://github.com/sfitz42/NET8080.git) (included as submodule)
- Space Invaders ROM files (not provided in repo):
    - invaders.h
    - invaders.g
    - invaders.f
    - invaders.e
- Space Invaders sound files (not provided in repo)

```ps
git clone https://github.com/sfitz42/SpaceInvaders.NET.git --recurse-submodules
cd SpaceInvaders.NET
dotnet build
```

## Emulator
### Installing Game Files
The following ROM files need to be placed inside SpaceInvaders.WPF\Roms:
- invaders.h
- invaders.g
- invaders.f
- invaders.e

The following sound files need to be placed inside SpaceInvaders.WPF\Sounds:
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

### Running Emulator
#### OpenTK
The OpenTK frontend (OpenGL / OpenAL) can be ran on Windows / MacOS / Linux using the following commands:
```ps
cd SpaceInvaders.OpenTK
dotnet run
```

Change display scale (default 1x) by providing the scale argument
```
dotnet run -- -s 2
dotnet run -- --displayScale 2
```

#### WPF
The WPF implementation of the emulator can be started using the following commands:

**Note:** This can only be ran on a Windows based machine
```ps
cd SpaceInvaders.WPF
dotnet run
```

## Useful Links / Resources
- [Computer Archeology - Space Invaders](https://computerarcheology.com/Arcade/SpaceInvaders)
- [superzazu/invaders](https://github.com/superzazu/invaders) - Useful for colour mappings
- [OpenTK (OpenGL Examples)](https://github.com/opentk/LearnOpenTK)
- [OpenTK (OpenAL Example)](https://github.com/mono/opentk/blob/e5859900d3a41885e03be46b492bfd382442f130/Source/Examples/OpenAL/1.1/Playback.cs) - Useful WAV file method