namespace CRCorrea
{
    partial class frmCotacaoVis
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
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dgvCotacao = new System.Windows.Forms.DataGridView();
            this.gbxItensCotacao = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnIdUsuario = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxAutorizante = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxTermino = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxAno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxTudoFechado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxDataFechamento = new System.Windows.Forms.TextBox();
            this.tbxComprador = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxDataMontagem = new System.Windows.Forms.TextBox();
            this.tbxNumero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxObservacao = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tbxTotalPrevisto = new System.Windows.Forms.TextBox();
            this.dgvCotacao1 = new System.Windows.Forms.DataGridView();
            this.gbxFornecedores = new System.Windows.Forms.GroupBox();
            this.dgvCotacao2fornecedor = new System.Windows.Forms.DataGridView();
            this.gbxCotacao = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxQtdeCotacoes = new System.Windows.Forms.TextBox();
            this.tspCotacao = new System.Windows.Forms.ToolStrip();
            this.tspCotacaoAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspCotacaoExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.rbnTerminoN = new System.Windows.Forms.RadioButton();
            this.rbnTodas = new System.Windows.Forms.RadioButton();
            this.rbnTerminoA = new System.Windows.Forms.RadioButton();
            this.gbxOpcoes = new System.Windows.Forms.GroupBox();
            this.rbnTerminoF = new System.Windows.Forms.RadioButton();
            this.rbnTerminoO = new System.Windows.Forms.RadioButton();
            this.rbnTerminoR = new System.Windows.Forms.RadioButton();
            this.tspCotacaoR = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.gbxStatusMotivos = new System.Windows.Forms.GroupBox();
            this.gbxMotivo = new System.Windows.Forms.GroupBox();
            this.tbxMotivoReprovado = new System.Windows.Forms.TextBox();
            this.gbxJustificativa = new System.Windows.Forms.GroupBox();
            this.tbxRespostaComprador = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tspCotacaoIncluir = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao)).BeginInit();
            this.gbxItensCotacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao1)).BeginInit();
            this.gbxFornecedores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao2fornecedor)).BeginInit();
            this.gbxCotacao.SuspendLayout();
            this.tspCotacao.SuspendLayout();
            this.gbxOpcoes.SuspendLayout();
            this.tspCotacaoR.SuspendLayout();
            this.gbxStatusMotivos.SuspendLayout();
            this.gbxMotivo.SuspendLayout();
            this.gbxJustificativa.SuspendLayout();
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
            this.dgvCotacao.Size = new System.Drawing.Size(294, 455);
            this.dgvCotacao.TabIndex = 125;
            this.dgvCotacao.TabStop = false;
            this.ToolTip.SetToolTip(this.dgvCotacao, "Cotação");
            this.dgvCotacao.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvCotacao_MouseClick);
            this.dgvCotacao.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvCotacao_MouseDoubleClick);
            // 
            // gbxItensCotacao
            // 
            this.gbxItensCotacao.Controls.Add(this.label11);
            this.gbxItensCotacao.Controls.Add(this.btnIdUsuario);
            this.gbxItensCotacao.Controls.Add(this.label1);
            this.gbxItensCotacao.Controls.Add(this.tbxAutorizante);
            this.gbxItensCotacao.Controls.Add(this.label8);
            this.gbxItensCotacao.Controls.Add(this.tbxTermino);
            this.gbxItensCotacao.Controls.Add(this.label7);
            this.gbxItensCotacao.Controls.Add(this.tbxAno);
            this.gbxItensCotacao.Controls.Add(this.label5);
            this.gbxItensCotacao.Controls.Add(this.tbxTudoFechado);
            this.gbxItensCotacao.Controls.Add(this.label4);
            this.gbxItensCotacao.Controls.Add(this.tbxDataFechamento);
            this.gbxItensCotacao.Controls.Add(this.tbxComprador);
            this.gbxItensCotacao.Controls.Add(this.label3);
            this.gbxItensCotacao.Controls.Add(this.label6);
            this.gbxItensCotacao.Controls.Add(this.tbxDataMontagem);
            this.gbxItensCotacao.Controls.Add(this.tbxNumero);
            this.gbxItensCotacao.Controls.Add(this.label2);
            this.gbxItensCotacao.Controls.Add(this.tbxObservacao);
            this.gbxItensCotacao.Controls.Add(this.label25);
            this.gbxItensCotacao.Controls.Add(this.tbxTotalPrevisto);
            this.gbxItensCotacao.Controls.Add(this.dgvCotacao1);
            this.gbxItensCotacao.Controls.Add(this.gbxFornecedores);
            this.gbxItensCotacao.Location = new System.Drawing.Point(325, 40);
            this.gbxItensCotacao.Name = "gbxItensCotacao";
            this.gbxItensCotacao.Size = new System.Drawing.Size(683, 436);
            this.gbxItensCotacao.TabIndex = 2;
            this.gbxItensCotacao.TabStop = false;
            this.gbxItensCotacao.Text = "Itens da Cotaçao";
            this.ToolTip.SetToolTip(this.gbxItensCotacao, "Grupo Itens da Cotração");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 164;
            this.label11.Text = "Número:";
            // 
            // btnIdUsuario
            // 
            this.btnIdUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIdUsuario.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIdUsuario.Location = new System.Drawing.Point(602, 33);
            this.btnIdUsuario.Name = "btnIdUsuario";
            this.btnIdUsuario.Size = new System.Drawing.Size(21, 21);
            this.btnIdUsuario.TabIndex = 163;
            this.btnIdUsuario.TabStop = false;
            this.ToolTip.SetToolTip(this.btnIdUsuario, "Usuario");
            this.btnIdUsuario.UseVisualStyleBackColor = true;
            this.btnIdUsuario.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(491, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 162;
            this.label1.Text = "Autorizante:";
            // 
            // tbxAutorizante
            // 
            this.tbxAutorizante.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAutorizante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxAutorizante.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAutorizante.Location = new System.Drawing.Point(494, 33);
            this.tbxAutorizante.Name = "tbxAutorizante";
            this.tbxAutorizante.ReadOnly = true;
            this.tbxAutorizante.Size = new System.Drawing.Size(102, 21);
            this.tbxAutorizante.TabIndex = 161;
            this.tbxAutorizante.TabStop = false;
            this.ToolTip.SetToolTip(this.tbxAutorizante, "Autorizante");
            this.tbxAutorizante.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxAutorizante.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxAutorizante.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(626, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 153;
            this.label8.Text = "Status:";
            // 
            // tbxTermino
            // 
            this.tbxTermino.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTermino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTermino.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTermino.Location = new System.Drawing.Point(629, 33);
            this.tbxTermino.Name = "tbxTermino";
            this.tbxTermino.Size = new System.Drawing.Size(48, 21);
            this.tbxTermino.TabIndex = 152;
            this.tbxTermino.Tag = "";
            this.tbxTermino.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.tbxTermino, "Termino");
            this.tbxTermino.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxTermino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxTermino.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 151;
            this.label7.Text = "Ano:";
            this.label7.Visible = false;
            // 
            // tbxAno
            // 
            this.tbxAno.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxAno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxAno.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAno.Location = new System.Drawing.Point(50, 60);
            this.tbxAno.Name = "tbxAno";
            this.tbxAno.Size = new System.Drawing.Size(38, 21);
            this.tbxAno.TabIndex = 150;
            this.tbxAno.Tag = "";
            this.tbxAno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.tbxAno, "Ano Cotação");
            this.tbxAno.Visible = false;
            this.tbxAno.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxAno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxAno.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(271, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 149;
            this.label5.Text = "Data Término:";
            // 
            // tbxTudoFechado
            // 
            this.tbxTudoFechado.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTudoFechado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTudoFechado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTudoFechado.Location = new System.Drawing.Point(274, 33);
            this.tbxTudoFechado.Name = "tbxTudoFechado";
            this.tbxTudoFechado.ReadOnly = true;
            this.tbxTudoFechado.Size = new System.Drawing.Size(80, 21);
            this.tbxTudoFechado.TabIndex = 148;
            this.ToolTip.SetToolTip(this.tbxTudoFechado, "data Termino");
            this.tbxTudoFechado.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxTudoFechado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxTudoFechado.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 147;
            this.label4.Text = "Data Fechamento:";
            // 
            // tbxDataFechamento
            // 
            this.tbxDataFechamento.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxDataFechamento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDataFechamento.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataFechamento.Location = new System.Drawing.Point(180, 33);
            this.tbxDataFechamento.Name = "tbxDataFechamento";
            this.tbxDataFechamento.ReadOnly = true;
            this.tbxDataFechamento.Size = new System.Drawing.Size(88, 21);
            this.tbxDataFechamento.TabIndex = 146;
            this.ToolTip.SetToolTip(this.tbxDataFechamento, "Data Fechamento");
            this.tbxDataFechamento.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataFechamento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDataFechamento.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxComprador
            // 
            this.tbxComprador.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxComprador.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxComprador.Location = new System.Drawing.Point(360, 33);
            this.tbxComprador.Name = "tbxComprador";
            this.tbxComprador.ReadOnly = true;
            this.tbxComprador.Size = new System.Drawing.Size(128, 21);
            this.tbxComprador.TabIndex = 145;
            this.ToolTip.SetToolTip(this.tbxComprador, "Comprador Emitente");
            this.tbxComprador.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxComprador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxComprador.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(357, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 144;
            this.label3.Text = "Comprador Emitente:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(91, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 143;
            this.label6.Text = "Data:";
            // 
            // tbxDataMontagem
            // 
            this.tbxDataMontagem.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxDataMontagem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDataMontagem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataMontagem.Location = new System.Drawing.Point(94, 33);
            this.tbxDataMontagem.Name = "tbxDataMontagem";
            this.tbxDataMontagem.ReadOnly = true;
            this.tbxDataMontagem.Size = new System.Drawing.Size(80, 21);
            this.tbxDataMontagem.TabIndex = 142;
            this.ToolTip.SetToolTip(this.tbxDataMontagem, "data Montagem");
            this.tbxDataMontagem.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataMontagem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDataMontagem.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxNumero
            // 
            this.tbxNumero.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNumero.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNumero.Location = new System.Drawing.Point(6, 33);
            this.tbxNumero.Name = "tbxNumero";
            this.tbxNumero.ReadOnly = true;
            this.tbxNumero.Size = new System.Drawing.Size(82, 21);
            this.tbxNumero.TabIndex = 141;
            this.tbxNumero.Tag = "";
            this.tbxNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.tbxNumero, "Nro Cotação");
            this.tbxNumero.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNumero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNumero.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 139;
            this.label2.Text = "Observação:";
            // 
            // tbxObservacao
            // 
            this.tbxObservacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxObservacao.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxObservacao.Location = new System.Drawing.Point(169, 60);
            this.tbxObservacao.Name = "tbxObservacao";
            this.tbxObservacao.Size = new System.Drawing.Size(319, 21);
            this.tbxObservacao.TabIndex = 140;
            this.ToolTip.SetToolTip(this.tbxObservacao, "Observação");
            this.tbxObservacao.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxObservacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxObservacao.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(497, 63);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(80, 13);
            this.label25.TabIndex = 132;
            this.label25.Text = "Total Previsto :";
            // 
            // tbxTotalPrevisto
            // 
            this.tbxTotalPrevisto.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTotalPrevisto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTotalPrevisto.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalPrevisto.Location = new System.Drawing.Point(583, 60);
            this.tbxTotalPrevisto.Name = "tbxTotalPrevisto";
            this.tbxTotalPrevisto.ReadOnly = true;
            this.tbxTotalPrevisto.Size = new System.Drawing.Size(94, 21);
            this.tbxTotalPrevisto.TabIndex = 133;
            this.tbxTotalPrevisto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ToolTip.SetToolTip(this.tbxTotalPrevisto, "Total Previsto");
            this.tbxTotalPrevisto.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxTotalPrevisto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxTotalPrevisto.Leave += new System.EventHandler(this.ControlLeave);
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
            this.dgvCotacao1.Location = new System.Drawing.Point(6, 87);
            this.dgvCotacao1.MultiSelect = false;
            this.dgvCotacao1.Name = "dgvCotacao1";
            this.dgvCotacao1.ReadOnly = true;
            this.dgvCotacao1.RowHeadersVisible = false;
            this.dgvCotacao1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCotacao1.Size = new System.Drawing.Size(671, 187);
            this.dgvCotacao1.TabIndex = 125;
            this.dgvCotacao1.TabStop = false;
            this.ToolTip.SetToolTip(this.dgvCotacao1, "Itens da Cotação obs.");
            this.dgvCotacao1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvCotacao1_MouseDoubleClick);
            // 
            // gbxFornecedores
            // 
            this.gbxFornecedores.Controls.Add(this.dgvCotacao2fornecedor);
            this.gbxFornecedores.Location = new System.Drawing.Point(274, 280);
            this.gbxFornecedores.Name = "gbxFornecedores";
            this.gbxFornecedores.Size = new System.Drawing.Size(192, 151);
            this.gbxFornecedores.TabIndex = 134;
            this.gbxFornecedores.TabStop = false;
            this.gbxFornecedores.Text = "Fornecedores desta Cotação";
            this.ToolTip.SetToolTip(this.gbxFornecedores, "Grupo Fornecedores desta Cotação");
            // 
            // dgvCotacao2fornecedor
            // 
            this.dgvCotacao2fornecedor.AccessibleDescription = "Itens da Cotação";
            this.dgvCotacao2fornecedor.AllowUserToAddRows = false;
            this.dgvCotacao2fornecedor.AllowUserToDeleteRows = false;
            this.dgvCotacao2fornecedor.AllowUserToResizeRows = false;
            this.dgvCotacao2fornecedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCotacao2fornecedor.BackgroundColor = System.Drawing.Color.White;
            this.dgvCotacao2fornecedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCotacao2fornecedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvCotacao2fornecedor.Location = new System.Drawing.Point(6, 20);
            this.dgvCotacao2fornecedor.MultiSelect = false;
            this.dgvCotacao2fornecedor.Name = "dgvCotacao2fornecedor";
            this.dgvCotacao2fornecedor.ReadOnly = true;
            this.dgvCotacao2fornecedor.RowHeadersVisible = false;
            this.dgvCotacao2fornecedor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCotacao2fornecedor.Size = new System.Drawing.Size(180, 125);
            this.dgvCotacao2fornecedor.TabIndex = 165;
            this.dgvCotacao2fornecedor.TabStop = false;
            this.ToolTip.SetToolTip(this.dgvCotacao2fornecedor, "Itens da Cotação obs.");
            // 
            // gbxCotacao
            // 
            this.gbxCotacao.Controls.Add(this.label12);
            this.gbxCotacao.Controls.Add(this.tbxQtdeCotacoes);
            this.gbxCotacao.Controls.Add(this.tspCotacao);
            this.gbxCotacao.Controls.Add(this.dgvCotacao);
            this.gbxCotacao.Location = new System.Drawing.Point(12, 118);
            this.gbxCotacao.Name = "gbxCotacao";
            this.gbxCotacao.Size = new System.Drawing.Size(307, 516);
            this.gbxCotacao.TabIndex = 1;
            this.gbxCotacao.TabStop = false;
            this.gbxCotacao.Text = "Cotação";
            this.ToolTip.SetToolTip(this.gbxCotacao, "Grupo das Cotações");
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(257, 474);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 153;
            this.label12.Text = "Total";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxQtdeCotacoes
            // 
            this.tbxQtdeCotacoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxQtdeCotacoes.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxQtdeCotacoes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxQtdeCotacoes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxQtdeCotacoes.Location = new System.Drawing.Point(227, 488);
            this.tbxQtdeCotacoes.Name = "tbxQtdeCotacoes";
            this.tbxQtdeCotacoes.Size = new System.Drawing.Size(73, 21);
            this.tbxQtdeCotacoes.TabIndex = 152;
            this.tbxQtdeCotacoes.Tag = "";
            this.tbxQtdeCotacoes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ToolTip.SetToolTip(this.tbxQtdeCotacoes, "Qtde Total de Cotações");
            this.tbxQtdeCotacoes.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxQtdeCotacoes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxQtdeCotacoes.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tspCotacao
            // 
            this.tspCotacao.AllowMerge = false;
            this.tspCotacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tspCotacao.AutoSize = false;
            this.tspCotacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspCotacao.Dock = System.Windows.Forms.DockStyle.None;
            this.tspCotacao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspCotacaoIncluir,
            this.tspCotacaoAlterar,
            this.tspCotacaoExcluir,
            this.tspRetornar});
            this.tspCotacao.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspCotacao.Location = new System.Drawing.Point(3, 477);
            this.tspCotacao.Name = "tspCotacao";
            this.tspCotacao.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspCotacao.Size = new System.Drawing.Size(251, 35);
            this.tspCotacao.TabIndex = 132;
            this.ToolTip.SetToolTip(this.tspCotacao, "Barra de controle da Cotaçao");
            // 
            // tspCotacaoAlterar
            // 
            this.tspCotacaoAlterar.AutoSize = false;
            this.tspCotacaoAlterar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCotacaoAlterar.ForeColor = System.Drawing.Color.Black;
            this.tspCotacaoAlterar.Image = global::CRCorrea.Properties.Resources.Editar;
            this.tspCotacaoAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspCotacaoAlterar.Name = "tspCotacaoAlterar";
            this.tspCotacaoAlterar.Size = new System.Drawing.Size(54, 31);
            this.tspCotacaoAlterar.Text = "Alterar";
            this.tspCotacaoAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspCotacaoAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
            // 
            // tspCotacaoExcluir
            // 
            this.tspCotacaoExcluir.AutoSize = false;
            this.tspCotacaoExcluir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCotacaoExcluir.ForeColor = System.Drawing.Color.Black;
            this.tspCotacaoExcluir.Image = global::CRCorrea.Properties.Resources.Excluir;
            this.tspCotacaoExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspCotacaoExcluir.Name = "tspCotacaoExcluir";
            this.tspCotacaoExcluir.Size = new System.Drawing.Size(54, 31);
            this.tspCotacaoExcluir.Text = "Excluir";
            this.tspCotacaoExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspCotacaoExcluir.Click += new System.EventHandler(this.tspCotacaoExcluir_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = global::CRCorrea.Properties.Resources.Retornar;
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(54, 31);
            this.tspRetornar.Text = "&Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // rbnTerminoN
            // 
            this.rbnTerminoN.AccessibleDescription = "Cotações em Aberto";
            this.rbnTerminoN.AutoSize = true;
            this.rbnTerminoN.Checked = true;
            this.rbnTerminoN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTerminoN.Location = new System.Drawing.Point(16, 33);
            this.rbnTerminoN.Name = "rbnTerminoN";
            this.rbnTerminoN.Size = new System.Drawing.Size(98, 17);
            this.rbnTerminoN.TabIndex = 2;
            this.rbnTerminoN.TabStop = true;
            this.rbnTerminoN.Text = "[N]=Em Aberto";
            this.ToolTip.SetToolTip(this.rbnTerminoN, "[N]=Em Aberto");
            this.rbnTerminoN.UseVisualStyleBackColor = true;
            this.rbnTerminoN.Click += new System.EventHandler(this.rbnTerminoN_Click);
            // 
            // rbnTodas
            // 
            this.rbnTodas.AccessibleDescription = "Todas as Cotações";
            this.rbnTodas.AutoSize = true;
            this.rbnTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTodas.Location = new System.Drawing.Point(16, 15);
            this.rbnTodas.Name = "rbnTodas";
            this.rbnTodas.Size = new System.Drawing.Size(54, 17);
            this.rbnTodas.TabIndex = 1;
            this.rbnTodas.Text = "Todas";
            this.ToolTip.SetToolTip(this.rbnTodas, "Todas as Cotações");
            this.rbnTodas.UseVisualStyleBackColor = true;
            this.rbnTodas.Click += new System.EventHandler(this.rbnTodas_Click);
            // 
            // rbnTerminoA
            // 
            this.rbnTerminoA.AccessibleDescription = "Cotações em Aprovação";
            this.rbnTerminoA.AutoSize = true;
            this.rbnTerminoA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTerminoA.Location = new System.Drawing.Point(128, 15);
            this.rbnTerminoA.Name = "rbnTerminoA";
            this.rbnTerminoA.Size = new System.Drawing.Size(162, 17);
            this.rbnTerminoA.TabIndex = 3;
            this.rbnTerminoA.Text = "[A]=Aguardando Aprovação";
            this.ToolTip.SetToolTip(this.rbnTerminoA, "[A]=Aguardando Aprovação");
            this.rbnTerminoA.UseVisualStyleBackColor = true;
            this.rbnTerminoA.Click += new System.EventHandler(this.rbnTerminoA_Click);
            // 
            // gbxOpcoes
            // 
            this.gbxOpcoes.Controls.Add(this.rbnTerminoF);
            this.gbxOpcoes.Controls.Add(this.rbnTerminoO);
            this.gbxOpcoes.Controls.Add(this.rbnTerminoR);
            this.gbxOpcoes.Controls.Add(this.rbnTerminoA);
            this.gbxOpcoes.Controls.Add(this.rbnTerminoN);
            this.gbxOpcoes.Controls.Add(this.rbnTodas);
            this.gbxOpcoes.Location = new System.Drawing.Point(12, 35);
            this.gbxOpcoes.Name = "gbxOpcoes";
            this.gbxOpcoes.Size = new System.Drawing.Size(309, 78);
            this.gbxOpcoes.TabIndex = 0;
            this.gbxOpcoes.TabStop = false;
            this.gbxOpcoes.Text = "Visualizar as Cotações no Status";
            this.ToolTip.SetToolTip(this.gbxOpcoes, "Grupo Filtrar o Tipo da Cotação");
            // 
            // rbnTerminoF
            // 
            this.rbnTerminoF.AccessibleDescription = "[F]=Cotações Fechadas";
            this.rbnTerminoF.AutoSize = true;
            this.rbnTerminoF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTerminoF.Location = new System.Drawing.Point(16, 52);
            this.rbnTerminoF.Name = "rbnTerminoF";
            this.rbnTerminoF.Size = new System.Drawing.Size(93, 17);
            this.rbnTerminoF.TabIndex = 6;
            this.rbnTerminoF.Text = "[F]=Fechadas";
            this.ToolTip.SetToolTip(this.rbnTerminoF, "[F]=Cotações Fechadas");
            this.rbnTerminoF.UseVisualStyleBackColor = true;
            this.rbnTerminoF.CheckedChanged += new System.EventHandler(this.rbnTerminoF_CheckedChanged);
            // 
            // rbnTerminoO
            // 
            this.rbnTerminoO.AccessibleDescription = "[O]=Aprovadas Emitir P.Compra";
            this.rbnTerminoO.AutoSize = true;
            this.rbnTerminoO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTerminoO.Location = new System.Drawing.Point(128, 52);
            this.rbnTerminoO.Name = "rbnTerminoO";
            this.rbnTerminoO.Size = new System.Drawing.Size(180, 17);
            this.rbnTerminoO.TabIndex = 5;
            this.rbnTerminoO.Text = "[O]=Aprovadas Emitir P.Compra";
            this.ToolTip.SetToolTip(this.rbnTerminoO, "[O]=Aprovadas Emitir P.Compra");
            this.rbnTerminoO.UseVisualStyleBackColor = true;
            this.rbnTerminoO.Click += new System.EventHandler(this.rbnTerminoO_Click);
            // 
            // rbnTerminoR
            // 
            this.rbnTerminoR.AccessibleDescription = "Cotações Reprovadas";
            this.rbnTerminoR.AutoSize = true;
            this.rbnTerminoR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnTerminoR.Location = new System.Drawing.Point(128, 33);
            this.rbnTerminoR.Name = "rbnTerminoR";
            this.rbnTerminoR.Size = new System.Drawing.Size(106, 17);
            this.rbnTerminoR.TabIndex = 4;
            this.rbnTerminoR.Text = "[R]=Reprovadas";
            this.ToolTip.SetToolTip(this.rbnTerminoR, "[R]=Cotações Reprovadas");
            this.rbnTerminoR.UseVisualStyleBackColor = true;
            this.rbnTerminoR.Click += new System.EventHandler(this.rbnTerminoR_Click);
            // 
            // tspCotacaoR
            // 
            this.tspCotacaoR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspCotacaoR.AutoSize = false;
            this.tspCotacaoR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspCotacaoR.Dock = System.Windows.Forms.DockStyle.None;
            this.tspCotacaoR.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCotacaoR.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspCotacaoR.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspCotacaoR.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspCotacaoR.Location = new System.Drawing.Point(322, 587);
            this.tspCotacaoR.Name = "tspCotacaoR";
            this.tspCotacaoR.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspCotacaoR.Size = new System.Drawing.Size(332, 47);
            this.tspCotacaoR.TabIndex = 3;
            this.ToolTip.SetToolTip(this.tspCotacaoR, "Retornar");
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
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(64, 16);
            this.tslbLocalizar.Text = "Procurar";
            // 
            // tstbxLocalizar
            // 
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(200, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstPesquisa_KeyUp);
            this.tstbxLocalizar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tstPesquisa_MouseUp);
            // 
            // gbxStatusMotivos
            // 
            this.gbxStatusMotivos.AccessibleDescription = "Motivo Devolução ou Justificativa";
            this.gbxStatusMotivos.Controls.Add(this.gbxMotivo);
            this.gbxStatusMotivos.Controls.Add(this.gbxJustificativa);
            this.gbxStatusMotivos.Location = new System.Drawing.Point(510, 482);
            this.gbxStatusMotivos.Name = "gbxStatusMotivos";
            this.gbxStatusMotivos.Size = new System.Drawing.Size(496, 124);
            this.gbxStatusMotivos.TabIndex = 141;
            this.gbxStatusMotivos.TabStop = false;
            this.gbxStatusMotivos.Text = "Status Motivos";
            this.ToolTip.SetToolTip(this.gbxStatusMotivos, "Motivo Devolução ou Justificativa");
            this.gbxStatusMotivos.Visible = false;
            // 
            // gbxMotivo
            // 
            this.gbxMotivo.AccessibleDescription = "Motivo da Reprovação";
            this.gbxMotivo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxMotivo.Controls.Add(this.tbxMotivoReprovado);
            this.gbxMotivo.Location = new System.Drawing.Point(227, 14);
            this.gbxMotivo.Name = "gbxMotivo";
            this.gbxMotivo.Size = new System.Drawing.Size(260, 99);
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
            this.tbxMotivoReprovado.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxMotivoReprovado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMotivoReprovado.Location = new System.Drawing.Point(6, 20);
            this.tbxMotivoReprovado.Multiline = true;
            this.tbxMotivoReprovado.Name = "tbxMotivoReprovado";
            this.tbxMotivoReprovado.Size = new System.Drawing.Size(248, 73);
            this.tbxMotivoReprovado.TabIndex = 121;
            this.ToolTip.SetToolTip(this.tbxMotivoReprovado, "Motivo da Reprovação");
            // 
            // gbxJustificativa
            // 
            this.gbxJustificativa.AccessibleDescription = "Justificativa do Comprador";
            this.gbxJustificativa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxJustificativa.Controls.Add(this.tbxRespostaComprador);
            this.gbxJustificativa.Location = new System.Drawing.Point(6, 13);
            this.gbxJustificativa.Name = "gbxJustificativa";
            this.gbxJustificativa.Size = new System.Drawing.Size(215, 99);
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
            this.tbxRespostaComprador.Size = new System.Drawing.Size(203, 73);
            this.tbxRespostaComprador.TabIndex = 121;
            this.ToolTip.SetToolTip(this.tbxRespostaComprador, "Resposta Justificando");
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label26.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(12, 4);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(989, 29);
            this.label26.TabIndex = 50;
            this.label26.Text = "Visualizando as Cotações de Materiais";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tspCotacaoIncluir
            // 
            this.tspCotacaoIncluir.AutoSize = false;
            this.tspCotacaoIncluir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCotacaoIncluir.ForeColor = System.Drawing.Color.Black;
            this.tspCotacaoIncluir.Image = global::CRCorrea.Properties.Resources.Incluir;
            this.tspCotacaoIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspCotacaoIncluir.Name = "tspCotacaoIncluir";
            this.tspCotacaoIncluir.Size = new System.Drawing.Size(54, 31);
            this.tspCotacaoIncluir.Text = "Incluir";
            this.tspCotacaoIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspCotacaoIncluir.Click += new System.EventHandler(this.tspIncluir_Click);
            // 
            // frmCotacaoVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxStatusMotivos);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.tspCotacaoR);
            this.Controls.Add(this.gbxOpcoes);
            this.Controls.Add(this.gbxCotacao);
            this.Controls.Add(this.gbxItensCotacao);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmCotacaoVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "34007";
            this.Text = "Cotação de Preços - Visualização";
            this.ToolTip.SetToolTip(this, "Cotação de Preços - Visualização");
            this.Activated += new System.EventHandler(this.frmCotacaoVis_Activated);
            this.Load += new System.EventHandler(this.frmCotacaoVis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao)).EndInit();
            this.gbxItensCotacao.ResumeLayout(false);
            this.gbxItensCotacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao1)).EndInit();
            this.gbxFornecedores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotacao2fornecedor)).EndInit();
            this.gbxCotacao.ResumeLayout(false);
            this.gbxCotacao.PerformLayout();
            this.tspCotacao.ResumeLayout(false);
            this.tspCotacao.PerformLayout();
            this.gbxOpcoes.ResumeLayout(false);
            this.gbxOpcoes.PerformLayout();
            this.tspCotacaoR.ResumeLayout(false);
            this.tspCotacaoR.PerformLayout();
            this.gbxStatusMotivos.ResumeLayout(false);
            this.gbxMotivo.ResumeLayout(false);
            this.gbxMotivo.PerformLayout();
            this.gbxJustificativa.ResumeLayout(false);
            this.gbxJustificativa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.GroupBox gbxCotacao;
        private System.Windows.Forms.DataGridView dgvCotacao;
        private System.Windows.Forms.GroupBox gbxItensCotacao;
        private System.Windows.Forms.DataGridView dgvCotacao1;
        private System.Windows.Forms.GroupBox gbxOpcoes;
        private System.Windows.Forms.RadioButton rbnTerminoN;
        private System.Windows.Forms.RadioButton rbnTodas;
        private System.Windows.Forms.ToolStrip tspCotacaoR;
        private System.Windows.Forms.ToolStrip tspCotacao;
        private System.Windows.Forms.ToolStripButton tspCotacaoIncluir;
        private System.Windows.Forms.ToolStripButton tspCotacaoAlterar;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbxTotalPrevisto;
        private System.Windows.Forms.GroupBox gbxFornecedores;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxObservacao;
        private System.Windows.Forms.TextBox tbxNumero;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxTudoFechado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxDataFechamento;
        private System.Windows.Forms.TextBox tbxComprador;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxDataMontagem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxAno;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxTermino;
        private System.Windows.Forms.RadioButton rbnTerminoA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxAutorizante;
        private System.Windows.Forms.Button btnIdUsuario;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvCotacao2fornecedor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxQtdeCotacoes;
        private System.Windows.Forms.ToolStripButton tspCotacaoExcluir;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.RadioButton rbnTerminoR;
        private System.Windows.Forms.GroupBox gbxStatusMotivos;
        private System.Windows.Forms.GroupBox gbxMotivo;
        private System.Windows.Forms.TextBox tbxMotivoReprovado;
        private System.Windows.Forms.GroupBox gbxJustificativa;
        private System.Windows.Forms.TextBox tbxRespostaComprador;
        private System.Windows.Forms.RadioButton rbnTerminoF;
        private System.Windows.Forms.RadioButton rbnTerminoO;
        private System.Windows.Forms.ToolStripButton tspRetornar;
    }
}