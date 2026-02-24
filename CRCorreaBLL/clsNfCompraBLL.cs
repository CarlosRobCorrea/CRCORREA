using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{ 
    public class clsNfCompraBLL : SQLFactory<clsNfCompraInfo>
    {
        private enum ServicoImpostoTipo
        {
            IRRF,
            INSS,
            PISCOFINSCSLL,
            ISS,
            PIS,
            COFINS,
            CSLL
        }

        public override int Incluir(clsNfCompraInfo info, string cnn)
        {
            VerificaInfo(info, cnn);

            info.id = base.Incluir(info, cnn);

            ServicosImpostos(info, cnn);

            return info.id;
        }

        public override int Alterar(clsNfCompraInfo info, string cnn)
        {
            Int32 result;

            VerificaInfo(info, cnn);

            result = base.Alterar(info, cnn);

            ServicosImpostos(info,cnn);

            return result;
        }

        private void ServicosImpostos(clsNfCompraInfo info, String cnn)
        {
            NotaEntradaImpostoServico(ServicoImpostoTipo.IRRF, info, cnn);
            NotaEntradaImpostoServico(ServicoImpostoTipo.INSS, info, cnn);
            NotaEntradaImpostoServico(ServicoImpostoTipo.PISCOFINSCSLL, info, cnn);
            NotaEntradaImpostoServico(ServicoImpostoTipo.ISS, info, cnn);
            NotaEntradaImpostoServico(ServicoImpostoTipo.PIS, info, cnn);
            NotaEntradaImpostoServico(ServicoImpostoTipo.COFINS, info, cnn);
            NotaEntradaImpostoServico(ServicoImpostoTipo.CSLL, info, cnn);
            
        }

        private void NotaEntradaImpostoServico(ServicoImpostoTipo tipo, clsNfCompraInfo info, String cnn)
        {
            String query;
            String empresagere_campo;
            Decimal valor = 0;

            Int32 nfcomprafiscal_id;
            Int32 idfornecedor_fiscal;
            Int32 idcondpagto_fiscal;
            Int32 idcodigo_fiscal;

            clsPagarBLL PagarBLL = new clsPagarBLL();

            clsNfCompraInfo infoImposto = null;
            clsNfCompra1Info infoImpostoItem = null;

            clsNfCompra1BLL nfcompra1BLL = new clsNfCompra1BLL(); ;

            if (tipo == ServicoImpostoTipo.COFINS)
            {
                valor = info.cofins;
                empresagere_campo = "COFINS";
                idcodigo_fiscal = clsInfo.zfiscalcofinsid;
            }
            else if (tipo == ServicoImpostoTipo.CSLL)
            {
                valor = info.csll;
                empresagere_campo = "CSLL";
                idcodigo_fiscal = clsInfo.zfiscalcsllid;
            }
            else if (tipo == ServicoImpostoTipo.INSS)
            {
                valor = info.inss;
                empresagere_campo = "INSS";
                idcodigo_fiscal = clsInfo.zfiscalinssid;
            }
            else if (tipo == ServicoImpostoTipo.IRRF)
            {
                valor = info.irrf;
                empresagere_campo = "IRRF";
                idcodigo_fiscal = clsInfo.zfiscalirrfid;
            }
            else if (tipo == ServicoImpostoTipo.ISS)
            {
                valor = info.iss;
                empresagere_campo = "ISS";
                idcodigo_fiscal = clsInfo.zfiscalissid;
            }
            else if (tipo == ServicoImpostoTipo.PIS)
            {
                valor = info.pis;
                empresagere_campo = "PIS";
                idcodigo_fiscal = clsInfo.zfiscalpisid;
            }
            else if (tipo == ServicoImpostoTipo.PISCOFINSCSLL)
            {
                valor = info.piscofinscsll;
                empresagere_campo = "PISCOFINS";
                idcodigo_fiscal = clsInfo.zfiscalpiscofinsid;
            }
            else
            {
                empresagere_campo = "";
                idcodigo_fiscal = clsInfo.zpecas;
            }

            idfornecedor_fiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select IDFOR" + empresagere_campo+"  from EMPRESAGERE where ID = "+ clsInfo.zempresaid,"0"));
            //idfornecedor_fiscal = clsParser.Int32Parse(Procedure.PesquisaValor(cnn, "EMPRESAGERE", "IDFOR" + empresagere_campo, "ID", clsInfo.zempresaid));

            idcondpagto_fiscal = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select IDPAG" + empresagere_campo+" from EMPRESAGERE where ID = " + clsInfo.zempresaid, "0"));
            //idcondpagto_fiscal = clsParser.Int32Parse(Procedure.PesquisaValor(cnn, "EMPRESAGERE", "IDPAG" + empresagere_campo, "ID", clsInfo.zempresaid));

            query = "SELECT " +
                        "ID " +
                     "FROM " +
                        "NFCOMPRA " +
                    "WHERE " +
                        "FILIAL = " + info.filial + " AND " +
                        "NUMERO = " + info.numero + " AND " +
                        "SERIE = " + info.serie + " AND " +
                        "IDFORNECEDORORIGEM = " + info.idfornecedor + " AND " +
                        "IDFORNECEDOR = " + idfornecedor_fiscal;

            nfcomprafiscal_id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, query));
            
            // Cabeçalho
            if (nfcomprafiscal_id == 0) infoImposto = new clsNfCompraInfo();
            else infoImposto = Carregar(nfcomprafiscal_id, cnn);

            if (valor > 0)
            {
                infoImposto.chnfe = "";
                infoImposto.data = info.data;
                infoImposto.datalanca = info.datalanca;
                infoImposto.datarecebimento = info.datarecebimento;
                infoImposto.emitente = info.emitente;
                infoImposto.filial = info.filial;
                infoImposto.frete = info.frete;
                infoImposto.fretepaga = info.fretepaga;
                infoImposto.idcondpagto = idcondpagto_fiscal;
                infoImposto.iddocumento = info.iddocumento;
                infoImposto.idformapagto = clsInfo.zformapagto;
                infoImposto.idfornecedor = idfornecedor_fiscal;
                infoImposto.idfornecedororigem = info.idfornecedor;
                infoImposto.idpedido = clsInfo.zcompras;
                infoImposto.idtransportadora = clsInfo.zempresaclienteid;
                infoImposto.numero = info.numero;
                infoImposto.observa = "";
                infoImposto.serie = info.serie;
                infoImposto.setor = info.setor;
                infoImposto.setorfator = info.setorfator;
                infoImposto.situacao = info.situacao;
                infoImposto.tipoentrada = info.tipoentrada;
                infoImposto.tipofrete = info.tipofrete;
                infoImposto.transporte = info.transporte;

                // Item
                if (nfcomprafiscal_id == 0) infoImpostoItem = new clsNfCompra1Info();
                else infoImpostoItem = nfcompra1BLL.Carregar(clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from nfcompra1 where numero=" + nfcomprafiscal_id)), cnn);

                infoImpostoItem.calculoautomatico = "S";
                infoImpostoItem.codigoemp01 = "";
                infoImpostoItem.codigoemp02 = "";
                infoImpostoItem.codigoemp03 = "";
                infoImpostoItem.codigoemp04 = "";
                infoImpostoItem.complemento = "";
                infoImpostoItem.complemento1 = "";
                infoImpostoItem.consumo = "S";
                infoImpostoItem.creditaricm = "N";
                //infoImpostoItem.dataentrega = infoImposto.data;
                infoImpostoItem.descricaoesp = "";
                infoImpostoItem.fatura = "S";
                infoImpostoItem.faturando = "N";
                infoImpostoItem.idcentrocusto = clsInfo.zcentrocustos;
                infoImpostoItem.idcfop = clsInfo.zcfop;
                infoImpostoItem.idcfopfis = clsInfo.zcfop;
                infoImpostoItem.idcodigo = idcodigo_fiscal;
                infoImpostoItem.idcodigo1 = clsInfo.zpecas;
                infoImpostoItem.idcodigoctabil = clsInfo.zcontacontabil;
                infoImpostoItem.idcotacao = clsInfo.zcotacao;
                infoImpostoItem.iddestino = clsInfo.zpecas;
                infoImpostoItem.idhistorico = clsInfo.zhistoricos;
                infoImpostoItem.idipi = clsInfo.zipi;
                infoImpostoItem.iditemcotacao = clsInfo.zcotacao1;
                infoImpostoItem.iditempedido = clsInfo.zcompras1;
                infoImpostoItem.iditempedidoentrega = clsInfo.zcomprasentrega;
                infoImpostoItem.idnfvendaitem = clsInfo.znfvenda1;
                infoImpostoItem.idnotafiscal = clsInfo.znfvenda;
                infoImpostoItem.idordemservico = clsInfo.zordemservico;
                infoImpostoItem.idpedido = clsInfo.zcompras;
                infoImpostoItem.idpedidovenda = clsInfo.zpedido1;
                infoImpostoItem.idpedidovendaitem = clsInfo.zpedido2;
                infoImpostoItem.idsittriba = clsInfo.zsituacaotriba;
                infoImpostoItem.idsittribb = clsInfo.zsituacaotribb;
                infoImpostoItem.idsittribcofins1 = clsInfo.zsittribcofins;
                infoImpostoItem.idsittribipi = clsInfo.zsittribipi;
                infoImpostoItem.idsittribpis = clsInfo.zsittribpis;
                infoImpostoItem.idsolicitacao = clsInfo.zsolicitacao;
                infoImpostoItem.idunid = clsInfo.zunidade;
                infoImpostoItem.idunidfiscal = clsInfo.zunidade;
                infoImpostoItem.msg = "";
                infoImpostoItem.preco = valor;
                infoImpostoItem.qtde = 1;
                infoImpostoItem.qtdefiscal = 1;
                infoImpostoItem.situacao = "";
                infoImpostoItem.somaproduto = "S";
                infoImpostoItem.tipodestino = "";
                infoImpostoItem.tipoproduto = "00";
                infoImpostoItem.totalmercado = valor;
                infoImpostoItem.totalnota = valor;
                infoImpostoItem.valorfreteicms = "N";
                infoImpostoItem.valoroutrasicms = "N";
                infoImpostoItem.valorseguroicms = "N";

                infoImposto.totalmercadoria = infoImpostoItem.totalmercado;
                infoImposto.totalnotafiscal = infoImpostoItem.totalnota;

                if (nfcomprafiscal_id == 0)
                {
                    infoImposto.id = Incluir(infoImposto, cnn);
                }
                else
                {
                    Alterar(infoImposto, cnn);
                }

                infoImpostoItem.numero = infoImposto.id;

                if (infoImpostoItem.id == 0)
                {
                    infoImpostoItem.id = nfcompra1BLL.Incluir(infoImpostoItem, cnn);
                }
                else
                {
                    nfcompra1BLL.Alterar(infoImpostoItem, cnn);
                }

                // Gera Pagamentos
                clsNfCompraPagarBLL nfcompraPagarBLL = new clsNfCompraPagarBLL();
                clsNfCompraPagarInfo nfcompraPagarInfo;

                DataTable dtPagamentos = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("select * from nfcomprapagar where idnota=" + infoImposto.id, cnn);
                sda.Fill(dtPagamentos);
                if (dtPagamentos.Select("pagou = 'S'").Length > 0)
                {
                    throw new Exception("Não é possível editar uma Nota Fiscal de Entrada que já teve parcela(s) paga(s).");
                }

                foreach (DataRow row in dtPagamentos.Rows)
                {
                    PagarBLL.Excluir(clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from pagar where iddocumento=" + infoImposto.iddocumento + " and idnotafiscal=" + infoImposto.id + " and idpagarnfe=" + row["id"].ToString())), cnn);
                    nfcompraPagarBLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }

                dtPagamentos = clsFinanceiro.GerarFatura(infoImposto.data,
                                                        infoImposto.totalnotafiscal,
                                                        infoImposto.idformapagto,
                                                        infoImposto.idcondpagto);

                foreach (DataRow row in dtPagamentos.Rows)
                {
                    nfcompraPagarInfo = new clsNfCompraPagarInfo();
                    nfcompraPagarInfo.boletonro = clsParser.Int32Parse(row["boletonro"].ToString());
                    nfcompraPagarInfo.data = clsParser.DateTimeParse(row["data"].ToString());
                    nfcompraPagarInfo.dv = row["dv"].ToString();
                    nfcompraPagarInfo.idnota = infoImposto.id;
                    nfcompraPagarInfo.idtipopaga = clsParser.Int32Parse(row["idtipopaga"].ToString());
                    nfcompraPagarInfo.pagou = row["pagou"].ToString();
                    nfcompraPagarInfo.posicao = clsParser.Int32Parse(row["posicao"].ToString());
                    nfcompraPagarInfo.posicaofim = clsParser.Int32Parse(row["posicaofim"].ToString());
                    nfcompraPagarInfo.valor = clsParser.DecimalParse(row["valor"].ToString());

                    nfcompraPagarBLL.Incluir(nfcompraPagarInfo, cnn);
                }
            }
            else
            {
                if (nfcomprafiscal_id > 0)
                {
                    Excluir(nfcomprafiscal_id, cnn);
                }
            }

            if (infoImposto.id > 0)
            {
                PagarBLL.SicronizarPorDocumento(infoImposto.id, infoImposto.iddocumento, cnn);
            }
        }

        public override bool Excluir(int id, string cnn)
        {
            DataTable dtPagar = new DataTable();
            DataTable dtNfcompraPagar = new DataTable();
            DataTable dtNfcompra1 = new DataTable();
            
            String query;
            Int32 idtipodocumento;
            SqlDataAdapter sda;

            idtipodocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select iddocumento from nfcompra where id=" + id));

            query = "select id from pagar where idnotafiscal=" + id + " and iddocumento=" + idtipodocumento;
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dtPagar);

            query = "select id, pagou from nfcomprapagar where idnota=" + id;
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dtNfcompraPagar);

            query = "select id, idordemservico from nfcompra1 where numero=" + id;
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dtNfcompra1);

            // Verifica se já houve alguma parcela paga
            foreach (DataRow row in dtNfcompraPagar.Rows)
            {
                if (row["pagou"].ToString() == "S")
                {
                    throw new Exception("Já houve parcelas pagas. Não é possível excluir NF.");
                }
            }

            // Verifica se algum item está ligado a uma Ordem de Serviço
            Int32 idos;
            foreach (DataRow row in dtNfcompra1.Rows)
            {
                idos = clsParser.Int32Parse(row["idordemservico"].ToString());
                if (idos != 0 && idos != clsInfo.zordemservico)
                {
                    throw new Exception("Existe item(ns) da NF ligados a Ordem(ns) de Serviço.");
                }
            }

            clsPagarBLL PagarBLL = new clsPagarBLL();

            foreach (DataRow row in dtPagar.Rows)
            {
                PagarBLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
            }

            clsNfCompraPagarBLL nfcompraPagarBLL = new clsNfCompraPagarBLL();

            foreach (DataRow row in dtNfcompraPagar.Rows)
            {
                nfcompraPagarBLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
            }

            clsNfCompra1BLL nfcompra1BLL = new clsNfCompra1BLL();

            foreach (DataRow row in dtNfcompra1.Rows)
            {
                nfcompra1BLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
            }

            return base.Excluir(id, cnn);
        }

        void VerificaInfo(clsNfCompraInfo info, String cnn)
        {
            if (info.numero <= 0)   throw new Exception("Nº da NF não pode ser 0(zero).");
            if (info.filial <= 0)   throw new Exception("Nº da Filial não pode ser 0(zero).");
            if (info.serie == null) throw new Exception("Nº da Série deve ser definido.");
            if (info.idfornecedor <= 0) throw new Exception("CNPJ deve ser definido.");

            if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from nfcompra where numero = " + info.numero + " and filial = " + info.filial + " and serie='" + info.serie + "' and idfornecedor=" + info.idfornecedor + " and id<>" + info.id)) > 0)
            {
                throw new Exception("Nº da NF - Série para esse CNPJ já existe.");
            }
        }

        public static DataTable GridDados(Int32 Filial, String Periodo, String DaData, String AteData, String TipoLista)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT NFCOMPRA.ID, NFCOMPRA.FILIAL, NFCOMPRA.NUMERO, " +
                    " case len(NFCOMPRA.CAMINHONFE_XML) when 0 then '' " +
                    "when 1 then '' else 'OK' end 	as XML, " +
                    " case len(NFCOMPRA.CAMINHONFE_PDF) when 0 then '' " +
                    "when 1 then '' else 'OK' end 	as PDF, " +    
                    "DOCFISCAL.COGNOME AS [DOCUMENTO], " +
                    "NFCOMPRA.DATA, NFCOMPRA.DATARECEBIMENTO, CLIENTE.COGNOME AS FORNECEDOR, " +
                    "NFCOMPRA.SITUACAO, COMPRAS.NUMERO AS [PEDCOMPRA], TOTALNOTAFISCAL, " +
                    "(NFCOMPRA.TOTALNOTAFISCAL - (NFCOMPRA.IRRF+NFCOMPRA.INSS+NFCOMPRA.PISCOFINSCSLL+NFCOMPRA.CSLL+NFCOMPRA.ISS)) AS [TOTLIQ], NFCOMPRA.OBSERVA " +
			        "FROM NFCOMPRA   " +
                    "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID=NFCOMPRA.IDDOCUMENTO " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=NFCOMPRA.IDFORNECEDOR " +
                    "LEFT JOIN COMPRAS ON COMPRAS.ID=NFCOMPRA.IDPEDIDO ";
            if (Filial > 0)
            {
                filtro = "NFCOMPRA.FILIAL = " + Filial;
            }
            if (Periodo == "E")
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "NFCOMPRA.DATARECEBIMENTO >= " + clsParser.SqlDateTimeFormat(DaData + " 00:00", true);
                filtro = filtro + " AND NFCOMPRA.DATARECEBIMENTO <= " + clsParser.SqlDateTimeFormat(AteData + " 23:59", true);
            }

            if (TipoLista == "1")
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "LEFT(NFCOMPRA.SITUACAO,1) = '" + TipoLista + "' ";
            }
            if (TipoLista == "2")
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "LEFT(NFCOMPRA.SITUACAO,1) = '" + TipoLista + "' ";
            }
            if (TipoLista == "3")
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "LEFT(NFCOMPRA.SITUACAO,1) = '" + TipoLista + "' ";
            }
            if (TipoLista == "4")
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "LEFT(NFCOMPRA.SITUACAO,1) = '" + TipoLista + "' ";
            }
            if (filtro.Length > 2)
            {
                filtro = filtro + " AND ";
            }
            filtro = filtro + " NFCOMPRA.NUMERO > " + clsParser.Int32Parse("0".ToString()) + " ";
            if (filtro.Length > 2)
            {
                filtro = " where " + filtro;
            }
            query = query + filtro + " ORDER BY NFCOMPRA.DATARECEBIMENTO, NFCOMPRA.NUMERO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
//            sda.SelectCommand.Parameters.Add("FILIAL", SqlDbType.Int).Value = Filial;
//            sda.SelectCommand.Parameters.Add("SITUACAO", SqlDbType.Int).Value = DoAno;
//            sda.SelectCommand.Parameters.Add("ATEANO", SqlDbType.Int).Value = AteAno;
//            sda.SelectCommand.Parameters.Add("TERMINO", SqlDbType.NVarChar).Value = 'A';
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 10, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("F.", "FILIAL", 15, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nº NFE", "NUMERO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("PDF", "PDF", 25, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("XML", "XML", 25, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc", "DOCUMENTO", 30, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("D.Emissão", "DATA", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("D.Receb.", "DATARECEBIMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fornecedor", "FORNECEDOR", 110, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Sit", "SITUACAO", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Ped.Comp", "PEDCOMPRA", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total NF", "TOTALNOTAFISCAL", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total Liq", "TOTLIQ", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Observa", "OBSERVA", 220, true, DataGridViewContentAlignment.MiddleLeft),
        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["NUMERO"].DefaultCellStyle.Format = "N0";
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["DATARECEBIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["TOTALNOTAFISCAL"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTLIQ"].DefaultCellStyle.Format = "N2";
        }


/*
        public DataTable CarregaGrid(String _conexao, DateTime _datade, DateTime _dataate, String _situacao, String _serie, Int32 _filial)
        {
            clsNfCompraDAL clsNfCompraDAL = new clsNfCompraDAL();
            return clsNfCompraDAL.CarregaGrid(_conexao, _datade, _dataate, _situacao, _serie, _filial);
        }

        public DataTable CarregaGrid(String _conexao, Int32 _mes, Int32 _ano, Int32 _filial, String _situacao, Int32 _index)
        {
            clsNfCompraDAL clsNfCompraDAL = new clsNfCompraDAL();
            return clsNfCompraDAL.CarregaGrid(_conexao,_mes, _ano, _filial, _situacao, _index);
        }
        public DataTable CarregaGrid_Ultimas_Entradas(String _conexao, Int32 _idcodigo)
        {
            clsNfCompraDAL clsNfCompraDAL = new clsNfCompraDAL();
            return clsNfCompraDAL.CarregaGrid_Ultimas_Entradas(_conexao, _idcodigo);
        }
        public DataTable CarregaGrid_Nfcompra_compras1_grid(String _conexao, Int32 _idcompras1)
        {
            clsNfCompraDAL clsNfCompraDAL = new clsNfCompraDAL();
            return clsNfCompraDAL.CarregaGrid_Nfcompra_compras1_grid(_conexao, _idcompras1);
        }
 */

        public void EntradaEntregaItemPedidoCompra(Int32 idcomprasentrega, ref DataTable dtNFCompra1, String cnn)
        {
            clsCompras1BLL Compras1BLL;
            clsCompras1Info Compras1Info;

            clsComprasEntregaBLL ComprasentregaBLL;
            clsComprasEntregaInfo ComprasentregaInfo;

            ComprasentregaBLL = new clsComprasEntregaBLL();
            ComprasentregaInfo = ComprasentregaBLL.Carregar(idcomprasentrega, cnn);

            Compras1BLL = new clsCompras1BLL();
            Compras1Info = Compras1BLL.Carregar(ComprasentregaInfo.idcompras1, cnn);

            // Verifica se este item já não está na lista
            foreach (DataRow row in dtNFCompra1.Rows)
            {
                if (row.RowState != DataRowState.Detached &&
                    row.RowState != DataRowState.Deleted)
                {
                    if (row["IDITEMPEDIDOENTREGA"].ToString() == ComprasentregaInfo.ToString())
                    {
                        return;
                    }
                }
            }

            if (ComprasentregaInfo.qtdesaldo <= 0)
            {
                return;
            }

            DataRow rowNfcompra1 = dtNFCompra1.NewRow();

            rowNfcompra1["ID"] = 0;
            rowNfcompra1["IDPEDIDO"] = Compras1Info.idcompras;
            rowNfcompra1["IDITEMPEDIDO"] = Compras1Info.id;
            rowNfcompra1["IDITEMPEDIDOENTREGA"] = ComprasentregaInfo.id;
            rowNfcompra1["IDCOTACAO"] = Compras1Info.idcotacao;
            rowNfcompra1["IDITEMCOTACAO"] = Compras1Info.idcotacaoitem;
            rowNfcompra1["IDSOLICITACAO"] = Compras1Info.idsolicitacao;
            rowNfcompra1["IDPEDIDOVENDA"] = clsInfo.zpedido;
            rowNfcompra1["IDPEDIDOVENDAITEM"] = clsInfo.zpedido1;
            rowNfcompra1["IDORDEMSERVICO"] = clsInfo.zordemservico;
            rowNfcompra1["IDTIPONOTA"] = Compras1Info.idtiponota;
            rowNfcompra1["IDTIPONOTASAIDA"] = Compras1Info.idtiponota;
            rowNfcompra1["IDCFOPFIS"] = Compras1Info.idcfopfis;
            rowNfcompra1["IDCFOP"] = Compras1Info.idcfop;
            rowNfcompra1["IDCENTROCUSTO"] = Compras1Info.idcentrocusto;
            rowNfcompra1["IDHISTORICO"] = Compras1Info.idhistorico;
            rowNfcompra1["IDCODIGOCTABIL"] = Compras1Info.idcodigoctabil;
            rowNfcompra1["IDCODIGO"] = Compras1Info.idcodigo;
            rowNfcompra1["IDUNIDFISCAL"] = Compras1Info.idunidfiscal;
            rowNfcompra1["IDUNID"] = Compras1Info.idunid;
            rowNfcompra1["IDCODIGO1"] = Compras1Info.idcodigo1;
            rowNfcompra1["IDDESTINO"] = Compras1Info.iddestino;
            rowNfcompra1["IDSITTRIBA"] = Compras1Info.idsittriba;
            rowNfcompra1["IDSITTRIBB"] = Compras1Info.idsittribb;
            rowNfcompra1["IDSITTRIBIPI"] = Compras1Info.idsittribipi;
            rowNfcompra1["IDIPI"] = Compras1Info.idipi;
            rowNfcompra1["IRRFPORC"] = Compras1Info.irrfporc;
            rowNfcompra1["IRRF"] = Compras1Info.irrf;
            rowNfcompra1["IDIRRF"] = clsInfo.zfiscalirrfid;
            rowNfcompra1["IDPISCOFINSCSLL"] = clsInfo.zfiscalpiscofinsid;
            rowNfcompra1["IDISS"] = clsInfo.zfiscaliss;
            rowNfcompra1["IDPIS"] = clsInfo.zfiscalpis;
            rowNfcompra1["IDCOFINS"] = clsInfo.zfiscalcofins;
            rowNfcompra1["IDNOTAFISCAL"] = clsInfo.znfvenda;
            rowNfcompra1["IDNFVENDAITEM"] = clsInfo.znfvenda1;
            rowNfcompra1["IDNFEVALES"] = 0;
            //rowNfcompra1["IDORDEMSERVICOITEM"] = 0;

            rowNfcompra1["NUMERO"] = 0;
            rowNfcompra1["COMPLEMENTO"] = Compras1Info.complemento;
            rowNfcompra1["COMPLEMENTO1"] = Compras1Info.complemento1;
            rowNfcompra1["DESCRICAOESP"] = Compras1Info.descricaoesp;
            rowNfcompra1["MSG"] = Compras1Info.msg;
            //rowNfcompra1["TIPOENTRADA"] = Compras1Info.tipoentrada;
            rowNfcompra1["CONSUMO"] = Compras1Info.consumo;
            rowNfcompra1["SITUACAO"] = "";
            rowNfcompra1["QTDEFISCAL"] = ComprasentregaInfo.qtdesaldo;
            rowNfcompra1["QTDE"] = ComprasentregaInfo.qtdesaldo;
            rowNfcompra1["QTDETICKET"] = 0;
            rowNfcompra1["FATORCONV"] = Compras1Info.fatorconv;
            rowNfcompra1["PRECO"] = Compras1Info.preco;
            rowNfcompra1["TOTALMERCADO"] = Compras1Info.totalmercado;
            rowNfcompra1["CODIGOEMP01"] = Compras1Info.codigoemp01;
            rowNfcompra1["CODIGOEMP02"] = Compras1Info.codigoemp02;
            rowNfcompra1["CODIGOEMP03"] = Compras1Info.codigoemp03;
            rowNfcompra1["CODIGOEMP04"] = Compras1Info.codigoemp04;
            //rowNfcompra1["DATAENTREGA"] = DateTime.Now;
            rowNfcompra1["TIPODESTINO"] = Compras1Info.tipodestino;
            rowNfcompra1["CREDITARICM"] = Compras1Info.creditaricm;
            rowNfcompra1["FATURA"] = "S";
            rowNfcompra1["SOMAPRODUTO"] = "S";
            rowNfcompra1["CALCULOAUTOMATICO"] = Compras1Info.calculoautomatico;
            rowNfcompra1["BASEICM"] = Compras1Info.baseicm;
            rowNfcompra1["ICM"] = Compras1Info.icm;
            rowNfcompra1["BASEMP"] = Compras1Info.basemp;
            rowNfcompra1["CUSTOICM"] = Compras1Info.custoicm;
            rowNfcompra1["BCIPI"] = Compras1Info.bcipi;
            rowNfcompra1["IPI"] = Compras1Info.ipi;
            rowNfcompra1["CUSTOIPI"] = Compras1Info.custoipi;
            rowNfcompra1["BASEICMSUBST"] = Compras1Info.baseicmsubst;
            rowNfcompra1["ICMINTERNO"] = Compras1Info.icminterno;
            //rowNfcompra1["ICMSSUBSTREDUCAO"] = ;
            rowNfcompra1["REDUCAO"] = Compras1Info.reducao;
            rowNfcompra1["ICMSUBST"] = Compras1Info.icmsubst;
            rowNfcompra1["IDSITTRIBPIS"] = Compras1Info.idsittribpis;
            rowNfcompra1["IDSITTRIBCOFINS1"] = Compras1Info.idsittribcofins1;
            rowNfcompra1["BCPISPASEP"] = Compras1Info.bcpispasep;
            rowNfcompra1["ALIQPISPASEP"] = Compras1Info.aliqpispasep;
            rowNfcompra1["PISPASEP"] = Compras1Info.pispasep;
            rowNfcompra1["BCCOFINS1"] = Compras1Info.bccofins1;
            rowNfcompra1["ALIQCOFINS1"] = Compras1Info.aliqcofins1;
            rowNfcompra1["COFINS1"] = Compras1Info.cofins1;
            rowNfcompra1["VALOROUTRAS"] = Compras1Info.valoroutras;
            rowNfcompra1["VALOROUTRASICMS"] = Compras1Info.valoroutrasicms;
            rowNfcompra1["VALORFRETE"] = Compras1Info.valorfrete;
            rowNfcompra1["VALORFRETEICMS"] = Compras1Info.valorfreteicms;
            rowNfcompra1["VALORSEGURO"] = Compras1Info.valorseguro;
            rowNfcompra1["VALORSEGUROICMS"] = Compras1Info.valorseguroicms;
            rowNfcompra1["TOTALNOTA"] = Compras1Info.totalnota;
            rowNfcompra1["INSSPORC"] = Compras1Info.inssporc;
            rowNfcompra1["INSS"] = Compras1Info.inss;
            rowNfcompra1["PISCOFINSCSLLPORC"] = Compras1Info.piscofinscsllporc;
            rowNfcompra1["PISCOFINSCSLL"] = Compras1Info.piscofinscsll;
            rowNfcompra1["ISSPORC"] = Compras1Info.issporc;
            rowNfcompra1["ISS"] = Compras1Info.iss;
            rowNfcompra1["PISPORC"] = Compras1Info.pisporc;
            rowNfcompra1["PIS"] = Compras1Info.pis;
            rowNfcompra1["COFINSPORC"] = Compras1Info.cofinsporc;
            rowNfcompra1["COFINS"] = Compras1Info.cofins;
            rowNfcompra1["CSLLPORC"] = Compras1Info.csllporc;
            rowNfcompra1["CSLL"] = Compras1Info.csll;
            rowNfcompra1["PESO"] = Compras1Info.peso;
            rowNfcompra1["PESOTOTAL"] = Compras1Info.totalpeso;
            rowNfcompra1["VIDAUTIL"] = 0;
            rowNfcompra1["QTDEDEV"] = 0;
            rowNfcompra1["QTDEDEVBAIXA"] = 0;
            rowNfcompra1["QTDEDEVSALDO"] = ComprasentregaInfo.qtdesaldo;
            rowNfcompra1["FATURANDO"] = "N";
            

            /*
            "PECAS.TIPOPRODUTO, " +
            */

            // COMPRAS.NUMERO AS PEDIDO
            if (dtNFCompra1.Columns.Contains("PEDIDO") == true) rowNfcompra1["PEDIDO"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from compras where id = " + Compras1Info.idcompras));

            // CFOP.CFOP AS [CFOPFIS]
            if (dtNFCompra1.Columns.Contains("CFOPFIS") == true) rowNfcompra1["CFOPFIS"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select CFOP from CFOP where id = " + Compras1Info.idcfopfis));

            // UNIDADEFISCAL.CODIGO AS UNIDFISCAL
            if (dtNFCompra1.Columns.Contains("UNIDFISCAL") == true) rowNfcompra1["UNIDFISCAL"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select CODIGO from UNIDADE where id = " + Compras1Info.idunidfiscal));

            // UNIDADEFISCAL.CODIGO AS UNIDFISCAL
            if (dtNFCompra1.Columns.Contains("UNIDFISCAL") == true) rowNfcompra1["UNIDFISCAL"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select CODIGO from UNIDADE where id = " + Compras1Info.idunidfiscal));

            // UNIDADE.CODIGO AS UNID
            if (dtNFCompra1.Columns.Contains("UNID") == true) rowNfcompra1["UNID"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select CODIGO from UNIDADE where id = " + Compras1Info.idunid));

            // PECAS.CODIGO
            if (dtNFCompra1.Columns.Contains("CODIGO") == true) rowNfcompra1["CODIGO"] = Procedure.PesquisaoPrimeiro(cnn, "select CODIGO from PECAS where id = " + Compras1Info.idcodigo);

            // PECAS.NOME
            if (dtNFCompra1.Columns.Contains("NOME") == true) rowNfcompra1["NOME"] = Procedure.PesquisaoPrimeiro(cnn, "select NOME from PECAS where id = " + Compras1Info.idcodigo);

            // ORDEMSERVICO.NUMERO as [NROOS]
            if (dtNFCompra1.Columns.Contains("NROOS") == true) rowNfcompra1["NROOS"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select NUMERO from ORDEMSERVICO where id = " + Compras1Info.idos).ToString());

            // PEDIDO.NUMERO AS [NROPV]
            if (dtNFCompra1.Columns.Contains("NROPV") == true) rowNfcompra1["NROPV"] = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select NUMERO from PEDIDO where id = " + clsInfo.zpedido).ToString());

            // TIPOENTRADA
            //if (dtNFCompra1.Columns.Contains("TIPOENTRADA") == true) rowNfcompra1["TIPOENTRADA"] = "";


            if (dtNFCompra1.Columns.Contains("nfcompra1_posicao") == true) rowNfcompra1["nfcompra1_posicao"] = dtNFCompra1.Rows.Count + 1;

            dtNFCompra1.Rows.Add(rowNfcompra1);
        }

        public void EntradaPedidoCompra(Int32 idcompras, ref DataTable dtNFCompra1, String cnn)
        {
            String query;
            DataTable dtComprasentrega;
            SqlDataAdapter sda;

            List<Int32> comprasentrega_ids = new List<int>();

            query = "select id, idcompras1 from comprasentrega where idcompras=@idcompras order by dataentrega ";

            dtComprasentrega = new DataTable();

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@idcompras", SqlDbType.Int).Value = idcompras;
            sda.Fill(dtComprasentrega);

            foreach (DataRow row in dtComprasentrega.Rows)
            {
                //if (comprasentrega_ids.Contains(clsParser.Int32Parse(row["idcompras1"].ToString())) == false)
                //{ // Não deixava importar com mais de 1 item
                    comprasentrega_ids.Add(clsParser.Int32Parse(row["idcompras1"].ToString()));
                    EntradaEntregaItemPedidoCompra(clsParser.Int32Parse(row["id"].ToString()), ref dtNFCompra1, cnn);
//                }
            }
        }
    }
}
