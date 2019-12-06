using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas
{
    public partial class ErrorWindow : Form
    {
        public ErrorWindow(string s)
        {
            InitializeComponent();
            textBox1.Text = s;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
