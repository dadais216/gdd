namespace FrbaOfertas.ListadoEstadistico
{
    partial class ListadoView
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
            this.BestDiscounts = new System.Windows.Forms.Button();
            this.mostFacturado = new System.Windows.Forms.Button();
            this.TablaListado = new System.Windows.Forms.DataGridView();
            this.selectSemestre = new System.Windows.Forms.Label();
            this.selectSemestreCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.TablaListado)).BeginInit();
            this.SuspendLayout();
            // 
            // BestDiscounts
            // 
            this.BestDiscounts.Location = new System.Drawing.Point(866, 161);
            this.BestDiscounts.Name = "BestDiscounts";
            this.BestDiscounts.Size = new System.Drawing.Size(350, 76);
            this.BestDiscounts.TabIndex = 0;
            this.BestDiscounts.Text = "Best Discounts";
            this.BestDiscounts.UseVisualStyleBackColor = true;
            this.BestDiscounts.Click += new System.EventHandler(this.BestRent_Click);
            // 
            // mostFacturado
            // 
            this.mostFacturado.Location = new System.Drawing.Point(55, 161);
            this.mostFacturado.Name = "mostFacturado";
            this.mostFacturado.Size = new System.Drawing.Size(350, 76);
            this.mostFacturado.TabIndex = 1;
            this.mostFacturado.Text = "Mayor Facturacion";
            this.mostFacturado.UseVisualStyleBackColor = true;
            this.mostFacturado.Click += new System.EventHandler(this.mostFacturadoClick);
            // 
            // TablaListado
            // 
            this.TablaListado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaListado.Location = new System.Drawing.Point(55, 274);
            this.TablaListado.Name = "TablaListado";
            this.TablaListado.RowTemplate.Height = 33;
            this.TablaListado.Size = new System.Drawing.Size(1161, 502);
            this.TablaListado.TabIndex = 2;
            // 
            // selectSemestre
            // 
            this.selectSemestre.AutoSize = true;
            this.selectSemestre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectSemestre.Location = new System.Drawing.Point(64, 56);
            this.selectSemestre.Name = "selectSemestre";
            this.selectSemestre.Size = new System.Drawing.Size(322, 37);
            this.selectSemestre.TabIndex = 3;
            this.selectSemestre.Text = "Seleccionar semestre";
            // 
            // selectSemestreCombo
            // 
            this.selectSemestreCombo.FormattingEnabled = true;
            this.selectSemestreCombo.Location = new System.Drawing.Point(596, 62);
            this.selectSemestreCombo.Name = "selectSemestreCombo";
            this.selectSemestreCombo.Size = new System.Drawing.Size(620, 33);
            this.selectSemestreCombo.TabIndex = 4;
            // 
            // ListadoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 834);
            this.Controls.Add(this.selectSemestreCombo);
            this.Controls.Add(this.selectSemestre);
            this.Controls.Add(this.TablaListado);
            this.Controls.Add(this.mostFacturado);
            this.Controls.Add(this.BestDiscounts);
            this.Name = "ListadoView";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TablaListado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BestDiscounts;
        private System.Windows.Forms.Button mostFacturado;
        private System.Windows.Forms.DataGridView TablaListado;
        private System.Windows.Forms.Label selectSemestre;
        private System.Windows.Forms.ComboBox selectSemestreCombo;
    }
}