﻿using System;
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
        public struct SixteenBitRegister
        {
            Byte LowerByte;
            Byte UpperByte;

            public void SetLowerByte(Byte value) { LowerByte = value; }
            public void SetUpperByte(Byte value) { UpperByte = value; }

            public Byte GetLowerByte() { return LowerByte; }
            public Byte GetUpperByte() { return UpperByte; }

            public void SetValue(UInt16 value)
            {
                UpperByte = (Byte)(value >> 8);
                LowerByte = (Byte)(value & 0xFF);
            }

            public UInt16 GetValue() { return (UInt16)((UpperByte << 8) | LowerByte); }

            public SixteenBitRegister(Byte hi, Byte lo) { LowerByte = lo; UpperByte = hi; }
        };

        public enum Interrupts
        {
            INTERRUPT_VBLANK = 0x01,
            INTERRUPT_LCDSTAT = 0x02,
            INTERRUPT_TIMER = 0x04,
            INTERRUPT_SERIAL = 0x08,
            INTERRUPT_PAD = 0x10
        };

        public enum Flag_Register_Bits : Byte
        {
            FLAG_REGISTER_ZERO = 0x07,
            FLAG_REGISTER_SUBSTRACT = 0x06,
            FLAG_REGISTER_H_CARRY = 0x05,
            FLAG_REGISTER_CARRY = 0x04
        };

        public Task cpuTask;

        // FOR FUTURE USAGE
        private readonly double CPU_CLOCK = 1/(4.194304 * 1000000);
        private double CPU_CLOCK_MULTIPLY = 10.0;
        private Boolean FAST_CLOCK = true;

        public CPU() { }

        // 16 bit registers
        UInt16 reg_sp = 0xFFFE;
        UInt16 reg_pc = 0x0000; // Start program location nop instruction in rom and jump instruction

        // Info: I removed 8bit register due to problems in fast and logical implementation
        // Special Paired Registers
        // AF, BC, DE, HL
        SixteenBitRegister reg_af = new SixteenBitRegister(0x01, 0xB0);
        SixteenBitRegister reg_bc = new SixteenBitRegister(0x00, 0x13);
        SixteenBitRegister reg_de = new SixteenBitRegister(0x00, 0xD8);
        SixteenBitRegister reg_hl = new SixteenBitRegister(0x01, 0x4D);

        public ref SixteenBitRegister getRegister_af() { return ref reg_af; }
        public ref SixteenBitRegister getRegister_bc() { return ref reg_bc; }
        public ref SixteenBitRegister getRegister_de() { return ref reg_de; }
        public ref SixteenBitRegister getRegister_hl() { return ref reg_hl; }

        // Program counter register
        public void set_reg_pc(UInt16 address) { reg_pc = address; }
        public UInt16 get_reg_pc() { return reg_pc; }

        // Stack Pointer Register
        public void set_reg_sp(UInt16 value) { reg_sp = value; }
        public UInt16 get_reg_sp() { return reg_sp; }

        // 8Bit regs
        public void set_reg_a(Byte value) { reg_af.SetUpperByte(value); }
        public Byte get_reg_a() { return reg_af.GetUpperByte(); }

        public void set_reg_b(Byte value) { reg_bc.SetUpperByte(value); }
        public Byte get_reg_b() { return reg_bc.GetUpperByte(); }

        public void set_reg_c(Byte value) { reg_bc.SetLowerByte(value); }
        public Byte get_reg_c() { return reg_bc.GetLowerByte(); }

        public void set_reg_d(Byte value) { reg_de.SetUpperByte(value); }
        public Byte get_reg_d() { return reg_de.GetUpperByte(); }

        public void set_reg_e(Byte value) { reg_de.SetLowerByte(value); }
        public Byte get_reg_e() { return reg_de.GetLowerByte(); }

        // FLAG REGISTER
        public void set_reg_f(Byte value) { reg_af.SetLowerByte(value); }
        public Byte get_reg_f() { return reg_af.GetLowerByte(); }

        public void set_reg_h(Byte value) { reg_hl.SetUpperByte(value); }
        public Byte get_reg_h() { return reg_hl.GetUpperByte(); }

        public void set_reg_l(Byte value) { reg_hl.SetLowerByte(value); }
        public Byte get_reg_l() { return reg_hl.GetLowerByte(); }

        // 16 bit registers access
        public void set_reg_hl(UInt16 value) { reg_hl.SetValue(value); }
        public UInt16 get_reg_hl(){ return reg_hl.GetValue(); }

        public void set_reg_af(UInt16 value) { reg_af.SetValue(value); }
        public UInt16 get_reg_af() { return reg_af.GetValue(); }

        public void set_reg_bc(UInt16 value){ reg_bc.SetValue(value); }
        public UInt16 get_reg_bc() { return reg_bc.GetValue(); }

        public void set_reg_de(UInt16 value) { reg_de.SetValue(value); }
        public UInt16 get_reg_de() { return reg_de.GetValue(); }

        public Byte getIF_register() { return Program.emulator.GetMemory().ReadFromMemory(0xFF0F); } // To define IR
        public Byte getIE_register() { return Program.emulator.GetMemory().ReadFromMemory(0xFFFF); } // To control IR

        public Byte getTIMA_register() { return Program.emulator.GetMemory().ReadFromMemory(0xFF05); }
        public Byte getTMA_register() { return Program.emulator.GetMemory().ReadFromMemory(0xFF06); }
        public Byte getTAC_register() { return Program.emulator.GetMemory().ReadFromMemory(0xFF07); }

        /*
         The interrupt process is as follows:
            1 When an interrupt is processed, the corresponding IF flag is set.
            2 Interrupt enabled.
            If the IME flag (Interrupt Master Enable) and the corresponding IE flag are set, the interrupt is performed by the following steps.

            3 The IME flag is reset, and all interrupts are prohibited.
            4 The contents of the PC (program counter) are pushed onto the stack RAM.
            5 Control jumps to the interrupt starting address of the interrupt.
        */

        public bool GetFlagBit(Flag_Register_Bits bitNumber) { return (Program.emulator.getCPU().get_reg_f() & (1 << (Byte)bitNumber)) != 0; }

        public bool HaveFlagZero() { return GetFlagBit(Flag_Register_Bits.FLAG_REGISTER_ZERO); }
        public bool HaveFlagSubstract() { return GetFlagBit(Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT); }
        public bool HaveFlagHalfCarry() { return GetFlagBit(Flag_Register_Bits.FLAG_REGISTER_H_CARRY); }
        public bool HaveFlagCarry() { return GetFlagBit(Flag_Register_Bits.FLAG_REGISTER_CARRY); }

        public void ResetFlags() { set_reg_f(0x00); }

        public void SetFlagBit(Flag_Register_Bits bitNumber, bool state)
        {
            Byte reg_f = Program.emulator.getCPU().get_reg_f();

            if (state)
                reg_f |= (Byte)(1 << (Byte)bitNumber);
            else reg_f &= (Byte)~(1 << (Byte)bitNumber);

            Program.emulator.getCPU().set_reg_f(reg_f);
        }

        public void StackPush(SixteenBitRegister register)
        {
            Program.emulator.GetMemory().WriteToMemory(--reg_sp, register.GetUpperByte());
            Program.emulator.GetMemory().WriteToMemory(--reg_sp, register.GetLowerByte());
        }

        public void StackPush(UInt16 value)
        {
            Byte lo = (Byte)(value & 0xFF);
            Byte hi = (Byte)((value >> 8) & 0xFF);

            Program.emulator.GetMemory().WriteToMemory(--reg_sp, hi);
            Program.emulator.GetMemory().WriteToMemory(--reg_sp, lo);
        }

        public void StackPop(ref SixteenBitRegister register)
        {
            Byte lo = Program.emulator.GetMemory().ReadFromMemory(reg_sp++);
            Byte hi = Program.emulator.GetMemory().ReadFromMemory(reg_sp++);

            register.SetLowerByte(lo);
            register.SetUpperByte(hi);
        }

        public void StackPop(ref UInt16 value)
        {
            Byte lo = Program.emulator.GetMemory().ReadFromMemory(reg_sp++);
            Byte hi = Program.emulator.GetMemory().ReadFromMemory(reg_sp++);

            value = ((UInt16)((hi << 8) | lo));
        }

        public void exe_ins()
        {
            Byte op = Program.emulator.GetMemory().ReadFromMemory(reg_pc);

           // Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "[" + String.Format("{0:X4}", get_reg_pc()) + "]: " + String.Format("{0:X2}", op) + "PRE Execute");
            if (!Enum.IsDefined(typeof(Opcodes.Opcode), op))
            {
                Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_WARNING, "UNKNOWN OPCODE OCCURED: " + String.Format("{0:X2}", op)
                    + " At Address: " + String.Format("{0:X4}", reg_pc));

                set_reg_pc((UInt16)(get_reg_pc() + 1));
                return;
            }

            // Special Execute for CB Opcodes
            if((Opcodes.Opcode)op == Opcodes.Opcode.OPCODE_INTERNAL_CB)
            {
                // Increase Program Counter to next location
                set_reg_pc((UInt16)(get_reg_pc() + 1));
                op = Program.emulator.GetMemory().ReadFromMemory(reg_pc);

                OpcodesCB.ExecuteOpcodeCB((OpcodesCB.OpcodeCB)op);
                return;
            }

            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "[" + String.Format("{0:X4}", get_reg_pc()) + "]: " + ((Opcodes.Opcode)op).ToString() + " Executing");
            Opcodes.ExecuteOpcode((Opcodes.Opcode)op);
        }

        // TODO: Need to implement fast timer for RTC support!
        public void Start()
        {
            cpuTask = Task.Run(() =>
            {
                do
                {
                    // Breakpoints support
                    if (Program.emulator.breakPointsList.Count != 0)
                    {
                        if (Program.emulator.breakPointsList.Contains(Program.emulator.getCPU().get_reg_pc()))
                        {
                            Program.emulator.isRunning = false;
                            break;
                        }
                    }

                    if ((Program.emulator.GetMemory().ReadFromMemory(0xFF40) & 0x80) == 0x80)
                    {
                        if(!Program.emulator.getRenderer().isDisplayOn)
                            Program.emulator.getRenderer().Start();

                        // Display start!

                    }
                    // TODO: Interrupts Support here


                    exe_ins();

                } while (Program.emulator.isRunning);
            });
        }


        public void RequestIR() // IRQ
        {

        }

        public void ExecuteIR()
        {

        }
    }
}
