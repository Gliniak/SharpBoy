using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    // AKA LCD CONTROLLER (In CGB)

    /*
     * It suppose to have access to some registers (Memory Registers)
     * Copy memory to OAM
     * Send data to display 
     * Is responsible for: LCDC Interrupt and (i guess) Vblank Interrupt
     */

    class DMAController
    {
        private Boolean enabled = false;
        private UInt16 adrBegin = 0;
        private UInt16 bytesCopied = 0;

        public DMAController() { }

        public Byte GetDMA() { return Program.emulator.GetMemory().ReadFromMemory(0xFF46); }

        public void StartOAM(UInt16 adr)
        {
            enabled = true;
            adrBegin = (UInt16)(adr << 8); // 0x0100
            bytesCopied = 0;
        }

        public void Tick()
        {
            if (!enabled)
                return;

            // It always copies 160 (0xA0) bytes of data
            while(bytesCopied < 160)
            {
                Program.emulator.GetMemory().WriteToMemory((UInt16)(0xFE00 + bytesCopied), Program.emulator.GetMemory().ReadFromMemory((UInt16)(adrBegin + bytesCopied)));
                bytesCopied++;
            }

            enabled = false;
        }
    }
}
