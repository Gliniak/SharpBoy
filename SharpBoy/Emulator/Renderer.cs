using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class Sprite
    {
        Byte posX;
        Byte posY;
        Byte pattern;
        Byte Flags;

        public Sprite(Byte[] data, Byte length)
        {
            if(length != 4)
            {
                // Wrong Sprite data?
                return;
            }

            posX = data[0];
            posY = data[1];
            pattern = data[2];
            Flags = data[3];
        }
    }

    class Renderer
    {
        public Renderer() { }

        // Values From 0 - 255
        public Byte[] GetTilePatternSprites() { return Program.emulator.GetMemory().ReadFromMemory(0x8000, 0x0FFF); }

        // TODO: Values From -128 - 127
        public Byte[] GetTilePattern() { return Program.emulator.GetMemory().ReadFromMemory(0x8800, 0x0FFF); }

        public Byte[] GetSpriteAttributes() { return Program.emulator.GetMemory().ReadFromMemory(0xFE00, 0x009F); }

    }
}
