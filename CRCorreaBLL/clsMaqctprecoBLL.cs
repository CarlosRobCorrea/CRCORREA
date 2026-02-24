using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsMaqctprecoBLL : SQLFactory<clsMaqctprecoInfo>
    {
        public DataTable CarregaGrid(String cnn, Int32 idmaqct)
        {
            DataTable dt;
            String query;
            SqlDataAdapter sda;

            dt = new DataTable();

            query = "SELECT " +
                            "* " +
                        "FROM " +
                            "MAQCTPRECO " +
                        "where IDMAQCT = @IDMAQCT ";

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDMAQCT", SqlDbType.Int).Value = idmaqct;

            sda.Fill(dt);

            return dt;
        }

        /// <summary>
        /// ////////////////
        /// </summary>
         private static GridColuna[] dtGridColunasMaqCtPreco = new GridColuna[]
        {
                new GridColuna("Data", "DATA", 90, true,DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Valor", "VALOR", 90, true, DataGridViewContentAlignment.MiddleRight),         
        };
        
        public static void GridMontaMaqCtPreco(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasMaqCtPreco, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DATA");
            dgv.AllowUserToResizeColumns = true;
            
        }
    }
    
}
