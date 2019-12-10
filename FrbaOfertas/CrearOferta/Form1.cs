using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas.CrearOferta
{
    public partial class Form1 : Form
    {
        public Dictionary<string, string> datosProveedorSeleccionado = new Dictionary<string, string>();
        DateTime fechaConfig = Convert.ToDateTime(ConfigurationManager.AppSettings["fecha"]);
        private bool haySeleccionado = false;

        public Form1()
        {
            InitializeComponent();
            if (true/*rol de usuario > 2 , osea proveedor o administrador (el true lo puse para que no joda con el error)*/)
            {
                btnBuscar.Hide();
                proveedor.ReadOnly = true;
                /*proveedor.Text = /*el id del proveedor;*/
                /*datosProveedorSeleccionado = /*datos del proveedor;*/
            }
            calendarioPublicacion.MinDate = fechaConfig;
            calendarioVencimiento.MinDate = fechaConfig;
        }

        private bool sonCamposValidos()
        {
            bool sonNumericos = true;

            try
            {
                Convert.ToDouble(precioOferta.Text);
            }
            catch (Exception e)
            {
                sonNumericos = false;
            }

            try
            {
                Convert.ToDouble(precioListado.Text);
            }
            catch (Exception e)
            {
                sonNumericos = false;
            }


            if (stock.Text == "" || precioListado.Text == "" || precioOferta.Text == "" || proveedor.Text == "" || descripcion.Text == "")
            {
                MessageBox.Show("No puede haber campos vacios");
                return false;
            }
            
            if (sonNumericos && Convert.ToDouble(precioListado.Text) < 0 || sonNumericos && Convert.ToDouble(precioOferta.Text) < 0)
            {
                MessageBox.Show("Hay precios de oferta negativos");
                return false;
            }

            if (!sonNumericos)
            {
                MessageBox.Show("Campos numericos invalidos");
                return false;
            }

            DateTime fechaDelDia = DateTime.Parse(ConfigurationManager.AppSettings["fecha_dia"]);
            if (DateTime.Parse(calendarioVencimiento.Text) < DateTime.Parse(calendarioPublicacion.Text))
            {
                MessageBox.Show("La oferta no puede estar vencida");
                return false;
            }

            return true;
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            if (this.sonCamposValidos())
            {
                util.execCommand("INSERT INTO Oferta (descripcion, cantidad, fecha, fecha_Venc, " +
                                "precio, precio_Ficticio, proveedor) " +
                                "VALUES (@de,@st,@fe,@fv,@pr,@pf,@pr)",
                                
                                "@de", descripcion.Text,
                                "@st", stock.Text,
                                "@fe", calendarioPublicacion.Text,
                                "@fv", calendarioVencimiento.Text,
                                "@pr", precioListado.Text,
                                "@pf", precioOferta.Text,
                                "@pr", proveedor.Text
                                );
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            /*Cursor = Cursors.WaitCursor;
            using (BuscarProveedor ventanaBusqueda = new BuscarProveedor())
            {
                if (ventanaBusqueda.ShowDialog() == DialogResult.OK)
                {
                    this.datosProveedorSeleccionado = ventanaCreacion.datosProveedor;
                    proveedor.Text = datosProveedorSeleccionado["razón social"].ToString();
                    haySeleccionado = true;
                }
            }
            Cursor = Cursors.Default;*/
            //TODO: Hacer una vista para seleccionar el proveedor
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
