using Intel8080.Emulator.IO;

namespace SpaceInvaders.Core.Ports.Input
{
    public class SoundOutput0 : IOutputDevice
    {
        public const int Port = 0x03;

        public void Write(byte data)
        {
            return;
        }
    }
}