using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas
{
    static public class util
    {
        static public DataTable tableQuery(string s)
        {
            SqlDataAdapter adp = new SqlDataAdapter(s, Program.con);
            DataTable table = new DataTable();

            adp.Fill(table);
            return table;
        }
        static public void execCommand(string s, params string[] parameters)
        {
            var command = new SqlCommand(s, Program.con);
            for(int i = 0; i < parameters.Length; i += 2)
            {
                command.Parameters.AddWithValue(parameters[i],parameters[i+1]);
            }
            command.ExecuteNonQuery();
        }
    }
}
