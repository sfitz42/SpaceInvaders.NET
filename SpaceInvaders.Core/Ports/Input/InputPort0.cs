using Intel8080.Emulator.IO;

namespace SpaceInvaders.Core.Ports.Input
{
    public class InputPort0 : IInputDevice
    {
        public enum Input
        {
            Fire = 0x10,
            Left = 0x20,
            Right = 0x40
        }

        public const int Port = 0x00;

        /**
         * Default = 0x0E (0b00001110)
         * 
         * Bit 0: Self-test enable
         * Bit 1: 1
         * Bit 2: 1
         * Bit 3: 1
         * Bit 4: Fire
         * Bit 5: Left
         * Bit 6: Right
         * Bit 7: N/A
         * 
         */
        private byte _data = 0x0E;

        public byte Read()
        {
            return _data;
        }

        public void HandleInput(Input input, bool press)
        {
            var inputMask = (byte)input;

            SetBit(press, inputMask);
        }

        private void SetBit(bool value, byte mask)
        {
            _data = (byte)(value ? (_data | mask) : (_data & ~mask));
        }
    }
}
