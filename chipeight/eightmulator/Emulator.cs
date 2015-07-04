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

        public void Cycle()
        {

        }

        public void Draw(SpriteBatch sb)
        {

        }
    }
}
