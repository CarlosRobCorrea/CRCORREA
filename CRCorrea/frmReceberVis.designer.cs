namespace CRCorrea
{
    partial class frmReceberVis
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceberVis));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxReceber = new System.Windows.Forms.GroupBox();
            this.pbxReceber = new System.Windows.Forms.PictureBox();
            this.dgvReceber = new System.Windows.Forms.DataGridView();
            this.gbxOpcoes = new System.Windows.Forms.GroupBox();
            this.rbnUmDia = new System.Windows.Forms.RadioButton();
            this.rbnAtrasados = new System.Windows.Forms.RadioButton();
            this.rbnApartirdeHoje = new System.Windows.Forms.RadioButton();
            this.rbnTodos = new System.Windows.Forms.RadioButton();
            this.gbxPorData = new System.Windows.Forms.GroupBox();
            this.rbnDataPrev = new System.Windows.Forms.RadioButton();
            this.rbnDataVenc = new System.Windows.Forms.RadioButton();
            this.gbxReceberCliente = new System.Windows.Forms.GroupBox();
            this.pbxReceberAcumula = new System.Windows.Forms.PictureBox();
            this.gbxAcumulaOp = new System.Windows.Forms.GroupBox();
            this.rbnAcumulaTudo = new System.Windows.Forms.RadioButton();
            this.rbnAcumula90 = new System.Windows.Forms.RadioButton();
            this.rbnAcumulaDia = new System.Windows.Forms.RadioButton();
            this.rbnAcumulaVencido = new System.Windows.Forms.RadioButton();
            this.dgvReceberAcumula = new System.Windows.Forms.DataGridView();
            this.gbxValores = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxAReceberTudo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxAReceber90 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxAReceberNoMes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxAReceberVencido = new System.Windows.Forms.TextBox();
            this.gbxTipoDocumento = new System.Windows.Forms.GroupBox();
            this.dgvReceberTipoDocumento = new System.Windows.Forms.DataGridView();
            this.gbxConferidos = new System.Windows.Forms.GroupBox();
            this.rbnConferidosNao = new System.Windows.Forms.RadioButton();
            this.rbnConferidos = new System.Windows.Forms.RadioButton();
            this.rbnConferidosTodos = new System.Windows.Forms.RadioButton();
            this.gbxData = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxDataAtualAte = new System.Windows.Forms.TextBox();
            this.tbxDataAtualDe = new System.Windows.Forms.TextBox();
            this.gbxInadimplencia = new System.Windows.Forms.GroupBox();
            this.ckxInadimplente = new System.Windows.Forms.CheckBox();
            this.tbxTotalVisualizacao = new System.Windows.Forms.TextBox();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspMnuImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tbxPesquisa = new System.Windows.Forms.ToolStripTextBox();
            this.bwrReceberAcumula = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gbxReceber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxReceber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceber)).BeginInit();
            this.gbxOpcoes.SuspendLayout();
            this.gbxPorData.SuspendLayout();
            this.gbxReceberCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxReceberAcumula)).BeginInit();
            this.gbxAcumulaOp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceberAcumula)).BeginInit();
            this.gbxValores.SuspendLayout();
            this.gbxTipoDocumento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceberTipoDocumento)).BeginInit();
            this.gbxConferidos.SuspendLayout();
            this.gbxData.SuspendLayout();
            this.gbxInadimplencia.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxReceber
            // 
            this.gbxReceber.Controls.Add(this.pbxReceber);
            this.gbxReceber.Controls.Add(this.dgvReceber);
            this.gbxReceber.Location = new System.Drawing.Point(12, 69);
            this.gbxReceber.Name = "gbxReceber";
            this.gbxReceber.Size = new System.Drawing.Size(996, 323);
            this.gbxReceber.TabIndex = 0;
            this.gbxReceber.TabStop = false;
            this.gbxReceber.Text = "Títulos";
            this.toolTip1.SetToolTip(this.gbxReceber, "Grupo Titulos a Receber");
            // 
            // pbxReceber
            // 
            this.pbxReceber.BackColor = System.Drawing.Color.White;
            this.pbxReceber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxReceber.Location = new System.Drawing.Point(6, 17);
            this.pbxReceber.Name = "pbxReceber";
            this.pbxReceber.Size = new System.Drawing.Size(984, 297);
            this.pbxReceber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxReceber.TabIndex = 139;
            this.pbxReceber.TabStop = false;
            this.toolTip1.SetToolTip(this.pbxReceber, "Imagem Receber");
            this.pbxReceber.Visible = false;
            // 
            // dgvReceber
            // 
            this.dgvReceber.AccessibleDescription = "Receber";
            this.dgvReceber.AllowUserToAddRows = false;
            this.dgvReceber.AllowUserToDeleteRows = false;
            this.dgvReceber.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvReceber.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReceber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvReceber.BackgroundColor = System.Drawing.Color.White;
            this.dgvReceber.ColumnHeadersHeight = 28;
            this.dgvReceber.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReceber.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReceber.Location = new System.Drawing.Point(6, 14);
            this.dgvReceber.MultiSelect = false;
            this.dgvReceber.Name = "dgvReceber";
            this.dgvReceber.ReadOnly = true;
            this.dgvReceber.RowHeadersVisible = false;
            this.dgvReceber.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvReceber.Size = new System.Drawing.Size(984, 303);
            this.dgvReceber.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dgvReceber, "Grade de clsVisualização Ctas a Receber");
            this.dgvReceber.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvReceber_MouseDoubleClick);
            // 
            // gbxOpcoes
            // 
            this.gbxOpcoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxOpcoes.Controls.Add(this.rbnUmDia);
            this.gbxOpcoes.Controls.Add(this.rbnAtrasados);
            this.gbxOpcoes.Controls.Add(this.rbnApartirdeHoje);
            this.gbxOpcoes.Controls.Add(this.rbnTodos);
            this.gbxOpcoes.Location = new System.Drawing.Point(12, 2);
            this.gbxOpcoes.Name = "gbxOpcoes";
            this.gbxOpcoes.Size = new System.Drawing.Size(209, 62);
            this.gbxOpcoes.TabIndex = 0;
            this.gbxOpcoes.TabStop = false;
            this.gbxOpcoes.Text = "Situação da clsVisualização dos Títulos";
            this.toolTip1.SetToolTip(this.gbxOpcoes, "Grupo de Opções Filtro");
            // 
            // rbnUmDia
            // 
            this.rbnUmDia.AccessibleDescription = "Um Dia";
            this.rbnUmDia.AutoSize = true;
            this.rbnUmDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnUmDia.Location = new System.Drawing.Point(80, 37);
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
            this.rbnAtrasados.AutoSize = true;
            this.rbnAtrasados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAtrasados.Location = new System.Drawing.Point(5, 37);
            this.rbnAtrasados.Name = "rbnAtrasados";
            this.rbnAtrasados.Size = new System.Drawing.Size(74, 17);
            this.rbnAtrasados.TabIndex = 2;
            this.rbnAtrasados.Text = "Em Atraso";
            this.toolTip1.SetToolTip(this.rbnAtrasados, "Em Atraso");
            this.rbnAtrasados.UseVisualStyleBackColor = true;
            this.rbnAtrasados.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnAtrasados.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtrasados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtrasados.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnApartirdeHoje
            // 
            this.rbnApartirdeHoje.AutoSize = true;
            this.rbnApartirdeHoje.Checked = true;
            this.rbnApartirdeHoje.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnApartirdeHoje.Location = new System.Drawing.Point(79, 18);
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
            this.rbnTodos.AutoSize = true;
            this.rbnTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTodos.Location = new System.Drawing.Point(6, 18);
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
            this.gbxPorData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxPorData.Controls.Add(this.rbnDataPrev);
            this.gbxPorData.Controls.Add(this.rbnDataVenc);
            this.gbxPorData.Location = new System.Drawing.Point(535, 12);
            this.gbxPorData.Name = "gbxPorData";
            this.gbxPorData.Size = new System.Drawing.Size(130, 46);
            this.gbxPorData.TabIndex = 3;
            this.gbxPorData.TabStop = false;
            this.gbxPorData.Text = "Visualizar pela:";
            this.toolTip1.SetToolTip(this.gbxPorData, "Grupo de Opções Filtro");
            // 
            // rbnDataPrev
            // 
            this.rbnDataPrev.AutoSize = true;
            this.rbnDataPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnDataPrev.Location = new System.Drawing.Point(6, 26);
            this.rbnDataPrev.Name = "rbnDataPrev";
            this.rbnDataPrev.Size = new System.Drawing.Size(107, 17);
            this.rbnDataPrev.TabIndex = 1;
            this.rbnDataPrev.Text = "Data de Previsão";
            this.toolTip1.SetToolTip(this.rbnDataPrev, "Pedidos em Aberto");
            this.rbnDataPrev.UseVisualStyleBackColor = true;
            this.rbnDataPrev.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnDataPrev.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnDataPrev.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnDataPrev.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnDataVenc
            // 
            this.rbnDataVenc.AutoSize = true;
            this.rbnDataVenc.Checked = true;
            this.rbnDataVenc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnDataVenc.Location = new System.Drawing.Point(6, 11);
            this.rbnDataVenc.Name = "rbnDataVenc";
            this.rbnDataVenc.Size = new System.Drawing.Size(121, 17);
            this.rbnDataVenc.TabIndex = 0;
            this.rbnDataVenc.TabStop = true;
            this.rbnDataVenc.Text = "Data de Vencimento";
            this.toolTip1.SetToolTip(this.rbnDataVenc, "Todos os Pedidos");
            this.rbnDataVenc.UseVisualStyleBackColor = true;
            this.rbnDataVenc.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnDataVenc.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnDataVenc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnDataVenc.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxReceberCliente
            // 
            this.gbxReceberCliente.Controls.Add(this.pbxReceberAcumula);
            this.gbxReceberCliente.Controls.Add(this.gbxAcumulaOp);
            this.gbxReceberCliente.Controls.Add(this.dgvReceberAcumula);
            this.gbxReceberCliente.Location = new System.Drawing.Point(194, 389);
            this.gbxReceberCliente.Name = "gbxReceberCliente";
            this.gbxReceberCliente.Size = new System.Drawing.Size(485, 204);
            this.gbxReceberCliente.TabIndex = 91;
            this.gbxReceberCliente.TabStop = false;
            this.gbxReceberCliente.Text = "Valor Acumulado a Receber por Cliente (Valor e %)";
            this.toolTip1.SetToolTip(this.gbxReceberCliente, "Grupo Valor Acumulado a Pagar por Fornecedor (Valor e %)");
            // 
            // pbxReceberAcumula
            // 
            this.pbxReceberAcumula.BackColor = System.Drawing.Color.White;
            this.pbxReceberAcumula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxReceberAcumula.Location = new System.Drawing.Point(121, 20);
            this.pbxReceberAcumula.Name = "pbxReceberAcumula";
            this.pbxReceberAcumula.Size = new System.Drawing.Size(353, 167);
            this.pbxReceberAcumula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxReceberAcumula.TabIndex = 139;
            this.pbxReceberAcumula.TabStop = false;
            this.toolTip1.SetToolTip(this.pbxReceberAcumula, "Imagem Acumua");
            this.pbxReceberAcumula.Visible = false;
            // 
            // gbxAcumulaOp
            // 
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumulaTudo);
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumula90);
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumulaDia);
            this.gbxAcumulaOp.Controls.Add(this.rbnAcumulaVencido);
            this.gbxAcumulaOp.Location = new System.Drawing.Point(6, 53);
            this.gbxAcumulaOp.Name = "gbxAcumulaOp";
            this.gbxAcumulaOp.Size = new System.Drawing.Size(107, 116);
            this.gbxAcumulaOp.TabIndex = 131;
            this.gbxAcumulaOp.TabStop = false;
            this.gbxAcumulaOp.Text = "Filtro Dt Previsão";
            this.toolTip1.SetToolTip(this.gbxAcumulaOp, "Tipo de Opção");
            // 
            // rbnAcumulaTudo
            // 
            this.rbnAcumulaTudo.AutoSize = true;
            this.rbnAcumulaTudo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumulaTudo.Location = new System.Drawing.Point(6, 89);
            this.rbnAcumulaTudo.Name = "rbnAcumulaTudo";
            this.rbnAcumulaTudo.Size = new System.Drawing.Size(49, 17);
            this.rbnAcumulaTudo.TabIndex = 133;
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
            this.rbnAcumula90.AutoSize = true;
            this.rbnAcumula90.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumula90.Location = new System.Drawing.Point(6, 66);
            this.rbnAcumula90.Name = "rbnAcumula90";
            this.rbnAcumula90.Size = new System.Drawing.Size(59, 17);
            this.rbnAcumula90.TabIndex = 132;
            this.rbnAcumula90.Text = "90 dias";
            this.toolTip1.SetToolTip(this.rbnAcumula90, "Todos os Pedidos");
            this.rbnAcumula90.UseVisualStyleBackColor = true;
            this.rbnAcumula90.Click += new System.EventHandler(this.rbnAcumula90_Click);
            this.rbnAcumula90.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAcumula90.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAcumula90.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAcumulaDia
            // 
            this.rbnAcumulaDia.AutoSize = true;
            this.rbnAcumulaDia.Checked = true;
            this.rbnAcumulaDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumulaDia.Location = new System.Drawing.Point(6, 43);
            this.rbnAcumulaDia.Name = "rbnAcumulaDia";
            this.rbnAcumulaDia.Size = new System.Drawing.Size(56, 17);
            this.rbnAcumulaDia.TabIndex = 131;
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
            this.rbnAcumulaVencido.AutoSize = true;
            this.rbnAcumulaVencido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAcumulaVencido.Location = new System.Drawing.Point(6, 20);
            this.rbnAcumulaVencido.Name = "rbnAcumulaVencido";
            this.rbnAcumulaVencido.Size = new System.Drawing.Size(62, 17);
            this.rbnAcumulaVencido.TabIndex = 130;
            this.rbnAcumulaVencido.Text = "Vencido";
            this.toolTip1.SetToolTip(this.rbnAcumulaVencido, "Todos os Pedidos");
            this.rbnAcumulaVencido.UseVisualStyleBackColor = true;
            this.rbnAcumulaVencido.Click += new System.EventHandler(this.rbnAcumulaVencido_Click);
            this.rbnAcumulaVencido.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAcumulaVencido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAcumulaVencido.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // dgvReceberAcumula
            // 
            this.dgvReceberAcumula.AccessibleDescription = "A Receber por Cliente";
            this.dgvReceberAcumula.AllowUserToAddRows = false;
            this.dgvReceberAcumula.AllowUserToDeleteRows = false;
            this.dgvReceberAcumula.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvReceberAcumula.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvReceberAcumula.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvReceberAcumula.BackgroundColor = System.Drawing.Color.White;
            this.dgvReceberAcumula.ColumnHeadersHeight = 28;
            this.dgvReceberAcumula.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReceberAcumula.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvReceberAcumula.Location = new System.Drawing.Point(120, 11);
            this.dgvReceberAcumula.MultiSelect = false;
            this.dgvReceberAcumula.Name = "dgvReceberAcumula";
            this.dgvReceberAcumula.ReadOnly = true;
            this.dgvReceberAcumula.RowHeadersVisible = false;
            this.dgvReceberAcumula.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvReceberAcumula.Size = new System.Drawing.Size(353, 182);
            this.dgvReceberAcumula.TabIndex = 125;
            this.toolTip1.SetToolTip(this.dgvReceberAcumula, "A Receber por Cliente");
            // 
            // gbxValores
            // 
            this.gbxValores.Controls.Add(this.label4);
            this.gbxValores.Controls.Add(this.tbxAReceberTudo);
            this.gbxValores.Controls.Add(this.label2);
            this.gbxValores.Controls.Add(this.tbxAReceber90);
            this.gbxValores.Controls.Add(this.label1);
            this.gbxValores.Controls.Add(this.tbxAReceberNoMes);
            this.gbxValores.Controls.Add(this.label3);
            this.gbxValores.Controls.Add(this.tbxAReceberVencido);
            this.gbxValores.Location = new System.Drawing.Point(12, 389);
            this.gbxValores.Name = "gbxValores";
            this.gbxValores.Size = new System.Drawing.Size(176, 201);
            this.gbxValores.TabIndex = 131;
            this.gbxValores.TabStop = false;
            this.gbxValores.Text = "Totais Dt Previsão";
            this.toolTip1.SetToolTip(this.gbxValores, "Grupo de Valores Resumo");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 93;
            this.label4.Text = "Tudo:";
            // 
            // tbxAReceberTudo
            // 
            this.tbxAReceberTudo.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAReceberTudo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAReceberTudo.Location = new System.Drawing.Point(60, 105);
            this.tbxAReceberTudo.Name = "tbxAReceberTudo";
            this.tbxAReceberTudo.ReadOnly = true;
            this.tbxAReceberTudo.Size = new System.Drawing.Size(108, 22);
            this.tbxAReceberTudo.TabIndex = 92;
            this.tbxAReceberTudo.Tag = "";
            this.tbxAReceberTudo.Text = "5.000.000,00";
            this.tbxAReceberTudo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAReceberTudo, "Total de Pedidos em Aberto");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 91;
            this.label2.Text = "90 Dias:";
            // 
            // tbxAReceber90
            // 
            this.tbxAReceber90.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAReceber90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAReceber90.Location = new System.Drawing.Point(60, 77);
            this.tbxAReceber90.Name = "tbxAReceber90";
            this.tbxAReceber90.ReadOnly = true;
            this.tbxAReceber90.Size = new System.Drawing.Size(108, 22);
            this.tbxAReceber90.TabIndex = 90;
            this.tbxAReceber90.Tag = "";
            this.tbxAReceber90.Text = "5.000.000,00";
            this.tbxAReceber90.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAReceber90, "Total de Pedidos em Aberto");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "No Dia:";
            // 
            // tbxAReceberNoMes
            // 
            this.tbxAReceberNoMes.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAReceberNoMes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAReceberNoMes.Location = new System.Drawing.Point(60, 49);
            this.tbxAReceberNoMes.Name = "tbxAReceberNoMes";
            this.tbxAReceberNoMes.ReadOnly = true;
            this.tbxAReceberNoMes.Size = new System.Drawing.Size(108, 22);
            this.tbxAReceberNoMes.TabIndex = 88;
            this.tbxAReceberNoMes.Tag = "";
            this.tbxAReceberNoMes.Text = "5.000.000,00";
            this.tbxAReceberNoMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAReceberNoMes, "Total de Pedidos em Aberto");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 87;
            this.label3.Text = "Vencido:";
            // 
            // tbxAReceberVencido
            // 
            this.tbxAReceberVencido.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAReceberVencido.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAReceberVencido.Location = new System.Drawing.Point(60, 21);
            this.tbxAReceberVencido.Name = "tbxAReceberVencido";
            this.tbxAReceberVencido.ReadOnly = true;
            this.tbxAReceberVencido.Size = new System.Drawing.Size(108, 22);
            this.tbxAReceberVencido.TabIndex = 86;
            this.tbxAReceberVencido.Tag = "";
            this.tbxAReceberVencido.Text = "5.000.000,00";
            this.tbxAReceberVencido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxAReceberVencido, "Total de Pedidos em Aberto");
            // 
            // gbxTipoDocumento
            // 
            this.gbxTipoDocumento.Controls.Add(this.dgvReceberTipoDocumento);
            this.gbxTipoDocumento.Location = new System.Drawing.Point(685, 413);
            this.gbxTipoDocumento.Name = "gbxTipoDocumento";
            this.gbxTipoDocumento.Size = new System.Drawing.Size(323, 179);
            this.gbxTipoDocumento.TabIndex = 135;
            this.gbxTipoDocumento.TabStop = false;
            this.gbxTipoDocumento.Text = "Acumulado por Tipo de Documento conforme clsVisualização";
            this.toolTip1.SetToolTip(this.gbxTipoDocumento, "Acumulado por Tipo de Documento");
            // 
            // dgvReceberTipoDocumento
            // 
            this.dgvReceberTipoDocumento.AccessibleDescription = "A Receber por Tipo de Documento";
            this.dgvReceberTipoDocumento.AllowUserToAddRows = false;
            this.dgvReceberTipoDocumento.AllowUserToDeleteRows = false;
            this.dgvReceberTipoDocumento.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvReceberTipoDocumento.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvReceberTipoDocumento.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvReceberTipoDocumento.BackgroundColor = System.Drawing.Color.White;
            this.dgvReceberTipoDocumento.ColumnHeadersHeight = 28;
            this.dgvReceberTipoDocumento.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReceberTipoDocumento.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvReceberTipoDocumento.Location = new System.Drawing.Point(7, 10);
            this.dgvReceberTipoDocumento.MultiSelect = false;
            this.dgvReceberTipoDocumento.Name = "dgvReceberTipoDocumento";
            this.dgvReceberTipoDocumento.ReadOnly = true;
            this.dgvReceberTipoDocumento.RowHeadersVisible = false;
            this.dgvReceberTipoDocumento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvReceberTipoDocumento.Size = new System.Drawing.Size(302, 162);
            this.dgvReceberTipoDocumento.TabIndex = 125;
            this.toolTip1.SetToolTip(this.dgvReceberTipoDocumento, "A Receber por Tipo de Documento");
            // 
            // gbxConferidos
            // 
            this.gbxConferidos.AccessibleDescription = "Conferidos";
            this.gbxConferidos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxConferidos.Controls.Add(this.rbnConferidosNao);
            this.gbxConferidos.Controls.Add(this.rbnConferidos);
            this.gbxConferidos.Controls.Add(this.rbnConferidosTodos);
            this.gbxConferidos.Location = new System.Drawing.Point(225, 2);
            this.gbxConferidos.Name = "gbxConferidos";
            this.gbxConferidos.Size = new System.Drawing.Size(114, 63);
            this.gbxConferidos.TabIndex = 1;
            this.gbxConferidos.TabStop = false;
            this.gbxConferidos.Text = "Conferidos :";
            this.toolTip1.SetToolTip(this.gbxConferidos, "Conferidos");
            // 
            // rbnConferidosNao
            // 
            this.rbnConferidosNao.AccessibleDescription = "Não Conferidos";
            this.rbnConferidosNao.AutoSize = true;
            this.rbnConferidosNao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnConferidosNao.Location = new System.Drawing.Point(6, 42);
            this.rbnConferidosNao.Name = "rbnConferidosNao";
            this.rbnConferidosNao.Size = new System.Drawing.Size(99, 17);
            this.rbnConferidosNao.TabIndex = 2;
            this.rbnConferidosNao.TabStop = true;
            this.rbnConferidosNao.Text = "Não Conferidos";
            this.toolTip1.SetToolTip(this.rbnConferidosNao, "Não Conferidos");
            this.rbnConferidosNao.UseVisualStyleBackColor = true;
            this.rbnConferidosNao.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnConferidosNao.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnConferidosNao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnConferidosNao.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnConferidos
            // 
            this.rbnConferidos.AccessibleDescription = "Só os Conferidos";
            this.rbnConferidos.AutoSize = true;
            this.rbnConferidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnConferidos.Location = new System.Drawing.Point(6, 26);
            this.rbnConferidos.Name = "rbnConferidos";
            this.rbnConferidos.Size = new System.Drawing.Size(106, 17);
            this.rbnConferidos.TabIndex = 1;
            this.rbnConferidos.TabStop = true;
            this.rbnConferidos.Text = "Só os Conferidos";
            this.toolTip1.SetToolTip(this.rbnConferidos, "Só os Conferidos");
            this.rbnConferidos.UseVisualStyleBackColor = true;
            this.rbnConferidos.CheckedChanged += new System.EventHandler(this.rbnConferidos_CheckedChanged);
            this.rbnConferidos.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnConferidos.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnConferidos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnConferidos.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnConferidosTodos
            // 
            this.rbnConferidosTodos.AccessibleDescription = "Todos os Conferidos";
            this.rbnConferidosTodos.AutoSize = true;
            this.rbnConferidosTodos.Checked = true;
            this.rbnConferidosTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnConferidosTodos.Location = new System.Drawing.Point(6, 11);
            this.rbnConferidosTodos.Name = "rbnConferidosTodos";
            this.rbnConferidosTodos.Size = new System.Drawing.Size(54, 17);
            this.rbnConferidosTodos.TabIndex = 0;
            this.rbnConferidosTodos.TabStop = true;
            this.rbnConferidosTodos.Text = "Todos";
            this.toolTip1.SetToolTip(this.rbnConferidosTodos, "Todos os Conferidos");
            this.rbnConferidosTodos.UseVisualStyleBackColor = true;
            this.rbnConferidosTodos.Click += new System.EventHandler(this.rbnTodos_Click);
            this.rbnConferidosTodos.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnConferidosTodos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnConferidosTodos.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxData
            // 
            this.gbxData.AccessibleDescription = "Visualizar pela:";
            this.gbxData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxData.Controls.Add(this.label6);
            this.gbxData.Controls.Add(this.tbxDataAtualAte);
            this.gbxData.Controls.Add(this.tbxDataAtualDe);
            this.gbxData.Location = new System.Drawing.Point(345, 13);
            this.gbxData.Name = "gbxData";
            this.gbxData.Size = new System.Drawing.Size(188, 46);
            this.gbxData.TabIndex = 2;
            this.gbxData.TabStop = false;
            this.gbxData.Text = "Data p/Visualizar";
            this.toolTip1.SetToolTip(this.gbxData, "Grupo de Opções Filtro");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "A";
            // 
            // tbxDataAtualAte
            // 
            this.tbxDataAtualAte.AccessibleDescription = "Ate a Data";
            this.tbxDataAtualAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDataAtualAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataAtualAte.Location = new System.Drawing.Point(103, 19);
            this.tbxDataAtualAte.MaxLength = 20;
            this.tbxDataAtualAte.Name = "tbxDataAtualAte";
            this.tbxDataAtualAte.Size = new System.Drawing.Size(81, 21);
            this.tbxDataAtualAte.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbxDataAtualAte, "Ate a Data");
            this.tbxDataAtualAte.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataAtualAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataAtualAte.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxDataAtualDe
            // 
            this.tbxDataAtualDe.AccessibleDescription = "Data Atual";
            this.tbxDataAtualDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDataAtualDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataAtualDe.Location = new System.Drawing.Point(5, 19);
            this.tbxDataAtualDe.MaxLength = 20;
            this.tbxDataAtualDe.Name = "tbxDataAtualDe";
            this.tbxDataAtualDe.Size = new System.Drawing.Size(81, 21);
            this.tbxDataAtualDe.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxDataAtualDe, "Data Atual");
            this.tbxDataAtualDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataAtualDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataAtualDe.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxInadimplencia
            // 
            this.gbxInadimplencia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxInadimplencia.Controls.Add(this.ckxInadimplente);
            this.gbxInadimplencia.Location = new System.Drawing.Point(669, 13);
            this.gbxInadimplencia.Name = "gbxInadimplencia";
            this.gbxInadimplencia.Size = new System.Drawing.Size(100, 46);
            this.gbxInadimplencia.TabIndex = 4;
            this.gbxInadimplencia.TabStop = false;
            this.gbxInadimplencia.Text = "Inadimplentes";
            this.toolTip1.SetToolTip(this.gbxInadimplencia, "Grupo de Opções Filtro");
            // 
            // ckxInadimplente
            // 
            this.ckxInadimplente.AutoSize = true;
            this.ckxInadimplente.Location = new System.Drawing.Point(5, 14);
            this.ckxInadimplente.Name = "ckxInadimplente";
            this.ckxInadimplente.Size = new System.Drawing.Size(93, 30);
            this.ckxInadimplente.TabIndex = 0;
            this.ckxInadimplente.Text = "Com os \r\nInadimplentes";
            this.ckxInadimplente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckxInadimplente.UseVisualStyleBackColor = true;
            this.ckxInadimplente.Click += new System.EventHandler(this.ckxInadimplente_Click);
            this.ckxInadimplente.Enter += new System.EventHandler(this.ControlEnter);
            this.ckxInadimplente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.ckxInadimplente.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxTotalVisualizacao
            // 
            this.tbxTotalVisualizacao.AccessibleDescription = "Total clsVisualização";
            this.tbxTotalVisualizacao.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTotalVisualizacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTotalVisualizacao.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalVisualizacao.Location = new System.Drawing.Point(864, 389);
            this.tbxTotalVisualizacao.MaxLength = 20;
            this.tbxTotalVisualizacao.Name = "tbxTotalVisualizacao";
            this.tbxTotalVisualizacao.ReadOnly = true;
            this.tbxTotalVisualizacao.Size = new System.Drawing.Size(81, 21);
            this.tbxTotalVisualizacao.TabIndex = 136;
            this.tbxTotalVisualizacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxTotalVisualizacao, "Total clsVisualização");
            // 
            // tspTool
            // 
            this.tspTool.AllowMerge = false;
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspImprimir,
            this.tspMnuImprimir,
            this.tspExcluir,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tbxPesquisa});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(12, 595);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(751, 47);
            this.tspTool.TabIndex = 9;
            // 
            // tspIncluir
            // 
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
            // tspImprimir
            // 
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
            // tspExcluir
            // 
            this.tspExcluir.AutoSize = false;
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tspExcluir.Image")));
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(66, 42);
            this.tspExcluir.Text = "Excluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.Visible = false;
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
            // 
            // tspRetornar
            // 
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
            // tbxPesquisa
            // 
            this.tbxPesquisa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxPesquisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxPesquisa.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPesquisa.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tbxPesquisa.Name = "tbxPesquisa";
            this.tbxPesquisa.Size = new System.Drawing.Size(280, 21);
            this.tbxPesquisa.ToolTipText = "Procurar por : Nrº NFE,  Data,  Cognome ou Pedido";
            this.tbxPesquisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxPesquisa_KeyUp);
            // 
            // bwrReceberAcumula
            // 
            this.bwrReceberAcumula.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrReceber_DoWork);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(773, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(235, 52);
            this.label5.TabIndex = 132;
            this.label5.Text = "Contas a Receber";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(727, 393);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 13);
            this.label7.TabIndex = 137;
            this.label7.Text = "Total desta clsVisualização :";
            // 
            // frmReceberVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbxTotalVisualizacao);
            this.Controls.Add(this.gbxInadimplencia);
            this.Controls.Add(this.gbxData);
            this.Controls.Add(this.gbxConferidos);
            this.Controls.Add(this.gbxTipoDocumento);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbxValores);
            this.Controls.Add(this.gbxReceberCliente);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxPorData);
            this.Controls.Add(this.gbxOpcoes);
            this.Controls.Add(this.gbxReceber);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReceberVis";
            this.ShowInTaskbar = false;
            this.Tag = "550";
            this.Text = "Contas a Receber -  clsVisualização";
            this.toolTip1.SetToolTip(this, "Contas a Receber -  clsVisualização");
            this.Activated += new System.EventHandler(this.frmReceberVis_Activated);
            this.Load += new System.EventHandler(this.frmReceberVis_Load);
            this.Shown += new System.EventHandler(this.frmReceberVis_Shown);
            this.gbxReceber.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxReceber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceber)).EndInit();
            this.gbxOpcoes.ResumeLayout(false);
            this.gbxOpcoes.PerformLayout();
            this.gbxPorData.ResumeLayout(false);
            this.gbxPorData.PerformLayout();
            this.gbxReceberCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxReceberAcumula)).EndInit();
            this.gbxAcumulaOp.ResumeLayout(false);
            this.gbxAcumulaOp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceberAcumula)).EndInit();
            this.gbxValores.ResumeLayout(false);
            this.gbxValores.PerformLayout();
            this.gbxTipoDocumento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceberTipoDocumento)).EndInit();
            this.gbxConferidos.ResumeLayout(false);
            this.gbxConferidos.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbxReceber;
        private System.Windows.Forms.DataGridView dgvReceber;
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
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.ComponentModel.BackgroundWorker bwrReceber;
        private System.Windows.Forms.GroupBox gbxReceberCliente;
        private System.Windows.Forms.DataGridView dgvReceberAcumula;
        private System.ComponentModel.BackgroundWorker bwrReceberAcumula;
        private System.Windows.Forms.GroupBox gbxAcumulaOp;
        private System.Windows.Forms.RadioButton rbnAcumulaTudo;
        private System.Windows.Forms.RadioButton rbnAcumula90;
        private System.Windows.Forms.RadioButton rbnAcumulaDia;
        private System.Windows.Forms.RadioButton rbnAcumulaVencido;
        private System.Windows.Forms.GroupBox gbxValores;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxAReceberTudo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxAReceber90;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxAReceberNoMes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxAReceberVencido;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tbxPesquisa;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.PictureBox pbxReceber;
        private System.Windows.Forms.PictureBox pbxReceberAcumula;
        private System.Windows.Forms.ToolStripButton tspMnuImprimir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbnUmDia;
        private System.Windows.Forms.GroupBox gbxTipoDocumento;
        private System.Windows.Forms.DataGridView dgvReceberTipoDocumento;
        private System.Windows.Forms.GroupBox gbxConferidos;
        private System.Windows.Forms.RadioButton rbnConferidosNao;
        private System.Windows.Forms.RadioButton rbnConferidos;
        private System.Windows.Forms.RadioButton rbnConferidosTodos;
        private System.Windows.Forms.GroupBox gbxData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxDataAtualAte;
        private System.Windows.Forms.TextBox tbxDataAtualDe;
        private System.Windows.Forms.GroupBox gbxInadimplencia;
        private System.Windows.Forms.CheckBox ckxInadimplente;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxTotalVisualizacao;
    }
}