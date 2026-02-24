namespace CRCorreaFuncoes
{
    partial class frmCrystalReportsreduzido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCrystalReportsreduzido));
            this.pbxAguarde = new System.Windows.Forms.PictureBox();
            this.TooTip = new System.Windows.Forms.ToolTip(this.components);
            this.tspCrystal = new System.Windows.Forms.ToolStrip();
            this.tspEmail = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.CrystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.bwrCrystal = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAguarde)).BeginInit();
            this.tspCrystal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxAguarde
            // 
            this.pbxAguarde.AccessibleDescription = "Carregando Relatório";
            this.pbxAguarde.BackColor = System.Drawing.Color.White;
            this.pbxAguarde.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxAguarde.Image = ((System.Drawing.Image)(resources.GetObject("pbxAguarde.Image")));
            this.pbxAguarde.Location = new System.Drawing.Point(1, 31);
            this.pbxAguarde.Name = "pbxAguarde";
            this.pbxAguarde.Size = new System.Drawing.Size(785, 447);
            this.pbxAguarde.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxAguarde.TabIndex = 27;
            this.pbxAguarde.TabStop = false;
            // 
            // tspCrystal
            // 
            this.tspCrystal.AccessibleDescription = "Barra de Opções";
            this.tspCrystal.AutoSize = false;
            this.tspCrystal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspCrystal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCrystal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspEmail,
            this.tspRetornar});
            this.tspCrystal.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspCrystal.Location = new System.Drawing.Point(0, 503);
            this.tspCrystal.Name = "tspCrystal";
            this.tspCrystal.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspCrystal.Size = new System.Drawing.Size(786, 37);
            this.tspCrystal.TabIndex = 26;
            this.TooTip.SetToolTip(this.tspCrystal, "Barra de Opções");
            // 
            // tspEmail
            // 
            this.tspEmail.AccessibleDescription = "E-mail";
            this.tspEmail.AutoSize = false;
            this.tspEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspEmail.Image = global::CRCorreaFuncoes.Properties.Resources.Email;
            this.tspEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspEmail.Name = "tspEmail";
            this.tspEmail.Size = new System.Drawing.Size(66, 36);
            this.tspEmail.Text = "E-mail";
            this.tspEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspEmail.Click += new System.EventHandler(this.tspEmail_Click);
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
            // CrystalReportViewer
            // 
            this.CrystalReportViewer.AccessibleDescription = "Vizualização de Relatório";
            this.CrystalReportViewer.ActiveViewIndex = -1;
            this.CrystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CrystalReportViewer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.CrystalReportViewer.Location = new System.Drawing.Point(0, 1);
            this.CrystalReportViewer.Name = "CrystalReportViewer";
            this.CrystalReportViewer.ShowCloseButton = false;
            this.CrystalReportViewer.ShowGroupTreeButton = false;
            this.CrystalReportViewer.ShowParameterPanelButton = false;
            this.CrystalReportViewer.ShowRefreshButton = false;
            this.CrystalReportViewer.Size = new System.Drawing.Size(785, 498);
            this.CrystalReportViewer.TabIndex = 25;
            this.CrystalReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // bwrCrystal
            // 
            this.bwrCrystal.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrCrystal_DoWork);
            this.bwrCrystal.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrCrystal_RunWorkerCompleted);
            // 
            // frmCrystalReportsreduzido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 540);
            this.Controls.Add(this.pbxAguarde);
            this.Controls.Add(this.CrystalReportViewer);
            this.Controls.Add(this.tspCrystal);
            this.Name = "frmCrystalReportsreduzido";
            this.Text = "frmCrystalReportsreduzido";
            this.Load += new System.EventHandler(this.frmCrystalReportsreduzido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxAguarde)).EndInit();
            this.tspCrystal.ResumeLayout(false);
            this.tspCrystal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxAguarde;
        private System.Windows.Forms.ToolTip TooTip;
        private System.Windows.Forms.ToolStrip tspCrystal;
        private System.Windows.Forms.ToolStripButton tspEmail;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReportViewer;
        private System.ComponentModel.BackgroundWorker bwrCrystal;
    }
}