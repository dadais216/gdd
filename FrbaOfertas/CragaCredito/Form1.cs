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

namespace FrbaOfertas.CragaCredito
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            var command = new SqlCommand("INSERT INTO Cliente " +
                "(Carga_Credito,Carga_Fecha,Tipo_Pago_Desc) " +
                "VALUES (@monto,@fechaCredito,@tipopago)", Program.con);
            command.Parameters.AddWithValue("@monto", monto.Text);
            //command.Parameters.AddWithValue("@fechaCredito", fechacredito.Text);
            //La fecha tiene que ser del archivo de configuracion de la aplicacion
            command.Parameters.AddWithValue("@tipopago", tipopago.Text);
            //el tipo de pago tiene que salir de la tabla 

            //para agregar el id de cliente habra que hacer un join?

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException er)
            {
                var newForm = new ErrorWindow();

                Console.WriteLine(er.Message + " >>>>>>>" + er.Number);
                if (er.Number == 2627)
                {
                    newForm.setText("Un credito con esos datos ya existe");
                }
                else
                {
                    newForm.setText("Datos faltantes o mal ingresados");
                }

                newForm.Show();
            }
            Close();
        }
    }
}
