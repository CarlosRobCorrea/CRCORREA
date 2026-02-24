namespace CRCorrea
{
    partial class frmClienteComercial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClienteComercial));
            this.gbxFicha = new System.Windows.Forms.GroupBox();
            this.btnRetornar = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnRefazerFichaComercial = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.tbxDataateC = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.tbxDatadeC = new System.Windows.Forms.TextBox();
            this.gbxFichaComercial = new System.Windows.Forms.GroupBox();
            this.dgvFichaComercial = new System.Windows.Forms.DataGridView();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tbxCognome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxFicha.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.gbxFichaComercial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFichaComercial)).BeginInit();
            this.groupBox13.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxFicha
            // 
            this.gbxFicha.Controls.Add(this.btnRetornar);
            this.gbxFicha.Controls.Add(this.groupBox7);
            this.gbxFicha.Controls.Add(this.gbxFichaComercial);
            this.gbxFicha.Location = new System.Drawing.Point(12, 73);
            this.gbxFicha.Name = "gbxFicha";
            this.gbxFicha.Size = new System.Drawing.Size(996, 479);
            this.gbxFicha.TabIndex = 0;
            this.gbxFicha.TabStop = false;
            // 
            // btnRetornar
            // 
            this.btnRetornar.AccessibleDescription = "Retornar";
            this.btnRetornar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetornar.Image = global::CRCorrea.Properties.Resources.Retornar;
            this.btnRetornar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRetornar.Location = new System.Drawing.Point(845, 29);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(97, 42);
            this.btnRetornar.TabIndex = 93;
            this.btnRetornar.TabStop = false;
            this.btnRetornar.Text = "Retornar";
            this.btnRetornar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRetornar.UseVisualStyleBackColor = false;
            this.btnRetornar.Click += new System.EventHandler(this.btnRetornar_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.AccessibleDescription = "Filtro - Data";
            this.groupBox7.Controls.Add(this.btnRefazerFichaComercial);
            this.groupBox7.Controls.Add(this.label29);
            this.groupBox7.Controls.Add(this.tbxDataateC);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.tbxDatadeC);
            this.groupBox7.Location = new System.Drawing.Point(12, 14);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(323, 54);
            this.groupBox7.TabIndex = 92;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Filtro - Data";
            // 
            // btnRefazerFichaComercial
            // 
            this.btnRefazerFichaComercial.AccessibleDescription = "Pesquisar Condições de Pagamento";
            this.btnRefazerFichaComercial.Font = new System.Drawing.Font("Tahoma", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefazerFichaComercial.Image = ((System.Drawing.Image)(resources.GetObject("btnRefazerFichaComercial.Image")));
            this.btnRefazerFichaComercial.Location = new System.Drawing.Point(278, 13);
            this.btnRefazerFichaComercial.Name = "btnRefazerFichaComercial";
            this.btnRefazerFichaComercial.Size = new System.Drawing.Size(34, 28);
            this.btnRefazerFichaComercial.TabIndex = 4;
            this.btnRefazerFichaComercial.TabStop = false;
            this.btnRefazerFichaComercial.Text = "&C";
            this.btnRefazerFichaComercial.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnRefazerFichaComercial.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(144, 23);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(26, 13);
            this.label29.TabIndex = 3;
            this.label29.Text = "Até:";
            // 
            // tbxDataateC
            // 
            this.tbxDataateC.AccessibleDescription = "Data ate:";
            this.tbxDataateC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDataateC.Location = new System.Drawing.Point(178, 20);
            this.tbxDataateC.Name = "tbxDataateC";
            this.tbxDataateC.Size = new System.Drawing.Size(85, 21);
            this.tbxDataateC.TabIndex = 1;
            this.tbxDataateC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(16, 23);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(24, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "De:";
            // 
            // tbxDatadeC
            // 
            this.tbxDatadeC.AccessibleDescription = "Data de:";
            this.tbxDatadeC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDatadeC.Location = new System.Drawing.Point(46, 20);
            this.tbxDatadeC.Name = "tbxDatadeC";
            this.tbxDatadeC.Size = new System.Drawing.Size(85, 21);
            this.tbxDatadeC.TabIndex = 0;
            this.tbxDatadeC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbxFichaComercial
            // 
            this.gbxFichaComercial.AccessibleDescription = "Comercial";
            this.gbxFichaComercial.Controls.Add(this.dgvFichaComercial);
            this.gbxFichaComercial.Location = new System.Drawing.Point(5, 79);
            this.gbxFichaComercial.Name = "gbxFichaComercial";
            this.gbxFichaComercial.Size = new System.Drawing.Size(937, 402);
            this.gbxFichaComercial.TabIndex = 91;
            this.gbxFichaComercial.TabStop = false;
            // 
            // dgvFichaComercial
            // 
            this.dgvFichaComercial.AccessibleDescription = "Grid Comercial";
            this.dgvFichaComercial.AllowUserToAddRows = false;
            this.dgvFichaComercial.AllowUserToDeleteRows = false;
            this.dgvFichaComercial.AllowUserToResizeColumns = false;
            this.dgvFichaComercial.AllowUserToResizeRows = false;
            this.dgvFichaComercial.BackgroundColor = System.Drawing.Color.Silver;
            this.dgvFichaComercial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFichaComercial.Location = new System.Drawing.Point(6, 16);
            this.dgvFichaComercial.Name = "dgvFichaComercial";
            this.dgvFichaComercial.ReadOnly = true;
            this.dgvFichaComercial.RowHeadersVisible = false;
            this.dgvFichaComercial.Size = new System.Drawing.Size(914, 380);
            this.dgvFichaComercial.TabIndex = 0;
            // 
            // groupBox13
            // 
            this.groupBox13.AccessibleDescription = "Dados Básicos";
            this.groupBox13.Controls.Add(this.tbxNome);
            this.groupBox13.Controls.Add(this.label2);
            this.groupBox13.Controls.Add(this.label23);
            this.groupBox13.Controls.Add(this.tbxCognome);
            this.groupBox13.Location = new System.Drawing.Point(12, 5);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(619, 62);
            this.groupBox13.TabIndex = 1;
            this.groupBox13.TabStop = false;
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Nome";
            this.tbxNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(152, 33);
            this.tbxNome.MaxLength = 50;
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(449, 23);
            this.tbxNome.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Razão Social / Nome:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 11);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(55, 13);
            this.label23.TabIndex = 1;
            this.label23.Text = "Cognome:";
            // 
            // tbxCognome
            // 
            this.tbxCognome.AccessibleDescription = "Cognome";
            this.tbxCognome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCognome.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCognome.Location = new System.Drawing.Point(10, 33);
            this.tbxCognome.MaxLength = 20;
            this.tbxCognome.Name = "tbxCognome";
            this.tbxCognome.Size = new System.Drawing.Size(136, 23);
            this.tbxCognome.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(637, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 39);
            this.label1.TabIndex = 52;
            this.label1.Text = "Ficha Comercial";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmClienteComercial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 601);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox13);
            this.Controls.Add(this.gbxFicha);
            this.Name = "frmClienteComercial";
            this.Text = "frmClienteComercial";
            this.Load += new System.EventHandler(this.frmClienteComercial_Load);
            this.gbxFicha.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.gbxFichaComercial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFichaComercial)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxFicha;
        private System.Windows.Forms.Button btnRetornar;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnRefazerFichaComercial;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tbxDataateC;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox tbxDatadeC;
        private System.Windows.Forms.GroupBox gbxFichaComercial;
        private System.Windows.Forms.DataGridView dgvFichaComercial;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbxCognome;
        private System.Windows.Forms.Label label1;
    }
}