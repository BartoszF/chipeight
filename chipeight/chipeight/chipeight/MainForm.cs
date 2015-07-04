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

namespace chipeight
{
    public partial class MainForm : Form
    {
        Emulator emul8;

        public MainForm(Emulator emul8)
        {
            this.emul8 = emul8;
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
            emul8.LoadFile(openFileDialog1.OpenFile());
        }
    }
}
