namespace CRCorrea
{
    partial class frmPecasEstoqueAut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPecasEstoqueAut));
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lbxNome = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbxDescricao = new System.Windows.Forms.Label();
            this.pgrExecuta = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxPeriodo = new System.Windows.Forms.GroupBox();
            this.tbxDatafim = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxDataini = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxItens = new System.Windows.Forms.GroupBox();
            this.tbxCodigoNomeAte = new System.Windows.Forms.TextBox();
            this.tbxCodigoNomeDe = new System.Windows.Forms.TextBox();
            this.btnCodigoAte = new System.Windows.Forms.Button();
            this.btnCodigoDe = new System.Windows.Forms.Button();
            this.tbxCodigoAte = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxCodigoDe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bwrAcertoAutomatico = new System.ComponentModel.BackgroundWorker();
            this.bwrCarregaAutoComplete = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tspAcertoAutomatico = new System.Windows.Forms.ToolStrip();
            this.tspExecutar = new System.Windows.Forms.ToolStripButton();
            this.tspCancelar = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.label7 = new System.Windows.Forms.Label();
            this.lbxNome.SuspendLayout();
            this.gbxPeriodo.SuspendLayout();
            this.gbxItens.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tspAcertoAutomatico.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxNome
            // 
            this.lbxNome.AccessibleDescription = "Nome";
            this.lbxNome.Controls.Add(this.label6);
            this.lbxNome.Controls.Add(this.lbxDescricao);
            this.lbxNome.Controls.Add(this.pgrExecuta);
            this.lbxNome.Controls.Add(this.label3);
            this.lbxNome.Location = new System.Drawing.Point(42, 156);
            this.lbxNome.Name = "lbxNome";
            this.lbxNome.Size = new System.Drawing.Size(618, 181);
            this.lbxNome.TabIndex = 3;
            this.lbxNome.TabStop = false;
            this.ToolTip.SetToolTip(this.lbxNome, "Execução");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(176, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Código - Nome do produto";
            // 
            // lbxDescricao
            // 
            this.lbxDescricao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbxDescricao.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxDescricao.Location = new System.Drawing.Point(8, 54);
            this.lbxDescricao.Name = "lbxDescricao";
            this.lbxDescricao.Size = new System.Drawing.Size(604, 25);
            this.lbxDescricao.TabIndex = 14;
            this.lbxDescricao.Text = "0 - ABC...";
            // 
            // pgrExecuta
            // 
            this.pgrExecuta.AccessibleDescription = "Progresso da Execução";
            this.pgrExecuta.Location = new System.Drawing.Point(7, 113);
            this.pgrExecuta.Maximum = 0;
            this.pgrExecuta.Name = "pgrExecuta";
            this.pgrExecuta.Size = new System.Drawing.Size(604, 21);
            this.pgrExecuta.TabIndex = 12;
            this.ToolTip.SetToolTip(this.pgrExecuta, "Atualizando Estoque");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "Progresso da Execução";
            // 
            // gbxPeriodo
            // 
            this.gbxPeriodo.AccessibleDescription = "Parametros de Périodo";
            this.gbxPeriodo.Controls.Add(this.tbxDatafim);
            this.gbxPeriodo.Controls.Add(this.label5);
            this.gbxPeriodo.Controls.Add(this.tbxDataini);
            this.gbxPeriodo.Controls.Add(this.label4);
            this.gbxPeriodo.Location = new System.Drawing.Point(480, 50);
            this.gbxPeriodo.Name = "gbxPeriodo";
            this.gbxPeriodo.Size = new System.Drawing.Size(180, 106);
            this.gbxPeriodo.TabIndex = 2;
            this.gbxPeriodo.TabStop = false;
            this.gbxPeriodo.Text = "Parametros de Périodo";
            this.ToolTip.SetToolTip(this.gbxPeriodo, "Grupo de Parametros de Periodo");
            // 
            // tbxDatafim
            // 
            this.tbxDatafim.AccessibleDescription = "Da Data até";
            this.tbxDatafim.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxDatafim.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDatafim.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDatafim.Location = new System.Drawing.Point(80, 55);
            this.tbxDatafim.Name = "tbxDatafim";
            this.tbxDatafim.ReadOnly = true;
            this.tbxDatafim.Size = new System.Drawing.Size(77, 21);
            this.tbxDatafim.TabIndex = 3;
            this.tbxDatafim.TabStop = false;
            this.ToolTip.SetToolTip(this.tbxDatafim, "Data Final");
            this.tbxDatafim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDatafim.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxDatafim.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Da Data até";
            // 
            // tbxDataini
            // 
            this.tbxDataini.AccessibleDescription = "Da Data de ";
            this.tbxDataini.BackColor = System.Drawing.Color.White;
            this.tbxDataini.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDataini.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataini.Location = new System.Drawing.Point(80, 28);
            this.tbxDataini.Name = "tbxDataini";
            this.tbxDataini.Size = new System.Drawing.Size(77, 21);
            this.tbxDataini.TabIndex = 1;
            this.ToolTip.SetToolTip(this.tbxDataini, "Data Inicial");
            this.tbxDataini.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataini.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxDataini.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Da Data de";
            // 
            // gbxItens
            // 
            this.gbxItens.AccessibleDescription = "Parametros dos Itens";
            this.gbxItens.Controls.Add(this.tbxCodigoNomeAte);
            this.gbxItens.Controls.Add(this.tbxCodigoNomeDe);
            this.gbxItens.Controls.Add(this.btnCodigoAte);
            this.gbxItens.Controls.Add(this.btnCodigoDe);
            this.gbxItens.Controls.Add(this.tbxCodigoAte);
            this.gbxItens.Controls.Add(this.label2);
            this.gbxItens.Controls.Add(this.tbxCodigoDe);
            this.gbxItens.Controls.Add(this.label1);
            this.gbxItens.Location = new System.Drawing.Point(41, 50);
            this.gbxItens.Name = "gbxItens";
            this.gbxItens.Size = new System.Drawing.Size(433, 105);
            this.gbxItens.TabIndex = 1;
            this.gbxItens.TabStop = false;
            this.gbxItens.Text = "Parametros dos Itens";
            this.ToolTip.SetToolTip(this.gbxItens, "Grupo Parametros dos Itens");
            // 
            // tbxCodigoNomeAte
            // 
            this.tbxCodigoNomeAte.AccessibleDescription = "Do Código";
            this.tbxCodigoNomeAte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxCodigoNomeAte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxCodigoNomeAte.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxCodigoNomeAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigoNomeAte.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigoNomeAte.Location = new System.Drawing.Point(68, 78);
            this.tbxCodigoNomeAte.Name = "tbxCodigoNomeAte";
            this.tbxCodigoNomeAte.ReadOnly = true;
            this.tbxCodigoNomeAte.Size = new System.Drawing.Size(334, 18);
            this.tbxCodigoNomeAte.TabIndex = 87;
            this.tbxCodigoNomeAte.TabStop = false;
            this.ToolTip.SetToolTip(this.tbxCodigoNomeAte, "Código Inicial");
            // 
            // tbxCodigoNomeDe
            // 
            this.tbxCodigoNomeDe.AccessibleDescription = "Do Código";
            this.tbxCodigoNomeDe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxCodigoNomeDe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxCodigoNomeDe.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxCodigoNomeDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigoNomeDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigoNomeDe.Location = new System.Drawing.Point(68, 36);
            this.tbxCodigoNomeDe.Name = "tbxCodigoNomeDe";
            this.tbxCodigoNomeDe.ReadOnly = true;
            this.tbxCodigoNomeDe.Size = new System.Drawing.Size(334, 18);
            this.tbxCodigoNomeDe.TabIndex = 86;
            this.tbxCodigoNomeDe.TabStop = false;
            this.ToolTip.SetToolTip(this.tbxCodigoNomeDe, "Código Inicial");
            // 
            // btnCodigoAte
            // 
            this.btnCodigoAte.AccessibleDescription = "Busca Código Até";
            this.btnCodigoAte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCodigoAte.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCodigoAte.Image = ((System.Drawing.Image)(resources.GetObject("btnCodigoAte.Image")));
            this.btnCodigoAte.Location = new System.Drawing.Point(403, 54);
            this.btnCodigoAte.Name = "btnCodigoAte";
            this.btnCodigoAte.Size = new System.Drawing.Size(24, 25);
            this.btnCodigoAte.TabIndex = 85;
            this.btnCodigoAte.TabStop = false;
            this.ToolTip.SetToolTip(this.btnCodigoAte, "Localizar Código");
            this.btnCodigoAte.UseVisualStyleBackColor = true;
            this.btnCodigoAte.Click += new System.EventHandler(this.btnCodigoAte_Click);
            // 
            // btnCodigoDe
            // 
            this.btnCodigoDe.AccessibleDescription = "Busca Código De";
            this.btnCodigoDe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCodigoDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCodigoDe.Image = ((System.Drawing.Image)(resources.GetObject("btnCodigoDe.Image")));
            this.btnCodigoDe.Location = new System.Drawing.Point(403, 12);
            this.btnCodigoDe.Name = "btnCodigoDe";
            this.btnCodigoDe.Size = new System.Drawing.Size(24, 25);
            this.btnCodigoDe.TabIndex = 84;
            this.btnCodigoDe.TabStop = false;
            this.ToolTip.SetToolTip(this.btnCodigoDe, "Localizar Código");
            this.btnCodigoDe.UseVisualStyleBackColor = true;
            this.btnCodigoDe.Click += new System.EventHandler(this.btnCodigoDe_Click);
            // 
            // tbxCodigoAte
            // 
            this.tbxCodigoAte.AccessibleDescription = "Até Código";
            this.tbxCodigoAte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxCodigoAte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxCodigoAte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigoAte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigoAte.Location = new System.Drawing.Point(68, 56);
            this.tbxCodigoAte.Name = "tbxCodigoAte";
            this.tbxCodigoAte.Size = new System.Drawing.Size(334, 21);
            this.tbxCodigoAte.TabIndex = 2;
            this.ToolTip.SetToolTip(this.tbxCodigoAte, "Código Final");
            this.tbxCodigoAte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigoAte.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxCodigoAte.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Até Codigo";
            // 
            // tbxCodigoDe
            // 
            this.tbxCodigoDe.AccessibleDescription = "Do Código";
            this.tbxCodigoDe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxCodigoDe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxCodigoDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigoDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigoDe.Location = new System.Drawing.Point(68, 13);
            this.tbxCodigoDe.Name = "tbxCodigoDe";
            this.tbxCodigoDe.Size = new System.Drawing.Size(334, 21);
            this.tbxCodigoDe.TabIndex = 1;
            this.ToolTip.SetToolTip(this.tbxCodigoDe, "Código Inicial");
            this.tbxCodigoDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigoDe.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxCodigoDe.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Do Codigo";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tspAcertoAutomatico);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lbxNome);
            this.groupBox1.Controls.Add(this.gbxPeriodo);
            this.groupBox1.Controls.Add(this.gbxItens);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(169, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(717, 411);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Refazer o Saldo do Estoque Automaticamente";
            // 
            // tspAcertoAutomatico
            // 
            this.tspAcertoAutomatico.AccessibleDescription = "Barra de Opções";
            this.tspAcertoAutomatico.AllowMerge = false;
            this.tspAcertoAutomatico.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspAcertoAutomatico.AutoSize = false;
            this.tspAcertoAutomatico.Dock = System.Windows.Forms.DockStyle.None;
            this.tspAcertoAutomatico.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspAcertoAutomatico.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspExecutar,
            this.tspCancelar,
            this.tspRetornar});
            this.tspAcertoAutomatico.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspAcertoAutomatico.Location = new System.Drawing.Point(41, 348);
            this.tspAcertoAutomatico.Name = "tspAcertoAutomatico";
            this.tspAcertoAutomatico.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspAcertoAutomatico.Size = new System.Drawing.Size(619, 46);
            this.tspAcertoAutomatico.TabIndex = 4;
            this.tspAcertoAutomatico.TabStop = true;
            // 
            // tspExecutar
            // 
            this.tspExecutar.AccessibleDescription = "Executar";
            this.tspExecutar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExecutar.Image = ((System.Drawing.Image)(resources.GetObject("tspExecutar.Image")));
            this.tspExecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExecutar.Name = "tspExecutar";
            this.tspExecutar.Size = new System.Drawing.Size(64, 42);
            this.tspExecutar.Text = "Executar";
            this.tspExecutar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExecutar.Click += new System.EventHandler(this.tspExecutar_Click);
            // 
            // tspCancelar
            // 
            this.tspCancelar.AccessibleDescription = "Cancelar";
            this.tspCancelar.Enabled = false;
            this.tspCancelar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCancelar.Image = ((System.Drawing.Image)(resources.GetObject("tspCancelar.Image")));
            this.tspCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspCancelar.Name = "tspCancelar";
            this.tspCancelar.Size = new System.Drawing.Size(62, 42);
            this.tspCancelar.Text = "Cancelar";
            this.tspCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspCancelar.Click += new System.EventHandler(this.tspCancelar_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label7.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(122, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(521, 23);
            this.label7.TabIndex = 90;
            this.label7.Text = "O sistema refaz Automaticamente todas as Entradas e Saidas";
            // 
            // frmPecasEstoqueAut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPecasEstoqueAut";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "34188";
            this.Text = "Peças Estoque Acerto Automatico - Registrando";
            this.ToolTip.SetToolTip(this, "Peças Estoque Acerto  Automatico - Registrando");
            this.Load += new System.EventHandler(this.frmPecasEstoqueAut_Load);
            this.Activated += new System.EventHandler(this.frmPecasEstoqueAut_Activated);
            this.Click += new System.EventHandler(this.frmPecasEstoqueAut_Load);
            this.lbxNome.ResumeLayout(false);
            this.lbxNome.PerformLayout();
            this.gbxPeriodo.ResumeLayout(false);
            this.gbxPeriodo.PerformLayout();
            this.gbxItens.ResumeLayout(false);
            this.gbxItens.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tspAcertoAutomatico.ResumeLayout(false);
            this.tspAcertoAutomatico.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ToolTip;
        private System.ComponentModel.BackgroundWorker bwrAcertoAutomatico;
        private System.ComponentModel.BackgroundWorker bwrCarregaAutoComplete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip tspAcertoAutomatico;
        private System.Windows.Forms.ToolStripButton tspExecutar;
        private System.Windows.Forms.ToolStripButton tspCancelar;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox lbxNome;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbxDescricao;
        private System.Windows.Forms.ProgressBar pgrExecuta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbxPeriodo;
        private System.Windows.Forms.TextBox tbxDatafim;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxDataini;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbxItens;
        private System.Windows.Forms.Button btnCodigoAte;
        private System.Windows.Forms.Button btnCodigoDe;
        private System.Windows.Forms.TextBox tbxCodigoAte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxCodigoDe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxCodigoNomeAte;
        private System.Windows.Forms.TextBox tbxCodigoNomeDe;
    }
}