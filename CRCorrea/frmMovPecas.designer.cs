namespace CRCorrea
{
    partial class frmMovPecas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMovPecas));
            this.ttpMovPecas = new System.Windows.Forms.ToolTip(this.components);
            this.gbxCadastro = new System.Windows.Forms.GroupBox();
            this.btnPecas = new System.Windows.Forms.Button();
            this.tbxUnidVenda = new System.Windows.Forms.TextBox();
            this.tbxUnidCompra = new System.Windows.Forms.TextBox();
            this.tbxPecasTipoMaterial = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.tbxPecasCodigo = new System.Windows.Forms.TextBox();
            this.tbxPecasDescricaoPeca = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxLocalArmazenagem = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxOpcoes = new System.Windows.Forms.GroupBox();
            this.rbnRevisao = new System.Windows.Forms.RadioButton();
            this.rbnInativo = new System.Windows.Forms.RadioButton();
            this.rbnAtivo = new System.Windows.Forms.RadioButton();
            this.dgvMovPecas = new System.Windows.Forms.DataGridView();
            this.gbxResumoMovimento = new System.Windows.Forms.GroupBox();
            this.dgvResumoMovimento = new System.Windows.Forms.DataGridView();
            this.dgvPecasUltimasCompras = new System.Windows.Forms.DataGridView();
            this.gbxMovPecasPeriodo = new System.Windows.Forms.GroupBox();
            this.tbxDataAte = new System.Windows.Forms.TextBox();
            this.tbxDataDe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.gbxMovPecas = new System.Windows.Forms.GroupBox();
            this.gbxPecasUltimasCompras = new System.Windows.Forms.GroupBox();
            this.tspMovPecas = new System.Windows.Forms.ToolStrip();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxCadastro.SuspendLayout();
            this.gbxOpcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovPecas)).BeginInit();
            this.gbxResumoMovimento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumoMovimento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPecasUltimasCompras)).BeginInit();
            this.gbxMovPecasPeriodo.SuspendLayout();
            this.gbxMovPecas.SuspendLayout();
            this.gbxPecasUltimasCompras.SuspendLayout();
            this.tspMovPecas.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxCadastro
            // 
            this.gbxCadastro.AccessibleDescription = "Cadastro";
            this.gbxCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxCadastro.Controls.Add(this.btnPecas);
            this.gbxCadastro.Controls.Add(this.tbxUnidVenda);
            this.gbxCadastro.Controls.Add(this.tbxUnidCompra);
            this.gbxCadastro.Controls.Add(this.tbxPecasTipoMaterial);
            this.gbxCadastro.Controls.Add(this.label75);
            this.gbxCadastro.Controls.Add(this.tbxPecasCodigo);
            this.gbxCadastro.Controls.Add(this.tbxPecasDescricaoPeca);
            this.gbxCadastro.Controls.Add(this.label1);
            this.gbxCadastro.Controls.Add(this.label9);
            this.gbxCadastro.Controls.Add(this.label7);
            this.gbxCadastro.Controls.Add(this.tbxLocalArmazenagem);
            this.gbxCadastro.Controls.Add(this.label6);
            this.gbxCadastro.Controls.Add(this.label3);
            this.gbxCadastro.Controls.Add(this.gbxOpcoes);
            this.gbxCadastro.Location = new System.Drawing.Point(12, 3);
            this.gbxCadastro.Name = "gbxCadastro";
            this.gbxCadastro.Size = new System.Drawing.Size(792, 100);
            this.gbxCadastro.TabIndex = 0;
            this.gbxCadastro.TabStop = false;
            this.ttpMovPecas.SetToolTip(this.gbxCadastro, "Grupo de Cabeçario");
            // 
            // btnPecas
            // 
            this.btnPecas.AccessibleDescription = "Busca Produto";
            this.btnPecas.Image = ((System.Drawing.Image)(resources.GetObject("btnPecas.Image")));
            this.btnPecas.Location = new System.Drawing.Point(213, 33);
            this.btnPecas.Name = "btnPecas";
            this.btnPecas.Size = new System.Drawing.Size(23, 21);
            this.btnPecas.TabIndex = 1;
            this.btnPecas.TabStop = false;
            this.btnPecas.UseVisualStyleBackColor = true;
            // 
            // tbxUnidVenda
            // 
            this.tbxUnidVenda.AccessibleDescription = "Un. Ctle/Venda:";
            this.tbxUnidVenda.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxUnidVenda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxUnidVenda.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUnidVenda.Location = new System.Drawing.Point(494, 73);
            this.tbxUnidVenda.Name = "tbxUnidVenda";
            this.tbxUnidVenda.ReadOnly = true;
            this.tbxUnidVenda.Size = new System.Drawing.Size(35, 21);
            this.tbxUnidVenda.TabIndex = 6;
            this.tbxUnidVenda.TabStop = false;
            this.tbxUnidVenda.Tag = "";
            this.ttpMovPecas.SetToolTip(this.tbxUnidVenda, " ");
            // 
            // tbxUnidCompra
            // 
            this.tbxUnidCompra.AccessibleDescription = "Unidade Compra";
            this.tbxUnidCompra.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxUnidCompra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxUnidCompra.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUnidCompra.Location = new System.Drawing.Point(404, 73);
            this.tbxUnidCompra.Name = "tbxUnidCompra";
            this.tbxUnidCompra.ReadOnly = true;
            this.tbxUnidCompra.Size = new System.Drawing.Size(35, 21);
            this.tbxUnidCompra.TabIndex = 5;
            this.tbxUnidCompra.TabStop = false;
            this.tbxUnidCompra.Tag = "";
            this.ttpMovPecas.SetToolTip(this.tbxUnidCompra, " ");
            // 
            // tbxPecasTipoMaterial
            // 
            this.tbxPecasTipoMaterial.AccessibleDescription = "Tipo Material";
            this.tbxPecasTipoMaterial.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxPecasTipoMaterial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPecasTipoMaterial.Location = new System.Drawing.Point(133, 73);
            this.tbxPecasTipoMaterial.Name = "tbxPecasTipoMaterial";
            this.tbxPecasTipoMaterial.ReadOnly = true;
            this.tbxPecasTipoMaterial.Size = new System.Drawing.Size(233, 21);
            this.tbxPecasTipoMaterial.TabIndex = 4;
            this.tbxPecasTipoMaterial.TabStop = false;
            this.ttpMovPecas.SetToolTip(this.tbxPecasTipoMaterial, " ");
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(130, 57);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(72, 13);
            this.label75.TabIndex = 101;
            this.label75.Text = "Tipo Material:";
            // 
            // tbxPecasCodigo
            // 
            this.tbxPecasCodigo.AccessibleDescription = "Código Item";
            this.tbxPecasCodigo.BackColor = System.Drawing.Color.LightGray;
            this.tbxPecasCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPecasCodigo.Location = new System.Drawing.Point(6, 33);
            this.tbxPecasCodigo.Name = "tbxPecasCodigo";
            this.tbxPecasCodigo.Size = new System.Drawing.Size(201, 21);
            this.tbxPecasCodigo.TabIndex = 0;
            this.ttpMovPecas.SetToolTip(this.tbxPecasCodigo, " ");
            // 
            // tbxPecasDescricaoPeca
            // 
            this.tbxPecasDescricaoPeca.AccessibleDescription = "Descrição do Material";
            this.tbxPecasDescricaoPeca.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxPecasDescricaoPeca.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPecasDescricaoPeca.Location = new System.Drawing.Point(242, 33);
            this.tbxPecasDescricaoPeca.Name = "tbxPecasDescricaoPeca";
            this.tbxPecasDescricaoPeca.ReadOnly = true;
            this.tbxPecasDescricaoPeca.Size = new System.Drawing.Size(467, 21);
            this.tbxPecasDescricaoPeca.TabIndex = 2;
            this.tbxPecasDescricaoPeca.TabStop = false;
            this.ttpMovPecas.SetToolTip(this.tbxPecasDescricaoPeca, " ");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "Código Item:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(238, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 13);
            this.label9.TabIndex = 99;
            this.label9.Text = "Descrição do Material:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Local de Armazenagem:";
            // 
            // tbxLocalArmazenagem
            // 
            this.tbxLocalArmazenagem.AccessibleDescription = "Local de Armaznagem";
            this.tbxLocalArmazenagem.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxLocalArmazenagem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLocalArmazenagem.Location = new System.Drawing.Point(6, 73);
            this.tbxLocalArmazenagem.Name = "tbxLocalArmazenagem";
            this.tbxLocalArmazenagem.ReadOnly = true;
            this.tbxLocalArmazenagem.Size = new System.Drawing.Size(121, 21);
            this.tbxLocalArmazenagem.TabIndex = 3;
            this.tbxLocalArmazenagem.TabStop = false;
            this.ttpMovPecas.SetToolTip(this.tbxLocalArmazenagem, " ");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(445, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Un. Ctle/Venda:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Un. Compra:";
            // 
            // gbxOpcoes
            // 
            this.gbxOpcoes.AccessibleDescription = "Opções";
            this.gbxOpcoes.Controls.Add(this.rbnRevisao);
            this.gbxOpcoes.Controls.Add(this.rbnInativo);
            this.gbxOpcoes.Controls.Add(this.rbnAtivo);
            this.gbxOpcoes.Location = new System.Drawing.Point(715, 13);
            this.gbxOpcoes.Name = "gbxOpcoes";
            this.gbxOpcoes.Size = new System.Drawing.Size(71, 81);
            this.gbxOpcoes.TabIndex = 7;
            this.gbxOpcoes.TabStop = false;
            this.ttpMovPecas.SetToolTip(this.gbxOpcoes, "Opções Ativo ou Inativo");
            // 
            // rbnRevisao
            // 
            this.rbnRevisao.AccessibleDescription = "Revisão";
            this.rbnRevisao.AutoSize = true;
            this.rbnRevisao.Enabled = false;
            this.rbnRevisao.Location = new System.Drawing.Point(6, 56);
            this.rbnRevisao.Name = "rbnRevisao";
            this.rbnRevisao.Size = new System.Drawing.Size(63, 17);
            this.rbnRevisao.TabIndex = 3;
            this.rbnRevisao.Text = "Revisão";
            this.ttpMovPecas.SetToolTip(this.rbnRevisao, "Peça em Revisão");
            this.rbnRevisao.UseVisualStyleBackColor = true;
            // 
            // rbnInativo
            // 
            this.rbnInativo.AccessibleDescription = "Inativo";
            this.rbnInativo.AutoSize = true;
            this.rbnInativo.Enabled = false;
            this.rbnInativo.Location = new System.Drawing.Point(6, 34);
            this.rbnInativo.Name = "rbnInativo";
            this.rbnInativo.Size = new System.Drawing.Size(59, 17);
            this.rbnInativo.TabIndex = 2;
            this.rbnInativo.Text = "Inativa";
            this.ttpMovPecas.SetToolTip(this.rbnInativo, "Peça Inativa");
            this.rbnInativo.UseVisualStyleBackColor = true;
            // 
            // rbnAtivo
            // 
            this.rbnAtivo.AccessibleDescription = "Ativo";
            this.rbnAtivo.Checked = true;
            this.rbnAtivo.Enabled = false;
            this.rbnAtivo.Location = new System.Drawing.Point(6, 12);
            this.rbnAtivo.Name = "rbnAtivo";
            this.rbnAtivo.Size = new System.Drawing.Size(50, 20);
            this.rbnAtivo.TabIndex = 5;
            this.rbnAtivo.TabStop = true;
            this.rbnAtivo.Text = "Ativa";
            this.ttpMovPecas.SetToolTip(this.rbnAtivo, "Peça Ativa");
            this.rbnAtivo.UseVisualStyleBackColor = true;
            // 
            // dgvMovPecas
            // 
            this.dgvMovPecas.AccessibleDescription = "Movimentação das Peças";
            this.dgvMovPecas.AllowUserToAddRows = false;
            this.dgvMovPecas.AllowUserToDeleteRows = false;
            this.dgvMovPecas.AllowUserToResizeRows = false;
            this.dgvMovPecas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMovPecas.BackgroundColor = System.Drawing.Color.White;
            this.dgvMovPecas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovPecas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvMovPecas.Location = new System.Drawing.Point(7, 0);
            this.dgvMovPecas.MultiSelect = false;
            this.dgvMovPecas.Name = "dgvMovPecas";
            this.dgvMovPecas.ReadOnly = true;
            this.dgvMovPecas.RowHeadersVisible = false;
            this.dgvMovPecas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovPecas.Size = new System.Drawing.Size(982, 338);
            this.dgvMovPecas.TabIndex = 125;
            this.dgvMovPecas.TabStop = false;
            this.ttpMovPecas.SetToolTip(this.dgvMovPecas, "Grade -  Movimentação das Peças");
            // 
            // gbxResumoMovimento
            // 
            this.gbxResumoMovimento.AccessibleDescription = "Resumo dos Últimos 12 Meses";
            this.gbxResumoMovimento.Controls.Add(this.dgvResumoMovimento);
            this.gbxResumoMovimento.Location = new System.Drawing.Point(12, 449);
            this.gbxResumoMovimento.Name = "gbxResumoMovimento";
            this.gbxResumoMovimento.Size = new System.Drawing.Size(498, 156);
            this.gbxResumoMovimento.TabIndex = 82;
            this.gbxResumoMovimento.TabStop = false;
            this.gbxResumoMovimento.Text = "Resumo dos Últimos 12 Meses";
            this.ttpMovPecas.SetToolTip(this.gbxResumoMovimento, "Grupo de Visualizar o Resumo do Estoque dos Ultimos 12 Meses");
            // 
            // dgvResumoMovimento
            // 
            this.dgvResumoMovimento.AccessibleDescription = "Grid Ultimas Compras";
            this.dgvResumoMovimento.AllowUserToAddRows = false;
            this.dgvResumoMovimento.AllowUserToDeleteRows = false;
            this.dgvResumoMovimento.AllowUserToResizeRows = false;
            this.dgvResumoMovimento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResumoMovimento.BackgroundColor = System.Drawing.Color.White;
            this.dgvResumoMovimento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumoMovimento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvResumoMovimento.Location = new System.Drawing.Point(7, 14);
            this.dgvResumoMovimento.MultiSelect = false;
            this.dgvResumoMovimento.Name = "dgvResumoMovimento";
            this.dgvResumoMovimento.ReadOnly = true;
            this.dgvResumoMovimento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResumoMovimento.Size = new System.Drawing.Size(486, 130);
            this.dgvResumoMovimento.TabIndex = 125;
            this.dgvResumoMovimento.TabStop = false;
            this.ttpMovPecas.SetToolTip(this.dgvResumoMovimento, "Visualizar o Resumo do Estoque dos Ultimos 12 Meses");
            // 
            // dgvPecasUltimasCompras
            // 
            this.dgvPecasUltimasCompras.AccessibleDescription = "Grid Ultimas Compras";
            this.dgvPecasUltimasCompras.AllowUserToAddRows = false;
            this.dgvPecasUltimasCompras.AllowUserToDeleteRows = false;
            this.dgvPecasUltimasCompras.AllowUserToResizeRows = false;
            this.dgvPecasUltimasCompras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPecasUltimasCompras.BackgroundColor = System.Drawing.Color.White;
            this.dgvPecasUltimasCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPecasUltimasCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvPecasUltimasCompras.Location = new System.Drawing.Point(6, 14);
            this.dgvPecasUltimasCompras.MultiSelect = false;
            this.dgvPecasUltimasCompras.Name = "dgvPecasUltimasCompras";
            this.dgvPecasUltimasCompras.ReadOnly = true;
            this.dgvPecasUltimasCompras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPecasUltimasCompras.Size = new System.Drawing.Size(482, 131);
            this.dgvPecasUltimasCompras.TabIndex = 125;
            this.dgvPecasUltimasCompras.TabStop = false;
            this.ttpMovPecas.SetToolTip(this.dgvPecasUltimasCompras, "Visualizar Ultimas Compras Entradas");
            // 
            // gbxMovPecasPeriodo
            // 
            this.gbxMovPecasPeriodo.AccessibleDescription = "Periodo para Visualizar";
            this.gbxMovPecasPeriodo.Controls.Add(this.tbxDataAte);
            this.gbxMovPecasPeriodo.Controls.Add(this.tbxDataDe);
            this.gbxMovPecasPeriodo.Controls.Add(this.label4);
            this.gbxMovPecasPeriodo.Controls.Add(this.label8);
            this.gbxMovPecasPeriodo.Location = new System.Drawing.Point(810, 10);
            this.gbxMovPecasPeriodo.Name = "gbxMovPecasPeriodo";
            this.gbxMovPecasPeriodo.Size = new System.Drawing.Size(197, 89);
            this.gbxMovPecasPeriodo.TabIndex = 11;
            this.gbxMovPecasPeriodo.TabStop = false;
            this.gbxMovPecasPeriodo.Text = "Período para Visualizar";
            this.ttpMovPecas.SetToolTip(this.gbxMovPecasPeriodo, "Periodo para Visualizar");
            // 
            // tbxDataAte
            // 
            this.tbxDataAte.AccessibleDescription = "Ate a Data de:";
            this.tbxDataAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataAte.Location = new System.Drawing.Point(102, 58);
            this.tbxDataAte.Name = "tbxDataAte";
            this.tbxDataAte.Size = new System.Drawing.Size(77, 21);
            this.tbxDataAte.TabIndex = 1;
            this.tbxDataAte.Text = "01/01/2009";
            this.ttpMovPecas.SetToolTip(this.tbxDataAte, "Da data ate");
            this.tbxDataAte.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataAte.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxDataDe
            // 
            this.tbxDataDe.AccessibleDescription = "Da Data de:";
            this.tbxDataDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataDe.Location = new System.Drawing.Point(102, 31);
            this.tbxDataDe.Name = "tbxDataDe";
            this.tbxDataDe.Size = new System.Drawing.Size(77, 21);
            this.tbxDataDe.TabIndex = 0;
            this.tbxDataDe.Text = "01/01/2009";
            this.ttpMovPecas.SetToolTip(this.tbxDataDe, "Da data de");
            this.tbxDataDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataDe.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Até a Data de:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Da Data de:";
            // 
            // gbxMovPecas
            // 
            this.gbxMovPecas.AccessibleDescription = "Últimas Movimentações";
            this.gbxMovPecas.Controls.Add(this.dgvMovPecas);
            this.gbxMovPecas.Location = new System.Drawing.Point(12, 109);
            this.gbxMovPecas.Name = "gbxMovPecas";
            this.gbxMovPecas.Size = new System.Drawing.Size(995, 364);
            this.gbxMovPecas.TabIndex = 81;
            this.gbxMovPecas.TabStop = false;
            this.gbxMovPecas.Text = "Últimas Movimentações";
            this.ttpMovPecas.SetToolTip(this.gbxMovPecas, "Últimas Compras/Entradas");
            // 
            // gbxPecasUltimasCompras
            // 
            this.gbxPecasUltimasCompras.AccessibleDescription = "Últimas Compras/Entradas";
            this.gbxPecasUltimasCompras.Controls.Add(this.dgvPecasUltimasCompras);
            this.gbxPecasUltimasCompras.Location = new System.Drawing.Point(512, 449);
            this.gbxPecasUltimasCompras.Name = "gbxPecasUltimasCompras";
            this.gbxPecasUltimasCompras.Size = new System.Drawing.Size(494, 157);
            this.gbxPecasUltimasCompras.TabIndex = 83;
            this.gbxPecasUltimasCompras.TabStop = false;
            this.gbxPecasUltimasCompras.Text = "Últimas Compras/Entradas";
            this.ttpMovPecas.SetToolTip(this.gbxPecasUltimasCompras, "Grupo Últimas Compras/Entradas");
            // 
            // tspMovPecas
            // 
            this.tspMovPecas.AccessibleDescription = "Barra de Opções";
            this.tspMovPecas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspMovPecas.AutoSize = false;
            this.tspMovPecas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspMovPecas.Dock = System.Windows.Forms.DockStyle.None;
            this.tspMovPecas.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspMovPecas.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspMovPecas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspImprimir,
            this.tspRetornar});
            this.tspMovPecas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspMovPecas.Location = new System.Drawing.Point(12, 609);
            this.tspMovPecas.Name = "tspMovPecas";
            this.tspMovPecas.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspMovPecas.Size = new System.Drawing.Size(989, 46);
            this.tspMovPecas.TabIndex = 85;
            // 
            // tspPrimeiro
            // 
            this.tspPrimeiro.AccessibleDescription = "Primeiro";
            this.tspPrimeiro.AutoSize = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = ((System.Drawing.Image)(resources.GetObject("tspPrimeiro.Image")));
            this.tspPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Size = new System.Drawing.Size(66, 42);
            this.tspPrimeiro.Text = "Pr&imeiro";
            this.tspPrimeiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspPrimeiro.Click += new System.EventHandler(this.tspPrimeiro_Click);
            // 
            // tspAnterior
            // 
            this.tspAnterior.AccessibleDescription = "Anterior";
            this.tspAnterior.AutoSize = false;
            this.tspAnterior.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAnterior.Image = ((System.Drawing.Image)(resources.GetObject("tspAnterior.Image")));
            this.tspAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAnterior.Name = "tspAnterior";
            this.tspAnterior.Size = new System.Drawing.Size(66, 42);
            this.tspAnterior.Text = "&Anterior";
            this.tspAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAnterior.Click += new System.EventHandler(this.tspAnterior_Click);
            // 
            // tspProximo
            // 
            this.tspProximo.AccessibleDescription = "Próximo";
            this.tspProximo.AutoSize = false;
            this.tspProximo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspProximo.Image = ((System.Drawing.Image)(resources.GetObject("tspProximo.Image")));
            this.tspProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspProximo.Name = "tspProximo";
            this.tspProximo.Size = new System.Drawing.Size(66, 42);
            this.tspProximo.Text = "Pró&ximo";
            this.tspProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspProximo.Click += new System.EventHandler(this.tspProximo_Click);
            // 
            // tspUltimo
            // 
            this.tspUltimo.AccessibleDescription = "Último";
            this.tspUltimo.AutoSize = false;
            this.tspUltimo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUltimo.Image = ((System.Drawing.Image)(resources.GetObject("tspUltimo.Image")));
            this.tspUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Size = new System.Drawing.Size(66, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspUltimo.Click += new System.EventHandler(this.tspUltimo_Click);
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
            this.tspImprimir.Text = "&Imprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
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
            this.tspRetornar.Text = "&Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // frmMovPecas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxCadastro);
            this.Controls.Add(this.tspMovPecas);
            this.Controls.Add(this.gbxPecasUltimasCompras);
            this.Controls.Add(this.gbxResumoMovimento);
            this.Controls.Add(this.gbxMovPecas);
            this.Controls.Add(this.gbxMovPecasPeriodo);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMovPecas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "34187";
            this.Text = "Movimentação de Estoque de Material - Visualizando";
            this.ttpMovPecas.SetToolTip(this, "Movimentação de Estoque de Material - Visualizando");
            this.Activated += new System.EventHandler(this.frmMovPecas_Activated);
            this.Load += new System.EventHandler(this.frmMovPecas_Load);
            this.gbxCadastro.ResumeLayout(false);
            this.gbxCadastro.PerformLayout();
            this.gbxOpcoes.ResumeLayout(false);
            this.gbxOpcoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovPecas)).EndInit();
            this.gbxResumoMovimento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumoMovimento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPecasUltimasCompras)).EndInit();
            this.gbxMovPecasPeriodo.ResumeLayout(false);
            this.gbxMovPecasPeriodo.PerformLayout();
            this.gbxMovPecas.ResumeLayout(false);
            this.gbxPecasUltimasCompras.ResumeLayout(false);
            this.tspMovPecas.ResumeLayout(false);
            this.tspMovPecas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpMovPecas;
        private System.Windows.Forms.GroupBox gbxCadastro;
        private System.Windows.Forms.GroupBox gbxOpcoes;
        private System.Windows.Forms.RadioButton rbnRevisao;
        private System.Windows.Forms.RadioButton rbnInativo;
        private System.Windows.Forms.RadioButton rbnAtivo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbxMovPecasPeriodo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxLocalArmazenagem;
        private System.Windows.Forms.GroupBox gbxMovPecas;
        private System.Windows.Forms.DataGridView dgvMovPecas;
        private System.Windows.Forms.GroupBox gbxResumoMovimento;
        private System.Windows.Forms.DataGridView dgvResumoMovimento;
        private System.Windows.Forms.GroupBox gbxPecasUltimasCompras;
        private System.Windows.Forms.DataGridView dgvPecasUltimasCompras;
        private System.Windows.Forms.ToolStrip tspMovPecas;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.TextBox tbxPecasCodigo;
        private System.Windows.Forms.TextBox tbxPecasDescricaoPeca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxPecasTipoMaterial;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox tbxUnidVenda;
        private System.Windows.Forms.TextBox tbxUnidCompra;
        private System.Windows.Forms.TextBox tbxDataAte;
        private System.Windows.Forms.TextBox tbxDataDe;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.Button btnPecas;
    }
}