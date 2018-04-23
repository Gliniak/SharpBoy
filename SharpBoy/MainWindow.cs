using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SharpBoy
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            // Initialize all required data before running openTK
            OpenTK.Toolkit.Init();
            InitializeComponent();

            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "GUI: Initialized");
        }

        // It is better to create another class for this?
        // I need to have access to GL from inside the Emulator not GUI code
        private void OpenGLKeyPressed(object sender, KeyEventArgs args)
        {
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "OPEN GL KEY");
        }

        private void OpenGLResize(object sender, EventArgs args)
        {
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "OPEN GL RESIZE");
            openGLControl.Size = Size;
        }

        private void OpenGLPaint(object sender, PaintEventArgs args)
        {
            //GL.bindte
            //Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "OPEN GL PAINT");
            openGLControl.SwapBuffers();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear Everything Before Exiting!


            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileOk += new CancelEventHandler(openFileDialog_FileOpening);
            dialog.ShowDialog();
        }

        // Created in openToolStripMenuItem_Click
        private void openFileDialog_FileOpening(object sender, EventArgs e)
        {
            OpenFileDialog opener = (OpenFileDialog)sender;

            // TODO: Check if this make sense (Sometimes maybe?)
            if (opener == null)
                return;

            if (!opener.CheckFileExists)
                return;

            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "Reading File: " + opener.FileName);

            Program.emulator.LoadCartridge(opener.FileName);

            Text = Program.emulator.GetCartridge().GetGameTitle();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.emulator.Stop();

            Program.loggerW.Invoke(new Action(delegate ()
            {
                Program.loggerW.Close();
            }));
            
        }

        private void disassemblerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisassemblerWindow disassembler = new DisassemblerWindow();
            disassembler.Show();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.emulatorThread = new System.Threading.Thread(() => Program.emulator.Start());
            Program.emulatorThread.Start();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
        }

        private void memoryViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryViewer memView = new MemoryViewer();
            memView.Show();
        }
    }
}
