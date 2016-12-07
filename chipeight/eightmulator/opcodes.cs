using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eightmulator
{
    public class Opcode
    {
        public Func<ushort, bool> execF;
        public Func<ushort, string> listF;

        public Opcode(Func<ushort, bool> e, Func<ushort,string> l)
        {
            this.execF = e;
            this.listF = l;
        }
    }
    public class Opcodes
    {
        public static Dictionary<ushort, Opcode> opcodes;// = new Dictionary<ushort, Func<ushort, bool>>();
        public static Emulator emu;

        public Opcodes(Emulator e)
        {
            emu = e;

            opcodes = new Dictionary<ushort, Opcode>()
            {
                {0x00E0, new Opcode(cls,listCls)},
                {0x1000, new Opcode(JP,listJP)},
                {0x00EE, new Opcode(ret,listRet)},
                {0x2000, new Opcode(call,listCall)},
                {0x3000, new Opcode(SEXb,listSEXb)},
                {0x4000, new Opcode(SNEXb,listSNEXb)},
                {0x5000, new Opcode(SEXY,listSEXY)},
                {0x6000, new Opcode(LDXb,listLDXb)},
                {0x7000, new Opcode(ADXb,listADXb)},
                {0x8000, new Opcode(LDXY,listLDXY)},
                {0x8001, new Opcode(ORXY,listORXY)},
                {0x8002, new Opcode(ANDXY,listANDXY)},
                {0x8003, new Opcode(XORXY,listXORXY)},
                {0x8004, new Opcode(ADDXY,listADDXY)},
                {0x8005, new Opcode(SUBXY,listSUBXY)},
                {0x8006, new Opcode(SHRXY,listSHRXY)},
                {0x8007, new Opcode(SUBNXY,listSUBNXY)},
                {0x8008, new Opcode(SHLXY,null)},
                {0x9000, new Opcode(SNEXy,null)},
                {0xA000, new Opcode(LDIa,null)},
                {0xB000, new Opcode(JP0a,null)},
                {0xC000, new Opcode(RNDxb,null)},
                {0xD000, new Opcode(DRWXYN,null)},
                {0xE09E, new Opcode(SKPx,null)},
                {0xE0A1, new Opcode(SKPNx,null)},
                {0xF007, new Opcode(LDxDT,listLDxDT)},
                {0xF00A, new Opcode(LDxk,null)},
                {0xF015, new Opcode(LDDTx,null)},
                {0xF018, new Opcode(LDSTx,null)},
                {0xF01E, new Opcode(ADDIx,null)},
                {0xF029, new Opcode(LDFx,null)},
                {0xF033, new Opcode(decVX,null)},
                {0xF055, new Opcode(LDIx,null)},
                {0xF065, new Opcode(LDxI,null)}
            };
        }

        public bool DoOpcode(ushort op)
        {
            if (opcodes.ContainsKey((ushort)(op & (ushort)0xF00F)))
            {
                return opcodes[(ushort)(op & (ushort)0xF00F)].execF(op);
            }
            else if (opcodes.ContainsKey((ushort)(op & (ushort)0xF000)))
            {
                return opcodes[(ushort)(op & (ushort)0xF000)].execF(op);
            }
            else if(opcodes.ContainsKey((ushort)(op & (ushort)0x000F)))
            {
                return opcodes[(ushort)(op & (ushort)0x000F)].execF(op);
            }
            else if (opcodes.ContainsKey((ushort)(op & (ushort)0xF0FF)))
            {
                return opcodes[(ushort)(op & (ushort)0xF0FF)].execF(op);
            }
            else
            {
                Debugger.WriteLine("ERROR : UNKOWN OPCODE " + op.ToString("X"));
                return false;
            }
        }

        public string ListOpcode(ushort op)
        {
            if (opcodes.ContainsKey((ushort)(op & (ushort)0xF00F)))
            {
                if(opcodes[(ushort)(op & (ushort)0xF00F)].listF != null)
                    return opcodes[(ushort)(op & (ushort)0xF00F)].listF(op);
            }
            else if (opcodes.ContainsKey((ushort)(op & (ushort)0xF000)))
            {
                if(opcodes[(ushort)(op & (ushort)0xF000)].listF != null)
                    return opcodes[(ushort)(op & (ushort)0xF000)].listF(op);
            }
            else if (opcodes.ContainsKey((ushort)(op & (ushort)0x000F)))
            {
                if(opcodes[(ushort)(op & (ushort)0x000F)].listF != null)
                    return opcodes[(ushort)(op & (ushort)0x000F)].listF(op);
            }
            else if (opcodes.ContainsKey((ushort)(op & (ushort)0xF0FF)))
            {
                if(opcodes[(ushort)(op & (ushort)0xF0FF)].listF != null)
                    return opcodes[(ushort)(op & (ushort)0xF0FF)].listF(op);
            }
            else
            {
                return "UNKNOWN";
            }

            return "";
        }

        public bool cls(ushort op)  //00E0
        {
            for(int i = 0;i<emu.gfx.Length;i++)
            {
                emu.gfx[i] = 0;
            }

            Debugger.WriteLine("Clear screen");

            return true;
        }

        public string listCls(ushort op)
        {
            return "CLS";
        }

        public bool JP(ushort op)   //1000
        {
            ushort p = (ushort)(op & 0x0FFF);
            emu.PC = (ushort)(p);

            Debugger.WriteLine("Set PC to " + p.ToString("X"));

            return true;
        }

        public string listJP(ushort op)   //1000
        {
            ushort p = (ushort)(op & 0x0FFF);

            return "JP #"+p.ToString("X");
        }

        public bool ret(ushort op)  //00EE
        {
            if (emu.sp == 0) return false;
            emu.PC = (ushort)(emu.stack[--emu.sp]-2);

            Debugger.WriteLine("Return");

            return true;
        }

        public string listRet(ushort op)
        {
            return "RET";
        }

        public bool call(ushort op) //0x2000
        {
            if (emu.sp >= 15) return false;
            emu.stack[emu.sp++] = emu.PC;
            emu.PC = (ushort)((op & 0x0FFF)-2);

            Debugger.WriteLine("Call");

            return true;
        }

        public string listCall(ushort op) //0x2000
        {
            ushort p = (ushort)((op & 0x0FFF) - 2);

            return "CALL #" + p.ToString("X");
        }

        public bool SEXb(ushort op) //3000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            if (emu.V[x] == nn) emu.PC+=2;

            Debugger.WriteLine("Skip if Vx == nn | x = " + x.ToString("X") + " Vx = " + emu.V[x].ToString("X") + " nn = " + nn.ToString("X"));

            return true;
        }

        public string listSEXb(ushort op) //3000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            return "SE "+x.ToString("X") + ", #"+nn.ToString("X");
        }

        public bool SNEXb(ushort op) //4000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            if (emu.V[x] != nn) emu.PC += 2;

            Debugger.WriteLine("Skip if Vx != nn | x = " + x.ToString("X") + " Vx = " + emu.V[x].ToString("X") + " nn = " + nn.ToString("X"));

            return true;
        }

        public string listSNEXb(ushort op) //4000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            return "SNE " + x.ToString("X") + ", #" + nn.ToString("X");
        }

        public bool SEXY(ushort op) //5000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            if (emu.V[x] == emu.V[y]) emu.PC += 2;

            Debugger.WriteLine("Skip if Vx != Vy | x = " + x.ToString("X") + " Vx = " + emu.V[x].ToString("X") + "y = " + y.ToString("X") + " Vy = " + emu.V[y].ToString("X"));

            return true;
        }

        public string listSEXY(ushort op) //5000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            return "SE " + x.ToString("X") + ", " + y.ToString("X");
        }

        public bool LDXb(ushort op) //6000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            Debugger.WriteLine("Load nn in x | x: " + x + " nn: " + nn);

            emu.V[x] = nn;

            return true;
        }

        public string listLDXb(ushort op) //6000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            return "LD " + x.ToString("X") + ", #" + nn.ToString("X");
        }

        public bool ADXb(ushort op) //7000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            emu.V[x] += nn;

            Debugger.WriteLine("Add nn to Vx | x = " + x.ToString("X") + " nn = " + nn.ToString("X"));

            return true;
        }

        public string listADXb(ushort op) //7000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte nn = (byte)(op & 0x00FF);

            return "ADD "+x.ToString("X") + ", #" + nn.ToString("X");
        }

        public bool LDXY(ushort op) //8000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            emu.V[x] = emu.V[y];

            Debugger.WriteLine("Load Vy to Vx | x = " + x.ToString("X") + " y = " + y.ToString("X"));

            return true;
        }

        public string listLDXY(ushort op) //8000
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            return "LD " + x.ToString("X") + ", " + y.ToString("X");
        }

        public bool ORXY(ushort op) //8001
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            emu.V[x] |= emu.V[y];

            Debugger.WriteLine("X = X OR Y");

            return true;
        }

        public string listORXY(ushort op) //8001
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            return "OR " + x.ToString("X") + ", " + y.ToString("X");
        }

        public bool ANDXY(ushort op) //8002
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            emu.V[x] &= emu.V[y];

            Debugger.WriteLine("X = X AND Y");

            return true;
        }

        public string listANDXY(ushort op) //8002
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            return "AND " + x.ToString("X") + ", " + y.ToString("X");
        }

        public bool XORXY(ushort op) //8003
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            emu.V[x] ^= emu.V[y];

            Debugger.WriteLine("X = X XOR Y");

            return true;
        }

        public string listXORXY(ushort op) //8003
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            return "XOR " + x.ToString("X") + ", " + y.ToString("X");
        }

        public bool ADDXY(ushort op) //8004
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            int temp = emu.V[x] + emu.V[y];

            emu.V[x] += emu.V[y];

            if (temp > 255)
            {
                emu.V[0xF] = 1;
                emu.V[x] = (byte)(temp & 0xFF);
            }

            Debugger.WriteLine("X = X + Y");

            return true;
        }

        public string listADDXY(ushort op) //8004
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            return "ADD " + x.ToString("X") + ", " + y.ToString("X");
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

            Debugger.WriteLine("X = X - Y If X > Y -> F = 1");

            return true;
        }

        public string listSUBXY(ushort op) //8005
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            return "SUB " + x.ToString("X") + ", " + y.ToString("X");
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

            Debugger.WriteLine("X = X >> 1");

            return true;
        }

        public string listSHRXY(ushort op) //8006
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            return "SHR " + x.ToString("X") + ", " + y.ToString("X");
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

            Debugger.WriteLine("Y = Y - X If Y > X -> F = 1 ");

            return true;
        }

        public string listSUBNXY(ushort op) //8007
        {
            byte x = (byte)((op & 0x0F00) >> 8);
            byte y = (byte)((op & 0x00F0) >> 4);

            return "SUBN " + x.ToString("X") + ", " + y.ToString("X");
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

            Debugger.WriteLine(" X = X LHS 1");

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
            ushort nnn = (ushort)(op & 0x0FFF);

            emu.I = nnn;

            Debugger.WriteLine("Load {0} to I ({1})", nnn, nnn.ToString("X"), null);

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
            byte x = (byte)(emu.V[(byte)((op & 0x0F00) >> 8)] % (byte)64);
            byte y = (byte)(emu.V[(byte)((op & 0x00F0) >> 4)] % (byte)32);
            byte b = (byte)((op & 0x000F));

            for (byte yline=0;yline<b;yline++)
            {
                byte data = emu.memory[emu.I+yline]; //this retreives the byte for a give line of pixels
                for(byte xpix=0;xpix<8;xpix++)
                {
                    //if ((data&(0x80>>xpix))!=0)
                    if(GetBit(data,7-xpix) == true)
                    {
                        int pos = xpix + x + ((yline + y) * 64);
                        if (emu.gfx[pos] == 1) emu.V[0xF] = 1; //there has been a collision
                        else emu.V[0xF] = 0;
                        emu.gfx[pos]^=1;	//note: coordinate registers from opcode
                    }
                }
            }

            emu.draw = true;

            Debugger.WriteLine("Draw {0} bytes of sprite at {1},{2}", b, x, y);

            return true;
        }

        public bool SKPx(ushort op) //E09E
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            if(emu.keys[emu.V[x]] != Microsoft.Xna.Framework.Input.KeyState.Up)
            {
                emu.PC += 2;
            }

            return true;
        }

        public string listSKPx(ushort op) //E09E
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            return "SKP " + x.ToString("X");
        }

        public bool SKPNx(ushort op) //E0A1
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            if (emu.keys[emu.V[x]] == Microsoft.Xna.Framework.Input.KeyState.Up)
            {
                emu.PC += 2;
            }

            return true;
        }

        public string listSKPNx(ushort op) //E0A1
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            return "SKPN " + x.ToString("X");
        }

        public bool LDxDT(ushort op) //F007
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            emu.V[x] = emu.delay_timer;

            return true;
        }

        public string listLDxDT(ushort op) //F007
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            return "LD " + x.ToString("X") + ", DT";
        }

        public bool LDxk(ushort op) //F00A
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            //Little hack ;)
            emu.waitKey = true;
            emu.key = x;

            return true;
        }

        public string listLDxk(ushort op) //F00A
        {
            byte x = (byte)((op & 0x0F00) >> 8);

            return "LD " + x.ToString("X") + ", K";
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
