namespace CRCorrea
{
    partial class frmEscFormVisPes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEscFormVisPes));
            this.tspEscFormVisPes = new System.Windows.Forms.ToolStrip();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxLocalizar = new System.Windows.Forms.ToolStripTextBox();
            this.bwrEscFormVisPes = new System.ComponentModel.BackgroundWorker();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dgvEscForm = new System.Windows.Forms.DataGridView();
            this.lblNomeTabela = new System.Windows.Forms.Label();
            this.tspEscFormVisPes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEscForm)).BeginInit();
            this.SuspendLayout();
            // 
            // tspEscFormVisPes
            // 
            this.tspEscFormVisPes.AccessibleDescription = "Barra de Opções";
            this.tspEscFormVisPes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspEscFormVisPes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspEscFormVisPes.Dock = System.Windows.Forms.DockStyle.None;
            this.tspEscFormVisPes.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspEscFormVisPes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspEscolher,
            this.tspImprimir,
            this.tspRetornar,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxLocalizar});
            this.tspEscFormVisPes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspEscFormVisPes.Location = new System.Drawing.Point(12, 582);
            this.tspEscFormVisPes.Name = "tspEscFormVisPes";
            this.tspEscFormVisPes.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspEscFormVisPes.Size = new System.Drawing.Size(558, 47);
            this.tspEscFormVisPes.TabIndex = 2;
            this.tspEscFormVisPes.Text = "tspEscFormVisPes";
            this.ToolTip.SetToolTip(this.tspEscFormVisPes, "Menu");
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
            // tspImprimir
            // 
            this.tspImprimir.AccessibleDescription = "Imprimir";
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimir.Image")));
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(66, 42);
            this.tspImprimir.Text = "I&mprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
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
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click_);
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
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(10, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(56, 13);
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
            this.tstbxLocalizar.Enter += new System.EventHandler(this.ControlEnter);
            this.tstbxLocalizar.Leave += new System.EventHandler(this.ControlLeave);
            this.tstbxLocalizar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tstbxLocalizar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // bwrEscFormVisPes
            // 
            this.bwrEscFormVisPes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrEscFormVisPes_DoWork);
            this.bwrEscFormVisPes.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrEscFormVisPes_RunWorkerCompleted);
            // 
            // dgvEscForm
            // 
            this.dgvEscForm.AccessibleDescription = "Pesquisa";
            this.dgvEscForm.AllowUserToAddRows = false;
            this.dgvEscForm.AllowUserToDeleteRows = false;
            this.dgvEscForm.AllowUserToResizeRows = false;
            this.dgvEscForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEscForm.BackgroundColor = System.Drawing.Color.White;
            this.dgvEscForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEscForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvEscForm.Location = new System.Drawing.Point(12, 65);
            this.dgvEscForm.MultiSelect = false;
            this.dgvEscForm.Name = "dgvEscForm";
            this.dgvEscForm.ReadOnly = true;
            this.dgvEscForm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvEscForm.Size = new System.Drawing.Size(996, 514);
            this.dgvEscForm.TabIndex = 1;
            this.dgvEscForm.TabStop = false;
            this.ToolTip.SetToolTip(this.dgvEscForm, "Grade de Pesquisa");
            this.dgvEscForm.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEscForm_CellDoubleClick);
            this.dgvEscForm.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEscForm_ColumnHeaderMouseClick);
            // 
            // lblNomeTabela
            // 
            this.lblNomeTabela.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeTabela.Location = new System.Drawing.Point(94, 12);
            this.lblNomeTabela.Name = "lblNomeTabela";
            this.lblNomeTabela.Size = new System.Drawing.Size(671, 35);
            this.lblNomeTabela.TabIndex = 3;
            this.lblNomeTabela.Text = "label1";
            this.lblNomeTabela.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmEscFormVisPes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.lblNomeTabela);
            this.Controls.Add(this.dgvEscForm);
            this.Controls.Add(this.tspEscFormVisPes);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(2, 21);
            this.Name = "frmEscFormVisPes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "";
            this.Text = "Formulário Padrão Pesquisa - clsVisualização";
            this.ToolTip.SetToolTip(this, "Formulário Padrão Pesquisa - clsVisualização");
            this.Activated += new System.EventHandler(this.frmEscFormVisPes_Activated);
            this.Load += new System.EventHandler(this.frmEscFormVisPes_Load);
            this.Shown += new System.EventHandler(this.frmEscFormVisPes_Shown);
            this.tspEscFormVisPes.ResumeLayout(false);
            this.tspEscFormVisPes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEscForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspEscFormVisPes;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.ComponentModel.BackgroundWorker bwrEscFormVisPes;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxLocalizar;
        private System.Windows.Forms.DataGridView dgvEscForm;
        private System.Windows.Forms.Label lblNomeTabela;
    }
}