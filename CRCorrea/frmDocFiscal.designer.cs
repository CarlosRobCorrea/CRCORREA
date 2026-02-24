namespace CRCorrea
{
    partial class frmDocFiscal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocFiscal));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbxModelo = new System.Windows.Forms.TextBox();
            this.gbxDocFiscal = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxAtivo = new System.Windows.Forms.ComboBox();
            this.tbxCogNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxSerie = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tspColaborador = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxDocFiscal.SuspendLayout();
            this.tspColaborador.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxModelo
            // 
            this.tbxModelo.AccessibleDescription = "Modelo Danfe";
            this.tbxModelo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxModelo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxModelo.Location = new System.Drawing.Point(144, 149);
            this.tbxModelo.MaxLength = 2;
            this.tbxModelo.Name = "tbxModelo";
            this.tbxModelo.Size = new System.Drawing.Size(41, 21);
            this.tbxModelo.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tbxModelo, "Modelo Danfe");
            this.tbxModelo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxModelo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxModelo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxDocFiscal
            // 
            this.gbxDocFiscal.AccessibleDescription = "Documento Fiscal";
            this.gbxDocFiscal.Controls.Add(this.tbxModelo);
            this.gbxDocFiscal.Controls.Add(this.label5);
            this.gbxDocFiscal.Controls.Add(this.label14);
            this.gbxDocFiscal.Controls.Add(this.cbxAtivo);
            this.gbxDocFiscal.Controls.Add(this.tbxCogNome);
            this.gbxDocFiscal.Controls.Add(this.label3);
            this.gbxDocFiscal.Controls.Add(this.tbxSerie);
            this.gbxDocFiscal.Controls.Add(this.label2);
            this.gbxDocFiscal.Controls.Add(this.tbxNome);
            this.gbxDocFiscal.Controls.Add(this.label4);
            this.gbxDocFiscal.Controls.Add(this.tbxCodigo);
            this.gbxDocFiscal.Controls.Add(this.label1);
            this.gbxDocFiscal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDocFiscal.ForeColor = System.Drawing.Color.DarkRed;
            this.gbxDocFiscal.Location = new System.Drawing.Point(17, 42);
            this.gbxDocFiscal.Name = "gbxDocFiscal";
            this.gbxDocFiscal.Size = new System.Drawing.Size(640, 218);
            this.gbxDocFiscal.TabIndex = 1;
            this.gbxDocFiscal.TabStop = false;
            this.gbxDocFiscal.Text = "Documento Fiscal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(130, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 106;
            this.label5.Text = "Codigo Danfe";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(66, 133);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 104;
            this.label14.Text = "Ativo";
            // 
            // cbxAtivo
            // 
            this.cbxAtivo.AccessibleDescription = "Ativo";
            this.cbxAtivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAtivo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAtivo.FormattingEnabled = true;
            this.cbxAtivo.Items.AddRange(new object[] {
            "S",
            "N"});
            this.cbxAtivo.Location = new System.Drawing.Point(69, 149);
            this.cbxAtivo.Name = "cbxAtivo";
            this.cbxAtivo.Size = new System.Drawing.Size(46, 21);
            this.cbxAtivo.TabIndex = 4;
            this.cbxAtivo.Enter += new System.EventHandler(this.ControlEnter);
            this.cbxAtivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.cbxAtivo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxCogNome
            // 
            this.tbxCogNome.AccessibleDescription = "Cognome";
            this.tbxCogNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCogNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCogNome.Location = new System.Drawing.Point(89, 42);
            this.tbxCogNome.MaxLength = 7;
            this.tbxCogNome.Name = "tbxCogNome";
            this.tbxCogNome.Size = new System.Drawing.Size(85, 21);
            this.tbxCogNome.TabIndex = 1;
            this.tbxCogNome.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCogNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCogNome.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(89, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 89;
            this.label3.Text = "Sigla Documento";
            // 
            // tbxSerie
            // 
            this.tbxSerie.AccessibleDescription = "Série";
            this.tbxSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxSerie.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSerie.Location = new System.Drawing.Point(10, 149);
            this.tbxSerie.MaxLength = 3;
            this.tbxSerie.Name = "tbxSerie";
            this.tbxSerie.Size = new System.Drawing.Size(41, 21);
            this.tbxSerie.TabIndex = 3;
            this.tbxSerie.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxSerie.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(6, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "Série";
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Nome";
            this.tbxNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(10, 93);
            this.tbxNome.MaxLength = 100;
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(612, 21);
            this.tbxNome.TabIndex = 2;
            this.tbxNome.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(7, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "Nome";
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.AccessibleDescription = "Codigo";
            this.tbxCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Location = new System.Drawing.Point(10, 42);
            this.tbxCodigo.MaxLength = 3;
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(60, 21);
            this.tbxCodigo.TabIndex = 0;
            this.tbxCodigo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo";
            // 
            // tspColaborador
            // 
            this.tspColaborador.AccessibleDescription = "Barra de Opções Principal do Colaborador";
            this.tspColaborador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tspColaborador.AutoSize = false;
            this.tspColaborador.BackColor = System.Drawing.Color.DarkGray;
            this.tspColaborador.Dock = System.Windows.Forms.DockStyle.None;
            this.tspColaborador.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspColaborador.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspColaborador.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspRetornar});
            this.tspColaborador.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspColaborador.Location = new System.Drawing.Point(17, 277);
            this.tspColaborador.Name = "tspColaborador";
            this.tspColaborador.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspColaborador.Size = new System.Drawing.Size(163, 45);
            this.tspColaborador.TabIndex = 15;
            this.tspColaborador.TabStop = true;
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspSalvar.Image")));
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(58, 42);
            this.tspSalvar.Text = "&Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Reotornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(58, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // frmDocFiscal
            // 
            this.AccessibleDescription = "Documento Fiscal";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 367);
            this.Controls.Add(this.tspColaborador);
            this.Controls.Add(this.gbxDocFiscal);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDocFiscal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDocFiscal";
            this.toolTip1.SetToolTip(this, "Documento Fiscal");
            this.Activated += new System.EventHandler(this.frmDocFiscal_Activated);
            this.Load += new System.EventHandler(this.frmDocFiscal_Load);
            this.gbxDocFiscal.ResumeLayout(false);
            this.gbxDocFiscal.PerformLayout();
            this.tspColaborador.ResumeLayout(false);
            this.tspColaborador.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbxDocFiscal;
        private System.Windows.Forms.TextBox tbxModelo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbxAtivo;
        private System.Windows.Forms.TextBox tbxCogNome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxSerie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip tspColaborador;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspRetornar;
    }
}