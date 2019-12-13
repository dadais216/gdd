using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas.CrearOferta
{
    public partial class Form1 : Form
    {
        DateTime fecha = Convert.ToDateTime(ConfigurationManager.AppSettings["fecha"]);
        string proveedorId=null;

        public Form1(string userId,string rolId)
        {
            InitializeComponent();

            if (util.getVal("SELECT id FROM LOS_SIN_VOZ.Rol WHERE nombre = 'Proveedor'").ToString() == rolId)
            {
                lblProveedor.Hide();
                CUIT.Hide();

                proveedorId = util.getVal("SELECT Proveedor FROM LOS_SIN_VOZ.Usuario WHERE id=" + userId).ToString();
            }
            calendarioPublicacion.MinDate = fecha;
            calendarioVencimiento.MinDate = fecha;
        }

        private bool sonCamposValidos()
        {
            if (stock.Text == "" || precioListado.Text == "" || precioOferta.Text == "" 
                || descripcion.Text == "" || proveedorId==null&&CUIT.Text=="")
            {
                MessageBox.Show("No puede haber campos vacios");
                return false;
            }
            try
            {
                double oferta= Convert.ToDouble(precioOferta.Text);
                double listado= Convert.ToDouble(precioListado.Text);
                if (oferta < 0 || listado < 0 || Convert.ToDouble(stock.Text)<0)
                    throw new Exception();
                if (oferta > listado)
                {
                    MessageBox.Show("No es una oferta");
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Campos numericos invalidos");
                return false;
            }
            if (DateTime.Parse(calendarioVencimiento.Text) < DateTime.Parse(calendarioPublicacion.Text))
            {
                MessageBox.Show("La oferta no puede estar vencida");
                return false;
            }
            return true;
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            if (this.sonCamposValidos())
            {
                if (proveedorId == null)
                {
                    var table = util.tableQuery("SELECT id FROM LOS_SIN_VOZ.Proveedor WHERE CUIT= @cu",
                                            "@cu",CUIT.Text);
                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("no hay proveedor con ese CUIT");
                        return;
                    }
                    proveedorId = table.Rows[0].ItemArray[0].ToString();
                }

                char[] codigo = new char[16];
                string codigoStr;
                while (true)
                {
                    var random = new Random();
                    for (int i = 0; i < 16; i++)
                    {
                        switch (random.Next(0, 3))
                        {
                        case 0: codigo[i] = (char)random.Next(48, 58); break;
                        case 1: codigo[i] = (char)random.Next(65, 91); break;
                        case 2: codigo[i] = (char)random.Next(97, 123); break;
                        }
                    }

                    codigoStr = new String(codigo);


                    if (util.tableQuery("SELECT 1 FROM LOS_SIN_VOZ.Oferta WHERE codigo = '" + codigoStr + "'").Rows.Count == 0)
                        //improbable que haya colisiones pero no esta de mas probar.
                        break;

                }

                var command = new SqlCommand("INSERT INTO LOS_SIN_VOZ.Oferta (codigo,descripcion, cantidad, fecha, fecha_Venc, " +
                                            "precio, precio_Ficticio, proveedor) " +
                                             "VALUES (@co,@de,@st,@fp,@fv,@po,@pl,@pr)", Program.con);


                command.Parameters.AddWithValue("@co", codigoStr);
                command.Parameters.AddWithValue("@de", descripcion.Text);
                command.Parameters.AddWithValue("@st", stock.Text);
                command.Parameters.AddWithValue("@fp", calendarioPublicacion.Value);
                command.Parameters.AddWithValue("@fv", calendarioVencimiento.Value);
                command.Parameters.AddWithValue("@po", precioOferta.Text);
                command.Parameters.AddWithValue("@pl", precioListado.Text);
                command.Parameters.AddWithValue("@pr", proveedorId);

                command.ExecuteNonQuery();

                MessageBox.Show("Publicacion exitosa");



                this.Close();
            }
        }
    }
}
