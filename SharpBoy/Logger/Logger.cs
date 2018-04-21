using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpBoy.Logger
{
    public class Logger
    {
        public enum LOG_LEVEL
        {
            LOG_LEVEL_ERROR,
            LOG_LEVEL_WARNING,
            LOG_LEVEL_INFO,
            LOG_LEVEL_DEBUG
        };

        private static readonly FileStream logfile = File.Create("SharpBoy.txt");

        private Logger() { }

        public static void AppendLog(LOG_LEVEL level, String message)
        {
            if (logfile == null)
                return;

            // Need to create some async queue
            if (!logfile.CanWrite)
                return;

            // Add new line marker to end of each message 
            message += Environment.NewLine;

            char[] mess = message.ToCharArray();
            logfile.Write(UnicodeEncoding.Default.GetBytes(mess), 0, UnicodeEncoding.Default.GetByteCount(mess));

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
