using CRCorreaInfo;
using CRCorreaFuncoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsOrcamento1BLL : SQLFactory<clsOrcamento1Info>
    {

        public static DataTable GridDados(Int32 idOrcamento, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT ORCAMENTO1.ID, PECAS.CODIGO, CASE WHEN PECAS.CODIGO = '0' THEN ORCAMENTO1.DESCRICAO1 ELSE PECAS.NOME END AS DESCRITAB, " +
                    " ORCAMENTO1.DESCONTO, ORCAMENTO1.DESCRICAO1,ORCAMENTO1.DESCRICAO2,ORCAMENTO1.DESCRICAO3,ORCAMENTO1.DESCRICAO4,ORCAMENTO1.IDCODIGO, " +
                    " ORCAMENTO1.FOTO " +
                    ",ORCAMENTO1.IDORCAMENTO,ORCAMENTO1.IDORDEMSERVICO,ORCAMENTO1.IDORDEMSERVICOITEM,ORCAMENTO1.IDPEDIDO,ORCAMENTO1.IDPEDIDOITEM,ORCAMENTO1.ITEM " +
                    ",ORCAMENTO1.MOTIVO,ORCAMENTO1.PESO,ORCAMENTO1.PRECO, ORCAMENTO1.PRECOCONFIRMADO,ORCAMENTO1.QTDE,ORCAMENTO1.UNID,ORCAMENTO1.PRECOLIQUIDO, ORCAMENTO1.TOTALMERCADORIA, ORCAMENTO1.REFERENCIA1,ORCAMENTO1.REFERENCIA2 " +
                    ",ORCAMENTO1.SITUACAO,ORCAMENTO1.TOTALPESO,ORCAMENTO1.QTDECONFIRMADAITEM, ORCAMENTO1.TOTALCONFIRMADOITEM,ORCAMENTO1.VALORDESCONTO " +
                    " FROM ORCAMENTO1 " +
                    "LEFT JOIN PECAS ON PECAS.ID = ORCAMENTO1.IDCODIGO ";
            filtro = "WHERE " +
                        "ORCAMENTO1.IDORCAMENTO = @IDORCAMENTO ";

            query = query + filtro + " ORDER BY ORCAMENTO1.ITEM";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("IDORCAMENTO", SqlDbType.Int).Value = idOrcamento;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("ID", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Item", "ITEM", 40, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Codigo", "CODIGO", 85, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descricao", "DESCRITAB", 300, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qtde", "QTDE", 30, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Preço Liq", "PRECOLIQUIDO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total", "TOTALMERCADORIA", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qtde oK", "QTDECONFIRMADAITEM", 30, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total oK", "TOTALCONFIRMADOITEM", 80, true, DataGridViewContentAlignment.MiddleRight),
        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
        }

    }
}
