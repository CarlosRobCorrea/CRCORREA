namespace CRCorrea
{
    partial class frmAcerto
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
            this.btnAcerto = new System.Windows.Forms.Button();
            this.btnRetornar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAcerto
            // 
            this.btnAcerto.Location = new System.Drawing.Point(406, 137);
            this.btnAcerto.Name = "btnAcerto";
            this.btnAcerto.Size = new System.Drawing.Size(156, 23);
            this.btnAcerto.TabIndex = 0;
            this.btnAcerto.Text = "Acertar o Preço de Custos";
            this.btnAcerto.UseVisualStyleBackColor = true;
            this.btnAcerto.Click += new System.EventHandler(this.btnAcerto_Click);
            // 
            // btnRetornar
            // 
            this.btnRetornar.Location = new System.Drawing.Point(406, 175);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(156, 23);
            this.btnRetornar.TabIndex = 1;
            this.btnRetornar.Text = "Retornar";
            this.btnRetornar.UseVisualStyleBackColor = true;
            this.btnRetornar.Click += new System.EventHandler(this.btnRetornar_Click);
            // 
            // frmAcerto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRetornar);
            this.Controls.Add(this.btnAcerto);
            this.Name = "frmAcerto";
            this.Text = "frmAcerto";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAcerto;
        private System.Windows.Forms.Button btnRetornar;
    }
}