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
    public class clsPagarObservaBLL : SQLFactory<clsPagarObservaInfo>
    {
        public static DataTable GridDados(Int32 idPagar)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT ID, IDDUPLICATA, DATA, OBSERVAR, EMITENTE " +
                    "FROM PAGAROBSERVA " +
                    "WHERE PAGAROBSERVA.IDDUPLICATA = @IDPAGAR " +
                    "ORDER BY DATA DESC ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDPAGAR", SqlDbType.Int).Value = idPagar;
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
