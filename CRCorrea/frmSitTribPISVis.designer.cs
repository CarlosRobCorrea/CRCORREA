namespace CRCorrea
{
    partial class frmSitTribPISVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSitTribPISVis));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tspSitTribPIS = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.ttpSitTribPIS = new System.Windows.Forms.ToolTip(this.components);
            this.gbxSitTribPISVis = new System.Windows.Forms.GroupBox();
            this.pbxSitTribPISVis = new System.Windows.Forms.PictureBox();
            this.dgvSitTribPIS_fil = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bwrSitTribPIS = new System.ComponentModel.BackgroundWorker();
            this.tspSitTribPIS.SuspendLayout();
            this.gbxSitTribPISVis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSitTribPISVis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSitTribPIS_fil)).BeginInit();
            this.SuspendLayout();
            // 
            // tspSitTribPIS
            // 
            this.tspSitTribPIS.AccessibleDescription = "Barra de Opções";
            this.tspSitTribPIS.AllowMerge = false;
            this.tspSitTribPIS.AutoSize = false;
            this.tspSitTribPIS.Dock = System.Windows.Forms.DockStyle.None;
            this.tspSitTribPIS.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspSitTribPIS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspEscolher,
            this.tspImprimir,
            this.tspExcluir,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspSitTribPIS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspSitTribPIS.Location = new System.Drawing.Point(7, 640);
            this.tspSitTribPIS.Name = "tspSitTribPIS";
            this.tspSitTribPIS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspSitTribPIS.Size = new System.Drawing.Size(1006, 45);
            this.tspSitTribPIS.TabIndex = 1;
            this.ttpSitTribPIS.SetToolTip(this.tspSitTribPIS, "Menu");
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
            this.tspEscolher.Image = global::CRCorrea.Properties.Resources.Escolher;
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
            // gbxSitTribPISVis
            // 
            this.gbxSitTribPISVis.AccessibleDescription = "Situação Tributaria PIS";
            this.gbxSitTribPISVis.Controls.Add(this.pbxSitTribPISVis);
            this.gbxSitTribPISVis.Controls.Add(this.dgvSitTribPIS_fil);
            this.gbxSitTribPISVis.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSitTribPISVis.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbxSitTribPISVis.Location = new System.Drawing.Point(12, 12);
            this.gbxSitTribPISVis.Name = "gbxSitTribPISVis";
            this.gbxSitTribPISVis.Size = new System.Drawing.Size(1005, 623);
            this.gbxSitTribPISVis.TabIndex = 1;
            this.gbxSitTribPISVis.TabStop = false;
            this.gbxSitTribPISVis.Text = "Situação Tributaria PIS";
            this.ttpSitTribPIS.SetToolTip(this.gbxSitTribPISVis, "Grupo SitTribPISVis");
            // 
            // pbxSitTribPISVis
            // 
            this.pbxSitTribPISVis.AccessibleDescription = "Carregando Situação Tributária";
            this.pbxSitTribPISVis.BackColor = System.Drawing.Color.White;
            this.pbxSitTribPISVis.Image = ((System.Drawing.Image)(resources.GetObject("pbxSitTribPISVis.Image")));
            this.pbxSitTribPISVis.Location = new System.Drawing.Point(7, 21);
            this.pbxSitTribPISVis.Name = "pbxSitTribPISVis";
            this.pbxSitTribPISVis.Size = new System.Drawing.Size(994, 595);
            this.pbxSitTribPISVis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxSitTribPISVis.TabIndex = 138;
            this.pbxSitTribPISVis.TabStop = false;
            this.ttpSitTribPIS.SetToolTip(this.pbxSitTribPISVis, "Imagem");
            this.pbxSitTribPISVis.Visible = false;
            // 
            // dgvSitTribPIS_fil
            // 
            this.dgvSitTribPIS_fil.AccessibleDescription = "Situação Tributária PIS";
            this.dgvSitTribPIS_fil.AllowUserToAddRows = false;
            this.dgvSitTribPIS_fil.AllowUserToDeleteRows = false;
            this.dgvSitTribPIS_fil.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvSitTribPIS_fil.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSitTribPIS_fil.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvSitTribPIS_fil.BackgroundColor = System.Drawing.Color.White;
            this.dgvSitTribPIS_fil.ColumnHeadersHeight = 28;
            this.dgvSitTribPIS_fil.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.CODIGO,
            this.NOME});
            this.dgvSitTribPIS_fil.Location = new System.Drawing.Point(6, 21);
            this.dgvSitTribPIS_fil.MultiSelect = false;
            this.dgvSitTribPIS_fil.Name = "dgvSitTribPIS_fil";
            this.dgvSitTribPIS_fil.ReadOnly = true;
            this.dgvSitTribPIS_fil.RowHeadersVisible = false;
            this.dgvSitTribPIS_fil.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSitTribPIS_fil.Size = new System.Drawing.Size(991, 590);
            this.dgvSitTribPIS_fil.TabIndex = 0;
            this.ttpSitTribPIS.SetToolTip(this.dgvSitTribPIS_fil, "Grade de Visualização SitTribPIS");
            this.dgvSitTribPIS_fil.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSitTribPIS_fil_CellDoubleClick);
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
            this.NOME.Width = 895;
            // 
            // bwrSitTribPIS
            // 
            this.bwrSitTribPIS.WorkerSupportsCancellation = true;
            this.bwrSitTribPIS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrSitTribPIS_DoWork);
            this.bwrSitTribPIS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrSitTribPIS_RunWorkerCompleted);
            // 
            // frmSitTribPISVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.gbxSitTribPISVis);
            this.Controls.Add(this.tspSitTribPIS);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSitTribPISVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4013";
            this.Text = "Situação Tributaria PIS - Visualização";
            this.ttpSitTribPIS.SetToolTip(this, "Situação Tributaria PIS - Visualização");
            this.Load += new System.EventHandler(this.frmSitTribPISVis_Load);
            this.Shown += new System.EventHandler(this.frmSitTribPISVis_Shown);
            this.Activated += new System.EventHandler(this.frmSitTribPISVis_Activated);
            this.tspSitTribPIS.ResumeLayout(false);
            this.tspSitTribPIS.PerformLayout();
            this.gbxSitTribPISVis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxSitTribPISVis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSitTribPIS_fil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspSitTribPIS;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip ttpSitTribPIS;
        private System.Windows.Forms.GroupBox gbxSitTribPISVis;
        private System.ComponentModel.BackgroundWorker bwrSitTribPIS;
        private System.Windows.Forms.PictureBox pbxSitTribPISVis;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.DataGridView dgvSitTribPIS_fil;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOME;
    }
}