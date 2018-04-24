using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class Emulator
    {
        public Boolean isRunning = false;

        private CPU cpu = new CPU();
        private Memory memory = new Memory();
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
            return;
        }

        public void Stop()
        {
            return;
        }

        public void LoadCartridge(String path)
        {
            cart.LoadCartridge(path);
        }

        public Memory GetMemory() { return memory; }
        public CPU getCPU() { return cpu; }
        public Renderer getRenderer() { return renderer; }

        public Cartridge GetCartridge() { return cart; }
    }
}
