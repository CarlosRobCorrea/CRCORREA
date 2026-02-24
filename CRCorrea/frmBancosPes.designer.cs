namespace CRCorrea
{
    partial class frmBancosPes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBancosPes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.dgvBancos = new System.Windows.Forms.DataGridView();
            this.bwrBancos = new System.ComponentModel.BackgroundWorker();
            this.gbxGroup = new System.Windows.Forms.GroupBox();
            this.rbnInativos = new System.Windows.Forms.RadioButton();
            this.rbnAtivos = new System.Windows.Forms.RadioButton();
            this.rbnTodos = new System.Windows.Forms.RadioButton();
            this.ttpBancos = new System.Windows.Forms.ToolTip(this.components);
            this.label26 = new System.Windows.Forms.Label();
            this.tspTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBancos)).BeginInit();
            this.gbxGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspEscolher,
            this.tspRetornar,
            this.toolStripSeparator1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(18, 574);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(446, 47);
            this.tspTool.TabIndex = 8;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "Barra de Opções";
            this.tspTool.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tspTool_ItemClicked);
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
            // dgvBancos
            // 
            this.dgvBancos.AccessibleDescription = "Grid";
            this.dgvBancos.AllowUserToAddRows = false;
            this.dgvBancos.AllowUserToDeleteRows = false;
            this.dgvBancos.AllowUserToResizeColumns = false;
            this.dgvBancos.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.dgvBancos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBancos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvBancos.BackgroundColor = System.Drawing.Color.White;
            this.dgvBancos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBancos.Location = new System.Drawing.Point(12, 66);
            this.dgvBancos.MultiSelect = false;
            this.dgvBancos.Name = "dgvBancos";
            this.dgvBancos.ReadOnly = true;
            this.dgvBancos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvBancos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvBancos.Size = new System.Drawing.Size(996, 505);
            this.dgvBancos.TabIndex = 7;
            this.ttpBancos.SetToolTip(this.dgvBancos, "Grid Bancos");
            this.dgvBancos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBancos_CellContentClick);
            this.dgvBancos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBancos_CellDoubleClick);
            // 
            // bwrBancos
            // 
            this.bwrBancos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrBancos_DoWork);
            this.bwrBancos.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrBancos_RunWorkerCompleted);
            // 
            // gbxGroup
            // 
            this.gbxGroup.AccessibleDescription = "Pré-Filtro";
            this.gbxGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxGroup.Controls.Add(this.rbnInativos);
            this.gbxGroup.Controls.Add(this.rbnAtivos);
            this.gbxGroup.Controls.Add(this.rbnTodos);
            this.gbxGroup.Location = new System.Drawing.Point(12, 12);
            this.gbxGroup.Name = "gbxGroup";
            this.gbxGroup.Size = new System.Drawing.Size(307, 48);
            this.gbxGroup.TabIndex = 46;
            this.gbxGroup.TabStop = false;
            this.gbxGroup.Text = "Pré-Filtro";
            this.ttpBancos.SetToolTip(this.gbxGroup, "Grupo - Filtro");
            // 
            // rbnInativos
            // 
            this.rbnInativos.AccessibleDescription = "Bancos Inativos";
            this.rbnInativos.AutoSize = true;
            this.rbnInativos.Location = new System.Drawing.Point(201, 20);
            this.rbnInativos.Name = "rbnInativos";
            this.rbnInativos.Size = new System.Drawing.Size(101, 17);
            this.rbnInativos.TabIndex = 2;
            this.rbnInativos.Text = "Bancos Inativos";
            this.ttpBancos.SetToolTip(this.rbnInativos, "Filtro - Bancos Inativos");
            this.rbnInativos.UseVisualStyleBackColor = true;
            this.rbnInativos.Click += new System.EventHandler(this.rbnFiltro);
            // 
            // rbnAtivos
            // 
            this.rbnAtivos.AccessibleDescription = "Bancos Ativos";
            this.rbnAtivos.AutoSize = true;
            this.rbnAtivos.Checked = true;
            this.rbnAtivos.Location = new System.Drawing.Point(103, 20);
            this.rbnAtivos.Name = "rbnAtivos";
            this.rbnAtivos.Size = new System.Drawing.Size(92, 17);
            this.rbnAtivos.TabIndex = 1;
            this.rbnAtivos.TabStop = true;
            this.rbnAtivos.Text = "Bancos Ativos";
            this.ttpBancos.SetToolTip(this.rbnAtivos, "Filtro - Bancos Ativos");
            this.rbnAtivos.UseVisualStyleBackColor = true;
            this.rbnAtivos.Click += new System.EventHandler(this.rbnFiltro);
            // 
            // rbnTodos
            // 
            this.rbnTodos.AccessibleDescription = "Todos os Bancos";
            this.rbnTodos.AutoSize = true;
            this.rbnTodos.Location = new System.Drawing.Point(6, 20);
            this.rbnTodos.Name = "rbnTodos";
            this.rbnTodos.Size = new System.Drawing.Size(91, 17);
            this.rbnTodos.TabIndex = 0;
            this.rbnTodos.Text = "Todos Bancos";
            this.ttpBancos.SetToolTip(this.rbnTodos, "Filtro - Todos Bancos");
            this.rbnTodos.UseVisualStyleBackColor = true;
            this.rbnTodos.Click += new System.EventHandler(this.rbnFiltro);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label26.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(803, 12);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(205, 29);
            this.label26.TabIndex = 49;
            this.label26.Text = "Contas Aplibank";
            // 
            // frmBancosPes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.gbxGroup);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.dgvBancos);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBancosPes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "401";
            this.Text = "Bancos - Visualização";
            this.ttpBancos.SetToolTip(this, "Bancos - Visualização");
            this.Activated += new System.EventHandler(this.frmBancosPes_Activated);
            this.Load += new System.EventHandler(this.frmBancosPes_Load);
            this.Shown += new System.EventHandler(this.frmBancosPes_Shown);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBancos)).EndInit();
            this.gbxGroup.ResumeLayout(false);
            this.gbxGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.DataGridView dgvBancos;
        private System.ComponentModel.BackgroundWorker bwrBancos;
        private System.Windows.Forms.GroupBox gbxGroup;
        private System.Windows.Forms.RadioButton rbnInativos;
        private System.Windows.Forms.RadioButton rbnAtivos;
        private System.Windows.Forms.RadioButton rbnTodos;
        private System.Windows.Forms.ToolTip ttpBancos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.Label label26;
    }
}