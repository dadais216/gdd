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

namespace FrbaOfertas.AbmCliente
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            var command = new SqlCommand("INSERT INTO Cliente " +
                "(Cli_Dni,Cli_Nombre,Cli_Apellido,Cli_Direccion,Cli_Telefono,Cli_Mail,Cli_Ciudad) " + //"Cli_Fecha_Nac=@fe+"
                "VALUES (@dn,@no,@ap,@di,@te,@ma,@ci)", Program.con);
            command.Parameters.AddWithValue("@no", nombre.Text);
            command.Parameters.AddWithValue("@ap", apellido.Text);
            command.Parameters.AddWithValue("@dn", dni.Text);
            command.Parameters.AddWithValue("@di", direccion.Text);
            command.Parameters.AddWithValue("@te", telefono.Text);
            command.Parameters.AddWithValue("@ma", mail.Text);
            command.Parameters.AddWithValue("@ci", ciudad.Text);


            //el ToString hace mierda el formato de datetime, lo tengo que arreglar a mano?
            //command.Parameters.AddWithValue("@fe", textBox14.Text);



            command.ExecuteNonQuery();

            Close();

        }
    }
}
