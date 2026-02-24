namespace CRCorrea
{
    partial class frmPagasVis
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.ttpPagas = new System.Windows.Forms.ToolTip(this.components);
            this.gbxTransporte = new System.Windows.Forms.GroupBox();
            this.dgvPagas = new System.Windows.Forms.DataGridView();
            this.gbxPeriodo = new System.Windows.Forms.GroupBox();
            this.tbxDataAte = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxDataDe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxOpcoes = new System.Windows.Forms.GroupBox();
            this.rbnTipoP = new System.Windows.Forms.RadioButton();
            this.rbnTipoT = new System.Windows.Forms.RadioButton();
            this.gbxPor = new System.Windows.Forms.GroupBox();
            this.rbnDataPag = new System.Windows.Forms.RadioButton();
            this.rbnDataVenc = new System.Windows.Forms.RadioButton();
            this.bwrPagas = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDFORNECEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DUPLICATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POSICAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POSICAOFIM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pbxPagas = new System.Windows.Forms.PictureBox();
            this.tspExtornar = new System.Windows.Forms.ToolStripButton();
            this.tspVisualizar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspTool.SuspendLayout();
            this.gbxTransporte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagas)).BeginInit();
            this.gbxPeriodo.SuspendLayout();
            this.gbxOpcoes.SuspendLayout();
            this.gbxPor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPagas)).BeginInit();
            this.SuspendLayout();
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspExtornar,
            this.tspVisualizar,
            this.tspImprimir,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(6, 528);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(979, 47);
            this.tspTool.TabIndex = 4;
            this.ttpPagas.SetToolTip(this.tspTool, "Menu");
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
            this.tslbLocalizar.Size = new System.Drawing.Size(64, 16);
            this.tslbLocalizar.Text = "Procurar";
            // 
            // tstbxLocalizar
            // 
            this.tstbxLocalizar.AccessibleDescription = "Procurar";
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(280, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // gbxTransporte
            // 
            this.gbxTransporte.AccessibleDescription = "Titulos";
            this.gbxTransporte.Controls.Add(this.pbxPagas);
            this.gbxTransporte.Controls.Add(this.dgvPagas);
            this.gbxTransporte.Controls.Add(this.tspTool);
            this.gbxTransporte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxTransporte.ForeColor = System.Drawing.Color.Black;
            this.gbxTransporte.Location = new System.Drawing.Point(12, 66);
            this.gbxTransporte.Name = "gbxTransporte";
            this.gbxTransporte.Size = new System.Drawing.Size(996, 571);
            this.gbxTransporte.TabIndex = 3;
            this.gbxTransporte.TabStop = false;
            this.gbxTransporte.Text = "Títulos";
            this.ttpPagas.SetToolTip(this.gbxTransporte, "Contas Pagas");
            // 
            // dgvPagas
            // 
            this.dgvPagas.AccessibleDescription = "Pagas";
            this.dgvPagas.AllowUserToAddRows = false;
            this.dgvPagas.AllowUserToDeleteRows = false;
            this.dgvPagas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvPagas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPagas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvPagas.BackgroundColor = System.Drawing.Color.White;
            this.dgvPagas.ColumnHeadersHeight = 28;
            this.dgvPagas.Location = new System.Drawing.Point(6, 20);
            this.dgvPagas.MultiSelect = false;
            this.dgvPagas.Name = "dgvPagas";
            this.dgvPagas.ReadOnly = true;
            this.dgvPagas.RowHeadersVisible = false;
            this.dgvPagas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPagas.Size = new System.Drawing.Size(984, 505);
            this.dgvPagas.TabIndex = 0;
            this.ttpPagas.SetToolTip(this.dgvPagas, "Grade de clsVisualização de Empresas Onibus");
            this.dgvPagas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagas_CellContentClick);
            this.dgvPagas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvPagas_fil_MouseDoubleClick);
            // 
            // gbxPeriodo
            // 
            this.gbxPeriodo.AccessibleDescription = "Período";
            this.gbxPeriodo.Controls.Add(this.tbxDataAte);
            this.gbxPeriodo.Controls.Add(this.label2);
            this.gbxPeriodo.Controls.Add(this.tbxDataDe);
            this.gbxPeriodo.Controls.Add(this.label1);
            this.gbxPeriodo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gbxPeriodo.Location = new System.Drawing.Point(151, 12);
            this.gbxPeriodo.Name = "gbxPeriodo";
            this.gbxPeriodo.Size = new System.Drawing.Size(244, 48);
            this.gbxPeriodo.TabIndex = 1;
            this.gbxPeriodo.TabStop = false;
            this.gbxPeriodo.Text = "Período";
            this.ttpPagas.SetToolTip(this.gbxPeriodo, "Grupo Periodo");
            // 
            // tbxDataAte
            // 
            this.tbxDataAte.AccessibleDescription = "Até";
            this.tbxDataAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataAte.Location = new System.Drawing.Point(154, 20);
            this.tbxDataAte.Name = "tbxDataAte";
            this.tbxDataAte.Size = new System.Drawing.Size(83, 21);
            this.tbxDataAte.TabIndex = 1;
            this.ttpPagas.SetToolTip(this.tbxDataAte, "Ate");
            this.tbxDataAte.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataAte.Leave += new System.EventHandler(this.tbxDataAte_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "até";
            // 
            // tbxDataDe
            // 
            this.tbxDataDe.AccessibleDescription = "De";
            this.tbxDataDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataDe.Location = new System.Drawing.Point(36, 20);
            this.tbxDataDe.Name = "tbxDataDe";
            this.tbxDataDe.Size = new System.Drawing.Size(83, 21);
            this.tbxDataDe.TabIndex = 0;
            this.ttpPagas.SetToolTip(this.tbxDataDe, "De");
            this.tbxDataDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataDe.Leave += new System.EventHandler(this.tbxDataAte_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "De:";
            // 
            // gbxOpcoes
            // 
            this.gbxOpcoes.AccessibleDescription = "Opções";
            this.gbxOpcoes.Controls.Add(this.rbnTipoP);
            this.gbxOpcoes.Controls.Add(this.rbnTipoT);
            this.gbxOpcoes.Cursor = System.Windows.Forms.Cursors.Default;
            this.gbxOpcoes.Location = new System.Drawing.Point(12, 12);
            this.gbxOpcoes.Name = "gbxOpcoes";
            this.gbxOpcoes.Size = new System.Drawing.Size(133, 48);
            this.gbxOpcoes.TabIndex = 0;
            this.gbxOpcoes.TabStop = false;
            this.ttpPagas.SetToolTip(this.gbxOpcoes, "Grupo Opções");
            // 
            // rbnTipoP
            // 
            this.rbnTipoP.AccessibleDescription = "Período";
            this.rbnTipoP.AutoSize = true;
            this.rbnTipoP.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbnTipoP.Location = new System.Drawing.Point(66, 20);
            this.rbnTipoP.Name = "rbnTipoP";
            this.rbnTipoP.Size = new System.Drawing.Size(61, 17);
            this.rbnTipoP.TabIndex = 1;
            this.rbnTipoP.Text = "Período";
            this.ttpPagas.SetToolTip(this.rbnTipoP, "Periodo");
            this.rbnTipoP.UseVisualStyleBackColor = true;
            this.rbnTipoP.CheckedChanged += new System.EventHandler(this.rbnValue_CheckedChanged);
            // 
            // rbnTipoT
            // 
            this.rbnTipoT.AccessibleDescription = "Todos";
            this.rbnTipoT.AutoSize = true;
            this.rbnTipoT.Checked = true;
            this.rbnTipoT.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbnTipoT.Location = new System.Drawing.Point(6, 20);
            this.rbnTipoT.Name = "rbnTipoT";
            this.rbnTipoT.Size = new System.Drawing.Size(54, 17);
            this.rbnTipoT.TabIndex = 0;
            this.rbnTipoT.TabStop = true;
            this.rbnTipoT.Text = "Todas";
            this.ttpPagas.SetToolTip(this.rbnTipoT, "Todas");
            this.rbnTipoT.UseVisualStyleBackColor = true;
            this.rbnTipoT.CheckedChanged += new System.EventHandler(this.rbnValue_CheckedChanged);
            // 
            // gbxPor
            // 
            this.gbxPor.AccessibleDescription = "Ordenador por:";
            this.gbxPor.Controls.Add(this.rbnDataPag);
            this.gbxPor.Controls.Add(this.rbnDataVenc);
            this.gbxPor.Cursor = System.Windows.Forms.Cursors.Default;
            this.gbxPor.Location = new System.Drawing.Point(401, 12);
            this.gbxPor.Name = "gbxPor";
            this.gbxPor.Size = new System.Drawing.Size(229, 48);
            this.gbxPor.TabIndex = 2;
            this.gbxPor.TabStop = false;
            this.gbxPor.Text = "Ordenador por:";
            this.ttpPagas.SetToolTip(this.gbxPor, "Grupo Opções");
            // 
            // rbnDataPag
            // 
            this.rbnDataPag.AccessibleDescription = "Data Pagamento";
            this.rbnDataPag.AutoSize = true;
            this.rbnDataPag.Checked = true;
            this.rbnDataPag.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbnDataPag.Location = new System.Drawing.Point(6, 20);
            this.rbnDataPag.Name = "rbnDataPag";
            this.rbnDataPag.Size = new System.Drawing.Size(105, 17);
            this.rbnDataPag.TabIndex = 1;
            this.rbnDataPag.TabStop = true;
            this.rbnDataPag.Text = "Data Pagamento";
            this.ttpPagas.SetToolTip(this.rbnDataPag, "Pagamento");
            this.rbnDataPag.UseVisualStyleBackColor = true;
            this.rbnDataPag.CheckedChanged += new System.EventHandler(this.rbnValue_CheckedChanged);
            // 
            // rbnDataVenc
            // 
            this.rbnDataVenc.AccessibleDescription = "Data Vencimento";
            this.rbnDataVenc.AutoSize = true;
            this.rbnDataVenc.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbnDataVenc.Location = new System.Drawing.Point(117, 20);
            this.rbnDataVenc.Name = "rbnDataVenc";
            this.rbnDataVenc.Size = new System.Drawing.Size(106, 17);
            this.rbnDataVenc.TabIndex = 0;
            this.rbnDataVenc.Text = "Data Vencimento";
            this.ttpPagas.SetToolTip(this.rbnDataVenc, "Vencimento");
            this.rbnDataVenc.UseVisualStyleBackColor = true;
            this.rbnDataVenc.CheckedChanged += new System.EventHandler(this.rbnValue_CheckedChanged);
            // 
            // bwrPagas
            // 
            this.bwrPagas.WorkerSupportsCancellation = true;
            this.bwrPagas.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrpagas_DoWork);
            this.bwrPagas.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrpagas_RunWorkerCompleted);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(636, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(372, 54);
            this.label5.TabIndex = 134;
            this.label5.Text = "Contas a Pagas";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 5;
            // 
            // IDFORNECEDOR
            // 
            this.IDFORNECEDOR.DataPropertyName = "IDFORNECEDOR";
            this.IDFORNECEDOR.HeaderText = "IDFORNECEDOR";
            this.IDFORNECEDOR.Name = "IDFORNECEDOR";
            this.IDFORNECEDOR.Visible = false;
            // 
            // DUPLICATA
            // 
            this.DUPLICATA.DataPropertyName = "DUPLICATA";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DUPLICATA.DefaultCellStyle = dataGridViewCellStyle2;
            this.DUPLICATA.HeaderText = "Duplicata";
            this.DUPLICATA.Name = "DUPLICATA";
            this.DUPLICATA.Width = 65;
            // 
            // POSICAO
            // 
            this.POSICAO.DataPropertyName = "POSICAO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.POSICAO.DefaultCellStyle = dataGridViewCellStyle3;
            this.POSICAO.HeaderText = "Posição";
            this.POSICAO.Name = "POSICAO";
            this.POSICAO.Width = 60;
            // 
            // POSICAOFIM
            // 
            this.POSICAOFIM.DataPropertyName = "POSICAOFIM";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.POSICAOFIM.DefaultCellStyle = dataGridViewCellStyle4;
            this.POSICAOFIM.HeaderText = "Pos. Fim";
            this.POSICAOFIM.Name = "POSICAOFIM";
            this.POSICAOFIM.Width = 60;
            // 
            // EMISSAO
            // 
            this.EMISSAO.DataPropertyName = "EMISSAO";
            this.EMISSAO.HeaderText = "Emissao";
            this.EMISSAO.Name = "EMISSAO";
            this.EMISSAO.Width = 80;
            // 
            // pbxPagas
            // 
            this.pbxPagas.AccessibleDescription = "Carregando Pagas";
            this.pbxPagas.BackColor = System.Drawing.Color.White;
            this.pbxPagas.Location = new System.Drawing.Point(6, 20);
            this.pbxPagas.Name = "pbxPagas";
            this.pbxPagas.Size = new System.Drawing.Size(984, 505);
            this.pbxPagas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxPagas.TabIndex = 139;
            this.pbxPagas.TabStop = false;
            this.ttpPagas.SetToolTip(this.pbxPagas, "Imagem");
            this.pbxPagas.Visible = false;
            // 
            // tspExtornar
            // 
            this.tspExtornar.AccessibleDescription = "Estornar Pagto";
            this.tspExtornar.AutoSize = false;
            this.tspExtornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExtornar.Image = global::CRCorrea.Properties.Resources.Cancelar;
            this.tspExtornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExtornar.Name = "tspExtornar";
            this.tspExtornar.Size = new System.Drawing.Size(96, 42);
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
            this.tspVisualizar.Image = global::CRCorrea.Properties.Resources.Editar;
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
            this.tspImprimir.Image = global::CRCorrea.Properties.Resources.Imprimir;
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(88, 42);
            this.tspImprimir.Text = "Mnu Imprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = global::CRCorrea.Properties.Resources.Retornar;
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // frmPagasVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbxPor);
            this.Controls.Add(this.gbxPeriodo);
            this.Controls.Add(this.gbxOpcoes);
            this.Controls.Add(this.gbxTransporte);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPagasVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4013";
            this.Text = "Contas Pagas - clsVisualização";
            this.ttpPagas.SetToolTip(this, "Contas Pagas - clsVisualização");
            this.Activated += new System.EventHandler(this.frmpagasVis_Activated);
            this.Load += new System.EventHandler(this.frmpagasVis_Load);
            this.Shown += new System.EventHandler(this.frmpagasVis_Shown);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxTransporte.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagas)).EndInit();
            this.gbxPeriodo.ResumeLayout(false);
            this.gbxPeriodo.PerformLayout();
            this.gbxOpcoes.ResumeLayout(false);
            this.gbxOpcoes.PerformLayout();
            this.gbxPor.ResumeLayout(false);
            this.gbxPor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPagas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip ttpPagas;
        private System.Windows.Forms.GroupBox gbxTransporte;
        private System.Windows.Forms.DataGridView dgvPagas;
        private System.ComponentModel.BackgroundWorker bwrPagas;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripButton tspVisualizar;
        private System.Windows.Forms.GroupBox gbxPeriodo;
        private System.Windows.Forms.TextBox tbxDataAte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxDataDe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxOpcoes;
        private System.Windows.Forms.RadioButton rbnTipoP;
        private System.Windows.Forms.RadioButton rbnTipoT;
        private System.Windows.Forms.GroupBox gbxPor;
        private System.Windows.Forms.RadioButton rbnDataPag;
        private System.Windows.Forms.RadioButton rbnDataVenc;
        private System.Windows.Forms.ToolStripButton tspExtornar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbxPagas;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDFORNECEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn DUPLICATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn POSICAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn POSICAOFIM;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMISSAO;
    }
}