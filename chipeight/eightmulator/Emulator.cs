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
        Color[] data = new Color[64 * 32];
        public byte delay_timer;
        public byte sound_timer;
        public bool draw = false;
        public ushort[] stack = new ushort[16];
        public ushort sp;
        public byte[] keys = new byte[16];
        public bool waitKey = false;
        public byte key;
        public Random random;

        public bool running = false;
        public bool ready = false;

        public Opcodes opcodes;

        Texture2D tex;
        public Rectangle size;

        public void Init(GraphicsDevice gd)
        {
            PC = 0x200;         // Program counter starts at 0x200
            opcode = 0;         // Reset current opcode	
            I = 0;              // Reset index register
            sp = 0;

            random = new Random();

            opcodes = new Opcodes(this);

            for(int i = 0; i<font.Length;i++)
            {
                memory[i] = font[i];
            }

            tex = new Texture2D(gd, 64, 32);
            gd.Textures[0] = null;

            gfx = new byte[64 * 32];
        }

        public void LoadFile(Stream io)
        {
            Init(tex.GraphicsDevice);
            using (BinaryReader sr = new BinaryReader(io))
            {
                byte[] buff = sr.ReadBytes((int)sr.BaseStream.Length);
                
                for(int i=0;i<buff.Length;i++)
                {
                    memory[0x200 + i] = buff[i];
                }

                //running = true;
                ready = true;
            }
        }

        public void Cycle()
        {
            if (delay_timer > 0) delay_timer--;
            if (sound_timer > 0)
            {
                sound_timer--;
            }

            opcode = (ushort)((memory[PC] << 8) | (memory[PC + 1]));

            Console.WriteLine(opcode.ToString("X"));

            if(!opcodes.DoOpcode(opcode))
            {
                Console.WriteLine("Problem executing opcode [{0}]! PC++", opcode.ToString("X"));
            }

            if(draw)
            {
                for (int i = 0; i < gfx.Length; i++)
                {
                    data[i] = new Color(gfx[i] * 255, gfx[i] * 255, gfx[i] * 255);
                }

                tex.GraphicsDevice.Textures[0] = null;
                tex.SetData<Color>(data);

                draw = false;
            }
            PC += 2;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, size, Color.White);
        }

        byte[] font = new byte[80]
        {
            0xF0, 0x90, 0x90, 0x90, 0xF0,     //0
            0x20, 0x60, 0x20, 0x20, 0x70,     //1
            0xF0, 0x10, 0xF0, 0x80, 0xF0,     //2
            0xF0, 0x10, 0xF0, 0x10, 0xF0,     //3
            0x90, 0x90, 0xF0, 0x10, 0x10,     //4
            0xF0, 0x80, 0xF0, 0x10, 0xF0,     //5
            0xF0, 0x80, 0xF0, 0x90, 0xF0,     //6
            0xF0, 0x10, 0x20, 0x40, 0x40,     //7
            0xF0, 0x90, 0xF0, 0x90, 0xF0,     //8
            0xF0, 0x90, 0xF0, 0x10, 0xF0,     //9
            0xF0, 0x90, 0xF0, 0x90, 0x90,     //A
            0xE0, 0x90, 0xE0, 0x90, 0xE0,     //B
            0xF0, 0x80, 0x80, 0x80, 0xF0,     //C
            0xE0, 0x90, 0x90, 0x90, 0xE0,     //D
            0xF0, 0x80, 0xF0, 0x80, 0xF0,     //E
            0xF0, 0x80, 0xF0, 0x80, 0x80      //F
        };
    }
}
