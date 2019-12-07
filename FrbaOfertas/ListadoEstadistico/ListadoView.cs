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

namespace FrbaOfertas.ListadoEstadistico
{
    public partial class ListadoView : Form
    {
        string[] userIds;

        public ListadoView()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        SqlCommand query;
 
        private void BestRent_Click(object sender, EventArgs e)
        {
            query = new SqlCommand();
            query.Connection = Program.con;
            query.CommandText = "SELECT id, nombre FROM Cliente ";
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
                TablaListado.DataSource = table;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                //esto creo que solo pasa cuando se pone una letra en dni, tambien se podria solucionar ahi
                TablaListado.DataSource = new DataTable();
            }

        }

        private void bestValue_Click(object sender, EventArgs e)
        {
            query = new SqlCommand();
            query.Connection = Program.con;
            query.CommandText = "SELECT id,dni,nombre,apellido FROM Cliente ";
            doQuery();
        }

    }
}
