namespace CRCorrea
{
    partial class frmTab_Bancos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTab_Bancos));
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxCogCnab = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbxHomePage = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxCognome = new System.Windows.Forms.TextBox();
            this.rbnInativo = new System.Windows.Forms.RadioButton();
            this.rbnAtivo = new System.Windows.Forms.RadioButton();
            this.gbxEspecie = new System.Windows.Forms.GroupBox();
            this.btnEspecie = new System.Windows.Forms.Button();
            this.dgvTab_BancosEspecie = new System.Windows.Forms.DataGridView();
            this.gbxMoeda = new System.Windows.Forms.GroupBox();
            this.btnMoeda = new System.Windows.Forms.Button();
            this.dgvTab_BancosMoeda = new System.Windows.Forms.DataGridView();
            this.gbxAceite = new System.Windows.Forms.GroupBox();
            this.btnAceite = new System.Windows.Forms.Button();
            this.dgvTab_BancosAceite = new System.Windows.Forms.DataGridView();
            this.gbxProtesto = new System.Windows.Forms.GroupBox();
            this.btnProtestar = new System.Windows.Forms.Button();
            this.dgvTab_BancosProtestar = new System.Windows.Forms.DataGridView();
            this.gbxImpressao = new System.Windows.Forms.GroupBox();
            this.btnImpressao = new System.Windows.Forms.Button();
            this.dgvTab_BancosImpressao = new System.Windows.Forms.DataGridView();
            this.gbxModalidade = new System.Windows.Forms.GroupBox();
            this.btnModalidade = new System.Windows.Forms.Button();
            this.dgvTab_BancosModalidade = new System.Windows.Forms.DataGridView();
            this.bwrTab_BancosEspecie = new System.ComponentModel.BackgroundWorker();
            this.bwrTab_BancosMoeda = new System.ComponentModel.BackgroundWorker();
            this.bwrTab_BancosProtestar = new System.ComponentModel.BackgroundWorker();
            this.bwrTab_BancosImpressao = new System.ComponentModel.BackgroundWorker();
            this.bwrTab_BancosModalidade = new System.ComponentModel.BackgroundWorker();
            this.gbxEstado = new System.Windows.Forms.GroupBox();
            this.btnEstado = new System.Windows.Forms.Button();
            this.dgvTab_BancosEstado = new System.Windows.Forms.DataGridView();
            this.bwrTab_BancosEstado = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tspTool.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbxEspecie.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosEspecie)).BeginInit();
            this.gbxMoeda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosMoeda)).BeginInit();
            this.gbxAceite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosAceite)).BeginInit();
            this.gbxProtesto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosProtestar)).BeginInit();
            this.gbxImpressao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosImpressao)).BeginInit();
            this.gbxModalidade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosModalidade)).BeginInit();
            this.gbxEstado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosEstado)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(12, 607);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(353, 45);
            this.tspTool.TabIndex = 2;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            this.toolTip1.SetToolTip(this.tspTool, "Barra de COntrole de Bancos");
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspSalvar.Image")));
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(58, 42);
            this.tspSalvar.Text = "&Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspPrimeiro
            // 
            this.tspPrimeiro.AccessibleDescription = "Primeiro";
            this.tspPrimeiro.AutoSize = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = ((System.Drawing.Image)(resources.GetObject("tspPrimeiro.Image")));
            this.tspPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Size = new System.Drawing.Size(58, 42);
            this.tspPrimeiro.Text = "Pr&imeiro";
            this.tspPrimeiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspPrimeiro.Click += new System.EventHandler(this.tspPrimeiro_Click);
            // 
            // tspAnterior
            // 
            this.tspAnterior.AccessibleDescription = "Anterior";
            this.tspAnterior.AutoSize = false;
            this.tspAnterior.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAnterior.Image = ((System.Drawing.Image)(resources.GetObject("tspAnterior.Image")));
            this.tspAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAnterior.Name = "tspAnterior";
            this.tspAnterior.Size = new System.Drawing.Size(58, 42);
            this.tspAnterior.Text = "&Anterior";
            this.tspAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAnterior.Click += new System.EventHandler(this.tspAnterior_Click);
            // 
            // tspProximo
            // 
            this.tspProximo.AccessibleDescription = "Próximo";
            this.tspProximo.AutoSize = false;
            this.tspProximo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspProximo.Image = ((System.Drawing.Image)(resources.GetObject("tspProximo.Image")));
            this.tspProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspProximo.Name = "tspProximo";
            this.tspProximo.Size = new System.Drawing.Size(58, 42);
            this.tspProximo.Text = "Pró&ximo";
            this.tspProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspProximo.Click += new System.EventHandler(this.tspProximo_Click);
            // 
            // tspUltimo
            // 
            this.tspUltimo.AccessibleDescription = "Último";
            this.tspUltimo.AutoSize = false;
            this.tspUltimo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUltimo.Image = ((System.Drawing.Image)(resources.GetObject("tspUltimo.Image")));
            this.tspUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Size = new System.Drawing.Size(58, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspUltimo.Click += new System.EventHandler(this.tspUltimo_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(62, 41);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.AccessibleDescription = "Dados";
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.tbxCogCnab);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Controls.Add(this.tbxHomePage);
            this.groupBox7.Location = new System.Drawing.Point(85, 49);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(418, 64);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox7, "Site");
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 43);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 13);
            this.label19.TabIndex = 5;
            this.label19.Text = "CNAB:";
            // 
            // tbxCogCnab
            // 
            this.tbxCogCnab.AccessibleDescription = "CNBA";
            this.tbxCogCnab.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCogCnab.Location = new System.Drawing.Point(50, 40);
            this.tbxCogCnab.Name = "tbxCogCnab";
            this.tbxCogCnab.Size = new System.Drawing.Size(362, 21);
            this.tbxCogCnab.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbxCogCnab, "CNAB");
            this.tbxCogCnab.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCogCnab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCogCnab.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "Site:";
            // 
            // tbxHomePage
            // 
            this.tbxHomePage.AccessibleDescription = "Site";
            this.tbxHomePage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxHomePage.Location = new System.Drawing.Point(50, 13);
            this.tbxHomePage.Name = "tbxHomePage";
            this.tbxHomePage.Size = new System.Drawing.Size(362, 21);
            this.tbxHomePage.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxHomePage, "Site");
            this.tbxHomePage.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxHomePage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxHomePage.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "Dados Banco";
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbxCodigo);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.tbxNome);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbxCognome);
            this.groupBox2.Location = new System.Drawing.Point(12, -2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(491, 50);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, "Dados Basico banco");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nome:";
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.AccessibleDescription = "N° Banco";
            this.tbxCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Location = new System.Drawing.Point(6, 23);
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(61, 21);
            this.tbxCodigo.TabIndex = 0;
            this.tbxCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxCodigo, "Codigo do Banco");
            this.tbxCodigo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 2);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Nº Banco:";
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Nome Banco";
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(250, 23);
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(235, 21);
            this.tbxNome.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tbxNome, "NOme do banco");
            this.tbxNome.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cognome:";
            // 
            // tbxCognome
            // 
            this.tbxCognome.AccessibleDescription = "Cognome";
            this.tbxCognome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCognome.Location = new System.Drawing.Point(73, 23);
            this.tbxCognome.Name = "tbxCognome";
            this.tbxCognome.Size = new System.Drawing.Size(171, 21);
            this.tbxCognome.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbxCognome, "Apelido do Banco");
            this.tbxCognome.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCognome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCognome.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnInativo
            // 
            this.rbnInativo.AccessibleDescription = "Inativo";
            this.rbnInativo.AutoSize = true;
            this.rbnInativo.Checked = true;
            this.rbnInativo.Location = new System.Drawing.Point(6, 33);
            this.rbnInativo.Name = "rbnInativo";
            this.rbnInativo.Size = new System.Drawing.Size(44, 17);
            this.rbnInativo.TabIndex = 3;
            this.rbnInativo.TabStop = true;
            this.rbnInativo.Text = "Não";
            this.toolTip1.SetToolTip(this.rbnInativo, "Inativo");
            this.rbnInativo.UseVisualStyleBackColor = true;
            // 
            // rbnAtivo
            // 
            this.rbnAtivo.AccessibleDescription = "Ativo";
            this.rbnAtivo.AutoSize = true;
            this.rbnAtivo.Location = new System.Drawing.Point(6, 15);
            this.rbnAtivo.Name = "rbnAtivo";
            this.rbnAtivo.Size = new System.Drawing.Size(41, 17);
            this.rbnAtivo.TabIndex = 2;
            this.rbnAtivo.Text = "Sim";
            this.toolTip1.SetToolTip(this.rbnAtivo, "Ativo");
            this.rbnAtivo.UseVisualStyleBackColor = true;
            // 
            // gbxEspecie
            // 
            this.gbxEspecie.AccessibleDescription = "Tipo Documento/Espécie";
            this.gbxEspecie.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.gbxEspecie.Controls.Add(this.btnEspecie);
            this.gbxEspecie.Controls.Add(this.dgvTab_BancosEspecie);
            this.gbxEspecie.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxEspecie.Location = new System.Drawing.Point(12, 114);
            this.gbxEspecie.Name = "gbxEspecie";
            this.gbxEspecie.Size = new System.Drawing.Size(491, 162);
            this.gbxEspecie.TabIndex = 3;
            this.gbxEspecie.TabStop = false;
            this.gbxEspecie.Text = "Tipo Documento/Espécie";
            this.toolTip1.SetToolTip(this.gbxEspecie, "Tipo Documento/Especie");
            // 
            // btnEspecie
            // 
            this.btnEspecie.AccessibleDescription = "Incluir / Alterar Tipos de Documento";
            this.btnEspecie.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnEspecie.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEspecie.Location = new System.Drawing.Point(7, 130);
            this.btnEspecie.Name = "btnEspecie";
            this.btnEspecie.Size = new System.Drawing.Size(477, 29);
            this.btnEspecie.TabIndex = 1;
            this.btnEspecie.Text = "Incluir / Alterar Tipos de Documento";
            this.toolTip1.SetToolTip(this.btnEspecie, "Incluir / Alterar Tipos de Documento");
            this.btnEspecie.UseVisualStyleBackColor = false;
            this.btnEspecie.Click += new System.EventHandler(this.btnEspecie_Click);
            // 
            // dgvTab_BancosEspecie
            // 
            this.dgvTab_BancosEspecie.AccessibleDescription = "Documentos";
            this.dgvTab_BancosEspecie.AllowUserToAddRows = false;
            this.dgvTab_BancosEspecie.AllowUserToDeleteRows = false;
            this.dgvTab_BancosEspecie.AllowUserToOrderColumns = true;
            this.dgvTab_BancosEspecie.AllowUserToResizeColumns = false;
            this.dgvTab_BancosEspecie.AllowUserToResizeRows = false;
            this.dgvTab_BancosEspecie.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_BancosEspecie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTab_BancosEspecie.Location = new System.Drawing.Point(6, 17);
            this.dgvTab_BancosEspecie.MultiSelect = false;
            this.dgvTab_BancosEspecie.Name = "dgvTab_BancosEspecie";
            this.dgvTab_BancosEspecie.ReadOnly = true;
            this.dgvTab_BancosEspecie.RowHeadersVisible = false;
            this.dgvTab_BancosEspecie.Size = new System.Drawing.Size(479, 109);
            this.dgvTab_BancosEspecie.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dgvTab_BancosEspecie, "Grade Bancos Especie");
            this.dgvTab_BancosEspecie.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_BancosEspecie_CellEnter);
            // 
            // gbxMoeda
            // 
            this.gbxMoeda.AccessibleDescription = "Tipo Moeda";
            this.gbxMoeda.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.gbxMoeda.Controls.Add(this.btnMoeda);
            this.gbxMoeda.Controls.Add(this.dgvTab_BancosMoeda);
            this.gbxMoeda.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxMoeda.Location = new System.Drawing.Point(12, 277);
            this.gbxMoeda.Name = "gbxMoeda";
            this.gbxMoeda.Size = new System.Drawing.Size(491, 170);
            this.gbxMoeda.TabIndex = 4;
            this.gbxMoeda.TabStop = false;
            this.gbxMoeda.Text = "Tipo Moeda";
            this.toolTip1.SetToolTip(this.gbxMoeda, "Tipo Moeda");
            // 
            // btnMoeda
            // 
            this.btnMoeda.AccessibleDescription = "Incluir / Alterar Tipos de Moeda";
            this.btnMoeda.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoeda.Location = new System.Drawing.Point(8, 130);
            this.btnMoeda.Name = "btnMoeda";
            this.btnMoeda.Size = new System.Drawing.Size(477, 29);
            this.btnMoeda.TabIndex = 3;
            this.btnMoeda.Text = "Incluir / Alterar Tipos de Moeda";
            this.toolTip1.SetToolTip(this.btnMoeda, "Incluir / Alterar Tipos de Moeda");
            this.btnMoeda.UseVisualStyleBackColor = true;
            this.btnMoeda.Click += new System.EventHandler(this.btnMoeda_Click);
            // 
            // dgvTab_BancosMoeda
            // 
            this.dgvTab_BancosMoeda.AccessibleDescription = "Moeda";
            this.dgvTab_BancosMoeda.AllowUserToAddRows = false;
            this.dgvTab_BancosMoeda.AllowUserToDeleteRows = false;
            this.dgvTab_BancosMoeda.AllowUserToOrderColumns = true;
            this.dgvTab_BancosMoeda.AllowUserToResizeColumns = false;
            this.dgvTab_BancosMoeda.AllowUserToResizeRows = false;
            this.dgvTab_BancosMoeda.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_BancosMoeda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTab_BancosMoeda.Location = new System.Drawing.Point(6, 17);
            this.dgvTab_BancosMoeda.MultiSelect = false;
            this.dgvTab_BancosMoeda.Name = "dgvTab_BancosMoeda";
            this.dgvTab_BancosMoeda.ReadOnly = true;
            this.dgvTab_BancosMoeda.RowHeadersVisible = false;
            this.dgvTab_BancosMoeda.Size = new System.Drawing.Size(479, 109);
            this.dgvTab_BancosMoeda.TabIndex = 2;
            this.toolTip1.SetToolTip(this.dgvTab_BancosMoeda, "Tipo da Moeda");
            this.dgvTab_BancosMoeda.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_BancosMoeda_CellEnter);
            // 
            // gbxAceite
            // 
            this.gbxAceite.AccessibleDescription = "Tipo Aceite";
            this.gbxAceite.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.gbxAceite.Controls.Add(this.btnAceite);
            this.gbxAceite.Controls.Add(this.dgvTab_BancosAceite);
            this.gbxAceite.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxAceite.Location = new System.Drawing.Point(12, 444);
            this.gbxAceite.Name = "gbxAceite";
            this.gbxAceite.Size = new System.Drawing.Size(491, 162);
            this.gbxAceite.TabIndex = 5;
            this.gbxAceite.TabStop = false;
            this.gbxAceite.Text = "Tipo Aceite";
            this.toolTip1.SetToolTip(this.gbxAceite, "Tipo Aceite");
            // 
            // btnAceite
            // 
            this.btnAceite.AccessibleDescription = "Incluir / Alterar Tipos de Aceite";
            this.btnAceite.Location = new System.Drawing.Point(6, 131);
            this.btnAceite.Name = "btnAceite";
            this.btnAceite.Size = new System.Drawing.Size(477, 29);
            this.btnAceite.TabIndex = 3;
            this.btnAceite.Text = "Incluir / Alterar Tipos de Aceite";
            this.toolTip1.SetToolTip(this.btnAceite, "Incluir / Alterar Tipos de Aceite");
            this.btnAceite.UseVisualStyleBackColor = true;
            this.btnAceite.Click += new System.EventHandler(this.btnAceite_Click);
            // 
            // dgvTab_BancosAceite
            // 
            this.dgvTab_BancosAceite.AccessibleDescription = "Aceites";
            this.dgvTab_BancosAceite.AllowUserToAddRows = false;
            this.dgvTab_BancosAceite.AllowUserToDeleteRows = false;
            this.dgvTab_BancosAceite.AllowUserToOrderColumns = true;
            this.dgvTab_BancosAceite.AllowUserToResizeColumns = false;
            this.dgvTab_BancosAceite.AllowUserToResizeRows = false;
            this.dgvTab_BancosAceite.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_BancosAceite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTab_BancosAceite.Location = new System.Drawing.Point(6, 16);
            this.dgvTab_BancosAceite.MultiSelect = false;
            this.dgvTab_BancosAceite.Name = "dgvTab_BancosAceite";
            this.dgvTab_BancosAceite.ReadOnly = true;
            this.dgvTab_BancosAceite.RowHeadersVisible = false;
            this.dgvTab_BancosAceite.Size = new System.Drawing.Size(479, 109);
            this.dgvTab_BancosAceite.TabIndex = 2;
            this.toolTip1.SetToolTip(this.dgvTab_BancosAceite, "Tipo Aceite");
            this.dgvTab_BancosAceite.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_BancosAceite_CellEnter);
            // 
            // gbxProtesto
            // 
            this.gbxProtesto.AccessibleDescription = "Tipo Protesto";
            this.gbxProtesto.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.gbxProtesto.Controls.Add(this.btnProtestar);
            this.gbxProtesto.Controls.Add(this.dgvTab_BancosProtestar);
            this.gbxProtesto.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxProtesto.Location = new System.Drawing.Point(509, 148);
            this.gbxProtesto.Name = "gbxProtesto";
            this.gbxProtesto.Size = new System.Drawing.Size(491, 147);
            this.gbxProtesto.TabIndex = 6;
            this.gbxProtesto.TabStop = false;
            this.gbxProtesto.Text = "Tipo Protesto";
            this.toolTip1.SetToolTip(this.gbxProtesto, "Tipo Protesto");
            // 
            // btnProtestar
            // 
            this.btnProtestar.AccessibleDescription = "Incluir / Alterar Tipos de Protesto";
            this.btnProtestar.Location = new System.Drawing.Point(6, 125);
            this.btnProtestar.Name = "btnProtestar";
            this.btnProtestar.Size = new System.Drawing.Size(477, 21);
            this.btnProtestar.TabIndex = 3;
            this.btnProtestar.Text = "Incluir / Alterar Tipos de Protesto";
            this.toolTip1.SetToolTip(this.btnProtestar, "Incluir / Alterar Tipos de Protesto");
            this.btnProtestar.UseVisualStyleBackColor = true;
            this.btnProtestar.Click += new System.EventHandler(this.btnProtestar_Click);
            // 
            // dgvTab_BancosProtestar
            // 
            this.dgvTab_BancosProtestar.AccessibleDescription = "Protestos";
            this.dgvTab_BancosProtestar.AllowUserToAddRows = false;
            this.dgvTab_BancosProtestar.AllowUserToDeleteRows = false;
            this.dgvTab_BancosProtestar.AllowUserToOrderColumns = true;
            this.dgvTab_BancosProtestar.AllowUserToResizeColumns = false;
            this.dgvTab_BancosProtestar.AllowUserToResizeRows = false;
            this.dgvTab_BancosProtestar.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_BancosProtestar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTab_BancosProtestar.Location = new System.Drawing.Point(6, 14);
            this.dgvTab_BancosProtestar.MultiSelect = false;
            this.dgvTab_BancosProtestar.Name = "dgvTab_BancosProtestar";
            this.dgvTab_BancosProtestar.ReadOnly = true;
            this.dgvTab_BancosProtestar.RowHeadersVisible = false;
            this.dgvTab_BancosProtestar.Size = new System.Drawing.Size(479, 109);
            this.dgvTab_BancosProtestar.TabIndex = 4;
            this.toolTip1.SetToolTip(this.dgvTab_BancosProtestar, "Grade Bancos Protesto");
            this.dgvTab_BancosProtestar.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_BancosProtestar_CellEnter);
            // 
            // gbxImpressao
            // 
            this.gbxImpressao.AccessibleDescription = "Tipo Impressão";
            this.gbxImpressao.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.gbxImpressao.Controls.Add(this.btnImpressao);
            this.gbxImpressao.Controls.Add(this.dgvTab_BancosImpressao);
            this.gbxImpressao.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxImpressao.Location = new System.Drawing.Point(509, 297);
            this.gbxImpressao.Name = "gbxImpressao";
            this.gbxImpressao.Size = new System.Drawing.Size(491, 150);
            this.gbxImpressao.TabIndex = 7;
            this.gbxImpressao.TabStop = false;
            this.gbxImpressao.Text = "Tipo Impressão";
            this.toolTip1.SetToolTip(this.gbxImpressao, "Tipo Impressão");
            // 
            // btnImpressao
            // 
            this.btnImpressao.AccessibleDescription = "Incluir / Alterar de Impressão";
            this.btnImpressao.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImpressao.Location = new System.Drawing.Point(6, 125);
            this.btnImpressao.Name = "btnImpressao";
            this.btnImpressao.Size = new System.Drawing.Size(477, 21);
            this.btnImpressao.TabIndex = 3;
            this.btnImpressao.Text = "Incluir / Alterar de Impressão";
            this.toolTip1.SetToolTip(this.btnImpressao, "Incluir / Alterar de Impressão");
            this.btnImpressao.UseVisualStyleBackColor = true;
            this.btnImpressao.Click += new System.EventHandler(this.btnImpressao_Click);
            // 
            // dgvTab_BancosImpressao
            // 
            this.dgvTab_BancosImpressao.AccessibleDescription = "Impressão";
            this.dgvTab_BancosImpressao.AllowUserToAddRows = false;
            this.dgvTab_BancosImpressao.AllowUserToDeleteRows = false;
            this.dgvTab_BancosImpressao.AllowUserToOrderColumns = true;
            this.dgvTab_BancosImpressao.AllowUserToResizeColumns = false;
            this.dgvTab_BancosImpressao.AllowUserToResizeRows = false;
            this.dgvTab_BancosImpressao.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_BancosImpressao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTab_BancosImpressao.Location = new System.Drawing.Point(6, 15);
            this.dgvTab_BancosImpressao.MultiSelect = false;
            this.dgvTab_BancosImpressao.Name = "dgvTab_BancosImpressao";
            this.dgvTab_BancosImpressao.ReadOnly = true;
            this.dgvTab_BancosImpressao.RowHeadersVisible = false;
            this.dgvTab_BancosImpressao.Size = new System.Drawing.Size(479, 109);
            this.dgvTab_BancosImpressao.TabIndex = 2;
            this.toolTip1.SetToolTip(this.dgvTab_BancosImpressao, "Tipo Impressão");
            this.dgvTab_BancosImpressao.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_BancosImpressao_CellEnter);
            // 
            // gbxModalidade
            // 
            this.gbxModalidade.AccessibleDescription = "Tipo Modalidade";
            this.gbxModalidade.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.gbxModalidade.Controls.Add(this.btnModalidade);
            this.gbxModalidade.Controls.Add(this.dgvTab_BancosModalidade);
            this.gbxModalidade.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxModalidade.Location = new System.Drawing.Point(509, 447);
            this.gbxModalidade.Name = "gbxModalidade";
            this.gbxModalidade.Size = new System.Drawing.Size(491, 150);
            this.gbxModalidade.TabIndex = 8;
            this.gbxModalidade.TabStop = false;
            this.gbxModalidade.Text = "Tipo Modalidade";
            this.toolTip1.SetToolTip(this.gbxModalidade, "Tipo Modalidade");
            // 
            // btnModalidade
            // 
            this.btnModalidade.AccessibleDescription = "Incluir / Alterar Tipos Modalidade";
            this.btnModalidade.Location = new System.Drawing.Point(7, 124);
            this.btnModalidade.Name = "btnModalidade";
            this.btnModalidade.Size = new System.Drawing.Size(477, 21);
            this.btnModalidade.TabIndex = 3;
            this.btnModalidade.Text = "Incluir / Alterar Tipos Modalidade";
            this.toolTip1.SetToolTip(this.btnModalidade, "Incluir / Alterar Tipos Modalidade");
            this.btnModalidade.UseVisualStyleBackColor = true;
            this.btnModalidade.Click += new System.EventHandler(this.btnModalidade_Click);
            // 
            // dgvTab_BancosModalidade
            // 
            this.dgvTab_BancosModalidade.AccessibleDescription = "Modalidades";
            this.dgvTab_BancosModalidade.AllowUserToAddRows = false;
            this.dgvTab_BancosModalidade.AllowUserToDeleteRows = false;
            this.dgvTab_BancosModalidade.AllowUserToOrderColumns = true;
            this.dgvTab_BancosModalidade.AllowUserToResizeColumns = false;
            this.dgvTab_BancosModalidade.AllowUserToResizeRows = false;
            this.dgvTab_BancosModalidade.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_BancosModalidade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTab_BancosModalidade.Location = new System.Drawing.Point(6, 14);
            this.dgvTab_BancosModalidade.MultiSelect = false;
            this.dgvTab_BancosModalidade.Name = "dgvTab_BancosModalidade";
            this.dgvTab_BancosModalidade.ReadOnly = true;
            this.dgvTab_BancosModalidade.RowHeadersVisible = false;
            this.dgvTab_BancosModalidade.Size = new System.Drawing.Size(479, 109);
            this.dgvTab_BancosModalidade.TabIndex = 2;
            this.toolTip1.SetToolTip(this.dgvTab_BancosModalidade, "Tipo Modalidade");
            this.dgvTab_BancosModalidade.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_BancosModalidade_CellEnter);
            // 
            // bwrTab_BancosEspecie
            // 
            this.bwrTab_BancosEspecie.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrTab_BancosEspecie_DoWork);
            this.bwrTab_BancosEspecie.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrTab_BancosEspecie_RunWorkerCompleted);
            // 
            // bwrTab_BancosMoeda
            // 
            this.bwrTab_BancosMoeda.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrTab_BancosMoeda_DoWork);
            this.bwrTab_BancosMoeda.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrTab_BancosMoeda_RunWorkerCompleted);
            // 
            // bwrTab_BancosProtestar
            // 
            this.bwrTab_BancosProtestar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrTab_BancosProtestar_DoWork);
            this.bwrTab_BancosProtestar.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrTab_BancosProtestar_RunWorkerCompleted);
            // 
            // bwrTab_BancosImpressao
            // 
            this.bwrTab_BancosImpressao.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrTab_BancosImpressao_DoWork);
            this.bwrTab_BancosImpressao.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrTab_BancosImpressao_RunWorkerCompleted);
            // 
            // bwrTab_BancosModalidade
            // 
            this.bwrTab_BancosModalidade.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrTab_BancosModalidade_DoWork);
            this.bwrTab_BancosModalidade.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrTab_BancosModalidade_RunWorkerCompleted);
            // 
            // gbxEstado
            // 
            this.gbxEstado.AccessibleDescription = "Codigos de Relacionamento/Estado ";
            this.gbxEstado.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.gbxEstado.Controls.Add(this.btnEstado);
            this.gbxEstado.Controls.Add(this.dgvTab_BancosEstado);
            this.gbxEstado.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxEstado.Location = new System.Drawing.Point(509, 3);
            this.gbxEstado.Name = "gbxEstado";
            this.gbxEstado.Size = new System.Drawing.Size(491, 144);
            this.gbxEstado.TabIndex = 10;
            this.gbxEstado.TabStop = false;
            this.gbxEstado.Text = "Códigos de Relacionamento/Estado ";
            this.toolTip1.SetToolTip(this.gbxEstado, "Codigos de Relacionamento/Estado ");
            // 
            // btnEstado
            // 
            this.btnEstado.AccessibleDescription = "Incluir / Alterar Tipos de Documento";
            this.btnEstado.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnEstado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstado.Location = new System.Drawing.Point(8, 125);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(477, 21);
            this.btnEstado.TabIndex = 5;
            this.btnEstado.Text = "Incluir / Alterar Tipos de Documento";
            this.toolTip1.SetToolTip(this.btnEstado, "Incluir / Alterar Tipos de Documento");
            this.btnEstado.UseVisualStyleBackColor = false;
            this.btnEstado.Click += new System.EventHandler(this.btnEstado_Click);
            // 
            // dgvTab_BancosEstado
            // 
            this.dgvTab_BancosEstado.AccessibleDescription = "Códigos de Relacionamento";
            this.dgvTab_BancosEstado.AllowUserToAddRows = false;
            this.dgvTab_BancosEstado.AllowUserToDeleteRows = false;
            this.dgvTab_BancosEstado.AllowUserToOrderColumns = true;
            this.dgvTab_BancosEstado.AllowUserToResizeColumns = false;
            this.dgvTab_BancosEstado.AllowUserToResizeRows = false;
            this.dgvTab_BancosEstado.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_BancosEstado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTab_BancosEstado.Location = new System.Drawing.Point(6, 15);
            this.dgvTab_BancosEstado.MultiSelect = false;
            this.dgvTab_BancosEstado.Name = "dgvTab_BancosEstado";
            this.dgvTab_BancosEstado.ReadOnly = true;
            this.dgvTab_BancosEstado.RowHeadersVisible = false;
            this.dgvTab_BancosEstado.Size = new System.Drawing.Size(479, 109);
            this.dgvTab_BancosEstado.TabIndex = 4;
            this.toolTip1.SetToolTip(this.dgvTab_BancosEstado, "Codigos de Relacionamento/Estado ");
            this.dgvTab_BancosEstado.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_BancosEstado_CellEnter);
            // 
            // bwrTab_BancosEstado
            // 
            this.bwrTab_BancosEstado.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrTab_BancosEstado_DoWork);
            this.bwrTab_BancosEstado.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrTab_BancosEstado_RunWorkerCompleted);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.rbnAtivo);
            this.groupBox10.Controls.Add(this.rbnInativo);
            this.groupBox10.Location = new System.Drawing.Point(12, 49);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(67, 61);
            this.groupBox10.TabIndex = 10;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Ativo";
            // 
            // frmTab_Bancos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.gbxEstado);
            this.Controls.Add(this.gbxModalidade);
            this.Controls.Add(this.gbxImpressao);
            this.Controls.Add(this.gbxProtesto);
            this.Controls.Add(this.gbxAceite);
            this.Controls.Add(this.gbxMoeda);
            this.Controls.Add(this.gbxEspecie);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tspTool);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTab_Bancos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "201";
            this.Text = "Registrando Banco  - Cadastro";
            this.Activated += new System.EventHandler(this.frmTab_Bancos_Activated);
            this.Load += new System.EventHandler(this.frmTab_Bancos_Load);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbxEspecie.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosEspecie)).EndInit();
            this.gbxMoeda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosMoeda)).EndInit();
            this.gbxAceite.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosAceite)).EndInit();
            this.gbxProtesto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosProtestar)).EndInit();
            this.gbxImpressao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosImpressao)).EndInit();
            this.gbxModalidade.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosModalidade)).EndInit();
            this.gbxEstado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_BancosEstado)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbxCogCnab;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbxHomePage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxCognome;
        private System.Windows.Forms.GroupBox gbxEspecie;
        private System.Windows.Forms.GroupBox gbxMoeda;
        private System.Windows.Forms.GroupBox gbxAceite;
        private System.Windows.Forms.GroupBox gbxProtesto;
        private System.Windows.Forms.GroupBox gbxImpressao;
        private System.Windows.Forms.GroupBox gbxModalidade;
        private System.Windows.Forms.Button btnEspecie;
        private System.Windows.Forms.DataGridView dgvTab_BancosEspecie;
        private System.Windows.Forms.Button btnMoeda;
        private System.Windows.Forms.DataGridView dgvTab_BancosMoeda;
        private System.Windows.Forms.Button btnAceite;
        private System.Windows.Forms.DataGridView dgvTab_BancosAceite;
        private System.Windows.Forms.Button btnProtestar;
        private System.Windows.Forms.Button btnImpressao;
        private System.Windows.Forms.DataGridView dgvTab_BancosImpressao;
        private System.Windows.Forms.Button btnModalidade;
        private System.Windows.Forms.DataGridView dgvTab_BancosModalidade;
        private System.ComponentModel.BackgroundWorker bwrTab_BancosAceite;
        private System.ComponentModel.BackgroundWorker bwrTab_BancosProtestar;
        private System.ComponentModel.BackgroundWorker bwrTab_BancosImpressao;
        private System.ComponentModel.BackgroundWorker bwrTab_BancosModalidade;
        private System.Windows.Forms.GroupBox gbxEstado;
        private System.Windows.Forms.RadioButton rbnInativo;
        private System.Windows.Forms.RadioButton rbnAtivo;
        private System.Windows.Forms.DataGridView dgvTab_BancosProtestar;
        private System.Windows.Forms.DataGridView dgvTab_BancosEstado;
        private System.ComponentModel.BackgroundWorker bwrTab_BancosEstado;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEstado;
    }
}