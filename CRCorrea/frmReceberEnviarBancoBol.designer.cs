namespace CRCorrea
{
    partial class frmReceberEnviarBancoBol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceberEnviarBancoBol));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rbnEnviarLote = new System.Windows.Forms.RadioButton();
            this.rbnEnviarIndividual = new System.Windows.Forms.RadioButton();
            this.gbxConfiguracao = new System.Windows.Forms.GroupBox();
            this.tbxBoletolinha01 = new System.Windows.Forms.TextBox();
            this.tbxProtestar = new System.Windows.Forms.TextBox();
            this.tbxJurosMes = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspGerarArquivo = new System.Windows.Forms.ToolStripButton();
            this.tspCarregarArquivo = new System.Windows.Forms.ToolStripButton();
            this.tspVisualizar = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxBoletoEnviar = new System.Windows.Forms.GroupBox();
            this.dgvBoletoEnviar = new System.Windows.Forms.DataGridView();
            this.gbxBoletoNaoEnviado = new System.Windows.Forms.GroupBox();
            this.dgvBoletoNaoEnviado = new System.Windows.Forms.DataGridView();
            this.gbxConta = new System.Windows.Forms.GroupBox();
            this.tbxTab_BancoNomeDestino = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tbxTab_BancoCodigoDestino = new System.Windows.Forms.TextBox();
            this.chxBancosTodos = new System.Windows.Forms.CheckBox();
            this.tbxNroNota = new System.Windows.Forms.TextBox();
            this.label111 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.btnBancosDestino = new System.Windows.Forms.Button();
            this.tbxBancosNomeDestino = new System.Windows.Forms.TextBox();
            this.tbxBancosCodigoDestino = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBancos = new System.Windows.Forms.Button();
            this.tbxBancosNome = new System.Windows.Forms.TextBox();
            this.tbxBancosCodigo = new System.Windows.Forms.TextBox();
            this.label105 = new System.Windows.Forms.Label();
            this.tbxTab_BancoNome = new System.Windows.Forms.TextBox();
            this.tbxTab_BancoCodigo = new System.Windows.Forms.TextBox();
            this.gbxBoletoEnviado = new System.Windows.Forms.GroupBox();
            this.dgvBoletoEnviado = new System.Windows.Forms.DataGridView();
            this.gbxTipoEnvio = new System.Windows.Forms.GroupBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.gbxConfiguracao.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.gbxBoletoEnviar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletoEnviar)).BeginInit();
            this.gbxBoletoNaoEnviado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletoNaoEnviado)).BeginInit();
            this.gbxConta.SuspendLayout();
            this.gbxBoletoEnviado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletoEnviado)).BeginInit();
            this.gbxTipoEnvio.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbnEnviarLote
            // 
            this.rbnEnviarLote.AccessibleDescription = "Juntar mesmo CPF/CNPJ";
            this.rbnEnviarLote.AutoSize = true;
            this.rbnEnviarLote.Enabled = false;
            this.rbnEnviarLote.Location = new System.Drawing.Point(6, 45);
            this.rbnEnviarLote.Name = "rbnEnviarLote";
            this.rbnEnviarLote.Size = new System.Drawing.Size(46, 17);
            this.rbnEnviarLote.TabIndex = 1;
            this.rbnEnviarLote.Text = "Lote";
            this.toolTip1.SetToolTip(this.rbnEnviarLote, "Juntar mesmo CPF/CNPJ");
            this.rbnEnviarLote.UseVisualStyleBackColor = true;
            // 
            // rbnEnviarIndividual
            // 
            this.rbnEnviarIndividual.AccessibleDescription = "Enviar Individualmente";
            this.rbnEnviarIndividual.AutoSize = true;
            this.rbnEnviarIndividual.Checked = true;
            this.rbnEnviarIndividual.Location = new System.Drawing.Point(6, 23);
            this.rbnEnviarIndividual.Name = "rbnEnviarIndividual";
            this.rbnEnviarIndividual.Size = new System.Drawing.Size(71, 17);
            this.rbnEnviarIndividual.TabIndex = 0;
            this.rbnEnviarIndividual.TabStop = true;
            this.rbnEnviarIndividual.Text = "Individual";
            this.toolTip1.SetToolTip(this.rbnEnviarIndividual, "Enviar Individualmente");
            this.rbnEnviarIndividual.UseVisualStyleBackColor = true;
            // 
            // gbxConfiguracao
            // 
            this.gbxConfiguracao.AccessibleDescription = "";
            this.gbxConfiguracao.Controls.Add(this.tbxBoletolinha01);
            this.gbxConfiguracao.Controls.Add(this.tbxProtestar);
            this.gbxConfiguracao.Controls.Add(this.tbxJurosMes);
            this.gbxConfiguracao.Controls.Add(this.label11);
            this.gbxConfiguracao.Controls.Add(this.label12);
            this.gbxConfiguracao.Controls.Add(this.label13);
            this.gbxConfiguracao.Controls.Add(this.label14);
            this.gbxConfiguracao.Location = new System.Drawing.Point(827, 420);
            this.gbxConfiguracao.Name = "gbxConfiguracao";
            this.gbxConfiguracao.Size = new System.Drawing.Size(181, 210);
            this.gbxConfiguracao.TabIndex = 54;
            this.gbxConfiguracao.TabStop = false;
            this.gbxConfiguracao.Text = "Configuração";
            // 
            // tbxBoletolinha01
            // 
            this.tbxBoletolinha01.AccessibleDescription = "Boleto Linha1";
            this.tbxBoletolinha01.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxBoletolinha01.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBoletolinha01.Location = new System.Drawing.Point(6, 74);
            this.tbxBoletolinha01.Multiline = true;
            this.tbxBoletolinha01.Name = "tbxBoletolinha01";
            this.tbxBoletolinha01.Size = new System.Drawing.Size(172, 130);
            this.tbxBoletolinha01.TabIndex = 50;
            // 
            // tbxProtestar
            // 
            this.tbxProtestar.AccessibleDescription = "Protestar Qtde Dias";
            this.tbxProtestar.BackColor = System.Drawing.Color.White;
            this.tbxProtestar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxProtestar.Location = new System.Drawing.Point(101, 47);
            this.tbxProtestar.Name = "tbxProtestar";
            this.tbxProtestar.Size = new System.Drawing.Size(35, 21);
            this.tbxProtestar.TabIndex = 49;
            this.tbxProtestar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxProtestar.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxProtestar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxProtestar.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxJurosMes
            // 
            this.tbxJurosMes.AccessibleDescription = "Juros Mês";
            this.tbxJurosMes.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxJurosMes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxJurosMes.Location = new System.Drawing.Point(101, 20);
            this.tbxJurosMes.Name = "tbxJurosMes";
            this.tbxJurosMes.Size = new System.Drawing.Size(35, 21);
            this.tbxJurosMes.TabIndex = 48;
            this.tbxJurosMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(142, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 13);
            this.label11.TabIndex = 47;
            this.label11.Text = "%";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(142, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 46;
            this.label12.Text = "dias";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 13);
            this.label13.TabIndex = 45;
            this.label13.Text = "Protestar após:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(36, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 43;
            this.label14.Text = "Juros Mês:";
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspGerarArquivo,
            this.tspCarregarArquivo,
            this.tspVisualizar,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(0, 639);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(1020, 46);
            this.tspTool.TabIndex = 53;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "Barra de Opções";
            // 
            // tspGerarArquivo
            // 
            this.tspGerarArquivo.AccessibleDescription = "Gera o arquivo para enviar ao banco.";
            this.tspGerarArquivo.AutoSize = false;
            this.tspGerarArquivo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspGerarArquivo.Image = global::CRCorrea.Properties.Resources.Exportar;
            this.tspGerarArquivo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspGerarArquivo.Name = "tspGerarArquivo";
            this.tspGerarArquivo.Size = new System.Drawing.Size(200, 42);
            this.tspGerarArquivo.Text = "Gerar Arquivo para Integração";
            this.tspGerarArquivo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspGerarArquivo.Click += new System.EventHandler(this.tspGerarArquivo_Click);
            // 
            // tspCarregarArquivo
            // 
            this.tspCarregarArquivo.AccessibleDescription = "Carrega o arquivo retornado pelo banco.";
            this.tspCarregarArquivo.AutoSize = false;
            this.tspCarregarArquivo.Enabled = false;
            this.tspCarregarArquivo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCarregarArquivo.Image = global::CRCorrea.Properties.Resources.Importar;
            this.tspCarregarArquivo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspCarregarArquivo.Name = "tspCarregarArquivo";
            this.tspCarregarArquivo.Size = new System.Drawing.Size(200, 42);
            this.tspCarregarArquivo.Text = "Carregar Arquivo para Integração";
            this.tspCarregarArquivo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspVisualizar
            // 
            this.tspVisualizar.AccessibleDescription = "Visualizar Boleto";
            this.tspVisualizar.AutoSize = false;
            this.tspVisualizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspVisualizar.Image = global::CRCorrea.Properties.Resources.Clientes;
            this.tspVisualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspVisualizar.Name = "tspVisualizar";
            this.tspVisualizar.Size = new System.Drawing.Size(120, 42);
            this.tspVisualizar.Text = "Visualizar Boleto";
            this.tspVisualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspVisualizar.Click += new System.EventHandler(this.tspVisualizar_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(90, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // gbxBoletoEnviar
            // 
            this.gbxBoletoEnviar.Controls.Add(this.dgvBoletoEnviar);
            this.gbxBoletoEnviar.Location = new System.Drawing.Point(12, 420);
            this.gbxBoletoEnviar.Name = "gbxBoletoEnviar";
            this.gbxBoletoEnviar.Size = new System.Drawing.Size(809, 210);
            this.gbxBoletoEnviar.TabIndex = 52;
            this.gbxBoletoEnviar.TabStop = false;
            this.gbxBoletoEnviar.Text = "Enviar";
            // 
            // dgvBoletoEnviar
            // 
            this.dgvBoletoEnviar.AllowUserToAddRows = false;
            this.dgvBoletoEnviar.AllowUserToDeleteRows = false;
            this.dgvBoletoEnviar.AllowUserToResizeRows = false;
            this.dgvBoletoEnviar.BackgroundColor = System.Drawing.Color.White;
            this.dgvBoletoEnviar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoletoEnviar.Location = new System.Drawing.Point(6, 20);
            this.dgvBoletoEnviar.MultiSelect = false;
            this.dgvBoletoEnviar.Name = "dgvBoletoEnviar";
            this.dgvBoletoEnviar.ReadOnly = true;
            this.dgvBoletoEnviar.RowHeadersVisible = false;
            this.dgvBoletoEnviar.Size = new System.Drawing.Size(793, 184);
            this.dgvBoletoEnviar.TabIndex = 2;
            // 
            // gbxBoletoNaoEnviado
            // 
            this.gbxBoletoNaoEnviado.Controls.Add(this.dgvBoletoNaoEnviado);
            this.gbxBoletoNaoEnviado.Location = new System.Drawing.Point(12, 254);
            this.gbxBoletoNaoEnviado.Name = "gbxBoletoNaoEnviado";
            this.gbxBoletoNaoEnviado.Size = new System.Drawing.Size(996, 160);
            this.gbxBoletoNaoEnviado.TabIndex = 51;
            this.gbxBoletoNaoEnviado.TabStop = false;
            this.gbxBoletoNaoEnviado.Text = "Sem emissão de boleto";
            // 
            // dgvBoletoNaoEnviado
            // 
            this.dgvBoletoNaoEnviado.AllowUserToAddRows = false;
            this.dgvBoletoNaoEnviado.AllowUserToDeleteRows = false;
            this.dgvBoletoNaoEnviado.AllowUserToResizeRows = false;
            this.dgvBoletoNaoEnviado.BackgroundColor = System.Drawing.Color.White;
            this.dgvBoletoNaoEnviado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoletoNaoEnviado.Location = new System.Drawing.Point(6, 20);
            this.dgvBoletoNaoEnviado.MultiSelect = false;
            this.dgvBoletoNaoEnviado.Name = "dgvBoletoNaoEnviado";
            this.dgvBoletoNaoEnviado.ReadOnly = true;
            this.dgvBoletoNaoEnviado.RowHeadersVisible = false;
            this.dgvBoletoNaoEnviado.Size = new System.Drawing.Size(984, 134);
            this.dgvBoletoNaoEnviado.TabIndex = 1;
            this.dgvBoletoNaoEnviado.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoletoNaoEnviado_CellDoubleClick);
            // 
            // gbxConta
            // 
            this.gbxConta.AccessibleDescription = "";
            this.gbxConta.Controls.Add(this.tbxTab_BancoNomeDestino);
            this.gbxConta.Controls.Add(this.label27);
            this.gbxConta.Controls.Add(this.tbxTab_BancoCodigoDestino);
            this.gbxConta.Controls.Add(this.chxBancosTodos);
            this.gbxConta.Controls.Add(this.tbxNroNota);
            this.gbxConta.Controls.Add(this.label111);
            this.gbxConta.Controls.Add(this.label106);
            this.gbxConta.Controls.Add(this.btnBancosDestino);
            this.gbxConta.Controls.Add(this.tbxBancosNomeDestino);
            this.gbxConta.Controls.Add(this.tbxBancosCodigoDestino);
            this.gbxConta.Controls.Add(this.label9);
            this.gbxConta.Controls.Add(this.btnBancos);
            this.gbxConta.Controls.Add(this.tbxBancosNome);
            this.gbxConta.Controls.Add(this.tbxBancosCodigo);
            this.gbxConta.Controls.Add(this.label105);
            this.gbxConta.Controls.Add(this.tbxTab_BancoNome);
            this.gbxConta.Controls.Add(this.tbxTab_BancoCodigo);
            this.gbxConta.Location = new System.Drawing.Point(12, 6);
            this.gbxConta.Name = "gbxConta";
            this.gbxConta.Size = new System.Drawing.Size(893, 76);
            this.gbxConta.TabIndex = 49;
            this.gbxConta.TabStop = false;
            this.gbxConta.Text = "Contas";
            // 
            // tbxTab_BancoNomeDestino
            // 
            this.tbxTab_BancoNomeDestino.AccessibleDescription = "Cognome do banco Febraban";
            this.tbxTab_BancoNomeDestino.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTab_BancoNomeDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTab_BancoNomeDestino.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTab_BancoNomeDestino.Location = new System.Drawing.Point(484, 47);
            this.tbxTab_BancoNomeDestino.MaxLength = 20;
            this.tbxTab_BancoNomeDestino.Name = "tbxTab_BancoNomeDestino";
            this.tbxTab_BancoNomeDestino.ReadOnly = true;
            this.tbxTab_BancoNomeDestino.Size = new System.Drawing.Size(156, 21);
            this.tbxTab_BancoNomeDestino.TabIndex = 9;
            this.tbxTab_BancoNomeDestino.TabStop = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(326, 51);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(89, 13);
            this.label27.TabIndex = 38;
            this.label27.Text = "Banco Febraban:";
            // 
            // tbxTab_BancoCodigoDestino
            // 
            this.tbxTab_BancoCodigoDestino.AccessibleDescription = "Numero do Banco Febraban Destino";
            this.tbxTab_BancoCodigoDestino.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTab_BancoCodigoDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTab_BancoCodigoDestino.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTab_BancoCodigoDestino.Location = new System.Drawing.Point(421, 47);
            this.tbxTab_BancoCodigoDestino.MaxLength = 50;
            this.tbxTab_BancoCodigoDestino.Name = "tbxTab_BancoCodigoDestino";
            this.tbxTab_BancoCodigoDestino.ReadOnly = true;
            this.tbxTab_BancoCodigoDestino.Size = new System.Drawing.Size(57, 21);
            this.tbxTab_BancoCodigoDestino.TabIndex = 8;
            this.tbxTab_BancoCodigoDestino.TabStop = false;
            this.tbxTab_BancoCodigoDestino.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chxBancosTodos
            // 
            this.chxBancosTodos.AccessibleDescription = "Todos os Bancos";
            this.chxBancosTodos.AccessibleName = "Localizar todos os Boletos sem Banco Indicado";
            this.chxBancosTodos.AutoSize = true;
            this.chxBancosTodos.Location = new System.Drawing.Point(648, 49);
            this.chxBancosTodos.Name = "chxBancosTodos";
            this.chxBancosTodos.Size = new System.Drawing.Size(236, 17);
            this.chxBancosTodos.TabIndex = 11;
            this.chxBancosTodos.Text = "Apenas duplicatas Banco Febraban indicado";
            this.chxBancosTodos.UseVisualStyleBackColor = true;
            this.chxBancosTodos.Enter += new System.EventHandler(this.ControlEnter);
            this.chxBancosTodos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.chxBancosTodos.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxNroNota
            // 
            this.tbxNroNota.AccessibleDescription = "Numero da Nota Fiscal";
            this.tbxNroNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNroNota.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNroNota.Location = new System.Drawing.Point(805, 19);
            this.tbxNroNota.MaxLength = 20;
            this.tbxNroNota.Name = "tbxNroNota";
            this.tbxNroNota.Size = new System.Drawing.Size(79, 21);
            this.tbxNroNota.TabIndex = 10;
            this.tbxNroNota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxNroNota.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNroNota.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNroNota.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(682, 22);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(117, 13);
            this.label111.TabIndex = 36;
            this.label111.Text = "A partir da Nota Fiscal:";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(336, 23);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(79, 13);
            this.label106.TabIndex = 35;
            this.label106.Text = "Conta Destino:";
            // 
            // btnBancosDestino
            // 
            this.btnBancosDestino.AccessibleDescription = "Contas Aplibank Destino";
            this.btnBancosDestino.Image = ((System.Drawing.Image)(resources.GetObject("btnBancosDestino.Image")));
            this.btnBancosDestino.Location = new System.Drawing.Point(455, 20);
            this.btnBancosDestino.Name = "btnBancosDestino";
            this.btnBancosDestino.Size = new System.Drawing.Size(23, 21);
            this.btnBancosDestino.TabIndex = 6;
            this.btnBancosDestino.TabStop = false;
            this.btnBancosDestino.UseVisualStyleBackColor = true;
            this.btnBancosDestino.Click += new System.EventHandler(this.btnBancosDestino_Click);
            // 
            // tbxBancosNomeDestino
            // 
            this.tbxBancosNomeDestino.AccessibleDescription = "Cognome do Aplibank Destino";
            this.tbxBancosNomeDestino.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxBancosNomeDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxBancosNomeDestino.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBancosNomeDestino.Location = new System.Drawing.Point(484, 19);
            this.tbxBancosNomeDestino.MaxLength = 20;
            this.tbxBancosNomeDestino.Name = "tbxBancosNomeDestino";
            this.tbxBancosNomeDestino.ReadOnly = true;
            this.tbxBancosNomeDestino.Size = new System.Drawing.Size(156, 21);
            this.tbxBancosNomeDestino.TabIndex = 7;
            this.tbxBancosNomeDestino.TabStop = false;
            // 
            // tbxBancosCodigoDestino
            // 
            this.tbxBancosCodigoDestino.AccessibleDescription = "Nro Conta Aplibank para onde vai enviar as duplicatas";
            this.tbxBancosCodigoDestino.BackColor = System.Drawing.Color.LightGray;
            this.tbxBancosCodigoDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxBancosCodigoDestino.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBancosCodigoDestino.Location = new System.Drawing.Point(421, 20);
            this.tbxBancosCodigoDestino.MaxLength = 50;
            this.tbxBancosCodigoDestino.Name = "tbxBancosCodigoDestino";
            this.tbxBancosCodigoDestino.Size = new System.Drawing.Size(28, 21);
            this.tbxBancosCodigoDestino.TabIndex = 5;
            this.tbxBancosCodigoDestino.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxBancosCodigoDestino.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxBancosCodigoDestino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxBancosCodigoDestino.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Conta Aplibank:";
            // 
            // btnBancos
            // 
            this.btnBancos.AccessibleDescription = "Contas Aplibank";
            this.btnBancos.Image = ((System.Drawing.Image)(resources.GetObject("btnBancos.Image")));
            this.btnBancos.Location = new System.Drawing.Point(135, 19);
            this.btnBancos.Name = "btnBancos";
            this.btnBancos.Size = new System.Drawing.Size(23, 21);
            this.btnBancos.TabIndex = 1;
            this.btnBancos.TabStop = false;
            this.btnBancos.UseVisualStyleBackColor = true;
            this.btnBancos.Click += new System.EventHandler(this.btnBancos_Click);
            // 
            // tbxBancosNome
            // 
            this.tbxBancosNome.AccessibleDescription = "Cognome do Aplibank para Achar as Duplicatas";
            this.tbxBancosNome.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxBancosNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxBancosNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBancosNome.Location = new System.Drawing.Point(164, 20);
            this.tbxBancosNome.MaxLength = 20;
            this.tbxBancosNome.Name = "tbxBancosNome";
            this.tbxBancosNome.ReadOnly = true;
            this.tbxBancosNome.Size = new System.Drawing.Size(156, 21);
            this.tbxBancosNome.TabIndex = 2;
            this.tbxBancosNome.TabStop = false;
            // 
            // tbxBancosCodigo
            // 
            this.tbxBancosCodigo.AccessibleDescription = "Nro Conta Aplibank para Analisar as Duplicatas a Enviar";
            this.tbxBancosCodigo.BackColor = System.Drawing.Color.LightGray;
            this.tbxBancosCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxBancosCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBancosCodigo.Location = new System.Drawing.Point(101, 19);
            this.tbxBancosCodigo.MaxLength = 50;
            this.tbxBancosCodigo.Name = "tbxBancosCodigo";
            this.tbxBancosCodigo.Size = new System.Drawing.Size(28, 21);
            this.tbxBancosCodigo.TabIndex = 0;
            this.tbxBancosCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxBancosCodigo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxBancosCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxBancosCodigo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(6, 49);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(89, 13);
            this.label105.TabIndex = 27;
            this.label105.Text = "Banco Febraban:";
            // 
            // tbxTab_BancoNome
            // 
            this.tbxTab_BancoNome.AccessibleDescription = "Cognome do banco Febraban";
            this.tbxTab_BancoNome.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTab_BancoNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTab_BancoNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTab_BancoNome.Location = new System.Drawing.Point(164, 47);
            this.tbxTab_BancoNome.MaxLength = 20;
            this.tbxTab_BancoNome.Name = "tbxTab_BancoNome";
            this.tbxTab_BancoNome.ReadOnly = true;
            this.tbxTab_BancoNome.Size = new System.Drawing.Size(156, 21);
            this.tbxTab_BancoNome.TabIndex = 4;
            this.tbxTab_BancoNome.TabStop = false;
            // 
            // tbxTab_BancoCodigo
            // 
            this.tbxTab_BancoCodigo.AccessibleDescription = "Numero do Banco Febraban";
            this.tbxTab_BancoCodigo.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTab_BancoCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTab_BancoCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTab_BancoCodigo.Location = new System.Drawing.Point(101, 46);
            this.tbxTab_BancoCodigo.MaxLength = 50;
            this.tbxTab_BancoCodigo.Name = "tbxTab_BancoCodigo";
            this.tbxTab_BancoCodigo.ReadOnly = true;
            this.tbxTab_BancoCodigo.Size = new System.Drawing.Size(57, 21);
            this.tbxTab_BancoCodigo.TabIndex = 3;
            this.tbxTab_BancoCodigo.TabStop = false;
            this.tbxTab_BancoCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gbxBoletoEnviado
            // 
            this.gbxBoletoEnviado.Controls.Add(this.dgvBoletoEnviado);
            this.gbxBoletoEnviado.Location = new System.Drawing.Point(12, 88);
            this.gbxBoletoEnviado.Name = "gbxBoletoEnviado";
            this.gbxBoletoEnviado.Size = new System.Drawing.Size(996, 160);
            this.gbxBoletoEnviado.TabIndex = 50;
            this.gbxBoletoEnviado.TabStop = false;
            this.gbxBoletoEnviado.Text = "Boletos enviados";
            // 
            // dgvBoletoEnviado
            // 
            this.dgvBoletoEnviado.AllowUserToAddRows = false;
            this.dgvBoletoEnviado.AllowUserToDeleteRows = false;
            this.dgvBoletoEnviado.AllowUserToResizeRows = false;
            this.dgvBoletoEnviado.BackgroundColor = System.Drawing.Color.White;
            this.dgvBoletoEnviado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoletoEnviado.Location = new System.Drawing.Point(6, 20);
            this.dgvBoletoEnviado.MultiSelect = false;
            this.dgvBoletoEnviado.Name = "dgvBoletoEnviado";
            this.dgvBoletoEnviado.ReadOnly = true;
            this.dgvBoletoEnviado.RowHeadersVisible = false;
            this.dgvBoletoEnviado.Size = new System.Drawing.Size(984, 134);
            this.dgvBoletoEnviado.TabIndex = 0;
            // 
            // gbxTipoEnvio
            // 
            this.gbxTipoEnvio.AccessibleDescription = "Tipo de Envio das Duplicatas";
            this.gbxTipoEnvio.Controls.Add(this.rbnEnviarLote);
            this.gbxTipoEnvio.Controls.Add(this.rbnEnviarIndividual);
            this.gbxTipoEnvio.Location = new System.Drawing.Point(911, 6);
            this.gbxTipoEnvio.Name = "gbxTipoEnvio";
            this.gbxTipoEnvio.Size = new System.Drawing.Size(97, 76);
            this.gbxTipoEnvio.TabIndex = 55;
            this.gbxTipoEnvio.TabStop = false;
            this.gbxTipoEnvio.Text = "Modo de Envio";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Arquivos de Retorno (*.ret)|*.ret|Todos Arquivos (*.*)|*.*";
            // 
            // frmReceberEnviarBancoBol
            // 
            this.AccessibleDescription = "Enviar Duplicatas para os Bancos Febraban";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxTipoEnvio);
            this.Controls.Add(this.gbxConfiguracao);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxBoletoEnviar);
            this.Controls.Add(this.gbxBoletoNaoEnviado);
            this.Controls.Add(this.gbxConta);
            this.Controls.Add(this.gbxBoletoEnviado);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReceberEnviarBancoBol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "581";
            this.Text = "Banco Envio de  Duplicatas - clsVisualizando";
            this.toolTip1.SetToolTip(this, "Banco Envio de  Duplicatas - clsVisualizando");
            this.Activated += new System.EventHandler(this.frmReceberEnviarBancoBol_Activated);
            this.Load += new System.EventHandler(this.frmReceberEnviarBancoBol_Load);
            this.gbxConfiguracao.ResumeLayout(false);
            this.gbxConfiguracao.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxBoletoEnviar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletoEnviar)).EndInit();
            this.gbxBoletoNaoEnviado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletoNaoEnviado)).EndInit();
            this.gbxConta.ResumeLayout(false);
            this.gbxConta.PerformLayout();
            this.gbxBoletoEnviado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletoEnviado)).EndInit();
            this.gbxTipoEnvio.ResumeLayout(false);
            this.gbxTipoEnvio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbxConfiguracao;
        private System.Windows.Forms.TextBox tbxBoletolinha01;
        private System.Windows.Forms.TextBox tbxProtestar;
        private System.Windows.Forms.TextBox tbxJurosMes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspCarregarArquivo;
        private System.Windows.Forms.ToolStripButton tspGerarArquivo;
        private System.Windows.Forms.ToolStripButton tspVisualizar;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxBoletoEnviar;
        private System.Windows.Forms.DataGridView dgvBoletoEnviar;
        private System.Windows.Forms.GroupBox gbxBoletoNaoEnviado;
        private System.Windows.Forms.DataGridView dgvBoletoNaoEnviado;
        private System.Windows.Forms.GroupBox gbxConta;
        private System.Windows.Forms.TextBox tbxTab_BancoNomeDestino;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tbxTab_BancoCodigoDestino;
        private System.Windows.Forms.CheckBox chxBancosTodos;
        private System.Windows.Forms.TextBox tbxNroNota;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Button btnBancosDestino;
        private System.Windows.Forms.TextBox tbxBancosNomeDestino;
        private System.Windows.Forms.TextBox tbxBancosCodigoDestino;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnBancos;
        private System.Windows.Forms.TextBox tbxBancosNome;
        private System.Windows.Forms.TextBox tbxBancosCodigo;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.TextBox tbxTab_BancoNome;
        private System.Windows.Forms.TextBox tbxTab_BancoCodigo;
        private System.Windows.Forms.GroupBox gbxBoletoEnviado;
        private System.Windows.Forms.DataGridView dgvBoletoEnviado;
        private System.Windows.Forms.GroupBox gbxTipoEnvio;
        private System.Windows.Forms.RadioButton rbnEnviarLote;
        private System.Windows.Forms.RadioButton rbnEnviarIndividual;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}