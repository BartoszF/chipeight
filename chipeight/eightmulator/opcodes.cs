using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eightmulator
{
    public static class Opcodes
    {
        public static Dictionary<ushort, Func<ushort, bool>> opcodes = new Dictionary<ushort, Func<ushort, bool>>();
        public static Emulator emu;

        public static void Init(Emulator e)
        {
            emu = e;
        }

        public bool DoOpcode(ushort op)
        {
            if (opcodes.ContainsKey((ushort)(op & (ushort)0xF000)))
            {
                return opcodes[(ushort)(op & (ushort)0xF000)](op);
            }
            else if(opcodes.ContainsKey((ushort)(op & (ushort)0x000F)))
            {
                return opcodes[(ushort)(op & (ushort)0x000F)](op);
            }
            else
            {
                Console.WriteLine("ERROR : UNKOWN OPCODE " + op);
                return false;
            }
        }

        public bool call(ushort op)
        {
            emu.stack[emu.sp] = emu.PC;
            ++emu.sp;
            emu.PC = (ushort)(op & 0x0FFF);

            return true;
        }
    }
}
