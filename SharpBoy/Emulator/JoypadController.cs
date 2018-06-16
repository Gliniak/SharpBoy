using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy
{
    class JoypadController
    {
        // FF00 - ADR

        /*  Bit 7 - Not used
            Bit 6 - Not used
            Bit 5 - P15 Select Button Keys      (0=Select)
            Bit 4 - P14 Select Direction Keys   (0=Select)
            Bit 3 - P13 Input Down  or Start    (0=Pressed) (Read Only)
            Bit 2 - P12 Input Up    or Select   (0=Pressed) (Read Only)
            Bit 1 - P11 Input Left  or Button B (0=Pressed) (Read Only)
            Bit 0 - P10 Input Right or Button A (0=Pressed) (Read Only)
        */

        public void Update(Byte button = 0)
        {
            Byte value = 0xEF;//0x3F;

            switch(button)
            {
                default: break;
            }

            Program.GetEmulator().GetMemory().WriteToMemory(0xFF00, value);
        }
    }
}
