using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class Emulator
    {
        public Boolean isRunning = false;
        public List<UInt16> breakPointsList = new List<UInt16>();

        private Memory memory = new Memory();

        private CPU cpu = new CPU();
        private Renderer renderer = new Renderer();

        private Cartridge cart = new Cartridge();

        public Emulator()
        {
            Initialize();
        } 
            
        public bool Initialize()
        {
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_INFO, "Initializing...");
            memory.InitializeMemory();

            return true;
        }

        public void Start()
        {
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_INFO, "EMULATOR STARTING");
            isRunning = true;

            cpu.Start(); 
        }

        public void Reset(bool BiosLoading = false)
        {
            if (isRunning)
                Stop();

            memory.InitializeMemory();
            cpu.Reset(BiosLoading);

            if(BiosLoading)
                LoadBios(Directory.GetCurrentDirectory() + "/bios.gb");
        }

        public void Stop()
        {
            //Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_INFO, "EMULATOR STOPPED");
            isRunning = false;
        }

        public void LoadCartridge(String path)
        {
            cart.LoadCartridge(path);
        }

        public void LoadBios(String filepath)
        {
            FileStream file = File.OpenRead(filepath);

            if (file == null)
                return;

            byte[] data = new byte[0xFF];
            file.Seek(0x0000, SeekOrigin.Begin);

            // Loading data
            file.Read(data, 0, 0xFF);
            memory.WriteToMemory(0x0000, data, (UInt16)data.Length);
        }

        public Memory GetMemory() { return memory; }
        public CPU getCPU() { return cpu; }
        public Renderer getRenderer() { return renderer; }

        public Cartridge GetCartridge() { return cart; }
    }
}
