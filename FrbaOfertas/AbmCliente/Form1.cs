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


        
        private void Button1_Click(object sender, EventArgs e)
        {


            string query="SELECT * FROM Cliente ";

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

            Console.WriteLine(query);

            SqlDataAdapter adp = new SqlDataAdapter(query, Program.con);
            DataTable table = new DataTable();
            adp.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
