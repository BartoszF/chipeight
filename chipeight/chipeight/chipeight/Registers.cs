using eightmulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chipeight
{
    public partial class Registers : Form
    {
        Emulator emul8;

        public Registers(Emulator emul8)
        {
            InitializeComponent();
            this.emul8 = emul8;
        }

        public void RegUpdate()
        {
            V0_val.Text = emul8.V[0].ToString("X");
            V1_val.Text = emul8.V[1].ToString("X");
            V2_val.Text = emul8.V[2].ToString("X");
            V3_val.Text = emul8.V[3].ToString("X");
            V4_val.Text = emul8.V[4].ToString("X");
            V5_val.Text = emul8.V[5].ToString("X");
            V6_val.Text = emul8.V[6].ToString("X");
            V7_val.Text = emul8.V[7].ToString("X");
            V8_val.Text = emul8.V[8].ToString("X");
            V9_val.Text = emul8.V[9].ToString("X");
            VA_val.Text = emul8.V[10].ToString("X");
            VB_val.Text = emul8.V[11].ToString("X");
            VC_val.Text = emul8.V[12].ToString("X");
            VD_val.Text = emul8.V[13].ToString("X");
            VE_val.Text = emul8.V[14].ToString("X");
            VF_val.Text = emul8.V[15].ToString("X");

            I_val.Text = emul8.I.ToString("X");
            PC_val.Text = emul8.PC.ToString("X");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Registers_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    }
}
