namespace CRCorrea
{
    partial class frmImpNFCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImpNFCompra));
            this.gbxPeriodo = new System.Windows.Forms.GroupBox();
            this.tbxFornecedor = new System.Windows.Forms.TextBox();
            this.btnIdNFCompra = new System.Windows.Forms.Button();
            this.tbxNFImportar = new System.Windows.Forms.TextBox();
            this.btnRetornar = new System.Windows.Forms.Button();
            this.gbxNovaNota = new System.Windows.Forms.GroupBox();
            this.lblTransferencia = new System.Windows.Forms.Label();
            this.btnImportar = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.tbxTotalNotaFiscal = new System.Windows.Forms.TextBox();
            this.tbxData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxNumero = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxPeriodo.SuspendLayout();
            this.gbxNovaNota.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxPeriodo
            // 
            this.gbxPeriodo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxPeriodo.Controls.Add(this.tbxFornecedor);
            this.gbxPeriodo.Controls.Add(this.btnIdNFCompra);
            this.gbxPeriodo.Controls.Add(this.tbxNFImportar);
            this.gbxPeriodo.Location = new System.Drawing.Point(12, 12);
            this.gbxPeriodo.Name = "gbxPeriodo";
            this.gbxPeriodo.Size = new System.Drawing.Size(495, 57);
            this.gbxPeriodo.TabIndex = 1;
            this.gbxPeriodo.TabStop = false;
            this.gbxPeriodo.Text = "Período";
            // 
            // tbxFornecedor
            // 
            this.tbxFornecedor.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxFornecedor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxFornecedor.Location = new System.Drawing.Point(183, 19);
            this.tbxFornecedor.Name = "tbxFornecedor";
            this.tbxFornecedor.ReadOnly = true;
            this.tbxFornecedor.Size = new System.Drawing.Size(292, 21);
            this.tbxFornecedor.TabIndex = 4;
            this.tbxFornecedor.TabStop = false;
            // 
            // btnIdNFCompra
            // 
            this.btnIdNFCompra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIdNFCompra.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIdNFCompra.Image = ((System.Drawing.Image)(resources.GetObject("btnIdNFCompra.Image")));
            this.btnIdNFCompra.Location = new System.Drawing.Point(156, 19);
            this.btnIdNFCompra.Name = "btnIdNFCompra";
            this.btnIdNFCompra.Size = new System.Drawing.Size(21, 21);
            this.btnIdNFCompra.TabIndex = 3;
            this.btnIdNFCompra.TabStop = false;
            this.btnIdNFCompra.UseVisualStyleBackColor = true;
            this.btnIdNFCompra.Click += new System.EventHandler(this.btnIdNFCompra_Click);
            // 
            // tbxNFImportar
            // 
            this.tbxNFImportar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNFImportar.Location = new System.Drawing.Point(16, 19);
            this.tbxNFImportar.Name = "tbxNFImportar";
            this.tbxNFImportar.Size = new System.Drawing.Size(134, 21);
            this.tbxNFImportar.TabIndex = 2;
            this.tbxNFImportar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRetornar
            // 
            this.btnRetornar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnRetornar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetornar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetornar.Image = global::CRCorrea.Properties.Resources.Retornar;
            this.btnRetornar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRetornar.Location = new System.Drawing.Point(574, 160);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(126, 50);
            this.btnRetornar.TabIndex = 4;
            this.btnRetornar.TabStop = false;
            this.btnRetornar.Text = "Retornar";
            this.btnRetornar.UseVisualStyleBackColor = false;
            this.btnRetornar.Click += new System.EventHandler(this.btnRetornar_Click);
            // 
            // gbxNovaNota
            // 
            this.gbxNovaNota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxNovaNota.Controls.Add(this.lblTransferencia);
            this.gbxNovaNota.Controls.Add(this.btnImportar);
            this.gbxNovaNota.Controls.Add(this.label31);
            this.gbxNovaNota.Controls.Add(this.tbxTotalNotaFiscal);
            this.gbxNovaNota.Controls.Add(this.tbxData);
            this.gbxNovaNota.Controls.Add(this.label2);
            this.gbxNovaNota.Controls.Add(this.tbxNumero);
            this.gbxNovaNota.Controls.Add(this.label1);
            this.gbxNovaNota.Location = new System.Drawing.Point(12, 75);
            this.gbxNovaNota.Name = "gbxNovaNota";
            this.gbxNovaNota.Size = new System.Drawing.Size(495, 173);
            this.gbxNovaNota.TabIndex = 5;
            this.gbxNovaNota.TabStop = false;
            this.gbxNovaNota.Text = "Nota Fiscal Destino";
            this.gbxNovaNota.Visible = false;
            // 
            // lblTransferencia
            // 
            this.lblTransferencia.AutoSize = true;
            this.lblTransferencia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblTransferencia.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransferencia.Location = new System.Drawing.Point(103, 131);
            this.lblTransferencia.Name = "lblTransferencia";
            this.lblTransferencia.Size = new System.Drawing.Size(263, 25);
            this.lblTransferencia.TabIndex = 115;
            this.lblTransferencia.Text = "Transferencia Concluida";
            this.lblTransferencia.Visible = false;
            // 
            // btnImportar
            // 
            this.btnImportar.BackColor = System.Drawing.Color.DarkOrange;
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Image = global::CRCorrea.Properties.Resources.Importar;
            this.btnImportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportar.Location = new System.Drawing.Point(307, 37);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(126, 50);
            this.btnImportar.TabIndex = 2;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = false;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(45, 79);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(77, 18);
            this.label31.TabIndex = 113;
            this.label31.Text = "Total Nota";
            // 
            // tbxTotalNotaFiscal
            // 
            this.tbxTotalNotaFiscal.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTotalNotaFiscal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTotalNotaFiscal.Location = new System.Drawing.Point(128, 80);
            this.tbxTotalNotaFiscal.Name = "tbxTotalNotaFiscal";
            this.tbxTotalNotaFiscal.ReadOnly = true;
            this.tbxTotalNotaFiscal.Size = new System.Drawing.Size(99, 21);
            this.tbxTotalNotaFiscal.TabIndex = 112;
            this.tbxTotalNotaFiscal.TabStop = false;
            this.tbxTotalNotaFiscal.Tag = "N2";
            this.tbxTotalNotaFiscal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxData
            // 
            this.tbxData.BackColor = System.Drawing.Color.White;
            this.tbxData.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxData.Location = new System.Drawing.Point(128, 53);
            this.tbxData.Name = "tbxData";
            this.tbxData.Size = new System.Drawing.Size(64, 21);
            this.tbxData.TabIndex = 1;
            this.tbxData.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownData);
            this.tbxData.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Emissão:";
            // 
            // tbxNumero
            // 
            this.tbxNumero.BackColor = System.Drawing.Color.White;
            this.tbxNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNumero.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNumero.Location = new System.Drawing.Point(128, 27);
            this.tbxNumero.Name = "tbxNumero";
            this.tbxNumero.Size = new System.Drawing.Size(93, 21);
            this.tbxNumero.TabIndex = 0;
            this.tbxNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxNumero.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNumero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNumero.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Nº Nota / Documento";
            // 
            // frmImpNFCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbxNovaNota);
            this.Controls.Add(this.btnRetornar);
            this.Controls.Add(this.gbxPeriodo);
            this.Name = "frmImpNFCompra";
            this.Text = "Importar Nota Fiscal de Entrada";
            this.Activated += new System.EventHandler(this.frmImpNFCompra_Activated);
            this.Load += new System.EventHandler(this.frmImpNFCompra_Load);
            this.gbxPeriodo.ResumeLayout(false);
            this.gbxPeriodo.PerformLayout();
            this.gbxNovaNota.ResumeLayout(false);
            this.gbxNovaNota.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPeriodo;
        private System.Windows.Forms.TextBox tbxNFImportar;
        private System.Windows.Forms.Button btnIdNFCompra;
        private System.Windows.Forms.TextBox tbxFornecedor;
        private System.Windows.Forms.Button btnRetornar;
        private System.Windows.Forms.GroupBox gbxNovaNota;
        private System.Windows.Forms.TextBox tbxData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxNumero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox tbxTotalNotaFiscal;
        private System.Windows.Forms.Label lblTransferencia;
    }
}