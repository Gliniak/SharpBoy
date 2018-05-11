using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class InterruptController
    {
        public enum Interrupt
        {
            INTERRUPT_NONE = 0x00,
            INTERRUPT_VBLANK = 0x01,
            INTERRUPT_LCDC = 0x02,
            INTERRUPT_TIMER =0x04,
            INTERRUPT_SERIAL = 0x08,
            INTERRUPT_PAD = 0x10
        }

        private List<Interrupt> interruptQueue;

        public Interrupt Update()
        {
            Logger.AppendLog(Logger.LOG_LEVEL.LOG_LEVEL_DEBUG, "InterruptController: Update()");
            return Interrupt.INTERRUPT_NONE;
        }
    }
}
