using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Informacoes.Transporte;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPedido : Form
    {
        DataGridViewRowCollection rows;

        // Pedido
        clsPedidoInfo clsPedidoInfo;
        clsPedidoInfo clsPedidoInfoOld;
        clsPedidoBLL clsPedidoBLL;

        private Int32 id;
        private Int32 idcliente;
        private Int32 idtransportadora;
        private Int32 idredespacho;
        private Int32 idvendedor;
        private Int32 idformapagto;
        private Int32 idcondpagto;
        private Int32 filial;
        private Int32 idufdestino;


        // Pedido1
        DataTable dtPedido1;
        clsPedido1Info clsPedido1Info;
        clsPedido1Info clsPedido1InfoOld;
        clsPedido1BLL clsPedido1BLL;
        Int32 pedido1_id;
        Int32 pedido1_posicao;
        Int32 pedido1_idcfop;
        Int32 pedido1_idcodigo;
        Int32 pedido1_idipi;
        Int32 pedido1_idorcamento;
        Int32 pedido1_idorcamentoitem;
        Int32 pedido1_idordemservico;
        Int32 pedido1_idpedido;
        Int32 pedido1_idsittriba;
        Int32 pedido1_idsittribb;
        Int32 pedido1_idsittribcofins;
        Int32 pedido1_idsittribipi;
        Int32 pedido1_idsittribpispasep;
        Int32 pedido1_idtiponota;
        Int32 pedido1_idunidade;
        Int32 pedido1_item;
        Int32 pedido1_idcentocusto;
        Int32 pedido1_idcontacontabil;
        Double fatortributo= 30.75/100;
        // Pedido1 para criar a Ordem de Servico
        DataTable dtPedidoOS;

        BackgroundWorker bwrMaterialDevolvido;
        DataTable dtMaterialDevolvido;
        DataTable dtMaterialEnviado;
        Int32 idNFEntrada;

        Boolean tiponota_devolucao = false;

        SqlConnection scn;
        SqlCommand scd;

        // Pedido Receber
        String PedidoPago = "";
        DataTable dtPedidoReceber;
        DataTable dtPedidoReceberTempDeletar;

        Int32 pedidoreceber_id;
        Int32 pedidoreceber_posicao;
        Int32 pedidoreceber_idnota;
        Int32 pedidoreceber_idtipopaga;

        clsCondpagtoInfo clsCondpagtoInfo = new clsCondpagtoInfo();

        clsPedidoReceberInfo clsPedidoReceberInfo;
        clsPedidoReceberInfo clsPedidoReceberInfoOld = new clsPedidoReceberInfo();

        clsPedidoReceberBLL clsPedidoReceberBLL = new clsPedidoReceberBLL();

        clsReceberBLL ReceberBLL = new clsReceberBLL();



        public frmPedido()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            this.id = _id;
            this.rows = _rows;

            clsPedidoBLL = new clsPedidoBLL();
            clsPedido1BLL = new clsPedido1BLL();
            clsPedidoReceberBLL = new clsPedidoReceberBLL();
            ReceberBLL = new clsReceberBLL();

            // Carrega os AutoComplete
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente ", tbxClienteC_cognome);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente ", tbxRedespacho);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente ", tbxClienteT_cognome);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from SITUACAOTIPOTITULO ", tbxFormapagto_codigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from CONDPAGTO ", tbxCondpagto_codigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from unidade ", tbxFreteunid);

            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", tbxCentrocusto);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", tbxContaContabil);

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select CODIGO from PECAS ", tbxPecas_codigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cfop from cfop ", tbxCfop_cfop);

            //Visual.FillComboBox(cbxTiponota, "select codigo + ' = ' + nome from tiponota inner join tiponotacliente on tiponota.id = tiponotacliente.idtiponota where tiponotacliente.idcliente = " + _clsPedidoInfo.idcliente + " and movimentacao='1' and left(codigo, 1) = '" + pCFOP + "' order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxTiponota, "select codigo + ' = ' + nome from tiponota order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxSittriba, "select codigo from sittributariaa order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxSittribb, "select codigo from sittributariab order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxSittribipi, "select codigo from sittribipi order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxSittribpispasep, "select codigo from sittribpis order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxSittribcofins1, "select codigo from sittribcofins order by codigo", clsInfo.conexaosqldados);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "UNIDADE", "CODIGO", "", tbxUnidade);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "IPI", "CODIGO", "", tbxIpi_codigo);

            cbxTiponota.SelectedIndex = 0;
            cbxSittriba.SelectedIndex = 0;
            cbxSittribb.SelectedIndex = 0;
            cbxSittribipi.SelectedIndex = 0;
            cbxSittribpispasep.SelectedIndex = 0;
            cbxSittribcofins1.SelectedIndex = 0;
        }

        private void frmPedido_Load(object sender, EventArgs e)
        {
            if (clsInfo.zusuario == "SUPERVISOR")
            {
                tbxTotalCusto.Visible = true;
                tbxValorApurado.Visible = true;
                tbxPrecoCusto.Visible = true;
                tbxTotalCustoItem.Visible = true;
                label17.Visible = true;
                label86.Visible = true;
            }
            Decimal x = 3079;
            Decimal y = 10000;

            PedidoCarregar();
        }

        private void frmPedido_Activated(object sender, EventArgs e)
        {
           TrataCampos((Control)sender);
        }

        private void frmPedido_Shown(object sender, EventArgs e)
        {
            //FormHelper.VerificarForm(this, toolTip1);
        }

        private void PedidoCarregar()
        {
            clsPedidoInfoOld = new clsPedidoInfo();
            if (id == 0)
            {
                clsPedidoInfo = new clsPedidoInfo();
                label1.Text = "NºPed Previsto";
                clsPedidoInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOP 1 NUMERO FROM PEDIDO WHERE YEAR(DATA) = " + DateTime.Now.Year + " ORDER BY NUMERO DESC ")) + 1;
                clsPedidoInfo.data = DateTime.Now;
                clsPedidoInfo.ano = DateTime.Now.Year;
                clsPedidoInfo.emitente = clsInfo.zusuario;
                clsPedidoInfo.filial = clsInfo.zfilial;
                clsPedidoInfo.idcliente = clsInfo.zempresaclienteid;
                clsPedidoInfo.idcondpagto = clsInfo.zcondpagto;
                clsPedidoInfo.idformapagto = clsInfo.zformapagto;
                clsPedidoInfo.idtransportadora = clsInfo.zempresaclienteid;
                clsPedidoInfo.idredespacho = clsInfo.zempresaclienteid;
                clsPedidoInfo.idvendedor = clsInfo.zempresaclienteid;
                clsPedidoInfo.observa = "";
                clsPedidoInfo.pago_ctareceber = "N";
                cbxTipoDesconto.SelectedIndex = clsVisual.SelecionarIndex("D", 1, cbxTipoDesconto);
            }
            else
            {
                clsPedidoInfo = clsPedidoBLL.Carregar(id, clsInfo.conexaosqldados);
                if (DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy")) > DateTime.Parse(clsPedidoInfo.data.ToString("dd/MM/yyyy")))
                {
                    // travar pedido
                    // data de hoje maior que a emissão do pedido
                    //gbxCliente.Enabled = false;
                    //gbxFrete.Enabled = false;
                    //gbxFreteporconta.Enabled = false;
                    //gbxTransporte.Enabled = false;
                    //gbxFormapagto.Enabled = false;
                    //gbxFretecalculo.Enabled = false;
                    //tspPedido.Enabled = false;
                    //gbxCodigo.Enabled = false;
                    //gbxItem.Enabled = false;
                    //gbxTiponota.Enabled = false;
                    //gbxCfop.Enabled = false;
                    //gbxDestino.Enabled = false;
                    //gbxQtdepedido.Enabled = false;
                }

            }

            PedidoCampos(clsPedidoInfo);
            PedidoFillInfo(clsPedidoInfoOld);

            // Carregar o Contas a Receber dos Pedidos
            dtPedidoReceber = clsPedidoReceberBLL.GridDados(clsPedidoInfo.id);

            DataColumn dcPosicaoReceber = new DataColumn("posicaorec", Type.GetType("System.Int32"));
            dtPedidoReceber.Columns.Add(dcPosicaoReceber);
            PedidoPago = "N";
            for (Int32 i = 1; i <= dtPedidoReceber.Rows.Count; i++)
            {
                dtPedidoReceber.Rows[i - 1]["posicaorec"] = i;

                // Se alguma parcela for paga desativar botoes
                if (dtPedidoReceber.Rows[i - 1]["pagou"] == "S")
                {
                    PedidoPago = "S";
                    tsbCondpagto_Incluir.Enabled = false;
                    tsbCondpagto_Excluir.Enabled = false;
                }
            }
            dtPedidoReceber.AcceptChanges();
            clsPedidoReceberBLL.GridMonta(dgvPagamentos, dtPedidoReceber, pedidoreceber_posicao);

            dgvPagamentos.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPagamentos.Columns["VALOR"].DefaultCellStyle.Format = "N2";

            SomarPagamentos();

            tbxClienteC_cognome.Select();
            tbxClienteC_cognome.SelectAll();

        }

        private void PedidoCampos(clsPedidoInfo info)
        {
            id = info.id;

            tbxAno.Text = info.ano.ToString();
           // tbxComissaoRepresentante.Text = info.comissaorepresentante.ToString("N2");
            tbxData.Text = info.data.ToString("dd/MM/yyyy HH:mm");
            tbxEmitente.Text = info.emitente;
            filial = info.filial;
            if (info.frete == "C")
            {
                rbnFreteC.Checked = true;
            }
            else
            {
                rbnFreteF.Checked = true;
            }
            tbxFretebaseicms.Text = clsParser.DecimalParse(info.fretebaseicms.ToString()).ToString();
            tbxFreteicmaliq.Text = clsParser.DecimalParse(info.freteicmaliq.ToString()).ToString();
            if (info.fretepaga == "0")
            {
                rbnFretepaga0.Checked = true;
            }
            else
            {
                rbnFretepaga1.Checked = true;
            }
            tbxFreteprecouni.Text = clsParser.DecimalParse(info.freteprecouni.ToString()).ToString();
            tbxFreteqtde.Text = clsParser.DecimalParse(info.freteqtde.ToString()).ToString();
            //info.fretetipo
            tbxFretetotal.Text = clsParser.DecimalParse(info.fretetotal.ToString()).ToString();
            tbxFreteunid.Text = info.freteunid;
            tbxFretevaloricms.Text = clsParser.DecimalParse(info.fretevaloricms.ToString()).ToString();
            idcliente = info.idcliente;
            if (idcliente == 0)
            {
                idcliente = clsInfo.zempresaclienteid;
            }
            tbxClienteC_cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idcliente + " ");
            idufdestino = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idestado from cliente where id = " + idcliente + " "));
            tbxUfDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id = " + idufdestino + " ");
            tbxCliente_cnpj.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from cliente where id = " + idcliente + " ");
            tbxCliente_telefoneDDD.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from cliente where id = " + idcliente + " ");
            tbxCliente_telefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from cliente where id = " + idcliente + " ");
            tbxCliente_contato.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select contato from cliente where id = " + idcliente + " ");
            idcondpagto = info.idcondpagto;
            if (idcondpagto == 0)
            {
                idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcondpagto from cliente where id = " + idcliente + " ").ToString());
            }
            tbxCondpagto_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from condpagto where id = " + idcondpagto + " ");
            idformapagto = info.idformapagto;
            if (idformapagto == 0)
            {
                idformapagto = clsInfo.zformapagto;
            }
            tbxFormapagto_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTIPOTITULO where id = " + idformapagto + " ");
            tbxFormapagto_codigo.Text += "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTIPOTITULO where id = " + idformapagto + " ");
            idtransportadora = info.idtransportadora;
            if (idtransportadora == 0)
            {
                idtransportadora = clsInfo.zempresaclienteid;
            }
            tbxClienteT_cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idtransportadora + " ");

            idredespacho = info.idredespacho;
            if (idredespacho == 0)
            {
                idredespacho = clsInfo.zempresaclienteid;
            }
            tbxRedespacho.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idredespacho + " ");

            idvendedor = info.idvendedor;

            tbxNumero.Text = info.numero.ToString();
            tbxObserva.Text = info.observa;
            tbxPago_CtaReceber.Text = info.pago_ctareceber;
            tbxQtdebaixada.Text = clsParser.DecimalParse(info.qtdebaixada.ToString()).ToString("N2");
            tbxQtdedefeito.Text = clsParser.DecimalParse(info.qtdedefeito.ToString()).ToString("N2");
            tbxQtdeentregue.Text = clsParser.DecimalParse(info.qtdeentregue.ToString()).ToString("N2");
            tbxQtdePedido.Text = clsParser.DecimalParse(info.qtdepedido.ToString()).ToString("N2");
            tbxQtdeosaux.Text = clsParser.DecimalParse(info.qtdeosaux.ToString()).ToString("N2");
            tbxQtdesaldo.Text = clsParser.DecimalParse(info.qtdesaldo.ToString()).ToString("N2");
            tbxQtdesucata.Text = clsParser.DecimalParse(info.qtdesucata.ToString()).ToString("N2");

            //info.setor;
            //info.setorfator;
            if (info.situacao == "1")
            {
                rbnSituacao1.Checked = true;
            }
            else if (info.situacao == "2")
            {
                rbnSituacao2.Checked = true;
            }
            else if (info.situacao == "3")
            {
                rbnSituacao3.Checked = true;
            }
            if (info.tipofrete == "0")
            {
                rbnTipofrete0.Checked = true;
            }
            else
            {
                rbnTipofrete1.Checked = true;
            }
            tbxTotalbaseicm.Text = clsParser.DecimalParse(info.totalbaseicm.ToString()).ToString();
            tbxTotalbaseicmsubst.Text = clsParser.DecimalParse(info.totalbaseicmsubst.ToString()).ToString();
            tbxTotalcofins1.Text = clsParser.DecimalParse(info.totalcofins1.ToString()).ToString();
            tbxTotalicm.Text = clsParser.DecimalParse(info.totalicm.ToString()).ToString();
            tbxTotalicmsubst.Text = clsParser.DecimalParse(info.totalicmsubst.ToString()).ToString();
            tbxTotalipi.Text = clsParser.DecimalParse(info.totalipi.ToString()).ToString();
            tbxTotaloutras.Text = clsParser.DecimalParse(info.totaloutras.ToString()).ToString();
            tbxTotalpispasep.Text = clsParser.DecimalParse(info.totalpispasep.ToString()).ToString();
            tbxTotalseguro.Text = clsParser.DecimalParse(info.totalseguro.ToString()).ToString();
            tbxTotalpeso.Text = clsParser.DecimalParse(info.totalpeso.ToString()).ToString();
            tbxTotalmercadoria.Text = clsParser.DecimalParse(info.totalmercadoria.ToString()).ToString();
            tbxTotalCusto.Text = clsParser.DecimalParse(info.totalcusto.ToString()).ToString();
            tbxTotalDesconto.Text = clsParser.DecimalParse(info.totaldesconto.ToString()).ToString();
            tbxTotalPedido.Text = clsParser.DecimalParse(info.totalpedido.ToString()).ToString();
            tbxPag_TotalPedido.Text = tbxTotalPedido.Text;
            if (info.transporte == "S")
            {
                rbnTransporteS.Checked = true;
            }
            else if (info.transporte == "R")
            {
                rbnTransporteR.Checked = true;
            }
            else if (info.transporte == "N")
            {
                rbnTransporteN.Checked = true;
            }
            else
            {
                rbnTransporteT.Checked = true;
            }
            //            tbxTotalfrete.Text = clsParser.DecimalParse(info.totalfrete.ToString()).ToString(); 
            cbxTipoDesconto.SelectedIndex = clsVisual.SelecionarIndex(info.tipodesconto, 1, cbxTipoDesconto);

            // carregando os itens do Pedido de Vendas
            dtPedido1 = clsPedido1BLL.GridDados(clsPedidoInfo.id, clsInfo.conexaosqldados);
            DataColumn dcPosicao = new DataColumn("pedido1_posicao", Type.GetType("System.Int32"));
            dtPedido1.Columns.Add(dcPosicao);
            for (Int32 i = 1; i <= dtPedido1.Rows.Count; i++)
            {
                dtPedido1.Rows[i - 1]["pedido1_posicao"] = i;
            }
            dtPedido1.AcceptChanges();
            clsPedido1BLL.GridMonta(dgvPedido1, dtPedido1, pedido1_posicao);
            clsGridHelper.FontGrid(dgvPedido1, 8);
            // Colocando as Cores nos Itens
            for (Int32 X = 0; X < dgvPedido1.Rows.Count; X++)
            {
                if (clsParser.DecimalParse(dgvPedido1.Rows[X].Cells["QTDESALDO"].Value.ToString()) > 0)
                {
                    dgvPedido1.Rows[X].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    dgvPedido1.Rows[X].DefaultCellStyle.BackColor = Color.PaleGoldenrod;
                }
            }




            tbxClienteC_cognome.Select();
            tbxClienteC_cognome.SelectAll();

            if (id == 0)
            {
                tspImprimir.Enabled = false;
            }
        }

        private void PedidoFillInfo(clsPedidoInfo info)
        {
            info.id = id;
            info.idcliente = idcliente;
            info.idtransportadora = idtransportadora;
            info.idredespacho = idredespacho;
            info.idcondpagto = idcondpagto;
            info.idformapagto = idformapagto;
            info.idvendedor = idvendedor;
            info.filial = filial;

            info.numero = clsParser.Int32Parse(tbxNumero.Text);
            info.data = DateTime.Parse(tbxData.Text);
            info.ano = clsParser.Int32Parse(tbxAno.Text);
            //info.comissaorepresentante = clsParser.DecimalParse(tbxComissaoRepresentante.Text);
            info.emitente = tbxEmitente.Text;

            if (rbnSituacao1.Checked)
                info.situacao = "1";
            else if (rbnSituacao2.Checked)
                info.situacao = "2";
            else if (rbnSituacao3.Checked)
                info.situacao = "3";

            if (rbnFreteC.Checked)
                info.frete = "C";
            else if (rbnFreteF.Checked)
                info.frete = "F";

            if (rbnFretepaga0.Checked)
                info.fretepaga = "0";
            else if (rbnFretepaga1.Checked)
                info.fretepaga = "1";

            if (rbnTipofrete0.Checked)
                info.tipofrete = "0";
            else if (rbnTipofrete1.Checked)
                info.tipofrete = "1";

            if (rbnTransporteS.Checked)
                info.transporte = "S";
            else if (rbnTransporteR.Checked)
                info.transporte = "R";
            else if (rbnTransporteN.Checked)
                info.transporte = "N";
            else if (rbnTransporteT.Checked)
                info.transporte = "T";

            info.freteqtde = clsParser.DecimalParse(tbxFreteqtde.Text);
            info.freteunid = tbxFreteunid.Text;
            info.freteprecouni = clsParser.DecimalParse(tbxFreteprecouni.Text);
            info.fretetotal = clsParser.DecimalParse(tbxFretetotal.Text);
            info.fretebaseicms = clsParser.DecimalParse(tbxFretebaseicms.Text);
            info.freteicmaliq = clsParser.DecimalParse(tbxFreteicmaliq.Text);
            info.fretevaloricms = clsParser.DecimalParse(tbxFretevaloricms.Text);
            info.observa = tbxObserva.Text;
            info.pago_ctareceber = tbxPago_CtaReceber.Text;
            info.totalipi = clsParser.DecimalParse(tbxTotalipi.Text);
            info.totalfrete = clsParser.DecimalParse(tbxTotalfrete.Text);
            info.totalseguro = clsParser.DecimalParse(tbxTotalseguro.Text);
            info.totaloutras = clsParser.DecimalParse(tbxTotaloutras.Text);
            info.totalmercadoria = clsParser.DecimalParse(tbxTotalmercadoria.Text);
            info.totalcusto = clsParser.DecimalParse(tbxTotalCusto.Text);
            info.totaldesconto = clsParser.DecimalParse(tbxTotalDesconto.Text);
            info.totalpedido = clsParser.DecimalParse(tbxTotalPedido.Text);
            info.tipodesconto = cbxTipoDesconto.Text.Substring(0, 1);
            /*            if (cbxTotalfreteicms.Checked)
                            info.totalfreteicms = "S";
                        else
                            info.totalfreteicms = "N";

                        if (cbxTotaloutrasicms.Checked)
                            info.totaloutrasicms = "S";
                        else
                            info.totaloutrasicms = "N";

                        if (cbxTotalseguroicms.Checked)
                            info.totalseguroicms = "S";
                        else
                            info.totalseguroicms = "N";
            */
            info.totalpispasep = clsParser.DecimalParse(tbxTotalpispasep.Text);
            info.totalcofins1 = clsParser.DecimalParse(tbxTotalcofins1.Text);
            info.totalpeso = clsParser.DecimalParse(tbxTotalpeso.Text);
            // tbxTotalpesob ????
            info.totalbaseicm = clsParser.DecimalParse(tbxTotalbaseicm.Text);
            info.totalicm = clsParser.DecimalParse(tbxTotalicm.Text);
            info.totalbaseicmsubst = clsParser.DecimalParse(tbxTotalbaseicmsubst.Text);
            info.totalicmsubst = clsParser.DecimalParse(tbxTotalicmsubst.Text);
            info.qtdepedido = clsParser.DecimalParse(tbxQtdePedido.Text);
            info.qtdeentregue = clsParser.DecimalParse(tbxQtdeentregue.Text);
            info.qtdedefeito = clsParser.DecimalParse(tbxQtdedefeito.Text);
            info.qtdesucata = clsParser.DecimalParse(tbxQtdesucata.Text);
            info.qtdebaixada = clsParser.DecimalParse(tbxQtdebaixada.Text);
            info.qtdeosaux = clsParser.DecimalParse(tbxQtdeosaux.Text);
            info.qtdesaldo = clsParser.DecimalParse(tbxQtdesaldo.Text);
            //info.totalprevisto = clsParser.DecimalParse(tbxTotalDesconto.Text);
        }


        private Boolean HouveModificacoes()
        {
            clsPedidoInfo = new clsPedidoInfo();
            PedidoFillInfo(clsPedidoInfo);
            if (clsPedidoBLL.Equals(clsPedidoInfo, clsPedidoInfoOld) == false)
            {
                return true;
            }
            return false;
        }

        private void PedidoSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: PEDIDO
                clsPedidoInfo = new clsPedidoInfo();
                PedidoFillInfo(clsPedidoInfo);

                if (id == 0)
                {
                    clsPedidoInfo.id = clsPedidoBLL.Incluir(clsPedidoInfo, clsInfo.conexaosqldados);
                    id = clsPedidoInfo.id;
                }
                else
                {
                    clsPedidoBLL.Alterar(clsPedidoInfo, clsInfo.conexaosqldados);
                }
                // ITENS DO PEDIDO DE VENDA
                foreach (DataRow row in dtPedido1.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idpedido"] = clsPedidoInfo.id;
                    }
                }

                foreach (DataRow row in dtPedido1.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsPedido1BLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsPedido1Info = new clsPedido1Info();
                        Pedido1GridToInfo(clsPedido1Info, Int32.Parse(row["pedido1_posicao"].ToString()));

                        if (clsPedido1Info.id == 0)
                        {
                            clsPedido1Info.id = clsPedido1BLL.Incluir(clsPedido1Info, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsPedido1BLL.Alterar(clsPedido1Info, clsInfo.conexaosqldados);
                        }
                    }
                }
                // Transferir Contas a Receber Antigo
                //if (tbxPago_CtaReceber.Text != "S")
                //{
                //    Int32 iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from docfiscal where cognome='PED'"));
                //    ReceberBLL.SicronizarPorDocumento(id, iddocumento, clsInfo.conexaosqldados);

                //}
                // Transferir Contas a Receber Atual com Parcelas 12/05/2021
                foreach (DataRow row in dtPedidoReceber.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached && 
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["IDNOTA"] = clsPedidoInfo.id;
                    }
                }
                // CONTAS A RECEBER   
                foreach (DataRow row in dtPedidoReceber.Rows)
                {
                    if (row["PAGOU"].ToString() == "s")
                    {
                        return;
                    }
                   if (row.RowState == DataRowState.Unchanged)
                   {
                        // Foi colocado pois quando altera apenas o cognome do fornecedor tem que atualizar o contas a pagar
                        row.SetModified();
                    }
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsPedidoReceberBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsPedidoReceberInfo = new clsPedidoReceberInfo();
                        PedidoReceberGridToInfo(dtPedidoReceber, clsPedidoReceberInfo, Int32.Parse(row["posicaorec"].ToString()));

                        if (clsPedidoReceberInfo.id == 0)
                        {
                            clsPedidoReceberInfo.id = clsPedidoReceberBLL.Incluir(clsPedidoReceberInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsPedidoReceberBLL.Alterar(clsPedidoReceberInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
                //////////////////////////////
                List<Produto> listaProdutos = new List<Produto>();
                foreach (DataRow row in dtPedido1.Rows)
                {

                    listaProdutos.Add(new Produto
                    {
                        numero_item = row["NITEM"].ToString(),
                        codigo_ncm = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from IPI where id = " + clsParser.Int32Parse(row["idIPI"].ToString()) + " "),
                        quantidade_comercial = row["QTDE"].ToString().Replace(",", "."),
                        quantidade_tributavel = row["QTDE"].ToString().Replace(",", "."),
                        cfop = "5102",
                        valor_unitario_tributavel = row["PRECO"].ToString().Replace(",", "."),
                        valor_unitario_comercial = row["PRECO"].ToString().Replace(",", "."),
                        valor_desconto = 0.ToString("N2").Replace(",", "."),
                        descricao = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where id = " + clsParser.Int32Parse(row["idcodigo"].ToString()) + " "),
                        codigo_produto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS where id = " + clsParser.Int32Parse(row["idcodigo"].ToString()) + " "),
                        icms_origem = "0",
                        icms_situacao_tributaria = "102",
                        unidade_comercial = "UN",
                        unidade_tributavel = "UN",
                        valor_total_tributos = row["tributo_previsto"].ToString().Replace(",", ".")
                    });
                }

                List<Formas_Pagamentos> ListaFormaPagtos = new List<Formas_Pagamentos>();
                //foreach (DataRow row in dtPedido1.Rows)
                //{

                    ListaFormaPagtos.Add(new Formas_Pagamentos
                    {
                        forma_pagamento = "0",
                        valor_pagamento = clsPedidoInfo.totalmercadoria.ToString().Replace(",", "."),
                        nome_credenciadora = "",
                        bandeira_operadora = "",
                        numero_autorizacao = ""
                    });
                //}
                var MeuRelatorio = new Relatorio
                {
                    cnpj_emitente = "38029184000195",
                    data_emissao = clsPedidoInfo.data.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                    indicador_inscricao_estadual_destinatario = "9",
                    modalidade_frete = "9",
                    local_destino = "1",
                    presenca_comprador = "1",
                    natureza_operacao = "VENDA AO CONSUMIDOR",
                    items = listaProdutos,
                    formas_pagamento = ListaFormaPagtos
                    
                }; 
                // 3. Serialize
                //string json = JsonSerializer.Serialize(MeuRelatorio);
                var strJson = JsonConvert.SerializeObject(MeuRelatorio, Formatting.Indented);
                //using (StreamWriter sw = new StreamWriter("C:\\Clientes\\CASACORREA\\XML\\Saidas\\Ped" + clsPedidoInfo.ano + clsPedidoInfo.numero.ToString().PadLeft(5, '0') + ".json"))
                using (StreamWriter sw = new StreamWriter(clsInfo.saidaxml + "Ped" + clsPedidoInfo.ano + clsPedidoInfo.numero.ToString().PadLeft(5, '0') + ".json"))
                {
                    sw.WriteLine(strJson);
                    //Console.WriteLine(strJson);
                }
                tse.Complete();
            }

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
                    PedidoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                PedidoSalvar();
            }
            return drt;

        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    //if (clsParser.DecimalParse(tbxValorParcelas.Text) == 0 && tbxCondpagto_codigo.Text =="0")
                    if (clsParser.DecimalParse(tbxValorParcelas.Text) == 0)
                    {
                       PedidoReceberCalcularPagamentos();
                    }
                    if (clsParser.DecimalParse(tbxValorParcelas.Text) != clsParser.DecimalParse(tbxPag_TotalPedido.Text))
                    {
                        MessageBox.Show("Valor das Parcelas não Confere com Total do Pedido");
                        return;
                    }
                    else
                    { 
                        PedidoSalvar();
                    }
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxClienteC_cognome.Select();
                    tbxClienteC_cognome.SelectAll();
                    return;
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxClienteC_cognome.Select();
                tbxClienteC_cognome.SelectAll();
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
                                PedidoCarregar();
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
                                PedidoCarregar();
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
                    PedidoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1)
            {
                if (clsInfo.znomegrid == btnIdCliente.Name)
                {
                    idcliente = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxClienteC_cognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxCliente_cnpj.Text = clsInfo.zrow.Cells["CGC"].Value.ToString();

                    tbxCliente_telefoneDDD.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from cliente where id = " + idcliente + " ");
                    tbxCliente_telefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from cliente where id = " + idcliente + " ");
                    tbxCliente_contato.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select contato from cliente where id = " + idcliente + " ");

                    idvendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idvendedor from cliente where id = " + idcliente + " "));
                    tbxVendedor.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idvendedor + " ");

                    // Condição de Pagamento
                    idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcondpagto from cliente where id = " + idcliente + " "));
                    tbxCondpagto_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from condpagto where id = " + idcondpagto + " ");

                    tbxClienteC_cognome.Select();
                    tbxClienteC_cognome.SelectAll();
                }
                else if (clsInfo.znomegrid == btnTransportadora_cognome.Name)
                {
                    idtransportadora = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxClienteT_cognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();

                    tbxClienteT_cognome.Select();
                    tbxClienteT_cognome.SelectAll();
                }
                else if (clsInfo.znomegrid == btnRedespacho.Name)
                {
                    idredespacho = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxRedespacho.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();

                    tbxRedespacho.Select();
                    tbxRedespacho.SelectAll();
                }
                else if (clsInfo.znomegrid == btnCondpagto_codigo.Name)
                {
                    idcondpagto = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxCondpagto_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    tbxCondpagto_codigo.Select();
                    tbxCondpagto_codigo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnFormaPagto_codigo.Name)
                {
                    idformapagto = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxFormapagto_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxFormapagto_codigo.Text += "=" + clsInfo.zrow.Cells["NOME"].Value.ToString();

                    tbxFormapagto_codigo.Select();
                    tbxFormapagto_codigo.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdVendedor.Name)
                {
                    idvendedor = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxVendedor.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                }
            }

            if (ctl.Name == tbxClienteC_cognome.Name)
            { // Verificar se já não existe codigo similar

                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxClienteC_cognome.Text + "'"));
                if (idcliente == 0)
                {
                    idcliente = clsInfo.zempresaclienteid;
                }
                tbxClienteC_cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idcliente + " ");
                tbxCliente_cnpj.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from cliente where id = " + idcliente + " ");

                tbxCliente_telefoneDDD.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from cliente where id = " + idcliente + " ");
                tbxCliente_telefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from cliente where id = " + idcliente + " ");
                tbxCliente_contato.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select contato from cliente where id = " + idcliente + " ");

                // Carregar Tudo
                // UF
                idufdestino = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idestado from cliente where id = " + idcliente + " "));
                tbxUfDestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id = " + idufdestino + " ");

                cbxIsentoipi.Checked = (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select isentoipi from cliente where id=" + idcliente) == "S");
                idvendedor = clsParser.Int32Parse((Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idvendedor from cliente where id=" + idcliente)));
                tbxVendedor.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idvendedor + " ");
                cbxZfm.Checked = (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select alc from cliente where id=" + idcliente) == "S");
                // Meio de Transporte
                String meiotransporte = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select MEIODETRANSPORTE from cliente where id=" + idcliente);
                if (meiotransporte == "S")
                {
                    rbnTransporteS.Checked = true;
                }
                else if (meiotransporte == "R")
                {
                    rbnTransporteR.Checked = true;
                }
                else if (meiotransporte == "T")
                {
                    rbnTransporteT.Checked = true;
                }
                else
                {
                    rbnTransporteN.Checked = true;
                }

                // Frete Incluso
                String freteincluso = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FRETEINCLUSO from cliente where id=" + idcliente);
                if (freteincluso == "C")
                {
                    rbnFreteC.Checked = true;
                }
                else
                {
                    rbnFreteF.Checked = true;
                }
                // Frete por Conta
                String freteporconta = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select FRETEPORCONTA from cliente where id=" + idcliente);
                if (freteporconta == "0")
                {
                    rbnFretepaga0.Checked = true;
                }
                else
                {
                    rbnFretepaga1.Checked = true;
                }
                rbnTipofrete0.Checked = true;
                idtransportadora = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDTRANSPORTADORA from cliente where id=" + idcliente));
                tbxClienteT_cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idtransportadora + " ");

                idredespacho = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idredespacho from cliente where id=" + idcliente));
                tbxRedespacho.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idredespacho + " ");

                if (clsParser.DecimalParse(tbxTotalPedido.Text) == 0)
                {

                    idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDFORMAPAGTO from cliente where id=" + idcliente));
                    tbxFormapagto_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTIPOTITULO where id = " + idformapagto + " ");
                    tbxFormapagto_codigo.Text += "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTIPOTITULO where id = " + idformapagto + " ");

                    idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcondpagto from cliente where id=" + idcliente));
                    tbxCondpagto_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from condpagto where id = " + idcondpagto + " ");
                }
                /*
                LimiteCreditoCarrega();
                */
            }
            if (ctl.Name == tbxClienteT_cognome.Name)
            { // Verificar se já não existe codigo similar

                idtransportadora = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxClienteT_cognome.Text + "'"));
                if (idtransportadora == 0)
                {
                    idtransportadora = clsInfo.zempresaclienteid;
                }
                tbxClienteT_cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idtransportadora + " ");
            }
            if (ctl.Name == tbxRedespacho.Name)
            {
                idredespacho = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxRedespacho.Text + "'"));
                if (idredespacho == 0)
                {
                    idredespacho = clsInfo.zempresaclienteid;
                }
                tbxRedespacho.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idredespacho + " ");
            }
            if (ctl.Name == tbxFormapagto_codigo.Name)
            { // Verificar se já não existe codigo similar

                idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOTIPOTITULO where CODIGO='" + tbxFormapagto_codigo.Text.Substring(0, 2) + "'"));
                if (idformapagto == 0)
                {
                    idformapagto = clsInfo.zformapagto;
                }
                tbxFormapagto_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTIPOTITULO where id = " + idformapagto + " ");
                tbxFormapagto_codigo.Text += "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTIPOTITULO where id = " + idformapagto + " ");
            }
            if (ctl.Name == tbxCondpagto_codigo.Name)
            { // Verificar se já não existe codigo similar

                idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from condpagto where CODIGO='" + tbxCondpagto_codigo.Text + "'"));
                if (idcondpagto == 0)
                {
                    idcondpagto = clsInfo.zcondpagto;
                }
                tbxCondpagto_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from condpagto where id = " + idcondpagto + " ");
            }
            //Cabeçalho do Item
            tbxNumero_Item.Text = tbxNumero.Text;
            tbxData_Item.Text = tbxData.Text;
            tbxAno_Item.Text = tbxAno.Text;
            tbxEmitente_Item.Text = tbxEmitente.Text;
            tbxCliente_Item.Text = tbxClienteC_cognome.Text;
            if (rbnSituacao1.Checked == true)
            {
                tbxSituacao.Text = "1 - Venda ";
            }
            else if (rbnSituacao2.Checked == true)
            {
                tbxSituacao.Text = "2 - Beneficiamento ";
            }
            else
            {
                tbxSituacao.Text = "3 - Mão Obra ";
            }

            // Item do Pedido
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1)
            {
                if (clsInfo.znomegrid == btnPecas.Name)    // PRODUTO
                {
                    pedido1_idcodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPecas_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxPecas_nome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    pedido1_idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idIPI from pecas where id = " + pedido1_idcodigo + " "));
                    tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from ipi where id = " + pedido1_idipi + " ");
                    tbxPecas_codigo.Select();
                    tbxPecas_codigo.SelectAll();
                    if (pedido1_idpedido == 0)
                    {
                        TipoNotaCarregar();
                    }
                }
                if (clsInfo.znomegrid == btnCfop_codigo.Name)
                {
                    pedido1_idcfop = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxCfop_cfop.Text = clsInfo.zrow.Cells["CFOP"].Value.ToString();
                    tbxCfop_cfop.Select();
                    tbxCfop_cfop.SelectAll();
                }
                if (clsInfo.znomegrid == btnSitTriba.Name)
                {
                    pedido1_idsittriba = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    cbxSittriba.SelectedIndex = cbxSittriba.FindString(clsInfo.zrow.Cells["CODIGO"].Value.ToString());
                    if (cbxSittriba.SelectedIndex == -1)
                    {
                        cbxSittriba.SelectedIndex = 0;
                    }
                    cbxSittriba.Select();
                    cbxSittriba.SelectAll();
                }
                if (clsInfo.znomegrid == btnSitTribb.Name)
                {
                    pedido1_idsittribb = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    cbxSittribb.SelectedIndex = cbxSittribb.FindString(clsInfo.zrow.Cells["CODIGO"].Value.ToString());
                    if (cbxSittribb.SelectedIndex == -1)
                    {
                        cbxSittribb.SelectedIndex = 0;
                    }
                    cbxSittribb.Select();
                    cbxSittribb.SelectAll();
                }
                if (clsInfo.znomegrid == btnSittribipi.Name)
                {
                    pedido1_idsittribipi = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    cbxSittribipi.SelectedIndex = cbxSittribipi.FindString(clsInfo.zrow.Cells["CODIGO"].Value.ToString());
                    if (cbxSittribipi.SelectedIndex == -1)
                    {
                        cbxSittribipi.SelectedIndex = 0;
                    }
                    cbxSittribipi.Select();
                    cbxSittribipi.SelectAll();
                }
                if (clsInfo.znomegrid == btnClassfiscal.Name)
                {
                    pedido1_idipi = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxIpi_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxIpi.Text = clsInfo.zrow.Cells["ALIQUOTA"].Value.ToString();
                    tbxIpi_codigo.Select();
                    tbxIpi_codigo.SelectAll();
                }
                if (clsInfo.znomegrid == btnSittribpispasep.Name)
                {
                    pedido1_idsittribpispasep = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    cbxSittribpispasep.SelectedIndex = cbxSittribpispasep.FindString(clsInfo.zrow.Cells["CODIGO"].Value.ToString());
                    if (cbxSittribpispasep.SelectedIndex == -1)
                    {
                        cbxSittribpispasep.SelectedIndex = 0;
                    }
                    cbxSittribpispasep.Select();
                    cbxSittribpispasep.SelectAll();
                }
                if (clsInfo.znomegrid == btnSittribcofins1.Name)
                {
                    pedido1_idsittribcofins = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    cbxSittribcofins1.SelectedIndex = cbxSittribcofins1.FindString(clsInfo.zrow.Cells["CODIGO"].Value.ToString());
                    if (cbxSittribcofins1.SelectedIndex == -1)
                    {
                        cbxSittribcofins1.SelectedIndex = 0;
                    }
                    cbxSittribcofins1.Select();
                    cbxSittribcofins1.SelectAll();
                }
                if (clsInfo.znomegrid == btnCentroCusto.Name)
                {
                    pedido1_idcentocusto = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxCentrocusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxCentrocusto.Select();
                    tbxCentrocusto.SelectAll();
                }
                if (clsInfo.znomegrid == btnContaContabil.Name)
                {
                    pedido1_idcontacontabil = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxContaContabil.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxContaContabil.Select();
                    tbxContaContabil.SelectAll();
                }
            }

            //textos
            if (ctl.Name == tbxPecas_codigo.Name)
            {
                pedido1_idcodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from pecas where codigo='" + tbxPecas_codigo.Text + "'"));
                if (pedido1_idcodigo == 0)
                {
                    pedido1_idcodigo = clsInfo.zpecas;
                }
                tbxPecas_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id = " + pedido1_idcodigo + " ");
                tbxPecas_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id = " + pedido1_idcodigo + " ");
                pedido1_idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idIPI from pecas where id = " + pedido1_idcodigo + " "));
                tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from ipi where id = " + pedido1_idipi + " ");

                // idsittriba = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDSITTRIBA FROM PECAS WHERE ID=" + idcodigo));
                pedido1_idunidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADE FROM PECAS WHERE ID=" + pedido1_idcodigo));
                if (pedido1_idunidade == 0)
                {
                    pedido1_idunidade = clsInfo.zunidade;
                }
                tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where ID= " + pedido1_idunidade);
                //tbxBaseMP.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select basemp from pecas where ID= " + pedido1_idcodigo);
                tbxPeso.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select pesounit from pecas where ID= " + pedido1_idcodigo);

                pedido1_idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idipi from pecas where ID= " + pedido1_idcodigo));
                tbxIpi.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquotaipi from pecas where ID= " + pedido1_idcodigo))).ToString("N2");
                if (pedido1_idipi == 0)
                {
                    pedido1_idipi = clsInfo.zipi;
                }
                tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from ipi where ID= " + pedido1_idipi);
                // Verifica se incide PisPasep
                tbxAliqpispasep.Text = "0";
                tbxAliqcofins1.Text = "0";
                if ((Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PISPASEP from ipi where ID= " + pedido1_idipi)) == "S")
                {// Pis
                    tbxAliqpispasep.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select LEIPISPASEP from EMPRESASGERE where EMPRESA= " + clsInfo.zfilial))).ToString("N2");
                }
                if ((Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COFINS from ipi where ID= " + pedido1_idipi)) == "S")
                {// Cofins
                    tbxAliqcofins1.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select LEICOFINS from EMPRESASGERE where EMPRESA= " + clsInfo.zfilial))).ToString("N2");
                }

                tbxCodigoemp04.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aplicacao from pecas where ID= " + pedido1_idcodigo);
                if (clsParser.DecimalParse(tbxPreco.Text) == 0)
                {
                    //tbxPreco.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoravista from pecaspreco where IDcodigo= " + pedido1_idcodigo + " ORDER BY DATA DESC"))).ToString("N4");
                    tbxPrecoTabela.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoravista from pecaspreco where IDcodigo= " + pedido1_idcodigo + " ORDER BY DATA DESC"))).ToString("N4");
                     (clsParser.DecimalParse(tbxPrecoTabela.Text) - clsParser.DecimalParse(tbxPrecoDesconto.Text)).ToString("N4");
                    // Preco de Custo
                    tbxPrecoTabela.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoravista from pecaspreco where IDcodigo= " + pedido1_idcodigo + " ORDER BY DATA DESC"))).ToString("N4");
                    tbxPreco.Text = (clsParser.DecimalParse(tbxPrecoTabela.Text) - clsParser.DecimalParse(tbxPrecoDesconto.Text)).ToString("N4");

                    tbxPrecoCusto.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select precocompra from pecas where Id= " + pedido1_idcodigo + " "))).ToString("N4");
                    if (clsParser.DecimalParse(tbxPrecoCusto.Text) == 0 || clsParser.DecimalParse(tbxPrecoCusto.Text) > clsParser.DecimalParse(tbxPreco.Text))
                    {
                        tbxPrecoCusto.Text =((clsParser.DecimalParse(tbxPreco.Text) * 70)/100).ToString("N4");
                    }

                    cbxConsumo.SelectedIndex = cbxConsumo.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CONSUMO from pecas where ID= " + pedido1_idcodigo));
                    if (cbxConsumo.SelectedIndex == -1)
                    {
                        cbxConsumo.SelectedIndex = 0;
                    }
                    CarregaImpostos();
                }
            }
            if (ctl.Name == tbxPrecoTabela.Name || ctl.Name == tbxPrecoDesconto.Name)
            {
                tbxPreco.Text = (clsParser.DecimalParse(tbxPrecoTabela.Text) - clsParser.DecimalParse(tbxPrecoDesconto.Text)).ToString("N4");
            }
            if (ctl.Name == tbxCfop_cfop.Name)
            {
                pedido1_idcfop = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CFOP where CFOP='" + tbxCfop_cfop.Text + "'"));
                if (pedido1_idcfop == 0)
                {
                    pedido1_idcfop = clsInfo.zcfop;
                }
                tbxCfop_cfop.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cfop from cfop where id = " + pedido1_idcfop + " ");
            }
            if (ctl.Name == tbxIpi_codigo.Name)
            {
                pedido1_idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from IPI where codigo='" + tbxIpi_codigo.Text + "'"));
                if (pedido1_idipi == 0)
                {
                    pedido1_idipi = clsInfo.zipi;
                }
                tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from IPI where id = " + pedido1_idipi + " ");
                tbxIpi.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ALIQUOTA from IPI where id = " + pedido1_idipi + " ");
            }
            if (ctl.Name == cbxTiponota.Name)
            {
                String tipoNota = cbxTiponota.Text.PadRight(8, ' ').Substring(0, 8);
                cbxTiponota.SelectedIndex = clsVisual.SelecionarIndex(tipoNota, 8, cbxTiponota);
                if (cbxTiponota.SelectedIndex == -1)
                {
                    cbxTiponota.SelectedIndex = 0;
                }
                // Capturar o Id
                pedido1_idtiponota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tiponota where codigo = '" + tipoNota + "' ", "0"));
                TipoNotaCarregar();

            }
            if (tclPedidoVenda.SelectedIndex == 1)
            {
                if (clsParser.DecimalParse(tbxQtde.Text) > 0)
                {
                    Pedido1Calcular();
                    Pedido1CalcularSaldo();
                }

            }

            if (ctl.Name == tbxCentrocusto.Name)
            {
                pedido1_idcentocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where codigo='" + tbxCentrocusto.Text + "'"));
                tbxCentrocusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where codigo='" + tbxCentrocusto.Text + "'");

            }
            if (clsInfo.znomegrid == btnContaContabil.Name)
            {
                pedido1_idcontacontabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where codigo='" + tbxContaContabil.Text + "'"));
                tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where codigo='" + tbxContaContabil.Text + "'");
            }


            PedidoCalcular();

            // campos cabeçalho
            tbxFreteqtde.Text = clsParser.DecimalParse(tbxFreteqtde.Text).ToString("N3");
            tbxFreteprecouni.Text = clsParser.DecimalParse(tbxFreteprecouni.Text).ToString("###,###,##0.00");
            tbxFretebaseicms.Text = clsParser.DecimalParse(tbxFretebaseicms.Text).ToString("###,###,##0.00");
            tbxFreteicmaliq.Text = clsParser.DecimalParse(tbxFreteicmaliq.Text).ToString("###,###,##0.00");
            tbxFretevaloricms.Text = clsParser.DecimalParse(tbxFretevaloricms.Text).ToString("###,###,##0.00");
            tbxFretetotal.Text = clsParser.DecimalParse(tbxFretetotal.Text).ToString("###,###,##0.00");
            ////////
            tbxQtdePedido.Text = clsParser.DecimalParse(tbxQtdePedido.Text).ToString("N2");
            tbxQtdeentregue.Text = clsParser.DecimalParse(tbxQtdeentregue.Text).ToString("N2");
            tbxQtdedefeito.Text = clsParser.DecimalParse(tbxQtdedefeito.Text).ToString("N2");
            tbxQtdesucata.Text = clsParser.DecimalParse(tbxQtdesucata.Text).ToString("N2");
            tbxQtdebaixada.Text = clsParser.DecimalParse(tbxQtdebaixada.Text).ToString("N2");
            tbxQtdeosaux.Text = clsParser.DecimalParse(tbxQtdeosaux.Text).ToString("N2");
            tbxQtdesaldo.Text = clsParser.DecimalParse(tbxQtdesaldo.Text).ToString("N2");
            ////////
            tbxTotalpeso.Text = clsParser.DecimalParse(tbxTotalpeso.Text).ToString("###,###,##0.00");
            tbxTotalbaseicm.Text = clsParser.DecimalParse(tbxTotalbaseicm.Text).ToString("###,###,##0.00");
            tbxTotalicm.Text = clsParser.DecimalParse(tbxTotalicm.Text).ToString("###,###,##0.00");
            tbxTotalbaseicmsubst.Text = clsParser.DecimalParse(tbxTotalbaseicmsubst.Text).ToString("###,###,##0.00");
            tbxTotalicmsubst.Text = clsParser.DecimalParse(tbxTotalicmsubst.Text).ToString("###,###,##0.00");
            tbxTotalipi.Text = clsParser.DecimalParse(tbxTotalipi.Text).ToString("###,###,##0.00");
            tbxTotalpispasep.Text = clsParser.DecimalParse(tbxTotalpispasep.Text).ToString("###,###,##0.00");
            tbxTotalcofins1.Text = clsParser.DecimalParse(tbxTotalcofins1.Text).ToString("###,###,##0.00");
            tbxTotalfrete.Text = clsParser.DecimalParse(tbxTotalfrete.Text).ToString("###,###,##0.00");
            tbxTotalseguro.Text = clsParser.DecimalParse(tbxTotalseguro.Text).ToString("###,###,##0.00");
            tbxTotaloutras.Text = clsParser.DecimalParse(tbxTotaloutras.Text).ToString("###,###,##0.00");
            tbxTotalmercadoria.Text = clsParser.DecimalParse(tbxTotalmercadoria.Text).ToString("###,###,##0.00");
            tbxTotalCusto.Text = clsParser.DecimalParse(tbxTotalCusto.Text).ToString("###,###,##0.00");
            tbxTotalDesconto.Text = clsParser.DecimalParse(tbxTotalDesconto.Text).ToString("###,###,##0.00");
            tbxTotalPedido.Text = clsParser.DecimalParse(tbxTotalPedido.Text).ToString("###,###,##0.00");

            tbxValorApurado.Text = (clsParser.DecimalParse(tbxTotalPedido.Text) - clsParser.DecimalParse(tbxTotalCusto.Text)).ToString("###,###,##0.00");
            // campos item
            tbxQtde.Text = clsParser.DecimalParse(tbxQtde.Text).ToString("N2");
            tbxPrecoDesconto.Text = clsParser.DecimalParse(tbxPrecoDesconto.Text).ToString("N4");
            tbxPrecoTabela.Text = clsParser.DecimalParse(tbxPrecoTabela.Text).ToString("N4");
            tbxPreco.Text = (clsParser.DecimalParse(tbxPrecoTabela.Text) - clsParser.DecimalParse(tbxPrecoDesconto.Text)).ToString("N4");

            tbxTotalmercado.Text = clsParser.DecimalParse(tbxTotalmercado.Text).ToString("N2");
            tbxPeso.Text = clsParser.DecimalParse(tbxPeso.Text).ToString("N3");
            tbxTotalPeso1.Text = clsParser.DecimalParse(tbxTotalPeso1.Text).ToString("N3");
            tbxBaseicm.Text = clsParser.DecimalParse(tbxBaseicm.Text).ToString("N2");
            tbxIcm.Text = clsParser.DecimalParse(tbxIcm.Text).ToString("N2");
            tbxReducao.Text = clsParser.DecimalParse(tbxReducao.Text).ToString("N2");
            tbxBaseicm.Text = clsParser.DecimalParse(tbxBaseicm.Text).ToString("N2");
            tbxCustoicm.Text = clsParser.DecimalParse(tbxCustoicm.Text).ToString("N2");
            tbxBaseicmsubst.Text = clsParser.DecimalParse(tbxBaseicmsubst.Text).ToString("N2");
            tbxIcmsubst.Text = clsParser.DecimalParse(tbxIcmsubst.Text).ToString("N2");
            tbxIcmssubstreducao.Text = clsParser.DecimalParse(tbxIcmssubstreducao.Text).ToString("N2");
            tbxMva.Text = clsParser.DecimalParse(tbxMva.Text).ToString("N2");
            tbxIcmssubsttotal.Text = clsParser.DecimalParse(tbxIcmssubsttotal.Text).ToString("N2");
            tbxBcipi.Text = clsParser.DecimalParse(tbxBcipi.Text).ToString("N2");
            tbxIpi.Text = clsParser.DecimalParse(tbxIpi.Text).ToString("N2");
            tbxCustoipi.Text = clsParser.DecimalParse(tbxCustoipi.Text).ToString("N2");
            tbxBcpispasep.Text = clsParser.DecimalParse(tbxBcpispasep.Text).ToString("N2");
            tbxAliqpispasep.Text = clsParser.DecimalParse(tbxAliqpispasep.Text).ToString("N2");
            tbxPispasep.Text = clsParser.DecimalParse(tbxPispasep.Text).ToString("N2");
            tbxBccofins1.Text = clsParser.DecimalParse(tbxBccofins1.Text).ToString("N2");
            tbxAliqcofins1.Text = clsParser.DecimalParse(tbxAliqcofins1.Text).ToString("N2");
            tbxCofins1.Text = clsParser.DecimalParse(tbxCofins1.Text).ToString("N2");
            tbxTotalnota.Text = clsParser.DecimalParse(tbxTotalnota.Text).ToString("N2");

            tbxTotalPrevistoItem.Text = clsParser.DecimalParse(tbxTotalPrevistoItem.Text).ToString("N2");
           
;           tbxTributo_Previsto.Text = (clsParser.DoubleParse(tbxTotalnota.Text) * fatortributo).ToString("N3");
            ///////////
//            tbxComissaoRepresentante.Text = (clsParser.DecimalParse(tbxTotalPedido.Text) * clsParser.DecimalParse("0,02")).ToString("N2");
            /// 
            // Campos semelhantes tela de pagamentos
            tbxPag_Pedido.Text = tbxNumero.Text;
            tbxPag_Data.Text = tbxData.Text;
            tbxPag_Cliente.Text = tbxClienteC_cognome.Text;
            tbxPag_TotalPedido.Text = tbxTotalPedido.Text;
            // Tela de Registro de Pagamentos
            if (clsParser.DecimalParse(PedidoReceber_tbxValor.Text) > 0)
            {
                PedidoReceber_tbxPosicao.Text = clsParser.Int32Parse(PedidoReceber_tbxPosicao.Text).ToString("N0");
                PedidoReceber_tbxPosicaoFim.Text = clsParser.Int32Parse(PedidoReceber_tbxPosicaoFim.Text).ToString("N0");
                PedidoReceber_tbxData.Text = (DateTime.Parse((PedidoReceber_tbxData.Text)).ToString("dd/MM/yyyy"));
                PedidoReceber_tbxValor.Text = clsParser.DecimalParse(PedidoReceber_tbxValor.Text).ToString("N2");
                PedidoReceber_tbxBoletoNro.Text = clsParser.Int32Parse(PedidoReceber_tbxBoletoNro.Text).ToString();
            }
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoes() == true)
            {
                if (tspSalvar.Enabled == false)
                {
                    this.Close();
                }
                else
                {
                    tspSalvar.PerformClick();
                }
            }
            else
            {
                this.Close();
            }

        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }
        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void btnIdCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCliente.Name;
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", idcliente);
            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
        }

        private void btnTransportadora_cognome_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnTransportadora_cognome.Name;
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Transportadoras", idtransportadora);
            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
        }

        private void btnRedespacho_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnRedespacho.Name;
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Transportadoras", idredespacho);
            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
        }

        private void btnFormaPagto_codigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFormaPagto_codigo.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", 0, "Tipo Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnCondpagto_codigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCondpagto_codigo.Name;
            frmCondPagtoPes frmCondPagtoPes = new frmCondPagtoPes();
            frmCondPagtoPes.Init(clsInfo.conexaosqldados, idcondpagto);

            clsFormHelper.AbrirForm(this.MdiParent, frmCondPagtoPes, clsInfo.conexaosqldados);
        }

        private void tspPedido1Incluir_Click(object sender, EventArgs e)
        {
            // Verificar o Proximo Item
            pedido1_item = 0;
            foreach (DataGridViewRow linha in dgvPedido1.Rows)
            {
                pedido1_item = clsParser.Int32Parse(linha.Cells["NITEM"].Value.ToString());
            }
            pedido1_item += 1;

            tclPedidoVenda.SelectedIndex = 1;
            pedido1_posicao = 0;
            gbxPedidoItem.Visible = true;
            Pedido1Carregar();

        }

        private void tspPedido1Alterar_Click(object sender, EventArgs e)
        {
            if (dgvPedido1.CurrentRow != null)
            {
                tclPedidoVenda.SelectedIndex = 1;
                gbxPedidoItem.Visible = true;
                pedido1_posicao = Int32.Parse(dgvPedido1.CurrentRow.Cells["pedido1_posicao"].Value.ToString());
                Pedido1Carregar();
            }

        }

        private void tspPedido1Excluir_Click(object sender, EventArgs e)
        {
            if (dgvPedido1.CurrentRow != null)
            {
                DialogResult drt;

                drt = MessageBox.Show("Deseja o excluir item selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {

                    //foreach (DataRow rowEntrega in dtPedidoOS.Rows)
                    //{
                    //    if (rowEntrega.RowState == DataRowState.Deleted ||
                    //        rowEntrega.RowState == DataRowState.Detached)
                    //    {
                    //        if (rowEntrega["id", DataRowVersion.Original].ToString() == dgvPedido1.CurrentRow.Cells["iditempedidoentrega"].Value.ToString())
                    //        {
                    //            rowEntrega.RejectChanges();
                    //        }
                    //    }
                    //}
                    pedido1_posicao = Int32.Parse(dgvPedido1.CurrentRow.Cells["pedido1_posicao"].Value.ToString());
                    dtPedido1.Select("pedido1_posicao = " + dgvPedido1.CurrentRow.Cells["pedido1_posicao"].Value.ToString())[0].Delete();
                    //Pedido1Carregar();
                    PedidoCalcular();
                }
            }

        }

        private void dgvPedido1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (tspPedido1Alterar.Enabled == true)
            {
                tspPedido1Alterar.PerformClick();
            }
            else
            {
                tclPedidoVenda.SelectedIndex = 1;
                gbxPedidoItem.Visible = true;
                pedido1_posicao = Int32.Parse(dgvPedido1.CurrentRow.Cells["pedido1_posicao"].Value.ToString());
                Pedido1Carregar();
            }

        }

        void Pedido1Carregar()
        {
            clsPedido1Info = new clsPedido1Info();
            clsPedido1InfoOld = new clsPedido1Info();

            if (pedido1_posicao == 0)
            {
                pedido1_posicao = dtPedido1.Rows.Count + 1;
                clsPedido1Info.nitem = pedido1_item;
                clsPedido1Info.idtiponota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tiponota where codigo= '5124-000' "));
                clsPedido1Info.idcfop = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcfopok from tiponota where id= " + clsPedido1Info.idtiponota + ""));
                clsPedido1Info.idcodigo = clsInfo.zpecas;
                clsPedido1Info.idipi = clsInfo.zipi;
                clsPedido1Info.idorcamento = clsInfo.zorcamento;
                clsPedido1Info.idorcamentoitem = clsInfo.zorcamento1;
                clsPedido1Info.idordemservico = clsInfo.zordemservico;
                clsPedido1Info.idpedido = clsInfo.zpedido;
                clsPedido1Info.idsittriba = clsInfo.zsituacaotriba;
                clsPedido1Info.idsittribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCSTICMS from tiponota where id= " + clsPedido1Info.idtiponota + ""));
                clsPedido1Info.idsittribcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCSTCOFINS from tiponota where id= " + clsPedido1Info.idtiponota + ""));
                clsPedido1Info.idsittribipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCSTIPI from tiponota where id= " + clsPedido1Info.idtiponota + ""));
                clsPedido1Info.idsittribpispasep = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCSTPIS from tiponota where id= " + clsPedido1Info.idtiponota + ""));
                clsPedido1Info.idunidade = clsInfo.zunidade;
                clsPedido1Info.totalprevisto = 1; // APENAS PARA PODER INCLUIR UM PEDIDO- ja tendo um pedido em berto

            }
            else
            {
                Pedido1GridToInfo(clsPedido1Info, pedido1_posicao);
            }

            if (id != 0 && PedidoPago == "S")
            {
                gbxCfop.Enabled = false;
                groupBox7.Enabled = false;
                groupBox6.Enabled = false;
                groupBox5.Enabled = false;
                label82.Enabled = false;
                gbxCodigo.Enabled = false;
                gbxItem.Enabled = false;
                gbxTiponota.Enabled = false;
                Item_gbxCentrocusto.Enabled = false;
                gbxDestino.Enabled = false;
                gbxRastreabilidade.Enabled = false;
                gbxQtdepedido.Enabled = false;
                gbxDevolvido.Enabled = false;
                gbxNFE.Enabled = false;
                gbxQtdes.Enabled = false;
                gbxTotal.Enabled = false;
                groupBox1.Enabled = false;
                gbxEnviado.Enabled = false;

            }
            else
            {
                gbxCfop.Enabled = true;
                groupBox7.Enabled = true;
                groupBox6.Enabled = true;
                groupBox5.Enabled = true;
                label82.Enabled = true;
                gbxCodigo.Enabled = true;
                gbxItem.Enabled = true;
                gbxTiponota.Enabled = true;
                Item_gbxCentrocusto.Enabled = true;
                gbxDestino.Enabled = true;
                gbxRastreabilidade.Enabled = true;
                gbxQtdepedido.Enabled = true;
                gbxDevolvido.Enabled = true;
                gbxNFE.Enabled = true;
                gbxQtdes.Enabled = true;
                gbxTotal.Enabled = true;
                groupBox1.Enabled = true;
                gbxEnviado.Enabled = true;
            }

            Pedido1Campos(clsPedido1Info);
            Pedido1FillInfo(clsPedido1InfoOld);

            tbxPecas_codigo.Select();
        }
        void Pedido1GridToInfo(clsPedido1Info info, Int32 posicao)
        {
            DataRow row = dtPedido1.Select("pedido1_posicao = " + posicao)[0];

            info.id = Int32.Parse(row["id"].ToString());

            info.baseicm = clsParser.DecimalParse(row["baseicm"].ToString());
            info.baseicmsubst = clsParser.DecimalParse(row["baseicmsubst"].ToString());
            //info.basemp = clsParser.DecimalParse(row["basemp"].ToString());
            info.calculoautomatico = row["calculoautomatico"].ToString();
            info.codigoemp01 = row["codigoemp01"].ToString();
            info.codigoemp02 = row["codigoemp02"].ToString();
            info.codigoemp03 = row["codigoemp03"].ToString();
            info.codigoemp04 = row["codigoemp04"].ToString();
            info.cofins1 = clsParser.DecimalParse(row["cofins1"].ToString());
            info.complemento = row["complemento"].ToString();
            info.consumo = row["consumo"].ToString();
            info.custoicm = clsParser.DecimalParse(row["custoicm"].ToString());
            info.custoipi = clsParser.DecimalParse(row["custoipi"].ToString());
            info.icm = clsParser.DecimalParse(row["icm"].ToString());
            info.icmsubst = clsParser.DecimalParse(row["icmsubst"].ToString());
            info.idcfop = clsParser.Int32Parse(row["idcfop"].ToString());
            info.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
            info.idipi = clsParser.Int32Parse(row["idipi"].ToString());
            info.idorcamento = clsParser.Int32Parse(row["idorcamento"].ToString());
            info.idorcamentoitem = clsParser.Int32Parse(row["idorcamentoitem"].ToString());
            info.idordemservico = clsParser.Int32Parse(row["idordemservico"].ToString());
            info.idpedido = clsParser.Int32Parse(row["idpedido"].ToString());
            info.idsittriba = clsParser.Int32Parse(row["idsittriba"].ToString());
            info.idsittribb = clsParser.Int32Parse(row["idsittribb"].ToString());
            info.idsittribcofins = clsParser.Int32Parse(row["idsittribcofins"].ToString());
            info.idsittribipi = clsParser.Int32Parse(row["idsittribipi"].ToString());
            //info.idsittribpispasep = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCSTPIS from tiponota where id= " + clsPedido1Info.idtiponota + ""));
            info.idsittribpispasep = clsParser.Int32Parse(row["idsittribpispasep"].ToString());

            info.idtiponota = clsParser.Int32Parse(row["idtiponota"].ToString());
            info.idunidade = clsParser.Int32Parse(row["idunidade"].ToString());
            info.ipi = clsParser.DecimalParse(row["ipi"].ToString());
            info.nitem = clsParser.Int32Parse(row["nitem"].ToString());

            info.idcentrocusto = clsParser.Int32Parse(row["idcentrocusto"].ToString());
            info.idcontacontabil = clsParser.Int32Parse(row["idcontacontabil"].ToString());

            info.peso = clsParser.DecimalParse(row["peso"].ToString());
            info.pispasep = clsParser.DecimalParse(row["pispasep"].ToString());
            info.preco = clsParser.DecimalParse(row["preco"].ToString());
            info.precocusto = clsParser.DecimalParse(row["precocusto"].ToString());
            info.precodesconto = clsParser.DecimalParse(row["precodesconto"].ToString());
            info.precotabela = clsParser.DecimalParse(row["precotabela"].ToString());
            info.qtde = clsParser.DecimalParse(row["qtde"].ToString());
            info.qtdebaixada = clsParser.DecimalParse(row["qtdebaixada"].ToString());
            info.qtdedefeito = clsParser.DecimalParse(row["qtdedefeito"].ToString());
            info.qtdeentregue = clsParser.DecimalParse(row["qtdeentregue"].ToString());
            info.qtdeosaux = clsParser.DecimalParse(row["qtdeosaux"].ToString());
            info.qtdesaldo = clsParser.DecimalParse(row["qtdesaldo"].ToString());
            info.qtdesucata = clsParser.DecimalParse(row["qtdesucata"].ToString());
            info.totalcustoitem = clsParser.DecimalParse(row["totalcustoitem"].ToString());
            info.totalmercado = clsParser.DecimalParse(row["totalmercado"].ToString());
            info.totalnota = clsParser.DecimalParse(row["totalnota"].ToString());
            info.totalpeso = clsParser.DecimalParse(row["totalpeso"].ToString());
            info.totalprevisto = clsParser.DecimalParse(row["totalprevisto"].ToString());
            info.tributo_previsto = clsParser.DecimalParse(row["tributo_previsto"].ToString());
            info.valorfrete = clsParser.DecimalParse(row["valorfrete"].ToString());
            //            info.valorfreteicms = row["valorfreteicms"].ToString();
            info.valoroutras = clsParser.DecimalParse(row["valoroutras"].ToString());
            //            info.valoroutrasicms = row["valoroutrasicms"].ToString();
            info.valorseguro = clsParser.DecimalParse(row["valorseguro"].ToString());
            //            info.valorseguroicms = row["valorseguroicms"].ToString();


        }

        void Pedido1Campos(clsPedido1Info info)
        {
            pedido1_id = info.id;

            tbxBaseicm.Text = info.baseicm.ToString("N2");
            tbxBaseicmsubst.Text = info.baseicmsubst.ToString("N2");
//            tbxBaseMP.Text = info.basemp.ToString("N2");

            if (info.calculoautomatico == "N")
            {
                ckxCalculoautomatico.Checked = false;
                ckxCalculoautomatico.Text = "Não fara o Calculo Automatico";
            }
            else
            {
                ckxCalculoautomatico.Checked = true;
                ckxCalculoautomatico.Text = "Sim o Calculo será Automatico";
            }

            CalculoAutomatico();

            tbxCodigoemp01.Text = info.codigoemp01;
            tbxCodigoemp02.Text = info.codigoemp02;
            tbxCodigoemp03.Text = info.codigoemp03;
            tbxCodigoemp04.Text = info.codigoemp04;

            tbxCofins1.Text = info.cofins1.ToString("N2");
            tbxComplemento.Text = info.complemento;

            cbxConsumo.SelectedIndex = cbxConsumo.FindString(info.consumo);
            if (cbxConsumo.SelectedIndex == -1)
            {
                cbxConsumo.SelectedIndex = 0;
            }
            tbxCustoicm.Text = info.custoicm.ToString("N2");
            tbxCustoipi.Text = info.custoipi.ToString("N2");
            tbxIcm.Text = info.icm.ToString("N2");
            tbxIcmsubst.Text = info.icmsubst.ToString("N2");
            pedido1_idcfop = info.idcfop;
            // CAMPOS QUE NÃO SAO DA BASE
            if (pedido1_idcfop == 0)
            {
                pedido1_idcfop = clsInfo.zcfop;
            }
            tbxCfop_cfop.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cfop from cfop where ID= " + pedido1_idcfop);
            pedido1_idcodigo = info.idcodigo;
            pedido1_idipi = info.idipi;
            pedido1_idorcamento = info.idorcamento;
            pedido1_idorcamentoitem = info.idorcamentoitem;
            pedido1_idordemservico = info.idordemservico;
            pedido1_idpedido = id;
            pedido1_idsittriba = info.idsittriba;
            pedido1_idsittribb = info.idsittribb;
            pedido1_idsittribcofins = info.idsittribcofins;
            if (pedido1_idsittribcofins == 0)
            {
                pedido1_idsittribcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from sittribcofins where CODIGO= '" + "07" + "' "));
            }
            pedido1_idsittribipi = info.idsittribipi;
            if (pedido1_idsittribipi == 0)
            {
                pedido1_idsittribipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from sittribipi where CODIGO= '" + "52" + "' "));
            }
            pedido1_idsittribpispasep = info.idsittribpispasep;
            if (pedido1_idsittribpispasep == 0)
            {
                pedido1_idsittribpispasep = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from sittribpis where CODIGO= '" + "07" + "' "));
            }
            pedido1_idtiponota = info.idtiponota;
            if (pedido1_idtiponota == 0)
            {
                pedido1_idtiponota = clsInfo.ztiponota;
            }
            pedido1_idunidade = info.idunidade;
            tbxIpi.Text = info.ipi.ToString("N2");
            tbxItem.Text = info.nitem.ToString("N0");
            tbxPeso.Text = info.peso.ToString("N3");
            tbxPispasep.Text = info.pispasep.ToString("N2");
            tbxPreco.Text = info.preco.ToString("N4");
            tbxPrecoCusto.Text = info.precocusto.ToString("N4");
            tbxPrecoDesconto.Text = info.precodesconto.ToString("N4");
            tbxPrecoTabela.Text = info.precotabela.ToString("N4");
            tbxQtde.Text = info.qtde.ToString("N2");
            Pedido1_tbxQtdebaixada.Text = info.qtdebaixada.ToString("N2");
            Pedido1_tbxQtdedefeito.Text = info.qtdedefeito.ToString("N2");
            Pedido1_tbxQtdeEntregue.Text = info.qtdeentregue.ToString("N2");
            Pedido1_tbxQtdeosaux.Text = info.qtdeosaux.ToString("N2");
            Pedido1_tbxQtdesaldo.Text = info.qtdesaldo.ToString("N2");
            Pedido1_tbxQtdesucata.Text = info.qtdesucata.ToString("N2");
            tbxTotalCustoItem.Text = info.totalcustoitem.ToString("N2");
            tbxTotalmercado.Text = info.totalmercado.ToString("N2");
            tbxTotalnota.Text = info.totalnota.ToString("N2");
            tbxTotalPeso1.Text = info.totalpeso.ToString("N3");
            tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from IPI where ID= " + pedido1_idipi);
            //tbxTotalDesconto.Text = info.totalprevisto.ToString("N2");
            //            tbxValorfrete.Text = info.valorfrete.ToString("N2");
            //            tbxFretevaloricms.Text = info.valorfreteicms.ToString("N2");
            tbxTotaloutras.Text = info.valoroutras.ToString("N2");
            tbxTributo_Previsto.Text = info.tributo_previsto.ToString("N2");

            //info.valoroutrasicms.ToString("N2");

            //            tbxValorseguro.Text = info.valorseguro.ToString("N2");
            //info.valorseguroicms;


            if (pedido1_idcodigo == 0)
            {
                pedido1_idcodigo = clsInfo.zpecas;
            }
            tbxPecas_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where ID= " + pedido1_idcodigo);
            tbxPecas_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where ID= " + pedido1_idcodigo);
            if (pedido1_idipi == 0)
            {
                pedido1_idipi = clsInfo.zipi;
            }
            tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from ipi where ID= " + pedido1_idipi);
            if (pedido1_idorcamento == 0)
            {
                pedido1_idorcamento = clsInfo.zorcamento;
            }
            tbxOrcamento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from orcamento where ID= " + pedido1_idorcamento);
            if (pedido1_idordemservico == 0)
            {
                pedido1_idordemservico = clsInfo.zordemservico;
            }
            tbxOrdemservico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from ordemservico where ID= " + pedido1_idordemservico);

            cbxSittriba.SelectedIndex = cbxSittriba.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittributariaa where ID= " + pedido1_idsittriba));
            if (cbxSittriba.SelectedIndex == -1)
            {
                cbxSittriba.SelectedIndex = 0;
            }
            cbxSittribb.SelectedIndex = cbxSittribb.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittributariab where ID= " + pedido1_idsittribb));
            if (cbxSittribb.SelectedIndex == -1)
            {
                cbxSittribb.SelectedIndex = 0;
            }
            cbxSittribcofins1.SelectedIndex = cbxSittribcofins1.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribcofins where ID= " + pedido1_idsittribcofins));
            if (cbxSittribcofins1.SelectedIndex == -1)
            {
                cbxSittribcofins1.SelectedIndex = 0;
            }
            cbxSittribipi.SelectedIndex = cbxSittribipi.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribipi where ID= " + pedido1_idsittribipi));
            if (cbxSittribipi.SelectedIndex == -1)
            {
                cbxSittribipi.SelectedIndex = 0;
            }
            cbxSittribpispasep.SelectedIndex = cbxSittribpispasep.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribpis where ID= " + pedido1_idsittribpispasep));
            if (cbxSittribpispasep.SelectedIndex == -1)
            {
                cbxSittribpispasep.SelectedIndex = 0;
            }
            cbxTiponota.SelectedIndex = cbxTiponota.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tiponota where ID= " + pedido1_idtiponota));
            if (cbxTiponota.SelectedIndex == -1)
            {
                cbxTiponota.SelectedIndex = 0;
            }
            tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where ID= " + pedido1_idunidade);
            /// Pegar o numero da nota fiscal de entrada se tiver
            tbxNFEntrada.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nfcompra.numero from nfcompra inner join nfcompra1 on nfcompra1.numero=nfcompra.id where nfcompra1.idpedidovendaitem= " + pedido1_id)).ToString("N0");

            //
            pedido1_idcentocusto = info.idcentrocusto;
            if (pedido1_idcentocusto == 0)
            {
                pedido1_idcentocusto = clsInfo.zcentrocustos;
            }
            tbxCentrocusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where id=" + pedido1_idcentocusto);
            //

            pedido1_idcontacontabil = info.idcontacontabil;
            if (pedido1_idcontacontabil == 0)
            {
                pedido1_idcontacontabil = clsInfo.zcontacontabil;
            }
            tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where id=" + pedido1_idcontacontabil);

            bwrMaterialDevolvido_Run();
            //Pedido1CalcularSaldo();
        }

        void Pedido1FillInfo(clsPedido1Info info)
        {
            info.id = pedido1_id;

            info.baseicm = clsParser.DecimalParse(tbxBaseicm.Text);
            info.baseicmsubst = clsParser.DecimalParse(tbxBaseicmsubst.Text);
//            info.basemp = clsParser.DecimalParse(tbxBaseMP.Text);
            if (ckxCalculoautomatico.Checked == false)
            {
                info.calculoautomatico = "N";
            }
            else
            {
                info.calculoautomatico = "S";
            }
            info.codigoemp01 = tbxCodigoemp01.Text;
            info.codigoemp02 = tbxCodigoemp02.Text;
            info.codigoemp03 = tbxCodigoemp03.Text;
            info.codigoemp04 = tbxCodigoemp04.Text;
            info.cofins1 = clsParser.DecimalParse(tbxCofins1.Text);
            info.complemento = tbxComplemento.Text;
            info.consumo = cbxConsumo.Text.Substring(0, 1);
            info.custoicm = clsParser.DecimalParse(tbxCustoicm.Text);
            info.custoipi = clsParser.DecimalParse(tbxCustoipi.Text);
            info.icm = clsParser.DecimalParse(tbxIcm.Text);
            info.icmsubst = clsParser.DecimalParse(tbxIcmsubst.Text);
            info.idcfop = pedido1_idcfop;
            info.idcodigo = pedido1_idcodigo;
            info.idipi = pedido1_idipi;
            info.idorcamento = pedido1_idorcamento;
            info.idorcamentoitem = pedido1_idorcamentoitem;
            info.idordemservico = pedido1_idordemservico;
            info.idpedido = pedido1_idpedido;
            info.idsittriba = pedido1_idsittriba;
            info.idsittribb = pedido1_idsittribb;
            info.idsittribcofins = pedido1_idsittribcofins;
            info.idsittribipi = pedido1_idsittribipi;
            info.idsittribpispasep = pedido1_idsittribpispasep;

            info.idcentrocusto = pedido1_idcentocusto;
            info.idcontacontabil = pedido1_idcontacontabil;

            info.idtiponota = pedido1_idtiponota;
            info.idunidade = pedido1_idunidade;
            info.ipi = clsParser.DecimalParse(tbxIpi.Text);
            info.nitem = clsParser.Int32Parse(tbxItem.Text);
            info.peso = clsParser.DecimalParse(tbxPeso.Text);
            info.pispasep = clsParser.DecimalParse(tbxPispasep.Text);
            info.preco = clsParser.DecimalParse(tbxPreco.Text);
            info.precocusto = clsParser.DecimalParse(tbxPrecoCusto.Text);
            info.precodesconto = clsParser.DecimalParse(tbxPrecoDesconto.Text);
            info.precotabela = clsParser.DecimalParse(tbxPrecoTabela.Text);
            info.qtde = clsParser.DecimalParse(tbxQtde.Text);
            info.qtdebaixada = clsParser.DecimalParse(Pedido1_tbxQtdebaixada.Text);
            info.qtdedefeito = clsParser.DecimalParse(Pedido1_tbxQtdedefeito.Text);
            info.qtdeentregue = clsParser.DecimalParse(Pedido1_tbxQtdeEntregue.Text);
            info.qtdeosaux = clsParser.DecimalParse(Pedido1_tbxQtdeosaux.Text);
            info.qtdesaldo = clsParser.DecimalParse(Pedido1_tbxQtdesaldo.Text);
            info.qtdesucata = clsParser.DecimalParse(Pedido1_tbxQtdesucata.Text);
            info.totalcustoitem = clsParser.DecimalParse(tbxTotalCustoItem.Text);
            info.totalmercado = clsParser.DecimalParse(tbxTotalmercado.Text);
            info.totalnota = clsParser.DecimalParse(tbxTotalnota.Text);
            info.totalpeso = clsParser.DecimalParse(tbxTotalpeso.Text);
            info.totalprevisto = clsParser.DecimalParse(tbxTotalPrevistoItem.Text);
            info.tributo_previsto = clsParser.DecimalParse(tbxTributo_Previsto.Text);
            info.valorfrete = clsParser.DecimalParse("0".ToString());
            //            info.valorfreteicms = row["valorfreteicms"].ToString();
            //            info.valoroutras = clsParser.DecimalParse(row["valoroutras"].ToString());
            //            info.valoroutrasicms = row["valoroutrasicms"].ToString();
            info.valorseguro = clsParser.DecimalParse("0".ToString());
            //info.valorseguroicms = row["valorseguroicms"].ToString();

        }

        void Pedido1FillInfoToGrid(clsPedido1Info info)
        {
            DataRow row;
            DataRow[] rows = dtPedido1.Select("pedido1_posicao = " + pedido1_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtPedido1.NewRow();
            }

            row["id"] = pedido1_id;

            row["baseicm"] = clsParser.DecimalParse(tbxBaseicm.Text);
            row["baseicmsubst"] = clsParser.DecimalParse(tbxBaseicmsubst.Text);
//            row["basemp"] = clsParser.DecimalParse(tbxBaseMP.Text);
            if (ckxCalculoautomatico.Checked == false)
            {
                row["calculoautomatico"] = "N";
            }
            else
            {
                row["calculoautomatico"] = "S";
            }
            row["codigoemp01"] = tbxCodigoemp01.Text;
            row["codigoemp02"] = tbxCodigoemp02.Text;
            row["codigoemp03"] = tbxCodigoemp03.Text;
            row["codigoemp04"] = tbxCodigoemp04.Text;
            row["cofins1"] = clsParser.DecimalParse(tbxCofins1.Text);
            row["complemento"] = tbxComplemento.Text;
            row["consumo"] = cbxConsumo.Text.Substring(0, 1);
            row["custoicm"] = clsParser.DecimalParse(tbxCustoicm.Text);
            row["custoipi"] = clsParser.DecimalParse(tbxCustoipi.Text);
            row["icm"] = clsParser.DecimalParse(tbxIcm.Text);
            row["icmsubst"] = clsParser.DecimalParse(tbxIcmsubst.Text);
            row["idcfop"] = pedido1_idcfop;
            row["idcodigo"] = pedido1_idcodigo;
            row["idipi"] = pedido1_idipi;
            row["idorcamento"] = pedido1_idorcamento;
            row["idorcamentoitem"] = pedido1_idorcamentoitem;
            row["idordemservico"] = pedido1_idordemservico;
            row["idpedido"] = pedido1_idpedido;
            row["idsittriba"] = pedido1_idsittriba;
            row["idsittribb"] = pedido1_idsittribb;
            row["idsittribcofins"] = pedido1_idsittribcofins;
            row["idsittribipi"] = pedido1_idsittribipi;
            row["idsittribpispasep"] = pedido1_idsittribpispasep;
            row["idtiponota"] = pedido1_idtiponota;
            row["idunidade"] = pedido1_idunidade;
            row["ipi"] = clsParser.DecimalParse(tbxIpi.Text);
            row["nitem"] = clsParser.Int32Parse(tbxItem.Text);

            row["idcentrocusto"] = pedido1_idcentocusto;
            row["idcontacontabil"] = pedido1_idcontacontabil;

            row["peso"] = clsParser.DecimalParse(tbxPeso.Text);
            row["pispasep"] = clsParser.DecimalParse(tbxPispasep.Text);
            row["preco"] = clsParser.DecimalParse(tbxPreco.Text);
            row["precocusto"] = clsParser.DecimalParse(tbxPrecoCusto.Text);
            row["precodesconto"] = clsParser.DecimalParse(tbxPrecoDesconto.Text);
            row["precotabela"] = clsParser.DecimalParse(tbxPrecoTabela.Text);
            row["qtde"] = clsParser.DecimalParse(tbxQtde.Text);
            row["qtdebaixada"] = clsParser.DecimalParse(Pedido1_tbxQtdebaixada.Text);
            row["qtdedefeito"] = clsParser.DecimalParse(Pedido1_tbxQtdedefeito.Text);
            row["qtdeentregue"] = clsParser.DecimalParse(Pedido1_tbxQtdeEntregue.Text);
            row["qtdeosaux"] = clsParser.DecimalParse(Pedido1_tbxQtdeosaux.Text);
            row["qtdesaldo"] = clsParser.DecimalParse(Pedido1_tbxQtdesaldo.Text);
            row["qtdesucata"] = clsParser.DecimalParse(Pedido1_tbxQtdesucata.Text);
            //row["reducao"] = clsParser.DecimalParse(tbxReducao.Text);
            row["totalcustoitem"] = clsParser.DecimalParse(tbxTotalCustoItem.Text);
            row["totalmercado"] = clsParser.DecimalParse(tbxTotalmercado.Text);
            row["totalnota"] = clsParser.DecimalParse(tbxTotalnota.Text);
            row["totalpeso"] = clsParser.DecimalParse(tbxTotalpeso.Text);
            row["totalprevisto"] = clsParser.DecimalParse(tbxTotalPrevistoItem.Text);
            row["tributo_previsto"] = clsParser.DecimalParse(tbxTributo_Previsto.Text);
            row["valorfrete"] = clsParser.DecimalParse("0".ToString());

            // dados externos
            row["CODIGO"] = tbxPecas_codigo.Text;
            if (tbxPecas_codigo.Text == "0")
            {
                row["DESCRICAO"] = tbxComplemento.Text;
            }
            else
            {
                row["DESCRICAO"] = tbxPecas_nome.Text;
            }
                row["tiponota"] = cbxTiponota.Text.PadRight(8, ' ').Substring(0, 8);
            row["cfop"] = tbxCfop_cfop.Text;



            if (rows.Length == 0)
            {
                row["pedido1_posicao"] = pedido1_posicao;
                dtPedido1.Rows.Add(row);
            }
        }

        private void tspPedido1Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar e alterar?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    // Verificar se o tipo da nota é igual ao que esta no id.
                    String tipoNota1 = cbxTiponota.Text.PadRight(8, ' ').Substring(0, 8);
                    pedido1_idtiponota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tiponota where codigo = '" + tipoNota1 + "' ", "0"));
                    


                    Pedido1Salvar();
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspPedido1Primeiro_Click(object sender, EventArgs e)
        {

        }

        private void tspPedido1Anterior_Click(object sender, EventArgs e)
        {

        }

        private void tspPedido1Proximo_Click(object sender, EventArgs e)
        {

        }

        private void tspPedido1Ultimo_Click(object sender, EventArgs e)
        {

        }

        private void tspPedido1Retornar_Click(object sender, EventArgs e)
        {

            gbxPedidoItem.Visible = false;
            tclPedidoVenda.SelectedIndex = 0;

        }

        private void Pedido1Salvar()
        {
            clsPedido1Info = new clsPedido1Info();
            Pedido1FillInfo(clsPedido1Info);
            clsPedido1BLL clsPedido1BLL = new clsPedido1BLL();
            clsPedido1BLL.VerificaInfo(clsPedido1Info);
            Pedido1FillInfoToGrid(clsPedido1Info);

            gbxPedidoItem.Visible = false;
            tclPedidoVenda.SelectedIndex = 0;

            //NFCompraSomar();
        }

        private void btnCfop_codigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCfop_codigo.Name;
            frmCfopPes frmCfopPes = new frmCfopPes();
            frmCfopPes.Init(pedido1_idcfop);

            clsFormHelper.AbrirForm(this.MdiParent, frmCfopPes, clsInfo.conexaosqldados);

        }

        private void btnPecas_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPecas.Name;
            if (rbnSituacao2.Checked == true)
            {
                frmPecasPes frmPecasPes = new frmPecasPes();
                frmPecasPes.Init(0);

                clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
            }
            else
            {
                frmPecasPes frmPecasPes = new frmPecasPes();
                frmPecasPes.Init(0);

                clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
            }
        }

        private void btnClassfiscal_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnClassfiscal.Name;
            frmIpiPes frmIpiPes = new frmIpiPes();
            frmIpiPes.Init(pedido1_idipi);

            clsFormHelper.AbrirForm(this.MdiParent, frmIpiPes, clsInfo.conexaosqldados);
        }

        private void btnSitTriba_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSitTriba.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBUTARIAA", pedido1_idsittriba, "Situação Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnSitTribb_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSitTribb.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBUTARIAB", pedido1_idsittribb, "Situação Tributaria B");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnSittribipi_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSittribipi.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBIPI", pedido1_idsittribipi, "Situação IPI");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnSittribpispasep_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSittribpispasep.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBPIS", pedido1_idsittribpispasep, "Situação Pis");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnSittribcofins1_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSittribcofins1.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBCOFINS", pedido1_idsittribcofins, "Situação Cofins");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void TipoNotaCarregar()
        {
            pedido1_idcfop = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcfopok from tiponota where id=" + pedido1_idtiponota));
            if (pedido1_idcfop == 0)
            {
                pedido1_idcfop = clsInfo.zcfop;
            }
            tbxCfop_cfop.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cfop from cfop where id = " + pedido1_idcfop + " ");

            tiponota_devolucao = (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select devolucao from tiponota where id=" + pedido1_idtiponota) == "S");

            pedido1_idsittribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcsticms from tiponota where id=" + pedido1_idtiponota));
            cbxSittribb.SelectedIndex = cbxSittribb.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittributariab where ID= " + pedido1_idsittribb));
            if (cbxSittribb.SelectedIndex == -1)
            {
                cbxSittribb.SelectedIndex = 0;
            }
            pedido1_idsittribipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcstipi from tiponota where id=" + pedido1_idtiponota));
            cbxSittribipi.SelectedIndex = cbxSittribipi.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribipi where ID= " + pedido1_idsittribipi));
            if (cbxSittribipi.SelectedIndex == -1)
            {
                cbxSittribipi.SelectedIndex = 0;
            }
            pedido1_idsittribpispasep = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcstpis from tiponota where id=" + pedido1_idtiponota));
            cbxSittribpispasep.SelectedIndex = cbxSittribpispasep.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribpis where ID= " + pedido1_idsittribpispasep));
            if (cbxSittribpispasep.SelectedIndex == -1)
            {
                cbxSittribpispasep.SelectedIndex = 0;
            }
            pedido1_idsittribcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcstcofins from tiponota where id=" + pedido1_idtiponota));
            cbxSittribcofins1.SelectedIndex = cbxSittribcofins1.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribcofins where ID= " + pedido1_idsittribcofins));
            if (cbxSittribcofins1.SelectedIndex == -1)
            {
                cbxSittribcofins1.SelectedIndex = 0;
            }

            // CarregaImpostos(); 
        }
        void CarregaImpostos()
        {
            // Carrega os impostos
            // Icms
            //if (cbxSittriba.SelectedIndex != -1 && cbxSittriba.Text.Substring(0, 1) == "1") // Se for de origem estrangeira, utiliza-se alíquota interna do estado
            //{
            //    tbxIcm.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + clsInfo.zipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
            //    tbxReducao.Text = clsParser.DecimalParse("0").ToString("n2");
            //}
            //else
            //{
            //    if (tiponota_devolucao == true) // Beneficiamento
            //    {
            //        tbxIcm.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + pedido1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
            //        tbxReducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducao from ipiestadosicms where idipi=" + pedido1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
            //    }
            //    else
            //    {
            //        tbxIcm.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + pedido1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
            //        tbxReducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducao from ipiestadosicms where idipi=" + pedido1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
            //    }
            //}



            Int32 daUF = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idestado from cliente where id=" + idcliente));
            tbxIcm.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ESTADOSICMS.ALIQUOTA From ESTADOSICMS inner join estados on estados.id=estadosicms.idestado inner join estados estados1 on estados.id=estadosicms.IDESTADODESTINO WHERE ESTADOS.ID = " + clsInfo.zempresa_ufid + " AND ESTADOS1.ID=" + daUF + "")).ToString("n2");
            tbxReducao.Text = "0";


            // ST
            tbxIcmsubst.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + pedido1_idipi + " and idestado = " + idufdestino + " and idestadodestino = " + idufdestino)).ToString("n2");
            tbxIcmssubstreducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducaoiva from ipiestadosicms where idipi=" + pedido1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
            if (idufdestino != clsInfo.zempresa_ufid && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + idufdestino) == "MT")
            {
                tbxMva.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from tab_cnae_uf where idtabcnae=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcnae from cliente where id=" + clsPedidoInfo.idcliente) + " and iduf=" + idufdestino)).ToString("n2");
            }
            else
            {
                tbxMva.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from ipiestadosicms where idipi=" + pedido1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
            }


            if (clsParser.DecimalParse(tbxIcm.Text) == 0)
            {
                if (tbxPecas_codigo.Text != "0")
                {
                    ////MessageBox.Show("Entre na Tabela de Ipi na Classificação Fiscal deste item " + Environment.NewLine +
                    ////                "Verifique se os Estados da Federação estão cadastrados ?? " + Environment.NewLine +
                    ////                "Se estiverem CLICK no Botão Salvar para Confirmar ");
                }
            }
        }

        private void Pedido1Calcular()
        {
            if (ckxCalculoautomatico.Checked == true && cbxSittriba.SelectedIndex != -1 && cbxConsumo.SelectedIndex != -1)
            {
                clsNotafiscal NF = new clsNotafiscal();
                NF.iduforigem = clsInfo.zempresa_ufid;
                NF.idufdestino = idufdestino;
                NF.idipi = pedido1_idipi;  // Ipi deve ser visto na introdução
                NF.idcliente = idcliente;
                NF.revendedor = (cbxRevendedor.Checked == true);
                NF.isentoipi = (cbxIsentoipi.Checked == true);
                NF.arealivrecomercio = (cbxZfm.Checked == true);
                NF.consumo = (cbxConsumo.Text.Substring(0, 1) == "S");

                NF.qtde = clsParser.DecimalParse(tbxQtde.Text);
                NF.qtdesaldo = clsParser.DecimalParse(Pedido1_tbxQtdesaldo.Text);

                NF.preco = clsParser.DecimalParse(tbxPreco.Text);

                NF.pesounit = clsParser.DecimalParse(tbxPeso.Text);
                NF.pesototal = clsParser.DecimalParse(tbxTotalPeso1.Text);

                NF.origem = cbxSittriba.Text.Substring(0, 1);
                NF.icmscst = cbxSittribb.Text.Substring(0, 2);
                NF.icms = clsParser.DecimalParse(tbxIcm.Text);
                NF.icmsreducao = clsParser.DecimalParse(tbxReducao.Text);
//                NF.basemp = clsParser.DecimalParse(tbxBaseMP.Text);

                NF.icmsst = clsParser.DecimalParse(tbxIcmsubst.Text);
                NF.icmsstreducao = clsParser.DecimalParse(tbxIcmssubstreducao.Text);
                NF.icmsstmva = clsParser.DecimalParse(tbxMva.Text);

                NF.ipicst = cbxSittribipi.Text.Substring(0, 2);
                NF.ipi = clsParser.DecimalParse(tbxIpi.Text);

                NF.piscst = cbxSittribpispasep.Text.Substring(0, 2);
                NF.pis = clsParser.DecimalParse(tbxAliqpispasep.Text);
                NF.cofinscst = cbxSittribcofins1.Text.Substring(0, 2);
                NF.cofins = clsParser.DecimalParse(tbxAliqcofins1.Text);

                NF.CalcularNota();

                cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex(NF.ipicst, 2, cbxSittribipi);
                if (cbxSittribipi.SelectedIndex == -1)
                {
                    cbxSittribipi.SelectedIndex = 0;
                }
                cbxSittribb.SelectedIndex = clsVisual.SelecionarIndex(NF.icmscst, 2, cbxSittribb);
                if (cbxSittribb.SelectedIndex == -1)
                {
                    cbxSittribb.SelectedIndex = 0;
                }
                cbxSittribpispasep.SelectedIndex = clsVisual.SelecionarIndex(NF.piscst, 2, cbxSittribpispasep);
                if (cbxSittribpispasep.SelectedIndex == -1)
                {
                    cbxSittribpispasep.SelectedIndex = 0;
                }
                cbxSittribcofins1.SelectedIndex = clsVisual.SelecionarIndex(NF.cofinscst, 2, cbxSittribcofins1);
                if (cbxSittribcofins1.SelectedIndex == -1)
                {
                    cbxSittribcofins1.SelectedIndex = 0;
                }

                tbxPreco.Text = NF.preco.ToString("n4");
                tbxIcm.Text = NF.icms.ToString("n2");
                tbxReducao.Text = NF.icmsreducao.ToString("n2");
                tbxBaseicm.Text = NF.icmsbc.ToString("N2");
                tbxCustoicm.Text = NF.icmstotal.ToString("N2");
                tbxBaseicmsubst.Text = NF.icmsstbc.ToString("N2");
                tbxIcmsubst.Text = NF.icmsst.ToString("N2");
                tbxIcmssubstreducao.Text = NF.icmsstreducao.ToString("N2");
                tbxMva.Text = NF.icmsstmva.ToString("N2");
                tbxIcmssubsttotal.Text = NF.icmssttotal.ToString("N2");
                tbxBcipi.Text = NF.ipibc.ToString("N2");
                tbxIpi.Text = NF.ipi.ToString("N2");
                tbxCustoipi.Text = NF.ipitotal.ToString("N2");
                tbxBcpispasep.Text = NF.pisbc.ToString("N2");
                tbxAliqpispasep.Text = NF.pis.ToString("N2");
                tbxPispasep.Text = NF.pistotal.ToString("N2");
                tbxBccofins1.Text = NF.cofinsbc.ToString("N2");
                tbxAliqcofins1.Text = NF.cofins.ToString("N2");
                tbxCofins1.Text = NF.cofinstotal.ToString("N2");
                tbxTotalmercado.Text = NF.totalmercadoria.ToString("N2");
                tbxTotalnota.Text = NF.totalnota.ToString("N2");
                tbxTotalPeso1.Text = NF.pesototal.ToString("N3");
                tbxTotalPrevistoItem.Text = NF.totalprevisto.ToString("N2");
            }

            if (id > 0 && clsParser.DecimalParse(tbxQtde.Text) > 0 && clsParser.DecimalParse(tbxQtde.Text) == clsParser.DecimalParse(tbxQtdeosaux.Text))
            {
                //cancelado = "S";
            }
        }
        private void Pedido1CalcularSaldo()
        {
            Decimal Qtdedevolvida = 0;
            Decimal Qtdeenviada = 0;
            if (dtMaterialEnviado != null)
            {
                Pedido1_tbxQtdeEntregue.Text = "0";
                Pedido1_tbxQtdedefeito.Text = "0";
                Pedido1_tbxQtdesucata.Text = "0";
                Pedido1_tbxQtdebaixada.Text = "0";
                //Pedido1_tbxQtdeosaux.Text = "0";
                Pedido1_tbxQtdesaldo.Text = "0";
                foreach (DataRow row in dtMaterialEnviado.Rows)
                {
                    if (row["tipoitem"].ToString() == "00")
                    {
                        Pedido1_tbxQtdeEntregue.Text = (clsParser.DecimalParse(Pedido1_tbxQtdeEntregue.Text) + clsParser.DecimalParse(row["qtde"].ToString())).ToString("N2");
                    }
                    else if (row["tipoitem"].ToString() == "01")
                    {
                        Pedido1_tbxQtdedefeito.Text = (clsParser.DecimalParse(Pedido1_tbxQtdedefeito.Text) + clsParser.DecimalParse(row["qtde"].ToString())).ToString("N2");
                    }
                    else if (row["tipoitem"].ToString() == "02")
                    {
                        Pedido1_tbxQtdesucata.Text = (clsParser.DecimalParse(Pedido1_tbxQtdesucata.Text) + clsParser.DecimalParse(row["qtde"].ToString())).ToString("N2");
                    }
                    else if (row["tipoitem"].ToString() == "03")
                    {
                        Pedido1_tbxQtdebaixada.Text = (clsParser.DecimalParse(Pedido1_tbxQtdebaixada.Text) + clsParser.DecimalParse(row["qtde"].ToString())).ToString("N2");
                    }
                    else
                    {
                        // Pedido1_tbxQtdeosaux.Text = (clsParser.DecimalParse(Pedido1_tbxQtdeosaux.Text) + clsParser.DecimalParse(row["qtde"].ToString())).ToString("N0");
                    }
                    Qtdeenviada = Qtdeenviada + clsParser.DecimalParse(row["qtde"].ToString());
                }

            }
            if (dtMaterialDevolvido != null)
            {
                // Como tem Faturamento - Travar tudo

                Qtdedevolvida = 0;
                foreach (DataRow row in dtMaterialDevolvido.Rows)
                {
                    Qtdedevolvida = (Qtdedevolvida + clsParser.DecimalParse(row["qtde"].ToString()));
                }
            }
            // Comparar se qtde emitida > qtde devolvida ou vice versa
            if (Qtdedevolvida > Qtdeenviada)
            {
                Pedido1_tbxQtdeosaux.Text = (clsParser.DecimalParse(Pedido1_tbxQtdeosaux.Text) + (Qtdedevolvida - Qtdeenviada)).ToString("N2");
            }
            else if (Qtdeenviada > Qtdedevolvida)
            {
                MessageBox.Show(" Problema no calculo do Saldo ?? - Aplisoft deve analisar esta situação");
            }
            Pedido1_tbxQtdeEntregue.Text = clsParser.DecimalParse(Pedido1_tbxQtdeEntregue.Text).ToString();
            Pedido1_tbxQtdedefeito.Text = clsParser.DecimalParse(Pedido1_tbxQtdedefeito.Text).ToString();
            Pedido1_tbxQtdesucata.Text = clsParser.DecimalParse(Pedido1_tbxQtdesucata.Text).ToString();
            Pedido1_tbxQtdebaixada.Text = clsParser.DecimalParse(Pedido1_tbxQtdebaixada.Text).ToString();
            Pedido1_tbxQtdeosaux.Text = clsParser.DecimalParse(Pedido1_tbxQtdeosaux.Text).ToString();

            Pedido1_tbxQtdesaldo.Text = (clsParser.DecimalParse(tbxQtde.Text) -
                                         (clsParser.DecimalParse(Pedido1_tbxQtdeEntregue.Text) +
                                         clsParser.DecimalParse(Pedido1_tbxQtdedefeito.Text) +
                                         clsParser.DecimalParse(Pedido1_tbxQtdesucata.Text) +
                                         clsParser.DecimalParse(Pedido1_tbxQtdebaixada.Text) +
                                         clsParser.DecimalParse(Pedido1_tbxQtdeosaux.Text))).ToString("N2");
            tbxPreco.Text = (clsParser.DecimalParse(tbxPrecoTabela.Text) - clsParser.DecimalParse(tbxPrecoDesconto.Text)).ToString("N4");
            tbxTotalmercado.Text = (clsParser.DecimalParse(tbxPreco.Text) * clsParser.DecimalParse(tbxQtde.Text)).ToString("N2");
            tbxTotalCustoItem.Text = (clsParser.DecimalParse(tbxPrecoCusto.Text) * clsParser.DecimalParse(tbxQtde.Text)).ToString("N2");
            tbxTotalPrevistoItem.Text = (clsParser.DecimalParse(Pedido1_tbxQtdesaldo.Text) * clsParser.DecimalParse(tbxPreco.Text)).ToString("N2");
            if (clsParser.DecimalParse(Pedido1_tbxQtdesaldo.Text) == 0)
            {
                btnBaixarPedido.Enabled = false;
            }
            else
            {
                btnBaixarPedido.Enabled = true;
            }

        }
        private void tbxEmitente_TextChanged(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnVerDevolvido_Click(object sender, EventArgs e)
        {
            if (gbxDevolvido.Width > 300)
            {
                gbxEnviado.Visible = true;
                gbxNFE.Visible = true;
                gbxDevolvido.Width = 225;
                gbxDevolvido.Height = 110;

            }
            else
            {
                gbxEnviado.Visible = false;
                gbxNFE.Visible = false;
                gbxDevolvido.Width = 585;
                gbxDevolvido.Height = 110;
            }

        }

        private void btnVerCobrado_Click(object sender, EventArgs e)
        {

        }
        private void bwrMaterialDevolvido_Run()
        {
            bwrMaterialDevolvido = new BackgroundWorker();
            bwrMaterialDevolvido.DoWork += new DoWorkEventHandler(bwrMaterialDevolvido_DoWork);
            bwrMaterialDevolvido.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrMaterialDevolvido_RunWorkerCompleted);
            bwrMaterialDevolvido.RunWorkerAsync();
        }

        private void bwrMaterialDevolvido_DoWork(object sender, DoWorkEventArgs e)
        {

            idNFEntrada = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT id FROM NFCOMPRA1 WHERE IDPEDIDOVENDAITEM = " + pedido1_id + " "));
            //dtMaterialDevolvido = clsNfvendaBLL.CarregaGridMaterialDevolvido("", idNFEntrada);
            //dtMaterialEnviado = clsNfvendaBLL.CarregaGridMaterialEnviado("40", idNFEntrada, idNFEntrada);
        }

        private void bwrMaterialDevolvido_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {


                // material devolvido
                //clsNfvendaBLL.GridMontaMaterialDevolvido(dgvMaterialDevolvido, dtMaterialDevolvido, 0);
                ////material enviado
                //clsNfvendaBLL.GridMontaMaterialEnviado(dgvMaterialEnviado, dtMaterialEnviado, 0);

                Pedido1CalcularSaldo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PedidoCalcular()
        {
                tbxQtdePedido.Text = "0";
                tbxQtdeentregue.Text = "0";
                tbxQtdedefeito.Text = "0";
                tbxQtdesucata.Text = "0";
                tbxQtdebaixada.Text = "0";
                tbxQtdeosaux.Text = "0";
                tbxQtdesaldo.Text = "0";
                ////////
                tbxTotalpeso.Text = "0";
                tbxTotalbaseicm.Text = "0";
                tbxTotalicm.Text = "0";
                tbxTotalbaseicmsubst.Text = "0";
                tbxTotalicmsubst.Text = "0";
                tbxTotalipi.Text = "0";
                tbxTotalpispasep.Text = "0";
                tbxTotalcofins1.Text = "0";
                tbxTotalfrete.Text = "0";
                tbxTotalseguro.Text = "0";
                tbxTotaloutras.Text = "0";
                tbxTotalmercadoria.Text = "0";
                tbxTotalCusto.Text = "0";

                foreach (DataRow row in dtPedido1.Rows)
                {
                    if (row.RowState == DataRowState.Deleted ||
                        row.RowState == DataRowState.Detached)
                    {
                        continue;   // não somar se apagou
                    }
                    else
                    {


                        tbxQtdePedido.Text = (clsParser.DecimalParse(tbxQtdePedido.Text) + clsParser.DecimalParse(row["qtde"].ToString())).ToString("N2");
                        tbxQtdeentregue.Text = (clsParser.DecimalParse(tbxQtdeentregue.Text) + clsParser.DecimalParse(row["qtdeentregue"].ToString())).ToString("N2");
                        tbxQtdedefeito.Text = (clsParser.DecimalParse(tbxQtdedefeito.Text) + clsParser.DecimalParse(row["qtdedefeito"].ToString())).ToString("N2");
                        tbxQtdesucata.Text = (clsParser.DecimalParse(tbxQtdesucata.Text) + clsParser.DecimalParse(row["qtdesucata"].ToString())).ToString("N2");
                        tbxQtdebaixada.Text = (clsParser.DecimalParse(tbxQtdebaixada.Text) + clsParser.DecimalParse(row["qtdebaixada"].ToString())).ToString("N2");
                        tbxQtdeosaux.Text = (clsParser.DecimalParse(tbxQtdeosaux.Text) + clsParser.DecimalParse(row["qtdeosaux"].ToString())).ToString("N2");
                        tbxQtdesaldo.Text = (clsParser.DecimalParse(tbxQtdesaldo.Text) + clsParser.DecimalParse(row["qtdesaldo"].ToString())).ToString("N2");
                        ////////
                        tbxTotalpeso.Text = (clsParser.DecimalParse(tbxTotalpeso.Text) + clsParser.DecimalParse(row["totalpeso"].ToString())).ToString("N3");
                        tbxTotalmercadoria.Text = (clsParser.DecimalParse(tbxTotalmercadoria.Text) + clsParser.DecimalParse(row["totalmercado"].ToString())).ToString("N2");
                        tbxTotalCusto.Text = (clsParser.DecimalParse(tbxTotalCusto.Text) + clsParser.DecimalParse(row["totalcustoitem"].ToString())).ToString("N2");
                        tbxTotalbaseicm.Text = (clsParser.DecimalParse(tbxTotalbaseicm.Text) + clsParser.DecimalParse(row["baseicm"].ToString())).ToString("N2");
                        tbxTotalicm.Text = (clsParser.DecimalParse(tbxTotalicm.Text) + clsParser.DecimalParse(row["custoicm"].ToString())).ToString("N2");
                        tbxTotalbaseicmsubst.Text = (clsParser.DecimalParse(tbxTotalbaseicmsubst.Text) + clsParser.DecimalParse(row["baseicmsubst"].ToString())).ToString("N2");
                        tbxTotalicmsubst.Text = (clsParser.DecimalParse(tbxTotalicmsubst.Text) + clsParser.DecimalParse(row["icmsubst"].ToString())).ToString("N2");
                        tbxTotalipi.Text = (clsParser.DecimalParse(tbxTotalipi.Text) + clsParser.DecimalParse(row["custoipi"].ToString())).ToString("N2");
                        tbxTotalpispasep.Text = (clsParser.DecimalParse(tbxTotalpispasep.Text) + clsParser.DecimalParse(row["pispasep"].ToString())).ToString("N2");
                        tbxTotalcofins1.Text = (clsParser.DecimalParse(tbxTotalcofins1.Text) + clsParser.DecimalParse(row["cofins1"].ToString())).ToString("N2");
                        tbxTotalfrete.Text = (clsParser.DecimalParse(tbxTotalfrete.Text) + clsParser.DecimalParse(row["valorfrete"].ToString())).ToString("N2");
                        tbxTotalseguro.Text = (clsParser.DecimalParse(tbxTotalseguro.Text) + clsParser.DecimalParse(row["valorseguro"].ToString())).ToString("N2");
                        tbxTotaloutras.Text = (clsParser.DecimalParse(tbxTotaloutras.Text) + clsParser.DecimalParse(row["valoroutras"].ToString())).ToString("N2");
                    }
                }
                tbxQtdesaldo.Text = (clsParser.DecimalParse(tbxQtdePedido.Text) -
                                             (clsParser.DecimalParse(tbxQtdeentregue.Text) +
                                             clsParser.DecimalParse(tbxQtdedefeito.Text) +
                                             clsParser.DecimalParse(tbxQtdesucata.Text) +
                                             clsParser.DecimalParse(tbxQtdebaixada.Text) +
                                             clsParser.DecimalParse(tbxQtdeosaux.Text))).ToString("N2");

            if (clsParser.DecimalParse(tbxTotalmercadoria.Text) == 0)
            {
                tbxTotalDesconto.Text = "0";
            }


            if (cbxTipoDesconto.Text.Substring(0,1) == "D")
            {
                tbxTotalPedido.Text = (clsParser.DecimalParse(tbxTotalmercadoria.Text) - clsParser.DecimalParse(tbxTotalDesconto.Text)).ToString("N2");
            }
            else
            {
                tbxTotalPedido.Text = (clsParser.DecimalParse(tbxTotalmercadoria.Text) + clsParser.DecimalParse(tbxTotalDesconto.Text)).ToString("N2");
            }

            tbxTotalDesconto.Text = (clsParser.DecimalParse(tbxTotalDesconto.Text)).ToString("N2");
            tbxTotalPedido.Text = (clsParser.DecimalParse(tbxTotalPedido.Text)).ToString("N2");

        }

        private void btnBaixarPedido_Click(object sender, EventArgs e)
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Baixar // Cancelar este Item do Pedido de Venda ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                // Se o Preço Unitario estiver 0 (zero) colocar 1 (hum) apenas para poder fechar
                tbxPrecoTabela.Text = "1";
                tbxPrecoDesconto.Text = "0";
                tbxPreco.Text = "1";
                Pedido1Calcular();

                // Verificar se a O.S. tem Saldo
                Decimal qtdesaldoOS = 0;
                qtdesaldoOS = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select qtdesaldo from ordemservico where id = " + pedido1_idordemservico + " "));
                if (qtdesaldoOS > 0)
                {
                    MessageBox.Show("Você possue " + qtdesaldoOS.ToString("N0") + " pçs Pendentes nas O.S. ???");

                    MessageBox.Show("Não pode efetuar nehuma baixa se não baixar a Ordem mde Serviço ");

                }
                else
                {  // verificar se todas as O.S foram faturadas ?
                    qtdesaldoOS = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT SUM(ORDEMSERVICOFECHA.QTDEOK + ORDEMSERVICOFECHA.QTDEDEFEITO + ORDEMSERVICOFECHA.QTDEMORTA + ORDEMSERVICOFECHA.QTDEBAIXADA + ORDEMSERVICOFECHA.QTDEOSAUX) AS [QTDESALDO] " +
                                                        "FROM ORDEMSERVICOFECHA " +
                                                        "LEFT JOIN NFVENDA ON NFVENDA.ID = ORDEMSERVICOFECHA.IDNFVENDA " +
                                                        "WHERE ORDEMSERVICOFECHA.IDOS = " + pedido1_idordemservico + " " +
                                                        "AND NFVENDA.NUMERO = " + 0 + " "));
                    if (qtdesaldoOS > 0)
                    {
                        //                        MessageBox.Show("Você possue " + qtdesaldoOS.ToString("N0") + " pçs nas O.S. Fechadas - Aguardando Faturamento ???");
                        //                        MessageBox.Show("Se quiser mesmo fechar vamos colocar como Qtde Baixada Manual no Pedido e na O.S. vamos colocar que foi faturada na Nota Fiscal de Venda Nro 1 (Hum) !!!!");
                        drt = MessageBox.Show("Você possue " + qtdesaldoOS.ToString("N0") + " pçs nas O.S. Fechadas - Aguardando Faturamento !!! " + Environment.NewLine +
                            "Se quiser mesmo fechar vamos colocar como qtde baixada manualmente no Pedido e; " + Environment.NewLine +
                            "na O.S. vamos colocar que foi faturada na Nota Fiscal de Venda Nro 1 (Hum) !!!!" + Environment.NewLine +
                            "Deseja mesmo Baixar // Cancelar este Item do Pedido de Venda ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
                        if (drt == DialogResult.Yes)
                        {
                            // Colocar a Nota Fiscal Numero 1 nas Ordens de Serviço
                            Int32 idnfvenda1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from nfvenda where numero = " + 1 + " "));
                            if (idnfvenda1 > 0)
                            {
                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scn.Open();
                                scd = new SqlCommand("update ordemservicofecha " +
                                                     "set idnfvenda=" + idnfvenda1 + ", idnfvendaitem=" + clsInfo.znfvenda1 + " " +
                                                     "WHERE ORDEMSERVICOFECHA.IDOS = " + pedido1_idordemservico + " " +
                                                     "AND ORDEMSERVICOFECHA.IDNFVENDA = " + clsInfo.znfvenda + " ", scn);
                                scd.ExecuteNonQuery();

                                Pedido1_tbxQtdeosaux.Text = (clsParser.DecimalParse(Pedido1_tbxQtdeosaux.Text) + clsParser.DecimalParse(Pedido1_tbxQtdesaldo.Text)).ToString("N2");
                                tbxTotalPrevistoItem.Text = 0.ToString("N2");
                                Pedido1_tbxQtdesaldo.Text = 0.ToString("N2");
                            }
                            else
                            {
                                MessageBox.Show("Você não pode baixar sem a Nota Fiscal de nro 1 !!!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vamos colocar o Saldo do Pedido como Baixa Manual para conseguir Fechar !!!!");
                        drt = MessageBox.Show("Deseja mesmo Baixar // Cancelar este Item do Pedido de Venda ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
                        if (drt == DialogResult.Yes)
                        {
                            Pedido1_tbxQtdeosaux.Text = (clsParser.DecimalParse(Pedido1_tbxQtdeosaux.Text) + clsParser.DecimalParse(Pedido1_tbxQtdesaldo.Text)).ToString("N2");
                            tbxTotalPrevistoItem.Text = 0.ToString("N2");
                            Pedido1_tbxQtdesaldo.Text = 0.ToString("N2");
                        }
                    }
                }
            }
        }

        private void ckxCalculoautomatico_CheckedChanged(object sender, EventArgs e)
        {
            CalculoAutomatico();
        }

        private void CalculoAutomatico()
        {
            if (ckxCalculoautomatico.Checked == true)
            {
                //tbxQtde.ReadOnly = true;
                //tbxQtde.TabStop = false;
                //tbxQtde.BackColor = Color.LemonChiffon;
                tbxQtde.ReadOnly = false;
                tbxQtde.TabStop = true;
                tbxQtde.BackColor = Color.White;


                tbxTotalmercado.ReadOnly = true;
                tbxTotalmercado.TabStop = false;
                tbxTotalmercado.BackColor = Color.LemonChiffon;

                tbxTotalPeso1.ReadOnly = true;
                tbxTotalPeso1.TabStop = false;
                tbxTotalPeso1.BackColor = Color.LemonChiffon;

                tbxBaseicm.ReadOnly = true;
                tbxBaseicm.TabStop = false;
                tbxBaseicm.BackColor = Color.LemonChiffon;

                tbxCustoicm.ReadOnly = true;
                tbxCustoicm.TabStop = false;
                tbxCustoicm.BackColor = Color.LemonChiffon;

                tbxBaseicmsubst.ReadOnly = true;
                tbxBaseicmsubst.TabStop = false;
                tbxBaseicmsubst.BackColor = Color.LemonChiffon;

                tbxIcmssubsttotal.ReadOnly = true;
                tbxIcmssubsttotal.TabStop = false;
                tbxIcmssubsttotal.BackColor = Color.LemonChiffon;

                tbxBcipi.ReadOnly = true;
                tbxBcipi.TabStop = false;
                tbxBcipi.BackColor = Color.LemonChiffon;

                tbxCustoipi.ReadOnly = true;
                tbxCustoipi.TabStop = false;
                tbxCustoipi.BackColor = Color.LemonChiffon;

                tbxBcpispasep.ReadOnly = true;
                tbxBcpispasep.TabStop = false;
                tbxBcpispasep.BackColor = Color.LemonChiffon;

                tbxPispasep.ReadOnly = true;
                tbxPispasep.TabStop = false;
                tbxPispasep.BackColor = Color.LemonChiffon;

                tbxBccofins1.ReadOnly = true;
                tbxBccofins1.TabStop = false;
                tbxBccofins1.BackColor = Color.LemonChiffon;

                tbxCofins1.ReadOnly = true;
                tbxCofins1.TabStop = false;
                tbxCofins1.BackColor = Color.LemonChiffon;

                tbxTotalnota.ReadOnly = true;
                tbxTotalnota.TabStop = false;
                tbxTotalnota.BackColor = Color.LemonChiffon;

                tbxTotalPrevistoItem.ReadOnly = true;
                tbxTotalPrevistoItem.TabStop = false;
                tbxTotalPrevistoItem.BackColor = Color.LemonChiffon;
            }
            else
            {
                tbxQtde.ReadOnly = false;
                tbxQtde.TabStop = true;
                tbxQtde.BackColor = Color.White;

                tbxTotalmercado.ReadOnly = false;
                tbxTotalmercado.TabStop = true;
                tbxTotalmercado.BackColor = Color.White;

                tbxTotalPeso1.ReadOnly = false;
                tbxTotalPeso1.TabStop = true;
                tbxTotalPeso1.BackColor = Color.White;

                tbxBaseicm.ReadOnly = false;
                tbxBaseicm.TabStop = true;
                tbxBaseicm.BackColor = Color.White;

                tbxCustoicm.ReadOnly = false;
                tbxCustoicm.TabStop = true;
                tbxCustoicm.BackColor = Color.White;

                tbxBaseicmsubst.ReadOnly = false;
                tbxBaseicmsubst.TabStop = true;
                tbxBaseicmsubst.BackColor = Color.White;

                tbxIcmssubsttotal.ReadOnly = false;
                tbxIcmssubsttotal.TabStop = true;
                tbxIcmssubsttotal.BackColor = Color.White;

                tbxBcipi.ReadOnly = false;
                tbxBcipi.TabStop = true;
                tbxBcipi.BackColor = Color.White;

                tbxCustoipi.ReadOnly = false;
                tbxCustoipi.TabStop = true;
                tbxCustoipi.BackColor = Color.White;

                tbxBcpispasep.ReadOnly = false;
                tbxBcpispasep.TabStop = true;
                tbxBcpispasep.BackColor = Color.White;

                tbxPispasep.ReadOnly = false;
                tbxPispasep.TabStop = true;
                tbxPispasep.BackColor = Color.White;

                tbxBccofins1.ReadOnly = false;
                tbxBccofins1.TabStop = true;
                tbxBccofins1.BackColor = Color.White;

                tbxCofins1.ReadOnly = false;
                tbxCofins1.TabStop = true;
                tbxCofins1.BackColor = Color.White;

                tbxTotalnota.ReadOnly = false;
                tbxTotalnota.TabStop = true;
                tbxTotalnota.BackColor = Color.White;

                tbxTotalPrevistoItem.ReadOnly = false;
                tbxTotalPrevistoItem.TabStop = true;
                tbxTotalPrevistoItem.BackColor = Color.White;
            }
        }
        private void CriarOrdemServico(Int32 _idPedidoVenda)
        {
            //// Inicio - Tabela Pedido
            ////
            //dtPedidoOS = new DataTable();
            //String query = "";
            //SqlDataAdapter sda;
            //query = "SELECT * FROM PEDIDO1 WHERE PEDIDO1.IDPEDIDO = " + _idPedidoVenda + "";
            //dtPedidoOS = new DataTable();
            //sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            //sda.Fill(dtPedidoOS);
            //foreach (DataRow row in dtPedidoOS.Rows)
            //{
            //    clsPedido1Info = new clsPedido1Info();
            //    clsPedido1Info = clsPedido1BLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), clsInfo.conexaosqldados);
            //    // Verificar se ja foi criado a Ordem de Serviço
            //    if (clsPedido1Info.idordemservico == clsInfo.zordemservico || clsPedido1Info.idordemservico == 0)
            //    {   // Criar a Ordem de Serviço para este item
            //        clsOrdemServicoInfo = new clsOrdemServicoInfo();
            //        clsOrdemServicoInfo.ano = DateTime.Now.Year;
            //        clsOrdemServicoInfo.complemento = "";
            //        clsOrdemServicoInfo.custounitarioproduz = 0;
            //        clsOrdemServicoInfo.data = DateTime.Now;
            //        clsOrdemServicoInfo.dataentrega = DateTime.Now;
            //        clsOrdemServicoInfo.id = 0;
            //        clsOrdemServicoInfo.idcliente = clsPedidoInfo.idcliente;
            //        clsOrdemServicoInfo.idcodigo = clsPedido1Info.idcodigo;
            //        clsOrdemServicoInfo.numero = 0;
            //        clsOrdemServicoInfo.qtde = clsPedido1Info.qtde;
            //        clsOrdemServicoInfo.qtdeok = 0;
            //        clsOrdemServicoInfo.qtdesaldo = clsPedido1Info.qtde;
            //        clsOrdemServicoInfo.status = "P";  // Para Produzir separar
            //        clsOrdemServicoInfo.tipoos = "VEN";  // Venda   //FAB=Fabricacao
            //        clsOrdemServicoInfo.totalfornece = 0;
            //        //Incluindo OS
            //        clsOrdemServicoInfo.id = clsOrdemServicoBLL.Incluir(clsOrdemServicoInfo, clsInfo.conexaosqldados);
            //        // Colocando o numero da OS no Pedido
            //        clsPedido1Info.idordemservico = clsOrdemServicoInfo.id;
            //        clsPedido1BLL.Alterar(clsPedido1Info, clsInfo.conexaosqldados);

            //    }
            //    else
            //    {
            //        // Verificar se mudou a quantidade no pedido comparada com a OS.
            //        clsOrdemServicoInfo = clsOrdemServicoBLL.Carregar(clsPedido1Info.idordemservico, clsInfo.conexaosqldados);
            //        if (clsOrdemServicoInfo.qtde == clsPedido1Info.qtde)
            //        {
            //            // Pode continuar sem mensagem
            //        }
            //        else
            //        {
            //            MessageBox.Show("Ele vai deixar a OS errada ? " + clsOrdemServicoInfo.numero + "  - analisar alteracao do pedido (qtde mudou?)");
            //        }
            //    }
            //}
        }

        private void btnCapturarCNPJ_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCapturarCNPJ.Name;
            frmCadastrarCNPJ frmCadastrarCNPJ = new frmCadastrarCNPJ();
            frmCadastrarCNPJ.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmCadastrarCNPJ, clsInfo.conexaosqldados);

        }

        private void btnIdVendedor_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdVendedor.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Vendedor", idvendedor);
            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            // Salvar
            if (tspSalvar.Enabled == true)
            {
                PedidoSalvar();
            }

            // Imprimir Entrada da despesa
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = id;
            field.Name = "id";
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            // Fazer um loop para ver os valores e vencimentos
            int Parcelas = 0;
            String CondPagto = "";
            foreach (DataRow row in dtPedidoReceber.Rows)
            {
                if (row.RowState == DataRowState.Unchanged)
                {
                    // Foi colocado pois quando altera apenas o cognome do fornecedor tem que atualizar o contas a pagar
//                    row.SetModified();
                }
                if (row.RowState == DataRowState.Detached ||
                    row.RowState == DataRowState.Unchanged)
                {
                    continue;
                }
                else if (row.RowState == DataRowState.Deleted)
                {
  //                  clsPedidoReceberBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                    continue;
                }
                else
                {
                    clsPedidoReceberInfo = new clsPedidoReceberInfo();
                    PedidoReceberGridToInfo(dtPedidoReceber, clsPedidoReceberInfo, Int32.Parse(row["posicaorec"].ToString()));
                    if (clsPedidoReceberInfo.valor > 0)
                    {
                        Parcelas = Parcelas + 1;
                        if (Parcelas > 6)
                        {
                            MessageBox.Show("Preparar programa para mais de 6 parcelas");
                        }
                        if (Parcelas == 1)
                        {
                            CondPagto = "1- " + clsPedidoReceberInfo.data.ToString("dd/MM/yy") + "     R$=" + clsPedidoReceberInfo.valor.ToString("N2") + "      ";
                        }
                        else if (Parcelas == 2)
                        {
                            CondPagto = CondPagto + "2- " + clsPedidoReceberInfo.data.ToString("dd/MM/yy") + "     R$=" + clsPedidoReceberInfo.valor.ToString("N2") + "      ";
                        }
                        else if (Parcelas == 3)
                        {
                            CondPagto = CondPagto + "3- " + clsPedidoReceberInfo.data.ToString("dd/MM/yy") + "     R$=" + clsPedidoReceberInfo.valor.ToString("N2") + "      ";
                        }
                        else if (Parcelas == 4)
                        {
                            CondPagto = CondPagto + "4- " + clsPedidoReceberInfo.data.ToString("dd/MM/yy") + "     R$=" + clsPedidoReceberInfo.valor.ToString("N2") + "      ";
                        }
                        else if (Parcelas == 5)
                        {
                            CondPagto = CondPagto + "5- " + clsPedidoReceberInfo.data.ToString("dd/MM/yy") + "     R$=" + clsPedidoReceberInfo.valor.ToString("N2") + "      ";
                        }
                        else if (Parcelas == 6)
                        {
                            CondPagto = CondPagto + "6- " + clsPedidoReceberInfo.data.ToString("dd/MM/yy") + "     R$=" + clsPedidoReceberInfo.valor.ToString("N2") + "      ";
                        }
                    }
                }
            }
            field = new ParameterField();
            field.Name = "condpagto";
            valor = new ParameterDiscreteValue();
            valor.Value = CondPagto;
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            frmCrystalReport.Init(clsInfo.caminhorelatorios, "PEDIDO_PDV.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

        }

        private void tbxPrecoDesconto_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsbCondpagto_Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar/alterar os Pagamentos ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsPedidoReceberInfo = new clsPedidoReceberInfo();
                    PedidoReceberFillInfo(clsPedidoReceberInfo);
                    PedidoReceberFillInfoToGrid(dtPedidoReceber, clsPedidoReceberInfo);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

                //NFCompra1SomaTotal();

                //NFCompraSomar();

                gbxPagamentosRegistro.Visible = false;
                gbxPagamentos.Visible = true;
                tclPagamentos.SelectedIndex = 0;

                SomarPagamentos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsbCondpagto_Retornar_Click(object sender, EventArgs e)
        {
            tclPedidoVenda.SelectedIndex = 0;
        }

        private void tsbCondpagto_RetornarPagto_Click(object sender, EventArgs e)
        {
            tclPagamentos.SelectedIndex = 0;
        }

        private void tspRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (idcondpagto > 0)
                {
                    PedidoReceberCalcularPagamentos();
                }
                else
                {
                    throw new Exception("Antes de iniciar o re-cálculo é necessário escolher uma 'Condição de Pagamento'.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void PedidoReceberCalcularPagamentos()
        {
            // Realiza as verificações necessárias
            // para ver se é possível realizar o re-cálculo
            foreach (DataRow row in dtPedidoReceber.Rows)
            {
                if (row.RowState != DataRowState.Detached &&
                    row.RowState != DataRowState.Added)
                {
                    if (row["PAGOU", DataRowVersion.Original].ToString() == "S")
                    {
                        throw new Exception("Já houve parcelas Pagas. Não é possível realizar o re-cálculo.");
                    }
                }
            }

            Decimal totalcobranca = 0;
            Decimal totalcobrancaipi = 0;
            Decimal totalcobrancast = 0;
            Decimal totalcobrancapis = 0;
            Decimal totalcobrancacofins = 0;

            //Decimal totalcomissao = 0;
            //Decimal totalcomissaoger = 0;
            //Decimal totalcomissaosup = 0;

            Decimal totalcobrancamoirrf = 0;
            Decimal totalcobrancamoinss = 0;
            Decimal totalcobrancamopiscofinscsll = 0;
            Decimal totalcobrancamopis = 0;
            Decimal totalcobrancamocofins = 0;
            Decimal totalcobrancamocsll = 0;

            // Soma os valores a serem cobrados
            foreach (DataRow row in dtPedido1.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    row.RowState != DataRowState.Detached)
                {
                    //if (row["fatura"].ToString() == "S")
                    //{
                    totalcobranca += clsParser.DecimalParse(row["totalnota"].ToString());
                    ////totalcomissao += clsParser.DecimalParse(row["valorcomissao"].ToString());
                    ////totalcomissaoger += clsParser.DecimalParse(row["valorcomissaoger"].ToString());
                    ////totalcomissaosup += clsParser.DecimalParse(row["valorcomissaosup"].ToString());

                    //}
                }
            }

            if (totalcobranca <= 0)
            {
                return;
            }
            else
            {
                //totalcobranca -= (totalcobrancamoirrf +
                //                  totalcobrancamoinss +
                //                  totalcobrancamopiscofinscsll +
                //                  totalcobrancamopis +
                //                  totalcobrancamocofins +
                //                  totalcobrancamocsll);
            }

            // Verifica se irá ou não descontar pis/cofins
            Boolean descontapis;
            Boolean descontacofins;
            descontapis = false; //descontapis = (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DESCONTAPISPASEPSAI from cliente where id=" + idcliente) == "S");
            descontacofins = false; //descontacofins = (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DESCONTACOFINSSAI from cliente where id=" + idcliente) == "S");

            clsCondpagtoBLL CondpagtoBLL = new clsCondpagtoBLL();
            clsCondpagtoInfo CondpagtoInfo = CondpagtoBLL.Carregar(idcondpagto, clsInfo.conexaosqldados);
            if (CondpagtoInfo == null)
            {
                throw new Exception("Condição de Pagamento não foi escolhida, deve escolher a Condição de Pagamento antes de calcularos pagamentos.");
            }

            dgvPagamentos.DataSource = null;


            if (dtPedidoReceberTempDeletar == null)
            {
                dtPedidoReceberTempDeletar = new DataTable();
                dtPedidoReceberTempDeletar = dtPedidoReceber.Copy();
            }
            else
            {
                foreach (DataRow row in dtPedidoReceber.Rows)
                {
                    if (row.RowState != DataRowState.Added &&
                        row.RowState != DataRowState.Detached)
                    {
                        dtPedidoReceberTempDeletar.Rows.Add(row);
                    }
                }
            }

            dtPedidoReceber = clsFinanceiro.GerarFatura(DateTime.Parse(tbxData.Text),
                                                        totalcobranca,
                                                        0,
                                                        totalcobrancaipi,
                                                        totalcobrancast,
                                                        (descontapis == false),
                                                        totalcobrancapis,
                                                        (descontacofins == false),
                                                        totalcobrancacofins,
                                                        idformapagto,
                                                        idcondpagto,
                                                        "N", "S");

            DataColumn dcId = new DataColumn("ID", Type.GetType("System.Int32"));
            DataColumn dcIdNota = new DataColumn("IDNOTA", Type.GetType("System.Int32"));
            DataColumn dcPosicaoRec = new DataColumn("POSICAOREC", Type.GetType("System.Int32"));

            dtPedidoReceber.Columns.Add(dcId);
            dtPedidoReceber.Columns.Add(dcIdNota);
            dtPedidoReceber.Columns.Add(dcPosicaoRec);

            Int32 posicaorec = 1;
            for (Int32 x = 0; x < dtPedidoReceber.Rows.Count; x++)
            {
                if (dtPedidoReceber.Rows[x].RowState != DataRowState.Detached &&
                    dtPedidoReceber.Rows[x].RowState != DataRowState.Deleted)
                {
                    dtPedidoReceber.Rows[x]["ID"] = 0;
                    dtPedidoReceber.Rows[x]["IDNOTA"] = id;
                    dtPedidoReceber.Rows[x]["POSICAOREC"] = posicaorec;
                    dtPedidoReceber.Rows[x]["IDTIPOPAGA"] = idformapagto;
                    posicaorec++;
                }
            }

            dgvPagamentos.DataSource = dtPedidoReceber;

            clsGridHelper.MontaGrid2(dgvPagamentos, clsPedidoReceberBLL.dtGridColunas, true);

            dgvPagamentos.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPagamentos.Columns["VALOR"].DefaultCellStyle.Format = "N2";

            SomarPagamentos();

            //}
        }
        private void SomarPagamentos()
        {
            tbxValorParcelas.Text = "0";
            Decimal soma = 0;
            // vERIFICAR SE JA HOUVE PAGAMENTO
            String Pago = "N";

            foreach (DataRow row in dtPedidoReceber.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    row.RowState != DataRowState.Detached)
                {
                    soma = (soma + clsParser.DecimalParse(row["valor"].ToString()));
                    if (row["PAGOU"].ToString() == "S")
                    {
                        Pago = "S";
                    }
                }

            }
            tbxValorParcelas.Text = soma.ToString("N2");

            // Comparar e chamar a atenção
            if (clsParser.DecimalParse(tbxValorParcelas.Text) != clsParser.DecimalParse(tbxPag_TotalPedido.Text))
            {
                tbxValorParcelas.BackColor = Color.Red;
            }
            else
            {
                //'ok'
                tbxValorParcelas.BackColor = Color.LemonChiffon;
            }
            if (Pago == "S")
            {
                tspSalvar.Enabled = false;
            }

        }

        private void tsbCondpagto_Calcular_Click(object sender, EventArgs e)
        {
            tspRecalcular.PerformClick();
        }

        private void tsbCondpagto_Incluir_Click(object sender, EventArgs e)
        {
            pedidoreceber_posicao = 0;
            PedidoReceberCarregar();

        }
        private void PedidoReceberCarregar()
        {
            clsPedidoReceberInfo = new clsPedidoReceberInfo();
            clsPedidoReceberInfoOld = new clsPedidoReceberInfo();

            if (pedidoreceber_posicao == 0)
            {
                pedidoreceber_posicao = dtPedidoReceber.Rows.Count + 1;
                clsPedidoReceberInfo.boletonro = 0;
                clsPedidoReceberInfo.data = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                clsPedidoReceberInfo.dv = "";
                clsPedidoReceberInfo.id = 0;
                clsPedidoReceberInfo.idnota = id;
                clsPedidoReceberInfo.idtipopaga = clsInfo.zformapagto;
                clsPedidoReceberInfo.pagou = "N";
                clsPedidoReceberInfo.posicao = 1;
                clsPedidoReceberInfo.posicaofim = 1;
                clsPedidoReceberInfo.valor = 0;
                clsPedidoReceberInfo.tipopaga = "";
            }
            else
            {
                PedidoReceberGridToInfo(dtPedidoReceber, clsPedidoReceberInfo, pedidoreceber_posicao);
            }

            PedidoReceberCampos(clsPedidoReceberInfo);
            PedidoReceberFillInfo(clsPedidoReceberInfoOld);

            tclPagamentos.SelectedIndex = 1;
            gbxPagamentosRegistro.Visible = true;
            if (gbxRegistro.Enabled == true)
            {
                PedidoReceber_tbxPosicao.Select();
            }

        }
        private void PedidoReceberGridToInfo(DataTable dt, clsPedidoReceberInfo info, Int32 posicao)
        {
            DataRow row = dt.Select("posicaorec = " + posicao)[0];

            info.boletonro = clsParser.DecimalParse(row["boletonro"].ToString());
            info.data = DateTime.Parse(row["data"].ToString());
            info.dv = row["dv"].ToString();
            info.id = clsParser.Int32Parse(row["id"].ToString());
            info.idnota = clsParser.Int32Parse(row["idnota"].ToString());
            info.idtipopaga = clsParser.Int32Parse(row["idtipopaga"].ToString());
            info.pagou = row["pagou"].ToString();
            info.posicao = clsParser.Int32Parse(row["posicao"].ToString());
            info.posicaofim = clsParser.Int32Parse(row["posicaofim"].ToString());
            info.valor = clsParser.DecimalParse(row["valor"].ToString());
            //info.tipopaga = row["tipopaga"].ToString();
            info.tipopaga = row["tipopaga"].ToString().PadRight(2, ' ').Substring(0,2);
        }
        private void PedidoReceberFillInfoToGrid(DataTable dt, clsPedidoReceberInfo info)
        {
            DataRow row;
            DataRow[] rows = dt.Select("posicaorec = " + pedidoreceber_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dt.NewRow();
            }
            row["id"] = info.id;
            row["boletonro"] = info.boletonro;
            row["data"] = info.data;
            row["dv"] = info.dv;
            row["idnota"] = info.idnota;
            row["idtipopaga"] = info.idtipopaga;
            row["pagou"] = info.pagou;
            row["posicao"] = info.posicao;
            row["posicaofim"] = info.posicaofim;
            row["valor"] = info.valor;
            row["tipopaga"] = info.tipopaga;


            // Colunas que petencem a outras tabelas
            /*            row["codigo"] = Orcamento1_tbxCodigo.Text;
                        row["nome"] = Orcamento1_tbxCodigoNome.Text;
                        row["unid"] = Orcamento1_Pecas_tbxUnidade.Text;
                        */
            if (rows.Length == 0)
            {
                row["posicaorec"] = pedidoreceber_posicao;
                dt.Rows.Add(row);
            }
        }

        private void PedidoReceberCampos(clsPedidoReceberInfo info)
        {
            PedidoReceber_tbxBoletoNro.Text = info.boletonro.ToString();

            pedidoreceber_id = info.id;
            PedidoReceber_tbxData.Text = info.data.ToString("dd/MM/yyyy");
            PedidoReceber_tbxDV.Text = info.dv;
            pedidoreceber_idnota = info.idnota;
            pedidoreceber_idtipopaga = info.idtipopaga;

            if (pedidoreceber_idtipopaga == 0) { pedidoreceber_idtipopaga = clsInfo.zformapagto; }
            PedidoReceber_tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + pedidoreceber_idtipopaga);
            PedidoReceber_tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + pedidoreceber_idtipopaga);

            PedidoReceber_tbxPagou.Text = info.pagou;
            PedidoReceber_tbxPosicao.Text = info.posicao.ToString("N0");
            PedidoReceber_tbxPosicaoFim.Text = info.posicaofim.ToString("N0");
            PedidoReceber_tbxValor.Text = info.valor.ToString("N2");

        }
        private void PedidoReceberFillInfo(clsPedidoReceberInfo info)
        {
            info.id = pedidoreceber_id;
            info.idnota = pedidoreceber_idnota;
            info.boletonro = clsParser.DecimalParse(PedidoReceber_tbxBoletoNro.Text);
            info.data = DateTime.Parse(PedidoReceber_tbxData.Text);
            info.dv = PedidoReceber_tbxDV.Text;
            info.idtipopaga = pedidoreceber_idtipopaga;
            info.pagou = PedidoReceber_tbxPagou.Text;
            info.posicao = clsParser.Int32Parse(PedidoReceber_tbxPosicao.Text);
            info.posicaofim = clsParser.Int32Parse(PedidoReceber_tbxPosicaoFim.Text);
            info.valor = clsParser.DecimalParse(PedidoReceber_tbxValor.Text);
            info.tipopaga = PedidoReceber_tbxFormaPagto.Text;
        }

        private void PedidoReceber_btnIdFormaPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = PedidoReceber_btnIdFormaPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", pedidoreceber_idtipopaga, "Situação do Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void dgvPagamentos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsbCondpagto_Alterar.PerformClick();
        }

        private void tsbCondpagto_Alterar_Click(object sender, EventArgs e)
        {
            if (dgvPagamentos.CurrentRow != null)
            {
                pedidoreceber_posicao = Int32.Parse(dgvPagamentos.CurrentRow.Cells["posicaorec"].Value.ToString());
                PedidoReceberCarregar();
            }

        }


        private void tsbCondpagto_Excluir_Click(object sender, EventArgs e)
        {
            if (dgvPagamentos.CurrentRow != null)
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja realmente excluir o item selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    dtPedidoReceber.Select("posicaorec =" + dgvPagamentos.CurrentRow.Cells["posicaorec"].Value.ToString())[0].Delete();
                }
            }

        }

        private void tbxPecas_codigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCliente.Name;
            frmCliente frmCliente = new frmCliente();
            frmCliente.Init(0, this.rows);
            clsFormHelper.AbrirForm(this.MdiParent, frmCliente, clsInfo.conexaosqldados);

        }

        private void gbxPedido_Enter(object sender, EventArgs e)
        {

        }

        private void tbxQtdeentregue_TextChanged(object sender, EventArgs e)
        {

        }

        private void label80_Click(object sender, EventArgs e)
        {

        }

        private void tbxQtdedefeito_TextChanged(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void tbxQtdesucata_TextChanged(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void tbxQtdebaixada_TextChanged(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void tbxQtdeosaux_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxTotalbaseicm_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void tbxTotalicm_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void tbxTotalicmsubst_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxTotalbaseicmsubst_TextChanged(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void tbxTotaloutras_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxTotalseguro_TextChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void tbxTotalfrete_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxRetido_TextChanged(object sender, EventArgs e)
        {

        }

        private void label98_Click(object sender, EventArgs e)
        {

        }

        private void gbxRedespacho_Enter(object sender, EventArgs e)
        {

        }

        private void gbxFretecalculo_Enter(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label86_Click(object sender, EventArgs e)
        {

        }

        private void tbxComissaoRepresentante_TextChanged(object sender, EventArgs e)
        {

        }

        private void label85_Click(object sender, EventArgs e)
        {

        }

        private void tbxPago_CtaReceber_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClientecontato_telefone_Click(object sender, EventArgs e)
        {

        }

        private void tbxCliente_telefone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

    }
}