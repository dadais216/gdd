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
        string clienteId;

        public Form1(string userId,string rolId)
        {
            if((string)util.getVal("SELECT nombre FROM LOS_SIN_VOZ.Rol WHERE id=" + rolId) != "cliente")
            {
                MessageBox.Show("funcionalidad solo disponible para clientes");
                Close();
                return;
            }

            clienteId = util.getVal("SELECT cliente FROM LOS_SIN_VOZ.Usuario WHERE id="+userId).ToString();
            InitializeComponent();
            numeroTarjeta.Hide();
            label3.Hide();
            Show();
        }

        private void ButtonCargar_Click(object sender, EventArgs e)
        {
            if (sonCamposValidos()&&
                tipoPago.Text=="Efectivo"||
                (tipoPago.Text=="Crédito"||tipoPago.Text=="Débito")&&sonCamposValidosTarjeta())
            {

                var command = new SqlCommand("INSERT INTO LOS_SIN_VOZ.Carga (cliente, credito, fecha, tipo_Pago,numeroTarjeta) " +
                                            "VALUES (@cli,@cre,@fe,@tp,@nu)", Program.con);

                command.Parameters.AddWithValue("@cli", clienteId);
                command.Parameters.AddWithValue("@cre", monto.Text);
                command.Parameters.AddWithValue("@fe", fecha);
                command.Parameters.AddWithValue("@tp", util.getVal("SELECT id FROM LOS_SIN_VOZ.Tipo_Pago WHERE descripcion = '" + tipoPago.Text+"'").ToString());
                command.Parameters.AddWithValue("@nu", tipoPago.Text=="Efectivo"?DBNull.Value:(object)numeroTarjeta.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("carga exitosa");
                Close();
            }
        }

        private bool sonCamposValidos()
        {
            if (monto.Text == "")
            {
                MessageBox.Show("No puede haber campos vacios");
                return false;
            }
            try
            {
                if (Convert.ToDouble(monto.Text) < 0)
                {
                    MessageBox.Show("El monto a acreditar no puede ser negativo");
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Campos numericos invalidos");
                return false;
            }

            if (tipoPago.SelectedItem==null)
            {
                MessageBox.Show("Debe seleccionar un tipo de pago");
                return false;
            }

            return true;
        }

        private bool sonCamposValidosTarjeta()
        {
            if (numeroTarjeta.Text == "")
            {
                MessageBox.Show("No puede haber campos vacios");
                return false;
            }
            try
            {
                Convert.ToInt64(numeroTarjeta.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("números de la tarjeta de credito invalidos");
                return false;
            }
            return true;
        }

        private void TipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combobox = (ComboBox)sender;//ahi tenes tu type system
            if (combobox.SelectedIndex != 0)
            {
                numeroTarjeta.Show();
                label3.Show();
            }
            else
            {
                numeroTarjeta.Hide();
                label3.Hide();
            }
        }
    }
}
