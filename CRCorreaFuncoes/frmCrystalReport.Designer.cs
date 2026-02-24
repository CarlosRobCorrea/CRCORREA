namespace CRCorreaFuncoes
{
    partial class frmCrystalReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCrystalReport));
            this.crvRel = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tspCrystal = new System.Windows.Forms.ToolStrip();
            this.tspEmail = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.bwrCrystal = new System.ComponentModel.BackgroundWorker();
            this.tspCrystal.SuspendLayout();
            this.SuspendLayout();
            // 
            // crvRel
            // 
            this.crvRel.ActiveViewIndex = -1;
            this.crvRel.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.crvRel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvRel.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvRel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crvRel.Location = new System.Drawing.Point(0, 0);
            this.crvRel.Name = "crvRel";
            this.crvRel.Size = new System.Drawing.Size(800, 600);
            this.crvRel.TabIndex = 0;
            this.crvRel.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crvRel.Load += new System.EventHandler(this.crvRel_Load);
            // 
            // tspCrystal
            // 
            this.tspCrystal.AccessibleDescription = "Barra de Opções";
            this.tspCrystal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tspCrystal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspCrystal.Dock = System.Windows.Forms.DockStyle.None;
            this.tspCrystal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCrystal.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.tspCrystal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspEmail,
            this.tspRetornar});
            this.tspCrystal.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspCrystal.Location = new System.Drawing.Point(707, 3);
            this.tspCrystal.Name = "tspCrystal";
            this.tspCrystal.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspCrystal.Size = new System.Drawing.Size(144, 35);
            this.tspCrystal.TabIndex = 1;
            this.tspCrystal.Text = "Imprimir";
            // 
            // tspEmail
            // 
            this.tspEmail.AutoSize = false;
            this.tspEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspEmail.Image = ((System.Drawing.Image)(resources.GetObject("tspEmail.Image")));
            this.tspEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspEmail.Name = "tspEmail";
            this.tspEmail.Size = new System.Drawing.Size(56, 32);
            this.tspEmail.Text = "&Email";
            this.tspEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspEmail.Click += new System.EventHandler(this.tspEmail_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(56, 32);
            this.tspRetornar.Text = "&Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // bwrCrystal
            // 
            this.bwrCrystal.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrCrystal_DoWork);
            this.bwrCrystal.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrCrystal_RunWorkerCompleted);
            // 
            // frmCrystalReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tspCrystal);
            this.Controls.Add(this.crvRel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCrystalReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impressão de Relatorios";
            this.tspCrystal.ResumeLayout(false);
            this.tspCrystal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tspCrystal;
        private System.Windows.Forms.ToolStripButton tspEmail;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.ComponentModel.BackgroundWorker bwrCrystal;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvRel;
    }
}