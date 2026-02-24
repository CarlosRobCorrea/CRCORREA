using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRelReceber : Form
    {
        clsRptFluxoFinanceiroInfo clsRptFluxoFinanceiroInfo = new clsRptFluxoFinanceiroInfo();
        clsRptFluxoFinanceiroBLL clsRptFluxoFinanceiroBLL = new clsRptFluxoFinanceiroBLL();

        ParameterFields pfields = new ParameterFields();

        String sql;
        String sql1;
        String query;
        String ordem;
        String cabecalho;

        String dataatual;

        Int32 idNorClienteDe;
        Int32 idNorClienteAte;
        Int32 idNorSituacaoDe;
        Int32 idNorSituacaoAte;
        Int32 idNorBancoDe;
        Int32 idNorBancoAte;
        Int32 idFormapagtoDe;
        Int32 idFormapagtoAte;
        Int32 idNorVendedor;

        //Ctas Recebidas
        Int32 idRecNorFornecedorDe;
        Int32 idRecNorFornecedorAte;
        Int32 idSituacaoCobrancaCodDe;
        Int32 idSituacaoCobrancaCodAte;

        Int32 idFluClienteDe;
        Int32 idFluClienteAte;

        Int32 idFeriado;

        Int32 idBcoCtaCab01;
        Int32 idBcoCtaCab02;
        Int32 idBcoCtaCab03;
        Int32 idBcoCtaCab04;
        Int32 idBcoCtaCab05;
        Int32 idBcoCtaCab06;

        Decimal credito;
        Decimal debito;
        Decimal saldo;

        Int32 idBanco;
        Int32 dias = 0;

        Boolean bok;

        DataTable Rel = new DataTable();
        DataTable RelCopia = new DataTable();
        DataTable dtRPTFluxoFinanceiro = new DataTable();

        public frmRelReceber()
        {
            InitializeComponent();
        }

        public void Init()
        {
            // tela normal   
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxNorClienteDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxNorClienteAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxRecNorClienteDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxRecNorClienteAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxFluClienteDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxFluClienteAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from situacaotipotitulo order by codigo", tbxFormaPagtoDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo + ' = ' + nome from situacaotipotitulo order by codigo", tbxFormaPagtoAte);

            DateTime daData = Convert.ToDateTime("01/" + DateTime.Now.AddMonths(-1).ToString("MM/yyyy") + " 00:00");
            DateTime ateData = Convert.ToDateTime(daData.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy") + " 23:59");

            //tbxRecNorDtEmissaoDe.Text = daData.ToString("dd/MM/yyyy");
            //tbxRecNorDtEmissaoAte.Text = ateData.ToString("dd/MM/yyyy");

        }

        private void frmRelReceber_Load(object sender, EventArgs e)
        {
            lbxNorOrdem.SelectedIndex = 0;
            lbxRecNorOrdem.SelectedIndex = 0;
            rbnReceberSimples.Checked = true;
            rbnReceberSimples.PerformClick();

            tbxSituacaoCobrancaCodDe.Text = "00";
            tbxSituacaoCobrancaCodAte.Text = "00";
            
            tbxFluBcoCtaDa.Text = "0";
            tbxFluBcoCtaAte.Text = "49";
            tbxFluBcoDtVencimentoDe.Text = DateTime.Now.ToString("dd/MM/yyyy");
            tbxFluBcoDtVencimentoAte.Text = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
            dataatual = DateTime.Now.ToString("dd/MM/yyyy");


            tbxFluDtVencimentoDe.Text = DateTime.Now.AddDays(-90).ToString("dd/MM/yyyy");
            tbxFluDtVencimentoAte.Text = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");

            tbxPrevisaoDiaria.Text = clsParser.DecimalParse("700,00").ToString("N2");

            clsRptFluxoFinanceiroBLL = new clsRptFluxoFinanceiroBLL();


        }
        private void frmRelReceber_Activated(object sender, EventArgs e)
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

        private void tspNorImprimir_Click(object sender, EventArgs e)
        {
            //
            cabecalho = "";
            query = "";
            ordem = "";

            // Cabeçalho  -- objetivo da lista
            if (rbnReceberSimples.Checked == true)
            {
                cabecalho = "Relatorio Ctas a Receber ";
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
                //query = "CLIENTE.COGNOME >= '" + tbxNorClienteDe.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " Do Fornecedor : " + tbxNorClienteDe.Text.Trim();
            }
            if (tbxNorClienteDe.Text.Length == 0 && tbxNorClienteAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                //query = "CLIENTE.COGNOME <= '" + tbxNorClienteAte.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo do : " + tbxNorClienteAte.Text.Trim();
            }
            if (tbxNorClienteDe.Text.Length > 0 && tbxNorClienteAte.Text.Length > 0)
            {
                //query = "CLIENTE.COGNOME >= '" + tbxNorClienteDe.Text + "' AND CLIENTE.COGNOME <= '" + tbxNorClienteAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxNorClienteDe.Text + "  até a : " + tbxNorClienteAte.Text;
            }

            //Data Emissao
            if (clsParser.SqlDateTimeParse(tbxNorDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtEmissaoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                  //  query = query + " AND ";
                }
                //query = query + " RECEBER.EMISSAO >= " + clsParser.SqlDateTimeFormat(tbxNorDtEmissaoDe.Text + " 00:00", true) +
                //"AND RECEBER.EMISSAO <= " + clsParser.SqlDateTimeFormat(tbxNorDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxNorDtEmissaoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxNorDtEmissaoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxNorDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "NFCOMPRA.DATA <= " + "CONVERT(DATETIME,'" + clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text).Value.Day.ToString() + "/" + clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text).Value.Month.ToString() + "/" + clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text).Value.Year.ToString() + "',103)";
                //query = query + "RECEBER.EMISSAO >= " + clsParser.SqlDateTimeFormat(tbxNorDtEmissaoDe.Text + " 00:00", true) +
                //"AND RECEBER.EMISSAO <= " + clsParser.SqlDateTimeFormat(tbxNorDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Emissão: " + tbxNorDtEmissaoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxNorDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "NFCOMPRA.DATA >= " + "CONVERT(DATETIME,'" + clsParser.SqlDateTimeParse(tbxItemDtEmissaoDe.Text).Value.Day.ToString() + "/" + clsParser.SqlDateTimeParse(tbxItemDtEmissaoDe.Text).Value.Month.ToString() + "/" + clsParser.SqlDateTimeParse(tbxItemDtEmissaoDe.Text).Value.Year.ToString() + "',103)" + " AND NFCOMPRA.DATA <= " + "CONVERT(DATETIME,'" + clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text).Value.Day.ToString() + "/" + clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text).Value.Month.ToString() + "/" + clsParser.SqlDateTimeParse(tbxItemDtEmissaoAte.Text).Value.Year.ToString() + "',103)";
                //query = query + "RECEBER.EMISSAO >= " + clsParser.SqlDateTimeFormat(tbxNorDtEmissaoDe.Text + " 00:00", true) +
                //"AND RECEBER.EMISSAO <= " + clsParser.SqlDateTimeFormat(tbxNorDtEmissaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxNorDtEmissaoDe.Text + "  Até a data de Emissão: " + tbxNorDtEmissaoAte.Text;
            }
            //Data Vencimento
            if (clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtVencimentoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxNorDtVencimentoDe.Text + " 00:00", true) +
                //"AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxNorDtVencimentoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento : " + tbxNorDtVencimentoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxNorDtVencimentoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxNorDtVencimentoDe.Text + " 00:00", true) +
                //"AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxNorDtVencimentoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Vencimento : " + tbxNorDtVencimentoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtVencimentoDe.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxNorDtVencimentoDe.Text + " 00:00", true) +
                //"AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxNorDtVencimentoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento : " + tbxNorDtVencimentoDe.Text + "  Até a data de Vencimento: " + tbxNorDtVencimentoAte.Text;
            }
            //Data Previsao
            if (clsParser.SqlDateTimeParse(tbxNorDtPrevisaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtPrevisaoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxNorDtPrevisaoDe.Text + " 00:00", true) +
                //"AND RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxNorDtPrevisaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Previsao : " + tbxNorDtPrevisaoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxNorDtPrevisaoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxNorDtPrevisaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxNorDtPrevisaoDe.Text + " 00:00", true) +
                //"AND RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxNorDtPrevisaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Previsao : " + tbxNorDtPrevisaoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxNorDtPrevisaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxNorDtPrevisaoDe.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxNorDtPrevisaoDe.Text + " 00:00", true) +
                //"AND RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxNorDtPrevisaoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Previsao : " + tbxNorDtPrevisaoDe.Text + "  Até a data de Previsao: " + tbxNorDtPrevisaoAte.Text;
            }
            // NUMERO DO BANCO
            if (tbxNorBancoDe.Text.Length > 0 && tbxNorBancoAte.Text.Length == 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "BANCOS.CONTA >= " + clsParser.Int32Parse(tbxNorBancoDe.Text) + " ";
                cabecalho = cabecalho + Environment.NewLine + " Do Banco Interno : " + tbxNorBancoDe.Text.Trim();
            }
            if (tbxNorBancoDe.Text.Length == 0 && tbxNorBancoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "BANCOS.CONTA <= " + clsParser.Int32Parse(tbxNorBancoAte.Text) + " ";
                cabecalho = cabecalho + Environment.NewLine + " abaixo do : " + tbxNorBancoAte.Text.Trim();
            }
            if (tbxNorBancoDe.Text.Length > 0 && tbxNorBancoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "BANCOS.CONTA >= " + clsParser.Int32Parse(tbxNorBancoDe.Text) + "  AND SITUACAOTITULO.CODIGO <= " + clsParser.Int32Parse(tbxNorBancoAte.Text) + " ";
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxNorBancoDe.Text + "  até a : " + tbxNorBancoAte.Text;
            }

            // SITUAÇÃO DO TITULO
            if (tbxNorSituacaoDe.Text.Length > 0 && tbxNorSituacaoAte.Text.Length == 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "SITUACAOTITULO.CODIGO >= '" + tbxNorSituacaoDe.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " Da Situação : " + tbxNorSituacaoDe.Text.Trim();
            }
            if (tbxNorSituacaoDe.Text.Length == 0 && tbxNorSituacaoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "SITUACAOTITULO.CODIGO <= '" + tbxNorSituacaoAte.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo do : " + tbxNorSituacaoAte.Text.Trim();
            }
            if (tbxNorSituacaoDe.Text.Length > 0 && tbxNorSituacaoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "SITUACAOTITULO.CODIGO >= '" + tbxNorSituacaoDe.Text + "' AND SITUACAOTITULO.CODIGO <= '" + tbxNorSituacaoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxNorSituacaoDe.Text + "  até a : " + tbxNorSituacaoAte.Text;
            }
            // TIPO DO TITULO
            if (tbxFormaPagtoDe.Text.Length > 0 && tbxFormaPagtoAte.Text.Length == 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "SITUACAOTIPOTITULO.CODIGO >= '" + tbxFormaPagtoDe.Text.Trim().Substring(0, 2) + "'";
                cabecalho = cabecalho + Environment.NewLine + " Do Tipo : " + tbxFormaPagtoDe.Text.Trim();
            }
            if (tbxFormaPagtoDe.Text.Length == 0 && tbxFormaPagtoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "SITUACAOTIPOTITULO.CODIGO <= '" + tbxFormaPagtoAte.Text.Trim().Substring(0, 2) + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo do tipo : " + tbxFormaPagtoAte.Text.Trim();
            }
            if (tbxFormaPagtoDe.Text.Length > 0 && tbxFormaPagtoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = query + "SITUACAOTIPOTITULO.CODIGO >= '" + tbxFormaPagtoDe.Text.Trim().Substring(0, 2) + "' AND SITUACAOTIPOTITULO.CODIGO <= '" + tbxFormaPagtoAte.Text.Trim().Substring(0, 2) + "'";
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxFormaPagtoDe.Text + "  até a : " + tbxFormaPagtoAte.Text;
            }

            if (tbxNorVendedor.Text.Length > 1)
            { // Do Vendedor
                if (query.ToString().Length > 0)
                {
                    //query = query + " AND ";
                }
                //query = "CLIENTEVEND.COGNOME = '" + tbxNorVendedor.Text.Trim() + "' ";
                cabecalho = cabecalho + Environment.NewLine + " Vendedor : " + tbxNorVendedor.Text.Trim();
            }


            //if (query.ToString().Length > 0)
            //{
            //    query = query + " AND ";
            //}
            //query = query + "receber.filial= " + clsInfo.zfilial;

            /////////////////////////////////////////////////////////////////////////////////////////
            // ESPECIAIS DE CADA RELATORIO / MODELO PARA A QUERY
           
            /////////////////////////////////////////////////////////////////////////////////////////
            //FILIAL
          
          
            if (rbnConferidos.Checked == true)
            {
                // Só os Conferidos
                if (query.ToString().Length > 2)
                {
                    query= query + " AND ";
                }
               //query = query + "RECEBER.DESPESAPUBLICA = 'S' ";
                //ckxDespesaPublica
            }
            if(rbnConferidosNao.Checked == true)
            {
                // Só os Não Conferidos
                if (query.ToString().Length > 2)
                {
                   // query = query + " AND ";
                }
               // query = query + "RECEBER.DESPESAPUBLICA = 'N' ";
            }

            //Ordem
            if (rbnNorDtVencimento.Checked == true)
            {
               //Ordem para Vencimento
                ordem = "RECEBER.VENCIMENTO, CLIENTE.COGNOME, RECEBER.DUPLICATA";
                if (lbxNorOrdem.SelectedIndex == 0)
                {
                    ordem = "RECEBER.VENCIMENTO, CLIENTE.COGNOME, RECEBER.DUPLICATA";
                }
                if (lbxNorOrdem.SelectedIndex == 1)
                {
                    ordem = "CLIENTE.COGNOME, RECEBER.VENCIMENTO, RECEBER.DUPLICATA";
                }
                if (lbxNorOrdem.SelectedIndex == 2)
                {
                    ordem = "RECEBER.DUPLICATA, CLIENTE.COGNOME, RECEBER.VENCIMENTO";
                }
            }
            else
            {
                //Ordem para Previsao
                ordem = "RECEBER.VENCIMENTOPREV, CLIENTE.COGNOME, RECEBER.DUPLICATA";
                if (lbxNorOrdem.SelectedIndex == 0)
                {
                    ordem = "RECEBER.VENCIMENTOPREV, CLIENTE.COGNOME, RECEBER.DUPLICATA";
                }
                if (lbxNorOrdem.SelectedIndex == 1)
                {
                    ordem = "CLIENTE.COGNOME, RECEBER.VENCIMENTOPREV, RECEBER.DUPLICATA";
                }
                if (lbxNorOrdem.SelectedIndex == 2)
                {
                    ordem = "RECEBER.DUPLICATA, CLIENTE.COGNOME, RECEBER.VENCIMENTOPREV";
                }
            }
            if (rbnConferidos.Checked == true)
            // Cabecalho (lista Analitica ou Sintetica)
            if (rbnNorAnalitica.Checked == true)
            {
                cabecalho = cabecalho +  " Tipo Relatorio Analitica ";
            }
            else
            {
                cabecalho = cabecalho + " Tipo Relatorio Sintetica ";
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
            // Ordem da Lista williams
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxNorOrdem.Text;
            // QUERY

            DataTable Rel = new DataTable();

            //DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            //ParameterField pfieldEmpresa = new ParameterField();
            //ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            //if (carregarcampos.Rows.Count > 0)
            //{
            //    disvalEmpresa.Value = carregarcampos.Rows[0]["NOME"].ToString();
            //    pfieldEmpresa.Name = "EMPRESA";
            //    pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            //}
            //ParameterField pfieldCabecalho = new ParameterField();
            //ParameterDiscreteValue disvalCabecalho = new ParameterDiscreteValue();
            //disvalCabecalho.Value = cabecalho.ToString();
            //pfieldCabecalho.Name = "CABECALHO";
            //pfieldCabecalho.CurrentValues.Add(disvalCabecalho);
            //pfields.Add(pfieldEmpresa);
            //pfields.Add(pfieldCabecalho);

                //A query abaixo serve tanto para o Venvimento quanto para a Previsão
                SqlDataAdapter sda;
                String query1= "SELECT RECEBER.ID ,RECEBER.FILIAL ,RECEBER.DUPLICATA ,RECEBER.POSICAO ,RECEBER.POSICAOFIM ,RECEBER.EMISSAO ,RECEBER.IDDOCUMENTO, DOCFISCAL.COGNOME AS [DOCUMENTO] ,RECEBER.SETOR  " +
                        ",RECEBER.IDCLIENTE, CLIENTE.COGNOME AS [CLIENTE]   " +
                        ",RECEBER.DATALANCA ,RECEBER.EMITENTE ,RECEBER.IDHISTORICO , HISTORICOS.CODIGO AS [HISTORICO], HISTORICOS.NOME AS [HISTORICONOM]   " +
                        ",RECEBER.IDCENTROCUSTO , CENTROCUSTOS.CODIGO AS [CENTROCUSTO], CENTROCUSTOS.NOME AS [CENTROCUSTONOM]   " +
                        ", RECEBER.IDCODIGOCTABIL , CONTACONTABIL.CODIGO AS [CTACONTABIL], CONTACONTABIL.NOME AS [CTACONTABILNOM]   " +
                        ",RECEBER.IDNOTAFISCAL , NFVENDA.NUMERO AS [NOTAFISCAL],  RECEBER.IDRECEBERNFV   " +
                        ",RECEBER.IDFORMAPAGTO , SITUACAOTIPOTITULO.CODIGO AS [FORMAPAGTO], SITUACAOTIPOTITULO.NOME AS [FORMAPAGTONOM]   " +
                        ",RECEBER.OBSERVA  ,  RECEBER.IDBANCO , TAB_BANCOS.CODIGO AS [BANCO], TAB_BANCOS.COGNOME AS [BANCONOM]   " +
                        ",RECEBER.IDBANCOINT , BANCOS.CONTA, BANCOS.NOME, RECEBER.CHEGOU , RECEBER.DESPESAPUBLICA ,RECEBER.BOLETO ,RECEBER.BOLETONRO   " +
                        ",RECEBER.DV ,RECEBER.BAIXA ,RECEBER.VENCIMENTO ,RECEBER.VENCIMENTOPREV ,RECEBER.VALOR ,RECEBER.VALORDESCONTO ,RECEBER.ATEVENCIMENTO,RECEBER.VALORLIQUIDO   " +
                        ",RECEBER.VALORJUROS ,RECEBER.VALORMULTA  " +
                        ",(RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO) AS [VALORARECEBER] " +
                        ",RECEBER.IDSITBANCO , SITUACAOTITULO.CODIGO AS [SITUACAOCODIGO], SITUACAOTITULO.NOME  AS [SITUACAONOME], RECEBER.IMPRIMIR ,RECEBER.VALORPAGO ,RECEBER.VALORBAIXANDO, RECEBER.VALORDEVOLVIDO   " +
                        ",RECEBER.IDVENDEDOR , CLIENTEVEND.COGNOME AS [VENDEDOR], RECEBER.VALORCOMISSAO    " +
                        ",RECEBER.IDSUPERVISOR , CLIENTESUP.COGNOME AS [SUPERVISOR], RECEBER.VALORCOMISSAOSUP  " +
                        ",RECEBER.IDCOORDENADOR  ,CLIENTECOO.COGNOME AS [COORDENADOR], RECEBER.VALORCOMISSAOGER " +
                        "FROM RECEBER " +
                        "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBER.IDDOCUMENTO  " +
                        "LEFT JOIN CLIENTE ON CLIENTE.ID = RECEBER.IDCLIENTE " +
                        "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS ON HISTORICOS.ID = RECEBER.IDHISTORICO " +
                        "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS ON CENTROCUSTOS.ID = RECEBER.IDCENTROCUSTO " +
                        "LEFT JOIN CONTACONTABIL ON CONTACONTABIL.ID = RECEBER.IDCODIGOCTABIL " +
                        "LEFT JOIN NFVENDA ON NFVENDA.ID = RECEBER.IDNOTAFISCAL " +
                        "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBER.IDFORMAPAGTO " +
                        "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID = RECEBER.IDBANCO " +
                        "LEFT JOIN [" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].BANCOS ON BANCOS.ID = RECEBER.IDBANCOINT " +
                        "LEFT JOIN SITUACAOTITULO ON SITUACAOTITULO.ID = RECEBER.IDSITBANCO " +
                        "LEFT JOIN CLIENTE CLIENTEVEND ON CLIENTEVEND.ID = RECEBER.IDVENDEDOR " +
                        "LEFT JOIN CLIENTE CLIENTESUP ON CLIENTESUP.ID = RECEBER.IDSUPERVISOR " +
                        "LEFT JOIN CLIENTE CLIENTECOO ON CLIENTECOO.ID = RECEBER.IDCOORDENADOR ";
                if (query.Length > 0)
                {
                    query1 = query1 + " where " + query;
                }                
                query1 = query1 + " ORDER BY " + ordem;
                sda = new SqlDataAdapter(query1, clsInfo.conexaosqldados);
                sda.Fill(Rel);

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
            valor.Value =cabecalho;
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
            // iNADIMPLENCIAS
            field = new ParameterField();
            field.Name = "Inadimplencia";
            valor = new ParameterDiscreteValue();
            if (rbnInadim_Todos.Checked == true)
            {
                valor.Value = 0;
            } else if (rbnInadim_Sim.Checked == true)
            {
                valor.Value = 999;
            } else if (rbnInadim_Nao.Checked == true)
            {
                valor.Value = 1;
            }
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            field = new ParameterField();
            field.Name = "Vendedor";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxNorVendedor.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            if (rbnNorDtVencimento.Checked == true)
                { // Por Data de Vencimento
                    if (rbnNorAnalitica.Checked == true)  //Analitica
                    {
                        if (rbnNorSemTotal.Checked == true)
                        {
                             //frmCrystalReport frmCrystalReport = new frmCrystalReport();
                             //frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                             frmCrystalReport.Init(clsInfo.caminhorelatorios, "RECEBER_VENCIMENTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                             clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            if (lbxNorOrdem.SelectedIndex == 0)
                            {
                              //  frmCrystalReport frmCrystalReport = new frmCrystalReport();
//                                frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO_ANA_VENC.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                                frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO_ANA_VENC.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                            if (lbxNorOrdem.SelectedIndex == 1)
                            {
                              //  frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                //frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO_ANA_CLIEN.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                                frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO_ANA_CLIEN.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                            if (lbxNorOrdem.SelectedIndex == 2)
                            {   // NESTE CASO ELE NÃO AGRUPA ELE MOSTRA A LISTA ANALITICA
                              //  frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                //frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                                frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                        }
                    }
                    else
                    {
                        if (lbxNorOrdem.SelectedIndex == 0)
                        {
                           // frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            //frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO_SIN_VENC.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO_SIN_VENC.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        if (lbxNorOrdem.SelectedIndex == 1)
                        {
                           // frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            //frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO_SIN_CLIEN.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO_SIN_CLIEN.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        if (lbxNorOrdem.SelectedIndex == 2)
                        {   // NESTE CASO ELE NÃO AGRUPA ELE MOSTRA A LISTA ANALITICA
                           // frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            //frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_VENCIMENTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                    }
                }
                else
                {
                    //Por Data de Previsão
                    if (rbnNorAnalitica.Checked == true)  //Analitica
                    {
                        if (rbnNorSemTotal.Checked == true)
                        {
                           // frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            MessageBox.Show("Precisa atualizar o RPT");
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_PREVISAO.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            if (lbxNorOrdem.SelectedIndex == 0)
                            {
                             //   frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                MessageBox.Show("Precisa atualizar o RPT");
                                frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_PREVISAO_ANA_VENC.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                            if (lbxNorOrdem.SelectedIndex == 1)
                            {
                               // frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                MessageBox.Show("Precisa atualizar o RPT");
                                frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_PREVISAO_ANA_CLIEN.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                            if (lbxNorOrdem.SelectedIndex == 2)
                            {   // NESTE CASO ELE NÃO AGRUPA ELE MOSTRA A LISTA ANALITICA
                                //frmCrystalReport frmCrystalReport = new frmCrystalReport();
                                MessageBox.Show("Precisa atualizar o RPT");
                                frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_PREVISAO_SIN_VENC.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                            }
                        }
                    }
                    else
                    {
                        if (lbxNorOrdem.SelectedIndex == 0)
                        {
                            //frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            MessageBox.Show("Precisa atualizar o RPT");
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_PREVISAO_SIN_VENC.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        if (lbxNorOrdem.SelectedIndex == 1)
                        {
                            //frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            MessageBox.Show("Precisa atualizar o RPT");
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_PREVISAO_SIN_CLIEN.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        if (lbxNorOrdem.SelectedIndex == 2)
                        {   // NESTE CASO ELE NÃO AGRUPA ELE MOSTRA A LISTA ANALITICA
                            //frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            MessageBox.Show("Precisa atualizar o RPT");
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBER_PREVISAO.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                    }
                }
        }


        private void btnNorClienteDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid =  btnNorClienteDe.Name;
            //frmClientePes frmClientePes = new frmClientePes();
            //frmClientePes.Init("Todos", idNorClienteDe);

            //clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnNorClienteAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnNorClienteAte.Name;
            //frmClientePes frmClientePes = new frmClientePes();
            //frmClientePes.Init("Todos", idNorClienteAte);

            //clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void rbnReceberSimples_Click(object sender, EventArgs e)
        {
            tclRelReceber.SelectedTab = tabReceber1;
            gbxReceberNormal.Visible = true;
            gbxRecebidasNormal.Visible = false;
            if (rbnNorDtVencimento.Checked == true)
            {
                rbnNorDtVencimento.Select();
            }
            else
            {
                rbnNorDtPrevisao.Select();
            }

        }
        
        private void rbnRecebidasSimples_Click(object sender, EventArgs e)
        {
            tclRelReceber.SelectedTab = tabRecebidas1;
            gbxReceberNormal.Visible = false;
            gbxRecebidasNormal.Visible = true;
            if (rbnRecNorDtVencimento.Checked == true)
            {
                rbnRecNorDtVencimento.Select();
            }
            else
            {
                rbnRecNorDtPaga.Select();
            }

        }

        private void tspRecNorImprimir_Click(object sender, EventArgs e)
        {
            //
            cabecalho = "";
            query = "";
            ordem = "";

            // Cabeçalho  -- objetivo da lista
            //if (rbnReceberSimples.Checked == true)
            //{
                cabecalho = "Relatorio Ctas Recebidas ";
            //}

            if (rbnRecNorDtVencimento.Checked == true)
            {
                cabecalho = cabecalho + " [ Data de Vencimento ]";
            }
            else
            {
                cabecalho = cabecalho + " [ Data de Pagamento ]";
            }

            // filtrar pelo cognome
            if (tbxRecNorClienteDe.Text.Length > 0 && tbxRecNorClienteAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                query = "CLIENTE.COGNOME >= '" + tbxRecNorClienteDe.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " Do Fornecedor : " + tbxRecNorClienteDe.Text.Trim();
            }
            if (tbxRecNorClienteDe.Text.Length == 0 && tbxRecNorClienteAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                query = "CLIENTE.COGNOME <= '" + tbxRecNorClienteAte.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo do : " + tbxRecNorClienteAte.Text.Trim();
            }
            if (tbxRecNorClienteDe.Text.Length > 0 && tbxRecNorClienteAte.Text.Length > 0)
            {
                query = "CLIENTE.COGNOME >= '" + tbxRecNorClienteDe.Text + "' AND CLIENTE.COGNOME <= '" + tbxRecNorClienteAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxRecNorClienteDe.Text + "  até a : " + tbxRecNorClienteAte.Text;
            }

            //Data Emissao
            if (clsParser.SqlDateTimeParse(tbxRecNorDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxRecNorDtEmissaoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " RECEBIDA.EMISSAO >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtEmissaoDe.Text + " 00:00", true) +
                 " AND RECEBIDA.EMISSAO <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtEmissaoAte.Text + " 23:59", true);  
                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxRecNorDtEmissaoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxRecNorDtEmissaoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxRecNorDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " RECEBIDA.EMISSAO >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtEmissaoDe.Text + " 00:00", true) +
                 " AND RECEBIDA.EMISSAO <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtEmissaoAte.Text + " 23:59", true);  
                cabecalho = cabecalho + Environment.NewLine + "Até a data de Emissão: " + tbxRecNorDtEmissaoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxRecNorDtEmissaoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxRecNorDtEmissaoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " RECEBIDA.EMISSAO >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtEmissaoDe.Text + " 00:00", true) +
                 " AND RECEBIDA.EMISSAO <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtEmissaoAte.Text + " 23:59", true);  

                cabecalho = cabecalho + Environment.NewLine + "Da data de Emissão: " + tbxRecNorDtEmissaoDe.Text + "  Até a data de Emissão: " + tbxRecNorDtEmissaoAte.Text;
            }
            //Data Vencimento
            if (clsParser.SqlDateTimeParse(tbxRecNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxRecNorDtVencimentoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RECEBIDA.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtVencimentoDe.Text + " 00:00", true) +
                "AND RECEBIDA.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtVencimentoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento : " + tbxRecNorDtVencimentoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxRecNorDtVencimentoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxRecNorDtVencimentoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RECEBIDA.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtVencimentoDe.Text + " 00:00", true) +
                "AND RECEBIDA.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtVencimentoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Vencimento : " + tbxRecNorDtVencimentoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxRecNorDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxRecNorDtVencimentoDe.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RECEBIDA.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtVencimentoDe.Text + " 00:00", true) +
                "AND RECEBIDA.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtVencimentoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento : " + tbxRecNorDtVencimentoDe.Text + "  Até a data de Recebimento: " + tbxNorDtVencimentoAte.Text;
            }
            // Situação da Baixa
            if (tbxSituacaoCobrancaCodDe.Text.Length > 0 && tbxSituacaoCobrancaCodAte.Text.Length == 0)
            { 
                // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query += "SITUACAOCOBRANCACOD.CODIGO >= '" + tbxSituacaoCobrancaCodDe.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " Do Cod Bx Cobrança : " + tbxSituacaoCobrancaCodAte.Text.Trim();
            }
            if (tbxSituacaoCobrancaCodDe.Text.Length == 0 && tbxSituacaoCobrancaCodAte.Text.Length > 0)
            { 
                // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query += "SITUACAOCOBRANCACOD.CODIGO <= '" + tbxSituacaoCobrancaCodAte.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo do : " + tbxSituacaoCobrancaCodAte.Text.Trim();
            }
            if (tbxSituacaoCobrancaCodDe.Text.Length > 0 && tbxSituacaoCobrancaCodAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query += "SITUACAOCOBRANCACOD.CODIGO >= '" + tbxSituacaoCobrancaCodDe.Text + "' AND SITUACAOCOBRANCACOD.CODIGO <= '" + tbxSituacaoCobrancaCodAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxSituacaoCobrancaCodDe.Text + "  até a : " + tbxSituacaoCobrancaCodAte.Text;
            }

            //Data Pagamento
            if (clsParser.SqlDateTimeParse(tbxRecNorDtPagtoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxRecNorDtPagtoAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RECEBIDA01.DATAOK >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtPagtoDe.Text + " 00:00", true) +
                "AND RECEBIDA01.DATAOK <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtPagtoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Pagamento : " + tbxRecNorDtPagtoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxRecNorDtPagtoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxRecNorDtPagtoAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RECEBIDA01.DATAOK >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtPagtoDe.Text + " 00:00", true) +
                "AND RECEBIDA01.DATAOK <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtPagtoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data de Pagamento : " + tbxNorDtVencimentoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxRecNorDtPagtoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxRecNorDtPagtoDe.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RECEBIDA01.DATAOK >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtPagtoDe.Text + " 00:00", true) +
                "AND RECEBIDA01.DATAOK <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtPagtoAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data de Pagamento : " + tbxRecNorDtPagtoDe.Text + "  Até a data de Recebimento: " + tbxRecNorDtPagtoAte.Text;
            }
            //Data Baixa
            if (clsParser.SqlDateTimeParse(tbxRecNorDtBaixaDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxRecNorDtBaixaAte.Text).IsNull == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RECEBIDA01.DATAENVIO >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtBaixaDe.Text + " 00:00", true) +
                "AND RECEBIDA01.DATAENVIO <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtBaixaAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data da Baixa : " + tbxRecNorDtBaixaDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxRecNorDtBaixaDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxRecNorDtBaixaAte.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RECEBIDA01.DATAENVIO >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtBaixaDe.Text + " 00:00", true) +
                "AND RECEBIDA01.DATAENVIO <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtBaixaAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Até a data da Baixa : " + tbxNorDtVencimentoAte.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxRecNorDtBaixaDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxRecNorDtBaixaDe.Text).IsNull == false)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RECEBIDA01.DATAENVIO >= " + clsParser.SqlDateTimeFormat(tbxRecNorDtBaixaDe.Text + " 00:00", true) +
                "AND RECEBIDA01.DATAENVIO <= " + clsParser.SqlDateTimeFormat(tbxRecNorDtBaixaAte.Text + " 23:59", true);

                cabecalho = cabecalho + Environment.NewLine + "Da data da Baixa : " + tbxRecNorDtBaixaDe.Text + "  Até a data da Baixa: " + tbxRecNorDtBaixaAte.Text;
            }

            if (tbxRecNorVendedor.Text.Length > 1)
            { // Do Vendedor
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = "CLIENTEVEND.COGNOME = '" + tbxRecNorVendedor.Text.Trim() + "' ";
                cabecalho = cabecalho + Environment.NewLine + " Vendedor : " + tbxRecNorVendedor.Text.Trim();
            }


            if (query.ToString().Length > 0)
            {
                query = query + " AND ";
            }
            query = query + "recebida.filial= " + clsInfo.zfilial + " ";


            /////////////////////////////////////////////////////////////////////////////////////////
            // ESPECIAIS DE CADA RELATORIO / MODELO PARA A QUERY
            /////////////////////////////////////////////////////////////////////////////////////////

            //Ordem
            ordem = " RECEBIDA01.DATAOK, CLIENTE.COGNOME, RECEBIDA.DUPLICATA";
            if (lbxRecNorOrdem.SelectedIndex == 0)
            {
                ordem = " RECEBIDA01.DATAOK, CLIENTE.COGNOME, RECEBIDA.DUPLICATA";
            }
            if (lbxRecNorOrdem.SelectedIndex == 1)
            {
                ordem = " RECEBIDA.VENCIMENTO, CLIENTE.COGNOME, RECEBIDA.DUPLICATA";
            }
            if (lbxRecNorOrdem.SelectedIndex == 2)
            {
                ordem = " RECEBIDA01.DATAENVIO, CLIENTE.COGNOME, RECEBIDA.DUPLICATA";
            }
            if (lbxRecNorOrdem.SelectedIndex == 3)
            {
                ordem = " CLIENTE.COGNOME, RECEBIDA.DUPLICATA";
            }
            if (lbxRecNorOrdem.SelectedIndex == 4)
            {
                ordem = " RECEBIDA.DUPLICATA, CLIENTE.COGNOME, RECEBIDA.VENCIMENTO";
            }
            if (lbxRecNorOrdem.SelectedIndex == 5)
            {
                ordem = " HISTORICO.CODIGO, RECEBIDA01.DATAOK";
            }


            // Cabecalho (lista Analitica ou Sintetica)
            if (rbnRecNorAnalitica.Checked == true)
            {
                cabecalho = cabecalho + " Tipo relatorio Analitica ";
            }
            else
            {
                cabecalho = cabecalho + " Tipo relatorio Sintetica ";
            }
            //  
            if (rbnRecNorSemTotal.Checked == true)
            {
                cabecalho = cabecalho + " Sem Sub-Total ";
            }
            else
            {
                cabecalho = cabecalho + " Com Sub-Total ";
            }
            // Ordem da Lista williams
            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxRecNorOrdem.Text;
            // QUERY

            DataTable Rel = new DataTable();

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
            valor.Value = tbxRecNorClienteDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Cliente ate
            field = new ParameterField();
            field.Name = "ClienteAte";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxRecNorClienteAte.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Da Data Vencimento
            field = new ParameterField();
            field.Name = "DataPagoDe";
            valor = new ParameterDiscreteValue();
            if (tbxRecNorDtPagtoDe.Text.Trim() == "")
            {
                tbxRecNorDtPagtoDe.Text = "01/01/1900";
            }
            valor.Value = tbxRecNorDtPagtoDe.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            // Ate Data Vencimento
            field = new ParameterField();
            field.Name = "DataPagoAte";
            valor = new ParameterDiscreteValue();
            if (tbxRecNorDtPagtoAte.Text.Trim() == "")
            {
                tbxRecNorDtPagtoAte.Text = "01/01/2100";
            }
            valor.Value = tbxRecNorDtPagtoAte.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);
            //Vendedor
            field = new ParameterField();
            field.Name = "Vendedor";
            valor = new ParameterDiscreteValue();
            valor.Value = tbxRecNorVendedor.Text;
            field.CurrentValues.Add(valor);
            parameters.Add(field);


            if (rbnRecNorDtPaga.Checked == true)
            { // Por Data de Vencimento
                if (rbnRecNorAnalitica.Checked == true)  //Analitica
                {
                    if (rbnRecNorSemTotal.Checked == true)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBIDA_DATAPAGO1.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                    }
                    else
                    {
/*                        if (lbxPagNorOrdem.SelectedIndex == 0)
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBIDA_DATAPAGO_ANA_VENC.RPT", Rel, pfields, "");

                             clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);
                        }
                        if (lbxPagNorOrdem.SelectedIndex == 1)
                        {
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBIDA_DATAPAGO_ANA_FORNEC.RPT", Rel, pfields, "");

                             clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);
                        }
                        if (lbxPagNorOrdem.SelectedIndex == 2)
                        {   // NESTE CASO ELE NÃO AGRUPA ELE MOSTRA A LISTA ANALITICA
                            frmCrystalReport frmCrystalReport = new frmCrystalReport();
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBIDA_DATAPAGO.RPT", Rel, pfields, "");

                             clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);
                        }
*/                    }
                }
                else
                {
/*                    if (lbxPagNorOrdem.SelectedIndex == 0)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBIDA_DATAPAGO_SIN_VENC.RPT", Rel, pfields, "");

                         clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);
                    }
                    if (lbxPagNorOrdem.SelectedIndex == 1)
                    {
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBIDA_DATAPAGO_SIN_FORNEC.RPT", Rel, pfields, "");

                         clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);
                    }
                    if (lbxPagNorOrdem.SelectedIndex == 2)
                    {   // NESTE CASO ELE NÃO AGRUPA ELE MOSTRA A LISTA ANALITICA
                        frmCrystalReport frmCrystalReport = new frmCrystalReport();
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_RECEBIDA_DATAPAGO.RPT", Rel, pfields, "");

                         clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport);
                    }
*/               }
            }
        }
        private void tspRecNorRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnNorSituacaoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnNorSituacaoDe.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTITULO", idNorSituacaoDe, "Situacao Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnNorSituacaoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnNorSituacaoAte.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTITULO", idNorSituacaoAte, "Situacao Titulo");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnNorBancoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnNorBancoDe.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idNorBancoDe,clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);

        }

        private void btnNorBancoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnNorBancoAte.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idNorBancoAte,clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);

        }

        private void tspNorRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void tspFluImprimir_Click(object sender, EventArgs e)
        {
            //

            Rel = new DataTable();

            sql = "";
            cabecalho = "";
            query = "";
            ordem = "";

            // Cabeçalho  -- objetivo da lista
            cabecalho = "Relatorio Fluxo Financeiro ";

            if (rbnFluxoSimples.Checked == true)
            {
                cabecalho = cabecalho + " Ctas a Pagar + Previsão Vendas Diaria de R$ " + clsParser.DecimalParse(tbxPrevisaoDiaria.Text).ToString("N2");
            }

            // filtrar pelo cognome
            if (tbxFluClienteDe.Text.Length > 0 && tbxFluClienteAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                query = "CLIENTE.COGNOME >= '" + tbxFluClienteDe.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " Do Cliente/Fornecedor : " + tbxFluClienteDe.Text.Trim();
            }
            if (tbxFluClienteDe.Text.Length == 0 && tbxFluClienteAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                query = "CLIENTE.COGNOME <= '" + tbxFluClienteAte.Text.Trim() + "'";
                cabecalho = cabecalho + Environment.NewLine + " abaixo do : " + tbxFluClienteAte.Text.Trim();
            }
            if (tbxFluClienteDe.Text.Length > 0 && tbxFluClienteAte.Text.Length > 0)
            {
                query = "CLIENTE.COGNOME >= '" + tbxFluClienteDe.Text + "' AND CLIENTE.COGNOME <= '" + tbxFluClienteAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " de : " + tbxFluClienteDe.Text + "  até a : " + tbxFluClienteAte.Text;
            }

            //Data Vencimento
            if (rbnFluDtVencimento.Checked == true)
            {
                if (clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxFluDtVencimentoAte.Text).IsNull == true)
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " AND ";
                    }
                    query = query + "VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoDe.Text + " 00:00", true) +
                    "AND VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoAte.Text + " 23:59", true);

                    cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento : " + tbxFluDtVencimentoDe.Text;
                }
                if (clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxFluDtVencimentoAte.Text).IsNull == false)
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " AND ";
                    }
                    query = query + "VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoDe.Text + " 00:00", true) +
                    "AND VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoAte.Text + " 23:59", true);

                    cabecalho = cabecalho + Environment.NewLine + "Até a data de Vencimento : " + tbxFluDtVencimentoAte.Text;
                }
                if (clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == false)
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " AND ";
                    }
                    query = query + "VENCIMENTO >= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoDe.Text + " 00:00", true) +
                    "AND VENCIMENTO <= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoAte.Text + " 23:59", true);

                    cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento : " + tbxFluDtVencimentoDe.Text + "  Até a data de Recebimento: " + tbxFluDtVencimentoAte.Text;
                }
            }
            else
            {// Data Prevista
                if (clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxFluDtVencimentoAte.Text).IsNull == true)
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " AND ";
                    }
                    query = query + "VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoDe.Text + " 00:00", true) +
                    "AND VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoAte.Text + " 23:59", true);

                    cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento Previsto : " + tbxFluDtVencimentoDe.Text;
                }
                if (clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxFluDtVencimentoAte.Text).IsNull == false)
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " AND ";
                    }
                    query = query + "VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoDe.Text + " 00:00", true) +
                    "AND VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoAte.Text + " 23:59", true);

                    cabecalho = cabecalho + Environment.NewLine + "Até a data de Vencimento Previsto : " + tbxFluDtVencimentoAte.Text;
                }
                if (clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == false)
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " AND ";
                    }
                    query = query + "VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoDe.Text + " 00:00", true) +
                    "AND VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(tbxFluDtVencimentoAte.Text + " 23:59", true);

                    cabecalho = cabecalho + Environment.NewLine + "Da data de Vencimento Previsto : " + tbxFluDtVencimentoDe.Text + "  Até a data de Recebimento Previsto : " + tbxFluDtVencimentoAte.Text;
                }
            }
            if (rbnFlu_Todos.Checked == true)
            {
                cabecalho = cabecalho + " Com e Sem Inadimplentes ";
            }
            else if (rbnFlu_Inadimplentes.Checked == true) 
            {
                cabecalho = cabecalho + " So Inadimplentes ";
            }
            else if (rbnFlu_NaoInadimplentes.Checked == true)
            {
                cabecalho = cabecalho + " Sem Inadimplentes ";
            }

            if (rbnFilial.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + " Filial " + clsInfo.zfilial;
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + " Todas as Filiais ";
            }

            /////////////////////////////////////////////////////////////////////////////////////////
            // ESPECIAIS DE CADA RELATORIO / MODELO PARA A QUERY
            /////////////////////////////////////////////////////////////////////////////////////////
            //Ordem
            if (rbnFluDtVencimento.Checked == true)
            {
                ordem = "VENCIMENTO";
            }
            else
            {
                ordem = "VENCIMENTOPREV";
            }
            // Cabecalho (lista Analitica ou Sintetica)
            if (rbnFluAnalitica.Checked == true)
            {
                cabecalho = cabecalho + " Tipo relatorio Analitica ";
            }
            else
            {
                cabecalho = cabecalho + " Tipo relatorio Sintetica ";
            }
            //  
            // Ordem da Lista 
            if (rbnFluDtVencimento.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + " Ordenação: Vencimento + Credito + Debito + Cliente/Fornecedor + Duplicata ";
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + " Ordenação: Vencimento Previsto + Credito + Debito + Cliente/Fornecedor + Duplicata ";
            }
            // QUERY
            if (rbnFluxoSimples.Checked == true)
            {
                sql = "SELECT  'BPAG' AS [tabela], PAGAR.VALORDESCONTO AS CREDITO, PAGAR.VALORDESCONTO AS DEBITO, PAGAR.VALORDESCONTO AS SALDO, " +
                        "PAGAR.ID, PAGAR.FILIAL, PAGAR.DUPLICATA, PAGAR.POSICAO, PAGAR.POSICAOFIM, PAGAR.EMISSAO, PAGAR.IDDOCUMENTO,  " +
                        "DOCFISCAL.COGNOME AS DOCUMENTO, PAGAR.SETOR, PAGAR.IDFORNECEDOR AS [IDCLIENTE], CLIENTE.COGNOME AS CLIENTE, PAGAR.DATALANCA, PAGAR.EMITENTE,  " +
                        "PAGAR.IDHISTORICO, HISTORICOS.CODIGO AS HISTORICO, HISTORICOS.NOME AS HISTORICONOM, PAGAR.IDCENTROCUSTO,  " +
                        "CENTROCUSTOS.CODIGO AS CENTROCUSTO, CENTROCUSTOS.NOME AS CENTROCUSTONOM, PAGAR.IDCODIGOCTABIL,  " +
                        "CONTACONTABIL.CODIGO AS CTACONTABIL, CONTACONTABIL.NOME AS CTACONTABILNOM, PAGAR.IDNOTAFISCAL, NFCOMPRA.NUMERO AS NOTAFISCAL, " +
                        "PAGAR.IDPAGARNFE  AS IDNOTAPRINCIPAL, PAGAR.IDFORMAPAGTO, SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTO, SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOM,  " +
                        "PAGAR.OBSERVA, PAGAR.IDBANCO, TAB_BANCOS.CODIGO AS BANCO, TAB_BANCOS.COGNOME AS BANCONOM, PAGAR.IDBANCOINT,  " +
                        "BANCOS.CONTA, BANCOS.NOME, PAGAR.CHEGOU, PAGAR.DESPESAPUBLICA, PAGAR.BOLETO, PAGAR.BOLETONRO, PAGAR.DV,  " +
                        "PAGAR.BAIXA, PAGAR.VENCIMENTO, PAGAR.VENCIMENTOPREV, PAGAR.VALOR, PAGAR.VALORDESCONTO, PAGAR.ATEVENCIMENTO, PAGAR.VALORLIQUIDO,  " +
                        "PAGAR.VALORJUROS, PAGAR.VALORMULTA, PAGAR.IDSITBANCO,  SITUACAOTITULO.CODIGO AS [SITBANCOCOD],  " +
                        "(PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO) AS [VALORARECEBER], " +
                        "SITUACAOTITULO.CODIGO AS SITUACAOCODIGO, SITUACAOTITULO.NOME AS SITUACAONOME, PAGAR.IMPRIMIR, PAGAR.VALORPAGO, PAGAR.VALORBAIXANDO,  " +
                        "PAGAR.VALORDEVOLVIDO, PAGAR.IDVENDEDOR, CLIENTEVEND.COGNOME AS VENDEDOR, PAGAR.VALORCOMISSAO, PAGAR.IDSUPERVISOR,  " +
                        "CLIENTESUP.COGNOME AS SUPERVISOR, PAGAR.VALORCOMISSAOSUP, PAGAR.IDCOORDENADOR, CLIENTECOO.COGNOME AS COORDENADOR,  " +
                        "PAGAR.VALORCOMISSAOGER " +
                        "FROM PAGAR LEFT OUTER JOIN " +
                        "DOCFISCAL ON DOCFISCAL.ID = PAGAR.IDDOCUMENTO LEFT OUTER JOIN " +
                        "CLIENTE ON CLIENTE.ID = PAGAR.IDFORNECEDOR LEFT OUTER JOIN " +
                        "[" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS ON HISTORICOS.ID = PAGAR.IDHISTORICO LEFT OUTER JOIN " +
                        "[" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS ON CENTROCUSTOS.ID = PAGAR.IDCENTROCUSTO LEFT OUTER JOIN " +
                        "CONTACONTABIL ON CONTACONTABIL.ID = PAGAR.IDCODIGOCTABIL LEFT OUTER JOIN " +
                        "NFCOMPRA ON NFCOMPRA.ID = PAGAR.IDNOTAFISCAL LEFT OUTER JOIN  " +
                        "SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAR.IDFORMAPAGTO LEFT OUTER JOIN " +
                        "TAB_BANCOS ON TAB_BANCOS.ID = PAGAR.IDBANCO LEFT OUTER JOIN " +
                        "[" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].BANCOS ON BANCOS.ID = PAGAR.IDBANCOINT LEFT OUTER JOIN " +
                        "SITUACAOTITULO ON SITUACAOTITULO.ID = PAGAR.IDSITBANCO LEFT OUTER JOIN  " +
                        "CLIENTE AS CLIENTEVEND ON CLIENTEVEND.ID = PAGAR.IDVENDEDOR LEFT OUTER JOIN  " +
                        "CLIENTE AS CLIENTESUP ON CLIENTESUP.ID = PAGAR.IDSUPERVISOR LEFT OUTER JOIN " +
                        "CLIENTE AS CLIENTECOO ON CLIENTECOO.ID = PAGAR.IDCOORDENADOR ";
                sql = sql + " WHERE SITUACAOTITULO.CODIGO = 0 ";

                Int32 idBancoInt = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + 999, "0"));
                if (rbnFlu_Todos.Checked == true)
                {
                    //cabecalho = cabecalho + " Com e Sem Inadimplentes ";
                }
                else if (rbnFlu_Inadimplentes.Checked == true)
                {
                    sql = sql + " AND IDBANCOINT = " + idBancoInt;
                }
                else
                {
                    sql = sql + " AND IDBANCOINT != " + idBancoInt;
                }

                if (rbnFilial.Checked == true)
                {
                    sql = sql + " AND PAGAR.FILIAL=" + clsInfo.zfilial;
                }

                if (query.Length > 1)
                {
                    sql = sql + " AND " + query + " ";
                }
                if (rbnFluDtPrevisao.Checked == true)
                { // data de previsao
                    sql = sql + " ORDER BY VENCIMENTOPREV ";
                }
                else
                {
                    sql = sql + " ORDER BY VENCIMENTO ";
                }

                SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
                Rel = new DataTable();
                sda.Fill(Rel);

                RelCopia = new DataTable();
                sda.Fill(RelCopia);

                // Colocar o valor da previsão diaria se tiver
                // Antes verificar os dias que não tem duplicatas a pagar
                DateTime verificadia = DateTime.Now.Date.AddDays(-1);
                DateTime dataduplicata = clsParser.DateTimeParse("01/01/1900");
                DateTime datahoje = DateTime.Now.Date;

                foreach (DataRow row in RelCopia.Rows)
                {
                    dataduplicata = clsParser.DateTimeParse(row["VENCIMENTOPREV"].ToString());
                    if (dataduplicata >= DateTime.Today)
                    {
                        TimeSpan diferencadias = dataduplicata - verificadia;
                        if (diferencadias.Days > 1)
                        {
                            for (int i = 1; i < diferencadias.Days; i++)
                            {
                                verificadia = verificadia.AddDays(1);
                                // não tem duplicata a pagar neste dia então inclui
                                // um novo registro para colocar o valor previsto de venda
                                DataRow rowIncluir = Rel.NewRow();
                                rowIncluir["Tabela"] = "AREC";
                                rowIncluir["CREDITO"] = 0; //clsParser.DecimalParse(tbxPrevisaoDiaria.Text);
                                rowIncluir["DEBITO"] = 0;
                                rowIncluir["SALDO"] = 0;
                                rowIncluir["ID"] = 0;
                                rowIncluir["FILIAL"] = 1;
                                rowIncluir["DUPLICATA"] = 0;
                                rowIncluir["POSICAO"] = 1;
                                rowIncluir["POSICAOFIM"] = 1;
                                rowIncluir["EMISSAO"] = DateTime.Now;
                                rowIncluir["DOCUMENTO"] = "PED";
                                rowIncluir["SETOR"] = "N";
                                rowIncluir["IDCLIENTE"] = clsInfo.zempresaclienteid;
                                rowIncluir["CLIENTE"] = clsInfo.zempresacliente_cognome;
                                rowIncluir["DATALANCA"] = DateTime.Now;
                                rowIncluir["EMITENTE"] = clsInfo.zusuario;
                                rowIncluir["IDHISTORICO"] = 0;
                                rowIncluir["HISTORICO"] = "";
                                rowIncluir["HISTORICONOM"] = "";
                                rowIncluir["IDCENTROCUSTO"] = 0;
                                rowIncluir["CENTROCUSTO"] = "";
                                rowIncluir["CENTROCUSTONOM"] = "";
                                rowIncluir["IDCODIGOCTABIL"] = 0;
                                rowIncluir["CTACONTABIL"] = "";
                                rowIncluir["CTACONTABILNOM"] = "";
                                rowIncluir["IDNOTAFISCAL"] = 0;
                                rowIncluir["NOTAFISCAL"] = 0;
                                rowIncluir["IDNOTAPRINCIPAL"] = 0;
                                rowIncluir["IDFORMAPAGTO"] = 0;
                                rowIncluir["FORMAPAGTO"] = "";
                                rowIncluir["FORMAPAGTONOM"] = "";
                                rowIncluir["OBSERVA"] = "";
                                rowIncluir["IDBANCO"] = 0;
                                rowIncluir["BANCO"] = 0;
                                rowIncluir["BANCONOM"] = "";
                                rowIncluir["IDBANCOINT"] = clsInfo.zbancoint;
                                rowIncluir["CONTA"] = 0;
                                rowIncluir["NOME"] = "";
                                rowIncluir["CHEGOU"] = "N";
                                rowIncluir["DESPESAPUBLICA"] = "N";
                                rowIncluir["BOLETO"] = "";
                                rowIncluir["BOLETONRO"] = 0;
                                rowIncluir["DV"] = "";
                                rowIncluir["BAIXA"] = "N";
                                rowIncluir["VENCIMENTO"] = verificadia;
                                rowIncluir["VENCIMENTOPREV"] = verificadia;
                                rowIncluir["VALOR"] = 0;
                                rowIncluir["VALORDESCONTO"] = 0;
                                rowIncluir["ATEVENCIMENTO"] = "N";
                                rowIncluir["VALORLIQUIDO"] = 0;
                                rowIncluir["VALORJUROS"] = 0;
                                rowIncluir["VALORMULTA"] = 0;
                                rowIncluir["VALORARECEBER"] = 0;
                                rowIncluir["IDSITBANCO"] = 0;
                                rowIncluir["SITUACAOCODIGO"] = 0;
                                rowIncluir["SITUACAONOME"] = "";
                                rowIncluir["IMPRIMIR"] = "N";
                                rowIncluir["VALORPAGO"] = 0;
                                rowIncluir["VALORBAIXANDO"] = 0;
                                rowIncluir["IDVENDEDOR"] = 0;
                                rowIncluir["VENDEDOR"] =
                                rowIncluir["VALORCOMISSAO"] = 0;
                                rowIncluir["IDSUPERVISOR"] = 0;
                                rowIncluir["SUPERVISOR"] = "";
                                rowIncluir["VALORCOMISSAOSUP"] = 0;
                                rowIncluir["IDCOORDENADOR"] = 0;
                                rowIncluir["COORDENADOR"] = "";
                                rowIncluir["VALORCOMISSAOGER"] = 0;
                                Rel.Rows.Add(rowIncluir);
                                //Rel.AcceptChanges();
                            }
                        }
                        else
                        {
                            verificadia = clsParser.DateTimeParse(row["VENCIMENTOPREV"].ToString());
                        }

                    }

                }
                // Gravar na Base de Dados para poder manipular melhor
                // Apagar o Arquivo RPT
                SqlConnection scn;
                SqlCommand scd;
                SqlDataReader sdr;
                scn = new SqlConnection(clsInfo.conexaosqldados);

                scd = new SqlCommand("delete rptfluxofinanceiro ", scn);
                scn.Open();
                sdr = scd.ExecuteReader();
                scn.Close();

                foreach (DataRow row in Rel.Rows)
                {
                    clsRptFluxoFinanceiroInfo = new clsRptFluxoFinanceiroInfo();
                    clsRptFluxoFinanceiroInfo.ATEVENCIMENTO = row["atevencimento"].ToString();
                    clsRptFluxoFinanceiroInfo.BAIXA = row["BAIXA"].ToString();
                    clsRptFluxoFinanceiroInfo.BANCO = row["BANCO"].ToString();
                    clsRptFluxoFinanceiroInfo.BANCONOM = row["BANCONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.BOLETO = row["BOLETO"].ToString();
                    clsRptFluxoFinanceiroInfo.BOLETONRO = clsParser.Int32Parse(row["BOLETONRO"].ToString());
                    clsRptFluxoFinanceiroInfo.CENTROCUSTO = row["CENTROCUSTO"].ToString();
                    clsRptFluxoFinanceiroInfo.CENTROCUSTONOM = row["CENTROCUSTONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.CHEGOU = row["CHEGOU"].ToString();
                    clsRptFluxoFinanceiroInfo.CLIENTE = row["CLIENTE"].ToString();
                    clsRptFluxoFinanceiroInfo.CONTA = row["CONTA"].ToString();
                    clsRptFluxoFinanceiroInfo.COORDENADOR = row["COORDENADOR"].ToString();
                    clsRptFluxoFinanceiroInfo.CREDITO = clsParser.DecimalParse(row["CREDITO"].ToString());
                    clsRptFluxoFinanceiroInfo.CTACONTABIL = row["CTACONTABIL"].ToString();
                    clsRptFluxoFinanceiroInfo.CENTROCUSTO = row["CENTROCUSTO"].ToString();
                    clsRptFluxoFinanceiroInfo.CENTROCUSTONOM = row["CENTROCUSTONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.CHEGOU = row["CHEGOU"].ToString();
                    clsRptFluxoFinanceiroInfo.CLIENTE = row["CLIENTE"].ToString();
                    clsRptFluxoFinanceiroInfo.CONTA = row["CONTA"].ToString();
                    clsRptFluxoFinanceiroInfo.COORDENADOR = row["COORDENADOR"].ToString();
                    clsRptFluxoFinanceiroInfo.CREDITO = clsParser.DecimalParse(row["CREDITO"].ToString());
                    clsRptFluxoFinanceiroInfo.CTACONTABIL = row["CTACONTABIL"].ToString();
                    clsRptFluxoFinanceiroInfo.CTACONTABILNOM = row["CTACONTABILNOM"].ToString();
                    clsRptFluxoFinanceiroInfo.DATALANCA = clsParser.DateTimeParse(row["DATALANCA"].ToString());
                    clsRptFluxoFinanceiroInfo.DEBITO = clsParser.DecimalParse(row["DEBITO"].ToString());
                    clsRptFluxoFinanceiroInfo.DESPESAPUBLICA = row["DESPESAPUBLICA"].ToString();
                    clsRptFluxoFinanceiroInfo.DOCUMENTO = row["DOCUMENTO"].ToString();
                    clsRptFluxoFinanceiroInfo.DUPLICATA = clsParser.Int32Parse(row["DUPLICATA"].ToString());
                    clsRptFluxoFinanceiroInfo.DV = clsParser.Int32Parse(row["DV"].ToString());
                    clsRptFluxoFinanceiroInfo.EMISSAO = clsParser.DateTimeParse(row["EMISSAO"].ToString());
                    clsRptFluxoFinanceiroInfo.EMITENTE = row["EMITENTE"].ToString();
                    clsRptFluxoFinanceiroInfo.FILIAL = clsParser.Int32Parse(row["FILIAL"].ToString());
                    clsRptFluxoFinanceiroInfo.FORMAPAGTO = row["FORMAPAGTO"].ToString();
                    clsRptFluxoFinanceiroInfo.FORMAPAGTONOM = row["FORMAPAGTONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.HISTORICO = row["HISTORICO"].ToString();
                    clsRptFluxoFinanceiroInfo.HISTORICONOM = row["HISTORICONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.ID = clsParser.Int32Parse(row["ID"].ToString());
                    clsRptFluxoFinanceiroInfo.IDBANCO = clsParser.Int32Parse(row["IDBANCO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDBANCOINT = clsParser.Int32Parse(row["IDBANCOINT"].ToString());
                    clsRptFluxoFinanceiroInfo.IDCENTROCUSTO = clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDCLIENTE = clsParser.Int32Parse(row["IDCLIENTE"].ToString());
                    clsRptFluxoFinanceiroInfo.IDCODIGOCTABIL = clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString());
                    clsRptFluxoFinanceiroInfo.IDCOORDENADOR = clsParser.Int32Parse(row["IDCOORDENADOR"].ToString());
                    clsRptFluxoFinanceiroInfo.IDDOCUMENTO = clsParser.Int32Parse(row["IDDOCUMENTO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDFORMAPAGTO = clsParser.Int32Parse(row["IDFORMAPAGTO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDHISTORICO = clsParser.Int32Parse(row["IDHISTORICO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDNOTAFISCAL = clsParser.Int32Parse(row["IDNOTAFISCAL"].ToString());
                    clsRptFluxoFinanceiroInfo.IDNOTAPRINCIPAL = clsParser.Int32Parse(row["IDNOTAPRINCIPAL"].ToString());
                    clsRptFluxoFinanceiroInfo.IDSITBANCO = clsParser.Int32Parse(row["IDSITBANCO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDSUPERVISOR = clsParser.Int32Parse(row["IDSUPERVISOR"].ToString());
                    clsRptFluxoFinanceiroInfo.IDVENDEDOR = clsParser.Int32Parse(row["IDVENDEDOR"].ToString());
                    clsRptFluxoFinanceiroInfo.IMPRIMIR = row["IMPRIMIR"].ToString();
                    clsRptFluxoFinanceiroInfo.NOME = row["NOME"].ToString();
                    clsRptFluxoFinanceiroInfo.NOTAFISCAL = row["NOTAFISCAL"].ToString();
                    clsRptFluxoFinanceiroInfo.OBSERVA = row["OBSERVA"].ToString();
                    clsRptFluxoFinanceiroInfo.POSICAO = clsParser.Int32Parse(row["POSICAO"].ToString());
                    clsRptFluxoFinanceiroInfo.POSICAOFIM = clsParser.Int32Parse(row["POSICAOFIM"].ToString());
                    clsRptFluxoFinanceiroInfo.SALDO = clsParser.DecimalParse(row["SALDO"].ToString());
                    clsRptFluxoFinanceiroInfo.SETOR = row["SETOR"].ToString();
                    clsRptFluxoFinanceiroInfo.SITBANCOCOD = row["SITBANCOCOD"].ToString();
                    clsRptFluxoFinanceiroInfo.SITUACAOCODIGO = row["SITUACAOCODIGO"].ToString();
                    clsRptFluxoFinanceiroInfo.SITUACAONOME = row["SITUACAONOME"].ToString();
                    clsRptFluxoFinanceiroInfo.SUPERVISOR = row["SUPERVISOR"].ToString();
                    clsRptFluxoFinanceiroInfo.TABELA = row["TABELA"].ToString();
                    clsRptFluxoFinanceiroInfo.VALOR = clsParser.DecimalParse(row["VALOR"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORARECEBER = clsParser.DecimalParse(row["VALORARECEBER"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORBAIXANDO = clsParser.DecimalParse(row["VALORBAIXANDO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORCOMISSAO = clsParser.DecimalParse(row["VALORCOMISSAO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORCOMISSAOGER = clsParser.DecimalParse(row["VALORCOMISSAOGER"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORCOMISSAOSUP = clsParser.DecimalParse(row["VALORCOMISSAOSUP"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORDESCONTO = clsParser.DecimalParse(row["VALORDESCONTO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORDEVOLVIDO = clsParser.DecimalParse(row["VALORDEVOLVIDO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORJUROS = clsParser.DecimalParse(row["VALORJUROS"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORLIQUIDO = clsParser.DecimalParse(row["VALORLIQUIDO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORMULTA = clsParser.DecimalParse(row["VALORMULTA"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORPAGO = clsParser.DecimalParse(row["VALORPAGO"].ToString());
                    clsRptFluxoFinanceiroInfo.VENCIMENTO = clsParser.DateTimeParse(row["VENCIMENTO"].ToString());
                    clsRptFluxoFinanceiroInfo.VENCIMENTOPREV = clsParser.DateTimeParse(row["VENCIMENTOPREV"].ToString());
                    clsRptFluxoFinanceiroInfo.VENDEDOR = row["VENDEDOR"].ToString();

                    clsRptFluxoFinanceiroInfo.ID = clsRptFluxoFinanceiroBLL.Incluir(clsRptFluxoFinanceiroInfo, clsInfo.conexaosqldados);
                }
                sql = "Select * from RPTFLUXOFINANCEIRO ORDER BY VENCIMENTOPREV, TABELA, ID";
                sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
                dtRPTFluxoFinanceiro = new DataTable();
                sda.Fill(dtRPTFluxoFinanceiro);
                verificadia = DateTime.Now.Date.AddDays(-1);
                dataduplicata = clsParser.DateTimeParse("01/01/1900");
                // Colocar a taxa de previsao
                foreach (DataRow row in dtRPTFluxoFinanceiro.Rows)
                {
                    dataduplicata = clsParser.DateTimeParse(row["VENCIMENTOPREV"].ToString());
                    if (dataduplicata >= DateTime.Today)
                    {
                        if (verificadia != clsParser.DateTimeParse(row["VENCIMENTOPREV"].ToString()))
                        {
                            clsRptFluxoFinanceiroInfo = clsRptFluxoFinanceiroBLL.Carregar(clsParser.Int32Parse(row["ID"].ToString()), clsInfo.conexaosqldados);
                            clsRptFluxoFinanceiroInfo.CREDITO = clsParser.DecimalParse(tbxPrevisaoDiaria.Text);
                            clsRptFluxoFinanceiroBLL.Alterar(clsRptFluxoFinanceiroInfo, clsInfo.conexaosqldados);
                            verificadia = clsParser.DateTimeParse(row["VENCIMENTOPREV"].ToString());
                        }
                    }
                }

                // Calcular
                sql = "Select * from RPTFLUXOFINANCEIRO ORDER BY VENCIMENTOPREV, TABELA, ID";
                sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
                dtRPTFluxoFinanceiro = new DataTable();
                sda.Fill(dtRPTFluxoFinanceiro);

                saldo = 0;
                foreach (DataRow row in dtRPTFluxoFinanceiro.Rows)
                {

                    credito = 0;
                    debito = 0;
                    if (row["TABELA"].ToString() == "AREC")
                    {
                        if (clsParser.Int32Parse(row["SITUACAOCODIGO"].ToString()) > 0)
                        {
                            credito = 0;
                        }
                        else
                        {
                            //credito = clsParser.DecimalParse(row["VALORARECEBER"].ToString());
                        }
                    }
                    else
                    {
                        debito = clsParser.DecimalParse(row["VALORARECEBER"].ToString());
                        if (clsParser.DecimalParse(row["CREDITO"].ToString()) > 0)
                        {
                            credito = clsParser.DecimalParse(row["CREDITO"].ToString());
                        }
                    }
                    if (credito == 0 && debito == 0)
                    {
                       // row.Delete();
                    }
                    else
                    {
                        saldo = ((saldo + credito) - debito);
                        clsRptFluxoFinanceiroInfo = clsRptFluxoFinanceiroBLL.Carregar(clsParser.Int32Parse(row["ID"].ToString()), clsInfo.conexaosqldados);
                        clsRptFluxoFinanceiroInfo.CREDITO = credito;
                        clsRptFluxoFinanceiroInfo.DEBITO = debito;
                        clsRptFluxoFinanceiroInfo.SALDO = saldo;
                        clsRptFluxoFinanceiroBLL.Alterar(clsRptFluxoFinanceiroInfo, clsInfo.conexaosqldados);
                    }

                }
            }
            else
            {
                sql = "SELECT  'AREC' AS Tabela,  RECEBER.VALORDESCONTO AS CREDITO, RECEBER.VALORDESCONTO AS DEBITO, RECEBER.VALORDESCONTO AS SALDO, " +
                "RECEBER.ID, RECEBER.FILIAL, RECEBER.DUPLICATA, RECEBER.POSICAO, RECEBER.POSICAOFIM, RECEBER.EMISSAO, RECEBER.IDDOCUMENTO,  " +
                "DOCFISCAL.COGNOME AS DOCUMENTO, RECEBER.SETOR, RECEBER.IDCLIENTE, CLIENTE.COGNOME AS CLIENTE, RECEBER.DATALANCA, RECEBER.EMITENTE,  " +
                "RECEBER.IDHISTORICO, HISTORICOS.CODIGO AS HISTORICO, HISTORICOS.NOME AS HISTORICONOM,  " +
                "RECEBER.IDCENTROCUSTO, CENTROCUSTOS.CODIGO AS CENTROCUSTO, CENTROCUSTOS.NOME AS CENTROCUSTONOM,  " +
                "RECEBER.IDCODIGOCTABIL, CONTACONTABIL.CODIGO AS CTACONTABIL, CONTACONTABIL.NOME AS CTACONTABILNOM, RECEBER.IDNOTAFISCAL,  " +
                "NFVENDA.NUMERO AS NOTAFISCAL, RECEBER.IDRECEBERNFV AS IDNOTAPRINCIPAL, RECEBER.IDFORMAPAGTO, SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTO,  " +
                "SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOM, RECEBER.OBSERVA, RECEBER.IDBANCO, TAB_BANCOS.CODIGO AS BANCO,  " +
                "TAB_BANCOS.COGNOME AS BANCONOM, RECEBER.IDBANCOINT, BANCOS.CONTA, BANCOS.NOME, RECEBER.CHEGOU,  " +
                "RECEBER.DESPESAPUBLICA, RECEBER.BOLETO, RECEBER.BOLETONRO, RECEBER.DV, RECEBER.BAIXA, RECEBER.VENCIMENTO,  " +
                "RECEBER.VENCIMENTOPREV, RECEBER.VALOR, RECEBER.VALORDESCONTO, RECEBER.ATEVENCIMENTO, RECEBER.VALORLIQUIDO, RECEBER.VALORJUROS,  " +
                "RECEBER.VALORMULTA, RECEBER.IDSITBANCO, SITUACAOTITULO.CODIGO AS [SITBANCOCOD],  " +
                "(RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO) AS [VALORARECEBER], " +
                "SITUACAOTITULO.CODIGO AS SITUACAOCODIGO, SITUACAOTITULO.NOME AS SITUACAONOME, RECEBER.IMPRIMIR, RECEBER.VALORPAGO,  " +
                "RECEBER.VALORBAIXANDO, RECEBER.VALORDEVOLVIDO, RECEBER.IDVENDEDOR, CLIENTEVEND.COGNOME AS VENDEDOR, RECEBER.VALORCOMISSAO,  " +
                "RECEBER.IDSUPERVISOR, CLIENTESUP.COGNOME AS SUPERVISOR, RECEBER.VALORCOMISSAOSUP, RECEBER.IDCOORDENADOR,  " +
                "CLIENTECOO.COGNOME AS COORDENADOR, RECEBER.VALORCOMISSAOGER " +
                "FROM RECEBER LEFT OUTER JOIN " +
                "DOCFISCAL ON DOCFISCAL.ID = RECEBER.IDDOCUMENTO LEFT OUTER JOIN " +
                "CLIENTE ON CLIENTE.ID = RECEBER.IDCLIENTE LEFT OUTER JOIN " +
                "[" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS ON HISTORICOS.ID = RECEBER.IDHISTORICO LEFT OUTER JOIN " +
                "[" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS ON CENTROCUSTOS.ID = RECEBER.IDCENTROCUSTO LEFT OUTER JOIN " +
                "CONTACONTABIL ON CONTACONTABIL.ID = RECEBER.IDCODIGOCTABIL LEFT OUTER JOIN " +
                "NFVENDA ON NFVENDA.ID = RECEBER.IDNOTAFISCAL LEFT OUTER JOIN " +
                "SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBER.IDFORMAPAGTO LEFT OUTER JOIN " +
                "TAB_BANCOS ON TAB_BANCOS.ID = RECEBER.IDBANCO LEFT OUTER JOIN " +
                "[" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].BANCOS ON BANCOS.ID = RECEBER.IDBANCOINT LEFT OUTER JOIN " +
                "SITUACAOTITULO ON SITUACAOTITULO.ID = RECEBER.IDSITBANCO LEFT OUTER JOIN " +
                "CLIENTE AS CLIENTEVEND ON CLIENTEVEND.ID = RECEBER.IDVENDEDOR LEFT OUTER JOIN " +
                "CLIENTE AS CLIENTESUP ON CLIENTESUP.ID = RECEBER.IDSUPERVISOR LEFT OUTER JOIN " +
                "CLIENTE AS CLIENTECOO ON CLIENTECOO.ID = RECEBER.IDCOORDENADOR ";

                sql = sql + " WHERE SITUACAOTITULO.CODIGO = 0 ";
                Int32 idBancoInt = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + 999, "0"));
                if (rbnFlu_Todos.Checked == true)
                {
                    //cabecalho = cabecalho + " Com e Sem Inadimplentes ";
                }
                else if (rbnFlu_Inadimplentes.Checked == true)
                {
                    sql = sql + " AND IDBANCOINT = " + idBancoInt;
                }
                else
                {
                    sql = sql + " AND IDBANCOINT != " + idBancoInt;
                }
                if (rbnFilial.Checked == true)
                {
                    sql = sql + " AND RECEBER.FILIAL=" + clsInfo.zfilial;
                }
                if (query.Length > 1)
                {
                    sql = sql + " AND " + query + " ";
                }
                sql = sql + " union " +
                "SELECT  'BPAG' AS [tabela], PAGAR.VALORDESCONTO AS CREDITO, PAGAR.VALORDESCONTO AS DEBITO, PAGAR.VALORDESCONTO AS SALDO, " +
                "PAGAR.ID, PAGAR.FILIAL, PAGAR.DUPLICATA, PAGAR.POSICAO, PAGAR.POSICAOFIM, PAGAR.EMISSAO, PAGAR.IDDOCUMENTO,  " +
                "DOCFISCAL.COGNOME AS DOCUMENTO, PAGAR.SETOR, PAGAR.IDFORNECEDOR AS [IDCLIENTE], CLIENTE.COGNOME AS CLIENTE, PAGAR.DATALANCA, PAGAR.EMITENTE,  " +
                "PAGAR.IDHISTORICO, HISTORICOS.CODIGO AS HISTORICO, HISTORICOS.NOME AS HISTORICONOM, PAGAR.IDCENTROCUSTO,  " +
                "CENTROCUSTOS.CODIGO AS CENTROCUSTO, CENTROCUSTOS.NOME AS CENTROCUSTONOM, PAGAR.IDCODIGOCTABIL,  " +
                "CONTACONTABIL.CODIGO AS CTACONTABIL, CONTACONTABIL.NOME AS CTACONTABILNOM, PAGAR.IDNOTAFISCAL, NFCOMPRA.NUMERO AS NOTAFISCAL, " +
                "PAGAR.IDPAGARNFE  AS IDNOTAPRINCIPAL, PAGAR.IDFORMAPAGTO, SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTO, SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOM,  " +
                "PAGAR.OBSERVA, PAGAR.IDBANCO, TAB_BANCOS.CODIGO AS BANCO, TAB_BANCOS.COGNOME AS BANCONOM, PAGAR.IDBANCOINT,  " +
                "BANCOS.CONTA, BANCOS.NOME, PAGAR.CHEGOU, PAGAR.DESPESAPUBLICA, PAGAR.BOLETO, PAGAR.BOLETONRO, PAGAR.DV,  " +
                "PAGAR.BAIXA, PAGAR.VENCIMENTO, PAGAR.VENCIMENTOPREV, PAGAR.VALOR, PAGAR.VALORDESCONTO, PAGAR.ATEVENCIMENTO, PAGAR.VALORLIQUIDO,  " +
                "PAGAR.VALORJUROS, PAGAR.VALORMULTA, PAGAR.IDSITBANCO,  SITUACAOTITULO.CODIGO AS [SITBANCOCOD],  " +
                "(PAGAR.VALORLIQUIDO + pagar.VALORBAIXANDO) - (PAGAR.VALORPAGO + pagar.VALORDEVOLVIDO) AS [VALORARECEBER], " +
                "SITUACAOTITULO.CODIGO AS SITUACAOCODIGO, SITUACAOTITULO.NOME AS SITUACAONOME, PAGAR.IMPRIMIR, PAGAR.VALORPAGO, PAGAR.VALORBAIXANDO,  " +
                "PAGAR.VALORDEVOLVIDO, PAGAR.IDVENDEDOR, CLIENTEVEND.COGNOME AS VENDEDOR, PAGAR.VALORCOMISSAO, PAGAR.IDSUPERVISOR,  " +
                "CLIENTESUP.COGNOME AS SUPERVISOR, PAGAR.VALORCOMISSAOSUP, PAGAR.IDCOORDENADOR, CLIENTECOO.COGNOME AS COORDENADOR,  " +
                "PAGAR.VALORCOMISSAOGER " +
                "FROM PAGAR LEFT OUTER JOIN " +
                "DOCFISCAL ON DOCFISCAL.ID = PAGAR.IDDOCUMENTO LEFT OUTER JOIN " +
                "CLIENTE ON CLIENTE.ID = PAGAR.IDFORNECEDOR LEFT OUTER JOIN " +
                "[" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].HISTORICOS ON HISTORICOS.ID = PAGAR.IDHISTORICO LEFT OUTER JOIN " +
                "[" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].CENTROCUSTOS ON CENTROCUSTOS.ID = PAGAR.IDCENTROCUSTO LEFT OUTER JOIN " +
                "CONTACONTABIL ON CONTACONTABIL.ID = PAGAR.IDCODIGOCTABIL LEFT OUTER JOIN " +
                "NFCOMPRA ON NFCOMPRA.ID = PAGAR.IDNOTAFISCAL LEFT OUTER JOIN  " +
                "SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAR.IDFORMAPAGTO LEFT OUTER JOIN " +
                "TAB_BANCOS ON TAB_BANCOS.ID = PAGAR.IDBANCO LEFT OUTER JOIN " +
                "[" + clsInfo.conexaosqlbanco.Split('=').GetValue(2).ToString().Split(';').GetValue(0).ToString() + "].[dbo].BANCOS ON BANCOS.ID = PAGAR.IDBANCOINT LEFT OUTER JOIN " +
                "SITUACAOTITULO ON SITUACAOTITULO.ID = PAGAR.IDSITBANCO LEFT OUTER JOIN  " +
                "CLIENTE AS CLIENTEVEND ON CLIENTEVEND.ID = PAGAR.IDVENDEDOR LEFT OUTER JOIN  " +
                "CLIENTE AS CLIENTESUP ON CLIENTESUP.ID = PAGAR.IDSUPERVISOR LEFT OUTER JOIN " +
                "CLIENTE AS CLIENTECOO ON CLIENTECOO.ID = PAGAR.IDCOORDENADOR ";
                sql = sql + " WHERE SITUACAOTITULO.CODIGO = 0 ";

                if (rbnFlu_Todos.Checked == true)
                {
                    //cabecalho = cabecalho + " Com e Sem Inadimplentes ";
                }
                else if (rbnFlu_Inadimplentes.Checked == true)
                {
                    sql = sql + " AND IDBANCOINT = " + idBancoInt;
                }
                else
                {
                    sql = sql + " AND IDBANCOINT != " + idBancoInt;
                }

                if (rbnFilial.Checked == true)
                {
                    sql = sql + " AND PAGAR.FILIAL=" + clsInfo.zfilial;
                }
                if (query.Length > 1)
                {
                    sql = sql + " AND " + query + " ";
                }
                SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
                //scd.Parameters.Add("@id", SqlDbType.Int).Value = idPagar;
                Rel = new DataTable();
                sda.Fill(Rel);


                ///////////////////////////////////

                DataRow drFluxo;
                DateTime dtCompensar;
                // ACERTAR AS DATAS DE COMPENSAÇÃO
                Rel.DefaultView.Sort = "IDBANCOINT"; //, TABELA, CLIENTE ASC";
                Rel = Rel.DefaultView.ToTable();
                dias = 0;
                idBanco = 0;
                for (Int32 x = 0; x < Rel.Rows.Count; x++)
                {// fazer o calculo
                    drFluxo = Rel.Rows[x];
                    if (clsParser.Int32Parse(drFluxo["IDBANCOINT"].ToString()) != idBanco)
                    {  // pesquisar O cmapo compensar (qtde de dias) que cai o cheque dentro da cta do aplibank
                        idBanco = clsParser.Int32Parse(drFluxo["IDBANCOINT"].ToString());
                        if (idBanco > 0)
                        {
                            dias = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select COMPENSAR from BANCOS where ID = " + idBanco, "").ToString());
                            //dias =  clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "COMPENSAR", "ID", idBanco).ToString());
                        }
                        else
                        {
                            dias = 0;
                        }
                    }
                    if (clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString()) > 0)
                    {
                        if (rbnFluDtVencimento.Checked == true)
                        {   // Por Vencimento

                            if (drFluxo["VENCIMENTO"].ToString() != "")
                            {
                                if (drFluxo["TABELA"].ToString() == "AREC")
                                {
                                    dtCompensar = DateTime.Parse(drFluxo["VENCIMENTO"].ToString());
                                    drFluxo["VENCIMENTO"] = dtCompensar.AddDays(dias).ToString("dd/MM/yyyy");
                                    // VERIFICAR SE É SABADO/DOMINGO OU FERIADO
                                    dtCompensar = DateTime.Parse(drFluxo["VENCIMENTO"].ToString());
                                    bok = true;
                                    while (bok == true)
                                    {
                                        if (dtCompensar.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            dtCompensar = dtCompensar.AddDays(2);
                                        }
                                        else if (dtCompensar.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            dtCompensar = dtCompensar.AddDays(1);
                                        }
                                        // verificar se não é feriado
                                        // 
                                        //idFeriado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from FERIADOS where DATA = '" + dtCompensar.ToString("dd/MM/yyyy") + "'", "0").ToString());
                                        idFeriado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from FERIADOS where DATA = " + clsParser.SqlDateTimeFormat(dtCompensar.ToString("dd/MM/yyyy") + " 00:00", true), "0").ToString());
                                        //idFeriado = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "FERIADOS", "ID", "DATA", dtCompensar.ToString("yyyy-MM-dd")).ToString());
                                        if (idFeriado > 0)
                                        {  // é feriado => somar + 1 dia
                                            dtCompensar = dtCompensar.AddDays(1);
                                        }
                                        else
                                        { // sair
                                            bok = false;
                                            drFluxo["VENCIMENTO"] = dtCompensar.ToString("dd/MM/yyyy");
                                        }
                                    }

                                }
                                else
                                {
                                    dtCompensar = DateTime.Parse(drFluxo["VENCIMENTO"].ToString());
                                    bok = true;
                                    while (bok == true)
                                    {
                                        if (dtCompensar.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            if (drFluxo["DESPESAPUBLICA"].ToString() == "S")
                                            {
                                                dtCompensar = dtCompensar.AddDays(-1);
                                            }
                                            else
                                            {
                                                dtCompensar = dtCompensar.AddDays(2);
                                            }

                                        }
                                        else if (dtCompensar.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            if (drFluxo["DESPESAPUBLICA"].ToString() == "S")
                                            {
                                                dtCompensar = dtCompensar.AddDays(-2);
                                            }
                                            else
                                            {
                                                dtCompensar = dtCompensar.AddDays(1);
                                            }
                                        }
                                        // verificar se não é feriado
                                        //
                                        idFeriado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from FERIADOS where DATA = " + clsParser.SqlDateTimeFormat(DateTime.Now.ToString("dd/MM/yyyy") + " 00:00", true), "0").ToString());
                                        //idFeriado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from FERIADOS where DATA = '" + dtCompensar.ToString("yyyy-MM-dd") + "'", "0").ToString());
                                        //idFeriado = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "FERIADOS", "ID", "DATA", dtCompensar.ToString("yyyy-MM-dd")).ToString());
                                        if (idFeriado > 0)
                                        {  // é feriado => somar + 1 dia
                                            if (drFluxo["DESPESAPUBLICA"].ToString() == "S")
                                            {
                                                dtCompensar = dtCompensar.AddDays(-1);
                                            }
                                            else
                                            {
                                                dtCompensar = dtCompensar.AddDays(1);
                                            }
                                        }
                                        else
                                        { // sair
                                            bok = false;
                                            drFluxo["VENCIMENTO"] = dtCompensar.ToString("dd/MM/yyyy");
                                        }
                                    }
                                }

                            }
                            else
                            {
                                drFluxo.Delete();
                            }
                        }
                        else
                        { // Por data de Previsão
                            if (drFluxo["VENCIMENTOPREV"].ToString() != "")
                            {
                                if (drFluxo["TABELA"].ToString() == "AREC")
                                {
                                    dtCompensar = DateTime.Parse(drFluxo["VENCIMENTOPREV"].ToString());
                                    drFluxo["VENCIMENTOPREV"] = dtCompensar.AddDays(dias).ToString("dd/MM/yyyy");
                                    // VERIFICAR SE É SABADO/DOMINGO OU FERIADO
                                    dtCompensar = DateTime.Parse(drFluxo["VENCIMENTOPREV"].ToString());
                                    bok = true;
                                    while (bok == true)
                                    {
                                        if (dtCompensar.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            dtCompensar = dtCompensar.AddDays(2);
                                        }
                                        else if (dtCompensar.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            dtCompensar = dtCompensar.AddDays(1);
                                        }
                                        // verificar se não é feriado
                                        // 
                                        //idFeriado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from FERIADOS where DATA = '" + dtCompensar.ToString("yyyy-MM-dd") + "'", "0").ToString());
                                        //idFeriado = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "FERIADOS", "ID", "DATA", dtCompensar.ToString("yyyy-MM-dd")).ToString());
                                        idFeriado = 0;
                                        if (idFeriado > 0)
                                        {  // é feriado => somar + 1 dia
                                            dtCompensar = dtCompensar.AddDays(1);
                                        }
                                        else
                                        { // sair
                                            bok = false;
                                            drFluxo["VENCIMENTOPREV"] = dtCompensar.ToString("dd/MM/yyyy");
                                        }
                                    }

                                }
                                else
                                {
                                    dtCompensar = DateTime.Parse(drFluxo["VENCIMENTOPREV"].ToString());
                                    bok = true;
                                    /*
                                    while (bok == true)
                                    {
                                        if (dtCompensar.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            if (drFluxo["DESPESAPUBLICA"].ToString() == "S")
                                            {
                                                dtCompensar = dtCompensar.AddDays(-1);
                                            }
                                            else
                                            {
                                                dtCompensar = dtCompensar.AddDays(2);
                                            }

                                        }
                                        else if (dtCompensar.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            if (drFluxo["DESPESAPUBLICA"].ToString() == "S")
                                            {
                                                dtCompensar = dtCompensar.AddDays(-2);
                                            }
                                            else
                                            {
                                                dtCompensar = dtCompensar.AddDays(1);
                                            }
                                        }
                                        // verificar se não é feriado
                                        // 
                                        idFeriado = 0;
                                        //idFeriado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from FERIADOS where DATA = '" + dtCompensar.ToString("yyyy-MM-dd") + "'", "0"));
                                        //idFeriado = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "FERIADOS", "ID", "DATA", dtCompensar.ToString("yyyy-MM-dd")).ToString());
                                        if (idFeriado > 0)
                                        {  // é feriado => somar + 1 dia
                                            if (drFluxo["DESPESAPUBLICA"].ToString() == "S")
                                            {
                                                dtCompensar = dtCompensar.AddDays(-1);
                                            }
                                            else
                                            {
                                                dtCompensar = dtCompensar.AddDays(1);
                                            }
                                        }
                                        else
                                        { // sair
                                            bok = false;
                                            drFluxo["VENCIMENTOPREV"] = dtCompensar.ToString("dd/MM/yyyy");
                                        }
                                    }*/
                                }

                            }
                            else
                            {
                                drFluxo.Delete();
                            }
                        }
                    }
                    else
                    {
                        drFluxo.Delete();
                    }


                }
                Rel.AcceptChanges();
                //    
                if (rbnFluDtVencimento.Checked == true)
                {   // Por Vencimento
                    Rel.DefaultView.Sort = "VENCIMENTO, TABELA, DUPLICATA"; //, TABELA, CLIENTE ASC";
                }
                else
                {
                    Rel.DefaultView.Sort = "VENCIMENTOPREV, TABELA, DUPLICATA"; //, TABELA, CLIENTE ASC";
                }
                Rel = Rel.DefaultView.ToTable();
                saldo = 0;
                for (Int32 x = 0; x < Rel.Rows.Count; x++)
                {// fazer o calculo
                    drFluxo = Rel.Rows[x];
                    credito = 0;
                    debito = 0;
                    if (drFluxo["TABELA"].ToString() == "AREC")
                    {
                        if (clsParser.Int32Parse(drFluxo["SITUACAOCODIGO"].ToString()) > 0)
                        {
                            credito = 0;
                        }
                        else
                        {
                            credito = clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString());
                        }
                    }
                    else
                    {
                        debito = clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString());
                        if (clsParser.DecimalParse(drFluxo["CREDITO"].ToString()) > 0)
                        {
                            credito = clsParser.DecimalParse(drFluxo["CREDITO"].ToString());
                        }
                    }
                    if (credito == 0 && debito == 0)
                    {
                        drFluxo.Delete();
                    }
                    else
                    {
                        saldo = ((saldo + credito) - debito);
                        drFluxo["CREDITO"] = credito;
                        drFluxo["DEBITO"] = debito;
                        drFluxo["SALDO"] = saldo;
                    }
                }
                Rel.AcceptChanges();

                // Apagar o Arquivo RPT
                SqlConnection scn;
                SqlCommand scd;
                SqlDataReader sdr;
                scn = new SqlConnection(clsInfo.conexaosqldados);

                scd = new SqlCommand("delete rptfluxofinanceiro ", scn);
                scn.Open();
                sdr = scd.ExecuteReader();
                scn.Close();

                foreach (DataRow row in Rel.Rows)
                {
                    clsRptFluxoFinanceiroInfo = new clsRptFluxoFinanceiroInfo();
                    clsRptFluxoFinanceiroInfo.ATEVENCIMENTO = row["atevencimento"].ToString();
                    clsRptFluxoFinanceiroInfo.BAIXA = row["BAIXA"].ToString();
                    clsRptFluxoFinanceiroInfo.BANCO = row["BANCO"].ToString();
                    clsRptFluxoFinanceiroInfo.BANCONOM = row["BANCONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.BOLETO = row["BOLETO"].ToString();
                    clsRptFluxoFinanceiroInfo.BOLETONRO = clsParser.Int32Parse(row["BOLETONRO"].ToString());
                    clsRptFluxoFinanceiroInfo.CENTROCUSTO = row["CENTROCUSTO"].ToString();
                    clsRptFluxoFinanceiroInfo.CENTROCUSTONOM = row["CENTROCUSTONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.CHEGOU = row["CHEGOU"].ToString();
                    clsRptFluxoFinanceiroInfo.CLIENTE = row["CLIENTE"].ToString();
                    clsRptFluxoFinanceiroInfo.CONTA = row["CONTA"].ToString();
                    clsRptFluxoFinanceiroInfo.COORDENADOR = row["COORDENADOR"].ToString();
                    clsRptFluxoFinanceiroInfo.CREDITO = clsParser.DecimalParse(row["CREDITO"].ToString());
                    clsRptFluxoFinanceiroInfo.CTACONTABIL = row["CTACONTABIL"].ToString();
                    clsRptFluxoFinanceiroInfo.CENTROCUSTO = row["CENTROCUSTO"].ToString();
                    clsRptFluxoFinanceiroInfo.CENTROCUSTONOM = row["CENTROCUSTONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.CHEGOU = row["CHEGOU"].ToString();
                    clsRptFluxoFinanceiroInfo.CLIENTE = row["CLIENTE"].ToString();
                    clsRptFluxoFinanceiroInfo.CONTA = row["CONTA"].ToString();
                    clsRptFluxoFinanceiroInfo.COORDENADOR = row["COORDENADOR"].ToString();
                    clsRptFluxoFinanceiroInfo.CREDITO = clsParser.DecimalParse(row["CREDITO"].ToString());
                    clsRptFluxoFinanceiroInfo.CTACONTABIL = row["CTACONTABIL"].ToString();
                    clsRptFluxoFinanceiroInfo.CTACONTABILNOM = row["CTACONTABILNOM"].ToString();
                    clsRptFluxoFinanceiroInfo.DATALANCA = clsParser.DateTimeParse(row["DATALANCA"].ToString());
                    clsRptFluxoFinanceiroInfo.DEBITO = clsParser.DecimalParse(row["DEBITO"].ToString());
                    clsRptFluxoFinanceiroInfo.DESPESAPUBLICA = row["DESPESAPUBLICA"].ToString();
                    clsRptFluxoFinanceiroInfo.DOCUMENTO = row["DOCUMENTO"].ToString();
                    clsRptFluxoFinanceiroInfo.DUPLICATA = clsParser.Int32Parse(row["DUPLICATA"].ToString());
                    clsRptFluxoFinanceiroInfo.DV = clsParser.Int32Parse(row["DV"].ToString());
                    clsRptFluxoFinanceiroInfo.EMISSAO = clsParser.DateTimeParse(row["EMISSAO"].ToString());
                    clsRptFluxoFinanceiroInfo.EMITENTE = row["EMITENTE"].ToString();
                    clsRptFluxoFinanceiroInfo.FILIAL = clsParser.Int32Parse(row["FILIAL"].ToString());
                    clsRptFluxoFinanceiroInfo.FORMAPAGTO = row["FORMAPAGTO"].ToString();
                    clsRptFluxoFinanceiroInfo.FORMAPAGTONOM = row["FORMAPAGTONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.HISTORICO = row["HISTORICO"].ToString();
                    clsRptFluxoFinanceiroInfo.HISTORICONOM = row["HISTORICONOM"].ToString();
                    clsRptFluxoFinanceiroInfo.ID = clsParser.Int32Parse(row["ID"].ToString());
                    clsRptFluxoFinanceiroInfo.IDBANCO = clsParser.Int32Parse(row["IDBANCO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDBANCOINT = clsParser.Int32Parse(row["IDBANCOINT"].ToString());
                    clsRptFluxoFinanceiroInfo.IDCENTROCUSTO = clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDCLIENTE = clsParser.Int32Parse(row["IDCLIENTE"].ToString());
                    clsRptFluxoFinanceiroInfo.IDCODIGOCTABIL = clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString());
                    clsRptFluxoFinanceiroInfo.IDCOORDENADOR = clsParser.Int32Parse(row["IDCOORDENADOR"].ToString());
                    clsRptFluxoFinanceiroInfo.IDDOCUMENTO = clsParser.Int32Parse(row["IDDOCUMENTO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDFORMAPAGTO = clsParser.Int32Parse(row["IDFORMAPAGTO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDHISTORICO = clsParser.Int32Parse(row["IDHISTORICO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDNOTAFISCAL = clsParser.Int32Parse(row["IDNOTAFISCAL"].ToString());
                    clsRptFluxoFinanceiroInfo.IDNOTAPRINCIPAL = clsParser.Int32Parse(row["IDNOTAPRINCIPAL"].ToString());
                    clsRptFluxoFinanceiroInfo.IDSITBANCO = clsParser.Int32Parse(row["IDSITBANCO"].ToString());
                    clsRptFluxoFinanceiroInfo.IDSUPERVISOR = clsParser.Int32Parse(row["IDSUPERVISOR"].ToString());
                    clsRptFluxoFinanceiroInfo.IDVENDEDOR = clsParser.Int32Parse(row["IDVENDEDOR"].ToString());
                    clsRptFluxoFinanceiroInfo.IMPRIMIR = row["IMPRIMIR"].ToString();
                    clsRptFluxoFinanceiroInfo.NOME = row["NOME"].ToString();
                    clsRptFluxoFinanceiroInfo.NOTAFISCAL = row["NOTAFISCAL"].ToString();
                    clsRptFluxoFinanceiroInfo.OBSERVA = row["OBSERVA"].ToString();
                    clsRptFluxoFinanceiroInfo.POSICAO = clsParser.Int32Parse(row["POSICAO"].ToString());
                    clsRptFluxoFinanceiroInfo.POSICAOFIM = clsParser.Int32Parse(row["POSICAOFIM"].ToString());
                    clsRptFluxoFinanceiroInfo.SALDO = clsParser.DecimalParse(row["SALDO"].ToString());
                    clsRptFluxoFinanceiroInfo.SETOR = row["SETOR"].ToString();
                    clsRptFluxoFinanceiroInfo.SITBANCOCOD = row["SITBANCOCOD"].ToString();
                    clsRptFluxoFinanceiroInfo.SITUACAOCODIGO = row["SITUACAOCODIGO"].ToString();
                    clsRptFluxoFinanceiroInfo.SITUACAONOME = row["SITUACAONOME"].ToString();
                    clsRptFluxoFinanceiroInfo.SUPERVISOR = row["SUPERVISOR"].ToString();
                    clsRptFluxoFinanceiroInfo.TABELA = row["TABELA"].ToString();
                    clsRptFluxoFinanceiroInfo.VALOR = clsParser.DecimalParse(row["VALOR"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORARECEBER = clsParser.DecimalParse(row["VALORARECEBER"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORBAIXANDO = clsParser.DecimalParse(row["VALORBAIXANDO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORCOMISSAO = clsParser.DecimalParse(row["VALORCOMISSAO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORCOMISSAOGER = clsParser.DecimalParse(row["VALORCOMISSAOGER"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORCOMISSAOSUP = clsParser.DecimalParse(row["VALORCOMISSAOSUP"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORDESCONTO = clsParser.DecimalParse(row["VALORDESCONTO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORDEVOLVIDO = clsParser.DecimalParse(row["VALORDEVOLVIDO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORJUROS = clsParser.DecimalParse(row["VALORJUROS"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORLIQUIDO = clsParser.DecimalParse(row["VALORLIQUIDO"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORMULTA = clsParser.DecimalParse(row["VALORMULTA"].ToString());
                    clsRptFluxoFinanceiroInfo.VALORPAGO = clsParser.DecimalParse(row["VALORPAGO"].ToString());
                    clsRptFluxoFinanceiroInfo.VENCIMENTO = clsParser.DateTimeParse(row["VENCIMENTO"].ToString());
                    clsRptFluxoFinanceiroInfo.VENCIMENTOPREV = clsParser.DateTimeParse(row["VENCIMENTOPREV"].ToString());
                    clsRptFluxoFinanceiroInfo.VENDEDOR = row["VENDEDOR"].ToString();

                    clsRptFluxoFinanceiroInfo.ID = clsRptFluxoFinanceiroBLL.Incluir(clsRptFluxoFinanceiroInfo, clsInfo.conexaosqldados);
                }
            }
            // Imprimir Fluxo Financeiro
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = cabecalho;
            field.Name = "CABECALHO";
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            if (rbnFluDtVencimento.Checked == true)
            {   // Por Vencimento
                if (rbnFluAnalitica.Checked == true)  //Analitica
                {
                    if (rbnFluSemTotal.Checked == true)
                    {
                        // colocar modelo novo
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIRO.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "FLUXOFINANCEIRO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (rbnFluNormal.Checked == true)
                    {
                        // ja eh o novo rpt
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "FLUXOFINANCEIRO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                    }
                    else if (rbnFluSemanal.Checked == true)
                    {
                        //colocar modelo novo
                        MessageBox.Show("Acertar Relatorio - FLUXOFINANCEIRO_ANA_SEM.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "FLUXOFINANCEIRO_ANA_SEM.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                    }
                }
                else
                {
                    if (rbnFluNormal.Checked == true)
                    {
                        //colocar modelo novo
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIRO_SIN_DIA.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIRO_SIN_DIA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                    }
                    else
                    {
                        //colocar modelo novo
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIRO_SIN_SEM.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIRO_SIN_SEM.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);

                    }
                }
            }
            else
            { // Por Data de Previsão
                if (rbnFluAnalitica.Checked == true)  //Analitica
                {
                    if (rbnFluSemTotal.Checked == true)
                    {
                        //colocar modelo novo
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROPREV.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROPREV.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (rbnFluNormal.Checked == true)
                    {
                        //colocar modelo novo
                        //MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROPREV_ANA_DIA.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "FLUXOFINANCEIROPREV_DIA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else if (rbnFluSemanal.Checked == true)
                    {
                        //colocar modelo novo
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROPREV_ANA_SEM.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROPREV_ANA_SEM.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                }
                else
                {
                    if (rbnFluNormal.Checked == true)
                    {
                        //colocar modelo novo
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROPREV_SIN_DIA.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROPREV_SIN_DIA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        //colocar modelo novo
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROPREV_SIN_SEM.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROPREV_SIN_SEM.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                }


            }
        }

        private void tspFluRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnFluClienteDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvFluClienteDe";
            //frmClientePes frmClientePes = new frmClientePes();
            //frmClientePes.Init("Todos",  idFluClienteDe);

            //FormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnFluClienteAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvFluClienteAte";
            //frmClientePes frmClientePes = new frmClientePes();
            //frmClientePes.Init("Todos", idFluClienteAte);
            //FormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void rbnFluxoFinanceiro_Click(object sender, EventArgs e)
        {
            tclRelReceber.SelectedTab = tabFluxo;
            gbxReceberNormal.Visible = false;
            gbxRecebidasNormal.Visible = false;
            gbxFluxoOpcao.Visible = true;
            tclFluxo.Visible = true;

        }

        private void rbnReceberSimples_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbnRecebidasSimples_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void tspFluBcoImprimir_Click(object sender, EventArgs e)
        {
            //

            cabecalho = "";
            cabecalho = "Fluxo Financeiro [A Pagar/A Receber/Compras/Aplibank] ";

            if (rbnFluBcoDtVencimento.Checked == true)
            {
                cabecalho = cabecalho + " [ Data de Vencimento ]";
            }
            else
            {
                cabecalho = cabecalho + " [ Data de Previsão ]";
            }

            //Data Vencimento
            if (clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == false && clsParser.SqlDateTimeParse(tbxFluDtVencimentoAte.Text).IsNull == true)
            {
                cabecalho = cabecalho + Environment.NewLine + "Da data de : " + tbxFluBcoDtVencimentoDe.Text;
            }
            if (clsParser.SqlDateTimeParse(tbxFluDtVencimentoDe.Text).IsNull == true && clsParser.SqlDateTimeParse(tbxFluDtVencimentoAte.Text).IsNull == false)
            {
                cabecalho = cabecalho + Environment.NewLine + "Até a data de : " + tbxFluBcoDtVencimentoAte.Text;
            }
            // Titulos em Atraso A Pagar
            if (ckxFluBcoPagarAtraso.Checked == false)
            {
                cabecalho = cabecalho + Environment.NewLine + "Não foi Incluido Titulos em Atraso A Pagar ";
            }
            // Titulos em Atraso A Receber
            if (ckxFluBcoReceberAtraso.Checked == false)
            {
                cabecalho = cabecalho + Environment.NewLine + "Não foi Incluido Titulos em Atraso A Receber ";
            }
            
            if (rbnFilialBco.Checked == true)
            {
                cabecalho = cabecalho + Environment.NewLine + " Filial " + clsInfo.zfilial;
            }
            else
            {
                cabecalho = cabecalho + Environment.NewLine + " Todas as Filiais ";
            }

            String TipoData = "";
            if (rbnFluBcoDtVencimento.Checked == true)
            {
                TipoData = "V";  // data de vencimento
            }
            else
            {
                TipoData = "P";  // data de previsão
            }
            String BcoSim = "S";
            String PagarAtraso = "N";
            String ReceberAtraso = "N";
            if (ckxFluBcoPagarAtraso.Checked == true)
            {
                PagarAtraso = "S";
            }
            if (ckxFluBcoReceberAtraso.Checked == true)
            {
                ReceberAtraso = "S";
            }
            Int32 Filial = 0;
            if (rbnFilialBco.Checked == true)
            {
                Filial = clsInfo.zfilial;
            }

            DataTable dtFluxo = clsFinanceiro.FluxoFinanceiro(Filial, 0, "", TipoData, tbxFluBcoDtVencimentoDe.Text, tbxFluBcoDtVencimentoAte.Text,
                                                              clsParser.Int32Parse(tbxFluBcoCtaDa.Text), clsParser.Int32Parse(tbxFluBcoCtaAte.Text),
                                                              clsParser.Int32Parse(tbxFluBcoCtaNao01.Text),clsParser.Int32Parse(tbxFluBcoCtaNao02.Text),clsParser.Int32Parse(tbxFluBcoCtaNao03.Text),clsParser.Int32Parse(tbxFluBcoCtaNao04.Text),clsParser.Int32Parse(tbxFluBcoCtaNao05.Text),clsParser.Int32Parse(tbxFluBcoCtaNao06.Text),clsParser.Int32Parse(tbxFluBcoCtaNao07.Text),clsParser.Int32Parse(tbxFluBcoCtaNao08.Text),clsParser.Int32Parse(tbxFluBcoCtaNao09.Text),clsParser.Int32Parse(tbxFluBcoCtaNao10.Text),clsParser.Int32Parse(tbxFluBcoCtaNao11.Text),clsParser.Int32Parse(tbxFluBcoCtaNao12.Text),
                                                              BcoSim, PagarAtraso, ReceberAtraso,
                                                              clsParser.Int32Parse(tbxFluBcoDiasBom.Text), clsParser.Int32Parse(tbxFluBcoDiasMedio.Text), clsParser.Int32Parse(tbxFluBcoDiasRuim.Text));

            // Gravar no RPTFLUXOFINANCEIRO

            // Apagar o Arquivo RPT
            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;
            scn = new SqlConnection(clsInfo.conexaosqldados);

            scd = new SqlCommand("delete rptfluxofinanceiro ", scn);
            scn.Open();
            sdr = scd.ExecuteReader();
            scn.Close();

            foreach (DataRow row in dtFluxo.Rows)
            {
                clsRptFluxoFinanceiroInfo = new clsRptFluxoFinanceiroInfo();
                clsRptFluxoFinanceiroInfo.ATEVENCIMENTO = row["atevencimento"].ToString();
                clsRptFluxoFinanceiroInfo.BAIXA = row["BAIXA"].ToString();
                clsRptFluxoFinanceiroInfo.BANCO = row["BANCO"].ToString();
                clsRptFluxoFinanceiroInfo.BANCONOM = row["BANCONOM"].ToString();
                clsRptFluxoFinanceiroInfo.BOLETO = row["BOLETO"].ToString();
                clsRptFluxoFinanceiroInfo.BOLETONRO = clsParser.Int32Parse(row["BOLETONRO"].ToString());
                clsRptFluxoFinanceiroInfo.CENTROCUSTO = row["CENTROCUSTO"].ToString();
                clsRptFluxoFinanceiroInfo.CENTROCUSTONOM = row["CENTROCUSTONOM"].ToString();
                clsRptFluxoFinanceiroInfo.CHEGOU = row["CHEGOU"].ToString();
                clsRptFluxoFinanceiroInfo.CLIENTE = row["CLIENTE"].ToString();
                clsRptFluxoFinanceiroInfo.CONTA = row["CONTA"].ToString();
                clsRptFluxoFinanceiroInfo.COORDENADOR = row["COORDENADOR"].ToString();
                clsRptFluxoFinanceiroInfo.CREDITO = clsParser.DecimalParse(row["CREDITO"].ToString());
                clsRptFluxoFinanceiroInfo.CTACONTABIL = row["CTACONTABIL"].ToString();
                clsRptFluxoFinanceiroInfo.CENTROCUSTO = row["CENTROCUSTO"].ToString();
                clsRptFluxoFinanceiroInfo.CENTROCUSTONOM = row["CENTROCUSTONOM"].ToString();
                clsRptFluxoFinanceiroInfo.CHEGOU = row["CHEGOU"].ToString();
                clsRptFluxoFinanceiroInfo.CLIENTE = row["CLIENTE"].ToString();
                clsRptFluxoFinanceiroInfo.CONTA = row["CONTA"].ToString();
                clsRptFluxoFinanceiroInfo.COORDENADOR = row["COORDENADOR"].ToString();
                clsRptFluxoFinanceiroInfo.CREDITO = clsParser.DecimalParse(row["CREDITO"].ToString());
                clsRptFluxoFinanceiroInfo.CTACONTABIL = row["CTACONTABIL"].ToString();
                clsRptFluxoFinanceiroInfo.CTACONTABILNOM = row["CTACONTABILNOM"].ToString();
                clsRptFluxoFinanceiroInfo.DATALANCA = clsParser.DateTimeParse(row["DATALANCA"].ToString());
                clsRptFluxoFinanceiroInfo.DEBITO = clsParser.DecimalParse(row["DEBITO"].ToString());
                clsRptFluxoFinanceiroInfo.DESPESAPUBLICA = row["DESPESAPUBLICA"].ToString();
                clsRptFluxoFinanceiroInfo.DOCUMENTO = row["DOCUMENTO"].ToString();
                clsRptFluxoFinanceiroInfo.DUPLICATA = clsParser.Int32Parse(row["DUPLICATA"].ToString());
                clsRptFluxoFinanceiroInfo.DV = clsParser.Int32Parse(row["DV"].ToString());
                clsRptFluxoFinanceiroInfo.EMISSAO = clsParser.DateTimeParse(row["EMISSAO"].ToString());
                clsRptFluxoFinanceiroInfo.EMITENTE = row["EMITENTE"].ToString();
                clsRptFluxoFinanceiroInfo.FILIAL = clsParser.Int32Parse(row["FILIAL"].ToString());
                clsRptFluxoFinanceiroInfo.FORMAPAGTO = row["FORMAPAGTO"].ToString();
                clsRptFluxoFinanceiroInfo.FORMAPAGTONOM = row["FORMAPAGTONOM"].ToString();
                clsRptFluxoFinanceiroInfo.HISTORICO = row["HISTORICO"].ToString();
                clsRptFluxoFinanceiroInfo.HISTORICONOM = row["HISTORICONOM"].ToString();
                clsRptFluxoFinanceiroInfo.ID = clsParser.Int32Parse(row["ID"].ToString());
                clsRptFluxoFinanceiroInfo.IDBANCO = clsParser.Int32Parse(row["IDBANCO"].ToString());
                clsRptFluxoFinanceiroInfo.IDBANCOINT = clsParser.Int32Parse(row["IDBANCOINT"].ToString());
                clsRptFluxoFinanceiroInfo.IDCENTROCUSTO = clsParser.Int32Parse(row["IDCENTROCUSTO"].ToString());
                clsRptFluxoFinanceiroInfo.IDCLIENTE = clsParser.Int32Parse(row["IDCLIENTE"].ToString());
                clsRptFluxoFinanceiroInfo.IDCODIGOCTABIL = clsParser.Int32Parse(row["IDCODIGOCTABIL"].ToString());
                clsRptFluxoFinanceiroInfo.IDCOORDENADOR = clsParser.Int32Parse(row["IDCOORDENADOR"].ToString());
                clsRptFluxoFinanceiroInfo.IDDOCUMENTO = clsParser.Int32Parse(row["IDDOCUMENTO"].ToString());
                clsRptFluxoFinanceiroInfo.IDFORMAPAGTO = clsParser.Int32Parse(row["IDFORMAPAGTO"].ToString());
                clsRptFluxoFinanceiroInfo.IDHISTORICO = clsParser.Int32Parse(row["IDHISTORICO"].ToString());
                clsRptFluxoFinanceiroInfo.IDNOTAFISCAL = clsParser.Int32Parse(row["IDNOTAFISCAL"].ToString());
                clsRptFluxoFinanceiroInfo.IDNOTAPRINCIPAL = clsParser.Int32Parse(row["IDNOTAPRINCIPAL"].ToString());
                clsRptFluxoFinanceiroInfo.IDSITBANCO = clsParser.Int32Parse(row["IDSITBANCO"].ToString());
                clsRptFluxoFinanceiroInfo.IDSUPERVISOR = clsParser.Int32Parse(row["IDSUPERVISOR"].ToString());
                clsRptFluxoFinanceiroInfo.IDVENDEDOR = clsParser.Int32Parse(row["IDVENDEDOR"].ToString());
                clsRptFluxoFinanceiroInfo.IMPRIMIR = row["IMPRIMIR"].ToString();
                clsRptFluxoFinanceiroInfo.NOME = row["NOME"].ToString();
                clsRptFluxoFinanceiroInfo.NOTAFISCAL = row["NOTAFISCAL"].ToString();
                clsRptFluxoFinanceiroInfo.OBSERVA = row["OBSERVA"].ToString();
                clsRptFluxoFinanceiroInfo.POSICAO = clsParser.Int32Parse(row["POSICAO"].ToString());
                clsRptFluxoFinanceiroInfo.POSICAOFIM = clsParser.Int32Parse(row["POSICAOFIM"].ToString());
                clsRptFluxoFinanceiroInfo.SALDO = clsParser.DecimalParse(row["SALDO"].ToString());
                clsRptFluxoFinanceiroInfo.SETOR = row["SETOR"].ToString();
                clsRptFluxoFinanceiroInfo.SITBANCOCOD = row["SITBANCOCOD"].ToString();
                clsRptFluxoFinanceiroInfo.SITUACAOCODIGO = row["SITBANCOCOD"].ToString();
                clsRptFluxoFinanceiroInfo.SITUACAONOME = row["SITUACAONOME"].ToString();
                clsRptFluxoFinanceiroInfo.SUPERVISOR = row["SUPERVISOR"].ToString();
                clsRptFluxoFinanceiroInfo.TABELA = row["TABELA"].ToString();
                clsRptFluxoFinanceiroInfo.VALOR = clsParser.DecimalParse(row["VALOR"].ToString());
                clsRptFluxoFinanceiroInfo.VALORARECEBER = clsParser.DecimalParse(row["VALORARECEBER"].ToString());
                clsRptFluxoFinanceiroInfo.VALORBAIXANDO = clsParser.DecimalParse(row["VALORBAIXANDO"].ToString());
                clsRptFluxoFinanceiroInfo.VALORCOMISSAO = clsParser.DecimalParse(row["VALORCOMISSAO"].ToString());
                clsRptFluxoFinanceiroInfo.VALORCOMISSAOGER = clsParser.DecimalParse(row["VALORCOMISSAOGER"].ToString());
                clsRptFluxoFinanceiroInfo.VALORCOMISSAOSUP = clsParser.DecimalParse(row["VALORCOMISSAOSUP"].ToString());
                clsRptFluxoFinanceiroInfo.VALORDESCONTO = clsParser.DecimalParse(row["VALORDESCONTO"].ToString());
                //clsRptFluxoFinanceiroInfo.VALORDEVOLVIDO = clsParser.DecimalParse(row["VALORDEVOLVIDO"].ToString());
                clsRptFluxoFinanceiroInfo.VALORJUROS = clsParser.DecimalParse(row["VALORJUROS"].ToString());
                clsRptFluxoFinanceiroInfo.VALORLIQUIDO = clsParser.DecimalParse(row["VALORLIQUIDO"].ToString());
                clsRptFluxoFinanceiroInfo.VALORMULTA = clsParser.DecimalParse(row["VALORMULTA"].ToString());
                clsRptFluxoFinanceiroInfo.VALORPAGO = clsParser.DecimalParse(row["VALORPAGO"].ToString());
                clsRptFluxoFinanceiroInfo.VENCIMENTO = clsParser.DateTimeParse(row["VENCIMENTO"].ToString());
                clsRptFluxoFinanceiroInfo.VENCIMENTOPREV = clsParser.DateTimeParse(row["VENCIMENTOPREV"].ToString());
                clsRptFluxoFinanceiroInfo.VENDEDOR = row["VENDEDOR"].ToString();

                clsRptFluxoFinanceiroInfo.ID = clsRptFluxoFinanceiroBLL.Incluir(clsRptFluxoFinanceiroInfo, clsInfo.conexaosqldados);
            }



            // Contas Especiais apenas para aparecer no cabeçalho  [ Passar como Parametro]
            // Pegar os Saldos
            /////////////////// Incluir os Saldos dos Bancos Apibank
            Int32 idFluxo = 0;
            sql1 = "";
            sql1 = "select (select top 1 fluxo.id from fluxo where fluxo.conta = bancos.id and fluxo.datadeposito < getdate() " +
                    "order by fluxo.datadeposito desc, fluxo.id desc) as idfluxo, BANCOS.CONTA, BANCOS.NOME " +
                //(select top 1 fluxo.NUMERO from fluxo where fluxo.conta = bancos.id and fluxo.datadeposito < getdate() order by fluxo.id desc) as NUMERO,
                //(select top 1 fluxo.DATADEPOSITO from fluxo where fluxo.conta = bancos.id and fluxo.datadeposito < getdate() order by fluxo.id desc) as datadeposito,
                //(select top 1 fluxo.saldo from fluxo where fluxo.conta = bancos.id and fluxo.datadeposito < getdate() order by fluxo.id desc) as saldodacta
                   "from bancos order by bancos.conta";

            cabecalho = cabecalho + Environment.NewLine;
            if (clsParser.Int32Parse(tbxFluBcoCtaCab01.Text) > 0 )
            {
                cabecalho = cabecalho.PadRight(300, '-') + Environment.NewLine;
            }
            Int32 x = 0;
            Int32 y = cabecalho.Length;

            scn = new SqlConnection(clsInfo.conexaosqlbanco);
            scn.Open();
            scd = new SqlCommand(sql1, scn);
            sdr = scd.ExecuteReader();
            while (sdr.Read() == true)
            {
                bok = false;
                if (clsParser.Int32Parse(sdr["CONTA"].ToString()) > 0)
                {
                    // Verificar se a Conta não deve aparecer
                    if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == clsParser.Int32Parse(tbxFluBcoCtaCab01.Text)) { bok = true; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == clsParser.Int32Parse(tbxFluBcoCtaCab02.Text)) { bok = true; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == clsParser.Int32Parse(tbxFluBcoCtaCab03.Text)) { bok = true; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == clsParser.Int32Parse(tbxFluBcoCtaCab04.Text)) { bok = true; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == clsParser.Int32Parse(tbxFluBcoCtaCab05.Text)) { bok = true; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == clsParser.Int32Parse(tbxFluBcoCtaCab06.Text)) { bok = true; };

                    if (bok == true)
                    {
                        //  puxar o id do fluxo que possui o ultimo saldo
                        idFluxo = clsParser.Int32Parse(sdr["IDFLUXO"].ToString());
                        x = x + 1;

                        cabecalho = cabecalho + sdr["CONTA"].ToString() + " = " + sdr["NOME"].ToString() + " ( " + clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select SALDO from FLUXO where ID = " + idFluxo, "0").ToString()).ToString("N2") + " ) ";
                        //cabecalho = cabecalho + sdr["CONTA"].ToString() + " = " + sdr["NOME"].ToString() + " ( " + clsParser.DecimalParse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "FLUXO", "SALDO", "ID", idFluxo).ToString()).ToString("N2") + " ) ";
                        if (x == 1)
                        {
                            cabecalho = cabecalho.PadRight(y + 40, ' ');
                        }
                        else if (x == 2)
                        {
                            cabecalho = cabecalho.PadRight(y + 80, ' ') + Environment.NewLine;
                        }
                        else if (x == 3)
                        {
                            cabecalho = cabecalho.PadRight(y + 120, ' ');
                        }
                        else if (x == 4)
                        {
                            cabecalho = cabecalho.PadRight(y + 160, ' ') + Environment.NewLine;
                        }
                        else if (x == 5)
                        {
                            cabecalho = cabecalho.PadRight(y + 200, ' ');
                        }
                        else
                        {
                            cabecalho = cabecalho.PadRight(y + 240, ' ') + Environment.NewLine;
                        }
                    }
                }
            }
            /////////////////// Termino de Incluir o Saldo Aplibank no Fluxo
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = cabecalho;
            field.Name = "CABECALHO";
            field.CurrentValues.Add(valor);
            parameters.Add(field);


            if (rbnFluBcoDtVencimento.Checked == true)
            {  // Data de Vencimento
                if (rbnFluBcoTipoAna.Checked == true)
                {
                    if (rbnFluBcoSemTotal.Checked == true)
                    {
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCO.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        if (rbnFluBcoDia.Checked == true)
                        {
//                            MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCO_TOTALDIA.RPT");
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCO_TOTALDIA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCO_TOTALSEMANA.RPT");
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCO_TOTALSEMANA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }

                    }
                }
                else
                { // sintetica
                    if (rbnFluBcoDia.Checked == true)
                    {
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCO_SIN_TOTALDIA.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCO_SIN_TOTALDIA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCO_SIN_TOTALSEMANA.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCO_SIN_TOTALSEMANA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }

                }
            }
            else
            { // Data de Previsão
                if (rbnFluBcoTipoAna.Checked == true)
                {
                    if (rbnFluBcoSemTotal.Checked == true)
                    {
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCOPREV.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCOPREV.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        if (rbnFluBcoDia.Checked == true)
                        {
                            MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCOPREV_TOTALDIA.RPT");
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCOPREV_TOTALDIA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCOPREV_TOTALSEMANA.RPT");
                            frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCOPREV_TOTALSEMANA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                            clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                        }

                    }
                }

                else
                { // sintetica
                    if (rbnFluBcoDia.Checked == true)
                    {
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCOPREV_SIN_TOTALDIA.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCOPREV_SIN_TOTALDIA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        MessageBox.Show("Acertar Relatorio - DADOS_FLUXOFINANCEIROBCOPREV_SIN_TOTALSEMANA.RPT");
                        frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_FLUXOFINANCEIROBCOPREV_SIN_TOTALSEMANA.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                        clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
                    }

                }


            }

        }
        private void tspFluBcoRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void tclFluxo_Click(object sender, EventArgs e)
        {
            tclFluxo.Visible = true;
            tclFluxo.SelectedTab = tabFluxoSimples;
            gbxReceberNormal.Visible = false;
            gbxRecebidasNormal.Visible = false;
            gbxFluxoOpcao.Visible = true;
            gbxFluxoSimples.Visible = true;

        }

        private void rbnFluxoAplibank_Click(object sender, EventArgs e)
        {
            if (clsInfo.zempresacliente_cognome.ToUpper().IndexOf("ACASACORREA") != -1)
            {
                tbxFluBcoCtaNao01.Text = "900";
                tbxFluBcoCtaNao02.Text = "901";
                tbxFluBcoCtaNao03.Text = "991";
                tbxFluBcoCtaNao04.Text = "995";
                tbxFluBcoCtaNao05.Text = "999";
                tbxFluBcoCtaCab01.Text = "1";
                tbxFluBcoCtaCab02.Text = "10";
                tbxFluBcoCtaCab03.Text = "11";
                tbxFluBcoCtaCab04.Text = "12";
                
            }
           

            tclFluxo.Visible = true;
            tclFluxo.SelectedTab = tabFluxoAplibank;
            gbxReceberNormal.Visible = false;
            gbxRecebidasNormal.Visible = false;
            gbxFluxoOpcao.Visible = true;
            gbxFluxoBanco.Visible = true;

        }
        private void btnFluBcoCtaCab01_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFluBcoCtaCab01.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBcoCtaCab01,clsInfo.conexaosqlbanco);

             clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }
        private void btnFluBcoCtaCab02_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFluBcoCtaCab02.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBcoCtaCab02,clsInfo.conexaosqlbanco);

             clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }
        private void btnFluBcoCtaCab03_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFluBcoCtaCab03.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBcoCtaCab03,clsInfo.conexaosqlbanco);

             clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }
        private void btnFluBcoCtaCab04_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFluBcoCtaCab04.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBcoCtaCab04,clsInfo.conexaosqlbanco);

             clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }

        private void btnFluBcoCtaCab05_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFluBcoCtaCab05.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBcoCtaCab05, clsInfo.conexaosqlbanco);

             clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }
        private void btnFluBcoCtaCab06_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFluBcoCtaCab06.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idBcoCtaCab06, clsInfo.conexaosqlbanco);

             clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }

        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                 clsInfo.zrow.Index != -1 &&
                 clsInfo.znomegrid != null &&
                 clsInfo.znomegrid != "")
            {
                //inicio buscas para o Aba Ctas Receber Normal
                //cliente de
                if (clsInfo.znomegrid == btnNorClienteDe.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        idNorClienteDe = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxNorClienteDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID = " + idNorClienteDe, "").ToString();
                        //tbxNorClienteDe.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idNorClienteDe).ToString();
                    }
                    tbxNorClienteDe.Select();
                }
                //cliente ate
                if (clsInfo.znomegrid == btnNorClienteAte.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        idNorClienteAte = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxNorClienteDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID = " + idNorClienteDe, "").ToString();
                        //tbxNorClienteDe.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idNorClienteDe).ToString();
                        tbxNorClienteAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID = " + idNorClienteAte, "").ToString();
                        //tbxNorClienteAte.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idNorClienteAte).ToString();
                    }
                    tbxNorClienteAte.Select();
                }
                //situação do titulo de
                if (clsInfo.znomegrid == btnNorSituacaoDe.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        idNorSituacaoDe = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxNorSituacaoDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOTITULO where ID = " + idNorSituacaoDe, "").ToString();
                        //tbxNorSituacaoDe.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "CODIGO", "ID", idNorSituacaoDe).ToString();
                    }
                    tbxNorSituacaoDe.Select();
                }
                //situação do titulo ate
                if (clsInfo.znomegrid == btnNorSituacaoAte.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        idNorSituacaoAte = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxNorSituacaoAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from SITUACAOTITULO where ID = " + idNorSituacaoAte, "").ToString();
                        //tbxNorSituacaoAte.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "CODIGO", "ID", idNorSituacaoAte).ToString();
                    }
                    tbxNorSituacaoAte.Select();
                }
                //banco de
                if (clsInfo.znomegrid == btnNorBancoDe.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        idNorBancoDe = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxNorBancoDe.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                    }
                    tbxNorBancoDe.Select();
                }
                //banco ate
                if (clsInfo.znomegrid == btnNorBancoAte.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        idNorBancoAte = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxNorBancoAte.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                    }
                    tbxNorBancoAte.Select();
                }
                //forma de pagamento de 
                if (clsInfo.znomegrid == btnFormaPagtoDe.Name)
                {
                    if (clsInfo.zrow != null)
                    {

                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            idFormapagtoDe = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                            tbxFormaPagtoDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                            tbxFormaPagtoDe.Text += " = " + clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                            tbxFormaPagtoDe.Select();
                            tbxFormaPagtoDe.SelectAll();
                        }
                    }
                }
                //forma de pagamento ate
                if (clsInfo.znomegrid == btnFormaPagtoAte.Name)
                {
                    idFormapagtoAte = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM SITUACAOTIPOTITULO where CODIGO='" + tbxFormaPagtoAte.Text.Substring(0, 2) + "' "));
                    if (idFormapagtoAte == 0)
                    {
                        idFormapagtoAte = clsInfo.zformapagto;
                    }
                    tbxFormaPagtoAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO where id=" + idFormapagtoAte,"");
                    tbxFormaPagtoAte.Text += " = " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM SITUACAOTIPOTITULO where id=" + idFormapagtoAte,"");
                }
                if (clsInfo.znomegrid == btnNorVendedor.Name)
                {
                    idNorVendedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxNorVendedor.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID = " + idNorVendedor, "").ToString();
                    tbxNorVendedor.Select();
                }
                if (clsInfo.znomegrid == btnReciVendedor.Name)
                {
                    idNorVendedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxRecNorVendedor.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID = " + idNorVendedor, "").ToString();
                    tbxRecNorVendedor.Select();
                }

                

                //fim buscas para o Aba Ctas Receber Normal

                // inicio buscas para Aba de Ctas Recebidas Normal
                // Clientes de / ate
                if (clsInfo.znomegrid == btnRecNorFornecedorDe.Name)
                {
                    idRecNorFornecedorDe = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxRecNorClienteDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID = " + idRecNorFornecedorDe, "").ToString();
                }
                if (clsInfo.znomegrid == btnRecNorFornecedorAte.Name)
                {
                    idRecNorFornecedorAte = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxRecNorClienteAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID = " + idRecNorFornecedorAte, "").ToString();
                }
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


                //inicio buscas para o Aba Fluxo Financeiro
                //cliente de
                if (clsInfo.znomegrid == "dgvFluClienteDe")
                {
                    if (clsInfo.zrow != null)
                    {
                        idFluClienteDe = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxFluClienteDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID = " + idFluClienteDe, "").ToString();
                        //tbxFluClienteDe.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idFluClienteDe).ToString();
                    }
                    tbxFluClienteDe.Select();
                }
                //cliente ate
                if (clsInfo.znomegrid == "dgvFluClienteAte")
                {
                    if (clsInfo.zrow != null)
                    {
                        idFluClienteAte = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxFluClienteAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID = " + idFluClienteAte, "").ToString();
                        //tbxFluClienteAte.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "CLIENTE", "COGNOME", "ID", idFluClienteAte).ToString();
                    }
                    tbxFluClienteAte.Select();
                }
                tbxFluBcoCtaNao01.Text = clsParser.Int32Parse(tbxFluBcoCtaNao01.Text).ToString();
                tbxFluBcoCtaNao02.Text = clsParser.Int32Parse(tbxFluBcoCtaNao02.Text).ToString();
                tbxFluBcoCtaNao03.Text = clsParser.Int32Parse(tbxFluBcoCtaNao03.Text).ToString();
                tbxFluBcoCtaNao04.Text = clsParser.Int32Parse(tbxFluBcoCtaNao04.Text).ToString();
                tbxFluBcoCtaNao05.Text = clsParser.Int32Parse(tbxFluBcoCtaNao05.Text).ToString();
                tbxFluBcoCtaNao06.Text = clsParser.Int32Parse(tbxFluBcoCtaNao06.Text).ToString();
                tbxFluBcoCtaNao07.Text = clsParser.Int32Parse(tbxFluBcoCtaNao07.Text).ToString();
                tbxFluBcoCtaNao08.Text = clsParser.Int32Parse(tbxFluBcoCtaNao08.Text).ToString();
                tbxFluBcoCtaNao09.Text = clsParser.Int32Parse(tbxFluBcoCtaNao09.Text).ToString();
                tbxFluBcoCtaNao10.Text = clsParser.Int32Parse(tbxFluBcoCtaNao10.Text).ToString();
                tbxFluBcoCtaNao11.Text = clsParser.Int32Parse(tbxFluBcoCtaNao11.Text).ToString();
                tbxFluBcoCtaNao12.Text = clsParser.Int32Parse(tbxFluBcoCtaNao12.Text).ToString();

                tbxFluBcoCtaCab01.Text = clsParser.Int32Parse(tbxFluBcoCtaCab01.Text).ToString();
                tbxFluBcoCtaCab02.Text = clsParser.Int32Parse(tbxFluBcoCtaCab02.Text).ToString();
                tbxFluBcoCtaCab03.Text = clsParser.Int32Parse(tbxFluBcoCtaCab03.Text).ToString();
                tbxFluBcoCtaCab04.Text = clsParser.Int32Parse(tbxFluBcoCtaCab04.Text).ToString();
                tbxFluBcoCtaCab05.Text = clsParser.Int32Parse(tbxFluBcoCtaCab05.Text).ToString();
                tbxFluBcoCtaCab06.Text = clsParser.Int32Parse(tbxFluBcoCtaCab06.Text).ToString();


                tbxFluBcoDiasBom.Text = clsParser.Int32Parse(tbxFluBcoDiasBom.Text).ToString();
                tbxFluBcoDiasMedio.Text = clsParser.Int32Parse(tbxFluBcoDiasMedio.Text).ToString();
                tbxFluBcoDiasRuim.Text = clsParser.Int32Parse(tbxFluBcoDiasRuim.Text).ToString();


                if (ctl.Name == rbnReceberSimples.Name)
                {
                    if (rbnNorDtVencimento.Checked == true)
                    {
                        rbnNorDtVencimento.Select();
                    }
                    else
                    {
                        rbnNorDtPrevisao.Select();
                    }
                }
                // Fluxo Financeiro Banco
                if (ctl.Name == tbxFluBcoDtVencimentoDe.Name)
                {
                    if (DateTime.Parse(tbxFluBcoDtVencimentoDe.Text).ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                    {
                        tbxFluBcoDtVencimentoDe.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                }
                if (ctl.Name == tbxFluBcoDtVencimentoAte.Name)
                {
                    if (DateTime.Parse(tbxFluBcoDtVencimentoAte.Text).Date < DateTime.Parse(tbxFluBcoDtVencimentoDe.Text).Date)
                    {
                        tbxFluBcoDtVencimentoAte.Text = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
                    }

                }
                if (ctl.Name == tbxFluBcoCtaDa.Name)
                {
                    tbxFluBcoCtaDa.Text = clsParser.Int32Parse(tbxFluBcoCtaDa.Text).ToString();
                }

                if (ctl.Name == tbxFluBcoCtaAte.Name)
                {
                    tbxFluBcoCtaAte.Text = clsParser.Int32Parse(tbxFluBcoCtaAte.Text).ToString();
                    if (clsParser.Int32Parse(tbxFluBcoCtaDa.Text) > clsParser.Int32Parse(tbxFluBcoCtaAte.Text))
                    {
                        tbxFluBcoCtaAte.Text = clsParser.Int32Parse(tbxFluBcoCtaDa.Text).ToString();
                    }
                }
                //
                if (ctl.Name == tbxFluBcoCtaNao01.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao01.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao01.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao02.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao02.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao03.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao03.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao03.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao03.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao04.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao04.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao04.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao05.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao05.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao05.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao06.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao06.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao06.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao07.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao07.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao07.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao08.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao08.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao08.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao09.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao09.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao09.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao10.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao10.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao10.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao11.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao11.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao11.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                if (ctl.Name == tbxFluBcoCtaNao12.Name)
                {
                    Int32 idBcoCta = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaNao12.Text + "'"));
                    if (idBcoCta == 0)
                    {
                        idBcoCta = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaNao12.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCta + "");
                }
                //
                if (clsInfo.znomegrid == btnFluBcoCtaCab01.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            tbxFluBcoCtaCab01.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                            tbxFluBcoCtaCab01.Select();
                        }
                    }
                    tbxFluBcoCtaCab01.Select();
                }
                if (ctl.Name == tbxFluBcoCtaCab01.Name)
                {
                    idBcoCtaCab01 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaCab01.Text + "'"));
                    if (idBcoCtaCab01 == 0)
                    {
                        idBcoCtaCab01 = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaCab01.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCtaCab01 + "");
                }
                if (clsInfo.znomegrid == btnFluBcoCtaCab02.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            tbxFluBcoCtaCab02.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                            tbxFluBcoCtaCab02.Select();
                        }
                    }
                    tbxFluBcoCtaCab02.Select();
                }
                if (ctl.Name == tbxFluBcoCtaCab02.Name)
                {
                    idBcoCtaCab02 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaCab02.Text + "'"));
                    if (idBcoCtaCab02 == 0)
                    {
                        idBcoCtaCab02 = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaCab02.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCtaCab02 + "");
                }
                if (clsInfo.znomegrid == btnFluBcoCtaCab03.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            tbxFluBcoCtaCab03.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                            tbxFluBcoCtaCab03.Select();
                        }
                    }
                    tbxFluBcoCtaCab03.Select();
                }
                if (ctl.Name == tbxFluBcoCtaCab03.Name)
                {
                    idBcoCtaCab03 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaCab03.Text + "'"));
                    if (idBcoCtaCab03 == 0)
                    {
                        idBcoCtaCab03 = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaCab03.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCtaCab03 + "");
                }
                if (clsInfo.znomegrid == btnFluBcoCtaCab04.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            tbxFluBcoCtaCab04.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                            tbxFluBcoCtaCab04.Select();
                        }
                    }
                    tbxFluBcoCtaCab04.Select();
                }
                if (ctl.Name == tbxFluBcoCtaCab04.Name)
                {
                    idBcoCtaCab04 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaCab04.Text + "'"));
                    if (idBcoCtaCab04 == 0)
                    {
                        idBcoCtaCab04 = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaCab04.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCtaCab04 + "");
                }
                if (clsInfo.znomegrid == btnFluBcoCtaCab05.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            tbxFluBcoCtaCab05.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                            tbxFluBcoCtaCab05.Select();
                        }
                    }
                    tbxFluBcoCtaCab05.Select();
                }
                if (ctl.Name == tbxFluBcoCtaCab05.Name)
                {
                    idBcoCtaCab05 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaCab05.Text + "'"));
                    if (idBcoCtaCab05 == 0)
                    {
                        idBcoCtaCab05 = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaCab05.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCtaCab05 + "");
                }
                if (clsInfo.znomegrid == btnFluBcoCtaCab06.Name)
                {
                    if (clsInfo.zrow != null)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            tbxFluBcoCtaCab06.Text = clsInfo.zrow.Cells["CONTA"].Value.ToString().Trim();
                            tbxFluBcoCtaCab06.Select();
                        }
                    }
                    tbxFluBcoCtaCab06.Select();
                }
                if (ctl.Name == tbxFluBcoCtaCab06.Name)
                {
                    idBcoCtaCab06 = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA='" + tbxFluBcoCtaCab06.Text + "'"));
                    if (idBcoCtaCab06 == 0)
                    {
                        idBcoCtaCab06 = clsInfo.zbancoint;
                    }
                    tbxFluBcoCtaCab06.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idBcoCtaCab06 + "");
                }
                //fim buscas para o Aba Fluxo Financeiro
            }
            else
            {
                // ###############################
                // Verifica os campos de pesquisa
                // ###############################

                //Cliente
                if (ctl.Name == tbxNorClienteDe.Name)    // 
                {
                    if (tbxNorClienteDe.Text.Length > 0 && tbxNorClienteAte.Text.Length <= 0)
                    {
                        tbxNorClienteAte.Text = tbxNorClienteDe.Text;
                    }
                }
                //emissão
                if (ctl.Name == tbxNorDtEmissaoDe.Name)    // 
                {
                    if (tbxNorDtEmissaoDe.Text.Length > 0 && tbxNorDtEmissaoAte.Text.Length <= 0)
                    {
                        tbxNorDtEmissaoAte.Text = tbxNorDtEmissaoDe.Text;
                    }
                }
                //Vencimento
                if (ctl.Name == tbxNorDtVencimentoDe.Name)    // 
                {
                    if (tbxNorDtVencimentoDe.Text.Length > 0 && tbxNorDtVencimentoAte.Text.Length <= 0)
                    {
                        tbxNorDtVencimentoAte.Text = tbxNorDtVencimentoDe.Text;
                    }
                } 
                //Previsão
                if (ctl.Name == tbxNorDtPrevisaoDe.Name)    // 
                {
                    if (tbxNorDtPrevisaoDe.Text.Length > 0 && tbxNorDtPrevisaoAte.Text.Length <= 0)
                    {
                        tbxNorDtPrevisaoAte.Text = tbxNorDtPrevisaoDe.Text;
                    }
                }
                //Situação do titulo
                if (ctl.Name == tbxNorSituacaoDe.Name)    // 
                {
                    if (tbxNorSituacaoDe.Text.Length > 0 && tbxNorSituacaoAte.Text.Length <= 0)
                    {
                        tbxNorSituacaoAte.Text = tbxNorSituacaoDe.Text;
                    }
                } 
                 //Banco
                if (ctl.Name == tbxNorBancoDe.Name)    // 
                {
                    if (tbxNorBancoDe.Text.Length > 0 && tbxNorBancoAte.Text.Length <= 0)
                    {
                        tbxNorBancoAte.Text = tbxNorBancoDe.Text;
                    }
                } 
                //Forma de pagamento
                if (ctl.Name == tbxFormaPagtoDe.Name)    // 
                {
                    if (tbxFormaPagtoDe.Text.Length > 0 && tbxFormaPagtoAte.Text.Length <= 0)
                    {
                        tbxFormaPagtoAte.Text = tbxFormaPagtoDe.Text;
                    }
                } 
            }
            tbxPrevisaoDiaria.Text = clsParser.DecimalParse(tbxPrevisaoDiaria.Text).ToString("N2");
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }

        private void ckxFluBcoPagarAtraso_Click(object sender, EventArgs e)
        {
            if (ckxFluBcoPagarAtraso.Checked == true)
            {
                ckxFluBcoPagarAtraso.Text = "Sim Incluir o Contas a Pagar em Atraso nos Calculos";
            }
            else
            {
                ckxFluBcoPagarAtraso.Text = "Não Incluir o Contas a Pagar em Atraso nos Calculos";
            }

        }

        private void ckxFluBcoReceberAtraso_Click(object sender, EventArgs e)
        {
            if (ckxFluBcoReceberAtraso.Checked == true)
            {
                ckxFluBcoReceberAtraso.Text = "Sim Incluir o Contas a Receber em Atraso nos Calculos";
            }
            else
            {
                ckxFluBcoReceberAtraso.Text = "Não Incluir o Contas a Receber em Atraso nos Calculos";
            }

        }

        private void btnRecNorFornecedorDe_Click(object sender, EventArgs e)
        {
            //clsInfo.znomegrid = btnRecNorFornecedorDe.Name;
            //frmClientePes frmClientePes = new frmClientePes();
            //frmClientePes.Init("Todos", idNorClienteDe);

            // clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void gbxRelReceber_Enter(object sender, EventArgs e)
        {

        }

        private void btnFormaPagtoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFormaPagtoDe.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", idFormapagtoDe, "Situacao de Titulo");

             clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void btnFormaPagtoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnFormaPagtoAte.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", idFormapagtoAte, "Situação de Titulo");

             clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }

        private void rbnNorDtPrevisao_Vencimento_Changed(object sender, EventArgs e)
        {
            if(rbnNorDtVencimento.Checked ==true)
            {
                lbxNorOrdem.Items.Clear();                  
                lbxNorOrdem.Items.Add("Por Data Vencimento + Cliente");
                lbxNorOrdem.Items.Add("Por Cliente + Data de Vencimento");
                lbxNorOrdem.Items.Add("Por Numero de Duplicata");
                lbxNorOrdem.SelectedIndex = 0;
            }
            else
            {
                lbxNorOrdem.Items.Clear();
                lbxNorOrdem.Items.Add("Por Data Previsao + Cliente");
                lbxNorOrdem.Items.Add("Por Cliente + Data de Previsao");
                lbxNorOrdem.Items.Add("Por Numero de Duplicata");
                lbxNorOrdem.SelectedIndex = 0;
            }
        }

        private void btnRecNorFornecedorAte_Click(object sender, EventArgs e)
        {
            //clsInfo.znomegrid = btnRecNorFornecedorAte.Name;
            //frmClientePes frmClientePes = new frmClientePes();
            //frmClientePes.Init("Todos", idNorClienteAte);
            //clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

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

        private void rbnFluxoFinanceiro_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnVendedor_Click(object sender, EventArgs e)
        {
            //clsInfo.znomegrid = btnNorVendedor.Name;
            //frmClientePes frmClientePes = new frmClientePes();
            //frmClientePes.Init("Todos", idNorVendedor);

            //clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void btnReciVendedor_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnReciVendedor.Name;

            //frmClientePes frmClientePes = new frmClientePes();
            //frmClientePes.Init("Todos", idNorVendedor);
            //clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void rbnFluxoSimples_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbnFluxoAplibank_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbnFluxoSimples_Click(object sender, EventArgs e)
        {

        }
    }
}
