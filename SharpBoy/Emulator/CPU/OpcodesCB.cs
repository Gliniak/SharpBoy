using System;
using System.Collections;
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
            OPCODE_CB_BIT_7_H = 0x7C,
            OPCODE_CB_RES_B_A = 0x87,
            OPCODE_CB_RES_2B_B = 0x50,
            OPCODE_CB_RES_3B_B = 0x58,
            OPCODE_CB_RES_4B_B = 0x60,
            OPCODE_CB_RES_5B_B = 0x68,

            OPCODE_SLA_A = 0x27,

            OPCODE_CB_RL_C_CF = 0x11, // Rotate left register C through carry flag

        }

        static Dictionary<OpcodeCB, Action> opcodesCB = new Dictionary<OpcodeCB, Action>()
        {
            { OpcodeCB.OPCODE_CB_SWAP_A, () => cb_swap_a_ins() },
            { OpcodeCB.OPCODE_CB_BIT_7_H, () => cb_bit_7_h_ins() },
            { OpcodeCB.OPCODE_CB_RES_B_A, () => cb_res_bit_a() },

            { OpcodeCB.OPCODE_CB_RES_2B_B, () => cb_res_bit2_b() },
            { OpcodeCB.OPCODE_CB_RES_3B_B, () => cb_res_bit3_b() },
            { OpcodeCB.OPCODE_CB_RES_4B_B, () => cb_res_bit4_b() },
            { OpcodeCB.OPCODE_CB_RES_5B_B, () => cb_res_bit5_b() },

            { OpcodeCB.OPCODE_SLA_A, () => cb_sla_a_ins() },

            { OpcodeCB.OPCODE_CB_RL_C_CF, () => cb_rl_c_cf_ins() }
        };

        public static void cb_sla_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = cpu.get_reg_a();

            cpu.ResetFlags();

            if ((value & 0x80) != 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, true);

            value <<= 1;

            if(value == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_a(value);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void cb_res_bit5_b()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = cpu.get_reg_b();
            value &= (Byte)(255 - 16);
            cpu.set_reg_b(value);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void cb_res_bit4_b()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = cpu.get_reg_b();
            value &= (Byte)(255 - 8);
            cpu.set_reg_b(value);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void cb_res_bit3_b()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = cpu.get_reg_b();
            value &= (Byte)(255 - 4);
            cpu.set_reg_b(value);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void cb_res_bit2_b()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = cpu.get_reg_b();
            value &= (Byte)(255 - 2);
            cpu.set_reg_b(value);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void cb_res_bit_a()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = cpu.get_reg_a();
            value &= (Byte)(255 - 1);
            cpu.set_reg_a(value);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void cb_rl_c_cf_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Byte carry = cpu.HaveFlagCarry() ? (Byte)(1) : (Byte)(0); // WHY THOU?
            Byte value = cpu.get_reg_c();

            // Clear all flags
            cpu.ResetFlags();

            if ((value & 0x80) != 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, true);

            value <<= 1;
            value |= carry;
            cpu.set_reg_c(value);

            if(value == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void cb_bit_7_h_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            BitArray value = new BitArray(new byte[] { cpu.get_reg_h() });
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, (value.Get(7) == false) ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

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
                    str_op = "[CB] SWAP A";
                    address += 2;
                    break;

                case OpcodeCB.OPCODE_CB_BIT_7_H:
                    str_op = "[CB] BIT 7 H";
                    address += 1;
                    break;

                case OpcodeCB.OPCODE_CB_RL_C_CF:
                    str_op = "[CB] RL C (CF)";
                    address += 1;
                    break;

                case OpcodeCB.OPCODE_CB_RES_B_A:
                    str_op = "[CB] RES A, 0";
                    address += 1;
                    break;
                default:
                    str_op = "[CB] UNKNOWN";
                    address += 1;
                    break;
            }
            return str_op;
        }
    }
}
