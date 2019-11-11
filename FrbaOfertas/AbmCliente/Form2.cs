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
        public Form2()
        {
            InitializeComponent();
        }

        Form1 parent;
        public void init(Form1 form1,DataGridViewCellCollection cells)
        {
            parent = form1;
            nombre.Text = cells[2].Value.ToString();
            apellido.Text = cells[3].Value.ToString();
            dni.Text = cells[1].Value.ToString();
            mail.Text = cells[6].Value.ToString();
            telefono.Text = cells[5].Value.ToString();
            direccion.Text = cells[4].Value.ToString();
            ciudad.Text = cells[8].Value.ToString();
            fnac.Text = cells[7].Value.ToString();


            //hacer query que haga join con usuario
            //ver si esta habilitado o no, poner el text en el boton que corresponda
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //por ahora aparece un textbox para dni pero la modificacion no se efectua.
            //podria efectuarse haciendo que el query busque por el dni anterior
            //pero modificar la pk por ahora me hace ruido
            //para mi dni no deberia ser la pk de todas formas

            
            var command = new SqlCommand("UPDATE Cliente SET Cli_Nombre=@no,Cli_Apellido=@ap,Cli_Direccion=@di, " +
                                                            "Cli_Telefono=@te,Cli_Mail=@ma,Cli_Ciudad=@ci " + //"Cli_Fecha_Nac=@fe"+
                                                            "WHERE Cli_Dni=@dn", Program.con);
            command.Parameters.AddWithValue("@no", nombre.Text);
            command.Parameters.AddWithValue("@ap", apellido.Text);
            command.Parameters.AddWithValue("@dn", dni.Text);
            command.Parameters.AddWithValue("@di", direccion.Text);
            command.Parameters.AddWithValue("@te", telefono.Text);
            command.Parameters.AddWithValue("@ma", mail.Text);

            
            //el ToString hace mierda el formato de datetime, lo tengo que arreglar a mano?
            //command.Parameters.AddWithValue("@fe", textBox14.Text);


            command.Parameters.AddWithValue("@ci", ciudad.Text);
            
            command.ExecuteNonQuery();

            parent.doQuery();
            Close();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //
        }
    }
}
