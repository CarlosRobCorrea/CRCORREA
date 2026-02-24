namespace CRCorrea
{
    partial class frmPecasAcertaSaldo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPecasAcertaSaldo));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxParametros = new System.Windows.Forms.GroupBox();
            this.pbxFoto = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxQtdeSaldoAtual = new System.Windows.Forms.TextBox();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.tbxQtdeSaldo = new System.Windows.Forms.TextBox();
            this.tbxCodigoNomeDe = new System.Windows.Forms.TextBox();
            this.btnCodigoDe = new System.Windows.Forms.Button();
            this.tbxCodigoDe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxEstoqueManual = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxParametros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFoto)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbxEstoqueManual.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxParametros
            // 
            this.gbxParametros.AccessibleDescription = "Parametros de Filtragem do Acerto";
            this.gbxParametros.Controls.Add(this.pbxFoto);
            this.gbxParametros.Controls.Add(this.groupBox1);
            this.gbxParametros.Location = new System.Drawing.Point(14, 48);
            this.gbxParametros.Name = "gbxParametros";
            this.gbxParametros.Size = new System.Drawing.Size(957, 163);
            this.gbxParametros.TabIndex = 1;
            this.gbxParametros.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxParametros, "Parametros de Filtragem do Acerto");
            // 
            // pbxFoto
            // 
            this.pbxFoto.AccessibleDescription = "Foto Produto";
            this.pbxFoto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbxFoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxFoto.Location = new System.Drawing.Point(746, 26);
            this.pbxFoto.Name = "pbxFoto";
            this.pbxFoto.Size = new System.Drawing.Size(151, 116);
            this.pbxFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxFoto.TabIndex = 172;
            this.pbxFoto.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Parametros dos Itens";
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxQtdeSaldoAtual);
            this.groupBox1.Controls.Add(this.btnExecutar);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.tbxQtdeSaldo);
            this.groupBox1.Controls.Add(this.tbxCodigoNomeDe);
            this.groupBox1.Controls.Add(this.btnCodigoDe);
            this.groupBox1.Controls.Add(this.tbxCodigoDe);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(699, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametros dos Itens";
            this.toolTip1.SetToolTip(this.groupBox1, "Grupo Parametros dos Itens");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 105;
            this.label2.Text = "Saldo Realinhado :";
            // 
            // tbxQtdeSaldoAtual
            // 
            this.tbxQtdeSaldoAtual.AccessibleDescription = "Saldo Real";
            this.tbxQtdeSaldoAtual.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxQtdeSaldoAtual.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxQtdeSaldoAtual.Location = new System.Drawing.Point(214, 93);
            this.tbxQtdeSaldoAtual.Name = "tbxQtdeSaldoAtual";
            this.tbxQtdeSaldoAtual.ReadOnly = true;
            this.tbxQtdeSaldoAtual.Size = new System.Drawing.Size(108, 21);
            this.tbxQtdeSaldoAtual.TabIndex = 104;
            this.tbxQtdeSaldoAtual.TabStop = false;
            this.tbxQtdeSaldoAtual.Tag = "N2";
            this.tbxQtdeSaldoAtual.Text = "0,00";
            this.tbxQtdeSaldoAtual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnExecutar
            // 
            this.btnExecutar.AccessibleDescription = "Executar conforme parametros";
            this.btnExecutar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnExecutar.Image")));
            this.btnExecutar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutar.Location = new System.Drawing.Point(328, 64);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(238, 24);
            this.btnExecutar.TabIndex = 103;
            this.btnExecutar.TabStop = false;
            this.btnExecutar.Text = "Executar Conforme Parametros";
            this.toolTip1.SetToolTip(this.btnExecutar, "Executar conforme parametros");
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(132, 68);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(77, 13);
            this.label25.TabIndex = 102;
            this.label25.Text = "Saldo Atual :";
            // 
            // tbxQtdeSaldo
            // 
            this.tbxQtdeSaldo.AccessibleDescription = "Saldo Real";
            this.tbxQtdeSaldo.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxQtdeSaldo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxQtdeSaldo.Location = new System.Drawing.Point(214, 64);
            this.tbxQtdeSaldo.Name = "tbxQtdeSaldo";
            this.tbxQtdeSaldo.ReadOnly = true;
            this.tbxQtdeSaldo.Size = new System.Drawing.Size(108, 21);
            this.tbxQtdeSaldo.TabIndex = 101;
            this.tbxQtdeSaldo.TabStop = false;
            this.tbxQtdeSaldo.Tag = "N2";
            this.tbxQtdeSaldo.Text = "0,00";
            this.tbxQtdeSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxCodigoNomeDe
            // 
            this.tbxCodigoNomeDe.AccessibleDescription = "Do Código";
            this.tbxCodigoNomeDe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbxCodigoNomeDe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxCodigoNomeDe.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxCodigoNomeDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigoNomeDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigoNomeDe.Location = new System.Drawing.Point(68, 40);
            this.tbxCodigoNomeDe.Name = "tbxCodigoNomeDe";
            this.tbxCodigoNomeDe.ReadOnly = true;
            this.tbxCodigoNomeDe.Size = new System.Drawing.Size(617, 18);
            this.tbxCodigoNomeDe.TabIndex = 1;
            this.tbxCodigoNomeDe.TabStop = false;
            // 
            // btnCodigoDe
            // 
            this.btnCodigoDe.AccessibleDescription = "Código de";
            this.btnCodigoDe.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCodigoDe.Image = ((System.Drawing.Image)(resources.GetObject("btnCodigoDe.Image")));
            this.btnCodigoDe.Location = new System.Drawing.Point(302, 14);
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
            this.tbxCodigoDe.Location = new System.Drawing.Point(68, 16);
            this.tbxCodigoDe.Name = "tbxCodigoDe";
            this.tbxCodigoDe.Size = new System.Drawing.Size(233, 21);
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
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Do Codigo";
            // 
            // gbxEstoqueManual
            // 
            this.gbxEstoqueManual.Controls.Add(this.label3);
            this.gbxEstoqueManual.Controls.Add(this.tspTool);
            this.gbxEstoqueManual.Controls.Add(this.gbxParametros);
            this.gbxEstoqueManual.Location = new System.Drawing.Point(15, 46);
            this.gbxEstoqueManual.Name = "gbxEstoqueManual";
            this.gbxEstoqueManual.Size = new System.Drawing.Size(984, 508);
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
            this.label3.Size = new System.Drawing.Size(434, 25);
            this.label3.TabIndex = 28;
            this.label3.Text = "Controle Para Acerto de Estoque Manual";
            // 
            // tspTool
            // 
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            // frmPecasAcertaSaldo
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
            this.Name = "frmPecasAcertaSaldo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "34189";
            this.Text = "Peças Estoque Acerto Manual - Registrando";
            this.toolTip1.SetToolTip(this, "Peças Estoque Acerto Manual - Registrando");
            this.Activated += new System.EventHandler(this.frmPecasEstoqueMan_Activated);
            this.Load += new System.EventHandler(this.frmPecasEstoqueMan_Load);
            this.gbxParametros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxFoto)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCodigoDe;
        private System.Windows.Forms.TextBox tbxCodigoDe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxCodigoNomeDe;
        private System.Windows.Forms.PictureBox pbxFoto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxQtdeSaldoAtual;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbxQtdeSaldo;
    }
}