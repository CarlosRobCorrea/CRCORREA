namespace CRCorrea
{
    partial class frmOrcamentoConfirmarVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrcamentoConfirmarVis));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tsbVisualizar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.dgvOrcamento = new System.Windows.Forms.DataGridView();
            this.bwrOrcamento = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label78 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tspTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrcamento)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRegistros
            // 
            this.lblRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.Location = new System.Drawing.Point(615, 51);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(104, 13);
            this.lblRegistros.TabIndex = 137;
            this.lblRegistros.Text = "Nº Registros: 00000";
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbVisualizar,
            this.tspImprimir,
            this.tspRetornar,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(12, 290);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(694, 47);
            this.tspTool.TabIndex = 138;
            // 
            // tsbVisualizar
            // 
            this.tsbVisualizar.AccessibleDescription = "Visualizar";
            this.tsbVisualizar.AutoSize = false;
            this.tsbVisualizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbVisualizar.Image = ((System.Drawing.Image)(resources.GetObject("tsbVisualizar.Image")));
            this.tsbVisualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVisualizar.Name = "tsbVisualizar";
            this.tsbVisualizar.Size = new System.Drawing.Size(66, 42);
            this.tsbVisualizar.Text = "&Visualizar";
            this.tsbVisualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbVisualizar.Click += new System.EventHandler(this.tsbVisualizar_Click);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 47);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 16);
            this.toolStripLabel1.Text = "Procurar";
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
            // dgvOrcamento
            // 
            this.dgvOrcamento.AccessibleDescription = " Orcamento";
            this.dgvOrcamento.AllowUserToAddRows = false;
            this.dgvOrcamento.AllowUserToDeleteRows = false;
            this.dgvOrcamento.AllowUserToResizeColumns = false;
            this.dgvOrcamento.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.dgvOrcamento.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOrcamento.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvOrcamento.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvOrcamento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOrcamento.Location = new System.Drawing.Point(12, 80);
            this.dgvOrcamento.MultiSelect = false;
            this.dgvOrcamento.Name = "dgvOrcamento";
            this.dgvOrcamento.ReadOnly = true;
            this.dgvOrcamento.RowHeadersVisible = false;
            this.dgvOrcamento.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOrcamento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOrcamento.Size = new System.Drawing.Size(707, 207);
            this.dgvOrcamento.TabIndex = 139;
            this.toolTip1.SetToolTip(this.dgvOrcamento, "Visualizando Orçamento");
            this.dgvOrcamento.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrcamento_CellDoubleClick);
            this.dgvOrcamento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvOrcamento_KeyDown);
            // 
            // bwrOrcamento
            // 
            this.bwrOrcamento.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrOrcamento_DoWork);
            this.bwrOrcamento.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrOrcamento_RunWorkerCompleted);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label78.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label78.Location = new System.Drawing.Point(243, 8);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(283, 29);
            this.label78.TabIndex = 150;
            this.label78.Text = "Confirmar Orçamentos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(164, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 14);
            this.label1.TabIndex = 151;
            this.label1.Text = "(Somente pode ser confirmado orçamentos realizados nos ultimos 60 dias)";
            // 
            // frmOrcamentoConfirmarVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(746, 350);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label78);
            this.Controls.Add(this.dgvOrcamento);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.lblRegistros);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrcamentoConfirmarVis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirmar Orçamento - Visualização";
            this.Activated += new System.EventHandler(this.frmOrcamentoVis_Activated);
            this.Load += new System.EventHandler(this.frmOrcamentoConfirmarVis_Load);
            this.Shown += new System.EventHandler(this.frmOrcamentoVis_Shown);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrcamento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRegistros;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tsbVisualizar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.DataGridView dgvOrcamento;
        private System.ComponentModel.BackgroundWorker bwrOrcamento;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label1;
    }
}