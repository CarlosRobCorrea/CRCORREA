using CRCorreaInfo;
using CRCorreaFuncoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsOrcamentoBLL : SQLFactory<clsOrcamentoInfo>
    {
        public override int Incluir(clsOrcamentoInfo info, string cnn)
        {
            info.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                "SELECT MAX(NUMERO + 1) FROM ORCAMENTO where  YEAR(DATA) = YEAR(GETDATE()) AND FILIAL = " + clsInfo.zfilial));
            if (info.numero == 0)
            {
                info.numero = 1;
            }

            return base.Incluir(info, cnn);
        }

        public static DataTable GridDados(Int32 Filial, DateTime PeriodoDe, DateTime PeriodoAte, String TipoLista)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            query = "SELECT ORCAMENTO.ID, ORCAMENTO.FILIAL, ORCAMENTO.NUMERO, ORCAMENTO.DATA, CLIENTE.COGNOME, CASE ORCAMENTO.SITUACAO WHEN 'S' THEN 'Fechado' ELSE 'Aberto' END AS [SITU], ORCAMENTO.TOTALORCAMENTOBRUTO " +
                    "FROM ORCAMENTO " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID = ORCAMENTO.IDCLIENTE " +
                    "WHERE ORCAMENTO.DATA >= " + clsParser.SqlDateTimeFormat(PeriodoDe.ToString("dd/MM/yyyy") + " 00:00", true) +
                    " AND ORCAMENTO.DATA <= " + clsParser.SqlDateTimeFormat(PeriodoAte.ToString("dd/MM/yyyy") + " 00:00", true);
            if (Filial > 0)
            {
                query = query + " AND ORCAMENTO.FILIAL = @FILIAL ";
            }
            if (TipoLista == "A") // Aberto
            {
                query = query + " AND ORCAMENTO.SITUACAO != @TipLista ";
            }
            else if (TipoLista == "S") // Fechado
            {
                query = query + " AND ORCAMENTO.SITUACAO = @TipLista ";
            }
            query = query + " ORDER BY ORCAMENTO.DATA DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("FILIAL", SqlDbType.Int).Value = Filial;
            //sda.SelectCommand.Parameters.Add("PeriodoDe", SqlDbType.DateTime).Value = PeriodoDe;
            //sda.SelectCommand.Parameters.Add("PeriodoAte", SqlDbType.DateTime).Value = PeriodoAte;
            sda.SelectCommand.Parameters.Add("TipLista", SqlDbType.NVarChar).Value = 'S';
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("F", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("NºPed", "NUMERO", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("D.Emissão", "DATA", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fornecedor", "COGNOME", 150, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Termino", "SITU", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("T.Pedido ", "TOTALORCAMENTOBRUTO", 70, true, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("T.Previsto ", "TOTALPREVISTO", 70, true, DataGridViewContentAlignment.MiddleRight),
            //new GridColuna("Observa ", "OBSERVA", 150, true, DataGridViewContentAlignment.MiddleLeft),

        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["NUMERO"].DefaultCellStyle.Format = "N0";
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["TOTALORCAMENTOBRUTO"].DefaultCellStyle.Format = "N2";
        }
        public static DataTable GridDadosConfirma(Int32 Filial, DateTime PeriodoDe, DateTime PeriodoAte, String TipoLista)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            query = "SELECT ORCAMENTO.ID, ORCAMENTO.FILIAL, ORCAMENTO.NUMERO, ORCAMENTO.DATA, CLIENTE.COGNOME, CASE ORCAMENTO.SITUACAO WHEN 'S' THEN 'Fechado' ELSE 'Aberto' END AS [SITU], ORCAMENTO.TOTALORCAMENTOBRUTO " +
                    "FROM ORCAMENTO " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID = ORCAMENTO.IDCLIENTE " +
                    "WHERE ORCAMENTO.DATA >= " + clsParser.SqlDateTimeFormat(PeriodoDe.ToString("dd/MM/yyyy") + " 00:00", true) +
                    " AND ORCAMENTO.DATA <= " + clsParser.SqlDateTimeFormat(PeriodoAte.ToString("dd/MM/yyyy") + " 00:00", true);
            if (Filial > 0)
            {
                query = query + " AND ORCAMENTO.FILIAL = @FILIAL ";
            }
            if (TipoLista == "A") // Aberto
            {
                query = query + " AND ORCAMENTO.SITUACAO = @TipLista ";
            }
            else if (TipoLista == "S") // Fechado
            {
                query = query + " AND ORCAMENTO.SITUACAO = @TipLista ";
            }
            query = query + " ORDER BY ORCAMENTO.DATA DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("FILIAL", SqlDbType.Int).Value = Filial;
            //sda.SelectCommand.Parameters.Add("PeriodoDe", SqlDbType.DateTime).Value = PeriodoDe;
            //sda.SelectCommand.Parameters.Add("PeriodoAte", SqlDbType.DateTime).Value = PeriodoAte;
            sda.SelectCommand.Parameters.Add("TipLista", SqlDbType.NVarChar).Value = TipoLista;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static void GridMontaConfirma(DataGridView dgv, DataTable dt, Int32 id)
        {

            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasConfirma, true);
            //dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["NUMERO"].DefaultCellStyle.Format = "N0";
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["TOTALORCAMENTOBRUTO"].DefaultCellStyle.Format = "N2";
        }
        private static GridColuna[] dtGridColunasConfirma = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("F", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nro Orcam.", "NUMERO", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("D.Emissão", "DATA", 80, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cliente", "COGNOME", 150, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Termino", "SITU", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("T.Pedido ", "TOTALORCAMENTOBRUTO", 70, true, DataGridViewContentAlignment.MiddleRight),
    //new GridColuna("T.Previsto ", "TOTALPREVISTO", 70, true, DataGridViewContentAlignment.MiddleRight),
    //new GridColuna("Observa ", "OBSERVA", 150, true, DataGridViewContentAlignment.MiddleLeft),

        };

    }
}
