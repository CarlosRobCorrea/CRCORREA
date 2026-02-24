using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPagar : Form
    {
        
        
        
        clsFinanceiro clsFinanceiro = new clsFinanceiro();

        ParameterFields pfields = new ParameterFields();

        clsPagarBLL clsPagarBLL;
        clsPagarInfo clsPagarInfo;
        clsPagarInfo clsPagarInfoOld;

        DataGridViewRowCollection rows;

        Int32 id;
        Int32 idPagarNFE;

        Int32 idDocumento;
        Int32 idFornecedor;
        Int32 idHistorico;
        Int32 idContacontabil;
        Int32 idCentrocusto;
        Int32 idNotafiscal;
        Int32 idFormapagto;
        Int32 idBanco;
        Int32 idBancoint;
        Int32 idSitBanco;
        Int32 idDocNFV;

        Int32 idHistoricoBaixa;
        Int32 idContacontabilBaixa;
        Int32 idCentrocustoBaixa;
        Int32 idFormapagtoBaixa;
        Int32 idBancointBaixa;

        Int32 totaldias;
        Int32 dias;

        String atevencimento;

        DialogResult resultado;

        SqlConnection scn;
        SqlCommand scd;

        // Pagar Observa
        DataTable dtPagarObserva;
        clsPagarObservaBLL clsPagarObservaBLL;
        clsPagarObservaInfo clsPagarObservaInfo;
        clsPagarObservaInfo clsPagarObservaInfoOld;

        Int32 pagarobserva_id;
        Int32 pagarobserva_idduplicata;
        Int32 pagarobserva_posicao;


        // Tabela Virtual
        DataTable dtPagar01;
        
        DataTable dtPagas01;

        GridColuna[] dtPagar01Colunas;

        // Dados virtuais
        Int32 posicao;
        Int32 pagar01_idcobrancacod;
        Int32 pagar01_idcobrancahis;
        Int32 pagar01_idhistorico;
        Int32 pagar01_idcentrocusto;
        Int32 pagar01_idcodigoctabil;

        DateTime dataBaixa;
        DateTime dataVencimento;

        Decimal valorPrincipalDuplicata = 0;  // VALOR PRINCIPAL A PAGAR
        Decimal ValorPrincipal = 0;           // VALORPRINCIPAL PAGO
        Decimal ValorDescontoPago = 0;        // Valor de Desconto que foi pago parcial (antecipado)
        public frmPagar()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            this.id = _id;
            this.rows = _rows;

            clsPagarBLL = new clsPagarBLL();

            tbxDataBaixa.Text = DateTime.Today.ToString("dd/MM/yyyy");
            tbxDataCredito.Text = DateTime.Today.ToString("dd/MM/yyyy");

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from docfiscal order by cognome", tbxDocumento);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxClienteCognome);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", tbxHistorico);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", tbxCentroCusto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from contacontabil order by codigo", tbxContaContabil);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select numero from nfcompra order by numero", tbxNFVendaNumero);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaotipotitulo order by codigo", tbxFormaPagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from tab_bancos order by codigo", tbxBancoConta);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBancoInt);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaotitulo order by codigo", tbxSitBanco);

            // tela da baixa
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", tbxBaixaHistorico);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", tbxBaixaCentroCusto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from contacontabil order by codigo", tbxBaixaContacontabil);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaotipotitulo order by codigo", tbxBaixaFormaPagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBaixaBcoInt);

            // tela registro pagar01
            clsPagarObservaBLL = new clsPagarObservaBLL();
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaocobrancacod order by codigo", tbxPagar01CobrancaCodCodigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", tbxPagar01HistCod);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", tbxPagar01CCCod);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from contacontabil order by codigo", tbxPagar01CtabilCodigo);


            dtPagar01Colunas = new GridColuna[]
                        {
                            new GridColuna("Data Baixa", "DATAENVIO", 60, true, DataGridViewContentAlignment.MiddleCenter),
                            new GridColuna("Data Pagto", "DATAOK", 60, true, DataGridViewContentAlignment.MiddleCenter),
                            new GridColuna("Id Cobrança", "IDCOBRANCACOD", 1, false, DataGridViewContentAlignment.MiddleCenter),
                            new GridColuna("Cod. Cobrança", "CODCOBRANCA", 120, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Id Hist Cobranca", "IDCOBRANCAHIS", 1, false, DataGridViewContentAlignment.MiddleCenter),
                            new GridColuna("Hist Cob", "CODHISTORICO", 30, true, DataGridViewContentAlignment.MiddleCenter),
                            new GridColuna("Valor", "VALOR", 70, true, DataGridViewContentAlignment.MiddleRight),
                            new GridColuna("D/C", "DEBCRED", 25, true, DataGridViewContentAlignment.MiddleCenter),
                            new GridColuna("Motivo", "MOTIVO", 40, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Id Historico", "IDHISTORICO", 1, false, DataGridViewContentAlignment.MiddleCenter),
                            new GridColuna("Hist", "HISTORICO", 35, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Id Centro Custo", "IDCENTROCUSTO", 1, false, DataGridViewContentAlignment.MiddleCenter),
                            new GridColuna("C.Custo", "CENTROCUSTO", 35, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Id Cta Contabil", "IDCODIGOCTABIL", 1, false, DataGridViewContentAlignment.MiddleCenter),
                            new GridColuna("Cta Ctabil", "CONTACONTABIL", 35, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Vl Com. Vendedor", "VALORCOMISSAO", 70, true, DataGridViewContentAlignment.MiddleRight),
                            new GridColuna("Vl Com. Coordenador", "VALORCOMISSAOGER", 70, true, DataGridViewContentAlignment.MiddleRight),
                            new GridColuna("Vl Com. Supervisor", "VALORCOMISSAOSUP", 70, true, DataGridViewContentAlignment.MiddleRight)
                        };
        }

        private void frmPagar_Load(object sender, EventArgs e)
        {
            PagarCarregar();
        }

        private void frmPagar_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
            
        }

        private void frmPagar_Shown(object sender, EventArgs e)
        {

        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }


        private void ControlLeave(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);

            // SE O DOCUMENTO GERADOR EXISTE - DEVE RESPEITAR O VENCIMENTO E VALOR RECEBIDO DELE
            if (clsParser.Int32Parse(tbxNFVendaNumero.Text) > 0)
            { // TRAVAR VENCIMENTO E VALOR PRINCIPAL
                gbxDuplicata.Enabled = false;
                gbxSetor.Enabled = false;
                gbxFornecedor.Enabled = false;
                tbxVencimento.ReadOnly = true;
                tbxValor.ReadOnly = true;
            }
            else
            {  //DESTRAVAR VENCIMENTO E VALOR PRINCIPAL
                gbxDuplicata.Enabled = true;
                gbxSetor.Enabled = true;
                gbxFornecedor.Enabled = true;
                tbxVencimento.ReadOnly = false;
                tbxValor.ReadOnly = false;
            }

            switch (((Control)sender).Name)
            {
                case "tbxValorDesconto":
                    calcular();
                    break;
                case "tbxValorJuros":
                    calcular();
                    break;
                case "tbxValorMulta":
                    calcular();
                    break;
                case "tbxVencimentoPrev":
                    calcular();
                    break;
                case "tbxDataBaixa":
                    if (clsParser.SqlDateTimeParse(tbxDataBaixa.Text).Value != null)
                    {
                        if (clsParser.SqlDateTimeParse(tbxDataBaixa.Text).Value > clsParser.SqlDateTimeParse(tbxDataCredito.Text).Value)
                        {
                            tbxDataCredito.Text = tbxDataBaixa.Text;
                        }
                        calcular();
                        VerificarPago();       //verifica se já foi pago algum valor (apenas principal)
                        CalcularPagando();
                    }
                    else
                    {
                        MessageBox.Show("Data Baixa não pode ser nula", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbxDataBaixa.Select();
                        tbxDataBaixa.SelectAll();
                    }
                    break;
                case "tbxDataCredito":
                    if (clsParser.SqlDateTimeParse(tbxDataCredito.Text).IsNull != true)
                    {   // Se a data que estamos baixando é maior que a data do credito 
                        if (clsParser.SqlDateTimeParse(tbxDataBaixa.Text).Value > clsParser.SqlDateTimeParse(tbxDataCredito.Text).Value)
                        {   // Se a data do credito é maior ou igual a data de vencimento
                            if (clsParser.SqlDateTimeParse(tbxDataCredito.Text).Value >= clsParser.SqlDateTimeParse(tbxVencimento.Text).Value)
                            {
                                //Mantem a data do credito pois esta baixando depois que aconteceu
                            }
                            else
                            {
                                tbxDataCredito.Text = tbxDataBaixa.Text;
                            }
                        }

                        calcular();
                        VerificarPago();       //verifica se já foi pago algum valor (apenas principal)
                        CalcularPagando();
                    }
                    else
                    {
                        MessageBox.Show("Data Crédito não pode ser nula","Aplisoft",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        tbxDataCredito.Select();
                        tbxDataCredito.SelectAll();
                    }
                    break;
                case "tbxHistorico":
                    calcular();
                    break;
                case "tbxCentroCusto":
                    calcular();
                    break;
                    

                default:
                    break;
            }

            //SalvarMovimentacaoPagar01();

            clsVisual.ControlLeave(sender);
            clsVisual.FormatarCampoNumerico(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void ControlKeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisual.ControlKeyPressNumero((TextBox)sender, e, true);
        }
        private void TrataCampos(Control ctl)
        {
            tbxDuplicata3.Text = tbxDuplicata2.Text = tbxDuplicata1.Text = tbxDuplicata.Text;
            tbxEmissao3.Text = tbxEmissao2.Text = tbxEmissao1.Text = tbxEmissao.Text;
            tbxPosicao3.Text = tbxPosicao2.Text = tbxPosicao1.Text = tbxPosicao.Text;
            tbxPosicaoFim3.Text = tbxPosicaoFim2.Text = tbxPosicaoFim1.Text = tbxPosicaoFim.Text;
            tbxClienteCognome3.Text = tbxClienteCognome2.Text = tbxClienteCognome1.Text = tbxClienteCognome.Text;
            tbxClienteTelefone3.Text = tbxClienteTelefone2.Text = tbxClienteTelefone1.Text = tbxClienteTelefone.Text;
            tbxClienteCGC3.Text = tbxClienteCGC2.Text = tbxClienteCGC1.Text = tbxClienteCGC.Text;
            tbxDocumento3.Text = tbxDocumento2.Text = tbxDocumento1.Text = tbxDocumento.Text;

            tbxPagar01Valor.Text = clsParser.DecimalParse(tbxPagar01Valor.Text).ToString("N2");
            tbxPagar01Comissao.Text = clsParser.DecimalParse(tbxPagar01Comissao.Text).ToString("N2");
            tbxPagar01ComissaoGer.Text = clsParser.DecimalParse(tbxPagar01ComissaoGer.Text).ToString("N2");
            tbxPagar01ComissaoSup.Text = clsParser.DecimalParse(tbxPagar01ComissaoSup.Text).ToString("N2");

            if (ckxChegou.Checked == true)
            {
                ckxChegou.Text = "Sim - Chegou a cobrança";
            }
            else
            {
                ckxChegou.Text = "Não - Chegou a cobrança";
            }

            if (ckxDespesaPublica.Checked == true)
            {
                ckxDespesaPublica.Text = "Sim - Despesa Publica";
            }
            else
            {
                ckxDespesaPublica.Text = "Não é Despesa Publica";
            }
            if (ctl.Name == tbxDataBaixa.Name)
            {
                if (DateTime.Parse(tbxDataBaixa.Text) != DateTime.Parse(tbxDataCredito.Text))
                {
                    tbxDataCredito.Text = tbxDataBaixa.Text;
                }
            }

            if (clsInfo.zrow != null &&
               clsInfo.zrow.Index != -1 &&
               clsInfo.znomegrid != null &&
               clsInfo.znomegrid != "")
            {
                // ###############################
                // Verifica os botões de pesquisa
                // ###############################
                if (clsInfo.znomegrid == btnIdDocumento.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idDocumento = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxDocumento.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
                        tbxDocumento.Select();
                    }
                }

                if (clsInfo.znomegrid == btnIdFornecedor.Name)
                {
                    idFornecedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxClienteCognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
                    //tbxClienteCGC.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "CGC", "ID", idFornecedor);
                    tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CGC from CLIENTE where ID = " + idFornecedor,"");

                    //tbxClienteTelefone.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "DDD", "ID", idFornecedor) +
                     //" - " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "TELEFONE", "ID", idFornecedor);
                    tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DDD from CLIENTE where ID = "+ idFornecedor,"") +
                    " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TELEFONE from CLIENTE where ID = " + idFornecedor,"");

                    tbxClienteCognome.Select();
                    tbxClienteCognome.SelectAll();
                }

                if (clsInfo.znomegrid == btnIdHistorico.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            idHistorico = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                            tbxHistorico.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                            tbxHistoricoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        }
                    }
                    tbxHistorico.Select();
                    tbxHistorico.SelectAll();
                }

                if (clsInfo.znomegrid == btnIdCentroCusto.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idCentrocusto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCentroCusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCentroCustoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    }
                    tbxCentroCusto.Select();
                    tbxCentroCusto.SelectAll();
                }

                if (clsInfo.znomegrid == btnIdCodigoCtaBil.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idContacontabil = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxContaContabil.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxContaContabilNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        tbxContaContabil.Select();
                        tbxContaContabil.SelectAll();
                    }
                }

                if (clsInfo.znomegrid == btnIdFormaPagto.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idFormapagto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxFormaPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxFormaPagto.Text += " = " + clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        tbxFormaPagto.Select();
                        tbxFormaPagto.SelectAll();
                    }
                }

                if (clsInfo.znomegrid == btnIdBanco.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idBanco = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBancoConta.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxBancoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        tbxBancoConta.Select();
                        tbxBancoConta.SelectAll();
                    }
                }

                if (clsInfo.znomegrid == btnIdBancoInt.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idBancoint = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBancoInt.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                        tbxBancoNomeInterno.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    }
                    tbxBaixaBcoInt.Select();
                }

                if (clsInfo.znomegrid == btnIdSitBanco.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxSitBanco.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxSituacaoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxSitBanco.Select();
                        tbxSituacaoNome.SelectAll();
                    }
                }
               
                if (clsInfo.znomegrid == btnPagar01_idcobrancacod.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        pagar01_idcobrancacod = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01CobrancaCodCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01CobrancaCodNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxPagar01CobrancaCodCodigo.Select();
                    tbxPagar01CobrancaCodCodigo.SelectAll();
                }

                if (clsInfo.znomegrid == btnPagar01_idcobrancahis.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        pagar01_idcobrancahis = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01CobrancaCod1Codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01CobrancaCod1Nome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxPagar01CobrancaCod1Codigo.Select();
                    tbxPagar01CobrancaCod1Codigo.SelectAll();
                }
                
                if (clsInfo.znomegrid == btnPagar01_idhistorico.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        pagar01_idhistorico = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01HistCod.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01HistNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    }
                    tbxPagar01HistCod.Select();
                    tbxPagar01HistCod.SelectAll();
                }

                if (clsInfo.znomegrid == btnPagar01_idcentrocusto.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        pagar01_idcentrocusto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01CCCod.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01CCNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxPagar01CCCod.Select();
                    tbxPagar01CCCod.SelectAll();
                }


                if (clsInfo.znomegrid == btnPagar01_idcodigoctabil.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        pagar01_idcodigoctabil = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01CtabilCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01CtabilNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        tbxPagar01CtabilCodigo.Select();
                        tbxPagar01CtabilCodigo.SelectAll();
                    }
                }
               
                if (clsInfo.znomegrid == btnBaixaIdHistorico.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idHistoricoBaixa = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBaixaHistorico.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxBaixaHistoricoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
//                    tbxBaixaHistorico.Select();
//                    tbxBaixaHistorico.SelectAll();
                    tbxBaixaFormaPagto.Select();
                    tbxBaixaFormaPagto.SelectAll();

                }

                if (clsInfo.znomegrid == btnBaixaIdCentroCusto.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idCentrocustoBaixa = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBaixaCentroCusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxBaixaCentroCustoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxBaixaCentroCusto.Select();
                    tbxBaixaCentroCusto.SelectAll();
                }

                if (clsInfo.znomegrid == btnBaixaIdContaContabil.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idContacontabilBaixa = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBaixaContacontabil.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxBaixaContacontabilNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxBaixaContacontabil.Select();
                        tbxBaixaContacontabil.SelectAll();
                    }
                }

                if (clsInfo.znomegrid == btnBaixaIdFormaPagto.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idFormapagtoBaixa = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString().Trim());
                        tbxBaixaFormaPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim() + " = " + clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxBaixaFormaPagto.Select();
                        tbxBaixaFormaPagto.SelectAll();
                    }
                }
               
                if (clsInfo.znomegrid == btnBaixaIdBancoInt.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idBancointBaixa = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString().Trim());
                        tbxBaixaBcoInt.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                        tbxBaixaBcoIntNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        tbxBaixaBcoIntNome.Text = tbxBaixaBcoIntNome.Text + "-" + clsInfo.zrow.Cells["AGENCIA"].Value.ToString().Trim();
                        tbxBaixaBcoInt.Select();
                        tbxBaixaBcoInt.SelectAll();
                    }
                }  //fim
            }
            else
            {
                // ###############################
                // Verifica os campos de pesquisa
                // ###############################
                if (ctl.Name == tbxVencimentoPrev.Name)
                {
                    if (DateTime.Parse(tbxVencimentoPrev.Text) > DateTime.Parse(tbxVencimento.Text))
                    {
                        tbxDataBaixa.Text = tbxVencimentoPrev.Text;
                    }
                }

                if (ctl.Name == tbxDocumento.Name)
                {
                    idDocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + tbxDocumento.Text + "' "));
                    if (idDocumento == 0)
                    {
                        idDocumento = clsInfo.zdocumento;
                    }
                    tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + idDocumento + " ");
                }

                if (ctl.Name == tbxClienteCognome.Name)
                {
                    idFornecedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CLIENTE where COGNOME='" + tbxClienteCognome.Text + "' "));
                    if (idFornecedor == 0)
                    {
                        idFornecedor = clsInfo.zempresaclienteid;
                    }
                    tbxClienteCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idFornecedor);
                    tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CGC FROM CLIENTE where id=" + idFornecedor);
                    tbxClienteCGC.Text = clsVisual.CamposVisual("CGC", tbxClienteCGC.Text);
                    tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT DDD FROM CLIENTE where id=" + idFornecedor);
                    tbxClienteTelefone.Text += " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TELEFONE FROM CLIENTE where id=" + idFornecedor);
                }

                if (ctl.Name == tbxHistorico.Name)
                {
                    idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT ID FROM HISTORICOS where CODIGO='" + tbxHistorico.Text + "' "));
                    if (idHistorico == 0)
                    {
                        idHistorico = clsInfo.zhistoricos;
                    }
                    tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + idHistorico);
                    tbxHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT NOME FROM HISTORICOS where id=" + idHistorico);
                }

                if (ctl.Name == tbxCentroCusto.Name)
                {
                    idCentrocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + tbxCentroCusto.Text + "'"));
                    if (idCentrocusto == 0)
                    {
                        idCentrocusto = clsInfo.zcentrocustos;
                    }
                    tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID= " + idCentrocusto);
                    tbxCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID= " + idCentrocusto);
                }

                if (ctl.Name == tbxContaContabil.Name)
                {
                    idContacontabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONTACONTABIL where CODIGO='" + tbxContaContabil.Text + "'"));
                    if (idContacontabil == 0)
                    {
                        idContacontabil = clsInfo.zcontacontabil;
                    }
                    tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID= " + idContacontabil);
                    tbxContaContabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID= " + idContacontabil);
                }

                if (ctl.Name == tbxFormaPagto.Name)
                {
                    idFormapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM SITUACAOTIPOTITULO where CODIGO='" + tbxFormaPagto.Text.Substring(0, 2) + "'"));
                    if (idFormapagto == 0)
                    {
                        idFormapagto = clsInfo.zformapagto;
                    }
                    tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + idFormapagto);
                    tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + idFormapagto);
                }

                if (ctl.Name == tbxBancoConta.Name)
                {
                    idBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from TAB_BANCOS where CODIGO='" + tbxBancoConta.Text + "'"));
                    if (idBanco == 0)
                    {
                        idBanco = clsInfo.zbanco;
                    }
                    tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco);
                    tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);
                }

                if (ctl.Name == tbxBancoInt.Name)
                {
                    idBancoint = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA= " + clsParser.Int32Parse(tbxBancoInt.Text) + " "));
                    if (idBancoint == 0)
                    {
                        idBancoint = clsInfo.zbancoint;
                    }
                    tbxBancoInt.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID= " + idBancoint);
                    tbxBancoNomeInterno.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID= " + idBancoint);
                }

                if (ctl.Name == tbxSitBanco.Name)
                {
                    idSitBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM SITUACAOTITULO where CODIGO='" + tbxSitBanco.Text + "' "));

                    if (idSitBanco == 0)
                    {
                        idSitBanco = clsInfo.zbanco;
                    }
                    tbxSitBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTITULO where id=" + idSitBanco);
                    tbxSituacaoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTITULO where id=" + idSitBanco);
                }

                if (ctl.Name == tbxPagar01CobrancaCodCodigo.Name)
                {
                    //pagar01_idcobrancacod = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", tbxPagar01CobrancaCodCodigo.Text.Trim()));
                    pagar01_idcobrancacod = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO ='" +  tbxPagar01CobrancaCodCodigo.Text.Trim() + "'","0"));
                   
                    if (pagar01_idcobrancacod == 0)
                    {
                        pagar01_idcobrancacod = clsInfo.zsituacaocobrancacod;
                    }
                    //tbxPagar01CobrancaCodCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", pagar01_idcobrancacod).Trim();
                    tbxPagar01CobrancaCodCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID ="  + pagar01_idcobrancacod,"").Trim();
                 
                    //tbxPagar01CobrancaCodNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", pagar01_idcobrancacod).Trim();
                    tbxPagar01CobrancaCodNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + pagar01_idcobrancacod,"").Trim();
                }

                if (ctl.Name == tbxPagar01CobrancaCod1Codigo.Name)
                {
                    if (tbxPagar01CobrancaCod1Codigo.ReadOnly == false)
                    {
                        //pagar01_idcobrancahis = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", tbxPagar01CobrancaCod1Codigo.Text.Trim()));
                        pagar01_idcobrancahis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD1 where CODIGO ='" + tbxPagar01CobrancaCod1Codigo.Text.Trim() + "'","0"));
                        if (pagar01_idcobrancahis == 0)
                        {
                            pagar01_idcobrancahis = 0;
                        }
                        //tbxPagar01CobrancaCod1Codigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", pagar01_idcobrancahis).Trim();
                        tbxPagar01CobrancaCod1Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD1 where ID = " + pagar01_idcobrancahis,"").Trim();
                    
                        //tbxPagar01CobrancaCod1Nome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", pagar01_idcobrancahis).Trim();
                        tbxPagar01CobrancaCod1Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD1 where ID = " + pagar01_idcobrancahis,"").Trim();
                    }
                }

                if (ctl.Name == tbxPagar01CCCod.Name)
                {
                    if (tbxPagar01CCCod.ReadOnly == false)
                    {
                        //pagar01_idcentrocusto = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ID", "CODIGO", tbxPagar01CCCod.Text.Trim()));
                        pagar01_idcentrocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO ='" +  tbxPagar01CCCod.Text.Trim() + "'", "0"));

                        if (pagar01_idcentrocusto == 0)
                        {
                            pagar01_idcentrocusto = clsInfo.zcentrocustos;
                        }
                        //tbxPagar01CCCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", pagar01_idcentrocusto).Trim();
                        tbxPagar01CCCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + pagar01_idcentrocusto, "").Trim();
                    
                        //tbxPagar01CCNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", pagar01_idcentrocusto).Trim();
                        tbxPagar01CCNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID = " + pagar01_idcentrocusto, "").Trim();
                    }
                }

                if (ctl.Name == tbxPagar01CtabilCodigo.Name)
                {
                    if (tbxPagar01CtabilCodigo.ReadOnly == false)
                    {
                        // pagar01_idcodigoctabil = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "ID", "CODIGO", tbxPagar01CtabilCodigo.Text));
                        pagar01_idcodigoctabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONTACONTABIL where CODIGO ='" + tbxPagar01CtabilCodigo.Text + "'", "0"));

                        if (pagar01_idcodigoctabil == 0)
                        {
                            pagar01_idcodigoctabil = clsInfo.zcontacontabil;
                        }
                        //tbxPagar01CtabilCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", pagar01_idcodigoctabil).Trim();
                        tbxPagar01CtabilCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID = " + pagar01_idcodigoctabil, "").Trim();

                        //tbxPagar01CtabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", pagar01_idcodigoctabil).Trim();
                        tbxPagar01CtabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID = " + pagar01_idcodigoctabil, "").Trim();
                    }
                }

                if (ctl.Name == tbxBaixaHistorico.Name)
                {
                    if (tbxBaixaHistorico.ReadOnly == false)
                    {
                        //idHistoricoBaixa = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ID", "CODIGO", tbxBaixaHistorico.Text.Trim()));
                        idHistoricoBaixa = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO='" + tbxBaixaHistorico.Text.Trim() + "' "));

                        if (idHistoricoBaixa == 0)
                        {
                            idHistoricoBaixa = clsInfo.zhistoricos;
                        }
                        //tbxBaixaHistorico.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idHistoricoBaixa).Trim();
                        //tbxBaixaHistoricoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", idHistoricoBaixa).Trim();
                        tbxBaixaHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO FROM HISTORICOS where ID=" + idHistoricoBaixa);
                        tbxBaixaHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME FROM HISTORICOS where ID=" + idHistoricoBaixa);


                    }
                }

                if (ctl.Name == tbxBaixaCentroCusto.Name)
                {
                    if (tbxBaixaCentroCusto.ReadOnly == false)
                    {
                        idCentrocustoBaixa = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + tbxBaixaCentroCusto.Text.Trim() + "' "));
                        if (idCentrocustoBaixa == 0)
                        {
                            idCentrocustoBaixa = clsInfo.zcentrocustos;
                        }
                        tbxBaixaCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO FROM CENTROCUSTOS where ID=" + idCentrocustoBaixa);
                        tbxBaixaCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME FROM CENTROCUSTOS where ID=" + idCentrocustoBaixa);
                    }
                }

                if (ctl.Name == tbxBaixaContacontabil.Name)
                {
                    if (tbxBaixaContacontabil.ReadOnly == false)
                    {
                        idContacontabilBaixa = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONTACONTABIL where CODIGO='" + tbxBaixaContacontabil.Text.Trim() + "'"));
                        if (idContacontabilBaixa == 0)
                        {
                            idContacontabilBaixa = clsInfo.zcontacontabil;
                        }
                        tbxBaixaContacontabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO FROM CONTACONTABIL where ID=" + idContacontabilBaixa);
                        tbxBaixaContacontabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME FROM CONTACONTABIL where ID=" + idContacontabilBaixa); 
                    }
                }

                if (ctl.Name == tbxBaixaFormaPagto.Name)
                {
                    int indexFim = 0;
                    if (tbxBaixaFormaPagto.Text.Contains("-"))
                    {
                        indexFim = tbxBaixaFormaPagto.Text.IndexOf("-");
                    }
                    else if (tbxBaixaFormaPagto.Text.Contains("="))
                    {
                        indexFim = tbxBaixaFormaPagto.Text.IndexOf("=");
                    }
                    else
                    {
                        indexFim = tbxBaixaFormaPagto.TextLength;
                    }
                    string _codigo = tbxBaixaFormaPagto.Text.Substring(0, indexFim).Trim();

                    idFormapagtoBaixa = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOTIPOTITULO where CODIGO='" + _codigo + "'"));
                    if (idFormapagtoBaixa == 0)
                    {
                        idFormapagtoBaixa = clsInfo.zformapagto;
                    }
                    tbxBaixaFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO +' - '+ NOME  from SITUACAOTIPOTITULO where ID = " + idFormapagtoBaixa, "").Trim();
                }

                if (ctl.Name == tbxBaixaBcoInt.Name)
                {
                    idBancointBaixa = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA= " + clsParser.Int32Parse(tbxBaixaBcoInt.Text) + " "));
                    if (idBancointBaixa == 0)
                    {
                        idBancointBaixa = clsInfo.zbancoint;
                    }
                    tbxBaixaBcoInt.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID= " + idBancointBaixa);
                    tbxBaixaBcoIntNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID= " + idBancointBaixa);
                    tbxBaixaBcoIntNome.Text = tbxBaixaBcoIntNome.Text + "-" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select AGENCIA from BANCOS where ID= " + idBancointBaixa);
                }
            }

            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
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
                    PagarCarregar();
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
                                PagarCarregar();
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
                                PagarCarregar();
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
                    PagarCarregar();
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
            clsPagarInfo = new clsPagarInfo();
            PagarFillInfo(clsPagarInfo);
            if (clsPagarBLL.Equals(clsPagarInfo, clsPagarInfoOld) == false)
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
                PagarSalvar();
            }
            return drt;
        }

        private void PagarSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: pagar
                clsPagarInfo = new clsPagarInfo();
                PagarFillInfo(clsPagarInfo);
                if (id == 0)
                {
                    clsPagarInfo.id = clsPagarBLL.Incluir(clsPagarInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsPagarBLL.Alterar(clsPagarInfo, clsInfo.conexaosqldados);
                }
                
                // Gravando as Observações
                foreach (DataRow row in dtPagarObserva.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idduplicata"] = clsPagarInfo.id;
                    }
                }

                foreach (DataRow row in dtPagarObserva.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsPagarObservaBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsPagarObservaInfo = new clsPagarObservaInfo();
                        PagarObservaGridToInfo(clsPagarObservaInfo, Int32.Parse(row["posicao"].ToString()));

                        if (clsPagarObservaInfo.id == 0)
                        {
                            clsPagarObservaInfo.id = clsPagarObservaBLL.Incluir(clsPagarObservaInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsPagarObservaBLL.Alterar(clsPagarObservaInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
                tse.Complete();
            }
        }

        private void PagarCarregar()
        {
            clsPagarInfoOld = new clsPagarInfo();
            if (id == 0)
            {
                clsPagarInfo = new clsPagarInfo();

                clsPagarInfo.atevencimento = "S";
                clsPagarInfo.baixa = "N";
                clsPagarInfo.boleto = "";
                clsPagarInfo.boletonro = 0;
                clsPagarInfo.chegou = "N";
                clsPagarInfo.datalanca = DateTime.Now;
                clsPagarInfo.despesapublica = "N";
                clsPagarInfo.duplicata = 0;
                clsPagarInfo.dv = "";
                clsPagarInfo.emissao = DateTime.Now;
                clsPagarInfo.emitente = clsInfo.zusuario;
                clsPagarInfo.filial = clsInfo.zfilial;
                clsPagarInfo.id = 0;
                clsPagarInfo.idbanco = clsInfo.zbanco;
                clsPagarInfo.idbancoint = clsInfo.zbancoint;
                clsPagarInfo.idcentrocusto = clsInfo.zcentrocustos;
                clsPagarInfo.idcodigoctabil = clsInfo.zcontacontabil;
                clsPagarInfo.iddocumento = clsInfo.zdocumento;
                clsPagarInfo.idformapagto = clsInfo.zformapagto;
                clsPagarInfo.idfornecedor = clsInfo.zempresaclienteid;
                clsPagarInfo.idhistorico = clsInfo.zhistoricos;
                clsPagarInfo.idnotafiscal = clsInfo.znfcompra;
                clsPagarInfo.idpagarnfe = clsInfo.znfcomprapagar;
                clsPagarInfo.idsitbanco = clsInfo.zsituacaotitulo;
                clsPagarInfo.imprimir = "N";
                clsPagarInfo.observa = "";
                clsPagarInfo.posicao = 1;
                clsPagarInfo.posicaofim = 1;
                clsPagarInfo.setor = "N";
                clsPagarInfo.valor = 0;
                clsPagarInfo.valorbaixando = 0;
                clsPagarInfo.valordesconto = 0;
                clsPagarInfo.valordevolvido = 0;
                clsPagarInfo.valorjuros = 0;
                clsPagarInfo.valorliquido = 0;
                clsPagarInfo.valormulta = 0;
                clsPagarInfo.valorpago = 0;
                clsPagarInfo.vencimento = DateTime.Now;
                clsPagarInfo.vencimentoprev = DateTime.Now;
            }
            else
            {
               clsPagarInfo = clsPagarBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            PagarCampos(clsPagarInfo);
            PagarFillInfo(clsPagarInfoOld);

            if (gbxDuplicata.Enabled == true)
            {
                tbxDuplicata.Select();
                tbxDuplicata.SelectAll();
            }
            else
            {
                tbxHistorico.Select();
                tbxHistorico.SelectAll();
            }
            //
            calcular();
            VerificarPago();       //verifica se já foi pago algum valor (apenas principal)
            bwrPagas01_RunWorkerAsync();

            // SE O DOCUMENTO GERADOR EXISTE - DEVE RESPEITAR O VENCIMENTO E VALOR RECEBIDO DELE
            if (clsParser.Int32Parse(tbxNFVendaNumero.Text) > 0)
            { // TRAVAR VENCIMENTO E VALOR PRINCIPAL
                gbxDuplicata.Enabled = false;
                gbxSetor.Enabled = false;
                gbxFornecedor.Enabled = false;
                tbxVencimento.ReadOnly = true;
                tbxValor.ReadOnly = true;
            }
            else
            {  //DESTRAVAR VENCIMENTO E VALOR PRINCIPAL
                gbxDuplicata.Enabled = true;
                gbxSetor.Enabled = true;
                gbxFornecedor.Enabled = true;
                tbxVencimento.ReadOnly = false;
                tbxValor.ReadOnly = false;
            }

            
            // carregando as observações
            dtPagarObserva = clsPagarObservaBLL.GridDados(clsPagarInfo.id);

            DataColumn dcPosicao = new DataColumn("posicao", Type.GetType("System.Int32"));
            dtPagarObserva.Columns.Add(dcPosicao);

            for (Int32 i = 1; i <= dtPagarObserva.Rows.Count; i++)
            {
                dtPagarObserva.Rows[i - 1]["posicao"] = i;
            }

            dtPagarObserva.AcceptChanges();
            clsPagarObservaBLL.GridMonta(dgvPagarObserva, dtPagarObserva, pagarobserva_posicao);


        }
        private void PagarCampos(clsPagarInfo info)
        {

            id = info.id;
            if (info.atevencimento == "S") { ckxAteVencimento.Checked = true; } else { ckxAteVencimento.Checked = false; }
            if (ckxAteVencimento.Checked == true)
            {
                atevencimento = "S";
                ckxAteVencimento.Text = "Sim Descontar após Vencimento";
            }
            else
            {
                atevencimento = "N";
                ckxAteVencimento.Text = "Não Descontar após o Vencimento";
            }


            //info.baixa;
            tbxBoleto.Text = info.boleto;
            tbxBoletoNro.Text = info.boletonro.ToString();
            if (info.chegou == "S") { ckxChegou.Checked = true; }
            tbxDataLanca.Text = info.datalanca.ToString("dd/MM/yyyy");
            if (info.despesapublica == "S") { ckxDespesaPublica.Checked = true; }
            tbxDuplicata.Text = info.duplicata.ToString();
            tbxDv.Text = info.dv;
            tbxEmissao.Text = info.emissao.ToString("dd/MM/yyyy");
            tbxEmitente.Text =  info.emitente;
            tbxFilial.Text = info.filial.ToString();
            idBanco = info.idbanco;
            if (idBanco == 0) {idBanco = clsInfo.zbanco;} 
            tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco);
            tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);
            idBancoint = info.idbancoint;
            if (idBancoint == 0) {idBancoint = clsInfo.zbancoint;}
            tbxBancoInt.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID= " + idBancoint);
            tbxBancoNomeInterno.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID= " + idBancoint);
            idCentrocusto = info.idcentrocusto;
            if (idCentrocusto == 0) { idCentrocusto = clsInfo.zcentrocustos; } 
            tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID= " + idCentrocusto);
            if (tbxCentroCusto.Text == "")
            {
                idCentrocusto = clsInfo.zcentrocustos;
                tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID= " + idCentrocusto);
            }
            tbxCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID= " + idCentrocusto);
            idContacontabil = info.idcodigoctabil;
            if (idContacontabil == 0) { idContacontabil = clsInfo.zcontacontabil; }
            tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID= " + idContacontabil);
            tbxContaContabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID= " + idContacontabil);
            idDocumento = info.iddocumento;
            if (idDocumento == 0) { idDocumento = clsInfo.zdocumento; }
            tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + idDocumento);

            if (tbxDocumento.Text.ToUpper() == "PRO")
            {
                tbxDocumento.Enabled = false;
                btnIdDocumento.Enabled = false;
            }

            idFormapagto = info.idformapagto;
            if (idFormapagto == 0) {idFormapagto = clsInfo.zformapagto; }
            tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + idFormapagto);
            tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + idFormapagto);
            idFornecedor = info.idfornecedor;
            if (idFornecedor == 0) { idFornecedor = clsInfo.zempresaclienteid; }
            tbxClienteCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idFornecedor);
            tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CGC FROM CLIENTE where id=" + idFornecedor);
            tbxClienteCGC.Text = clsVisual.CamposVisual("CGC", tbxClienteCGC.Text);
            tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT DDD FROM CLIENTE where id=" + idFornecedor);
            tbxClienteTelefone.Text += " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TELEFONE FROM CLIENTE where id=" + idFornecedor);
            tbxPix.Text = tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT PIX FROM CLIENTE where id=" + idFornecedor);
            tbxNomePix.Text = tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TITULARCTA FROM CLIENTE where id=" + idFornecedor);
            idHistorico = info.idhistorico;
            if (idHistorico == 0) {idHistorico = clsInfo.zhistoricos; }
            tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + idHistorico);
            if (tbxHistorico.Text == "")
            {
                idHistorico = clsInfo.zhistoricos;
                tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + idHistorico);
            }
            tbxHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT NOME FROM HISTORICOS where id=" + idHistorico);
            idNotafiscal = info.idnotafiscal;
            if (idNotafiscal == 0) { idNotafiscal = clsInfo.znfcompra; }
            tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM NFCOMPRA where id=" + idNotafiscal);
            tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOTALNOTAFISCAL FROM NFCOMPRA where id=" + idNotafiscal);
            if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDDOCUMENTO FROM NFCOMPRA where id=" + idNotafiscal)) > 0)
            {
                idDocNFV = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM DOCFISCAL where id=" + clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDDOCUMENTO FROM NFCOMPRA where id=" + idNotafiscal))));
                if (idDocNFV == 0)
                    idDocNFV = clsInfo.zdocumento;

                tbxNotaDoc.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM DOCFISCAL where id=" +  idDocNFV + "");
            }
            else
            {
                tbxNotaDoc.Text = "";
            }
            idPagarNFE = info.idpagarnfe;
            idSitBanco = info.idsitbanco;
            if (idSitBanco == 0) { idSitBanco = clsInfo.zsituacaotitulo; }
            tbxSitBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTITULO where id=" + idSitBanco);
            tbxSituacaoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTITULO where id=" + idSitBanco);

            //info.imprimir;
            tbxObserva.Text = info.observa;
            tbxPosicao.Text = info.posicao.ToString();
            tbxPosicaoFim.Text = info.posicaofim.ToString();
            tbxParcelas.Text = tbxPosicaoFim.Text;

            if (info.setor == "S")
            {
                rbnSetorSim.Checked = true;
            }
            else
            {
                rbnSetorSim.Checked = false;
            }
            
            tbxValor.Text = info.valor.ToString("N2");
            //info.valorbaixando;
            tbxValorDesconto.Text = info.valordesconto.ToString("N2");
            tbxDevolucao.Text = info.valordevolvido.ToString("N2");
            tbxValorJuros.Text = info.valorjuros.ToString("N2");
            tbxValorLiquido.Text = info.valorliquido.ToString("N2");
            tbxValorMulta.Text = info.valormulta.ToString("N2");
            tbxRecebido.Text = info.valorpago.ToString("N2");
            tbxVencimento.Text = info.vencimento.ToString("dd/MM/yyyy");
            tbxVencimentoPrev.Text = info.vencimentoprev.ToString("dd/MM/yyyy");
            
            tbxMulta.Text = "0";
            tbxDesconto.Text = "0";
            tbxSaldo.Text = "0";
            tbxRecebido.Text = "0";
            tbxDevolucao.Text =  info.valordevolvido.ToString("N2");
            tbxDiferenca.Text = "0";
            tbxPagando.Text = "0";

            // Se a Data da Baixa for superior a 3 dias enviar uma mensagem
            dataBaixa = DateTime.Parse(tbxDataBaixa.Text);
            dataVencimento = DateTime.Parse(tbxVencimento.Text);

            dias = dataBaixa.Subtract(dataVencimento).Days;
            if (dias > 0)
            {
                MessageBox.Show("Data da Baixa com " + dias + " dias de diferença." + Environment.NewLine + "Diferença entre a Data de Vencimento e a Data da Baixa ");
                tbxDataBaixa.Select();
                tbxDataBaixa.SelectAll();
            }
        }
        private void PagarFillInfo(clsPagarInfo info)
        {
            info.id = id;

            if (ckxAteVencimento.Checked == true) {info.atevencimento = "S";} else { info.atevencimento = "N";}
            info.baixa = "";
            info.boleto = tbxBoleto.Text;
            info.boletonro = clsParser.DecimalParse(tbxBoletoNro.Text);
            if (ckxChegou.Checked == true) { info.chegou = "S"; } else { info.chegou = "N"; }
            info.datalanca = DateTime.Parse(tbxDataLanca.Text);
            if (ckxDespesaPublica.Checked == true) { info.despesapublica = "S" ; } else { info.despesapublica = "N"; }
            info.duplicata = clsParser.Int64Parse(tbxDuplicata.Text);
            info.dv = tbxDv.Text;
            info.emissao = DateTime.Parse(tbxEmissao.Text);
            info.emitente = tbxEmitente.Text;
            info.filial = clsParser.Int32Parse(tbxFilial.Text);
            info.idbanco = idBanco;
            info.idbancoint = idBancoint;
            info.idcentrocusto = idCentrocusto;
            info.idcodigoctabil = idContacontabil;
            info.iddocumento = idDocumento;
            info.idformapagto = idFormapagto;
            info.idfornecedor = idFornecedor;
            info.idhistorico = idHistorico;
            info.idnotafiscal = idNotafiscal;
            info.idpagarnfe = idPagarNFE;
            info.idsitbanco = idSitBanco;
            info.imprimir = "N";
            info.observa = tbxObserva.Text;
            info.posicao = clsParser.Int32Parse(tbxPosicao.Text);
            info.posicaofim = clsParser.Int32Parse(tbxPosicaoFim.Text);
            if (rbnSetorSim.Checked == true)
            {
                info.setor = "S";
            }
            else
            {
                info.setor = "N";
            }
            info.valor = clsParser.DecimalParse(tbxValor.Text);
            info.valorbaixando = clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text);
            info.valordesconto = clsParser.DecimalParse(tbxValorDesconto.Text);
            info.valordevolvido = clsParser.DecimalParse(tbxDevolucao.Text);
            info.valorjuros = clsParser.DecimalParse(tbxValorJuros.Text);
            info.valorliquido = clsParser.DecimalParse(tbxValorLiquido.Text);
            info.valormulta = clsParser.DecimalParse(tbxValorMulta.Text);
            info.valorpago =  clsParser.DecimalParse(tbxRecebido.Text);
            info.vencimento = DateTime.Parse(tbxVencimento.Text);
            info.vencimentoprev = DateTime.Parse(tbxVencimentoPrev.Text);
        }
        private void ckxAteVencimento_Click(object sender, EventArgs e)
        {
            if (ckxAteVencimento.Checked == true)
            {
                atevencimento = "S";
                ckxAteVencimento.Text = "Sim Descontar após Vencimento";
            }
            else
            {
                atevencimento = "N";
                ckxAteVencimento.Text = "Não Descontar após o Vencimento";
            }
            calcular();
        }

        private void ckxChegou_Click(object sender, EventArgs e)
        {
            if (ckxChegou.Checked == true)
            {
                ckxChegou.Text = "Sim - Já Chegou Boleto";
            }
            else
            {
                ckxChegou.Text = "Não - Chegou Boleto";
            }
        }

        private void ckxDespesaPublica_Click(object sender, EventArgs e)
        {
            if (ckxDespesaPublica.Checked == true)
            {
                ckxDespesaPublica.Text = "Sim - Despesa Publica";
            }
            else
            {
                ckxDespesaPublica.Text = "Não é Despesa Publica";
            }
        }

        private void btnIdDocumento_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdDocumento.Name;
            frmDocFiscalVis frmDocFiscalVis = new frmDocFiscalVis();
            frmDocFiscalVis.Init(idDocumento);

            clsFormHelper.AbrirForm(this.MdiParent, frmDocFiscalVis, clsInfo.conexaosqldados);
        }
        private void btnidFornecedor_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdFornecedor.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idFornecedor);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }
        private void btnIdHistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdHistorico.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", idHistorico, "Historicos Banco");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void btnIdCentroCusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCentroCusto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", idCentrocusto, "Centro de Custo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void btnIdCodigoCtaBil_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCodigoCtaBil.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONTACONTABIL", idContacontabil, "Contas Contabeis");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdFormaPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdFormaPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", idFormapagto, "Situações de Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdNotaFiscal_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdNotaFiscal.Name;
            //frmNFCompraPes frmNFCompraVis = new frmNFCompraPes(idNotafiscal);
            //FormHelper.AbrirForm(this.MdiParent, frmNFCompraVis);
        }


        private void btnIdBanco_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBanco.Name;
            frmTab_BancosPes frmTab_BancosPes = new frmTab_BancosPes();
            frmTab_BancosPes.Init(clsInfo.conexaosqldados, idBanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmTab_BancosPes, clsInfo.conexaosqldados);
        }

        private void btnIdBancoInt_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBancoInt.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBancoint, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdSitBanco_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdSitBanco.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTITULO", idSitBanco, "Situação do Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void calcular()
        { //Colocamos numa função financeiro
            DuplicataInfo DuplicataInfo;
            DuplicataInfo = clsFinanceiro.CalcularDuplicata("Pagar", tbxVencimento.Text, tbxDataBaixa.Text, 
                                                                    clsParser.DecimalParse(tbxValor.Text),  +
                                                                    clsParser.DecimalParse(tbxValorDesconto.Text), +
                                                                    clsParser.DecimalParse(tbxValorJuros.Text), + 
                                                                    clsParser.DecimalParse(tbxValorMulta.Text), +
                                                                    0, atevencimento);

            tbxValorLiquido.Text = DuplicataInfo.valorliquido.ToString("N2");
            tbxMulta.Text = DuplicataInfo.multas.ToString("N2");
            tbxJuros.Text = DuplicataInfo.juros.ToString("N2");
            tbxDesconto.Text = DuplicataInfo.descontos.ToString("N2");
            tbxSaldo.Text = ((clsParser.DecimalParse(tbxValor.Text) + clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text)) - clsParser.DecimalParse(tbxDesconto.Text)).ToString("N2");
            totaldias = DuplicataInfo.ntotaldias;
            tbxDiferenca.Text = ((clsParser.DecimalParse(tbxSaldo.Text) - ( clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");



            Pagar01Grid();

        }
        private void CalcularPagando()
        {
            //calcular o total que esta pagando apos ter excluido 1 linha

            tbxPagando.Text = "0";

            for (int x = 0; x < dtPagar01.Rows.Count; x++)
            {
                if (dtPagar01.Rows[x].RowState != DataRowState.Deleted &&
                    dtPagar01.Rows[x].RowState != DataRowState.Detached)
                {
                    if (dtPagar01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) != "08")
                    {
                        if (dtPagar01.Rows[x]["DEBCRED"].ToString() == "D")
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                        else
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                    }
                }
            }
            tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");
            tbxSaldo.Text = ((clsParser.DecimalParse(tbxValor.Text) + clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text)) - clsParser.DecimalParse(tbxDesconto.Text)).ToString("N2");
            tbxDiferenca.Text = ((clsParser.DecimalParse(tbxSaldo.Text) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");

            if (clsParser.DecimalParse(tbxRecebido.Text) > 0)
            {
                Decimal ValorPagtoParcial = valorPrincipalDuplicata - ValorPrincipal;
                if (ValorPagtoParcial > 0)
                {  // colocar como valor principal agora
                    for (int x = 0; x < dtPagar01.Rows.Count; x++)
                    {
                        if (dtPagar01.Rows[x]["CODCOBRANCA"].ToString().PadRight(2, ' ').Substring(0, 2) == "00")
                        {
                            //ValorPagtoParcial = ValorPagtoParcial - ValorDescontoPago;

                            dtPagar01.Select("Posicao=" + dtPagar01.Rows[x]["POSICAO"].ToString())[0]["VALOR"] = ValorPagtoParcial;
                        }
                    }
                    // somar novamente para não haver problemas com o saldo
                    tbxPagando.Text = "0";

                    for (int x = 0; x < dtPagar01.Rows.Count; x++)
                    {
                        if (dtPagar01.Rows[x].RowState != DataRowState.Deleted &&
                            dtPagar01.Rows[x].RowState != DataRowState.Detached)
                        {
                            if (dtPagar01.Rows[x]["CODCOBRANCA"].ToString().PadRight(2, ' ').Substring(0, 2) != "08")
                            {
                                if (dtPagar01.Rows[x]["DEBCRED"].ToString() == "D")
                                {
                                    tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                                }
                                else
                                {
                                    tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                                }
                            }
                        }
                    }
                    tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");
                    tbxSaldo.Text = ((clsParser.DecimalParse(tbxValor.Text) + clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text)) - clsParser.DecimalParse(tbxDesconto.Text)).ToString("N2");
                    tbxDiferenca.Text = ((clsParser.DecimalParse(tbxSaldo.Text) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");

                }
                
            }

        }
        private void Pagar01Grid()
        {
            SqlDataAdapter sda;
            sda = new SqlDataAdapter("select * from Pagar01 where idduplicata = " + id, clsInfo.conexaosqldados);
            dtPagar01 = new DataTable();
            sda.Fill(dtPagar01);
            // Confirma se o valor da devolução foi lançado direto pela nota fiscal
            if (clsParser.DecimalParse(tbxDevolucao.Text) > 0)
            {
                if (dtPagar01.Rows.Count == 0)
                {  // não foi feito lançamento tem que incluir aqui a devolução.
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    scn.Open();
                    scd = new SqlCommand("insert into PAGAR01 (" +
                         "IDDUPLICATA, DATAENVIO, DATAOK, IDCOBRANCACOD, IDCOBRANCAHIS, VALOR, DEBCRED, IDRECEBIDA01, " +
                         "MOTIVO, IDHISTORICO, IDCENTROCUSTO, IDCODIGOCTABIL, VALORCOMISSAO, VALORCOMISSAOGER, VALORCOMISSAOSUP " +
                         ") values (" +
                         "@IDDUPLICATA, @DATAENVIO, @DATAOK, @IDCOBRANCACOD, @IDCOBRANCAHIS, @VALOR, @DEBCRED, @IDRECEBIDA01, " +
                         "@MOTIVO, @IDHISTORICO, @IDCENTROCUSTO, @IDCODIGOCTABIL, @VALORCOMISSAO, @VALORCOMISSAOGER, @VALORCOMISSAOSUP " +
                         ")", scn);
                    scd.Parameters.Add("@IDDUPLICATA", SqlDbType.Int).Value = id;
                    scd.Parameters.Add("@DATAENVIO", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(tbxVencimento.Text);
                    scd.Parameters.Add("@DATAOK", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(tbxDataBaixa.Text);
                    scd.Parameters.Add("@IDCOBRANCACOD", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaocobrancacod where codigo='10'"));
                    scd.Parameters.Add("@IDCOBRANCAHIS", SqlDbType.Int).Value = 0;
                    scd.Parameters.Add("@VALOR", SqlDbType.Decimal).Value = clsParser.DecimalParse(tbxDevolucao.Text);
                    scd.Parameters.Add("@DEBCRED", SqlDbType.NVarChar).Value = "C";
                    scd.Parameters.Add("@IDRECEBIDA01", SqlDbType.Int).Value = 0;
                    scd.Parameters.Add("@MOTIVO", SqlDbType.NVarChar).Value = "DV";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        scd.Parameters.Add("@IDHISTORICO", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idhistoricoreceber from situacaocobrancacod where codigo='10'"));
                        scd.Parameters.Add("@IDCENTROCUSTO", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcentrocustoreceber from situacaocobrancacod where codigo='10'"));
                    }
                    else
                    {
                        scd.Parameters.Add("@IDHISTORICO", SqlDbType.Int).Value = idHistorico;
                        scd.Parameters.Add("@IDCENTROCUSTO", SqlDbType.Int).Value = idCentrocusto;
                    }
                    scd.Parameters.Add("@IDCODIGOCTABIL", SqlDbType.Int).Value = clsInfo.zcontacontabil;
                    scd.Parameters.Add("@VALORCOMISSAO", SqlDbType.Decimal).Value = 0;
                    scd.Parameters.Add("@VALORCOMISSAOGER", SqlDbType.Decimal).Value = 0;
                    scd.Parameters.Add("@VALORCOMISSAOSUP", SqlDbType.Decimal).Value = 0;

                    scd.ExecuteNonQuery();

                    MessageBox.Show("Duplicata Reanalisada - Você deve sair da Duplicata e depois entrar Novamente");


                }
            }

            //////////
            DataColumn colunaPosicao = new DataColumn("Posicao", Type.GetType("System.Int32"));
            dtPagar01.Columns.Add(colunaPosicao);

            DataColumn colunaCodcobranca = new DataColumn("codcobranca", Type.GetType("System.String"));
            dtPagar01.Columns.Add(colunaCodcobranca);

            DataColumn colunaCodhistorico = new DataColumn("codhistorico", Type.GetType("System.String"));
            dtPagar01.Columns.Add(colunaCodhistorico);

            DataColumn colunaHistorico = new DataColumn("historico", Type.GetType("System.String"));
            dtPagar01.Columns.Add(colunaHistorico);

            DataColumn colunaCentrocusto = new DataColumn("centrocusto", Type.GetType("System.String"));
            dtPagar01.Columns.Add(colunaCentrocusto);

            DataColumn colunaContacontabil = new DataColumn("contacontabil", Type.GetType("System.String"));
            dtPagar01.Columns.Add(colunaContacontabil);


            dgvPagar01.DataSource = dtPagar01;
            clsGridHelper.MontaGrid2(dgvPagar01, dtPagar01Colunas, true);
            clsGridHelper.FontGrid(dgvPagar01, 7);
            dgvPagar01.Columns["DATAENVIO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPagar01.Columns["DATAOK"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPagar01.Columns["VALOR"].DefaultCellStyle.Format = "N2";
            dgvPagar01.Columns["VALORCOMISSAO"].DefaultCellStyle.Format = "N2";
            dgvPagar01.Columns["VALORCOMISSAOGER"].DefaultCellStyle.Format = "N2";
            dgvPagar01.Columns["VALORCOMISSAOSUP"].DefaultCellStyle.Format = "N2";

            if (clsParser.DecimalParse(tbxValor.Text) > 0)
            {
                DataRow rowPrincipal = dtPagar01.NewRow();
                rowPrincipal["DATAENVIO"] = DateTime.Parse(tbxDataBaixa.Text); ;
                rowPrincipal["DATAOK"] = DateTime.Parse(tbxDataCredito.Text); ;
                // pegar o id da situacaocobrancacod
                //rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "00".ToString()));
                rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO ='" + "00".ToString() + "'", "0"));

                if (clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()) > 0)
                {
                    //rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                    rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "").Trim();

                    //rowPrincipal["CODCOBRANCA"] = rowPrincipal["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                    rowPrincipal["CODCOBRANCA"] = rowPrincipal["CODCOBRANCA"] + " " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "").Trim();

                    rowPrincipal["IDCOBRANCAHIS"] = "0";
                    rowPrincipal["CODHISTORICO"] = "";
                    if (clsParser.DecimalParse(tbxDesconto.Text) > 0)
                    {
                        rowPrincipal["VALOR"] = clsParser.DecimalParse(tbxValor.Text) - clsParser.DecimalParse(tbxDesconto.Text);
                    }
                    else
                    {
                        rowPrincipal["VALOR"] = clsParser.DecimalParse(tbxValor.Text);
                    }
                    rowPrincipal["DEBCRED"] = "D";
                    rowPrincipal["MOTIVO"] = "00";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        //rowPrincipal["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));
                        rowPrincipal["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "0"));

                        //rowPrincipal["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString())).Trim();
                        rowPrincipal["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + clsParser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString()), "0").Trim();

                        //rowPrincipal["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));
                        rowPrincipal["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "0"));

                        //rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString())).Trim();
                        rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString()), "").Trim();
                    }
                    else
                    {
                        rowPrincipal["IDHISTORICO"] = idHistorico;
                        rowPrincipal["HISTORICO"] = tbxHistorico.Text;
                        rowPrincipal["IDCENTROCUSTO"] = idCentrocusto;
                        rowPrincipal["CENTROCUSTO"] = tbxCentroCusto.Text;
                    }
                    rowPrincipal["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                    rowPrincipal["CONTACONTABIL"] = "";
                    rowPrincipal["VALORCOMISSAO"] = clsParser.DecimalParse(tbxValorComissao.Text).ToString("N2");
                    rowPrincipal["VALORCOMISSAOGER"] = clsParser.DecimalParse(tbxValorComissaoGer.Text).ToString("N2");
                    rowPrincipal["VALORCOMISSAOSUP"] = clsParser.DecimalParse(tbxValorComissaoSup.Text).ToString("N2");
                }
                else
                {
                    MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (00-Valor Titulo) ");
                }

                dtPagar01.Rows.Add(rowPrincipal);
            }
            if (clsParser.DecimalParse(tbxMulta.Text) > 0)
            { // multa
                DataRow rowMulta = dtPagar01.NewRow();
                rowMulta["DATAENVIO"] = DateTime.Parse(tbxDataBaixa.Text); ;
                rowMulta["DATAOK"] = DateTime.Parse(tbxDataBaixa.Text);
                // pegar o id da situacaocobrancacod
                //rowMulta["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "02".ToString()));
                rowMulta["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO ='" + "02".ToString() + "'", "0"));

                if (clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString()) > 0)
                {
                    //rowMulta["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString())).Trim();
                    rowMulta["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString()), "").Trim();

                    //rowMulta["CODCOBRANCA"] = rowMulta["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString())).Trim();
                    rowMulta["CODCOBRANCA"] = rowMulta["CODCOBRANCA"] + " " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString()), "").Trim();

                    rowMulta["IDCOBRANCAHIS"] = 0;
                    rowMulta["CODHISTORICO"] = "";
                    rowMulta["VALOR"] = clsParser.DecimalParse(tbxValorMulta.Text).ToString();
                    rowMulta["DEBCRED"] = "D";
                    rowMulta["MOTIVO"] = "00";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        //rowMulta["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOPAGAR", "ID", clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString())));
                        rowMulta["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString()), "0"));

                        //rowMulta["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowMulta["IDHISTORICO"].ToString())).Trim(); ;
                        rowMulta["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + clsParser.Int32Parse(rowMulta["IDHISTORICO"].ToString()), "").Trim(); ;

                        //rowMulta["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOPAGAR", "ID", clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString())));
                        rowMulta["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString()), "0"));

                        //rowMulta["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowMulta["IDCENTROCUSTO"].ToString())).Trim();
                        rowMulta["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowMulta["IDCENTROCUSTO"].ToString()), "").Trim();
                    }
                    else
                    {
                        rowMulta["IDHISTORICO"] = idHistorico;
                        rowMulta["HISTORICO"] = tbxHistorico.Text;
                        rowMulta["IDCENTROCUSTO"] = idCentrocusto;
                        rowMulta["CENTROCUSTO"] = tbxCentroCusto.Text;
                    }
                    rowMulta["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                    rowMulta["CONTACONTABIL"] = "00";
                    rowMulta["VALORCOMISSAO"] = 0;
                    rowMulta["VALORCOMISSAOGER"] = 0;
                    rowMulta["VALORCOMISSAOSUP"] = 0;
                }
                else
                {
                    MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (02 - Multa) ");
                }
                dtPagar01.Rows.Add(rowMulta);
            }

            if (clsParser.DecimalParse(tbxJuros.Text) > 0)
            { // juros
                DataRow rowJuros = dtPagar01.NewRow();
                rowJuros["DATAENVIO"] = DateTime.Parse(tbxDataBaixa.Text); ;
                rowJuros["DATAOK"] = DateTime.Parse(tbxDataCredito.Text); ;
                //rowJuros["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "03".ToString()));
                rowJuros["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO ='" + "03".ToString() + "'", "0"));

                if (clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString()) > 0)
                {
                    //rowJuros["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString())).Trim();
                    rowJuros["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString()), "").Trim();

                    //rowJuros["CODCOBRANCA"] = rowJuros["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString())).Trim();
                    rowJuros["CODCOBRANCA"] = rowJuros["CODCOBRANCA"] + " " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString()), "").Trim();

                    rowJuros["CODCOBRANCA"] = rowJuros["CODCOBRANCA"] + " " + totaldias.ToString() + " dias";
                    rowJuros["IDCOBRANCAHIS"] = 0;
                    rowJuros["CODHISTORICO"] = "";
                    rowJuros["VALOR"] = clsParser.DecimalParse(tbxJuros.Text).ToString();
                    rowJuros["DEBCRED"] = "D";
                    rowJuros["MOTIVO"] = "00";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        //rowJuros["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOPAGAR", "ID", clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString())));
                        rowJuros["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString()), "0"));

                        //rowJuros["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowJuros["IDHISTORICO"].ToString())).Trim(); ;
                        rowJuros["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + clsParser.Int32Parse(rowJuros["IDHISTORICO"].ToString()), "").Trim(); ;

                        //rowJuros["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOPAGAR", "ID", clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString())));
                        rowJuros["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString()), "0"));

                        //rowJuros["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowJuros["IDCENTROCUSTO"].ToString())).Trim();
                        rowJuros["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowJuros["IDCENTROCUSTO"].ToString()), "").Trim();

                    }
                    else
                    {
                        rowJuros["IDHISTORICO"] = idHistorico;
                        rowJuros["HISTORICO"] = tbxHistorico.Text;
                        rowJuros["IDCENTROCUSTO"] = idCentrocusto;
                        rowJuros["CENTROCUSTO"] = tbxCentroCusto.Text;
                    }
                    rowJuros["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                    rowJuros["CONTACONTABIL"] = "00";
                    rowJuros["VALORCOMISSAO"] = 0;
                    rowJuros["VALORCOMISSAOGER"] = 0;
                    rowJuros["VALORCOMISSAOSUP"] = 0;
                }
                else
                {
                    MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (03 - Juros) ");
                }
                dtPagar01.Rows.Add(rowJuros);

            }


            if (clsParser.DecimalParse(tbxDesconto.Text) > 0)
            {
                DataRow rowDesconto = dtPagar01.NewRow();
                rowDesconto["DATAENVIO"] = DateTime.Parse(tbxDataBaixa.Text); ;
                rowDesconto["DATAOK"] = DateTime.Parse(tbxDataCredito.Text); ;
                //rowDesconto["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "08".ToString()));
                rowDesconto["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO ='" + "08".ToString() + "'", "0"));


                if (clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString()) > 0)
                {
                    //rowDesconto["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString())).Trim();
                    rowDesconto["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString()), "").Trim();

                    //rowDesconto["CODCOBRANCA"] = rowDesconto["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString())).Trim();
                    rowDesconto["CODCOBRANCA"] = rowDesconto["CODCOBRANCA"] + " " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString()), "").Trim();

                    rowDesconto["IDCOBRANCAHIS"] = 0;
                    rowDesconto["CODHISTORICO"] = "";
                    rowDesconto["VALOR"] = clsParser.DecimalParse(tbxDesconto.Text);
                    rowDesconto["DEBCRED"] = "C";
                    rowDesconto["MOTIVO"] = "00";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        //rowDesconto["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOPAGAR", "ID", clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString())));
                        rowDesconto["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString()), "0"));

                        //rowDesconto["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowDesconto["IDHISTORICO"].ToString())).Trim(); ;
                        rowDesconto["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + clsParser.Int32Parse(rowDesconto["IDHISTORICO"].ToString()), "").Trim(); ;

                        //rowDesconto["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOPAGAR", "ID", clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString())));
                        rowDesconto["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString()), "0"));

                        // rowDesconto["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowDesconto["IDCENTROCUSTO"].ToString())).Trim();
                        rowDesconto["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowDesconto["IDCENTROCUSTO"].ToString()), "").Trim();
                    }
                    else
                    {
                        rowDesconto["IDHISTORICO"] = idHistorico;
                        rowDesconto["HISTORICO"] = tbxHistorico.Text;
                        rowDesconto["IDCENTROCUSTO"] = idCentrocusto;
                        rowDesconto["CENTROCUSTO"] = tbxCentroCusto.Text;
                    }

                    rowDesconto["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                    rowDesconto["CONTACONTABIL"] = "00";
                    rowDesconto["VALORCOMISSAO"] = 0;
                    rowDesconto["VALORCOMISSAOGER"] = 0;
                    rowDesconto["VALORCOMISSAOSUP"] = 0;
                }
                else
                {
                    MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (08 - Desconto) ");
                }
                dtPagar01.Rows.Add(rowDesconto);
            }

            tbxPagando.Text = "0";
            for (int x = 0; x < dtPagar01.Rows.Count; x++)
            {
                dtPagar01.Rows[x]["Posicao"] = x + 1;
                if (!String.IsNullOrEmpty(dtPagar01.Rows[x]["CODCOBRANCA"].ToString()))
                {
                    if (dtPagar01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) == "00")
                    {
                        valorPrincipalDuplicata = clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString());
                    }
                    if (dtPagar01.Rows[x]["CODCOBRANCA"].ToString().PadRight(2, ' ').Substring(0, 2) != "08")
                    {
                        if (dtPagar01.Rows[x]["DEBCRED"].ToString() == "D")
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                        else
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                    }
                }
            }
            tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");
            dtPagar01.AcceptChanges();
        }

        private void tspCobAlterar_Click(object sender, EventArgs e)
        {
            DataRow row;
            posicao = clsParser.Int32Parse(dgvPagar01.CurrentRow.Cells["posicao"].Value.ToString());

            try
            {
                row = dtPagar01.Select("posicao=" + posicao)[0];
            }
            catch
            {
                MessageBox.Show("Não existe nenhuma linha selecionada.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tclPagar.SelectedTab = tabRegistro;
            gbxPagar.Enabled = false;
            gbxPagarBaixa.Visible = true;
            tbxDuplicata3.Text = tbxDuplicata2.Text = tbxDuplicata1.Text = tbxDuplicata.Text;
            tbxEmissao3.Text = tbxEmissao2.Text = tbxEmissao1.Text = tbxEmissao.Text;
            tbxPosicao3.Text = tbxPosicao2.Text = tbxPosicao1.Text = tbxPosicao.Text;
            tbxPosicaoFim3.Text = tbxPosicaoFim2.Text = tbxPosicaoFim1.Text = tbxPosicaoFim.Text;
            tbxClienteCognome3.Text = tbxClienteCognome2.Text = tbxClienteCognome1.Text = tbxClienteCognome.Text;
            tbxClienteTelefone3.Text = tbxClienteTelefone2.Text = tbxClienteTelefone1.Text = tbxClienteTelefone.Text;
            tbxClienteCGC3.Text = tbxClienteCGC2.Text = tbxClienteCGC1.Text = tbxClienteCGC.Text;
            
            // colocar os campos do grid selecionado / indicado
            pagar01_idcobrancacod = clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString());
            pagar01_idcobrancahis = clsParser.Int32Parse(row["IDCOBRANCAHIS"].ToString());
            pagar01_idhistorico = clsParser.Int32Parse(row["IDHISTORICO"].ToString());
            pagar01_idcentrocusto = clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString());
            pagar01_idcodigoctabil = clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString());

            //tbxPagar01CobrancaCodCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", pagar01_idcobrancacod);
            tbxPagar01CobrancaCodCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + pagar01_idcobrancacod, "");
           
            //tbxPagar01CobrancaCodNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", pagar01_idcobrancacod);
            tbxPagar01CobrancaCodNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + pagar01_idcobrancacod, "");

            //tbxPagar01CobrancaCod1Codigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", pagar01_idcobrancahis);
            tbxPagar01CobrancaCod1Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + pagar01_idcobrancahis, "");
           
            //tbxPagar01CobrancaCod1Nome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", pagar01_idcobrancahis);
            tbxPagar01CobrancaCod1Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD1 where ID = " + pagar01_idcobrancahis, "");

            tbxPagar01Valor.Text = clsParser.DecimalParse(row["VALOR"].ToString()).ToString("N2");
            tbxPagar01DC.Text = row["DEBCRED"].ToString();


            //tbxPagar01HistCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", pagar01_idhistorico);
            tbxPagar01HistCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + pagar01_idhistorico, "");
          
            //tbxPagar01HistNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", pagar01_idhistorico);
            tbxPagar01HistNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from HISTORICOS where ID = " + pagar01_idhistorico, "");

            //tbxPagar01CCCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", pagar01_idcentrocusto);
            tbxPagar01CCCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + pagar01_idcentrocusto, "");
           
            //tbxPagar01CCNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", pagar01_idcentrocusto);
            tbxPagar01CCNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID = " + pagar01_idcentrocusto, "");

            //tbxPagar01CtabilCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", pagar01_idcodigoctabil);
            tbxPagar01CtabilCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID = " + pagar01_idcodigoctabil, "");
           
            //tbxPagar01CtabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", pagar01_idcodigoctabil);
            tbxPagar01CtabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID = " + pagar01_idcodigoctabil, "");

            tbxPagar01Comissao.Text = clsParser.DecimalParse(row["VALORCOMISSAO"].ToString()).ToString("N2");
            tbxPagar01ComissaoGer.Text = clsParser.DecimalParse(row["VALORCOMISSAOGER"].ToString()).ToString("N2");
            tbxPagar01ComissaoSup.Text = clsParser.DecimalParse(row["VALORCOMISSAOSUP"].ToString()).ToString("N2");

            tbxPagar01CobrancaCodCodigo.Select();
            tbxPagar01CobrancaCodCodigo.SelectAll();
        }

        private void tspCobExcluir_Click(object sender, EventArgs e)
        {
            resultado = MessageBox.Show("Deseja apagar/excluir este item ? ( " + dgvPagar01.CurrentRow.Cells["codcobranca"].Value.ToString() + " )", "Aplisoft",
                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1);

            if (resultado == DialogResult.Yes)
            {
                dtPagar01.Select("Posicao=" + dgvPagar01.CurrentRow.Cells["posicao"].Value.ToString())[0].Delete();
                

                //calcular o total que esta pagando apos ter excluido 1 linha

                tbxPagando.Text = "0";

                for (int x = 0; x < dtPagar01.Rows.Count; x++)
                {
                    if (dtPagar01.Rows[x].RowState != DataRowState.Deleted &&
                        dtPagar01.Rows[x].RowState != DataRowState.Detached)
                    {
                        if (dtPagar01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) != "08")
                        {
                            if (dtPagar01.Rows[x]["DEBCRED"].ToString() == "D")
                            {
                                tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                            }
                            else
                            {
                                tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                            }
                        }
                    }
                }
                tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");
            }

        }

        private void tspCobBaixar_Click(object sender, EventArgs e)
        {
            Boolean oK = true;
            if (clsInfo.zbaixatemnotafiscal.ToString() != "S")
            {
                if (clsParser.Int64Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM NFCOMPRA where id=" + idNotafiscal)) == 0)
                {
                    MessageBox.Show("Você não pode baixar uma duplicata de previsão de pagamento !!!" + Environment.NewLine + " " +
                                   " 1 - Apague esta Duplicata " + Environment.NewLine + " " +
                                   " 2 - Lance em Nota Fiscal de Entrada [NFE] " + Environment.NewLine + " " +
                                   " 3 - Ai sim poderá baixar ... ");

                    
                    oK = false;
                }
                else
                {
                    if (tbxDocumento.Text.ToUpper().IndexOf("NF") != -1)
                    {
                        oK = true;
                    }
                    else if (tbxDocumento.Text.ToUpper().IndexOf("DES") != -1)
                    {
                        oK = true;
                    }
                    else
                    {
                        MessageBox.Show("Você não pode baixar uma duplicata de previsão de pagamento !!!" + Environment.NewLine + " " +
                                       " 1 - Apague esta Duplicata " + Environment.NewLine + " " +
                                       " 2 - Lance em Nota Fiscal de Entrada [NFE] " + Environment.NewLine + " " +
                                       " 3 - Ai sim poderá baixar ... ");
                        oK = false;
                    }
                }
            }
            else
            {  // S = sim pode baixar sem nota fiscal
                oK = true;
            }
            PagarSalvar();
            if (oK == true)
            {
                if (clsParser.DecimalParse(tbxDiferencaPagamento.Text) < 0)
                {
                    MessageBox.Show("" + clsInfo.zusuario + " você esta pagando ou recebendo valor indevido (Saldo Negativo)" + Environment.NewLine + "" +
                                    "Saldo da Duplicata : " + tbxDiferencaPagamento.Text + " ? ");
                }
                else
                {
                    tclPagar.SelectedTab = tabBaixa;
                    gbxPagar.Enabled = false;
                    gbxBaixar.Visible = true;
                    tbxBaixaValorPago.Text = tbxPagando.Text;
                    tbxBaixaValorPendente.Text = tbxDiferencaPagamento.Text;
                    tbxBaixaHistorico.Text = tbxHistorico.Text;
                    tbxBaixaHistoricoNome.Text = tbxHistoricoNome.Text;
                    tbxBaixaCentroCusto.Text = tbxCentroCusto.Text;
                    tbxBaixaCentroCustoNome.Text = tbxCentroCustoNome.Text;
                    tbxBaixaContacontabil.Text = tbxContaContabil.Text;
                    tbxBaixaContacontabilNome.Text = tbxContaContabilNome.Text;
                    idHistoricoBaixa = idHistorico;
                    idCentrocustoBaixa = idCentrocusto;
                    idContacontabilBaixa = idContacontabil;
                    tbxBaixaFormaPagto.Text = tbxFormaPagto.Text;
                    idFormapagtoBaixa = idFormapagto;
                    tbxBaixaBcoInt.Text = tbxBancoInt.Text;
                    tbxBaixaBcoIntNome.Text = tbxBancoNome.Text;
                    tbxBaixaBoletoNro.Text = tbxBoletoNro.Text;
                    //tbxBaixaNominal.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "NOME", "ID", idFornecedor).Trim();
                    tbxBaixaNominal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CLIENTE where ID = " + idFornecedor, "").Trim();
                
                    tbxBaixaValorComissao.Text = tbxValorComissao.Text;
                    tbxBaixaValorComissaoGer.Text = tbxValorComissaoGer.Text;
                    tbxBaixaValorComissaoSup.Text = tbxValorComissaoSup.Text;
                    tbxDataBaixa2.Text = tbxDataBaixa.Text;
                    tbxDataCredito2.Text = tbxDataCredito.Text;

                    // Se a Data da Baixa for superior a 3 dias enviar uma mensagem
                    dataBaixa = DateTime.Parse(tbxDataBaixa.Text);
                    dataVencimento = DateTime.Parse(tbxVencimento.Text);

                    tbxAviso.Text = ("Dt Baixa =" + tbxDataBaixa.Text + "  Dt Deposito = " + tbxDataCredito.Text + " ");

                    dias = dataBaixa.Subtract(dataVencimento).Days;
                    if (dias > 0)
                    {
                        MessageBox.Show("Data da Baixa com " + dias + " dias de diferença." + Environment.NewLine + "Diferença entre a Data de Vencimento e a Data da Baixa ");
                        tbxAviso.Text = tbxAviso.Text + " Verifique : " + dias + " dias de diferença entre a Data de Vencimento e a Data da Baixa ";
                        tbxAviso.ForeColor = Color.Red;
                    }
//                    tbxBaixaHistorico.Select();
//                    tbxBaixaHistorico.SelectAll();
                    tbxBaixaFormaPagto.Select();
                    tbxBaixaFormaPagto.SelectAll();

                }
            }
        }
        private void tbxBaixaValorComissaoSup_TextChanged(object sender, EventArgs e)
        {
        }

        private void tspSalvarObserva_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar e alterar?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    clsPagarObservaInfo = new clsPagarObservaInfo();
                    PagarObservaFillInfo(clsPagarObservaInfo);
                    PagarObservaFillInfoToGrid(clsPagarObservaInfo);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }
                tabPagar.Select();
                gbxObserva.Visible = false;
                gbxPagar.Enabled = true;
                tclPagar.SelectedTab = tabPagar;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspRetornarObserva_Click(object sender, EventArgs e)
        {
            tabPagar.Select();
            gbxObserva.Visible = false;
            gbxPagar.Enabled = true;
            tclPagar.SelectedTab = tabPagar;

        }

        private void tspSalvarPagar01_Click(object sender, EventArgs e)
        {
            DataRow row;
            try
            {
                row = dtPagar01.Select("posicao=" + posicao)[0];
            }
            catch
            {
                MessageBox.Show("Posição da tabela não foi encontrada.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            row["IDCOBRANCACOD"] = pagar01_idcobrancacod;
            row["IDCOBRANCAHIS"] = pagar01_idcobrancahis;
            row["IDHISTORICO"] = pagar01_idhistorico;
            row["IDCENTROCUSTO"] = pagar01_idcentrocusto;
            row["IDCODIGOCTABIL"] = pagar01_idcodigoctabil;
            //row["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString())).Trim();
            row["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString()), "").Trim();
          
            //row["CODCOBRANCA"] = row["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString())).Trim();
            row["CODCOBRANCA"] = row["CODCOBRANCA"] + " " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString()), "").Trim();
           
            row["CODHISTORICO"] = tbxPagar01CobrancaCod1Codigo.Text;
            row["HISTORICO"] = tbxPagar01HistCod.Text;
            row["CENTROCUSTO"] = tbxPagar01CCCod.Text;
            row["CONTACONTABIL"] = tbxContaContabil.Text;
            row["VALOR"] = clsParser.DecimalParse(tbxPagar01Valor.Text);
            row["DEBCRED"] = tbxPagar01DC.Text;
            row["VALORCOMISSAO"] = clsParser.DecimalParse(tbxPagar01Comissao.Text);
            row["VALORCOMISSAOGER"] = clsParser.DecimalParse(tbxPagar01ComissaoGer.Text);
            row["VALORCOMISSAOSUP"] = clsParser.DecimalParse(tbxPagar01ComissaoSup.Text);

            dtPagar01.AcceptChanges();

            //calcular o total que esta pagando apos ter excluido 1 linha

            tbxPagando.Text = "0";

            for (int x = 0; x < dtPagar01.Rows.Count; x++)
            {
                if (dtPagar01.Rows[x].RowState != DataRowState.Deleted &&
                    dtPagar01.Rows[x].RowState != DataRowState.Detached)
                {
                    if (dtPagar01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) != "08")
                    {
                        if (dtPagar01.Rows[x]["DEBCRED"].ToString() == "D")
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                        else
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtPagar01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                    }
                }
            }
            tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");


            
            tabPagar.Select();
            gbxPagarBaixa.Visible = false;
            gbxPagar.Enabled = true;
            tclPagar.SelectedTab = tabPagar;
        }

        private void tspPrimeiroPagar01_Click(object sender, EventArgs e)
        {

        }

        private void tspAnteriorPagar01_Click(object sender, EventArgs e)
        {

        }

        private void tspProximoPagar01_Click(object sender, EventArgs e)
        {

        }

        private void tspUltimoPagar01_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornarPagar01_Click(object sender, EventArgs e)
        {
            tabPagar.Select();
            gbxPagarBaixa.Visible = false;
            gbxPagar.Enabled = true;
            tclPagar.SelectedTab = tabPagar;
        }

        private void tspSalvarBaixa_Click(object sender, EventArgs e)
        {
            // Verificar se gravou os Id necessarios
            if (clsParser.Int32Parse(tbxBaixaBcoInt.Text) > 0)
            {
                if (idHistoricoBaixa > 0)
                {
                    if (idCentrocustoBaixa > 0)
                    {
                        if (idContacontabilBaixa > 0)
                        {
                            if (idFormapagtoBaixa > 0)
                            {
                                if (idBancointBaixa > 0)
                                {
                                    //using (TransactionScope tse = new TransactionScope())
                                    //{
                                        // transferindo para o Aplibank  ==> 1a Fase 
                                        Decimal ValorCheque = 0;
                                        String NroLote = "";
                                        String Comobaixou = "";
                                        DateTime databaixa = DateTime.Parse(tbxDataBaixa2.Text);
                                        DateTime datacredito = DateTime.Parse(tbxDataCredito.Text);
                                        //TimeSpan Compensar = 0;

                                        NroLote = "PAG" + DateTime.Now.ToString("yyyyMMddHHmmss") + clsParser.Int32Parse(tbxBaixaBoletoNro.Text).ToString("0000000000") + clsInfo.zusuarioid.ToString("0000000");
                                        // 
                                        //Compensar = datacredito.Subtract(databaixa);
                                        switch (tbxBaixaFormaPagto.Text.Substring(0, 2))
                                        {
                                            case "CH":
                                                Comobaixou = "02-Cheque na Cta= " + tbxBaixaBcoInt.Text.Trim() + " - " + tbxBaixaBoletoNro.Text.Trim();
                                                break;
                                            case "DP":
                                                Comobaixou = "01-Deposito na Cta= " + tbxBaixaBcoInt.Text.Trim() + " - " + tbxBaixaBoletoNro.Text.Trim();
                                                break;
                                            default:
                                                Comobaixou = tbxBaixaFormaPagto.Text + " Cta = " + tbxBaixaBcoInt.Text.Trim() + " - " + tbxBaixaBoletoNro.Text.Trim(); ;
                                                break;
                                        }
                                        DataRow row;


                                        /// Baixar
                                        for (int x = 0; x < dtPagar01.Rows.Count; x++)
                                        {  // só deixei porque no programa anterior não cadastravam o desconto (08)
                                            // neste estou lançando tudo  
                                            row = dtPagar01.Rows[x];
                                            switch (row["codcobranca"].ToString().Substring(0, 2))
                                            {
                                                case "00":
                                                    ValorCheque = clsParser.DecimalParse(tbxPagando.Text);
                                                    break;
                                                case "08":
                                                    // quando for desconto incluir como principal para bater os calculos 
                                                    // primeiro verifique o historico do banco pagar sera 999999
                                                    //Int32 idHistoricoEsp = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ID", "CODIGO", "999999"));
                                                    Int32 idHistoricoEsp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO ='" + "999999" + "'", "0"));
                                                    
                                                    if (idHistoricoEsp == 0)
                                                    { // criar a conta no aplibank
                                                        idHistoricoEsp = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO, NOME, ATIVO", "'999999', 'Pagar Desconto Concedido', 'S'");
                                                    }
                                                    DataRow rowPrincipal = dtPagar01.NewRow();
                                                    rowPrincipal["DATAENVIO"] = DateTime.Now;
                                                    rowPrincipal["DATAOK"] = DateTime.Now;
                                                    if (clsInfo.zbaixarcomhistorico == "CX")
                                                    {
                                                        // pegar o id da situacaocobrancacod
                                                        //rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "01".ToString()));
                                                        rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO = '" +  "01".ToString() + "'", "0"));
                                                       
                                                        if (clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()) > 0)
                                                        {
                                                            //rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                                                            rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "").Trim();
                                                           
                                                            //rowPrincipal["CODCOBRANCA"] = rowPrincipal["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                                                            rowPrincipal["CODCOBRANCA"] = rowPrincipal["CODCOBRANCA"] + " " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "").Trim();
                                                           
                                                            rowPrincipal["IDCOBRANCAHIS"] = "0";
                                                            rowPrincipal["CODHISTORICO"] = "";
                                                            rowPrincipal["VALOR"] = clsParser.DecimalParse(tbxDesconto.Text);
                                                            rowPrincipal["DEBCRED"] = "D";
                                                            rowPrincipal["MOTIVO"] = "01";
                                                            rowPrincipal["IDHISTORICO"] = idHistoricoEsp; // clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));
                                                            //rowPrincipal["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idHistoricoEsp); //Parser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString())).Trim();
                                                            rowPrincipal["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + idHistoricoEsp, ""); //Parser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString())).Trim();

                                                            //rowPrincipal["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));
                                                            rowPrincipal["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "0"));
                                                           
                                                            //rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString())).Trim();
                                                            rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString()), "").Trim();
                                                           
                                                            rowPrincipal["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                                                            rowPrincipal["CONTACONTABIL"] = "";
                                                            rowPrincipal["VALORCOMISSAO"] = clsParser.DecimalParse(tbxValorComissao.Text).ToString("N2");
                                                            rowPrincipal["VALORCOMISSAOGER"] = clsParser.DecimalParse(tbxValorComissaoGer.Text).ToString("N2");
                                                            rowPrincipal["VALORCOMISSAOSUP"] = clsParser.DecimalParse(tbxValorComissaoSup.Text).ToString("N2");
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (00-Valor Titulo) ");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // pegar o id da situacaocobrancacod
                                                        //rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "01".ToString()));
                                                        rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO ='" + "01".ToString() + "'", "0"));
                                                      
                                                        if (clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()) > 0)
                                                        {
                                                            //rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                                                            rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "").Trim();
                                                            
                                                            //rowPrincipal["CODCOBRANCA"] = rowPrincipal["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                                                            rowPrincipal["CODCOBRANCA"] = rowPrincipal["CODCOBRANCA"] + " " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "").Trim();
                                                           
                                                            rowPrincipal["IDCOBRANCAHIS"] = "0";
                                                            rowPrincipal["CODHISTORICO"] = "";
                                                            rowPrincipal["VALOR"] = clsParser.DecimalParse(tbxDesconto.Text);
                                                            rowPrincipal["DEBCRED"] = "D";
                                                            rowPrincipal["MOTIVO"] = "01";
                                                            rowPrincipal["IDHISTORICO"] = idHistoricoEsp; // clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));
                                                            //rowPrincipal["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idHistoricoEsp); //Parser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString())).Trim();
                                                            rowPrincipal["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + idHistoricoEsp, ""); //Parser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString())).Trim();

                                                            rowPrincipal["IDCENTROCUSTO"] = idCentrocustoBaixa;  // clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));
                                                            rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID=" + clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString()), "").Trim();
                                                            //rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString())).Trim();
                                                            rowPrincipal["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                                                            rowPrincipal["CONTACONTABIL"] = "";
                                                            rowPrincipal["VALORCOMISSAO"] = clsParser.DecimalParse(tbxValorComissao.Text).ToString("N2");
                                                            rowPrincipal["VALORCOMISSAOGER"] = clsParser.DecimalParse(tbxValorComissaoGer.Text).ToString("N2");
                                                            rowPrincipal["VALORCOMISSAOSUP"] = clsParser.DecimalParse(tbxValorComissaoSup.Text).ToString("N2");
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (00-Valor Titulo) ");
                                                        }
                                                    }
                                                    dtPagar01.Rows.Add(rowPrincipal);
                                                    break;
                                                default:
                                                    ValorCheque = clsParser.DecimalParse(tbxPagando.Text);
                                                    break;
                                            }

                                            String nrocheque = "";
                                            if (clsParser.Int32Parse(tbxBancoConta.Text) > 0)
                                            {
                                                nrocheque = "(" + tbxBancoConta.Text + ")" + tbxBaixaBoletoNro.Text;
                                            }
                                            else
                                            {
                                                nrocheque = tbxBaixaBoletoNro.Text;
                                            }
                                            Int32 conferido = 0;
                                            // Colocar o historico no complemento do lançamento
                                            String complemento = "";
                                            if (tbxBaixaHistorico.Text != "NC")
                                            {
                                                complemento = tbxBaixaHistoricoNome.Text;
                                            }
                                            if (tbxObserva.Text.Length > 0)
                                            {
                                                if (complemento.Length > 0)
                                                {
                                                    complemento = complemento + "-" + tbxObserva.Text;
                                                }
                                                else
                                                {
                                                    complemento = tbxObserva.Text;
                                                }

                                            }
                                            if (tbxNotaDoc.Text.Length > 0)
                                            {
                                                if (complemento.Length > 0)
                                                {
                                                    complemento = complemento + "-" + tbxNotaDoc.Text;
                                                }
                                                else
                                                {
                                                    complemento = tbxNotaDoc.Text;
                                                }
                                            }
                                            if (tbxNFVendaNumero.Text.Length > 0)
                                            {
                                                if (complemento.Length > 0)
                                                {
                                                    complemento = complemento + "=" + tbxNFVendaNumero.Text;
                                                }
                                                else
                                                {
                                                    complemento = tbxNFVendaNumero.Text;
                                                }
                                            }
                                            if (tbxBaixaBoletoNro.Text.Length >= 2)
                                            {
                                                if (complemento.Length > 0)
                                                {
                                                    complemento = complemento + "- BO/CH= " + tbxBaixaBoletoNro.Text;
                                                }
                                                else
                                                {
                                                    complemento = "BO/CH= " + tbxBaixaBoletoNro.Text;
                                                }
                                            }
                                            tbxObserva.Text = complemento;

                                            clsFinanceiro.BaixarBanco("PAGAR", id, NroLote, tbxBaixaBoletoNro.Text, idBancointBaixa, clsParser.Int32Parse(row["IDHISTORICO"].ToString()),
                                                                      clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString()), clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString()),
                                                                      DateTime.Parse(tbxDataBaixa2.Text), DateTime.Parse(tbxDataCredito.Text),
                                                                      clsParser.DecimalParse(row["VALOR"].ToString()),
                                                                      row["codcobranca"].ToString(), row["DEBCRED"].ToString(), tbxObserva.Text, tbxClienteCognome2.Text, tbxDocumento.Text,
                                                                      tbxNotaDoc.Text, tbxNFVendaNumero.Text, clsParser.Int32Parse(tbxPosicao.Text), clsParser.Int32Parse(tbxPosicaoFim.Text), tbxBaixaFormaPagto.Text.Substring(0, 2), clsInfo.zusuario, nrocheque, conferido, "UNICA");
                                        }
                                        // Transferir para o Contas Pagas
                                        for (int x = 0; x < dtPagar01.Rows.Count; x++)
                                        {  // só deixei porque no programa anterior não cadastravam o desconto (08)
                                            // neste estou lançando tudo  
                                            row = dtPagar01.Rows[x];
                                            switch (row["codcobranca"].ToString().Substring(0, 2))
                                            {
                                                case "00":
                                                    ValorCheque = clsParser.DecimalParse(tbxPagando.Text);
                                                    break;
                                                default:
                                                    ValorCheque = clsParser.DecimalParse(tbxPagando.Text);
                                                    break;
                                            }
                                            clsFinanceiro.TransferirPago("PAGAR", id, tbxDataBaixa2.Text,
                                                tbxDataCredito.Text,
                                                clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString()),
                                                clsParser.Int32Parse(row["IDCOBRANCAHIS"].ToString()),
                                                clsParser.DecimalParse(row["VALOR"].ToString()),
                                                row["DEBCRED"].ToString(),
                                                clsParser.Int32Parse(row["IDHISTORICO"].ToString()),
                                                clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString()),
                                                clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString()),
                                                Comobaixou,
                                                clsParser.DecimalParse(row["VALORCOMISSAO"].ToString()),
                                                clsParser.DecimalParse(row["VALORCOMISSAOGER"].ToString()),
                                                clsParser.DecimalParse(row["VALORCOMISSAOSUP"].ToString()));
                                        }
                                        // Transferir as Observações do Contas a Pagar para o Pagas
                                        clsFinanceiro.TransferirPagoObserva("PAGAR", id);

                                        if (clsInfo.zbaixatemnotafiscal != "S")
                                        {
                                            // Marcar na Nota Fiscal que foi Pago
                                            clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "NFCOMPRAPAGAR",
                                                                    "PAGOU = 'S'", "ID = " + idPagarNFE);
                                        }

                                        // Excluir a Duplicata se foi Total
                                        if (clsParser.DecimalParse(tbxDiferencaPagamento.Text) == 0)
                                        {
                                            // Apagar observação do pagar
                                            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
                                            SqlCommand scd = new SqlCommand("delete pagarobserva where idduplicata=@id", scn);
                                            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                            scn.Open();
                                            scd.ExecuteNonQuery();
                                            scn.Close();
                                            // Apagar os itens do pagar
                                            scn = new SqlConnection(clsInfo.conexaosqldados);
                                            scd = new SqlCommand("delete pagar01 where idduplicata=@id", scn);
                                            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                            scn.Open();
                                            scd.ExecuteNonQuery();
                                            scn.Close();
                                            // Apagar o pagar
                                            scn = new SqlConnection(clsInfo.conexaosqldados);
                                            scd = new SqlCommand("delete pagar where id=@id", scn);
                                            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                            scn.Open();
                                            scd.ExecuteNonQuery();
                                            scn.Close();
                                        }
                                        else
                                        {
                                            // verificar o que foi pago
                                            VerificarPago();
                                        }

                                    //    tse.Complete();
                                    //}

                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Não adicionou a Conta do Banco Interno (Aplisoft)!!");
                                    tbxBaixaBcoInt.Select();
                                    tbxBaixaBcoInt.SelectAll();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Não adicionou a Forma de Pagto na Baixa da Duplicata !!");
                                tbxBaixaFormaPagto.Select();
                                tbxBaixaFormaPagto.SelectAll();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não adicionou a Conta Contabil na Baixa da Duplicata !!");
                            tbxBaixaContacontabil.Select();
                            tbxBaixaContacontabil.SelectAll();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não adicionou o Centro de Custo na Baixa da Duplicata !!");
                        tbxBaixaCentroCusto.Select();
                        tbxBaixaCentroCusto.SelectAll();
                    }
                }
                else
                {
                    MessageBox.Show("Não adicionou o Historico na Baixa da Duplicata !!");
//                    tbxBaixaHistorico.Select();
//                    tbxBaixaHistorico.SelectAll();
                    tbxBaixaFormaPagto.Select();
                    tbxBaixaFormaPagto.SelectAll();

                }
            }
            else
            {
                MessageBox.Show("Indique a Conta do Aplibank que vai efetuar o Pagamento !!");
                tbxBaixaBcoInt.Select();
                tbxBaixaBcoInt.SelectAll();
            }
        }
        private void tspRetornarBaixa_Click(object sender, EventArgs e)
        {
            tbxAviso.ForeColor = Color.Black;
            tabPagar.Select();
            gbxBaixar.Visible = false;
            gbxPagar.Enabled = true;
            tclPagar.SelectedTab = tabPagar;
        }


        private void tbxClienteTelefone2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBaixaIdHistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBaixaIdHistorico.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", idHistoricoBaixa, "Baixas de Banco");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnBaixaIdCentroCusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBaixaIdCentroCusto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS",idCentrocustoBaixa, "Centro de Custos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnBaixaIdContaContabil_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBaixaIdContaContabil.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONTACONTABIL", idContacontabilBaixa, "Contas Contabeis");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnBaixaIdFormaPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBaixaIdFormaPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", idFormapagtoBaixa, "Situação de Tipo do Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnBaixaIdBancoInt_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBaixaIdBancoInt.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBancointBaixa, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }

        private void dgvPagar01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspCobAlterar.PerformClick();
        }

        private void btnPagar01_idcobrancacod_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPagar01_idcobrancacod.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", pagar01_idcobrancacod, "Situacao de Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnPagar01_idcobrancahis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPagar01_idcobrancahis.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD1", pagar01_idcobrancahis, "Situação de Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnPagar01_idhistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPagar01_idhistorico.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", pagar01_idhistorico, "Hisoricos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void btnPagar01_idcentrocusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPagar01_idcentrocusto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", pagar01_idcentrocusto, "Centro de Custos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void btnPagar01_idcodigoctabil_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPagar01_idcodigoctabil.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONTACONTABIL", pagar01_idcodigoctabil, "Contas Contabeis");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void bwrPagarObserva_RunWorkerAsync()
        {
            bwrPagarObserva = new BackgroundWorker();
            bwrPagarObserva.DoWork += new DoWorkEventHandler(bwrPagarObserva_DoWork);
            bwrPagarObserva.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPagarObserva_RunWorkerCompleted);
            bwrPagarObserva.RunWorkerAsync();
        }

        private void bwrPagarObserva_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                String tabela = "PAGAROBSERVA ";
                String query = "ID, IDDUPLICATA, DATA, OBSERVAR, EMITENTE";
                String ordem = "DATA";
                String filtro = "IDDUPLICATA=" + id ;

                dtPagarObserva = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, tabela, query, filtro, ordem);
                dtPagarObserva.Columns.Add("POSICAO", Type.GetType("System.Int32"));

                for (Int32 x = 0; x < dtPagarObserva.Rows.Count; x++ )
                {
                    dtPagarObserva.Rows[x]["posicao"] = x + 1;
                }
                dtPagarObserva.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bwrPagarObserva_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvPagarObserva.DataSource = dtPagarObserva;

                clsGridHelper.MontaGrid(dgvPagarObserva,
                            new String[] { "ID", "IDDUPLICATA", "Data", "Observação", "Emitente", "Posicao" },
                            new String[] { "ID", "IDDUPLICATA", "DATA", "OBSERVAR", "EMITENTE", "POSICAO" },
                            new Int32[] { 0, 0, 70, 90, 85, 0 },
                            new DataGridViewContentAlignment[] {
                            DataGridViewContentAlignment.MiddleLeft,
                            DataGridViewContentAlignment.MiddleRight,
                            DataGridViewContentAlignment.MiddleCenter,
                            DataGridViewContentAlignment.MiddleLeft,
                            DataGridViewContentAlignment.MiddleLeft,
                            DataGridViewContentAlignment.MiddleRight},
                            new Boolean[] { false, false, true, true, true, false },
                            true, 2, ListSortDirection.Ascending);
                dgvPagarObserva.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
                clsGridHelper.FontGrid(dgvPagarObserva, 7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tspPagarObservaIncluir_Click(object sender, EventArgs e)
        {

            // incluir
            pagarobserva_posicao = 0;
            PagarObservaCarregar();
/*
            DataRow rowIncluir = dtPagarObserva.NewRow();
            posicaoPagarObserva = dtPagarObserva.Rows.Count + 1;
            rowIncluir["IDDUPLICATA"] = id;
            rowIncluir["DATA"] = DateTime.Now;
            rowIncluir["EMITENTE"] = clsInfo.zusuario;
            rowIncluir["OBSERVAR"] = "";
            rowIncluir["POSICAO"] = posicaoPagarObserva;
            dtPagarObserva.Rows.Add(rowIncluir);
            PagarObservaGridCampos(posicaoPagarObserva);
            tbxObservaObservar.Select();
            tbxObservaObservar.SelectAll();
 */

        }
        private void tspPagarObservaAlterar_Click(object sender, EventArgs e)
        {
            if (dgvPagarObserva.CurrentRow != null)
            {
                pagarobserva_posicao = Int32.Parse(dgvPagarObserva.CurrentRow.Cells["posicao"].Value.ToString());
                PagarObservaCarregar();
            }
        }

        void PagarObservaCarregar()
        {
            clsPagarObservaInfo = new clsPagarObservaInfo();
            clsPagarObservaInfoOld = new clsPagarObservaInfo();
            if (pagarobserva_posicao == 0)
            {
                pagarobserva_posicao = dtPagarObserva.Rows.Count + 1;


            }
            else
            {
                PagarObservaGridToInfo(clsPagarObservaInfo, pagarobserva_posicao);
            }
            PagarObservaCampos(clsPagarObservaInfo);
            PagarObservaFillInfo(clsPagarObservaInfoOld);


            tbxDuplicata3.Text = tbxDuplicata2.Text = tbxDuplicata1.Text = tbxDuplicata.Text;
            tbxEmissao3.Text = tbxEmissao2.Text = tbxEmissao1.Text = tbxEmissao.Text;
            tbxPosicao3.Text = tbxPosicao2.Text = tbxPosicao1.Text = tbxPosicao.Text;
            tbxPosicaoFim3.Text = tbxPosicaoFim2.Text = tbxPosicaoFim1.Text = tbxPosicaoFim.Text;
            tbxClienteCognome3.Text = tbxClienteCognome2.Text = tbxClienteCognome1.Text = tbxClienteCognome.Text;
            tbxClienteTelefone3.Text = tbxClienteTelefone2.Text = tbxClienteTelefone1.Text = tbxClienteTelefone.Text;
            tbxClienteCGC3.Text = tbxClienteCGC2.Text = tbxClienteCGC1.Text = tbxClienteCGC.Text;
            tbxDocumento3.Text = tbxDocumento2.Text = tbxDocumento1.Text = tbxDocumento.Text;


            tclPagar.SelectedTab = tabObserva;
            gbxPagar.Enabled = false;
            gbxObserva.Visible = true;

        }

        private void PagarObservaGridToInfo(clsPagarObservaInfo info, Int32 posicao)
        {
            DataRow row = dtPagarObserva.Select("posicao = " + posicao)[0];

            info.id = Int32.Parse(row["id"].ToString());
            info.data =  DateTime.Parse(row["data"].ToString());
            info.emitente = row["emitente"].ToString();
            info.idduplicata = id;
            info.observar = row["observar"].ToString();
        }
        private void PagarObservaCampos(clsPagarObservaInfo info)
        {
            pagarobserva_id = info.id;
            pagarobserva_idduplicata = id;
            if (info.id == 0)
            {
                tbxObservaData.Text =  DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                tbxObservaEmitente.Text = clsInfo.zusuario;
            }
            else
            {
                tbxObservaData.Text = info.data.ToString("dd/MM/yyyy HH:mm");
                tbxObservaEmitente.Text = info.emitente;
            }
            tbxObservaObservar.Text = info.observar;
            if (info.id == 0)
            {
                gbxObservaTexto.Enabled = true;
                tbxObservaObservar.Select();
                tbxObservaObservar.SelectAll();
            }
            else
            {
                gbxObservaTexto.Enabled = false;
            }

        }

        private void PagarObservaFillInfo(clsPagarObservaInfo info)
        {
            info.id = pagarobserva_id;
            info.idduplicata = pagarobserva_idduplicata;
            info.data = DateTime.Parse(tbxObservaData.Text);
            info.emitente = tbxObservaEmitente.Text;
            info.observar = tbxObservaObservar.Text;
        }

        void PagarObservaFillInfoToGrid(clsPagarObservaInfo info)
        {
            DataRow row;
            DataRow[] rows = dtPagarObserva.Select("posicao = " + pagarobserva_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtPagarObserva.NewRow();
            }

            row["id"] = info.id;

            row["DATA"] = info.data;
            row["EMITENTE"] = info.emitente;
            row["IDDUPLICATA"] = info.idduplicata;
            row["OBSERVAR"] = info.observar;

            // Colunas que petencem a outras tabelas
            //row["formapagto"] = OrcamentoReceber_tbxFormaPagto.Text;


            if (rows.Length == 0)
            {
                row["posicao"] = pagarobserva_posicao;
                dtPagarObserva.Rows.Add(row);
            }
        }

        private void PagarObservaGridCampos(Int32 posicao)
        {
            DataRow row = dtPagarObserva.Select("POSICAO=" + posicao)[0];
            tbxObservaData.Text = clsParser.SqlDateTimeParse(row["DATA"].ToString()).Value.ToString("dd/MM/yyyy");
            tbxObservaEmitente.Text = row["EMITENTE"].ToString();
            tbxObservaObservar.Text = row["OBSERVAR"].ToString();
        }


        private void dgvPagarObserva_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspPagarObservaAlterar.PerformClick();
        }
        private void SalvarPagarObserva()
        {
            String query;
            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
            SqlCommand scd;

            foreach (DataRow row in dtPagarObserva.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    if (clsParser.Int32Parse(row["ID"].ToString()) > 0)
                    {
                        // Apagar observação do pagar
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scd = new SqlCommand("delete pagarobserva where id=@id", scn);
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();

                    }
                }
                else if (row.RowState == DataRowState.Added)
                {
                    // Incluir
                    query = "Insert Into PAGAROBSERVA (IDDUPLICATA, DATA, OBSERVAR, EMITENTE) VALUES (@IDDUPLICATA, @DATA, @OBSERVAR, @EMITENTE); SELECT SCOPE_IDENTITY()";
                    scd = new SqlCommand(query, scn);

                    scd.Parameters.Add("@IDDUPLICATA", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDDUPLICATA"].ToString());
                    scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(row["DATA"].ToString());
                    scd.Parameters.Add("@OBSERVAR", SqlDbType.NVarChar).Value = row["OBSERVAR"].ToString();
                    scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = row["EMITENTE"].ToString();
                    scn.Open();
                    scd.ExecuteNonQuery();
                    //row["ID"] = scd.ExecuteScalar(); APENAS SE FOR PUXAR O ID
                    scn.Close();
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    query = "UPDATE PAGAROBSERVA SET IDDUPLICATA =@IDDUPLICATA, DATA=@DATA, OBSERVAR=@OBSERVAR, EMITENTE=@EMITENTE WHERE ID=@ID";
                    scd = new SqlCommand(query, scn);
                    scd.Parameters.Add("@ID", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());
                    scd.Parameters.Add("@IDDUPLICATA", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDDUPLICATA"].ToString());
                    scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(row["DATA"].ToString());
                    scd.Parameters.Add("@OBSERVAR", SqlDbType.NVarChar).Value = row["OBSERVAR"].ToString();
                    scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = row["EMITENTE"].ToString();
                    scn.Open();
                    scd.ExecuteNonQuery();
                    //row["ID"] = scd.ExecuteScalar(); APENAS SE FOR PUXAR O ID
                    scn.Close();
                }
            }

        }
        private void VerificarPago()
        {
            DataTable dtPago = new DataTable();
            dtPago = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "PAGAS " +
                                "left join PAGAS01 on PAGAS01.IDDUPLICATA=PAGAS.ID  " +
                                "left join SITUACAOCOBRANCACOD on SITUACAOCOBRANCACOD.id=PAGAS01.IDCOBRANCACOD  " +
                                "left join SITUACAOCOBRANCACOD1 on SITUACAOCOBRANCACOD1.id=PAGAS01.IDCOBRANCAHIS  ",
                                "PAGAS01.ID, PAGAS01.IDDUPLICATA, PAGAS01.DATAENVIO, PAGAS01.DATAOK, " +
                                "SITUACAOCOBRANCACOD.CODIGO + '=' + SITUACAOCOBRANCACOD.NOME AS [COB], SITUACAOCOBRANCACOD1.CODIGO AS [HIS], " +
                                "PAGAS01.VALOR, PAGAS01.DEBCRED, PAGAS01.BAIXOUCOMO ",
                                "PAGAS.IDPAGAR = " + clsParser.Int32Parse(id.ToString()), "DATAENVIO");
            tbxRecebido.Text = "0";
            ValorPrincipal = 0;
            foreach (DataRow drPago in dtPago.Rows)
            { // Somar os Pagamentos
                if (drPago["DEBCRED"].ToString() == "D")
                {
                    tbxRecebido.Text = (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(drPago["VALOR"].ToString())).ToString("N2");
                    if (drPago["COB"].ToString().Substring(0, 2) == "00" || drPago["COB"].ToString().Substring(0, 2) == "01")
                    {
                        ValorPrincipal = ValorPrincipal + clsParser.DecimalParse(drPago["VALOR"].ToString());
                    }
                }
                else
                {
                    if (drPago["COB"].ToString().Substring(0, 2) == "08")
                    {
                        // não descontar 
                        // Separar o Valor Para finalizar o Pagamento
                        ValorDescontoPago = clsParser.DecimalParse(drPago["VALOR"].ToString());
                        // EXCLUIR DA MOVIMENTAÇÃO DO TITULO QUE GERA O TOTAL A PAGAR
                        for (int x = 0; x < dtPagar01.Rows.Count; x++)
                        {
                            if (dtPagar01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) == "08")
                            {
                                dtPagar01.Select("Posicao=" + dtPagar01.Rows[x]["POSICAO"].ToString())[0].Delete();
                                dtPagar01.AcceptChanges();
                            }
                        }
                    }
                    else
                    {
                        tbxRecebido.Text = (clsParser.DecimalParse(tbxRecebido.Text) - clsParser.DecimalParse(drPago["VALOR"].ToString())).ToString("N2"); 
                    }
                    if (drPago["COB"].ToString().Substring(0, 2) == "00" || drPago["COB"].ToString().Substring(0, 2) == "01")
                    {
                        ValorPrincipal = ValorPrincipal - clsParser.DecimalParse(drPago["VALOR"].ToString());
                    }
                }
            }
            // GRAVAR O VALOR PRINCIPAL COMO VALOR PAGO
            // DEVIDO AO QUE MOSTRO NA TELA PRINCIPAL EU NÃO COLOCO OS JUROS PAGO AQUI POIS AI PODERIA FICAR NEGATIVO
            // FICA ERRADO SE ELE PAGOU JUROS (VERIFICAR NO FUTURO)
            // tbxrecebido = valor pago ( ele soma tudo quando entra no form )
            // como não grava este campo ele clsVisualmente fica certo
            

            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("UPDATE PAGAR SET VALORPAGO=@valorprincipal WHERE id=@id", scn);
            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            scd.Parameters.Add("@valorprincipal", SqlDbType.Decimal).Value = ValorPrincipal;
            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();

            if (clsParser.DecimalParse(tbxRecebido.Text) > 0)
            {
                gbxBaseCalculo.Enabled = false;
                
                // SE TIVER LANÇAMENTO DE MULTA NA GRADE DA MOVIMENTAÇÃO FINANCEIRA TEMOS QUE EXCLUIR
                CalcularPagando();
            }
        }
        private void bwrPagas01_RunWorkerAsync()
        {
            bwrPagas01 = new BackgroundWorker();
            bwrPagas01.DoWork += new DoWorkEventHandler(bwrPagas01_DoWork);
            bwrPagas01.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPagas01_RunWorkerCompleted);
            bwrPagas01.RunWorkerAsync();
        }

        private void bwrPagas01_DoWork(object sender, DoWorkEventArgs e)
        {
            dtPagas01 = CarregaGridPagas01();  
        }
        public DataTable CarregaGridPagas01()
        {
            try
            {

                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                       "PAGAS " +
                       "left join PAGAS01 on PAGAS01.IDDUPLICATA=PAGAS.ID " +
                       "left join SITUACAOCOBRANCACOD on SITUACAOCOBRANCACOD.id=PAGAS01.IDCOBRANCACOD " +
                       "left join SITUACAOCOBRANCACOD1 on SITUACAOCOBRANCACOD1.id=PAGAS01.IDCOBRANCAHIS ",
                       "PAGAS01.ID, PAGAS01.IDDUPLICATA, PAGAS01.DATAENVIO, PAGAS01.DATAOK, " +
                       "SITUACAOCOBRANCACOD.CODIGO + '=' + SITUACAOCOBRANCACOD.NOME AS [COB], " +
                       "PAGAS01.VALOR, PAGAS01.DEBCRED, PAGAS01.BAIXOUCOMO ",
                       "PAGAS.IDPAGAR = " + id, "SITUACAOCOBRANCACOD.CODIGO");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrPagas01_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvPagas01.DataSource = dtPagas01;
                clsGridHelper.MontaGrid(dgvPagas01,
                        new String[] {"ID", "Id Dup", "Data Baixa", "Data Pagto" , "Codigo Cobrança" ,
                                         "Valor", "D/C",
                                        "Como Baixou"},
                        new String[] {"ID","IDDUPLICATA", "DATAENVIO", "DATAOK", "COB",
                                        "VALOR", "DEBCRED",
                                        "BAIXOUCOMO"},
                        new int[] { 1, 1, 60, 60, 120,
                                    70, 25, 
                                    200},
                        new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleRight,
                         DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft},
                        new bool[] { false, false, true, true, true, 
                             true, true, true},
                        true, 3, ListSortDirection.Ascending);

                dgvPagas01.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvPagas01.Columns["DATAENVIO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPagas01.Columns["DATAOK"].DefaultCellStyle.Format = "dd/MM/yyyy";

                clsGridHelper.FontGrid(dgvPagas01, 7);

                //tbxRecebido.Text = clsGridHelper.SomaGrid(dgvPagas01, "VALOR").ToString("N2");


                dgvPagas01.Sort(dgvPagas01.Columns["DATAENVIO"], ListSortDirection.Ascending);

                //GridHelper.SelecionaLinha(indexPagar, dgvPagas01, 1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbxValorDesconto_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxValorJuros_TextChanged(object sender, EventArgs e)
        {

        }



        /*
        private void SalvarDuplicata()
        {

            if (id == 0)    // Incluindo os valores do Cabeçalho -- valores locais
            {

                idPagar = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "PAGAR", " " +
                    "FILIAL,DUPLICATA,POSICAO,POSICAOFIM,EMISSAO,IDDOCUMENTO,SETOR,IDFORNECEDOR,DATALANCA,EMITENTE, " +
                    "IDHISTORICO, IDCENTROCUSTO, IDCODIGOCTABIL, IDNOTAFISCAL, OBSERVA, IDFORMAPAGTO, IDBANCO, " +
                    "IDBANCOINT, CHEGOU, DESPESAPUBLICA, BOLETONRO, DV, BAIXA, IDSITBANCO, BOLETO, VENCIMENTO, VENCIMENTOPREV, " +
                    "ATEVENCIMENTO, VALOR, VALORDESCONTO, VALORLIQUIDO, VALORJUROS, VALORMULTA, " +
                    "VALORPAGO, VALORDEVOLVIDO ",
                    clsParser.SqlInt32Format(tbxFilial.Text, false, "FILIAL") +
                    clsParser.SqlInt32Format(tbxDuplicata.Text, false, "DUPLICATA") +
                    clsParser.SqlInt32Format(tbxPosicao.Text, false, "POSICAO") +
                    clsParser.SqlInt32Format(tbxPosicaoFim.Text, false, "POSICAOFIM") +
                    clsParser.SqlDateTimeFormat(tbxEmissao.Text, false, "EMISSAO") +
                    clsParser.SqlInt32Format(idDocumento.ToString(), false, "IDDOCUMENTO") +
                    clsParser.SqlStringFormat(setor.ToString(), false, "SETOR") +
                    clsParser.SqlInt32Format(idFornecedor.ToString(), false, "IDFORNECEDOR") +
                    clsParser.SqlDateTimeFormat(tbxDataLanca.Text, false, "DATALANCA") +
                    clsParser.SqlStringFormat(tbxEmitente.Text, false, "EMITENTE") +
                    clsParser.SqlInt32Format(idHistorico.ToString(), false, "IDHISTORICO") +
                    clsParser.SqlInt32Format(idCentrocusto.ToString(), false, "IDCENTROCUSTO") +
                    clsParser.SqlInt32Format(idContacontabil.ToString(), false, "IDCODIGOCTABIL") +
                    clsParser.SqlInt32Format(idNotafiscal.ToString(), false, "IDNOTAFISCAL") +
                    clsParser.SqlStringFormat(tbxObserva.Text, false, "OBSERVA") +
                    clsParser.SqlInt32Format(idFormapagto.ToString(), false, "IDFORMAPAGTO") +
                    clsParser.SqlInt32Format(idBanco.ToString(), false, "IDBANCO") +
                    clsParser.SqlInt32Format(idBancoint.ToString(), false, "IDBANCOINT") +
                    clsParser.SqlStringFormat(chegou.ToString(), false, "CHEGOU") +
                    clsParser.SqlStringFormat(despesapublica.ToString(), false, "DESPESAPUBLICA") +
                    clsParser.SqlInt32Format(tbxBoletoNro.Text, false, "BOLETONRO") +
                    clsParser.SqlStringFormat(tbxDv.Text, false, "DV") +
                    clsParser.SqlStringFormat(tbxBaixa.Text, false, "BAIXA") +
                    clsParser.SqlInt32Format(idSitBanco.ToString(), false, "IDSITBANCO") +
                    clsParser.SqlStringFormat(tbxBoleto.Text, false, "BOLETO") +
                    clsParser.SqlDateTimeFormat(tbxVencimento.Text, false, "VENCIMENTO") +
                    clsParser.SqlDateTimeFormat(tbxVencimentoPrev.Text, false, "VENCIMENTOPREV") +
                    clsParser.SqlStringFormat(atevencimento.ToString(), false, "ATEVENCIMENTO") +
                    clsParser.SqlDecimalFormat(tbxValor.Text, false, "VALOR") +
                    clsParser.SqlDecimalFormat(tbxValorDesconto.Text, false, "VALORDESCONTO") +
                    clsParser.SqlDecimalFormat(tbxValorLiquido.Text, false, "VALORLIQUIDO") +
                    clsParser.SqlDecimalFormat(tbxValorJuros.Text, false, "VALORJUROS") +
                    clsParser.SqlDecimalFormat(tbxValorMulta.Text, false, "VALORMULTA") +
                    clsParser.SqlDecimalFormat(tbxRecebido.Text, false, "VALORPAGO") +
//                    clsParser.SqlDecimalFormat("0".ToString(), false, "VALORBAIXANDO") +
                    clsParser.SqlDecimalFormat(tbxDevolucao.Text, true, "VALORDEVOLVIDO"));
            }
            else
            {
                // Alterando os valores do Cabeçalho -- valores locais
                clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "PAGAR",
                    "FILIAL = " + clsParser.SqlInt32Format(tbxFilial.Text, false, "FILIAL") +
                    "DUPLICATA = " + clsParser.SqlInt32Format(tbxDuplicata.Text, false, "DUPLICATA") +
                    "POSICAO = " + clsParser.SqlInt32Format(tbxPosicao.Text, false, "POSICAO") +
                    "POSICAOFIM = " + clsParser.SqlInt32Format(tbxPosicaoFim.Text, false, "POSICAOFIM") +
                    "EMISSAO = " + clsParser.SqlDateTimeFormat(tbxEmissao.Text, false, "EMISSAO") +
                    "IDDOCUMENTO = " + clsParser.SqlInt32Format(idDocumento.ToString(), false, "IDDOCUMENTO") +
                    "SETOR = " + clsParser.SqlStringFormat(setor.ToString(), false, "SETOR") +
                    "IDFORNECEDOR = " + clsParser.SqlInt32Format(idFornecedor.ToString(), false, "IDFORNECEDOR") +
                    "DATALANCA = " + clsParser.SqlDateTimeFormat(tbxDataLanca.Text, false, "DATALANCA") +
                    "EMITENTE = " + clsParser.SqlStringFormat(tbxEmitente.Text, false, "EMITENTE") +
                    "IDHISTORICO = " + clsParser.SqlInt32Format(idHistorico.ToString(), false, "IDHISTORICO") +
                    "IDCENTROCUSTO = " + clsParser.SqlInt32Format(idCentrocusto.ToString(), false, "IDCENTROCUSTO") +
                    "IDCODIGOCTABIL = " + clsParser.SqlInt32Format(idContacontabil.ToString(), false, "IDCODIGOCTABIL") +
                    "IDNOTAFISCAL = " + clsParser.SqlInt32Format(idNotafiscal.ToString(), false, "IDNOTAFISCAL") +
                    "OBSERVA = " + clsParser.SqlStringFormat(tbxObserva.Text, false, "OBSERVA") +
                    "IDFORMAPAGTO = " + clsParser.SqlInt32Format(idFormapagto.ToString(), false, "IDFORMAPAGTO") +
                    "IDBANCO = " + clsParser.SqlInt32Format(idBanco.ToString(), false, "IDBANCO") +
                    "IDBANCOINT = " + clsParser.SqlInt32Format(idBancoint.ToString(), false, "IDBANCOINT") +
                    "CHEGOU = " + clsParser.SqlStringFormat(chegou.ToString(), false, "CHEGOU") +
                    "DESPESAPUBLICA = " + clsParser.SqlStringFormat(despesapublica.ToString(), false, "DESPESAPUBLICA") +
                    "BOLETONRO = " + clsParser.SqlInt32Format(tbxBoletoNro.Text, false, "BOLETONRO") +
                    "DV = " + clsParser.SqlStringFormat(tbxDv.Text, false, "DV") +
                    "BAIXA = " + clsParser.SqlStringFormat(tbxBaixa.Text, false, "BAIXA") +
                    "IDSITBANCO = " + clsParser.SqlInt32Format(idSitBanco.ToString(), false, "IDSITBANCO") +
                    "BOLETO = " + clsParser.SqlStringFormat(tbxBoleto.Text, false, "BOLETO") +
                    "VENCIMENTO = " + clsParser.SqlDateTimeFormat(tbxVencimento.Text, false, "VENCIMENTO") +
                    "VENCIMENTOPREV = " + clsParser.SqlDateTimeFormat(tbxVencimentoPrev.Text, false, "VENCIMENTOPREV") +
                    "ATEVENCIMENTO = " + clsParser.SqlStringFormat(atevencimento.ToString(), false, "ATEVENCIMENTO") +
                    "VALOR = " + clsParser.SqlDecimalFormat(tbxValor.Text, false, "VALOR") +
                    "VALORDESCONTO = " + clsParser.SqlDecimalFormat(tbxValorDesconto.Text, false, "VALORDESCONTO") +
                    "VALORLIQUIDO = " + clsParser.SqlDecimalFormat(tbxValorLiquido.Text, false, "VALORLIQUIDO") +
                    "VALORJUROS = " + clsParser.SqlDecimalFormat(tbxValorJuros.Text, false, "VALORJUROS") +
                    "VALORMULTA = " + clsParser.SqlDecimalFormat(tbxValorMulta.Text, false, "VALORMULTA") +
                    "VALORPAGO = " + clsParser.SqlDecimalFormat(tbxRecebido.Text, false, "VALORPAGO") +
//                    "VALORBAIXANDO = " + clsParser.SqlDecimalFormat("0".ToString(), false, "VALORBAIXANDO") +
                    "VALORDEVOLVIDO = " + clsParser.SqlDecimalFormat(tbxDevolucao.Text, true, "VALORDEVOLVIDO"),
                    "ID = " + idPagar.ToString());
            }
        }
        */


    }
}
