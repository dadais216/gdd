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

namespace FrbaOfertas.CrearOferta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            //la validacion de no duplicados se hace con una constain unique en la db.
            //segun lo que lei en internet hacer un query y verificar por codigo es mas rapido
            //que responder a una excepcion de sql server, pero hacerlo es una solucion mas simple
            //y este no es un caso comun que requiera eficiencia.

            try
            {
                util.execCommand("INSERT INTO Oferta " +
                                "(descripcion,cantidad,fecha_Venc,precio,precio_Ficticio,proveedor) " + 
                                "VALUES (@de,@cd,@fv,@pl,@po,@pr)",
                                "@de", desc.Text,
                                "@fv", fechavenc.Text,
                                "@po", preciooferta.Text,
                                "@pl", preciolista.Text,
                                "@cd", cantidaddisp.Text);

                //para el proveedor se hara un join?
            }
            catch (SqlException er)
            {
                var newForm = new ErrorWindow();

                Console.WriteLine(er.Message + " >>>>>>>" + er.Number);
                if (er.Number == 2627)
                {
                    newForm.setText("Una oferta con esos datos ya existe");
                }
                else
                {
                    newForm.setText("Datos faltantes o mal ingresados"); //tira el mismo error para datos vacios y malos sql
                }

                newForm.Show();
            }
            Close();

        }
    }
}
