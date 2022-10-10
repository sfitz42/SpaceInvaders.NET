using Intel8080.Emulator.IO;
using System;

namespace SpaceInvaders.Core.Ports.Output
{
    internal class SoundOutput0 : IOutputDevice
    {
        public const int Port = 0x03;

        private const byte UFOMask = 0x01;
        private const byte FireMask = 0x02;
        private const byte FlashMask = 0x04;
        private const byte InvaderDieMask = 0x08;

        private readonly SoundDevice _soundDevice;

        /**
         * Default = 0x00 (0b00000000)
         * 
         * Bit 0: UFO (looping while on screen) - 0.wav
         * Bit 1: Shot - 1.wav
         * Bit 2: Flash - 2.wav
         * Bit 3: Invader Death - 3.wav
         * Bit 4: Extended play
         * Bit 5: AMP enable
         * Bit 6: N/A
         * Bit 7: N/A
         * 
         */
        private byte _pData;

        public SoundOutput0(SoundDevice soundDevice)
        {
            _soundDevice = soundDevice;
        }

        public void Write(byte data)
        {
            SoundType? sound = null;

            if ((data & UFOMask) != 0 && (_pData & UFOMask) == 0)
                sound = SoundType.UFO;

            if ((data & FireMask) != 0 && (_pData & FireMask) == 0)
                sound = SoundType.Fire;

            if ((data & FlashMask) != 0 && (_pData & FlashMask) == 0)
                sound = SoundType.Flash;

            if ((data & InvaderDieMask) != 0 && (_pData & InvaderDieMask) == 0)
                sound = SoundType.InvaderDeath;

            if ((data & UFOMask) == 0 && (_pData & UFOMask) != 0)
                _soundDevice.OnUFOEnd(new EventArgs());

            _pData = data;

            if (sound != null)
                _soundDevice.OnSoundOutputChange(new SoundOutputChangeEventArgs(sound.Value));
        }
    }
}