using Intel8080.Emulator.IO;

namespace SpaceInvaders.Core.Ports.Output
{
    public class WatchdogPort : IOutputDevice
    {
        public const int Port = 0x06;

        public void Write(byte data)
        {
            return;
        }
    }
}
