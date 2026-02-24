namespace CRCorrea
{
    partial class frmProgresso
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbrProgresso = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tmrProgresso = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pbrProgresso);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(176, 43);
            this.panel1.TabIndex = 0;
            // 
            // pbrProgresso
            // 
            this.pbrProgresso.AccessibleDescription = "Processando";
            this.pbrProgresso.Location = new System.Drawing.Point(3, 24);
            this.pbrProgresso.MarqueeAnimationSpeed = 40;
            this.pbrProgresso.Maximum = 1000;
            this.pbrProgresso.Name = "pbrProgresso";
            this.pbrProgresso.Size = new System.Drawing.Size(166, 12);
            this.pbrProgresso.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbrProgresso.TabIndex = 1;
            this.toolTip1.SetToolTip(this.pbrProgresso, "Barra de Progresso");
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 8);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(128, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Processando. Aguarde...";
            // 
            // tmrProgresso
            // 
            this.tmrProgresso.Enabled = true;
            this.tmrProgresso.Interval = 500;
            // 
            // frmProgresso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(176, 43);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProgresso";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "16";
            this.Text = "Barra de Progresso ";
            this.toolTip1.SetToolTip(this, "Barra de Progresso");
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar pbrProgresso;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer tmrProgresso;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}