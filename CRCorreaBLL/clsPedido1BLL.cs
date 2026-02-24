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
    public class clsPedido1BLL : SQLFactory<clsPedido1Info>
    {
        Int32 idtipodocumento;
        clsMovPecasBLL movPecasBLL = new clsMovPecasBLL();

        public clsPedido1BLL()
        {
            movPecasBLL = new clsMovPecasBLL();
            idtipodocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select top 1 id from docfiscal where cognome = 'PED'"));
        }
        public override int Incluir(clsPedido1Info info, string cnn)
        {
            VerificaInfo(info);

            Int32 result = 0;

            result = base.Incluir(info, cnn);

            String data = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from pedido where id = "
            + clsParser.Int32Parse(info.idpedido.ToString()) + " ");

            movPecasBLL = new clsMovPecasBLL();
            movPecasBLL.MovimentaItem(info.idcodigo,
                                      "S",
                                      info.qtde,
                                      clsParser.DateTimeParse(data),
                                      idtipodocumento,
                                      info.idpedido,
                                      result,
                                      0,
                                      info.preco,
                                      clsInfo.zusuario,
                                      cnn);

            return result;
        }

        public override int Alterar(clsPedido1Info info, string cnn)
        {
            Int32 result;

            VerificaInfo(info);

            result = base.Alterar(info, cnn);

            String data = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from pedido where id = " + clsParser.Int32Parse(info.idpedido.ToString()) + " ");

            movPecasBLL.MovimentaItem(info.qtde,
                                      "S",
                                      idtipodocumento,
                                      info.idpedido,
                                      info.id,
                                      0,
                                      info.preco,
                                      info.idcodigo,
                                      clsParser.DateTimeParse(data),
                                      cnn);

            return result;
        }

        public override bool Excluir(int id, string cnn)
        {
            Boolean result = false;

            Int32 iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select numero from nfvenda1 where id = " + id));

            result = base.Excluir(id, cnn);

            movPecasBLL.ExcluirDocumentoItem(idtipodocumento,
                                              iddocumento,
                                              id,
                                              0,
                                              cnn);

            return result;
        }


        public void VerificaInfo(clsPedido1Info _info)
        {
            

            if (_info.idcodigo <= 0)
                throw new Exception("Produto inválido.");
            //else if (_info.idcfop <= 0)
            //    throw new Exception("CFOP inválido.");
            else if (_info.idipi <= 1)
                throw new Exception("Classificação Fiscal/ NCM inválida.");
            //else if (_info.idordemservico <= 0)
            //    throw new Exception("Ordem de Serviço Inválida.");
            //else if (_info.idsittriba <= 0)
            //    throw new Exception("Situação Tributária de Origem inválida.");
            //else if (_info.idsittribb <= 0)
            //    throw new Exception("Situação Tributária de ICMS inválida.");
            //else if (_info.idsittribcofins <= 0)
            //    throw new Exception("Situação Tributária de COFINS inválida.");
            //else if (_info.idsittribipi <= 0)
            //    throw new Exception("Situação Tributária de IPI inválida.");
            //else if (_info.idsittribpispasep <= 0)
            //    throw new Exception("Situação Tributária de PisPasep inválida.");
            //else if (_info.idtiponota <= 0)
            //    throw new Exception("Tipo da Nota Inválida.");
            else if (_info.idunidade <= 0)
                throw new Exception("Unidade do Produto inválida.");
            
            
            if (_info.totalnota == 0)
            {
                throw new Exception("Pedido sem valor o sistema não aceita !!!! ");
            }

        }
        public static DataTable GridDados(Int32 idpedido, String cnn)
        {
            DataTable dt;
            String query = "";
            //String filtro = "";
            SqlDataAdapter sda;
            query = " " +
            "SELECT PEDIDO1.ID ,PEDIDO1.IDPEDIDO , " + //PEDIDO1.ITEM ,
            "PEDIDO1.IDTIPONOTA ,TIPONOTA.CODIGO AS [TIPCOD], TIPONOTA.NOME AS [TIPONOTA]," +
            "PEDIDO1.CONSUMO ,PEDIDO1.IDCFOP , CFOP.CFOP, " +
            "PEDIDO1.IDCODIGO ,PECAS.CODIGO AS [CODIGO], " +
            "CASE WHEN PECAS.CODIGO = '0' THEN PEDIDO1.COMPLEMENTO " +
            "WHEN PEDIDO1.COMPLEMENTO <> '' THEN PECAS.NOME + ' - ' + PEDIDO1.COMPLEMENTO " +
            "ELSE PECAS.NOME END AS DESCRICAO, " +
            "PEDIDO1.COMPLEMENTO, " +
            "PEDIDO1.QTDE ,PEDIDO1.IDUNIDADE , UNIDADE.CODIGO AS [UNID], " +
            "PEDIDO1.QTDEENTREGUE ,PEDIDO1.QTDEDEFEITO ," +
            "PEDIDO1.QTDESUCATA ,PEDIDO1.QTDEBAIXADA ,PEDIDO1.QTDEOSAUX ,PEDIDO1.QTDESALDO ,PEDIDO1.PRECO ,PEDIDO1.PRECOTABELA ,PEDIDO1.PRECODESCONTO ," +
            "PEDIDO1.TOTALMERCADO ,PEDIDO1.IDIPI ,PEDIDO1.IPI ,PEDIDO1.BCIPI ,PEDIDO1.CUSTOIPI ," +
            "PEDIDO1.IDSITTRIBIPI ,PEDIDO1.IDSITTRIBA ,PEDIDO1.BASEICM ,PEDIDO1.ICM ,PEDIDO1.BASEMP,PEDIDO1.CUSTOICM ," +
            "PEDIDO1.IDSITTRIBB ,PEDIDO1.BASEICMSUBST ,PEDIDO1.ICMSUBST ,PEDIDO1.ICMSSUBSTREDUCAO ,PEDIDO1.MVA ," +
            "PEDIDO1.IDSITTRIBPISPASEP ,PEDIDO1.BCPISPASEP ,PEDIDO1.ALIQPISPASEP ,PEDIDO1.PISPASEP ," +
            "PEDIDO1.IDSITTRIBCOFINS ,PEDIDO1.BCCOFINS1 ,PEDIDO1.ALIQCOFINS1 ,PEDIDO1.COFINS1 ,PEDIDO1.VALORFRETE ," +
            "PEDIDO1.VALORSEGURO ,PEDIDO1.VALOROUTRAS ,PEDIDO1.TOTALNOTA , PEDIDO1.TOTALPREVISTO, PEDIDO1.CALCULOAUTOMATICO, " +
            "PEDIDO1.CODIGOEMP01 , PEDIDO1.CODIGOEMP02 ,PEDIDO1.CODIGOEMP03 ,PEDIDO1.CODIGOEMP04 ,PEDIDO1.COMPLEMENTO, " +
            "PEDIDO1.IDORCAMENTO, PEDIDO1.IDORCAMENTOITEM, PEDIDO1.IDORDEMSERVICO, PEDIDO1.PESO, PEDIDO1.TOTALPESO, " + 
            "PEDIDO1.IDCENTROCUSTO, PEDIDO1.IDCONTACONTABIL, PEDIDO1.NITEM, PEDIDO1.PRECOCUSTO, PEDIDO1.TOTALCUSTOITEM, PEDIDO1.TRIBUTO_PREVISTO " +
            "FROM PEDIDO1 " +
            "LEFT JOIN PECAS ON PECAS.ID=PEDIDO1.IDCODIGO " +
            "LEFT JOIN TIPONOTA ON PEDIDO1.IDTIPONOTA=TIPONOTA.ID " +
            "LEFT JOIN CFOP ON CFOP.ID=PEDIDO1.IDCFOP " +
            "LEFT JOIN UNIDADE ON UNIDADE.ID=PEDIDO1.IDUNIDADE " +

            "WHERE PEDIDO1.IDPEDIDO = " + idpedido + " " +
            "ORDER BY NITEM ";
            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("It.", "nitem", 25, true, DataGridViewContentAlignment.MiddleRight),
///            new GridColuna("Tipo NF", "TIPCOD", 60, true, DataGridViewContentAlignment.MiddleCenter),
            //new GridColuna("CFOP", "cfop", 35, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Código", "codigo", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome Produto", "DESCRICAO", 360, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qtde", "qtde:n2", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Un.", "UNID", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Saldo", "qtdesaldo:n2", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Preço", "preco:n4", 75, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total", "totalnota:n2", 80, true, DataGridViewContentAlignment.MiddleRight)
            //new GridColuna("Tt Prev", "totalprevisto:n2", 80, true, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Complemento", "complemento", 160, true, DataGridViewContentAlignment.MiddleLeft)


        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
        }
        // PDV
        public static DataTable GridDadosPDV(Int32 idpedido, String cnn)
        {
            DataTable dt;
            String query = "";
            //String filtro = "";
            SqlDataAdapter sda;
            query = " " +
            "SELECT PEDIDO1.ID ,PEDIDO1.IDPEDIDO , " + //PEDIDO1.ITEM ,
            "PEDIDO1.IDCODIGO ,PECAS.CODIGO AS [CODIGO], " +
            "CASE WHEN PECAS.CODIGO = '0' THEN PEDIDO1.COMPLEMENTO " +
            "WHEN PEDIDO1.COMPLEMENTO <> '' THEN PECAS.NOME + ' - ' + PEDIDO1.COMPLEMENTO " +
            "ELSE PECAS.NOME END AS DESCRICAO, " +
            "PEDIDO1.COMPLEMENTO, " +
            "PEDIDO1.QTDE ,PEDIDO1.IDUNIDADE , UNIDADE.CODIGO AS [UNID], PEDIDO1.IDIPI, " +
            "PEDIDO1.PRECO ,PEDIDO1.PRECOTABELA ,PEDIDO1.TOTALMERCADO ,PEDIDO1.TOTALNOTA , PEDIDO1.PESO, PEDIDO1.TOTALPESO, " +
            "PEDIDO1.NITEM, PEDIDO1.PRECOCUSTO, PEDIDO1.TOTALCUSTOITEM, PEDIDO1.TRIBUTO_PREVISTO " + 
            "FROM PEDIDO1 " +
            "LEFT JOIN PECAS ON PECAS.ID=PEDIDO1.IDCODIGO " +
            "LEFT JOIN UNIDADE ON UNIDADE.ID=PEDIDO1.IDUNIDADE " +

            "WHERE PEDIDO1.IDPEDIDO = " + idpedido + " " +
            "ORDER BY NITEM ";
            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunasPDV = new GridColuna[]
        {
            new GridColuna("Id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("It.", "nitem", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Código", "codigo", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome Produto", "DESCRICAO", 290, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qtde", "qtde:n2", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Un.", "UNID", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Preço", "preco:n4", 75, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Preço", "precocusto:n4", 75, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total", "totalnota:n2", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total", "totalcustoitem:n2", 80, false, DataGridViewContentAlignment.MiddleRight),


        };
        public static void GridMontaPDV(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasPDV, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
        }
        // Quando entra no cadastro da peça mostra tudo que vendeu dela
        public static DataTable GridDadosPeca(Int32 idCodigo, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            // criei uma query que vai servir para os itens e também no cadastro de peças
            query = "SELECT " +
                        "PEDIDO1.ID AS [IDPED1], " +
                        "PEDIDO.ID AS [IDPED], " +
                        "PEDIDO.NUMERO, " +
                        "PEDIDO.DATA, " +
                        "CLIENTE.COGNOME AS [CLIENTE], " +
                        "PEDIDO1.QTDE, " +
                        "UNIDADE.CODIGO AS UNID, " +
                        "PEDIDO1.QTDE, " +
                        "PEDIDO1.PRECO, " +
                        "PEDIDO1.TOTALMERCADO, " +
                        "PEDIDO1.TOTALNOTA " +
            "FROM PEDIDO1 " +
                "INNER JOIN PEDIDO ON PEDIDO1.IDPEDIDO = PEDIDO.ID " +
                "LEFT JOIN UNIDADE ON UNIDADE.ID = PEDIDO1.IDUNIDADE   " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = PEDIDO.IDCLIENTE " +
            "WHERE PEDIDO1.IDCODIGO=" + idCodigo;
            query = query + " " + filtro + " ORDER BY PEDIDO.DATA DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunasPeca = new GridColuna[]
        {
            new GridColuna("id", "IDPED1", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Nro NF", "NUMERO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Dt Emissao", "DATA", 80, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cliente", "CLIENTE", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qtde", "QTDE", 55, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNID", 35, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Preço Unit", "PRECO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Valor Mercad", "TOTALMERCADO", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Valor Total", "TOTALNOTA", 80, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMontaPeca(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasPeca, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DATA");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["QTDE"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PRECO"].DefaultCellStyle.Format = "N4";
            dgv.Columns["TOTALMERCADO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
        }

    }
}
