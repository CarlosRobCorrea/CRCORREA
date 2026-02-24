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
    public class clsPecasFornePrecoBLL : SQLFactory<clsPecasFornePrecoInfo>
    {
        public static DataTable GridDados(Int32 IdCodigo, Int32 IdFornecedor, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PECASFORNEPRECO.ID, PECASFORNEPRECO.IDCODIGO, PECASFORNEPRECO.IDFORNECEDOR, " +
                    "CLIENTE.COGNOME as [FORNECEDOR], PECASFORNEPRECO.DATA, PECASFORNEPRECO.VALORVENDA, " +
                    "PECASFORNEPRECO.IDUNIDADE, UNIDADE.CODIGO AS [UNID], " +
                    "PECASFORNEPRECO.ICMS, PECASFORNEPRECO.PISCOFINS, " +
                    "PECASFORNEPRECO.VALORCUSTO, PECASFORNEPRECO.TIPO, PECASFORNEPRECO.SEQUENCIA,  PECASFORNEPRECO.BASEICMS " +
                    "FROM PECASFORNEPRECO " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=PECASFORNEPRECO.IDFORNECEDOR " +
                    "LEFT JOIN UNIDADE ON UNIDADE.ID=PECASFORNEPRECO.IDUNIDADE ";
            filtro = " WHERE PECASFORNEPRECO.IDCODIGO = @IDCODIGO ";
            if (IdFornecedor > 0)
            {
                filtro = filtro + " AND PECASFORNEPRECO.IDFORNECEDOR = @IDFORNECEDOR ";
            }
            filtro = filtro + " ORDER BY CLIENTE.COGNOME, PECASFORNEPRECO.DATA DESC  ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = IdCodigo;
            sda.SelectCommand.Parameters.Add("@IDFORNECEDOR", SqlDbType.Int).Value = IdFornecedor;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Fornecedor", "Fornecedor", 60, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Data", "DATA", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Preço Unit.", "VALORVENDA", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Un", "UNID", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Icm", "ICMS", 30, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Pis", "PISCOFINS", 27, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Preço s/Imp", "VALORCUSTO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("T", "TIPO", 20, true, DataGridViewContentAlignment.MiddleCenter)

        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "duplicata");

            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VALORVENDA"].DefaultCellStyle.Format = "N6";
            dgv.Columns["ICMS"].DefaultCellStyle.Format = "N2";
            dgv.Columns["PISCOFINS"].DefaultCellStyle.Format = "N2";
            dgv.Columns["VALORCUSTO"].DefaultCellStyle.Format = "N6";
        }

    }
}
