namespace CRCorrea
{
    partial class frmSolicitacaoVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSolicitacaoVis));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxFilial = new System.Windows.Forms.GroupBox();
            this.cbxFilial = new System.Windows.Forms.ComboBox();
            this.dgvSolicitacao = new System.Windows.Forms.DataGridView();
            this.tbxUsuario = new System.Windows.Forms.TextBox();
            this.tspRequisicao = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspImprimirMnu = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxPeriodo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxPeriodoAte = new System.Windows.Forms.TextBox();
            this.tbxPeriodoDe = new System.Windows.Forms.TextBox();
            this.gbxStatus = new System.Windows.Forms.GroupBox();
            this.rbnStatusF = new System.Windows.Forms.RadioButton();
            this.rbnStatusC = new System.Windows.Forms.RadioButton();
            this.rbnStatusA = new System.Windows.Forms.RadioButton();
            this.gbxUsuario = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gbxFilial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitacao)).BeginInit();
            this.tspRequisicao.SuspendLayout();
            this.gbxPeriodo.SuspendLayout();
            this.gbxStatus.SuspendLayout();
            this.gbxUsuario.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxFilial
            // 
            this.gbxFilial.Controls.Add(this.cbxFilial);
            this.gbxFilial.Location = new System.Drawing.Point(573, 18);
            this.gbxFilial.Name = "gbxFilial";
            this.gbxFilial.Size = new System.Drawing.Size(127, 47);
            this.gbxFilial.TabIndex = 126;
            this.gbxFilial.TabStop = false;
            this.gbxFilial.Text = "Filial";
            this.toolTip1.SetToolTip(this.gbxFilial, "Filtra=Lista de Precos");
            // 
            // cbxFilial
            // 
            this.cbxFilial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFilial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFilial.FormattingEnabled = true;
            this.cbxFilial.Location = new System.Drawing.Point(6, 20);
            this.cbxFilial.Name = "cbxFilial";
            this.cbxFilial.Size = new System.Drawing.Size(115, 21);
            this.cbxFilial.TabIndex = 0;
            this.cbxFilial.Enter += new System.EventHandler(this.ControlEnter);
            this.cbxFilial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.cbxFilial.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // dgvSolicitacao
            // 
            this.dgvSolicitacao.AccessibleDescription = "Solicitação de Compra - Almoxarifado";
            this.dgvSolicitacao.AllowUserToAddRows = false;
            this.dgvSolicitacao.AllowUserToDeleteRows = false;
            this.dgvSolicitacao.AllowUserToOrderColumns = true;
            this.dgvSolicitacao.AllowUserToResizeRows = false;
            this.dgvSolicitacao.BackgroundColor = System.Drawing.Color.White;
            this.dgvSolicitacao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSolicitacao.Location = new System.Drawing.Point(12, 117);
            this.dgvSolicitacao.MultiSelect = false;
            this.dgvSolicitacao.Name = "dgvSolicitacao";
            this.dgvSolicitacao.ReadOnly = true;
            this.dgvSolicitacao.RowHeadersVisible = false;
            this.dgvSolicitacao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSolicitacao.Size = new System.Drawing.Size(996, 464);
            this.dgvSolicitacao.StandardTab = true;
            this.dgvSolicitacao.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dgvSolicitacao, "Grade de Visualização Solicitação de Compras (Almox Aprova)");
            this.dgvSolicitacao.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvSolicitacao_MouseDoubleClick);
            // 
            // tbxUsuario
            // 
            this.tbxUsuario.AccessibleDescription = "Usuario";
            this.tbxUsuario.BackColor = System.Drawing.Color.White;
            this.tbxUsuario.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUsuario.Location = new System.Drawing.Point(6, 20);
            this.tbxUsuario.Name = "tbxUsuario";
            this.tbxUsuario.Size = new System.Drawing.Size(110, 18);
            this.tbxUsuario.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxUsuario, "Usuario");
            this.tbxUsuario.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxUsuario.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tspRequisicao
            // 
            this.tspRequisicao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspRequisicao.AutoSize = false;
            this.tspRequisicao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspRequisicao.Dock = System.Windows.Forms.DockStyle.None;
            this.tspRequisicao.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRequisicao.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspRequisicao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspExcluir,
            this.tspImprimir,
            this.tspImprimirMnu,
            this.tspRetornar,
            this.toolStripSeparator1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspRequisicao.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspRequisicao.Location = new System.Drawing.Point(12, 584);
            this.tspRequisicao.Name = "tspRequisicao";
            this.tspRequisicao.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspRequisicao.Size = new System.Drawing.Size(1020, 47);
            this.tspRequisicao.TabIndex = 4;
            // 
            // tspIncluir
            // 
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
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir";
            this.tspExcluir.AutoSize = false;
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tspExcluir.Image")));
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(66, 42);
            this.tspExcluir.Text = "E&xcluir ";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
            // 
            // tspImprimir
            // 
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimir.Image")));
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(66, 42);
            this.tspImprimir.Text = "Im&primir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
            // 
            // tspImprimirMnu
            // 
            this.tspImprimirMnu.AutoSize = false;
            this.tspImprimirMnu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimirMnu.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimirMnu.Image")));
            this.tspImprimirMnu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimirMnu.Name = "tspImprimirMnu";
            this.tspImprimirMnu.Size = new System.Drawing.Size(66, 42);
            this.tspImprimirMnu.Text = "Menu Rel.";
            this.tspImprimirMnu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimirMnu.Click += new System.EventHandler(this.tspImprimirMnu_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "&Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
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
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(63, 14);
            this.tslbLocalizar.Text = "Procurar:";
            // 
            // tstbxLocalizar
            // 
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(120, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstPesquisa_KeyUp);
            this.tstbxLocalizar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tstPesquisa_MouseUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label4.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(752, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(256, 25);
            this.label4.TabIndex = 125;
            this.label4.Text = "Solicitação de Compras";
            this.label4.UseWaitCursor = true;
            // 
            // gbxPeriodo
            // 
            this.gbxPeriodo.Controls.Add(this.label1);
            this.gbxPeriodo.Controls.Add(this.label11);
            this.gbxPeriodo.Controls.Add(this.tbxPeriodoAte);
            this.gbxPeriodo.Controls.Add(this.tbxPeriodoDe);
            this.gbxPeriodo.Location = new System.Drawing.Point(208, 12);
            this.gbxPeriodo.Name = "gbxPeriodo";
            this.gbxPeriodo.Size = new System.Drawing.Size(129, 80);
            this.gbxPeriodo.TabIndex = 124;
            this.gbxPeriodo.TabStop = false;
            this.gbxPeriodo.Text = "Período a Visualizar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "De:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 13);
            this.label11.TabIndex = 120;
            this.label11.Text = "até";
            // 
            // tbxPeriodoAte
            // 
            this.tbxPeriodoAte.BackColor = System.Drawing.Color.White;
            this.tbxPeriodoAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPeriodoAte.Location = new System.Drawing.Point(36, 47);
            this.tbxPeriodoAte.Name = "tbxPeriodoAte";
            this.tbxPeriodoAte.Size = new System.Drawing.Size(77, 21);
            this.tbxPeriodoAte.TabIndex = 1;
            this.tbxPeriodoAte.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxPeriodoAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxPeriodoAte.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxPeriodoDe
            // 
            this.tbxPeriodoDe.BackColor = System.Drawing.Color.White;
            this.tbxPeriodoDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPeriodoDe.Location = new System.Drawing.Point(36, 20);
            this.tbxPeriodoDe.Name = "tbxPeriodoDe";
            this.tbxPeriodoDe.Size = new System.Drawing.Size(77, 21);
            this.tbxPeriodoDe.TabIndex = 0;
            this.tbxPeriodoDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxPeriodoDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxPeriodoDe.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxStatus
            // 
            this.gbxStatus.Controls.Add(this.rbnStatusF);
            this.gbxStatus.Controls.Add(this.rbnStatusC);
            this.gbxStatus.Controls.Add(this.rbnStatusA);
            this.gbxStatus.Location = new System.Drawing.Point(12, 12);
            this.gbxStatus.Name = "gbxStatus";
            this.gbxStatus.Size = new System.Drawing.Size(191, 80);
            this.gbxStatus.TabIndex = 123;
            this.gbxStatus.TabStop = false;
            this.gbxStatus.Text = "Tipo da Visualização";
            // 
            // rbnStatusF
            // 
            this.rbnStatusF.AutoSize = true;
            this.rbnStatusF.Location = new System.Drawing.Point(24, 57);
            this.rbnStatusF.Name = "rbnStatusF";
            this.rbnStatusF.Size = new System.Drawing.Size(147, 17);
            this.rbnStatusF.TabIndex = 5;
            this.rbnStatusF.Text = "Fechados/Pedido Compra";
            this.rbnStatusF.UseVisualStyleBackColor = true;
            this.rbnStatusF.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnStatusF.Click += new System.EventHandler(this.rbnStatusF_Click);
            // 
            // rbnStatusC
            // 
            this.rbnStatusC.AutoSize = true;
            this.rbnStatusC.Location = new System.Drawing.Point(24, 36);
            this.rbnStatusC.Name = "rbnStatusC";
            this.rbnStatusC.Size = new System.Drawing.Size(82, 17);
            this.rbnStatusC.TabIndex = 4;
            this.rbnStatusC.Text = "Em Cotação";
            this.rbnStatusC.UseVisualStyleBackColor = true;
            this.rbnStatusC.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnStatusC.Click += new System.EventHandler(this.rbnStatusC_Click);
            // 
            // rbnStatusA
            // 
            this.rbnStatusA.AccessibleDescription = "Todos em Aberto";
            this.rbnStatusA.AutoSize = true;
            this.rbnStatusA.Checked = true;
            this.rbnStatusA.Location = new System.Drawing.Point(24, 14);
            this.rbnStatusA.Name = "rbnStatusA";
            this.rbnStatusA.Size = new System.Drawing.Size(63, 17);
            this.rbnStatusA.TabIndex = 0;
            this.rbnStatusA.TabStop = true;
            this.rbnStatusA.Text = "Abertos";
            this.rbnStatusA.UseVisualStyleBackColor = true;
            this.rbnStatusA.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnStatusA.Click += new System.EventHandler(this.rbnStatusA_Click);
            // 
            // gbxUsuario
            // 
            this.gbxUsuario.Controls.Add(this.textBox1);
            this.gbxUsuario.Controls.Add(this.tbxUsuario);
            this.gbxUsuario.Location = new System.Drawing.Point(343, 12);
            this.gbxUsuario.Name = "gbxUsuario";
            this.gbxUsuario.Size = new System.Drawing.Size(129, 80);
            this.gbxUsuario.TabIndex = 129;
            this.gbxUsuario.TabStop = false;
            this.gbxUsuario.Text = "Usuario";
            this.gbxUsuario.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(110, 18);
            this.textBox1.TabIndex = 1;
            this.textBox1.Enter += new System.EventHandler(this.ControlEnter);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.textBox1.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // frmSolicitacaoVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxUsuario);
            this.Controls.Add(this.gbxFilial);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gbxPeriodo);
            this.Controls.Add(this.gbxStatus);
            this.Controls.Add(this.tspRequisicao);
            this.Controls.Add(this.dgvSolicitacao);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSolicitacaoVis";
            this.ShowInTaskbar = false;
            this.Tag = "34002";
            this.Text = "Solicitação de Compras - Visualização";
            this.toolTip1.SetToolTip(this, "Solicitação de Compras - Visualização");
            this.Activated += new System.EventHandler(this.frmSolicitacaoVis_Activated);
            this.Load += new System.EventHandler(this.frmSolicitacaoVis_Load);
            this.gbxFilial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitacao)).EndInit();
            this.tspRequisicao.ResumeLayout(false);
            this.tspRequisicao.PerformLayout();
            this.gbxPeriodo.ResumeLayout(false);
            this.gbxPeriodo.PerformLayout();
            this.gbxStatus.ResumeLayout(false);
            this.gbxStatus.PerformLayout();
            this.gbxUsuario.ResumeLayout(false);
            this.gbxUsuario.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip tspRequisicao;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbxPeriodo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxPeriodoAte;
        private System.Windows.Forms.TextBox tbxPeriodoDe;
        private System.Windows.Forms.GroupBox gbxStatus;
        private System.Windows.Forms.RadioButton rbnStatusA;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspImprimirMnu;
        private System.Windows.Forms.GroupBox gbxFilial;
        private System.Windows.Forms.ComboBox cbxFilial;
        private System.Windows.Forms.DataGridView dgvSolicitacao;
        private System.Windows.Forms.RadioButton rbnStatusF;
        private System.Windows.Forms.RadioButton rbnStatusC;
        private System.Windows.Forms.GroupBox gbxUsuario;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbxUsuario;
        private System.Windows.Forms.ToolStripButton tspExcluir;
    }
}