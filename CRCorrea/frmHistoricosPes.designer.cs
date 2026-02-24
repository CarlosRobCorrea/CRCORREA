namespace CRCorrea
{
    partial class frmHistoricosPes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbxAtivo = new System.Windows.Forms.GroupBox();
            this.rbnInativos = new System.Windows.Forms.RadioButton();
            this.rbnAtivos = new System.Windows.Forms.RadioButton();
            this.rbnTodos = new System.Windows.Forms.RadioButton();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.dgvHistoricos = new System.Windows.Forms.DataGridView();
            this.bwrHistoricos = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.gbxAtivo.SuspendLayout();
            this.tspTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoricos)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxAtivo
            // 
            this.gbxAtivo.AccessibleDescription = "Pré-Filtro";
            this.gbxAtivo.Controls.Add(this.rbnInativos);
            this.gbxAtivo.Controls.Add(this.rbnAtivos);
            this.gbxAtivo.Controls.Add(this.rbnTodos);
            this.gbxAtivo.Location = new System.Drawing.Point(12, 12);
            this.gbxAtivo.Name = "gbxAtivo";
            this.gbxAtivo.Size = new System.Drawing.Size(347, 48);
            this.gbxAtivo.TabIndex = 49;
            this.gbxAtivo.TabStop = false;
            this.gbxAtivo.Text = "Pré-Filtro";
            this.toolTip1.SetToolTip(this.gbxAtivo, "Filtro");
            // 
            // rbnInativos
            // 
            this.rbnInativos.AccessibleDescription = "Inativos";
            this.rbnInativos.AutoSize = true;
            this.rbnInativos.Location = new System.Drawing.Point(216, 20);
            this.rbnInativos.Name = "rbnInativos";
            this.rbnInativos.Size = new System.Drawing.Size(64, 17);
            this.rbnInativos.TabIndex = 2;
            this.rbnInativos.Text = "Inativos";
            this.toolTip1.SetToolTip(this.rbnInativos, "Inativos");
            this.rbnInativos.UseVisualStyleBackColor = true;
            // 
            // rbnAtivos
            // 
            this.rbnAtivos.AccessibleDescription = "Ativos";
            this.rbnAtivos.AutoSize = true;
            this.rbnAtivos.Checked = true;
            this.rbnAtivos.Location = new System.Drawing.Point(111, 20);
            this.rbnAtivos.Name = "rbnAtivos";
            this.rbnAtivos.Size = new System.Drawing.Size(55, 17);
            this.rbnAtivos.TabIndex = 1;
            this.rbnAtivos.TabStop = true;
            this.rbnAtivos.Text = "Ativos";
            this.toolTip1.SetToolTip(this.rbnAtivos, "Ativos");
            this.rbnAtivos.UseVisualStyleBackColor = true;
            // 
            // rbnTodos
            // 
            this.rbnTodos.AccessibleDescription = "Todos";
            this.rbnTodos.AutoSize = true;
            this.rbnTodos.Location = new System.Drawing.Point(7, 20);
            this.rbnTodos.Name = "rbnTodos";
            this.rbnTodos.Size = new System.Drawing.Size(54, 17);
            this.rbnTodos.TabIndex = 0;
            this.rbnTodos.Text = "Todos";
            this.toolTip1.SetToolTip(this.rbnTodos, "Todos");
            this.rbnTodos.UseVisualStyleBackColor = true;
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspEscolher,
            this.tspRetornar,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(0, 584);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(1020, 101);
            this.tspTool.TabIndex = 48;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            this.toolTip1.SetToolTip(this.tspTool, "Barra de Opções");
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
            this.tstbxLocalizar.AccessibleDescription = "Pesquisa Rapida";
            this.tstbxLocalizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxLocalizar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxLocalizar.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxLocalizar.Name = "tstbxLocalizar";
            this.tstbxLocalizar.Size = new System.Drawing.Size(280, 21);
            this.tstbxLocalizar.ToolTipText = "Pesquisa Rapida";
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // dgvHistoricos
            // 
            this.dgvHistoricos.AccessibleDescription = "Historicos";
            this.dgvHistoricos.AllowUserToAddRows = false;
            this.dgvHistoricos.AllowUserToDeleteRows = false;
            this.dgvHistoricos.AllowUserToResizeColumns = false;
            this.dgvHistoricos.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.dgvHistoricos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHistoricos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistoricos.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistoricos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvHistoricos.Location = new System.Drawing.Point(12, 65);
            this.dgvHistoricos.MultiSelect = false;
            this.dgvHistoricos.Name = "dgvHistoricos";
            this.dgvHistoricos.ReadOnly = true;
            this.dgvHistoricos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvHistoricos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHistoricos.Size = new System.Drawing.Size(996, 572);
            this.dgvHistoricos.TabIndex = 47;
            this.toolTip1.SetToolTip(this.dgvHistoricos, "Grade - Históricos");
            // 
            // bwrHistoricos
            // 
            this.bwrHistoricos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrHistoricos_DoWork);
            this.bwrHistoricos.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrHistoricos_RunWorkerCompleted);
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "Historicos Pesquisa ApliBank";
            this.label5.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(541, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(467, 54);
            this.label5.TabIndex = 136;
            this.label5.Text = "Historicos Pesquisa ApliBank";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label5, "Historicos Pesquisa ApliBank");
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
            // frmHistoricosPes
            // 
            this.AccessibleDescription = "Historicos Pesquisa ApliBank";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbxAtivo);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.dgvHistoricos);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHistoricosPes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "410";
            this.Text = "Historicos Pesquisa ApliBank";
            this.toolTip1.SetToolTip(this, "Historicos Pesquisa ApliBank");
            this.Load += new System.EventHandler(this.frmHistoricosPes_Load);
            this.gbxAtivo.ResumeLayout(false);
            this.gbxAtivo.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoricos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxAtivo;
        private System.Windows.Forms.RadioButton rbnInativos;
        private System.Windows.Forms.RadioButton rbnAtivos;
        private System.Windows.Forms.RadioButton rbnTodos;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.DataGridView dgvHistoricos;
        private System.ComponentModel.BackgroundWorker bwrHistoricos;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.Label label5;
    }
}