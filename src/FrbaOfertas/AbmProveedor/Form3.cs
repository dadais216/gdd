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
        public bool finished = false;
        private void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                var table = util.tableQuery("SELECT id FROM LOS_SIN_VOZ.Rubro WHERE nombre = @no", "@no", rubro.Text);
                string rubroId;
                if (table.Rows.Count == 0)
                {
                    util.execCommand("INSERT LOS_SIN_VOZ.Rubro VALUES (@no)", "@no", rubro.Text);
                    rubroId = util.getVal("SELECT @@IDENTITY").ToString();
                }
                else
                {
                    rubroId = table.Rows[0].ItemArray[0].ToString();
                }
                util.execCommand("INSERT INTO LOS_SIN_VOZ.Proveedor (RS,dom,ciudad,telefono,CUIT,mail,codigoPostal,rubro,contacto) " +
                                 "VALUES (@RS,@di,@ci,@te,@cu,@ma,@co,@ru,@no)",
                                                            "@RS", razonSocial.Text,
                                                            "@di", direccion.Text,
                                                            "@ci", ciudad.Text,
                                                            "@te", telefono.Text,
                                                            "@cu", CUIT.Text,
                                                            "@ma", mail.Text,
                                                            "@co", codigoPostal.Text,
                                                            "@ru", rubroId,
                                                            "@no", contacto.Text
                                                            );
                finished = true;
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
        }
    }
}
