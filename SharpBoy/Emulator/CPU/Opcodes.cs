using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class Opcodes
    {
        // MAY CAUSE ERRORS?
        private static CPU cpu = Program.emulator.getCPU();

        public enum Opcode : Byte
        {
            OPCODE_NOP = 0x00,

            OPCODE_INC_C = 0x0C, // INC C
            OPCODE_INC_DE = 0x13,
            OPCODE_DEC_B = 0x05, // DEC B
            OPCODE_DEC_C = 0x0D, // DEC C
            OPCODE_DEC_BC = 0x0B, // DEC BC
            OPCODE_LD_A_n = 0x3E, // LOAD A, n
            OPCODE_LD_B_n = 0x06, // LOAD B, n
            OPCODE_LD_C_n = 0x0E, // LOAD C, n
            OPCODE_LD_A_B = 0x78, // LOAD A, B
            OPCODE_LD_ADR_C_A = 0xE2, // LOAD $0xFF00+C, A
            OPCODE_LD_A_HL = 0x2A, // LOAD A, HL+ (inc)
            OPCODE_LD_DE_A = 0x12, // LOAD DE, A
            OPCODE_LD_HL_n = 0x36, // LOAD HL, n
            OPCODE_LD_BC_nn = 0x01, // LOAD BC, nn
            OPCODE_LD_DE_nn = 0x11,
            OPCODE_LD_HL_nn = 0x21, // LOAD HL, nn
            OPCODE_LD_SP_nn = 0x31, // LOAD SP, nn (SP <- nn)
            OPCODE_LD_nn_A = 0xEA, // LOAD [$xxxx], A
            OPCODE_LDD_HL = 0x32, // LOAD DEC HL, A - Set A in HL and decrease value by 1
            OPCODE_LDH_N_A = 0xE0, // LOAD A to $FF00+n
            OPCODE_LHD_A_N = 0xF0, // LOAD $FF00+n to A


            // OTHER OPCODES
            OPCODE_OR_A_C = 0xB1, // OR C
            OPCODE_XOR = 0xAF, // XOR reg_a
            OPCODE_JR_NZ = 0x20, // JR_FLAG_ZERO_RESET
            OPCODE_JMP = 0xC3,
            OPCODE_DI = 0xF3, // DI - Disable Interrupts
            OPCODE_RRCA = 0x0F, // Rotate A Right
            OPCODE_CP_n = 0xFE,
            OPCODE_CALL_ADDRESS = 0xCD,
            OPCODE_RET = 0xC9, // RET
            
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
            { Opcode.OPCODE_LD_A_HL, () => ld_a_nn_ins() },
            { Opcode.OPCODE_LD_ADR_C_A, () => ld_c_a_ins() },
            { Opcode.OPCODE_INC_C, () => inc_c_ins() },
            { Opcode.OPCODE_CALL_ADDRESS, () => call_adr_ins() },
            { Opcode.OPCODE_LD_BC_nn, () => ld_bc_nn_ins() },
            { Opcode.OPCODE_DEC_BC, () => dec_bc_ins() },
            { Opcode.OPCODE_LD_A_B, () => ld_a_b_ins() },
            { Opcode.OPCODE_OR_A_C, () => or_a_c_ins() },
            { Opcode.OPCODE_RET, () => ret_ins() },
            { Opcode.OPCODE_LD_DE_A, () => ld_de_a_ins() },
            { Opcode.OPCODE_INC_DE, () => inc_de_ins() },
            { Opcode.OPCODE_LD_DE_nn, () => ld_de_nn_ins() }
        };

        public static void unimplemented_ins()
        {
            UInt16 address = cpu.get_reg_pc();
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
            // Is it correct?
            cpu.set_reg_pc(cpu.get_reg_sp());
            cpu.set_reg_pc((UInt16)(address + 1));
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
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DEC BC INSTRUCTION EXECUTED");
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
            cpu.set_reg_sp((UInt16)(address + 3));

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 value = Convert.ToUInt16((hi << 8) + lo);
            cpu.set_reg_pc(value);
        }

        public static void inc_c_ins()
        {
            // TODO: FLAGS
            UInt16 address = cpu.get_reg_pc();
            cpu.set_reg_c((Byte)(cpu.get_reg_c() + 1));
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_c_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Program.emulator.GetMemory().WriteToMemory((UInt16)(0xFF00 + cpu.get_reg_c()), cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 1));
        }

        public static void ld_a_nn_ins()
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

            cpu.set_reg_pc((UInt16)(address + 2));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "CP A, n INSTRUCTION EXECUTED");
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
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "RRCA INSTRUCTION EXECUTED");
        }


        public static void ldh_a_n_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Byte value = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            cpu.set_reg_a(Program.emulator.GetMemory().ReadFromMemory((UInt16)(0xFF00 + value)));
            cpu.set_reg_pc((UInt16)(address + 2));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LDH A, [$FF00+n] INSTRUCTION EXECUTED");
        }

        public static void ldh_n_a_ins()
        {
            UInt16 address = cpu.get_reg_pc();
            Byte value = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            Program.emulator.GetMemory().WriteToMemory((UInt16)(0xFF00 + value), cpu.get_reg_a());
            cpu.set_reg_pc((UInt16)(address + 2));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LDH [$FF00+n],A INSTRUCTION EXECUTED");
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
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD A,n INSTRUCTION EXECUTED");
        }

        public static void jr_nz_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            if (!cpu.GetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO))
            {
                Byte add_pc = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                // Why I need to Add 2?
                cpu.set_reg_pc((UInt16)(address + (SByte)add_pc + 2));
                return;
            }

            cpu.set_reg_pc((UInt16)(address + 2));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "JR_NZ INSTRUCTION EXECUTED");
        }

        public static void nop_instruction()
        {
            cpu.set_reg_pc(Convert.ToUInt16(cpu.get_reg_pc() + 1));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "NOP INSTRUCTION EXECUTED");
        }

        public static void jmp_instruction()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 newAddress = Convert.ToUInt16((hi << 8) + lo);

            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "JMP INSTRUCTION EXECUTED AT ADDRESS: " + address.ToString() + " TO ADDRESS: " + newAddress.ToString());
            cpu.set_reg_pc(newAddress);
        }

        public static void xor_a_ins()
        {
            
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            Byte reg_a = cpu.get_reg_a();
            cpu.set_reg_a(Convert.ToByte(reg_a ^ reg_a));

            if (cpu.get_reg_a() == 0)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            cpu.set_reg_pc(Convert.ToUInt16(cpu.get_reg_pc() + 1));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "XOR_A INSTRUCTION EXECUTED");
        }

        public static void ld_hl_nn_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

            UInt16 value = Convert.ToUInt16((hi << 8) + lo);

            cpu.set_reg_hl(value);
            cpu.set_reg_pc((UInt16)(address + 3));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD HL INSTRUCTION EXECUTED");
        }


        public static void ld_c_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_c(Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1)));
            cpu.set_reg_pc((UInt16)(address + 2));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD C INSTRUCTION EXECUTED");
        }

        public static void ld_b_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_b(Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1)));
            cpu.set_reg_pc((UInt16)(address + 2));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD B INSTRUCTION EXECUTED");
        }

        public static void ldd_hl_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            cpu.set_reg_l((Byte)(cpu.get_reg_a() - 1));

            cpu.set_reg_hl(Convert.ToUInt16(cpu.get_reg_a()));
            cpu.set_reg_hl((UInt16)Convert.ToInt16(cpu.get_reg_hl() - 1));

            cpu.set_reg_pc((UInt16)(address + 1));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD HL, A DEC A INSTRUCTION EXECUTED");
        }

        public static void dec_b_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_b() - 1);

            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            cpu.set_reg_b(value);
            cpu.set_reg_pc((UInt16)(address + 1));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DEC B INSTRUCTION EXECUTED");
        }

        public static void dec_c_ins()
        {
            UInt16 address = cpu.get_reg_pc();

            Byte value = (Byte)(cpu.get_reg_c() - 1);

            // TODO: NEED TO ADD HALF-CARRY FLAG
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);
            cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            cpu.set_reg_c(value);

            if((value & 0x0F) == 0x0F)
                cpu.SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, true);

            cpu.set_reg_pc((UInt16)(address + 1));
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DEC C INSTRUCTION EXECUTED");
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

            SByte val1 = 0;
            SByte val2 = 0;
            UInt16 val3 = 0;

            switch(opcode)
            {
                case Opcode.OPCODE_NOP:
                    str_op = "NOP";
                    address++;
                    break;

                case Opcode.OPCODE_JMP:
                    str_op = "JMP ";

                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    val2 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", val1) + String.Format("{0:X2}", val2) + "h";
                    address += 3;
                    break;

                case Opcode.OPCODE_XOR:
                    str_op = "XOR A";
                    address++;
                    break;

                case Opcode.OPCODE_LD_HL_nn:
                    str_op = "LD HL, ";

                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    val2 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", val1) + String.Format("{0:X2}", val2) + "h";
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
                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));
                    str_op += String.Format("{0:X4}", Convert.ToUInt16(address + val1 + 2)) + "h";
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
                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));
                    str_op += String.Format("{0:X4}", 0xFF00 + (Byte)val1) + "], A";
                    address += 2;
                    break;

                case Opcode.OPCODE_LHD_A_N:
                    str_op = "LDH A, [$";
                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));
                    str_op += String.Format("{0:X4}", 0xFF00 + (Byte)val1) + "]";
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

                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    val2 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", val1) + String.Format("{0:X2}", val2) + "], A";
                    address += 3;
                    break;

                case Opcode.OPCODE_LD_SP_nn:
                    str_op = "LD SP, ";

                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    val2 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", val1) + String.Format("{0:X2}", val2);
                    address += 3;
                    break;

                case Opcode.OPCODE_LD_A_HL:
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

                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    val2 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", val1) + String.Format("{0:X2}", val2);
                    address += 3;
                    break;

                case Opcode.OPCODE_LD_BC_nn:
                    str_op = "LD BC, ";

                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    val2 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", val1) + String.Format("{0:X2}", val2);
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

                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 2));
                    val2 = (SByte)Program.emulator.GetMemory().ReadFromMemory((UInt16)(address + 1));

                    str_op += String.Format("{0:X2}", val1) + String.Format("{0:X2}", val2);
                    address += 3;
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
