namespace CRCorrea
{
    partial class frmSituacaoCobrancaCod1Pes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSituacaoCobrancaCod1Pes));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxProjeto = new System.Windows.Forms.GroupBox();
            this.dgvSituacao = new System.Windows.Forms.DataGridView();
            this.tbxPesquisa = new System.Windows.Forms.TextBox();
            this.gbxFiltro = new System.Windows.Forms.GroupBox();
            this.rbnAtivoN = new System.Windows.Forms.RadioButton();
            this.rbnAtivoS = new System.Windows.Forms.RadioButton();
            this.rbnAtivo = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspConsultar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspImprimirMnu = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.gbxProjeto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSituacao)).BeginInit();
            this.gbxFiltro.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxProjeto
            // 
            this.gbxProjeto.Controls.Add(this.dgvSituacao);
            this.gbxProjeto.Location = new System.Drawing.Point(12, 59);
            this.gbxProjeto.Name = "gbxProjeto";
            this.gbxProjeto.Size = new System.Drawing.Size(996, 552);
            this.gbxProjeto.TabIndex = 43;
            this.gbxProjeto.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxProjeto, "Visualizar Centros de Custos");
            // 
            // dgvSituacao
            // 
            this.dgvSituacao.AllowUserToAddRows = false;
            this.dgvSituacao.AllowUserToDeleteRows = false;
            this.dgvSituacao.AllowUserToOrderColumns = true;
            this.dgvSituacao.BackgroundColor = System.Drawing.Color.White;
            this.dgvSituacao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSituacao.Location = new System.Drawing.Point(6, 20);
            this.dgvSituacao.MultiSelect = false;
            this.dgvSituacao.Name = "dgvSituacao";
            this.dgvSituacao.ReadOnly = true;
            this.dgvSituacao.RowHeadersVisible = false;
            this.dgvSituacao.Size = new System.Drawing.Size(977, 526);
            this.dgvSituacao.StandardTab = true;
            this.dgvSituacao.TabIndex = 3;
            // 
            // tbxPesquisa
            // 
            this.tbxPesquisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxPesquisa.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPesquisa.Location = new System.Drawing.Point(783, 657);
            this.tbxPesquisa.Name = "tbxPesquisa";
            this.tbxPesquisa.Size = new System.Drawing.Size(178, 21);
            this.tbxPesquisa.TabIndex = 41;
            this.toolTip1.SetToolTip(this.tbxPesquisa, "Pesquisa avançada");
            // 
            // gbxFiltro
            // 
            this.gbxFiltro.Controls.Add(this.rbnAtivoN);
            this.gbxFiltro.Controls.Add(this.rbnAtivoS);
            this.gbxFiltro.Controls.Add(this.rbnAtivo);
            this.gbxFiltro.Location = new System.Drawing.Point(12, 7);
            this.gbxFiltro.Name = "gbxFiltro";
            this.gbxFiltro.Size = new System.Drawing.Size(232, 45);
            this.gbxFiltro.TabIndex = 39;
            this.gbxFiltro.TabStop = false;
            this.gbxFiltro.Text = "Filtro=Opção(Todos/Ativo/Inativo)";
            this.toolTip1.SetToolTip(this.gbxFiltro, "Filtro=Opção(Todos/Ativo/Inativo)");
            // 
            // rbnAtivoN
            // 
            this.rbnAtivoN.AutoSize = true;
            this.rbnAtivoN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnAtivoN.Location = new System.Drawing.Point(142, 20);
            this.rbnAtivoN.Name = "rbnAtivoN";
            this.rbnAtivoN.Size = new System.Drawing.Size(79, 17);
            this.rbnAtivoN.TabIndex = 3;
            this.rbnAtivoN.Text = "Só Inativos";
            this.toolTip1.SetToolTip(this.rbnAtivoN, "Só Inativos");
            this.rbnAtivoN.UseVisualStyleBackColor = true;
            // 
            // rbnAtivoS
            // 
            this.rbnAtivoS.AutoSize = true;
            this.rbnAtivoS.Checked = true;
            this.rbnAtivoS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnAtivoS.Location = new System.Drawing.Point(66, 20);
            this.rbnAtivoS.Name = "rbnAtivoS";
            this.rbnAtivoS.Size = new System.Drawing.Size(70, 17);
            this.rbnAtivoS.TabIndex = 1;
            this.rbnAtivoS.TabStop = true;
            this.rbnAtivoS.Text = "Só Ativos";
            this.toolTip1.SetToolTip(this.rbnAtivoS, "Só Ativos");
            this.rbnAtivoS.UseVisualStyleBackColor = true;
            // 
            // rbnAtivo
            // 
            this.rbnAtivo.AutoSize = true;
            this.rbnAtivo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnAtivo.Location = new System.Drawing.Point(6, 20);
            this.rbnAtivo.Name = "rbnAtivo";
            this.rbnAtivo.Size = new System.Drawing.Size(54, 17);
            this.rbnAtivo.TabIndex = 0;
            this.rbnAtivo.Text = "Todos";
            this.toolTip1.SetToolTip(this.rbnAtivo, "Visualiza Todos");
            this.rbnAtivo.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(720, 660);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "Pesquisa";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleDescription = "c";
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspConsultar,
            this.tspImprimir,
            this.tspImprimirMnu,
            this.tspEscolher,
            this.tspRetornar});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 634);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1020, 51);
            this.toolStrip1.TabIndex = 40;
            // 
            // tspConsultar
            // 
            this.tspConsultar.AutoSize = false;
            this.tspConsultar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspConsultar.Image = ((System.Drawing.Image)(resources.GetObject("tspConsultar.Image")));
            this.tspConsultar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspConsultar.Name = "tspConsultar";
            this.tspConsultar.Size = new System.Drawing.Size(66, 42);
            this.tspConsultar.Text = "&Consultar";
            this.tspConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspImprimir
            // 
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimir.Image")));
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(66, 42);
            this.tspImprimir.Text = "I&mprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
            // 
            // tspImprimirMnu
            // 
            this.tspImprimirMnu.AutoSize = false;
            this.tspImprimirMnu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimirMnu.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimirMnu.Image")));
            this.tspImprimirMnu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimirMnu.Name = "tspImprimirMnu";
            this.tspImprimirMnu.Size = new System.Drawing.Size(96, 42);
            this.tspImprimirMnu.Text = "&ImprimirMnu";
            this.tspImprimirMnu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspEscolher
            // 
            this.tspEscolher.AutoSize = false;
            this.tspEscolher.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspEscolher.Image = ((System.Drawing.Image)(resources.GetObject("tspEscolher.Image")));
            this.tspEscolher.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspEscolher.Name = "tspEscolher";
            this.tspEscolher.Size = new System.Drawing.Size(66, 42);
            this.tspEscolher.Text = "&Escolher";
            this.tspEscolher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspEscolher.Click += new System.EventHandler(this.tspEscolher_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(268, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(740, 30);
            this.label2.TabIndex = 45;
            this.label2.Text = "Historicos de Cobrança - Pesquisa";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSituacaoCobrancaCod1Pes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbxProjeto);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbxPesquisa);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gbxFiltro);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSituacaoCobrancaCod1Pes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Projeto - Pesquisa";
            this.toolTip1.SetToolTip(this, "Projeto - Pesquisa");
            this.Activated += new System.EventHandler(this.frmSituacaoCobrancaCod1Pes_Activated);
            this.gbxProjeto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSituacao)).EndInit();
            this.gbxFiltro.ResumeLayout(false);
            this.gbxFiltro.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbxProjeto;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxPesquisa;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxFiltro;
        private System.Windows.Forms.RadioButton rbnAtivoN;
        private System.Windows.Forms.RadioButton rbnAtivoS;
        private System.Windows.Forms.RadioButton rbnAtivo;
        private System.Windows.Forms.ToolStripButton tspConsultar;
        private System.Windows.Forms.ToolStripButton tspImprimirMnu;
        private System.Windows.Forms.DataGridView dgvSituacao;
        private System.Windows.Forms.Label label2;
    }
}

