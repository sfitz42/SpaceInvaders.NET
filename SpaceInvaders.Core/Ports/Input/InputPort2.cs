using Intel8080.Emulator.IO;

namespace SpaceInvaders.Core.Ports.Input
{
    public class InputPort2 : IInputDevice
    {
        public const int Port = 0x02;

        /**
         * Default = 0x00 (0b00001110)
         * 
         * Bit 0 / 1: Set ships \ lives
         *            00 = 3 ships
         *            01 = 4 ships
         *            10 = 5 ships
         *            11 = 6 ships
         * Bit 2: Tilt
         * Bit 3: Extra ship score threshold
         *        1 = 1500
         *        1 = 1000
         * Bit 4: Player 2 Shot
         * Bit 5: Player 2 Left
         * Bit 6: Player 2 right
         * Bit 7: Display coin info during demo. 0 = On
         * 
         */
        private byte _data = 0x00;

        public byte Read()
        {
            return _data;
        }
    }
}
