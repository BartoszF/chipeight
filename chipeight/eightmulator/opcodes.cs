using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eightmulator
{
    public class Opcodes
    {
        public static Dictionary<ushort, Func<ushort, bool>> opcodes;// = new Dictionary<ushort, Func<ushort, bool>>();
        public static Emulator emu;

        public Opcodes(Emulator e)
        {
            emu = e;

            opcodes = new Dictionary<ushort, Func<ushort, bool>>()
            {
                {0x200,call},
                {0xF033, decVX}
            };
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

        public bool call(ushort op) //0x2NNN
        {
            emu.stack[emu.sp] = emu.PC;
            ++emu.sp;
            emu.PC = (ushort)(op & 0x0FFF);

            return true;
        }

        public bool decVX(ushort op) //0xFX33
        {
            emu.memory[emu.I] = (byte)(emu.V[op & 0x0F00 >> 8] / 100);
            emu.memory[emu.I + 1] = (byte)((emu.V[(op & 0x0F00) >> 8] / 10) % 10);
            emu.memory[emu.I + 2] = (byte)((emu.V[(op & 0x0F00) >> 8] % 100) % 10);
            emu.PC += 2;

            return true;
        }
    }
}
