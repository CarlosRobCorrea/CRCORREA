using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsComprasBLL : SQLFactory<clsComprasInfo>
    {
        public override int Incluir(clsComprasInfo info, string cnn)
        {
            info.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                "SELECT MAX(NUMERO + 1) FROM COMPRAS where  YEAR(DATA) = YEAR(GETDATE()) AND FILIAL = " + clsInfo.zfilial));
            if (info.numero == 0)
            {
                info.numero = 1;
            }

            return base.Incluir(info, cnn);
        }

        public static DataTable GridDados(Int32 Filial, Int32 DoAno, Int32 AteAno, String TipoLista)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            query = "SELECT COMPRAS.ID, COMPRAS.FILIAL, COMPRAS.NUMERO, COMPRAS.DATA, " +
                    "CLIENTE.COGNOME, " +
                    "CASE COMPRAS.TERMINO WHEN 'S' THEN 'Fechado' ELSE 'Aberto' END AS [TERM], " +
                    "COMPRAS.TOTALPECA,COMPRAS.TOTALPECAENTRA,COMPRAS.TOTALPECATRANSFE, " +
                    "(COMPRAS.TOTALPECA-(COMPRAS.TOTALPECAENTRA+COMPRAS.TOTALPECATRANSFE)) AS [SALDO], " +
                    "COMPRAS.TOTALPEDIDO, COMPRAS.TOTALPREVISTO, COMPRAS.OBSERVA " +
                    "FROM COMPRAS " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=COMPRAS.IDFORNECEDOR " +
                    "WHERE YEAR(COMPRAS.DATA) >= @DOANO AND YEAR(COMPRAS.DATA) <= @ATEANO ";
            if (Filial > 0)
            {
                query = query + " AND COMPRAS.FILIAL = @FILIAL ";
            }
            if (TipoLista == "A") // Aberto
            {
                query = query + " AND COMPRAS.TERMINO != @TERMINO ";
            }
            else if (TipoLista == "S") // Fechado
            {
                query = query + " AND COMPRAS.TERMINO = @TERMINO ";
            }
            query = query + " ORDER BY COMPRAS.DATA DESC ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("FILIAL", SqlDbType.Int).Value = Filial;
            sda.SelectCommand.Parameters.Add("DOANO", SqlDbType.Int).Value = DoAno;
            sda.SelectCommand.Parameters.Add("ATEANO", SqlDbType.Int).Value = AteAno;
            sda.SelectCommand.Parameters.Add("TERMINO", SqlDbType.NVarChar).Value = 'S';
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
            new GridColuna("Fornecedor", "COGNOME", 120, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Termino", "TERM", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt.Pçs", "SALDO", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("T.Pedido ", "TOTALPEDIDO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("T.Previsto ", "TOTALPREVISTO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Observa ", "OBSERVA", 150, true, DataGridViewContentAlignment.MiddleLeft),

        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["NUMERO"].DefaultCellStyle.Format = "N0";
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["SALDO"].DefaultCellStyle.Format = "N3";
            dgv.Columns["TOTALPEDIDO"].DefaultCellStyle.Format = "N2";
            dgv.Columns["TOTALPREVISTO"].DefaultCellStyle.Format = "N2";
        }
    }
}
