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
    public class clsEstadosBLL : SQLFactory<clsEstadosInfo>
    {
        public DataTable GridDados(String cnn)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;

            query = "SELECT ID, ESTADO, NOMEEXT, CAPITAL FROM ESTADOS ORDER BY ESTADO";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            
            dt = new DataTable();

            sda.Fill(dt);

            return dt;
        }
        //

        public DataTable GridDadosUF( String cnn)
        {
            String query;   
            DataTable dte = new DataTable();
            query = "SELECT id, estado FROM estados";           
            SqlDataAdapter sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dte);

            return dte;
        }
        private static GridColuna[] dtGridColunasUF = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("UF", "estado", 40, true, DataGridViewContentAlignment.MiddleLeft)         
        };

        public static void GridMontaUF(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasUF, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "estado");
        }
    }
}
