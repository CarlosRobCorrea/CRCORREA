using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorreaFuncoes
{
    public partial class frmCrystalReportsreduzido: Form
    {
        private String localarquivo;
        private String arquivo;    
        private String smtp;
        private String smtpporta;
        private String smtpemail;
        private String smtpsenha;
        private String arquivoanexo = "";       
        private String destinatario;
        private String assunto;
        private String nome_aleatorio = "";
        private String sintaxecrystal;
        private String metodo = "";
        private String servidor;
        private String conexao;

        private DataTable tabela;
        private DataSet ds;
               
        private ParameterFields parametros;

        
        

        private ConnectionInfo crConnection = new ConnectionInfo();

        public frmCrystalReportsreduzido()
        {
            InitializeComponent();
        }

        public void Init(String _localaquivo, String _arquivo, DataTable _tabela, ParameterFields _paramentros, String _destinatario, String _conexao)
        {
            conexao = _conexao;
            tspCrystal.Cursor = Cursors.Hand;
            localarquivo = _localaquivo;
            arquivo = _arquivo;
            tabela = _tabela;           
            parametros = _paramentros;
            destinatario = _destinatario;

            
            
        }

        public void Init(String _localaquivo, String _arquivo, DataSet _tabela, ParameterFields _paramentros, String _destinatario, String _conexao)
        {
            tspCrystal.Cursor = Cursors.Hand;
            localarquivo = _localaquivo;
            arquivo = _arquivo;
            ds = _tabela;
            parametros = _paramentros;
            destinatario = _destinatario;
            conexao = _conexao;
            
            
        }

        public void Init(String _localaquivo, String _arquivo, DataTable _tabela, ParameterFields _paramentros, String _destinatario, String _nome_aleatorio, String _assunto, String _conexao)
        {
            localarquivo = _localaquivo;
            arquivo = _arquivo;
            tabela = _tabela;
            parametros = _paramentros;
            destinatario = _destinatario;
            assunto = _assunto;
            conexao = _conexao;
            
            

            nome_aleatorio = _nome_aleatorio;
        }

        public void Init(String _localaquivo, String _arquivo, DataSet _tabela, ParameterFields _paramentros, String _destinatario, String _nome_aleatorio, String _assunto, String _conexao)
        {
            localarquivo = _localaquivo;
            arquivo = _arquivo;
            ds = _tabela;
            parametros = _paramentros;
            destinatario = _destinatario;
            assunto = _assunto;
            conexao = _conexao;
            
            

            nome_aleatorio = _nome_aleatorio;
        }


        public void Init(String _localaquivo, String _arquivo, String _sintaxecrystal, ParameterFields _parametros, String _destinatario, String _nome_aleatorio, String _assunto, String _servidor, String _conexao)
        {
            localarquivo = _localaquivo;
            arquivo = _arquivo;           
            parametros = _parametros;
            destinatario = _destinatario;
            assunto = _assunto;
            conexao = _conexao;
            
            

            nome_aleatorio = _nome_aleatorio;
            sintaxecrystal = _sintaxecrystal;
            metodo = "CR_OLE";
            servidor = _servidor;
        }

        private void frmCrystalReportsreduzido_Load(object sender, EventArgs e)
         {
            if (clsInfo.zfilial == 0)
            {
                tspEmail.Enabled = false;
            }

            if (this.MdiParent == null)
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterScreen;
            }

            bwrCrystal.RunWorkerAsync();
        }

        private void bwrCrystal_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (metodo)
            {
                case "":
                    if (clsInfo.rdtDocumento == null)
                    {
                        clsInfo.rdtDocumento = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    }
                    arquivoanexo = "";
                    if (parametros.Count != 0)
                    {
                        CrystalReportViewer.ParameterFieldInfo = parametros;
                    }
                    clsInfo.rdtDocumento.Load(localarquivo + arquivo);
                    if (ds != null)
                    {
                        clsInfo.rdtDocumento.SetDataSource(ds);

                        Int32 i = 0;
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (i == 0)
                            {
                                i++;
                                continue;
                            }

                            clsInfo.rdtDocumento.Subreports[dt.TableName].SetDataSource(dt);
                        }
                    }
                    else
                    {
                        clsInfo.rdtDocumento.SetDataSource(tabela);
                    }
                    break;

                case "CR_OLE":

                    if (clsInfo.rdtDocumento == null)
                    {
                        clsInfo.rdtDocumento = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    }

                    arquivoanexo = "";

                    if (parametros.Count != 0)
                    {
                        CrystalReportViewer.ParameterFieldInfo = parametros;
                    }

                    clsInfo.rdtDocumento.Load(localarquivo + arquivo);                   
                    crConnection.UserID = servidor.Split('=').GetValue(3).ToString().Split(';').GetValue(0).ToString();
                    crConnection.ServerName = servidor.Split('=').GetValue(1).ToString().Split(';').GetValue(0).ToString();
                    crConnection.DatabaseName = servidor.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString();
                    crConnection.Password = servidor.Split('=').GetValue(4).ToString().Split(';').GetValue(0).ToString();

                    AssignConnectionInfo(clsInfo.rdtDocumento, crConnection);

                    clsInfo.rdtDocumento.RecordSelectionFormula = sintaxecrystal.ToString();                                   
                    break;

                default:
                    break;
            }
        }

        private void AssignConnectionInfo(ReportDocument document, ConnectionInfo crConnection)
        {
            try
            {
                CrystalReportViewer.LogOnInfo = new TableLogOnInfos();
                int x = 0;
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in document.Database.Tables)
                {
                    TableLogOnInfo logOnInfo = new TableLogOnInfo();
                    logOnInfo = table.LogOnInfo;
                    if (logOnInfo != null)
                    {
                        table.ApplyLogOnInfo(table.LogOnInfo);
                        table.LogOnInfo.TableName = table.Name;
                        table.LogOnInfo.ReportName = arquivo;
                        table.LogOnInfo.ConnectionInfo.UserID = crConnection.UserID;
                        table.LogOnInfo.ConnectionInfo.Password = crConnection.Password;
                        table.LogOnInfo.ConnectionInfo.DatabaseName = crConnection.DatabaseName;
                        table.LogOnInfo.ConnectionInfo.ServerName = crConnection.ServerName;
                        table.LogOnInfo.ConnectionInfo.Type = ConnectionInfoType.SQL;
                        table.LogOnInfo.ConnectionInfo.IntegratedSecurity = false;
                        clsInfo.rdtDocumento.Database.Tables[x].ApplyLogOnInfo(table.LogOnInfo);
                        clsInfo.rdtDocumento.Refresh();
                        x = x + 1;                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateSortField(ReportDocument reportDocument, String tableName, String fieldName)
        {
            DatabaseFieldDefinition databaseFieldDefinition;

            databaseFieldDefinition = reportDocument.Database.Tables[tableName].Fields[fieldName];

            SortFields sortFields = reportDocument.DataDefinition.SortFields;

            if (sortFields[0].SortType == SortFieldType.RecordSortField)
            {
                sortFields[0].Field = databaseFieldDefinition;
                sortFields[0].SortDirection = SortDirection.AscendingOrder;
            }
        }

        private void bwrCrystal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {           
            CrystalReportViewer.ReportSource = clsInfo.rdtDocumento;          
         
            CrystalReportViewer.Zoom(86);
            
            tspRetornar.Enabled = true;
            
            if (clsInfo.zfilial == 0)
            {
                tspEmail.Enabled = false;
            }

            pbxAguarde.Visible = false;
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void tspEmail_Click(object sender, EventArgs e)
        {
             try
            {               
                //exportando o arquivo
                if (nome_aleatorio != "")
                {
                    clsInfo.rdtDocumento.ExportToDisk(ExportFormatType.PortableDocFormat, clsInfo.arquivos + nome_aleatorio + ".pdf");
                
                    arquivoanexo = clsInfo.arquivos + nome_aleatorio + ".pdf";
                }
                else
                {

                    CrystalReportViewer.ExportReport();
                }

                //retorna o SMTP e porta
                smtp = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select smtp from EMPRESAS where ID = " + clsInfo.zempresaid, "");
                smtpporta = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select smtpporta from EMPRESAS where ID = " + clsInfo.zempresaid, "");

                 //retorna o email e senha do usuario
                smtpemail = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "seelct email from USUARIO where ID = " + clsInfo.zusuarioid, "");
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
    }
}

