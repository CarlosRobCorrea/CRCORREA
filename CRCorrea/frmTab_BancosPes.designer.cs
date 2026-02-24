namespace CRCorrea
{
    partial class frmTab_BancosPes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTab_BancosPes));
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.dgvTab_Bancos = new System.Windows.Forms.DataGridView();
            this.bwrTab_Bancos = new System.ComponentModel.BackgroundWorker();
            this.gbxGroup = new System.Windows.Forms.GroupBox();
            this.rbnInativos = new System.Windows.Forms.RadioButton();
            this.rbnAtivos = new System.Windows.Forms.RadioButton();
            this.rbnTodos = new System.Windows.Forms.RadioButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tspTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_Bancos)).BeginInit();
            this.gbxGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Bara de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspEscolher,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(9, 607);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(1020, 45);
            this.tspTool.TabIndex = 44;
            this.toolTip.SetToolTip(this.tspTool, "Barra de Controle de bancos");
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
            this.tspEscolher.Text = "Escolher";
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
            // tspSeparador1
            // 
            this.tspSeparador1.AutoSize = false;
            this.tspSeparador1.Name = "tspSeparador1";
            this.tspSeparador1.Size = new System.Drawing.Size(6, 45);
            // 
            // tslbLocalizar
            // 
            this.tslbLocalizar.AutoSize = false;
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(66, 42);
            this.tslbLocalizar.Text = "Procurar";
            this.tslbLocalizar.ToolTipText = "Procurar";
            // 
            // tstbxLocalizar
            // 
            this.tstbxLocalizar.AccessibleDescription = "Procurar";
            this.tstbxLocalizar.AutoSize = false;
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(280, 21);
            this.tstbxLocalizar.ToolTipText = "Localizar Lançamento";
            // 
            // dgvTab_Bancos
            // 
            this.dgvTab_Bancos.AccessibleDescription = " Bancos";
            this.dgvTab_Bancos.AllowDrop = true;
            this.dgvTab_Bancos.AllowUserToAddRows = false;
            this.dgvTab_Bancos.AllowUserToDeleteRows = false;
            this.dgvTab_Bancos.AllowUserToResizeRows = false;
            this.dgvTab_Bancos.BackgroundColor = System.Drawing.Color.White;
            this.dgvTab_Bancos.Location = new System.Drawing.Point(12, 65);
            this.dgvTab_Bancos.MultiSelect = false;
            this.dgvTab_Bancos.Name = "dgvTab_Bancos";
            this.dgvTab_Bancos.ReadOnly = true;
            this.dgvTab_Bancos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTab_Bancos.Size = new System.Drawing.Size(996, 539);
            this.dgvTab_Bancos.TabIndex = 43;
            this.toolTip.SetToolTip(this.dgvTab_Bancos, "Visualizar Bancos (Febraban) - Pesquisa");
            this.dgvTab_Bancos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTab_Bancos_CellDoubleClick);
            this.dgvTab_Bancos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTab_Bancos_ColumnHeaderMouseClick);
            this.dgvTab_Bancos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTab_Bancos_KeyDown);
            // 
            // bwrTab_Bancos
            // 
            this.bwrTab_Bancos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrTab_Bancos_DoWork);
            this.bwrTab_Bancos.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrTab_Bancos_RunWorkerCompleted);
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
            this.gbxGroup.Size = new System.Drawing.Size(347, 47);
            this.gbxGroup.TabIndex = 45;
            this.gbxGroup.TabStop = false;
            this.gbxGroup.Text = "Pré-Filtro";
            this.toolTip.SetToolTip(this.gbxGroup, "Pré-Filtro");
            // 
            // rbnInativos
            // 
            this.rbnInativos.AccessibleDescription = "Bancos Inativos";
            this.rbnInativos.AutoSize = true;
            this.rbnInativos.Location = new System.Drawing.Point(216, 20);
            this.rbnInativos.Name = "rbnInativos";
            this.rbnInativos.Size = new System.Drawing.Size(101, 17);
            this.rbnInativos.TabIndex = 2;
            this.rbnInativos.Text = "Bancos Inativos";
            this.toolTip.SetToolTip(this.rbnInativos, "Bancos Inativos");
            this.rbnInativos.UseVisualStyleBackColor = true;
            this.rbnInativos.Click += new System.EventHandler(this.rbnFiltro);
            // 
            // rbnAtivos
            // 
            this.rbnAtivos.AccessibleDescription = "Bancos Ativos";
            this.rbnAtivos.AutoSize = true;
            this.rbnAtivos.Checked = true;
            this.rbnAtivos.Location = new System.Drawing.Point(111, 20);
            this.rbnAtivos.Name = "rbnAtivos";
            this.rbnAtivos.Size = new System.Drawing.Size(92, 17);
            this.rbnAtivos.TabIndex = 1;
            this.rbnAtivos.TabStop = true;
            this.rbnAtivos.Text = "Bancos Ativos";
            this.toolTip.SetToolTip(this.rbnAtivos, "Bancos Ativos");
            this.rbnAtivos.UseVisualStyleBackColor = true;
            this.rbnAtivos.Click += new System.EventHandler(this.rbnFiltro);
            // 
            // rbnTodos
            // 
            this.rbnTodos.AccessibleDescription = "Todos Bancos";
            this.rbnTodos.AutoSize = true;
            this.rbnTodos.Location = new System.Drawing.Point(7, 20);
            this.rbnTodos.Name = "rbnTodos";
            this.rbnTodos.Size = new System.Drawing.Size(91, 17);
            this.rbnTodos.TabIndex = 0;
            this.rbnTodos.Text = "Todos Bancos";
            this.toolTip.SetToolTip(this.rbnTodos, "Todos Bancos");
            this.rbnTodos.UseVisualStyleBackColor = true;
            this.rbnTodos.Click += new System.EventHandler(this.rbnFiltro);
            // 
            // frmTab_BancosPes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxGroup);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.dgvTab_Bancos);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTab_BancosPes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "202";
            this.Text = "Bancos  - Pesquisa";
            this.toolTip.SetToolTip(this, "Visualizar Bancos (Febraban) - Pesquisa");
            this.Activated += new System.EventHandler(this.frmTab_BancosPes_Activated);
            this.Load += new System.EventHandler(this.frmTab_BancoPes_Load);
            this.Shown += new System.EventHandler(this.frmTab_BancosPes_Shown);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTab_Bancos)).EndInit();
            this.gbxGroup.ResumeLayout(false);
            this.gbxGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.DataGridView dgvTab_Bancos;
        private System.ComponentModel.BackgroundWorker bwrTab_Bancos;
        private System.Windows.Forms.GroupBox gbxGroup;
        private System.Windows.Forms.RadioButton rbnInativos;
        private System.Windows.Forms.RadioButton rbnAtivos;
        private System.Windows.Forms.RadioButton rbnTodos;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
    }
}