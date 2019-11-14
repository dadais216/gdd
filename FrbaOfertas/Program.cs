using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas
{
    class ConexionSQL
    {
        private static SqlConnection conexion;
        static string connectionString = "Data Source= localhost\\SQLSERVER2012;" +
                                 "Initial Catalog = GD2C2019; " +
                                 "User ID = gdCupon2019; Password = gd2019;";
        internal static SqlConnection GetConexion()
        {
            /*if (conexion == null)
            {*/
            
            conexion = new SqlConnection(connectionString);
            return conexion;

            /*}
            return conexion;*/
        }
    }

        static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>


        static string connectionString = "Data Source= localhost\\SQLSERVER2012;" +
                                 "Initial Catalog = GD2C2019; " +
                                 "User ID = gdCupon2019; Password = gd2019;";
        public static System.Data.SqlClient.SqlConnection con = new SqlConnection(connectionString);

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           /* SqlConnection conexion = ConexionSQL.GetConexion();*/
            con.Open();

            Application.Run(new AbmCliente.Form1());
        }
    }
}
