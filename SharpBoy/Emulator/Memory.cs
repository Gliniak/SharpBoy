using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Emulator
{
    class Memory
    {
        private Byte[] BaseMemory = new Byte[0xFFFF+1];

        public Memory() { }

        public void InitializeMemory()
        {
            for (UInt32 i = 0; i < 0xFFFF+1; i++)
                BaseMemory[i] = 0x00;

            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "INITIALIZE MEMORY TO VALUE: 0x00");
            return;
        }

        public void WriteToMemory(UInt16 Address, Byte Value)
        {
            BaseMemory[Address] = Value;
            return;
        }

        /*
        public void WriteToMemory(UInt16 Address, UInt16 Offset, Byte Value)
        {
            BaseMemory[Address+Offset] = Value;
            return;
        }
        */
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
