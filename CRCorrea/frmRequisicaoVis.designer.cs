namespace CRCorrea
{
    partial class frmRequisicaoVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRequisicaoVis));
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dgvRequisicao = new System.Windows.Forms.DataGridView();
            this.gbxItensRequisicao = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvRequisicao1 = new System.Windows.Forms.DataGridView();
            this.gbxRequisicao = new System.Windows.Forms.GroupBox();
            this.tspRequisicao = new System.Windows.Forms.ToolStrip();
            this.tspRequisicaoIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspRequisicaoAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspRequisicaoImprimirEPI = new System.Windows.Forms.ToolStripButton();
            this.tspRequisicaoImprimirMnu = new System.Windows.Forms.ToolStripButton();
            this.rbnAno = new System.Windows.Forms.RadioButton();
            this.rbnTodas = new System.Windows.Forms.RadioButton();
            this.gbxOpcoes = new System.Windows.Forms.GroupBox();
            this.nudRequisicao = new System.Windows.Forms.NumericUpDown();
            this.tspRequisicao1 = new System.Windows.Forms.ToolStrip();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tbxPesquisa = new System.Windows.Forms.ToolStripTextBox();
            this.label25 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequisicao)).BeginInit();
            this.gbxItensRequisicao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequisicao1)).BeginInit();
            this.gbxRequisicao.SuspendLayout();
            this.tspRequisicao.SuspendLayout();
            this.gbxOpcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRequisicao)).BeginInit();
            this.tspRequisicao1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRequisicao
            // 
            this.dgvRequisicao.AccessibleDescription = "Requisições";
            this.dgvRequisicao.AllowUserToAddRows = false;
            this.dgvRequisicao.AllowUserToDeleteRows = false;
            this.dgvRequisicao.AllowUserToOrderColumns = true;
            this.dgvRequisicao.AllowUserToResizeRows = false;
            this.dgvRequisicao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRequisicao.BackgroundColor = System.Drawing.Color.White;
            this.dgvRequisicao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequisicao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvRequisicao.Location = new System.Drawing.Point(6, 19);
            this.dgvRequisicao.MultiSelect = false;
            this.dgvRequisicao.Name = "dgvRequisicao";
            this.dgvRequisicao.ReadOnly = true;
            this.dgvRequisicao.RowHeadersVisible = false;
            this.dgvRequisicao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvRequisicao.Size = new System.Drawing.Size(294, 432);
            this.dgvRequisicao.StandardTab = true;
            this.dgvRequisicao.TabIndex = 125;
            this.ToolTip.SetToolTip(this.dgvRequisicao, "Requisição");
            this.dgvRequisicao.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRequisicao_CellClick);
            this.dgvRequisicao.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRequisicao_CellContentDoubleClick);
            // 
            // gbxItensRequisicao
            // 
            this.gbxItensRequisicao.AccessibleDescription = "Itens da Requisição";
            this.gbxItensRequisicao.Controls.Add(this.label2);
            this.gbxItensRequisicao.Controls.Add(this.label1);
            this.gbxItensRequisicao.Controls.Add(this.dgvRequisicao1);
            this.gbxItensRequisicao.Location = new System.Drawing.Point(325, 62);
            this.gbxItensRequisicao.Name = "gbxItensRequisicao";
            this.gbxItensRequisicao.Size = new System.Drawing.Size(683, 515);
            this.gbxItensRequisicao.TabIndex = 2;
            this.gbxItensRequisicao.TabStop = false;
            this.gbxItensRequisicao.Text = "Itens da Requisição";
            this.ToolTip.SetToolTip(this.gbxItensRequisicao, "Grupo ItensRequisicao");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 13);
            this.label2.TabIndex = 133;
            this.label2.Text = "E/S = (E = Entrada Estoque) (S = Saida Estoque)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 474);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 13);
            this.label1.TabIndex = 132;
            this.label1.Text = "Tp = (N = Não Retorna) (S = Sim Retorna)";
            // 
            // dgvRequisicao1
            // 
            this.dgvRequisicao1.AccessibleDescription = "Itens da Requisição";
            this.dgvRequisicao1.AllowUserToAddRows = false;
            this.dgvRequisicao1.AllowUserToDeleteRows = false;
            this.dgvRequisicao1.AllowUserToOrderColumns = true;
            this.dgvRequisicao1.AllowUserToResizeRows = false;
            this.dgvRequisicao1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRequisicao1.BackgroundColor = System.Drawing.Color.White;
            this.dgvRequisicao1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequisicao1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvRequisicao1.Location = new System.Drawing.Point(6, 18);
            this.dgvRequisicao1.MultiSelect = false;
            this.dgvRequisicao1.Name = "dgvRequisicao1";
            this.dgvRequisicao1.ReadOnly = true;
            this.dgvRequisicao1.RowHeadersVisible = false;
            this.dgvRequisicao1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvRequisicao1.Size = new System.Drawing.Size(671, 427);
            this.dgvRequisicao1.StandardTab = true;
            this.dgvRequisicao1.TabIndex = 125;
            this.ToolTip.SetToolTip(this.dgvRequisicao1, "Itens da Requisição");
            // 
            // gbxRequisicao
            // 
            this.gbxRequisicao.AccessibleDescription = "Requisições";
            this.gbxRequisicao.Controls.Add(this.tspRequisicao);
            this.gbxRequisicao.Controls.Add(this.dgvRequisicao);
            this.gbxRequisicao.Location = new System.Drawing.Point(12, 62);
            this.gbxRequisicao.Name = "gbxRequisicao";
            this.gbxRequisicao.Size = new System.Drawing.Size(307, 515);
            this.gbxRequisicao.TabIndex = 1;
            this.gbxRequisicao.TabStop = false;
            this.gbxRequisicao.Text = "Requisições";
            this.ToolTip.SetToolTip(this.gbxRequisicao, "Grupo das Requisições");
            // 
            // tspRequisicao
            // 
            this.tspRequisicao.AccessibleDescription = "Barra de Opções";
            this.tspRequisicao.AllowMerge = false;
            this.tspRequisicao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspRequisicao.AutoSize = false;
            this.tspRequisicao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tspRequisicao.Dock = System.Windows.Forms.DockStyle.None;
            this.tspRequisicao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspRequisicaoIncluir,
            this.tspRequisicaoAlterar,
            this.tspRequisicaoImprimirEPI,
            this.tspRequisicaoImprimirMnu});
            this.tspRequisicao.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspRequisicao.Location = new System.Drawing.Point(2, 467);
            this.tspRequisicao.Name = "tspRequisicao";
            this.tspRequisicao.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspRequisicao.Size = new System.Drawing.Size(302, 35);
            this.tspRequisicao.TabIndex = 132;
            // 
            // tspRequisicaoIncluir
            // 
            this.tspRequisicaoIncluir.AccessibleDescription = "Incluir";
            this.tspRequisicaoIncluir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRequisicaoIncluir.ForeColor = System.Drawing.Color.Black;
            this.tspRequisicaoIncluir.Image = ((System.Drawing.Image)(resources.GetObject("tspRequisicaoIncluir.Image")));
            this.tspRequisicaoIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRequisicaoIncluir.Name = "tspRequisicaoIncluir";
            this.tspRequisicaoIncluir.Size = new System.Drawing.Size(42, 31);
            this.tspRequisicaoIncluir.Text = "Incluir";
            this.tspRequisicaoIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRequisicaoIncluir.Click += new System.EventHandler(this.tspRequisicaoIncluir_Click);
            // 
            // tspRequisicaoAlterar
            // 
            this.tspRequisicaoAlterar.AccessibleDescription = "Procurar";
            this.tspRequisicaoAlterar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRequisicaoAlterar.ForeColor = System.Drawing.Color.Black;
            this.tspRequisicaoAlterar.Image = ((System.Drawing.Image)(resources.GetObject("tspRequisicaoAlterar.Image")));
            this.tspRequisicaoAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRequisicaoAlterar.Name = "tspRequisicaoAlterar";
            this.tspRequisicaoAlterar.Size = new System.Drawing.Size(46, 31);
            this.tspRequisicaoAlterar.Text = "Alterar";
            this.tspRequisicaoAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRequisicaoAlterar.Click += new System.EventHandler(this.tspRequisicaoAlterar_Click);
            // 
            // tspRequisicaoImprimirEPI
            // 
            this.tspRequisicaoImprimirEPI.AccessibleDescription = "Imprimir Recibo - EPI";
            this.tspRequisicaoImprimirEPI.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRequisicaoImprimirEPI.ForeColor = System.Drawing.Color.Black;
            this.tspRequisicaoImprimirEPI.Image = ((System.Drawing.Image)(resources.GetObject("tspRequisicaoImprimirEPI.Image")));
            this.tspRequisicaoImprimirEPI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRequisicaoImprimirEPI.Name = "tspRequisicaoImprimirEPI";
            this.tspRequisicaoImprimirEPI.Size = new System.Drawing.Size(118, 31);
            this.tspRequisicaoImprimirEPI.Text = "Imprimir Recibo - EPI";
            this.tspRequisicaoImprimirEPI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRequisicaoImprimirEPI.Click += new System.EventHandler(this.tspRequisicaoImprimirEPI_Click);
            // 
            // tspRequisicaoImprimirMnu
            // 
            this.tspRequisicaoImprimirMnu.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRequisicaoImprimirMnu.ForeColor = System.Drawing.Color.Black;
            this.tspRequisicaoImprimirMnu.Image = ((System.Drawing.Image)(resources.GetObject("tspRequisicaoImprimirMnu.Image")));
            this.tspRequisicaoImprimirMnu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRequisicaoImprimirMnu.Name = "tspRequisicaoImprimirMnu";
            this.tspRequisicaoImprimirMnu.Size = new System.Drawing.Size(78, 31);
            this.tspRequisicaoImprimirMnu.Text = "Imprimir Mnu";
            this.tspRequisicaoImprimirMnu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRequisicaoImprimirMnu.Click += new System.EventHandler(this.tspRequisicaoImprimirMnu_Click);
            // 
            // rbnAno
            // 
            this.rbnAno.AccessibleDescription = "Do ano";
            this.rbnAno.AutoSize = true;
            this.rbnAno.Checked = true;
            this.rbnAno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAno.Location = new System.Drawing.Point(66, 16);
            this.rbnAno.Name = "rbnAno";
            this.rbnAno.Size = new System.Drawing.Size(60, 17);
            this.rbnAno.TabIndex = 2;
            this.rbnAno.TabStop = true;
            this.rbnAno.Text = "Do Ano";
            this.ToolTip.SetToolTip(this.rbnAno, "Do Ano");
            this.rbnAno.UseVisualStyleBackColor = true;
            this.rbnAno.Click += new System.EventHandler(this.rbnAno_Click);
            this.rbnAno.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAno.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnTodas
            // 
            this.rbnTodas.AccessibleDescription = "Todas";
            this.rbnTodas.AutoSize = true;
            this.rbnTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTodas.Location = new System.Drawing.Point(6, 16);
            this.rbnTodas.Name = "rbnTodas";
            this.rbnTodas.Size = new System.Drawing.Size(54, 17);
            this.rbnTodas.TabIndex = 1;
            this.rbnTodas.Text = "Todas";
            this.ToolTip.SetToolTip(this.rbnTodas, "Todas");
            this.rbnTodas.UseVisualStyleBackColor = true;
            this.rbnTodas.Click += new System.EventHandler(this.rbnTodas_Click);
            this.rbnTodas.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTodas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTodas.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxOpcoes
            // 
            this.gbxOpcoes.AccessibleDescription = "Opções";
            this.gbxOpcoes.Controls.Add(this.nudRequisicao);
            this.gbxOpcoes.Controls.Add(this.rbnAno);
            this.gbxOpcoes.Controls.Add(this.rbnTodas);
            this.gbxOpcoes.Location = new System.Drawing.Point(12, 12);
            this.gbxOpcoes.Name = "gbxOpcoes";
            this.gbxOpcoes.Size = new System.Drawing.Size(196, 44);
            this.gbxOpcoes.TabIndex = 0;
            this.gbxOpcoes.TabStop = false;
            this.ToolTip.SetToolTip(this.gbxOpcoes, "Grupo  - Filtro");
            // 
            // nudRequisicao
            // 
            this.nudRequisicao.AccessibleDescription = "Ano";
            this.nudRequisicao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nudRequisicao.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRequisicao.Location = new System.Drawing.Point(129, 14);
            this.nudRequisicao.Maximum = new decimal(new int[] {
            2060,
            0,
            0,
            0});
            this.nudRequisicao.Minimum = new decimal(new int[] {
            1999,
            0,
            0,
            0});
            this.nudRequisicao.Name = "nudRequisicao";
            this.nudRequisicao.Size = new System.Drawing.Size(55, 21);
            this.nudRequisicao.TabIndex = 3;
            this.nudRequisicao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ToolTip.SetToolTip(this.nudRequisicao, "Ano");
            this.nudRequisicao.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudRequisicao.Click += new System.EventHandler(this.nudRequisicao_Click);
            this.nudRequisicao.Enter += new System.EventHandler(this.ControlEnter);
            this.nudRequisicao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.nudRequisicao.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tspRequisicao1
            // 
            this.tspRequisicao1.AccessibleDescription = "Rtornar";
            this.tspRequisicao1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspRequisicao1.AutoSize = false;
            this.tspRequisicao1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspRequisicao1.Dock = System.Windows.Forms.DockStyle.None;
            this.tspRequisicao1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRequisicao1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspRequisicao1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspRetornar,
            this.toolStripSeparator1,
            this.tslbLocalizar,
            this.tbxPesquisa});
            this.tspRequisicao1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspRequisicao1.Location = new System.Drawing.Point(9, 580);
            this.tspRequisicao1.Name = "tspRequisicao1";
            this.tspRequisicao1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspRequisicao1.Size = new System.Drawing.Size(1020, 47);
            this.tspRequisicao1.TabIndex = 3;
            // 
            // tspRetornar
            // 
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(64, 16);
            this.tslbLocalizar.Text = "Procurar";
            // 
            // tbxPesquisa
            // 
            this.tbxPesquisa.AccessibleDescription = "Procurar";
            this.tbxPesquisa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxPesquisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxPesquisa.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPesquisa.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tbxPesquisa.Name = "tbxPesquisa";
            this.tbxPesquisa.Size = new System.Drawing.Size(200, 21);
            this.tbxPesquisa.ToolTipText = "Procurar";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(441, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(486, 25);
            this.label25.TabIndex = 62;
            this.label25.Text = "Requisições de Materiais [RM] (Visualização)";
            // 
            // frmRequisicaoVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.tspRequisicao1);
            this.Controls.Add(this.gbxOpcoes);
            this.Controls.Add(this.gbxItensRequisicao);
            this.Controls.Add(this.gbxRequisicao);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRequisicaoVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "34340";
            this.Text = "Requisicao de Material - Visualização";
            this.ToolTip.SetToolTip(this, "Requisicao de Material - Visualização");
            this.Activated += new System.EventHandler(this.frmRequisicaoVis_Activated);
            this.Load += new System.EventHandler(this.frmRequisicao_ApliVis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequisicao)).EndInit();
            this.gbxItensRequisicao.ResumeLayout(false);
            this.gbxItensRequisicao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequisicao1)).EndInit();
            this.gbxRequisicao.ResumeLayout(false);
            this.tspRequisicao.ResumeLayout(false);
            this.tspRequisicao.PerformLayout();
            this.gbxOpcoes.ResumeLayout(false);
            this.gbxOpcoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRequisicao)).EndInit();
            this.tspRequisicao1.ResumeLayout(false);
            this.tspRequisicao1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.GroupBox gbxRequisicao;
        private System.Windows.Forms.DataGridView dgvRequisicao;
        private System.Windows.Forms.GroupBox gbxItensRequisicao;
        private System.Windows.Forms.DataGridView dgvRequisicao1;
        private System.Windows.Forms.GroupBox gbxOpcoes;
        private System.Windows.Forms.RadioButton rbnAno;
        private System.Windows.Forms.RadioButton rbnTodas;
        private System.Windows.Forms.ToolStrip tspRequisicao1;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.NumericUpDown nudRequisicao;
        private System.Windows.Forms.ToolStrip tspRequisicao;
        private System.Windows.Forms.ToolStripButton tspRequisicaoIncluir;
        private System.Windows.Forms.ToolStripButton tspRequisicaoAlterar;
        private System.Windows.Forms.ToolStripButton tspRequisicaoImprimirEPI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tbxPesquisa;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ToolStripButton tspRequisicaoImprimirMnu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}