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
    public class clsXmlNFCompra1BLL : SQLFactory<clsXmlNFCompra1Info>
    {
        public static DataTable GridDados(Int32 idXmlNFCompra)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "select " +
                        "XMLNFCOMPRA1.ID, " +
                        "XMLNFCOMPRA1.QTDE, " +
                        "XMLNFCOMPRA1.UNID, " +
                        "XMLNFCOMPRA1.IDCODIGO, " +
                        "XMLNFCOMPRA1.CODIGO, " +
                        "XMLNFCOMPRA1.CODIGOBARRA, " +
                        "XMLNFCOMPRA1.NCM, " +
                        "XMLNFCOMPRA1.DESCRICAO, " +
                        "XMLNFCOMPRA1.DESCRICAOCADASTRO, " +
                        "XMLNFCOMPRA1.PRECO, " +
                        "XMLNFCOMPRA1.TOTALMERCADO, " +
                        "XMLNFCOMPRA1.PRECOVENDA, " +
                        "XMLNFCOMPRA1.STATUS, " +
                        "XMLNFCOMPRA1.FATURA, XMLNFCOMPRA1.FATOR, XMLNFCOMPRA1.VALORICM, XMLNFCOMPRA1.VALORIPI " +
                    "FROM  XMLNFCOMPRA1 ";

            filtro = "WHERE " +
                        "XMLNFCOMPRA1.NUMERO = @IDNFCOMPRA ";

            query = query + filtro; // + " ORDER BY NFCOMPRA1.TIPOENTRADA";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("IDNFCOMPRA", SqlDbType.Int).Value = idXmlNFCompra;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("ID", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qtde", "QTDE", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNID", 20, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id Codigo", "IDCODIGO", 0, false  , DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Codigo", "CODIGO", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Cod.Barr", "CODIGOBARRA", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("N.C.M.", "NCM", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descrição", "DESCRICAO", 265, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descrição Cadastro", "DESCRICAOCADASTRO", 265, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Pr Unitário", "PRECO", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tt Mercadoria", "TOTALMERCADO", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Preço Venda", "PRECOVENDA", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Status", "STATUS", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Ok", "FATURA", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fator", "FATOR", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("ICM", "VALORICM", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("IPI", "VALORIPI", 70, true, DataGridViewContentAlignment.MiddleRight),
        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            dgv.Columns["FATOR"].DefaultCellStyle.Format = "N3";
        }


    }
}
