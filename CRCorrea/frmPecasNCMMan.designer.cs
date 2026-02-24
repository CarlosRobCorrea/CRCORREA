namespace CRCorrea
{
    partial class frmPecasNCMMan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPecasNCMMan));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxParametros = new System.Windows.Forms.GroupBox();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClassfiscal = new System.Windows.Forms.Button();
            this.tbxClassificacaoDe = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxNomeBusca = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxCodigoNomeDe = new System.Windows.Forms.TextBox();
            this.btnCodigoDe = new System.Windows.Forms.Button();
            this.tbxCodigoDe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxEstoqueManual = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxParametros.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbxEstoqueManual.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxParametros
            // 
            this.gbxParametros.AccessibleDescription = "Parametros de Filtragem do Acerto";
            this.gbxParametros.Controls.Add(this.btnExecutar);
            this.gbxParametros.Controls.Add(this.groupBox2);
            this.gbxParametros.Controls.Add(this.groupBox1);
            this.gbxParametros.Location = new System.Drawing.Point(14, 48);
            this.gbxParametros.Name = "gbxParametros";
            this.gbxParametros.Size = new System.Drawing.Size(957, 129);
            this.gbxParametros.TabIndex = 1;
            this.gbxParametros.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxParametros, "Parametros de Filtragem do Acerto");
            // 
            // btnExecutar
            // 
            this.btnExecutar.AccessibleDescription = "Executar conforme parametros";
            this.btnExecutar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnExecutar.Image")));
            this.btnExecutar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutar.Location = new System.Drawing.Point(715, 69);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(238, 40);
            this.btnExecutar.TabIndex = 2;
            this.btnExecutar.TabStop = false;
            this.btnExecutar.Text = "Executar Conforme Parametros";
            this.toolTip1.SetToolTip(this.btnExecutar, "Executar conforme parametros");
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "Pertence ao Grupo de :";
            this.groupBox2.Controls.Add(this.btnClassfiscal);
            this.groupBox2.Controls.Add(this.tbxClassificacaoDe);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(711, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(146, 49);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Codigo NCM :";
            this.toolTip1.SetToolTip(this.groupBox2, "Grupo Pertence ao Grupo de :");
            // 
            // btnClassfiscal
            // 
            this.btnClassfiscal.AccessibleDescription = "Peças Grupo";
            this.btnClassfiscal.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClassfiscal.Image = ((System.Drawing.Image)(resources.GetObject("btnClassfiscal.Image")));
            this.btnClassfiscal.Location = new System.Drawing.Point(110, 13);
            this.btnClassfiscal.Name = "btnClassfiscal";
            this.btnClassfiscal.Size = new System.Drawing.Size(24, 24);
            this.btnClassfiscal.TabIndex = 25;
            this.btnClassfiscal.TabStop = false;
            this.btnClassfiscal.Text = "...";
            this.toolTip1.SetToolTip(this.btnClassfiscal, "Grupo");
            this.btnClassfiscal.UseVisualStyleBackColor = true;
            this.btnClassfiscal.Click += new System.EventHandler(this.btnClassfiscal_Click);
            // 
            // tbxClassificacaoDe
            // 
            this.tbxClassificacaoDe.AccessibleDescription = "Classificação de";
            this.tbxClassificacaoDe.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxClassificacaoDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxClassificacaoDe.Location = new System.Drawing.Point(4, 16);
            this.tbxClassificacaoDe.Name = "tbxClassificacaoDe";
            this.tbxClassificacaoDe.Size = new System.Drawing.Size(104, 21);
            this.tbxClassificacaoDe.TabIndex = 0;
            this.tbxClassificacaoDe.TabStop = false;
            this.toolTip1.SetToolTip(this.tbxClassificacaoDe, "Grupo de Material");
            this.tbxClassificacaoDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxClassificacaoDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxClassificacaoDe.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Parametros dos Itens";
            this.groupBox1.Controls.Add(this.tbxNomeBusca);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxCodigoNomeDe);
            this.groupBox1.Controls.Add(this.btnCodigoDe);
            this.groupBox1.Controls.Add(this.tbxCodigoDe);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(699, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametros dos Itens";
            this.toolTip1.SetToolTip(this.groupBox1, "Grupo Parametros dos Itens");
            // 
            // tbxNomeBusca
            // 
            this.tbxNomeBusca.AccessibleDescription = "Do Código";
            this.tbxNomeBusca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxNomeBusca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxNomeBusca.BackColor = System.Drawing.Color.White;
            this.tbxNomeBusca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNomeBusca.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNomeBusca.Location = new System.Drawing.Point(200, 70);
            this.tbxNomeBusca.Name = "tbxNomeBusca";
            this.tbxNomeBusca.Size = new System.Drawing.Size(453, 18);
            this.tbxNomeBusca.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(188, 13);
            this.label12.TabIndex = 103;
            this.label12.Text = "Nome do Material  a ser Incluido NCM ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 101;
            this.label2.Text = "Codigo  Material ";
            // 
            // tbxCodigoNomeDe
            // 
            this.tbxCodigoNomeDe.AccessibleDescription = "Do Código";
            this.tbxCodigoNomeDe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxCodigoNomeDe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxCodigoNomeDe.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxCodigoNomeDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigoNomeDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigoNomeDe.Location = new System.Drawing.Point(100, 46);
            this.tbxCodigoNomeDe.Name = "tbxCodigoNomeDe";
            this.tbxCodigoNomeDe.ReadOnly = true;
            this.tbxCodigoNomeDe.Size = new System.Drawing.Size(553, 18);
            this.tbxCodigoNomeDe.TabIndex = 1;
            // 
            // btnCodigoDe
            // 
            this.btnCodigoDe.AccessibleDescription = "Código de";
            this.btnCodigoDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCodigoDe.Image = ((System.Drawing.Image)(resources.GetObject("btnCodigoDe.Image")));
            this.btnCodigoDe.Location = new System.Drawing.Point(658, 43);
            this.btnCodigoDe.Name = "btnCodigoDe";
            this.btnCodigoDe.Size = new System.Drawing.Size(20, 24);
            this.btnCodigoDe.TabIndex = 100;
            this.btnCodigoDe.TabStop = false;
            this.btnCodigoDe.Text = "...";
            this.toolTip1.SetToolTip(this.btnCodigoDe, "do Codigo");
            this.btnCodigoDe.UseVisualStyleBackColor = true;
            this.btnCodigoDe.Click += new System.EventHandler(this.btnCodigoDe_Click);
            // 
            // tbxCodigoDe
            // 
            this.tbxCodigoDe.AccessibleDescription = "Do Código";
            this.tbxCodigoDe.BackColor = System.Drawing.Color.White;
            this.tbxCodigoDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigoDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigoDe.Location = new System.Drawing.Point(100, 19);
            this.tbxCodigoDe.Name = "tbxCodigoDe";
            this.tbxCodigoDe.Size = new System.Drawing.Size(148, 21);
            this.tbxCodigoDe.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxCodigoDe, "Do Codigo");
            this.tbxCodigoDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCodigoDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigoDe.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nome do Material ";
            // 
            // gbxEstoqueManual
            // 
            this.gbxEstoqueManual.Controls.Add(this.label3);
            this.gbxEstoqueManual.Controls.Add(this.tspTool);
            this.gbxEstoqueManual.Controls.Add(this.gbxParametros);
            this.gbxEstoqueManual.Location = new System.Drawing.Point(15, 46);
            this.gbxEstoqueManual.Name = "gbxEstoqueManual";
            this.gbxEstoqueManual.Size = new System.Drawing.Size(984, 465);
            this.gbxEstoqueManual.TabIndex = 0;
            this.gbxEstoqueManual.TabStop = false;
            this.gbxEstoqueManual.Enter += new System.EventHandler(this.gbxEstoqueManual_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LemonChiffon;
            this.label3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(311, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(543, 25);
            this.label3.TabIndex = 28;
            this.label3.Text = "Controle Para Acerto de Codigo NCM Manualmente";
            // 
            // tspTool
            // 
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(19, 418);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(424, 44);
            this.tspTool.TabIndex = 3;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            // 
            // tspSalvar
            // 
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Enabled = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspSalvar.Image")));
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(64, 42);
            this.tspSalvar.Text = "&Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(64, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // frmPecasNCMMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.gbxEstoqueManual);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPecasNCMMan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "34189";
            this.Text = "Controle Acerto NCM";
            this.toolTip1.SetToolTip(this, "Controle Acerto NCM");
            this.Activated += new System.EventHandler(this.frmPecasEstoqueMan_Activated);
            this.Load += new System.EventHandler(this.frmPecasEstoqueMan_Load);
            this.gbxParametros.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxEstoqueManual.ResumeLayout(false);
            this.gbxEstoqueManual.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbxEstoqueManual;
        private System.Windows.Forms.GroupBox gbxParametros;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClassfiscal;
        private System.Windows.Forms.TextBox tbxClassificacaoDe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCodigoDe;
        private System.Windows.Forms.TextBox tbxCodigoDe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxCodigoNomeDe;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.TextBox tbxNomeBusca;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
    }
}