using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class Emulator
    {
        private CPU cpu = new CPU();
        private Memory memory = new Memory();

        private Cartridge cart = new Cartridge();

        public Emulator()
        {
            Initialize();
        } 
            
        public bool Initialize()
        {
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "Initializing...");
            memory.InitializeMemory();

            return true;
        }

        public void Start()
        {
            Logger.Logger.AppendLog(Logger.Logger.LOG_LEVEL.LOG_LEVEL_INFO, "EMULATOR STARTING");
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

        public Cartridge GetCartridge() { return cart; }
    }
}
