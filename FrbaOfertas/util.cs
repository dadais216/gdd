using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FrbaOfertas
{
    static public class util
    {
        static public DataTable tableQuery(string s, params string[] parameters)
        {
            var command = new SqlCommand(s, Program.con);
            for (int i = 0; i < parameters.Length; i += 2)
            {
                command.Parameters.AddWithValue(parameters[i], parameters[i + 1]);
            }

            SqlDataAdapter adp = new SqlDataAdapter(command);
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
        //en vez de tomar pares (@val,val) podria tomar solo los valores y asignarlos a los @ segun el orden
        //que esten en el query original. No lo hago porque seguro que la manipulacion de strings aca es horrible,
        //y manejar queries que nombren un mismo @ mas de una vez seria incomodo si no se sabe como esta implementado.
        //igual es algo que me parece mejor, aunque en este tp no creo que cambie mucho

        static public byte[] hashString(string s)
        {
            return new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(s));
        } 
    }
}
