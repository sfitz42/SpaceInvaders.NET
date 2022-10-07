using Intel8080.Emulator.IO;
using SpaceInvaders.Core.Ports.Input;

namespace SpaceInvaders.Core.Ports.Output
{
    internal class ShiftRegisterOutput : IOutputDevice
    {
        public const int Port = 0x04;

        private readonly ShiftRegisterIn _shiftRegisterIn;

        public ShiftRegisterOutput(ShiftRegisterIn shiftRegisterIn)
        {
            _shiftRegisterIn = shiftRegisterIn;
        }

        public void Write(byte data)
        {
            _shiftRegisterIn.RHS = _shiftRegisterIn.LHS;
            _shiftRegisterIn.LHS = data;
        }
    }
}
