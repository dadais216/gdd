﻿using System;
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
    public partial class Registro : Form
    {
        public Registro(string n,string c)
        {
            InitializeComponent();

            nombre.Text = n;
            contraseña.Text = c;
        }

        bool verificarUsuarioContraseñaValido()
        {
            if (contraseña.Text.Length == 0 || nombre.Text.Length == 0)
            {
                info.Text = "Insertar usuario y contraseña antes de seguir";
                return false;
            }
            if (util.tableQuery("SELECT 1 FROM tp.Usuario WHERE nombre = @no", "@no", nombre.Text).Rows.Count != 0)
            {
                info.Text = "Ese nombre de usuario ya fue tomado";
                return false;
            }
            return true;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (verificarUsuarioContraseñaValido())
            {
                var creador=new AbmCliente.Form3();

                creador.Closed += (s, arg) =>
                {
                    //hago un query para verificar que el cliente/proveedor fue efectivamente creado.
                    //podria mirar si la form3 termino con exito tambien, pero como necesito hacer el query 
                    //de todas formas para conseguir el id hago todo con el query y listo
                    if (!creador.finished)
                    {
                        Show();
                        return;
                    }
                    //@@IDENTITY me da el valor del ultimo insert de esta conexion, lo que esta bueno porque me ahorra
                    //hacer algun query raro aca para conseguir el id del cliente/proveedor que acabo de insertar
                    //si hay un insert en un trigger se romperia
                    var id = util.tableQuery("SELECT @@IDENTITY").Rows[0].ItemArray[0].ToString();



                    //no uso util porque necesito pasar contraseña sanatizado Y en UTF8, y no puedo hacer las 2 cosas con
                    //esa capa de abstraccion
                    var command = new SqlCommand("INSERT INTO tp.Usuario (nombre,contraseña,rol,cliente,proveedor)" +
                                        "VALUES (@no,HASHBYTES('SHA2_256',@co)," +
                                        "(SELECT id FROM tp.Rol WHERE nombre='Cliente')," + id + ",null)", Program.con);

                    command.Parameters.AddWithValue("@no", nombre.Text);
                    command.Parameters.Add(new SqlParameter {
                        SqlDbType = SqlDbType.VarChar, //c# pasa por default unicode, osea nvarchar
                        ParameterName = "@co",
                        Value = contraseña.Text
                    });

                    command.ExecuteNonQuery();

                    Close();
                };
                creador.Show();
                Hide();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //si c# tuviera templates de texto podria juntar este con el de arriba
            //pero no tiene templates de herencia que son un asco
            if (verificarUsuarioContraseñaValido())
            {
                var creador = new AbmProveedor.Form3();
                creador.Closed += (s, arg) =>
                {
                    if (!creador.finished)
                    {
                        Show();
                        return;
                    }
                    var id = util.tableQuery("SELECT @@IDENTITY").Rows[0].ItemArray[0].ToString();

                    var command = new SqlCommand("INSERT INTO tp.Usuario (nombre,contraseña,rol,cliente,proveedor)" +
                    "VALUES (@no,HASHBYTES('SHA2_256',@co)," +
                    "(SELECT id FROM tp.Rol WHERE nombre='Proveedor'), null," + id + ")", Program.con);

                    command.Parameters.AddWithValue("@no", nombre.Text);
                    command.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.VarChar,
                        ParameterName = "@co",
                        Value = contraseña.Text
                    });

                    command.ExecuteNonQuery();

                    Close();
                };
                creador.Show();
                Hide();
            }
        }


    }
}
