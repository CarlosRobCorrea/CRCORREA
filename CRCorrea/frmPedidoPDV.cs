using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace CRCorrea
{
    public partial class frmPedidoPDV : Form
    {
        Int32 idcliente = 0;
        Int32 idvendedor = 0;
        Int32 idcodigo = 0;
        Int32 idunidade = 0;
        Int32 idpedido = 0;
        Int32 idpedido1 = 0;
        Int32 idorcamento = 0;
        Int32 idorcamento1 = 0;
        Int32 idformapagto = clsInfo.zformapagto;
        Int32 idcondpagto = clsInfo.zcondpagto;

        Int32 pedido1_item = 0;
        Int32 pedido1_posicao = 0;
        Int32 idipi = 0;
        Double fatortributo = 30.75 / 100;

        clsPecasInfo clsPecasInfo = new clsPecasInfo();
        clsPecasBLL clsPecasBLL = new clsPecasBLL();

        DataTable dtPedido;
        clsPedidoInfo clsPedidoInfo;
        clsPedidoBLL clsPedidoBLL;

        DataTable dtPedido1;
        clsPedido1Info clsPedido1Info = new clsPedido1Info();
        clsPedido1Info clsPedido1InfoOld = new clsPedido1Info();
        clsPedido1BLL clsPedido1BLL = new clsPedido1BLL();

        DataTable dtPedidoReceber;
        clsPedidoReceberInfo clsPedidoReceberInfo = new clsPedidoReceberInfo();
        clsPedidoReceberBLL clsPedidoReceberBLL = new clsPedidoReceberBLL();

        clsOrcamentoInfo clsOrcamentoInfo = new clsOrcamentoInfo();
        clsOrcamentoBLL clsOrcamentoBLL = new clsOrcamentoBLL();

        clsOrcamento1Info clsOrcamento1Info = new clsOrcamento1Info();
        clsOrcamento1BLL clsOrcamento1BLL = new clsOrcamento1BLL();

        public frmPedidoPDV()
        {
            InitializeComponent();
        }
        public void Init()
        {
        }

        private void frmVendaPDV_Load(object sender, EventArgs e)
        {
            if (clsInfo.zusuario == "SUPERVISOR")
            {
                tbxTotalCusto.Visible = true;
                //tbxValorApurado.Visible = true;
                tbxPrecoCusto.Visible = true;
                tbxTotalCustoItem.Visible = true;
                //label17.Visible = true;
                label9.Visible = true;
                tbxTributo_Previsto.Visible = true;
            }

            // Capturar o Usuario Vendedor
            idvendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDFOLHA from USUARIO where id = " + clsInfo.zusuarioid, ""));
            tbxVendedor.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idvendedor + " ");
            dtPedidoReceber = clsPedidoReceberBLL.GridDados(0);

            tbxCPF_CNPJ.Select();
            tbxCPF_CNPJ.SelectAll();

            cbxTipoDesconto.SelectedIndex = clsVisual.SelecionarIndex("D", 1, cbxTipoDesconto);

            // carregando os itens do Pedido de Vendas
            //Pedido1 = clsPedido1BLL.GridDados(clsPedidoInfo.id, clsInfo.conexaosqldados);
            dtPedido1 = clsPedido1BLL.GridDadosPDV(0, clsInfo.conexaosqldados);
            DataColumn dcPosicao = new DataColumn("pedido1_posicao", Type.GetType("System.Int32"));
            dtPedido1.Columns.Add(dcPosicao);
            for (Int32 i = 1; i <= dtPedido1.Rows.Count; i++)
            {
                dtPedido1.Rows[i - 1]["pedido1_posicao"] = i;
            }
            dtPedido1.AcceptChanges();
            clsPedido1BLL.GridMontaPDV(dgvPedido1, dtPedido1, pedido1_posicao);
            clsGridHelper.FontGrid(dgvPedido1, 7);
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
        }

        private void frmVendaPDV_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void btnIdCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCliente.Name;
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("Todos", idcliente);
            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCliente.Name;
            frmCliente frmCliente = new frmCliente();
            frmCliente.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmCliente, clsInfo.conexaosqldados);

        }
        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1)
            {
                if (clsInfo.znomegrid == btnIdCliente.Name)
                {
                    idcliente = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxCognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxCPF_CNPJ.Text = clsInfo.zrow.Cells["CGC"].Value.ToString();

                    tbxCliente_telefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from cliente where id = " + idcliente + " ");

                    //idvendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idvendedor from cliente where id = " + idcliente + " "));
                    //tbxVendedor.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idvendedor + " ");

                    tbxCPF_CNPJ.Select();
                    tbxCPF_CNPJ.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPecas.Name)    // PRODUTO
                {
                    idcodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    // Carregar Cadastro de Pecas
                    clsPecasInfo pecasInfo = new clsPecasInfo();
                    clsPecasInfo = clsPecasBLL.Carregar(idcodigo, clsInfo.conexaosqldados);

                    tbxPecas_codigo.Text = clsPecasInfo.codigo;
                    tbxPecas_nome.Text = clsPecasInfo.nome;

                    idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDIPI FROM PECAS WHERE ID=" + idcodigo));
                    if (idipi <= 1)
                    {
                        MessageBox.Show("Produto sem NCM ou NCM com Tributação Zero - Favor Consultar !! \r\n:        Produto Excluido");
                    }
                    idunidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADE FROM PECAS WHERE ID=" + idcodigo));
                    if (idunidade == 0)
                    {
                        idunidade = clsInfo.zunidade;
                    }
                    tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where ID= " + idunidade);
                    if (clsParser.DecimalParse(tbxPreco.Text) == 0)
                    {
                        tbxPreco.Text = clsPecasInfo.precovenda.ToString("N3");
                        tbxPrecoCusto.Text = clsPecasInfo.precocompra.ToString("N4");
                        if (clsParser.DecimalParse(tbxPrecoCusto.Text) == 0 || clsParser.DecimalParse(tbxPrecoCusto.Text) > clsParser.DecimalParse(tbxPreco.Text))
                        {
                            tbxPrecoCusto.Text = ((clsParser.DecimalParse(tbxPreco.Text) * 70) / 100).ToString("N4");
                        }
                    }
                    pbxFoto.Image = clsPecasInfo.foto;
                    tbxQtdeSaldo.Text = clsPecasInfo.qtdesaldo.ToString("N2");
                    tbxPecas_codigo.Select();

                }
            }
            tbxCPF_CNPJ.Text = tbxCPF_CNPJ.Text.Replace(".", "");
            tbxCPF_CNPJ.Text = tbxCPF_CNPJ.Text.Replace("-", "");
            tbxCPF_CNPJ.Text = tbxCPF_CNPJ.Text.Replace("/", "");

            ////textos
            if (ctl.Name == tbxCPF_CNPJ.Name)
            {
                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from cliente where cgc='" + tbxCPF_CNPJ.Text + "'"));
                if (idcliente > 0)
                {
                    tbxCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idcliente + " ");
                    tbxCPF_CNPJ.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from cliente where id = " + idcliente + " ");
                    tbxCliente_telefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from cliente where id = " + idcliente + " ");
                    //idvendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idvendedor from cliente where id = " + idcliente + " "));
                    //tbxVendedor.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idvendedor + " ");
                }
                else
                {
                    idcliente = clsInfo.zempresaclienteid;
                    tbxCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id = " + idcliente + " ");
                    tbxCPF_CNPJ.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from cliente where id = " + idcliente + " ");
                    tbxCliente_telefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from cliente where id = " + idcliente + " ");
                }
            }
            else if (ctl.Name == tbxPecas_codigo.Name)
            {
                idcodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from pecas where codigo='" + tbxPecas_codigo.Text + "'"));
                if (idcodigo > 0)
                {
                    // Carregar Cadastro de Pecas
                    clsPecasInfo pecasInfo = new clsPecasInfo();
                    clsPecasInfo = clsPecasBLL.Carregar(idcodigo, clsInfo.conexaosqldados);

                    tbxPecas_codigo.Text = clsPecasInfo.codigo;
                    tbxPecas_nome.Text = clsPecasInfo.nome;
                    idunidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADE FROM PECAS WHERE ID=" + idcodigo));
                    if (idunidade == 0)
                    {
                        idunidade = clsInfo.zunidade;
                    }
                    tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where ID= " + idunidade);

                    idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDIPI FROM PECAS WHERE ID=" + idcodigo));
                    if (idipi <= 1)
                    {
                        MessageBox.Show("Produto sem NCM ou NCM com Tributação Zero - Favor Consultar !! \r\n:        Produto Excluido");
                    }

                    if (tbxPecas_codigo.Text != "0")
                    {
                        if (clsParser.DecimalParse(tbxPreco.Text) == 0)
                        {
                            tbxPreco.Text = clsPecasInfo.precovenda.ToString("N3");
                        }
                        pbxFoto.Image = clsPecasInfo.foto;
                        tbxQtdeSaldo.Text = clsPecasInfo.qtdesaldo.ToString("N2");
                            if (clsParser.DecimalParse(tbxPreco.Text) == 0)
                            {
                                MessageBox.Show("Preço de Venda Zerado - Favor Consultar !! \r\n:        Produto Excluido");
                                cbxTipoDesconto.SelectedIndex = clsVisual.SelecionarIndex("D", 1, cbxTipoDesconto);
                                idcodigo = 0;
                                tbxPecas_codigo.Text = "";
                                tbxPecas_nome.Text = "";
                                tbxQtde.Text = "0,00";
                                tbxPreco.Text = "0,0000";
                                tbxPosicao.Text = "";
                                tbxQtdeSaldo.Text = "";
                                tbxTotalmercado.Text = "";
                                tbxTotalnota.Text = "";
                                tbxTotalDesconto.Text = "";
                                tbxTributo_Previsto.Text = "";
                            pbxFoto.Image = null;
                                tbxPecas_codigo.Select();
                                tbxPecas_codigo.SelectAll();
                            }
                            else if (clsParser.DecimalParse(tbxQtde.Text) == 0)
                            {
                                tbxQtde.Text = "1,00";
                                tbxQtde.Select();
                                tbxQtde.SelectAll();
                            }
                    }
                    else
                    {
                        // liberar a descrição e o preço
                        tbxPecas_nome.ReadOnly = false;
                        tbxPecas_nome.Enabled = true;
                        tbxPreco.ReadOnly = false;
                        tbxPreco.Enabled = true;
                        tbxQtde.Text = "1,00";
                        tbxQtde.Select();
                        tbxQtde.SelectAll();


                    }
                }
                else
                {
                    if (clsParser.DecimalParse(tbxTotalMercadoriaSoma.Text) > 0)
                    {
                        tbxTotalDesconto.Select();
                        tbxTotalDesconto.SelectAll();

                    }
                }

            }
            tbxQtdeParcela.Text = clsParser.Int32Parse(tbxQtdeParcela.Text).ToString("N0");
            if (clsParser.Int32Parse(tbxQtdeParcela.Text) > 4)
            {
                MessageBox.Show("Qtde de Parcelas no Maximo 4");
            }
            tbxQtde.Text = clsParser.DecimalParse(tbxQtde.Text).ToString("N2");
            tbxPreco.Text = clsParser.DecimalParse(tbxPreco.Text).ToString("N2");

            tbxTotalmercado.Text = (clsParser.DecimalParse(tbxQtde.Text) * clsParser.DecimalParse(tbxPreco.Text)).ToString("N2");
            tbxTotalnota.Text = tbxTotalmercado.Text;
            tbxTributo_Previsto.Text = (clsParser.DoubleParse(tbxTotalnota.Text) * fatortributo).ToString("N3");
            tbxTotalCustoItem.Text = (clsParser.DecimalParse(tbxQtde.Text) * clsParser.DecimalParse(tbxPrecoCusto.Text)).ToString("N2");

            if (ctl.Name == tbxQtde.Name)
            {
                if (clsParser.Int32Parse(tbxPosicao.Text) == 0)
                {
                    if (tbxPecas_codigo.Text != "0")
                    {
                        // Se estou saindo do campo tbxQtde, INICIAR A GRAVAÇÃO
                        pedido1_item = 0;
                        foreach (DataGridViewRow linha in dgvPedido1.Rows)
                        {
                            pedido1_item = clsParser.Int32Parse(linha.Cells["NITEM"].Value.ToString());
                        }
                        pedido1_item += 1;

                        pedido1_posicao = 0;
                        Pedido1Carregar();

                        cbxTipoDesconto.SelectedIndex = clsVisual.SelecionarIndex("D", 1, cbxTipoDesconto);
                        tbxPecas_codigo.Text = "";
                        tbxPecas_nome.Text = "";
                        tbxQtde.Text = "0,00";
                        tbxPreco.Text = "0,0000";
                        tbxPrecoCusto.Text = "0,0000";
                        tbxPosicao.Text = "";
                        tbxQtdeSaldo.Text = "";
                        tbxTotalmercado.Text = "";
                        tbxTotalCustoItem.Text = "";
                        tbxTotalnota.Text = "";
                        tbxTotalDesconto.Text = "";
                        tbxTributo_Previsto.Text = "";
                        pbxFoto.Image = null;
                    }
                    else
                    {

                    }
                }
                else
                {
                    gbxCodigo.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
                    Pedido1Salvar();
                }
            }
            if (ctl.Name == tbxPreco.Name)
            {
                if (clsParser.Int32Parse(tbxPosicao.Text) == 0)
                {
                    if (tbxPecas_codigo.Text == "0")
                    {
                        // Se estou saindo do campo tbxQtde, INICIAR A GRAVAÇÃO
                        pedido1_item = 0;
                        foreach (DataGridViewRow linha in dgvPedido1.Rows)
                        {
                            pedido1_item = clsParser.Int32Parse(linha.Cells["NITEM"].Value.ToString());
                        }
                        pedido1_item += 1;

                        pedido1_posicao = 0;
                        Pedido1Carregar();

                        cbxTipoDesconto.SelectedIndex = clsVisual.SelecionarIndex("D", 1, cbxTipoDesconto);
                        tbxPecas_codigo.Text = "";
                        tbxPecas_nome.Text = "";
                        tbxQtde.Text = "0,00";
                        tbxPreco.Text = "0,0000";
                        tbxPosicao.Text = "";
                        tbxQtdeSaldo.Text = "";
                        tbxTotalmercado.Text = "";
                        tbxTotalnota.Text = "";
                        tbxTotalDesconto.Text = "";
                        pbxFoto.Image = null;

                        tbxPecas_nome.ReadOnly = true;
                        tbxPecas_nome.Enabled = false;
                        tbxPreco.ReadOnly = true;
                        tbxPreco.Enabled = false;

                    }
                    else
                    {

                    }
                }
                else
                {
                    gbxCodigo.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
                    Pedido1Salvar();
                }
            }


            PedidoCalcular();

            if (idcliente != clsInfo.zempresaclienteid)
            {
                rbnBoleto.Visible = true;
                tbxQtdeParcela.Visible = true;
            }
            else
            {
                rbnBoleto.Visible = false;
                tbxQtdeParcela.Visible = false;
            }

            clsInfo.znomegrid = "";
            clsInfo.zrow = null;

        }

        private void Pedido1Carregar()
        {
            clsPedido1Info = new clsPedido1Info();
            clsPedido1InfoOld = new clsPedido1Info();

            if (pedido1_posicao == 0)
            {
                pedido1_posicao = dtPedido1.Rows.Count + 1;
                clsPedido1Info.nitem = pedido1_item;
                clsPedido1Info.idcodigo = idcodigo;
                clsPedido1Info.idipi = idipi;
                clsPedido1Info.idunidade = idunidade;
                clsPedido1Info.complemento = tbxPecas_nome.Text;
                clsPedido1Info.preco = clsParser.DecimalParse(tbxPreco.Text);
                clsPedido1Info.precocusto = clsParser.DecimalParse(tbxPrecoCusto.Text);
                clsPedido1Info.qtde = clsParser.DecimalParse(tbxQtde.Text);
                clsPedido1Info.totalmercado = clsParser.DecimalParse(tbxTotalmercado.Text);
                clsPedido1Info.totalcustoitem = clsParser.DecimalParse(tbxTotalCustoItem.Text);
                clsPedido1Info.totalnota = clsParser.DecimalParse(tbxTotalnota.Text);
                clsPedido1Info.tributo_previsto = clsParser.DecimalParse(tbxTributo_Previsto.Text);

            }
            else
            {
                clsPedido1Info.nitem = pedido1_item;
                clsPedido1Info.idcodigo = idcodigo;
                clsPedido1Info.idipi = idipi;
                clsPedido1Info.idunidade = idunidade;
                clsPedido1Info.complemento = tbxPecas_nome.Text;
                clsPedido1Info.preco = clsParser.DecimalParse(tbxPreco.Text);
                clsPedido1Info.precocusto = clsParser.DecimalParse(tbxPrecoCusto.Text);
                clsPedido1Info.qtde = clsParser.DecimalParse(tbxQtde.Text);
                clsPedido1Info.totalmercado = clsParser.DecimalParse(tbxTotalmercado.Text);
                clsPedido1Info.totalcustoitem = clsParser.DecimalParse(tbxTotalCustoItem.Text);
                clsPedido1Info.totalnota = clsParser.DecimalParse(tbxTotalnota.Text);
                clsPedido1Info.tributo_previsto = clsParser.DecimalParse(tbxTributo_Previsto.Text);

                Pedido1GridToInfo(clsPedido1Info, pedido1_posicao);
            }

            Pedido1Campos(clsPedido1Info);
            Pedido1FillInfo(clsPedido1InfoOld);
            Pedido1FillInfoToGrid(clsPedido1Info);

        }
        void Pedido1Campos(clsPedido1Info info)
        {
            idpedido1 = info.id;
            idcodigo = info.idcodigo;
            idipi = info.idipi;
            idunidade = info.idunidade;
            tbxPosicao.Text = info.nitem.ToString();
            tbxPreco.Text = info.preco.ToString("N4");
            tbxPrecoCusto.Text = info.precocusto.ToString("N4");
            tbxQtde.Text = info.qtde.ToString("N2");
            tbxTotalmercado.Text = info.totalmercado.ToString("N2");
            tbxTotalCustoItem.Text = info.totalcustoitem.ToString("N2");
            tbxTotalnota.Text = info.totalnota.ToString("N2");
            tbxTributo_Previsto.Text = info.tributo_previsto.ToString("N3");
            clsPecasInfo = clsPecasBLL.Carregar(idcodigo, clsInfo.conexaosqldados);
            pbxFoto.Image = clsPecasInfo.foto;
            tbxPecas_codigo.Text = clsPecasInfo.codigo;
            //tbxPecas_nome.Text = clsPecasInfo.nome;
            tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where ID= " + idunidade);
        }

        void Pedido1FillInfo(clsPedido1Info info)
        {
            info.id = idpedido1;
            info.baseicm = 0;
            info.baseicmsubst = 0;
            info.basemp = 0;
            info.calculoautomatico = "S";
            info.codigoemp01 = "";
            info.codigoemp02 = "";
            info.codigoemp03 = "";
            info.codigoemp04 = "";
            info.cofins1 = 0;
            info.complemento = tbxPecas_nome.Text;
            info.consumo = "S";
            info.custoicm = 0;
            info.custoipi = 0;
            info.icm = 0;
            info.icmsubst = 0;
            info.idcfop = 0;
            info.idcodigo = idcodigo;
            info.idipi = idipi;
            info.idorcamento = 0;
            info.idorcamentoitem = 0;
            info.idordemservico = 0;
            info.idpedido = idpedido;
            info.idsittriba = 0;
            info.idsittribb = 0;
            info.idsittribcofins = 0;
            info.idsittribipi = 0;
            info.idsittribpispasep = 0;

            info.idcentrocusto = 0;
            info.idcontacontabil = 0;

            info.idtiponota = 0;
            info.idunidade = idunidade;
            info.ipi = 0;
            info.nitem = 0;
            info.peso = 0;
            info.pispasep = 0;
            info.preco = clsParser.DecimalParse(tbxPreco.Text);
            info.precocusto = clsParser.DecimalParse(tbxPrecoCusto.Text);
            info.precodesconto = 0;
            info.precotabela = clsParser.DecimalParse(tbxPreco.Text);
            info.qtde = clsParser.DecimalParse(tbxQtde.Text);
            info.qtdebaixada = 0;
            info.qtdedefeito = 0;
            info.qtdeentregue = 0;
            info.qtdeosaux = 0;
            info.qtdesaldo = 0;
            info.qtdesucata = 0;
            info.totalmercado = clsParser.DecimalParse(tbxTotalmercado.Text);
            info.totalnota = clsParser.DecimalParse(tbxTotalnota.Text);
            info.tributo_previsto = clsParser.DecimalParse(tbxTributo_Previsto.Text);
            info.totalcustoitem = clsParser.DecimalParse(tbxTotalCustoItem.Text);
            info.totalpeso = 0;
            info.totalprevisto = clsParser.DecimalParse(tbxTotalnota.Text);
            info.valorfrete = clsParser.DecimalParse("0".ToString());
            info.valorseguro = clsParser.DecimalParse("0".ToString());

        }

        void Pedido1GridToInfo(clsPedido1Info info, Int32 posicao)
        {
            DataRow row = dtPedido1.Select("pedido1_posicao = " + posicao)[0];

            info.id = Int32.Parse(row["id"].ToString());

            //info.baseicm = clsParser.DecimalParse(row["baseicm"].ToString());
            //info.baseicmsubst = clsParser.DecimalParse(row["baseicmsubst"].ToString());
            //info.basemp = clsParser.DecimalParse(row["basemp"].ToString());
            //info.calculoautomatico = row["calculoautomatico"].ToString();
            //info.codigoemp01 = row["codigoemp01"].ToString();
            //info.codigoemp02 = row["codigoemp02"].ToString();
            //info.codigoemp03 = row["codigoemp03"].ToString();
            //info.codigoemp04 = row["codigoemp04"].ToString();
            //info.cofins1 = clsParser.DecimalParse(row["cofins1"].ToString());
            info.complemento = row["complemento"].ToString();
            //info.consumo = row["consumo"].ToString();
            //info.custoicm = clsParser.DecimalParse(row["custoicm"].ToString());
            //info.custoipi = clsParser.DecimalParse(row["custoipi"].ToString());
            //info.icm = clsParser.DecimalParse(row["icm"].ToString());
            //info.icmsubst = clsParser.DecimalParse(row["icmsubst"].ToString());
            //info.idcfop = clsParser.Int32Parse(row["idcfop"].ToString());
            info.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
            info.idipi = clsParser.Int32Parse(row["idipi"].ToString());
            //info.idorcamento = clsParser.Int32Parse(row["idorcamento"].ToString());
            //info.idorcamentoitem = clsParser.Int32Parse(row["idorcamentoitem"].ToString());
            //info.idordemservico = clsParser.Int32Parse(row["idordemservico"].ToString());
            info.idpedido = clsParser.Int32Parse(row["idpedido"].ToString());
            //info.idsittriba = clsParser.Int32Parse(row["idsittriba"].ToString());
            //info.idsittribb = clsParser.Int32Parse(row["idsittribb"].ToString());
            //info.idsittribcofins = clsParser.Int32Parse(row["idsittribcofins"].ToString());
            //info.idsittribipi = clsParser.Int32Parse(row["idsittribipi"].ToString());
            //info.idsittribpispasep = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCSTPIS from tiponota where id= " + clsPedido1Info.idtiponota + ""));
            //info.idsittribpispasep = clsParser.Int32Parse(row["idsittribpispasep"].ToString());

            //info.idtiponota = clsParser.Int32Parse(row["idtiponota"].ToString());
            info.idunidade = clsParser.Int32Parse(row["idunidade"].ToString());
            //info.ipi = clsParser.DecimalParse(row["ipi"].ToString());
            info.nitem = clsParser.Int32Parse(row["nitem"].ToString());

            //info.idcentrocusto = clsParser.Int32Parse(row["idcentrocusto"].ToString());
            //info.idcontacontabil = clsParser.Int32Parse(row["idcontacontabil"].ToString());

            info.peso = clsParser.DecimalParse(row["peso"].ToString());
            //info.pispasep = clsParser.DecimalParse(row["pispasep"].ToString());
            info.preco = clsParser.DecimalParse(row["preco"].ToString());
            info.precocusto = clsParser.DecimalParse(row["precocusto"].ToString());
            //info.precodesconto = clsParser.DecimalParse(row["precodesconto"].ToString());
            info.precotabela = clsParser.DecimalParse(row["preco"].ToString());
            info.qtde = clsParser.DecimalParse(row["qtde"].ToString());
            //info.qtdebaixada = clsParser.DecimalParse(row["qtdebaixada"].ToString());
            //info.qtdedefeito = clsParser.DecimalParse(row["qtdedefeito"].ToString());
            //info.qtdeentregue = clsParser.DecimalParse(row["qtdeentregue"].ToString());
            //info.qtdeosaux = clsParser.DecimalParse(row["qtdeosaux"].ToString());
            info.qtdesaldo = clsParser.DecimalParse(row["qtde"].ToString());
            //info.qtdesucata = clsParser.DecimalParse(row["qtdesucata"].ToString());
            info.totalmercado = clsParser.DecimalParse(row["totalmercado"].ToString());
            info.totalnota = clsParser.DecimalParse(row["totalnota"].ToString());
            info.tributo_previsto = clsParser.DecimalParse(row["tributo_previsto"].ToString());
            info.totalpeso = clsParser.DecimalParse(row["totalpeso"].ToString());
            info.totalcustoitem = clsParser.DecimalParse(row["totalcustoitem"].ToString());
            //info.totalprevisto = clsParser.DecimalParse(row["totalprevisto"].ToString());
            //info.valorfrete = clsParser.DecimalParse(row["valorfrete"].ToString());
            //            info.valorfreteicms = row["valorfreteicms"].ToString();
            //info.valoroutras = clsParser.DecimalParse(row["valoroutras"].ToString());
            //            info.valoroutrasicms = row["valoroutrasicms"].ToString();
            //info.valorseguro = clsParser.DecimalParse(row["valorseguro"].ToString());
            //            info.valorseguroicms = row["valorseguroicms"].ToString();


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

            row["id"] = idpedido1;
            row["nitem"] = pedido1_posicao;
            row["idcodigo"] = idcodigo;
            row["idipi"] = idipi;
            row["idpedido"] = idpedido;
            row["idunidade"] = idunidade;
            row["peso"] = clsParser.DecimalParse("0");
            row["preco"] = clsParser.DecimalParse(tbxPreco.Text);
            row["precocusto"] = clsParser.DecimalParse(tbxPrecoCusto.Text);
            row["precotabela"] = clsParser.DecimalParse(tbxPreco.Text);
            row["qtde"] = clsParser.DecimalParse(tbxQtde.Text);
            row["totalmercado"] = clsParser.DecimalParse(tbxTotalmercado.Text);
            row["totalcustoitem"] = clsParser.DecimalParse(tbxTotalCustoItem.Text);

            row["totalnota"] = clsParser.DecimalParse(tbxTotalnota.Text);
            row["tributo_previsto"] = clsParser.DecimalParse(tbxTributo_Previsto.Text);

            // dados externos
            row["CODIGO"] = tbxPecas_codigo.Text;
            row["DESCRICAO"] = tbxPecas_nome.Text;
            if (tbxPecas_codigo.Text == "0")
            {
                row["COMPLEMENTO"] = tbxPecas_nome.Text;
            }
            row["UNID"] = tbxUnidade.Text;
            if (rows.Length == 0)
            {
                row["pedido1_posicao"] = pedido1_posicao;
                row["nitem"] = pedido1_posicao;
                dtPedido1.Rows.Add(row);
            }
            dgvPedido1.Refresh();
        }
        private void PedidoCalcular()
        {
            tbxQtdeTotal.Text = "0";
            tbxTotalMercadoriaSoma.Text = "0";
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


                    tbxQtdeTotal.Text = (clsParser.DecimalParse(tbxQtdeTotal.Text) + clsParser.DecimalParse(row["qtde"].ToString())).ToString("N2");
                    tbxTotalMercadoriaSoma.Text = (clsParser.DecimalParse(tbxTotalMercadoriaSoma.Text) + clsParser.DecimalParse(row["totalnota"].ToString())).ToString("N2");
                    tbxTotalCusto.Text = (clsParser.DecimalParse(tbxTotalCusto.Text) + clsParser.DecimalParse(row["totalcustoitem"].ToString())).ToString("N2");

                }
            }
            tbxQtdeTotal.Text = clsParser.DecimalParse(tbxQtdeTotal.Text).ToString("N2");
            tbxTotalMercadoriaSoma.Text = clsParser.DecimalParse(tbxTotalMercadoriaSoma.Text).ToString("N2");
            tbxTotalDesconto.Text = clsParser.DecimalParse(tbxTotalDesconto.Text).ToString("N2");
            if (cbxTipoDesconto.Text == "Acrescimo")
            {
                tbxTotalPedido.Text = (clsParser.DecimalParse(tbxTotalMercadoriaSoma.Text) + clsParser.DecimalParse(tbxTotalDesconto.Text)).ToString("N2");
            }
            else
            {
                tbxTotalPedido.Text = (clsParser.DecimalParse(tbxTotalMercadoriaSoma.Text) - clsParser.DecimalParse(tbxTotalDesconto.Text)).ToString("N2");
            }
            tbxTotalCusto.Text = clsParser.DecimalParse(tbxTotalCusto.Text).ToString("N2");
        }
        private void Pedido1Salvar()
        {
            clsPedido1Info = new clsPedido1Info();
            Pedido1FillInfo(clsPedido1Info);
            clsPedido1BLL clsPedido1BLL = new clsPedido1BLL();
            //clsPedido1BLL.VerificaInfo(clsPedido1Info);
            Pedido1FillInfoToGrid(clsPedido1Info);

            tbxPecas_codigo.Text = "";
            tbxPecas_nome.Text = "";
            tbxQtde.Text = "0,00";
            tbxPreco.Text = "0,0000";
            tbxPrecoCusto.Text = "0,0000";
            tbxPosicao.Text = "";
            tbxQtdeSaldo.Text = "";
            tbxTotalmercado.Text = "";
            tbxTotalCustoItem.Text = "";
            tbxTotalnota.Text = "";
            tbxTributo_Previsto.Text = "";
            pbxFoto.Image = null;
        }

        private void btnPecas_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPecas.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
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

        private void tspPedido1Retornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPedido1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tspPedido1Alterar.Enabled == true)
            {
                tspPedido1Alterar.PerformClick();
            }
            else
            {
                pedido1_posicao = Int32.Parse(dgvPedido1.CurrentRow.Cells["pedido1_posicao"].Value.ToString());
                Pedido1Carregar();
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
                pedido1_posicao = Int32.Parse(dgvPedido1.CurrentRow.Cells["pedido1_posicao"].Value.ToString());
                Pedido1Carregar();
            }

        }

        private void tspPedido1Alterar_Click(object sender, EventArgs e)
        {
            if (dgvPedido1.CurrentRow != null)
            {

                pedido1_posicao = Int32.Parse(dgvPedido1.CurrentRow.Cells["pedido1_posicao"].Value.ToString());
                Pedido1Carregar();
                gbxCodigo.BackColor = Color.Yellow;

                tbxPecas_codigo.Select();
                tbxPecas_codigo.SelectAll();
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
        private void tspPedido1Salvar_Click(object sender, EventArgs e)
        {
            // Salvar Pedido
            PedidoSalvar();
        }

        private void tbxPosicao_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxTotalnota_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxTotalmercado_TextChanged(object sender, EventArgs e)
        {

        }

        private void gbxCliente_Enter(object sender, EventArgs e)
        {

        }

        private void tspPedido1Proximo_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    OrcamentoSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void PedidoSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: PEDIDO
                clsPedidoBLL clsPedidoBLL = new clsPedidoBLL();
                clsPedidoInfo clsPedidoInfo = new clsPedidoInfo();
                clsPedido1Info clsPedido1Info = new clsPedido1Info();
                clsPedido1BLL clsPedido1BLL = new clsPedido1BLL();
                clsPedidoReceberInfo clsPedidoReceberInfo1 = new clsPedidoReceberInfo();
                clsPedidoReceberBLL clsPedidoReceberBLL1 = new clsPedidoReceberBLL();
                PedidoFillInfo(clsPedidoInfo);

                if (idpedido == 0)
                {
                    clsPedidoInfo.id = clsPedidoBLL.Incluir(clsPedidoInfo, clsInfo.conexaosqldados);
                    idpedido = clsPedidoInfo.id;
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
                clsPedido1BLL = new clsPedido1BLL();
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
                        clsPedido1BLL = new clsPedido1BLL();
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
                // Verifica se não é parcelado
                if (rbnBoleto.Checked == false)
                { // Condição de pagamento no dia
                    idcondpagto = clsInfo.zformapagto;
                }
                else
                {
                    if (clsParser.Int32Parse(tbxQtdeParcela.Text) == 2)
                    {
                        // pegar a condição de pagamento' 0/30
                        idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from condpagto where codigo = '02X_0/30' "));
                    }
                    else if (clsParser.Int32Parse(tbxQtdeParcela.Text) == 3)
                    {
                        // pegar a condição de pagamento' 0/30/60
                        idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from condpagto where codigo = '03x_0/30/60' "));
                    }
                    else if (clsParser.Int32Parse(tbxQtdeParcela.Text) == 4)
                    {
                        // pegar a condição de pagamento' 0/30/60/120
                        idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from condpagto where codigo = '04X_0/30/60/90' "));
                    }
                    else
                    {
                        idcondpagto = clsInfo.zcondpagto;
                    }
                }
                PedidoReceberCalcularPagamentos();

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
                // Emitir NFCE
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
                    nome_credenciadora = null,
                    bandeira_operadora = null,
                    numero_autorizacao = null
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
                }

                String referenciaNfce = "PED" + clsPedidoInfo.ano + clsPedidoInfo.numero.ToString().PadLeft(5, '0');
                FocusNFeResponse respostaFocus = APIs.EmitirNFCe(strJson, referenciaNfce);

                if (respostaFocus.status == "erro" ||
                    respostaFocus.status == "erro_autorizacao" ||
                    String.IsNullOrEmpty(respostaFocus.status))
                {
                    String msgErro = "Retorno da API Focus NFe:\n\n" + (respostaFocus.resposta_raw ?? respostaFocus.mensagem ?? "Sem resposta");
                    MessageBox.Show(msgErro, "NFC-e", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    String msgNfce = "NFC-e enviada!" +
                        "\nStatus: " + (respostaFocus.status ?? "") +
                        "\nStatus SEFAZ: " + (respostaFocus.status_sefaz ?? "") +
                        "\nMensagem SEFAZ: " + (respostaFocus.mensagem_sefaz ?? "");

                    if (!String.IsNullOrEmpty(respostaFocus.chave_nfe))
                        msgNfce += "\nChave: " + respostaFocus.chave_nfe;

                    msgNfce += "\n\nResposta completa:\n" + (respostaFocus.resposta_raw ?? "");
                    MessageBox.Show(msgNfce, "NFC-e", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                tse.Complete();
            }

            DialogResult resultado1;
            resultado1 = MessageBox.Show("Deseja Imprimir Pedido ?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (resultado1 == DialogResult.Yes)
            {
                frmCrystalReport frmCrystalReport;
                frmCrystalReport = new frmCrystalReport();
                ParameterFields parameters = new ParameterFields();
                // Cabeçalho
                ParameterDiscreteValue valor = new ParameterDiscreteValue();
                ParameterField field = new ParameterField();

                valor.Value = idpedido;
                field.Name = "id";
                field.CurrentValues.Add(valor);
                parameters.Add(field);

                // Fazer um loop para ver os valores e vencimentos
                String CondPagto = "";
                CondPagto = "1- " + clsPedidoReceberInfo.data.ToString("dd/MM/yy") + "     R$=" + clsPedidoReceberInfo.valor.ToString("N2") + "      ";

                field = new ParameterField();
                field.Name = "condpagto";
                valor = new ParameterDiscreteValue();
                valor.Value = CondPagto;
                field.CurrentValues.Add(valor);
                parameters.Add(field);

                frmCrystalReport.Init(clsInfo.caminhorelatorios, "PEDIDO_PDV.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                ApagarTelaPrincipal();
            }
            else if (resultado1 == DialogResult.Cancel)
            {
                ApagarTelaPrincipal();

            }
            // Apagar a Tela principal
            ApagarTelaPrincipal();
    
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


            // Como tem desconto ele não soma os itens
            totalcobranca = clsParser.DecimalParse(tbxTotalPedido.Text);

            // Soma os valores a serem cobrados
            foreach (DataRow row in dtPedido1.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    row.RowState != DataRowState.Detached)
                {
                    //if (row["fatura"].ToString() == "S")
                    //{
                    // totalcobranca += clsParser.DecimalParse(row["totalnota"].ToString());
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

            /*
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
            */
            dtPedidoReceber = clsFinanceiro.GerarFatura(DateTime.Now,
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
                    dtPedidoReceber.Rows[x]["IDNOTA"] = idpedido;
                    dtPedidoReceber.Rows[x]["POSICAOREC"] = posicaorec;
                    dtPedidoReceber.Rows[x]["IDTIPOPAGA"] = idformapagto;
                    posicaorec++;
                }
            }

            dgvPagamentos.DataSource = dtPedidoReceber;

            clsGridHelper.MontaGrid2(dgvPagamentos, clsPedidoReceberBLL.dtGridColunas, true);

            dgvPagamentos.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPagamentos.Columns["VALOR"].DefaultCellStyle.Format = "N2";

            //SomarPagamentos();

            //}
        }
        private void PedidoReceberGridToInfo(DataTable dt, clsPedidoReceberInfo info, Int32 posicao)
        {
            DataRow row = dt.Select("posicaorec = " + posicao)[0];

            info.boletonro = clsParser.DecimalParse(row["boletonro"].ToString());
            info.data = DateTime.Parse(row["data"].ToString());
            info.dv = row["dv"].ToString();
            info.id = clsParser.Int32Parse(row["id"].ToString());
            info.idnota = clsParser.Int32Parse(row["idnota"].ToString());
            info.idtipopaga = idformapagto; //clsParser.Int32Parse(row["idtipopaga"].ToString());
            info.pagou = row["pagou"].ToString();
            info.posicao = clsParser.Int32Parse(row["posicao"].ToString());
            info.posicaofim = clsParser.Int32Parse(row["posicaofim"].ToString());
            info.valor = clsParser.DecimalParse(row["valor"].ToString());
            //info.tipopaga = row["tipopaga"].ToString();
            info.tipopaga = row["tipopaga"].ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void tbxQtdeTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void gbxItens_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_2(object sender, EventArgs e)
        {

        }
        private void PedidoFillInfo(clsPedidoInfo info)
        {
            info.id = 0;
            if (idcliente == 0)
            {
                idcliente = clsInfo.zempresaclienteid;
            }
            info.idcliente = idcliente;
            info.idtransportadora = idcliente;
            info.idredespacho = idcliente;
            info.idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from condpagto where codigo = '0' "));
            if (rbnPix.Checked == true)
            { info.idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where codigo = 'PI' ")); }
            else if (rbnDinheiro.Checked == true)
            { info.idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where codigo = 'DI' ")); }
            else if (rbnCartao.Checked == true)
            { info.idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where codigo = 'CA' ")); }
            else if (rbnCartaoCredito.Checked == true)
            { info.idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where codigo = 'CC' ")); }
            else
            {
                info.idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where codigo = 'BO' "));
            }
            idformapagto = info.idformapagto;
            info.idvendedor = idvendedor;
            info.filial = clsInfo.zfilial;
            info.numero = 0;
            info.data = DateTime.Now;
            info.ano = DateTime.Now.Year;
            info.comissaorepresentante = 0;
            info.emitente = clsInfo.zusuario;
            info.situacao = "1";
            info.frete = "F"; // NÃO INCLUSO
            info.fretepaga = "1"; //PAGA PELO DESTINATÁRIO
            info.tipofrete = "1"; // TRATADO
            info.transporte = "N"; // NOSSO CARRO
            info.freteqtde = 0;
            info.freteunid = "";
            info.freteprecouni = 0;
            info.fretetotal = 0;
            info.fretebaseicms = 0;
            info.freteicmaliq = 0;
            info.fretevaloricms = 0;
            info.observa = "";
            info.pago_ctareceber = "";
            info.tipodesconto = cbxTipoDesconto.Text.Substring(0, 1);
            info.totalcusto = clsParser.DecimalParse(tbxTotalCusto.Text); ;
            info.totalipi = 0;
            info.totalfrete = 0;
            info.totalseguro = 0;
            info.totaloutras = 0;
            info.totaldesconto = clsParser.DecimalParse(tbxTotalDesconto.Text);
            info.totalmercadoria = clsParser.DecimalParse(tbxTotalMercadoriaSoma.Text);
            info.totalpedido = clsParser.DecimalParse(tbxTotalPedido.Text);
            info.totalpispasep = 0;
            info.totalcofins1 = 0;
            info.totalpeso = 0;
            // tbxTotalpesob ????
            info.totalbaseicm = 0;
            info.totalicm = 0;
            info.totalbaseicmsubst = 0;
            info.totalicmsubst = 0;
            info.qtdepedido = clsParser.DecimalParse(tbxQtdeTotal.Text);
            info.qtdeentregue = 0;
            info.qtdedefeito = 0;
            info.qtdesucata = 0;
            info.qtdebaixada = 0;
            info.qtdeosaux = 0;
            info.qtdesaldo = clsParser.DecimalParse(tbxQtdeTotal.Text);
        }
        private void OrcamentoSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {

                // ###############################
                // Tabela: ORCAMENTO
                clsOrcamentoInfo = new clsOrcamentoInfo();
                OrcamentoFillInfo(clsOrcamentoInfo);
                clsOrcamentoInfo.id = clsOrcamentoBLL.Incluir(clsOrcamentoInfo, clsInfo.conexaosqldados);
                idorcamento = clsOrcamentoInfo.id;

                //// Ver os itens do Pedido1       
                foreach (DataRow row in dtPedido1.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        MessageBox.Show("Não é possível excluir itens do pedido, somente do orçamento.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //clsOrcamento1BLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsOrcamento1Info = new clsOrcamento1Info();
                        Orcamento1FillGridToInfo(clsOrcamento1Info, clsParser.Int32Parse(row["pedido1_posicao"].ToString()));

                        clsOrcamento1Info.id = clsOrcamento1BLL.Incluir(clsOrcamento1Info, clsInfo.conexaosqldados);
                        idorcamento1 = clsOrcamento1Info.id;
                    }
                }
                // ###############################
                tse.Complete();
            }
            DialogResult resultado1;
            resultado1 = MessageBox.Show("Deseja Imprimir Orçamento ?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (resultado1 == DialogResult.Yes)
            {
                frmCrystalReport frmCrystalReport;
                frmCrystalReport = new frmCrystalReport();
                ParameterFields parameters = new ParameterFields();
                // Cabeçalho
                ParameterDiscreteValue valor = new ParameterDiscreteValue();
                ParameterField field = new ParameterField();

                valor.Value = idorcamento;
                field.Name = "id";
                field.CurrentValues.Add(valor);
                parameters.Add(field);

                frmCrystalReport.Init(clsInfo.caminhorelatorios, "ORCAMENTO_FOTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
            else if (resultado1 == DialogResult.Cancel)
            {

            }
            // Apagar a Tela principal
            ApagarTelaPrincipal();

        }
        void OrcamentoFillInfo(clsOrcamentoInfo info)
        {
            info.id = 0;
            info.atencao = tbxCognome.Text;
            //info.cognome = tbxCognome.Text;
            info.data = DateTime.Now;
            info.ddd = "";
            info.email = "";
            //info.emitente =
            info.filial = 1;
            if (idcliente == 0)
            {
                idcliente = clsInfo.zempresaclienteid;
            }

            info.idcliente = idcliente;
            info.idcondpag = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from condpagto where codigo = '0' "));
            //info.idresponsavel =
            info.idvendedor = idvendedor;
            //info.motivo =
            info.numero = 0;
            //info.observa =
            //info.observa1 =
            //info.observa2 =
            //info.observa3=
            //info.observa4 =
            //info.observaa =
            //            info.pordesc = clsParser.DecimalParse(tbxPorDesc.Text);
            info.qtdeconfirmada = 0;
            info.qtdeorcada = clsParser.DecimalParse(tbxQtdeTotal.Text);
            //info.qtdeorcadanao;
            //info.referencia =
            //info.sedex =
            info.setor = "S";
            info.situacao = "";
            info.telefone = tbxCliente_telefone.Text;
            info.totalconfirmado = 0;
            //            info.totaldesconto = clsParser.DecimalParse(tbxTotalOrcamentoDesconto.Text);
            //info.totalfrete;
            info.totalorcamentobruto = clsParser.DecimalParse(tbxTotalMercadoriaSoma.Text);
            //            info.totalorcamentoliquido = clsParser.DecimalParse(tbxTotalOrcamentoLiquido.Text);
            //info.totalpeso;
            //info.validade;
        }
        private void Orcamento1FillGridToInfo(clsOrcamento1Info info, Int32 posicao)
        {
            DataRow row = dtPedido1.Select("pedido1_posicao=" + posicao)[0];

            info.id = 0;
            info.desconto = 0;
            info.descricao1 = "";
            info.descricao2 = "";
            info.descricao3 = "";
            info.descricao4 = "";
            //info.foto = clsParser.imageToByteArray(row["foto"].ToString());
            //info.fotocodigo = row["fotocodigo"].ToString();
            //if (File.Exists(info.fotocodigo.Trim()))
            //{
            //    info.foto = Image.FromFile(info.fotocodigo);
            //}
            //else
            //{
            //    info.foto = null;
            //}


            //if (File.Exists(info.fotocodigo.Trim()))
            //{
            //    try
            //    {
            //        var img = Image.FromFile(info.fotocodigo); //
            //        info.foto = img;
            //    }
            //    catch
            //    {
            //        info.foto = null;
            //    }
            //}
            //else
            //{
            //    info.foto = null;
            //}
            info.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
            info.idorcamento = idorcamento;
            info.idordemservico = 0;
            info.idordemservicoitem = 0;
            info.idpedido = 0;
            info.idpedidoitem = 0;
            info.item = clsParser.Int32Parse(row["nitem"].ToString());
            info.motivo = "";
            info.peso = clsParser.DecimalParse(row["peso"].ToString());
            info.preco = clsParser.DecimalParse(row["preco"].ToString());
            info.precoconfirmado = 0;
            info.precoliquido = clsParser.DecimalParse(row["preco"].ToString());
            info.qtde = clsParser.DecimalParse(row["qtde"].ToString());
            info.qtdeconfirmadaitem = 0;
            info.referencia1 = "";
            info.referencia2 = "";
            info.situacao = "";
            info.totalconfirmadoitem = 0;
            info.totalmercadoria = clsParser.DecimalParse(row["totalnota"].ToString());
            //info.tributo_previsto = clsParser.DecimalParse(row["tributo_previsto"].ToString());
            info.totalpeso = clsParser.DecimalParse(row["totalpeso"].ToString());
            info.unid = row["unid"].ToString();
            info.valordesconto = 0;
        }
        private void ApagarTelaPrincipal()
        {
            tbxCPF_CNPJ.Text = "";
            tbxCognome.Text = "";
            tbxCliente_telefone.Text = "";
            //tbxVendedor.Text = "";
            idcliente = 0;
            idcodigo = 0;
            idpedido = 0;
            tbxQtdeParcela.Text = "0";
            tbxQtdeTotal.Text = "0,00";
            tbxTotalMercadoriaSoma.Text = "0,00";
            tbxTotalDesconto.Text = "0,00";
            tbxTotalPedido.Text = "0,00";
            tbxTotalCusto.Text = "0,00";


            // limpar 
            //dtPedido1 = clsPedido1BLL.GridDadosPDV(0, clsInfo.conexaosqldados);
            //dgvPedido1.DataSource = null;

            dtPedido1 = clsPedido1BLL.GridDadosPDV(0, clsInfo.conexaosqldados);
            DataColumn dcPosicao = new DataColumn("pedido1_posicao", Type.GetType("System.Int32"));
            dtPedido1.Columns.Add(dcPosicao);
            for (Int32 i = 1; i <= dtPedido1.Rows.Count; i++)
            {
                dtPedido1.Rows[i - 1]["pedido1_posicao"] = i;
            }
            dtPedido1.AcceptChanges();
            clsPedido1BLL.GridMontaPDV(dgvPedido1, dtPedido1, pedido1_posicao);


            tbxCPF_CNPJ.Select();
            tbxCPF_CNPJ.SelectAll();


        }

        private void tbxTotalMercadoriaSoma_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxQtdeSaldo_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxTotalDesconto_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxTotalMercadoriaSoma_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void cbxSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gbxFormaPagamento_Enter(object sender, EventArgs e)
        {

        }

        private void btnVoltaCodigo_Click(object sender, EventArgs e)
        {
            tbxPecas_codigo.Select();
            tbxPecas_codigo.SelectAll();
        }

        private void tbxPecas_codigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxPreco_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnComercial_Click(object sender, EventArgs e)
        {
            if (idcliente > 0 & idcliente != clsInfo.zempresaclienteid)
            {
                clsInfo.znomegrid = btnComercial.Name;
                frmClienteComercial frmClienteComercial = new frmClienteComercial();
                frmClienteComercial.Init(idcliente);
                clsFormHelper.AbrirForm(this.MdiParent, frmClienteComercial, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Favor informar o Cliente para acessar a Ficha Comercial");
            }

        }
    }
}