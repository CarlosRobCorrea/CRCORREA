
namespace CRCorrea
{
    partial class frmRelNFCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelNFCompra));
            this.label2 = new System.Windows.Forms.Label();
            this.tspItens = new System.Windows.Forms.ToolStrip();
            this.tspItemImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspItemRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxItemParametros = new System.Windows.Forms.GroupBox();
            this.btnItemGrupoDe = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.tbxItemGrupo = new System.Windows.Forms.TextBox();
            this.btnItemCodAte = new System.Windows.Forms.Button();
            this.tbxItemCodAte = new System.Windows.Forms.TextBox();
            this.btnItemCodDe = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.tbxItemCodDe = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbxItemDtEmissaoAte = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tbxItemDtEmissaoDe = new System.Windows.Forms.TextBox();
            this.btnItemFornecedorDe = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.tbxItemFornecedorDe = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbnApuracoes = new System.Windows.Forms.RadioButton();
            this.rbnDetalhes = new System.Windows.Forms.RadioButton();
            this.rbnCabecalho = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.gbxResAnalSint = new System.Windows.Forms.GroupBox();
            this.rbnResAnalitica = new System.Windows.Forms.RadioButton();
            this.rbnResSintetica = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.gbxResSub = new System.Windows.Forms.GroupBox();
            this.rbnResSemSub = new System.Windows.Forms.RadioButton();
            this.rbnResComSub = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxResOrdem = new System.Windows.Forms.GroupBox();
            this.lbxResOrdem = new System.Windows.Forms.ListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tspItens.SuspendLayout();
            this.gbxItemParametros.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbxResAnalSint.SuspendLayout();
            this.gbxResSub.SuspendLayout();
            this.gbxResOrdem.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(988, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Imprimir Despesas [Notas Fiscais de Entrada]";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tspItens
            // 
            this.tspItens.AccessibleDescription = "Barra de Opções";
            this.tspItens.AutoSize = false;
            this.tspItens.Dock = System.Windows.Forms.DockStyle.None;
            this.tspItens.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspItens.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspItens.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspItemImprimir,
            this.tspItemRetornar});
            this.tspItens.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspItens.Location = new System.Drawing.Point(438, 484);
            this.tspItens.Name = "tspItens";
            this.tspItens.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tspItens.Size = new System.Drawing.Size(169, 44);
            this.tspItens.TabIndex = 5;
            this.tspItens.TabStop = true;
            this.tspItens.Text = "toolStrip1";
            // 
            // tspItemImprimir
            // 
            this.tspItemImprimir.AccessibleDescription = "Imprimir";
            this.tspItemImprimir.AutoSize = false;
            this.tspItemImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspItemImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspItemImprimir.Image")));
            this.tspItemImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspItemImprimir.Name = "tspItemImprimir";
            this.tspItemImprimir.Size = new System.Drawing.Size(58, 42);
            this.tspItemImprimir.Text = "&Imprimir";
            this.tspItemImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspItemImprimir.ToolTipText = "Item - Imprimir";
            this.tspItemImprimir.Click += new System.EventHandler(this.tspItemImprimir_Click);
            // 
            // tspItemRetornar
            // 
            this.tspItemRetornar.AccessibleDescription = "Retornar";
            this.tspItemRetornar.AutoSize = false;
            this.tspItemRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspItemRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspItemRetornar.Image")));
            this.tspItemRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspItemRetornar.Name = "tspItemRetornar";
            this.tspItemRetornar.Size = new System.Drawing.Size(58, 42);
            this.tspItemRetornar.Text = "Retorna&r";
            this.tspItemRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspItemRetornar.ToolTipText = "Item - Retornar";
            this.tspItemRetornar.Click += new System.EventHandler(this.tspItemRetornar_Click);
            // 
            // gbxItemParametros
            // 
            this.gbxItemParametros.AccessibleDescription = "Parametros da Filtragem";
            this.gbxItemParametros.Controls.Add(this.btnItemGrupoDe);
            this.gbxItemParametros.Controls.Add(this.label45);
            this.gbxItemParametros.Controls.Add(this.tbxItemGrupo);
            this.gbxItemParametros.Controls.Add(this.btnItemCodAte);
            this.gbxItemParametros.Controls.Add(this.tbxItemCodAte);
            this.gbxItemParametros.Controls.Add(this.btnItemCodDe);
            this.gbxItemParametros.Controls.Add(this.label38);
            this.gbxItemParametros.Controls.Add(this.label39);
            this.gbxItemParametros.Controls.Add(this.tbxItemCodDe);
            this.gbxItemParametros.Controls.Add(this.label20);
            this.gbxItemParametros.Controls.Add(this.tbxItemDtEmissaoAte);
            this.gbxItemParametros.Controls.Add(this.label23);
            this.gbxItemParametros.Controls.Add(this.tbxItemDtEmissaoDe);
            this.gbxItemParametros.Controls.Add(this.btnItemFornecedorDe);
            this.gbxItemParametros.Controls.Add(this.label25);
            this.gbxItemParametros.Controls.Add(this.tbxItemFornecedorDe);
            this.gbxItemParametros.Location = new System.Drawing.Point(227, 108);
            this.gbxItemParametros.Name = "gbxItemParametros";
            this.gbxItemParametros.Size = new System.Drawing.Size(556, 158);
            this.gbxItemParametros.TabIndex = 1;
            this.gbxItemParametros.TabStop = false;
            this.gbxItemParametros.Text = "Parametros da Filtragem";
            // 
            // btnItemGrupoDe
            // 
            this.btnItemGrupoDe.AccessibleDescription = "Busca Grupo Material";
            this.btnItemGrupoDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemGrupoDe.Image = ((System.Drawing.Image)(resources.GetObject("btnItemGrupoDe.Image")));
            this.btnItemGrupoDe.Location = new System.Drawing.Point(304, 113);
            this.btnItemGrupoDe.Name = "btnItemGrupoDe";
            this.btnItemGrupoDe.Size = new System.Drawing.Size(20, 24);
            this.btnItemGrupoDe.TabIndex = 27;
            this.btnItemGrupoDe.TabStop = false;
            this.btnItemGrupoDe.Text = "...";
            this.btnItemGrupoDe.UseVisualStyleBackColor = true;
            this.btnItemGrupoDe.Click += new System.EventHandler(this.btnItemGrupoDe_Click);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(33, 118);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(100, 13);
            this.label45.TabIndex = 152;
            this.label45.Text = "Grupo Material De :";
            // 
            // tbxItemGrupo
            // 
            this.tbxItemGrupo.AccessibleDescription = "Grupo Material De";
            this.tbxItemGrupo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxItemGrupo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxItemGrupo.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxItemGrupo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxItemGrupo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxItemGrupo.Location = new System.Drawing.Point(149, 115);
            this.tbxItemGrupo.Name = "tbxItemGrupo";
            this.tbxItemGrupo.Size = new System.Drawing.Size(153, 21);
            this.tbxItemGrupo.TabIndex = 16;
            this.tbxItemGrupo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxItemGrupo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxItemGrupo.Leave += new System.EventHandler(this.ControlEnter);
            // 
            // btnItemCodAte
            // 
            this.btnItemCodAte.AccessibleDescription = "Busca Código Material";
            this.btnItemCodAte.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemCodAte.Image = ((System.Drawing.Image)(resources.GetObject("btnItemCodAte.Image")));
            this.btnItemCodAte.Location = new System.Drawing.Point(526, 79);
            this.btnItemCodAte.Name = "btnItemCodAte";
            this.btnItemCodAte.Size = new System.Drawing.Size(19, 24);
            this.btnItemCodAte.TabIndex = 13;
            this.btnItemCodAte.TabStop = false;
            this.btnItemCodAte.Text = "...";
            this.btnItemCodAte.UseVisualStyleBackColor = true;
            this.btnItemCodAte.Click += new System.EventHandler(this.btnItemCodAte_Click);
            // 
            // tbxItemCodAte
            // 
            this.tbxItemCodAte.AccessibleDescription = "Código Material Até";
            this.tbxItemCodAte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxItemCodAte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxItemCodAte.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxItemCodAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxItemCodAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxItemCodAte.Location = new System.Drawing.Point(371, 81);
            this.tbxItemCodAte.Name = "tbxItemCodAte";
            this.tbxItemCodAte.Size = new System.Drawing.Size(153, 21);
            this.tbxItemCodAte.TabIndex = 9;
            this.tbxItemCodAte.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxItemCodAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxItemCodAte.Leave += new System.EventHandler(this.ControlEnter);
            // 
            // btnItemCodDe
            // 
            this.btnItemCodDe.AccessibleDescription = "Busca Código Matrial";
            this.btnItemCodDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemCodDe.Image = ((System.Drawing.Image)(resources.GetObject("btnItemCodDe.Image")));
            this.btnItemCodDe.Location = new System.Drawing.Point(303, 79);
            this.btnItemCodDe.Name = "btnItemCodDe";
            this.btnItemCodDe.Size = new System.Drawing.Size(20, 24);
            this.btnItemCodDe.TabIndex = 11;
            this.btnItemCodDe.TabStop = false;
            this.btnItemCodDe.Text = "...";
            this.btnItemCodDe.UseVisualStyleBackColor = true;
            this.btnItemCodDe.Click += new System.EventHandler(this.btnItemCodDe_Click);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(335, 84);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(31, 13);
            this.label38.TabIndex = 129;
            this.label38.Text = "Até :";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(27, 84);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(104, 13);
            this.label39.TabIndex = 128;
            this.label39.Text = "Codigo Material De :";
            // 
            // tbxItemCodDe
            // 
            this.tbxItemCodDe.AccessibleDescription = "Código Material De";
            this.tbxItemCodDe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxItemCodDe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxItemCodDe.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxItemCodDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxItemCodDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxItemCodDe.Location = new System.Drawing.Point(148, 81);
            this.tbxItemCodDe.Name = "tbxItemCodDe";
            this.tbxItemCodDe.Size = new System.Drawing.Size(153, 21);
            this.tbxItemCodDe.TabIndex = 8;
            this.tbxItemCodDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxItemCodDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxItemCodDe.Leave += new System.EventHandler(this.ControlEnter);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(334, 55);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 117;
            this.label20.Text = "Até :";
            // 
            // tbxItemDtEmissaoAte
            // 
            this.tbxItemDtEmissaoAte.AccessibleDescription = "Data Emissão Até";
            this.tbxItemDtEmissaoAte.BackColor = System.Drawing.SystemColors.Window;
            this.tbxItemDtEmissaoAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxItemDtEmissaoAte.Location = new System.Drawing.Point(370, 52);
            this.tbxItemDtEmissaoAte.Name = "tbxItemDtEmissaoAte";
            this.tbxItemDtEmissaoAte.Size = new System.Drawing.Size(153, 21);
            this.tbxItemDtEmissaoAte.TabIndex = 3;
            this.tbxItemDtEmissaoAte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxItemDtEmissaoAte.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxItemDtEmissaoAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxItemDtEmissaoAte.Leave += new System.EventHandler(this.ControlEnter);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(53, 55);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(82, 13);
            this.label23.TabIndex = 110;
            this.label23.Text = "Dt Emissão De :";
            // 
            // tbxItemDtEmissaoDe
            // 
            this.tbxItemDtEmissaoDe.AccessibleDescription = "Data de Emissão De";
            this.tbxItemDtEmissaoDe.BackColor = System.Drawing.SystemColors.Window;
            this.tbxItemDtEmissaoDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxItemDtEmissaoDe.Location = new System.Drawing.Point(147, 52);
            this.tbxItemDtEmissaoDe.Name = "tbxItemDtEmissaoDe";
            this.tbxItemDtEmissaoDe.Size = new System.Drawing.Size(153, 21);
            this.tbxItemDtEmissaoDe.TabIndex = 2;
            this.tbxItemDtEmissaoDe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxItemDtEmissaoDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxItemDtEmissaoDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxItemDtEmissaoDe.Leave += new System.EventHandler(this.ControlEnter);
            // 
            // btnItemFornecedorDe
            // 
            this.btnItemFornecedorDe.AccessibleDescription = "Busca Cliente/Fornecedor";
            this.btnItemFornecedorDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemFornecedorDe.Image = ((System.Drawing.Image)(resources.GetObject("btnItemFornecedorDe.Image")));
            this.btnItemFornecedorDe.Location = new System.Drawing.Point(302, 24);
            this.btnItemFornecedorDe.Name = "btnItemFornecedorDe";
            this.btnItemFornecedorDe.Size = new System.Drawing.Size(20, 24);
            this.btnItemFornecedorDe.TabIndex = 1;
            this.btnItemFornecedorDe.TabStop = false;
            this.btnItemFornecedorDe.Text = "...";
            this.btnItemFornecedorDe.UseVisualStyleBackColor = true;
            this.btnItemFornecedorDe.Click += new System.EventHandler(this.btnItemFornecedorDe_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 29);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(122, 13);
            this.label25.TabIndex = 47;
            this.label25.Text = "Fornecedor\\Cliente De :";
            // 
            // tbxItemFornecedorDe
            // 
            this.tbxItemFornecedorDe.AccessibleDescription = "Fornecedor\\Cliente De :";
            this.tbxItemFornecedorDe.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.tbxItemFornecedorDe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxItemFornecedorDe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxItemFornecedorDe.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxItemFornecedorDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxItemFornecedorDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxItemFornecedorDe.Location = new System.Drawing.Point(147, 26);
            this.tbxItemFornecedorDe.Name = "tbxItemFornecedorDe";
            this.tbxItemFornecedorDe.Size = new System.Drawing.Size(153, 21);
            this.tbxItemFornecedorDe.TabIndex = 0;
            this.tbxItemFornecedorDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxItemFornecedorDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxItemFornecedorDe.Leave += new System.EventHandler(this.ControlEnter);
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "Filtro";
            this.groupBox2.Controls.Add(this.rbnApuracoes);
            this.groupBox2.Controls.Add(this.rbnDetalhes);
            this.groupBox2.Controls.Add(this.rbnCabecalho);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(193, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(635, 41);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // rbnApuracoes
            // 
            this.rbnApuracoes.AccessibleDescription = "Apenas Apuracoes";
            this.rbnApuracoes.AutoSize = true;
            this.rbnApuracoes.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbnApuracoes.ForeColor = System.Drawing.Color.Navy;
            this.rbnApuracoes.Location = new System.Drawing.Point(427, 15);
            this.rbnApuracoes.Name = "rbnApuracoes";
            this.rbnApuracoes.Size = new System.Drawing.Size(131, 17);
            this.rbnApuracoes.TabIndex = 10;
            this.rbnApuracoes.Text = "Apurações e Resumos";
            this.rbnApuracoes.UseVisualStyleBackColor = true;
            // 
            // rbnDetalhes
            // 
            this.rbnDetalhes.AccessibleDescription = "Detalhes / Itens das Notas";
            this.rbnDetalhes.AutoSize = true;
            this.rbnDetalhes.Checked = true;
            this.rbnDetalhes.ForeColor = System.Drawing.Color.Navy;
            this.rbnDetalhes.Location = new System.Drawing.Point(221, 15);
            this.rbnDetalhes.Name = "rbnDetalhes";
            this.rbnDetalhes.Size = new System.Drawing.Size(168, 17);
            this.rbnDetalhes.TabIndex = 1;
            this.rbnDetalhes.TabStop = true;
            this.rbnDetalhes.Text = "Detalhes / Itens das Entradas";
            this.rbnDetalhes.UseVisualStyleBackColor = true;
            this.rbnDetalhes.CheckedChanged += new System.EventHandler(this.rbnDetalhes_CheckedChanged);
            // 
            // rbnCabecalho
            // 
            this.rbnCabecalho.AccessibleDescription = "Apenas Cabeçalho / Resumo";
            this.rbnCabecalho.AutoSize = true;
            this.rbnCabecalho.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbnCabecalho.ForeColor = System.Drawing.Color.Navy;
            this.rbnCabecalho.Location = new System.Drawing.Point(12, 15);
            this.rbnCabecalho.Name = "rbnCabecalho";
            this.rbnCabecalho.Size = new System.Drawing.Size(162, 17);
            this.rbnCabecalho.TabIndex = 0;
            this.rbnCabecalho.Text = "Apenas Cabeçalho / Resumo";
            this.rbnCabecalho.UseVisualStyleBackColor = true;
            this.rbnCabecalho.CheckedChanged += new System.EventHandler(this.rbnCabecalho_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 9;
            // 
            // gbxResAnalSint
            // 
            this.gbxResAnalSint.AccessibleDescription = "Resultado";
            this.gbxResAnalSint.Controls.Add(this.rbnResAnalitica);
            this.gbxResAnalSint.Controls.Add(this.rbnResSintetica);
            this.gbxResAnalSint.Controls.Add(this.label7);
            this.gbxResAnalSint.Location = new System.Drawing.Point(378, 387);
            this.gbxResAnalSint.Name = "gbxResAnalSint";
            this.gbxResAnalSint.Size = new System.Drawing.Size(111, 67);
            this.gbxResAnalSint.TabIndex = 3;
            this.gbxResAnalSint.TabStop = false;
            // 
            // rbnResAnalitica
            // 
            this.rbnResAnalitica.AccessibleDescription = "Analítica";
            this.rbnResAnalitica.AutoSize = true;
            this.rbnResAnalitica.Checked = true;
            this.rbnResAnalitica.Location = new System.Drawing.Point(9, 20);
            this.rbnResAnalitica.Name = "rbnResAnalitica";
            this.rbnResAnalitica.Size = new System.Drawing.Size(65, 17);
            this.rbnResAnalitica.TabIndex = 0;
            this.rbnResAnalitica.TabStop = true;
            this.rbnResAnalitica.Text = "Analítica";
            this.rbnResAnalitica.UseVisualStyleBackColor = true;
            // 
            // rbnResSintetica
            // 
            this.rbnResSintetica.AccessibleDescription = "Sintética";
            this.rbnResSintetica.AutoSize = true;
            this.rbnResSintetica.Location = new System.Drawing.Point(9, 43);
            this.rbnResSintetica.Name = "rbnResSintetica";
            this.rbnResSintetica.Size = new System.Drawing.Size(66, 17);
            this.rbnResSintetica.TabIndex = 1;
            this.rbnResSintetica.Text = "Sintética";
            this.rbnResSintetica.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 9;
            // 
            // gbxResSub
            // 
            this.gbxResSub.AccessibleDescription = "Resultado Sub Total";
            this.gbxResSub.Controls.Add(this.rbnResSemSub);
            this.gbxResSub.Controls.Add(this.rbnResComSub);
            this.gbxResSub.Controls.Add(this.label3);
            this.gbxResSub.Location = new System.Drawing.Point(510, 387);
            this.gbxResSub.Name = "gbxResSub";
            this.gbxResSub.Size = new System.Drawing.Size(139, 67);
            this.gbxResSub.TabIndex = 4;
            this.gbxResSub.TabStop = false;
            // 
            // rbnResSemSub
            // 
            this.rbnResSemSub.AccessibleDescription = "Sem Sub-Total";
            this.rbnResSemSub.AutoSize = true;
            this.rbnResSemSub.Checked = true;
            this.rbnResSemSub.Location = new System.Drawing.Point(18, 19);
            this.rbnResSemSub.Name = "rbnResSemSub";
            this.rbnResSemSub.Size = new System.Drawing.Size(94, 17);
            this.rbnResSemSub.TabIndex = 0;
            this.rbnResSemSub.TabStop = true;
            this.rbnResSemSub.Text = "Sem Sub-Total";
            this.rbnResSemSub.UseVisualStyleBackColor = true;
            // 
            // rbnResComSub
            // 
            this.rbnResComSub.AccessibleDescription = "Com Sub-Total";
            this.rbnResComSub.AutoSize = true;
            this.rbnResComSub.Location = new System.Drawing.Point(18, 42);
            this.rbnResComSub.Name = "rbnResComSub";
            this.rbnResComSub.Size = new System.Drawing.Size(95, 17);
            this.rbnResComSub.TabIndex = 1;
            this.rbnResComSub.Text = "Com Sub-Total";
            this.rbnResComSub.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 9;
            // 
            // gbxResOrdem
            // 
            this.gbxResOrdem.Controls.Add(this.lbxResOrdem);
            this.gbxResOrdem.Controls.Add(this.label15);
            this.gbxResOrdem.Location = new System.Drawing.Point(230, 268);
            this.gbxResOrdem.Name = "gbxResOrdem";
            this.gbxResOrdem.Size = new System.Drawing.Size(556, 107);
            this.gbxResOrdem.TabIndex = 2;
            this.gbxResOrdem.TabStop = false;
            this.gbxResOrdem.Text = "A lista deverá sair na seguinte Ordem :";
            // 
            // lbxResOrdem
            // 
            this.lbxResOrdem.FormattingEnabled = true;
            this.lbxResOrdem.Items.AddRange(new object[] {
            "Data + Numero da Nota + Codigo Item",
            "Por Codigo Item + Data + Numero da Nota",
            "Por Fornecedor + Data + Codigo Item",
            "Por Nome do Produto"});
            this.lbxResOrdem.Location = new System.Drawing.Point(11, 16);
            this.lbxResOrdem.Name = "lbxResOrdem";
            this.lbxResOrdem.Size = new System.Drawing.Size(532, 82);
            this.lbxResOrdem.TabIndex = 0;
            this.lbxResOrdem.Enter += new System.EventHandler(this.ControlEnter);
            this.lbxResOrdem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 13);
            this.label15.TabIndex = 9;
            // 
            // frmRelNFCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxResAnalSint);
            this.Controls.Add(this.gbxResSub);
            this.Controls.Add(this.gbxResOrdem);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbxItemParametros);
            this.Controls.Add(this.tspItens);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRelNFCompra";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio de Entradas";
            this.Activated += new System.EventHandler(this.frmRelNFCompra_Activated);
            this.Load += new System.EventHandler(this.frmRelNFCompra_Load);
            this.tspItens.ResumeLayout(false);
            this.tspItens.PerformLayout();
            this.gbxItemParametros.ResumeLayout(false);
            this.gbxItemParametros.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbxResAnalSint.ResumeLayout(false);
            this.gbxResAnalSint.PerformLayout();
            this.gbxResSub.ResumeLayout(false);
            this.gbxResSub.PerformLayout();
            this.gbxResOrdem.ResumeLayout(false);
            this.gbxResOrdem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip tspItens;
        private System.Windows.Forms.ToolStripButton tspItemImprimir;
        private System.Windows.Forms.ToolStripButton tspItemRetornar;
        private System.Windows.Forms.GroupBox gbxItemParametros;
        private System.Windows.Forms.Button btnItemGrupoDe;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox tbxItemGrupo;
        private System.Windows.Forms.Button btnItemCodAte;
        private System.Windows.Forms.TextBox tbxItemCodAte;
        private System.Windows.Forms.Button btnItemCodDe;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox tbxItemCodDe;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbxItemDtEmissaoAte;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbxItemDtEmissaoDe;
        private System.Windows.Forms.Button btnItemFornecedorDe;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbxItemFornecedorDe;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbnApuracoes;
        private System.Windows.Forms.RadioButton rbnDetalhes;
        private System.Windows.Forms.RadioButton rbnCabecalho;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbxResAnalSint;
        private System.Windows.Forms.RadioButton rbnResAnalitica;
        private System.Windows.Forms.RadioButton rbnResSintetica;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbxResSub;
        private System.Windows.Forms.RadioButton rbnResSemSub;
        private System.Windows.Forms.RadioButton rbnResComSub;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbxResOrdem;
        private System.Windows.Forms.ListBox lbxResOrdem;
        private System.Windows.Forms.Label label15;
    }
}