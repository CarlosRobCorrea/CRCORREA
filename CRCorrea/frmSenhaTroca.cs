using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;


namespace CRCorrea
{
    public partial class frmSenhaTroca : Form
    {
        private const Int32 zFormNumero = 2;

        String conexao;
        Int32 registroId;

        public frmSenhaTroca()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Int32 _registroId)
        {
            registroId = _registroId;
            conexao = _conexao;
        }

        private void frmSenhaTroca_Load(object sender, EventArgs e)
        {
            try
            {
                clsUsuarioInfo usuarioInfo;
                clsUsuarioBLL usuarioBLL = new clsUsuarioBLL();

                usuarioInfo = usuarioBLL.Carregar(registroId, conexao);

                tbxUsuario.Text = usuarioInfo.Usuario;
                tbxNovaSenha.Select();
                tbxNovaSenha.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormGotFocus(object sender, EventArgs e)
        {
            
            clsVisual.ControlEnter(sender);
        }

        private void FormLostFocus(object sender, EventArgs e)
        {
            
            clsVisual.ControlLeave(sender);
        }

        private void TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se são iguais(Nova Senha e Senha Redigitada)
                if (tbxNovaSenha.Text != tbxNovaSenha2.Text)
                {
                    throw new Exception("Senhas não conferem.");
                }

                // Verifica tamanho de Senhas(Não pode ser menor que 2 caracteres)
                if (tbxNovaSenha.Text.Length < 2)
                {
                    throw new Exception("Necessário mais de 2(dois) caracteres para cadastrar sua Senha.");
                }

                clsUsuarioInfo usuarioInfo;
                clsUsuarioBLL usuarioBLL = new clsUsuarioBLL();
                clsCriptografia Criptografia = new clsCriptografia();

                usuarioInfo = usuarioBLL.Carregar(registroId, conexao);
                usuarioInfo.Senha = Criptografia.Criptografar(tbxNovaSenha.Text);

                if (usuarioInfo.Trocar == null )
                    usuarioInfo.Trocar = DateTime.Now;

                //if (usuarioInfo.trocasenha.IsNull)
                //    usuarioInfo.trocasenha = false;
              

                if (usuarioInfo.Trocar.Subtract(DateTime.Now).TotalDays < 0 &&
                    usuarioInfo.Trocasenha.ToString() == "S")
                {
                    usuarioInfo.Trocar = DateTime.Now.AddMonths(3);
                }

                usuarioBLL.Alterar(usuarioInfo, conexao);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
