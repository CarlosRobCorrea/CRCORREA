namespace CRCorrea
{
    partial class frmUnidade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnidade));
            this.ttpUnidade = new System.Windows.Forms.ToolTip(this.components);
            this.tspUnidade = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxUnidade = new System.Windows.Forms.GroupBox();
            this.tbxUnidDec = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tspUnidade.SuspendLayout();
            this.gbxUnidade.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspUnidade
            // 
            this.tspUnidade.AccessibleDescription = "Barra de Opções";
            this.tspUnidade.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspUnidade.AutoSize = false;
            this.tspUnidade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspUnidade.Dock = System.Windows.Forms.DockStyle.None;
            this.tspUnidade.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUnidade.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspUnidade.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspRetornar});
            this.tspUnidade.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspUnidade.Location = new System.Drawing.Point(141, 393);
            this.tspUnidade.Name = "tspUnidade";
            this.tspUnidade.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspUnidade.Size = new System.Drawing.Size(735, 44);
            this.tspUnidade.TabIndex = 2;
            this.tspUnidade.TabStop = true;
            this.tspUnidade.Text = "toolStrip1";
            this.ttpUnidade.SetToolTip(this.tspUnidade, "Menu1");
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspSalvar.Image")));
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(48, 42);
            this.tspSalvar.Text = "&Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspPrimeiro
            // 
            this.tspPrimeiro.AccessibleDescription = "Primeiro";
            this.tspPrimeiro.AutoSize = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = ((System.Drawing.Image)(resources.GetObject("tspPrimeiro.Image")));
            this.tspPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Size = new System.Drawing.Size(61, 42);
            this.tspPrimeiro.Text = "Pr&imeiro";
            this.tspPrimeiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspPrimeiro.Click += new System.EventHandler(this.tspPrimeiro_Click);
            // 
            // tspAnterior
            // 
            this.tspAnterior.AccessibleDescription = "Anterior";
            this.tspAnterior.AutoSize = false;
            this.tspAnterior.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAnterior.Image = ((System.Drawing.Image)(resources.GetObject("tspAnterior.Image")));
            this.tspAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAnterior.Name = "tspAnterior";
            this.tspAnterior.Size = new System.Drawing.Size(62, 42);
            this.tspAnterior.Text = "&Anterior";
            this.tspAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAnterior.Click += new System.EventHandler(this.tspAnterior_Click);
            // 
            // tspProximo
            // 
            this.tspProximo.AccessibleDescription = "Próximo";
            this.tspProximo.AutoSize = false;
            this.tspProximo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspProximo.Image = ((System.Drawing.Image)(resources.GetObject("tspProximo.Image")));
            this.tspProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspProximo.Name = "tspProximo";
            this.tspProximo.Size = new System.Drawing.Size(61, 42);
            this.tspProximo.Text = "Pró&ximo";
            this.tspProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspProximo.Click += new System.EventHandler(this.tspProximo_Click);
            // 
            // tspUltimo
            // 
            this.tspUltimo.AccessibleDescription = "Último";
            this.tspUltimo.AutoSize = false;
            this.tspUltimo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUltimo.Image = ((System.Drawing.Image)(resources.GetObject("tspUltimo.Image")));
            this.tspUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Size = new System.Drawing.Size(50, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspUltimo.Click += new System.EventHandler(this.tspUltimo_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // gbxUnidade
            // 
            this.gbxUnidade.AccessibleDescription = "Unidade";
            this.gbxUnidade.Controls.Add(this.tbxUnidDec);
            this.gbxUnidade.Controls.Add(this.label2);
            this.gbxUnidade.Controls.Add(this.tbxNome);
            this.gbxUnidade.Controls.Add(this.label4);
            this.gbxUnidade.Controls.Add(this.tbxCodigo);
            this.gbxUnidade.Controls.Add(this.label1);
            this.gbxUnidade.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxUnidade.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbxUnidade.Location = new System.Drawing.Point(141, 208);
            this.gbxUnidade.Name = "gbxUnidade";
            this.gbxUnidade.Size = new System.Drawing.Size(735, 182);
            this.gbxUnidade.TabIndex = 0;
            this.gbxUnidade.TabStop = false;
            this.gbxUnidade.Text = "Unidade";
            this.ttpUnidade.SetToolTip(this.gbxUnidade, "Grupo Unidade");
            // 
            // tbxUnidDec
            // 
            this.tbxUnidDec.AccessibleDescription = "Casas Decimais";
            this.tbxUnidDec.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxUnidDec.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUnidDec.Location = new System.Drawing.Point(10, 146);
            this.tbxUnidDec.MaxLength = 1;
            this.tbxUnidDec.Name = "tbxUnidDec";
            this.tbxUnidDec.Size = new System.Drawing.Size(21, 21);
            this.tbxUnidDec.TabIndex = 88;
            this.ttpUnidade.SetToolTip(this.tbxUnidDec, "Casas Decimais");
            this.tbxUnidDec.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxUnidDec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxUnidDec.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(7, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "Casas Decimais";
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Nome";
            this.tbxNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(10, 93);
            this.tbxNome.MaxLength = 100;
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(711, 21);
            this.tbxNome.TabIndex = 2;
            this.ttpUnidade.SetToolTip(this.tbxNome, "Nome");
            this.tbxNome.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(7, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "Nome";
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.AccessibleDescription = "Código";
            this.tbxCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Location = new System.Drawing.Point(10, 42);
            this.tbxCodigo.MaxLength = 2;
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(60, 21);
            this.tbxCodigo.TabIndex = 0;
            this.ttpUnidade.SetToolTip(this.tbxCodigo, "Codigo");
            this.tbxCodigo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCodigo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Código";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(147, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(729, 50);
            this.label3.TabIndex = 130;
            this.label3.Text = " Unidade de Medida";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmUnidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbxUnidade);
            this.Controls.Add(this.tspUnidade);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUnidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4014";
            this.Text = "Unidade - Cadastro";
            this.ttpUnidade.SetToolTip(this, "Unidade  - Registrando");
            this.Activated += new System.EventHandler(this.frmUnidade_Activated);
            this.Load += new System.EventHandler(this.frmUnidade_Load);
            this.Shown += new System.EventHandler(this.frmUnidade_Shown);
            this.tspUnidade.ResumeLayout(false);
            this.tspUnidade.PerformLayout();
            this.gbxUnidade.ResumeLayout(false);
            this.gbxUnidade.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpUnidade;
        private System.Windows.Forms.ToolStrip tspUnidade;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxUnidade;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxUnidDec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        //private Aplisoft.DataSets.FOLHA.FUNCAO.taFuncaoFuncionarioCommands taFuncaoFuncionarioCommands1;
    }
}