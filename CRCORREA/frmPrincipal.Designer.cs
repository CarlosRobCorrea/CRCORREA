namespace CRCORREA
{
    partial class frmPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            mnuPrincipal = new MenuStrip();
            mnuMovimentacao = new ToolStripMenuItem();
            mnuCadastros = new ToolStripMenuItem();
            mnuTabelas = new ToolStripMenuItem();
            mnuRelatorios = new ToolStripMenuItem();
            mnuConfiguracao = new ToolStripMenuItem();
            mnuSair = new ToolStripMenuItem();
            sspStatus = new StatusStrip();
            sspEstacao = new ToolStripStatusLabel();
            sspEstacaoValor = new ToolStripStatusLabel();
            sspUsuario = new ToolStripStatusLabel();
            sspUsuarioValor = new ToolStripStatusLabel();
            sspForm = new ToolStripStatusLabel();
            sspFormValor = new ToolStripStatusLabel();
            sspData = new ToolStripStatusLabel();
            sspDataValor = new ToolStripStatusLabel();
            sspHora = new ToolStripStatusLabel();
            sspHoraValor = new ToolStripStatusLabel();
            mnuPrincipal.SuspendLayout();
            sspStatus.SuspendLayout();
            SuspendLayout();
            // 
            // mnuPrincipal
            // 
            mnuPrincipal.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            mnuPrincipal.Items.AddRange(new ToolStripItem[] { mnuMovimentacao, mnuCadastros, mnuTabelas, mnuRelatorios, mnuConfiguracao, mnuSair });
            mnuPrincipal.Location = new Point(0, 0);
            mnuPrincipal.Name = "mnuPrincipal";
            mnuPrincipal.Padding = new Padding(5, 2, 0, 2);
            mnuPrincipal.Size = new Size(1018, 24);
            mnuPrincipal.TabIndex = 1;
            mnuPrincipal.Text = "mnuPrincipal";
            // 
            // mnuMovimentacao
            // 
            mnuMovimentacao.AccessibleDescription = "Mnu Movimentacao";
            mnuMovimentacao.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            mnuMovimentacao.Image = Properties.Resources.Movimentacao;
            mnuMovimentacao.Name = "mnuMovimentacao";
            mnuMovimentacao.Size = new Size(119, 20);
            mnuMovimentacao.Text = "Movimentação";
            mnuMovimentacao.ToolTipText = "Mnu Movimentacao";
            // 
            // mnuCadastros
            // 
            mnuCadastros.AccessibleDescription = "Mnu Cadastros";
            mnuCadastros.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            mnuCadastros.Image = Properties.Resources.Cadastro;
            mnuCadastros.Name = "mnuCadastros";
            mnuCadastros.Size = new Size(92, 20);
            mnuCadastros.Text = "Cadastros";
            mnuCadastros.ToolTipText = "Mnu Cadastros";
            // 
            // mnuTabelas
            // 
            mnuTabelas.AccessibleDescription = "mnuTabelas";
            mnuTabelas.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            mnuTabelas.Image = Properties.Resources.Tabelas;
            mnuTabelas.Name = "mnuTabelas";
            mnuTabelas.Size = new Size(79, 20);
            mnuTabelas.Text = "Tabelas";
            mnuTabelas.ToolTipText = "mnuTabelas";
            // 
            // mnuRelatorios
            // 
            mnuRelatorios.AccessibleDescription = "mnu Relatorio";
            mnuRelatorios.Image = Properties.Resources.imprimir;
            mnuRelatorios.Name = "mnuRelatorios";
            mnuRelatorios.Size = new Size(93, 20);
            mnuRelatorios.Text = "Relatorios";
            mnuRelatorios.ToolTipText = "mnu Relatorios";
            // 
            // mnuConfiguracao
            // 
            mnuConfiguracao.AccessibleDescription = "mnu Configuracao";
            mnuConfiguracao.Image = Properties.Resources.Configuracoes;
            mnuConfiguracao.Name = "mnuConfiguracao";
            mnuConfiguracao.Size = new Size(115, 20);
            mnuConfiguracao.Text = "Configurações";
            mnuConfiguracao.ToolTipText = "mnu Configuracao";
            // 
            // mnuSair
            // 
            mnuSair.AccessibleDescription = "Sair Sistema";
            mnuSair.Image = Properties.Resources.exit;
            mnuSair.Name = "mnuSair";
            mnuSair.Size = new Size(106, 20);
            mnuSair.Text = "Sair Sistema";
            mnuSair.ToolTipText = "Sair";
            // 
            // sspStatus
            // 
            sspStatus.AutoSize = false;
            sspStatus.Items.AddRange(new ToolStripItem[] { sspEstacao, sspEstacaoValor, sspUsuario, sspUsuarioValor, sspForm, sspFormValor, sspData, sspDataValor, sspHora, sspHoraValor });
            sspStatus.Location = new Point(0, 679);
            sspStatus.Name = "sspStatus";
            sspStatus.Padding = new Padding(1, 0, 12, 0);
            sspStatus.Size = new Size(1018, 19);
            sspStatus.TabIndex = 3;
            sspStatus.Text = "sspStatus";
            // 
            // sspEstacao
            // 
            sspEstacao.AccessibleDescription = "Label Estacao";
            sspEstacao.AutoSize = false;
            sspEstacao.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspEstacao.Image = Properties.Resources.Estacao;
            sspEstacao.Name = "sspEstacao";
            sspEstacao.Size = new Size(66, 14);
            sspEstacao.Text = "Terminal:";
            sspEstacao.TextAlign = ContentAlignment.MiddleLeft;
            sspEstacao.ToolTipText = "Terminal Atual";
            // 
            // sspEstacaoValor
            // 
            sspEstacaoValor.AccessibleDescription = "Nome Terminal";
            sspEstacaoValor.AutoSize = false;
            sspEstacaoValor.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspEstacaoValor.Name = "sspEstacaoValor";
            sspEstacaoValor.Size = new Size(55, 14);
            sspEstacaoValor.Text = "Nome Terminal";
            sspEstacaoValor.TextAlign = ContentAlignment.MiddleLeft;
            sspEstacaoValor.ToolTipText = "Nome Terminal";
            // 
            // sspUsuario
            // 
            sspUsuario.AccessibleDescription = "Label Usuario";
            sspUsuario.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspUsuario.Image = Properties.Resources.usuario;
            sspUsuario.Name = "sspUsuario";
            sspUsuario.Size = new Size(69, 14);
            sspUsuario.Text = "Usuario:";
            sspUsuario.TextAlign = ContentAlignment.MiddleLeft;
            sspUsuario.ToolTipText = "Label Usuario";
            // 
            // sspUsuarioValor
            // 
            sspUsuarioValor.AccessibleDescription = "Nome Usuario";
            sspUsuarioValor.AutoSize = false;
            sspUsuarioValor.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspUsuarioValor.Name = "sspUsuarioValor";
            sspUsuarioValor.Size = new Size(106, 14);
            sspUsuarioValor.Text = "Nome Usuario";
            sspUsuarioValor.TextAlign = ContentAlignment.MiddleLeft;
            sspUsuarioValor.ToolTipText = "Nome Usuario";
            // 
            // sspForm
            // 
            sspForm.AccessibleDescription = "Label Formulario";
            sspForm.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspForm.Image = Properties.Resources.Forms;
            sspForm.Name = "sspForm";
            sspForm.Size = new Size(55, 14);
            sspForm.Text = "Form:";
            sspForm.TextAlign = ContentAlignment.MiddleLeft;
            sspForm.ToolTipText = "Nome Formulario";
            // 
            // sspFormValor
            // 
            sspFormValor.AccessibleDescription = "Nome Form";
            sspFormValor.AutoSize = false;
            sspFormValor.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspFormValor.Name = "sspFormValor";
            sspFormValor.Size = new Size(483, 14);
            sspFormValor.Text = "Nome Form";
            sspFormValor.TextAlign = ContentAlignment.MiddleLeft;
            sspFormValor.ToolTipText = "Nome Form";
            // 
            // sspData
            // 
            sspData.AccessibleDescription = "Label Data";
            sspData.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspData.Name = "sspData";
            sspData.Size = new Size(37, 14);
            sspData.Text = "Data:";
            sspData.TextAlign = ContentAlignment.MiddleLeft;
            sspData.ToolTipText = "Label Data";
            // 
            // sspDataValor
            // 
            sspDataValor.AccessibleDescription = "Data";
            sspDataValor.AutoSize = false;
            sspDataValor.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspDataValor.Name = "sspDataValor";
            sspDataValor.Size = new Size(38, 14);
            sspDataValor.Text = "Data";
            sspDataValor.TextAlign = ContentAlignment.MiddleLeft;
            sspDataValor.ToolTipText = "Data";
            // 
            // sspHora
            // 
            sspHora.AccessibleDescription = "Label Hora";
            sspHora.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspHora.Name = "sspHora";
            sspHora.Size = new Size(37, 14);
            sspHora.Text = "Hora:";
            sspHora.TextAlign = ContentAlignment.MiddleLeft;
            sspHora.ToolTipText = "Label Hora";
            // 
            // sspHoraValor
            // 
            sspHoraValor.AccessibleDescription = "Hora";
            sspHoraValor.AutoSize = false;
            sspHoraValor.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sspHoraValor.Name = "sspHoraValor";
            sspHoraValor.Size = new Size(34, 14);
            sspHoraValor.Text = "Hora";
            sspHoraValor.TextAlign = ContentAlignment.MiddleLeft;
            sspHoraValor.ToolTipText = "Hora";
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1018, 698);
            Controls.Add(sspStatus);
            Controls.Add(mnuPrincipal);
            Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = mnuPrincipal;
            Name = "frmPrincipal";
            Text = "Menu Principal C R Correa";
            mnuPrincipal.ResumeLayout(false);
            mnuPrincipal.PerformLayout();
            sspStatus.ResumeLayout(false);
            sspStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnuPrincipal;
        private ToolStripMenuItem mnuMovimentacao;
        private ToolStripMenuItem mnuCadastros;
        private ToolStripMenuItem mnuTabelas;
        private ToolStripMenuItem mnuRelatorios;
        private ToolStripMenuItem mnuConfiguracao;
        private ToolStripMenuItem mnuSair;
        private StatusStrip sspStatus;
        private ToolStripStatusLabel sspEstacao;
        private ToolStripStatusLabel sspEstacaoValor;
        private ToolStripStatusLabel sspUsuario;
        private ToolStripStatusLabel sspUsuarioValor;
        private ToolStripStatusLabel sspForm;
        private ToolStripStatusLabel sspFormValor;
        private ToolStripStatusLabel sspData;
        private ToolStripStatusLabel sspDataValor;
        private ToolStripStatusLabel sspHora;
        private ToolStripStatusLabel sspHoraValor;
    }
}
