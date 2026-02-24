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
    public class clsPagar01BLL : SQLFactory<clsPagar01Info>
    {
        public static DataTable GridDados(Int32 idPagar)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;

            query = "SELECT PAGAS01.ID, PAGAS01.IDDUPLICATA, PAGAS01.DATAENVIO, PAGAS01.DATAOK, " +
                    "SITUACAOCOBRANCACOD.CODIGO + '=' + SITUACAOCOBRANCACOD.NOME AS [COB], " +
                    "PAGAS01.VALOR, PAGAS01.DEBCRED, PAGAS01.BAIXOUCOMO " +
                    "FROM RECEBIDA " +
                    "left join PAGAS01 on PAGAS01.IDDUPLICATA=PAGAS.ID " +
                    "left join SITUACAOCOBRANCACOD on SITUACAOCOBRANCACOD.id=PAGAS01.IDCOBRANCACOD " +
                    "left join SITUACAOCOBRANCACOD1 on SITUACAOCOBRANCACOD1.id=PAGAS01.IDCOBRANCAHIS " +
                    "WHERE PAGAS.IDRECEBER = @IDPAGAS " +
                    "ORDER BY SITUACAOCOBRANCACOD.CODIGO ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDPAGAS", SqlDbType.Int).Value = idPagar;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("id Dup", "IDDUPLICATA", 1, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Data Baixa", "DATAENVIO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Data Pagto", "DATAOK", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Codigo Cobrança", "COB", 120, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Valor", "VALOR", 70, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("D/C", "DEBCRED", 25, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Como Baixou", "BAIXOUCOMO", 200, true, DataGridViewContentAlignment.MiddleLeft),
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["DATAENVIO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["DATAOK"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VALOR"].DefaultCellStyle.Format = "N2";
        }

    }
}
