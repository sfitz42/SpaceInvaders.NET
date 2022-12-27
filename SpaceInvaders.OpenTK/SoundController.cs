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

    private static (byte[], int channels, int bit, int rate) LoadWave(string path)
    {
        using var stream = new FileStream(path, FileMode.Open);
        using var reader = new BinaryReader(stream);

        // RIFF header
        var signature = new string(reader.ReadChars(4));
        if (signature != "RIFF")
            throw new NotSupportedException("Specified stream is not a wave file.");

        var riff_chunck_size = reader.ReadInt32();

        var format = new string(reader.ReadChars(4));
        if (format != "WAVE")
            throw new NotSupportedException("Specified stream is not a wave file.");

        // WAVE header
        var format_signature = new string(reader.ReadChars(4));

        if (format_signature != "fmt ")
            throw new NotSupportedException("Specified wave file is not supported.");

        var format_chunk_size = reader.ReadInt32();
        var audio_format = reader.ReadInt16();
        var num_channels = reader.ReadInt16();
        var sample_rate = reader.ReadInt32();
        var byte_rate = reader.ReadInt32();
        var block_align = reader.ReadInt16();
        var bits_per_sample = reader.ReadInt16();

        var data_signature = new string(reader.ReadChars(4));

        int data_chunk_size = reader.ReadInt32();

        return (reader.ReadBytes(data_chunk_size), num_channels, bits_per_sample, sample_rate);
    }

    private static ALFormat GetSoundFormat(int channels, int bits) => channels switch
    {
        1 => bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16,
        2 => bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16,
        _ => throw new NotSupportedException("The specified sound format is not supported."),
    };
   
    private void BufferSoundData()
    {
        for (var sound = 0; sound < _soundPaths.Length; sound++)
        {
            (var pcm, var channels, var bits, var rate) = LoadWave(_soundPaths[sound]);

            AL.BufferData(_buffers[sound], GetSoundFormat(channels, bits), pcm, rate);
            AL.Source(_sources[sound], ALSourcei.Buffer, _buffers[sound]);

            if (sound == (int) SoundType.UFO)
            {
                AL.Source(_sources[sound], ALSourceb.Looping, true);
            }
        }
    }
}
