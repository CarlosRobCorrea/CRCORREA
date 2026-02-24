using CRCorreaBLL;
using CRCorreaFuncoes;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRelPagar : Form
    {
        ParameterFields pfields = new ParameterFields();

        String query;
        String query1;
        String ordem;
        String cabecalho;

        Int32  idNorClienteDe;
        Int32  idNorClienteAte;

        String tbxVencimento;
        String tbxDataBaixa;
        Decimal tbxValor;
        Decimal tbxValorDesconto;
        Decimal tbxValorJuros;
        Decimal tbxValorMulta;
        String atevencimento;
        Decimal tbxDiferenca;

        Int32 idSituacaoCobrancaCodDe;
        Int32 idSituacaoCobrancaCodAte;

        DateTime daData;
        DateTime ateData;

        DataTable dtCtasPagas;
        DataTable dtTabela;

        String fornecedor = "";
        Decimal valor01;
        Decimal valor02;
        Decimal valor03;
        Decimal valor04;
        Decimal valor05;
        Decimal valor06;
        Decimal total;

        DateTime dataaux;
        DateTime Mes01Data;
        DateTime Mes02Data;
        DateTime Mes03Data;
        DateTime Mes04Data;
        DateTime Mes05Data;
        DateTime Mes06Data;

        String Mes01Nom;
        String Mes02Nom;
        String Mes03Nom;
        String Mes04Nom;
        String Mes05Nom;
        String Mes06Nom;

        Int32 Mes01Num;
        Int32 Mes02Num;
        Int32 Mes03Num;
        Int32 Mes04Num;
        Int32 Mes05Num;
        Int32 Mes06Num;

        Int32 idBancoInt;
        String Pendencias = "S";

        public frmRelPagar()
        {
            InitializeComponent();
        }

        public void Init()
        {
            // tela normal   
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxNorClienteDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxNorClienteAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxPagNorClienteDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxPagNorClienteAte);


            daData = Convert.ToDateTime("01/" + DateTime.Now.ToString("MM/yyyy") + " 00:00");
            ateData = daData.AddDays(-1);
            daData = daData.AddMonths(-6);


            tbxPagResDtPagtoDe.Text = daData.ToString("dd/MM/yyyy");
            tbxPagResDtPagtoAte.Text = ateData.ToString("dd/MM/yyyy");


        }

        private void frmRelPagar_Load(object sender, EventArgs e)
        {
            lbxNorOrdem.SelectedIndex = 0;
            lbxPagNorOrdem.SelectedIndex = 0;

            tbxSituacaoCobrancaCodDe.Text = "00";
            tbxSituacaoCobrancaCodAte.Text = "00";

            // Calcular os Valores com o Juros + Multas etc..
            //calcularprevisao();   // não entendi porque esta aqui 01/06/2014 Carlos

            idBancoInt = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + 999, "0"));
        }

        private void frmRelPagar_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
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

        private void ControlKeyDownDataHora(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownDataHora((TextBox)sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void calcularprevisao()
        {
            // Colocamos numa função financeiro
            // Objetivo é resumir o total com juros e multa para mostrar no fluxo e na tela
            // Mostrar no Grid sem entrar no registro

            ////Decimal tbxValorLiquido;
            ////Decimal tbxMulta;
            ////Decimal tbxJuros;
            ////Decimal tbxDesconto;
            ////String dataAtual = DateTime.Now.ToString("dd/MM/yyyy");

            ////// a data atual saiu dos rbn
            ////DataTable dtPagar = pagarBLL.GridDadosTodosCampos(0, dataAtual, "PREV", "A");

            ////foreach (DataRow row in dtPagar.Rows)
            ////{
            ////    if (row["DUPLICATA"].ToString() == "5616")
            ////    {
            ////        // MessageBox.Show("pare");
            ////    }
            ////    tbxVencimento = row["VENCIMENTO"].ToString();
            ////    tbxDataBaixa = dataAtual;
            ////    tbxValor = clsParser.DecimalParse(row["VALOR"].ToString());
            ////    tbxValorDesconto = clsParser.DecimalParse(row["VALORDESCONTO"].ToString());
            ////    tbxValorJuros = clsParser.DecimalParse(row["VALORJUROS"].ToString());
            ////    tbxValorMulta = clsParser.DecimalParse(row["VALORMULTA"].ToString());
            ////    atevencimento = row["ATEVENCIMENTO"].ToString();

            ////    DuplicataInfo DuplicataInfo;
            ////    DuplicataInfo = clsFinanceiro.CalcularDuplicata("Pagar", tbxVencimento, tbxDataBaixa,
            ////                                                            tbxValor, +
            ////                                                            tbxValorDesconto, +
            ////                                                            tbxValorJuros, +
            ////                                                            tbxValorMulta, +
            ////                                                            0, atevencimento);


            ////    tbxValorLiquido = DuplicataInfo.valorliquido; //.ToString("N2");
            ////    tbxMulta = DuplicataInfo.multas; //.ToString("N2");
            ////    tbxJuros = DuplicataInfo.juros; //.ToString("N2");
            ////    tbxDesconto = DuplicataInfo.descontos; //.ToString("N2");
            ////    //tbxSaldo = ((tbxValor + tbxMulta + tbxJuros) - tbxDesconto); //.ToString("N2");
            ////    //totaldias = DuplicataInfo.ntotaldias;
            ////    //tbxDiferenca = (tbxSaldo - (clsParser.DecimalParse(row["VALORPAGO"].ToString()) + clsParser.DecimalParse(row["VALORDEVOLVIDO"].ToString()))); //.ToString("N2");
            ////    tbxDiferenca = (tbxMulta + tbxJuros);
            ////    // Gravar no pagar a diferença no campo valorbaixando
            ////    // ele será somado quando faz o calculo no grid
            ////    if (tbxDiferenca > 0)
            ////    {
            ////        // Gravar no pagar
            ////        SqlConnection scn;
            ////        SqlCommand scd;
            ////        scn = new SqlConnection(clsInfo.conexaosqldados);
            ////        scd = new SqlCommand("UPDATE PAGAR SET " +
            ////                                "VALORBAIXANDO=@DIFERENCA " +
            ////                                "WHERE ID = @ID", scn);
            ////        scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());
            ////        scd.Parameters.AddWithValue("@DIFERENCA", SqlDbType.Decimal).Value = tbxDiferenca;
            ////        scn.Open();
            ////        scd.ExecuteNonQuery();
            ////        scn.Close();
            ////    }
            ////}
        }

        private void tspNorImprimir_Click(object sender, EventArgs e)
        {
            //
            cabecalho = "";
            query = "";
            ordem = "";

            // Cabeçalho  -- objetivo da lista
            if (rbnPagarSimples.Checked == true)
            {
                cabecalho = "Relatorio Ctas a Pagar ";
            }

            if (rbnNorDtVencimento.Checked == true)
            {
                cabecalho = cabecalho + " [ Data de Vencimento ]";
            }
            else
            {
                cabecalho = cabecalho + " [ Data de Previsão ]";
            }

            // filtrar pelo cognome
            if (tbxNorClienteDe.Text.Length > 0 && tbxNorClienteAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                cabecalho = cabecalho + Environment.NewLine + " Do Fornecedor : " + tbxNorClienteDe.Text.Trim();
            }
            if (tbxNorClienteDe.Text.Length == 0 && tbxNorClienteAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                cabecalho = cabecalho + Environment.NewLine + " abaixo do : " + tbxNorClienteAte.Text.Trim();
            }
            if (tbxNorClienteDe.Text.Length > 0 && tbxNorClienteAte.Text.Length > 0)
            {
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxNorClienteDe.Text + "  até a : " + tbxNorClienteAte.Text;
            }

            //Data Emissao
            if (clsParser.SqlDateTimeParse(tbxNorDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtEmissaoAte.Text).IsNull == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxNorDtEmissaoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxNorDtEmissaoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxNorDtEmissaoAte.Text).IsNull == false)
            {
                cabecalho = cabecalho + Environment.NewLine + "Até a data de Emissão: " + tbxNorDtEmissaoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxNorDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtEmissaoAte.Text).IsNull == false)
            {
                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxNorDtEmissaoDe.Text + "  Até a data de Emissão: " + tbxNorDtEmissaoAte.Text;
            }
            //Data Vencimento Previsto
            if (rbnNorDtVencimento.Checked == true)
            {
                if (clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtVencimentoAte.Text).IsNull == true)
                {
                    cabecalho = cabecalho + Environment.NewLine + "Da data Venc: " + tbxNorDtVencimentoDe.Text;
                }
                if (clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxNorDtVencimentoAte.Text).IsNull == false)
                {
                    cabecalho = cabecalho + Environment.NewLine + "Até a data Venc: " + tbxNorDtVencimentoAte.Text;
                }
                if (clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == false)
                {
                    cabecalho = cabecalho + Environment.NewLine + "Da data Venc: " + tbxNorDtVencimentoDe.Text + "  Até a data Venc: " + tbxNorDtVencimentoAte.Text;
                }
            }
            else
            {
                if (clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtVencimentoAte.Text).IsNull == true)
                {
                    cabecalho = cabecalho + Environment.NewLine + "Da data Venc Prev : " + tbxNorDtVencimentoDe.Text;
                }
                if (clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxNorDtVencimentoAte.Text).IsNull == false)
                {
                    cabecalho = cabecalho + Environment.NewLine + "Até a data Venc Prev : " + tbxNorDtVencimentoAte.Text;
                }
                if (clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == false)
                {
                     cabecalho = cabecalho + Environment.NewLine + "Da data Venc Prev : " + tbxNorDtVencimentoDe.Text + "  Até a data Venc Prev : " + tbxNorDtVencimentoAte.Text;
                }
            }
            /////////////////////////////////////////////////////////////////////////////////////////
            // ESPECIAIS DE CADA RELATORIO / MODELO PARA A QUERY
            if (cbxNorChequePre.Checked == true)
            { // incluir os cheques pre datados
               cabecalho = cabecalho + Environment.NewLine + "Com cheques pré datados";
            }
            // Cabecalho (lista Analitica ou Sintetica)
            if (rbnNorAnalitica.Checked == true)
            {
                cabecalho = cabecalho +  " Tipo relatorio Analitica ";
            }
            else
            {
                cabecalho = cabecalho + " Tipo relatorio Sintetica ";
            }
            //  
            if (rbnNorSemTotal.Checked == true)
            {
                cabecalho = cabecalho  + " Sem Sub-Total ";
            }
            else
            {
                cabecalho = cabecalho + " Com Sub-Total ";
            }
            if (Pendencias == "S")
            {
                cabecalho = cabecalho + " Sem as Pendecias ";
            }

            // Ordem da Lista williams
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxNorOrdem.Text;

            // QUERY
            DataTable Rel = new DataTable();
 
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
 
            ParameterFields parameters = new ParameterFields();
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();

            field.Name = "EMPRESA";
            valor.Value = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome FROM EMPRESA where id=" + clsInfo.zempresaid + " ");
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cabeçalho
            field = new ParameterField();
            field.Name = "CABECALHO";
            valor = new ParameterDiscreteValue();
            valor.Value = cabecalho;
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            // Cliente de
            field = new ParameterField();
            field.Name = "ClienteDe";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxNorClienteDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cliente ate
            field = new ParameterField();
            field.Name = "ClienteAte";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxNorClienteAte.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Da Data emissao
            field = new ParameterField();
            field.Name = "DataEmissaoDe";
            valor = new ParameterDiscreteValue();
            if (tbxNorDtEmissaoDe.Text.Trim() == "")
            {
                tbxNorDtEmissaoDe.Text = "01/01/1900";
            }
            valor.Value = tbxNorDtEmissaoDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Ate Data emissao
            field = new ParameterField();
            field.Name = "DataEmissaoAte";
            valor = new ParameterDiscreteValue();
            if (tbxNorDtEmissaoAte.Text.Trim() == "")
            {
                tbxNorDtEmissaoAte.Text = "01/01/2100";
            }
            valor.Value = tbxNorDtEmissaoAte.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Da Data Vencimento
            field = new ParameterField();
            field.Name = "DataVencimentoDe";
            valor = new ParameterDiscreteValue();
            if (tbxNorDtVencimentoDe.Text.Trim() == "")
            {
                tbxNorDtVencimentoDe.Text = "01/01/1900";
            }
            valor.Value = tbxNorDtVencimentoDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Ate Data Vencimento
            field = new ParameterField();
            field.Name = "DataVencimentoAte";
            valor = new ParameterDiscreteValue();
            if (tbxNorDtVencimentoAte.Text.Trim() == "")
            {
                tbxNorDtVencimentoAte.Text = "01/01/2100";
            }
            valor.Value = tbxNorDtVencimentoAte.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Pendencias  idbanco =0 com pendencias/tudo  id banco 99 - sem as pendencias
            field = new ParameterField();
            field.Name = "PendenciaBanco";
            valor = new ParameterDiscreteValue();
            Int32 idBanco = 0;
            if (Pendencias == "S")
            {
                idBanco = idBancoInt;
            }
            valor.Value = idBanco;
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            if (rbnNorDtVencimento.Checked == true)
            { // Por Data de Vencimento e Previsao
                if (rbnNorAnalitica.Checked == true)  //Analitica
                {
                    if (rbnNorSemTotal.Checked == true)
                    {
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "PAGAR_VENCIMENTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {  
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "PAGAR_VENCIMENTO_SUBTOTAL.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                }
                else
                {  // Sintetica
                    frmCrystalReport.Init(clsInfo.caminhorelatorios, "PAGAR_VENCIMENTO_SINTETICA_DATA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                }
            }
        }

        private void tspNorRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnNorFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvNorFornecedorDe";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idNorClienteDe);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnNorFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvNorFornecedorAte";
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idNorClienteAte);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void rbnPagarSimples_Click(object sender, EventArgs e)
        {
            tclRelPagar.SelectedTab = tabPagar1;
            gbxPagarNormal.Visible = true;
            gbxPagasNormal.Visible = false;
            //if (rbnNorDtVencimento.Checked == true)
            //{
            //    rbnNorDtVencimento.Select();
            //}
            //else
            //{
            //    rbnNorDtPrevisao.Select();
            //}

        }

        private void rbnPagasSimples_Click(object sender, EventArgs e)
        {
            tclRelPagar.SelectedTab = tabPagas1;
            gbxPagarNormal.Visible = false;
            gbxPagasNormal.Visible = true;
            if (rbnPagNorDtVencimento.Checked == true)
            {
                rbnPagNorDtVencimento.Select();
            }
            else
            {
                rbnPagNorDtPaga.Select();
            }

        }

        private void tspPagNorImprimir_Click(object sender, EventArgs e)
        {
            //
            cabecalho = "";
            query = "";
            ordem = "";

            // Cabeçalho  -- objetivo da lista
            if (rbnPagasSimples.Checked == true)
            {
                cabecalho = "Relatorio Ctas Pagas " + Environment.NewLine;
            }

            if (rbnPagNorDtVencimento.Checked == true)
            {
                cabecalho = cabecalho + " [ Data de Vencimento ]";
            }
            else
            {
                cabecalho = cabecalho + " [ Data de Pagamento ]";
            }

            // filtrar pelo cognome
            if (tbxPagNorClienteDe.Text.Length > 0 && tbxPagNorClienteAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                query = "CLIENTE.COGNOME >= '" + tbxPagNorClienteDe.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " Do Fornecedor : " + tbxPagNorClienteDe.Text.Trim();
            }                                                                                                                                                                                                                                                  
            if (tbxPagNorClienteDe.Text.Length == 0 && tbxPagNorClienteAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                query = "CLIENTE.COGNOME <= '" + tbxPagNorClienteAte.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo do : " + tbxPagNorClienteAte.Text.Trim();
            }
            if (tbxPagNorClienteDe.Text.Length > 0 && tbxPagNorClienteAte.Text.Length > 0)
            {
                query = "CLIENTE.COGNOME >= '" + tbxPagNorClienteDe.Text + "' AND CLIENTE.COGNOME <= '" + tbxPagNorClienteAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxPagNorClienteDe.Text + "  até a : " + tbxPagNorClienteAte.Text;
            }

            //Data Emissao
            if (clsParser.SqlDateTimeParse(tbxPagNorDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxPagNorDtEmissaoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS.EMISSAO >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtEmissaoDe.Text + " 00:00", true) +
                "AND PAGAS.EMISSAO <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxPagNorDtEmissaoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxPagNorDtEmissaoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxPagNorDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS.EMISSAO >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtEmissaoDe.Text + " 00:00", true) +
                "AND PAGAS.EMISSAO <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Emissão: " + tbxPagNorDtEmissaoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxPagNorDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxPagNorDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS.EMISSAO >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtEmissaoDe.Text + " 00:00", true) +
                "AND PAGAS.EMISSAO <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxPagNorDtEmissaoDe.Text + "  Até a data de Emissão: " + tbxPagNorDtEmissaoAte.Text;
            }
            //Data Vencimento
            if (clsParser.SqlDateTimeParse(tbxPagNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxPagNorDtVencimentoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtVencimentoDe.Text + " 00:00", true) +
                "AND PAGAS.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtVencimentoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento : " + tbxPagNorDtVencimentoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxPagNorDtVencimentoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxPagNorDtVencimentoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtVencimentoDe.Text + " 00:00", true) +
                "AND PAGAS.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtVencimentoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Vencimento : " + tbxPagNorDtVencimentoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxPagNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxPagNorDtVencimentoDe.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtVencimentoDe.Text + " 00:00", true) +
                "AND PAGAS.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtVencimentoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento : " + tbxPagNorDtVencimentoDe.Text + "  Até a data de Recebimento: " + tbxNorDtVencimentoAte.Text;
            }
            // Situação da Baixa
            if (tbxSituacaoCobrancaCodDe.Text.Length > 0 && tbxSituacaoCobrancaCodAte.Text.Length == 0)
            { 
                // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query += " SITUACAOCOBRANCACOD.CODIGO >= '" + tbxSituacaoCobrancaCodDe.Text.Trim() + "' ";
                cabecalho = cabecalho + Environment.NewLine + " Do Cod Bx Cobrança : " + tbxSituacaoCobrancaCodAte.Text.Trim();
            }
            if (tbxSituacaoCobrancaCodDe.Text.Length == 0 && tbxSituacaoCobrancaCodAte.Text.Length > 0)
            {
                // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query += " SITUACAOCOBRANCACOD.CODIGO <= '" + tbxSituacaoCobrancaCodAte.Text.Trim() + "' ";
                cabecalho = cabecalho + Environment.NewLine + " abaixo do : " + tbxSituacaoCobrancaCodAte.Text.Trim();
            }
            if (tbxSituacaoCobrancaCodDe.Text.Length > 0 && tbxSituacaoCobrancaCodAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query += "SITUACAOCOBRANCACOD.CODIGO >= '" + tbxSituacaoCobrancaCodDe.Text + "' AND SITUACAOCOBRANCACOD.CODIGO <= '" + tbxSituacaoCobrancaCodAte.Text + "' ";
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxSituacaoCobrancaCodDe.Text + "  até a : " + tbxSituacaoCobrancaCodAte.Text;
            }

            //Data Pagamento
            if (clsParser.SqlDateTimeParse(tbxPagNorDtPagtoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxPagNorDtPagtoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS01.DATAOK >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtPagtoDe.Text + " 00:00", true) +
                "AND PAGAS01.DATAOK <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtPagtoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Pagamento : " + tbxPagNorDtPagtoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxPagNorDtPagtoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxPagNorDtPagtoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS01.DATAOK >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtPagtoDe.Text + " 00:00", true) +
                "AND PAGAS01.DATAOK <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtPagtoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Pagamento : " + tbxNorDtVencimentoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxPagNorDtPagtoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxPagNorDtPagtoDe.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS01.DATAOK >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtPagtoDe.Text + " 00:00", true) +
                "AND PAGAS01.DATAOK <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtPagtoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Pagamento : " + tbxPagNorDtPagtoDe.Text + "  Até a data de Recebimento: " + tbxPagNorDtPagtoAte.Text;
            }
            //Data Baixa
            if (clsParser.SqlDateTimeParse(tbxPagNorDtBaixaDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxPagNorDtBaixaAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS01.DATAENVIO >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtBaixaDe.Text + " 00:00", true) +
                "AND PAGAS01.DATAENVIO <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtBaixaAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data da Baixa : " + tbxPagNorDtBaixaDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxPagNorDtBaixaDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxPagNorDtBaixaAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS01.DATAENVIO >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtBaixaDe.Text + " 00:00", true) +
                "AND PAGAS01.DATAENVIO <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtBaixaAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data da Baixa : " + tbxNorDtVencimentoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxPagNorDtBaixaDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxPagNorDtBaixaDe.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "PAGAS01.DATAENVIO >= " + clsParser.SqlDateTimeFormat(tbxPagNorDtBaixaDe.Text + " 00:00", true) +
                "AND PAGAS01.DATAENVIO <= " + clsParser.SqlDateTimeFormat(tbxPagNorDtBaixaAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data da Baixa : " + tbxPagNorDtBaixaDe.Text + "  Até a data da Baixa: " + tbxPagNorDtBaixaAte.Text;
            }
            if (query.ToString().Length > 0)
            {
                query = query + " AND ";
            }
            query = query + "pagas.filial= " + clsInfo.zfilial;



            /////////////////////////////////////////////////////////////////////////////////////////
            // ESPECIAIS DE CADA RELATORIO / MODELO PARA A QUERY
            /////////////////////////////////////////////////////////////////////////////////////////

            //Ordem
            ordem = "PAGAS01.DATAOK, CLIENTE.COGNOME, PAGAS.DUPLICATA";
            if (lbxPagNorOrdem.SelectedIndex == 0)
            {
                ordem = "PAGAS01.DATAOK, CLIENTE.COGNOME, PAGAS.DUPLICATA";
            }
            if (lbxPagNorOrdem.SelectedIndex == 1)
            {
                ordem = "PAGAS.VENCIMENTO, CLIENTE.COGNOME, PAGAS.DUPLICATA";
            }
            if (lbxPagNorOrdem.SelectedIndex == 2)
            {
                ordem = "PAGAS01.DATAENVIO, CLIENTE.COGNOME, PAGAS.DUPLICATA";
            }
            if (lbxPagNorOrdem.SelectedIndex == 3)
            {
                ordem = "CLIENTE.COGNOME, PAGAS.DUPLICATA";
            }
            if (lbxPagNorOrdem.SelectedIndex == 4)
            {
                ordem = "PAGAS.DUPLICATA, CLIENTE.COGNOME, PAGAS.VENCIMENTO";
            }
            if (lbxPagNorOrdem.SelectedIndex == 5)
            {
                ordem = "HISTORICOS.CODIGO, PAGAS01.DATAOK";
            }


            // Cabecalho (lista Analitica ou Sintetica)
            if (rbnPagNorAnalitica.Checked == true)
            {
                cabecalho = cabecalho + " Tipo relatorio Analitica ";
            }
            else
            {
                cabecalho = cabecalho + " Tipo relatorio Sintetica ";
            }
            //  
            if (rbnPagNorSemTotal.Checked == true)
            {
                cabecalho = cabecalho + " Sem Sub-Total ";
            }
            else
            {
                cabecalho = cabecalho + " Com Sub-Total ";
            }
            // Ordem da Lista williams
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxPagNorOrdem.Text;
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

            query1 = "select PAGAS.ID AS [IDPAGAS], PAGAS.FILIAL, PAGAS.DUPLICATA, PAGAS.POSICAO, PAGAS.POSICAOFIM, PAGAS.EMISSAO, PAGAS.IDDOCUMENTO, DOCFISCAL.COGNOME AS [DOCUMENTO], PAGAS.SETOR " +
             ", PAGAS.IDFORNECEDOR,  CLIENTE.COGNOME AS [FORNECEDOR] " +
             ", PAGAS.DATALANCA, PAGAS.EMITENTE, PAGAS.IDHISTORICO , HISTORICOS.CODIGO AS [HISTORICO], HISTORICOS.NOME AS [HISTORICONOM] " +
             ", PAGAS.IDCENTROCUSTO , CENTROCUSTOS.CODIGO AS [CENTROCUSTO], CENTROCUSTOS.NOME AS [CENTROCUSTONOM] " +
             ", PAGAS.IDCODIGOCTABIL , CONTACONTABIL.CODIGO AS [CTACONTABIL], CONTACONTABIL.NOME AS [CTACONTABILNOM] " +
             ", PAGAS.IDNOTAFISCAL , NFCOMPRA.NUMERO AS [NOTAFISCAL],  PAGAS.IDPAGARNFE, PAGAS.OBSERVA " +
             ", PAGAS.IDFORMAPAGTO , SITUACAOTIPOTITULO.CODIGO AS [FORMAPAGTO], SITUACAOTIPOTITULO.NOME AS [FORMAPAGTONOM] " +
             ", PAGAS.IDBANCO , TAB_BANCOS.CODIGO AS [BANCO], TAB_BANCOS.COGNOME AS [BANCONOM] " +
             ", PAGAS.IDBANCOINT , BANCOS.CONTA, BANCOS.NOME, PAGAS.CHEGOU , PAGAS.DESPESAPUBLICA ,PAGAS.BOLETO ,PAGAS.BOLETONRO " +
             ", PAGAS.DV ,PAGAS.BAIXA ,PAGAS.VENCIMENTO ,PAGAS.VALOR ,PAGAS.VALORDESCONTO ,PAGAS.ATEVENCIMENTO, PAGAS.VALORLIQUIDO " +
             ", PAGAS.VALORJUROS ,PAGAS.VALORMULTA " +
             ",(PAGAS.VALORLIQUIDO + clsPaGAS.VALORBAIXANDO) - (PAGAS.VALORPAGO + clsPaGAS.VALORDEVOLVIDO) AS [VALORPAGAR] " +
             ", PAGAS.IDSITBANCO , SITUACAOTITULO.CODIGO AS [SITUACAOCODIGO], SITUACAOTITULO.NOME  AS [SITUACAONOME] " +
             ", PAGAS.VALORPAGO ,PAGAS.VALORBAIXANDO, PAGAS.VALORDEVOLVIDO " +
             /*", PAGAS.IDVENDEDOR ,"*/ ", CLIENTEVEND.COGNOME AS [VENDEDOR] " /* PAGAS.VALORCOMISSAO*/  +
             ", PAGAS01.DATAENVIO ,PAGAS01.DATAOK ,PAGAS01.IDCOBRANCACOD, SITUACAOCOBRANCACOD.CODIGO AS [SITUACAOCODCOBRANCA] , SITUACAOCOBRANCACOD.NOME AS [SITUACAOCODCOBRANCANOME] " +
             ", PAGAS01.IDCOBRANCAHIS , SITUACAOCOBRANCACOD1.CODIGO AS [SITUACAOCODCOBRANCAHIS] , SITUACAOCOBRANCACOD1.NOME AS [SITUACAOCODCOBRANCAHISNOME] " +
             ", PAGAS01.VALOR AS [VALORPAGAS01], PAGAS01.DEBCRED, PAGAS01.MOTIVO " +
             ", PAGAS01.IDHISTORICO , HISTORICOS01.CODIGO AS [HISTORICO01], HISTORICOS01.NOME AS [HISTORICO01NOM] " +
             ", PAGAS01.IDCENTROCUSTO , CENTROCUSTOS01.CODIGO AS [CENTROCUSTO01], CENTROCUSTOS01.NOME AS [CENTROCUSTO01NOM] " +
             ", PAGAS01.IDCODIGOCTABIL , CONTACONTABIL.CODIGO AS [CTACONTABIL01], CONTACONTABIL.NOME AS [CTACONTABIL01NOM] " +
             ", PAGAS01.BAIXOUCOMO " +
             ", PAGAS01.VALORCOMISSAO AS [VALORCOMISSAOPAGAS01] " +
             "from PAGAS " +
             "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAS.IDDOCUMENTO " +
             "LEFT JOIN CLIENTE ON CLIENTE.ID = PAGAS.IDFORNECEDOR " +
             "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS ON HISTORICOS.ID = PAGAS.IDHISTORICO " +
             "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS ON CENTROCUSTOS.ID = PAGAS.IDCENTROCUSTO " +
             "LEFT JOIN CONTACONTABIL ON CONTACONTABIL.ID = PAGAS.IDCODIGOCTABIL " +
             "LEFT JOIN NFCOMPRA ON NFCOMPRA.ID = PAGAS.IDNOTAFISCAL " +
             "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAS.IDFORMAPAGTO " +
             "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID = PAGAS.IDBANCO " +
             "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].BANCOS ON BANCOS.ID = PAGAS.IDBANCOINT " +
             "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID = PAGAS.IDSITBANCO " +
             "LEFT JOIN CLIENTE CLIENTEVEND ON CLIENTEVEND.ID = 1 " +
             "LEFT JOIN PAGAS01 PAGAS01 ON PAGAS01.IDDUPLICATA = PAGAS.ID " +
             "LEFT JOIN SITUACAOCOBRANCACOD ON SITUACAOCOBRANCACOD.ID = PAGAS01.IDCOBRANCACOD " +
             "LEFT JOIN SITUACAOCOBRANCACOD1 ON SITUACAOCOBRANCACOD1.ID = PAGAS01.IDCOBRANCAHIS " +
             "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS HISTORICOS01 ON HISTORICOS01.ID = PAGAS01.IDHISTORICO " +
             "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS CENTROCUSTOS01 ON CENTROCUSTOS01.ID = PAGAS01.IDCENTROCUSTO " +
             "LEFT JOIN CONTACONTABIL CONTACONTABIL01 ON CONTACONTABIL01.ID = PAGAS01.IDCODIGOCTABIL ";

            if (query.Length > 0)
            {
                query1 = query1 + " where " + query;
            }                
            query1 = query1 + " ORDER BY " + ordem;
            SqlDataAdapter sda = new SqlDataAdapter(query1, clsInfo.conexaosqldados);
            sda.Fill(Rel);


            if (rbnPagNorDtVencimento.Checked == true)
            {
                MessageBox.Show("Desenvolver");
            }
            else if (rbnPagNorDtPaga.Checked == true)
            { // Por Data de Vencimento
                if (rbnPagNorAnalitica.Checked == true)  //Analitica
                {
                    if (rbnPagNorSemTotal.Checked == true)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "PAGAS_DATAPAGO.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        MessageBox.Show("Desenvolver");
                    }
                }
                else
                {
                    MessageBox.Show("Desenvolver");
                }
            }
        }
        private void tspPagNorRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void rbnPagNorDtPaga_Click(object sender, EventArgs e)
        {
            rbnPagNorDtPaga.Checked = true;
            rbnPagNorDtVencimento.Checked = false;
        }

        private void rbnPagNorDtVencimento_Click(object sender, EventArgs e)
        {
            rbnPagNorDtVencimento.Checked = true;
            rbnPagNorDtPaga.Checked = false;
        }

        private void btnSituacaoCobrancaCodDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSituacaoCobrancaCodDe.Name;
            frmSituacaocobrancacodVis frmSituacaocobrancacodVis = new frmSituacaocobrancacodVis();
            frmSituacaocobrancacodVis.Init(clsInfo.conexaosqldados, clsInfo.conexaosqlbanco);
            clsFormHelper.AbrirForm(this.MdiParent, frmSituacaocobrancacodVis, clsInfo.conexaosqldados);

        }

        private void btnSituacaoCobrancaCodAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnSituacaoCobrancaCodAte.Name;
            frmSituacaocobrancacodVis frmSituacaocobrancacodVis = new frmSituacaocobrancacodVis();
            frmSituacaocobrancacodVis.Init(clsInfo.conexaosqldados, clsInfo.conexaosqlbanco);
            clsFormHelper.AbrirForm(this.MdiParent, frmSituacaocobrancacodVis, clsInfo.conexaosqldados);

        }

        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                 clsInfo.zrow.Index != -1 &&
                 clsInfo.znomegrid != null &&
                 clsInfo.znomegrid != "")
            {
                if (clsInfo.znomegrid == "dgvNorFornecedorDe")
                {
                    if (clsInfo.zrow != null)
                    {
                        idNorClienteDe = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxNorClienteDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID=" + idNorClienteDe, "").ToString();
                        //tbxNorClienteDe.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idNorClienteDe).ToString();
                    }
                    tbxNorClienteDe.Select();
                }
                if (clsInfo.znomegrid == "dgvNorFornecedorAte")
                {
                    if (clsInfo.zrow != null)
                    {
                        idNorClienteAte = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxNorClienteAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID=" + idNorClienteAte, "0").ToString();
                        //tbxNorClienteAte.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idNorClienteAte).ToString();
                    }
                    tbxNorClienteAte.Select();
                }

                //inicio buscas para o Aba Ctas Receber Normal
                //cliente de
                if (clsInfo.znomegrid == btnSituacaoCobrancaCodDe.Name)
                {
                    idSituacaoCobrancaCodDe = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxSituacaoCobrancaCodDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + idSituacaoCobrancaCodDe, "00").ToString();
                }
                if (clsInfo.znomegrid == btnSituacaoCobrancaCodAte.Name)
                {
                    idSituacaoCobrancaCodAte = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxSituacaoCobrancaCodAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOCOBRANCACOD where ID = " + idSituacaoCobrancaCodAte, "00").ToString();
                }

            }
            daData = clsParser.DateTimeParse(tbxPagResDtPagtoDe.Text);
            ateData = clsParser.DateTimeParse(tbxPagResDtPagtoAte.Text);
            TimeSpan dias = (ateData.Subtract(daData));
            if (dias.Days > 188)
            {
                MessageBox.Show("Não pode ser superior a 6 meses");
                tspPagResImprimir.Enabled = false;
            }
            else
            {
                tspPagResImprimir.Enabled = true;
            }

            if (ckxPendencias.Checked == true)
            {
                Pendencias = "S";
            }
            else
            {
                Pendencias = "N";
            }

        }

        private void tspPagResRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void tspPagResImprimir_Click(object sender, EventArgs e)
        {
            //
            cabecalho = "";
            query = "";
            ordem = "";

            // Cabeçalho  -- objetivo da lista
            cabecalho = "Relatorio Ctas Pagas " + Environment.NewLine;
            cabecalho = cabecalho + " [ Data de Pagamento ]";
            //Data Pagamento
            query = query + "and PAGAS01.DATAOK >= " + clsParser.SqlDateTimeFormat(tbxPagResDtPagtoDe.Text + " 00:00", true) +
            "AND PAGAS01.DATAOK <= " + clsParser.SqlDateTimeFormat(tbxPagResDtPagtoAte.Text + " 23:59", true);
            cabecalho = cabecalho + Environment.NewLine + "Da data de Pagamento : " + tbxPagResDtPagtoDe.Text + "  Até a data de Recebimento: " + tbxPagResDtPagtoAte.Text;
            query = query + " and pagas.filial= " + clsInfo.zfilial;

            //Ordem
            ordem = "CLIENTE.COGNOME, PAGAS01.DATAOK ";
            if (lbxPagNorOrdem.SelectedIndex == 0)
            {
                ordem = "CLIENTE.COGNOME, PAGAS01.DATAOK ";
            }
            //if (lbxPagNorOrdem.SelectedIndex == 1)
            //{
            //}
            // Ordem da Lista 
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxPagNorOrdem.Text;
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

            query1 = "select CLIENTE.COGNOME AS [FORNECEDOR], PAGAS01.DATAOK, PAGAS01.VALOR AS [VALORPAGAS01] " +
                    "FROM PAGAS01 " +
                    "INNER JOIN PAGAS ON PAGAS.ID=PAGAS01.IDDUPLICATA " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID = PAGAS.IDFORNECEDOR " +
                    "LEFT JOIN SITUACAOCOBRANCACOD ON SITUACAOCOBRANCACOD.ID = PAGAS01.IDCOBRANCACOD " +
                    "where SITUACAOCOBRANCACOD.CODIGO >= '00' AND SITUACAOCOBRANCACOD.CODIGO <= '01'  ";
            if (query.Length > 0)
            {
                query1 = query1 + " " + query;
            }
            query1 = query1 + " ORDER BY " + ordem;
            dtCtasPagas = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(query1, clsInfo.conexaosqldados);
            sda.Fill(dtCtasPagas);
            /// Preparar os Meses
            /// 

            String cMes = "JanFevMarAbrMaiJunJulAgoSetOutNovDez";
            dataaux = DateTime.Parse(tbxPagResDtPagtoDe.Text);
            int Y = 0;
            for (int i = 1; i <= 6; i++)
            {

                switch (i)
                {
                    case 1:
                        Mes01Data = dataaux;
                        Mes01Num = dataaux.Month;
                        Y = ((Mes01Num * 3) - 3);
                        Mes01Nom = cMes.Substring(Y, 3);
  //                      tbxMesAno.Text = cMes.Substring(Y, 3) + "/";
                        Mes01Nom += "/" + dataaux.Year;

                        break;
                    case 2:
                        Mes02Data = dataaux;
                        Mes02Num = dataaux.Month;
                        Y = ((Mes02Num * 3) - 3);
                        Mes02Nom = cMes.Substring(Y, 3);
                        Mes02Nom += "/" + dataaux.Year;
                        break;
                    case 3:
                        Mes03Data = dataaux;
                        Mes03Num = dataaux.Month;
                        Y = ((Mes03Num * 3) - 3);
                        Mes03Nom = cMes.Substring(Y, 3);
                        Mes03Nom += "/" + dataaux.Year;
                        break;
                    case 4:
                        Mes04Data = dataaux;
                        Mes04Num = dataaux.Month;
                        Y = ((Mes04Num * 3) - 3);
                        Mes04Nom = cMes.Substring(Y, 3);
                        Mes04Nom += "/" + dataaux.Year;
                        break;
                    case 5:
                        Mes05Data = dataaux;
                        Mes05Num = dataaux.Month;
                        Y = ((Mes05Num * 3) - 3);
                        Mes05Nom = cMes.Substring(Y, 3);
                        Mes05Nom += "/" + dataaux.Year;
                        break;
                    case 6:
                        Mes06Data = dataaux;
                        Mes06Num = dataaux.Month;
                        Y = ((Mes06Num * 3) - 3);
                        Mes06Nom = cMes.Substring(Y, 3);
                        Mes06Nom += "/" + dataaux.Year;
                        break;
                }

                dataaux = dataaux.AddMonths(1);
            }
            // Criar a Tabela para Armazenar os valores Mes a Mes
            dtTabela = new DataTable();
            dtTabela.Columns.Add("Mes01Nom", Type.GetType("System.String"));
            dtTabela.Columns.Add("Mes02Nom", Type.GetType("System.String"));
            dtTabela.Columns.Add("Mes03Nom", Type.GetType("System.String"));
            dtTabela.Columns.Add("Mes04Nom", Type.GetType("System.String"));
            dtTabela.Columns.Add("Mes05Nom", Type.GetType("System.String"));
            dtTabela.Columns.Add("Mes06Nom", Type.GetType("System.String"));
            dtTabela.Columns.Add("Fornecedor", Type.GetType("System.String"));
            dtTabela.Columns.Add("ValorMes01", Type.GetType("System.Decimal"));
            dtTabela.Columns.Add("ValorMes02", Type.GetType("System.Decimal"));
            dtTabela.Columns.Add("ValorMes03", Type.GetType("System.Decimal"));
            dtTabela.Columns.Add("ValorMes04", Type.GetType("System.Decimal"));
            dtTabela.Columns.Add("ValorMes05", Type.GetType("System.Decimal"));
            dtTabela.Columns.Add("ValorMes06", Type.GetType("System.Decimal"));
            dtTabela.Columns.Add("Total", Type.GetType("System.Decimal"));
            dtTabela.Columns.Add("Participa", Type.GetType("System.Decimal"));

            fornecedor = "__Resol";
            foreach (DataRow row in dtCtasPagas.Rows)
            { // fazer o Loop no Contas Pagas para Acumular o Mes a Mes por Fornecedor

                if (fornecedor != row["FORNECEDOR"].ToString())
                {
                    // Gravar Total
                    DataRow row1 = dtTabela.NewRow();
                    row1["Mes01Nom"] = Mes01Nom;
                    row1["Mes02Nom"] = Mes02Nom;
                    row1["Mes03Nom"] = Mes03Nom;
                    row1["Mes04Nom"] = Mes04Nom;
                    row1["Mes05Nom"] = Mes05Nom;
                    row1["Mes06Nom"] = Mes06Nom;
                    row1["Fornecedor"] = fornecedor;
                    row1["ValorMes01"] = valor01;
                    row1["ValorMes02"] = valor02;
                    row1["ValorMes03"] = valor03;
                    row1["ValorMes04"] = valor04;
                    row1["ValorMes05"] = valor05;
                    row1["ValorMes06"] = valor06;
                    row1["Total"] = valor01 + valor02 + valor03 + valor04 + valor05 + valor06;
                    row1["Participa"] = 0;
                    dtTabela.Rows.Add(row1);
                    // Abrir um Novo
                    fornecedor = row["FORNECEDOR"].ToString();
                    valor01 = 0;
                    valor02 = 0;
                    valor03 = 0;
                    valor04 = 0;
                    valor05 = 0;
                    valor06 = 0;
                    total = 0;

                }
                if (clsParser.DateTimeParse(row["DATAOK"].ToString()).Year == Mes01Data.Year && clsParser.DateTimeParse(row["DATAOK"].ToString()).Month == Mes01Data.Month)
                {
                    valor01 += clsParser.DecimalParse(row["VALORPAGAS01"].ToString());
                }
                if (clsParser.DateTimeParse(row["DATAOK"].ToString()).Year == Mes02Data.Year && clsParser.DateTimeParse(row["DATAOK"].ToString()).Month == Mes02Data.Month)
                {
                    valor02 += clsParser.DecimalParse(row["VALORPAGAS01"].ToString());
                }
                if (clsParser.DateTimeParse(row["DATAOK"].ToString()).Year == Mes03Data.Year && clsParser.DateTimeParse(row["DATAOK"].ToString()).Month == Mes03Data.Month)
                {
                    valor03 += clsParser.DecimalParse(row["VALORPAGAS01"].ToString());
                }
                if (clsParser.DateTimeParse(row["DATAOK"].ToString()).Year == Mes04Data.Year && clsParser.DateTimeParse(row["DATAOK"].ToString()).Month == Mes04Data.Month)
                {
                    valor04 += clsParser.DecimalParse(row["VALORPAGAS01"].ToString());
                }
                if (clsParser.DateTimeParse(row["DATAOK"].ToString()).Year == Mes05Data.Year && clsParser.DateTimeParse(row["DATAOK"].ToString()).Month == Mes05Data.Month)
                {
                    valor05 += clsParser.DecimalParse(row["VALORPAGAS01"].ToString());
                }
                if (clsParser.DateTimeParse(row["DATAOK"].ToString()).Year == Mes06Data.Year && clsParser.DateTimeParse(row["DATAOK"].ToString()).Month == Mes06Data.Month)
                {
                    valor06 += clsParser.DecimalParse(row["VALORPAGAS01"].ToString());
                }
                total = valor01 + valor02 + valor03 + valor04 + valor05 + valor06;

            }
            // Gravar o Ultimo fornecedor acumulado no loop
            DataRow row2 = dtTabela.NewRow();
            row2["Mes01Nom"] = Mes01Nom;
            row2["Mes02Nom"] = Mes02Nom;
            row2["Mes03Nom"] = Mes03Nom;
            row2["Mes04Nom"] = Mes04Nom;
            row2["Mes05Nom"] = Mes05Nom;
            row2["Mes06Nom"] = Mes06Nom;
            row2["Fornecedor"] = fornecedor;
            row2["ValorMes01"] = valor01;
            row2["ValorMes02"] = valor02;
            row2["ValorMes03"] = valor03;
            row2["ValorMes04"] = valor04;
            row2["ValorMes05"] = valor05;
            row2["ValorMes06"] = valor06;
            row2["Total"] = total;
            row2["Participa"] = 0;
            dtTabela.Rows.Add(row2);

            dtTabela.Rows[0].Delete();

            Rel = dtTabela;

            frmCrystalReport frmCrystalReport = new frmCrystalReport();
            frmCrystalReport.Init(clsInfo.caminhorelatorios, "PAGAS_FORNECEDOR.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void rbnPagasResumo_Click(object sender, EventArgs e)
        {
            tclRelPagar.SelectedTab = tabPagas2;
            gbxPagasResumo.Visible = false;
            gbxPagasResumo.Visible = true;
            tbxPagResDtPagtoDe.Select();

        }

        private void rbnPagasSimples_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbnPagasResumo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbnNorDtVencimento_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
