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
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace SharpBoy
{
    public partial class MainWindow : Form
    {
        OpenGLWindow openGLWindow;

        public MainWindow()
        {
            InitializeComponent();
            
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "GUI: Initialized");
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

            openGLWindow = new OpenGLWindow();

            openGLWindow.Location = new Point(5, 5);
            openGLWindow.Size = new Size(640, 480);
            openGLWindow.BackColor = Color.Black;
            openGLWindow.TabIndex = 0;
            // PROBLEM HERE?
            Controls.Add(openGLWindow);
            openGLWindow.Show();
            ((GLControl)openGLWindow.Controls[0]).Invalidate();
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
            if (openGLWindow == null)
                return;

            openGLWindow.PerformAutoScale();
            ((GLControl)openGLWindow.Controls[0]).Invalidate();
            ((GLControl)openGLWindow.Controls[0]).Update();
        }

        private void memoryViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryViewer memView = new MemoryViewer();
            memView.Show();
        }
    }
}
