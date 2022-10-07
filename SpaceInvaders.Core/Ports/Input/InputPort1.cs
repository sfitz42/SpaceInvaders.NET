using Intel8080.Emulator.IO;

namespace SpaceInvaders.Core.Ports.Input
{
    public class InputPort1 : IInputDevice
    {
        public const int Port = 0x01;

        private const int CreditMask = 0x01;
        private const int OnePlayerStartMask = 0x04;
        private const int TwoPlayerStartMask = 0x02;
        private const int FireMask = 0x10;
        private const int LeftMask = 0x20;
        private const int RightMask = 0x40;

        public bool Credit { get => GetBit(CreditMask); set => SetBit(value, CreditMask); }
        public bool OnePlayerStart { get => GetBit(OnePlayerStartMask); set => SetBit(value, OnePlayerStartMask); }
        public bool TwoPlayerStart { get => GetBit(TwoPlayerStartMask); set => SetBit(value, TwoPlayerStartMask); }
        public bool Fire { get => GetBit(FireMask); set => SetBit(value, FireMask); }
        public bool Left { get => GetBit(LeftMask); set => SetBit(value, LeftMask); }
        public bool Right { get => GetBit(RightMask); set => SetBit(value, RightMask); }

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

        private bool GetBit(byte mask)
        {
            return (_data & mask) == mask;
        }

        private void SetBit(bool value, byte mask)
        {
            _data = (byte)(value ? (_data | mask) : (_data & ~mask));
        }
    }
}