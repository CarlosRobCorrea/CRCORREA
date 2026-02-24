using System;
using System.Collections;
using System.Net;
using System.Net.Mail;

namespace CRCorreaFuncoes
{
    public class clsEnviarEmail
    {
        public static bool usarssl = false;

        public static String inicio(String remetente, String destinatario, String comcopia, String variosemail, String assunto, String textodamensagem, String arquivoaanexar, String smtp, String login, String senha, String porta)
        {
            //Attachment att = null;

            //if (!String.IsNullOrEmpty(arquivoaanexar))
            //{
            //    att = new Attachment(@arquivoaanexar);
            //}

            int x = 0;
            int tam = 0;
            int inicio = 0;
            int fim = 0;
            string pedaco;
            //if ((remetente.Length + destinatario.Length + variosemail.Length) == 0)
            //{
            if (remetente.Length == 0)
                return "Favor cadastrar o E-mail para este usuário, no cadastro de Usuários";
            if (destinatario.Length == 0)
                return "Informe destinatario";
            //}

            if (assunto.Length == 0)
                return "Informe o assunto";
            if (login.Length == 0)
                return "Favor cadastrar o E-mail para este usuário, no cadastro de Usuários";
            if (senha.Length == 0)
                return "Favor cadastrar a Senha de e-mail para este usuário, no cadastro de Usuários";

            SmtpClient client = new SmtpClient(smtp, Int32.Parse(porta));
            if (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usarssl from empresa where id = " + clsInfo.zempresaid) == "S")
            {
                client.EnableSsl = true;
            }
            else
            {
                client.EnableSsl = false;
            }
            client.Credentials = new NetworkCredential(login, senha);
            //client.Credentials = new System.Net.NetworkCredential(login, senha);
            MailMessage message = new MailMessage();
            message.Sender = new MailAddress(remetente.ToString(), remetente);
            message.From = new MailAddress(remetente.ToString(), remetente);
            if (destinatario.Contains(";"))
            {
                var destinatarios = destinatario.Split(';');
                foreach (string dest in destinatarios)
                {
                    message.To.Add(new MailAddress(dest, dest));
                }
            }
            else
            {
                message.To.Add(new MailAddress(destinatario, destinatario));
            }

            //********************************AQUI CARREGA COPIAS QDO MAIS DE UMA Normal ********************************        
            x = comcopia.IndexOf(";");
            while (comcopia.IndexOf(";") > 0 || comcopia.IndexOf(";") > 0)
            {
                tam = comcopia.Length;
                fim = comcopia.IndexOf(";");
                if (fim < 0) { fim = comcopia.IndexOf(";"); }
                pedaco = comcopia.Substring(inicio, fim);
                message.CC.Add(new MailAddress(pedaco));
                inicio = fim + 1;
                comcopia = comcopia.Substring(inicio, tam - inicio);
                tam = variosemail.Length;
                inicio = 0;
            }
            if (comcopia.Length > 0) { message.CC.Add(new MailAddress(comcopia)); }



            //********************************AQUI CARREGA COPIAS QDO MAIS DE UMA Oculta ********************************        
            x = variosemail.IndexOf(";");
            while (variosemail.IndexOf(";") > 0 || variosemail.IndexOf(";") > 0)
            {
                tam = variosemail.Length;
                fim = variosemail.IndexOf(";");
                if (fim < 0) { fim = variosemail.IndexOf(";"); }
                pedaco = variosemail.Substring(inicio, fim);
                message.Bcc.Add(new MailAddress(pedaco));
                inicio = fim + 1;
                variosemail = variosemail.Substring(inicio, tam - inicio);
                tam = variosemail.Length;
                inicio = 0;
            }
            if (variosemail.Length > 0) { message.Bcc.Add(new MailAddress(variosemail)); }

            message.Subject = assunto;
            message.Body = textodamensagem;

            if (arquivoaanexar.Length > 0)
            {
                if (arquivoaanexar.Contains(";"))
                {
                    foreach (string anexo in arquivoaanexar.Split(';'))
                    {
                        // Desabilitei em 01/09/2018 - sem explicação para deixar de funcionar
                        Attachment att = new Attachment(anexo); 
                        message.Attachments.Add(att);
                    }
                }
                else
                {
                    message.Attachments.Add(new Attachment(arquivoaanexar));
                }
            }

            try
            {
                client.Send(message);

                return "Enviado com Sucesso";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public static void Enviar(String remetente,
                                    String destinatario,
                                    String cc,
                                    String cco,
                                    String assunto,
                                    String mensagem,
                                    String anexos,
                                    String login,
                                    String senha,
                                    String smtp,
                                    String porta)
        {
            if (remetente == null)
            {
                throw new Exception("E-mail do Remetente é inválido.");
            }

            if (destinatario == null)
            {
                throw new Exception("E-mail do Destinatário é inválido.");
            }

            remetente = remetente.Replace(" ", "");
            remetente = remetente.ToLower();

            destinatario = destinatario.Replace(" ", "");
            destinatario = destinatario.ToLower();

            if (remetente.Length < 3)
            {
                throw new Exception("E-mail do Remetente é inválido.");
            }

            if (destinatario.Length < 3)
            {
                throw new Exception("E-mail do Destinatário é inválido.");
            }

            ArrayList destinatario_lista = new ArrayList();
            if (destinatario != null && destinatario.Length > 2)
            {
                foreach (String s in destinatario.Split(';'))
                {
                    destinatario_lista.Add(s);
                }
            }

            ArrayList cc_lista = new ArrayList();
            if (cc != null && cc.Length > 2)
            {
                cc = cc.Replace(" ", "");
                cc = cc.ToLower();

                foreach (String s in cc.Split(';'))
                {
                    cc_lista.Add(s);
                }
            }

            ArrayList cco_lista = new ArrayList();

            if (cco != null && cco.Length > 2)
            {
                cco = cco.Replace(" ", "");
                cco = cco.ToLower();

                foreach (String s in cco.Split(';'))
                {
                    cco_lista.Add(s);
                }
            }

            ArrayList anexos_lista = new ArrayList();
            if (anexos != null && anexos.Length > 2)
            {
                foreach (String s in anexos.Split(';'))
                {
                    anexos_lista.Add(s);
                }
            }

            SmtpClient client = new SmtpClient(smtp, Int32.Parse(porta));

            if (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usarssl from empresa where id = " + clsInfo.zempresaid) == "S")
            {
                client.EnableSsl = true;
            }
            else
            {
                client.EnableSsl = false;
            }

            client.Credentials = new NetworkCredential(login, senha);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(remetente);

            foreach (String s in destinatario_lista)
            {
                message.To.Add(new MailAddress(s));
            }

            foreach (String s in cc_lista)
            {
                message.CC.Add(new MailAddress(s));
            }

            foreach (String s in cco_lista)
            {
                message.Bcc.Add(new MailAddress(s));
            }

            foreach (String s in anexos_lista)
            {
                message.Attachments.Add(new Attachment(s));
            }

            if (assunto != null)
            {
                message.Subject = assunto;
            }
            else
            {
                message.Subject = "";
            }

            if (mensagem != null)
            {
                message.Body = mensagem;
            }
            else
            {
                message.Body = "";
            }

            client.Send(message);
        }


        public static String Enviar2(String remetente,
                                   String destinatario,
                                   String cc,
                                   String cco,
                                   String assunto,
                                   String mensagem,
                                   String anexos,
                                   String login,
                                   String senha,
                                   String smtp,
                                   String porta)
        {
            remetente = remetente.Replace(" ", "");
            remetente = remetente.ToLower();

            destinatario = destinatario.Replace(" ", "");
            destinatario = destinatario.ToLower();

            if (remetente.Length == 0)
                return "Favor cadastrar o E-mail para este usuário, no cadastro de Usuários";
            if (destinatario.Length == 0)
                return "Informe destinatario";
            //}

            if (assunto.Length == 0)
                return "Informe o assunto";
            if (login.Length == 0)
                return "Favor cadastrar o E-mail para este usuário, no cadastro de Usuários";
            if (senha.Length == 0)
                return "Favor cadastrar a Senha de e-mail para este usuário, no cadastro de Usuários";


            ArrayList destinatario_lista = new ArrayList();
            if (destinatario != null && destinatario.Length > 2)
            {
                foreach (String s in destinatario.Split(';'))
                {
                    destinatario_lista.Add(s);
                }
            }

            ArrayList cc_lista = new ArrayList();
            if (cc != null && cc.Length > 2)
            {
                cc = cc.Replace(" ", "");
                cc = cc.ToLower();

                foreach (String s in cc.Split(';'))
                {
                    cc_lista.Add(s);
                }
            }

            ArrayList cco_lista = new ArrayList();

            if (cco != null && cco.Length > 2)
            {
                cco = cco.Replace(" ", "");
                cco = cco.ToLower();

                foreach (String s in cco.Split(';'))
                {
                    cco_lista.Add(s);
                }
            }

            ArrayList anexos_lista = new ArrayList();
            if (anexos != null && anexos.Length > 2)
            {
                foreach (String s in anexos.Split(';'))
                {
                    anexos_lista.Add(s);
                }
            }

            SmtpClient client = new SmtpClient(smtp, Int32.Parse(porta));

            if (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usarssl from empresa where id = " + clsInfo.zempresaid) == "S")
            {
                client.EnableSsl = true;
            }
            else
            {
                client.EnableSsl = false;
            }

            client.Credentials = new NetworkCredential(login, senha);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(remetente);

            foreach (String s in destinatario_lista)
            {
                message.To.Add(new MailAddress(s));
            }

            foreach (String s in cc_lista)
            {
                message.CC.Add(new MailAddress(s));
            }

            foreach (String s in cco_lista)
            {
                message.Bcc.Add(new MailAddress(s));
            }

            foreach (String s in anexos_lista)
            {
                message.Attachments.Add(new Attachment(s));
            }

            if (assunto != null)
            {
                message.Subject = assunto;
            }
            else
            {
                message.Subject = "";
            }

            if (mensagem != null)
            {
                message.Body = mensagem;
            }
            else
            {
                message.Body = "";
            }

            try
            {
                client.Send(message);
                return "Enviado com Sucesso";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}