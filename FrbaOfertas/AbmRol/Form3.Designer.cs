namespace FrbaOfertas.AbmRol
{
    partial class Form3
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
            this.nombre = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(12, 34);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(260, 20);
            this.nombre.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "nombre";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(263, 62);
            this.button2.TabIndex = 22;
            this.button2.Text = "guardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 393);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button2);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
    }
}