using Intel8080.Emulator.IO;

namespace SpaceInvaders.Core.Ports.Input
{
    public class SoundOutput1 : IOutputDevice
    {
        public const int Port = 0x05;

        public void Write(byte data)
        {
            return;
        }
    }
}