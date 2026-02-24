using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsXmlNFCompraBLL : SQLFactory<clsXmlNFCompraInfo>
    {
        public static DataTable GridDados()
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT XMLNFCOMPRA.ID, XMLNFCOMPRA.NUMERO, " +
                    "XMLNFCOMPRA.DATA, XMLNFCOMPRA.FORNECEDOR AS FORNECEDOR, " +
                    "XMLNFCOMPRA.TOTALNOTAFISCAL,XMLNFCOMPRA.STATUS " +
                    "FROM XMLNFCOMPRA   " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=XMLNFCOMPRA.IDFORNECEDOR ";
            query = query + filtro + " ORDER BY XMLNFCOMPRA.NUMERO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 1, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id.", "STATUS", 1, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Nrº Nota", "NUMERO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("D.Emissão", "DATA", 80, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fornecedor", "FORNECEDOR", 110, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Total NF", "TOTALNOTAFISCAL", 80, true, DataGridViewContentAlignment.MiddleRight),
        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["NUMERO"].DefaultCellStyle.Format = "N0";
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["TOTALNOTAFISCAL"].DefaultCellStyle.Format = "N2";
        }

    }
}
