namespace CRCorrea
{
    partial class frmDocFiscalVis
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgvDocFiscal = new System.Windows.Forms.DataGridView();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.gbxStatus = new System.Windows.Forms.GroupBox();
            this.rbnStatusT = new System.Windows.Forms.RadioButton();
            this.rbnStatusF = new System.Windows.Forms.RadioButton();
            this.rbnStatusA = new System.Windows.Forms.RadioButton();
            this.labelTipoProduto = new System.Windows.Forms.Label();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocFiscal)).BeginInit();
            this.tspTool.SuspendLayout();
            this.gbxStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDocFiscal
            // 
            this.dgvDocFiscal.AccessibleDescription = "Documentos Fiscais";
            this.dgvDocFiscal.AllowUserToAddRows = false;
            this.dgvDocFiscal.AllowUserToDeleteRows = false;
            this.dgvDocFiscal.AllowUserToResizeColumns = false;
            this.dgvDocFiscal.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDocFiscal.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDocFiscal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDocFiscal.BackgroundColor = System.Drawing.Color.White;
            this.dgvDocFiscal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDocFiscal.Location = new System.Drawing.Point(12, 75);
            this.dgvDocFiscal.MultiSelect = false;
            this.dgvDocFiscal.Name = "dgvDocFiscal";
            this.dgvDocFiscal.ReadOnly = true;
            this.dgvDocFiscal.RowHeadersVisible = false;
            this.dgvDocFiscal.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDocFiscal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDocFiscal.Size = new System.Drawing.Size(665, 555);
            this.dgvDocFiscal.TabIndex = 1;
            this.toolTip1.SetToolTip(this.dgvDocFiscal, "Documentos Fiscais");
            this.dgvDocFiscal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvDocFiscal_MouseDoubleClick);
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspExcluir,
            this.tspEscolher,
            this.tspImprimir,
            this.tspRetornar,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(0, 633);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(677, 46);
            this.tspTool.TabIndex = 2;
            // 
            // tspAlterar
            // 
            this.tspAlterar.AccessibleDescription = "Alterar";
            this.tspAlterar.AutoSize = false;
            this.tspAlterar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAlterar.Image = global::CRCorrea.Properties.Resources.Editar;
            this.tspAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAlterar.Name = "tspAlterar";
            this.tspAlterar.Size = new System.Drawing.Size(66, 42);
            this.tspAlterar.Text = "&Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
            // 
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir";
            this.tspExcluir.AutoSize = false;
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.Image = global::CRCorrea.Properties.Resources.Excluir;
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(66, 42);
            this.tspExcluir.Text = "E&xcluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
            // 
            // tspEscolher
            // 
            this.tspEscolher.AccessibleDescription = "Escolher";
            this.tspEscolher.AutoSize = false;
            this.tspEscolher.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspEscolher.Image = global::CRCorrea.Properties.Resources.Escolher;
            this.tspEscolher.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspEscolher.Name = "tspEscolher";
            this.tspEscolher.Size = new System.Drawing.Size(66, 42);
            this.tspEscolher.Text = "&Escolher";
            this.tspEscolher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspEscolher.Click += new System.EventHandler(this.tspEscolher_Click);
            // 
            // tspImprimir
            // 
            this.tspImprimir.AccessibleDescription = "Imprimir";
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = global::CRCorrea.Properties.Resources.Imprimir;
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
            this.tspRetornar.Image = global::CRCorrea.Properties.Resources.Retornar;
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
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 16);
            this.toolStripLabel1.Text = "Procurar";
            // 
            // tstbxLocalizar
            // 
            this.tstbxLocalizar.AccessibleDescription = "Procurar";
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(120, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            // 
            // gbxStatus
            // 
            this.gbxStatus.AccessibleDescription = "Status";
            this.gbxStatus.Controls.Add(this.rbnStatusT);
            this.gbxStatus.Controls.Add(this.rbnStatusF);
            this.gbxStatus.Controls.Add(this.rbnStatusA);
            this.gbxStatus.Location = new System.Drawing.Point(12, 6);
            this.gbxStatus.Name = "gbxStatus";
            this.gbxStatus.Size = new System.Drawing.Size(206, 47);
            this.gbxStatus.TabIndex = 0;
            this.gbxStatus.TabStop = false;
            this.gbxStatus.Text = "Status";
            // 
            // rbnStatusT
            // 
            this.rbnStatusT.AccessibleDescription = "Todos";
            this.rbnStatusT.AutoSize = true;
            this.rbnStatusT.Location = new System.Drawing.Point(6, 20);
            this.rbnStatusT.Name = "rbnStatusT";
            this.rbnStatusT.Size = new System.Drawing.Size(54, 17);
            this.rbnStatusT.TabIndex = 1;
            this.rbnStatusT.Text = "Todos";
            this.rbnStatusT.UseVisualStyleBackColor = true;
            this.rbnStatusT.Click += new System.EventHandler(this.rbnStatusT_Click);
            this.rbnStatusT.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnStatusT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnStatusT.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnStatusF
            // 
            this.rbnStatusF.AccessibleDescription = "Fechado";
            this.rbnStatusF.AutoSize = true;
            this.rbnStatusF.Location = new System.Drawing.Point(130, 20);
            this.rbnStatusF.Name = "rbnStatusF";
            this.rbnStatusF.Size = new System.Drawing.Size(64, 17);
            this.rbnStatusF.TabIndex = 3;
            this.rbnStatusF.Text = "Inativos";
            this.rbnStatusF.UseVisualStyleBackColor = true;
            this.rbnStatusF.Click += new System.EventHandler(this.rbnStatusF_Click);
            this.rbnStatusF.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnStatusF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnStatusF.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnStatusA
            // 
            this.rbnStatusA.AccessibleDescription = "Aberto";
            this.rbnStatusA.AutoSize = true;
            this.rbnStatusA.Checked = true;
            this.rbnStatusA.Location = new System.Drawing.Point(68, 20);
            this.rbnStatusA.Name = "rbnStatusA";
            this.rbnStatusA.Size = new System.Drawing.Size(55, 17);
            this.rbnStatusA.TabIndex = 2;
            this.rbnStatusA.TabStop = true;
            this.rbnStatusA.Text = "Ativos";
            this.rbnStatusA.UseVisualStyleBackColor = true;
            this.rbnStatusA.Click += new System.EventHandler(this.rbnStatusA_Click);
            this.rbnStatusA.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnStatusA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnStatusA.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // labelTipoProduto
            // 
            this.labelTipoProduto.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTipoProduto.Location = new System.Drawing.Point(224, 12);
            this.labelTipoProduto.Name = "labelTipoProduto";
            this.labelTipoProduto.Size = new System.Drawing.Size(430, 36);
            this.labelTipoProduto.TabIndex = 150;
            this.labelTipoProduto.Text = "Visualizando Documentos Fiscais";
            this.labelTipoProduto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tspIncluir
            // 
            this.tspIncluir.AccessibleDescription = "Incluir";
            this.tspIncluir.AutoSize = false;
            this.tspIncluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspIncluir.Image = global::CRCorrea.Properties.Resources.Incluir;
            this.tspIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspIncluir.Name = "tspIncluir";
            this.tspIncluir.Size = new System.Drawing.Size(66, 42);
            this.tspIncluir.Text = "&Incluir";
            this.tspIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspIncluir.Click += new System.EventHandler(this.tspIncluir_Click);
            // 
            // frmDocFiscalVis
            // 
            this.AccessibleDescription = "Visualizando Documentos Fiscais";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 685);
            this.Controls.Add(this.labelTipoProduto);
            this.Controls.Add(this.dgvDocFiscal);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxStatus);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDocFiscalVis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDocFiscalVis";
            this.toolTip1.SetToolTip(this, "Visualizando Documentos Fiscais");
            this.Activated += new System.EventHandler(this.frmDocFiscalVis_Activated);
            this.Load += new System.EventHandler(this.frmDocFiscalVis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocFiscal)).EndInit();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxStatus.ResumeLayout(false);
            this.gbxStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dgvDocFiscal;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.GroupBox gbxStatus;
        private System.Windows.Forms.RadioButton rbnStatusT;
        private System.Windows.Forms.RadioButton rbnStatusF;
        private System.Windows.Forms.RadioButton rbnStatusA;
        private System.Windows.Forms.Label labelTipoProduto;
    }
}