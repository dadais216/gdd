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
        string[] userIds;
        public Form1()
        {
            InitializeComponent();
        }


        string query;
        private void Button1_Click(object sender, EventArgs e)
        {
            //pido cada uno en vez de usar * por si hay un cambio de columnas, que no explote nada 
            query = "SELECT id,dni,nombre,apellido,direccion,telefono,mail,fecha_Nac,ciudad,saldo FROM Cliente ";

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

            addFilter(textBox1,"nombre LIKE \'%","%\' ");
            addFilter(textBox2, "apellido LIKE \'%", "%\' ");
            addFilter(textBox3, "dni = "," ");
            addFilter(textBox4, "mail LIKE \'%", "%\' ");

            doQuery();
        }

        public void doQuery()
        {
            try
            {
                var table=util.tableQuery(query);

                //esta gilada esta para no mostrar ids pero traermelos en un mismo query

                userIds = new string[table.Rows.Count];

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    userIds[i]=table.Rows[i].ItemArray[0].ToString();
                }

                table.Columns.RemoveAt(0);
                dataGridView1.DataSource = table;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                //esto creo que solo pasa cuando se pone una letra en dni, tambien se podria solucionar ahi
                dataGridView1.DataSource = new DataTable();
            }

        }


        private void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

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
