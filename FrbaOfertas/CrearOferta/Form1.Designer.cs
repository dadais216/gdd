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
            this.desc = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.fechavenc = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.preciooferta = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.preciolista = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cantidaddisp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // desc
            // 
            this.desc.Location = new System.Drawing.Point(16, 39);
            this.desc.Margin = new System.Windows.Forms.Padding(4);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(345, 22);
            this.desc.TabIndex = 32;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 83);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 17);
            this.label15.TabIndex = 31;
            this.label15.Text = "fechavenc";
            // 
            // fechavenc
            // 
            this.fechavenc.Location = new System.Drawing.Point(16, 104);
            this.fechavenc.Margin = new System.Windows.Forms.Padding(4);
            this.fechavenc.Name = "fechavenc";
            this.fechavenc.Size = new System.Drawing.Size(345, 24);
            this.fechavenc.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 17);
            this.label14.TabIndex = 29;
            this.label14.Text = "desc";
            // 
            // preciooferta
            // 
            this.preciooferta.Location = new System.Drawing.Point(16, 237);
            this.preciooferta.Margin = new System.Windows.Forms.Padding(4);
            this.preciooferta.Name = "preciooferta";
            this.preciooferta.Size = new System.Drawing.Size(345, 22);
            this.preciooferta.TabIndex = 28;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 285);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 17);
            this.label13.TabIndex = 27;
            this.label13.Text = "cantidaddisp";
            // 
            // preciolista
            // 
            this.preciolista.Location = new System.Drawing.Point(16, 166);
            this.preciolista.Margin = new System.Windows.Forms.Padding(4);
            this.preciolista.Name = "preciolista";
            this.preciolista.Size = new System.Drawing.Size(345, 22);
            this.preciolista.TabIndex = 26;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 145);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 17);
            this.label12.TabIndex = 25;
            this.label12.Text = "preciolista";
            // 
            // cantidaddisp
            // 
            this.cantidaddisp.Location = new System.Drawing.Point(16, 306);
            this.cantidaddisp.Margin = new System.Windows.Forms.Padding(4);
            this.cantidaddisp.Name = "cantidaddisp";
            this.cantidaddisp.Size = new System.Drawing.Size(345, 22);
            this.cantidaddisp.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 216);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 17);
            this.label11.TabIndex = 23;
            this.label11.Text = "preciooferta";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 352);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(345, 87);
            this.button1.TabIndex = 33;
            this.button1.Text = "Agregar Oferta";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 463);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.desc);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.fechavenc);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.preciooferta);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.preciolista);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cantidaddisp);
            this.Controls.Add(this.label11);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox desc;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox fechavenc;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox preciooferta;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox preciolista;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox cantidaddisp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
    }
}