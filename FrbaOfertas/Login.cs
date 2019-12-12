using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
            var user = util.tableQuery("SELECT contraseña,habilitado,rol,id FROM tp.Usuario WHERE nombre = @no", 
                                        "@no",nombre.Text);

            if (user.Rows.Count == 0)
            {
                info.Text = "usuario no existe";
            }
            else
            {
                if ((bool)user.Rows[0].ItemArray[1])
                {
                    byte[] inputBytes= util.hashString(contraseña.Text);
                    byte[] passBytes = (byte[])user.Rows[0].ItemArray[0];

                    if (inputBytes.SequenceEqual(passBytes))
                    {
                        util.execCommand("UPDATE tp.Usuario SET " +
                                                "fallosLogin = 0 " +
                                         "WHERE nombre = @no", "@no", nombre.Text);

                        //medio choto esto, por ahi es mejor tener un rol vacio
                        string rolIdOrNull = user.Rows[0].ItemArray[2].ToString();
                        if (rolIdOrNull == "") rolIdOrNull = "null";

                        var main=new Main(user.Rows[0].ItemArray[3].ToString(), rolIdOrNull);
                        Hide();
                        main.Closed += (s, args) => Close();
                        main.Show();
                    }
                    else
                    {
                        info.Text = "contraseña incorrecta";

                        util.execCommand("UPDATE tp.Usuario SET " +
                                                "fallosLogin = fallosLogin + 1, "+
                                                "habilitado = (CASE WHEN fallosLogin > 2 THEN 0 ELSE habilitado END) " +
                                         "WHERE nombre = @no", "@no", nombre.Text);
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

        private void Button2_Click(object sender, EventArgs e)
        {
            var registro = new Registro(nombre.Text,contraseña.Text);

            Hide();
            registro.Closed += (s, args) => Show();
            registro.Show();
        }
    }
}
