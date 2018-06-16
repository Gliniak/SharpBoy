using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace SharpBoy
{
    // README: Renderer class is supposed only to made post-effects and send data to screen, all stuff about reading frame etc is in DMA (LCD) Controller class
    class Renderer
    {

        public UInt32[] ScreenBuffer = new UInt32[144 * 160];

        // Default Gameboy have only this "colors" defined in byte at address: 0xFF47 
        // There are 4 color combinations stored each for 2 bits

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

        public System.Boolean isDisplayOn = true;

        public Renderer() { }

        // Values From 0 - 255
        public Byte[] GetTilePatternSprites() { return Program.emulator.GetMemory().ReadFromMemory(0x8000, 0x0FFF); }

        // Values From -128 - 127
        public Byte[] GetTilePattern() { return Program.emulator.GetMemory().ReadFromMemory(0x8800, 0x0FFF); }

        public Byte[] GetBGMapData() { return Program.emulator.GetMemory().ReadFromMemory(0x9800, 0x03FF); }

        public UInt32[] GetScreenBuffer() { return ScreenBuffer; }

        public void Start()
        {
            isDisplayOn = true;
        }

        public void PrepareFrame()
        {
            //GL.Bitmap()

            // TODO: Prepare Data as Bitmap to render

        }
        public void Render()
        {
            // Send data to window?

            // Create Bitmap
            PrepareFrame();
            Program.mainWindow.openGLControl.Refresh(); // FORCES REPAINT

            // Clear BUffer
            //for (int i = 0; i < ScreenBuffer.Length; i++)
            //    ScreenBuffer[i] = 0;
        }
    }
}
