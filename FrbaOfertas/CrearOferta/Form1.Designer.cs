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
            this.lblprecioAntiguo = new System.Windows.Forms.Label();
            this.lblPrecioNuevo = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.lblPublicacion = new System.Windows.Forms.Label();
            this.calendarioPublicacion = new System.Windows.Forms.DateTimePicker();
            this.calendarioVencimiento = new System.Windows.Forms.DateTimePicker();
            this.stock = new System.Windows.Forms.TextBox();
            this.precioAntiguo = new System.Windows.Forms.TextBox();
            this.precioNuevo = new System.Windows.Forms.TextBox();
            this.proveedor = new System.Windows.Forms.TextBox();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPublicar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(21, 25);
            this.lblStock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(43, 17);
            this.lblStock.TabIndex = 1;
            this.lblStock.Text = "Stock";
            // 
            // lblprecioAntiguo
            // 
            this.lblprecioAntiguo.AutoSize = true;
            this.lblprecioAntiguo.Location = new System.Drawing.Point(21, 71);
            this.lblprecioAntiguo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblprecioAntiguo.Name = "lblprecioAntiguo";
            this.lblprecioAntiguo.Size = new System.Drawing.Size(100, 17);
            this.lblprecioAntiguo.TabIndex = 3;
            this.lblprecioAntiguo.Text = "Precio Antiguo";
            // 
            // lblPrecioNuevo
            // 
            this.lblPrecioNuevo.AutoSize = true;
            this.lblPrecioNuevo.Location = new System.Drawing.Point(21, 116);
            this.lblPrecioNuevo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrecioNuevo.Name = "lblPrecioNuevo";
            this.lblPrecioNuevo.Size = new System.Drawing.Size(93, 17);
            this.lblPrecioNuevo.TabIndex = 4;
            this.lblPrecioNuevo.Text = "Precio Nuevo";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(21, 158);
            this.lblProveedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(102, 17);
            this.lblProveedor.TabIndex = 5;
            this.lblProveedor.Text = "Proveedor Cuit";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(21, 304);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(82, 17);
            this.lblDescripcion.TabIndex = 6;
            this.lblDescripcion.Text = "Descripción";
            // 
            // lblVencimiento
            // 
            this.lblVencimiento.AutoSize = true;
            this.lblVencimiento.Location = new System.Drawing.Point(21, 255);
            this.lblVencimiento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(105, 17);
            this.lblVencimiento.TabIndex = 7;
            this.lblVencimiento.Text = "Fecha de venc.";
            // 
            // lblPublicacion
            // 
            this.lblPublicacion.AutoSize = true;
            this.lblPublicacion.Location = new System.Drawing.Point(21, 211);
            this.lblPublicacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPublicacion.Name = "lblPublicacion";
            this.lblPublicacion.Size = new System.Drawing.Size(102, 17);
            this.lblPublicacion.TabIndex = 8;
            this.lblPublicacion.Text = "Fecha de publ.";
            // 
            // calendarioPublicacion
            // 
            this.calendarioPublicacion.Location = new System.Drawing.Point(151, 211);
            this.calendarioPublicacion.Margin = new System.Windows.Forms.Padding(4);
            this.calendarioPublicacion.Name = "calendarioPublicacion";
            this.calendarioPublicacion.Size = new System.Drawing.Size(265, 22);
            this.calendarioPublicacion.TabIndex = 8;
            // 
            // calendarioVencimiento
            // 
            this.calendarioVencimiento.Location = new System.Drawing.Point(151, 250);
            this.calendarioVencimiento.Margin = new System.Windows.Forms.Padding(4);
            this.calendarioVencimiento.Name = "calendarioVencimiento";
            this.calendarioVencimiento.Size = new System.Drawing.Size(265, 22);
            this.calendarioVencimiento.TabIndex = 9;
            // 
            // stock
            // 
            this.stock.Location = new System.Drawing.Point(151, 25);
            this.stock.Margin = new System.Windows.Forms.Padding(4);
            this.stock.MaxLength = 5;
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(265, 22);
            this.stock.TabIndex = 2;
            // 
            // precioAntiguo
            // 
            this.precioAntiguo.Location = new System.Drawing.Point(151, 68);
            this.precioAntiguo.Margin = new System.Windows.Forms.Padding(4);
            this.precioAntiguo.MaxLength = 18;
            this.precioAntiguo.Name = "txtPrecioAntiguo";
            this.precioAntiguo.Size = new System.Drawing.Size(265, 22);
            this.precioAntiguo.TabIndex = 4;
            // 
            // precioNuevo
            // 
            this.precioNuevo.Location = new System.Drawing.Point(151, 111);
            this.precioNuevo.Margin = new System.Windows.Forms.Padding(4);
            this.precioNuevo.MaxLength = 18;
            this.precioNuevo.Name = "txtPrecioNuevo";
            this.precioNuevo.Size = new System.Drawing.Size(265, 22);
            this.precioNuevo.TabIndex = 5;
            // 
            // proveedor
            // 
            this.proveedor.Enabled = false;
            this.proveedor.Location = new System.Drawing.Point(151, 155);
            this.proveedor.Margin = new System.Windows.Forms.Padding(4);
            this.proveedor.MaxLength = 20;
            this.proveedor.Name = "txtProveedor";
            this.proveedor.Size = new System.Drawing.Size(265, 22);
            this.proveedor.TabIndex = 6;
            // 
            // descripcion
            // 
            this.descripcion.Location = new System.Drawing.Point(24, 335);
            this.descripcion.Margin = new System.Windows.Forms.Padding(4);
            this.descripcion.MaxLength = 255;
            this.descripcion.Multiline = true;
            this.descripcion.Name = "txtDescripcion";
            this.descripcion.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.descripcion.Size = new System.Drawing.Size(392, 153);
            this.descripcion.TabIndex = 7;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(24, 546);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(543, 31);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnPublicar
            // 
            this.btnPublicar.Location = new System.Drawing.Point(24, 508);
            this.btnPublicar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPublicar.Name = "btnPublicar";
            this.btnPublicar.Size = new System.Drawing.Size(543, 30);
            this.btnPublicar.TabIndex = 12;
            this.btnPublicar.Text = "Publicar";
            this.btnPublicar.UseVisualStyleBackColor = true;
            this.btnPublicar.Click += new System.EventHandler(this.btnPublicar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(433, 152);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(134, 28);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.Text = "Buscar proveedor";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 589);
            this.Controls.Add(this.calendarioVencimiento);
            this.Controls.Add(this.calendarioPublicacion);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnPublicar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.descripcion);
            this.Controls.Add(this.proveedor);
            this.Controls.Add(this.precioNuevo);
            this.Controls.Add(this.precioAntiguo);
            this.Controls.Add(this.stock);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.lblPrecioNuevo);
            this.Controls.Add(this.lblprecioAntiguo);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.lblVencimiento);
            this.Controls.Add(this.lblPublicacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRBA Ofertas - Crear Oferta";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblprecioAntiguo;
        private System.Windows.Forms.Label lblPrecioNuevo;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblVencimiento;
        private System.Windows.Forms.Label lblPublicacion;
        private System.Windows.Forms.TextBox stock;
        private System.Windows.Forms.TextBox precioAntiguo;
        private System.Windows.Forms.TextBox precioNuevo;
        private System.Windows.Forms.TextBox proveedor;
        private System.Windows.Forms.TextBox descripcion;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnPublicar;
        private System.Windows.Forms.DateTimePicker calendarioPublicacion;
        private System.Windows.Forms.DateTimePicker calendarioVencimiento;
        private System.Windows.Forms.Button btnBuscar;
    }
}