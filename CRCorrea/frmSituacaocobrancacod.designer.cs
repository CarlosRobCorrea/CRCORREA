namespace CRCorrea
{ 
    partial class frmSituacaocobrancacod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSituacaocobrancacod));
            this.ttpSitucaocobranca = new System.Windows.Forms.ToolTip(this.components);
            this.btnIdHistoricoPagar = new System.Windows.Forms.Button();
            this.btnIdCentroCustoPagar = new System.Windows.Forms.Button();
            this.btnIdCentroCustoReceber = new System.Windows.Forms.Button();
            this.btnIdHistoricoReceber = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxCentrocustoreceber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxHistoricoreceber = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxCentrocustopagar = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxHistoricopagar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspSetorSetorIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspSetorSetorAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspSetorSetorExcluir = new System.Windows.Forms.ToolStripButton();
            this.dgvSituacaocobrancacod1 = new System.Windows.Forms.DataGridView();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.bwrSituacaocobrancacod1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSituacaocobrancacod1)).BeginInit();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnIdHistoricoPagar
            // 
            this.btnIdHistoricoPagar.AccessibleDescription = "Busca Histórico";
            this.btnIdHistoricoPagar.Image = ((System.Drawing.Image)(resources.GetObject("btnIdHistoricoPagar.Image")));
            this.btnIdHistoricoPagar.Location = new System.Drawing.Point(84, 33);
            this.btnIdHistoricoPagar.Name = "btnIdHistoricoPagar";
            this.btnIdHistoricoPagar.Size = new System.Drawing.Size(21, 21);
            this.btnIdHistoricoPagar.TabIndex = 8;
            this.btnIdHistoricoPagar.TabStop = false;
            this.ttpSitucaocobranca.SetToolTip(this.btnIdHistoricoPagar, "Escolher Histórico do Banco - CP");
            this.btnIdHistoricoPagar.UseVisualStyleBackColor = true;
            this.btnIdHistoricoPagar.Click += new System.EventHandler(this.btnIdHistoricoPagar_Click);
            // 
            // btnIdCentroCustoPagar
            // 
            this.btnIdCentroCustoPagar.AccessibleDescription = "Busca Centro de Custo";
            this.btnIdCentroCustoPagar.Image = ((System.Drawing.Image)(resources.GetObject("btnIdCentroCustoPagar.Image")));
            this.btnIdCentroCustoPagar.Location = new System.Drawing.Point(224, 34);
            this.btnIdCentroCustoPagar.Name = "btnIdCentroCustoPagar";
            this.btnIdCentroCustoPagar.Size = new System.Drawing.Size(21, 21);
            this.btnIdCentroCustoPagar.TabIndex = 11;
            this.btnIdCentroCustoPagar.TabStop = false;
            this.ttpSitucaocobranca.SetToolTip(this.btnIdCentroCustoPagar, "Localizar o Funcionario no R.H.");
            this.btnIdCentroCustoPagar.UseVisualStyleBackColor = true;
            this.btnIdCentroCustoPagar.Click += new System.EventHandler(this.btnIdCentroCustoPagar_Click);
            // 
            // btnIdCentroCustoReceber
            // 
            this.btnIdCentroCustoReceber.AccessibleDescription = "Busca Centro de Custo";
            this.btnIdCentroCustoReceber.Image = ((System.Drawing.Image)(resources.GetObject("btnIdCentroCustoReceber.Image")));
            this.btnIdCentroCustoReceber.Location = new System.Drawing.Point(224, 33);
            this.btnIdCentroCustoReceber.Name = "btnIdCentroCustoReceber";
            this.btnIdCentroCustoReceber.Size = new System.Drawing.Size(21, 21);
            this.btnIdCentroCustoReceber.TabIndex = 11;
            this.btnIdCentroCustoReceber.TabStop = false;
            this.ttpSitucaocobranca.SetToolTip(this.btnIdCentroCustoReceber, "Localizar o Funcionario no R.H.");
            this.btnIdCentroCustoReceber.UseVisualStyleBackColor = true;
            this.btnIdCentroCustoReceber.Click += new System.EventHandler(this.btnIdCentroCustoReceber_Click);
            // 
            // btnIdHistoricoReceber
            // 
            this.btnIdHistoricoReceber.AccessibleDescription = "Busca Histórico";
            this.btnIdHistoricoReceber.Image = ((System.Drawing.Image)(resources.GetObject("btnIdHistoricoReceber.Image")));
            this.btnIdHistoricoReceber.Location = new System.Drawing.Point(84, 34);
            this.btnIdHistoricoReceber.Name = "btnIdHistoricoReceber";
            this.btnIdHistoricoReceber.Size = new System.Drawing.Size(21, 21);
            this.btnIdHistoricoReceber.TabIndex = 8;
            this.btnIdHistoricoReceber.TabStop = false;
            this.ttpSitucaocobranca.SetToolTip(this.btnIdHistoricoReceber, "Localizar o Funcionario no R.H.");
            this.btnIdHistoricoReceber.UseVisualStyleBackColor = true;
            this.btnIdHistoricoReceber.Click += new System.EventHandler(this.btnIdHistoricoReceber_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Dados";
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxNome);
            this.groupBox1.Controls.Add(this.tbxCodigo);
            this.groupBox1.Location = new System.Drawing.Point(240, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.ttpSitucaocobranca.SetToolTip(this.groupBox1, "Grupo - ND");
            // 
            // groupBox3
            // 
            this.groupBox3.AccessibleDescription = "Efetua uma Baixa no Contas a Receber";
            this.groupBox3.Controls.Add(this.btnIdCentroCustoReceber);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tbxCentrocustoreceber);
            this.groupBox3.Controls.Add(this.btnIdHistoricoReceber);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbxHistoricoreceber);
            this.groupBox3.Location = new System.Drawing.Point(267, 61);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 67);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Efetua uma Baixa no Contas a Receber";
            this.ttpSitucaocobranca.SetToolTip(this.groupBox3, "Grupo - Efetua uma Baixa no Contas a Receber");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(108, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Centro de Custo do Banco:";
            // 
            // tbxCentrocustoreceber
            // 
            this.tbxCentrocustoreceber.AccessibleDescription = "Centro de Custo do Banco";
            this.tbxCentrocustoreceber.BackColor = System.Drawing.Color.LightGray;
            this.tbxCentrocustoreceber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCentrocustoreceber.Location = new System.Drawing.Point(111, 33);
            this.tbxCentrocustoreceber.MaxLength = 20;
            this.tbxCentrocustoreceber.Name = "tbxCentrocustoreceber";
            this.tbxCentrocustoreceber.Size = new System.Drawing.Size(107, 21);
            this.tbxCentrocustoreceber.TabIndex = 1;
            this.ttpSitucaocobranca.SetToolTip(this.tbxCentrocustoreceber, "Centro de Custo do Banco - Contas a Receber");
            this.tbxCentrocustoreceber.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCentrocustoreceber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCentrocustoreceber.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Histórico do Banco:";
            // 
            // tbxHistoricoreceber
            // 
            this.tbxHistoricoreceber.AccessibleDescription = "Histórico do Banco";
            this.tbxHistoricoreceber.BackColor = System.Drawing.Color.LightGray;
            this.tbxHistoricoreceber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxHistoricoreceber.Location = new System.Drawing.Point(6, 33);
            this.tbxHistoricoreceber.MaxLength = 20;
            this.tbxHistoricoreceber.Name = "tbxHistoricoreceber";
            this.tbxHistoricoreceber.Size = new System.Drawing.Size(72, 21);
            this.tbxHistoricoreceber.TabIndex = 0;
            this.ttpSitucaocobranca.SetToolTip(this.tbxHistoricoreceber, "Histórico do Banco - Contas a Receber");
            this.tbxHistoricoreceber.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxHistoricoreceber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxHistoricoreceber.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "Efetua uma Baixa no Contas a Pagar";
            this.groupBox2.Controls.Add(this.btnIdCentroCustoPagar);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbxCentrocustopagar);
            this.groupBox2.Controls.Add(this.btnIdHistoricoPagar);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbxHistoricopagar);
            this.groupBox2.Location = new System.Drawing.Point(6, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 67);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Efetua uma Baixa no Contas a Pagar";
            this.ttpSitucaocobranca.SetToolTip(this.groupBox2, "Grupo - Efetua uma Baixa no Contas a Pagar");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Centro de Custo do Banco:";
            // 
            // tbxCentrocustopagar
            // 
            this.tbxCentrocustopagar.AccessibleDescription = "Centro de Custo do Banco";
            this.tbxCentrocustopagar.BackColor = System.Drawing.Color.LightGray;
            this.tbxCentrocustopagar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCentrocustopagar.Location = new System.Drawing.Point(111, 34);
            this.tbxCentrocustopagar.MaxLength = 20;
            this.tbxCentrocustopagar.Name = "tbxCentrocustopagar";
            this.tbxCentrocustopagar.Size = new System.Drawing.Size(107, 21);
            this.tbxCentrocustopagar.TabIndex = 1;
            this.ttpSitucaocobranca.SetToolTip(this.tbxCentrocustopagar, "Centro de Custo do Banco - Contas a Pagar");
            this.tbxCentrocustopagar.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCentrocustopagar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCentrocustopagar.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Histórico do Banco:";
            // 
            // tbxHistoricopagar
            // 
            this.tbxHistoricopagar.AccessibleDescription = "Histórico do Banco";
            this.tbxHistoricopagar.BackColor = System.Drawing.Color.LightGray;
            this.tbxHistoricopagar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxHistoricopagar.Location = new System.Drawing.Point(6, 33);
            this.tbxHistoricopagar.MaxLength = 20;
            this.tbxHistoricopagar.Name = "tbxHistoricopagar";
            this.tbxHistoricopagar.Size = new System.Drawing.Size(72, 21);
            this.tbxHistoricopagar.TabIndex = 0;
            this.ttpSitucaocobranca.SetToolTip(this.tbxHistoricopagar, "Histórico do Banco - Contas a Pagar");
            this.tbxHistoricopagar.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxHistoricopagar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxHistoricopagar.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Breve Descrição do Código:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Código:";
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Descrição";
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(59, 34);
            this.tbxNome.MaxLength = 25;
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(464, 21);
            this.tbxNome.TabIndex = 1;
            this.ttpSitucaocobranca.SetToolTip(this.tbxNome, "Breve Descrição do Código");
            this.tbxNome.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.AccessibleDescription = "Código";
            this.tbxCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Location = new System.Drawing.Point(6, 34);
            this.tbxCodigo.MaxLength = 2;
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(47, 21);
            this.tbxCodigo.TabIndex = 0;
            this.ttpSitucaocobranca.SetToolTip(this.tbxCodigo, "Código");
            this.tbxCodigo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // groupBox4
            // 
            this.groupBox4.AccessibleDescription = "Sub-Códigos do Código";
            this.groupBox4.Controls.Add(this.toolStrip1);
            this.groupBox4.Controls.Add(this.dgvSituacaocobrancacod1);
            this.groupBox4.Location = new System.Drawing.Point(240, 295);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(531, 239);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sub-Códigos do Código";
            this.ttpSitucaocobranca.SetToolTip(this.groupBox4, "Grupo - Sub-Códigos do Código");
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleDescription = "Barra de Opções";
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSetorSetorIncluir,
            this.tspSetorSetorAlterar,
            this.tspSetorSetorExcluir});
            this.toolStrip1.Location = new System.Drawing.Point(3, 200);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(525, 36);
            this.toolStrip1.TabIndex = 2;
            // 
            // tspSetorSetorIncluir
            // 
            this.tspSetorSetorIncluir.AccessibleDescription = "Incluir";
            this.tspSetorSetorIncluir.AutoSize = false;
            this.tspSetorSetorIncluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSetorSetorIncluir.Image = ((System.Drawing.Image)(resources.GetObject("tspSetorSetorIncluir.Image")));
            this.tspSetorSetorIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSetorSetorIncluir.Name = "tspSetorSetorIncluir";
            this.tspSetorSetorIncluir.Size = new System.Drawing.Size(54, 33);
            this.tspSetorSetorIncluir.Text = "Incluir";
            this.tspSetorSetorIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSetorSetorIncluir.ToolTipText = "Setores - Incluir";
            this.tspSetorSetorIncluir.Click += new System.EventHandler(this.tspSetorSetorIncluir_Click);
            // 
            // tspSetorSetorAlterar
            // 
            this.tspSetorSetorAlterar.AccessibleDescription = "Alterar";
            this.tspSetorSetorAlterar.AutoSize = false;
            this.tspSetorSetorAlterar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSetorSetorAlterar.Image = ((System.Drawing.Image)(resources.GetObject("tspSetorSetorAlterar.Image")));
            this.tspSetorSetorAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSetorSetorAlterar.Name = "tspSetorSetorAlterar";
            this.tspSetorSetorAlterar.Size = new System.Drawing.Size(54, 33);
            this.tspSetorSetorAlterar.Text = "Alterar";
            this.tspSetorSetorAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSetorSetorAlterar.ToolTipText = "Setores - Alterar";
            this.tspSetorSetorAlterar.Click += new System.EventHandler(this.tspSetorSetorAlterar_Click);
            // 
            // tspSetorSetorExcluir
            // 
            this.tspSetorSetorExcluir.AccessibleDescription = "Excluir";
            this.tspSetorSetorExcluir.AutoSize = false;
            this.tspSetorSetorExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSetorSetorExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tspSetorSetorExcluir.Image")));
            this.tspSetorSetorExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSetorSetorExcluir.Name = "tspSetorSetorExcluir";
            this.tspSetorSetorExcluir.Size = new System.Drawing.Size(54, 33);
            this.tspSetorSetorExcluir.Text = "Excluir";
            this.tspSetorSetorExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSetorSetorExcluir.ToolTipText = "Setores - Excluir";
            this.tspSetorSetorExcluir.Click += new System.EventHandler(this.tspSetorSetorExcluir_Click);
            // 
            // dgvSituacaocobrancacod1
            // 
            this.dgvSituacaocobrancacod1.AccessibleDescription = "Sub-Códigos";
            this.dgvSituacaocobrancacod1.AllowUserToAddRows = false;
            this.dgvSituacaocobrancacod1.AllowUserToDeleteRows = false;
            this.dgvSituacaocobrancacod1.AllowUserToResizeRows = false;
            this.dgvSituacaocobrancacod1.BackgroundColor = System.Drawing.Color.White;
            this.dgvSituacaocobrancacod1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSituacaocobrancacod1.Location = new System.Drawing.Point(6, 20);
            this.dgvSituacaocobrancacod1.MultiSelect = false;
            this.dgvSituacaocobrancacod1.Name = "dgvSituacaocobrancacod1";
            this.dgvSituacaocobrancacod1.ReadOnly = true;
            this.dgvSituacaocobrancacod1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSituacaocobrancacod1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSituacaocobrancacod1.Size = new System.Drawing.Size(519, 177);
            this.dgvSituacaocobrancacod1.TabIndex = 0;
            this.ttpSitucaocobranca.SetToolTip(this.dgvSituacaocobrancacod1, "Grade dos Sub-Códigos do Código");
            this.dgvSituacaocobrancacod1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSituacaocobrancacod1_CellMouseDoubleClick);
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
            this.tspTool.Location = new System.Drawing.Point(243, 537);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(397, 45);
            this.tspTool.TabIndex = 2;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspSalvar.Image")));
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(66, 42);
            this.tspSalvar.Text = "Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspPrimeiro
            // 
            this.tspPrimeiro.AccessibleDescription = "Primerio";
            this.tspPrimeiro.AutoSize = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = ((System.Drawing.Image)(resources.GetObject("tspPrimeiro.Image")));
            this.tspPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Size = new System.Drawing.Size(66, 42);
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
            this.tspAnterior.Size = new System.Drawing.Size(66, 42);
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
            this.tspProximo.Size = new System.Drawing.Size(66, 42);
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
            this.tspUltimo.Size = new System.Drawing.Size(66, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspUltimo.Click += new System.EventHandler(this.tspUltimo_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // bwrSituacaocobrancacod1
            // 
            this.bwrSituacaocobrancacod1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrSituacaocobrancacod1_DoWork);
            this.bwrSituacaocobrancacod1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrSituacaocobrancacod1_RunWorkerCompleted);
            // 
            // frmSituacaocobrancacod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSituacaocobrancacod";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "206";
            this.Text = "Situação Cobrança Código  - Cadastro";
            this.ttpSitucaocobranca.SetToolTip(this, "Codigo Cobranca (Aplisoft) - ");
            this.Activated += new System.EventHandler(this.frmSituacaocobrancacod_Activated);
            this.Load += new System.EventHandler(this.frmSituacaocobrancacod_Load);
            this.Shown += new System.EventHandler(this.frmSituacaocobrancacod_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSituacaocobrancacod1)).EndInit();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpSitucaocobranca;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxHistoricopagar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnIdCentroCustoReceber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxCentrocustoreceber;
        private System.Windows.Forms.Button btnIdHistoricoReceber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxHistoricoreceber;
        private System.Windows.Forms.Button btnIdCentroCustoPagar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxCentrocustopagar;
        private System.Windows.Forms.Button btnIdHistoricoPagar;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspSetorSetorIncluir;
        private System.Windows.Forms.ToolStripButton tspSetorSetorAlterar;
        private System.Windows.Forms.ToolStripButton tspSetorSetorExcluir;
        private System.Windows.Forms.DataGridView dgvSituacaocobrancacod1;
        private System.ComponentModel.BackgroundWorker bwrSituacaocobrancacod1;
    }
}