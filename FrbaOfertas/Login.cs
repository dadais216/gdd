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

namespace FrbaOfertas
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = util.tableQuery( "SELECT contraseña,habilitado,rol FROM Usuario WHERE nombre = \'"+nombre.Text+"\'");

            //@todo cuando se encripte la contraseña hay que encriptar el input del usuario para poder comparar

            if (user.Rows.Count == 0)
            {
                info.Text = "usuario no existe";
            }
            else
            {
                if ((bool)user.Rows[0].ItemArray[1])
                {
                    if ((string)user.Rows[0].ItemArray[0] == contraseña.Text)
                    {
                        util.execCommand("UPDATE Usuario SET " +
                                                "fallosLogin = 0 " +
                                         "WHERE nombre = \'" + nombre.Text + "\'");

                        //medio choto esto, por ahi es mejor tener un rol vacio
                        string rolIdOrNull = user.Rows[0].ItemArray[2].ToString();
                        if (rolIdOrNull == "") rolIdOrNull = "null";

                        var main=new Main(rolIdOrNull);
                        Hide();
                        main.Closed += (s, args) => this.Close();
                        main.Show();
                    }
                    else
                    {
                        info.Text = "contraseña incorrecta";

                        util.execCommand("UPDATE Usuario SET " +
                                                "fallosLogin = fallosLogin + 1, "+
                                                "habilitado = (CASE WHEN fallosLogin > 2 THEN 0 ELSE habilitado END) " +
                                         "WHERE nombre = \'" + nombre.Text + "\'");
                        //fallosLogin > 2 para que sean 3 intentos y despues falle. La comparacion toma el valor desactualizado
                        //intento 1 fallosLogin 1 0>2=>false
                        //intento 2 fallosLogin 2 1>2=>false
                        //intento 3 fallosLogin 3 2>2=>false
                        //intento 4 fallosLogin 4 3>2=>true


                    }
                }
                else
                {
                    info.Text = "usuario deshabilitado";
                }
            }





        }
    }
}
