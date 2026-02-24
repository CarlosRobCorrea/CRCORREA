namespace CRCorrea
{
    partial class frmUsuarioVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsuarioVis));
            this.dgvUsuario = new System.Windows.Forms.DataGridView();
            this.tspDefault = new System.Windows.Forms.ToolStrip();
            this.tsbIncluir = new System.Windows.Forms.ToolStripButton();
            this.tsbAlterar = new System.Windows.Forms.ToolStripButton();
            this.tsbImprimir = new System.Windows.Forms.ToolStripButton();
            this.tsbEscolher = new System.Windows.Forms.ToolStripButton();
            this.tsbExcluir = new System.Windows.Forms.ToolStripButton();
            this.tsbRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstPesquisa = new System.Windows.Forms.ToolStripTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxGroup = new System.Windows.Forms.GroupBox();
            this.rbnInativos = new System.Windows.Forms.RadioButton();
            this.rbnAtivos = new System.Windows.Forms.RadioButton();
            this.rbnTodos = new System.Windows.Forms.RadioButton();
            this.pbxUsuario = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuario)).BeginInit();
            this.tspDefault.SuspendLayout();
            this.gbxGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUsuario)).BeginInit();
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
            this.dgvUsuario.Size = new System.Drawing.Size(993, 516);
            this.dgvUsuario.StandardTab = true;
            this.dgvUsuario.TabIndex = 0;
            this.dgvUsuario.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvUsuario_MouseDoubleClick);
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
            this.tsbIncluir,
            this.tsbAlterar,
            this.tsbImprimir,
            this.tsbEscolher,
            this.tsbExcluir,
            this.tsbRetornar,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tstPesquisa});
            this.tspDefault.Location = new System.Drawing.Point(13, 588);
            this.tspDefault.Name = "tspDefault";
            this.tspDefault.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspDefault.Size = new System.Drawing.Size(984, 46);
            this.tspDefault.TabIndex = 2;
            // 
            // tsbIncluir
            // 
            this.tsbIncluir.AccessibleDescription = "Incluir";
            this.tsbIncluir.AutoSize = false;
            this.tsbIncluir.Image = global::CRCorrea.Properties.Resources.Incluir;
            this.tsbIncluir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbIncluir.Name = "tsbIncluir";
            this.tsbIncluir.Size = new System.Drawing.Size(62, 41);
            this.tsbIncluir.Text = "Incluir";
            this.tsbIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbIncluir.Click += new System.EventHandler(this.tsbIncluir_Click);
            // 
            // tsbAlterar
            // 
            this.tsbAlterar.AccessibleDescription = "Alterar";
            this.tsbAlterar.AutoSize = false;
            this.tsbAlterar.Image = global::CRCorrea.Properties.Resources.Editar;
            this.tsbAlterar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAlterar.Name = "tsbAlterar";
            this.tsbAlterar.Size = new System.Drawing.Size(62, 41);
            this.tsbAlterar.Text = "Alterar";
            this.tsbAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAlterar.Click += new System.EventHandler(this.tsbAlterar_Click);
            // 
            // tsbImprimir
            // 
            this.tsbImprimir.AccessibleDescription = "Imprimir";
            this.tsbImprimir.AutoSize = false;
            this.tsbImprimir.Image = global::CRCorrea.Properties.Resources.Imprimir;
            this.tsbImprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImprimir.Name = "tsbImprimir";
            this.tsbImprimir.Size = new System.Drawing.Size(62, 41);
            this.tsbImprimir.Text = "Imprimir";
            this.tsbImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbImprimir.Click += new System.EventHandler(this.tsbImprimir_Click);
            // 
            // tsbEscolher
            // 
            this.tsbEscolher.AccessibleDescription = "Escolher";
            this.tsbEscolher.AutoSize = false;
            this.tsbEscolher.Image = global::CRCorrea.Properties.Resources.Escolher;
            this.tsbEscolher.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbEscolher.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEscolher.Name = "tsbEscolher";
            this.tsbEscolher.Size = new System.Drawing.Size(62, 41);
            this.tsbEscolher.Text = "Escolher";
            this.tsbEscolher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbEscolher.Click += new System.EventHandler(this.tsbEscolher_Click);
            // 
            // tsbExcluir
            // 
            this.tsbExcluir.AccessibleDescription = "Excluir";
            this.tsbExcluir.AutoSize = false;
            this.tsbExcluir.Image = global::CRCorrea.Properties.Resources.Excluir;
            this.tsbExcluir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcluir.Name = "tsbExcluir";
            this.tsbExcluir.Size = new System.Drawing.Size(62, 41);
            this.tsbExcluir.Text = "Excluir";
            this.tsbExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExcluir.Click += new System.EventHandler(this.tsbExcluir_Click);
            // 
            // tsbRetornar
            // 
            this.tsbRetornar.AccessibleDescription = "Retornar";
            this.tsbRetornar.AutoSize = false;
            this.tsbRetornar.Image = global::CRCorrea.Properties.Resources.Retornar;
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
            // tstPesquisa
            // 
            this.tstPesquisa.AccessibleDescription = "Procurar";
            this.tstPesquisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstPesquisa.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstPesquisa.Name = "tstPesquisa";
            this.tstPesquisa.Size = new System.Drawing.Size(120, 46);
            this.tstPesquisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstPesquisa_KeyUp);
            this.tstPesquisa.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tstPesquisa_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(652, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 25);
            this.label1.TabIndex = 133;
            this.label1.Text = "Visualizar Cadastro de Usuários";
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
            this.gbxGroup.Size = new System.Drawing.Size(301, 48);
            this.gbxGroup.TabIndex = 0;
            this.gbxGroup.TabStop = false;
            this.gbxGroup.Text = "Usuario";
            // 
            // rbnInativos
            // 
            this.rbnInativos.AccessibleDescription = "Bancos Inativos";
            this.rbnInativos.AutoSize = true;
            this.rbnInativos.Location = new System.Drawing.Point(178, 20);
            this.rbnInativos.Name = "rbnInativos";
            this.rbnInativos.Size = new System.Drawing.Size(108, 17);
            this.rbnInativos.TabIndex = 2;
            this.rbnInativos.Text = "Usuarios Inativos";
            this.rbnInativos.UseVisualStyleBackColor = true;
            this.rbnInativos.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnInativos.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnInativos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnInativos.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAtivos
            // 
            this.rbnAtivos.AccessibleDescription = "Bancos Ativos";
            this.rbnAtivos.AutoSize = true;
            this.rbnAtivos.Checked = true;
            this.rbnAtivos.Location = new System.Drawing.Point(73, 20);
            this.rbnAtivos.Name = "rbnAtivos";
            this.rbnAtivos.Size = new System.Drawing.Size(99, 17);
            this.rbnAtivos.TabIndex = 1;
            this.rbnAtivos.TabStop = true;
            this.rbnAtivos.Text = "Usuarios Ativos";
            this.rbnAtivos.UseVisualStyleBackColor = true;
            this.rbnAtivos.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnAtivos.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtivos.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnTodos
            // 
            this.rbnTodos.AccessibleDescription = "Todos Bancos";
            this.rbnTodos.AutoSize = true;
            this.rbnTodos.Location = new System.Drawing.Point(7, 20);
            this.rbnTodos.Name = "rbnTodos";
            this.rbnTodos.Size = new System.Drawing.Size(57, 17);
            this.rbnTodos.TabIndex = 0;
            this.rbnTodos.Text = "Todos ";
            this.rbnTodos.UseVisualStyleBackColor = true;
            this.rbnTodos.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            this.rbnTodos.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTodos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnTodos.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // pbxUsuario
            // 
            this.pbxUsuario.AccessibleDescription = "Carregando";
            this.pbxUsuario.BackColor = System.Drawing.Color.White;
            this.pbxUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxUsuario.Image = ((System.Drawing.Image)(resources.GetObject("pbxUsuario.Image")));
            this.pbxUsuario.Location = new System.Drawing.Point(12, 65);
            this.pbxUsuario.Name = "pbxUsuario";
            this.pbxUsuario.Size = new System.Drawing.Size(993, 510);
            this.pbxUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxUsuario.TabIndex = 134;
            this.pbxUsuario.TabStop = false;
            // 
            // frmUsuarioVis
            // 
            this.AccessibleDescription = "Usuarios - Visualização";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.pbxUsuario);
            this.Controls.Add(this.gbxGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tspDefault);
            this.Controls.Add(this.dgvUsuario);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUsuarioVis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmUsuarioNewVis";
            this.Activated += new System.EventHandler(this.frmUsuarioVis_Activated);
            this.Load += new System.EventHandler(this.frmUsuarioVis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuario)).EndInit();
            this.tspDefault.ResumeLayout(false);
            this.tspDefault.PerformLayout();
            this.gbxGroup.ResumeLayout(false);
            this.gbxGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUsuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsuario;
        private System.Windows.Forms.ToolStrip tspDefault;
        private System.Windows.Forms.ToolStripButton tsbIncluir;
        private System.Windows.Forms.ToolStripButton tsbAlterar;
        private System.Windows.Forms.ToolStripButton tsbImprimir;
        private System.Windows.Forms.ToolStripButton tsbEscolher;
        private System.Windows.Forms.ToolStripButton tsbExcluir;
        private System.Windows.Forms.ToolStripButton tsbRetornar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstPesquisa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxGroup;
        private System.Windows.Forms.RadioButton rbnInativos;
        private System.Windows.Forms.RadioButton rbnAtivos;
        private System.Windows.Forms.RadioButton rbnTodos;
        private System.Windows.Forms.PictureBox pbxUsuario;

    }
}