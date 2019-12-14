using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas.ListadoEstadistico
{
    public partial class ListadoView : Form
    {
        SqlCommand query;
        DateTime now;
        DateTime lastSemester;

        public ListadoView()
        {
            InitializeComponent();
            now = DateTime.Parse(ConfigurationManager.AppSettings["fecha"]);
            lastSemester = now.AddMonths(-6);

            selectSemestreCombo.Items.Add("Semestre Actual");
            selectSemestreCombo.Items.Add("Semestre Anterior");
            selectSemestreCombo.SelectedIndex = 0;  // Por default, mostrar semestre actual
        }

        private void BestRent_Click(object sender, EventArgs e)
        {   
            // Mejores descuentos
            query = new SqlCommand();
            query.Connection = Program.con;
            query.CommandText = @"
            SELECT TOP 5 
                    Proveedor.RS AS Proveedor,
                    FORMAT(AVG(LOS_SIN_VOZ.descuento(Oferta.precio, Oferta.precio_Ficticio)), 'p') as DESCUENTOS_PROMEDIO
            FROM LOS_SIN_VOZ.Oferta
            JOIN LOS_SIN_VOZ.Proveedor ON LOS_SIN_VOZ.Proveedor.id = LOS_SIN_VOZ.Oferta.proveedor
            WHERE LOS_SIN_VOZ.Oferta.fecha > @startDate AND LOS_SIN_VOZ.Oferta.fecha_Venc <= @endDate
            GROUP BY LOS_SIN_VOZ.Proveedor.RS
            ORDER BY AVG(LOS_SIN_VOZ.descuento(LOS_SIN_VOZ.Oferta.precio, LOS_SIN_VOZ.Oferta.precio_Ficticio)) DESC
            ";

            DateTime moment;
            DateTime before;
            int chosenSemestre = int.Parse(selectSemestreCombo.SelectedIndex.ToString());
            if (chosenSemestre == 0) {
                // Semestre actual
                moment = now;
                before = lastSemester;
            }
            else {
                // Setear las variables para los filtros al semestre anterior
                moment = lastSemester;
                before = moment.AddMonths(-6);
            }
              
    		// Defino parametros de filtro
			SqlParameter startDate  = new SqlParameter();
            SqlParameter endDate    = new SqlParameter();
            startDate.ParameterName = "@startDate";
            endDate.ParameterName   = "@endDate";
            startDate.Value = before;//.ToShortDateString();
            endDate.Value = moment;//.ToShortDateString();

            query.Parameters.Add(startDate);
            query.Parameters.Add(endDate);

            PopulateTableWithQuery();
        }

        private void mostFacturadoClick(object sender, EventArgs e)
        {
            // Armo query de proveedores que mas facturaron
            query = new SqlCommand();
            query.Connection = Program.con;
            query.CommandText = @"
            SELECT TOP 5 Proveedor.RS, SUM(Oferta.precio) AS FACTURACION
            FROM LOS_SIN_VOZ.Compra_Oferta
            JOIN LOS_SIN_VOZ.Oferta ON Compra_Oferta.oferta = Oferta.codigo
            JOIN LOS_SIN_VOZ.Proveedor ON Oferta.proveedor = Proveedor.id
            WHERE Oferta.fecha > @startDate AND Oferta.fecha_Venc <= @endDate
            GROUP BY Proveedor.RS
            ORDER BY 2 DESC
            ";

            DateTime moment;
            DateTime before;
            int chosenSemestre = int.Parse(selectSemestreCombo.SelectedIndex.ToString());
            if (chosenSemestre == 0)
            {
                // Semestre actual
                moment = now;
                before = lastSemester;
            }
            else
            {
                // Setear las variables para los filtros al semestre anterior
                moment = lastSemester;
                before = moment.AddMonths(-6);
            }


            // Defino parametros de filtro
            SqlParameter startDate = new SqlParameter();
            SqlParameter endDate = new SqlParameter();
            startDate.ParameterName = "@startDate";
            endDate.ParameterName = "@endDate";
            startDate.Value = before;
            endDate.Value = moment;

            query.Parameters.Add(startDate);
            query.Parameters.Add(endDate);


            PopulateTableWithQuery();
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
