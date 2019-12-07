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
            PopulateTableWithQuery();
        }

        private void mostFacturadoClick(object sender, EventArgs e)
        {
            // Mas Facturado
            query = new SqlCommand();
            query.Connection = Program.con;
            query.CommandText = @"
            SELECT TOP 5 Proveedor.RS, SUM(Oferta.precio) AS FACTURACION
            FROM Compra_Oferta
            JOIN Oferta ON Compra_Oferta.oferta = Oferta.codigo
            JOIN Proveedor ON Oferta.proveedor = Proveedor.id
            WHERE Oferta.fecha > '1990-01-01' AND Oferta.fecha_Venc < '2300-01-01'
            GROUP BY Proveedor.RS
            ORDER BY 2 DESC
            ";
            PopulateTableWithQuery();
            System.Diagnostics.Debug.WriteLine(selectSemestreCombo.SelectedIndex);
        }

        public void PopulateTableWithQuery()
        {
            var adapter = new SqlDataAdapter(query);
            var table = new DataTable();
            adapter.Fill(table);
            TablaListado.DataSource = table;
            TablaListado.AutoResizeColumns();     

        }
    }
}
