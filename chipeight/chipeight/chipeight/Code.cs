using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;
using eightmulator;
using System.IO;

namespace chipeight
{
    public partial class Code : Form
    {
        Emulator emul8;

        public Code(Emulator emul8)
        {
            InitializeComponent();
            this.emul8 = emul8;
        }

        public void Update()
        {
            if (this.editor != null)
            {
                string mem = "";
                for(int i =512;i<emul8.memory.Length;i+=2)
                {
                    string t = ((emul8.memory[i] << 8) | (emul8.memory[i + 1])).ToString("X");
                    if (t != "0")
                    {
                        mem += t;
                        mem += '\n';
                    }
                }

                this.editor.Text = mem;
                //this.editor.GotoPosition((int)emul8.PC/2);
            }
        }

        public string parseOp(ushort op)
        {
            if (Opcodes.opcodes.ContainsKey((ushort)(op & (ushort)0xF00F)))
            {
                return op.ToString("X");
            }
            else if (Opcodes.opcodes.ContainsKey((ushort)(op & (ushort)0xF000)))
            {
                return op.ToString("X");
            }
            else if (Opcodes.opcodes.ContainsKey((ushort)(op & (ushort)0x000F)))
            {
                return op.ToString("X");
            }
            else if (Opcodes.opcodes.ContainsKey((ushort)(op & (ushort)0xF0FF)))
            {
                return op.ToString("X");
            }
            else
            {
                return "UNKNOWN" + op.ToString("X");
            }
        }

        private void Code_Load(object sender, EventArgs e)
        {
            this.Hide();
        }

        private int maxLineNumberCharLength;
        private void editor_TextChanged(object sender, EventArgs e)
        {
            /*var maxLineNumberCharLength = editor.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            editor.Margins[0].Width = editor.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;*/
        }
    }
}
