using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas.Facturar
{
    public partial class ListarFacturasProveedor : Form
    {
        /*
         Esta funcionalidad permite a un administrativo facturar a un proveedor
         todas las ofertas compradas por los clientes.
         Para ello ingresará el período de facturación por intervalos de fecha,
         se deberá seleccionar el proveedor y a continuación 
         se listaran todos las ofertas que fueron adquiridas por los clientes.
         Una vez que se tiene dicho listado, se informará el 
         importe de la factura y el número correspondiente de la misma.
         De más esta decir que este proceso debe quedar registrado en la base datos. 
        */
        public ListarFacturasProveedor()
        {
            InitializeComponent();
        }

    }
}
