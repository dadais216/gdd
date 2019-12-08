using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FrbaOfertas.Facturar
{
    public partial class ListarFacturasProveedor : Form
    {
        /*
         Facturar a un proveedor las compras de todos los clientes
         desde un periodo de fechas.
         Antes de facturar se mostraran la lista de compras.
         Se informara el importe de la factura y su numero.
        */
        string chosenProveedor;

        public ListarFacturasProveedor()
        {
            InitializeComponent();
            facturarButton.Enabled = false; // Si no hay resultados, no se puede facturar

            // Setear período a fecha de app - 3 meses.
            string currentDate = ConfigurationManager.AppSettings["fecha"].ToString();
            hastaPicker.Value = DateTime.Parse(currentDate); // Current date
            desdePicker.Value = DateTime.Parse(currentDate).AddMonths(-3); // 3 months before

            // Populate proveedores.
            List<string> proveedores = getProveedores();
            foreach (string proveedor in proveedores)
            {
                selectProveedor.Items.Add(proveedor);
            }
            selectProveedor.SelectedIndex = 0;

        }

        private List<string> getProveedores()
        {
            List<string> proveedores = new List<string> { };
            SqlCommand proveedoresQuery = new SqlCommand("SELECT RS FROM Proveedor", Program.con);
            SqlDataReader reader = proveedoresQuery.ExecuteReader();

            while (reader.Read())
            {
                proveedores.Add(reader.GetValue(0).ToString());
            }
            reader.Close();
            return proveedores;
        }

        private void facturarButton_Click(object sender, EventArgs e)
        {
            // if datagrid is empty do nothing.
        }

        private void listarFacturas_Click(object sender, EventArgs e)
        {
            // Obtener parametros de DESDE, HASTA y proveedor.
            DateTime start = desdePicker.Value;
            DateTime end = hastaPicker.Value;
            chosenProveedor = selectProveedor.SelectedItem.ToString();

            // Create query for proveedores filtering by proveedor and fechas
            SqlCommand query = new SqlCommand(
                @"
                SELECT  Cliente.nombre AS CLIENTE,
		                Oferta.codigo AS COD_OFERTA,
		                Oferta.precio AS PRECIO,
		                Oferta.fecha AS FECHA,
		                Oferta.fecha_Venc AS VENCIMIENTO
                FROM Compra_Oferta
                JOIN Oferta ON Compra_Oferta.oferta = Oferta.codigo
                JOIN Proveedor ON Proveedor.id = Oferta.proveedor
                JOIN Cliente ON Cliente.id = Compra_Oferta.cliente
                WHERE Proveedor.RS = 'Proveedor N°16S.R.L.' AND 
	                  Compra_Oferta.fecha_Compra < '2099-01-01' AND
	                  Compra_Oferta.fecha_Compra > '1990-01-01'
                ORDER BY 3 ASC
                ",
                Program.con
            );

            // Defino parametros de filtro
            //SqlParameter startDate = new SqlParameter();
            //SqlParameter endDate = new SqlParameter();
            //startDate.ParameterName = "@startDate";
            //endDate.ParameterName = "@endDate";
            //startDate.Value = before;
            //endDate.Value = moment;

            //query.Parameters.Add(startDate);
            //query.Parameters.Add(endDate);

            PopulateTableWithQuery(query);
            facturarButton.Enabled = true;
        }

        public void PopulateTableWithQuery(SqlCommand query)
        {
            var adapter = new SqlDataAdapter(query);
            var table = new DataTable();
            adapter.Fill(table);
            TablaProveedores.DataSource = table;
            TablaProveedores.AutoResizeColumns();

        }
    }
}
