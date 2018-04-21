using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpBoy
{
    static class Program
    {
        public static Emulator.emulator emulator;
        public static LoggerWindow loggerW;
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread loggerThread = new Thread(() => Application.Run(loggerW = new LoggerWindow()));
            loggerThread.Start();

            Thread emulatorThread = new Thread(() => emulator = new Emulator.emulator());
            emulatorThread.Start();

            Application.Run(new MainWindow());
        }
    }
}
