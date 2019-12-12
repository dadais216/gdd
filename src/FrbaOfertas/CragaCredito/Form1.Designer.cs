namespace FrbaOfertas.CragaCredito
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.monto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tipoPago = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numeroTarjeta = new System.Windows.Forms.TextBox();
            this.vencimientoTarjeta = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.tarjetas = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // monto
            // 
            this.monto.Location = new System.Drawing.Point(135, 20);
            this.monto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.monto.Name = "monto";
            this.monto.ShortcutsEnabled = false;
            this.monto.Size = new System.Drawing.Size(272, 22);
            this.monto.TabIndex = 0;
            this.monto.Tag = "";
            this.monto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.monto_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Monto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tipo de pago";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 224);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(380, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cargar Credito";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonCargar_Click);
            // 
            // tiposPago
            // 
            this.tipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipoPago.FormattingEnabled = true;
            this.tipoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Tarjeta de crédito",
            "Tarjeta de débito"});
            this.tipoPago.Location = new System.Drawing.Point(135, 58);
            this.tipoPago.Margin = new System.Windows.Forms.Padding(4);
            this.tipoPago.Name = "tiposPago";
            this.tipoPago.Size = new System.Drawing.Size(272, 24);
            this.tipoPago.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Núm. de tarjeta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Fecha de venc.";
            // 
            // numeroTarjeta
            // 
            this.numeroTarjeta.Enabled = false;
            this.numeroTarjeta.Location = new System.Drawing.Point(135, 142);
            this.numeroTarjeta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numeroTarjeta.Name = "numeroTarjeta";
            this.numeroTarjeta.Size = new System.Drawing.Size(272, 22);
            this.numeroTarjeta.TabIndex = 3;
            this.numeroTarjeta.Tag = "";
            // 
            // vencimientoTarjeta
            // 
            this.vencimientoTarjeta.Enabled = false;
            this.vencimientoTarjeta.Location = new System.Drawing.Point(135, 184);
            this.vencimientoTarjeta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.vencimientoTarjeta.Name = "vencimientoTarjeta";
            this.vencimientoTarjeta.Size = new System.Drawing.Size(272, 22);
            this.vencimientoTarjeta.TabIndex = 4;
            this.vencimientoTarjeta.Tag = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Tarjeta";
            // 
            // tarjetas
            // 
            this.tarjetas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tarjetas.FormattingEnabled = true;
            this.tarjetas.Location = new System.Drawing.Point(135, 98);
            this.tarjetas.Margin = new System.Windows.Forms.Padding(4);
            this.tarjetas.Name = "tarjetas";
            this.tarjetas.Size = new System.Drawing.Size(272, 24);
            this.tarjetas.TabIndex = 16;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(27, 270);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(380, 31);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 319);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.vencimientoTarjeta);
            this.Controls.Add(this.tarjetas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numeroTarjeta);
            this.Controls.Add(this.tipoPago);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monto);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRBA Ofertas - Cargar crédito";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox monto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox tipoPago;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numeroTarjeta;
        private System.Windows.Forms.DateTimePicker vencimientoTarjeta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox tarjetas;
        private System.Windows.Forms.Button btnCancelar;
    }
}