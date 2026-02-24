using CRCorreaFuncoes;
using CRCorreaInfo;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;


namespace CRCorreaBLL
{
    public class clsDocFiscalBLL : SQLFactory<clsDocFiscalInfo>
    {
        public DataTable GridDados(String ativo, String cnn)
        {
            if (ativo == null)
            {
                ativo = "";
            }
            else
            {
                ativo = ativo.ToUpper();
            }

            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "select " +
                        "ID, " +
                        "CODIGO, " +
                        "COGNOME, " +
                        "NOME, " +
                        "SERIE, MODELO, ATIVO " +
                    "FROM " +
                        "DOCFISCAL ";

            if (ativo == "S" || ativo == "N")
            {
                query += "WHERE " +
                            "ATIVO = @ATIVO";
            }
            dt = new DataTable();
            query = query + " order by codigo ";
            sda = new SqlDataAdapter(query, cnn);

            if (ativo == "S" || ativo == "N")
            {
                sda.SelectCommand.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = ativo;
            }

            sda.Fill(dt);

            return dt;
        }
        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 10, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Codigo", "CODIGO", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Sigla Int", "COGNOME", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Descrição", "NOME", 450, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Serie", "SERIE", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Modelo", "MODELO", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Ativo", "ATIVO", 40, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "codigo");
        }

    }
}
