namespace CRCorrea
{
    partial class frmTab_CNAE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTab_CNAE));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ttpTab_CNAE = new System.Windows.Forms.ToolTip(this.components);
            this.tspTab_CNAE = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxDadosCNAE = new System.Windows.Forms.GroupBox();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxLinhas = new System.Windows.Forms.GroupBox();
            this.pbxTab_CNAE_UF = new System.Windows.Forms.PictureBox();
            this.tspTab_CNAE_UF = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.dgvTab_CNAE_UF = new System.Windows.Forms.DataGridView();
            this.bwrTab_CNAE_UF = new System.ComponentModel.BackgroundWorker();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDTABCNAE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDUF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDDIZERESNFV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ICMSST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tspTab_CNAE.SuspendLayout();
            this.gbxDadosCNAE.SuspendLayout();
            this.gbxLinhas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTab_CNAE_UF)).BeginInit();
            this.tspTab_CNAE_UF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_CNAE_UF)).BeginInit();
            this.SuspendLayout();
            // 
            // tspTab_CNAE
            // 
            this.tspTab_CNAE.AccessibleDescription = "Bara de Opções";
            this.tspTab_CNAE.AutoSize = false;
            this.tspTab_CNAE.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTab_CNAE.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTab_CNAE.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTab_CNAE.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspRetornar});
            this.tspTab_CNAE.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTab_CNAE.Location = new System.Drawing.Point(12, 636);
            this.tspTab_CNAE.Name = "tspTab_CNAE";
            this.tspTab_CNAE.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTab_CNAE.Size = new System.Drawing.Size(1003, 44);
            this.tspTab_CNAE.TabIndex = 2;
            this.tspTab_CNAE.TabStop = true;
            this.tspTab_CNAE.Text = "toolStrip1";
            this.ttpTab_CNAE.SetToolTip(this.tspTab_CNAE, "Menu1");
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspSalvar.Image")));
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(48, 42);
            this.tspSalvar.Text = "Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspPrimeiro
            // 
            this.tspPrimeiro.AccessibleDescription = "Primeiro";
            this.tspPrimeiro.AutoSize = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = ((System.Drawing.Image)(resources.GetObject("tspPrimeiro.Image")));
            this.tspPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Size = new System.Drawing.Size(48, 42);
            this.tspPrimeiro.Text = "Pr&imeiro";
            this.tspPrimeiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspPrimeiro.Click += new System.EventHandler(this.tspPrimeiro_Click);
            // 
            // tspAnterior
            // 
            this.tspAnterior.AccessibleDescription = "Anterior";
            this.tspAnterior.AutoSize = false;
            this.tspAnterior.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAnterior.Image = ((System.Drawing.Image)(resources.GetObject("tspAnterior.Image")));
            this.tspAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAnterior.Name = "tspAnterior";
            this.tspAnterior.Size = new System.Drawing.Size(48, 42);
            this.tspAnterior.Text = "&Anterior";
            this.tspAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAnterior.Click += new System.EventHandler(this.tspAnterior_Click);
            // 
            // tspProximo
            // 
            this.tspProximo.AccessibleDescription = "Próximo";
            this.tspProximo.AutoSize = false;
            this.tspProximo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspProximo.Image = ((System.Drawing.Image)(resources.GetObject("tspProximo.Image")));
            this.tspProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspProximo.Name = "tspProximo";
            this.tspProximo.Size = new System.Drawing.Size(48, 42);
            this.tspProximo.Text = "Pró&ximo";
            this.tspProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspProximo.Click += new System.EventHandler(this.tspProximo_Click);
            // 
            // tspUltimo
            // 
            this.tspUltimo.AccessibleDescription = "Último";
            this.tspUltimo.AutoSize = false;
            this.tspUltimo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUltimo.Image = ((System.Drawing.Image)(resources.GetObject("tspUltimo.Image")));
            this.tspUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Size = new System.Drawing.Size(48, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspUltimo.Click += new System.EventHandler(this.tspUltimo_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(54, 41);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // gbxDadosCNAE
            // 
            this.gbxDadosCNAE.AccessibleDescription = "Dados do CNAE";
            this.gbxDadosCNAE.Controls.Add(this.tbxNome);
            this.gbxDadosCNAE.Controls.Add(this.label4);
            this.gbxDadosCNAE.Controls.Add(this.tbxCodigo);
            this.gbxDadosCNAE.Controls.Add(this.label1);
            this.gbxDadosCNAE.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDadosCNAE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbxDadosCNAE.Location = new System.Drawing.Point(322, 12);
            this.gbxDadosCNAE.Name = "gbxDadosCNAE";
            this.gbxDadosCNAE.Size = new System.Drawing.Size(389, 119);
            this.gbxDadosCNAE.TabIndex = 0;
            this.gbxDadosCNAE.TabStop = false;
            this.gbxDadosCNAE.Text = "Dados do CNAE";
            this.ttpTab_CNAE.SetToolTip(this.gbxDadosCNAE, "Grupo CNAE");
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Nome";
            this.tbxNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(10, 81);
            this.tbxNome.MaxLength = 50;
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(369, 21);
            this.tbxNome.TabIndex = 1;
            this.ttpTab_CNAE.SetToolTip(this.tbxNome, "Nome");
            this.tbxNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxNome.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(7, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "Nome";
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.AccessibleDescription = "Código";
            this.tbxCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Location = new System.Drawing.Point(10, 33);
            this.tbxCodigo.MaxLength = 20;
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(152, 21);
            this.tbxCodigo.TabIndex = 0;
            this.ttpTab_CNAE.SetToolTip(this.tbxCodigo, "Codigo");
            this.tbxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigo.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxCodigo.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Código";
            // 
            // gbxLinhas
            // 
            this.gbxLinhas.AccessibleDescription = "CNAE UF";
            this.gbxLinhas.Controls.Add(this.pbxTab_CNAE_UF);
            this.gbxLinhas.Controls.Add(this.tspTab_CNAE_UF);
            this.gbxLinhas.Controls.Add(this.dgvTab_CNAE_UF);
            this.gbxLinhas.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxLinhas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbxLinhas.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gbxLinhas.Location = new System.Drawing.Point(391, 137);
            this.gbxLinhas.Name = "gbxLinhas";
            this.gbxLinhas.Size = new System.Drawing.Size(250, 492);
            this.gbxLinhas.TabIndex = 1;
            this.gbxLinhas.TabStop = false;
            this.gbxLinhas.Text = "CNAE UF";
            this.ttpTab_CNAE.SetToolTip(this.gbxLinhas, "Grupo CNAE UF");
            // 
            // pbxTab_CNAE_UF
            // 
            this.pbxTab_CNAE_UF.AccessibleDescription = "Carregando Cnae UF";
            this.pbxTab_CNAE_UF.BackColor = System.Drawing.Color.White;
            this.pbxTab_CNAE_UF.Image = ((System.Drawing.Image)(resources.GetObject("pbxTab_CNAE_UF.Image")));
            this.pbxTab_CNAE_UF.Location = new System.Drawing.Point(20, 21);
            this.pbxTab_CNAE_UF.Name = "pbxTab_CNAE_UF";
            this.pbxTab_CNAE_UF.Size = new System.Drawing.Size(210, 423);
            this.pbxTab_CNAE_UF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxTab_CNAE_UF.TabIndex = 139;
            this.pbxTab_CNAE_UF.TabStop = false;
            this.ttpTab_CNAE.SetToolTip(this.pbxTab_CNAE_UF, "Imagem");
            this.pbxTab_CNAE_UF.Visible = false;
            // 
            // tspTab_CNAE_UF
            // 
            this.tspTab_CNAE_UF.AccessibleDescription = "Barra de Opções";
            this.tspTab_CNAE_UF.AllowMerge = false;
            this.tspTab_CNAE_UF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTab_CNAE_UF.AutoSize = false;
            this.tspTab_CNAE_UF.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTab_CNAE_UF.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspExcluir});
            this.tspTab_CNAE_UF.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTab_CNAE_UF.Location = new System.Drawing.Point(53, 453);
            this.tspTab_CNAE_UF.Name = "tspTab_CNAE_UF";
            this.tspTab_CNAE_UF.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTab_CNAE_UF.Size = new System.Drawing.Size(144, 35);
            this.tspTab_CNAE_UF.TabIndex = 1;
            this.ttpTab_CNAE.SetToolTip(this.tspTab_CNAE_UF, "Menu2");
            // 
            // tspIncluir
            // 
            this.tspIncluir.AccessibleDescription = "Incluir";
            this.tspIncluir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspIncluir.ForeColor = System.Drawing.Color.Black;
            this.tspIncluir.Image = ((System.Drawing.Image)(resources.GetObject("tspIncluir.Image")));
            this.tspIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspIncluir.Name = "tspIncluir";
            this.tspIncluir.Size = new System.Drawing.Size(42, 31);
            this.tspIncluir.Text = "Incluir";
            this.tspIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspIncluir.ToolTipText = "Incluir  Transporte";
            this.tspIncluir.Click += new System.EventHandler(this.tspIncluir_Click);
            // 
            // tspAlterar
            // 
            this.tspAlterar.AccessibleDescription = "Alterar";
            this.tspAlterar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAlterar.ForeColor = System.Drawing.Color.Black;
            this.tspAlterar.Image = ((System.Drawing.Image)(resources.GetObject("tspAlterar.Image")));
            this.tspAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAlterar.Name = "tspAlterar";
            this.tspAlterar.Size = new System.Drawing.Size(46, 31);
            this.tspAlterar.Text = "Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.ToolTipText = "Alterar  Transporte";
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
            // 
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir";
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.ForeColor = System.Drawing.Color.Black;
            this.tspExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tspExcluir.Image")));
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(43, 31);
            this.tspExcluir.Text = "Excluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.ToolTipText = "Excluir  Transporte";
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
            // 
            // dgvTab_CNAE_UF
            // 
            this.dgvTab_CNAE_UF.AccessibleDescription = "CNAE UF";
            this.dgvTab_CNAE_UF.AccessibleName = "";
            this.dgvTab_CNAE_UF.AllowUserToAddRows = false;
            this.dgvTab_CNAE_UF.AllowUserToDeleteRows = false;
            this.dgvTab_CNAE_UF.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvTab_CNAE_UF.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTab_CNAE_UF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvTab_CNAE_UF.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_CNAE_UF.ColumnHeadersHeight = 31;
            this.dgvTab_CNAE_UF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.IDTABCNAE,
            this.IDUF,
            this.ESTADO,
            this.IVA,
            this.IDDIZERESNFV,
            this.ICMSST,
            this.TEMP});
            this.dgvTab_CNAE_UF.Location = new System.Drawing.Point(20, 21);
            this.dgvTab_CNAE_UF.MultiSelect = false;
            this.dgvTab_CNAE_UF.Name = "dgvTab_CNAE_UF";
            this.dgvTab_CNAE_UF.ReadOnly = true;
            this.dgvTab_CNAE_UF.RowHeadersVisible = false;
            this.dgvTab_CNAE_UF.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTab_CNAE_UF.Size = new System.Drawing.Size(210, 423);
            this.dgvTab_CNAE_UF.TabIndex = 0;
            this.dgvTab_CNAE_UF.TabStop = false;
            this.ttpTab_CNAE.SetToolTip(this.dgvTab_CNAE_UF, "Grade de visualização CNAE UF");
            this.dgvTab_CNAE_UF.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_CNAE_UF_CellDoubleClick);
            // 
            // bwrTab_CNAE_UF
            // 
            this.bwrTab_CNAE_UF.WorkerSupportsCancellation = true;
            this.bwrTab_CNAE_UF.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrTab_CNAE_UF_DoWork);
            this.bwrTab_CNAE_UF.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrTab_CNAE_UF_RunWorkerCompleted);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 5;
            // 
            // IDTABCNAE
            // 
            this.IDTABCNAE.DataPropertyName = "IDTABCNAE";
            this.IDTABCNAE.HeaderText = "IDTABCNAE";
            this.IDTABCNAE.Name = "IDTABCNAE";
            this.IDTABCNAE.ReadOnly = true;
            this.IDTABCNAE.Visible = false;
            // 
            // IDUF
            // 
            this.IDUF.DataPropertyName = "IDUF";
            this.IDUF.HeaderText = "IDUF";
            this.IDUF.Name = "IDUF";
            this.IDUF.ReadOnly = true;
            this.IDUF.Visible = false;
            // 
            // ESTADO
            // 
            this.ESTADO.DataPropertyName = "ESTADO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ESTADO.DefaultCellStyle = dataGridViewCellStyle2;
            this.ESTADO.HeaderText = "UF";
            this.ESTADO.Name = "ESTADO";
            this.ESTADO.ReadOnly = true;
            this.ESTADO.Width = 40;
            // 
            // IVA
            // 
            this.IVA.DataPropertyName = "IVA";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.IVA.DefaultCellStyle = dataGridViewCellStyle3;
            this.IVA.HeaderText = "IVA";
            this.IVA.Name = "IVA";
            this.IVA.ReadOnly = true;
            this.IVA.Width = 80;
            // 
            // IDDIZERESNFV
            // 
            this.IDDIZERESNFV.DataPropertyName = "IDDIZERESNFV";
            this.IDDIZERESNFV.HeaderText = "IDDIZERESNFV";
            this.IDDIZERESNFV.Name = "IDDIZERESNFV";
            this.IDDIZERESNFV.ReadOnly = true;
            this.IDDIZERESNFV.Visible = false;
            // 
            // ICMSST
            // 
            this.ICMSST.DataPropertyName = "ICMSST";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.ICMSST.DefaultCellStyle = dataGridViewCellStyle4;
            this.ICMSST.HeaderText = "ICMSST";
            this.ICMSST.Name = "ICMSST";
            this.ICMSST.ReadOnly = true;
            this.ICMSST.Width = 80;
            // 
            // TEMP
            // 
            this.TEMP.DataPropertyName = "TEMP";
            this.TEMP.HeaderText = "TEMP";
            this.TEMP.Name = "TEMP";
            this.TEMP.ReadOnly = true;
            this.TEMP.Visible = false;
            // 
            // frmTab_CNAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxDadosCNAE);
            this.Controls.Add(this.gbxLinhas);
            this.Controls.Add(this.tspTab_CNAE);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTab_CNAE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4014";
            this.Text = "CNAE - Cadastro";
            this.ttpTab_CNAE.SetToolTip(this, "CNAE - Registrando");
            this.Load += new System.EventHandler(this.frmTab_CNAE_Load);
            this.Shown += new System.EventHandler(this.frmTab_CNAE_Shown);
            this.Activated += new System.EventHandler(this.frmTab_CNAE_Activated);
            this.tspTab_CNAE.ResumeLayout(false);
            this.tspTab_CNAE.PerformLayout();
            this.gbxDadosCNAE.ResumeLayout(false);
            this.gbxDadosCNAE.PerformLayout();
            this.gbxLinhas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxTab_CNAE_UF)).EndInit();
            this.tspTab_CNAE_UF.ResumeLayout(false);
            this.tspTab_CNAE_UF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_CNAE_UF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpTab_CNAE;
        private System.Windows.Forms.ToolStrip tspTab_CNAE;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxDadosCNAE;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxLinhas;
        private System.Windows.Forms.ToolStrip tspTab_CNAE_UF;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.DataGridView dgvTab_CNAE_UF;
        private System.ComponentModel.BackgroundWorker bwrTab_CNAE_UF;
        private System.Windows.Forms.PictureBox pbxTab_CNAE_UF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDTABCNAE;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDUF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDDIZERESNFV;
        private System.Windows.Forms.DataGridViewTextBoxColumn ICMSST;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEMP;
    }
}