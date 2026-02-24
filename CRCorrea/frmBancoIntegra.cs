using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmBancoIntegra : Form
    {
        clsBancosInfo clsBancosInfo;
        clsBancosInfo clsBancosInfoOld;
        clsBancosBLL clsBancosBLL;

        int id;
        int idbanco;
        int idcontabil;
        int idbcoespecie;
        int idbcomoeda;
        int idbcoaceite;
        int idbcoprotestar;
        int idbcoprotestar2;
        int idbcoimpressao;
        int idbcomodalidade;

        DataGridViewRowCollection rows;

        public frmBancoIntegra()
        {
            InitializeComponent();
        }

        public void Init(int id,
                         DataGridViewRowCollection rows)
        {
            this.id = id;
            this.rows = rows;

            clsBancosBLL = new clsBancosBLL();

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSESPECIE order by codigo", tbxBcoEspecie);
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
                clsBancosInfo = clsBancosBLL.Carregar(id, clsInfo.conexaosqlbanco);
            }

            BancosCampos(clsBancosInfo);
            BancosFillInfo(clsBancosInfoOld);
        }

        private void BancosCampos(clsBancosInfo info)
        {
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
            cbxCtaTransitoria.SelectedIndex = info.ctatransitoria == "S" ? 0 : 1;
            tbxTxBoleto.Text = info.txboleto.ToString();
            tbxTxCartorio.Text = info.txcartorio.ToString();
            cbxAtivo.SelectedIndex = info.ativo == "S" ? 0 : 1;
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

            CarregarAutoCompletar();
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
            info.ctatransitoria = cbxCtaTransitoria.SelectedIndex == 0 ? "S" : "N";
            info.txboleto = clsParser.DecimalParse(tbxTxBoleto.Text);
            info.txcartorio = clsParser.DecimalParse(tbxTxCartorio.Text);
            info.ativo = cbxAtivo.SelectedIndex == 0 ? "S" : "N";
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
            if (clsInfo.zrow != null && clsInfo.zrow.Index != -1)
            {
                if (clsInfo.znomegrid == btnIdBanco.Name)
                {
                    idbanco = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxBanco.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    
                    if (tbxBanco.Text != "0")
                    {
                        tbxNome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    }

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

                tbxBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSESPECIE where ID= " + idbcoespecie);
                lblBcoEspecie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSESPECIE where ID= " + idbcoespecie);
            }

            if (ctl.Name == tbxBcoMoeda.Name)
            {
                idbcomoeda = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSMOEDA where CODIGO='" + tbxBcoMoeda.Text + "' AND  TAB_BANCOSMOEDA.IDCODIGO = " + idbanco + " "));

                tbxBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMOEDA where ID= " + idbcomoeda);
                lblBcoMoeda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSMOEDA where ID= " + idbcomoeda);
            }

            if (ctl.Name == tbxBcoAceite.Name)
            {
                idbcoaceite = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSACEITE where CODIGO='" + tbxBcoAceite.Text + "' AND  TAB_BANCOSACEITE.IDCODIGO = " + idbanco + " "));

                tbxBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSACEITE where ID= " + idbcoaceite);
                lblBcoAceite.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSACEITE where ID= " + idbcoaceite);
            }

            if (ctl.Name == tbxBcoProtestar.Name)
            {
                idbcoprotestar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSPROTESTAR where CODIGO='" + tbxBcoProtestar.Text + "' AND  TAB_BANCOSPROTESTAR.IDCODIGO = " + idbanco + " "));

                tbxBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar);
                lblBcoProtestar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar);
            }

            if (ctl.Name == tbxBcoImpressao.Name)
            {
                idbcoimpressao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSIMPRESSAO where CODIGO='" + tbxBcoImpressao.Text + "' AND  TAB_BANCOSIMPRESSAO.IDCODIGO = " + idbanco + " "));

                tbxBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSIMPRESSAO where ID= " + idbcoimpressao);
                lblBcoImpressao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSIMPRESSAO where ID= " + idbcoimpressao);
            }

            if (ctl.Name == tbxBcoModalidade.Name)
            {
                idbcomodalidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSMODALIDADE where CODIGO='" + tbxBcoModalidade.Text + "' AND  TAB_BANCOSMODALIDADE.IDCODIGO = " + idbanco + " "));

                tbxBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSMODALIDADE where ID= " + idbcomodalidade);
                lblBcoModalidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSMODALIDADE where ID= " + idbcomodalidade);
            }

            if (ctl.Name == tbxBcoProtestar2.Name)
            {
                idbcoprotestar2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM TAB_BANCOSPROTESTAR where CODIGO='" + tbxBcoProtestar2.Text + "'  AND  TAB_BANCOSPROTESTAR.IDCODIGO = " + idbanco + " "));

                tbxBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar2);
                lblBcoProtestar2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from TAB_BANCOSPROTESTAR where ID= " + idbcoprotestar2);
            }

            // Se for banco do Brasil e o campo convenio estiver em branco
            if (clsParser.Int32Parse(tbxBanco.Text) == 1 && tbxConvenio.Text.Length == 0 && tbxCarteira.Text.Length > 0)
            {
                try
                {
                    // carteira
                    tbxSubCarteira.Text = tbxCarteira.Text.Substring(6, 2).PadLeft(3, '0');

                    // Variação
                    tbxVariacao.Text = tbxCarteira.Text.Substring(8, 3).PadRight(3, '0');

                    // Convenio
                    tbxConvenio.Text = tbxCarteira.Text.Substring(0, 6).PadLeft(6, '0');
                }
                catch
                {

                }
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

            return clsBancosBLL.Equals(clsBancosInfo, clsBancosInfoOld) == false;
        }

        private DialogResult Salvar()
        {
            var drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);

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
            var frmTab_BancosVis = new frmTab_BancosVis();
            frmTab_BancosVis.Init(clsInfo.conexaosqldados);
            clsInfo.znomegrid = btnIdBanco.Name;

            clsFormHelper.AbrirForm(this.MdiParent, frmTab_BancosVis, clsInfo.conexaosqldados);
        }

        private void CarregarAutoCompletar()
        {
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSESPECIE where IDCODIGO=" + idbanco + " order by codigo", tbxBcoEspecie);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSMOEDA where IDCODIGO=" + idbanco + " order by codigo", tbxBcoMoeda);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSACEITE where IDCODIGO=" + idbanco + " order by codigo", tbxBcoAceite);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSPROTESTAR where IDCODIGO=" + idbanco + " order by codigo", tbxBcoProtestar);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSIMPRESSAO where IDCODIGO=" + idbanco + " order by codigo", tbxBcoImpressao);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSMODALIDADE where IDCODIGO=" + idbanco + " order by codigo", tbxBcoModalidade);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from TAB_BANCOSPROTESTAR where IDCODIGO=" + idbanco + " order by codigo", tbxBcoProtestar2);
        }

        private void btnIdBcoEspecie_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBcoEspecie.Name;

            var frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "TAB_BANCOSESPECIE", idbcoespecie, "IDCODIGO", idbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdBcoMoeda_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBcoMoeda.Name;

            var frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "TAB_BANCOSMOEDA", idbcomoeda, "IDCODIGO", idbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdBcoAceite_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBcoAceite.Name;

            var frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "TAB_BANCOSACEITE", idbcoaceite, "IDCODIGO", idbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdBcoProtestar_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBcoProtestar.Name;

            var frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "TAB_BANCOSPROTESTAR", idbcoprotestar, "IDCODIGO", idbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdBcoImpressao_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBcoImpressao.Name;

            var frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "TAB_BANCOSIMPRESSAO", idbcoimpressao, "IDCODIGO", idbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdBcoModalidade_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBcoModalidade.Name;

            var frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "TAB_BANCOSMODALIDADE", idbcomodalidade, "IDCODIGO", idbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdProtestar2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdProtestar2.Name;

            var frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "TAB_BANCOSPROTESTAR", idbcoprotestar2, "IDCODIGO", idbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void tspRetornar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tspSalvar_Click_1(object sender, EventArgs e)
        {
            if (Salvar() == DialogResult.Cancel)
            {
                return;
            }

            this.Close();
        }

        private void tspPrimeiro_Click_1(object sender, EventArgs e)
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

        private void tspAnterior_Click_1(object sender, EventArgs e)
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

        private void tspProximo_Click_1(object sender, EventArgs e)
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

        private void tspUltimo_Click_1(object sender, EventArgs e)
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
    }
}
