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

namespace FrbaOfertas.AbmRol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SqlDataAdapter adp = new SqlDataAdapter("SELECT id,name FROM Rol ", Program.con);
            DataTable table = new DataTable();


            adp.Fill(table);
            //@TODO mismo tema que con cliente, quiero el id para ahorrar un join pero no quiero mostrarlo

            dataGridView1.DataSource = table;


        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var modForm = new Form2(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

            modForm.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var newForm = new Form3();

            newForm.Show();
        }
    }
}
