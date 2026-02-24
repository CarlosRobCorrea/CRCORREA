using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsCompras1BLL : SQLFactory<clsCompras1Info>
    {

        public static DataTable GridDados(Int32 idCompras)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "select " +
                        "COMPRAS1.ID, " +
                        "COMPRAS1.TIPOENTRADA, " +
                        "CFOP.CFOP,  " +
                        "UNIDADE_FISCAL.CODIGO AS [UNIDFISCAL], " +
                        "COMPRAS1.QTDEFISCAL,  " +
                        "UNIDADE.CODIGO AS [UNID], " +
                        "COMPRAS1.QTDE, " +
                        "COMPRAS1.QTDESALDO, " +
                        "PECAS.CODIGO,  " +
                        "CASE WHEN PECAS.CODIGO = '0' THEN COMPRAS1.COMPLEMENTO " +
                        "WHEN COMPRAS1.COMPLEMENTO <> '' THEN PECAS.NOME + ' - ' + COMPRAS1.COMPLEMENTO " +
                        "ELSE PECAS.NOME END AS PECASDESCRI, " +
                        "COMPRAS1.PRECO, " +
                        "COMPRAS1.TOTALMERCADO, " +
                        "COMPRAS1.CUSTOIPI, " +
                        "COMPRAS1.TOTALNOTA, " +
                        "COMPRAS1.TOTALPREVISTO, " +
                        "COMPRAS1.BASEICM, " +
                        "COMPRAS1.BASEICMSUBST, " +
                        "COMPRAS1.BASEMP, " +
                        "COMPRAS1.CALCULOAUTOMATICO, " +
                        "COMPRAS1.CODIGOEMP01, " +
                        "COMPRAS1.CODIGOEMP02, " +
                        "COMPRAS1.CODIGOEMP03, " +
                        "COMPRAS1.CODIGOEMP04, " +
                        "COMPRAS1.COFINS, " +
                        "COMPRAS1.COFINS1, " +
                        "COMPRAS1.COFINSPORC, " +
                        "COMPRAS1.COMPLEMENTO, " +
                        "COMPRAS1.COMPLEMENTO1, " +
                        "COMPRAS1.CONSUMO, " +
                        "COMPRAS1.CREDITARICM, " +
                        "COMPRAS1.CSLL, " +
                        "COMPRAS1.CSLLPORC, " +
                        "COMPRAS1.CUSTOICM, " +
                        "COMPRAS1.DESCONTO, " +
                        "COMPRAS1.DESCRICAO, " +
                        "COMPRAS1.DESCRICAOESP, " +
                        "COMPRAS1.FATORCONV, " +
                        "COMPRAS1.ICM, " +
                        "COMPRAS1.ICMINTERNO, " +
                        "COMPRAS1.ICMSUBST, " +
                        "COMPRAS1.IDCENTROCUSTO, " +
                        "COMPRAS1.IDCFOP, " +
                        "COMPRAS1.IDCFOPFIS, " +
                        "COMPRAS1.IDCODIGO, " +
                        "COMPRAS1.IDCODIGO1, " +
                        "COMPRAS1.IDCODIGOCTABIL, " +
                        "COMPRAS1.IDCOMPRAS, " +
                        "COMPRAS1.IDCOTACAO, " +
                        "COMPRAS1.IDCOTACAOITEM, " +
                        "COMPRAS1.IDCOTACAO2, " +
                        "COMPRAS1.IDDESTINO, " +
                        "COMPRAS1.IDHISTORICO, " +
                        "COMPRAS1.IDIPI, " +
                        "COMPRAS1.IDOS, " +
                        "COMPRAS1.IDSITTRIBA, " +
                        "COMPRAS1.IDSITTRIBB, " +
                        "COMPRAS1.IDSOLICITACAO, " +
                        "COMPRAS1.IDTIPONOTA, " +
                        "COMPRAS1.IDUNID, " +
                        "COMPRAS1.IDUNIDFISCAL, " +
                        "COMPRAS1.INSS, " +
                        "COMPRAS1.INSSPORC, " +
                        "COMPRAS1.IPI, " +
                        "COMPRAS1.IRRF, " +
                        "COMPRAS1.IRRFPORC, " +
                        "COMPRAS1.ISS, " +
                        "COMPRAS1.ISSPORC, " +
                        "COMPRAS1.MSG, " +
                        "COMPRAS1.OBSERVAR, " +
                        "COMPRAS1.PESO, " +
                        "COMPRAS1.PIS, " +
                        "COMPRAS1.PISCOFINSCSLL, " +
                        "COMPRAS1.PISCOFINSCSLLPORC, " +
                        "COMPRAS1.PISPASEP, " +
                        "COMPRAS1.PISPORC, " +
                        "COMPRAS1.PRECOBRUTO, " +
                        "COMPRAS1.QTDEBAIXADA, " +
                        "COMPRAS1.QTDEDEFEITO, " +
                        "COMPRAS1.QTDEENTREGUE, " +
                        "COMPRAS1.QTDEFISCAL, " +
                        "COMPRAS1.QTDEOSAUX, " +
                        "COMPRAS1.QTDESUCATA, " +
                        "COMPRAS1.QTDETOTAL, " +
                        "COMPRAS1.QTDEENTRA, " +
                        "COMPRAS1.QTDETRANSFE, " +
                        "COMPRAS1.REDUCAO, " +
                        "COMPRAS1.TERMINO, " +
                        "COMPRAS1.TIPODESTINO, " +
                        "COMPRAS1.TOTALPESO, " +
                        "COMPRAS1.VALORDESCONTO, " +
                        "COMPRAS1.VALORFRETE, " +
                        "COMPRAS1.VALORFRETEICMS, " +
                        "COMPRAS1.VALOROUTRAS, " +
                        "COMPRAS1.VALOROUTRASICMS, " +
                        "COMPRAS1.VALORSEGURO, " +
                        "COMPRAS1.VALORSEGUROICMS, " +
                        "COMPRAS1.ALIQPISPASEP, " +
                        "COMPRAS1.BCPISPASEP, " +
                        "COMPRAS1.ALIQCOFINS1, " +
                        "COMPRAS1.BCCOFINS1, " +
                        "COMPRAS1.BCIPI, " +
                        "COMPRAS1.IDSITTRIBIPI, " +
                        "COMPRAS1.IDSITTRIBPIS, " +
                        "COMPRAS1.IDSITTRIBCOFINS1 " +
                        //"COMPRAS1.APROVADO, " +
                        //"COMPRAS1.NRO " +
                    "FROM COMPRAS1  " +
                        "LEFT JOIN CFOP ON CFOP.ID = COMPRAS1.IDCFOPFIS " +
                        "LEFT JOIN PECAS ON PECAS.ID=COMPRAS1.IDCODIGO " +
                        "LEFT JOIN UNIDADE ON UNIDADE.ID=COMPRAS1.IDUNID   " +
                        "LEFT JOIN UNIDADE AS UNIDADE_FISCAL ON UNIDADE_FISCAL.ID=COMPRAS1.IDUNIDFISCAL ";
            filtro = " WHERE COMPRAS1.IDCOMPRAS = @IDCOMPRAS ";
            filtro = filtro + " ORDER BY PECASDESCRI  ";

            query = query + " " + filtro;

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("IDCOMPRAS", SqlDbType.Int).Value = idCompras;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("ID", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tp", "TIPOENTRADA", 25, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cfop", "CFOP", 30, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Unid", "UNIDFISCAL", 30, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Fis", "QTDEFISCAL", 65, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Un", "UNID", 25, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Int", "QTDE", 65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt Saldo", "QTDESALDO", 65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Codigo", "CODIGO", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descrição", "PECASDESCRI", 335, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("P.Unitário", "PRECO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("T.Mercadoria", "TOTALMERCADO", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Custo IPI","CUSTOIPI", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total Item", "TOTALNOTA", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tt Previsto", "TOTALPREVISTO", 90, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
            dgv.Columns["QTDEFISCAL"].DefaultCellStyle.Format = "N3";
            dgv.Columns["QTDE"].DefaultCellStyle.Format = "N3";
            dgv.Columns["QTDESALDO"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PRECO"].DefaultCellStyle.Format = "N6";
            dgv.Columns["TOTALMERCADO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["CUSTOIPI"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALPREVISTO"].DefaultCellStyle.Format = "N2";
        }
        ////////////////////   compras pendentes por fornecedor
        //public static DataTable GridDadosPendencia(Int32 idFornecedor)
        //{
        //    DataTable dt;
        //    String query = "";
        //    String filtro = "";
        //    SqlDataAdapter sda;
        //    query = "SELECT COMPRASENTREGA.ID, COMPRASENTREGA.IDCOMPRAS, COMPRASENTREGA.IDCOMPRAS1, " +
        //    "COMPRAS.NUMERO, COMPRASENTREGA.DATAENTREGA , COMPRASENTREGA.QTDESALDO, UNIDADE.CODIGO AS [UNID], " +
        //    "PECAS.CODIGO AS [CODITEM], " +
        //    "CASE WHEN PECAS.CODIGO = '0' THEN COMPRAS1.COMPLEMENTO " +
        //    "WHEN COMPRAS1.COMPLEMENTO <> '' THEN PECAS.NOME + ' - ' + COMPRAS1.COMPLEMENTO " +
        //    "ELSE PECAS.NOME END AS DESCRICAO, COMPRAS1.PRECO " +
        //    "FROM COMPRAS1 " +
        //    "LEFT JOIN COMPRASENTREGA ON COMPRASENTREGA.IDCOMPRAS1 = COMPRAS1.ID " +
        //    "LEFT JOIN COMPRAS ON COMPRAS.ID = COMPRAS1.IDCOMPRAS " +
        //    "LEFT JOIN PECAS ON PECAS.ID = COMPRAS1.IDCODIGO " +
        //    "LEFT JOIN UNIDADE ON UNIDADE.ID = COMPRAS1.IDUNIDFISCAL ";
        //    filtro = " WHERE COMPRASENTREGA.QTDESALDO > 0 AND COMPRAS.IDFORNECEDOR = @IDFORNECEDOR  AND COMPRAS.FILIAL = @FILIAL ";
        //    filtro = filtro + " ORDER BY PECAS.CODIGO, COMPRASENTREGA.DATAENTREGA ";
        //    query = query + " " + filtro;
        //    sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
        //    sda.SelectCommand.Parameters.Add("IDFORNECEDOR", SqlDbType.Int).Value = idFornecedor;
        //    sda.SelectCommand.Parameters.Add("FILIAL", SqlDbType.Int).Value = clsInfo.zfilial;
        //    dt = new DataTable();
        //    sda.Fill(dt);
        //    return dt;
        //}
        ////////////////////   compras pendentes por fornecedor
        public static DataTable GridDadosPendencia(Int32 idFornecedor, Int32 iddiferentedestepedido)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT COMPRASENTREGA.ID, COMPRASENTREGA.IDCOMPRAS, COMPRASENTREGA.IDCOMPRAS1, " +
            "COMPRAS.NUMERO, COMPRASENTREGA.DATAENTREGA , COMPRASENTREGA.QTDESALDO, UNIDADE.CODIGO AS [UNID], " +
            "PECAS.CODIGO AS [CODITEM], " +
            "CASE WHEN PECAS.CODIGO = '0' THEN COMPRAS1.COMPLEMENTO " +
            "WHEN COMPRAS1.COMPLEMENTO <> '' THEN PECAS.NOME + ' - ' + COMPRAS1.COMPLEMENTO " +
            "ELSE PECAS.NOME END AS DESCRICAO, COMPRAS1.PRECO " +
            "FROM COMPRAS1 " +
            "LEFT JOIN COMPRASENTREGA ON COMPRASENTREGA.IDCOMPRAS1 = COMPRAS1.ID " +
            "LEFT JOIN COMPRAS ON COMPRAS.ID = COMPRAS1.IDCOMPRAS " +
            "LEFT JOIN PECAS ON PECAS.ID = COMPRAS1.IDCODIGO " +
            "LEFT JOIN UNIDADE ON UNIDADE.ID = COMPRAS1.IDUNIDFISCAL ";
            filtro = " WHERE COMPRASENTREGA.QTDESALDO > 0 AND COMPRAS.IDFORNECEDOR = @IDFORNECEDOR  AND COMPRAS.FILIAL = @FILIAL " +
                     " AND COMPRAS.ID <> @IDDIFERENTEDESTEPEDIDO ";
            filtro = filtro + " ORDER BY PECAS.CODIGO, COMPRASENTREGA.DATAENTREGA ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("IDFORNECEDOR", SqlDbType.Int).Value = idFornecedor;
            sda.SelectCommand.Parameters.Add("FILIAL", SqlDbType.Int).Value = clsInfo.zfilial;
            sda.SelectCommand.Parameters.Add("IDDIFERENTEDESTEPEDIDO", SqlDbType.Int).Value = iddiferentedestepedido;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunasPendencia = new GridColuna[]
        {
            new GridColuna("ID", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("ID", "IDCOMPRAS", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nro Ped", "NUMERO", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Dt. Entrega", "DATAENTREGA", 65, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qtde Saldo", "QTDESALDO", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNID", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Codigo", "CODITEM", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descrição", "DESCRICAO", 325, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Preço Unit", "PRECO", 85, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMontaPendencia(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasPendencia, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["DATAENTREGA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["QTDESALDO"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PRECO"].DefaultCellStyle.Format = "N6";
        }
    }
}
