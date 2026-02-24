namespace CRCorrea
{
    partial class frmFechamentoCaixa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFechamentoCaixa));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbxFechamentoCaixa = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxTaxaCrescimo = new System.Windows.Forms.TextBox();
            this.tbxTotalMes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvFechamentoCaixa = new System.Windows.Forms.DataGridView();
            this.tbxDataFechamento = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxFechamentoCaixaAno = new System.Windows.Forms.GroupBox();
            this.dgvFechamentoCaixaAno = new System.Windows.Forms.DataGridView();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnRevisaCusto = new System.Windows.Forms.Button();
            this.tbxTotalApurado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxFechamentoCaixa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFechamentoCaixa)).BeginInit();
            this.tspTool.SuspendLayout();
            this.gbxFechamentoCaixaAno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFechamentoCaixaAno)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxFechamentoCaixa
            // 
            this.gbxFechamentoCaixa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbxFechamentoCaixa.Controls.Add(this.label4);
            this.gbxFechamentoCaixa.Controls.Add(this.tbxTotalApurado);
            this.gbxFechamentoCaixa.Controls.Add(this.label3);
            this.gbxFechamentoCaixa.Controls.Add(this.label2);
            this.gbxFechamentoCaixa.Controls.Add(this.tbxTaxaCrescimo);
            this.gbxFechamentoCaixa.Controls.Add(this.tbxTotalMes);
            this.gbxFechamentoCaixa.Controls.Add(this.label1);
            this.gbxFechamentoCaixa.Controls.Add(this.dgvFechamentoCaixa);
            this.gbxFechamentoCaixa.Location = new System.Drawing.Point(4, 4);
            this.gbxFechamentoCaixa.Name = "gbxFechamentoCaixa";
            this.gbxFechamentoCaixa.Size = new System.Drawing.Size(638, 625);
            this.gbxFechamentoCaixa.TabIndex = 0;
            this.gbxFechamentoCaixa.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(467, 577);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Mês Anterior";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(518, 595);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "%";
            // 
            // tbxTaxaCrescimo
            // 
            this.tbxTaxaCrescimo.AccessibleDescription = "Suframa";
            this.tbxTaxaCrescimo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxTaxaCrescimo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbxTaxaCrescimo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTaxaCrescimo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTaxaCrescimo.Location = new System.Drawing.Point(474, 592);
            this.tbxTaxaCrescimo.Name = "tbxTaxaCrescimo";
            this.tbxTaxaCrescimo.Size = new System.Drawing.Size(43, 21);
            this.tbxTaxaCrescimo.TabIndex = 18;
            this.tbxTaxaCrescimo.Tag = "CGC";
            this.tbxTaxaCrescimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxTotalMes
            // 
            this.tbxTotalMes.AccessibleDescription = "Suframa";
            this.tbxTotalMes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxTotalMes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tbxTotalMes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTotalMes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalMes.Location = new System.Drawing.Point(359, 592);
            this.tbxTotalMes.Name = "tbxTotalMes";
            this.tbxTotalMes.Size = new System.Drawing.Size(112, 21);
            this.tbxTotalMes.TabIndex = 9;
            this.tbxTotalMes.Tag = "CGC";
            this.tbxTotalMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(364, 577);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Total  Faturado Mês ";
            // 
            // dgvFechamentoCaixa
            // 
            this.dgvFechamentoCaixa.AccessibleDescription = "Grid Clientes";
            this.dgvFechamentoCaixa.AllowUserToAddRows = false;
            this.dgvFechamentoCaixa.AllowUserToDeleteRows = false;
            this.dgvFechamentoCaixa.AllowUserToOrderColumns = true;
            this.dgvFechamentoCaixa.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvFechamentoCaixa.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFechamentoCaixa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvFechamentoCaixa.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvFechamentoCaixa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFechamentoCaixa.Location = new System.Drawing.Point(8, 15);
            this.dgvFechamentoCaixa.MultiSelect = false;
            this.dgvFechamentoCaixa.Name = "dgvFechamentoCaixa";
            this.dgvFechamentoCaixa.ReadOnly = true;
            this.dgvFechamentoCaixa.RowHeadersVisible = false;
            this.dgvFechamentoCaixa.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvFechamentoCaixa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvFechamentoCaixa.Size = new System.Drawing.Size(624, 559);
            this.dgvFechamentoCaixa.TabIndex = 8;
            this.dgvFechamentoCaixa.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFechamentoCaixa_CellMouseDoubleClick);
            this.dgvFechamentoCaixa.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvFechamentoCaixa_MouseDoubleClick);
            // 
            // tbxDataFechamento
            // 
            this.tbxDataFechamento.AccessibleDescription = "Suframa";
            this.tbxDataFechamento.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxDataFechamento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDataFechamento.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataFechamento.Location = new System.Drawing.Point(783, 16);
            this.tbxDataFechamento.Name = "tbxDataFechamento";
            this.tbxDataFechamento.Size = new System.Drawing.Size(93, 21);
            this.tbxDataFechamento.TabIndex = 0;
            this.tbxDataFechamento.Tag = "CGC";
            this.tbxDataFechamento.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDataFechamento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxDataFechamento.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label46
            // 
            this.label46.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(661, 19);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(118, 13);
            this.label46.TabIndex = 4;
            this.label46.Text = "Fechar o Caixa do Dia: ";
            // 
            // btnFechar
            // 
            this.btnFechar.AccessibleDescription = "Fechar Caixa";
            this.btnFechar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFechar.Image = global::CRCorrea.Properties.Resources._327;
            this.btnFechar.Location = new System.Drawing.Point(882, 9);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(43, 38);
            this.btnFechar.TabIndex = 1;
            this.btnFechar.TabStop = false;
            this.ToolTip.SetToolTip(this.btnFechar, "Fechar Caixa do Dia");
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspImprimir,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(656, 463);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(164, 45);
            this.tspTool.TabIndex = 16;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            // 
            // tspImprimir
            // 
            this.tspImprimir.AccessibleDescription = "Imprimir Pedido";
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimir.Image")));
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(66, 42);
            this.tspImprimir.Text = "&Imprimir";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.ToolTipText = "Imprimir Pedido";
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
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // gbxFechamentoCaixaAno
            // 
            this.gbxFechamentoCaixaAno.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbxFechamentoCaixaAno.Controls.Add(this.dgvFechamentoCaixaAno);
            this.gbxFechamentoCaixaAno.Location = new System.Drawing.Point(648, 53);
            this.gbxFechamentoCaixaAno.Name = "gbxFechamentoCaixaAno";
            this.gbxFechamentoCaixaAno.Size = new System.Drawing.Size(340, 393);
            this.gbxFechamentoCaixaAno.TabIndex = 17;
            this.gbxFechamentoCaixaAno.TabStop = false;
            // 
            // dgvFechamentoCaixaAno
            // 
            this.dgvFechamentoCaixaAno.AccessibleDescription = "Grid Clientes";
            this.dgvFechamentoCaixaAno.AllowUserToAddRows = false;
            this.dgvFechamentoCaixaAno.AllowUserToDeleteRows = false;
            this.dgvFechamentoCaixaAno.AllowUserToOrderColumns = true;
            this.dgvFechamentoCaixaAno.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.dgvFechamentoCaixaAno.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFechamentoCaixaAno.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvFechamentoCaixaAno.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvFechamentoCaixaAno.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFechamentoCaixaAno.Location = new System.Drawing.Point(8, 19);
            this.dgvFechamentoCaixaAno.MultiSelect = false;
            this.dgvFechamentoCaixaAno.Name = "dgvFechamentoCaixaAno";
            this.dgvFechamentoCaixaAno.ReadOnly = true;
            this.dgvFechamentoCaixaAno.RowHeadersVisible = false;
            this.dgvFechamentoCaixaAno.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvFechamentoCaixaAno.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvFechamentoCaixaAno.Size = new System.Drawing.Size(326, 357);
            this.dgvFechamentoCaixaAno.TabIndex = 8;
            // 
            // btnRevisaCusto
            // 
            this.btnRevisaCusto.AccessibleDescription = "Fechar Caixa";
            this.btnRevisaCusto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRevisaCusto.Image = ((System.Drawing.Image)(resources.GetObject("btnRevisaCusto.Image")));
            this.btnRevisaCusto.Location = new System.Drawing.Point(931, 9);
            this.btnRevisaCusto.Name = "btnRevisaCusto";
            this.btnRevisaCusto.Size = new System.Drawing.Size(43, 38);
            this.btnRevisaCusto.TabIndex = 18;
            this.btnRevisaCusto.TabStop = false;
            this.ToolTip.SetToolTip(this.btnRevisaCusto, "Revisar Custos 0(zero)");
            this.btnRevisaCusto.UseVisualStyleBackColor = true;
            this.btnRevisaCusto.Click += new System.EventHandler(this.btnRevisaCusto_Click);
            // 
            // tbxTotalApurado
            // 
            this.tbxTotalApurado.AccessibleDescription = "Suframa";
            this.tbxTotalApurado.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxTotalApurado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tbxTotalApurado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTotalApurado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalApurado.Location = new System.Drawing.Point(539, 592);
            this.tbxTotalApurado.Name = "tbxTotalApurado";
            this.tbxTotalApurado.Size = new System.Drawing.Size(92, 21);
            this.tbxTotalApurado.TabIndex = 21;
            this.tbxTotalApurado.Tag = "CGC";
            this.tbxTotalApurado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxTotalApurado.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.DarkGray;
            this.label4.Location = new System.Drawing.Point(536, 577);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Total Apurado Mês ";
            // 
            // frmFechamentoCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 646);
            this.Controls.Add(this.btnRevisaCusto);
            this.Controls.Add(this.gbxFechamentoCaixaAno);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.tbxDataFechamento);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.gbxFechamentoCaixa);
            this.Name = "frmFechamentoCaixa";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmFechamentoCaixa";
            this.Activated += new System.EventHandler(this.frmFechamentoCaixa_Activated);
            this.Load += new System.EventHandler(this.frmFechamentoCaixa_Load);
            this.gbxFechamentoCaixa.ResumeLayout(false);
            this.gbxFechamentoCaixa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFechamentoCaixa)).EndInit();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.gbxFechamentoCaixaAno.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFechamentoCaixaAno)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxFechamentoCaixa;
        private System.Windows.Forms.DataGridView dgvFechamentoCaixa;
        private System.Windows.Forms.TextBox tbxDataFechamento;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.TextBox tbxTotalMes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxFechamentoCaixaAno;
        private System.Windows.Forms.DataGridView dgvFechamentoCaixaAno;
        private System.Windows.Forms.TextBox tbxTaxaCrescimo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Button btnRevisaCusto;
        private System.Windows.Forms.TextBox tbxTotalApurado;
        private System.Windows.Forms.Label label4;
    }
}