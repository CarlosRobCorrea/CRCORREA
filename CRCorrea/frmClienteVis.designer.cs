namespace CRCorrea
{
    partial class frmClienteVis
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClienteVis));
            this.dgvCliente = new System.Windows.Forms.DataGridView();
            this.gbxFiltro = new System.Windows.Forms.GroupBox();
            this.rbnAtivoN = new System.Windows.Forms.RadioButton();
            this.rbnAtivoS = new System.Windows.Forms.RadioButton();
            this.rbnAtivo = new System.Windows.Forms.RadioButton();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstPesquisa = new System.Windows.Forms.ToolStripTextBox();
            this.gbxTipo = new System.Windows.Forms.GroupBox();
            this.rbnTipoU = new System.Windows.Forms.RadioButton();
            this.rbnTipoT = new System.Windows.Forms.RadioButton();
            this.rbnTipoV = new System.Windows.Forms.RadioButton();
            this.rbnTipoF = new System.Windows.Forms.RadioButton();
            this.rbnTipoC = new System.Windows.Forms.RadioButton();
            this.rbnTipo = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblRegistros = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCliente)).BeginInit();
            this.gbxFiltro.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.gbxTipo.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCliente
            // 
            this.dgvCliente.AccessibleDescription = "Grid Clientes";
            this.dgvCliente.AllowUserToAddRows = false;
            this.dgvCliente.AllowUserToDeleteRows = false;
            this.dgvCliente.AllowUserToOrderColumns = true;
            this.dgvCliente.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvCliente.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCliente.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCliente.Location = new System.Drawing.Point(12, 64);
            this.dgvCliente.MultiSelect = false;
            this.dgvCliente.Name = "dgvCliente";
            this.dgvCliente.ReadOnly = true;
            this.dgvCliente.RowHeadersVisible = false;
            this.dgvCliente.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCliente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCliente.Size = new System.Drawing.Size(980, 354);
            this.dgvCliente.TabIndex = 7;
            this.toolTip1.SetToolTip(this.dgvCliente, "Visualizando Fornecedores");
            this.dgvCliente.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCliente_CellDoubleClick);
            // 
            // gbxFiltro
            // 
            this.gbxFiltro.AccessibleDescription = "Ativo?";
            this.gbxFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxFiltro.Controls.Add(this.rbnAtivoN);
            this.gbxFiltro.Controls.Add(this.rbnAtivoS);
            this.gbxFiltro.Controls.Add(this.rbnAtivo);
            this.gbxFiltro.Location = new System.Drawing.Point(12, 10);
            this.gbxFiltro.Name = "gbxFiltro";
            this.gbxFiltro.Size = new System.Drawing.Size(169, 48);
            this.gbxFiltro.TabIndex = 8;
            this.gbxFiltro.TabStop = false;
            this.gbxFiltro.Text = "Ativo?";
            this.toolTip1.SetToolTip(this.gbxFiltro, "Filtro=Opção(Todos/Ativo/Inativo)");
            // 
            // rbnAtivoN
            // 
            this.rbnAtivoN.AccessibleDescription = "Não";
            this.rbnAtivoN.AutoSize = true;
            this.rbnAtivoN.Location = new System.Drawing.Point(53, 20);
            this.rbnAtivoN.Name = "rbnAtivoN";
            this.rbnAtivoN.Size = new System.Drawing.Size(44, 17);
            this.rbnAtivoN.TabIndex = 3;
            this.rbnAtivoN.TabStop = true;
            this.rbnAtivoN.Text = "Não";
            this.toolTip1.SetToolTip(this.rbnAtivoN, "Só Inativos");
            this.rbnAtivoN.UseVisualStyleBackColor = true;
            this.rbnAtivoN.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnAtivoN.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivoN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtivoN.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAtivoS
            // 
            this.rbnAtivoS.AccessibleDescription = "Sim";
            this.rbnAtivoS.AutoSize = true;
            this.rbnAtivoS.Checked = true;
            this.rbnAtivoS.Location = new System.Drawing.Point(6, 20);
            this.rbnAtivoS.Name = "rbnAtivoS";
            this.rbnAtivoS.Size = new System.Drawing.Size(41, 17);
            this.rbnAtivoS.TabIndex = 1;
            this.rbnAtivoS.TabStop = true;
            this.rbnAtivoS.Text = "Sim";
            this.toolTip1.SetToolTip(this.rbnAtivoS, "Só Ativos");
            this.rbnAtivoS.UseVisualStyleBackColor = true;
            this.rbnAtivoS.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnAtivoS.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivoS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtivoS.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAtivo
            // 
            this.rbnAtivo.AccessibleDescription = "Ambos";
            this.rbnAtivo.AutoSize = true;
            this.rbnAtivo.Location = new System.Drawing.Point(103, 20);
            this.rbnAtivo.Name = "rbnAtivo";
            this.rbnAtivo.Size = new System.Drawing.Size(57, 17);
            this.rbnAtivo.TabIndex = 0;
            this.rbnAtivo.TabStop = true;
            this.rbnAtivo.Text = "Ambos";
            this.toolTip1.SetToolTip(this.rbnAtivo, "Visualiza Todos");
            this.rbnAtivo.UseVisualStyleBackColor = true;
            this.rbnAtivo.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnAtivo.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtivo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspImprimir,
            this.tspEscolher,
            this.tspRetornar,
            this.tspExcluir,
            this.toolStripSeparator1,
            this.tslbLocalizar,
            this.tstPesquisa});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(12, 421);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(854, 63);
            this.tspTool.TabIndex = 15;
            this.tspTool.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tspTool_ItemClicked);
            // 
            // tspIncluir
            // 
            this.tspIncluir.AccessibleDescription = "Incluir";
            this.tspIncluir.AutoSize = false;
            this.tspIncluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspIncluir.Image = ((System.Drawing.Image)(resources.GetObject("tspIncluir.Image")));
            this.tspIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspIncluir.Name = "tspIncluir";
            this.tspIncluir.Size = new System.Drawing.Size(66, 42);
            this.tspIncluir.Text = "&Incluir";
            this.tspIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspIncluir.Click += new System.EventHandler(this.tspIncluir_Click);
            // 
            // tspAlterar
            // 
            this.tspAlterar.AccessibleDescription = "Alterar";
            this.tspAlterar.AutoSize = false;
            this.tspAlterar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAlterar.Image = ((System.Drawing.Image)(resources.GetObject("tspAlterar.Image")));
            this.tspAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAlterar.Name = "tspAlterar";
            this.tspAlterar.Size = new System.Drawing.Size(66, 42);
            this.tspAlterar.Text = "&Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
            // 
            // tspImprimir
            // 
            this.tspImprimir.AccessibleDescription = "Imprimir";
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimir.Image")));
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(66, 42);
            this.tspImprimir.Text = "I&mprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
            // 
            // tspEscolher
            // 
            this.tspEscolher.AccessibleDescription = "Escolher";
            this.tspEscolher.AutoSize = false;
            this.tspEscolher.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspEscolher.Image = ((System.Drawing.Image)(resources.GetObject("tspEscolher.Image")));
            this.tspEscolher.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspEscolher.Name = "tspEscolher";
            this.tspEscolher.Size = new System.Drawing.Size(66, 42);
            this.tspEscolher.Text = "&Escolher";
            this.tspEscolher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspEscolher.Click += new System.EventHandler(this.tspEscolher_Click);
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
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir";
            this.tspExcluir.AutoSize = false;
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tspExcluir.Image")));
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(66, 42);
            this.tspExcluir.Text = "E&xcluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.Visible = false;
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 47);
            // 
            // tslbLocalizar
            // 
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(64, 16);
            this.tslbLocalizar.Text = "Procurar";
            // 
            // tstPesquisa
            // 
            this.tstPesquisa.AccessibleDescription = "Pesquisar";
            this.tstPesquisa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstPesquisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstPesquisa.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstPesquisa.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstPesquisa.Name = "tstPesquisa";
            this.tstPesquisa.Size = new System.Drawing.Size(150, 21);
            this.tstPesquisa.ToolTipText = "Procurar";
            this.tstPesquisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstPesquisa_KeyUp);
            this.tstPesquisa.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tstPesquisa_MouseUp);
            // 
            // gbxTipo
            // 
            this.gbxTipo.AccessibleDescription = "Tipos";
            this.gbxTipo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxTipo.Controls.Add(this.rbnTipoU);
            this.gbxTipo.Controls.Add(this.rbnTipoT);
            this.gbxTipo.Controls.Add(this.rbnTipoV);
            this.gbxTipo.Controls.Add(this.rbnTipoF);
            this.gbxTipo.Controls.Add(this.rbnTipoC);
            this.gbxTipo.Controls.Add(this.rbnTipo);
            this.gbxTipo.Location = new System.Drawing.Point(187, 10);
            this.gbxTipo.Name = "gbxTipo";
            this.gbxTipo.Size = new System.Drawing.Size(506, 48);
            this.gbxTipo.TabIndex = 16;
            this.gbxTipo.TabStop = false;
            this.gbxTipo.Text = "Tipo";
            this.toolTip1.SetToolTip(this.gbxTipo, "Filtra=Opção(Todos/Cliente/Fornecedor/Vendedor)");
            // 
            // rbnTipoU
            // 
            this.rbnTipoU.AccessibleDescription = "Funcionário";
            this.rbnTipoU.AutoSize = true;
            this.rbnTipoU.Location = new System.Drawing.Point(404, 20);
            this.rbnTipoU.Name = "rbnTipoU";
            this.rbnTipoU.Size = new System.Drawing.Size(80, 17);
            this.rbnTipoU.TabIndex = 17;
            this.rbnTipoU.TabStop = true;
            this.rbnTipoU.Tag = "";
            this.rbnTipoU.Text = "Funcionário";
            this.toolTip1.SetToolTip(this.rbnTipoU, "Funcionário");
            this.rbnTipoU.UseVisualStyleBackColor = true;
            this.rbnTipoU.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnTipoU.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTipoU.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTipoU.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnTipoT
            // 
            this.rbnTipoT.AccessibleDescription = "Transportadora";
            this.rbnTipoT.AutoSize = true;
            this.rbnTipoT.Location = new System.Drawing.Point(298, 20);
            this.rbnTipoT.Name = "rbnTipoT";
            this.rbnTipoT.Size = new System.Drawing.Size(100, 17);
            this.rbnTipoT.TabIndex = 16;
            this.rbnTipoT.TabStop = true;
            this.rbnTipoT.Tag = "";
            this.rbnTipoT.Text = "Transportadora";
            this.toolTip1.SetToolTip(this.rbnTipoT, "Vendedores");
            this.rbnTipoT.UseVisualStyleBackColor = true;
            this.rbnTipoT.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnTipoT.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTipoT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTipoT.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnTipoV
            // 
            this.rbnTipoV.AccessibleDescription = "Vendedor";
            this.rbnTipoV.AutoSize = true;
            this.rbnTipoV.Location = new System.Drawing.Point(221, 20);
            this.rbnTipoV.Name = "rbnTipoV";
            this.rbnTipoV.Size = new System.Drawing.Size(71, 17);
            this.rbnTipoV.TabIndex = 15;
            this.rbnTipoV.TabStop = true;
            this.rbnTipoV.Tag = "";
            this.rbnTipoV.Text = "Vendedor";
            this.toolTip1.SetToolTip(this.rbnTipoV, "Vendedores");
            this.rbnTipoV.UseVisualStyleBackColor = true;
            this.rbnTipoV.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnTipoV.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTipoV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTipoV.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnTipoF
            // 
            this.rbnTipoF.AccessibleDescription = "Fornecedor";
            this.rbnTipoF.AutoSize = true;
            this.rbnTipoF.Location = new System.Drawing.Point(135, 20);
            this.rbnTipoF.Name = "rbnTipoF";
            this.rbnTipoF.Size = new System.Drawing.Size(80, 17);
            this.rbnTipoF.TabIndex = 3;
            this.rbnTipoF.TabStop = true;
            this.rbnTipoF.Text = "Fornecedor";
            this.toolTip1.SetToolTip(this.rbnTipoF, "Fornecedores");
            this.rbnTipoF.UseVisualStyleBackColor = true;
            this.rbnTipoF.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnTipoF.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTipoF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTipoF.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnTipoC
            // 
            this.rbnTipoC.AccessibleDescription = "Cliente";
            this.rbnTipoC.AutoSize = true;
            this.rbnTipoC.Location = new System.Drawing.Point(66, 20);
            this.rbnTipoC.Name = "rbnTipoC";
            this.rbnTipoC.Size = new System.Drawing.Size(58, 17);
            this.rbnTipoC.TabIndex = 1;
            this.rbnTipoC.TabStop = true;
            this.rbnTipoC.Text = "Cliente";
            this.toolTip1.SetToolTip(this.rbnTipoC, "Clientes");
            this.rbnTipoC.UseVisualStyleBackColor = true;
            this.rbnTipoC.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnTipoC.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTipoC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTipoC.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnTipo
            // 
            this.rbnTipo.AccessibleDescription = "Todos";
            this.rbnTipo.AutoSize = true;
            this.rbnTipo.Checked = true;
            this.rbnTipo.Location = new System.Drawing.Point(6, 20);
            this.rbnTipo.Name = "rbnTipo";
            this.rbnTipo.Size = new System.Drawing.Size(54, 17);
            this.rbnTipo.TabIndex = 0;
            this.rbnTipo.TabStop = true;
            this.rbnTipo.Text = "Todos";
            this.toolTip1.SetToolTip(this.rbnTipo, "Todos os Tipos");
            this.rbnTipo.UseVisualStyleBackColor = true;
            this.rbnTipo.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnTipo.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTipo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTipo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(760, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 25);
            this.label1.TabIndex = 133;
            this.label1.Text = "Cadastro de Parceiros";
            // 
            // lblRegistros
            // 
            this.lblRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblRegistros.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.Location = new System.Drawing.Point(869, 428);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(131, 14);
            this.lblRegistros.TabIndex = 134;
            this.lblRegistros.Text = "Nº Registros: 00000";
            // 
            // frmClienteVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 510);
            this.Controls.Add(this.lblRegistros);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbxTipo);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxFiltro);
            this.Controls.Add(this.dgvCliente);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmClienteVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "510";
            this.Text = "Clientes/Fornecedores - Visualização";
            this.toolTip1.SetToolTip(this, "Clientes/Fornecedores - Visualização");
            this.Activated += new System.EventHandler(this.frmClienteVis_Activated);
            this.Load += new System.EventHandler(this.frmClienteVis_Load);
            this.Shown += new System.EventHandler(this.frmClienteVis_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCliente)).EndInit();
            this.gbxFiltro.ResumeLayout(false);
            this.gbxFiltro.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxTipo.ResumeLayout(false);
            this.gbxTipo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCliente;
        private System.Windows.Forms.GroupBox gbxFiltro;
        private System.Windows.Forms.RadioButton rbnAtivoN;
        private System.Windows.Forms.RadioButton rbnAtivoS;
        private System.Windows.Forms.RadioButton rbnAtivo;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxTipo;
        private System.Windows.Forms.RadioButton rbnTipoF;
        private System.Windows.Forms.RadioButton rbnTipoC;
        private System.Windows.Forms.RadioButton rbnTipo;
        private System.Windows.Forms.RadioButton rbnTipoV;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RadioButton rbnTipoT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbnTipoU;
        private System.Windows.Forms.ToolStripTextBox tstPesquisa;
        private System.Windows.Forms.Label lblRegistros;
    }
}