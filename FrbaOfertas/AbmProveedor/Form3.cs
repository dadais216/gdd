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

namespace FrbaOfertas.AbmProveedor
{
    public partial class Form3 : Form
    {
        public Form3()
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
                util.execCommand("INSERT INTO Cliente " +
                                "(dni,nombre,apellido,direccion,telefono,mail,ciudad,saldo) " + //"Cli_Fecha_Nac=@fe+"
                                "VALUES (@dn,@no,@ap,@di,@te,@ma,@ci,@saldoInicial)",
                                "@no", nombre.Text,
                                "@ap", apellido.Text,
                                "@dn", dni.Text,
                                "@di", direccion.Text,
                                "@te", telefono.Text,
                                "@ma", mail.Text,
                                "@ci", ciudad.Text,
                                "@saldoInicial", 200.ToString());

                //pongo el valor inicial por codigo en vez de en la db con DEFAULT porque DEFAULT afectaria a usuarios viejos
                //que se estan migrando. Podria hacerse con defualt metiendo la regla despues de hacer la migracion, pero en el 
                //tp no podemos controlar que pasa antes y despues.

                //@TODO se podria registar la carga de bienvenida como una carga


                //@TODO el ToString hace mierda el formato de datetime, lo tengo que arreglar a mano?
                //command.Parameters.AddWithValue("@fe", textBox14.Text);
            }
            catch (SqlException er)
            {
                var newForm = new ErrorWindow();

                Console.WriteLine(er.Message + " >>>>>>>" + er.Number);
                if (er.Number == 2627)
                {
                    newForm.setText("un usuario con esos datos ya existe");
                }
                else
                {
                    newForm.setText("datos faltantes o mal ingresados"); //tira el mismo error para datos vacios y malos sql
                }

                newForm.Show();
            }
            Close();

        }
    }
}
