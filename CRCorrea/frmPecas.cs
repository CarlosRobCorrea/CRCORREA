using CRCorreaBLL;
using CRCorrea;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPecas : Form
    {
        clsPecasBLL clsPecasBLL;
        clsPecasInfo clsPecasInfo;
        clsPecasInfo clsPecasInfoOld;
        
        Int32 id;
        Int32 idclassifica;
        Int32 idclassifica1;
        Int32 idclassifica2;
        Int32 idcliente;
        Int32 idhistoricobco;
        Int32 idipi;
        Int32 idMarca;
        Int32 idsittriba;
        Int32 idsittribvenda;
        Int32 idunidade;
        Int32 idunidadecom;
        Int32 idSemelhante;
        Int32 idSemelhante1;
        Int32 idSemelhante2;

        DataGridViewRowCollection rows;

        //Lista de Preços (PecasPreco)
        Int32 pecaspreco_id;
        Int32 pecaspreco_idcodigo;
        Int32 pecaspreco_posicao;

        clsPecasPrecoBLL clsPecasPrecoBLL;
        clsPecasPrecoInfo clsPecasPrecoInfo;
        clsPecasPrecoInfo clsPecasPrecoInfoOld;

        DataTable dtPecasPreco;

        //Notas fiscais de Entrada Itens
        DataTable dtNFCompra1;

        //Pedidos de Vendas Itens
        DataTable dtPedido1;
        clsPedido1BLL clsPedido1BLL = new clsPedido1BLL();
        clsPedido1Info clsPedido1Info = new clsPedido1Info();


        //Fornecedores PECASFORNE
        Int32 pecasforne_id;
        Int32 pecasforne_idcodigo;
        Int32 pecasforne_idfornecedor;
        Int32 pecasforne_idunidade;
        Int32 pecasforne_posicao;

        public frmPecas()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            this.id = _id;
            this.rows = _rows;

            clsPecasBLL = new clsPecasBLL();

            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "", tbxCliente);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "UNIDADE", "CODIGO", "", tbxUnidCompra);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "UNIDADE", "CODIGO", "", tbxUnidVenda);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "SITTRIBUTARIAA", "CODIGO", "", tbxSituacaOrigem);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "SITTRIBUTARIAB", "CODIGO", "", tbxSituacaoIcms);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "IPI", "CODIGO", "", tbxIpi);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "PECASCLASSIFICA", "CODIGO", "", tbxPecasClassifica);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "PECASTIPO", "CODIGO", "", tbxMarcaProduto);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "", tbxHistoricoBco);

            clsPecasPrecoBLL = new clsPecasPrecoBLL();


            tspPecasPreco.Visible = true;
            tspUltimasRemessas.Visible = true;
            tspUltimasCompras.Visible = true;
            tspPecas.Visible = true;
            tspPecasPrecoRegistro.Visible = true;

        }

        private void frmPecas_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);

        }

        private void frmPecas_Load(object sender, EventArgs e)
        {
            PecasCarregar();
        }

        private void frmPecas_Shown(object sender, EventArgs e)
        {
            //            clsFormHelper.VerificarForm(this, ttpPecas);
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

        private void PecasCarregar()
        {
            clsPecasInfoOld = new clsPecasInfo();
            if (id == 0)
            {
                clsPecasInfo = new clsPecasInfo();
                clsPecasInfo.aplicacao = "";
                clsPecasInfo.ativo = "S";
                //clsPecasInfo.cetesb = "N";
                clsPecasInfo.codigo = "";
                clsPecasInfo.codigobarra = "";
                clsPecasInfo.codigofornecedor = "";
                clsPecasInfo.compraautomatica = "N";
                clsPecasInfo.comprarqtde = 0;
                clsPecasInfo.comprarvia = "S";
                clsPecasInfo.diasentrega = 7;
                clsPecasInfo.diasestoque = 30;
                clsPecasInfo.estoquemin = 0;
                clsPecasInfo.estoqueminaut = "S";
                clsPecasInfo.fatorconv = 0;
                clsPecasInfo.foto = null;
                clsPecasInfo.id = 0;
                clsPecasInfo.idclassifica = clsInfo.zclassificacao;
                clsPecasInfo.idclassifica1 = clsInfo.zclassificacao1;
                clsPecasInfo.idclassifica2 = clsInfo.zclassificacao2;
                clsPecasInfo.idcliente = clsInfo.zempresaclienteid;
                clsPecasInfo.idhistoricobco = clsInfo.zhistoricos;
                clsPecasInfo.idipi = clsInfo.zipi;
                clsPecasInfo.idmarca = 0;
                clsPecasInfo.idsemelhante = 0;
                clsPecasInfo.idsemelhante1 = 0;
                clsPecasInfo.idsemelhante2 = 0;
                clsPecasInfo.idsittriba = clsInfo.zsituacaotriba;
                clsPecasInfo.idsittribvenda = clsInfo.zsituacaotribb;
                clsPecasInfo.idunidade = clsInfo.zunidade;
                clsPecasInfo.idunidadecom = clsInfo.zunidade;
                clsPecasInfo.locacao = "";
                clsPecasInfo.nome = "";
                clsPecasInfo.pesobruto = 0;
                clsPecasInfo.pesounit = 0;
                clsPecasInfo.precocompra = 0;
                clsPecasInfo.precovenda = 0;
                //clsPecasInfo.produto = "N";
                clsPecasInfo.qtdeentra = 0;
                clsPecasInfo.qtdeinicio = 0;
                clsPecasInfo.qtdeentra = 0;
                clsPecasInfo.qtdesaida = 0;
                clsPecasInfo.qtdesaldo = 0;
                clsPecasInfo.tipoproduto = "00";
            }
            else
            {
                clsPecasInfo = clsPecasBLL.Carregar(id, clsInfo.conexaosqldados);
            }

            PecasCampos(clsPecasInfo);
            PecasFillInfo(clsPecasInfoOld);

            // carregando o pecaspreco
            dtPecasPreco = clsPecasPrecoBLL.GridDados(id, clsInfo.conexaosqldados);
            DataColumn dcPosicao = new DataColumn("posicao", Type.GetType("System.Int32"));
            dtPecasPreco.Columns.Add(dcPosicao);
            for (Int32 i = 1; i <= dtPecasPreco.Rows.Count; i++)
            {
                dtPecasPreco.Rows[i - 1]["posicao"] = i;
            }
            dtPecasPreco.AcceptChanges();
            //String tipoproduto = lbxTipoProduto.Text.Substring(0, 1);
            clsPecasPrecoBLL.GridMonta(dgvPecasPreco, dtPecasPreco, pecaspreco_posicao);


            // carregando as notas fiscais de entrada relaciona com este produto
            dtNFCompra1 = clsNfCompra1BLL.GridDadosPeca(id, clsInfo.conexaosqldados);
            clsNfCompra1BLL.GridMontaPeca(dgvNFCompra, dtNFCompra1, 0);

            // carregando as notas fiscais saidas/emitidas relacionada com este produto  (vide resol)
            dtPedido1 = clsPedido1BLL.GridDadosPeca(id, clsInfo.conexaosqldados);
            clsPedido1BLL.GridMontaPeca(dgvNFVenda, dtPedido1, 0);

            // gravar o ultimo preço de venda (aqui ele encherga a mudança para gravar
            if (id > 0)
            {
                tbxPrecoVenda.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoraprazo from pecaspreco where idcodigo = " + id + " order by data desc")).ToString("N4");
                if (clsParser.DecimalParse(tbxPrecoVenda.Text) > 0)
                {
                    tbxPecasPrecoData.Text = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from pecaspreco where idcodigo = " + id + " order by data desc")).ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                {
                    tbxPecasPrecoData.Text = "";
                }
            }
            // gravar o ultimo preço de compra (aqui ele enxerga a mudança para gravar
            Decimal precocompranfe = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select preco from nfcompra1 left join nfcompra on nfcompra.id = nfcompra1.numero where idcodigo = " + id + " order by nfcompra.data desc", ""));
            if (precocompranfe > 0)
            {  // as vezes o valor entrou via requisição de material e não por NFiscal
                tbxPrecoCompra.Text = precocompranfe.ToString("N4");
            }
            Int32 idnfcompra = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nfcompra.id from nfcompra1 left join nfcompra on nfcompra.id = nfcompra1.numero where idcodigo = " + id + " order by nfcompra.data desc"));
            if (clsParser.DecimalParse(tbxPrecoCompra.Text) > 0)
            {
                tbxDataCompra.Text = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from nfcompra where id = " + idnfcompra + "")).ToString("dd/MM/yyyy HH:mm:ss");
            }
            else
            {
                tbxDataCompra.Text = "";
            }
            // Verificar o Fator
            if (clsParser.DecimalParse(tbxFatorConv.Text) == 0 && clsParser.DecimalParse(tbxPrecoVenda.Text) > 0 && clsParser.DecimalParse(tbxPrecoCompra.Text) > 0)
            {  // Fator Padrão esta Zerado então calcula e coloca
                tbxFatorConv.Text = (clsParser.DecimalParse(tbxPrecoVenda.Text) / clsParser.DecimalParse(tbxPrecoCompra.Text)).ToString("N4");
            }
            // Atualizar o Fator da Ultima compra
            if (clsParser.DecimalParse(tbxPrecoVenda.Text) > 0 && clsParser.DecimalParse(tbxPrecoCompra.Text) > 0)
            {  // Fator Padrão esta Zerado então calcula e coloca
                tbxFatorConvHoje.Text = (clsParser.DecimalParse(tbxPrecoVenda.Text) / clsParser.DecimalParse(tbxPrecoCompra.Text)).ToString("N4");
            }
            else
            {
                tbxFatorConvHoje.Text = "";
            }

        }
        void PecasCampos(clsPecasInfo info)
        {
            id = info.id;

            //Pecastipo_Carrega();  // CARREGAR ESTE TIPO 

            tbxAplicacao.Text = info.aplicacao;
//            tbxAplicacao1.Text = info.aplicacao1;
            if (info.ativo == "S") rbnAtivo.Checked = true;
            else rbnInativo.Checked = true;


            tbxCodigo.Text = info.codigo;
            tbxCodigoBarra.Text = info.codigobarra;
            tbxCodigoFornecedor.Text = info.codigofornecedor;
            cbxCompraAutomatica.SelectedIndex = cbxCompraAutomatica.FindString(info.compraautomatica);
            if (cbxCompraAutomatica.SelectedIndex == -1)
            {
                cbxCompraAutomatica.SelectedIndex = 0;
            }
            tbxComprarQtde.Text = info.comprarqtde.ToString();
            cbxComprarVia.SelectedIndex = cbxComprarVia.FindString(info.comprarvia);
            if (cbxComprarVia.SelectedIndex == -1)
            {
                cbxComprarVia.SelectedIndex = 0;
            }
            tbxDiasEntrega.Text = info.diasentrega.ToString();
            tbxDiasEstoque.Text = info.diasestoque.ToString();
            tbxEstoqueMin.Text = info.estoquemin.ToString("N3");
            if (info.estoqueminaut == "S")
            {
                ckxEstoqueMinAtu.Checked = true;
                ckxEstoqueMinAtu.Text = "Sim - calcula estoque minimo automáticamente";
            }
            else
            {
                ckxEstoqueMinAtu.Checked = false;
                ckxEstoqueMinAtu.Text = "Não - calcula estoque minimo automáticamente";
            }
            tbxFatorConv.Text = info.fatorconv.ToString("N4");
            pbxFoto.Image = info.foto;
            id = info.id;
            idclassifica = info.idclassifica;
            if (idclassifica == 0)
            {
                idclassifica = clsInfo.zclassificacao;
            }
            tbxPecasClassifica.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECASCLASSIFICA where id = " + idclassifica);
            idclassifica1 = info.idclassifica1;
            if (idclassifica1 == 0)
            {
                idclassifica1 = clsInfo.zclassificacao1;
            }
            tbxPecasClassifica1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECASCLASSIFICA1 where id = " + idclassifica1);
            idclassifica2 = info.idclassifica2;
            if (idclassifica2 == 0)
            {
                idclassifica2 = clsInfo.zclassificacao2;
            }
            tbxPecasClassifica2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECASCLASSIFICA2 where id = " + idclassifica2);
            idcliente = info.idcliente;
            if (idcliente == 0)
            {
                idcliente = clsInfo.zempresaclienteid;
            }
            //tbxCliente.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where id = " + idcliente);
            idhistoricobco = info.idhistoricobco;
            if (idhistoricobco == 0)
            {
                idhistoricobco = clsInfo.zhistoricos;
            }
            tbxHistoricoBco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where id = " + idhistoricobco);
            idipi = info.idipi;
            if (idipi == 0)
            {
                idipi = clsInfo.zipi;
            }
            tbxIpi.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from IPI where id = " + idipi);
            idMarca = info.idmarca;
            if (idMarca > 0)
            {
                tbxMarcaProduto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECASTIPO where id = " + idMarca + "","");
            }
            else
            {
                tbxMarcaProduto.Text = "";
            }
            idSemelhante = info.idsemelhante;
            tbxSemelhante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where id = " + idSemelhante + "", "");
            tbxSemelhanteNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where id = " + idSemelhante + "", "");
            idSemelhante1 = info.idsemelhante1;
            tbxSemelhante1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where id = " + idSemelhante1 + "", "");
            tbxSemelhanteNome1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where id = " + idSemelhante1 + "", "");
            idSemelhante2 = info.idsemelhante2;
            tbxSemelhante2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where id = " + idSemelhante2 + "", "");
            tbxSemelhanteNome2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where id = " + idSemelhante2 + "", "");
            idsittriba = info.idsittriba;
            if (idsittriba == 0)
            {
                idsittriba = clsInfo.zsituacaotriba;
            }
            tbxSituacaOrigem.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITTRIBUTARIAA where id = " + idsittriba);
            idsittribvenda = info.idsittribvenda; // SITTRIBUTARIAB
            if (idsittribvenda == 0)
            {
                idsittribvenda = clsInfo.zsituacaotribb;
            }
            tbxSituacaoIcms.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITTRIBUTARIAB where id = " + idsittribvenda);
            idunidade = info.idunidade;
            if (idunidade == 0)
            {
                idunidade = clsInfo.zunidade;
            }
            tbxUnidVenda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from unidade where id = " + idunidade);
            idunidadecom = info.idunidadecom;
            if (idunidadecom == 0)
            {
                idunidadecom = clsInfo.zunidade;
            }
            tbxUnidCompra.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from unidade where id = " + idunidadecom);
            tbxLocacao.Text = info.locacao;
            tbxNome.Text = info.nome;
            tbxPesoBruto.Text = info.pesobruto.ToString("N3");
            tbxPesoUnit.Text = info.pesounit.ToString("N3");
            // o preço objetivo na Resol é sempre o valor do Dolar
            tbxDataCompra.Text = info.datacompra.ToString("dd/MM/yy");
            tbxPrecoCompra.Text = (info.precocompra).ToString("N4");
            // pegar a lista de preço e verificar o ultimo preço vigente
            if (dtPecasPreco != null)
            {
                Int32 xy = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecaspreco where idcodigo = " + id + " "));
                if (xy > 0)
                {
                    tbxPecasPrecoData.Text = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from pecaspreco where idcodigo = " + id + " order by data desc")).ToString("dd/MM/yyyy HH:mm:ss");
                }
            }
            else
            {
                tbxPecasPrecoData.Text = "";
            }
            tbxPrecoVenda.Text = info.precovenda.ToString("N4");
            tbxQtdeEntra.Text = info.qtdeentra.ToString("N3");
            tbxQtdeInicio.Text = info.qtdeinicio.ToString("N3");
            tbxQtdeSaida.Text = info.qtdesaida.ToString("N3");
            tbxQtdeSaldo.Text = ((info.qtdeinicio + info.qtdeentra) - info.qtdesaida).ToString("N3");
            lbxTipoProduto.SelectedIndex = lbxTipoProduto.FindString(info.tipoproduto);
            if (lbxTipoProduto.SelectedIndex == -1)
            {
                lbxTipoProduto.SelectedIndex = 0;
            }


            if (id == 0)
            {
                tbxCodigo.ReadOnly = false;
                tbxCodigo.TabStop = true;

            }
            else
            {
                if (info.ativo != "S")
                {
                    tbxCodigo.ReadOnly = false;
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                }
                else
                {
                    tbxNome.Select();
                    tbxNome.SelectAll();
                }
            }

        }
        void PecasFillInfo(clsPecasInfo info)
        {

            if (rbnAtivo.Checked == true) info.ativo = "S";
            else info.ativo = "N";
            info.aplicacao = tbxAplicacao.Text;
            info.codigo = tbxCodigo.Text;
            info.codigobarra = tbxCodigoBarra.Text;
            info.codigofornecedor = tbxCodigoFornecedor.Text;
            info.compraautomatica = cbxCompraAutomatica.Text.Substring(0, 1);
            info.comprarqtde = clsParser.DecimalParse(tbxComprarQtde.Text);
            info.comprarvia = cbxCompraAutomatica.Text.Substring(0, 1);
            //info.consumo = lbxConsumo.Text.Substring(0, 1);
            info.datacompra = clsParser.DateTimeParse(tbxDataCompra.Text);
            info.datapreco = clsParser.DateTimeParse(tbxPecasPrecoData.Text);
            info.diasentrega = clsParser.Int32Parse(tbxDiasEntrega.Text);
            info.diasestoque = clsParser.Int32Parse(tbxDiasEstoque.Text);
            info.estoquemin = clsParser.DecimalParse(tbxEstoqueMin.Text);
            info.estoqueminaut = ckxEstoqueMinAtu.Text.Substring(0, 1);
            info.fatorconv = clsParser.DecimalParse(tbxFatorConv.Text);
            info.foto = pbxFoto.Image;
            //info.fotocaminho = tbxFotoCaminho.Text;
            info.id = id;
            info.idclassifica = idclassifica;
            info.idclassifica1 = idclassifica1;
            info.idclassifica2 = idclassifica2;
            info.idcliente = idcliente;
            info.idhistoricobco = idhistoricobco;
            info.idipi = idipi;
            info.idmarca = idMarca;
            info.idsemelhante = idSemelhante;
            info.idsemelhante1 = idSemelhante1;
            info.idsemelhante2 = idSemelhante2;
            info.idsittriba = idsittriba;
            info.idsittribvenda = idsittribvenda;
            info.idunidade = idunidade;
            info.idunidadecom = idunidadecom;
            info.locacao = tbxLocacao.Text;
            info.nome = tbxNome.Text;
            info.pesounit = clsParser.DecimalParse(tbxPesoUnit.Text);
            info.pesobruto = clsParser.DecimalParse(tbxPesoBruto.Text);
            info.precocompra = clsParser.DecimalParse(tbxPrecoCompra.Text);
            info.precovenda = clsParser.DecimalParse(tbxPrecoVenda.Text);          
            info.qtdeentra = clsParser.DecimalParse(tbxQtdeEntra.Text);
            info.qtdeinicio = clsParser.DecimalParse(tbxQtdeInicio.Text);
            info.qtdeentra = clsParser.DecimalParse(tbxQtdeEntra.Text);
            info.qtdesaida = clsParser.DecimalParse(tbxQtdeSaida.Text);
            info.qtdesaldo = clsParser.DecimalParse(tbxQtdeSaldo.Text);
            info.tipoproduto = lbxTipoProduto.Text.Substring(0, 2);

        }

        private Boolean HouveModificacoes()
        {
            clsPecasInfo = new clsPecasInfo();
            PecasFillInfo(clsPecasInfo);
            if (clsPecasBLL.Equals(clsPecasInfo, clsPecasInfoOld) == false)
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
                    PecasSalvar();
                }     
            return drt;
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Salvar() == DialogResult.Cancel)
                {
                    return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message: " + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    PecasCarregar();
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
                                PecasCarregar();
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
                                PecasCarregar();
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
                    PecasCarregar();
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
        //private void Pecastipo_Carrega()
        //{
        //    lbxTipoProduto.Items.Clear();
        //    String query;
        //    SqlDataAdapter sda;
        //    query = "select ID, CODIGO + ' - ' + NOME AS [CODIGONOME] FROM PECASTIPO ORDER BY CODIGO";

        //    DataTable dtTemp = new DataTable();
        //    sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
        //    sda.Fill(dtTemp);
        //    foreach (DataRow row in dtTemp.Rows)
        //    {
        //        lbxTipoProduto.Items.Add(row["CODIGONOME"].ToString());
        //    }
        //}

        void TrataCampos(Control ctl)
        {
            if (ctl is TextBox)
            {
                if (tbxCodigo.Text != "")
                {
                    if (ctl.Name == tbxCodigo.Name)
                    { // Verificar se já não existe codigo similar

                        Int32 idcodigonew = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + tbxCodigo.Text + "'"));
                        if (idcodigonew > 0 && idcodigonew != id)
                        { // esta apenas alterando algo
                            MessageBox.Show("O codigo do item que esta digitando já existe - altere - não pode haver 2 codigos iguais");
                            tbxCodigo.Text = "";
                            tbxCodigo.Select();

                        }

                    }
                }
                if (ctl.Name == tbxMarcaProduto.Name)
                {
                    idMarca = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASTIPO where CODIGO='" + tbxMarcaProduto.Text + "'"));
                    if (idMarca == 0)
                    {
                        idMarca = clsInfo.zmarca;
                    }
                    tbxMarcaProduto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASTIPO where id = " + idMarca + "", "");
                }


                if (ctl.Name == tbxUnidCompra.Name)
                {
                    idunidadecom = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from UNIDADE where CODIGO='" + tbxUnidCompra.Text + "'"));
                    if (idunidadecom == 0)
                    {
                        idunidadecom = clsInfo.zunidade;
                    }
                    tbxUnidCompra.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id = " + idunidadecom);
                }
                if (ctl.Name == tbxUnidVenda.Name)
                {
                    idunidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from UNIDADE where CODIGO='" + tbxUnidVenda.Text + "'"));
                    if (idunidade == 0)
                    {
                        idunidade = clsInfo.zunidade;
                    }
                    tbxUnidVenda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id = " + idunidade);
                }
                if (ctl.Name == tbxSituacaOrigem.Name)
                {
                    idsittriba = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAA where CODIGO='" + tbxSituacaOrigem.Text + "'"));
                    if (idsittriba == 0)
                    {
                        idsittriba = clsInfo.zsituacaotriba;
                    }
                    tbxSituacaOrigem.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITTRIBUTARIAA where id = " + idsittriba);
                }
                if (ctl.Name == tbxSituacaoIcms.Name)
                {
                    idsittribvenda = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAB where CODIGO='" + tbxSituacaoIcms.Text + "'"));
                    if (idsittribvenda == 0)
                    {
                        idsittribvenda = clsInfo.zsituacaotribb;
                    }
                    tbxSituacaoIcms.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITTRIBUTARIAB where id = " + idsittribvenda);
                }
                if (ctl.Name == tbxIpi.Name)
                {
                    Int32 idtmp = idipi;

                    idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from IPI where CODIGO='" + tbxIpi.Text + "'"));
                    if (idipi == 0)
                    {
                        idipi = clsInfo.zipi;
                    }
                    tbxIpi.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from IPI where id = " + idipi);

                }
                if (ctl.Name == tbxPecasClassifica.Name)
                {
                    idclassifica = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASCLASSIFICA where CODIGO='" + tbxPecasClassifica.Text + "'"));
                    if (idclassifica == 0)
                    {
                        idclassifica = clsInfo.zclassificacao;
                    }
                    tbxPecasClassifica.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA where id = " + idclassifica);
                }
                if (ctl.Name == tbxPecasClassifica1.Name)
                {
                    idclassifica1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASCLASSIFICA1 where IDCLASSIFICA=" + idclassifica + " AND CODIGO='" + tbxPecasClassifica1.Text + "'"));
                    if (idclassifica1 == 0)
                    {
                        idclassifica1 = clsInfo.zclassificacao1;
                    }
                    tbxPecasClassifica1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA1 where id = " + idclassifica1);
                }
                if (ctl.Name == tbxPecasClassifica2.Name)
                {
                    idclassifica2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECASCLASSIFICA2 where IDCLASSIFICA=" + idclassifica + " AND IDCLASSIFICA1=" + idclassifica1 + " AND CODIGO='" + tbxPecasClassifica2.Text + "'"));
                    if (idclassifica2 == 0)
                    {
                        idclassifica2 = clsInfo.zclassificacao2;
                    }
                    tbxPecasClassifica2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA2 where id = " + idclassifica2);
                }
                if (ctl.Name == tbxHistoricoBco.Name)
                {
                    idhistoricobco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO='" + tbxHistoricoBco.Text + "'"));
                    if (idhistoricobco == 0)
                    {
                        idhistoricobco = clsInfo.zhistoricos;
                    }
                    tbxHistoricoBco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where id = " + idhistoricobco);

                }
                else if (ctl.Name == tbxSemelhante.Name)
                {
                    idSemelhante = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + tbxSemelhante.Text + "'"));
                    if (idSemelhante == 0)
                    {
                        idSemelhante = clsInfo.zpecas;
                    }
                    tbxSemelhanteNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from PECAS where id = " + idSemelhante);
                }
                else if (ctl.Name == tbxSemelhante1.Name)
                {
                    idSemelhante1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + tbxSemelhante1.Text + "'"));
                    if (idSemelhante1 == 0)
                    {
                        idSemelhante1 = clsInfo.zpecas;
                    }
                    tbxSemelhanteNome1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from PECAS where id = " + idSemelhante1);
                }
                else if (ctl.Name == tbxSemelhante2.Name)
                {
                    idSemelhante2 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + tbxSemelhante2.Text + "'"));
                    if (idSemelhante2 == 0)
                    {
                        idSemelhante2 = clsInfo.zpecas;
                    }
                    tbxSemelhanteNome2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from PECAS where id = " + idSemelhante2);
                }

            }
            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {
                if (clsInfo.znomegrid == btnMarca.Name)
                {
                    idMarca = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxMarcaProduto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxMarcaProduto.Select();
                    tbxMarcaProduto.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdUnidadeCom.Name)
                {
                    idunidadecom = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxUnidCompra.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id = " + idunidadecom);
                    tbxUnidCompra.Select();
                    tbxUnidCompra.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdUnidade.Name)
                {
                    idunidade = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxUnidVenda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id = " + idunidade);
                    tbxUnidVenda.Select();
                    tbxUnidVenda.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdSittriba.Name)
                {
                    idsittriba = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxSituacaOrigem.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITTRIBUTARIAA where id = " + idsittriba);
                    tbxSituacaOrigem.Select();
                    tbxSituacaOrigem.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdSittribaVenda.Name)
                {
                    idsittribvenda = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxSituacaoIcms.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITTRIBUTARIAB where id = " + idsittribvenda);
                    tbxSituacaoIcms.Select();
                    tbxSituacaoIcms.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdIpi.Name)
                {
                    idipi = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxIpi.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from IPI where id = " + idipi);
                    //tbxAliquotaIpi.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from IPI where id = " + idipi)).ToString("n2");
                    tbxIpi.Select();
                    tbxIpi.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdPecasClassifica.Name)
                {
                    idclassifica = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPecasClassifica.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA where id = " + idclassifica);
                    tbxPecasClassifica.Select();
                    tbxPecasClassifica.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdPecasClassifica1.Name)
                {
                    idclassifica1 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPecasClassifica1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA1 where id = " + idclassifica1);
                    tbxPecasClassifica1.Select();
                    tbxPecasClassifica1.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdPecasClassifica2.Name)
                {
                    idclassifica2 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPecasClassifica2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA2 where id = " + idclassifica2);
                    tbxPecasClassifica2.Select();
                    tbxPecasClassifica2.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdHistoricobco.Name)
                {
                    idhistoricobco = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxHistoricoBco.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where id = " + idhistoricobco);
                    tbxHistoricoBco.Select();
                    tbxHistoricoBco.SelectAll();
                }
                else if (clsInfo.znomegrid == btnSemelhante.Name)
                {
                    idSemelhante = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxSemelhante.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS where id = " + idSemelhante);
                    tbxSemelhanteNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from PECAS where id = " + idSemelhante);
                    tbxSemelhante.Select();
                    tbxSemelhante.SelectAll();
                }
                else if (clsInfo.znomegrid == btnSemelhante1.Name)
                {
                    idSemelhante1 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxSemelhante1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS where id = " + idSemelhante1);
                    tbxSemelhanteNome1.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from PECAS where id = " + idSemelhante1);
                    tbxSemelhante1.Select();
                    tbxSemelhante1.SelectAll();
                }
                else if (clsInfo.znomegrid == btnSemelhante2.Name)
                {
                    idSemelhante2 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxSemelhante2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS where id = " + idSemelhante2);
                    tbxSemelhanteNome2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from PECAS where id = " + idSemelhante2);
                    tbxSemelhante2.Select();
                    tbxSemelhante2.SelectAll();
                }

            }

            //if (ctl.Name == lbxTipoEntrada.Name)
            //{
            //    tbxSituacaOrigem.Select();
            //    tbxSituacaOrigem.SelectAll();
            //}
            // CALCULOS
            tbxNome.Text = clsVisual.RemoveAcentos(tbxNome.Text);
            //tbxCodigoOnu.Text = clsParser.Int32Parse(tbxCodigoOnu.Text).ToString();
            tbxPesoBruto.Text = (clsParser.DecimalParse(tbxPesoBruto.Text)).ToString("N3");
            tbxPesoUnit.Text = (clsParser.DecimalParse(tbxPesoUnit.Text)).ToString("N3");
            //tbxFatorConv.Text = (clsParser.DecimalParse(tbxFatorConv.Text)).ToString("N6");
            //tbxBaseMP.Text = (clsParser.DecimalParse(tbxBaseMP.Text)).ToString("N2");
            //tbxMaoObra.Text = (100 - clsParser.DecimalParse(tbxBaseMP.Text)).ToString("N2");

            // aba de compras
            if (ctl.Name == ckxEstoqueMinAtu.Name)
            {
                if (ckxEstoqueMinAtu.Checked == true)
                {
                    ckxEstoqueMinAtu.Text = "Sim - calcula estoque minimo automáticamente";
                }
                else
                {
                    ckxEstoqueMinAtu.Text = "Não - calcula estoque minimo automáticamente";
                }
            }
            tbxDiasEntrega.Text = (clsParser.DecimalParse(tbxDiasEntrega.Text)).ToString("N0");
            tbxDiasEstoque.Text = (clsParser.DecimalParse(tbxDiasEstoque.Text)).ToString("N0");
            tbxComprarQtde.Text = (clsParser.DecimalParse(tbxComprarQtde.Text)).ToString("N3");
            tbxEstoqueMin.Text = (clsParser.DecimalParse(tbxEstoqueMin.Text)).ToString("N3");
            tbxDataCompra.Text = (clsParser.DateTimeParse(tbxDataCompra.Text)).ToString("dd/MM/yy");
            tbxPrecoCompra.Text = (clsParser.DecimalParse(tbxPrecoCompra.Text)).ToString("N4");

            //if (ctl.Name == tbxDataCompra.Name)
            //{
            //    tclPecas.SelectedIndex = 0;

            //}
            // TABELA DE PREÇOS
            PecasPreco_tbxValorAVista.Text = clsParser.DecimalParse(PecasPreco_tbxValorAVista.Text).ToString("N4");
            if (clsParser.DecimalParse(PecasPreco_tbxValorAVista.Text) > 0 && clsParser.DecimalParse(PecasPreco_tbxValorAPrazo.Text) == 0)
            {
                PecasPreco_tbxValorAPrazo.Text = PecasPreco_tbxValorAVista.Text;
                //PecasPreco_tbxValorManutencao.Text = PecasPreco_tbxValorAVista.Text;
            }

            PecasPreco_tbxValorAPrazo.Text = clsParser.DecimalParse(PecasPreco_tbxValorAPrazo.Text).ToString("N4");
            //PecasPreco_tbxValorManutencao.Text = clsParser.DecimalParse(PecasPreco_tbxValorManutencao.Text).ToString("N4");

            // Verificar o Fator
            if (clsParser.DecimalParse(tbxFatorConv.Text) == 0 && clsParser.DecimalParse(tbxPrecoVenda.Text) > 0 && clsParser.DecimalParse(tbxPrecoCompra.Text) > 0)
            {  // Fator Padrão esta Zerado então calcula e coloca
               tbxFatorConv.Text = (clsParser.DecimalParse(tbxPrecoVenda.Text) / clsParser.DecimalParse(tbxPrecoCompra.Text)).ToString("N4");
            }
            // Atualizar o Fator da Ultima compra
            if (clsParser.DecimalParse(tbxPrecoVenda.Text) > 0 && clsParser.DecimalParse(tbxPrecoCompra.Text) > 0)
            {  // Fator Padrão esta Zerado então calcula e coloca
                tbxFatorConvHoje.Text = (clsParser.DecimalParse(tbxPrecoVenda.Text) / clsParser.DecimalParse(tbxPrecoCompra.Text)).ToString("N4");
            }

            clsInfo.znomegrid = "";
        }

        private void btnIdUnidadeCom_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdUnidadeCom.Name;
            frmUnidadePes frmUnidadePes = new frmUnidadePes();
            frmUnidadePes.Init(idunidadecom);

            clsFormHelper.AbrirForm(this.MdiParent, frmUnidadePes, clsInfo.conexaosqldados);

        }

        private void btnIdUnidade_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdUnidade.Name;
            frmUnidadePes frmUnidadePes = new frmUnidadePes();
            frmUnidadePes.Init(idunidade);

            clsFormHelper.AbrirForm(this.MdiParent, frmUnidadePes, clsInfo.conexaosqldados);
        }

        private void btnIdSittriba_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdSittriba.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBUTARIAA", idsittriba, "Situação Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdSittribaVenda_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdSittribaVenda.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBUTARIAB", idsittribvenda, "Situação Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdIpi_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdIpi.Name;
            frmIpiPes frmIpiPes = new frmIpiPes();
            frmIpiPes.Init(idipi);

            clsFormHelper.AbrirForm(this.MdiParent, frmIpiPes, clsInfo.conexaosqldados);
        }
        private void btnIdPecasClassifica_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdPecasClassifica.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA", idclassifica, "Pecas Classificacao");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void btnIdPecasClassifica1_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdPecasClassifica1.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA1", idclassifica1, "IDCLASSIFICA", idclassifica.ToString());

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void btnIdPecasClassifica2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdPecasClassifica2.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA2", idclassifica2, "IDCLASSIFICA1", idclassifica1.ToString());

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnIdHistoricobco_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdHistoricobco.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", idhistoricobco, "Historicos");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }
        private void PecasSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: PECAS
                clsPecasInfo = new clsPecasInfo();
                PecasFillInfo(clsPecasInfo);
                if (id == 0)
                {
                    clsPecasInfo.id = clsPecasBLL.Incluir(clsPecasInfo, clsInfo.conexaosqldados);
                    frmPecasVis.id = clsPecasInfo.id;
                }
                else
                {
                    clsPecasBLL.Alterar(clsPecasInfo, clsInfo.conexaosqldados);
                }
                // LISTA DE PREÇOS (PECAS PREÇO)
                foreach (DataRow row in dtPecasPreco.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idcodigo"] = clsPecasInfo.id;
                    }
                }

                foreach (DataRow row in dtPecasPreco.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsPecasPrecoBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsPecasPrecoInfo = new clsPecasPrecoInfo();
                        PecasPrecoGridToInfo(clsPecasPrecoInfo, Int32.Parse(row["posicao"].ToString()));

                        if (clsPecasPrecoInfo.id == 0)
                        {
                            clsPecasPrecoInfo.id = clsPecasPrecoBLL.Incluir(clsPecasPrecoInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsPecasPrecoBLL.Alterar(clsPecasPrecoInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
                tse.Complete();
            }
        }


        private void dgvPecasForne_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ckxEstoqueMinAtu_Click(object sender, EventArgs e)
        {
            if (ckxEstoqueMinAtu.Checked == true)
            {
                ckxEstoqueMinAtu.Text = "Sim - calcula estoque minimo automáticamente";
            }
            else
            {
                ckxEstoqueMinAtu.Text = "Não - calcula estoque minimo automáticamente";
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void tbxFatorConv_TextChanged(object sender, EventArgs e)
        {

        }

        private void tspIncluirPecasForne_Click(object sender, EventArgs e)
        {

        }

        private void tspAlterarPecasForne_Click(object sender, EventArgs e)
        {

        }

        private void tspExcluirPecasForne_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornarCompras_Click(object sender, EventArgs e)
        {
            tclPecas.SelectedIndex = 0;
        }


        private void tspPecasPrecoIncluir_Click(object sender, EventArgs e)
        {
            tclPecas.SelectedIndex = 2;

            pecaspreco_posicao = 0;
            PecasPreco_tbxData.Text = "";
            gbxPecaPrecoRegistro.Visible = true;
            PecasPrecoCarregar();
        }

        private void tspPecasPrecoAlterar_Click(object sender, EventArgs e)
        {
            if (dgvPecasPreco.CurrentRow != null)
            {
                tclPecas.SelectedIndex = 2;
                gbxPecaPrecoRegistro.Visible = true;
                pecaspreco_posicao = Int32.Parse(dgvPecasPreco.CurrentRow.Cells["posicao"].Value.ToString());
                PecasPrecoCarregar();
            }
        }

        private void dgvPecasPreco_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspPecasPrecoAlterar.PerformClick();
        }

        private void tspPecasPrecoSalvar_Click(object sender, EventArgs e)
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja salvar e alterar?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (drt == DialogResult.Yes)
            {
                clsPecasPrecoInfo = new clsPecasPrecoInfo();
                PecasPrecoFillInfo(clsPecasPrecoInfo);
                PecasPrecoFillInfoToGrid(clsPecasPrecoInfo);
            }
            else if (drt == DialogResult.Cancel)
            {
                return;
            }
            tclPecas.SelectedIndex = 0;
            gbxPecaPrecoRegistro.Visible = false;

        }

        private void tspPecasPrecoPrimeiro_Click(object sender, EventArgs e)
        {

        }

        private void tspPecasPrecoAnterior_Click(object sender, EventArgs e)
        {

        }

        private void tspPecasPrecoProximo_Click(object sender, EventArgs e)
        {

        }

        private void tspPecasPrecoUltimo_Click(object sender, EventArgs e)
        {

        }

        private void tspPecasPrecoRetornar_Click(object sender, EventArgs e)
        {
            tclPecas.SelectedIndex = 0;
            gbxPecaPrecoRegistro.Visible = false;
            PecasPreco_tbxData.Enabled = true;
            PecasPreco_tbxValorAVista.Enabled = true;
        }

        void PecasPrecoCarregar()
        {
            clsPecasPrecoInfo = new clsPecasPrecoInfo();
            clsPecasPrecoInfoOld = new clsPecasPrecoInfo();

            if (pecaspreco_posicao == 0)
            {
                pecaspreco_posicao = dtPecasPreco.Rows.Count + 1;
            }
            else
            {
                PecasPrecoGridToInfo(clsPecasPrecoInfo, pecaspreco_posicao);
            }

            PecasPrecoCampos(clsPecasPrecoInfo);
            PecasPrecoFillInfo(clsPecasPrecoInfoOld);

            PecasPreco_tbxData.Select();
        }

        void PecasPrecoGridToInfo(clsPecasPrecoInfo info, Int32 posicao)
        {
            DataRow row = dtPecasPreco.Select("posicao = " + posicao)[0];

            info.id = Int32.Parse(row["id"].ToString());
            info.idcodigo = Int32.Parse(row["idcodigo"].ToString());
            info.data = clsParser.DateTimeParse(row["data"].ToString());
            info.valoravista = Decimal.Parse(row["valoravista"].ToString());
            if (row["valoraprazo"].ToString() == "")
            {
                info.valoraprazo = 0;
                //info.valormanutencao = 0;
            }
            else
            {
                info.valoraprazo = Decimal.Parse(row["valoraprazo"].ToString());
                //info.valormanutencao = Decimal.Parse(row["valormanutencao"].ToString());
            }

        }
        void PecasPrecoCampos(clsPecasPrecoInfo info)
        {
            pecaspreco_id = info.id;
            pecaspreco_idcodigo = info.idcodigo;
            if (pecaspreco_id == 0)
            {
                PecasPreco_tbxData.Enabled = true;
                PecasPreco_tbxValorAVista.Enabled = true;
                PecasPreco_tbxValorAPrazo.Enabled = true;
                //PecasPreco_tbxValorManutencao.Enabled = true;

                if (PecasPreco_tbxData.Text == "")
                {
                    PecasPreco_tbxData.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    PecasPreco_tbxValorAVista.Text = "0";
                    PecasPreco_tbxValorAPrazo.Text = "0";
                    //PecasPreco_tbxValorManutencao.Text = "0";
                }
                PecasPreco_tbxValorAVista.Text = info.valoravista.ToString("N4");
                PecasPreco_tbxValorAPrazo.Text = info.valoraprazo.ToString("N4");
                //PecasPreco_tbxValorManutencao.Text = info.va.valormanutencao.ToString("N4");
            }
            else
            {
                PecasPreco_tbxData.Enabled = false;
                PecasPreco_tbxValorAVista.Enabled = false;
                PecasPreco_tbxValorAPrazo.Enabled = false;
                //PecasPreco_tbxValorManutencao.Enabled = false;

                PecasPreco_tbxData.Text = info.data.ToString("dd/MM/yyyy HH:mm:ss");
                PecasPreco_tbxValorAVista.Text = info.valoravista.ToString("N4");
                PecasPreco_tbxValorAPrazo.Text = info.valoraprazo.ToString("N4");
                //PecasPreco_tbxValorManutencao.Text = info/.valormanutencao.ToString("N4");

            }
        }
        void PecasPrecoFillInfo(clsPecasPrecoInfo info)
        {
            info.id = pecaspreco_id;
            info.idcodigo = pecaspreco_idcodigo;

            info.data = clsParser.DateTimeParse(PecasPreco_tbxData.Text);
            info.valoravista = Decimal.Parse(PecasPreco_tbxValorAVista.Text);
            info.valoraprazo = Decimal.Parse(PecasPreco_tbxValorAPrazo.Text);
            //info.valormanutencao = Decimal.Parse(PecasPreco_tbxValorManutencao.Text);
        }

        void PecasPrecoFillInfoToGrid(clsPecasPrecoInfo info)
        {
            DataRow row;
            DataRow[] rows = dtPecasPreco.Select("posicao = " + pecaspreco_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtPecasPreco.NewRow();
            }

            row["id"] = info.id;
            row["idcodigo"] = info.idcodigo;
            row["data"] = info.data;
            row["valoravista"] = info.valoravista;
            row["valoraprazo"] = info.valoraprazo;
            //row["valormanutencao"] = info.valormanutencao;


            if (rows.Length == 0)
            {
                row["posicao"] = pecaspreco_posicao;
                dtPecasPreco.Rows.Add(row);
                if (info.data > clsParser.DateTimeParse(tbxPecasPrecoData.Text))
                {
                    tbxPecasPrecoData.Text = info.data.ToString("dd/MM/yyyy HH:mm:ss");
                    tbxPrecoVenda.Text = info.valoravista.ToString("N4");
                }
            }
        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void lbxTipoProduto_Click(object sender, EventArgs e)
        {

        }

        private void lbxTipoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {   /* desabilitado em 08/06/2012
            if (lbxTipoProduto.Text.Substring(0, 1) == "A" || lbxTipoProduto.Text.Substring(0, 1) == "B" || lbxTipoProduto.Text.Substring(0, 1) == "N")
            {
                labelCliente.Visible = true;
                tbxCliente.Visible = true;
                btnIdCliente.Visible = true;
            }
            else
            {
                labelCliente.Visible = false;
                tbxCliente.Visible = false;
                btnIdCliente.Visible = false;
            }*/
        }

        private void tspDetalharUltimasCompras_Click(object sender, EventArgs e)
        {
            if (gbxUltimasCompras.Size.Width < 950)
            {
                gbxUltimasCompras.BackColor = Color.AntiqueWhite;
                gbxUltimasCompras.Location = new Point(10, 4);
                gbxUltimasCompras.Size = new Size(960, 410);
                //tspDetalharUltimasCompras.Image =
                tspDetalharUltimasCompras.Text = "Retornar";
                gbxDeptoFiscal.Visible = false;
                gbxGrupos.Visible = false;
                //gbxCobrarICMS.Visible = false;
                gbxPecasPreco.Visible = false;
                //gbxObservacao.Visible = false;
                gbxUltimasRemessas.Visible = false;
                //gbxAplicacoes.Visible = false;
                //gbxFornecedores.Visible = false;
                //tspPecasAplica.Visible = false;
                //tspPecasAplica1.Visible = false;
            }
            else
            {
                gbxUltimasCompras.BackColor = Color.Silver;
                gbxUltimasCompras.Location = new Point(3, 114);
                gbxUltimasCompras.Size = new Size(753, 148);
                //tspDetalharUltimasCompras.Image =
                tspDetalharUltimasCompras.Text = "Aumentar";
                gbxDeptoFiscal.Visible = true;
                gbxGrupos.Visible = true;
                //gbxCobrarICMS.Visible = true;
                gbxPecasPreco.Visible = true;
                //gbxObservacao.Visible = true;
                gbxUltimasRemessas.Visible = true;
                //gbxAplicacoes.Visible = true;
                //gbxFornecedores.Visible = true;
                //tspPecasAplica.Visible = true;
                //tspPecasAplica1.Visible = true;

            }

        }

        private void tspDetalharUltimasRemessas_Click(object sender, EventArgs e)
        {
            if (gbxUltimasRemessas.Size.Width < 950)
            {
                gbxUltimasRemessas.BackColor = Color.AntiqueWhite;
                gbxUltimasRemessas.Location = new Point(10, 4);
                gbxUltimasRemessas.Size = new Size(960, 410);
                //tspDetalharUltimasCompras.Image =
                tspDetalharUltimasCompras.Text = "Retornar";
                gbxDeptoFiscal.Visible = false;
                gbxGrupos.Visible = false;
                //gbxCobrarICMS.Visible = false;
                gbxPecasPreco.Visible = false;
                //gbxObservacao.Visible = false;
                gbxUltimasCompras.Visible = false;
                //gbxAplicacoes.Visible = false;
                //gbxFornecedores.Visible = false;
            }
            else
            {
                gbxUltimasRemessas.BackColor = Color.Silver;
                gbxUltimasRemessas.Location = new Point(3, 268);
                gbxUltimasRemessas.Size = new Size(753, 148);
                tspDetalharUltimasCompras.Text = "Aumentar";
                gbxDeptoFiscal.Visible = true;
                gbxGrupos.Visible = true;
                //gbxCobrarICMS.Visible = true;
                gbxPecasPreco.Visible = true;
                //gbxObservacao.Visible = true;
                gbxUltimasCompras.Visible = true;
                //gbxAplicacoes.Visible = true;
                //gbxFornecedores.Visible = true;
            }
        }

        private void gbxUltimasRemessas_Enter(object sender, EventArgs e)
        {

        }

        private void tbxCliente_TextChanged(object sender, EventArgs e)
        {

        }


        private void tspMovimentacaoEstoque_Click(object sender, EventArgs e)
        {
            frmMovPecas frmMovPecas = new frmMovPecas();
            frmMovPecas.Init(id, rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmMovPecas, clsInfo.conexaosqldados);

        }

        private void tspPecasFormulaRetornar_Click(object sender, EventArgs e)
        {
            tclPecas.SelectedIndex = 0;
        }

        private void gbxPecas_Enter(object sender, EventArgs e)
        {

        }

        private void tspPecasEstoqueRetornar_Click(object sender, EventArgs e)
        {
            tclPecas.SelectedIndex = 0;
        }

        private void pbxFoto_Click(object sender, EventArgs e)
        {
            this.SelecionarImagem(ref this.pbxFoto, ref this.pbxFoto);
        }
        private void SelecionarImagem(ref PictureBox pbxCatalogo, ref PictureBox pbxFoto)
        {
            try
            {
                this.ofdFoto.FileName = "";

                this.ofdFoto.Filter = "Png (*.png;)|*.png|Bitmap (*.bmp;)|*.bmp|Jpeg (*.jpg;)|*.jpg|Todos (*.*)|*.*";

                this.ofdFoto.ShowDialog();

                if (this.ofdFoto.FileName.ToString().Length > 0)
                {
                    var img = Image.FromFile(this.ofdFoto.FileName);

                    // Maior que 100 KB
                    while (clsVisual.TamanhoImagem(img) / 1024 > 100)
                    {
                        img = clsVisual.RedimensionarImagem(img, new Size((int)(img.Width - (img.Width * 0.1)), (int)(img.Height - (img.Height * 0.1))));
                    }

                    pbxCatalogo.Image = img;

                    if (pbxFoto != null)
                    {
                        int INICIO = this.ofdFoto.FileName.ToString().LastIndexOf("\\") + 1;
//                        tbxFotoCaminho.Text = this.ofdFoto.FileName.ToString().Substring(INICIO);
                        pbxFoto.Image = pbxCatalogo.Image;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nenhum arquivo selecionado.");
            }
        }

        private void ofdFoto_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnMarca.Name;
            frmEscFormVis frmEscFormVis = new frmEscFormVis();
            frmEscFormVis.Init(clsInfo.conexaosqldados, "PECASTIPO", idMarca, 30, 60, "Marca do Produto");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVis, clsInfo.conexaosqldados);

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tspPecasFormulaIncluir_Click(object sender, EventArgs e)
        {

        }

        private void gbxDeptoFiscal_Enter(object sender, EventArgs e)
        {

        }

        private void btnSemelhante_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSemelhante.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idSemelhante);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

        }

        private void btnSemelhante1_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSemelhante1.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idSemelhante1);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void btnSemelhante2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSemelhante2.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idSemelhante2);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void tbxQtdeSaldo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
