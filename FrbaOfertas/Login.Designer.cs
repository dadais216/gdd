namespace FrbaOfertas
{
    partial class Login
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nombre = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.contraseña = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.info = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(101, 50);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(132, 20);
            this.nombre.TabIndex = 0;
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(37, 50);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(43, 13);
            this.userLabel.TabIndex = 0;
            this.userLabel.Text = "Usuario";
            // 
            // contraseña
            // 
            this.contraseña.Location = new System.Drawing.Point(101, 83);
            this.contraseña.Name = "contraseña";
            this.contraseña.PasswordChar = '#';
            this.contraseña.Size = new System.Drawing.Size(132, 20);
            this.contraseña.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Contraseña";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(158, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(0, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(100, 23);
            this.linkLabel1.TabIndex = 0;
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Location = new System.Drawing.Point(37, 127);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(0, 13);
            this.info.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(40, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "registrar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 205);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.info);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contraseña);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.nombre);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.TextBox contraseña;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.Button button2;
    }
}

