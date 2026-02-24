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
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsEmpresaBLL : SQLFactory<clsEmpresaInfo>
    {
        public static DataTable GridDados(String cnn)
        {

            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "select " +
                        "ID, " +
                        "CODIGO, " +
                        "COGNOME, " +
                        "NOME, " +
                        "CGC " +
                    "FROM " +
                        "EMPRESA ";

            dt = new DataTable();
            query = query + " order by codigo ";
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }
        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Codigo", "CODIGO", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cognome", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Descrição", "NOME", 450, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("CNPJ/CPF", "CGC", 120, true, DataGridViewContentAlignment.MiddleCenter)
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
