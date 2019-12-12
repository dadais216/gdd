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

            var table = util.tableQuery("SELECT habilitado FROM tp.Usuario WHERE proveedor = " + userId);

            habilitado = (bool)table.Rows[0].ItemArray[0];

            button1.Text = habilitado ? "deshabilitar" : "habilitar";
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            util.execCommand("UPDATE tp.Proveedor SET RS=@RS,dom=@di,ciudad=@ci,telefono=@te,CUIT=@CU,mail=@ma, " +
                                                            "codigoPostal=@co,rubro=@ru " +
                                                            "WHERE id=@id",
                                                            "@RS", razonSocial.Text,
                                                            "@di", direccion.Text,
                                                            "@ci", ciudad.Text,
                                                            "@te", telefono.Text,
                                                            "@cu", CUIT.Text,
                                                            "@ma", mail.Text,
                                                            "@co", codigoPostal.Text,
                                                            "@ru", rubro.Text,
                                                            "@no", contacto.Text,
                                                            "@id", userId);

            //hacer una modificacion setea los nulls de mail y codigoPostal a "" y 0, arreglarlo implica no usar los parameters
            //estos (que son una cagada). No sé si vale la pena molestarse por eso igual


            if (contraseña.Text != "")
            {
                CambioContraseña.cambiarContraseña(contraseña.Text,userId);
            }

            parent.doQuery();
            Close();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            util.execCommand("UPDATE Usuario SET habilitado = " + (habilitado ? "0 " : "1 ") + "WHERE proveedor = " + userId);

            Close();
        }
    }
}
