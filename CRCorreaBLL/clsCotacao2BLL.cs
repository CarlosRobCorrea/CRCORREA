using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

using CRCorreaInfo;
using CRCorreaFuncoes;


namespace CRCorreaBLL
{
    public class clsCotacao2BLL : SQLFactory<clsCotacao2Info>
    {
        public static DataTable GridDados(Int32 idCotacao1, String cnn)
        {
            String query = "";
            SqlDataAdapter sda;

            DataTable dt = new DataTable();
            query = "SELECT CLIENTE.COGNOME AS [FORNECEDOR], " +
                        "SITUACAOTIPOTITULO.CODIGO AS [FORMAPAGTO], " +
                        "CONDPAGTO.CODIGO AS [CONDPAGTO],  " +
                        "UNIDADE.CODIGO AS [UNID], " +
                        "COMPRAS.NUMERO AS [PEDIDOCOMPRA], " +
                        "COTACAO2.* " +
                "FROM COTACAO2 " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID = COTACAO2.IDFORNECEDOR " +
                    "LEFT JOIN UNIDADE ON UNIDADE.ID = COTACAO2.IDUNIDADE " +
                    "LEFT JOIN COMPRAS ON COMPRAS.ID = COTACAO2.IDPEDIDOCOMPRA " +
                    "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID = COTACAO2.IDCONDPAGTO " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = COTACAO2.IDFORMAPAGTO " +
                "WHERE " +
                    "COTACAO2.IDCOTACAO1= @IDCOTACAO1 " +
                "ORDER BY FORNECEDOR ";

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDCOTACAO1", SqlDbType.Int).Value = idCotacao1;
            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id.Cotacao1", "IDCOTACAO1", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Fornecedor", "FORNECEDOR", 150, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qt Orca", "QTDEORCADA", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNID", 25, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Preço", "PRECO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Ipi", "IPI", 30, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Icm", "ICM", 30, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total Nota", "TOTALNOTA", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Cond Pagto", "CONDPAGTO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("P.C.", "PEDIDOCOMPRA", 40, true, DataGridViewContentAlignment.MiddleRight)

        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["QTDEORCADA"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PRECO"].DefaultCellStyle.Format = "N6";
            dgv.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
        }

        //

        public static DataTable GridDadosTodos(Int32 idCotacao, String cnn)
        {
            String query = "";
            SqlDataAdapter sda;

            DataTable dt = new DataTable();
            query = "SELECT  " +
                        "COTACAO2.ID, COTACAO2.IDCOTACAO1, " +
                        "CLIENTE.COGNOME AS [FORNECEDOR], COTACAO2.QTDEORCADA, " +
                        "UNIDADE.CODIGO AS [UNID], COTACAO2.PRECO, COTACAO2.IPI, COTACAO2.ICM, COTACAO2.TOTALNOTA, " +
                        "CONDPAGTO.CODIGO + ' = ' + CONDPAGTO.NOME As [CONDPAGTO], " +
                        "COMPRAS.NUMERO AS [PEDIDOCOMPRA], " + 
                        "SITUACAOTIPOTITULO.CODIGO AS [FORMAPAGTO], " +
                        "COTACAO2.* " +
                "FROM COTACAO2 " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID = COTACAO2.IDFORNECEDOR " +
                    "LEFT JOIN UNIDADE ON UNIDADE.ID = COTACAO2.IDUNIDADE " +
                    "LEFT JOIN COMPRAS ON COMPRAS.ID = COTACAO2.IDPEDIDOCOMPRA " +
                    "LEFT JOIN CONDPAGTO ON CONDPAGTO.ID = COTACAO2.IDCONDPAGTO " +
                    "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = COTACAO2.IDFORMAPAGTO " +
                "WHERE " +
                    "COTACAO2.IDCOTACAO= @IDCOTACAO " +
                "ORDER BY FORNECEDOR ";

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDCOTACAO", SqlDbType.Int).Value = idCotacao;
            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunasTodos = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id.Cotacao1", "IDCOTACAO1", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Fornecedor", "FORNECEDOR", 150, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qt Orca", "QTDEORCADA", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNID", 25, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Preço", "PRECO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Ipi", "IPI", 30, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Icm", "ICM", 30, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Total Nota", "TOTALNOTA", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Cond Pagto", "CONDPAGTO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("P.C.", "PEDIDOCOMPRA", 40, true, DataGridViewContentAlignment.MiddleRight)

        };

        public static void GridMontaTodos(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasTodos, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["QTDEORCADA"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PRECO"].DefaultCellStyle.Format = "N6";
            dgv.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
        }

        public static void ExcluirCotacao2_cotacao1(Int32 idCotacao1, String cnn)
        {
            SqlConnection scn;
            SqlCommand scd;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("delete cotacao2 where idcotacao1 = @idcotacao1", scn);
            scd.Parameters.Add("@idcotacao1", SqlDbType.Int).Value = idCotacao1;

            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();
        }

        public override Boolean Excluir(Int32 idCotacao2, String cnn)
        {
            clsCotacaoentregaBLL clsCotacaoentregaBLL = new clsCotacaoentregaBLL();
            clsCotacaoentregaBLL.ExcluirEntregas_cotacao2(idCotacao2, cnn);          

            return base.Excluir(idCotacao2, cnn);
        }
    }
}
