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
//using Itaesbra.DAL;
using CRCorreaFuncoes;

namespace CRCorreaBLL
{
    public class clsPecasFamiliaBLL : SQLFactory<clsPecasFamiliaInfo>
    {
        public static DataTable GridDados(String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PECASFAMILIA.ID, PECASFAMILIA.CODIGO, PECASFAMILIA.NOME, UNIDADE.CODIGO AS [UNID], PECASFAMILIA.TIPO " +
                    "FROM PECASFAMILIA " + 
                    "LEFT JOIN UNIDADE ON UNIDADE.ID=PECASFAMILIA.IDUNIDADE ";
            query = query + " " + filtro + " ORDER BY PECASFAMILIA.CODIGO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Familia", "CODIGO", 35, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nome Familia", "NOME", 250, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Unid", "UNID", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Tipo", "TIPO", 30, true, DataGridViewContentAlignment.MiddleCenter),

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
