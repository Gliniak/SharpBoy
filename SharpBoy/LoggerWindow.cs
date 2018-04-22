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

            Logger.Logger.LOG_LEVEL log = Logger.Logger.getLoglevel();

            if (Convert.ToBoolean(log & Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG))
                cb_log_debug.Checked = true;

            if (Convert.ToBoolean(log & Logger.Logger.LOG_LEVEL.LOG_LEVEL_ERROR))
                cb_log_error.Checked = true;

            if (Convert.ToBoolean(log & Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO))
                cb_log_info.Checked = true;

            if (Convert.ToBoolean(log & Logger.Logger.LOG_LEVEL.LOG_LEVEL_WARNING))
                cb_log_warn.Checked = true;

        }

        public static void PutInLog(string data)
        {
            Program.loggerW.logger_textbox.AppendText(data);
        }

        public static void SetTextboxColor(Color color)
        {
            Program.loggerW.logger_textbox.SelectionColor = color;
        }

        private void cb_log_debug_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_log_debug.Checked)
                Logger.Logger.setLoglevel(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG);
            else Logger.Logger.removeLoglevel(Logger.Logger.LOG_LEVEL.LOG_LEVEL_DEBUG);
        }

        private void cb_log_error_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_log_error.Checked)
                Logger.Logger.setLoglevel(Logger.Logger.LOG_LEVEL.LOG_LEVEL_ERROR);
            else Logger.Logger.removeLoglevel(Logger.Logger.LOG_LEVEL.LOG_LEVEL_ERROR);
        }

        private void cb_log_warn_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_log_warn.Checked)
                Logger.Logger.setLoglevel(Logger.Logger.LOG_LEVEL.LOG_LEVEL_WARNING);
            else Logger.Logger.removeLoglevel(Logger.Logger.LOG_LEVEL.LOG_LEVEL_WARNING);
        }

        private void cb_log_info_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_log_info.Checked)
                Logger.Logger.setLoglevel(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO);
            else Logger.Logger.removeLoglevel(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO);
        }
    }
}
