namespace CRCorrea
{
    partial class frmContacontabil
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContacontabil));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxReduzido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbnAtivoN = new System.Windows.Forms.RadioButton();
            this.rbnAtivoS = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbnAnalitica = new System.Windows.Forms.RadioButton();
            this.rbnTotalizadora = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.tbxReduzido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxNome);
            this.groupBox1.Controls.Add(this.tbxCodigo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // tbxReduzido
            // 
            resources.ApplyResources(this.tbxReduzido, "tbxReduzido");
            this.tbxReduzido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxReduzido.Name = "tbxReduzido";
            this.toolTip1.SetToolTip(this.tbxReduzido, resources.GetString("tbxReduzido.ToolTip"));
            this.tbxReduzido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxReduzido.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxReduzido.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.rbnAtivoN);
            this.groupBox4.Controls.Add(this.rbnAtivoS);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // rbnAtivoN
            // 
            resources.ApplyResources(this.rbnAtivoN, "rbnAtivoN");
            this.rbnAtivoN.Name = "rbnAtivoN";
            this.toolTip1.SetToolTip(this.rbnAtivoN, resources.GetString("rbnAtivoN.ToolTip"));
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
            this.toolTip1.SetToolTip(this.rbnAtivoS, resources.GetString("rbnAtivoS.ToolTip"));
            this.rbnAtivoS.UseVisualStyleBackColor = true;
            this.rbnAtivoS.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnAtivoS.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivoS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.rbnAnalitica);
            this.groupBox2.Controls.Add(this.rbnTotalizadora);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // rbnAnalitica
            // 
            resources.ApplyResources(this.rbnAnalitica, "rbnAnalitica");
            this.rbnAnalitica.Checked = true;
            this.rbnAnalitica.Name = "rbnAnalitica";
            this.rbnAnalitica.TabStop = true;
            this.toolTip1.SetToolTip(this.rbnAnalitica, resources.GetString("rbnAnalitica.ToolTip"));
            this.rbnAnalitica.UseVisualStyleBackColor = true;
            this.rbnAnalitica.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnAnalitica.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAnalitica.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // rbnTotalizadora
            // 
            resources.ApplyResources(this.rbnTotalizadora, "rbnTotalizadora");
            this.rbnTotalizadora.Name = "rbnTotalizadora";
            this.toolTip1.SetToolTip(this.rbnTotalizadora, resources.GetString("rbnTotalizadora.ToolTip"));
            this.rbnTotalizadora.UseVisualStyleBackColor = true;
            this.rbnTotalizadora.Leave += new System.EventHandler(this.ControlLeave);
            this.rbnTotalizadora.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnTotalizadora.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // frmContacontabil
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmContacontabil";
            this.ShowInTaskbar = false;
            this.Tag = "53";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.frmContacontabil_Load);
            this.Shown += new System.EventHandler(this.frmContacontabil_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxReduzido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbnAtivoN;
        private System.Windows.Forms.RadioButton rbnAtivoS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbnAnalitica;
        private System.Windows.Forms.RadioButton rbnTotalizadora;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
    }
}