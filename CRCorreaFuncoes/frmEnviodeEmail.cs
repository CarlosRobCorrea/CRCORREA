using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorreaFuncoes
{
    public partial class frmEnviodeEmail : Form
    {
        String smtp;
        String smtpporta;
        String  smtpemail;
        String smtpsenha;
        String arquivo;       
        String destinatario;
        String assunto;
        String mensagem;
        Int32 iddocumento = 0;
        String tipodocumento = "";
        String[,] iddocumento_tipodocumento_array;  
        Boolean ok;

        clsEnviarEmail clsEnviarEmail = new clsEnviarEmail();

        String resposta;

        public frmEnviodeEmail()
        {
            InitializeComponent();
        }

        public void Init(String  _smtp,
                                String _smtpporta,
                                String _smtpemail,
                                String _smtpsenha,
                                String _arquivo,
                                String _destinatario,
                                Int32 _iddocumento,
                                String _tipodocumento)
        {
            smtp = _smtp;
            smtpporta = _smtpporta ;
            smtpemail = _smtpemail;
            smtpsenha = _smtpsenha;
            arquivo = _arquivo;           
            destinatario = _destinatario;
            iddocumento = _iddocumento;
            tipodocumento = _tipodocumento;
            ok = false;
            
        }

        public void Init(String _smtp,
                                String _smtpporta,
                                String _smtpemail,
                                String _smtpsenha,
                                String _arquivo,
                                String _destinatario,
                                String _assunto,
                                Int32 _iddocumento,
                                String _tipodocumento)
        {
            smtp = _smtp;
            smtpporta = _smtpporta;
            smtpemail = _smtpemail;
            smtpsenha = _smtpsenha;
            arquivo = _arquivo;
            destinatario = _destinatario;
            assunto = _assunto;
            iddocumento = _iddocumento;
            tipodocumento = _tipodocumento;

            ok = false;
            
        }

        public void Init(String _smtp,
                                String _smtpporta,
                                String _smtpemail,
                                String _smtpsenha,
                                String _arquivo,
                                String _destinatario,
                                String[,] _iddocumento_tipodocumento_array)
        {
            smtp = _smtp;
            smtpporta = _smtpporta;
            smtpemail = _smtpemail;
            smtpsenha = _smtpsenha;
            arquivo = _arquivo;
            destinatario = _destinatario;
            iddocumento_tipodocumento_array = _iddocumento_tipodocumento_array;
            ok = false;
            
        }

        public void Init(String _smtp,
                                String _smtpporta,
                                String _smtpemail,
                                String _smtpsenha,
                                String _arquivo,
                                String _destinatario,
                                String _assunto,
                                String[,] _iddocumento_tipodocumento_array)
        {
            smtp = _smtp;
            smtpporta = _smtpporta;
            smtpemail = _smtpemail;
            smtpsenha = _smtpsenha;
            arquivo = _arquivo;
            destinatario = _destinatario;
            assunto = _assunto;
            iddocumento_tipodocumento_array = _iddocumento_tipodocumento_array;

            ok = false;
            
        }

        public void Init(String _smtp,
                               String _smtpporta,
                               String _smtpemail,
                               String _smtpsenha,
                               String _arquivo,
                               String _destinatario,
                               String _assunto,
                               String _mensagem)
        {
            smtp = _smtp;
            smtpporta = _smtpporta;
            smtpemail = _smtpemail;
            smtpsenha = _smtpsenha;
            arquivo = _arquivo;
            destinatario = _destinatario;
            assunto = _assunto;
            mensagem = _mensagem;
            ok = true;
            
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ok == false)
                {
                    resposta = clsEnviarEmail.inicio(tbxDe.Text, tbxPara.Text, tbxComCopia.Text, tbxCopiaOculta.Text, tbxAssunto.Text, tbxMensagem.Text, tbxArquivo.Text, smtp, tbxDe.Text, smtpsenha, smtpporta);
                    if (resposta != "Enviado com Sucesso")
                    {
                        MessageBox.Show(resposta, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        if (resposta == "Favor cadastrar o E-mail para este usuário, no cadastro de Usuários")
                        {

                        }

                        if (resposta == "Informe destinatario")
                        {
                            tbxPara.Select();
                            tbxPara.SelectAll();
                        }

                        if (resposta == "Informe o assunto")
                        {
                            tbxAssunto.Select();
                            tbxAssunto.SelectAll();
                        }

                        if (resposta == "Favor cadastrar a Senha de e-mail para este usuário, no cadastro de Usuários")
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show(resposta, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ConfirmaEnvioEmail();
                        this.Close();
                    }
                }
                else
                {                  
                    resposta = clsEnviarEmail.Enviar2(tbxDe.Text, tbxPara.Text, tbxComCopia.Text, tbxCopiaOculta.Text, tbxAssunto.Text, tbxMensagem.Text, tbxArquivo.Text, tbxDe.Text, smtpsenha, smtp, smtpporta);
                    
                    if (resposta != "Enviado com Sucesso")
                    {
                        MessageBox.Show(resposta, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        if (resposta == "Favor cadastrar o E-mail para este usuário, no cadastro de Usuários")
                        {

                        }

                        if (resposta == "Informe destinatario")
                        {
                            tbxPara.Select();
                            tbxPara.SelectAll();
                        }

                        if (resposta == "Informe o assunto")
                        {
                            tbxAssunto.Select();
                            tbxAssunto.SelectAll();
                        }

                        if (resposta == "Favor cadastrar a Senha de e-mail para este usuário, no cadastro de Usuários")
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show(resposta, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ConfirmaEnvioEmail();
                        this.Close();
                    }
                
                }
            }                     
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        { 
            clsVisual.ControlLeave(sender);
            clsVisual.FormatarCampoNumerico(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);            
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEnviodeEmail_Load(object sender, EventArgs e)
        {
            tbxDe.Text = smtpemail.ToString();
            tbxArquivo.Text = arquivo.ToString();
            //if (destinatario.IndexOf(";") > 0)
            //{
            //    fim = destinatario.IndexOf(";");
            //    tbxPara.Text = destinatario.Substring(inicio, fim - 1);

            //    tbxComCopia.Text = destinatario.Substring(fim + 1);
            //}
            //else
            //{
            tbxPara.Text = destinatario.ToString();
            //}
            tbxSmtp.Text = smtp.ToString();
            tbxPorta.Text = smtpporta.ToString();
            tbxPara.Text = destinatario.ToString();
            tbxAssunto.Text = assunto;
            if (mensagem == null)
            {
                tbxMensagem.Text = "";
            }
            else
            {
                tbxMensagem.Text = mensagem.ToString();
            }
        }

        private void btnAnexo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog ();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbxArquivo.Text = openFileDialog1.FileName.ToString();
            }
        }

        private void frmEnviodeEmail_Shown(object sender, EventArgs e)
        {
            
        }
        private void ConfirmaEnvioEmail()
        {
            if (iddocumento > 0 && tipodocumento.ToString() == "CERT")
            {
                String query = "";
                SqlConnection scn;
                SqlCommand scd;

                query = "update certificado set enviado= @enviado, horadoenvio = @horadoenvio where id = " + iddocumento.ToString();
                scn = new SqlConnection(clsInfo.conexaosqldados);
                scd = new SqlCommand(query, scn);

                scd.Parameters.Add("@enviado", System.Data.SqlDbType.NVarChar).Value = "S";
                scd.Parameters.Add("@horadoenvio", System.Data.SqlDbType.DateTime).Value = DateTime.Now;

                scn.Open();
                scd.ExecuteScalar();
                scn.Close();
            }

            if (iddocumento_tipodocumento_array != null)
            {
                for (Int32 cont = 0; cont <= (iddocumento_tipodocumento_array.Length /2)-1; cont++)
                {
                    String query = "";
                    SqlConnection scn;
                    SqlCommand scd;

                    //Certificado de qualidade
                    if (iddocumento_tipodocumento_array[cont, 1].ToString() == "CERT")
                    {
                        query = "update certificado set enviado= @enviado, horadoenvio = @horadoenvio where id = " +
                                               iddocumento_tipodocumento_array[cont, 0].ToString();
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scd = new SqlCommand(query, scn);

                        scd.Parameters.Add("@enviado", System.Data.SqlDbType.NVarChar).Value = "S";
                        scd.Parameters.Add("@horadoenvio", System.Data.SqlDbType.DateTime).Value = DateTime.Now;

                        scn.Open();
                        scd.ExecuteScalar();
                        scn.Close();
                    }
                }
            }
        }
    }
}
