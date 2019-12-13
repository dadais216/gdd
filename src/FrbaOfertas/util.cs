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

        static public string flipDayMonth(string s)
        {//probablemente haya una libreria que haya esto pero es una boludes ni ganas de ponerme a buscar
            var c=s.ToCharArray();
            char temp0 = c[0];
            char temp1 = c[1];

            c[0] = c[3];
            c[1] = c[4];
            c[3] = temp0;
            c[4] = temp1;

            return new string(c);
        }

        static public object getVal(string s, params string[] parameters)
        {
            return tableQuery(s,parameters).Rows[0].ItemArray[0];
        }
    }
}
