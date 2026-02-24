namespace CRCorrea
{
    partial class frmUnidadePes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnidadePes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tspUnidade = new System.Windows.Forms.ToolStrip();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.ttpUnidade = new System.Windows.Forms.ToolTip(this.components);
            this.gbxUnidade = new System.Windows.Forms.GroupBox();
            this.pbxUnidade = new System.Windows.Forms.PictureBox();
            this.dgvUnidade_fil = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UNIDDEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bwrUnidade = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.tspUnidade.SuspendLayout();
            this.gbxUnidade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUnidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnidade_fil)).BeginInit();
            this.SuspendLayout();
            // 
            // tspUnidade
            // 
            this.tspUnidade.AccessibleDescription = "Barra de Opções";
            this.tspUnidade.AllowMerge = false;
            this.tspUnidade.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspUnidade.AutoSize = false;
            this.tspUnidade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspUnidade.Dock = System.Windows.Forms.DockStyle.None;
            this.tspUnidade.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspUnidade.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspImprimir,
            this.tspEscolher,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspUnidade.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspUnidade.Location = new System.Drawing.Point(12, 595);
            this.tspUnidade.Name = "tspUnidade";
            this.tspUnidade.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspUnidade.Size = new System.Drawing.Size(1020, 47);
            this.tspUnidade.TabIndex = 1;
            this.ttpUnidade.SetToolTip(this.tspUnidade, "Menu");
            // 
            // tspImprimir
            // 
            this.tspImprimir.AccessibleDescription = "Imprimir";
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = global::CRCorrea.Properties.Resources.Imprimir;
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(66, 42);
            this.tspImprimir.Text = "&Imprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
            // 
            // tspEscolher
            // 
            this.tspEscolher.AccessibleDescription = "Escolher";
            this.tspEscolher.AutoSize = false;
            this.tspEscolher.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspEscolher.Image = global::CRCorrea.Properties.Resources.Escolher;
            this.tspEscolher.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspEscolher.Name = "tspEscolher";
            this.tspEscolher.Size = new System.Drawing.Size(66, 42);
            this.tspEscolher.Text = "&Escolher";
            this.tspEscolher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspEscolher.ToolTipText = "Escolher";
            this.tspEscolher.Click += new System.EventHandler(this.tspEscolher_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "&Retornar";
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
            this.tstbxLocalizar.Size = new System.Drawing.Size(280, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // gbxUnidade
            // 
            this.gbxUnidade.AccessibleDescription = "Unidade";
            this.gbxUnidade.Controls.Add(this.pbxUnidade);
            this.gbxUnidade.Controls.Add(this.dgvUnidade_fil);
            this.gbxUnidade.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxUnidade.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbxUnidade.Location = new System.Drawing.Point(12, 48);
            this.gbxUnidade.Name = "gbxUnidade";
            this.gbxUnidade.Size = new System.Drawing.Size(996, 541);
            this.gbxUnidade.TabIndex = 1;
            this.gbxUnidade.TabStop = false;
            this.gbxUnidade.Text = "Unidade";
            this.ttpUnidade.SetToolTip(this.gbxUnidade, "Grupo Unidade");
            // 
            // pbxUnidade
            // 
            this.pbxUnidade.AccessibleDescription = "Carregando Unidades";
            this.pbxUnidade.BackColor = System.Drawing.Color.White;
            this.pbxUnidade.Image = ((System.Drawing.Image)(resources.GetObject("pbxUnidade.Image")));
            this.pbxUnidade.Location = new System.Drawing.Point(8, 22);
            this.pbxUnidade.Name = "pbxUnidade";
            this.pbxUnidade.Size = new System.Drawing.Size(980, 509);
            this.pbxUnidade.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxUnidade.TabIndex = 138;
            this.pbxUnidade.TabStop = false;
            this.ttpUnidade.SetToolTip(this.pbxUnidade, "Imagem");
            this.pbxUnidade.Visible = false;
            // 
            // dgvUnidade_fil
            // 
            this.dgvUnidade_fil.AccessibleDescription = "Unidades";
            this.dgvUnidade_fil.AllowUserToAddRows = false;
            this.dgvUnidade_fil.AllowUserToDeleteRows = false;
            this.dgvUnidade_fil.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvUnidade_fil.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUnidade_fil.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvUnidade_fil.BackgroundColor = System.Drawing.Color.White;
            this.dgvUnidade_fil.ColumnHeadersHeight = 28;
            this.dgvUnidade_fil.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.CODIGO,
            this.NOME,
            this.UNIDDEC});
            this.dgvUnidade_fil.Location = new System.Drawing.Point(8, 17);
            this.dgvUnidade_fil.MultiSelect = false;
            this.dgvUnidade_fil.Name = "dgvUnidade_fil";
            this.dgvUnidade_fil.ReadOnly = true;
            this.dgvUnidade_fil.RowHeadersVisible = false;
            this.dgvUnidade_fil.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnidade_fil.Size = new System.Drawing.Size(981, 510);
            this.dgvUnidade_fil.TabIndex = 0;
            this.ttpUnidade.SetToolTip(this.dgvUnidade_fil, "Grade de Visualização SitTribIPI");
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
            this.NOME.Width = 790;
            // 
            // UNIDDEC
            // 
            this.UNIDDEC.DataPropertyName = "UNIDDEC";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.UNIDDEC.DefaultCellStyle = dataGridViewCellStyle4;
            this.UNIDDEC.HeaderText = "Casas Dec.";
            this.UNIDDEC.Name = "UNIDDEC";
            this.UNIDDEC.ReadOnly = true;
            this.UNIDDEC.Width = 90;
            // 
            // bwrUnidade
            // 
            this.bwrUnidade.WorkerSupportsCancellation = true;
            this.bwrUnidade.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrUnidade_DoWork);
            this.bwrUnidade.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrUnidade_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(988, 19);
            this.label1.TabIndex = 128;
            this.label1.Text = "Pesquisando as Unidades de Medida";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmUnidadePes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbxUnidade);
            this.Controls.Add(this.tspUnidade);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUnidadePes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4013";
            this.Text = "Unidade - Pesquisa";
            this.ttpUnidade.SetToolTip(this, "Unidade");
            this.Activated += new System.EventHandler(this.frmUnidadePes_Activated);
            this.Load += new System.EventHandler(this.frmUnidadePes_Load);
            this.Shown += new System.EventHandler(this.frmUnidadePes_Shown);
            this.tspUnidade.ResumeLayout(false);
            this.tspUnidade.PerformLayout();
            this.gbxUnidade.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxUnidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnidade_fil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspUnidade;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip ttpUnidade;
        private System.Windows.Forms.GroupBox gbxUnidade;
        private System.ComponentModel.BackgroundWorker bwrUnidade;
        private System.Windows.Forms.PictureBox pbxUnidade;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.DataGridView dgvUnidade_fil;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOME;
        private System.Windows.Forms.DataGridViewTextBoxColumn UNIDDEC;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.Label label1;
    }
}