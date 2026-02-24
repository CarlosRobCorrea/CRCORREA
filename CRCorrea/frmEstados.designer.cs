namespace CRCorrea
{
    partial class frmEstados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEstados));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ttpESTADOS = new System.Windows.Forms.ToolTip(this.components);
            this.tspESTADOS = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxDadosEstados = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxIEST = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxZonaFranca = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbxFimcep = new System.Windows.Forms.TextBox();
            this.tbxInicep = new System.Windows.Forms.TextBox();
            this.tbxAliquota = new System.Windows.Forms.TextBox();
            this.tbxIbge = new System.Windows.Forms.TextBox();
            this.tbxRegiao = new System.Windows.Forms.TextBox();
            this.tbxCapital = new System.Windows.Forms.TextBox();
            this.tbxNomext = new System.Windows.Forms.TextBox();
            this.tbxEstado = new System.Windows.Forms.TextBox();
            this.gbxESTADOSICMS = new System.Windows.Forms.GroupBox();
            this.pbxESTADOSICMS = new System.Windows.Forms.PictureBox();
            this.tspESTADOSICMS = new System.Windows.Forms.ToolStrip();
            this.tspIncluir = new System.Windows.Forms.ToolStripButton();
            this.tspAlterar = new System.Windows.Forms.ToolStripButton();
            this.tspExcluir = new System.Windows.Forms.ToolStripButton();
            this.dgvESTADOSICMS = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDESTADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDESTADODESTINO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALIQUOTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDDIZERESNFV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bwrESTADOSICMS = new System.ComponentModel.BackgroundWorker();
            this.tspESTADOS.SuspendLayout();
            this.gbxDadosEstados.SuspendLayout();
            this.gbxESTADOSICMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxESTADOSICMS)).BeginInit();
            this.tspESTADOSICMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvESTADOSICMS)).BeginInit();
            this.SuspendLayout();
            // 
            // tspESTADOS
            // 
            this.tspESTADOS.AccessibleDescription = "Barra de Opções";
            this.tspESTADOS.AutoSize = false;
            this.tspESTADOS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspESTADOS.Dock = System.Windows.Forms.DockStyle.None;
            this.tspESTADOS.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspESTADOS.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspESTADOS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspRetornar});
            this.tspESTADOS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspESTADOS.Location = new System.Drawing.Point(19, 441);
            this.tspESTADOS.Name = "tspESTADOS";
            this.tspESTADOS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspESTADOS.Size = new System.Drawing.Size(1003, 44);
            this.tspESTADOS.TabIndex = 2;
            this.tspESTADOS.TabStop = true;
            this.tspESTADOS.Text = "toolStrip1";
            this.ttpESTADOS.SetToolTip(this.tspESTADOS, "Menu1");
            this.tspESTADOS.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tspESTADOS_ItemClicked);
            // 
            // tspSalvar
            // 
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tspPrimeiro.AutoSize = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = ((System.Drawing.Image)(resources.GetObject("tspPrimeiro.Image")));
            this.tspPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Size = new System.Drawing.Size(48, 42);
            this.tspPrimeiro.Text = "Pr&imeiro";
            this.tspPrimeiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspPrimeiro.Click += new System.EventHandler(this.tspPrimeiro_Click);
            // 
            // tspAnterior
            // 
            this.tspAnterior.AutoSize = false;
            this.tspAnterior.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAnterior.Image = ((System.Drawing.Image)(resources.GetObject("tspAnterior.Image")));
            this.tspAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAnterior.Name = "tspAnterior";
            this.tspAnterior.Size = new System.Drawing.Size(48, 42);
            this.tspAnterior.Text = "&Anterior";
            this.tspAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAnterior.Click += new System.EventHandler(this.tspAnterior_Click);
            // 
            // tspProximo
            // 
            this.tspProximo.AccessibleDescription = "Próximo";
            this.tspProximo.AutoSize = false;
            this.tspProximo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspProximo.Image = ((System.Drawing.Image)(resources.GetObject("tspProximo.Image")));
            this.tspProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspProximo.Name = "tspProximo";
            this.tspProximo.Size = new System.Drawing.Size(48, 42);
            this.tspProximo.Text = "Próximo";
            this.tspProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspProximo.Click += new System.EventHandler(this.tspProximo_Click);
            // 
            // tspUltimo
            // 
            this.tspUltimo.AccessibleDescription = "Último";
            this.tspUltimo.AutoSize = false;
            this.tspUltimo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUltimo.Image = ((System.Drawing.Image)(resources.GetObject("tspUltimo.Image")));
            this.tspUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Size = new System.Drawing.Size(48, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspUltimo.Click += new System.EventHandler(this.tspUltimo_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(54, 41);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // gbxDadosEstados
            // 
            this.gbxDadosEstados.AccessibleDescription = "Estado da Federação de Origem";
            this.gbxDadosEstados.Controls.Add(this.label4);
            this.gbxDadosEstados.Controls.Add(this.tbxIEST);
            this.gbxDadosEstados.Controls.Add(this.label1);
            this.gbxDadosEstados.Controls.Add(this.cbxZonaFranca);
            this.gbxDadosEstados.Controls.Add(this.label7);
            this.gbxDadosEstados.Controls.Add(this.label6);
            this.gbxDadosEstados.Controls.Add(this.label5);
            this.gbxDadosEstados.Controls.Add(this.label2);
            this.gbxDadosEstados.Controls.Add(this.label3);
            this.gbxDadosEstados.Controls.Add(this.label8);
            this.gbxDadosEstados.Controls.Add(this.label9);
            this.gbxDadosEstados.Controls.Add(this.label15);
            this.gbxDadosEstados.Controls.Add(this.tbxFimcep);
            this.gbxDadosEstados.Controls.Add(this.tbxInicep);
            this.gbxDadosEstados.Controls.Add(this.tbxAliquota);
            this.gbxDadosEstados.Controls.Add(this.tbxIbge);
            this.gbxDadosEstados.Controls.Add(this.tbxRegiao);
            this.gbxDadosEstados.Controls.Add(this.tbxCapital);
            this.gbxDadosEstados.Controls.Add(this.tbxNomext);
            this.gbxDadosEstados.Controls.Add(this.tbxEstado);
            this.gbxDadosEstados.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDadosEstados.ForeColor = System.Drawing.Color.DarkRed;
            this.gbxDadosEstados.Location = new System.Drawing.Point(31, 12);
            this.gbxDadosEstados.Name = "gbxDadosEstados";
            this.gbxDadosEstados.Size = new System.Drawing.Size(395, 193);
            this.gbxDadosEstados.TabIndex = 0;
            this.gbxDadosEstados.TabStop = false;
            this.gbxDadosEstados.Text = "Estado da Federação de Origem";
            this.ttpESTADOS.SetToolTip(this.gbxDadosEstados, "Grupo Estados");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(69, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 13);
            this.label4.TabIndex = 100;
            this.label4.Text = " Nº de Inscrição Estadual Substituição Tributária:";
            // 
            // tbxIEST
            // 
            this.tbxIEST.AccessibleDescription = "Até o Cep";
            this.tbxIEST.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxIEST.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxIEST.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbxIEST.Location = new System.Drawing.Point(316, 160);
            this.tbxIEST.MaxLength = 9;
            this.tbxIEST.Name = "tbxIEST";
            this.tbxIEST.Size = new System.Drawing.Size(67, 21);
            this.tbxIEST.TabIndex = 99;
            this.tbxIEST.Tag = "";
            this.ttpESTADOS.SetToolTip(this.tbxIEST, "CEP FIM");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(108, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "Zona Franca:";
            // 
            // cbxZonaFranca
            // 
            this.cbxZonaFranca.AccessibleDescription = "Zona Franca";
            this.cbxZonaFranca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZonaFranca.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxZonaFranca.FormattingEnabled = true;
            this.cbxZonaFranca.Items.AddRange(new object[] {
            "S",
            "N"});
            this.cbxZonaFranca.Location = new System.Drawing.Point(184, 132);
            this.cbxZonaFranca.Name = "cbxZonaFranca";
            this.cbxZonaFranca.Size = new System.Drawing.Size(46, 21);
            this.cbxZonaFranca.TabIndex = 8;
            this.ttpESTADOS.SetToolTip(this.cbxZonaFranca, "Zona Franca");
            this.cbxZonaFranca.Enter += new System.EventHandler(this.ControlEnter);
            this.cbxZonaFranca.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.cbxZonaFranca.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(260, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 96;
            this.label7.Text = "Até Cep:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(132, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 95;
            this.label6.Text = "Do Cep:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(290, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "% ICM:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(217, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 93;
            this.label2.Text = "IBGE:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(134, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "Região:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(89, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 91;
            this.label8.Text = "Nome da Capital:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(10, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 90;
            this.label9.Text = "UF:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(89, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 89;
            this.label15.Text = "Nome do Estado:";
            // 
            // tbxFimcep
            // 
            this.tbxFimcep.AccessibleDescription = "Até o Cep";
            this.tbxFimcep.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxFimcep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxFimcep.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbxFimcep.Location = new System.Drawing.Point(316, 105);
            this.tbxFimcep.MaxLength = 9;
            this.tbxFimcep.Name = "tbxFimcep";
            this.tbxFimcep.Size = new System.Drawing.Size(67, 21);
            this.tbxFimcep.TabIndex = 7;
            this.tbxFimcep.Tag = "";
            this.ttpESTADOS.SetToolTip(this.tbxFimcep, "CEP FIM");
            this.tbxFimcep.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxFimcep.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxFimcep.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxInicep
            // 
            this.tbxInicep.AccessibleDescription = "Do Cep";
            this.tbxInicep.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxInicep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxInicep.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbxInicep.Location = new System.Drawing.Point(184, 105);
            this.tbxInicep.MaxLength = 9;
            this.tbxInicep.Name = "tbxInicep";
            this.tbxInicep.Size = new System.Drawing.Size(67, 21);
            this.tbxInicep.TabIndex = 6;
            this.tbxInicep.Tag = "";
            this.ttpESTADOS.SetToolTip(this.tbxInicep, "CEP INICIO");
            this.tbxInicep.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxInicep.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxInicep.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxAliquota
            // 
            this.tbxAliquota.AccessibleDescription = "% ICM";
            this.tbxAliquota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxAliquota.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAliquota.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbxAliquota.Location = new System.Drawing.Point(340, 78);
            this.tbxAliquota.Name = "tbxAliquota";
            this.tbxAliquota.Size = new System.Drawing.Size(43, 21);
            this.tbxAliquota.TabIndex = 5;
            this.tbxAliquota.Tag = "N2";
            this.tbxAliquota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttpESTADOS.SetToolTip(this.tbxAliquota, "ALIQUOTA");
            this.tbxAliquota.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxAliquota.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxAliquota.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlKeyKeyPress);
            this.tbxAliquota.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxIbge
            // 
            this.tbxIbge.AccessibleDescription = "IBGE";
            this.tbxIbge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxIbge.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxIbge.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbxIbge.Location = new System.Drawing.Point(257, 78);
            this.tbxIbge.MaxLength = 2;
            this.tbxIbge.Name = "tbxIbge";
            this.tbxIbge.Size = new System.Drawing.Size(27, 21);
            this.tbxIbge.TabIndex = 4;
            this.ttpESTADOS.SetToolTip(this.tbxIbge, "IBGE");
            this.tbxIbge.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxIbge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxIbge.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxRegiao
            // 
            this.tbxRegiao.AccessibleDescription = "Região";
            this.tbxRegiao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxRegiao.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxRegiao.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbxRegiao.Location = new System.Drawing.Point(184, 78);
            this.tbxRegiao.MaxLength = 2;
            this.tbxRegiao.Name = "tbxRegiao";
            this.tbxRegiao.Size = new System.Drawing.Size(27, 21);
            this.tbxRegiao.TabIndex = 3;
            this.ttpESTADOS.SetToolTip(this.tbxRegiao, "REGIÃO");
            this.tbxRegiao.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxRegiao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxRegiao.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxCapital
            // 
            this.tbxCapital.AccessibleDescription = "Nome da Capital";
            this.tbxCapital.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCapital.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCapital.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbxCapital.Location = new System.Drawing.Point(184, 51);
            this.tbxCapital.MaxLength = 20;
            this.tbxCapital.Name = "tbxCapital";
            this.tbxCapital.Size = new System.Drawing.Size(199, 21);
            this.tbxCapital.TabIndex = 2;
            this.ttpESTADOS.SetToolTip(this.tbxCapital, "CAPITAL");
            this.tbxCapital.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCapital.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCapital.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxNomext
            // 
            this.tbxNomext.AccessibleDescription = "Nome do Estado";
            this.tbxNomext.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNomext.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNomext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbxNomext.Location = new System.Drawing.Point(184, 24);
            this.tbxNomext.MaxLength = 20;
            this.tbxNomext.Name = "tbxNomext";
            this.tbxNomext.Size = new System.Drawing.Size(199, 21);
            this.tbxNomext.TabIndex = 1;
            this.ttpESTADOS.SetToolTip(this.tbxNomext, "NomeExt");
            this.tbxNomext.TextChanged += new System.EventHandler(this.tbxNomext_TextChanged);
            this.tbxNomext.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxNomext.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNomext.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxEstado
            // 
            this.tbxEstado.AccessibleDescription = "UF";
            this.tbxEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxEstado.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxEstado.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbxEstado.Location = new System.Drawing.Point(40, 24);
            this.tbxEstado.MaxLength = 2;
            this.tbxEstado.Name = "tbxEstado";
            this.tbxEstado.Size = new System.Drawing.Size(38, 21);
            this.tbxEstado.TabIndex = 0;
            this.ttpESTADOS.SetToolTip(this.tbxEstado, "Uf");
            this.tbxEstado.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxEstado.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // gbxESTADOSICMS
            // 
            this.gbxESTADOSICMS.AccessibleDescription = "Visualizando os Estados da Federação de Destino";
            this.gbxESTADOSICMS.Controls.Add(this.pbxESTADOSICMS);
            this.gbxESTADOSICMS.Controls.Add(this.tspESTADOSICMS);
            this.gbxESTADOSICMS.Controls.Add(this.dgvESTADOSICMS);
            this.gbxESTADOSICMS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxESTADOSICMS.ForeColor = System.Drawing.Color.DarkRed;
            this.gbxESTADOSICMS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gbxESTADOSICMS.Location = new System.Drawing.Point(478, 12);
            this.gbxESTADOSICMS.Name = "gbxESTADOSICMS";
            this.gbxESTADOSICMS.Size = new System.Drawing.Size(329, 403);
            this.gbxESTADOSICMS.TabIndex = 1;
            this.gbxESTADOSICMS.TabStop = false;
            this.gbxESTADOSICMS.Text = "Visualizando os Estados da Federação de Destino";
            this.ttpESTADOS.SetToolTip(this.gbxESTADOSICMS, "Grupo ESTADOSICMS");
            // 
            // pbxESTADOSICMS
            // 
            this.pbxESTADOSICMS.AccessibleDescription = "Carregando Estados";
            this.pbxESTADOSICMS.BackColor = System.Drawing.Color.White;
            this.pbxESTADOSICMS.Image = ((System.Drawing.Image)(resources.GetObject("pbxESTADOSICMS.Image")));
            this.pbxESTADOSICMS.Location = new System.Drawing.Point(36, 32);
            this.pbxESTADOSICMS.Name = "pbxESTADOSICMS";
            this.pbxESTADOSICMS.Size = new System.Drawing.Size(248, 335);
            this.pbxESTADOSICMS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxESTADOSICMS.TabIndex = 139;
            this.pbxESTADOSICMS.TabStop = false;
            this.ttpESTADOS.SetToolTip(this.pbxESTADOSICMS, "Imagem");
            this.pbxESTADOSICMS.Visible = false;
            // 
            // tspESTADOSICMS
            // 
            this.tspESTADOSICMS.AccessibleDescription = "Barra de Opções";
            this.tspESTADOSICMS.AllowMerge = false;
            this.tspESTADOSICMS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspESTADOSICMS.AutoSize = false;
            this.tspESTADOSICMS.Dock = System.Windows.Forms.DockStyle.None;
            this.tspESTADOSICMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspIncluir,
            this.tspAlterar,
            this.tspExcluir});
            this.tspESTADOSICMS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspESTADOSICMS.Location = new System.Drawing.Point(9, 370);
            this.tspESTADOSICMS.Name = "tspESTADOSICMS";
            this.tspESTADOSICMS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspESTADOSICMS.Size = new System.Drawing.Size(313, 30);
            this.tspESTADOSICMS.TabIndex = 1;
            this.ttpESTADOS.SetToolTip(this.tspESTADOSICMS, "Menu2");
            // 
            // tspIncluir
            // 
            this.tspIncluir.AccessibleDescription = "Incluir";
            this.tspIncluir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspIncluir.ForeColor = System.Drawing.Color.Black;
            this.tspIncluir.Image = ((System.Drawing.Image)(resources.GetObject("tspIncluir.Image")));
            this.tspIncluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspIncluir.Name = "tspIncluir";
            this.tspIncluir.Size = new System.Drawing.Size(42, 31);
            this.tspIncluir.Text = "Incluir";
            this.tspIncluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspIncluir.ToolTipText = "Incluir  Transporte";
            this.tspIncluir.Click += new System.EventHandler(this.tspIncluir_Click);
            // 
            // tspAlterar
            // 
            this.tspAlterar.AccessibleDescription = "Alterar";
            this.tspAlterar.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAlterar.ForeColor = System.Drawing.Color.Black;
            this.tspAlterar.Image = ((System.Drawing.Image)(resources.GetObject("tspAlterar.Image")));
            this.tspAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAlterar.Name = "tspAlterar";
            this.tspAlterar.Size = new System.Drawing.Size(46, 31);
            this.tspAlterar.Text = "Alterar";
            this.tspAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAlterar.ToolTipText = "Alterar  Transporte";
            this.tspAlterar.Click += new System.EventHandler(this.tspAlterar_Click);
            // 
            // tspExcluir
            // 
            this.tspExcluir.AccessibleDescription = "Excluir";
            this.tspExcluir.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspExcluir.ForeColor = System.Drawing.Color.Black;
            this.tspExcluir.Image = ((System.Drawing.Image)(resources.GetObject("tspExcluir.Image")));
            this.tspExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspExcluir.Name = "tspExcluir";
            this.tspExcluir.Size = new System.Drawing.Size(43, 31);
            this.tspExcluir.Text = "Excluir";
            this.tspExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspExcluir.ToolTipText = "Excluir  Transporte";
            this.tspExcluir.Click += new System.EventHandler(this.tspExcluir_Click);
            // 
            // dgvESTADOSICMS
            // 
            this.dgvESTADOSICMS.AccessibleDescription = "Estados";
            this.dgvESTADOSICMS.AccessibleName = "";
            this.dgvESTADOSICMS.AllowUserToAddRows = false;
            this.dgvESTADOSICMS.AllowUserToDeleteRows = false;
            this.dgvESTADOSICMS.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvESTADOSICMS.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvESTADOSICMS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvESTADOSICMS.BackgroundColor = System.Drawing.Color.White;
            this.dgvESTADOSICMS.ColumnHeadersHeight = 31;
            this.dgvESTADOSICMS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.IDESTADO,
            this.IDESTADODESTINO,
            this.UF,
            this.ALIQUOTA,
            this.IVA,
            this.IDDIZERESNFV,
            this.TEMP});
            this.dgvESTADOSICMS.Location = new System.Drawing.Point(48, 32);
            this.dgvESTADOSICMS.MultiSelect = false;
            this.dgvESTADOSICMS.Name = "dgvESTADOSICMS";
            this.dgvESTADOSICMS.ReadOnly = true;
            this.dgvESTADOSICMS.RowHeadersVisible = false;
            this.dgvESTADOSICMS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvESTADOSICMS.Size = new System.Drawing.Size(236, 331);
            this.dgvESTADOSICMS.TabIndex = 0;
            this.dgvESTADOSICMS.TabStop = false;
            this.ttpESTADOS.SetToolTip(this.dgvESTADOSICMS, "Grade de visualização CNAE UF");
            this.dgvESTADOSICMS.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvESTADOSICMS_CellDoubleClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 5;
            // 
            // IDESTADO
            // 
            this.IDESTADO.DataPropertyName = "IDESTADO";
            this.IDESTADO.HeaderText = "IDESTADO";
            this.IDESTADO.Name = "IDESTADO";
            this.IDESTADO.ReadOnly = true;
            this.IDESTADO.Visible = false;
            // 
            // IDESTADODESTINO
            // 
            this.IDESTADODESTINO.DataPropertyName = "IDESTADODESTINO";
            this.IDESTADODESTINO.HeaderText = "IDESTADODESTINO";
            this.IDESTADODESTINO.Name = "IDESTADODESTINO";
            this.IDESTADODESTINO.ReadOnly = true;
            this.IDESTADODESTINO.Visible = false;
            // 
            // UF
            // 
            this.UF.DataPropertyName = "UF";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UF.DefaultCellStyle = dataGridViewCellStyle7;
            this.UF.HeaderText = "UF";
            this.UF.Name = "UF";
            this.UF.ReadOnly = true;
            this.UF.Width = 35;
            // 
            // ALIQUOTA
            // 
            this.ALIQUOTA.DataPropertyName = "ALIQUOTA";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = "0";
            this.ALIQUOTA.DefaultCellStyle = dataGridViewCellStyle8;
            this.ALIQUOTA.HeaderText = "Alíquota";
            this.ALIQUOTA.Name = "ALIQUOTA";
            this.ALIQUOTA.ReadOnly = true;
            this.ALIQUOTA.Width = 70;
            // 
            // IVA
            // 
            this.IVA.DataPropertyName = "IVA";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = "0";
            this.IVA.DefaultCellStyle = dataGridViewCellStyle9;
            this.IVA.HeaderText = "IVA";
            this.IVA.Name = "IVA";
            this.IVA.ReadOnly = true;
            // 
            // IDDIZERESNFV
            // 
            this.IDDIZERESNFV.DataPropertyName = "IDDIZERESNFV";
            dataGridViewCellStyle10.NullValue = "0";
            this.IDDIZERESNFV.DefaultCellStyle = dataGridViewCellStyle10;
            this.IDDIZERESNFV.HeaderText = "IDDIZERESNFV";
            this.IDDIZERESNFV.Name = "IDDIZERESNFV";
            this.IDDIZERESNFV.ReadOnly = true;
            this.IDDIZERESNFV.Visible = false;
            // 
            // TEMP
            // 
            this.TEMP.DataPropertyName = "TEMP";
            this.TEMP.HeaderText = "TEMP";
            this.TEMP.Name = "TEMP";
            this.TEMP.ReadOnly = true;
            this.TEMP.Visible = false;
            // 
            // bwrESTADOSICMS
            // 
            this.bwrESTADOSICMS.WorkerSupportsCancellation = true;
            this.bwrESTADOSICMS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrESTADOSICMS_DoWork);
            this.bwrESTADOSICMS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrESTADOSICMS_RunWorkerCompleted);
            // 
            // frmEstados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.gbxDadosEstados);
            this.Controls.Add(this.gbxESTADOSICMS);
            this.Controls.Add(this.tspESTADOS);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEstados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "4014";
            this.Text = "Estados - Registrando";
            this.ttpESTADOS.SetToolTip(this, "Estados - Registrando");
            this.Activated += new System.EventHandler(this.frmEstados_Activated);
            this.Load += new System.EventHandler(this.frmEstados_Load);
            this.Shown += new System.EventHandler(this.frmEstados_Shown);
            this.tspESTADOS.ResumeLayout(false);
            this.tspESTADOS.PerformLayout();
            this.gbxDadosEstados.ResumeLayout(false);
            this.gbxDadosEstados.PerformLayout();
            this.gbxESTADOSICMS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxESTADOSICMS)).EndInit();
            this.tspESTADOSICMS.ResumeLayout(false);
            this.tspESTADOSICMS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvESTADOSICMS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttpESTADOS;
        private System.Windows.Forms.ToolStrip tspESTADOS;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox gbxDadosEstados;
        private System.Windows.Forms.GroupBox gbxESTADOSICMS;
        private System.Windows.Forms.ToolStrip tspESTADOSICMS;
        private System.Windows.Forms.ToolStripButton tspIncluir;
        private System.Windows.Forms.ToolStripButton tspAlterar;
        private System.Windows.Forms.ToolStripButton tspExcluir;
        private System.Windows.Forms.DataGridView dgvESTADOSICMS;
        private System.ComponentModel.BackgroundWorker bwrESTADOSICMS;
        private System.Windows.Forms.PictureBox pbxESTADOSICMS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbxFimcep;
        private System.Windows.Forms.TextBox tbxInicep;
        private System.Windows.Forms.TextBox tbxAliquota;
        private System.Windows.Forms.TextBox tbxIbge;
        private System.Windows.Forms.TextBox tbxRegiao;
        private System.Windows.Forms.TextBox tbxCapital;
        private System.Windows.Forms.TextBox tbxNomext;
        private System.Windows.Forms.TextBox tbxEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDESTADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDESTADODESTINO;
        private System.Windows.Forms.DataGridViewTextBoxColumn UF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALIQUOTA;
        private System.Windows.Forms.DataGridViewTextBoxColumn IVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDDIZERESNFV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEMP;
        private System.Windows.Forms.ComboBox cbxZonaFranca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxIEST;
    }
}