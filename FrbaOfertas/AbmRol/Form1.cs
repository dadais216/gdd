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
        string[] userIds;
        public Form1()
        {
            InitializeComponent();

            doQuery();
        }

        public void doQuery()
        {
            var table = util.tableQuery("SELECT id,nombre,habilitado FROM Rol ");

            userIds = new string[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                userIds[i]=table.Rows[i].ItemArray[0].ToString();
            }

            table.Columns.RemoveAt(0);

            dataGridView1.DataSource = table;
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var modForm = new Form2(this,
                                    userIds[e.RowIndex], 
                                    dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                    (bool)dataGridView1.Rows[e.RowIndex].Cells[1].Value);

            modForm.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var newForm = new Form3(this);

            newForm.Show();
        }
    }
}
