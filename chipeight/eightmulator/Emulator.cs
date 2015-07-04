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
        ushort opcode;
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

        }

        public void Draw(SpriteBatch sb)
        {

        }
    }
}
