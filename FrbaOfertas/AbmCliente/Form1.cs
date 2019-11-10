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

        string connectionString = "Data Source= localhost\\SQLSERVER2012;" +
                                 "Initial Catalog = GD2C2019; " +
                                 "User ID = gdCupon2019; Password = gd2019;";
        
        private void Button1_Click(object sender, EventArgs e)
        {
            //using esta para que se llame al destructor de con cuando se salga de scope, y se cierre la conexion.
            //Si no estuviera la conexion se cerraria cuando el gc tenga ganas, o se tendria que cerrar explicitamente
            //al final del metodo y en un catch
            using (System.Data.SqlClient.SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM Cliente", con);

                DataTable table = new DataTable();

                adp.Fill(table);

                dataGridView1.DataSource = table;
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
