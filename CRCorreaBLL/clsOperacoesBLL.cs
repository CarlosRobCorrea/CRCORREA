using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsOperacoesBLL : SQLFactory<clsOperacoesInfo>
    {
        /*
        public override int Incluir(clsOperacoesInfo info, String cnn)
        {
           // VerificaInfo(info);

//            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsOperacoesInfo info, String cnn)
        {
  //          VerificaInfo(info);

    //        return base.Alterar(info, cnn);
        }*/

        public void VerificaInfo(clsOperacoesInfo info)
        {

        }


        public DataTable GridDados(String ativo, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;
            query = "SELECT ID, CODIGO, NOME, ATIVO " +
                    "FROM OPERACOES ";
            if (ativo == "S" || ativo == "N")
            {
                query += " WHERE ATIVO = '" + ativo + "' ";
            }
            query += " ORDER BY CODIGO ";
            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);
            return dt;
        }
        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Codigo", "CODIGO", 120, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 400, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Ativo", "ATIVO", 50, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
        }

    }
}
