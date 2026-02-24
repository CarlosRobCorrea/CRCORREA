using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsPagarBLL : SQLFactory<clsPagarInfo>
    {
        Boolean Ok_ContinuaProcesso = true;
        String query;
        DataTable dt;
        SqlDataAdapter sda;

        clsDocFiscalInfo DocfiscalInfo;
        clsDocFiscalBLL DocfiscaBLL;

        clsPagarBLL PagarBLL;
        clsPagarInfo PagarInfo;

        Int32 pagar_id;

        Int32 documento_numero;
        DateTime documento_dataemissao;
        String documento_emitente;
        Int32 documento_idfornecedor;
        String documento_observa;
        Int32 documento_idhistorico;
        Int32 documento_idcentrocusto;
        Int32 documento_idcontacontabil;

        Decimal valoratual;
        Decimal valorjuros;
        Decimal valorjuros_mes;

        Int32 empresa_id;
        Int32 empresa_filial;


        /// <summary>
        /// Sicroniza todos os lançamentos ligados a um determinado documento.
        /// </summary>
        /// <param name="idgeradordocumento">Identificador do documento que gerou os lançamentos.</param>
        /// <param name="iddocumento">Identificador do tipo do documento.</param>
        /// <param name="cnn">Conexão com o banco de dados.</param>
        /// <returns></returns>
        public void SicronizarPorDocumento(int idgeradordocumento, int iddocumento, string cnn)
        {
            Ok_ContinuaProcesso = true;

            DocfiscaBLL = new clsDocFiscalBLL();
            DocfiscalInfo = DocfiscaBLL.Carregar(iddocumento, cnn);

            documento_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from nfcompra where id=" + idgeradordocumento));


            if (DocfiscalInfo.cognome == null ||
                DocfiscalInfo.cognome.Trim() == "")
            {
                //throw new Exception("Documento Fiscal inválido para a sicronização do documento.");
                MessageBox.Show("Documento Fiscal inválido para a sicronização do documento. Nro = " + documento_numero + Environment.NewLine + 
                    " Este tipo de documento " + DocfiscalInfo.cognome + " - Comunique Aplisoft");

                Ok_ContinuaProcesso = false ;
            }

            if (DocfiscalInfo.cognome.IndexOf("NFE") != -1 || DocfiscalInfo.cognome.IndexOf("DES") != -1
                || DocfiscalInfo.cognome.IndexOf("CON") != -1 || DocfiscalInfo.cognome.IndexOf("DAR") != -1)
            {
/*                query = "SELECT * FROM NFCOMPRAPAGAR " +
                        "WHERE IDNOTA = @IDGERADORDOCUMENTO AND " +
                        "(SELECT COUNT(*) FROM NFCOMPRAPAGAR " +
                        "WHERE IDNOTA = @IDGERADORDOCUMENTO AND PAGOU='S') = 0 "; */
                // mudei a query em 06/01/2013 - pois se tinha duplicata paga e não paga ele não transfere o que não foi paga ainda
                query = "SELECT * FROM NFCOMPRAPAGAR WHERE IDNOTA = @IDGERADORDOCUMENTO AND " +
                        "(SELECT COUNT(*) FROM NFCOMPRAPAGAR WHERE IDNOTA =  @IDGERADORDOCUMENTO AND PAGOU<>'S') > 0 ";

                empresa_filial = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select filial from nfcompra where id=" + idgeradordocumento));
                documento_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from nfcompra where id=" + idgeradordocumento));
                documento_dataemissao = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(cnn, "select data from nfcompra where id=" + idgeradordocumento));
                documento_emitente = Procedure.PesquisaoPrimeiro(cnn, "select emitente from nfcompra where id=" + idgeradordocumento);
                documento_idfornecedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idfornecedor from nfcompra where id=" + idgeradordocumento));
                documento_observa = Procedure.PesquisaoPrimeiro(cnn, "select observa from nfcompra where id=" + idgeradordocumento);
                documento_idhistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idhistorico from nfcompra1 where numero=" + idgeradordocumento));
                documento_idcentrocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcentrocusto from nfcompra1 where numero=" + idgeradordocumento));
                documento_idcontacontabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcodigoctabil from nfcompra1 where numero=" + idgeradordocumento));
            }
            else if (DocfiscalInfo.cognome.IndexOf("PCO") != -1)
            {
                query = "SELECT " +
                        "* " +
                    "FROM " +
                        "COMPRASPAGAR " +
                    "WHERE " +
                        "IDCOMPRA = @IDGERADORDOCUMENTO AND " +
                        "(SELECT " +
                            "COUNT(*) " +
                        "FROM " +
                            "COMPRASPAGAR " +
                        "WHERE " +
                            "IDCOMPRA = @IDGERADORDOCUMENTO AND " +
                            "PAGOU='S') = 0 ";

                empresa_filial = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select filial from compras where id=" + idgeradordocumento));
                documento_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from compras where id=" + idgeradordocumento));
                documento_dataemissao = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(cnn, "select data from compras where id=" + idgeradordocumento));
                documento_emitente = Procedure.PesquisaoPrimeiro(cnn, "select usuario from usuario where id=(select top 1 idemitente from compras where id=" + idgeradordocumento + ")");
                documento_idfornecedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idfornecedor from compras where id=" + idgeradordocumento));
                documento_observa = "";
                documento_idhistorico = 0;
                documento_idcentrocusto = 0;
                documento_idcontacontabil = 0;
            }
            else
            {
                Ok_ContinuaProcesso = false ;
            }
            if (Ok_ContinuaProcesso == true)
            {
                if (documento_idhistorico == 0)
                {
                    documento_idhistorico = clsInfo.zhistoricos;
                }

                if (documento_idcentrocusto == 0)
                {
                    documento_idcentrocusto = clsInfo.zcentrocustos;
                }

                if (documento_idcontacontabil == 0)
                {
                    documento_idcontacontabil = clsInfo.zcontacontabil;
                }

                PagarBLL = new clsPagarBLL();

                empresa_id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from empresa where codigo=" + empresa_filial));

                //valorjuros_mes = clsParser.DecimalParse(Procedures.PesquisaoPrimeiro(cnn, "SELECT JUROSMES FROM EMPRESAGERE WHERE EMPRESA=" + empresa_id));
                valorjuros_mes = 0;
                dt = new DataTable();
                sda = new SqlDataAdapter(query, cnn);
                sda.SelectCommand.Parameters.Add("@IDGERADORDOCUMENTO", SqlDbType.Int).Value = idgeradordocumento;
                sda.Fill(dt);

                foreach (DataRow linha in dt.Rows)
                {
                    if (linha["PAGOU"].ToString() != "S")
                    {
                        valoratual = Decimal.Round(clsParser.DecimalParse(linha["VALOR"].ToString()), 2);
                        if (valoratual > 0 && valorjuros_mes > 0)
                        {
                            valorjuros = (valoratual * (valorjuros_mes / 100)) / 30;
                        }
                        else
                        {
                            valorjuros = 0;
                        }

                        // Verifica se existe no Contas a Receber
                        PagarInfo = new clsPagarInfo();

                        pagar_id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "SELECT ID FROM PAGAR WHERE IDDOCUMENTO=" + iddocumento + " AND IDPAGARNFE=" + linha["ID"]));

                        if (pagar_id > 0)
                        {
                            PagarInfo = PagarBLL.Carregar(pagar_id, cnn);
                        }
                        else
                        {
                            // Se não existir, tentar encontrar de outra forma
                            if (pagar_id > 0)
                            {
                                PagarInfo = PagarBLL.Carregar(pagar_id, cnn);
                            }
                            else
                            {
                                PagarInfo = new clsPagarInfo();

                                PagarInfo.boleto = "";
                                //PagarInfo.contrato = "";
                                PagarInfo.chegou = "N";
                                PagarInfo.datalanca = DateTime.Now;
                                PagarInfo.despesapublica = "N";
                                //PagarInfo.idcodigo01 = 0;
                                //PagarInfo.idcodigo02 = 0;
                                //PagarInfo.idcodigo03 = 0;
                                //PagarInfo.idcodigo04 = 0;
                                //PagarInfo.iddespesa = 0;
                                PagarInfo.idsitbanco = clsInfo.zsituacaotitulo;
                                //PagarInfo.idvendedor = clsInfo.zempresaclienteid;
                                PagarInfo.imprimir = "";
                                PagarInfo.setor = "N";
                                //PagarInfo.transfebanco = "N";
                                //PagarInfo.transfenumero = 0;
                                PagarInfo.valorbaixando = 0;
                                PagarInfo.valordesconto = 0;
                                PagarInfo.valordevolvido = 0;
                                PagarInfo.valorliquido = 0;
                                PagarInfo.valormulta = 0;
                                PagarInfo.valorpago = 0;
                                PagarInfo.observa = "";
                                PagarInfo.atevencimento = "S";
                                PagarInfo.baixa = "N";
                            }
                        }

                        PagarInfo.idbanco = clsInfo.zbanco;
                        if (clsInfo.zempresacliente_cognome.IndexOf("COATING") != -1)
                        {
                            PagarInfo.idbancoint = 2;
                        }
                        else
                        {
                            PagarInfo.idbancoint = clsInfo.zbancoint;
                        }
                        PagarInfo.idcentrocusto = documento_idcentrocusto;
                        PagarInfo.idhistorico = documento_idhistorico;
                        PagarInfo.idcodigoctabil = documento_idcontacontabil;
                        PagarInfo.idfornecedor = documento_idfornecedor;
                        PagarInfo.iddocumento = iddocumento;
                        PagarInfo.idformapagto = clsParser.Int32Parse(linha["idtipopaga"].ToString());
                        PagarInfo.idnotafiscal = idgeradordocumento;
                        PagarInfo.idpagarnfe = clsParser.Int32Parse(linha["ID"].ToString());
                        PagarInfo.idsitbanco = clsInfo.zsituacaotitulo;
                        PagarInfo.boletonro = clsParser.DecimalParse(linha["BOLETONRO"].ToString());
                        PagarInfo.duplicata = documento_numero;
                        PagarInfo.dv = linha["DV"].ToString();
                        PagarInfo.emissao = documento_dataemissao;
                        PagarInfo.emitente = documento_emitente;
                        PagarInfo.filial = empresa_filial;
                        PagarInfo.posicao = clsParser.Int32Parse(linha["POSICAO"].ToString());
                        PagarInfo.posicaofim = clsParser.Int32Parse(linha["POSICAOFIM"].ToString());
                        PagarInfo.valor = clsParser.DecimalParse(linha["VALOR"].ToString());
                        //PagarInfo.valorcomissao = clsParser.DecimalParse(linha["VALORCOMISSAO"].ToString());
                        //PagarInfo.valorcomissaoger = clsParser.DecimalParse(linha["VALORCOMISSAOGER"].ToString());
                        PagarInfo.valorjuros = valorjuros;
                        PagarInfo.valorliquido = valoratual;
                        PagarInfo.vencimento = clsParser.DateTimeParse(linha["DATA"].ToString());
                        PagarInfo.vencimentoprev = clsParser.DateTimeParse(linha["DATA"].ToString());

                        if (PagarInfo.chegou == null) PagarInfo.chegou = "N";
                        if (PagarInfo.despesapublica == null) PagarInfo.despesapublica = "N";

                        if (PagarInfo.observa == "")
                        {
                            PagarInfo.observa = documento_observa;
                        }

                        if (PagarInfo.id == 0)
                        {
                            PagarInfo.id = PagarBLL.Incluir(PagarInfo, cnn);
                        }
                        else
                        {
                            PagarBLL.Alterar(PagarInfo, cnn);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Documento Fiscal inválido para a sicronização do documento. Nro = " + documento_numero + Environment.NewLine +
                    " Este tipo de documento " + DocfiscalInfo.cognome + " - Comunique Aplisoft");
            }
        }

        /// <summary>
        /// Exclui todos os lançamentos ligados a um determinado documento (Nota Fiscal, Contrato, Ordem de Serviço, etc...)
        /// </summary>
        /// <param name="idgeradordocumento">Identificador do documento que gerou os lançamentos.</param>
        /// <param name="iddocumento">Identificador do tipo do documento ('NFV', 'CTO', 'OS', etc...)</param>
        /// <param name="cnn">Conexão com o banco de dados.</param>
        /// <returns></returns>
        public bool ExcluirPorDocumento(int idgeradordocumento, int iddocumento, string cnn)
        {
            Boolean result = true;

            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID " +
                    "FROM " +
                        "PAGAR " +
                    "WHERE " +
                        "IDNOTAFISCAL = @IDNOTAFISCAL AND " +
                        "IDDOCUMENTO = @IDDOCUMENTO ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDNOTAFISCAL", SqlDbType.Int).Value = idgeradordocumento;
            sda.SelectCommand.Parameters.Add("@IDDOCUMENTO", SqlDbType.Int).Value = iddocumento;
            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (result == true)
                {
                    result = Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
                else
                {
                    Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
            }

            return result;
        }

        public bool ExcluirPorLancamento(int idpagarnfe, int iddocumento, string cnn)
        {
            Boolean result = true;

            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID " +
                    "FROM " +
                        "PAGAR " +
                    "WHERE " +
                        "IDPAGARNFE = @IDPAGARNFE AND " +
                        "IDDOCUMENTO = @IDDOCUMENTO ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDPAGARNFE", SqlDbType.Int).Value = idpagarnfe;
            sda.SelectCommand.Parameters.Add("@IDDOCUMENTO", SqlDbType.Int).Value = iddocumento;
            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (result == true)
                {
                    result = Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
                else
                {
                    Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
            }

            return result;
        }

        public static DataTable GridDados(Int32 Filial, String DataAtual, String TipoData, String TipoLista, String Pendencia)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PAGAR.ID AS [ID], PAGAR.FILIAL, PAGAR.DUPLICATA, PAGAR.POSICAO , PAGAR.POSICAOFIM, PAGAR.EMISSAO, " +
                    "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, PAGAR.VENCIMENTO,PAGAR.VENCIMENTOPREV, " +
                    "(PAGAR.VALORLIQUIDO + PAGAR.VALORBAIXANDO) - (PAGAR.VALORPAGO + PAGAR.VALORDEVOLVIDO) AS [VALORPAGAR], " +
                    "SITUACAOTIPOTITULO.CODIGO AS [TITULO], PAGAR.OBSERVA AS [OBSERVA], PAGAR.CHEGOU, " +
                    "TAB_BANCOS.COGNOME AS [BANCO], PAGAR.IDNOTAFISCAL, PAGAR.IDPAGARNFE " +
                    "FROM PAGAR " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID = PAGAR.IDFORNECEDOR " +
                    "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAR.IDDOCUMENTO " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAR.IDFORMAPAGTO " +
                    "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID = PAGAR.IDBANCO ";
            //FILIAL
            if (Filial > 0)
            { // filial especifica
                filtro = "PAGAR.FILIAL = @FILIAL ";
            }
            if (TipoData == "VENC")
            { 
                if (TipoLista == "F") // Futuro
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "A")  // Atraso
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "PAGAR.VENCIMENTO < " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "D")  // Apenas 1 um dia
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                    filtro = filtro + "AND PAGAR.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(DataAtual + " 23:59", true);
                }

            }
            else
            { // DT PREVISTA
                if (TipoLista == "F")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro +  " AND ";
                    }
                    filtro = filtro + " PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "A")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro +  " PAGAR.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "D")  // Apenas 1 um dia
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                    filtro = filtro + "AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(DataAtual + " 23:59", true);
                }

            }
            if (Pendencia == "S")
            {
                Int32 idBancoInt = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where CONTA = " + 999, "0"));
                filtro = filtro + "AND IDBANCOINT != " + idBancoInt;
            }
            else
            {
                // não declarar
            }

            if (filtro.Length > 2)
            {
                filtro = " WHERE " + filtro;
            }
            if (TipoData == "VENC")
            {
                filtro = filtro + " ORDER BY PAGAR.VENCIMENTO, PAGAR.DUPLICATA ";
            }
            else
            {
                filtro = filtro + " ORDER BY PAGAR.VENCIMENTOPREV, PAGAR.DUPLICATA ";
            }


            
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            //sda.SelectCommand.Parameters.Add("@DataDe", SqlDbType.DateTime).Value = DataAtual + " 00:00:00";
            sda.SelectCommand.Parameters.Add("@Filial", SqlDbType.Int).Value = Filial;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("idpagarnfe", "IDpagarnfe", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("F", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nro Dup", "DUPLICATA", 65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("P.", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("P.Fim", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissão", "EMISSAO", 65, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc", "DOCUMENTO", 35, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cliente", "COGNOME", 150, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Vencimento", "VENCIMENTO", 65, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Venc. Prev  ", "VENCIMENTOPREV", 65, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Valor Titulo", "VALORPAGAR", 100, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tipo", "TITULO", 35, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Observa", "OBSERVA", 200, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Chegou", "CHEGOU", 45, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Banco", "BANCO", 100, true, DataGridViewContentAlignment.MiddleLeft),
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "duplicata");
            dgv.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VENCIMENTOPREV"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VALORPAGAR"].DefaultCellStyle.Format = "N2";
        }

        public static DataTable GridDadosTodosCampos(Int32 Filial, String DataAtual, String TipoData, String TipoLista)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PAGAR.ID AS [ID], PAGAR.FILIAL, PAGAR.DUPLICATA, PAGAR.POSICAO , PAGAR.POSICAOFIM, PAGAR.EMISSAO, " +
                    "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, PAGAR.VENCIMENTO,PAGAR.VENCIMENTOPREV, " +
                    "(PAGAR.VALORLIQUIDO + PAGAR.VALORBAIXANDO)  - (PAGAR.VALORPAGO + PAGAR.VALORDEVOLVIDO) AS [VALORPAGAR], " +
                    "SITUACAOTIPOTITULO.CODIGO AS [TITULO], PAGAR.OBSERVA AS [OBSERVA], PAGAR.CHEGOU, " +
                    "TAB_BANCOS.COGNOME AS [BANCO], PAGAR.IDNOTAFISCAL, " +
                    "PAGAR.IDDOCUMENTO ,PAGAR.SETOR ,PAGAR.IDFORNECEDOR ,PAGAR.DATALANCA ,PAGAR.EMITENTE ,PAGAR.IDHISTORICO , " +
                    "PAGAR.IDCENTROCUSTO ,PAGAR.IDCODIGOCTABIL ,PAGAR.IDPAGARNFE ,PAGAR.IDFORMAPAGTO , " +
                    "PAGAR.IDBANCO ,PAGAR.IDBANCOINT ,PAGAR.DESPESAPUBLICA ,PAGAR.BOLETO ,PAGAR.BOLETONRO ,PAGAR.DV , " +
                    "PAGAR.BAIXA ,PAGAR.VALOR ,PAGAR.VALORDESCONTO ,PAGAR.ATEVENCIMENTO ,PAGAR.VALORLIQUIDO ,PAGAR.VALORJUROS , " +
                    "PAGAR.VALORMULTA ,PAGAR.IDSITBANCO ,PAGAR.IMPRIMIR ,PAGAR.VALORPAGO ,PAGAR.VALORBAIXANDO ,PAGAR.VALORDEVOLVIDO , " +
                    "PAGAR.IDVENDEDOR ,PAGAR.VALORCOMISSAO ,PAGAR.IDSUPERVISOR ,PAGAR.VALORCOMISSAOSUP ,PAGAR.IDCOORDENADOR , " +
                    "PAGAR.VALORCOMISSAOGER  " +
                    "FROM PAGAR " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID = PAGAR.IDFORNECEDOR " +
                    "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAR.IDDOCUMENTO " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAR.IDFORMAPAGTO " +
                    "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID = PAGAR.IDBANCO ";
            //FILIAL
            if (Filial > 0)
            { // filial especifica
                filtro = "PAGAR.FILIAL = @FILIAL ";
            }
            if (TipoData == "VENC")
            {
                if (TipoLista == "F") // Futuro
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "A")  // Atraso
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "PAGAR.VENCIMENTO < " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "D")  // Apenas 1 um dia
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "PAGAR.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                    filtro = filtro + "AND PAGAR.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(DataAtual + " 23:59", true);
                }

            }
            else
            { // DT PREVISTA
                if (TipoLista == "F")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + " PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "A")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + " PAGAR.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "D")  // Apenas 1 um dia
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "PAGAR.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                    filtro = filtro + "AND PAGAR.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(DataAtual + " 23:59", true);
                }

            }
            if (filtro.Length > 2)
            {
                filtro = " WHERE " + filtro;
            }
            if (TipoData == "VENC")
            {
                filtro = filtro + " ORDER BY PAGAR.VENCIMENTO, PAGAR.DUPLICATA ";
            }
            else
            {
                filtro = filtro + " ORDER BY PAGAR.VENCIMENTOPREV, PAGAR.DUPLICATA ";
            }
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            //sda.SelectCommand.Parameters.Add("@DataDe", SqlDbType.DateTime).Value = DataAtual + " 00:00:00";
            sda.SelectCommand.Parameters.Add("@Filial", SqlDbType.Int).Value = Filial;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public override bool Excluir(int id, string cnn)
        {
            Boolean result = false;
            //tem de excluir o pagarobserva antes
            DataTable dtObs = Procedure.RetornaDataTable(clsInfo.conexaosqldados,
                "select id from pagarobserva where idduplicata = " + id);
            if (dtObs.Rows.Count>0)
            {
                 clsPagarObservaBLL clsPagarObservaBLL = new clsPagarObservaBLL();
                 foreach (DataRow row in dtObs.Rows)
                 {
                     clsPagarObservaBLL.Excluir(clsParser.Int32Parse(row["id"].ToString()),
                         clsInfo.conexaosqldados);
                 }
            }
           //agora sim exclui  pagar
            result = base.Excluir(id, cnn);         

            return result;
        }

    }
}
