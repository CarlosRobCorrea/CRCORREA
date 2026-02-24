using CRCorreaInfo;
using CRCorreaFuncoes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsPecasCustoBLL : SQLFactory<clsPecasCustoInfo>
    {
        public static DataTable GridDados(Int32 _idPeca)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            query = "SELECT PECASCUSTO.*, PECAS.CODIGO, PECAS.NOME, UNIDADE.CODIGO AS UNIDADE " +
                    "FROM PECASCUSTO " +
                    "LEFT JOIN PECAS ON PECAS.ID=PECASCUSTO.IDCODIGO " +
                    "LEFT JOIN UNIDADE ON PECAS.IDUNIDADE=UNIDADE.ID " +
                    "WHERE PECASCUSTO.IDPECA=@IDPECA ";
            query = query + " ORDER BY PECASCUSTO.ORDEM ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("IDPECA", SqlDbType.Int).Value = _idPeca;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Ordem", "ORDEM", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Codigo", "CODIGO", 60, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 260, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tipo", "TIPO", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qtde", "QTDE", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNIDADE", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Participa", "PARTICIPACAO", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Preço Unit", "PRECOUNITARIO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Preço Total", "PRECOTOTAL", 70, true, DataGridViewContentAlignment.MiddleRight),

        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "ORDEM");
            dgv.Columns["QTDE"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PARTICIPACAO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["PRECOUNITARIO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["PRECOTOTAL"].DefaultCellStyle.Format = "N2";
        }

    }
}
