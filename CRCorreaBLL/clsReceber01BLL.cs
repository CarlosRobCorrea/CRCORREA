using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;


using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsReceber01BLL : SQLFactory<clsReceber01Info>
    {
        public static DataTable GridDados(Int32 idReceber)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PAGAR01.ID, PAGAR01.IDDUPLICATA, PAGAR01.DATAENVIO, PAGAR01.DATAOK, " +
                    "SITUACAOCOBRANCACOD.CODIGO + '=' + SITUACAOCOBRANCACOD.NOME AS [COB], " +
                    "PAGAR01.VALOR, PAGAR01.DEBCRED, PAGAR01.BAIXOUCOMO " +
                    "FROM RECEBIDA " +
                    "left join PAGAR01 on PAGAR01.IDDUPLICATA=RECEBIDA.ID " +
                    "left join SITUACAOCOBRANCACOD on SITUACAOCOBRANCACOD.id=PAGAR01.IDCOBRANCACOD " +
                    "left join SITUACAOCOBRANCACOD1 on SITUACAOCOBRANCACOD1.id=PAGAR01.IDCOBRANCAHIS " +
                    "WHERE PAGAR.IDPAGAR = @IDPAGAR " +
                    "ORDER BY SITUACAOCOBRANCACOD.CODIGO ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDPAGAR", SqlDbType.Int).Value = idReceber;
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
