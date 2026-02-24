namespace CRCorrea
{
    partial class frmPecasPes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPecasPes));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxPeca = new System.Windows.Forms.GroupBox();
            this.pbxPeca = new System.Windows.Forms.PictureBox();
            this.dgvPecas = new System.Windows.Forms.DataGridView();
            this.labelQtde = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxAtivoInativo = new System.Windows.Forms.GroupBox();
            this.rbnAtivoRevisao = new System.Windows.Forms.RadioButton();
            this.rbnAtivoTodos = new System.Windows.Forms.RadioButton();
            this.rbnAtivoSim = new System.Windows.Forms.RadioButton();
            this.rbnAtivoNao = new System.Windows.Forms.RadioButton();
            this.tstPesquisa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxPeca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPeca)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPecas)).BeginInit();
            this.tspTool.SuspendLayout();
            this.gbxAtivoInativo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxPeca
            // 
            this.gbxPeca.AccessibleDescription = "Peça";
            this.gbxPeca.Controls.Add(this.pbxPeca);
            this.gbxPeca.Controls.Add(this.dgvPecas);
            this.gbxPeca.Location = new System.Drawing.Point(10, 65);
            this.gbxPeca.Name = "gbxPeca";
            this.gbxPeca.Size = new System.Drawing.Size(998, 528);
            this.gbxPeca.TabIndex = 1;
            this.gbxPeca.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxPeca, "Visualizar Centros de Custos");
            // 
            // pbxPeca
            // 
            this.pbxPeca.AccessibleDescription = "Carregando Peças";
            this.pbxPeca.BackColor = System.Drawing.Color.White;
            this.pbxPeca.Location = new System.Drawing.Point(9, 36);
            this.pbxPeca.Name = "pbxPeca";
            this.pbxPeca.Size = new System.Drawing.Size(969, 483);
            this.pbxPeca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxPeca.TabIndex = 141;
            this.pbxPeca.TabStop = false;
            this.pbxPeca.Visible = false;
            // 
            // dgvPecas
            // 
            this.dgvPecas.AccessibleDescription = "Peças";
            this.dgvPecas.AllowUserToAddRows = false;
            this.dgvPecas.AllowUserToDeleteRows = false;
            this.dgvPecas.AllowUserToOrderColumns = true;
            this.dgvPecas.AllowUserToResizeRows = false;
            this.dgvPecas.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPecas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPecas.Location = new System.Drawing.Point(9, 19);
            this.dgvPecas.MultiSelect = false;
            this.dgvPecas.Name = "dgvPecas";
            this.dgvPecas.ReadOnly = true;
            this.dgvPecas.RowHeadersVisible = false;
            this.dgvPecas.Size = new System.Drawing.Size(982, 502);
            this.dgvPecas.StandardTab = true;
            this.dgvPecas.TabIndex = 2;
            this.dgvPecas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvPeca_MouseDoubleClick);
            // 
            // labelQtde
            // 
            this.labelQtde.AutoSize = true;
            this.labelQtde.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQtde.Location = new System.Drawing.Point(735, 609);
            this.labelQtde.Name = "labelQtde";
            this.labelQtde.Size = new System.Drawing.Size(181, 13);
            this.labelQtde.TabIndex = 48;
            this.labelQtde.Text = "Total de Itens Cadastrados =>";
            // 
            // tspTool
            // 
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
            this.tslbLocalizar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(13, 597);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(435, 47);
            this.tspTool.TabIndex = 0;
            // 
            // tspEscolher
            // 
            this.tspEscolher.AutoSize = false;
            this.tspEscolher.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // label3
            // 
            this.label3.AccessibleDescription = "Certificado de Qualidade (Visualização)";
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(167, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(414, 50);
            this.label3.TabIndex = 138;
            this.label3.Text = "Visualizar Cadastro de Materiais/Produtos";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxAtivoInativo
            // 
            this.gbxAtivoInativo.AccessibleDescription = "Opção";
            this.gbxAtivoInativo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxAtivoInativo.Controls.Add(this.rbnAtivoRevisao);
            this.gbxAtivoInativo.Controls.Add(this.rbnAtivoTodos);
            this.gbxAtivoInativo.Controls.Add(this.rbnAtivoSim);
            this.gbxAtivoInativo.Controls.Add(this.rbnAtivoNao);
            this.gbxAtivoInativo.Location = new System.Drawing.Point(671, 20);
            this.gbxAtivoInativo.Name = "gbxAtivoInativo";
            this.gbxAtivoInativo.Size = new System.Drawing.Size(253, 42);
            this.gbxAtivoInativo.TabIndex = 137;
            this.gbxAtivoInativo.TabStop = false;
            // 
            // rbnAtivoRevisao
            // 
            this.rbnAtivoRevisao.AccessibleDescription = "Revisão";
            this.rbnAtivoRevisao.AutoSize = true;
            this.rbnAtivoRevisao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAtivoRevisao.Location = new System.Drawing.Point(186, 15);
            this.rbnAtivoRevisao.Name = "rbnAtivoRevisao";
            this.rbnAtivoRevisao.Size = new System.Drawing.Size(63, 17);
            this.rbnAtivoRevisao.TabIndex = 17;
            this.rbnAtivoRevisao.Text = "Revisão";
            this.rbnAtivoRevisao.UseVisualStyleBackColor = true;
            this.rbnAtivoRevisao.Visible = false;
            // 
            // rbnAtivoTodos
            // 
            this.rbnAtivoTodos.AccessibleDescription = "Todos";
            this.rbnAtivoTodos.AutoSize = true;
            this.rbnAtivoTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAtivoTodos.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnAtivoTodos.Location = new System.Drawing.Point(5, 15);
            this.rbnAtivoTodos.Name = "rbnAtivoTodos";
            this.rbnAtivoTodos.Size = new System.Drawing.Size(54, 17);
            this.rbnAtivoTodos.TabIndex = 15;
            this.rbnAtivoTodos.Text = "Todos";
            this.rbnAtivoTodos.UseVisualStyleBackColor = true;
            // 
            // rbnAtivoSim
            // 
            this.rbnAtivoSim.AccessibleDescription = "Ativo";
            this.rbnAtivoSim.AutoSize = true;
            this.rbnAtivoSim.Checked = true;
            this.rbnAtivoSim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAtivoSim.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnAtivoSim.Location = new System.Drawing.Point(65, 15);
            this.rbnAtivoSim.Name = "rbnAtivoSim";
            this.rbnAtivoSim.Size = new System.Drawing.Size(50, 17);
            this.rbnAtivoSim.TabIndex = 13;
            this.rbnAtivoSim.TabStop = true;
            this.rbnAtivoSim.Text = "Ativo";
            this.rbnAtivoSim.UseVisualStyleBackColor = true;
            // 
            // rbnAtivoNao
            // 
            this.rbnAtivoNao.AccessibleDescription = "Inativo";
            this.rbnAtivoNao.AutoSize = true;
            this.rbnAtivoNao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnAtivoNao.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnAtivoNao.Location = new System.Drawing.Point(121, 15);
            this.rbnAtivoNao.Name = "rbnAtivoNao";
            this.rbnAtivoNao.Size = new System.Drawing.Size(59, 17);
            this.rbnAtivoNao.TabIndex = 14;
            this.rbnAtivoNao.Text = "Inativo";
            this.rbnAtivoNao.UseVisualStyleBackColor = true;
            // 
            // tstPesquisa
            // 
            this.tstPesquisa.Location = new System.Drawing.Point(229, 611);
            this.tstPesquisa.Name = "tstPesquisa";
            this.tstPesquisa.Size = new System.Drawing.Size(147, 21);
            this.tstPesquisa.TabIndex = 0;
            this.tstPesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstPesquisa_KeyDown);
            this.tstPesquisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstPesquisa_KeyUp);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkOrange;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(457, 602);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 19);
            this.label2.TabIndex = 140;
            this.label2.Text = "Comprar";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Salmon;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(457, 626);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 19);
            this.label1.TabIndex = 139;
            this.label1.Text = "Saldo Negativo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPecasPes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tstPesquisa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbxAtivoInativo);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.labelQtde);
            this.Controls.Add(this.gbxPeca);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPecasPes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Materiais/Produtos/Despesas - Visualização";
            this.toolTip1.SetToolTip(this, "Visualizar o Cadastro de Peças Coating");
            this.Activated += new System.EventHandler(this.frmPecasPes_Activated);
            this.Load += new System.EventHandler(this.frmPecasPes_Load);
            this.gbxPeca.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxPeca)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPecas)).EndInit();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxAtivoInativo.ResumeLayout(false);
            this.gbxAtivoInativo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbxPeca;
        private System.Windows.Forms.DataGridView dgvPecas;
        private System.Windows.Forms.PictureBox pbxPeca;
        private System.Windows.Forms.Label labelQtde;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripButton tspEscolher;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbxAtivoInativo;
        private System.Windows.Forms.RadioButton rbnAtivoRevisao;
        private System.Windows.Forms.RadioButton rbnAtivoTodos;
        private System.Windows.Forms.RadioButton rbnAtivoSim;
        private System.Windows.Forms.RadioButton rbnAtivoNao;
        private System.Windows.Forms.TextBox tstPesquisa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

