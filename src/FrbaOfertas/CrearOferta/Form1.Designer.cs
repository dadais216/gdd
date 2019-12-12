namespace FrbaOfertas.CrearOferta
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
            this.lblStock = new System.Windows.Forms.Label();
            this.lblprecioListado = new System.Windows.Forms.Label();
            this.lblPrecioOferta = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.lblPublicacion = new System.Windows.Forms.Label();
            this.calendarioPublicacion = new System.Windows.Forms.DateTimePicker();
            this.calendarioVencimiento = new System.Windows.Forms.DateTimePicker();
            this.stock = new System.Windows.Forms.TextBox();
            this.precioListado = new System.Windows.Forms.TextBox();
            this.precioOferta = new System.Windows.Forms.TextBox();
            this.CUIT = new System.Windows.Forms.TextBox();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.btnPublicar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(16, 20);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(35, 13);
            this.lblStock.TabIndex = 1;
            this.lblStock.Text = "Stock";
            // 
            // lblprecioListado
            // 
            this.lblprecioListado.AutoSize = true;
            this.lblprecioListado.Location = new System.Drawing.Point(16, 58);
            this.lblprecioListado.Name = "lblprecioListado";
            this.lblprecioListado.Size = new System.Drawing.Size(76, 13);
            this.lblprecioListado.TabIndex = 3;
            this.lblprecioListado.Text = "Precio Antiguo";
            // 
            // lblPrecioOferta
            // 
            this.lblPrecioOferta.AutoSize = true;
            this.lblPrecioOferta.Location = new System.Drawing.Point(16, 94);
            this.lblPrecioOferta.Name = "lblPrecioOferta";
            this.lblPrecioOferta.Size = new System.Drawing.Size(72, 13);
            this.lblPrecioOferta.TabIndex = 4;
            this.lblPrecioOferta.Text = "Precio Nuevo";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(16, 128);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(77, 13);
            this.lblProveedor.TabIndex = 5;
            this.lblProveedor.Text = "Proveedor Cuit";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(16, 247);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblDescripcion.TabIndex = 6;
            this.lblDescripcion.Text = "Descripción";
            // 
            // lblVencimiento
            // 
            this.lblVencimiento.AutoSize = true;
            this.lblVencimiento.Location = new System.Drawing.Point(16, 207);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(82, 13);
            this.lblVencimiento.TabIndex = 7;
            this.lblVencimiento.Text = "Fecha de venc.";
            // 
            // lblPublicacion
            // 
            this.lblPublicacion.AutoSize = true;
            this.lblPublicacion.Location = new System.Drawing.Point(16, 171);
            this.lblPublicacion.Name = "lblPublicacion";
            this.lblPublicacion.Size = new System.Drawing.Size(78, 13);
            this.lblPublicacion.TabIndex = 8;
            this.lblPublicacion.Text = "Fecha de publ.";
            // 
            // calendarioPublicacion
            // 
            this.calendarioPublicacion.Location = new System.Drawing.Point(113, 171);
            this.calendarioPublicacion.Name = "calendarioPublicacion";
            this.calendarioPublicacion.Size = new System.Drawing.Size(200, 20);
            this.calendarioPublicacion.TabIndex = 8;
            // 
            // calendarioVencimiento
            // 
            this.calendarioVencimiento.Location = new System.Drawing.Point(113, 203);
            this.calendarioVencimiento.Name = "calendarioVencimiento";
            this.calendarioVencimiento.Size = new System.Drawing.Size(200, 20);
            this.calendarioVencimiento.TabIndex = 9;
            // 
            // stock
            // 
            this.stock.Location = new System.Drawing.Point(113, 20);
            this.stock.MaxLength = 5;
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(200, 20);
            this.stock.TabIndex = 2;
            // 
            // precioListado
            // 
            this.precioListado.Location = new System.Drawing.Point(113, 55);
            this.precioListado.MaxLength = 18;
            this.precioListado.Name = "precioListado";
            this.precioListado.Size = new System.Drawing.Size(200, 20);
            this.precioListado.TabIndex = 4;
            // 
            // precioOferta
            // 
            this.precioOferta.Location = new System.Drawing.Point(113, 90);
            this.precioOferta.MaxLength = 18;
            this.precioOferta.Name = "precioOferta";
            this.precioOferta.Size = new System.Drawing.Size(200, 20);
            this.precioOferta.TabIndex = 5;
            // 
            // CUIT
            // 
            this.CUIT.Location = new System.Drawing.Point(113, 126);
            this.CUIT.MaxLength = 20;
            this.CUIT.Name = "CUIT";
            this.CUIT.Size = new System.Drawing.Size(200, 20);
            this.CUIT.TabIndex = 6;
            // 
            // descripcion
            // 
            this.descripcion.Location = new System.Drawing.Point(18, 272);
            this.descripcion.MaxLength = 255;
            this.descripcion.Multiline = true;
            this.descripcion.Name = "descripcion";
            this.descripcion.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.descripcion.Size = new System.Drawing.Size(295, 125);
            this.descripcion.TabIndex = 7;
            // 
            // btnPublicar
            // 
            this.btnPublicar.Location = new System.Drawing.Point(18, 413);
            this.btnPublicar.Name = "btnPublicar";
            this.btnPublicar.Size = new System.Drawing.Size(295, 24);
            this.btnPublicar.TabIndex = 12;
            this.btnPublicar.Text = "Publicar";
            this.btnPublicar.UseVisualStyleBackColor = true;
            this.btnPublicar.Click += new System.EventHandler(this.btnPublicar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 446);
            this.Controls.Add(this.calendarioVencimiento);
            this.Controls.Add(this.calendarioPublicacion);
            this.Controls.Add(this.btnPublicar);
            this.Controls.Add(this.descripcion);
            this.Controls.Add(this.CUIT);
            this.Controls.Add(this.precioOferta);
            this.Controls.Add(this.precioListado);
            this.Controls.Add(this.stock);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.lblPrecioOferta);
            this.Controls.Add(this.lblprecioListado);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.lblVencimiento);
            this.Controls.Add(this.lblPublicacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRBA Ofertas - Crear Oferta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblprecioListado;
        private System.Windows.Forms.Label lblPrecioOferta;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblVencimiento;
        private System.Windows.Forms.Label lblPublicacion;
        private System.Windows.Forms.TextBox stock;
        private System.Windows.Forms.TextBox precioListado;
        private System.Windows.Forms.TextBox precioOferta;
        private System.Windows.Forms.TextBox CUIT;
        private System.Windows.Forms.TextBox descripcion;
        private System.Windows.Forms.Button btnPublicar;
        private System.Windows.Forms.DateTimePicker calendarioPublicacion;
        private System.Windows.Forms.DateTimePicker calendarioVencimiento;
    }
}