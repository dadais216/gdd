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
            textBox8.Text = cells[1].Value.ToString();
            textBox9.Text = cells[2].Value.ToString();
            textBox10.Text = cells[0].Value.ToString();
            textBox11.Text = cells[5].Value.ToString();
            textBox12.Text = cells[4].Value.ToString();
            textBox13.Text = cells[3].Value.ToString();
            textBox14.Text = cells[7].Value.ToString();
            textBox15.Text = cells[6].Value.ToString();
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
            command.Parameters.AddWithValue("@no", textBox8.Text);
            command.Parameters.AddWithValue("@ap", textBox9.Text);
            command.Parameters.AddWithValue("@dn", textBox10.Text);
            command.Parameters.AddWithValue("@di", textBox13.Text);
            command.Parameters.AddWithValue("@te", textBox12.Text);
            command.Parameters.AddWithValue("@ma", textBox11.Text);

            
            //el ToString hace mierda el formato de datetime, lo tengo que arreglar a mano?
            //command.Parameters.AddWithValue("@fe", textBox15.Text);


            command.Parameters.AddWithValue("@ci", textBox14.Text);
            
            command.ExecuteNonQuery();

            parent.doQuery();
            Close();

        }
    }
}
