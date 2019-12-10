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

namespace FrbaOfertas
{
    public partial class Main : Form
    {
        string userId;
        string rolId; //tecnicamente puede ser null pero se usa desde funcionalidades, lo que te asegura que no es null
        public Main(string userId_,string rolIdOrNull_)
        {
            userId = userId_;
            rolId = rolIdOrNull_;
            InitializeComponent();

            var table = util.tableQuery("SELECT nombre FROM " +
                           "Funcionalidad f JOIN RolxFuncionalidad rf ON f.id = rf.funcionalidad " +
                           "WHERE rf.rol = " + rolIdOrNull_);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Button button = new Button();

                button.Width = 120;
                button.Height = 40;

                button.Text = table.Rows[i].ItemArray[0].ToString();
                button.Click += handleClick;

                button.Left = (i/5)*160+40;
                button.Top = (i%5)*60+40;
                Controls.Add(button);
            }
        }
        public void handleClick(object sender,EventArgs args)
        {
            Button b = (Button)sender;

            //antes usaba un diccionario, pero un switch es mejor porque es estatico,
            //ademas el diccionario era <string,Form>, por lo que necesitaba alocar todas
            //las forms en su inicializacion y tenia problemas si se cerraba una y se queria volver a abrir
            switch (b.Text)
            {
                case "abm cliente": new AbmCliente.Form1().Show();break;
                case "abm proveedor": new AbmProveedor.Form1().Show();break;
                case "abm rol": new AbmRol.Form1().Show(); break;
                case "compra oferta": new ComprarOferta.Form1(rolId,userId).Show(); break;
                case "carga credito": new CragaCredito.Form1().Show(); break;
                case "confeccion y publicacion de oferta": new CrearOferta.Form1().Show(); break;
                case "facturacion a proveedor": new Facturar.ListarFacturasProveedor().Show(); break;
                case "listado estadistico": new ListadoEstadistico.ListadoView().Show(); break;
                case "consumo oferta": new ConsumoOferta.Form1(userId).Show(); break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new CambioContraseña(userId).Show();   
        }
    }
}
