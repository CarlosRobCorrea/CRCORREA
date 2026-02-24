namespace CRCorrea
{
    partial class frmFormsScanVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormsScanVis));
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tsbconfirmarAlteracoes = new System.Windows.Forms.ToolStripButton();
            this.tsbEscanear = new System.Windows.Forms.ToolStripButton();
            this.tsbRetornar = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.tspSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslbLocalizar = new System.Windows.Forms.ToolStripLabel();
            this.tstbxFiltrarForms = new System.Windows.Forms.ToolStripTextBox();
            this.dgvForms = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFormsRegistros = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tspTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForms)).BeginInit();
            this.SuspendLayout();
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AllowMerge = false;
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbconfirmarAlteracoes,
            this.tsbEscanear,
            this.tsbRetornar,
            this.tspExcluir,
            this.tspSeparador1,
            this.tslbLocalizar,
            this.tstbxFiltrarForms});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(0, 638);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(1020, 47);
            this.tspTool.TabIndex = 19;
            // 
            // tsbconfirmarAlteracoes
            // 
            this.tsbconfirmarAlteracoes.AccessibleDescription = "Incluir";
            this.tsbconfirmarAlteracoes.AutoSize = false;
            this.tsbconfirmarAlteracoes.Enabled = false;
            this.tsbconfirmarAlteracoes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbconfirmarAlteracoes.Image = global::CRCorrea.Properties.Resources.Ok;
            this.tsbconfirmarAlteracoes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbconfirmarAlteracoes.Name = "tsbconfirmarAlteracoes";
            this.tsbconfirmarAlteracoes.Size = new System.Drawing.Size(66, 42);
            this.tsbconfirmarAlteracoes.Text = "Confirmar";
            this.tsbconfirmarAlteracoes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbconfirmarAlteracoes.Click += new System.EventHandler(this.tsbconfirmarAlteracoes_Click);
            // 
            // tsbEscanear
            // 
            this.tsbEscanear.AccessibleDescription = "Retornar";
            this.tsbEscanear.AutoSize = false;
            this.tsbEscanear.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbEscanear.Image = global::CRCorrea.Properties.Resources.Atualizar;
            this.tsbEscanear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEscanear.Name = "tsbEscanear";
            this.tsbEscanear.Size = new System.Drawing.Size(66, 42);
            this.tsbEscanear.Text = "Escanear";
            this.tsbEscanear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbEscanear.Click += new System.EventHandler(this.tsbEscanear_Click);
            // 
            // tsbRetornar
            // 
            this.tsbRetornar.AccessibleDescription = "Retornar";
            this.tsbRetornar.AutoSize = false;
            this.tsbRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tsbRetornar.Image")));
            this.tsbRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRetornar.Name = "tsbRetornar";
            this.tsbRetornar.Size = new System.Drawing.Size(66, 42);
            this.tsbRetornar.Text = "Retorna&r";
            this.tsbRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir";
            this.tspExcluir.AutoSize = false;
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.Image = global::CRCorrea.Properties.Resources.Excluir;
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(66, 42);
            this.tspExcluir.Text = "E&xcluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
            // 
            // tspSeparador1
            // 
            this.tspSeparador1.AutoSize = false;
            this.tspSeparador1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.tspSeparador1.Name = "tspSeparador1";
            this.tspSeparador1.Size = new System.Drawing.Size(6, 47);
            // 
            // tslbLocalizar
            // 
            this.tslbLocalizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslbLocalizar.Margin = new System.Windows.Forms.Padding(5, 17, 0, 2);
            this.tslbLocalizar.Name = "tslbLocalizar";
            this.tslbLocalizar.Size = new System.Drawing.Size(51, 16);
            this.tslbLocalizar.Text = "Forms:";
            // 
            // tstbxFiltrarForms
            // 
            this.tstbxFiltrarForms.AccessibleDescription = "Procurar";
            this.tstbxFiltrarForms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbxFiltrarForms.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tstbxFiltrarForms.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstbxFiltrarForms.Margin = new System.Windows.Forms.Padding(1, 15, 1, 0);
            this.tstbxFiltrarForms.Name = "tstbxFiltrarForms";
            this.tstbxFiltrarForms.Size = new System.Drawing.Size(120, 18);
            this.tstbxFiltrarForms.ToolTipText = "Procurar";
            this.tstbxFiltrarForms.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbxLocalizar_KeyUp);
            // 
            // dgvForms
            // 
            this.dgvForms.AllowUserToAddRows = false;
            this.dgvForms.AllowUserToDeleteRows = false;
            this.dgvForms.AllowUserToOrderColumns = true;
            this.dgvForms.AllowUserToResizeRows = false;
            this.dgvForms.BackgroundColor = System.Drawing.Color.White;
            this.dgvForms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForms.Location = new System.Drawing.Point(12, 26);
            this.dgvForms.MultiSelect = false;
            this.dgvForms.Name = "dgvForms";
            this.dgvForms.ReadOnly = true;
            this.dgvForms.RowHeadersVisible = false;
            this.dgvForms.Size = new System.Drawing.Size(996, 609);
            this.dgvForms.StandardTab = true;
            this.dgvForms.TabIndex = 20;
            this.dgvForms.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvForms_CellDoubleClick);
            this.dgvForms.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvForms_ColumnHeaderMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 14);
            this.label1.TabIndex = 23;
            this.label1.Text = "Formulários que fazem parte do seu Módulo:";
            // 
            // lblFormsRegistros
            // 
            this.lblFormsRegistros.Location = new System.Drawing.Point(863, 8);
            this.lblFormsRegistros.Name = "lblFormsRegistros";
            this.lblFormsRegistros.Size = new System.Drawing.Size(145, 14);
            this.lblFormsRegistros.TabIndex = 25;
            this.lblFormsRegistros.Text = "Nº de Formulários:  00000";
            this.lblFormsRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmFormsScanVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.lblFormsRegistros);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvForms);
            this.Controls.Add(this.tspTool);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmFormsScanVis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulários - Visualização";
            this.toolTip1.SetToolTip(this, "Formulários - Visualização");
            this.Load += new System.EventHandler(this.frmFormsVis_Load);
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tsbconfirmarAlteracoes;
        private System.Windows.Forms.ToolStripButton tsbRetornar;
        private System.Windows.Forms.ToolStripSeparator tspSeparador1;
        private System.Windows.Forms.ToolStripLabel tslbLocalizar;
        private System.Windows.Forms.ToolStripTextBox tstbxFiltrarForms;
        private System.Windows.Forms.DataGridView dgvForms;
        private System.Windows.Forms.ToolStripButton tsbEscanear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFormsRegistros;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton tspExcluir;
    }
}