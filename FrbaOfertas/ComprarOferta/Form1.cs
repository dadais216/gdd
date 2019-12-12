﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas.ComprarOferta
{
    public partial class Form1 : Form
    {
        string rolId, userId, fecha;
        string clienteId; decimal saldo;
        public Form1(string rolId_,string userId_)
        {
            rolId = rolId_;
            userId = userId_;
            InitializeComponent();

            var date=DateTime.Parse(ConfigurationManager.AppSettings["fecha"]);

            fecha = date.Month.ToString() + "/" + date.Day.ToString() + "/" + date.Year.ToString();

            clienteId = util.getVal("SELECT cliente FROM tp.Usuario WHERE id= " + userId).ToString();
            doQuery();
        }

        private void actualizarSaldo()
        {
            saldo = (decimal)util.getVal("SELECT saldo FROM tp.Cliente WHERE id= " + clienteId);
            //se podria mantener el estado de saldo desde la aplicacion para no tener que hacer este query pero neh
            label1.Text = "Saldo: " + saldo.ToString();
        }

        private void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (rolId != util.tableQuery("SELECT id FROM tp.Rol WHERE nombre = 'Cliente'").Rows[0].ItemArray[0].ToString())
            {
                new ErrorWindow("un no cliente no puede comprar nada porque no tiene saldo en el sistema").Show();
                return;
            }


            if (saldo < (decimal)dataGridView1.Rows[e.RowIndex].Cells[1].Value)
            {
                new ErrorWindow("saldo insuficiente");
                return;
            }

            var confirm = new Confirm(dataGridView1.Rows[e.RowIndex].Cells,userId,fecha);
            confirm.Closed += (s, args) => doQuery();
            confirm.Show();
        }

        private void doQuery()
        {
            actualizarSaldo();
            dataGridView1.DataSource = util.tableQuery("SELECT descripcion,precio AS precioOferta ,precio_Ficticio AS precioOriginal,cantidad, codigo FROM tp.Oferta " +
                                                        "WHERE '" + fecha + "' BETWEEN fecha AND fecha_Venc AND cantidad > 0");
        }
    }
}
