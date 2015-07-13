using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.IO;

namespace eightmulator
{
    public class Emulator
    {
        public ushort opcode;
        public byte[] memory = new byte[4096];
        public byte[] V = new byte[16];
        public ushort I;
        public ushort PC;
        public byte[] gfx = new byte[64 * 32];
        public byte delay_timer;
        public byte sound_timer;
        public ushort[] stack = new ushort[16];
        public ushort sp;
        public byte[] keys = new byte[16];

        public void Init()
        {
            PC = 0x200;         // Program counter starts at 0x200
            opcode = 0;         // Reset current opcode	
            I = 0;              // Reset index register
            sp = 0;

            Opcodes.Init(this);
        }

        public void LoadFile(Stream io)
        {
            using (BinaryReader sr = new BinaryReader(io))
            {
                byte[] buff = sr.ReadBytes((int)sr.BaseStream.Length);
                
                for(int i=0;i<buff.Length;i++)
                {
                    memory[0x200 + i] = buff[i];
                    Console.Write(buff[i]);
                }
            }
        }

        public void Cycle()
        {
            opcode = (ushort)((memory[PC] << 8) | (memory[PC + 1]));
        }

        public void Draw(SpriteBatch sb)
        {

        }
    }
}
