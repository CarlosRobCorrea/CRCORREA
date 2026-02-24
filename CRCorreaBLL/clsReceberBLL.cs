using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{ 
    public class clsReceberBLL : SQLFactory<clsReceberInfo>
    {
        /// <summary>
        /// Sicroniza todos os lançamentos ligados a um determinado documento (Nota Fiscal, Contrato, Ordem de Serviço, etc...)
        /// </summary>
        /// <param name="idgeradordocumento">Identificador do documento que gerou os lançamentos.</param>
        /// <param name="iddocumento">Identificador do tipo do documento ('NFV', 'CTO', 'OS', etc...)</param>
        /// <param name="cnn">Conexão com o banco de dados.</param>
        /// <returns></returns>
        public void SicronizarPorDocumento(int idgeradordocumento, int iddocumento, string cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            clsDocFiscalInfo DocfiscalInfo;
            clsDocFiscalBLL DocfiscaBLL;

            clsReceberBLL ReceberBLL;
            clsReceberInfo ReceberInfo;

            Int32 receber_id;
            
            Int32 documento_numero;
            DateTime documento_dataemissao;
            String documento_emitente;
            Int32 documento_idcliente;

            Decimal valoratual;
            Decimal valorjuros;
            Decimal valorjuros_mes;

            Int32 empresa_id;
            Int32 empresa_filial;
            Int32 empresa_idgeradordocumento;

            DocfiscaBLL = new clsDocFiscalBLL();
            DocfiscalInfo = DocfiscaBLL.Carregar(iddocumento, cnn);

            if (DocfiscalInfo.cognome == null ||
                DocfiscalInfo.cognome.Trim() == "")
            {
                throw new Exception("Documento Fiscal inválido para a sicronização do documento.");
            }

            if (DocfiscalInfo.cognome.IndexOf("NFV") != -1)
            {
                query = "SELECT " +
                        "* " +
                    "FROM " +
                        "NFVENDARECEBER " +
                    "WHERE " +
                        "IDNOTA = @IDGERADORDOCUMENTO AND " +
                        "(SELECT " +
                            "COUNT(*) " +
                        "FROM " +
                            "NFVENDARECEBER " +
                        "WHERE " +
                            "IDNOTA = @IDGERADORDOCUMENTO AND " +
                            "PAGOU='S') = 0 ";

                empresa_filial = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select filial from nfvenda where id=" + idgeradordocumento));
                documento_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from nfvenda where id=" + idgeradordocumento));
                documento_dataemissao = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(cnn, "select data from nfvenda where id=" + idgeradordocumento));
                documento_emitente = Procedure.PesquisaoPrimeiro(cnn, "select emitente from nfvenda where id=" + idgeradordocumento);
                documento_idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente from nfvenda where id=" + idgeradordocumento));
            }
            else if (DocfiscalInfo.cognome.IndexOf("CTO") != -1)
            {
                query = "SELECT " +
                        "* " +
                    "FROM " +
                        "CONTRATORECEBER " +
                    "WHERE " +
                        "IDCONTRATO = @IDGERADORDOCUMENTO AND " +
                        "(SELECT " +
                            "COUNT(*) " +
                        "FROM " +
                            "CONTRATORECEBER " +
                        "WHERE " +
                            "IDCONTRATO = @IDGERADORDOCUMENTO AND " +
                            "PAGOU='S') = 0 ";

                empresa_filial = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select filial from contrato where id=" + idgeradordocumento));
                documento_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from contrato where id=" + idgeradordocumento));
                documento_dataemissao = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(cnn, "select data from contrato where id=" + idgeradordocumento));
                documento_emitente = Procedure.PesquisaoPrimeiro(cnn, "select usuario from usuario where id=(select top 1 idemitente from contrato where id=" + idgeradordocumento + ")");
                documento_idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente from contrato where id=" + idgeradordocumento));
            }
            else if (DocfiscalInfo.cognome.IndexOf("OS") != -1)
            {   // ordens de serviço do transveiculos [OST]
                query = "SELECT " +
                        "* " +
                    "FROM " +
                        "ORDEMSERVICORECEBER " +
                    "WHERE " +
                        "IDORDEMSERVICO= @IDGERADORDOCUMENTO AND " +
                        "(SELECT " +
                            "COUNT(*) " +
                        "FROM " +
                            "ORDEMSERVICORECEBER " +
                        "WHERE " +
                            "IDORDEMSERVICO = @IDGERADORDOCUMENTO AND " +
                            "PAGOU='S') = 0 ";

                empresa_filial = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select filial from ordemservico where id=" + idgeradordocumento));
                documento_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from ordemservico where id=" + idgeradordocumento));
                documento_dataemissao = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(cnn, "select data from ordemservico where id=" + idgeradordocumento));
                documento_emitente = Procedure.PesquisaoPrimeiro(cnn, "select usuario.usuario as emitente from ordemservico inner join usuario " +
                        " on ordemservico.idemitente = usuario.id where ordemservico.id =" + idgeradordocumento);
                documento_idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente from ordemservico where id=" + idgeradordocumento));
            }
            else if (DocfiscalInfo.cognome.IndexOf("CON") != -1)
            {
                query = "SELECT " +
                        "* " +
                    "FROM " +
                        "CONHECIMENTORECEBER " +
                    "WHERE " +
                        "IDCONHECIMENTO = @IDGERADORDOCUMENTO AND " +
                        "(SELECT " +
                            "COUNT(*) " +
                        "FROM " +
                            "CONHECIMENTORECEBER " +
                        "WHERE " +
                            "IDCONHECIMENTO = @IDGERADORDOCUMENTO AND " +
                            "PAGOU='S') = 0 ";

                empresa_filial = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select filial from conhecimento where id=" + idgeradordocumento));
                documento_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from conhecimento where id=" + idgeradordocumento));
                documento_dataemissao = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(cnn, "select data from conhecimento where id=" + idgeradordocumento));
                documento_emitente = Procedure.PesquisaoPrimeiro(cnn, "select usuario.usuario as emitente from conhecimento inner join usuario " +
                        " on conhecimento.idemitente = usuario.id where conhecimento.id =" + idgeradordocumento);
                documento_idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idpagador from conhecimento where id=" + idgeradordocumento));
            }
            else if (DocfiscalInfo.cognome.IndexOf("PED") != -1)
            {
                query = "SELECT " +
                            "* " +
                        "FROM " +
                            "PEDIDORECEBER " +
                        "WHERE " +
                            "IDNOTA = @IDGERADORDOCUMENTO AND " +
                            "(SELECT " +
                                "COUNT(*) " +
                            "FROM " +
                                "PEDIDORECEBER " +
                            "WHERE " +
                                "IDNOTA = @IDGERADORDOCUMENTO AND " +
                                "PAGOU='S') = 0 ";

                empresa_filial = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select filial from pedido where id=" + idgeradordocumento));
                documento_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from pedido where id=" + idgeradordocumento));
                documento_dataemissao = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(cnn, "select data from pedido where id=" + idgeradordocumento));
                documento_emitente = Procedure.PesquisaoPrimeiro(cnn, "select emitente from pedido where id=" + idgeradordocumento);
                documento_idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente from pedido where id=" + idgeradordocumento));

                // Desativado em 13/05/2021 - passou a ter tabela
                // NESTE CASO NÃO TEM OUTRA TABELA A PARTE ELE PEGA O PROPRIO PEDIDO DE VENDA
                // DATA DE VENCIMENTO É A DATA DE EMISSAO + 1 DIA

                //query = "SELECT " +
                //        "*, PEDIDO.TOTALPEDIDO AS [VALOR], 7 AS [IDTIPOPAGA], 0 AS [BOLETONRO], 0 AS [DV], 1 AS [POSICAO], 1 AS [POSICAOFIM], " +
                //        " PEDIDO.COMISSAOREPRESENTANTE AS [VALORCOMISSAO], 0 AS [VALORCOMISSAOGER] " +
                //empresa_filial = 1; clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select filial from PEDIDO where id=" + idgeradordocumento));
                //documento_numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from PEDIDO where id=" + idgeradordocumento));
                //documento_dataemissao = clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(cnn, "select data from PEDIDO where id=" + idgeradordocumento));
                //documento_emitente = Procedure.PesquisaoPrimeiro(cnn, "select emitente from PEDIDO where id =" + idgeradordocumento);
                //documento_idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcliente pagador from PEDIDO where id=" + idgeradordocumento));
            }

            else
            {
                throw new Exception("Documento Fiscal inválido para a sicronização do documento." + DocfiscalInfo.cognome + " ! ");
            }

            ReceberBLL = new clsReceberBLL();

            empresa_id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from empresa where codigo=" + empresa_filial));
            empresa_idgeradordocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idgeradordocumento from empresa where id=" + empresa_id));

            valorjuros_mes = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "SELECT JUROSMES FROM EMPRESAGERE WHERE EMPRESA=" + empresa_id));

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDGERADORDOCUMENTO", SqlDbType.Int).Value = idgeradordocumento;
            sda.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                valoratual = Decimal.Round(clsParser.DecimalParse(linha["VALOR"].ToString()), 2);
                if (valoratual > 0)
                {
                    valorjuros = (valoratual * (valorjuros_mes / 100)) / 30;
                }
                else
                {
                    valorjuros = 0;
                }

                // Verifica se existe no Contas a Receber
                ReceberInfo = new clsReceberInfo();

                receber_id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "SELECT ID FROM RECEBER WHERE IDDOCUMENTO=" + iddocumento + " AND IDRECEBERNFV=" + linha["ID"]));

                if (receber_id > 0)
                {
                    ReceberInfo = ReceberBLL.Carregar(receber_id, cnn);
                }
                else
                {
                    // Se não existir, tentar encontrar de outra forma 
                    if (receber_id > 0)
                    {
                        ReceberInfo = ReceberBLL.Carregar(receber_id, cnn);
                    }
                    else
                    {
                        ReceberInfo = new clsReceberInfo();

                        ReceberInfo.boleto = "";
                        ReceberInfo.contrato = "";
                        ReceberInfo.chegou = "N";
                        ReceberInfo.datalanca = DateTime.Now;
                        ReceberInfo.despesapublica = "N";

                        ReceberInfo.idbanco = clsInfo.zbanco;
                        ReceberInfo.idbancoint = clsInfo.zbancoint;

                        if (clsInfo.zempresacliente_cognome.IndexOf("COATING") != -1 &&
                            clsInfo.zempresacliente_cognome.IndexOf("NEWCOATING") == -1
                            )
                        {
                            ReceberInfo.idbancoint = 2;
                            // Capturar  o Id dBanco dentro do Aplibanco
                            ReceberInfo.idbanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select IDBANCO from BANCOS where ID= " + ReceberInfo.idbancoint + "").ToString());
                            if (ReceberInfo.idbanco == 0)
                            {
                                ReceberInfo.idbanco = clsInfo.zbanco;
                            }
                        } 
                        else
                        {
                            ReceberInfo.idbancoint = clsInfo.zbancoint;
                        }
                        ReceberInfo.idcentrocusto = clsInfo.zcentrocustos;
                        ReceberInfo.idcodigo01 = 0;
                        ReceberInfo.idcodigo02 = 0;
                        ReceberInfo.idcodigo03 = 0;
                        ReceberInfo.idcodigo04 = 0;
                        ReceberInfo.idcodigoctabil = clsInfo.zcontacontabil;
                        ReceberInfo.idcoordenador = clsInfo.zempresaclienteid;
                        ReceberInfo.iddespesa = 0;
                        ReceberInfo.iddocumento = clsInfo.zdocumento;
                        ReceberInfo.idformapagto = clsInfo.zformapagto;
                        ReceberInfo.idhistorico = clsInfo.zhistoricos;
                        ReceberInfo.idnotafiscal = clsInfo.znfvenda;
                        ReceberInfo.idrecebernfv = 0;
                        ReceberInfo.idsupervisor = clsInfo.zempresaclienteid;
                        ReceberInfo.idsitbanco = clsInfo.zsituacaotitulo;
                        ReceberInfo.idvendedor = clsInfo.zempresaclienteid;
                        ReceberInfo.imprimir = "";
                        ReceberInfo.setor = "N";

                        ReceberInfo.transfebanco = "N";
                        ReceberInfo.transfenumero = 0;
                        ReceberInfo.valorbaixando = 0;
                        ReceberInfo.valordesconto = 0;
                        ReceberInfo.valordevolvido = 0;
                        ReceberInfo.valorliquido = 0;
                        ReceberInfo.valormulta = 0;
                        ReceberInfo.valorpago = 0;
                        ReceberInfo.observa = "";

                        ReceberInfo.atevencimento = "S";
                        ReceberInfo.baixa = "N";
                    }
                }

                ReceberInfo.idcliente = documento_idcliente;
                ReceberInfo.iddocumento = iddocumento;
                ReceberInfo.idformapagto = clsParser.Int32Parse(linha["idtipopaga"].ToString());
                ReceberInfo.idnotafiscal = idgeradordocumento;
                ReceberInfo.idrecebernfv = clsParser.Int32Parse(linha["ID"].ToString());
                ReceberInfo.idsitbanco = clsInfo.zsituacaotitulo;

                if (DocfiscalInfo.cognome.IndexOf("NFV") != -1)
                {
                    ReceberInfo.idvendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idvendedor from nfvenda where id=" + idgeradordocumento));
                    ReceberInfo.idsupervisor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idsupervisor from nfvenda where id=" + idgeradordocumento));
                    ReceberInfo.idcoordenador = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcoordenador from nfvenda where id=" + idgeradordocumento));
                    ReceberInfo.idhistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idhistorico from nfvenda1 where numero=" + idgeradordocumento));
                }
                else if (DocfiscalInfo.cognome.IndexOf("CTO") != -1)
                {
                    ReceberInfo.idvendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idvendedor from contrato where id=" + idgeradordocumento));
                }
                else if (DocfiscalInfo.cognome.IndexOf("PED") != -1)
                {
                    ReceberInfo.idvendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idvendedor from pedido where id=" + idgeradordocumento));
                }

                else
                {
                    ReceberInfo.idvendedor = clsInfo.zempresaclienteid;
                }
                
                ReceberInfo.boletonro = clsParser.DecimalParse(linha["BOLETONRO"].ToString());
                ReceberInfo.duplicata = documento_numero;
                ReceberInfo.dv = linha["DV"].ToString();
                ReceberInfo.emissao = documento_dataemissao;
                ReceberInfo.emitente = documento_emitente;
                ReceberInfo.filial = empresa_filial;
                ReceberInfo.posicao = clsParser.Int32Parse(linha["POSICAO"].ToString());
                ReceberInfo.posicaofim = clsParser.Int32Parse(linha["POSICAOFIM"].ToString());
                ReceberInfo.valor = clsParser.DecimalParse(linha["VALOR"].ToString());
                ReceberInfo.valorcomissao = clsParser.DecimalParse(linha["VALORCOMISSAO"].ToString());
                ReceberInfo.valorcomissaoger = clsParser.DecimalParse(linha["VALORCOMISSAOGER"].ToString());
                ReceberInfo.valorjuros = valorjuros;
                ReceberInfo.valorliquido = valoratual;
                ReceberInfo.vencimento = clsParser.DateTimeParse(linha["DATA"].ToString());
                ReceberInfo.vencimentoprev = clsParser.DateTimeParse(linha["DATA"].ToString());

                if (ReceberInfo.chegou == null) ReceberInfo.chegou = "N";
                if (ReceberInfo.despesapublica == null) ReceberInfo.despesapublica = "N";

                if (ReceberInfo.id == 0)
                {
                    ReceberInfo.id = ReceberBLL.Incluir(ReceberInfo, cnn);
                }
                else
                {
                    ReceberBLL.Alterar(ReceberInfo, cnn);
                }
            }
        }

        public void Reparar(Int32 iddocumento, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            clsDocFiscalInfo DocfiscalInfo;
            clsDocFiscalBLL DocfiscaBLL;

            DocfiscaBLL = new clsDocFiscalBLL();
            DocfiscalInfo = DocfiscaBLL.Carregar(iddocumento, cnn);

            if (DocfiscalInfo.cognome == null ||
                DocfiscalInfo.cognome.Trim() == "")
            {
                throw new Exception("Documento Fiscal inválido para a sicronização do documento.");
            }

            if (DocfiscalInfo.cognome.IndexOf("NFV") != -1)
            {
                query = "SELECT DISTINCT " +
                            "NFV.IDNOTA " +
                        "FROM " +
                            "NFVENDARECEBER AS NFV " +
                        "WHERE " +
                            "(SELECT " +
                                "COUNT(*) " +
                            "FROM " +
                                "RECEBER " +
                            "WHERE " +
                                "RECEBER.IDRECEBERNFV = NFV.ID AND " +
                                "RECEBER.IDDOCUMENTO=@IDDOCUMENTO) = 0 AND " +
                            "(SELECT " +
                                "COUNT(*) " +
                            "FROM " +
                                "NFVENDARECEBER " +
                            "WHERE " +
                                "NFVENDARECEBER.IDNOTA = NFV.IDNOTA AND " +
                                "NFVENDARECEBER.PAGOU='S') = 0 AND " +
                            "(SELECT TOP 1 YEAR(DATA) FROM NFVENDA WHERE ID=NFV.IDNOTA) >= 2011 ";
            }
            else if (DocfiscalInfo.cognome.IndexOf("CTO") != -1)
            {
                query = "SELECT DISTINCT " +
                            "CTR.IDCONTRATO " +
                        "FROM " +
                            "CONTRATORECEBER AS CTR " +
                        "WHERE " +
                            "(SELECT " +
                                "COUNT(*) " +
                            "FROM " +
                                "RECEBER " +
                            "WHERE " +
                                "RECEBER.IDRECEBERNFV = CTR.ID AND " +
                                "RECEBER.IDDOCUMENTO=@IDDOCUMENTO) = 0 AND " +
                            "(SELECT " +
                                "COUNT(*) " +
                            "FROM " +
                                "CONTRATORECEBER " +
                            "WHERE " +
                                "CONTRATORECEBER.IDCONTRATO = CTR.IDCONTRATO AND " +
                                "CONTRATORECEBER.PAGOU='S') = 0 AND " +
                            "(SELECT TOP 1 YEAR(DATA) FROM CONTRATO WHERE ID=CTR.IDCONTRATO) >= 2011 ";
            }
            else if (DocfiscalInfo.cognome.IndexOf("OS") != -1)
            {
                query = "SELECT DISTINCT " +
                            "OSR.IDOS " +
                        "FROM " +
                            "ORDEMSERVICORECEBER AS OSR " +
                        "WHERE " +
                            "(SELECT " +
                                "COUNT(*) " +
                            "FROM " +
                                "RECEBER " +
                            "WHERE " +
                                "RECEBER.IDRECEBERNFV = OSR.ID AND " +
                                "RECEBER.IDDOCUMENTO=@IDDOCUMENTO) = 0 AND " +
                            "(SELECT " +
                                "COUNT(*) " +
                            "FROM " +
                                "ORDEMSERVICORECEBER " +
                            "WHERE " +
                                "ORDEMSERVICORECEBER.IDOS = OSR.IDOS AND " +
                                "ORDEMSERVICORECEBER.PAGOU='S') = 0 AND " +
                            "(SELECT TOP 1 YEAR(DATA) FROM ORDEMSERVICO WHERE ID=OSR.IDOS) >= 2011 ";
            }
            else
            {
                throw new Exception("Documento Fiscal inválido para a sicronização do documento.");
            }

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDDOCUMENTO", SqlDbType.Int).Value = iddocumento;
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DialogResult drt = DialogResult.No;

                drt = MessageBox.Show("Existem " + dt.Rows.Count + " lançamentos no documento " + DocfiscalInfo.cognome + " sem registro equivalente no Contas a Receber. Deseja Sicronizar e eliminar essa divergência?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    foreach (DataRow linha in dt.Rows)
                    {
                        SicronizarPorDocumento(clsParser.Int32Parse(linha[0].ToString()), iddocumento, cnn);
                    }

                    MessageBox.Show("Sicronização terminada com sucesso.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Nenhuma divergência foi encontrada.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public override bool Excluir(int id, string cnn)
        {
            Boolean result = true;

            clsReceber01BLL Receber01BLL = new clsReceber01BLL();

            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID " +
                    "FROM " +
                        "RECEBER01 " +
                    "WHERE " +
                        "IDDUPLICATA = @IDDUPLICATA ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDDUPLICATA", SqlDbType.Int).Value = id;
            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (result == true)
                {
                    result = Receber01BLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
                else
                {
                    Receber01BLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
            }


            if (result == true)
            {
                result = base.Excluir(id, cnn);
            }
            else
            {
                base.Excluir(id, cnn);
            }

            return result;
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
                        "RECEBER " +
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

        public bool ExcluirPorLancamento(int idrecebernfv, int iddocumento, string cnn)
        {
            Boolean result = true;

            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID " +
                    "FROM " +
                        "RECEBER " +
                    "WHERE " +
                        "IDRECEBERNFV = @IDRECEBERNFV AND " +
                        "IDDOCUMENTO = @IDDOCUMENTO ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDRECEBERNFV", SqlDbType.Int).Value = idrecebernfv;
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

        public static DataTable GridDados(Int32 Filial, String DataAtual, String TipoData, String TipoLista)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT RECEBER.ID AS [ID], RECEBER.FILIAL, RECEBER.DUPLICATA, RECEBER.POSICAO , RECEBER.POSICAOFIM, RECEBER.EMISSAO, " +
                "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, RECEBER.VENCIMENTO, RECEBER.VENCIMENTOPREV, " +
                "(RECEBER.VALORLIQUIDO + RECEBER.VALORBAIXANDO) - (RECEBER.VALORPAGO + RECEBER.VALORDEVOLVIDO) AS [VALORRECEBER], " +
                "SITUACAOTIPOTITULO.CODIGO AS [TITULO], RECEBER.OBSERVA AS [OBSERVA], RECEBER.CHEGOU, " +
                "TAB_BANCOS.COGNOME AS [BANCO], RECEBER.IDNOTAFISCAL, RECEBER.DESPESAPUBLICA " +
                "FROM RECEBER " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = RECEBER.IDCLIENTE  " +
                "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBER.IDDOCUMENTO  " +
                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBER.IDFORMAPAGTO  " +
                "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID = RECEBER.IDBANCO  ";
            /*
            //FILIAL
            if (Filial > 0)
            { // filial especifica
                filtro = "RECEBER.FILIAL = @FILIAL ";
            }
            if (TipoData == "VENC")
            {
                if (TipoLista == "F") // Futuro
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "A")  // Atraso
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTO < " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "D")  // Apenas 1 um dia
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                    filtro = filtro + "AND RECEBER.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(DataAtual + " 23:59", true);
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
                    filtro = filtro + " RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "A")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + " RECEBER.VENCIMENTOPREV < " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                }
                else if (TipoLista == "D")  // Apenas 1 um dia
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "RECEBER.VENCIMENTOPREV >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);
                    filtro = filtro + "AND RECEBER.VENCIMENTOPREV <= " + clsParser.SqlDateTimeFormat(DataAtual + " 23:59", true);
                }

            }
            if (filtro.Length > 2)
            {
                filtro = " WHERE " + filtro;
            }
            if (TipoData == "VENC")
            {
                filtro = filtro + " ORDER BY RECEBER.VENCIMENTO, RECEBER.DUPLICATA ";
            }
            else
            {
                filtro = filtro + " ORDER BY RECEBER.VENCIMENTOPREV, RECEBER.DUPLICATA ";
            }
             */
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@Filial", SqlDbType.Int).Value = Filial;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("F", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nro Dup", "DUPLICATA", 65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("P.", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("P.Fim", "POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissão", "EMISSAO", 65, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc", "DOCUMENTO", 35, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cliente", "COGNOME", 150, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Vencimento", "VENCIMENTO", 65, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Venc. Prev  ", "VENCIMENTOPREV", 65, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Valor Titulo", "VALORRECEBER", 100, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tipo", "TITULO", 35, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Observa", "OBSERVA", 200, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Enviou", "CHEGOU", 45, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Banco", "BANCO", 90, true, DataGridViewContentAlignment.MiddleLeft)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DUPLICATA");

            dgv.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VENCIMENTOPREV"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VALORRECEBER"].DefaultCellStyle.Format = "N2";
        }

        /*
        public void SicronizarNfvendareceber_Receber(Int32 nfvenda_id, String conexao, String pasta)
        {
            SqlDataAdapter sda;
            DataTable dtReceber = new DataTable();
            DataTable dtReceber_erro = new DataTable();
            DataTable dtRecebida = new DataTable();
            DataTable dtNfvendareceber = new DataTable();

            sda = new SqlDataAdapter("select nfvendareceber.id, nfvenda.numero, nfvendareceber.posicao, nfvendareceber.posicaofim from nfvendareceber inner join nfvenda on nfvenda.id = nfvendareceber.idnota where nfvendareceber.idnota = @idnotafiscal", conexao);
            sda.SelectCommand.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = nfvenda_id;
            sda.Fill(dtNfvendareceber);

            sda = new SqlDataAdapter("select id, duplicata, idrecebernfv, posicao, posicaofim from receber where idnotafiscal = @idnotafiscal", conexao);
            sda.SelectCommand.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = nfvenda_id;
            sda.Fill(dtReceber);

            sda = new SqlDataAdapter("select id, duplicata, idrecebernfv, posicao, posicaofim from recebida where idnotafiscal = @idnotafiscal", conexao);
            sda.SelectCommand.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = nfvenda_id;
            sda.Fill(dtRecebida);

            // Verifica parcela por parcela
            Boolean existeRegistro;
            for (int i = 0; i < dtNfvendareceber.Rows.Count; i++)
            {
                existeRegistro = false;

                for (int i2 = 0; i2 < dtReceber.Rows.Count; i2++)
                {
                    if (dtNfvendareceber.Rows[i]["id"].ToString().Equals(dtReceber.Rows[i2]["idrecebernfv"].ToString()) == true)
                    {
                        existeRegistro = true;
                        break;
                    }
                }

                if (existeRegistro == false)
                {
                    for (int i2 = 0; i2 < dtRecebida.Rows.Count; i2++)
                    {
                        if (dtNfvendareceber.Rows[i]["id"].ToString().Equals(dtRecebida.Rows[i2]["idrecebernfv"].ToString()) == true)
                        {
                            existeRegistro = true;
                            break;
                        }
                    }
                }

                if (existeRegistro == false)
                {
                    for (int i2 = 0; i2 < dtReceber.Rows.Count; i2++)
                    {
                        if (dtNfvendareceber.Rows[i]["posicao"].ToString().Equals(dtReceber.Rows[i2]["posicao"].ToString()) == true)
                        {
                            SqlConnection scn = new SqlConnection(conexao);
                            SqlCommand scd = new SqlCommand("update receber set idrecebernfv=@idrecebernfv where id = @id", scn);
                            scd.Parameters.Add("@idrecebernfv", SqlDbType.Int).Value = dtNfvendareceber.Rows[i]["id"];
                            scd.Parameters.Add("@id", SqlDbType.Int).Value = dtReceber.Rows[i]["id"];

                            scn.Open();
                            scd.ExecuteNonQuery();
                            scn.Close();

                            existeRegistro = true;
                            break;
                        }
                    }

                    if (existeRegistro == false)
                    {
                        for (int i2 = 0; i2 < dtRecebida.Rows.Count; i2++)
                        {
                            if (dtNfvendareceber.Rows[i]["posicao"].ToString().Equals(dtRecebida.Rows[i2]["posicao"].ToString()) == true)
                            {
                                SqlConnection scn = new SqlConnection(conexao);
                                SqlCommand scd = new SqlCommand("update recebida set idrecebernfv=@idrecebernfv where id = @id", scn);
                                scd.Parameters.Add("@idrecebernfv", SqlDbType.Int).Value = dtNfvendareceber.Rows[i]["id"];
                                scd.Parameters.Add("@id", SqlDbType.Int).Value = dtRecebida.Rows[i]["id"];

                                scn.Open();
                                scd.ExecuteNonQuery();
                                scn.Close();

                                existeRegistro = true;

                                System.IO.File.AppendAllText(pasta + "\\erro_receber.txt", "\nNF:" + dtNfvendareceber.Rows[i]["numero"].ToString() + " - " + dtNfvendareceber.Rows[i]["posicao"].ToString());

                                break;
                            }
                        }
                    }
                }
            }

            DataRow rowAtual;
            DataRow rowAnterior;

            sda = new SqlDataAdapter("select id, posicao, posicaofim, valor from receber where idnotafiscal = @idnotafiscal and (select count(*) from nfvendareceber where id=receber.idrecebernfv) = 0 order by posicao", conexao);
            sda.SelectCommand.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = nfvenda_id;
            sda.Fill(dtReceber_erro);
            for (int i = 0; i < dtReceber_erro.Rows.Count; i++)
            {
                if (i == 0)
                {
                    rowAtual = dtReceber_erro.Rows[i];
                }
                else
                {
                    rowAnterior = dtReceber_erro.Rows[i - 1];
                    rowAtual = dtReceber_erro.Rows[i];

                    if (rowAtual["posicao"].ToString() == rowAnterior["posicao"].ToString() &&
                        rowAtual["posicaofim"].ToString() == rowAnterior["posicaofim"].ToString() &&
                        rowAtual["valor"].ToString() == rowAnterior["valor"].ToString())
                    {
                        Excluir(clsParser.Int32Parse(rowAtual["id"].ToString()), conexao);
                    }
                }
            }

            dtReceber_erro = new DataTable();
            sda = new SqlDataAdapter("select id, posicao, posicaofim, valor from receber where idnotafiscal = @idnotafiscal and (select count(*) from nfvendareceber where id=receber.idrecebernfv) = 0 order by posicao", conexao);
            sda.SelectCommand.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = nfvenda_id;
            sda.Fill(dtReceber_erro);

            for (int i = 0; i < dtReceber_erro.Rows.Count; i++)
            {
                SqlConnection scn = new SqlConnection(conexao);
                SqlCommand scd = new SqlCommand("select id from nfvendareceber where posicao=@posicao and posicaofim=@posicaofim and valor=@valor and idnota = @idnota", scn);
                SqlDataReader sdr;

                scd.Parameters.Add("@posicao", SqlDbType.Int).Value = dtReceber_erro.Rows[i]["posicao"];
                scd.Parameters.Add("@posicaofim", SqlDbType.Int).Value = dtReceber_erro.Rows[i]["posicaofim"];
                scd.Parameters.Add("@valor", SqlDbType.Decimal).Value = dtReceber_erro.Rows[i]["valor"];
                scd.Parameters.Add("@idnota", SqlDbType.Decimal).Value = nfvenda_id;

                scn.Open();
                sdr = scd.ExecuteReader();
                if (sdr.Read() == true)
                {
                    SqlConnection scnReceber = new SqlConnection(conexao);
                    SqlCommand scdReceber = new SqlCommand("update receber set idrecebernfv=@idrecebernfv where id = @id", scnReceber);
                    scdReceber.Parameters.Add("@idrecebernfv", SqlDbType.Int).Value = sdr["id"];
                    scdReceber.Parameters.Add("@id", SqlDbType.Int).Value = dtReceber_erro.Rows[i]["id"];

                    scnReceber.Open();
                    scdReceber.ExecuteNonQuery();
                    scnReceber.Close();
                }
                else
                {
                    Excluir(clsParser.Int32Parse(dtReceber_erro.Rows[i]["id"].ToString()), conexao);
                }

                scn.Close();
            }
        }*/

        /*
        public void ReceberTransfere(Int32 nfvenda_id, Int32 bancos_id, Int32 _diavencimento, DataTable _dtNfvendareceber)
        {
            clsBancosInfo clsBancosInfo;
            clsBancosBLL clsBancosBLL;

            clsCentrocustosInfo clsCentrocustosInfo;
            clsCentrocustosBLL clsCentrocustosBLL;

            clsHistoricosInfo clsHistoricosInfo;
            clsHistoricosBLL clsHistoricosBLL;

            clsReceberInfo clsReceberInfo;
            clsReceberBLL clsReceberBLL;

            clsReceber01BLL clsReceber01BLL;

            clsSituacaocobrancacodInfo clsSituacaocobrancacodInfo;
            clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL;

            DataTable dtNfvendareceber;
            clsNfvendareceberBLL clsNfvendareceberBLL;

            clsNfvendaInfo clsNfvendaInfo;
            clsNfvendaBLL clsNfvendaBLL;

            clsParse clsParse;
            clsProcedures clsProcedures;

            clsBancosBLL = new clsBancosBLL();
            clsCentrocustosBLL = new clsCentrocustosBLL();
            clsHistoricosBLL = new clsHistoricosBLL();
            clsNfvendaBLL = new clsNfvendaBLL();
            clsNfvendareceberBLL = new clsNfvendareceberBLL();
            clsReceberBLL = new clsReceberBLL();
            clsReceber01BLL = new clsReceber01BLL();
            clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();

            clsParse = new clsParse();
            clsProcedures = new clsProcedures();

            clsBancosInfo = clsBancosBLL.Carregar(bancos_id, clsInfo.conexaosqlbanco);
            clsCentrocustosInfo = clsCentrocustosBLL.Carregar(clsInfo.zcentrocustos, clsInfo.conexaosqlbanco);
            clsHistoricosInfo = clsHistoricosBLL.Carregar(clsInfo.zhistoricos, clsInfo.conexaosqlbanco);
            clsNfvendaInfo = clsNfvendaBLL.Carregar(nfvenda_id, clsInfo.conexaosqldados);
            //dtNfvendareceber = clsNfvendareceberBLL.CarregaGrid(clsInfo.conexaosqldados, nfvenda_id);
            dtNfvendareceber = _dtNfvendareceber;
            clsSituacaocobrancacodInfo = clsSituacaocobrancacodBLL.Carregar(clsParser.Int32Parse(clsProcedures.PesquisaValor(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD", "ID", "CODIGO", "00")), clsInfo.conexaosqldados);

            Decimal nfvendareceber_valor;
            Decimal valorjuros;
            Decimal valorjuros_mes;

            Int32 receber_id;

            Int32 x = 0;
            Int32[] ids = new Int32[dtNfvendareceber.Rows.Count];

            valorjuros_mes = clsParser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT JUROSMES FROM EMPRESAGERE WHERE EMPRESA=" + clsInfo.zempresaid.ToString()));

            foreach (DataRow linha in dtNfvendareceber.Rows)
            {
                if (linha.RowState != DataRowState.Deleted)
                {
                    if (linha["PAGOU"].ToString() == "S")
                    {
                        return;
                    }
                }
            }

            foreach (DataRow linha in dtNfvendareceber.Rows)
            {
                if (linha.RowState == DataRowState.Deleted &&
                    clsParser.Int32Parse(linha[0, DataRowVersion.Original].ToString()) > 0)
                {
                    Excluir(clsParser.Int32Parse(linha[0, DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                    x++;
                    continue;
                }
                else if (linha.RowState == DataRowState.Modified ||
                         linha.RowState == DataRowState.Added)
                {
                    nfvendareceber_valor = Decimal.Round(clsParser.DecimalParse(linha["VALOR"].ToString()), 2);
                    if (nfvendareceber_valor > 0)
                        valorjuros = (nfvendareceber_valor * (valorjuros_mes / 100)) / 30;
                    else
                        valorjuros = 0;

                    // Verifica se existe no Contas a Receber
                    clsReceberInfo = new clsReceberInfo();
                    receber_id = clsParser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT RECEBER.ID FROM RECEBER INNER JOIN DOCFISCAL ON DOCFISCAL.ID=RECEBER.IDDOCUMENTO WHERE RECEBER.IDRECEBERNFV=" + clsParser.Int32Parse(linha["ID"].ToString()).ToString() + " AND LEFT(DOCFISCAL.CODIGO,3)='NFV'"));
                    if (receber_id > 0)
                        clsReceberInfo = clsReceberBLL.Carregar(receber_id, clsInfo.conexaosqldados);
                    else
                    {
                        receber_id = clsParser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM RECEBER WHERE IDNOTAFISCAL=" + clsNfvendaInfo.id.ToString() + " AND IDRECEBERNFV=" + clsParser.Int32Parse(linha["ID"].ToString()) + ""));

                        // Se não existir, tentar encontrar de outra forma
                        if (receber_id > 0)
                        {
                            clsReceberInfo = clsReceberBLL.Carregar(receber_id, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsReceberInfo = new clsReceberInfo();
                        }
                    }

                    clsReceberInfo.boleto = "";
                    clsReceberInfo.idnotafiscal = clsNfvendaInfo.id;

                    clsReceberInfo.boletonro = clsParser.DecimalParse(linha["BOLETONRO"].ToString());
                    clsReceberInfo.contrato = "";
                    clsReceberInfo.datalanca = clsParser.DateTimeParse(clsNfvendaInfo.data.ToString());
                    clsReceberInfo.duplicata = clsNfvendaInfo.numero;
                    clsReceberInfo.dv = linha["DV"].ToString();
                    clsReceberInfo.emissao = clsParser.DateTimeParse(clsNfvendaInfo.data.ToString());
                    clsReceberInfo.emitente = clsNfvendaInfo.emitente;
                    clsReceberInfo.idbanco = clsBancosInfo.idbanco;
                    clsReceberInfo.idbancoint = clsBancosInfo.id;
                    clsReceberInfo.idcentrocusto = clsCentrocustosInfo.id;
                    clsReceberInfo.idcliente = clsNfvendaInfo.idcliente;
                    clsReceberInfo.idcodigo01 = 0;
                    clsReceberInfo.idcodigo02 = 0;
                    clsReceberInfo.idcodigo03 = 0;
                    clsReceberInfo.idcodigo04 = 0;
                    clsReceberInfo.iddespesa = 0;
                    clsReceberInfo.idcodigoctabil = clsInfo.zcontacontabil;
                    clsReceberInfo.iddocumento = clsNfvendaInfo.iddocumento;
                    clsReceberInfo.idformapagto = clsNfvendaInfo.idformapagto;
                    clsReceberInfo.filial = clsNfvendaInfo.filial;

                    if (clsReceberInfo.filial == 0)
                    {
                        clsReceberInfo.filial = clsInfo.zfilial;
                    }

                    clsReceberInfo.idsitbanco = clsInfo.zsituacaotitulo;
                    clsReceberInfo.idnotafiscal = clsNfvendaInfo.id;
                    clsReceberInfo.idrecebernfv = clsParser.Int32Parse(linha["ID"].ToString());
                    clsReceberInfo.idsitbanco = 0;
                    //clsReceberInfo.idvendedor = clsNfvendaInfo.idvendedor;
                    clsReceberInfo.idvendedor = 0;
                    clsReceberInfo.imprimir = "";
                    clsReceberInfo.observa = clsNfvendaInfo.observa;
                    clsReceberInfo.posicao = clsParser.Int32Parse(linha["POSICAO"].ToString());
                    clsReceberInfo.posicaofim = clsParser.Int32Parse(linha["POSICAOFIM"].ToString());
                    clsReceberInfo.setor = "N";
                    clsReceberInfo.transfebanco = "";
                    clsReceberInfo.transfenumero = 0;
                    clsReceberInfo.valor = clsParser.DecimalParse(linha["VALOR"].ToString());
                    clsReceberInfo.valorbaixando = 0;
                    clsReceberInfo.valorcomissao = clsParser.DecimalParse(linha["VALORCOMISSAO"].ToString());
                    clsReceberInfo.valorcomissaoger = clsParser.DecimalParse(linha["VALORCOMISSAOGER"].ToString());
                    clsReceberInfo.valordesconto = 0;
                    clsReceberInfo.valordevolvido = 0;
                    clsReceberInfo.valorliquido = 0;
                    clsReceberInfo.valormulta = 0;
                    clsReceberInfo.valorpago = 0;
                    clsReceberInfo.valorjuros = valorjuros;
                    clsReceberInfo.valorliquido = nfvendareceber_valor;

                    //if (_diavencimento <= 0)
                    clsReceberInfo.vencimento = clsParser.DateTimeParse(linha["DATA"].ToString());
                    //else
                    //    clsReceberInfo.vencimento = clsParser.DateTimeParse(_diavencimento.ToString() + "/" + clsNfvendaInfo.data.Value.ToString("MM/yyyy") + " " + clsNfvendaInfo.data.Value.ToString("HH:mm:ss"));
                    clsReceberInfo.vencimentoprev = clsParser.DateTimeParse(linha["DATA"].ToString());

                    if (linha["BOLETONRO"].ToString().Trim() == "")
                    {
                        clsReceberInfo.idformapagto = clsParser.Int32Parse(linha["IDTIPOPAGA"].ToString());
                        clsReceberInfo.idsitbanco = 0;
                        clsReceberInfo.contrato = "";
                    }

                    // Se mesmo assim não existir, sistema então Inclui o registro
                    if (clsReceberInfo.id == 0)
                    {
                        clsReceberInfo.atevencimento = "S";
                        clsReceberInfo.baixa = "N";

                        clsReceberInfo.id = clsReceberBLL.Incluir(clsReceberInfo, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        clsReceberBLL.Alterar(clsReceberInfo, clsInfo.conexaosqldados);
                    }

                    ids.SetValue(clsReceberInfo.id, x);

                    //receber01_id = clsParser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT RECEBER01.ID FROM RECEBER01 INNER JOIN SITUACAOCOBRANCACOD ON SITUACAOCOBRANCACOD.ID=RECEBER01.IDCOBRANCACOD WHERE RECEBER01.IDDUPLICATA=" + clsReceberInfo.id.ToString() + " AND SITUACAOCOBRANCACOD.CODIGO='00' ORDER BY ID DESC"));
                    //clsReceber01Info = new clsReceber01Info();
                    //if (receber01_id > 0)
                    //    clsReceber01Info = clsReceber01BLL.Carregar(clsInfo.conexaosqldados, receber01_id);

                    //clsReceber01Info.dataenvio = clsReceberInfo.vencimento;
                    //clsReceber01Info.dataok = clsReceberInfo.vencimento;
                    //clsReceber01Info.debcred = "C";
                    //clsReceber01Info.idcentrocusto = clsInfo.zcentrocustos;
                    //clsReceber01Info.idcobrancacod = clsSituacaocobrancacodInfo.id;
                    //clsReceber01Info.idcobrancahis = 0;
                    //clsReceber01Info.idcodigoctabil = clsInfo.zcontacontabil;
                    //clsReceber01Info.idduplicata = clsReceberInfo.id;
                    //clsReceber01Info.idhistorico = clsInfo.zhistoricos;
                    //clsReceber01Info.idrecebida01 = 0;
                    //clsReceber01Info.motivo = "";
                    //clsReceber01Info.valor = clsReceberInfo.valor;
                    //clsReceber01Info.valorcomissao = clsReceberInfo.valorcomissao;
                    //clsReceber01Info.valorcomissaoger = clsReceberInfo.valorcomissaoger;

                    //if (clsReceber01Info.id == 0)
                    //    clsReceber01BLL.Incluir(clsInfo.conexaosqldados, clsReceber01Info);
                    //else
                    //    clsReceber01BLL.Alterar(clsInfo.conexaosqldados, clsReceber01Info);
                }
                x++;
            }

            dtNfvendareceber.AcceptChanges();
        }*/
    }
}
