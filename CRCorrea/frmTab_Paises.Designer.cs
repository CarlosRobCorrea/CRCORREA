namespace CRCorrea
{
    partial class frmTab_Paises
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTab_Paises));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbxBacen = new System.Windows.Forms.TextBox();
            this.tbxArea = new System.Windows.Forms.TextBox();
            this.tbxNomeInt = new System.Windows.Forms.TextBox();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.tbxUF = new System.Windows.Forms.TextBox();
            this.tbxDDD = new System.Windows.Forms.TextBox();
            this.tbxDDDDireto = new System.Windows.Forms.TextBox();
            this.tbxISO3166A = new System.Windows.Forms.TextBox();
            this.tbxISO3166B = new System.Windows.Forms.TextBox();
            this.tbxISO3166C = new System.Windows.Forms.TextBox();
            this.tbxPerimetro = new System.Windows.Forms.TextBox();
            this.tbxContinente = new System.Windows.Forms.TextBox();
            this.tbxComex = new System.Windows.Forms.TextBox();
            this.gbxUf = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbxAnoFundacao = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.gbxUf.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxBacen
            // 
            this.tbxBacen.AccessibleDescription = "BACEN";
            this.tbxBacen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxBacen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBacen.Location = new System.Drawing.Point(40, 166);
            this.tbxBacen.MaxLength = 4;
            this.tbxBacen.Name = "tbxBacen";
            this.tbxBacen.Size = new System.Drawing.Size(41, 21);
            this.tbxBacen.TabIndex = 8;
            this.tbxBacen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.tbxBacen, "Código BACEN");
            this.tbxBacen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxBacen.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxBacen.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxArea
            // 
            this.tbxArea.AccessibleDescription = "Área";
            this.tbxArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxArea.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxArea.Location = new System.Drawing.Point(100, 166);
            this.tbxArea.MaxLength = 3;
            this.tbxArea.Name = "tbxArea";
            this.tbxArea.Size = new System.Drawing.Size(40, 21);
            this.tbxArea.TabIndex = 9;
            this.tbxArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.tbxArea, "Sigla do País");
            this.tbxArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxArea.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxArea.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxNomeInt
            // 
            this.tbxNomeInt.AccessibleDescription = "Nome Inteiro do País";
            this.tbxNomeInt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNomeInt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNomeInt.Location = new System.Drawing.Point(40, 120);
            this.tbxNomeInt.MaxLength = 20;
            this.tbxNomeInt.Name = "tbxNomeInt";
            this.tbxNomeInt.Size = new System.Drawing.Size(453, 21);
            this.tbxNomeInt.TabIndex = 7;
            this.toolTip1.SetToolTip(this.tbxNomeInt, "Nome da Capital do Pais");
            this.tbxNomeInt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNomeInt.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxNomeInt.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Nome do País";
            this.tbxNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(40, 33);
            this.tbxNome.MaxLength = 20;
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(453, 21);
            this.tbxNome.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbxNome, "Nome do País");
            this.tbxNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxNome.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxNome.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxUF
            // 
            this.tbxUF.AccessibleDescription = "UF";
            this.tbxUF.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxUF.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUF.Location = new System.Drawing.Point(6, 33);
            this.tbxUF.MaxLength = 2;
            this.tbxUF.Name = "tbxUF";
            this.tbxUF.Size = new System.Drawing.Size(28, 21);
            this.tbxUF.TabIndex = 0;
            this.tbxUF.Text = "XX";
            this.toolTip1.SetToolTip(this.tbxUF, "Unidade Federal");
            this.tbxUF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxUF.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxUF.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxDDD
            // 
            this.tbxDDD.AccessibleDescription = "DDD";
            this.tbxDDD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDDD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDDD.Location = new System.Drawing.Point(40, 74);
            this.tbxDDD.MaxLength = 2;
            this.tbxDDD.Name = "tbxDDD";
            this.tbxDDD.Size = new System.Drawing.Size(28, 21);
            this.tbxDDD.TabIndex = 2;
            this.tbxDDD.Text = "XX";
            this.tbxDDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxDDD, "Unidade Federal");
            this.tbxDDD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDDD.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxDDD.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxDDDDireto
            // 
            this.tbxDDDDireto.AccessibleDescription = "DDD Direto";
            this.tbxDDDDireto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDDDDireto.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDDDDireto.Location = new System.Drawing.Point(95, 74);
            this.tbxDDDDireto.MaxLength = 2;
            this.tbxDDDDireto.Name = "tbxDDDDireto";
            this.tbxDDDDireto.Size = new System.Drawing.Size(28, 21);
            this.tbxDDDDireto.TabIndex = 3;
            this.tbxDDDDireto.Text = "XX";
            this.tbxDDDDireto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.tbxDDDDireto, "Unidade Federal");
            this.tbxDDDDireto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDDDDireto.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxDDDDireto.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxISO3166A
            // 
            this.tbxISO3166A.AccessibleDescription = "ISO 3166 A";
            this.tbxISO3166A.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxISO3166A.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxISO3166A.Location = new System.Drawing.Point(160, 74);
            this.tbxISO3166A.MaxLength = 2;
            this.tbxISO3166A.Name = "tbxISO3166A";
            this.tbxISO3166A.Size = new System.Drawing.Size(59, 21);
            this.tbxISO3166A.TabIndex = 4;
            this.tbxISO3166A.Text = "XX";
            this.toolTip1.SetToolTip(this.tbxISO3166A, "Unidade Federal");
            this.tbxISO3166A.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxISO3166A.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxISO3166A.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxISO3166B
            // 
            this.tbxISO3166B.AccessibleDescription = "ISO  3166 B";
            this.tbxISO3166B.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxISO3166B.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxISO3166B.Location = new System.Drawing.Point(243, 74);
            this.tbxISO3166B.MaxLength = 2;
            this.tbxISO3166B.Name = "tbxISO3166B";
            this.tbxISO3166B.Size = new System.Drawing.Size(59, 21);
            this.tbxISO3166B.TabIndex = 5;
            this.tbxISO3166B.Text = "XX";
            this.toolTip1.SetToolTip(this.tbxISO3166B, "Unidade Federal");
            this.tbxISO3166B.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxISO3166B.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxISO3166B.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxISO3166C
            // 
            this.tbxISO3166C.AccessibleDescription = "ISO 3166 C";
            this.tbxISO3166C.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxISO3166C.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxISO3166C.Location = new System.Drawing.Point(327, 74);
            this.tbxISO3166C.MaxLength = 2;
            this.tbxISO3166C.Name = "tbxISO3166C";
            this.tbxISO3166C.Size = new System.Drawing.Size(59, 21);
            this.tbxISO3166C.TabIndex = 6;
            this.tbxISO3166C.Text = "XX";
            this.toolTip1.SetToolTip(this.tbxISO3166C, "Unidade Federal");
            this.tbxISO3166C.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxISO3166C.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxISO3166C.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxPerimetro
            // 
            this.tbxPerimetro.AccessibleDescription = "Perimetro";
            this.tbxPerimetro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxPerimetro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPerimetro.Location = new System.Drawing.Point(151, 166);
            this.tbxPerimetro.MaxLength = 3;
            this.tbxPerimetro.Name = "tbxPerimetro";
            this.tbxPerimetro.Size = new System.Drawing.Size(70, 21);
            this.tbxPerimetro.TabIndex = 10;
            this.tbxPerimetro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.tbxPerimetro, "Sigla do País");
            this.tbxPerimetro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxPerimetro.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxPerimetro.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxContinente
            // 
            this.tbxContinente.AccessibleDescription = "Continente";
            this.tbxContinente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxContinente.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxContinente.Location = new System.Drawing.Point(289, 166);
            this.tbxContinente.MaxLength = 3;
            this.tbxContinente.Name = "tbxContinente";
            this.tbxContinente.Size = new System.Drawing.Size(150, 21);
            this.tbxContinente.TabIndex = 12;
            this.tbxContinente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.tbxContinente, "Sigla do País");
            this.tbxContinente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxContinente.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxContinente.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxComex
            // 
            this.tbxComex.AccessibleDescription = "COMEX";
            this.tbxComex.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxComex.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxComex.Location = new System.Drawing.Point(452, 166);
            this.tbxComex.MaxLength = 4;
            this.tbxComex.Name = "tbxComex";
            this.tbxComex.Size = new System.Drawing.Size(41, 21);
            this.tbxComex.TabIndex = 13;
            this.tbxComex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.tbxComex, "Código BACEN");
            this.tbxComex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxComex.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxComex.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // gbxUf
            // 
            this.gbxUf.AccessibleDescription = "Paises";
            this.gbxUf.Controls.Add(this.label13);
            this.gbxUf.Controls.Add(this.tbxAnoFundacao);
            this.gbxUf.Controls.Add(this.label12);
            this.gbxUf.Controls.Add(this.tbxComex);
            this.gbxUf.Controls.Add(this.label11);
            this.gbxUf.Controls.Add(this.tbxContinente);
            this.gbxUf.Controls.Add(this.label10);
            this.gbxUf.Controls.Add(this.tbxPerimetro);
            this.gbxUf.Controls.Add(this.label9);
            this.gbxUf.Controls.Add(this.tbxISO3166C);
            this.gbxUf.Controls.Add(this.label8);
            this.gbxUf.Controls.Add(this.tbxISO3166B);
            this.gbxUf.Controls.Add(this.label7);
            this.gbxUf.Controls.Add(this.tbxISO3166A);
            this.gbxUf.Controls.Add(this.label6);
            this.gbxUf.Controls.Add(this.tbxDDDDireto);
            this.gbxUf.Controls.Add(this.label5);
            this.gbxUf.Controls.Add(this.tbxDDD);
            this.gbxUf.Controls.Add(this.label1);
            this.gbxUf.Controls.Add(this.tbxUF);
            this.gbxUf.Controls.Add(this.label4);
            this.gbxUf.Controls.Add(this.label3);
            this.gbxUf.Controls.Add(this.label2);
            this.gbxUf.Controls.Add(this.label15);
            this.gbxUf.Controls.Add(this.tbxBacen);
            this.gbxUf.Controls.Add(this.tbxArea);
            this.gbxUf.Controls.Add(this.tbxNomeInt);
            this.gbxUf.Controls.Add(this.tbxNome);
            this.gbxUf.Location = new System.Drawing.Point(259, 129);
            this.gbxUf.Name = "gbxUf";
            this.gbxUf.Size = new System.Drawing.Size(526, 227);
            this.gbxUf.TabIndex = 0;
            this.gbxUf.TabStop = false;
            this.toolTip1.SetToolTip(this.gbxUf, "Registrando Pais");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(233, 150);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 13);
            this.label13.TabIndex = 97;
            this.label13.Text = "Ano Fund";
            // 
            // tbxAnoFundacao
            // 
            this.tbxAnoFundacao.AccessibleDescription = "Ano Fundação";
            this.tbxAnoFundacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxAnoFundacao.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAnoFundacao.Location = new System.Drawing.Point(240, 166);
            this.tbxAnoFundacao.MaxLength = 3;
            this.tbxAnoFundacao.Name = "tbxAnoFundacao";
            this.tbxAnoFundacao.Size = new System.Drawing.Size(40, 21);
            this.tbxAnoFundacao.TabIndex = 11;
            this.tbxAnoFundacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.tbxAnoFundacao, "Sigla do País");
            this.tbxAnoFundacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxAnoFundacao.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxAnoFundacao.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(452, 150);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 13);
            this.label12.TabIndex = 95;
            this.label12.Text = "COMEX";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(299, 150);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 93;
            this.label11.Text = "Continente";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(161, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 91;
            this.label10.Text = "Perimetro";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(324, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 89;
            this.label9.Text = "ISO 3166 C";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(240, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 87;
            this.label8.Text = "ISO 3166 B";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(157, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 85;
            this.label7.Text = "ISO 3166 A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(83, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 83;
            this.label6.Text = "DDD Direto";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 81;
            this.label5.Text = "DDD";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "UF:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "BACEN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "Area";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "Nome Interno do País :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(40, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 13);
            this.label15.TabIndex = 73;
            this.label15.Text = "Nome do País:";
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(0, 640);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(1020, 45);
            this.tspTool.TabIndex = 80;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspSalvar.Image")));
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(66, 42);
            this.tspSalvar.Text = "&Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspPrimeiro
            // 
            this.tspPrimeiro.AccessibleDescription = "Primeiro";
            this.tspPrimeiro.AutoSize = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = ((System.Drawing.Image)(resources.GetObject("tspPrimeiro.Image")));
            this.tspPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Size = new System.Drawing.Size(66, 42);
            this.tspPrimeiro.Text = "Pr&imeiro";
            this.tspPrimeiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspPrimeiro.Click += new System.EventHandler(this.tspPrimeiro_Click);
            // 
            // tspAnterior
            // 
            this.tspAnterior.AccessibleDescription = "Anterior";
            this.tspAnterior.AutoSize = false;
            this.tspAnterior.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAnterior.Image = ((System.Drawing.Image)(resources.GetObject("tspAnterior.Image")));
            this.tspAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAnterior.Name = "tspAnterior";
            this.tspAnterior.Size = new System.Drawing.Size(66, 42);
            this.tspAnterior.Text = "&Anterior";
            this.tspAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspAnterior.Click += new System.EventHandler(this.tspAnterior_Click);
            // 
            // tspProximo
            // 
            this.tspProximo.AccessibleDescription = "Próximo";
            this.tspProximo.AutoSize = false;
            this.tspProximo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspProximo.Image = ((System.Drawing.Image)(resources.GetObject("tspProximo.Image")));
            this.tspProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspProximo.Name = "tspProximo";
            this.tspProximo.Size = new System.Drawing.Size(66, 42);
            this.tspProximo.Text = "Pró&ximo";
            this.tspProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspProximo.Click += new System.EventHandler(this.tspProximo_Click);
            // 
            // tspUltimo
            // 
            this.tspUltimo.AccessibleDescription = "Último";
            this.tspUltimo.AutoSize = false;
            this.tspUltimo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUltimo.Image = ((System.Drawing.Image)(resources.GetObject("tspUltimo.Image")));
            this.tspUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Size = new System.Drawing.Size(66, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspUltimo.Click += new System.EventHandler(this.tspUltimo_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // frmTab_Paises
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.gbxUf);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTab_Paises";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "26";
            this.Text = "Paises - Cadastro";
            this.toolTip1.SetToolTip(this, "Paises - Registrando");
            this.Load += new System.EventHandler(this.frmPaises_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPaises_FormClosed);
            this.gbxUf.ResumeLayout(false);
            this.gbxUf.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbxUf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbxBacen;
        private System.Windows.Forms.TextBox tbxArea;
        private System.Windows.Forms.TextBox tbxNomeInt;
        private System.Windows.Forms.TextBox tbxNome;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxUF;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxDDDDireto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxDDD;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxPerimetro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxISO3166C;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxISO3166B;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxISO3166A;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxComex;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxContinente;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbxAnoFundacao;
    }
}