using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Emulator
{
    class Cartridge
    {
        enum cartridgeAddress : UInt16
        {
            CARTRIDGE_ADDRESS_BEGIN = 0x0100,
            CARTRIDGE_HEADER_SIZE = 0x004F,

            // Header Elements
            CARTRIDGE_TITLE = 0x0134,
            CARTRIDGE_COLOR_FLAG = 0x0143,
            CARTRIDGE_CART_TYPE = 0x0147,
            CARTRIDGE_ROM_SIZE = 0x0148,
            CARTRIDGE_RAM_SIZE = 0x0149,

            CARTRIDGE_HEADER_END = CARTRIDGE_ADDRESS_BEGIN+CARTRIDGE_HEADER_SIZE,

        }

        public enum cartridgeType : Byte
        {
            CARTRIDGE_ROM_ONLY = 0x00,
            CARTRIDGE_ROM_MCB1 = 0x01,
            CARTRIDGE_ROM_RAM_MCB1 = 0x02,
            CARTRIDGE_ROM_RAM_MCB1_BATT = 0x03,
            CARTRIDGE_ROM_MBC2 = 0x05,
            CARTRIDGE_ROM_MBC2_BATT = 0x06,
            CARTRIDGE_ROM_RAM = 0x08,
            CARTRIDGE_ROM_RAM_BATT = 0x09,

            CARTRIDGE_ROM_MMM01 = 0x0B,
            CARTRIDGE_ROM_MMM01_SRAM = 0x0C,
            CARTRIDGE_ROM_MMM01_SRAM_BATT = 0x0D,

            CARTRIDGE_ROM_MBC3_RAM = 0x12,
            CARTRIDGE_ROM_MBC3_RAM_BATT = 0x13,

            CARTRIDGE_ROM_MCB5 = 0x19,
            CARTRIDGE_ROM_MCB5_RAM = 0x1A,
            CARTRIDGE_ROM_MCB5_RAM_BATT = 0x1B,

            CARTRIDGE_ROM_MCB5_RUMBLE = 0x1C,
            CARTRIDGE_ROM_MCB5_RUMBLE_SRAM = 0x1D,
            CARTRIDGE_ROM_MCB5_RUMBLE_SRAM_BATT = 0x1E,

            CARTRIDGE_POCKET_CAM = 0x1F,
            CARTRIDGE_TAMA5 = 0xFD,
            CARTRIDGE_HUC3 = 0xFE
        };

        public enum CGB_Flag : Byte
        {
            CGB_FLAG_MONO = 0x00,
            CGB_FLAG_MONO_COLOR = 0x80,
            CGB_FLAG_COLOR_ONLY = 0xC0
        };



        public String GetGameTitle() { return Encoding.Default.GetString(Program.emulator.GetMemory().ReadFromMemory((UInt16)cartridgeAddress.CARTRIDGE_TITLE, 0x0F)); }
        public CGB_Flag GetCGB_Flag() { return (CGB_Flag)Program.emulator.GetMemory().ReadFromMemory((UInt16)cartridgeAddress.CARTRIDGE_COLOR_FLAG); }
        public cartridgeType GetCartridgeType() { return (cartridgeType)Program.emulator.GetMemory().ReadFromMemory((UInt16)cartridgeAddress.CARTRIDGE_CART_TYPE); }
        public Byte GetROMsize() { return Program.emulator.GetMemory().ReadFromMemory((UInt16)cartridgeAddress.CARTRIDGE_ROM_SIZE); }
        public Byte GetRAMsize() { return Program.emulator.GetMemory().ReadFromMemory((UInt16)cartridgeAddress.CARTRIDGE_RAM_SIZE); }

        public void LoadCartridge(String path)
        {
            byte[] header = new byte[0x4F];
            FileStream file = File.OpenRead(path);

            file.Seek((UInt16)cartridgeAddress.CARTRIDGE_ADDRESS_BEGIN, SeekOrigin.Begin);
            file.Read(header, 0, (UInt16)cartridgeAddress.CARTRIDGE_HEADER_SIZE);

            Program.emulator.GetMemory().WriteToMemory((UInt16)cartridgeAddress.CARTRIDGE_ADDRESS_BEGIN, header, (UInt16)header.Length);
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "Loaded Data Header: \n" + BitConverter.ToString(header).Replace("-", " "));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "GAME NAME: " + GetGameTitle() + Environment.NewLine);
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "CARTRIDGE TYPE: " + GetCartridgeType());
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "ROM TYPE: " + GetROMsize());
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "RAM TYPE: " + GetRAMsize());

            // Loading data
            byte[] data = new byte[0x7FFF - (UInt16)cartridgeAddress.CARTRIDGE_HEADER_SIZE];
            file.Seek(0x0147, SeekOrigin.Begin);
            file.Read(data, 0, 0x7FFF - (UInt16)cartridgeAddress.CARTRIDGE_HEADER_SIZE);
            Program.emulator.GetMemory().WriteToMemory(0x0147, data, (UInt16)data.Length);
        }
    }
}
