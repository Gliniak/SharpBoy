using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    // My second idea how to wrote it

    /*
    class Opcode
    {
        private Opcodes.Opcode opcode;
        private Byte cycleTime;

        private Action executedFunction;

        public Opcode() { }
        public Opcode(Opcodes.Opcode op, Byte cycles, Action func)
        {
            opcode = op;
            cycleTime = cycles;
            executedFunction = func;
        }

        public void Execute() { executedFunction.Invoke(); }
    }
    */
    class Opcodes
    {
        private static CPU cpu = Program.emulator.getCPU();

        public enum Opcode : Byte
        {
            OPCODE_NOP = 0x00,

            OPCODE_INC_C = 0x0C, // INC C
            OPCODE_INC_L = 0x2C,
            OPCODE_INC_H = 0x24,
            OPCODE_INC_BC = 0x03,
            OPCODE_INC_DE = 0x13,
            OPCODE_DEC_B = 0x05, // DEC B
            OPCODE_DEC_C = 0x0D, // DEC C
            OPCODE_DEC_D = 0x15, // DEC D
            OPCODE_DEC_E = 0x1D, // DEC E

            OPCODE_DEC_BC = 0x0B, // DEC BC
            OPCODE_DEC_HL = 0x35,
            OPCODE_LD_A_n = 0x3E, // LOAD A, n
            OPCODE_LD_B_n = 0x06, // LOAD B, n
            OPCODE_LD_C_n = 0x0E, // LOAD C, n
            OPCODE_LD_D_n = 0x16, // LOAD D, n
            OPCODE_LD_A_B = 0x78, // LOAD A, B
            OPCODE_LD_A_C = 0x79, // LOAD A, C
            OPCODE_LD_C_A = 0x4F, // LOAD C, A

            OPCODE_LD_A_nn = 0xFA,
            OPCODE_LD_A_A = 0x7F, // Why this exist?
            OPCODE_LD_B_A = 0x47,
            OPCODE_LD_A_H = 0x7C,
            OPCODE_LD_ADR_C_A = 0xE2, // LOAD $0xFF00+C, A
            OPCODE_LD_A_HL = 0x7E,
            OPCODE_LD_A_HL_ADD = 0x2A, // LOAD A, HL+ (inc)
            OPCODE_LD_DE_A = 0x12, // LOAD DE, A
            OPCODE_LD_HL_n = 0x36, // LOAD HL, n
            OPCODE_LD_BC_nn = 0x01, // LOAD BC, nn
            OPCODE_LD_DE_nn = 0x11,
            OPCODE_LD_HL_nn = 0x21, // LOAD HL, nn
            OPCODE_LD_ADR_HL_A = 0x77, // LD HL, A
            OPCODE_LD_SP_nn = 0x31, // LOAD SP, nn (SP <- nn)
            OPCODE_LD_nn_A = 0xEA, // LOAD [$xxxx], A
            OPCODE_LDD_HL = 0x32, // LOAD DEC HL, A - Set A in HL and decrease value by 1
            OPCODE_LDH_N_A = 0xE0, // LOAD A to $FF00+n
            OPCODE_LHD_A_N = 0xF0, // LOAD $FF00+n to A
            OPCODE_LD_HLI_A = 0x22,

            // OTHER OPCODES
            OPCODE_JMP = 0xC3,
            OPCODE_DI = 0xF3, // DI - Disable Interrupts
            OPCODE_RRCA = 0x0F, // Rotate A Right
            OPCODE_CP_n = 0xFE,
            OPCODE_EI = 0xFB, // ENABLE INTERRUPTS
            OPCODE_CPL_A = 0x2F, // Flip all bits in reg A

            OPCODE_INTERNAL_CB = 0xCB, // IMPLEMENTED IN FILE OpcodesCB.cs

            OPCODE_OR_B = 0xB0,
            OPCODE_OR_A_C = 0xB1, // OR C
            OPCODE_XOR = 0xAF, // XOR A, A
            OPCODE_XOR_A_C = 0xA9, // XOR A, C
            OPCODE_AND_A_A = 0xA7,
            OPCODE_AND_A_C = 0xA1, // AND A, C
            OPCODE_AND_A_n = 0xE6,

            OPCODE_JR_Z = 0x28, // JR_FLAG_ZERO_RESET
            OPCODE_JP_Z_nn = 0xCA,
            OPCODE_JR_NZ = 0x20, // JR_FLAG_ZERO_RESET
            OPCODE_JP_NZ_cc = 0xC2,

            OPCODE_PUSH_AF = 0xF5, // PUSH SP, AF THEN SP-2
            OPCODE_PUSH_BC = 0xC5, // PUSH SP, BC THEN SP-2
            OPCODE_PUSH_DE = 0xD5,
            OPCODE_PUSH_HL = 0xE5,

            OPCODE_POP_HL = 0xE1,
            OPCODE_POP_DE = 0xD1,
            OPCODE_POP_BC = 0xC1,
            OPCODE_POP_AF = 0xF1,

            OPCODE_ADD_A_B = 0x80,
            OPCODE_ADD_A_A = 0x87,

            OPCODE_CALL_ADDRESS = 0xCD,
            OPCODE_RET = 0xC9,
            OPCODE_RET_NC = 0xD0,
            OPCODE_RST_28H = 0xEF,

            OPCODE_SP_HL = 0xF9,
            OPCODE_LD_A_DE = 0x1A,
            OPCODE_INC_HL = 0x23,

            OPCODE_RL_A_CF = 0x17, // Rotate left register A carry flag
            OPCODE_LD_A_E = 0x7B,
            OPCODE_DEC_A = 0x3D,
            OPCODE_LD_HL_A = 0x67,
            OPCODE_LD_D_A = 0x57,

            OPCODE_INC_B = 0x04,
            OPCODE_LD_E_n = 0x1E,

            OPCODE_SUB_A_B = 0x90, // A =- B
            OPCODE_JR_n = 0x18, // Jump to ADR +/- n
            OPCODE_CP_A_HL = 0xBE,
            // TO DO!

            //OPCODE_CP_A_A = 0xBF, // COMPARE A, A
            //OPCODE_CCF = 0x3F, // CARRY FLAG


        }

        static Dictionary<Opcode, Action> opcodes = new Dictionary<Opcode, Action>()
        {
            { Opcode.OPCODE_NOP, () => nop_instruction() },
            { Opcode.OPCODE_JMP, () => jmp_instruction() },
            { Opcode.OPCODE_XOR, () => xor_a_ins() },
            { Opcode.OPCODE_LD_HL_nn, () => ld_hl_nn_ins() },
            { Opcode.OPCODE_LD_C_n, () => ld_c_ins() },
            { Opcode.OPCODE_LD_B_n, () => ld_b_ins() },
            { Opcode.OPCODE_LDD_HL, () => ldd_hl_ins() },
            { Opcode.OPCODE_DEC_B, () => dec_b_ins() },
            { Opcode.OPCODE_JR_NZ, () => jr_nz_ins() },
            { Opcode.OPCODE_DEC_C, () => dec_c_ins() },
            { Opcode.OPCODE_LD_A_n, () => ld_a_n_ins() },
            { Opcode.OPCODE_DI, () => di_ins() },
            { Opcode.OPCODE_LDH_N_A, () => ldh_n_a_ins() },
            { Opcode.OPCODE_LHD_A_N, () => ldh_a_n_ins() },
            { Opcode.OPCODE_RRCA, () => rrca_ins() },
            { Opcode.OPCODE_CP_n, () => cp_n_ins() },
            { Opcode.OPCODE_LD_HL_n, () => ld_hl_n_ins() },
            { Opcode.OPCODE_LD_nn_A, () => ld_nn_a_ins() },
            { Opcode.OPCODE_LD_SP_nn, () => ld_sp_nn_ins() },
            { Opcode.OPCODE_LD_A_HL_ADD, () => ld_a_hl_add_ins() },
            { Opcode.OPCODE_LD_ADR_C_A, () => ld_adr_c_a_ins() },
            { Opcode.OPCODE_INC_C, () => inc_c_ins() },
            { Opcode.OPCODE_CALL_ADDRESS, () => call_adr_ins() },
            { Opcode.OPCODE_LD_BC_nn, () => ld_bc_nn_ins() },
            { Opcode.OPCODE_DEC_BC, () => dec_bc_ins() },
            { Opcode.OPCODE_LD_A_B, () => ld_a_b_ins() },
            { Opcode.OPCODE_OR_A_C, () => or_a_c_ins() },
            { Opcode.OPCODE_RET, () => ret_ins() },
            { Opcode.OPCODE_LD_DE_A, () => ld_de_a_ins() },
            { Opcode.OPCODE_INC_DE, () => inc_de_ins() },
            { Opcode.OPCODE_LD_DE_nn, () => ld_de_nn_ins() },
            { Opcode.OPCODE_LD_A_A, () => ld_a_a_ins() },
            { Opcode.OPCODE_CPL_A, () => cpl_a_ins() },
            { Opcode.OPCODE_AND_A_n, () => and_a_n_ins() },
            { Opcode.OPCODE_OR_B, () => or_a_b_ins() },
            { Opcode.OPCODE_LD_C_A, () => ld_c_a_ins() },
            { Opcode.OPCODE_XOR_A_C, () => xor_a_c_ins() },
            { Opcode.OPCODE_AND_A_C, () => and_a_c_ins() },
            { Opcode.OPCODE_LD_A_C, () => ld_a_c_ins() },
            { Opcode.OPCODE_LD_ADR_HL_A, () => ld_adr_hl_a_ins() },
            { Opcode.OPCODE_INC_BC, () => inc_bc_ins() },
            { Opcode.OPCODE_PUSH_AF, () => push_af_ins() },
            { Opcode.OPCODE_PUSH_BC, () => push_bc_ins() },
            { Opcode.OPCODE_PUSH_DE, () => push_de_ins() },
            { Opcode.OPCODE_PUSH_HL, () => push_hl_ins() },
            { Opcode.OPCODE_LD_A_nn, () => ld_a_nn_ins() },
            { Opcode.OPCODE_JR_Z, () => jr_z_ins() },
            { Opcode.OPCODE_AND_A_A, () => and_a_a_ins() },
            { Opcode.OPCODE_DEC_HL, () => dec_hl_ins() },
            { Opcode.OPCODE_LD_A_HL, () => ld_a_hl_ins() },
            { Opcode.OPCODE_POP_HL, () => pop_hl_ins() },
            { Opcode.OPCODE_POP_DE, () => pop_de_ins() },
            { Opcode.OPCODE_POP_BC, () => pop_bc_ins() },
            { Opcode.OPCODE_POP_AF, () => pop_af_ins() },
            { Opcode.OPCODE_ADD_A_B, () => add_a_b_ins() },
            { Opcode.OPCODE_JP_Z_nn, () => jp_z_nn_ins() },
            { Opcode.OPCODE_INC_L, () => inc_l_ins() },
            { Opcode.OPCODE_ADD_A_A, () => add_a_a_ins() },
            { Opcode.OPCODE_RET_NC, () => ret_nc_ins() },
            { Opcode.OPCODE_LD_HLI_A, () => ld_hli_a_ins() },
            { Opcode.OPCODE_RST_28H, () => rst_28h_ins() },
            { Opcode.OPCODE_JP_NZ_cc, () => jp_nz_cc_ins() },
            { Opcode.OPCODE_SP_HL, () => sp_hl_ins() },
            { Opcode.OPCODE_LD_B_A, () => ld_b_a_ins() },
            { Opcode.OPCODE_LD_A_DE, () => ld_a_de_ins() },
            { Opcode.OPCODE_INC_HL, () => inc_hl_ins() },
            { Opcode.OPCODE_RL_A_CF, () => rl_a_cf_ins() },
            { Opcode.OPCODE_LD_A_E, () => ld_a_e_ins() },
            { Opcode.OPCODE_DEC_A, () => dec_a_ins() },
            { Opcode.OPCODE_LD_HL_A, () => ld_hl_a_ins() },
            { Opcode.OPCODE_LD_D_A, () => ld_d_a_ins() },
            { Opcode.OPCODE_INC_B, () => inc_b_ins() },
            { Opcode.OPCODE_LD_E_n, () => ld_e_n_ins() },
            { Opcode.OPCODE_DEC_E, () => dec_e_ins() },
            { Opcode.OPCODE_INC_H, () => inc_h_ins() },
            { Opcode.OPCODE_LD_A_H, () => ld_a_h_ins() },
            { Opcode.OPCODE_DEC_D, () => dec_d_ins() },
            { Opcode.OPCODE_SUB_A_B, () => sub_a_b_ins() },
            { Opcode.OPCODE_LD_D_n, () => ld_d_ins() },
            { Opcode.OPCODE_JR_n, () => jr_n_ins() },
            { Opcode.OPCODE_CP_A_HL, () => cp_a_hl_ins() },

            // Add to Dissasembler
            { Opcode.OPCODE_EI, () => enable_interrupts() },

        };

        public static void cp_a_hl_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = Program.emulator.GetMemory().ReadFromMemory(cpu.get_reg_hl());

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            if (cpu.get_reg_a() == value)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            if (cpu.get_reg_a() < value)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, true);

            if (((cpu.get_reg_a() - value) & 0x0F) > (cpu.get_reg_a() & 0x0F))
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void jr_n_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            SByte value = ((SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1)));
            cpu.set_reg_pc((UInt16)(address + value + 2));
        }

        public static void ld_d_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_d(Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1)));
            cpu.set_reg_pc((UInt16)(address + 2));
        }

        public static void sub_a_b_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Byte value = (Byte)(cpu.get_reg_a() - cpu.get_reg_b());

            // Is This ok?
            Byte carry = (Byte)((cpu.get_reg_a() ^ cpu.get_reg_b()) ^ value);
            cpu.set_reg_a(value);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, cpu.get_reg_a() == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, ((carry & 0x0100) != 0) ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, ((carry & 0x0010) != 0) ? true : false);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void dec_d_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_d() - 1);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, cpu.HaveFlagCarry() ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            cpu.set_reg_d(value);

            if ((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void enable_interrupts()
        {
            UInt16 address = cpu.get_reg_pc();
            // Is it really works that way?
            Program.emulator.GetMemory().WriteToMemory(0xFFFF, 0x1F);
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void unimplemented_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_a_h_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_a(cpu.get_reg_h());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void inc_h_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_h() + 1);
            cpu.set_reg_h(value);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            if ((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void dec_e_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_e() - 1);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, cpu.HaveFlagCarry() ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            cpu.set_reg_e(value);

            if ((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_e_n_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_e(Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1)));
            cpu.set_reg_pc((UInt16)(address + 2));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD A,n INSTRUCTION EXECUTED");
        }

        public static void inc_b_ins()
        {
            // TODO: FLAGS
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_b() + 1);
            cpu.set_reg_b(value);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            if ((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_d_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_d(cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_hl_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_hl(cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void dec_a_ins()
        {
            // ANY FLAGS?
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_a() - 1);
            cpu.set_reg_a(value);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            if ((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            if (value == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(address + 1));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DEC HL INSTRUCTION EXECUTED");
        }


        public static void ld_a_e_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_a(cpu.get_reg_e());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void rl_a_cf_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte carry = cpu.HaveFlagCarry() ? (Byte)(1) : (Byte)(0); // WHY THOU?
            Byte value = cpu.get_reg_a();

            // Clear all flags
            cpu.ResetFlags();

            if ((value & 0x80) != 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, true);

            value <<= 1;
            value |= carry;
            cpu.set_reg_a(value);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void inc_hl_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_hl((UInt16)(cpu.get_reg_hl() + 1));
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_a_de_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            // Is it ok? Shouldn't it load pointer value?
            //cpu.set_reg_a(cpu.get_reg_e());
            cpu.set_reg_a(Program.emulator.GetMemory().ReadFromMemory(cpu.get_reg_de()));

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_b_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_b(cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void sp_hl_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.StackPush(cpu.get_reg_hl());
            cpu.set_reg_pc((UInt16)(address + 1));
        }


        public static void jp_nz_cc_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            if(!cpu.HaveFlagZero())
            {
                Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));
                Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));

                UInt16 value = Convert.ToUInt16((hi << 8) + lo);
                // Why I need to Add 2?
                cpu.set_reg_pc(value);
                return;
            }

            cpu.set_reg_pc((UInt16)(address + 3));
        }

        public static void rst_28h_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.StackPush(cpu.get_reg_pc());
            cpu.set_reg_pc(0x0028);
        }

        public static void ld_hli_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Program.emulator.GetMemory().WriteToMemory(cpu.get_reg_hl(), cpu.get_reg_a());
            cpu.set_reg_hl((UInt16)(cpu.get_reg_hl() + 1));

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ret_nc_ins()
        {
            if (cpu.HaveFlagCarry())
            {
                cpu.set_reg_pc((UInt16)(cpu.get_reg_pc() + 1));
                return;
            }

            UInt16 address = cpu.get_reg_pc();

            cpu.StackPop(ref address);
            cpu.set_reg_pc(address);
        }

        public static void add_a_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            UInt16 value = (UInt16)(cpu.get_reg_a() + cpu.get_reg_a());

            // Is This ok?
            Byte carry = (Byte)((cpu.get_reg_a() ^ cpu.get_reg_a()) ^ value);
            cpu.set_reg_a((Byte)(value));

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, cpu.get_reg_a() == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, ((carry & 0x0100) != 0) ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, ((carry & 0x0010) != 0) ? true : false);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void inc_l_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_l() + 1);
            cpu.set_reg_l(value);
            
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            if ((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void jp_z_nn_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            if (cpu.GetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO))
            {
                Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));
                Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));

                UInt16 value = Convert.ToUInt16((hi << 8) + lo);
                // Why I need to Add 2?
                cpu.set_reg_pc(value);
                return;
            }

            cpu.set_reg_pc((UInt16)(address + 3));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "JUMP (Z) NN INSTRUCTION EXECUTED");
        }

        public static void add_a_b_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            // Is This ok?
            Byte carry = (Byte)(cpu.get_reg_a() ^ cpu.get_reg_b() ^ (cpu.get_reg_a() + cpu.get_reg_b()));
            cpu.set_reg_a((Byte)(cpu.get_reg_a() + cpu.get_reg_b()));
            
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, cpu.get_reg_a() == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, ((carry & 0x0100) != 0) ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, ((carry & 0x0010) != 0) ? true : false);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void pop_af_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.StackPop(ref cpu.getRegister_af());

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void pop_bc_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.StackPop(ref cpu.getRegister_bc());

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void pop_de_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.StackPop(ref cpu.getRegister_de());

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void pop_hl_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.StackPop(ref cpu.getRegister_hl());

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_a_hl_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_a(Program.emulator.GetMemory().ReadFromMemory(cpu.get_reg_hl()));
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void dec_hl_ins()
        {
            // ANY FLAGS?
            UInt16 address = cpu.get_reg_pc();

            UInt16 value = (UInt16)(cpu.get_reg_hl() - 1);
            cpu.set_reg_hl(value);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            if ((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            if (value == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(address + 1));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DEC HL INSTRUCTION EXECUTED");
        }

        public static void and_a_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_a() & cpu.get_reg_a());
            cpu.set_reg_a(value);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            if (value == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void jr_z_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            if (cpu.GetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO))
            {
                Byte add_pc = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                // Why I need to Add 2?
                cpu.set_reg_pc((UInt16)((address + (SByte)(add_pc)) + 2));
                return;
            }

            cpu.set_reg_pc((UInt16)(address + 2));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "JR_Z INSTRUCTION EXECUTED");
        }

        public static void ld_a_nn_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            
            // CANT WE JUST USE LOWER BYTE?
            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 value = Convert.ToUInt16((hi << 8) + lo);

            cpu.set_reg_a((Byte)value);
            cpu.set_reg_pc((UInt16)(address + 3));
        }

        public static void push_hl_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.StackPush(cpu.get_reg_hl());

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void push_de_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.StackPush(cpu.get_reg_de());

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void push_bc_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.StackPush(cpu.get_reg_bc());

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void push_af_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.StackPush(cpu.get_reg_af());

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void inc_bc_ins()
        {
            // FLAGS?
            UInt16 address = cpu.get_reg_pc();



            cpu.set_reg_bc((UInt16)(cpu.get_reg_bc() + 1));
            cpu.set_reg_pc((UInt16)(address + 1));
        }


        public static void ld_adr_hl_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Program.emulator.GetMemory().WriteToMemory(cpu.get_reg_hl(), cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 1));
        }


        public static void ld_a_c_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_a(cpu.get_reg_c());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void and_a_c_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_a() & cpu.get_reg_c());
            cpu.set_reg_a(value);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            if (value == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void xor_a_c_ins()
        {
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            Byte reg_a = cpu.get_reg_a();
            Byte reg_c = cpu.get_reg_c();

            cpu.set_reg_a(Convert.ToByte(reg_a ^ reg_c));

            if (cpu.get_reg_a() == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc(Convert.ToUInt16(cpu.get_reg_pc() + 1));
        }

        public static void ld_c_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_c(cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void or_a_b_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            cpu.set_reg_a((Byte)(cpu.get_reg_a() | cpu.get_reg_b()));

            if (cpu.get_reg_a() == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void and_a_n_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_a() & (Byte)(cpu.get_reg_pc()));
            cpu.set_reg_a(value);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            if(value == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void cpl_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            cpu.set_reg_a((Byte)(~cpu.get_reg_a()));
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_a_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_a(cpu.get_reg_a()); // WHY?
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_de_nn_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 value = Convert.ToUInt16((hi << 8) + lo);

            cpu.set_reg_de(value);
            cpu.set_reg_pc((UInt16)(address + 3));
        }

        public static void inc_de_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_de((UInt16)(cpu.get_reg_de() + 1));
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_de_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_de(cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ret_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            // Better Way for this!
            Byte lo = Program.emulator.GetMemory().ReadFromMemory(cpu.get_reg_sp(), 1)[0];
            cpu.set_reg_sp((UInt16)(cpu.get_reg_sp() + 1));
            Byte hi = Program.emulator.GetMemory().ReadFromMemory(cpu.get_reg_sp(), 1)[0];
            cpu.set_reg_sp((UInt16)(cpu.get_reg_sp() + 1));

            cpu.set_reg_pc((UInt16)(hi << 8 | lo));
            //cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void or_a_c_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            cpu.set_reg_a((Byte)(cpu.get_reg_a() | cpu.get_reg_c()));

            if(cpu.get_reg_a() == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_a_b_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_a(cpu.get_reg_b());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void dec_bc_ins()
        {
            // ANY FLAGS?
            UInt16 address = cpu.get_reg_pc();

            UInt16 value = (UInt16)(cpu.get_reg_bc() - 1);
            cpu.set_reg_bc(value);

            cpu.set_reg_pc((UInt16)(address + 1));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DEC BC INSTRUCTION EXECUTED");
        }

        public static void ld_bc_nn_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 value = Convert.ToUInt16((hi << 8) + lo);

            cpu.set_reg_bc(value);
            cpu.set_reg_pc((UInt16)(address + 3));
        }

        public static void call_adr_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.StackPush((UInt16)(address+3));

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 value = Convert.ToUInt16((hi << 8) + lo);
            cpu.set_reg_pc(value);
        }

        public static void inc_c_ins()
        {
            // TODO: FLAGS
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_c() + 1);
            cpu.set_reg_c(value);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            if ((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);

            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_adr_c_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Program.emulator.GetMemory().WriteToMemory((UInt16)(0xFF00 + cpu.get_reg_c()), cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_a_hl_add_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_a(Program.emulator.GetMemory().ReadFromMemory(cpu.get_reg_hl()));
            cpu.set_reg_hl((UInt16)(cpu.get_reg_hl()+1));
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_sp_nn_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 value = Convert.ToUInt16((hi << 8) + lo);

            cpu.set_reg_sp(value);
            cpu.set_reg_pc((UInt16)(address + 3));
        }

        public static void ld_nn_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 newAddress = Convert.ToUInt16((hi << 8) + lo);

            Program.emulator.GetMemory().WriteToMemory(newAddress, cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 3));
        }


        public static void ld_hl_n_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Byte value = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            cpu.set_reg_hl(value);
            cpu.set_reg_pc((UInt16)(address + 2));
        }

        public static void cp_n_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            
            Byte value = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            if (cpu.get_reg_a() == value)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            if (cpu.get_reg_a() < value)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, true);

            if(((cpu.get_reg_a() - value) & 0x0F) > (cpu.get_reg_a() & 0x0F))
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.set_reg_pc((UInt16)(address + 2));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "CP A, n INSTRUCTION EXECUTED");
        }

        public static void rrca_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, (cpu.get_reg_a() & 1) == 1);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, false); // Do we have to clear it?

            cpu.set_reg_a((Byte)(cpu.get_reg_a() >> 1));

            if(cpu.get_reg_a() == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(address + 1));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "RRCA INSTRUCTION EXECUTED");
        }


        public static void ldh_a_n_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Byte value = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            cpu.set_reg_a(Program.emulator.GetMemory().ReadFromMemory((UInt16)(0xFF00 + value)));
            cpu.set_reg_pc((UInt16)(address + 2));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LDH A, [$FF00+n] INSTRUCTION EXECUTED");
        }

        public static void ldh_n_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Byte value = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            Program.emulator.GetMemory().WriteToMemory((UInt16)(0xFF00 + value), cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 2));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LDH [$FF00+n],A INSTRUCTION EXECUTED");
        }

        public static void di_ins()
        {
            // TODO: NOT IMPLEMENTED!
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_pc((UInt16)(address + 1));
            return;
        }

        public static void ld_a_n_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_a(Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1)));
            cpu.set_reg_pc((UInt16)(address + 2));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD A,n INSTRUCTION EXECUTED");
        }

        public static void jr_nz_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            if (!cpu.GetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO))
            {
                Byte add_pc = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                cpu.set_reg_pc((UInt16)((address + (SByte)(add_pc)) + 2));
                return;
            }

            cpu.set_reg_pc((UInt16)(address + 2));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "JR_NZ INSTRUCTION EXECUTED");
        }

        public static void nop_instruction()
        {
            cpu.set_reg_pc(Convert.ToUInt16(cpu.get_reg_pc() + 1));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "NOP INSTRUCTION EXECUTED");
        }

        public static void jmp_instruction()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 newAddress = Convert.ToUInt16((hi << 8) + lo);

           // Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "JMP INSTRUCTION EXECUTED AT ADDRESS: " + address.ToString() + " TO ADDRESS: " + newAddress.ToString());
            cpu.set_reg_pc(newAddress);
        }

        public static void xor_a_ins()
        {
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            Byte reg_a = cpu.get_reg_a();
            cpu.set_reg_a((Byte)(reg_a ^ reg_a));

            if (cpu.get_reg_a() == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc((UInt16)(cpu.get_reg_pc() + 1));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "XOR_A INSTRUCTION EXECUTED");
        }

        public static void ld_hl_nn_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 value = Convert.ToUInt16((hi << 8) + lo);

            cpu.set_reg_hl(value);
            cpu.set_reg_pc((UInt16)(address + 3));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD HL INSTRUCTION EXECUTED");
        }


        public static void ld_c_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_c(Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1)));
            cpu.set_reg_pc((UInt16)(address + 2));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD C INSTRUCTION EXECUTED");
        }

        public static void ld_b_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_b(Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1)));
            cpu.set_reg_pc((UInt16)(address + 2));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD B INSTRUCTION EXECUTED");
        }

        public static void ldd_hl_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            //cpu.set_reg_hl((UInt16)(cpu.get_reg_a()));
            Program.emulator.GetMemory().WriteToMemory(cpu.get_reg_hl(), cpu.get_reg_a());
            cpu.set_reg_hl((UInt16)(cpu.get_reg_hl() - 1));

            cpu.set_reg_pc((UInt16)(address + 1));
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD HL, A DEC A INSTRUCTION EXECUTED");
        }

        public static void dec_b_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_b() - 1);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, cpu.HaveFlagCarry() ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            if ((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.set_reg_b(value);
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void dec_c_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_c() - 1);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, cpu.HaveFlagCarry() ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            cpu.set_reg_c(value);

            if((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.set_reg_pc((UInt16)(address + 1));
        }


        public static void ExecuteOpcode(Opcode opcode)
        {
            if (opcodes[opcode] != null)
                opcodes[opcode].Invoke();
            else
            {
                UInt16 address = cpu.get_reg_pc();
                cpu.set_reg_pc((UInt16)(address + 1));
                Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "Trying to execute not implemented opcode: " + opcode.ToString());
            }
        }

        public static String GetOpcode(Opcode opcode, ref UInt16 address)
        {
            String str_op = "nop";

            SByte lo = 0;
            SByte hi = 0;

            switch(opcode)
            {
                case Opcode.OPCODE_NOP:
                    str_op = "NOP";
                    address++;
                    break;

                case Opcode.OPCODE_JMP:
                    str_op = "JMP ";

                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    hi = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", lo) + String.Format("{0:X2}", hi) + "h";
                    address += 3;
                    break;

                case Opcode.OPCODE_XOR:
                    str_op = "XOR A";
                    address++;
                    break;

                case Opcode.OPCODE_LD_HL_nn:
                    str_op = "LD HL, ";

                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    hi = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", lo) + String.Format("{0:X2}", hi) + "h";
                    address += 3;
                    break;

                case Opcode.OPCODE_DEC_B:
                    str_op = "DEC B";
                    address++;
                    break;

                case Opcode.OPCODE_DEC_C:
                    str_op = "DEC C";
                    address++;
                    break;

                case Opcode.OPCODE_JR_NZ:
                    str_op = "JR NZ, ";
                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));
                    str_op += String.Format("{0:X4}", Convert.ToUInt16(address + lo + 2)) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_LDD_HL:
                    str_op = "LDD HL, A";
                    address++;
                    break;

                case Opcode.OPCODE_LD_A_n:
                    str_op = "LD A, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1))) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_LD_B_n:
                    str_op = "LD B, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1))) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_LD_C_n:
                    str_op = "LD C, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1))) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_DI:
                    str_op = "DI";
                    address++;
                    break;

                case Opcode.OPCODE_RRCA:
                    str_op = "RRCA A";
                    address++;
                    break;

                case Opcode.OPCODE_LDH_N_A:
                    str_op = "LDH [$";
                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));
                    str_op += String.Format("{0:X4}", 0xFF00 + (Byte)lo) + "], A";
                    address += 2;
                    break;

                case Opcode.OPCODE_LHD_A_N:
                    str_op = "LDH A, [$";
                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));
                    str_op += String.Format("{0:X4}", 0xFF00 + (Byte)lo) + "]";
                    address += 2;
                    break;

                case Opcode.OPCODE_CP_n:
                    str_op = "CP A, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1))) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_LD_HL_n:
                    str_op = "LD HL, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1))) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_LD_nn_A:
                    str_op = "LD [$";

                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    hi = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", lo) + String.Format("{0:X2}", hi) + "], A";
                    address += 3;
                    break;

                case Opcode.OPCODE_LD_SP_nn:
                    str_op = "LD SP, ";

                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    hi = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", lo) + String.Format("{0:X2}", hi);
                    address += 3;
                    break;

                case Opcode.OPCODE_LD_A_HL_ADD:
                    str_op = "LD A, HL+";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_ADR_C_A:
                    str_op = "LD [$FF00+C], A";
                    address += 1;
                    break;

                case Opcode.OPCODE_INC_C:
                    str_op = "INC C";
                    address += 1;
                    break;

                case Opcode.OPCODE_CALL_ADDRESS:
                    str_op = "CALL, ";

                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    hi = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", lo) + String.Format("{0:X2}", hi);
                    address += 3;
                    break;

                case Opcode.OPCODE_LD_BC_nn:
                    str_op = "LD BC, ";

                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    hi = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", lo) + String.Format("{0:X2}", hi);
                    address += 3;
                    break;

                case Opcode.OPCODE_DEC_BC:
                    str_op = "DEC BC";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_A_B:
                    str_op = "LD A, B";
                    address += 1;
                    break;

                case Opcode.OPCODE_OR_A_C:
                    str_op = "OR A, C";
                    address += 1;
                    break;

                case Opcode.OPCODE_RET:
                    str_op = "RET $" + String.Format("{0:X4}", cpu.get_reg_sp());
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_DE_A:
                    str_op = "LOAD DE, A";
                    address += 1;
                    break;

                case Opcode.OPCODE_INC_DE:
                    str_op = "INC DE";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_DE_nn:
                    str_op = "LD DE, ";

                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    hi = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", lo) + String.Format("{0:X2}", hi);
                    address += 3;
                    break;

                case Opcode.OPCODE_LD_A_A:
                    str_op = "LD A, A";
                    address += 1;
                    break;

                case Opcode.OPCODE_CPL_A:
                    str_op = "CPL A";
                    address += 1;
                    break;

                case Opcode.OPCODE_AND_A_n:
                    str_op = "AND A, ";
                    address += 2;
                    break;

                case Opcode.OPCODE_OR_B:
                    str_op = "OR A, B";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_C_A:
                    str_op = "LD C, A";
                    address += 1;
                    break;

                case Opcode.OPCODE_XOR_A_C:
                    str_op = "XOR A, C";
                    address += 1;
                    break;

                case Opcode.OPCODE_AND_A_C:
                    str_op = "AND A, C";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_A_C:
                    str_op = "LD A, C";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_ADR_HL_A:
                    str_op = "LD [HL], A";
                    address += 1;
                    break;

                case Opcode.OPCODE_INC_BC:
                    str_op = "INC BC";
                    address += 1;
                    break;

                case Opcode.OPCODE_PUSH_AF:
                    str_op = "PUSH SP, AF";
                    address += 1;
                    break;
                case Opcode.OPCODE_PUSH_BC:
                    str_op = "PUSH SP, BC";
                    address += 1;
                    break;

                case Opcode.OPCODE_PUSH_DE:
                    str_op = "PUSH SP, DE";
                    address += 1;
                    break;

                case Opcode.OPCODE_PUSH_HL:
                    str_op = "PUSH SP, HL";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_A_nn:
                    str_op = "LD A, ";

                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    hi = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", lo) + String.Format("{0:X2}", hi);
                    address += 3;
                    break;


                case Opcode.OPCODE_JR_Z:
                    str_op = "JR Z, ";
                    lo = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));
                    str_op += String.Format("{0:X4}", Convert.ToUInt16(address + lo + 2)) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_AND_A_A:
                    str_op = "AND A, A";
                    address += 1;
                    break;

                case Opcode.OPCODE_DEC_HL:
                    str_op = "DEC HL";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_A_HL:
                    str_op = "LD A, HL";
                    address += 1;
                    break;


                case Opcode.OPCODE_POP_HL:
                    str_op = "POP HL";
                    address += 1;
                    break;
                case Opcode.OPCODE_POP_DE:
                    str_op = "POP DE";
                    address += 1;
                    break;
                case Opcode.OPCODE_POP_BC:
                    str_op = "POP BC";
                    address += 1;
                    break;
                case Opcode.OPCODE_POP_AF:
                    str_op = "POP AF";
                    address += 1;
                    break;

                case Opcode.OPCODE_ADD_A_B:
                    str_op = "ADD A, B";
                    address += 1;
                    break;

                case Opcode.OPCODE_JP_Z_nn:
                    str_op = "JUMP (Z), NN";
                    address += 3;
                    break;

                case Opcode.OPCODE_INC_L:
                    str_op = "INC L";
                    address += 1;
                    break;

                case Opcode.OPCODE_ADD_A_A:
                    str_op = "ADD A, A";
                    address += 1;
                    break;

                case Opcode.OPCODE_RET_NC:
                    str_op = "RET NC";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_HLI_A:
                    str_op = "LD HLI, A";
                    address += 1;
                    break;

                case Opcode.OPCODE_RST_28H:
                    str_op = "RST 28H";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_A_DE:
                    str_op = "LD A, DE";
                    address += 1;
                    break;

                case Opcode.OPCODE_INC_HL:
                    str_op = "INC HL";
                    address += 1;
                    break;

                case Opcode.OPCODE_RL_A_CF:
                    str_op = "RL A";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_A_E:
                    str_op = "LD A, E";
                    address += 1;
                    break;

                case Opcode.OPCODE_DEC_A:
                    str_op = "DEC A";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_HL_A:
                    str_op = "LD HL, A";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_D_A:
                    str_op = "LD D, A";
                    address += 1;
                    break;

                case Opcode.OPCODE_INC_B:
                    str_op = "INC B";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_E_n:
                    str_op = "LD E, n";
                    address += 2;
                    break;

                case Opcode.OPCODE_DEC_D:
                    str_op = "DEC D";
                    address += 1;
                    break;

                case Opcode.OPCODE_SUB_A_B:
                    str_op = "SUB A, B";
                    address += 1;
                    break;

                case Opcode.OPCODE_LD_D_n:
                    str_op = "LD D, n";
                    address += 2;
                    break;

                case Opcode.OPCODE_JR_n:
                    str_op = "JR n";
                    address += 2;
                    break;

                case Opcode.OPCODE_CP_A_HL:
                    str_op = "CP A, HL";
                    address += 1;
                    break;

                case Opcode.OPCODE_INTERNAL_CB:
                    address += 1;
                    break;

                default:
                    str_op = "UNKNOWN";
                    address++;
                    break;
            }

            return str_op;
        }
    }
}
