namespace CRCorrea
{
    partial class frmFormIdVis
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
            this.bwrFormId = new System.ComponentModel.BackgroundWorker();
            this.gbxRegistrar = new System.Windows.Forms.GroupBox();
            this.tbxNome1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxCodigo1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gbxCodigos = new System.Windows.Forms.GroupBox();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.dgvFormId = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxCognome = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar1 = new System.Windows.Forms.ToolStripButton();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxRegistrar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbxCodigos.SuspendLayout();
            this.tspTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormId)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bwrFormId
            // 
            this.bwrFormId.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrFormId_DoWork);
            this.bwrFormId.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrFormId_RunWorkerCompleted);
            // 
            // gbxRegistrar
            // 
            this.gbxRegistrar.AccessibleDescription = "Grupo - Registrando os Dados";
            this.gbxRegistrar.Controls.Add(this.tbxNome1);
            this.gbxRegistrar.Controls.Add(this.label3);
            this.gbxRegistrar.Controls.Add(this.tbxCodigo1);
            this.gbxRegistrar.Controls.Add(this.label4);
            this.gbxRegistrar.Controls.Add(this.toolStrip1);
            this.gbxRegistrar.Location = new System.Drawing.Point(92, 478);
            this.gbxRegistrar.Name = "gbxRegistrar";
            this.gbxRegistrar.Size = new System.Drawing.Size(841, 186);
            this.gbxRegistrar.TabIndex = 2;
            this.gbxRegistrar.TabStop = false;
            this.gbxRegistrar.Text = "Grupo - Registrando os Dados";
            this.toolTip1.SetToolTip(this.gbxRegistrar, "Grupo - Registrando os Dados");
            this.gbxRegistrar.Visible = false;
            // 
            // tbxNome1
            // 
            this.tbxNome1.AccessibleDescription = "Descrição";
            this.tbxNome1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNome1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome1.Location = new System.Drawing.Point(138, 63);
            this.tbxNome1.MaxLength = 50;
            this.tbxNome1.Name = "tbxNome1";
            this.tbxNome1.Size = new System.Drawing.Size(697, 21);
            this.tbxNome1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbxNome1, "Descrição");
            this.tbxNome1.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNome1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome1.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Descrição";
            // 
            // tbxCodigo1
            // 
            this.tbxCodigo1.AccessibleDescription = "Código";
            this.tbxCodigo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigo1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo1.Location = new System.Drawing.Point(26, 63);
            this.tbxCodigo1.MaxLength = 10;
            this.tbxCodigo1.Name = "tbxCodigo1";
            this.tbxCodigo1.Size = new System.Drawing.Size(104, 21);
            this.tbxCodigo1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxCodigo1, "Código");
            this.tbxCodigo1.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCodigo1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigo1.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Código:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleDescription = "Barra de Opções";
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.toolStripButton7,
            this.tspRetornar1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(3, 138);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(835, 45);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            this.toolTip1.SetToolTip(this.toolStrip1, "Barra de Opções - Registrando Dados");
            // 
            // gbxCodigos
            // 
            this.gbxCodigos.AccessibleDescription = "Tipo Documento";
            this.gbxCodigos.Controls.Add(this.tspTool);
            this.gbxCodigos.Controls.Add(this.dgvFormId);
            this.gbxCodigos.Location = new System.Drawing.Point(87, 117);
            this.gbxCodigos.Name = "gbxCodigos";
            this.gbxCodigos.Size = new System.Drawing.Size(846, 340);
            this.gbxCodigos.TabIndex = 1;
            this.gbxCodigos.TabStop = false;
            this.gbxCodigos.Text = "Tipo Documento";
            this.toolTip1.SetToolTip(this.gbxCodigos, "Grupo - Tipo de Documento");
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspImprimir,
            this.tspExcluir,
            this.tspRetornar,
            this.toolStripSeparator1,
            this.tslbLocalizar,
            this.toolStripTextBox1});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(3, 290);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(840, 47);
            this.tspTool.TabIndex = 3;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            this.toolTip1.SetToolTip(this.tspTool, "Barra de Opções Documentos");
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
            this.tslbLocalizar.Size = new System.Drawing.Size(65, 16);
            this.tslbLocalizar.Text = "Procurar";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.AccessibleDescription = "Procurar";
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripTextBox1.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(200, 21);
            this.toolStripTextBox1.ToolTipText = "Procurar";
            this.toolStripTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // dgvFormId
            // 
            this.dgvFormId.AccessibleDescription = "Documentos";
            this.dgvFormId.AllowUserToAddRows = false;
            this.dgvFormId.AllowUserToDeleteRows = false;
            this.dgvFormId.BackgroundColor = System.Drawing.Color.White;
            this.dgvFormId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormId.Location = new System.Drawing.Point(8, 16);
            this.dgvFormId.MultiSelect = false;
            this.dgvFormId.Name = "dgvFormId";
            this.dgvFormId.ReadOnly = true;
            this.dgvFormId.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvFormId.Size = new System.Drawing.Size(816, 261);
            this.dgvFormId.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dgvFormId, "Grade - Documentos");
            this.dgvFormId.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEscForm_CellDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "Dados";
            this.groupBox2.Controls.Add(this.tbxCodigo);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.tbxNome);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbxCognome);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(87, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(846, 72);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, "Grupo - Cabeçalho");
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.AccessibleDescription = "Número Código";
            this.tbxCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Location = new System.Drawing.Point(12, 26);
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(61, 21);
            this.tbxCodigo.TabIndex = 0;
            this.tbxCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxCodigo, "N° Código");
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 11);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Nrº Codigo";
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Nome";
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(255, 27);
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(523, 21);
            this.tbxNome.TabIndex = 2;
            this.toolTip1.SetToolTip(this.tbxNome, "Nome");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CodNome";
            // 
            // tbxCognome
            // 
            this.tbxCognome.AccessibleDescription = "Código Nome";
            this.tbxCognome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCognome.Location = new System.Drawing.Point(79, 27);
            this.tbxCognome.Name = "tbxCognome";
            this.tbxCognome.Size = new System.Drawing.Size(171, 21);
            this.tbxCognome.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbxCognome, "Cognome");
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = global::CRCorrea.Properties.Resources.Salvar;
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(66, 42);
            this.tspSalvar.Text = "&Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.AccessibleDescription = "Ordem";
            this.toolStripButton7.AutoSize = false;
            this.toolStripButton7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton7.Image = global::CRCorrea.Properties.Resources.Ordenar;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(66, 42);
            this.toolStripButton7.Text = "&Ordem";
            this.toolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton7.ToolTipText = "Salvar";
            // 
            // tspRetornar1
            // 
            this.tspRetornar1.AccessibleDescription = "Retornar";
            this.tspRetornar1.AutoSize = false;
            this.tspRetornar1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar1.Image = global::CRCorrea.Properties.Resources.Retornar;
            this.tspRetornar1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar1.Name = "tspRetornar1";
            this.tspRetornar1.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar1.Text = "Retorna&r";
            this.tspRetornar1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar1.Click += new System.EventHandler(this.tspRetornar1_Click);
            // 
            // tspIncluir
            // 
            this.tspIncluir.AccessibleDescription = "Incluir";
            this.tspIncluir.AutoSize = false;
            this.tspIncluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspIncluir.Image = global::CRCorrea.Properties.Resources.Incluir;
            this.tspIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspIncluir.Name = "tspIncluir";
            this.tspIncluir.Size = new System.Drawing.Size(66, 42);
            this.tspIncluir.Text = "&Incluir";
            this.tspIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspIncluir.Click += new System.EventHandler(this.tspIncluir_Click_1);
            // 
            // tspAlterar
            // 
            this.tspAlterar.AccessibleDescription = "Alterar";
            this.tspAlterar.AutoSize = false;
            this.tspAlterar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAlterar.Image = global::CRCorrea.Properties.Resources.Editar;
            this.tspAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAlterar.Name = "tspAlterar";
            this.tspAlterar.Size = new System.Drawing.Size(66, 42);
            this.tspAlterar.Text = "&Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.ToolTipText = "Alterar";
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click_1);
            // 
            // tspImprimir
            // 
            this.tspImprimir.AccessibleDescription = "Imprimir";
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = global::CRCorrea.Properties.Resources.Imprimir;
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(66, 42);
            this.tspImprimir.Text = "I&mprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Click += new System.EventHandler(this.tspImprimir_Click);
            // 
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir";
            this.tspExcluir.AutoSize = false;
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.Image = global::CRCorrea.Properties.Resources.Excluir;
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(66, 42);
            this.tspExcluir.Text = "E&xcluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
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
            // frmFormIdVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxRegistrar);
            this.Controls.Add(this.gbxCodigos);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmFormIdVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Formulários Padrão - Visualização ";
            this.toolTip1.SetToolTip(this, "Formulários Padrão - Visualização ");
            this.Activated += new System.EventHandler(this.frmFormIdVis_Activated);
            this.Load += new System.EventHandler(this.frmFormIdVis_Load);
            this.gbxRegistrar.ResumeLayout(false);
            this.gbxRegistrar.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbxCodigos.ResumeLayout(false);
            this.gbxCodigos.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormId)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bwrFormId;
        private System.Windows.Forms.GroupBox gbxRegistrar;
        private System.Windows.Forms.TextBox tbxNome1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxCodigo1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton tspRetornar1;
        private System.Windows.Forms.GroupBox gbxCodigos;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.DataGridView dgvFormId;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxCognome;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
    }
}