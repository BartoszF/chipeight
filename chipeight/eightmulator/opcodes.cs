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
                {0x00E0, cls},
                {0x1000, JP},
                {0x00EE, ret},
                {0x2000, call},
                {0x3000, SEXb},
                {0x4000, SNEXb},
                {0x5000, SEXY},
                {0x6000, LDXb},
                {0x7000, ADXb},
                {0x8000, LDXY},
                {0x8001, ORXY},
                {0x8002, ANDXY},
                {0x8003, XORXY},
                {0x8004, ADDXY},
                {0x8005, SUBXY},
                {0x8006, SHRXY},
                {0x8007, SUBNXY},
                {0x8008, SHLXY},
                {0x9000, SNEXy},
                {0xA000, LDIa},
                {0xB000, JP0a},
                {0xC000, RNDxb},
                {0xD000, DRWXYN},
                {0xE09E, SKPx},
                {0xE0A1, SKPNx},
                {0xF00A, LDxk},
                {0xF015, LDDTx},
                {0xF018, LDSTx},
                {0xF01E, ADDIx},
                {0xF029, LDFx},
                {0xF033, decVX},
                {0xF055, LDIx},
                {0xF065, LDxI}
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
            else if (opcodes.ContainsKey((ushort)(op & (ushort)0xF0FF)))
            {
                return opcodes[(ushort)(op & (ushort)0xF0FF)](op);
            }
            else
            {
                Console.WriteLine("ERROR : UNKOWN OPCODE " + op.ToString("X"));
                return false;
            }
        }

        public bool cls(ushort op)  //00E0
        {
            for(int i = 0;i<emu.gfx.Length;i++)
            {
                emu.gfx[i] = 0;
            }

            Console.WriteLine("Clear screen");

            return true;
        }

        public bool JP(ushort op)   //1000
        {
            ushort p = (ushort)(op & 0x0FFF);
            emu.PC = (ushort)(p-2);

            Console.WriteLine("Set PC to " + p.ToString("X"));

            return true;
        }

        public bool ret(ushort op)  //00EE
        {
            emu.PC = (ushort)(emu.stack[--emu.sp]-2);

            Console.WriteLine("Return");

            return true;
        }

        public bool call(ushort op) //0x2000
        {
            emu.stack[emu.sp++] = emu.PC;
            emu.PC = (ushort)((op & 0x0FFF) - 2);

            Console.WriteLine("Call");

            return true;
        }

        public bool SEXb(ushort op) //3000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            if (emu.V[x] == nn) emu.PC+=2;

            Console.WriteLine("Skip if Vx == nn | x = " + x.ToString("X") + " Vx = " + emu.V[x].ToString("X") + " nn = " + nn.ToString("X"));

            return true;
        }

        public bool SNEXb(ushort op) //4000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            if (emu.V[x] != nn) emu.PC += 2;

            Console.WriteLine("Skip if Vx != nn | x = " + x.ToString("X") + " Vx = " + emu.V[x].ToString("X") + " nn = " + nn.ToString("X"));

            return true;
        }

        public bool SEXY(ushort op) //5000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            if (emu.V[x] == emu.V[y]) emu.PC += 2;

            Console.WriteLine("Skip if Vx != Vy | x = " + x.ToString("X") + " Vx = " + emu.V[x].ToString("X") + "y = " + y.ToString("X") + " Vy = " + emu.V[y].ToString("X"));

            return true;
        }

        public bool LDXb(ushort op) //6000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            Console.WriteLine("Load nn in x | x: " + x + " nn: " + nn);

            emu.V[x] = nn;

            return true;
        }

        public bool ADXb(ushort op) //7000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            emu.V[x] += nn;

            Console.WriteLine("Add nn to Vx | x = " + x.ToString("X") + " nn = " + nn.ToString("X"));

            return true;
        }

        public bool LDXY(ushort op) //8000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            emu.V[x] = emu.V[y];

            Console.WriteLine("Load Vy to Vx | x = " + x.ToString("X") + " y = " + y.ToString("X"));

            return true;
        }

        public bool ORXY(ushort op) //8001
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            emu.V[x] |= emu.V[y];

            Console.WriteLine("X = X OR Y");

            return true;
        }

        public bool ANDXY(ushort op) //8002
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            emu.V[x] &= emu.V[y];

            Console.WriteLine("X = X AND Y");

            return true;
        }

        public bool XORXY(ushort op) //8003
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            emu.V[x] ^= emu.V[y];

            Console.WriteLine("X = X XOR Y");

            return true;
        }

        public bool ADDXY(ushort op) //8004
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            emu.V[x] += emu.V[y];

            if (emu.V[x] > 255)
            {
                emu.V[0xF] = 1;
                emu.V[x] = 255;
            }

            Console.WriteLine("X = X + Y");

            return true;
        }

        public bool SUBXY(ushort op) //8005
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            if (emu.V[x] > emu.V[y])
            {
                emu.V[0xF] = 1;
            }
            else
            {
                emu.V[0xF] = 0;
            }

            emu.V[x] -= emu.V[y];

            Console.WriteLine("X = X - Y If X > Y -> F = 1");

            return true;
        }

        public bool SHRXY(ushort op) //8006
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            if (GetBit(emu.V[x], 0)) emu.V[0xF] = 1;
            else
            {
                emu.V[0xF] = 0;
            }

            emu.V[x] = (byte)(emu.V[x] >> 1);

            Console.WriteLine("X = X >> 1");

            return true;
        }

        public bool SUBNXY(ushort op) //8007
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            if (emu.V[y] > emu.V[x])
            {
                emu.V[0xF] = 1;
            }
            else
            {
                emu.V[0xF] = 0;
            }

            emu.V[y] -= emu.V[x];

            Console.WriteLine("Y = Y - X If Y > X -> F = 1 ");

            return true;
        }

        public bool SHLXY(ushort op) //8008
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            if (GetBit(emu.V[x], 7)) emu.V[0xF] = 1;
            else
            {
                emu.V[0xF] = 0;
            }

            emu.V[x] = (byte)(emu.V[x] << 1);

            Console.WriteLine(" X = X LHS 1");

            return true;
        }

        public bool SNEXy(ushort op) //9000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            if (emu.V[x] != emu.V[y]) emu.PC += 2;

            return true;
        }

        public bool LDIa(ushort op) //A000
        {
            byte nnn = (byte)(op & 0x0FFF);

            emu.I = nnn;

            Console.WriteLine("Load nnn to I");

            return true;
        }

        public bool JP0a(ushort op) //B000
        {
            byte nnn = (byte)(op & 0x0FFF);

            emu.I = (ushort)(nnn + emu.V[0]);

            return true;
        }

        public bool RNDxb(ushort op) //C000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte kk = (byte)(op & 0x00FF);

            emu.V[x] = (byte)(emu.random.Next(0, 255) & kk);

            return true;
        }

        public bool DRWXYN(ushort op) //D000
        {
            byte x = emu.V[(byte)((op & 0x0F00) >> 8)];
            byte y = emu.V[(byte)((op & 0x00F0) >> 4)];

            for (byte yline=0;yline<(op&0x000F);yline++)
            {
                byte data = emu.memory[emu.I+yline]; //this retreives the byte for a give line of pixels
                for(byte xpix=0;xpix<8;xpix++)
                {
                    if ((data&(0x80>>xpix))!=0)
                    {
                        if (emu.gfx[x + (y * 64)] == 1) emu.V[0xF] = 1; //there has been a collision
                        emu.gfx[x +(y*64)]^=1;	//note: coordinate registers from opcode
                    }
                }
        }

            emu.draw = true;

            return true;
        }

        public bool SKPx(ushort op) //E09E
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            if(emu.keys[emu.V[x]] != 0)
            {
                emu.PC += 2;
            }

            return true;
        }

        public bool SKPNx(ushort op) //E0A1
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            if (emu.keys[emu.V[x]] == 0)
            {
                emu.PC += 2;
            }

            return true;
        }

        public bool LDxk(ushort op) //F00A
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            //Little hack ;)
            emu.waitKey = true;
            emu.key = x;

            return true;
        }

        public bool LDDTx(ushort op) //F015
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            emu.delay_timer = emu.V[x];

            return true;
        }

        public bool LDSTx(ushort op) //F018
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            emu.sound_timer = emu.V[x];

            return true;
        }

        public bool ADDIx(ushort op) //F01E
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            emu.I += emu.V[x];

            return true;
        }

        public bool LDFx(ushort op) //F029
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            emu.I = (ushort)(emu.V[x] * 5);

            return true;
        }

        public bool decVX(ushort op) //0xF033
        {
            emu.memory[emu.I] = (byte)(emu.V[(op & 0x0F00) >> 8] / 100);
            emu.memory[emu.I + 1] = (byte)((emu.V[(op & 0x0F00) >> 8] / 10) % 10);
            emu.memory[emu.I + 2] = (byte)((emu.V[(op & 0x0F00) >> 8] % 100) % 10);

            return true;
        }

        public bool LDIx(ushort op) //F055
        {
            byte x = (byte)((op & 0x0F00)>>8);

            for (byte i = 0; i < emu.V[x];i++)
            {
                emu.memory[emu.I++] = emu.V[i];
            }

            return true;
        }

        public bool LDxI(ushort op) //F065
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            for (byte i = 0; i < emu.V[x]; i++)
            {
                emu.V[i] = emu.memory[emu.I++];
            }

            return true;
        }

        bool GetBit(byte thebyte, int position)
        {
            return (1 == ((thebyte >> position) & 1));
        }
    }
}
