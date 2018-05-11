using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace SharpBoy
{
    

    /* Tile is stored as i.e 00 CC
       00 - is 0000 0000
       we use 2-bits per pixel (only 4 posible values)
    */

    // README: Renderer class is supposed only to made post-effects and send data to screen, all stuff about reading frame etc is in DMA (LCD) Controller class
    class Renderer
    {
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

        public System.Boolean isDisplayOn = false;

        public Renderer() { }

        // Values From 0 - 255
        public Byte[] GetTilePatternSprites() { return Program.emulator.GetMemory().ReadFromMemory(0x8000, 0x0FFF); }

        // Values From -128 - 127
        public Byte[] GetTilePattern() { return Program.emulator.GetMemory().ReadFromMemory(0x8800, 0x0FFF); }

        public Byte[] GetBGMapData() { return Program.emulator.GetMemory().ReadFromMemory(0x9800, 0x03FF); }

        public void Start()
        {
            isDisplayOn = true;
        }

        public void PrepareFrame()
        {
            // Thanks to: http://gameboy.mongenel.com/dmg/asmmemmap.html

            /*
            $9800-$9BFF - BG Map Data 1
            This 1024-byte long area is what the video processor uses to build the display. 
            Each byte in this space represnts an 8x8 pixel space on the display. 
            This area is 32x32 tiles large... EG: 1024 bytes. 
            The display processor takes each byte and then goes into the Character RAM area and gets the corresponding tile from that area and draws it to the screen. 
            So, if the first byte in the Map area contained $40, the display processor would get tile $40 from the Character RAM and put it in the top-left corner of the virtual screen. 
            */

            /*
             * OAM is sprite RAM. This area is 40 sprites X 4 bytes long. 
             * When you with to display an object (sprite) you write 4 corresponding bytes to OAM. These 4 bytes are:
                Byte 1: X Location
                Byte 2: Y Location
                Tile Number (0-255)
                Attributes
                The tile number is taken from the Character RAM, just as BG tiles are. 
                The X and Y locations are slightly offset (8 pixels and 16 pixels), so you can have sprites partially off of the left and top of the LCD. 
                So if you set the location to 0,0 then the sprite would be off of the screen. 
                To set a sprite to the top-left corner, you'd set it's location to 8,16
            */

        }
        public void Render()
        {
            // Send data to window?
            Program.mainWindow.openGLControl.Refresh(); // FORCES REPAINT
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_INFO, "RENDER");
        }

        public void Update()
        {

        }
    }
}
