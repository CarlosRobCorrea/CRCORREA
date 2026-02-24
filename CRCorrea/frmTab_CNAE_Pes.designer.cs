namespace CRCorrea
{
    partial class frmTab_CNAE_Pes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTab_CNAE_Pes));
            this.tspCNAE = new System.Windows.Forms.ToolStrip();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.ttpCNAE = new System.Windows.Forms.ToolTip(this.components);
            this.gbxCNAE = new System.Windows.Forms.GroupBox();
            this.dgvTab_CNAE_fil = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bwrTab_CNAE = new System.ComponentModel.BackgroundWorker();
            this.pbxTab_CNAE = new System.Windows.Forms.PictureBox();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tspCNAE.SuspendLayout();
            this.gbxCNAE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_CNAE_fil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTab_CNAE)).BeginInit();
            this.SuspendLayout();
            // 
            // tspCNAE
            // 
            this.tspCNAE.AccessibleDescription = "Barra de Opções";
            this.tspCNAE.AllowMerge = false;
            this.tspCNAE.AutoSize = false;
            this.tspCNAE.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspCNAE.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspCNAE.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspEscolher,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspCNAE.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspCNAE.Location = new System.Drawing.Point(0, 638);
            this.tspCNAE.Name = "tspCNAE";
            this.tspCNAE.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspCNAE.Size = new System.Drawing.Size(1020, 47);
            this.tspCNAE.TabIndex = 1;
            this.ttpCNAE.SetToolTip(this.tspCNAE, "Menu");
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
            // gbxCNAE
            // 
            this.gbxCNAE.AccessibleDescription = "CNAE";
            this.gbxCNAE.Controls.Add(this.pbxTab_CNAE);
            this.gbxCNAE.Controls.Add(this.dgvTab_CNAE_fil);
            this.gbxCNAE.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxCNAE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbxCNAE.Location = new System.Drawing.Point(12, 57);
            this.gbxCNAE.Name = "gbxCNAE";
            this.gbxCNAE.Size = new System.Drawing.Size(996, 578);
            this.gbxCNAE.TabIndex = 0;
            this.gbxCNAE.TabStop = false;
            this.gbxCNAE.Text = "CNAE";
            this.ttpCNAE.SetToolTip(this.gbxCNAE, "Grupo CNAE");
            // 
            // dgvTab_CNAE_fil
            // 
            this.dgvTab_CNAE_fil.AccessibleDescription = "CNAE";
            this.dgvTab_CNAE_fil.AllowUserToAddRows = false;
            this.dgvTab_CNAE_fil.AllowUserToDeleteRows = false;
            this.dgvTab_CNAE_fil.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvTab_CNAE_fil.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvTab_CNAE_fil.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvTab_CNAE_fil.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_CNAE_fil.ColumnHeadersHeight = 28;
            this.dgvTab_CNAE_fil.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.CODIGO,
            this.NOME});
            this.dgvTab_CNAE_fil.Location = new System.Drawing.Point(4, 16);
            this.dgvTab_CNAE_fil.MultiSelect = false;
            this.dgvTab_CNAE_fil.Name = "dgvTab_CNAE_fil";
            this.dgvTab_CNAE_fil.ReadOnly = true;
            this.dgvTab_CNAE_fil.RowHeadersVisible = false;
            this.dgvTab_CNAE_fil.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvTab_CNAE_fil.Size = new System.Drawing.Size(987, 545);
            this.dgvTab_CNAE_fil.TabIndex = 139;
            this.ttpCNAE.SetToolTip(this.dgvTab_CNAE_fil, "Grade de Visualização CNAE");
            this.dgvTab_CNAE_fil.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_CNAE_CellDoubleClick);
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
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CODIGO.DefaultCellStyle = dataGridViewCellStyle8;
            this.CODIGO.HeaderText = "Codigo";
            this.CODIGO.Name = "CODIGO";
            this.CODIGO.ReadOnly = true;
            this.CODIGO.Width = 170;
            // 
            // NOME
            // 
            this.NOME.DataPropertyName = "NOME";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.NOME.DefaultCellStyle = dataGridViewCellStyle9;
            this.NOME.HeaderText = "Nome";
            this.NOME.Name = "NOME";
            this.NOME.ReadOnly = true;
            this.NOME.Width = 780;
            // 
            // bwrTab_CNAE
            // 
            this.bwrTab_CNAE.WorkerSupportsCancellation = true;
            this.bwrTab_CNAE.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrTab_CNAE_DoWork);
            this.bwrTab_CNAE.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrTab_CNAE_RunWorkerCompleted);
            // 
            // pbxTab_CNAE
            // 
            this.pbxTab_CNAE.AccessibleDescription = "Carregando CNAE";
            this.pbxTab_CNAE.BackColor = System.Drawing.Color.White;
            this.pbxTab_CNAE.Image = ((System.Drawing.Image)(resources.GetObject("pbxTab_CNAE.Image")));
            this.pbxTab_CNAE.Location = new System.Drawing.Point(4, 17);
            this.pbxTab_CNAE.Name = "pbxTab_CNAE";
            this.pbxTab_CNAE.Size = new System.Drawing.Size(987, 544);
            this.pbxTab_CNAE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxTab_CNAE.TabIndex = 138;
            this.pbxTab_CNAE.TabStop = false;
            this.ttpCNAE.SetToolTip(this.pbxTab_CNAE, "Imagem");
            this.pbxTab_CNAE.Visible = false;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(360, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 19);
            this.label1.TabIndex = 135;
            this.label1.Text = "Pesquisando Tabela de C.N.A.E. ";
            // 
            // frmTab_CNAE_Pes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbxCNAE);
            this.Controls.Add(this.tspCNAE);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTab_CNAE_Pes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4013";
            this.Text = "CNAE - Pesquisa";
            this.ttpCNAE.SetToolTip(this, "CNAE - Pesquisa");
            this.Load += new System.EventHandler(this.frmCNAEVis_Load);
            this.Shown += new System.EventHandler(this.frmCNAEVis_Shown);
            this.Activated += new System.EventHandler(this.frmCNAEVis_Activated);
            this.tspCNAE.ResumeLayout(false);
            this.tspCNAE.PerformLayout();
            this.gbxCNAE.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_CNAE_fil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTab_CNAE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspCNAE;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip ttpCNAE;
        private System.Windows.Forms.GroupBox gbxCNAE;
        private System.ComponentModel.BackgroundWorker bwrTab_CNAE;
        private System.Windows.Forms.PictureBox pbxTab_CNAE;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.DataGridView dgvTab_CNAE_fil;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOME;
        private System.Windows.Forms.Label label1;
    }
}