using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace FrbaOfertas
{
        static class Program
        {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// 
        private static string server = ConfigurationManager.AppSettings["server"].ToString();
        private static string db = ConfigurationManager.AppSettings["db"].ToString();
        private static string user = ConfigurationManager.AppSettings["user"].ToString();
        private static string password = ConfigurationManager.AppSettings["password"].ToString();

        private static string connString = "Data Source= {0}\\SQLSERVER2012;" +
					                       "Initial Catalog = {1};" +
					                       "User ID = {2};" +
					                       "Password = {3};";
        private static string connectionStr = string.Format(connString, server, db, user, password);
        public static System.Data.SqlClient.SqlConnection con = new SqlConnection(connectionStr);
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            con.Open(); // a veces falla la conexion no sé por que
#if false 
            Application.Run(new Login());
#else //salto el login porque rompe las bolas
#if true
            Application.Run(new Main("1", "4"));
#else
            util.execCommand("UPDATE Cliente SET saldo=5000 WHERE id=1");
            Application.Run(new Main("1","1"));
#endif
#endif

        }
    }
}
