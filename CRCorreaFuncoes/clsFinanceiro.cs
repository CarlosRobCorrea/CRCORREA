using CRCorreaInfo;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Transactions;


namespace CRCorreaFuncoes
{
    public class DuplicataInfo
    {
        public Decimal valorliquido;
        public Decimal multas;
        public Decimal juros;
        public Decimal descontos;
        public Decimal credito;
        public Decimal saldo;
        public Decimal recebido;
        public Decimal devolucao;
        public Decimal diferenca;
        public Decimal pagando;
        public Int32 ntotaldias;
    }

    public class BaixarBoletoInfo
    {
        public Boolean IncluirRegistro;
        public String Codigo;
        public String Historico;
        public String DebCred;
        public Int32 ntotaldias;
        public Decimal ValorLancamento;
        public Decimal ValorTitulo;
        public String Nome;
        public Decimal VlComissao;
        public Decimal VlComissaoGer;
    }

    public class clsFinanceiro
    {
        SqlConnection scn;
        SqlCommand scd;

        SqlConnection scn1;
        SqlCommand scd1;

        SqlDataReader sdrDuplicata;
        SqlDataReader sdrFluxo;

        String query;
        String query1;
        String query2;

        String Tipodeb;

        String continuar;

        Int32 idFluxoOld;
        Int32 idPagarNew;
        Int32 idPagarObserva;

        SqlDataAdapter sda;
        DataTable dtTemp = new DataTable();

        DataTable dtObservacao = new DataTable();

        SqlDataReader sdrRecePaga;
        Int32 idRecePaga;

        clsPagarInfo clsPagarInfo = new clsPagarInfo();

        clsReceberInfo clsReceberInfo = new clsReceberInfo();
        
        public static DuplicataInfo CalcularDuplicata(String _documento,
                                                        String _vencimento,
                                                        String _databaixa,
                                                        Decimal _valor,
                                                        Decimal _valordesconto,
                                                        Decimal _valorjuros,
                                                        Decimal _valormulta,
                                                        Decimal _valorcredito,
                                                        String _atevencimento)
        {
            String ok = "N";

            SqlConnection scn;
            SqlCommand scd;
            String query;

            DateTime datavence = DateTime.Parse(_vencimento);
            DateTime datapaga = DateTime.Parse(_databaixa);
            DateTime dataAux;

            SqlDataReader sdrFeriado;

            TimeSpan totaldias;

            DuplicataInfo DuplicataInfo = new DuplicataInfo();

            DuplicataInfo.multas = 0;
            DuplicataInfo.juros = 0;
            DuplicataInfo.descontos = 0;
            DuplicataInfo.credito = 0;
            DuplicataInfo.saldo = 0;
            DuplicataInfo.recebido = 0;
            DuplicataInfo.devolucao = 0;
            DuplicataInfo.diferenca = 0;
            DuplicataInfo.pagando = 0;
            DuplicataInfo.ntotaldias = 0;

            if (DateTime.Parse(_vencimento) < DateTime.Parse(_databaixa))
            {
                if (_atevencimento == "S")
                { // sim calcula desconto mesmo após o vencimento
                    ok = "S";
                }
                else
                {
                    ok = "N";
                }
                DuplicataInfo.multas = _valormulta;
                // qtde de dias em atraso
                //totaldias.Days = datapaga.Subtract(datavence);
                totaldias = datapaga.Subtract(datavence);
                // verificar se é feriado / sabado / domingo
                dataAux = DateTime.Parse(_vencimento);
                Int32 idFeriado;
                Int32 verDias = 0;
                if (totaldias.Days > 0)
                {
                    verDias = totaldias.Days;
                    if (verDias < 7)
                    {
                        for (int x = 0; x < totaldias.Days; x++)
                        {
                            idFeriado = 0;

                            switch (dataAux.DayOfWeek.ToString().ToUpper().Substring(0, 3))
                            {
                                case "MON":
                                    // VERIFICAR SE existe feriado
                                    query = "select * from FERIADOS WHERE DATA=" + clsParser.SqlDateTimeFormat(dataAux.ToString("dd/MM/yyyy") + " 00:00", true);
                                    scn = new SqlConnection(clsInfo.conexaosqldados);
                                    scn.Open();
                                    scd = new SqlCommand(query, scn);
                                    sdrFeriado = scd.ExecuteReader();
                                    if (sdrFeriado.Read())
                                    { // achou
                                        idFeriado = clsParser.Int32Parse(sdrFeriado["ID"].ToString());
                                        verDias = verDias - 1;
                                    }
                                    scn.Close();
                                    break;
                                case "TUE":
                                    // VERIFICAR SE existe feriado
                                    query = "select * from FERIADOS WHERE DATA=" + clsParser.SqlDateTimeFormat(dataAux.ToString("dd/MM/yyyy") + " 00:00", true);
                                    scn = new SqlConnection(clsInfo.conexaosqldados);
                                    scn.Open();
                                    scd = new SqlCommand(query, scn);
                                    sdrFeriado = scd.ExecuteReader();
                                    if (sdrFeriado.Read())
                                    { // achou
                                        idFeriado = clsParser.Int32Parse(sdrFeriado["ID"].ToString());
                                        verDias = verDias - 1;
                                    }
                                    scn.Close();
                                    break;
                                case "WED":
                                    // VERIFICAR SE existe feriado
                                    query = "select * from FERIADOS WHERE DATA=" + clsParser.SqlDateTimeFormat(dataAux.ToString("dd/MM/yyyy") + " 00:00", true);
                                    scn = new SqlConnection(clsInfo.conexaosqldados);
                                    scn.Open();
                                    scd = new SqlCommand(query, scn);
                                    sdrFeriado = scd.ExecuteReader();
                                    if (sdrFeriado.Read())
                                    { // achou
                                        idFeriado = clsParser.Int32Parse(sdrFeriado["ID"].ToString());
                                        verDias = verDias - 1;
                                    }
                                    scn.Close();
                                    break;
                                case "FRI":
                                    // VERIFICAR SE existe feriado
                                    query = "select * from FERIADOS WHERE DATA=" + clsParser.SqlDateTimeFormat(dataAux.ToString("dd/MM/yyyy") + " 00:00", true);
                                    scn = new SqlConnection(clsInfo.conexaosqldados);
                                    scn.Open();
                                    scd = new SqlCommand(query, scn);
                                    sdrFeriado = scd.ExecuteReader();
                                    if (sdrFeriado.Read())
                                    { // achou
                                        idFeriado = clsParser.Int32Parse(sdrFeriado["ID"].ToString());
                                        verDias = verDias - 1;
                                    }
                                    scn.Close();
                                    break;
                                case "THU":
                                    // VERIFICAR SE existe feriado
                                    query = "select * from FERIADOS WHERE DATA=" + clsParser.SqlDateTimeFormat(dataAux.ToString("dd/MM/yyyy") + " 00:00", true);
                                    scn = new SqlConnection(clsInfo.conexaosqldados);
                                    scn.Open();
                                    scd = new SqlCommand(query, scn);
                                    sdrFeriado = scd.ExecuteReader();
                                    if (sdrFeriado.Read())
                                    { // achou
                                        idFeriado = clsParser.Int32Parse(sdrFeriado["ID"].ToString());
                                        verDias = verDias - 1;
                                    }
                                    scn.Close();
                                    break;
                                case "SAT":
                                    if (dataAux.ToString("dd/MM/yyyy") == DateTime.Parse(_databaixa.ToString()).ToString("dd/MM/yyyy"))
                                    {
                                        // não pode descontar nada já esta pagando no sabado
                                    }
                                    else
                                    {
                                        verDias = verDias - 1; //totaldias = totaldias.Add(new TimeSpan(-1, 0, 0, 0));
                                    }
                                    break;
                                case "SUN":
                                    if (dataAux.ToString("dd/MM/yyyy") == DateTime.Parse(_databaixa.ToString()).ToString("dd/MM/yyyy"))
                                    {
                                        // não pode descontar nada já esta pagando no sabado
                                    }
                                    else
                                    {
                                        verDias = verDias - 1;  //totaldias = totaldias.Add(new TimeSpan(-1, 0, 0, 0));
                                    }
                                    break;
                                default:
                                    break;
                            }
                            dataAux = dataAux.AddDays(1);
                        }
                    }
                }
                if (verDias == 0)
                {
                    DuplicataInfo.ntotaldias = 0;
                    DuplicataInfo.juros = 0;
                }
                else
                {
                    DuplicataInfo.ntotaldias = clsParser.Int32Parse(totaldias.TotalDays.ToString());
                    DuplicataInfo.juros = Math.Round(totaldias.Days * _valorjuros, 2);
                }
            }
            else
            {
                ok = "S";
            }

            if (ok == "S")
            { // calcula com desconto
                DuplicataInfo.valorliquido = _valor - _valordesconto;
                DuplicataInfo.descontos = _valordesconto;
            }
            else
            {
                DuplicataInfo.valorliquido = _valor;
                DuplicataInfo.descontos = 0;
            }
            DuplicataInfo.credito = _valorcredito;

            return DuplicataInfo;
        }

        /* 
            ' documento qual o documento (PAGAR/RECEBER)
            ' id do Documento
            ' Lote (Documento + Data e Hora de Hoje + Numero do Cheque + Numero do Id do Usuario
            '  Exemplo =>"PAG" + DateTime.Now.ToString("yyyyMMddHHmmss") + clsParser.Int32Parse(tbxBaixaBoletoNro.Text).ToString("0000000000") + clsInfo.zusuarioid.ToString("0000000");
            ' NROCHEQUE => SE POR CHEQUE QUAL O NUMRO DO CHEQUE
            ' IDBANCO => ID DA CONTA INTERNA DO BANCO
            ' IDHISTORICO => ID DO HISTORICO DO BANCO
            ' IDCENTROCUSTO => ID DO CENTRO DE CUSTO DO BANCO
            ' IDCONTACONTABIL => ID DA CONTA CONTABIL
            ' DATADEPPOSITO => DATA DO DEPOSITO
            ' VALOR => VALOR QUE ESTA SENDO PAGO
            ' DESCRICAO => HISTORICO DA BAIXA MAIS DETALHADO
            ' DEBCRED =<> O QUE ESTA VINDO DEBIDO OU CREDITO
            ' OBSERVACAO => QUANDO VEM DO BAIXAR CHEQUE ELE TRAZ O NOME DE QUEM DEU
        */
        // Transferir a Baixa para o Aplibank
        public void BaixarBanco(String Documento,
                                Int32 idDocumento,
                                String Lote,
                                String Nrocheque,
                                Int32 idBanco,
                                Int32 idHistorico,
                                Int32 idCentroCusto,
                                Int32 idContaContabil,
                                DateTime DataDeposito,
                                DateTime DataCredito,
                                Decimal Valor,
                                String CodigoCobrancaBaixa,
                                String DebCred,
                                String Observacao,
                                String Destinatario,
                                String TipoDoc,
                                String NotaDoc,
                                String NFVendaNumero,  
                                Int32 Posicao,
                                Int32 PosicaoFim,
                                String Pagoucom,
                                String Usuario, 
                                String nrocheque, 
                                Int32 Conferido, 
                                String TipoBaixa)

        {
            // TIPOBAIXA : UNICA - VEM DO CTAS A RECEBER E A PAGAR
            //             DIVERSAS - VEM DOS SISTEMAS DE BAIXA

            Int32 idFluxo = 0;

            String continuar = "S";
            //String Boleto = "";
            String complemento1;

            switch (Documento.Trim().ToUpper())
            {
                case "PAGAR":
                    query = "select * from PAGAR WHERE ID=" + idDocumento + "";
                    break;
                case "PAGARNFEZ":
                    query = "select * from NFCOMPRARESUMIDA WHERE ID=" + idDocumento + "";
                    break;
                case "RECEBER":
                    query = "select * from RECEBER WHERE ID=" + idDocumento + "";
                    //Boleto = Nrocheque;
                    if (Pagoucom != "CH")
                    { // se não for cheque (0 zera ) o numero do cheque
                        Nrocheque = "0";
                    }
                    break;
                default:
                    continuar = "N";
                    break;
            }
            if (continuar == "S")
            {
                // ABRIR A DUPLICATA o documento original               
                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtTemp);
                foreach (DataRow row in dtTemp.Rows)
                {
                    if (clsParser.Int32Parse(row["ID"].ToString()) > 0)
                    {
                        // VERIFICAR SE JA LANÇOU NO BANCO
                        query = "select * from FLUXO WHERE LOTE='" + Lote + "' ";
                        scn = new SqlConnection(clsInfo.conexaosqlbanco);
                        scn.Open();
                        scd = new SqlCommand(query, scn);
                        sdrFluxo = scd.ExecuteReader();
                        if (sdrFluxo.Read())
                        { // achou
                            idFluxo = clsParser.Int32Parse(sdrFluxo["ID"].ToString());
                        }
                        else
                        {
                            scn.Close();
                            idFluxo = 0;
                        }
                    }
                }
                if (idFluxo == 0)
                {
                    String Destinatario1 = Destinatario;
                    // Incluindo os lançamentos do fluxo01
                    if (TipoBaixa.ToUpper().Substring(0, 1) == "U")
                    {
                        complemento1 = "";
                        if (Observacao.Trim().Length > 1)
                        {
                            complemento1 = (Observacao.Trim());
                        }
                        if (Application.ProductName.ToUpper().Trim() != "APLIESQUADRIA")
                        {
                            complemento1 = complemento1 + " Cod.Bx=" + CodigoCobrancaBaixa.Trim().ToString(); 
                        }
                        if (complemento1.Trim().Length > 100)
                        {
                            complemento1 = complemento1.Trim().PadRight(100).Substring(0, 99);
                        }
                        if (Destinatario1.Trim().Length > 50)
                        {
                            Destinatario1 = Destinatario1.Trim().PadRight(50).Substring(0, 49);
                        }
                    }
                    else
                    {
                        if (Documento.ToUpper().Substring(0,3) == "REC")
                        {
                            complemento1 = "CREDITOS DIVERSOS";
                            Destinatario1 = "VARIOS CLIENTES";
                        }
                        else
                        {
                            complemento1 = "DEBITOS DIVERSOS";
                            Destinatario1 = "VARIOS FORNECEDORES";
                        }
                        
                    }
                    scn = new SqlConnection(clsInfo.conexaosqlbanco);
                    scn.Open();
                    scd = new SqlCommand("INSERT INTO FLUXO (" +
                             "NUMERO, EMISSAO, CONTA, COMPLEMENTO, DESTINATARIO, " +
                             "TIPO,   " +
                             "DATADEPOSITO, COMPENSACAO, DATAFINAL, VALOR, SALDO, DATATRANSFERENCIA, " +
                             "TRANSFERIDO, TRANSFERIDOPARA, DATALANCAMENTO, LOTE, CTATRANSFERIDA, SALDOREAL, " +
                             "VALORBLOQUEADO, SALDOBLOQUEADO, ESTORNO, CONFERIDO, USUARIO " +
                             ") VALUES ( " +
                             "@NUMERO, @EMISSAO, @CONTA, @COMPLEMENTO, @DESTINATARIO, " +
                             "@TIPO, " +
                             "@DATADEPOSITO, @COMPENSACAO, @DATAFINAL, @VALOR, @SALDO, @DATATRANSFERENCIA, " +
                             "@TRANSFERIDO, @TRANSFERIDOPARA, @DATALANCAMENTO, @LOTE, @CTATRANSFERIDA, @SALDOREAL, " +
                             "@VALORBLOQUEADO, @SALDOBLOQUEADO, @ESTORNO, @CONFERIDO, @USUARIO );" +
                            "SELECT SCOPE_IDENTITY()", scn);

                    scd.Parameters.AddWithValue("@NUMERO", SqlDbType.Int).Value = Nrocheque;
                    scd.Parameters.AddWithValue("@EMISSAO", SqlDbType.DateTime).Value = DataDeposito;
                    scd.Parameters.AddWithValue("@CONTA", SqlDbType.Int).Value = idBanco;
//                    scd.Parameters.AddWithValue("@HISTORICO", SqlDbType.Int).Value = idHistorico;
//                    scd.Parameters.AddWithValue("@CENTROCUSTO", SqlDbType.Int).Value = idCentroCusto;
//                    scd.Parameters.AddWithValue("@CONTABIL", SqlDbType.Int).Value = idContaContabil;
                    if (Application.ProductName.ToUpper().Trim() != "APLIESQUADRIA")
                    {
                        scd.Parameters.AddWithValue("@COMPLEMENTO", SqlDbType.NVarChar).Value = (complemento1 + " " + Destinatario1).PadRight(100).Substring(0, 99); 
                    }
                    else
                    {
                        scd.Parameters.AddWithValue("@COMPLEMENTO", SqlDbType.NVarChar).Value = complemento1;
                    }


                    scd.Parameters.AddWithValue("@DESTINATARIO", SqlDbType.NVarChar).Value = Destinatario1;
//                    scd.Parameters.AddWithValue("@TIPODOC", SqlDbType.NVarChar).Value = TipoDoc;
//                    scd.Parameters.AddWithValue("@DOCUMENTO", SqlDbType.NVarChar).Value = Documento;
//                    scd.Parameters.AddWithValue("@POSICAO", SqlDbType.Int).Value = Posicao;
//                    scd.Parameters.AddWithValue("@POSICAOFIM", SqlDbType.Int).Value = PosicaoFim;
//                    scd.Parameters.AddWithValue("@BOLETO", SqlDbType.NVarChar).Value = Boleto;
//                    scd.Parameters.AddWithValue("@TEMRECIBO", SqlDbType.NVarChar).Value = "S";
                    scd.Parameters.AddWithValue("@TIPO", SqlDbType.NVarChar).Value = "";
                    scd.Parameters.AddWithValue("@DATADEPOSITO", SqlDbType.DateTime).Value = DataCredito;
                    scd.Parameters.AddWithValue("@COMPENSACAO", SqlDbType.Int).Value = 0;
                    scd.Parameters.AddWithValue("@DATAFINAL", SqlDbType.DateTime).Value = DataCredito;
                    scd.Parameters.AddWithValue("@VALOR", SqlDbType.Decimal).Value = Valor;
                    scd.Parameters.AddWithValue("@SALDO", SqlDbType.Decimal).Value = 0;
                    scd.Parameters.AddWithValue("@DATATRANSFERENCIA", SqlDbType.DateTime).Value = DateTime.Now;
                    scd.Parameters.AddWithValue("@TRANSFERIDO", SqlDbType.Int).Value = "999";
                    scd.Parameters.AddWithValue("@TRANSFERIDOPARA", SqlDbType.Int).Value = "0";
//                    scd.Parameters.AddWithValue("@COMPENSAR", SqlDbType.Decimal).Value = Compensar;
                    scd.Parameters.AddWithValue("@DATALANCAMENTO", SqlDbType.DateTime).Value = DateTime.Now;
                    scd.Parameters.AddWithValue("@LOTE", SqlDbType.NVarChar).Value = Lote;
                    scd.Parameters.AddWithValue("@CTATRANSFERIDA", SqlDbType.NVarChar).Value = "";
                    scd.Parameters.AddWithValue("@SALDOREAL", SqlDbType.Decimal).Value = 0;
                    scd.Parameters.AddWithValue("@VALORBLOQUEADO", SqlDbType.Decimal).Value = 0;
                    scd.Parameters.AddWithValue("@SALDOBLOQUEADO", SqlDbType.Decimal).Value = 0;
                    scd.Parameters.AddWithValue("@ESTORNO", SqlDbType.NVarChar).Value = "N";
                    scd.Parameters.AddWithValue("@CONFERIDO", SqlDbType.Int).Value = Conferido;
                    scd.Parameters.AddWithValue("@USUARIO", SqlDbType.NVarChar).Value = Usuario;

                    scd.ExecuteNonQuery();
                    scn.Close();

                    idFluxo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from fluxo where lote= '" + Lote + "' "));
                }

                // Incluindo os lançamentos do fluxo01
                String complemento = "";
                if (Observacao.Trim().Length > 1)
                {
                    complemento = (Observacao.Trim());
                }
                if (Application.ProductName.ToUpper().Trim() != "APLIESQUADRIA")
                {
                    complemento = complemento + " Cod.Bx=" + CodigoCobrancaBaixa.Trim().ToString();
                }
                if (complemento.Trim().Length > 100)
                {
                    complemento = complemento.Trim().PadRight(100).Substring(0, 99);
                }
                if (Destinatario.Trim().Length > 50)
                {
                    Destinatario = Destinatario.Trim().PadRight(50).Substring(0, 49);
                }
                scn = new SqlConnection(clsInfo.conexaosqlbanco);
                scn.Open();
                scd = new SqlCommand("INSERT INTO FLUXO01 (" +
                         "FLUXO, HISTORICO, CENTROCUSTO, CONTABIL, COMPLEMENTO, DESTINATARIO, " +
                         "TIPODOC, DOCUMENTO, POSICAO, POSICAOFIM, BOLETO, TEMRECIBO, TIPO,   " +
                         "VALOR, CONFERIDO " +
                         ") VALUES ( " +
                         "@FLUXO, @HISTORICO, @CENTROCUSTO, @CONTABIL, @COMPLEMENTO, @DESTINATARIO, " +
                         "@TIPODOC, @DOCUMENTO, @POSICAO, @POSICAOFIM, @BOLETO, @TEMRECIBO, @TIPO, " +
                         "@VALOR, @CONFERIDO );" +
                        "SELECT SCOPE_IDENTITY()", scn);

                scd.Parameters.AddWithValue("@FLUXO", SqlDbType.Int).Value = idFluxo;
                scd.Parameters.AddWithValue("@HISTORICO", SqlDbType.Int).Value = idHistorico;
                scd.Parameters.AddWithValue("@CENTROCUSTO", SqlDbType.Int).Value = idCentroCusto;
                scd.Parameters.AddWithValue("@CONTABIL", SqlDbType.Int).Value = idContaContabil;
                scd.Parameters.AddWithValue("@COMPLEMENTO", SqlDbType.NVarChar).Value = complemento;
                scd.Parameters.AddWithValue("@DESTINATARIO", SqlDbType.NVarChar).Value = Destinatario;
                scd.Parameters.AddWithValue("@TIPODOC", SqlDbType.NVarChar).Value = NotaDoc;
                scd.Parameters.AddWithValue("@DOCUMENTO", SqlDbType.NVarChar).Value = NFVendaNumero;
                scd.Parameters.AddWithValue("@POSICAO", SqlDbType.Int).Value = Posicao;
                scd.Parameters.AddWithValue("@POSICAOFIM", SqlDbType.Int).Value = PosicaoFim;
                scd.Parameters.AddWithValue("@BOLETO", SqlDbType.NVarChar).Value = nrocheque;
                scd.Parameters.AddWithValue("@TEMRECIBO", SqlDbType.NVarChar).Value = "S";
                scd.Parameters.AddWithValue("@TIPO", SqlDbType.NVarChar).Value = DebCred;
//                scd.Parameters.AddWithValue("@DATADEPOSITO", SqlDbType.DateTime).Value = DataCredito;
//                scd.Parameters.AddWithValue("@COMPENSACAO", SqlDbType.Int).Value = Compensar;
//                scd.Parameters.AddWithValue("@DATAFINAL", SqlDbType.DateTime).Value = DataCredito;
                scd.Parameters.AddWithValue("@VALOR", SqlDbType.Decimal).Value = Valor;
//                scd.Parameters.AddWithValue("@SALDO", SqlDbType.Decimal).Value = 0;
//                scd.Parameters.AddWithValue("@DATATRANSFERENCIA", SqlDbType.DateTime).Value = DateTime.Now;
//                scd.Parameters.AddWithValue("@TRANSFERIDO", SqlDbType.Int).Value = "999";
//                scd.Parameters.AddWithValue("@COMPENSAR", SqlDbType.Decimal).Value = Compensar;
//                scd.Parameters.AddWithValue("@DATALANCAMENTO", SqlDbType.DateTime).Value = DateTime.Now;
                scd.Parameters.AddWithValue("@CONFERIDO", SqlDbType.Int).Value = Conferido;


                scd.ExecuteNonQuery();
                scn.Close();

                //////////////////////////////////////////////////////////////////////
                if (Documento.Trim().ToUpper() == "PAGARNFEZ") // entrada rapida / ja lança direto no Banco
                {
                    // Marcar na Nota Fiscal que foi Pago
                    //clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "NFCOMPRARESUMIDA","FLUXO = " + idFluxo + "", "ID = " + idDocumento);
                    scn1 = new SqlConnection(clsInfo.conexaosqldados);
                    query = "select * FROM NFCOMPRARESUMIDA where id=" + idDocumento;
                    scn1.Open();
                    //scd1 = new SqlCommand(query, scn1);

                    scd1 = new SqlCommand("UPDATE NFCOMPRARESUMIDA SET " +
                    "IDFLUXO=@IDFLUXO " +
                    "WHERE ID = @ID", scn1);
                    scd1.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = idDocumento;
                    scd1.Parameters.AddWithValue("@IDFLUXO", SqlDbType.Int).Value = idFluxo;
                    scd1.ExecuteNonQuery();
                    scn1.Close();

                }


                // calcular o valor do fluxo e o saldo
                //clsFinanceiro.SomarValorItensFluxo(clsParser.Int32Parse(idFluxo), clsParser.Int32Parse(idBanco), DataDeposito.ToString());
                SomarValorItensFluxo(idFluxo, idBanco, DataDeposito.ToString());
            }
            else
            {
                MessageBox.Show("Documento não Confere (PAGAR/RECEBER)");
            }
        }
        // Baixar o Boleto  -- Preparando num arquivo virtual para depois baixar definitivamente
        public static BaixarBoletoInfo BaixarBoleto(Int32 Fase, 
                                 Int32 idReceber, 
                                 Decimal VlJuros,
                                 Int32 BancoFebraban,
                                 DateTime DataBaixa,
                                 DateTime DataVencimento,
                                 Decimal ValorTitulo,
                                 Decimal TarifaBoleto,
                                 Decimal TarifaCartorio,
                                 Decimal Multa,
                                 Decimal Desconto,
                                 String  AteVencimento,
                                 Decimal VlComissao,
                                 Decimal VlComissaoGer)
        {

            BaixarBoletoInfo  BaixarBoletoInfo = new BaixarBoletoInfo();

            BaixarBoletoInfo.Codigo = "";
            BaixarBoletoInfo.DebCred = "";
            BaixarBoletoInfo.Historico = "";
            BaixarBoletoInfo.IncluirRegistro = true;
            BaixarBoletoInfo.Nome = "";
            BaixarBoletoInfo.ntotaldias = 0;
            BaixarBoletoInfo.ValorLancamento = 0;
            BaixarBoletoInfo.ValorTitulo = 0;
            BaixarBoletoInfo.VlComissao = 0;
            BaixarBoletoInfo.VlComissaoGer = 0;

            if (Fase == 1 )
            {
               //' 1 = baixa o valor principal
                BaixarBoletoInfo.Codigo = "01";
                BaixarBoletoInfo.Historico = "00";
                BaixarBoletoInfo.Nome = "Liquidação Total";
                BaixarBoletoInfo.ValorLancamento = ValorTitulo;
                BaixarBoletoInfo.ValorTitulo = ValorTitulo;
                BaixarBoletoInfo.DebCred = "C";
            }  
            else if(Fase == 2)
            {
               //' 2 = valor tarifa por titulo baixado
                BaixarBoletoInfo.Codigo = "04";
                BaixarBoletoInfo.Historico = "40";
                BaixarBoletoInfo.Nome = "Tar Cobrança Titulo";
                BaixarBoletoInfo.DebCred = "D";
               
               //'Bancos que não cobram a tarifa na hora da baixa
               if (BancoFebraban ==0) 
               {
                  //' NAÕ CALCULA PORQUE VEIO DO BAIXA DE CHEQUES
                  BaixarBoletoInfo.IncluirRegistro = false;
               }
               else if (BancoFebraban == 1)
               {
                  //BANCO DO BRASIL
                  BaixarBoletoInfo.IncluirRegistro = false;
               }
               else if (BancoFebraban == 237)
               {
                  //BANCO BRADESCO
                  BaixarBoletoInfo.IncluirRegistro = false;
               }
               else 
               {
                   BaixarBoletoInfo.ValorLancamento = TarifaBoleto;
               }
               VlComissao = 0;
               VlComissaoGer = 0;
            }
            else if(Fase == 3)
            {
              // ' 3 = valor dos juros por dia de atraso
                BaixarBoletoInfo.Codigo = "03";
                BaixarBoletoInfo.Historico = "";
                BaixarBoletoInfo.DebCred = "C";
               //' Calcular o Valor do Juros
               // ==> Ja esta vindo calculado
                BaixarBoletoInfo.ntotaldias = DataVencimento.Subtract(DataBaixa).Days;
                BaixarBoletoInfo.ValorLancamento = VlJuros;
                BaixarBoletoInfo.Nome = "Juros " + BaixarBoletoInfo.ntotaldias + " dias";
                BaixarBoletoInfo.VlComissao = 0;
                BaixarBoletoInfo.VlComissaoGer = 0;
                if (BaixarBoletoInfo.ValorLancamento == 0)
               {
                   BaixarBoletoInfo.IncluirRegistro = false;
               }
            }
            else if (Fase == 4)
            {
               //' 4 = valor da multa por atraso
                BaixarBoletoInfo.Codigo = "02";
                BaixarBoletoInfo.Historico = "";
                BaixarBoletoInfo.Nome = "Multa por Atraso";
                BaixarBoletoInfo.DebCred = "C";
               //' VER SE TEM VALOR DE MULTA MARCADO
               if (Multa > 0 )
               {
                  if (DataBaixa <= DataVencimento)
                  {
                     // dentro do vencimento
                     // não cobra MULTA
                     BaixarBoletoInfo.IncluirRegistro = false;
                  }
                  else 
                  {
                      BaixarBoletoInfo.ValorLancamento = ValorTitulo * (Multa / 100);
                  }
               }
               else
               {
                  BaixarBoletoInfo.IncluirRegistro = false;
               }
               VlComissao = 0;
               VlComissaoGer = 0;
            }
            else if (Fase == 5)
            {
               //' 5 = valor do desconto na duplicata
                BaixarBoletoInfo.Codigo = "08";
                BaixarBoletoInfo.Historico = "";
                BaixarBoletoInfo.Nome = "Desconto";
                BaixarBoletoInfo.DebCred = "D";
               //' VER SE TEM VALOR DE DESCONTO MARCADO
               if (Desconto > 0 )
               {
                   if (DataBaixa <= DataVencimento)
                   {
                     //' dentro do vencimento
                       BaixarBoletoInfo.ValorLancamento = Desconto;
                   }
                   else 
                   {
                     //'passou do vencimento
                     if (AteVencimento == "S")
                     {
                        BaixarBoletoInfo.IncluirRegistro = false;
                     }
                     else
                     {
                         BaixarBoletoInfo.ValorLancamento = Desconto;
                     } 
                   }
               }
               else
               {
                  BaixarBoletoInfo.IncluirRegistro = false;
               }
               VlComissao = 0;
               VlComissaoGer = 0;
            }
            return BaixarBoletoInfo;
        }
        //
        // Baixar o Boleto  -- Preparando num arquivo virtual para depois baixar definitivamente
        public static BaixarBoletoInfo BaixarBoletoPagar(Int32 Fase,
                                 Int32 idReceber,
                                 Decimal VlJuros,
                                 Int32 BancoFebraban,
                                 DateTime DataBaixa,
                                 DateTime DataVencimento,
                                 Decimal ValorTitulo,
                                 Decimal TarifaBoleto,
                                 Decimal TarifaCartorio,
                                 Decimal Multa,
                                 Decimal Desconto,
                                 String AteVencimento,
                                 Decimal VlComissao,
                                 Decimal VlComissaoGer)
        {

            BaixarBoletoInfo BaixarBoletoInfo = new BaixarBoletoInfo();

            BaixarBoletoInfo.Codigo = "";
            BaixarBoletoInfo.DebCred = "";
            BaixarBoletoInfo.Historico = "";
            BaixarBoletoInfo.IncluirRegistro = true;
            BaixarBoletoInfo.Nome = "";
            BaixarBoletoInfo.ntotaldias = 0;
            BaixarBoletoInfo.ValorLancamento = 0;
            BaixarBoletoInfo.ValorTitulo = 0;
            BaixarBoletoInfo.VlComissao = 0;
            BaixarBoletoInfo.VlComissaoGer = 0;

            if (Fase == 1)
            {
                //' 1 = baixa o valor principal
                BaixarBoletoInfo.Codigo = "01";
                BaixarBoletoInfo.Historico = "00";
                BaixarBoletoInfo.Nome = "Liquidação Total";
                BaixarBoletoInfo.ValorLancamento = ValorTitulo;
                BaixarBoletoInfo.ValorTitulo = ValorTitulo;
                BaixarBoletoInfo.DebCred = "D";
            }
            else if (Fase == 2)
            {
                //' 2 = valor tarifa por titulo baixado
                BaixarBoletoInfo.Codigo = "04";
                BaixarBoletoInfo.Historico = "40";
                BaixarBoletoInfo.Nome = "Tar Cobrança Titulo";
                BaixarBoletoInfo.DebCred = "C";

                //'Bancos que não cobram a tarifa na hora da baixa
                if (BancoFebraban == 0)
                {
                    //' NAÕ CALCULA PORQUE VEIO DO BAIXA DE CHEQUES
                    BaixarBoletoInfo.IncluirRegistro = false;
                }
                else if (BancoFebraban == 1)
                {
                    //BANCO DO BRASIL
                    BaixarBoletoInfo.IncluirRegistro = false;
                }
                else if (BancoFebraban == 237)
                {
                    //BANCO BRADESCO
                    BaixarBoletoInfo.IncluirRegistro = false;
                }
                else
                {
                    BaixarBoletoInfo.ValorLancamento = TarifaBoleto;
                }
                VlComissao = 0;
                VlComissaoGer = 0;
            }
            else if (Fase == 3)
            {
                // ' 3 = valor dos juros por dia de atraso
                BaixarBoletoInfo.Codigo = "03";
                BaixarBoletoInfo.Historico = "";
                BaixarBoletoInfo.DebCred = "D";
                //' Calcular o Valor do Juros
                // ==> Ja esta vindo calculado
                BaixarBoletoInfo.ntotaldias = DataVencimento.Subtract(DataBaixa).Days;
                BaixarBoletoInfo.ValorLancamento = VlJuros;
                BaixarBoletoInfo.Nome = "Juros " + BaixarBoletoInfo.ntotaldias + " dias";
                BaixarBoletoInfo.VlComissao = 0;
                BaixarBoletoInfo.VlComissaoGer = 0;
                if (BaixarBoletoInfo.ValorLancamento == 0)
                {
                    BaixarBoletoInfo.IncluirRegistro = false;
                }
            }
            else if (Fase == 4)
            {
                //' 4 = valor da multa por atraso
                BaixarBoletoInfo.Codigo = "02";
                BaixarBoletoInfo.Historico = "";
                BaixarBoletoInfo.Nome = "Multa por Atraso";
                BaixarBoletoInfo.DebCred = "D";
                //' VER SE TEM VALOR DE MULTA MARCADO
                if (Multa > 0)
                {
                    if (DataBaixa <= DataVencimento)
                    {
                        // dentro do vencimento
                        // não cobra MULTA
                        BaixarBoletoInfo.IncluirRegistro = false;
                    }
                    else
                    {
                        BaixarBoletoInfo.ValorLancamento = ValorTitulo * (Multa / 100);
                    }
                }
                else
                {
                    BaixarBoletoInfo.IncluirRegistro = false;
                }
                VlComissao = 0;
                VlComissaoGer = 0;
            }
            else if (Fase == 5)
            {
                //' 5 = valor do desconto na duplicata
                BaixarBoletoInfo.Codigo = "08";
                BaixarBoletoInfo.Historico = "";
                BaixarBoletoInfo.Nome = "Desconto";
                BaixarBoletoInfo.DebCred = "C";
                //' VER SE TEM VALOR DE DESCONTO MARCADO
                if (Desconto > 0)
                {
                    if (DataBaixa <= DataVencimento)
                    {
                        //' dentro do vencimento
                        BaixarBoletoInfo.ValorLancamento = Desconto;
                    }
                    else
                    {
                        //'passou do vencimento
                        if (AteVencimento == "S")
                        {
                            BaixarBoletoInfo.IncluirRegistro = false;
                        }
                        else
                        {
                            BaixarBoletoInfo.ValorLancamento = Desconto;
                        }
                    }
                }
                else
                {
                    BaixarBoletoInfo.IncluirRegistro = false;
                }
                VlComissao = 0;
                VlComissaoGer = 0;
            }
            return BaixarBoletoInfo;
        }
        // Calcular o Saldo do Banco (Aplibank)
        public void SaldoBanco(Int32 idConta, DateTime dataDeposito)
        {
            String query;
            DataTable dtFluxo;

            SqlDataAdapter sda;

            Decimal saldoAtual = 0;
            dataDeposito = new DateTime(dataDeposito.Year, dataDeposito.Month, dataDeposito.Day);

            query = "select top 1 " +
                        " SALDO " +
                    "from " +
                        "FLUXO " +
                    "WHERE " +
                        "CONTA = @IDCONTA AND " +
                        "DATADEPOSITO < @DATADEPOSITO " +
                    "ORDER BY " +
                        "DATADEPOSITO DESC, " +
                        "TIPO DESC, " +
                        "NUMERO DESC, " +
                        "ID DESC ";

            dtFluxo = new DataTable();

            sda = new SqlDataAdapter(query, clsInfo.conexaosqlbanco);
            sda.SelectCommand.Parameters.Add("@IDCONTA", SqlDbType.Int).Value = idConta;
            sda.SelectCommand.Parameters.Add("@DATADEPOSITO", SqlDbType.DateTime).Value = dataDeposito;
            sda.Fill(dtFluxo);

            foreach (DataRow row in dtFluxo.Rows)
            {
                saldoAtual += clsParser.DecimalParse(row[0].ToString());
            }

            query = "select " +
                        " ID, " +
                        " TIPO, " +
                        " VALOR " +
                    "from " +
                        "FLUXO " +
                    "WHERE " +
                        "CONTA = @IDCONTA AND " +
                        "DATADEPOSITO >= @DATADEPOSITO " +
                    "ORDER BY " +
                        "DATADEPOSITO, " +
                        "TIPO, " +
                        "NUMERO, " +
                        "ID ";

            dtFluxo = new DataTable();

            sda = new SqlDataAdapter(query, clsInfo.conexaosqlbanco);
            sda.SelectCommand.Parameters.Add("@IDCONTA", SqlDbType.Int).Value = idConta;
            sda.SelectCommand.Parameters.Add("@DATADEPOSITO", SqlDbType.DateTime).Value = dataDeposito;
            sda.Fill(dtFluxo);

            foreach (DataRow row in dtFluxo.Rows)
            {
                if (row["TIPO"].ToString() == "D")
                {
                    saldoAtual -= clsParser.DecimalParse(row["valor"].ToString());
                }
                else
                {
                    saldoAtual += clsParser.DecimalParse(row["valor"].ToString());
                }
                SqlConnection scn = new SqlConnection(clsInfo.conexaosqlbanco);
                SqlCommand scd = new SqlCommand("update fluxo set SALDO = @SALDO where ID=@ID ", scn);
                scd.Parameters.AddWithValue("@ID", clsParser.Int32Parse(row["ID"].ToString()));
                scd.Parameters.AddWithValue("@SALDO", saldoAtual);
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }
        }

        // Somar os itens do fluxo01 e gravar no fluxo (Aplibank)
        public void SomarValorItensFluxo(Int32 idfluxo, Int32 idconta, String DataDeposito)
        {
            // pegar o saldo
            DataTable dtValorItensFluxo;
            Decimal Valor = 0;
            //
            dtValorItensFluxo = new DataTable();
            query = "select ID, VALOR, TIPO  FROM FLUXO01 WHERE FLUXO=@IDFLUXO ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqlbanco);
            sda.SelectCommand.Parameters.Add("@IDFLUXO", SqlDbType.Int).Value = idfluxo;
            sda.Fill(dtValorItensFluxo);

            foreach (DataRow row in dtValorItensFluxo.Rows)
            {
                if (row["TIPO"].ToString().Trim() == "D")
                {
                    Valor = Valor - clsParser.DecimalParse(row["VALOR"].ToString());
                }
                else
                {
                    Valor = Valor + clsParser.DecimalParse(row["VALOR"].ToString());
                }
    
            }
            //
            Tipodeb = "D";
            if (Valor > 0)
            {// credito
                Tipodeb = "C";
            }
            else
            {
                Valor = (Valor * -1);
            }
            //
            scn = new SqlConnection(clsInfo.conexaosqlbanco);
            scn.Open();
            scd = new SqlCommand("UPDATE FLUXO SET VALOR=@VALOR, TIPO=@TIPO WHERE ID=@IDFLUXO ", scn);
            scd.Parameters.AddWithValue("@IDFLUXO", SqlDbType.Int).Value = idfluxo;
            scd.Parameters.AddWithValue("@VALOR", SqlDbType.Decimal).Value = Valor;
            scd.Parameters.AddWithValue("@TIPO", SqlDbType.NVarChar).Value = Tipodeb;
            scd.ExecuteNonQuery();
            scn.Close();

            /*
            clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqlbanco, "FLUXO",
                "TIPO = " + clsParser.SqlStringFormat(Tipodeb.ToString(), false, "TIPO") +
                "VALOR = " + clsParser.SqlDecimalFormat(Valor.ToString(), true, "VALOR"),
                "ID = " + clsParser.Int32Parse(idfluxo.ToString()));
            */
            // poderiamos até quando baixa ja calcular o saldo do banco
            SaldoBanco(idconta, clsParser.DateTimeParse(DataDeposito));
        }

        //clsFinanceiro.TranferirPago agora retorna o valor do idFluxoOld como requisitado
        //para usar basta atribuir a função a uma variavel conforme necessário
        // Transferir Para o Ctas Pagas // Ctas Recebidas
        public int TransferirPago(String Documento, Int32 idDocumento, String DataEnvio, String DataOk, Int32 idCobrancaCod, 
                             Int32 idCobrancaHis, Decimal Valor, String DebCred, Int32 idHistorico, 
                             Int32 idCentroCusto, Int32 idContaContabil, String baixoucomo, 
                             Decimal ValorComissao, Decimal ValorComissaoGer, Decimal ValorComissaoSup)
         /* 
            ' documento qual o documento (PAGAR/RECEBER)
            ' id do Documento
        */
        {
            continuar = "S";
            switch (Documento.Trim().ToUpper())
            {
                case "PAGAR":
                    query = "select * from PAGAR WHERE ID=" + idDocumento + "";
                    query1 = "select * from PAGAS WHERE IDPAGAR=" + idDocumento + "";
                    break;
                case "RECEBER":
                    query = "select * from RECEBER WHERE ID=" + idDocumento + "";
                    query1 = "select * from RECEBIDA WHERE IDRECEBER=" + idDocumento + "";
                    break;

                default:
                    continuar = "N";
                    break;
            }
            if (continuar == "S")
            {
                // ABRIR A DUPLICATA                
                scn = new SqlConnection(clsInfo.conexaosqldados);
                scn.Open();
                scd = new SqlCommand(query, scn);
                sdrDuplicata = scd.ExecuteReader();
                if (sdrDuplicata.Read())
                {  // Duplicata Existe
                        if (Documento.Trim().ToUpper() == "PAGAR")
                        {
                            // ABRIR A DUPLICATA PAGA
                            scn = new SqlConnection(clsInfo.conexaosqldados);
                            scn.Open();
                            scd = new SqlCommand(query1, scn);
                            sdrRecePaga = scd.ExecuteReader();
                            if (sdrRecePaga.Read())
                            {
                                idRecePaga = clsParser.Int32Parse(sdrRecePaga["ID"].ToString());
                            }
                            else
                            {
                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scd = new SqlCommand("insert into PAGAS (" +
                                            "FILIAL,DUPLICATA,POSICAO,POSICAOFIM,EMISSAO,IDDOCUMENTO,SETOR,IDFORNECEDOR,DATALANCA, " +
                                            "EMITENTE,IDHISTORICO,IDCENTROCUSTO, IDCODIGOCTABIL,IDNOTAFISCAL,IDPAGAR,IDPAGARNFE, " +
                                            "OBSERVA,IDFORMAPAGTO,IDBANCO,IDBANCOINT,CHEGOU,DESPESAPUBLICA,BOLETO, " +
                                            "BOLETONRO,DV,BAIXA,VENCIMENTO,VALOR,VALORDESCONTO,ATEVENCIMENTO,VALORLIQUIDO, " +
                                            "VALORJUROS,VALORMULTA, " +
                                            "IDSITBANCO, VALORPAGO,VALORBAIXANDO, " +
                                            "IMPRIMIR " +
                                     ") values (" +
                                            "@FILIAL,@DUPLICATA,@POSICAO,@POSICAOFIM,@EMISSAO,@IDDOCUMENTO,@SETOR,@IDFORNECEDOR,@DATALANCA, " +
                                            "@EMITENTE,@IDHISTORICO,@IDCENTROCUSTO,@IDCODIGOCTABIL,@IDNOTAFISCAL,@IDPAGAR,@IDPAGARNFE, " +
                                            "@OBSERVA,@IDFORMAPAGTO,@IDBANCO,@IDBANCOINT,@CHEGOU,@DESPESAPUBLICA,@BOLETO, " +
                                            "@BOLETONRO,@DV,@BAIXA,@VENCIMENTO,@VALOR,@VALORDESCONTO,@ATEVENCIMENTO,@VALORLIQUIDO, " +
                                            "@VALORJUROS,@VALORMULTA, " +
                                            "@IDSITBANCO,@VALORPAGO,@VALORBAIXANDO, " +
                                            "@IMPRIMIR " +
                                     "); select SCOPE_IDENTITY()", scn);
                                scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["FILIAL"].ToString());
                                scd.Parameters.Add("@DUPLICATA", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["DUPLICATA"].ToString());
                                scd.Parameters.Add("@POSICAO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["POSICAO"].ToString());
                                scd.Parameters.Add("@POSICAOFIM", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["POSICAOFIM"].ToString());
                                scd.Parameters.Add("@EMISSAO", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(sdrDuplicata["EMISSAO"].ToString());
                                scd.Parameters.Add("@IDDOCUMENTO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDDOCUMENTO"].ToString());
                                scd.Parameters.Add("@SETOR", SqlDbType.NVarChar).Value = sdrDuplicata["SETOR"].ToString();
                                scd.Parameters.Add("@IDFORNECEDOR", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDFORNECEDOR"].ToString());
                                scd.Parameters.Add("@DATALANCA", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(DateTime.Now.ToString());
                                scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = clsInfo.zusuario;
                                scd.Parameters.Add("@IDHISTORICO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDHISTORICO"].ToString());
                                scd.Parameters.Add("@IDCENTROCUSTO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDCENTROCUSTO"].ToString());
                                scd.Parameters.Add("@IDCODIGOCTABIL", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDCODIGOCTABIL"].ToString());
                                if (clsInfo.zbaixatemnotafiscal != "S")
                                {
                                    scd.Parameters.Add("@IDNOTAFISCAL", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDNOTAFISCAL"].ToString());
                                }
                                else
                                {
                                    scd.Parameters.Add("@IDNOTAFISCAL", SqlDbType.Int).Value = clsInfo.znfcompra;
                                }
                                scd.Parameters.Add("@IDPAGAR", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["ID"].ToString());
                                if (clsInfo.zbaixatemnotafiscal != "S")
                                {
                                    scd.Parameters.Add("@IDPAGARNFE", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDPAGARNFE"].ToString());
                                }
                                else
                                {
                                    scd.Parameters.Add("@IDPAGARNFE", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from nfcomprapagar where idnota=" + clsInfo.znfcompra));
                                }
                                scd.Parameters.Add("@OBSERVA", SqlDbType.NVarChar).Value = sdrDuplicata["OBSERVA"].ToString();
                                scd.Parameters.Add("@IDFORMAPAGTO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDFORMAPAGTO"].ToString());
                                scd.Parameters.Add("@IDBANCO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDBANCO"].ToString());
                                scd.Parameters.Add("@IDBANCOINT", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDBANCOINT"].ToString());
                                scd.Parameters.Add("@CHEGOU", SqlDbType.NVarChar).Value = sdrDuplicata["CHEGOU"].ToString();
                                scd.Parameters.Add("@DESPESAPUBLICA", SqlDbType.NVarChar).Value = sdrDuplicata["DESPESAPUBLICA"].ToString();
                                scd.Parameters.Add("@BOLETO", SqlDbType.NVarChar).Value = sdrDuplicata["BOLETO"].ToString();
                                scd.Parameters.Add("@BOLETONRO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["BOLETONRO"].ToString());
                                scd.Parameters.Add("@DV", SqlDbType.NVarChar).Value = sdrDuplicata["DV"].ToString();
                                scd.Parameters.Add("@BAIXA", SqlDbType.NVarChar).Value = sdrDuplicata["BAIXA"].ToString();
                                scd.Parameters.Add("@VENCIMENTO", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(sdrDuplicata["VENCIMENTO"].ToString());
                                scd.Parameters.Add("@VALOR", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALOR"].ToString());
                                scd.Parameters.Add("@VALORDESCONTO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORDESCONTO"].ToString());
                                scd.Parameters.Add("@ATEVENCIMENTO", SqlDbType.NVarChar).Value = sdrDuplicata["ATEVENCIMENTO"].ToString();
                                scd.Parameters.Add("@VALORLIQUIDO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORLIQUIDO"].ToString());
                                scd.Parameters.Add("@VALORJUROS", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORJUROS"].ToString());
                                scd.Parameters.Add("@VALORMULTA", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORMULTA"].ToString());
                                scd.Parameters.Add("@IDSITBANCO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDSITBANCO"].ToString());
                                scd.Parameters.Add("@VALORPAGO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORPAGO"].ToString());
                                scd.Parameters.Add("@VALORBAIXANDO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORBAIXANDO"].ToString());
                                //scd.Parameters.Add("@VALORDEVOLVIDO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORDEVOLVIDO"].ToString());
                                scd.Parameters.Add("@IMPRIMIR", SqlDbType.NVarChar).Value = sdrDuplicata["IMPRIMIR"].ToString();
                                scn.Open();
                                idRecePaga = Int32.Parse(scd.ExecuteScalar().ToString());
                                scn.Close();
                            }
                            sdrRecePaga.Close();
                            // Incluindo os Lançamento do Pagamento
                            baixoucomo = baixoucomo.PadRight(30).Substring(0, 30);
                            idFluxoOld = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "PAGAS01",
                                     "IDDUPLICATA,DATAENVIO,DATAOK,IDCOBRANCACOD,IDCOBRANCAHIS,VALOR, " +
                                     "DEBCRED, MOTIVO, IDHISTORICO, IDCENTROCUSTO, IDCODIGOCTABIL, BAIXOUCOMO, " +
                                     "IDFLUXO, IDFLUXO1, VALORCOMISSAO, VALORCOMISSAOGER, VALORCOMISSAOSUP ",
                                     clsParser.SqlInt32Format(idRecePaga.ToString(), false, "IDDUPLICATA") +
                                     clsParser.SqlDateTimeFormat(DataEnvio.ToString().Trim(), false, "DATAENVIO") +
                                     clsParser.SqlDateTimeFormat(DataOk.ToString().Trim(), false, "DATAOK") +
                                     clsParser.SqlInt32Format(idCobrancaCod.ToString(), false, "IDCOBRANCACOD") +
                                     clsParser.SqlInt32Format(idCobrancaHis.ToString(), false, "IDCOBRANCAHIS") +
                                     clsParser.SqlDecimalFormat(Valor.ToString(), false, "VALOR") +
                                     clsParser.SqlStringFormat(DebCred.ToString().Trim(), false, "DEBCRED") +
                                     clsParser.SqlStringFormat("".ToString().Trim(), false, "MOTIVO") +
                                     clsParser.SqlInt32Format(idHistorico.ToString(), false, "IDHISTORICO") +
                                     clsParser.SqlInt32Format(idCentroCusto.ToString(), false, "IDCENTROCUSTO") +
                                     clsParser.SqlInt32Format(idContaContabil.ToString(), false, "IDCODIGOCTABIL") +
                                     clsParser.SqlStringFormat(baixoucomo.ToString().Trim(), false, "BAIXOUCOMO") +
                                     clsParser.SqlInt32Format(0.ToString(), false, "IDFLUXO") +
                                     clsParser.SqlInt32Format(0.ToString(), false, "IDFLUXO1") +
                                     clsParser.SqlDecimalFormat(ValorComissao.ToString(), false, "VALORCOMISSAO") +
                                     clsParser.SqlDecimalFormat(ValorComissaoGer.ToString(), false, "VALORCOMISSAOGER") +
                                     clsParser.SqlDecimalFormat(ValorComissaoSup.ToString(), true, "VALORCOMISSAOSUP"));
                    }
                    else
                    { // SE NÃO FOR PAGAR SO PODE SER RECEBER
                        // ABRIR A DUPLICATA RECEBIDA               
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scn.Open();
                        scd = new SqlCommand(query1, scn);
                        sdrRecePaga = scd.ExecuteReader();
                        if (sdrRecePaga.Read())
                        {
                            idRecePaga = clsParser.Int32Parse(sdrRecePaga["ID"].ToString());
                        }
                        else
                        {
                            scn = new SqlConnection(clsInfo.conexaosqldados);
                            scd = new SqlCommand("insert into RECEBIDA (" +
                                        "FILIAL,DUPLICATA,POSICAO,POSICAOFIM,EMISSAO,IDDOCUMENTO,SETOR,IDCLIENTE,DATALANCA, " +
                                        "EMITENTE,IDHISTORICO,IDCENTROCUSTO, IDCODIGOCTABIL,IDNOTAFISCAL,IDRECEBER,IDRECEBERNFV, " +
                                        "OBSERVA,IDFORMAPAGTO,IDBANCO,IDBANCOINT,CHEGOU,DESPESAPUBLICA,BOLETO, " +
                                        "BOLETONRO,DV,BAIXA,VENCIMENTO,VALOR,VALORDESCONTO,ATEVENCIMENTO,VALORLIQUIDO, " +
                                        "VALORJUROS,VALORMULTA, " +
                                        "IDSITBANCO, VALORPAGO,VALORBAIXANDO, " +
                                        "IDVENDEDOR, VALORCOMISSAO, IDSUPERVISOR, VALORCOMISSAOSUP, IDCOORDENADOR, VALORCOMISSAOGER, " +
                                        "IMPRIMIR " +
                                 ") values (" +
                                        "@FILIAL,@DUPLICATA,@POSICAO,@POSICAOFIM,@EMISSAO,@IDDOCUMENTO,@SETOR,@IDCLIENTE,@DATALANCA, " +
                                        "@EMITENTE,@IDHISTORICO,@IDCENTROCUSTO,@IDCODIGOCTABIL,@IDNOTAFISCAL,@IDRECEBER,@IDRECEBERNFV, " +
                                        "@OBSERVA,@IDFORMAPAGTO,@IDBANCO,@IDBANCOINT,@CHEGOU,@DESPESAPUBLICA,@BOLETO, " +
                                        "@BOLETONRO,@DV,@BAIXA,@VENCIMENTO,@VALOR,@VALORDESCONTO,@ATEVENCIMENTO,@VALORLIQUIDO, " +
                                        "@VALORJUROS,@VALORMULTA, " +
                                        "@IDSITBANCO,@VALORPAGO,@VALORBAIXANDO, " +
                                        "@IDVENDEDOR,@VALORCOMISSAO,@IDSUPERVISOR,@VALORCOMISSAOSUP,@IDCOORDENADOR,@VALORCOMISSAOGER, " +
                                        "@IMPRIMIR " +
                                 "); select SCOPE_IDENTITY()", scn);
                            scd.Parameters.Add("@FILIAL", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["FILIAL"].ToString());
                            scd.Parameters.Add("@DUPLICATA", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["DUPLICATA"].ToString());
                            scd.Parameters.Add("@POSICAO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["POSICAO"].ToString());
                            scd.Parameters.Add("@POSICAOFIM", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["POSICAOFIM"].ToString());
                            scd.Parameters.Add("@EMISSAO", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(sdrDuplicata["EMISSAO"].ToString());
                            scd.Parameters.Add("@IDDOCUMENTO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDDOCUMENTO"].ToString());
                            scd.Parameters.Add("@SETOR", SqlDbType.NVarChar).Value = sdrDuplicata["SETOR"].ToString();
                            scd.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDCLIENTE"].ToString());
                            scd.Parameters.Add("@DATALANCA", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(DateTime.Now.ToString());
                            scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = clsInfo.zusuario;
                            scd.Parameters.Add("@IDHISTORICO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDHISTORICO"].ToString());
                            scd.Parameters.Add("@IDCENTROCUSTO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDCENTROCUSTO"].ToString());
                            scd.Parameters.Add("@IDCODIGOCTABIL", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDCODIGOCTABIL"].ToString());
                            if (clsInfo.zbaixatemnotafiscal != "S")
                            {
                                scd.Parameters.Add("@IDNOTAFISCAL", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDNOTAFISCAL"].ToString());
                            }
                            else
                            {
                                scd.Parameters.Add("@IDNOTAFISCAL", SqlDbType.Int).Value = clsInfo.znfvenda;
                            }
                            scd.Parameters.Add("@IDRECEBER", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["ID"].ToString());
                            if (clsInfo.zbaixatemnotafiscal != "S")
                            {
                                scd.Parameters.Add("@IDRECEBERNFV", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDRECEBERNFV"].ToString());
                            }
                            else
                            {
                                scd.Parameters.Add("@IDRECEBERNFV", SqlDbType.Int).Value = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from nfvendareceber where idnota=" + clsInfo.znfvenda));
                            }
                            scd.Parameters.Add("@OBSERVA", SqlDbType.NVarChar).Value = sdrDuplicata["OBSERVA"].ToString();
                            scd.Parameters.Add("@IDFORMAPAGTO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDFORMAPAGTO"].ToString());
                            scd.Parameters.Add("@IDBANCO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDBANCO"].ToString());
                            scd.Parameters.Add("@IDBANCOINT", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDBANCOINT"].ToString());
                            scd.Parameters.Add("@CHEGOU", SqlDbType.NVarChar).Value = sdrDuplicata["CHEGOU"].ToString();
                            scd.Parameters.Add("@DESPESAPUBLICA", SqlDbType.NVarChar).Value = sdrDuplicata["DESPESAPUBLICA"].ToString();
                            scd.Parameters.Add("@BOLETO", SqlDbType.NVarChar).Value = sdrDuplicata["BOLETO"].ToString();
                            scd.Parameters.Add("@BOLETONRO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["BOLETONRO"].ToString());
                            scd.Parameters.Add("@DV", SqlDbType.NVarChar).Value = sdrDuplicata["DV"].ToString();
                            scd.Parameters.Add("@BAIXA", SqlDbType.NVarChar).Value = sdrDuplicata["BAIXA"].ToString();
                            scd.Parameters.Add("@VENCIMENTO", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(sdrDuplicata["VENCIMENTO"].ToString());
                            scd.Parameters.Add("@VALOR", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALOR"].ToString());
                            scd.Parameters.Add("@VALORDESCONTO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORDESCONTO"].ToString());
                            scd.Parameters.Add("@ATEVENCIMENTO", SqlDbType.NVarChar).Value = sdrDuplicata["ATEVENCIMENTO"].ToString();
                            scd.Parameters.Add("@VALORLIQUIDO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORLIQUIDO"].ToString());
                            scd.Parameters.Add("@VALORJUROS", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORJUROS"].ToString());
                            scd.Parameters.Add("@VALORMULTA", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORMULTA"].ToString());
                            scd.Parameters.Add("@IDSITBANCO", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDSITBANCO"].ToString());
                            scd.Parameters.Add("@VALORPAGO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORPAGO"].ToString());
                            scd.Parameters.Add("@VALORBAIXANDO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORBAIXANDO"].ToString());
                            scd.Parameters.Add("@IDVENDEDOR", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDVENDEDOR"].ToString());
                            scd.Parameters.Add("@VALORCOMISSAO", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORCOMISSAO"].ToString());
                            scd.Parameters.Add("@IDSUPERVISOR", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDSUPERVISOR"].ToString());
                            scd.Parameters.Add("@VALORCOMISSAOSUP", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORCOMISSAOSUP"].ToString());
                            scd.Parameters.Add("@IDCOORDENADOR", SqlDbType.Int).Value = clsParser.Int32Parse(sdrDuplicata["IDCOORDENADOR"].ToString());
                            scd.Parameters.Add("@VALORCOMISSAOGER", SqlDbType.Decimal).Value = clsParser.DecimalParse(sdrDuplicata["VALORCOMISSAOGER"].ToString());
                            scd.Parameters.Add("@IMPRIMIR", SqlDbType.NVarChar).Value = sdrDuplicata["IMPRIMIR"].ToString();

                            scn.Open();
                            //scd.ExecuteNonQuery();
                            //idRecePaga = scd.ExecuteScalar();
                            idRecePaga = Int32.Parse(scd.ExecuteScalar().ToString());
                            scn.Close();

                        }
                        sdrRecePaga.Close();
                        // Incluindo os Lançamento do Pagamento
                        baixoucomo = baixoucomo.PadRight(30).Substring(0, 30);
                        idFluxoOld = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "RECEBIDA01",
                                 "IDDUPLICATA,DATAENVIO,DATAOK,IDCOBRANCACOD,IDCOBRANCAHIS,VALOR, " +
                                 "DEBCRED, MOTIVO, IDHISTORICO, IDCENTROCUSTO, IDCODIGOCTABIL, BAIXOUCOMO, " +
                                 "IDFLUXO, IDFLUXO1, VALORCOMISSAO, VALORCOMISSAOGER, VALORCOMISSAOSUP ",
                                 clsParser.SqlInt32Format(idRecePaga.ToString(), false, "IDDUPLICATA") +
                                 clsParser.SqlDateTimeFormat(DataEnvio.ToString().Trim(), false, "DATAENVIO") +
                                 clsParser.SqlDateTimeFormat(DataOk.ToString().Trim(), false, "DATAOK") +
                                 clsParser.SqlInt32Format(idCobrancaCod.ToString(), false, "IDCOBRANCACOD") +
                                 clsParser.SqlInt32Format(idCobrancaHis.ToString(), false, "IDCOBRANCAHIS") +
                                 clsParser.SqlDecimalFormat(Valor.ToString(), false, "VALOR") +
                                 clsParser.SqlStringFormat(DebCred.ToString().Trim(), false, "DEBCRED") +
                                 clsParser.SqlStringFormat("".ToString().Trim(), false, "MOTIVO") +
                                 clsParser.SqlInt32Format(idHistorico.ToString(), false, "IDHISTORICO") +
                                 clsParser.SqlInt32Format(idCentroCusto.ToString(), false, "IDCENTROCUSTO") +
                                 clsParser.SqlInt32Format(idContaContabil.ToString(), false, "IDCODIGOCTABIL") +
                                 clsParser.SqlStringFormat(baixoucomo.ToString().Trim(), false, "BAIXOUCOMO") +
                                 clsParser.SqlInt32Format(0.ToString(), false, "IDFLUXO") +
                                 clsParser.SqlInt32Format(0.ToString(), false, "IDFLUXO1") +
                                 clsParser.SqlDecimalFormat(ValorComissao.ToString(), false, "VALORCOMISSAO") +
                                 clsParser.SqlDecimalFormat(ValorComissaoGer.ToString(), false, "VALORCOMISSAOGER") +
                                 clsParser.SqlDecimalFormat(ValorComissaoSup.ToString(), true, "VALORCOMISSAOSUP"));

                    }
                }
                sdrDuplicata.Close();
            }
            return idFluxoOld;
        }

        // Transferir Para as Observações para o Ctas Pagas // Ctas Recebidas
        public void TransferirPagoObserva(String Documento, Int32 idDocumento)
        /* 
           ' documento qual o documento (PAGAR/RECEBER)
           ' id do Documento
       */
        {
            continuar = "S";
            switch (Documento.Trim().ToUpper())
            {
                case "PAGAR":
                    query = "select * from PAGAROBSERVA WHERE IDDUPLICATA=" + idDocumento + "";
                    query1 = "select * from PAGAS WHERE IDPAGAR=" + idDocumento + "";
                    break;
                case "RECEBER":
                    query = "select * from RECEBEROBSERVA WHERE IDDUPLICATA=" + idDocumento + "";
                    query1 = "select * from RECEBIDA WHERE IDRECEBER=" + idDocumento + "";
                    break;

                default:
                    continuar = "N";
                    break;
            }
            if (continuar == "S")
            {
                // ABRIR AS OBSERVAÇÕES DO CONTAS A PAGAR/RECEBER                
                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtObservacao);
                foreach (DataRow row in dtObservacao.Rows)
                {
//                scn = new SqlConnection(clsInfo.conexaosqldados);
//                scn.Open();
//                scd = new SqlCommand(query, scn);
//                sdrDuplicata = scd.ExecuteReader();
//                if (sdrDuplicata.Read())
//                {  // Duplicata Existe
                    // ABRIR A DUPLICATA PAGA // RECEBIDA               
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    scn.Open();
                    scd = new SqlCommand(query1, scn);
                    sdrRecePaga = scd.ExecuteReader();
                    if (sdrRecePaga.Read())
                    {
                        idRecePaga = clsParser.Int32Parse(sdrRecePaga["ID"].ToString());
                    }
                    else
                    {
                        MessageBox.Show("Se não existe Id da Duplicata Liquidada - então esta com problema");
                    }
                    switch (Documento.Trim().ToUpper())
                    {
                        case "PAGAR":
                            //query2 = "select * from PAGASOBSERVA WHERE IDDUPLICATA=" + idRecePaga + " AND DATA=" + clsParser.SqlDateTimeFormat(sdrDuplicata["DATA"].ToString(), true);
                            query2 = "select * from PAGASOBSERVA WHERE IDDUPLICATA=" + idRecePaga + " AND DATA=" + clsParser.SqlDateTimeFormat(row["DATA"].ToString(), true);
                            break;                                                               
                        case "RECEBER":
                            //query2 = "select * from RECEBIDAOBSERVA WHERE IDDUPLICATA=" + idRecePaga + " AND DATA=" + clsParser.SqlDateTimeFormat(sdrDuplicata["DATA"].ToString(), true);
                            query2 = "select * from RECEBIDAOBSERVA WHERE IDDUPLICATA=" + idRecePaga + " AND DATA=" + clsParser.SqlDateTimeFormat(row["DATA"].ToString(), true);
                            break;                                                                                   
                        default:
                            continuar = "N";
                            break;
                    }

                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    scn.Open();
                    scd = new SqlCommand(query2, scn);
                    sdrRecePaga = scd.ExecuteReader();
                    if (sdrRecePaga.Read())
                    { // JÁ EXISTE ESTA OBSERVAÇÃO

                    }
                    else
                    {
                        // Incluindo os Lançamento da Observação
                        if (Documento.Trim().ToUpper() == "PAGAR")
                        {
                            // incluir a Observação
                            scn = new SqlConnection(clsInfo.conexaosqldados);
                            scn.Open();
                            scd = new SqlCommand("insert into PAGASOBSERVA (" +
                                        "IDDUPLICATA,DATA,OBSERVAR,EMITENTE" +
                                 ") values (" +
                                        "@IDDUPLICATA,@DATA,@OBSERVAR,@EMITENTE " +
                                 ");select SCOPE_IDENTITY()", scn);
                            scd.Parameters.Add("@IDDUPLICATA", SqlDbType.Int).Value = idRecePaga;
                            scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = clsParser.DateTimeParse(row["DATA"].ToString());
                            scd.Parameters.Add("@OBSERVAR", SqlDbType.NVarChar).Value = row["OBSERVAR"].ToString();
                            scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = row["EMITENTE"].ToString();
                            idFluxoOld = clsParser.Int32Parse(scd.ExecuteScalar().ToString());

                        }
                        else
                        {
                            scn = new SqlConnection(clsInfo.conexaosqldados);
                            scn.Open();
                            scd = new SqlCommand("insert into RECEBIDAOBSERVA (" +
                                        "IDDUPLICATA,DATA,OBSERVAR,EMITENTE" +
                                 ") values (" +
                                        "@IDDUPLICATA,@DATA,@OBSERVAR,@EMITENTE " +
                                 ");select SCOPE_IDENTITY()", scn);
                            scd.Parameters.Add("@IDDUPLICATA", SqlDbType.Int).Value = idRecePaga;
                            scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = clsParser.DateTimeParse(row["DATA"].ToString());
                            scd.Parameters.Add("@OBSERVAR", SqlDbType.NVarChar).Value = row["OBSERVAR"].ToString();
                            scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = row["EMITENTE"].ToString();
                            idFluxoOld = clsParser.Int32Parse(scd.ExecuteScalar().ToString());
                        }
                    }
                }
            }
        }
        
        // Extornar do Contas Pagas // Devolver para o Contas a Pagar e Apagar Ctas Pagas
        public void ExtornoPagas(String Documento, Int32 idDocumento, Int32 IdPagarNFE, Int32 IdPagas)
        {
            // Marcar na Nota Fiscal que não foi Pago
            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
            scn.Open();
            scd = new SqlCommand("UPDATE NFCOMPRAPAGAR SET " +
                                    "PAGOU=@PAGOU " +
                                 "WHERE ID=@ID", scn);
            scd.Parameters.Add("@ID", SqlDbType.Int).Value = IdPagarNFE;
            scd.Parameters.Add("@PAGOU", SqlDbType.NVarChar).Value = "N";
            scd.ExecuteNonQuery();
            scn.Close();

            /*clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "NFCOMPRAPAGAR",
                                    "PAGOU = 'N'", "ID = " + IdPagarNFE ); */
            // Incluir o Contas a Pagar da Nota Fiscal
            GravarNotaRecePaga("PAG", idDocumento, IdPagarNFE, IdPagas);
        }

        // Extornar do Contas Pagas // Devolver para o Contas a Pagar e Apagar Ctas Pagas
        //public void ExtornoRecebida(String Documento, Int32 idDocumento, Int32 IdReceberNFV, Int32 IdRecebida)
        //{
        //    // Marcar na Nota Fiscal que não foi Pago
        //    if (Documento.Substring(0, 3).ToUpper() == "CTO")
        //    {
        //        //// Gravar na OrdemServico Fecha 
        //        //SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
        //        //scn.Open();
        //        //scd = new SqlCommand("UPDATE CONTRATORECEBER SET " +
        //        //                        "PAGOU=@PAGOU " +
        //        //                     "WHERE ID=@ID", scn);
        //        //scd.Parameters.Add("@ID", SqlDbType.Int).Value = IdReceberNFV;
        //        //scd.Parameters.Add("@PAGOU", SqlDbType.NVarChar).Value = "N";
        //        //scd.ExecuteNonQuery();
        //        //scn.Close();
        //    }
        //    else if (Documento.Substring(0, 3).ToUpper() == "NFV")
        //    {
        //        //// Gravar na OrdemServico Fecha 
        //        //SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
        //        //scn.Open();
        //        //scd = new SqlCommand("UPDATE NFVENDARECEBER SET " +
        //        //                        "PAGOU=@PAGOU " +
        //        //                     "WHERE ID=@ID", scn);
        //        //scd.Parameters.Add("@ID", SqlDbType.Int).Value = IdReceberNFV;
        //        //scd.Parameters.Add("@PAGOU", SqlDbType.NVarChar).Value = "N";
        //        //scd.ExecuteNonQuery();
        //        //scn.Close();

        //    }
        //    else
        //    {
        //        MessageBox.Show("Documento não Preparado para Extornar");
        //    }
        //    // Incluir o Contas a Receber Novamente
        //    // GravarNotaRecePaga(Documento, idDocumento, IdReceberNFV, IdRecebida);
        //}

        // Incluir uma Nova Duplicata no Contas a Pagar 
        public void GravarNotaRecePaga(String Documento, Int32 idDocumento, Int32 idPagarNFE, Int32 idPagas)
        {
            //using (TransactionScope tse = new TransactionScope())  // dava bug em 23/08/16
            //{
                SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
                Int32 idHistorico = 0;
                Int32 idCentroCusto = 0;
                Int32 idCodigoctabil= 0;
                Int32 idCodigoDocumento = 0;
                continuar = "S";
                switch (Documento.Trim().Substring(0,3).ToUpper())
                {
                    case "PAG":

                        idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFCOMPRA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                        idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFCOMPRA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                        idCodigoctabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDCODIGOCTABIL FROM NFCOMPRA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));

                        query = "select * from NFCOMPRA WHERE ID=" + idDocumento + "";
                        query1 = "select * from NFCOMPRAPAGAR WHERE IDNOTA=" + idDocumento + "";
                        break;
                    case "NFV":
                        idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFVENDA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                        idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFVENDA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                        idCodigoctabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDCODIGOCTABIL FROM NFVENDA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));

                        // Gravar na OrdemServico Fecha 
                        //SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
                        scn.Open();
                        scd = new SqlCommand("UPDATE NFVENDARECEBER SET " +
                                                "PAGOU=@PAGOU " +
                                             "WHERE ID=@ID", scn);
                        scd.Parameters.Add("@ID", SqlDbType.Int).Value = idPagarNFE; //IdReceberNFV;
                        scd.Parameters.Add("@PAGOU", SqlDbType.NVarChar).Value = "N";
                        scd.ExecuteNonQuery();
                        scn.Close();


                        query = "select * from NFVENDA WHERE ID=" + idDocumento + "";
                        query1 = "select * from NFVENDARECEBER WHERE IDNOTA=" + idDocumento + "";
                        break;
                    case "REC":
                        idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFVENDA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                        idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFVENDA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                        idCodigoctabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDCODIGOCTABIL FROM NFVENDA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));

                        query = "select * from NFVENDA WHERE ID=" + idDocumento + "";
                        query1 = "select * from NFVENDARECEBER WHERE IDNOTA=" + idDocumento + "";
                        break;
                    case "CTO":
                        idHistorico = clsInfo.zhistoricos;  //Parser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM CONTRATO1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                        idCentroCusto = clsInfo.zcentrocustos; //Parser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM CONTRATO1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                        idCodigoctabil = clsInfo.zcontacontabil;  //Parser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDCODIGOCTABIL FROM CONTRATO1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                        idCodigoDocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + Documento + "'").ToString());

                        // Gravar na OrdemServico Fecha 
                        //SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
                        scn.Open();
                        scd = new SqlCommand("UPDATE CONTRATORECEBER SET " +
                                                "PAGOU=@PAGOU " +
                                             "WHERE ID=@ID", scn);
                        scd.Parameters.Add("@ID", SqlDbType.Int).Value = idPagarNFE; //IdReceberNFV;
                        scd.Parameters.Add("@PAGOU", SqlDbType.NVarChar).Value = "N";
                        scd.ExecuteNonQuery();
                        scn.Close();

                        query = "select * from CONTRATO WHERE ID=" + idDocumento + "";
                        query1 = "select * from CONTRATORECEBER WHERE IDCONTRATO=" + idDocumento + "";
                        break;

                    default:
                        continuar = "N";
                        break;
                }
                if (continuar == "S")
                {
                    // ABRIR O Documento Original
                    SqlDataAdapter sda;
                    DataTable dtDocumen = new DataTable();
                    sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                    sda.Fill(dtDocumen);
                    if (dtDocumen.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtDocumen.Rows)
                        {   // Passar o Documento para 1 Linha
                            // ABRIR AS NFCOMPRAPAGAR/NFVENDARECEBER/CONTRATORECEBER
                            DataTable dtDocumenPagRec = new DataTable();
                            sda = new SqlDataAdapter(query1, clsInfo.conexaosqldados);
                            sda.Fill(dtDocumenPagRec);
                            foreach (DataRow rowDocPago in dtDocumenPagRec.Rows)
                            {
                                if (rowDocPago["PAGOU"].ToString().Trim() != "S")
                                {
                                    if (Documento.Trim().Substring(0, 3).ToUpper() == "PAG")
                                    {
                                        // Puxar os dados que vão a parte
                                        //Int32 idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFCOMPRA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                                        //Int32 idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFCOMPRA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                                        //Int32 idCodigoctabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDCODIGOCTABIL FROM NFCOMPRA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));

                                        query2 = "select * from PAGAR WHERE IDPAGARNFE=" + clsParser.Int32Parse(rowDocPago["ID"].ToString()) + "";
                                        // Verificar se existe a Duplicata no contas a pagar
                                        scn1 = new SqlConnection(clsInfo.conexaosqldados);
                                        scn1.Open();
                                        scd1 = new SqlCommand(query2, scn1);
                                        sdrDuplicata = scd1.ExecuteReader();
                                        if (sdrDuplicata.Read())
                                        {   // Duplicata Existe
                                            // Deixar como esta
                                            idPagarNew = clsParser.Int32Parse(sdrDuplicata["ID"].ToString().Trim());
                                        }
                                        else
                                        {
                                            // incluir a duplicata a Pagar
                                            scn = new SqlConnection(clsInfo.conexaosqldados);
                                            scn.Open();
                                            scd = new SqlCommand("insert into PAGAR (" +
                                                        "atevencimento,baixa,boleto,boletonro,chegou,datalanca,despesapublica,duplicata,dv,emissao,emitente,filial," +
                                                        "idbanco,idbancoint,idcentrocusto,idcodigoctabil,iddocumento,idformapagto,idfornecedor,idhistorico,idnotafiscal," +
                                                        "idpagarnfe,idsitbanco,imprimir,observa,posicao,posicaofim,setor,valor,valorbaixando,valordesconto," +
                                                        "valorliquido,valorjuros,valormulta,valorpago,vencimento,vencimentoprev" +
                                                 ") values (" +
                                                        "@atevencimento,@baixa,@boleto,@boletonro,@chegou,@datalanca,@despesapublica,@duplicata,@dv,@emissao,@emitente,@filial," +
                                                        "@idbanco,@idbancoint,@idcentrocusto,@idcodigoctabil,@iddocumento,@idformapagto,@idfornecedor,@idhistorico,@idnotafiscal," +
                                                        "@idpagarnfe,@idsitbanco,@imprimir,@observa,@posicao,@posicaofim,@setor,@valor,@valorbaixando,@valordesconto," +
                                                        "@valorliquido,@valorjuros,@valormulta,@valorpago,@vencimento,@vencimentoprev " +
                                                 ");select SCOPE_IDENTITY()", scn);

                                            scd.Parameters.Add("@atevencimento", SqlDbType.NVarChar).Value = "S";
                                            scd.Parameters.Add("@baixa", SqlDbType.NVarChar).Value = "N";
                                            scd.Parameters.Add("@boleto", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@boletonro", SqlDbType.Decimal).Value = clsParser.DecimalParse(rowDocPago["BOLETONRO"].ToString());
                                            scd.Parameters.Add("@chegou", SqlDbType.NVarChar).Value = "S";
                                            scd.Parameters.Add("@datalanca", SqlDbType.DateTime).Value = DateTime.Now;
                                            scd.Parameters.Add("@despesapublica", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@duplicata", SqlDbType.Int).Value = clsParser.Int64Parse(row["NUMERO"].ToString());
                                            scd.Parameters.Add("@dv", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@emissao", SqlDbType.DateTime).Value = clsParser.DateTimeParse(row["DATA"].ToString());
                                            scd.Parameters.Add("@emitente", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@filial", SqlDbType.Int).Value = clsParser.Int32Parse(row["FILIAL"].ToString());
                                            scd.Parameters.Add("@idbanco", SqlDbType.Int).Value = clsInfo.zbanco;
                                            scd.Parameters.Add("@idbancoint", SqlDbType.Int).Value = clsInfo.zbancoint;
                                            if (idCentroCusto == 0)
                                            {
                                                idCentroCusto = clsInfo.zcentrocustos;
                                            }
                                            scd.Parameters.Add("@idcentrocusto", SqlDbType.Int).Value = idCentroCusto;
                                            if (idCodigoctabil == 0)
                                            {
                                                idCodigoctabil = clsInfo.zcontacontabil;
                                            }
                                            scd.Parameters.Add("@idcodigoctabil", SqlDbType.Int).Value = idCodigoctabil;
                                            scd.Parameters.Add("@iddocumento", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDDOCUMENTO"].ToString());
                                            scd.Parameters.Add("@idformapagto", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["IDTIPOPAGA"].ToString());
                                            scd.Parameters.Add("@idfornecedor", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDFORNECEDOR"].ToString());
                                            scd.Parameters.Add("@idhistorico", SqlDbType.Int).Value = idHistorico;
                                            scd.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());

                                            scd.Parameters.Add("@idpagarnfe", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["ID"].ToString());
                                            scd.Parameters.Add("@idsitbanco", SqlDbType.Int).Value = clsInfo.zsituacaotitulo;
                                            scd.Parameters.Add("@imprimir", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@observa", SqlDbType.NVarChar).Value = row["OBSERVA"].ToString();
                                            scd.Parameters.Add("@posicao", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["POSICAO"].ToString());
                                            scd.Parameters.Add("@posicaofim", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["POSICAOFIM"].ToString());

                                            scd.Parameters.Add("@setor", SqlDbType.NVarChar).Value = row["SETOR"].ToString().Trim();
                                            scd.Parameters.Add("@valor", SqlDbType.Decimal).Value = clsParser.DecimalParse(rowDocPago["VALOR"].ToString());
                                            scd.Parameters.Add("@valorbaixando", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valordesconto", SqlDbType.Decimal).Value = 0;
                                            //scd.Parameters.Add("@valordevolvido", SqlDbType.Decimal).Value = 0;

                                            scd.Parameters.Add("@valorliquido", SqlDbType.Decimal).Value = clsParser.DecimalParse(rowDocPago["VALOR"].ToString());
                                            scd.Parameters.Add("@valorjuros", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valormulta", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valorpago", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@vencimento", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(rowDocPago["DATA"].ToString());
                                            scd.Parameters.Add("@vencimentoprev", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(rowDocPago["DATA"].ToString());
                                            //scd.ExecuteNonQuery();
                                            idPagarNew = clsParser.Int32Parse(scd.ExecuteScalar().ToString());


                                        }
                                    }
                                    else if (Documento.Trim().Substring(0, 3).ToUpper() == "REC" || Documento.Trim().Substring(0, 3).ToUpper() == "NFV")
                                    {
                                        // Puxar os dados que vão a parte
                                        //Int32 idHistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFVENDA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                                        //Int32 idCentroCusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM NFVENDA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                                        //Int32 idCodigoctabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDCODIGOCTABIL FROM NFVENDA1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));

                                        query2 = "select * from RECEBER WHERE IDRECEBERNFV=" + clsParser.Int32Parse(rowDocPago["ID"].ToString()) + "";
                                        // Verificar se existe a Duplicata no contas a pagar
                                        scn1 = new SqlConnection(clsInfo.conexaosqldados);
                                        scn1.Open();
                                        scd1 = new SqlCommand(query2, scn1);
                                        sdrDuplicata = scd1.ExecuteReader();
                                        if (sdrDuplicata.Read())
                                        {   // Duplicata Existe
                                            // Deixar como esta
                                            idPagarNew = clsParser.Int32Parse(sdrDuplicata["ID"].ToString().Trim());
                                        }
                                        else
                                        {
                                            // incluir a duplicata a Pagar
                                            scn = new SqlConnection(clsInfo.conexaosqldados);
                                            scn.Open();

                                            scd = new SqlCommand("insert into RECEBER (" +
                                                        "atevencimento,baixa,boleto,boletonro,chegou,contrato,datalanca,despesapublica,duplicata,dv,emissao,emitente,filial," +
                                                        "idbanco,idbancoint,idcentrocusto,idcliente,idcodigoctabil,iddocumento,idformapagto,idhistorico,idnotafiscal," +
                                                        "idrecebernfv,idsitbanco,idsupervisor,idvendedor,imprimir,observa,posicao,posicaofim,transfebanco,transfenumero, setor,valor,valorbaixando, " +
                                                        "valorcomissao, valorcomissaoger, valorcomissaosup, valordesconto," +
                                                        "valorliquido,valorjuros,valormulta,valorpago,vencimento,vencimentoprev " +
                                                 ") values (" +
                                                        "@atevencimento,@baixa,@boleto,@boletonro,@chegou,@contrato,@datalanca,@despesapublica,@duplicata,@dv,@emissao,@emitente,@filial," +
                                                        "@idbanco,@idbancoint,@idcentrocusto,@idcliente,@idcodigoctabil,@iddocumento,@idformapagto,@idhistorico,@idnotafiscal," +
                                                        "@idpagarnfe,@idsitbanco,@idsupervisor,@idvendedor,@imprimir,@observa,@posicao,@posicaofim,@transfebanco,@transfenumero, @setor,@valor,@valorbaixando, " +
                                                        "@valorcomissao, @valorcomissaoger, @valorcomissaosup,@valordesconto," +
                                                        "@valorliquido,@valorjuros,@valormulta,@valorpago,@vencimento,@vencimentoprev " +
                                                 ");select SCOPE_IDENTITY()", scn);
                                            scd.Parameters.Add("@atevencimento", SqlDbType.NVarChar).Value = "S";
                                            scd.Parameters.Add("@baixa", SqlDbType.NVarChar).Value = "N";
                                            scd.Parameters.Add("@boleto", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@boletonro", SqlDbType.Decimal).Value = clsParser.DecimalParse(rowDocPago["BOLETONRO"].ToString());
                                            scd.Parameters.Add("@chegou", SqlDbType.NVarChar).Value = "S";
                                            scd.Parameters.Add("@contrato", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@datalanca", SqlDbType.DateTime).Value = DateTime.Now;
                                            scd.Parameters.Add("@despesapublica", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@duplicata", SqlDbType.Int).Value = clsParser.Int64Parse(row["NUMERO"].ToString());
                                            scd.Parameters.Add("@dv", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@emissao", SqlDbType.DateTime).Value = clsParser.DateTimeParse(row["DATA"].ToString());
                                            scd.Parameters.Add("@emitente", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@filial", SqlDbType.Int).Value = clsParser.Int32Parse(row["FILIAL"].ToString());
                                            scd.Parameters.Add("@idbanco", SqlDbType.Int).Value = clsInfo.zbanco;
                                            scd.Parameters.Add("@idbancoint", SqlDbType.Int).Value = clsInfo.zbancoint;
                                            scd.Parameters.Add("@idcentrocusto", SqlDbType.Int).Value = idCentroCusto;
                                            scd.Parameters.Add("@idcliente", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDCLIENTE"].ToString());
                                            scd.Parameters.Add("@idcodigoctabil", SqlDbType.Int).Value = idCodigoctabil;
                                            scd.Parameters.Add("@iddocumento", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDDOCUMENTO"].ToString());
                                            scd.Parameters.Add("@idformapagto", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["IDTIPOPAGA"].ToString());
                                            scd.Parameters.Add("@idhistorico", SqlDbType.Int).Value = idHistorico;
                                            scd.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());

                                            scd.Parameters.Add("@idpagarnfe", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["ID"].ToString());
                                            scd.Parameters.Add("@idsitbanco", SqlDbType.Int).Value = clsInfo.zsituacaotitulo;
                                            scd.Parameters.Add("@idsupervisor", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDSUPERVISOR"].ToString());
                                            scd.Parameters.Add("@idvendedor", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDVENDEDOR"].ToString());
                                            scd.Parameters.Add("@imprimir", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@observa", SqlDbType.NVarChar).Value = row["OBSERVA"].ToString();
                                            scd.Parameters.Add("@posicao", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["POSICAO"].ToString());
                                            scd.Parameters.Add("@posicaofim", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["POSICAOFIM"].ToString());

                                            scd.Parameters.Add("@setor", SqlDbType.NVarChar).Value = row["SETOR"].ToString().Trim();
                                            scd.Parameters.Add("@transfebanco", SqlDbType.NVarChar).Value = "N";
                                            scd.Parameters.Add("@transfenumero", SqlDbType.Int).Value = 0;
                                            scd.Parameters.Add("@valor", SqlDbType.Decimal).Value = clsParser.DecimalParse(rowDocPago["VALOR"].ToString());
                                            scd.Parameters.Add("@valorbaixando", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valorcomissao", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valorcomissaoger", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valorcomissaosup", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valordesconto", SqlDbType.Decimal).Value = 0;

                                            scd.Parameters.Add("@valorliquido", SqlDbType.Decimal).Value = clsParser.DecimalParse(rowDocPago["VALOR"].ToString());
                                            scd.Parameters.Add("@valorjuros", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valormulta", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valorpago", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@vencimento", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(rowDocPago["DATA"].ToString());
                                            scd.Parameters.Add("@vencimentoprev", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(rowDocPago["DATA"].ToString());
                                            //scd.ExecuteNonQuery();
                                            idPagarNew = clsParser.Int32Parse(scd.ExecuteScalar().ToString());
                                        }
                                    }
                                    else if (Documento.Trim().Substring(0, 3).ToUpper() == "CTO")
                                    {
                                        // Puxar os dados que vão a parte
                                        //Int32 idHistorico = clsInfo.zhistoricos;  //Parser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM CONTRATO1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                                        //Int32 idCentroCusto = clsInfo.zcentrocustos; //Parser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDHISTORICO FROM CONTRATO1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                                        //Int32 idCodigoctabil = clsInfo.zcontacontabil;  //Parser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDCODIGOCTABIL FROM CONTRATO1 WHERE ID = " + clsParser.Int32Parse(idDocumento.ToString())));
                                        //Int32 idCodigoDocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DOCFISCAL where COGNOME='" + Documento + "'").ToString());

                                        query2 = "select * from RECEBER WHERE IDRECEBERNFV=" + clsParser.Int32Parse(rowDocPago["ID"].ToString()) + "";
                                        // Verificar se existe a Duplicata no contas a pagar
                                        scn1 = new SqlConnection(clsInfo.conexaosqldados);
                                        scn1.Open();
                                        scd1 = new SqlCommand(query2, scn1);
                                        sdrDuplicata = scd1.ExecuteReader();
                                        if (sdrDuplicata.Read())
                                        {   // Duplicata Existe
                                            // Deixar como esta
                                            idPagarNew = clsParser.Int32Parse(sdrDuplicata["ID"].ToString().Trim());
                                        }
                                        else
                                        {
                                            // incluir a duplicata a Receber
                                            scn = new SqlConnection(clsInfo.conexaosqldados);
                                            scn.Open();

                                            scd = new SqlCommand("insert into RECEBER (" +
                                                        "atevencimento,baixa,boleto,boletonro,chegou,contrato,datalanca,despesapublica,duplicata,dv,emissao,emitente,filial," +
                                                        "idbanco,idbancoint,idcentrocusto,idcliente,idcodigoctabil,iddocumento,idformapagto,idhistorico,idnotafiscal," +
                                                        "idrecebernfv,idsitbanco,idsupervisor,idvendedor,imprimir,observa,posicao,posicaofim,transfebanco,transfenumero, setor,valor,valorbaixando, " +
                                                        "valorcomissao, valorcomissaoger, valorcomissaosup, valordesconto," +
                                                        "valorliquido,valorjuros,valormulta,valorpago,vencimento,vencimentoprev " +
                                                 ") values (" +
                                                        "@atevencimento,@baixa,@boleto,@boletonro,@chegou,@contrato,@datalanca,@despesapublica,@duplicata,@dv,@emissao,@emitente,@filial," +
                                                        "@idbanco,@idbancoint,@idcentrocusto,@idcliente,@idcodigoctabil,@iddocumento,@idformapagto,@idhistorico,@idnotafiscal," +
                                                        "@idpagarnfe,@idsitbanco,@idsupervisor,@idvendedor,@imprimir,@observa,@posicao,@posicaofim,@transfebanco,@transfenumero, @setor,@valor,@valorbaixando, " +
                                                        "@valorcomissao, @valorcomissaoger, @valorcomissaosup,@valordesconto, " +
                                                        "@valorliquido,@valorjuros,@valormulta,@valorpago,@vencimento,@vencimentoprev " +
                                                 ");select SCOPE_IDENTITY()", scn);
                                            scd.Parameters.Add("@atevencimento", SqlDbType.NVarChar).Value = "S";
                                            scd.Parameters.Add("@baixa", SqlDbType.NVarChar).Value = "N";
                                            scd.Parameters.Add("@boleto", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@boletonro", SqlDbType.Decimal).Value = clsParser.DecimalParse(rowDocPago["BOLETONRO"].ToString());
                                            scd.Parameters.Add("@chegou", SqlDbType.NVarChar).Value = "S";
                                            scd.Parameters.Add("@contrato", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@datalanca", SqlDbType.DateTime).Value = DateTime.Now;
                                            scd.Parameters.Add("@despesapublica", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@duplicata", SqlDbType.Int).Value = clsParser.Int64Parse(row["NUMERO"].ToString());
                                            scd.Parameters.Add("@dv", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@emissao", SqlDbType.DateTime).Value = clsParser.DateTimeParse(row["DATA"].ToString());
                                            scd.Parameters.Add("@emitente", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@filial", SqlDbType.Int).Value = clsParser.Int32Parse(row["FILIAL"].ToString());
                                            scd.Parameters.Add("@idbanco", SqlDbType.Int).Value = clsInfo.zbanco;
                                            scd.Parameters.Add("@idbancoint", SqlDbType.Int).Value = clsInfo.zbancoint;
                                            scd.Parameters.Add("@idcentrocusto", SqlDbType.Int).Value = idCentroCusto;
                                            scd.Parameters.Add("@idcliente", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDCLIENTE"].ToString());
                                            scd.Parameters.Add("@idcodigoctabil", SqlDbType.Int).Value = idCodigoctabil;
                                            scd.Parameters.Add("@iddocumento", SqlDbType.Int).Value = idCodigoDocumento;

                                            scd.Parameters.Add("@idformapagto", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["IDTIPOPAGA"].ToString());
                                            scd.Parameters.Add("@idhistorico", SqlDbType.Int).Value = idHistorico;
                                            scd.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());

                                            scd.Parameters.Add("@idpagarnfe", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["ID"].ToString());
                                            scd.Parameters.Add("@idsitbanco", SqlDbType.Int).Value = clsInfo.zsituacaotitulo;
                                            scd.Parameters.Add("@idsupervisor", SqlDbType.Int).Value = clsInfo.zempresaclienteid;
                                            scd.Parameters.Add("@idvendedor", SqlDbType.Int).Value = clsParser.Int32Parse(row["IDVENDEDOR"].ToString());
                                            scd.Parameters.Add("@imprimir", SqlDbType.NVarChar).Value = "";
                                            scd.Parameters.Add("@observa", SqlDbType.NVarChar).Value = row["OBSERVA"].ToString();
                                            scd.Parameters.Add("@posicao", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["POSICAO"].ToString());
                                            scd.Parameters.Add("@posicaofim", SqlDbType.Int).Value = clsParser.Int32Parse(rowDocPago["POSICAOFIM"].ToString());

                                            scd.Parameters.Add("@setor", SqlDbType.NVarChar).Value = row["SETOR"].ToString().Trim();
                                            scd.Parameters.Add("@transfebanco", SqlDbType.NVarChar).Value = "N";
                                            scd.Parameters.Add("@transfenumero", SqlDbType.Int).Value = 0;
                                            scd.Parameters.Add("@valor", SqlDbType.Decimal).Value = clsParser.DecimalParse(rowDocPago["VALOR"].ToString());
                                            scd.Parameters.Add("@valorbaixando", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valorcomissao", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valorcomissaoger", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valorcomissaosup", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valordesconto", SqlDbType.Decimal).Value = 0;

                                            scd.Parameters.Add("@valorliquido", SqlDbType.Decimal).Value = clsParser.DecimalParse(rowDocPago["VALOR"].ToString());
                                            scd.Parameters.Add("@valorjuros", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valormulta", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@valorpago", SqlDbType.Decimal).Value = 0;
                                            scd.Parameters.Add("@vencimento", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(rowDocPago["DATA"].ToString());
                                            scd.Parameters.Add("@vencimentoprev", SqlDbType.DateTime).Value = clsParser.SqlDateTimeParse(rowDocPago["DATA"].ToString());
                                            //scd.ExecuteNonQuery();
                                            idPagarNew = clsParser.Int32Parse(scd.ExecuteScalar().ToString());
                                        }
                                    }
                                }
                            }
                            // Devolver as Observações do Ctas Pagas para o Contas a Pagar
                            // Observações do Contas Pagas - Devolver ao Ctas a Pagar
                            SqlConnection scn2;
                            SqlCommand scd2;
                            if (Documento.Trim().Substring(0, 3).ToUpper() == "PAG")
                            {
                                DataTable dtPagasObserva = new DataTable();
                                query1 = "SELECT * FROM PAGASOBSERVA WHERE IDDUPLICATA= " + idPagas + "";
                                sda = new SqlDataAdapter(query1, clsInfo.conexaosqldados);
                                sda.Fill(dtPagasObserva);
                                foreach (DataRow rowPagasObs in dtPagasObserva.Rows)
                                {
                                    String DataAtual = (clsParser.DateTimeParse(rowPagasObs["DATA"].ToString())).ToString("dd/MM/yyyy");
                                    DataAtual = DataAtual + " " + 
                                                rowPagasObs["DATA"].ToString().Substring(11,5);

                                    //query2 = "select * from PAGAROBSERVA WHERE IDDUPLICATA=" + idPagarNew + " AND DATA =" + clsParser.SqlDateTimeParse(rowPagasObs["DATA"].ToString());
                                    //Parser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                                    query2 = "select * from PAGAROBSERVA WHERE IDDUPLICATA=" + idPagarNew + " AND DATA =" + clsParser.SqlDateTimeFormat(DataAtual, true);

                                    // Verificar se existe a Duplicata no contas a pagar
                                    scn1 = new SqlConnection(clsInfo.conexaosqldados);
                                    scn1.Open();
                                    scd1 = new SqlCommand(query2, scn1);
                                    sdrDuplicata = scd1.ExecuteReader();
                                    if (sdrDuplicata.Read())
                                    {   // Observação Existe
                                        // Deixar como esta
                                        idPagarObserva = clsParser.Int32Parse(sdrDuplicata["ID"].ToString().Trim());
                                    }
                                    else
                                    {
                                        // incluir a Observação
                                        scn = new SqlConnection(clsInfo.conexaosqldados);
                                        scn.Open();
                                        scd = new SqlCommand("insert into PAGAROBSERVA (" +
                                                    "IDDUPLICATA,DATA,OBSERVAR,EMITENTE" +
                                             ") values (" +
                                                    "@IDDUPLICATA,@DATA,@OBSERVAR,@EMITENTE " +
                                             ");select SCOPE_IDENTITY()", scn);
                                        scd.Parameters.Add("@IDDUPLICATA", SqlDbType.Int).Value = idPagarNew;
                                        scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = clsParser.DateTimeParse(rowPagasObs["DATA"].ToString());
                                        scd.Parameters.Add("@OBSERVAR", SqlDbType.NVarChar).Value = rowPagasObs["OBSERVAR"].ToString();
                                        scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = rowPagasObs["EMITENTE"].ToString();
                                        idPagarObserva = clsParser.Int32Parse(scd.ExecuteScalar().ToString());

                                    }
                                }
                                // Apagar do Ctas Pagas (Observação)
                                scn2 = new SqlConnection(clsInfo.conexaosqldados);
                                scd2 = new SqlCommand("DELETE PAGASOBSERVA WHERE IDDUPLICATA=@id", scn2);
                                scd2.Parameters.Add("@id", SqlDbType.Int).Value = idPagas;
                                scn2.Open();
                                scd2.ExecuteNonQuery();
                                scn2.Close();

                                // Apagar do Ctas Pagas (Itens Baixados)
                                scn2 = new SqlConnection(clsInfo.conexaosqldados);
                                scd2 = new SqlCommand("DELETE PAGAS01 WHERE IDDUPLICATA=@id", scn2);
                                scd2.Parameters.Add("@id", SqlDbType.Int).Value = idPagas;
                                scn2.Open();
                                scd2.ExecuteNonQuery();
                                scn2.Close();

                                // Apagar do Ctas Pagas (Registro)
                                scn2 = new SqlConnection(clsInfo.conexaosqldados);
                                scd2 = new SqlCommand("DELETE PAGAS WHERE ID=@id", scn2);
                                scd2.Parameters.Add("@id", SqlDbType.Int).Value = idPagas;
                                scn2.Open();
                                scd2.ExecuteNonQuery();
                                scn2.Close();
                            }
                            else
                            {
                                DataTable dtPagasObserva = new DataTable();
                                query1 = "SELECT * FROM RECEBIDAOBSERVA WHERE IDDUPLICATA= " + idPagas + "";
                                sda = new SqlDataAdapter(query1, clsInfo.conexaosqldados);
                                sda.Fill(dtPagasObserva);
                                foreach (DataRow rowPagasObs in dtPagasObserva.Rows)
                                {
                                    String DataAtual = (clsParser.DateTimeParse(rowPagasObs["DATA"].ToString())).ToString("dd/MM/yyyy");
                                    DataAtual = DataAtual + " " +
                                                rowPagasObs["DATA"].ToString().Substring(11, 5);
                                    query2 = "select * from RECEBEROBSERVA WHERE IDDUPLICATA=" + idPagarNew + " AND DATA =" + clsParser.SqlDateTimeFormat(DataAtual, true);
                                    //query2 = "select * from RECEBEROBSERVA WHERE IDDUPLICATA=" + idPagarNew  + " AND DATA =" + clsParser.SqlDateTimeParse(rowPagasObs["DATA"].ToString());
                                    // Verificar se existe a Duplicata no contas a receber
                                    scn1 = new SqlConnection(clsInfo.conexaosqldados);
                                    scn1.Open();
                                    scd1 = new SqlCommand(query2, scn1);
                                    sdrDuplicata = scd1.ExecuteReader();
                                    if (sdrDuplicata.Read())
                                    {   // Observação Existe
                                        // Deixar como esta
                                        idPagarObserva = clsParser.Int32Parse(sdrDuplicata["ID"].ToString().Trim());
                                    }
                                    else
                                    {
                                        // incluir a Observação
                                        scn = new SqlConnection(clsInfo.conexaosqldados);

                                        scn = new SqlConnection(clsInfo.conexaosqldados);
                                        scn.Open();

                                        scd = new SqlCommand("insert into RECEBEROBSERVA (" +
                                                    "IDDUPLICATA,DATA,OBSERVAR,EMITENTE" +
                                             ") values (" +
                                                    "@IDDUPLICATA,@DATA,@OBSERVAR,@EMITENTE " +
                                             ");select SCOPE_IDENTITY()", scn);
                                        scd.Parameters.Add("@IDDUPLICATA", SqlDbType.Int).Value = idPagarNew;
                                        scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = clsParser.DateTimeParse(rowPagasObs["DATA"].ToString());
                                        scd.Parameters.Add("@OBSERVAR", SqlDbType.NVarChar).Value = rowPagasObs["OBSERVAR"].ToString();
                                        scd.Parameters.Add("@EMITENTE", SqlDbType.NVarChar).Value = rowPagasObs["EMITENTE"].ToString();
                                        idPagarObserva = clsParser.Int32Parse(scd.ExecuteScalar().ToString());
                                    }
                                }
                                // Apagar do Ctas Recebidas (Observação)
                                scn2 = new SqlConnection(clsInfo.conexaosqldados);
                                scd2 = new SqlCommand("DELETE RECEBIDAOBSERVA WHERE IDDUPLICATA=@id", scn2);
                                scd2.Parameters.Add("@id", SqlDbType.Int).Value = idPagas;
                                scn2.Open();
                                scd2.ExecuteNonQuery();
                                scn2.Close();

                                // Apagar do Ctas Recebidas (Itens Baixados)
                                scn2 = new SqlConnection(clsInfo.conexaosqldados);
                                scd2 = new SqlCommand("DELETE RECEBIDA01 WHERE IDDUPLICATA=@id", scn2);
                                scd2.Parameters.Add("@id", SqlDbType.Int).Value = idPagas;
                                scn2.Open();
                                scd2.ExecuteNonQuery();
                                scn2.Close();

                                // Apagar do Ctas Recebidas (Registro)
                                scn2 = new SqlConnection(clsInfo.conexaosqldados);
                                scd2 = new SqlCommand("DELETE RECEBIDA WHERE ID=@id", scn2);
                                scd2.Parameters.Add("@id", SqlDbType.Int).Value = idPagas;
                                scn2.Open();
                                scd2.ExecuteNonQuery();
                                scn2.Close();
                            }
                        }
                    }
                //}
                //tse.Complete();
            }

        }

        /// <summary>
        /// Gera um DataTable das duplicatas a partir de uma Condição de Pagamento.
        /// </summary>
        /// <param name="data">Data base das Duplicatas.</param>
        /// <param name="total">Total da Fatura.</param>
        /// <param name="idformapagto">Identificação do registro da Forma de Pagamento.</param>
        /// <param name="idcondpagto">Identificação do registro da Condição de Pagamento.</param>
        /// <returns></returns>
        public static DataTable GerarFatura(DateTime data, 
                                                Decimal total,
                                                Int32 idformapagto,
                                                Int32 idcondpagto)
        {
            return GerarFatura(data, total, 0, 0, 0, false, 0, false, 0, idformapagto, idcondpagto,"N", "S");
        }

        /// <summary>
        /// Gera um DataTable das duplicatas a partir de uma Condição de Pagamento.
        /// </summary>
        /// <param name="data">Data base das Duplicatas.</param>
        /// <param name="total">Total da Fatura.</param>
        /// <param name="comissao">Total da Comissão.</param>
        /// <param name="comissaoger">Total da Comissão Gerencia.</param>
        /// <param name="comissaosup">Total da Comissão Supervisor</param>
        /// <param name="ipi">Total do IPI.</param>
        /// <param name="totalipi">Fatura ou não na primeira parcela.</param>
        /// <param name="st">Total da Substituição Tributária.</param>
        /// <param name="totalst">Fatura ou não na primeira parcela.</param>
        /// <param name="pispasep">Fatura ou não o valor do Pis/Pasep. (se 'false' desconta o valor da variável 'total')</param>
        /// <param name="totalpis">Total do Pis/Pasep.</param>
        /// <param name="cofins">Fatura ou não o valor do Cofins. (se 'false' desconta o valor da variável 'total')</param>
        /// <param name="totalcofins">Total do Cofins.</param>
        /// <param name="idformapagto">Identificação do registro da Forma de Pagamento.</param>
        /// <param name="idcondpagto">Identificação do registro da Condição de Pagamento.</param>
        /// <returns></returns>
        public static DataTable GerarFatura(DateTime data,
                                         Decimal total,
                                         Decimal comissao,
                                         Decimal totalipi,
                                         Decimal totalst,
                                         Boolean pispasep,
                                         Decimal totalpis,
                                         Boolean cofins,
                                         Decimal totalcofins,
                                         Int32 idformapagto,
                                         Int32 idcondpagto,
                                         String fixardiareferencia,
                                         String calculajuros)
        {
            SqlDataAdapter sda;

            DataRow rowCondpagto;
            DataRow rowDuplicata;

            DataTable dtDuplicatas = new DataTable();
            DataTable dtFeriados = new DataTable();
            DataTable dtCondPagto = new DataTable();

            List<Decimal> parcelasvalor = new List<Decimal>();
            List<Decimal> parcelasvalorcomissao = new List<Decimal>();

            Decimal juros = 0;
            Decimal parcelastotal = 0;
            Decimal comissaototal = 0;
            Decimal valorx = 0;
            Decimal valorxcomi = 0;
            Decimal porx = 0;
            Int32 parcelas = 0;
            Int32 x = 0;
            Int32 diacai1 = 0;
            Int32 diacai2 = 0;
            Int32 diacai3 = 0;
            Int32 diacai4 = 0;

            DateTime tmp_data = DateTime.Now;
            DateTime tmp_data_ant = DateTime.Now;

            // Criar a Tabela Virtual para Armazenar os dados
            dtDuplicatas = new DataTable();
            dtDuplicatas.Columns.Add("posicao", Type.GetType("System.Int32"));
            dtDuplicatas.Columns.Add("posicaofim", Type.GetType("System.Int32"));
            dtDuplicatas.Columns.Add("pagou", Type.GetType("System.String"));
            dtDuplicatas.Columns.Add("data", Type.GetType("System.DateTime"));
            dtDuplicatas.Columns.Add("valor", Type.GetType("System.Decimal"));
            dtDuplicatas.Columns.Add("valorcomissao", Type.GetType("System.Decimal"));
            dtDuplicatas.Columns.Add("valorcomissaoger", Type.GetType("System.Decimal"));
            dtDuplicatas.Columns.Add("valorcomissaosup", Type.GetType("System.Decimal"));
            dtDuplicatas.Columns.Add("idtipopaga", Type.GetType("System.Int32"));
            dtDuplicatas.Columns.Add("tipopaga", Type.GetType("System.String"));
            dtDuplicatas.Columns.Add("boletonro", Type.GetType("System.Decimal"));
            dtDuplicatas.Columns.Add("dv", Type.GetType("System.String"));
            dtDuplicatas.Columns.Add("caixa", Type.GetType("System.String"));


            sda = new SqlDataAdapter("select * from condpagto where id=@id", clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = idcondpagto;
            sda.Fill(dtCondPagto);
            if (dtCondPagto.Rows.Count == 0)
            {
                throw new Exception("Condição de Pagamento escolhida não existe  !!! ");
            }

            rowCondpagto = dtCondPagto.Rows[0];  // transferir para variaveis

            sda = new SqlDataAdapter("select * from feriados where data>=@data order by data", clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@data", SqlDbType.DateTime).Value = data;
            sda.Fill(dtFeriados);

            try
            {
                juros = clsParser.DecimalParse(rowCondpagto["juros"].ToString());
                if (calculajuros == "N")
                {
                    juros = 0;
                }
            }
            catch
            {
                juros = 0;
            }


            parcelas = clsParser.Int32Parse(rowCondpagto["parcelas"].ToString());
            if (parcelas <= 0)
            {
                parcelas = 1;
            }

            //if (pispasep == false)
            //{
            //    total -= totalpis;
            //}

            //if (cofins == false)
            //{
            //    total -= totalcofins;
            //}

            if (juros > 0)
            {
                total = clsVisual.RoundDown(((juros / 100) * total) + total, 2);
            }

            // Calculando Valor das parcelas   ///
            for (x = 0; x < parcelas; x++)
            {
                if (x == 0 && rowCondpagto["ipi"].ToString() == "S" && rowCondpagto["st"].ToString() == "S" && parcelas > 1)
                {
                    valorx = total - (totalipi + totalst);
                    if (clsParser.DecimalParse(rowCondpagto["por1"].ToString()) > 0)
                    {
                        valorx = (valorx * (clsParser.DecimalParse(rowCondpagto["por1"].ToString()) / 100));
                        valorxcomi = (comissao * (clsParser.DecimalParse(rowCondpagto["por1"].ToString()) / 100));
                    }
                    else
                    {
                        valorx = (valorx / parcelas);
                        valorxcomi = (comissao / parcelas);
                    }
                    valorx = Math.Round((valorx + totalipi + totalst), 2);
                    valorxcomi = Math.Round(valorxcomi, 2);
                    parcelasvalor.Add(valorx);
                    parcelasvalorcomissao.Add(valorxcomi);
                }
                else if (x == 0 && rowCondpagto["ipi"].ToString() == "S" && parcelas > 1)
                {
                    valorx = total - (totalipi);
                    if (clsParser.DecimalParse(rowCondpagto["por1"].ToString()) > 0)
                    {
                        valorx = (valorx * (clsParser.DecimalParse(rowCondpagto["por1"].ToString()) / 100));
                        valorxcomi = (comissao * (clsParser.DecimalParse(rowCondpagto["por1"].ToString()) / 100));
                    }
                    else
                    {
                        valorx = (valorx / parcelas);
                        valorxcomi = (comissao / parcelas);
                    }
                    valorx = Math.Round((valorx + totalipi), 2);
                    valorxcomi = Math.Round(valorxcomi, 2);
                    parcelasvalor.Add(valorx);
                    parcelasvalorcomissao.Add(valorxcomi);
                }
                else if (x == 0 && rowCondpagto["st"].ToString() == "S" && parcelas > 1)
                {
                    valorx = total - (totalst);
                    if (clsParser.DecimalParse(rowCondpagto["por1"].ToString()) > 0)
                    {
                        valorx = (valorx * (clsParser.DecimalParse(rowCondpagto["por1"].ToString()) / 100));
                        valorxcomi = (comissao * (clsParser.DecimalParse(rowCondpagto["por1"].ToString()) / 100));
                    }
                    else
                    {
                        valorx = (valorx / parcelas);
                        valorxcomi = (comissao / parcelas);
                    }
                    valorx = Math.Round((valorx + totalst), 2);
                    valorxcomi = Math.Round(valorxcomi, 2);
                    parcelasvalor.Add(valorx);
                    parcelasvalorcomissao.Add(valorxcomi);
                }
                else
                {
                    porx = 0;
                    if (x == 1)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por2"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por2"].ToString());
                        }
                    }
                    else if (x == 2)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por3"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por3"].ToString());
                        }
                    }
                    else if (x == 3)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por4"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por4"].ToString());
                        }
                    }
                    else if (x == 4)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por5"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por5"].ToString());
                        }
                    }
                    else if (x == 4)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por5"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por5"].ToString());
                        }
                    }
                    else if (x == 5)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por6"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por6"].ToString());
                        }
                    }
                    else if (x == 6)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por7"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por7"].ToString());
                        }
                    }
                    else if (x == 7)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por8"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por8"].ToString());
                        }
                    }
                    else if (x == 8)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por9"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por9"].ToString());
                        }
                    }
                    else if (x == 9)
                    {
                        if (clsParser.DecimalParse(rowCondpagto["por10"].ToString()) > 0)
                        {
                            porx = clsParser.DecimalParse(rowCondpagto["por10"].ToString());
                        }
                    }

                    if (porx > 0)
                    {
                        valorx = Math.Round((total * (porx / 100)), 2);
                        valorxcomi = Math.Round((comissao * (porx / 100)), 2);

                    }
                    else
                    {
                        valorx = Math.Round((total / parcelas), 2);
                        valorxcomi = Math.Round((comissao / parcelas), 2);

                        //parcelasvalor.Add(clsParser.DecimalParse((total / parcelas).ToString("n2")));
                        //parcelasvalorcomissao.Add(clsParser.DecimalParse((comissao / parcelas).ToString("n2")));
                    }
                    parcelasvalor.Add(valorx);
                    parcelasvalorcomissao.Add(valorxcomi);
                }
                parcelastotal += parcelasvalor[x];
                comissaototal += parcelasvalorcomissao[x];
            }
            // Fazer o fechamento das Parcelas e Verificar se bateu com o total
            if (total != parcelastotal)
            {
                // Ajustando valores se as parcelas for maior ou menor que o total da cobranca
                // Sempre ajusta na primeira parcela
                parcelasvalor[0] += total - parcelastotal;
                parcelastotal += total - parcelastotal;
            }

            // Dia que deve cair 
            if (fixardiareferencia == "S")
            {
                diacai1 = data.Day;
                diacai2 = 0;
                diacai3 = 0;
                diacai4 = 0;

            }
            else
            {
                diacai1 = clsParser.Int32Parse(rowCondpagto["cai1"].ToString());
                diacai2 = clsParser.Int32Parse(rowCondpagto["cai2"].ToString());
                diacai3 = clsParser.Int32Parse(rowCondpagto["cai4"].ToString());
                diacai4 = clsParser.Int32Parse(rowCondpagto["cai4"].ToString());
            }
            //Como será o ponto de partida da data : da emissão, fora semana, dezena, quinzena e mês etc...
            switch (rowCondpagto["dadata"].ToString())
            {
                case "A":  //a vista - o ponto de partida é ele mesmo
                    //data = data;
                    break;
                case "D":  //fora dezena
                    if (clsParser.Int32Parse(data.Day.ToString()) <= 10)
                    {
                        data = DateTime.Parse("11/" + data.Month + "/" + data.Year);
                    }
                    else if (clsParser.Int32Parse(data.Day.ToString()) <= 20)
                    {
                        data = DateTime.Parse("21/" + data.Month + "/" + data.Year);
                    }
                    else
                    {
                        data = data.AddMonths(1);
                        data = DateTime.Parse("01/" + data.Month + "/" + data.Year);
                    }
                    break;
                case "E": //emissão o ponto de partida é ele mesmo
                    //ok
                    break;
                case "L": //C/apresentação emissão o ponto de partida é ele mesmo
                    //ok
                    break;
                case "M": //fora mês
                    data = data.AddMonths(1);
                    data = DateTime.Parse("01/" + data.Month + "/" + data.Year);
                    break;
                case "Q": //fora quinzena                            
                    if (clsParser.Int32Parse(data.Day.ToString()) <= 15)
                    {
                        data = DateTime.Parse("16/" + data.Month + "/" + data.Year);
                    }
                    else
                    {
                        data = data.AddMonths(1);
                        data = DateTime.Parse("01/" + data.AddMonths(1).Month + "/" + data.AddMonths(1).Year);
                    }
                    break;
                case "S": //fora semana
                    switch (data.DayOfWeek)
                    {
                        case DayOfWeek.Sunday://domingo
                            data = data.AddDays(7);
                            break;
                        case DayOfWeek.Monday://segunda
                            data = data.AddDays(6);
                            break;
                        case DayOfWeek.Tuesday://terça
                            data = data.AddDays(5);
                            break;
                        case DayOfWeek.Wednesday://quarta
                            data = data.AddDays(4);
                            break;
                        case DayOfWeek.Thursday://quinta
                            data = data.AddDays(3);
                            break;
                        case DayOfWeek.Friday://sexta
                            data = data.AddDays(2);
                            break;
                        case DayOfWeek.Saturday://sabado
                            data = data.AddDays(1);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            // Calcular as Datas de Vencimento
            for (x = 1; x <= parcelas; x++)
            {

                if (parcelas <= 10)
                {
                    if (x == 1)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia1"].ToString()));
                    }
                    if (x == 2)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia2"].ToString()));
                        if (tmp_data.Month == tmp_data_ant.Month)
                        {
                           // tmp_data = tmp_data.AddMonths(1);
                        }
                    }
                    if (x == 3)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia3"].ToString()));
                        if (tmp_data.Month == tmp_data_ant.Month)
                        {
                            //tmp_data = tmp_data.AddMonths(1);
                        }
                    }
                    if (x == 4)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia4"].ToString()));
                        if (tmp_data.Month == tmp_data_ant.Month)
                        {
                            //tmp_data = tmp_data.AddMonths(1);
                        }
                    }
                    if (x == 5)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia5"].ToString()));
                        if (tmp_data.Month == tmp_data_ant.Month)
                        {
                            //tmp_data = tmp_data.AddMonths(1);
                        }
                    }
                    if (x == 6)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia6"].ToString()));
                        if (tmp_data.Month == tmp_data_ant.Month)
                        {
                            //tmp_data = tmp_data.AddMonths(1);
                        }
                    }
                    if (x == 7)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia7"].ToString()));
                        if (tmp_data.Month == tmp_data_ant.Month)
                        {
                            //tmp_data = tmp_data.AddMonths(1);
                        }
                    }
                    if (x == 8)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia8"].ToString()));
                        if (tmp_data.Month == tmp_data_ant.Month)
                        {
                            //tmp_data = tmp_data.AddMonths(1);
                        }
                    }
                    if (x == 9)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia9"].ToString()));
                        if (tmp_data.Month == tmp_data_ant.Month)
                        {
                            //tmp_data = tmp_data.AddMonths(1);
                        }
                    }
                    if (x == 10)
                    {
                        tmp_data = data.AddDays(clsParser.Int32Parse(rowCondpagto["dia10"].ToString()));
                        if (tmp_data.Month == tmp_data_ant.Month)
                        {
                            //tmp_data = tmp_data.AddMonths(1);
                        }
                    }
                }
                else
                {
                    tmp_data = data.AddDays(x * clsParser.Int32Parse(rowCondpagto["qtdedias"].ToString()));
                }
                // Verificar se existe um dia Pre Determinado para Cair
                if (diacai1 > 0)
                {
                    if (tmp_data.Day != diacai1)
                    {
                        String Data_Prev = "";
                        //Numero para calcular a diferença de dias entre o dia que o pagamento deveria cair e o dia final do mesmo
                        int diferencaDias = 0;
                        if (diacai1 > DateTime.DaysInMonth(tmp_data.Year, tmp_data.Month)) //Ver se o dia do pagamento é maior que o numero de dias do mes atual
                        {
                            diferencaDias = diacai1 - DateTime.DaysInMonth(tmp_data.Year, tmp_data.Month); //Pegar a diferença e subtrair para conseguir o dia final do mês para estes casos
                            Data_Prev = (diacai1 - diferencaDias).ToString() + "/" + tmp_data.Month.ToString() + "/" + tmp_data.Year;
                        }
                        else
                        {
                            diferencaDias = DateTime.DaysInMonth(tmp_data.Year, tmp_data.Month) - diacai1; //Pegar a diferença e subtrair para conseguir o dia final do mês para estes casos
                            Data_Prev = (diacai1).ToString() + "/" + tmp_data.Month.ToString() + "/" + tmp_data.Year;
                        }

                        // Procurar pela parcela anterior para saber se não pulo mais de 2 meses - caso fevereiro
                        Int32 xmesesdif = 0;
                        if (x > 1)
                        {
                            DataRow rowsAnterior = dtDuplicatas.Select("posicao=" + (x - 1))[0];
                            xmesesdif = DateTime.Parse(Data_Prev).Month - DateTime.Parse(rowsAnterior["data"].ToString()).Month;
                        }
                        if (xmesesdif > 1)
                        { // ele aumentou em dois mes por causa de fevereiro
                            /// Colocar dia 1 na data e retroceder 1 dia
                            Data_Prev = "01/" + tmp_data.Month.ToString() + "/" + tmp_data.Year;
                            Data_Prev = DateTime.Parse(Data_Prev).AddDays(-1).ToString("dd/MM/yyyy");
                            tmp_data = DateTime.Parse(Data_Prev);
                        }
                        else
                        {
                            tmp_data = DateTime.Parse(Data_Prev);
                            //tmp_data = DateTime.Parse((diacai1 - diferencaDias).ToString() + "/" + tmp_data.Month.ToString() + "/" + tmp_data.Year);
                        }
                    }
                    else if (tmp_data.Day < diacai2 && diacai2 > 0)
                    {
                        tmp_data = DateTime.Parse(diacai2.ToString() + "/" + tmp_data.Month.ToString() + "/" + tmp_data.Year);
                    }
                    else if (tmp_data.Day < diacai3 && diacai3 > 0)
                    {
                        tmp_data = DateTime.Parse(diacai3.ToString() + "/" + tmp_data.Month.ToString() + "/" + tmp_data.Year);
                    }
                    else if (tmp_data.Day < diacai4 && diacai4 > 0)
                    {
                        tmp_data = DateTime.Parse(diacai4.ToString() + "/" + tmp_data.Month.ToString() + "/" + tmp_data.Year);
                    }
                    else if (tmp_data.Day == diacai1) // inclui 03/05/2017 por causa do 10x
                    {
                       ////// tmp_data = DateTime.Parse(diacai4.ToString() + "/" + tmp_data.Month.ToString() + "/" + tmp_data.Year);
                    }
                    else
                    {
                        tmp_data = tmp_data.AddMonths(1);
                        tmp_data = DateTime.Parse(diacai1.ToString() + "/" + tmp_data.Month.ToString() + "/" + tmp_data.Year);
                    }
                }
                else
                {  // se não tem dia certo para cair verifica o dia da semana que deve cair
                    if (rowCondpagto["semana"].ToString() == "SEG")
                    {
                        tmp_data = ProximoDiaSemana(tmp_data, DayOfWeek.Monday);
                    }
                    else if (rowCondpagto["semana"].ToString() == "TER")
                    {
                        tmp_data = ProximoDiaSemana(tmp_data, DayOfWeek.Tuesday);
                    }
                    else if (rowCondpagto["semana"].ToString() == "QUA")
                    {
                        tmp_data = ProximoDiaSemana(tmp_data, DayOfWeek.Wednesday);
                    }
                    else if (rowCondpagto["semana"].ToString() == "QUI")
                    {
                        tmp_data = ProximoDiaSemana(tmp_data, DayOfWeek.Thursday);
                    }
                    else if (rowCondpagto["semana"].ToString() == "SEX")
                    {
                        tmp_data = ProximoDiaSemana(tmp_data, DayOfWeek.Friday);
                    }
                    else
                    { // Qualquer dia da semana sem ser sabado e domingo

                    }
                    // Verifica se o Vencimento não esta caindo em nenhum feriado
                    // Agora verifica se está em algum feriado, se tiver caído em um feriado, procura
                    DateTime dataFeriado;
                    foreach (DataRow row in dtFeriados.Rows)
                    {
                        dataFeriado = DateTime.Parse(row["data"].ToString()); // data do feriado
                        tmp_data = ProximoDiaUtil(tmp_data, dataFeriado);
                    }

                }
                rowDuplicata = dtDuplicatas.NewRow();
                rowDuplicata["posicao"] = x;
                rowDuplicata["posicaofim"] = parcelas;
                rowDuplicata["data"] = tmp_data;
                rowDuplicata["pagou"] = "N";
                rowDuplicata["valor"] = parcelasvalor[x - 1];
                rowDuplicata["valorcomissao"] = parcelasvalorcomissao[x - 1];
                rowDuplicata["valorcomissaoger"] = 0; // parcelasvalorcomissaoger[x - 1];
                rowDuplicata["valorcomissaosup"] = 0; // parcelasvalorcomissaosup[x - 1];
                rowDuplicata["idtipopaga"] = idformapagto;
                //rowDuplicata["tipopaga"] = Procedures.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "CODIGO", "ID", idformapagto);
                rowDuplicata["tipopaga"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM SITUACAOTIPOTITULO WHERE ID = " + idformapagto);
                rowDuplicata["boletonro"] = 0;
                rowDuplicata["dv"] = "";
                rowDuplicata["tipopaga"] = "";
                rowDuplicata["caixa"] = "";

                dtDuplicatas.Rows.Add(rowDuplicata);
                tmp_data_ant = tmp_data;
            }
            return dtDuplicatas;
        }

        public static DateTime ProximoDiaSemana(DateTime data, DayOfWeek diasemana)
        {
            if (data.DayOfWeek != diasemana)
            {
                for (int x = 1; x <= 7; x++)
                {
                    data = data.AddDays(1);

                    if (data.DayOfWeek == diasemana)
                    {
                        break;
                    }
                }
            }

            return data;
        }
        public static DateTime ProximoDiaUtil(DateTime data, DateTime feriado)
        {
            if (data.Day == feriado.Day && data.Month == feriado.Month)
            {
                data = data.AddDays(1);

                if (data.DayOfWeek == DayOfWeek.Saturday ||
                    data.DayOfWeek == DayOfWeek.Sunday)
                {
                    data = ProximoDiaSemana(data, DayOfWeek.Monday);
                }
            }

            return data;
        }

        public static String DiaSemana(DateTime data, String diasemana)
        {

            if (data.DayOfWeek == DayOfWeek.Monday)
            {
                    diasemana = "SEG"; 
            }
            if (data.DayOfWeek == DayOfWeek.Tuesday)
            {
                diasemana = "TER";
            }
            if (data.DayOfWeek == DayOfWeek.Wednesday)
            {
                diasemana = "QUA";
            }
            if (data.DayOfWeek == DayOfWeek.Thursday)
            {
                diasemana = "QUI";
            }
            if (data.DayOfWeek == DayOfWeek.Friday)
            {
                diasemana = "SEX";
            }
            if (data.DayOfWeek == DayOfWeek.Saturday)
            {
                diasemana = "SAB";
            }
            if (data.DayOfWeek == DayOfWeek.Sunday)
            {
                diasemana = "DOM";
            }
            return diasemana;
        }

        public static DataTable FluxoFinanceiro(Int32 Filial, Int32 idCliente, String Cliente, String TipoData, 
                                                String DataDe, String DataAte, 
                                                Int32 ContaDe, Int32 ContaAte,
                                                Int32 CtaNao01, Int32 CtaNao02, Int32 CtaNao03, Int32 CtaNao04, 
                                                Int32 CtaNao05, Int32 CtaNao06, Int32 CtaNao07, Int32 CtaNao08, 
                                                Int32 CtaNao09, Int32 CtaNao10, Int32 CtaNao11, Int32 CtaNao12, 
                                                String Inclui_Banco, String Inclui_AtrasoPagar, String Inclui_AtrasoReceber,
                                                Int32 Dias_ClienteBom, Int32 Dias_ClienteMedio, Int32 Dias_ClienteRuim)
        {
            String sql;
            String query;
            String sql1;
            Int32 dias = 0;
            Int32 idBanco = 0;
            Int32 idFeriado = 0;
            Decimal saldo = 0;
            Decimal credito = 0;
            Decimal debito = 0;
            Boolean bok;
            DataTable dtFluxoFinanceiro;
            DateTime daDataDe = DateTime.MinValue;
            DateTime daDataAte = DateTime.MaxValue;

            if (DataDe.Length > 0)
            {
                daDataDe = DateTime.Parse(DataDe);
            }
            if (DataAte.Length > 0)
            {
                daDataAte = DateTime.Parse(DataAte);
            }
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
            "RECEBER.VALORMULTA, RECEBER.VALORLIQUIDO - (RECEBER.VALORPAGO) AS VALORARECEBER, RECEBER.IDSITBANCO,  " +
            "SITUACAOTITULO.CODIGO AS SITBANCOCOD, SITUACAOTITULO.NOME AS SITUACAONOME, RECEBER.IMPRIMIR, RECEBER.VALORPAGO,  " +
            "RECEBER.VALORBAIXANDO, RECEBER.IDVENDEDOR, CLIENTEVEND.COGNOME AS VENDEDOR, RECEBER.VALORCOMISSAO,  " +
            "RECEBER.IDSUPERVISOR, CLIENTESUP.COGNOME AS SUPERVISOR, RECEBER.VALORCOMISSAOSUP, RECEBER.IDCOORDENADOR,  " +
            "CLIENTECOO.COGNOME AS COORDENADOR, RECEBER.VALORCOMISSAOGER, CLIENTE.CREDITO " +
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
            query = "";
            if (Filial > 0)
            {
                query = " RECEBER.FILIAL=" + Filial;
            }
            if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
            {
                if (query.Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " RECEBER.IDCLIENTE = " + idCliente + " ";
            }
            if (DataDe.ToString() != "")
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                if (TipoData == "P")  // PREV
                {
                    query = query + " RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                    query = query + " and ";
                    query = query + " RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
                }
                else
                {
                    query = query + " RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                    query = query + " and ";
                    query = query + " RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
                }
            }
            if (query.ToString().Length > 0)
            {
                sql = sql + " where " + query + " ";
            }
            sql = sql + " union " +
            "SELECT  'BPAG' AS [tabela], PAGAR.VALORDESCONTO AS CREDITO, PAGAR.VALORDESCONTO AS DEBITO, PAGAR.VALORDESCONTO AS SALDO, " +
            "PAGAR.ID, PAGAR.FILIAL, PAGAR.DUPLICATA, PAGAR.POSICAO, PAGAR.POSICAOFIM, PAGAR.EMISSAO, PAGAR.IDDOCUMENTO,  " +
            "DOCFISCAL.COGNOME AS DOCUMENTO, PAGAR.SETOR, PAGAR.IDFORNECEDOR, CLIENTE.COGNOME AS CLIENTE, PAGAR.DATALANCA, PAGAR.EMITENTE,  " +
            "PAGAR.IDHISTORICO, HISTORICOS.CODIGO AS HISTORICO, HISTORICOS.NOME AS HISTORICONOM, PAGAR.IDCENTROCUSTO,  " +
            "CENTROCUSTOS.CODIGO AS CENTROCUSTO, CENTROCUSTOS.NOME AS CENTROCUSTONOM, PAGAR.IDCODIGOCTABIL,  " +
            "CONTACONTABIL.CODIGO AS CTACONTABIL, CONTACONTABIL.NOME AS CTACONTABILNOM, PAGAR.IDNOTAFISCAL, NFCOMPRA.NUMERO AS NOTAFISCAL, " +
            "PAGAR.IDPAGARNFE  AS IDNOTAPRINCIPAL, PAGAR.IDFORMAPAGTO, SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTO, SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOM,  " +
            "PAGAR.OBSERVA, PAGAR.IDBANCO, TAB_BANCOS.CODIGO AS BANCO, TAB_BANCOS.COGNOME AS BANCONOM, PAGAR.IDBANCOINT,  " +
            "BANCOS.CONTA, BANCOS.NOME, PAGAR.CHEGOU, PAGAR.DESPESAPUBLICA, PAGAR.BOLETO, PAGAR.BOLETONRO, PAGAR.DV,  " +
            "PAGAR.BAIXA, PAGAR.VENCIMENTO, PAGAR.VENCIMENTOPREV, PAGAR.VALOR, PAGAR.VALORDESCONTO, PAGAR.ATEVENCIMENTO, PAGAR.VALORLIQUIDO,  " +
            "PAGAR.VALORJUROS, PAGAR.VALORMULTA, PAGAR.VALORLIQUIDO - (PAGAR.VALORPAGO) AS VALORARECEBER, PAGAR.IDSITBANCO,  " +
            "SITUACAOTITULO.CODIGO AS SITUACAOCODIGO, SITUACAOTITULO.NOME AS SITUACAONOME, PAGAR.IMPRIMIR, PAGAR.VALORPAGO, PAGAR.VALORBAIXANDO,  " +
            "PAGAR.IDVENDEDOR, CLIENTEVEND.COGNOME AS VENDEDOR, PAGAR.VALORCOMISSAO, PAGAR.IDSUPERVISOR,  " +
            "CLIENTESUP.COGNOME AS SUPERVISOR, PAGAR.VALORCOMISSAOSUP, PAGAR.IDCOORDENADOR, CLIENTECOO.COGNOME AS COORDENADOR,  " +
            "PAGAR.VALORCOMISSAOGER, CLIENTE.CREDITO " +
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
            query = "";
            if (Filial > 0)
            {
                query = " PAGAR.FILIAL=" + Filial;
            }
            if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " PAGAR.IDFORNECEDOR = " + idCliente + " ";
            }
            if (DataDe.ToString() != "")
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                if (TipoData == "P")
                {
                    query = query + " PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                    query = query + " and ";
                    query = query + " PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
                }
                else
                {
                    query = query + " PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                    query = query + " and ";
                    query = query + " PAGAR.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
                }
            }
            if (query.ToString().Length > 0)
            {
                sql = sql + " where " + query + " ";
            }
            SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
            //scd.Parameters.Add("@id", SqlDbType.Int).Value = idPagar;
            dtFluxoFinanceiro = new DataTable();
            sda.Fill(dtFluxoFinanceiro);
            //////////////////// Incluir o Pedido de Compras no Fluxo
            sql1 = "";
            sql1 = "SELECT COMPRAS.ID,COMPRAS.FILIAL, COMPRAS.NUMERO,COMPRAS.ANO,COMPRAS.DATA,COMPRAS.IDFORNECEDOR, CLIENTE.COGNOME, " +
                          "COMPRAS.IDEMITENTE, USUARIO.USUARIO, COMPRAS.IDAUTORIZANTE,COMPRAS.IDCOMPRADOR,COMPRAS.IDCONTATO,COMPRAS.SITUACAO,COMPRAS.SETOR,COMPRAS.SETORFATOR,COMPRAS.FRETE,COMPRAS.FRETEPAGA,COMPRAS.TRANSPORTE,COMPRAS.TIPOFRETE,COMPRAS.FRETETIPO,COMPRAS.IDTRANSPORTADORA,COMPRAS.FRETEQTDE,COMPRAS.FRETEUNID,COMPRAS.FRETEPRECOUNI,COMPRAS.FRETETOTAL,COMPRAS.FRETEBASEICMS,COMPRAS.FRETEICMALIQ,COMPRAS.FRETEVALORICMS, " +
                          "COMPRAS.IDFORMAPAGTO,COMPRAS.IDCONDPAGTO,COMPRAS.TOTALBRUTO,COMPRAS.TOTALDESCONTO,COMPRAS.DESCONTO,COMPRAS.TOTALMERCADORIA,COMPRAS.TOTALIPI,COMPRAS.TOTALFRETE,COMPRAS.TOTALFRETEICMS,COMPRAS.TOTALSEGURO,COMPRAS.TOTALSEGUROICMS,COMPRAS.TOTALOUTRAS,COMPRAS.TOTALOUTRASICMS,COMPRAS.TOTALPEDIDO,COMPRAS.TOTALBASEICM,COMPRAS.TOTALICM,COMPRAS.TOTALBASEICMSUBST,COMPRAS.TOTALICMSUBST,COMPRAS.IRRFPORC,COMPRAS.IRRF,COMPRAS.INSSPORC,COMPRAS.INSS,COMPRAS.PISCOFINSCSLLPORC, " +
                          "COMPRAS.PISCOFINSCSLL,COMPRAS.PISPORC,COMPRAS.PIS,COMPRAS.COFINSPORC,COMPRAS.COFINS,COMPRAS.CSLLPORC,COMPRAS.CSLL,COMPRAS.ISSPORC,COMPRAS.ISS,COMPRAS.TOTALPESO,COMPRAS.TOTALPECA,COMPRAS.QTDEENTREGUE,COMPRAS.QTDEDEFEITO,COMPRAS.QTDESUCATA,COMPRAS.QTDEBAIXADA,COMPRAS.QTDEOSAUX,COMPRAS.QTDESALDO,COMPRAS.TOTALPECAENTRA,COMPRAS.TOTALPECATRANSFE,COMPRAS.TERMINO,COMPRAS.FECHADO,COMPRAS.TOTALPREVISTO, " +
                          "COMPRASPAGAR.IDCOMPRA, COMPRASPAGAR.POSICAO, COMPRASPAGAR.POSICAOFIM, COMPRASPAGAR.DATA AS [DATAVENC], COMPRASPAGAR.PAGOU, " +
                          "COMPRASPAGAR.VALOR, COMPRASPAGAR.IDTIPOPAGA, COMPRASPAGAR.BOLETONRO, COMPRASPAGAR.DV " +
                          "FROM COMPRAS " +
                          "INNER JOIN COMPRASPAGAR ON COMPRASPAGAR.IDCOMPRA = COMPRAS.ID " +
                          "LEFT JOIN CLIENTE ON CLIENTE.ID=COMPRAS.IDFORNECEDOR " +
                          "LEFT JOIN USUARIO ON USUARIO.ID=COMPRAS.IDEMITENTE ";
            // filtrar pelo cognome
            query = "";
            if (Filial > 0)
            {
                query = " COMPRAS.FILIAL=" + Filial;
            }
            if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " COMPRAS.IDFORNECEDOR = " + idCliente + " ";
            }
            if (DataDe.ToString() != "")
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " COMPRASPAGAR.DATA >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                query = query + " and ";
                query = query + " COMPRASPAGAR.DATA <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
            }
            if (query.ToString().Length > 0)
            {
                sql1 = sql1 + " where " + query + " ";
            }

            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
            scn.Open();
            SqlCommand scd = new SqlCommand(sql1, scn);
            SqlDataReader sdr = scd.ExecuteReader();
            while (sdr.Read() == true)
            {
                DataRow rowIncluir = dtFluxoFinanceiro.NewRow();
                //            posicaoPagarObserva = dtPagarObserva.Rows.Count + 1;
                rowIncluir["Tabela"] = "PCO";
                rowIncluir["CREDITO"] = 0;
                rowIncluir["DEBITO"] = 0;
                rowIncluir["SALDO"] = 0;
                rowIncluir["ID"] = clsParser.Int32Parse(sdr["ID"].ToString());
                rowIncluir["FILIAL"] = clsParser.Int32Parse(sdr["FILIAL"].ToString());
                rowIncluir["DUPLICATA"] = clsParser.Int32Parse(sdr["NUMERO"].ToString());
                rowIncluir["POSICAO"] = clsParser.Int32Parse(sdr["POSICAO"].ToString());
                rowIncluir["POSICAOFIM"] = clsParser.Int32Parse(sdr["POSICAOFIM"].ToString());
                rowIncluir["EMISSAO"] = DateTime.Parse(sdr["DATA"].ToString()).ToString("dd/MM/yyyy"); // clsParser.SqlDateTimeParse(sdr["DATA"].ToString());
                //rowIncluir["DOCUMENTO"] = sdr["PCO"].ToString();
                rowIncluir["DOCUMENTO"] = "PCO";
                rowIncluir["SETOR"] = sdr["SETOR"].ToString();
                rowIncluir["IDCLIENTE"] = clsParser.Int32Parse(sdr["IDFORNECEDOR"].ToString());
                rowIncluir["CLIENTE"] = sdr["COGNOME"].ToString();
                rowIncluir["DATALANCA"] = clsParser.SqlDateTimeParse(sdr["DATA"].ToString()).ToString();
                rowIncluir["EMITENTE"] = sdr["USUARIO"].ToString();
                rowIncluir["IDHISTORICO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idhistorico from compras1 where idcompras= " + clsParser.Int32Parse(sdr["ID"].ToString()) + " ").ToString());
                rowIncluir["HISTORICO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where ID = " + clsParser.Int32Parse(rowIncluir["IDHISTORICO"].ToString()), "").Trim();
                //rowIncluir["HISTORICO"] = clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", clsParser.Int32Parse(rowIncluir["IDHISTORICO"].ToString())).Trim();
                rowIncluir["HISTORICONOM"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from HISTORICOS where ID = " + clsParser.Int32Parse(rowIncluir["IDHISTORICO"].ToString()), "").Trim();
                //rowIncluir["HISTORICONOM"] = clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "NOME", "ID", clsParser.Int32Parse(rowIncluir["IDHISTORICO"].ToString())).Trim();
                rowIncluir["IDCENTROCUSTO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcentrocusto from compras1 where idcompras= " + clsParser.Int32Parse(sdr["ID"].ToString()) + " "));
                rowIncluir["CENTROCUSTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowIncluir["IDCENTROCUSTO"].ToString()), "").Trim();
                //rowIncluir["CENTROCUSTO"] = clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", clsParser.Int32Parse(rowIncluir["IDCENTROCUSTO"].ToString())).Trim();
                rowIncluir["CENTROCUSTONOM"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from CENTROCUSTOS where ID = " + clsParser.Int32Parse(rowIncluir["IDCENTROCUSTO"].ToString()), "").Trim();
                //rowIncluir["CENTROCUSTONOM"] = clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "NOME", "ID", clsParser.Int32Parse(rowIncluir["IDCENTROCUSTO"].ToString())).Trim();
                rowIncluir["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                rowIncluir["CTACONTABIL"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from CONTACONTABIL where ID = " + clsParser.Int32Parse(rowIncluir["IDCODIGOCTABIL"].ToString()), "").Trim();
                //rowIncluir["CTACONTABIL"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "CODIGO", "ID", clsParser.Int32Parse(rowIncluir["IDCODIGOCTABIL"].ToString())).Trim();
                rowIncluir["CTACONTABILNOM"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from CONTACONTABIL where ID = " + clsParser.Int32Parse(rowIncluir["IDCODIGOCTABIL"].ToString()), "").Trim();
                //rowIncluir["CTACONTABILNOM"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "CONTACONTABIL", "NOME", "ID", clsParser.Int32Parse(rowIncluir["IDCODIGOCTABIL"].ToString())).Trim();
                rowIncluir["IDNOTAFISCAL"] = clsInfo.znfcompra;
                rowIncluir["NOTAFISCAL"] = 0;
                rowIncluir["IDNOTAPRINCIPAL"] = clsInfo.znfcompra;
                rowIncluir["IDFORMAPAGTO"] = clsParser.Int32Parse(sdr["IDFORMAPAGTO"].ToString());
                rowIncluir["FORMAPAGTO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTIPOTITULO where ID = " + clsParser.Int32Parse(rowIncluir["IDFORMAPAGTO"].ToString()), "").Trim();
                //rowIncluir["FORMAPAGTO"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "CODIGO", "ID", clsParser.Int32Parse(rowIncluir["IDFORMAPAGTO"].ToString())).Trim();
                rowIncluir["FORMAPAGTONOM"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTIPOTITULO where ID = " + clsParser.Int32Parse(rowIncluir["IDFORMAPAGTO"].ToString()), "").Trim();
                //rowIncluir["FORMAPAGTONOM"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTIPOTITULO", "NOME", "ID", clsParser.Int32Parse(rowIncluir["IDFORMAPAGTO"].ToString())).Trim();
                rowIncluir["OBSERVA"] = "";
                rowIncluir["IDBANCO"] = clsInfo.zbanco;
                rowIncluir["BANCO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_BANCOS where ID = " + clsParser.Int32Parse(rowIncluir["IDBANCO"].ToString()), "").Trim();
                //rowIncluir["BANCO"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "CODIGO", "ID", clsParser.Int32Parse(rowIncluir["IDBANCO"].ToString())).Trim();
                rowIncluir["BANCONOM"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from TAB_BANCOS where ID = " + clsParser.Int32Parse(rowIncluir["IDBANCO"].ToString()), "").Trim();
                //rowIncluir["BANCONOM"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "TAB_BANCOS", "NOME", "ID", clsParser.Int32Parse(rowIncluir["IDBANCO"].ToString())).Trim();
                rowIncluir["IDBANCOINT"] = clsInfo.zbancoint;
                rowIncluir["CONTA"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select conta from BANCOS where ID = " + clsParser.Int32Parse(rowIncluir["IDBANCOINT"].ToString()), "").Trim();
                //rowIncluir["CONTA"] = clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "CONTA", "ID", clsParser.Int32Parse(rowIncluir["IDBANCOINT"].ToString())).Trim();
                rowIncluir["NOME"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select nome from BANCOS where ID = " + clsParser.Int32Parse(rowIncluir["IDBANCOINT"].ToString()), "").Trim();
                //rowIncluir["NOME"] = clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "NOME", "ID", clsParser.Int32Parse(rowIncluir["IDBANCOINT"].ToString())).Trim();
                rowIncluir["CHEGOU"] = "N";
                rowIncluir["DESPESAPUBLICA"] = "N";
                rowIncluir["BOLETO"] = "";
                rowIncluir["BOLETONRO"] = clsParser.Int32Parse(sdr["BOLETONRO"].ToString()); ;
                rowIncluir["DV"] = sdr["DV"].ToString(); ;
                rowIncluir["BAIXA"] = "N";
                rowIncluir["VENCIMENTO"] = DateTime.Parse(sdr["DATAVENC"].ToString());
                rowIncluir["VENCIMENTOPREV"] = DateTime.Parse(sdr["DATAVENC"].ToString()).ToString("dd/MM/yyyy");
                rowIncluir["VALOR"] = clsParser.DecimalParse(sdr["VALOR"].ToString());
                rowIncluir["VALORDESCONTO"] = 0;
                rowIncluir["ATEVENCIMENTO"] = "N";
                rowIncluir["VALORLIQUIDO"] = clsParser.DecimalParse(sdr["VALOR"].ToString());
                rowIncluir["VALORJUROS"] = 0;
                rowIncluir["VALORMULTA"] = 0;
                rowIncluir["VALORARECEBER"] = clsParser.DecimalParse(sdr["VALOR"].ToString());
                rowIncluir["IDSITBANCO"] = clsInfo.zsituacaotitulo;
                rowIncluir["SITUACAOCODIGO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITUACAOTITULO where ID = " + clsParser.Int32Parse(rowIncluir["IDSITBANCO"].ToString()), "").Trim();
                //rowIncluir["SITUACAOCODIGO"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "CODIGO", "ID", clsParser.Int32Parse(rowIncluir["IDSITBANCO"].ToString())).Trim();
                rowIncluir["SITUACAONOME"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from SITUACAOTITULO where ID = " + clsParser.Int32Parse(rowIncluir["IDSITBANCO"].ToString()), "").Trim();
                //rowIncluir["SITUACAONOME"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOTITULO", "NOME", "ID", clsParser.Int32Parse(rowIncluir["IDSITBANCO"].ToString())).Trim();
                rowIncluir["IMPRIMIR"] = "N";
                rowIncluir["VALORPAGO"] = 0;
                rowIncluir["VALORBAIXANDO"] = 0;
                rowIncluir["IDVENDEDOR"] = clsInfo.zempresaclienteid;
                rowIncluir["VENDEDOR"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from CLIENTEPROSPECCAO where ID = " + clsParser.Int32Parse(rowIncluir["IDVENDEDOR"].ToString()), "").Trim();
                //rowIncluir["VENDEDOR"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "CLIENTEPROSPECCAO", "COGNOME", "ID", clsParser.Int32Parse(rowIncluir["IDVENDEDOR"].ToString())).Trim();
                rowIncluir["VALORCOMISSAO"] = 0;
                rowIncluir["IDSUPERVISOR"] = clsInfo.zempresaclienteid;
                rowIncluir["SUPERVISOR"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from CLIENTEPROSPECCAO where ID = " + clsParser.Int32Parse(rowIncluir["IDSUPERVISOR"].ToString()), "").Trim();
                //rowIncluir["SUPERVISOR"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "CLIENTEPROSPECCAO", "COGNOME", "ID", clsParser.Int32Parse(rowIncluir["IDSUPERVISOR"].ToString())).Trim();
                rowIncluir["VALORCOMISSAOSUP"] = 0;
                rowIncluir["IDCOORDENADOR"] = clsInfo.zempresaclienteid;
                rowIncluir["COORDENADOR"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from CLIENTEPROSPECCAO where ID = " + clsParser.Int32Parse(rowIncluir["IDCOORDENADOR"].ToString()), "").Trim();
                //rowIncluir["COORDENADOR"] = clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "CLIENTEPROSPECCAO", "COGNOME", "ID", clsParser.Int32Parse(rowIncluir["IDCOORDENADOR"].ToString())).Trim();
                rowIncluir["VALORCOMISSAOGER"] = 0;
                dtFluxoFinanceiro.Rows.Add(rowIncluir);

            }
            dtFluxoFinanceiro.AcceptChanges();
            /////////////////// Termino de Incluir o Pedido de Compras no Fluxo
            if (Inclui_Banco == "S")  // como vem de outros lugares pode ser que não tenha que calcular
            {
                /////////////////// Incluir os Saldos dos Bancos Apibank
                Int32 idFluxo = 0;
                sql1 = "";
                sql1 = "select (select top 1 fluxo.id from fluxo where fluxo.conta = bancos.id and fluxo.datadeposito <= getdate() " +
                        "order by  fluxo.datadeposito desc, fluxo.tipo desc, fluxo.numero desc ,fluxo.id desc) as idfluxo, BANCOS.CONTA, BANCOS.NOME " +
                       "from bancos ";
                // filtrar pelo cognome
                query = "";
                // APENAS AS CONTAS DETERMINADAS
                query = "WHERE BANCOS.CONTA >= " + ContaDe + " AND BANCOS.CONTA <=" + ContaAte + " and bancos.ativo='S' order by bancos.conta ";

                sql1 = sql1 + query;
                scn = new SqlConnection(clsInfo.conexaosqlbanco);
                scn.Open();
                scd = new SqlCommand(sql1, scn);
                sdr = scd.ExecuteReader();
                while (sdr.Read() == true)
                {
                    bok = true;
                    // Verificar se a Conta não deve aparecer
                    if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao01) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao02) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao03) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao04) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao05) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao06) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao07) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao08) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao09) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao10) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao11) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao12) { bok = false; };

                    if (bok == true)
                    {

                        DataRow rowIncluir = dtFluxoFinanceiro.NewRow();
                        //  puxar o id do fluxo que possui o ultimo saldo
                        idFluxo = clsParser.Int32Parse(sdr["IDFLUXO"].ToString());
                        if (idFluxo > 0)
                        {
                            rowIncluir["Tabela"] = "ABCO";
                            rowIncluir["CREDITO"] = 0;
                            rowIncluir["DEBITO"] = 0;
                            rowIncluir["SALDO"] = 0;
                            rowIncluir["ID"] = idFluxo;
                            rowIncluir["FILIAL"] = 0;
                            rowIncluir["DUPLICATA"] = 0;
                            rowIncluir["POSICAO"] = 0;
                            rowIncluir["POSICAOFIM"] = 0;
                            rowIncluir["EMISSAO"] = DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select DATADEPOSITO from fluxo where ID = " + idFluxo, "").ToString()).ToString("dd/MM/yyyy");
                            //rowIncluir["EMISSAO"] = DateTime.Parse(clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "FLUXO", "DATADEPOSITO", "ID", idFluxo).ToString()).ToString("dd/MM/yyyy");
                            //rowIncluir["DOCUMENTO"] = sdr["PCO"].ToString();
                            rowIncluir["DOCUMENTO"] = "BCO";
                            rowIncluir["SETOR"] = "";
                            rowIncluir["IDCLIENTE"] = 0;
                            rowIncluir["CLIENTE"] = "";
                            rowIncluir["DATALANCA"] = DateTime.Parse(rowIncluir["EMISSAO"].ToString()).ToString("dd/MM/yyyy"); 
                            rowIncluir["EMITENTE"] = "";
                            rowIncluir["IDHISTORICO"] = 0;
                            rowIncluir["HISTORICO"] = "";
                            rowIncluir["HISTORICONOM"] = "Saldo da Conta " + sdr["CONTA"].ToString() + " " + sdr["NOME"].ToString();
                            rowIncluir["IDCENTROCUSTO"] = 0;
                            rowIncluir["CENTROCUSTO"] = "";
                            rowIncluir["CENTROCUSTONOM"] = "";
                            rowIncluir["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
                            rowIncluir["CTACONTABIL"] = "";
                            rowIncluir["CTACONTABILNOM"] = "";
                            rowIncluir["IDNOTAFISCAL"] = 0;
                            rowIncluir["NOTAFISCAL"] = 0;
                            rowIncluir["IDNOTAPRINCIPAL"] = 0;
                            rowIncluir["IDFORMAPAGTO"] = clsInfo.zformapagto;
                            rowIncluir["FORMAPAGTO"] = "";
                            rowIncluir["FORMAPAGTONOM"] = "";
                            rowIncluir["OBSERVA"] = "";
                            rowIncluir["IDBANCO"] = 0;
                            rowIncluir["BANCO"] = 0;
                            rowIncluir["BANCONOM"] = "";
                            rowIncluir["IDBANCOINT"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select conta from fluxo where ID = " + idFluxo, "").ToString());
                            //rowIncluir["IDBANCOINT"] = clsParser.Int32Parse(clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "FLUXO", "CONTA", "ID", idFluxo).ToString());
                            rowIncluir["CONTA"] = clsParser.Int32Parse(sdr["CONTA"].ToString());
                            rowIncluir["NOME"] = sdr["NOME"].ToString();
                            rowIncluir["CHEGOU"] = "S";
                            rowIncluir["DESPESAPUBLICA"] = "N";
                            rowIncluir["BOLETO"] = "";
                            rowIncluir["BOLETONRO"] = 0;
                            rowIncluir["DV"] = "";
                            rowIncluir["BAIXA"] = "N";
                            rowIncluir["VENCIMENTO"] = DateTime.Now.ToString("dd/MM/yyyy");   //DateTime.Parse(clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "FLUXO", "DATADEPOSITO", "ID", idFluxo).ToString());
                            rowIncluir["VENCIMENTOPREV"] = DateTime.Parse(rowIncluir["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                            rowIncluir["VALOR"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select saldo from FLUXO where ID = " + idFluxo, "0");
                            //rowIncluir["VALOR"] = clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "FLUXO", "SALDO", "ID", idFluxo);
                            rowIncluir["VALORDESCONTO"] = 0;
                            rowIncluir["ATEVENCIMENTO"] = "N";
                            rowIncluir["VALORLIQUIDO"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select saldo from FLUXO where ID = " + idFluxo, "0");
                            //rowIncluir["VALORLIQUIDO"] = clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "FLUXO", "SALDO", "ID", idFluxo);
                            rowIncluir["VALORJUROS"] = 0;
                            rowIncluir["VALORMULTA"] = 0;
                            rowIncluir["VALORARECEBER"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select saldo from FLUXO where ID = " + idFluxo, "0");
                            //rowIncluir["VALORARECEBER"] = clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "FLUXO", "SALDO", "ID", idFluxo);
                            rowIncluir["IDSITBANCO"] = 0;
                            rowIncluir["SITBANCOCOD"] = 0;   //SITUACAOCODIGO
                            rowIncluir["SITUACAONOME"] = "";
                            rowIncluir["IMPRIMIR"] = "N";
                            rowIncluir["VALORPAGO"] = 0;
                            rowIncluir["VALORBAIXANDO"] = 0;
                            rowIncluir["IDVENDEDOR"] = 0;
                            rowIncluir["VENDEDOR"] = "";
                            rowIncluir["VALORCOMISSAO"] = 0;
                            rowIncluir["IDSUPERVISOR"] = 0;
                            rowIncluir["SUPERVISOR"] = "";
                            rowIncluir["VALORCOMISSAOSUP"] = 0;
                            rowIncluir["IDCOORDENADOR"] = 0;
                            rowIncluir["COORDENADOR"] = "";
                            rowIncluir["VALORCOMISSAOGER"] = 0;
                            dtFluxoFinanceiro.Rows.Add(rowIncluir);
                        }
                    }
                }
                dtFluxoFinanceiro.AcceptChanges();
                /////////////////// Termino de Incluir o Saldo Aplibank no Fluxo


                /////////////////// Incluir o Aplibank no Fluxo Financeiro Dia a Dia
                sql1 = "";
/*                sql1 = "select FLUXO.ID as [IDFLUXO], FLUXO.DATADEPOSITO, FLUXO.EMISSAO, FLUXO.NUMERO, RTRIM(HISTORICOS.NOME) + SPACE(2) + RTRIM(FLUXO.COMPLEMENTO) + SPACE(2) + RTRIM(FLUXO.DESTINATARIO) as [DESCRI], " +
                       "FLUXO.DESTINATARIO, HISTORICOS.CODIGO AS [HISTCOD], HISTORICOS.NOME AS [HISTNOM], FLUXO.COMPLEMENTO AS [COMPLE], " +
                       "FLUXO.CENTROCUSTO, " +
                       "FLUXO.VALOR, FLUXO.TIPO, FLUXO.SALDO, FLUXO.VALORBLOQUEADO, FLUXO.SALDOREAL, FLUXO.TRANSFERIDO, FLUXO.CONTA AS [IDCONTA], FLUXO.HISTORICO, " +
                       "FLUXO.DATAFINAL, FLUXO.CONFERIDO, BANCOS.CONTA, BANCOS.NOME, BANCOS.AGENCIA, BANCOS.CONTACOR, BANCOS.IDBANCO " +
                       "FROM FLUXO " +
                       "LEFT JOIN HISTORICOS ON HISTORICOS.ID = FLUXO.HISTORICO " +
                       "LEFT JOIN BANCOS ON BANCOS.ID = FLUXO.CONTA "; */
                sql1 = "select FLUXO.ID as [IDFLUXO], FLUXO.DATADEPOSITO, FLUXO.EMISSAO, FLUXO.NUMERO, RTRIM(FLUXO.COMPLEMENTO) + SPACE(2) + RTRIM(FLUXO.DESTINATARIO) as [DESCRI], " +
                       "FLUXO.DESTINATARIO, FLUXO.COMPLEMENTO AS [COMPLE], " +
                       "FLUXO.VALOR, FLUXO.TIPO, FLUXO.SALDO, FLUXO.VALORBLOQUEADO, FLUXO.SALDOREAL, FLUXO.TRANSFERIDO, FLUXO.CONTA AS [IDCONTA], " +
                       "FLUXO.DATAFINAL, FLUXO.CONFERIDO, BANCOS.CONTA, BANCOS.NOME, BANCOS.AGENCIA, BANCOS.CONTACOR, BANCOS.IDBANCO " +
                       "FROM FLUXO " +
                       "LEFT JOIN BANCOS ON BANCOS.ID = FLUXO.CONTA ";

                // filtrar pelo cognome
                query = "";
                // APENAS AS CONTAS DETERMINADAS
                query = "WHERE BANCOS.CONTA >= " + ContaDe + " AND BANCOS.CONTA <=" + ContaAte + " ";
                if (DataDe.ToString() != "")
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " AND ";
                    }
                    //query = query + " FLUXO.DATADEPOSITO >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);

                    query = query + " FLUXO.DATADEPOSITO > " + clsParser.SqlDateTimeFormat(daDataDe.AddDays(1).ToString("dd/MM/yyyy") + " 00:00", true);
                    query = query + " AND ";
                    query = query + " FLUXO.DATADEPOSITO <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
                }
                sql1 = sql1 + " " + query;
                sql1 = sql1 + " ORDER BY FLUXO.DATADEPOSITO, FLUXO.TIPO, FLUXO.NUMERO, FLUXO.ID ";

                scn = new SqlConnection(clsInfo.conexaosqlbanco);
                scn.Open();
                scd = new SqlCommand(sql1, scn);
                sdr = scd.ExecuteReader();
                while (sdr.Read() == true)
                {
                    bok = true;
                    // Verificar se a Conta não deve aparecer
                    if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao01) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao02) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao03) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao04) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao05) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao06) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao07) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao08) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao09) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao10) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao11) { bok = false; }
                    else if (clsParser.Int32Parse(sdr["CONTA"].ToString()) == CtaNao12) { bok = false; };

                    if (bok == true)
                    {
                        DataRow rowIncluir = dtFluxoFinanceiro.NewRow();
                        idFluxo = clsParser.Int32Parse(sdr["IDFLUXO"].ToString());
                        if (idFluxo > 0)
                        {
                            rowIncluir["Tabela"] = "ABCO";
                            rowIncluir["CREDITO"] = 0;
                            rowIncluir["DEBITO"] = 0;
                            rowIncluir["SALDO"] = 0;
                            rowIncluir["ID"] = idFluxo;
                            rowIncluir["FILIAL"] = 0;
                            rowIncluir["DUPLICATA"] = clsParser.Int32Parse(sdr["NUMERO"].ToString());
                            rowIncluir["POSICAO"] = 0;
                            rowIncluir["POSICAOFIM"] = 0;
                            rowIncluir["EMISSAO"] = DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select datadeposito from FLUXO where ID = " + idFluxo, "").ToString()).ToString("dd/MM/yyyy");
                            //rowIncluir["EMISSAO"] = DateTime.Parse(clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "FLUXO", "DATADEPOSITO", "ID", idFluxo).ToString()).ToString("dd/MM/yyyy"); 
                            rowIncluir["DOCUMENTO"] = "BCO";
                            rowIncluir["SETOR"] = "";
                            rowIncluir["IDCLIENTE"] = 0;
                            rowIncluir["CLIENTE"] = "";
                            rowIncluir["DATALANCA"] = DateTime.Parse(rowIncluir["EMISSAO"].ToString()).ToString("dd/MM/yyyy"); 
                            rowIncluir["EMITENTE"] = "";
                            rowIncluir["IDHISTORICO"] = 0;
                            rowIncluir["HISTORICO"] = "";
                            rowIncluir["HISTORICONOM"] = ("Cta :" + sdr["CONTA"].ToString() + "=" + sdr["NOME"].ToString() + "=" + sdr["DESTINATARIO"].ToString()).ToString().PadRight(40).Substring(0, 39);
                            rowIncluir["IDCENTROCUSTO"] = 0;
                            rowIncluir["CENTROCUSTO"] = "";
                            rowIncluir["CENTROCUSTONOM"] = "";
                            rowIncluir["IDCODIGOCTABIL"] = clsInfo.zcontacontabil;
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
                            rowIncluir["IDBANCOINT"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select conta from FLUXO where id = " + idFluxo, "").ToString());
                            //rowIncluir["IDBANCOINT"] = clsParser.Int32Parse(clsProcedures.PesquisaValor(clsInfo.conexaosqlbanco, "FLUXO", "CONTA", "ID", idFluxo).ToString());
                            rowIncluir["CONTA"] = clsParser.Int32Parse(sdr["CONTA"].ToString());
                            rowIncluir["NOME"] = sdr["NOME"].ToString();
                            rowIncluir["CHEGOU"] = "S";
                            rowIncluir["DESPESAPUBLICA"] = "N";
                            rowIncluir["BOLETO"] = "";
                            rowIncluir["BOLETONRO"] = 0;
                            rowIncluir["DV"] = "";
                            rowIncluir["BAIXA"] = "N";
                            rowIncluir["VENCIMENTO"] = DateTime.Parse(sdr["DATADEPOSITO"].ToString()).ToString("dd/MM/yyyy");
                            rowIncluir["VENCIMENTOPREV"] = DateTime.Parse(sdr["DATADEPOSITO"].ToString()).ToString("dd/MM/yyyy"); 
                            rowIncluir["VALOR"] = clsParser.DecimalParse(sdr["VALOR"].ToString());
                            rowIncluir["VALORDESCONTO"] = 0;
                            rowIncluir["ATEVENCIMENTO"] = "N";
                            rowIncluir["VALORLIQUIDO"] = clsParser.DecimalParse(sdr["VALOR"].ToString());
                            rowIncluir["VALORJUROS"] = 0;
                            rowIncluir["VALORMULTA"] = 0;
                            rowIncluir["VALORARECEBER"] = clsParser.DecimalParse(sdr["VALOR"].ToString());
                            rowIncluir["IDSITBANCO"] = 0;
                            rowIncluir["SITBANCOCOD"] = 0;
                            rowIncluir["SITUACAONOME"] = "";
                            rowIncluir["IMPRIMIR"] = "N";
                            rowIncluir["VALORPAGO"] = 0;
                            rowIncluir["VALORBAIXANDO"] = 0;
                            rowIncluir["IDVENDEDOR"] = 0;
                            rowIncluir["VENDEDOR"] = "";
                            rowIncluir["VALORCOMISSAO"] = 0;
                            rowIncluir["IDSUPERVISOR"] = 0;
                            rowIncluir["SUPERVISOR"] = "";
                            rowIncluir["VALORCOMISSAOSUP"] = 0;
                            rowIncluir["IDCOORDENADOR"] = 0;
                            rowIncluir["COORDENADOR"] = "";
                            rowIncluir["VALORCOMISSAOGER"] = 0;
                            dtFluxoFinanceiro.Rows.Add(rowIncluir);
                        }
                    }
                }
                dtFluxoFinanceiro.AcceptChanges();
                /////////////////// Termino de Incluir o Aplibank no Fluxo
            }
            //////////////////////// Ver se Vai incluir o Contas a Pagar em Atraso
            if (Inclui_AtrasoPagar == "S")  // como vem de outros lugares pode ser que não tenha que calcular
            {

                sql1 = "";
                sql1 = "SELECT sum(PAGAR.VALORLIQUIDO - (PAGAR.VALORPAGO)) AS VALORARECEBER " +
                // Item a item seria conforme a query abaixo porem só preciso do total conforme acima
                /*sql1 = "SELECT  'BPAG' AS [tabela], PAGAR.VALORDESCONTO AS CREDITO, PAGAR.VALORDESCONTO AS DEBITO, PAGAR.VALORDESCONTO AS SALDO, " +
                "PAGAR.ID, PAGAR.FILIAL, PAGAR.DUPLICATA, PAGAR.POSICAO, PAGAR.POSICAOFIM, PAGAR.EMISSAO, PAGAR.IDDOCUMENTO,  " +
                "DOCFISCAL.COGNOME AS DOCUMENTO, PAGAR.SETOR, PAGAR.IDFORNECEDOR, CLIENTE.COGNOME AS CLIENTE, PAGAR.DATALANCA, PAGAR.EMITENTE,  " +
                "PAGAR.IDHISTORICO, HISTORICOS.CODIGO AS HISTORICO, HISTORICOS.NOME AS HISTORICONOM, PAGAR.IDCENTROCUSTO,  " +
                "CENTROCUSTOS.CODIGO AS CENTROCUSTO, CENTROCUSTOS.NOME AS CENTROCUSTONOM, PAGAR.IDCODIGOCTABIL,  " +
                "CONTACONTABIL.CODIGO AS CTACONTABIL, CONTACONTABIL.NOME AS CTACONTABILNOM, PAGAR.IDNOTAFISCAL, NFCOMPRA.NUMERO AS NOTAFISCAL, " +
                "PAGAR.IDPAGARNFE  AS IDNOTAPRINCIPAL, PAGAR.IDFORMAPAGTO, SITUACAOTIPOTITULO.CODIGO AS FORMAPAGTO, SITUACAOTIPOTITULO.NOME AS FORMAPAGTONOM,  " +
                "PAGAR.OBSERVA, PAGAR.IDBANCO, TAB_BANCOS.CODIGO AS BANCO, TAB_BANCOS.COGNOME AS BANCONOM, PAGAR.IDBANCOINT,  " +
                "BANCOS.CONTA, BANCOS.NOME, PAGAR.CHEGOU, PAGAR.DESPESAPUBLICA, PAGAR.BOLETO, PAGAR.BOLETONRO, PAGAR.DV,  " +
                "PAGAR.BAIXA, PAGAR.VENCIMENTO, PAGAR.VENCIMENTOPREV, PAGAR.VALOR, PAGAR.VALORDESCONTO, PAGAR.ATEVENCIMENTO, PAGAR.VALORLIQUIDO,  " +
                "PAGAR.VALORJUROS, PAGAR.VALORMULTA, PAGAR.VALORLIQUIDO - (PAGAR.VALORPAGO + clsPaGAR.VALORDEVOLVIDO) AS VALORARECEBER, PAGAR.IDSITBANCO,  " +
                "SITUACAOTITULO.CODIGO AS SITUACAOCODIGO, SITUACAOTITULO.NOME AS SITUACAONOME, PAGAR.IMPRIMIR, PAGAR.VALORPAGO, PAGAR.VALORBAIXANDO,  " +
                "PAGAR.VALORDEVOLVIDO, PAGAR.IDVENDEDOR, CLIENTEVEND.COGNOME AS VENDEDOR, PAGAR.VALORCOMISSAO, PAGAR.IDSUPERVISOR,  " +
                "CLIENTESUP.COGNOME AS SUPERVISOR, PAGAR.VALORCOMISSAOSUP, PAGAR.IDCOORDENADOR, CLIENTECOO.COGNOME AS COORDENADOR,  " +
                "PAGAR.VALORCOMISSAOGER, CLIENTE.CREDITO " +
                 */
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
                query = "";
                if (Filial > 0)
                {
                    query = " PAGAR.FILIAL=" + Filial;
                }
                if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " and ";
                    }
                    query = query + " PAGAR.IDFORNECEDOR = " + idCliente + " ";
                }
                if (DataDe.ToString() != "")
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " and ";
                    }
                    if (TipoData == "P")
                    {
                        query = query + " PAGAR.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                        query = query + " and ";
                        query = query + " PAGAR.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
                    }
                    else
                    {
                        query = query + " PAGAR.VENCIMENTO < " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                        query = query + " and ";
                        query = query + " PAGAR.VENCIMENTO < " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
                    }
                }
                if (query.ToString().Length > 0)
                {
                    sql1 = sql1 + " where " + query + " ";
                }
                // GRAVAR o contas a pagar em atrasao
                scn = new SqlConnection(clsInfo.conexaosqldados);
                scn.Open();
                scd = new SqlCommand(sql1, scn);
                sdr = scd.ExecuteReader();
                while (sdr.Read() == true)
                {
                    DataRow rowIncluir = dtFluxoFinanceiro.NewRow();
                    rowIncluir["Tabela"] = "ABCO2"; // para ficar na ordem apos o banco e o contas a receber em atraso se tiver
                    rowIncluir["CREDITO"] = 0;
                    rowIncluir["DEBITO"] = 0;
                    rowIncluir["SALDO"] = 0;
                    rowIncluir["ID"] = 0;
                    rowIncluir["FILIAL"] = 0;
                    rowIncluir["DUPLICATA"] = 0;
                    rowIncluir["POSICAO"] = 0;
                    rowIncluir["POSICAOFIM"] = 0;
                    rowIncluir["EMISSAO"] = (DateTime.Parse(daDataDe.ToString()).AddDays(-1)).ToString("dd/MM/yyyy");
                    rowIncluir["DOCUMENTO"] = "NFE";
                    rowIncluir["SETOR"] = "";
                    rowIncluir["IDCLIENTE"] = 0;
                    rowIncluir["CLIENTE"] = "";
                    rowIncluir["DATALANCA"] = rowIncluir["EMISSAO"];
                    rowIncluir["EMITENTE"] = "";
                    rowIncluir["IDHISTORICO"] = 0;
                    rowIncluir["HISTORICO"] = "";
                    rowIncluir["HISTORICONOM"] = "Contas a Pagar em Atraso até esta data" ;
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
                    rowIncluir["IDBANCOINT"] = 0;
                    rowIncluir["CONTA"] = 0;
                    rowIncluir["NOME"] = "";
                    rowIncluir["CHEGOU"] = "S";
                    rowIncluir["DESPESAPUBLICA"] = "N";
                    rowIncluir["BOLETO"] = "";
                    rowIncluir["BOLETONRO"] = 0;
                    rowIncluir["DV"] = "";
                    rowIncluir["BAIXA"] = "N";
                    rowIncluir["VENCIMENTO"] = rowIncluir["EMISSAO"]; 
                    rowIncluir["VENCIMENTOPREV"] = rowIncluir["EMISSAO"]; 
                    rowIncluir["VALOR"] = clsParser.DecimalParse(sdr["VALORARECEBER"].ToString());
                    rowIncluir["VALORDESCONTO"] = 0;
                    rowIncluir["ATEVENCIMENTO"] = "N";
                    rowIncluir["VALORLIQUIDO"] = clsParser.DecimalParse(sdr["VALORARECEBER"].ToString());
                    rowIncluir["VALORJUROS"] = 0;
                    rowIncluir["VALORMULTA"] = 0;
                    rowIncluir["VALORARECEBER"] = clsParser.DecimalParse(sdr["VALORARECEBER"].ToString());
                    rowIncluir["IDSITBANCO"] = 0;
                    rowIncluir["SITBANCOCOD"] = 0;
                    rowIncluir["SITUACAONOME"] = "";
                    rowIncluir["IMPRIMIR"] = "N";
                    rowIncluir["VALORPAGO"] = 0;
                    rowIncluir["VALORBAIXANDO"] = 0;
                    rowIncluir["IDVENDEDOR"] = 0;
                    rowIncluir["VENDEDOR"] = "";
                    rowIncluir["VALORCOMISSAO"] = 0;
                    rowIncluir["IDSUPERVISOR"] = 0;
                    rowIncluir["SUPERVISOR"] = "";
                    rowIncluir["VALORCOMISSAOSUP"] = 0;
                    rowIncluir["IDCOORDENADOR"] = 0;
                    rowIncluir["COORDENADOR"] = "";
                    rowIncluir["VALORCOMISSAOGER"] = 0;
                    dtFluxoFinanceiro.Rows.Add(rowIncluir);
                }
                dtFluxoFinanceiro.AcceptChanges();
            }
            /////////////////////// Termino de Incluir o Contas a Pagar em Atraso 

            //////////////////////// Ver se Vai incluir o Contas a Receber em Atraso
            if (Inclui_AtrasoReceber == "S")  // como vem de outros lugares pode ser que não tenha que calcular
            {
                sql1 = "";
                sql1 = "SELECT SUM(RECEBER.VALORLIQUIDO - (RECEBER.VALORPAGO)) AS VALORARECEBER " +
                /*
                sql1 = "SELECT  'AREC' AS Tabela,  RECEBER.VALORDESCONTO AS CREDITO, RECEBER.VALORDESCONTO AS DEBITO, RECEBER.VALORDESCONTO AS SALDO, " +
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
                "RECEBER.VALORMULTA, RECEBER.VALORLIQUIDO - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO) AS VALORARECEBER, RECEBER.IDSITBANCO,  " +
                "SITUACAOTITULO.CODIGO AS SITUACAOCODIGO, SITUACAOTITULO.NOME AS SITUACAONOME, RECEBER.IMPRIMIR, RECEBER.VALORPAGO,  " +
                "RECEBER.VALORBAIXANDO, RECEBER.VALORDEVOLVIDO, RECEBER.IDVENDEDOR, CLIENTEVEND.COGNOME AS VENDEDOR, RECEBER.VALORCOMISSAO,  " +
                "RECEBER.IDSUPERVISOR, CLIENTESUP.COGNOME AS SUPERVISOR, RECEBER.VALORCOMISSAOSUP, RECEBER.IDCOORDENADOR,  " +
                "CLIENTECOO.COGNOME AS COORDENADOR, RECEBER.VALORCOMISSAOGER, CLIENTE.CREDITO " + */

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
                query = "";
                if (Filial > 0)
                {
                    query = " RECEBER.FILIAL=" + Filial;
                }
                if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
                {
                    if (query.Length > 0)
                    {
                        query = query + " and ";
                    }
                    query = query + " RECEBER.IDCLIENTE = " + idCliente + " ";
                }
                if (DataDe.ToString() != "")
                {
                    if (query.ToString().Length > 0)
                    {
                        query = query + " and ";
                    }
                    if (TipoData == "P")
                    {
                        query = query + " RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                        query = query + " and ";
                        query = query + " RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
                    }
                    else
                    {
                        query = query + " RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                        query = query + " and ";
                        query = query + " RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
                    }
                }
                if (query.ToString().Length > 0)
                {
                    sql1 = sql1 + " where " + query + " ";
                }
                // GRAVAR o contas a pagar em atrasao
                scn = new SqlConnection(clsInfo.conexaosqldados);
                scn.Open();
                scd = new SqlCommand(sql1, scn);
                sdr = scd.ExecuteReader();
                while (sdr.Read() == true)
                {
                    DataRow rowIncluir = dtFluxoFinanceiro.NewRow();
                    rowIncluir["Tabela"] = "ABCO1"; // para ficar na ordem apos o banco e antes do contas a pagar em atraso
                    rowIncluir["CREDITO"] = 0;
                    rowIncluir["DEBITO"] = 0;
                    rowIncluir["SALDO"] = 0;
                    rowIncluir["ID"] = 0;
                    rowIncluir["FILIAL"] = 0;
                    rowIncluir["DUPLICATA"] = 0;
                    rowIncluir["POSICAO"] = 0;
                    rowIncluir["POSICAOFIM"] = 0;
                    rowIncluir["EMISSAO"] = (DateTime.Parse(daDataDe.ToString()).AddDays(-1)).ToString("dd/MM/yyyy");
                    rowIncluir["DOCUMENTO"] = "NFV";
                    rowIncluir["SETOR"] = "";
                    rowIncluir["IDCLIENTE"] = 0;
                    rowIncluir["CLIENTE"] = "";
                    rowIncluir["DATALANCA"] = rowIncluir["EMISSAO"];
                    rowIncluir["EMITENTE"] = "";
                    rowIncluir["IDHISTORICO"] = 0;
                    rowIncluir["HISTORICO"] = "";
                    rowIncluir["HISTORICONOM"] = "Contas a Receber em Atraso até esta data";
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
                    rowIncluir["IDBANCOINT"] = 0;
                    rowIncluir["CONTA"] = 0;
                    rowIncluir["NOME"] = "";
                    rowIncluir["CHEGOU"] = "S";
                    rowIncluir["DESPESAPUBLICA"] = "N";
                    rowIncluir["BOLETO"] = "";
                    rowIncluir["BOLETONRO"] = 0;
                    rowIncluir["DV"] = "";
                    rowIncluir["BAIXA"] = "N";
                    rowIncluir["VENCIMENTO"] = rowIncluir["EMISSAO"];
                    rowIncluir["VENCIMENTOPREV"] = rowIncluir["EMISSAO"];
                    rowIncluir["VALOR"] = clsParser.DecimalParse(sdr["VALORARECEBER"].ToString());
                    rowIncluir["VALORDESCONTO"] = 0;
                    rowIncluir["ATEVENCIMENTO"] = "N";
                    rowIncluir["VALORLIQUIDO"] = clsParser.DecimalParse(sdr["VALORARECEBER"].ToString());
                    rowIncluir["VALORJUROS"] = 0;
                    rowIncluir["VALORMULTA"] = 0;
                    rowIncluir["VALORARECEBER"] = clsParser.DecimalParse(sdr["VALORARECEBER"].ToString());
                    rowIncluir["IDSITBANCO"] = 0;
                    rowIncluir["SITUACAOCODIGO"] = 0;
                    rowIncluir["SITUACAONOME"] = "";
                    rowIncluir["IMPRIMIR"] = "N";
                    rowIncluir["VALORPAGO"] = 0;
                    rowIncluir["VALORBAIXANDO"] = 0;
                    rowIncluir["IDVENDEDOR"] = 0;
                    rowIncluir["VENDEDOR"] = "";
                    rowIncluir["VALORCOMISSAO"] = 0;
                    rowIncluir["IDSUPERVISOR"] = 0;
                    rowIncluir["SUPERVISOR"] = "";
                    rowIncluir["VALORCOMISSAOSUP"] = 0;
                    rowIncluir["IDCOORDENADOR"] = 0;
                    rowIncluir["COORDENADOR"] = "";
                    rowIncluir["VALORCOMISSAOGER"] = 0;
                    dtFluxoFinanceiro.Rows.Add(rowIncluir);
                }
                dtFluxoFinanceiro.AcceptChanges();
            }
            /////////////////////// Termino de Incluir o Contas a RECEBER em Atraso 
            DataRow drFluxo;
            DateTime dtCompensar;
            // ACERTAR AS DATAS DE COMPENSAÇÃO
            dtFluxoFinanceiro.DefaultView.Sort = "IDBANCOINT"; //, TABELA, CLIENTE ASC";
            dtFluxoFinanceiro = dtFluxoFinanceiro.DefaultView.ToTable();
            dias = 0;
            idBanco = 0;
            for (Int32 x = 0; x < dtFluxoFinanceiro.Rows.Count; x++)
            {// fazer o calculo
                drFluxo = dtFluxoFinanceiro.Rows[x];
                // deixar as datas sem a hora
                drFluxo["EMISSAO"] = DateTime.Parse(drFluxo["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
                drFluxo["VENCIMENTO"] = DateTime.Parse(drFluxo["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                drFluxo["VENCIMENTOPREV"] = DateTime.Parse(drFluxo["VENCIMENTOPREV"].ToString()).ToString("dd/MM/yyyy");

                if (clsParser.Int32Parse(drFluxo["IDBANCOINT"].ToString()) != idBanco)
                {  // pesquisar O cmapo compensar (qtde de dias) que cai o cheque dentro da cta do aplibank
                    idBanco = clsParser.Int32Parse(drFluxo["IDBANCOINT"].ToString());
                    if (idBanco > 0)
                    {
                        dias = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select compensar from BANCOS where ID = " + idBanco, "").ToString());
                    }
                    else
                    {
                        dias = 0;
                    }
                }
                if (clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString()) != 0)
                {
                    if (drFluxo["VENCIMENTO"].ToString() != "")
                    {
                        if (drFluxo["TABELA"].ToString() == "AREC")
                        {
                            Int32 xdias = 0;

                            // verificar o cliente se é bom / ruim / medio [CREDITO]
                            if (drFluxo["CREDITO"].Equals("B"))
                            {
                                xdias = Dias_ClienteBom;
                            }
                            else if (drFluxo["CREDITO"].Equals("B"))
                            {
                                xdias = Dias_ClienteMedio;
                            }
                            else
                            {
                                xdias = Dias_ClienteRuim;
                            }


                            dtCompensar = DateTime.Parse(drFluxo["VENCIMENTO"].ToString());
                            if (dtCompensar >= DateTime.Now.AddDays(dias * -1))
                            {   // se ainda não venceu pode acrescentar 
                                drFluxo["VENCIMENTO"] = dtCompensar.AddDays(dias + xdias).ToString("dd/MM/yyyy");
                            }
                            else
                            {   // se já venceu não acrescentar + nada
                                drFluxo["VENCIMENTO"] = dtCompensar.AddDays(dias + xdias).ToString("dd/MM/yyyy");
                            }
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
                                
                                //idFeriado = clsParser.Int32Parse(clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "FERIADOS", "ID", "DATA", dtCompensar.ToString("yyyy-MM-dd")).ToString());

                                idFeriado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from FERIADOS where data = " + clsParser.SqlDateTimeFormat(dtCompensar.ToString("dd/MM/yyyy") + " 00:00", true)));
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
                        else if (drFluxo["TABELA"].ToString() == "ABCO")
                        {   // JA É O DIA DA COMPENSAÇÃO !!!
                        }
                        else if (drFluxo["TABELA"].ToString() == "ABCO1")
                        {   // JA É O DIA DA COMPENSAÇÃO !!!
                        }
                        else if (drFluxo["TABELA"].ToString() == "ABCO2")
                        {   // JA É O DIA DA COMPENSAÇÃO !!!
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
                                //idFeriado = clsParser.Int32Parse(clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "FERIADOS", "ID", "DATA", dtCompensar.ToString("yyyy-MM-dd")).ToString());
                                idFeriado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from FERIADOS where data = " + clsParser.SqlDateTimeFormat(dtCompensar.ToString("dd/MM/yyyy") + " 00:00", true)));

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
                {
                    drFluxo.Delete();
                }


            }
            dtFluxoFinanceiro.AcceptChanges();
            //    
            dtFluxoFinanceiro.DefaultView.Sort = "VENCIMENTO, TABELA, DUPLICATA"; //, TABELA, CLIENTE ASC";
            dtFluxoFinanceiro = dtFluxoFinanceiro.DefaultView.ToTable();
            saldo = 0;
            for (Int32 x = 0; x < dtFluxoFinanceiro.Rows.Count; x++)
            {// fazer o calculo
                drFluxo = dtFluxoFinanceiro.Rows[x];
                credito = 0;
                debito = 0;
                if (drFluxo["TABELA"].ToString() == "AREC")
                {
                    //if (clsParser.Int32Parse(drFluxo["SITUACAOCODIGO"].ToString()) > 0)
                    if (clsParser.Int32Parse(drFluxo["SITBANCOCOD"].ToString()) > 0)
                    {
                        credito = 0;
                    }
                    else
                    {
                        credito = clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString());
                    }
                }else if (drFluxo["TABELA"].ToString() == "ABCO")
                {
                    if (clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString()) > 0)
                    {
                        credito = clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString());
                    }
                    else
                    {
                        debito = clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString());
                    }
                }
                else if (drFluxo["TABELA"].ToString() == "ABCO1")
                {
                    if (clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString()) > 0)
                    {
                        credito = clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString());
                    }
                }
                else if (drFluxo["TABELA"].ToString() == "ABCO2")
                {
                    if (clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString()) > 0)
                    {
                        debito = clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString());
                    }
                }
                else
                {
                    debito = clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString());
                }
                if (credito == 0 && debito == 0)
                {
                    drFluxo.Delete();
                }
                else
                {
                    if (debito < 0)
                    {
                        debito = debito * -1;
                    }
                    saldo = ((saldo + credito) - debito);
                    drFluxo["CREDITO"] = credito;
                    drFluxo["DEBITO"] = debito;
                    drFluxo["SALDO"] = saldo;
                }
            }
            dtFluxoFinanceiro.AcceptChanges();
            return dtFluxoFinanceiro;
        }

        private static GridColuna[] dtFluxoFinanceiroColunas = new GridColuna[]
        {
            new GridColuna("Fil", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Duplicata", "DUPLICATA", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Ini", "POSICAO", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fim", "POSICAOFIM", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Emissão", "EMISSAO", 70, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc", "DOCUMENTO", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cliente", "CLIENTE", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Venc.", "VENCIMENTO", 70, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Pagto", "VENCIMENTO", 70, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Credito", "CREDITO", 120, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Debito", "DEBITO", 120, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Saldo", "SALDO", 130, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Observa", "OBSERVA", 230, true, DataGridViewContentAlignment.MiddleRight)
        };

        // segundo método
        public static void GridFluxoFinanceiroMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtFluxoFinanceiroColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            dgv.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["CREDITO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["DEBITO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["SALDO"].DefaultCellStyle.Format = "N2";
        }

        public static DataTable FichaFinanceira(Int32 Filial, Int32 idCliente, String Cliente, String TipoData, DateTime daDataDe, DateTime daDataAte, Decimal SaldoAnterior)
        {
            String sql;
            String query;
            String sql1;
            Decimal saldo = 0;
            Decimal credito = 0;
            Decimal debito = 0;
            DataTable dtFichaFinanceira;

            sql = "SELECT  'AREC' AS Tabela,  RECEBER.VALORDESCONTO AS CREDITO, RECEBER.VALORDESCONTO AS DEBITO, RECEBER.VALORDESCONTO AS SALDO, " +
                "RECEBER.ID, RECEBER.FILIAL, RECEBER.DUPLICATA, RECEBER.POSICAO, RECEBER.POSICAOFIM, RECEBER.EMISSAO, RECEBER.IDDOCUMENTO,  " +
                "DOCFISCAL.COGNOME AS DOCUMENTO, RECEBER.SETOR, RECEBER.IDCLIENTE, CLIENTE.COGNOME AS CLIENTE, " +
                "RECEBER.IDNOTAFISCAL, NFVENDA.NUMERO AS NOTAFISCAL, RECEBER.IDRECEBERNFV AS IDNOTAPRINCIPAL, RECEBER.ID AS [IDRECEBER], " +
                "RECEBER.OBSERVA, RECEBER.BAIXA, RECEBER.VENCIMENTO, RECEBER.VENCIMENTO AS [DATAPAGO], " +
                "RECEBER.VALOR, RECEBER.VALORDESCONTO, RECEBER.ATEVENCIMENTO, RECEBER.VALORLIQUIDO, RECEBER.VALORJUROS, " +
                "RECEBER.VALORMULTA, RECEBER.VALORLIQUIDO - (RECEBER.VALORPAGO) AS VALORARECEBER, " +
                " '' AS [COB], RECEBER.VALORDESCONTO AS VALOR01, '' AS [DEBCRED], '' AS [BAIXOUCOMO] " +
                "FROM RECEBER " +
                "LEFT OUTER JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBER.IDDOCUMENTO " +
                "LEFT OUTER JOIN CLIENTE ON CLIENTE.ID = RECEBER.IDCLIENTE " +
                "LEFT OUTER JOIN NFVENDA ON NFVENDA.ID = RECEBER.IDNOTAFISCAL ";
            query = "";
            if (Filial > 0)
            {
                query = " RECEBER.FILIAL=" + Filial;
            }
            if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
            {
                if (query.Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " RECEBER.IDCLIENTE = " + idCliente + " ";
            }
            if (daDataDe.ToString() != "")
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                query = query + " and ";
                query = query + " RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
            }
            if (query.ToString().Length > 0)
            {
                sql = sql + " where " + query + " ";
            }

            sql = sql + " union " +
            "SELECT  'BPAG' AS [tabela], PAGAR.VALORDESCONTO AS CREDITO, PAGAR.VALORDESCONTO AS DEBITO, PAGAR.VALORDESCONTO AS SALDO, " +
            "PAGAR.ID, PAGAR.FILIAL, PAGAR.DUPLICATA, PAGAR.POSICAO, PAGAR.POSICAOFIM, PAGAR.EMISSAO, PAGAR.IDDOCUMENTO, " +
            "DOCFISCAL.COGNOME AS DOCUMENTO, PAGAR.SETOR, PAGAR.IDFORNECEDOR, CLIENTE.COGNOME AS CLIENTE, " +
            "PAGAR.IDNOTAFISCAL, NFCOMPRA.NUMERO AS NOTAFISCAL, PAGAR.IDPAGARNFE  AS IDNOTAPRINCIPAL, PAGAR.ID AS [IDRECEBER], " +
            "PAGAR.OBSERVA, PAGAR.BAIXA, PAGAR.VENCIMENTO, PAGAR.VENCIMENTO AS [DATAPAGO], " +
            "PAGAR.VALOR, PAGAR.VALORDESCONTO, PAGAR.ATEVENCIMENTO, PAGAR.VALORLIQUIDO, PAGAR.VALORJUROS, " +
            "PAGAR.VALORMULTA, PAGAR.VALORLIQUIDO - (PAGAR.VALORPAGO) AS VALORARECEBER, " +
            " '' AS [COB], PAGAR.VALORDESCONTO AS VALOR01, '' AS [DEBCRED], '' AS [BAIXOUCOMO] " +
            "FROM PAGAR " +
            "LEFT OUTER JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAR.IDDOCUMENTO " +
            "LEFT OUTER JOIN CLIENTE ON CLIENTE.ID = PAGAR.IDFORNECEDOR " +
            "LEFT OUTER JOIN NFCOMPRA ON NFCOMPRA.ID = PAGAR.IDNOTAFISCAL ";
            query = "";
            if (Filial > 0)
            {
                query = " PAGAR.FILIAL=" + Filial;
            }
            if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " PAGAR.IDFORNECEDOR = " + idCliente + " ";
            }
            if (daDataDe.ToString() != "")
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                query = query + " and ";
                query = query + " PAGAR.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
            }
            if (query.ToString().Length > 0)
            {
                sql = sql + " where " + query + " ";
            }
            SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
            //scd.Parameters.Add("@id", SqlDbType.Int).Value = idPagar;
            dtFichaFinanceira = new DataTable();
            sda.Fill(dtFichaFinanceira);

            //////////////////// Incluir o Ctas Recebidas
            sql1 = "";
            sql1 = "SELECT  'BREC' AS Tabela,  RECEBIDA.VALORDESCONTO AS CREDITO, RECEBIDA.VALORDESCONTO AS DEBITO, RECEBIDA.VALORDESCONTO AS SALDO, " +
                "RECEBIDA.ID, RECEBIDA.FILIAL, RECEBIDA.DUPLICATA, RECEBIDA.POSICAO, RECEBIDA.POSICAOFIM, RECEBIDA.EMISSAO, RECEBIDA.IDDOCUMENTO,  " +
                "DOCFISCAL.COGNOME AS DOCUMENTO, RECEBIDA.SETOR, RECEBIDA.IDCLIENTE, CLIENTE.COGNOME AS CLIENTE, " +
                "RECEBIDA.IDNOTAFISCAL, NFVENDA.NUMERO AS NOTAFISCAL, RECEBIDA.IDRECEBERNFV AS IDNOTAPRINCIPAL,  RECEBIDA.IDRECEBER AS IDRECEBER, " +
                "RECEBIDA.OBSERVA, RECEBIDA.BAIXA, RECEBIDA.VENCIMENTO, RECEBIDA.VENCIMENTO AS [DATAPAGO], " +
                "RECEBIDA.VALOR, RECEBIDA.VALORDESCONTO, RECEBIDA.ATEVENCIMENTO, RECEBIDA.VALORLIQUIDO, RECEBIDA.VALORJUROS, " +
                "RECEBIDA.VALORMULTA, RECEBIDA.VALORLIQUIDO - (RECEBIDA.VALORPAGO) AS VALORARECEBIDA, " +
                "RECEBIDA01.ID, RECEBIDA01.IDDUPLICATA, RECEBIDA01.DATAENVIO, RECEBIDA01.DATAOK, " +
                "SITUACAOCOBRANCACOD.CODIGO + '=' + SITUACAOCOBRANCACOD.NOME AS [COB], " +
                "RECEBIDA01.VALOR AS VALOR01, RECEBIDA01.DEBCRED, RECEBIDA01.BAIXOUCOMO  " +
                "FROM RECEBIDA " +
                "LEFT OUTER JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBIDA.IDDOCUMENTO " +
                "LEFT OUTER JOIN CLIENTE ON CLIENTE.ID = RECEBIDA.IDCLIENTE " +
                "LEFT OUTER JOIN NFVENDA ON NFVENDA.ID = RECEBIDA.IDNOTAFISCAL " +
                "LEFT OUTER JOIN RECEBIDA01 on RECEBIDA01.IDDUPLICATA=RECEBIDA.ID " +
                "LEFT OUTER JOIN SITUACAOCOBRANCACOD on SITUACAOCOBRANCACOD.id=RECEBIDA01.IDCOBRANCACOD " +
                "LEFT OUTER JOIN SITUACAOCOBRANCACOD1 on SITUACAOCOBRANCACOD1.id=RECEBIDA01.IDCOBRANCAHIS ";
            /////// filtrar pelo cognome
            query = "";
            if (Filial > 0)
            {
                query = " RECEBIDA.FILIAL=" + Filial;
            }
            if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " RECEBIDA.IDCLIENTE = " + idCliente + " ";
            }
            if (daDataDe.ToString() != "")
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " RECEBIDA.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                query = query + " and ";
                query = query + " RECEBIDA.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
            }
            if (query.ToString().Length > 0)
            {
                sql1 = sql1 + " where " + query + " ";
            }

            DataRow drFluxo;
            DataTable dtRecebidas;

            // Incluir no dtFichaFinanceira as duplicatas recebidas
            SqlConnection scn = new SqlConnection(clsInfo.conexaosqldados);
            scn.Open();
            SqlCommand scd = new SqlCommand(sql1, scn);
            SqlDataReader sdr = scd.ExecuteReader();
            while (sdr.Read() == true)
            {
                DataRow rowIncluir = dtFichaFinanceira.NewRow();
                //  posicaoPagarObserva = dtPagarObserva.Rows.Count + 1;
                // Se for cta recebida - verificar se a duplicata principal esta lançada
                Boolean incluir = true;
                Int32 linha = 0;
                if (sdr["TABELA"].ToString() == "BREC") // CONTA RECEBIDA
                {
                    dtRecebidas = dtFichaFinanceira;
                    for (Int32 x = 0; x < dtRecebidas.Rows.Count; x++)
                    {// fazer o calculo
                        drFluxo = dtRecebidas.Rows[x];
                        if (clsParser.Int32Parse(drFluxo["IDRECEBER"].ToString()) == clsParser.Int32Parse(sdr["IDRECEBER"].ToString())) // LOCALIZAR PELO ID
                        { // achou a duplicata principal esta no fluxo  - sair sem incluir
                            linha = x;
                            incluir = false;
                            break;
                        }
                    }

                    if (dtRecebidas.Rows.Count == 0)
                    {
                        continue;
                    }

                    if (incluir == true)
                    {  
                        drFluxo = dtRecebidas.Rows[linha];
                        rowIncluir["TABELA"] = "AREC";
                        rowIncluir["CREDITO"] = 0;
                        rowIncluir["DEBITO"] = 0;
                        rowIncluir["SALDO"] = 0;
                        rowIncluir["ID"] = clsParser.Int32Parse(sdr["ID"].ToString());
                        rowIncluir["FILIAL"] = clsParser.Int32Parse(sdr["FILIAL"].ToString());
                        rowIncluir["DUPLICATA"] = clsParser.Int32Parse(sdr["DUPLICATA"].ToString());
                        rowIncluir["POSICAO"] = clsParser.Int32Parse(sdr["POSICAO"].ToString());
                        rowIncluir["POSICAOFIM"] = clsParser.Int32Parse(sdr["POSICAOFIM"].ToString());
                        rowIncluir["EMISSAO"] = DateTime.Parse(sdr["EMISSAO"].ToString()).ToString("dd/MM/yyyy"); 
                        rowIncluir["DOCUMENTO"] = sdr["DOCUMENTO"].ToString();
                        rowIncluir["SETOR"] = sdr["SETOR"].ToString();
                        rowIncluir["IDCLIENTE"] = clsParser.Int32Parse(sdr["IDCLIENTE"].ToString());
                        rowIncluir["CLIENTE"] = sdr["CLIENTE"].ToString();
                        rowIncluir["IDNOTAFISCAL"] = clsParser.Int32Parse(sdr["IDNOTAPRINCIPAL"].ToString());
                        rowIncluir["NOTAFISCAL"] = clsParser.Int32Parse(sdr["NOTAFISCAL"].ToString());
                        rowIncluir["IDRECEBER"] = clsParser.Int32Parse(sdr["IDRECEBER"].ToString());
                        rowIncluir["OBSERVA"] = sdr["OBSERVA"].ToString();
                        rowIncluir["BAIXA"] = sdr["BAIXA"].ToString();
                        rowIncluir["VENCIMENTO"] = DateTime.Parse(sdr["VENCIMENTO"].ToString()).ToString("dd/MM/yyyy"); 
                        if (clsParser.DateTimeParse(sdr["DATAOK"].ToString()) == null || sdr["DATAOK"].ToString() == "")
                        {
                            //rowIncluir["DATAPAGO"] = clsParser.SqlDateTimeParse(sdr["DATAOK"].ToString()).ToString();
                            //rowIncluir["DATAPAGO"] = "";
                        }
                        else
                        {
                            rowIncluir["DATAPAGO"] = DateTime.Parse(sdr["DATAOK"].ToString()).ToString("dd/MM/yyyy"); 
                        }
                        rowIncluir["VALOR"] = clsParser.DecimalParse(sdr["VALOR"].ToString());
                        rowIncluir["VALORDESCONTO"] = clsParser.DecimalParse(sdr["VALORDESCONTO"].ToString());
                        rowIncluir["ATEVENCIMENTO"] = sdr["ATEVENCIMENTO"].ToString();
                        rowIncluir["VALORJUROS"] = clsParser.DecimalParse(sdr["VALORJUROS"].ToString());
                        rowIncluir["VALORMULTA"] = clsParser.DecimalParse(sdr["VALORMULTA"].ToString());
                        rowIncluir["VALORARECEBER"] = 0;
                        rowIncluir["COB"] = "";
                        rowIncluir["VALOR01"] = 0;
                        rowIncluir["DEBCRED"] = "";
                        rowIncluir["BAIXOUCOMO"] = "";
                        dtFichaFinanceira.Rows.Add(rowIncluir);
                        rowIncluir = dtFichaFinanceira.NewRow();

                    }
                }
                rowIncluir["Tabela"] = sdr["TABELA"].ToString();
                rowIncluir["CREDITO"] = 0;
                rowIncluir["DEBITO"] = 0;
                rowIncluir["SALDO"] = 0;
                rowIncluir["ID"] = clsParser.Int32Parse(sdr["ID"].ToString());
                rowIncluir["FILIAL"] = clsParser.Int32Parse(sdr["FILIAL"].ToString());
                rowIncluir["DUPLICATA"] = clsParser.Int32Parse(sdr["DUPLICATA"].ToString());
                rowIncluir["POSICAO"] = clsParser.Int32Parse(sdr["POSICAO"].ToString());
                rowIncluir["POSICAOFIM"] = clsParser.Int32Parse(sdr["POSICAOFIM"].ToString());
                rowIncluir["EMISSAO"] = DateTime.Parse(sdr["EMISSAO"].ToString()).ToString("dd/MM/yyyy");
                rowIncluir["DOCUMENTO"] = sdr["DOCUMENTO"].ToString();
                rowIncluir["SETOR"] = sdr["SETOR"].ToString();
                rowIncluir["IDCLIENTE"] = clsParser.Int32Parse(sdr["IDCLIENTE"].ToString());
                rowIncluir["CLIENTE"] = sdr["CLIENTE"].ToString();
                rowIncluir["IDNOTAFISCAL"] = clsParser.Int32Parse(sdr["IDNOTAPRINCIPAL"].ToString());
                rowIncluir["NOTAFISCAL"] = clsParser.Int32Parse(sdr["NOTAFISCAL"].ToString());
                rowIncluir["IDRECEBER"] = clsParser.Int32Parse(sdr["IDRECEBER"].ToString());
                rowIncluir["OBSERVA"] = sdr["OBSERVA"].ToString();
                rowIncluir["BAIXA"] = sdr["BAIXA"].ToString();
                rowIncluir["VENCIMENTO"] = clsParser.DateTimeParse(sdr["DATAOK"].ToString()).ToString("dd/MM/yyyy");
                rowIncluir["DATAPAGO"] = clsParser.DateTimeParse(sdr["DATAOK"].ToString()).ToString("dd/MM/yyyy");
                rowIncluir["VALOR"] = clsParser.DecimalParse(sdr["VALOR"].ToString());
                rowIncluir["VALORDESCONTO"] = clsParser.DecimalParse(sdr["VALORDESCONTO"].ToString());
                rowIncluir["ATEVENCIMENTO"] = sdr["ATEVENCIMENTO"].ToString();
                rowIncluir["VALORJUROS"] = clsParser.DecimalParse(sdr["VALORJUROS"].ToString());
                rowIncluir["VALORMULTA"] = clsParser.DecimalParse(sdr["VALORMULTA"].ToString());
                rowIncluir["VALORARECEBER"] = 0;
                rowIncluir["COB"] = sdr["COB"].ToString();
                rowIncluir["VALOR01"] = clsParser.DecimalParse(sdr["VALOR01"].ToString());
                rowIncluir["DEBCRED"] = sdr["DEBCRED"].ToString();
                rowIncluir["BAIXOUCOMO"] = sdr["BAIXOUCOMO"].ToString();
                dtFichaFinanceira.Rows.Add(rowIncluir);
            }
            dtFichaFinanceira.AcceptChanges();
            
            /////////////////// Termino de Incluir o Pedido de Compras no Fluxo
            //    
            dtFichaFinanceira.DefaultView.Sort = "VENCIMENTO, TABELA, DUPLICATA"; //, TABELA, CLIENTE ASC";
            dtFichaFinanceira = dtFichaFinanceira.DefaultView.ToTable();
            saldo = SaldoAnterior;
            for (Int32 x = 0; x < dtFichaFinanceira.Rows.Count; x++)
            {// fazer o calculo
                drFluxo = dtFichaFinanceira.Rows[x];
                credito = 0;
                debito = 0;
                if (drFluxo["TABELA"].ToString() == "AREC") // CONTAS A RECEBER
                {
                    credito = clsParser.DecimalParse(drFluxo["VALOR"].ToString());
                }
                else if (drFluxo["TABELA"].ToString() == "BREC") // CONTAS RECEBIDAS
                {
                    if (drFluxo["DEBCRED"].ToString() == "C")
                    {
                        debito = clsParser.DecimalParse(drFluxo["VALOR01"].ToString());
                    }
                    else
                    {
                        credito = clsParser.DecimalParse(drFluxo["VALOR01"].ToString());
                    }
                }
                else if (drFluxo["TABELA"].ToString() == "BPAG")
                {
                    debito = clsParser.DecimalParse(drFluxo["VALORARECEBER"].ToString());
                }

                if (credito == 0 && debito == 0)
                {
//                    drFluxo.Delete();
                }
                saldo = ((saldo + credito) - debito);
                drFluxo["CREDITO"] = credito;
                drFluxo["DEBITO"] = debito;
                drFluxo["SALDO"] = saldo;
            }
            dtFichaFinanceira.AcceptChanges();
            return dtFichaFinanceira;
        }

        private static GridColuna[] dtFichaFinanceiraColunas = new GridColuna[]
        {
            new GridColuna("Fil", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Pedido", "DUPLICATA", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Ini", "POSICAO", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fim", "POSICAOFIM", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Emissão", "EMISSAO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc", "TABELA", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cliente", "CLIENTE", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("  Data", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
//            new GridColuna("Pagto", "VENCIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("   Credito", "CREDITO", 120, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("   Debito", "DEBITO", 120, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("   Saldo", "SALDO", 130, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Observa", "OBSERVA", 200, true, DataGridViewContentAlignment.MiddleRight)
        };

        // segundo método
        public static void GridFichaFinanceiraMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtFichaFinanceiraColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            dgv.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["CREDITO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["DEBITO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["SALDO"].DefaultCellStyle.Format = "N2";
        }
        ///////////////
        public static DataTable FichaComercial(Int32 Filial, Int32 idCliente, String Cliente, DateTime daDataDe, DateTime daDataAte)
        {
            String sql;
            String query;
            DataTable dtFichaComercial;
            sql = "SELECT  PEDIDO.FILIAL, 'PED' AS Tabela, PEDIDO.NUMERO AS [NRONF], PEDIDO.DATA as [DATAEMISSAO], PEDIDO.IDCLIENTE, CLIENTE.COGNOME AS [CLIENTE], PEDIDO1.ID AS [IDITEM], " +
                  "PEDIDO1.IDPEDIDO AS [IDNF], PEDIDO1.TIPOITEM, QTDE, UNIDADE.CODIGO AS UNIDADE_CODIGO, PECAS.CODIGO AS PECAS_CODIGO, PECAS.NOME AS PECAS_NOME, PRECO, PEDIDO1.TOTALMERCADO, " +
                  "PEDIDO1.TOTALNOTA,PEDIDO1.PESO, PEDIDO1.PESOTOTAL " +
                  "FROM PEDIDO1 " +
                  "INNER JOIN PEDIDO ON PEDIDO.ID = PEDIDO1.IDPEDIDO " +
                  "LEFT JOIN CLIENTE ON PEDIDO.IDCLIENTE = CLIENTE.ID " +
                  "LEFT JOIN PECAS ON PEDIDO1.IDCODIGO = PECAS.ID " +
                  "LEFT JOIN UNIDADE ON PEDIDO1.IDUNIDADE = UNIDADE.ID ";
            query = "";
            if (Filial > 0)
            {
                query = " PEDIDO.FILIAL=" + Filial;
            }
            if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
            {
                if (query.Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " PEDIDO.IDCLIENTE = " + idCliente + " ";
            }
            if (daDataDe.ToString() != "")
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " PEDIDO.DATA >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                query = query + " and ";
                query = query + " PEDIDO.DATA <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
            }
            if (query.ToString().Length > 0)
            {
                sql = sql + " where " + query + " ";
            }
            if (query.ToString().Length > 0)
            {
                sql = sql + " order by PEDIDO.DATA, PEDIDO.ID, PECAS.NOME " ;
            }



            SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
            //scd.Parameters.Add("@id", SqlDbType.Int).Value = idPagar;
            dtFichaComercial = new DataTable();
            sda.Fill(dtFichaComercial);
            return dtFichaComercial;
        }

        private static GridColuna[] dtFichaComercialColunas = new GridColuna[]
        {
            new GridColuna("Fil", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Tab", "TABELA", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nro Ped", "NRONF", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Data", "DATAEMISSAO", 65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Cliente", "CLIENTE", 0, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tipo", "TIPOITEM", 0, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("CFO", "CFOP_CFOP", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qtde", "QTDE", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNIDADE_CODIGO", 25, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Codigo", "PECAS_CODIGO", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "PECAS_NOME", 160, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Preço Unit", "PRECO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total Merc", "TOTALMERCADO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total Nota", "TOTALNOTA", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Peso Un", "PESO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Peso T.", "PESOTOTAL", 70, true, DataGridViewContentAlignment.MiddleRight),
        };

        // segundo método
        public static void GridFichaComercialMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtFichaComercialColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            dgv.Columns["DATAEMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["PRECO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALMERCADO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
            dgv.Columns["PESO"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PESOTOTAL"].DefaultCellStyle.Format = "N3";
        }

        public static DataTable FichaComercialCompras(Int32 Filial, Int32 idCliente, String Cliente, DateTime daDataDe, DateTime daDataAte)
        {
            String sql;
            String query;
            DataTable dtFichaComercialCompras;
            sql = "SELECT  NFCOMPRA.FILIAL, 'PED' AS Tabela, NFCOMPRA.NUMERO AS [NRONF], NFCOMPRA.DATA as [DATAEMISSAO], NFCOMPRA.IDFORNECEDOR, CLIENTE.COGNOME AS [CLIENTE], NFCOMPRA1.ID AS [IDITEM], " +
                  "NFCOMPRA1.IDPEDIDO AS [IDNF], NFCOMPRA1.TIPOPRODUTO, QTDE, UNIDADE.CODIGO AS UNIDADE_CODIGO, PECAS.CODIGO AS PECAS_CODIGO, PECAS.NOME AS PECAS_NOME, PRECO, NFCOMPRA1.TOTALMERCADO, " +
                  "NFCOMPRA1.TOTALNOTA,NFCOMPRA1.PESO, NFCOMPRA1.PESOTOTAL " +
                  "FROM NFCOMPRA1 " +
                  "INNER JOIN NFCOMPRA ON NFCOMPRA.ID = NFCOMPRA1.NUMERO " +
                  "LEFT JOIN CLIENTE ON NFCOMPRA.IDFORNECEDOR = CLIENTE.ID " +
                  "LEFT JOIN PECAS ON NFCOMPRA1.IDCODIGO = PECAS.ID " +
                  "LEFT JOIN UNIDADE ON NFCOMPRA1.IDUNID = UNIDADE.ID ";
            query = "";
            if (Filial > 0)
            {
                query = " NFCOMPRA.FILIAL=" + Filial;
            }
            if (idCliente > 0)  // quando vem do cadastro de cliente vem com numero apenas de 1 cliente
            {
                if (query.Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " NFCOMPRA.IDFORNECEDOR = " + idCliente + " ";
            }
            if (daDataDe.ToString() != "")
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " and ";
                }
                query = query + " NFCOMPRA.DATA >= " + clsParser.SqlDateTimeFormat(daDataDe.ToString("dd/MM/yyyy") + " 00:00", true);
                query = query + " and ";
                query = query + " NFCOMPRA.DATA <= " + clsParser.SqlDateTimeFormat(daDataAte.ToString("dd/MM/yyyy") + " 23:59", true);
            }
            if (query.ToString().Length > 0)
            {
                sql = sql + " where " + query + " ";
            }
            if (query.ToString().Length > 0)
            {
                sql = sql + " order by NFCOMPRA.DATA, PECAS.NOME ";
            }



            SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
            //scd.Parameters.Add("@id", SqlDbType.Int).Value = idPagar;
            dtFichaComercialCompras = new DataTable();
            sda.Fill(dtFichaComercialCompras);
            return dtFichaComercialCompras;
        }

        private static GridColuna[] dtFichaComercialComprasColunas = new GridColuna[]
        {
            new GridColuna("Fil", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Tab", "TABELA", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nro Ped", "NRONF", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Data", "DATAEMISSAO", 65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Cliente", "CLIENTE", 0, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tipo", "TIPOPRODUTO", 0, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("CFO", "CFOP_CFOP", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qtde", "QTDE", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNIDADE_CODIGO", 25, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Codigo", "PECAS_CODIGO", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "PECAS_NOME", 160, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Preço Unit", "PRECO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total Merc", "TOTALMERCADO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total Nota", "TOTALNOTA", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Peso Un", "PESO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Peso T.", "PESOTOTAL", 70, true, DataGridViewContentAlignment.MiddleRight),
        };

        // segundo método
        public static void GridFichaComercialComprasMonta(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtFichaComercialComprasColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            dgv.Columns["DATAEMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["PRECO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALMERCADO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
            dgv.Columns["PESO"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PESOTOTAL"].DefaultCellStyle.Format = "N3";
        }

        public static String Digito_Mod11_Brasil(String NroDigito)
        {   // Calcular o digito do MOD11

            Int32[] numeros = new int[11];
            // for (int i = 0; i < 11; i++)
            for (int i = 10; i >= 0; i--)
            {
                numeros[i] = int.Parse(NroDigito[i].ToString());
            }
            Int32 soma = 0;
            Int32 nIntMultiplicador = 10;
            for (int i = 10; i >= 0; i--)
            {
                if (nIntMultiplicador > 2)
                {
                    nIntMultiplicador -= 1;
                }
                else
                {
                    nIntMultiplicador = 9;
                }
                soma += nIntMultiplicador * numeros[i];
            }

            Int32 resultado = soma % 11;
            if (resultado >= 10)
            {
                NroDigito = "X";
            }
            else
            {
                NroDigito = resultado.ToString();
            }
            return NroDigito;
        }

        public static String Digito_Mod11_Santander(String NroDigito)
        {   // Calcular o digito do MOD11

            Int32[] numeros = new int[12];
            // for (int i = 0; i < 11; i++)
            for (int i = 11; i >= 0; i--)
            {
                numeros[i] = int.Parse(NroDigito[i].ToString());
            }
            Int32 soma = 0;
            Int32 nIntMultiplicador = 1;
            for (int i = 11; i >= 0; i--)
            {
                if (nIntMultiplicador < 9)
                {
                    nIntMultiplicador += 1;
                }
                else
                {
                    nIntMultiplicador = 2;
                }
                soma += nIntMultiplicador * numeros[i];
            }

            Int32 resultado = soma % 11;

            if (resultado == 10)
            {
                NroDigito = "1";
            }
            else if (resultado == 0 || resultado == 1)
            {
                NroDigito = "0";
            }
            else
            {
                resultado = 11 - resultado;
                NroDigito = resultado.ToString();
            }

            return NroDigito;
        }


        public static String Digito_Mod11BarrasSantander(String NroDigito)
        {   // Calcular o digito do MOD11
            
            Int32[] numeros = new int[43];
            // for (int i = 0; i < 11; i++)
            for (int i = 42; i >= 0; i--)
            {
                //if (i == 4)
                //{
                //    // Não pode contar a 5a casa
                //    String Xteste = NroDigito[i].ToString();
                //}
                //else
                //{
                    numeros[i] = int.Parse(NroDigito[i].ToString());
                //}
            }
            Int32 soma = 0;
            Int32 nIntMultiplicador = 1;
            for (int i = 42; i >= 0; i--)
            {
                if (nIntMultiplicador < 9)
                {
                    nIntMultiplicador += 1;
                }
                else
                {
                    nIntMultiplicador = 2;
                }
                soma += nIntMultiplicador * numeros[i];
            }

            Int32 resultado = (soma * 10) % 11;

            if (resultado == 0 || resultado == 1 || resultado == 10)
            {
                NroDigito = "1";
            }
            else
            {
                NroDigito = resultado.ToString();
            }
            return NroDigito;
        }
        public static String Digito_Mod10_Itau(String NroDigito)
        {   // Calcular o digito do MOD10

            Int32[] numeros = new int[NroDigito.Length];

            for (int i = NroDigito.Length - 1; i >= 0; i--)
            {
                numeros[i] = int.Parse(NroDigito[i].ToString());
            }

            Int32 soma = 0;
            Int32 soma1 = 0;
            Int32 nIntMultiplicador = 1;
            for (int i = NroDigito.Length - 1; i >= 0; i--)
            {
                if (nIntMultiplicador == 1)
                {
                    nIntMultiplicador = 2;
                }
                else
                {
                    nIntMultiplicador = 1;
                }

                soma1 = nIntMultiplicador * numeros[i];

                if (soma1 > 9)
                {
                    soma1 = Int32.Parse(soma1.ToString().Substring(0, 1)) +
                        Int32.Parse(soma1.ToString().Substring(1, 1));
                }

                soma += soma1;
            }

            Int32 resultado = (soma % 10);

            if (resultado != 0)
            {
                return NroDigito = (10 - resultado).ToString();
            }
            else
            {
                NroDigito = "0".ToString();
            }
            /////
/*                          ' codigo composto corretamente
                          nSoma = 0
                          For I = 1 To 20
                              Select Case I
                                     Case 1, 3, 5, 7, 9, 11, 13, 15, 17, 19
                                          nProduto = Val(Mid(Nosso, I, 1)) * 1
                                          If nProduto > 9 Then
                                             nSoma = nSoma + Val(Left(nProduto, 1))
                                             nSoma = nSoma + Val(Mid(nProduto, 2, 1))
                                          Else
                                             nSoma = nSoma + (Val(nProduto) * 1)
                                          End If
                                     Case 2, 4, 6, 8, 10, 12, 14, 16, 18, 20
                                          nProduto = Val(Mid(Nosso, I, 1)) * 2
                                          If nProduto > 9 Then
                                             nSoma = nSoma + Val(Left(nProduto, 1))
                                             nSoma = nSoma + Val(Mid(nProduto, 2, 1))
                                          Else
                                             nSoma = nSoma + Val(nProduto)
                                          End If
                              End Select
                                  
                          Next I
                          nSoma = (nSoma / 10) - (Int(nSoma / 10))
                          nSoma = (10 - (nSoma * 10))
                          cDv = CStr(nSoma)
            */

            /////
            return NroDigito;
        }

        public static String Digito_Mod10Santander(String NroDigito)
        {   // Calcular o digito do MOD11

            Int32[] numeros = new int[NroDigito.Length];

            for (int i = NroDigito.Length - 1; i >= 0; i--)
            {
                numeros[i] = int.Parse(NroDigito[i].ToString());
            }

            Int32 soma = 0;
            Int32 soma1 = 0;
            Int32 nIntMultiplicador = 1;
            for (int i = NroDigito.Length - 1; i >= 0; i--)
            {
                if (nIntMultiplicador == 1)
                {
                    nIntMultiplicador = 2;
                }
                else
                {
                    nIntMultiplicador = 1;
                }

                soma1 = nIntMultiplicador * numeros[i];

                if (soma1 > 9)
                {
                    soma1 = Int32.Parse(soma1.ToString().Substring(0, 1)) +
                        Int32.Parse(soma1.ToString().Substring(1, 1));
                }

                soma += soma1;
            }

            Int32 resultado = (soma % 10);

            if (resultado != 0)
            {
                return NroDigito = (10 - resultado).ToString();
            }
            else
            {
                NroDigito = "0".ToString();
            }
            return NroDigito;
        }
        public static String Digito_Mod11Itau(String NroDigito)
        {   // Calcular o digito do MOD11

            Int32[] numeros = new int[43];
            // for (int i = 0; i < 11; i++)
            for (int i = 42; i >= 0; i--)
            {
                //if (i == 4)
                //{
                //    // Não pode contar a 5a casa
                //    String Xteste = NroDigito[i].ToString();
                //}
                //else
                //{
                numeros[i] = int.Parse(NroDigito[i].ToString());
                //}
            }
            Int32 soma = 0;
            Int32 nIntMultiplicador = 1;
            for (int i = 42; i >= 0; i--)
            {
                if (nIntMultiplicador < 9)
                {
                    nIntMultiplicador += 1;
                }
                else
                {
                    nIntMultiplicador = 2;
                }
                soma += nIntMultiplicador * numeros[i];
            }

            Int32 resultado = (soma * 10) % 11;

            if (resultado == 0 || resultado == 1 || resultado == 10)
            {
                NroDigito = "1";
            }
            else
            {
                NroDigito = resultado.ToString();
            }
            return NroDigito;
        }

    }
}
