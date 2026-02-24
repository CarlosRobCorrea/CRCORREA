namespace CRCorrea
{
    partial class frmIpiVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIpiVis));
            this.dgvIpi = new System.Windows.Forms.DataGridView();
            this.gbxFiltro = new System.Windows.Forms.GroupBox();
            this.rbnResumoN = new System.Windows.Forms.RadioButton();
            this.rbnResumoS = new System.Windows.Forms.RadioButton();
            this.rbnResumoT = new System.Windows.Forms.RadioButton();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxAtivo = new System.Windows.Forms.GroupBox();
            this.rbnAtivoN = new System.Windows.Forms.RadioButton();
            this.rbnAtivoS = new System.Windows.Forms.RadioButton();
            this.rbnAtivo = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpi)).BeginInit();
            this.gbxFiltro.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.gbxAtivo.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvIpi
            // 
            this.dgvIpi.AccessibleDescription = "Grid Ipi";
            this.dgvIpi.AllowUserToAddRows = false;
            this.dgvIpi.AllowUserToDeleteRows = false;
            this.dgvIpi.AllowUserToResizeRows = false;
            this.dgvIpi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvIpi.BackgroundColor = System.Drawing.Color.White;
            this.dgvIpi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvIpi.Location = new System.Drawing.Point(12, 66);
            this.dgvIpi.MultiSelect = false;
            this.dgvIpi.Name = "dgvIpi";
            this.dgvIpi.ReadOnly = true;
            this.dgvIpi.RowHeadersVisible = false;
            this.dgvIpi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvIpi.Size = new System.Drawing.Size(996, 497);
            this.dgvIpi.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dgvIpi, "Grade - IPI");
            this.dgvIpi.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvIpi_CellMouseDoubleClick);
            // 
            // gbxFiltro
            // 
            this.gbxFiltro.AccessibleDescription = "Tipo do Código NBM";
            this.gbxFiltro.Controls.Add(this.rbnResumoN);
            this.gbxFiltro.Controls.Add(this.rbnResumoS);
            this.gbxFiltro.Controls.Add(this.rbnResumoT);
            this.gbxFiltro.Location = new System.Drawing.Point(246, 12);
            this.gbxFiltro.Name = "gbxFiltro";
            this.gbxFiltro.Size = new System.Drawing.Size(233, 48);
            this.gbxFiltro.TabIndex = 2;
            this.gbxFiltro.TabStop = false;
            this.gbxFiltro.Text = "Tipo do Código NBM";
            this.toolTip1.SetToolTip(this.gbxFiltro, "Grupo - Filtro");
            // 
            // rbnResumoN
            // 
            this.rbnResumoN.AccessibleDescription = "Só Normal";
            this.rbnResumoN.AutoSize = true;
            this.rbnResumoN.Checked = true;
            this.rbnResumoN.Location = new System.Drawing.Point(150, 20);
            this.rbnResumoN.Name = "rbnResumoN";
            this.rbnResumoN.Size = new System.Drawing.Size(73, 17);
            this.rbnResumoN.TabIndex = 3;
            this.rbnResumoN.TabStop = true;
            this.rbnResumoN.Tag = "";
            this.rbnResumoN.Text = "Só Normal";
            this.toolTip1.SetToolTip(this.rbnResumoN, "Só Normal");
            this.rbnResumoN.UseVisualStyleBackColor = true;
            this.rbnResumoN.Click += new System.EventHandler(this.rbnResumoN_Click);
            // 
            // rbnResumoS
            // 
            this.rbnResumoS.AccessibleDescription = "Só Resumo";
            this.rbnResumoS.AutoSize = true;
            this.rbnResumoS.Location = new System.Drawing.Point(66, 20);
            this.rbnResumoS.Name = "rbnResumoS";
            this.rbnResumoS.Size = new System.Drawing.Size(78, 17);
            this.rbnResumoS.TabIndex = 1;
            this.rbnResumoS.Tag = "";
            this.rbnResumoS.Text = "Só Resumo";
            this.toolTip1.SetToolTip(this.rbnResumoS, "Só Resumo");
            this.rbnResumoS.UseVisualStyleBackColor = true;
            this.rbnResumoS.Click += new System.EventHandler(this.rbnResumoS_Click);
            // 
            // rbnResumoT
            // 
            this.rbnResumoT.AccessibleDescription = "Todos";
            this.rbnResumoT.AutoSize = true;
            this.rbnResumoT.Location = new System.Drawing.Point(6, 20);
            this.rbnResumoT.Name = "rbnResumoT";
            this.rbnResumoT.Size = new System.Drawing.Size(54, 17);
            this.rbnResumoT.TabIndex = 0;
            this.rbnResumoT.Tag = "";
            this.rbnResumoT.Text = "Todos";
            this.toolTip1.SetToolTip(this.rbnResumoT, "Todos");
            this.rbnResumoT.UseVisualStyleBackColor = true;
            this.rbnResumoT.Click += new System.EventHandler(this.rbnResumoT_Click);
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspImprimir,
            this.tspExcluir,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(12, 566);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(996, 47);
            this.tspTool.TabIndex = 43;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            this.toolTip1.SetToolTip(this.tspTool, "Barra de Opções");
            // 
            // tspIncluir
            // 
            this.tspIncluir.AccessibleDescription = "Incluir";
            this.tspIncluir.AutoSize = false;
            this.tspIncluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspIncluir.Image = ((System.Drawing.Image)(resources.GetObject("tspIncluir.Image")));
            this.tspIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspIncluir.Name = "tspIncluir";
            this.tspIncluir.Size = new System.Drawing.Size(66, 42);
            this.tspIncluir.Text = "&Incluir";
            this.tspIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspIncluir.Click += new System.EventHandler(this.tspIncluir_Click);
            // 
            // tspAlterar
            // 
            this.tspAlterar.AccessibleDescription = "Alterar";
            this.tspAlterar.AutoSize = false;
            this.tspAlterar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAlterar.Image = ((System.Drawing.Image)(resources.GetObject("tspAlterar.Image")));
            this.tspAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAlterar.Name = "tspAlterar";
            this.tspAlterar.Size = new System.Drawing.Size(66, 42);
            this.tspAlterar.Text = "&Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.ToolTipText = "Alterar";
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
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
            this.tspImprimir.Text = "I&mprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
            // 
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir";
            this.tspExcluir.AutoSize = false;
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tspExcluir.Image")));
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(66, 42);
            this.tspExcluir.Text = "E&xcluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
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
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // tspSeparador1
            // 
            this.tspSeparador1.AutoSize = false;
            this.tspSeparador1.Name = "tspSeparador1";
            this.tspSeparador1.Size = new System.Drawing.Size(6, 47);
            // 
            // tslbLocalizar
            // 
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(69, 16);
            this.tslbLocalizar.Text = "Procurar:";
            // 
            // tstbxLocalizar
            // 
            this.tstbxLocalizar.AccessibleDescription = "Procurar";
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(120, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            this.tstbxLocalizar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tstbxLocalizar_MouseUp);
            // 
            // gbxAtivo
            // 
            this.gbxAtivo.AccessibleDescription = "Filtro";
            this.gbxAtivo.Controls.Add(this.rbnAtivoN);
            this.gbxAtivo.Controls.Add(this.rbnAtivoS);
            this.gbxAtivo.Controls.Add(this.rbnAtivo);
            this.gbxAtivo.Location = new System.Drawing.Point(12, 12);
            this.gbxAtivo.Name = "gbxAtivo";
            this.gbxAtivo.Size = new System.Drawing.Size(228, 48);
            this.gbxAtivo.TabIndex = 1;
            this.gbxAtivo.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxAtivo, "Grupo Ativo");
            // 
            // rbnAtivoN
            // 
            this.rbnAtivoN.AccessibleDescription = "Só Inativos";
            this.rbnAtivoN.AutoSize = true;
            this.rbnAtivoN.Location = new System.Drawing.Point(143, 20);
            this.rbnAtivoN.Name = "rbnAtivoN";
            this.rbnAtivoN.Size = new System.Drawing.Size(79, 17);
            this.rbnAtivoN.TabIndex = 3;
            this.rbnAtivoN.Text = "Só Inativos";
            this.toolTip1.SetToolTip(this.rbnAtivoN, "Grupo Filtro Tipo: Inativos");
            this.rbnAtivoN.UseVisualStyleBackColor = true;
            this.rbnAtivoN.CheckedChanged += new System.EventHandler(this.rbnAtivoN_CheckedChanged);
            // 
            // rbnAtivoS
            // 
            this.rbnAtivoS.AccessibleDescription = "Só Ativos";
            this.rbnAtivoS.AutoSize = true;
            this.rbnAtivoS.Checked = true;
            this.rbnAtivoS.Location = new System.Drawing.Point(66, 20);
            this.rbnAtivoS.Name = "rbnAtivoS";
            this.rbnAtivoS.Size = new System.Drawing.Size(70, 17);
            this.rbnAtivoS.TabIndex = 1;
            this.rbnAtivoS.TabStop = true;
            this.rbnAtivoS.Text = "Só Ativos";
            this.toolTip1.SetToolTip(this.rbnAtivoS, "Grupo Filtro Tipo: Ativos");
            this.rbnAtivoS.UseVisualStyleBackColor = true;
            this.rbnAtivoS.CheckedChanged += new System.EventHandler(this.rbnAtivoS_CheckedChanged);
            // 
            // rbnAtivo
            // 
            this.rbnAtivo.AccessibleDescription = "Todos";
            this.rbnAtivo.AutoSize = true;
            this.rbnAtivo.Location = new System.Drawing.Point(6, 20);
            this.rbnAtivo.Name = "rbnAtivo";
            this.rbnAtivo.Size = new System.Drawing.Size(54, 17);
            this.rbnAtivo.TabIndex = 0;
            this.rbnAtivo.Text = "Todos";
            this.toolTip1.SetToolTip(this.rbnAtivo, "Grupo Filtro Tipo: Todos");
            this.rbnAtivo.UseVisualStyleBackColor = true;
            this.rbnAtivo.Click += new System.EventHandler(this.rbnAtivo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(535, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(452, 19);
            this.label1.TabIndex = 141;
            this.label1.Text = "N.C.M. / I.P.I. - Imposto sobre Produto Industrializado";
            // 
            // frmIpiVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbxAtivo);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxFiltro);
            this.Controls.Add(this.dgvIpi);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 50);
            this.Name = "frmIpiVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "537";
            this.Text = "IPI  2 - Visualização";
            this.toolTip1.SetToolTip(this, "IPI  2 - Visualização");
            this.Activated += new System.EventHandler(this.frmIpiVis_Activated);
            this.Load += new System.EventHandler(this.frmIpiVis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpi)).EndInit();
            this.gbxFiltro.ResumeLayout(false);
            this.gbxFiltro.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxAtivo.ResumeLayout(false);
            this.gbxAtivo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvIpi;
        private System.Windows.Forms.GroupBox gbxFiltro;
        private System.Windows.Forms.RadioButton rbnResumoN;
        private System.Windows.Forms.RadioButton rbnResumoS;
        private System.Windows.Forms.RadioButton rbnResumoT;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbxAtivo;
        private System.Windows.Forms.RadioButton rbnAtivoN;
        private System.Windows.Forms.RadioButton rbnAtivoS;
        private System.Windows.Forms.RadioButton rbnAtivo;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.Label label1;
    }
}