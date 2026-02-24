namespace CRCorrea
{
    partial class frmZonas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZonas));
            this.gbxDados = new System.Windows.Forms.GroupBox();
            this.gbxAtivo = new System.Windows.Forms.GroupBox();
            this.rbnAtivoN = new System.Windows.Forms.RadioButton();
            this.rbnAtivoS = new System.Windows.Forms.RadioButton();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDiretorio = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxDados.SuspendLayout();
            this.gbxAtivo.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxDados
            // 
            resources.ApplyResources(this.gbxDados, "gbxDados");
            this.gbxDados.Controls.Add(this.gbxAtivo);
            this.gbxDados.Controls.Add(this.tbxCodigo);
            this.gbxDados.Controls.Add(this.label1);
            this.gbxDados.Controls.Add(this.lblDiretorio);
            this.gbxDados.Controls.Add(this.tbxNome);
            this.gbxDados.Name = "gbxDados";
            this.gbxDados.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxDados, resources.GetString("gbxDados.ToolTip"));
            // 
            // gbxAtivo
            // 
            resources.ApplyResources(this.gbxAtivo, "gbxAtivo");
            this.gbxAtivo.Controls.Add(this.rbnAtivoN);
            this.gbxAtivo.Controls.Add(this.rbnAtivoS);
            this.gbxAtivo.Name = "gbxAtivo";
            this.gbxAtivo.TabStop = false;
            // 
            // rbnAtivoN
            // 
            resources.ApplyResources(this.rbnAtivoN, "rbnAtivoN");
            this.rbnAtivoN.Name = "rbnAtivoN";
            this.rbnAtivoN.TabStop = true;
            this.rbnAtivoN.UseVisualStyleBackColor = true;
            this.rbnAtivoN.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnAtivoN.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivoN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // rbnAtivoS
            // 
            resources.ApplyResources(this.rbnAtivoS, "rbnAtivoS");
            this.rbnAtivoS.Checked = true;
            this.rbnAtivoS.Name = "rbnAtivoS";
            this.rbnAtivoS.TabStop = true;
            this.rbnAtivoS.UseVisualStyleBackColor = true;
            this.rbnAtivoS.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnAtivoS.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivoS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // tbxCodigo
            // 
            resources.ApplyResources(this.tbxCodigo, "tbxCodigo");
            this.tbxCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigo.Name = "tbxCodigo";
            this.toolTip1.SetToolTip(this.tbxCodigo, resources.GetString("tbxCodigo.ToolTip"));
            this.tbxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigo.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxCodigo.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblDiretorio
            // 
            resources.ApplyResources(this.lblDiretorio, "lblDiretorio");
            this.lblDiretorio.Name = "lblDiretorio";
            // 
            // tbxNome
            // 
            resources.ApplyResources(this.tbxNome, "tbxNome");
            this.tbxNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNome.Name = "tbxNome";
            this.toolTip1.SetToolTip(this.tbxNome, resources.GetString("tbxNome.ToolTip"));
            this.tbxNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxNome.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tspTool
            // 
            resources.ApplyResources(this.tspTool, "tspTool");
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.TabStop = true;
            this.toolTip1.SetToolTip(this.tspTool, resources.GetString("tspTool.ToolTip"));
            // 
            // tspSalvar
            // 
            resources.ApplyResources(this.tspSalvar, "tspSalvar");
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspPrimeiro
            // 
            resources.ApplyResources(this.tspPrimeiro, "tspPrimeiro");
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Click += new System.EventHandler(this.tspPrimeiro_Click);
            // 
            // tspAnterior
            // 
            resources.ApplyResources(this.tspAnterior, "tspAnterior");
            this.tspAnterior.Name = "tspAnterior";
            this.tspAnterior.Click += new System.EventHandler(this.tspAnterior_Click);
            // 
            // tspProximo
            // 
            resources.ApplyResources(this.tspProximo, "tspProximo");
            this.tspProximo.Name = "tspProximo";
            this.tspProximo.Click += new System.EventHandler(this.tspProximo_Click);
            // 
            // tspUltimo
            // 
            resources.ApplyResources(this.tspUltimo, "tspUltimo");
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Click += new System.EventHandler(this.tspUltimo_Click);
            // 
            // tspRetornar
            // 
            resources.ApplyResources(this.tspRetornar, "tspRetornar");
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // frmZonas
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxDados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmZonas";
            this.ShowInTaskbar = false;
            this.Tag = "56";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.frmZonas_Load);
            this.gbxDados.ResumeLayout(false);
            this.gbxDados.PerformLayout();
            this.gbxAtivo.ResumeLayout(false);
            this.gbxAtivo.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxDados;
        private System.Windows.Forms.Label lblDiretorio;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbxAtivo;
        private System.Windows.Forms.RadioButton rbnAtivoN;
        private System.Windows.Forms.RadioButton rbnAtivoS;
    }
}