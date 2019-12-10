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
        public Confirm(DataGridViewCellCollection cells,string userId_,string fecha_)
        {
            userId = userId_;
            fecha = fecha_;
            InitializeComponent();
            label1.Text = "¿comprar " + cells[0].Value.ToString()+ " ?";
            codigo = cells[4].Value.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //moverlo a un procedure seria mejor porque ahorra el envio pero ni ganas
            util.execCommand(@"
            BEGIN TRANSACTION
            INSERT INTO Compra_Oferta VALUES ("+userId+",'"+codigo+"',"+fecha+@",0)
            UPDATE Oferta SET cantidad=cantidad-1 WHERE codigo='"+codigo+@"'
            COMMIT TRANSACTION
            ");
            var form=new codigoCompra(util.getVal("SELECT @@IDENTITY").ToString());
            Hide();
            form.Closed += (s, a) => Close();
            form.Show();
        }
    }
}
