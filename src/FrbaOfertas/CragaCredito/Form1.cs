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

namespace FrbaOfertas.CragaCredito
{
    public partial class Form1 : Form
    {
        DateTime fecha = Convert.ToDateTime(ConfigurationManager.AppSettings["fecha"]);
        string tipoPagoId = null;
        string clienteId;

        public Form1(string userId)
        {
            clienteId = userId;
            InitializeComponent();
        }

        private void ButtonCargar_Click(object sender, EventArgs e)
        {
            var table = util.tableQuery("SELECT id FROM LOS_SIN_VOZ.Tipo_Pago WHERE descripcion = @de", "@de", tipoPago.Text);
            tipoPagoId = table.Rows[0].ItemArray[0].ToString();

            if (this.sonCamposValidos())
            {
                if (tipoPagoId == "1")
                {
                    numeroTarjeta.Hide();
                    vencimientoTarjeta.Hide();
                    label3.Hide();
                    label4.Hide();

                    cargarCmd();
                    this.Close();
                }
                else
                {
                    if (sonCamposValidosTarjeta())
                    {
                        cargarCmd();
                        this.Close();
                    }
                }
            }
        }

        private bool sonCamposValidos()
        {
            bool sonNumericos = true;

            try
            {
                Convert.ToDouble(monto.Text);
            }
            catch (Exception e)
            {
                sonNumericos = false;
            }


            if (monto.Text == "")
            {
                MessageBox.Show("No puede haber campos vacios");
                return false;
            }
            if (tipoPago.SelectedItem.ToString().Equals(""))
            {
                MessageBox.Show("Debe seleccionar un tipo de pago");
                return false;
            }
            if (sonNumericos && Convert.ToDouble(monto.Text) < 0)
            {
                MessageBox.Show("El monto a acreditar no puede ser negativo");
                return false;
            }

            if (!sonNumericos)
            {
                MessageBox.Show("Campos numericos invalidos");
                return false;
            }

            return true;
        }

        private bool sonCamposValidosTarjeta()
        {
            bool sonNumericos = true;

            try
            {
                Convert.ToInt32(numeroTarjeta.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("La cantidad de números de la tarjeta de credito sobrepasa a la cantidad esperada");
                sonNumericos = false;
            }

            if (numeroTarjeta.Text == "")
            {
                MessageBox.Show("No puede haber campos vacios");
                return false;
            }

            if (!sonNumericos)
            {
                MessageBox.Show("Campos numericos invalidos");
                return false;
            }

            DateTime fechaDelDia = DateTime.Parse(ConfigurationManager.AppSettings["fecha_dia"]);
            if (DateTime.Parse(vencimientoTarjeta.Text) < fechaDelDia)
            {
                MessageBox.Show("La tarjeta no puede estar vencida");
                return false;
            }

            return true;
        }

        private void cargarCmd()
        {
            var command = new SqlCommand("INSERT INTO LOS_SIN_VOZ.Carga (cliente, credito, fecha, tipo_Pago) " +
                                            "VALUES (@cli,@cre,@fe,@tp)", Program.con);

            command.Parameters.AddWithValue("@cli", clienteId);
            command.Parameters.AddWithValue("@cre", monto.Text);
            command.Parameters.AddWithValue("@fe", fecha);
            command.Parameters.AddWithValue("@tp", tipoPagoId);
            command.ExecuteNonQuery();
        }

        private void monto_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar que la tecla presionada no sea CTRL u otra tecla no numerica
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Solo se permiten numeros Enteros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
