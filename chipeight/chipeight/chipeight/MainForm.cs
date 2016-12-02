using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using eightmulator;
using System.IO;

namespace chipeight
{
    public partial class MainForm : Form
    {
        Emulator emul8;
        public Registers regs;
        public Code code;

        public MainForm(Emulator emul8)
        {
            this.emul8 = emul8;
            regs = new Registers(emul8);
            regs.Hide();
            code = new Code(emul8);
            code.Hide();
            InitializeComponent();
        }

        public IntPtr getDrawSurface()
        {
            return canvas.Handle;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            emul8.size = new Microsoft.Xna.Framework.Rectangle(0, 0, canvas.Size.Width, canvas.Size.Height);
            Stream file = openFileDialog1.OpenFile();
            emul8.LoadFile(file);
        }

        private void registersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            regs.Show();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            emul8.size = new Microsoft.Xna.Framework.Rectangle(0, 0, canvas.Size.Width, canvas.Size.Height);
        }

        private void codeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Show();
        }
    }
}
