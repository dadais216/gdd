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

namespace FrbaOfertas.AbmCliente
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
            dni.Text = cells[0].Value.ToString();
            nombre.Text = cells[1].Value.ToString();
            apellido.Text = cells[2].Value.ToString();
            direccion.Text = cells[3].Value.ToString();
            telefono.Text = cells[4].Value.ToString();
            mail.Text = cells[5].Value.ToString();
            fnac.Text = ((DateTime)cells[6].Value).ToString("d");
            ciudad.Text = cells[7].Value.ToString();


            userId = userId_;

            var table = util.tableQuery("SELECT habilitado FROM tp.Usuario WHERE cliente = " + userId);

            habilitado = (bool)table.Rows[0].ItemArray[0];

            button1.Text = habilitado ? "deshabilitar" : "habilitar";
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                util.execCommand("UPDATE tp.Cliente SET dni=@dn,nombre=@no,apellido=@ap,direccion=@di, " +
                                                                "telefono=@te,mail=@ma,ciudad=@ci, fecha_Nac=@fe " +
                                                                "WHERE id=@id",
                                                                "@no", nombre.Text,
                                                                "@ap", apellido.Text,
                                                                "@dn", dni.Text,
                                                                "@di", direccion.Text,
                                                                "@te", telefono.Text,
                                                                "@ma", mail.Text,
                                                                "@ci", ciudad.Text,
                                                                "@fe", util.flipDayMonth(fnac.Text),
                                                                "@id", userId);
            } catch (Exception _)
            {
                //se puso una letra en un campo de numero o una fecha absurda
                new ErrorWindow("Datos Mal Ingresados").Show();
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
            util.execCommand("UPDATE tp.Usuario SET habilitado = " + (habilitado ? "0 " : "1 ") + "WHERE cliente = " + userId);

            Close();
        }
    }
}
