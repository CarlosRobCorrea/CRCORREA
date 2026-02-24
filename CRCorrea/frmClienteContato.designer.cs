namespace CRCorrea
{
    partial class frmClienteContato
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxClienteprospeccao_numero = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxClienteprospeccao_cognome = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxSetor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxContato = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbxTipocontato = new System.Windows.Forms.ListBox();
            this.tbxTipoContato = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tbxObserva = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxCelular = new System.Windows.Forms.TextBox();
            this.tbxDddcelular = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxRamalfax = new System.Windows.Forms.TextBox();
            this.tbxTelefax = new System.Windows.Forms.TextBox();
            this.tbxDddfax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxRamal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxTelefone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxDdd = new System.Windows.Forms.TextBox();
            this.tspTool2 = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.ttpContato = new System.Windows.Forms.ToolTip(this.components);
            this.tbxEmail = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tspTool2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Cadastro";
            this.groupBox1.Controls.Add(this.tbxClienteprospeccao_numero);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxClienteprospeccao_cognome);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.ttpContato.SetToolTip(this.groupBox1, "Grupo - Cabeçalho");
            // 
            // tbxClienteprospeccao_numero
            // 
            this.tbxClienteprospeccao_numero.AccessibleDescription = "Número do Cadastro";
            this.tbxClienteprospeccao_numero.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxClienteprospeccao_numero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxClienteprospeccao_numero.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxClienteprospeccao_numero.Location = new System.Drawing.Point(15, 33);
            this.tbxClienteprospeccao_numero.Name = "tbxClienteprospeccao_numero";
            this.tbxClienteprospeccao_numero.ReadOnly = true;
            this.tbxClienteprospeccao_numero.Size = new System.Drawing.Size(61, 21);
            this.tbxClienteprospeccao_numero.TabIndex = 0;
            this.tbxClienteprospeccao_numero.TabStop = false;
            this.tbxClienteprospeccao_numero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttpContato.SetToolTip(this.tbxClienteprospeccao_numero, "Nº Cadastro");
            this.tbxClienteprospeccao_numero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxClienteprospeccao_numero.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxClienteprospeccao_numero.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Nº Cadastro:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Cognome:";
            // 
            // tbxClienteprospeccao_cognome
            // 
            this.tbxClienteprospeccao_cognome.AccessibleDescription = "Cognome Cliente";
            this.tbxClienteprospeccao_cognome.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxClienteprospeccao_cognome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxClienteprospeccao_cognome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxClienteprospeccao_cognome.Location = new System.Drawing.Point(82, 33);
            this.tbxClienteprospeccao_cognome.Name = "tbxClienteprospeccao_cognome";
            this.tbxClienteprospeccao_cognome.ReadOnly = true;
            this.tbxClienteprospeccao_cognome.Size = new System.Drawing.Size(173, 21);
            this.tbxClienteprospeccao_cognome.TabIndex = 1;
            this.tbxClienteprospeccao_cognome.TabStop = false;
            this.ttpContato.SetToolTip(this.tbxClienteprospeccao_cognome, "Cognome");
            this.tbxClienteprospeccao_cognome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxClienteprospeccao_cognome.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxClienteprospeccao_cognome.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // groupBox4
            // 
            this.groupBox4.AccessibleDescription = "Contato";
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.tbxEmail);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.tbxSetor);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.tbxContato);
            this.groupBox4.Location = new System.Drawing.Point(5, 102);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(311, 104);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.ttpContato.SetToolTip(this.groupBox4, "Grupo - Contato");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "E-mail:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Depto / Setor:";
            // 
            // tbxSetor
            // 
            this.tbxSetor.AccessibleDescription = "Departamento / Setor";
            this.tbxSetor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxSetor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSetor.Location = new System.Drawing.Point(89, 47);
            this.tbxSetor.Name = "tbxSetor";
            this.tbxSetor.Size = new System.Drawing.Size(216, 21);
            this.tbxSetor.TabIndex = 1;
            this.ttpContato.SetToolTip(this.tbxSetor, "Depto/ Setor");
            this.tbxSetor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxSetor.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxSetor.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Nome Contato:";
            // 
            // tbxContato
            // 
            this.tbxContato.AccessibleDescription = "Nome do Contato";
            this.tbxContato.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxContato.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxContato.Location = new System.Drawing.Point(89, 20);
            this.tbxContato.Name = "tbxContato";
            this.tbxContato.Size = new System.Drawing.Size(216, 21);
            this.tbxContato.TabIndex = 0;
            this.ttpContato.SetToolTip(this.tbxContato, "Nome do Contato");
            this.tbxContato.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxContato.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxContato.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // groupBox5
            // 
            this.groupBox5.AccessibleDescription = "Tipo do Contato";
            this.groupBox5.Controls.Add(this.lbxTipocontato);
            this.groupBox5.Controls.Add(this.tbxTipoContato);
            this.groupBox5.Location = new System.Drawing.Point(279, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(261, 82);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo do Contato";
            this.ttpContato.SetToolTip(this.groupBox5, "Grupo - Tipo do Contato");
            // 
            // lbxTipocontato
            // 
            this.lbxTipocontato.AccessibleDescription = "";
            this.lbxTipocontato.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxTipocontato.FormattingEnabled = true;
            this.lbxTipocontato.Items.AddRange(new object[] {
            "1 - PADRÃO",
            "2 - COMPRADOR",
            "3 - VENDEDOR",
            "4 - OUTROS"});
            this.lbxTipocontato.Location = new System.Drawing.Point(6, 20);
            this.lbxTipocontato.Name = "lbxTipocontato";
            this.lbxTipocontato.Size = new System.Drawing.Size(106, 56);
            this.lbxTipocontato.TabIndex = 0;
            this.lbxTipocontato.TabStop = false;
            this.ttpContato.SetToolTip(this.lbxTipocontato, "Lista - Contato");
            this.lbxTipocontato.SelectedIndexChanged += new System.EventHandler(this.lbxTipocontato_SelectedIndexChanged);
            this.lbxTipocontato.Leave += new System.EventHandler(this.ControlLeave);
            this.lbxTipocontato.Enter += new System.EventHandler(this.ControlEnter);
            this.lbxTipocontato.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            // 
            // tbxTipoContato
            // 
            this.tbxTipoContato.AccessibleDescription = "Tipo de Contato";
            this.tbxTipoContato.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxTipoContato.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTipoContato.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTipoContato.Location = new System.Drawing.Point(118, 55);
            this.tbxTipoContato.Name = "tbxTipoContato";
            this.tbxTipoContato.ReadOnly = true;
            this.tbxTipoContato.Size = new System.Drawing.Size(132, 21);
            this.tbxTipoContato.TabIndex = 1;
            this.tbxTipoContato.TabStop = false;
            this.ttpContato.SetToolTip(this.tbxTipoContato, "Tipo de Contato");
            this.tbxTipoContato.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxTipoContato.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxTipoContato.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // groupBox6
            // 
            this.groupBox6.AccessibleDescription = "Observação";
            this.groupBox6.Controls.Add(this.tbxObserva);
            this.groupBox6.Location = new System.Drawing.Point(5, 214);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(536, 56);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Observação";
            this.ttpContato.SetToolTip(this.groupBox6, "Grupo - Observação");
            // 
            // tbxObserva
            // 
            this.tbxObserva.AccessibleDescription = "Observação";
            this.tbxObserva.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxObserva.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxObserva.Location = new System.Drawing.Point(6, 20);
            this.tbxObserva.MaxLength = 50;
            this.tbxObserva.Name = "tbxObserva";
            this.tbxObserva.Size = new System.Drawing.Size(526, 21);
            this.tbxObserva.TabIndex = 0;
            this.ttpContato.SetToolTip(this.tbxObserva, "Observaçao do Contato");
            this.tbxObserva.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxObserva.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxObserva.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // groupBox7
            // 
            this.groupBox7.AccessibleDescription = "Telefones";
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.tbxCelular);
            this.groupBox7.Controls.Add(this.tbxDddcelular);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.tbxRamalfax);
            this.groupBox7.Controls.Add(this.tbxTelefax);
            this.groupBox7.Controls.Add(this.tbxDddfax);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.tbxRamal);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.tbxTelefone);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.tbxDdd);
            this.groupBox7.Location = new System.Drawing.Point(320, 102);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(220, 105);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.ttpContato.SetToolTip(this.groupBox7, "Grupo - Telefones");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 60;
            this.label12.Text = "Celular:";
            // 
            // tbxCelular
            // 
            this.tbxCelular.AccessibleDescription = "Número Celular";
            this.tbxCelular.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCelular.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCelular.Location = new System.Drawing.Point(93, 80);
            this.tbxCelular.MaxLength = 11;
            this.tbxCelular.Name = "tbxCelular";
            this.tbxCelular.Size = new System.Drawing.Size(76, 21);
            this.tbxCelular.TabIndex = 7;
            this.ttpContato.SetToolTip(this.tbxCelular, "Celular Número");
            this.tbxCelular.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownTelefone);
            this.tbxCelular.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxCelular.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxDddcelular
            // 
            this.tbxDddcelular.AccessibleDescription = "DDD Celular";
            this.tbxDddcelular.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDddcelular.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDddcelular.Location = new System.Drawing.Point(57, 80);
            this.tbxDddcelular.MaxLength = 2;
            this.tbxDddcelular.Name = "tbxDddcelular";
            this.tbxDddcelular.Size = new System.Drawing.Size(34, 21);
            this.tbxDddcelular.TabIndex = 6;
            this.tbxDddcelular.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttpContato.SetToolTip(this.tbxDddcelular, "Celular DDD");
            this.tbxDddcelular.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDddcelular.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxDddcelular.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "Fax:";
            // 
            // tbxRamalfax
            // 
            this.tbxRamalfax.AccessibleDescription = "Ramal Fax";
            this.tbxRamalfax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxRamalfax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxRamalfax.Location = new System.Drawing.Point(173, 53);
            this.tbxRamalfax.MaxLength = 4;
            this.tbxRamalfax.Name = "tbxRamalfax";
            this.tbxRamalfax.Size = new System.Drawing.Size(38, 21);
            this.tbxRamalfax.TabIndex = 5;
            this.tbxRamalfax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttpContato.SetToolTip(this.tbxRamalfax, "Telefax Ramal");
            this.tbxRamalfax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxRamalfax.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxRamalfax.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxTelefax
            // 
            this.tbxTelefax.AccessibleDescription = "Número Fax";
            this.tbxTelefax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTelefax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTelefax.Location = new System.Drawing.Point(93, 53);
            this.tbxTelefax.MaxLength = 11;
            this.tbxTelefax.Name = "tbxTelefax";
            this.tbxTelefax.Size = new System.Drawing.Size(76, 21);
            this.tbxTelefax.TabIndex = 4;
            this.ttpContato.SetToolTip(this.tbxTelefax, "Telefax Número");
            this.tbxTelefax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownTelefone);
            this.tbxTelefax.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxTelefax.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tbxDddfax
            // 
            this.tbxDddfax.AccessibleDescription = "DDDFax";
            this.tbxDddfax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDddfax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDddfax.Location = new System.Drawing.Point(57, 53);
            this.tbxDddfax.MaxLength = 2;
            this.tbxDddfax.Name = "tbxDddfax";
            this.tbxDddfax.Size = new System.Drawing.Size(34, 21);
            this.tbxDddfax.TabIndex = 3;
            this.tbxDddfax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttpContato.SetToolTip(this.tbxDddfax, "Telefax DDD");
            this.tbxDddfax.TextChanged += new System.EventHandler(this.tbxDddfax_TextChanged);
            this.tbxDddfax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDddfax.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxDddfax.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "Telefone:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(170, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Ramal";
            // 
            // tbxRamal
            // 
            this.tbxRamal.AccessibleDescription = "Ramal";
            this.tbxRamal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxRamal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxRamal.Location = new System.Drawing.Point(173, 26);
            this.tbxRamal.MaxLength = 4;
            this.tbxRamal.Name = "tbxRamal";
            this.tbxRamal.Size = new System.Drawing.Size(38, 21);
            this.tbxRamal.TabIndex = 2;
            this.tbxRamal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttpContato.SetToolTip(this.tbxRamal, "Telefone Ramal");
            this.tbxRamal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxRamal.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxRamal.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(103, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Número";
            // 
            // tbxTelefone
            // 
            this.tbxTelefone.AccessibleDescription = "Número";
            this.tbxTelefone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxTelefone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTelefone.Location = new System.Drawing.Point(93, 26);
            this.tbxTelefone.MaxLength = 11;
            this.tbxTelefone.Name = "tbxTelefone";
            this.tbxTelefone.Size = new System.Drawing.Size(76, 21);
            this.tbxTelefone.TabIndex = 1;
            this.ttpContato.SetToolTip(this.tbxTelefone, "Telefone Número");
            this.tbxTelefone.TextChanged += new System.EventHandler(this.tbxTelefone_TextChanged);
            this.tbxTelefone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDownTelefone);
            this.tbxTelefone.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxTelefone.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(62, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "DDD";
            // 
            // tbxDdd
            // 
            this.tbxDdd.AccessibleDescription = "DDD";
            this.tbxDdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxDdd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDdd.Location = new System.Drawing.Point(57, 26);
            this.tbxDdd.MaxLength = 4;
            this.tbxDdd.Name = "tbxDdd";
            this.tbxDdd.Size = new System.Drawing.Size(34, 21);
            this.tbxDdd.TabIndex = 0;
            this.tbxDdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ttpContato.SetToolTip(this.tbxDdd, "Telefone DDD");
            this.tbxDdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDdd.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxDdd.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // tspTool2
            // 
            this.tspTool2.AccessibleDescription = "Barra de Opções";
            this.tspTool2.AutoSize = false;
            this.tspTool2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspTool2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspRetornar});
            this.tspTool2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool2.Location = new System.Drawing.Point(0, 296);
            this.tspTool2.Name = "tspTool2";
            this.tspTool2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool2.Size = new System.Drawing.Size(550, 45);
            this.tspTool2.TabIndex = 6;
            this.tspTool2.TabStop = true;
            this.tspTool2.Text = "toolStrip1";
            this.ttpContato.SetToolTip(this.tspTool2, "Barra de Opções");
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = global::CRCorrea.Properties.Resources.Salvar;
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
            this.tspPrimeiro.Enabled = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = global::CRCorrea.Properties.Resources.Primeiro;
            this.tspPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Size = new System.Drawing.Size(66, 42);
            this.tspPrimeiro.Text = "Pr&imeiro";
            this.tspPrimeiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspAnterior
            // 
            this.tspAnterior.AccessibleDescription = "Anterior";
            this.tspAnterior.AutoSize = false;
            this.tspAnterior.Enabled = false;
            this.tspAnterior.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAnterior.Image = global::CRCorrea.Properties.Resources.Anterior;
            this.tspAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAnterior.Name = "tspAnterior";
            this.tspAnterior.Size = new System.Drawing.Size(66, 42);
            this.tspAnterior.Text = "&Anterior";
            this.tspAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspProximo
            // 
            this.tspProximo.AccessibleDescription = "Próximo";
            this.tspProximo.AutoSize = false;
            this.tspProximo.Enabled = false;
            this.tspProximo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspProximo.Image = global::CRCorrea.Properties.Resources.Proximo;
            this.tspProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspProximo.Name = "tspProximo";
            this.tspProximo.Size = new System.Drawing.Size(66, 42);
            this.tspProximo.Text = "Pró&ximo";
            this.tspProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspUltimo
            // 
            this.tspUltimo.AccessibleDescription = "Último";
            this.tspUltimo.AutoSize = false;
            this.tspUltimo.Enabled = false;
            this.tspUltimo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUltimo.Image = global::CRCorrea.Properties.Resources.Ultimo;
            this.tspUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Size = new System.Drawing.Size(66, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = global::CRCorrea.Properties.Resources.Retornar;
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // tbxEmail
            // 
            this.tbxEmail.AccessibleDescription = "E-mail";
            this.tbxEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxEmail.Font = new System.Drawing.Font("Tahoma", 6.4F, System.Drawing.FontStyle.Bold);
            this.tbxEmail.Location = new System.Drawing.Point(44, 74);
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.Size = new System.Drawing.Size(261, 18);
            this.tbxEmail.TabIndex = 2;
            this.ttpContato.SetToolTip(this.tbxEmail, "E-mail");
            this.tbxEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxEmail.Leave += new System.EventHandler(this.ControlLeave);
            this.tbxEmail.Enter += new System.EventHandler(this.ControlEnter);
            // 
            // frmClienteprospeccaocontato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 341);
            this.ControlBox = false;
            this.Controls.Add(this.tspTool2);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmClienteprospeccaocontato";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "523";
            this.Text = "Cliente Prospecção Contato - Registrando";
            this.ttpContato.SetToolTip(this, "Cliente Prospecção Contato - Registrando");
            this.Load += new System.EventHandler(this.frmClienteProspeccaoContato_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tspTool2.ResumeLayout(false);
            this.tspTool2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxClienteprospeccao_numero;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxClienteprospeccao_cognome;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbxContato;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbxTipoContato;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox tbxObserva;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tbxDdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxSetor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxRamal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxTelefone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxCelular;
        private System.Windows.Forms.TextBox tbxDddcelular;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxRamalfax;
        private System.Windows.Forms.TextBox tbxTelefax;
        private System.Windows.Forms.TextBox tbxDddfax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStrip tspTool2;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspPrimeiro;
        private System.Windows.Forms.ToolStripButton tspAnterior;
        private System.Windows.Forms.ToolStripButton tspProximo;
        private System.Windows.Forms.ToolStripButton tspUltimo;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.ListBox lbxTipocontato;
        private System.Windows.Forms.ToolTip ttpContato;
        private System.Windows.Forms.TextBox tbxEmail;
    }
}