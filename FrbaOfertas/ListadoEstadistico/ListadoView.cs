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
        SqlCommand query;

        public ListadoView()
        {
            InitializeComponent();
            selectSemestreCombo.Items.Add("1° Semestre 2018");
            selectSemestreCombo.Items.Add("2° Semestre 2018");
            selectSemestreCombo.Items.Add("1° Semestre 2019");
            selectSemestreCombo.Items.Add("2° Semestre 2019");
            selectSemestreCombo.SelectedIndex = 0;
        }

        private void BestRent_Click(object sender, EventArgs e)
        {   
            // Mejores descuentos
            query = new SqlCommand();
            query.Connection = Program.con;
            query.CommandText = @"
            SELECT TOP 10 Proveedor.RS AS Proveedor,
            FORMAT(AVG(dbo.descuento(Oferta.precio, Oferta.precio_Ficticio)), 'p') as DESCUENTOS_PROMEDIO
            FROM Oferta
            JOIN Proveedor ON Proveedor.id = Oferta.proveedor
            WHERE Oferta.fecha > '1990-01-01' AND Oferta.fecha_Venc < '2300-01-01'
            GROUP BY Proveedor.RS
            ORDER BY AVG(dbo.descuento(Oferta.precio, Oferta.precio_Ficticio)) DESC
            ";
            FillTable();
        }

        private void mostFacturadoClick(object sender, EventArgs e)
        {
            // Mas Facturado
            query = new SqlCommand();
            query.Connection = Program.con;
            query.CommandText = "SELECT id,dni,nombre,apellido FROM Cliente ";
            doQuery();
            System.Diagnostics.Debug.WriteLine(selectSemestreCombo.SelectedIndex);
        }

        public void FillTable() {
            var adapter = new SqlDataAdapter(query);
            var table = new DataTable();
            adapter.Fill(table);
            TablaListado.DataSource = table;
            TablaListado.AutoResizeColumns();     

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


    }
}
