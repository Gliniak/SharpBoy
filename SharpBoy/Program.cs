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
        public static Emulator.emulator emulator = new Emulator.emulator();
        public static LoggerWindow loggerW;
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread thread = new Thread(() => Application.Run(loggerW = new LoggerWindow()));
            thread.Start();

            emulator.Initialize();

            Application.Run(new MainWindow());
        }
    }
}
