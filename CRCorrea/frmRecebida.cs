using CRCorreaFuncoes;

using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRecebida : Form
    {
        Int32 idRecebida;
        Int32 idReceberNFV;
        Int32 idRecebidaObserva;
        Int32 idRecebida01;

        //Int32 posicaoPagar01;
        //Int32 posicaoRecebidaObserva;

        Int32 idDocumento;
        Int32 idCliente;
        Int32 idHistorico;
        Int32 idContacontabil;
        Int32 idCentrocusto;
        Int32 idNotafiscal;
        Int32 idFormapagto;
        Int32 idBanco;
        Int32 idBancoint;
        Int32 idSitBanco;
        Int32 idDocNFV;
        Int32 idcoordenador;
        Int32 idsupervisor;
        Int32 idvendedor;


        Int32 totaldias;
        //Int32 dias;

        String setor;
        String chegou;
        String despesapublica;
        String atevencimento;

        DialogResult resultado;

        //DataGridView dgvPagar;

        String[] valoresantigos = new String[] { };

        
        
        clsFinanceiro clsFinanceiro = new clsFinanceiro();
        

        // Tabela Virtual
        DataTable dtRecebida01;
        DataTable dtRecebidaObserva;

        GridColuna[] dtRecebida01Colunas;

        // Dados virtuais
        //Int32 posicao;
        Int32 recebida01_idcobrancacod;
        Int32 recebida01_idcobrancahis;
        Int32 recebida01_idhistorico;
        Int32 recebida01_idcentrocusto;
        Int32 recebida01_idcodigoctabil;

        //DateTime dataBaixa;
        DateTime dataVencimento;

        //Decimal valorPrincipalDuplicata = 0;  // VALOR PRINCIPAL A PAGAR
        //Decimal ValorPrincipal = 0;           // VALORPRINCIPAL PAGO
        //Decimal ValorDescontoPago = 0;        // Valor de Desconto que foi pago parcial (antecipado)

        public frmRecebida()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idRecebida)
        {
            idRecebida = _idRecebida;

            //tbxDataBaixa.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //tbxDataCredito.Text = DateTime.Today.ToString("dd/MM/yyyy");

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


            // tela registro pagar01
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaocobrancacod order by codigo", tbxPagar01CobrancaCodCodigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", tbxPagar01HistCod);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", tbxPagar01CCCod);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from contacontabil order by codigo", tbxPagar01CtabilCodigo);


            dtRecebida01Colunas = new GridColuna[]
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

        private void frmRecebida_Load(object sender, EventArgs e)
        {
            CarregaRecebida(idRecebida);
            bwrRecebida01_RunWorkerAsync();
        }

        private void frmRecebida_Activated(object sender, EventArgs e)
        {
            Lupa();
            bwrRecebidaObserva_RunWorkerAsync(); 
        }

        private void frmRecebida_Shown(object sender, EventArgs e)
        {

        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft",
                                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                       MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {

                    if (VerificaGravacaoDuplicata("TODOS") == true)
                    {
                        if (resultado != DialogResult.Cancel)
                        {
                            using (TransactionScope tspTool = new TransactionScope())
                            {
                                SalvarDuplicata();
                                SalvarRecebidaObserva();
                                tspTool.Complete();
                                tspTool.Dispose();
                            }
                        }
                    }
                }
                else if (resultado == DialogResult.Cancel)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                if (resultado != DialogResult.Cancel)
                {
                    if (idRecebida > 0)
                    {
                       // frmPagarVis.idRecebida = idRecebida;
                    }
                    this.Close();
                    this.Dispose();
                }
            }

        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {

        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {

        }

        private void tspProximo_Click(object sender, EventArgs e)
        {

        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            tbxDuplicata3.Text =  tbxDuplicata1.Text = tbxDuplicata.Text;
            tbxEmissao3.Text = tbxEmissao1.Text = tbxEmissao.Text;
            tbxPosicao3.Text = tbxPosicao1.Text = tbxPosicao.Text;
            tbxPosicaoFim3.Text = tbxPosicaoFim1.Text = tbxPosicaoFim.Text;
            tbxClienteCognome3.Text = tbxClienteCognome1.Text = tbxClienteCognome.Text;
            tbxClienteTelefone3.Text = tbxClienteTelefone1.Text = tbxClienteTelefone.Text;
            tbxClienteCGC3.Text = tbxClienteCGC1.Text = tbxClienteCGC.Text;

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

            if (((Control)sender).Name == "tbxDocumento")
            {
                if (tbxDocumento.ReadOnly == false)
                {
                    idDocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from DOCFISCAL where COGNOME='" + tbxDocumento.Text.Trim() + "'", "0"));
                    //idDocumento = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "ID", "COGNOME", tbxDocumento.Text.Trim()));
                    if (idDocumento == 0)
                    {
                        idDocumento = clsInfo.zhistoricos;
                    }
                    tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from DOCFISCAL where ID=" + idDocumento, "").Trim();
                    //tbxDocumento.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", idDocumento).Trim();
                }
            }
            if (((Control)sender).Name == "tbxClienteCognome")
            {
                if (tbxClienteCognome.ReadOnly == false)
                {
                    idCliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from CLIENTE where COGNOME='" + tbxClienteCognome.Text.Trim() + "'", ""));
                    //idCliente = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ID", "COGNOME", tbxClienteCognome.Text.Trim()));
                    if (idCliente == 0)
                    {
                        idCliente = clsInfo.zempresaclienteid;
                    }
                    tbxClienteCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from CLIENTE where ID=" + idCliente, "").Trim();
                    //  tbxClienteCognome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idCliente).Trim();
                    tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from CLIENTE where ID=" + idCliente, "").Trim();
                    // tbxClienteCGC.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "CGC", "ID", idCliente).Trim();
                    tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from CLIENTE where ID=" + idCliente, "") +
                         " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from CLIENTE where ID=" + idCliente, "");
                    //tbxClienteTelefone.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "DDD", "ID", idCliente) +
                       //" - " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "TELEFONE", "ID", idCliente);

                }
            }
            if (((Control)sender).Name == "tbxHistorico")
            {
                if (tbxHistorico.ReadOnly == false)
                {
                    idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from HISTORICOS where CODIGO='" + tbxHistorico.Text.Trim() + "'", "0"));
                    //idHistorico = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ID", "CODIGO", tbxHistorico.Text.Trim()));
                    if (idHistorico == 0)
                    {
                        idHistorico = clsInfo.zhistoricos;
                    }
                    tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where ID=" + idHistorico, "").Trim();
                    //tbxHistorico.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idHistorico).Trim();
                    tbxHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from HISTORICOS where ID=" + idHistorico, "").Trim();
                    //    tbxHistoricoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", idHistorico).Trim();                }
                }
            }

            if (((Control)sender).Name == "tbxContaContabil")
            {
                if (tbxContaContabil.ReadOnly == false)
                {
                    idContacontabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from CONTACONTABIL where CODIGO='" + tbxContaContabil.Text + "'", "0"));
                    //idContacontabil = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "ID", "CODIGO", tbxContaContabil.Text));

                    if (idContacontabil == 0)
                    {
                        idContacontabil = clsInfo.zcontacontabil;
                    }
                    tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from CONTACONTABIL where ID=" + idContacontabil, "").Trim();
                    // tbxContaContabil.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", idContacontabil).Trim();
                    tbxContaContabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from CONTACONTABIL where ID=" + idContacontabil, "").Trim();
                    // tbxContaContabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", idContacontabil).Trim();

                }
            }
            if (((Control)sender).Name == "tbxCentroCusto")
            {
                if (tbxCentroCusto.ReadOnly == false)
                {
                    idCentrocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from CENTROCUSTOS where CODIGO='" + tbxCentroCusto.Text.Trim() + "'", ""));
                    //idCentrocusto = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ID", "CODIGO", tbxCentroCusto.Text.Trim()));

                    if (idCentrocusto == 0)
                    {
                        idCentrocusto = clsInfo.zcentrocustos;
                    }
                    tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID=" + idCentrocusto, "").Trim();
                    // tbxCentroCusto.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", idCentrocusto).Trim();
                    tbxCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from CENTROCUSTOS where ID=" + idCentrocusto, "").Trim();
                    // tbxCentroCustoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", idCentrocusto).Trim();
                }
            }

            
            if (((Control)sender).Name == "tbxNFVendaNumero")
            {
                if (tbxNFVendaNumero.ReadOnly == false)
                {
                    idNotafiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from NFCOMPRA where NUMERO=" + tbxNFVendaNumero.Text.Trim(), ""));
                    //idNotafiscal = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "ID", "NUMERO", tbxNFVendaNumero.Text.Trim()));
                    if (idNotafiscal == 0)
                    {
                        idNotafiscal = clsInfo.znfcompra;
                    }
                    tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from NFCOMPRA where ID=" + idNotafiscal, "");
                    //tbxNFVendaNumero.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "NUMERO", "ID", idNotafiscal);
                    tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TOTALNOTAFISCAL from NFCOMPRA where ID=" + idNotafiscal, "").ToString();
                    // tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "TOTALNOTAFISCAL", "ID", idNotafiscal).ToString();

                    // if (clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDDOCUMENTO", "ID", idNotafiscal)) > 0)
                    if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from NFCOMPRA where ID=" + idNotafiscal, "")) > 0)
                    {
                        idDocNFV = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from NFCOMPRA where ID=" + idNotafiscal, ""));
                        //idDocNFV = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDDOCUMENTO", "ID", idNotafiscal))));
                        if (idDocNFV == 0)
                            idDocNFV = clsInfo.zdocumento;

                        tbxNotaDoc.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from DOCFISCAL where ID=" + idDocNFV, "");
                        // tbxNotaDoc.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", idDocNFV))).Trim();
                    }
                    else
                    {
                        tbxNotaDoc.Text = "";
                    }

                }
            }
            if (((Control)sender).Name == "tbxFormaPagto")
            {
                if (tbxFormaPagto.ReadOnly == false)
                {
                    idFormapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from SITUACAOTIPOTITULO where CODIGO='" + tbxFormaPagto.Text.Substring(0, 2).Trim() + "'", ""));
                    //idFormapagto = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "ID", "CODIGO", tbxFormaPagto.Text.Substring(0, 2).Trim()));
                    if (idFormapagto == 0)
                    {
                        idFormapagto = clsInfo.zformapagto;
                    }
                    tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTIPOTITULO where ID=" + idFormapagto, "").Trim();
                    //tbxFormaPagto.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "CODIGO", "ID", idFormapagto).Trim();
                    tbxFormaPagto.Text = tbxFormaPagto.Text + "-" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTIPOTITULO where ID=" + idFormapagto, "").Trim();
                    //tbxFormaPagto.Text = tbxFormaPagto.Text + "-" + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "NOME", "ID", idFormapagto).Trim();
                }
            }
            if (((Control)sender).Name == "tbxBancoConta")
            {
                if (tbxBancoConta.ReadOnly == false)
                {
                    idBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from TAB_BANCOS where CODIGO=" + tbxBancoConta.Text.Trim(), "0"));
                    //idBanco = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "ID", "CODIGO", tbxBancoConta.Text.Trim()));
                    if (idBanco == 0)
                    {
                        idBanco = clsInfo.zbanco;
                    }
                    tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_BANCOS where ID=" + idBanco, "").Trim();
                    // tbxBancoConta.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "CODIGO", "ID", idBanco).Trim();
                    tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from TAB_BANCOS where ID=" + idBanco, "").Trim();
                    //tbxBancoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "COGNOME", "ID", idBanco).Trim();
                }
            }
            if (((Control)sender).Name == "tbxBancoInt")
            {
                if (tbxBancoInt.ReadOnly == false)
                {
                    idBancoint = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from BANCOS where CONTA=" + tbxBancoInt.Text.Trim(), "0"));
                    // idBancoint = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "ID", "CONTA", tbxBancoInt.Text.Trim()));

                    if (idBancoint == 0)
                    {
                        idBancoint = clsInfo.zbancoint;
                    }
                    tbxBancoInt.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select conta from BANCOS where ID=" + idBancoint, "");
                    //tbxBancoInt.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "CONTA", "ID", idBancoint);
                    tbxBancoNomeInterno.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from BANCOS where ID=" + idBancoint, "").Trim();
                    //tbxBancoNomeInterno.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "NOME", "ID", idBancoint).Trim();
                }
            }

            if (((Control)sender).Name == "tbxSitBanco")
            {
                if (tbxSitBanco.ReadOnly == false)
                {
                    idSitBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from SITUACAOTITULO where CODIGO='" + tbxSitBanco.Text.Trim() + "'", "0"));
                    // idSitBanco = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "ID", "CODIGO", tbxSitBanco.Text.Trim()));
                    if (idSitBanco == 0)
                    {
                        idSitBanco = clsInfo.zbanco;
                    }
                    tbxSitBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo  from SITUACAOTITULO where ID=" + idSitBanco, "").Trim();
                    // tbxSitBanco.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "CODIGO", "ID", idSitBanco).Trim();
                    tbxSituacaoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTITULO where ID=" + idSitBanco, "").Trim();
                    // tbxSituacaoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "NOME", "ID", idSitBanco).Trim();
                }
            }
            // pegar os tbx do pagar01 (registrando o valor da baixa)
            if (((Control)sender).Name == "tbxPagar01CobrancaCodCodigo")
            {
                if (tbxPagar01CobrancaCodCodigo.ReadOnly == false)
                {
                    recebida01_idcobrancacod = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from SITUACAOCOBRANCACOD where CODIGO='" + tbxPagar01CobrancaCodCodigo.Text.Trim() + "'", "0"));
                    //recebida01_idcobrancacod = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", tbxPagar01CobrancaCodCodigo.Text.Trim()));
                    if (recebida01_idcobrancacod == 0)
                    {
                        recebida01_idcobrancacod = clsInfo.zsituacaocobrancacod;
                    }
                    tbxPagar01CobrancaCodCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOCOBRANCACOD where ID=" + recebida01_idcobrancacod, "").Trim();
                    //tbxPagar01CobrancaCodCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", recebida01_idcobrancacod).Trim();
                    tbxPagar01CobrancaCodNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOCOBRANCACOD where ID=" + recebida01_idcobrancacod, "").Trim();
                    // tbxPagar01CobrancaCodNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", recebida01_idcobrancacod).Trim();
                }
            }
            if (((Control)sender).Name == "tbxPagar01CobrancaCod1Codigo") 
            {
                if (tbxPagar01CobrancaCod1Codigo.ReadOnly == false)
                {
                    recebida01_idcobrancahis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from SITUACAOCOBRANCACOD where CODIGO='" + tbxPagar01CobrancaCod1Codigo.Text.Trim() + "'", "0"));
                    // recebida01_idcobrancahis = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", tbxPagar01CobrancaCod1Codigo.Text.Trim()));
                    if (recebida01_idcobrancahis == 0)
                    {
                        recebida01_idcobrancahis = 0;
                    }
                    tbxPagar01CobrancaCod1Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOCOBRANCACOD where ID=" + recebida01_idcobrancahis, "").Trim();
                    // tbxPagar01CobrancaCod1Codigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", recebida01_idcobrancahis).Trim();
                    tbxPagar01CobrancaCod1Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOCOBRANCACOD where ID=" + recebida01_idcobrancahis, "").Trim();
                    //tbxPagar01CobrancaCod1Nome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", recebida01_idcobrancahis).Trim();
                }
            }
            if (((Control)sender).Name == "tbxPagar01HistCod") 
            {
                if (tbxPagar01HistCod.ReadOnly == false)
                {
                    recebida01_idhistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from HISTORICOS where CODIGO='" + tbxPagar01HistCod.Text.Trim() + "'", "0"));
                    //recebida01_idhistorico = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ID", "CODIGO", tbxPagar01HistCod.Text.Trim()));
                   
                    if (recebida01_idhistorico == 0)
                    {
                        recebida01_idhistorico = clsInfo.zhistoricos;
                    }
                    tbxPagar01HistCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where ID=" + recebida01_idhistorico, "").Trim();
                    //tbxPagar01HistCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", recebida01_idhistorico).Trim();
                    tbxPagar01HistNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from HISTORICOS where ID=" + recebida01_idhistorico, "").Trim();
                    //         tbxPagar01HistNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", recebida01_idhistorico).Trim();
                }
            }
            if (((Control)sender).Name == "tbxPagar01CtabilCodigo") 
            {
                if (tbxPagar01CtabilCodigo.ReadOnly == false)
                {
                    recebida01_idcodigoctabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from CONTACONTABIL where CODIGO='" + tbxPagar01CtabilCodigo.Text + "'", "0"));
                    // recebida01_idcodigoctabil = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "ID", "CODIGO", tbxPagar01CtabilCodigo.Text));
                    if (recebida01_idcodigoctabil == 0)
                    {
                        recebida01_idcodigoctabil = clsInfo.zcontacontabil;
                    }
                    tbxPagar01CtabilCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from CONTACONTABIL where ID=" + recebida01_idcodigoctabil, "").Trim();
                    // tbxPagar01CtabilCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", recebida01_idcodigoctabil).Trim();
                    tbxPagar01CtabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from CONTACONTABIL where ID=" + recebida01_idcodigoctabil, "").Trim();
                    //tbxPagar01CtabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", recebida01_idcodigoctabil).Trim();
                }
            }
            if (((Control)sender).Name == "tbxPagar01CCCod") 
            {
                if (tbxPagar01CCCod.ReadOnly == false)
                {
                    recebida01_idcentrocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from CENTROCUSTOS where CODIGO='" + tbxPagar01CCCod.Text.Trim() + "'", "0"));
                    // recebida01_idcentrocusto = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ID", "CODIGO", tbxPagar01CCCod.Text.Trim()));
                    if (recebida01_idcentrocusto == 0)
                    {
                        recebida01_idcentrocusto = clsInfo.zcentrocustos;
                    }
                    tbxPagar01CCCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID=" + recebida01_idcentrocusto, "").Trim();
                    // tbxPagar01CCCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", recebida01_idcentrocusto).Trim();
                    tbxPagar01CCNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from CENTROCUSTOS where ID=" + recebida01_idcentrocusto, "").Trim();
                    //tbxPagar01CCNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", recebida01_idcentrocusto).Trim();
                }
            }



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

            switch ( ((Control)sender).Name )
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
            case "tbxDataBaixa":
                   // calcular();
                   // VerificarPago();       //verifica se já foi pago algum valor (apenas principal)
                   // CalcularPagando();
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
        private void CarregaRecebida(Int32 idRecebida)
        {
            try
            {
                //Carrega os Dados 
                tspTool.Cursor = Cursors.Hand;
                PreencheCamposRecebida();
                valoresantigos = PreencheValoresRecebida();
                if (idRecebida == 0)
                {

                    tspPrimeiro.Enabled = false;
                    tspAnterior.Enabled = false;
                    tspProximo.Enabled = false;
                    tspUltimo.Enabled = false;
                }
                tbxDuplicata.Select();
                tbxDuplicata.SelectAll();

                //Pagar01Grid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Carrega Pagar - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void PreencheCamposRecebida()
        {
            if (idRecebida > 0)
            {
                DataRow drRecebida;
                drRecebida = clsSelectInsertUpdateBLL.SelectCollectionFields(clsInfo.conexaosqldados, "*", "RECEBIDA", "ID = " + clsParser.Int32Parse(idRecebida.ToString()), "ID");

                if (drRecebida != null)
                {
                    idReceberNFV = clsParser.Int32Parse(drRecebida["idReceberNFV"].ToString());
                    tbxFilial.Text = clsParser.Int32Parse(drRecebida["FILIAL"].ToString()).ToString();
                    tbxDuplicata.Text = clsParser.Int32Parse(drRecebida["DUPLICATA"].ToString()).ToString();
                    tbxPosicao.Text = clsParser.Int32Parse(drRecebida["POSICAO"].ToString()).ToString();
                    tbxPosicaoFim.Text = clsParser.Int32Parse(drRecebida["POSICAOFIM"].ToString()).ToString();
                    if (drRecebida["EMISSAO"].ToString() != "")
                    {
                        tbxEmissao.Text = (clsParser.SqlDateTimeParse(drRecebida["EMISSAO"].ToString())).Value.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        tbxEmissao.Text = "";
                    }

                    idDocumento = clsParser.Int32Parse(drRecebida["IDDOCUMENTO"].ToString());
                    if (idDocumento == 0)
                        idDocumento = clsInfo.zdocumento;

                    tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from DOCFISCAL where ID=" + idDocumento, "");
                    //tbxDocumento.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", idDocumento);
                    if (drRecebida["SETOR"].ToString() != "S")
                    {
                        rbnSetorSim.Checked = true;
                        setor = "S";
                    }
                    else
                    {
                        rbnSetorNao.Checked = true;
                        setor = "N";
                    }
                    idCliente = clsParser.Int32Parse(drRecebida["IDCLIENTE"].ToString());
                    if (idCliente == 0)
                        idCliente = clsInfo.zempresaclienteid;

                    tbxClienteCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from CLIENTE where ID=" + idCliente, "").Trim();
                    //tbxClienteCognome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idCliente).Trim();
                    tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from CLIENTE where ID=" + idCliente, "").Trim();
                    // tbxClienteCGC.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "CGC", "ID", idCliente).Trim();
                    tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from CLIENTE where ID=" + idCliente, "").Trim() +
                         " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from CLIENTE where ID=" + idCliente, "");
                    //tbxClienteTelefone.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "DDD", "ID", idCliente).Trim() +
                         //" - " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "TELEFONE", "ID", idCliente);

                    if (drRecebida["DATALANCA"].ToString() != "")
                    {
                        tbxDataLanca.Text = clsParser.SqlDateTimeParse(drRecebida["DATALANCA"].ToString()).Value.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        tbxDataLanca.Text = "";
                    }
                    tbxEmitente.Text = drRecebida["EMITENTE"].ToString();

                    idHistorico = clsParser.Int32Parse(drRecebida["idHistorico"].ToString());
                    if (idHistorico == 0)
                        idHistorico = clsInfo.zhistoricos;

                    tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where ID=" + idHistorico, "").Trim();
                    // tbxHistorico.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idHistorico).Trim();
                    tbxHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from HISTORICOS where ID=" + idHistorico, "").Trim();
                    //tbxHistoricoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", idHistorico).Trim();
                    
                    idCentrocusto = clsParser.Int32Parse(drRecebida["idCentrocusto"].ToString());
                    if (idCentrocusto == 0)
                        idCentrocusto = clsInfo.zcentrocustos;

                    tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID=" + idCentrocusto, "").Trim();
                    //tbxCentroCusto.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", idCentrocusto).Trim();
                    tbxCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from CENTROCUSTOS where ID=" + idCentrocusto, "").Trim();
                    //tbxCentroCustoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", idCentrocusto).Trim();

                    idContacontabil = clsParser.Int32Parse(drRecebida["IDCODIGOCTABIL"].ToString());
                    if (idContacontabil == 0)
                        idContacontabil = clsInfo.zcontacontabil;

                    tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from CONTACONTABIL where ID=" + idContacontabil, "").Trim();
                    //tbxContaContabil.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", idContacontabil).Trim();
                    tbxContaContabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from CONTACONTABIL where ID=" + idContacontabil, "").Trim();
                    //tbxContaContabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", idContacontabil).Trim();

                    idNotafiscal = clsParser.Int32Parse(drRecebida["IDNOTAFISCAL"].ToString());
                    if (idNotafiscal == 0)
                        idNotafiscal = clsInfo.zcontacontabil;

                    tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from NFCOMPRA where ID=" + idNotafiscal , "0");
                    //tbxNFVendaNumero.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "NUMERO", "ID", idNotafiscal);
                    tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TOTALNOTAFISCAL from  NFCOMPRA where ID=" + idNotafiscal, "0").ToString();
                    //tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "TOTALNOTAFISCAL", "ID", idNotafiscal).ToString();

                    //  if (clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDDOCUMENTO", "ID", idNotafiscal)) > 0)
                    if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iddocumento from NFCOMPRA where ID=" + idNotafiscal, "0")) > 0)
                    {
                        idDocNFV = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from NFCOMPRA where ID=" + idNotafiscal, "0"));
                        // idDocNFV = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDDOCUMENTO", "ID", idNotafiscal))));
                        if (idDocNFV == 0)
                            idDocNFV = clsInfo.zdocumento;

                        tbxNotaDoc.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from DOCFISCAL where ID=" + idDocNFV, "");
                    }
                    else
                    {
                        tbxNotaDoc.Text = "";
                    }

                    // if (clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDCONDPAGTO", "ID", idNotafiscal)) > 0)
                    if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCONDPAGTO from NFCOMPRA where ID=" + idNotafiscal, "0")) > 0)
                    {
                        tbxParcelas.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select parcelas from CONDPAGTO where ID=" + clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCONDPAGTO from NFCOMPRA where ID=" + idNotafiscal, "0")));
                        //tbxParcelas.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "PARCELAS", "ID", clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDCONDPAGTO", "ID", idNotafiscal)));
                    }
                    else
                    {
                        tbxParcelas.Text = "";
                    }
                    tbxObserva.Text = drRecebida["OBSERVA"].ToString();

                    idFormapagto = clsParser.Int32Parse(drRecebida["IDFORMAPAGTO"].ToString());
                    if (idFormapagto == 0)
                        idFormapagto = clsInfo.zformapagto;

                    tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTIPOTITULO where ID=" + idFormapagto, "").Trim();
                    // tbxFormaPagto.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "CODIGO", "ID", idFormapagto).Trim();
                    tbxFormaPagto.Text = tbxFormaPagto.Text + "-" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTIPOTITULO where ID=" + idFormapagto, "").Trim();
                    //tbxFormaPagto.Text = tbxFormaPagto.Text + "-" + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "NOME", "ID", idFormapagto).Trim();

                    idBanco = clsParser.Int32Parse(drRecebida["IDBANCO"].ToString());
                    if (idBanco == 0)
                        idBanco = clsInfo.zbanco;

                    tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_BANCOS where ID=" + idBanco, "").Trim();
                    //tbxBancoConta.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "CODIGO", "ID", idBanco).Trim();
                    tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from TAB_BANCOS where ID=" + idBanco, "").Trim();
                    //tbxBancoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "COGNOME", "ID", idBanco).Trim();

                    idBancoint = clsParser.Int32Parse(drRecebida["IDBANCOINT"].ToString()); ;
                    if (idBancoint == 0)
                        idBancoint = clsInfo.zbancoint;

                    tbxBancoInt.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select conta from BANCOS where ID=" + idBancoint, "0").Trim();
                    // tbxBancoInt.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "CONTA", "ID", idBancoint).Trim();
                    tbxBancoNomeInterno.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from BANCOS where ID=" + idBancoint, "").Trim();
                    //tbxBancoNomeInterno.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "NOME", "ID", idBancoint).Trim();
                    if (drRecebida["CHEGOU"].ToString() == "S")
                    {
                        ckxChegou.Checked = true;
                        chegou = "S";
                        ckxChegou.Text = "Sim - Chegou a cobrança";
                    }
                    else
                    {
                        ckxChegou.Checked = false;
                        chegou = "N";
                        ckxChegou.Text = "Não - Chegou a cobrança";
                    }

                    if (drRecebida["DESPESAPUBLICA"].ToString() == "S")
                    {
                        ckxDespesaPublica.Checked = true;
                        despesapublica = "S";
                        ckxDespesaPublica.Text = "Sim - é Despesa Publica";
                    }
                    else
                    {
                        ckxDespesaPublica.Checked = false;
                        despesapublica = "N";
                        ckxDespesaPublica.Text = "Não - é Despesa Publica";
                    }


                    tbxBoletoNro.Text = clsParser.Int32Parse(drRecebida["BOLETONRO"].ToString()).ToString();
                    tbxDv.Text = drRecebida["DV"].ToString();
                    tbxBaixa.Text = drRecebida["BAIXA"].ToString();

                    idSitBanco = clsParser.Int32Parse(drRecebida["IDSITBANCO"].ToString());
                    if (idSitBanco == 0)
                        idSitBanco = clsInfo.zsituacaotitulo;

                    tbxSitBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTITULO where ID=" + idSitBanco, "").Trim();
                    //tbxSitBanco.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "CODIGO", "ID", idSitBanco).Trim();
                    tbxSituacaoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTITULO where ID=" + idSitBanco, "").Trim();
                    //tbxSituacaoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "NOME", "ID", idSitBanco).Trim();

                    tbxBoleto.Text = drRecebida["BOLETO"].ToString();

                    if (drRecebida["VENCIMENTO"].ToString() != "")
                    {
                        tbxVencimento.Text = clsParser.SqlDateTimeParse(drRecebida["VENCIMENTO"].ToString()).Value.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        tbxVencimento.Text = "";
                    }

                    if (drRecebida["ATEVENCIMENTO"].ToString() == "S")
                    {
                        ckxAteVencimento.Checked = true;
                        atevencimento = "S";
                        ckxAteVencimento.Text = "Desconto mesmo após Vencimento";
                    }
                    else
                    {
                        ckxAteVencimento.Checked = false;
                        atevencimento = "N";
                        ckxAteVencimento.Text = "Desconto Apenas até o Vencimento";

                    }


                    tbxValor.Text = clsParser.DecimalParse(drRecebida["VALOR"].ToString()).ToString("N2");
                    tbxValorDesconto.Text = clsParser.DecimalParse(drRecebida["VALORDESCONTO"].ToString()).ToString("N2");

                    tbxValorLiquido.Text = clsParser.DecimalParse(drRecebida["VALORLIQUIDO"].ToString()).ToString("N2");
                    tbxValorJuros.Text = clsParser.DecimalParse(drRecebida["VALORJUROS"].ToString()).ToString("N2");
                    //                    tbxValorAtraso.Text = clsParser.DecimalParse(drRecebida["VALORATRASO"].ToString()).ToString("N2");
                    tbxValorMulta.Text = clsParser.DecimalParse(drRecebida["VALORMULTA"].ToString()).ToString("N2");
                    //                    tbxMulta.Text = clsParser.DecimalParse(drRecebida["MULTA"].ToString()).ToString("N2");


                    idcoordenador = clsParser.Int32Parse(drRecebida["IDCOORDENADOR"].ToString());
                    if (idcoordenador == 0) { idcoordenador = clsInfo.zempresaclienteid; }
                    tbxCoordenador_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idcoordenador);
                    tbxValorComissao.Text = clsParser.DecimalParse(drRecebida["VALORCOMISSAO"].ToString()).ToString("N2");

                    idvendedor = clsParser.Int32Parse(drRecebida["idvendedor"].ToString());
                    if (idvendedor == 0) { idvendedor = clsInfo.zempresaclienteid; }
                    tbxVendedor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idvendedor);
                    tbxValorComissaoGer.Text = clsParser.DecimalParse(drRecebida["VALORCOMISSAOGER"].ToString()).ToString("N2");

                    idsupervisor = clsParser.Int32Parse(drRecebida["idsupervisor"].ToString());
                    if (idsupervisor == 0) { idsupervisor = clsInfo.zempresaclienteid; }
                    tbxSupervisor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idsupervisor);
                    tbxValorComissaoSup.Text = clsParser.DecimalParse(drRecebida["VALORCOMISSAOSUP"].ToString()).ToString("N2");

                    tbxMulta.Text = "0";
                    tbxDesconto.Text = "0";
                    tbxSaldo.Text = "0";
                    tbxRecebido.Text = "0";
                    tbxDevolucao.Text = "0";
                    tbxDiferenca.Text = "0";
                    tbxPagando.Text = "0";

                    // Se a Data da Baixa for superior a 3 dias enviar uma mensagem
                    // dataBaixa = DateTime.Parse(tbxDataBaixa.Text);
                    dataVencimento = DateTime.Parse(tbxVencimento.Text);
                    /*
                    dias = dataBaixa.Subtract(dataVencimento).Days;
                    if (dias > 0)
                    {
                        MessageBox.Show("Data da Baixa com " + dias + " dias de diferença." + Environment.NewLine + "Diferença entre a Data de Vencimento e a Data da Baixa ");
                        tbxDataBaixa.Select();
                        tbxDataBaixa.SelectAll();
                    }
                    */
                }
            }
            else
            { // Incluindo
//                DataRow drRecebida;
//                drRecebida = clsSelectInsertUpdateBLL.SelectCollectionFields(clsInfo.conexaosqldados, "*", "PAGAR", "ID = " + clsParser.Int32Parse(idRecebida.ToString()), "ID");

                idReceberNFV = 0;
                tbxFilial.Text = clsInfo.zfilial.ToString();
                tbxDuplicata.Text = "0";
                tbxPosicao.Text = "1";
                tbxPosicaoFim.Text = "1";
                tbxEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                idDocumento = clsInfo.zdocumento;
                tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from DOCFISCAL where ID=" + idDocumento, "");
                // tbxDocumento.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", idDocumento);
                rbnSetorNao.Checked = true;
                setor = "N";
                idCliente = clsInfo.zempresaclienteid;
                tbxClienteCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from CLIENTE where ID=" + idCliente, "").Trim();
                //tbxClienteCognome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idCliente).Trim();
                tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from CLIENTE where ID=" + idCliente, "").Trim();
                // tbxClienteCGC.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "CGC", "ID", idCliente).Trim();
                tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from CLIENTE where ID=" + idCliente, "").Trim() +
                            " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from CLIENTE where ID=" + idCliente, "");
                //tbxClienteTelefone.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "DDD", "ID", idCliente).Trim() +
                            //" - " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "TELEFONE", "ID", idCliente);
                tbxDataLanca.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tbxEmitente.Text = clsInfo.zusuario;
                idHistorico = clsInfo.zhistoricos;
                tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from HISTORICOS where ID=" + idHistorico, "0").Trim();
                // tbxHistorico.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idHistorico).Trim();
                tbxHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from HISTORICOS where ID=" + idHistorico, "").Trim();
                //tbxHistoricoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", idHistorico).Trim();
                idCentrocusto = clsInfo.zcentrocustos;
                tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID=" + idCentrocusto, "").Trim();
                //tbxCentroCusto.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", idCentrocusto).Trim();
                tbxCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from CENTROCUSTOS where ID=" + idCentrocusto).Trim();
                //tbxCentroCustoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", idCentrocusto).Trim();
                idContacontabil = clsInfo.zcontacontabil;
                tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from CONTACONTABIL where ID=" + idContacontabil, "").Trim();
                //tbxContaContabil.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", idContacontabil).Trim();
                tbxContaContabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from CONTACONTABIL where ID=" + idContacontabil, "").Trim();
                //tbxContaContabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", idContacontabil).Trim();
                idNotafiscal = clsInfo.znfcompra;
                tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from NFCOMPRA where ID=" + idNotafiscal, "0");
                //tbxNFVendaNumero.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "NUMERO", "ID", idNotafiscal);
                tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TOTALNOTAFISCAL from NFCOMPRA where ID=" + idNotafiscal, "0").ToString();
                //tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "TOTALNOTAFISCAL", "ID", idNotafiscal).ToString();
                
                //if (clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDDOCUMENTO", "ID", idNotafiscal)) > 0)
                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from NFCOMPRA where ID=" + idNotafiscal, "0")) > 0)
                {
                    idDocNFV = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from NFCOMPRA where ID=" + idNotafiscal, "0"));
                    //idDocNFV = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDDOCUMENTO", "ID", idNotafiscal))));
                    
                    if (idDocNFV == 0)
                        idDocNFV = clsInfo.zdocumento;

                    tbxNotaDoc.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from DOCFISCAL where ID=" + idDocNFV, "").Trim();
                    //tbxNotaDoc.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", idDocNFV))).Trim();
                }
                else
                {
                    tbxNotaDoc.Text = "";
                }

                //if (clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDCONDPAGTO", "ID", idNotafiscal)) > 0)
                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCONDPAGTO from NFCOMPRA where ID=" + idNotafiscal, "0")) > 0)
                {
                    tbxParcelas.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select parcelas from CONDPAGTO where ID=" + clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCONDPAGTO from NFCOMPRA where ID=" + idNotafiscal, "0")));
                    //tbxParcelas.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONDPAGTO", "PARCELAS", "ID", clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDCONDPAGTO", "ID", idNotafiscal)));
                }
                else
                {
                    tbxParcelas.Text = "";
                }
                tbxObserva.Text = "";

                idFormapagto = clsInfo.zformapagto;
                tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTIPOTITULO where ID=" + idFormapagto, "").Trim();
                //  tbxFormaPagto.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "CODIGO", "ID", idFormapagto).Trim();
                
                tbxFormaPagto.Text = tbxFormaPagto.Text + "-" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTIPOTITULO where ID=" + idFormapagto, "").Trim();
                // tbxFormaPagto.Text = tbxFormaPagto.Text + "-" + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "NOME", "ID", idFormapagto).Trim();

                idBanco = clsInfo.zbanco;
                tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_BANCOS where ID=" + idBanco, "").Trim();
                //tbxBancoConta.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "CODIGO", "ID", idBanco).Trim();
                tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from TAB_BANCOS where ID=" + idBanco, "").Trim();
                // tbxBancoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "COGNOME", "ID", idBanco).Trim();

                idBancoint = clsInfo.zbancoint;
                tbxBancoInt.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select conta from BANCOS where ID=" + idBancoint, "").Trim();
                // tbxBancoInt.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "CONTA", "ID", idBancoint).Trim();
                
                tbxBancoNomeInterno.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from BANCOS where ID=" + idBancoint, "").Trim();
                //tbxBancoNomeInterno.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "NOME", "ID", idBancoint).Trim();
                ckxChegou.Checked = false;
                chegou = "N";
                ckxChegou.Text = "Não - Chegou a cobrança";
                ckxDespesaPublica.Checked = false;
                despesapublica = "N";
                ckxDespesaPublica.Text = "Não - é Despesa Publica";


                tbxBoletoNro.Text = "0".ToString();
                tbxDv.Text = "";
                tbxBaixa.Text = "";
                idSitBanco = clsInfo.zsituacaotitulo;
                tbxSitBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTITULO ID=" + idSitBanco, "").Trim();
                // tbxSitBanco.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "CODIGO", "ID", idSitBanco).Trim();
                tbxSituacaoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTITULO where ID=" + idSitBanco, "").Trim();
                //tbxSituacaoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "NOME", "ID", idSitBanco).Trim();
               
                tbxBoleto.Text = "";
                tbxVencimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ckxAteVencimento.Checked = false;
                atevencimento = "N";
                ckxAteVencimento.Text = "Desconto Apenas até o Vencimento";
                tbxValor.Text = "0";
                tbxValorDesconto.Text = "0";
                tbxValorLiquido.Text = "0";
                tbxValorJuros.Text = "0";
                tbxValorMulta.Text = "0";
                tbxMulta.Text = "0";
                tbxDesconto.Text = "0";
                tbxSaldo.Text = "0";
                tbxRecebido.Text = "0";
                tbxDevolucao.Text = "0";
                tbxDiferenca.Text = "0";
                tbxPagando.Text = "0";

                
                idcoordenador = clsInfo.zempresaclienteid; 
                tbxCoordenador_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idcoordenador);
                tbxValorComissao.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");

                idvendedor =  clsInfo.zempresaclienteid; 
                tbxVendedor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idvendedor);
                tbxValorComissaoGer.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");

                idsupervisor = clsInfo.zempresaclienteid; 
                tbxSupervisor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idsupervisor);
                tbxValorComissaoSup.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");

            }
        }
        private String[] PreencheValoresRecebida()
        {
            String[] _valores = { 
                    tbxFilial.Text,
                    tbxDuplicata.Text,
                    tbxPosicao.Text,
                    tbxPosicaoFim.Text,
                    tbxEmissao.Text,
                    idDocumento.ToString(),
                    tbxDocumento.Text,
                    idCliente.ToString(),
                    tbxClienteCognome.Text,
                    tbxClienteCGC.Text,
                    tbxClienteTelefone.Text,
                    tbxDataLanca.Text,
                    tbxEmitente.Text,
                    idHistorico.ToString(),
                    tbxHistorico.Text,
                    tbxHistoricoNome.Text,
                    idCentrocusto.ToString(),
                    tbxCentroCusto.Text,
                    tbxCentroCustoNome.Text,
                    idContacontabil.ToString(),
                    tbxContaContabil.Text,
                    tbxContaContabilNome.Text,
                    idNotafiscal.ToString(),
                    tbxNFVendaNumero.Text,
                    tbxNFVendaTotalNotaFiscal.Text,
                    tbxNotaDoc.Text,
                    tbxParcelas.Text,
                    tbxObserva.Text,
                    idFormapagto.ToString(),
                    tbxFormaPagto.Text,
                    tbxFormaPagto.Text,
                    idBanco.ToString(),
                    tbxBancoConta.Text,
                    tbxBancoNome.Text,
                    idBancoint.ToString(),
                    tbxBancoInt.Text,
                    tbxBancoNomeInterno.Text,
                    tbxBoletoNro.Text,
                    tbxDv.Text,
                    tbxBaixa.Text,
                    idSitBanco.ToString(),
                    tbxSitBanco.Text,
                    tbxSituacaoNome.Text,
                    tbxBoleto.Text,
                    tbxVencimento.Text,
                    tbxValor.Text,
                    tbxValorDesconto.Text,
                    tbxValorLiquido.Text,
                    tbxValorJuros.Text,
                    tbxValorMulta.Text};
            return _valores;
        }

        private void ckxAteVencimento_Click(object sender, EventArgs e)
        {
            if (ckxAteVencimento.Checked == true)
            {
                atevencimento = "S";
                ckxAteVencimento.Text = "Desconto mesmo após Vencimento";
            }
            else
            {
                atevencimento = "N";
                ckxAteVencimento.Text = "Desconto Apenas até o Vencimento";
            }
            calcular();
        }

        private void ckxChegou_Click(object sender, EventArgs e)
        {
            if (ckxChegou.Checked == true)
            {
                chegou = "S";
                ckxChegou.Text = "Sim - Já Chegou Boleto";
            }
            else
            {
                chegou = "N";
                ckxChegou.Text = "Não - Chegou Boleto";
            }
        }
        private void ckxDespesaPublica_Click(object sender, EventArgs e)
        {

            if (ckxDespesaPublica.Checked == true)
            {
                despesapublica = "S";
                ckxDespesaPublica.Text = "Sim - Despesa Publica";
            }
            else
            {
                despesapublica = "N";
                ckxDespesaPublica.Text = "Não é Despesa Publica";
            }
        }


        private void Lupa()
        {
            if (clsInfo.zrow != null && clsInfo.zrow.Index != -1)
            {
                if (clsInfo.znomegrid == "dgvDocFiscal")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idDocumento = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxDocumento.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxDocumento.Select();
                    }
                }
                //
                if (clsInfo.znomegrid == "dgvCliente")
                {
                    idCliente = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxClienteCognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
                    tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from CLIENTE where ID=" + idCliente, "");
                    //tbxClienteCGC.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "CGC", "ID", idCliente);
                   
                    tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from CLIENTE where ID=" + idCliente, "") +
                          " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from CLIENTE where ID=" + idCliente, "");
                    // tbxClienteTelefone.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "DDD", "ID", idCliente) +
                    //    " - " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "TELEFONE", "ID", idCliente);

                    clsInfo.znomegrid = "";
                    tbxClienteCognome.Select();
                    tbxClienteCognome.SelectAll();
                }
                //
                if (clsInfo.znomegrid == "dgvHistoricos")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idHistorico = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxHistorico.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxHistoricoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxHistorico.Select();
                    tbxHistorico.SelectAll();

                }
                if (clsInfo.znomegrid == "dgvCentroCusto")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idCentrocusto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCentroCusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCentroCustoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxCentroCusto.Select();
                    tbxCentroCusto.SelectAll();

                }

                if (clsInfo.znomegrid == "dgvContacontabil")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idContacontabil = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxContaContabil.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxContaContabilNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxContaContabil.Select();
                        tbxContaContabil.SelectAll();
                    }
                }
                if (clsInfo.znomegrid == "dgvNFCompra")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idNotafiscal = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from NFCOMPRA where ID=" + idNotafiscal, "0");
                        //tbxNFVendaNumero.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "NUMERO", "ID", idNotafiscal);
                        tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TOTALNOTAFISCAL from NFCOMPRA where ID=" + idNotafiscal, "0").ToString();
                        //tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "TOTALNOTAFISCAL", "ID", idNotafiscal).ToString();
                        
                        //if (clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDDOCUMENTO", "ID", idNotafiscal)) > 0)
                         if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from NFCOMPRA where ID=" + idNotafiscal, "0")) > 0)
                        {
                            idDocNFV = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDDOCUMENTO from NFCOMPRA where ID=" + idNotafiscal, "0"));
                            //idDocNFV = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "NFCOMPRA", "IDDOCUMENTO", "ID", idNotafiscal))));
                           
                             if (idDocNFV == 0)
                                idDocNFV = clsInfo.zdocumento;

                            tbxNotaDoc.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from DOCFISCAL where ID=" + idDocNFV, "");
                            //tbxNotaDoc.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "DOCFISCAL", "COGNOME", "ID", idDocNFV)));
                        }
                        else
                        {
                            tbxNotaDoc.Text = "";
                        }
                        clsInfo.znomegrid = "";
                    }
                }
                if (clsInfo.znomegrid == "dgvFormaPagto")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxFormaPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim() + " = " + clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxFormaPagto.Select();
                        tbxFormaPagto.SelectAll();
                    }
                }
                if (clsInfo.znomegrid == "dgvBanco")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxBancoConta.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxBancoNome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxBancoConta.Select();
                        tbxBancoConta.SelectAll();
                    }
                }
                //
                if (clsInfo.znomegrid == "dgvBancoInt")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxBancoInt.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                        tbxBancoNomeInterno.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxBancoInt.Select();
                        tbxBancoNomeInterno.SelectAll();
                    }
                }

                if (clsInfo.znomegrid == "dgvSitBanco")
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
                //COBRANCACOD (PAGAR01)
                if (clsInfo.znomegrid == "dgvCobrancaCod")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        recebida01_idcobrancacod = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01CobrancaCodCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01CobrancaCodNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxPagar01CobrancaCodCodigo.Select();
                    tbxPagar01CobrancaCodCodigo.SelectAll();
                }
                if (clsInfo.znomegrid == "dgvCobrancaCodHis")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        recebida01_idcobrancahis = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01CobrancaCod1Codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01CobrancaCod1Nome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxPagar01CobrancaCod1Codigo.Select();
                    tbxPagar01CobrancaCod1Codigo.SelectAll();

                }
                if (clsInfo.znomegrid == "dgvCobrancaHistorico")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        recebida01_idhistorico = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01HistCod.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01HistNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxPagar01HistCod.Select();
                    tbxPagar01HistCod.SelectAll();

                }
                if (clsInfo.znomegrid == "dgvCobrancaCentroCusto")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        recebida01_idcentrocusto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01CCCod.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01CCNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                    tbxPagar01CCCod.Select();
                    tbxPagar01CCCod.SelectAll();

                }
                if (clsInfo.znomegrid == "dgvCobrancaContaContabil")
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        recebida01_idcodigoctabil = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxPagar01CtabilCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxPagar01CtabilNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxPagar01CtabilCodigo.Select();
                        tbxPagar01CtabilCodigo.SelectAll();
                    }
                }


            }
        }
        private void btnIdDocumento_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvDocFiscal";
            frmDocFiscalVis frmDocFiscalVis = new frmDocFiscalVis();
            frmDocFiscalVis.Init(idDocumento);

            clsFormHelper.AbrirForm(this.MdiParent, frmDocFiscalVis, clsInfo.conexaosqldados);
        }
        private void btnidCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCliente";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idCliente);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }
        private void btnIdHistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvHistoricos";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", idHistorico, "Historicos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void btnIdCentroCusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCentroCusto";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", idCentrocusto, "Centro de Custos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void btnIdCodigoCtaBil_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvContaContabil";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONTACONTABIL", idContacontabil, "Conta Contabil");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdFormaPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvFormaPagto";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", idFormapagto, "Situacao Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdNotaFiscal_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvNFCompra";
            //frmNFCompraPes frmNFCompraVis = new frmNFCompraPes(idNotafiscal);
            //FormHelper.AbrirForm(this.MdiParent, frmNFCompraVis);
        }


        private void btnIdBanco_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvBanco";
            frmTab_BancosPes frmTab_BancosPes = new frmTab_BancosPes();
            frmTab_BancosPes.Init(clsInfo.conexaosqldados, idBanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmTab_BancosPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdBancoInt_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvBancoInt";
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBancoint, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdSitBanco_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvSitBanco";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTITULO", idSitBanco, "Situacao Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void calcular()
        { //Colocamos numa função financeiro
            DuplicataInfo DuplicataInfo;
            DuplicataInfo = clsFinanceiro.CalcularDuplicata("Receber", tbxVencimento.Text, tbxDataBaixa.Text, 
                                                                    clsParser.DecimalParse(tbxValor.Text),  +
                                                                    clsParser.DecimalParse(tbxValorDesconto.Text), +
                                                                    clsParser.DecimalParse(tbxValorJuros.Text), + 
                                                                    clsParser.DecimalParse(tbxValorMulta.Text), 
                                                                    0, atevencimento);

            tbxValorLiquido.Text = DuplicataInfo.valorliquido.ToString("N2");
            tbxMulta.Text = DuplicataInfo.multas.ToString("N2");
            tbxJuros.Text = DuplicataInfo.juros.ToString("N2");
            tbxDesconto.Text = DuplicataInfo.descontos.ToString("N2");
            tbxSaldo.Text = ((clsParser.DecimalParse(tbxValor.Text) + clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text)) - clsParser.DecimalParse(tbxDesconto.Text)).ToString("N2");
            totaldias = DuplicataInfo.ntotaldias;
            tbxDiferenca.Text = ((clsParser.DecimalParse(tbxSaldo.Text) - ( clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");

            //Pagar01Grid();

        }
        /*
        private void CalcularPagando()
        {
            //calcular o total que esta pagando apos ter excluido 1 linha

            tbxPagando.Text = "0";

            for (int x = 0; x < dtRecebida01.Rows.Count; x++)
            {
                if (dtRecebida01.Rows[x].RowState != DataRowState.Deleted &&
                    dtRecebida01.Rows[x].RowState != DataRowState.Detached)
                {
                    if (dtRecebida01.Rows[x]["DEBCRED"].ToString() == "D")
                    {
                        tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtRecebida01.Rows[x]["VALOR"].ToString())).ToString("N2");
                    }
                    else
                    {
                        tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtRecebida01.Rows[x]["VALOR"].ToString())).ToString("N2");
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
                    for (int x = 0; x < dtRecebida01.Rows.Count; x++)
                    {
                        if (dtRecebida01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) == "00")
                        {
                            ValorPagtoParcial = ValorPagtoParcial - ValorDescontoPago;
                            dtRecebida01.Select("Posicao=" + dtRecebida01.Rows[x]["POSICAO"].ToString())[0]["VALOR"] = ValorPagtoParcial;
                        }
                    }
                    // somar novamente para não haver problemas com o saldo
                    tbxPagando.Text = "0";

                    for (int x = 0; x < dtRecebida01.Rows.Count; x++)
                    {
                        if (dtRecebida01.Rows[x].RowState != DataRowState.Deleted &&
                            dtRecebida01.Rows[x].RowState != DataRowState.Detached)
                        {
                            if (dtRecebida01.Rows[x]["DEBCRED"].ToString() == "D")
                            {
                                tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtRecebida01.Rows[x]["VALOR"].ToString())).ToString("N2");
                            }
                            else
                            {
                                tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtRecebida01.Rows[x]["VALOR"].ToString())).ToString("N2");
                            }
                        }
                    }
                    tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");
                    tbxSaldo.Text = ((clsParser.DecimalParse(tbxValor.Text) + clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text)) - clsParser.DecimalParse(tbxDesconto.Text)).ToString("N2");
                    tbxDiferenca.Text = ((clsParser.DecimalParse(tbxSaldo.Text) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");

                }
                
            }

        }
         */
        private Boolean VerificaGravacaoDuplicata(String _tipo)
        {
            try
            {
                // Verficando se a referencia esta em branco
                if (_tipo == "CODIGO" || _tipo == "TODOS")
                {
                    if (idDocumento == 0)
                    {
                        throw new Exception("Informe o Tipo de Documento que é esta Duplicata");
                    }
                    if (idCliente == 0)
                    {
                        throw new Exception("Informe o Fornecedor desta Duplicata");
                    }
                    if (idHistorico == 0)
                    {
                        throw new Exception("Informe o Historico do Banco desta Duplicata");
                    }
                    if (idContacontabil == 0)
                    {
                        throw new Exception("Informe a Conta Contabil desta Duplicata");
                    }
                    if (idCentrocusto == 0)
                    {
                        throw new Exception("Informe o Centro de Custo desta Duplicata");
                    }
                    if (idNotafiscal == 0)
                    {
                        throw new Exception("Informe o Numero da Nota Fiscal a que se refere esta Duplicata");
                    }
                    if (idFormapagto == 0)
                    {
                        throw new Exception("Informe como será a Forma de Pagamento desta Duplicata");
                    }
                    if (idBanco == 0)
                    {
                        throw new Exception("Informe o numero do banco em que foi colocado esta Duplicata");
                    }
                    if (idBancoint == 0)
                    {
                        throw new Exception("Informe o banco que colocamos para efetuar o debito automatico desta Duplicata");
                    }
                    if (idSitBanco == 0)
                    {
                        throw new Exception("Informe a Situação do Banco para esta Duplicata");
                    }
                    if (idDocNFV  == 0)
                    {
                        throw new Exception("Informe o documento a que se refere a nota fiscal geradora desta Duplicata");
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = DialogResult.Cancel; // Retorna para o foco
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                switch (ex.Message)
                {
                    case "Informe o Tipo de Documento que é esta Duplicata":
                        tbxDocumento.Select(); // btnEstados_uf.Select();
                        tbxDocumento.SelectAll();
                        break;

                    case "Informe o Fornecedor desta Duplicata":
                        tbxClienteCognome.Select();
                        tbxClienteCognome.SelectAll();
                        break;

                    case "Informe o Historico do Banco desta Duplicata":
                        tbxHistorico.Select();
                        tbxHistorico.SelectAll();
                        break;

                    case "Informe a Conta Contabil desta Duplicata":
                        tbxContaContabil.Select();
                        tbxContaContabil.SelectAll();
                        break;

                    case "Informe o Centro de Custo desta Duplicata":
                        tbxCentroCusto.Select();
                        tbxCentroCusto.SelectAll();
                        break;

                    case "Informe o Numero da Nota Fiscal a que se refere esta Duplicata":
                        tbxNFVendaNumero.Select();
                        tbxNFVendaNumero.SelectAll();
                        break;

                    case "Informe como será a Forma de Pagamento desta Duplicata":
                        tbxFormaPagto.Select();
                        tbxFormaPagto.SelectAll();
                        break;

                    case "Informe o numero do banco em que foi colocado esta Duplicata":
                        tbxBancoConta.Select();
                        tbxBancoConta.SelectAll();
                        break;

                    case "Informe o banco que colocamos para efetuar o debito automatico desta Duplicata":
                        tbxBancoInt.Select();
                        tbxBancoInt.SelectAll();
                        break;

                    case "Informe a Situação do Banco para esta Duplicata":
                        tbxSitBanco.Select();
                        tbxSitBanco.SelectAll();
                        break;

                    case "Informe o documento a que se refere a nota fiscal geradora desta Duplicata":
                        tbxNotaDoc.Select();
                        tbxNotaDoc.SelectAll();
                        break;

                    default:
                        break;
                }
                return false;
            }
            return true;
        }
        private void SalvarDuplicata()
        {

            if (idRecebida == 0)    // Incluindo os valores do Cabeçalho -- valores locais
            {

                idRecebida = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "RECEBIDA", " " +
                    "FILIAL,DUPLICATA,POSICAO,POSICAOFIM,EMISSAO,IDDOCUMENTO,SETOR,IDCLIENTE,DATALANCA,EMITENTE, " +
                    "IDHISTORICO, IDCENTROCUSTO, IDCODIGOCTABIL, IDNOTAFISCAL, OBSERVA, IDFORMAPAGTO, IDBANCO, " +
                    "IDBANCOINT, CHEGOU, DESPESAPUBLICA, BOLETONRO, DV, BAIXA, IDSITBANCO, BOLETO, VENCIMENTO, VENCIMENTOPREV, " +
                    "ATEVENCIMENTO, VALOR, VALORDESCONTO, VALORLIQUIDO, VALORJUROS, VALORMULTA ",
                    clsParser.SqlInt32Format(tbxFilial.Text, false, "FILIAL") +
                    clsParser.SqlInt32Format(tbxDuplicata.Text, false, "DUPLICATA") +
                    clsParser.SqlInt32Format(tbxPosicao.Text, false, "POSICAO") +
                    clsParser.SqlInt32Format(tbxPosicaoFim.Text, false, "POSICAOFIM") +
                    clsParser.SqlDateTimeFormat(tbxEmissao.Text, false, "EMISSAO") +
                    clsParser.SqlInt32Format(idDocumento.ToString(), false, "IDDOCUMENTO") +
                    clsParser.SqlStringFormat(setor.ToString(), false, "SETOR") +
                    clsParser.SqlInt32Format(idCliente.ToString(), false, "IDCLIENTE") +
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
                    clsParser.SqlStringFormat(atevencimento.ToString(), false, "ATEVENCIMENTO") +
                    clsParser.SqlDecimalFormat(tbxValor.Text, false, "VALOR") +
                     clsParser.SqlDecimalFormat(tbxValorDesconto.Text, false, "VALORDESCONTO") +
                     clsParser.SqlDecimalFormat(tbxValorLiquido.Text, false, "VALORLIQUIDO") +
                     clsParser.SqlDecimalFormat(tbxValorJuros.Text, false, "VALORJUROS") +
                     clsParser.SqlDecimalFormat(tbxValorMulta.Text, true, "VALORMULTA"));
            }
            else
            {
                // Alterando os valores do Cabeçalho -- valores locais
                clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "RECEBIDA",
                    "FILIAL = " + clsParser.SqlInt32Format(tbxFilial.Text, false, "FILIAL") +
                    "DUPLICATA = " + clsParser.SqlInt32Format(tbxDuplicata.Text, false, "DUPLICATA") +
                    "POSICAO = " + clsParser.SqlInt32Format(tbxPosicao.Text, false, "POSICAO") +
                    "POSICAOFIM = " + clsParser.SqlInt32Format(tbxPosicaoFim.Text, false, "POSICAOFIM") +
                    "EMISSAO = " + clsParser.SqlDateTimeFormat(tbxEmissao.Text, false, "EMISSAO") +
                    "IDDOCUMENTO = " + clsParser.SqlInt32Format(idDocumento.ToString(), false, "IDDOCUMENTO") +
                    "SETOR = " + clsParser.SqlStringFormat(setor.ToString(), false, "SETOR") +
                    "IDCLIENTE = " + clsParser.SqlInt32Format(idCliente.ToString(), false, "IDCLIENTE") +
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
                    "ATEVENCIMENTO = " + clsParser.SqlStringFormat(atevencimento.ToString(), false, "ATEVENCIMENTO") +
                    "VALOR = " + clsParser.SqlDecimalFormat(tbxValor.Text, false, "VALOR") +
                    "VALORDESCONTO = " + clsParser.SqlDecimalFormat(tbxValorDesconto.Text, false, "VALORDESCONTO") +
                    "VALORLIQUIDO = " + clsParser.SqlDecimalFormat(tbxValorLiquido.Text, false, "VALORLIQUIDO") +
                    "VALORJUROS = " + clsParser.SqlDecimalFormat(tbxValorJuros.Text, false, "VALORJUROS") +
                    "VALORMULTA = " + clsParser.SqlDecimalFormat(tbxValorMulta.Text, true, "VALORMULTA"),
                    "ID = " + idRecebida.ToString());
            }
        }
        private void tspCobIncluir_Click(object sender, EventArgs e)
        {

        }

        private void tspCobAlterar_Click(object sender, EventArgs e)
        {
            DataRow row;
            if (dgvRecebida01.CurrentRow != null)
            {
                idRecebida01 = clsParser.Int32Parse(dgvRecebida01.CurrentRow.Cells["ID"].Value.ToString());

                row = clsSelectInsertUpdateBLL.SelectCollectionFields(clsInfo.conexaosqldados, "*", "Recebida01", "ID = " + clsParser.Int32Parse(idRecebida01.ToString()), "ID");

                tclPagar.SelectedTab = tabRegistro;
                gbxPagar.Enabled = false;
                gbxPagarBaixa.Visible = true;

                tbxDuplicata3.Text = tbxDuplicata1.Text = tbxDuplicata.Text;
                tbxEmissao3.Text = tbxEmissao1.Text = tbxEmissao.Text;
                tbxPosicao3.Text = tbxPosicao1.Text = tbxPosicao.Text;
                tbxPosicaoFim3.Text = tbxPosicaoFim1.Text = tbxPosicaoFim.Text;
                tbxClienteCognome3.Text = tbxClienteCognome1.Text = tbxClienteCognome.Text;
                tbxClienteTelefone3.Text = tbxClienteTelefone1.Text = tbxClienteTelefone.Text;
                tbxClienteCGC3.Text = tbxClienteCGC1.Text = tbxClienteCGC.Text;

                // colocar os campos do grid selecionado / indicado
                recebida01_idcobrancacod = clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString());
                recebida01_idcobrancahis = clsParser.Int32Parse(row["IDCOBRANCAHIS"].ToString());
                recebida01_idhistorico = clsParser.Int32Parse(row["IDHISTORICO"].ToString());
                recebida01_idcentrocusto = clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString());
                recebida01_idcodigoctabil = clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString());

                tbxPagar01CobrancaCodCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOCOBRANCACOD where ID=" + recebida01_idcobrancacod, "");
                //tbxPagar01CobrancaCodCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", recebida01_idcobrancacod);

                tbxPagar01CobrancaCodNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOCOBRANCACOD where ID=" + recebida01_idcobrancacod, "");
                //tbxPagar01CobrancaCodNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", recebida01_idcobrancacod);


                tbxPagar01CobrancaCod1Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOCOBRANCACOD where ID=" + recebida01_idcobrancahis, "");
                //tbxPagar01CobrancaCod1Codigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", recebida01_idcobrancahis);

                tbxPagar01CobrancaCod1Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOCOBRANCACOD where ID=" + recebida01_idcobrancahis, "");
                //tbxPagar01CobrancaCod1Nome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", recebida01_idcobrancahis);

                tbxPagar01Valor.Text = clsParser.DecimalParse(row["VALOR"].ToString()).ToString("N2");
                tbxPagar01DC.Text = row["DEBCRED"].ToString();
                
                tbxPagar01HistCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where ID=" + recebida01_idhistorico, "");
                //tbxPagar01HistCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", recebida01_idhistorico);
              
                tbxPagar01HistNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from HISTORICOS where ID=" + recebida01_idhistorico, "");
                // tbxPagar01HistNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", recebida01_idhistorico);

                tbxPagar01CCCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID=" + recebida01_idcentrocusto, "");
                //tbxPagar01CCCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", recebida01_idcentrocusto);
              
                tbxPagar01CCNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from CENTROCUSTOS where ID=" + recebida01_idcentrocusto, "");
                //tbxPagar01CCNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", recebida01_idcentrocusto);

                tbxPagar01CtabilCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from CONTACONTABIL where ID=" + recebida01_idcodigoctabil, "");
                //tbxPagar01CtabilCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", recebida01_idcodigoctabil);
               
                tbxPagar01CtabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from CONTACONTABIL where ID=" + recebida01_idcodigoctabil, "");
                //tbxPagar01CtabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", recebida01_idcodigoctabil);

                tbxPagar01Comissao.Text = clsParser.DecimalParse(row["VALORCOMISSAO"].ToString()).ToString("N2");
                tbxPagar01ComissaoGer.Text = clsParser.DecimalParse(row["VALORCOMISSAOGER"].ToString()).ToString("N2");
                tbxPagar01ComissaoSup.Text = clsParser.DecimalParse(row["VALORCOMISSAOSUP"].ToString()).ToString("N2");

                tbxPagar01CobrancaCodCodigo.Select();
                tbxPagar01CobrancaCodCodigo.SelectAll();


            }
            else
            {
                MessageBox.Show("Primeiro selecione um Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvRecebida01.Select();
            }

        }

        private void tspCobExcluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desativado neste formulario !!");
        }

        private void tspCobBaixar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desativado neste formulario !!");
        }

        private void tspSalvarObserva_Click(object sender, EventArgs e)
        {
            /*
            DataRow row = dtRecebidaObserva.Select("POSICAO=" + posicaoRecebidaObserva)[0];
            row["IDDUPLICATA"] = idRecebida;
            row["DATA"] = clsParser.SqlDateTimeParse(tbxObservaData.Text).Value;
            row["EMITENTE"] = tbxEmitente.Text;
            row["OBSERVAR"] = tbxObservaObservar.Text;
            */
            tabPagar.Select();
            gbxObserva.Visible = false;
            gbxPagar.Enabled = true;
            tclPagar.SelectedTab = tabPagar;


        }

        private void tspRetornarObserva_Click(object sender, EventArgs e)
        {
            tabPagar.Select();
            gbxObserva.Visible = false;
            gbxPagar.Enabled = true;
            tclPagar.SelectedTab = tabPagar;
        }

        private void tspSalvarrecebida01_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado neste Modulo");
            /*
            DataRow row;
            try
            {
                row = dtRecebida01.Select("posicao=" + posicao)[0];
            }
            catch
            {
                MessageBox.Show("Posição da tabela não foi encontrada.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            row["IDCOBRANCACOD"] = recebida01_idcobrancacod;
            row["IDCOBRANCAHIS"] = recebida01_idcobrancahis;
            row["IDHISTORICO"] = recebida01_idhistorico;
            row["IDCENTROCUSTO"] = recebida01_idcentrocusto;
            row["IDCODIGOCTABIL"] = recebida01_idcodigoctabil;
            row["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString())).Trim();
            row["CODCOBRANCA"] = row["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString())).Trim();
            row["CODHISTORICO"] = tbxPagar01CobrancaCod1Codigo.Text;
            row["HISTORICO"] = tbxPagar01HistCod.Text;
            row["CENTROCUSTO"] = tbxPagar01CCCod.Text;
            row["CONTACONTABIL"] = tbxContaContabil.Text;
            row["VALOR"] = clsParser.DecimalParse(tbxPagar01Valor.Text);
            row["DEBCRED"] = tbxPagar01DC.Text;
            row["VALORCOMISSAO"] = clsParser.DecimalParse(tbxPagar01Comissao.Text);
            row["VALORCOMISSAOGER"] = clsParser.DecimalParse(tbxPagar01ComissaoGer.Text);
            row["VALORCOMISSAOSUP"] = clsParser.DecimalParse(tbxPagar01ComissaoSup.Text);

            dtRecebida01.AcceptChanges();

            //calcular o total que esta pagando apos ter excluido 1 linha

            tbxPagando.Text = "0";

            for (int x = 0; x < dtRecebida01.Rows.Count; x++)
            {
                if (dtRecebida01.Rows[x].RowState != DataRowState.Deleted &&
                    dtRecebida01.Rows[x].RowState != DataRowState.Detached)
                {
                    if (dtRecebida01.Rows[x]["DEBCRED"].ToString() == "D")
                    {
                        tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtRecebida01.Rows[x]["VALOR"].ToString())).ToString("N2");
                    }
                    else
                    {
                        tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtRecebida01.Rows[x]["VALOR"].ToString())).ToString("N2");
                    }
                }
            }
            tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");


            */
            tabPagar.Select();
            gbxPagarBaixa.Visible = false;
            gbxPagar.Enabled = true;
            tclPagar.SelectedTab = tabPagar;
        }

        private void tspPrimeirorecebida01_Click(object sender, EventArgs e)
        {

        }

        private void tspAnteriorrecebida01_Click(object sender, EventArgs e)
        {

        }

        private void tspProximorecebida01_Click(object sender, EventArgs e)
        {

        }

        private void tspUltimorecebida01_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornarrecebida01_Click(object sender, EventArgs e)
        {
            tabPagar.Select();
            gbxPagarBaixa.Visible = false;
            gbxPagar.Enabled = true;
            tclPagar.SelectedTab = tabPagar;
        }

        private void dgvrecebida01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspCobAlterar.PerformClick();
        }

        private void btnrecebida01_idcobrancacod_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCobrancaCod";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", recebida01_idcobrancacod, "Situação Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnrecebida01_idcobrancahis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCobrancaCodHis";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD1", recebida01_idcobrancahis, "Situacao Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnrecebida01_idhistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCobrancaHistorico";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", recebida01_idhistorico,"Historicos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void btnrecebida01_idcentrocusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCobrancaCentroCusto";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", recebida01_idcentrocusto, "Centro de Custos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void btnrecebida01_idcodigoctabil_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCobrancaContaContabil";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONTACONTABIL", recebida01_idcodigoctabil, "Conta Contabil");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void bwrRecebidaObserva_RunWorkerAsync()
        {
            bwrRecebidaObserva = new BackgroundWorker();
            bwrRecebidaObserva.DoWork += new DoWorkEventHandler(bwrRecebidaObserva_DoWork);
            bwrRecebidaObserva.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrRecebidaObserva_RunWorkerCompleted);
            bwrRecebidaObserva.RunWorkerAsync();
        }

        private void bwrRecebidaObserva_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                String tabela = "RECEBIDAOBSERVA";
                String query = "ID, IDDUPLICATA, DATA, OBSERVAR, EMITENTE";
                String ordem = "DATA";
                String filtro = "IDDUPLICATA=" + idRecebida ;

                dtRecebidaObserva = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, tabela, query, filtro, ordem);
                dtRecebidaObserva.Columns.Add("POSICAO", Type.GetType("System.Int32"));

                for (Int32 x = 0; x < dtRecebidaObserva.Rows.Count; x++ )
                {
                    dtRecebidaObserva.Rows[x]["posicao"] = x + 1;
                }
                dtRecebidaObserva.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bwrRecebidaObserva_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRecebidaObserva.DataSource = dtRecebidaObserva;

                clsGridHelper.MontaGrid(dgvRecebidaObserva,
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
                dgvRecebidaObserva.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
                clsGridHelper.FontGrid(dgvRecebidaObserva, 7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tspRecebidaObservaIncluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Não Disponivel neste Modulo");
            /*
            tclPagar.SelectedTab = tabObserva;
            gbxPagar.Enabled = false;
            gbxObserva.Visible = true;
            tbxDuplicata3.Text =  tbxDuplicata1.Text = tbxDuplicata.Text;
            tbxEmissao3.Text = tbxEmissao1.Text = tbxEmissao.Text;
            tbxPosicao3.Text = tbxPosicao1.Text = tbxPosicao.Text;
            tbxPosicaoFim3.Text = tbxPosicaoFim1.Text = tbxPosicaoFim.Text;
            tbxClienteCognome3.Text = tbxClienteCognome1.Text = tbxClienteCognome.Text;
            tbxClienteTelefone3.Text = tbxClienteTelefone1.Text = tbxClienteTelefone.Text;
            tbxClienteCGC3.Text = tbxClienteCGC1.Text = tbxClienteCGC.Text;
            // incluir
            DataRow rowIncluir = dtPagasObserva.NewRow();
            posicaoRecebidaObserva = dtPagasObserva.Rows.Count + 1;
            rowIncluir["IDDUPLICATA"] = idRecebida;
            rowIncluir["DATA"] = DateTime.Now;
            rowIncluir["EMITENTE"] = clsInfo.zusuario;
            rowIncluir["OBSERVAR"] = "";
            rowIncluir["POSICAO"] = posicaoRecebidaObserva;
            dtPagasObserva.Rows.Add(rowIncluir);
            RecebidaObservaGridCampos(posicaoRecebidaObserva);

            tbxObservaObservar.Select();
            tbxObservaObservar.SelectAll();
             */

        }

        private void RecebidaObservaGridCampos(Int32 posicao)
        {
            DataRow row;
            row = clsSelectInsertUpdateBLL.SelectCollectionFields(clsInfo.conexaosqldados, "*", "RECEBIDAOBSERVA", "ID = " + clsParser.Int32Parse(idRecebidaObserva.ToString()), "ID");

            tbxObservaData.Text = clsParser.SqlDateTimeParse(row["DATA"].ToString()).Value.ToString("dd/MM/yyyy");
            tbxObservaEmitente.Text = row["EMITENTE"].ToString();
            tbxObservaObservar.Text = row["OBSERVAR"].ToString();
        }

        private void tspRecebidaObservaAlterar_Click(object sender, EventArgs e)
        {
            if (dgvRecebidaObserva.CurrentRow != null)
            {
                tclPagar.SelectedTab = tabObserva;
                gbxPagar.Enabled = false;
                gbxObserva.Visible = true;
                tbxDuplicata3.Text =  tbxDuplicata1.Text = tbxDuplicata.Text;
                tbxEmissao3.Text = tbxEmissao1.Text = tbxEmissao.Text;
                tbxPosicao3.Text = tbxPosicao1.Text = tbxPosicao.Text;
                tbxPosicaoFim3.Text = tbxPosicaoFim1.Text = tbxPosicaoFim.Text;
                tbxClienteCognome3.Text = tbxClienteCognome1.Text = tbxClienteCognome.Text;
                tbxClienteTelefone3.Text = tbxClienteTelefone1.Text = tbxClienteTelefone.Text;
                tbxClienteCGC3.Text = tbxClienteCGC1.Text = tbxClienteCGC.Text;

                // alterar
                idRecebidaObserva = clsParser.Int32Parse(dgvRecebidaObserva.CurrentRow.Cells["ID"].Value.ToString());
                RecebidaObservaGridCampos(idRecebidaObserva);

                tbxObservaObservar.Select();
                tbxObservaObservar.SelectAll();
            }
            else
            {
                MessageBox.Show("Primeiro selecione um registro!");
            }

        }

        private void dgvRecebidaObserva_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspRecebidaObservaAlterar.PerformClick();
        }
        private void SalvarRecebidaObserva()
        {
            String query;
            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
            SqlCommand scd;

            foreach (DataRow row in dtRecebidaObserva.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    if (clsParser.Int32Parse(row["ID"].ToString()) > 0)
                    {
                        // Apagar observação do pagar
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scd = new SqlCommand("delete recedibaobserva where id=@id", scn);
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();

                    }
                }
                else if (row.RowState == DataRowState.Added)
                {
                    // Incluir
                    query = "Insert Into RECEBIDAOBSERVA (IDDUPLICATA, DATA, OBSERVAR, EMITENTE) VALUES (@IDDUPLICATA, @DATA, @OBSERVAR, @EMITENTE); SELECT SCOPE_IDENTITY()";
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
                    query = "UPDATE RECEBIDAOBSERVA SET IDDUPLICATA =@IDDUPLICATA, DATA=@DATA, OBSERVAR=@OBSERVAR, EMITENTE=@EMITENTE WHERE ID=@ID";
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
        /*
        private void VerificarPago()
        {
            DataTable dtPago = new DataTable();
            dtPago = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "RECEBIDA " +
                                "left join Recebida01 on Recebida01.IDDUPLICATA=RECEBIDA.ID  " +
                                "left join SITUACAOCOBRANCACOD on SITUACAOCOBRANCACOD.id=Recebida01.IDCOBRANCACOD  " +
                                "left join SITUACAOCOBRANCACOD1 on SITUACAOCOBRANCACOD1.id=Recebida01.IDCOBRANCAHIS  ",
                                "Recebida01.ID, Recebida01.IDDUPLICATA, Recebida01.DATAENVIO, Recebida01.DATAOK, " +
                                "SITUACAOCOBRANCACOD.CODIGO + '=' + SITUACAOCOBRANCACOD.NOME AS [COB], SITUACAOCOBRANCACOD1.CODIGO AS [HIS], " +
                                "Recebida01.VALOR, Recebida01.DEBCRED, Recebida01.BAIXOUCOMO ",
                                "Recebida01.IDDUPLICATA = " + clsParser.Int32Parse(idRecebida.ToString()), "DATAENVIO");



            tbxRecebido.Text = "0";
            ValorPrincipal = 0;
            foreach (DataRow drPago in dtPago.Rows)
            { // Somar os Pagamentos
                if (drPago["COB"].ToString().Length >= 2)
                {

                    if (drPago["DEBCRED"].ToString() == "D")
                    {
                        tbxRecebido.Text = (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(drPago["VALOR"].ToString())).ToString("N2");
                        if (drPago["COB"].ToString().Substring(0, 2) == "00" || drPago["COB"].ToString().Substring(0, 2) == "01")
                        {
                            ValorPrincipal = ValorPrincipal + clsParser.DecimalParse(drPago["VALOR"].ToString());
                            if (tbxDataBaixa.Text.Length < 2)
                            {
                                tbxDataBaixa.Text = drPago["DATAOK"].ToString();
                            }
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
                            for (int x = 0; x < dtPago.Rows.Count; x++)
                            {
                                if (dtRecebida01.Rows[x]["COB"].ToString().Substring(0, 2) == "08")
                                {
                                    //dtRecebida01.Select("Posicao=" + dtRecebida01.Rows[x]["POSICAO"].ToString())[0].Delete();
                                    //dtRecebida01.AcceptChanges();
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
            }
            // GRAVAR O VALOR PRINCIPAL COMO VALOR PAGO
            // DEVIDO AO QUE MOSTRO NA TELA PRINCIPAL EU NÃO COLOCO OS JUROS PAGO AQUI POIS AI PODERIA FICAR NEGATIVO
            // FICA ERRADO SE ELE PAGOU JUROS (VERIFICAR NO FUTURO)
            // tbxrecebido = valor pago ( ele soma tudo quando entra no form )
            // como não grava este campo ele clsVisualmente fica certo
            

            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("UPDATE PAGAR SET VALORPAGO=@valorprincipal WHERE id=@id", scn);
            scd.Parameters.Add("@id", SqlDbType.Int).Value = idRecebida;
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
         * */
        private void bwrRecebida01_RunWorkerAsync()
        {
            bwrRecebida01 = new BackgroundWorker();
            bwrRecebida01.DoWork += new DoWorkEventHandler(bwrRecebida01_DoWork);
            bwrRecebida01.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrRecebida01_RunWorkerCompleted);
            bwrRecebida01.RunWorkerAsync();
        }

        private void bwrRecebida01_DoWork(object sender, DoWorkEventArgs e)
        {
            dtRecebida01 = CarregaGridRecebida01();  
        }
        public DataTable CarregaGridRecebida01()
        {
            try
            {
                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                       "RECEBIDA " +
                       "left join Recebida01 on Recebida01.IDDUPLICATA=RECEBIDA.ID " +
                       "left join SITUACAOCOBRANCACOD on SITUACAOCOBRANCACOD.id=Recebida01.IDCOBRANCACOD " +
                       "left join SITUACAOCOBRANCACOD1 on SITUACAOCOBRANCACOD1.id=Recebida01.IDCOBRANCAHIS ",
                       "Recebida01.ID, Recebida01.IDDUPLICATA, Recebida01.DATAENVIO, Recebida01.DATAOK, " +
                       "SITUACAOCOBRANCACOD.CODIGO + '=' + SITUACAOCOBRANCACOD.NOME AS [COB], " +
                       "Recebida01.VALOR, Recebida01.DEBCRED, Recebida01.BAIXOUCOMO ",
                       "Recebida01.IDDUPLICATA = " + idRecebida, "SITUACAOCOBRANCACOD.CODIGO");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrRecebida01_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRecebida01.DataSource = dtRecebida01;
                clsGridHelper.MontaGrid(dgvRecebida01,
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

                dgvRecebida01.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvRecebida01.Columns["DATAENVIO"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvRecebida01.Columns["DATAOK"].DefaultCellStyle.Format = "dd/MM/yyyy";

                clsGridHelper.FontGrid(dgvRecebida01, 7);

                //tbxRecebido.Text = clsGridHelper.SomaGrid(dgvRecebida01, "VALOR").ToString("N2");


                dgvRecebida01.Sort(dgvRecebida01.Columns["DATAENVIO"], ListSortDirection.Ascending);

                //GridHelper.SelecionaLinha(indexPagar, dgvRecebida01, 1);

                tbxRecebido.Text = "0";
                //ValorPrincipal = 0;
                foreach (DataRow drPago in dtRecebida01.Rows)
                { // Somar os Pagamentos
                    if (tbxDataBaixa.Text.Length < 2)
                    {
                        tbxDataBaixa.Text = clsParser.SqlDateTimeParse(drPago["DATAOK"].ToString()).Value.ToString("dd/MM/yyyy");
                    }
                }
                if (tbxDataBaixa.Text.Length > 2)
                {
                    calcular();
                }
                tbxRecebido.Text = "0";
                tbxPagando.Text = "0";
                foreach (DataRow drPago in dtRecebida01.Rows)
                {
                    if (drPago["DEBCRED"].ToString() == "C")
                    {
                        tbxRecebido.Text = (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(drPago["VALOR"].ToString())).ToString("N2");
                    }
                    else
                    {
                        tbxRecebido.Text = (clsParser.DecimalParse(tbxRecebido.Text) - clsParser.DecimalParse(drPago["VALOR"].ToString())).ToString("N2");
                    }
                }
                tbxDiferenca.Text = (clsParser.DecimalParse(tbxSaldo.Text) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text))).ToString("N2");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvRecebida01_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspCobAlterar.PerformClick();
        }

        private void dgvRecebidaObserva_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void tspRetornarPagar01_Click(object sender, EventArgs e)
        {
            tabPagar.Select();
            tclPagar.SelectedIndex = 0;
            gbxPagar.Enabled = true;
            gbxPagarBaixa.Visible = false;
        }


    }
}
