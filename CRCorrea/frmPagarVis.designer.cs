namespace CRCorrea
{
    partial class frmPagarVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPagarVis));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxPagar = new System.Windows.Forms.GroupBox();
            this.pbxPagar = new System.Windows.Forms.PictureBox();
            this.dgvPagar = new System.Windows.Forms.DataGridView();
            this.gbxOpcoes = new System.Windows.Forms.GroupBox();
            this.rbnUmDia = new System.Windows.Forms.RadioButton();
            this.rbnAtrasados = new System.Windows.Forms.RadioButton();
            this.rbnApartirdeHoje = new System.Windows.Forms.RadioButton();
            this.rbnTodos = new System.Windows.Forms.RadioButton();
            this.gbxPorData = new System.Windows.Forms.GroupBox();
            this.rbnDataPrev = new System.Windows.Forms.RadioButton();
            this.rbnDataVenc = new System.Windows.Forms.RadioButton();
            this.gbxPagarFornecedor = new System.Windows.Forms.GroupBox();
            this.pbxPagarAcumula = new System.Windows.Forms.PictureBox();
            this.gbxAcumulaOp = new System.Windows.Forms.GroupBox();
            this.rbnAcumula60 = new System.Windows.Forms.RadioButton();
            this.rbnAcumula30 = new System.Windows.Forms.RadioButton();
            this.rbnAcumulaTudo = new System.Windows.Forms.RadioButton();
            this.rbnAcumula90 = new System.Windows.Forms.RadioButton();
            this.rbnAcumulaDia = new System.Windows.Forms.RadioButton();
            this.rbnAcumulaVencido = new System.Windows.Forms.RadioButton();
            this.dgvPagarAcumula = new System.Windows.Forms.DataGridView();
            this.gbxValores = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxAPagar60 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxAPagar30 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxAPagarTudo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxAPagar90 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxAPagarNoMes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxAPagarVencido = new System.Windows.Forms.TextBox();
            this.gbxData = new System.Windows.Forms.GroupBox();
            this.tbxDataAtual = new System.Windows.Forms.TextBox();
            this.tbxTotalVisualizacao = new System.Windows.Forms.TextBox();
            this.gbxInadimplencia = new System.Windows.Forms.GroupBox();
            this.ckxPendencias = new System.Windows.Forms.CheckBox();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspMnuImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspAnalise = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstPesquisa = new System.Windows.Forms.ToolStripTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxAPagar07 = new System.Windows.Forms.TextBox();
            this.gbxPagar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPagar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagar)).BeginInit();
            this.gbxOpcoes.SuspendLayout();
            this.gbxPorData.SuspendLayout();
            this.gbxPagarFornecedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPagarAcumula)).BeginInit();
            this.gbxAcumulaOp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagarAcumula)).BeginInit();
            this.gbxValores.SuspendLayout();
            this.gbxData.SuspendLayout();
            this.gbxInadimplencia.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxPagar
            // 
            this.gbxPagar.AccessibleDescription = "Titulos";
            this.gbxPagar.Controls.Add(this.pbxPagar);
            this.gbxPagar.Controls.Add(this.dgvPagar);
            this.gbxPagar.Location = new System.Drawing.Point(12, 60);
            this.gbxPagar.Name = "gbxPagar";
            this.gbxPagar.Size = new System.Drawing.Size(996, 332);
            this.gbxPagar.TabIndex = 0;
            this.gbxPagar.TabStop = false;
            this.gbxPagar.Text = "Títulos";
            this.toolTip1.SetToolTip(this.gbxPagar, "Grupo Pedidos de Compras");
            // 
            // pbxPagar
            // 
            this.pbxPagar.AccessibleDescription = "Carregando Titulos";
            this.pbxPagar.BackColor = System.Drawing.Color.White;
            this.pbxPagar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxPagar.Image = ((System.Drawing.Image)(resources.GetObject("pbxPagar.Image")));
            this.pbxPagar.Location = new System.Drawing.Point(6, 14);
            this.pbxPagar.Name = "pbxPagar";
            this.pbxPagar.Size = new System.Drawing.Size(967, 316);
            this.pbxPagar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxPagar.TabIndex = 128;
            this.pbxPagar.TabStop = false;
            this.toolTip1.SetToolTip(this.pbxPagar, "Imagem Ctas a Pagar");
            this.pbxPagar.Visible = false;
            // 
            // dgvPagar
            // 
            this.dgvPagar.AccessibleDescription = "Pagar";
            this.dgvPagar.AllowUserToAddRows = false;
            this.dgvPagar.AllowUserToDeleteRows = false;
            this.dgvPagar.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvPagar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPagar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvPagar.BackgroundColor = System.Drawing.Color.White;
            this.dgvPagar.ColumnHeadersHeight = 28;
            this.dgvPagar.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPagar.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPagar.Location = new System.Drawing.Point(6, 15);
            this.dgvPagar.MultiSelect = false;
            this.dgvPagar.Name = "dgvPagar";
            this.dgvPagar.ReadOnly = true;
            this.dgvPagar.RowHeadersVisible = false;
            this.dgvPagar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPagar.Size = new System.Drawing.Size(984, 316);
            this.dgvPagar.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dgvPagar, "Grade de clsVisualização Pedidos de Compras");
            this.dgvPagar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagar_CellDoubleClick);
            this.dgvPagar.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPagar_ColumnHeaderMouseClick);
            // 
            // gbxOpcoes
            // 
            this.gbxOpcoes.AccessibleDescription = "Titulos";
            this.gbxOpcoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxOpcoes.Controls.Add(this.rbnUmDia);
            this.gbxOpcoes.Controls.Add(this.rbnAtrasados);
            this.gbxOpcoes.Controls.Add(this.rbnApartirdeHoje);
            this.gbxOpcoes.Controls.Add(this.rbnTodos);
            this.gbxOpcoes.Location = new System.Drawing.Point(12, 12);
            this.gbxOpcoes.Name = "gbxOpcoes";
            this.gbxOpcoes.Size = new System.Drawing.Size(358, 46);
            this.gbxOpcoes.TabIndex = 0;
            this.gbxOpcoes.TabStop = false;
            this.gbxOpcoes.Text = "Títulos";
            this.toolTip1.SetToolTip(this.gbxOpcoes, "Grupo de Opções Filtro");
            // 
            // rbnUmDia
            // 
            this.rbnUmDia.AccessibleDescription = "Um Dia";
            this.rbnUmDia.AutoSize = true;
            this.rbnUmDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnUmDia.Location = new System.Drawing.Point(253, 20);
            this.rbnUmDia.Name = "rbnUmDia";
            this.rbnUmDia.Size = new System.Drawing.Size(99, 17);
            this.rbnUmDia.TabIndex = 3;
            this.rbnUmDia.Text = "Um Dia Especial";
            this.toolTip1.SetToolTip(this.rbnUmDia, "Um Dia Especial");
            this.rbnUmDia.UseVisualStyleBackColor = true;
            this.rbnUmDia.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnUmDia.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnUmDia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnUmDia.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAtrasados
            // 
            this.rbnAtrasados.AccessibleDescription = "Em Atraso";
            this.rbnAtrasados.AutoSize = true;
            this.rbnAtrasados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAtrasados.Location = new System.Drawing.Point(173, 20);
            this.rbnAtrasados.Name = "rbnAtrasados";
            this.rbnAtrasados.Size = new System.Drawing.Size(74, 17);
            this.rbnAtrasados.TabIndex = 2;
            this.rbnAtrasados.Text = "Em Atraso";
            this.toolTip1.SetToolTip(this.rbnAtrasados, "Titulos em Atraso");
            this.rbnAtrasados.UseVisualStyleBackColor = true;
            this.rbnAtrasados.CheckedChanged += new System.EventHandler(this.rbnAtrasados_CheckedChanged);
            this.rbnAtrasados.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnAtrasados.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtrasados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtrasados.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnApartirdeHoje
            // 
            this.rbnApartirdeHoje.AccessibleDescription = "A Partir de Hoje";
            this.rbnApartirdeHoje.AutoSize = true;
            this.rbnApartirdeHoje.Checked = true;
            this.rbnApartirdeHoje.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnApartirdeHoje.Location = new System.Drawing.Point(66, 20);
            this.rbnApartirdeHoje.Name = "rbnApartirdeHoje";
            this.rbnApartirdeHoje.Size = new System.Drawing.Size(101, 17);
            this.rbnApartirdeHoje.TabIndex = 1;
            this.rbnApartirdeHoje.TabStop = true;
            this.rbnApartirdeHoje.Text = "A partir de Hoje";
            this.toolTip1.SetToolTip(this.rbnApartirdeHoje, "Titulos  a partir de Hoje");
            this.rbnApartirdeHoje.UseVisualStyleBackColor = true;
            this.rbnApartirdeHoje.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnApartirdeHoje.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnApartirdeHoje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnApartirdeHoje.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnTodos
            // 
            this.rbnTodos.AccessibleDescription = "Todos";
            this.rbnTodos.AutoSize = true;
            this.rbnTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTodos.Location = new System.Drawing.Point(6, 20);
            this.rbnTodos.Name = "rbnTodos";
            this.rbnTodos.Size = new System.Drawing.Size(54, 17);
            this.rbnTodos.TabIndex = 0;
            this.rbnTodos.Text = "Todos";
            this.toolTip1.SetToolTip(this.rbnTodos, "Todos os Pedidos");
            this.rbnTodos.UseVisualStyleBackColor = true;
            this.rbnTodos.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnTodos.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTodos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTodos.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxPorData
            // 
            this.gbxPorData.AccessibleDescription = "Visualizar pela:";
            this.gbxPorData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxPorData.Controls.Add(this.rbnDataPrev);
            this.gbxPorData.Controls.Add(this.rbnDataVenc);
            this.gbxPorData.Location = new System.Drawing.Point(490, 12);
            this.gbxPorData.Name = "gbxPorData";
            this.gbxPorData.Size = new System.Drawing.Size(142, 46);
            this.gbxPorData.TabIndex = 2;
            this.gbxPorData.TabStop = false;
            this.gbxPorData.Text = "Visualizar pela:";
            this.toolTip1.SetToolTip(this.gbxPorData, "Grupo de Opções Filtro");
            this.gbxPorData.Visible = false;
            // 
            // rbnDataPrev
            // 
            this.rbnDataPrev.AccessibleDescription = "Data de Previsão";
            this.rbnDataPrev.AutoSize = true;
            this.rbnDataPrev.Checked = true;
            this.rbnDataPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnDataPrev.Location = new System.Drawing.Point(6, 27);
            this.rbnDataPrev.Name = "rbnDataPrev";
            this.rbnDataPrev.Size = new System.Drawing.Size(107, 17);
            this.rbnDataPrev.TabIndex = 2;
            this.rbnDataPrev.TabStop = true;
            this.rbnDataPrev.Text = "Data de Previsão";
            this.toolTip1.SetToolTip(this.rbnDataPrev, "Data de Previsão");
            this.rbnDataPrev.UseVisualStyleBackColor = true;
            this.rbnDataPrev.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnDataPrev.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnDataPrev.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnDataVenc
            // 
            this.rbnDataVenc.AccessibleDescription = "Data de Vencimento";
            this.rbnDataVenc.AutoSize = true;
            this.rbnDataVenc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnDataVenc.Location = new System.Drawing.Point(6, 11);
            this.rbnDataVenc.Name = "rbnDataVenc";
            this.rbnDataVenc.Size = new System.Drawing.Size(121, 17);
            this.rbnDataVenc.TabIndex = 1;
            this.rbnDataVenc.Text = "Data de Vencimento";
            this.toolTip1.SetToolTip(this.rbnDataVenc, "Data de Vencimento");
            this.rbnDataVenc.UseVisualStyleBackColor = true;
            this.rbnDataVenc.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnDataVenc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnDataVenc.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxPagarFornecedor
            // 
            this.gbxPagarFornecedor.AccessibleDescription = "Valor Acumulado a Pagar por Fornecedor (Valor e %)";
            this.gbxPagarFornecedor.Controls.Add(this.pbxPagarAcumula);
            this.gbxPagarFornecedor.Controls.Add(this.gbxAcumulaOp);
            this.gbxPagarFornecedor.Controls.Add(this.dgvPagarAcumula);
            this.gbxPagarFornecedor.Location = new System.Drawing.Point(379, 396);
            this.gbxPagarFornecedor.Name = "gbxPagarFornecedor";
            this.gbxPagarFornecedor.Size = new System.Drawing.Size(478, 193);
            this.gbxPagarFornecedor.TabIndex = 91;
            this.gbxPagarFornecedor.TabStop = false;
            this.gbxPagarFornecedor.Text = "Valor Acumulado a Pagar por Fornecedor (Valor e %)";
            this.toolTip1.SetToolTip(this.gbxPagarFornecedor, "Grupo Valor Acumulado a Pagar por Fornecedor (Valor e %)");
            // 
            // pbxPagarAcumula
            // 
            this.pbxPagarAcumula.AccessibleDescription = "Carregando Valores";
            this.pbxPagarAcumula.BackColor = System.Drawing.Color.White;
            this.pbxPagarAcumula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxPagarAcumula.Image = ((System.Drawing.Image)(resources.GetObject("pbxPagarAcumula.Image")));
            this.pbxPagarAcumula.Location = new System.Drawing.Point(119, 31);
            this.pbxPagarAcumula.Name = "pbxPagarAcumula";
            this.pbxPagarAcumula.Size = new System.Drawing.Size(353, 149);
            this.pbxPagarAcumula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxPagarAcumula.TabIndex = 132;
            this.pbxPagarAcumula.TabStop = false;
            this.toolTip1.SetToolTip(this.pbxPagarAcumula, "Imagem Acumulativo Contas a Pagar");
            this.pbxPagarAcumula.Visible = false;
            // 
            // gbxAcumulaOp
            // 
            this.gbxAcumulaOp.AccessibleDescription = "Filtro";
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumula60);
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumula30);
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumulaTudo);
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumula90);
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumulaDia);
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumulaVencido);
            this.gbxAcumulaOp.Location = new System.Drawing.Point(6, 27);
            this.gbxAcumulaOp.Name = "gbxAcumulaOp";
            this.gbxAcumulaOp.Size = new System.Drawing.Size(106, 150);
            this.gbxAcumulaOp.TabIndex = 131;
            this.gbxAcumulaOp.TabStop = false;
            this.gbxAcumulaOp.Text = "Filtro Dt Previsão";
            this.toolTip1.SetToolTip(this.gbxAcumulaOp, "Tipo de Opção");
            // 
            // rbnAcumula60
            // 
            this.rbnAcumula60.AccessibleDescription = "60 Dias";
            this.rbnAcumula60.AutoSize = true;
            this.rbnAcumula60.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumula60.Location = new System.Drawing.Point(6, 82);
            this.rbnAcumula60.Name = "rbnAcumula60";
            this.rbnAcumula60.Size = new System.Drawing.Size(59, 17);
            this.rbnAcumula60.TabIndex = 3;
            this.rbnAcumula60.Text = "60 dias";
            this.toolTip1.SetToolTip(this.rbnAcumula60, "Acumula 60 Dias");
            this.rbnAcumula60.UseVisualStyleBackColor = true;
            this.rbnAcumula60.Click += new System.EventHandler(this.rbnAcumula60_Click);
            // 
            // rbnAcumula30
            // 
            this.rbnAcumula30.AccessibleDescription = "90 Dias";
            this.rbnAcumula30.AutoSize = true;
            this.rbnAcumula30.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumula30.Location = new System.Drawing.Point(6, 61);
            this.rbnAcumula30.Name = "rbnAcumula30";
            this.rbnAcumula30.Size = new System.Drawing.Size(59, 17);
            this.rbnAcumula30.TabIndex = 2;
            this.rbnAcumula30.Text = "30 dias";
            this.toolTip1.SetToolTip(this.rbnAcumula30, "Acumula 30 Dias");
            this.rbnAcumula30.UseVisualStyleBackColor = true;
            this.rbnAcumula30.Click += new System.EventHandler(this.rbnAcumula30_Click);
            // 
            // rbnAcumulaTudo
            // 
            this.rbnAcumulaTudo.AccessibleDescription = "Tudo";
            this.rbnAcumulaTudo.AutoSize = true;
            this.rbnAcumulaTudo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumulaTudo.Location = new System.Drawing.Point(6, 124);
            this.rbnAcumulaTudo.Name = "rbnAcumulaTudo";
            this.rbnAcumulaTudo.Size = new System.Drawing.Size(49, 17);
            this.rbnAcumulaTudo.TabIndex = 5;
            this.rbnAcumulaTudo.Text = "Tudo";
            this.toolTip1.SetToolTip(this.rbnAcumulaTudo, "Todos os Pedidos");
            this.rbnAcumulaTudo.UseVisualStyleBackColor = true;
            this.rbnAcumulaTudo.Click += new System.EventHandler(this.rbnAcumulaTudo_Click);
            this.rbnAcumulaTudo.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAcumulaTudo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAcumulaTudo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAcumula90
            // 
            this.rbnAcumula90.AccessibleDescription = "90 Dias";
            this.rbnAcumula90.AutoSize = true;
            this.rbnAcumula90.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumula90.Location = new System.Drawing.Point(6, 103);
            this.rbnAcumula90.Name = "rbnAcumula90";
            this.rbnAcumula90.Size = new System.Drawing.Size(59, 17);
            this.rbnAcumula90.TabIndex = 4;
            this.rbnAcumula90.Text = "90 dias";
            this.toolTip1.SetToolTip(this.rbnAcumula90, "Acumula 90 Dias");
            this.rbnAcumula90.UseVisualStyleBackColor = true;
            this.rbnAcumula90.Click += new System.EventHandler(this.rbnAcumula90_Click);
            this.rbnAcumula90.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAcumula90.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAcumula90.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAcumulaDia
            // 
            this.rbnAcumulaDia.AccessibleDescription = "No Dia";
            this.rbnAcumulaDia.AutoSize = true;
            this.rbnAcumulaDia.Checked = true;
            this.rbnAcumulaDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumulaDia.Location = new System.Drawing.Point(6, 40);
            this.rbnAcumulaDia.Name = "rbnAcumulaDia";
            this.rbnAcumulaDia.Size = new System.Drawing.Size(56, 17);
            this.rbnAcumulaDia.TabIndex = 1;
            this.rbnAcumulaDia.TabStop = true;
            this.rbnAcumulaDia.Text = "No Dia";
            this.toolTip1.SetToolTip(this.rbnAcumulaDia, "Todos os Pedidos");
            this.rbnAcumulaDia.UseVisualStyleBackColor = true;
            this.rbnAcumulaDia.Click += new System.EventHandler(this.rbnAcumulaDia_Click);
            this.rbnAcumulaDia.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAcumulaDia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAcumulaDia.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAcumulaVencido
            // 
            this.rbnAcumulaVencido.AccessibleDescription = "Vencido";
            this.rbnAcumulaVencido.AutoSize = true;
            this.rbnAcumulaVencido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumulaVencido.Location = new System.Drawing.Point(6, 20);
            this.rbnAcumulaVencido.Name = "rbnAcumulaVencido";
            this.rbnAcumulaVencido.Size = new System.Drawing.Size(62, 17);
            this.rbnAcumulaVencido.TabIndex = 0;
            this.rbnAcumulaVencido.Text = "Vencido";
            this.toolTip1.SetToolTip(this.rbnAcumulaVencido, "Todos os Pedidos");
            this.rbnAcumulaVencido.UseVisualStyleBackColor = true;
            this.rbnAcumulaVencido.Click += new System.EventHandler(this.rbnAcumulaVencido_Click);
            this.rbnAcumulaVencido.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAcumulaVencido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAcumulaVencido.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // dgvPagarAcumula
            // 
            this.dgvPagarAcumula.AccessibleDescription = "A Pagar por Fornecedor";
            this.dgvPagarAcumula.AllowUserToAddRows = false;
            this.dgvPagarAcumula.AllowUserToDeleteRows = false;
            this.dgvPagarAcumula.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvPagarAcumula.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPagarAcumula.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvPagarAcumula.BackgroundColor = System.Drawing.Color.White;
            this.dgvPagarAcumula.ColumnHeadersHeight = 28;
            this.dgvPagarAcumula.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPagarAcumula.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPagarAcumula.Location = new System.Drawing.Point(119, 21);
            this.dgvPagarAcumula.MultiSelect = false;
            this.dgvPagarAcumula.Name = "dgvPagarAcumula";
            this.dgvPagarAcumula.ReadOnly = true;
            this.dgvPagarAcumula.RowHeadersVisible = false;
            this.dgvPagarAcumula.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPagarAcumula.Size = new System.Drawing.Size(353, 159);
            this.dgvPagarAcumula.TabIndex = 125;
            this.toolTip1.SetToolTip(this.dgvPagarAcumula, "Grade de Valor Acumulado");
            // 
            // gbxValores
            // 
            this.gbxValores.AccessibleDescription = "Totais Dt Previsão";
            this.gbxValores.Controls.Add(this.label9);
            this.gbxValores.Controls.Add(this.tbxAPagar07);
            this.gbxValores.Controls.Add(this.label6);
            this.gbxValores.Controls.Add(this.tbxAPagar60);
            this.gbxValores.Controls.Add(this.label8);
            this.gbxValores.Controls.Add(this.tbxAPagar30);
            this.gbxValores.Controls.Add(this.label4);
            this.gbxValores.Controls.Add(this.tbxAPagarTudo);
            this.gbxValores.Controls.Add(this.label2);
            this.gbxValores.Controls.Add(this.tbxAPagar90);
            this.gbxValores.Controls.Add(this.label1);
            this.gbxValores.Controls.Add(this.tbxAPagarNoMes);
            this.gbxValores.Controls.Add(this.label3);
            this.gbxValores.Controls.Add(this.tbxAPagarVencido);
            this.gbxValores.Location = new System.Drawing.Point(90, 396);
            this.gbxValores.Name = "gbxValores";
            this.gbxValores.Size = new System.Drawing.Size(182, 200);
            this.gbxValores.TabIndex = 131;
            this.gbxValores.TabStop = false;
            this.gbxValores.Text = "Totais Dt Previsão";
            this.toolTip1.SetToolTip(this.gbxValores, "Grupo de Valores Resumo");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 97;
            this.label6.Text = "60 Dias :";
            // 
            // tbxAPagar60
            // 
            this.tbxAPagar60.AccessibleDescription = "No Dia";
            this.tbxAPagar60.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAPagar60.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAPagar60.Location = new System.Drawing.Point(61, 116);
            this.tbxAPagar60.Name = "tbxAPagar60";
            this.tbxAPagar60.ReadOnly = true;
            this.tbxAPagar60.Size = new System.Drawing.Size(108, 22);
            this.tbxAPagar60.TabIndex = 96;
            this.tbxAPagar60.Tag = "";
            this.tbxAPagar60.Text = "5.000.000,00";
            this.tbxAPagar60.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAPagar60, "Total de Pedidos em Aberto");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 95;
            this.label8.Text = "30 Dias :";
            // 
            // tbxAPagar30
            // 
            this.tbxAPagar30.AccessibleDescription = "Vencido";
            this.tbxAPagar30.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAPagar30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAPagar30.Location = new System.Drawing.Point(61, 91);
            this.tbxAPagar30.Name = "tbxAPagar30";
            this.tbxAPagar30.ReadOnly = true;
            this.tbxAPagar30.Size = new System.Drawing.Size(108, 22);
            this.tbxAPagar30.TabIndex = 94;
            this.tbxAPagar30.Tag = "";
            this.tbxAPagar30.Text = "5.000.000,00";
            this.tbxAPagar30.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAPagar30, "Total de Pedidos em Aberto");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 93;
            this.label4.Text = "Tudo:";
            // 
            // tbxAPagarTudo
            // 
            this.tbxAPagarTudo.AccessibleDescription = "Tudo";
            this.tbxAPagarTudo.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAPagarTudo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAPagarTudo.Location = new System.Drawing.Point(60, 170);
            this.tbxAPagarTudo.Name = "tbxAPagarTudo";
            this.tbxAPagarTudo.ReadOnly = true;
            this.tbxAPagarTudo.Size = new System.Drawing.Size(108, 22);
            this.tbxAPagarTudo.TabIndex = 92;
            this.tbxAPagarTudo.Tag = "";
            this.tbxAPagarTudo.Text = "5.000.000,00";
            this.tbxAPagarTudo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAPagarTudo, "Total de Pedidos em Aberto");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 91;
            this.label2.Text = "90 Dias :";
            // 
            // tbxAPagar90
            // 
            this.tbxAPagar90.AccessibleDescription = "90 Dias";
            this.tbxAPagar90.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAPagar90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAPagar90.Location = new System.Drawing.Point(60, 143);
            this.tbxAPagar90.Name = "tbxAPagar90";
            this.tbxAPagar90.ReadOnly = true;
            this.tbxAPagar90.Size = new System.Drawing.Size(108, 22);
            this.tbxAPagar90.TabIndex = 90;
            this.tbxAPagar90.Tag = "";
            this.tbxAPagar90.Text = "5.000.000,00";
            this.tbxAPagar90.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAPagar90, "Total de Pedidos em Aberto");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "No Dia:";
            // 
            // tbxAPagarNoMes
            // 
            this.tbxAPagarNoMes.AccessibleDescription = "No Dia";
            this.tbxAPagarNoMes.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAPagarNoMes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAPagarNoMes.Location = new System.Drawing.Point(60, 41);
            this.tbxAPagarNoMes.Name = "tbxAPagarNoMes";
            this.tbxAPagarNoMes.ReadOnly = true;
            this.tbxAPagarNoMes.Size = new System.Drawing.Size(108, 22);
            this.tbxAPagarNoMes.TabIndex = 88;
            this.tbxAPagarNoMes.Tag = "";
            this.tbxAPagarNoMes.Text = "5.000.000,00";
            this.tbxAPagarNoMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAPagarNoMes, "Total de Pedidos em Aberto");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 87;
            this.label3.Text = "Vencido:";
            // 
            // tbxAPagarVencido
            // 
            this.tbxAPagarVencido.AccessibleDescription = "Vencido";
            this.tbxAPagarVencido.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAPagarVencido.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAPagarVencido.Location = new System.Drawing.Point(60, 15);
            this.tbxAPagarVencido.Name = "tbxAPagarVencido";
            this.tbxAPagarVencido.ReadOnly = true;
            this.tbxAPagarVencido.Size = new System.Drawing.Size(108, 22);
            this.tbxAPagarVencido.TabIndex = 86;
            this.tbxAPagarVencido.Tag = "";
            this.tbxAPagarVencido.Text = "5.000.000,00";
            this.tbxAPagarVencido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAPagarVencido, "Total de Pedidos em Aberto");
            this.tbxAPagarVencido.TextChanged += new System.EventHandler(this.tbxAPagarVencido_TextChanged);
            // 
            // gbxData
            // 
            this.gbxData.AccessibleDescription = "Visualizar pela:";
            this.gbxData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxData.Controls.Add(this.tbxDataAtual);
            this.gbxData.Location = new System.Drawing.Point(370, 12);
            this.gbxData.Name = "gbxData";
            this.gbxData.Size = new System.Drawing.Size(118, 46);
            this.gbxData.TabIndex = 1;
            this.gbxData.TabStop = false;
            this.gbxData.Text = "Data Visualizar";
            this.toolTip1.SetToolTip(this.gbxData, "Grupo de Opções Filtro");
            this.gbxData.Visible = false;
            // 
            // tbxDataAtual
            // 
            this.tbxDataAtual.AccessibleDescription = "Data Atual";
            this.tbxDataAtual.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDataAtual.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataAtual.Location = new System.Drawing.Point(9, 19);
            this.tbxDataAtual.MaxLength = 20;
            this.tbxDataAtual.Name = "tbxDataAtual";
            this.tbxDataAtual.Size = new System.Drawing.Size(81, 21);
            this.tbxDataAtual.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxDataAtual, "Data Atual");
            this.tbxDataAtual.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataAtual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataAtual.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxTotalVisualizacao
            // 
            this.tbxTotalVisualizacao.AccessibleDescription = "Total clsVisualização";
            this.tbxTotalVisualizacao.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTotalVisualizacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTotalVisualizacao.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalVisualizacao.Location = new System.Drawing.Point(904, 465);
            this.tbxTotalVisualizacao.MaxLength = 20;
            this.tbxTotalVisualizacao.Name = "tbxTotalVisualizacao";
            this.tbxTotalVisualizacao.ReadOnly = true;
            this.tbxTotalVisualizacao.Size = new System.Drawing.Size(81, 21);
            this.tbxTotalVisualizacao.TabIndex = 138;
            this.tbxTotalVisualizacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxTotalVisualizacao, "Total clsVisualização");
            // 
            // gbxInadimplencia
            // 
            this.gbxInadimplencia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxInadimplencia.Controls.Add(this.ckxPendencias);
            this.gbxInadimplencia.Location = new System.Drawing.Point(638, 12);
            this.gbxInadimplencia.Name = "gbxInadimplencia";
            this.gbxInadimplencia.Size = new System.Drawing.Size(144, 46);
            this.gbxInadimplencia.TabIndex = 140;
            this.gbxInadimplencia.TabStop = false;
            this.gbxInadimplencia.Text = "Inadimplentes";
            this.toolTip1.SetToolTip(this.gbxInadimplencia, "Grupo de Opções Filtro");
            // 
            // ckxPendencias
            // 
            this.ckxPendencias.AutoSize = true;
            this.ckxPendencias.Checked = true;
            this.ckxPendencias.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckxPendencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckxPendencias.Location = new System.Drawing.Point(3, 17);
            this.ckxPendencias.Name = "ckxPendencias";
            this.ckxPendencias.Size = new System.Drawing.Size(138, 26);
            this.ckxPendencias.TabIndex = 0;
            this.ckxPendencias.Text = "Sem as Pendencias";
            this.ckxPendencias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckxPendencias.UseVisualStyleBackColor = true;
            this.ckxPendencias.CheckedChanged += new System.EventHandler(this.ckxPendencias_CheckedChanged);
            this.ckxPendencias.Click += new System.EventHandler(this.ckxPendencias_Click);
            this.ckxPendencias.Enter += new System.EventHandler(this.ControlEnter);
            this.ckxPendencias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.ckxPendencias.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspMnuImprimir,
            this.tspAnalise,
            this.tspExcluir,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstPesquisa});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(0, 601);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(1011, 47);
            this.tspTool.TabIndex = 9;
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
            this.tspIncluir.Text = "Incluir";
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
            this.tspAlterar.Text = "Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
            // 
            // tspMnuImprimir
            // 
            this.tspMnuImprimir.AccessibleDescription = "Menu Rel";
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
            // tspAnalise
            // 
            this.tspAnalise.AccessibleDescription = "Verificação";
            this.tspAnalise.AutoSize = false;
            this.tspAnalise.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAnalise.Image = global::CRCorrea.Properties.Resources.Atualizar;
            this.tspAnalise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAnalise.Name = "tspAnalise";
            this.tspAnalise.Size = new System.Drawing.Size(73, 41);
            this.tspAnalise.Text = "Verificação";
            this.tspAnalise.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAnalise.ToolTipText = "Verificação";
            this.tspAnalise.Click += new System.EventHandler(this.tspAnalise_Click);
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
            this.tspExcluir.Text = "Excluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
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
            this.tslbLocalizar.Size = new System.Drawing.Size(64, 16);
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
            this.tstPesquisa.ToolTipText = "Procurar por : Nrº NFE,  Data,  Cognome ou Pedido";
            this.tstPesquisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstPesquisa_KeyUp);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label5.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(788, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(220, 49);
            this.label5.TabIndex = 133;
            this.label5.Text = "Contas a Pagar";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(874, 443);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 139;
            this.label7.Text = "Total desta Visualização :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 99;
            this.label9.Text = "07 Dias :";
            // 
            // tbxAPagar07
            // 
            this.tbxAPagar07.AccessibleDescription = "Vencido";
            this.tbxAPagar07.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAPagar07.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAPagar07.Location = new System.Drawing.Point(61, 66);
            this.tbxAPagar07.Name = "tbxAPagar07";
            this.tbxAPagar07.ReadOnly = true;
            this.tbxAPagar07.Size = new System.Drawing.Size(108, 22);
            this.tbxAPagar07.TabIndex = 98;
            this.tbxAPagar07.Tag = "";
            this.tbxAPagar07.Text = "5.000.000,00";
            this.tbxAPagar07.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAPagar07, "Total de Pedidos em Aberto");
            // 
            // frmPagarVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxInadimplencia);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbxTotalVisualizacao);
            this.Controls.Add(this.gbxData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbxValores);
            this.Controls.Add(this.gbxPagarFornecedor);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxPorData);
            this.Controls.Add(this.gbxOpcoes);
            this.Controls.Add(this.gbxPagar);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPagarVis";
            this.ShowInTaskbar = false;
            this.Tag = "550";
            this.Text = "Contas à Pagar -  clsVisualização";
            this.toolTip1.SetToolTip(this, "Contas à Pagar -  clsVisualização");
            this.Activated += new System.EventHandler(this.frmPagarVis_Activated);
            this.Load += new System.EventHandler(this.frmPagarVis_Load);
            this.Shown += new System.EventHandler(this.frmPagarVis_Shown);
            this.gbxPagar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxPagar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagar)).EndInit();
            this.gbxOpcoes.ResumeLayout(false);
            this.gbxOpcoes.PerformLayout();
            this.gbxPorData.ResumeLayout(false);
            this.gbxPorData.PerformLayout();
            this.gbxPagarFornecedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxPagarAcumula)).EndInit();
            this.gbxAcumulaOp.ResumeLayout(false);
            this.gbxAcumulaOp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagarAcumula)).EndInit();
            this.gbxValores.ResumeLayout(false);
            this.gbxValores.PerformLayout();
            this.gbxData.ResumeLayout(false);
            this.gbxData.PerformLayout();
            this.gbxInadimplencia.ResumeLayout(false);
            this.gbxInadimplencia.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbxPagar;
        private System.Windows.Forms.DataGridView dgvPagar;
        private System.Windows.Forms.GroupBox gbxOpcoes;
        private System.Windows.Forms.RadioButton rbnAtrasados;
        private System.Windows.Forms.RadioButton rbnApartirdeHoje;
        private System.Windows.Forms.RadioButton rbnTodos;
        private System.Windows.Forms.GroupBox gbxPorData;
        private System.Windows.Forms.RadioButton rbnDataPrev;
        private System.Windows.Forms.RadioButton rbnDataVenc;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxPagarFornecedor;
        private System.Windows.Forms.DataGridView dgvPagarAcumula;
        private System.Windows.Forms.GroupBox gbxAcumulaOp;
        private System.Windows.Forms.RadioButton rbnAcumulaTudo;
        private System.Windows.Forms.RadioButton rbnAcumula90;
        private System.Windows.Forms.RadioButton rbnAcumulaDia;
        private System.Windows.Forms.RadioButton rbnAcumulaVencido;
        private System.Windows.Forms.GroupBox gbxValores;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxAPagarTudo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxAPagar90;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxAPagarNoMes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxAPagarVencido;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstPesquisa;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripButton tspMnuImprimir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbxPagar;
        private System.Windows.Forms.PictureBox pbxPagarAcumula;
        private System.Windows.Forms.RadioButton rbnUmDia;
        private System.Windows.Forms.GroupBox gbxData;
        private System.Windows.Forms.TextBox tbxDataAtual;
        private System.Windows.Forms.ToolStripButton tspAnalise;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxTotalVisualizacao;
        private System.Windows.Forms.GroupBox gbxInadimplencia;
        private System.Windows.Forms.CheckBox ckxPendencias;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxAPagar60;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxAPagar30;
        private System.Windows.Forms.RadioButton rbnAcumula60;
        private System.Windows.Forms.RadioButton rbnAcumula30;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxAPagar07;
    }
}