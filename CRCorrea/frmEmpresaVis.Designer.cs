using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CRCorrea
{
    partial class frmEmpresaVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpresaVis));
            this.gbxEmpresas = new System.Windows.Forms.GroupBox();
            this.dgvEmpresas = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbxPesquisa = new System.Windows.Forms.TextBox();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.gbxOpcoes = new System.Windows.Forms.GroupBox();
            this.tspOpcoesEmpresa = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tspEscolher = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tspRelatorios = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.gbxPesquisa = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxEmpresas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpresas)).BeginInit();
            this.gbxOpcoes.SuspendLayout();
            this.tspOpcoesEmpresa.SuspendLayout();
            this.gbxPesquisa.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxEmpresas
            // 
            this.gbxEmpresas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxEmpresas.BackColor = System.Drawing.Color.DimGray;
            this.gbxEmpresas.Controls.Add(this.dgvEmpresas);
            this.gbxEmpresas.Location = new System.Drawing.Point(6, 5);
            this.gbxEmpresas.Name = "gbxEmpresas";
            this.gbxEmpresas.Size = new System.Drawing.Size(990, 456);
            this.gbxEmpresas.TabIndex = 0;
            this.gbxEmpresas.TabStop = false;
            // 
            // dgvEmpresas
            // 
            this.dgvEmpresas.AccessibleDescription = "Visualizar Cadastro das Empresas";
            this.dgvEmpresas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEmpresas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmpresas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpresas.Location = new System.Drawing.Point(8, 21);
            this.dgvEmpresas.Name = "dgvEmpresas";
            this.dgvEmpresas.Size = new System.Drawing.Size(976, 429);
            this.dgvEmpresas.TabIndex = 1;
            this.dgvEmpresas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvEmpresas_MouseDoubleClick);
            // 
            // tbxPesquisa
            // 
            this.tbxPesquisa.AccessibleDescription = "Pesquisa";
            this.tbxPesquisa.Location = new System.Drawing.Point(84, 29);
            this.tbxPesquisa.Name = "tbxPesquisa";
            this.tbxPesquisa.Size = new System.Drawing.Size(193, 21);
            this.tbxPesquisa.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxPesquisa, "Pesquisa");
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AccessibleDescription = "Botões de Opção";
            this.miniToolStrip.AccessibleName = "Nova seleção de item";
            this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
            this.miniToolStrip.AllowMerge = false;
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.SystemColors.ControlDark;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.miniToolStrip.Location = new System.Drawing.Point(1, 2);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.miniToolStrip.Size = new System.Drawing.Size(359, 54);
            this.miniToolStrip.TabIndex = 2;
            // 
            // gbxOpcoes
            // 
            this.gbxOpcoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxOpcoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gbxOpcoes.Controls.Add(this.tspOpcoesEmpresa);
            this.gbxOpcoes.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxOpcoes.Location = new System.Drawing.Point(6, 469);
            this.gbxOpcoes.Name = "gbxOpcoes";
            this.gbxOpcoes.Size = new System.Drawing.Size(471, 69);
            this.gbxOpcoes.TabIndex = 1;
            this.gbxOpcoes.TabStop = false;
            this.gbxOpcoes.Text = "Opções";
            // 
            // tspOpcoesEmpresa
            // 
            this.tspOpcoesEmpresa.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.tspIncluir,
            this.toolStripSeparator2,
            this.tspAlterar,
            this.toolStripSeparator3,
            this.tspExcluir,
            this.toolStripSeparator4,
            this.tspEscolher,
            this.toolStripSeparator5,
            this.tspRelatorios,
            this.toolStripSeparator6,
            this.tspRetornar,
            this.toolStripSeparator7});
            this.tspOpcoesEmpresa.Location = new System.Drawing.Point(3, 17);
            this.tspOpcoesEmpresa.Name = "tspOpcoesEmpresa";
            this.tspOpcoesEmpresa.Size = new System.Drawing.Size(465, 45);
            this.tspOpcoesEmpresa.TabIndex = 0;
            this.tspOpcoesEmpresa.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // tspIncluir
            // 
            this.tspIncluir.AccessibleDescription = "Incluir Empresa";
            this.tspIncluir.AutoSize = false;
            this.tspIncluir.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tspIncluir.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspIncluir.Image = ((System.Drawing.Image)(resources.GetObject("tspIncluir.Image")));
            this.tspIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspIncluir.Name = "tspIncluir";
            this.tspIncluir.Size = new System.Drawing.Size(66, 42);
            this.tspIncluir.Text = "Incluir";
            this.tspIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspIncluir.ToolTipText = "Incluir Empresa";
            this.tspIncluir.Click += new System.EventHandler(this.tspIncluir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // tspAlterar
            // 
            this.tspAlterar.AccessibleDescription = "Alterar Empresa";
            this.tspAlterar.AutoSize = false;
            this.tspAlterar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tspAlterar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAlterar.Image = ((System.Drawing.Image)(resources.GetObject("tspAlterar.Image")));
            this.tspAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAlterar.Name = "tspAlterar";
            this.tspAlterar.Size = new System.Drawing.Size(66, 42);
            this.tspAlterar.Text = "Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.ToolTipText = "Alterar Empresa";
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 45);
            // 
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir Empresa";
            this.tspExcluir.AutoSize = false;
            this.tspExcluir.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tspExcluir.Image")));
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(66, 42);
            this.tspExcluir.Text = "Excluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.ToolTipText = "Excluir Empresa";
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 45);
            // 
            // tspEscolher
            // 
            this.tspEscolher.AccessibleDescription = "Escolher Empresa";
            this.tspEscolher.AutoSize = false;
            this.tspEscolher.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tspEscolher.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspEscolher.Image = ((System.Drawing.Image)(resources.GetObject("tspEscolher.Image")));
            this.tspEscolher.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspEscolher.Name = "tspEscolher";
            this.tspEscolher.Size = new System.Drawing.Size(66, 42);
            this.tspEscolher.Text = "Escolher";
            this.tspEscolher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspEscolher.ToolTipText = "Escolher Empresa";
            this.tspEscolher.Click += new System.EventHandler(this.tspEscolher_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 45);
            // 
            // tspRelatorios
            // 
            this.tspRelatorios.AccessibleDescription = "Imprimir Relatorios";
            this.tspRelatorios.AutoSize = false;
            this.tspRelatorios.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tspRelatorios.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRelatorios.Image = ((System.Drawing.Image)(resources.GetObject("tspRelatorios.Image")));
            this.tspRelatorios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRelatorios.Name = "tspRelatorios";
            this.tspRelatorios.Size = new System.Drawing.Size(66, 42);
            this.tspRelatorios.Text = "Relatorios";
            this.tspRelatorios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRelatorios.ToolTipText = "Imprimir Relatorios";
            this.tspRelatorios.Click += new System.EventHandler(this.tspRelatorios_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 45);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar Menu Principal";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retornar";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.ToolTipText = "Retornar Menu Principal";
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 45);
            // 
            // gbxPesquisa
            // 
            this.gbxPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.gbxPesquisa.Controls.Add(this.label1);
            this.gbxPesquisa.Controls.Add(this.tbxPesquisa);
            this.gbxPesquisa.Location = new System.Drawing.Point(486, 469);
            this.gbxPesquisa.Name = "gbxPesquisa";
            this.gbxPesquisa.Size = new System.Drawing.Size(506, 69);
            this.gbxPesquisa.TabIndex = 2;
            this.gbxPesquisa.TabStop = false;
            this.gbxPesquisa.Text = "Pesquisa/Informações";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pesquisar";
            // 
            // frmEmpresaVis
            // 
            this.AccessibleDescription = "Visualizar Empresas Cadastradas";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 572);
            this.Controls.Add(this.gbxPesquisa);
            this.Controls.Add(this.gbxOpcoes);
            this.Controls.Add(this.gbxEmpresas);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEmpresaVis";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visualizar Cadastros das Empresas Cadasatradas";
            this.Activated += new System.EventHandler(this.frmEmpresaVis_Activated);
            this.gbxEmpresas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpresas)).EndInit();
            this.gbxOpcoes.ResumeLayout(false);
            this.gbxOpcoes.PerformLayout();
            this.tspOpcoesEmpresa.ResumeLayout(false);
            this.tspOpcoesEmpresa.PerformLayout();
            this.gbxPesquisa.ResumeLayout(false);
            this.gbxPesquisa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gbxEmpresas;
        private ToolTip toolTip1;
        private ToolStrip miniToolStrip;
        private GroupBox gbxOpcoes;
        private ToolStrip tspOpcoesEmpresa;
        private GroupBox gbxPesquisa;
        private Label label1;
        private TextBox tbxPesquisa;
        private ToolStripButton tspIncluir;
        private ToolStripButton tspAlterar;
        private ToolStripButton tspExcluir;
        private ToolStripButton tspEscolher;
        private ToolStripButton tspRelatorios;
        private ToolStripButton tspRetornar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private DataGridView dgvEmpresas;
    }
}