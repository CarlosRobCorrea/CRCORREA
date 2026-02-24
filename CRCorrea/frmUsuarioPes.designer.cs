namespace CRCorrea
{
    partial class frmUsuarioPes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsuarioPes));
            this.dgvUsuario = new System.Windows.Forms.DataGridView();
            this.gbxgroup = new System.Windows.Forms.GroupBox();
            this.cbxStatus = new System.Windows.Forms.ComboBox();
            this.tspDefault = new System.Windows.Forms.ToolStrip();
            this.tsbEscolher = new System.Windows.Forms.ToolStripButton();
            this.tsbRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbxProcurar = new System.Windows.Forms.ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuario)).BeginInit();
            this.gbxgroup.SuspendLayout();
            this.tspDefault.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUsuario
            // 
            this.dgvUsuario.AccessibleDescription = "Usuários";
            this.dgvUsuario.AllowUserToAddRows = false;
            this.dgvUsuario.AllowUserToDeleteRows = false;
            this.dgvUsuario.AllowUserToOrderColumns = true;
            this.dgvUsuario.AllowUserToResizeRows = false;
            this.dgvUsuario.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuario.Location = new System.Drawing.Point(12, 65);
            this.dgvUsuario.MultiSelect = false;
            this.dgvUsuario.Name = "dgvUsuario";
            this.dgvUsuario.ReadOnly = true;
            this.dgvUsuario.RowHeadersVisible = false;
            this.dgvUsuario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvUsuario.Size = new System.Drawing.Size(996, 498);
            this.dgvUsuario.StandardTab = true;
            this.dgvUsuario.TabIndex = 0;
            this.dgvUsuario.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuario_CellContentClick);
            this.dgvUsuario.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuario_CellDoubleClick);
            // 
            // gbxgroup
            // 
            this.gbxgroup.AccessibleDescription = "Status";
            this.gbxgroup.Controls.Add(this.cbxStatus);
            this.gbxgroup.Location = new System.Drawing.Point(12, 12);
            this.gbxgroup.Name = "gbxgroup";
            this.gbxgroup.Size = new System.Drawing.Size(135, 47);
            this.gbxgroup.TabIndex = 1;
            this.gbxgroup.TabStop = false;
            this.gbxgroup.Text = "Ativo";
            // 
            // cbxStatus
            // 
            this.cbxStatus.AccessibleDescription = "Status";
            this.cbxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxStatus.FormattingEnabled = true;
            this.cbxStatus.Items.AddRange(new object[] {
            "Todos",
            "Sim",
            "Não"});
            this.cbxStatus.Location = new System.Drawing.Point(6, 20);
            this.cbxStatus.Name = "cbxStatus";
            this.cbxStatus.Size = new System.Drawing.Size(123, 21);
            this.cbxStatus.TabIndex = 0;
            this.cbxStatus.SelectedIndexChanged += new System.EventHandler(this.cbxStatus_SelectedIndexChanged);
            // 
            // tspDefault
            // 
            this.tspDefault.AccessibleDescription = "Bara de Opções";
            this.tspDefault.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspDefault.AutoSize = false;
            this.tspDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspDefault.Dock = System.Windows.Forms.DockStyle.None;
            this.tspDefault.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspDefault.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tspDefault.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEscolher,
            this.tsbRetornar,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tstbxProcurar});
            this.tspDefault.Location = new System.Drawing.Point(12, 566);
            this.tspDefault.Name = "tspDefault";
            this.tspDefault.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspDefault.Size = new System.Drawing.Size(976, 46);
            this.tspDefault.TabIndex = 2;
            // 
            // tsbEscolher
            // 
            this.tsbEscolher.AccessibleDescription = "Escolher";
            this.tsbEscolher.AutoSize = false;
            this.tsbEscolher.Image = ((System.Drawing.Image)(resources.GetObject("tsbEscolher.Image")));
            this.tsbEscolher.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbEscolher.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEscolher.Name = "tsbEscolher";
            this.tsbEscolher.Size = new System.Drawing.Size(62, 41);
            this.tsbEscolher.Text = "Escolher";
            this.tsbEscolher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbEscolher.Click += new System.EventHandler(this.tsbEscolher_Click);
            // 
            // tsbRetornar
            // 
            this.tsbRetornar.AccessibleDescription = "Retornar";
            this.tsbRetornar.AutoSize = false;
            this.tsbRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tsbRetornar.Image")));
            this.tsbRetornar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRetornar.Name = "tsbRetornar";
            this.tsbRetornar.Size = new System.Drawing.Size(62, 41);
            this.tsbRetornar.Text = "Retornar";
            this.tsbRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRetornar.Click += new System.EventHandler(this.tsbRetornar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 46);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(59, 43);
            this.toolStripLabel1.Text = "Procurar:";
            // 
            // tstbxProcurar
            // 
            this.tstbxProcurar.AccessibleDescription = "Procurar";
            this.tstbxProcurar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxProcurar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxProcurar.Name = "tstbxProcurar";
            this.tstbxProcurar.Size = new System.Drawing.Size(120, 46);
            this.tstbxProcurar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxProcurar_KeyUp);
            // 
            // frmUsuarioPes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.tspDefault);
            this.Controls.Add(this.gbxgroup);
            this.Controls.Add(this.dgvUsuario);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUsuarioPes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Escolher Usuário";
            this.Activated += new System.EventHandler(this.frmUsuarioPes_Activated);
            this.Load += new System.EventHandler(this.frmUsuarioPes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuario)).EndInit();
            this.gbxgroup.ResumeLayout(false);
            this.tspDefault.ResumeLayout(false);
            this.tspDefault.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsuario;
        private System.Windows.Forms.GroupBox gbxgroup;
        private System.Windows.Forms.ToolStrip tspDefault;
        private System.Windows.Forms.ToolStripButton tsbEscolher;
        private System.Windows.Forms.ToolStripButton tsbRetornar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstbxProcurar;
        private System.Windows.Forms.ComboBox cbxStatus;

    }
}