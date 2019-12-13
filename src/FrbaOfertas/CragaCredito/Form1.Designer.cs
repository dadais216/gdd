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
            this.numeroTarjeta = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // monto
            // 
            this.monto.Location = new System.Drawing.Point(101, 16);
            this.monto.Margin = new System.Windows.Forms.Padding(2);
            this.monto.Name = "monto";
            this.monto.ShortcutsEnabled = false;
            this.monto.Size = new System.Drawing.Size(205, 20);
            this.monto.TabIndex = 0;
            this.monto.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Monto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tipo de pago";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 118);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(285, 25);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cargar Credito";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonCargar_Click);
            // 
            // tipoPago
            // 
            this.tipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipoPago.FormattingEnabled = true;
            this.tipoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Crédito",
            "Débito"});
            this.tipoPago.Location = new System.Drawing.Point(101, 47);
            this.tipoPago.Name = "tipoPago";
            this.tipoPago.Size = new System.Drawing.Size(205, 21);
            this.tipoPago.TabIndex = 6;
            this.tipoPago.SelectedIndexChanged += new System.EventHandler(this.TipoPago_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Núm. de tarjeta";
            // 
            // numeroTarjeta
            // 
            this.numeroTarjeta.Location = new System.Drawing.Point(101, 81);
            this.numeroTarjeta.Margin = new System.Windows.Forms.Padding(2);
            this.numeroTarjeta.Name = "numeroTarjeta";
            this.numeroTarjeta.Size = new System.Drawing.Size(205, 20);
            this.numeroTarjeta.TabIndex = 3;
            this.numeroTarjeta.Tag = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 156);
            this.Controls.Add(this.numeroTarjeta);
            this.Controls.Add(this.tipoPago);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRBA Ofertas - Cargar crédito";
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
        private System.Windows.Forms.TextBox numeroTarjeta;
    }
}