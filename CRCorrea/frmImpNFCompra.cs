using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRCorrea
{
    public partial class frmImpNFCompra : Form
    {
        Int32 idNota;
        Int32 idNotaNew;
        Int32 idNotaNew1;
        Int32 idNotaNewPagar;
        clsNfCompraBLL clsNfCompraBLL = new clsNfCompraBLL();   
        clsNfCompraInfo clsNfCompraInfo = new clsNfCompraInfo();
        clsNfCompraInfo clsNfCompraInfoNew = new clsNfCompraInfo();
        clsNfCompra1BLL clsNfCompra1BLL = new clsNfCompra1BLL();
        clsNfCompra1Info clsNfCompra1Info = new clsNfCompra1Info();
        clsNfCompra1Info clsNfCompra1InfoNew = new clsNfCompra1Info();
        clsNfCompraPagarBLL clsNfCompraPagarBLL = new clsNfCompraPagarBLL();
        clsNfCompraPagarInfo clsNfCompraPagarInfo = new clsNfCompraPagarInfo();
        clsNfCompraPagarInfo clsNfCompraPagarInfoNew = new clsNfCompraPagarInfo();

        DataTable dtNFCompra = new DataTable();
        DataTable dtNFCompra1 = new DataTable();
        DataTable dtNFCompraPagar = new DataTable();
        
        public frmImpNFCompra()
        {
            InitializeComponent();
        }
        public void Init()
        {

        }


        private void frmImpNFCompra_Load(object sender, EventArgs e)
        {
            tbxData.Text = DateTime.Today.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Today.Year.ToString();

        }
        private void frmImpNFCompra_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void btnIdNFCompra_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdNFCompra.Name;
            frmNFCompraVis frmNFCompraVis = new frmNFCompraVis();
            frmNFCompraVis.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmNFCompraVis, clsInfo.conexaosqldados);
        }
        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1 &&
                clsInfo.znomegrid != null &&
                clsInfo.znomegrid != "")
            {
                // ###############################
                // Verifica os botões de pesquisa
                // ###############################

                if (clsInfo.znomegrid == btnIdNFCompra.Name)  // FORNECEDOR
                {
                    tbxFornecedor.Text = clsInfo.zrow.Cells["FORNECEDOR"].Value.ToString();
                    tbxNFImportar.Text = clsInfo.zrow.Cells["NUMERO"].Value.ToString();
                    tbxTotalNotaFiscal.Text = clsInfo.zrow.Cells["TOTALNOTAFISCAL"].Value.ToString();
                    idNota = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());

                    tbxNumero.Select();
                    tbxNumero.SelectAll();
                }
            }
            else
            {
                // ###############################
                // Verifica os campos de pesquisa
                // ###############################

                if (ctl.Name == tbxNFImportar.Name)    // FORNECEDOR
                {
                }
            }
            if (idNota > 0)
            {
                gbxNovaNota.Visible = true;
            }
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja mesmo Importar esta Nota para o mês Indicado ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                using (TransactionScope tse = new TransactionScope())
                {

                    // ###############################
                    // Copiar o Cabecalho da Nota
                    clsNfCompraInfo = new clsNfCompraInfo();
                    clsNfCompraInfoNew = new clsNfCompraInfo();
                    clsNfCompraInfo = clsNfCompraBLL.Carregar(idNota, clsInfo.conexaosqldados);
                    NFCompraFillInfo();
                    idNotaNew = clsNfCompraBLL.Incluir(clsNfCompraInfoNew, clsInfo.conexaosqldados);

                    //// Copiar os itens da Nota Fiscal
                    dtNFCompra1 = clsNfCompra1BLL.GridDados(idNota, clsInfo.conexaosqldados);
                    foreach (DataRow row in dtNFCompra1.Rows)
                    {
                        clsNfCompra1InfoNew = new clsNfCompra1Info();
                        clsNfCompra1Info = clsNfCompra1BLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), clsInfo.conexaosqldados);
                        NFCompra1FillInfo(clsNfCompra1Info);
                        idNotaNew1 = clsNfCompra1BLL.Incluir(clsNfCompra1InfoNew, clsInfo.conexaosqldados);
                    }
                    //// Copiar as datas de Pagamento
                    dtNFCompraPagar = clsNfCompraPagarBLL.GridDados(idNota);
                    foreach (DataRow row in dtNFCompraPagar.Rows)
                    {
                        clsNfCompraPagarInfoNew = new clsNfCompraPagarInfo();
                        clsNfCompraPagarInfo = clsNfCompraPagarBLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), clsInfo.conexaosqldados);
                        NFCompraPagarFillInfo(clsNfCompraPagarInfo);
                        idNotaNewPagar = clsNfCompraPagarBLL.Incluir(clsNfCompraPagarInfoNew, clsInfo.conexaosqldados);
                    }
                    // ###############################
                    tse.Complete();
                    lblTransferencia.Visible = true;
                }


            }
        }
        private void NFCompraFillInfo()
        {

            clsNfCompraInfoNew.chnfe = clsNfCompraInfo.chnfe;
            clsNfCompraInfoNew.cofins = clsNfCompraInfo.cofins;
            clsNfCompraInfoNew.cofins1 = clsNfCompraInfo.cofins1;
            clsNfCompraInfoNew.csll = clsNfCompraInfo.csll;
            clsNfCompraInfoNew.data = DateTime.Parse(tbxData.Text);
            clsNfCompraInfoNew.datalanca = DateTime.Parse(tbxData.Text); 
            clsNfCompraInfoNew.datarecebimento = DateTime.Parse(tbxData.Text);
            clsNfCompraInfoNew.emitente = clsNfCompraInfo.emitente;
            clsNfCompraInfoNew.filial = clsNfCompraInfo.filial;
            clsNfCompraInfoNew.frete = clsNfCompraInfo.frete;
            //info.fretebaseicms
            //info.freteicmaliq
            clsNfCompraInfoNew.fretepaga = clsNfCompraInfo.fretepaga;
            //info.fretevaloricms
            clsNfCompraInfoNew.id = 0;
            clsNfCompraInfoNew.idcondpagto = clsNfCompraInfo.idcondpagto;
            clsNfCompraInfoNew.iddocumento = clsNfCompraInfo.iddocumento;
            clsNfCompraInfoNew.idformapagto = clsNfCompraInfo.idformapagto;
            clsNfCompraInfoNew.idfornecedor = clsNfCompraInfo.idfornecedor;
            clsNfCompraInfoNew.idfornecedororigem = clsNfCompraInfo.idfornecedororigem;
            clsNfCompraInfoNew.idpedido = clsNfCompraInfo.idpedido;
            clsNfCompraInfoNew.idtransportadora = clsNfCompraInfo.idtransportadora;
            clsNfCompraInfoNew.inss = clsNfCompraInfo.inss;
            clsNfCompraInfoNew.irrf = clsNfCompraInfo.irrf;
            clsNfCompraInfoNew.iss = clsNfCompraInfo.iss;
            clsNfCompraInfoNew.numero = clsParser.Int64Parse(tbxNumero.Text.Replace(".", ""));
            clsNfCompraInfoNew.observa = clsNfCompraInfo.observa;
            clsNfCompraInfoNew.pis = clsNfCompraInfo.pis;
            clsNfCompraInfoNew.piscofinscsll = clsNfCompraInfo.piscofinscsll;
            clsNfCompraInfoNew.pispasep = clsNfCompraInfo.pispasep;
            clsNfCompraInfoNew.serie = clsNfCompraInfo.serie;
            clsNfCompraInfoNew.setor = clsNfCompraInfo.setor;
            clsNfCompraInfoNew.setorfator = clsNfCompraInfo.setorfator;
            clsNfCompraInfoNew.situacao = clsNfCompraInfo.situacao;
            clsNfCompraInfoNew.tipoentrada = clsNfCompraInfo.tipoentrada;
            clsNfCompraInfoNew.tipofrete = clsNfCompraInfo.tipofrete;
            clsNfCompraInfoNew.totalbaseicm = clsNfCompraInfo.totalbaseicm;
            clsNfCompraInfoNew.totalbaseicmsubst = clsNfCompraInfo.totalbaseicmsubst;
            clsNfCompraInfoNew.totalfrete = clsNfCompraInfo.totalfrete;
            //info.totalfreteicms = clsParser.DecimalParse(tbxto tbxPisPorc.Text);
            clsNfCompraInfoNew.totalicm = clsNfCompraInfo.totalicm;
            clsNfCompraInfoNew.totalicmsubst = clsNfCompraInfo.totalicmsubst;
            clsNfCompraInfoNew.totalipi = clsNfCompraInfo.totalipi;
            clsNfCompraInfoNew.totalmercadoria = clsNfCompraInfo.totalmercadoria;
            clsNfCompraInfoNew.totalnotafiscal = clsNfCompraInfo.totalnotafiscal;
            clsNfCompraInfoNew.totaloutras = clsNfCompraInfo.totaloutras;
            //info.totaloutrasicms = clsParser.DecimalParse( tbxPisPorc.Text);
            clsNfCompraInfoNew.totalpeso = clsNfCompraInfo.totalpeso;
            clsNfCompraInfoNew.totalpesobruto = clsNfCompraInfo.totalpesobruto;
            clsNfCompraInfoNew.totalseguro = clsNfCompraInfo.totalseguro;
            clsNfCompraInfoNew.transporte = clsNfCompraInfo.transporte;
        }
        private void NFCompra1FillInfo(clsNfCompra1Info info)
        {
            clsNfCompra1InfoNew.id = 0;
            clsNfCompra1InfoNew.numero = idNotaNew; // id

            clsNfCompra1InfoNew.aliqcofins1 = clsNfCompra1Info.aliqcofins1;
            clsNfCompra1InfoNew.aliqpispasep = clsNfCompra1Info.aliqpispasep;
            clsNfCompra1InfoNew.baseicm = clsNfCompra1Info.baseicm;
            clsNfCompra1InfoNew.baseicmsubst = clsNfCompra1Info.baseicmsubst;
            clsNfCompra1InfoNew.basemp = clsNfCompra1Info.basemp;
            clsNfCompra1InfoNew.bccofins1 = clsNfCompra1Info.bccofins1;
            clsNfCompra1InfoNew.bcipi = clsNfCompra1Info.bcipi;
            clsNfCompra1InfoNew.bcpispasep = clsNfCompra1Info.bcpispasep;
            clsNfCompra1InfoNew.calculoautomatico = clsNfCompra1Info.calculoautomatico;
            clsNfCompra1InfoNew.codigoemp01 = clsNfCompra1Info.codigoemp01;
            clsNfCompra1InfoNew.codigoemp02 = clsNfCompra1Info.codigoemp02;
            clsNfCompra1InfoNew.codigoemp03 = clsNfCompra1Info.codigoemp03;
            clsNfCompra1InfoNew.codigoemp04 = clsNfCompra1Info.codigoemp04;
            clsNfCompra1InfoNew.cofins = clsNfCompra1Info.cofins;
            clsNfCompra1InfoNew.cofins1 = clsNfCompra1Info.cofins1;
            clsNfCompra1InfoNew.cofinsporc = clsNfCompra1Info.cofinsporc;
            clsNfCompra1InfoNew.complemento = clsNfCompra1Info.complemento;
            clsNfCompra1InfoNew.complemento1 = clsNfCompra1Info.complemento1;
            clsNfCompra1InfoNew.consumo = clsNfCompra1Info.consumo;
            // clsNfCompra1InfoNew.creditaricm
            clsNfCompra1InfoNew.csll = clsNfCompra1Info.csll;
            clsNfCompra1InfoNew.csllporc = clsNfCompra1Info.csllporc;
            clsNfCompra1InfoNew.custoicm = clsNfCompra1Info.custoicm;
            clsNfCompra1InfoNew.custoipi = clsNfCompra1Info.custoipi;
            clsNfCompra1InfoNew.datatabela = clsNfCompra1Info.datatabela;
            clsNfCompra1InfoNew.fatura = clsNfCompra1Info.fatura;
            clsNfCompra1InfoNew.faturando = clsNfCompra1Info.faturando;
            clsNfCompra1InfoNew.icm = clsNfCompra1Info.icm;
            // clsNfCompra1InfoNew.icminterno.ToString("N2");
            clsNfCompra1InfoNew.icmssubstreducao = clsNfCompra1Info.icmssubstreducao;
            clsNfCompra1InfoNew.idcentrocusto = clsNfCompra1Info.idcentrocusto;
            clsNfCompra1InfoNew.idcfop = clsNfCompra1Info.idcfop;
            clsNfCompra1InfoNew.idcfopfis = clsNfCompra1Info.idcfopfis;
            clsNfCompra1InfoNew.idcodigo = clsNfCompra1Info.idcodigo;
            clsNfCompra1InfoNew.idcodigo1 = clsNfCompra1Info.idcodigo1;
            clsNfCompra1InfoNew.idcodigoctabil = clsNfCompra1Info.idcodigoctabil;
            clsNfCompra1InfoNew.idcotacao = clsNfCompra1Info.idcotacao;
            clsNfCompra1InfoNew.iddestino = clsNfCompra1Info.iddestino;
            clsNfCompra1InfoNew.idhistorico = clsNfCompra1Info.idhistorico;
            clsNfCompra1InfoNew.idipi = clsNfCompra1Info.idipi;
            clsNfCompra1InfoNew.iditemcotacao = clsNfCompra1Info.iditemcotacao;
            clsNfCompra1InfoNew.iditempedido = clsNfCompra1Info.iditempedido;
            clsNfCompra1InfoNew.iditempedidoentrega = clsNfCompra1Info.iditempedidoentrega;
            clsNfCompra1InfoNew.idnfevales = clsNfCompra1Info.idnfevales;
            clsNfCompra1InfoNew.idnfvendaitem = clsNfCompra1Info.idnfvendaitem;
            clsNfCompra1InfoNew.idnotafiscal = clsNfCompra1Info.idnotafiscal;
            clsNfCompra1InfoNew.idordemservico = clsNfCompra1Info.idordemservico;
            clsNfCompra1InfoNew.idpedido = clsNfCompra1Info.idpedido;
            clsNfCompra1InfoNew.idpedidovenda = clsNfCompra1Info.idpedidovenda;
            clsNfCompra1InfoNew.idpedidovendaitem =clsNfCompra1Info.idpedidovendaitem;
            clsNfCompra1InfoNew.idsittriba = clsNfCompra1Info.idsittriba;
            clsNfCompra1InfoNew.idsittribb = clsNfCompra1Info.idsittribb;
            clsNfCompra1InfoNew.idsittribcofins1 = clsNfCompra1Info.idsittribcofins1;
            clsNfCompra1InfoNew.idsittribipi = clsNfCompra1Info.idsittribipi;
            clsNfCompra1InfoNew.idsittribpis = clsNfCompra1Info.idsittribpis;
            clsNfCompra1InfoNew.idsolicitacao = clsNfCompra1Info.idsolicitacao;
            clsNfCompra1InfoNew.idtiponota = clsNfCompra1Info.idtiponota;
            clsNfCompra1InfoNew.idtiponotasaida = clsNfCompra1Info.idtiponotasaida;
            clsNfCompra1InfoNew.idunid = clsNfCompra1Info.idunid;
            clsNfCompra1InfoNew.idunidfiscal = clsNfCompra1Info.idunidfiscal;
            clsNfCompra1InfoNew.irrfporc = clsNfCompra1Info.irrfporc;
            clsNfCompra1InfoNew.irrf = clsNfCompra1Info.irrf;
            clsNfCompra1InfoNew.inss = clsNfCompra1Info.inss;
            clsNfCompra1InfoNew.inssporc = clsNfCompra1Info.inssporc;
            clsNfCompra1InfoNew.iss = clsNfCompra1Info.iss;
            clsNfCompra1InfoNew.issporc = clsNfCompra1Info.issporc;
            clsNfCompra1InfoNew.ipi = clsNfCompra1Info.ipi;
            clsNfCompra1InfoNew.peso = clsNfCompra1Info.peso;
            clsNfCompra1InfoNew.pesototal = clsNfCompra1Info.pesototal;
            clsNfCompra1InfoNew.pis = clsNfCompra1Info.pis;
            clsNfCompra1InfoNew.piscofinscsll = clsNfCompra1Info.piscofinscsll;
            clsNfCompra1InfoNew.piscofinscsllporc = clsNfCompra1Info.piscofinscsllporc;
            clsNfCompra1InfoNew.pispasep = clsNfCompra1Info.pispasep;
            clsNfCompra1InfoNew.pisporc = clsNfCompra1Info.pisporc;

            clsNfCompra1InfoNew.preco = clsNfCompra1Info.preco;
            clsNfCompra1InfoNew.qtde = clsNfCompra1Info.qtde;
            clsNfCompra1InfoNew.qtdefiscal = clsNfCompra1Info.qtdefiscal;
            clsNfCompra1InfoNew.qtdeticket = clsNfCompra1Info.qtdeticket;
            clsNfCompra1InfoNew.reducao = clsNfCompra1Info.reducao;
            clsNfCompra1InfoNew.somaproduto = clsNfCompra1Info.somaproduto;
            clsNfCompra1InfoNew.tipodestino = clsNfCompra1Info.tipodestino;
            clsNfCompra1InfoNew.tipoproduto = clsNfCompra1Info.tipoproduto;
            clsNfCompra1InfoNew.totalmercado = clsNfCompra1Info.totalmercado;
            clsNfCompra1InfoNew.totalnota = clsNfCompra1Info.totalnota;
            clsNfCompra1InfoNew.valorfrete = clsNfCompra1Info.valorfrete;
            clsNfCompra1InfoNew.valorfreteicms = clsNfCompra1Info.valorfreteicms;
            clsNfCompra1InfoNew.valoroutras = clsNfCompra1Info.valoroutras;
            clsNfCompra1InfoNew.valoroutrasicms = clsNfCompra1Info.valoroutrasicms;
            clsNfCompra1InfoNew.valorseguro = clsNfCompra1Info.valorseguro;
            clsNfCompra1InfoNew.valorseguroicms = clsNfCompra1Info.valorseguroicms;
        }
        private void NFCompraPagarFillInfo(clsNfCompraPagarInfo info)
        {
            clsNfCompraPagarInfoNew.id = 0;
            clsNfCompraPagarInfoNew.idnota = idNotaNew;
            clsNfCompraPagarInfoNew.boletonro =  clsNfCompraPagarInfo.boletonro;
            clsNfCompraPagarInfo.data = clsNfCompraPagarInfo.data.AddMonths(1);
            clsNfCompraPagarInfoNew.data = clsNfCompraPagarInfo.data;
            clsNfCompraPagarInfoNew.dv = clsNfCompraPagarInfo.dv;
            clsNfCompraPagarInfoNew.idtipopaga = clsNfCompraPagarInfo.idtipopaga;
            clsNfCompraPagarInfoNew.pagou = "N";
            clsNfCompraPagarInfoNew.posicao = clsNfCompraPagarInfo.posicao;
            clsNfCompraPagarInfoNew.posicaofim = clsNfCompraPagarInfo.posicaofim;
            clsNfCompraPagarInfoNew.valor = clsNfCompraPagarInfo.valor;
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


    }
}
