namespace CRCorrea
{
    partial class frmMaqCt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaqCt));
            this.tspMaqct = new System.Windows.Forms.ToolStrip();
            this.tsbSalvar = new System.Windows.Forms.ToolStripButton();
            this.tsbPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tsbAnterior = new System.Windows.Forms.ToolStripButton();
            this.tsbProximo = new System.Windows.Forms.ToolStripButton();
            this.tsbUltimo = new System.Windows.Forms.ToolStripButton();
            this.tsbRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspMaqctPrecoVis = new System.Windows.Forms.ToolStrip();
            this.tsbMaqctPrecoVis_Incluir = new System.Windows.Forms.ToolStripButton();
            this.tsbMaqctPrecoVis_Alterar = new System.Windows.Forms.ToolStripButton();
            this.tsbMaqctPrecoVis_Imprimir = new System.Windows.Forms.ToolStripButton();
            this.tsbMaqctPrecoVis_Excluir = new System.Windows.Forms.ToolStripButton();
            this.dgvMaqctpreco = new System.Windows.Forms.DataGridView();
            this.gbxPrincipal = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.cbxAtivo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.bwrmaqctpreco = new System.ComponentModel.BackgroundWorker();
            this.tclMaqctPreco = new System.Windows.Forms.TabControl();
            this.tpeMaqctPrecoVis = new System.Windows.Forms.TabPage();
            this.tpeMaqctPreco = new System.Windows.Forms.TabPage();
            this.tbxMaqctPreco_Data = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxMaqctPreco_Valor = new System.Windows.Forms.TextBox();
            this.tspMaqctPreco = new System.Windows.Forms.ToolStrip();
            this.tsbMaqctPreco_Salvar = new System.Windows.Forms.ToolStripButton();
            this.tspMaqctPreco_Retornar = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tspMaqct.SuspendLayout();
            this.tspMaqctPrecoVis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaqctpreco)).BeginInit();
            this.gbxPrincipal.SuspendLayout();
            this.tclMaqctPreco.SuspendLayout();
            this.tpeMaqctPrecoVis.SuspendLayout();
            this.tpeMaqctPreco.SuspendLayout();
            this.tspMaqctPreco.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspMaqct
            // 
            this.tspMaqct.AccessibleDescription = "Barra de Opções";
            this.tspMaqct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspMaqct.AutoSize = false;
            this.tspMaqct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspMaqct.Dock = System.Windows.Forms.DockStyle.None;
            this.tspMaqct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspMaqct.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspMaqct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSalvar,
            this.tsbPrimeiro,
            this.tsbAnterior,
            this.tsbProximo,
            this.tsbUltimo,
            this.tsbRetornar});
            this.tspMaqct.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspMaqct.Location = new System.Drawing.Point(317, 525);
            this.tspMaqct.Name = "tspMaqct";
            this.tspMaqct.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspMaqct.Size = new System.Drawing.Size(394, 46);
            this.tspMaqct.TabIndex = 2;
            this.tspMaqct.TabStop = true;
            this.tspMaqct.Text = "toolStrip1";
            // 
            // tsbSalvar
            // 
            this.tsbSalvar.AccessibleDescription = "Salvar";
            this.tsbSalvar.AutoSize = false;
            this.tsbSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tsbSalvar.Image")));
            this.tsbSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSalvar.Name = "tsbSalvar";
            this.tsbSalvar.Size = new System.Drawing.Size(62, 41);
            this.tsbSalvar.Text = "&Salvar";
            this.tsbSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tsbPrimeiro
            // 
            this.tsbPrimeiro.AccessibleDescription = "Primeiro";
            this.tsbPrimeiro.AutoSize = false;
            this.tsbPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbPrimeiro.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrimeiro.Image")));
            this.tsbPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrimeiro.Name = "tsbPrimeiro";
            this.tsbPrimeiro.Size = new System.Drawing.Size(62, 41);
            this.tsbPrimeiro.Text = "Pr&imeiro";
            this.tsbPrimeiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbPrimeiro.Click += new System.EventHandler(this.tsbPrimeiro_Click);
            // 
            // tsbAnterior
            // 
            this.tsbAnterior.AccessibleDescription = "Anterior";
            this.tsbAnterior.AutoSize = false;
            this.tsbAnterior.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbAnterior.Image = ((System.Drawing.Image)(resources.GetObject("tsbAnterior.Image")));
            this.tsbAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnterior.Name = "tsbAnterior";
            this.tsbAnterior.Size = new System.Drawing.Size(62, 41);
            this.tsbAnterior.Text = "&Anterior";
            this.tsbAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAnterior.Click += new System.EventHandler(this.tsbAnterior_Click);
            // 
            // tsbProximo
            // 
            this.tsbProximo.AccessibleDescription = "Próximo";
            this.tsbProximo.AutoSize = false;
            this.tsbProximo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbProximo.Image = ((System.Drawing.Image)(resources.GetObject("tsbProximo.Image")));
            this.tsbProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProximo.Name = "tsbProximo";
            this.tsbProximo.Size = new System.Drawing.Size(62, 41);
            this.tsbProximo.Text = "Pró&ximo";
            this.tsbProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbProximo.Click += new System.EventHandler(this.tsbProximo_Click);
            // 
            // tsbUltimo
            // 
            this.tsbUltimo.AccessibleDescription = "Último";
            this.tsbUltimo.AutoSize = false;
            this.tsbUltimo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbUltimo.Image = ((System.Drawing.Image)(resources.GetObject("tsbUltimo.Image")));
            this.tsbUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUltimo.Name = "tsbUltimo";
            this.tsbUltimo.Size = new System.Drawing.Size(62, 41);
            this.tsbUltimo.Text = "&Último";
            this.tsbUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbUltimo.Click += new System.EventHandler(this.tsbUltimo_Click);
            // 
            // tsbRetornar
            // 
            this.tsbRetornar.AccessibleDescription = "Retornar";
            this.tsbRetornar.AutoSize = false;
            this.tsbRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tsbRetornar.Image")));
            this.tsbRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRetornar.Name = "tsbRetornar";
            this.tsbRetornar.Size = new System.Drawing.Size(62, 41);
            this.tsbRetornar.Text = "Retorna&r";
            this.tsbRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // tspMaqctPrecoVis
            // 
            this.tspMaqctPrecoVis.AccessibleDescription = "Barra de Opções";
            this.tspMaqctPrecoVis.AllowMerge = false;
            this.tspMaqctPrecoVis.AutoSize = false;
            this.tspMaqctPrecoVis.Dock = System.Windows.Forms.DockStyle.Right;
            this.tspMaqctPrecoVis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMaqctPrecoVis_Incluir,
            this.tsbMaqctPrecoVis_Alterar,
            this.tsbMaqctPrecoVis_Imprimir,
            this.tsbMaqctPrecoVis_Excluir});
            this.tspMaqctPrecoVis.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspMaqctPrecoVis.Location = new System.Drawing.Point(299, 3);
            this.tspMaqctPrecoVis.Name = "tspMaqctPrecoVis";
            this.tspMaqctPrecoVis.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspMaqctPrecoVis.Size = new System.Drawing.Size(54, 268);
            this.tspMaqctPrecoVis.TabIndex = 1;
            // 
            // tsbMaqctPrecoVis_Incluir
            // 
            this.tsbMaqctPrecoVis_Incluir.AccessibleDescription = "Incluir";
            this.tsbMaqctPrecoVis_Incluir.AutoSize = false;
            this.tsbMaqctPrecoVis_Incluir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbMaqctPrecoVis_Incluir.ForeColor = System.Drawing.Color.Black;
            this.tsbMaqctPrecoVis_Incluir.Image = ((System.Drawing.Image)(resources.GetObject("tsbMaqctPrecoVis_Incluir.Image")));
            this.tsbMaqctPrecoVis_Incluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMaqctPrecoVis_Incluir.Margin = new System.Windows.Forms.Padding(0);
            this.tsbMaqctPrecoVis_Incluir.Name = "tsbMaqctPrecoVis_Incluir";
            this.tsbMaqctPrecoVis_Incluir.Size = new System.Drawing.Size(54, 31);
            this.tsbMaqctPrecoVis_Incluir.Text = "Incluir";
            this.tsbMaqctPrecoVis_Incluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbMaqctPrecoVis_Incluir.Click += new System.EventHandler(this.tsbMaqctPrecoVis_Incluir_Click);
            // 
            // tsbMaqctPrecoVis_Alterar
            // 
            this.tsbMaqctPrecoVis_Alterar.AccessibleDescription = "Alterar";
            this.tsbMaqctPrecoVis_Alterar.AutoSize = false;
            this.tsbMaqctPrecoVis_Alterar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbMaqctPrecoVis_Alterar.ForeColor = System.Drawing.Color.Black;
            this.tsbMaqctPrecoVis_Alterar.Image = ((System.Drawing.Image)(resources.GetObject("tsbMaqctPrecoVis_Alterar.Image")));
            this.tsbMaqctPrecoVis_Alterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMaqctPrecoVis_Alterar.Margin = new System.Windows.Forms.Padding(0);
            this.tsbMaqctPrecoVis_Alterar.Name = "tsbMaqctPrecoVis_Alterar";
            this.tsbMaqctPrecoVis_Alterar.Size = new System.Drawing.Size(54, 31);
            this.tsbMaqctPrecoVis_Alterar.Text = "Alterar";
            this.tsbMaqctPrecoVis_Alterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbMaqctPrecoVis_Alterar.Click += new System.EventHandler(this.tsbMaqctPrecoVis_Alterar_Click);
            // 
            // tsbMaqctPrecoVis_Imprimir
            // 
            this.tsbMaqctPrecoVis_Imprimir.AccessibleDescription = "Imprimir";
            this.tsbMaqctPrecoVis_Imprimir.AutoSize = false;
            this.tsbMaqctPrecoVis_Imprimir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbMaqctPrecoVis_Imprimir.ForeColor = System.Drawing.Color.Black;
            this.tsbMaqctPrecoVis_Imprimir.Image = ((System.Drawing.Image)(resources.GetObject("tsbMaqctPrecoVis_Imprimir.Image")));
            this.tsbMaqctPrecoVis_Imprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMaqctPrecoVis_Imprimir.Margin = new System.Windows.Forms.Padding(0);
            this.tsbMaqctPrecoVis_Imprimir.Name = "tsbMaqctPrecoVis_Imprimir";
            this.tsbMaqctPrecoVis_Imprimir.Size = new System.Drawing.Size(54, 31);
            this.tsbMaqctPrecoVis_Imprimir.Text = "Imprimir";
            this.tsbMaqctPrecoVis_Imprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbMaqctPrecoVis_Imprimir.Click += new System.EventHandler(this.tsbMaqctPrecoVis_Imprimir_Click);
            // 
            // tsbMaqctPrecoVis_Excluir
            // 
            this.tsbMaqctPrecoVis_Excluir.AutoSize = false;
            this.tsbMaqctPrecoVis_Excluir.Enabled = false;
            this.tsbMaqctPrecoVis_Excluir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbMaqctPrecoVis_Excluir.ForeColor = System.Drawing.Color.Black;
            this.tsbMaqctPrecoVis_Excluir.Image = ((System.Drawing.Image)(resources.GetObject("tsbMaqctPrecoVis_Excluir.Image")));
            this.tsbMaqctPrecoVis_Excluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMaqctPrecoVis_Excluir.Margin = new System.Windows.Forms.Padding(0);
            this.tsbMaqctPrecoVis_Excluir.Name = "tsbMaqctPrecoVis_Excluir";
            this.tsbMaqctPrecoVis_Excluir.Size = new System.Drawing.Size(54, 31);
            this.tsbMaqctPrecoVis_Excluir.Text = "Excluir";
            this.tsbMaqctPrecoVis_Excluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbMaqctPrecoVis_Excluir.Click += new System.EventHandler(this.tsbMaqctPrecoVis_Excluir_Click);
            // 
            // dgvMaqctpreco
            // 
            this.dgvMaqctpreco.AccessibleDescription = "Preços";
            this.dgvMaqctpreco.AllowUserToAddRows = false;
            this.dgvMaqctpreco.AllowUserToDeleteRows = false;
            this.dgvMaqctpreco.AllowUserToResizeColumns = false;
            this.dgvMaqctpreco.AllowUserToResizeRows = false;
            this.dgvMaqctpreco.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaqctpreco.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaqctpreco.Location = new System.Drawing.Point(6, 6);
            this.dgvMaqctpreco.MultiSelect = false;
            this.dgvMaqctpreco.Name = "dgvMaqctpreco";
            this.dgvMaqctpreco.ReadOnly = true;
            this.dgvMaqctpreco.RowHeadersVisible = false;
            this.dgvMaqctpreco.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaqctpreco.Size = new System.Drawing.Size(290, 262);
            this.dgvMaqctpreco.TabIndex = 0;
            this.dgvMaqctpreco.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaqctpreco_CellDoubleClick);
            // 
            // gbxPrincipal
            // 
            this.gbxPrincipal.AccessibleDescription = "Dados";
            this.gbxPrincipal.Controls.Add(this.label2);
            this.gbxPrincipal.Controls.Add(this.tbxCodigo);
            this.gbxPrincipal.Controls.Add(this.cbxAtivo);
            this.gbxPrincipal.Controls.Add(this.label7);
            this.gbxPrincipal.Controls.Add(this.label1);
            this.gbxPrincipal.Controls.Add(this.tbxNome);
            this.gbxPrincipal.Location = new System.Drawing.Point(328, 146);
            this.gbxPrincipal.Name = "gbxPrincipal";
            this.gbxPrincipal.Size = new System.Drawing.Size(364, 60);
            this.gbxPrincipal.TabIndex = 0;
            this.gbxPrincipal.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Ativo:";
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.AccessibleDescription = "Código";
            this.tbxCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Location = new System.Drawing.Point(6, 33);
            this.tbxCodigo.MaxLength = 20;
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(74, 21);
            this.tbxCodigo.TabIndex = 0;
            this.tbxCodigo.Tag = "";
            this.tbxCodigo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // cbxAtivo
            // 
            this.cbxAtivo.AccessibleDescription = "Ativo";
            this.cbxAtivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAtivo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAtivo.FormattingEnabled = true;
            this.cbxAtivo.Items.AddRange(new object[] {
            "Sim",
            "Não"});
            this.cbxAtivo.Location = new System.Drawing.Point(294, 33);
            this.cbxAtivo.Name = "cbxAtivo";
            this.cbxAtivo.Size = new System.Drawing.Size(64, 21);
            this.cbxAtivo.TabIndex = 2;
            this.cbxAtivo.Enter += new System.EventHandler(this.ControlEnter);
            this.cbxAtivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.cbxAtivo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Código:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "CT - Nome:";
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "CT - Nome";
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(86, 33);
            this.tbxNome.MaxLength = 50;
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(202, 21);
            this.tbxNome.TabIndex = 1;
            this.tbxNome.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tclMaqctPreco
            // 
            this.tclMaqctPreco.AccessibleDescription = "Preço";
            this.tclMaqctPreco.Controls.Add(this.tpeMaqctPrecoVis);
            this.tclMaqctPreco.Controls.Add(this.tpeMaqctPreco);
            this.tclMaqctPreco.Location = new System.Drawing.Point(328, 212);
            this.tclMaqctPreco.Name = "tclMaqctPreco";
            this.tclMaqctPreco.SelectedIndex = 0;
            this.tclMaqctPreco.Size = new System.Drawing.Size(364, 300);
            this.tclMaqctPreco.TabIndex = 1;
            this.tclMaqctPreco.SelectedIndexChanged += new System.EventHandler(this.tclMaqctPreco_SelectedIndexChanged);
            // 
            // tpeMaqctPrecoVis
            // 
            this.tpeMaqctPrecoVis.Controls.Add(this.tspMaqctPrecoVis);
            this.tpeMaqctPrecoVis.Controls.Add(this.dgvMaqctpreco);
            this.tpeMaqctPrecoVis.Location = new System.Drawing.Point(4, 22);
            this.tpeMaqctPrecoVis.Name = "tpeMaqctPrecoVis";
            this.tpeMaqctPrecoVis.Padding = new System.Windows.Forms.Padding(3);
            this.tpeMaqctPrecoVis.Size = new System.Drawing.Size(356, 274);
            this.tpeMaqctPrecoVis.TabIndex = 0;
            this.tpeMaqctPrecoVis.Text = "Visualização";
            this.tpeMaqctPrecoVis.UseVisualStyleBackColor = true;
            // 
            // tpeMaqctPreco
            // 
            this.tpeMaqctPreco.Controls.Add(this.tbxMaqctPreco_Data);
            this.tpeMaqctPreco.Controls.Add(this.label3);
            this.tpeMaqctPreco.Controls.Add(this.label4);
            this.tpeMaqctPreco.Controls.Add(this.tbxMaqctPreco_Valor);
            this.tpeMaqctPreco.Controls.Add(this.tspMaqctPreco);
            this.tpeMaqctPreco.Location = new System.Drawing.Point(4, 22);
            this.tpeMaqctPreco.Name = "tpeMaqctPreco";
            this.tpeMaqctPreco.Padding = new System.Windows.Forms.Padding(3);
            this.tpeMaqctPreco.Size = new System.Drawing.Size(356, 274);
            this.tpeMaqctPreco.TabIndex = 1;
            this.tpeMaqctPreco.Text = "Cadastro";
            this.tpeMaqctPreco.UseVisualStyleBackColor = true;
            // 
            // tbxMaqctPreco_Data
            // 
            this.tbxMaqctPreco_Data.AccessibleDescription = "Data";
            this.tbxMaqctPreco_Data.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMaqctPreco_Data.Location = new System.Drawing.Point(81, 124);
            this.tbxMaqctPreco_Data.MaxLength = 20;
            this.tbxMaqctPreco_Data.Name = "tbxMaqctPreco_Data";
            this.tbxMaqctPreco_Data.Size = new System.Drawing.Size(80, 21);
            this.tbxMaqctPreco_Data.TabIndex = 27;
            this.tbxMaqctPreco_Data.Tag = "";
            this.tbxMaqctPreco_Data.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxMaqctPreco_Data.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxMaqctPreco_Data.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Data:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Valor:";
            // 
            // tbxMaqctPreco_Valor
            // 
            this.tbxMaqctPreco_Valor.AccessibleDescription = "Valor";
            this.tbxMaqctPreco_Valor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMaqctPreco_Valor.Location = new System.Drawing.Point(167, 124);
            this.tbxMaqctPreco_Valor.MaxLength = 50;
            this.tbxMaqctPreco_Valor.Name = "tbxMaqctPreco_Valor";
            this.tbxMaqctPreco_Valor.Size = new System.Drawing.Size(95, 21);
            this.tbxMaqctPreco_Valor.TabIndex = 28;
            this.tbxMaqctPreco_Valor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxMaqctPreco_Valor.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxMaqctPreco_Valor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxMaqctPreco_Valor.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tspMaqctPreco
            // 
            this.tspMaqctPreco.AccessibleDescription = "Barra de Opções";
            this.tspMaqctPreco.AutoSize = false;
            this.tspMaqctPreco.Dock = System.Windows.Forms.DockStyle.Right;
            this.tspMaqctPreco.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspMaqctPreco.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspMaqctPreco.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMaqctPreco_Salvar,
            this.tspMaqctPreco_Retornar});
            this.tspMaqctPreco.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspMaqctPreco.Location = new System.Drawing.Point(298, 3);
            this.tspMaqctPreco.Name = "tspMaqctPreco";
            this.tspMaqctPreco.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspMaqctPreco.Size = new System.Drawing.Size(55, 268);
            this.tspMaqctPreco.TabIndex = 4;
            this.tspMaqctPreco.TabStop = true;
            this.tspMaqctPreco.Text = "toolStrip4";
            // 
            // tsbMaqctPreco_Salvar
            // 
            this.tsbMaqctPreco_Salvar.AccessibleDescription = "Salvar";
            this.tsbMaqctPreco_Salvar.AutoSize = false;
            this.tsbMaqctPreco_Salvar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbMaqctPreco_Salvar.Image = global::CRCorrea.Properties.Resources.Salvar;
            this.tsbMaqctPreco_Salvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMaqctPreco_Salvar.Margin = new System.Windows.Forms.Padding(0);
            this.tsbMaqctPreco_Salvar.Name = "tsbMaqctPreco_Salvar";
            this.tsbMaqctPreco_Salvar.Size = new System.Drawing.Size(52, 30);
            this.tsbMaqctPreco_Salvar.Text = "&Salvar";
            this.tsbMaqctPreco_Salvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbMaqctPreco_Salvar.Click += new System.EventHandler(this.tsbMaqctPreco_Salvar_Click);
            // 
            // tspMaqctPreco_Retornar
            // 
            this.tspMaqctPreco_Retornar.AccessibleDescription = "Retornar";
            this.tspMaqctPreco_Retornar.AutoSize = false;
            this.tspMaqctPreco_Retornar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspMaqctPreco_Retornar.Image = ((System.Drawing.Image)(resources.GetObject("tspMaqctPreco_Retornar.Image")));
            this.tspMaqctPreco_Retornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspMaqctPreco_Retornar.Margin = new System.Windows.Forms.Padding(0);
            this.tspMaqctPreco_Retornar.Name = "tspMaqctPreco_Retornar";
            this.tspMaqctPreco_Retornar.Size = new System.Drawing.Size(52, 30);
            this.tspMaqctPreco_Retornar.Text = "Retorna&r";
            this.tspMaqctPreco_Retornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspMaqctPreco_Retornar.Click += new System.EventHandler(this.tspMaqctPreco_Retornar_Click);
            // 
            // frmMaqCt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.tclMaqctPreco);
            this.Controls.Add(this.tspMaqct);
            this.Controls.Add(this.gbxPrincipal);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMaqCt";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "283";
            this.Text = "Indice Tabela MaqCT - Registrando";
            this.toolTip1.SetToolTip(this, "Indice Tabela MaqCT - Registrando");
            this.Load += new System.EventHandler(this.frmMaqct_Load);
            this.tspMaqct.ResumeLayout(false);
            this.tspMaqct.PerformLayout();
            this.tspMaqctPrecoVis.ResumeLayout(false);
            this.tspMaqctPrecoVis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaqctpreco)).EndInit();
            this.gbxPrincipal.ResumeLayout(false);
            this.gbxPrincipal.PerformLayout();
            this.tclMaqctPreco.ResumeLayout(false);
            this.tpeMaqctPrecoVis.ResumeLayout(false);
            this.tpeMaqctPreco.ResumeLayout(false);
            this.tpeMaqctPreco.PerformLayout();
            this.tspMaqctPreco.ResumeLayout(false);
            this.tspMaqctPreco.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPrincipal;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMaqctpreco;
        private System.Windows.Forms.ToolStrip tspMaqct;
        private System.Windows.Forms.ToolStripButton tsbSalvar;
        private System.Windows.Forms.ToolStripButton tsbPrimeiro;
        private System.Windows.Forms.ToolStripButton tsbAnterior;
        private System.Windows.Forms.ToolStripButton tsbProximo;
        private System.Windows.Forms.ToolStripButton tsbUltimo;
        private System.Windows.Forms.ToolStripButton tsbRetornar;
        private System.Windows.Forms.ToolStrip tspMaqctPrecoVis;
        private System.Windows.Forms.ToolStripButton tsbMaqctPrecoVis_Incluir;
        private System.Windows.Forms.ToolStripButton tsbMaqctPrecoVis_Alterar;
        private System.Windows.Forms.ToolStripButton tsbMaqctPrecoVis_Imprimir;
        private System.Windows.Forms.ToolStripButton tsbMaqctPrecoVis_Excluir;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.Label label7;
        private System.ComponentModel.BackgroundWorker bwrmaqctpreco;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxAtivo;
        private System.Windows.Forms.TabControl tclMaqctPreco;
        private System.Windows.Forms.TabPage tpeMaqctPrecoVis;
        private System.Windows.Forms.TabPage tpeMaqctPreco;
        private System.Windows.Forms.ToolStrip tspMaqctPreco;
        private System.Windows.Forms.ToolStripButton tsbMaqctPreco_Salvar;
        private System.Windows.Forms.ToolStripButton tspMaqctPreco_Retornar;
        private System.Windows.Forms.TextBox tbxMaqctPreco_Data;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxMaqctPreco_Valor;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}