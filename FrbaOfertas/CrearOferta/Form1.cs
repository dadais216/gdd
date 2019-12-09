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
        public Form1()
        {
            InitializeComponent();
        }

        private bool sonCamposValidos()
        {
            bool sonNumericos = true;

            try
            {
                Convert.ToInt32(precioNuevo.Text);
            }
            catch (Exception e)
            {
                sonNumericos = false;
            }

            try
            {
                Convert.ToDouble(precioAntiguo.Text);
            }
            catch (Exception e)
            {
                sonNumericos = false;
            }


            if (stock.Text == "" || precioAntiguo.Text == "" || precioNuevo.Text == "" || proveedor.Text == "" || descripcion.Text == "")
            {
                MessageBox.Show("No puede haber campos vacios");
                return false;
            }
            
            if (sonNumericos && Convert.ToDouble(precioAntiguo.Text) < 0 || sonNumericos && Convert.ToDouble(precioNuevo.Text) < 0)
            {
                MessageBox.Show("El monto a acreditar no puede ser negativo");
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

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
