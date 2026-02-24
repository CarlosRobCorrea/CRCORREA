using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsPagasobservaBLL : SQLFactory<clsPagarObservaInfo>
    {
        public static DataTable GridDados(Int32 idPagas)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            query = "SELECT ID, IDDUPLICATA, DATA, OBSERVAR, EMITENTE " +
                    "FROM PAGASOBSERVA " +
                    "WHERE PAGASOBSERVA.IDDUPLICATA = @IDPAGAR " +
                    "ORDER BY DATA DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDPAGAR", SqlDbType.Int).Value = idPagas;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;

        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("id Dup", "IDDUPLICATA", 1, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Data", "DATA", 70, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Observação", "OBSERVAR", 90, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Emitente", "EMITENTE", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Posição", "POSICAO", 0 , true, DataGridViewContentAlignment.MiddleLeft),
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

    }
}
