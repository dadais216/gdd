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

namespace FrbaOfertas.AbmRol
{
    public partial class Form3 : Form
    {
        Form1 parent;
        Tuple<CheckBox, Label, int>[] checks;
        public Form3(Form1 form)
        {
            parent = form;

            InitializeComponent();

            SqlDataAdapter adp = new SqlDataAdapter("SELECT id,nombre " +
                                                    "FROM tp.Funcionalidad"
                                                    , Program.con);
            DataTable funcs = new DataTable();
            adp.Fill(funcs);

            //no puedo asumir que la posicion de checked es igual al id porque no lo es si se modifica algo en la db
            checks = new Tuple<CheckBox, Label, int>[funcs.Rows.Count];
            for (int i = 0; i < funcs.Rows.Count; i++)
            {
                checks[i] = new Tuple<CheckBox, Label, int>(new CheckBox(), new Label(), (int)funcs.Rows[i].ItemArray[0]);
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
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            util.execCommand("INSERT INTO tp.Rol (nombre) VALUES (@no)",
                                "@no",nombre.Text);

            var table = util.tableQuery("SELECT id FROM tp.Rol WHERE nombre = @no",
                                "@no", nombre.Text);

            string rolId = table.Rows[0].ItemArray[0].ToString();

            string query = "INSERT INTO tp.RolxFuncionalidad (rol,funcionalidad) VALUES ";

            bool oneValIn = false;
            foreach (var tuple in checks)
            {
                if (tuple.Item1.Checked)
                {
                    if (oneValIn)
                    {
                        query += ", ";
                    }
                    query += "(" + rolId + "," + tuple.Item3.ToString() + ")";
                    oneValIn = true;
                }

            }

            if (oneValIn)
            {
                util.execCommand(query);
            }

            parent.doQuery();
            Close();
        }
    }
}
