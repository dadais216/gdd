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

namespace FrbaOfertas.AbmRol
{
    public partial class Form2 : Form
    {
        Form1 parent;
        Tuple<CheckBox,Label,int>[] checks;
        string rolId;
        string rolName;
        bool habilitado;
        public Form2(Form1 parent_, string rolId_,string rolName_,bool habilitado_)
        {
            rolId = rolId_;
            rolName = rolName_;
            habilitado = habilitado_;
            parent = parent_;

            InitializeComponent();

            nombre.Text = rolName;

            button1.Text = habilitado ? "deshabilitar" : "habilitar";

            var funcs = util.tableQuery("SELECT id,nombre FROM tp.Funcionalidad");


            //no puedo asumir que la posicion de checked es igual al id porque no lo es si se modifica algo en la db
            checks = new Tuple<CheckBox, Label,int>[funcs.Rows.Count];
            for (int i = 0; i < funcs.Rows.Count; i++)
            {
                checks[i] = new Tuple<CheckBox, Label,int>(new CheckBox(),new Label(),(int)funcs.Rows[i].ItemArray[0]);
                var box = checks[i].Item1;
                box.Checked = false;

                box.Width = 20;
                box.Height = 20;

                box.Left = (i / 8) * 220 + 20;
                box.Top = (i % 8) * 30 + 60;

                var label = checks[i].Item2;

                label.Width = 200;
                label.Text = (string)funcs.Rows[i].ItemArray[1];
                label.Left = (i / 8) * 220 + 40;
                label.Top = (i % 8) * 30 + 60;

                Controls.Add(box);
                Controls.Add(label);
            }

            var funcsChecked = util.tableQuery("SELECT f.id " +
                                       "FROM tp.Funcionalidad f JOIN tp.RolxFuncionalidad rf ON f.id = rf.funcionalidad " +
                                       "WHERE rf.rol = " + rolId);

            for (int i = 0; i < funcsChecked.Rows.Count; i++)
            {
                int id = (int)funcsChecked.Rows[i].ItemArray[0];

                foreach(var tuple in checks)
                {
                    if (tuple.Item3 == id)
                    {
                        tuple.Item1.Checked = true;
                        break;
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            util.execCommand("DELETE FROM tp.RolxFuncionalidad WHERE rol = " + rolId);

            string query = "INSERT INTO tp.RolxFuncionalidad (rol,funcionalidad) VALUES ";

            bool oneValIn = false;
            foreach(var tuple in checks)
            {
                if (tuple.Item1.Checked)
                {
                    if (oneValIn)
                    {
                        query += ", ";
                    }
                    query += "("+rolId+","+tuple.Item3.ToString()+")";
                    oneValIn = true;
                }

            }

            if (oneValIn)
            {
                util.execCommand(query);
            }

            if (rolName != nombre.Text)
            {
                util.execCommand("UPDATE tp.Rol SET nombre = @no WHERE id = " + rolId,
                                    "@no",nombre.Text);
            }

            parent.doQuery();
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            util.execCommand("UPDATE tp.Rol SET habilitado = " + (habilitado ? "0" : "1")
                                         + " WHERE id = " + rolId);
            if (habilitado)
            {
                util.execCommand("UPDATE tp.Usuario SET rol = null WHERE rol = " + rolId);
            }
            parent.doQuery();
            Close();
        }
    }



        //en retrospectiva hubiera sido mucho mas facil y mejor manejar la funcionalidad como una lista
        //de bits directamente en rol, seria mas rapido y haria este codigo mucho mas simple.
        //usando un int en rol se podrian guardar hasta 32 funcionalidades
        //no me tiro por hacerlo ahora porque creo que el tp no apunta a eso
}
