using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindCouple
{
    public partial class Started : Form
    {
        private Form1 forma;
        public Started(Form1 form)
        {
            InitializeComponent();
            this.forma = form;
        }

        private void Started_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.forma.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Game(int.Parse(numericUpDown1.Value.ToString()), int.Parse(numericUpDown2.Value.ToString()), this.forma).Show();
            this.Hide();
        }
    }
}
