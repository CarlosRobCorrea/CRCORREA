namespace CRCorrea
{
    partial class frmSenha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSenha));
            this.gbxUsuario = new System.Windows.Forms.GroupBox();
            this.tbxSenha = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxEmpresa = new System.Windows.Forms.GroupBox();
            this.btnAnoMenos = new System.Windows.Forms.Button();
            this.btnAnoMais = new System.Windows.Forms.Button();
            this.btnMesMenos = new System.Windows.Forms.Button();
            this.btnMesMais = new System.Windows.Forms.Button();
            this.btnEmpresaMenos = new System.Windows.Forms.Button();
            this.btnEmpresaMais = new System.Windows.Forms.Button();
            this.tbxAno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxMes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxEmpresa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pbr = new System.Windows.Forms.ProgressBar();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.bwrDadosPadrao = new System.ComponentModel.BackgroundWorker();
            this.btnConfigConnection = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbxOrigem = new System.Windows.Forms.ListBox();
            this.gbxUsuario.SuspendLayout();
            this.gbxEmpresa.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxUsuario
            // 
            this.gbxUsuario.AccessibleDescription = "Usuário";
            this.gbxUsuario.Controls.Add(this.tbxSenha);
            this.gbxUsuario.Controls.Add(this.label2);
            this.gbxUsuario.Controls.Add(this.tbxUsuario);
            this.gbxUsuario.Controls.Add(this.label1);
            this.gbxUsuario.Location = new System.Drawing.Point(12, 12);
            this.gbxUsuario.Name = "gbxUsuario";
            this.gbxUsuario.Size = new System.Drawing.Size(200, 83);
            this.gbxUsuario.TabIndex = 0;
            this.gbxUsuario.TabStop = false;
            // 
            // tbxSenha
            // 
            this.tbxSenha.AccessibleDescription = "Senha";
            this.tbxSenha.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxSenha.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbxSenha, "digite a sua senha");
            this.tbxSenha.Location = new System.Drawing.Point(59, 47);
            this.tbxSenha.MaxLength = 20;
            this.tbxSenha.Name = "tbxSenha";
            this.helpProvider1.SetShowHelp(this.tbxSenha, true);
            this.tbxSenha.Size = new System.Drawing.Size(126, 21);
            this.tbxSenha.TabIndex = 2;
            this.tbxSenha.UseSystemPasswordChar = true;
            this.tbxSenha.GotFocus += new System.EventHandler(this.FormGotFocus);
            this.tbxSenha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.tbxSenha.LostFocus += new System.EventHandler(this.FormLostFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Senha:";
            // 
            // tbxUsuario
            // 
            this.tbxUsuario.AccessibleDescription = "Usuário";
            this.tbxUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxUsuario.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbxUsuario, "Informe o seu nome usuário");
            this.tbxUsuario.Location = new System.Drawing.Point(59, 20);
            this.tbxUsuario.MaxLength = 20;
            this.tbxUsuario.Name = "tbxUsuario";
            this.helpProvider1.SetShowHelp(this.tbxUsuario, true);
            this.tbxUsuario.Size = new System.Drawing.Size(126, 21);
            this.tbxUsuario.TabIndex = 1;
            this.tbxUsuario.GotFocus += new System.EventHandler(this.FormGotFocus);
            this.tbxUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.tbxUsuario.LostFocus += new System.EventHandler(this.FormLostFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuário:";
            // 
            // gbxEmpresa
            // 
            this.gbxEmpresa.AccessibleDescription = "Empresa";
            this.gbxEmpresa.Controls.Add(this.btnAnoMenos);
            this.gbxEmpresa.Controls.Add(this.btnAnoMais);
            this.gbxEmpresa.Controls.Add(this.btnMesMenos);
            this.gbxEmpresa.Controls.Add(this.btnMesMais);
            this.gbxEmpresa.Controls.Add(this.btnEmpresaMenos);
            this.gbxEmpresa.Controls.Add(this.btnEmpresaMais);
            this.gbxEmpresa.Controls.Add(this.tbxAno);
            this.gbxEmpresa.Controls.Add(this.label5);
            this.gbxEmpresa.Controls.Add(this.tbxMes);
            this.gbxEmpresa.Controls.Add(this.label4);
            this.gbxEmpresa.Controls.Add(this.tbxEmpresa);
            this.gbxEmpresa.Controls.Add(this.label3);
            this.gbxEmpresa.Location = new System.Drawing.Point(12, 101);
            this.gbxEmpresa.Name = "gbxEmpresa";
            this.gbxEmpresa.Size = new System.Drawing.Size(200, 93);
            this.gbxEmpresa.TabIndex = 3;
            this.gbxEmpresa.TabStop = false;
            // 
            // btnAnoMenos
            // 
            this.btnAnoMenos.AccessibleDescription = "Menos";
            this.btnAnoMenos.Location = new System.Drawing.Point(156, 60);
            this.btnAnoMenos.Name = "btnAnoMenos";
            this.btnAnoMenos.Size = new System.Drawing.Size(22, 21);
            this.btnAnoMenos.TabIndex = 9;
            this.btnAnoMenos.Text = "-";
            this.btnAnoMenos.UseVisualStyleBackColor = true;
            this.btnAnoMenos.Click += new System.EventHandler(this.btnAnoMenos_Click);
            // 
            // btnAnoMais
            // 
            this.btnAnoMais.AccessibleDescription = "Mais";
            this.btnAnoMais.Location = new System.Drawing.Point(130, 60);
            this.btnAnoMais.Name = "btnAnoMais";
            this.btnAnoMais.Size = new System.Drawing.Size(22, 21);
            this.btnAnoMais.TabIndex = 8;
            this.btnAnoMais.Text = "+";
            this.btnAnoMais.UseVisualStyleBackColor = true;
            this.btnAnoMais.Click += new System.EventHandler(this.btnAnoMais_Click);
            // 
            // btnMesMenos
            // 
            this.btnMesMenos.AccessibleDescription = "Menos";
            this.btnMesMenos.Location = new System.Drawing.Point(102, 60);
            this.btnMesMenos.Name = "btnMesMenos";
            this.btnMesMenos.Size = new System.Drawing.Size(22, 21);
            this.btnMesMenos.TabIndex = 7;
            this.btnMesMenos.Text = "-";
            this.btnMesMenos.UseVisualStyleBackColor = true;
            this.btnMesMenos.Click += new System.EventHandler(this.btnMesMenos_Click);
            // 
            // btnMesMais
            // 
            this.btnMesMais.AccessibleDescription = "Mais";
            this.btnMesMais.Location = new System.Drawing.Point(76, 60);
            this.btnMesMais.Name = "btnMesMais";
            this.btnMesMais.Size = new System.Drawing.Size(22, 21);
            this.btnMesMais.TabIndex = 6;
            this.btnMesMais.Text = "+";
            this.btnMesMais.UseVisualStyleBackColor = true;
            this.btnMesMais.Click += new System.EventHandler(this.btnMesMais_Click);
            // 
            // btnEmpresaMenos
            // 
            this.btnEmpresaMenos.AccessibleDescription = "Menos";
            this.btnEmpresaMenos.Location = new System.Drawing.Point(48, 60);
            this.btnEmpresaMenos.Name = "btnEmpresaMenos";
            this.btnEmpresaMenos.Size = new System.Drawing.Size(22, 21);
            this.btnEmpresaMenos.TabIndex = 5;
            this.btnEmpresaMenos.Text = "-";
            this.btnEmpresaMenos.UseVisualStyleBackColor = true;
            this.btnEmpresaMenos.Click += new System.EventHandler(this.btnEmpresaMenos_Click);
            // 
            // btnEmpresaMais
            // 
            this.btnEmpresaMais.AccessibleDescription = "Mai";
            this.btnEmpresaMais.Location = new System.Drawing.Point(22, 60);
            this.btnEmpresaMais.Name = "btnEmpresaMais";
            this.btnEmpresaMais.Size = new System.Drawing.Size(22, 21);
            this.btnEmpresaMais.TabIndex = 4;
            this.btnEmpresaMais.Text = "+";
            this.btnEmpresaMais.UseVisualStyleBackColor = true;
            this.btnEmpresaMais.Click += new System.EventHandler(this.btnEmpresaMais_Click);
            // 
            // tbxAno
            // 
            this.tbxAno.AccessibleDescription = "Ano";
            this.tbxAno.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.tbxAno.Enabled = false;
            this.tbxAno.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbxAno, "ano de competencia");
            this.tbxAno.Location = new System.Drawing.Point(130, 33);
            this.tbxAno.MaxLength = 4;
            this.tbxAno.Name = "tbxAno";
            this.helpProvider1.SetShowHelp(this.tbxAno, true);
            this.tbxAno.Size = new System.Drawing.Size(48, 21);
            this.tbxAno.TabIndex = 3;
            this.tbxAno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxAno.GotFocus += new System.EventHandler(this.FormGotFocus);
            this.tbxAno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.tbxAno.LostFocus += new System.EventHandler(this.FormLostFocus);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(143, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ano";
            // 
            // tbxMes
            // 
            this.tbxMes.AccessibleDescription = "Mês";
            this.tbxMes.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.tbxMes.Enabled = false;
            this.tbxMes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbxMes, "mes de competencia ");
            this.tbxMes.Location = new System.Drawing.Point(76, 33);
            this.tbxMes.MaxLength = 4;
            this.tbxMes.Name = "tbxMes";
            this.helpProvider1.SetShowHelp(this.tbxMes, true);
            this.tbxMes.Size = new System.Drawing.Size(48, 21);
            this.tbxMes.TabIndex = 2;
            this.tbxMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxMes.GotFocus += new System.EventHandler(this.FormGotFocus);
            this.tbxMes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.tbxMes.LostFocus += new System.EventHandler(this.FormLostFocus);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(87, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mês";
            // 
            // tbxEmpresa
            // 
            this.tbxEmpresa.AccessibleDescription = "Empresa";
            this.tbxEmpresa.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.tbxEmpresa.Enabled = false;
            this.tbxEmpresa.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpString(this.tbxEmpresa, "informe a filial que deseja trabalhar");
            this.tbxEmpresa.Location = new System.Drawing.Point(22, 33);
            this.tbxEmpresa.MaxLength = 4;
            this.tbxEmpresa.Name = "tbxEmpresa";
            this.helpProvider1.SetShowHelp(this.tbxEmpresa, true);
            this.tbxEmpresa.Size = new System.Drawing.Size(48, 21);
            this.tbxEmpresa.TabIndex = 1;
            this.tbxEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxEmpresa.GotFocus += new System.EventHandler(this.FormGotFocus);
            this.tbxEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPress);
            this.tbxEmpresa.LostFocus += new System.EventHandler(this.FormLostFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Empresa";
            // 
            // btnOk
            // 
            this.btnOk.AccessibleDescription = "Ok";
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(75, 216);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 47);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&Ok";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AccessibleDescription = "Cancelar";
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(145, 216);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(64, 47);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // helpProvider1
            // 
            this.helpProvider1.HelpNamespace = "Aplisoft.chm";
            // 
            // pbr
            // 
            this.pbr.AccessibleDescription = "Tempo";
            this.pbr.Location = new System.Drawing.Point(12, 200);
            this.pbr.Maximum = 60000;
            this.pbr.Name = "pbr";
            this.pbr.Size = new System.Drawing.Size(200, 10);
            this.pbr.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbr.TabIndex = 4;
            // 
            // tmr
            // 
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // bwrDadosPadrao
            // 
            this.bwrDadosPadrao.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrDadosPadrao_DoWork);
            // 
            // btnConfigConnection
            // 
            this.btnConfigConnection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigConnection.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnConfigConnection.FlatAppearance.BorderSize = 0;
            this.btnConfigConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigConnection.Location = new System.Drawing.Point(0, 263);
            this.btnConfigConnection.Name = "btnConfigConnection";
            this.btnConfigConnection.Size = new System.Drawing.Size(9, 11);
            this.btnConfigConnection.TabIndex = 5;
            this.btnConfigConnection.UseVisualStyleBackColor = true;
            // 
            // lbxOrigem
            // 
            this.lbxOrigem.FormattingEnabled = true;
            this.lbxOrigem.Location = new System.Drawing.Point(11, 228);
            this.lbxOrigem.Name = "lbxOrigem";
            this.lbxOrigem.Size = new System.Drawing.Size(49, 17);
            this.lbxOrigem.TabIndex = 6;
            this.toolTip1.SetToolTip(this.lbxOrigem, "Usado para Importar dados das tabelas (TXT)");
            this.lbxOrigem.Visible = false;
            // 
            // frmSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 275);
            this.Controls.Add(this.lbxOrigem);
            this.Controls.Add(this.btnConfigConnection);
            this.Controls.Add(this.pbr);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbxEmpresa);
            this.Controls.Add(this.gbxUsuario);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSenha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "7";
            this.Text = "Senha  Sistema - Registrando";
            this.toolTip1.SetToolTip(this, "Senha  Sistema - Registrando");
            this.Activated += new System.EventHandler(this.frmSenha_Activated);
            this.Deactivate += new System.EventHandler(this.frmSenha_Deactivate);
            this.Load += new System.EventHandler(this.frmSenha_Load);
            this.Shown += new System.EventHandler(this.frmSenha_Shown);
            this.gbxUsuario.ResumeLayout(false);
            this.gbxUsuario.PerformLayout();
            this.gbxEmpresa.ResumeLayout(false);
            this.gbxEmpresa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxUsuario;
        private System.Windows.Forms.TextBox tbxSenha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxEmpresa;
        private System.Windows.Forms.Button btnAnoMenos;
        private System.Windows.Forms.Button btnAnoMais;
        private System.Windows.Forms.Button btnMesMenos;
        private System.Windows.Forms.Button btnMesMais;
        private System.Windows.Forms.Button btnEmpresaMenos;
        private System.Windows.Forms.Button btnEmpresaMais;
        private System.Windows.Forms.TextBox tbxAno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxMes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxEmpresa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ProgressBar pbr;
        private System.Windows.Forms.Timer tmr;
        private System.ComponentModel.BackgroundWorker bwrDadosPadrao;
        private System.Windows.Forms.Button btnConfigConnection;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListBox lbxOrigem;
    }
}