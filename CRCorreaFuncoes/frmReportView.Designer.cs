namespace CRCorreaFuncoes
{
    partial class frmReportView
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
            this.crvrReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crvrReport
            // 
            this.crvrReport.ActiveViewIndex = -1;
            this.crvrReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.crvrReport.DisplayGroupTree = false; obsoleto
            this.crvrReport.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crvrReport.Location = new System.Drawing.Point(12, 12);
            this.crvrReport.Name = "crvrReport";
            this.crvrReport.SelectionFormula = "";
            this.crvrReport.ShowGroupTreeButton = false;
            this.crvrReport.Size = new System.Drawing.Size(925, 661);
            this.crvrReport.TabIndex = 0;
            this.crvrReport.ViewTimeSelectionFormula = "";
            // 
            // button1
            // 
            this.button1.Image = global::CRCorreaFuncoes.Properties.Resources.Retornar;
            this.button1.Location = new System.Drawing.Point(943, 627);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 46);
            this.button1.TabIndex = 1;
            this.button1.Text = "Retorna&r";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.crvrReport);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReportView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visualizador de Relatórios";
            this.Load += new System.EventHandler(this.frmReportView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvrReport;
        private System.Windows.Forms.Button button1;
    }
}