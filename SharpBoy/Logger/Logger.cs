using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpBoy
{
    public class Logger
    {
        public enum LOG_LEVEL : Byte
        {
            LOG_LEVEL_ERROR = 0x01,
            LOG_LEVEL_WARNING = 0x02,
            LOG_LEVEL_INFO = 0x04,
            LOG_LEVEL_DEBUG = 0x08
        };

        private static readonly FileStream logfile = File.Create("SharpBoy.txt");
        private static LOG_LEVEL log_level = LOG_LEVEL.LOG_LEVEL_DEBUG | LOG_LEVEL.LOG_LEVEL_INFO | LOG_LEVEL.LOG_LEVEL_WARNING | LOG_LEVEL.LOG_LEVEL_ERROR;

        private Logger() { }

        public static void setLoglevel(LOG_LEVEL level) { log_level |= level; }
        public static void removeLoglevel(LOG_LEVEL level) { log_level &= ~level; }
        public static LOG_LEVEL getLoglevel() { return log_level; }

        public static void AppendLog(LOG_LEVEL level, String message)
        {
            if (logfile == null)
                return;

            // Need to create some async queue
            if (!logfile.CanWrite)
                return;

            if (!Convert.ToBoolean(log_level & level))
                return;

            // Add new line marker to end of each message 
            message += Environment.NewLine;

            char[] mess = message.ToCharArray();
            logfile.Write(Encoding.Default.GetBytes(mess), 0, Encoding.Default.GetByteCount(mess));

            if (Program.loggerW == null)
                return;

            Program.loggerW.Invoke(new Action(delegate ()
            {
                switch (level)
                {
                    case LOG_LEVEL.LOG_LEVEL_ERROR:
                        LoggerWindow.SetTextboxColor(System.Drawing.Color.Red);
                        break;

                    case LOG_LEVEL.LOG_LEVEL_INFO:
                        LoggerWindow.SetTextboxColor(System.Drawing.Color.GreenYellow);
                        break;

                    case LOG_LEVEL.LOG_LEVEL_WARNING:
                        LoggerWindow.SetTextboxColor(System.Drawing.Color.Yellow);
                        break;

                    default:
                        LoggerWindow.SetTextboxColor(System.Drawing.Color.White);
                        break;
                }
                LoggerWindow.PutInLog(message);
            }));
        }
    }
}
