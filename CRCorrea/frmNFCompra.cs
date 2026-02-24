using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Transactions;
using System.Windows.Forms;
using System.Xml;

namespace CRCorrea
{ 
    public partial class frmNFCompra : Form
    {
        Int32 painelPrincipal = 0;
        String caminhoXML = "";
        String caminhoPDF = "";

        ParameterFields pfields = new ParameterFields();

        // NFCompra
        clsNfCompraBLL clsNfCompraBLL;
        clsNfCompraInfo clsNfCompraInfo;
        clsNfCompraInfo clsNfCompraInfoOld;



        Int32 id;
        DataGridViewRowCollection rows;

        Int32 nfcompra_idcondpagto;
        Int32 nfcompra_iddocumento;
        Int32 nfcompra_idformapagto;
        Int32 nfcompra_idfornecedor;
        Int32 nfcompra_idfornecedorold;
        Int32 nfcompra_idfornecedororigem;
        Int32 nfcompra_idpedido;
        Int32 nfcompra_idtransportadora;

        // NFCompra1
        DataTable dtNFCompra1;

        clsNfCompra1BLL clsNfCompra1BLL;
        clsNfCompra1Info clsNfCompra1Info;
        clsNfCompra1Info clsNfCompra1InfoOld;

        Int32 nfcompra1_id;
        Int32 nfcompra1_posicao;
        Int32 nfcompra1_numero;
        Int32 nfcompra1_idcentrocusto;
        Int32 nfcompra1_idcfop;
        Int32 nfcompra1_idcfopfis;
        Int32 nfcompra1_idcodigo;
        Int32 nfcompra1_idcodigo1;
        Int32 nfcompra1_idcodigoctabil;
        Int32 nfcompra1_idcotacao;
        Int32 nfcompra1_iddestino;
        Int32 nfcompra1_idhistorico;
        Int32 nfcompra1_idipi;
        Int32 nfcompra1_iditemcotacao;
        Int32 nfcompra1_iditempedido;
        Int32 nfcompra1_iditempedidoentrega;
        Int32 nfcompra1_idnfevales;
        Int32 nfcompra1_idnfvendaitem;
        Int32 nfcompra1_idnotafiscal;
        Int32 nfcompra1_idordemservico;
        Int32 nfcompra1_idpedido;
        Int32 nfcompra1_idpedidovenda;
        Int32 nfcompra1_idpedidovendaitem;
        Int32 nfcompra1_idsittriba;
        Int32 nfcompra1_idsittribb;
        Int32 nfcompra1_idsittribipi;
        Int32 nfcompra1_idsittribpis;
        Int32 nfcompra1_idsittribcofins1;
        Int32 nfcompra1_idsolicitacao;
        Int32 nfcompra1_idtiponota;
        Int32 nfcompra1_idtiponotasaida;
        Int32 nfcompra1_idunid;
        Int32 nfcompra1_idunidfiscal;
        String tipoproduto;
        String faturando;

        // NFCompraPagar
        DataTable dtNFCompraPagar;
        DataTable dtNFCompraPagarTempDeletar;

        clsNfCompraPagarBLL clsNfCompraPagarBLL;
        clsNfCompraPagarInfo clsNfCompraPagarInfo;
        clsNfCompraPagarInfo clsNfCompraPagarInfoOld;

        Int32 nfcomprapagar_id;
        Int32 nfcomprapagar_posicao;
        Int32 nfcomprapagar_idnota;
        Int32 nfcomprapagar_idtipopaga;

        // Compras Pendentes
        DataTable dtComprasPendentes;

        clsCompras1BLL clsCompras1BLL;

        // Fiscal a Nota Fiscal 
        clsNfCompraBLL clsNfCompraFiscalBLL;

        // NFCompra1
        clsNfCompra1BLL clsNfCompra1FiscalBLL;

        clsPecasInfo clsPecasInfo = new clsPecasInfo();
        clsPecasBLL clsPecasBLL = new clsPecasBLL();

        //AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        //WebBrowser webBrowser1;

        public frmNFCompra()
        {
            InitializeComponent();
        }

        public void Init(Int32 id,
                         DataGridViewRowCollection rows)
        {
            this.id = id;
            this.rows = rows;

            //criarmos em tempo de execução porque não funciona 
            //na hora de scanear os formularios para a pemissão do usuario
            //axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            //tabPDF.Controls.Add(axAcroPDF1);
            //
            //webBrowser1 = new WebBrowser();
            //tabXML1.Controls.Add(webBrowser1);
            //

            clsNfCompraBLL = new clsNfCompraBLL();
            clsNfCompra1BLL = new clsNfCompra1BLL();
            clsNfCompraPagarBLL = new clsNfCompraPagarBLL();
            clsCompras1BLL = new clsCompras1BLL();
            clsNfCompraFiscalBLL = new clsNfCompraBLL();
            clsNfCompra1FiscalBLL = new clsNfCompra1BLL();


            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxCliente_Cognome);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from situacaotipotitulo order by codigo", tbxFormaPagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from condpagto where ativo = 's' order by codigo", tbxCondPagto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxCognomeTransporte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from docfiscal order by codigo", tbxDocumento);
            // nfcompra1 - 
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", NFCompra1_tbxPecas_codigo);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from tiponota order by codigo", NFCompra1_tbxTipoNota);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cfop from cfop order by cfop", NFCompra1_tbxCfopfis);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cfop from cfop order by cfop", NFCompra1_tbxCfop);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from sittributariaa order by codigo", NFCompra1_tbxSitOrigem);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from sittributariaa order by codigo", NFCompra1_tbxSitICMS);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", NFCompra1_tbxCentrocusto);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", NFCompra1_tbxHistoricos_codigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", NFCompra1_tbxPecas_codigo2);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from unidade order by codigo", NFCompra1_tbxUnidadeFiscal);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from unidade order by codigo", NFCompra1_tbxUnidade);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from ipi order by codigo", NFCompra1_tbxIpi_codigo);

            clsVisual.FillComboBox(NFCompra1_cbxTiponota, "select codigo + ' = ' + nome from tiponota order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(NFCompra1_cbxSittriba, "select codigo from sittributariaa order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(NFCompra1_cbxSittribb, "select codigo from sittributariab order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(NFCompra1_cbxSittribipi, "select codigo from sittribipi order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(NFCompra1_cbxSittribpis, "select codigo from sittribpis order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(NFCompra1_cbxSittribcofins1, "select codigo from sittribcofins order by codigo", clsInfo.conexaosqldados);

            //NFCompra1_cbxTiponota.SelectedIndex = 0;
            NFCompra1_cbxSittriba.SelectedIndex = 0;
            NFCompra1_cbxSittribb.SelectedIndex = 0;
            NFCompra1_cbxSittribipi.SelectedIndex = 0;
            NFCompra1_cbxSittribpis.SelectedIndex = 0;
            NFCompra1_cbxSittribcofins1.SelectedIndex = 0;
        }

        private void frmNFCompra_Load(object sender, EventArgs e)
        {
            tspNFCompraPagar.Visible = true;
            tspNFcompra1Itens.Visible = true;
            tspNFCompraPg.Visible = true;
            tspNFCompra.Visible = true;
            tspNFCompra1.Visible = true;
            tspRetornar_XML_PDF.Visible = true;

            //webBrowser1.Visible = false;

            NFCompraCarregar();
        }

        private void frmNFCompra_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void frmNFCompra_Shown(object sender, EventArgs e)
        {

        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Salvar() == DialogResult.Cancel)
                {
                    return;
                }

                DataGridView dgv = new DataGridView();
                DataGridViewTextBoxColumn dgvC = new DataGridViewTextBoxColumn();

                dgvC.Name = "ID";
                dgvC.HeaderText = "ID";

                dgv.Columns.Add(dgvC);
                dgv.Rows.Add(new Object[] { id });

                clsInfo.zrow = dgv.Rows[0];

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    NFCompraCarregar();
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
                                NFCompraCarregar();
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
                                NFCompraCarregar();
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
                    NFCompraCarregar();
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
                if (id > 0)
                {
                    DataGridView dgv = new DataGridView();
                    DataGridViewTextBoxColumn dgvC = new DataGridViewTextBoxColumn();

                    dgvC.Name = "ID";
                    dgvC.HeaderText = "ID";

                    dgv.Columns.Add(dgvC);
                    dgv.Rows.Add(new Object[] { id });

                    clsInfo.zrow = dgv.Rows[0];
                }

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
            /*
            if (tclNFCompra.SelectedIndex == 1)
            {
                NFCompra1SomaTotal();
            }*/
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
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

                if (clsInfo.znomegrid == btnIdCliente.Name)  // FORNECEDOR
                {
                    tbxCliente_Cognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();

                    if (Check_Fornecedor() == true) // Detectou mudança de Fornecedor
                    {
                        Fill_Fornecedor();          // Carrega os dados do Fornecedor escolhido
                    }

                    tbxCliente_Cognome.Select();
                    tbxCliente_Cognome.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdPedido.Name) // PEDIDO DE COMPRA
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        nfcompra_idpedido = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCompra_Numero.Text = clsInfo.zrow.Cells["COMPRAS_NUMERO"].Value.ToString();
                        tbxCompra_Ano.Text = clsParser.DateTimeParse(clsInfo.zrow.Cells["COMPRAS_DATA"].Value.ToString()).Year.ToString();

                        clsInfo.zrow = null;
                        clsInfo.znomegrid = "";

                        // incluir todos os itens do pedido indicado 
                        // com qtde de saldo > 0
                        EntradaPedidoCompra(nfcompra_idpedido);

                        // ComprasEntregaCarregarGrid(nfcompra_idpedido);
                    }

                    tbxNumero.Select();
                    tbxNumero.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdDocumento.Name)   // COMPRADOR
                {
                    tbxDocumento.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxSerie.Text = clsInfo.zrow.Cells["SERIE"].Value.ToString();
                    if (Check_Documento() == true)
                    {
                        Fill_Documento();
                    }

                    tbxDocumento.Select();
                    tbxDocumento.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdFormaPagto.Name)  // FORMA DE PAGAMENTO
                {
                    tbxFormaPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxFormaPagto.Text += "=" + clsInfo.zrow.Cells["NOME"].Value.ToString();

                    if (Check_Formapagto() == true)
                    {
                        Fill_Formapagto();
                    }

                    tbxFormaPagto.Select();
                    tbxFormaPagto.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdCondPagto.Name)   // CONDIÇÃO DE PAGAMENTO
                {
                    tbxCondPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxCondPagto.Text += "=" + clsInfo.zrow.Cells["NOME"].Value.ToString();

                    if (Check_Condpagto() == true)
                    {
                        Fill_Condpagto();
                    }

                    tbxCondPagto.Select();
                    tbxCondPagto.SelectAll();
                }
                else if (clsInfo.znomegrid == btnIdTransportadora.Name)  // TRANSPORTADORA
                {
                    tbxCognomeTransporte.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();

                    if (Check_Transportadora() == true)
                    {
                        Fill_Transportadora();
                    }

                    tbxCognomeTransporte.Select();
                    tbxCognomeTransporte.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnIdCodigo.Name)    // PRODUTO
                {
                    NFCompra1_tbxPecas_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_Pecas() == true)
                    {
                        Fill_Pecas();
                    }

                    NFCompra1_tbxPecas_codigo.Select();
                    NFCompra1_tbxPecas_codigo.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnIdCodigo2.Name)   // PRODUTO DESTINO
                {
                    NFCompra1_tbxPecas_codigo2.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_Pecas2() == true)
                    {
                        Fill_Pecas2();
                    }

                    NFCompra1_tbxPecas_codigo2.Select();
                    NFCompra1_tbxPecas_codigo2.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnIdTipoNota.Name)    // TIPO DA NOTA
                {
                    /*
                    NFCompra1_tbxTipoNota.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_TipoNota() == true)
                    {
                        Fill_TipoNota();
                    }

                    Compras1_tbxTipoNota.Select();
                    Compras1_tbxTipoNota.SelectAll();*/
                }
                else if (clsInfo.znomegrid == NFCompra1_btnIdcfopFiscal.Name)    // CFOP FISCAL
                {
                    NFCompra1_tbxCfopfis.Text = clsInfo.zrow.Cells["CFOP"].Value.ToString();

                    if (Check_CfopFis() == true)
                    {
                        Fill_CfopFis();
                    }

                    NFCompra1_tbxCfopfis.Select();
                    NFCompra1_tbxCfopfis.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnIdcfop.Name)  // CFOP
                {
                    NFCompra1_tbxCfop.Text = clsInfo.zrow.Cells["CFOP"].Value.ToString();

                    if (Check_Cfop() == true)
                    {
                        Fill_Cfop();
                    }

                    NFCompra1_tbxCfop.Select();
                    NFCompra1_tbxCfop.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnSitTriba.Name) // SIT. TRIB. A
                {
                    NFCompra1_cbxSittriba.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["codigo"].Value.ToString(), 0, NFCompra1_cbxSittriba);

                    if (Check_Sittriba() == true)
                    {
                        Fill_Sittriba();
                    }

                    NFCompra1_cbxSittriba.Select();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnSitTribb.Name) // SIT. TRIB. B
                {
                    NFCompra1_cbxSittribb.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["codigo"].Value.ToString(), 0, NFCompra1_cbxSittriba);

                    if (Check_Sittribb() == true)
                    {
                        Fill_Sittribb();
                    }

                    NFCompra1_cbxSittribb.Select();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnCentrocusto.Name)  // CENTRO DE CUSTO
                {
                    NFCompra1_tbxCentrocusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_CentroCusto() == true)
                    {
                        Fill_CentroCusto();
                    }

                    NFCompra1_tbxCentrocusto.Select();
                    NFCompra1_tbxCentrocusto.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnHistorico.Name)   // HISTORICO
                {
                    NFCompra1_tbxHistoricos_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_Historico() == true)
                    {
                        Fill_Historico();
                    }

                    NFCompra1_tbxHistoricos_codigo.Select();
                    NFCompra1_tbxHistoricos_codigo.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnUnidade_codigo.Name)  // UNIDADE
                {
                    NFCompra1_tbxUnidade.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_Unidade() == true)
                    {
                        Fill_Unidade();
                    }

                    NFCompra1_tbxUnidade.Select();
                    NFCompra1_tbxUnidade.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnUnidadeFis_codigo.Name)  // UNIDADE FISCAL
                {
                    NFCompra1_tbxUnidadeFiscal.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_UnidFiscal() == true)
                    {
                        Fill_UnidFiscal();
                    }

                    NFCompra1_tbxUnidadeFiscal.Select();
                    NFCompra1_tbxUnidadeFiscal.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnClassfiscal.Name) // CLASS. FISCAL
                {
                    NFCompra1_tbxIpi_codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();

                    if (Check_ClassFiscal() == true)
                    {
                        Fill_ClassFiscal();
                    }

                    NFCompra1_tbxIpi_codigo.Select();
                    NFCompra1_tbxIpi_codigo.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnSittribipi.Name)   // CST IPI
                {
                    NFCompra1_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["CODIGO"].Value.ToString(), 0, NFCompra1_cbxSittribipi);

                    if (Check_Sittribipi() == true)
                    {
                        Fill_Sittribipi();
                    }

                    NFCompra1_cbxSittribipi.Select();
                    NFCompra1_cbxSittribipi.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnSittribpis.Name)   // CST PIS
                {
                    NFCompra1_cbxSittribpis.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["CODIGO"].Value.ToString(), 0, NFCompra1_cbxSittribpis);

                    if (Check_Sittribpis() == true)
                    {
                        Fill_Sittribpis();
                    }

                    NFCompra1_cbxSittribpis.Select();
                    NFCompra1_cbxSittribpis.SelectAll();
                }
                else if (clsInfo.znomegrid == NFCompra1_btnSittribcofins1.Name)   // CST COFINS
                {
                    NFCompra1_cbxSittribcofins1.SelectedIndex = clsVisual.SelecionarIndex(clsInfo.zrow.Cells["CODIGO"].Value.ToString(), 0, NFCompra1_cbxSittribcofins1);

                    if (Check_Sittribcofins() == true)
                    {
                        Fill_Sittribcofins();
                    }

                    NFCompra1_cbxSittribcofins1.Select();
                    NFCompra1_cbxSittribcofins1.SelectAll();
                }
            }
            else
            {
                // ###############################
                // Verifica os campos de pesquisa
                // ###############################

                if (ctl.Name == tbxCliente_Cognome.Name)    // FORNECEDOR
                {
                    if (Check_Fornecedor() == true) // Detectou mudança de Fornecedor
                    {
                        Fill_Fornecedor();          // Carrega os dados do Fornecedor escolhido
                    }
                }
                else if (ctl.Name == tbxDocumento.Name) // COMPRADOR
                {
                    if (Check_Documento() == true)
                    {
                        Fill_Documento();
                    }
                }
                else if (ctl.Name == tbxFormaPagto.Name)    // FORMA DE PAGAMENTO
                {
                    if (Check_Formapagto() == true)
                    {
                        Fill_Formapagto();
                    }
                }
                else if (ctl.Name == tbxCondPagto.Name)     // CONDIÇÃO DE PAGAMENTO
                {
                    if (Check_Condpagto() == true)
                    {
                        Fill_Condpagto();
                    }
                }
                else if (ctl.Name == tbxCognomeTransporte.Name) // TRANSPORTADORA
                {
                    if (Check_Transportadora() == true)
                    {
                        Fill_Transportadora();
                    }
                }
                else if (ctl.Name == NFCompra1_tbxPecas_codigo.Name) // PRODUTO
                {
                    if (Check_Pecas() == true)
                    {
                        Fill_Pecas();
                    }
                }
                else if (ctl.Name == NFCompra1_tbxPecas_codigo2.Name)    // PRODUTO DESTINO
                {
                    if (Check_Pecas2() == true)
                    {
                        Fill_Pecas2();
                    }
                }
                else if (ctl.Name == NFCompra1_cbxTiponota.Name) // TIPO DA NF
                {
                    /*
                    if (Check_TipoNota() == true)
                    {
                        Fill_TipoNota();
                    }*/
                }
                else if (ctl.Name == NFCompra1_tbxCfopfis.Name)  // CFOP FISCAL
                {
                    if (Check_CfopFis() == true)
                    {
                        Fill_CfopFis();
                    }
                }
                else if (ctl.Name == NFCompra1_tbxCfop.Name) // CFOP
                {
                    if (Check_Cfop() == true)
                    {
                        Fill_Cfop();
                    }
                }
                else if (ctl.Name == NFCompra1_cbxSittriba.Name) // SIT. TRIB. A
                {
                    if (Check_Sittriba() == true)
                    {
                        Fill_Sittriba();
                    }
                }
                else if (ctl.Name == NFCompra1_cbxSittribb.Name) // SIT. TRIB. B
                {
                    if (Check_Sittribb() == true)
                    {
                        Fill_Sittribb();
                    }
                }
                else if (ctl.Name == NFCompra1_tbxCentrocusto.Name)  // CENTRO DE CUSTO
                {
                    if (Check_CentroCusto() == true)
                    {
                        Fill_CentroCusto();
                    }
                }
                else if (ctl.Name == NFCompra1_tbxHistoricos_codigo.Name)    // HISTORICO
                {
                    if (Check_Historico() == true)
                    {
                        Fill_Historico();
                    }
                }
                else if (ctl.Name == NFCompra1_tbxUnidade.Name)  // UNIDADE
                {
                    if (Check_Unidade() == true)
                    {
                        Fill_Unidade();
                    }
                }
                else if (ctl.Name == NFCompra1_tbxUnidadeFiscal.Name)  // UNIDADE FISCAL
                {
                    if (Check_UnidFiscal() == true)
                    {
                        Fill_UnidFiscal();
                    }
                }
                else if (ctl.Name == NFCompra1_tbxIpi_codigo.Name)   // CLASS. FISCAL
                {
                    if (Check_ClassFiscal() == true)
                    {
                        Fill_ClassFiscal();
                    }
                }
                else if (ctl.Name == NFCompra1_cbxSittribipi.Name)   // CST IPI
                {
                    if (Check_Sittribipi() == true)
                    {
                        Fill_Sittribipi();
                    }
                }
                else if (ctl.Name == NFCompra1_cbxSittribpis.Name)   // CST PIS
                {
                    if (Check_Sittribpis() == true)
                    {
                        Fill_Sittribpis();
                    }
                }
                else if (ctl.Name == NFCompra1_cbxSittribcofins1.Name)   // CST COFINS
                {
                    if (Check_Sittribcofins() == true)
                    {
                        Fill_Sittribcofins();
                    }
                }
                else if (ctl.Name == cbxSetor.Name) // SETOR
                {
                    if (cbxSetor.Text.Substring(0, 1) == "N")
                    {
                        tbxSetorFator.Text = "0";
                    }
                }
                else if (ctl.Name == NFCompraPagar_tbxFormaPagto.Name)
                {
                    nfcomprapagar_idtipopaga = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITUACAOTIPOTITULO where codigo='" + NFCompraPagar_tbxFormaPagto.Text.Substring(0, 2) + "' "));
                    if (nfcomprapagar_idtipopaga == 0)
                    {
                        nfcomprapagar_idtipopaga = clsInfo.zformapagto;
                    }
                    NFCompraPagar_tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + nfcomprapagar_idtipopaga);
                    NFCompraPagar_tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + nfcomprapagar_idtipopaga);
                }
                else if (clsInfo.znomegrid == NFCompraPagar_btnIdFormaPagto.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            nfcomprapagar_idtipopaga = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                            NFCompraPagar_tbxFormaPagto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                            NFCompraPagar_tbxFormaPagto.Text += " = " + clsInfo.zrow.Cells["NOME"].Value.ToString();
                        }
                    }
                    NFCompraPagar_tbxFormaPagto.Select();
                }
                else if (ctl.Name == NFCompra1_tbxCodigoemp01.Name)
                {
                    
                }
                else if (tclNFCompra.SelectedIndex == 1)    // SÓ SE ESTIVER COM O ITEM ABERTO
                {
                    if (ctl.Name == NFCompra1_tbxQtdeFiscal.Name)
                    {
                        if (clsParser.DecimalParse(NFCompra1_tbxQtde.Text) <= 0)
                        {
                            NFCompra1_tbxQtdeFiscal.Text = clsParser.DecimalParse(NFCompra1_tbxQtdeFiscal.Text).ToString("n5");
                            NFCompra1_tbxQtde.Text = NFCompra1_tbxQtdeFiscal.Text;
                        }

                        Calcular();
                    }
                    else if (ctl.Name == NFCompra1_tbxPreco.Name ||
                             ctl.Name == NFCompra1_cbxSittriba.Name ||
                             ctl.Name == NFCompra1_cbxSittribb.Name ||
                             ctl.Name == NFCompra1_tbxIcm.Name ||
                             ctl.Name == NFCompra1_tbxBasemp.Name ||
                             ctl.Name == NFCompra1_tbxIcminterno.Name ||
                             ctl.Name == NFCompra1_tbxIcmssubstreducao.Name ||
                             ctl.Name == NFCompra1_tbxReducao.Name ||
                             ctl.Name == NFCompra1_tbxIpi.Name ||
                             ctl.Name == NFCompra1_tbxAliqpispasep.Name ||
                             ctl.Name == NFCompra1_tbxAliqcofins1.Name)
                    {
                        Calcular();
                    }
                    else if (ctl.Name == NFCompra1_lbxTipoProduto.Name)
                    {
                        if (NFCompra1_lbxTipoProduto.Text.Substring(0, 2) == "30")
                        {
                            gbxPrestacaoServico.Enabled = true;
                        }
                        else
                        {
                            gbxPrestacaoServico.Enabled = false;
                            NFCompra1_ckxIrrf.Checked = false;
                            NFCompra1_ckxInss.Checked = false;
                            NFCompra1_ckxPisCofinsCsll.Checked = false;
                            NFCompra1_ckxIss.Checked = false;
                            NFCompra1_ckxPis.Checked = false;
                            NFCompra1_ckxCofins.Checked = false;
                            NFCompra1_ckxCsll.Checked = false;
                        }
                    }
                }
            }
            // Pegar o Preço de Venda se existir
            if (clsParser.DecimalParse(NFCompra1_tbxPrecoCustounit.Text) > 0 & clsParser.DecimalParse(NFCompra1_tbxPreco.Text) > 0)
            {   //Calcular Lucratividade
                NFCompra1_tbxMargemLucro.Text = (clsParser.DecimalParse(NFCompra1_tbxPrecoCustounit.Text) / clsParser.DecimalParse(NFCompra1_tbxPreco.Text)).ToString("N4");

            }
            NFCompraPagar_tbxValor.Text = clsParser.DecimalParse(NFCompraPagar_tbxValor.Text).ToString("N2");
            NFCompra1SomaTotal();
             
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }

        private Boolean Check_Fornecedor()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxCliente_Cognome.Text + "'"));

            if (idtmp == nfcompra_idfornecedor)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zempresaclienteid;
            }

            nfcompra_idfornecedor = idtmp;

            return true;
        }

        private void Fill_Fornecedor()
        {
            tbxCliente_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID=" + nfcompra_idfornecedor);

            String pessoa = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PESSOA from CLIENTE where ID=" + nfcompra_idfornecedor + "");
            tbxCliente_cnpj.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CGC from CLIENTE where ID=" + nfcompra_idfornecedor);

            if (pessoa == "j") tbxCliente_cnpj.Text = clsVisual.CamposVisual("CGC", tbxCliente_cnpj.Text);
            else tbxCliente_cnpj.Text = clsVisual.CamposVisual("CPF", tbxCliente_cnpj.Text);

            if (id ==0)
            { // Trazer a forma de pagto e a condição
                nfcompra_idcondpagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcondpagto from CLIENTE where ID=" + nfcompra_idfornecedor));
                tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM CONDPAGTO where id=" + nfcompra_idcondpagto) + "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM CONDPAGTO where id=" + nfcompra_idcondpagto);
                nfcompra_idformapagto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idformapagto from CLIENTE where ID=" + nfcompra_idfornecedor));
                tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from situacaotipotitulo where id=" + nfcompra_idformapagto) + "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from situacaotipotitulo where id=" + nfcompra_idformapagto);
            }

            ComprasEntregaCarregarGrid(0);
        }

        private Boolean Check_Transportadora()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxCognomeTransporte.Text + "'"));

            if (idtmp == nfcompra_idtransportadora)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zempresaclienteid;
            }

            nfcompra_idtransportadora = idtmp;

            return true;
        }

        private void Fill_Transportadora()
        {
            tbxCognomeTransporte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID=" + nfcompra_idtransportadora);
        }

        private Boolean Check_Documento()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + tbxDocumento.Text + "'"));

            if (idtmp == nfcompra_iddocumento)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zdocumento;
            }

            nfcompra_iddocumento = idtmp;

            return true;
        }

        private void Fill_Documento()
        {
            tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID=" + nfcompra_iddocumento);
            tbxSerie.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select serie from DOCFISCAL where ID=" + nfcompra_iddocumento);
        }

        private Boolean Check_Formapagto()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from situacaotipotitulo where codigo='" + tbxFormaPagto.Text.Split('=')[0] + "'"));

            if (idtmp == nfcompra_idformapagto)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zformapagto;
            }

            nfcompra_idformapagto = idtmp;

            return true;
        }

        private void Fill_Formapagto()
        {
            tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from situacaotipotitulo where id=" + nfcompra_idformapagto) + "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from situacaotipotitulo where id=" + nfcompra_idformapagto);
        }

        private Boolean Check_Condpagto()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from condpagto where codigo='" + tbxCondPagto.Text.Split('=')[0] + "'"));

            if (idtmp == nfcompra_idcondpagto)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcondpagto;
            }

            nfcompra_idcondpagto = idtmp;

            return true;
        }

        private void Fill_Condpagto()
        {
            tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM CONDPAGTO where id=" + nfcompra_idcondpagto) + "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM CONDPAGTO where id=" + nfcompra_idcondpagto);
        }

        private Boolean Check_Pecas()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='" + NFCompra1_tbxPecas_codigo.Text + "'"));

            if (idtmp == nfcompra1_idcodigo)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zpecas;
            }

            nfcompra1_idcodigo = idtmp;

            //tipoproduto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TIPOPRODUTO from PECAS WHERE ID= " + nfcompra1_idcodigo);
            //NFCompra1_lbxTipoProduto.SelectedIndex = clsVisual.SelecionarIndex(tipoproduto, 0, NFCompra1_lbxTipoProduto);

            if (nfcompra1_iddestino == clsInfo.zpecas) nfcompra1_iddestino = nfcompra1_idcodigo;

            if (NFCompra1_rbnTipodestinoIte.Checked == true)
            {
                if (nfcompra1_idcodigo1 == clsInfo.zpecas)
                {
                    nfcompra1_idcodigo1 = nfcompra1_idcodigo;
                    Fill_Pecas2();
                }
            }

            nfcompra1_idhistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT idhistoricobco FROM PECAS where id=" + nfcompra1_idcodigo));
            if (nfcompra1_idhistorico == 0) nfcompra1_idhistorico = clsInfo.zhistoricos;
            Fill_Historico();

            //  sit tributaria origem
            nfcompra1_idsittriba = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT idsittriba FROM PECAS where id=" + nfcompra1_idcodigo));
            Fill_Sittriba();

            // sit tributaria icm
            nfcompra1_idsittribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT idsittribvenda FROM PECAS where id=" + nfcompra1_idcodigo));
            Fill_Sittribb();

            // ipi // classificação fiscal
            nfcompra1_idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT idipi FROM PECAS where id=" + nfcompra1_idcodigo));
            if (nfcompra1_idipi == 0) nfcompra1_idipi = clsInfo.zipi;
            Fill_ClassFiscal();
            NFCompra1_tbxIpi.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ALIQUOTA FROM IPI where id=" + nfcompra1_idipi)).ToString("N2");
            if (clsParser.DecimalParse(NFCompra1_tbxIpi.Text) > 0)
            {
                NFCompra1_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex("50", 0, NFCompra1_cbxSittribipi);
            }
            else
            {
                NFCompra1_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex("51", 0, NFCompra1_cbxSittribipi);
            }

            // UNIDADE FISCAL
            nfcompra1_idunidfiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADECOM FROM PECAS where id=" + nfcompra1_idcodigo));
            if (nfcompra1_idunidfiscal == 0) nfcompra1_idunidfiscal = clsInfo.zunidade;
            Fill_UnidFiscal();

            // UNIDADE
            nfcompra1_idunid = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADE FROM pecas where id=" + nfcompra1_idcodigo));
            if (nfcompra1_idunid == 0) nfcompra1_idunid = clsInfo.zunidade;
            Fill_Unidade();

            // baseMP
            //NFCompra1_tbxBasemp.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT BASEMP FROM PECAS where id=" + nfcompra1_idcodigo))).ToString("N2");

            // peso
            NFCompra1_tbxPeso.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT PESOUNIT FROM PECAS where id=" + nfcompra1_idcodigo))).ToString("N3");

            //Ipi_Carrega();
            //NFCompra1_tbxPrecoCustounit.Text = "0";
            //NFCompra1_tbxMargemLucro.Text = "";

            CarregaImpostos();

            return true;
        }

        private void Fill_Pecas()
        {
            clsPecasInfo = clsPecasBLL.Carregar(nfcompra1_idcodigo, clsInfo.conexaosqldados);

            NFCompra1_tbxPecas_codigo.Text = clsPecasInfo.codigo; //Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + nfcompra1_idcodigo);
            NFCompra1_tbxPecas_nome.Text = clsPecasInfo.nome; //Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + nfcompra1_idcodigo);
            pbxFoto.Image = clsPecasInfo.foto;
            NFCompra1_lbxTipoProduto.SelectedIndex = clsVisual.SelecionarIndex(clsPecasInfo.tipoproduto, 2, NFCompra1_lbxTipoProduto);
            //
            // NFCompra1_tbxFatorConversao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select fatorconv from pecas where id=" + nfcompra1_idcodigo)).ToString("N6");
            if (clsParser.DecimalParse(NFCompra1_tbxPreco.Text) == 0)
            {  // pegar o ultimo valor enviado pelo fornecedor
                NFCompra1_tbxPreco.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                                               "select top 1  nfcompra1.preco from nfcompra inner join nfcompra1  on nfcompra.id = nfcompra1.numero " +
                                                               "inner join cliente on cliente.id = nfcompra.idfornecedor where nfcompra1.idcodigo= " +
                                                              nfcompra1_idcodigo +
                                                               " and nfcompra.idfornecedor = " +
                                                               nfcompra_idfornecedor
                                                               + " ORDER BY nfcompra1.id DESC")).ToString("N6");
                // Pegar o preço de venda para calcular o lucro
                NFCompra1_tbxPrecoCustounit.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoravista from pecaspreco where IDcodigo= " + nfcompra1_idcodigo + " ORDER BY DATA DESC"))).ToString("N4");
                NFCompra1_tbxMargemLucro.Text = "";
                if (clsParser.DecimalParse(NFCompra1_tbxPrecoCustounit.Text) > 0 & clsParser.DecimalParse(NFCompra1_tbxPreco.Text) > 0)
                {   //Calcular Lucratividade
                    NFCompra1_tbxMargemLucro.Text = (clsParser.DecimalParse(NFCompra1_tbxPrecoCustounit.Text) / clsParser.DecimalParse(NFCompra1_tbxPreco.Text)).ToString("N4");

                }

                // Pegar o tipo do produto
                tipoproduto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TIPOPRODUTO from PECAS WHERE ID= " + nfcompra1_idcodigo);
                NFCompra1_lbxTipoProduto.SelectedIndex = clsVisual.SelecionarIndex(tipoproduto, 2, NFCompra1_lbxTipoProduto);

            }
        }

        private Boolean Check_Pecas2()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='" + NFCompra1_tbxPecas_codigo2.Text + "'"));

            if (idtmp == nfcompra1_idcodigo1) 
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zpecas;
            }

            nfcompra1_idcodigo1 = idtmp;

            return true;
        }

        private void Fill_Pecas2()
        {
            NFCompra1_tbxPecas_codigo2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM PECAS where id=" + nfcompra1_idcodigo1);
            NFCompra1_tbxPecas_nome2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM PECAS where id=" + nfcompra1_idcodigo1);
        }

        private Boolean Check_TipoNota()
        {
            /*
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='" + NFCompra1_tbxTipoNota.Text + "'"));

            if (idtmp == nfcompra1_idtiponota)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tiponota where codigo='0000-000'"));
            }

            nfcompra1_idtiponota = idtmp;
            */
            return true;
        }

        private void Fill_TipoNota()
        {
            /*
            NFCompra1_tbxTipoNota.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + nfcompra1_idtiponota);
            NFCompra1_tbxTipoNota.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + nfcompra1_idtiponota);*/
        }

        private Boolean Check_CfopFis()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CFOP where CFOP='" + NFCompra1_tbxCfopfis.Text + "'"));

            if (idtmp == nfcompra1_idcfopfis)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcfop;
            }

            nfcompra1_idcfopfis = idtmp;

            return true;
        }

        private void Fill_CfopFis()
        {
            NFCompra1_tbxCfopfis.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CFOP FROM CFOP where id=" + nfcompra1_idcfopfis);
        }

        private Boolean Check_Cfop()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CFOP where CFOP='" + NFCompra1_tbxCfop.Text + "'"));

            if (idtmp == nfcompra1_idcfop)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcfop;
            }

            nfcompra1_idcfop = idtmp;

            return true;
        }

        private void Fill_Cfop()
        {
            NFCompra1_tbxCfop.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CFOP FROM CFOP where id=" + nfcompra1_idcfop);
        }

        private Boolean Check_Sittriba()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAA where CODIGO='" + NFCompra1_cbxSittriba.Text + "'"));

            if (idtmp == nfcompra1_idsittriba)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsituacaotriba;
            }

            nfcompra1_idsittriba = idtmp;

            return true;
        }

        private void Fill_Sittriba()
        {
            String sittriba = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBUTARIAA where id=" + nfcompra1_idsittriba);
            NFCompra1_cbxSittriba.SelectedIndex = clsVisual.SelecionarIndex(sittriba, 0, NFCompra1_cbxSittriba);
        }

        private Boolean Check_Sittribb()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAB where CODIGO='" + NFCompra1_cbxSittribb.Text + "'"));

            if (idtmp == nfcompra1_idsittribb)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsituacaotribb;
            }

            nfcompra1_idsittribb = idtmp;

            return true;
        }

        private void Fill_Sittribb()
        {
            String sittribb = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBUTARIAB where id=" + nfcompra1_idsittribb);
            NFCompra1_cbxSittribb.SelectedIndex = clsVisual.SelecionarIndex(sittribb, 0, NFCompra1_cbxSittribb);
        }

        private Boolean Check_CentroCusto()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where codigo='" + NFCompra1_tbxCentrocusto.Text + "'"));

            if (idtmp == nfcompra1_idcentrocusto)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zcentrocustos;
            }

            nfcompra1_idcentrocusto = idtmp;

            return true;
        }

        private void Fill_CentroCusto()
        {
            NFCompra1_tbxCentrocusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM CENTROCUSTOS where id=" + nfcompra1_idcentrocusto,"GER");
        }

        private Boolean Check_Historico()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where codigo='" + NFCompra1_tbxHistoricos_codigo.Text + "'","GER"));

            if (idtmp == nfcompra1_idhistorico)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zhistoricos;
            }

            nfcompra1_idhistorico = idtmp;

            return true;
        }

        private void Fill_Historico()
        {
            NFCompra1_tbxHistoricos_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS where id=" + nfcompra1_idhistorico);
        }

        private Boolean Check_UnidFiscal()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from UNIDADE where codigo='" + NFCompra1_tbxUnidadeFiscal.Text + "'"));

            if (idtmp == nfcompra1_idunidfiscal)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zunidade;
            }

            nfcompra1_idunidfiscal = idtmp;

            return true;
        }

        private void Fill_UnidFiscal()
        {
            NFCompra1_tbxUnidadeFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM UNIDADE where id=" + nfcompra1_idunidfiscal);
        }

        private Boolean Check_Unidade()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from UNIDADE where codigo='" + NFCompra1_tbxUnidade.Text + "'"));

            if (idtmp == nfcompra1_idunid)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zunidade;
            }

            nfcompra1_idunid = idtmp;

            return true;
        }

        private void Fill_Unidade()
        {
            NFCompra1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM UNIDADE where id=" + nfcompra1_idunid);
        }

        private Boolean Check_ClassFiscal()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from IPI where codigo='" + NFCompra1_tbxIpi_codigo.Text + "'"));

            if (idtmp == nfcompra1_idipi)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zipi;
            }

            nfcompra1_idipi = idtmp;

            NFCompra1_tbxIpi.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ALIQUOTA FROM IPI where id=" + nfcompra1_idipi)).ToString("N2");

            return true;
        }

        private void Fill_ClassFiscal()
        {
            NFCompra1_tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM IPI where id=" + nfcompra1_idipi);
        }

        private Boolean Check_Sittribipi()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from sittribipi where codigo='" + NFCompra1_cbxSittribipi.Text + "'"));

            if (idtmp == nfcompra1_idsittribipi)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsittribipi;
            }

            nfcompra1_idsittribipi = idtmp;

            return true;
        }

        private void Fill_Sittribipi()
        {
            String cstipi = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBIPI where id=" + nfcompra1_idsittribipi);
            NFCompra1_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex(cstipi, 0, NFCompra1_cbxSittribipi);
        }

        private Boolean Check_Sittribpis()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from sittribpis where codigo='" + NFCompra1_cbxSittribpis.Text + "'"));

            if (idtmp == nfcompra1_idsittribpis)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsittribpis;
            }

            nfcompra1_idsittribpis = idtmp;

            return true;
        }

        private void Fill_Sittribpis()
        {
            String cstpis = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBPIS where id=" + nfcompra1_idsittribpis);
            NFCompra1_cbxSittribpis.SelectedIndex = clsVisual.SelecionarIndex(cstpis, 0, NFCompra1_cbxSittribpis);
        }

        private Boolean Check_Sittribcofins()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from sittribcofins where codigo='" + NFCompra1_cbxSittribcofins1.Text + "'"));

            if (idtmp == nfcompra1_idsittribcofins1)
            {
                return false;
            }
            else if (idtmp == 0)
            {
                idtmp = clsInfo.zsittribcofins;
            }

            nfcompra1_idsittribcofins1 = idtmp;

            return true;
        }

        private void Fill_Sittribcofins()
        {
            String cstcofins1 = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITTRIBCOFINS where id=" + nfcompra1_idsittribcofins1);
            NFCompra1_cbxSittribcofins1.SelectedIndex = clsVisual.SelecionarIndex(cstcofins1, 0, NFCompra1_cbxSittribcofins1);
        }

        void CarregaImpostos()
        {
            Int32 iduforigem = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idestado from cliente where id=" + nfcompra_idfornecedor));

            // Icms
            if (NFCompra1_cbxSittriba.SelectedIndex != -1 && NFCompra1_cbxSittriba.Text.Substring(0, 1) == "1") // Se for de origem estrangeira, utiliza-se alíquota interna do estado
            {
                NFCompra1_tbxIcm.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + clsInfo.zipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
//                NFCompra1_tbxBasemp.Text = clsParser.DecimalParse("0").ToString("n2");
            }
            else
            {
                /*
                if (item_tiponota_devolucao == true) // Beneficiamento
                {
                    Item_tbxIcm.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + clsInfo.zipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
                    Item_tbxBasemp.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducao from ipiestadosicms where idipi=" + clsInfo.zipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + idufdestino)).ToString("n2");
                }
                else
                {*/
                NFCompra1_tbxIcm.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + nfcompra1_idipi + " and idestado = " + iduforigem + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
                //NFCompra1_tbxBasemp.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducao from ipiestadosicms where idipi=" + nfcompra1_idipi + " and idestado = " + iduforigem + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
                //}
            }

            // ST
            NFCompra1_tbxIcminterno.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + nfcompra1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
            NFCompra1_tbxIcmssubstreducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducaoiva from ipiestadosicms where idipi=" + nfcompra1_idipi + " and idestado = " + iduforigem + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
            NFCompra1_tbxReducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from ipiestadosicms where idipi=" + nfcompra1_idipi + " and idestado = " + iduforigem + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");

            if (iduforigem != clsInfo.zempresa_ufid && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + clsInfo.zempresa_ufid) == "MT")
            {
                NFCompra1_tbxReducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from tab_cnae_uf where idtabcnae=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcnae from cliente where id=" + clsInfo.zempresaclienteid) + " and iduf=" + clsInfo.zempresa_ufid)).ToString("n2");
            }
            else
            {
                NFCompra1_tbxReducao.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from ipiestadosicms where idipi=" + nfcompra1_idipi + " and idestado = " + clsInfo.zempresa_ufid + " and idestadodestino = " + clsInfo.zempresa_ufid)).ToString("n2");
            }

            // Pis
            NFCompra1_tbxAliqpispasep.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select LEIPISPASEP FROM EMPRESAGERE where EMPRESA = " + clsInfo.zempresaid, "0")).ToString("n2");
            //NFCompra1_tbxAliqpispasep.Text = clsParser.DecimalParse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "EMPRESASGERE", "LEIPISPASEP", "EMPRESA", clsInfo.zempresaid)).ToString("n2");

            // Cofins
            NFCompra1_tbxAliqcofins1.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select LEICOFINS FROM EMPRESAGERE where EMPRESA = " + clsInfo.zempresaclienteid, "0")).ToString("n2");
            //NFCompra1_tbxAliqcofins1.Text = clsParser.DecimalParse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "EMPRESASGERE", "LEICOFINS", "EMPRESA", clsInfo.zempresaclienteid)).ToString("n2");

            // Verifica se é remessa e que seja o primeiro lançamento
            if (cbxSituacao.Text.Substring(0, 1) == "4")
            {
                if (clsParser.DecimalParse(NFCompra1_tbxQtdeFiscal.Text) == 0)
                {
                    // colocando os codigos 
                    nfcompra1_idsittriba = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAA WHERE CODIGO= '0' "));
                    NFCompra1_cbxSittriba.SelectedIndex = NFCompra1_cbxSittriba.FindString("0");
                    if (NFCompra1_cbxSittriba.SelectedIndex == -1)
                    {
                        NFCompra1_cbxSittriba.SelectedIndex = 0;
                    }
                    nfcompra1_idsittribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAB WHERE CODIGO= '50' "));
                    NFCompra1_cbxSittribb.SelectedIndex = NFCompra1_cbxSittribb.FindString("50");
                    if (NFCompra1_cbxSittribb.SelectedIndex == -1)
                    {
                        NFCompra1_cbxSittribb.SelectedIndex = 0;
                    }
                    nfcompra1_idsittribipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBIPI WHERE CODIGO= '05' "));
                    NFCompra1_cbxSittribipi.SelectedIndex = NFCompra1_cbxSittribipi.FindString("05");
                    if (NFCompra1_cbxSittribipi.SelectedIndex == -1)
                    {
                        NFCompra1_cbxSittribipi.SelectedIndex = 0;
                    }
                    nfcompra1_idsittribpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBPIS WHERE CODIGO= '09' "));
                    NFCompra1_cbxSittribpis.SelectedIndex = NFCompra1_cbxSittribpis.FindString("09");
                    if (NFCompra1_cbxSittribpis.SelectedIndex == -1)
                    {
                        NFCompra1_cbxSittribpis.SelectedIndex = 0;
                    }
                    nfcompra1_idsittribcofins1 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBCOFINS WHERE CODIGO= '09' "));
                    NFCompra1_cbxSittribcofins1.SelectedIndex = NFCompra1_cbxSittribcofins1.FindString("09");

                    if (NFCompra1_cbxSittribcofins1.SelectedIndex == -1)
                    {
                        NFCompra1_cbxSittribcofins1.SelectedIndex = 0;
                    }
                    // colocando zero
                    NFCompra1_tbxIpi.Text = 0.ToString("N2");
                    NFCompra1_tbxIcm.Text = 0.ToString("N2");
                    NFCompra1_tbxIcminterno.Text = 0.ToString("N2");
                    NFCompra1_tbxReducao.Text = 0.ToString("N2");
                    NFCompra1_tbxAliqpispasep.Text = 0.ToString("N2");
                    NFCompra1_tbxAliqcofins1.Text = 0.ToString("N2");
                }
            }


            Calcular();
        }

        private void Calcular()
        {
            if (NFCompra1_ckxCalculoautomatico.Checked == true &&
                NFCompra1_cbxSittriba.SelectedIndex != -1 &&
                NFCompra1_cbxConsumo.SelectedIndex != -1 &&
                clsParser.DecimalParse(NFCompra1_tbxPreco.Text) > 0)
            {
                clsNotafiscal NF = new clsNotafiscal();

                NF.iduforigem = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDESTADO from CLIENTE where ID=" + nfcompra_idfornecedor));
                NF.idufdestino = clsInfo.zempresa_ufid;
                NF.idipi = nfcompra1_idipi;
                NF.idcliente = clsInfo.zempresaclienteid;
                NF.revendedor = false;
                NF.isentoipi = false;
                NF.arealivrecomercio = false;
                NF.consumo = (NFCompra1_cbxConsumo.Text.Substring(0, 1) == "S");

                NF.qtde = clsParser.DecimalParse(NFCompra1_tbxQtdeFiscal.Text);
                NF.preco = clsParser.DecimalParse(NFCompra1_tbxPreco.Text);

                NF.origem = NFCompra1_cbxSittriba.Text.Substring(0, 1);
                NF.icmscst = NFCompra1_cbxSittribb.Text.Substring(0, 2);
                NF.icms = clsParser.DecimalParse(NFCompra1_tbxIcm.Text);
                NF.icmsreducao = clsParser.DecimalParse(NFCompra1_tbxBasemp.Text);

                NF.icmsst = clsParser.DecimalParse(NFCompra1_tbxIcminterno.Text);
                NF.icmsstreducao = clsParser.DecimalParse(NFCompra1_tbxIcmssubstreducao.Text);
                NF.icmsstmva = clsParser.DecimalParse(NFCompra1_tbxReducao.Text);

                if (NFCompra1_cbxSittribipi.SelectedIndex == -1)
                {
                    NFCompra1_cbxSittribipi.SelectedIndex = 0;
                }

                NF.ipicst = NFCompra1_cbxSittribipi.Text.Substring(0, 2);
                NF.ipi = clsParser.DecimalParse(NFCompra1_tbxIpi.Text);

                NF.piscst = NFCompra1_cbxSittribpis.Text;
                NF.pis = clsParser.DecimalParse(NFCompra1_tbxAliqpispasep.Text);
                NF.cofinscst = NFCompra1_cbxSittribcofins1.Text;
                NF.cofins = clsParser.DecimalParse(NFCompra1_tbxAliqcofins1.Text);

                NF.valoroutrassomabcicms = NFCompra1_ckxValorOutrasIcms.Checked;
                NF.valoroutras = clsParser.DecimalParse(NFCompra1_tbxValoroutros.Text);

                NF.valorfretesomabcicms = NFCompra1_ckxValorFreteIcms.Checked;
                NF.valorfrete = clsParser.DecimalParse(NFCompra1_tbxValorfrete.Text);

                NF.valorsegurosomabcicms = NFCompra1_ckxValorSeguroIcms.Checked;
                NF.valorseguro = clsParser.DecimalParse(NFCompra1_tbxValorseguro.Text);

                NF.Calcular();

                if (NFCompra1_cbxSittribipi.SelectedIndex == -1)
                {
                    NFCompra1_cbxSittribipi.SelectedIndex = 0;
                }

                NFCompra1_cbxSittribipi.SelectedIndex = clsVisual.SelecionarIndex(NF.ipicst, 2, NFCompra1_cbxSittribipi);
                Check_ClassFiscal();

                NFCompra1_cbxSittribb.SelectedIndex = clsVisual.SelecionarIndex(NF.icmscst, 2, NFCompra1_cbxSittribb);
                Check_Sittribb();

                NFCompra1_cbxSittribpis.SelectedIndex = clsVisual.SelecionarIndex(NF.piscst, 2, NFCompra1_cbxSittribpis);
                Check_Sittribpis();

                NFCompra1_cbxSittribcofins1.SelectedIndex = clsVisual.SelecionarIndex(NF.cofinscst, 2, NFCompra1_cbxSittribcofins1);
                Check_Sittribcofins();

                NFCompra1_tbxQtdeFiscal.Text = NF.qtde.ToString("n5");
                NFCompra1_tbxPreco.Text = NF.preco.ToString("n5");
                NFCompra1_tbxBaseicm.Text = NF.icmsbc.ToString("N2");
                NFCompra1_tbxIcm.Text = NF.icms.ToString("N2");
                NFCompra1_tbxBasemp.Text = NF.icmsreducao.ToString("N2");
                NFCompra1_tbxCustoicm.Text = NF.icmstotal.ToString("N2");
                NFCompra1_tbxBaseicmsubst.Text = NF.icmsstbc.ToString("N2");
                NFCompra1_tbxIcminterno.Text = NF.icmsst.ToString("N2");
                NFCompra1_tbxIcmssubstreducao.Text = NF.icmsstreducao.ToString("N2");
                NFCompra1_tbxReducao.Text = NF.icmsstmva.ToString("N2");
                NFCompra1_tbxIcmsubst.Text = NF.icmssttotal.ToString("N2");
                NFCompra1_tbxBcipi.Text = NF.ipibc.ToString("N2");
                NFCompra1_tbxIpi.Text = NF.ipi.ToString("N2");
                NFCompra1_tbxCustoipi.Text = NF.ipitotal.ToString("N2");
                NFCompra1_tbxBcpispasep.Text = NF.pisbc.ToString("N2");
                NFCompra1_tbxAliqpispasep.Text = NF.pis.ToString("N2");
                NFCompra1_tbxPispasep.Text = NF.pistotal.ToString("N2");
                NFCompra1_tbxBccofins1.Text = NF.cofinsbc.ToString("N2");
                NFCompra1_tbxAliqcofins1.Text = NF.cofins.ToString("N2");
                NFCompra1_tbxCofins1.Text = NF.cofinstotal.ToString("N2");
                NFCompra1_tbxTotalmercado.Text = NF.totalmercadoria.ToString("N2");
                NFCompra1_tbxTotalnota.Text = NF.totalnota.ToString("N2");
                NFCompra1_tbxValoroutros.Text = NF.valoroutras.ToString("N2");
                NFCompra1_tbxValorfrete.Text = NF.valorfrete.ToString("N2");
                NFCompra1_tbxValorseguro.Text = NF.valorseguro.ToString("N2");

                // Somar Impostos Prestação de Serviços
                // prestação de serviços (impostos)
                //Decimal por = clsParser.DecimalParse(clsNfCompra1Info.irrfporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.inssporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.piscofinscsllporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.issporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.pisporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.cofinsporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.csllporc.ToString());
                //if (NFCompra1_cbxTipoEntrada.Text.PadRight(2, ' ').Substring(0, 2) == "30")
                //{
                //    if (por == 0)
                //    {
                //        clsNfCompra1Info.irrfporc = clsInfo.zfiscalirrf;
                //        clsNfCompra1Info.inssporc = clsInfo.zfiscalinss;
                //        clsNfCompra1Info.piscofinscsllporc = clsInfo.zfiscalpiscofins;
                //        clsNfCompra1Info.issporc = clsInfo.zfiscaliss;
                //        clsNfCompra1Info.pisporc = clsInfo.zfiscalpis;
                //        clsNfCompra1Info.cofinsporc = clsInfo.zfiscalcofins;
                //        clsNfCompra1Info.csllporc = clsInfo.zfiscalcsll;
                //        NFCompra1_tbxIrrfPorc.Text = clsNfCompra1Info.irrfporc.ToString("N2");
                //        NFCompra1_tbxInssPorc.Text = clsNfCompra1Info.inssporc.ToString("N2");
                //        NFCompra1_tbxPisCofinsCsllPorc.Text = clsNfCompra1Info.piscofinscsllporc.ToString("N2");
                //        NFCompra1_tbxIssPorc.Text = clsNfCompra1Info.issporc.ToString("N2");
                //        NFCompra1_tbxPisPorc.Text = clsNfCompra1Info.pisporc.ToString("N2");
                //        NFCompra1_tbxCofinsPorc.Text = clsNfCompra1Info.cofinsporc.ToString("N2");
                //        NFCompra1_tbxCsllPorc.Text = clsNfCompra1Info.csllporc.ToString("N2");
                //    }
                //}

                NFCompra1_tbxIrrfPorc.Text = clsParser.DecimalParse(NFCompra1_tbxIrrfPorc.Text).ToString("N2");
                NFCompra1_tbxIrrf.Text = clsParser.DecimalParse(NFCompra1_tbxIrrf.Text).ToString("N2");
                NFCompra1_tbxInssPorc.Text = clsParser.DecimalParse(NFCompra1_tbxInssPorc.Text).ToString("N2");
                NFCompra1_tbxInss.Text = clsParser.DecimalParse(NFCompra1_tbxInss.Text).ToString("N2");
                NFCompra1_tbxPisCofinsCsllPorc.Text = clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsllPorc.Text).ToString("N2");
                NFCompra1_tbxPisCofinsCsll.Text = clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsll.Text).ToString("N2");
                NFCompra1_tbxIssPorc.Text = clsParser.DecimalParse(NFCompra1_tbxIssPorc.Text).ToString("N2");
                NFCompra1_tbxIss.Text = clsParser.DecimalParse(NFCompra1_tbxIss.Text).ToString("N2");
                NFCompra1_tbxPisPorc.Text = clsParser.DecimalParse(NFCompra1_tbxPisPorc.Text).ToString("N2");
                NFCompra1_tbxPis.Text = clsParser.DecimalParse(NFCompra1_tbxPis.Text).ToString("N2");
                NFCompra1_tbxCofinsPorc.Text = clsParser.DecimalParse(NFCompra1_tbxCofinsPorc.Text).ToString("N2");
                NFCompra1_tbxCofins.Text = clsParser.DecimalParse(NFCompra1_tbxCofins.Text).ToString("N2");
                NFCompra1_tbxCsllPorc.Text = clsParser.DecimalParse(NFCompra1_tbxCsllPorc.Text).ToString("N2");
                NFCompra1_tbxCsll.Text = clsParser.DecimalParse(NFCompra1_tbxCsll.Text).ToString("N2");

                if (NFCompra1_ckxIrrf.Checked == true)
                {
                    NFCompra1_ckxIrrf.Text = "Sim Tem I.R.R.F.";
                    if (clsParser.DecimalParse(NFCompra1_tbxIrrfPorc.Text) > 0)
                    {
                        NFCompra1_tbxIrrf.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxIrrfPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    NFCompra1_ckxIrrf.Text = "Não Tem I.R.R.F.";
                    NFCompra1_tbxIrrf.Text = 0.ToString("N2");
                }
                if (NFCompra1_ckxInss.Checked == true)
                {
                    NFCompra1_ckxInss.Text = "Sim Tem I.N.S.S.";
                    if (clsParser.DecimalParse(NFCompra1_tbxInssPorc.Text) > 0)
                    {
                        NFCompra1_tbxInss.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxInssPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    NFCompra1_ckxInss.Text = "Não Tem I.N.S.S.";
                    NFCompra1_tbxInss.Text = 0.ToString("N2");
                }
                if (NFCompra1_ckxPisCofinsCsll.Checked == true)
                {
                    NFCompra1_ckxPisCofinsCsll.Text = "Sim Pis/Cofins/Csll";
                    if (clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsllPorc.Text) > 0)
                    {
                        NFCompra1_tbxPisCofinsCsll.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsllPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    NFCompra1_ckxPisCofinsCsll.Text = "Não Pis/Cofins/Csll";
                    NFCompra1_tbxPisCofinsCsll.Text = 0.ToString("N2");
                }
                if (NFCompra1_ckxIss.Checked == true)
                {
                    NFCompra1_ckxIss.Text = "Sim Tem I.S.S.";
                    if (clsParser.DecimalParse(NFCompra1_tbxIssPorc.Text) > 0)
                    {
                        NFCompra1_tbxIss.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxIssPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    NFCompra1_ckxIss.Text = "Não Tem I.S.S.";
                    NFCompra1_tbxIss.Text = 0.ToString("N2");
                }
                if (NFCompra1_ckxPis.Checked == true)
                {
                    NFCompra1_ckxPis.Text = "Sim Tem PIS";
                    if (clsParser.DecimalParse(NFCompra1_tbxPisPorc.Text) > 0)
                    {
                        NFCompra1_tbxPis.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxPisPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    NFCompra1_ckxPis.Text = "Não Tem PIS";
                    NFCompra1_tbxPis.Text = 0.ToString("N2");
                }
                if (NFCompra1_ckxCofins.Checked == true)
                {
                    NFCompra1_ckxCofins.Text = "Sim Tem Cofins";
                    if (clsParser.DecimalParse(NFCompra1_tbxCofinsPorc.Text) > 0)
                    {
                        NFCompra1_tbxCofins.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxCofinsPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    NFCompra1_ckxCofins.Text = "Não Tem Cofins";
                    NFCompra1_tbxCofins.Text = 0.ToString("N2");
                }
                if (NFCompra1_ckxCsll.Checked == true)
                {
                    NFCompra1_ckxCsll.Text = "Sim Tem Cofins";
                    if (clsParser.DecimalParse(NFCompra1_tbxCsllPorc.Text) > 0)
                    {
                        NFCompra1_tbxCsll.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxCsllPorc.Text) / 100)).ToString("N2");
                    }
                }
                else
                {
                    NFCompra1_ckxCsll.Text = "Não Tem Cofins";
                    NFCompra1_tbxCsll.Text = 0.ToString("N2");
                }
            }
        }

        private void EntradaPedidoCompra(int idpedidocompra)
        {
            EntradaPedidoCompraCabecalho(idpedidocompra);

            clsNfCompraBLL.EntradaPedidoCompra(idpedidocompra, ref dtNFCompra1, clsInfo.conexaosqldados);

            foreach (DataRow row in dtNFCompra1.Rows)
            {
                nfcompra1_posicao = clsParser.Int32Parse(row["nfcompra1_posicao"].ToString());
                NFCompra1Carregar();

                Calcular();

                NFCompra1Salvar();
            }

            foreach (DataRow row in dtNFCompra1.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    row.RowState != DataRowState.Detached)
                {
                    foreach (DataRow rowEntrega in dtComprasPendentes.Select("id=" + row["iditempedidoentrega"].ToString()))
                    {
                        if (rowEntrega.RowState != DataRowState.Deleted &&
                            rowEntrega.RowState != DataRowState.Detached)
                        {
                            rowEntrega.Delete();
                        }
                    }
                }
            }

            nfcompra1_posicao = 0;

            NFCompraSomar();
        }

        private void EntradaPedidoCompraEntrega(int idcomprasentrega)
        {
            if (dtNFCompra1.Rows.Count == 0)
            {
                EntradaPedidoCompraCabecalho(clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcompras from comprasentrega where id = " + idcomprasentrega)));
            }

            clsNfCompraBLL.EntradaEntregaItemPedidoCompra(idcomprasentrega, ref dtNFCompra1, clsInfo.conexaosqldados);

            foreach (DataRow row in dtNFCompra1.Rows)
            {
                nfcompra1_posicao = clsParser.Int32Parse(row["nfcompra1_posicao"].ToString());
                NFCompra1Carregar();

                Calcular();

                NFCompra1Salvar();
            }

            foreach (DataRow row in dtComprasPendentes.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    row.RowState != DataRowState.Detached)
                {
                    if (idcomprasentrega.ToString() == row["id"].ToString())
                    {
                        row.Delete();
                    }
                }
            }

            NFCompraSomar();

            nfcompra1_posicao = 0;
        }

        private void EntradaPedidoCompraCabecalho(int idpedidocompra)
        {
            clsComprasInfo ComprasInfo;
            clsComprasBLL ComprasBLL = new clsComprasBLL();

            ComprasInfo = ComprasBLL.Carregar(idpedidocompra, clsInfo.conexaosqldados);

            nfcompra_idpedido = ComprasInfo.id;
            idpedidocompra = ComprasInfo.id;

            nfcompra_idfornecedor = ComprasInfo.idfornecedor;
            nfcompra_idfornecedororigem = ComprasInfo.idfornecedor;
            tbxCliente_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + nfcompra_idfornecedor);
            tbxCliente_cnpj.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from cliente where id=" + nfcompra_idfornecedor);
            String pessoa = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PESSOA from CLIENTE where ID=" + nfcompra_idfornecedor + "");
            if (pessoa == "F")
            {
                tbxCliente_cnpj.Text = clsVisual.CamposVisual("CPF", tbxCliente_cnpj.Text);
            }
            else
            {
                tbxCliente_cnpj.Text = clsVisual.CamposVisual("CGC", tbxCliente_cnpj.Text);
            }


            TrataCampos(tbxCliente_Cognome);

            nfcompra_idcondpagto = ComprasInfo.idcondpagto;
            tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from condpagto where id=" + nfcompra_idcondpagto);
            TrataCampos(tbxCondPagto);

            nfcompra_idformapagto = ComprasInfo.idformapagto;
            tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + '=' + nome from SITUACAOTIPOTITULO where id=" + nfcompra_idformapagto);
            TrataCampos(tbxFormaPagto);

            cbxFrete.SelectedIndex = clsVisual.SelecionarIndex(ComprasInfo.frete, 1, cbxFrete);
            cbxFretepaga.SelectedIndex = clsVisual.SelecionarIndex(ComprasInfo.fretepaga, 1, cbxFretepaga);
            cbxTipofrete.SelectedIndex = clsVisual.SelecionarIndex(ComprasInfo.tipofrete, 1, cbxTipofrete);
            cbxTransporte.SelectedIndex = clsVisual.SelecionarIndex(ComprasInfo.transporte, 1, cbxTransporte);

            nfcompra_idtransportadora = ComprasInfo.idtransportadora;
            tbxCognomeTransporte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + nfcompra_idtransportadora);
            TrataCampos(tbxCognomeTransporte);

            cbxSetor.SelectedIndex = clsVisual.SelecionarIndex(ComprasInfo.setor, 1, cbxSetor);
            tbxSetorFator.Text = ComprasInfo.setorfator.ToString("n2");
        }

        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                NFCompraSalvar();
            }
            return drt;
        }

        private Boolean HouveModificacoes()
        {
            clsNfCompraInfo = new clsNfCompraInfo();
            NFCompraFillInfo(clsNfCompraInfo);
            if (clsNfCompraBLL.Equals(clsNfCompraInfo, clsNfCompraInfoOld) == false)
            {
                return true;
            }
            return false;
        }

        private void NFCompraSalvar()
        {
            DataTable dtNFCompra1_temp = new DataTable();
            DataTable dtNFCompraPagar_temp = new DataTable();

            dtNFCompra1_temp = dtNFCompra1.Copy();
            dtNFCompraPagar_temp = dtNFCompraPagar.Copy();

            // ###############################
            // Verifica as parcelas
            // Verifica se as parcelas estão de acordo com o Total da Fatura
            Decimal totalcobranca = 0;
            Decimal totalcobrancaipi = 0;
            Decimal totalcobrancast = 0;
            Decimal totalcobrancapis = 0;
            Decimal totalcobrancacofins = 0;

            // Mão de Obra
            Decimal totalcobrancamoirrf = 0;
            Decimal totalcobrancamoinss = 0;
            Decimal totalcobrancamopiscofinscsll = 0;
            Decimal totalcobrancamopis = 0;
            Decimal totalcobrancamocofins = 0;
            Decimal totalcobrancamocsll = 0;

            Decimal totalnfcomprapagar = 0;

            // Soma os valores a serem cobrados
            foreach (DataRow row in dtNFCompra1_temp.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    row.RowState != DataRowState.Detached)
                {
                    if (row["fatura"].ToString() == "S")
                    {
                        totalcobranca += clsParser.DecimalParse(row["totalnota"].ToString());
                        totalcobrancaipi += clsParser.DecimalParse(row["custoipi"].ToString());
                        totalcobrancast += clsParser.DecimalParse(row["icmsubst"].ToString());
                        totalcobrancapis += clsParser.DecimalParse(row["pispasep"].ToString());
                        totalcobrancacofins += clsParser.DecimalParse(row["cofins1"].ToString());

                        totalcobrancamoirrf += clsParser.DecimalParse(row["irrf"].ToString());
                        totalcobrancamoinss += clsParser.DecimalParse(row["inss"].ToString());
                        totalcobrancamopiscofinscsll += clsParser.DecimalParse(row["piscofinscsll"].ToString());
                        totalcobrancamopis += clsParser.DecimalParse(row["pis"].ToString());
                        totalcobrancamocofins += clsParser.DecimalParse(row["cofins"].ToString());
                        totalcobrancamocsll += clsParser.DecimalParse(row["csll"].ToString());
                    }
                }
            }

            if (totalcobranca <= 0)
            {
                for (int i = 0; i < dtNFCompraPagar_temp.Rows.Count; i++)
                {
                    if (dtNFCompraPagar_temp.Rows[i].RowState != DataRowState.Deleted &&
                        dtNFCompraPagar_temp.Rows[i].RowState != DataRowState.Detached)
                    {
                        dtNFCompraPagar_temp.Rows[i].Delete();
                    }
                }
            }

            totalcobranca -= (totalcobrancamoirrf +
                              totalcobrancamoinss +
                              totalcobrancamopiscofinscsll +
                              totalcobrancamopis +
                              totalcobrancamocofins +
                              totalcobrancamocsll);

            // Verifica se irá ou não descontar pis/cofins
            Boolean descontapis;
            Boolean descontacofins;
            descontapis = (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DESCONTAPISPASEPSAI from cliente where id=" + nfcompra_idfornecedor) == "S");
            descontacofins = (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DESCONTACOFINSSAI from cliente where id=" + nfcompra_idfornecedor) == "S");

            if (descontapis == true)
            {
                totalcobranca -= totalcobrancapis;
            }

            if (descontacofins == true)
            {
                totalcobranca -= totalcobrancacofins;
            }

            foreach (DataRow row in dtNFCompraPagar_temp.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    row.RowState != DataRowState.Detached)
                {
                    totalnfcomprapagar += clsParser.DecimalParse(row["valor"].ToString());
                }
            }

            if (totalcobranca != totalnfcomprapagar)
            {
                tclNFCompra1Itens.SelectedIndex = 1;
                throw new Exception("Total da Fatura não é igual ao total que está calculado nas Datas de Pagamentos.");
            }

            // ###############################
            // Tabela: NFCOMPRA
            clsNfCompraInfo = new clsNfCompraInfo();
            NFCompraFillInfo(clsNfCompraInfo);
            var option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            option.Timeout = TimeSpan.FromMinutes(10);

            //using (TransactionScope tse = new TransactionScope(TransactionScopeOption.RequiresNew, option))
            //{
                if (id == 0)
                {
                    clsNfCompraInfo.id = clsNfCompraBLL.Incluir(clsNfCompraInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsNfCompraBLL.Alterar(clsNfCompraInfo, clsInfo.conexaosqldados);
                }

                // ITENS DA NOTA FISCAL DE ENTRADA - marca os registros filho da principal
                foreach (DataRow row in dtNFCompra1_temp.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["numero"] = clsNfCompraInfo.id;
                    }
                }

                //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, option))
                {
                    foreach (DataRow row in dtNFCompra1_temp.Rows)
                    {
                        if (row.RowState == DataRowState.Detached ||
                            row.RowState == DataRowState.Unchanged)
                        {
                            continue;
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            clsNfCompra1BLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                            continue;
                        }
                        else
                        {
                            clsNfCompra1Info = new clsNfCompra1Info();
                            NFCompra1GridToInfo(dtNFCompra1_temp, clsNfCompra1Info, Int32.Parse(row["nfcompra1_posicao"].ToString()));

                            if (clsNfCompra1Info.id == 0)
                            {
                                clsNfCompra1Info.id = clsNfCompra1BLL.Incluir(clsNfCompra1Info, clsInfo.conexaosqldados);
                            }
                            else
                            {
                                clsNfCompra1BLL.Alterar(clsNfCompra1Info, clsInfo.conexaosqldados);
                            }
                        }
                    }
                }
                //greidi contas a pagar
                if (dtNFCompraPagarTempDeletar != null)
                {
                    foreach (DataRow row in dtNFCompraPagarTempDeletar.Rows)
                    {
                        clsNfCompraPagarBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                    }
                }
                // contas a pagar
                foreach (DataRow row in dtNFCompraPagar_temp.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idnota"] = clsNfCompraInfo.id;
                    }
                }

                foreach (DataRow row in dtNFCompraPagar_temp.Rows)
                {
                    if (nfcompra_idfornecedor != nfcompra_idfornecedorold && row.RowState != DataRowState.Deleted && clsParser.Int32Parse(row["id"].ToString()) > 0)
                    {
                        if (row.RowState == DataRowState.Unchanged)
                        {
                            // Foi colocado pois quando altera apenas o cognome do fornecedor tem que atualizar o contas a pagar
                            row.SetModified();
                        }
                    }

                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsNfCompraPagarBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsNfCompraPagarInfo = new clsNfCompraPagarInfo();
                        NFCompraPagarGridToInfo(dtNFCompraPagar_temp, clsNfCompraPagarInfo, Int32.Parse(row["posicaorec"].ToString()));
                        if (clsNfCompraPagarInfo.id == 0)
                        {
                            clsNfCompraPagarInfo.id = clsNfCompraPagarBLL.Incluir(clsNfCompraPagarInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsNfCompraPagarBLL.Alterar(clsNfCompraPagarInfo, clsInfo.conexaosqldados);
                        }
                    }
                }

                dtNFCompra1 = dtNFCompra1_temp.Copy();
                dtNFCompraPagar = dtNFCompraPagar_temp.Copy();
                id = clsNfCompraInfo.id;

            //    tse.Complete();
            //}

            //copiando os arquivos XML e PDF para a nova pasta
            //try
            //{
            //    if (caminhoXML.ToString() != tbxCaminhoNFE.Text)
            //    {
            //        if (File.Exists(tbxCaminhoNFE.Text))
            //        {
            //            MessageBox.Show("Arquivo XML já existe na pasta padrão!", "ApliSoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            File.Copy(caminhoXML.ToString(), tbxCaminhoNFE.Text);
            //        }
            //    }
            //    //
            //    if (caminhoPDF.ToString() != tbxCaminhoPDF.Text)
            //    {
            //        if (File.Exists(tbxCaminhoPDF.Text))
            //        {
            //            MessageBox.Show("Arquivo PDF já existe na pasta padrão!", "ApliSoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            File.Copy(caminhoPDF.ToString(), tbxCaminhoPDF.Text);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + Environment.NewLine +
            //        "Verifique a configuração do campo: XML Past, no cadastro da empresa!", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            // Criar a O.S. e Pedido de Venda
            // 1. Criar o Pedido de Venda
            // Inicio -Verificando se o pedido é '0' , situacao = 40 , tipo produto igual produto de venda ou 
            // beneficiamento e tipo entrada 20, 44, 45 se for criar Pedido de Venda e OS
            //                        
            //clsNfCompraInfo.id 
            // Carregar os itens da Nota Fiscal de Entrada
            //if (clsNfCompraInfo.situacao.Substring(0, 1) == "4")
            //{

            //    dtNFCompra1 = clsNfCompra1BLL.GridDados(clsNfCompraInfo.id, clsInfo.conexaosqldados);
            //    foreach (DataRow row in dtNFCompra1.Rows)
            //    {

            //        if (clsParser.Int32Parse(row["idpedidovenda"].ToString()) == clsInfo.zpedido &&
            //           row["tipoproduto"].ToString() == "A" ||
            //           row["tipoproduto"].ToString() == "B" ||
            //           row["tipoproduto"].ToString() == "C" &&
            //           row["tipoentrada"].ToString().Substring(0, 2) == "20" ||
            //           row["tipoentrada"].ToString().Substring(0, 2) == "44" ||
            //           row["tipoentrada"].ToString().Substring(0, 2) == "45")
            //        {
            //            if (clsInfo.zempresacliente_cognome.Substring(0, 7) == "COATING")
            //            {
            //                CriarPedidoVenda_e_OS_Coating(nfcompra1_idpedidovenda, nfcompra1_idpedidovendaitem,
            //                                              nfcompra1_idordemservico, row["tipoproduto"].ToString().Trim(),
            //                                              clsParser.Int32Parse(row["id"].ToString()));
            //            }
            //            else if (clsInfo.zempresacliente_cognome.Substring(0, 5) == "RESOL")
            //            {

            //            }
            //        }
            //    }
            //}
        }


        private void NFCompraCarregar()
        {
            clsNfCompraInfoOld = new clsNfCompraInfo();

            if (id == 0)
            {
                clsNfCompraInfo = new clsNfCompraInfo();
                clsNfCompraInfo.cofins = 0;
                clsNfCompraInfo.cofins1 = clsInfo.zleicofins;
                clsNfCompraInfo.csll = 0;
                clsNfCompraInfo.data = DateTime.Now;
                clsNfCompraInfo.datalanca = DateTime.Now;
                clsNfCompraInfo.datarecebimento = DateTime.Now;
                clsNfCompraInfo.emitente = clsInfo.zusuario;
                clsNfCompraInfo.filial = clsInfo.zfilial;
                clsNfCompraInfo.frete = "C";
                clsNfCompraInfo.fretepaga = "1";
                clsNfCompraInfo.id = 0;
                clsNfCompraInfo.idcondpagto = clsInfo.zcondpagto;
                clsNfCompraInfo.iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where CODIGO= '" + "NFE1" + "' "));
                if (clsNfCompraInfo.iddocumento == 0) { clsNfCompraInfo.iddocumento = clsInfo.zdocumento; }
                clsNfCompraInfo.idformapagto = clsInfo.zformapagto;
                clsNfCompraInfo.idfornecedor = clsInfo.zempresaclienteid;
                clsNfCompraInfo.idfornecedororigem = clsInfo.zempresaclienteid;
                clsNfCompraInfo.idpedido = clsInfo.zcompras;
                clsNfCompraInfo.idtransportadora = clsInfo.zempresaclienteid;
                clsNfCompraInfo.inss = 0;
                clsNfCompraInfo.irrf = 0;
                clsNfCompraInfo.iss = 0;
                clsNfCompraInfo.numero = 0;
                clsNfCompraInfo.chnfe = "";
                clsNfCompraInfo.caminhonfe_xml = "";
                clsNfCompraInfo.caminhonfe_pdf = "";
                clsNfCompraInfo.observa = "";
                clsNfCompraInfo.pis = 0;
                clsNfCompraInfo.piscofinscsll = 0;
                clsNfCompraInfo.pispasep = 0;
                clsNfCompraInfo.serie = "1";
                clsNfCompraInfo.setor = "N";
                clsNfCompraInfo.setorfator = 0;
                clsNfCompraInfo.situacao = "11";
                clsNfCompraInfo.tipoentrada = "P";
                clsNfCompraInfo.tipofrete = "0";
                clsNfCompraInfo.totalbaseicm = 0;
                clsNfCompraInfo.totalbaseicmsubst = 0;
                clsNfCompraInfo.totalfrete = 0;
                clsNfCompraInfo.totalicm = 0;
                clsNfCompraInfo.totalicmsubst = 0;
                clsNfCompraInfo.totalipi = 0;
                clsNfCompraInfo.totalmercadoria = 0;
                clsNfCompraInfo.totalnotafiscal = 0;
                clsNfCompraInfo.totaloutras = 0;
                clsNfCompraInfo.totalpeso = 0;
                clsNfCompraInfo.totalpesobruto = 0;
                clsNfCompraInfo.totalseguro = 0;
                clsNfCompraInfo.transporte = "N";

            }
            else
            {
                clsNfCompraInfo = clsNfCompraBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            NFCompraCampos(clsNfCompraInfo);
            NFCompraFillInfo(clsNfCompraInfoOld);
            //

            // carregando os itens da Nota Fiscal de Entrada

            dtNFCompra1 = clsNfCompra1BLL.GridDados(clsNfCompraInfo.id, clsInfo.conexaosqldados);

            DataColumn dcPosicao = new DataColumn("nfcompra1_posicao", Type.GetType("System.Int32"));
            dtNFCompra1.Columns.Add(dcPosicao);

            for (Int32 i = 1; i <= dtNFCompra1.Rows.Count; i++)
            {
                dtNFCompra1.Rows[i - 1]["nfcompra1_posicao"] = i;
            }

            dtNFCompra1.AcceptChanges();
            clsNfCompra1BLL.GridMonta(dgvNFCompra1, dtNFCompra1, nfcompra1_posicao);

            dgvNFCompra1.Columns["QTDEFISCAL"].DefaultCellStyle.Format = "N3";
            dgvNFCompra1.Columns["QTDE"].DefaultCellStyle.Format = "N3";
            dgvNFCompra1.Columns["PRECO"].DefaultCellStyle.Format = "N6";
            dgvNFCompra1.Columns["TOTALMERCADO"].DefaultCellStyle.Format = "N2";

            // Colocando as Cores nos Itens
            for (Int32 X = 0; X < dgvNFCompra1.Rows.Count; X++)
            {
                switch (dgvNFCompra1.Rows[X].Cells["TIPOPRODUTO"].Value.ToString())
                {
                    case "00":
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.White;
                        break;
                    case "09":
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.PaleGoldenrod;
                        break;
                    case "10":
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.PaleGoldenrod;
                        break;
                    case "20":
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.PaleTurquoise;
                        break;
                    case "30":
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.Thistle;
                        break;
                    case "40":
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.PowderBlue;
                        break;
                    case "44":
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                        break;
                    case "45":
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                        break;
                    case "90":
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.LightSalmon;
                        break;
                    default:    // > 11 e < 22
                        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.Aquamarine;
                        break;
                }
            }


            // carregando o contas a receber do orçamento
            dtNFCompraPagar = clsNfCompraPagarBLL.GridDados(clsNfCompraInfo.id);

            DataColumn dcPosicaoReceber = new DataColumn("posicaorec", Type.GetType("System.Int32"));
            dtNFCompraPagar.Columns.Add(dcPosicaoReceber);

            for (Int32 i = 1; i <= dtNFCompraPagar.Rows.Count; i++)
            {
                dtNFCompraPagar.Rows[i - 1]["posicaorec"] = i;

                // Se alguma parcela for paga desativar botoes
                if (dtNFCompraPagar.Rows[i - 1]["pagou"] == "S")
                {
                    tspNFCompraPagarIncluir.Enabled = false;
                    tspNFCompraPagarExcluir.Enabled = false;
                }
            }
            dtNFCompraPagar.AcceptChanges();
            clsNfCompraPagarBLL.GridMonta(dgvNFCompraPagar, dtNFCompraPagar, nfcomprapagar_posicao);

            dgvNFCompraPagar.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvNFCompraPagar.Columns["VALOR"].DefaultCellStyle.Format = "N2";

            ComprasEntregaCarregarGrid(0);

            // Notas de Saida Emitida para o Fornecedor que ainda não retornaram
            /*
            dtNFVenda1Pendentes = clsNfvenda1BLL.GridDadosPendencia(nfcompra_idfornecedor);
            DataColumn dcPosicaoNFVenda1Pendentes = new DataColumn("posicao_nfvenda1pendentes", Type.GetType("System.Int32"));
            dtNFVenda1Pendentes.Columns.Add(dcPosicaoNFVenda1Pendentes);

            for (Int32 i = 1; i <= dtNFVenda1Pendentes.Rows.Count; i++)
            {
                dtNFVenda1Pendentes.Rows[i - 1]["posicao_nfvenda1pendentes"] = i;
            }
            dtNFVenda1Pendentes.AcceptChanges();
            clsNfvenda1BLL.GridMontaPendencia(dgvNFVenda1Pendentes, dtNFVenda1Pendentes, nfvenda1pendentes_posicao);
            */

            NFCompraSomar();

        }
        private void NFCompraCampos(clsNfCompraInfo info)
        {
            id = info.id;
            tbxChNFe.Text = info.chnfe;
            //tbxCaminhoNFE.Text = info.caminhonfe_xml;
            caminhoXML = info.caminhonfe_xml;
            //tbxCaminhoPDF.Text = info.caminhonfe_pdf;
            caminhoPDF = info.caminhonfe_pdf;
            tbxCofins.Text = info.cofins.ToString("N2");
            tbxTotalcofins1.Text = info.cofins1.ToString("N2");
            tbxCsll.Text = info.csll.ToString("N2");
            tbxData.Text = info.data.ToString("dd/MM/yyyy");
            tbxDataLanca.Text = info.datalanca.ToString("dd/MM/yyyy HH:mm");
            tbxDataRecebimento.Text = info.datarecebimento.ToString("dd/MM/yyyy HH:mm");
            tbxEmitente.Text = info.emitente;
            tbxFilial.Text = info.filial.ToString();
            cbxFrete.Text = info.frete;
            cbxFrete.SelectedIndex = cbxFrete.FindString(info.frete);
            if (cbxFrete.SelectedIndex == -1)
            {
                cbxFrete.SelectedIndex = 0;
            }
            // info.fretebaseicms.ToString("N2");
            //info.freteicmaliq
            cbxFretepaga.SelectedIndex = cbxFretepaga.FindString(info.fretepaga);
            if (cbxFretepaga.SelectedIndex == -1)
            {
                cbxFretepaga.SelectedIndex = 0;
            }
            nfcompra_idcondpagto = info.idcondpagto;
            if (nfcompra_idcondpagto == 0) { nfcompra_idcondpagto = clsInfo.zcondpagto; }
            tbxCondPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONDPAGTO where ID= " + nfcompra_idcondpagto);
            nfcompra_iddocumento = info.iddocumento;
            if (nfcompra_iddocumento == 0) { nfcompra_iddocumento = clsInfo.zdocumento; }
            tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from DOCFISCAL where ID= " + nfcompra_iddocumento);
            nfcompra_idformapagto = info.idformapagto;
            if (nfcompra_idformapagto == 0) { nfcompra_idformapagto = clsInfo.zformapagto; }
            tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + nfcompra_idformapagto);
            tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + nfcompra_idformapagto);
            nfcompra_idfornecedor = info.idfornecedor;
            if (nfcompra_idfornecedor == 0) { nfcompra_idfornecedor = clsInfo.zempresaclienteid; }
            nfcompra_idfornecedorold = nfcompra_idfornecedor;
            tbxCliente_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + nfcompra_idfornecedor);
            tbxCliente_cnpj.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CGC FROM CLIENTE where id=" + nfcompra_idfornecedor);
            String pessoa = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PESSOA from CLIENTE where ID=" + nfcompra_idfornecedor + "");
            if (pessoa == "F")
            {
                tbxCliente_cnpj.Text = clsVisual.CamposVisual("CPF", tbxCliente_cnpj.Text);
            }
            else
            {
                tbxCliente_cnpj.Text = clsVisual.CamposVisual("CGC", tbxCliente_cnpj.Text);
            }
            nfcompra_idfornecedororigem = info.idfornecedororigem;
            if (nfcompra_idfornecedororigem == 0) { nfcompra_idfornecedororigem = nfcompra_idfornecedor; }
            nfcompra_idpedido = info.idpedido;
            if (nfcompra_idpedido == 0) { nfcompra_idpedido = clsInfo.zcompras; }
            tbxCompra_Numero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NUMERO FROM COMPRAS where id=" + nfcompra_idpedido);
            tbxCompra_Ano.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ANO FROM COMPRAS where id=" + nfcompra_idpedido);
            nfcompra_idtransportadora = info.idtransportadora;
            if (nfcompra_idtransportadora == 0) { nfcompra_idtransportadora = clsInfo.zempresaclienteid; }
            tbxCognomeTransporte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE where id=" + nfcompra_idtransportadora);

            tbxInss.Text = info.inss.ToString("N2");
            tbxIrrf.Text = info.irrf.ToString("N2");
            tbxIss.Text = info.iss.ToString("N2");
            tbxNumero.Text = info.numero.ToString();
            tbxObserva.Text = info.observa;
            tbxPis.Text = info.pis.ToString("N2");
            tbxPisCofinsCsll.Text = info.piscofinscsll.ToString("N2");
            tbxSerie.Text = info.serie;
            cbxSetor.SelectedIndex = cbxSetor.FindString(info.setor);
            if (cbxSetor.SelectedIndex == -1)
            {
                cbxSetor.SelectedIndex = 0;
            }
            tbxSetorFator.Text = info.setorfator.ToString("N2");
            cbxSituacao.SelectedIndex = cbxSituacao.FindString(info.situacao);
            if (cbxSituacao.SelectedIndex == -1)
            {
                cbxSituacao.SelectedIndex = 0;
            }
            cbxTipoentrada.SelectedIndex = cbxTipoentrada.FindString(info.tipoentrada);
            if (cbxTipoentrada.SelectedIndex == -1)
            {
                cbxTipoentrada.SelectedIndex = 0;
            }
            cbxTipofrete.SelectedIndex = cbxTipofrete.FindString(info.tipofrete);
            if (cbxTipofrete.SelectedIndex == -1)
            {
                cbxTipofrete.SelectedIndex = 0;
            }
            tbxTotalbaseicm.Text = info.totalbaseicm.ToString("N2");
            tbxTotalbaseicmsubst.Text = info.totalbaseicmsubst.ToString("N2");
            tbxTotalfrete.Text = info.totalfrete.ToString("N2");
            //tbxTotalBaseIcms.Text = info.totalfreteicms;
            tbxTotalicm.Text = info.totalicm.ToString("N2");
            tbxTotalIcmSubst.Text = info.totalicmsubst.ToString("N2");
            tbxTotalipi.Text = info.totalipi.ToString("N2");
            tbxTotalmercadoria.Text = info.totalmercadoria.ToString("N2");
            tbxTotalNotaFiscal.Text = info.totalnotafiscal.ToString("N2");
            tbxTotalOutras.Text = info.totaloutras.ToString("N2");
            tbxTotalPeso.Text = info.totalpeso.ToString("N2");
            tbxTotalPesoBruto.Text = info.totalpesobruto.ToString("N2");
            tbxTotalseguro.Text = info.totalseguro.ToString("N2");
            cbxTransporte.SelectedIndex = cbxTransporte.FindString(info.transporte);
            if (cbxTransporte.SelectedIndex == -1)
            {
                cbxTransporte.SelectedIndex = 0;
            }

            if (clsParser.DecimalParse(tbxTotalNotaFiscal.Text) > 0)
            {
                gbxTipoEntrada.Enabled = false;
            }
            else
            {
                cbxTipoentrada.Select();
                cbxTipoentrada.SelectAll();
            }
        }
        private void NFCompraFillInfo(clsNfCompraInfo info)
        {
            info.chnfe = tbxChNFe.Text;
            //info.caminhonfe_xml = tbxCaminhoNFE.Text;
            //info.caminhonfe_pdf = tbxCaminhoPDF.Text;
            info.cofins = clsParser.DecimalParse(tbxCofins.Text);
            info.cofins1 = clsParser.DecimalParse(tbxTotalcofins1.Text);
            info.csll = clsParser.DecimalParse(tbxCsll.Text);
            info.data = DateTime.Parse(tbxData.Text);
            info.datalanca = DateTime.Parse(tbxDataLanca.Text);
            info.datarecebimento = DateTime.Parse(tbxDataRecebimento.Text);
            info.emitente = tbxEmitente.Text;
            info.filial = clsParser.Int32Parse(tbxFilial.Text);
            info.frete = cbxFrete.Text.Substring(0, 1);
            //info.fretebaseicms
            //info.freteicmaliq
            info.fretepaga = cbxFretepaga.Text.Substring(0, 1);
            //info.fretevaloricms
            info.id = id;
            info.idcondpagto = nfcompra_idcondpagto;
            info.iddocumento = nfcompra_iddocumento;
            info.idformapagto = nfcompra_idformapagto;
            info.idfornecedor = nfcompra_idfornecedor;
            info.idfornecedororigem = nfcompra_idfornecedororigem;
            info.idpedido = nfcompra_idpedido;
            info.idtransportadora = nfcompra_idtransportadora;
            info.inss = clsParser.DecimalParse(tbxInss.Text);
            info.irrf = clsParser.DecimalParse(tbxIrrf.Text);
            info.iss = clsParser.DecimalParse(tbxIss.Text);
            info.numero = clsParser.Int64Parse(tbxNumero.Text.Replace(".", ""));
            info.observa = tbxObserva.Text;
            info.pis = clsParser.DecimalParse(tbxPis.Text);
            info.piscofinscsll = clsParser.DecimalParse(tbxPisCofinsCsll.Text);
            info.pispasep = clsParser.DecimalParse(tbxTotalpispasep.Text);
            info.serie = tbxSerie.Text;
            info.setor = cbxSetor.Text.Substring(0, 1);
            info.setorfator = clsParser.DecimalParse(tbxSetorFator.Text);
            info.situacao = cbxSituacao.Text.Substring(0, 1);
            info.tipoentrada = cbxTipoentrada.Text.Substring(0, 1);
            info.tipofrete = cbxTipofrete.Text.Substring(0, 1);
            info.totalbaseicm = clsParser.DecimalParse(tbxTotalbaseicm.Text);
            info.totalbaseicmsubst = clsParser.DecimalParse(tbxTotalbaseicmsubst.Text);
            info.totalfrete = clsParser.DecimalParse(tbxTotalfrete.Text);
            //info.totalfreteicms = clsParser.DecimalParse(tbxto tbxPisPorc.Text);
            info.totalicm = clsParser.DecimalParse(tbxTotalicm.Text);
            info.totalicmsubst = clsParser.DecimalParse(tbxTotalIcmSubst.Text);
            info.totalipi = clsParser.DecimalParse(tbxTotalipi.Text);
            info.totalmercadoria = clsParser.DecimalParse(tbxTotalmercadoria.Text);
            info.totalnotafiscal = clsParser.DecimalParse(tbxTotalNotaFiscal.Text);
            info.totaloutras = clsParser.DecimalParse(tbxTotalOutras.Text);
            //info.totaloutrasicms = clsParser.DecimalParse( tbxPisPorc.Text);
            info.totalpeso = clsParser.DecimalParse(tbxTotalPeso.Text);
            info.totalpesobruto = clsParser.DecimalParse(tbxTotalPesoBruto.Text);
            info.totalseguro = clsParser.DecimalParse(tbxTotalseguro.Text);
            info.transporte = cbxTransporte.Text.Substring(0, 1);
        }

        public void ComprasEntregaCarregarGrid(Int32 diferentedesteid)
        {  // diferente desde id é por quando carrega pelo cabeçalho tava carregando de 
            // novo o mesmo pedido 
            // Compras Pendentes
            dgvComprasPendentes.DataSource = null;

            dtComprasPendentes = clsCompras1BLL.GridDadosPendencia(nfcompra_idfornecedor, diferentedesteid);
            DataColumn dcPosicaoComprasPendentes = new DataColumn("posicao_compraspendentes", Type.GetType("System.Int32"));
            dtComprasPendentes.Columns.Add(dcPosicaoComprasPendentes);

            for (Int32 i = 1; i <= dtComprasPendentes.Rows.Count; i++)
            {
                dtComprasPendentes.Rows[i - 1]["posicao_compraspendentes"] = i;
            }
            dtComprasPendentes.AcceptChanges();
            clsCompras1BLL.GridMontaPendencia(dgvComprasPendentes, dtComprasPendentes, 0);
        }

        private void btnIdPedido_Click(object sender, EventArgs e)
        {
            if (cbxTipoentrada.Text.Substring(0, 1) == "P")
            {
                clsInfo.znomegrid = btnIdPedido.Name;
                frmComprasPes frmComprasPes = new frmComprasPes();
                frmComprasPes.Init();
                clsFormHelper.AbrirForm(this.MdiParent, frmComprasPes, clsInfo.conexaosqldados);
            }
        }

        private void btnIdDocumento_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdDocumento.Name;
            frmDocFiscalVis frmDocFiscalVis = new frmDocFiscalVis();
            frmDocFiscalVis.Init(nfcompra_iddocumento);

            clsFormHelper.AbrirForm(this.MdiParent, frmDocFiscalVis, clsInfo.conexaosqldados);

        }

        private void btnIdCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCliente.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", nfcompra_idfornecedor);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnIdFormaPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdFormaPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", nfcompra_idformapagto, "Formas Pagto");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnIdCondPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCondPagto.Name;
            frmCondPagtoPes frmCondPagtoPes = new frmCondPagtoPes();
            frmCondPagtoPes.Init(clsInfo.conexaosqldados, nfcompra_idcondpagto);

            clsFormHelper.AbrirForm(this.MdiParent, frmCondPagtoPes, clsInfo.conexaosqldados);

        }

        private void btnIdTransportadora_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdTransportadora.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", nfcompra_idtransportadora);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void tspNFCompra1ItensIncluir_Click(object sender, EventArgs e)
        {
            painelPrincipal = 1;
            tclResolve();
            nfcompra1_posicao = 0;
            NFCompra1Carregar();
        }

        private void tspNFCompra1ItensAlterar_Click(object sender, EventArgs e)
        {
            if (dgvNFCompra1.CurrentRow != null)
            {
                painelPrincipal = 1;
                tclResolve();
                nfcompra1_posicao = Int32.Parse(dgvNFCompra1.CurrentRow.Cells["nfcompra1_posicao"].Value.ToString());
                NFCompra1Carregar();
            }
        }

        private void tspNFCompra1ItensExcluir_Click(object sender, EventArgs e)
        {
            if (dgvNFCompra1.CurrentRow != null)
            {
                DialogResult drt;

                drt = MessageBox.Show("Deseja o excluir item selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    foreach (DataRow rowEntrega in dtComprasPendentes.Rows)
                    {
                        if (rowEntrega.RowState == DataRowState.Deleted ||
                            rowEntrega.RowState == DataRowState.Detached)
                        {
                            if (rowEntrega["id", DataRowVersion.Original].ToString() == dgvNFCompra1.CurrentRow.Cells["iditempedidoentrega"].Value.ToString())
                            {
                                rowEntrega.RejectChanges();
                            }
                        }
                    }

                    dtNFCompra1.Select("nfcompra1_posicao = " + dgvNFCompra1.CurrentRow.Cells["nfcompra1_posicao"].Value.ToString())[0].Delete();
                    NFCompraSomar();
                }
            }
        }

        private void tspNFCompra1ItensSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar e alterar?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    NFCompra1Salvar();
                    painelPrincipal = 0;
                    tclResolve();
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

        private void NFCompra1Salvar()
        {
            clsNfCompra1Info = new clsNfCompra1Info();
            NFCompra1FillInfo(clsNfCompra1Info);
            NFCompra1FillInfoToGrid(dtNFCompra1, clsNfCompra1Info);

            gbxNFCompra1.Visible = false;
            tclNFCompra.SelectedIndex = 0;

            NFCompraSomar();
        }

        private void tspNFCompra1ItensPrimeiro_Click(object sender, EventArgs e)
        {

        }

        private void tspNFCompra1ItensAnterior_Click(object sender, EventArgs e)
        {

        }

        private void tspNFCompra1ItensProximo_Click(object sender, EventArgs e)
        {
        }

        private void tspNFCompra1ItensUltimo_Click(object sender, EventArgs e)
        {

        }

        private void tspNFCompra1ItensRetornar_Click(object sender, EventArgs e)
        {
            NFCompraSomar();

            gbxNFCompra1.Visible = false;
            painelPrincipal = 0;
            tclResolve();
        }

        private void NFCompra1Carregar()
        {
            clsNfCompra1Info = new clsNfCompra1Info();
            clsNfCompra1InfoOld = new clsNfCompra1Info();

            if (nfcompra1_posicao == 0)
            {
                nfcompra1_posicao = dtNFCompra1.Rows.Count + 1;

                clsNfCompra1Info.datatabela = DateTime.Now;

                clsNfCompra1Info.idcentrocusto = clsInfo.zcentrocustos;
                clsNfCompra1Info.idcfop = clsInfo.zcfop;
                clsNfCompra1Info.idcfopfis = clsInfo.zcfop;
                clsNfCompra1Info.idcodigo = clsInfo.zpecas;
                clsNfCompra1Info.idcodigo1 = clsInfo.zpecas;
                clsNfCompra1Info.idcodigoctabil = clsInfo.zcontacontabil;
                clsNfCompra1Info.idcotacao = clsInfo.zcotacao;
                clsNfCompra1Info.idhistorico = clsInfo.zhistoricos;
                clsNfCompra1Info.idipi = clsInfo.zipi;
                clsNfCompra1Info.iditemcotacao = clsInfo.zcotacao1;
                clsNfCompra1Info.iditempedido = clsInfo.zcompras1;
                clsNfCompra1Info.iditempedidoentrega = clsInfo.zcomprasentrega;
                clsNfCompra1Info.idnfvendaitem = clsInfo.znfvenda1;
                clsNfCompra1Info.idnotafiscal = clsInfo.znfvenda;
                clsNfCompra1Info.idordemservico = clsInfo.zordemservico;
                clsNfCompra1Info.idpedido = clsInfo.zcompras;
                clsNfCompra1Info.idpedidovenda = clsInfo.zpedido;
                clsNfCompra1Info.idpedidovendaitem = clsInfo.zpedido1;
                clsNfCompra1Info.idsittriba = clsInfo.zsituacaotriba;
                clsNfCompra1Info.idsittribb = clsInfo.zsituacaotribb;
                clsNfCompra1Info.idsittribcofins1 = clsInfo.zsittribcofins;
                clsNfCompra1Info.idsittribipi = clsInfo.zsittribipi;
                clsNfCompra1Info.idsittribpis = clsInfo.zsittribpis;
                clsNfCompra1Info.idsolicitacao = clsInfo.zsolicitacao;
                clsNfCompra1Info.idunid = clsInfo.zunidade;
                clsNfCompra1Info.idunidfiscal = clsInfo.zunidade;

                clsNfCompra1Info.irrfporc = clsInfo.zfiscalirrf;
                clsNfCompra1Info.inssporc = clsInfo.zfiscalinss;
                clsNfCompra1Info.piscofinscsllporc = clsInfo.zfiscalpiscofins;
                clsNfCompra1Info.issporc = clsInfo.zfiscaliss;
                clsNfCompra1Info.pisporc = clsInfo.zfiscalpis;
                clsNfCompra1Info.cofinsporc = clsInfo.zfiscalcofins;
                clsNfCompra1Info.csllporc = clsInfo.zfiscalcsll;
                clsNfCompra1Info.fatura = "S";
                clsNfCompra1Info.calculoautomatico = "S";
            }
            else
            {
                NFCompra1GridToInfo(dtNFCompra1, clsNfCompra1Info, nfcompra1_posicao);
                Decimal por = clsNfCompra1Info.irrfporc + clsNfCompra1Info.inssporc + clsNfCompra1Info.piscofinscsllporc + clsNfCompra1Info.issporc + clsNfCompra1Info.pisporc + clsNfCompra1Info.cofinsporc + clsNfCompra1Info.csllporc;
                if (clsNfCompra1Info.tipoproduto == "30")
                {
                    if (por == 0)
                    {
                        clsNfCompra1Info.irrfporc = clsInfo.zfiscalirrf;
                        clsNfCompra1Info.inssporc = clsInfo.zfiscalinss;
                        clsNfCompra1Info.piscofinscsllporc = clsInfo.zfiscalpiscofins;
                        clsNfCompra1Info.issporc = clsInfo.zfiscaliss;
                        clsNfCompra1Info.pisporc = clsInfo.zfiscalpis;
                        clsNfCompra1Info.cofinsporc = clsInfo.zfiscalcofins;
                        clsNfCompra1Info.csllporc = clsInfo.zfiscalcsll;
                    }
                }
                else
                {
                    clsNfCompra1Info.irrf = 0;
                    clsNfCompra1Info.inss = 0;
                    clsNfCompra1Info.piscofinscsll = 0;
                    clsNfCompra1Info.iss = 0;
                    clsNfCompra1Info.pis = 0;
                    clsNfCompra1Info.cofins = 0;
                    clsNfCompra1Info.csll = 0;
                }
            }

            NFCompra1Campos(clsNfCompra1Info);
            NFCompra1FillInfo(clsNfCompra1InfoOld);

            painelPrincipal = 1;
            tclResolve();
            gbxNFCompra1.Visible = true;

            tbxNFCompra1__numero.Text = clsParser.Int64Parse(tbxNumero.Text).ToString();
            tbxNFCompra1___datarecebimento.Text = DateTime.Parse(tbxDataRecebimento.Text).ToString("dd/MM/yyyy");
            tbxNFCompra1__cognome.Text = tbxCliente_Cognome.Text;

            NFCompra1_tbxPecas_codigo.Select();
            NFCompra1_tbxPecas_codigo.SelectAll();


            NFCompra1SomaTotal();
        }

        private void NFCompra1GridToInfo(DataTable dt, clsNfCompra1Info info, Int32 posicao)
        {
            DataRow row = dt.Select("nfcompra1_posicao = " + posicao)[0];

            info.aliqcofins1 = clsParser.DecimalParse(row["aliqcofins1"].ToString());
            info.aliqpispasep = clsParser.DecimalParse(row["aliqpispasep"].ToString());
            info.baseicm = clsParser.DecimalParse(row["baseicm"].ToString());
            info.baseicmsubst = clsParser.DecimalParse(row["baseicmsubst"].ToString());
            info.basemp = clsParser.DecimalParse(row["basemp"].ToString());
            info.bccofins1 = clsParser.DecimalParse(row["bccofins1"].ToString());
            info.bcipi = clsParser.DecimalParse(row["bcipi"].ToString());
            info.bcpispasep = clsParser.DecimalParse(row["bcpispasep"].ToString());
            info.calculoautomatico = row["calculoautomatico"].ToString();
            info.codigoemp01 = row["codigoemp01"].ToString();
            info.codigoemp02 = row["codigoemp02"].ToString();
            info.codigoemp03 = row["codigoemp03"].ToString();
            info.codigoemp04 = row["codigoemp04"].ToString();
            info.cofins = clsParser.DecimalParse(row["cofins"].ToString());
            info.cofins1 = clsParser.DecimalParse(row["cofins1"].ToString());
            info.cofinsporc = clsParser.DecimalParse(row["cofinsporc"].ToString());
            info.complemento = row["complemento"].ToString();
            info.complemento1 = row["complemento1"].ToString();
            info.consumo = row["consumo"].ToString();
            info.creditaricm = row["creditaricm"].ToString();
            info.csll = clsParser.DecimalParse(row["csll"].ToString());
            info.csllporc = clsParser.DecimalParse(row["csllporc"].ToString());
            info.custoicm = clsParser.DecimalParse(row["custoicm"].ToString());
            info.custoipi = clsParser.DecimalParse(row["custoipi"].ToString());
            if (row["datatabela"].ToString() == "")
            {
                row["datatabela"] = DateTime.Now.ToString();
            }
            info.datatabela = DateTime.Parse(row["datatabela"].ToString());
            info.descricaoesp = row["descricaoesp"].ToString();
            info.fatorconv = clsParser.DecimalParse(row["fatorconv"].ToString());
            info.fatura = row["fatura"].ToString();
            info.faturando = row["faturando"].ToString();
            info.icm = clsParser.DecimalParse(row["icm"].ToString());
            info.icminterno = clsParser.DecimalParse(row["icminterno"].ToString());
            info.icmssubstreducao = clsParser.DecimalParse(row["icmssubstreducao"].ToString());
            info.icmsubst = clsParser.DecimalParse(row["icmsubst"].ToString());
            info.id = clsParser.Int32Parse(row["id"].ToString());
            info.idcentrocusto = clsParser.Int32Parse(row["idcentrocusto"].ToString());
            info.idcfop = clsParser.Int32Parse(row["idcfop"].ToString());
            info.idcfopfis = clsParser.Int32Parse(row["idcfopfis"].ToString());
            info.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
            info.idcodigo1 = clsParser.Int32Parse(row["idcodigo1"].ToString());
            info.idcodigoctabil = clsParser.Int32Parse(row["idcodigoctabil"].ToString());
            info.idcotacao = clsParser.Int32Parse(row["idcotacao"].ToString());
            info.iddestino = clsParser.Int32Parse(row["iddestino"].ToString());
            info.idhistorico = clsParser.Int32Parse(row["idhistorico"].ToString());
            info.idipi = clsParser.Int32Parse(row["idipi"].ToString());
            info.iditemcotacao = clsParser.Int32Parse(row["iditemcotacao"].ToString());
            info.iditempedido = clsParser.Int32Parse(row["iditempedido"].ToString());
            info.iditempedidoentrega = clsParser.Int32Parse(row["iditempedidoentrega"].ToString());
            info.idnfevales = clsParser.Int32Parse(row["idnfevales"].ToString());
            info.idnfvendaitem = clsParser.Int32Parse(row["idnfvendaitem"].ToString());
            info.idnotafiscal = clsParser.Int32Parse(row["idnotafiscal"].ToString());
            info.idordemservico = clsParser.Int32Parse(row["idordemservico"].ToString());
            info.idpedido = clsParser.Int32Parse(row["idpedido"].ToString());
            info.idpedidovenda = clsParser.Int32Parse(row["idpedidovenda"].ToString());
            info.idpedidovendaitem = clsParser.Int32Parse(row["idpedidovendaitem"].ToString());
            info.idsittriba = clsParser.Int32Parse(row["idsittriba"].ToString());
            info.idsittribb = clsParser.Int32Parse(row["idsittribb"].ToString());
            //info.idsittribcofins = clsParser.Int32Parse(row["idsittribcofins"].ToString());
            info.idsittribcofins1 = clsParser.Int32Parse(row["idsittribcofins1"].ToString());
            info.idsittribipi = clsParser.Int32Parse(row["idsittribipi"].ToString());
            info.idsittribpis = clsParser.Int32Parse(row["idsittribpis"].ToString());
            info.idsolicitacao = clsParser.Int32Parse(row["idsolicitacao"].ToString());
            info.idtiponota = clsParser.Int32Parse(row["idtiponota"].ToString());
            info.idtiponotasaida = clsParser.Int32Parse(row["idtiponotasaida"].ToString());
            info.idunid = clsParser.Int32Parse(row["idunid"].ToString());
            info.idunidfiscal = clsParser.Int32Parse(row["idunidfiscal"].ToString());
            info.inss = clsParser.DecimalParse(row["inss"].ToString());
            info.inssporc = clsParser.DecimalParse(row["inssporc"].ToString());
            info.ipi = clsParser.DecimalParse(row["ipi"].ToString());
            info.irrf = clsParser.DecimalParse(row["irrf"].ToString());
            info.irrfporc = clsParser.DecimalParse(row["irrfporc"].ToString());
            info.iss = clsParser.DecimalParse(row["iss"].ToString());
            info.issporc = clsParser.DecimalParse(row["issporc"].ToString());
            info.msg = row["msg"].ToString();
            info.numero = clsParser.Int32Parse(row["numero"].ToString());
            info.peso = clsParser.DecimalParse(row["peso"].ToString());
            info.pesototal = clsParser.DecimalParse(row["pesototal"].ToString());
            info.pis = clsParser.DecimalParse(row["pis"].ToString());
            info.piscofinscsll = clsParser.DecimalParse(row["piscofinscsll"].ToString());
            info.piscofinscsllporc = clsParser.DecimalParse(row["piscofinscsllporc"].ToString());
            info.pispasep = clsParser.DecimalParse(row["pispasep"].ToString());
            info.pisporc = clsParser.DecimalParse(row["pisporc"].ToString());
            info.preco = clsParser.DecimalParse(row["preco"].ToString());
            info.qtde = clsParser.DecimalParse(row["qtde"].ToString());
            info.qtdedev = clsParser.DecimalParse(row["qtdedev"].ToString());
            info.qtdedevbaixa = clsParser.DecimalParse(row["qtdedevbaixa"].ToString());
            info.qtdedevsaldo = clsParser.DecimalParse(row["qtdedevsaldo"].ToString());
            info.qtdefiscal = clsParser.DecimalParse(row["qtdefiscal"].ToString());
            info.qtdeticket = clsParser.DecimalParse(row["qtdeticket"].ToString());
            info.reducao = clsParser.DecimalParse(row["reducao"].ToString());
            info.situacao = row["situacao"].ToString();
            info.somaproduto = row["somaproduto"].ToString();
            info.tipodestino = row["tipodestino"].ToString();
            info.tipoproduto = row["tipoproduto"].ToString();
            info.totalmercado = clsParser.DecimalParse(row["totalmercado"].ToString());
            info.totalnota = clsParser.DecimalParse(row["totalnota"].ToString());
            info.valorfrete = clsParser.DecimalParse(row["valorfrete"].ToString());
            info.valorfreteicms = row["valorfreteicms"].ToString();
            info.valoroutras = clsParser.DecimalParse(row["valoroutras"].ToString());
            info.valoroutrasicms = row["valoroutrasicms"].ToString();
            info.valorseguro = clsParser.DecimalParse(row["valorseguro"].ToString());
            info.valorseguroicms = row["valorseguroicms"].ToString();
            info.vidautil = clsParser.Int32Parse(row["vidautil"].ToString());
        }
        private void NFCompra1Campos(clsNfCompra1Info info)
        {
            nfcompra1_id = info.id;

            NFCompra1_tbxAliqcofins1.Text = info.aliqcofins1.ToString("N2");
            NFCompra1_tbxAliqpispasep.Text = info.aliqpispasep.ToString("N2");
            NFCompra1_tbxBccofins1.Text = info.bccofins1.ToString("N2");
            NFCompra1_tbxBcipi.Text = info.bcipi.ToString("N2");
            NFCompra1_tbxBcpispasep.Text = info.bcpispasep.ToString("N2");
            NFCompra1_tbxBaseicm.Text = info.baseicm.ToString("N2");
            NFCompra1_tbxBaseicmsubst.Text = info.baseicmsubst.ToString("N2");
            NFCompra1_tbxBasemp.Text = info.basemp.ToString("N2");
            if (info.calculoautomatico == "S") { NFCompra1_ckxCalculoautomatico.Checked = true; } else { NFCompra1_ckxCalculoautomatico.Checked = false; }
            NFCompra1_tbxCodigoemp01.Text = info.codigoemp01;
            NFCompra1_tbxCodigoemp02.Text = info.codigoemp02;
            NFCompra1_tbxCodigoemp03.Text = info.codigoemp03;
            NFCompra1_tbxCodigoemp04.Text = info.codigoemp04;
            NFCompra1_tbxCofins.Text = info.cofins.ToString("N2");
            NFCompra1_tbxCofinsPorc.Text = info.cofinsporc.ToString("N2");
            if (info.cofins > 0)
            {
                NFCompra1_ckxCofins.Checked = true;
                NFCompra1_ckxCofins.Text = "Sim Tem COFINS";
            }
            else
            {
                NFCompra1_ckxCofins.Checked = false;
                NFCompra1_ckxCofins.Text = "Não Tem COFINS";
            }

            NFCompra1_tbxCofins1.Text = info.cofins1.ToString("N2");
            NFCompra1_tbxComplemento.Text = info.complemento;
            NFCompra1_tbxComplemento1.Text = info.complemento1;
            NFCompra1_cbxConsumo.SelectedIndex = NFCompra1_cbxConsumo.FindString(info.consumo);
            if (NFCompra1_cbxConsumo.SelectedIndex == -1)
            {
                NFCompra1_cbxConsumo.SelectedIndex = 0;
            }
            // info.creditaricm
            if (info.creditaricm == "S") { NFCompra1_ckxCreditarIcm.Checked = true; } else { NFCompra1_ckxCreditarIcm.Checked = false; }
            NFCompra1_tbxCsll.Text = info.csll.ToString("N2");
            NFCompra1_tbxCsllPorc.Text = info.csllporc.ToString("N2");
            if (info.csll > 0)
            {
                NFCompra1_ckxCsll.Checked = true;
                NFCompra1_ckxCsll.Text = "Sim Tem CSLL";
            }
            else
            {
                NFCompra1_ckxCsll.Checked = false;
                NFCompra1_ckxCsll.Text = "Não Tem CSLL";
            }
            NFCompra1_tbxCustoicm.Text = info.custoicm.ToString("N2");
            NFCompra1_tbxCustoipi.Text = info.custoipi.ToString("N2");
            if (info.datatabela.ToString() == "")
            {
                NFCompra1_tbxDataTabela.Text = "01/01/1900";
            }
            else
            {
                NFCompra1_tbxDataTabela.Text = info.datatabela.ToString("dd/MM/yyyy");
            }
            NFCompra1_tbxDescricaoEsp.Text = info.descricaoesp;
            NFCompra1_tbxFatorConversao.Text = info.fatorconv.ToString("N6");
            //   ==================== NFCompra1_tbxHistoricos_codigo =
            if (info.fatura == "S") { NFCompra1_ckxFatura.Checked = true; } else { NFCompra1_ckxFatura.Checked = false; }

            if (NFCompra1_ckxFatura.Checked == true)
            {
                NFCompra1_ckxFatura.Text = "Sim Será Cobrado ";
            }
            else
            {
                NFCompra1_ckxFatura.Text = "Não Será Cobrado ";
            }

            faturando = info.faturando;
            NFCompra1_tbxIcm.Text = info.icm.ToString("N2");
            NFCompra1_tbxIcminterno.Text = info.icminterno.ToString("N2");
            NFCompra1_tbxIcmssubstreducao.Text = info.icmssubstreducao.ToString("N2");

            if (info.idcentrocusto == 0)
            {
                info.idcentrocusto = clsInfo.zcentrocustos;
            }
            nfcompra1_idcentrocusto = info.idcentrocusto;
            NFCompra1_tbxCentrocusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS WHERE ID= " + nfcompra1_idcentrocusto + " ","GER");
            if (info.idcfop == 0)
            {
                info.idcfop = clsInfo.zcfop;
            }
            nfcompra1_idcfop = info.idcfop;
            NFCompra1_tbxCfop.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CFOP from CFOP WHERE ID= " + nfcompra1_idcfop + " ");
            if (info.idcfopfis == 0)
            {
                info.idcfopfis = clsInfo.zcfop;
            }
            nfcompra1_idcfopfis = info.idcfopfis;
            NFCompra1_tbxCfopfis.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CFOP from CFOP WHERE ID= " + nfcompra1_idcfopfis + " ");
            if (info.idcodigo == 0)
            {
                info.idcodigo = clsInfo.zpecas;
            }
            nfcompra1_idcodigo = info.idcodigo;
            Fill_Pecas();
            //NFCompra1_tbxPecas_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS WHERE ID= " + nfcompra1_idcodigo + " ");
            //NFCompra1_tbxPecas_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE ID= " + nfcompra1_idcodigo + " ");
            //tipoproduto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select TIPOPRODUTO from PECAS WHERE ID= " + nfcompra1_idcodigo + " ");
            //NFCompra1_lbxTipoProduto.SelectedIndex = NFCompra1_lbxTipoProduto.FindString(tipoproduto);
            //if (NFCompra1_lbxTipoProduto.SelectedIndex == -1)
            //{
            //    NFCompra1_lbxTipoProduto.SelectedIndex = 0;
            //}
            if (info.idcodigo1 == 0)
            {
                info.idcodigo1 = clsInfo.zpecas;
            }
            nfcompra1_idcodigo1 = info.idcodigo1;
            NFCompra1_tbxPecas_codigo2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS WHERE ID= " + nfcompra1_idcodigo1 + " ");
            NFCompra1_tbxPecas_nome2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE ID= " + nfcompra1_idcodigo1 + " ");

            if (info.idcodigoctabil == 0)
            {
                info.idcodigoctabil = clsInfo.zcontacontabil;
            }
            nfcompra1_idcodigoctabil = info.idcodigoctabil;
            if (info.idcotacao == 0)
            {
                info.idcotacao = clsInfo.zcotacao;
            }
            nfcompra1_idcotacao = info.idcotacao;

            //nfcompra1_iddestino = info.iddestino;
            if (info.idhistorico == 0)
            {
                info.idhistorico = clsInfo.zhistoricos;
            }
            nfcompra1_idhistorico = info.idhistorico;
            NFCompra1_tbxHistoricos_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS WHERE ID= " + nfcompra1_idhistorico + " ");
            if (info.idipi == 0)
            {
                info.idipi = clsInfo.zipi;
            }
            nfcompra1_idipi = info.idipi;
            NFCompra1_tbxIpi_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from IPI WHERE ID= " + nfcompra1_idipi + " ");
            if (info.iditemcotacao == 0)
            {
                info.iditemcotacao = clsInfo.zcotacao1;
            }
            nfcompra1_iditemcotacao = info.iditemcotacao;
            if (info.iditempedido == 0)
            {
                info.iditempedido = clsInfo.zcompras1;
            }
            nfcompra1_iditempedido = info.iditempedido;
            if (info.iditempedidoentrega == 0)
            {
                info.iditempedidoentrega = clsInfo.zcomprasentrega;
            }
            nfcompra1_iditempedidoentrega = info.iditempedidoentrega;
            nfcompra1_idnfevales = info.idnfevales;
            if (info.idnfvendaitem == 0)
            {
                info.idnfvendaitem = clsInfo.znfvenda1;
            }
            nfcompra1_idnfvendaitem = info.idnfvendaitem;
            if (info.idnotafiscal == 0)
            {
                info.idnotafiscal = clsInfo.znfvenda;
            }
            nfcompra1_idnotafiscal = info.idnotafiscal;
            if (info.idordemservico == 0)
            {
                info.idordemservico = clsInfo.zordemservico;
            }

            nfcompra1_idordemservico = info.idordemservico;
            NFCompra1_tbxOrdemServico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from ORDEMSERVICO WHERE ID= " + nfcompra1_idordemservico + " ");
            if (info.idpedido == 0)
            {
                info.idpedido = clsInfo.zcompras;
            }
            nfcompra1_idpedido = info.idpedido;
            NFCompra1_tbxPedCompra.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from COMPRAS WHERE ID= " + nfcompra1_idpedido + " ")).ToString();
            if (clsParser.Int32Parse(NFCompra1_tbxPedCompra.Text) > 0)
            {
                NFCompra1_tbxPedCompraAno.Text = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ANO from COMPRAS WHERE ID= " + nfcompra1_idpedido + " ")).ToString();
            }
            else
            {
                NFCompra1_tbxPedCompraAno.Text = DateTime.Now.Year.ToString();
            }
            if (info.idpedidovenda == 0)
            {
                info.idpedidovenda = clsInfo.zpedido;
            }

            nfcompra1_idpedidovenda = info.idpedidovenda;
            NFCompra1_tbxPedVenda.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from PEDIDO WHERE ID= " + nfcompra1_idpedidovenda + " ");
            if (info.idpedidovendaitem == 0)
            {
                info.idpedidovendaitem = clsInfo.zpedido1;
            }
            nfcompra1_idpedidovendaitem = info.idpedidovendaitem;
            if (info.idsittriba == 0)
            {
                info.idsittriba = clsInfo.zsituacaotriba;
            }
            nfcompra1_idsittriba = info.idsittriba;
            String sitOrigem = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITTRIBUTARIAA WHERE ID= " + nfcompra1_idsittriba + " ");
            NFCompra1_cbxSittriba.SelectedIndex = NFCompra1_cbxSittriba.FindString(sitOrigem);
            if (NFCompra1_cbxSittriba.SelectedIndex == -1)
            {
                NFCompra1_cbxSittriba.SelectedIndex = 0;
            }

            if (info.idsittribb == 0)
            {
                info.idsittribb = clsInfo.zsituacaotribb;
            }
            nfcompra1_idsittribb = info.idsittribb;
            String sitICMS = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITTRIBUTARIAB WHERE ID= " + nfcompra1_idsittribb + " ");
            NFCompra1_cbxSittribb.SelectedIndex = NFCompra1_cbxSittribb.FindString(sitICMS);
            if (NFCompra1_cbxSittribb.SelectedIndex == -1)
            {
                NFCompra1_cbxSittribb.SelectedIndex = 0;
            }
            nfcompra1_idsittribcofins1 = info.idsittribcofins1;
            String sitcofins1 = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITTRIBCOFINS WHERE ID= " + nfcompra1_idsittribcofins1 + " ");
            NFCompra1_cbxSittribcofins1.SelectedIndex = NFCompra1_cbxSittribcofins1.FindString(sitcofins1);
            if (NFCompra1_cbxSittribcofins1.SelectedIndex == -1)
            {
                NFCompra1_cbxSittribcofins1.SelectedIndex = 0;
            }

            nfcompra1_idsittribipi = info.idsittribipi;
            String sitipi = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITTRIBIPI WHERE ID= " + nfcompra1_idsittribipi + " ");
            NFCompra1_cbxSittribipi.SelectedIndex = NFCompra1_cbxSittribipi.FindString(sitipi);
            if (NFCompra1_cbxSittribipi.SelectedIndex == -1)
            {
                NFCompra1_cbxSittribipi.SelectedIndex = 0;
            }
            nfcompra1_idsittribpis = info.idsittribpis;
            String sitpis = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITTRIBPIS WHERE ID= " + nfcompra1_idsittribpis + " ");
            NFCompra1_cbxSittribpis.SelectedIndex = NFCompra1_cbxSittribpis.FindString(sitipi);
            if (NFCompra1_cbxSittribpis.SelectedIndex == -1)
            {
                NFCompra1_cbxSittribpis.SelectedIndex = 0;
            }
            if (info.idsolicitacao == 0)
            {
                info.idsolicitacao = clsInfo.zsolicitacao;
            }
            nfcompra1_idsolicitacao = info.idsolicitacao;
            if (info.idtiponota == 0)
            {
                info.idtiponota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from tiponota where codigo='" + "0000-000" + "'"));
            }
            nfcompra1_idtiponota = info.idtiponota;
            String tiponota = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo + ' = ' + nome as [nome] from tiponota where id=" + nfcompra1_idtiponota + " ");
            NFCompra1_cbxTiponota.SelectedIndex = NFCompra1_cbxTiponota.FindString(tiponota);
            if (NFCompra1_cbxTiponota.SelectedIndex == -1)
            {
                NFCompra1_cbxTiponota.SelectedIndex = 0;
            }
            NFCompra1_cbxTiponota.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM TIPONOTA where id=" + nfcompra1_idtiponota);
            NFCompra1_cbxTiponota.Text += "=" + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM TIPONOTA where id=" + nfcompra1_idtiponota);

            nfcompra1_idtiponotasaida = info.idtiponotasaida;
            if (info.idunid == 0)
            {
                info.idunid = clsInfo.zunidade;
            }
            nfcompra1_idunid = info.idunid;
            NFCompra1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE WHERE ID= " + nfcompra1_idunid + " ");
            if (info.idunidfiscal == 0)
            {
                info.idunidfiscal = clsInfo.zunidade;
            }
            nfcompra1_idunidfiscal = info.idunidfiscal;
            NFCompra1_tbxUnidadeFiscal.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE WHERE ID= " + nfcompra1_idunidfiscal + " ");

            NFCompra1_tbxInss.Text = info.inss.ToString("N2");
            NFCompra1_tbxInssPorc.Text = info.inssporc.ToString("N2");
            if (info.inss > 0)
            {
                NFCompra1_ckxInss.Checked = true;
                NFCompra1_ckxInss.Text = "Sim Tem INSS";
            }
            else
            {
                NFCompra1_ckxInss.Checked = false;
                NFCompra1_ckxInss.Text = "Não Tem INSS";
            }

            NFCompra1_tbxIpi.Text = info.ipi.ToString("n2");

            NFCompra1_tbxIrrf.Text = info.irrf.ToString("N2");
            NFCompra1_tbxIrrfPorc.Text = info.irrfporc.ToString("N2");
            if (info.irrf > 0)
            {
                NFCompra1_ckxIrrf.Checked = true;
                NFCompra1_ckxIrrf.Text = "Sim Tem IRRF";
            }
            else
            {
                NFCompra1_ckxIrrf.Checked = false;
                NFCompra1_ckxIrrf.Text = "Não Tem IRRF";
            }
            NFCompra1_tbxIss.Text = info.iss.ToString("N2");
            NFCompra1_tbxIssPorc.Text = info.issporc.ToString("N2");
            if (info.iss > 0)
            {
                NFCompra1_ckxIss.Checked = true;
                NFCompra1_ckxIss.Text = "Sim Tem ISS";
            }
            else
            {
                NFCompra1_ckxIss.Checked = false;
                NFCompra1_ckxIss.Text = "Não Tem ISS";
            }
            nfcompra1_numero = info.numero;
            NFCompra1_tbxPeso.Text = info.peso.ToString("N3");
            NFCompra1_tbxPesoTotal.Text = info.pesototal.ToString("N3"); //info.pesototal;
            NFCompra1_tbxPis.Text = info.pis.ToString("N2");
            NFCompra1_tbxPisPorc.Text = info.pisporc.ToString("N2");
            if (info.pis > 0)
            {
                NFCompra1_ckxPis.Checked = true;
                NFCompra1_ckxPis.Text = "Sim Tem Pis";
            }
            else
            {
                NFCompra1_ckxPis.Checked = false;
                NFCompra1_ckxPis.Text = "Não Tem Pis";
            }
            NFCompra1_tbxPisCofinsCsll.Text = info.piscofinscsll.ToString("N2");
            NFCompra1_tbxPisCofinsCsllPorc.Text = info.piscofinscsllporc.ToString("N2");
            if (info.piscofinscsll > 0)
            {
                NFCompra1_ckxPisCofinsCsll.Checked = true;
                NFCompra1_ckxPisCofinsCsll.Text = "Sim Pis/Cofins/Csll";
            }
            else
            {
                NFCompra1_ckxPisCofinsCsll.Checked = false;
                NFCompra1_ckxPisCofinsCsll.Text = "Não Pis/Cofins/Csll";
            }
            NFCompra1_tbxPispasep.Text = info.pispasep.ToString("N2");

            NFCompra1_tbxPreco.Text = info.preco.ToString("N6");
            NFCompra1_tbxQtde.Text = info.qtde.ToString("N3");
            // info.qtdedev.ToString("N3");
            //info.qtdedevbaixa;
            //info.qtdedevsaldo;
            NFCompra1_tbxQtdeFiscal.Text = info.qtdefiscal.ToString("N3");
            NFCompra1_tbxQtdeTicket.Text = info.qtdeticket.ToString("N3");
            NFCompra1_tbxReducao.Text = info.reducao.ToString("N2");
            if (info.somaproduto == "S") { NFCompra1_ckxSomaproduto.Checked = true; } else { NFCompra1_ckxSomaproduto.Checked = false; }
            if (NFCompra1_ckxSomaproduto.Checked == true)
            {
                NFCompra1_ckxSomaproduto.Text = "Soma no Total NF";
            }
            else
            {
                NFCompra1_ckxSomaproduto.Text = "Não Soma no Total NF";
            }

            // info.situacao;
            if (info.tipodestino == "M")
            {
                NFCompra1_rbnTipodestinoMaq.Checked = true;
            }
            else
            {
                NFCompra1_rbnTipodestinoIte.Checked = true;
            }
            NFCompra1_lbxTipoProduto.SelectedIndex = NFCompra1_lbxTipoProduto.FindString(info.tipoproduto);
            if (NFCompra1_lbxTipoProduto.SelectedIndex == -1)
            {
                NFCompra1_lbxTipoProduto.SelectedIndex = 0;
            }
            NFCompra1_tbxTotalmercado.Text = info.totalmercado.ToString("N2");
            NFCompra1_tbxTotalnota.Text = info.totalnota.ToString("N2");
            NFCompra1_tbxValorfrete.Text = info.valorfrete.ToString("N2");
            if (info.valorfreteicms == "S")
            {
                NFCompra1_ckxValorFreteIcms.Checked = true;
                NFCompra1_ckxValorFreteIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                NFCompra1_ckxValorFreteIcms.Text = "Não Incide ICMS";
            }
            NFCompra1_tbxValoroutros.Text = info.valoroutras.ToString("N2");
            if (info.valoroutrasicms == "S")
            {
                NFCompra1_ckxValorOutrasIcms.Checked = true;
                NFCompra1_ckxValorOutrasIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                NFCompra1_ckxValorOutrasIcms.Text = "Não Incide ICMS";
            }

            NFCompra1_tbxValorseguro.Text = info.valorseguro.ToString("N2");
            if (info.valorseguroicms == "S")
            {
                NFCompra1_ckxValorSeguroIcms.Checked = true;
                NFCompra1_ckxValorSeguroIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                NFCompra1_ckxValorSeguroIcms.Text = "Não Incide ICMS";
            }

            //info.vidautil;

            if (Item_gbxProduto.Enabled == true)
            {
                NFCompra1_tbxPecas_codigo.Select();
            }

            // IMPOSTOS DE MÃO DE OBRA
            if (NFCompra1_lbxTipoProduto.Text.Substring(0, 2) == "30")
            {
                gbxPrestacaoServico.Enabled = true;
            }
            else
            {
                gbxPrestacaoServico.Enabled = false;
            }

            NFCompra1_tbxPrecoCustounit.Text = (clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoravista from pecaspreco where IDcodigo= " + nfcompra1_idcodigo + " ORDER BY DATA DESC"))).ToString("N4");
            if (clsParser.DecimalParse(NFCompra1_tbxPrecoCustounit.Text) > 0 & clsParser.DecimalParse(NFCompra1_tbxPreco.Text) > 0)
            {   //Calcular Lucratividade
                NFCompra1_tbxMargemLucro.Text = (clsParser.DecimalParse(NFCompra1_tbxPrecoCustounit.Text) / clsParser.DecimalParse(NFCompra1_tbxPreco.Text)).ToString("N4");

            }

            //NFCompra1SomaTotal();
        }

        private void NFCompra1FillInfo(clsNfCompra1Info info)
        {
            info.id = nfcompra1_id;
            info.numero = nfcompra1_numero; // id

            info.aliqcofins1 = clsParser.DecimalParse(NFCompra1_tbxAliqcofins1.Text);
            info.aliqpispasep = clsParser.DecimalParse(NFCompra1_tbxAliqpispasep.Text);
            info.baseicm = clsParser.DecimalParse(NFCompra1_tbxBaseicm.Text);
            info.baseicmsubst = clsParser.DecimalParse(NFCompra1_tbxBaseicmsubst.Text);
            info.basemp = clsParser.DecimalParse(NFCompra1_tbxBasemp.Text);
            info.bccofins1 = clsParser.DecimalParse(NFCompra1_tbxBccofins1.Text);
            info.bcipi = clsParser.DecimalParse(NFCompra1_tbxBcipi.Text);
            info.bcpispasep = clsParser.DecimalParse(NFCompra1_tbxPispasep.Text);
            if (NFCompra1_ckxCalculoautomatico.Checked == true)
            {
                info.calculoautomatico = "S";
                NFCompra1_ckxCalculoautomatico.Text = "Calculo Automático";
            }
            else
            {
                info.calculoautomatico = "N";
                NFCompra1_ckxCalculoautomatico.Text = "Calculo Manual";
            }
            info.codigoemp01 = NFCompra1_tbxCodigoemp01.Text;
            info.codigoemp02 = NFCompra1_tbxCodigoemp02.Text;
            info.codigoemp03 = NFCompra1_tbxCodigoemp03.Text;
            info.codigoemp04 = NFCompra1_tbxCodigoemp04.Text;
            info.cofins = clsParser.DecimalParse(NFCompra1_tbxCofins.Text);
            info.cofins1 = clsParser.DecimalParse(NFCompra1_tbxCofins1.Text);
            info.cofinsporc = clsParser.DecimalParse(NFCompra1_tbxCofinsPorc.Text);
            info.complemento = NFCompra1_tbxComplemento.Text;
            info.complemento1 = NFCompra1_tbxComplemento1.Text;
            info.consumo = NFCompra1_cbxConsumo.Text.Substring(0, 1);
            // info.creditaricm
            info.csll = clsParser.DecimalParse(NFCompra1_tbxCsll.Text);
            info.csllporc = clsParser.DecimalParse(NFCompra1_tbxCsllPorc.Text);
            info.custoicm = clsParser.DecimalParse(NFCompra1_tbxCustoicm.Text);
            info.custoipi = clsParser.DecimalParse(NFCompra1_tbxCustoipi.Text);
            info.datatabela = DateTime.Parse(NFCompra1_tbxDataTabela.Text);
            //info.descricaoesp
            // info.fatorconv.ToString("N6");
            //   ==================== NFCompra1_tbxHistoricos_codigo =
            if (NFCompra1_ckxFatura.Checked == true)
            {
                info.fatura = "S";
            }
            else
            {
                info.fatura = "N";
            }
            info.faturando = "N";
            info.icm = clsParser.DecimalParse(NFCompra1_tbxIcm.Text);
            // info.icminterno.ToString("N2");
            info.icmssubstreducao = clsParser.DecimalParse(NFCompra1_tbxIcmssubstreducao.Text);
            info.idcentrocusto = nfcompra1_idcentrocusto;
            info.idcfop = nfcompra1_idcfop;
            info.idcfopfis = nfcompra1_idcfopfis;
            info.idcodigo = nfcompra1_idcodigo;
            info.idcodigo1 = nfcompra1_idcodigo1;
            info.idcodigoctabil = nfcompra1_idcodigoctabil;
            info.idcotacao = nfcompra1_idcotacao;
            info.iddestino = nfcompra1_iddestino;
            info.idhistorico = nfcompra1_idhistorico;
            info.idipi = nfcompra1_idipi;
            info.iditemcotacao = nfcompra1_iditemcotacao;
            info.iditempedido = nfcompra1_iditempedido;
            info.iditempedidoentrega = nfcompra1_iditempedidoentrega;
            info.idnfevales = nfcompra1_idnfevales;
            info.idnfvendaitem = nfcompra1_idnfvendaitem;
            info.idnotafiscal = nfcompra1_idnotafiscal;
            info.idordemservico = nfcompra1_idordemservico;
            info.idpedido = nfcompra1_idpedido;
            info.idpedidovenda = nfcompra1_idpedidovenda;
            info.idpedidovendaitem = nfcompra1_idpedidovendaitem;
            info.idsittriba = nfcompra1_idsittriba;
            info.idsittribb = nfcompra1_idsittribb;
            info.idsittribcofins1 = nfcompra1_idsittribcofins1;
            info.idsittribipi = nfcompra1_idsittribipi;
            info.idsittribpis = nfcompra1_idsittribpis;
            info.idsolicitacao = nfcompra1_idsolicitacao;
            info.idtiponota = nfcompra1_idtiponota;
            info.idtiponotasaida = nfcompra1_idtiponotasaida;
            info.idunid = nfcompra1_idunid;
            info.idunidfiscal = nfcompra1_idunidfiscal;
            info.irrfporc = clsParser.DecimalParse(NFCompra1_tbxIrrfPorc.Text);
            info.irrf = clsParser.DecimalParse(NFCompra1_tbxIrrf.Text);
            info.inss = clsParser.DecimalParse(NFCompra1_tbxInss.Text);
            info.inssporc = clsParser.DecimalParse(NFCompra1_tbxInssPorc.Text);
            info.iss = clsParser.DecimalParse(NFCompra1_tbxIss.Text);
            info.issporc = clsParser.DecimalParse(NFCompra1_tbxIssPorc.Text);
            info.ipi = clsParser.DecimalParse(NFCompra1_tbxIpi.Text);
            info.peso = clsParser.DecimalParse(NFCompra1_tbxPeso.Text);
            info.pesototal = clsParser.DecimalParse(NFCompra1_tbxPesoTotal.Text);
            info.pis = clsParser.DecimalParse(NFCompra1_tbxPis.Text);
            info.piscofinscsll = clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsll.Text);
            info.piscofinscsllporc = clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsllPorc.Text);
            info.pispasep = clsParser.DecimalParse(NFCompra1_tbxPispasep.Text);
            info.pisporc = clsParser.DecimalParse(NFCompra1_tbxPisPorc.Text);

            info.preco = clsParser.DecimalParse(NFCompra1_tbxPreco.Text);
            info.qtde = clsParser.DecimalParse(NFCompra1_tbxQtde.Text);
            // info.qtdedev.ToString("N3");
            //info.qtdedevbaixa;
            //info.qtdedevsaldo;
            info.qtdefiscal = clsParser.DecimalParse(NFCompra1_tbxQtdeFiscal.Text);
            info.qtdeticket = clsParser.DecimalParse(NFCompra1_tbxQtdeTicket.Text);
            info.reducao = clsParser.DecimalParse(NFCompra1_tbxReducao.Text);
            if (NFCompra1_ckxSomaproduto.Checked == true)
            {
                info.somaproduto = "S";
                NFCompra1_ckxSomaproduto.Text = "Soma no Total NF";
            }
            else
            {
                info.somaproduto = "N";
                NFCompra1_ckxSomaproduto.Text = "Não Soma no Total NF";
            }


            // info.situacao;
            if (NFCompra1_rbnTipodestinoMaq.Checked == true)
            {
                info.tipodestino = "M";
            }
            else
            {
                info.tipodestino = "I";  //NFCompra1_rbnTipodestinoIte.Checked = true;
            }
            info.tipoproduto = NFCompra1_lbxTipoProduto.Text.Substring(0, 2);
            info.totalmercado = clsParser.DecimalParse(NFCompra1_tbxTotalmercado.Text);
            info.totalnota = clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text);
            info.valorfrete = clsParser.DecimalParse(NFCompra1_tbxValorfrete.Text);
            if (NFCompra1_ckxValorFreteIcms.Checked == true)
            {
                info.valorfreteicms = "S";
                NFCompra1_ckxValorFreteIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                info.valorfreteicms = "S";
                NFCompra1_ckxValorFreteIcms.Text = "Não Incide ICMS";
            }
            info.valoroutras = clsParser.DecimalParse(NFCompra1_tbxValoroutros.Text);
            if (NFCompra1_ckxValorOutrasIcms.Checked == true)
            {
                info.valoroutrasicms = "S";
                NFCompra1_ckxValorOutrasIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                info.valoroutrasicms = "S";
                NFCompra1_ckxValorOutrasIcms.Text = "Não Incide ICMS";
            }
            info.valorseguro = clsParser.DecimalParse(NFCompra1_tbxValorseguro.Text);
            if (NFCompra1_ckxValorSeguroIcms.Checked == true)
            {
                info.valorseguroicms = "S";
                NFCompra1_ckxValorSeguroIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                info.valorseguroicms = "N";
                NFCompra1_ckxValorSeguroIcms.Text = "Não Incide ICMS";
            }
            //info.vidautil;
        }

        private void NFCompra1FillInfoToGrid(DataTable dt, clsNfCompra1Info info)
        {
            DataRow row;
            DataRow[] rows = dt.Select("nfcompra1_posicao = " + nfcompra1_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dt.NewRow();
            }

            row["id"] = info.id;
            row["aliqcofins1"] = info.aliqcofins1;
            row["aliqpispasep"] = info.aliqpispasep;
            row["baseicm"] = info.baseicm;
            row["baseicmsubst"] = info.baseicmsubst;
            row["basemp"] = info.basemp;
            row["bccofins1"] = info.bccofins1;
            row["bcipi"] = info.bcipi;
            row["bcpispasep"] = info.bcpispasep;
            row["calculoautomatico"] = info.calculoautomatico;
            row["codigoemp01"] = info.codigoemp01;
            row["codigoemp02"] = info.codigoemp02;
            row["codigoemp03"] = info.codigoemp03;
            row["codigoemp04"] = info.codigoemp04;
            row["cofins"] = info.cofins;
            row["cofins1"] = info.cofins1;
            row["cofinsporc"] = info.cofinsporc;
            row["complemento"] = info.complemento;
            row["complemento1"] = info.complemento1;
            row["consumo"] = info.consumo;
            row["creditaricm"] = info.creditaricm;
            row["csll"] = info.csll;
            row["csllporc"] = info.csllporc;
            row["custoicm"] = info.custoicm;
            row["custoipi"] = info.custoipi;
            row["datatabela"] = info.datatabela;
            row["descricaoesp"] = info.descricaoesp;
            row["fatorconv"] = info.fatorconv;
            row["fatura"] = info.fatura;
            row["faturando"] = info.faturando;
            row["icm"] = info.icm;
            row["icminterno"] = info.icminterno;
            row["icmsubst"] = info.icmsubst;
            row["icmssubstreducao"] = info.icmssubstreducao;
            row["idcentrocusto"] = info.idcentrocusto;
            row["idcfop"] = info.idcfop;
            row["idcfopfis"] = info.idcfopfis;
            row["idcodigo"] = info.idcodigo;
            row["idcodigo1"] = info.idcodigo1;
            row["idcodigoctabil"] = info.idcodigoctabil;
            row["idcotacao"] = info.idcotacao;
            row["iddestino"] = info.iddestino;
            row["idhistorico"] = info.idhistorico;
            row["idipi"] = info.idipi;
            row["iditemcotacao"] = info.iditemcotacao;
            row["iditempedido"] = info.iditempedido;
            row["iditempedidoentrega"] = info.iditempedidoentrega;
            row["idnfevales"] = info.idnfevales;
            row["idnfvendaitem"] = info.idnfvendaitem;
            row["idnotafiscal"] = info.idnotafiscal;
            row["idordemservico"] = info.idordemservico;
            row["idpedido"] = info.idpedido;
            row["idpedidovenda"] = info.idpedidovenda;
            row["idpedidovendaitem"] = info.idpedidovendaitem;
            row["idsittriba"] = info.idsittriba;
            row["idsittribb"] = info.idsittribb;

            //row["idsittribcofins"] = info.idsittribcofins;
            row["idsittribcofins1"] = info.idsittribcofins1;
            row["idsittribipi"] = info.idsittribipi;
            row["idsittribpis"] = info.idsittribpis;
            row["idsolicitacao"] = info.idsolicitacao;
            row["idtiponota"] = info.idtiponota;
            row["idtiponotasaida"] = info.idtiponotasaida;
            row["idunid"] = info.idunid;
            row["idunidfiscal"] = info.idunidfiscal;
            row["inss"] = info.inss;
            row["inssporc"] = info.inssporc;
            row["ipi"] = info.ipi;
            row["irrf"] = info.irrf;
            row["irrfporc"] = info.irrfporc;
            row["iss"] = info.iss;
            row["issporc"] = info.issporc;
            row["msg"] = info.msg;
            row["numero"] = info.numero;
            row["peso"] = info.peso;
            row["pesototal"] = info.pesototal;
            row["pis"] = info.pis;
            row["piscofinscsll"] = info.piscofinscsll;
            row["piscofinscsllporc"] = info.piscofinscsllporc;
            row["pispasep"] = info.pispasep;
            row["pisporc"] = info.pisporc;
            row["preco"] = info.preco;
            row["qtde"] = info.qtde;
            row["qtdedev"] = info.qtdedev;
            row["qtdedevbaixa"] = info.qtdedevbaixa;
            row["qtdedevsaldo"] = info.qtdedevsaldo;
            row["qtdefiscal"] = info.qtdefiscal;
            row["qtdeticket"] = info.qtdeticket;
            row["reducao"] = info.reducao;
            row["situacao"] = info.situacao;
            row["somaproduto"] = info.somaproduto;
            row["tipodestino"] = info.tipodestino;
            row["tipoproduto"] = info.tipoproduto;
            row["totalmercado"] = info.totalmercado;
            row["totalnota"] = info.totalnota;
            row["valorfrete"] = info.valorfrete;
            row["valorfreteicms"] = info.valorfreteicms;
            row["valoroutras"] = info.valoroutras;
            row["valoroutrasicms"] = info.valoroutrasicms;
            row["valorseguro"] = info.valorseguro;
            row["valorseguroicms"] = info.valorseguroicms;
            row["vidautil"] = info.vidautil;

            // Colunas que petencem a outras tabelas
            row["PEDIDO"] = clsParser.Int32Parse(NFCompra1_tbxPedCompra.Text);
            row["CFOPFIS"] = NFCompra1_tbxCfopfis.Text;
            row["unidfiscal"] = NFCompra1_tbxUnidadeFiscal.Text;
            row["unid"] = NFCompra1_tbxUnidade.Text;
            row["codigo"] = NFCompra1_tbxPecas_codigo.Text;
            //            row["nome"] = NFCompra1_tbxPecas_nome.Text;
            if (NFCompra1_tbxPecas_codigo.Text == "0")
            {
                row["DESCRICAO"] = NFCompra1_tbxComplemento.Text;
            }
            else
            {
                row["DESCRICAO"] = NFCompra1_tbxPecas_nome.Text;
            }

            row["nroos"] = clsParser.Int32Parse(NFCompra1_tbxOrdemServico.Text);
            row["nropv"] = clsParser.Int32Parse(NFCompra1_tbxPedVenda.Text);

            if (rows.Length == 0)
            {
                row["nfcompra1_posicao"] = nfcompra1_posicao;
                dt.Rows.Add(row);
            }

            NFCompraSomar();
        }

        private void dgvNFCompra1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspNFCompra1ItensAlterar.PerformClick();
        }

        private void NFCompra1_btnIdCodigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnIdCodigo.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(nfcompra1_idcodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void NFCompra1_btnIdTipoNota_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnIdTipoNota.Name;
            frmTiponotaPes frmTiponotaPes = new frmTiponotaPes();
            frmTiponotaPes.Init(nfcompra1_idtiponota);

            clsFormHelper.AbrirForm(this.MdiParent, frmTiponotaPes, clsInfo.conexaosqldados);
        }

        private void NFCompra1_btnIdcfopFiscal_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnIdcfopFiscal.Name;
            frmCfopPes frmCfopPes = new frmCfopPes();
            frmCfopPes.Init(nfcompra1_idcfopfis);

            clsFormHelper.AbrirForm(this.MdiParent, frmCfopPes, clsInfo.conexaosqldados);
        }

        private void NFCompra1_btnIdcfop_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnIdcfop.Name;
            frmCfopPes frmCfopPes = new frmCfopPes();
            frmCfopPes.Init(nfcompra1_idcfop);

            clsFormHelper.AbrirForm(this.MdiParent, frmCfopPes, clsInfo.conexaosqldados);
        }

        private void NFCompra1_btnSitTriba_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnSitTriba.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBUTARIAA", nfcompra1_idsittriba, "Situação Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void NFCompra1_btnSitTribb_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnSitTribb.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBUTARIAB", nfcompra1_idsittribb, "Situação Tributaria");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void NFCompra1_btnCentrocusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnCentrocusto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", nfcompra1_idcentrocusto, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void NFCompra1_btnHistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnHistorico.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", nfcompra1_idhistorico, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void NFCompra1_btnIdCodigo2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnIdCodigo2.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

        }

        private void NFCompra1_btnUnidadeFis_codigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnUnidadeFis_codigo.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "UNIDADE", nfcompra1_idunidfiscal, "Unidade");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);


        }

        private void NFCompra1_btnUnidade_codigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnUnidade_codigo.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "UNIDADE", nfcompra1_idunid, "Unidade");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }
        private void NFCompra1_btnClassfiscal_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnClassfiscal.Name;
            frmIpiPes frmIpiPes = new frmIpiPes();
            frmIpiPes.Init(nfcompra1_idipi);

            clsFormHelper.AbrirForm(this.MdiParent, frmIpiPes, clsInfo.conexaosqldados);
        }


        private void NFCompra1_tbxTotalmercado_TextChanged(object sender, EventArgs e)
        {

        }

        private void NFCompra1_btnSittribipi_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnSittribipi.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBIPI", nfcompra1_idsittribipi, "Situação IPI");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }
        private void NFCompra1_btnSittribpis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnSittribpis.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBPIS", nfcompra1_idsittribpis, "Pis");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void NFCompra1_btnSittribcofins1_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompra1_btnSittribcofins1.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITTRIBCOFINS", nfcompra1_idsittribcofins1, "Cofins");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }
        private void NFCompraSomar()
        {
            //
            tbxTotalbaseicm.Text = "0";
            tbxTotalbaseicmsubst.Text = "0";
            tbxCofins.Text = "0";
            tbxTotalcofins1.Text = "0";
            tbxCsll.Text = "0";
            tbxTotalicm.Text = "0";
            tbxTotalipi.Text = "0";
            // info.desconto = clsParser.DecimalParse(row["desconto"].ToString());
            tbxInss.Text = "0";
            tbxTotalipi.Text = "0";
            tbxIrrf.Text = "0";
            tbxIss.Text = "0";
            tbxPis.Text = "0";
            tbxPisCofinsCsll.Text = "0";
            tbxTotalpispasep.Text = "0";
            tbxTotalmercadoria.Text = "0";
            tbxTotalNotaFiscal.Text = "0";
            tbxTotalPeso.Text = "0";
            tbxTotalfrete.Text = "0";
            tbxTotalOutras.Text = "0";
            tbxTotalseguro.Text = "0";
            tbxTotalFatura.Text = "0";

            foreach (DataRow row in dtNFCompra1.Rows)
            {
                if (row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Detached)
                {
                    continue;   // não somar se apagou
                }
                else
                {
                    // Somar os campos do cabeçalho
                    tbxTotalbaseicm.Text = (clsParser.DecimalParse(tbxTotalbaseicm.Text) + clsParser.DecimalParse(row["baseicm"].ToString())).ToString("N2");
                    tbxTotalbaseicmsubst.Text = (clsParser.DecimalParse(tbxTotalbaseicmsubst.Text) + clsParser.DecimalParse(row["baseicmsubst"].ToString())).ToString("N2");
                    tbxCofins.Text = (clsParser.DecimalParse(tbxCofins.Text) + clsParser.DecimalParse(row["cofins"].ToString())).ToString("N2");
                    tbxTotalcofins1.Text = (clsParser.DecimalParse(tbxTotalcofins1.Text) + clsParser.DecimalParse(row["cofins1"].ToString())).ToString("N2");
                    tbxCsll.Text = (clsParser.DecimalParse(tbxCsll.Text) + clsParser.DecimalParse(row["csll"].ToString())).ToString("N2");
                    tbxTotalicm.Text = (clsParser.DecimalParse(tbxTotalicm.Text) + clsParser.DecimalParse(row["custoicm"].ToString())).ToString("N2");
                    tbxTotalipi.Text = (clsParser.DecimalParse(tbxTotalipi.Text) + clsParser.DecimalParse(row["custoipi"].ToString())).ToString("N2");
                    // info.desconto = (clsParser.DecimalParse(row["desconto"].ToString());
                    tbxInss.Text = (clsParser.DecimalParse(tbxInss.Text) + clsParser.DecimalParse(row["inss"].ToString())).ToString("N2");
                    tbxIrrf.Text = (clsParser.DecimalParse(tbxIrrf.Text) + clsParser.DecimalParse(row["irrf"].ToString())).ToString("N2");
                    tbxIss.Text = (clsParser.DecimalParse(tbxIss.Text) + clsParser.DecimalParse(row["iss"].ToString())).ToString("N2");
                    tbxPis.Text = (clsParser.DecimalParse(tbxPis.Text) + clsParser.DecimalParse(row["pis"].ToString())).ToString("N2");
                    tbxPisCofinsCsll.Text = (clsParser.DecimalParse(tbxPisCofinsCsll.Text) + clsParser.DecimalParse(row["piscofinscsll"].ToString())).ToString("N2");
                    tbxTotalpispasep.Text = (clsParser.DecimalParse(tbxTotalpispasep.Text) + clsParser.DecimalParse(row["pispasep"].ToString())).ToString("N2");
                    tbxTotalPeso.Text = (clsParser.DecimalParse(tbxTotalPeso.Text) + clsParser.DecimalParse(row["pesototal"].ToString())).ToString("N3");
                    tbxTotalmercadoria.Text = (clsParser.DecimalParse(tbxTotalmercadoria.Text) + clsParser.DecimalParse(row["totalmercado"].ToString())).ToString("N2");
                    tbxTotalNotaFiscal.Text = (clsParser.DecimalParse(tbxTotalNotaFiscal.Text) + clsParser.DecimalParse(row["totalnota"].ToString())).ToString("N2");
                    tbxTotalfrete.Text = (clsParser.DecimalParse(tbxTotalfrete.Text) + clsParser.DecimalParse(row["valorfrete"].ToString())).ToString("N2");
                    tbxTotalOutras.Text = (clsParser.DecimalParse(tbxTotalOutras.Text) + clsParser.DecimalParse(row["valoroutras"].ToString())).ToString("N2");
                    tbxTotalseguro.Text = (clsParser.DecimalParse(tbxTotalseguro.Text) + clsParser.DecimalParse(row["valorseguro"].ToString())).ToString("N2");
                    if (row["fatura"].ToString() == "S")
                    {
                        tbxTotalFatura.Text = (clsParser.DecimalParse(tbxTotalFatura.Text) +
                                               clsParser.DecimalParse(row["totalnota"].ToString()) -
                                               clsParser.DecimalParse(row["inss"].ToString()) -
                                               clsParser.DecimalParse(row["irrf"].ToString()) -
                                               clsParser.DecimalParse(row["iss"].ToString()) -
                                               clsParser.DecimalParse(row["pis"].ToString()) -
                                               clsParser.DecimalParse(row["csll"].ToString()) -
                                               clsParser.DecimalParse(row["cofins"].ToString()) -
                                               clsParser.DecimalParse(row["piscofinscsll"].ToString())).ToString("N2");
                    }
                }
            }

            tbxTotalPesoBruto.Text = tbxTotalPeso.Text;
        }
        
        private void NFCompra1SomaTotal()
        {
            // qtde
            NFCompra1_tbxQtdeFiscal.Text = clsParser.DecimalParse(NFCompra1_tbxQtdeFiscal.Text).ToString("N3");
            NFCompra1_tbxQtde.Text = clsParser.DecimalParse(NFCompra1_tbxQtde.Text).ToString("N3");
            NFCompra1_tbxFatorConversao.Text = clsParser.DecimalParse(NFCompra1_tbxFatorConversao.Text).ToString("N6");
            if (clsParser.DecimalParse(NFCompra1_tbxFatorConversao.Text) > 0)
            {
                NFCompra1_tbxQtde.Text = (clsParser.DecimalParse(NFCompra1_tbxQtdeFiscal.Text) * clsParser.DecimalParse(NFCompra1_tbxFatorConversao.Text)).ToString("N3");
            }
            else
            {
                if (clsParser.DecimalParse(NFCompra1_tbxQtde.Text) == 0)
                {
                    NFCompra1_tbxQtde.Text = clsParser.DecimalParse(NFCompra1_tbxQtdeFiscal.Text).ToString("N3");
                }
            }

            // peso
            NFCompra1_tbxPeso.Text = clsParser.DecimalParse(NFCompra1_tbxPeso.Text).ToString("N3");
            NFCompra1_tbxPesoTotal.Text = (clsParser.DecimalParse(NFCompra1_tbxPeso.Text) * clsParser.DecimalParse(NFCompra1_tbxQtdeFiscal.Text)).ToString("N3"); 
            //preço
            NFCompra1_tbxPreco.Text = clsParser.DecimalParse(NFCompra1_tbxPreco.Text).ToString("N6");
            NFCompra1_tbxTotalmercado.Text = (clsParser.DecimalParse(NFCompra1_tbxPreco.Text) * clsParser.DecimalParse(NFCompra1_tbxQtdeFiscal.Text)).ToString("N2");
            //preço que entra no estoque e faz custo
            //if (NFCompra1_ckxCreditarIcm.Checked == true)
            //{
            //    NFCompra1_ckxCreditarIcm.Text = "Sim Creditar ICM's+Pis+Cofins";
            //    if (clsParser.DecimalParse(NFCompra1_tbxPreco.Text) > 0)
            //    {
            //        Double indice = 0;
            //        indice = Double.Parse(clsInfo.zfiscalpis.ToString()) + Double.Parse(clsInfo.zfiscalcofins.ToString()) + Double.Parse(NFCompra1_tbxIcm.Text);
            //        if (indice > 0)
            //        {
            //            NFCompra1_tbxPrecoCustounit.Text = (Double.Parse(NFCompra1_tbxPreco.Text) * ((100 - indice) / 100)).ToString("N6");
            //        }
            //    }
            //}
            //else
            //{
            //    NFCompra1_ckxCreditarIcm.Text = "Não Creditar ICM's+Pis+Cofins";
            //    NFCompra1_tbxPrecoCustounit.Text = clsParser.DecimalParse(NFCompra1_tbxPreco.Text).ToString("N6");
            //}
            // ipi
            if (clsParser.DecimalParse(NFCompra1_tbxIpi.Text) > 0)
            {
                NFCompra1_tbxBcipi.Text = clsParser.DecimalParse(NFCompra1_tbxTotalmercado.Text).ToString("N2");
                NFCompra1_tbxCustoipi.Text = (clsParser.DecimalParse(NFCompra1_tbxBcipi.Text) * (clsParser.DecimalParse(NFCompra1_tbxIpi.Text) / 100)).ToString("N2");
            }
            else
            {
                NFCompra1_tbxIpi.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                NFCompra1_tbxBcipi.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                NFCompra1_tbxCustoipi.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            }
            // icms
//            NFCompra1SomaICM();
            // icms subst
            NFCompra1_tbxBaseicmsubst.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            NFCompra1_tbxIcminterno.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            NFCompra1_tbxIcmssubstreducao.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            NFCompra1_tbxReducao.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            NFCompra1_tbxIcmsubst.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            // pispasep 
            NFCompra1_tbxBcpispasep.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            NFCompra1_tbxAliqpispasep.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            NFCompra1_tbxPispasep.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            //cofins
            NFCompra1_tbxBccofins1.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            NFCompra1_tbxAliqcofins1.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            NFCompra1_tbxCofins1.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            // valor outras
            NFCompra1_tbxValoroutros.Text = clsParser.DecimalParse(NFCompra1_tbxValoroutros.Text).ToString("N2");
            NFCompra1_tbxValorfrete.Text = clsParser.DecimalParse(NFCompra1_tbxValorfrete.Text).ToString("N2");
            NFCompra1_tbxValorseguro.Text = clsParser.DecimalParse(NFCompra1_tbxValorseguro.Text).ToString("N2");
            //if (NFCompra1_ckxValorOutrasIcms.Checked == true)
            //{
            //    NFCompra1SomaICM();
            //}
            //if (NFCompra1_ckxValorFreteIcms.Checked == true)
            //{
            //    NFCompra1SomaICM();
            //}
            //if (NFCompra1_ckxValorSeguroIcms.Checked == true)
            //{
            //    NFCompra1SomaICM();
            //}
            
            // total Nota
            NFCompra1_tbxTotalnota.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalmercado.Text) +
                                           clsParser.DecimalParse(NFCompra1_tbxValoroutros.Text) +
                                           clsParser.DecimalParse(NFCompra1_tbxValorfrete.Text) +
                                           clsParser.DecimalParse(NFCompra1_tbxValorseguro.Text) +
                                           clsParser.DecimalParse(NFCompra1_tbxCustoipi.Text)).ToString("N2");

            // Somar Impostos Prestação de Serviços
            // prestação de serviços (impostos)
            //Decimal por = clsParser.DecimalParse(clsNfCompra1Info.irrfporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.inssporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.piscofinscsllporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.issporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.pisporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.cofinsporc.ToString()) + clsParser.DecimalParse(clsNfCompra1Info.csllporc.ToString());
            //if (NFCompra1_cbxTipoEntrada.Text.PadRight(2, ' ').Substring(0, 2) == "30")
            //{
            //    if (por == 0)
            //    {
            //        clsNfCompra1Info.irrfporc = clsInfo.zfiscalirrf;
            //        clsNfCompra1Info.inssporc = clsInfo.zfiscalinss;
            //        clsNfCompra1Info.piscofinscsllporc = clsInfo.zfiscalpiscofins;
            //        clsNfCompra1Info.issporc = clsInfo.zfiscaliss;
            //        clsNfCompra1Info.pisporc = clsInfo.zfiscalpis;
            //        clsNfCompra1Info.cofinsporc = clsInfo.zfiscalcofins;
            //        clsNfCompra1Info.csllporc = clsInfo.zfiscalcsll;
            //        NFCompra1_tbxIrrfPorc.Text = clsNfCompra1Info.irrfporc.ToString("N2");
            //        NFCompra1_tbxInssPorc.Text = clsNfCompra1Info.inssporc.ToString("N2");
            //        NFCompra1_tbxPisCofinsCsllPorc.Text = clsNfCompra1Info.piscofinscsllporc.ToString("N2");
            //        NFCompra1_tbxIssPorc.Text = clsNfCompra1Info.issporc.ToString("N2");
            //        NFCompra1_tbxPisPorc.Text = clsNfCompra1Info.pisporc.ToString("N2");
            //        NFCompra1_tbxCofinsPorc.Text = clsNfCompra1Info.cofinsporc.ToString("N2");
            //        NFCompra1_tbxCsllPorc.Text = clsNfCompra1Info.csllporc.ToString("N2");
            //    }
            //}

            NFCompra1_tbxIrrfPorc.Text = clsParser.DecimalParse(NFCompra1_tbxIrrfPorc.Text).ToString("N2");
            NFCompra1_tbxIrrf.Text = clsParser.DecimalParse(NFCompra1_tbxIrrf.Text).ToString("N2");
            NFCompra1_tbxInssPorc.Text = clsParser.DecimalParse(NFCompra1_tbxInssPorc.Text).ToString("N2");
            NFCompra1_tbxInss.Text = clsParser.DecimalParse(NFCompra1_tbxInss.Text).ToString("N2");
            NFCompra1_tbxPisCofinsCsllPorc.Text = clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsllPorc.Text).ToString("N2");
            NFCompra1_tbxPisCofinsCsll.Text = clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsll.Text).ToString("N2");
            NFCompra1_tbxIssPorc.Text = clsParser.DecimalParse(NFCompra1_tbxIssPorc.Text).ToString("N2");
            NFCompra1_tbxIss.Text = clsParser.DecimalParse(NFCompra1_tbxIss.Text).ToString("N2");
            NFCompra1_tbxPisPorc.Text = clsParser.DecimalParse(NFCompra1_tbxPisPorc.Text).ToString("N2");
            NFCompra1_tbxPis.Text = clsParser.DecimalParse(NFCompra1_tbxPis.Text).ToString("N2");
            NFCompra1_tbxCofinsPorc.Text = clsParser.DecimalParse(NFCompra1_tbxCofinsPorc.Text).ToString("N2");
            NFCompra1_tbxCofins.Text = clsParser.DecimalParse(NFCompra1_tbxCofins.Text).ToString("N2");
            NFCompra1_tbxCsllPorc.Text = clsParser.DecimalParse(NFCompra1_tbxCsllPorc.Text).ToString("N2");
            NFCompra1_tbxCsll.Text = clsParser.DecimalParse(NFCompra1_tbxCsll.Text).ToString("N2");

            if (NFCompra1_ckxIrrf.Checked == true)
            {
                NFCompra1_ckxIrrf.Text = "Sim Tem I.R.R.F.";
                if (clsParser.DecimalParse(NFCompra1_tbxIrrfPorc.Text) > 0)
                {
                    NFCompra1_tbxIrrf.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxIrrfPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                NFCompra1_ckxIrrf.Text = "Não Tem I.R.R.F.";
                NFCompra1_tbxIrrf.Text = 0.ToString("N2");
            }
            if (NFCompra1_ckxInss.Checked == true)
            {
                NFCompra1_ckxInss.Text = "Sim Tem I.N.S.S.";
                if (clsParser.DecimalParse(NFCompra1_tbxInssPorc.Text) > 0)
                {
                    NFCompra1_tbxInss.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxInssPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                NFCompra1_ckxInss.Text = "Não Tem I.N.S.S.";
                NFCompra1_tbxInss.Text = 0.ToString("N2");
            }
            if (NFCompra1_ckxPisCofinsCsll.Checked == true)
            {
                NFCompra1_ckxPisCofinsCsll.Text = "Sim Pis/Cofins/Csll";
                if (clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsllPorc.Text) > 0)
                {
                    NFCompra1_tbxPisCofinsCsll.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxPisCofinsCsllPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                NFCompra1_ckxPisCofinsCsll.Text = "Não Pis/Cofins/Csll";
                NFCompra1_tbxPisCofinsCsll.Text = 0.ToString("N2");
            }
            if (NFCompra1_ckxIss.Checked == true)
            {
                NFCompra1_ckxIss.Text = "Sim Tem I.S.S.";
                if (clsParser.DecimalParse(NFCompra1_tbxIssPorc.Text) > 0)
                {
                    NFCompra1_tbxIss.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxIssPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                NFCompra1_ckxIss.Text = "Não Tem I.S.S.";
                NFCompra1_tbxIss.Text = 0.ToString("N2");
            }
            if (NFCompra1_ckxPis.Checked == true)
            {
                NFCompra1_ckxPis.Text = "Sim Tem PIS";
                if (clsParser.DecimalParse(NFCompra1_tbxPisPorc.Text) > 0)
                {
                    NFCompra1_tbxPis.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxPisPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                NFCompra1_ckxPis.Text = "Não Tem PIS";
                NFCompra1_tbxPis.Text = 0.ToString("N2");
            }
            if (NFCompra1_ckxCofins.Checked == true)
            {
                NFCompra1_ckxCofins.Text = "Sim Tem Cofins";
                if (clsParser.DecimalParse(NFCompra1_tbxCofinsPorc.Text) > 0)
                {
                    NFCompra1_tbxCofins.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxCofinsPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                NFCompra1_ckxCofins.Text = "Não Tem Cofins";
                NFCompra1_tbxCofins.Text = 0.ToString("N2");
            }
            if (NFCompra1_ckxCsll.Checked == true)
            {
                NFCompra1_ckxCsll.Text = "Sim Tem Cofins";
                if (clsParser.DecimalParse(NFCompra1_tbxCsllPorc.Text) > 0)
                {
                    NFCompra1_tbxCsll.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalnota.Text) * (clsParser.DecimalParse(NFCompra1_tbxCsllPorc.Text) / 100)).ToString("N2");
                }
            }
            else
            {
                NFCompra1_ckxCsll.Text = "Não Tem Cofins";
                NFCompra1_tbxCsll.Text = 0.ToString("N2");
            }



        }

        /*
        private void NFCompra1SomaICM()
        {
            if (NFCompra1_cbxConsumo.Text.PadRight(1,' ').Substring(0,1) == "S")
            {
                NFCompra1_tbxBaseicm.Text = (clsParser.DecimalParse(NFCompra1_tbxCustoipi.Text) + clsParser.DecimalParse(NFCompra1_tbxTotalmercado.Text)).ToString("N2");
            }
            else
            {
                NFCompra1_tbxBaseicm.Text = (clsParser.DecimalParse(NFCompra1_tbxTotalmercado.Text)).ToString("N2");
            }
            // adicionando valores na base do icms
            if (NFCompra1_ckxValorOutrasIcms.Checked == true)
            {
                NFCompra1_tbxBaseicm.Text = clsParser.DecimalParse(NFCompra1_tbxBaseicm.Text) + clsParser.DecimalParse(NFCompra1_tbxValoroutros.Text).ToString("N2"); 
            }
            if (NFCompra1_ckxValorFreteIcms.Checked == true)
            {
                NFCompra1_tbxBaseicm.Text = clsParser.DecimalParse(NFCompra1_tbxBaseicm.Text) + clsParser.DecimalParse(NFCompra1_tbxValorfrete.Text).ToString("N2");
            }
            if (NFCompra1_ckxValorSeguroIcms.Checked == true)
            {
                NFCompra1_tbxBaseicm.Text = clsParser.DecimalParse(NFCompra1_tbxBaseicm.Text) + clsParser.DecimalParse(NFCompra1_tbxValorseguro.Text).ToString("N2");
            }


            if (clsParser.DecimalParse(NFCompra1_tbxBasemp.Text) == 100)
            {
                NFCompra1_tbxBaseicm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                NFCompra1_tbxIcm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                NFCompra1_tbxCustoicm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
            }
            else if (clsParser.DecimalParse(NFCompra1_tbxBasemp.Text) == 0)
            {
                if (clsParser.DecimalParse(NFCompra1_tbxIcm.Text) > 0)
                {
                    NFCompra1_tbxCustoicm.Text = (clsParser.DecimalParse(NFCompra1_tbxBaseicm.Text) * (clsParser.DecimalParse(NFCompra1_tbxIcm.Text) / 100)).ToString("N2");
                }
                else
                {
                    NFCompra1_tbxCustoicm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                }
            }
            else
            {
                NFCompra1_tbxBaseicm.Text = (clsParser.DecimalParse(NFCompra1_tbxBaseicm.Text) * (clsParser.DecimalParse(NFCompra1_tbxBasemp.Text) / 100)).ToString("N2");
                if (clsParser.DecimalParse(NFCompra1_tbxIcm.Text) > 0)
                {
                    NFCompra1_tbxCustoicm.Text = (clsParser.DecimalParse(NFCompra1_tbxBaseicm.Text) * (clsParser.DecimalParse(NFCompra1_tbxIcm.Text) / 100)).ToString("N2");
                }
                else
                {
                    NFCompra1_tbxCustoicm.Text = clsParser.DecimalParse("0".ToString()).ToString("N2");
                }
            }
}*/

        private void NFCompra1_ckxValorOutrasIcms_Click(object sender, EventArgs e)
        {
            if (NFCompra1_ckxValorOutrasIcms.Checked == true)
            {
                NFCompra1_ckxValorOutrasIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                NFCompra1_ckxValorOutrasIcms.Text = "Não Incide ICMS";
            }
        }

        private void NFCompra1_ckxValorFreteIcms_Click(object sender, EventArgs e)
        {
            if (NFCompra1_ckxValorFreteIcms.Checked == true)
            {
                NFCompra1_ckxValorFreteIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                NFCompra1_ckxValorFreteIcms.Text = "Não Incide ICMS";
            }
        }

        private void NFCompra1_ckxValorSeguroIcms_Click(object sender, EventArgs e)
        {
            if (NFCompra1_ckxValorSeguroIcms.Checked == true)
            {
                NFCompra1_ckxValorSeguroIcms.Text = "Sim Incide ICMS";
            }
            else
            {
                NFCompra1_ckxValorSeguroIcms.Text = "Não Incide ICMS";
            }
        }

        private void NFCompra1_ckxIrrf_Click(object sender, EventArgs e)
        {
            Calcular();
            //NFCompra1SomaTotal();
        }

        private void NFCompra1_ckxInss_Click(object sender, EventArgs e)
        {
            Calcular();
            //NFCompra1SomaTotal();
        }

        private void NFCompra1_ckxPisCofinsCsll_Click(object sender, EventArgs e)
        {
            Calcular();
            //NFCompra1SomaTotal();
        }

        private void NFCompra1_ckxIss_Click(object sender, EventArgs e)
        {
            Calcular();
            //NFCompra1SomaTotal();
        }

        private void NFCompra1_ckxPis_Click(object sender, EventArgs e)
        {
            Calcular();
            //NFCompra1SomaTotal();
        }

        private void NFCompra1_ckxCofins_Click(object sender, EventArgs e)
        {
            Calcular();
            //NFCompra1SomaTotal();
        }

        private void NFCompra1_ckxCsll_Click(object sender, EventArgs e)
        {
            Calcular();
            //NFCompra1SomaTotal();
        }

        private void NFCompraPagar_tbxPagou_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxRetorno_TextChanged(object sender, EventArgs e)
        {

        }

        private void tspNFCompraPagarIncluir_Click(object sender, EventArgs e)
        {
            nfcomprapagar_posicao = 0;
            NFCompraPagarCarregar();

        }

        private void tspNFCompraPagarAlterar_Click(object sender, EventArgs e)
        {
            if (dgvNFCompraPagar.CurrentRow != null)
            {
                nfcomprapagar_posicao = Int32.Parse(dgvNFCompraPagar.CurrentRow.Cells["posicaorec"].Value.ToString());
                NFCompraPagarCarregar();
            }

        }

        private void tspNFCompraPagarExcluir_Click(object sender, EventArgs e)
        {
            if (dgvNFCompraPagar.CurrentRow != null)
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja realmente excluir o item selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    dtNFCompraPagar.Select("posicaorec =" + dgvNFCompraPagar.CurrentRow.Cells["posicaorec"].Value.ToString())[0].Delete();
                }
            }
        }

        private void tspNFCompraPagarCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (nfcompra_idcondpagto > 0)
                {
                    NfcompraPagarCalcularPagamentos();
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

        private void NfcompraPagarCalcularPagamentos()
        {
            // Realiza as verificações necessárias
            // para ver se é possível realizar o re-cálculo
            foreach (DataRow row in dtNFCompraPagar.Rows)
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
            foreach (DataRow row in dtNFCompra1.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    row.RowState != DataRowState.Detached)
                {
                    if (row["fatura"].ToString() == "S")
                    {
                        totalcobranca += clsParser.DecimalParse(row["totalnota"].ToString());
                        totalcobrancaipi += clsParser.DecimalParse(row["custoipi"].ToString());
                        totalcobrancast += clsParser.DecimalParse(row["icmsubst"].ToString());
                        totalcobrancapis += clsParser.DecimalParse(row["pispasep"].ToString());
                        totalcobrancacofins += clsParser.DecimalParse(row["cofins1"].ToString());
                        //totalcomissao += clsParser.DecimalParse(row["valorcomissao"].ToString());
                        //totalcomissaoger += clsParser.DecimalParse(row["valorcomissaoger"].ToString());
                        //totalcomissaosup += clsParser.DecimalParse(row["valorcomissaosup"].ToString());

                        totalcobrancamoirrf += clsParser.DecimalParse(row["irrf"].ToString());
                        totalcobrancamoinss += clsParser.DecimalParse(row["inss"].ToString());
                        totalcobrancamopiscofinscsll += clsParser.DecimalParse(row["piscofinscsll"].ToString());
                        totalcobrancamopis += clsParser.DecimalParse(row["pis"].ToString());
                        totalcobrancamocofins += clsParser.DecimalParse(row["cofins"].ToString());
                        totalcobrancamocsll += clsParser.DecimalParse(row["csll"].ToString());
                    }
                }
            }

            if (totalcobranca <= 0)
            {
                return;
            }
            else
            {
                totalcobranca -= (totalcobrancamoirrf +
                                  totalcobrancamoinss +
                                  totalcobrancamopiscofinscsll +
                                  totalcobrancamopis +
                                  totalcobrancamocofins +
                                  totalcobrancamocsll);
            }

            // Verifica se irá ou não descontar pis/cofins
            Boolean descontapis;
            Boolean descontacofins;
            descontapis = (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DESCONTAPISPASEPSAI from cliente where id=" + nfcompra_idfornecedor) == "S");
            descontacofins = (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select DESCONTACOFINSSAI from cliente where id=" + nfcompra_idfornecedor) == "S");

            clsCondpagtoBLL CondpagtoBLL = new clsCondpagtoBLL();
            clsCondpagtoInfo CondpagtoInfo = CondpagtoBLL.Carregar(nfcompra_idcondpagto, clsInfo.conexaosqldados);
            if (CondpagtoInfo == null)
            {
                throw new Exception("Condição de Pagamento não foi escolhida, deve escolher a Condição de Pagamento antes de calcularos pagamentos.");
            }

            dgvNFCompraPagar.DataSource = null;


            if (dtNFCompraPagarTempDeletar == null)
            {
                dtNFCompraPagarTempDeletar = new DataTable();
                dtNFCompraPagarTempDeletar = dtNFCompraPagar.Copy();
            }
            else
            {
                foreach (DataRow row in dtNFCompraPagar.Rows)
                {
                    if (row.RowState != DataRowState.Added &&
                        row.RowState != DataRowState.Detached)
                    {
                        dtNFCompraPagarTempDeletar.Rows.Add(row);
                    }
                }
            }

            dtNFCompraPagar = clsFinanceiro.GerarFatura(DateTime.Parse(tbxData.Text),
                                                        totalcobranca,
                                                        0,
                                                        totalcobrancaipi,
                                                        totalcobrancast,
                                                        (descontapis == false),
                                                        totalcobrancapis,
                                                        (descontacofins == false),
                                                        totalcobrancacofins,
                                                        nfcompra_idformapagto,
                                                        nfcompra_idcondpagto,
                                                        "N", "S");

            DataColumn dcId = new DataColumn("ID", Type.GetType("System.Int32"));
            DataColumn dcIdNota = new DataColumn("IDNOTA", Type.GetType("System.Int32"));
            DataColumn dcPosicaoRec = new DataColumn("POSICAOREC", Type.GetType("System.Int32"));

            dtNFCompraPagar.Columns.Add(dcId);
            dtNFCompraPagar.Columns.Add(dcIdNota);
            dtNFCompraPagar.Columns.Add(dcPosicaoRec);

            Int32 posicaorec = 1;
            for (Int32 x = 0; x < dtNFCompraPagar.Rows.Count; x++)
            {
                if (dtNFCompraPagar.Rows[x].RowState != DataRowState.Detached &&
                    dtNFCompraPagar.Rows[x].RowState != DataRowState.Deleted)
                {
                    dtNFCompraPagar.Rows[x]["ID"] = 0;
                    dtNFCompraPagar.Rows[x]["IDNOTA"] = id;
                    dtNFCompraPagar.Rows[x]["POSICAOREC"] = posicaorec;
                    dtNFCompraPagar.Rows[x]["IDTIPOPAGA"] = nfcompra_idformapagto;
                    posicaorec++;
                }
            }

            dgvNFCompraPagar.DataSource = dtNFCompraPagar;

            clsGridHelper.MontaGrid2(dgvNFCompraPagar, clsNfCompraPagarBLL.dtGridColunas, true);
            //}
        }

        private void NFCompraPagarCarregar()
        {
            clsNfCompraPagarInfo = new clsNfCompraPagarInfo();
            clsNfCompraPagarInfoOld = new clsNfCompraPagarInfo();

            if (nfcomprapagar_posicao == 0)
            {
                nfcomprapagar_posicao = dtNFCompraPagar.Rows.Count + 1;
                clsNfCompraPagarInfo.boletonro = 0;
                clsNfCompraPagarInfo.data = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                clsNfCompraPagarInfo.dv = "";
                clsNfCompraPagarInfo.id = 0;
                clsNfCompraPagarInfo.idnota = id;
                clsNfCompraPagarInfo.idtipopaga = clsInfo.zformapagto;
                clsNfCompraPagarInfo.pagou = "N";
                clsNfCompraPagarInfo.posicao = 1;
                clsNfCompraPagarInfo.posicaofim = 1;
                clsNfCompraPagarInfo.valor = 0;
            }
            else
            {
                NFCompraPagarGridToInfo(dtNFCompraPagar, clsNfCompraPagarInfo, nfcomprapagar_posicao);
            }

            NFCompraPagarCampos(clsNfCompraPagarInfo);
            NFCompraPagarFillInfo(clsNfCompraPagarInfoOld);

            tclNFCompra1Itens.SelectedIndex = 5;
            gbxNFCompraPagarItem.Visible = true;
            if (gbxGrupoCondicaopagamento.Enabled == true)
            {
                NFCompraPagar_tbxPosicao.Select();
            }

        }
        private void NFCompraPagarGridToInfo(DataTable dt, clsNfCompraPagarInfo info, Int32 posicao)
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
        }
        private void NFCompraPagarCampos(clsNfCompraPagarInfo info)
        {
            nfcomprapagar_id = info.id;
            NFCompraPagar_tbxData.Text = info.data.ToString("dd/MM/yyyy");
            NFCompraPagar_tbxDV.Text = info.dv;
            nfcomprapagar_idnota = info.idnota;
            nfcomprapagar_idtipopaga = info.idtipopaga;

            if (nfcomprapagar_idtipopaga == 0) { nfcomprapagar_idtipopaga = clsInfo.zformapagto; }
            NFCompraPagar_tbxFormaPagto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + nfcomprapagar_idtipopaga);
            NFCompraPagar_tbxFormaPagto.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + nfcomprapagar_idtipopaga);

            NFCompraPagar_tbxPagou.Text = info.pagou;
            NFCompraPagar_tbxPosicao.Text = info.posicao.ToString("N0");
            NFCompraPagar_tbxPosicaoFim.Text = info.posicaofim.ToString("N0");
            NFCompraPagar_tbxValor.Text = info.valor.ToString("N2");
        }
        private void NFCompraPagarFillInfo(clsNfCompraPagarInfo info)
        {
            info.id = nfcomprapagar_id;
            info.idnota = nfcomprapagar_idnota;
            info.boletonro = clsParser.DecimalParse(NFCompraPagar_tbxBoletoNro.Text);
            info.data = DateTime.Parse(NFCompraPagar_tbxData.Text);
            info.dv = NFCompraPagar_tbxDV.Text;
            info.idtipopaga = nfcomprapagar_idtipopaga;
            info.pagou = NFCompraPagar_tbxPagou.Text;
            info.posicao = clsParser.Int32Parse(NFCompraPagar_tbxPosicao.Text);
            info.posicaofim = clsParser.Int32Parse(NFCompraPagar_tbxPosicaoFim.Text);
            info.valor = clsParser.DecimalParse(NFCompraPagar_tbxValor.Text);
        }

        private void tspNFCompraPagarSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar/alterar os Pagamentos ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsNfCompraPagarInfo = new clsNfCompraPagarInfo();
                    NFCompraPagarFillInfo(clsNfCompraPagarInfo);
                    NFCompraPagarFillInfoToGrid(dtNFCompraPagar, clsNfCompraPagarInfo);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

                //NFCompra1SomaTotal();

                //NFCompraSomar();

                gbxNFCompraPagarItem.Visible = false;
                tclNFCompra1Itens.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspNFCompraPagarPrimeiro_Click(object sender, EventArgs e)
        {

        }

        private void tspNFCompraPagarAnterior_Click(object sender, EventArgs e)
        {

        }

        private void tspNFCompraPagarProximo_Click(object sender, EventArgs e)
        {

        }

        private void tspNFCompraPagarUltimo_Click(object sender, EventArgs e)
        {

        }

        private void tspNFCompraPagarRetornar_Click(object sender, EventArgs e)
        {
            gbxNFCompraPagarItem.Visible = false;
            tclNFCompra1Itens.SelectedIndex = 0;

        }
        private void NFCompraPagarFillInfoToGrid(DataTable dt, clsNfCompraPagarInfo info)
        {
            DataRow row;
            DataRow[] rows = dt.Select("posicaorec = " + nfcomprapagar_posicao);

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

            // Colunas que petencem a outras tabelas
            /*            row["codigo"] = Orcamento1_tbxCodigo.Text;
                        row["nome"] = Orcamento1_tbxCodigoNome.Text;
                        row["unid"] = Orcamento1_Pecas_tbxUnidade.Text;
                        */
            if (rows.Length == 0)
            {
                row["posicaorec"] = nfcomprapagar_posicao;
                dt.Rows.Add(row);
            }
        }

        private void dgvNFCompraPagar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspNFCompraPagarAlterar.PerformClick();
        }

        private void NFCompraPagar_btnIdFormaPagto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = NFCompraPagar_btnIdFormaPagto.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", nfcomprapagar_idtipopaga, "Tipo Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void NFCompra1_ckxCreditarIcm_Click(object sender, EventArgs e)
        {

        }

        private void tsptbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }

        private void tsptbxPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
        }

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tsptbxPesquisaCompras.Text, dgvComprasPendentes);

            clsGridHelper.SelecionaLinha(id, dgvComprasPendentes, "NUMERO");
            tsptbxPesquisaCompras.Focus();
            if (dgvComprasPendentes.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvComprasPendentes.CurrentRow.Cells["ID"].Value.ToString());
            }
            else
            {
                id = 0;
            }
        }

        private void NFCompra1_ckxFatura_Click(object sender, EventArgs e)
        {
            if (NFCompra1_ckxFatura.Checked == true)
            {
                NFCompra1_ckxFatura.Text = "Sim Será Cobrado ";
            }
            else
            {
                NFCompra1_ckxFatura.Text = "Não Será Cobrado ";
            }
        }

        private void NFCompra1_ckxSomaproduto_Click(object sender, EventArgs e)
        {
            if (NFCompra1_ckxSomaproduto.Checked == true)
            {
                NFCompra1_ckxSomaproduto.Text = "Soma no Total NF";
            }
            else
            {
                NFCompra1_ckxSomaproduto.Text = "Não Soma no Total NF";
            }
        }

        private void NFCompra1_ckxCalculoautomatico_Click(object sender, EventArgs e)
        {

        }

        private void dgvComprasPendentes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvComprasPendentes.CurrentRow != null)
            {
                EntradaPedidoCompraEntrega(clsParser.Int32Parse(dgvComprasPendentes.CurrentRow.Cells["id"].Value.ToString()));
            }
        }
        private void CriarPedidoVenda_e_OS_Coating(Int32 _idPedidoVenda, Int32 _idPedidoVendaItem, Int32 _idOrdemServico, String _tipoProduto, Int32 _idNFCompra1)
        {
            // Inicio - Tabela Pedido
            //
            Int32 idfornecedorpedido;
            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;


            Int32 _idPedidoVendaItemPrg = 0;

            if (_tipoProduto == "C")
            { // neste caso a ordem de servico é para a propria empresa
                idfornecedorpedido = clsInfo.zempresaclienteid;
            }
            else
            {  // neste caso a ordem de serviço é para o cliente/fornecedor
                idfornecedorpedido = clsNfCompraInfo.idfornecedor;
            }
            if (idfornecedorpedido != clsInfo.zempresaclienteid)
            {  // se o cliente é a propria empresa não criar o pedido apenas a O.S.                                                     
                if (_idPedidoVenda == 0 || _idPedidoVenda == clsInfo.zpedido)
                {
                    /* analisar                   scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "SELECT @NUMERO = MAX(NUMERO + 1)  FROM PEDIDO WHERE YEAR(DATA) = YEAR(GETDATE()) AND FILIAL = @FILIAL  " +
                                        "INSERT INTO PEDIDO ( " +
                                                                        "FILIAL, NUMERO, DATA, IDUSUARIO, IDCLIENTE, IDREPRESENTANTE, COMISSAOREPRESENTANTE, IDCOORDENADOR, COMISSAOCOORDENADOR, IDSUPERVISOR, COMISSAOSUPERVISOR, SITUACAO, OBSERVA, SETOR, SETORFATOR, FRETE, FRETEPAGA, TRANSPORTE, TIPOFRETE, FRETETIPO, IDTRANSPORTADORA, FRETEQTDE, FRETEUNID, FRETEPRECOUNI, FRETETOTAL, FRETEBASEICMS, FRETEICMALIQ, FRETEVALORICMS, IDFORMAPAGTO, IDCONDPAGTO, TOTALPEDIDOBRUTO, TOTALDESCONTO, PORDESC, TOTALPEDIDOLIQUIDO, TOTALBCIPI, TOTALIPI, TOTALFRETE, TOTALSEGURO, TOTALOUTRAS, TOTALBASEICM, TOTALICM, TOTALBASEICMSUBST, TOTALICMSUBST, TOTALBCPISPASEP, TOTALPISPASEP, TOTALBCCOFINS1, TOTALCOFINS1, TOTALPREVISTO, TOTALFATURAR, ISENTOIPI, REVENDEDOR, ZFM, AUTORIZA_IDUSUARIO, AUTORIZA_DATA, ANO " +
                                                                        ") VALUES ( " +
                                                                        "@FILIAL, @NUMERO, @DATA, @IDUSUARIO, @IDCLIENTE, @IDREPRESENTANTE, @COMISSAOREPRESENTANTE, @IDCOORDENADOR, @COMISSAOCOORDENADOR, @IDSUPERVISOR, @COMISSAOSUPERVISOR, @SITUACAO, @OBSERVA, @SETOR, @SETORFATOR, @FRETE, @FRETEPAGA, @TRANSPORTE, @TIPOFRETE, @FRETETIPO, @IDTRANSPORTADORA, @FRETEQTDE, @FRETEUNID, @FRETEPRECOUNI, @FRETETOTAL, @FRETEBASEICMS, @FRETEICMALIQ, @FRETEVALORICMS, @IDFORMAPAGTO, @IDCONDPAGTO, @TOTALPEDIDOBRUTO, @TOTALDESCONTO, @PORDESC, @TOTALPEDIDOLIQUIDO, @TOTALBCIPI, @TOTALIPI, @TOTALFRETE, @TOTALSEGURO, @TOTALOUTRAS, @TOTALBASEICM, @TOTALICM, @TOTALBASEICMSUBST, @TOTALICMSUBST, @TOTALBCPISPASEP, @TOTALPISPASEP, @TOTALBCCOFINS1, @TOTALCOFINS1, @TOTALPREVISTO, @TOTALFATURAR, @ISENTOIPI, @REVENDEDOR, @ZFM, @AUTORIZA_IDUSUARIO, @AUTORIZA_DATA, @ANO " +
                                                                        "); SELECT SCOPE_IDENTITY() ", CommandType.Text);
                                        _idPedidoVenda = 0;
                     */
                }
                else
                {
                    /* analisar         scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "UPDATE PEDIDO SET " +
                                                    "FILIAL = @FILIAL, NUMERO = @NUMERO, DATA = @DATA, IDUSUARIO = @IDUSUARIO, IDCLIENTE = @IDCLIENTE, IDREPRESENTANTE = @IDREPRESENTANTE, COMISSAOREPRESENTANTE = @COMISSAOREPRESENTANTE, IDCOORDENADOR = @IDCOORDENADOR, COMISSAOCOORDENADOR = @COMISSAOCOORDENADOR, IDSUPERVISOR = @IDSUPERVISOR, COMISSAOSUPERVISOR = @COMISSAOSUPERVISOR, SITUACAO=@SITUACAO, OBSERVA = @OBSERVA, SETOR = @SETOR, SETORFATOR = @SETORFATOR, FRETE = @FRETE, FRETEPAGA = @FRETEPAGA, TRANSPORTE = @TRANSPORTE, TIPOFRETE = @TIPOFRETE, FRETETIPO = @FRETETIPO, IDTRANSPORTADORA = @IDTRANSPORTADORA, FRETEQTDE = @FRETEQTDE, FRETEUNID = @FRETEUNID, FRETEPRECOUNI = @FRETEPRECOUNI, FRETETOTAL = @FRETETOTAL, FRETEBASEICMS = @FRETEBASEICMS, FRETEICMALIQ = @FRETEICMALIQ, FRETEVALORICMS = @FRETEVALORICMS, IDFORMAPAGTO = @IDFORMAPAGTO, IDCONDPAGTO = @IDCONDPAGTO, TOTALPEDIDOBRUTO=@TOTALPEDIDOBRUTO, TOTALDESCONTO=@TOTALDESCONTO, PORDESC=@PORDESC, TOTALPEDIDOLIQUIDO=@TOTALPEDIDOLIQUIDO, TOTALBCIPI = @TOTALBCIPI, TOTALIPI = @TOTALIPI, TOTALFRETE = @TOTALFRETE, TOTALSEGURO = @TOTALSEGURO, TOTALOUTRAS = @TOTALOUTRAS, TOTALBASEICM = @TOTALBASEICM, TOTALICM = @TOTALICM, TOTALBASEICMSUBST = @TOTALBASEICMSUBST, TOTALICMSUBST = @TOTALICMSUBST, TOTALBCPISPASEP = @TOTALBCPISPASEP, TOTALPISPASEP = @TOTALPISPASEP, TOTALBCCOFINS1 = @TOTALBCCOFINS1, TOTALCOFINS1 = @TOTALCOFINS1, TOTALPREVISTO = @TOTALPREVISTO, TOTALFATURAR = @TOTALFATURAR, ISENTOIPI = @ISENTOIPI, REVENDEDOR = @REVENDEDOR, ZFM = @ZFM, AUTORIZA_IDUSUARIO = @AUTORIZA_IDUSUARIO, AUTORIZA_DATA = @AUTORIZA_DATA, ANO=@ANO " +
                                                    "WHERE ID = @ID", CommandType.Text);
                                        scd.Parameters.AddWithValue("@ID", _idPedidoVenda);
                    */
                }
                /*                scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = clsInfo.zfilial;
                                scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from pedido where id=" + _idPedidoVenda));
                                scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = clsNfCompraInfo.data;
                                scd.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = clsInfo.zusuarioid;
                                scd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = idfornecedorpedido;
                                Int32 idRepresentante = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idvendedor from cliente where id=" + idfornecedorpedido));
                                scd.Parameters.Add("@IDREPRESENTANTE", SqlDbType.Int).Value = idRepresentante;
                                scd.Parameters.Add("@COMISSAOREPRESENTANTE", SqlDbType.Decimal).Value = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaoaliquota from cliente where id=" + idRepresentante));
                                scd.Parameters.Add("@IDCOORDENADOR", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcoordenador from cliente where id=" + idRepresentante));
                                scd.Parameters.Add("@COMISSAOCOORDENADOR", SqlDbType.Decimal).Value = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaocoordenador from cliente where id=" + idRepresentante));
                                scd.Parameters.Add("@IDSUPERVISOR", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idsupervisor from cliente where id=" + idRepresentante));
                                scd.Parameters.Add("@COMISSAOSUPERVISOR", SqlDbType.Decimal).Value = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select comissaocoordenador from cliente where id=" + idRepresentante));
                                scd.Parameters.Add("@SITUACAO", SqlDbType.NVarChar).Value = "2";
                                scd.Parameters.Add("@OBSERVA", SqlDbType.NVarChar).Value = clsNfCompraInfo.observa;
                                scd.Parameters.Add("@SETOR", SqlDbType.NVarChar).Value = "N";
                                scd.Parameters.Add("@SETORFATOR", SqlDbType.Decimal).Value = "0";
                                scd.Parameters.Add("@FRETE", SqlDbType.NVarChar).Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select freteincluso from cliente where id=" + idfornecedorpedido);
                                scd.Parameters.Add("@FRETEPAGA", SqlDbType.NVarChar).Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select freteporconta from cliente where id=" + idfornecedorpedido); ;
                                scd.Parameters.Add("@TRANSPORTE", SqlDbType.NVarChar).Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select meiodetransporte from cliente where id=" + idfornecedorpedido); ;
                                scd.Parameters.Add("@TIPOFRETE", SqlDbType.NVarChar).Value = "0";
                                scd.Parameters.Add("@FRETETIPO", SqlDbType.NVarChar).Value = "";
                                scd.Parameters.Add("@IDTRANSPORTADORA", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idtransportadora from cliente where id=" + idRepresentante));
                                scd.Parameters.Add("@FRETEQTDE", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@FRETEUNID", SqlDbType.NVarChar).Value = "";
                                scd.Parameters.Add("@FRETEPRECOUNI", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@FRETETOTAL", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@FRETEBASEICMS", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@FRETEICMALIQ", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@FRETEVALORICMS", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@IDFORMAPAGTO", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idformapagto from cliente where id=" + idfornecedorpedido));
                                scd.Parameters.Add("@IDCONDPAGTO", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcondpagto from cliente where id=" + idfornecedorpedido));
                                scd.Parameters.Add("@TOTALPEDIDOBRUTO", SqlDbType.Decimal).Value = 0;
                                //scd.Parameters.Add("@TOTALMERCADORIA", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALDESCONTO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@PORDESC", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALPEDIDOLIQUIDO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALBCIPI", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALIPI", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALFRETE", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALSEGURO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALOUTRAS", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALBASEICM", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALICM", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALBASEICMSUBST", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALICMSUBST", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALBCPISPASEP", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALPISPASEP", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALBCCOFINS1", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALCOFINS1", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALPREVISTO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALFATURAR", SqlDbType.Decimal).Value = 0;
                                //scd.Parameters.Add("@TOTALPEDIDO", SqlDbType.Decimal).Value = 0;

                                scd.Parameters.Add("@ISENTOIPI", SqlDbType.NVarChar).Value = "";
                                scd.Parameters.Add("@REVENDEDOR", SqlDbType.NVarChar).Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select revendedor from cliente where id=" + idfornecedorpedido); ;
                                scd.Parameters.Add("@ZFM", SqlDbType.NVarChar).Value = "";
                                scd.Parameters.Add("@AUTORIZA_IDUSUARIO", SqlDbType.Int).Value = 1;
                                scd.Parameters.Add("@AUTORIZA_DATA", SqlDbType.DateTime).Value = DateTime.Now;
                                scd.Parameters.Add("@ANO", SqlDbType.Int).Value = DateTime.Now.Year;
                */
                if (_idPedidoVenda == 0)
                {
                    /*
                     _idPedidoVenda = clsSql.ExecuteQuery(scd);
                     */
                }
                else
                {
                    /*
                                        clsSql.ExecuteQuery(scd);
                     */
                }

                // Fim - Tabela Pedido             

                // Inicio - Tabela Pedido1
                //                            
                if (_idPedidoVendaItem == 0 || _idPedidoVendaItem == clsInfo.zpedido1)
                {
                    /*
                                        scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "INSERT INTO PEDIDO1 (" +
                                                                                                 "IDPEDIDO, IDORCAMENTO, IDORCAMENTOITEM, ITEM, IDCODIGO, COMPLEMENTO, COMPLEMENTO1, DESCRICAOESP, MSG, CONSUMO, IDTIPONOTA, IDCFOP, CODIGOEMP01, CODIGOEMP02, CODIGOEMP03, CODIGOEMP04, QTDE, QTDEENTREGUE, QTDEDEFEITO, QTDESUCATA, QTDEBAIXADA, QTDEOSAUX, QTDESALDO, IDUNIDADE, PRECOLISTA, PRECO, TOTALMERCADO, DESCONTO, VALORDESCONTO, IDIPI, IPI, BCIPI, CUSTOIPI, IDSITTRIBIPI, IDSITTRIBA, BASEICM, ICM, REDUCAO, CUSTOICM, IDSITTRIBB, BASEICMSUBST, ICMSUBST, ICMSSUBSTREDUCAO, MVA, ICMSSUBSTTOTAL, IDSITTRIBPISPASEP, BCPISPASEP, ALIQPISPASEP, PISPASEP, IDSITTRIBCOFINS1, BCCOFINS1, ALIQCOFINS1, COFINS1, VALORFRETE, VALORSEGURO, VALOROUTRAS, TOTALPREVISTO, TOTALFATURAR, TOTALNOTA, CALCULOAUTOMATICO, PROGRAMACAOTIPO, CANCELADO, BASEMP, PESO, TOTALPESO " +
                                                                                                 ") VALUES ( " +
                                                                                                 "@IDPEDIDO, @IDORCAMENTO, @IDORCAMENTOITEM, @ITEM, @IDCODIGO, @COMPLEMENTO, @COMPLEMENTO1, @DESCRICAOESP, @MSG, @CONSUMO, @IDTIPONOTA, @IDCFOP, @CODIGOEMP01, @CODIGOEMP02, @CODIGOEMP03, @CODIGOEMP04, @QTDE, @QTDEENTREGUE, @QTDEDEFEITO, @QTDESUCATA, @QTDEBAIXADA, @QTDEOSAUX, @QTDESALDO, @IDUNIDADE, @PRECOLISTA, @PRECO, @TOTALMERCADO, @DESCONTO, @VALORDESCONTO, @IDIPI, @IPI, @BCIPI, @CUSTOIPI, @IDSITTRIBIPI, @IDSITTRIBA, @BASEICM, @ICM, @REDUCAO, @CUSTOICM, @IDSITTRIBB, @BASEICMSUBST, @ICMSUBST, @ICMSSUBSTREDUCAO, @MVA, @ICMSSUBSTTOTAL, @IDSITTRIBPISPASEP, @BCPISPASEP, @ALIQPISPASEP, @PISPASEP, @IDSITTRIBCOFINS1, @BCCOFINS1, @ALIQCOFINS1, @COFINS1, @VALORFRETE, @VALORSEGURO, @VALOROUTRAS, @TOTALPREVISTO, @TOTALFATURAR, @TOTALNOTA, @CALCULOAUTOMATICO, @PROGRAMACAOTIPO, @CANCELADO, @BASEMP, @PESO, @TOTALPESO " +
                                                                                                 "); SELECT SCOPE_IDENTITY()", CommandType.Text);
                                        _idPedidoVendaItem = 0;
                     */
                }
                else
                {
                    /*                    scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "UPDATE PEDIDO1 SET " +
                                                                                "IDPEDIDO = @IDPEDIDO, IDORCAMENTO = @IDORCAMENTO, IDORCAMENTOITEM = @IDORCAMENTOITEM, ITEM = @ITEM, IDCODIGO = @IDCODIGO, COMPLEMENTO = @COMPLEMENTO, COMPLEMENTO1 = @COMPLEMENTO1, DESCRICAOESP = @DESCRICAOESP, MSG = @MSG, CONSUMO = @CONSUMO, IDTIPONOTA = @IDTIPONOTA, IDCFOP = @IDCFOP, CODIGOEMP01 = @CODIGOEMP01, CODIGOEMP02 = @CODIGOEMP02, CODIGOEMP03 = @CODIGOEMP03, CODIGOEMP04 = @CODIGOEMP04, QTDE = @QTDE, QTDEENTREGUE = @QTDEENTREGUE, QTDEDEFEITO = @QTDEDEFEITO, QTDESUCATA = @QTDESUCATA, QTDEBAIXADA = @QTDEBAIXADA, QTDEOSAUX = @QTDEOSAUX, QTDESALDO = @QTDESALDO, IDUNIDADE = @IDUNIDADE, PRECOLISTA = @PRECOLISTA, PRECO = @PRECO, TOTALMERCADO = @TOTALMERCADO, DESCONTO = @DESCONTO, VALORDESCONTO = @VALORDESCONTO, IDIPI = @IDIPI, IPI = @IPI, BCIPI = @BCIPI, CUSTOIPI = @CUSTOIPI, IDSITTRIBIPI = @IDSITTRIBIPI, IDSITTRIBA = @IDSITTRIBA, BASEICM = @BASEICM, ICM = @ICM, REDUCAO = @REDUCAO, CUSTOICM = @CUSTOICM, IDSITTRIBB = @IDSITTRIBB, BASEICMSUBST = @BASEICMSUBST, ICMSUBST = @ICMSUBST, ICMSSUBSTREDUCAO = @ICMSSUBSTREDUCAO, MVA = @MVA, ICMSSUBSTTOTAL = @ICMSSUBSTTOTAL, IDSITTRIBPISPASEP = @IDSITTRIBPISPASEP, BCPISPASEP = @BCPISPASEP, ALIQPISPASEP = @ALIQPISPASEP, PISPASEP = @PISPASEP, IDSITTRIBCOFINS1 = @IDSITTRIBCOFINS1, BCCOFINS1 = @BCCOFINS1, ALIQCOFINS1 = @ALIQCOFINS1, COFINS1 = @COFINS1, VALORFRETE = @VALORFRETE, VALORSEGURO = @VALORSEGURO, VALOROUTRAS = @VALOROUTRAS, TOTALPREVISTO = @TOTALPREVISTO, TOTALFATURAR  = @TOTALFATURAR, TOTALNOTA = @TOTALNOTA, CALCULOAUTOMATICO = @CALCULOAUTOMATICO, PROGRAMACAOTIPO = @PROGRAMACAOTIPO, CANCELADO = @CANCELADO, BASEMP = @BASEMP, PESO = @PESO, TOTALPESO=@TOTALPESO " +
                                                                                " WHERE ID = @ID", CommandType.Text);
                                        scd.Parameters.AddWithValue("@ID", _idPedidoVendaItem);
                     */
                }

                /*                scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = _idPedidoVenda;
                                scd.Parameters.Add("@IDORCAMENTO", SqlDbType.Int).Value = clsInfo.zorcamento;
                                scd.Parameters.Add("@IDORCAMENTOITEM", SqlDbType.Int).Value = clsInfo.zorcamento1;
                                scd.Parameters.Add("@ITEM", SqlDbType.Int).Value = 1;
                                scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = clsNfCompra1Info.idcodigo;
                                scd.Parameters.Add("@COMPLEMENTO", SqlDbType.NVarChar).Value = clsNfCompra1Info.complemento;
                                scd.Parameters.Add("@COMPLEMENTO1", SqlDbType.NVarChar).Value = clsNfCompra1Info.complemento1;
                                scd.Parameters.Add("@DESCRICAOESP", SqlDbType.NVarChar).Value = clsNfCompra1Info.descricaoesp;
                                scd.Parameters.Add("@MSG", SqlDbType.NVarChar).Value = clsNfCompra1Info.msg;
                                String cConsumo = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CONSUMO FROM PECAS WHERE ID=" + clsNfCompra1Info.idcodigo);
                                scd.Parameters.Add("@CONSUMO", SqlDbType.NVarChar).Value = cConsumo;
                                scd.Parameters.Add("@IDTIPONOTA", SqlDbType.Int).Value = 1;
                                scd.Parameters.Add("@IDCFOP", SqlDbType.Int).Value = clsNfCompra1Info.idcfop;
                                scd.Parameters.Add("@CODIGOEMP01", SqlDbType.NVarChar).Value = clsNfCompra1Info.codigoemp01;
                                scd.Parameters.Add("@CODIGOEMP02", SqlDbType.NVarChar).Value = clsNfCompra1Info.codigoemp02;
                                scd.Parameters.Add("@CODIGOEMP03", SqlDbType.NVarChar).Value = clsNfCompra1Info.codigoemp03;
                                scd.Parameters.Add("@CODIGOEMP04", SqlDbType.NVarChar).Value = clsNfCompra1Info.codigoemp04;
                                scd.Parameters.Add("@QTDE", SqlDbType.Decimal).Value = clsNfCompra1Info.qtde;
                                scd.Parameters.Add("@QTDEENTREGUE", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@QTDEDEFEITO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@QTDESUCATA", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@QTDEBAIXADA", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@QTDEOSAUX", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@QTDESALDO", SqlDbType.Decimal).Value = clsNfCompra1Info.qtde;
                                scd.Parameters.Add("@IDUNIDADE", SqlDbType.Int).Value = clsNfCompra1Info.idunid;
                                // pegar o preço na lista de preço
                                Decimal nPrecoLista = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT VALORAVISTA FROM PECASPRECO WHERE IDCODIGO=" + clsNfCompra1Info.idcodigo + " ORDER BY DATA DESC"));
                                scd.Parameters.Add("@PRECOLISTA", SqlDbType.Decimal).Value = nPrecoLista;
                                scd.Parameters.Add("@PRECO", SqlDbType.Decimal).Value = nPrecoLista;
                                Decimal nTotalMercado = clsNfCompra1Info.qtde * nPrecoLista;
                                scd.Parameters.Add("@TOTALMERCADO", SqlDbType.Decimal).Value = nTotalMercado;
                                scd.Parameters.Add("@DESCONTO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@VALORDESCONTO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@IDIPI", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDIPI FROM PECAS WHERE ID=" + clsNfCompra1Info.idcodigo));
                                Decimal nIPI = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ALIQUOTAIPI FROM PECAS WHERE ID=" + clsNfCompra1Info.idcodigo));
                                Decimal nValorIPI = 0;
                                scd.Parameters.Add("@IPI", SqlDbType.Decimal).Value = nIPI;
                                if (nIPI > 0)
                                {
                                    nValorIPI = nTotalMercado * (nIPI / 100);
                                    scd.Parameters.Add("@BCIPI", SqlDbType.Decimal).Value = nTotalMercado;
                                    scd.Parameters.Add("@CUSTOIPI", SqlDbType.Decimal).Value = nValorIPI;
                                }
                                else
                                {
                                    scd.Parameters.Add("@BCIPI", SqlDbType.Decimal).Value = 0;
                                    scd.Parameters.Add("@CUSTOIPI", SqlDbType.Decimal).Value = 0;
                                }
                                scd.Parameters.Add("@IDSITTRIBIPI", SqlDbType.Int).Value = 1;
                                scd.Parameters.Add("@IDSITTRIBA", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDSITTRIBA FROM PECAS WHERE ID=" + clsNfCompra1Info.idcodigo));
                                Decimal nBaseMP = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT BASEMP FROM PECAS WHERE ID=" + clsNfCompra1Info.idcodigo));
                                Decimal nBaseIcm = 0;
                                Decimal nTotalPedido = 0;
                                if (cConsumo == "N")
                                {
                                    if (nBaseMP > 0)
                                    {
                                        nBaseIcm = nTotalMercado * (nBaseMP / 100);
                                        scd.Parameters.Add("@BASEICM", SqlDbType.Decimal).Value = nBaseIcm;
                                    }
                                    else
                                    {
                                        nBaseIcm = nTotalMercado;
                                        scd.Parameters.Add("@BASEICM", SqlDbType.Decimal).Value = nBaseIcm;
                                    }
                                }
                                else
                                {
                                    nBaseIcm = nTotalMercado + nValorIPI;
                                    scd.Parameters.Add("@BASEICM", SqlDbType.Decimal).Value = nTotalMercado + nValorIPI;
                                }
                                nTotalPedido = nTotalMercado + nValorIPI;
                                Int32 daUF = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idestado from cliente where id=" + idfornecedorpedido));
                                Decimal nIcms = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ESTADOSICMS.ALIQUOTA From ESTADOSICMS inner join estados on estados.id=estadosicms.idestado inner join estados estados1 on estados.id=estadosicms.IDESTADODESTINO WHERE ESTADOS.ID = " + clsInfo.zempresa_ufid + " AND ESTADOS1.ID=" + daUF + ""));
                                scd.Parameters.Add("@ICM", SqlDbType.Decimal).Value = nIcms;
                                scd.Parameters.Add("@REDUCAO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@CUSTOICM", SqlDbType.Decimal).Value = nBaseIcm * (nIcms / 100);
                                scd.Parameters.Add("@IDSITTRIBB", SqlDbType.Int).Value = clsNfCompra1Info.idsittribb;
                                scd.Parameters.Add("@BASEICMSUBST", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@ICMSUBST", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@ICMSSUBSTREDUCAO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@MVA", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@ICMSSUBSTTOTAL", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@IDSITTRIBPISPASEP", SqlDbType.Int).Value = 1;
                                scd.Parameters.Add("@BCPISPASEP", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@ALIQPISPASEP", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@PISPASEP", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@IDSITTRIBCOFINS1", SqlDbType.Int).Value = 1;
                                scd.Parameters.Add("@BCCOFINS1", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@ALIQCOFINS1", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@COFINS1", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@VALORFRETE", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@VALORSEGURO", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@VALOROUTRAS", SqlDbType.Decimal).Value = 0;
                                scd.Parameters.Add("@TOTALPREVISTO", SqlDbType.Decimal).Value = nTotalMercado;
                                scd.Parameters.Add("@TOTALFATURAR", SqlDbType.Decimal).Value = nTotalPedido;
                                scd.Parameters.Add("@TOTALNOTA", SqlDbType.Decimal).Value = nTotalPedido;
                                scd.Parameters.Add("@CALCULOAUTOMATICO", SqlDbType.NVarChar).Value = "S";
                                scd.Parameters.Add("@PROGRAMACAOTIPO", SqlDbType.NVarChar).Value = "S";
                                scd.Parameters.Add("@CANCELADO", SqlDbType.NVarChar).Value = "N";
                                scd.Parameters.Add("@IDORSEMSERVICO", SqlDbType.Int).Value = clsInfo.zordemservico;
                                scd.Parameters.Add("@BASEMP", SqlDbType.Decimal).Value = nBaseMP;
                                Decimal nPeso = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT PESOUNIT FROM PECAS WHERE ID=" + clsNfCompra1Info.idcodigo));
                                scd.Parameters.Add("@PESO", SqlDbType.Decimal).Value = nPeso;
                                scd.Parameters.Add("@TOTALPESO", SqlDbType.Decimal).Value = nPeso * clsNfCompra1Info.qtde;
                */
                /*             if (_idPedidoVendaItem == 0)
                             {
                                 _idPedidoVendaItem = clsSql.ExecuteQuery(scd);
                             }
                             else
                             {
                                 clsSql.ExecuteQuery(scd);
                             }
                 */
                //
                // Fim - Tabela Pedido1                
                //--
                // Inicio - Tabela Pedido2
                // Carregar a tabela para ver se existe
                scn = new SqlConnection(clsInfo.conexaosqldados);
                scn.Open();
                scd = new SqlCommand("select * from pedido2 where idpedido=@IDPEDIDO AND idpedido1=@IDPEDIDO1 ", scn);
                scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = _idPedidoVenda;
                scd.Parameters.Add("@IDPEDIDO1", SqlDbType.Int).Value = _idPedidoVendaItem;
                sdr = scd.ExecuteReader();
                if (sdr.Read() == true)
                {
                    _idPedidoVendaItemPrg = clsParser.Int32Parse(sdr["id"].ToString());
                }

                if (_idPedidoVendaItemPrg == 0)
                {
                    /*                    scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "INSERT INTO PEDIDO2 (" +
                                                                                                "IDPEDIDO, IDPEDIDO1, IDORDEMSERVICO, IDORDEMSERVICOITEM, ENTREGA, QTDE, QTDEENTREGUE, QTDEDEFEITO, QTDEBAIXADA, QTDESUCATA, QTDEOSAUX, QTDESALDO, DIVERGENCIA, MOTIVO01, MOTIVO02 " +
                                                                                                ") VALUES ( " +
                                                                                                "@IDPEDIDO, @IDPEDIDO1, @IDORDEMSERVICO, @IDORDEMSERVICOITEM, @ENTREGA, @QTDE, @QTDEENTREGUE, @QTDEDEFEITO, @QTDEBAIXADA, @QTDESUCATA, @QTDEOSAUX, @QTDESALDO, @DIVERGENCIA, @MOTIVO01, @MOTIVO02 " +
                                                                                                "); SELECT SCOPE_IDENTITY()", CommandType.Text);
                    */
                    _idPedidoVendaItemPrg = 0;
                }
                else
                {
                    /*                    scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "UPDATE PEDIDO2 SET " +
                                                                "IDPEDIDO = @IDPEDIDO, IDPEDIDO1 = @IDPEDIDO1, IDORDEMSERVICO = @IDORDEMSERVICO, IDORDEMSERVICOITEM = @IDORDEMSERVICOITEM, ENTREGA = @ENTREGA, QTDE = @QTDE, QTDEENTREGUE = @QTDEENTREGUE, QTDEDEFEITO = @QTDEDEFEITO, QTDEBAIXADA = @QTDEBAIXADA, QTDESUCATA = @QTDESUCATA, QTDEOSAUX = @QTDEOSAUX, QTDESALDO = @QTDESALDO, DIVERGENCIA = @DIVERGENCIA, MOTIVO01 = @MOTIVO01, MOTIVO02 = @MOTIVO02 " + " WHERE ID = @ID", CommandType.Text);
                    */
                    scd.Parameters.AddWithValue("@ID", _idPedidoVendaItemPrg);
                }
                scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = _idPedidoVenda;
                scd.Parameters.Add("@IDPEDIDO1", SqlDbType.Int).Value = _idPedidoVendaItem;
                scd.Parameters.Add("@IDORDEMSERVICO", SqlDbType.Int).Value = clsInfo.zordemservico;
                scd.Parameters.Add("@IDORDEMSERVICOITEM", SqlDbType.Int).Value = 1;
                scd.Parameters.Add("@ENTREGA", SqlDbType.DateTime).Value = clsNfCompra1Info.datatabela;
                scd.Parameters.Add("@QTDE", SqlDbType.Decimal).Value = clsNfCompra1Info.qtde;
                scd.Parameters.Add("@QTDEENTREGUE", SqlDbType.Decimal).Value = 0;
                scd.Parameters.Add("@QTDEDEFEITO", SqlDbType.Decimal).Value = 0;
                scd.Parameters.Add("@QTDEBAIXADA", SqlDbType.Decimal).Value = 0;
                scd.Parameters.Add("@QTDESUCATA", SqlDbType.Decimal).Value = 0;
                scd.Parameters.Add("@QTDEOSAUX", SqlDbType.Decimal).Value = 0;
                scd.Parameters.Add("@QTDESALDO", SqlDbType.Decimal).Value = clsNfCompra1Info.qtde;
                scd.Parameters.Add("@DIVERGENCIA", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@MOTIVO01", SqlDbType.NVarChar).Value = "";
                scd.Parameters.Add("@MOTIVO02", SqlDbType.NVarChar).Value = "";

                if (_idPedidoVendaItemPrg == 0)
                {
                    /*                    _idPedidoVendaItemPrg = clsSql.ExecuteQuery(scd); */
                }
                else
                {
                    /*                    clsSql.ExecuteQuery(scd); */
                }

                // Fim - Tabela Pedido2                
            }
            else
            {
                _idPedidoVenda = clsInfo.zpedido;
                _idPedidoVendaItem = clsInfo.zpedido1;
                _idPedidoVendaItemPrg = clsInfo.zpedido2;
            }
            //--

            // Inicio - Tabela Ordem Servico
            //
            if (_idOrdemServico == 0 || _idOrdemServico == clsInfo.zordemservico)
            {
                /*                scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "SELECT TOP 1 @NUMERO = NUMERO  FROM ORDEMSERVICO WHERE YEAR(@DATA) = YEAR(GETDATE()) ORDER BY NUMERO DESC " +
                                                                                    "if  @NUMERO > 0 " +
                                                                                        "SET @NUMERO = @NUMERO + 1 " +
                                                                                    "ELSE " +
                                                                                        "SET @NUMERO = 1 " +
                scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "SELECT @NUMERO = MAX(NUMERO + 1)  FROM ORDEMSERVICO WHERE YEAR(DATA) = YEAR(GETDATE()) AND FILIAL = @FILIAL  " +
                                                                    "INSERT INTO ORDEMSERVICO (" +
                                                                        "FILIAL,NUMERO,ANO, AUXILIAR,DATA,IDCLIENTE,SITUACAO,SITUACAOITEM,OBSERVA,OBSERVA1,DATATERMINO,TEMPOUNI,TEMPO,TEMPOGASTO,TEMPOTOTAL,QTDEPEDIDO,QTDEOK,QTDEDEFEITO,QTDEMORTA,QTDEBAIXADA,QTDEOSAUX,QTDESALDO,PESOBRUTO,DATAENTREGA,LIBERADOPROCESSO,LIBERADOUSUARIO,LIBERADODATA,OS1,OS2,QT1,QT2,TOTQTPED,TOTQTEST,TOTQTSAL,TIPOTRANSPORTE,EMITENTE,CAIXA,DATALANCA,IDOS,CONJUNTO,QTDE1,QTDE2,IDCODIGO,COMPLEMENTO,QTDECLIENTE,QTDEESTOQUE,IDUNID,IDPEDIDO,IDPEDIDOITEM,IDORCAMENTO,IDORCAMENTO1,IDCODIGOSAIDA,QTDESAIDA,SETOR,IDFUNCIONARIO,IDNFCOMPRA,NFCOMPRA,DATANFE,IDNFCOMPRAITEM,OSSITUACAO,OSCT,OSMAQUINA,OSOPERACAO,OSTIPOMARCACAO,DIASPERMANENCIASUB,IDTOLERANCIA,CLASSE,IDFAMILIA,TIPODAORDEM,IDTIPOMP,PRAZODIASINT,PRAZODIAS,LOTEFABRICA,LOTEFABRICAMIN,OSTRANSF " +
                                                                        ") VALUES ( " +
                                                                        "@FILIAL,@NUMERO,@ANO, @AUXILIAR,@DATA,@IDCLIENTE,@SITUACAO,@SITUACAOITEM,@OBSERVA,@OBSERVA1,@DATATERMINO,@TEMPOUNI,@TEMPO,@TEMPOGASTO,@TEMPOTOTAL,@QTDEPEDIDO,@QTDEOK,@QTDEDEFEITO,@QTDEMORTA,@QTDEBAIXADA,@QTDEOSAUX,@QTDESALDO,@PESOBRUTO,@DATAENTREGA,@LIBERADOPROCESSO,@LIBERADOUSUARIO,@LIBERADODATA,@OS1,@OS2,@QT1,@QT2,@TOTQTPED,@TOTQTEST,@TOTQTSAL,@TIPOTRANSPORTE,@EMITENTE,@CAIXA,@DATALANCA,@IDOS,@CONJUNTO,@QTDE1,@QTDE2,@IDCODIGO,@COMPLEMENTO,@QTDECLIENTE,@QTDEESTOQUE,@IDUNID,@IDPEDIDO,@IDPEDIDOITEM,@IDORCAMENTO,@IDORCAMENTO1,@IDCODIGOSAIDA,@QTDESAIDA,@SETOR,@IDFUNCIONARIO,@IDNFCOMPRA,@NFCOMPRA,@DATANFE,@IDNFCOMPRAITEM,@OSSITUACAO,@OSCT,@OSMAQUINA,@OSOPERACAO,@OSTIPOMARCACAO,@DIASPERMANENCIASUB,@IDTOLERANCIA,@CLASSE,@IDFAMILIA,@TIPODAORDEM,@IDTIPOMP,@PRAZODIASINT,@PRAZODIAS,@LOTEFABRICA,@LOTEFABRICAMIN,@OSTRANSF " +
                                                                        "); SELECT SCOPE_IDENTITY()", CommandType.Text);
*/
                _idOrdemServico = 0;
            }
            else
            {
                /*                scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "UPDATE ORDEMSERVICO SET " +
                                                        "FILIAL = @FILIAL,NUMERO= @NUMERO,ANO= @ANO,AUXILIAR= @AUXILIAR, DATA= @DATA,IDCLIENTE= @IDCLIENTE,SITUACAO= @SITUACAO, SITUACAOITEM= @SITUACAOITEM, OBSERVA= @OBSERVA, OBSERVA1= @OBSERVA1, DATATERMINO= @DATATERMINO,TEMPOUNI= @TEMPOUNI, TEMPO= @TEMPO, TEMPOGASTO= @TEMPOGASTO, TEMPOTOTAL= @TEMPOTOTAL, QTDEPEDIDO= @QTDEPEDIDO, QTDEOK= @QTDEOK, QTDEDEFEITO= @QTDEDEFEITO, QTDEMORTA= @QTDEMORTA, QTDEBAIXADA= @QTDEBAIXADA, QTDEOSAUX= @QTDEOSAUX, QTDESALDO= @QTDESALDO, PESOBRUTO= @PESOBRUTO, DATAENTREGA= @DATAENTREGA,LIBERADOPROCESSO= @LIBERADOPROCESSO, LIBERADOUSUARIO= @LIBERADOUSUARIO, LIBERADODATA= @LIBERADODATA,OS1= @OS1,OS2= @OS2,QT1= @QT1, QT2= @QT2, TOTQTPED= @TOTQTPED, TOTQTEST= @TOTQTEST, TOTQTSAL= @TOTQTSAL, TIPOTRANSPORTE= @TIPOTRANSPORTE, EMITENTE= @EMITENTE, CAIXA= @CAIXA, DATALANCA= @DATALANCA,IDOS= @IDOS,CONJUNTO= @CONJUNTO,QTDE1= @QTDE1, QTDE2= @QTDE2, IDCODIGO= @IDCODIGO,CODIGO= @CODIGO, DESCRICAO= @DESCRICAO, COMPLEMENTO= @COMPLEMENTO, QTDECLIENTE= @QTDECLIENTE, QTDEESTOQUE= @QTDEESTOQUE, IDUNID= @IDUNID, IDPEDIDO= @IDPEDIDO, IDPEDIDOITEM= @IDPEDIDOITEM,IDORCAMENTO= @IDORCAMENTO, IDORCAMENTO1= @IDORCAMENTO1, IDCODIGOSAIDA= @IDCODIGOSAIDA,QTDESAIDA= @QTDESAIDA, SETOR= @SETOR, IDFUNCIONARIO= @IDFUNCIONARIO,IDNFCOMPRA= @IDNFCOMPRA,NFCOMPRA= @NFCOMPRA,DATANFE= @DATANFE,IDNFCOMPRAITEM= @IDNFCOMPRAITEM,OSSITUACAO= @OSSITUACAO, OSCT= @OSCT, OSMAQUINA= @OSMAQUINA,OSOPERACAO= @OSOPERACAO,OSTIPOMARCACAO= @OSTIPOMARCACAO, DIASPERMANENCIASUB= @DIASPERMANENCIASUB,IDTOLERANCIA= @IDTOLERANCIA, CLASSE= @CLASSE, IDFAMILIA= @IDFAMILIA,TIPODAORDEM= @TIPODAORDEM, IDTIPOMP= @IDTIPOMP,PRAZODIASINT= @PRAZODIASINT,PRAZODIAS= @PRAZODIAS,LOTEFABRICA= @LOTEFABRICA, LOTEFABRICAMIN= @LOTEFABRICAMIN, OSTRANSF= @OSTRANSF " +
                                                        " WHERE ID = @ID", CommandType.Text);
                                scd.Parameters.AddWithValue("@ID", _idOrdemServico); */
            }
            /*            scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = clsInfo.zfilial;
                        scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from ordemservico where id=" + _idOrdemServico));
                        scd.Parameters.Add("@ANO", SqlDbType.Int).Value = DateTime.Now.Year;
                        scd.Parameters.Add("@AUXILIAR", SqlDbType.NVarChar).Value = "N";
                        scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DateTime.Now;
                        scd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = idfornecedorpedido;
                        scd.Parameters.Add("@SITUACAO", SqlDbType.NVarChar).Value = "20";
                        scd.Parameters.Add("@SITUACAOITEM", SqlDbType.NVarChar).Value = tipoentrada;
                        scd.Parameters.Add("@OBSERVA", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@OBSERVA1", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@DATATERMINO", SqlDbType.DateTime).Value = DateTime.Now;
                        scd.Parameters.Add("@TEMPOUNI", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@TEMPO", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@TEMPOGASTO", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@TEMPOTOTAL", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@QTDEPEDIDO", SqlDbType.Decimal).Value = clsNfCompra1Info.qtde;
                        scd.Parameters.Add("@QTDEOK", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@QTDEDEFEITO", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@QTDEMORTA", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@QTDEBAIXADA", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@QTDEOSAUX", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@QTDESALDO", SqlDbType.Decimal).Value = clsNfCompra1Info.qtde;
                        scd.Parameters.Add("@PESOBRUTO", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@DATAENTREGA", SqlDbType.DateTime).Value = clsNfCompra1Info.dataentrega;
                        scd.Parameters.Add("@LIBERADOPROCESSO", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@LIBERADOUSUARIO", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@LIBERADODATA", SqlDbType.DateTime).Value = DateTime.Now;
                        scd.Parameters.Add("@OS1", SqlDbType.Int).Value = 0;
                        scd.Parameters.Add("@OS2", SqlDbType.Int).Value = 0;
                        scd.Parameters.Add("@QT1", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@QT2", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@TOTQTPED", SqlDbType.Decimal).Value = clsNfCompra1Info.qtde;
                        scd.Parameters.Add("@TOTQTEST", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@TOTQTSAL", SqlDbType.Decimal).Value = clsNfCompra1Info.qtde;
                        scd.Parameters.Add("@TIPOTRANSPORTE", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = clsInfo.zusuario;
                        scd.Parameters.Add("@CAIXA", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@DATALANCA", SqlDbType.DateTime).Value = DateTime.Now;
                        scd.Parameters.Add("@IDOS", SqlDbType.Int).Value = clsInfo.zordemservico;
                        scd.Parameters.Add("@CONJUNTO", SqlDbType.Int).Value = 0;
                        scd.Parameters.Add("@QTDE1", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@QTDE2", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = clsNfCompra1Info.idcodigo;
                        scd.Parameters.Add("@COMPLEMENTO", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@NROREVISAO", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@QTDECLIENTE", SqlDbType.Decimal).Value = clsNfCompra1Info.qtde;
                        scd.Parameters.Add("@QTDEESTOQUE", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@IDUNID", SqlDbType.Int).Value = clsNfCompra1Info.idunid; ;
                        scd.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = _idPedidoVenda;
                        scd.Parameters.Add("@IDPEDIDOITEM", SqlDbType.Int).Value = _idPedidoVendaItem;
                        scd.Parameters.Add("@IDORCAMENTO", SqlDbType.Int).Value = clsInfo.zorcamento;
                        scd.Parameters.Add("@IDORCAMENTO1", SqlDbType.Int).Value = clsInfo.zorcamento1;
                        //            scd.Parameters.Add("@DATAADMINI", SqlDbType.DateTime).Value = null;
                        scd.Parameters.Add("@IDCODIGOSAIDA", SqlDbType.Int).Value = clsInfo.zpecas;
                        scd.Parameters.Add("@QTDESAIDA", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@SETOR", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@IDFUNCIONARIO", SqlDbType.Int).Value = 1;
                        scd.Parameters.Add("@IDNFCOMPRA", SqlDbType.Int).Value = id;
                        scd.Parameters.Add("@NFCOMPRA", SqlDbType.Int).Value = clsParser.Int32Parse(tbxNumero.Text);
                        scd.Parameters.Add("@DATANFE", SqlDbType.DateTime).Value = "";//Parser.SqlDateTimeParse(mkdData.Text); ;
                        scd.Parameters.Add("@IDNFCOMPRAITEM", SqlDbType.Int).Value = "";//indexnfcompraitem;
                        scd.Parameters.Add("@FUNCIONARIOPROCE", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@REVISAOPROCESSO", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@REVISAORIP", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@REVISAORIF", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@DATAREVPROCE", SqlDbType.DateTime).Value = DateTime.Now;
                        scd.Parameters.Add("@OSSITUACAO", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@OSCT", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@OSMAQUINA", SqlDbType.Int).Value = 1;
                        scd.Parameters.Add("@OSOPERACAO", SqlDbType.Int).Value = 1;
                        scd.Parameters.Add("@OSTIPOMARCACAO", SqlDbType.NVarChar).Value = "";
                        // scd.Parameters.Add("@OSDATAINI", SqlDbType.DateTime).Value = null;
                        // scd.Parameters.Add("@OSDATAFIM", SqlDbType.DateTime).Value = null;
                        scd.Parameters.Add("@DIASPERMANENCIASUB", SqlDbType.Int).Value = 0;
                        scd.Parameters.Add("@IDTOLERANCIA", SqlDbType.Int).Value = clsInfo.ztolerancia;
                        scd.Parameters.Add("@CLASSE", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@IDFAMILIA", SqlDbType.Int).Value = clsInfo.zpecasfamilia;
                        scd.Parameters.Add("@TIPODAORDEM", SqlDbType.NVarChar).Value = "";
                        scd.Parameters.Add("@IDTIPOMP", SqlDbType.Int).Value = clsInfo.zpecas;
                        scd.Parameters.Add("@PRAZODIASINT", SqlDbType.Int).Value = 0;
                        scd.Parameters.Add("@PRAZODIAS", SqlDbType.Int).Value = 0;
                        scd.Parameters.Add("@LOTEFABRICA", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@LOTEFABRICAMIN", SqlDbType.Decimal).Value = 0;
                        scd.Parameters.Add("@OSTRANSF", SqlDbType.NVarChar).Value = "N";
            */
            if (_idOrdemServico == 0 || _idOrdemServico == clsInfo.zordemservico)
            {
                /*                _idOrdemServico = clsSql.ExecuteQuery(scd); */
            }

            // Fim - Tabela Ordem Servico                
            //--
            // Inicio - Atualizando o Pedido1 com o ID da OS e numero da O.S no campo rastreablilidade do cliente
            //
            /*
                        scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "UPDATE PEDIDO1 SET " +
                                                    "IDORDEMSERVICO = @IDORDEMSERVICO, CODIGOEMP01 = @ORDEMSERVICO " +
                                                    " WHERE ID = @ID", CommandType.Text);
  
                        scd.Parameters.AddWithValue("@ID", _idPedidoVendaItem);
                        scd.Parameters.AddWithValue("@IDORDEMSERVICO", _idOrdemServico);
                        scd.Parameters.AddWithValue("@ORDEMSERVICO", clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from ordemservico where id=" + _idOrdemServico)));
                        clsSql.ExecuteQuery(scd);

                        scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "UPDATE PEDIDO2 SET " +
                                                    "IDORDEMSERVICO = @IDORDEMSERVICO " +
                                                    " WHERE ID = @ID", CommandType.Text);
                        scd.Parameters.AddWithValue("@ID", _idPedidoVendaItemPrg);
                        scd.Parameters.AddWithValue("@IDORDEMSERVICO", _idOrdemServico);
                        clsSql.ExecuteQuery(scd);
                        // Fim - Atualizando o Pedido1 com o ID da OS e numero da O.S no campo rastreablilidade do cliente
                        scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "UPDATE NFCOMPRA1 SET " +
                                                    "IDORDEMSERVICO = @IDORDEMSERVICO, IDPEDIDOVENDA = @IDPEDIDOVENDA , IDPEDIDOVENDAITEM = @IDPEDIDOVENDAITEM " +
                                                    " WHERE ID = @ID", CommandType.Text);
                        scd.Parameters.AddWithValue("@ID", _idNFCompra1);
                        scd.Parameters.AddWithValue("@IDORDEMSERVICO", _idOrdemServico);
                        scd.Parameters.AddWithValue("@IDPEDIDOVENDA", _idPedidoVenda);
                        scd.Parameters.AddWithValue("@IDPEDIDOVENDAITEM", _idPedidoVendaItem);
                        clsSql.ExecuteQuery(scd);


                        if (clsNfCompra1Info.codigoemp01.Length > 0)
                        {
                            scd = clsSql.CriarCommand(clsInfo.conexaosqldados, "UPDATE NFCOMPRA1 SET " +
                                                        "CODIGOEMP01 = @CODIGOEMP01 " +
                                                        " WHERE ID = @ID", CommandType.Text);
                            scd.Parameters.AddWithValue("@ID", _idNFCompra1);
                            scd.Parameters.AddWithValue("@CODIGOEMP01", clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from ordemservico where id=" + _idOrdemServico)));
                            clsSql.ExecuteQuery(scd);
                        }
            */
            //
            nfcompra1_idpedidovenda = _idPedidoVenda;
            nfcompra1_idpedidovendaitem = _idPedidoVendaItem;
            nfcompra1_idordemservico = _idOrdemServico;
        }

        private void NFCompra1_ckxCalculoautomatico_CheckedChanged(object sender, EventArgs e)
        {
            if (NFCompra1_ckxCalculoautomatico.Checked == true)
            {
                //NFCompra1_ckxCalculoautomatico.Text = "Calculo Automatico";

                NFCompra1_tbxPesoTotal.ReadOnly = true;
                NFCompra1_tbxPesoTotal.TabStop = false;
                NFCompra1_tbxPesoTotal.BackColor = Color.LemonChiffon;

                NFCompra1_tbxTotalmercado.ReadOnly = true;
                NFCompra1_tbxTotalmercado.TabStop = false;
                NFCompra1_tbxTotalmercado.BackColor = Color.LemonChiffon;

                NFCompra1_tbxPrecoCustounit.Enabled = false;
                NFCompra1_tbxPrecoCustounit.ReadOnly = true;
                NFCompra1_tbxPrecoCustounit.TabStop = false;
                NFCompra1_tbxPrecoCustounit.BackColor = Color.LemonChiffon;

                NFCompra1_tbxBcipi.ReadOnly = true;
                NFCompra1_tbxBcipi.TabStop = false;
                NFCompra1_tbxBcipi.BackColor = Color.LemonChiffon;

                NFCompra1_tbxCustoipi.ReadOnly = true;
                NFCompra1_tbxCustoipi.TabStop = false;
                NFCompra1_tbxCustoipi.BackColor = Color.LemonChiffon;

                NFCompra1_tbxBaseicm.ReadOnly = true;
                NFCompra1_tbxBaseicm.TabStop = false;
                NFCompra1_tbxBaseicm.BackColor = Color.LemonChiffon;

                NFCompra1_tbxCustoicm.ReadOnly = true;
                NFCompra1_tbxCustoicm.TabStop = false;
                NFCompra1_tbxCustoicm.BackColor = Color.LemonChiffon;

                NFCompra1_tbxBaseicmsubst.ReadOnly = true;
                NFCompra1_tbxBaseicmsubst.TabStop = false;
                NFCompra1_tbxBaseicmsubst.BackColor = Color.LemonChiffon;

                NFCompra1_tbxIcmsubst.ReadOnly = true;
                NFCompra1_tbxIcmsubst.TabStop = false;
                NFCompra1_tbxIcmsubst.BackColor = Color.LemonChiffon;

                NFCompra1_tbxBcpispasep.ReadOnly = true;
                NFCompra1_tbxBcpispasep.TabStop = false;
                NFCompra1_tbxBcpispasep.BackColor = Color.LemonChiffon;

                NFCompra1_tbxPispasep.ReadOnly = true;
                NFCompra1_tbxPispasep.TabStop = false;
                NFCompra1_tbxPispasep.BackColor = Color.LemonChiffon;

                NFCompra1_tbxBccofins1.ReadOnly = true;
                NFCompra1_tbxBccofins1.TabStop = false;
                NFCompra1_tbxBccofins1.BackColor = Color.LemonChiffon;

                NFCompra1_tbxCofins1.ReadOnly = true;
                NFCompra1_tbxCofins1.TabStop = false;
                NFCompra1_tbxCofins1.BackColor = Color.LemonChiffon;

                NFCompra1_tbxTotalnota.ReadOnly = true;
                NFCompra1_tbxTotalnota.TabStop = false;
                NFCompra1_tbxTotalnota.BackColor = Color.LemonChiffon;

                NFCompra1_tbxIrrf.ReadOnly = true;
                NFCompra1_tbxIrrf.TabStop = false;
                NFCompra1_tbxIrrf.BackColor = Color.LemonChiffon;

                NFCompra1_tbxInss.ReadOnly = true;
                NFCompra1_tbxInss.TabStop = false;
                NFCompra1_tbxInss.BackColor = Color.LemonChiffon;

                NFCompra1_tbxPisCofinsCsll.ReadOnly = true;
                NFCompra1_tbxPisCofinsCsll.TabStop = false;
                NFCompra1_tbxPisCofinsCsll.BackColor = Color.LemonChiffon;

                NFCompra1_tbxIss.ReadOnly = true;
                NFCompra1_tbxIss.TabStop = false;
                NFCompra1_tbxIss.BackColor = Color.LemonChiffon;

                NFCompra1_tbxPis.ReadOnly = true;
                NFCompra1_tbxPis.TabStop = false;
                NFCompra1_tbxPis.BackColor = Color.LemonChiffon;

                NFCompra1_tbxCofins.ReadOnly = true;
                NFCompra1_tbxCofins.TabStop = false;
                NFCompra1_tbxCofins.BackColor = Color.LemonChiffon;

                NFCompra1_tbxCsll.ReadOnly = true;
                NFCompra1_tbxCsll.TabStop = false;
                NFCompra1_tbxCsll.BackColor = Color.LemonChiffon;
            }
            else
            {
                //NFCompra1_ckxCalculoautomatico.Text = "Calculo Manual";

                NFCompra1_tbxPesoTotal.ReadOnly = false;
                NFCompra1_tbxPesoTotal.TabStop = true;
                NFCompra1_tbxPesoTotal.BackColor = Color.White;

                NFCompra1_tbxTotalmercado.ReadOnly = false;
                NFCompra1_tbxTotalmercado.TabStop = true;
                NFCompra1_tbxTotalmercado.BackColor = Color.White;

                NFCompra1_tbxPrecoCustounit.Enabled = true;
                NFCompra1_tbxPrecoCustounit.ReadOnly = false;
                NFCompra1_tbxPrecoCustounit.TabStop = true;
                NFCompra1_tbxPrecoCustounit.BackColor = Color.White;

                NFCompra1_tbxBcipi.ReadOnly = false;
                NFCompra1_tbxBcipi.TabStop = true;
                NFCompra1_tbxBcipi.BackColor = Color.White;

                NFCompra1_tbxCustoipi.ReadOnly = false;
                NFCompra1_tbxCustoipi.TabStop = true;
                NFCompra1_tbxCustoipi.BackColor = Color.White;

                NFCompra1_tbxBaseicm.ReadOnly = false;
                NFCompra1_tbxBaseicm.TabStop = true;
                NFCompra1_tbxBaseicm.BackColor = Color.White;

                NFCompra1_tbxCustoicm.ReadOnly = false;
                NFCompra1_tbxCustoicm.TabStop = true;
                NFCompra1_tbxCustoicm.BackColor = Color.White;

                NFCompra1_tbxBaseicmsubst.ReadOnly = false;
                NFCompra1_tbxBaseicmsubst.TabStop = true;
                NFCompra1_tbxBaseicmsubst.BackColor = Color.White;

                NFCompra1_tbxIcmsubst.ReadOnly = false;
                NFCompra1_tbxIcmsubst.TabStop = true;
                NFCompra1_tbxIcmsubst.BackColor = Color.White;

                NFCompra1_tbxBcpispasep.ReadOnly = false;
                NFCompra1_tbxBcpispasep.TabStop = true;
                NFCompra1_tbxBcpispasep.BackColor = Color.White;

                NFCompra1_tbxPispasep.ReadOnly = false;
                NFCompra1_tbxPispasep.TabStop = true;
                NFCompra1_tbxPispasep.BackColor = Color.White;

                NFCompra1_tbxBccofins1.ReadOnly = false;
                NFCompra1_tbxBccofins1.TabStop = true;
                NFCompra1_tbxBccofins1.BackColor = Color.White;

                NFCompra1_tbxCofins1.ReadOnly = false;
                NFCompra1_tbxCofins1.TabStop = true;
                NFCompra1_tbxCofins1.BackColor = Color.White;

                NFCompra1_tbxTotalnota.ReadOnly = false;
                NFCompra1_tbxTotalnota.TabStop = true;
                NFCompra1_tbxTotalnota.BackColor = Color.White;

                NFCompra1_tbxIrrf.ReadOnly = false;
                NFCompra1_tbxIrrf.TabStop = true;
                NFCompra1_tbxIrrf.BackColor = Color.White;

                NFCompra1_tbxInss.ReadOnly = false;
                NFCompra1_tbxInss.TabStop = true;
                NFCompra1_tbxInss.BackColor = Color.White;

                NFCompra1_tbxPisCofinsCsll.ReadOnly = false;
                NFCompra1_tbxPisCofinsCsll.TabStop = true;
                NFCompra1_tbxPisCofinsCsll.BackColor = Color.White;

                NFCompra1_tbxIss.ReadOnly = false;
                NFCompra1_tbxIss.TabStop = true;
                NFCompra1_tbxIss.BackColor = Color.White;

                NFCompra1_tbxPis.ReadOnly = false;
                NFCompra1_tbxPis.TabStop = true;
                NFCompra1_tbxPis.BackColor = Color.White;

                NFCompra1_tbxCofins.ReadOnly = false;
                NFCompra1_tbxCofins.TabStop = true;
                NFCompra1_tbxCofins.BackColor = Color.White;

                NFCompra1_tbxCsll.ReadOnly = false;
                NFCompra1_tbxCsll.TabStop = true;
                NFCompra1_tbxCsll.BackColor = Color.White;
            }
        }

        //private void btnNFE_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ofdNFE.FileName = ""; //Limpa o campo do nome do arquivo quando o a janela aparecer

        //        this.ofdNFE.Filter = " XML Files|*.xml";

        //        this.ofdNFE.ShowDialog(); //mostra a janela

        //        if (this.ofdNFE.FileName.ToString().Length > 1)
        //        {
        //            caminhoXML = this.ofdNFE.FileName.ToString();
        //            //
        //            XmlDocument doc = new XmlDocument();
        //            doc.Load(caminhoXML.ToString());

        //            //verifica se é Nota Fiscal ou Conheciemnto Rodoviario
        //            var objLista = doc.GetElementsByTagName("*");

        //            for (int i = 0; i <= (objLista.ToString().Length - 1); )
        //            {
        //                if (objLista.Item(i).Name == "chCTe" ||
        //                    objLista.Item(i).Name == "cteProc" ||
        //                    objLista.Item(i).Name == "CTe")
        //                {
        //                    //é conhecimento
        //                    var infEvento = doc.GetElementsByTagName("infProt");
        //                    //chave
        //                    foreach (XmlElement nodo in infEvento)
        //                    {
        //                        tbxChNFe.Text = nodo.GetElementsByTagName("chCTe")[0].InnerText.Trim();
        //                    }
        //                    //numero 
        //                    infEvento = doc.GetElementsByTagName("infCte");
        //                    foreach (XmlElement nodo in infEvento)
        //                    {
        //                        tbxNumero.Text = nodo.GetElementsByTagName("nCT")[0].InnerText.Trim();
        //                    }
        //                    //caminho novo
        //                    tbxCaminhoNFE.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
        //                        "select NFe_XSDPASTA FROM EMPRESA where id = " + clsInfo.zempresaid, "").ToString() +
        //                        "\\XML_NFE_ENTRADA" +
        //                        ofdNFE.FileName.Substring(ofdNFE.FileName.ToString().LastIndexOf("\\"));
        //                    return;
        //                }
        //                else
        //                {
        //                    //é Nota fiscal
        //                    var infEvento = doc.GetElementsByTagName("infProt");
        //                    //chave
        //                    foreach (XmlElement nodo in infEvento)
        //                    {
        //                        tbxChNFe.Text = nodo.GetElementsByTagName("chNFe")[0].InnerText.Trim();
        //                    }
        //                    //numero 
        //                    infEvento = doc.GetElementsByTagName("infNFe");
        //                    foreach (XmlElement nodo in infEvento)
        //                    {
        //                        tbxNumero.Text = nodo.GetElementsByTagName("nNF")[0].InnerText.Trim();
        //                    }
        //                    //caminho novo
        //                    tbxCaminhoNFE.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
        //                        "select NFe_XSDPASTA FROM EMPRESA where id = " + clsInfo.zempresaid, "").ToString() +
        //                        "\\XML_NFE_ENTRADA" +
        //                        ofdNFE.FileName.Substring(ofdNFE.FileName.ToString().LastIndexOf("\\"));
        //                    return;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Nenhum arquivo selecionado");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        tbxNumero.Text = "";
        //        caminhoXML = "";
        //        caminhoPDF = "";
        //        tbxCaminhoNFE.Text = "";
        //        tbxCaminhoPDF.Text = "";
        //        MessageBox.Show("Erro na leitura do XML");
        //    }
        //}

        private void tclResolve()
        {
            if (painelPrincipal == 0)
            {
                tclNFCompra.SelectedIndex = 0;
            }
            else if (painelPrincipal == 1)
            {
                tclNFCompra.SelectedIndex = 1;
            }
            else if (painelPrincipal == 2)
            {
                tclNFCompra.SelectedIndex = 2;
                if (tclNFCompra.SelectedIndex == 2)
                {
                    //XML
                    if (File.Exists(caminhoXML.ToString()) == true)
                    {
                        //webBrowser1.Location = new System.Drawing.Point(0, 0);
                        //webBrowser1.Size = new System.Drawing.Size(988, 544);
                        //webBrowser1.Visible = true;
                        //webBrowser1.Navigate(caminhoXML.ToString());
                    }
                    else
                    {
                        //webBrowser1.Visible = false;
                        //MessageBox.Show("Caminho do arquivo XML não existe ou não indicado, favor verificar!",
                        //    "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //PDF
                    if (File.Exists(caminhoPDF.ToString()) == true)
                    {
                        //axAcroPDF1.Location = new System.Drawing.Point(0, 0);
                        //axAcroPDF1.Size = new System.Drawing.Size(988, 544);
                        //axAcroPDF1.Visible = true;
                        //axAcroPDF1.Refresh();
                        //axAcroPDF1.LoadFile(caminhoPDF.ToString());
                    }
                    else
                    {
                        //axAcroPDF1.Refresh();
                        //axAcroPDF1.Visible = false;
                        //MessageBox.Show("Caminho do arquivo PDF não existe ou não indicado, favor verificar!",
                        //    "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void tclNFCompra_Click(object sender, EventArgs e)
        {
            tclResolve();
        }

        private void btnVisualizaXML_Click(object sender, EventArgs e)
        {
            painelPrincipal = 2;
            tclResolve();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                this.ofdNFE.FileName = ""; //Limpa o campo do nome do arquivo quando o a janela aparecer

                this.ofdNFE.Filter = " PDF|*.pdf";

                this.ofdNFE.ShowDialog(); //mostra a janela

                if (this.ofdNFE.FileName.ToString().Length > 1)
                {
                    caminhoPDF = this.ofdNFE.FileName.ToString();
                    //tbxCaminhoPDF.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                    //    "select NFe_XSDPASTA FROM EMPRESA where id = " + clsInfo.zempresaid, "").ToString() +
                    //    "\\PDF_NFE_ENTRADA" +
                    //    ofdNFE.FileName.Substring(ofdNFE.FileName.ToString().LastIndexOf("\\"));

                    ////PDF
                    //if (File.Exists(caminhoPDF.ToString()) == true)
                    //{
                    //    axAcroPDF1.Location = new System.Drawing.Point(0, 0);
                    //    axAcroPDF1.Size = new System.Drawing.Size(988, 544);
                    //    axAcroPDF1.Visible = true;
                    //    axAcroPDF1.Refresh();
                    //    axAcroPDF1.LoadFile(caminhoPDF.ToString());
                    //}
                    //else
                    //{
                    //    axAcroPDF1.Refresh();
                    //    axAcroPDF1.Visible = false;
                    //    MessageBox.Show("Caminho do arquivo PDF não existe ou não indicado, favor verificar!",
                    //        "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
                else
                {
                    MessageBox.Show("Nenhum arquivo selecionado");
                }
            }
            catch (Exception)
            {
                //caminhoPDF = "";
                //tbxCaminhoPDF.Text = "";
                MessageBox.Show("Erro na leitura do PDF");
            }
        }

        private void tspRetornarXML_Click(object sender, EventArgs e)
        {
            painelPrincipal = 0;
            tclResolve();
        }

        private void tclNFCompra_Click_1(object sender, EventArgs e)
        {
            tclResolve();
        }

        private void tabNota_Click(object sender, EventArgs e)
        {

        }

        private void tbxCliente_Cognome_TextChanged(object sender, EventArgs e)
        {

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            // Salvar
            if (tspSalvar.Enabled == true)
            {
                Salvar();
            }
            // Imprimir Pedido Venda
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

            frmCrystalReport.Init(clsInfo.caminhorelatorios, "ENTRADA_FERPAL.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

        }

        private void NFCompra1_tbxComplemento1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gbxNFCompra1_Enter(object sender, EventArgs e)
        {

        }

        private void NFCompra1_btnSittribpis_Click_1(object sender, EventArgs e)
        {

        }
    }
}
