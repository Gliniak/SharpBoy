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
    public partial class LoggerWindow : Form
    {
        public LoggerWindow()
        {
            InitializeComponent();
            logger_textbox.BackColor = Color.Black;
            logger_textbox.ForeColor = Color.White;
        }

        public static void PutInLog(string data)
        {
            Program.loggerW.logger_textbox.AppendText(data);
        }

        public static void SetTextboxColor(Color color)
        {
            Program.loggerW.logger_textbox.SelectionColor = color;
        }
    }
}
