namespace CRCorreaFuncoes
{
    partial class frmReportView2
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
            this.btnRetornar = new System.Windows.Forms.Button();
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
            this.crvrReport.TabIndex = 2;
            this.crvrReport.ViewTimeSelectionFormula = "";
            // 
            // btnRetornar
            // 
            this.btnRetornar.Image = global::CRCorreaFuncoes.Properties.Resources.Retornar;
            this.btnRetornar.Location = new System.Drawing.Point(943, 627);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(65, 46);
            this.btnRetornar.TabIndex = 3;
            this.btnRetornar.Text = "Retorna&r";
            this.btnRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRetornar.UseVisualStyleBackColor = true;
            this.btnRetornar.Click += new System.EventHandler(this.btnRetornar_Click);
            // 
            // frmReportView2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.btnRetornar);
            this.Controls.Add(this.crvrReport);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReportView2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visualizar Relatório";
            this.Load += new System.EventHandler(this.frmReportView2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRetornar;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvrReport;
    }
}