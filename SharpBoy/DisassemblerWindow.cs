using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpBoy
{
    public partial class DisassemblerWindow : Form
    {
        public DisassemblerWindow()
        {
            InitializeComponent();
        }

        private void cpu_regs_update_in_window()
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

            cb_zero_flag.Checked = Program.emulator.getCPU().GetFlagBit(Emulator.CPU.Flag_Register_Bits.FLAG_REGISTER_ZERO);
            cb_substract_flag.Checked = Program.emulator.getCPU().GetFlagBit(Emulator.CPU.Flag_Register_Bits.FLAG_REGISTER_SUBSTRACT);
            cb_half_carry_flag.Checked = Program.emulator.getCPU().GetFlagBit(Emulator.CPU.Flag_Register_Bits.FLAG_REGISTER_H_CARRY);
            cb_carry_flag.Checked = Program.emulator.getCPU().GetFlagBit(Emulator.CPU.Flag_Register_Bits.FLAG_REGISTER_CARRY);

            tb_goto_address.Text = tb_reg_pc_dis.Text;
        }

        private void lv_windowLoadData(UInt16 i_address)
        {
            lv_dissassembly.Clear();

            lv_dissassembly.Columns.Add("Address", 70);
            lv_dissassembly.Columns.Add("Opcode", 70);
            lv_dissassembly.Columns.Add("Instruction", 200);

            for (UInt16 i = Convert.ToUInt16(i_address - 0x0020); i < Convert.ToUInt16(i_address + 0x0020);)
            {
                Byte opcode = Program.emulator.GetMemory().ReadFromMemory(i);
                String address = String.Format("{0:X4}", i);
                String value = String.Format("{0:X2}", opcode);

                ListViewItem item = new ListViewItem(address);
                item.SubItems.Add(value);
                item.SubItems.Add(Emulator.Opcodes.GetOpcode((Emulator.Opcodes.Opcode)opcode, ref i));
                lv_dissassembly.Items.Add(item);
            }

            ListViewItem sel_item = lv_dissassembly.Items[32];
            sel_item.Selected = true;
            sel_item.Focused = true;
            lv_dissassembly.Select();
            lv_dissassembly.EnsureVisible(32);
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
            Program.emulator.getCPU().exe_ins();

            cpu_regs_update_in_window();

            String address = String.Format("{0:X4}", Program.emulator.getCPU().get_reg_pc());

            // Check if item exist in current list
            if(!isAddressExist(address))
                lv_windowLoadData(Program.emulator.getCPU().get_reg_pc());

            int itemIndex = GetItemIndexWithAddress(address);

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
    }
}
