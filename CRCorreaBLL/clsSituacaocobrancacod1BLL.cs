using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;
using System.Drawing;

using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsSituacaocobrancacod1BLL : SQLFactory<clsSituacaocobrancacod1Info>
    {
        public DataTable GridDados(Int32 idcobrancacod, String cnn)
        {
            String query;
            DataTable dte = new DataTable();
            query = "SELECT " +
                        "ID, " +
                        "(SELECT CODIGO FROM SITUACAOCOBRANCACOD WHERE ID = SITUACAOCOBRANCACOD1.IDCOBRANCACOD) AS PRINCIPAL, " +
                        "CODIGO, " +
                        "NOME " +
                    "FROM " +
                        "SITUACAOCOBRANCACOD1 " +
                    "WHERE " +
                        "IDCOBRANCACOD = @IDCOBRANCACOD";

            SqlDataAdapter sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDCOBRANCACOD", SqlDbType.Int).Value = idcobrancacod;
            sda.Fill(dte);

            return dte;
        }
        public GridColuna[] dtDadosColunas = new GridColuna[]
        {
            new GridColuna("iD", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter), 
            new GridColuna("Codigo", "CODIGO", 175, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 470, true, DataGridViewContentAlignment.MiddleLeft)
            //new GridColuna("Ativo", "Ativo", 50, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            dgv.DataSource = dt;
            
            clsGridHelper.MontaGrid2(dgv, dtDadosColunas, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
        }

    }
}
