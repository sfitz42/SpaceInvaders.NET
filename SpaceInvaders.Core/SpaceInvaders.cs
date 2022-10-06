using Intel8080.Emulator;
using System.Diagnostics;
using System.Timers;

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
        public const int ClockSpeed = 2000000;
        public const int CyclesPerFrame = ClockSpeed / FramesPerSecond;
        public const int VBlankPeriod = CyclesPerFrame / 2;

        public CPU Cpu { get; }

        public MainMemory Memory { get; }

        private readonly Timer _clock;
        private readonly Stopwatch _stopwatch;

        private Interrupt _nextInterrupt = Interrupt.MidFrame;

        private double _lastTime = 0;

        public SpaceInvaders()
        {
            Memory = new MainMemory();
            Cpu = new CPU(Memory);

            _clock = new Timer
            {
                Enabled = false,
                Interval = 1
            };

            _stopwatch = new Stopwatch();

            _clock.Elapsed += UpdateCPU;
        }

        public void GenerateInterrupt()
        {
            Cpu.RaiseInterrupt((byte) _nextInterrupt);

            _nextInterrupt = _nextInterrupt != Interrupt.MidFrame ?
                Interrupt.MidFrame :
                Interrupt.EndFrame;
        }

        public void Run()
        {
            _stopwatch.Start();
            _clock.Start();
        }

        private void UpdateCPU(object? sender, ElapsedEventArgs e)
        {
            _clock.Stop();

            var currentTime = _stopwatch.ElapsedMilliseconds;
            var deltaTime = currentTime - _lastTime;

            var cycles = 2000 * deltaTime;
            var vblank = 0L;

            while (cycles > 0)
            {
                var currentCycles = Cpu.Cycles;

                Cpu.Step();

                var completedCycles = Cpu.Cycles - currentCycles;

                vblank += completedCycles;
                cycles -= completedCycles;

                if (vblank >= VBlankPeriod)
                {
                    GenerateInterrupt();

                    vblank = 0;
                }
            }

            _lastTime = currentTime;

            _clock.Start();
        }
    }
}
