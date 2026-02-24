namespace CRCorrea
{
    partial class frmSituacaocobrancacodVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSituacaocobrancacodVis));
            this.ttpSituacaocobrancacod = new System.Windows.Forms.ToolTip(this.components);
            this.dgvSituacaocobrancacod = new System.Windows.Forms.DataGridView();
            this.dgvSituacaocobrancacod1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
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
            this.label3 = new System.Windows.Forms.Label();
            this.bwrSituacaocobrancacod = new System.ComponentModel.BackgroundWorker();
            this.bwrSituacaocobrancacod1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSituacaocobrancacod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSituacaocobrancacod1)).BeginInit();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSituacaocobrancacod
            // 
            this.dgvSituacaocobrancacod.AccessibleDescription = "Códigos de Cobrança";
            this.dgvSituacaocobrancacod.AllowUserToAddRows = false;
            this.dgvSituacaocobrancacod.AllowUserToDeleteRows = false;
            this.dgvSituacaocobrancacod.AllowUserToResizeColumns = false;
            this.dgvSituacaocobrancacod.AllowUserToResizeRows = false;
            this.dgvSituacaocobrancacod.BackgroundColor = System.Drawing.Color.White;
            this.dgvSituacaocobrancacod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSituacaocobrancacod.Location = new System.Drawing.Point(12, 29);
            this.dgvSituacaocobrancacod.MultiSelect = false;
            this.dgvSituacaocobrancacod.Name = "dgvSituacaocobrancacod";
            this.dgvSituacaocobrancacod.ReadOnly = true;
            this.dgvSituacaocobrancacod.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSituacaocobrancacod.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSituacaocobrancacod.Size = new System.Drawing.Size(996, 267);
            this.dgvSituacaocobrancacod.TabIndex = 0;
            this.ttpSituacaocobrancacod.SetToolTip(this.dgvSituacaocobrancacod, "Grade de Códigos Iternos de Cobrança (Aplisoft)");
            this.dgvSituacaocobrancacod.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSituacaocobrancacod_CellClick);
            this.dgvSituacaocobrancacod.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSituacaocobrancacod_CellDoubleClick);
            // 
            // dgvSituacaocobrancacod1
            // 
            this.dgvSituacaocobrancacod1.AccessibleDescription = "Sub-Códigos do Código";
            this.dgvSituacaocobrancacod1.AllowUserToAddRows = false;
            this.dgvSituacaocobrancacod1.AllowUserToDeleteRows = false;
            this.dgvSituacaocobrancacod1.AllowUserToResizeColumns = false;
            this.dgvSituacaocobrancacod1.AllowUserToResizeRows = false;
            this.dgvSituacaocobrancacod1.BackgroundColor = System.Drawing.Color.White;
            this.dgvSituacaocobrancacod1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSituacaocobrancacod1.Location = new System.Drawing.Point(12, 319);
            this.dgvSituacaocobrancacod1.MultiSelect = false;
            this.dgvSituacaocobrancacod1.Name = "dgvSituacaocobrancacod1";
            this.dgvSituacaocobrancacod1.ReadOnly = true;
            this.dgvSituacaocobrancacod1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSituacaocobrancacod1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSituacaocobrancacod1.Size = new System.Drawing.Size(996, 270);
            this.dgvSituacaocobrancacod1.TabIndex = 3;
            this.ttpSituacaocobrancacod.SetToolTip(this.dgvSituacaocobrancacod1, "Grade dos Sub-Códigos do Código selecionado");
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(996, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Códigos Internos de Cobrança (Aplisoft)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.tspTool.Location = new System.Drawing.Point(9, 592);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(679, 47);
            this.tspTool.TabIndex = 43;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
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
            // tspEscolher
            // 
            this.tspEscolher.AccessibleDescription = "Escolher";
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
            this.tstbxLocalizar.AccessibleDescription = "Procurar";
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(200, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 299);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(996, 17);
            this.label3.TabIndex = 44;
            this.label3.Text = "Sub-Códigos do Código";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bwrSituacaocobrancacod
            // 
            this.bwrSituacaocobrancacod.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrSituacaocobrancacod_DoWork);
            this.bwrSituacaocobrancacod.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrSituacaocobrancacod_RunWorkerCompleted);
            // 
            // bwrSituacaocobrancacod1
            // 
            this.bwrSituacaocobrancacod1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrSituacaocobrancacod1_DoWork);
            this.bwrSituacaocobrancacod1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrSituacaocobrancacod1_RunWorkerCompleted);
            // 
            // frmSituacaocobrancacodVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.dgvSituacaocobrancacod1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSituacaocobrancacod);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSituacaocobrancacodVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "205";
            this.Text = "Situação  Cobrança Código - Visualização";
            this.ttpSituacaocobrancacod.SetToolTip(this, "Codigo Cobranca (Aplisoft) - Visualizar");
            this.Activated += new System.EventHandler(this.frmSituacaocobrancacodVis_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSituacaocobrancacodVis_FormClosed);
            this.Load += new System.EventHandler(this.frmSituacaocobrancacodVis_Load);
            this.Shown += new System.EventHandler(this.frmSituacaocobrancacodVis_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSituacaocobrancacod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSituacaocobrancacod1)).EndInit();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpSituacaocobrancacod;
        private System.Windows.Forms.DataGridView dgvSituacaocobrancacod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvSituacaocobrancacod1;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bwrSituacaocobrancacod;
        private System.ComponentModel.BackgroundWorker bwrSituacaocobrancacod1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
    }
}