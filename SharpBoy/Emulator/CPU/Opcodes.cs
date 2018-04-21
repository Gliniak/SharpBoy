using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Emulator
{
    class Opcodes
    {
        public enum Opcode : Byte
        {
            OPCODE_NOP = 0x00,
            OPCODE_DEC_B = 0x05, // DEC B
            OPCODE_DEC_C = 0x0D, // DEC C

            OPCODE_LD_A_n = 0x3E, // LOAD A, n

            OPCODE_LD_B = 0x06, // LOAD B, n
            OPCODE_LD_C = 0x0E, // LOAD C, n

            OPCODE_JR_NZ = 0x20, // JR_FLAG_ZERO_RESET
            OPCODE_LD_HL = 0x21, // LOAD HL, nn

            OPCODE_LDD_HL = 0x32, // LOAD DEC HL, A - Set A in HL and decrease value by 1
            OPCODE_LD_HL_n = 0x36, // LOAD HL, n

            OPCODE_JMP = 0xC3, // IMPLEMENTED
            OPCODE_XOR = 0xAF, // XOR reg_a

            OPCODE_DI = 0xF3, // DI - Disable Interrupts

            OPCODE_RRCA = 0x0F, // Rotate A Right

            OPCODE_LDH_N_A = 0xE0, // LOAD A to $FF00+n
            OPCODE_LHD_A_N = 0xF0, // LOAD $FF00+n to A

            OPCODE_CP_n = 0xFE
        }

        static Dictionary<Opcode, Action> opcodes = new Dictionary<Opcode, Action>()
        {
            { Opcode.OPCODE_NOP, () => nop_instruction() },
            { Opcode.OPCODE_JMP, () => jmp_instruction() },
            { Opcode.OPCODE_XOR, () => xor_a_ins() },
            { Opcode.OPCODE_LD_HL, () => ld_hl_ins() },
            { Opcode.OPCODE_LD_C, () => ld_c_ins() },
            { Opcode.OPCODE_LD_B, () => ld_b_ins() },
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
            { Opcode.OPCODE_LD_HL_n, () => ld_hl_n() }
        };

        public static void unimplemented_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 1));
            return;
        }

        public static void ld_hl_n()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();
            Byte value = Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));

            // TODO: Fix here!
            Program.emulator.getCPU().set_reg_f(value);
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 2));
        }

        public static void cp_n_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();
            

            Byte value = Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));

            Program.emulator.getCPU().SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            if (Program.emulator.getCPU().get_reg_a() == value)
                Program.emulator.getCPU().SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            if (Program.emulator.getCPU().get_reg_a() < value)
                Program.emulator.getCPU().SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, true);

            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 2));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "CP A, n INSTRUCTION EXECUTED");
        }

        public static void rrca_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();
            Program.emulator.getCPU().SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, (Program.emulator.getCPU().get_reg_a() & 1) == 1);
            Program.emulator.getCPU().SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            Program.emulator.getCPU().SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            // Need to do Zero Flag!

            Program.emulator.getCPU().set_reg_a((Byte)(Program.emulator.getCPU().get_reg_a() >> 1));
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 1));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "RRCA INSTRUCTION EXECUTED");
        }


        public static void ldh_a_n_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();
            Byte value = Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));

            Program.emulator.getCPU().set_reg_a(Program.emulator.GetMemory().ReadFromMemory((UInt16)(0xFF00 + value)));
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 2));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LDH A, [$FF00+n] INSTRUCTION EXECUTED");
        }

        public static void ldh_n_a_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();
            Byte value = Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));

            Program.emulator.GetMemory().WriteToMemory((UInt16)(0xFF00 + value), Program.emulator.getCPU().get_reg_a());
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 2));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LDH [$FF00+n],A INSTRUCTION EXECUTED");
        }

        public static void di_ins()
        {
            // NOT IMPLEMENTED!
            UInt16 address = Program.emulator.getCPU().get_reg_pc();
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 1));
            return;
        }

        public static void ld_a_n_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();

            Program.emulator.getCPU().set_reg_a(Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1)));
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 2));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD A,n INSTRUCTION EXECUTED");
        }

        public static void jr_nz_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();

            if (!Program.emulator.getCPU().GetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO))
            {
                Byte add_pc = Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));

                // Why I need to Add 2?
                Program.emulator.getCPU().set_reg_pc((UInt16)(address + (SByte)add_pc + 2));
                return;
            }

            Program.emulator.getCPU().set_reg_pc((UInt16)(address + 2));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "JR_NZ INSTRUCTION EXECUTED");
        }

        public static void nop_instruction()
        {
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(Program.emulator.getCPU().get_reg_pc() + 1));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "NOP INSTRUCTION EXECUTED");
        }

        public static void jmp_instruction()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));

            UInt16 newAddress = Convert.ToUInt16((hi << 8) + lo);

            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "JMP INSTRUCTION EXECUTED AT ADDRESS: " + address.ToString() + " TO ADDRESS: " + newAddress.ToString());
            Program.emulator.getCPU().set_reg_pc(newAddress);
        }

        public static void xor_a_ins()
        {
            
            Program.emulator.getCPU().SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY, false);
            Program.emulator.getCPU().SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY, false);
            Program.emulator.getCPU().SetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, false);

            Byte reg_a = Program.emulator.getCPU().get_reg_a();
            Program.emulator.getCPU().set_reg_a(Convert.ToByte(reg_a ^ reg_a));

            if (Program.emulator.getCPU().get_reg_a() == 0)
                Program.emulator.getCPU().SetFlagBit(Emulator.CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, true);

            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(Program.emulator.getCPU().get_reg_pc() + 1));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "XOR_A INSTRUCTION EXECUTED");
        }

        public static void ld_hl_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();

            Byte hi = Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 2));
            Byte lo = Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));

            UInt16 value = Convert.ToUInt16((hi << 8) + lo);

            Program.emulator.getCPU().set_reg_h(hi);
            Program.emulator.getCPU().set_reg_l(lo);

            Program.emulator.getCPU().set_reg_hl(value);
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 3));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD HL INSTRUCTION EXECUTED");
        }


        public static void ld_c_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();

            Program.emulator.getCPU().set_reg_c(Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1)));
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 2));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD C INSTRUCTION EXECUTED");
        }

        public static void ld_b_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();

            Program.emulator.getCPU().set_reg_b(Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1)));
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 2));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD B INSTRUCTION EXECUTED");
        }

        public static void ldd_hl_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();

            Program.emulator.getCPU().set_reg_l((Byte)(Program.emulator.getCPU().get_reg_a() - 1));

            Program.emulator.getCPU().set_reg_hl(Convert.ToUInt16(Program.emulator.getCPU().get_reg_a()));
            Program.emulator.getCPU().set_reg_hl((UInt16)Convert.ToInt16(Program.emulator.getCPU().get_reg_hl() - 1));

            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 1));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "LOAD HL, A DEC A INSTRUCTION EXECUTED");
        }

        public static void dec_b_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();

            Byte value = (Byte)(Program.emulator.getCPU().get_reg_b() - 1);

            Program.emulator.getCPU().SetFlagBit(Emulator.CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);
            Program.emulator.getCPU().SetFlagBit(Emulator.CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            Program.emulator.getCPU().set_reg_b(value);
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 1));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DEC B INSTRUCTION EXECUTED");
        }

        public static void dec_c_ins()
        {
            UInt16 address = Program.emulator.getCPU().get_reg_pc();

            Byte value = (Byte)(Program.emulator.getCPU().get_reg_c() - 1);

            // TODO: NEED TO ADD CARRY FLAG
            Program.emulator.getCPU().SetFlagBit(Emulator.CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO, value == 0 ? true : false);
            Program.emulator.getCPU().SetFlagBit(Emulator.CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT, true);

            Program.emulator.getCPU().set_reg_c(value);
            Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 1));
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "DEC C INSTRUCTION EXECUTED");
        }


        public static void ExecuteOpcode(Opcode opcode)
        {
            if (opcodes[opcode] != null)
                opcodes[opcode].Invoke();
            else
            {
                UInt16 address = Program.emulator.getCPU().get_reg_pc();
                Program.emulator.getCPU().set_reg_pc(Convert.ToUInt16(address + 1));
                Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "Trying to execute not implemented opcode: " + opcode.ToString());
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

                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 2));
                    val2 = (SByte)Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));

                    str_op += String.Format("{0:X2}", val1) + String.Format("{0:X2}", val2) + "h";
                    address += 3;
                    break;

                case Opcode.OPCODE_XOR:
                    str_op = "XOR A";
                    address++;
                    break;

                case Opcode.OPCODE_LD_HL:
                    str_op = "LD HL, ";

                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 2));
                    val2 = (SByte)Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));

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
                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));
                    str_op += String.Format("{0:X4}", Convert.ToUInt16(address + val1 + 2)) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_LDD_HL:
                    str_op = "LDD HL, A";
                    address++;
                    break;

                case Opcode.OPCODE_LD_A_n:
                    str_op = "LD A, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1))) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_LD_B:
                    str_op = "LD B, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1))) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_LD_C:
                    str_op = "LD C, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1))) + "h";
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
                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));
                    str_op += String.Format("{0:X4}", 0xFF00 + (Byte)val1) + "], A";
                    address += 2;
                    break;

                case Opcode.OPCODE_LHD_A_N:
                    str_op = "LDH A, [$";
                    val1 = (SByte)Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1));
                    str_op += String.Format("{0:X4}", 0xFF00 + (Byte)val1) + "]";
                    address += 2;
                    break;

                case Opcode.OPCODE_CP_n:
                    str_op = "CP A, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1))) + "h";
                    address += 2;
                    break;

                case Opcode.OPCODE_LD_HL_n:
                    str_op = "LD HL, ";
                    str_op += String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory(Convert.ToUInt16(address + 1))) + "h";
                    address += 2;
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
