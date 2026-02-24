namespace CRCorrea
{
    partial class frmRelCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelCliente));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabClientesRes = new System.Windows.Forms.TabControl();
            this.tabCabecalho = new System.Windows.Forms.TabPage();
            this.gbxResTipos = new System.Windows.Forms.GroupBox();
            this.rbnTransportadorasRes = new System.Windows.Forms.RadioButton();
            this.rbnClienteRes = new System.Windows.Forms.RadioButton();
            this.rbnFornecedorRes = new System.Windows.Forms.RadioButton();
            this.rbnTodosRes = new System.Windows.Forms.RadioButton();
            this.tspResumo = new System.Windows.Forms.ToolStrip();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxResOrdem = new System.Windows.Forms.GroupBox();
            this.lbxResOrdem = new System.Windows.Forms.ListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.gbxResParametros = new System.Windows.Forms.GroupBox();
            this.btnResZonaAte = new System.Windows.Forms.Button();
            this.btnResZonaDe = new System.Windows.Forms.Button();
            this.btnResUfAte = new System.Windows.Forms.Button();
            this.btnResUfDe = new System.Windows.Forms.Button();
            this.btnResCidadeAte = new System.Windows.Forms.Button();
            this.btnResCidadeDe = new System.Windows.Forms.Button();
            this.btnResRepresentanteAte = new System.Windows.Forms.Button();
            this.btnResRamoAte = new System.Windows.Forms.Button();
            this.btnResRepresentanteDe = new System.Windows.Forms.Button();
            this.btnResRamoDe = new System.Windows.Forms.Button();
            this.tbxResVendedorAte = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxResVendedorDe = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbxResRamoAte = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxResRamoDe = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxResZonaAte = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxResZonaDe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxResEstadoAte = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxResEstadoDe = new System.Windows.Forms.TextBox();
            this.tbxResCidadeAte = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxResCidadeDe = new System.Windows.Forms.TextBox();
            this.tbxResCepAte = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxResCepDe = new System.Windows.Forms.TextBox();
            this.btnResFornecedorAte = new System.Windows.Forms.Button();
            this.tbxResFornecedorAte = new System.Windows.Forms.TextBox();
            this.bntResFornecedorDe = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.tbxResFornecedorDe = new System.Windows.Forms.TextBox();
            this.gbxResTiposdeListas = new System.Windows.Forms.GroupBox();
            this.rbnResSimples01 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bwrCarregaAutoComplete = new System.ComponentModel.BackgroundWorker();
            this.rbnItemMp = new System.Windows.Forms.RadioButton();
            this.rbnItemContato = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            this.tabClientesRes.SuspendLayout();
            this.tabCabecalho.SuspendLayout();
            this.gbxResTipos.SuspendLayout();
            this.tspResumo.SuspendLayout();
            this.gbxResOrdem.SuspendLayout();
            this.gbxResParametros.SuspendLayout();
            this.gbxResTiposdeListas.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabClientesRes);
            this.groupBox3.Location = new System.Drawing.Point(7, 41);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1004, 632);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox3, "Grupo - Visualizar Cotação");
            // 
            // tabClientesRes
            // 
            this.tabClientesRes.AccessibleDescription = "Filtros";
            this.tabClientesRes.Controls.Add(this.tabCabecalho);
            this.tabClientesRes.Location = new System.Drawing.Point(10, 20);
            this.tabClientesRes.Name = "tabClientesRes";
            this.tabClientesRes.SelectedIndex = 0;
            this.tabClientesRes.Size = new System.Drawing.Size(983, 602);
            this.tabClientesRes.TabIndex = 0;
            this.tabClientesRes.Tag = "Filtros";
            this.toolTip1.SetToolTip(this.tabClientesRes, "Filtros");
            // 
            // tabCabecalho
            // 
            this.tabCabecalho.Controls.Add(this.gbxResTipos);
            this.tabCabecalho.Controls.Add(this.tspResumo);
            this.tabCabecalho.Controls.Add(this.gbxResOrdem);
            this.tabCabecalho.Controls.Add(this.gbxResParametros);
            this.tabCabecalho.Controls.Add(this.gbxResTiposdeListas);
            this.tabCabecalho.Location = new System.Drawing.Point(4, 22);
            this.tabCabecalho.Name = "tabCabecalho";
            this.tabCabecalho.Padding = new System.Windows.Forms.Padding(3);
            this.tabCabecalho.Size = new System.Drawing.Size(975, 576);
            this.tabCabecalho.TabIndex = 0;
            this.tabCabecalho.Text = ".";
            this.toolTip1.SetToolTip(this.tabCabecalho, "Cabeçalho / Resumo");
            this.tabCabecalho.UseVisualStyleBackColor = true;
            // 
            // gbxResTipos
            // 
            this.gbxResTipos.AccessibleDescription = "Tipo Cliente";
            this.gbxResTipos.Controls.Add(this.rbnItemMp);
            this.gbxResTipos.Controls.Add(this.rbnTransportadorasRes);
            this.gbxResTipos.Controls.Add(this.rbnClienteRes);
            this.gbxResTipos.Controls.Add(this.rbnFornecedorRes);
            this.gbxResTipos.Controls.Add(this.rbnTodosRes);
            this.gbxResTipos.Location = new System.Drawing.Point(626, 119);
            this.gbxResTipos.Name = "gbxResTipos";
            this.gbxResTipos.Size = new System.Drawing.Size(343, 91);
            this.gbxResTipos.TabIndex = 2;
            this.gbxResTipos.TabStop = false;
            this.gbxResTipos.Text = "Tipo Cliente";
            this.toolTip1.SetToolTip(this.gbxResTipos, "Tipo Cliente");
            // 
            // rbnTransportadorasRes
            // 
            this.rbnTransportadorasRes.AccessibleDescription = "Cliente";
            this.rbnTransportadorasRes.AutoSize = true;
            this.rbnTransportadorasRes.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbnTransportadorasRes.ForeColor = System.Drawing.Color.Navy;
            this.rbnTransportadorasRes.Location = new System.Drawing.Point(12, 47);
            this.rbnTransportadorasRes.Name = "rbnTransportadorasRes";
            this.rbnTransportadorasRes.Size = new System.Drawing.Size(165, 17);
            this.rbnTransportadorasRes.TabIndex = 1;
            this.rbnTransportadorasRes.Text = "Apenas Transportadoras";
            this.toolTip1.SetToolTip(this.rbnTransportadorasRes, "Tipo - Cliente");
            this.rbnTransportadorasRes.UseVisualStyleBackColor = true;
            this.rbnTransportadorasRes.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnTransportadorasRes.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTransportadorasRes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // rbnClienteRes
            // 
            this.rbnClienteRes.AccessibleDescription = "Cliente";
            this.rbnClienteRes.AutoSize = true;
            this.rbnClienteRes.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbnClienteRes.ForeColor = System.Drawing.Color.Navy;
            this.rbnClienteRes.Location = new System.Drawing.Point(190, 47);
            this.rbnClienteRes.Name = "rbnClienteRes";
            this.rbnClienteRes.Size = new System.Drawing.Size(115, 17);
            this.rbnClienteRes.TabIndex = 3;
            this.rbnClienteRes.Text = "Apenas Clientes";
            this.toolTip1.SetToolTip(this.rbnClienteRes, "Tipo - Cliente");
            this.rbnClienteRes.UseVisualStyleBackColor = true;
            this.rbnClienteRes.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnClienteRes.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnClienteRes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // rbnFornecedorRes
            // 
            this.rbnFornecedorRes.AccessibleDescription = "Fornecedor";
            this.rbnFornecedorRes.AutoSize = true;
            this.rbnFornecedorRes.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbnFornecedorRes.ForeColor = System.Drawing.Color.Navy;
            this.rbnFornecedorRes.Location = new System.Drawing.Point(190, 65);
            this.rbnFornecedorRes.Name = "rbnFornecedorRes";
            this.rbnFornecedorRes.Size = new System.Drawing.Size(147, 17);
            this.rbnFornecedorRes.TabIndex = 4;
            this.rbnFornecedorRes.Text = "Apenas Fornecedores";
            this.toolTip1.SetToolTip(this.rbnFornecedorRes, "Tipo - Fornecedor");
            this.rbnFornecedorRes.UseVisualStyleBackColor = true;
            this.rbnFornecedorRes.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnFornecedorRes.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnFornecedorRes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // rbnTodosRes
            // 
            this.rbnTodosRes.AccessibleDescription = "Todos";
            this.rbnTodosRes.AutoSize = true;
            this.rbnTodosRes.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbnTodosRes.Checked = true;
            this.rbnTodosRes.ForeColor = System.Drawing.Color.Navy;
            this.rbnTodosRes.Location = new System.Drawing.Point(12, 27);
            this.rbnTodosRes.Name = "rbnTodosRes";
            this.rbnTodosRes.Size = new System.Drawing.Size(59, 17);
            this.rbnTodosRes.TabIndex = 0;
            this.rbnTodosRes.TabStop = true;
            this.rbnTodosRes.Text = "Todos";
            this.toolTip1.SetToolTip(this.rbnTodosRes, "Tipo - Todos");
            this.rbnTodosRes.UseVisualStyleBackColor = true;
            this.rbnTodosRes.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnTodosRes.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTodosRes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // tspResumo
            // 
            this.tspResumo.AccessibleDescription = "Barra de Opções";
            this.tspResumo.AutoSize = false;
            this.tspResumo.Dock = System.Windows.Forms.DockStyle.None;
            this.tspResumo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspResumo.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspResumo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspImprimir,
            this.tspRetornar});
            this.tspResumo.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspResumo.Location = new System.Drawing.Point(224, 478);
            this.tspResumo.Name = "tspResumo";
            this.tspResumo.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspResumo.Size = new System.Drawing.Size(556, 44);
            this.tspResumo.TabIndex = 4;
            this.tspResumo.TabStop = true;
            this.tspResumo.Text = "toolStrip1";
            this.toolTip1.SetToolTip(this.tspResumo, "Resumo - Botoes");
            // 
            // tspImprimir
            // 
            this.tspImprimir.AccessibleDescription = "Imprimir";
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimir.Image")));
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(58, 42);
            this.tspImprimir.Text = "&Imprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.ToolTipText = "Resumo - Imprimir";
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
            this.tspRetornar.Size = new System.Drawing.Size(58, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.ToolTipText = "Resumo - Retornar";
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // gbxResOrdem
            // 
            this.gbxResOrdem.Controls.Add(this.lbxResOrdem);
            this.gbxResOrdem.Controls.Add(this.label15);
            this.gbxResOrdem.Location = new System.Drawing.Point(627, 226);
            this.gbxResOrdem.Name = "gbxResOrdem";
            this.gbxResOrdem.Size = new System.Drawing.Size(343, 136);
            this.gbxResOrdem.TabIndex = 3;
            this.gbxResOrdem.TabStop = false;
            this.gbxResOrdem.Text = "A lista deverá sair na seguinte Ordem :";
            this.toolTip1.SetToolTip(this.gbxResOrdem, "Grupo -Resumo - Dados do Produto");
            // 
            // lbxResOrdem
            // 
            this.lbxResOrdem.AccessibleDescription = "Ordem das Listas";
            this.lbxResOrdem.FormattingEnabled = true;
            this.lbxResOrdem.Items.AddRange(new object[] {
            "Por Cognome",
            "Por Cidade + Bairro + Cognome",
            "Por Cep + Cognome",
            "Por Estado + Cep + Cognome",
            "Por Zona/Região + Cognome",
            "Por Ramo de Atividade + Cognome"});
            this.lbxResOrdem.Location = new System.Drawing.Point(6, 24);
            this.lbxResOrdem.Name = "lbxResOrdem";
            this.lbxResOrdem.Size = new System.Drawing.Size(247, 95);
            this.lbxResOrdem.TabIndex = 0;
            this.toolTip1.SetToolTip(this.lbxResOrdem, "Resumo - Ordem");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 13);
            this.label15.TabIndex = 9;
            // 
            // gbxResParametros
            // 
            this.gbxResParametros.AccessibleDescription = "Parametros da Filtragem";
            this.gbxResParametros.Controls.Add(this.btnResZonaAte);
            this.gbxResParametros.Controls.Add(this.btnResZonaDe);
            this.gbxResParametros.Controls.Add(this.btnResUfAte);
            this.gbxResParametros.Controls.Add(this.btnResUfDe);
            this.gbxResParametros.Controls.Add(this.btnResCidadeAte);
            this.gbxResParametros.Controls.Add(this.btnResCidadeDe);
            this.gbxResParametros.Controls.Add(this.btnResRepresentanteAte);
            this.gbxResParametros.Controls.Add(this.btnResRamoAte);
            this.gbxResParametros.Controls.Add(this.btnResRepresentanteDe);
            this.gbxResParametros.Controls.Add(this.btnResRamoDe);
            this.gbxResParametros.Controls.Add(this.tbxResVendedorAte);
            this.gbxResParametros.Controls.Add(this.label8);
            this.gbxResParametros.Controls.Add(this.tbxResVendedorDe);
            this.gbxResParametros.Controls.Add(this.label16);
            this.gbxResParametros.Controls.Add(this.tbxResRamoAte);
            this.gbxResParametros.Controls.Add(this.label5);
            this.gbxResParametros.Controls.Add(this.tbxResRamoDe);
            this.gbxResParametros.Controls.Add(this.label7);
            this.gbxResParametros.Controls.Add(this.tbxResZonaAte);
            this.gbxResParametros.Controls.Add(this.label3);
            this.gbxResParametros.Controls.Add(this.tbxResZonaDe);
            this.gbxResParametros.Controls.Add(this.label4);
            this.gbxResParametros.Controls.Add(this.tbxResEstadoAte);
            this.gbxResParametros.Controls.Add(this.label13);
            this.gbxResParametros.Controls.Add(this.label14);
            this.gbxResParametros.Controls.Add(this.tbxResEstadoDe);
            this.gbxResParametros.Controls.Add(this.tbxResCidadeAte);
            this.gbxResParametros.Controls.Add(this.label11);
            this.gbxResParametros.Controls.Add(this.label12);
            this.gbxResParametros.Controls.Add(this.tbxResCidadeDe);
            this.gbxResParametros.Controls.Add(this.tbxResCepAte);
            this.gbxResParametros.Controls.Add(this.label9);
            this.gbxResParametros.Controls.Add(this.label10);
            this.gbxResParametros.Controls.Add(this.tbxResCepDe);
            this.gbxResParametros.Controls.Add(this.btnResFornecedorAte);
            this.gbxResParametros.Controls.Add(this.tbxResFornecedorAte);
            this.gbxResParametros.Controls.Add(this.bntResFornecedorDe);
            this.gbxResParametros.Controls.Add(this.label6);
            this.gbxResParametros.Controls.Add(this.label29);
            this.gbxResParametros.Controls.Add(this.tbxResFornecedorDe);
            this.gbxResParametros.Location = new System.Drawing.Point(12, 119);
            this.gbxResParametros.Name = "gbxResParametros";
            this.gbxResParametros.Size = new System.Drawing.Size(608, 243);
            this.gbxResParametros.TabIndex = 1;
            this.gbxResParametros.TabStop = false;
            this.gbxResParametros.Text = "Parametros da Filtragem";
            this.toolTip1.SetToolTip(this.gbxResParametros, "Grupo - Resumo - Parametros da Filtragem");
            // 
            // btnResZonaAte
            // 
            this.btnResZonaAte.AccessibleDescription = "Busca Fornecedor";
            this.btnResZonaAte.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResZonaAte.Image = ((System.Drawing.Image)(resources.GetObject("btnResZonaAte.Image")));
            this.btnResZonaAte.Location = new System.Drawing.Point(582, 132);
            this.btnResZonaAte.Name = "btnResZonaAte";
            this.btnResZonaAte.Size = new System.Drawing.Size(20, 24);
            this.btnResZonaAte.TabIndex = 17;
            this.btnResZonaAte.TabStop = false;
            this.btnResZonaAte.Text = "...";
            this.toolTip1.SetToolTip(this.btnResZonaAte, " Escolher - Resumo -  Fornecedor De");
            this.btnResZonaAte.UseVisualStyleBackColor = true;
            this.btnResZonaAte.Click += new System.EventHandler(this.btnResZonaAte_Click);
            // 
            // btnResZonaDe
            // 
            this.btnResZonaDe.AccessibleDescription = "Busca Fornecedor";
            this.btnResZonaDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResZonaDe.Image = ((System.Drawing.Image)(resources.GetObject("btnResZonaDe.Image")));
            this.btnResZonaDe.Location = new System.Drawing.Point(316, 129);
            this.btnResZonaDe.Name = "btnResZonaDe";
            this.btnResZonaDe.Size = new System.Drawing.Size(20, 24);
            this.btnResZonaDe.TabIndex = 15;
            this.btnResZonaDe.TabStop = false;
            this.btnResZonaDe.Text = "...";
            this.toolTip1.SetToolTip(this.btnResZonaDe, " Escolher - Resumo -  Fornecedor De");
            this.btnResZonaDe.UseVisualStyleBackColor = true;
            this.btnResZonaDe.Click += new System.EventHandler(this.btnResZonaDe_Click);
            // 
            // btnResUfAte
            // 
            this.btnResUfAte.AccessibleDescription = "Busca Fornecedor";
            this.btnResUfAte.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResUfAte.Image = ((System.Drawing.Image)(resources.GetObject("btnResUfAte.Image")));
            this.btnResUfAte.Location = new System.Drawing.Point(459, 104);
            this.btnResUfAte.Name = "btnResUfAte";
            this.btnResUfAte.Size = new System.Drawing.Size(20, 24);
            this.btnResUfAte.TabIndex = 13;
            this.btnResUfAte.TabStop = false;
            this.btnResUfAte.Text = "...";
            this.toolTip1.SetToolTip(this.btnResUfAte, " Escolher - Resumo -  Fornecedor De");
            this.btnResUfAte.UseVisualStyleBackColor = true;
            this.btnResUfAte.Click += new System.EventHandler(this.btnResUfAte_Click);
            // 
            // btnResUfDe
            // 
            this.btnResUfDe.AccessibleDescription = "Busca Fornecedor";
            this.btnResUfDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResUfDe.Image = ((System.Drawing.Image)(resources.GetObject("btnResUfDe.Image")));
            this.btnResUfDe.Location = new System.Drawing.Point(194, 102);
            this.btnResUfDe.Name = "btnResUfDe";
            this.btnResUfDe.Size = new System.Drawing.Size(20, 24);
            this.btnResUfDe.TabIndex = 11;
            this.btnResUfDe.TabStop = false;
            this.btnResUfDe.Text = "...";
            this.toolTip1.SetToolTip(this.btnResUfDe, " Escolher - Resumo -  Fornecedor De");
            this.btnResUfDe.UseVisualStyleBackColor = true;
            this.btnResUfDe.Click += new System.EventHandler(this.btnResUfDe_Click);
            // 
            // btnResCidadeAte
            // 
            this.btnResCidadeAte.AccessibleDescription = "Busca Fornecedor";
            this.btnResCidadeAte.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResCidadeAte.Image = ((System.Drawing.Image)(resources.GetObject("btnResCidadeAte.Image")));
            this.btnResCidadeAte.Location = new System.Drawing.Point(582, 77);
            this.btnResCidadeAte.Name = "btnResCidadeAte";
            this.btnResCidadeAte.Size = new System.Drawing.Size(20, 24);
            this.btnResCidadeAte.TabIndex = 9;
            this.btnResCidadeAte.TabStop = false;
            this.btnResCidadeAte.Text = "...";
            this.toolTip1.SetToolTip(this.btnResCidadeAte, " Escolher - Resumo -  Fornecedor De");
            this.btnResCidadeAte.UseVisualStyleBackColor = true;
            this.btnResCidadeAte.Click += new System.EventHandler(this.btnResCidadeAte_Click);
            // 
            // btnResCidadeDe
            // 
            this.btnResCidadeDe.AccessibleDescription = "Busca Fornecedor";
            this.btnResCidadeDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResCidadeDe.Image = ((System.Drawing.Image)(resources.GetObject("btnResCidadeDe.Image")));
            this.btnResCidadeDe.Location = new System.Drawing.Point(316, 77);
            this.btnResCidadeDe.Name = "btnResCidadeDe";
            this.btnResCidadeDe.Size = new System.Drawing.Size(20, 24);
            this.btnResCidadeDe.TabIndex = 7;
            this.btnResCidadeDe.TabStop = false;
            this.btnResCidadeDe.Text = "...";
            this.toolTip1.SetToolTip(this.btnResCidadeDe, " Escolher - Resumo -  Fornecedor De");
            this.btnResCidadeDe.UseVisualStyleBackColor = true;
            this.btnResCidadeDe.Click += new System.EventHandler(this.btnResCidadeDe_Click);
            // 
            // btnResRepresentanteAte
            // 
            this.btnResRepresentanteAte.AccessibleDescription = "Busca Fornecedor";
            this.btnResRepresentanteAte.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResRepresentanteAte.Image = ((System.Drawing.Image)(resources.GetObject("btnResRepresentanteAte.Image")));
            this.btnResRepresentanteAte.Location = new System.Drawing.Point(582, 185);
            this.btnResRepresentanteAte.Name = "btnResRepresentanteAte";
            this.btnResRepresentanteAte.Size = new System.Drawing.Size(20, 24);
            this.btnResRepresentanteAte.TabIndex = 25;
            this.btnResRepresentanteAte.TabStop = false;
            this.btnResRepresentanteAte.Text = "...";
            this.toolTip1.SetToolTip(this.btnResRepresentanteAte, " Escolher - Resumo -  Fornecedor De");
            this.btnResRepresentanteAte.UseVisualStyleBackColor = true;
            this.btnResRepresentanteAte.Click += new System.EventHandler(this.btnResRepresentanteAte_Click);
            // 
            // btnResRamoAte
            // 
            this.btnResRamoAte.AccessibleDescription = "Busca Fornecedor";
            this.btnResRamoAte.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResRamoAte.Image = ((System.Drawing.Image)(resources.GetObject("btnResRamoAte.Image")));
            this.btnResRamoAte.Location = new System.Drawing.Point(582, 157);
            this.btnResRamoAte.Name = "btnResRamoAte";
            this.btnResRamoAte.Size = new System.Drawing.Size(20, 24);
            this.btnResRamoAte.TabIndex = 21;
            this.btnResRamoAte.TabStop = false;
            this.btnResRamoAte.Text = "...";
            this.toolTip1.SetToolTip(this.btnResRamoAte, " Escolher - Resumo -  Fornecedor De");
            this.btnResRamoAte.UseVisualStyleBackColor = true;
            this.btnResRamoAte.Click += new System.EventHandler(this.btnResRamoAte_Click);
            // 
            // btnResRepresentanteDe
            // 
            this.btnResRepresentanteDe.AccessibleDescription = "Busca Fornecedor";
            this.btnResRepresentanteDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResRepresentanteDe.Image = ((System.Drawing.Image)(resources.GetObject("btnResRepresentanteDe.Image")));
            this.btnResRepresentanteDe.Location = new System.Drawing.Point(316, 184);
            this.btnResRepresentanteDe.Name = "btnResRepresentanteDe";
            this.btnResRepresentanteDe.Size = new System.Drawing.Size(20, 24);
            this.btnResRepresentanteDe.TabIndex = 23;
            this.btnResRepresentanteDe.TabStop = false;
            this.btnResRepresentanteDe.Text = "...";
            this.toolTip1.SetToolTip(this.btnResRepresentanteDe, " Escolher - Resumo -  Fornecedor De");
            this.btnResRepresentanteDe.UseVisualStyleBackColor = true;
            this.btnResRepresentanteDe.Click += new System.EventHandler(this.btnResRepresentanteDe_Click);
            // 
            // btnResRamoDe
            // 
            this.btnResRamoDe.AccessibleDescription = "Busca Fornecedor";
            this.btnResRamoDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResRamoDe.Image = ((System.Drawing.Image)(resources.GetObject("btnResRamoDe.Image")));
            this.btnResRamoDe.Location = new System.Drawing.Point(316, 158);
            this.btnResRamoDe.Name = "btnResRamoDe";
            this.btnResRamoDe.Size = new System.Drawing.Size(20, 24);
            this.btnResRamoDe.TabIndex = 19;
            this.btnResRamoDe.TabStop = false;
            this.btnResRamoDe.Text = "...";
            this.toolTip1.SetToolTip(this.btnResRamoDe, " Escolher - Resumo -  Fornecedor De");
            this.btnResRamoDe.UseVisualStyleBackColor = true;
            this.btnResRamoDe.Click += new System.EventHandler(this.btnResRamoDe_Click);
            // 
            // tbxResVendedorAte
            // 
            this.tbxResVendedorAte.AccessibleDescription = "Até Vendedor";
            this.tbxResVendedorAte.BackColor = System.Drawing.Color.LightGray;
            this.tbxResVendedorAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResVendedorAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResVendedorAte.Location = new System.Drawing.Point(396, 187);
            this.tbxResVendedorAte.Name = "tbxResVendedorAte";
            this.tbxResVendedorAte.Size = new System.Drawing.Size(182, 21);
            this.tbxResVendedorAte.TabIndex = 24;
            this.toolTip1.SetToolTip(this.tbxResVendedorAte, "Resumo - Dt Recebimento Ate");
            this.tbxResVendedorAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResVendedorAte.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResVendedorAte.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 13);
            this.label8.TabIndex = 135;
            this.label8.Text = "Do Representante de :";
            // 
            // tbxResVendedorDe
            // 
            this.tbxResVendedorDe.AccessibleDescription = "Do Vendedor";
            this.tbxResVendedorDe.BackColor = System.Drawing.Color.LightGray;
            this.tbxResVendedorDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResVendedorDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResVendedorDe.Location = new System.Drawing.Point(142, 186);
            this.tbxResVendedorDe.Name = "tbxResVendedorDe";
            this.tbxResVendedorDe.Size = new System.Drawing.Size(169, 21);
            this.tbxResVendedorDe.TabIndex = 22;
            this.toolTip1.SetToolTip(this.tbxResVendedorDe, "Resumo - Dt Recebimento De");
            this.tbxResVendedorDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResVendedorDe.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResVendedorDe.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(359, 191);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 134;
            this.label16.Text = "Até :";
            // 
            // tbxResRamoAte
            // 
            this.tbxResRamoAte.AccessibleDescription = "Até Ramo de Atividade";
            this.tbxResRamoAte.BackColor = System.Drawing.Color.LightGray;
            this.tbxResRamoAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResRamoAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResRamoAte.Location = new System.Drawing.Point(395, 160);
            this.tbxResRamoAte.Name = "tbxResRamoAte";
            this.tbxResRamoAte.Size = new System.Drawing.Size(183, 21);
            this.tbxResRamoAte.TabIndex = 20;
            this.toolTip1.SetToolTip(this.tbxResRamoAte, "Resumo - Dt Recebimento Ate");
            this.tbxResRamoAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResRamoAte.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResRamoAte.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 13);
            this.label5.TabIndex = 131;
            this.label5.Text = "Do Ramo de Atividade de :";
            // 
            // tbxResRamoDe
            // 
            this.tbxResRamoDe.AccessibleDescription = "Do Ramo de Atividade";
            this.tbxResRamoDe.BackColor = System.Drawing.Color.LightGray;
            this.tbxResRamoDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResRamoDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResRamoDe.Location = new System.Drawing.Point(161, 159);
            this.tbxResRamoDe.Name = "tbxResRamoDe";
            this.tbxResRamoDe.Size = new System.Drawing.Size(150, 21);
            this.tbxResRamoDe.TabIndex = 18;
            this.toolTip1.SetToolTip(this.tbxResRamoDe, "Resumo - Dt Recebimento De");
            this.tbxResRamoDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResRamoDe.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResRamoDe.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(358, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 130;
            this.label7.Text = "Até :";
            // 
            // tbxResZonaAte
            // 
            this.tbxResZonaAte.AccessibleDescription = "Até Zona/Região";
            this.tbxResZonaAte.BackColor = System.Drawing.Color.LightGray;
            this.tbxResZonaAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResZonaAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResZonaAte.Location = new System.Drawing.Point(395, 133);
            this.tbxResZonaAte.Name = "tbxResZonaAte";
            this.tbxResZonaAte.Size = new System.Drawing.Size(183, 21);
            this.tbxResZonaAte.TabIndex = 16;
            this.toolTip1.SetToolTip(this.tbxResZonaAte, "Resumo - Dt Recebimento Ate");
            this.tbxResZonaAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResZonaAte.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResZonaAte.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 127;
            this.label3.Text = "Da Zona/Região de :";
            // 
            // tbxResZonaDe
            // 
            this.tbxResZonaDe.AccessibleDescription = "Da Zona/Região";
            this.tbxResZonaDe.BackColor = System.Drawing.Color.LightGray;
            this.tbxResZonaDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResZonaDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResZonaDe.Location = new System.Drawing.Point(129, 132);
            this.tbxResZonaDe.Name = "tbxResZonaDe";
            this.tbxResZonaDe.Size = new System.Drawing.Size(182, 21);
            this.tbxResZonaDe.TabIndex = 14;
            this.toolTip1.SetToolTip(this.tbxResZonaDe, "Resumo - Dt Recebimento De");
            this.tbxResZonaDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResZonaDe.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResZonaDe.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(358, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "Até :";
            // 
            // tbxResEstadoAte
            // 
            this.tbxResEstadoAte.AccessibleDescription = "Ate Estado";
            this.tbxResEstadoAte.BackColor = System.Drawing.Color.LightGray;
            this.tbxResEstadoAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResEstadoAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResEstadoAte.Location = new System.Drawing.Point(396, 106);
            this.tbxResEstadoAte.Name = "tbxResEstadoAte";
            this.tbxResEstadoAte.Size = new System.Drawing.Size(59, 21);
            this.tbxResEstadoAte.TabIndex = 12;
            this.toolTip1.SetToolTip(this.tbxResEstadoAte, "Resumo - Dt Lançamento Ate");
            this.tbxResEstadoAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResEstadoAte.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResEstadoAte.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(359, 109);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 123;
            this.label13.Text = "Até :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(40, 108);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 13);
            this.label14.TabIndex = 122;
            this.label14.Text = "Do Estado De :";
            // 
            // tbxResEstadoDe
            // 
            this.tbxResEstadoDe.AccessibleDescription = "Do Estado";
            this.tbxResEstadoDe.BackColor = System.Drawing.Color.LightGray;
            this.tbxResEstadoDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResEstadoDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResEstadoDe.Location = new System.Drawing.Point(130, 105);
            this.tbxResEstadoDe.Name = "tbxResEstadoDe";
            this.tbxResEstadoDe.Size = new System.Drawing.Size(59, 21);
            this.tbxResEstadoDe.TabIndex = 10;
            this.toolTip1.SetToolTip(this.tbxResEstadoDe, "Resumo - Dt Lançamento De");
            this.tbxResEstadoDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResEstadoDe.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResEstadoDe.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxResCidadeAte
            // 
            this.tbxResCidadeAte.AccessibleDescription = "Até Cidade";
            this.tbxResCidadeAte.BackColor = System.Drawing.Color.LightGray;
            this.tbxResCidadeAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResCidadeAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResCidadeAte.Location = new System.Drawing.Point(395, 80);
            this.tbxResCidadeAte.Name = "tbxResCidadeAte";
            this.tbxResCidadeAte.Size = new System.Drawing.Size(183, 21);
            this.tbxResCidadeAte.TabIndex = 8;
            this.toolTip1.SetToolTip(this.tbxResCidadeAte, "Resumo - Dt Recebimento Ate");
            this.tbxResCidadeAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResCidadeAte.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResCidadeAte.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(359, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 117;
            this.label11.Text = "Até :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(40, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 116;
            this.label12.Text = "Da Cidade de :";
            // 
            // tbxResCidadeDe
            // 
            this.tbxResCidadeDe.AccessibleDescription = "Da Cidade";
            this.tbxResCidadeDe.BackColor = System.Drawing.Color.LightGray;
            this.tbxResCidadeDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResCidadeDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResCidadeDe.Location = new System.Drawing.Point(129, 79);
            this.tbxResCidadeDe.Name = "tbxResCidadeDe";
            this.tbxResCidadeDe.Size = new System.Drawing.Size(182, 21);
            this.tbxResCidadeDe.TabIndex = 6;
            this.toolTip1.SetToolTip(this.tbxResCidadeDe, "Resumo - Dt Recebimento De");
            this.tbxResCidadeDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResCidadeDe.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResCidadeDe.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxResCepAte
            // 
            this.tbxResCepAte.AccessibleDescription = "Ate Cep";
            this.tbxResCepAte.BackColor = System.Drawing.Color.LightGray;
            this.tbxResCepAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResCepAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResCepAte.Location = new System.Drawing.Point(395, 53);
            this.tbxResCepAte.Name = "tbxResCepAte";
            this.tbxResCepAte.Size = new System.Drawing.Size(106, 21);
            this.tbxResCepAte.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tbxResCepAte, "Resumo - Dt Emissão Ate");
            this.tbxResCepAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResCepAte.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResCepAte.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(358, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 111;
            this.label9.Text = "Até :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(36, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 110;
            this.label10.Text = "Do Cep de Nro :";
            // 
            // tbxResCepDe
            // 
            this.tbxResCepDe.AccessibleDescription = "Do Cep";
            this.tbxResCepDe.BackColor = System.Drawing.Color.LightGray;
            this.tbxResCepDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResCepDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResCepDe.Location = new System.Drawing.Point(129, 52);
            this.tbxResCepDe.Name = "tbxResCepDe";
            this.tbxResCepDe.Size = new System.Drawing.Size(106, 21);
            this.tbxResCepDe.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tbxResCepDe, "Resumo - Dt Emissão De");
            this.tbxResCepDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResCepDe.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResCepDe.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // btnResFornecedorAte
            // 
            this.btnResFornecedorAte.AccessibleDescription = "Busca Fronecedor";
            this.btnResFornecedorAte.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResFornecedorAte.Image = ((System.Drawing.Image)(resources.GetObject("btnResFornecedorAte.Image")));
            this.btnResFornecedorAte.Location = new System.Drawing.Point(582, 25);
            this.btnResFornecedorAte.Name = "btnResFornecedorAte";
            this.btnResFornecedorAte.Size = new System.Drawing.Size(20, 24);
            this.btnResFornecedorAte.TabIndex = 3;
            this.btnResFornecedorAte.TabStop = false;
            this.btnResFornecedorAte.Text = "...";
            this.toolTip1.SetToolTip(this.btnResFornecedorAte, "Escolher - Resumo - Fornecedor Ate");
            this.btnResFornecedorAte.UseVisualStyleBackColor = true;
            this.btnResFornecedorAte.Click += new System.EventHandler(this.btnResFornecedorAte_Click);
            // 
            // tbxResFornecedorAte
            // 
            this.tbxResFornecedorAte.AccessibleDescription = "Cognome Até";
            this.tbxResFornecedorAte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxResFornecedorAte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxResFornecedorAte.BackColor = System.Drawing.Color.LightGray;
            this.tbxResFornecedorAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResFornecedorAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResFornecedorAte.Location = new System.Drawing.Point(395, 27);
            this.tbxResFornecedorAte.Name = "tbxResFornecedorAte";
            this.tbxResFornecedorAte.Size = new System.Drawing.Size(183, 21);
            this.tbxResFornecedorAte.TabIndex = 2;
            this.toolTip1.SetToolTip(this.tbxResFornecedorAte, "Resumo - Fornecedor Ate");
            this.tbxResFornecedorAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResFornecedorAte.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResFornecedorAte.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // bntResFornecedorDe
            // 
            this.bntResFornecedorDe.AccessibleDescription = "Busca Fornecedor";
            this.bntResFornecedorDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntResFornecedorDe.Image = ((System.Drawing.Image)(resources.GetObject("bntResFornecedorDe.Image")));
            this.bntResFornecedorDe.Location = new System.Drawing.Point(316, 25);
            this.bntResFornecedorDe.Name = "bntResFornecedorDe";
            this.bntResFornecedorDe.Size = new System.Drawing.Size(20, 24);
            this.bntResFornecedorDe.TabIndex = 1;
            this.bntResFornecedorDe.TabStop = false;
            this.bntResFornecedorDe.Text = "...";
            this.toolTip1.SetToolTip(this.bntResFornecedorDe, " Escolher - Resumo -  Fornecedor De");
            this.bntResFornecedorDe.UseVisualStyleBackColor = true;
            this.bntResFornecedorDe.Click += new System.EventHandler(this.bntResFornecedorDe_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(359, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Até :";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(44, 29);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(84, 13);
            this.label29.TabIndex = 47;
            this.label29.Text = "Do Cognome :";
            // 
            // tbxResFornecedorDe
            // 
            this.tbxResFornecedorDe.AccessibleDescription = "Cognome De";
            this.tbxResFornecedorDe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxResFornecedorDe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxResFornecedorDe.BackColor = System.Drawing.Color.LightGray;
            this.tbxResFornecedorDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxResFornecedorDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxResFornecedorDe.Location = new System.Drawing.Point(129, 26);
            this.tbxResFornecedorDe.Name = "tbxResFornecedorDe";
            this.tbxResFornecedorDe.Size = new System.Drawing.Size(182, 21);
            this.tbxResFornecedorDe.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxResFornecedorDe, "Resumo - Fornecedor De");
            this.tbxResFornecedorDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxResFornecedorDe.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxResFornecedorDe.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // gbxResTiposdeListas
            // 
            this.gbxResTiposdeListas.AccessibleDescription = "Indique abaixo o tipo da lista :";
            this.gbxResTiposdeListas.Controls.Add(this.rbnItemContato);
            this.gbxResTiposdeListas.Controls.Add(this.rbnResSimples01);
            this.gbxResTiposdeListas.Controls.Add(this.label1);
            this.gbxResTiposdeListas.Location = new System.Drawing.Point(6, 8);
            this.gbxResTiposdeListas.Name = "gbxResTiposdeListas";
            this.gbxResTiposdeListas.Size = new System.Drawing.Size(435, 105);
            this.gbxResTiposdeListas.TabIndex = 0;
            this.gbxResTiposdeListas.TabStop = false;
            this.gbxResTiposdeListas.Text = "Indique abaixo o tipo da lista :";
            this.toolTip1.SetToolTip(this.gbxResTiposdeListas, "Grupo - Resumo - Dados do Produto");
            // 
            // rbnResSimples01
            // 
            this.rbnResSimples01.AccessibleDescription = "Simples";
            this.rbnResSimples01.AutoSize = true;
            this.rbnResSimples01.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbnResSimples01.Checked = true;
            this.rbnResSimples01.ForeColor = System.Drawing.Color.Navy;
            this.rbnResSimples01.Location = new System.Drawing.Point(6, 21);
            this.rbnResSimples01.Name = "rbnResSimples01";
            this.rbnResSimples01.Size = new System.Drawing.Size(250, 17);
            this.rbnResSimples01.TabIndex = 0;
            this.rbnResSimples01.TabStop = true;
            this.rbnResSimples01.Text = "Simples [Nome, Endereço, Fone, Região]";
            this.toolTip1.SetToolTip(this.rbnResSimples01, "Resumo - Simples");
            this.rbnResSimples01.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(988, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Imprimir Cadastro de Clientes/Fornecedores/Transportadoras";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label2, "Tolerancia Ensaio");
            // 
            // bwrCarregaAutoComplete
            // 
            this.bwrCarregaAutoComplete.WorkerSupportsCancellation = true;
            // 
            // rbnItemMp
            // 
            this.rbnItemMp.AccessibleDescription = "MP contaminada";
            this.rbnItemMp.AutoSize = true;
            this.rbnItemMp.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbnItemMp.ForeColor = System.Drawing.Color.Navy;
            this.rbnItemMp.Location = new System.Drawing.Point(12, 66);
            this.rbnItemMp.Name = "rbnItemMp";
            this.rbnItemMp.Size = new System.Drawing.Size(165, 17);
            this.rbnItemMp.TabIndex = 2;
            this.rbnItemMp.Text = "Apenas Mp Contaminada";
            this.toolTip1.SetToolTip(this.rbnItemMp, "Tipo - Mp");
            this.rbnItemMp.UseVisualStyleBackColor = true;
            // 
            // rbnItemContato
            // 
            this.rbnItemContato.AccessibleDescription = "Contato";
            this.rbnItemContato.AutoSize = true;
            this.rbnItemContato.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbnItemContato.Checked = true;
            this.rbnItemContato.ForeColor = System.Drawing.Color.Navy;
            this.rbnItemContato.Location = new System.Drawing.Point(6, 44);
            this.rbnItemContato.Name = "rbnItemContato";
            this.rbnItemContato.Size = new System.Drawing.Size(240, 17);
            this.rbnItemContato.TabIndex = 10;
            this.rbnItemContato.TabStop = true;
            this.rbnItemContato.Text = "Contato [Todos contatos do parceiro] ";
            this.toolTip1.SetToolTip(this.rbnItemContato, "Resumo - Simples");
            this.rbnItemContato.UseVisualStyleBackColor = true;
            // 
            // frmRelCliente
            // 
            this.AccessibleDescription = "NFE";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRelCliente";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "34532";
            this.Text = "Relatórios Notas Fiscais de Entrada - Visualizando";
            this.toolTip1.SetToolTip(this, "Relatórios Notas Fiscais de Entrada - Visualizando");
            this.Load += new System.EventHandler(this.frmRelCliente_Load);
            this.Activated += new System.EventHandler(this.frmRelCliente_Activated);
            this.groupBox3.ResumeLayout(false);
            this.tabClientesRes.ResumeLayout(false);
            this.tabCabecalho.ResumeLayout(false);
            this.gbxResTipos.ResumeLayout(false);
            this.gbxResTipos.PerformLayout();
            this.tspResumo.ResumeLayout(false);
            this.tspResumo.PerformLayout();
            this.gbxResOrdem.ResumeLayout(false);
            this.gbxResOrdem.PerformLayout();
            this.gbxResParametros.ResumeLayout(false);
            this.gbxResParametros.PerformLayout();
            this.gbxResTiposdeListas.ResumeLayout(false);
            this.gbxResTiposdeListas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker bwrCarregaAutoComplete;
        private System.Windows.Forms.TabControl tabClientesRes;
        private System.Windows.Forms.TabPage tabCabecalho;
        private System.Windows.Forms.GroupBox gbxResTipos;
        private System.Windows.Forms.RadioButton rbnClienteRes;
        private System.Windows.Forms.RadioButton rbnFornecedorRes;
        private System.Windows.Forms.RadioButton rbnTodosRes;
        private System.Windows.Forms.ToolStrip tspResumo;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxResOrdem;
        private System.Windows.Forms.ListBox lbxResOrdem;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox gbxResParametros;
        private System.Windows.Forms.TextBox tbxResEstadoAte;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbxResEstadoDe;
        private System.Windows.Forms.TextBox tbxResCidadeAte;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxResCidadeDe;
        private System.Windows.Forms.TextBox tbxResCepAte;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxResCepDe;
        private System.Windows.Forms.Button btnResFornecedorAte;
        private System.Windows.Forms.TextBox tbxResFornecedorAte;
        private System.Windows.Forms.Button bntResFornecedorDe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tbxResFornecedorDe;
        private System.Windows.Forms.GroupBox gbxResTiposdeListas;
        private System.Windows.Forms.RadioButton rbnResSimples01;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxResVendedorAte;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxResVendedorDe;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbxResRamoAte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxResRamoDe;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxResZonaAte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxResZonaDe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnResCidadeAte;
        private System.Windows.Forms.Button btnResCidadeDe;
        private System.Windows.Forms.Button btnResRepresentanteAte;
        private System.Windows.Forms.Button btnResRamoAte;
        private System.Windows.Forms.Button btnResRepresentanteDe;
        private System.Windows.Forms.Button btnResRamoDe;
        private System.Windows.Forms.Button btnResUfAte;
        private System.Windows.Forms.Button btnResUfDe;
        private System.Windows.Forms.Button btnResZonaAte;
        private System.Windows.Forms.Button btnResZonaDe;
        private System.Windows.Forms.RadioButton rbnTransportadorasRes;
        private System.Windows.Forms.RadioButton rbnItemMp;
        private System.Windows.Forms.RadioButton rbnItemContato;
    }
}