using Microsoft.VisualStudio.TestTools.UnitTesting;
using eightmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eightmulator.Tests
{
    [TestClass()]
    public class OpcodesTests
    {
        public Emulator getEmul()
        {
            Emulator emu = new Emulator();
            emu.Init(null);

            return emu;
        }

        [TestMethod()]
        public void OpcodesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DoOpcodeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void clsTest()
        {
            Emulator emu = getEmul();

            emu.gfx[1] = 2;

            emu.opcodes.DoOpcode(0x00E0);

            for (int i = 0; i < emu.gfx.Length; i++)
            {
                Assert.AreEqual(0, emu.gfx[i]);
            }
        }

        [TestMethod()]
        public void JPTest()
        {
            Emulator emu = getEmul();

            emu.opcodes.DoOpcode(0x1212);

            Assert.AreEqual(0x212, emu.PC);
        }

        [TestMethod()]
        public void retTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void callTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SEXbTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 2;

            emu.opcodes.DoOpcode(0x3002);

            Assert.AreEqual(0x202, emu.PC); //It's okay, because we increment PC in Cycle method of Emulator
        }

        [TestMethod()]
        public void SNEXbTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 2;

            emu.opcodes.DoOpcode(0x4003);

            Assert.AreEqual(0x202, emu.PC); //As above
        }

        [TestMethod()]
        public void SEXYTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 2;
            emu.V[1] = 2;

            emu.opcodes.DoOpcode(0x5010);

            Assert.AreEqual(0x202, emu.PC); //As above
        }

        [TestMethod()]
        public void LDXbTest()
        {
            Emulator emu = getEmul();

            emu.opcodes.DoOpcode(0x6010);

            Assert.AreEqual(0x10, emu.V[0]);
        }

        [TestMethod()]
        public void ADXbTest()
        {
            Emulator emu = getEmul();

            emu.opcodes.DoOpcode(0x7010);

            Assert.AreEqual(0x10, emu.V[0]);
        }

        [TestMethod()]
        public void LDXYTest()
        {
            Emulator emu = getEmul();

            emu.V[1] = 1;
            emu.opcodes.DoOpcode(0x8010);

            Assert.AreEqual(0x1, emu.V[0]);
        }

        [TestMethod()]
        public void ORXYTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 2;
            emu.V[1] = 1;
            emu.opcodes.DoOpcode(0x8011);

            Assert.AreEqual(3, emu.V[0]);
        }

        [TestMethod()]
        public void ANDXYTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 2;
            emu.V[1] = 1;
            emu.opcodes.DoOpcode(0x8012);

            Assert.AreEqual(0x1, emu.V[0]);
        }

        [TestMethod()]
        public void XORXYTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 2;
            emu.V[1] = 1;
            emu.opcodes.DoOpcode(0x8013);

            Assert.AreEqual(2^1, emu.V[0]);
        }

        [TestMethod()]
        public void ADDXYTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 20;
            emu.V[1] = 6;
            emu.opcodes.DoOpcode(0x8014);

            Assert.AreEqual(26, emu.V[0]);

            emu.V[0] = 250;
            emu.V[1] = 6;
            emu.opcodes.DoOpcode(0x8014);

            Assert.AreEqual(256 & 0xFF, emu.V[0]);
            Assert.AreEqual(1, emu.V[0xF]);
        }

        [TestMethod()]
        public void SUBXYTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 20;
            emu.V[1] = 6;
            emu.opcodes.DoOpcode(0x8015);

            Assert.AreEqual(14, emu.V[0]);
            Assert.AreEqual(1, emu.V[0xF]);
        }

        [TestMethod()]
        public void SHRXYTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SUBNXYTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SHLXYTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SNEXyTest()
        {
            Emulator emu = getEmul();

            ushort pc = emu.PC;

            emu.V[0] = 10;
            emu.V[1] = 12;

            emu.opcodes.DoOpcode(0x9010);

            Assert.AreNotEqual(pc, emu.PC);

            pc = emu.PC;

            emu.V[0] = 10;
            emu.V[1] = 10;

            emu.opcodes.DoOpcode(0x9010);

            Assert.AreEqual(pc, emu.PC);
        }

        [TestMethod()]
        public void LDIaTest()
        {
            Emulator emu = getEmul();

            emu.opcodes.DoOpcode(0xA210);

            Assert.AreEqual(0x210, emu.I);
        }

        [TestMethod()]
        public void JP0aTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 1;

            emu.opcodes.DoOpcode(0xB100);

            Assert.AreEqual(0x101, emu.I);
        }

        [TestMethod()]
        public void RNDxbTest()
        {
            Emulator emu = getEmul();

            byte x = emu.V[0];

            emu.opcodes.DoOpcode(0xC010);

            Assert.AreNotEqual(x, emu.V[0]);
        }

        [TestMethod()]
        public void DRWXYNTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SKPxTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SKPNxTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LDxkTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LDDTxTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 1;

            emu.opcodes.DoOpcode(0xF015);

            Assert.AreEqual(1, emu.delay_timer);
        }

        [TestMethod()]
        public void LDSTxTest()
        {
            Emulator emu = getEmul();

            emu.V[0] = 1;

            emu.opcodes.DoOpcode(0xF018);

            Assert.AreEqual(1, emu.sound_timer);
        }

        [TestMethod()]
        public void ADDIxTest()
        {
            Emulator emu = getEmul();

            ushort i = emu.I;
            emu.V[0] = 1;

            emu.opcodes.DoOpcode(0xF01E);

            Assert.AreEqual(i+1, emu.I);
        }

        [TestMethod()]
        public void LDFxTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void decVXTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LDIxTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LDxITest()
        {
            Assert.Fail();
        }
    }
}