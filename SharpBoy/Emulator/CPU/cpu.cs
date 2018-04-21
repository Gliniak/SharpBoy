using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Emulator
{
    class CPU
    {
        public enum Flag_Register_Bits : Byte
        {
            FLAG_REGISTER_ZERO = 0x07,
            FLAG_REGISTER_SUBSTRACT = 0x06,
            FLAG_REGISTER_H_CARRY = 0x05,
            FLAG_REGISTER_CARRY = 0x04
        };

        // 8 bit registers
        // based on GBCPUman
        Byte reg_a;
        Byte reg_b;
        Byte reg_c;
        Byte reg_d;
        Byte reg_e;
        Byte reg_f; // Flags register
        Byte reg_h;
        Byte reg_l;

        // 16 bit registers
        UInt16 reg_sp = 0xFFFE;
        UInt16 reg_pc = 0x0100; // Start program location nop instruction in rom and jump instruction

        // Special Paired Registers
        // AF, BC, DE, HL
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
        public void set_reg_hl(UInt16 value) { reg_hl = value; }
        public UInt16 get_reg_hl() { return reg_hl; }

        public void set_reg_af(UInt16 value) { reg_af = value; }
        public UInt16 get_reg_af() { return reg_af; }

        public void set_reg_bc(UInt16 value) { reg_bc = value; }
        public UInt16 get_reg_bc() { return reg_bc; }

        public void set_reg_de(UInt16 value) { reg_de = value; }
        public UInt16 get_reg_de() { return reg_de; }

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
            Opcodes.ExecuteOpcode((Opcodes.Opcode)Program.emulator.GetMemory().ReadFromMemory(reg_pc));
        }

        public void Start()
        {
            do
            {
                exe_ins();
            } while (true);
        }
    }
}
