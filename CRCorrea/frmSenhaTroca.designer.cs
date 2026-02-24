namespace CRCorrea
{
    partial class frmSenhaTroca
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSenhaTroca));
            this.gbxInfo = new System.Windows.Forms.GroupBox();
            this.tbxUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxSenha = new System.Windows.Forms.GroupBox();
            this.tbxNovaSenha2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxNovaSenha = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxInfo.SuspendLayout();
            this.gbxSenha.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxInfo
            // 
            this.gbxInfo.AccessibleDescription = "Usuário";
            this.gbxInfo.Controls.Add(this.tbxUsuario);
            this.gbxInfo.Controls.Add(this.label1);
            this.gbxInfo.Location = new System.Drawing.Point(12, 12);
            this.gbxInfo.Name = "gbxInfo";
            this.gbxInfo.Size = new System.Drawing.Size(218, 53);
            this.gbxInfo.TabIndex = 0;
            this.gbxInfo.TabStop = false;
            // 
            // tbxUsuario
            // 
            this.tbxUsuario.AccessibleDescription = "Usuário";
            this.tbxUsuario.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxUsuario.Enabled = false;
            this.tbxUsuario.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUsuario.Location = new System.Drawing.Point(65, 20);
            this.tbxUsuario.Name = "tbxUsuario";
            this.tbxUsuario.Size = new System.Drawing.Size(147, 21);
            this.tbxUsuario.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuário:";
            // 
            // gbxSenha
            // 
            this.gbxSenha.AccessibleDescription = "Senha";
            this.gbxSenha.Controls.Add(this.tbxNovaSenha2);
            this.gbxSenha.Controls.Add(this.label3);
            this.gbxSenha.Controls.Add(this.tbxNovaSenha);
            this.gbxSenha.Controls.Add(this.label2);
            this.gbxSenha.Location = new System.Drawing.Point(12, 71);
            this.gbxSenha.Name = "gbxSenha";
            this.gbxSenha.Size = new System.Drawing.Size(218, 83);
            this.gbxSenha.TabIndex = 1;
            this.gbxSenha.TabStop = false;
            // 
            // tbxNovaSenha2
            // 
            this.tbxNovaSenha2.AccessibleDescription = "Redigite Senha";
            this.tbxNovaSenha2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNovaSenha2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNovaSenha2.Location = new System.Drawing.Point(100, 47);
            this.tbxNovaSenha2.MaxLength = 20;
            this.tbxNovaSenha2.Name = "tbxNovaSenha2";
            this.tbxNovaSenha2.PasswordChar = '*';
            this.tbxNovaSenha2.Size = new System.Drawing.Size(112, 21);
            this.tbxNovaSenha2.TabIndex = 5;
            this.tbxNovaSenha2.GotFocus += new System.EventHandler(this.FormGotFocus);
            this.tbxNovaSenha2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.tbxNovaSenha2.LostFocus += new System.EventHandler(this.FormLostFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Redigite Senha:";
            // 
            // tbxNovaSenha
            // 
            this.tbxNovaSenha.AccessibleDescription = "Nova Senha";
            this.tbxNovaSenha.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNovaSenha.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNovaSenha.Location = new System.Drawing.Point(100, 20);
            this.tbxNovaSenha.MaxLength = 20;
            this.tbxNovaSenha.Name = "tbxNovaSenha";
            this.tbxNovaSenha.PasswordChar = '*';
            this.tbxNovaSenha.Size = new System.Drawing.Size(112, 21);
            this.tbxNovaSenha.TabIndex = 3;
            this.tbxNovaSenha.GotFocus += new System.EventHandler(this.FormGotFocus);
            this.tbxNovaSenha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.tbxNovaSenha.LostFocus += new System.EventHandler(this.FormLostFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nova Senha:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.AccessibleDescription = "Cancelar";
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(166, 160);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(64, 47);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleDescription = "Ok";
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(96, 160);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 47);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&Ok";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmSenhaTroca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 214);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbxSenha);
            this.Controls.Add(this.gbxInfo);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSenhaTroca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "8";
            this.Text = "Trocar Senha - Registrando";
            this.toolTip1.SetToolTip(this, "Trocar Senha - Registrando");
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSenhaTroca_Load);
            this.gbxInfo.ResumeLayout(false);
            this.gbxInfo.PerformLayout();
            this.gbxSenha.ResumeLayout(false);
            this.gbxSenha.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxUsuario;
        private System.Windows.Forms.GroupBox gbxSenha;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbxNovaSenha2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxNovaSenha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}