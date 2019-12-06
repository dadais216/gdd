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
    public partial class CambioContraseña : Form
    {
        string userId;
        public CambioContraseña(string userId_)
        {
            userId = userId_;
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (nueva.Text != "")
            {
                var table = util.tableQuery("SELECT contraseña FROM Usuario WHERE id = " + userId);
                if (((byte[])table.Rows[0].ItemArray[0])
                        .SequenceEqual(
                                        util.hashString(anterior.Text)))
                {
                    cambiarContraseña(nueva.Text,userId);

                    Close();
                }
                else
                {
                    label2.Text = "contraseña anterior incorrecta";
                }
            }
            else
            {
                label2.Text = "la contraseña no puede estar vacia";
            }
        }

        static public void cambiarContraseña(string nueva,string id)
        {
            var command = new SqlCommand("UPDATE Usuario SET contraseña = HASHBYTES('SHA2_256',@nu) WHERE id= " + id, Program.con);
            command.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.VarChar, //c# pasa por default unicode, osea nvarchar
                ParameterName = "@nu",
                Value = nueva
            });
            command.ExecuteNonQuery();
        }
    }
}
