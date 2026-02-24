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
    public class clsCotacaoentregaBLL  : SQLFactory<clsCotacaoentregaInfo>
    {
        public static DataTable GridDados(Int32 idcotacao2, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT COTACAOENTREGA.ID, COTACAOENTREGA.IDCOTACAO, COTACAOENTREGA.IDCOTACAO1, COTACAOENTREGA.IDCOTACAO2, " +
                    "COTACAOENTREGA.QTDEENTREGA, COTACAOENTREGA.DATAENTREGA " +
                    "From COTACAOENTREGA " +
                    "WHERE COTACAOENTREGA.IDCOTACAO2= @IDCOTACAO2 ";

            filtro = filtro + " ORDER BY COTACAOENTREGA.DATAENTREGA ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDCOTACAO2", SqlDbType.Int).Value = idcotacao2;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id. Cotacao", "IDCOTACAO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id. Cotacao", "IDCOTACAO1", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id. Cotacao", "IDCOTACAO2", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Data Entrega", "DATAENTREGA", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Entrega", "QTDEENTREGA", 60, true, DataGridViewContentAlignment.MiddleRight)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["DATAENTREGA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["QTDEENTREGA"].DefaultCellStyle.Format = "N3";
        }

        // 

        public static DataTable GridDadosTodos(Int32 idCotacao, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT  COTACAOENTREGA.ID, COTACAOENTREGA.IDCOTACAO, COTACAOENTREGA.IDCOTACAO1, COTACAOENTREGA.IDCOTACAO2, " +
                    "COTACAOENTREGA.QTDEENTREGA, COTACAOENTREGA.DATAENTREGA " +
                    "From COTACAOENTREGA " +
                    "WHERE COTACAOENTREGA.IDCOTACAO= @IDCOTACAO ";

            filtro = filtro + " ORDER BY COTACAOENTREGA.DATAENTREGA ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDCOTACAO", SqlDbType.Int).Value = idCotacao;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunasTodos = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id. Cotacao", "IDCOTACAO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id. Cotacao", "IDCOTACAO1", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id. Cotacao", "IDCOTACAO2", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Data Entrega", "DATAENTREGA", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Entrega", "QTDEENTREGA", 60, true, DataGridViewContentAlignment.MiddleRight)
        };

        public static void GridMontaTodos(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasTodos, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["DATAENTREGA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["QTDEENTREGA"].DefaultCellStyle.Format = "N3";
        }

        public static void ExcluirEntregas_cotacao1(Int32 idCotacao1, String cnn)
        {
            SqlConnection scn;
            SqlCommand scd;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("delete cotacaoentrega where idcotacao1 = @idcotacao1", scn);
            scd.Parameters.Add("@idcotacao1", SqlDbType.Int).Value = idCotacao1;

            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();

        }

        public static void ExcluirEntregas_cotacao2(Int32 idCotacao2, String cnn)
        {
            SqlConnection scn;
            SqlCommand scd;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("delete cotacaoentrega where idcotacao2 = @idcotacao2", scn);
            scd.Parameters.Add("@idcotacao2", SqlDbType.Int).Value = idCotacao2;

            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();

        }
    }
}

