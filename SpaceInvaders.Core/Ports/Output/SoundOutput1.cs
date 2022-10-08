using Intel8080.Emulator.IO;

namespace SpaceInvaders.Core.Ports.Output
{
    internal class SoundOutput1 : IOutputDevice
    {
        public const int Port = 0x05;

        private const byte Fleet1Mask = 0x01;
        private const byte Fleet2Mask = 0x02;
        private const byte Fleet3Mask = 0x04;
        private const byte Fleet4Mask = 0x08;
        private const byte UFOHitMask = 0x10;

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

        public SoundOutput1(SoundDevice soundDevice)
        {
            _soundDevice = soundDevice;
        }

        public void Write(byte data)
        {
            SoundType? sound = null;

            if ((data & Fleet1Mask) != 0 && (_pData & Fleet1Mask) == 0)
                sound = SoundType.Fleet1;

            if ((data & Fleet2Mask) != 0 && (_pData & Fleet2Mask) == 0)
                sound = SoundType.Fleet2;

            if ((data & Fleet3Mask) != 0 && (_pData & Fleet3Mask) == 0)
                sound = SoundType.Fleet3;

            if ((data & Fleet4Mask) != 0 && (_pData & Fleet4Mask) == 0)
                sound = SoundType.Fleet4;
            
            if ((data & UFOHitMask) != 0 && (_pData & UFOHitMask) == 0)
                sound = SoundType.UFOHit;

            _pData = data;

            if (sound != null)
                _soundDevice.OnSoundOutputChange(new SoundOutputChangeEventArgs(sound.Value));
        }
    }
}