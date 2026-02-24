namespace CRCorrea
{
    partial class frmCfopPes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCfopPes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.gbxFiltro = new System.Windows.Forms.GroupBox();
            this.rbnAtivoN = new System.Windows.Forms.RadioButton();
            this.rbnAtivoS = new System.Windows.Forms.RadioButton();
            this.rbnAtivo = new System.Windows.Forms.RadioButton();
            this.dgvCfop = new System.Windows.Forms.DataGridView();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.bwrCfop = new System.ComponentModel.BackgroundWorker();
            this.tspTool.SuspendLayout();
            this.gbxFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCfop)).BeginInit();
            this.SuspendLayout();
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspImprimir,
            this.tspEscolher,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(0, 638);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(1020, 47);
            this.tspTool.TabIndex = 18;
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
            // tspEscolher
            // 
            this.tspEscolher.AccessibleDescription = "Ecolher";
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
            this.tspSeparador1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.tspSeparador1.Name = "tspSeparador1";
            this.tspSeparador1.Size = new System.Drawing.Size(6, 47);
            // 
            // tslbLocalizar
            // 
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(65, 16);
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
            this.tstbxLocalizar.Size = new System.Drawing.Size(280, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // gbxFiltro
            // 
            this.gbxFiltro.AccessibleDescription = "Filtro- Opção";
            this.gbxFiltro.Controls.Add(this.rbnAtivoN);
            this.gbxFiltro.Controls.Add(this.rbnAtivoS);
            this.gbxFiltro.Controls.Add(this.rbnAtivo);
            this.gbxFiltro.Location = new System.Drawing.Point(12, 12);
            this.gbxFiltro.Name = "gbxFiltro";
            this.gbxFiltro.Size = new System.Drawing.Size(228, 48);
            this.gbxFiltro.TabIndex = 17;
            this.gbxFiltro.TabStop = false;
            this.gbxFiltro.Text = "Filtro=Opção(Todos/Ativo/Inativo)";
            this.toolTip.SetToolTip(this.gbxFiltro, "Grupo Filtro");
            // 
            // rbnAtivoN
            // 
            this.rbnAtivoN.AccessibleDescription = "Só Inativos";
            this.rbnAtivoN.AutoSize = true;
            this.rbnAtivoN.Location = new System.Drawing.Point(142, 20);
            this.rbnAtivoN.Name = "rbnAtivoN";
            this.rbnAtivoN.Size = new System.Drawing.Size(79, 17);
            this.rbnAtivoN.TabIndex = 3;
            this.rbnAtivoN.Text = "Só Inativos";
            this.toolTip.SetToolTip(this.rbnAtivoN, "Opção Inativos");
            this.rbnAtivoN.UseVisualStyleBackColor = true;
            this.rbnAtivoN.Click += new System.EventHandler(this.rbnFiltro);
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
            this.toolTip.SetToolTip(this.rbnAtivoS, "Opção Ativos");
            this.rbnAtivoS.UseVisualStyleBackColor = true;
            this.rbnAtivoS.Click += new System.EventHandler(this.rbnFiltro);
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
            this.toolTip.SetToolTip(this.rbnAtivo, "Opção Todos");
            this.rbnAtivo.UseVisualStyleBackColor = true;
            this.rbnAtivo.Click += new System.EventHandler(this.rbnFiltro);
            this.rbnAtivo.CheckedChanged += new System.EventHandler(this.rbnAtivo_CheckedChanged);
            // 
            // dgvCfop
            // 
            this.dgvCfop.AccessibleDescription = "Grid Cfop";
            this.dgvCfop.AllowUserToAddRows = false;
            this.dgvCfop.AllowUserToDeleteRows = false;
            this.dgvCfop.AllowUserToResizeColumns = false;
            this.dgvCfop.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvCfop.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCfop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCfop.BackgroundColor = System.Drawing.Color.White;
            this.dgvCfop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCfop.Location = new System.Drawing.Point(12, 66);
            this.dgvCfop.MultiSelect = false;
            this.dgvCfop.Name = "dgvCfop";
            this.dgvCfop.ReadOnly = true;
            this.dgvCfop.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCfop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCfop.Size = new System.Drawing.Size(996, 569);
            this.dgvCfop.TabIndex = 16;
            this.toolTip.SetToolTip(this.dgvCfop, " CFOP - Pesquisa");
            this.dgvCfop.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCfop_CellDoubleClick);
            // 
            // bwrCfop
            // 
            this.bwrCfop.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrCfop_DoWork);
            this.bwrCfop.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrCfop_RunWorkerCompleted);
            // 
            // frmCfopPes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxFiltro);
            this.Controls.Add(this.dgvCfop);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCfopPes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "51";
            this.Text = "CFOP -Pesquisando";
            this.toolTip.SetToolTip(this, "CFOP -Pesquisando");
            this.Load += new System.EventHandler(this.frmCfopPes_Load);
            this.Shown += new System.EventHandler(this.frmCfopPes_Shown);
            this.Activated += new System.EventHandler(this.rbnFiltro);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxFiltro.ResumeLayout(false);
            this.gbxFiltro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCfop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxFiltro;
        private System.Windows.Forms.RadioButton rbnAtivoN;
        private System.Windows.Forms.RadioButton rbnAtivoS;
        private System.Windows.Forms.RadioButton rbnAtivo;
        private System.Windows.Forms.DataGridView dgvCfop;
        private System.Windows.Forms.ToolTip toolTip;
        private System.ComponentModel.BackgroundWorker bwrCfop;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
    }
}