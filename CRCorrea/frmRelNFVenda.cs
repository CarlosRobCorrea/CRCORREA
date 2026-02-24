using CRCorreaFuncoes;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRelNFVenda : Form
    {
        

        Int32 idfornecedor;
        Int32 idfornecedor1;
        Int32 idClassifica;
        Int32 idClassificaa;
        Int32 idClassifica1;
        Int32 idClassifica1a;
        Int32 idClassifica2;
        Int32 idClassifica2a;

        

        delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        ParameterFields pfields = new ParameterFields();

        String query;
        String ordem;
        String cabecalho;
        String tipocli;

        public frmRelNFVenda()
        {
            InitializeComponent();
        }

        public void Init()
        {
            

            pfields = new ParameterFields();

            // Carrega os AutoComplete
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResClienteDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResClienteAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResVendedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResVendedorAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxItemVendedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxItemVendedorAte);

            // Determina os Filtros Padrões
            DateTime daData = Convert.ToDateTime("01/" + DateTime.Now.ToString("MM/yyyy") + " 00:00");
            DateTime ateData = Convert.ToDateTime(daData.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy") + " 23:59");

            // Cabeçalho
            rbnResTipoSimples.Checked = true;
            rbnResCadastroTodos.Checked = true;
            rbnResAnalitica.Checked = true;
            rbnResSemSub.Checked = true;
            lbxResSituacao.SelectedIndex = 1;
            lbxResOrdem.SelectedIndex = 1;
            tbxResDtEmissaoDe.Text = daData.ToString("dd/MM/yyyy HH:mm");
            tbxResDtEmissaoAte.Text = ateData.ToString("dd/MM/yyyy HH:mm");

            // Itens
            rbnItemCodigo.Checked = true;
            lbxItemSituacaoNota.SelectedIndex = 1;
            lbxItemTipoProduto.SelectedIndex = 0;
            lbxItemTipoEntrada.SelectedIndex = 1;
            lbxItemOrdem.SelectedIndex = 0;
            lbxItemConsumo.SelectedIndex = 0;
            tbxItemDtEmissaoDe.Text = daData.ToString("dd/MM/yyyy HH:mm:ss");
            tbxItemDtEmissaoAte.Text = ateData.ToString("dd/MM/yyyy HH:mm:ss");

            // Apuração
            rbnApuFornecedor.Checked = true;
            lbxApuSituacaoNota.SelectedIndex = 1;
            lbxApuTipoEntrada.SelectedIndex = 1;
            rbnApuModelo1.Checked = true;
            rbnApuTodos.Checked = true;

            tbxApuDtEmissaoDe.Text = Convert.ToDateTime("01/01/" + DateTime.Now.ToString("yyyy") + " 00:00").ToString();
            tbxApuDtEmissaoAte.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            tabFiltros.SelectedIndex = 2;

            // Especifica
            tbxEspDtEmissaoDe.Text = daData.ToString("dd/MM/yyyy HH:mm:ss");
            tbxEspDtEmissaoAte.Text = ateData.ToString("dd/MM/yyyy HH:mm:ss");
            lbxEspOrdem.SelectedIndex = 0;
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownDataHora(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownDataHora((TextBox)sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        public void Lupa()
        {
            if (clsInfo.znomegrid != null &&
                clsInfo.znomegrid != "" &&
                clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == btnResFornecedorDe.Name)
                {
                    tbxResClienteDe.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxResClienteDe.Select();
                }
                else if (clsInfo.znomegrid == btnResFornecedorAte.Name)
                {
                    tbxResClienteAte.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString(); ;
                    tbxResClienteAte.Select();
                }
                else if (clsInfo.znomegrid == btnResVendedorDe.Name)
                {
                    tbxResVendedorDe.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxResVendedorDe.Select();
                }
                else if (clsInfo.znomegrid == btnResVendedorAte.Name)
                {
                    tbxResVendedorAte.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxResVendedorAte.Select();
                }
                else if (clsInfo.znomegrid == btnItemVendedorDe.Name)
                {
                    tbxItemVendedorDe.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxItemVendedorDe.Select();
                }
                else if (clsInfo.znomegrid == btnItemVendedorAte.Name)
                {
                    tbxItemVendedorAte.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    tbxItemVendedorAte.Select();
                }
            }

            clsInfo.zrow = null;
            clsInfo.znomegrid = "";
        }

        private void frmRelNFVenda_Activated(object sender, EventArgs e)
        {
            Lupa();
            Lupa2();
            tipocli = "TODOS";
            bwrCarregaAutoComplete_RunWorkerAsync();
        }

        private void frmRelNFVenda_Shown(object sender, EventArgs e)
        {
            //FormHelper.VerificarForm(this, toolTip1);
        }

        private void VerificaFiltros()
        {
            if (rbnItemRemessa.Checked == true)
            {
                rbnModelo1.Text = "Pendencias";
                rbnModelo2.Text = "Movimentação";

                if (rbnModelo1.Checked == true)
                {
                    rbnItemSintetica.Enabled = true;
                    gbxItemSub.Enabled = true;
                }
                else
                {
                    rbnItemSintetica.Enabled = false;
                    gbxItemSub.Enabled = false;
                }

            }
            else
            {
                rbnModelo1.Text = "Modelo1";
                rbnModelo2.Text = "Modelo2";
            }
            lbxItemOrdem.Items.Clear();
            if (rbnItemCodigo.Checked == true)
            {
                lbxItemOrdem.Items.Add("Por Cliente + Nro da Nota Fiscal + Codigo ");
                lbxItemOrdem.Items.Add("Por Codigo + Nro da Nota Fiscal + Cliente ");
                lbxItemOrdem.Items.Add("Por Dt Emissao + Cliente+ Codigo ");
                lbxItemOrdem.Items.Add("Por Nro Nota Fiscal ");
            }
            else if (rbnItemCtaContabil.Checked == true)
            {
                lbxItemOrdem.Items.Add("Por Cliente + Nro da Nota Fiscal + Cta Contabil ");
                lbxItemOrdem.Items.Add("Por Cta Contabil + Nro da Nota Fiscal + Cliente ");
                lbxItemOrdem.Items.Add("Por Dt Emissao + Cliente+ Cta Contabil ");
                lbxItemOrdem.Items.Add("Por Nro Nota Fiscal ");
            }
            else if (rbnItemHistorico.Checked == true)
            {
                lbxItemOrdem.Items.Add("Por Cliente + Nro da Nota Fiscal + Historico ");
                lbxItemOrdem.Items.Add("Por Historico + Nro da Nota Fiscal + Cliente ");
                lbxItemOrdem.Items.Add("Por Dt Emissao + Cliente+ Historico ");
                lbxItemOrdem.Items.Add("Por Nro Nota Fiscal ");
            }
            else if (rbnItemCentroCusto.Checked == true)
            {
                lbxItemOrdem.Items.Add("Por Cliente + Nro da Nota Fiscal + Centro de Custo ");
                lbxItemOrdem.Items.Add("Por Centro de Custo + Nro da Nota Fiscal + Cliente ");
                lbxItemOrdem.Items.Add("Por Dt Emissao + Cliente+ Centro de Custo ");
                lbxItemOrdem.Items.Add("Por Nro Nota Fiscal ");
            }
            else if (rbnItemCFOP.Checked == true)
            {
                lbxItemOrdem.Items.Add("Por Cliente + Nro da Nota Fiscal + Cfop ");
                lbxItemOrdem.Items.Add("Por Cfop + Nro da Nota Fiscal + Cliente ");
                lbxItemOrdem.Items.Add("Por Dt Emissao + Cliente+ Cfop ");
                lbxItemOrdem.Items.Add("Por Nro Nota Fiscal ");
            }
            else if (rbnItemRemessa.Checked == true)
            {

                lbxItemOrdem.Items.Add("Por Numero de Nota + Data da Nota Fiscal + Fornecedor/Cliente");
                if (rbnModelo1.Checked == true)
                {
                    lbxItemOrdem.Items.Add("Por Codigo de Item + Nota Fiscal + Fornecedor/Cliente");
                }
            }
            else if (rbnItemContabilidade.Checked == true)
            {
                lbxItemOrdem.Items.Add("Esta desabilitada por enquanto");
            }

            lbxItemOrdem.SelectedIndex = 0;
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            cabecalho = "";
            query = "";
            ordem = "";

            // Cabeçalho  -- objetivo da lista
            if (rbnResTipoSimples.Checked == true)
            {
                cabecalho = "Relatório Cabeçalho de NF Venda Simples";
            }
            else if (rbnResTipoFaturamento.Checked == true)
            {
                cabecalho = "Relatório de Faturamento";
            }

            // Tipo de Nota / Tipo de Entrada
            if (cbxResFaturamentoS.Checked == true ||
                cbxResFaturamentoE.Checked == true ||
                cbxResFaturamentoC.Checked == true)
            {
                if (query.Length > 0) { query += " AND "; }

                if (cbxResFaturamentoS.Checked == true)
                {
                    query += " (LEFT(NFVENDA.TIPOENTRADA, 1) = 'S' ";
                    cabecalho += Environment.NewLine + "Apenas as Notas de Saida ";

                }
                if (cbxResFaturamentoE.Checked == true)
                {
                    if (cbxResFaturamentoS.Checked == true) { query += " OR "; }
                    else { query += "("; }

                    query += " LEFT(NFVENDA.TIPOENTRADA, 1) = 'E' ";
                    cabecalho += Environment.NewLine + "Apenas as Notas de Entrada ";
                }
                if (cbxResFaturamentoC.Checked == true)
                {
                    if (cbxResFaturamentoS.Checked == true || cbxResFaturamentoE.Checked == true) { query += " OR "; }
                    else { query += "("; }
                    query += " LEFT(NFVENDA.TIPOENTRADA, 1) = 'C' ";

                    cabecalho += Environment.NewLine + "Apenas as Notas Cancelada ";
                }
                query += ")";
            }


            // Tipo Cadastro
            if (rbnResCadastroFornecedor.Checked == true)
            {
                if (query.ToString().Length > 0) { query += " and "; }

                query = query + " cliente.tipo = 'C' ";
                cabecalho += " ( só de Clientes) ";
            }
            else if (rbnResCadastroFornecedor.Checked == true)
            {
                if (query.ToString().Length > 0) { query += " and "; }

                query += " cliente.tipo = 'F' ";
                cabecalho += " ( só de Fornecedor) ";
            }

            // Cliente
            if (tbxResClienteDe.Text.Length > 0 && tbxResClienteAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " CLIENTE.COGNOME >= '" + tbxResClienteDe.Text + "' AND ";
                query += " CLIENTE.COGNOME <= '" + tbxResClienteAte.Text + "' ";

                cabecalho += Environment.NewLine + "Do Cliente: " + tbxResClienteDe.Text + " até " + tbxResClienteAte.Text;
            }
            else if (tbxResClienteDe.Text.Length > 0 && tbxResClienteAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " CLIENTE.COGNOME >= '" + tbxResClienteDe.Text + "' ";

                cabecalho += Environment.NewLine + "A partir do Cliente: " + tbxResClienteDe.Text;
            }
            else if (tbxResClienteDe.Text.Length == 0 && tbxResClienteAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " CLIENTE.COGNOME <= '" + tbxResClienteAte.Text + "' ";

                cabecalho += Environment.NewLine + "Até o Cliente: " + tbxResClienteAte.Text;
            }

            // Data de Emissão
            SqlDateTime EmissaoDe;
            SqlDateTime EmissaoAte;

            EmissaoDe = clsParser.SqlDateTimeParse(tbxResDtEmissaoDe.Text);

            if (tbxResDtEmissaoAte.Text.Length >= 10)
            {
                tbxResDtEmissaoAte.Text = tbxResDtEmissaoAte.Text.Substring(0, 10) + " 23:59:59";
            }

            EmissaoAte = clsParser.SqlDateTimeParse(tbxResDtEmissaoAte.Text);

            if (EmissaoDe.IsNull == false && EmissaoAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATA >= " + clsParser.SqlDateTimeFormat(EmissaoDe, true) + " AND ";
                query += " NFVENDA.DATA <= " + clsParser.SqlDateTimeFormat(EmissaoAte, true);

                cabecalho += Environment.NewLine + "Da Data de Emissão: " + EmissaoDe.Value.ToString("dd/MM/yyyy") + " até " + EmissaoAte.Value.ToString("dd/MM/yyyy");
            }
            else if (EmissaoDe.IsNull == true && EmissaoAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATA <= " + clsParser.SqlDateTimeFormat(EmissaoAte, true);

                cabecalho += Environment.NewLine + "Até a Data de Emissão: " + tbxResDtEmissaoAte.Text;
            }
            else if (EmissaoDe.IsNull == false && EmissaoAte.IsNull == true)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATA >= " + clsParser.SqlDateTimeFormat(EmissaoDe, true);

                cabecalho += Environment.NewLine + "A partir Data de Emissão: " + tbxResDtEmissaoDe.Text;
            }

            // Data Saída
            SqlDateTime SaidaDe;
            SqlDateTime SaidaAte;

            SaidaDe = clsParser.SqlDateTimeParse(tbxResDtSaidaDe.Text);
            if (tbxResDtSaidaAte.Text.Length >= 10)
            {
                tbxResDtSaidaAte.Text = tbxResDtSaidaAte.Text.Substring(0, 10) + " 23:59:59";
            }
            SaidaAte = clsParser.SqlDateTimeParse(tbxResDtSaidaAte.Text);

            if (SaidaDe.IsNull == false && SaidaAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATASAIDA >= " + clsParser.SqlDateTimeFormat(SaidaDe, true) + " AND ";
                query += " NFVENDA.DATASAIDA <= " + clsParser.SqlDateTimeFormat(SaidaAte, true);

                cabecalho += Environment.NewLine + "Da Data de Saída: " + tbxResDtSaidaDe.Text + " até " + tbxResDtSaidaAte.Text;
            }
            else if (SaidaDe.IsNull == true && SaidaAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATASAIDA <= " + clsParser.SqlDateTimeFormat(SaidaAte, true);

                cabecalho += Environment.NewLine + "Até a Data de Saída: " + tbxResDtSaidaAte.Text;
            }
            else if (SaidaDe.IsNull == false && SaidaAte.IsNull == true)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATASAIDA >= " + clsParser.SqlDateTimeFormat(SaidaDe, true);

                cabecalho += Environment.NewLine + "A partir Data de Saída: " + tbxResDtSaidaDe.Text;
            }

            // Vendedor
            if (tbxResVendedorDe.Text.Length > 0 && tbxResVendedorAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " CLIENTE1.COGNOME >= '" + tbxResVendedorDe.Text + "' AND ";
                query += " CLIENTE1.COGNOME <= '" + tbxResVendedorAte.Text + "' ";

                cabecalho += Environment.NewLine + "Do Vendedor: " + tbxResVendedorDe.Text + " até " + tbxResVendedorAte.Text;
            }
            else if (tbxResVendedorDe.Text.Length > 0 && tbxResVendedorAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " CLIENTE1.COGNOME >= '" + tbxResVendedorDe.Text + "' ";

                cabecalho += Environment.NewLine + "A partir do Vendedor: " + tbxResVendedorDe.Text;
            }
            else if (tbxResVendedorDe.Text.Length == 0 && tbxResVendedorAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " CLIENTE1.COGNOME <= '" + tbxResVendedorAte.Text + "' ";

                cabecalho += Environment.NewLine + "Até o Vendedor: " + tbxResVendedorAte.Text;
            }

            //Situação da nota
            if (lbxResSituacao.SelectedIndex == 0)
            {
                cabecalho += Environment.NewLine + "Todas as Situações";
            }
            else if (lbxResSituacao.SelectedIndex == 1 && clsInfo.zempresacliente_cognome != "CASADOURADA")
            {
                if (query.Length > 0) { query += " AND "; }
                query += " LEFT(NFVENDA.SITUACAO, 1) = '1' ";
                cabecalho += " Apenas Vendas";
            }
            else if (lbxResSituacao.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + " LEFT(NFVENDA.SITUACAO, 1) = '2' ";
                cabecalho += " Apenas Consignação";
            }
            else if (lbxResSituacao.SelectedIndex == 3)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + " LEFT(NFVENDA.SITUACAO,1) = '3' ";
                cabecalho += " Apenas Devolução";
            }
            else if (lbxResSituacao.SelectedIndex == 4)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "LEFT(NFVENDA.SITUACAO,1) = '4'";
                cabecalho = cabecalho + Environment.NewLine + " Apenas Remessa";
            }

            if (query.Length > 0) { query += " AND "; }
            query = query + " NFVENDA.FILIAL =" + clsInfo.zfilial + " ";


            // Ordens - Agrupamentos
            if (lbxResOrdem.SelectedIndex == 0)
            {
                ordem = " CLIENTE, NUMERONFV ";
            }
            else if (lbxResOrdem.SelectedIndex == 1)
            {
                ordem = " NUMERONFV, CLIENTE ";

            }
            else if (lbxResOrdem.SelectedIndex == 2)
            {
                ordem = " DATANFV, NUMERONFV, CLIENTE ";
            }

            // Tipo de Relatório
            if (rbnResAnalitica.Checked == true)
            {
                cabecalho += Environment.NewLine + "Tipo Relatório Analítico";
            }
            else
            {
                cabecalho += Environment.NewLine + "Tipo Relatório Sintética";
            }

            // Tipo de Agrupamento
            if (rbnResSemSub.Checked == true)
            {
                cabecalho += Environment.NewLine + "Sem Sub-Total";
            }
            else
            {
                cabecalho += Environment.NewLine + "Com Sub-Total";
            }

            DataTable Rel = new DataTable();
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            ParameterField pfieldEmpresa = new ParameterField();
            ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            if (carregarcampos.Rows.Count > 0)
            {
                disvalEmpresa.Value = carregarcampos.Rows[0]["NOME"].ToString();
                pfieldEmpresa.Name = "EMPRESA";
                pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            }
            ParameterField pfieldCabecalho = new ParameterField();
            ParameterDiscreteValue disvalCabecalho = new ParameterDiscreteValue();
            disvalCabecalho.Value = cabecalho.ToString();
            pfieldCabecalho.Name = "CABECALHO";
            pfieldCabecalho.CurrentValues.Add(disvalCabecalho);
            pfields.Add(pfieldEmpresa);
            pfields.Add(pfieldCabecalho);

            String sql = "SELECT NFVENDA.ID AS IDNFV, NFVENDA.FILIAL AS FILIALNFV, NFVENDA.NUMERO AS NUMERONFV, NFVENDA.DATA AS DATANFV, " +
                        "NFVENDA.DATASAIDA AS DATASAIDANFV, DOCFISCAL.CODIGO AS DOCUMENTO, DOCFISCAL.COGNOME AS DOCUMENTOCOGNOME,  " +
                        "NFVENDA.SERIE, NFVENDA.TIPOENTRADA, CLIENTE.COGNOME as CLIENTE, NFVENDA.SITUACAO,  " +
                        "SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTOCODIGO, SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOME,  " +
                        "CLIENTE1.COGNOME AS VENDEDOR,  " + //--NFVENDA.TIPOCOMI, NFVENDA.COMISSAO, NFVENDA.TIPOCOMIGER, NFVENDA.COMISSAOGER,  
                        "NFVENDA.MODELO, NFVENDA.FRETE, NFVENDA.FRETEPAGA, NFVENDA.TRANSPORTE, CLIENTE2.COGNOME AS TRANSPORTADORA,  " +
                        "NFVENDA.TIPOFRETE, NFVENDA.TOTALBASEICM, NFVENDA.TOTALICM, NFVENDA.TOTALBASEICMSUBST, NFVENDA.TOTALICMSUBST,  " +
                        "NFVENDA.TOTALPESO, NFVENDA.TOTALPESOBRUTO, NFVENDA.TOTALMERCADORIA, NFVENDA.TOTALIPI, NFVENDA.TOTALFRETE,  " +
                        "NFVENDA.TOTALSEGURO, NFVENDA.TOTALOUTRAS, NFVENDA.TOTALNOTAFISCAL, NFVENDA.PISPASEP, NFVENDA.COFINS1,  " +
                        "NFVENDA.QTDEENVIADA, NFVENDA.QTDEDEVOLVIDA, NFVENDA.VALORCOMISSAO, NFVENDA.VALORCOMISSAOGER  " +
                        "FROM NFVENDA " +
                        "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = NFVENDA.IDDOCUMENTO  " +
                        "LEFT JOIN CLIENTE ON CLIENTE.ID = NFVENDA.IDCLIENTE  " +
                        "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID = NFVENDA.IDCONDPAGTO  " +
                        "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=NFVENDA.IDFORMAPAGTO  " +
                        "LEFT JOIN CLIENTE CLIENTE1 ON CLIENTE1.ID = NFVENDA.IDVENDEDOR  " +
                        "LEFT JOIN CLIENTE CLIENTE3 ON CLIENTE3.ID = NFVENDA.IDSUPERVISOR  " +
                        "LEFT JOIN CLIENTE CLIENTE4 ON CLIENTE4.ID = NFVENDA.IDCOORDENADOR  " +
                        "LEFT JOIN CLIENTE CLIENTE2 ON CLIENTE2.ID = NFVENDA.IDTRANSPORTADORA  ";

            if (query.ToString().Length > 0)
            {
                sql = sql + " where " + query + " ";
            }
            sql = sql + " order by " + ordem;

            SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
            Rel = new DataTable();
            sda.Fill(Rel);


            frmCrystalReport frmCrystalReport;

            if (rbnResTipoSimples.Checked == true || rbnResTipoFaturamento.Checked == true)
            {
                if (rbnResAnalitica.Checked == true)
                {
                    // Analítica
                    if (rbnResSemSub.Checked == true)
                    {
                        // Analítica - Sem Sub-Total
                        frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "APLISOFT_DADOS_NFVENDA_CABEC.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        // Analítica - Com Sub-Total
                        if (lbxResOrdem.SelectedIndex == 0)
                        {
                            frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_NFVENDA_CABEC_ANA_CLIENTESOFT.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else if (lbxResOrdem.SelectedIndex == 1)
                        {
                            MessageBox.Show("Nao existe relatório com a 'Ordem' escolhida.");
                        }
                        else if (lbxResOrdem.SelectedIndex == 2)
                        {
                            frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_NFVENDA_CABEC_ANA_DATASOFT.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                    }
                }
                else
                {
                    // Sintética 
                    if (lbxResOrdem.SelectedIndex == 0)
                    {
                        frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_NFVENDA_CABEC_SIN_CLIENTESOFT.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (lbxResOrdem.SelectedIndex == 1)
                    {
                        MessageBox.Show("Nao existe relatório com a 'Ordem' escolhida.");
                    }
                    else if (lbxResOrdem.SelectedIndex == 2)
                    {
                        frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_NFVENDA_CABEC_SIN_DATASOFT.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                }
            }
        }

        private void tspItemImprimir_Click(object sender, EventArgs e)
        {
            cabecalho = "";
            query = "";
            ordem = "";

            cabecalho = "Relatório Nota Fiscal Saída";

            // Cabeçalho  -- objetivo da lista
            if (rbnItemCodigo.Checked == true) { cabecalho += " [Código]"; }
            if (rbnItemCtaContabil.Checked == true) { cabecalho += " [Cta Contabil]"; }
            if (rbnItemHistorico.Checked == true) { cabecalho += " [Histórico Bco]"; }
            if (rbnItemCentroCusto.Checked == true) { cabecalho += " [Centro Custo=CT]"; }
            if (rbnItemCFOP.Checked == true) { cabecalho += " [CFOP]"; }
            if (rbnItemRemessa.Checked == true) { cabecalho += " [Remessa Enviada]"; }
            if (rbnItemContabilidade.Checked == true) { cabecalho += " [Contabilidade]"; }

            // Tipo do Cliente (Clientes/Fornecedor)
            if (rbnCliente.Checked == true)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "CLIENTE.TIPO = 'C'";
            }
            else if (rbnFornecedor.Checked == true)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "CLIENTE.TIPO = 'F'";
            }

            // Tipo de Nota / Tipo de Entrada
            if (cbxItemTipoFaturamentoS.Checked == true ||
                cbxItemTipoFaturamentoE.Checked == true ||
                cbxItemTipoFaturamentoC.Checked == true)
            {
                if (query.Length > 0) { query += " AND "; }

                if (cbxItemTipoFaturamentoS.Checked == true)
                {
                    query += " (LEFT(NFVENDA.TIPOENTRADA, 1) = 'S' ";
                }

                if (cbxItemTipoFaturamentoE.Checked == true)
                {
                    if (cbxItemTipoFaturamentoS.Checked == true) { query += " OR "; }
                    else { query += "("; }

                    query += " LEFT(NFVENDA.TIPOENTRADA, 1) = 'E'";
                }

                if (cbxItemTipoFaturamentoC.Checked == true)
                {
                    if (cbxItemTipoFaturamentoS.Checked == true || cbxItemTipoFaturamentoE.Checked == true) { query += " OR "; }
                    else { query += "("; }

                    query += " LEFT(NFVENDA.TIPOENTRADA, 1) = 'C' ";
                }

                query += ")";
            }

            // Cliente
            if (tbxItemClienteDe.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " CLIENTE.COGNOME >= '" + tbxItemClienteDe.Text + "' ";
                cabecalho += Environment.NewLine + "Do Cliente: " + tbxItemClienteDe.Text;
            }

            if (tbxItemClienteAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " CLIENTE.COGNOME <= '" + tbxItemClienteAte.Text + "' ";
                cabecalho += Environment.NewLine + "Até o Cliente: " + tbxItemClienteAte.Text;
            }

            // Data Emissão
            SqlDateTime EmissaoDe;
            SqlDateTime EmissaoAte;

            EmissaoDe = clsParser.SqlDateTimeParse(tbxItemDtEmissaoDe.Text);
            if (tbxItemDtEmissaoAte.Text.Length >= 10)
            {
                tbxItemDtEmissaoAte.Text = tbxItemDtEmissaoAte.Text.Substring(0, 10) + " 23:59:59";
            }
            EmissaoAte = clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text);



            if (EmissaoDe.IsNull == false && EmissaoAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATA >= " + clsParser.SqlDateTimeFormat(EmissaoDe, true) + " AND ";
                query += " NFVENDA.DATA <= " + clsParser.SqlDateTimeFormat(EmissaoAte, true);

                cabecalho += Environment.NewLine + " Da Data de Emissão: " + EmissaoDe.Value.ToString("dd/MM/yyyy") + " até " + EmissaoAte.Value.ToString("dd/MM/yyyy");
            }
            else if (EmissaoDe.IsNull == true && EmissaoAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATA <= " + clsParser.SqlDateTimeFormat(EmissaoAte, true);

                cabecalho += Environment.NewLine + " Até a Data de Emissão: " + tbxItemDtEmissaoAte.Text;
            }
            else if (EmissaoDe.IsNull == false && EmissaoAte.IsNull == true)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATA >= " + clsParser.SqlDateTimeFormat(EmissaoDe, true);

                cabecalho += Environment.NewLine + " Da Data de Emissão: " + tbxItemDtEmissaoDe.Text;
            }

            // Data Saída
            SqlDateTime SaidaDe;
            SqlDateTime SaidaAte;

            SaidaDe = clsParser.SqlDateTimeParse(tbxItemDtSaidaDe.Text);
            if (tbxItemDtSaidaAte.Text.Length >= 10)
            {
                tbxItemDtSaidaAte.Text = tbxItemDtSaidaAte.Text.Substring(0, 10) + " 23:59:59";
            }
            SaidaAte = clsParser.SqlDateTimeParse(tbxItemDtSaidaAte.Text);

            if (SaidaDe.IsNull == false && SaidaAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATASAIDA >= " + clsParser.SqlDateTimeFormat(SaidaDe, true) + " AND ";
                query += " NFVENDA.DATASAIDA <= " + clsParser.SqlDateTimeFormat(SaidaAte, true);

                cabecalho += Environment.NewLine + " Da Data de Saída: " + tbxItemDtSaidaDe.Text + " até " + tbxItemDtSaidaAte.Text;
            }
            else if (SaidaDe.IsNull == true && SaidaAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATASAIDA <= " + clsParser.SqlDateTimeFormat(SaidaAte, true);

                cabecalho += Environment.NewLine + " Até a Data de Saída: " + tbxItemDtSaidaAte.Text;
            }
            else if (SaidaDe.IsNull == false && SaidaAte.IsNull == true)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " NFVENDA.DATASAIDA >= " + clsParser.SqlDateTimeFormat(SaidaDe, true);

                cabecalho += Environment.NewLine + " Da Data de Saída: " + tbxItemDtSaidaDe.Text;
            }

            // Vendedor
            if (tbxItemVendedorDe.Text.Length > 0 && tbxItemVendedorAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " CLIENTE1.COGNOME >= '" + tbxItemVendedorDe.Text + "' AND ";
                query += " CLIENTE1.COGNOME <= '" + tbxItemVendedorAte.Text + "'";

                cabecalho += Environment.NewLine + " Do Vendedor: " + tbxItemVendedorDe.Text + " até " + tbxItemVendedorAte.Text;
            }
            else if (tbxItemVendedorDe.Text.Length > 0 && tbxItemVendedorAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " CLIENTE1.COGNOME >= '" + tbxItemVendedorDe.Text + "' ";

                cabecalho += Environment.NewLine + " Do Vendedor: " + tbxItemVendedorDe.Text;
            }
            else if (tbxItemVendedorDe.Text.Length == 0 && tbxItemVendedorAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += " CLIENTE1.COGNOME <= '" + tbxItemVendedorAte.Text + "'";

                cabecalho += Environment.NewLine + " Até o Vendedor: " + tbxItemVendedorAte.Text;
            }

            // Código
            if (tbxItemCodDe.Text.Length > 0 && tbxItemCodAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " PECAS.CODIGO >= '" + tbxItemCodDe.Text + "' and ";
                query += " PECAS.CODIGO <= '" + tbxItemCodAte.Text + "'";
                cabecalho += Environment.NewLine + " Do Código: " + tbxItemCodDe.Text + " até " + tbxItemCodAte.Text;
            }
            else if (tbxItemCodDe.Text.Length > 0 && tbxItemCodAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " PECAS.CODIGO >= '" + tbxItemCodDe.Text + "'";
                cabecalho += Environment.NewLine + " Do Código: " + tbxItemCodDe.Text;
            }
            else if (tbxItemCodDe.Text.Length == 0 && tbxItemCodAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECAS.CODIGO <= '" + tbxItemCodAte.Text + "'";
                cabecalho += Environment.NewLine + " Até o Código: " + tbxItemCodAte.Text;
            }

            // Histórico do Banco
            if (tbxItemHistDe.Text.Length > 0 && tbxItemHistAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " HISTORICOS.CODIGO >= '" + tbxItemHistDe.Text + "' and ";
                query += " HISTORICOS.CODIGO <= '" + tbxItemHistAte.Text + "'";
                cabecalho += " Do Histórico: " + tbxItemHistDe.Text + " até " + tbxItemHistAte.Text;
            }
            else if (tbxItemHistDe.Text.Length > 0 && tbxItemHistAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " HISTORICOS.CODIGO >= '" + tbxItemHistDe.Text + "'";
                cabecalho += " Do Histórico: " + tbxItemHistDe.Text;
            }
            else if (tbxItemHistDe.Text.Length == 0 && tbxItemHistAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " HISTORICOS.CODIGO <= '" + tbxItemHistAte.Text + "'";
                cabecalho = cabecalho + " Até o Histórico: " + tbxItemHistAte.Text;
            }

            // Conta Contábil
            if (tbxItemContaContabilDe.Text.Length > 0 && tbxItemContaContabilAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " CONTACONTABIL.CODIGO >= '" + tbxItemContaContabilDe.Text + "'";
                cabecalho += Environment.NewLine + " Da Cta Contábil: " + tbxItemContaContabilDe.Text;
            }
            else if (tbxItemContaContabilDe.Text.Length == 0 && tbxItemContaContabilAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " CONTACONTABIL.CODIGO <= '" + tbxItemContaContabilAte.Text + "' ";
                cabecalho += " Até Cta Contábil: " + tbxItemContaContabilAte.Text;
            }
            else if (tbxItemContaContabilDe.Text.Length > 0 && tbxItemContaContabilAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " CONTACONTABIL.CODIGO >= '" + tbxItemContaContabilDe.Text + "' AND CONTACONTABIL.CODIGO <= '" + tbxItemContaContabilAte.Text + "' ";
                cabecalho += " Do Cta Contábil: " + tbxItemContaContabilDe.Text + "  até " + tbxItemContaContabilAte.Text;
            }

            // Centro de Custo
            if (tbxItemCustoDe.Text.Length > 0 && tbxItemCustoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " CENTROCUSTOS.CODIGO >= '" + tbxItemCustoDe.Text + "' and ";
                query += " CENTROCUSTOS.CODIGO <= '" + tbxItemCustoDe.Text + "'";
                cabecalho += Environment.NewLine + "Do Centro Custo: " + tbxItemCustoDe.Text + " até " + tbxItemCustoAte.Text;
            }
            else if (tbxItemCustoDe.Text.Length > 0 && tbxItemCustoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " CENTROCUSTOS.CODIGO >= '" + tbxItemCustoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Do Centro Custo: " + tbxItemCustoDe.Text;
            }
            else if (tbxItemCustoDe.Text.Length == 0 && tbxItemCustoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " CENTROCUSTOS.CODIGO <= '" + tbxItemCustoAte.Text + "'";
                cabecalho += Environment.NewLine + "Até o Centro Custo: " + tbxItemCustoAte.Text;
            }

            // Grupo de Material
            if (tbxItemGrupoDe.Text.Length > 0 && tbxItemGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " PECASCLASSIFICA.CODIGO >= '" + tbxItemGrupoDe.Text + "' and ";
                query += " PECASCLASSIFICA.CODIGO <= '" + tbxItemGrupoAte.Text + "'";
                cabecalho += Environment.NewLine + "Do Grupo: " + tbxItemGrupoDe.Text + " até " + tbxItemGrupoAte.Text;
            }
            else if (tbxItemGrupoDe.Text.Length > 0 && tbxItemGrupoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " PECASCLASSIFICA.CODIGO >= '" + tbxItemGrupoDe.Text + "'";
                cabecalho += " Do Grupo: " + tbxItemGrupoDe.Text;
            }
            else if (tbxItemGrupoDe.Text.Length == 0 && tbxItemGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " PECASCLASSIFICA.CODIGO <= '" + tbxItemGrupoAte.Text + "'";
                cabecalho += Environment.NewLine + "Até o Grupo: " + tbxItemGrupoAte.Text;
            }

            // Sub-Grupo de Material
            if (tbxItemSubGrupoDe.Text.Length > 0 && tbxItemSubGrupoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + " PECASCLASSIFICA1.CODIGO >= '" + tbxItemSubGrupoDe.Text + "'";
                cabecalho += " Do Sub-Grupo: " + tbxItemSubGrupoDe.Text;
            }
            else if (tbxItemSubGrupoDe.Text.Length == 0 && tbxItemSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + " PECASCLASSIFICA1.CODIGO <= '" + tbxItemSubGrupoAte.Text + "'";
                cabecalho += " Até o Sub-Grupo: " + tbxItemSubGrupoAte.Text;
            }
            else if (tbxItemSubGrupoDe.Text.Length > 0 && tbxItemSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + " PECASCLASSIFICA1.CODIGO >= '" + tbxItemSubGrupoDe.Text + "' AND PECASCLASSIFICA1.CODIGO <= '" + tbxItemSubGrupoAte.Text + "' ";
                cabecalho += " Do Sub-Grupo: " + tbxItemSubGrupoDe.Text + "  Até o Sub-Grupo: " + tbxItemSubGrupoAte.Text;
            }

            // Item do Sub-Grupo de Material
            if (tbxItemItemSubGrupoDe.Text.Length > 0 && tbxItemItemSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += " PECASCLASSIFICA2.CODIGO >= '" + tbxItemItemSubGrupoDe.Text + "' AND ";
                query += " PECASCLASSIFICA2.CODIGO <= '" + tbxItemItemSubGrupoAte.Text + "' ";
                cabecalho += " Do Item Sub-Grupo: " + tbxItemItemSubGrupoDe.Text + "  Até o Item Sub-Grupo: " + tbxItemItemSubGrupoAte.Text;
            }
            else if (tbxItemItemSubGrupoDe.Text.Length > 0 && tbxItemItemSubGrupoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + " PECASCLASSIFICA2.CODIGO >= '" + tbxItemItemSubGrupoDe.Text + "'";
                cabecalho += " Do Item Sub-Grupo: " + tbxItemItemSubGrupoDe.Text;
            }
            else if (tbxItemItemSubGrupoDe.Text.Length == 0 && tbxItemItemSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + " PECASCLASSIFICA2.CODIGO <= '" + tbxItemItemSubGrupoAte.Text + "'";
                cabecalho += " Até o Item Sub-Grupo: " + tbxItemItemSubGrupoAte.Text;
            }

            //Situação da nota
            /*
             * Como a Casa Dourada não possui situação d nota de uma forma util, apenas dados em branco no banco de dados
             * e o sistema usa o formulário de relatórios padrão da aplisoft, forcei para que isso seja ignorado
             */

            if (clsInfo.zempresacliente_cognome.Contains("CASADOURADA"))
            {
                lbxItemSituacaoNota.SelectedIndex = 0;
            }
            if (lbxItemSituacaoNota.SelectedIndex == 0)
            {
                cabecalho = cabecalho + Environment.NewLine + "Todas as Situações";
            }
            else if (lbxItemSituacaoNota.SelectedIndex == 1)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA.SITUACAO,1) = '1' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Venda";
            }
            else if (lbxItemSituacaoNota.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA.SITUACAO,1) = '2' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Consignação";
            }
            else if (lbxItemSituacaoNota.SelectedIndex == 3)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA.SITUACAO,1) = '3' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Devolução";
            }
            else if (lbxItemSituacaoNota.SelectedIndex == 4)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA.SITUACAO,1) = '4' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Remessa";
            }

            // Tipo do Produto
            if (lbxItemTipoProduto.SelectedIndex == 0)
            {
                cabecalho = cabecalho + " Todos os Tipos";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 1)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'A' ";
                cabecalho = cabecalho + " Apenas tipo Venda";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'B' ";
                cabecalho = cabecalho + " Apenas Tipo Industrialização";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 3)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'C' ";
                cabecalho = cabecalho + " Apenas Tipo Conjunto";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 4)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'D' ";
                cabecalho = cabecalho + " Apenas Tipo Despesas/Impostos";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 5)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'F' ";
                cabecalho = cabecalho + " Apenas Tipo Dispositivos";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 6)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'I' ";
                cabecalho = cabecalho + " Apenas Tipo Instrumentos";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 7)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'K' ";
                cabecalho = cabecalho + " Apenas Tipo Kan-Ban";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 8)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'M' ";
                cabecalho = cabecalho + " Apenas Tipo Mat.Prima";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 9)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'N' ";
                cabecalho = cabecalho + " Apenas Tipo MP/Emb Clientes";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 10)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'O' ";
                cabecalho = cabecalho + " Apenas Tipo Uso e Consumo";
            }
            else if (lbxItemTipoProduto.SelectedIndex == 11)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(PECAS.TIPOPRODUTO,1) = 'P' ";
                cabecalho = cabecalho + " Apenas Tipo Componente";
            }

            // Tipo da Entrada
            if (lbxItemTipoEntrada.SelectedIndex == 0)
            {
                cabecalho = cabecalho + " Todos os Tipos de Saída";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 1)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "cast(NFVENDA1.TIPOITEM as int) between 0 and 9";
                cabecalho = cabecalho + " Apenas [T0= 00 Até 09 Vendas]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 0";
                cabecalho = cabecalho + " Apenas [00=Venda/Compra de Produto ok]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 3)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 1";
                cabecalho = cabecalho + " Apenas [01 - Venda de Produto C/Defeito]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 4)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 2";
                cabecalho = cabecalho + " Apenas [02 - Venda/Devolução Pçs Danificadas]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 5)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 3";
                cabecalho = cabecalho + " Apenas [03 - Venda/Devolução Pçs Baixadas]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 6)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 4";
                cabecalho = cabecalho + " Apenas [04 - Produto Retrabalhado]";
            }

            else if (lbxItemTipoEntrada.SelectedIndex == 7)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 9";
                cabecalho = cabecalho + " Apenas [09 - Complemento]";
            }

            else if (lbxItemTipoEntrada.SelectedIndex == 8)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 10";
                cabecalho = cabecalho + " Apenas [10 - Devolução de Embalagem]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 9)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) between 20 and 29";
                cabecalho = cabecalho + " Apenas [T2 =  Do tipo 20 ao 29 - Devolução de Mercadoria]";
            }

            else if (lbxItemTipoEntrada.SelectedIndex == 10)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 20";
                cabecalho = cabecalho + " Apenas [20 - Devolução de Mercadoria Beneficiada]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 11)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 21";
                cabecalho = cabecalho + " Apenas [21 - Devolução de Amostra]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 12)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 22";
                cabecalho = cabecalho + " Apenas [22 - Devolução de Mercadoria Não Beneficiada]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 13)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 30";
                cabecalho = cabecalho + " Apenas [30=Prestação Serviço]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 14)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) between 40 and 49";
                cabecalho = cabecalho + " Apenas [T4 =  Do tipo 40 ao 49]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 15)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 40";
                cabecalho = cabecalho + " Apenas [40 - Remessa]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 16)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 41";
                cabecalho = cabecalho + " Apenas [41 - Remessa MP Terceiro]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 17)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 42";
                cabecalho = cabecalho + " Apenas [42 - Remessa MP Terceiro com contra ordem]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 18)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 44";
                cabecalho = cabecalho + " Apenas [44 - Remessa p/Retrabalho [Deduz Ctas Pagar]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 19)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 45";
                cabecalho = cabecalho + " Apenas [45 - Remessa p/Retrabalho [Sem Debito]]";
            }
            else if (lbxItemTipoEntrada.SelectedIndex == 20)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 90";
                cabecalho = cabecalho + " Apenas [90=Imobilizado]";
            }

            // Consumo // Industrialização
            if (lbxItemConsumo.SelectedIndex == 0)
            {
                //cabecalho = cabecalho + Environment.NewLine + "Todas as Situações";
            }
            else if (lbxItemConsumo.SelectedIndex == 1)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA1.CONSUMO,1) = 'N' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Revenda/Industrializacao";
            }
            else if (lbxItemSituacaoNota.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA1.CONSUMO,1) = 'C' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Consumo";
            }

            /////////////////////////////////////////////////////////////////////////////////////////
            // ESPECIAIS DE CADA RELATORIO / MODELO PARA A QUERY
            if (rbnItemRemessa.Checked == true)
            { // QTDE DE REMESSA ENVIADA PENDENTE
                if (query.Length > 0) { query += " AND "; }

                if (rbnModelo1.Checked == true)
                {
                    cabecalho = cabecalho + Environment.NewLine + "Remessa Enviada Pendentes";
                    query = query + "left(NFVENDA.situacao,1)= '4' AND (NFVENDA1.qtde -(NFVENDA1.qtdeentra + NFVENDA1.qtdetransfe)) > 0 ";
                }
                else
                {
                    cabecalho = cabecalho + Environment.NewLine + "Remessa Enviada Movimentacao";
                    query = query + " left(NFVENDA.situacao,1)= '4' AND nfvenda1.tipoitem >= '10' ";
                }
            }
            /////////////////////////////////////////////////////////////////////////////////////////
            //Ordem
            if (lbxItemOrdem.SelectedIndex == 0)
            {
                if (rbnItemCodigo.Checked == true)
                {
                    ordem = "CLIENTE, DATANFV, NUMERONFV, CODIGO";
                }
                if (rbnItemCtaContabil.Checked == true)
                {
                    ordem = "CLIENTE, DATANFV, NUMERONFV, CTACONTABIL";
                }
                if (rbnItemHistorico.Checked == true)
                {
                    ordem = "CLIENTE, DATANFV, NUMERONFV, HISTORICO";
                }
                if (rbnItemCentroCusto.Checked == true)
                {
                    ordem = "CLIENTE, DATANFV, NUMERONFV, CENTROCUSTO";
                }
                if (rbnItemCFOP.Checked == true)
                {
                    ordem = "CLIENTE, DATANFV, NUMERONFV, CFOPFIS";
                }
                if (rbnItemRemessa.Checked == true)
                {
                    ordem = "NUMERONFV, DATANFV, CLIENTE";
                }
                if (rbnItemContabilidade.Checked == true)
                {
                    ordem = "NUMERONFV, DATANFV, CLIENTE";
                }

            }
            if (lbxItemOrdem.SelectedIndex == 1)
            {
                if (rbnItemCodigo.Checked == true)
                {
                    ordem = "PECAS.CODIGO, DATANFV, NUMERONFV, CLIENTE.COGNOME";
                }
                if (rbnItemCtaContabil.Checked == true)
                {
                    ordem = "CTACONTABIL, DATANFV, NUMERONFV, CLIENTE.COGNOME";
                }
                if (rbnItemHistorico.Checked == true)
                {
                    ordem = "HISTORICO, DATANFV, NUMERONFV, CLIENTE.COGNOME";
                }
                if (rbnItemCentroCusto.Checked == true)
                {
                    ordem = "CENTROCUSTO, DATANFV, NUMERONFV, CLIENTE.COGNOME";
                }
                if (rbnItemCFOP.Checked == true)
                {
                    ordem = "CFOPFIS, DATANFV, NUMERONFV, CLIENTE.COGNOME";
                }
                if (rbnItemRemessa.Checked == true)
                {
                    ordem = "CODIGO, DATANFV, NUMERONFV, CLIENTE.COGNOME";
                }

            }
            if (lbxItemOrdem.SelectedIndex == 2)
            {
                if (rbnItemCodigo.Checked == true)
                {
                    ordem = "DATANFV, CLIENTE.COGNOME,  PECAS.CODIGO";
                }
                if (rbnItemCtaContabil.Checked == true)
                {
                    ordem = "DATANFV, CLIENTE.COGNOME, CTACONTABIL";
                }
                if (rbnItemHistorico.Checked == true)
                {
                    ordem = "DATANFV, CLIENTE.COGNOME, HISTORICO";
                }
                if (rbnItemCentroCusto.Checked == true)
                {
                    ordem = "DATANFV, CLIENTE.COGNOME, CENTROCUSTO";
                }
                if (rbnItemCFOP.Checked == true)
                {
                    ordem = "DATANFV, CLIENTE.COGNOME, CFOPFIS";
                }

            }
            if (lbxItemOrdem.SelectedIndex == 3)
            {
                ordem = "NUMERONFV";
            }
            // Cabecalho (lista Analitica ou Sintetica)
            if (rbnItemAnalitica.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Tipo relatorio Analitica";
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + "Tipo relatorio Sintetica";
            }
            //  
            if (rbnItemSemTotal.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Sem Sub-Total";
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + "Com Sub-Total";
            }
            // Ordem da Lista williams
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxItemOrdem.Text;
            // QUERY

            DataTable Rel = new DataTable();
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            ParameterField pfieldEmpresa = new ParameterField();
            ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            if (carregarcampos.Rows.Count > 0)
            {
                disvalEmpresa.Value = carregarcampos.Rows[0]["NOME"].ToString();
                pfieldEmpresa.Name = "EMPRESA";
                pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            }
            ParameterField pfieldCabecalho = new ParameterField();
            ParameterDiscreteValue disvalCabecalho = new ParameterDiscreteValue();
            disvalCabecalho.Value = cabecalho.ToString();
            pfieldCabecalho.Name = "CABECALHO";
            pfieldCabecalho.CurrentValues.Add(disvalCabecalho);


            pfields.Add(pfieldEmpresa);
            pfields.Add(pfieldCabecalho);
            if (rbnItemRemessa.Checked == false)
            {
                Rel = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "NFVENDA " +
                "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = NFVENDA.IDDOCUMENTO  " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = NFVENDA.IDCLIENTE " +
                "LEFT JOIN ESTADOS ON ESTADOS.ID = CLIENTE.IDESTADO " +
                "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID = NFVENDA.IDCONDPAGTO " +
                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=NFVENDA.IDFORMAPAGTO " +
                "LEFT JOIN CLIENTE CLIENTE1 ON CLIENTE1.ID = NFVENDA.IDVENDEDOR  " +
                "LEFT JOIN CLIENTE CLIENTE3 ON CLIENTE3.ID = NFVENDA.IDSUPERVISOR " +
                "LEFT JOIN CLIENTE CLIENTE4 ON CLIENTE4.ID = NFVENDA.IDCOORDENADOR " +
                "LEFT JOIN CLIENTE CLIENTE2 ON CLIENTE2.ID = NFVENDA.IDTRANSPORTADORA " +
                "LEFT JOIN NFVENDA1 ON NFVENDA1.NUMERO = NFVENDA.ID " +
                "LEFT JOIN PEDIDO ON PEDIDO.ID = NFVENDA1.IDPEDIDO " +
                "LEFT JOIN PEDIDO1 ON PEDIDO1.ID = NFVENDA1.IDPEDIDOITEM " +
                "LEFT JOIN PEDIDO2 ON PEDIDO2.ID = NFVENDA1.IDPEDIDO2 " +
                "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = NFVENDA1.IDORDEMSERVICO " +
                "LEFT JOIN CERTIFICADO ON CERTIFICADO.ID=NFVENDA1.IDCERTIFICADO " +
                "LEFT JOIN PECAS ON PECAS.ID=NFVENDA1.IDCODIGO " +
                "LEFT JOIN SITTRIBUTARIAA ON SITTRIBUTARIAA.ID = NFVENDA1.IDSITTRIBA " +
                "LEFT JOIN SITTRIBUTARIAB ON SITTRIBUTARIAB.ID = NFVENDA1.IDSITTRIBB " +
                "LEFT JOIN CFOP CFOP0 ON CFOP0.ID = NFVENDA1.IDCFOP " +
                "LEFT JOIN CFOP CFOP1 ON CFOP1.ID = NFVENDA1.IDCFOPFIS " +
                "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS ON HISTORICOS.ID = NFVENDA1.IDHISTORICO " +
                "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS ON CENTROCUSTOS.ID = NFVENDA1.IDCENTROCUSTO " +
                "LEFT JOIN CONTACONTABIL ON CONTACONTABIL.ID = NFVENDA1.IDCODIGOCTABIL " +
                "LEFT JOIN UNIDADE ON UNIDADE.ID = NFVENDA1.IDUNID " +
                "LEFT JOIN IPI ON IPI.ID = NFVENDA1.IDIPI " +
                "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA " +
                "LEFT JOIN PECASCLASSIFICA1 ON PECASCLASSIFICA1.ID = PECAS.IDCLASSIFICA1 " +
                "LEFT JOIN PECASCLASSIFICA2 ON PECASCLASSIFICA2.ID = PECAS.IDCLASSIFICA2  ",

                "NFVENDA.ID AS IDNFV,NFVENDA.FILIAL AS FILIALNFV,NFVENDA.NUMERO AS NUMERONFV,NFVENDA.DATA AS DATANFV, " +
                "NFVENDA.DATASAIDA AS DATASAIDANFV, DOCFISCAL.CODIGO AS DOCUMENTO, DOCFISCAL.COGNOME AS DOCUMENTOCOGNOME, " +
                "ESTADOS.ESTADO, " +
                "ESTADOS.NOMEEXT, " +
                "NFVENDA.SERIE,NFVENDA.TIPOENTRADA,CLIENTE.COGNOME as CLIENTE,NFVENDA.SITUACAO,SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTOCODIGO, " +
                "SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOME,CLIENTE1.COGNOME AS VENDEDOR,NFVENDA.TIPOCOMI,NFVENDA.COMISSAO, " +
                "NFVENDA.TIPOCOMIGER,NFVENDA.COMISSAOGER,NFVENDA.MODELO,NFVENDA.FRETE,NFVENDA.FRETEPAGA,NFVENDA.TRANSPORTE, " +
                "CLIENTE2.COGNOME AS TRANSPORTADORA, NFVENDA.TIPOFRETE, NFVENDA.TOTALBASEICM, NFVENDA.TOTALICM, NFVENDA.TOTALBASEICMSUBST, " +
                "NFVENDA.TOTALICMSUBST,NFVENDA.TOTALPESO,NFVENDA.TOTALPESOBRUTO,NFVENDA.TOTALMERCADORIA,NFVENDA.TOTALIPI, " +
                "NFVENDA.TOTALFRETE, NFVENDA.TOTALSEGURO, NFVENDA.TOTALOUTRAS, NFVENDA.TOTALNOTAFISCAL, NFVENDA.PISPASEP, " +
                "NFVENDA.COFINS1, NFVENDA.QTDEENVIADA, NFVENDA.QTDEDEVOLVIDA, NFVENDA.VALORCOMISSAO, NFVENDA.VALORCOMISSAOGER, " +
                "NFVENDA1.TIPOITEM, PEDIDO.NUMERO AS [NROPEDIDOVENDA], ORDEMSERVICO.NUMERO AS [NROORDEMSERVICO], CERTIFICADO.NUMERO AS [NROCERTIFICADO], " +
                "PECAS.TIPOPRODUTO AS [TIPOPRODUTO],PECAS.CODIGO AS [CODIGO],PECAS.NOME AS [CODIGONOME],NFVENDA1.COMPLEMENTO, " +
                "NFVENDA1.COMPLEMENTO1,NFVENDA1.DESCRICAOESP,NFVENDA1.MSG,NFVENDA1.CONSUMO,SITTRIBUTARIAA.CODIGO AS [SITTRIBA], " +
                "SITTRIBUTARIAB.CODIGO AS [SITTRIBB], CFOP0.CFOP AS [CFOP], CFOP1.CFOP AS [CFOPFIS], HISTORICOS.CODIGO AS [HISTORICO], " +
                "CENTROCUSTOS.CODIGO AS [CENTROCUSTO], CONTACONTABIL.CODIGO AS [CTACONTABIL], NFVENDA1.CODIGOEMP01, NFVENDA1.CODIGOEMP02, " +
                "NFVENDA1.CODIGOEMP03,NFVENDA1.CODIGOEMP04,NFVENDA1.QTDE,UNIDADE.CODIGO AS [UNID],NFVENDA1.PRECO,NFVENDA1.TOTALMERCADO, " +
                "IPI.CODIGO AS [CODIPI],NFVENDA1.IPI,NFVENDA1.CUSTOIPI,NFVENDA1.VALORFRETE,NFVENDA1.VALORFRETEICMS,NFVENDA1.VALORSEGURO, " +
                "NFVENDA1.VALORSEGUROICMS, NFVENDA1.VALOROUTRAS, NFVENDA1.VALOROUTRASICMS, NFVENDA1.TOTALNOTA, NFVENDA1.BASEMP, " +
                "NFVENDA1.REDUCAO, NFVENDA1.BASEICM, NFVENDA1.ICM, NFVENDA1.CUSTOICM, NFVENDA1.BASEICMSUBST, NFVENDA1.ICMSUBST, " +
                "NFVENDA1.PISPASEP as [PISPASEPITEM],NFVENDA1.COFINS,NFVENDA1.IRRFPORC,NFVENDA1.IRRF,NFVENDA1.INSSPORC,NFVENDA1.INSS, " +
                "NFVENDA1.PISCOFINSCSLLPORC, NFVENDA1.PISCOFINSCSLL, NFVENDA1.PISPORC, NFVENDA1.PIS, NFVENDA1.COFINSPORC, " +
                "NFVENDA1.COFINS1 AS COFINS1ITEM, NFVENDA1.CSLLPORC, NFVENDA1.CSLL, NFVENDA1.ISSPORC, NFVENDA1.ISS, NFVENDA1.PESO, " +
                "NFVENDA1.PESOTOTAL, NFVENDA1.CANCELADA, NFVENDA1.NOTADEVOLUCAO, " +
                "NFVENDA1.DATADEVOLUCAO, NFVENDA1.QTDEDEVOLUCAO,NFVENDA1.UNIDDEVOLUCAO,NFVENDA1.VALORDEVOLUCAO,NFVENDA1.CODDEVOLUCAO, " +
                "NFVENDA1.QTDEDEVOLVIDA AS [QTDEDEVOLVIDA1],NFVENDA1.LEGENDACLASSFISCAL,NFVENDA1.QTDEENTRA,NFVENDA1.QTDETRANSFE,NFVENDA1.QTDESALDO, " +
                "NFVENDA1.DATADEVPREV, NFVENDA1.QTDEREJ,NFVENDA1.QTDEANA,NFVENDA1.ALIQCOMISSAO AS ALIQCOMISSAOITEM,NFVENDA1.TIPOCOMI AS TIPOCOMIITEM, " +
                "NFVENDA1.VALORCOMISSAO AS VALORCOMISSAOITEM, NFVENDA1.ALIQCOMISSAOGER AS ALIQCOMISSAOGERITEM, NFVENDA1.TIPOCOMIGER AS TIPOCOMIGERITEM, " +
                "NFVENDA1.VALORCOMISSAOGER AS VALORCOMISSAOGERITEM, PECASCLASSIFICA.CODIGO AS [GRUPO], PECASCLASSIFICA1.CODIGO AS [SUBGRUPO], " +
                "PECASCLASSIFICA2.CODIGO AS [ITEMSUBGRUPO], CLIENTE.CGC AS [CLIENTECNPJ], " +
                "NFVENDA1.PISPASEPRETIDO  AS [NFVENDA1_PISPASEPRETIDO], NFVENDA1.COFINS1RETIDO AS [NFVENDA1_COFINS1RETIDO] ",
                query, ordem);
            }
            else
            {// neste inclui as notas fiscais de vendas
                Rel = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "NFVENDA " +
                "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = NFVENDA.IDDOCUMENTO  " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = NFVENDA.IDCLIENTE " +
                "LEFT JOIN ESTADOS ON ESTADOS.ID = CLIENTE.IDESTADO " +
                "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID = NFVENDA.IDCONDPAGTO " +
                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=NFVENDA.IDFORMAPAGTO " +
                "LEFT JOIN CLIENTE CLIENTE1 ON CLIENTE1.ID = NFVENDA.IDVENDEDOR  " +
                "LEFT JOIN CLIENTE CLIENTE3 ON CLIENTE3.ID = NFVENDA.IDSUPERVISOR " +
                "LEFT JOIN CLIENTE CLIENTE4 ON CLIENTE4.ID = NFVENDA.IDCOORDENADOR " +
                "LEFT JOIN CLIENTE CLIENTE2 ON CLIENTE2.ID = NFVENDA.IDTRANSPORTADORA " +
                "LEFT JOIN NFVENDA1 ON NFVENDA1.NUMERO = NFVENDA.ID " +
                "LEFT JOIN PEDIDO ON PEDIDO.ID = NFVENDA1.IDPEDIDO " +
                "LEFT JOIN PEDIDO1 ON PEDIDO1.ID = NFVENDA1.IDPEDIDOITEM " +
                "LEFT JOIN PEDIDO2 ON PEDIDO2.ID = NFVENDA1.IDPEDIDO2 " +
                "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = NFVENDA1.IDORDEMSERVICO " +
                "LEFT JOIN CERTIFICADO ON CERTIFICADO.ID=NFVENDA1.IDCERTIFICADO " +
                "LEFT JOIN PECAS ON PECAS.ID=NFVENDA1.IDCODIGO " +
                "LEFT JOIN SITTRIBUTARIAA ON SITTRIBUTARIAA.ID = NFVENDA1.IDSITTRIBA " +
                "LEFT JOIN SITTRIBUTARIAB ON SITTRIBUTARIAB.ID = NFVENDA1.IDSITTRIBB " +
                "LEFT JOIN CFOP CFOP0 ON CFOP0.ID = NFVENDA1.IDCFOP " +
                "LEFT JOIN CFOP CFOP1 ON CFOP1.ID = NFVENDA1.IDCFOPFIS " +
                "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS ON HISTORICOS.ID = NFVENDA1.IDHISTORICO " +
                "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS ON CENTROCUSTOS.ID = NFVENDA1.IDCENTROCUSTO " +
                "LEFT JOIN CONTACONTABIL ON CONTACONTABIL.ID = NFVENDA1.IDCODIGOCTABIL " +
                "LEFT JOIN UNIDADE ON UNIDADE.ID = NFVENDA1.IDUNID " +
                "LEFT JOIN IPI ON IPI.ID = NFVENDA1.IDIPI " +
                "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA " +
                "LEFT JOIN PECASCLASSIFICA1 ON PECASCLASSIFICA1.ID = PECAS.IDCLASSIFICA1 " +
                "LEFT JOIN PECASCLASSIFICA2 ON PECASCLASSIFICA2.ID = PECAS.IDCLASSIFICA2  ",

                "NFVENDA.ID AS IDNFV,NFVENDA.FILIAL AS FILIALNFV,NFVENDA.NUMERO AS NUMERONFV,NFVENDA.DATA AS DATANFV, " +
                "NFVENDA.DATASAIDA AS DATASAIDANFV, DOCFISCAL.CODIGO AS DOCUMENTO, DOCFISCAL.COGNOME AS DOCUMENTOCOGNOME, " +
                "ESTADOS.ESTADO, " +
                "ESTADOS.NOMEEXT, " +
                "NFVENDA.SERIE,NFVENDA.TIPOENTRADA,CLIENTE.COGNOME as CLIENTE,NFVENDA.SITUACAO,SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTOCODIGO, " +
                "SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOME,CLIENTE1.COGNOME AS VENDEDOR,NFVENDA.TIPOCOMI,NFVENDA.COMISSAO, " +
                "NFVENDA.TIPOCOMIGER,NFVENDA.COMISSAOGER,NFVENDA.MODELO,NFVENDA.FRETE,NFVENDA.FRETEPAGA,NFVENDA.TRANSPORTE, " +
                "CLIENTE2.COGNOME AS TRANSPORTADORA, NFVENDA.TIPOFRETE, NFVENDA.TOTALBASEICM, NFVENDA.TOTALICM, NFVENDA.TOTALBASEICMSUBST, " +
                "NFVENDA.TOTALICMSUBST,NFVENDA.TOTALPESO,NFVENDA.TOTALPESOBRUTO,NFVENDA.TOTALMERCADORIA,NFVENDA.TOTALIPI, " +
                "NFVENDA.TOTALFRETE, NFVENDA.TOTALSEGURO, NFVENDA.TOTALOUTRAS, NFVENDA.TOTALNOTAFISCAL, NFVENDA.PISPASEP, " +
                "NFVENDA.COFINS1, NFVENDA.QTDEENVIADA, NFVENDA.QTDEDEVOLVIDA, NFVENDA.VALORCOMISSAO, NFVENDA.VALORCOMISSAOGER, " +
                "NFVENDA1.TIPOITEM, PEDIDO.NUMERO AS [NROPEDIDOVENDA], ORDEMSERVICO.NUMERO AS [NROORDEMSERVICO], CERTIFICADO.NUMERO AS [NROCERTIFICADO], " +
                "PECAS.TIPOPRODUTO AS [TIPOPRODUTO],PECAS.CODIGO AS [CODIGO],PECAS.NOME AS [CODIGONOME],NFVENDA1.COMPLEMENTO, " +
                "NFVENDA1.COMPLEMENTO1,NFVENDA1.DESCRICAOESP,NFVENDA1.MSG,NFVENDA1.CONSUMO,SITTRIBUTARIAA.CODIGO AS [SITTRIBA], " +
                "SITTRIBUTARIAB.CODIGO AS [SITTRIBB], CFOP0.CFOP AS [CFOP], CFOP1.CFOP AS [CFOPFIS], HISTORICOS.CODIGO AS [HISTORICO], " +
                "CENTROCUSTOS.CODIGO AS [CENTROCUSTO], CONTACONTABIL.CODIGO AS [CTACONTABIL], NFVENDA1.CODIGOEMP01, NFVENDA1.CODIGOEMP02, " +
                "NFVENDA1.CODIGOEMP03,NFVENDA1.CODIGOEMP04,NFVENDA1.QTDE,UNIDADE.CODIGO AS [UNID],NFVENDA1.PRECO,NFVENDA1.TOTALMERCADO, " +
                "IPI.CODIGO AS [CODIPI],NFVENDA1.IPI,NFVENDA1.CUSTOIPI,NFVENDA1.VALORFRETE,NFVENDA1.VALORFRETEICMS,NFVENDA1.VALORSEGURO, " +
                "NFVENDA1.VALORSEGUROICMS, NFVENDA1.VALOROUTRAS, NFVENDA1.VALOROUTRASICMS, NFVENDA1.TOTALNOTA, NFVENDA1.BASEMP, " +
                "NFVENDA1.REDUCAO, NFVENDA1.BASEICM, NFVENDA1.ICM, NFVENDA1.CUSTOICM, NFVENDA1.BASEICMSUBST, NFVENDA1.ICMSUBST, " +
                "NFVENDA1.PISPASEP as [PISPASEPITEM],NFVENDA1.COFINS,NFVENDA1.IRRFPORC,NFVENDA1.IRRF,NFVENDA1.INSSPORC,NFVENDA1.INSS, " +
                "NFVENDA1.PISCOFINSCSLLPORC, NFVENDA1.PISCOFINSCSLL, NFVENDA1.PISPORC, NFVENDA1.PIS, NFVENDA1.COFINSPORC, " +
                "NFVENDA1.COFINS1 AS COFINS1ITEM, NFVENDA1.CSLLPORC, NFVENDA1.CSLL, NFVENDA1.ISSPORC, NFVENDA1.ISS, NFVENDA1.PESO, " +
                "NFVENDA1.PESOTOTAL, NFVENDA1.CANCELADA, NFVENDA1.NOTADEVOLUCAO, " +
                "NFVENDA1.DATADEVOLUCAO, NFVENDA1.QTDEDEVOLUCAO,NFVENDA1.UNIDDEVOLUCAO,NFVENDA1.VALORDEVOLUCAO,NFVENDA1.CODDEVOLUCAO, " +
                "NFVENDA1.QTDEDEVOLVIDA AS [QTDEDEVOLVIDA1],NFVENDA1.LEGENDACLASSFISCAL,NFVENDA1.QTDEENTRA,NFVENDA1.QTDETRANSFE,NFVENDA1.QTDESALDO, " +
                "NFVENDA1.DATADEVPREV, NFVENDA1.QTDEREJ,NFVENDA1.QTDEANA,NFVENDA1.ALIQCOMISSAO AS ALIQCOMISSAOITEM,NFVENDA1.TIPOCOMI AS TIPOCOMIITEM, " +
                "NFVENDA1.VALORCOMISSAO AS VALORCOMISSAOITEM, NFVENDA1.ALIQCOMISSAOGER AS ALIQCOMISSAOGERITEM, NFVENDA1.TIPOCOMIGER AS TIPOCOMIGERITEM, " +
                "NFVENDA1.VALORCOMISSAOGER AS VALORCOMISSAOGERITEM, PECASCLASSIFICA.CODIGO AS [GRUPO], PECASCLASSIFICA1.CODIGO AS [SUBGRUPO], " +
                "PECASCLASSIFICA2.CODIGO AS [ITEMSUBGRUPO], CLIENTE.CGC AS [CLIENTECNPJ], " +
                "NFVENDA1.PISPASEPRETIDO  AS [NFVENDA1_PISPASEPRETIDO], NFVENDA1.COFINS1RETIDO AS [NFVENDA1_COFINS1RETIDO] ",
                query, ordem);

            }


            if (rbnItemCodigo.Checked == true)
            {
                if (rbnItemAnalitica.Checked == true)
                {
                    if (rbnItemSemTotal.Checked == true)
                    { // simples
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    { // com sub total - analitica
                        if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_FORNECEDORCODIGO.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else if (lbxItemOrdem.SelectedIndex == 1) // por codigo
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_CODIGO.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_DATA.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else if (lbxItemOrdem.SelectedIndex == 3) // numero de nota fiscal
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_NUMERONF.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        {  // demais opções deixei para depois
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_CODIGO.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                        }

                    }
                }
                else
                { // Sintetica ou Analitica é a mesma
                    if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_FORNECEDORCODIGO.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (lbxItemOrdem.SelectedIndex == 1) // por CODIGO
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_CODIGO.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_DATARECE.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 3) // data emissão
                    {                                         // deixei a mesma da data de emissao por enquanto 
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_DATARECE.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport); */
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");

                    }
                    else if (lbxItemOrdem.SelectedIndex == 4) // numero de nota fiscal
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_NUMERONF.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida !!");

                    }
                    else
                    {  // demais opções deixei para depois
                        /* frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                              "DADOS_NFVENDA_ITEM_SIN_CODIGO.RPT",
                              Rel, pfields, "");

                         clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida !!");
                    }
                }
            }
            if (rbnItemCtaContabil.Checked == true)
            {
                if (rbnItemAnalitica.Checked == true)
                {
                    if (rbnItemSemTotal.Checked == true)
                    { // simples
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    { // com sub total - analitica
                        if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_FORNECEDORCONTABIL.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 1) // por codigo
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_CONTABIL.RPT",
                                 Rel, pfields, "");
 
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_DATACONTABIL.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 3) // numero de nota fiscal
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_NUMERONFCONTABIL.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");

                        }
                        else
                        {  // demais opções deixei para depois
                            /* frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                  "DADOS_NFVENDA_ITEM_ANA_CONTABIL.RPT",
                                  Rel, pfields, "");

                             clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }


                    }
                }
                else
                { // Sintetica ou Analitica é a mesma
                    if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_FORNECEDORCONTABIL.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 1) // por CODIGO
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_CONTABIL.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_DATACONTABIL.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 3) // data emissão
                    {                                         // deixei a mesma da data de emissao por enquanto 
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_DATACONTABIL.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport); */
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");

                    }
                    else if (lbxItemOrdem.SelectedIndex == 4) // numero de nota fiscal
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_NUMERONF.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida !!");

                    }
                    else
                    {  // demais opções deixei para depois
                        /* frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                              "DADOS_NFVENDA_ITEM_SIN_CODIGO.RPT",
                              Rel, pfields, "");

                         clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida !!");
                    }
                }
            }
            if (rbnItemHistorico.Checked == true)
            {
                if (rbnItemAnalitica.Checked == true)
                {
                    if (rbnItemSemTotal.Checked == true)
                    { // simples
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    { // com sub total - analitica
                        if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_FORNECEDORHISTORICO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 1) // por codigo
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_HISTORICO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_DATAHISTORICO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 3) // numero de nota fiscal
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_NUMERONFHISTORICO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else
                        {  // demais opções deixei para depois
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_HISTORICO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/

                        }


                    }
                }
                else
                { // Sintetica ou Analitica é a mesma
                    if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_FORNECEDORHISTORICO.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 1) // por CODIGO
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_HISTORICO.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_DATAHISTORICO.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 3) // numero de nota fiscal
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_NUMERONFHISTORICO.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else
                    {  // demais opções deixei para depois
                        /* frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                              "DADOS_NFVENDA_ITEM_SIN_CODIGO.RPT",
                              Rel, pfields, "");

                         clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                }
            }
            if (rbnItemCentroCusto.Checked == true)
            {
                if (rbnItemAnalitica.Checked == true)
                {
                    if (rbnItemSemTotal.Checked == true)
                    { // simples
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    { // com sub total - analitica
                        if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_FORNECEDORCERNTROCUSTO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        }
                        else if (lbxItemOrdem.SelectedIndex == 1) // por codigo
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_CENTROCUSTO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_DATACENTROCUSTO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 3) // numero de nota fiscal
                        {
                            /*   frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            .Init(clsInfo.caminhorelatorios,
                                    "DADOS_NFVENDA_ITEM_ANA_NUMERONFCENTROCUSTO.RPT",
                                    Rel, pfields, "");

                               clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else
                        {  // demais opções deixei para depois
                            /*
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_CENTROCUSTO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");

                        }


                    }
                }
                else
                { // Sintetica ou Analitica é a mesma
                    if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_FORNECEDORCENTROCUSTO.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 1) // por CODIGO
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_CENTROCUSTO.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_DATACENTROCUSTO.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 3) // numero de nota fiscal
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_NUMERONFCENTROCUSTO.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else
                    {  // demais opções deixei para depois
                        /* frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                              "DADOS_NFVENDA_ITEM_SIN_CENTROCUSTO.RPT",
                              Rel, pfields, "");

                         clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida !!");
                    }
                }
            }
            if (rbnItemCFOP.Checked == true)
            {
                if (rbnItemAnalitica.Checked == true)
                {
                    if (rbnItemSemTotal.Checked == true)
                    { // simples
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    { // com sub total - analitica
                        if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_FORNECEDORCODIGO.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 1) // por codigo
                        {
                            if (clsInfo.zempresacliente_cognome.Substring(0, 3) != "COA")
                            {
                                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                     "DADOS_NFVENDA_ITEM_ANA_CFOPFIS.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                            else
                            {
                                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                     "COATING_NFVENDA_ITEM_ANA_CFOPFIS.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                        }
                        else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                        {
                            /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_DATACFOPFIS.RPT",
                                 Rel, pfields, "");

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                            MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else if (lbxItemOrdem.SelectedIndex == 3) // numero de nota fiscal
                        {
                            if (clsInfo.zempresacliente_cognome.Substring(0, 3) != "COA")
                            {
                                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                     "DADOS_NFVENDA_ITEM_ANA_NUMERONF_CFOPFIS.RPT",
                                     Rel, pfields, "", clsInfo.conexaosqldados);

                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                            else
                            {
                                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                     "COATING_NFVENDA_ITEM_ANA_NUMERONF_CFOPFIS.RPT",
                                     Rel, pfields, "", clsInfo.conexaosqldados);

                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                            }
                            //MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        else
                        {  // demais opções deixei para depois
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_ANA_CFOPFIS.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                        }

                    }
                }
                else
                { // Sintetica ou Analitica é a mesma
                    if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_FORNECEDORCFOP.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 1) // por CODIGO
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_CFOPFIS.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 2) // data recebimento
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_SIN_DATARECE.RPT",
                             Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                    else if (lbxItemOrdem.SelectedIndex == 3) // data emissão
                    {                                         // deixei a mesma da data de emissao por enquanto 
                        if (clsInfo.zempresacliente_cognome.Substring(0, 3) != "COA")
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ITEM_SIN_NUMERONF_CFOPFIS.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "COATING_NFVENDA_ITEM_SIN_NUMERONF_CFOPFIS.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                        }
                    }
                    else
                    {
                        // demais opções deixei para depois
                        /* frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                              "DADOS_NFVENDA_ITEM_SIN_CODIGO.RPT",
                              Rel, pfields, "");

                         clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                }
            }
            if (rbnItemRemessa.Checked == true)
            {
                if (rbnModelo1.Checked == true)
                {
                    if (rbnItemAnalitica.Checked == true)
                    {
                        if (rbnItemSemTotal.Checked == true)
                        { // simples

                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                            "DADOS_NFVENDA_ITEM_REMESSA.RPT",
                            Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            if (lbxItemOrdem.SelectedIndex == 0) // por numero de nota
                            {
                                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                "DADOS_NFVENDA_ITEM_REMESSA_ANA_NUMERONF.RPT",
                                Rel, pfields, "", clsInfo.conexaosqldados);

                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                            if (lbxItemOrdem.SelectedIndex == 1) // por codigo de peça
                            {
                                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                "DADOS_NFVENDA_ITEM_REMESSA_ANA_CODIGO.RPT",
                                Rel, pfields, "", clsInfo.conexaosqldados);

                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }

                        }
                    }
                    else
                    {
                        if (lbxItemOrdem.SelectedIndex == 0) // por numero de nota
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ITEM_REMESSA_SIN_NUMERONF.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            //MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                        if (lbxItemOrdem.SelectedIndex == 1) // por codigo de peça
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                            "DADOS_NFVENDA_ITEM_REMESSA_SIN_CODIGO.RPT",
                            Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            //MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                        }
                    }

                }
                else
                {
                    if (lbxItemOrdem.SelectedIndex == 0) // por fornecedor
                    {
                        /*frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                        "DADOS_NFVENDA_ITEM_REMESSAMOV.RPT",
                        Rel, pfields, "");

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);*/
                        MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
                    }
                }
            }
            if (rbnItemContabilidade.Checked == true)
            {
                MessageBox.Show("Lista não desenvolvida  - Favor Solicitar se necessario!!");
            }

        }

        private void bntResFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResFornecedorDe.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Fornecedores", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnResFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResFornecedorAte.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Fornecedores", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnResVendedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResVendedorDe.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Vendedores", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnResVendedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResVendedorAte.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Vendedores", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void ResTipoCheckedChanged(object sender, EventArgs e)
        {
            if (rbnResTipoFaturamento.Checked == true)
            {
                rbnResCadastroTodos.Checked = true;
                lbxResSituacao.SelectedIndex = 1;

                cbxResFaturamentoS.Checked = true;
            }

            gbxResCadastro.Enabled = rbnResTipoSimples.Checked;
            gbxResSituacao.Enabled = rbnResTipoSimples.Checked;


        }

        private void GuiaCheckedChanged(object sender, EventArgs e)
        {
            if (rbnGuiaCabecalho.Checked == true)
            {
                tabFiltros.SelectedIndex = 0;
            }
            else if (rbnGuiaDetalhes.Checked == true)
            {
                tabFiltros.SelectedIndex = 1;
            }
            else if (rbnGuiaApuracoes.Checked == true)
            {
                tabFiltros.SelectedIndex = 2;
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tspApuImprimir_Click(object sender, EventArgs e)
        {
            cabecalho = "";
            query = "";
            ordem = "";

            /*
            String tipocadastro;

            if (rbnApuCliente.Checked == true)
            {
                tipocadastro = "Cliente(s)";
            }
            else
            {
                tipocadastro = "Fornecedor(es)";
            }*/

            cabecalho = "Apuração Mensal por ";

            // Tipo da Lista / Objetivo
            if (rbnApuCodigo.Checked == true) { cabecalho += "Código"; }
            if (rbnApuCtaContabil.Checked == true) { cabecalho += "Cta Contábil"; }
            if (rbnApuHistorico.Checked == true) { cabecalho += "Histórico Bco"; }
            if (rbnApuCentroCusto.Checked == true) { cabecalho += "Centro Custo=CT"; }
            if (rbnApuFornecedor.Checked == true) { cabecalho += "Fornecedor"; }

            // Tipo do Cliente (Clientes/Fornecedor)
            if (rbnCliente.Checked == true)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "CLIENTE.TIPO = 'C'";
            }
            else if (rbnFornecedor.Checked == true)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "CLIENTE.TIPO = 'F'";
            }
            // Cliente
            if (tbxApuFornecedorDe.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CLIENTE.COGNOME >= '" + tbxApuFornecedorDe.Text + "'";
                cabecalho += Environment.NewLine + "Do Cliente: " + tbxApuFornecedorDe.Text;
            }

            if (tbxApuFornecedorAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CLIENTE.COGNOME <= '" + tbxApuFornecedorAte.Text + "'";
                cabecalho += Environment.NewLine + "Até o Cliente: " + tbxApuFornecedorAte.Text;
            }
            // Tipo de Nota / Tipo de Entrada
            if (cbxApuTipoEntradaS.Checked == true ||
                cbxApuTipoEntradaE.Checked == true ||
                cbxApuTipoEntradaC.Checked == true)
            {
                if (query.Length > 0) { query += " AND "; }

                if (cbxApuTipoEntradaS.Checked == true)
                {
                    query += "(LEFT(NFVENDA.TIPOENTRADA, 1) = 'S'";
                }

                if (cbxApuTipoEntradaE.Checked == true)
                {
                    if (cbxApuTipoEntradaS.Checked == true) { query += " OR "; }
                    else { query += "("; }

                    query += " LEFT(NFVENDA.TIPOENTRADA, 1) = 'E'";
                }

                if (cbxApuTipoEntradaC.Checked == true)
                {
                    if (cbxApuTipoEntradaS.Checked == true || cbxApuTipoEntradaE.Checked == true) { query += " OR "; }
                    else { query += "("; }

                    query += " LEFT(NFVENDA.TIPOENTRADA, 1) = 'C'";
                }

                query += ")";
            }
            else
            {
                //                if (query.Length > 0) { query += " AND "; }
                //                query += " LEFT(NFVENDA.TIPOENTRADA, 2) = 'SF' ";
            }

            // Data Emissão
            SqlDateTime EmissaoDe;
            SqlDateTime EmissaoAte;

            EmissaoDe = clsParser.SqlDateTimeParse(tbxApuDtEmissaoDe.Text);
            if (tbxApuDtEmissaoAte.Text.Length >= 10)
            {
                tbxApuDtEmissaoAte.Text = tbxApuDtEmissaoAte.Text.Substring(0, 10) + " 23:59:59";
            }
            EmissaoAte = clsParser.SqlDateTimeParse(tbxApuDtEmissaoAte.Text);


            if (EmissaoDe.IsNull == false && EmissaoAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATA >= " + clsParser.SqlDateTimeFormat(EmissaoDe, true) + " AND ";
                query += "NFVENDA.DATA <= " + clsParser.SqlDateTimeFormat(EmissaoAte, true);

                cabecalho += Environment.NewLine + " Da Data de Emissão: " + EmissaoDe.Value.ToString("dd/MM/yyyy") + " até " + EmissaoAte.Value.ToString("dd/MM/yyyy");
            }
            else if (EmissaoDe.IsNull == true && EmissaoAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATA <= " + clsParser.SqlDateTimeFormat(EmissaoAte, true);

                cabecalho += Environment.NewLine + " Até a Data de Emissão: " + tbxApuDtEmissaoAte.Text;
            }
            else if (EmissaoDe.IsNull == false && EmissaoAte.IsNull == true)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATA >= " + clsParser.SqlDateTimeFormat(EmissaoDe, true);

                cabecalho += Environment.NewLine + " Da Data de Emissão: " + tbxApuDtEmissaoDe.Text;
            }

            // Data Saída
            SqlDateTime SaidaDe;
            SqlDateTime SaidaAte;

            SaidaDe = clsParser.SqlDateTimeParse(tbxApuDtSaidaDe.Text);
            if (tbxApuDtSaidaAte.Text.Length >= 10)
            {
                tbxApuDtSaidaAte.Text = tbxApuDtSaidaAte.Text.Substring(0, 10) + " 23:59:59";
            }
            SaidaAte = clsParser.SqlDateTimeParse(tbxApuDtSaidaAte.Text);

            if (SaidaDe.IsNull == false && SaidaAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATASAIDA >= " + clsParser.SqlDateTimeFormat(SaidaDe, true) + " AND ";
                query += "NFVENDA.DATASAIDA <= " + clsParser.SqlDateTimeFormat(SaidaAte, true);

                cabecalho += Environment.NewLine + " Da Data de Saída: " + tbxApuDtSaidaDe.Text + " até " + tbxApuDtSaidaAte.Text;
            }
            else if (SaidaDe.IsNull == true && SaidaAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATASAIDA <= " + clsParser.SqlDateTimeFormat(SaidaAte, true);

                cabecalho += Environment.NewLine + " Até a Data de Saída: " + tbxApuDtSaidaAte.Text;
            }
            else if (SaidaDe.IsNull == false && SaidaAte.IsNull == true)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATASAIDA >= " + clsParser.SqlDateTimeFormat(SaidaDe, true);

                cabecalho += Environment.NewLine + " Da Data de Saída: " + tbxApuDtSaidaDe.Text;
            }

            // Vendedor
            if (tbxApuVendedorDe.Text.Length > 0 && tbxApuVendedorAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "CLIENTE1.COGNOME >= '" + tbxApuVendedorDe.Text + "' AND";
                query += "CLIENTE1.COGNOME <= '" + tbxApuVendedorAte.Text + "'";

                cabecalho += Environment.NewLine + " Do Vendedor: " + tbxApuVendedorDe.Text + " até " + tbxApuVendedorAte.Text;
            }
            else if (tbxApuVendedorDe.Text.Length > 0 && tbxApuVendedorAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "CLIENTE1.COGNOME >= '" + tbxApuVendedorDe.Text + "'";

                cabecalho += Environment.NewLine + " Do Vendedor: " + tbxApuVendedorDe.Text;
            }
            else if (tbxApuVendedorDe.Text.Length == 0 && tbxApuVendedorAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "CLIENTE1.COGNOME <= '" + tbxApuVendedorAte.Text + "'";

                cabecalho += Environment.NewLine + " Até o Vendedor: " + tbxApuVendedorAte.Text;
            }

            // Código
            if (tbxApuCodDe.Text.Length > 0 && tbxApuCodAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECAS.CODIGO >= '" + tbxApuCodDe.Text + "' and ";
                query += "PECAS.CODIGO <= '" + tbxApuCodAte.Text + "'";
                cabecalho += Environment.NewLine + " Do Código: " + tbxApuCodDe.Text + " até " + tbxApuCodAte.Text;
            }
            else if (tbxApuCodDe.Text.Length > 0 && tbxApuCodAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECAS.CODIGO >= '" + tbxApuCodDe.Text + "'";
                cabecalho += Environment.NewLine + " Do Código: " + tbxApuCodDe.Text;
            }
            else if (tbxApuCodDe.Text.Length == 0 && tbxApuCodAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECAS.CODIGO <= '" + tbxApuCodAte.Text + "'";
                cabecalho += Environment.NewLine + " Até o Código: " + tbxApuCodAte.Text;
            }

            // Histórico do Banco
            if (tbxApuHistDe.Text.Length > 0 && tbxApuHistAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "HISTORICOS.CODIGO >= '" + tbxApuHistDe.Text + "' and ";
                query += "HISTORICOS.CODIGO <= '" + tbxApuHistAte.Text + "'";
                cabecalho += " Do Histórico: " + tbxApuHistDe.Text + " até " + tbxApuHistAte.Text;
            }
            else if (tbxApuHistDe.Text.Length > 0 && tbxApuHistAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "HISTORICOS.CODIGO >= '" + tbxApuHistDe.Text + "'";
                cabecalho += " Do Histórico: " + tbxApuHistDe.Text;
            }
            else if (tbxApuHistDe.Text.Length == 0 && tbxApuHistAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "HISTORICOS.CODIGO <= '" + tbxApuHistAte.Text + "'";
                cabecalho = cabecalho + " Até o Histórico: " + tbxApuHistAte.Text;
            }

            // Conta Contábil
            if (tbxApuContaContabilDe.Text.Length > 0 && tbxApuContaContabilAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CONTACONTABIL.CODIGO >= '" + tbxApuContaContabilDe.Text + "'";
                cabecalho += Environment.NewLine + " Da Cta Contábil: " + tbxApuContaContabilDe.Text;
            }
            else if (tbxApuContaContabilDe.Text.Length == 0 && tbxApuContaContabilAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CONTACONTABIL.CODIGO <= '" + tbxApuContaContabilAte.Text + "'";
                cabecalho += " Até Cta Contábil: " + tbxApuContaContabilAte.Text;
            }
            else if (tbxApuContaContabilDe.Text.Length > 0 && tbxApuContaContabilAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CONTACONTABIL.CODIGO >= '" + tbxApuContaContabilDe.Text + "' AND CONTACONTABIL.CODIGO <= '" + tbxApuContaContabilAte.Text + "'";
                cabecalho += " Do Cta Contábil: " + tbxApuContaContabilDe.Text + "  até " + tbxApuContaContabilAte.Text;
            }

            // Centro de Custo
            if (tbxApuCustoDe.Text.Length > 0 && tbxApuCustoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CENTROCUSTOS.CODIGO >= '" + tbxApuCustoDe.Text + "' and ";
                query += "CENTROCUSTOS.CODIGO <= '" + tbxApuCustoDe.Text + "'";
                cabecalho += Environment.NewLine + "Do Centro Custo: " + tbxApuCustoDe.Text + " até " + tbxApuCustoAte.Text;
            }
            else if (tbxApuCustoDe.Text.Length > 0 && tbxApuCustoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CENTROCUSTOS.CODIGO >= '" + tbxApuCustoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Do Centro Custo: " + tbxApuCustoDe.Text;
            }
            else if (tbxApuCustoDe.Text.Length == 0 && tbxApuCustoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CENTROCUSTOS.CODIGO <= '" + tbxApuCustoAte.Text + "'";
                cabecalho += Environment.NewLine + "Até o Centro Custo: " + tbxApuCustoAte.Text;
            }

            // Grupo de Material
            if (tbxApuGrupoDe.Text.Length > 0 && tbxApuGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASCLASSIFICA.CODIGO >= '" + tbxApuGrupoDe.Text + "' and ";
                query += "PECASCLASSIFICA.CODIGO <= '" + tbxApuGrupoAte.Text + "'";
                cabecalho += Environment.NewLine + "Do Grupo: " + tbxApuGrupoDe.Text + " até " + tbxApuGrupoAte.Text;
            }
            else if (tbxApuGrupoDe.Text.Length > 0 && tbxApuGrupoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASCLASSIFICA.CODIGO >= '" + tbxApuGrupoDe.Text + "'";
                cabecalho += " Do Grupo: " + tbxApuGrupoDe.Text;
            }
            else if (tbxApuGrupoDe.Text.Length == 0 && tbxApuGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASCLASSIFICA.CODIGO <= '" + tbxApuGrupoAte.Text + "'";
                cabecalho += Environment.NewLine + "Até o Grupo: " + tbxApuGrupoAte.Text;
            }

            // Sub-Grupo de Material
            if (tbxApuSubGrupoDe.Text.Length > 0 && tbxApuSubGrupoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA1.CODIGO >= '" + tbxApuSubGrupoDe.Text + "'";
                cabecalho += " Do Sub-Grupo: " + tbxApuSubGrupoDe.Text;
            }
            else if (tbxApuSubGrupoDe.Text.Length == 0 && tbxApuSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA1.CODIGO <= '" + tbxApuSubGrupoAte.Text + "'";
                cabecalho += " Até o Sub-Grupo: " + tbxApuSubGrupoAte.Text;
            }
            else if (tbxApuSubGrupoDe.Text.Length > 0 && tbxApuSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA1.CODIGO >= '" + tbxApuSubGrupoDe.Text + "' AND PECASCLASSIFICA1.CODIGO <= '" + tbxApuSubGrupoAte.Text + "'";
                cabecalho += " Do Sub-Grupo: " + tbxApuSubGrupoDe.Text + "  Até o Sub-Grupo: " + tbxApuSubGrupoAte.Text;
            }

            // Apu do Sub-Grupo de Material
            if (tbxApuSubGrupoDe.Text.Length > 0 && tbxApuSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASCLASSIFICA2.CODIGO >= '" + tbxApuSubGrupoDe.Text + "' AND ";
                query += "PECASCLASSIFICA2.CODIGO <= '" + tbxApuSubGrupoAte.Text + "'";
                cabecalho += " Do Apu Sub-Grupo: " + tbxApuSubGrupoDe.Text + "  Até o Apu Sub-Grupo: " + tbxApuSubGrupoAte.Text;
            }
            else if (tbxApuSubGrupoDe.Text.Length > 0 && tbxApuSubGrupoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA2.CODIGO >= '" + tbxApuSubGrupoDe.Text + "'";
                cabecalho += " Do Apu Sub-Grupo: " + tbxApuSubGrupoDe.Text;
            }
            else if (tbxApuSubGrupoDe.Text.Length == 0 && tbxApuSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA2.CODIGO <= '" + tbxApuSubGrupoAte.Text + "'";
                cabecalho += " Até o Apu Sub-Grupo: " + tbxApuSubGrupoAte.Text;
            }

            //Situação da nota
            if (lbxApuSituacaoNota.SelectedIndex == 0)
            {
                cabecalho = cabecalho + Environment.NewLine + "Todas as Situações";
            }
            else if (lbxApuSituacaoNota.SelectedIndex == 1)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA.SITUACAO,1) = '1' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Venda";
            }
            else if (lbxApuSituacaoNota.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA.SITUACAO,1) = '2' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Consignação";
            }
            else if (lbxApuSituacaoNota.SelectedIndex == 3)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA.SITUACAO,1) = '3' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Devolução";
            }
            else if (lbxApuSituacaoNota.SelectedIndex == 4)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "left(NFVENDA.SITUACAO,1) = '4' ";
                cabecalho = cabecalho + Environment.NewLine + "Apenas Remessa";
            }

            // Tipo Apu
            // Tipo da Entrada
            if (lbxApuTipoEntrada.SelectedIndex == 0)
            {
                cabecalho = cabecalho + " Todos os Tipos de Saída";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 1)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "cast(NFVENDA1.TIPOITEM as int) between 0 and 9";
                cabecalho = cabecalho + " Apenas [T0= 00 Até 09 Vendas]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 2)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 0";
                cabecalho = cabecalho + " Apenas [00=Venda/Compra de Produto ok]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 3)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 1";
                cabecalho = cabecalho + " Apenas [01 - Venda de Produto C/Defeito]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 4)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 2";
                cabecalho = cabecalho + " Apenas [02 - Venda/Devolução Pçs Danificadas]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 5)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 3";
                cabecalho = cabecalho + " Apenas [03 - Venda/Devolução Pçs Baixadas]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 6)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 4";
                cabecalho = cabecalho + " Apenas [04 - Produto Retrabalhado]";
            }

            else if (lbxApuTipoEntrada.SelectedIndex == 7)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 9";
                cabecalho = cabecalho + " Apenas [09 - Complemento]";
            }

            else if (lbxApuTipoEntrada.SelectedIndex == 8)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 10";
                cabecalho = cabecalho + " Apenas [10 - Devolução de Embalagem]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 9)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) between 20 and 29";
                cabecalho = cabecalho + " Apenas [T2 =  Do tipo 20 ao 29 - Devolução de Mercadoria]";
            }

            else if (lbxApuTipoEntrada.SelectedIndex == 10)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 20";
                cabecalho = cabecalho + " Apenas [20 - Devolução de Mercadoria Beneficiada]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 11)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 21";
                cabecalho = cabecalho + " Apenas [21 - Devolução de Amostra]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 12)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 22";
                cabecalho = cabecalho + " Apenas [22 - Devolução de Mercadoria Não Beneficiada]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 13)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 30";
                cabecalho = cabecalho + " Apenas [30=Prestação Serviço]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 14)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) between 40 and 49";
                cabecalho = cabecalho + " Apenas [T4 =  Do tipo 40 ao 49]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 15)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 40";
                cabecalho = cabecalho + " Apenas [40 - Remessa]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 16)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 41";
                cabecalho = cabecalho + " Apenas [41 - Remessa MP Terceiro]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 17)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 42";
                cabecalho = cabecalho + " Apenas [42 - Remessa MP Terceiro com contra ordem]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 18)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 44";
                cabecalho = cabecalho + " Apenas [44 - Remessa p/Retrabalho [Deduz Ctas Pagar]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 19)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 45";
                cabecalho = cabecalho + " Apenas [45 - Remessa p/Retrabalho [Sem Debito]]";
            }
            else if (lbxApuTipoEntrada.SelectedIndex == 20)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "cast(NFVENDA1.TIPOITEM as int) = 90";
                cabecalho = cabecalho + " Apenas [90=Imobilizado]";
            }

            /////////////////////////////////////////////////////////////////////////////////////////
            // ESPECIAIS DE CADA RELATORIO / MODELO PARA A QUERY
            /////////////////////////////////////////////////////////////////////////////////////////
            //Ordem
            if (rbnApuCodigo.Checked == true)
            {
                ordem = "CODIGO";
                //lbxApuOrdem.Text = "Codigo";
            }
            if (rbnApuCtaContabil.Checked == true)
            {
                ordem = "CTACONTABIL";
                //lbxApuOrdem.Text = "Conta Contabil";
            }
            if (rbnApuHistorico.Checked == true)
            {
                ordem = "HISTORICO";
                //lbxApuOrdem.Text = "Historico";
            }
            if (rbnApuCentroCusto.Checked == true)
            {
                ordem = "CENTROCUSTO";
                //lbxApuOrdem.Text = "Centro de Custo";
            }
            if (rbnApuFornecedor.Checked == true)
            {
                ordem = "CLIENTE";
                //lbxApuOrdem.Text = "Fornecedor";
            }

            // Ordem da Lista williams
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + ordem;
            // QUERY

            DataTable Rel = new DataTable();
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            ParameterField pfieldEmpresa = new ParameterField();
            ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            if (carregarcampos.Rows.Count > 0)
            {
                disvalEmpresa.Value = carregarcampos.Rows[0]["NOME"].ToString();
                pfieldEmpresa.Name = "EMPRESA";
                pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            }
            ParameterField pfieldCabecalho = new ParameterField();
            ParameterDiscreteValue disvalCabecalho = new ParameterDiscreteValue();
            disvalCabecalho.Value = cabecalho.ToString();
            pfieldCabecalho.Name = "CABECALHO";
            pfieldCabecalho.CurrentValues.Add(disvalCabecalho);


            pfields.Add(pfieldEmpresa);
            pfields.Add(pfieldCabecalho);

            /*
            Rel = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "NFVENDA INNER JOIN " +
                                  "DOCFISCAL ON DOCFISCAL.ID = NFVENDA.IDDOCUMENTO INNER JOIN " +
                                  "CLIENTE ON CLIENTE.ID = NFVENDA.IDCLIENTE INNER JOIN " +
                                  "CONDPAGTO ON CONDPAGTO.ID = NFVENDA.IDCONDPAGTO INNER JOIN " +
                                  "SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = NFVENDA.IDFORMAPAGTO INNER JOIN " +
                                  "CLIENTE AS CLIENTE1 ON CLIENTE1.ID = NFVENDA.IDVENDEDOR INNER JOIN " +
                                  "CLIENTE AS CLIENTE2 ON CLIENTE2.ID = NFVENDA.IDTRANSPORTADORA INNER JOIN " +
                                  "NFVENDA1 ON NFVENDA1.NUMERO = NFVENDA.ID INNER JOIN " +
                                  "PEDIDO ON PEDIDO.ID = NFVENDA1.IDPEDIDO INNER JOIN " +
                                  "PEDIDO1 ON PEDIDO1.ID = NFVENDA1.IDPEDIDOITEM INNER JOIN " +
                                  "PEDIDO2 ON PEDIDO2.ID = NFVENDA1.IDPEDIDO2 INNER JOIN " +
                                  "ORDEMSERVICO ON ORDEMSERVICO.ID = NFVENDA1.IDORDEMSERVICO INNER JOIN " +
                                  "CERTIFICADO ON CERTIFICADO.ID = NFVENDA1.IDCERTIFICADO INNER JOIN " +
                                  "PECAS ON PECAS.ID = NFVENDA1.IDCODIGO INNER JOIN " +
                                  "SITTRIBUTARIAA ON SITTRIBUTARIAA.ID = NFVENDA1.IDSITTRIBA INNER JOIN " +
                                  "SITTRIBUTARIAB ON SITTRIBUTARIAB.ID = NFVENDA1.IDSITTRIBB INNER JOIN " +
                                  "CFOP AS CFOP0 ON CFOP0.ID = NFVENDA1.IDCFOP INNER JOIN " +
                                  "CFOP AS CFOP1 ON CFOP1.ID = NFVENDA1.IDCFOPFIS INNER JOIN " +
                                  "COABANCO.dbo.HISTORICOS ON COABANCO.dbo.HISTORICOS.ID = NFVENDA1.IDHISTORICO INNER JOIN " +
                                  "COABANCO.dbo.CENTROCUSTOS ON COABANCO.dbo.CENTROCUSTOS.ID = NFVENDA1.IDCENTROCUSTO INNER JOIN " +
                                  "CONTACONTABIL ON CONTACONTABIL.ID = NFVENDA1.IDCODIGOCTABIL INNER JOIN " +
                                  "UNIDADE ON UNIDADE.ID = NFVENDA1.IDUNID INNER JOIN " +
                                  "IPI ON IPI.ID = NFVENDA1.IDIPI INNER JOIN " +
                                  "PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA INNER JOIN " +
                                  "PECASCLASSIFICA1 ON PECASCLASSIFICA1.ID = PECAS.IDCLASSIFICA1 INNER JOIN " +
                                  "PECASCLASSIFICA2 ON PECASCLASSIFICA2.ID = PECAS.IDCLASSIFICA2 ",
                                  " NFVENDA.ID AS IDNFV, NFVENDA.FILIAL AS FILIALNFV, NFVENDA.NUMERO AS NUMERONFV, NFVENDA.DATA AS DATANFV, NFVENDA.DATASAIDA AS DATASAIDANFV, " +
                                  "DOCFISCAL.CODIGO AS DOCUMENTO, DOCFISCAL.COGNOME AS DOCUMENTOCOGNOME, NFVENDA.SERIE, NFVENDA.TIPOENTRADA, " +
                                  "CLIENTE.COGNOME AS CLIENTE, NFVENDA.SITUACAO, SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTOCODIGO, " +
                                  "SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOME, CLIENTE1.COGNOME AS VENDEDOR, NFVENDA.TIPOCOMI, NFVENDA.COMISSAO, NFVENDA.TIPOCOMIGER, " +
                                  "NFVENDA.COMISSAOGER, NFVENDA.MODELO, NFVENDA.FRETE, NFVENDA.FRETEPAGA, NFVENDA.TRANSPORTE, CLIENTE2.COGNOME AS TRANSPORTADORA, " +
                                  "NFVENDA.TIPOFRETE, NFVENDA.TOTALBASEICM, NFVENDA.TOTALICM, NFVENDA.TOTALBASEICMSUBST, NFVENDA.TOTALICMSUBST, NFVENDA.TOTALPESO, " +
                                  "NFVENDA.TOTALPESOBRUTO, NFVENDA.TOTALMERCADORIA, NFVENDA.TOTALIPI, NFVENDA.TOTALFRETE, NFVENDA.TOTALSEGURO, NFVENDA.TOTALOUTRAS, " +
                                  "NFVENDA.TOTALNOTAFISCAL, NFVENDA.PISPASEP, NFVENDA.COFINS1, NFVENDA.QTDEENVIADA, NFVENDA.QTDEDEVOLVIDA, NFVENDA.VALORCOMISSAO, " +
                                  "NFVENDA.VALORCOMISSAOGER, NFVENDA1.TIPOITEM, PEDIDO.NUMERO AS NROPEDIDOVENDA, ORDEMSERVICO.NUMERO AS NROORDEMSERVICO, " +
                                  "CERTIFICADO.NUMERO AS NROCERTIFICADO, PECAS.TIPOPRODUTO, PECAS.CODIGO, PECAS.NOME AS CODIGONOME, NFVENDA1.COMPLEMENTO, " +
                                  "NFVENDA1.COMPLEMENTO1, NFVENDA1.DESCRICAOESP, NFVENDA1.MSG, NFVENDA1.CONSUMO, SITTRIBUTARIAA.CODIGO AS SITTRIBA, " +
                                  "SITTRIBUTARIAB.CODIGO AS SITTRIBB, CFOP0.CFOP, CFOP1.CFOP AS CFOPFIS, COABANCO.dbo.HISTORICOS.CODIGO AS HISTORICO, " +
                                  "COABANCO.dbo.CENTROCUSTOS.CODIGO AS CENTROCUSTO, CONTACONTABIL.CODIGO AS CTACONTABIL, NFVENDA1.CODIGOEMP01, NFVENDA1.CODIGOEMP02, " +
                                  "NFVENDA1.CODIGOEMP03, NFVENDA1.CODIGOEMP04, NFVENDA1.QTDE, UNIDADE.CODIGO AS UNID, NFVENDA1.PRECO, NFVENDA1.TOTALMERCADO, " +
                                  "IPI.CODIGO AS CODIPI, NFVENDA1.IPI, NFVENDA1.CUSTOIPI, NFVENDA1.VALORFRETE, NFVENDA1.VALORFRETEICMS, NFVENDA1.VALORSEGURO, " +
                                  "NFVENDA1.VALORSEGUROICMS, NFVENDA1.VALOROUTRAS, NFVENDA1.VALOROUTRASICMS, NFVENDA1.TOTALNOTA, NFVENDA1.BASEMP, NFVENDA1.REDUCAO, " +
                                  "NFVENDA1.BASEICM, NFVENDA1.ICM, NFVENDA1.CUSTOICM, NFVENDA1.BASEICMSUBST, NFVENDA1.ICMSUBST, NFVENDA1.PISPASEP AS PISPASEPITEM, " +
                                  "NFVENDA1.COFINS, NFVENDA1.IRRFPORC, NFVENDA1.IRRF, NFVENDA1.INSSPORC, NFVENDA1.INSS, NFVENDA1.PISCOFINSCSLLPORC, NFVENDA1.PISCOFINSCSLL, " +
                                  "NFVENDA1.PISPORC, NFVENDA1.PIS, NFVENDA1.COFINSPORC, NFVENDA1.COFINS1 AS COFINS1ITEM, NFVENDA1.CSLLPORC, NFVENDA1.CSLL, " +
                                  "NFVENDA1.ISSPORC, NFVENDA1.ISS, NFVENDA1.PESO, NFVENDA1.PESOTOTAL, NFVENDA1.CANCELADA, NFVENDA1.IDNFCOMPRA, NFVENDA1.IDITEMNFCOMPRA, " +
                                  "NFVENDA1.NOTADEVOLUCAO, NFVENDA1.DATADEVOLUCAO, NFVENDA1.QTDEDEVOLUCAO, NFVENDA1.UNIDDEVOLUCAO, NFVENDA1.VALORDEVOLUCAO, " +
                                  "NFVENDA1.CODDEVOLUCAO, NFVENDA1.QTDEDEVOLVIDA AS QTDEDEVOLVIDA1, NFVENDA1.LEGENDACLASSFISCAL, NFVENDA1.QTDEENTRA, " +
                                  "NFVENDA1.QTDETRANSFE, NFVENDA1.QTDESALDO, NFVENDA1.DATADEVPREV, NFVENDA1.QTDEREJ, NFVENDA1.QTDEANA, " +
                                  "NFVENDA1.ALIQCOMISSAO AS ALIQCOMISSAOITEM, NFVENDA1.TIPOCOMI AS TIPOCOMIITEM, NFVENDA1.VALORCOMISSAO AS VALORCOMISSAOITEM, " +
                                  "NFVENDA1.ALIQCOMISSAOGER AS ALIQCOMISSAOGERITEM, NFVENDA1.TIPOCOMIGER AS TIPOCOMIGERITEM, " +
                                  "NFVENDA1.VALORCOMISSAOGER AS VALORCOMISSAOGERITEM, PECASCLASSIFICA.CODIGO AS GRUPO, PECASCLASSIFICA1.CODIGO AS SUBGRUPO, " +
                                  "PECASCLASSIFICA2.CODIGO AS ITEMSUBGRUPO",
                        query, ordem);
            */

            Rel = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "NFVENDA " +
                "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = NFVENDA.IDDOCUMENTO  " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = NFVENDA.IDCLIENTE " +
                "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID = NFVENDA.IDCONDPAGTO " +
                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=NFVENDA.IDFORMAPAGTO " +
                "LEFT JOIN CLIENTE CLIENTE1 ON CLIENTE1.ID = NFVENDA.IDVENDEDOR  " +
                "LEFT JOIN CLIENTE CLIENTE3 ON CLIENTE3.ID = NFVENDA.IDSUPERVISOR " +
                "LEFT JOIN CLIENTE CLIENTE4 ON CLIENTE4.ID = NFVENDA.IDCOORDENADOR " +
                "LEFT JOIN CLIENTE CLIENTE2 ON CLIENTE2.ID = NFVENDA.IDTRANSPORTADORA " +
                "LEFT JOIN NFVENDA1 ON NFVENDA1.NUMERO = NFVENDA.ID " +
                "LEFT JOIN PEDIDO ON PEDIDO.ID = NFVENDA1.IDPEDIDO " +
                "LEFT JOIN PEDIDO1 ON PEDIDO1.ID = NFVENDA1.IDPEDIDOITEM " +
                "LEFT JOIN PEDIDO2 ON PEDIDO2.ID = NFVENDA1.IDPEDIDO2 " +
                "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = NFVENDA1.IDORDEMSERVICO " +
                "LEFT JOIN CERTIFICADO ON CERTIFICADO.ID=NFVENDA1.IDCERTIFICADO " +
                "LEFT JOIN PECAS ON PECAS.ID=NFVENDA1.IDCODIGO " +
                "LEFT JOIN SITTRIBUTARIAA ON SITTRIBUTARIAA.ID = NFVENDA1.IDSITTRIBA " +
                "LEFT JOIN SITTRIBUTARIAB ON SITTRIBUTARIAB.ID = NFVENDA1.IDSITTRIBB " +
                "LEFT JOIN CFOP CFOP0 ON CFOP0.ID = NFVENDA1.IDCFOP " +
                "LEFT JOIN CFOP CFOP1 ON CFOP1.ID = NFVENDA1.IDCFOPFIS " +
                "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS ON HISTORICOS.ID = NFVENDA1.IDHISTORICO " +
                "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS ON CENTROCUSTOS.ID = NFVENDA1.IDCENTROCUSTO " +
                "LEFT JOIN CONTACONTABIL ON CONTACONTABIL.ID = NFVENDA1.IDCODIGOCTABIL " +
                "LEFT JOIN UNIDADE ON UNIDADE.ID = NFVENDA1.IDUNID " +
                "LEFT JOIN IPI ON IPI.ID = NFVENDA1.IDIPI " +
                "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA " +
                "LEFT JOIN PECASCLASSIFICA1 ON PECASCLASSIFICA1.ID = PECAS.IDCLASSIFICA1 " +
                "LEFT JOIN PECASCLASSIFICA2 ON PECASCLASSIFICA2.ID = PECAS.IDCLASSIFICA2  ",

                "NFVENDA.ID AS IDNFV,NFVENDA.FILIAL AS FILIALNFV,NFVENDA.NUMERO AS NUMERONFV,NFVENDA.DATA AS DATANFV, " +
                "NFVENDA.DATASAIDA AS DATASAIDANFV, DOCFISCAL.CODIGO AS DOCUMENTO, DOCFISCAL.COGNOME AS DOCUMENTOCOGNOME, " +
                "NFVENDA.SERIE,NFVENDA.TIPOENTRADA,CLIENTE.COGNOME as CLIENTE,NFVENDA.SITUACAO,SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTOCODIGO, " +
                "SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOME,CLIENTE1.COGNOME AS VENDEDOR,NFVENDA.TIPOCOMI,NFVENDA.COMISSAO, " +
                "NFVENDA.TIPOCOMIGER,NFVENDA.COMISSAOGER,NFVENDA.MODELO,NFVENDA.FRETE,NFVENDA.FRETEPAGA,NFVENDA.TRANSPORTE, " +
                "CLIENTE2.COGNOME AS TRANSPORTADORA, NFVENDA.TIPOFRETE, NFVENDA.TOTALBASEICM, NFVENDA.TOTALICM, NFVENDA.TOTALBASEICMSUBST, " +
                "NFVENDA.TOTALICMSUBST,NFVENDA.TOTALPESO,NFVENDA.TOTALPESOBRUTO,NFVENDA.TOTALMERCADORIA,NFVENDA.TOTALIPI, " +
                "NFVENDA.TOTALFRETE, NFVENDA.TOTALSEGURO, NFVENDA.TOTALOUTRAS, NFVENDA.TOTALNOTAFISCAL, NFVENDA.PISPASEP, " +
                "NFVENDA.COFINS1, NFVENDA.QTDEENVIADA, NFVENDA.QTDEDEVOLVIDA, NFVENDA.VALORCOMISSAO, NFVENDA.VALORCOMISSAOGER, " +
                "NFVENDA1.TIPOITEM, PEDIDO.NUMERO AS [NROPEDIDOVENDA], ORDEMSERVICO.NUMERO AS [NROORDEMSERVICO], CERTIFICADO.NUMERO AS [NROCERTIFICADO], " +
                "PECAS.TIPOPRODUTO AS [TIPOPRODUTO],PECAS.CODIGO AS [CODIGO],PECAS.NOME AS [CODIGONOME],NFVENDA1.COMPLEMENTO, " +
                "NFVENDA1.COMPLEMENTO1,NFVENDA1.DESCRICAOESP,NFVENDA1.MSG,NFVENDA1.CONSUMO,SITTRIBUTARIAA.CODIGO AS [SITTRIBA], " +
                "SITTRIBUTARIAB.CODIGO AS [SITTRIBB], CFOP0.CFOP AS [CFOP], CFOP1.CFOP AS [CFOPFIS], HISTORICOS.CODIGO AS [HISTORICO], " +
                "CENTROCUSTOS.CODIGO AS [CENTROCUSTO], CONTACONTABIL.CODIGO AS [CTACONTABIL], NFVENDA1.CODIGOEMP01, NFVENDA1.CODIGOEMP02, " +
                "NFVENDA1.CODIGOEMP03,NFVENDA1.CODIGOEMP04,NFVENDA1.QTDE,UNIDADE.CODIGO AS [UNID],NFVENDA1.PRECO,NFVENDA1.TOTALMERCADO, " +
                "IPI.CODIGO AS [CODIPI],NFVENDA1.IPI,NFVENDA1.CUSTOIPI,NFVENDA1.VALORFRETE,NFVENDA1.VALORFRETEICMS,NFVENDA1.VALORSEGURO, " +
                "NFVENDA1.VALORSEGUROICMS, NFVENDA1.VALOROUTRAS, NFVENDA1.VALOROUTRASICMS, NFVENDA1.TOTALNOTA, NFVENDA1.BASEMP, " +
                "NFVENDA1.REDUCAO, NFVENDA1.BASEICM, NFVENDA1.ICM, NFVENDA1.CUSTOICM, NFVENDA1.BASEICMSUBST, NFVENDA1.ICMSUBST, " +
                "NFVENDA1.PISPASEP as [PISPASEPITEM],NFVENDA1.COFINS,NFVENDA1.IRRFPORC,NFVENDA1.IRRF,NFVENDA1.INSSPORC,NFVENDA1.INSS, " +
                "NFVENDA1.PISCOFINSCSLLPORC, NFVENDA1.PISCOFINSCSLL, NFVENDA1.PISPORC, NFVENDA1.PIS, NFVENDA1.COFINSPORC, " +
                "NFVENDA1.COFINS1 AS COFINS1ITEM, NFVENDA1.CSLLPORC, NFVENDA1.CSLL, NFVENDA1.ISSPORC, NFVENDA1.ISS, NFVENDA1.PESO, " +
                "NFVENDA1.PESOTOTAL, NFVENDA1.CANCELADA, NFVENDA1.NOTADEVOLUCAO, " +
                "NFVENDA1.DATADEVOLUCAO, NFVENDA1.QTDEDEVOLUCAO,NFVENDA1.UNIDDEVOLUCAO,NFVENDA1.VALORDEVOLUCAO,NFVENDA1.CODDEVOLUCAO, " +
                "NFVENDA1.QTDEDEVOLVIDA AS [QTDEDEVOLVIDA1],NFVENDA1.LEGENDACLASSFISCAL,NFVENDA1.QTDEENTRA,NFVENDA1.QTDETRANSFE,NFVENDA1.QTDESALDO, " +
                "NFVENDA1.DATADEVPREV, NFVENDA1.QTDEREJ,NFVENDA1.QTDEANA,NFVENDA1.ALIQCOMISSAO AS ALIQCOMISSAOITEM,NFVENDA1.TIPOCOMI AS TIPOCOMIITEM, " +
                "NFVENDA1.VALORCOMISSAO AS VALORCOMISSAOITEM, NFVENDA1.ALIQCOMISSAOGER AS ALIQCOMISSAOGERITEM, NFVENDA1.TIPOCOMIGER AS TIPOCOMIGERITEM, " +
                "NFVENDA1.VALORCOMISSAOGER AS VALORCOMISSAOGERITEM, PECASCLASSIFICA.CODIGO AS [GRUPO], PECASCLASSIFICA1.CODIGO AS [SUBGRUPO], " +
                "PECASCLASSIFICA2.CODIGO AS [ITEMSUBGRUPO] ",
                query, ordem);

            Rel.DefaultView.Sort = "IDNFV";
            Rel = Rel.DefaultView.ToTable();

            ////ABAIXO SO USAMOS POR CAUSA DE DUPLICIDADE DE MESMO CODIGO EM MAIS DE UMA LINHA
            ////MAS A PROCEDURE ACIMA FOI MODIFICADA E ATE AGORA NÃO DUPLICOU MAIS
            //tabelaaux = clsGridHelper.SelectDistinct(Rel, "IDNFV1");
            //DataRow drTabelaaux2;
            //Int32 idold = 0;
            //tabelaaux2 = Rel.Clone();
            //foreach (DataRow dgvrTabela in tabelaaux.Rows)
            //{
            //    drTabelaaux2 = tabelaaux2.NewRow();

            //    foreach (DataRow dgvrRel in Rel.Rows)
            //    {
            //        if (dgvrTabela["IDNFV1"].ToString() == dgvrRel["IDNFV1"].ToString()
            //            && idold != clsParser.Int32Parse(dgvrTabela["IDNFV1"].ToString()))
            //        {
            //            idold = clsParser.Int32Parse(dgvrTabela["IDNFV1"].ToString());
            //            drTabelaaux2["IDNFV"] = dgvrRel["IDNFV"];
            //            drTabelaaux2["IDNFV1"] = dgvrRel["IDNFV1"];
            //            drTabelaaux2["CLIENTE"] = dgvrRel["CLIENTE"];
            //            drTabelaaux2["CODIGO"] = dgvrRel["CODIGO"];
            //            drTabelaaux2["CTACONTABIL"] = dgvrRel["CTACONTABIL"];
            //            drTabelaaux2["HISTORICO"] = dgvrRel["HISTORICO"];
            //            drTabelaaux2["CENTROCUSTO"] = dgvrRel["CENTROCUSTO"];
            //            drTabelaaux2["TOTALNOTA"] = dgvrRel["TOTALNOTA"];
            //            drTabelaaux2["DATASAIDANFV"] = dgvrRel["DATASAIDANFV"];
            //            tabelaaux2.Rows.Add(drTabelaaux2);
            //        }
            //        else
            //        {
            //        }
            //    }
            //}     

            if (rbnApuGraficoS.Checked == true)
            {
                if (rbnApuCodigo.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_G_CODIGO.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                if (rbnApuCtaContabil.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_G_CONTABIL.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                if (rbnApuHistorico.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_G_HISTORICO.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                if (rbnApuCentroCusto.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_G_CENTROCUSTO.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                if (rbnApuFornecedor.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_G_FORNECEDOR.RPT",
                          Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
            }
            else
            {
                if (rbnApuCodigo.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_CODIGO.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                if (rbnApuCtaContabil.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_CONTABIL.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                if (rbnApuHistorico.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_HISTORICO.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                if (rbnApuCentroCusto.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_CENTROCUSTO.RPT",
                         Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
                if (rbnApuFornecedor.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                         "DADOS_NFVENDA_APURA_FORNECEDOR.RPT",
                          Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
            }
        }

        private void tspApuRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddAutoComplete(TextBox _textbox, TextBox _colecao)
        {
            if (_textbox.InvokeRequired)
            {
                Invoke(new AddnewDelegate(AddAutoComplete), _textbox, _colecao);
            }
            else
            {
                _textbox.AutoCompleteCustomSource = _colecao.AutoCompleteCustomSource;
            }
        }

        private void bwrCarregaAutoComplete_RunWorkerAsync()
        {
            bwrCarregaAutoComplete = new BackgroundWorker();
            bwrCarregaAutoComplete.DoWork += new DoWorkEventHandler(bwrCarregaAutoComplete_DoWork);
            bwrCarregaAutoComplete.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCarregaAutoComplete_RunWorkerCompleted);
            bwrCarregaAutoComplete.RunWorkerAsync();
        }


        private void bwrCarregaAutoComplete_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (tipocli)
            {
                case "TODOS":
                    //AddAutoComplete(tbxItemFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "", "COGNOME", false));
                    //AddAutoComplete(tbxItemFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "", "COGNOME", false));

                    //AddAutoComplete(tbxResFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "", "COGNOME", false));
                    //AddAutoComplete(tbxResFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "", "COGNOME", false));

                    AddAutoComplete(tbxApuFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "", "COGNOME", false));
                    AddAutoComplete(tbxApuFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "", "COGNOME", false));

                    break;

                case "FORNECEDOR":
                    //AddAutoComplete(tbxItemFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));
                    //AddAutoComplete(tbxItemFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));

                    //AddAutoComplete(tbxResFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));
                    //AddAutoComplete(tbxResFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));

                    AddAutoComplete(tbxApuFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));
                    AddAutoComplete(tbxApuFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));

                    break;

                case "CLIENTE":
                    //AddAutoComplete(tbxItemFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'C'", "COGNOME", false));
                    //AddAutoComplete(tbxItemFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'C'", "COGNOME", false));

                    //AddAutoComplete(tbxResFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'C'", "COGNOME", false));
                    //AddAutoComplete(tbxResFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'C'", "COGNOME", false));

                    AddAutoComplete(tbxApuFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'C'", "COGNOME", false));
                    AddAutoComplete(tbxApuFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'C'", "COGNOME", false));

                    break;

                default:
                    //AddAutoComplete(tbxItemFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND (TIPO = 'F' OR TIPO = 'C')", "COGNOME", false));
                    //AddAutoComplete(tbxItemFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND (TIPO = 'F' OR TIPO = 'C')", "COGNOME", false));

                    //AddAutoComplete(tbxResFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));
                    //AddAutoComplete(tbxResFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));

                    AddAutoComplete(tbxApuFornecedorDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));
                    AddAutoComplete(tbxApuFornecedorAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ATIVO = 'S' AND TIPO = 'F'", "COGNOME", false));


                    break;
            }
            //AddAutoComplete(tbxItemCodDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECAS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            //AddAutoComplete(tbxItemCodAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECAS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            //AddAutoComplete(tbxItemHistDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            //AddAutoComplete(tbxItemHistAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            //AddAutoComplete(tbxItemContaContabilDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            //AddAutoComplete(tbxItemContaContabilAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            //AddAutoComplete(tbxItemCustoDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            //AddAutoComplete(tbxItemCustoAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            //AddAutoComplete(tbxItemGrupoDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA", "CODIGO", "", "CODIGO", false));
            //AddAutoComplete(tbxItemGrupoAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA", "CODIGO", "", "CODIGO", false));
            //AddAutoComplete(tbxItemSubGrupoDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA1", "CODIGO", "IDCLASSIFICA=" + idClassifica + " ", "CODIGO", false));
            //AddAutoComplete(tbxItemSubGrupoAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA1", "CODIGO", "IDCLASSIFICA=" + idClassifica + " ", "CODIGO", false));
            //AddAutoComplete(tbxItemItemSubGrupoDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA2", "CODIGO", "IDCLASSIFICA1=" + idClassifica1 + " ", "CODIGO", false));
            //AddAutoComplete(tbxItemItemSubGrupoAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA2", "CODIGO", "IDCLASSIFICA1=" + idClassifica1 + " ", "CODIGO", false));

            ///
            AddAutoComplete(tbxApuCodDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECAS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            AddAutoComplete(tbxApuCodAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECAS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            AddAutoComplete(tbxApuHistDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            AddAutoComplete(tbxApuHistAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            AddAutoComplete(tbxApuContaContabilDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            AddAutoComplete(tbxApuContaContabilAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            AddAutoComplete(tbxApuCustoDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            AddAutoComplete(tbxApuCustoAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ATIVO = 'S'", "CODIGO", false));
            AddAutoComplete(tbxApuGrupoDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA", "CODIGO", "", "CODIGO", false));
            AddAutoComplete(tbxApuGrupoAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA", "CODIGO", "", "CODIGO", false));
            AddAutoComplete(tbxApuSubGrupoDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA1", "CODIGO", "IDCLASSIFICA=" + idClassifica + " ", "CODIGO", false));
            AddAutoComplete(tbxApuSubGrupoAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA1", "CODIGO", "IDCLASSIFICA=" + idClassifica + " ", "CODIGO", false));
            AddAutoComplete(tbxApuItemSubGrupoDe, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA2", "CODIGO", "IDCLASSIFICA1=" + idClassifica1 + " ", "CODIGO", false));
            AddAutoComplete(tbxApuItemSubGrupoAte, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "PECASCLASSIFICA2", "CODIGO", "IDCLASSIFICA1=" + idClassifica1 + " ", "CODIGO", false));
        }

        private void bwrCarregaAutoComplete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnApuFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvClienteApu";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnApuFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvClienteApu";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnApuCodDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCodigoApu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECAS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnApuHistDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvHistoricoApu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnApuHistAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvHistorico1Apu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "HISTORICOS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnApuContaContabilDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCtaContabilApu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONTACONTABIL", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnApuContaContabilAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCtaContabil1Apu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "CONTACONTABIL", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnApuCustoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCtoCustoApu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnApuCustoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCtoCusto1Apu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqlbanco, "CENTROCUSTOS", 0, "ATIVO", "S");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqlbanco);
        }

        private void btnApuGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvGrupoApu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA", idClassifica, "Pecas Classifica");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnApuGrupoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvGrupo1Apu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA", idClassifica, "PECASCLASSIFICA");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnApuSubGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvSubGrupoApu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA1", idClassifica1, "IDCLASSIFICA", idClassifica.ToString());

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnApuSubGrupoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvSubGrupo1Apu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA1", idClassifica1, "IDCLASSIFICA", idClassifica.ToString());

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnApuItemSubGrupoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvItemSubGrupoApu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA2", idClassifica2, "IDCLASSIFICA1", idClassifica1.ToString());

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void btnApuItemSubGrupoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvItemSubGrupo1Apu";
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA2", idClassifica2, "IDCLASSIFICA1", idClassifica1.ToString());

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        public void Lupa2()
        {
            if (clsInfo.znomegrid == "dgvClienteApu")
            {
                if (clsInfo.zrow != null)
                {
                    idfornecedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    //if (idfornecedor > 0 && Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ATIVO", "ID", idfornecedor) == "S")
                    if (idfornecedor > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ativo from CLIENTE where ID=" + idfornecedor, "") == "S")
                    {
                        tbxApuFornecedorDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from CLIENTE where ID=" + idfornecedor, "").ToString();
                        //tbxApuFornecedorDe.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idfornecedor).ToString();
                    }
                }
                tbxApuFornecedorDe.Select();
            }

            if (clsInfo.znomegrid == "dgvCliente1Apu")
            {
                if (clsInfo.zrow != null)
                {
                    idfornecedor1 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    //if (idfornecedor1 > 0 && Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "ATIVO", "ID", idfornecedor) == "S")
                    if (idfornecedor1 > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ativo from CLIENTE where ID=" + idfornecedor, "") == "S")
                    {
                        //clsClienteInfo = clsClienteBLL.Carregar(clsInfo.conexaosqldados, idfornecedor1);
                        //tbxItemFornecedorAte.Text = clsClienteInfo.cognome;      
                        tbxApuFornecedorAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from CLIENTE where ID=" + idfornecedor1, "").ToString();
                        //tbxApuFornecedorAte.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idfornecedor1).ToString();
                    }
                }
                tbxApuFornecedorAte.Select();
            }

            if (clsInfo.znomegrid == "dgvCodigoApu")
            {
                if (clsInfo.zrow != null)
                {
                    //if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaValor(clsInfo.conexaosqldados, "PECAS", "ATIVO", "ID", clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString())) == "S")
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ativo from pecas where ID=" + clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()), "") == "S")
                    {
                        tbxApuCodDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvCodigo1Apu")
            {
                if (clsInfo.zrow != null)
                {
                    //if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaValor(clsInfo.conexaosqldados, "PECAS", "ATIVO", "ID", clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString())) == "S")
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ativo from PECAS where ID=" + clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()), "") == "S")
                    {
                        tbxApuCodAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvHistoricoApu")
            {
                if (clsInfo.zrow != null)
                {
                    // if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ATIVO", "ID", clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString())) == "S")
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ativo from HISTORICOS where ID=" + clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()), "") == "S")
                    {
                        tbxApuHistDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvHistorico1Apu")
            {
                if (clsInfo.zrow != null)
                {
                    // if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ATIVO", "ID", clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString())) == "S")
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ativo from HISTORICOS where ID=" + clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()), "") == "S")
                    {
                        tbxApuHistAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvCtaContabilApu")
            {
                if (clsInfo.zrow != null)
                {
                    //if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "ATIVO", "ID", clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString())) == "S")
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ativo from CONTACONTABIL where ID=" + clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()), "") == "S")
                    {
                        tbxApuContaContabilDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvCtaContabil1Apu")
            {
                if (clsInfo.zrow != null)
                {
                    //  if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "ATIVO", "ID", clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString())) == "S")
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ativo from CONTACONTABIL where ID=" + clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()), "") == "S")
                    {
                        tbxApuContaContabilAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvCtoCustoApu")
            {
                if (clsInfo.zrow != null)
                {
                    //if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ATIVO", "ID", clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString())) == "S")
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ativo from CENTROCUSTOS where ID=" + clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()), "") == "S")
                    {
                        tbxApuCustoDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvCtoCusto1Apu")
            {
                if (clsInfo.zrow != null)
                {
                    // if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ATIVO", "ID", clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString())) == "S")
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0 && Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ativo from CENTROCUSTOS where ID=" + clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()), "") == "S")
                    {
                        tbxApuCustoAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
            }

            if (clsInfo.znomegrid == "dgvGrupoApu")
            {
                if (clsInfo.zrow != null)
                {
                    idClassifica = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxApuGrupoDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
            }

            if (clsInfo.znomegrid == "dgvGrupo1Apu")
            {
                if (clsInfo.zrow != null)
                {
                    idClassificaa = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxApuGrupoAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
            }


            if (clsInfo.znomegrid == "dgvSubGrupoApu")
            {
                if (clsInfo.zrow != null)
                {
                    idClassifica1 = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxApuSubGrupoDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
            }
            if (clsInfo.znomegrid == "dgvSubGrupo1Apu")
            {
                if (clsInfo.zrow != null)
                {
                    idClassifica1a = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxApuSubGrupoAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
            }

            if (clsInfo.znomegrid == "dgvItemSubGrupoApu")
            {
                if (clsInfo.zrow != null)
                {
                    idClassifica2 = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxApuItemSubGrupoDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
            }
            if (clsInfo.znomegrid == "dgvItemSubGrupo1Apu")
            {
                if (clsInfo.zrow != null)
                {
                    idClassifica2a = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxApuItemSubGrupoAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
            }




            clsInfo.zrow = null;
            clsInfo.znomegrid = "";
        }

        private void rbnFiltroApu(object sender, EventArgs e)
        {
            if (rbnApuCliente.Checked == true)
            {
                tipocli = "CLIENTE";
                bwrCarregaAutoComplete_RunWorkerAsync();
            }
            if (rbnApuForne.Checked == true)
            {
                tipocli = "FORNECEDOR";
                bwrCarregaAutoComplete_RunWorkerAsync();
            }
            if (rbnApuTodos.Checked == true)
            {
                tipocli = "TODOS";
                bwrCarregaAutoComplete_RunWorkerAsync();
            }
        }

        private void frmRelNFVenda_Load(object sender, EventArgs e)
        {
            tspResumo.Visible = true;
            tspItens.Visible = true;
            tspItens.Visible = true;
            tspApuracao.Visible = true;
            tabFiltros.SelectedTab = tabCabecalho;
        }

        private void tspItemRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabFiltros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabFiltros.SelectedIndex == 0)
            {
                rbnResTipoSimples.Select();
            }
            if (tabFiltros.SelectedIndex == 1)
            {
                rbnItemCodigo.Select();
            }
            if (tabFiltros.SelectedIndex == 2)
            {
                rbnApuForne.Select();
            }
        }

        private void ResTipoFaturamentoS_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxResFaturamentoS.Checked == false &&
                cbxResFaturamentoC.Checked == false &&
                cbxResFaturamentoE.Checked == false)
            {
                cbxResFaturamentoS.Checked = true;
            }
        }

        private void ItemTipoFaturamento_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxItemTipoFaturamentoS.Checked == false &&
                cbxItemTipoFaturamentoC.Checked == false &&
                cbxItemTipoFaturamentoE.Checked == false)
            {
                // cbxItemTipoFaturamentoS.Checked = true;
            }
        }

        private void tspEspImprimir_Click(object sender, EventArgs e)
        {
            cabecalho = "";
            query = "";
            ordem = "";

            cabecalho = "Relatório ";

            // Cabeçalho  -- objetivo da lista
            if (rbnEspPintura.Checked == true)
            {                
                cabecalho += " de Peças Pintadas ";
                query = " LEFT(NFVENDA.TIPOENTRADA,1)='S' and LEFT(NFVENDA.SITUACAO,1)='1' and NFVENDA1.TIPOITEM<'09' and PECASPROCESSO.TIPO='4' ";
            }

            if (rbnEspPinturaPoXEcoat.Checked == true)
            {                
                cabecalho += " de Peças Pintadas ";
                query = " LEFT(NFVENDA.TIPOENTRADA,1)='S' and LEFT(NFVENDA.SITUACAO,1)='1' and NFVENDA1.TIPOITEM<'09' ";
            }


            // Tipo de Nota / Tipo de Entrada
            // apenas as de saida

            // Cliente
            if (tbxEspClienteDe.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CLIENTE.COGNOME >= '" + tbxEspClienteDe.Text + "'";
                cabecalho += Environment.NewLine + "Do Cliente: " + tbxEspClienteDe.Text;
            }

            if (tbxEspClienteAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CLIENTE.COGNOME <= '" + tbxEspClienteAte.Text + "'";
                cabecalho += Environment.NewLine + "Até o Cliente: " + tbxEspClienteAte.Text;
            }

            // Data Emissão
            SqlDateTime EmissaoDe;
            SqlDateTime EmissaoAte;

            EmissaoDe = clsParser.SqlDateTimeParse(tbxEspDtEmissaoDe.Text);
            EmissaoAte = clsParser.SqlDateTimeParse(tbxEspDtEmissaoAte.Text);

            if (EmissaoDe.IsNull == false && EmissaoAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATA >= " + clsParser.SqlDateTimeFormat(EmissaoDe, true) + " AND ";
                query += "NFVENDA.DATA <= " + clsParser.SqlDateTimeFormat(EmissaoAte, true);

                cabecalho += Environment.NewLine + " Da Data de Emissão: " + EmissaoDe.Value.ToString("dd/MM/yyyy") + " até " + EmissaoAte.Value.ToString("dd/MM/yyyy");
            }
            else if (EmissaoDe.IsNull == true && EmissaoAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATA <= " + clsParser.SqlDateTimeFormat(EmissaoAte, true);

                cabecalho += Environment.NewLine + " Até a Data de Emissão: " + tbxEspDtEmissaoAte.Text;
            }
            else if (EmissaoDe.IsNull == false && EmissaoAte.IsNull == true)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATA >= " + clsParser.SqlDateTimeFormat(EmissaoDe, true);

                cabecalho += Environment.NewLine + " Da Data de Emissão: " + tbxEspDtEmissaoDe.Text;
            }

            // Data Saída
            SqlDateTime SaidaDe;
            SqlDateTime SaidaAte;

            SaidaDe = clsParser.SqlDateTimeParse(tbxEspDtSaidaDe.Text);
            SaidaAte = clsParser.SqlDateTimeParse(tbxEspDtSaidaAte.Text);

            if (SaidaDe.IsNull == false && SaidaAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATASAIDA >= " + clsParser.SqlDateTimeFormat(SaidaDe, true) + " AND ";
                query += "NFVENDA.DATASAIDA <= " + clsParser.SqlDateTimeFormat(SaidaAte, true);

                cabecalho += Environment.NewLine + " Da Data de Saída: " + tbxEspDtSaidaDe.Text + " até " + tbxEspDtSaidaAte.Text;
            }
            else if (SaidaDe.IsNull == true && SaidaAte.IsNull == false)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATASAIDA <= " + clsParser.SqlDateTimeFormat(SaidaAte, true);

                cabecalho += Environment.NewLine + " Até a Data de Saída: " + tbxEspDtSaidaAte.Text;
            }
            else if (SaidaDe.IsNull == false && SaidaAte.IsNull == true)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "NFVENDA.DATASAIDA >= " + clsParser.SqlDateTimeFormat(SaidaDe, true);

                cabecalho += Environment.NewLine + " Da Data de Saída: " + tbxEspDtSaidaDe.Text;
            }

            // Vendedor
            if (tbxEspVendedorDe.Text.Length > 0 && tbxEspVendedorAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "CLIENTE1.COGNOME >= '" + tbxEspVendedorDe.Text + "' AND";
                query += "CLIENTE1.COGNOME <= '" + tbxEspVendedorAte.Text + "'";

                cabecalho += Environment.NewLine + " Do Vendedor: " + tbxEspVendedorDe.Text + " até " + tbxEspVendedorAte.Text;
            }
            else if (tbxEspVendedorDe.Text.Length > 0 && tbxEspVendedorAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "CLIENTE1.COGNOME >= '" + tbxEspVendedorDe.Text + "'";

                cabecalho += Environment.NewLine + " Do Vendedor: " + tbxEspVendedorDe.Text;
            }
            else if (tbxEspVendedorDe.Text.Length == 0 && tbxEspVendedorAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }

                query += "CLIENTE1.COGNOME <= '" + tbxEspVendedorAte.Text + "'";

                cabecalho += Environment.NewLine + " Até o Vendedor: " + tbxEspVendedorAte.Text;
            }

            // Código
            if (tbxEspCodDe.Text.Length > 0 && tbxEspCodAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECAS.CODIGO >= '" + tbxEspCodDe.Text + "' and ";
                query += "PECAS.CODIGO <= '" + tbxEspCodAte.Text + "'";
                cabecalho += Environment.NewLine + " Do Código: " + tbxEspCodDe.Text + " até " + tbxEspCodAte.Text;
            }
            else if (tbxEspCodDe.Text.Length > 0 && tbxEspCodAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECAS.CODIGO >= '" + tbxEspCodDe.Text + "'";
                cabecalho += Environment.NewLine + " Do Código: " + tbxEspCodDe.Text;
            }
            else if (tbxEspCodDe.Text.Length == 0 && tbxEspCodAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECAS.CODIGO <= '" + tbxEspCodAte.Text + "'";
                cabecalho += Environment.NewLine + " Até o Código: " + tbxEspCodAte.Text;
            }

            // Codigo da Pintura

            if (tbxEspPinturaDe.Text.Length > 0 && tbxEspPinturaAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASPROCESSO.CODIGO >= '" + tbxEspPinturaDe.Text + "' and ";
                query += "PECASPROCESSO.CODIGO <= '" + tbxEspPinturaAte.Text + "'";
                cabecalho += " Da Pintura: " + tbxEspPinturaDe.Text + " até " + tbxEspPinturaAte.Text;
            }
            else if (tbxEspPinturaDe.Text.Length > 0 && tbxEspPinturaAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASPROCESSO.CODIGO >= '" + tbxEspPinturaDe.Text + "'";
                cabecalho += " Da Pintura: " + tbxEspPinturaDe.Text;
            }
            else if (tbxEspPinturaDe.Text.Length == 0 && tbxEspPinturaAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASPROCESSO.CODIGO <= '" + tbxEspPinturaAte.Text + "'";
                cabecalho = cabecalho + " Até a Pintura: " + tbxEspPinturaAte.Text;
            }

            // Conta Contábil
            if (tbxEspContaContabilDe.Text.Length > 0 && tbxEspContaContabilAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CONTACONTABIL.CODIGO >= '" + tbxEspContaContabilDe.Text + "'";
                cabecalho += Environment.NewLine + " Da Cta Contábil: " + tbxEspContaContabilDe.Text;
            }
            else if (tbxEspContaContabilDe.Text.Length == 0 && tbxEspContaContabilAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CONTACONTABIL.CODIGO <= '" + tbxEspContaContabilAte.Text + "'";
                cabecalho += " Até Cta Contábil: " + tbxEspContaContabilAte.Text;
            }
            else if (tbxEspContaContabilDe.Text.Length > 0 && tbxEspContaContabilAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CONTACONTABIL.CODIGO >= '" + tbxEspContaContabilDe.Text + "' AND CONTACONTABIL.CODIGO <= '" + tbxEspContaContabilAte.Text + "'";
                cabecalho += " Do Cta Contábil: " + tbxEspContaContabilDe.Text + "  até " + tbxEspContaContabilAte.Text;
            }
            /*
            // Centro de Custo
            if (tbxEspCustoDe.Text.Length > 0 && tbxEspCustoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CENTROCUSTOS.CODIGO >= '" + tbxEspCustoDe.Text + "' and ";
                query += "CENTROCUSTOS.CODIGO <= '" + tbxEspCustoDe.Text + "'";
                cabecalho += Environment.NewLine + "Do Centro Custo: " + tbxEspCustoDe.Text + " até " + tbxEspCustoAte.Text;
            }
            else if (tbxEspCustoDe.Text.Length > 0 && tbxEspCustoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CENTROCUSTOS.CODIGO >= '" + tbxEspCustoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Do Centro Custo: " + tbxEspCustoDe.Text;
            }
            else if (tbxEspCustoDe.Text.Length == 0 && tbxEspCustoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "CENTROCUSTOS.CODIGO <= '" + tbxEspCustoAte.Text + "'";
                cabecalho += Environment.NewLine + "Até o Centro Custo: " + tbxEspCustoAte.Text;
            }
            */
            // Grupo de Material
            if (tbxEspGrupoDe.Text.Length > 0 && tbxEspGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASCLASSIFICA.CODIGO >= '" + tbxEspGrupoDe.Text + "' and ";
                query += "PECASCLASSIFICA.CODIGO <= '" + tbxEspGrupoAte.Text + "'";
                cabecalho += Environment.NewLine + "Do Grupo: " + tbxEspGrupoDe.Text + " até " + tbxEspGrupoAte.Text;
            }
            else if (tbxEspGrupoDe.Text.Length > 0 && tbxEspGrupoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASCLASSIFICA.CODIGO >= '" + tbxEspGrupoDe.Text + "'";
                cabecalho += " Do Grupo: " + tbxEspGrupoDe.Text;
            }
            else if (tbxEspGrupoDe.Text.Length == 0 && tbxEspGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASCLASSIFICA.CODIGO <= '" + tbxEspGrupoAte.Text + "'";
                cabecalho += Environment.NewLine + "Até o Grupo: " + tbxEspGrupoAte.Text;
            }

            // Sub-Grupo de Material
            if (tbxEspSubGrupoDe.Text.Length > 0 && tbxEspSubGrupoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA1.CODIGO >= '" + tbxEspSubGrupoDe.Text + "'";
                cabecalho += " Do Sub-Grupo: " + tbxEspSubGrupoDe.Text;
            }
            else if (tbxEspSubGrupoDe.Text.Length == 0 && tbxEspSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA1.CODIGO <= '" + tbxEspSubGrupoAte.Text + "'";
                cabecalho += " Até o Sub-Grupo: " + tbxEspSubGrupoAte.Text;
            }
            else if (tbxEspSubGrupoDe.Text.Length > 0 && tbxEspSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA1.CODIGO >= '" + tbxEspSubGrupoDe.Text + "' AND PECASCLASSIFICA1.CODIGO <= '" + tbxEspSubGrupoAte.Text + "'";
                cabecalho += " Do Sub-Grupo: " + tbxEspSubGrupoDe.Text + "  Até o Sub-Grupo: " + tbxEspSubGrupoAte.Text;
            }

            // Esp do Sub-Grupo de Material
            if (tbxEspSubGrupoDe.Text.Length > 0 && tbxEspSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query += "PECASCLASSIFICA2.CODIGO >= '" + tbxEspSubGrupoDe.Text + "' AND ";
                query += "PECASCLASSIFICA2.CODIGO <= '" + tbxEspSubGrupoAte.Text + "'";
                cabecalho += " Do Esp Sub-Grupo: " + tbxEspSubGrupoDe.Text + "  Até o Esp Sub-Grupo: " + tbxEspItemSubGrupoAte.Text;
            }
            else if (tbxItemItemSubGrupoDe.Text.Length > 0 && tbxItemItemSubGrupoAte.Text.Length == 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA2.CODIGO >= '" + tbxItemItemSubGrupoDe.Text + "'";
                cabecalho += " Do Item Sub-Grupo: " + tbxItemItemSubGrupoDe.Text;
            }
            else if (tbxItemItemSubGrupoDe.Text.Length == 0 && tbxItemItemSubGrupoAte.Text.Length > 0)
            {
                if (query.Length > 0) { query += " AND "; }
                query = query + "PECASCLASSIFICA2.CODIGO <= '" + tbxItemItemSubGrupoAte.Text + "'";
                cabecalho += " Até o Item Sub-Grupo: " + tbxItemItemSubGrupoAte.Text;
            }

            /////////////////////////////////////////////////////////////////////////////////////////
            // ESPECIAIS DE CADA RELATORIO / MODELO PARA A QUERY
            /////////////////////////////////////////////////////////////////////////////////////////
            
            //Ordem
            if (lbxEspOrdem.SelectedIndex == 0)
            {
                if (rbnEspPintura.Checked == true)
                {
                    ordem = "CLIENTE.COGNOME, PECAS.CODIGO, NFVENDA.NUMERO";
                }

                if (rbnEspPinturaPoXEcoat.Checked == true)
                {
                    ordem = "CLIENTE.COGNOME, PECAS.CODIGO, NFVENDA.NUMERO";
                }
            }
            else
            {
                ordem = "CLIENTE.COGNOME, PECAS.CODIGO, NFVENDA.NUMERO";           
            }

            // Cabecalho (lista Analitica ou Sintetica)
            if (rbnEspAnalitica.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Tipo relatorio Analitica";
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + "Tipo relatorio Sintetica";
            }
            //  
            if (rbnEspSemTotal.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Sem Sub-Total";
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + "Com Sub-Total";
            }
            // Ordem da Lista williams
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxEspOrdem.Text;
            // QUERY

            DataTable Rel = new DataTable();

            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            ParameterField pfieldEmpresa = new ParameterField();
            ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            if (carregarcampos.Rows.Count > 0)
            {
                disvalEmpresa.Value = carregarcampos.Rows[0]["NOME"].ToString();
                pfieldEmpresa.Name = "EMPRESA";
                pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            }
            ParameterField pfieldCabecalho = new ParameterField();
            ParameterDiscreteValue disvalCabecalho = new ParameterDiscreteValue();
            disvalCabecalho.Value = cabecalho.ToString();
            pfieldCabecalho.Name = "CABECALHO";
            pfieldCabecalho.CurrentValues.Add(disvalCabecalho);


            pfields.Add(pfieldEmpresa);
            pfields.Add(pfieldCabecalho);

            //RELATORIOS
            if (rbnEspPintura.Checked == true)
            {
                Rel = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                       "NFVENDA1  " +
                       "INNER JOIN NFVENDA ON NFVENDA.ID = NFVENDA1.NUMERO " +
                       "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = NFVENDA.IDDOCUMENTO   " +
                       "LEFT JOIN CLIENTE ON CLIENTE.ID = NFVENDA.IDCLIENTE  " +
                       "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID = NFVENDA.IDCONDPAGTO  " +
                       "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=NFVENDA.IDFORMAPAGTO  " +
                       "LEFT JOIN CLIENTE CLIENTE1 ON CLIENTE1.ID = NFVENDA.IDVENDEDOR   " +
                       "LEFT JOIN CLIENTE CLIENTE3 ON CLIENTE3.ID = NFVENDA.IDSUPERVISOR  " +
                       "LEFT JOIN CLIENTE CLIENTE4 ON CLIENTE4.ID = NFVENDA.IDCOORDENADOR  " +
                       "LEFT JOIN CLIENTE CLIENTE2 ON CLIENTE2.ID = NFVENDA.IDTRANSPORTADORA  " +
                       "LEFT JOIN PEDIDO ON PEDIDO.ID = NFVENDA1.IDPEDIDO  " +
                       "LEFT JOIN PEDIDO1 ON PEDIDO1.ID = NFVENDA1.IDPEDIDOITEM  " +
                       "LEFT JOIN PEDIDO2 ON PEDIDO2.ID = NFVENDA1.IDPEDIDO2  " +
                       "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = NFVENDA1.IDORDEMSERVICO  " +
                       "LEFT JOIN CERTIFICADO ON CERTIFICADO.ID=NFVENDA1.IDCERTIFICADO  " +
                       "LEFT JOIN PECAS ON PECAS.ID=NFVENDA1.IDCODIGO  " +
                       "LEFT JOIN PECASPROCESSO ON PECASPROCESSO.IDPRODUTO=PECAS.ID " +
                       "LEFT JOIN SITTRIBUTARIAA ON SITTRIBUTARIAA.ID = NFVENDA1.IDSITTRIBA  " +
                       "LEFT JOIN SITTRIBUTARIAB ON SITTRIBUTARIAB.ID = NFVENDA1.IDSITTRIBB  " +
                       "LEFT JOIN CFOP CFOP0 ON CFOP0.ID = NFVENDA1.IDCFOP  " +
                       "LEFT JOIN CFOP CFOP1 ON CFOP1.ID = NFVENDA1.IDCFOPFIS  " +
                       "LEFT JOIN CONTACONTABIL ON CONTACONTABIL.ID = NFVENDA1.IDCODIGOCTABIL  " +
                       "LEFT JOIN UNIDADE ON UNIDADE.ID = NFVENDA1.IDUNID  " +
                       "LEFT JOIN IPI ON IPI.ID = NFVENDA1.IDIPI  " +
                       "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA  " +
                       "LEFT JOIN PECASCLASSIFICA1 ON PECASCLASSIFICA1.ID = PECAS.IDCLASSIFICA1  " +
                       "LEFT JOIN PECASCLASSIFICA2 ON PECASCLASSIFICA2.ID = PECAS.IDCLASSIFICA2 ",  

                       "NFVENDA.ID AS IDNFV,NFVENDA.FILIAL AS FILIALNFV,NFVENDA.NUMERO AS NUMERONFV,NFVENDA.DATA AS DATANFV,  " +
                       "NFVENDA.DATASAIDA AS DATASAIDANFV, DOCFISCAL.CODIGO AS DOCUMENTO, DOCFISCAL.COGNOME AS DOCUMENTOCOGNOME,  " +
                       "NFVENDA.SERIE,NFVENDA.TIPOENTRADA,CLIENTE.COGNOME as CLIENTE,NFVENDA.SITUACAO,SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTOCODIGO,  " +
                       "SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOME,CLIENTE1.COGNOME AS VENDEDOR,NFVENDA.TIPOCOMI,NFVENDA.COMISSAO,  " +
                       "NFVENDA.TIPOCOMIGER,NFVENDA.COMISSAOGER,NFVENDA.MODELO,NFVENDA.FRETE,NFVENDA.FRETEPAGA,NFVENDA.TRANSPORTE,  " +
                       "CLIENTE2.COGNOME AS TRANSPORTADORA, NFVENDA.TIPOFRETE, NFVENDA.TOTALBASEICM, NFVENDA.TOTALICM, NFVENDA.TOTALBASEICMSUBST,  " +
                       "NFVENDA.TOTALICMSUBST,NFVENDA.TOTALPESO,NFVENDA.TOTALPESOBRUTO,NFVENDA.TOTALMERCADORIA,NFVENDA.TOTALIPI,  " +
                       "NFVENDA.TOTALFRETE, NFVENDA.TOTALSEGURO, NFVENDA.TOTALOUTRAS, NFVENDA.TOTALNOTAFISCAL, NFVENDA.PISPASEP,  " +
                       "NFVENDA.COFINS1, NFVENDA.QTDEENVIADA, NFVENDA.QTDEDEVOLVIDA, NFVENDA.VALORCOMISSAO, NFVENDA.VALORCOMISSAOGER, " +
                       "NFVENDA1.TIPOITEM, PEDIDO.NUMERO AS [NROPEDIDOVENDA], ORDEMSERVICO.NUMERO AS [NROORDEMSERVICO], CERTIFICADO.NUMERO AS [NROCERTIFICADO],  " +
                       "PECAS.TIPOPRODUTO AS [TIPOPRODUTO],PECAS.CODIGO AS [CODIGO],PECAS.NOME AS [CODIGONOME],NFVENDA1.COMPLEMENTO,  " +
                       "NFVENDA1.COMPLEMENTO1,NFVENDA1.DESCRICAOESP,NFVENDA1.MSG,NFVENDA1.CONSUMO,SITTRIBUTARIAA.CODIGO AS [SITTRIBA],  " +
                       "SITTRIBUTARIAB.CODIGO AS [SITTRIBB], CFOP0.CFOP AS [CFOP], CFOP1.CFOP AS [CFOPFIS], " +
                       "CONTACONTABIL.CODIGO AS [CTACONTABIL], NFVENDA1.CODIGOEMP01, NFVENDA1.CODIGOEMP02,  " +
                       "NFVENDA1.CODIGOEMP03,NFVENDA1.CODIGOEMP04,NFVENDA1.QTDE,UNIDADE.CODIGO AS [UNID],NFVENDA1.PRECO,NFVENDA1.TOTALMERCADO,  " +
                       "IPI.CODIGO AS [CODIPI],NFVENDA1.IPI,NFVENDA1.CUSTOIPI,NFVENDA1.VALORFRETE,NFVENDA1.VALORFRETEICMS,NFVENDA1.VALORSEGURO,  " +
                       "NFVENDA1.VALORSEGUROICMS, NFVENDA1.VALOROUTRAS, NFVENDA1.VALOROUTRASICMS, NFVENDA1.TOTALNOTA, NFVENDA1.BASEMP,  " +
                       "NFVENDA1.REDUCAO, NFVENDA1.BASEICM, NFVENDA1.ICM, NFVENDA1.CUSTOICM, NFVENDA1.BASEICMSUBST, NFVENDA1.ICMSUBST,  " +
                       "NFVENDA1.PISPASEP as [PISPASEPITEM],NFVENDA1.COFINS,NFVENDA1.IRRFPORC,NFVENDA1.IRRF,NFVENDA1.INSSPORC,NFVENDA1.INSS," +
                       "NFVENDA1.PISCOFINSCSLLPORC, NFVENDA1.PISCOFINSCSLL, NFVENDA1.PISPORC, NFVENDA1.PIS, NFVENDA1.COFINSPORC,  " +
                       "NFVENDA1.COFINS1 AS COFINS1ITEM, NFVENDA1.CSLLPORC, NFVENDA1.CSLL, NFVENDA1.ISSPORC, NFVENDA1.ISS, NFVENDA1.PESO,  " +
                       "NFVENDA1.PESOTOTAL, NFVENDA1.CANCELADA, NFVENDA1.NOTADEVOLUCAO,  " +
                       "NFVENDA1.DATADEVOLUCAO, NFVENDA1.QTDEDEVOLUCAO,NFVENDA1.UNIDDEVOLUCAO,NFVENDA1.VALORDEVOLUCAO,NFVENDA1.CODDEVOLUCAO,  " +
                       "NFVENDA1.QTDEDEVOLVIDA AS [QTDEDEVOLVIDA1],NFVENDA1.LEGENDACLASSFISCAL,NFVENDA1.QTDEENTRA,NFVENDA1.QTDETRANSFE,NFVENDA1.QTDESALDO,  " +
                       "NFVENDA1.DATADEVPREV, NFVENDA1.QTDEREJ,NFVENDA1.QTDEANA,NFVENDA1.ALIQCOMISSAO AS ALIQCOMISSAOITEM,NFVENDA1.TIPOCOMI AS TIPOCOMIITEM,  " +
                       "NFVENDA1.VALORCOMISSAO AS VALORCOMISSAOITEM, NFVENDA1.ALIQCOMISSAOGER AS ALIQCOMISSAOGERITEM, NFVENDA1.TIPOCOMIGER AS TIPOCOMIGERITEM,  " +
                       "NFVENDA1.VALORCOMISSAOGER AS VALORCOMISSAOGERITEM, PECASCLASSIFICA.CODIGO AS [GRUPO], PECASCLASSIFICA1.CODIGO AS [SUBGRUPO],  " +
                       "PECASCLASSIFICA2.CODIGO AS [ITEMSUBGRUPO], PECASPROCESSO.CODIGO AS [CODIGOPINTURA], PECAS.DIAMAREA ",
                       query, ordem);

                if (rbnEspModelo1.Checked == true)
                {
                    if (rbnItemAnalitica.Checked == true)
                    {
                        if (rbnItemSemTotal.Checked == true)
                        { // simples
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ESP_PINTURA.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        { // com sub total - analitica
                        }
                    }
                    else
                    { // Sintetica ou Analitica é a mesma
                    }
                }
                else
                {
                    MessageBox.Show("Apenas Lista simples");
                }
            }
            //Po X Ecoat
            if (rbnEspPinturaPoXEcoat.Checked == true)
            {
                Rel = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "NFVENDA " +
                "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = NFVENDA.IDDOCUMENTO  " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = NFVENDA.IDCLIENTE " +
                "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID = NFVENDA.IDCONDPAGTO " +
                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID=NFVENDA.IDFORMAPAGTO " +
                "LEFT JOIN CLIENTE CLIENTE1 ON CLIENTE1.ID = NFVENDA.IDVENDEDOR  " +
                "LEFT JOIN CLIENTE CLIENTE3 ON CLIENTE3.ID = NFVENDA.IDSUPERVISOR " +
                "LEFT JOIN CLIENTE CLIENTE4 ON CLIENTE4.ID = NFVENDA.IDCOORDENADOR " +
                "LEFT JOIN CLIENTE CLIENTE2 ON CLIENTE2.ID = NFVENDA.IDTRANSPORTADORA " +
                "LEFT JOIN NFVENDA1 ON NFVENDA1.NUMERO = NFVENDA.ID " +
                "LEFT JOIN PEDIDO ON PEDIDO.ID = NFVENDA1.IDPEDIDO " +
                "LEFT JOIN PEDIDO1 ON PEDIDO1.ID = NFVENDA1.IDPEDIDOITEM " +
                "LEFT JOIN PEDIDO2 ON PEDIDO2.ID = NFVENDA1.IDPEDIDO2 " +
                "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = NFVENDA1.IDORDEMSERVICO " +
                "LEFT JOIN CERTIFICADO ON CERTIFICADO.ID=NFVENDA1.IDCERTIFICADO " +
                "LEFT JOIN PECAS ON PECAS.ID=NFVENDA1.IDCODIGO " +
                "LEFT JOIN SITTRIBUTARIAA ON SITTRIBUTARIAA.ID = NFVENDA1.IDSITTRIBA " +
                "LEFT JOIN SITTRIBUTARIAB ON SITTRIBUTARIAB.ID = NFVENDA1.IDSITTRIBB " +
                "LEFT JOIN CFOP CFOP0 ON CFOP0.ID = NFVENDA1.IDCFOP " +
                "LEFT JOIN CFOP CFOP1 ON CFOP1.ID = NFVENDA1.IDCFOPFIS " +
                "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS ON HISTORICOS.ID = NFVENDA1.IDHISTORICO " +
                "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS ON CENTROCUSTOS.ID = NFVENDA1.IDCENTROCUSTO " +
                "LEFT JOIN CONTACONTABIL ON CONTACONTABIL.ID = NFVENDA1.IDCODIGOCTABIL " +
                "LEFT JOIN UNIDADE ON UNIDADE.ID = NFVENDA1.IDUNID " +
                "LEFT JOIN IPI ON IPI.ID = NFVENDA1.IDIPI " +
                "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA " +
                "LEFT JOIN PECASCLASSIFICA1 ON PECASCLASSIFICA1.ID = PECAS.IDCLASSIFICA1 " +
                "LEFT JOIN PECASCLASSIFICA2 ON PECASCLASSIFICA2.ID = PECAS.IDCLASSIFICA2  ",

                "NFVENDA.ID AS IDNFV,NFVENDA.FILIAL AS FILIALNFV,NFVENDA.NUMERO AS NUMERONFV,NFVENDA.DATA AS DATANFV, " +
                "NFVENDA.DATASAIDA AS DATASAIDANFV, DOCFISCAL.CODIGO AS DOCUMENTO, DOCFISCAL.COGNOME AS DOCUMENTOCOGNOME, " +
                "NFVENDA.SERIE,NFVENDA.TIPOENTRADA,CLIENTE.COGNOME as CLIENTE,NFVENDA.SITUACAO,SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTOCODIGO, " +
                "SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOME,CLIENTE1.COGNOME AS VENDEDOR,NFVENDA.TIPOCOMI,NFVENDA.COMISSAO, " +
                "NFVENDA.TIPOCOMIGER,NFVENDA.COMISSAOGER,NFVENDA.MODELO,NFVENDA.FRETE,NFVENDA.FRETEPAGA,NFVENDA.TRANSPORTE, " +
                "CLIENTE2.COGNOME AS TRANSPORTADORA, NFVENDA.TIPOFRETE, NFVENDA.TOTALBASEICM, NFVENDA.TOTALICM, NFVENDA.TOTALBASEICMSUBST, " +
                "NFVENDA.TOTALICMSUBST,NFVENDA.TOTALPESO,NFVENDA.TOTALPESOBRUTO,NFVENDA.TOTALMERCADORIA,NFVENDA.TOTALIPI, " +
                "NFVENDA.TOTALFRETE, NFVENDA.TOTALSEGURO, NFVENDA.TOTALOUTRAS, NFVENDA.TOTALNOTAFISCAL, NFVENDA.PISPASEP, " +
                "NFVENDA.COFINS1, NFVENDA.QTDEENVIADA, NFVENDA.QTDEDEVOLVIDA, NFVENDA.VALORCOMISSAO, NFVENDA.VALORCOMISSAOGER, " +
                "NFVENDA1.TIPOITEM, PEDIDO.NUMERO AS [NROPEDIDOVENDA], ORDEMSERVICO.NUMERO AS [NROORDEMSERVICO], CERTIFICADO.NUMERO AS [NROCERTIFICADO], " +
                "PECAS.TIPOPRODUTO AS [TIPOPRODUTO],PECAS.CODIGO AS [CODIGO],PECAS.NOME AS [CODIGONOME],NFVENDA1.COMPLEMENTO, " +
                "NFVENDA1.COMPLEMENTO1,NFVENDA1.DESCRICAOESP,NFVENDA1.MSG,NFVENDA1.CONSUMO,SITTRIBUTARIAA.CODIGO AS [SITTRIBA], " +
                "SITTRIBUTARIAB.CODIGO AS [SITTRIBB], CFOP0.CFOP AS [CFOP], CFOP1.CFOP AS [CFOPFIS], HISTORICOS.CODIGO AS [HISTORICO], " +
                "CENTROCUSTOS.CODIGO AS [CENTROCUSTO], CONTACONTABIL.CODIGO AS [CTACONTABIL], NFVENDA1.CODIGOEMP01, NFVENDA1.CODIGOEMP02, " +
                "NFVENDA1.CODIGOEMP03,NFVENDA1.CODIGOEMP04,NFVENDA1.QTDE,UNIDADE.CODIGO AS [UNID],NFVENDA1.PRECO,NFVENDA1.TOTALMERCADO, " +
                "IPI.CODIGO AS [CODIPI],NFVENDA1.IPI,NFVENDA1.CUSTOIPI,NFVENDA1.VALORFRETE,NFVENDA1.VALORFRETEICMS,NFVENDA1.VALORSEGURO, " +
                "NFVENDA1.VALORSEGUROICMS, NFVENDA1.VALOROUTRAS, NFVENDA1.VALOROUTRASICMS, NFVENDA1.TOTALNOTA, NFVENDA1.BASEMP, " +
                "NFVENDA1.REDUCAO, NFVENDA1.BASEICM, NFVENDA1.ICM, NFVENDA1.CUSTOICM, NFVENDA1.BASEICMSUBST, NFVENDA1.ICMSUBST, " +
                "NFVENDA1.PISPASEP as [PISPASEPITEM],NFVENDA1.COFINS,NFVENDA1.IRRFPORC,NFVENDA1.IRRF,NFVENDA1.INSSPORC,NFVENDA1.INSS, " +
                "NFVENDA1.PISCOFINSCSLLPORC, NFVENDA1.PISCOFINSCSLL, NFVENDA1.PISPORC, NFVENDA1.PIS, NFVENDA1.COFINSPORC, " +
                "NFVENDA1.COFINS1 AS COFINS1ITEM, NFVENDA1.CSLLPORC, NFVENDA1.CSLL, NFVENDA1.ISSPORC, NFVENDA1.ISS, NFVENDA1.PESO, " +
                "NFVENDA1.PESOTOTAL, NFVENDA1.CANCELADA, NFVENDA1.NOTADEVOLUCAO, " +
                "NFVENDA1.DATADEVOLUCAO, NFVENDA1.QTDEDEVOLUCAO,NFVENDA1.UNIDDEVOLUCAO,NFVENDA1.VALORDEVOLUCAO,NFVENDA1.CODDEVOLUCAO, " +
                "NFVENDA1.QTDEDEVOLVIDA AS [QTDEDEVOLVIDA1],NFVENDA1.LEGENDACLASSFISCAL,NFVENDA1.QTDEENTRA,NFVENDA1.QTDETRANSFE,NFVENDA1.QTDESALDO, " +
                "NFVENDA1.DATADEVPREV, NFVENDA1.QTDEREJ,NFVENDA1.QTDEANA,NFVENDA1.ALIQCOMISSAO AS ALIQCOMISSAOITEM,NFVENDA1.TIPOCOMI AS TIPOCOMIITEM, " +
                "NFVENDA1.VALORCOMISSAO AS VALORCOMISSAOITEM, NFVENDA1.ALIQCOMISSAOGER AS ALIQCOMISSAOGERITEM, NFVENDA1.TIPOCOMIGER AS TIPOCOMIGERITEM, " +
                "NFVENDA1.VALORCOMISSAOGER AS VALORCOMISSAOGERITEM, PECASCLASSIFICA.CODIGO AS [GRUPO], PECASCLASSIFICA1.CODIGO AS [SUBGRUPO], " +
                "PECASCLASSIFICA2.CODIGO AS [ITEMSUBGRUPO],PECAS.MATPRIMAPO ,nfvenda.id ",
                query, ordem);

                Rel.DefaultView.Sort = "CLIENTE, CODIGO, NUMERONFV";
                Rel = Rel.DefaultView.ToTable();
                
                //lista Simples
                if (rbnEspModelo1.Checked == true)
                {
                    if (rbnEspAnalitica.Checked == true)
                    {
                        if (rbnEspSemTotal.Checked == true)
                        { // simples
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                 "DADOS_NFVENDA_ESP_PO_X_ECOAT_ANA_SEMSUB.RPT",
                                 Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                  "DADOS_NFVENDA_ESP_PO_X_ECOAT_ANA_COMSUB.RPT",
                                  Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                        }
                    }
                    else
                    {
                        // Sintetica ou Analitica é a mesma                       
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios,
                             "DADOS_NFVENDA_ESP_PO_X_ECOAT_SIN_COMSUB.RPT",
                             Rel, pfields, "", clsInfo.conexaosqldados);

                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                    }
                }

                //lista faturamento mensal
                if (rbnEspModelo2.Checked == true)
                {
                    frmCrystalReport frmCrystalReport = new frmCrystalReport();
                    frmCrystalReport.Init(clsInfo.caminhorelatorios,
                                  "DADOS_NFVENDA_ESP_PO_X_ECOAT_MENSAL.RPT",
                                  Rel, pfields, "", clsInfo.conexaosqldados);

                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        
                }
            }
        }

        private void tspEspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbnEspModelo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnEspModelo1.Checked == true)
            {
                gbxEspAnalSint.Visible = true;
                gbxIEspSub.Visible = true;
            }
            if (rbnEspModelo2.Checked == true)
            {
                gbxEspAnalSint.Visible = false;
                gbxIEspSub.Visible = false;
            }
        }

        private void rbnEspSintetica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnEspAnalitica.Checked == true)
            {
                gbxIEspSub.Enabled = true;
            }

            if (rbnEspSintetica.Checked==true)
            {
                rbnEspSemTotal.Checked = true;
                gbxIEspSub.Enabled = false;
            }            
        }

        private void rbnEspPintura_CheckedChanged(object sender, EventArgs e)
        {
            if(rbnEspPintura.Checked == true)
            {
                tbxEspPinturaDe.Enabled = true;
                tbxEspPinturaAte.Enabled = true;
            }

            if (rbnEspPinturaPoXEcoat.Checked == true)
            {
                tbxEspPinturaDe.Text = "";
                tbxEspPinturaAte.Text = "";
                tbxEspPinturaDe.Enabled= false;
                tbxEspPinturaAte.Enabled = false;
            }
        }

        private void btnItemVendedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemVendedorDe.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Vendedores", 0);
            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnItemVendedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnItemVendedorAte.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Vendedores", 0);
            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }
    }
}
