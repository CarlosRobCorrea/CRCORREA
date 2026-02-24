namespace CRCorrea
{
    partial class frmMaqCtVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaqCtVis));
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.gbxFiltro = new System.Windows.Forms.GroupBox();
            this.rbnAtivoN = new System.Windows.Forms.RadioButton();
            this.rbnAtivoS = new System.Windows.Forms.RadioButton();
            this.rbnAtivo = new System.Windows.Forms.RadioButton();
            this.gbxIndice1 = new System.Windows.Forms.GroupBox();
            this.pbxMaqCtPreco = new System.Windows.Forms.PictureBox();
            this.dgvMaqCtPreco = new System.Windows.Forms.DataGridView();
            this.dgvMaqCt = new System.Windows.Forms.DataGridView();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pbxMaqCt = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tspTool.SuspendLayout();
            this.gbxFiltro.SuspendLayout();
            this.gbxIndice1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMaqCtPreco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaqCtPreco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaqCt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMaqCt)).BeginInit();
            this.SuspendLayout();
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
            this.tspEscolher,
            this.tspExcluir,
            this.tspRetornar,
            this.toolStripSeparator1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(9, 597);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(1020, 46);
            this.tspTool.TabIndex = 6;
            // 
            // tspIncluir
            // 
            this.tspIncluir.AccessibleDescription = "Incluir";
            this.tspIncluir.AutoSize = false;
            this.tspIncluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspIncluir.Image = ((System.Drawing.Image)(resources.GetObject("tspIncluir.Image")));
            this.tspIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspIncluir.Name = "tspIncluir";
            this.tspIncluir.Size = new System.Drawing.Size(62, 41);
            this.tspIncluir.Text = "Incluir";
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
            this.tspAlterar.Size = new System.Drawing.Size(62, 41);
            this.tspAlterar.Text = "Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            this.tspImprimir.Size = new System.Drawing.Size(62, 41);
            this.tspImprimir.Text = "Imprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
            // 
            // tspEscolher
            // 
            this.tspEscolher.AccessibleDescription = "Escolher";
            this.tspEscolher.AutoSize = false;
            this.tspEscolher.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspEscolher.Image = ((System.Drawing.Image)(resources.GetObject("tspEscolher.Image")));
            this.tspEscolher.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspEscolher.Name = "tspEscolher";
            this.tspEscolher.Size = new System.Drawing.Size(62, 41);
            this.tspEscolher.Text = "Escolher";
            this.tspEscolher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspEscolher.Click += new System.EventHandler(this.tspEscolher_Click);
            // 
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir";
            this.tspExcluir.AutoSize = false;
            this.tspExcluir.Enabled = false;
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tspExcluir.Image")));
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(62, 41);
            this.tspExcluir.Text = "Excluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(62, 41);
            this.tspRetornar.Text = "Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // tslbLocalizar
            // 
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(59, 14);
            this.tslbLocalizar.Text = "Procurar";
            // 
            // tstbxLocalizar
            // 
            this.tstbxLocalizar.AccessibleDescription = "Procurar";
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(215, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // gbxFiltro
            // 
            this.gbxFiltro.AccessibleDescription = "Filtro";
            this.gbxFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxFiltro.Controls.Add(this.rbnAtivoN);
            this.gbxFiltro.Controls.Add(this.rbnAtivoS);
            this.gbxFiltro.Controls.Add(this.rbnAtivo);
            this.gbxFiltro.Location = new System.Drawing.Point(12, 12);
            this.gbxFiltro.Name = "gbxFiltro";
            this.gbxFiltro.Size = new System.Drawing.Size(200, 43);
            this.gbxFiltro.TabIndex = 23;
            this.gbxFiltro.TabStop = false;
            // 
            // rbnAtivoN
            // 
            this.rbnAtivoN.AccessibleDescription = "Inativos";
            this.rbnAtivoN.AutoSize = true;
            this.rbnAtivoN.Location = new System.Drawing.Point(67, 20);
            this.rbnAtivoN.Name = "rbnAtivoN";
            this.rbnAtivoN.Size = new System.Drawing.Size(64, 17);
            this.rbnAtivoN.TabIndex = 3;
            this.rbnAtivoN.Text = "Inativos";
            this.rbnAtivoN.UseVisualStyleBackColor = true;
            this.rbnAtivoN.CheckedChanged += new System.EventHandler(this.StatusCheckedChanged);
            this.rbnAtivoN.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivoN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtivoN.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAtivoS
            // 
            this.rbnAtivoS.AccessibleDescription = "Ativos";
            this.rbnAtivoS.AutoSize = true;
            this.rbnAtivoS.Checked = true;
            this.rbnAtivoS.Location = new System.Drawing.Point(6, 20);
            this.rbnAtivoS.Name = "rbnAtivoS";
            this.rbnAtivoS.Size = new System.Drawing.Size(55, 17);
            this.rbnAtivoS.TabIndex = 1;
            this.rbnAtivoS.TabStop = true;
            this.rbnAtivoS.Text = "Ativos";
            this.rbnAtivoS.UseVisualStyleBackColor = true;
            this.rbnAtivoS.CheckedChanged += new System.EventHandler(this.StatusCheckedChanged);
            this.rbnAtivoS.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivoS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtivoS.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAtivo
            // 
            this.rbnAtivo.AccessibleDescription = "Ambos";
            this.rbnAtivo.AutoSize = true;
            this.rbnAtivo.Location = new System.Drawing.Point(138, 20);
            this.rbnAtivo.Name = "rbnAtivo";
            this.rbnAtivo.Size = new System.Drawing.Size(57, 17);
            this.rbnAtivo.TabIndex = 0;
            this.rbnAtivo.Text = "Ambos";
            this.rbnAtivo.UseVisualStyleBackColor = true;
            this.rbnAtivo.CheckedChanged += new System.EventHandler(this.StatusCheckedChanged);
            this.rbnAtivo.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtivo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxIndice1
            // 
            this.gbxIndice1.AccessibleDescription = "Evolução de Preços";
            this.gbxIndice1.Controls.Add(this.pbxMaqCtPreco);
            this.gbxIndice1.Controls.Add(this.dgvMaqCtPreco);
            this.gbxIndice1.Location = new System.Drawing.Point(629, 61);
            this.gbxIndice1.Name = "gbxIndice1";
            this.gbxIndice1.Size = new System.Drawing.Size(379, 532);
            this.gbxIndice1.TabIndex = 24;
            this.gbxIndice1.TabStop = false;
            this.gbxIndice1.Text = "Evolução de Precos";
            // 
            // pbxMaqCtPreco
            // 
            this.pbxMaqCtPreco.AccessibleDescription = "Carregando";
            this.pbxMaqCtPreco.BackColor = System.Drawing.Color.White;
            this.pbxMaqCtPreco.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxMaqCtPreco.Location = new System.Drawing.Point(22, 48);
            this.pbxMaqCtPreco.Name = "pbxMaqCtPreco";
            this.pbxMaqCtPreco.Size = new System.Drawing.Size(342, 465);
            this.pbxMaqCtPreco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxMaqCtPreco.TabIndex = 135;
            this.pbxMaqCtPreco.TabStop = false;
            // 
            // dgvMaqCtPreco
            // 
            this.dgvMaqCtPreco.AccessibleDescription = "Grid Indices Valores";
            this.dgvMaqCtPreco.AllowUserToAddRows = false;
            this.dgvMaqCtPreco.AllowUserToDeleteRows = false;
            this.dgvMaqCtPreco.AllowUserToResizeRows = false;
            this.dgvMaqCtPreco.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMaqCtPreco.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaqCtPreco.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaqCtPreco.Location = new System.Drawing.Point(6, 20);
            this.dgvMaqCtPreco.MultiSelect = false;
            this.dgvMaqCtPreco.Name = "dgvMaqCtPreco";
            this.dgvMaqCtPreco.ReadOnly = true;
            this.dgvMaqCtPreco.RowHeadersVisible = false;
            this.dgvMaqCtPreco.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaqCtPreco.Size = new System.Drawing.Size(367, 506);
            this.dgvMaqCtPreco.StandardTab = true;
            this.dgvMaqCtPreco.TabIndex = 132;
            this.dgvMaqCtPreco.TabStop = false;
            // 
            // dgvMaqCt
            // 
            this.dgvMaqCt.AccessibleDescription = "Tabela de Indices";
            this.dgvMaqCt.AllowUserToAddRows = false;
            this.dgvMaqCt.AllowUserToDeleteRows = false;
            this.dgvMaqCt.AllowUserToResizeRows = false;
            this.dgvMaqCt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMaqCt.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaqCt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaqCt.Location = new System.Drawing.Point(12, 61);
            this.dgvMaqCt.MultiSelect = false;
            this.dgvMaqCt.Name = "dgvMaqCt";
            this.dgvMaqCt.ReadOnly = true;
            this.dgvMaqCt.RowHeadersVisible = false;
            this.dgvMaqCt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaqCt.Size = new System.Drawing.Size(611, 531);
            this.dgvMaqCt.StandardTab = true;
            this.dgvMaqCt.TabIndex = 133;
            this.dgvMaqCt.TabStop = false;
            this.dgvMaqCt.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaqCt_CellClick);
            this.dgvMaqCt.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIndices_CellDoubleClick);
            // 
            // pbxMaqCt
            // 
            this.pbxMaqCt.AccessibleDescription = "Carregando";
            this.pbxMaqCt.BackColor = System.Drawing.Color.White;
            this.pbxMaqCt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxMaqCt.Location = new System.Drawing.Point(54, 92);
            this.pbxMaqCt.Name = "pbxMaqCt";
            this.pbxMaqCt.Size = new System.Drawing.Size(535, 490);
            this.pbxMaqCt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxMaqCt.TabIndex = 134;
            this.pbxMaqCt.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(286, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 19);
            this.label1.TabIndex = 143;
            this.label1.Text = "Visualizar Centro de Custos";
            // 
            // frmMaqCtVis
            // 
            this.AccessibleDescription = "Visualizar Divergencias";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbxMaqCt);
            this.Controls.Add(this.dgvMaqCt);
            this.Controls.Add(this.gbxIndice1);
            this.Controls.Add(this.gbxFiltro);
            this.Controls.Add(this.tspTool);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMaqCtVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "282";
            this.Text = "Indíces Econômicos - Visualização";
            this.ToolTip.SetToolTip(this, "Indíces Econômicos - Visualização");
            this.Activated += new System.EventHandler(this.frmIndicesVis_Activated);
            this.Load += new System.EventHandler(this.frmMaqCtVis_Load);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxFiltro.ResumeLayout(false);
            this.gbxFiltro.PerformLayout();
            this.gbxIndice1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxMaqCtPreco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaqCtPreco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaqCt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMaqCt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxFiltro;
        private System.Windows.Forms.RadioButton rbnAtivoN;
        private System.Windows.Forms.RadioButton rbnAtivoS;
        private System.Windows.Forms.RadioButton rbnAtivo;
        private System.Windows.Forms.GroupBox gbxIndice1;
        private System.Windows.Forms.DataGridView dgvMaqCtPreco;
        private System.Windows.Forms.DataGridView dgvMaqCt;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.PictureBox pbxMaqCtPreco;
        private System.Windows.Forms.PictureBox pbxMaqCt;
        private System.Windows.Forms.Label label1;
    }
}