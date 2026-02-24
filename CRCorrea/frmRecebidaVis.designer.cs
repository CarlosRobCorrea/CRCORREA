namespace CRCorrea
{
    partial class frmRecebidaVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecebidaVis));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspExtornar = new System.Windows.Forms.ToolStripButton();
            this.tspVisualizar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspMnuImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstPesquisa = new System.Windows.Forms.ToolStripTextBox();
            this.ttpPagas = new System.Windows.Forms.ToolTip(this.components);
            this.gbxRecebida = new System.Windows.Forms.GroupBox();
            this.pbxRecebida = new System.Windows.Forms.PictureBox();
            this.dgvRecebida = new System.Windows.Forms.DataGridView();
            this.gbxPeriodo = new System.Windows.Forms.GroupBox();
            this.tbxDataAte = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxDataDe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxPor = new System.Windows.Forms.GroupBox();
            this.rbnDataVenc = new System.Windows.Forms.RadioButton();
            this.rbnDataPag = new System.Windows.Forms.RadioButton();
            this.gbxOpcoes = new System.Windows.Forms.GroupBox();
            this.rbnApartirdeHoje = new System.Windows.Forms.RadioButton();
            this.rbnTodos = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.tspTool.SuspendLayout();
            this.gbxRecebida.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxRecebida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecebida)).BeginInit();
            this.gbxPeriodo.SuspendLayout();
            this.gbxPor.SuspendLayout();
            this.gbxOpcoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspExtornar,
            this.tspVisualizar,
            this.tspImprimir,
            this.tspMnuImprimir,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstPesquisa});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(12, 638);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(774, 47);
            this.tspTool.TabIndex = 4;
            this.ttpPagas.SetToolTip(this.tspTool, "Menu");
            this.tspTool.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tspTool_ItemClicked);
            // 
            // tspExtornar
            // 
            this.tspExtornar.AccessibleDescription = "Estornar Pagto";
            this.tspExtornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExtornar.Image = ((System.Drawing.Image)(resources.GetObject("tspExtornar.Image")));
            this.tspExtornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExtornar.Name = "tspExtornar";
            this.tspExtornar.Size = new System.Drawing.Size(95, 41);
            this.tspExtornar.Text = "Estornar Pagto";
            this.tspExtornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExtornar.ToolTipText = "Extornar Pagamento";
            this.tspExtornar.Click += new System.EventHandler(this.tspExtornar_Click);
            // 
            // tspVisualizar
            // 
            this.tspVisualizar.AccessibleDescription = "Visualizar";
            this.tspVisualizar.AutoSize = false;
            this.tspVisualizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspVisualizar.Image = ((System.Drawing.Image)(resources.GetObject("tspVisualizar.Image")));
            this.tspVisualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspVisualizar.Name = "tspVisualizar";
            this.tspVisualizar.Size = new System.Drawing.Size(66, 42);
            this.tspVisualizar.Text = "Visualizar";
            this.tspVisualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspVisualizar.Click += new System.EventHandler(this.tspVisualizar_Click);
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
            this.tspImprimir.Text = "Imprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
            // 
            // tspMnuImprimir
            // 
            this.tspMnuImprimir.AccessibleDescription = "Menu Rel.";
            this.tspMnuImprimir.AutoSize = false;
            this.tspMnuImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspMnuImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspMnuImprimir.Image")));
            this.tspMnuImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspMnuImprimir.Name = "tspMnuImprimir";
            this.tspMnuImprimir.Size = new System.Drawing.Size(66, 42);
            this.tspMnuImprimir.Text = "Menu Rel.";
            this.tspMnuImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspMnuImprimir.ToolTipText = "Menu Imprimir";
            this.tspMnuImprimir.Click += new System.EventHandler(this.tspMnuImprimir_Click);
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
            this.tspRetornar.Text = "Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // tspSeparador1
            // 
            this.tspSeparador1.AutoSize = false;
            this.tspSeparador1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.tspSeparador1.Name = "tspSeparador1";
            this.tspSeparador1.Size = new System.Drawing.Size(6, 47);
            // 
            // tslbLocalizar
            // 
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(65, 16);
            this.tslbLocalizar.Text = "Procurar";
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
            this.tstPesquisa.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tstPesquisa_MouseUp);
            // 
            // gbxRecebida
            // 
            this.gbxRecebida.AccessibleDescription = "Contas Pagas";
            this.gbxRecebida.Controls.Add(this.pbxRecebida);
            this.gbxRecebida.Controls.Add(this.dgvRecebida);
            this.gbxRecebida.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxRecebida.ForeColor = System.Drawing.Color.Black;
            this.gbxRecebida.Location = new System.Drawing.Point(12, 83);
            this.gbxRecebida.Name = "gbxRecebida";
            this.gbxRecebida.Size = new System.Drawing.Size(1005, 552);
            this.gbxRecebida.TabIndex = 3;
            this.gbxRecebida.TabStop = false;
            this.gbxRecebida.Text = "Contas Recebidas";
            this.ttpPagas.SetToolTip(this.gbxRecebida, "Contas Pagas");
            // 
            // pbxRecebida
            // 
            this.pbxRecebida.AccessibleDescription = "Carregando Contas Recebidas";
            this.pbxRecebida.BackColor = System.Drawing.Color.White;
            this.pbxRecebida.Image = ((System.Drawing.Image)(resources.GetObject("pbxRecebida.Image")));
            this.pbxRecebida.Location = new System.Drawing.Point(16, 38);
            this.pbxRecebida.Name = "pbxRecebida";
            this.pbxRecebida.Size = new System.Drawing.Size(965, 493);
            this.pbxRecebida.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxRecebida.TabIndex = 138;
            this.pbxRecebida.TabStop = false;
            this.ttpPagas.SetToolTip(this.pbxRecebida, "Imagem");
            this.pbxRecebida.Visible = false;
            // 
            // dgvRecebida
            // 
            this.dgvRecebida.AccessibleDescription = "Contas Recebidas";
            this.dgvRecebida.AllowUserToAddRows = false;
            this.dgvRecebida.AllowUserToDeleteRows = false;
            this.dgvRecebida.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvRecebida.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecebida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvRecebida.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecebida.ColumnHeadersHeight = 28;
            this.dgvRecebida.Location = new System.Drawing.Point(6, 19);
            this.dgvRecebida.MultiSelect = false;
            this.dgvRecebida.Name = "dgvRecebida";
            this.dgvRecebida.ReadOnly = true;
            this.dgvRecebida.RowHeadersVisible = false;
            this.dgvRecebida.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvRecebida.Size = new System.Drawing.Size(991, 527);
            this.dgvRecebida.TabIndex = 0;
            this.ttpPagas.SetToolTip(this.dgvRecebida, "Grade de clsVisualização de Contas Recebidas");
            this.dgvRecebida.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvRecebida_MouseDoubleClick);
            // 
            // gbxPeriodo
            // 
            this.gbxPeriodo.AccessibleDescription = "Período";
            this.gbxPeriodo.Controls.Add(this.tbxDataAte);
            this.gbxPeriodo.Controls.Add(this.label2);
            this.gbxPeriodo.Controls.Add(this.tbxDataDe);
            this.gbxPeriodo.Controls.Add(this.label1);
            this.gbxPeriodo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gbxPeriodo.Location = new System.Drawing.Point(361, 13);
            this.gbxPeriodo.Name = "gbxPeriodo";
            this.gbxPeriodo.Size = new System.Drawing.Size(197, 57);
            this.gbxPeriodo.TabIndex = 1;
            this.gbxPeriodo.TabStop = false;
            this.gbxPeriodo.Text = "Período";
            this.ttpPagas.SetToolTip(this.gbxPeriodo, "Grupo Periodo");
            // 
            // tbxDataAte
            // 
            this.tbxDataAte.AccessibleDescription = "Data Pagamento Até";
            this.tbxDataAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataAte.Location = new System.Drawing.Point(104, 25);
            this.tbxDataAte.Name = "tbxDataAte";
            this.tbxDataAte.Size = new System.Drawing.Size(83, 21);
            this.tbxDataAte.TabIndex = 1;
            this.ttpPagas.SetToolTip(this.tbxDataAte, "Ate");
            this.tbxDataAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataAte.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxDataAte.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Até";
            // 
            // tbxDataDe
            // 
            this.tbxDataDe.AccessibleDescription = "Data Pagamento De";
            this.tbxDataDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataDe.Location = new System.Drawing.Point(6, 25);
            this.tbxDataDe.Name = "tbxDataDe";
            this.tbxDataDe.Size = new System.Drawing.Size(83, 21);
            this.tbxDataDe.TabIndex = 0;
            this.ttpPagas.SetToolTip(this.tbxDataDe, "De");
            this.tbxDataDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataDe.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxDataDe.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "De:";
            // 
            // gbxPor
            // 
            this.gbxPor.AccessibleDescription = "Opção";
            this.gbxPor.AccessibleName = "Visualizar por:";
            this.gbxPor.Controls.Add(this.rbnDataVenc);
            this.gbxPor.Controls.Add(this.rbnDataPag);
            this.gbxPor.Cursor = System.Windows.Forms.Cursors.Default;
            this.gbxPor.Location = new System.Drawing.Point(218, 13);
            this.gbxPor.Name = "gbxPor";
            this.gbxPor.Size = new System.Drawing.Size(137, 57);
            this.gbxPor.TabIndex = 2;
            this.gbxPor.TabStop = false;
            this.gbxPor.Text = "Visualizar por:";
            this.ttpPagas.SetToolTip(this.gbxPor, "Grupo Opções");
            // 
            // rbnDataVenc
            // 
            this.rbnDataVenc.AccessibleDescription = "Por Data Vencimento";
            this.rbnDataVenc.AutoSize = true;
            this.rbnDataVenc.Checked = true;
            this.rbnDataVenc.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbnDataVenc.Location = new System.Drawing.Point(7, 37);
            this.rbnDataVenc.Name = "rbnDataVenc";
            this.rbnDataVenc.Size = new System.Drawing.Size(125, 17);
            this.rbnDataVenc.TabIndex = 1;
            this.rbnDataVenc.TabStop = true;
            this.rbnDataVenc.Text = "Por Data Vencimento";
            this.ttpPagas.SetToolTip(this.rbnDataVenc, "Vencimento");
            this.rbnDataVenc.UseVisualStyleBackColor = true;
            this.rbnDataVenc.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnDataVenc.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnDataVenc.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnDataVenc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // rbnDataPag
            // 
            this.rbnDataPag.AccessibleDescription = "Por Data Pagamento";
            this.rbnDataPag.AutoSize = true;
            this.rbnDataPag.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbnDataPag.Location = new System.Drawing.Point(8, 13);
            this.rbnDataPag.Name = "rbnDataPag";
            this.rbnDataPag.Size = new System.Drawing.Size(124, 17);
            this.rbnDataPag.TabIndex = 0;
            this.rbnDataPag.Text = "Por Data Pagamento";
            this.ttpPagas.SetToolTip(this.rbnDataPag, "Pagamento");
            this.rbnDataPag.UseVisualStyleBackColor = true;
            this.rbnDataPag.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnDataPag.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnDataPag.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnDataPag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // gbxOpcoes
            // 
            this.gbxOpcoes.AccessibleDescription = "Situação da clsVisualização dos Títulos";
            this.gbxOpcoes.Controls.Add(this.rbnApartirdeHoje);
            this.gbxOpcoes.Controls.Add(this.rbnTodos);
            this.gbxOpcoes.Location = new System.Drawing.Point(18, 13);
            this.gbxOpcoes.Name = "gbxOpcoes";
            this.gbxOpcoes.Size = new System.Drawing.Size(194, 57);
            this.gbxOpcoes.TabIndex = 14;
            this.gbxOpcoes.TabStop = false;
            this.gbxOpcoes.Text = "Situação da clsVisualização dos Títulos";
            this.ttpPagas.SetToolTip(this.gbxOpcoes, "Situação da clsVisualização dos Títulos");
            // 
            // rbnApartirdeHoje
            // 
            this.rbnApartirdeHoje.AccessibleDescription = "Período";
            this.rbnApartirdeHoje.AccessibleName = "";
            this.rbnApartirdeHoje.AutoSize = true;
            this.rbnApartirdeHoje.Checked = true;
            this.rbnApartirdeHoje.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnApartirdeHoje.Location = new System.Drawing.Point(81, 16);
            this.rbnApartirdeHoje.Name = "rbnApartirdeHoje";
            this.rbnApartirdeHoje.Size = new System.Drawing.Size(61, 17);
            this.rbnApartirdeHoje.TabIndex = 1;
            this.rbnApartirdeHoje.TabStop = true;
            this.rbnApartirdeHoje.Text = "Período";
            this.rbnApartirdeHoje.UseVisualStyleBackColor = true;
            this.rbnApartirdeHoje.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            // 
            // rbnTodos
            // 
            this.rbnTodos.AccessibleDescription = "Todos";
            this.rbnTodos.AccessibleName = "";
            this.rbnTodos.AutoSize = true;
            this.rbnTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTodos.Location = new System.Drawing.Point(6, 16);
            this.rbnTodos.Name = "rbnTodos";
            this.rbnTodos.Size = new System.Drawing.Size(54, 17);
            this.rbnTodos.TabIndex = 0;
            this.rbnTodos.TabStop = true;
            this.rbnTodos.Text = "Todos";
            this.rbnTodos.UseVisualStyleBackColor = true;
            this.rbnTodos.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(775, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 29);
            this.label5.TabIndex = 135;
            this.label5.Text = "Ctas Recebidas";
            // 
            // frmRecebidaVis
            // 
            this.AccessibleDescription = "Contas Recebidas - clsVisualização";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 685);
            this.ControlBox = false;
            this.Controls.Add(this.gbxOpcoes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbxPor);
            this.Controls.Add(this.gbxPeriodo);
            this.Controls.Add(this.gbxRecebida);
            this.Controls.Add(this.tspTool);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRecebidaVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4013";
            this.Text = "Contas Recebidas - clsVisualização";
            this.ttpPagas.SetToolTip(this, "Contas Recebidas - clsVisualização");
            this.Load += new System.EventHandler(this.frmRecebidaVis_Load);
            this.Shown += new System.EventHandler(this.frmRecebidaVis_Shown);
            this.Activated += new System.EventHandler(this.frmRecebidaVis_Activated);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxRecebida.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxRecebida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecebida)).EndInit();
            this.gbxPeriodo.ResumeLayout(false);
            this.gbxPeriodo.PerformLayout();
            this.gbxPor.ResumeLayout(false);
            this.gbxPor.PerformLayout();
            this.gbxOpcoes.ResumeLayout(false);
            this.gbxOpcoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip ttpPagas;
        private System.Windows.Forms.GroupBox gbxRecebida;
        private System.Windows.Forms.DataGridView dgvRecebida;
        private System.ComponentModel.BackgroundWorker bwrRecebida;
        private System.Windows.Forms.PictureBox pbxRecebida;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripTextBox tstPesquisa;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripButton tspVisualizar;
        private System.Windows.Forms.GroupBox gbxPeriodo;
        private System.Windows.Forms.TextBox tbxDataAte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxDataDe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxPor;
        private System.Windows.Forms.RadioButton rbnDataPag;
        private System.Windows.Forms.ToolStripButton tspExtornar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbnDataVenc;
        private System.Windows.Forms.ToolStripButton tspMnuImprimir;
        private System.Windows.Forms.GroupBox gbxOpcoes;
        private System.Windows.Forms.RadioButton rbnTodos;
        private System.Windows.Forms.RadioButton rbnApartirdeHoje;
    }
}