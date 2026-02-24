namespace CRCorrea
{
    partial class frmPedidoVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPedidoVis));
            this.ttpPedido = new System.Windows.Forms.ToolTip(this.components);
            this.dgvPedido = new System.Windows.Forms.DataGridView();
            this.gbxPedido = new System.Windows.Forms.GroupBox();
            this.dgvPedidoPendente = new System.Windows.Forms.DataGridView();
            this.gbxResumoPedido = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxTotalvalorpendente = new System.Windows.Forms.TextBox();
            this.gbxPedido1 = new System.Windows.Forms.GroupBox();
            this.dgvPedido1 = new System.Windows.Forms.DataGridView();
            this.gbxPendentes = new System.Windows.Forms.GroupBox();
            this.rbnPendentes = new System.Windows.Forms.RadioButton();
            this.rbnTodas = new System.Windows.Forms.RadioButton();
            this.gbxResumoPor = new System.Windows.Forms.GroupBox();
            this.rbnResumoProdutoItem = new System.Windows.Forms.RadioButton();
            this.rbnResumoProduto = new System.Windows.Forms.RadioButton();
            this.rbnResumoVendedor = new System.Windows.Forms.RadioButton();
            this.rbnResumoCliente = new System.Windows.Forms.RadioButton();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tsbExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspMenuImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbxVisualizarPeriodo = new System.Windows.Forms.GroupBox();
            this.tbxDataAte = new System.Windows.Forms.TextBox();
            this.tbxDataDe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).BeginInit();
            this.gbxPedido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidoPendente)).BeginInit();
            this.gbxResumoPedido.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbxPedido1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido1)).BeginInit();
            this.gbxPendentes.SuspendLayout();
            this.gbxResumoPor.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.gbxVisualizarPeriodo.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPedido
            // 
            this.dgvPedido.AccessibleDescription = "Pedidos";
            this.dgvPedido.AllowUserToAddRows = false;
            this.dgvPedido.AllowUserToDeleteRows = false;
            this.dgvPedido.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.dgvPedido.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPedido.BackgroundColor = System.Drawing.Color.White;
            this.dgvPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedido.Location = new System.Drawing.Point(6, 16);
            this.dgvPedido.MultiSelect = false;
            this.dgvPedido.Name = "dgvPedido";
            this.dgvPedido.ReadOnly = true;
            this.dgvPedido.RowHeadersVisible = false;
            this.dgvPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedido.Size = new System.Drawing.Size(672, 263);
            this.dgvPedido.TabIndex = 125;
            this.dgvPedido.TabStop = false;
            this.ttpPedido.SetToolTip(this.dgvPedido, "Grade de Pedidos");
            this.dgvPedido.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPedido_CellClick);
            this.dgvPedido.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPedido_CellDoubleClick);
            this.dgvPedido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvPedido_KeyDown);
            // 
            // gbxPedido
            // 
            this.gbxPedido.AccessibleDescription = "Pedidos de Venda";
            this.gbxPedido.Controls.Add(this.dgvPedido);
            this.gbxPedido.Location = new System.Drawing.Point(9, 68);
            this.gbxPedido.Name = "gbxPedido";
            this.gbxPedido.Size = new System.Drawing.Size(684, 285);
            this.gbxPedido.TabIndex = 81;
            this.gbxPedido.TabStop = false;
            this.gbxPedido.Text = "Pedidos de Venda";
            this.ttpPedido.SetToolTip(this.gbxPedido, "Grupo - Grade do Pedido");
            // 
            // dgvPedidoPendente
            // 
            this.dgvPedidoPendente.AccessibleDescription = "Pedidos por Cliente";
            this.dgvPedidoPendente.AllowUserToAddRows = false;
            this.dgvPedidoPendente.AllowUserToDeleteRows = false;
            this.dgvPedidoPendente.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.dgvPedidoPendente.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPedidoPendente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPedidoPendente.BackgroundColor = System.Drawing.Color.White;
            this.dgvPedidoPendente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidoPendente.Location = new System.Drawing.Point(6, 16);
            this.dgvPedidoPendente.MultiSelect = false;
            this.dgvPedidoPendente.Name = "dgvPedidoPendente";
            this.dgvPedidoPendente.ReadOnly = true;
            this.dgvPedidoPendente.RowHeadersVisible = false;
            this.dgvPedidoPendente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedidoPendente.Size = new System.Drawing.Size(297, 437);
            this.dgvPedidoPendente.TabIndex = 125;
            this.dgvPedidoPendente.TabStop = false;
            this.ttpPedido.SetToolTip(this.dgvPedidoPendente, "desconhecido");
            // 
            // gbxResumoPedido
            // 
            this.gbxResumoPedido.AccessibleDescription = "Total de Pedidos por Cliente";
            this.gbxResumoPedido.Controls.Add(this.groupBox1);
            this.gbxResumoPedido.Controls.Add(this.dgvPedidoPendente);
            this.gbxResumoPedido.Location = new System.Drawing.Point(699, 68);
            this.gbxResumoPedido.Name = "gbxResumoPedido";
            this.gbxResumoPedido.Size = new System.Drawing.Size(309, 526);
            this.gbxResumoPedido.TabIndex = 82;
            this.gbxResumoPedido.TabStop = false;
            this.gbxResumoPedido.Text = "Total de Pedidos por Cliente";
            this.ttpPedido.SetToolTip(this.gbxResumoPedido, "Grupo Desconnhecido");
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Peças";
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbxTotalvalorpendente);
            this.groupBox1.Location = new System.Drawing.Point(6, 455);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 61);
            this.groupBox1.TabIndex = 126;
            this.groupBox1.TabStop = false;
            this.ttpPedido.SetToolTip(this.groupBox1, "Grupo - Desconhecido do desconhecido");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Valor Total Mercadoria Pendente:";
            // 
            // tbxTotalvalorpendente
            // 
            this.tbxTotalvalorpendente.AccessibleDescription = "Valor Total Mercadoria Pendente:";
            this.tbxTotalvalorpendente.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTotalvalorpendente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTotalvalorpendente.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalvalorpendente.Location = new System.Drawing.Point(67, 33);
            this.tbxTotalvalorpendente.Name = "tbxTotalvalorpendente";
            this.tbxTotalvalorpendente.ReadOnly = true;
            this.tbxTotalvalorpendente.Size = new System.Drawing.Size(164, 21);
            this.tbxTotalvalorpendente.TabIndex = 3;
            this.tbxTotalvalorpendente.Tag = "";
            this.tbxTotalvalorpendente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttpPedido.SetToolTip(this.tbxTotalvalorpendente, "Ano ");
            // 
            // gbxPedido1
            // 
            this.gbxPedido1.AccessibleDescription = "Itens do Pedido";
            this.gbxPedido1.Controls.Add(this.dgvPedido1);
            this.gbxPedido1.Location = new System.Drawing.Point(9, 356);
            this.gbxPedido1.Name = "gbxPedido1";
            this.gbxPedido1.Size = new System.Drawing.Size(684, 241);
            this.gbxPedido1.TabIndex = 88;
            this.gbxPedido1.TabStop = false;
            this.gbxPedido1.Text = "Itens do Pedido";
            this.ttpPedido.SetToolTip(this.gbxPedido1, "Grupo - Itens do Pedido");
            // 
            // dgvPedido1
            // 
            this.dgvPedido1.AccessibleDescription = "Itens do Pedido";
            this.dgvPedido1.AllowUserToAddRows = false;
            this.dgvPedido1.AllowUserToDeleteRows = false;
            this.dgvPedido1.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Lavender;
            this.dgvPedido1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPedido1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPedido1.BackgroundColor = System.Drawing.Color.White;
            this.dgvPedido1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedido1.Location = new System.Drawing.Point(6, 16);
            this.dgvPedido1.MultiSelect = false;
            this.dgvPedido1.Name = "dgvPedido1";
            this.dgvPedido1.ReadOnly = true;
            this.dgvPedido1.RowHeadersVisible = false;
            this.dgvPedido1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedido1.Size = new System.Drawing.Size(672, 218);
            this.dgvPedido1.TabIndex = 125;
            this.dgvPedido1.TabStop = false;
            this.ttpPedido.SetToolTip(this.dgvPedido1, "Grade dos Itens do Pedido");
            // 
            // gbxPendentes
            // 
            this.gbxPendentes.AccessibleDescription = "Opções";
            this.gbxPendentes.Controls.Add(this.rbnPendentes);
            this.gbxPendentes.Controls.Add(this.rbnTodas);
            this.gbxPendentes.Location = new System.Drawing.Point(15, 12);
            this.gbxPendentes.Name = "gbxPendentes";
            this.gbxPendentes.Size = new System.Drawing.Size(158, 50);
            this.gbxPendentes.TabIndex = 135;
            this.gbxPendentes.TabStop = false;
            this.ttpPedido.SetToolTip(this.gbxPendentes, "Grupo - Tipo de Filtro do Pedido");
            // 
            // rbnPendentes
            // 
            this.rbnPendentes.AccessibleDescription = "Pendentes";
            this.rbnPendentes.AutoSize = true;
            this.rbnPendentes.Checked = true;
            this.rbnPendentes.Location = new System.Drawing.Point(66, 20);
            this.rbnPendentes.Name = "rbnPendentes";
            this.rbnPendentes.Size = new System.Drawing.Size(76, 17);
            this.rbnPendentes.TabIndex = 2;
            this.rbnPendentes.TabStop = true;
            this.rbnPendentes.Text = "Pendentes";
            this.ttpPedido.SetToolTip(this.rbnPendentes, "Pendentes");
            this.rbnPendentes.UseVisualStyleBackColor = true;
            this.rbnPendentes.Click += new System.EventHandler(this.rbnPendentes_Click);
            this.rbnPendentes.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnPendentes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnPendentes.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnTodas
            // 
            this.rbnTodas.AccessibleDescription = "Todos";
            this.rbnTodas.AutoSize = true;
            this.rbnTodas.Location = new System.Drawing.Point(6, 20);
            this.rbnTodas.Name = "rbnTodas";
            this.rbnTodas.Size = new System.Drawing.Size(54, 17);
            this.rbnTodas.TabIndex = 5;
            this.rbnTodas.Text = "Todos";
            this.ttpPedido.SetToolTip(this.rbnTodas, "Todas");
            this.rbnTodas.UseVisualStyleBackColor = true;
            this.rbnTodas.Click += new System.EventHandler(this.rbnTodas_Click);
            this.rbnTodas.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTodas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTodas.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxResumoPor
            // 
            this.gbxResumoPor.AccessibleDescription = "Resumo dos Pedidos Por Cliente/Vendedor";
            this.gbxResumoPor.Controls.Add(this.rbnResumoProdutoItem);
            this.gbxResumoPor.Controls.Add(this.rbnResumoProduto);
            this.gbxResumoPor.Controls.Add(this.rbnResumoVendedor);
            this.gbxResumoPor.Controls.Add(this.rbnResumoCliente);
            this.gbxResumoPor.Location = new System.Drawing.Point(395, 5);
            this.gbxResumoPor.Name = "gbxResumoPor";
            this.gbxResumoPor.Size = new System.Drawing.Size(254, 64);
            this.gbxResumoPor.TabIndex = 136;
            this.gbxResumoPor.TabStop = false;
            this.gbxResumoPor.Text = "Resumo dos Pedidos Por Cliente/Vendedor";
            this.ttpPedido.SetToolTip(this.gbxResumoPor, "Resumo dos Pedidos Por Cliente/Vendedor");
            // 
            // rbnResumoProdutoItem
            // 
            this.rbnResumoProdutoItem.AccessibleDescription = "Por Item de Produto";
            this.rbnResumoProdutoItem.AutoSize = true;
            this.rbnResumoProdutoItem.Location = new System.Drawing.Point(140, 39);
            this.rbnResumoProdutoItem.Name = "rbnResumoProdutoItem";
            this.rbnResumoProdutoItem.Size = new System.Drawing.Size(107, 17);
            this.rbnResumoProdutoItem.TabIndex = 3;
            this.rbnResumoProdutoItem.Text = "Por Item Produto";
            this.ttpPedido.SetToolTip(this.rbnResumoProdutoItem, "Por Item Produto");
            this.rbnResumoProdutoItem.UseVisualStyleBackColor = true;
            this.rbnResumoProdutoItem.Click += new System.EventHandler(this.rbnResumoProdutoItem_Click);
            // 
            // rbnResumoProduto
            // 
            this.rbnResumoProduto.AccessibleDescription = "Por Produto";
            this.rbnResumoProduto.AutoSize = true;
            this.rbnResumoProduto.Location = new System.Drawing.Point(10, 39);
            this.rbnResumoProduto.Name = "rbnResumoProduto";
            this.rbnResumoProduto.Size = new System.Drawing.Size(114, 17);
            this.rbnResumoProduto.TabIndex = 2;
            this.rbnResumoProduto.Text = "Por Grupo Produto";
            this.ttpPedido.SetToolTip(this.rbnResumoProduto, "Por Grupo Produto");
            this.rbnResumoProduto.UseVisualStyleBackColor = true;
            this.rbnResumoProduto.Click += new System.EventHandler(this.rbnResumoProduto_Click);
            // 
            // rbnResumoVendedor
            // 
            this.rbnResumoVendedor.AccessibleDescription = "Por Vendedor";
            this.rbnResumoVendedor.AutoSize = true;
            this.rbnResumoVendedor.Location = new System.Drawing.Point(140, 20);
            this.rbnResumoVendedor.Name = "rbnResumoVendedor";
            this.rbnResumoVendedor.Size = new System.Drawing.Size(90, 17);
            this.rbnResumoVendedor.TabIndex = 1;
            this.rbnResumoVendedor.Text = "Por Vendedor";
            this.ttpPedido.SetToolTip(this.rbnResumoVendedor, "Por Vendedor");
            this.rbnResumoVendedor.UseVisualStyleBackColor = true;
            this.rbnResumoVendedor.Click += new System.EventHandler(this.rbnResumoVendedor_Click);
            // 
            // rbnResumoCliente
            // 
            this.rbnResumoCliente.AccessibleDescription = "Por Cliente";
            this.rbnResumoCliente.AutoSize = true;
            this.rbnResumoCliente.Checked = true;
            this.rbnResumoCliente.Location = new System.Drawing.Point(10, 20);
            this.rbnResumoCliente.Name = "rbnResumoCliente";
            this.rbnResumoCliente.Size = new System.Drawing.Size(77, 17);
            this.rbnResumoCliente.TabIndex = 0;
            this.rbnResumoCliente.TabStop = true;
            this.rbnResumoCliente.Text = "Por Cliente";
            this.ttpPedido.SetToolTip(this.rbnResumoCliente, "Por Cliente");
            this.rbnResumoCliente.UseVisualStyleBackColor = true;
            this.rbnResumoCliente.Click += new System.EventHandler(this.rbnResumoCliente_Click);
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
            this.tsbExcluir,
            this.tspMenuImprimir,
            this.tspEscolher,
            this.tspRetornar,
            this.toolStripSeparator1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(11, 600);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(682, 47);
            this.tspTool.TabIndex = 87;
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
            // tsbExcluir
            // 
            this.tsbExcluir.AccessibleDescription = "Excluir";
            this.tsbExcluir.AutoSize = false;
            this.tsbExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tsbExcluir.Image")));
            this.tsbExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcluir.Name = "tsbExcluir";
            this.tsbExcluir.Size = new System.Drawing.Size(66, 42);
            this.tsbExcluir.Text = "&Cancelar";
            this.tsbExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExcluir.Click += new System.EventHandler(this.tsbExcluir_Click);
            // 
            // tspMenuImprimir
            // 
            this.tspMenuImprimir.AccessibleDescription = "Imprimir";
            this.tspMenuImprimir.AutoSize = false;
            this.tspMenuImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspMenuImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspMenuImprimir.Image")));
            this.tspMenuImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspMenuImprimir.Name = "tspMenuImprimir";
            this.tspMenuImprimir.Size = new System.Drawing.Size(76, 42);
            this.tspMenuImprimir.Text = "Rel I&mprimir";
            this.tspMenuImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspMenuImprimir.Click += new System.EventHandler(this.tspMenuImprimir_Click);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
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
            this.tstbxLocalizar.Size = new System.Drawing.Size(150, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            this.tstbxLocalizar.Click += new System.EventHandler(this.tstbxLocalizar_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(662, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(346, 35);
            this.label5.TabIndex = 134;
            this.label5.Text = "Pedido de Vendas";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // gbxVisualizarPeriodo
            // 
            this.gbxVisualizarPeriodo.AccessibleDescription = "Periodo para Visualizar";
            this.gbxVisualizarPeriodo.Controls.Add(this.tbxDataAte);
            this.gbxVisualizarPeriodo.Controls.Add(this.tbxDataDe);
            this.gbxVisualizarPeriodo.Controls.Add(this.label4);
            this.gbxVisualizarPeriodo.Controls.Add(this.label8);
            this.gbxVisualizarPeriodo.Location = new System.Drawing.Point(176, 12);
            this.gbxVisualizarPeriodo.Name = "gbxVisualizarPeriodo";
            this.gbxVisualizarPeriodo.Size = new System.Drawing.Size(183, 54);
            this.gbxVisualizarPeriodo.TabIndex = 1;
            this.gbxVisualizarPeriodo.TabStop = false;
            this.gbxVisualizarPeriodo.Text = "Período para Visualizar";
            // 
            // tbxDataAte
            // 
            this.tbxDataAte.AccessibleDescription = "Ate a Data de:";
            this.tbxDataAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataAte.Location = new System.Drawing.Point(100, 28);
            this.tbxDataAte.Name = "tbxDataAte";
            this.tbxDataAte.Size = new System.Drawing.Size(77, 21);
            this.tbxDataAte.TabIndex = 1;
            this.tbxDataAte.Text = "01/01/2009";
            this.tbxDataAte.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataAte.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxDataDe
            // 
            this.tbxDataDe.AccessibleDescription = "Da Data de:";
            this.tbxDataDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataDe.Location = new System.Drawing.Point(6, 28);
            this.tbxDataDe.Name = "tbxDataDe";
            this.tbxDataDe.Size = new System.Drawing.Size(77, 21);
            this.tbxDataDe.TabIndex = 0;
            this.tbxDataDe.Text = "01/01/2009";
            this.tbxDataDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataDe.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Até a Data de:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Da Data de:";
            // 
            // frmPedidoVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxResumoPor);
            this.Controls.Add(this.gbxPendentes);
            this.Controls.Add(this.gbxVisualizarPeriodo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbxPedido1);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxResumoPedido);
            this.Controls.Add(this.gbxPedido);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPedidoVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "34090";
            this.Text = "Pedido de Vendas - Visualização";
            this.ttpPedido.SetToolTip(this, "Pedido de Vendas - Visualização");
            this.Activated += new System.EventHandler(this.frmPedidoVis_Activated);
            this.Load += new System.EventHandler(this.frmPedidoVis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).EndInit();
            this.gbxPedido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidoPendente)).EndInit();
            this.gbxResumoPedido.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxPedido1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido1)).EndInit();
            this.gbxPendentes.ResumeLayout(false);
            this.gbxPendentes.PerformLayout();
            this.gbxResumoPor.ResumeLayout(false);
            this.gbxResumoPor.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxVisualizarPeriodo.ResumeLayout(false);
            this.gbxVisualizarPeriodo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpPedido;
        private System.Windows.Forms.GroupBox gbxPedido;
        private System.Windows.Forms.DataGridView dgvPedido;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.DataGridView dgvPedidoPendente;
        private System.Windows.Forms.GroupBox gbxResumoPedido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxTotalvalorpendente;
        private System.Windows.Forms.GroupBox gbxPedido1;
        private System.Windows.Forms.DataGridView dgvPedido1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tspMenuImprimir;
        private System.Windows.Forms.GroupBox gbxVisualizarPeriodo;
        private System.Windows.Forms.TextBox tbxDataAte;
        private System.Windows.Forms.TextBox tbxDataDe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbxPendentes;
        private System.Windows.Forms.RadioButton rbnPendentes;
        private System.Windows.Forms.RadioButton rbnTodas;
        private System.Windows.Forms.GroupBox gbxResumoPor;
        private System.Windows.Forms.RadioButton rbnResumoVendedor;
        private System.Windows.Forms.RadioButton rbnResumoCliente;
        private System.Windows.Forms.RadioButton rbnResumoProdutoItem;
        private System.Windows.Forms.RadioButton rbnResumoProduto;
        private System.Windows.Forms.ToolStripButton tsbExcluir;
    }
}