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
            this.BestRent = new System.Windows.Forms.Button();
            this.bestValue = new System.Windows.Forms.Button();
            this.TablaListado = new System.Windows.Forms.DataGridView();
            this.selectSemestre = new System.Windows.Forms.Label();
            this.selectSemestreCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.TablaListado)).BeginInit();
            this.SuspendLayout();
            // 
            // BestRent
            // 
            this.BestRent.Location = new System.Drawing.Point(866, 161);
            this.BestRent.Name = "BestRent";
            this.BestRent.Size = new System.Drawing.Size(350, 76);
            this.BestRent.TabIndex = 0;
            this.BestRent.Text = "Best Rent";
            this.BestRent.UseVisualStyleBackColor = true;
            this.BestRent.Click += new System.EventHandler(this.BestRent_Click);
            // 
            // bestValue
            // 
            this.bestValue.Location = new System.Drawing.Point(55, 161);
            this.bestValue.Name = "bestValue";
            this.bestValue.Size = new System.Drawing.Size(350, 76);
            this.bestValue.TabIndex = 1;
            this.bestValue.Text = "Best Value";
            this.bestValue.UseVisualStyleBackColor = true;
            this.bestValue.Click += new System.EventHandler(this.bestValue_Click);
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
            this.Controls.Add(this.bestValue);
            this.Controls.Add(this.BestRent);
            this.Name = "ListadoView";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TablaListado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BestRent;
        private System.Windows.Forms.Button bestValue;
        private System.Windows.Forms.DataGridView TablaListado;
        private System.Windows.Forms.Label selectSemestre;
        private System.Windows.Forms.ComboBox selectSemestreCombo;
    }
}