namespace CRCorrea
{
    partial class frmRelatorios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelatorios));
            this.label1 = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspImprimir = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.bwrRel3tipo = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnRelNFVenda = new System.Windows.Forms.Button();
            this.btnRelNFCompra = new System.Windows.Forms.Button();
            this.btnRelPagar = new System.Windows.Forms.Button();
            this.btnRelReceber = new System.Windows.Forms.Button();
            this.btnRelPecas = new System.Windows.Forms.Button();
            this.btnRelPedido = new System.Windows.Forms.Button();
            this.btnRelOrcamento = new System.Windows.Forms.Button();
            this.btnRelCliente = new System.Windows.Forms.Button();
            this.bwrRel6modulo_grupo = new System.ComponentModel.BackgroundWorker();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(288, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(527, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menu de Opções de Relatorios do Sistema por Modulo";
            this.toolTip1.SetToolTip(this.label1, "Relatorios do Sistema");
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.AutoSize = false;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspImprimir,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(399, 411);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(311, 46);
            this.tspTool.TabIndex = 184;
            this.tspTool.TabStop = true;
            this.toolTip1.SetToolTip(this.tspTool, "Botões");
            // 
            // tspImprimir
            // 
            this.tspImprimir.AccessibleDescription = "Imprimir/E-mail";
            this.tspImprimir.AutoSize = false;
            this.tspImprimir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspImprimir.Image = ((System.Drawing.Image)(resources.GetObject("tspImprimir.Image")));
            this.tspImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImprimir.Name = "tspImprimir";
            this.tspImprimir.Size = new System.Drawing.Size(100, 42);
            this.tspImprimir.Text = "Imprimir/E-mail";
            this.tspImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspImprimir.Visible = false;
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Rtornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(62, 42);
            this.tspRetornar.Text = "Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // btnRelNFVenda
            // 
            this.btnRelNFVenda.AccessibleDescription = "Modulo de Notas Fiscais de Vendas (Saidas)";
            this.btnRelNFVenda.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelNFVenda.Location = new System.Drawing.Point(399, 159);
            this.btnRelNFVenda.Name = "btnRelNFVenda";
            this.btnRelNFVenda.Size = new System.Drawing.Size(311, 36);
            this.btnRelNFVenda.TabIndex = 185;
            this.btnRelNFVenda.Text = "Modulo de Notas Fiscais de Vendas (Saidas)";
            this.toolTip1.SetToolTip(this.btnRelNFVenda, "Modulo de Notas Fiscais de Vendas (Saidas)");
            this.btnRelNFVenda.UseVisualStyleBackColor = true;
            this.btnRelNFVenda.UseWaitCursor = true;
            this.btnRelNFVenda.Click += new System.EventHandler(this.btnRelNFVenda_Click);
            // 
            // btnRelNFCompra
            // 
            this.btnRelNFCompra.AccessibleDescription = "Modulo de Notas Fiscais de Compras (Entradas)";
            this.btnRelNFCompra.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelNFCompra.Location = new System.Drawing.Point(399, 117);
            this.btnRelNFCompra.Name = "btnRelNFCompra";
            this.btnRelNFCompra.Size = new System.Drawing.Size(311, 36);
            this.btnRelNFCompra.TabIndex = 186;
            this.btnRelNFCompra.Text = "Modulo de Notas Fiscais de Compras (Entradas)";
            this.toolTip1.SetToolTip(this.btnRelNFCompra, "Modulo de Notas Fiscais de Compras (Entradas)");
            this.btnRelNFCompra.UseVisualStyleBackColor = true;
            this.btnRelNFCompra.Click += new System.EventHandler(this.btnRelNFCompra_Click);
            // 
            // btnRelPagar
            // 
            this.btnRelPagar.AccessibleDescription = "Modulo de Contas a Pagar";
            this.btnRelPagar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelPagar.Location = new System.Drawing.Point(399, 201);
            this.btnRelPagar.Name = "btnRelPagar";
            this.btnRelPagar.Size = new System.Drawing.Size(311, 36);
            this.btnRelPagar.TabIndex = 188;
            this.btnRelPagar.Text = "Modulo de Contas a Pagar";
            this.toolTip1.SetToolTip(this.btnRelPagar, "Modulo de Contas a Pagar");
            this.btnRelPagar.UseVisualStyleBackColor = true;
            this.btnRelPagar.Click += new System.EventHandler(this.btnRelPagar_Click);
            // 
            // btnRelReceber
            // 
            this.btnRelReceber.AccessibleDescription = "Modulo de Contas a Receber";
            this.btnRelReceber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelReceber.Location = new System.Drawing.Point(399, 243);
            this.btnRelReceber.Name = "btnRelReceber";
            this.btnRelReceber.Size = new System.Drawing.Size(311, 36);
            this.btnRelReceber.TabIndex = 187;
            this.btnRelReceber.Text = "Modulo de Contas a Receber";
            this.toolTip1.SetToolTip(this.btnRelReceber, "Modulo de Contas a Receber");
            this.btnRelReceber.UseVisualStyleBackColor = true;
            this.btnRelReceber.Click += new System.EventHandler(this.btnRelReceber_Click);
            // 
            // btnRelPecas
            // 
            this.btnRelPecas.AccessibleDescription = "Modulo de Peças ()";
            this.btnRelPecas.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelPecas.Location = new System.Drawing.Point(399, 75);
            this.btnRelPecas.Name = "btnRelPecas";
            this.btnRelPecas.Size = new System.Drawing.Size(311, 36);
            this.btnRelPecas.TabIndex = 189;
            this.btnRelPecas.Text = "Modulo de Peças ()";
            this.toolTip1.SetToolTip(this.btnRelPecas, "Modulo de Peças ()");
            this.btnRelPecas.UseVisualStyleBackColor = true;
            this.btnRelPecas.Click += new System.EventHandler(this.btnRelPecas_Click);
            // 
            // btnRelPedido
            // 
            this.btnRelPedido.AccessibleDescription = "Modulo de Pedidos de Vendas (PV)";
            this.btnRelPedido.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelPedido.Location = new System.Drawing.Point(399, 284);
            this.btnRelPedido.Name = "btnRelPedido";
            this.btnRelPedido.Size = new System.Drawing.Size(311, 36);
            this.btnRelPedido.TabIndex = 190;
            this.btnRelPedido.Text = "Modulo de Pedidos de Venda";
            this.toolTip1.SetToolTip(this.btnRelPedido, "Modulo de Pedidos de Vendas (PV)");
            this.btnRelPedido.UseVisualStyleBackColor = true;
            this.btnRelPedido.Click += new System.EventHandler(this.btnRelPedido_Click);
            // 
            // btnRelOrcamento
            // 
            this.btnRelOrcamento.AccessibleDescription = "Modulo de Orçamentos";
            this.btnRelOrcamento.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelOrcamento.Location = new System.Drawing.Point(399, 325);
            this.btnRelOrcamento.Name = "btnRelOrcamento";
            this.btnRelOrcamento.Size = new System.Drawing.Size(311, 36);
            this.btnRelOrcamento.TabIndex = 193;
            this.btnRelOrcamento.Text = "Modulo de Orçamentos";
            this.toolTip1.SetToolTip(this.btnRelOrcamento, "Modulo de Orçamentos");
            this.btnRelOrcamento.UseVisualStyleBackColor = true;
            this.btnRelOrcamento.Click += new System.EventHandler(this.btnRelOrcamento_Click);
            // 
            // btnRelCliente
            // 
            this.btnRelCliente.AccessibleDescription = "Modulo de Clientes";
            this.btnRelCliente.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelCliente.Location = new System.Drawing.Point(399, 367);
            this.btnRelCliente.Name = "btnRelCliente";
            this.btnRelCliente.Size = new System.Drawing.Size(311, 36);
            this.btnRelCliente.TabIndex = 194;
            this.btnRelCliente.Text = "Modulo de Clientes ()";
            this.toolTip1.SetToolTip(this.btnRelCliente, "Modulo de Ordens de Serviço (O.S.)");
            this.btnRelCliente.UseVisualStyleBackColor = true;
            this.btnRelCliente.Click += new System.EventHandler(this.btnRelCliente_Click);
            // 
            // frmRelatorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.ControlBox = false;
            this.Controls.Add(this.btnRelCliente);
            this.Controls.Add(this.btnRelOrcamento);
            this.Controls.Add(this.btnRelPedido);
            this.Controls.Add(this.btnRelPecas);
            this.Controls.Add(this.btnRelPagar);
            this.Controls.Add(this.btnRelReceber);
            this.Controls.Add(this.btnRelNFCompra);
            this.Controls.Add(this.btnRelNFVenda);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmRelatorios";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4056";
            this.Text = "Relatórios Por Modulo - Visualização";
            this.toolTip1.SetToolTip(this, "Relatórios Por Modulo - Visualização");
            this.Load += new System.EventHandler(this.frmRelatoriosFerpal_Load);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspImprimir;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.ComponentModel.BackgroundWorker bwrRel3tipo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnRelNFVenda;
        private System.Windows.Forms.Button btnRelNFCompra;
        private System.ComponentModel.BackgroundWorker bwrRel6modulo_grupo;
        private System.Windows.Forms.Button btnRelPagar;
        private System.Windows.Forms.Button btnRelReceber;
        private System.Windows.Forms.Button btnRelPecas;
        private System.Windows.Forms.Button btnRelPedido;
        private System.Windows.Forms.Button btnRelOrcamento;
        private System.Windows.Forms.Button btnRelCliente;
    }
}