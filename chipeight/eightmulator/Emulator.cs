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
        int opcode;
        byte[] memory = new byte[4096];
        byte[] V = new byte[16];
        ushort I;
        ushort PC;
        byte[] gfx = new byte[64 * 32];
        byte delay_timer;
        byte sound_timer;
        ushort[] stack = new ushort[16];
        ushort sp;
        byte[] keys = new byte[16];

        public void Init()
        {
            PC = 0x200;  // Program counter starts at 0x200
            opcode = 0;      // Reset current opcode	
            I = 0;      // Reset index register
            sp = 0;      // Reset stack pointer

            // Clear display	
            // Clear stack
            // Clear registers V0-VF
            // Clear memory

            // Load fontset
            /*for (int i = 0; i < 80; ++i)
                memory[i] = chip8_fontset[i];*/
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
            opcode = memory[PC] << 8 | memory[PC + 1];
        }

        public void Draw(SpriteBatch sb)
        {

        }
    }
}
