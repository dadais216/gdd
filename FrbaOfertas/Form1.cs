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
    public partial class Form1 : Form
    {
        string userId;
        int[,] clienteCompraIds;
        public Form1(string userId_)
        {
            userId = userId_;
            InitializeComponent();


            doQuery();
        }

        private void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            //no se necesita poteger contra un rol de usuario malo aca porque el query no trae nada para no proveedores

            var confirm = new Confirm(dataGridView1.Rows[e.RowIndex].Cells, clienteCompraIds[e.RowIndex,0].ToString(), clienteCompraIds[e.RowIndex, 1].ToString());
            confirm.Closed += (s, args) => doQuery();
            confirm.Show();
        }

        private void doQuery()
        {
            var table = util.tableQuery(@"
                    SELECT CONCAT(nombre,' ',apellido),oferta,descripcion,fecha_Compra, c.id,co.id
                    FROM Compra_Oferta co JOIN Cliente c ON c.id=co.cliente JOIN Oferta o ON o.codigo=co.oferta
                    WHERE co.fueCanjeado=0 AND o.proveedor=(SELECT proveedor FROM Usuario WHERE id="+userId+")");
            clienteCompraIds = new int[table.Rows.Count,2];
            //creo que los puse en el orden correcto, quiero que los pares esten juntos en memoria

            for (int i = 0; i < table.Rows.Count; i++)
            {
                clienteCompraIds[i,0] = (int)table.Rows[i].ItemArray[4];
                clienteCompraIds[i, 1] = (int)table.Rows[i].ItemArray[5];
            }

            table.Columns.RemoveAt(5);
            table.Columns.RemoveAt(4);

            dataGridView1.DataSource = table;
        }

    }
}
