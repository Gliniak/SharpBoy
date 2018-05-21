using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class VideoController
    {
        // 40x32bits OAMobj
        
        class Sprite
        {
            Byte posX;
            Byte posY;
            Byte tile;
            Byte Flags; // Bits 4 to 7

            public Sprite(UInt16 Address)
            {
                if (Address < 0xFE00)
                    return;

                posX = (Byte)(Program.emulator.GetMemory().ReadFromMemory(Address) - 16);
                posY = (Byte)(Program.emulator.GetMemory().ReadFromMemory((UInt16)(Address + 1)) - 8);

                tile = Program.emulator.GetMemory().ReadFromMemory((UInt16)(Address + 2));
                Flags = Program.emulator.GetMemory().ReadFromMemory((UInt16)(Address + 3));
            }

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

            public Byte GetPosX() { return posX; }
            public Byte GetPosY() { return posY; }
            public Byte GetTile() { return tile; }
            public Byte GetFlags() { return Flags; }

            public Byte GetHeight() { return (Byte)(((Byte)(Flags & 0x04) == 0) ? 8 : 16); }
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

        public const Byte GB_WIDTH = 160;
        public const Byte GB_HEIGHT = 144;

        private Int32 cycles = -1;

        public VideoController()
        {
            //SetLCDmode(LCDmodeFlag.LCD_MODE_FLAG_OAM_READ);
        }

        // OAM mem is available only during mode 0-1 (H and Vblank period), However DMA have access to it all time (0xFF46)
        public Byte[] GetOAM() { return Program.emulator.GetMemory().ReadFromMemory(0xFE00, 0x009F); }

        // Nah not actually that good i guess
        public Byte GetLCDCregister() { return Program.emulator.GetMemory().ReadFromMemory(0xFF40); }

        public Boolean GetLCDCregisterBit(Byte bit) { return (GetLCDCregister() & (0x01 << bit)) != 0; }

        // TODO: Bit 7 Check!
        public void SetLCDCregister(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF40, value); }
        public void SetLCDCregisterBit(Byte bit, bool toogle)
        {
            // Fix this!
            SetLCDCregister(toogle ? (Byte)(GetLCDCregister() | (1 << bit)) : (Byte)(GetLCDCregister() ^ (1 << bit)));
        }

        public bool IsDisplayOn() { return (GetLCDCregister() & 0x80) == 0 ? false : true; }

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
        public Boolean GetSTATregisterBit(Byte bit) { return (GetSTATregister() & (0x01 << bit)) != 0; }

        public void setSTATregister(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF41, value); }
        public void setSTATregisterBit(Byte bit, bool toogle)
        {
            // Fix this!
            setSTATregister(toogle ? (Byte)(GetSTATregister() | (1 << bit)) : (Byte)(GetSTATregister() ^ (1 << bit)));
        }

        public void SetLCDmode(LCDmodeFlag mode) { setSTATregister((Byte)mode); }

        public LCDmodeFlag getSTATmodeFlag() { return (LCDmodeFlag)(GetSTATregister() & 0x03); }

        public Byte GetSCY() { return Program.emulator.GetMemory().ReadFromMemory(0xFF42); }
        public Byte GetSCX() { return Program.emulator.GetMemory().ReadFromMemory(0xFF43); }

        public void SetSCY(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF42, value); }
        public void SetSCX(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF43, value); }

        public Byte GetLY() { return Program.emulator.GetMemory().ReadFromMemory(0xFF44); }
        public void SetLY(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF44, value); }

        public Byte GetLYC() { return Program.emulator.GetMemory().ReadFromMemory(0xFF45); }

        public bool CompareLYtoLYC() { return GetLY() == GetLYC(); }

        public Byte GetWY() { return Program.emulator.GetMemory().ReadFromMemory(0xFF4A); }
        public void SetWY(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF4A, value); }

        public Byte GetWX() { return Program.emulator.GetMemory().ReadFromMemory(0xFF4B); }
        public void SetWX(Byte value) { Program.emulator.GetMemory().WriteToMemory(0xFF4B, value); }

        public void Update(Byte cpu_ticks)
        {
            if (!IsDisplayOn())
            {
                // Do what when display is off?
                // In this mode we have full access to GB memory
                return;
            }

            if (cycles != -1)
                cycles += cpu_ticks;

            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_ERROR, "[GPU] Update: " + getSTATmodeFlag().ToString());

            switch (getSTATmodeFlag())
            {
                case LCDmodeFlag.LCD_MODE_FLAG_H_BLANK:
                    lcd_h_blank_update();
                    break;

                case LCDmodeFlag.LCD_MODE_FLAG_V_BLANK:
                    lcd_v_blank_update();
                    break;

                case LCDmodeFlag.LCD_MODE_FLAG_OAM_READ:
                    lcd_oam_read();
                    break;

                case LCDmodeFlag.LCD_MODE_FLAG_DATA_TO_LCD:
                    lcd_send_data_to_lcd();
                    break;
            }
        }

        private void lcd_h_blank_update()
        {
            // Exist for about 51 Machine Cycles
            if (cycles == -1)
                cycles = 0;

            if (cycles >= 51)
            {
                SetLY((Byte)(GetLY() + 1));
                cycles = -1;

                // VBlank period
                SetLCDmode(GetLY() == 143 ? LCDmodeFlag.LCD_MODE_FLAG_V_BLANK : LCDmodeFlag.LCD_MODE_FLAG_OAM_READ);
            }

        }

        private void lcd_v_blank_update()
        {
            // TODO: Better idea required!
            if (cycles == -1)
                cycles = 0;

            // TODO: Not perfectly correct :/
            if (cycles % 114 == 0)
                SetLY((Byte)(GetLY() + 1));

            if (cycles < 1140)
                return;

            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_ERROR, "[GPU] Update: " + getSTATmodeFlag().ToString());

            cycles = -1;

            if (GetLY() == 144 && !Program.emulator.getCPU().GetInterruptController().IsInQueue(InterruptController.Interrupt.INTERRUPT_VBLANK))
                Program.emulator.getCPU().GetInterruptController().RaiseInterrupt(InterruptController.Interrupt.INTERRUPT_VBLANK);

            if (GetLY() > 153)
            {
                SetLY(0);
                
                SetLCDmode(LCDmodeFlag.LCD_MODE_FLAG_OAM_READ);
                Program.emulator.getRenderer().Render();
            }
        }

        private void lcd_oam_read()
        {
            // TODO: Better idea required!
            if (cycles == -1)
                cycles = 0;

            if (cycles < 20)
                return;

            cycles = -1;

            SetLCDmode(LCDmodeFlag.LCD_MODE_FLAG_DATA_TO_LCD);
        }

        private void lcd_send_data_to_lcd()
        {
            if (cycles == -1)
                cycles = 0;

            if (cycles < 40)
                return;

            if (cycles >= 43)
            {
                cycles = -1;
                RenderScanline(GetLY());
                SetLCDmode(LCDmodeFlag.LCD_MODE_FLAG_H_BLANK);
            }
        }

        private void RenderScanline(Byte line)
        {
            if (line >= GB_HEIGHT)
                return;

            if (GetLCDCregisterBit(0))
                RenderBackground(line);

            if (GetLCDCregisterBit(1))
                RenderSprites(line);

        }

        private void RenderBackground(Byte line)
        {
            Byte WX = Program.emulator.GetMemory().ReadFromMemory(0xFF4B);
            Byte WY = Program.emulator.GetMemory().ReadFromMemory(0xFF4A);

            UInt16 tiles = (UInt16)(GetLCDCregisterBit(4) ? 0x8000 : 0x8800);
            UInt16 map = (UInt16)(GetLCDCregisterBit(3) ? 0x9C00 : 0x9800);

            bool windowMode = false;

            if (GetLCDCregisterBit(5) && WY > GetSCY())
                windowMode = true;

            Byte yPos = (Byte)((GetSCY() + line) % 0xFF);

            if (windowMode)
            {
                map = (UInt16)(GetLCDCregisterBit(6) ? 0x9C00 : 0x9800);
                yPos = (Byte)((GetSCY() - WY) % 0xFF);
            }

            UInt16 tileRow = (UInt16)((yPos >> 3) << 5);

            // Scan Whole line 0 to 160 pixels
            for (Byte x = 0; x < GB_WIDTH; x++)
            {
                Byte xPos = (Byte)((x + GetSCX()) % 0xFF);

                if (windowMode && x >= WX)
                    xPos = (Byte)(x - WX);

                UInt16 tile = Program.emulator.GetMemory().ReadFromMemory((UInt16)(map + tileRow + (xPos >> 3)));

                UInt16 tileAdr = (UInt16)(tile << 4);
                Byte tileLine = (Byte)((yPos % 8) << 1);

                Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(tiles + tileLine + tileAdr));
                Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(tiles + tileLine + tileAdr + 1));

                // OpenGL starts rendering from bottom left corner that is why we need to substract 160
                UInt16 position = (UInt16)(((GB_HEIGHT * GB_WIDTH) - 160) - (line * GB_WIDTH) + xPos);

                Byte palette = Program.emulator.GetMemory().ReadFromMemory(0xFF47);

                Byte currentBit = (Byte)(7 - (xPos % 8));

                Byte hiBool = (Byte)((hi & (0x01 << currentBit)) == 0 ? 0 : 2);
                Byte liBool = (Byte)((lo & (0x01 << currentBit)) == 0 ? 0 : 1);

                var color = hiBool | liBool;

                // TODO: Create Custom Color Palettes!

                Byte value = (Byte)(color);
                
                switch (color)
                {
                    case 0:
                        value = 220;
                        break;
                    case 1:
                        value = 66;
                        break;
                    case 2:
                        value = 33;
                        break;
                    case 3:
                        value = 0;
                        break;
                }

                // Switch to match OGL 8888 rgba standard
                UInt32 valFin = (UInt32)(value << 24 | value << 16 | value << 8) + 0xFF;

                Program.emulator.getRenderer().ScreenBuffer[position] = valFin;
            }
        }

        private void RenderSprites(Byte line)
        {

        }
    }
}
