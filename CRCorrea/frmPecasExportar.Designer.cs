namespace CRCorrea
{
    partial class frmPecasExportar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPecasExportar));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tspPecasExportaSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPecasExportaRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.AccessibleDescription = "Barra de Opções";
            this.toolStrip2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspPecasExportaSalvar,
            this.tspPecasExportaRetornar});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(186, 203);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(292, 45);
            this.toolStrip2.TabIndex = 9;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "toolStrip1";
            // 
            // tspPecasExportaSalvar
            // 
            this.tspPecasExportaSalvar.AccessibleDescription = "Salvar";
            this.tspPecasExportaSalvar.AutoSize = false;
            this.tspPecasExportaSalvar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPecasExportaSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspPecasExportaSalvar.Image")));
            this.tspPecasExportaSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPecasExportaSalvar.Name = "tspPecasExportaSalvar";
            this.tspPecasExportaSalvar.Size = new System.Drawing.Size(180, 42);
            this.tspPecasExportaSalvar.Text = "Exportar Arquivo Peças";
            this.tspPecasExportaSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspPecasExportaSalvar.Click += new System.EventHandler(this.tspPecasExportaSalvar_Click);
            // 
            // tspPecasExportaRetornar
            // 
            this.tspPecasExportaRetornar.AccessibleDescription = "Retornar";
            this.tspPecasExportaRetornar.AutoSize = false;
            this.tspPecasExportaRetornar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPecasExportaRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspPecasExportaRetornar.Image")));
            this.tspPecasExportaRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPecasExportaRetornar.Name = "tspPecasExportaRetornar";
            this.tspPecasExportaRetornar.Size = new System.Drawing.Size(80, 42);
            this.tspPecasExportaRetornar.Text = "Retorna&r";
            this.tspPecasExportaRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspPecasExportaRetornar.Click += new System.EventHandler(this.tspPecasExportaRetornar_Click);
            // 
            // frmPecasExportar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip2);
            this.Name = "frmPecasExportar";
            this.Text = "frmPecasExportar";
            this.Load += new System.EventHandler(this.frmPecasExportar_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tspPecasExportaSalvar;
        private System.Windows.Forms.ToolStripButton tspPecasExportaRetornar;
    }
}