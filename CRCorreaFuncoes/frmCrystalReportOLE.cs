using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms; 

namespace CRCorreaFuncoes
{ 
    public partial class frmCrystalReportOLE : Form
    {
        String localarquivo;
        String arquivo;
        String smtp;
        String smtpporta;
        String smtpemail;
        String smtpsenha;
        String arquivoanexo = "";
        String destinatario;
        String assunto;
        String nome_aleatorio = "";
        String sintaxecrystal;
        Int32 idreltipo;
        String servidor;
        String conexao;

        ParameterFields parametros;

        

        ConnectionInfo crConnection = new ConnectionInfo();
         
        public frmCrystalReportOLE()
        {
            InitializeComponent();
        }

        public void Init(String _localaquivo, String _arquivo, String _sintaxecrystal, ParameterFields _parametros, String _destinatario, String _nome_aleatorio, String _assunto, Int32 _idreltipo, String _conexao)
        {
            localarquivo = _localaquivo;
            conexao = _conexao;
            arquivo = _arquivo;
            parametros = _parametros;
            destinatario = _destinatario;
            assunto = _assunto;

            nome_aleatorio = _nome_aleatorio;

            nome_aleatorio = _nome_aleatorio;
            sintaxecrystal = _sintaxecrystal;
            //metodo = "CR_OLE";
            idreltipo = _idreltipo;

            
        }

        private void AssignConnectionInfo(ReportDocument document, DataTable _conectservidor)
        {
            try
            {
                crystalReportViewer1.LogOnInfo = new TableLogOnInfos();
                int x = 0;                
               
                PropertyInfo[] propriedades = typeof(clsInfo).GetProperties();
                //PropertyInfo[] propriedades = typeof(Properties.Settings).GetProperties();


                
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in document.Database.Tables)
                {
                    TableLogOnInfo logOnInfo = new TableLogOnInfo();
                    logOnInfo = table.LogOnInfo;
                    if (logOnInfo != null)
                    {
                        table.ApplyLogOnInfo(table.LogOnInfo);


                        table.LogOnInfo.TableName = table.Name;

                        table.LogOnInfo.ReportName = arquivo;

                        //Roda nas tabelas configurando as conexões
                        if (crConnection != null)
                        {
                            foreach (DataRow conectservidor_row in _conectservidor.Rows)
                            {
                                if (conectservidor_row["TABELA"].ToString() == table.Name)
                                { 
                                    switch (conectservidor_row["CONEXAO"].ToString().ToLower().Trim())
                                    {                                       
                                        case "clsinfo.conexaorelatorios":
                                            servidor = clsInfo.caminhorelatorios;
                                            break;
                                        case "clsinfo.conexaorpt":
                                            servidor = clsInfo.conexaorpt;
                                            break;
                                        //case "clsinfo.conexaosgp":
                                        //    servidor = clsInfo.conexaosgp;
                                        //    break;
                                        case "clsinfo.conexaosqlbanco":
                                            servidor = clsInfo.conexaosqlbanco;
                                            break;
                                        case "clsinfo.conexaosqldados":
                                            servidor = clsInfo.conexaosqldados;
                                            break;
                                        //Inutilizado em 19/07/2013 por Brian Lima, esta variável não tem mais utilidade
                                        //case "clsinfo.conexaosqldadosbanco":
                                        //    servidor = clsInfo.conexaosqldadosbanco;
                                        //    break;
                                        case "clsinfo.conexaosqlfolha":
                                            servidor = clsInfo.conexaosqlfolha;
                                            break;
                                        //case "clsinfo.conexaosqllog":
                                        //    servidor = clsInfo.conexaosqllog;
                                        //    break;
                                        //case "clsinfo.conexaosqlmald":
                                        //    servidor = clsInfo.conexaosqlmald;
                                        //    break;
                                        //case "clsinfo.conexaosqlos":
                                        //    servidor = clsInfo.conexaosqlos;
                                        //    break;
                                        //case "clsinfo.conexaosqlportaria":
                                        //    servidor = clsInfo.conexaosqlportaria;
                                        //    break;
                                        //case "clsinfo.conexaosqltransferencia":
                                        //    servidor = clsInfo.conexaosqltransferencia;
                                        //    break;                                        
                                        default:
                                            servidor = clsInfo.conexaosqldados;
                                            break;
                                    }

                                    crConnection.UserID = servidor.Split('=').GetValue(3).ToString().Split(';').GetValue(0).ToString();
                                    crConnection.ServerName = servidor.Split('=').GetValue(1).ToString().Split(';').GetValue(0).ToString();
                                    crConnection.DatabaseName = servidor.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString();
                                    crConnection.Password = servidor.Split('=').GetValue(4).ToString().Split(';').GetValue(0).ToString();
                                    break;

                                    // Percorre a lista, obtendo o nome de cada uma das propriedades
                                    //foreach (PropertyInfo p in propriedades)
                                    //{
                                    //     Obtém o nome da propriedade...                                       

                                    //    if (conectservidor_row["CONEXAO"].ToString() == "clsInfo." + p.Name)
                                    //    {
                                    //        servidor = p.ToString();
                                    //        //
                                    //        Type type = p.GetType();
                                    //        PropertyInfo stringx = type.GetProperty("Length");
                                    //        object propertyValue = stringx.GetValue(p, null);



                                    //        //
                                    //        break;
                                    //    }                                     
                                    //}   
                                }    
                            }
                        }         

                        table.LogOnInfo.ConnectionInfo.UserID = crConnection.UserID;
                        table.LogOnInfo.ConnectionInfo.Password = crConnection.Password;
                        table.LogOnInfo.ConnectionInfo.DatabaseName = crConnection.DatabaseName;
                        table.LogOnInfo.ConnectionInfo.ServerName = crConnection.ServerName;

                        table.LogOnInfo.ConnectionInfo.Type = ConnectionInfoType.SQL;
                        table.LogOnInfo.ConnectionInfo.IntegratedSecurity = false;

                        reportDocument1.Database.Tables[x].ApplyLogOnInfo(table.LogOnInfo);
                       
                        reportDocument1.Refresh();

                        x = x + 1;                      
                    }
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                this.ofdArquivo.FileName = ""; //Limpa o campo do nome do arquivo quando o a janela aparecer

                this.ofdArquivo.Filter = "Crystal Report(*.RPT;)|*.RPT|TODOS(*.*)|*.*";

                this.ofdArquivo.ShowDialog(); //mostra a janela

                tbxLayoutCrystal.Text = this.ofdArquivo.FileName.ToString();

                tbxCrystal.Text = this.ofdArquivo.SafeFileNames[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmCrystalReportOLE_Load(object sender, EventArgs e)
        {
            bwrCrystal.RunWorkerAsync();
        }           

        private void Email_Click(object sender, EventArgs e)
        {
              try
            {               
                //exportando o arquivo
                if (nome_aleatorio != "")
                {
                    reportDocument1.ExportToDisk(ExportFormatType.PortableDocFormat, clsInfo.arquivos + nome_aleatorio + ".pdf");
                
                    arquivoanexo = clsInfo.arquivos + nome_aleatorio + ".pdf";
                }
                else
                {
                    crystalReportViewer1.ExportReport();                   
                }

                //retorna o SMTP e porta
                smtp = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select smtp from EMPRESAS where ID = " + clsInfo.zempresaid, "");
                smtpporta = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select smtpporta from EMPRESAS where ID = " + clsInfo.zempresaid, "");
                smtpemail = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select email from USUARIO ID = " + clsInfo.zusuarioid);
                smtpsenha = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select emailsenha from USUARIO where ID = " + clsInfo.zusuarioid, "");

                frmEnviodeEmail frmEnviodeEmail;

                if (assunto != null && assunto.Length > 0)
                {
                    frmEnviodeEmail = new frmEnviodeEmail();
                    frmEnviodeEmail.Init(smtp, smtpporta, smtpemail, smtpsenha, arquivoanexo, destinatario, assunto, 0, "");
                }
                else
                {
                    frmEnviodeEmail = new frmEnviodeEmail();
                    frmEnviodeEmail.Init(smtp, smtpporta, smtpemail, smtpsenha, arquivoanexo, destinatario, 0, "");
                }

                clsFormHelper.AbrirForm(this.MdiParent, frmEnviodeEmail, conexao);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void bwrCrystal_DoWork(object sender, DoWorkEventArgs e)
        {
            reportDocument1.Load(localarquivo + arquivo);

            DataTable conectservidor = new DataTable();
            conectservidor = clsSelectInsertUpdateBLL.Select(clsInfo.conexaorpt, 
                      "REL4PARAMETRO inner join REL3TIPO on REL4PARAMETRO.IDREL3TIPO = REL3TIPO.id ", "CONEXAO, TABELA",
                            " REL4PARAMETRO.IDREL3TIPO = " + idreltipo, "CONEXAO");

            AssignConnectionInfo(reportDocument1, conectservidor);
            reportDocument1.RecordSelectionFormula = sintaxecrystal.ToString(); 
        }

        private void bwrCrystal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (parametros.Count != 0)
            {
                crystalReportViewer1.ParameterFieldInfo = parametros;
            }
            crystalReportViewer1.ReportSource = reportDocument1;
            crystalReportViewer1.Zoom(86);
            pbxAguarde.Visible = false;           
        } 
    }
}
