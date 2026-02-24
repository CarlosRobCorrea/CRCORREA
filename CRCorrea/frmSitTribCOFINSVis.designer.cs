namespace CRCorrea
{
    partial class frmSitTribCOFINSVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSitTribCOFINSVis));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tspSitTribCOFINS = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.ttpSitTribCOFINS = new System.Windows.Forms.ToolTip(this.components);
            this.gbxSitTribCOFINS = new System.Windows.Forms.GroupBox();
            this.pbxSitTribCOFINS = new System.Windows.Forms.PictureBox();
            this.dgvSitTribCOFINS_fil = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bwrSitTribCOFINS = new System.ComponentModel.BackgroundWorker();
            this.tspSitTribCOFINS.SuspendLayout();
            this.gbxSitTribCOFINS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSitTribCOFINS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSitTribCOFINS_fil)).BeginInit();
            this.SuspendLayout();
            // 
            // tspSitTribCOFINS
            // 
            this.tspSitTribCOFINS.AccessibleDescription = "Barra de Opções";
            this.tspSitTribCOFINS.AllowMerge = false;
            this.tspSitTribCOFINS.AutoSize = false;
            this.tspSitTribCOFINS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspSitTribCOFINS.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspSitTribCOFINS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspEscolher,
            this.tspImprimir,
            this.tspExcluir,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspSitTribCOFINS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspSitTribCOFINS.Location = new System.Drawing.Point(0, 638);
            this.tspSitTribCOFINS.Name = "tspSitTribCOFINS";
            this.tspSitTribCOFINS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspSitTribCOFINS.Size = new System.Drawing.Size(1020, 47);
            this.tspSitTribCOFINS.TabIndex = 1;
            this.ttpSitTribCOFINS.SetToolTip(this.tspSitTribCOFINS, "Menu");
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
            this.tspAlterar.Size = new System.Drawing.Size(66, 42);
            this.tspAlterar.Text = "Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
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
            this.tspEscolher.ToolTipText = "Escolher Cracha";
            this.tspEscolher.Click += new System.EventHandler(this.tspEscolher_Click);
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
            this.tspImprimir.Text = "Imprimir";
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
            this.tspExcluir.Text = "Excluir";
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
            this.tspRetornar.Text = "Retornar";
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
            this.tstbxLocalizar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // gbxSitTribCOFINS
            // 
            this.gbxSitTribCOFINS.AccessibleDescription = "Situação Tributaria COFINS";
            this.gbxSitTribCOFINS.Controls.Add(this.pbxSitTribCOFINS);
            this.gbxSitTribCOFINS.Controls.Add(this.dgvSitTribCOFINS_fil);
            this.gbxSitTribCOFINS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSitTribCOFINS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbxSitTribCOFINS.Location = new System.Drawing.Point(12, 12);
            this.gbxSitTribCOFINS.Name = "gbxSitTribCOFINS";
            this.gbxSitTribCOFINS.Size = new System.Drawing.Size(996, 623);
            this.gbxSitTribCOFINS.TabIndex = 1;
            this.gbxSitTribCOFINS.TabStop = false;
            this.gbxSitTribCOFINS.Text = "Situação Tributaria COFINS";
            this.ttpSitTribCOFINS.SetToolTip(this.gbxSitTribCOFINS, "Grupo SitTribCOFINS");
            // 
            // pbxSitTribCOFINS
            // 
            this.pbxSitTribCOFINS.AccessibleDescription = "Carregando Cofins";
            this.pbxSitTribCOFINS.BackColor = System.Drawing.Color.White;
            this.pbxSitTribCOFINS.Image = ((System.Drawing.Image)(resources.GetObject("pbxSitTribCOFINS.Image")));
            this.pbxSitTribCOFINS.Location = new System.Drawing.Point(6, 21);
            this.pbxSitTribCOFINS.Name = "pbxSitTribCOFINS";
            this.pbxSitTribCOFINS.Size = new System.Drawing.Size(987, 596);
            this.pbxSitTribCOFINS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxSitTribCOFINS.TabIndex = 138;
            this.pbxSitTribCOFINS.TabStop = false;
            this.ttpSitTribCOFINS.SetToolTip(this.pbxSitTribCOFINS, "Imagem");
            this.pbxSitTribCOFINS.Visible = false;
            // 
            // dgvSitTribCOFINS_fil
            // 
            this.dgvSitTribCOFINS_fil.AccessibleDescription = "Cofins";
            this.dgvSitTribCOFINS_fil.AllowUserToAddRows = false;
            this.dgvSitTribCOFINS_fil.AllowUserToDeleteRows = false;
            this.dgvSitTribCOFINS_fil.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvSitTribCOFINS_fil.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSitTribCOFINS_fil.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvSitTribCOFINS_fil.BackgroundColor = System.Drawing.Color.White;
            this.dgvSitTribCOFINS_fil.ColumnHeadersHeight = 28;
            this.dgvSitTribCOFINS_fil.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.CODIGO,
            this.NOME});
            this.dgvSitTribCOFINS_fil.Location = new System.Drawing.Point(6, 21);
            this.dgvSitTribCOFINS_fil.MultiSelect = false;
            this.dgvSitTribCOFINS_fil.Name = "dgvSitTribCOFINS_fil";
            this.dgvSitTribCOFINS_fil.ReadOnly = true;
            this.dgvSitTribCOFINS_fil.RowHeadersVisible = false;
            this.dgvSitTribCOFINS_fil.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSitTribCOFINS_fil.Size = new System.Drawing.Size(984, 590);
            this.dgvSitTribCOFINS_fil.TabIndex = 0;
            this.ttpSitTribCOFINS.SetToolTip(this.dgvSitTribCOFINS_fil, "Grade de Visualização SitTribCOFINS");
            this.dgvSitTribCOFINS_fil.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSitTribCOFINS_fil_CellDoubleClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 5;
            // 
            // CODIGO
            // 
            this.CODIGO.DataPropertyName = "CODIGO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CODIGO.DefaultCellStyle = dataGridViewCellStyle2;
            this.CODIGO.HeaderText = "Codigo";
            this.CODIGO.Name = "CODIGO";
            this.CODIGO.ReadOnly = true;
            this.CODIGO.Width = 70;
            // 
            // NOME
            // 
            this.NOME.DataPropertyName = "NOME";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.NOME.DefaultCellStyle = dataGridViewCellStyle3;
            this.NOME.HeaderText = "Nome";
            this.NOME.Name = "NOME";
            this.NOME.ReadOnly = true;
            this.NOME.Width = 885;
            // 
            // bwrSitTribCOFINS
            // 
            this.bwrSitTribCOFINS.WorkerSupportsCancellation = true;
            this.bwrSitTribCOFINS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrSitTribCOFINS_DoWork);
            this.bwrSitTribCOFINS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrSitTribCOFINS_RunWorkerCompleted);
            // 
            // frmSitTribCOFINSVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.gbxSitTribCOFINS);
            this.Controls.Add(this.tspSitTribCOFINS);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSitTribCOFINSVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4013";
            this.Text = "Situação Tributária Cofins - Visualização";
            this.ttpSitTribCOFINS.SetToolTip(this, "Situação Tributária Cofins - Visualização");
            this.Load += new System.EventHandler(this.frmSitTribCOFINSVis_Load);
            this.Shown += new System.EventHandler(this.frmSitTribCOFINSVis_Shown);
            this.Activated += new System.EventHandler(this.frmSitTribCOFINSVis_Activated);
            this.tspSitTribCOFINS.ResumeLayout(false);
            this.tspSitTribCOFINS.PerformLayout();
            this.gbxSitTribCOFINS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxSitTribCOFINS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSitTribCOFINS_fil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspSitTribCOFINS;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip ttpSitTribCOFINS;
        private System.Windows.Forms.GroupBox gbxSitTribCOFINS;
        private System.ComponentModel.BackgroundWorker bwrSitTribCOFINS;
        private System.Windows.Forms.PictureBox pbxSitTribCOFINS;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.DataGridView dgvSitTribCOFINS_fil;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOME;
    }
}