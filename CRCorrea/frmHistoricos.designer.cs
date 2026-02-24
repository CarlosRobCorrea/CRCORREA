namespace CRCorrea
{
    partial class frmHistoricos
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
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxTipo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblNomeContaContabil = new System.Windows.Forms.Label();
            this.tbxNivel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnIdContabil = new System.Windows.Forms.Button();
            this.tbxContabil = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnIdCobranca = new System.Windows.Forms.Button();
            this.tbxCobranca = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbxAtivo = new System.Windows.Forms.ComboBox();
            this.gbxAtivo = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tspTool.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbxAtivo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(9, 570);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(428, 45);
            this.tspTool.TabIndex = 4;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            this.toolTip1.SetToolTip(this.tspTool, "Barra Opções");
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = global::CRCorrea.Properties.Resources.Salvar;
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(66, 42);
            this.tspSalvar.Text = "&Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspPrimeiro
            // 
            this.tspPrimeiro.AccessibleDescription = "Primeiro";
            this.tspPrimeiro.AutoSize = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = global::CRCorrea.Properties.Resources.Primeiro;
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
            this.tspAnterior.Image = global::CRCorrea.Properties.Resources.Anterior;
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
            this.tspProximo.Image = global::CRCorrea.Properties.Resources.Proximo;
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
            this.tspUltimo.Image = global::CRCorrea.Properties.Resources.Ultimo;
            this.tspUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Size = new System.Drawing.Size(66, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspUltimo.Click += new System.EventHandler(this.tspUltimo_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = global::CRCorrea.Properties.Resources.Retornar;
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "Dados";
            this.groupBox2.Controls.Add(this.tbxCodigo);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.tbxNome);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(69, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(742, 62);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, "Grupo - Cabeçalho");
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.AccessibleDescription = "Código";
            this.tbxCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Location = new System.Drawing.Point(6, 33);
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(116, 21);
            this.tbxCodigo.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxCodigo, "Código");
            this.tbxCodigo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Código:";
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Descrição";
            this.tbxNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(128, 33);
            this.tbxNome.MaxLength = 40;
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(606, 21);
            this.tbxNome.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbxNome, "Descrição");
            this.tbxNome.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Descrição:";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Lançamentos";
            this.groupBox1.Controls.Add(this.cbxTipo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(319, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 48);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, "Grupo - Maioria dos Lançamentos");
            // 
            // cbxTipo
            // 
            this.cbxTipo.AccessibleDescription = "Tipo";
            this.cbxTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipo.FormattingEnabled = true;
            this.cbxTipo.Items.AddRange(new object[] {
            "C - Crédito",
            "D - Débito"});
            this.cbxTipo.Location = new System.Drawing.Point(231, 20);
            this.cbxTipo.Name = "cbxTipo";
            this.cbxTipo.Size = new System.Drawing.Size(81, 21);
            this.cbxTipo.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cbxTipo, "Tipo de Conta");
            this.cbxTipo.Enter += new System.EventHandler(this.ControlEnter);
            this.cbxTipo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.cbxTipo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Na maioria dos lançamento é uma conta de:";
            // 
            // groupBox3
            // 
            this.groupBox3.AccessibleDescription = "Conta Contábil";
            this.groupBox3.Controls.Add(this.lblNomeContaContabil);
            this.groupBox3.Controls.Add(this.tbxNivel);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btnIdContabil);
            this.groupBox3.Controls.Add(this.tbxContabil);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(269, 314);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(418, 70);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Conta Contábil";
            this.toolTip1.SetToolTip(this.groupBox3, "Grupo Conta Contábil");
            // 
            // lblNomeContaContabil
            // 
            this.lblNomeContaContabil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNomeContaContabil.Location = new System.Drawing.Point(6, 44);
            this.lblNomeContaContabil.Name = "lblNomeContaContabil";
            this.lblNomeContaContabil.Size = new System.Drawing.Size(406, 19);
            this.lblNomeContaContabil.TabIndex = 12;
            this.lblNomeContaContabil.Text = ".. descrição da conta contabil ..";
            this.lblNomeContaContabil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxNivel
            // 
            this.tbxNivel.AccessibleDescription = "Nível";
            this.tbxNivel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNivel.Location = new System.Drawing.Point(351, 20);
            this.tbxNivel.Name = "tbxNivel";
            this.tbxNivel.Size = new System.Drawing.Size(61, 21);
            this.tbxNivel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbxNivel, "Nível Conta");
            this.tbxNivel.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNivel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNivel.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(311, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Nível:";
            // 
            // btnIdContabil
            // 
            this.btnIdContabil.AccessibleDescription = "Busca";
            this.btnIdContabil.Enabled = false;
            this.btnIdContabil.Image = global::CRCorrea.Properties.Resources.Procurar;
            this.btnIdContabil.Location = new System.Drawing.Point(282, 19);
            this.btnIdContabil.Name = "btnIdContabil";
            this.btnIdContabil.Size = new System.Drawing.Size(23, 21);
            this.btnIdContabil.TabIndex = 7;
            this.btnIdContabil.TabStop = false;
            this.toolTip1.SetToolTip(this.btnIdContabil, "Escolher - Código Contábil");
            this.btnIdContabil.UseVisualStyleBackColor = true;
            // 
            // tbxContabil
            // 
            this.tbxContabil.AccessibleDescription = "Código";
            this.tbxContabil.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxContabil.Location = new System.Drawing.Point(56, 20);
            this.tbxContabil.Name = "tbxContabil";
            this.tbxContabil.Size = new System.Drawing.Size(220, 21);
            this.tbxContabil.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxContabil, "Código Conta Contábil");
            this.tbxContabil.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxContabil.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxContabil.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Código:";
            // 
            // groupBox4
            // 
            this.groupBox4.AccessibleDescription = "Código Cobrança Aplisoft:";
            this.groupBox4.Controls.Add(this.btnIdCobranca);
            this.groupBox4.Controls.Add(this.tbxCobranca);
            this.groupBox4.Location = new System.Drawing.Point(367, 390);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(222, 48);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Código Cobrança Aplisoft:";
            this.toolTip1.SetToolTip(this.groupBox4, "Grupo - Código Cobrança Aplisoft");
            // 
            // btnIdCobranca
            // 
            this.btnIdCobranca.AccessibleDescription = "Busca";
            this.btnIdCobranca.Enabled = false;
            this.btnIdCobranca.Image = global::CRCorrea.Properties.Resources.Procurar;
            this.btnIdCobranca.Location = new System.Drawing.Point(191, 19);
            this.btnIdCobranca.Name = "btnIdCobranca";
            this.btnIdCobranca.Size = new System.Drawing.Size(23, 21);
            this.btnIdCobranca.TabIndex = 7;
            this.btnIdCobranca.TabStop = false;
            this.toolTip1.SetToolTip(this.btnIdCobranca, "Escolher - Código Cobrança Aplisoft");
            this.btnIdCobranca.UseVisualStyleBackColor = true;
            // 
            // tbxCobranca
            // 
            this.tbxCobranca.AccessibleDescription = "Código Cobrança";
            this.tbxCobranca.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCobranca.Location = new System.Drawing.Point(6, 20);
            this.tbxCobranca.Name = "tbxCobranca";
            this.tbxCobranca.Size = new System.Drawing.Size(179, 21);
            this.tbxCobranca.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxCobranca, "Código Cobrança Aplisoft");
            this.tbxCobranca.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCobranca.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCobranca.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // cbxAtivo
            // 
            this.cbxAtivo.AccessibleDescription = "Ativo?";
            this.cbxAtivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAtivo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAtivo.FormattingEnabled = true;
            this.cbxAtivo.Items.AddRange(new object[] {
            "S - Sim",
            "N - Não"});
            this.cbxAtivo.Location = new System.Drawing.Point(6, 33);
            this.cbxAtivo.Name = "cbxAtivo";
            this.cbxAtivo.Size = new System.Drawing.Size(81, 21);
            this.cbxAtivo.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cbxAtivo, "Ativo?");
            this.cbxAtivo.Enter += new System.EventHandler(this.ControlEnter);
            this.cbxAtivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.cbxAtivo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxAtivo
            // 
            this.gbxAtivo.AccessibleDescription = "Ativo";
            this.gbxAtivo.Controls.Add(this.label5);
            this.gbxAtivo.Controls.Add(this.cbxAtivo);
            this.gbxAtivo.Location = new System.Drawing.Point(817, 192);
            this.gbxAtivo.Name = "gbxAtivo";
            this.gbxAtivo.Size = new System.Drawing.Size(93, 62);
            this.gbxAtivo.TabIndex = 1;
            this.gbxAtivo.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxAtivo, "Grupo - Ativo?");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Ativo?";
            // 
            // frmHistoricos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxAtivo);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tspTool);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHistoricos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "409";
            this.Text = "Historicos - Registrando";
            this.toolTip1.SetToolTip(this, "Historicos - Registrando");
            this.Load += new System.EventHandler(this.frmHistoricos_Load);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbxAtivo.ResumeLayout(false);
            this.gbxAtivo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbxContabil;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxNivel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnIdContabil;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnIdCobranca;
        private System.Windows.Forms.TextBox tbxCobranca;
        private System.Windows.Forms.Label lblNomeContaContabil;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbxAtivo;
        private System.Windows.Forms.GroupBox gbxAtivo;
        private System.Windows.Forms.ComboBox cbxTipo;
        private System.Windows.Forms.Label label5;
    }
}