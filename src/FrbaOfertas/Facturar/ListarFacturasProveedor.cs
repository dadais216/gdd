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
        DateTime fechaActual = DateTime.Parse(ConfigurationManager.AppSettings["fecha"]);

        public ListarFacturasProveedor()
        {
            InitializeComponent();
            facturarButton.Enabled = false; // Si no hay resultados, no se puede facturar

            // Setear período a fecha de app - 1 meses.
            string currentDate = fechaActual.ToString();
            hastaPicker.Value = DateTime.Parse(currentDate); // Current date
            desdePicker.Value = DateTime.Parse(currentDate).AddMonths(-1); // 1 month before

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
            SqlCommand proveedoresQuery = new SqlCommand("SELECT RS FROM LOS_SIN_VOZ.Proveedor", Program.con);
            SqlDataReader reader = proveedoresQuery.ExecuteReader();

            while (reader.Read())
            {
                proveedores.Add(reader.GetValue(0).ToString());
            }
            reader.Close();
            return proveedores;
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
                SELECT  Proveedor.RS,
                        Cliente.nombre AS CLIENTE,
		                Oferta.codigo AS COD_OFERTA,
		                Oferta.precio AS PRECIO,
		                Oferta.fecha AS FECHA,
		                Oferta.fecha_Venc AS VENCIMIENTO,
                        Compra_Oferta.id AS TICKET
                FROM LOS_SIN_VOZ.Compra_Oferta
                JOIN LOS_SIN_VOZ.Oferta ON LOS_SIN_VOZ.Compra_Oferta.oferta = LOS_SIN_VOZ.Oferta.codigo
                JOIN LOS_SIN_VOZ.Proveedor ON LOS_SIN_VOZ.Proveedor.id = LOS_SIN_VOZ.Oferta.proveedor
                JOIN LOS_SIN_VOZ.Cliente ON LOS_SIN_VOZ.Cliente.id = LOS_SIN_VOZ.Compra_Oferta.cliente
                WHERE LOS_SIN_VOZ.Proveedor.RS = @proveedor AND 
                      LOS_SIN_VOZ.Compra_Oferta.factura IS NULL AND
	                  LOS_SIN_VOZ.Compra_Oferta.fecha_Compra <= @endDate AND
	                  LOS_SIN_VOZ.Compra_Oferta.fecha_Compra >= @startDate
                ORDER BY 1 ASC
                ",
                Program.con
            );

            // Defino proveedor y fechas para la query SQL
            SqlParameter startDate = new SqlParameter();
            SqlParameter endDate = new SqlParameter();
            SqlParameter proveedor = new SqlParameter();
            startDate.ParameterName = "@startDate";
            endDate.ParameterName = "@endDate";
            proveedor.ParameterName = "@proveedor";

            startDate.Value = start;
            endDate.Value = end;
            proveedor.Value = chosenProveedor;

            query.Parameters.Add(startDate);
            query.Parameters.Add(endDate);
            query.Parameters.Add(proveedor);

            PopulateTableWithQuery(query);
            // Calculate monto total
            SqlCommand montoFacturaQuery = new SqlCommand(
                @"
                SELECT SUM(Oferta.precio)
                FROM LOS_SIN_VOZ.Compra_Oferta
                JOIN LOS_SIN_VOZ.Oferta ON LOS_SIN_VOZ.Compra_Oferta.oferta = LOS_SIN_VOZ.Oferta.codigo
                JOIN LOS_SIN_VOZ.Proveedor ON LOS_SIN_VOZ.Proveedor.id = LOS_SIN_VOZ.Oferta.proveedor
                WHERE LOS_SIN_VOZ.Proveedor.RS = @proveedor AND
                      LOS_SIN_VOZ.Compra_Oferta.factura IS NULL AND 
	                  LOS_SIN_VOZ.Compra_Oferta.fecha_Compra <= @end AND
	                  LOS_SIN_VOZ.Compra_Oferta.fecha_Compra >= @start

                GROUP BY LOS_SIN_VOZ.Proveedor.id
                ",
                Program.con
            );
            SqlParameter startd = new SqlParameter();
            SqlParameter endd = new SqlParameter();
            SqlParameter prov = new SqlParameter();
            startd.ParameterName = "@start";
            endd.ParameterName = "@end";
            prov.ParameterName = "@proveedor";
            startd.Value = start;
            endd.Value = end;
            prov.Value = chosenProveedor;

            montoFacturaQuery.Parameters.Add(startd);
            montoFacturaQuery.Parameters.Add(endd);
            montoFacturaQuery.Parameters.Add(prov);

            object dbresp = montoFacturaQuery.ExecuteScalar();
            if (dbresp == null)
            {
                montoTotalLabel.Text = "Monto total = 0";
            }
            else {
                montoTotalLabel.Text = "Monto total = " + dbresp.ToString();
                montoTotalLabel.Enabled = true;
                facturarButton.Enabled = true; // enable button now that there's a list of compras
            }
        }

        public void PopulateTableWithQuery(SqlCommand query)
        {
            var adapter = new SqlDataAdapter(query);
            var table = new DataTable();
            adapter.Fill(table);
            TablaFacturacion.DataSource = table;
            TablaFacturacion.AutoResizeColumns();

        }

        private string getProveedorIdFromName(string proveedor) {
            SqlCommand query = new SqlCommand("SELECT id FROM LOS_SIN_VOZ.Proveedor WHERE RS='"+proveedor+"'", Program.con);
            object a = query.ExecuteScalar();
            return a.ToString();
        }

        private void facturarButton_Click(object sender, EventArgs e)
        {
            // Ejecutar SP de facturacion pasandole parametros, proveedor id, fecha de facturacion, periodo desde y hasta
            string prov_id = getProveedorIdFromName(chosenProveedor);
            SqlCommand facturarQuery = new SqlCommand("LOS_SIN_VOZ.sp_facturar", Program.con);
            facturarQuery.CommandType = CommandType.StoredProcedure;
            facturarQuery.Parameters.AddWithValue("@prov", prov_id);
            facturarQuery.Parameters.AddWithValue("@fecha_facturacion", fechaActual);
            facturarQuery.Parameters.AddWithValue("@desde", desdePicker.Value);
            facturarQuery.Parameters.AddWithValue("@hasta", hastaPicker.Value);
            string msg = "Desea confirmar la facturacíon para el proveedor " + chosenProveedor+"?";
            DialogResult confirm = MessageBox.Show(msg, "Confirmar operación", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes) {
                try
                {
                    facturarQuery.ExecuteNonQuery();
                    MessageBox.Show("Facturacion exitosa", "Exito");
                    facturarButton.Enabled = false;
                }
                catch (Exception _) {
                    MessageBox.Show("Algo salió mal. Reintente", "Error");
                }
            }
        }


    }
}
