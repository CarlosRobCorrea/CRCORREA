namespace CRCorrea
{ 
    partial class frmIpiPes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIpiPes));
            this.dgvIpi = new System.Windows.Forms.DataGridView();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.bwrIpi = new System.ComponentModel.BackgroundWorker();
            this.gbxFiltro = new System.Windows.Forms.GroupBox();
            this.rbnResumoN = new System.Windows.Forms.RadioButton();
            this.rbnResumoS = new System.Windows.Forms.RadioButton();
            this.rbnResumoT = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpi)).BeginInit();
            this.tspTool.SuspendLayout();
            this.gbxFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvIpi
            // 
            this.dgvIpi.AccessibleDescription = "Grid Ipi";
            this.dgvIpi.AllowUserToAddRows = false;
            this.dgvIpi.AllowUserToDeleteRows = false;
            this.dgvIpi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIpi.BackgroundColor = System.Drawing.Color.White;
            this.dgvIpi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIpi.Location = new System.Drawing.Point(12, 66);
            this.dgvIpi.MultiSelect = false;
            this.dgvIpi.Name = "dgvIpi";
            this.dgvIpi.ReadOnly = true;
            this.dgvIpi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvIpi.Size = new System.Drawing.Size(996, 487);
            this.dgvIpi.TabIndex = 3;
            this.toolTip1.SetToolTip(this.dgvIpi, "Grade - IPI");
            this.dgvIpi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIpi_CellContentClick);
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
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
            this.tspTool.Location = new System.Drawing.Point(12, 556);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(996, 47);
            this.tspTool.TabIndex = 6;
            this.toolTip1.SetToolTip(this.tspTool, "Barra de Opções");
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
            // tspSeparador1
            // 
            this.tspSeparador1.AutoSize = false;
            this.tspSeparador1.Name = "tspSeparador1";
            this.tspSeparador1.Size = new System.Drawing.Size(6, 47);
            // 
            // tslbLocalizar
            // 
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(69, 16);
            this.tslbLocalizar.Text = "Procurar:";
            // 
            // tstbxLocalizar
            // 
            this.tstbxLocalizar.AccessibleDescription = "Procurar";
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(120, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // bwrIpi
            // 
            this.bwrIpi.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrIpi_DoWork);
            this.bwrIpi.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrIpi_RunWorkerCompleted);
            // 
            // gbxFiltro
            // 
            this.gbxFiltro.AccessibleDescription = "Filtro";
            this.gbxFiltro.Controls.Add(this.rbnResumoN);
            this.gbxFiltro.Controls.Add(this.rbnResumoS);
            this.gbxFiltro.Controls.Add(this.rbnResumoT);
            this.gbxFiltro.Location = new System.Drawing.Point(12, 12);
            this.gbxFiltro.Name = "gbxFiltro";
            this.gbxFiltro.Size = new System.Drawing.Size(236, 48);
            this.gbxFiltro.TabIndex = 9;
            this.gbxFiltro.TabStop = false;
            this.gbxFiltro.Text = "Filtro";
            this.toolTip1.SetToolTip(this.gbxFiltro, "Grupo - Filtro");
            // 
            // rbnResumoN
            // 
            this.rbnResumoN.AccessibleDescription = "Só Normal";
            this.rbnResumoN.AutoSize = true;
            this.rbnResumoN.Location = new System.Drawing.Point(155, 20);
            this.rbnResumoN.Name = "rbnResumoN";
            this.rbnResumoN.Size = new System.Drawing.Size(73, 17);
            this.rbnResumoN.TabIndex = 3;
            this.rbnResumoN.Tag = "";
            this.rbnResumoN.Text = "Só Normal";
            this.toolTip1.SetToolTip(this.rbnResumoN, "Só Normal");
            this.rbnResumoN.UseVisualStyleBackColor = true;
            this.rbnResumoN.Click += new System.EventHandler(this.rbnFiltro);
            // 
            // rbnResumoS
            // 
            this.rbnResumoS.AccessibleDescription = "Só Resumo";
            this.rbnResumoS.AutoSize = true;
            this.rbnResumoS.Location = new System.Drawing.Point(66, 20);
            this.rbnResumoS.Name = "rbnResumoS";
            this.rbnResumoS.Size = new System.Drawing.Size(78, 17);
            this.rbnResumoS.TabIndex = 1;
            this.rbnResumoS.Tag = "";
            this.rbnResumoS.Text = "Só Resumo";
            this.toolTip1.SetToolTip(this.rbnResumoS, "Só Resumo");
            this.rbnResumoS.UseVisualStyleBackColor = true;
            this.rbnResumoS.Click += new System.EventHandler(this.rbnFiltro);
            // 
            // rbnResumoT
            // 
            this.rbnResumoT.AccessibleDescription = "Todos";
            this.rbnResumoT.AutoSize = true;
            this.rbnResumoT.Checked = true;
            this.rbnResumoT.Location = new System.Drawing.Point(6, 20);
            this.rbnResumoT.Name = "rbnResumoT";
            this.rbnResumoT.Size = new System.Drawing.Size(54, 17);
            this.rbnResumoT.TabIndex = 0;
            this.rbnResumoT.TabStop = true;
            this.rbnResumoT.Tag = "";
            this.rbnResumoT.Text = "Todos";
            this.toolTip1.SetToolTip(this.rbnResumoT, "Todos");
            this.rbnResumoT.UseVisualStyleBackColor = true;
            this.rbnResumoT.Click += new System.EventHandler(this.rbnFiltro);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(469, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(381, 19);
            this.label1.TabIndex = 142;
            this.label1.Text = "I.P.I. - Imposto sobre Produto Industrializado";
            // 
            // frmIpiPes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbxFiltro);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.dgvIpi);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 50);
            this.Name = "frmIpiPes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "538";
            this.Text = "IPI - Pesquisando";
            this.toolTip1.SetToolTip(this, "IPI - Pesquisando");
            this.Load += new System.EventHandler(this.frmIpiPes_Load);
            this.Shown += new System.EventHandler(this.frmIpiPes_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpi)).EndInit();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxFiltro.ResumeLayout(false);
            this.gbxFiltro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvIpi;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.ComponentModel.BackgroundWorker bwrIpi;
        private System.Windows.Forms.GroupBox gbxFiltro;
        private System.Windows.Forms.RadioButton rbnResumoN;
        private System.Windows.Forms.RadioButton rbnResumoS;
        private System.Windows.Forms.RadioButton rbnResumoT;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
    }
}