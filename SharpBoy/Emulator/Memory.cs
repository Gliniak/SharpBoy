using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class Memory
    {
        private Byte[] BaseMemory = new Byte[0xFFFF+1];

        public Memory() { }

        public void InitializeMemory()
        {
            for (UInt32 i = 0; i < 0xFFFF+1; i++)
                BaseMemory[i] = 0x00;

            WriteToMemory(0xFF10, 0x80);
            WriteToMemory(0xFF11, 0xBF);
            WriteToMemory(0xFF12, 0xF3);
            WriteToMemory(0xFF14, 0xBF);
            WriteToMemory(0xFF16, 0x3F);

            WriteToMemory(0xFF19, 0xBF);
            WriteToMemory(0xFF1A, 0x7F);
            WriteToMemory(0xFF1B, 0xFF);
            WriteToMemory(0xFF1C, 0x9F);
            WriteToMemory(0xFF1E, 0xBF);

            WriteToMemory(0xFF20, 0xFF);
            WriteToMemory(0xFF23, 0xBF);
            WriteToMemory(0xFF24, 0x77);
            WriteToMemory(0xFF25, 0xF3);
            WriteToMemory(0xFF26, 0xF1); // special

            WriteToMemory(0xFF40, 0x91);
            WriteToMemory(0xFF47, 0xFC);
            WriteToMemory(0xFF48, 0xFF);
            WriteToMemory(0xFF49, 0xFF);

            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "INITIALIZE MEMORY TO VALUE: 0x00");
            return;
        }

        public void WriteToMemory(UInt16 Address, Byte Value)
        {
            BaseMemory[Address] = Value;
            return;
        }

        public void WriteToMemory(UInt16 Address, Byte[] Values, UInt16 count)
        {
            for(UInt16 i = 0; i < count; i++)
                WriteToMemory(Convert.ToUInt16(Address + i), Values[i]);
        }

        public Byte ReadFromMemory(UInt16 Address)
        {
            return BaseMemory[Address];
        }

        public Byte[] ReadFromMemory(UInt16 Address, UInt16 length)
        {
            Byte[] data = new Byte[length];
            for (int i = 0; i < length; i++)
                data[i] = ReadFromMemory(Convert.ToUInt16(Address + i));

            return data;
        }

        public Byte[] GetMemory() { return BaseMemory; }


    }
}
