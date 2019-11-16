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

        Dictionary<string, Form> funcionalidad=new Dictionary<string, Form>();
        public Main()
        {
            //un switch seria mejor porque es estatico y delega la creacion
            funcionalidad.Add("abm cliente", new AbmCliente.Form1());
            funcionalidad.Add("abm proveedor", new AbmProveedor.Form1());
            funcionalidad.Add("abm rol", new AbmRol.Form1());
            funcionalidad.Add("compra oferta", new ComprarOferta.Form1());
            funcionalidad.Add("carga credito", new CragaCredito.Form1());
            funcionalidad.Add("confeccion y publicacion de oferta", new CrearOferta.Form1());
            funcionalidad.Add("facturacion a proveedor", new Facturar.Form1());
            funcionalidad.Add("listado estadistico", new ListadoEstadistico.Form1());
            funcionalidad.Add("consumo oferta", null);

            InitializeComponent();

            //cuando el usuario se logee se hace un join y se consigue el rol
            string query = "SELECT name FROM "+
                           "Funcionalidad f JOIN RolxFuncionalidad rf ON f.id = rf.funcionalidad "+
                           "WHERE rf.rol = 4";

            SqlDataAdapter adp = new SqlDataAdapter(query, Program.con);
            DataTable table = new DataTable();
            adp.Fill(table);

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
            funcionalidad[b.Text].Show();
        }

    }
}
