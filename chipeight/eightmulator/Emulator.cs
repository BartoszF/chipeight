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
        public bool draw = false;
        public ushort[] stack = new ushort[16];
        public ushort sp;
        public byte[] keys = new byte[16];
        public bool waitKey = false;
        public byte key;
        public Random random;

        public Opcodes opcodes;

        public void Init()
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
            if (delay_timer > 0) delay_timer--;
            if (sound_timer > 0)
            {
                sound_timer--;
            }

            opcode = (ushort)((memory[PC] << 8) | (memory[PC + 1]));

            if(!opcodes.DoOpcode(opcode))
            {
                Console.WriteLine("Problem executing opcode [{0}]! PC++", opcode);
            }

            PC += 2;
        }

        public void Draw(SpriteBatch sb)
        {

        }

        byte[] font = new byte[80]
        {
            0xF, 0x9, 0x9, 0x9, 0xF,     //0
            0x2, 0x6, 0x2, 0x2, 0x7,     //1
            0xF, 0x1, 0xF, 0x8, 0xF,     //2
            0xF, 0x1, 0xF, 0x1, 0xF,     //3
            0x9, 0x9, 0xF, 0x1, 0x1,     //4
            0xF, 0x8, 0xF, 0x1, 0xF,     //5
            0xF, 0x8, 0xF, 0x9, 0xF,     //6
            0xF, 0x1, 0x2, 0x4, 0x4,     //7
            0xF, 0x9, 0xF, 0x9, 0xF,     //8
            0xF, 0x9, 0xF, 0x1, 0xF,     //9
            0xF, 0x9, 0xF, 0x9, 0x9,     //A
            0xE, 0x9, 0xE, 0x9, 0xE,     //B
            0xF, 0x8, 0x8, 0x8, 0xF,     //C
            0xE, 0x9, 0x9, 0x9, 0xE,     //D
            0xF, 0x8, 0xF, 0x8, 0xF,     //E
            0xF, 0x8, 0xF, 0x8, 0x8      //F
        };
    }
}
