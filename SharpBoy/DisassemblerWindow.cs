using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpBoy
{
    public partial class DisassemblerWindow : Form
    {
        public DisassemblerWindow()
        {
            InitializeComponent();

            if(Program.emulator.isRunning)
                btn_start.Text = "Stop";

            //Program.emulator.Stop();
            //Program.emulator.getCPU().onInstructionExecute += new CPU.InstructionExecutedHandler(cpu_regs_update_in_window, EventArgs.Empty);
        }

        public void cpu_regs_update_in_window()
        {
            tb_reg_pc_dis.Text = String.Format("{0:X4}", Program.emulator.getCPU().get_reg_pc());
            tb_reg_sp.Text = String.Format("{0:X4}", Program.emulator.getCPU().get_reg_sp());

            tb_reg_af.Text = String.Format("{0:X4}", Program.emulator.getCPU().get_reg_af());
            tb_reg_bc.Text = String.Format("{0:X4}", Program.emulator.getCPU().get_reg_bc());
            tb_reg_de.Text = String.Format("{0:X4}", Program.emulator.getCPU().get_reg_de());
            tb_reg_hl.Text = String.Format("{0:X4}", Program.emulator.getCPU().get_reg_hl());

            tb_reg_a.Text = String.Format("{0:X2}", Program.emulator.getCPU().get_reg_a());
            tb_reg_b.Text = String.Format("{0:X2}", Program.emulator.getCPU().get_reg_b());
            tb_reg_c.Text = String.Format("{0:X2}", Program.emulator.getCPU().get_reg_c());
            tb_reg_d.Text = String.Format("{0:X2}", Program.emulator.getCPU().get_reg_d());
            tb_reg_e.Text = String.Format("{0:X2}", Program.emulator.getCPU().get_reg_e());
            tb_reg_f.Text = String.Format("{0:X2}", Program.emulator.getCPU().get_reg_f());
            tb_reg_h.Text = String.Format("{0:X2}", Program.emulator.getCPU().get_reg_h());
            tb_reg_l.Text = String.Format("{0:X2}", Program.emulator.getCPU().get_reg_l());

            cb_zero_flag.Checked = Program.emulator.getCPU().GetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO);
            cb_substract_flag.Checked = Program.emulator.getCPU().GetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT);
            cb_half_carry_flag.Checked = Program.emulator.getCPU().GetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY);
            cb_carry_flag.Checked = Program.emulator.getCPU().GetFlagBit(CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY);

            tb_goto_address.Text = tb_reg_pc_dis.Text;
        }

        private void lv_windowLoadData(UInt16 i_address)
        {
            lv_dissassembly.Clear();

            lv_dissassembly.Columns.Add("Address", 70);
            lv_dissassembly.Columns.Add("Opcode", 70);
            lv_dissassembly.Columns.Add("Instruction", 200);

            UInt16 lo_val = (i_address < 0x00FF) ? (UInt16)(0) : (UInt16)(i_address - 0xFF);
            UInt16 hi_val = ((UInt32)(i_address + 0x00FF) > 0xFFFF) ? (UInt16)(0xFFFF) : (UInt16)(i_address + 0xFF);

            for (UInt16 i = lo_val; i < hi_val;)
            {
                Byte opcode = Program.emulator.GetMemory().ReadFromMemory(i);
                String address = String.Format("{0:X4}", i);
                String value = String.Format("{0:X2}", opcode);

                ListViewItem item = new ListViewItem(address);
                item.SubItems.Add(value);

                if((Opcodes.Opcode)opcode == Opcodes.Opcode.OPCODE_INTERNAL_CB)
                {
                    opcode = Program.emulator.GetMemory().ReadFromMemory(++i);
                    item.SubItems.Add(OpcodesCB.GetOpcodeCB((OpcodesCB.OpcodeCB)opcode, ref i));
                }
                else item.SubItems.Add(Opcodes.GetOpcode((Opcodes.Opcode)opcode, ref i));

                lv_dissassembly.Items.Add(item);
            }

            if (lv_dissassembly.Items.Count == 0)
                return;

            int index = GetItemIndexWithAddress(String.Format("{0:X4}", i_address));
            ListViewItem sel_item = lv_dissassembly.Items[index];
            sel_item.Selected = true;
            sel_item.Focused = true;
            lv_dissassembly.Select();
            lv_dissassembly.EnsureVisible(index);
        }

        private void DisassemblerWindow_Load(object sender, EventArgs e)
        {
            cpu_regs_update_in_window();

            lv_windowLoadData(Program.emulator.getCPU().get_reg_pc());
        }

        private bool isAddressExist(String address)
        {
            bool exist = false;
            foreach(ListViewItem item in lv_dissassembly.Items)
            {
                if (item.Text == address)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        private int GetItemIndexWithAddress(String address)
        {
            int index = -1;
            foreach (ListViewItem item in lv_dissassembly.Items)
            {
                if (item.Text == address)
                {
                    index = item.Index;
                    break;
                }
            }
            return index;
        }

        private void pause_button_Click(object sender, EventArgs e)
        {
            Program.emulator.getCPU().ExeCycle();

            cpu_regs_update_in_window();

            String address = String.Format("{0:X4}", Program.emulator.getCPU().get_reg_pc());

            // Check if item exist in current list
            if(!isAddressExist(address))
                lv_windowLoadData(Program.emulator.getCPU().get_reg_pc());

            int itemIndex = GetItemIndexWithAddress(address);

            if (itemIndex == -1)
                return;

            lv_dissassembly.SelectedItems.Clear();

            ListViewItem item = lv_dissassembly.Items[itemIndex];
            item.Selected = true;
            item.Focused = true;
            lv_dissassembly.Select();
            lv_dissassembly.EnsureVisible(itemIndex);
        }

        private void b_goto_address_Click(object sender, EventArgs e)
        {
            UInt16 address = UInt16.Parse(tb_goto_address.Text, NumberStyles.HexNumber);
            lv_windowLoadData(address);
        }

        private void bt_addBreakpoint_Click(object sender, EventArgs e)
        {
            UInt16 address = UInt16.Parse(tb_breakpointAdr.Text, NumberStyles.HexNumber);
            Program.emulator.breakPointsList.Add(address);

            lb_breakpoints.Items.Add(String.Format("{0:X4}", address));
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (Program.emulator.isRunning)
            {
                Program.emulator.Stop();
                btn_start.Text = "Start";

                lv_windowLoadData(Program.emulator.getCPU().get_reg_pc());
            }
            else
            {
                if(!Program.emulator.GetCartridge().IsCartridgeLoaded())
                    Program.emulator.Reset(true);

                Program.emulator.Start();

                btn_start.Text = "Stop";
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            lv_windowLoadData(Program.emulator.getCPU().get_reg_pc());
            cpu_regs_update_in_window();
        }

        private void tb_reg_b_MouseDown(object sender, MouseEventArgs e)
        {
            Program.emulator.getCPU().set_reg_b(0x01);
        }

        private void tb_reg_c_MouseDown(object sender, MouseEventArgs e)
        {
            Program.emulator.getCPU().set_reg_c(0x01);
        }
    }
}
