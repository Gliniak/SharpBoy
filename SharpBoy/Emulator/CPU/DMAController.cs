using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    // 40x32bits OAMobj
    class Sprite
    {
        Byte posX;
        Byte posY;
        Byte tile;
        Byte Flags; // Bits 4 to 7

        public Sprite(Byte[] data, Byte length)
        {
            if (length != 4)
            {
                // Wrong Sprite data?
                return;
            }

            posX = data[0];
            posY = data[1];
            tile = data[2];
            Flags = data[3];
        }
    }

    public enum GameboyColors
    {
        GB_COLOR_WHITE,
        GB_COLOR_LIGHT_GREY,
        GB_COLOR_DARK_GREY,
        GB_COLOR_BLACK
    };

    public enum LCDmodeFlag
    {
        LCD_MODE_FLAG_H_BLANK,
        LCD_MODE_FLAG_V_BLANK,
        LCD_MODE_FLAG_OAM_READ, // OAM Read time, moment when cpu dont have access to OAM
        LCD_MODE_FLAG_DATA_TO_LCD // During data send to renderer (LCD Driver)
    };

    // AKA LCD CONTROLLER (In CGB)

    /*
     * It suppose to have access to some registers (Memory Registers)
     * Copy memory to OAM
     * Send data to display 
     * Is responsible for: LCDC Interrupt and (i guess) Vblank Interrupt
     */

    class DMAController
    {
        public DMAController() { }

        // OAM mem is available only during mode 0-1 (H and Vblank period), However DMA have access to it all time (0xFF46)
        public Byte[] GetOAM() { return Program.emulator.GetMemory().ReadFromMemory(0xFE00, 0x009F); }

        // Nah not actually that good i guess
        public Byte GetLCDCregister() { return Program.emulator.GetMemory().ReadFromMemory(0xFF40); }
        public BitArray GetLCDCregisterBits() { return new BitArray(GetLCDCregister()); }

        // TODO: Bit 7 Check!
        public void SetLCDCregister(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF40, value); }
        public void SetLCDCregisterBit(Byte bit, bool toogle)
        {
            SetLCDCregister(toogle ? (Byte)(GetLCDCregister() | (1 << bit)) : (Byte)(GetLCDCregister() ^ (1 << bit)));
        }

        public bool IsDisplayOn() { return GetLCDCregisterBits().Get(7); }

        public void Update()
        {

            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DMAController: Update()");
        }
    }
}
