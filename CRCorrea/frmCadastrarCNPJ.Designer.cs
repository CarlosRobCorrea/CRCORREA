namespace CRCorrea
{
    partial class frmCadastrarCNPJ
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTexto_CNPJ = new System.Windows.Forms.TextBox();
            this.btnRetornar = new System.Windows.Forms.Button();
            this.txtTexto_CNPJ_Dig = new System.Windows.Forms.TextBox();
            this.txtCNPJ = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCNPJ);
            this.groupBox1.Controls.Add(this.txtTexto_CNPJ_Dig);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTexto_CNPJ);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(241, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 180);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proximo CNPJ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 64);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pode Cadastrar este CNPJ que não foi usado.\r\nMais não se esqueça de na proxima op" +
    "ortunidade \r\npegar o CNPJ/CPF do cliente e depois vir substituir \r\nno cadastro q" +
    "ue criou para ele.";
            // 
            // txtTexto_CNPJ
            // 
            this.txtTexto_CNPJ.Enabled = false;
            this.txtTexto_CNPJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTexto_CNPJ.Location = new System.Drawing.Point(101, 48);
            this.txtTexto_CNPJ.Name = "txtTexto_CNPJ";
            this.txtTexto_CNPJ.ReadOnly = true;
            this.txtTexto_CNPJ.Size = new System.Drawing.Size(107, 20);
            this.txtTexto_CNPJ.TabIndex = 0;
            this.txtTexto_CNPJ.Text = "57.788.830/0001";
            this.txtTexto_CNPJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTexto_CNPJ.TextChanged += new System.EventHandler(this.txtTexto_TextChanged);
            // 
            // btnRetornar
            // 
            this.btnRetornar.AccessibleDescription = "Retornar";
            //this.btnRetornar.Image = global::Refrigeracao.Properties.Resources.Retornar;
            this.btnRetornar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRetornar.Location = new System.Drawing.Point(322, 277);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(159, 37);
            this.btnRetornar.TabIndex = 5;
            this.btnRetornar.TabStop = false;
            this.btnRetornar.Text = "Retornar";
            this.btnRetornar.UseVisualStyleBackColor = true;
            this.btnRetornar.Click += new System.EventHandler(this.btnRetornar_Click);
            // 
            // txtTexto_CNPJ_Dig
            // 
            this.txtTexto_CNPJ_Dig.Enabled = false;
            this.txtTexto_CNPJ_Dig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTexto_CNPJ_Dig.Location = new System.Drawing.Point(214, 48);
            this.txtTexto_CNPJ_Dig.Name = "txtTexto_CNPJ_Dig";
            this.txtTexto_CNPJ_Dig.ReadOnly = true;
            this.txtTexto_CNPJ_Dig.Size = new System.Drawing.Size(26, 20);
            this.txtTexto_CNPJ_Dig.TabIndex = 2;
            this.txtTexto_CNPJ_Dig.Text = "XX";
            this.txtTexto_CNPJ_Dig.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCNPJ
            // 
            this.txtCNPJ.BackColor = System.Drawing.Color.Orange;
            this.txtCNPJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCNPJ.Location = new System.Drawing.Point(101, 73);
            this.txtCNPJ.Name = "txtCNPJ";
            this.txtCNPJ.ReadOnly = true;
            this.txtCNPJ.Size = new System.Drawing.Size(139, 20);
            this.txtCNPJ.TabIndex = 3;
            this.txtCNPJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCNPJ.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // frmCadastrarCNPJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRetornar);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCadastrarCNPJ";
            this.Text = "frmCadastrarCNPJ";
            this.Load += new System.EventHandler(this.frmCadastrarCNPJ_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTexto_CNPJ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRetornar;
        private System.Windows.Forms.TextBox txtTexto_CNPJ_Dig;
        private System.Windows.Forms.TextBox txtCNPJ;
    }
}