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
    public class clsPecasclassifica1BLL : SQLFactory<clsPecasclassifica1Info>
    {
        public override int Incluir(clsPecasclassifica1Info info, string cnn)
        {
            VerificaInfo(info);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsPecasclassifica1Info info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public void VerificaInfo(clsPecasclassifica1Info info)
        {

        }
        public static DataTable GridDados(Int32 idclassifica,String cnn)
        {
            DataTable dt;
            String query = "";
            //String filtro = "";
            SqlDataAdapter sda;
            query = "select ID, CODIGO, NOME " +
                    "from PECASCLASSIFICA1 WHERE IDCLASSIFICA=" + idclassifica + " ORDER BY CODIGO";
            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Codigo", "CODIGO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 150, true, DataGridViewContentAlignment.MiddleLeft)
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
