namespace CRCorrea
{
    partial class frmCotacaoAprovaVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCotacaoAprovaVis));
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dgvCotacao = new System.Windows.Forms.DataGridView();
            this.gbxItensCotacao = new System.Windows.Forms.GroupBox();
            this.gbxStatusMotivos = new System.Windows.Forms.GroupBox();
            this.btnEnviarNao = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.gbxMotivo = new System.Windows.Forms.GroupBox();
            this.tbxMotivoReprovado = new System.Windows.Forms.TextBox();
            this.gbxJustificativa = new System.Windows.Forms.GroupBox();
            this.tbxRespostaComprador = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxAutorizante = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxTermino = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxTudoFechado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxDataFechamento = new System.Windows.Forms.TextBox();
            this.tbxComprador = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxDataMontagem = new System.Windows.Forms.TextBox();
            this.tbxNumero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxObservacao = new System.Windows.Forms.TextBox();
            this.btnDevolver = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.tbxTotalPrevisto = new System.Windows.Forms.TextBox();
            this.dgvCotacao1 = new System.Windows.Forms.DataGridView();
            this.gbxCotacao = new System.Windows.Forms.GroupBox();
            this.tspCotacao = new System.Windows.Forms.ToolStrip();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCotacao2 = new System.Windows.Forms.DataGridView();
            this.gbxCotacaoReprovadas = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.dgvCotacaoReprovadas = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAprovaCotacao = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao)).BeginInit();
            this.gbxItensCotacao.SuspendLayout();
            this.gbxStatusMotivos.SuspendLayout();
            this.gbxMotivo.SuspendLayout();
            this.gbxJustificativa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao1)).BeginInit();
            this.gbxCotacao.SuspendLayout();
            this.tspCotacao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao2)).BeginInit();
            this.gbxCotacaoReprovadas.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacaoReprovadas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCotacao
            // 
            this.dgvCotacao.AccessibleDescription = "Cotações";
            this.dgvCotacao.AllowUserToAddRows = false;
            this.dgvCotacao.AllowUserToDeleteRows = false;
            this.dgvCotacao.AllowUserToResizeRows = false;
            this.dgvCotacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCotacao.BackgroundColor = System.Drawing.Color.White;
            this.dgvCotacao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCotacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvCotacao.Location = new System.Drawing.Point(6, 19);
            this.dgvCotacao.MultiSelect = false;
            this.dgvCotacao.Name = "dgvCotacao";
            this.dgvCotacao.ReadOnly = true;
            this.dgvCotacao.RowHeadersVisible = false;
            this.dgvCotacao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCotacao.Size = new System.Drawing.Size(304, 304);
            this.dgvCotacao.TabIndex = 125;
            this.dgvCotacao.TabStop = false;
            this.ToolTip.SetToolTip(this.dgvCotacao, "Visualizar Cotação");
            this.dgvCotacao.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvCotacao_MouseClick);
            // 
            // gbxItensCotacao
            // 
            this.gbxItensCotacao.Controls.Add(this.btnAprovaCotacao);
            this.gbxItensCotacao.Controls.Add(this.gbxStatusMotivos);
            this.gbxItensCotacao.Controls.Add(this.label11);
            this.gbxItensCotacao.Controls.Add(this.label3);
            this.gbxItensCotacao.Controls.Add(this.tbxAutorizante);
            this.gbxItensCotacao.Controls.Add(this.label8);
            this.gbxItensCotacao.Controls.Add(this.tbxTermino);
            this.gbxItensCotacao.Controls.Add(this.label5);
            this.gbxItensCotacao.Controls.Add(this.tbxTudoFechado);
            this.gbxItensCotacao.Controls.Add(this.label4);
            this.gbxItensCotacao.Controls.Add(this.tbxDataFechamento);
            this.gbxItensCotacao.Controls.Add(this.tbxComprador);
            this.gbxItensCotacao.Controls.Add(this.label6);
            this.gbxItensCotacao.Controls.Add(this.label9);
            this.gbxItensCotacao.Controls.Add(this.tbxDataMontagem);
            this.gbxItensCotacao.Controls.Add(this.tbxNumero);
            this.gbxItensCotacao.Controls.Add(this.label2);
            this.gbxItensCotacao.Controls.Add(this.tbxObservacao);
            this.gbxItensCotacao.Controls.Add(this.btnDevolver);
            this.gbxItensCotacao.Controls.Add(this.label25);
            this.gbxItensCotacao.Controls.Add(this.tbxTotalPrevisto);
            this.gbxItensCotacao.Controls.Add(this.dgvCotacao1);
            this.gbxItensCotacao.Location = new System.Drawing.Point(324, 43);
            this.gbxItensCotacao.Name = "gbxItensCotacao";
            this.gbxItensCotacao.Size = new System.Drawing.Size(683, 404);
            this.gbxItensCotacao.TabIndex = 2;
            this.gbxItensCotacao.TabStop = false;
            this.gbxItensCotacao.Text = "Itens da Cotaçao";
            this.ToolTip.SetToolTip(this.gbxItensCotacao, "Grupo Itens da Cotração");
            // 
            // gbxStatusMotivos
            // 
            this.gbxStatusMotivos.AccessibleDescription = "Motivo Devolução ou Justificativa";
            this.gbxStatusMotivos.Controls.Add(this.btnEnviarNao);
            this.gbxStatusMotivos.Controls.Add(this.btnEnviar);
            this.gbxStatusMotivos.Controls.Add(this.gbxMotivo);
            this.gbxStatusMotivos.Controls.Add(this.gbxJustificativa);
            this.gbxStatusMotivos.Location = new System.Drawing.Point(39, 137);
            this.gbxStatusMotivos.Name = "gbxStatusMotivos";
            this.gbxStatusMotivos.Size = new System.Drawing.Size(603, 207);
            this.gbxStatusMotivos.TabIndex = 140;
            this.gbxStatusMotivos.TabStop = false;
            this.gbxStatusMotivos.Text = "Status Motivos";
            this.ToolTip.SetToolTip(this.gbxStatusMotivos, "Motivo Devolução ou Justificativa");
            this.gbxStatusMotivos.Visible = false;
            // 
            // btnEnviarNao
            // 
            this.btnEnviarNao.AccessibleDescription = "Não confirmar a Devolução para Compras";
            this.btnEnviarNao.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEnviarNao.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarNao.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviarNao.Image")));
            this.btnEnviarNao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviarNao.Location = new System.Drawing.Point(291, 175);
            this.btnEnviarNao.Name = "btnEnviarNao";
            this.btnEnviarNao.Size = new System.Drawing.Size(112, 23);
            this.btnEnviarNao.TabIndex = 137;
            this.btnEnviarNao.Text = "Não - Retorne";
            this.btnEnviarNao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTip.SetToolTip(this.btnEnviarNao, "Não confirmar a Devolução para Compras");
            this.btnEnviarNao.UseVisualStyleBackColor = true;
            this.btnEnviarNao.Click += new System.EventHandler(this.btnEnviarNao_Click);
            // 
            // btnEnviar
            // 
            this.btnEnviar.AccessibleDescription = "Confirmar a Devolução para Compras";
            this.btnEnviar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEnviar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviar.Image")));
            this.btnEnviar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviar.Location = new System.Drawing.Point(417, 175);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(160, 23);
            this.btnEnviar.TabIndex = 136;
            this.btnEnviar.Text = "Ok Confirma Devolução";
            this.btnEnviar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTip.SetToolTip(this.btnEnviar, "Confirmar a Devolução para Compras");
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // gbxMotivo
            // 
            this.gbxMotivo.AccessibleDescription = "Motivo da Reprovação";
            this.gbxMotivo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxMotivo.Controls.Add(this.tbxMotivoReprovado);
            this.gbxMotivo.Location = new System.Drawing.Point(241, 20);
            this.gbxMotivo.Name = "gbxMotivo";
            this.gbxMotivo.Size = new System.Drawing.Size(355, 147);
            this.gbxMotivo.TabIndex = 134;
            this.gbxMotivo.TabStop = false;
            this.gbxMotivo.Text = "Motivo da Reprovação";
            this.ToolTip.SetToolTip(this.gbxMotivo, "Motivo da Reprovação");
            // 
            // tbxMotivoReprovado
            // 
            this.tbxMotivoReprovado.AccessibleDescription = "Motivo da Reprovação";
            this.tbxMotivoReprovado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxMotivoReprovado.BackColor = System.Drawing.Color.White;
            this.tbxMotivoReprovado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMotivoReprovado.Location = new System.Drawing.Point(6, 20);
            this.tbxMotivoReprovado.Multiline = true;
            this.tbxMotivoReprovado.Name = "tbxMotivoReprovado";
            this.tbxMotivoReprovado.Size = new System.Drawing.Size(343, 121);
            this.tbxMotivoReprovado.TabIndex = 121;
            this.ToolTip.SetToolTip(this.tbxMotivoReprovado, "Motivo da Reprovação");
            this.tbxMotivoReprovado.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxMotivoReprovado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxMotivoReprovado.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxJustificativa
            // 
            this.gbxJustificativa.AccessibleDescription = "Justificativa do Comprador";
            this.gbxJustificativa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxJustificativa.Controls.Add(this.tbxRespostaComprador);
            this.gbxJustificativa.Location = new System.Drawing.Point(6, 20);
            this.gbxJustificativa.Name = "gbxJustificativa";
            this.gbxJustificativa.Size = new System.Drawing.Size(232, 147);
            this.gbxJustificativa.TabIndex = 133;
            this.gbxJustificativa.TabStop = false;
            this.gbxJustificativa.Text = "Justificando a Resposta Comprador";
            this.ToolTip.SetToolTip(this.gbxJustificativa, "Justificativa do Comprador");
            // 
            // tbxRespostaComprador
            // 
            this.tbxRespostaComprador.AccessibleDescription = "Justificativa do Comprador";
            this.tbxRespostaComprador.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxRespostaComprador.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxRespostaComprador.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxRespostaComprador.Location = new System.Drawing.Point(6, 20);
            this.tbxRespostaComprador.Multiline = true;
            this.tbxRespostaComprador.Name = "tbxRespostaComprador";
            this.tbxRespostaComprador.ReadOnly = true;
            this.tbxRespostaComprador.Size = new System.Drawing.Size(220, 121);
            this.tbxRespostaComprador.TabIndex = 121;
            this.ToolTip.SetToolTip(this.tbxRespostaComprador, "Resposta Justificando");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 183;
            this.label11.Text = "Número";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(526, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 181;
            this.label3.Text = "Autorizante";
            // 
            // tbxAutorizante
            // 
            this.tbxAutorizante.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAutorizante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxAutorizante.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAutorizante.Location = new System.Drawing.Point(494, 31);
            this.tbxAutorizante.Name = "tbxAutorizante";
            this.tbxAutorizante.ReadOnly = true;
            this.tbxAutorizante.Size = new System.Drawing.Size(148, 18);
            this.tbxAutorizante.TabIndex = 180;
            this.tbxAutorizante.TabStop = false;
            this.ToolTip.SetToolTip(this.tbxAutorizante, "Autorizante");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(636, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 179;
            this.label8.Text = "Status";
            // 
            // tbxTermino
            // 
            this.tbxTermino.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTermino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTermino.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTermino.Location = new System.Drawing.Point(645, 31);
            this.tbxTermino.Name = "tbxTermino";
            this.tbxTermino.Size = new System.Drawing.Size(26, 18);
            this.tbxTermino.TabIndex = 178;
            this.tbxTermino.Tag = "";
            this.tbxTermino.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.tbxTermino, "Termino");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(282, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 175;
            this.label5.Text = "Data Término";
            // 
            // tbxTudoFechado
            // 
            this.tbxTudoFechado.AccessibleDescription = "Data de Fechamento Final da Cotação";
            this.tbxTudoFechado.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTudoFechado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTudoFechado.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTudoFechado.Location = new System.Drawing.Point(279, 31);
            this.tbxTudoFechado.Name = "tbxTudoFechado";
            this.tbxTudoFechado.ReadOnly = true;
            this.tbxTudoFechado.Size = new System.Drawing.Size(80, 18);
            this.tbxTudoFechado.TabIndex = 174;
            this.ToolTip.SetToolTip(this.tbxTudoFechado, "data Termino");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 173;
            this.label4.Text = "Data Fechamento";
            // 
            // tbxDataFechamento
            // 
            this.tbxDataFechamento.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxDataFechamento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDataFechamento.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataFechamento.Location = new System.Drawing.Point(165, 31);
            this.tbxDataFechamento.Name = "tbxDataFechamento";
            this.tbxDataFechamento.ReadOnly = true;
            this.tbxDataFechamento.Size = new System.Drawing.Size(110, 18);
            this.tbxDataFechamento.TabIndex = 172;
            this.ToolTip.SetToolTip(this.tbxDataFechamento, "Data Fechamento");
            // 
            // tbxComprador
            // 
            this.tbxComprador.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxComprador.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxComprador.Location = new System.Drawing.Point(364, 31);
            this.tbxComprador.Name = "tbxComprador";
            this.tbxComprador.ReadOnly = true;
            this.tbxComprador.Size = new System.Drawing.Size(128, 18);
            this.tbxComprador.TabIndex = 171;
            this.ToolTip.SetToolTip(this.tbxComprador, "Comprador Emitente");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(370, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 170;
            this.label6.Text = "Comprador Emitente";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(74, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 169;
            this.label9.Text = "Data Emissão";
            // 
            // tbxDataMontagem
            // 
            this.tbxDataMontagem.AccessibleDescription = "Data de Emissão";
            this.tbxDataMontagem.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxDataMontagem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDataMontagem.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataMontagem.Location = new System.Drawing.Point(51, 31);
            this.tbxDataMontagem.Name = "tbxDataMontagem";
            this.tbxDataMontagem.ReadOnly = true;
            this.tbxDataMontagem.Size = new System.Drawing.Size(110, 18);
            this.tbxDataMontagem.TabIndex = 168;
            this.tbxDataMontagem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.tbxDataMontagem, "Data Emissão");
            // 
            // tbxNumero
            // 
            this.tbxNumero.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNumero.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNumero.Location = new System.Drawing.Point(4, 31);
            this.tbxNumero.Name = "tbxNumero";
            this.tbxNumero.ReadOnly = true;
            this.tbxNumero.Size = new System.Drawing.Size(43, 18);
            this.tbxNumero.TabIndex = 167;
            this.tbxNumero.Tag = "";
            this.tbxNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.tbxNumero, "Nro Cotação");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 139;
            this.label2.Text = "Observação:";
            // 
            // tbxObservacao
            // 
            this.tbxObservacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxObservacao.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxObservacao.Location = new System.Drawing.Point(80, 58);
            this.tbxObservacao.Name = "tbxObservacao";
            this.tbxObservacao.Size = new System.Drawing.Size(590, 18);
            this.tbxObservacao.TabIndex = 140;
            this.ToolTip.SetToolTip(this.tbxObservacao, "Observação");
            // 
            // btnDevolver
            // 
            this.btnDevolver.AccessibleDescription = "Devolver a Cotação para Compras";
            this.btnDevolver.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDevolver.Image = ((System.Drawing.Image)(resources.GetObject("btnDevolver.Image")));
            this.btnDevolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDevolver.Location = new System.Drawing.Point(6, 373);
            this.btnDevolver.Name = "btnDevolver";
            this.btnDevolver.Size = new System.Drawing.Size(213, 24);
            this.btnDevolver.TabIndex = 135;
            this.btnDevolver.Text = "Devolver Cotação para Compras";
            this.btnDevolver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTip.SetToolTip(this.btnDevolver, "Devolver Cotação para Compras");
            this.btnDevolver.UseVisualStyleBackColor = true;
            this.btnDevolver.Visible = false;
            this.btnDevolver.Click += new System.EventHandler(this.btnDevolver_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(425, 377);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(100, 14);
            this.label25.TabIndex = 132;
            this.label25.Text = "Total Previsto :";
            // 
            // tbxTotalPrevisto
            // 
            this.tbxTotalPrevisto.AccessibleDescription = "Total Previsto Cotação";
            this.tbxTotalPrevisto.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTotalPrevisto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTotalPrevisto.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalPrevisto.Location = new System.Drawing.Point(531, 373);
            this.tbxTotalPrevisto.Name = "tbxTotalPrevisto";
            this.tbxTotalPrevisto.ReadOnly = true;
            this.tbxTotalPrevisto.Size = new System.Drawing.Size(98, 23);
            this.tbxTotalPrevisto.TabIndex = 133;
            this.tbxTotalPrevisto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ToolTip.SetToolTip(this.tbxTotalPrevisto, "Total Previsto");
            // 
            // dgvCotacao1
            // 
            this.dgvCotacao1.AccessibleDescription = "Itens da Cotação";
            this.dgvCotacao1.AllowUserToAddRows = false;
            this.dgvCotacao1.AllowUserToDeleteRows = false;
            this.dgvCotacao1.AllowUserToResizeRows = false;
            this.dgvCotacao1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCotacao1.BackgroundColor = System.Drawing.Color.White;
            this.dgvCotacao1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCotacao1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvCotacao1.Location = new System.Drawing.Point(6, 84);
            this.dgvCotacao1.MultiSelect = false;
            this.dgvCotacao1.Name = "dgvCotacao1";
            this.dgvCotacao1.ReadOnly = true;
            this.dgvCotacao1.RowHeadersVisible = false;
            this.dgvCotacao1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCotacao1.Size = new System.Drawing.Size(670, 281);
            this.dgvCotacao1.TabIndex = 125;
            this.dgvCotacao1.TabStop = false;
            this.ToolTip.SetToolTip(this.dgvCotacao1, "Itens da Cotação");
            this.dgvCotacao1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvCotacao1_MouseClick);
            this.dgvCotacao1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvCotacao1_MouseDoubleClick);
            // 
            // gbxCotacao
            // 
            this.gbxCotacao.AccessibleDescription = "Cotações Aguardando Aprovação";
            this.gbxCotacao.Controls.Add(this.tspCotacao);
            this.gbxCotacao.Controls.Add(this.dgvCotacao);
            this.gbxCotacao.Location = new System.Drawing.Point(6, 43);
            this.gbxCotacao.Name = "gbxCotacao";
            this.gbxCotacao.Size = new System.Drawing.Size(315, 381);
            this.gbxCotacao.TabIndex = 1;
            this.gbxCotacao.TabStop = false;
            this.gbxCotacao.Text = "Cotações Aguardando Aprovação";
            this.ToolTip.SetToolTip(this.gbxCotacao, "Cotações Aguardando Aprovação");
            // 
            // tspCotacao
            // 
            this.tspCotacao.AllowMerge = false;
            this.tspCotacao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspCotacao.AutoSize = false;
            this.tspCotacao.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tspCotacao.Dock = System.Windows.Forms.DockStyle.None;
            this.tspCotacao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspAlterar,
            this.tspRetornar,
            this.toolStripSeparator1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspCotacao.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspCotacao.Location = new System.Drawing.Point(7, 331);
            this.tspCotacao.Name = "tspCotacao";
            this.tspCotacao.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspCotacao.Size = new System.Drawing.Size(307, 41);
            this.tspCotacao.TabIndex = 132;
            // 
            // tspAlterar
            // 
            this.tspAlterar.AutoSize = false;
            this.tspAlterar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAlterar.ForeColor = System.Drawing.Color.Black;
            this.tspAlterar.Image = ((System.Drawing.Image)(resources.GetObject("tspAlterar.Image")));
            this.tspAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAlterar.Name = "tspAlterar";
            this.tspAlterar.Size = new System.Drawing.Size(54, 31);
            this.tspAlterar.Text = "Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.ForeColor = System.Drawing.Color.Black;
            this.tspRetornar.Image = global::CRCorrea.Properties.Resources.Retornar;
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(54, 31);
            this.tspRetornar.Text = "Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 47);
            // 
            // tslbLocalizar
            // 
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(50, 11);
            this.tslbLocalizar.Text = "Procurar";
            // 
            // tstbxLocalizar
            // 
            this.tstbxLocalizar.AutoSize = false;
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(100, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCotacao2);
            this.groupBox1.Location = new System.Drawing.Point(325, 445);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(683, 190);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item por Fornecedor";
            this.ToolTip.SetToolTip(this.groupBox1, "Grupo das Solicitações");
            // 
            // dgvCotacao2
            // 
            this.dgvCotacao2.AccessibleDescription = "Fornecedores do Item";
            this.dgvCotacao2.AllowUserToAddRows = false;
            this.dgvCotacao2.AllowUserToDeleteRows = false;
            this.dgvCotacao2.AllowUserToResizeRows = false;
            this.dgvCotacao2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCotacao2.BackgroundColor = System.Drawing.Color.White;
            this.dgvCotacao2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCotacao2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvCotacao2.Location = new System.Drawing.Point(6, 16);
            this.dgvCotacao2.MultiSelect = false;
            this.dgvCotacao2.Name = "dgvCotacao2";
            this.dgvCotacao2.ReadOnly = true;
            this.dgvCotacao2.RowHeadersVisible = false;
            this.dgvCotacao2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCotacao2.Size = new System.Drawing.Size(670, 168);
            this.dgvCotacao2.TabIndex = 125;
            this.dgvCotacao2.TabStop = false;
            this.ToolTip.SetToolTip(this.dgvCotacao2, "Fornecedores do Item");
            // 
            // gbxCotacaoReprovadas
            // 
            this.gbxCotacaoReprovadas.AccessibleDescription = "Cotações com Antecedente de Reprovação";
            this.gbxCotacaoReprovadas.Controls.Add(this.toolStrip1);
            this.gbxCotacaoReprovadas.Controls.Add(this.dgvCotacaoReprovadas);
            this.gbxCotacaoReprovadas.Location = new System.Drawing.Point(12, 434);
            this.gbxCotacaoReprovadas.Name = "gbxCotacaoReprovadas";
            this.gbxCotacaoReprovadas.Size = new System.Drawing.Size(307, 201);
            this.gbxCotacaoReprovadas.TabIndex = 5;
            this.gbxCotacaoReprovadas.TabStop = false;
            this.gbxCotacaoReprovadas.Text = "Cotações com Antecedente de Reprovação";
            this.ToolTip.SetToolTip(this.gbxCotacaoReprovadas, "Cotações com Antecedente de Reprovação");
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(2, 270);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(298, 35);
            this.toolStrip1.TabIndex = 132;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(42, 31);
            this.toolStripButton1.Text = "Incluir";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(46, 31);
            this.toolStripButton2.Text = "Alterar";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(54, 31);
            this.toolStripButton3.Text = "Procurar";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // dgvCotacaoReprovadas
            // 
            this.dgvCotacaoReprovadas.AccessibleDescription = "Cotações com Antecedente de Reprovação";
            this.dgvCotacaoReprovadas.AllowUserToAddRows = false;
            this.dgvCotacaoReprovadas.AllowUserToDeleteRows = false;
            this.dgvCotacaoReprovadas.AllowUserToResizeRows = false;
            this.dgvCotacaoReprovadas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCotacaoReprovadas.BackgroundColor = System.Drawing.Color.White;
            this.dgvCotacaoReprovadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCotacaoReprovadas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvCotacaoReprovadas.Location = new System.Drawing.Point(6, 20);
            this.dgvCotacaoReprovadas.MultiSelect = false;
            this.dgvCotacaoReprovadas.Name = "dgvCotacaoReprovadas";
            this.dgvCotacaoReprovadas.ReadOnly = true;
            this.dgvCotacaoReprovadas.RowHeadersVisible = false;
            this.dgvCotacaoReprovadas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCotacaoReprovadas.Size = new System.Drawing.Size(295, 175);
            this.dgvCotacaoReprovadas.TabIndex = 125;
            this.dgvCotacaoReprovadas.TabStop = false;
            this.ToolTip.SetToolTip(this.dgvCotacaoReprovadas, "Cotações com Antecedente de Reprovação");
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(989, 29);
            this.label1.TabIndex = 51;
            this.label1.Text = "Visualizando as Cotações de Materiais para Aprovação";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAprovaCotacao
            // 
            this.btnAprovaCotacao.AccessibleDescription = "Aprovar Para Emitir Pedido";
            this.btnAprovaCotacao.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAprovaCotacao.Image = ((System.Drawing.Image)(resources.GetObject("btnAprovaCotacao.Image")));
            this.btnAprovaCotacao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAprovaCotacao.Location = new System.Drawing.Point(225, 373);
            this.btnAprovaCotacao.Name = "btnAprovaCotacao";
            this.btnAprovaCotacao.Size = new System.Drawing.Size(186, 24);
            this.btnAprovaCotacao.TabIndex = 184;
            this.btnAprovaCotacao.Text = "Aprovar Para Emitir Pedido";
            this.btnAprovaCotacao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTip.SetToolTip(this.btnAprovaCotacao, "Aprovar Para Emitir Pedido");
            this.btnAprovaCotacao.UseVisualStyleBackColor = true;
            this.btnAprovaCotacao.Visible = false;
            this.btnAprovaCotacao.Click += new System.EventHandler(this.btnAprovaCotacao_Click);
            // 
            // frmCotacaoAprovaVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbxCotacaoReprovadas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxItensCotacao);
            this.Controls.Add(this.gbxCotacao);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCotacaoAprovaVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "34012";
            this.Text = "Cotação de Preços  Aprovação - Visualização";
            this.ToolTip.SetToolTip(this, "Cotação de Preços  Aprovação - Visualização");
            this.Activated += new System.EventHandler(this.frmCotacaoAprovaVis_Activated);
            this.Load += new System.EventHandler(this.frmCotacaoAprovaVis_Load);
            this.Shown += new System.EventHandler(this.frmCotacaoAprovaVis_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao)).EndInit();
            this.gbxItensCotacao.ResumeLayout(false);
            this.gbxItensCotacao.PerformLayout();
            this.gbxStatusMotivos.ResumeLayout(false);
            this.gbxMotivo.ResumeLayout(false);
            this.gbxMotivo.PerformLayout();
            this.gbxJustificativa.ResumeLayout(false);
            this.gbxJustificativa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao1)).EndInit();
            this.gbxCotacao.ResumeLayout(false);
            this.tspCotacao.ResumeLayout(false);
            this.tspCotacao.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao2)).EndInit();
            this.gbxCotacaoReprovadas.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacaoReprovadas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.GroupBox gbxCotacao;
        private System.Windows.Forms.DataGridView dgvCotacao;
        private System.Windows.Forms.GroupBox gbxItensCotacao;
        private System.Windows.Forms.DataGridView dgvCotacao1;
        private System.Windows.Forms.ToolStrip tspCotacao;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCotacao2;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbxTotalPrevisto;
        private System.Windows.Forms.Button btnDevolver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxObservacao;
        private System.Windows.Forms.GroupBox gbxCotacaoReprovadas;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.DataGridView dgvCotacaoReprovadas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxAutorizante;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxTermino;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxTudoFechado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxDataFechamento;
        private System.Windows.Forms.TextBox tbxComprador;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxDataMontagem;
        private System.Windows.Forms.TextBox tbxNumero;
        private System.Windows.Forms.GroupBox gbxStatusMotivos;
        private System.Windows.Forms.Button btnEnviarNao;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.GroupBox gbxMotivo;
        private System.Windows.Forms.TextBox tbxMotivoReprovado;
        private System.Windows.Forms.GroupBox gbxJustificativa;
        private System.Windows.Forms.TextBox tbxRespostaComprador;
        private System.Windows.Forms.Button btnAprovaCotacao;
    }
}