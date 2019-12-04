namespace FrbaOfertas
{
    partial class Registro
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
            this.label1 = new System.Windows.Forms.Label();
            this.contraseña = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.info = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Contraseña";
            // 
            // contraseña
            // 
            this.contraseña.Location = new System.Drawing.Point(95, 63);
            this.contraseña.Name = "contraseña";
            this.contraseña.PasswordChar = '#';
            this.contraseña.Size = new System.Drawing.Size(132, 20);
            this.contraseña.TabIndex = 5;
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(28, 30);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(43, 13);
            this.userLabel.TabIndex = 2;
            this.userLabel.Text = "Usuario";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(92, 30);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(132, 20);
            this.nombre.TabIndex = 3;
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Location = new System.Drawing.Point(71, 110);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(0, 13);
            this.info.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "cliente";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(149, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "proveedor";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "registrarse como";
            // 
            // Registro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 218);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.info);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contraseña);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.nombre);
            this.Name = "Registro";
            this.Text = "Registro";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox contraseña;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
    }
}