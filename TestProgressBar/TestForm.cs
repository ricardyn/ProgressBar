using AdnRme;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace TestProgressBar
{
    public partial class TestForm : Form
    {
        private ProgressForm pf;
        private int max;

        public TestForm()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMax.Text, out max))
            {
                MessageBox.Show("Enter a valid integer number");
                return;
            }

            if (max <= 1)
            {
                MessageBox.Show("Enter a number major or equal than 2");
                return;
            }

            this.pf = new ProgressForm("Progress", "Processing test...", max);
            this.numericUpDown1.Maximum = max;

            if (chkBox.Checked)
            {
                for (int i = 0; i < max; i++)
                {
                    pf.Increment();
                    Thread.Sleep(50);
                }
                
                MessageBox.Show(
                    "All documents load successfully.\nClick Ok to continue.",
                    "Document load",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                
                pf.Close();
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
            {
                return;
            }

            if (pf is null)
            {
                numericUpDown1.Value = 0;
                return;
            }

            pf.Increment();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
            if (!(pf is null)) pf.Close();
        }
    }
}
