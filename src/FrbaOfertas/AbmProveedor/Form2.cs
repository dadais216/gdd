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
    public partial class Form2 : Form
    {
        Form1 parent;
        string userId;
        bool habilitado;
        public Form2(Form1 form1, string userId_, DataGridViewCellCollection cells)
        {
            InitializeComponent();

            parent = form1;

            //RS,dom,ciudad,telefono,CUIT,mail,codigoPostal,rubro

            razonSocial.Text = cells[0].Value.ToString();
            direccion.Text = cells[1].Value.ToString();
            ciudad.Text = cells[2].Value.ToString();
            telefono.Text = cells[3].Value.ToString();
            CUIT.Text = cells[4].Value.ToString();
            mail.Text = cells[5].Value.ToString();
            codigoPostal.Text = cells[6].Value.ToString();
            rubro.Text = cells[7].Value.ToString();
            contacto.Text = cells[8].Value.ToString();

            userId = userId_;

            var table = util.tableQuery("SELECT habilitado FROM LOS_SIN_VOZ.Usuario WHERE proveedor = " + userId);

            habilitado = (bool)table.Rows[0].ItemArray[0];

            button1.Text = habilitado ? "deshabilitar" : "habilitar";
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            var table = util.tableQuery("SELECT id FROM LOS_SIN_VOZ.Rubro WHERE nombre = @no","@no",rubro.Text);

            string rubroId;
            if (table.Rows.Count == 0)
            {
                util.execCommand("INSERT LOS_SIN_VOZ.Rubro VALUES (@no)", "@no",rubro.Text);
                rubroId = util.getVal("SELECT @@IDENTITY").ToString();
            }
            else
            {
                rubroId = table.Rows[0].ItemArray[0].ToString();
            }
            try
            {
                var command = new SqlCommand("UPDATE LOS_SIN_VOZ.Proveedor SET RS=@RS,dom=@di,ciudad=@ci," +
                                                "telefono=@te,CUIT=@CU,mail=@ma, codigoPostal=@co,rubro=@ru " +
                                                "WHERE id=@id", Program.con);

                command.Parameters.AddWithValue("@RS", razonSocial.Text);
                command.Parameters.AddWithValue("@di", direccion.Text);
                command.Parameters.AddWithValue("@ci", ciudad.Text);
                command.Parameters.AddWithValue("@te", telefono.Text);
                command.Parameters.AddWithValue("@cu", CUIT.Text);
                command.Parameters.AddWithValue("@ma", mail.Text==""?DBNull.Value:(object)mail.Text);
                command.Parameters.AddWithValue("@co", codigoPostal.Text == "" ? DBNull.Value : (object)codigoPostal.Text);
                command.Parameters.AddWithValue("@ru", rubroId);
                command.Parameters.AddWithValue("@no", contacto.Text);
                command.Parameters.AddWithValue("@id", userId);

                command.ExecuteNonQuery();

            }
            catch (SqlException er)
            {
                if (er.Number == 2627)
                {
                    new ErrorWindow("un usuario con esos datos ya existe").Show();
                }
                else
                {
                    new ErrorWindow("datos faltantes o mal ingresados").Show(); //tira el mismo error para datos vacios y malos sql
                }
            }

            if (contraseña.Text != "")
            {
                CambioContraseña.cambiarContraseña(contraseña.Text,userId);
            }

            parent.doQuery();
            Close();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            util.execCommand("UPDATE LOS_SIN_VOZ.Usuario SET habilitado = " + (habilitado ? "0 " : "1 ") + "WHERE proveedor = " + userId);

            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            razonSocial.Text = "";
            direccion.Text = "";
            ciudad.Text = "";
            telefono.Text = "";
            CUIT.Text = "";
            mail.Text = "";
            codigoPostal.Text = "";
            rubro.Text = "";
            contacto.Text = "";
            contraseña.Text = "";
        }
    }
}
