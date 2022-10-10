using Intel8080.Emulator.IO;

namespace SpaceInvaders.Core.Ports.Output
{
    internal class ShiftOffset : IOutputDevice
    {
        public const int Port = 0x02;

        public byte Offset { get; private set; }

        public void Write(byte data)
        {
            Offset = (byte)(data & 0x07);
        }
    }
}