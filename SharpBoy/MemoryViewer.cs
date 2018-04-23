using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpBoy
{
    public partial class MemoryViewer : Form
    {
        private UInt16 Address = 0x0000;

        public MemoryViewer()
        {
            InitializeComponent();
        }

        private void MemoryViewer_Load(object sender, EventArgs e)
        {
            Update_MemView();
        }

        private void Update_MemView()
        {
            lv_memView.Items.Clear();

            for (Byte i = 0; i <= 16 * 4 - 1; i++)
            {
                if ((UInt32)(Address + i * 16) >= 0x10000)
                    return;

                lv_memView.Items.Add((String.Format("{0:X4}", Address + i * 16)));
                for (Byte j = 0; j <= 15; j++)
                    lv_memView.Items[i].SubItems.Add(String.Format("{0:X2}", Program.emulator.GetMemory().ReadFromMemory((UInt16)(Address + i * 16 + j))));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cb_specAddress
            switch(cb_specAddress.SelectedIndex)
            {
                case 0:
                    Address = 0x0000;
                    break;

                case 1:
                    Address = 0x8000;
                    break;

                case 2:
                    Address = 0xA000;
                    break;
                case 3:
                    Address = 0xC000;
                    break;
                case 4:
                    Address = 0xE000;
                    break;
                case 5:
                    Address = 0xFE00;
                    break;
                case 6:
                    Address = 0xFF00;
                    break;
                case 7:
                    Address = 0xFFE0;
                    break;
                default:
                    Address = 0x0000;
                    break;
            }
            Update_MemView();
        }
    }
}
