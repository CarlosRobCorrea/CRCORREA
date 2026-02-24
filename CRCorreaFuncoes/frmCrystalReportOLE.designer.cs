namespace CRCorreaFuncoes
{
    partial class frmCrystalReportOLE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCrystalReportOLE));
            this.reportDocument1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.ofdArquivo = new System.Windows.Forms.OpenFileDialog();
            this.TooTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxServidor = new System.Windows.Forms.ComboBox();
            this.tbxCrystal = new System.Windows.Forms.TextBox();
            this.btnRelatorio = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxLayoutCrystal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxBase = new System.Windows.Forms.TextBox();
            this.tbxSenha = new System.Windows.Forms.TextBox();
            this.tbxId = new System.Windows.Forms.TextBox();
            this.cmdVisualisar = new System.Windows.Forms.Button();
            this.pbxAguarde = new System.Windows.Forms.PictureBox();
            this.tspCrystal = new System.Windows.Forms.ToolStrip();
            this.Email = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.bwrCrystal = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAguarde)).BeginInit();
            this.tspCrystal.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.AccessibleDescription = "Visualização de Relatório";
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 1);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowParameterPanelButton = false;
            this.crystalReportViewer1.ShowRefreshButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1024, 644);
            this.crystalReportViewer1.TabIndex = 1;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.TooTip.SetToolTip(this.crystalReportViewer1, "Visualização");
            // 
            // ofdArquivo
            // 
            this.ofdArquivo.FileName = "ofdArquivo";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxServidor);
            this.groupBox1.Controls.Add(this.tbxCrystal);
            this.groupBox1.Controls.Add(this.btnRelatorio);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbxLayoutCrystal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxBase);
            this.groupBox1.Controls.Add(this.tbxSenha);
            this.groupBox1.Controls.Add(this.tbxId);
            this.groupBox1.Controls.Add(this.cmdVisualisar);
            this.groupBox1.Location = new System.Drawing.Point(12, 402);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(974, 211);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.TooTip.SetToolTip(this.groupBox1, "groupBox1");
            this.groupBox1.Visible = false;
            // 
            // cbxServidor
            // 
            this.cbxServidor.FormattingEnabled = true;
            this.cbxServidor.Items.AddRange(new object[] {
            "SERVIDOR",
            "WILLIAMS",
            "SERV01",
            "SERV02"});
            this.cbxServidor.Location = new System.Drawing.Point(216, 48);
            this.cbxServidor.Name = "cbxServidor";
            this.cbxServidor.Size = new System.Drawing.Size(121, 21);
            this.cbxServidor.TabIndex = 27;
            this.cbxServidor.Text = "SERVIDOR";
            this.TooTip.SetToolTip(this.cbxServidor, "1");
            // 
            // tbxCrystal
            // 
            this.tbxCrystal.Location = new System.Drawing.Point(155, 110);
            this.tbxCrystal.Name = "tbxCrystal";
            this.tbxCrystal.Size = new System.Drawing.Size(628, 21);
            this.tbxCrystal.TabIndex = 26;
            this.TooTip.SetToolTip(this.tbxCrystal, "1");
            // 
            // btnRelatorio
            // 
            this.btnRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRelatorio.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelatorio.Location = new System.Drawing.Point(125, 83);
            this.btnRelatorio.Name = "btnRelatorio";
            this.btnRelatorio.Size = new System.Drawing.Size(21, 21);
            this.btnRelatorio.TabIndex = 25;
            this.btnRelatorio.TabStop = false;
            this.TooTip.SetToolTip(this.btnRelatorio, "1");
            this.btnRelatorio.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Relatório";
            // 
            // tbxLayoutCrystal
            // 
            this.tbxLayoutCrystal.Location = new System.Drawing.Point(155, 84);
            this.tbxLayoutCrystal.Name = "tbxLayoutCrystal";
            this.tbxLayoutCrystal.Size = new System.Drawing.Size(628, 21);
            this.tbxLayoutCrystal.TabIndex = 23;
            this.TooTip.SetToolTip(this.tbxLayoutCrystal, "1");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(449, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Senha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(343, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Base";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Servidor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Id";
            // 
            // tbxBase
            // 
            this.tbxBase.Location = new System.Drawing.Point(346, 47);
            this.tbxBase.Name = "tbxBase";
            this.tbxBase.Size = new System.Drawing.Size(100, 21);
            this.tbxBase.TabIndex = 18;
            this.tbxBase.Text = "CCSFOLHA";
            this.TooTip.SetToolTip(this.tbxBase, "1");
            // 
            // tbxSenha
            // 
            this.tbxSenha.Location = new System.Drawing.Point(452, 47);
            this.tbxSenha.Name = "tbxSenha";
            this.tbxSenha.Size = new System.Drawing.Size(100, 21);
            this.tbxSenha.TabIndex = 17;
            this.tbxSenha.Text = "Apli5800";
            this.TooTip.SetToolTip(this.tbxSenha, "1");
            // 
            // tbxId
            // 
            this.tbxId.Location = new System.Drawing.Point(155, 47);
            this.tbxId.Name = "tbxId";
            this.tbxId.Size = new System.Drawing.Size(52, 21);
            this.tbxId.TabIndex = 16;
            this.tbxId.Text = "sa";
            this.TooTip.SetToolTip(this.tbxId, "1");
            // 
            // cmdVisualisar
            // 
            this.cmdVisualisar.Location = new System.Drawing.Point(6, 36);
            this.cmdVisualisar.Name = "cmdVisualisar";
            this.cmdVisualisar.Size = new System.Drawing.Size(70, 40);
            this.cmdVisualisar.TabIndex = 15;
            this.cmdVisualisar.Text = "Visualisar";
            this.TooTip.SetToolTip(this.cmdVisualisar, "1");
            this.cmdVisualisar.UseVisualStyleBackColor = true;
            // 
            // pbxAguarde
            // 
            this.pbxAguarde.AccessibleDescription = "Carregando Relatório";
            this.pbxAguarde.BackColor = System.Drawing.Color.White;
            this.pbxAguarde.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxAguarde.Image = ((System.Drawing.Image)(resources.GetObject("pbxAguarde.Image")));
            this.pbxAguarde.Location = new System.Drawing.Point(0, 31);
            this.pbxAguarde.Name = "pbxAguarde";
            this.pbxAguarde.Size = new System.Drawing.Size(1023, 593);
            this.pbxAguarde.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxAguarde.TabIndex = 25;
            this.pbxAguarde.TabStop = false;
            this.TooTip.SetToolTip(this.pbxAguarde, "Imagem");
            // 
            // tspCrystal
            // 
            this.tspCrystal.AccessibleDescription = "Barra de Opções";
            this.tspCrystal.AutoSize = false;
            this.tspCrystal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspCrystal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCrystal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Email,
            this.tspRetornar});
            this.tspCrystal.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspCrystal.Location = new System.Drawing.Point(0, 648);
            this.tspCrystal.Name = "tspCrystal";
            this.tspCrystal.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspCrystal.Size = new System.Drawing.Size(1024, 37);
            this.tspCrystal.TabIndex = 26;
            this.TooTip.SetToolTip(this.tspCrystal, "Barra de Opções");
            // 
            // Email
            // 
            this.Email.AccessibleDescription = "Email";
            this.Email.AutoSize = false;
            this.Email.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Email.Image = global::CRCorreaFuncoes.Properties.Resources.Email;
            this.Email.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(66, 36);
            this.Email.Text = "E-mail";
            this.Email.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Email.Click += new System.EventHandler(this.Email_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 36);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // bwrCrystal
            // 
            this.bwrCrystal.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrCrystal_DoWork);
            this.bwrCrystal.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrCrystal_RunWorkerCompleted);
            // 
            // frmCrystalReportOLE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 685);
            this.Controls.Add(this.tspCrystal);
            this.Controls.Add(this.pbxAguarde);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.crystalReportViewer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCrystalReportOLE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Relatorios - Visualização";
            this.TooTip.SetToolTip(this, "Visualização");
            this.Load += new System.EventHandler(this.frmCrystalReportOLE_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAguarde)).EndInit();
            this.tspCrystal.ResumeLayout(false);
            this.tspCrystal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocument1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.OpenFileDialog ofdArquivo;
        private System.Windows.Forms.ToolTip TooTip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxServidor;
        private System.Windows.Forms.TextBox tbxCrystal;
        private System.Windows.Forms.Button btnRelatorio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxLayoutCrystal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxBase;
        private System.Windows.Forms.TextBox tbxSenha;
        private System.Windows.Forms.TextBox tbxId;
        private System.Windows.Forms.Button cmdVisualisar;
        private System.Windows.Forms.PictureBox pbxAguarde;
        private System.Windows.Forms.ToolStrip tspCrystal;
        private System.Windows.Forms.ToolStripButton Email;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.ComponentModel.BackgroundWorker bwrCrystal;
    }
}

