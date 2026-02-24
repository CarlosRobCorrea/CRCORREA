using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsNfCompra1BLL : SQLFactory<clsNfCompra1Info>
    {
        Int32 idtipodocumento;
        clsMovPecasBLL movPecasBLL;
        Decimal preco;

        public clsNfCompra1BLL()
        {
            movPecasBLL = new clsMovPecasBLL();

            idtipodocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select top 1 id from docfiscal where cognome = 'NFE1'"));
        }

        public override int Incluir(clsNfCompra1Info info, string cnn)
        {
            Int32 result = 0;

            result = base.Incluir(info, cnn);

            ComprasEntregaSicronizar(info.iditempedidoentrega, cnn);

            // Regra de três com o preço
            // Para desccobrir o preço correto para a qtde interna a partir da qtde fiscal

            if (info.tipoproduto.Substring(0, 2) == "00")
            {
                if (info.creditaricm == "S")
                {
                    if (info.totalmercado > 0)
                    {
                        preco = clsVisual.Truncar(((info.totalmercado - info.custoicm) / info.qtde), 6);
                    }
                }
                else
                {
                    if (info.totalmercado > 0)
                    {
                        preco = clsVisual.Truncar(((info.totalmercado) / info.qtde), 6);
                    }
                }
                String datarecebimento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select datarecebimento from nfcompra where id = " + clsParser.Int32Parse(info.numero.ToString()) + " ");

                movPecasBLL.MovimentaItem(info.idcodigo,
                                          "E",
                                          info.qtde,
                                          clsParser.DateTimeParse(datarecebimento),
                                          idtipodocumento,
                                          info.numero,
                                          result,
                                          0,
                                          preco,
                                          clsInfo.zusuario,
                                          cnn);
            }
            return result;
        }

        public override int Alterar(clsNfCompra1Info info, string cnn)
        {
            Int32 result = 0;

            result = base.Alterar(info, cnn);

            ComprasEntregaSicronizar(info.iditempedidoentrega, cnn);

            // Calcular o Preço Unitario para Entrada no Estoque
            if (info.creditaricm == "S")
            {
                if (info.totalmercado > 0)
                {
                    preco = clsVisual.Truncar(((info.totalmercado - info.custoicm) / info.qtde), 6);
                }
            }
            else
            {
                if (info.totalmercado > 0)
                {
                    preco = clsVisual.Truncar(((info.totalmercado) / info.qtde), 6);
                }
            }

            String datarecebimento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select datarecebimento from nfcompra where id = " + clsParser.Int32Parse(info.numero.ToString()) + " ");
            if (info.tipoproduto.Substring(0, 2) == "00")
            {
                movPecasBLL.MovimentaItem(info.qtde,
                                          "E",
                                          idtipodocumento,
                                          info.numero,
                                          info.id,
                                          0,
                                          preco,
                                          info.idcodigo,
                                          clsParser.DateTimeParse(datarecebimento),
                                          cnn);
            }
            else
            { // excluir se incluiu
                movPecasBLL.ExcluirDocumentoItem(idtipodocumento, info.numero, info.id, 0, cnn);
            }
            return result;
        }

        public override bool Excluir(int id, string cnn)
        {
            Boolean result = false;

            Int32 iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from nfcompra1 where id = " + id));

            using (TransactionScope tse = new TransactionScope())
            {
                Int32 iditempedidoentrega = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select iditempedidoentrega from nfcompra1 where id=" + id));
                Int32 idos = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idordemservico from nfcompra1 where id=" + id));

                if (idos != 0 && idos != clsInfo.zordemservico)
                {
                    throw new Exception("Não é possível excluir um item da NF de Entrada que está ligado a uma OS.");
                }

                result =  base.Excluir(id, cnn);

                ComprasEntregaSicronizar(iditempedidoentrega, cnn);

                movPecasBLL.ExcluirDocumentoItem(idtipodocumento,
                                                  iddocumento,
                                                  id,
                                                  0,
                                                  cnn);

                tse.Complete();
            }

            return result;
        }

        private void ComprasEntregaSicronizar(Int32 iditempedidoentrega, String cnn)
        {
            if (iditempedidoentrega != 0 && iditempedidoentrega != clsInfo.zcomprasentrega)
            {
                Decimal qtdefiscal_entregue = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(cnn, "select sum(qtdefiscal) from nfcompra1 where iditempedidoentrega=" + iditempedidoentrega));

                clsComprasEntregaInfo ComprasEntregaInfo;
                clsComprasEntregaBLL ComprasEntregaBLL = new clsComprasEntregaBLL();

                ComprasEntregaInfo = ComprasEntregaBLL.Carregar(iditempedidoentrega, cnn);
                ComprasEntregaInfo.qtdeentregue = qtdefiscal_entregue +
                                                    ComprasEntregaInfo.qtdebaixada +
                                                    ComprasEntregaInfo.qtdeosaux;
                ComprasEntregaInfo.qtdesaldo = ComprasEntregaInfo.qtdeentrega - ComprasEntregaInfo.qtdeentregue;

                if (ComprasEntregaInfo.qtdesaldo < 0)
                {
                    throw new Exception("Não é possível realizar a entrada do item do Pedido de Compra pois ele ficará com Saldo de Entrega negativo.");
                }

                ComprasEntregaBLL.Alterar(ComprasEntregaInfo, cnn);
            }
        }

        public static DataTable GridDados(Int32 idNFCompra, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "select " +
                        "NFCOMPRA1.ID, " +
                        "COMPRAS.NUMERO AS [PEDIDO], " +
                        "NFCOMPRA1.TIPOPRODUTO, " +
                        "CFOP.CFOP AS [CFOPFIS], " +
                        "UNIDADEFISCAL.CODIGO AS UNIDFISCAL, " +
                        "NFCOMPRA1.QTDEFISCAL, " +
                        "UNIDADE.CODIGO AS UNID, " +
                        "NFCOMPRA1.QTDE, " +
                        "PECAS.CODIGO, " +
//                        "PECAS.NOME, " +
                        "CASE WHEN PECAS.CODIGO = '0' THEN NFCOMPRA1.COMPLEMENTO " +
                        "WHEN NFCOMPRA1.COMPLEMENTO <> '' THEN PECAS.NOME + ' - ' + NFCOMPRA1.COMPLEMENTO " +
                        "ELSE PECAS.NOME END AS DESCRICAO, " +
                        "NFCOMPRA1.PRECO, " +
                        "NFCOMPRA1.TOTALMERCADO, " +
                        "NFCOMPRA1.IPI, " +
                        "NFCOMPRA1.CUSTOIPI, " +
                        "NFCOMPRA1.TOTALNOTA, " +
                        "NFCOMPRA1.BASEICM, " +
                        "NFCOMPRA1.CUSTOICM, " +
                        "NFCOMPRA1.PESOTOTAL, " +
                        "NFCOMPRA1.IRRF, " +
                        "NFCOMPRA1.INSS, " +
                        "NFCOMPRA1.PISCOFINSCSLL, " +
                        "NFCOMPRA1.PIS, " +
                        "NFCOMPRA1.COFINS, " +
                        "NFCOMPRA1.CSLL, " +
                        "NFCOMPRA1.COMPLEMENTO, " +
                        "NFCOMPRA1.COMPLEMENTO1, " +
                        "NFCOMPRA1.numero, " +
                        "NFCOMPRA1.idpedido, " +
                        "NFCOMPRA1.iditempedido, " +
                        "NFCOMPRA1.idcotacao, " +
                        "NFCOMPRA1.iditemcotacao, " +
                        "NFCOMPRA1.idsolicitacao, " +
                        "NFCOMPRA1.idpedidovenda, " +
                        "NFCOMPRA1.idpedidovendaitem, " +
                        "NFCOMPRA1.iditempedidoentrega, " +
                        "NFCOMPRA1.idordemservico, " +
                        "NFCOMPRA1.idcodigo, " +
                        "NFCOMPRA1.descricaoesp, " +
                        "NFCOMPRA1.msg, " +
                        "NFCOMPRA1.idsittriba, " +
                        "NFCOMPRA1.idsittribb, " +
                        "NFCOMPRA1.idcfop, " +
                        "NFCOMPRA1.consumo, " +
                        "NFCOMPRA1.situacao, " +
                        "NFCOMPRA1.idunidfiscal, " +
                        "NFCOMPRA1.idunid, " +
                        "NFCOMPRA1.fatorconv, " +
                        "NFCOMPRA1.idhistorico, " +
                        "NFCOMPRA1.idcentrocusto, " +
                        "NFCOMPRA1.idcodigoctabil, " +
                        "NFCOMPRA1.idipi, " +
                        "NFCOMPRA1.icm, " +
                        "NFCOMPRA1.basemp, " +
                        "NFCOMPRA1.baseicmsubst, " +
                        "NFCOMPRA1.icmsubst, " +
                        "NFCOMPRA1.reducao, " +
                        "NFCOMPRA1.peso, " +
                        "NFCOMPRA1.vidautil, " +
                        "NFCOMPRA1.pispasep, " +
                        "NFCOMPRA1.cofins1, " +
                        "NFCOMPRA1.irrfporc, " +
                        "NFCOMPRA1.inssporc, " +
                        "NFCOMPRA1.piscofinscsllporc, " +
                        "NFCOMPRA1.pisporc, " +
                        "NFCOMPRA1.cofinsporc, " +
                        "NFCOMPRA1.csllporc, " +
                        "NFCOMPRA1.issporc, " +
                        "NFCOMPRA1.iss, " +
                        "NFCOMPRA1.codigoemp01, " +
                        "NFCOMPRA1.codigoemp02, " +
                        "NFCOMPRA1.codigoemp03, " +
                        "NFCOMPRA1.codigoemp04, " +
                        "NFCOMPRA1.datatabela, " +
                        "NFCOMPRA1.idcodigo1, " +
                        "NFCOMPRA1.valorfrete, " +
                        "NFCOMPRA1.valorfreteicms, " +
                        "NFCOMPRA1.valorseguro, " +
                        "NFCOMPRA1.valorseguroicms, " +
                        "NFCOMPRA1.valoroutras, " +
                        "NFCOMPRA1.valoroutrasicms, " +
                        "NFCOMPRA1.calculoautomatico, " +
                        "NFCOMPRA1.CREDITARICM, " +
                        "NFCOMPRA1.IDDESTINO, " +
                        "NFCOMPRA1.TIPODESTINO, " +
                        "NFCOMPRA1.IDNOTAFISCAL, " +
                        "NFCOMPRA1.IDNFVENDAITEM, " +
                        "NFCOMPRA1.IDNFEVALES, " +
                        "NFCOMPRA1.QTDEDEV, " +
                        "NFCOMPRA1.QTDEDEVBAIXA, " +
                        "NFCOMPRA1.QTDEDEVSALDO, " +
                        "NFCOMPRA1.FATURANDO, " +
                        "NFCOMPRA1.FATURA, " +
                        "NFCOMPRA1.idcfopfis, " +
                        "NFCOMPRA1.IDTIPONOTASAIDA, " +
                        "PECAS.TIPOPRODUTO, " +
                        "NFCOMPRA1.IDIRRF, " +
                        "NFCOMPRA1.IDINSS, " +
                        "NFCOMPRA1.IDISS, " +
                        "NFCOMPRA1.IDPISCOFINSCSLL, " +
                        "NFCOMPRA1.IDPIS, " +
                        "NFCOMPRA1.IDCOFINS, " +
                        "NFCOMPRA1.IDCSLL, " +
                        "NFCOMPRA1.ICMINTERNO, " +
                        "NFCOMPRA1.SOMAPRODUTO, " +
                        "ORDEMSERVICO.NUMERO as [NROOS], " +
                        "PEDIDO.NUMERO AS [NROPV], " +
                        "NFCOMPRA1.idsittribcofins1, " +
                        "NFCOMPRA1.idsittribipi, " +
                        "NFCOMPRA1.Idsittribpis, " +
                        "NFCOMPRA1.ALIQCOFINS1, " +
                        "NFCOMPRA1.ALIQPISPASEP, " +
                        "NFCOMPRA1.BCCOFINS1, " +
                        "NFCOMPRA1.BCIPI, " +
                        "NFCOMPRA1.BCPISPASEP, " +
                        "NFCOMPRA1.ICMSSUBSTREDUCAO, " +
                        "NFCOMPRA1.IDSITTRIBCOFINS1, " +
                        "NFCOMPRA1.IDTIPONOTA, " +
                        "NFCOMPRA1.QTDETICKET, " +
                        "NFCOMPRA1.MARGEMLUCRO " +
                    "FROM  NFCOMPRA1 " +
                        "LEFT JOIN PECAS ON PECAS.ID = NFCOMPRA1.IDCODIGO " +
                        "LEFT JOIN UNIDADE AS UNIDADEFISCAL ON UNIDADEFISCAL.ID = NFCOMPRA1.IDUNIDFISCAL " +
                        "LEFT JOIN UNIDADE ON UNIDADE.ID = NFCOMPRA1.IDUNID " +
                        "LEFT JOIN COMPRAS ON COMPRAS.ID = NFCOMPRA1.IDPEDIDO " +
                        "LEFT JOIN CFOP ON CFOP.ID =NFCOMPRA1.IDCFOPFIS  " +
                        "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = NFCOMPRA1.IDORDEMSERVICO   " +
                        "LEFT JOIN PEDIDO ON PEDIDO.ID = NFCOMPRA1.IDPEDIDOVENDA ";
            
            filtro = "WHERE " +
                        "NFCOMPRA1.NUMERO = @IDNFCOMPRA ";

            query = query + filtro + " ORDER BY NFCOMPRA1.TIPOPRODUTO";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("IDNFCOMPRA", SqlDbType.Int).Value = idNFCompra;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("ID", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("P.C.", "PEDIDO", 40, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tp", "TIPOPRODUTO", 25, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("CFOP", "CFOPFIS", 0, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNIDFISCAL", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Fis", "QTDEFISCAL", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Un", "UNID", 25, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Int", "QTDE", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Codigo", "CODIGO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descrição", "DESCRICAO", 265, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Pr Unitário", "PRECO", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tt Mercadoria", "TOTALMERCADO", 90, true, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("IPI", "IPI:N0", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Custo IPI","CUSTOIPI", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Total Item", "TOTALNOTA", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Base Icm", "BASEICM", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Custo Icm", "CUSTOICM", 10, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Peso ", "PESOTOTAL", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Irrf", "IRRF", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Inss", "INSS", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("PisCofinsCsll", "PISCOFINSCSLL", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("PIS", "PIS", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Cofins", "COFINS", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("CSLL", "CSLL", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Complemento", "COMPLEMENTO", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Numero", "numero", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Pedido", "idpedido", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Item Pedido", "iditempedido", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Cotacao", "idcotacao", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Item Cotacao", "iditemcotacao", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Solicitacao", "idsolicitacao", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Ped.Venda", "idpedidovenda", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Ped. Venda Item", "idpedidovendaitem", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Ped Venda Item Prg", "iditempedidoentrega", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Ordem Servico", "idordemservico", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Codigo", "idcodigo", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("DescricaoEsp", "descricaoesp", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Msg", "msg", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Sit Triba", "idsittriba", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Sit Tribb", "idsittribb", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("IdCFOP", "idcfop", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Consumo", "consumo", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Situacao", "situacao", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Unid Fiscal", "idunidfiscal", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Unid", "idunid", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Fator Conv", "fatorconv", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Historico", "idhistorico", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id C.Custo",  "idcentrocusto", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id C.Contab",  "idcodigoctabil", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id IPI",  "idipi", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Icm",  "icm", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Base Mp",  "basemp", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Base Icm Subst",  "baseicmsubst", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Icm Subst",  "icmsubst", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Reducao",  "reducao", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Peso",  "peso", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Vida Util",  "vidautil", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Pispasep",  "pispasep", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Cofins 1",  "cofins1", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Irrf Porc",  "irrfporc", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Inss Porc",  "inssporc", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Pis Cofins Csll Porc",  "piscofinscsllporc", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Pis Porc",  "pisporc", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Cofins Porc",  "cofinsporc", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Csll Porc",  "csllporc", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Iss Porc",  "issporc", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("OS/Of",  "codigoemp01", 95, true, DataGridViewContentAlignment.MiddleLeft),
            //new GridColuna("Codigo Emp02",  "codigoemp02", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Codigo Emp03",  "codigoemp03", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Codigo Emp04",  "codigoemp04", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Dt Entrega",  "dataentrega", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Id Codigo1",  "idcodigo1", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Valor Frete",  "valorfrete", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("valor Frete Icms",  "valorfreteicms", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Valor Seguro",  "valorseguro", 0, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Valor Seguro Icms",  "valorseguroicms", 0, false, DataGridViewContentAlignment.MiddleRight),
            /*new GridColuna("Valor Outras",  "valoroutras", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Valor Outras Icms",  "valoroutrasicms", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Calculo Automatico",  "calculoautomatico", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Creditar ICM",  "creditaricm", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Complemento1",  "COMPLEMENTO1", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Destino",  "IDDESTINO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tipo Destino",  "TIPODESTINO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id NotaFiscal",  "IDNOTAFISCAL", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id NotaFiscal Item",  "IDNFVENDAITEM", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id NFE Vale",  "IDNFEVALES", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qtde Dev",  "QTDEDEV", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qtde Dev Baixa",  "QTDEDEVBAIXA", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qtde Dev Saldo",  "QTDEDEVSALDO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Faturando",  "FATURANDO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id CFOP Fis",  "IDCFOPFIS", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Tipo NFSaida",  "IDTIPONOTASAIDA", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tp Prod",  "TIPOPRODUTO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Irrf",  "IDIRRF", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Inss",  "IDINSS", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Iss",  "IDISS", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id PisCofinsCsll",  "IDPISCOFINSCSLL", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Pis",  "IDPIS", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Cofins",  "IDCOFINS", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Csll",  "IDCSLL", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Icm Interno",  "ICMINTERNO", 0, false, DataGridViewContentAlignment.MiddleRight),
            */
            //new GridColuna("O.S.",  "NROOS", 40, true, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("P.V.",  "NROPV", 40, true, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("SomaProduto",  "SOMAPRODUTO", 40, false, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Fatura",  "FATURA", 0, false, DataGridViewContentAlignment.MiddleRight),
        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
        }
    

        // No cadastro de peças
        public static DataTable GridDadosPeca(Int32 idCodigo, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            // criei uma query que vai servir para os itens e também no cadastro de peças
            query = "SELECT NFCOMPRA1.ID AS [IDNFE1], NFCOMPRA.ID AS [IDNFE], NFCOMPRA.NUMERO, NFCOMPRA.DATA, NFCOMPRA.DATARECEBIMENTO,  NFCOMPRA1.situacao, NFCOMPRA1.TIPOPRODUTO, CFOP.CFOP AS [CFOPFIS], CLIENTE.COGNOME AS [FORNECEDOR], NFCOMPRA1.QTDEFISCAL, " +
            " UNIDADEFISCAL.CODIGO AS UNIDFISCAL, COMPRAS.NUMERO AS [PEDIDO],  UNIDADE.CODIGO AS UNID, NFCOMPRA1.QTDE, " +
            "PECAS.CODIGO,PECAS.NOME,NFCOMPRA1.PRECO,NFCOMPRA1.TOTALMERCADO, NFCOMPRA1.IPI,  " +
            "NFCOMPRA1.CUSTOIPI ,NFCOMPRA1.TOTALNOTA , NFCOMPRA1.BASEICM ,NFCOMPRA1.CUSTOICM , NFCOMPRA1.PESOTOTAL,  " +
            "NFCOMPRA1.IRRF, NFCOMPRA1.INSS , NFCOMPRA1.PISCOFINSCSLL , NFCOMPRA1.PIS , NFCOMPRA1.COFINS , NFCOMPRA1.CSLL,NFCOMPRA1.COMPLEMENTO,NFCOMPRA1.COMPLEMENTO1,  " +
            "NFCOMPRA1.numero,NFCOMPRA1.idpedido,NFCOMPRA1.iditempedido,NFCOMPRA1.idcotacao,NFCOMPRA1.iditemcotacao,NFCOMPRA1.idsolicitacao,NFCOMPRA1.idpedidovenda,NFCOMPRA1.idpedidovendaitem,NFCOMPRA1.iditempedidoentrega,  " +
            "NFCOMPRA1.idordemservico,NFCOMPRA1.idcodigo,NFCOMPRA1.descricaoesp,NFCOMPRA1.msg,NFCOMPRA1.idsittriba,NFCOMPRA1.idsittribb,NFCOMPRA1.idcfop,  " +
            "NFCOMPRA1.consumo,NFCOMPRA1.idunidfiscal,NFCOMPRA1.idunid,NFCOMPRA1.fatorconv,NFCOMPRA1.idhistorico,NFCOMPRA1.idcentrocusto,  " +
            "NFCOMPRA1.idcodigoctabil,NFCOMPRA1.idipi,NFCOMPRA1.icm,NFCOMPRA1.basemp,NFCOMPRA1.baseicmsubst,NFCOMPRA1.icmsubst,  " +
            "NFCOMPRA1.reducao,NFCOMPRA1.peso,NFCOMPRA1.vidautil,NFCOMPRA1.pispasep,NFCOMPRA1.cofins1,NFCOMPRA1.irrfporc,NFCOMPRA1.inssporc,  " +
            "NFCOMPRA1.piscofinscsllporc,NFCOMPRA1.pisporc,NFCOMPRA1.cofinsporc,NFCOMPRA1.csllporc,NFCOMPRA1.issporc,NFCOMPRA1.iss,  " +
            "NFCOMPRA1.codigoemp01,NFCOMPRA1.codigoemp02,NFCOMPRA1.codigoemp03,NFCOMPRA1.codigoemp04,NFCOMPRA1.idcodigo1,NFCOMPRA1.valorfrete,NFCOMPRA1.valorfreteicms,  " +
            "NFCOMPRA1.valorseguro,NFCOMPRA1.valorseguroicms,NFCOMPRA1.valoroutras,NFCOMPRA1.valoroutrasicms,NFCOMPRA1.calculoautomatico,NFCOMPRA1.CREDITARICM,NFCOMPRA1.IDDESTINO,NFCOMPRA1.TIPODESTINO,NFCOMPRA1.IDNOTAFISCAL,  " +
            "NFCOMPRA1.IDNFVENDAITEM,NFCOMPRA1.IDNFEVALES,NFCOMPRA1.QTDEDEV, NFCOMPRA1.QTDEDEVBAIXA, NFCOMPRA1.QTDEDEVSALDO, NFCOMPRA1.FATURANDO, NFCOMPRA1.idcfopfis, NFCOMPRA1.IDTIPONOTASAIDA, PECAS.TIPOPRODUTO, NFCOMPRA1.IDIRRF,  " +
            "NFCOMPRA1.IDINSS, NFCOMPRA1.IDISS, NFCOMPRA1.IDPISCOFINSCSLL, NFCOMPRA1.IDPIS, NFCOMPRA1.IDCOFINS, NFCOMPRA1.IDCSLL, NFCOMPRA1.ICMINTERNO,    " +
            "ORDEMSERVICO.NUMERO as [NROOS], PEDIDO.NUMERO AS [NROPV],  " +
            "NFCOMPRA1.idsittribcofins1, NFCOMPRA1.idsittribipi, NFCOMPRA1.Idsittribpis, " +
            "NFCOMPRA1.ALIQCOFINS1, NFCOMPRA1.ALIQPISPASEP, NFCOMPRA1.BCCOFINS1, NFCOMPRA1.BCIPI, NFCOMPRA1.BCPISPASEP, " +
            "NFCOMPRA1.ICMSSUBSTREDUCAO, NFCOMPRA1.IDSITTRIBCOFINS1,  NFCOMPRA1.IDTIPONOTA, NFCOMPRA1.QTDETICKET " +
            "FROM NFCOMPRA1 " +
            "INNER JOIN NFCOMPRA ON NFCOMPRA1.NUMERO = NFCOMPRA.ID " +
            "LEFT JOIN PECAS ON PECAS.ID = NFCOMPRA1.IDCODIGO   " +
            "LEFT JOIN UNIDADE AS UNIDADEFISCAL ON UNIDADEFISCAL.ID = NFCOMPRA1.IDUNIDFISCAL   " +
            "LEFT JOIN UNIDADE ON UNIDADE.ID = NFCOMPRA1.IDUNID   " +
            "LEFT JOIN COMPRAS ON COMPRAS.ID = NFCOMPRA1.IDPEDIDO   " +
            "LEFT JOIN CFOP ON CFOP.ID =NFCOMPRA1.IDCFOPFIS   " +
            "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = NFCOMPRA1.IDORDEMSERVICO   " +
            "LEFT JOIN PEDIDO ON PEDIDO.ID = NFCOMPRA1.IDPEDIDOVENDA " +
            "LEFT JOIN CLIENTE ON CLIENTE.ID = NFCOMPRA.IDFORNECEDOR " +
            "WHERE NFCOMPRA1.IDCODIGO=" + idCodigo;
            query = query + " " + filtro + " ORDER BY NFCOMPRA.DATARECEBIMENTO DESC, NFCOMPRA1.TIPOPRODUTO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static DataTable GridDadosPeca(Int32 idCodigo, Int32 nRegistros, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            // criei uma query que vai servir para os itens e também no cadastro de peças
            query = "SELECT top " + nRegistros + " " +
                        "NFCOMPRA1.ID AS [IDNFE1], " +
                        "NFCOMPRA.ID AS [IDNFE], " +
                        "NFCOMPRA.NUMERO, " +
                        "NFCOMPRA.DATA, " +
                        "NFCOMPRA.DATARECEBIMENTO, " +
                        "CLIENTE.COGNOME AS [FORNECEDOR], " +
                        "NFCOMPRA1.TIPOPRODUTO, " +
                        "CFOP.CFOP AS [CFOPFIS], " +
                        "UNIDADEFISCAL.CODIGO AS UNIDFISCAL, " +
                        "NFCOMPRA1.QTDEFISCAL, " +
                        "UNIDADE.CODIGO AS UNID, " +
                        "NFCOMPRA1.QTDE, " +
                        "NFCOMPRA1.PRECO, " +
                        "NFCOMPRA1.TOTALMERCADO, " +
                        "NFCOMPRA1.CUSTOIPI, " +
                        "NFCOMPRA1.TOTALNOTA, " +
                        "NFCOMPRA1.CUSTOICM, " +
                        "NFCOMPRA1.situacao " +
            "FROM NFCOMPRA1 " +
                "INNER JOIN NFCOMPRA ON NFCOMPRA1.NUMERO = NFCOMPRA.ID " +
                "LEFT JOIN UNIDADE AS UNIDADEFISCAL ON UNIDADEFISCAL.ID = NFCOMPRA1.IDUNIDFISCAL   " +
                "LEFT JOIN UNIDADE ON UNIDADE.ID = NFCOMPRA1.IDUNID   " +
                "LEFT JOIN CFOP ON CFOP.ID =NFCOMPRA1.IDCFOPFIS   " +
                "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = NFCOMPRA1.IDORDEMSERVICO   " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = NFCOMPRA.IDFORNECEDOR " +
            "WHERE NFCOMPRA1.IDCODIGO=" + idCodigo;
            query = query + " " + filtro + " ORDER BY NFCOMPRA.DATARECEBIMENTO DESC, NFCOMPRA1.TIPOPRODUTO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunasPeca = new GridColuna[]
        {
            new GridColuna("id", "IDNFE1", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Nro NF", "NUMERO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Dt Emissao", "DATA", 80, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Dt Recebeu", "DATARECEBIMENTO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Sit.", "SITUACAO", 15, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Tp", "TIPOPRODUTO", 25, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("CFOP", "CFOPFIS", 35, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Fornecedor", "FORNECEDOR", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qtde", "QTDEFISCAL", 55, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNIDFISCAL", 35, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Preço Unit", "PRECO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Valor Mercad", "TOTALMERCADO", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Valor Ipi", "CUSTOIPI", 75, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Valor Total", "TOTALNOTA", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Vl ICM", "CUSTOICM", 75, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMontaPeca(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasPeca, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DATA");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["DATARECEBIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["QTDEFISCAL"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PRECO"].DefaultCellStyle.Format = "N4";
            dgv.Columns["TOTALMERCADO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["CUSTOIPI"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
            dgv.Columns["CUSTOICM"].DefaultCellStyle.Format = "N2";
        }
        // Detalhando mais a Peça
        public static DataTable GridDadosPeca1(Int32 Mes, Int32 Ano, Int32 Filial, String TipoLista)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            // criei uma query que vai servir para os itens e também no cadastro de peças
            query = "SELECT NFCOMPRA1.ID AS [IDNFE1], " +
                        "NFCOMPRA.ID AS [IDNFE], " +
                        "NFCOMPRA.NUMERO, " +
                        "NFCOMPRA.DATA, " +
                        "NFCOMPRA.DATARECEBIMENTO, " +
                        "CLIENTE.COGNOME AS [FORNECEDOR], " +
                        "NFCOMPRA1.TIPOPRODUTO, " +
                        "PECAS.CODIGO, " +
                        "PECAS.NOME, " +
                        "NFCOMPRA1.COMPLEMENTO, " +
                        "CFOP.CFOP AS [CFOPFIS], " +
                        "UNIDADEFISCAL.CODIGO AS UNIDFISCAL, " +
                        "NFCOMPRA1.QTDEFISCAL, " +
                        "UNIDADE.CODIGO AS UNID, " +
                        "NFCOMPRA1.QTDE, " +
                        "NFCOMPRA1.PRECO, " +
                        "NFCOMPRA1.TOTALMERCADO, " +
                        "NFCOMPRA1.CUSTOIPI, " +
                        "NFCOMPRA1.TOTALNOTA, " +
                        "NFCOMPRA1.CUSTOICM, " +
                        "NFCOMPRA1.situacao, " +
                        "PECASCLASSIFICA.CODIGO AS CLASSIFICA " +
            "FROM NFCOMPRA1 " +
                "INNER JOIN NFCOMPRA ON NFCOMPRA1.NUMERO = NFCOMPRA.ID " +
                "LEFT JOIN UNIDADE AS UNIDADEFISCAL ON UNIDADEFISCAL.ID = NFCOMPRA1.IDUNIDFISCAL   " +
                "LEFT JOIN UNIDADE ON UNIDADE.ID = NFCOMPRA1.IDUNID   " +
                "LEFT JOIN CFOP ON CFOP.ID =NFCOMPRA1.IDCFOPFIS   " +
                "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = NFCOMPRA1.IDORDEMSERVICO   " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = NFCOMPRA.IDFORNECEDOR " +
                "LEFT JOIN PECAS ON PECAS.ID = NFCOMPRA1.IDCODIGO " +
                "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA ";
            //"WHERE NFCOMPRA1.IDCODIGO=" + idCodigo;
            if (TipoLista != "T")
            {
                // SE MES FOR 0 (ZERO) TRAZ TUDO DO ANO
                if (Mes != 0)
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + " MONTH(NFCOMPRA.DATA) = @MES ";
                }
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + " YEAR(NFCOMPRA.DATA) >= @ANO ";
            }
            if (filtro.Length > 2)
            {
                filtro = "WHERE " + filtro;
            }

            query = query + " " + filtro + " ORDER BY NFCOMPRA.DATARECEBIMENTO DESC, NFCOMPRA1.TIPOPRODUTO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@MES", SqlDbType.Int).Value = Mes;
            sda.SelectCommand.Parameters.Add("@ANO", SqlDbType.Int).Value = Ano;
            sda.SelectCommand.Parameters.Add("@Filial", SqlDbType.Int).Value = Filial;

            dt = new DataTable();

            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunasPeca1 = new GridColuna[]
        {
            new GridColuna("id", "IDNFE1", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Nro NF", "NUMERO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Dt Emissao", "DATA", 60, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("Dt Recebeu", "DATARECEBIMENTO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("Sit.", "SITUACAO", 15, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("Tp", "TIPOENTRADA", 25, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("CFOP", "CFOPFIS", 35, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Fornecedor", "FORNECEDOR", 70, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Codigo", "CODIGO", 55, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 155, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Complemento", "COMPLEMENTO", 225, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qtde", "QTDEFISCAL", 55, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNIDFISCAL", 35, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Preço Unit", "PRECO", 80, true, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Valor Mercad", "TOTALMERCADO", 90, true, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Valor Ipi", "CUSTOIPI", 75, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Valor Total", "TOTALNOTA", 80, true, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Vl ICM", "CUSTOICM", 75, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Class", "CLASSIFICA", 30, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMontaPeca1(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasPeca1, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DATA");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["DATARECEBIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["QTDEFISCAL"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PRECO"].DefaultCellStyle.Format = "N4";
            dgv.Columns["TOTALMERCADO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["CUSTOIPI"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
            dgv.Columns["CUSTOICM"].DefaultCellStyle.Format = "N2";
        }

        public static void SicronizarQtdeSaida(Int32 nfcompra1_id, String conexao)
        {
            String query;

            SqlConnection scn;
            SqlCommand scd;

            scn = new SqlConnection(conexao);

            Decimal qtdedev_entrada = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(conexao, "select SUM(nfvenda1.QTDE) from nfvenda1 inner join nfvenda on nfvenda1.numero = nfvenda.id where nfvenda1.iditemnfcompra = " + nfcompra1_id + " and (cast(nfvenda1.tipoitem as int) = 10 or (cast(nfvenda1.tipoitem as int) >= 20 and cast(nfvenda1.tipoitem as int) < 36)) and (nfvenda.tipoentrada='ED' or nfvenda.tipoentrada = 'EF')"));

            Decimal qtdedev = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(conexao, "select SUM(nfvenda1.QTDE) from nfvenda1 inner join nfvenda on nfvenda1.numero = nfvenda.id where nfvenda1.iditemnfcompra = " + nfcompra1_id + " and (cast(nfvenda1.tipoitem as int) = 10 or (cast(nfvenda1.tipoitem as int) >= 20 and cast(nfvenda1.tipoitem as int) < 36)) and (nfvenda.tipoentrada='SF' or nfvenda.tipoentrada='SD')"));

            qtdedev = qtdedev - qtdedev_entrada;

            query = "update nfcompra1 set qtdedev=@qtdedev, qtdedevsaldo=qtdefiscal-(@qtdedev + qtdedevbaixa) where id=@id and id<>@idpadrao";
            scd = new SqlCommand(query, scn);
            scd.Parameters.Add("@id", SqlDbType.Int).Value = nfcompra1_id;
            scd.Parameters.Add("@idpadrao", SqlDbType.Int).Value = clsInfo.znfcompra1;
            scd.Parameters.Add("@qtdedev", SqlDbType.Decimal).Value = qtdedev;

            scn.Open();

            scd.ExecuteNonQuery();

            scn.Close();
        }
    }
 
}
