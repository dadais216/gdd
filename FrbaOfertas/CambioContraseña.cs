using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                if ((string)table.Rows[0].ItemArray[0] == anterior.Text)
                {
                    util.execCommand("UPDATE Usuario SET contraseña = \'" + nueva.Text + "\' WHERE id= " + userId);
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
    }
}
