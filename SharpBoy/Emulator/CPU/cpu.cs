using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBoy
{
    class CPU
    {
        private readonly double CPU_CLOCK = 1/(4.194304 * 1000000);
        private double CPU_CLOCK_MULTIPLY = 10.0;

        private Boolean FAST_CLOCK = true;

        public delegate void InstructionExecutedHandler(object sender, EventArgs e);
        public event InstructionExecutedHandler onInstructionExecute;

        public enum Flag_Register_Bits : Byte
        {
            FLAG_REGISTER_ZERO = 0x07,
            FLAG_REGISTER_SUBSTRACT = 0x06,
            FLAG_REGISTER_H_CARRY = 0x05,
            FLAG_REGISTER_CARRY = 0x04
        };

        // 8 bit registers
        // based on GBCPUman
        Byte reg_a = 0x01;
        Byte reg_b = 0x00;
        Byte reg_c = 0x13;
        Byte reg_d = 0x00;
        Byte reg_e = 0xD8;
        Byte reg_f = 0xB0; // Flags register
        Byte reg_h = 0x01;
        Byte reg_l = 0x4D;

        // 16 bit registers
        UInt16 reg_sp = 0xFFFE;
        UInt16 reg_pc = 0x0100; // Start program location nop instruction in rom and jump instruction

        // Special Paired Registers
        // AF, BC, DE, HL
        // TODO: NOT REQUIRED! REMOVE SOON
        UInt16 reg_af;
        UInt16 reg_bc;
        UInt16 reg_de;
        UInt16 reg_hl;

        public CPU() { }

        // SPECIAL 16bit regs
        public void set_reg_pc(UInt16 address) { reg_pc = address; }
        public UInt16 get_reg_pc() { return reg_pc; }

        public void set_reg_sp(UInt16 value) { reg_sp = value; }
        public UInt16 get_reg_sp() { return reg_sp; }

        // 8Bit regs
        public void set_reg_a(Byte value) { reg_a = value; }
        public Byte get_reg_a() { return reg_a; }

        public void set_reg_b(Byte value) { reg_b = value; }
        public Byte get_reg_b() { return reg_b; }

        public void set_reg_c(Byte value) { reg_c = value; }
        public Byte get_reg_c() { return reg_c; }

        public void set_reg_d(Byte value) { reg_d = value; }
        public Byte get_reg_d() { return reg_d; }

        public void set_reg_e(Byte value) { reg_e = value; }
        public Byte get_reg_e() { return reg_e; }

        // FLAG REGISTER
        public void set_reg_f(Byte value) { reg_f = value; }
        public Byte get_reg_f() { return reg_f; }

        public void set_reg_h(Byte value) { reg_h = value; }
        public Byte get_reg_h() { return reg_h; }

        public void set_reg_l(Byte value) { reg_l = value; }
        public Byte get_reg_l() { return reg_l; }


        // 16 bit registers access
        // FIX THIS!
        public void set_reg_hl(UInt16 value)
        {
            Byte hi = (Byte)(value >> 8);
            Byte lo = (Byte)(value & 0xff);

            set_reg_h(hi);
            set_reg_l(lo);
            reg_hl = value;
        }
        public UInt16 get_reg_hl(){ return (UInt16)((get_reg_h() << 8) + get_reg_l()); }

        public void set_reg_af(UInt16 value)
        {
            Byte hi = (Byte)(value >> 8);
            Byte lo = (Byte)(value & 0xff);

            set_reg_a(hi);
            set_reg_f(lo);
            reg_af = value;
        }
        public UInt16 get_reg_af() { return (UInt16)((get_reg_a() << 8) + get_reg_f()); }

        public void set_reg_bc(UInt16 value)
        {
            Byte hi = (Byte)(value >> 8);
            Byte lo = (Byte)(value & 0xff);

            set_reg_b(hi);
            set_reg_c(lo);
            reg_bc = value;
        }
        public UInt16 get_reg_bc() { return (UInt16)((get_reg_b() << 8) + get_reg_c()); }

        public void set_reg_de(UInt16 value)
        {
            Byte hi = (Byte)(value >> 8);
            Byte lo = (Byte)(value & 0xff);

            set_reg_d(hi);
            set_reg_e(lo);
            reg_de = value;
        }
        public UInt16 get_reg_de() { return (UInt16)((get_reg_d() << 8) + get_reg_e()); }

        public void SetFlagBit(Flag_Register_Bits bitNumber, bool state)
        {
            Byte reg_f = Program.emulator.getCPU().get_reg_f();

            if(state)
                reg_f |= (Byte)(1 << (Byte)bitNumber);
            else reg_f &= (Byte)~(1 << (Byte)bitNumber);

            Program.emulator.getCPU().set_reg_f(reg_f);
        }

        public bool GetFlagBit(Flag_Register_Bits bitNumber)
        {
            return (Program.emulator.getCPU().get_reg_f() & (1 << (Byte)bitNumber)) != 0;
        }

        public bool HaveFlagZero() { return GetFlagBit(Flag_Register_Bits.FLAG_REGISTER_ZERO); }
        public bool HaveFlagSubstract() { return GetFlagBit(Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT); }
        public bool HaveFlagHalfCarry() { return GetFlagBit(Flag_Register_Bits.FLAG_REGISTER_H_CARRY); }
        public bool HaveFlagCarry() { return GetFlagBit(Flag_Register_Bits.FLAG_REGISTER_CARRY); }

        public void exe_ins()
        {
            Byte op = Program.emulator.GetMemory().ReadFromMemory(reg_pc);

            if (!Enum.IsDefined(typeof(Opcodes.Opcode), op))
            {
                Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_WARNING, "UNKNOWN OPCODE OCCURED: " + String.Format("{0:X2}", op)
                    + " At Address: " + String.Format("{0:X4}", reg_pc));

                set_reg_pc((UInt16)(get_reg_pc() + 1));
                return;
            }

            Opcodes.ExecuteOpcode((Opcodes.Opcode)op);

            if (onInstructionExecute == null)
                return;

            EventArgs args = new EventArgs();
            onInstructionExecute(this, args);
        }

        public void Start()
        {
            do
            {
                exe_ins();
            } while (Program.emulator.isRunning);
            
            // Need to implement fast timer for RTC support!
            //Stopwatch timer = new Stopwatch();


        }
    }
}
