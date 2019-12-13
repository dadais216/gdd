using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas.ComprarOferta
{
    public partial class Confirm : Form
    {
        string codigo;
        string userId;
        string fecha;
        string precio;
        public Confirm(DataGridViewCellCollection cells,string userId_,string fecha_)
        {
            userId = userId_;
            fecha = fecha_;
            InitializeComponent();
            label1.Text = "¿comprar " + cells[0].Value.ToString()+ " ?";
            codigo = cells[4].Value.ToString();
            precio = cells[1].Value.ToString().Replace(',','.');
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //moverlo a un procedure seria mejor porque ahorra el envio pero ni ganas
            util.execCommand(@"
            BEGIN TRANSACTION
            INSERT INTO LOS_SIN_VOZ.Compra_Oferta (cliente,oferta,fecha_Compra) VALUES (" + userId + ",'" + codigo + "'," + fecha + @")
            UPDATE LOS_SIN_VOZ.Oferta SET cantidad=cantidad-1 WHERE codigo='" + codigo + @"'
            UPDATE LOS_SIN_VOZ.Cliente SET saldo = saldo - " + precio + @"
            COMMIT TRANSACTION
            ");
            var form=new codigoCompra(util.getVal("SELECT @@IDENTITY").ToString());
            Hide();
            form.Closed += (s, a) => Close();
            form.Show();
        }
    }
}
