﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class OpcodesCB
    {
        private static CPU cpu = Program.emulator.getCPU();

        public enum OpcodeCB : Byte
        {
            OPCODE_CB_SWAP_A = 0x37,

        }

        static Dictionary<OpcodeCB, Action> opcodesCB = new Dictionary<OpcodeCB, Action>()
        {
            { OpcodeCB.OPCODE_CB_SWAP_A, () => cb_swap_a_ins() },
        };

        public static void cb_swap_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            // Is this correct? (Nibble swap)
            Byte hi = (Byte)(cpu.get_reg_a() >> 4);
            cpu.set_reg_a((Byte)((cpu.get_reg_a() << 4) + hi));

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, cpu.get_reg_a() == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void unimplemented_cb_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ExecuteOpcodeCB(OpcodeCB opcode)
        {
            if (opcodesCB[opcode] != null)
                opcodesCB[opcode].Invoke();
            else
            {
                UInt16 address = cpu.get_reg_pc();
                cpu.set_reg_pc((UInt16)(address + 1));
                Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "Trying to execute not implemented opcode: CB " + opcode.ToString());
            }
        }

        public static String GetOpcodeCB(OpcodeCB opcode, ref UInt16 address)
        {
            String str_op = "nop";

            switch(opcode)
            {
                case OpcodeCB.OPCODE_CB_SWAP_A:
                    str_op = "CB SWAP A";
                    address += 2;
                    break;

                default:
                    address += 1;
                    break;
            }
            return str_op;
        }
    }
}
