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
            // Fix this!
            SetLCDCregister(toogle ? (Byte)(GetLCDCregister() | (1 << bit)) : (Byte)(GetLCDCregister() ^ (1 << bit)));
        }

        public bool IsDisplayOn() { return GetLCDCregisterBits().Get(7); }

        /*
            Bit 6 - LYC=LY Coincidence Interrupt (1=Enable) (Read/Write)
            Bit 5 - Mode 2 OAM Interrupt         (1=Enable) (Read/Write)
            Bit 4 - Mode 1 V-Blank Interrupt     (1=Enable) (Read/Write)
            Bit 3 - Mode 0 H-Blank Interrupt     (1=Enable) (Read/Write)
            Bit 2 - Coincidence Flag  (0:LYC<>LY, 1:LYC=LY) (Read Only)
            Bit 1-0 - Mode Flag       (Mode 0-3, see below) (Read Only)
                0: During H-Blank
                1: During V-Blank
                2: During Searching OAM
                3: During Transferring Data to LCD Driver
         */

        public Byte GetSTATregister() { return Program.emulator.GetMemory().ReadFromMemory(0xFF41); }
        public BitArray GetSTATregisterBits() { return new BitArray(GetSTATregister()); }

        public void setSTATregister(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF41, value); }
        public void setSTATregisterBit(Byte bit, bool toogle)
        {
            // Fix this!
            setSTATregister(toogle ? (Byte)(GetSTATregister() | (1 << bit)) : (Byte)(GetSTATregister() ^ (1 << bit)));
        }

        public LCDmodeFlag getSTATmodeFlag() { return (LCDmodeFlag)(GetSTATregister() & 0x03); }

        public Byte GetSCY() { return Program.emulator.GetMemory().ReadFromMemory(0xFF42); }
        public Byte GetSCX() { return Program.emulator.GetMemory().ReadFromMemory(0xFF43); }

        public void SetSCY(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF42, value); }
        public void SetSCX(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF43, value); }

        public Byte GetLY() { return Program.emulator.GetMemory().ReadFromMemory(0xFF44); }
        public void SetLY(Byte value)
        {
            if(value > 153)
            {
                Program.emulator.GetMemory().WriteToMemory(0xFF44, 0);
                return;
            }
            // Vblank period
            if(value > 143 && !Program.emulator.getCPU().GetInterruptController().IsInQueue(InterruptController.Interrupt.INTERRUPT_VBLANK))
            {
                // Raise Interrupt!
                Program.emulator.getCPU().GetInterruptController().RaiseInterrupt(InterruptController.Interrupt.INTERRUPT_VBLANK);
                //Program.emulator.GetMemory().WriteToMemory(0xFF44, 0);
                return;
            }

            
            Program.emulator.GetMemory().WriteToMemory(0xFF44, value);
        }

        public Byte GetLYC() { return Program.emulator.GetMemory().ReadFromMemory(0xFF45); }

        public bool CompareLYtoLYC() { return GetLY() == GetLYC(); }

        public void Update()
        {
            if(CompareLYtoLYC())
            {
                // STAT INTERRUPT
                Program.emulator.getCPU().GetInterruptController().RaiseInterrupt(InterruptController.Interrupt.INTERRUPT_LCDC);

            }
            // Draw next line
            SetLY((Byte)(GetLY() + 1));


            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DMAController: Update()");
        }
    }
}
