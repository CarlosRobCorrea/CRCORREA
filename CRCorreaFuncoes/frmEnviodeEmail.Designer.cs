namespace CRCorreaFuncoes
{
    partial class frmEnviodeEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnviodeEmail));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbxSmtp = new System.Windows.Forms.TextBox();
            this.tbxDe = new System.Windows.Forms.TextBox();
            this.tbxPara = new System.Windows.Forms.TextBox();
            this.tbxPorta = new System.Windows.Forms.TextBox();
            this.tbxAssunto = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tbxMensagem = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxCopiaOculta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxComCopia = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAnexo = new System.Windows.Forms.Button();
            this.tbxArquivo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // tbxSmtp
            // 
            this.tbxSmtp.AccessibleDescription = "Servidor SMTP";
            this.tbxSmtp.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxSmtp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSmtp.Location = new System.Drawing.Point(126, 20);
            this.tbxSmtp.Name = "tbxSmtp";
            this.tbxSmtp.ReadOnly = true;
            this.tbxSmtp.Size = new System.Drawing.Size(864, 21);
            this.tbxSmtp.TabIndex = 0;
            this.tbxSmtp.TabStop = false;
            this.toolTip1.SetToolTip(this.tbxSmtp, "Servidor SMTP");
            this.tbxSmtp.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxSmtp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxSmtp.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxDe
            // 
            this.tbxDe.AccessibleDescription = "DE:";
            this.tbxDe.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxDe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDe.Location = new System.Drawing.Point(126, 74);
            this.tbxDe.Name = "tbxDe";
            this.tbxDe.ReadOnly = true;
            this.tbxDe.Size = new System.Drawing.Size(864, 21);
            this.tbxDe.TabIndex = 2;
            this.tbxDe.TabStop = false;
            this.toolTip1.SetToolTip(this.tbxDe, "Email Remetente");
            this.tbxDe.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxDe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxDe.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxPara
            // 
            this.tbxPara.AccessibleDescription = "Para:";
            this.tbxPara.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxPara.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPara.Location = new System.Drawing.Point(126, 100);
            this.tbxPara.Name = "tbxPara";
            this.tbxPara.Size = new System.Drawing.Size(864, 21);
            this.tbxPara.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tbxPara, "Email Destinatario");
            this.tbxPara.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxPara.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxPara.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxPorta
            // 
            this.tbxPorta.AccessibleDescription = "Porta SMTP";
            this.tbxPorta.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxPorta.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPorta.Location = new System.Drawing.Point(126, 47);
            this.tbxPorta.Name = "tbxPorta";
            this.tbxPorta.ReadOnly = true;
            this.tbxPorta.Size = new System.Drawing.Size(128, 21);
            this.tbxPorta.TabIndex = 1;
            this.tbxPorta.TabStop = false;
            this.toolTip1.SetToolTip(this.tbxPorta, "Porta do Servidor SMTP");
            this.tbxPorta.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxPorta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxPorta.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // tbxAssunto
            // 
            this.tbxAssunto.AccessibleDescription = "Assunto";
            this.tbxAssunto.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAssunto.Location = new System.Drawing.Point(126, 220);
            this.tbxAssunto.Name = "tbxAssunto";
            this.tbxAssunto.Size = new System.Drawing.Size(864, 21);
            this.tbxAssunto.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tbxAssunto, "Assunto do e-Mail");
            this.tbxAssunto.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxAssunto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxAssunto.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "Escrevendo E-Mail";
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbxAssunto);
            this.groupBox1.Controls.Add(this.tbxPorta);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxPara);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxSmtp);
            this.groupBox1.Controls.Add(this.tbxDe);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(996, 633);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Escrevendo E-mail";
            this.toolTip1.SetToolTip(this.groupBox1, "Escrevendo o E-Mail");
            // 
            // groupBox5
            // 
            this.groupBox5.AccessibleDescription = "Mensagem";
            this.groupBox5.Controls.Add(this.tbxMensagem);
            this.groupBox5.Location = new System.Drawing.Point(6, 313);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(984, 314);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Mensagem";
            this.toolTip1.SetToolTip(this.groupBox5, "Mensagem");
            // 
            // tbxMensagem
            // 
            this.tbxMensagem.AccessibleDescription = "Mensagem";
            this.tbxMensagem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMensagem.Location = new System.Drawing.Point(6, 20);
            this.tbxMensagem.Multiline = true;
            this.tbxMensagem.Name = "tbxMensagem";
            this.tbxMensagem.Size = new System.Drawing.Size(972, 288);
            this.tbxMensagem.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxMensagem, "Mensagem do E-Mail");
            // 
            // groupBox4
            // 
            this.groupBox4.AccessibleDescription = "Copia e Copia Oculta ";
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.tbxCopiaOculta);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.tbxComCopia);
            this.groupBox4.Location = new System.Drawing.Point(6, 127);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(984, 87);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cópia e Cópia Oculta ";
            this.toolTip1.SetToolTip(this.groupBox4, "Copia e Copia Oculta");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(42, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(277, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Utilize ponto e virgula \";\" para separar os E-Mail";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Cco:";
            // 
            // tbxCopiaOculta
            // 
            this.tbxCopiaOculta.AccessibleDescription = "Com Cópia Oculta";
            this.tbxCopiaOculta.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxCopiaOculta.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCopiaOculta.Location = new System.Drawing.Point(42, 60);
            this.tbxCopiaOculta.Name = "tbxCopiaOculta";
            this.tbxCopiaOculta.Size = new System.Drawing.Size(933, 21);
            this.tbxCopiaOculta.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbxCopiaOculta, "Para varios E-Mail");
            this.tbxCopiaOculta.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxCopiaOculta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxCopiaOculta.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Cc:";
            // 
            // tbxComCopia
            // 
            this.tbxComCopia.AccessibleDescription = "Com Cópia";
            this.tbxComCopia.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbxComCopia.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxComCopia.Location = new System.Drawing.Point(42, 33);
            this.tbxComCopia.Name = "tbxComCopia";
            this.tbxComCopia.Size = new System.Drawing.Size(933, 21);
            this.tbxComCopia.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbxComCopia, "Com copia para o E-Mail");
            this.tbxComCopia.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxComCopia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxComCopia.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // groupBox3
            // 
            this.groupBox3.AccessibleDescription = "Anexar Arquivo";
            this.groupBox3.Controls.Add(this.btnAnexo);
            this.groupBox3.Controls.Add(this.tbxArquivo);
            this.groupBox3.Location = new System.Drawing.Point(6, 247);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(984, 60);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Anexar Arquivo";
            this.toolTip1.SetToolTip(this.groupBox3, "Selecione arquivo para anexar");
            // 
            // btnAnexo
            // 
            this.btnAnexo.ForeColor = System.Drawing.Color.Black;
            this.btnAnexo.Image = global::CRCorreaFuncoes.Properties.Resources.Procurar;
            this.btnAnexo.Location = new System.Drawing.Point(946, 20);
            this.btnAnexo.Name = "btnAnexo";
            this.btnAnexo.Size = new System.Drawing.Size(32, 29);
            this.btnAnexo.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnAnexo, "Procurar Pasta");
            this.btnAnexo.UseVisualStyleBackColor = true;
            this.btnAnexo.Click += new System.EventHandler(this.btnAnexo_Click);
            // 
            // tbxArquivo
            // 
            this.tbxArquivo.AccessibleDescription = "Arquivo";
            this.tbxArquivo.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxArquivo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxArquivo.Location = new System.Drawing.Point(6, 16);
            this.tbxArquivo.Multiline = true;
            this.tbxArquivo.Name = "tbxArquivo";
            this.tbxArquivo.ReadOnly = true;
            this.tbxArquivo.Size = new System.Drawing.Size(934, 38);
            this.tbxArquivo.TabIndex = 1;
            this.tbxArquivo.TabStop = false;
            this.toolTip1.SetToolTip(this.tbxArquivo, "Arquivo em Anexo");
            this.tbxArquivo.Enter += new System.EventHandler(this.ControlEnter);
            this.tbxArquivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.tbxArquivo.Leave += new System.EventHandler(this.ControlLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(64, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Assunto:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Porta SMTP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Servidor SMTP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Para:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "De:";
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.AutoSize = false;
            this.tspTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(0, 648);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(1020, 37);
            this.tspTool.TabIndex = 1;
            this.tspTool.TabStop = true;
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Enviar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspSalvar.Image")));
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(66, 36);
            this.tspSalvar.Text = "&Enviar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspSalvar.Click += new System.EventHandler(this.tspSalvar_Click);
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 36);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspRetornar.Click += new System.EventHandler(this.tspRetornar_Click);
            // 
            // frmEnviodeEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 685);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tspTool);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEnviodeEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "60";
            this.Text = "Enviando Email - Cadastro";
            this.toolTip1.SetToolTip(this, "Enviando Email");
            this.Load += new System.EventHandler(this.frmEnviodeEmail_Load);
            this.Shown += new System.EventHandler(this.frmEnviodeEmail_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip tspTool;
        private System.Windows.Forms.ToolStripButton tspSalvar;
        private System.Windows.Forms.ToolStripButton tspRetornar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxSmtp;
        private System.Windows.Forms.TextBox tbxDe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxPara;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxPorta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxAssunto;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAnexo;
        private System.Windows.Forms.TextBox tbxArquivo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxCopiaOculta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxComCopia;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbxMensagem;
    }
}