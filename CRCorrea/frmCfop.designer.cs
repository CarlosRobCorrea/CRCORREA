namespace CRCorrea
{
    partial class frmCfop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCfop));
            this.gbxDados = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbnSaida = new System.Windows.Forms.RadioButton();
            this.rbnEntrada = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbnAtivoN = new System.Windows.Forms.RadioButton();
            this.rbnAtivoS = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbxContaCreRed = new System.Windows.Forms.TextBox();
            this.btnContaCre = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxContaCre = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxContaDebRed = new System.Windows.Forms.TextBox();
            this.btnContaDeb = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxContaDeb = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbnVariosCfop = new System.Windows.Forms.RadioButton();
            this.rbn1Cfop = new System.Windows.Forms.RadioButton();
            this.btnConcatenaCFOP = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxDizer = new System.Windows.Forms.TextBox();
            this.tbxCfop = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDiretorio = new System.Windows.Forms.Label();
            this.tbxNomeNota = new System.Windows.Forms.TextBox();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ckxCombustivel = new System.Windows.Forms.CheckBox();
            this.gbxDados.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxDados
            // 
            resources.ApplyResources(this.gbxDados, "gbxDados");
            this.gbxDados.Controls.Add(this.ckxCombustivel);
            this.gbxDados.Controls.Add(this.groupBox5);
            this.gbxDados.Controls.Add(this.groupBox4);
            this.gbxDados.Controls.Add(this.groupBox3);
            this.gbxDados.Controls.Add(this.groupBox2);
            this.gbxDados.Controls.Add(this.groupBox1);
            this.gbxDados.Controls.Add(this.label2);
            this.gbxDados.Controls.Add(this.tbxDizer);
            this.gbxDados.Controls.Add(this.tbxCfop);
            this.gbxDados.Controls.Add(this.label1);
            this.gbxDados.Controls.Add(this.lblDiretorio);
            this.gbxDados.Controls.Add(this.tbxNomeNota);
            this.gbxDados.Name = "gbxDados";
            this.gbxDados.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxDados, resources.GetString("gbxDados.ToolTip"));
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.rbnSaida);
            this.groupBox5.Controls.Add(this.rbnEntrada);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox5, resources.GetString("groupBox5.ToolTip"));
            // 
            // rbnSaida
            // 
            resources.ApplyResources(this.rbnSaida, "rbnSaida");
            this.rbnSaida.Name = "rbnSaida";
            this.toolTip1.SetToolTip(this.rbnSaida, resources.GetString("rbnSaida.ToolTip"));
            this.rbnSaida.UseVisualStyleBackColor = true;
            this.rbnSaida.CheckedChanged += new System.EventHandler(this.rbnSaida_CheckedChanged);
            this.rbnSaida.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnSaida.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnSaida.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnEntrada
            // 
            resources.ApplyResources(this.rbnEntrada, "rbnEntrada");
            this.rbnEntrada.Checked = true;
            this.rbnEntrada.Name = "rbnEntrada";
            this.rbnEntrada.TabStop = true;
            this.toolTip1.SetToolTip(this.rbnEntrada, resources.GetString("rbnEntrada.ToolTip"));
            this.rbnEntrada.UseVisualStyleBackColor = true;
            this.rbnEntrada.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnEntrada.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnEntrada.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbnAtivoN);
            this.groupBox4.Controls.Add(this.rbnAtivoS);
            resources.ApplyResources(this.groupBox4, "groupBox4");
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
            this.rbnAtivoN.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivoN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtivoN.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbnAtivoS
            // 
            resources.ApplyResources(this.rbnAtivoS, "rbnAtivoS");
            this.rbnAtivoS.Checked = true;
            this.rbnAtivoS.Name = "rbnAtivoS";
            this.rbnAtivoS.TabStop = true;
            this.toolTip1.SetToolTip(this.rbnAtivoS, resources.GetString("rbnAtivoS.ToolTip"));
            this.rbnAtivoS.UseVisualStyleBackColor = true;
            this.rbnAtivoS.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnAtivoS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnAtivoS.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbxContaCreRed);
            this.groupBox3.Controls.Add(this.btnContaCre);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tbxContaCre);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // tbxContaCreRed
            // 
            this.tbxContaCreRed.BackColor = System.Drawing.Color.LemonChiffon;
            resources.ApplyResources(this.tbxContaCreRed, "tbxContaCreRed");
            this.tbxContaCreRed.Name = "tbxContaCreRed";
            this.tbxContaCreRed.ReadOnly = true;
            this.tbxContaCreRed.TabStop = false;
            this.toolTip1.SetToolTip(this.tbxContaCreRed, resources.GetString("tbxContaCreRed.ToolTip"));
            this.tbxContaCreRed.TextChanged += new System.EventHandler(this.tbxContaCreRed_TextChanged);
            // 
            // btnContaCre
            // 
            resources.ApplyResources(this.btnContaCre, "btnContaCre");
            this.btnContaCre.Name = "btnContaCre";
            this.btnContaCre.TabStop = false;
            this.toolTip1.SetToolTip(this.btnContaCre, resources.GetString("btnContaCre.ToolTip"));
            this.btnContaCre.UseVisualStyleBackColor = true;
            this.btnContaCre.Click += new System.EventHandler(this.btnContaCre_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tbxContaCre
            // 
            this.tbxContaCre.BackColor = System.Drawing.Color.LemonChiffon;
            resources.ApplyResources(this.tbxContaCre, "tbxContaCre");
            this.tbxContaCre.Name = "tbxContaCre";
            this.tbxContaCre.ReadOnly = true;
            this.tbxContaCre.TabStop = false;
            this.toolTip1.SetToolTip(this.tbxContaCre, resources.GetString("tbxContaCre.ToolTip"));
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxContaDebRed);
            this.groupBox2.Controls.Add(this.btnContaDeb);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbxContaDeb);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // tbxContaDebRed
            // 
            this.tbxContaDebRed.BackColor = System.Drawing.Color.LemonChiffon;
            resources.ApplyResources(this.tbxContaDebRed, "tbxContaDebRed");
            this.tbxContaDebRed.Name = "tbxContaDebRed";
            this.tbxContaDebRed.ReadOnly = true;
            this.tbxContaDebRed.TabStop = false;
            this.toolTip1.SetToolTip(this.tbxContaDebRed, resources.GetString("tbxContaDebRed.ToolTip"));
            // 
            // btnContaDeb
            // 
            resources.ApplyResources(this.btnContaDeb, "btnContaDeb");
            this.btnContaDeb.Name = "btnContaDeb";
            this.btnContaDeb.TabStop = false;
            this.toolTip1.SetToolTip(this.btnContaDeb, resources.GetString("btnContaDeb.ToolTip"));
            this.btnContaDeb.UseVisualStyleBackColor = true;
            this.btnContaDeb.Click += new System.EventHandler(this.btnContaDeb_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // tbxContaDeb
            // 
            this.tbxContaDeb.BackColor = System.Drawing.Color.LemonChiffon;
            resources.ApplyResources(this.tbxContaDeb, "tbxContaDeb");
            this.tbxContaDeb.Name = "tbxContaDeb";
            this.tbxContaDeb.ReadOnly = true;
            this.tbxContaDeb.TabStop = false;
            this.toolTip1.SetToolTip(this.tbxContaDeb, resources.GetString("tbxContaDeb.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.rbnVariosCfop);
            this.groupBox1.Controls.Add(this.rbn1Cfop);
            this.groupBox1.Controls.Add(this.btnConcatenaCFOP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // rbnVariosCfop
            // 
            resources.ApplyResources(this.rbnVariosCfop, "rbnVariosCfop");
            this.rbnVariosCfop.Name = "rbnVariosCfop";
            this.rbnVariosCfop.TabStop = true;
            this.toolTip1.SetToolTip(this.rbnVariosCfop, resources.GetString("rbnVariosCfop.ToolTip"));
            this.rbnVariosCfop.UseVisualStyleBackColor = true;
            this.rbnVariosCfop.Enter += new System.EventHandler(this.ControlEnter);
            this.rbnVariosCfop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbnVariosCfop.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // rbn1Cfop
            // 
            resources.ApplyResources(this.rbn1Cfop, "rbn1Cfop");
            this.rbn1Cfop.Name = "rbn1Cfop";
            this.rbn1Cfop.TabStop = true;
            this.toolTip1.SetToolTip(this.rbn1Cfop, resources.GetString("rbn1Cfop.ToolTip"));
            this.rbn1Cfop.UseVisualStyleBackColor = true;
            this.rbn1Cfop.Enter += new System.EventHandler(this.ControlEnter);
            this.rbn1Cfop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.rbn1Cfop.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // btnConcatenaCFOP
            // 
            resources.ApplyResources(this.btnConcatenaCFOP, "btnConcatenaCFOP");
            this.btnConcatenaCFOP.Name = "btnConcatenaCFOP";
            this.btnConcatenaCFOP.TabStop = false;
            this.toolTip1.SetToolTip(this.btnConcatenaCFOP, resources.GetString("btnConcatenaCFOP.ToolTip"));
            this.btnConcatenaCFOP.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbxDizer
            // 
            resources.ApplyResources(this.tbxDizer, "tbxDizer");
            this.tbxDizer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDizer.Name = "tbxDizer";
            this.toolTip1.SetToolTip(this.tbxDizer, resources.GetString("tbxDizer.ToolTip"));
            this.tbxDizer.TextChanged += new System.EventHandler(this.tbxDizer_TextChanged);
            this.tbxDizer.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDizer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDizer.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxCfop
            // 
            resources.ApplyResources(this.tbxCfop, "tbxCfop");
            this.tbxCfop.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCfop.Name = "tbxCfop";
            this.toolTip1.SetToolTip(this.tbxCfop, resources.GetString("tbxCfop.ToolTip"));
            this.tbxCfop.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCfop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCfop.Leave += new System.EventHandler(this.ControlLeave);
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
            // tbxNomeNota
            // 
            resources.ApplyResources(this.tbxNomeNota, "tbxNomeNota");
            this.tbxNomeNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNomeNota.Name = "tbxNomeNota";
            this.toolTip1.SetToolTip(this.tbxNomeNota, resources.GetString("tbxNomeNota.ToolTip"));
            this.tbxNomeNota.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNomeNota.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNomeNota.Leave += new System.EventHandler(this.ControlLeave);
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
            // ckxCombustivel
            // 
            resources.ApplyResources(this.ckxCombustivel, "ckxCombustivel");
            this.ckxCombustivel.Name = "ckxCombustivel";
            this.ckxCombustivel.UseVisualStyleBackColor = true;
            // 
            // frmCfop
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxDados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmCfop";
            this.ShowInTaskbar = false;
            this.Tag = "50";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Activated += new System.EventHandler(this.frmCfop_Activated);
            this.Load += new System.EventHandler(this.frmCfop_Load);
            this.Shown += new System.EventHandler(this.frmCfop_Shown);
            this.gbxDados.ResumeLayout(false);
            this.gbxDados.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxDados;
        private System.Windows.Forms.Label lblDiretorio;
        private System.Windows.Forms.TextBox tbxNomeNota;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxCfop;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxDizer;
        private System.Windows.Forms.RadioButton rbnVariosCfop;
        private System.Windows.Forms.RadioButton rbn1Cfop;
        private System.Windows.Forms.Button btnConcatenaCFOP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbxContaCreRed;
        private System.Windows.Forms.Button btnContaCre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxContaCre;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbxContaDebRed;
        private System.Windows.Forms.Button btnContaDeb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxContaDeb;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbnAtivoN;
        private System.Windows.Forms.RadioButton rbnAtivoS;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbnSaida;
        private System.Windows.Forms.RadioButton rbnEntrada;
        private System.Windows.Forms.CheckBox ckxCombustivel;
    }
}