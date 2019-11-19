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

        List<string> userIds=new List<string>();
        public Form1()
        {
            InitializeComponent();
        }


        string query;
        private void Button1_Click(object sender, EventArgs e)
        {
            query = "SELECT * FROM Cliente ";

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

            try
            {
                adp.Fill(table);

                //esta gilada esta para no mostrar ids pero traermelos en un mismo query

                if(table.Rows.Count>userIds.Capacity)//chequeo porque c# no se la banca
                    userIds.Capacity = table.Rows.Count;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    userIds.Add(table.Rows[i].ItemArray[0].ToString());
                }

                table.Columns.RemoveAt(0);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                //esto creo que solo pasa cuando se pone una letra en dni, tambien se podria solucionar ahi
            }

            dataGridView1.DataSource = table;
        }


        private void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var modForm = new Form2(this, userIds[e.RowIndex],dataGridView1.Rows[e.RowIndex].Cells);


            modForm.Show();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            var newForm = new Form3();

            newForm.Show();
        }
    }
}
