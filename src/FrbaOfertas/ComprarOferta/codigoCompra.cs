﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaOfertas.ComprarOferta
{
    public partial class codigoCompra : Form
    {
        public codigoCompra(string codigoCompra)
        {
            InitializeComponent();
            label1.Text = codigoCompra;
        }
    }
}
