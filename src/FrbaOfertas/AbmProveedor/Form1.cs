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

namespace FrbaOfertas.AbmProveedor
{
    public partial class Form1 : Form
    {
        string[] userIds;
        public Form1()
        {
            InitializeComponent();
        }


        SqlCommand query;
        private void Button1_Click(object sender, EventArgs e)
        {
            query = new SqlCommand();
            query.Connection = Program.con;
            query.CommandText = "SELECT id,RS,dom,ciudad,telefono,CUIT,mail,codigoPostal,rubro,contacto FROM LOS_SIN_VOZ.Proveedor ";

            bool filterBefore = false;
            var addFilter = new Action<string,TextBox, string, string>((param,text, filterQueryBeg, filterQueryEnd) =>
            {
                if (text.Text != "")
                {
                    if (filterBefore)
                    {
                        query.CommandText += "AND ";
                    }
                    else
                    {
                        query.CommandText += "WHERE ";
                        filterBefore = true;
                    }
                    query.CommandText += filterQueryBeg + param + filterQueryEnd;
                    query.Parameters.AddWithValue(param, text.Text);
                }
            });

            addFilter("@rs",textBox1, "RS LIKE '%'+", "+'%' ");
            addFilter("@cu",textBox3, "CUIT = ", " ");
            addFilter("@ma",textBox4, "mail LIKE '%'+", "+'%' ");

            doQuery();
        }

        public void doQuery()
        {
            try
            {
                var adp = new SqlDataAdapter(query);
                var table = new DataTable();
                adp.Fill(table);

                //esta gilada esta para no mostrar ids pero traermelos en un mismo query

                userIds = new string[table.Rows.Count];

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    userIds[i] = table.Rows[i].ItemArray[0].ToString();
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

            var modForm = new Form2(this, userIds[e.RowIndex], dataGridView1.Rows[e.RowIndex].Cells);


            modForm.Show();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            var newForm = new Form3();

            newForm.Show();
        }
    }
}
