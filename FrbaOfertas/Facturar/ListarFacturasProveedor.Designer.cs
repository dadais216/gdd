namespace FrbaOfertas.Facturar
{
    partial class ListarFacturasProveedor
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
            this.selectProveedor = new System.Windows.Forms.ComboBox();
            this.labelProveedor = new System.Windows.Forms.Label();
            this.desdePicker = new System.Windows.Forms.DateTimePicker();
            this.hastaPicker = new System.Windows.Forms.DateTimePicker();
            this.ventasDesdeLabel = new System.Windows.Forms.Label();
            this.ventasHastaLabel = new System.Windows.Forms.Label();
            this.TablaProveedores = new System.Windows.Forms.DataGridView();
            this.listarFacturas = new System.Windows.Forms.Button();
            this.facturarButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TablaProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // selectProveedor
            // 
            this.selectProveedor.FormattingEnabled = true;
            this.selectProveedor.Location = new System.Drawing.Point(296, 37);
            this.selectProveedor.Name = "selectProveedor";
            this.selectProveedor.Size = new System.Drawing.Size(397, 33);
            this.selectProveedor.TabIndex = 0;
            // 
            // labelProveedor
            // 
            this.labelProveedor.AutoSize = true;
            this.labelProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProveedor.Location = new System.Drawing.Point(22, 37);
            this.labelProveedor.Name = "labelProveedor";
            this.labelProveedor.Size = new System.Drawing.Size(258, 29);
            this.labelProveedor.TabIndex = 1;
            this.labelProveedor.Text = "Seleccionar proveedor";
            // 
            // desdePicker
            // 
            this.desdePicker.Location = new System.Drawing.Point(296, 104);
            this.desdePicker.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.desdePicker.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.desdePicker.Name = "desdePicker";
            this.desdePicker.Size = new System.Drawing.Size(397, 31);
            this.desdePicker.TabIndex = 2;
            // 
            // hastaPicker
            // 
            this.hastaPicker.Location = new System.Drawing.Point(296, 166);
            this.hastaPicker.Name = "hastaPicker";
            this.hastaPicker.Size = new System.Drawing.Size(397, 31);
            this.hastaPicker.TabIndex = 3;
            // 
            // ventasDesdeLabel
            // 
            this.ventasDesdeLabel.AutoSize = true;
            this.ventasDesdeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ventasDesdeLabel.Location = new System.Drawing.Point(22, 104);
            this.ventasDesdeLabel.Name = "ventasDesdeLabel";
            this.ventasDesdeLabel.Size = new System.Drawing.Size(160, 29);
            this.ventasDesdeLabel.TabIndex = 4;
            this.ventasDesdeLabel.Text = "Ventas desde";
            // 
            // ventasHastaLabel
            // 
            this.ventasHastaLabel.AutoSize = true;
            this.ventasHastaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ventasHastaLabel.Location = new System.Drawing.Point(22, 166);
            this.ventasHastaLabel.Name = "ventasHastaLabel";
            this.ventasHastaLabel.Size = new System.Drawing.Size(149, 29);
            this.ventasHastaLabel.TabIndex = 5;
            this.ventasHastaLabel.Text = "Ventas hasta";
            // 
            // TablaProveedores
            // 
            this.TablaProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaProveedores.Location = new System.Drawing.Point(27, 240);
            this.TablaProveedores.Name = "TablaProveedores";
            this.TablaProveedores.RowTemplate.Height = 33;
            this.TablaProveedores.Size = new System.Drawing.Size(1432, 512);
            this.TablaProveedores.TabIndex = 6;
            // 
            // listarFacturas
            // 
            this.listarFacturas.Location = new System.Drawing.Point(1232, 156);
            this.listarFacturas.Name = "listarFacturas";
            this.listarFacturas.Size = new System.Drawing.Size(227, 50);
            this.listarFacturas.TabIndex = 7;
            this.listarFacturas.Text = "Listar";
            this.listarFacturas.UseVisualStyleBackColor = true;
            this.listarFacturas.Click += new System.EventHandler(this.listarFacturas_Click);
            // 
            // facturarButton
            // 
            this.facturarButton.Location = new System.Drawing.Point(1209, 777);
            this.facturarButton.Name = "facturarButton";
            this.facturarButton.Size = new System.Drawing.Size(250, 46);
            this.facturarButton.TabIndex = 8;
            this.facturarButton.Text = "Facturar";
            this.facturarButton.UseVisualStyleBackColor = true;
            this.facturarButton.Click += new System.EventHandler(this.facturarButton_Click);
            // 
            // ListarFacturasProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1553, 857);
            this.Controls.Add(this.facturarButton);
            this.Controls.Add(this.listarFacturas);
            this.Controls.Add(this.TablaProveedores);
            this.Controls.Add(this.ventasHastaLabel);
            this.Controls.Add(this.ventasDesdeLabel);
            this.Controls.Add(this.hastaPicker);
            this.Controls.Add(this.desdePicker);
            this.Controls.Add(this.labelProveedor);
            this.Controls.Add(this.selectProveedor);
            this.Name = "ListarFacturasProveedor";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TablaProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectProveedor;
        private System.Windows.Forms.Label labelProveedor;
        private System.Windows.Forms.DateTimePicker desdePicker;
        private System.Windows.Forms.DateTimePicker hastaPicker;
        private System.Windows.Forms.Label ventasDesdeLabel;
        private System.Windows.Forms.Label ventasHastaLabel;
        private System.Windows.Forms.DataGridView TablaProveedores;
        private System.Windows.Forms.Button listarFacturas;
        private System.Windows.Forms.Button facturarButton;
    }
}
