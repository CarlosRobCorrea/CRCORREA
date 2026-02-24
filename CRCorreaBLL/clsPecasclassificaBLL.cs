using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;

using CRCorreaInfo;

using CRCorreaFuncoes;


namespace CRCorreaBLL
{
    public class clsPecasclassificaBLL : SQLFactory<clsPecasclassificaInfo>
    {
        public override int Incluir(clsPecasclassificaInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsPecasclassificaInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public void VerificaInfo(clsPecasclassificaInfo _info)
        {            
            if (_info.codigo.ToString() == "")
            {
                throw new Exception("Codigo não pode ficar em branco !");
            }
            if (_info.nome.ToString() == "")
            {
                throw new Exception("Nome não pode ficar em branco !");
            }
        }
        public static DataTable GridDados(String cnn)
        {
            DataTable dt;
            String query = "";
            //String filtro = "";
            SqlDataAdapter sda;
            query = "select ID, CODIGO, NOME " +
                    "from PECASCLASSIFICA ORDER BY CODIGO";
            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Codigo", "CODIGO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 450, true, DataGridViewContentAlignment.MiddleLeft)
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
