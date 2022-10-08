using Intel8080.Emulator.IO;
using SpaceInvaders.Core.Ports.Output;

namespace SpaceInvaders.Core.Ports.Input
{
    internal class ShiftRegisterIn : IInputDevice
    {
        public const int Port = 0x03;

        public byte LHS { get; set; }
        public byte RHS { get; set; }

        private readonly ShiftOffset _shiftOffset;

        public ShiftRegisterIn(ShiftOffset shiftOffset)
        {
            _shiftOffset = shiftOffset;
        }

        public byte Read()
        {
            var v = (LHS << 8) | RHS;

            var result = (byte)(v >> (8 - _shiftOffset.Offset));

            return result;
        }
    }
}
