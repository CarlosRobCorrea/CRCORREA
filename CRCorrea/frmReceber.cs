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
    public partial class frmReceber : Form
    {
        
        
        
        clsFinanceiro clsFinanceiro = new clsFinanceiro();

        ParameterFields pfields = new ParameterFields();

        clsReceberBLL clsReceberBLL;
        clsReceberInfo clsReceberInfo;
        clsReceberInfo clsReceberInfoOld;

        DataGridViewRowCollection rows;

        Int32 id;
        Int32 idReceberNFV;

        Int32 idBanco;
        Int32 idBancoint;
        Int32 idBancointBaixa;
        Int32 idCentrocusto;
        Int32 idCentrocustoBaixa;
        Int32 idCliente;
        Int32 idContacontabil;
        Int32 idContacontabilBaixa;
        Int32 idCoordenador;
        Int32 idDocNFV;
        Int32 idDocumento;
        Int32 idFormapagto;
        Int32 idFormapagtoBaixa;
        Int32 idHistorico;
        Int32 idHistoricoBaixa;
        Int32 idNotafiscal;
        Int32 idSitBanco;
        Int32 idSupervisor;
        Int32 idVendedor;

        Int32 totaldias;
        Int32 dias;

        String atevencimento;

        DialogResult resultado;

        SqlConnection scn;
        SqlCommand scd;

        // Pagar Observa
        DataTable dtReceberObserva;
        clsReceberObservaBLL clsReceberObservaBLL;
        clsReceberObservaInfo clsReceberObservaInfo;
        clsReceberObservaInfo clsReceberObservaInfoOld;

        Int32 receberobserva_id;
        Int32 receberobserva_idduplicata;
        Int32 receberobserva_posicao;

        // Tabela Virtual
        DataTable dtReceber01;
        DataTable dtRecebida01;

        GridColuna[] dtReceber01Colunas;

        // Dados virtuais
        Int32 posicao;
        Int32 receber01_idcobrancacod;
        Int32 receber01_idcobrancahis;
        Int32 receber01_idhistorico;
        Int32 receber01_idcentrocusto;
        Int32 receber01_idcodigoctabil;

        DateTime dataBaixa;
        DateTime dataVencimento;

        Decimal valorPrincipalDuplicata = 0;  // VALOR PRINCIPAL A Receber
        Decimal ValorPrincipal = 0;           // VALORPRINCIPAL PAGO
        Decimal ValorDescontoPago = 0;        // Valor de Desconto que foi pago parcial (antecipado)

        public frmReceber()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idReceber,
                         DataGridViewRowCollection _rows)
        {
            this.id = _idReceber;
            this.rows = _rows;

            clsReceberBLL = new clsReceberBLL();

            tbxDataBaixa.Text = DateTime.Today.ToString("dd/MM/yyyy");
            tbxDataCredito.Text = DateTime.Today.ToString("dd/MM/yyyy");

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from docfiscal order by cognome", tbxDocumento);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxClienteCognome);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", tbxHistorico);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", tbxCentroCusto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from contacontabil order by codigo", tbxContaContabil);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select numero from nfvenda order by numero", tbxNFVendaNumero);
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

            clsReceberObservaBLL = new clsReceberObservaBLL();

            // tela registro Receber01
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaocobrancacod order by codigo", tbxReceber01CobrancaCodCodigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", tbxReceber01HistCod);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", tbxReceber01CCCod);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from contacontabil order by codigo", tbxReceber01CtabilCodigo);


            dtReceber01Colunas = new GridColuna[]
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

        private void frmReceber_Load(object sender, EventArgs e)
        {
            ReceberCarregar();
        }

        private void frmReceber_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
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
                case "tbxValorCredito":
                    calcular();
                    break;
                case "tbxVencimentoPrev":
                    calcular();
                    break;
                case "tbxDataBaixa":
                    if (clsParser.SqlDateTimeParse(tbxDataBaixa.Text).Value > clsParser.SqlDateTimeParse(tbxDataCredito.Text).Value)
                    {
                        tbxDataCredito.Text = tbxDataBaixa.Text;
                    }
                    calcular();
                    VerificarPago();       //verifica se já foi pago algum valor (apenas principal)
                    CalcularPagando();
                    break;
                case "tbxDataCredito":
                    if (clsParser.SqlDateTimeParse(tbxDataBaixa.Text).Value > clsParser.SqlDateTimeParse(tbxDataCredito.Text).Value)
                    {
                        tbxDataCredito.Text = tbxDataBaixa.Text;
                    }
                    calcular();
                    VerificarPago();       //verifica se já foi pago algum valor (apenas principal)
                    CalcularPagando();
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

            //SalvarMovimentacaoReceber01();
            clsVisual.FormatarCampoNumerico(sender);
            clsVisual.ControlLeave(sender);            
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

            tbxReceber01Valor.Text = clsParser.DecimalParse(tbxReceber01Valor.Text).ToString("N2");
            tbxReceber01Comissao.Text = clsParser.DecimalParse(tbxReceber01Comissao.Text).ToString("N2");
            tbxReceber01ComissaoGer.Text = clsParser.DecimalParse(tbxReceber01ComissaoGer.Text).ToString("N2");
            tbxReceber01ComissaoSup.Text = clsParser.DecimalParse(tbxReceber01ComissaoSup.Text).ToString("N2");

            if (ckxChegou.Checked == true)
            {
                ckxChegou.Text = "Sim - Enviou a cobrança";
            }
            else
            {
                ckxChegou.Text = "Não - Enviou a cobrança";
            }

            if (ckxDespesaPublica.Checked == true)
            {
                ckxDespesaPublica.Text = "Sim - Conferido";
            }
            else
            {
                ckxDespesaPublica.Text = "Não é Conferido";
            }
            if (ctl.Name == tbxVencimentoPrev.Name)
            {
                if (DateTime.Parse(tbxVencimentoPrev.Text) > DateTime.Parse(tbxVencimento.Text))
                {
                    tbxDataBaixa.Text = tbxVencimentoPrev.Text;
                }
            }
            if (ctl.Name == tbxDataBaixa.Name)
            {
                if (DateTime.Parse(tbxDataBaixa.Text) != DateTime.Parse(tbxDataCredito.Text))
                {
                    tbxDataCredito.Text = tbxDataBaixa.Text;
                }
            }


            if (clsInfo.znomegrid == btnIdDocumento.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idDocumento = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxDocumento.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
                        tbxDocumento.Select();
                    }
                }
            }
            if (ctl.Name == tbxDocumento.Name)
            {
                idDocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from DOCFISCAL where COGNOME= '" + tbxDocumento.Text + "' "));
                if (idDocumento == 0)
                {
                    idDocumento = clsInfo.zdocumento;
                }
                tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + idDocumento + " ");
            }

            //
            if (clsInfo.znomegrid == btnIdCliente.Name)
            {
                if (clsInfo.zrow != null)
                {
                    idCliente = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxClienteCognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();

                    tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CGC from CLIENTE where ID = " + idCliente, "");
                    //tbxClienteCGC.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "CGC", "ID", idCliente);

                    tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DDD + ' - ' + TELEFONE from CLIENTE where ID = " + idCliente, "");
                    //tbxClienteTelefone.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "DDD", "ID", idCliente) +
                    // " - " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "TELEFONE", "ID", idCliente);
                    tbxClienteCognome.Select();
                    tbxClienteCognome.SelectAll();
                }
            }

            if (ctl.Name == tbxClienteCognome.Name)
            {
                idCliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CLIENTE where COGNOME='" + tbxClienteCognome.Text + "' "));
                if (idCliente == 0)
                {
                    idCliente = clsInfo.zempresaclienteid;
                }
                tbxClienteCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idCliente);
                tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CGC FROM CLIENTE where id=" + idCliente);
                tbxClienteCGC.Text = clsVisual.CamposVisual("CGC", tbxClienteCGC.Text);
                tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT DDD FROM CLIENTE where id=" + idCliente);
                tbxClienteTelefone.Text += " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TELEFONE FROM CLIENTE where id=" + idCliente);
            }

            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1)
            {
                if (clsInfo.zrow != null)
                {
                    //if (clsInfo.znomegrid == btnIdVendedor.Name)
                    //{
                    //    idVendedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    //    tbxVendedor_Cognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();

                    //    tbxVendedor_Cognome.Select();
                    //    tbxVendedor_Cognome.SelectAll();
                    //}
                }
            }

            //if (ctl.Name == tbxVendedor_Cognome.Name)
            //{
            //    idVendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CLIENTE where COGNOME='" + tbxVendedor_Cognome.Text + "' "));
            //    if (idVendedor == 0)
            //    {
            //        idVendedor = clsInfo.zempresaclienteid;
            //    }
            //    tbxVendedor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idVendedor);
            //}
            //if (clsInfo.znomegrid == btnIdCoordenador.Name)
            //{
            //    if (clsInfo.zrow != null)
            //    {
            //        idCoordenador = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
            //        tbxCoordenador_Cognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
            //        tbxCoordenador_Cognome.Select();
            //        tbxCoordenador_Cognome.SelectAll();
            //    }
            //}
            //if (ctl.Name == tbxCoordenador_Cognome.Name)
            //{
            //    idCoordenador = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CLIENTE where COGNOME='" + tbxCoordenador_Cognome.Text + "' "));
            //    if (idCoordenador == 0)
            //    {
            //        idCoordenador = clsInfo.zempresaclienteid;
            //    }
            //    tbxCoordenador_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idCoordenador);
            //}
            //if (clsInfo.znomegrid == btnIdSupervisor.Name)
            //{
            //    if (clsInfo.zrow != null)
            //    {
            //        idSupervisor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
            //        tbxSupervisor_Cognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
            //        tbxSupervisor_Cognome.Select();
            //        tbxSupervisor_Cognome.SelectAll();
            //    }
            //}
            //if (ctl.Name == tbxSupervisor_Cognome.Name)
            //{
            //    idSupervisor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CLIENTE where COGNOME='" + tbxSupervisor_Cognome.Text + "' "));
            //    if (idSupervisor == 0)
            //    {
            //        idSupervisor = clsInfo.zempresaclienteid;
            //    }
            //    tbxSupervisor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idSupervisor);
            //}
            //
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
                    tbxHistorico.Select();
                    tbxHistorico.SelectAll();
                }
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
            if (clsInfo.znomegrid == btnIdCentroCusto.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idCentrocusto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCentroCusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCentroCustoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    }
                }
                tbxCentroCusto.Select();
                tbxCentroCusto.SelectAll();
            }
            if (ctl.Name == tbxCentroCusto.Name)
            {
                idCentrocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + tbxCentroCusto.Text + "' "));
                if (idCentrocusto == 0)
                {
                    idCentrocusto = clsInfo.zcentrocustos;
                }
                tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID= " + idCentrocusto);
                tbxCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID= " + idCentrocusto);
            }

            if (clsInfo.znomegrid == btnIdCodigoCtaBil.Name)
            {
                if (clsInfo.zrow != null)
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
            }
            if (ctl.Name == tbxContaContabil.Name)
            {
                idContacontabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONTACONTABIL where CODIGO=' " + tbxContaContabil.Text + "' "));
                if (idContacontabil == 0)
                {
                    idContacontabil = clsInfo.zcontacontabil;
                }
                tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID= " + idContacontabil);
                tbxContaContabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID= " + idContacontabil);
            }
            if (clsInfo.znomegrid == btnIdFormaPagto.Name)
            {
                if (clsInfo.zrow != null)
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
            }
            if (ctl.Name == tbxFormaPagto.Name)
            {
                idFormapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM SITUACAOTIPOTITULO where CODIGO='" + tbxFormaPagto.Text.Substring(0, 2) + "' "));
                if (idFormapagto == 0)
                {
                    idFormapagto = clsInfo.zformapagto;
                }
                tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + idFormapagto);
                tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + idFormapagto);
            }
            if (clsInfo.znomegrid == btnIdBanco.Name)
            {
                if (clsInfo.zrow != null)
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
            }
            if (ctl.Name == tbxBancoConta.Name)
            {
                idBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from TAB_BANCOS where CODIGO= '" + tbxBancoConta.Text + "' "));
                if (idBanco == 0)
                {
                    idBanco = clsInfo.zbanco;
                }
                tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco);
                tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);
            }
            if (clsInfo.znomegrid == btnIdBancoInt.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idBancoint = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBancoInt.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                        tbxBancoNomeInterno.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();

                        idBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancoint));
                        tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco);
                        tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);

                    }
                    tbxBaixaBcoInt.Select();
                }

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

                idBanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + idBancoint));
                tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco);
                tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);
            }

            if (clsInfo.znomegrid == btnIdSitBanco.Name)
            {
                if (clsInfo.zrow != null)
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
            // Receber01
            if (clsInfo.znomegrid == btnReceber01_idcobrancacod.Name)
            {
                if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                {
                    receber01_idcobrancacod = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxReceber01CobrancaCodCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    tbxReceber01CobrancaCodNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    clsInfo.znomegrid = "";
                }
                tbxReceber01CobrancaCodCodigo.Select();
                tbxReceber01CobrancaCodCodigo.SelectAll();
            }


            if (ctl.Name == tbxReceber01CobrancaCodCodigo.Name)
            {
                receber01_idcobrancacod = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO = '" + tbxReceber01CobrancaCodCodigo.Text.Trim()+"'", "0"));
                //receber01_idcobrancacod = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", tbxReceber01CobrancaCodCodigo.Text.Trim()));
                
                if (receber01_idcobrancacod == 0)
                {
                    receber01_idcobrancacod = clsInfo.zsituacaocobrancacod;
                }
                tbxReceber01CobrancaCodCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + receber01_idcobrancacod, "");
                //tbxReceber01CobrancaCodCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", receber01_idcobrancacod).Trim();
                tbxReceber01CobrancaCodNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + receber01_idcobrancacod, "");
                //tbxReceber01CobrancaCodNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", receber01_idcobrancacod).Trim();
            }
            if (clsInfo.znomegrid == btnReceber01_idcobrancahis.Name)
            {
                if (clsInfo.zrow.Cells["ID"] != null &&
                    clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                {
                    receber01_idcobrancahis = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxReceber01CobrancaCod1Codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    tbxReceber01CobrancaCod1Nome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    clsInfo.znomegrid = "";
                }
                tbxReceber01CobrancaCod1Codigo.Select();
                tbxReceber01CobrancaCod1Codigo.SelectAll();

            }
            if (ctl.Name == tbxReceber01CobrancaCod1Codigo.Name)
            {
                if (tbxReceber01CobrancaCod1Codigo.ReadOnly == false)
                {
                    receber01_idcobrancahis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD1 where CODIGO = '" + tbxReceber01CobrancaCod1Codigo.Text.Trim() + "'", ""));
                    //receber01_idcobrancahis = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", tbxReceber01CobrancaCod1Codigo.Text.Trim()));
                    if (receber01_idcobrancahis == 0)
                    {
                        receber01_idcobrancahis = 0;
                    }

                    tbxReceber01CobrancaCod1Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD1 where ID = " + receber01_idcobrancahis, "");
                    //tbxReceber01CobrancaCod1Codigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", receber01_idcobrancahis).Trim();
                    tbxReceber01CobrancaCod1Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD1 where ID = " + receber01_idcobrancahis, "");
                    //tbxReceber01CobrancaCod1Nome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", receber01_idcobrancahis).Trim();
                }
            }

            if (clsInfo.znomegrid == btnReceber01_idhistorico.Name)
            {
                if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                {
                    receber01_idhistorico = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxReceber01HistCod.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    tbxReceber01HistNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                }
                tbxReceber01HistCod.Select();
                tbxReceber01HistCod.SelectAll();

            }

            if (clsInfo.znomegrid == btnReceber01_idcentrocusto.Name)
            {
                if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                {
                    receber01_idcentrocusto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxReceber01CCCod.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    tbxReceber01CCNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    clsInfo.znomegrid = "";
                }
                tbxReceber01CCCod.Select();
                tbxReceber01CCCod.SelectAll();
            }
            if (ctl.Name == tbxReceber01CCCod.Name)
            {
                if (tbxReceber01CCCod.ReadOnly == false)
                {
                    receber01_idcentrocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO = '" + tbxReceber01CCCod.Text.Trim() + "'", ""));
                    //receber01_idcentrocusto = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ID", "CODIGO", tbxReceber01CCCod.Text.Trim()));

                    if (receber01_idcentrocusto == 0)
                    {
                        receber01_idcentrocusto = clsInfo.zcentrocustos;
                    }
                    tbxReceber01CCCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + receber01_idcentrocusto, "");
                    //tbxReceber01CCCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", receber01_idcentrocusto).Trim();
                    tbxReceber01CCNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID = " + receber01_idcentrocusto, "");
                    //tbxReceber01CCNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", receber01_idcentrocusto).Trim();

                }
            }

            if (clsInfo.znomegrid == btnReceber01_idcodigoctabil.Name)
            {
                if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                {
                    receber01_idcodigoctabil = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxReceber01CtabilCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                    tbxReceber01CtabilNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    tbxReceber01CtabilCodigo.Select();
                    tbxReceber01CtabilCodigo.SelectAll();
                }
            }
            if (ctl.Name == tbxReceber01CtabilCodigo.Name)
            {
                if (tbxReceber01CtabilCodigo.ReadOnly == false)
                {
                    receber01_idcodigoctabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONTACONTABIL where CODIGO = '" + tbxReceber01CtabilCodigo.Text+"'", "0"));
                    //receber01_idcodigoctabil = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "ID", "CODIGO", tbxReceber01CtabilCodigo.Text));

                    if (receber01_idcodigoctabil == 0)
                    {
                        receber01_idcodigoctabil = clsInfo.zcontacontabil;
                    }
                    tbxReceber01CtabilCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID = " + receber01_idcodigoctabil, "");
                    //tbxReceber01CtabilCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", receber01_idcodigoctabil).Trim();
                    tbxReceber01CtabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID = " + receber01_idcodigoctabil, "");
                    //tbxReceber01CtabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", receber01_idcodigoctabil).Trim();

                }
            }
            // Calcular a Comissão Parcial do Vendedor
            if (ctl.Name == tbxReceber01Valor.Name)
            {
                if (tbxReceber01CobrancaCodCodigo.Text == "00" || tbxReceber01CobrancaCodCodigo.Text == "01")
                {
                        Decimal PorVend = 0;
                        Decimal PorCoor = 0;
                        Decimal PorSup = 0;
                        // Vendedor
                        //if (clsParser.DecimalParse(tbxValorComissao.Text) > 0)
                        //{
                        //    PorVend = clsParser.DecimalParse(tbxValorComissao.Text) / clsParser.DecimalParse(tbxValor.Text);
                        //}
                        //// Coordenador

                        //if (clsParser.DecimalParse(tbxValorComissao.Text) > 0)
                        //{
                        //    PorCoor = clsParser.DecimalParse(tbxValorComissaoGer.Text) / clsParser.DecimalParse(tbxValor.Text);
                        //}
                        //// Supervisor
                        //if (clsParser.DecimalParse(tbxValorComissao.Text) > 0)
                        //{
                        //    PorSup = clsParser.DecimalParse(tbxValorComissaoSup.Text) / clsParser.DecimalParse(tbxValor.Text);
                        //}
                        //
                        if (PorVend > 0)
                        {
                            tbxReceber01Comissao.Text = (PorVend * clsParser.DecimalParse(tbxReceber01Valor.Text)).ToString("N2");
                        }
                        if (PorCoor > 0)
                        {
                            tbxReceber01ComissaoGer.Text = (PorCoor * clsParser.DecimalParse(tbxReceber01Valor.Text)).ToString("N2");
                        }
                        if (PorSup > 0)
                        {
                            tbxReceber01ComissaoSup.Text = (PorSup * clsParser.DecimalParse(tbxReceber01Valor.Text)).ToString("N2");
                        }

                }

            }
            // Baixar
            if (clsInfo.znomegrid == btnBaixaIdHistorico.Name)
            {
                if (clsInfo.zrow != null)
                {

                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idHistoricoBaixa = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBaixaHistorico.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxBaixaHistoricoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                }
//                tbxBaixaHistorico.Select();
//                tbxBaixaHistorico.SelectAll();
                tbxBaixaFormaPagto.Select();
                tbxBaixaFormaPagto.SelectAll();




            }
            if (ctl.Name == tbxBaixaHistorico.Name)
            {
                if (tbxBaixaHistorico.ReadOnly == false)
                {
                    idHistoricoBaixa = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO = '" + tbxBaixaHistorico.Text.Trim() + "'", "0"));
                    //idHistoricoBaixa = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ID", "CODIGO", tbxBaixaHistorico.Text.Trim()));
                    if (idHistoricoBaixa == 0)
                    {
                        idHistoricoBaixa = clsInfo.zhistoricos;
                    }
                    tbxBaixaHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + idHistoricoBaixa, "");
                    //tbxBaixaHistorico.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idHistoricoBaixa).Trim();
                    tbxBaixaHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from HISTORICOS where ID = " + idHistoricoBaixa, "");
                    //tbxBaixaHistoricoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", idHistoricoBaixa).Trim();

                }
            }

            if (clsInfo.znomegrid == btnBaixaIdCentroCusto.Name)
            {
                if (clsInfo.zrow != null)
                {

                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idCentrocustoBaixa = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxBaixaCentroCusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxBaixaCentroCustoNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                    }
                }
                tbxBaixaCentroCusto.Select();
                tbxBaixaCentroCusto.SelectAll();
            }
            if (ctl.Name == tbxBaixaCentroCusto.Name)
            {
                if (tbxBaixaCentroCusto.ReadOnly == false)
                {
                    idCentrocustoBaixa = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO = '" + tbxBaixaCentroCusto.Text.Trim() + "'", "0"));
                    //idCentrocustoBaixa = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ID", "CODIGO", tbxBaixaCentroCusto.Text.Trim()));

                    if (idCentrocustoBaixa == 0)
                    {
                        idCentrocustoBaixa = clsInfo.zcentrocustos;
                    }
                    tbxBaixaCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + idCentrocustoBaixa, "");
                    //tbxBaixaCentroCusto.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", idCentrocustoBaixa).Trim();

                    tbxBaixaCentroCustoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID = " + idCentrocustoBaixa, "");
                    //tbxBaixaCentroCustoNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", idCentrocustoBaixa).Trim();

                }
            }
            if (clsInfo.znomegrid == btnBaixaIdContaContabil.Name)
            {
                if (clsInfo.zrow != null)
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
            }
            if (ctl.Name == tbxBaixaContacontabil.Name)
            {
                if (tbxBaixaContacontabil.ReadOnly == false)
                {
                    idContacontabilBaixa = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONTACONTABIL where CODIGO = '" + tbxBaixaContacontabil.Text.Trim() + "'", "0"));
                    //idContacontabilBaixa = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "ID", "CODIGO", tbxBaixaContacontabil.Text));

                    if (idContacontabilBaixa == 0)
                    {
                        idContacontabilBaixa = clsInfo.zcontacontabil;
                    }
                    tbxBaixaContacontabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID = " + idContacontabilBaixa, "");
                    //tbxBaixaContacontabil.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", idContacontabilBaixa).Trim();
                    tbxBaixaContacontabilNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID = " + idContacontabilBaixa, "");
                    //tbxBaixaContacontabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", idContacontabilBaixa).Trim();

                }
            }
            if (clsInfo.znomegrid == btnBaixaIdFormaPagto.Name)
            {
                if (clsInfo.zrow != null)
                {

                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxBaixaFormaPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim() + " = " + clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                        clsInfo.znomegrid = "";
                        tbxBaixaFormaPagto.Select();
                        tbxBaixaFormaPagto.SelectAll();
                    }
                }
            }
            if (ctl.Name == tbxBaixaFormaPagto.Name)
            {
                idFormapagtoBaixa = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOTIPOTITULO where CODIGO = '" + tbxBaixaFormaPagto.Text.Substring(0, 2).Trim() + "'", "0"));
                //idFormapagtoBaixa = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "ID", "CODIGO", tbxBaixaFormaPagto.Text.Substring(0, 2).Trim()));
                if (idFormapagtoBaixa == 0)
                {
                    idFormapagtoBaixa = clsInfo.zformapagto;
                }
                tbxBaixaFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO +' - '+ NOME  from SITUACAOTIPOTITULO where ID = " + idFormapagtoBaixa, "").Trim();
                //tbxBaixaFormaPagto.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "CODIGO", "ID", idFormapagtoBaixa).Trim();
                //tbxBaixaFormaPagto.Text = tbxBaixaFormaPagto.Text + "-" + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "NOME", "ID", idFormapagtoBaixa).Trim();
            }
            //
            if (clsInfo.znomegrid == btnBaixaIdBancoInt.Name)
            {
                if (clsInfo.zrow != null)
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
                }
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
            clsInfo.znomegrid = "";
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
                    ReceberCarregar();
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
                                ReceberCarregar();
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
                                ReceberCarregar();
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
                    ReceberCarregar();
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
            clsReceberInfo = new clsReceberInfo();
            ReceberFillInfo(clsReceberInfo);
            if (clsReceberBLL.Equals(clsReceberInfo, clsReceberInfoOld) == false)
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
                ReceberSalvar();
            }
            return drt;
        }
        private void ReceberSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: Receber
                clsReceberInfo = new clsReceberInfo();
                ReceberFillInfo(clsReceberInfo);
                if (id == 0)
                {
                    clsReceberInfo.id = clsReceberBLL.Incluir(clsReceberInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsReceberBLL.Alterar(clsReceberInfo, clsInfo.conexaosqldados);
                }
                
                // Gravando as Observações
                foreach (DataRow row in dtReceberObserva.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idduplicata"] = clsReceberInfo.id;
                    }
                }

                foreach (DataRow row in dtReceberObserva.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsReceberObservaBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsReceberObservaInfo = new clsReceberObservaInfo();
                        ReceberObservaGridToInfo(clsReceberObservaInfo, Int32.Parse(row["posicao"].ToString()));

                        if (clsReceberObservaInfo.id == 0)
                        {
                            clsReceberObservaInfo.id = clsReceberObservaBLL.Incluir(clsReceberObservaInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsReceberObservaBLL.Alterar(clsReceberObservaInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
                tse.Complete();
            }
        }

        private void ReceberCarregar()
        {
            clsReceberInfoOld = new clsReceberInfo();
            if (id == 0)
            {
                clsReceberInfo = new clsReceberInfo();

                clsReceberInfo.atevencimento = "S";
                clsReceberInfo.baixa = "N";
                clsReceberInfo.boleto = "";
                clsReceberInfo.boletonro = 0;
                clsReceberInfo.chegou = "N";
                clsReceberInfo.contrato = "";
                clsReceberInfo.datalanca = DateTime.Now;
                clsReceberInfo.despesapublica = "N";
                clsReceberInfo.duplicata = 0;
                clsReceberInfo.dv = "";
                clsReceberInfo.emissao = DateTime.Now;
                clsReceberInfo.emitente = clsInfo.zusuario;
                clsReceberInfo.filial = clsInfo.zfilial;
                clsReceberInfo.id = 0;
                clsReceberInfo.idbanco = clsInfo.zbanco;
                clsReceberInfo.idbancoint = clsInfo.zbancoint;
                clsReceberInfo.idcentrocusto = clsInfo.zcentrocustos;
                clsReceberInfo.idcodigoctabil = clsInfo.zcontacontabil;
                clsReceberInfo.idcoordenador = clsInfo.zempresaclienteid;
                clsReceberInfo.iddocumento = clsInfo.zdocumento;
                clsReceberInfo.idformapagto = clsInfo.zformapagto;
                clsReceberInfo.idcliente = clsInfo.zempresaclienteid;
                clsReceberInfo.idhistorico = clsInfo.zhistoricos;
                clsReceberInfo.idnotafiscal = clsInfo.znfcompra;
                clsReceberInfo.idrecebernfv = 0; //clsInfo.znfvenda1
                clsReceberInfo.idsitbanco = clsInfo.zsituacaotitulo;
                clsReceberInfo.idsupervisor = clsInfo.zempresaclienteid;
                clsReceberInfo.idvendedor = clsInfo.zempresaclienteid;

                clsReceberInfo.imprimir = "N";
                clsReceberInfo.observa = "";
                clsReceberInfo.posicao = 1;
                clsReceberInfo.posicaofim = 1;
                clsReceberInfo.setor = "N";
                clsReceberInfo.transfebanco = "";
                clsReceberInfo.valor = 0;
                clsReceberInfo.valorbaixando = 0;
                clsReceberInfo.valorcomissao = 0;
                clsReceberInfo.valorcomissaoger = 0;
                clsReceberInfo.valorcomissaosup = 0;
                clsReceberInfo.valordesconto = 0;
                clsReceberInfo.valordevolvido = 0;
                clsReceberInfo.valorjuros = 0;
                clsReceberInfo.valorliquido = 0;
                clsReceberInfo.valormulta = 0;
                clsReceberInfo.valorpago = 0;
                clsReceberInfo.vencimento = DateTime.Now;
                clsReceberInfo.vencimentoprev = DateTime.Now;
            }
            else
            {
                clsReceberInfo = clsReceberBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            ReceberCampos(clsReceberInfo);
            ReceberFillInfo(clsReceberInfoOld);

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
            bwrRecebida01_RunWorkerAsync();

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
            dtReceberObserva = clsReceberObservaBLL.GridDados(clsReceberInfo.id);

            DataColumn dcPosicao = new DataColumn("posicao", Type.GetType("System.Int32"));
            dtReceberObserva.Columns.Add(dcPosicao);

            for (Int32 i = 1; i <= dtReceberObserva.Rows.Count; i++)
            {
                dtReceberObserva.Rows[i - 1]["posicao"] = i;
            }

            dtReceberObserva.AcceptChanges();
            clsReceberObservaBLL.GridMonta(dgvReceberObserva, dtReceberObserva, receberobserva_posicao);


        }
        private void ReceberCampos(clsReceberInfo info)
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
            if (info.chegou == "S")
            {
                ckxChegou.Checked = true;
                ckxChegou.Text = "Sim - enviou a cobrança";
            }
            else
            {
                ckxChegou.Checked = false;
                ckxChegou.Text = "Não - enviou a cobrança";
            }

            tbxDataLanca.Text = info.datalanca.ToString("dd/MM/yyyy");
            if (info.despesapublica == "S")
            {
                ckxDespesaPublica.Checked = true;
                ckxDespesaPublica.Text = "Sim - conferido";
            }
            else
            {
                ckxDespesaPublica.Checked = false;
                ckxDespesaPublica.Text = "Não - conferido";
            }
            tbxDuplicata.Text = info.duplicata.ToString();
            tbxDv.Text = info.dv;
            tbxEmissao.Text = info.emissao.ToString("dd/MM/yyyy");
            tbxEmitente.Text = info.emitente;
            tbxFilial.Text = info.filial.ToString();
            idBanco = info.idbanco;
            if (idBanco == 0) { idBanco = clsInfo.zbanco; }
            tbxBancoConta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from TAB_BANCOS where ID= " + idBanco);
            tbxBancoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from TAB_BANCOS where ID= " + idBanco);
            idBancoint = info.idbancoint;
            if (idBancoint == 0) { idBancoint = clsInfo.zbancoint; }
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

            //idCoordenador = info.idcoordenador;
            //if (idCoordenador == 0) { idCoordenador = clsInfo.zempresaclienteid; }
            //tbxCoordenador_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idCoordenador);


            idDocumento = info.iddocumento;
            if (idDocumento == 0) { idDocumento = clsInfo.zdocumento; }
            tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + idDocumento);
            if (tbxDocumento.Text == "CTO")
            {
                gbxNotaFiscal.Text = "Esta Duplicata se refere ao Contrato ";
            }
            idFormapagto = info.idformapagto;
            if (idFormapagto == 0) { idFormapagto = clsInfo.zformapagto; }
            tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + idFormapagto);
            tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + idFormapagto);
            idCliente = info.idcliente;
            if (idCliente == 0) { idCliente = clsInfo.zempresaclienteid; }
            tbxClienteCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idCliente);
            tbxClienteCGC.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CGC FROM CLIENTE where id=" + idCliente);
            tbxClienteCGC.Text = clsVisual.CamposVisual("CGC", tbxClienteCGC.Text);
            tbxClienteTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT DDD FROM CLIENTE where id=" + idCliente);
            tbxClienteTelefone.Text += " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TELEFONE FROM CLIENTE where id=" + idCliente);
            idHistorico = info.idhistorico;
            if (idHistorico == 0) { idHistorico = clsInfo.zhistoricos; }
            tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + idHistorico);
            if (tbxHistorico.Text == "")
            {
                idHistorico = clsInfo.zhistoricos;
                tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + idHistorico);
            }
            tbxHistoricoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT NOME FROM HISTORICOS where id=" + idHistorico);
            idNotafiscal = info.idnotafiscal;
            if (tbxDocumento.Text == "CTO")
            {
                if (idNotafiscal == 0) { idNotafiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM CONTRATO where NUMERO=" + 0));  }

                tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM CONTRATO where id=" + idNotafiscal);
                tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOTALLIQUIDO FROM CONTRATO where id=" + idNotafiscal);
                tbxNotaDoc.Text = tbxDocumento.Text;
            }
            else
            {
                if (idNotafiscal == 0) { idNotafiscal = clsInfo.znfvenda; }
                tbxNFVendaNumero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM NFVENDA where id=" + idNotafiscal);
                tbxNFVendaTotalNotaFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOTALNOTAFISCAL FROM NFVENDA where id=" + idNotafiscal);
                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDDOCUMENTO FROM NFVENDA where id=" + idNotafiscal)) > 0)
                {
                    idDocNFV = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDDOCUMENTO FROM NFVENDA where id=" + idNotafiscal));
                    if (idDocNFV == 0)
                        idDocNFV = clsInfo.zdocumento;

                    tbxNotaDoc.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM DOCFISCAL where id=" +  idDocNFV);
                }
                else
                {
                    tbxNotaDoc.Text = "";
                }
                
            }
            idReceberNFV = info.idrecebernfv;
            idSitBanco = info.idsitbanco;
            if (idSitBanco == 0) { idSitBanco = clsInfo.zsituacaotitulo; }
            tbxSitBanco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTITULO where id=" + idSitBanco);
            tbxSituacaoNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTITULO where id=" + idSitBanco);

            //idSupervisor = info.idsupervisor;
            //if (idSupervisor == 0) { idSupervisor = clsInfo.zempresaclienteid; }
            //tbxSupervisor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idSupervisor);

            //idVendedor = info.idvendedor;
            //if (idVendedor == 0) { idVendedor = clsInfo.zempresaclienteid; }
            //tbxVendedor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + idVendedor);

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
            //tbxValorComissao.Text = info.valorcomissao.ToString("N2");
            //tbxValorComissaoGer.Text = info.valorcomissaoger.ToString("N2");
            //tbxValorComissaoSup.Text = info.valorcomissaosup.ToString("N2");

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
            tbxDevolucao.Text = info.valordevolvido.ToString("N2");
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
            if (tbxFormaPagto.Text.Substring(0, 2) == "CH")
            { // Se for cheque colocar a data de deposito como credito
                tbxDataCredito.Text = tbxVencimento.Text;
            }
            else
            {
                tbxDataCredito.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }

        }
        private void ReceberFillInfo(clsReceberInfo info)
        {
            info.id = id;

            if (ckxAteVencimento.Checked == true) { info.atevencimento = "S"; } else { info.atevencimento = "N"; }
            info.baixa = "";
            info.boleto = tbxBoleto.Text;
            info.boletonro = clsParser.DecimalParse(tbxBoletoNro.Text);
            if (ckxChegou.Checked == true) { info.chegou = "S"; } else { info.chegou = "N"; }
            info.datalanca = DateTime.Parse(tbxDataLanca.Text);
            if (ckxDespesaPublica.Checked == true) { info.despesapublica = "S"; } else { info.despesapublica = "N"; }
            if (info.contrato == null)
            {
                info.contrato = "";
            }
            info.duplicata = clsParser.Int64Parse(tbxDuplicata.Text);
            info.dv = tbxDv.Text;
            info.emissao = DateTime.Parse(tbxEmissao.Text);
            info.emitente = tbxEmitente.Text;
            info.filial = clsParser.Int32Parse(tbxFilial.Text);
            info.idbanco = idBanco;
            info.idbancoint = idBancoint;
            info.idcentrocusto = idCentrocusto;
            info.idcodigoctabil = idContacontabil;
            info.idcoordenador = idCoordenador;
            info.iddocumento = idDocumento;
            info.idformapagto = idFormapagto;
            info.idcliente = idCliente;
            info.idhistorico = idHistorico;
            info.idnotafiscal = idNotafiscal;
            info.idrecebernfv = idReceberNFV;
            info.idsitbanco = idSitBanco;
            info.idsupervisor = idSupervisor;
            info.idvendedor = idVendedor;

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
            if (info.transfebanco == null)
            {
                info.transfebanco = "";
            }
            info.valor = clsParser.DecimalParse(tbxValor.Text);
            info.valorbaixando = clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text);
            //info.valorcomissao = clsParser.DecimalParse(tbxValorComissao.Text);
            //info.valorcomissaoger = clsParser.DecimalParse(tbxValorComissaoGer.Text);
            //info.valorcomissaosup = clsParser.DecimalParse(tbxValorComissaoSup.Text); 
            info.valordesconto = clsParser.DecimalParse(tbxValorDesconto.Text);
            info.valordevolvido = clsParser.DecimalParse(tbxDevolucao.Text);
            info.valorjuros = clsParser.DecimalParse(tbxValorJuros.Text);
            info.valorliquido = clsParser.DecimalParse(tbxValorLiquido.Text);
            info.valormulta = clsParser.DecimalParse(tbxValorMulta.Text);
            info.valorpago = clsParser.DecimalParse(tbxRecebido.Text);
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
                ckxChegou.Text = "Sim - Enviou a cobrança";
            }
            else
            {
                ckxChegou.Text = "Não - Enviou a cobrança";
            }
        }

        private void ckxDespesaPublica_Click(object sender, EventArgs e)
        {
            if (ckxDespesaPublica.Checked == true)
            {
                ckxDespesaPublica.Text = "Sim - Conferido";
            }
            else
            {
                ckxDespesaPublica.Text = "Não - Conferido";
            }
        }

        private void btnIdDocumento_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdDocumento.Name;
            frmDocFiscalVis frmDocFiscalVis = new frmDocFiscalVis();
            frmDocFiscalVis.Init(idDocumento);

            clsFormHelper.AbrirForm(this.MdiParent, frmDocFiscalVis, clsInfo.conexaosqldados);
        }

        private void btnIdCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCliente.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idCliente);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnIdHistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdHistorico.Name;
            frmHistoricosPes frmHistoricosPes = new frmHistoricosPes();
            frmHistoricosPes.Init(clsInfo.conexaosqlbanco, idHistorico);

            clsFormHelper.AbrirForm(this.MdiParent, frmHistoricosPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdCentroCusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCentroCusto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", idCentrocusto, "Centro de Custos");

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
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", idFormapagto, "Situação de Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdNotaFiscal_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdNotaFiscal.Name;
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
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTITULO", idSitBanco, "Situação de Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void calcular()
        {
            //Colocamos numa função financeiro
            DuplicataInfo DuplicataInfo;
            DuplicataInfo = clsFinanceiro.CalcularDuplicata("Receber", tbxVencimento.Text, tbxDataBaixa.Text,
                                                                    clsParser.DecimalParse(tbxValor.Text), +
                                                                    clsParser.DecimalParse(tbxValorDesconto.Text), +
                                                                    clsParser.DecimalParse(tbxValorJuros.Text), +
                                                                    clsParser.DecimalParse(tbxValorMulta.Text), +
                                                                    clsParser.DecimalParse(tbxValorCredito.Text), 
                                                                    atevencimento);
            

            tbxValorLiquido.Text = DuplicataInfo.valorliquido.ToString("N2");
            tbxMulta.Text = DuplicataInfo.multas.ToString("N2");
            tbxJuros.Text = DuplicataInfo.juros.ToString("N2");
            tbxDesconto.Text = DuplicataInfo.descontos.ToString("N2");
            tbxSaldo.Text = ((clsParser.DecimalParse(tbxValor.Text) + clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text)) - clsParser.DecimalParse(tbxDesconto.Text)).ToString("N2");
            totaldias = DuplicataInfo.ntotaldias;
            //tbxDiferenca.Text = ((clsParser.DecimalParse(tbxSaldo.Text) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");
            tbxDiferenca.Text = (((clsParser.DecimalParse(tbxSaldo.Text) + clsParser.DecimalParse(tbxValorCredito.Text)) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");

            Receber01Grid();
        }

        private void CalcularPagando()
        {
            //calcular o total que esta pagando apos ter excluido 1 linha
            tbxPagando.Text = "0";

            for (int x = 0; x < dtReceber01.Rows.Count; x++)
            {
                if (dtReceber01.Rows[x].RowState != DataRowState.Deleted &&
                    dtReceber01.Rows[x].RowState != DataRowState.Detached)
                {
                    if (dtReceber01.Rows[x]["CODCOBRANCA"] != null && 
                        dtReceber01.Rows[x]["CODCOBRANCA"].ToString().Length >= 2 &&
                        dtReceber01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) != "08")
                    {
                        if (dtReceber01.Rows[x]["DEBCRED"].ToString() == "C")
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                        else
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                    }
                }
            }

            tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");
            tbxSaldo.Text = ((clsParser.DecimalParse(tbxValor.Text) + clsParser.DecimalParse(tbxMulta.Text) + clsParser.DecimalParse(tbxJuros.Text)) - clsParser.DecimalParse(tbxDesconto.Text)).ToString("N2");
            /////tbxDiferenca.Text = ((clsParser.DecimalParse(tbxSaldo.Text) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");
            tbxDiferenca.Text = (((clsParser.DecimalParse(tbxSaldo.Text) + clsParser.DecimalParse(tbxValorCredito.Text)) - (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(tbxDevolucao.Text)))).ToString("N2");

            if (clsParser.DecimalParse(tbxRecebido.Text) > 0)
            {
                Decimal ValorPagtoParcial = valorPrincipalDuplicata - ValorPrincipal;
                if (ValorPagtoParcial > 0)
                {  // colocar como valor principal agora
                    for (int x = 0; x < dtReceber01.Rows.Count; x++)
                    {
                        if (dtReceber01.Rows[x]["CODCOBRANCA"].ToString().PadRight(2, ' ').Substring(0, 2) == "00")
                        {
                            //ValorPagtoParcial = ValorPagtoParcial - ValorDescontoPago;

                            dtReceber01.Select("Posicao=" + dtReceber01.Rows[x]["POSICAO"].ToString())[0]["VALOR"] = ValorPagtoParcial;
                        }
                    }
                    // somar novamente para não haver problemas com o saldo
                    tbxPagando.Text = "0";

                    for (int x = 0; x < dtReceber01.Rows.Count; x++)
                    {
                        if (dtReceber01.Rows[x].RowState != DataRowState.Deleted &&
                            dtReceber01.Rows[x].RowState != DataRowState.Detached)
                        {
                            if (dtReceber01.Rows[x]["CODCOBRANCA"].ToString().PadRight(2, ' ').Substring(0, 2) != "08")
                            {
                                if (dtReceber01.Rows[x]["DEBCRED"].ToString() == "C")
                                {
                                    tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
                                }
                                else
                                {
                                    tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
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

        private void Receber01Grid()
        {
            SqlDataAdapter sda;
            sda = new SqlDataAdapter("select * from Receber01 where idduplicata = " + id, clsInfo.conexaosqldados);
            dtReceber01 = new DataTable();
            sda.Fill(dtReceber01);
            // Confirma se o valor da devolução foi lançado direto pela nota fiscal
            if (clsParser.DecimalParse(tbxDevolucao.Text) > 0)
            {
                if (dtReceber01.Rows.Count == 0)
                {  // não foi feito lançamento tem que incluir aqui a devolução.
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    scn.Open();
                    scd = new SqlCommand("insert into Receber01 (" +
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
                    scd.Parameters.Add("@DEBCRED", SqlDbType.NVarChar).Value = "D";
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
            dtReceber01.Columns.Add(colunaPosicao);

            DataColumn colunaCodcobranca = new DataColumn("codcobranca", Type.GetType("System.String"));
            dtReceber01.Columns.Add(colunaCodcobranca);

            DataColumn colunaCodhistorico = new DataColumn("codhistorico", Type.GetType("System.String"));
            dtReceber01.Columns.Add(colunaCodhistorico);

            DataColumn colunaHistorico = new DataColumn("historico", Type.GetType("System.String"));
            dtReceber01.Columns.Add(colunaHistorico);

            DataColumn colunaCentrocusto = new DataColumn("centrocusto", Type.GetType("System.String"));
            dtReceber01.Columns.Add(colunaCentrocusto);

            DataColumn colunaContacontabil = new DataColumn("contacontabil", Type.GetType("System.String"));
            dtReceber01.Columns.Add(colunaContacontabil);


            dgvReceber01.DataSource = dtReceber01;
            clsGridHelper.MontaGrid2(dgvReceber01, dtReceber01Colunas, true);
            clsGridHelper.FontGrid(dgvReceber01, 7);
            dgvReceber01.Columns["DATAENVIO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvReceber01.Columns["DATAOK"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvReceber01.Columns["VALOR"].DefaultCellStyle.Format = "N2";
            dgvReceber01.Columns["VALORCOMISSAO"].DefaultCellStyle.Format = "N2";
            dgvReceber01.Columns["VALORCOMISSAOGER"].DefaultCellStyle.Format = "N2";
            dgvReceber01.Columns["VALORCOMISSAOSUP"].DefaultCellStyle.Format = "N2";

            if (clsParser.DecimalParse(tbxValor.Text) > 0)
            {
                DataRow rowPrincipal = dtReceber01.NewRow();
                rowPrincipal["DATAENVIO"] = DateTime.Parse(tbxDataBaixa.Text); ;
                rowPrincipal["DATAOK"] = DateTime.Parse(tbxDataCredito.Text); ;
                // pegar o id da situacaocobrancacod

                rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO =  '00'" , "0"));
                //rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "00".ToString()));
                if (clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()) > 0)
                {
                    rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO + ' ' + NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "");
                    //rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                    //rowPrincipal["CODCOBRANCA"] = rowPrincipal["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                    
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
                    rowPrincipal["DEBCRED"] = "C";
                    rowPrincipal["MOTIVO"] = "00";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        rowPrincipal["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "0"));
                        //rowPrincipal["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOReceber", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));
                        rowPrincipal["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + clsParser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString()), "");
                        //rowPrincipal["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString())).Trim();
                        rowPrincipal["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "0"));
                        //rowPrincipal["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOReceber", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));
                        rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString()), "");
                        //rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString())).Trim();
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
                    //rowPrincipal["VALORCOMISSAO"] = clsParser.DecimalParse(tbxValorComissao.Text).ToString("N2");
                    //rowPrincipal["VALORCOMISSAOGER"] = clsParser.DecimalParse(tbxValorComissaoGer.Text).ToString("N2");
                    //rowPrincipal["VALORCOMISSAOSUP"] = clsParser.DecimalParse(tbxValorComissaoSup.Text).ToString("N2");
                }
                else
                {
                    MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (00-Valor Titulo) ");
                }

                dtReceber01.Rows.Add(rowPrincipal);
            }
            if (clsParser.DecimalParse(tbxMulta.Text) > 0)
            { // multa
                DataRow rowMulta = dtReceber01.NewRow();
                rowMulta["DATAENVIO"] = DateTime.Parse(tbxDataBaixa.Text); ;
                rowMulta["DATAOK"] = DateTime.Parse(tbxDataBaixa.Text);
                // pegar o id da situacaocobrancacod

                rowMulta["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO = '02'", ""));
                //rowMulta["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "02".ToString()));
                if (clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString()) > 0)
                {
                    rowMulta["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO + ' - ' + NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString()), "");
                    //rowMulta["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString())).Trim();
                    //rowMulta["CODCOBRANCA"] = rowMulta["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString())).Trim();
                    rowMulta["IDCOBRANCAHIS"] = 0;
                    rowMulta["CODHISTORICO"] = "";
                    rowMulta["VALOR"] = clsParser.DecimalParse(tbxValorMulta.Text).ToString();
                    rowMulta["DEBCRED"] = "C";
                    rowMulta["MOTIVO"] = "00";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        rowMulta["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString()), ""));
                        //rowMulta["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOReceber", "ID", clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString())));

                        rowMulta["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + clsParser.Int32Parse(rowMulta["IDHISTORICO"].ToString()), "");
                        //rowMulta["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowMulta["IDHISTORICO"].ToString())).Trim(); ;

                        rowMulta["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString()), ""));
                        //rowMulta["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOReceber", "ID", clsParser.Int32Parse(rowMulta["IDCOBRANCACOD"].ToString())));

                        rowMulta["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowMulta["IDCENTROCUSTO"].ToString()), "");
                        //rowMulta["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowMulta["IDCENTROCUSTO"].ToString())).Trim();
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
                dtReceber01.Rows.Add(rowMulta);
            }

            if (clsParser.DecimalParse(tbxJuros.Text) > 0)
            { // juros
                DataRow rowJuros = dtReceber01.NewRow();
                rowJuros["DATAENVIO"] = DateTime.Parse(tbxDataBaixa.Text); ;
                rowJuros["DATAOK"] = DateTime.Parse(tbxDataCredito.Text); ;
                
                rowJuros["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO = '03'", ""));
                //rowJuros["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "03".ToString()));
                if (clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString()) > 0)
                {
                    rowJuros["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO + ' ' + NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString()), "") + " " + totaldias.ToString() + " dias"; 
                    //rowJuros["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString())).Trim();
                    //rowJuros["CODCOBRANCA"] = rowJuros["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString())).Trim();
                    //rowJuros["CODCOBRANCA"] = rowJuros["CODCOBRANCA"] + " " + totaldias.ToString() + " dias";
                    rowJuros["IDCOBRANCAHIS"] = 0;
                    rowJuros["CODHISTORICO"] = "";
                    rowJuros["VALOR"] = clsParser.DecimalParse(tbxJuros.Text).ToString();
                    rowJuros["DEBCRED"] = "C";
                    rowJuros["MOTIVO"] = "00";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        rowJuros["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString()), "0"));
                        //rowJuros["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOReceber", "ID", clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString())));
                        
                        rowJuros["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + clsParser.Int32Parse(rowJuros["IDHISTORICO"].ToString()), "");
                        //rowJuros["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowJuros["IDHISTORICO"].ToString())).Trim(); ;

                        rowJuros["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString()), "0"));
                        //rowJuros["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOReceber", "ID", clsParser.Int32Parse(rowJuros["IDCOBRANCACOD"].ToString())));

                        rowJuros["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowJuros["IDCENTROCUSTO"].ToString()), "");
                        //rowJuros["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowJuros["IDCENTROCUSTO"].ToString())).Trim();
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
                dtReceber01.Rows.Add(rowJuros);

            }


            if (clsParser.DecimalParse(tbxDesconto.Text) > 0)
            {
                DataRow rowDesconto = dtReceber01.NewRow();
                rowDesconto["DATAENVIO"] = DateTime.Parse(tbxDataBaixa.Text); ;
                rowDesconto["DATAOK"] = DateTime.Parse(tbxDataCredito.Text); ;

                rowDesconto["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO = '08'", "0"));
                //rowDesconto["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "08".ToString()));
                if (clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString()) > 0)
                {
                    rowDesconto["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO + ' '+ NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString()), "").Trim();
                    //rowDesconto["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString())).Trim();
                    //rowDesconto["CODCOBRANCA"] = rowDesconto["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString())).Trim();
                    rowDesconto["IDCOBRANCAHIS"] = 0;
                    rowDesconto["CODHISTORICO"] = "";
                    rowDesconto["VALOR"] = clsParser.DecimalParse(tbxDesconto.Text);
                    rowDesconto["DEBCRED"] = "D";
                    rowDesconto["MOTIVO"] = "00";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        rowDesconto["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString()), "0"));
                        //rowDesconto["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOReceber", "ID", clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString())));

                        rowDesconto["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + clsParser.Int32Parse(rowDesconto["IDHISTORICO"].ToString()), "");
                        //rowDesconto["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowDesconto["IDHISTORICO"].ToString())).Trim(); ;

                        rowDesconto["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString()), "0"));
                        //rowDesconto["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOReceber", "ID", clsParser.Int32Parse(rowDesconto["IDCOBRANCACOD"].ToString())));

                        rowDesconto["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowDesconto["IDCENTROCUSTO"].ToString()), "");
                        //rowDesconto["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowDesconto["IDCENTROCUSTO"].ToString())).Trim();
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
                dtReceber01.Rows.Add(rowDesconto);
            }
            if (clsParser.DecimalParse(tbxValorCredito.Text) > 0)
            { // valor de credito / arredondamento
                DataRow rowCredito = dtReceber01.NewRow();
                rowCredito["DATAENVIO"] = DateTime.Parse(tbxDataBaixa.Text); ;
                rowCredito["DATAOK"] = DateTime.Parse(tbxDataCredito.Text); ;
                // pegar o id da situacaocobrancacod

                rowCredito["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO = '09'", "0"));
                //rowCredito["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "09".ToString()));
                if (clsParser.Int32Parse(rowCredito["IDCOBRANCACOD"].ToString()) > 0)
                {
                    rowCredito["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO + ' ' + NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowCredito["IDCOBRANCACOD"].ToString()), "");
                    //rowCredito["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowCredito["IDCOBRANCACOD"].ToString())).Trim();
                    //rowCredito["CODCOBRANCA"] = rowCredito["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowCredito["IDCOBRANCACOD"].ToString())).Trim();
                    
                    rowCredito["IDCOBRANCAHIS"] = 0;
                    rowCredito["CODHISTORICO"] = "";
                    rowCredito["VALOR"] = clsParser.DecimalParse(tbxValorCredito.Text).ToString();
                    rowCredito["DEBCRED"] = "C";
                    rowCredito["MOTIVO"] = "00";
                    if (clsInfo.zbaixarcomhistorico == "CX")
                    {
                        rowCredito["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowCredito["IDCOBRANCACOD"].ToString()), "0"));
                        //rowCredito["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOReceber", "ID", clsParser.Int32Parse(rowCredito["IDCOBRANCACOD"].ToString())));

                        rowCredito["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + clsParser.Int32Parse(rowCredito["IDHISTORICO"].ToString()), "");
                        //rowCredito["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowCredito["IDHISTORICO"].ToString())).Trim(); ;

                        rowCredito["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOReceber from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowCredito["IDCOBRANCACOD"].ToString()), "0"));
                        //rowCredito["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOReceber", "ID", clsParser.Int32Parse(rowCredito["IDCOBRANCACOD"].ToString())));

                        rowCredito["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowCredito["IDCENTROCUSTO"].ToString()), "");
                        //rowCredito["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowCredito["IDCENTROCUSTO"].ToString())).Trim();
                    }
                    else
                    {
                        rowCredito["IDHISTORICO"] = idHistorico;
                        rowCredito["HISTORICO"] = tbxHistorico.Text;
                        rowCredito["IDCENTROCUSTO"] = idCentrocusto;
                        rowCredito["CENTROCUSTO"] = tbxCentroCusto.Text;
                    }
                    rowCredito["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                    rowCredito["CONTACONTABIL"] = "00";
                    rowCredito["VALORCOMISSAO"] = 0;
                    rowCredito["VALORCOMISSAOGER"] = 0;
                    rowCredito["VALORCOMISSAOSUP"] = 0;
                }
                else
                {
                    MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (09 - Multa) ");
                }
                dtReceber01.Rows.Add(rowCredito);
            }



            tbxPagando.Text = "0";
            for (int x = 0; x < dtReceber01.Rows.Count; x++)
            {
                dtReceber01.Rows[x]["Posicao"] = x + 1;
                if (dtReceber01.Rows[x]["CODCOBRANCA"].ToString().Length >= 2 &&
                    dtReceber01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) == "00")
                {
                    valorPrincipalDuplicata = clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString());
                }
                if (dtReceber01.Rows[x]["CODCOBRANCA"].ToString().PadRight(2, ' ').Substring(0, 2) != "08")
                {
                    if (dtReceber01.Rows[x]["DEBCRED"].ToString() == "C")
                    {
                        tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
                    }
                    else
                    {
                        tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
                    }
                }
            }
            tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");
            dtReceber01.AcceptChanges();
        }
        private void tspCobAlterar_Click(object sender, EventArgs e)
        {
            DataRow row;
            posicao = clsParser.Int32Parse(dgvReceber01.CurrentRow.Cells["posicao"].Value.ToString());

            try
            {
                row = dtReceber01.Select("posicao=" + posicao)[0];
            }
            catch
            {
                MessageBox.Show("Não existe nenhuma linha selecionada.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tclReceber.SelectedTab = tabRegistro;
            gbxReceber.Enabled = false;
            gbxReceberBaixa.Visible = true;
            tbxDuplicata3.Text = tbxDuplicata2.Text = tbxDuplicata1.Text = tbxDuplicata.Text;
            tbxEmissao3.Text = tbxEmissao2.Text = tbxEmissao1.Text = tbxEmissao.Text;
            tbxPosicao3.Text = tbxPosicao2.Text = tbxPosicao1.Text = tbxPosicao.Text;
            tbxPosicaoFim3.Text = tbxPosicaoFim2.Text = tbxPosicaoFim1.Text = tbxPosicaoFim.Text;
            tbxClienteCognome3.Text = tbxClienteCognome2.Text = tbxClienteCognome1.Text = tbxClienteCognome.Text;
            tbxClienteTelefone3.Text = tbxClienteTelefone2.Text = tbxClienteTelefone1.Text = tbxClienteTelefone.Text;
            tbxClienteCGC3.Text = tbxClienteCGC2.Text = tbxClienteCGC1.Text = tbxClienteCGC.Text;

            // colocar os campos do grid selecionado / indicado
            receber01_idcobrancacod = clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString());
            receber01_idcobrancahis = clsParser.Int32Parse(row["IDCOBRANCAHIS"].ToString());
            receber01_idhistorico = clsParser.Int32Parse(row["IDHISTORICO"].ToString());
            receber01_idcentrocusto = clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString());
            receber01_idcodigoctabil = clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString());

            tbxReceber01CobrancaCodCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + receber01_idcobrancacod, "");
            //tbxReceber01CobrancaCodCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", receber01_idcobrancacod);
            tbxReceber01CobrancaCodNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + receber01_idcobrancacod, "");
            //tbxReceber01CobrancaCodNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", receber01_idcobrancacod);

            tbxReceber01CobrancaCod1Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + receber01_idcobrancahis, "");
            //tbxReceber01CobrancaCod1Codigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", receber01_idcobrancahis);
            tbxReceber01CobrancaCod1Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from SITUACAOCOBRANCACOD where ID = " + receber01_idcobrancahis, "");
            //tbxReceber01CobrancaCod1Nome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", receber01_idcobrancahis);

            tbxReceber01Valor.Text = clsParser.DecimalParse(row["VALOR"].ToString()).ToString("N2");
            tbxReceber01DC.Text = row["DEBCRED"].ToString();

            tbxReceber01HistCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + receber01_idhistorico, "");
            //tbxReceber01HistCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", receber01_idhistorico);
            tbxReceber01HistNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from HISTORICOS where ID = " + receber01_idhistorico, "");
            //tbxReceber01HistNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", receber01_idhistorico);

            tbxReceber01CCCod.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + receber01_idcentrocusto, "");
            //tbxReceber01CCCod.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", receber01_idcentrocusto);
            tbxReceber01CCNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from CENTROCUSTOS where ID = " + receber01_idcentrocusto, "");
            //tbxReceber01CCNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", receber01_idcentrocusto);

            tbxReceber01CtabilCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID = " + receber01_idcodigoctabil, "");
            //tbxReceber01CtabilCodigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", receber01_idcodigoctabil);
            tbxReceber01CtabilCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CONTACONTABIL where ID = " + receber01_idcodigoctabil, "");
            //tbxReceber01CtabilNome.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", receber01_idcodigoctabil);

            tbxReceber01Comissao.Text = clsParser.DecimalParse(row["VALORCOMISSAO"].ToString()).ToString("N2");
            tbxReceber01ComissaoGer.Text = clsParser.DecimalParse(row["VALORCOMISSAOGER"].ToString()).ToString("N2");
            tbxReceber01ComissaoSup.Text = clsParser.DecimalParse(row["VALORCOMISSAOSUP"].ToString()).ToString("N2");

            tbxReceber01CobrancaCodCodigo.Select();
            tbxReceber01CobrancaCodCodigo.SelectAll();
        }

        private void tspCobExcluir_Click(object sender, EventArgs e)
        {
            resultado = MessageBox.Show("Deseja aReceber/excluir este item ? ( " + dgvReceber01.CurrentRow.Cells["codcobranca"].Value.ToString() + " )", "Aplisoft",
                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1);

            if (resultado == DialogResult.Yes)
            {
                dtReceber01.Select("Posicao=" + dgvReceber01.CurrentRow.Cells["posicao"].Value.ToString())[0].Delete();


                //calcular o total que esta pagando apos ter excluido 1 linha

                tbxPagando.Text = "0";

                for (int x = 0; x < dtReceber01.Rows.Count; x++)
                {
                    if (dtReceber01.Rows[x].RowState != DataRowState.Deleted &&
                        dtReceber01.Rows[x].RowState != DataRowState.Detached)
                    {
                        if (dtReceber01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) != "08")
                        {
                            if (dtReceber01.Rows[x]["DEBCRED"].ToString() == "C")
                            {
                                tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
                            }
                            else
                            {
                                tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
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
            if (clsInfo.zbaixatemnotafiscal != "S")
            {
                if (tbxDocumento.Text == "CTO")
                {
                    if (clsParser.Int64Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM CONTRATO where id=" + idNotafiscal)) == 0)
                    {
                        MessageBox.Show("Você não de baixar um contrato previsto de pagamento !!!" + Environment.NewLine + " " +
                       " 1 - Apague esta Duplicata " + Environment.NewLine + " " +
                       " 2 - Lance em Contrato [CTO] " + Environment.NewLine + " " +
                       " 3 - Ai sim poderá baixar ... ");
                        oK = false;
                    }
                    else
                    {
                        oK = true;
                    }
                }
                else if (tbxDocumento.Text.PadRight(3, ' ').Substring(0, 3) == "NFV" || tbxDocumento.Text.IndexOf("NFV") != -1)
                {
                    if (clsParser.Int64Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM NFVENDA where id=" + idNotafiscal)) == 0)
                    {
                        MessageBox.Show("Você não pode baixar uma duplicata de previsão de pagamento !!!" + Environment.NewLine + " " +
                                       " 1 - Apague esta Duplicata " + Environment.NewLine + " " +
                                       " 2 - Lance em Nota Fiscal de Venda [NFV] " + Environment.NewLine + " " +
                                       " 3 - Ai sim poderá baixar ... ");
                        oK = false;
                    }
                    else
                    {
                        oK = true;
                    }
                }
                else if (tbxDocumento.Text.PadRight(3, ' ').Substring(0, 3) == "PED" || tbxDocumento.Text.IndexOf("PED") != -1)
                {
                    if (clsParser.Int64Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM PEDIDO where id=" + idNotafiscal)) == 0)
                    {
                        MessageBox.Show("Você não pode baixar uma duplicata de previsão de pagamento !!!" + Environment.NewLine + " " +
                                       " 1 - Apague esta Duplicata " + Environment.NewLine + " " +
                                       " 2 - Lance em Pedido de Venda [PED] " + Environment.NewLine + " " +
                                       " 3 - Ai sim poderá baixar ... ");
                        oK = false;
                    }
                    else
                    {
                        oK = true;
                    }
                }

                else if (tbxDocumento.Text.PadRight(3, ' ').Substring(0, 3) == "OST" || tbxDocumento.Text.IndexOf("OS") != -1)
                {
                    oK = true;
                }
                else
                {
                    MessageBox.Show("Documento não disponivel para baixa !!!  [[" + tbxDocumento.Text + "]]");
                    oK = false;
                }
            }
            else
            {  // S = sim pode baixar sem nota fiscal
                oK = true;
            }
            ReceberSalvar();
            if (oK == true)
            {
                if (clsParser.DecimalParse(tbxDiferencaPagamento.Text) < 0)
                {
                    MessageBox.Show("" + clsInfo.zusuario + " você esta pagando ou recebendo valor indevido (Saldo Negativo)" + Environment.NewLine + "" +
                                    "Saldo da Duplicata : " + tbxDiferencaPagamento.Text + " ? ");
                }
                else
                {
                    tclReceber.SelectedTab = tabBaixa;
                    gbxReceber.Enabled = false;
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
                    tbxBaixaNominal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from CLIENTE where ID = " + idCliente, "");
                    //tbxBaixaNominal.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "NOME", "ID", idCliente).Trim();
                    //tbxBaixaValorComissao.Text = tbxValorComissao.Text;
                    //tbxBaixaValorComissaoGer.Text = tbxValorComissaoGer.Text;
                    //tbxBaixaValorComissaoSup.Text = tbxValorComissaoSup.Text;
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
                    clsReceberObservaInfo = new clsReceberObservaInfo();
                    ReceberObservaFillInfo(clsReceberObservaInfo);
                    ReceberObservaFillInfoToGrid(clsReceberObservaInfo);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }


                tabReceber.Select();
                gbxObserva.Visible = false;
                gbxReceber.Enabled = true;
                tclReceber.SelectedTab = tabReceber;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





        }

        private void tspRetornarObserva_Click(object sender, EventArgs e)
        {
            tabReceber.Select();
            gbxObserva.Visible = false;
            gbxReceber.Enabled = true;
            tclReceber.SelectedTab = tabReceber;

        }

        private void tspSalvarReceber01_Click(object sender, EventArgs e)
        {
            DataRow row;
            try
            {
                row = dtReceber01.Select("posicao=" + posicao)[0];
            }
            catch
            {
                MessageBox.Show("Posição da tabela não foi encontrada.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            row["IDCOBRANCACOD"] = receber01_idcobrancacod;
            row["IDCOBRANCAHIS"] = receber01_idcobrancahis;
            row["IDHISTORICO"] = receber01_idhistorico;
            row["IDCENTROCUSTO"] = receber01_idcentrocusto;
            row["IDCODIGOCTABIL"] = receber01_idcodigoctabil;

            row["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO + ' ' + NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString()), "");
            //row["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString())).Trim();
            //row["CODCOBRANCA"] = row["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(row["IDCOBRANCACOD"].ToString())).Trim();
            row["CODHISTORICO"] = tbxReceber01CobrancaCod1Codigo.Text;
            row["HISTORICO"] = tbxReceber01HistCod.Text;
            row["CENTROCUSTO"] = tbxReceber01CCCod.Text;
            row["CONTACONTABIL"] = tbxContaContabil.Text;
            row["VALOR"] = clsParser.DecimalParse(tbxReceber01Valor.Text);
            row["DEBCRED"] = tbxReceber01DC.Text;
            row["VALORCOMISSAO"] = clsParser.DecimalParse(tbxReceber01Comissao.Text);
            row["VALORCOMISSAOGER"] = clsParser.DecimalParse(tbxReceber01ComissaoGer.Text);
            row["VALORCOMISSAOSUP"] = clsParser.DecimalParse(tbxReceber01ComissaoSup.Text);

            dtReceber01.AcceptChanges();

            //calcular o total que esta pagando apos ter excluido 1 linha

            tbxPagando.Text = "0";

            for (int x = 0; x < dtReceber01.Rows.Count; x++)
            {
                if (dtReceber01.Rows[x].RowState != DataRowState.Deleted &&
                    dtReceber01.Rows[x].RowState != DataRowState.Detached)
                {
                    if (dtReceber01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) != "08")
                    {
                        if (dtReceber01.Rows[x]["DEBCRED"].ToString() == "C")
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) + clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                        else
                        {
                            tbxPagando.Text = (clsParser.DecimalParse(tbxPagando.Text) - clsParser.DecimalParse(dtReceber01.Rows[x]["VALOR"].ToString())).ToString("N2");
                        }
                    }
                }
            }
            tbxDiferencaPagamento.Text = (clsParser.DecimalParse(tbxDiferenca.Text) - clsParser.DecimalParse(tbxPagando.Text)).ToString("N2");

            tabReceber.Select();
            gbxReceberBaixa.Visible = false;
            gbxReceber.Enabled = true;
            tclReceber.SelectedTab = tabReceber;
        }

        private void tspPrimeiroReceber01_Click(object sender, EventArgs e)
        {

        }

        private void tspAnteriorReceber01_Click(object sender, EventArgs e)
        {

        }

        private void tspProximoReceber01_Click(object sender, EventArgs e)
        {

        }

        private void tspUltimoReceber01_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornarReceber01_Click(object sender, EventArgs e)
        {
            tabReceber.Select();
            gbxReceberBaixa.Visible = false;
            gbxReceber.Enabled = true;
            tclReceber.SelectedTab = tabReceber;
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
                                        TimeSpan Compensar;

                                        NroLote = "PAG" + DateTime.Now.ToString("yyyyMMddHHmmss") + clsParser.Int32Parse(tbxBaixaBoletoNro.Text).ToString("0000000000") + clsInfo.zusuarioid.ToString("0000000");
                                        Compensar = datacredito.Subtract(databaixa);
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
                                        for (int x = 0; x < dtReceber01.Rows.Count; x++)
                                        {  // só deixei porque no programa anterior não cadastravam o desconto (08)
                                            // neste estou lançando tudo  
                                            row = dtReceber01.Rows[x];

                                            if (row.RowState == DataRowState.Deleted ||
                                                row.RowState == DataRowState.Detached)
                                            {
                                                continue;
                                            }

                                            switch (row["codcobranca"].ToString().PadRight(2,' ').Substring(0, 2))
                                            {
                                                case "00":
                                                    ValorCheque = clsParser.DecimalParse(tbxPagando.Text);
                                                    break;
                                                case "08":
                                                    // quando for desconto incluir como principal para bater os calculos 
                                                    // primeiro verifique ao historico do banco pagar sera 999999
                                                    Int32 idHistoricoEsp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO = '999999'", "0"));
                                                    //Int32 idHistoricoEsp = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ID", "CODIGO", "999999"));
                                                    if (idHistoricoEsp == 0)
                                                    { // criar a conta no aplibank
                                                        idHistoricoEsp = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO, NOME, ATIVO", "'999999', 'Pagar Desconto Concedido', 'S'");
                                                    }
                                                    DataRow rowPrincipal = dtReceber01.NewRow();
                                                    rowPrincipal["DATAENVIO"] = DateTime.Now;
                                                    rowPrincipal["DATAOK"] = DateTime.Now;
                                                    if (clsInfo.zbaixarcomhistorico == "CX")
                                                    {
                                                        // pegar o id da situacaocobrancacod

                                                        rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO = '01'", "0"));
                                                        //rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "01".ToString()));
                                                        if (clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()) > 0)
                                                        {
                                                            rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO + ' ' + NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "");
                                                            //rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                                                            //rowPrincipal["CODCOBRANCA"] = rowPrincipal["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                                                            rowPrincipal["IDCOBRANCAHIS"] = "0";
                                                            rowPrincipal["CODHISTORICO"] = "";
                                                            rowPrincipal["VALOR"] = clsParser.DecimalParse(tbxDesconto.Text);
                                                            rowPrincipal["DEBCRED"] = "C";
                                                            rowPrincipal["MOTIVO"] = "01";
                                                            rowPrincipal["IDHISTORICO"] = idHistoricoEsp; // clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));
                                                            rowPrincipal["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + idHistoricoEsp, "");
                                                            //rowPrincipal["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idHistoricoEsp); //Parser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString())).Trim();

                                                            rowPrincipal["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCENTROCUSTOPAGAR from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "0"));
                                                            //rowPrincipal["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));

                                                            rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString()), "");
                                                            //rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString())).Trim();
                                                            rowPrincipal["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                                                            rowPrincipal["CONTACONTABIL"] = "";
                                                            rowPrincipal["VALORCOMISSAO"] = clsParser.DecimalParse(tbxBaixaValorComissao.Text).ToString("N2");
                                                            rowPrincipal["VALORCOMISSAOGER"] = clsParser.DecimalParse(tbxBaixaValorComissaoGer.Text).ToString("N2");
                                                            rowPrincipal["VALORCOMISSAOSUP"] = clsParser.DecimalParse(tbxBaixaValorComissaoSup.Text).ToString("N2");
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (00-Valor Titulo) ");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // pegar o id da situacaocobrancacod

                                                        rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOCOBRANCACOD where CODIGO = '01'", ""));
                                                        //rowPrincipal["IDCOBRANCACOD"] = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "01".ToString()));
                                                        if (clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()) > 0)
                                                        {
                                                            rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO + ' ' + NOME from SITUACAOCOBRANCACOD where ID = " + clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString()), "").Trim();
                                                            //rowPrincipal["CODCOBRANCA"] = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                                                            //rowPrincipal["CODCOBRANCA"] = rowPrincipal["CODCOBRANCA"] + " " + Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "NOME", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())).Trim();
                                                            rowPrincipal["IDCOBRANCAHIS"] = "0";
                                                            rowPrincipal["CODHISTORICO"] = "";
                                                            rowPrincipal["VALOR"] = clsParser.DecimalParse(tbxDesconto.Text);
                                                            rowPrincipal["DEBCRED"] = "C";
                                                            rowPrincipal["MOTIVO"] = "01";
                                                            rowPrincipal["IDHISTORICO"] = idHistoricoEsp; // clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDHISTORICOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));

                                                            rowPrincipal["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID = " + idHistoricoEsp, "");
                                                            //rowPrincipal["HISTORICO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idHistoricoEsp); //Parser.Int32Parse(rowPrincipal["IDHISTORICO"].ToString())).Trim();

                                                            rowPrincipal["IDCENTROCUSTO"] = idCentrocustoBaixa;  // clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "IDCENTROCUSTOPAGAR", "ID", clsParser.Int32Parse(rowPrincipal["IDCOBRANCACOD"].ToString())));

                                                            rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString()), "").Trim();
                                                            //rowPrincipal["CENTROCUSTO"] = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowPrincipal["IDCENTROCUSTO"].ToString())).Trim();
                                                            rowPrincipal["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                                                            rowPrincipal["CONTACONTABIL"] = "";
                                                            rowPrincipal["VALORCOMISSAO"] = clsParser.DecimalParse(tbxBaixaValorComissao.Text).ToString("N2");
                                                            rowPrincipal["VALORCOMISSAOGER"] = clsParser.DecimalParse(tbxBaixaValorComissaoGer.Text).ToString("N2");
                                                            rowPrincipal["VALORCOMISSAOSUP"] = clsParser.DecimalParse(tbxBaixaValorComissaoSup.Text).ToString("N2");
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Favor consultar Aplisoft - Codigo de Cobrança Inexistente - (00-Valor Titulo) ");
                                                        }
                                                    }
                                                    dtReceber01.Rows.Add(rowPrincipal);
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
                                            // Colocar o Complemento na Baixa
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

                                            clsFinanceiro.BaixarBanco("RECEBER", id, NroLote, tbxBaixaBoletoNro.Text, idBancointBaixa, clsParser.Int32Parse(row["IDHISTORICO"].ToString()),
                                                                      clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString()), clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString()),
                                                                      DateTime.Parse(tbxDataBaixa2.Text), DateTime.Parse(tbxDataCredito.Text),
                                                                      clsParser.DecimalParse(row["VALOR"].ToString()),
                                                                      row["codcobranca"].ToString(), row["DEBCRED"].ToString(), tbxObserva.Text, tbxClienteCognome2.Text, tbxDocumento.Text,
                                                                      tbxNotaDoc.Text, tbxNFVendaNumero.Text, clsParser.Int32Parse(tbxPosicao.Text), clsParser.Int32Parse(tbxPosicaoFim.Text), tbxBaixaFormaPagto.Text.Substring(0, 2), clsInfo.zusuario, nrocheque, conferido, "UNICA");
                                        }
                                        // Transferir para o Contas Pagas
                                        for (int x = 0; x < dtReceber01.Rows.Count; x++)
                                        {  // só deixei porque no programa anterior não cadastravam o desconto (08)
                                            // neste estou lançando tudo  
                                            row = dtReceber01.Rows[x];

                                            if (row.RowState == DataRowState.Deleted ||
                                                row.RowState == DataRowState.Detached)
                                            {
                                                continue;
                                            }

                                            switch (row["codcobranca"].ToString().PadRight(2, ' ').Substring(0, 2))
                                            {
                                                case "00":
                                                    ValorCheque = clsParser.DecimalParse(tbxPagando.Text);
                                                    break;
                                                default:
                                                    ValorCheque = clsParser.DecimalParse(tbxPagando.Text);
                                                    break;
                                            }
                                            clsFinanceiro.TransferirPago("RECEBER", id, tbxDataBaixa2.Text,
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
                                        // Transferir as Observações do Contas a Receber para o Recebida
                                        clsFinanceiro.TransferirPagoObserva("RECEBER", id);

                                        if (clsInfo.zbaixatemnotafiscal != "S")
                                        {

                                            // Marcar na Nota Fiscal que foi Pago
                                            if (tbxDocumento.Text == "CTO")
                                            {
                                                clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "CONTRATORECEBER",
                                                                        "PAGOU = 'S'", "ID = " + idReceberNFV);

                                            }
                                            else if (tbxDocumento.Text == "NFV" || tbxDocumento.Text.IndexOf("NFV") != -1)
                                            {
                                                clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "NFVENDARECEBER",
                                                                        "PAGOU = 'S'", "ID = " + idReceberNFV);
                                            }
                                        else if (tbxDocumento.Text == "PED" || tbxDocumento.Text.IndexOf("PED") != -1)
                                        {
                                            clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "PEDIDORECEBER",
                                                                    "PAGOU = 'S'", "ID = " + idReceberNFV);
                                        }

                                        else if (tbxDocumento.Text == "OST" || tbxDocumento.Text.IndexOf("OS") != -1)
                                            {
                                                clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "ORDEMSERVICORECEBER",
                                                                        "PAGOU = 'S'", "ID = " + idReceberNFV);
                                            }

                                        }
                                        else
                                        {
                                            // Não precisa marcar em nenhum documento a baixa
                                        }
                                        // Excluir a Duplicata se foi Total
                                        if (clsParser.DecimalParse(tbxDiferencaPagamento.Text) == 0)
                                        {
                                            // Apagar observação do receber
                                            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
                                            SqlCommand scd = new SqlCommand("delete receberobserva where idduplicata=@id", scn);
                                            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                            scn.Open();
                                            scd.ExecuteNonQuery();
                                            scn.Close();
                                            // Apagar os itens do receber
                                            scn = new SqlConnection(clsInfo.conexaosqldados);
                                            scd = new SqlCommand("delete receber01 where idduplicata=@id", scn);
                                            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                            scn.Open();
                                            scd.ExecuteNonQuery();
                                            scn.Close();
                                            // Apagar o receber
                                            scn = new SqlConnection(clsInfo.conexaosqldados);
                                            scd = new SqlCommand("delete receber where id=@id", scn);
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
                    //tbxBaixaHistorico.Select();   desabilitei porque não leva o historico para o banco corretamente
                    //tbxBaixaHistorico.SelectAll();
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
            tabReceber.Select();
            gbxBaixar.Visible = false;
            gbxReceber.Enabled = true;
            tclReceber.SelectedTab = tabReceber;
        }


        private void tbxClienteTelefone2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBaixaIdHistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBaixaIdHistorico.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", idHistoricoBaixa, "Historicos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnBaixaIdCentroCusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBaixaIdCentroCusto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", idCentrocustoBaixa, "Centro de Custos");

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
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", idFormapagtoBaixa, "Situacao de Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnBaixaIdBancoInt_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnBaixaIdBancoInt.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBancointBaixa, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqldados);
        }

        private void dgvReceber01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspCobAlterar.PerformClick();
        }

        private void btnReceber01_idcobrancahis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnReceber01_idcobrancahis.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD1", receber01_idcobrancahis, "Situação de Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnReceber01_idhistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnReceber01_idhistorico.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", receber01_idhistorico, "Historicos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void btnReceber01_idcentrocusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnReceber01_idcentrocusto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", receber01_idcentrocusto, "Centro de Custos ");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void btnReceber01_idcodigoctabil_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnReceber01_idcodigoctabil.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONTACONTABIL", receber01_idcodigoctabil, "Contas Contabeis");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void bwrReceberObserva_RunWorkerAsync()
        {
            bwrReceberObserva = new BackgroundWorker();
            bwrReceberObserva.DoWork += new DoWorkEventHandler(bwrReceberObserva_DoWork);
            bwrReceberObserva.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrReceberObserva_RunWorkerCompleted);
            bwrReceberObserva.RunWorkerAsync();
        }

        private void bwrReceberObserva_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                String tabela = "ReceberOBSERVA ";
                String query = "ID, IDDUPLICATA, DATA, OBSERVAR, EMITENTE";
                String ordem = "DATA";
                String filtro = "IDDUPLICATA=" + id;

                dtReceberObserva = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, tabela, query, filtro, ordem);
                dtReceberObserva.Columns.Add("POSICAO", Type.GetType("System.Int32"));

                for (Int32 x = 0; x < dtReceberObserva.Rows.Count; x++)
                {
                    dtReceberObserva.Rows[x]["posicao"] = x + 1;
                }
                dtReceberObserva.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bwrReceberObserva_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvReceberObserva.DataSource = dtReceberObserva;

                clsGridHelper.MontaGrid(dgvReceberObserva,
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
                dgvReceberObserva.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
                clsGridHelper.FontGrid(dgvReceberObserva, 7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tspReceberObservaIncluir_Click(object sender, EventArgs e)
        {

            // incluir
            receberobserva_posicao = 0;
            ReceberObservaCarregar();
            /*
                        DataRow rowIncluir = dtReceberObserva.NewRow();
                        posicaoReceberObserva = dtReceberObserva.Rows.Count + 1;
                        rowIncluir["IDDUPLICATA"] = id;
                        rowIncluir["DATA"] = DateTime.Now;
                        rowIncluir["EMITENTE"] = clsInfo.zusuario;
                        rowIncluir["OBSERVAR"] = "";
                        rowIncluir["POSICAO"] = posicaoReceberObserva;
                        dtReceberObserva.Rows.Add(rowIncluir);
                        ReceberObservaGridCampos(posicaoReceberObserva);
                        tbxObservaObservar.Select();
                        tbxObservaObservar.SelectAll();
             */

        }
        private void tspReceberObservaAlterar_Click(object sender, EventArgs e)
        {
            if (dgvReceberObserva.CurrentRow != null)
            {
                receberobserva_posicao = Int32.Parse(dgvReceberObserva.CurrentRow.Cells["posicao"].Value.ToString());
                ReceberObservaCarregar();
            }
        }

        void ReceberObservaCarregar()
        {
            clsReceberObservaInfo = new clsReceberObservaInfo();
            clsReceberObservaInfoOld = new clsReceberObservaInfo();
            if (receberobserva_posicao == 0)
            {
                receberobserva_posicao = dtReceberObserva.Rows.Count + 1;


            }
            else
            {
                ReceberObservaGridToInfo(clsReceberObservaInfo, receberobserva_posicao);
            }
            ReceberObservaCampos(clsReceberObservaInfo);
            ReceberObservaFillInfo(clsReceberObservaInfoOld);


            tbxDuplicata3.Text = tbxDuplicata2.Text = tbxDuplicata1.Text = tbxDuplicata.Text;
            tbxEmissao3.Text = tbxEmissao2.Text = tbxEmissao1.Text = tbxEmissao.Text;
            tbxPosicao3.Text = tbxPosicao2.Text = tbxPosicao1.Text = tbxPosicao.Text;
            tbxPosicaoFim3.Text = tbxPosicaoFim2.Text = tbxPosicaoFim1.Text = tbxPosicaoFim.Text;
            tbxClienteCognome3.Text = tbxClienteCognome2.Text = tbxClienteCognome1.Text = tbxClienteCognome.Text;
            tbxClienteTelefone3.Text = tbxClienteTelefone2.Text = tbxClienteTelefone1.Text = tbxClienteTelefone.Text;
            tbxClienteCGC3.Text = tbxClienteCGC2.Text = tbxClienteCGC1.Text = tbxClienteCGC.Text;
            tbxDocumento3.Text = tbxDocumento2.Text = tbxDocumento1.Text = tbxDocumento.Text;
            tclReceber.SelectedTab = tabObserva;
            gbxReceber.Enabled = false;
            gbxObserva.Visible = true;

        }

        private void ReceberObservaGridToInfo(clsReceberObservaInfo info, Int32 posicao)
        {
            DataRow row = dtReceberObserva.Select("posicao = " + posicao)[0];

            info.id = Int32.Parse(row["id"].ToString());
            info.data = DateTime.Parse(row["data"].ToString());
            info.emitente = row["emitente"].ToString();
            info.idduplicata = id;
            info.observar = row["observar"].ToString();
        }
        private void ReceberObservaCampos(clsReceberObservaInfo info)
        {
            receberobserva_id = info.id;
            receberobserva_idduplicata = id;
            if (info.id == 0)
            {
                tbxObservaData.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
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

        private void ReceberObservaFillInfo(clsReceberObservaInfo info)
        {
            info.id = receberobserva_id;
            info.idduplicata = receberobserva_idduplicata;
            info.data = DateTime.Parse(tbxObservaData.Text);
            info.emitente = tbxObservaEmitente.Text;
            info.observar = tbxObservaObservar.Text;
        }

        void ReceberObservaFillInfoToGrid(clsReceberObservaInfo info)
        {
            DataRow row;
            DataRow[] rows = dtReceberObserva.Select("posicao = " + receberobserva_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtReceberObserva.NewRow();
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
                row["posicao"] = receberobserva_posicao;
                dtReceberObserva.Rows.Add(row);
            }
        }

        private void ReceberObservaGridCampos(Int32 posicao)
        {
            DataRow row = dtReceberObserva.Select("POSICAO=" + posicao)[0];
            tbxObservaData.Text = clsParser.SqlDateTimeParse(row["DATA"].ToString()).Value.ToString("dd/MM/yyyy");
            tbxObservaEmitente.Text = row["EMITENTE"].ToString();
            tbxObservaObservar.Text = row["OBSERVAR"].ToString();
        }


        private void dgvReceberObserva_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspReceberObservaAlterar.PerformClick();
        }
        private void SalvarReceberObserva()
        {
            String query;
            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
            SqlCommand scd;

            foreach (DataRow row in dtReceberObserva.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    if (clsParser.Int32Parse(row["ID"].ToString()) > 0)
                    {
                        // AReceber observação do Receber
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scd = new SqlCommand("delete Receberobserva where id=@id", scn);
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());
                        scn.Open();
                        scd.ExecuteNonQuery();
                        scn.Close();

                    }
                }
                else if (row.RowState == DataRowState.Added)
                {
                    // Incluir
                    query = "Insert Into ReceberOBSERVA (IDDUPLICATA, DATA, OBSERVAR, EMITENTE) VALUES (@IDDUPLICATA, @DATA, @OBSERVAR, @EMITENTE); SELECT SCOPE_IDENTITY()";
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
                    query = "UPDATE RECEBEROBSERVA SET IDDUPLICATA =@IDDUPLICATA, DATA=@DATA, OBSERVAR=@OBSERVAR, EMITENTE=@EMITENTE WHERE ID=@ID";
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
            DataTable dtRecebida = new DataTable();
            dtRecebida = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "RECEBIDA " +
                                "left join RECEBIDA01 on RECEBIDA01.IDDUPLICATA=RECEBIDA.ID  " +
                                "left join SITUACAOCOBRANCACOD on SITUACAOCOBRANCACOD.id=RECEBIDA01.IDCOBRANCACOD  " +
                                "left join SITUACAOCOBRANCACOD1 on SITUACAOCOBRANCACOD1.id=RECEBIDA01.IDCOBRANCAHIS  ",
                                "RECEBIDA01.ID, RECEBIDA01.IDDUPLICATA, RECEBIDA01.DATAENVIO, RECEBIDA01.DATAOK, " +
                                "SITUACAOCOBRANCACOD.CODIGO + '=' + SITUACAOCOBRANCACOD.NOME AS [COB], SITUACAOCOBRANCACOD1.CODIGO AS [HIS], " +
                                "RECEBIDA01.VALOR, RECEBIDA01.DEBCRED, RECEBIDA01.BAIXOUCOMO ",
                                "RECEBIDA.IDReceber = " + clsParser.Int32Parse(id.ToString()), "DATAENVIO");
            tbxRecebido.Text = "0";
            ValorPrincipal = 0;
            foreach (DataRow drRecebida in dtRecebida.Rows)
            { // Somar os Pagamentos
                if (drRecebida["DEBCRED"].ToString() == "C")
                {
                    tbxRecebido.Text = (clsParser.DecimalParse(tbxRecebido.Text) + clsParser.DecimalParse(drRecebida["VALOR"].ToString())).ToString("N2");
                    if (drRecebida["COB"].ToString().Substring(0, 2) == "00" || drRecebida["COB"].ToString().Substring(0, 2) == "01")
                    {
                        ValorPrincipal = ValorPrincipal + clsParser.DecimalParse(drRecebida["VALOR"].ToString());
                    }
                }
                else
                {
                    if (drRecebida["COB"].ToString().Substring(0, 2) == "08")
                    {
                        // não descontar 
                        // Separar o Valor Para finalizar o Pagamento
                        ValorDescontoPago = clsParser.DecimalParse(drRecebida["VALOR"].ToString());
                        // EXCLUIR DA MOVIMENTAÇÃO DO TITULO QUE GERA O TOTAL A Receber
                        for (int x = 0; x < dtReceber01.Rows.Count; x++)
                        {
                            if (dtReceber01.Rows[x]["CODCOBRANCA"].ToString().Substring(0, 2) == "08")
                            {
                                dtReceber01.Select("Posicao=" + dtReceber01.Rows[x]["POSICAO"].ToString())[0].Delete();
                                dtReceber01.AcceptChanges();
                            }
                        }
                    }
                    else
                    {
                        tbxRecebido.Text = (clsParser.DecimalParse(tbxRecebido.Text) - clsParser.DecimalParse(drRecebida["VALOR"].ToString())).ToString("N2");
                    }
                    if (drRecebida["COB"].ToString().Substring(0, 2) == "00" || drRecebida["COB"].ToString().Substring(0, 2) == "01")
                    {
                        ValorPrincipal = ValorPrincipal - clsParser.DecimalParse(drRecebida["VALOR"].ToString());
                    }
                }
            }
            // GRAVAR O VALOR PRINCIPAL COMO VALOR PAGO
            // DEVIDO AO QUE MOSTRO NA TELA PRINCIPAL EU NÃO COLOCO OS JUROS PAGO AQUI POIS AI PODERIA FICAR NEGATIVO
            // FICA ERRADO SE ELE PAGOU JUROS (VERIFICAR NO FUTURO)
            // tbxrecebido = valor pago ( ele soma tudo quando entra no form )
            // como não grava este campo ele clsVisualmente fica certo


            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("UPDATE RECEBER SET VALORPAGO=@valorprincipal WHERE id=@id", scn);
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
                       "left join RECEBIDA01 on RECEBIDA01.IDDUPLICATA=RECEBIDA.ID " +
                       "left join SITUACAOCOBRANCACOD on SITUACAOCOBRANCACOD.id=RECEBIDA01.IDCOBRANCACOD " +
                       "left join SITUACAOCOBRANCACOD1 on SITUACAOCOBRANCACOD1.id=RECEBIDA01.IDCOBRANCAHIS ",
                       "RECEBIDA01.ID, RECEBIDA01.IDDUPLICATA, RECEBIDA01.DATAENVIO, RECEBIDA01.DATAOK, " +
                       "SITUACAOCOBRANCACOD.CODIGO + '=' + SITUACAOCOBRANCACOD.NOME AS [COB], " +
                       "RECEBIDA01.VALOR, RECEBIDA01.DEBCRED, RECEBIDA01.BAIXOUCOMO ",
                       "RECEBIDA.IDReceber = " + id, "SITUACAOCOBRANCACOD.CODIGO");


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

                //tbxRecebido.Text = clsGridHelper.SomaGrid(dgvPagas01, "VALOR").ToString("N2");


                dgvRecebida01.Sort(dgvRecebida01.Columns["DATAENVIO"], ListSortDirection.Ascending);

                //GridHelper.SelecionaLinha(indexReceber, dgvPagas01, 1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //private void btnIdVendedor_Click(object sender, EventArgs e)
        //{
        //    clsInfo.znomegrid = btnIdVendedor.Name;
        //    frmClientePes frmClientePes = new frmClientePes();
        //    frmClientePes.Init("Vendedores", idVendedor);

        //    clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        //}

        //private void btnIdCoordenador_Click(object sender, EventArgs e)
        //{
        //    clsInfo.znomegrid = btnIdCoordenador.Name;
        //    frmClientePes frmClientePes = new frmClientePes();
        //    frmClientePes.Init("Vendedores", idCoordenador);

        //    clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        //}

        //private void btnIdSupervisor_Click(object sender, EventArgs e)
        //{
        //    clsInfo.znomegrid = btnIdSupervisor.Name;
        //    frmClientePes frmClientePes = new frmClientePes();
        //    frmClientePes.Init("Vendedores", idSupervisor);

        //    clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        //}

        private void btnReceber01_idcobrancacod_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnReceber01_idcobrancacod.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", receber01_idcobrancacod, "Situacao de Cobrança");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void tbxDataBaixa_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
