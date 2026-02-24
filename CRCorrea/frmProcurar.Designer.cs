namespace CRCorrea
{
    partial class frmProcurar
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
            this.gbxGrid = new System.Windows.Forms.GroupBox();
            this.cbxGrid = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbxColuna = new System.Windows.Forms.GroupBox();
            this.cbxColuna = new System.Windows.Forms.ComboBox();
            this.gbxPesquisar = new System.Windows.Forms.GroupBox();
            this.tbxInformar = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxGrid.SuspendLayout();
            this.gbxColuna.SuspendLayout();
            this.gbxPesquisar.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxGrid
            // 
            this.gbxGrid.AccessibleDescription = "Grid";
            this.gbxGrid.Controls.Add(this.cbxGrid);
            this.gbxGrid.Location = new System.Drawing.Point(12, 12);
            this.gbxGrid.Name = "gbxGrid";
            this.gbxGrid.Size = new System.Drawing.Size(181, 51);
            this.gbxGrid.TabIndex = 0;
            this.gbxGrid.TabStop = false;
            this.gbxGrid.Text = "Grid";
            // 
            // cbxGrid
            // 
            this.cbxGrid.AccessibleDescription = "Grid";
            this.cbxGrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGrid.Location = new System.Drawing.Point(6, 20);
            this.cbxGrid.Name = "cbxGrid";
            this.cbxGrid.Size = new System.Drawing.Size(169, 21);
            this.cbxGrid.TabIndex = 0;
            this.cbxGrid.Leave += new System.EventHandler(this.ControlLeave);
            this.cbxGrid.Enter += new System.EventHandler(this.ControlEnter);
            this.cbxGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AccessibleDescription = "Cancelar";
            this.btnCancelar.Image = global::CRCorrea.Properties.Resources.Cancelar;
            this.btnCancelar.Location = new System.Drawing.Point(129, 183);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(64, 47);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleDescription = "OK";
            this.btnOk.Image = global::CRCorrea.Properties.Resources.Ok;
            this.btnOk.Location = new System.Drawing.Point(59, 183);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 47);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&Ok";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gbxColuna
            // 
            this.gbxColuna.AccessibleDescription = "Coluna";
            this.gbxColuna.Controls.Add(this.cbxColuna);
            this.gbxColuna.Location = new System.Drawing.Point(12, 69);
            this.gbxColuna.Name = "gbxColuna";
            this.gbxColuna.Size = new System.Drawing.Size(181, 52);
            this.gbxColuna.TabIndex = 5;
            this.gbxColuna.TabStop = false;
            this.gbxColuna.Text = "Coluna";
            // 
            // cbxColuna
            // 
            this.cbxColuna.AccessibleDescription = "Coluna";
            this.cbxColuna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxColuna.Location = new System.Drawing.Point(6, 20);
            this.cbxColuna.Name = "cbxColuna";
            this.cbxColuna.Size = new System.Drawing.Size(169, 21);
            this.cbxColuna.TabIndex = 1;
            this.cbxColuna.Leave += new System.EventHandler(this.ControlLeave);
            this.cbxColuna.Enter += new System.EventHandler(this.ControlEnter);
            this.cbxColuna.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // gbxPesquisar
            // 
            this.gbxPesquisar.AccessibleDescription = "Informação Desejada";
            this.gbxPesquisar.Controls.Add(this.tbxInformar);
            this.gbxPesquisar.Location = new System.Drawing.Point(12, 127);
            this.gbxPesquisar.Name = "gbxPesquisar";
            this.gbxPesquisar.Size = new System.Drawing.Size(181, 50);
            this.gbxPesquisar.TabIndex = 7;
            this.gbxPesquisar.TabStop = false;
            this.gbxPesquisar.Text = "Informação Desejada";
            // 
            // tbxInformar
            // 
            this.tbxInformar.AccessibleDescription = "Informar";
            this.tbxInformar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxInformar.Location = new System.Drawing.Point(6, 20);
            this.tbxInformar.Name = "tbxInformar";
            this.tbxInformar.Size = new System.Drawing.Size(169, 21);
            this.tbxInformar.TabIndex = 0;
            this.tbxInformar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxInformar.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxInformar.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // frmProcurar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 244);
            this.ControlBox = false;
            this.Controls.Add(this.gbxPesquisar);
            this.Controls.Add(this.gbxColuna);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbxGrid);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProcurar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "15";
            this.Text = "Procurar  Filtrar - Cadastro";
            this.toolTip1.SetToolTip(this, "Procurar  Filtrar - Cadastro");
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmProcurar_Load);
            this.gbxGrid.ResumeLayout(false);
            this.gbxColuna.ResumeLayout(false);
            this.gbxPesquisar.ResumeLayout(false);
            this.gbxPesquisar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxGrid;
        private System.Windows.Forms.ComboBox cbxGrid;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox gbxColuna;
        private System.Windows.Forms.ComboBox cbxColuna;
        private System.Windows.Forms.GroupBox gbxPesquisar;
        private System.Windows.Forms.TextBox tbxInformar;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}