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
    public class clsZonasBLL : SQLFactory<clsZonasInfo>
    {
        public DataTable GridDados(String Ativo, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;
            query = "SELECT " +
                        "ZONAS.ID, " +
                        "ZONAS.CODIGO, " +
                        "ZONAS.NOME, " +
                        "ZONAS.ATIVO " +
                    "FROM " +
                        "ZONAS ";
            if (Ativo != "")
            {
                query = query + " where ativo = '" + Ativo + "' ";
            }
            query = query + " order by zonas.codigo ";
            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);
            return dt;
        }

        public GridColuna[] dtDadosColunas = new GridColuna[]
        {
            new GridColuna("iD", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter), 
            new GridColuna("Codigo", "CODIGO", 175, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 470, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Ativo", "Ativo", 50, true, DataGridViewContentAlignment.MiddleCenter)
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
