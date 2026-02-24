namespace CRCorrea
{
    partial class frmEstadosVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEstadosVis));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tspESTADOS = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.ttpESTADOS = new System.Windows.Forms.ToolTip(this.components);
            this.gbxESTADOS = new System.Windows.Forms.GroupBox();
            this.pbxTab_ESTADOS = new System.Windows.Forms.PictureBox();
            this.dgvESTADOS_fil = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZONAFRANCA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALIQUOTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEEXT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAPITAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INICEP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIMCEP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REGIAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IBGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bwrESTADOS = new System.ComponentModel.BackgroundWorker();
            this.tspESTADOS.SuspendLayout();
            this.gbxESTADOS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTab_ESTADOS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvESTADOS_fil)).BeginInit();
            this.SuspendLayout();
            // 
            // tspESTADOS
            // 
            this.tspESTADOS.AccessibleDescription = "Barra de Opções";
            this.tspESTADOS.AllowMerge = false;
            this.tspESTADOS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspESTADOS.AutoSize = false;
            this.tspESTADOS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspESTADOS.Dock = System.Windows.Forms.DockStyle.None;
            this.tspESTADOS.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspESTADOS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspImprimir,
            this.tspExcluir,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspESTADOS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspESTADOS.Location = new System.Drawing.Point(18, 554);
            this.tspESTADOS.Name = "tspESTADOS";
            this.tspESTADOS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspESTADOS.Size = new System.Drawing.Size(984, 47);
            this.tspESTADOS.TabIndex = 1;
            this.ttpESTADOS.SetToolTip(this.tspESTADOS, "Menu");
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
            // gbxESTADOS
            // 
            this.gbxESTADOS.AccessibleDescription = "Estados";
            this.gbxESTADOS.Controls.Add(this.pbxTab_ESTADOS);
            this.gbxESTADOS.Controls.Add(this.dgvESTADOS_fil);
            this.gbxESTADOS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxESTADOS.ForeColor = System.Drawing.Color.DarkRed;
            this.gbxESTADOS.Location = new System.Drawing.Point(12, 12);
            this.gbxESTADOS.Name = "gbxESTADOS";
            this.gbxESTADOS.Size = new System.Drawing.Size(996, 539);
            this.gbxESTADOS.TabIndex = 0;
            this.gbxESTADOS.TabStop = false;
            this.gbxESTADOS.Text = "Estados";
            this.ttpESTADOS.SetToolTip(this.gbxESTADOS, "Grupo ESTADOS");
            // 
            // pbxTab_ESTADOS
            // 
            this.pbxTab_ESTADOS.AccessibleDescription = "Carregando Estados";
            this.pbxTab_ESTADOS.BackColor = System.Drawing.Color.White;
            this.pbxTab_ESTADOS.Image = ((System.Drawing.Image)(resources.GetObject("pbxTab_ESTADOS.Image")));
            this.pbxTab_ESTADOS.Location = new System.Drawing.Point(6, 21);
            this.pbxTab_ESTADOS.Name = "pbxTab_ESTADOS";
            this.pbxTab_ESTADOS.Size = new System.Drawing.Size(984, 494);
            this.pbxTab_ESTADOS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxTab_ESTADOS.TabIndex = 138;
            this.pbxTab_ESTADOS.TabStop = false;
            this.ttpESTADOS.SetToolTip(this.pbxTab_ESTADOS, "Imagem");
            this.pbxTab_ESTADOS.Visible = false;
            // 
            // dgvESTADOS_fil
            // 
            this.dgvESTADOS_fil.AccessibleDescription = "Estados";
            this.dgvESTADOS_fil.AllowUserToAddRows = false;
            this.dgvESTADOS_fil.AllowUserToDeleteRows = false;
            this.dgvESTADOS_fil.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvESTADOS_fil.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvESTADOS_fil.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvESTADOS_fil.BackgroundColor = System.Drawing.Color.White;
            this.dgvESTADOS_fil.ColumnHeadersHeight = 28;
            this.dgvESTADOS_fil.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ESTADO,
            this.ZONAFRANCA,
            this.ALIQUOTA,
            this.NOMEEXT,
            this.CAPITAL,
            this.INICEP,
            this.FIMCEP,
            this.REGIAO,
            this.IBGE});
            this.dgvESTADOS_fil.Location = new System.Drawing.Point(7, 21);
            this.dgvESTADOS_fil.MultiSelect = false;
            this.dgvESTADOS_fil.Name = "dgvESTADOS_fil";
            this.dgvESTADOS_fil.ReadOnly = true;
            this.dgvESTADOS_fil.RowHeadersVisible = false;
            this.dgvESTADOS_fil.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvESTADOS_fil.Size = new System.Drawing.Size(984, 512);
            this.dgvESTADOS_fil.TabIndex = 139;
            this.ttpESTADOS.SetToolTip(this.dgvESTADOS_fil, "Grade de Visualização ESTADOS");
            this.dgvESTADOS_fil.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvESTADOS_fil_CellDoubleClick);
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
            // ESTADO
            // 
            this.ESTADO.DataPropertyName = "ESTADO";
            this.ESTADO.HeaderText = "Estado";
            this.ESTADO.Name = "ESTADO";
            this.ESTADO.ReadOnly = true;
            this.ESTADO.Width = 60;
            // 
            // ZONAFRANCA
            // 
            this.ZONAFRANCA.DataPropertyName = "ZONAFRANCA";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ZONAFRANCA.DefaultCellStyle = dataGridViewCellStyle2;
            this.ZONAFRANCA.HeaderText = "ZONAFRANCA";
            this.ZONAFRANCA.Name = "ZONAFRANCA";
            this.ZONAFRANCA.ReadOnly = true;
            this.ZONAFRANCA.Visible = false;
            // 
            // ALIQUOTA
            // 
            this.ALIQUOTA.DataPropertyName = "ALIQUOTA";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.ALIQUOTA.DefaultCellStyle = dataGridViewCellStyle3;
            this.ALIQUOTA.HeaderText = "ALIQUOTA";
            this.ALIQUOTA.Name = "ALIQUOTA";
            this.ALIQUOTA.ReadOnly = true;
            this.ALIQUOTA.Visible = false;
            // 
            // NOMEEXT
            // 
            this.NOMEEXT.DataPropertyName = "NOMEEXT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.NOMEEXT.DefaultCellStyle = dataGridViewCellStyle4;
            this.NOMEEXT.HeaderText = "Nome Ext";
            this.NOMEEXT.Name = "NOMEEXT";
            this.NOMEEXT.ReadOnly = true;
            this.NOMEEXT.Width = 200;
            // 
            // CAPITAL
            // 
            this.CAPITAL.DataPropertyName = "CAPITAL";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CAPITAL.DefaultCellStyle = dataGridViewCellStyle5;
            this.CAPITAL.HeaderText = "Capital";
            this.CAPITAL.Name = "CAPITAL";
            this.CAPITAL.ReadOnly = true;
            this.CAPITAL.Width = 700;
            // 
            // INICEP
            // 
            this.INICEP.DataPropertyName = "INICEP";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.INICEP.DefaultCellStyle = dataGridViewCellStyle6;
            this.INICEP.HeaderText = "INICEP";
            this.INICEP.Name = "INICEP";
            this.INICEP.ReadOnly = true;
            this.INICEP.Visible = false;
            // 
            // FIMCEP
            // 
            this.FIMCEP.DataPropertyName = "FIMCEP";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FIMCEP.DefaultCellStyle = dataGridViewCellStyle7;
            this.FIMCEP.HeaderText = "FIMCEP";
            this.FIMCEP.Name = "FIMCEP";
            this.FIMCEP.ReadOnly = true;
            this.FIMCEP.Visible = false;
            // 
            // REGIAO
            // 
            this.REGIAO.DataPropertyName = "REGIAO";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.REGIAO.DefaultCellStyle = dataGridViewCellStyle8;
            this.REGIAO.HeaderText = "REGIAO";
            this.REGIAO.Name = "REGIAO";
            this.REGIAO.ReadOnly = true;
            this.REGIAO.Visible = false;
            // 
            // IBGE
            // 
            this.IBGE.DataPropertyName = "IBGE";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.IBGE.DefaultCellStyle = dataGridViewCellStyle9;
            this.IBGE.HeaderText = "IBGE";
            this.IBGE.Name = "IBGE";
            this.IBGE.ReadOnly = true;
            this.IBGE.Visible = false;
            // 
            // bwrESTADOS
            // 
            this.bwrESTADOS.WorkerSupportsCancellation = true;
            this.bwrESTADOS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrESTADOS_DoWork);
            this.bwrESTADOS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrESTADOS_RunWorkerCompleted);
            // 
            // frmEstadosVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.gbxESTADOS);
            this.Controls.Add(this.tspESTADOS);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEstadosVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4013";
            this.Text = "Estados - Visualização";
            this.ttpESTADOS.SetToolTip(this, "Estados - Visualização");
            this.Activated += new System.EventHandler(this.frmEstadosVis_Activated);
            this.Load += new System.EventHandler(this.frmEstadosVis_Load);
            this.Shown += new System.EventHandler(this.frmEstadosVis_Shown);
            this.tspESTADOS.ResumeLayout(false);
            this.tspESTADOS.PerformLayout();
            this.gbxESTADOS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxTab_ESTADOS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvESTADOS_fil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspESTADOS;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip ttpESTADOS;
        private System.Windows.Forms.GroupBox gbxESTADOS;
        private System.ComponentModel.BackgroundWorker bwrESTADOS;
        private System.Windows.Forms.PictureBox pbxTab_ESTADOS;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.DataGridView dgvESTADOS_fil;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZONAFRANCA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALIQUOTA;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEEXT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAPITAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn INICEP;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIMCEP;
        private System.Windows.Forms.DataGridViewTextBoxColumn REGIAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IBGE;
    }
}