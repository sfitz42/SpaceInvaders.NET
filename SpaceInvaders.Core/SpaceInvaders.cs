using Intel8080.Emulator;

namespace SpaceInvaders.Core
{
    public class SpaceInvaders
    {
        public enum Interrupt
        {
            MidFrame = 0xCF,
            EndFrame = 0xD7
        }

        public const int ScreenWidth = 256;
        public const int ScreenHeight = 224;
        public const int FramesPerSecond = 60;

        public CPU Cpu { get; }

        public MainMemory Memory { get; }

        private Interrupt _nextInterrupt = Interrupt.MidFrame;

        public SpaceInvaders()
        {
            Memory = new MainMemory();
            Cpu = new CPU(Memory);
        }

        public void GenerateInterrupt()
        {
            Cpu.RaiseInterrupt((byte) _nextInterrupt);

            if (_nextInterrupt == Interrupt.MidFrame)
                _nextInterrupt = Interrupt.EndFrame;
            else if (_nextInterrupt == Interrupt.EndFrame)
                _nextInterrupt = Interrupt.MidFrame;
        }

        public void Run()
        {

        }
    }
}
