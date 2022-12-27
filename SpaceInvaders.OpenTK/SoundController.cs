using OpenTK.Audio.OpenAL;
using SpaceInvaders.Core;
using SpaceInvaders.Core.Ports.Output;

namespace SpaceInvaders.OpenTK;

public class SoundController
{
    private readonly ALDevice _device;
    private readonly ALContext _context;
    private readonly ArcadeMachine _arcadeMachine;
    private readonly int[] _buffers;
    private readonly int[] _sources;

    private readonly string[] _soundPaths =
    {
        "Sounds/0.wav",
        "Sounds/1.wav",
        "Sounds/2.wav",
        "Sounds/3.wav",
        "Sounds/4.wav",
        "Sounds/5.wav",
        "Sounds/6.wav",
        "Sounds/7.wav",
        "Sounds/8.wav"
    };

    public SoundController(ArcadeMachine arcadeMachine)
    {
        _arcadeMachine = arcadeMachine;

        _arcadeMachine.SoundDevice.SoundChanged += SoundChanged;
        _arcadeMachine.SoundDevice.UFOEnd += UFOEnd;

        _device = ALC.OpenDevice(null);
        _context = ALC.CreateContext(_device, new ALContextAttributes());

        ALC.MakeContextCurrent(_context);

        _buffers = AL.GenBuffers(_soundPaths.Length);
        _sources = AL.GenSources(_soundPaths.Length);

        BufferSoundData();
    }

    private void SoundChanged(object? sender, SoundOutputChangeEventArgs e)
    {
        var sound = (int)e.Sound;

        AL.SourcePlay(_sources[sound]);
    }

    private void UFOEnd(object? sender, EventArgs e)
    {
        AL.SourceStop(_sources[(int)SoundType.UFO]);
    }

    private void BufferSoundData()
    {
        for (var sound = 0; sound < _soundPaths.Length; sound++)
        {
            var pcm = File.ReadAllBytes(_soundPaths[sound]);

            if (sound == (int) SoundType.UFO)
            {
                AL.BufferData(_buffers[sound], ALFormat.Mono16, pcm, 44100);
                AL.Source(_sources[sound], ALSourcei.Buffer, _buffers[sound]);
                AL.Source(_sources[sound], ALSourceb.Looping, true);
            }
            else
            {
                AL.BufferData(_buffers[sound], ALFormat.Mono8, pcm, 11025);
                AL.Source(_sources[sound], ALSourcei.Buffer, _buffers[sound]);
            }
        }
    }
}
