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
    public class clsPecasTipoBLL : SQLFactory<clsPecasTipoInfo>
    {
        public static DataTable GridDados(String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PECASTIPO.ID, PECASTIPO.CODIGO, PECASTIPO.NOME " +
                    "FROM PECASTIPO ";

            query = query + " " + filtro + " ORDER BY PECASTIPO.CODIGO ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("T.", "CODIGO", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nome Tipo", "NOME", 250, true, DataGridViewContentAlignment.MiddleLeft),

        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
        }

    }
}
