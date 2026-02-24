namespace CRCorrea
{ 
    partial class frmImportarFebraban
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportarFebraban));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dadosOrigemProcurar = new System.Windows.Forms.Button();
            this.tbxArquivoOrigem = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCaptura = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbxCadastro = new System.Windows.Forms.ComboBox();
            this.btnTransfere = new System.Windows.Forms.Button();
            this.pbr = new System.Windows.Forms.ProgressBar();
            this.lbxOrigem = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbxDestino = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspOrdem = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvAux = new System.Windows.Forms.DataGridView();
            this.lbxDefeitos = new System.Windows.Forms.ListBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tbxFalta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxTransferiu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxRegistros = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAux)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dadosOrigemProcurar);
            this.groupBox1.Controls.Add(this.tbxArquivoOrigem);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Localizar Arquivo texto dentro do servidor";
            // 
            // dadosOrigemProcurar
            // 
            this.dadosOrigemProcurar.Location = new System.Drawing.Point(494, 18);
            this.dadosOrigemProcurar.Name = "dadosOrigemProcurar";
            this.dadosOrigemProcurar.Size = new System.Drawing.Size(27, 23);
            this.dadosOrigemProcurar.TabIndex = 5;
            this.dadosOrigemProcurar.Text = "...";
            this.dadosOrigemProcurar.UseVisualStyleBackColor = true;
            this.dadosOrigemProcurar.Click += new System.EventHandler(this.dadosOrigemProcurar_Click);
            // 
            // tbxArquivoOrigem
            // 
            this.tbxArquivoOrigem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxArquivoOrigem.Location = new System.Drawing.Point(6, 20);
            this.tbxArquivoOrigem.Name = "tbxArquivoOrigem";
            this.tbxArquivoOrigem.Size = new System.Drawing.Size(483, 21);
            this.tbxArquivoOrigem.TabIndex = 4;
            this.tbxArquivoOrigem.Text = "\\\\";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCaptura);
            this.groupBox2.Location = new System.Drawing.Point(539, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnCaptura
            // 
            this.btnCaptura.Location = new System.Drawing.Point(6, 18);
            this.btnCaptura.Name = "btnCaptura";
            this.btnCaptura.Size = new System.Drawing.Size(174, 23);
            this.btnCaptura.TabIndex = 5;
            this.btnCaptura.Text = "Capturar Arquivo";
            this.btnCaptura.UseVisualStyleBackColor = true;
            this.btnCaptura.Click += new System.EventHandler(this.btnCaptura_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbxCadastro);
            this.groupBox4.Controls.Add(this.btnTransfere);
            this.groupBox4.Controls.Add(this.pbr);
            this.groupBox4.Controls.Add(this.lbxOrigem);
            this.groupBox4.Location = new System.Drawing.Point(12, 69);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(996, 314);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Dados Localizados dentro do Arquivo Texto";
            // 
            // cbxCadastro
            // 
            this.cbxCadastro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCadastro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCadastro.FormattingEnabled = true;
            this.cbxCadastro.Items.AddRange(new object[] {
            "00. Não Indicado",
            "01. Aceite",
            "02. Especie",
            "03. Estado",
            "04. Impressão",
            "05. Modalidade"});
            this.cbxCadastro.Location = new System.Drawing.Point(6, 287);
            this.cbxCadastro.Name = "cbxCadastro";
            this.cbxCadastro.Size = new System.Drawing.Size(399, 21);
            this.cbxCadastro.TabIndex = 7;
            // 
            // btnTransfere
            // 
            this.btnTransfere.Location = new System.Drawing.Point(424, 288);
            this.btnTransfere.Name = "btnTransfere";
            this.btnTransfere.Size = new System.Drawing.Size(174, 23);
            this.btnTransfere.TabIndex = 6;
            this.btnTransfere.Text = "Transferir para a Base";
            this.btnTransfere.UseVisualStyleBackColor = true;
            this.btnTransfere.Visible = false;
            this.btnTransfere.Click += new System.EventHandler(this.btnTransfere_Click);
            // 
            // pbr
            // 
            this.pbr.Location = new System.Drawing.Point(249, 113);
            this.pbr.Name = "pbr";
            this.pbr.Size = new System.Drawing.Size(437, 29);
            this.pbr.TabIndex = 5;
            this.pbr.Visible = false;
            // 
            // lbxOrigem
            // 
            this.lbxOrigem.FormattingEnabled = true;
            this.lbxOrigem.Location = new System.Drawing.Point(6, 20);
            this.lbxOrigem.Name = "lbxOrigem";
            this.lbxOrigem.Size = new System.Drawing.Size(980, 264);
            this.lbxOrigem.TabIndex = 4;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbxDestino);
            this.groupBox5.Location = new System.Drawing.Point(12, 389);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(542, 222);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Dados sendo Transferidos";
            // 
            // lbxDestino
            // 
            this.lbxDestino.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxDestino.FormattingEnabled = true;
            this.lbxDestino.ItemHeight = 11;
            this.lbxDestino.Location = new System.Drawing.Point(6, 20);
            this.lbxDestino.Name = "lbxDestino";
            this.lbxDestino.Size = new System.Drawing.Size(529, 180);
            this.lbxDestino.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(315, 617);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(232, 59);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Separação ( | ) ponto virgula\r\nInicia a linha sem (|) \r\nSe não tem informação naq" +
    "uela campo colocar entre ;;\r\n";
            // 
            // tspTool
            // 
            this.tspTool.AllowMerge = false;
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspAlterar,
            this.tspImprimir,
            this.tspOrdem,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(12, 614);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tspTool.Size = new System.Drawing.Size(300, 46);
            this.tspTool.TabIndex = 7;
            // 
            // tspAlterar
            // 
            this.tspAlterar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAlterar.Image = ((System.Drawing.Image)(resources.GetObject("tspAlterar.Image")));
            this.tspAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAlterar.Name = "tspAlterar";
            this.tspAlterar.Size = new System.Drawing.Size(53, 42);
            this.tspAlterar.Text = "Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspImprimir
            // 
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimir.Image")));
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(62, 42);
            this.tspImprimir.Text = "Imprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspOrdem
            // 
            this.tspOrdem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspOrdem.Image = ((System.Drawing.Image)(resources.GetObject("tspOrdem.Image")));
            this.tspOrdem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspOrdem.Name = "tspOrdem";
            this.tspOrdem.Size = new System.Drawing.Size(51, 42);
            this.tspOrdem.Text = "Ordem";
            this.tspOrdem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspOrdem.Click += new System.EventHandler(this.tspOrdem_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvAux);
            this.groupBox6.Controls.Add(this.lbxDefeitos);
            this.groupBox6.Location = new System.Drawing.Point(558, 389);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(449, 220);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Dados não Transferidos por motivos:";
            // 
            // dgvAux
            // 
            this.dgvAux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAux.Location = new System.Drawing.Point(56, 169);
            this.dgvAux.Name = "dgvAux";
            this.dgvAux.Size = new System.Drawing.Size(84, 30);
            this.dgvAux.TabIndex = 5;
            this.dgvAux.Visible = false;
            // 
            // lbxDefeitos
            // 
            this.lbxDefeitos.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxDefeitos.FormattingEnabled = true;
            this.lbxDefeitos.ItemHeight = 11;
            this.lbxDefeitos.Location = new System.Drawing.Point(6, 20);
            this.lbxDefeitos.Name = "lbxDefeitos";
            this.lbxDefeitos.Size = new System.Drawing.Size(434, 180);
            this.lbxDefeitos.TabIndex = 4;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tbxFalta);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.tbxTransferiu);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.tbxRegistros);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Location = new System.Drawing.Point(558, 612);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(449, 53);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Resumo em Quantidade de Registros";
            // 
            // tbxFalta
            // 
            this.tbxFalta.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxFalta.Location = new System.Drawing.Point(355, 20);
            this.tbxFalta.Name = "tbxFalta";
            this.tbxFalta.Size = new System.Drawing.Size(85, 21);
            this.tbxFalta.TabIndex = 5;
            this.tbxFalta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(321, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Falta";
            // 
            // tbxTransferiu
            // 
            this.tbxTransferiu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTransferiu.Location = new System.Drawing.Point(205, 20);
            this.tbxTransferiu.Name = "tbxTransferiu";
            this.tbxTransferiu.Size = new System.Drawing.Size(85, 21);
            this.tbxTransferiu.TabIndex = 3;
            this.tbxTransferiu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Transferiu";
            // 
            // tbxRegistros
            // 
            this.tbxRegistros.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxRegistros.Location = new System.Drawing.Point(55, 20);
            this.tbxRegistros.Name = "tbxRegistros";
            this.tbxRegistros.Size = new System.Drawing.Size(85, 21);
            this.tbxRegistros.TabIndex = 1;
            this.tbxRegistros.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Possui ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(816, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 39);
            this.label1.TabIndex = 11;
            this.label1.Text = "Importar das Tabelas Txt \r\nos Dados de Bancos para \r\n    Cobrança Febraban";
            // 
            // frmImportarFebraban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmImportarFebraban";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Importar Dados";
            this.Load += new System.EventHandler(this.frmImportarFebraban_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAux)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button dadosOrigemProcurar;
        private System.Windows.Forms.TextBox tbxArquivoOrigem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCaptura;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lbxDestino;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspOrdem;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListBox lbxDefeitos;
        private System.Windows.Forms.ProgressBar pbr;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxFalta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxTransferiu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxRegistros;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTransfere;
        private System.Windows.Forms.ListBox lbxOrigem;
        private System.Windows.Forms.ComboBox cbxCadastro;
        private System.Windows.Forms.DataGridView dgvAux;
        private System.Windows.Forms.Label label1;
    }
}