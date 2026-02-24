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
    public class clsBancosBLL : SQLFactory<clsBancosInfo>
    {
        public DataTable GridDados(String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
                        "CONTA, " +
                        "ATIVO, " +
                        "BANCO, " +
                        "AGENCIA, " +
                        "NOME, " +
                        "CONTACOR, " +
                        "CTATRANSITORIA " +
                    "FROM " +
                        "BANCOS " +
                    "ORDER BY " +
                        "CONTA";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }
        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Codigo", "CODIGO", 220, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 720, true, DataGridViewContentAlignment.MiddleLeft),
        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
        }



        public DataTable GridDados2(String ativo, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
                        "CONTA, " +
                        "ATIVO, " +
                        "BANCO, " +
                        "AGENCIA, " +
                        "NOME, " +
                        "CONTACOR, " +
                        "CTATRANSITORIA " +
                    "FROM " +
                        "BANCOS " +
                    "WHERE " +
                        "ATIVO = @ATIVO " +
                    "ORDER BY " +
                        "CONTA";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);

            sda.SelectCommand.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = ativo;

            sda.Fill(dt);

            return dt;
        }
    }
}
