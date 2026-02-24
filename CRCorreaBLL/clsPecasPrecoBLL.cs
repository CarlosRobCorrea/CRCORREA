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
    public class clsPecasPrecoBLL : SQLFactory<clsPecasPrecoInfo>
    {

        public static DataTable GridDados(Int32 idCodigo, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;

            query = "SELECT PECASPRECO.ID, PECASPRECO.IDCODIGO, PECASPRECO.DATA, PECASPRECO.VALORAVISTA, PECASPRECO.VALORAPRAZO " +
               "FROM PECASPRECO " +
               "WHERE PECASPRECO.IDCODIGO=" + idCodigo;
            query = query + " " + filtro + " ORDER BY PECASPRECO.DATA DESC, PECASPRECO.ID DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("idcodigo", "IDCODIGO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Data Valido", "DATA", 120, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Vl Venda", "VALORAVISTA", 75, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Vl Fornec", "VALORAPRAZO", 75, true, DataGridViewContentAlignment.MiddleRight),
//            new GridColuna("Vl Manuten", "VALORMANUTENCAO", 75, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DATA");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dgv.Columns["VALORAVISTA"].DefaultCellStyle.Format = "N4";
            dgv.Columns["VALORAPRAZO"].DefaultCellStyle.Format = "N4";
//            dgv.Columns["VALORMANUTENCAO"].DefaultCellStyle.Format = "N4";
        }


    }
}
