using Intel8080.Emulator;
using System.IO;

namespace SpaceInvaders.Core
{
    public class MainMemory : IMemory
    {
        public const int MemorySize = 0x4000;
        public const int RAMStart = 0x2000;
        public const int VideoMemoryStart = 0x2400;
        public const int VideoMemoryEnd = 0x4000;

        private readonly byte[] _memory;

        public byte this[int index]
        {
            get
            {
                if (index >= MemorySize)
                    index -= RAMStart;

                return _memory[index];
            }
            set
            {
                if (index >= RAMStart && index < MemorySize)
                    _memory[index] = value;
            }
        }

        public MainMemory()
        {
            _memory = new byte[MemorySize];

            LoadRom("Roms/invaders.h", 0x0000);
            LoadRom("Roms/invaders.g", 0x0800);
            LoadRom("Roms/invaders.f", 0x1000);
            LoadRom("Roms/invaders.e", 0x1800);
        }

        private void LoadRom(string path, int offset = 0x0000)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            fs.Read(_memory, offset, (int)fs.Length);
        }

        public byte[] ReadVRAM()
        {
            return _memory[VideoMemoryStart..VideoMemoryEnd];
        }
    }
}
