using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

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
        // Default Gameboy have only this "colors" defined in byte at address: 0xFF47 
        // There are 4 color combinations stored each for 2 bits
        public enum GameboyColors
        {
            GB_COLOR_WHITE,
            GB_COLOR_LIGHT_GREY,
            GB_COLOR_DARK_GREY,
            GB_COLOR_BLACK
        };

        // When Vblank iRQ occurs, redraw frame (AFAIK)

        // FF40 – LCDC – LCD Control 
        //Bit 7 - LCD Display Enable(0=Off, 1=On)
        //Bit 6 - Window Tile Map Display Select(0=9800-9BFF, 1=9C00-9FFF)
        //Bit 5 - Window Display Enable(0=Off, 1=On)
        //Bit 4 - BG & Window Tile Data Select(0=8800-97FF, 1=8000-8FFF)
        //Bit 3 - BG Tile Map Display Select(0=9800-9BFF, 1=9C00-9FFF)
        //Bit 2 - OBJ(Sprite) Size(0=8x8, 1=8x16)
        //Bit 1 - OBJ(Sprite) Display Enable(0=Off, 1=On)
        //Bit 0 - BG Display(for CGB see below) (0=Off, 1=On)

        public System.Boolean isDisplayOn = false;

        public Renderer() { }

        // Values From 0 - 255
        public Byte[] GetTilePatternSprites() { return Program.emulator.GetMemory().ReadFromMemory(0x8000, 0x0FFF); }

        // TODO: Values From -128 - 127
        public Byte[] GetTilePattern() { return Program.emulator.GetMemory().ReadFromMemory(0x8800, 0x0FFF); }

        public Byte[] GetSpriteAttributes() { return Program.emulator.GetMemory().ReadFromMemory(0xFE00, 0x009F); }

        public void Start()
        {
            isDisplayOn = true;
            GL.ClearColor(ConvertToFloat(139), ConvertToFloat(172), ConvertToFloat(15), 1.0f);
            
        }

        public void Render()
        {
            Byte[] values = new Byte[800 * 600];

            for (int i = 0; i < 800 * 600; i++)
                values[i] = (Byte)(i % 255);

            GL.DrawPixels(800, 600, PixelFormat.GreenInteger, PixelType.Byte, values);
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_INFO, "RENDER");
        }

        public float ConvertToFloat(Byte value)
        {
            return (value/255.0f);
        }
    }
}
