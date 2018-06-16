using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class InternalTimer
    {
       // private Memory memory = Program.emulator.GetMemory();

        // SPECIAL ADDRESSES
        private const ushort tima_reg = 0xFF05;
        private const ushort tma_reg  = 0xFF06;
        private const ushort tac_reg  = 0xFF07;
        
        public InternalTimer() { }

        public void SetTIMA(Byte value) { Program.emulator.GetMemory().WriteToMemory(tima_reg, value); } // Time Counter (IRQ during overflow)
        public void SetTMA(Byte value) { Program.emulator.GetMemory().WriteToMemory(tma_reg, value); } // Timer Modulo (Increase when TIMA overflows)
        public void SetTAC(Byte value) { Program.emulator.GetMemory().WriteToMemory(tac_reg, value); } // Only 3bits are used

        public Byte GetTIMA() { return Program.emulator.GetMemory().ReadFromMemory(tima_reg); }
        public Byte GetTMA() { return Program.emulator.GetMemory().ReadFromMemory(tma_reg); }
        public Byte GetTAC() { return Program.emulator.GetMemory().ReadFromMemory(tac_reg); }

        // Input Clock for TAC
        // 00 - 4.096Khz
        // 01 - 262.144Khz
        // 10 - 65.536
        // 11 - 16.384Khz

        public void StartTimer() { SetTAC((Byte)(GetTAC() | 0x04)); }
        public void StopTimer() { SetTAC((Byte)(GetTAC() ^ 0x04)); }
        public bool IsTimerOn() { return (GetTAC() & 0x04) != 0; }

        public void Update()
        {
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "InternalTimer: Update()");
        }
    }
}
