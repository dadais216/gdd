using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas
{
    public partial class Login : Form
    {
        SqlConnection conexion = Program.con;
        string fecha;
        public Login(string fecha)
        {
            this.fecha = fecha;
            conexion.Open();
            InitializeComponent();
            username.Text = "admin";
            password.Text = "w23e";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
