namespace CRCorrea
{
    partial class frmOrcamentoVisold
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrcamentoVisold));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbxTotais = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxTotalConfirmado = new System.Windows.Forms.TextBox();
            this.tbxTotalOrcado = new System.Windows.Forms.TextBox();
            this.gbxPeriodo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxPeriodoAte = new System.Windows.Forms.TextBox();
            this.tbxPeriodoDe = new System.Windows.Forms.TextBox();
            this.gbxStatus = new System.Windows.Forms.GroupBox();
            this.rbnStatusC = new System.Windows.Forms.RadioButton();
            this.rbnStatusT = new System.Windows.Forms.RadioButton();
            this.rbnStatusF = new System.Windows.Forms.RadioButton();
            this.rbnStatusA = new System.Windows.Forms.RadioButton();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspOrcamento_Imprimir1 = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstPesquisa = new System.Windows.Forms.ToolStripTextBox();
            this.dgvOrcamento = new System.Windows.Forms.DataGridView();
            this.pbxOrcamento = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rbnFilial00 = new System.Windows.Forms.RadioButton();
            this.labelTipoProduto = new System.Windows.Forms.Label();
            this.gbxTipoAnalise = new System.Windows.Forms.GroupBox();
            this.rbnFilial02 = new System.Windows.Forms.RadioButton();
            this.rbnFilial01 = new System.Windows.Forms.RadioButton();
            this.gbxTotais.SuspendLayout();
            this.gbxPeriodo.SuspendLayout();
            this.gbxStatus.SuspendLayout();
            this.tspTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrcamento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOrcamento)).BeginInit();
            this.gbxTipoAnalise.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxTotais
            // 
            this.gbxTotais.AccessibleDescription = "Período - Totais";
            this.gbxTotais.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxTotais.Controls.Add(this.label2);
            this.gbxTotais.Controls.Add(this.label3);
            this.gbxTotais.Controls.Add(this.tbxTotalConfirmado);
            this.gbxTotais.Controls.Add(this.tbxTotalOrcado);
            this.gbxTotais.Location = new System.Drawing.Point(446, 12);
            this.gbxTotais.Name = "gbxTotais";
            this.gbxTotais.Size = new System.Drawing.Size(233, 66);
            this.gbxTotais.TabIndex = 2;
            this.gbxTotais.TabStop = false;
            this.gbxTotais.Text = "Período - Totais";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 121;
            this.label2.Text = "Orçado:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 120;
            this.label3.Text = "Confirmado:";
            // 
            // tbxTotalConfirmado
            // 
            this.tbxTotalConfirmado.AccessibleDescription = "Confirmado";
            this.tbxTotalConfirmado.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tbxTotalConfirmado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalConfirmado.Location = new System.Drawing.Point(74, 38);
            this.tbxTotalConfirmado.Name = "tbxTotalConfirmado";
            this.tbxTotalConfirmado.Size = new System.Drawing.Size(151, 21);
            this.tbxTotalConfirmado.TabIndex = 1;
            this.tbxTotalConfirmado.TabStop = false;
            this.tbxTotalConfirmado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxTotalConfirmado.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxTotalConfirmado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxTotalConfirmado.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxTotalOrcado
            // 
            this.tbxTotalOrcado.AccessibleDescription = "Orçado";
            this.tbxTotalOrcado.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tbxTotalOrcado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalOrcado.Location = new System.Drawing.Point(74, 16);
            this.tbxTotalOrcado.Name = "tbxTotalOrcado";
            this.tbxTotalOrcado.Size = new System.Drawing.Size(151, 21);
            this.tbxTotalOrcado.TabIndex = 0;
            this.tbxTotalOrcado.TabStop = false;
            this.tbxTotalOrcado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxTotalOrcado.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxTotalOrcado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxTotalOrcado.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxPeriodo
            // 
            this.gbxPeriodo.AccessibleDescription = "Período a Visualizar";
            this.gbxPeriodo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxPeriodo.Controls.Add(this.label1);
            this.gbxPeriodo.Controls.Add(this.label11);
            this.gbxPeriodo.Controls.Add(this.tbxPeriodoAte);
            this.gbxPeriodo.Controls.Add(this.tbxPeriodoDe);
            this.gbxPeriodo.Location = new System.Drawing.Point(307, 12);
            this.gbxPeriodo.Name = "gbxPeriodo";
            this.gbxPeriodo.Size = new System.Drawing.Size(136, 66);
            this.gbxPeriodo.TabIndex = 1;
            this.gbxPeriodo.TabStop = false;
            this.gbxPeriodo.Text = "Período a Visualizar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "De:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 120;
            this.label11.Text = "até:";
            // 
            // tbxPeriodoAte
            // 
            this.tbxPeriodoAte.AccessibleDescription = "Periodo Até";
            this.tbxPeriodoAte.BackColor = System.Drawing.SystemColors.Window;
            this.tbxPeriodoAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPeriodoAte.Location = new System.Drawing.Point(36, 40);
            this.tbxPeriodoAte.Name = "tbxPeriodoAte";
            this.tbxPeriodoAte.Size = new System.Drawing.Size(89, 21);
            this.tbxPeriodoAte.TabIndex = 1;
            this.tbxPeriodoAte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxPeriodoAte.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxPeriodoAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxPeriodoAte.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxPeriodoDe
            // 
            this.tbxPeriodoDe.AccessibleDescription = "Periodo De";
            this.tbxPeriodoDe.BackColor = System.Drawing.SystemColors.Window;
            this.tbxPeriodoDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPeriodoDe.Location = new System.Drawing.Point(36, 16);
            this.tbxPeriodoDe.Name = "tbxPeriodoDe";
            this.tbxPeriodoDe.Size = new System.Drawing.Size(89, 21);
            this.tbxPeriodoDe.TabIndex = 0;
            this.tbxPeriodoDe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxPeriodoDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxPeriodoDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxPeriodoDe.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxStatus
            // 
            this.gbxStatus.AccessibleDescription = "Status";
            this.gbxStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxStatus.Controls.Add(this.rbnStatusC);
            this.gbxStatus.Controls.Add(this.rbnStatusT);
            this.gbxStatus.Controls.Add(this.rbnStatusF);
            this.gbxStatus.Controls.Add(this.rbnStatusA);
            this.gbxStatus.Location = new System.Drawing.Point(12, 12);
            this.gbxStatus.Name = "gbxStatus";
            this.gbxStatus.Size = new System.Drawing.Size(169, 64);
            this.gbxStatus.TabIndex = 0;
            this.gbxStatus.TabStop = false;
            this.gbxStatus.Text = "Status";
            // 
            // rbnStatusC
            // 
            this.rbnStatusC.AccessibleDescription = "Cancelados";
            this.rbnStatusC.AutoSize = true;
            this.rbnStatusC.Location = new System.Drawing.Point(78, 38);
            this.rbnStatusC.Name = "rbnStatusC";
            this.rbnStatusC.Size = new System.Drawing.Size(80, 17);
            this.rbnStatusC.TabIndex = 4;
            this.rbnStatusC.Text = "Cancelados";
            this.rbnStatusC.UseVisualStyleBackColor = true;
            this.rbnStatusC.CheckedChanged += new System.EventHandler(this.StatusCheckedChanged);
            this.rbnStatusC.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnStatusC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnStatusC.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnStatusT
            // 
            this.rbnStatusT.AccessibleDescription = "Todos";
            this.rbnStatusT.AutoSize = true;
            this.rbnStatusT.Location = new System.Drawing.Point(6, 20);
            this.rbnStatusT.Name = "rbnStatusT";
            this.rbnStatusT.Size = new System.Drawing.Size(54, 17);
            this.rbnStatusT.TabIndex = 1;
            this.rbnStatusT.Text = "Todos";
            this.rbnStatusT.UseVisualStyleBackColor = true;
            this.rbnStatusT.CheckedChanged += new System.EventHandler(this.StatusCheckedChanged);
            // 
            // rbnStatusF
            // 
            this.rbnStatusF.AccessibleDescription = "Fechado";
            this.rbnStatusF.AutoSize = true;
            this.rbnStatusF.Location = new System.Drawing.Point(6, 38);
            this.rbnStatusF.Name = "rbnStatusF";
            this.rbnStatusF.Size = new System.Drawing.Size(66, 17);
            this.rbnStatusF.TabIndex = 3;
            this.rbnStatusF.Text = "Fechado";
            this.rbnStatusF.UseVisualStyleBackColor = true;
            this.rbnStatusF.CheckedChanged += new System.EventHandler(this.StatusCheckedChanged);
            this.rbnStatusF.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnStatusF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnStatusF.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnStatusA
            // 
            this.rbnStatusA.AccessibleDescription = "Aberto";
            this.rbnStatusA.AutoSize = true;
            this.rbnStatusA.Checked = true;
            this.rbnStatusA.Location = new System.Drawing.Point(78, 20);
            this.rbnStatusA.Name = "rbnStatusA";
            this.rbnStatusA.Size = new System.Drawing.Size(58, 17);
            this.rbnStatusA.TabIndex = 2;
            this.rbnStatusA.TabStop = true;
            this.rbnStatusA.Text = "Aberto";
            this.rbnStatusA.UseVisualStyleBackColor = true;
            this.rbnStatusA.CheckedChanged += new System.EventHandler(this.StatusCheckedChanged);
            this.rbnStatusA.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnStatusA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnStatusA.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // lblRegistros
            // 
            this.lblRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.Location = new System.Drawing.Point(788, 65);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(104, 13);
            this.lblRegistros.TabIndex = 137;
            this.lblRegistros.Text = "Nº Registros: 00000";
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspOrcamento_Imprimir1,
            this.tspImprimir,
            this.tspExcluir,
            this.tspEscolher,
            this.tspRetornar,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tstPesquisa});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(12, 606);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(956, 46);
            this.tspTool.TabIndex = 138;
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
            // tspOrcamento_Imprimir1
            // 
            this.tspOrcamento_Imprimir1.AccessibleDescription = "Imprimir/Email";
            this.tspOrcamento_Imprimir1.AutoSize = false;
            this.tspOrcamento_Imprimir1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspOrcamento_Imprimir1.Image = ((System.Drawing.Image)(resources.GetObject("tspOrcamento_Imprimir1.Image")));
            this.tspOrcamento_Imprimir1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspOrcamento_Imprimir1.Name = "tspOrcamento_Imprimir1";
            this.tspOrcamento_Imprimir1.Size = new System.Drawing.Size(100, 42);
            this.tspOrcamento_Imprimir1.Text = "Imprimir/&E-mail";
            this.tspOrcamento_Imprimir1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspOrcamento_Imprimir1.Click += new System.EventHandler(this.tspOrcamento_Imprimir1_Click);
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
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 47);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 16);
            this.toolStripLabel1.Text = "Procurar";
            // 
            // tstPesquisa
            // 
            this.tstPesquisa.AccessibleDescription = "Procurar";
            this.tstPesquisa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstPesquisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstPesquisa.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstPesquisa.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstPesquisa.Name = "tstPesquisa";
            this.tstPesquisa.Size = new System.Drawing.Size(280, 21);
            this.tstPesquisa.ToolTipText = "Procurar";
            this.tstPesquisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstPesquisa_KeyUp);
            this.tstPesquisa.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tstPesquisa_MouseUp_1);
            // 
            // dgvOrcamento
            // 
            this.dgvOrcamento.AccessibleDescription = " Orcamento";
            this.dgvOrcamento.AllowUserToAddRows = false;
            this.dgvOrcamento.AllowUserToDeleteRows = false;
            this.dgvOrcamento.AllowUserToResizeColumns = false;
            this.dgvOrcamento.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvOrcamento.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrcamento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrcamento.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvOrcamento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOrcamento.Location = new System.Drawing.Point(12, 81);
            this.dgvOrcamento.MultiSelect = false;
            this.dgvOrcamento.Name = "dgvOrcamento";
            this.dgvOrcamento.ReadOnly = true;
            this.dgvOrcamento.RowHeadersVisible = false;
            this.dgvOrcamento.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOrcamento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOrcamento.Size = new System.Drawing.Size(996, 522);
            this.dgvOrcamento.TabIndex = 139;
            this.toolTip1.SetToolTip(this.dgvOrcamento, "Visualizando Orçamento");
            this.dgvOrcamento.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrcamento_CellDoubleClick);
            this.dgvOrcamento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvOrcamento_KeyDown);
            this.dgvOrcamento.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvOrcamento_MouseDoubleClick);
            // 
            // pbxOrcamento
            // 
            this.pbxOrcamento.AccessibleDescription = "Carregando Orçamento";
            this.pbxOrcamento.BackColor = System.Drawing.SystemColors.Window;
            this.pbxOrcamento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxOrcamento.Location = new System.Drawing.Point(12, 84);
            this.pbxOrcamento.Name = "pbxOrcamento";
            this.pbxOrcamento.Size = new System.Drawing.Size(956, 509);
            this.pbxOrcamento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxOrcamento.TabIndex = 148;
            this.pbxOrcamento.TabStop = false;
            this.toolTip1.SetToolTip(this.pbxOrcamento, "Imagem");
            // 
            // rbnFilial00
            // 
            this.rbnFilial00.AccessibleDescription = "Todas as Filiais";
            this.rbnFilial00.AutoSize = true;
            this.rbnFilial00.Checked = true;
            this.rbnFilial00.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnFilial00.Location = new System.Drawing.Point(5, 16);
            this.rbnFilial00.Name = "rbnFilial00";
            this.rbnFilial00.Size = new System.Drawing.Size(96, 17);
            this.rbnFilial00.TabIndex = 0;
            this.rbnFilial00.TabStop = true;
            this.rbnFilial00.Text = "Todas as Filiais";
            this.toolTip1.SetToolTip(this.rbnFilial00, "Todas as Filiais");
            this.rbnFilial00.UseVisualStyleBackColor = true;
            this.rbnFilial00.Click += new System.EventHandler(this.rbnFilial00_Click);
            this.rbnFilial00.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnFilial00.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnFilial00.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // labelTipoProduto
            // 
            this.labelTipoProduto.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTipoProduto.Location = new System.Drawing.Point(712, 19);
            this.labelTipoProduto.Name = "labelTipoProduto";
            this.labelTipoProduto.Size = new System.Drawing.Size(293, 36);
            this.labelTipoProduto.TabIndex = 149;
            this.labelTipoProduto.Text = "Orçamentos";
            this.labelTipoProduto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxTipoAnalise
            // 
            this.gbxTipoAnalise.AccessibleDescription = "Tipo Analise";
            this.gbxTipoAnalise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxTipoAnalise.Controls.Add(this.rbnFilial00);
            this.gbxTipoAnalise.Controls.Add(this.rbnFilial02);
            this.gbxTipoAnalise.Controls.Add(this.rbnFilial01);
            this.gbxTipoAnalise.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxTipoAnalise.Location = new System.Drawing.Point(187, 6);
            this.gbxTipoAnalise.Name = "gbxTipoAnalise";
            this.gbxTipoAnalise.Size = new System.Drawing.Size(112, 68);
            this.gbxTipoAnalise.TabIndex = 150;
            this.gbxTipoAnalise.TabStop = false;
            this.gbxTipoAnalise.Text = "Filial";
            // 
            // rbnFilial02
            // 
            this.rbnFilial02.AccessibleDescription = "Filial 02";
            this.rbnFilial02.AutoSize = true;
            this.rbnFilial02.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnFilial02.Location = new System.Drawing.Point(5, 48);
            this.rbnFilial02.Name = "rbnFilial02";
            this.rbnFilial02.Size = new System.Drawing.Size(78, 17);
            this.rbnFilial02.TabIndex = 2;
            this.rbnFilial02.Text = "[02] = SBC";
            this.rbnFilial02.UseVisualStyleBackColor = true;
            this.rbnFilial02.Click += new System.EventHandler(this.rbnFilial02_Click);
            this.rbnFilial02.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnFilial02.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnFilial02.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnFilial01
            // 
            this.rbnFilial01.AccessibleDescription = "Filial 01";
            this.rbnFilial01.AutoSize = true;
            this.rbnFilial01.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnFilial01.Location = new System.Drawing.Point(5, 32);
            this.rbnFilial01.Name = "rbnFilial01";
            this.rbnFilial01.Size = new System.Drawing.Size(100, 17);
            this.rbnFilial01.TabIndex = 1;
            this.rbnFilial01.Text = "[01] = Miracatu";
            this.rbnFilial01.UseVisualStyleBackColor = true;
            this.rbnFilial01.Click += new System.EventHandler(this.rbnFilial01_Click);
            this.rbnFilial01.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnFilial01.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnFilial01.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // frmOrcamentoVisold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxTipoAnalise);
            this.Controls.Add(this.labelTipoProduto);
            this.Controls.Add(this.pbxOrcamento);
            this.Controls.Add(this.dgvOrcamento);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.lblRegistros);
            this.Controls.Add(this.gbxTotais);
            this.Controls.Add(this.gbxPeriodo);
            this.Controls.Add(this.gbxStatus);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrcamentoVisold";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Orçamento - Visualização";
            this.Activated += new System.EventHandler(this.frmOrcamentoVis_Activated);
            this.Load += new System.EventHandler(this.frmOrcamentoVis_Load);
            this.Shown += new System.EventHandler(this.frmOrcamentoVis_Shown);
            this.gbxTotais.ResumeLayout(false);
            this.gbxTotais.PerformLayout();
            this.gbxPeriodo.ResumeLayout(false);
            this.gbxPeriodo.PerformLayout();
            this.gbxStatus.ResumeLayout(false);
            this.gbxStatus.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrcamento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOrcamento)).EndInit();
            this.gbxTipoAnalise.ResumeLayout(false);
            this.gbxTipoAnalise.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxTotais;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxTotalConfirmado;
        private System.Windows.Forms.TextBox tbxTotalOrcado;
        private System.Windows.Forms.GroupBox gbxPeriodo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxPeriodoAte;
        private System.Windows.Forms.TextBox tbxPeriodoDe;
        private System.Windows.Forms.GroupBox gbxStatus;
        private System.Windows.Forms.RadioButton rbnStatusF;
        private System.Windows.Forms.RadioButton rbnStatusA;
        private System.Windows.Forms.Label lblRegistros;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstPesquisa;
        private System.Windows.Forms.DataGridView dgvOrcamento;
        private System.Windows.Forms.PictureBox pbxOrcamento;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton rbnStatusT;
        private System.Windows.Forms.ToolStripButton tspOrcamento_Imprimir1;
        private System.Windows.Forms.RadioButton rbnStatusC;
        private System.Windows.Forms.Label labelTipoProduto;
        private System.Windows.Forms.GroupBox gbxTipoAnalise;
        private System.Windows.Forms.RadioButton rbnFilial00;
        private System.Windows.Forms.RadioButton rbnFilial02;
        private System.Windows.Forms.RadioButton rbnFilial01;
    }
}