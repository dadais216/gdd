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
    public partial class Form2 : Form
    {


        Form1 parent;
        string userId;
        public Form2(Form1 form1, string userId_, DataGridViewCellCollection cells)
        {
            InitializeComponent();

            parent = form1;
            nombre.Text = cells[1].Value.ToString();
            apellido.Text = cells[2].Value.ToString();
            dni.Text = cells[0].Value.ToString();
            mail.Text = cells[5].Value.ToString();
            telefono.Text = cells[4].Value.ToString();
            direccion.Text = cells[3].Value.ToString();
            ciudad.Text = cells[7].Value.ToString();
            fnac.Text = cells[6].Value.ToString();


            userId = userId_;

            //hacer query que haga join con usuario
            //ver si esta habilitado o no, poner el text en el boton que corresponda
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            var command = new SqlCommand("UPDATE Cliente SET Cli_Dni=@dn,Cli_Nombre=@no,Cli_Apellido=@ap,Cli_Direccion=@di, " +
                                                            "Cli_Telefono=@te,Cli_Mail=@ma,Cli_Ciudad=@ci " + //"Cli_Fecha_Nac=@fe"+
                                                            "WHERE id=@id", Program.con);
            command.Parameters.AddWithValue("@no", nombre.Text);
            command.Parameters.AddWithValue("@ap", apellido.Text);
            command.Parameters.AddWithValue("@dn", dni.Text);
            command.Parameters.AddWithValue("@di", direccion.Text);
            command.Parameters.AddWithValue("@te", telefono.Text);
            command.Parameters.AddWithValue("@ma", mail.Text);
            command.Parameters.AddWithValue("@ci", ciudad.Text);

            command.Parameters.AddWithValue("@id", userId);
            
            //el ToString hace mierda el formato de datetime, lo tengo que arreglar a mano?
            //command.Parameters.AddWithValue("@fe", textBox14.Text);


            
            command.ExecuteNonQuery();

            parent.doQuery();
            Close();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            /* falta hacer la tabla usuario, esta es la idea nomas
            
            var command = new SqlCommand("UPDATE Usuario SET habilitado = "+ usuarioHabilitado? "true ": "false " +"WHERE id= "+ userId, Program.con);
            command.ExecuteNonQuery();

            */
        }
    }
}
