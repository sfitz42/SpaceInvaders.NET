using Intel8080.Emulator;
using System;
using System.IO;

namespace SpaceInvaders.Core
{
    public class MainMemory : IMemory
    {
        public const int MemorySize = 0x4000;
        public const int RAMStart = 0x2000;
        public const int VideoMemoryStart = 0x2400;
        public const int VideoMemoryEnd = 0x4000;

        private readonly byte[] _memory = new byte[MemorySize];

        public byte this[int index]
        {
            get
            {
                if (index >= 0x6000)
                    return 0;

                if (index >= MemorySize && index < 0x6000)
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
        }

        public byte[] ReadVRAM()
        {
            return _memory[VideoMemoryStart..VideoMemoryEnd];
        }

        public void LoadRomFromArray(RomBank romBank, byte[] rom)
        {
            var offset = (int)romBank * 0x0800;

            Array.Copy(rom, 0, _memory, offset, rom.Length);
        }

        public void LoadRomsFromFile(string romPath)
        {
            LoadRomFromFileSystem(Path.Combine(romPath, "invaders.h"), 0x0000);
            LoadRomFromFileSystem(Path.Combine(romPath, "invaders.g"), 0x0800);
            LoadRomFromFileSystem(Path.Combine(romPath, "invaders.f"), 0x1000);
            LoadRomFromFileSystem(Path.Combine(romPath, "invaders.e"), 0x1800);
        }

        private void LoadRomFromFileSystem(string path, int offset = 0x0000)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            fs.Read(_memory, offset, (int)fs.Length);
        }
    }
}
