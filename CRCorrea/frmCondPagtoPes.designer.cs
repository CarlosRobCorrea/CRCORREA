namespace CRCorrea
{
    partial class frmCondPagtoPes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCondPagtoPes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxCondPagto = new System.Windows.Forms.GroupBox();
            this.dgvCondpagto = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbnTodos = new System.Windows.Forms.RadioButton();
            this.rbnInativo = new System.Windows.Forms.RadioButton();
            this.rbnAtivo = new System.Windows.Forms.RadioButton();
            this.tspTool.SuspendLayout();
            this.gbxCondPagto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondpagto)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspTool
            // 
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.tspTool.Location = new System.Drawing.Point(7, 558);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(415, 47);
            this.tspTool.TabIndex = 6;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            // 
            // tspEscolher
            // 
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
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(200, 21);
            this.tstbxLocalizar.ToolTipText = "Procurar";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // gbxCondPagto
            // 
            this.gbxCondPagto.Controls.Add(this.dgvCondpagto);
            this.gbxCondPagto.Location = new System.Drawing.Point(7, 58);
            this.gbxCondPagto.Name = "gbxCondPagto";
            this.gbxCondPagto.Size = new System.Drawing.Size(1001, 497);
            this.gbxCondPagto.TabIndex = 7;
            this.gbxCondPagto.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxCondPagto, "Visualizando as Condições de Pagamento");
            // 
            // dgvCondpagto
            // 
            this.dgvCondpagto.AccessibleDescription = "Grid";
            this.dgvCondpagto.AllowUserToAddRows = false;
            this.dgvCondpagto.AllowUserToDeleteRows = false;
            this.dgvCondpagto.AllowUserToOrderColumns = true;
            this.dgvCondpagto.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvCondpagto.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCondpagto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCondpagto.BackgroundColor = System.Drawing.Color.White;
            this.dgvCondpagto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCondpagto.Location = new System.Drawing.Point(5, 12);
            this.dgvCondpagto.MultiSelect = false;
            this.dgvCondpagto.Name = "dgvCondpagto";
            this.dgvCondpagto.ReadOnly = true;
            this.dgvCondpagto.RowHeadersVisible = false;
            this.dgvCondpagto.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCondpagto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCondpagto.Size = new System.Drawing.Size(989, 474);
            this.dgvCondpagto.StandardTab = true;
            this.dgvCondpagto.TabIndex = 6;
            this.toolTip1.SetToolTip(this.dgvCondpagto, "Grade - Condições de Pagamentos");
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(259, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(497, 25);
            this.label6.TabIndex = 172;
            this.label6.Text = "Pesquisando as Condições de Pagamento";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox1.Controls.Add(this.rbnTodos);
            this.groupBox1.Controls.Add(this.rbnInativo);
            this.groupBox1.Controls.Add(this.rbnAtivo);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 40);
            this.groupBox1.TabIndex = 173;
            this.groupBox1.TabStop = false;
            // 
            // rbnTodos
            // 
            this.rbnTodos.AutoSize = true;
            this.rbnTodos.Checked = true;
            this.rbnTodos.Location = new System.Drawing.Point(6, 15);
            this.rbnTodos.Name = "rbnTodos";
            this.rbnTodos.Size = new System.Drawing.Size(57, 17);
            this.rbnTodos.TabIndex = 0;
            this.rbnTodos.TabStop = true;
            this.rbnTodos.Text = "Ambos";
            this.rbnTodos.UseVisualStyleBackColor = true;
            this.rbnTodos.CheckedChanged += new System.EventHandler(this.rbnInativo_CheckedChanged);
            // 
            // rbnInativo
            // 
            this.rbnInativo.AutoSize = true;
            this.rbnInativo.Location = new System.Drawing.Point(125, 15);
            this.rbnInativo.Name = "rbnInativo";
            this.rbnInativo.Size = new System.Drawing.Size(59, 17);
            this.rbnInativo.TabIndex = 0;
            this.rbnInativo.TabStop = true;
            this.rbnInativo.Text = "Inativo";
            this.rbnInativo.UseVisualStyleBackColor = true;
            this.rbnInativo.CheckedChanged += new System.EventHandler(this.rbnInativo_CheckedChanged);
            // 
            // rbnAtivo
            // 
            this.rbnAtivo.AutoSize = true;
            this.rbnAtivo.Location = new System.Drawing.Point(69, 15);
            this.rbnAtivo.Name = "rbnAtivo";
            this.rbnAtivo.Size = new System.Drawing.Size(50, 17);
            this.rbnAtivo.TabIndex = 0;
            this.rbnAtivo.TabStop = true;
            this.rbnAtivo.Text = "Ativo";
            this.rbnAtivo.UseVisualStyleBackColor = true;
            this.rbnAtivo.CheckedChanged += new System.EventHandler(this.rbnInativo_CheckedChanged);
            // 
            // frmCondPagtoPes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gbxCondPagto);
            this.Controls.Add(this.tspTool);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCondPagtoPes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "240";
            this.Text = "Condições de Pagamento - Pesquisando";
            this.toolTip1.SetToolTip(this, "Condições de Pagamento - Pesquisando");
            this.Activated += new System.EventHandler(this.frmCondPagtoPes_Activated);
            this.Shown += new System.EventHandler(this.frmCondPagtoPes_Shown);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxCondPagto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondpagto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.GroupBox gbxCondPagto;
        private System.Windows.Forms.DataGridView dgvCondpagto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbnTodos;
        private System.Windows.Forms.RadioButton rbnInativo;
        private System.Windows.Forms.RadioButton rbnAtivo;
    }
}