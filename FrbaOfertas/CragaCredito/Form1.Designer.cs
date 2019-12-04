namespace FrbaOfertas.CragaCredito
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
            this.monto = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tipopago = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // monto
            // 
            this.monto.Location = new System.Drawing.Point(16, 110);
            this.monto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.monto.Name = "monto";
            this.monto.Size = new System.Drawing.Size(345, 22);
            this.monto.TabIndex = 32;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(13, 22);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 17);
            this.label20.TabIndex = 31;
            this.label20.Text = "monto";
            // 
            // tipopago
            // 
            this.tipopago.Location = new System.Drawing.Point(16, 43);
            this.tipopago.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tipopago.Name = "tipopago";
            this.tipopago.Size = new System.Drawing.Size(345, 22);
            this.tipopago.TabIndex = 30;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(13, 89);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(63, 17);
            this.label21.TabIndex = 29;
            this.label21.Text = "tipopago";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 158);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(345, 87);
            this.button1.TabIndex = 33;
            this.button1.Text = "agregar carga";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 266);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.monto);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.tipopago);
            this.Controls.Add(this.label21);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox monto;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tipopago;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button button1;
    }
}