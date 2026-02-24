using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmBancos : Form
    {
        clsFinanceiro clsFinanceiro = new clsFinanceiro();

        clsBancosInfo clsBancosInfo = new clsBancosInfo();
        clsBancosInfo clsBancosInfoOld = new clsBancosInfo();
        clsBancosBLL clsBancosBLL = new clsBancosBLL();

        String conexaobanco;
        String conexaodados;

        SqlDataAdapter sda;

        String query;

        public static Int32 id = 0;
        Int32 idbanco;
        Int32 idcontabil;
        Int32 idbcoespecie;
        Int32 idbcomoeda;
        Int32 idbcoaceite;
        Int32 idbcoprotestar;
        Int32 idbcoprotestar2;
        Int32 idbcoimpressao;
        Int32 idbcomodalidade;

        DataGridViewRowCollection rows;

        Boolean carregandoDocumentos;
        DataTable dtDocumentos;
        BackgroundWorker bwrDocumentos;

        String tipoDocumento;

        // Colunas do Documentos de Acordo com a Lupa que apertar
        GridColuna[] dtDocumentosColunas = new GridColuna[]
                                        {
                                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter),
                                            new GridColuna("Codigo", "CODIGO", 40, true, DataGridViewContentAlignment.MiddleLeft),
                                            new GridColuna("Descrição", "NOME", 350, true, DataGridViewContentAlignment.MiddleLeft)
                                        };

        public frmBancos()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            conexaobanco = clsInfo.conexaosqlbanco;
            conexaodados = clsInfo.conexaosqldados;
            id = _id;
            rows = _rows;

            clsBancosBLL = new clsBancosBLL();

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from tab_bancosespecie order by codigo", tbxBcoEspecie);
        }

        private void frmBancos_Load(object sender, EventArgs e)
        {
            try
            {
                BancosCarregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message: " + ex.Message + "/nStack Stace: " + ex.StackTrace, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void BancosCarregar()
        {
            /*            Arquivo.Write(ccarteira.Substring(6, 2).PadLeft(3, '0')); // POS 18/20  [03] [N] 'Carteira do convênio(2)
                        Arquivo.Write(ccarteira.Substring(8, 3).PadRight(3, '0')); // POS 21/23  [03] [N] 'Variacao
                        Arquivo.Write(ccarteira.Substring(0, 6).PadLeft(6, '0'));                     // POS 24/29  [06] [N] 'Numero do convenio
                        */

            clsBancosInfoOld = new clsBancosInfo();
            if (id == 0)
            {
                clsBancosInfo = new clsBancosInfo();

                clsBancosInfo.codigodetransmissao = "";               
                clsBancosInfo.agencia = "";
                clsBancosInfo.ativo = "S";
                clsBancosInfo.banco = 0;
                clsBancosInfo.bcoaceite = "N";
                clsBancosInfo.bcoespecie = "";
                clsBancosInfo.bcoimpressao = "";
                clsBancosInfo.bcomodalidade = "";
                clsBancosInfo.bcomoeda = "";
                clsBancosInfo.bcoprotestar = "";
                clsBancosInfo.carteira = "";
                clsBancosInfo.compensar = 0;
                clsBancosInfo.conta = 0;
                clsBancosInfo.contabil = "";
                clsBancosInfo.contacor = "";
                clsBancosInfo.convenio = "";
                clsBancosInfo.ctatransitoria = "N";
                clsBancosInfo.id = 0;
                clsBancosInfo.idbanco = clsInfo.zbanco;
                clsBancosInfo.idbcoaceite = 0;
                clsBancosInfo.idbcoimpressao = 0;
                clsBancosInfo.idbcomodalidade = 0;
                clsBancosInfo.idbcomoeda = 0;
                clsBancosInfo.idbcoprotestar = 0;
                clsBancosInfo.idbcoprotestar2 = 0;
                clsBancosInfo.idcontabil = 0;
                clsBancosInfo.layoutposicoes = "";
                clsBancosInfo.limite = 0;
                clsBancosInfo.nome = "";
                clsBancosInfo.nroboleto = "";
                clsBancosInfo.subcarteira = "";
                clsBancosInfo.txboleto = 0;
                clsBancosInfo.txcartorio = 0;
                clsBancosInfo.variacao = "";
            }
            else
            {
                clsBancosInfo = clsBancosBLL.Carregar(id, conexaobanco);
            }
            BancosCampos(clsBancosInfo);
            BancosFillInfo(clsBancosInfoOld);

        }

        private void BancosCampos(clsBancosInfo info)  // Inclusão
        {
            //apenas visualização
            tbxBcoNroInscrEmpresa.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc FROM EMPRESA where id = " + clsInfo.zempresaid, "");
            id = info.id;
            idbanco = info.idbanco;
            idcontabil = info.idcontabil;
            idbcoespecie = info.idbcoespecie;
            idbcomoeda = info.idbcomoeda;
            idbcoaceite = info.idbcoaceite;
            idbcoprotestar = info.idbcoprotestar;
            idbcoprotestar2 = info.idbcoprotestar2;
            idbcoimpressao = info.idbcoimpressao;
            idbcomodalidade = info.idbcomodalidade;

            tbxConta.Text = info.conta.ToString();
            if (idbanco > 0)
            {
                tbxBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idbanco);
                tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idbanco);
            }
            else
            {
                tbxBanco.Text = info.banco.ToString();
                tbxNome.Text = info.nome;
            }

            tbxAgencia.Text = info.agencia;
            tbxContaCor.Text = info.contacor;
            tbxContabil.Text = info.contabil;

            if (info.ctatransitoria == "S")
            {
                rbnCtaTransitoriaS.Checked = true;
            }
            else
            {
                rbnCtaTransitoriaN.Checked = true;
            }

            tbxTxBoleto.Text = info.txboleto.ToString();
            tbxTxCartorio.Text = info.txcartorio.ToString();

            if (info.ativo == "S")
            {
                rbnAtivoS.Checked = true;
            }
            else
            {
                rbnAtivoN.Checked = true;
            }

            tbxCarteira.Text = info.carteira;
            tbxConvenio.Text = info.convenio;
            tbxSubCarteira.Text = info.subcarteira;
            tbxVariacao.Text = info.variacao;
            tbxCompensar.Text = info.compensar.ToString();

            if (idbcoespecie > 0)
            {
                tbxBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSESPECIE where ID= " + idbcoespecie);
                lblBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSESPECIE where ID= " + idbcoespecie);
            }

            //idcontabil = info.idcontabil;

            if (idbcomoeda > 0)
            {
                tbxBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMOEDA where ID= " + idbcomoeda);
                lblBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSMOEDA where ID= " + idbcomoeda);
            }
            if (idbcoaceite > 0)
            {
                tbxBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSACEITE where ID= " + idbcoaceite);
                lblBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSACEITE where ID= " + idbcoaceite);
            }
            if (idbcoprotestar > 0)
            {
                tbxBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar);
                lblBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar);
            }
            if (idbcoimpressao > 0)
            {
                tbxBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSIMPRESSAO where ID= " + idbcoimpressao);
                lblBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSIMPRESSAO where ID= " + idbcoimpressao);
            }
            if (idbcomodalidade > 0)
            {
                tbxBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMODALIDADE where ID= " + idbcomodalidade);
                lblBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSMODALIDADE where ID= " + idbcomodalidade);
            }

            tbxLimite.Text = info.limite.ToString();
            tbxNroBoleto.Text = info.nroboleto;
            tbxLayoutPosicoes.Text = info.layoutposicoes;

            if (idbcoprotestar2 > 0)
            {
                tbxBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar2);
                lblBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar2);
            }
            tbxBcoCodigoTransmissao.Text = info.codigodetransmissao.ToString();
        }

        private void BancosFillInfo(clsBancosInfo info)  // Inclusão
        {
            info.id = id;
            info.idbanco = idbanco;
            info.idcontabil = idcontabil;
            info.idbcoespecie = idbcoespecie;
            info.idbcomoeda = idbcomoeda;
            info.idbcoaceite = idbcoaceite;
            info.idbcoprotestar = idbcoprotestar;
            info.idbcoprotestar2 = idbcoprotestar2;
            info.idbcoimpressao = idbcoimpressao;
            info.idbcomodalidade = idbcomodalidade;
            

            info.conta = clsParser.Int32Parse(tbxConta.Text);
            info.banco = clsParser.Int32Parse(tbxBanco.Text);
            info.nome = tbxNome.Text;
            info.agencia = tbxAgencia.Text;
            info.contacor = tbxContaCor.Text;
            info.contabil = tbxContabil.Text;
            if (rbnCtaTransitoriaS.Checked == true)
            {
                info.ctatransitoria = "S";
            }
            else
            {
                info.ctatransitoria = "N";
            }

            info.txboleto = clsParser.DecimalParse(tbxTxBoleto.Text);
            info.txcartorio = clsParser.DecimalParse(tbxTxCartorio.Text);
            if (rbnAtivoS.Checked == true)
            {
                info.ativo = "S";
            }
            else
            {
                info.ativo = "N";
            }

            info.carteira = tbxCarteira.Text;
            info.convenio = tbxConvenio.Text;
            info.subcarteira = tbxSubCarteira.Text;
            info.variacao = tbxVariacao.Text;
            info.compensar = clsParser.Int32Parse(tbxCompensar.Text);

            info.bcoespecie = tbxBcoEspecie.Text;
            info.bcomoeda = tbxBcoMoeda.Text;
            info.bcoaceite = tbxBcoAceite.Text;
            info.bcoprotestar = tbxBcoProtestar.Text;      
            info.bcoimpressao = tbxBcoImpressao.Text;
            info.bcomodalidade = tbxBcoModalidade.Text;

            info.limite = clsParser.DecimalParse(tbxLimite.Text);
            info.nroboleto = tbxNroBoleto.Text;
            info.layoutposicoes = tbxLayoutPosicoes.Text;
            info.codigodetransmissao = tbxBcoCodigoTransmissao.Text;           
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
            clsVisual.ControlLeave(sender);
            //            clsVisual.FormatarCampoNumerico(sender);

        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void frmBancos_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
            
            
        }

        public void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == btnIdBanco.Name)
                {
                    idbanco = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBanco.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    if (tbxBanco.Text != "0")
                    {
                        tbxNome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    }
                    tbxAgencia.Select();
                    clsInfo.znomegrid = "";
                }
                else if (clsInfo.znomegrid == btnIdBcoEspecie.Name)
                {
                    idbcoespecie = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoEspecie.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoEspecie.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();

                }
                else if (clsInfo.znomegrid == btnIdBcoMoeda.Name)
                {
                    idbcomoeda = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoMoeda.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoMoeda.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }
                else if (clsInfo.znomegrid == btnIdBcoAceite.Name)
                {
                    idbcoaceite = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoAceite.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoAceite.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }
                else if (clsInfo.znomegrid == btnIdBcoProtestar.Name)
                {
                    idbcoprotestar = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoProtestar.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoProtestar.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();

                }
                else if (clsInfo.znomegrid == btnIdBcoImpressao.Name)
                {
                    idbcoimpressao = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoImpressao.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoImpressao.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }
                else if (clsInfo.znomegrid == btnIdBcoModalidade.Name)
                {
                    idbcomodalidade = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoModalidade.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoModalidade.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }
                else if (clsInfo.znomegrid == btnIdProtestar2.Name)
                {
                    idbcoprotestar2 = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBcoProtestar2.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    lblBcoProtestar2.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }

            }
            if (ctl.Name == tbxBanco.Name)
            {
                idbanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOS where CODIGO=" + tbxBanco.Text + " "));
                if (idbanco == 0)
                {
                    idbanco = clsInfo.zbanco;
                }
                tbxBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idbanco);
                tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idbanco);
            }
            if (ctl.Name == tbxBcoEspecie.Name)
            {
                idbcoespecie = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSESPECIE where TAB_BANCOSESPECIE.CODIGO='" + tbxBcoEspecie.Text + "'  AND  TAB_BANCOSESPECIE.IDCODIGO = " + idbanco + " "));
                if (idbcoespecie == 0)
                {
                    //idbcoespecie = //clsInfo.zbanco;
                }
                tbxBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSESPECIE where ID= " + idbcoespecie);
                lblBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSESPECIE where ID= " + idbcoespecie);
            }
            if (ctl.Name == tbxBcoMoeda.Name)
            {
                idbcomoeda = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSMOEDA where CODIGO='" + tbxBcoMoeda.Text + "' AND  TAB_BANCOSMOEDA.IDCODIGO = " + idbanco + " "));
                if (idbcomoeda == 0)
                {
                    //idbcoespecie = //clsInfo.zbanco;
                }
                tbxBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMOEDA where ID= " + idbcomoeda);
                lblBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSMOEDA where ID= " + idbcomoeda);
            }
            if (ctl.Name == tbxBcoAceite.Name)
            {
                idbcoaceite = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSACEITE where CODIGO='" + tbxBcoAceite.Text + "' AND  TAB_BANCOSACEITE.IDCODIGO = " + idbanco + " "));
                if (idbcoaceite == 0)
                {
                    //idbcoaceite = //clsInfo.zbanco;
                }
                tbxBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSACEITE where ID= " + idbcoaceite);
                lblBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSACEITE where ID= " + idbcoaceite);
            }

            if (ctl.Name == tbxBcoProtestar.Name)
            {
                idbcoprotestar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSPROTESTAR where CODIGO='" + tbxBcoProtestar.Text + "' AND  TAB_BANCOSPROTESTAR.IDCODIGO = " + idbanco + " "));
                if (idbcoprotestar == 0)
                {
                    //idbcoprotestar = //clsInfo.zbanco;
                }
                tbxBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar);
                lblBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar);
            }

            if (ctl.Name == tbxBcoImpressao.Name)
            {
                idbcoimpressao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSIMPRESSAO where CODIGO='" + tbxBcoImpressao.Text + "' AND  TAB_BANCOSIMPRESSAO.IDCODIGO = " + idbanco + " "));
                if (idbcoimpressao == 0)
                {
                    //idbcoimpressao = //clsInfo.zbanco;
                }
                tbxBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSIMPRESSAO where ID= " + idbcoimpressao);
                lblBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSIMPRESSAO where ID= " + idbcoimpressao);
            }
            if (ctl.Name == tbxBcoModalidade.Name)
            {
                idbcomodalidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSMODALIDADE where CODIGO='" + tbxBcoModalidade.Text + "' AND  TAB_BANCOSMODALIDADE.IDCODIGO = " + idbanco + " "));
                if (idbcomodalidade == 0)
                {
                    //idbcomodalidade = //clsInfo.zbanco;
                }
                tbxBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMODALIDADE where ID= " + idbcomodalidade);
                lblBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSMODALIDADE where ID= " + idbcomodalidade);
            }

            if (ctl.Name == tbxBcoProtestar2.Name)
            {
                idbcoprotestar2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSPROTESTAR where CODIGO='" + tbxBcoProtestar2.Text + "'  AND  TAB_BANCOSPROTESTAR.IDCODIGO = " + idbanco + " "));
                if (idbcoprotestar2 == 0)
                {
                    //idbcoprotestar2 = //clsInfo.zbanco;
                }
                tbxBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar2);
                lblBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar2);
            }

            // Se for banco do Brasil e o campo convenio estiver em branco
            if (clsParser.Int32Parse(tbxBanco.Text) == 1 && tbxConvenio.Text.Length == 0 && tbxCarteira.Text.Length > 0)
            {
                // carteira
                tbxSubCarteira.Text = tbxCarteira.Text.Substring(6, 2).PadLeft(3, '0');
                // Variação
                tbxVariacao.Text = tbxCarteira.Text.Substring(8, 3).PadRight(3, '0');
                // Convenio
                tbxConvenio.Text = tbxCarteira.Text.Substring(0, 6).PadLeft(6, '0');
            }
        }


        private void tspSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == DialogResult.Cancel)
            {
                return;
            }
            this.Close();
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    BancosCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                BancosCarregar();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                BancosCarregar();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    BancosCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoes() == true)
            {
                tspSalvar.PerformClick();
            }
            else
            {
                this.Close();
            }

        }
        private Boolean HouveModificacoes()
        {
            clsBancosInfo = new clsBancosInfo();
            BancosFillInfo(clsBancosInfo);
            if (clsBancosBLL.Equals(clsBancosInfo, clsBancosInfoOld) == false)
            {
                return true;
            }
            return false;
        }
        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                BancosSalvar();
            }
            return drt;
        }
        private void BancosSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: Bancos
                clsBancosInfo = new clsBancosInfo();
                BancosFillInfo(clsBancosInfo);
                if (id == 0)
                {
                    clsBancosInfo.id = clsBancosBLL.Incluir(clsBancosInfo, clsInfo.conexaosqlbanco);
                }
                else
                {
                    clsBancosBLL.Alterar(clsBancosInfo, clsInfo.conexaosqlbanco);
                }
                tse.Complete();
            }
        }



        private void btnIdBanco_Click(object sender, EventArgs e)
        {
            frmTab_BancosVis frmTab_BancosVis = new frmTab_BancosVis();
            frmTab_BancosVis.Init(conexaodados);
            clsInfo.znomegrid = btnIdBanco.Name;

            clsFormHelper.AbrirForm(this.MdiParent, frmTab_BancosVis, conexaodados);
        }

    

        private void bwrDocumentos_Run()
        {
            if (carregandoDocumentos == false)
            {
                carregandoDocumentos = true;

                bwrDocumentos = new BackgroundWorker();
                bwrDocumentos.DoWork += new DoWorkEventHandler(bwrDocumentos_DoWork);
                bwrDocumentos.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrDocumentos_RunWorkerCompleted);
                bwrDocumentos.RunWorkerAsync();
            }
        }


        private void bwrDocumentos_DoWork(object sender, DoWorkEventArgs e)
        {
            query = "";
            dtDocumentos = new DataTable();

            if (tipoDocumento.ToUpper() == "ESPECIE")
            {
                query = "SELECT TAB_BANCOSESPECIE.ID,TAB_BANCOSESPECIE.CODIGO, TAB_BANCOSESPECIE.NOME " +
                               "FROM TAB_BANCOSESPECIE ";
                query = query + " WHERE  TAB_BANCOSESPECIE.IDCODIGO = " + idbanco;
                query = query + " ORDER BY TAB_BANCOSESPECIE.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "MOEDA")
            {
                query = "SELECT TAB_BANCOSMOEDA.ID,TAB_BANCOSMOEDA.CODIGO, TAB_BANCOSMOEDA.NOME " +
                               "FROM TAB_BANCOSMOEDA ";
                query = query + " WHERE  TAB_BANCOSMOEDA.IDCODIGO = " + idbanco;
                query = query + " ORDER BY TAB_BANCOSMOEDA.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "ACEITE")
            {
                query = "SELECT TAB_BANCOSACEITE.ID,TAB_BANCOSACEITE.CODIGO, TAB_BANCOSACEITE.NOME " +
                               "FROM TAB_BANCOSACEITE ";
                query = query + " WHERE  TAB_BANCOSACEITE.IDCODIGO = " + idbanco;
                query = query + " ORDER BY TAB_BANCOSACEITE.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "PROTESTAR")
            {
                query = "SELECT TAB_BANCOSPROTESTAR.ID,TAB_BANCOSPROTESTAR.CODIGO, TAB_BANCOSPROTESTAR.NOME " +
                               "FROM TAB_BANCOSPROTESTAR ";
                query = query + " WHERE  TAB_BANCOSPROTESTAR.IDCODIGO = " + idbanco;
                query = query + " ORDER BY TAB_BANCOSPROTESTAR.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "IMPRESSAO")
            {
                query = "SELECT TAB_BANCOSIMPRESSAO.ID,TAB_BANCOSIMPRESSAO.CODIGO, TAB_BANCOSIMPRESSAO.NOME " +
                               "FROM TAB_BANCOSIMPRESSAO ";
                query = query + " WHERE  TAB_BANCOSIMPRESSAO.IDCODIGO = " + idbanco;
                query = query + " ORDER BY TAB_BANCOSIMPRESSAO.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "MODALIDADE")
            {
                query = "SELECT TAB_BANCOSMODALIDADE.ID,TAB_BANCOSMODALIDADE.CODIGO, TAB_BANCOSMODALIDADE.NOME " +
                               "FROM TAB_BANCOSMODALIDADE ";
                query = query + " WHERE  TAB_BANCOSMODALIDADE.IDCODIGO = " + idbanco;
                query = query + " ORDER BY TAB_BANCOSMODALIDADE.CODIGO ";
            }
            else if (tipoDocumento.ToUpper() == "PROTESTAR2")
            {
                query = "SELECT TAB_BANCOSPROTESTAR.ID,TAB_BANCOSPROTESTAR.CODIGO, TAB_BANCOSPROTESTAR.NOME " +
                               "FROM TAB_BANCOSPROTESTAR ";
                query = query + " WHERE  TAB_BANCOSPROTESTAR.IDCODIGO = " + idbanco;
                query = query + " ORDER BY TAB_BANCOSPROTESTAR.CODIGO ";
            }
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.Fill(dtDocumentos);
        }

        private void bwrDocumentos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                dgvDocumentos.DataSource = dtDocumentos;
                clsGridHelper.MontaGrid2(dgvDocumentos, dtDocumentosColunas, true);
                clsGridHelper.FontGrid(dgvDocumentos, 7);                
                Ponteiro(tipoDocumento);
                carregandoDocumentos = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoDocumentos = false;
            }
        }
        private void btnIdBcoEspecie_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Especie";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdBcoMoeda_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Moeda";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();

        }

        private void btnIdBcoAceite_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Aceite";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdBcoProtestar_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Protestar";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdBcoImpressao_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Impressao";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdBcoModalidade_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Modalidade";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void btnIdProtestar2_Click(object sender, EventArgs e)
        {
            tipoDocumento = "Protestar2";
            gbxDocumentos.Text = tipoDocumento;
            bwrDocumentos_Run();
        }

        private void dgvDocumentos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvDocumentos.CurrentRow != null)
            {
                if (tipoDocumento.ToUpper() == "ESPECIE")
                {
                    idbcoespecie = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoEspecie.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoEspecie.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoEspecie.Select();
                    tbxBcoEspecie.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "MOEDA")
                {
                    idbcomoeda = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoMoeda.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoMoeda.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoMoeda.Select();
                    tbxBcoMoeda.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "ACEITE")
                {
                    idbcoaceite = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoAceite.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoAceite.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoAceite.Select();
                    tbxBcoAceite.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "PROTESTAR")
                {
                    idbcoprotestar = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoProtestar.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoProtestar.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoProtestar.Select();
                    tbxBcoProtestar.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "IMPRESSAO")
                {
                    idbcoimpressao = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoImpressao.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoImpressao.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoImpressao.Select();
                    tbxBcoImpressao.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "MODALIDADE")
                {
                    idbcomodalidade = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoModalidade.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoModalidade.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoModalidade.Select();
                    tbxBcoModalidade.SelectAll();
                }
                else if (tipoDocumento.ToUpper() == "PROTESTAR2")
                {
                    idbcoprotestar2 = (Int32)dgvDocumentos.CurrentRow.Cells[0].Value;
                    tbxBcoProtestar2.Text = dgvDocumentos.CurrentRow.Cells[1].Value.ToString();
                    lblBcoProtestar2.Text = dgvDocumentos.CurrentRow.Cells[2].Value.ToString();
                    tbxBcoProtestar2.Select();
                    tbxBcoProtestar2.SelectAll();
                }
            }
        }

        private void Ponteiro(String _tipoDocumento)
        {
            dgvDocumentos.MultiSelect = false;
            String _codigo = "";
            
            switch (_tipoDocumento.ToUpper())
            {
                case "ESPECIE":
                    _codigo = tbxBcoEspecie.Text;
                    break;
                case "MOEDA":
                    _codigo = tbxBcoMoeda.Text;
                    break;
                case "ACEITE":
                    _codigo = tbxBcoAceite.Text;
                    break;
                case "PROTESTAR":
                    _codigo = tbxBcoProtestar.Text;
                    break;
                case "IMPRESSAO":
                    _codigo = tbxBcoImpressao.Text;
                    break;
                case "MODALIDADE":
                    _codigo = tbxBcoModalidade.Text;
                    break;
                case "PROTESTAR2":
                    _codigo = tbxBcoProtestar2.Text;
                    break;
                default:
                    _codigo = "";
                    break;
            }            

            if (clsGridHelper.SelecionaLinha_ReturnBoolean(_codigo, dgvDocumentos, "codigo") == false)
            {
                if (dgvDocumentos.Rows.Count > 0)
                {
                    dgvDocumentos.CurrentCell = dgvDocumentos.Rows[0].Cells["codigo"];
                }
            }            
        }

        private void gbxBanco_Enter(object sender, EventArgs e)
        {

        }
    }
}
