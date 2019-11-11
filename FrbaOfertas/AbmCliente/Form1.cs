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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        string query;
        private void Button1_Click(object sender, EventArgs e)
        {
            query = "SELECT * FROM Cliente";

            bool filterBefore = false;
            var addFilter = new Action<TextBox,string,string>((text,filterQueryBeg,filterQueryEnd) =>
              {
                  if (text.Text != "")
                  {
                      if (filterBefore)
                      {
                          query += "AND ";
                      }
                      else
                      {
                          query += "WHERE ";
                          filterBefore = true;
                      }
                      query += filterQueryBeg + text.Text + filterQueryEnd;
                  }
              });

            addFilter(textBox1,"Cli_Nombre LIKE \'%","%\' ");
            addFilter(textBox2, "Cli_Apellido LIKE \'%", "%\' ");
            addFilter(textBox3, "Cli_Dni = "," ");
            addFilter(textBox4, "Cli_Mail LIKE \'%", "%\' ");

            doQuery();
        }
        public void doQuery()
        {
            SqlDataAdapter adp = new SqlDataAdapter(query, Program.con);
            DataTable table = new DataTable();
            adp.Fill(table);

            //table.Columns.RemoveAt(0);
            //me gustaria no mostrar el id, pero evitar tener que hacer otro query.
            //lo unico que se me ocurre es copiar la columna id a un array y sacarla del dataGridView,
            //manejarla como vector amigo
            //es algo estetico igual 

            dataGridView1.DataSource = table;
        }


        private void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var modForm = new Form2();
            modForm.init(this,dataGridView1.Rows[e.RowIndex].Cells);


            modForm.Show();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            var newForm = new Form3();

            newForm.Show();
        }
    }
}
