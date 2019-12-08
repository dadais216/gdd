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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.ventasDesdeLabel = new System.Windows.Forms.Label();
            this.ventasHastaLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listarFacturas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // selectProveedor
            // 
            this.selectProveedor.FormattingEnabled = true;
            this.selectProveedor.Location = new System.Drawing.Point(296, 37);
            this.selectProveedor.Name = "selectProveedor";
            this.selectProveedor.Size = new System.Drawing.Size(255, 33);
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
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(296, 104);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(397, 31);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(296, 166);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(397, 31);
            this.dateTimePicker2.TabIndex = 3;
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
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 240);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(989, 512);
            this.dataGridView1.TabIndex = 6;
            // 
            // listarFacturas
            // 
            this.listarFacturas.Location = new System.Drawing.Point(789, 156);
            this.listarFacturas.Name = "listarFacturas";
            this.listarFacturas.Size = new System.Drawing.Size(227, 50);
            this.listarFacturas.TabIndex = 7;
            this.listarFacturas.Text = "Listar";
            this.listarFacturas.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 803);
            this.Controls.Add(this.listarFacturas);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ventasHastaLabel);
            this.Controls.Add(this.ventasDesdeLabel);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.labelProveedor);
            this.Controls.Add(this.selectProveedor);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectProveedor;
        private System.Windows.Forms.Label labelProveedor;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label ventasDesdeLabel;
        private System.Windows.Forms.Label ventasHastaLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button listarFacturas;
    }
}