using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas.ConsumoOferta
{
    public partial class Confirm : Form
    {
        string cliente,compra;
        string fecha;
        public Confirm(DataGridViewCellCollection cells, string cliente_,string compra_)
        {
            cliente = cliente_;
            compra = compra_;

            var date = DateTime.Parse(ConfigurationManager.AppSettings["fecha"]);
            fecha = date.Month.ToString() + "/" + date.Day.ToString() + "/" + date.Year.ToString();

            InitializeComponent();
            label1.Text = "¿canjear " + cells[2].Value.ToString() + "a"+ cells[0].Value.ToString() + "?";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //moverlo a un procedure seria mejor porque ahorra el envio pero ni ganas
            util.execCommand(@"
                BEGIN TRANSACTION
                INSERT INTO Cupon VALUES (" + cliente + ",'" + fecha + @"')
                UPDATE Compra_Oferta SET fueCanjeado=1 WHERE id="+compra+@"
                COMMIT TRANSACTION");
            Close();
        }
    }
}
