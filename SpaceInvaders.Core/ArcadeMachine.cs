using Intel8080.Emulator;
using SpaceInvaders.Core.Ports.Input;
using SpaceInvaders.Core.Ports.Output;
using System.Diagnostics;
using System.Timers;

namespace SpaceInvaders.Core
{
    public class ArcadeMachine
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

        public InputPort0 InputPort0 { get; } = new();
        public InputPort1 InputPort1 { get; } = new();
        public InputPort2 InputPort2 { get; } = new();

        public SoundDevice SoundDevice { get; } = new();

        private readonly Timer _clock;
        private readonly Stopwatch _stopwatch;

        private readonly ShiftOffset _shiftOffset;
        private readonly ShiftRegisterIn _shiftRegisterIn;
        private readonly ShiftRegisterOutput _shiftRegisterOutput;

        private readonly WatchdogPort _watchdogPort = new();

        private Interrupt _nextInterrupt = Interrupt.MidFrame;

        private double _vblank = 0;
        private double _lastTime = 0;

        public ArcadeMachine()
        {
            Memory = new();
            Cpu = new(Memory);

            _shiftOffset = new();
            _shiftRegisterIn = new(_shiftOffset);
            _shiftRegisterOutput = new(_shiftRegisterIn);

            AddDevices();

            _clock = new()
            {
                Enabled = false,
                Interval = 1
            };

            _stopwatch = new();

            _clock.Elapsed += new ElapsedEventHandler(UpdateCPU);
        }

        public void AddDevices()
        {
            var ioController = Cpu.IOController;

            ioController.AddDevice(InputPort0, InputPort0.Port);
            ioController.AddDevice(InputPort1, InputPort1.Port);
            ioController.AddDevice(InputPort2, InputPort2.Port);
            ioController.AddDevice(_shiftRegisterIn, ShiftRegisterIn.Port);

            ioController.AddDevice(_shiftOffset, ShiftOffset.Port);
            ioController.AddDevice(_shiftRegisterOutput, ShiftRegisterOutput.Port);
            ioController.AddDevice(SoundDevice.SoundOutput0, SoundOutput0.Port);
            ioController.AddDevice(SoundDevice.SoundOutput1, SoundOutput1.Port);
            ioController.AddDevice(_watchdogPort, WatchdogPort.Port);
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

            while (cycles > 0)
            {
                var currentCycles = Cpu.Cycles;

                Cpu.Step();

                var completedCycles = Cpu.Cycles - currentCycles;

                _vblank += completedCycles;
                cycles -= completedCycles;

                if (_vblank >= VBlankPeriod)
                {
                    GenerateInterrupt();

                    _vblank = 0;
                }
            }

            _lastTime = currentTime;

            _clock.Start();
        }
    }
}
