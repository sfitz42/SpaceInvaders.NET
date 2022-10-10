using Intel8080.Emulator.IO;

namespace SpaceInvaders.Core.Ports.Input
{
    public class InputPort1 : IInputDevice
    {
        public enum Input
        {
            Credit = 0x01,
            OnePlayerStart = 0x04,
            TwoPlayerStart = 0x02,
            PlayerOneFire = 0x10,
            PlayerOneLeft = 0x20,
            PlayerOneRight = 0x40
        }

        public const int Port = 0x01;

        /**
         * Default = 0x08 (0b0001000)
         * 
         * Bits are set to 1 upon press or deposit
         * 
         * Bit 0: Credit
         * Bit 1: 2 Player Start
         * Bit 2: 1 Player Start
         * Bit 3: 1
         * Bit 4: Player One Fire
         * Bit 5: Player One Left
         * Bit 6: Player One Right
         * Bit 7: N/A
         * 
         */
        private byte _data = 0x08;

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