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
        string montoFactura;

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
                FROM Compra_Oferta
                JOIN Oferta ON Compra_Oferta.oferta = Oferta.codigo
                JOIN Proveedor ON Proveedor.id = Oferta.proveedor
                JOIN Cliente ON Cliente.id = Compra_Oferta.cliente
                WHERE Proveedor.RS = @proveedor AND 
                      Compra_Oferta.factura IS NULL AND
	                  Compra_Oferta.fecha_Compra < @endDate AND
	                  Compra_Oferta.fecha_Compra > @startDate
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
            facturarButton.Enabled = true; // enable button now that there's a list of compras

            // Calculate monto total
            SqlCommand montoFacturaQuery = new SqlCommand(
                @"
                SELECT  SUM(Oferta.precio)
                FROM Compra_Oferta
                JOIN Oferta ON Compra_Oferta.oferta = Oferta.codigo
                JOIN Proveedor ON Proveedor.id = Oferta.proveedor
                WHERE Proveedor.RS = @proveedor AND
                      Compra_Oferta.factura IS NULL AND 
	                  Compra_Oferta.fecha_Compra < @end AND
	                  Compra_Oferta.fecha_Compra > @start

                GROUP BY Proveedor.id
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

        private void facturarButton_Click(object sender, EventArgs e)
        {
            List<string> compra_ids = new List<string> {};
            foreach (DataGridViewRow compra in TablaFacturacion.Rows)
            {
                compra_ids.Add(compra.Cells["TICKET"].Value.ToString());
            }

            // Create Factura
            // Asociar compras a factura
            string msg = "Desea confirmar la facturacíon para el proveedor " + chosenProveedor+"?";
            DialogResult confirm = MessageBox.Show(msg, "Confirmar operación", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes) {
                crearFactura(compra_ids);
            }

            // Debug.WriteLine(compra_ids);
            // Crear row en factura
            // Obtener todos los ids de compra oferta
            // UPDATE Compra_Oferta SET factura = Factura.nro
        }

        private void crearFactura(List<string> compra_ids)
        {

            // Creo factura de proveedor
            SqlCommand insertarFactura = new SqlCommand(
                @"
                INSERT INTO Factura output INSERTED.nro
                VALUES (
                       GETDATE(),
                       (SELECT id FROM Proveedor WHERE RS=@proveedor)
                )
                ",
                Program.con
            );
            insertarFactura.Parameters.AddWithValue("@proveedor", chosenProveedor);
            int factura = Convert.ToInt32(insertarFactura.ExecuteScalar());
            SqlCommand updateCompras = buildComprasUpdateQuery(factura.ToString(), compra_ids);
            int affectedRows = updateCompras.ExecuteNonQuery();
            if (compra_ids.Count() == affectedRows) {
                MessageBox.Show("Facturacion exitosa", "Exito");
            }
            else {
                MessageBox.Show("Algo salió mal. Reintente", "Error");
            }
            facturarButton.Enabled = false;
            // clean datagrid view or disable facturar
        }

        private string buildComprasInClause(SqlCommand q,  List<string> compra_ids) 
        {
            StringBuilder in_clause = new StringBuilder();
            int i = 1;

            foreach (string compra_id in compra_ids)
            {
                string uniqueId = i.ToString();
                // Append parameter with unique id and comma
                in_clause.Append("@compraid" + uniqueId + ",");
                // Set value of created parameter
                q.Parameters.AddWithValue("@compraid" + uniqueId, compra_id);

                i++;
            }
            string result = in_clause.ToString();
            int lastCommaIndex = result.LastIndexOf(',');
            string trueResult = result.Remove(lastCommaIndex);

            return trueResult;
        }

        private SqlCommand buildComprasUpdateQuery(string factura, List<string> compras)
        {
            string parameterPrefix = "compra_id";
            string querystr = @"
                UPDATE Compra_Oferta 
                SET factura=@factura
                WHERE Compra_Oferta.id IN ({0})
                ";

            querystr = SqlWhereInParamBuilder.BuildWhereInClause(querystr, parameterPrefix, compras);

            SqlCommand sqlquery = new SqlCommand(querystr, Program.con);
            sqlquery.AddParamsToCommand(parameterPrefix, compras);
            sqlquery.Parameters.AddWithValue("@factura", factura);

            return sqlquery;
        }


    }
}
