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
//using ApliBuffet.DAL;

using CRCorreaFuncoes;

namespace CRCorreaBLL
{
    public class clsNFCompraResumidaBLL : SQLFactory<clsNFCompraResumidaInfo>
    {
        public static DataTable GridDados(Int32 Mes, Int32 Ano, Int32 Filial, String TipoLista)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT NFCOMPRARESUMIDA.ID as [ID],NFCOMPRARESUMIDA.FILIAL,NFCOMPRARESUMIDA.NUMERO " +
                    ",NFCOMPRARESUMIDA.DATA,NFCOMPRARESUMIDA.DATALANCA,NFCOMPRARESUMIDA.EMITENTE,NFCOMPRARESUMIDA.IDDOCUMENTO " +
                    ",DOCFISCAL.CODIGO AS [DOCUMENTO] " +
                    ",NFCOMPRARESUMIDA.IDFORNECEDOR,CLIENTE.COGNOME AS [FORNECEDOR], NFCOMPRARESUMIDA.IDCODIGO " + 
                    ",PECAS.CODIGO AS [CODIGO], PECAS.NOME AS [NOME], NFCOMPRARESUMIDA.COMPLEMENTO " +
                    ",NFCOMPRARESUMIDA.TOTALNOTA,NFCOMPRARESUMIDA.IDHISTORICO,NFCOMPRARESUMIDA.IDCENTROCUSTO " +
                    ",NFCOMPRARESUMIDA.IDCODIGOCTABIL,NFCOMPRARESUMIDA.IDBANCOINT,NFCOMPRARESUMIDA.IDFLUXO  " +
                    ",NFCOMPRARESUMIDA.IDFLUXO01 " +
                    "FROM NFCOMPRARESUMIDA  " +
                    "LEFT JOIN CLIENTE ON CLIENTE.ID=NFCOMPRARESUMIDA.IDFORNECEDOR  " +
                    "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = NFCOMPRARESUMIDA.IDDOCUMENTO " +
                    "LEFT JOIN PECAS ON PECAS.ID = NFCOMPRARESUMIDA.IDCODIGO ";
            //FILIAL
            filtro = "";
            if (Filial > 0)
            { // filial especifica
                filtro = "NFCOMPRARESUMIDA.FILIAL = @FILIAL ";
            }
            if (TipoLista != "T")
            {
                // SE MES FOR 0 (ZERO) TRAZ TUDO DO ANO
                if (Mes != 0)
                {
                    if (filtro.Length > 2)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + " MONTH(NFCOMPRARESUMIDA.DATA) = @MES ";
                }
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + " YEAR(NFCOMPRARESUMIDA.DATA) >= @ANO ";
            }
            if (filtro.Length > 2)
            {
                filtro = "WHERE " + filtro;
            }
            filtro = filtro + " ORDER BY NFCOMPRARESUMIDA.DATA, NFCOMPRARESUMIDA.NUMERO ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@MES", SqlDbType.Int).Value = Mes;
            sda.SelectCommand.Parameters.Add("@ANO", SqlDbType.Int).Value = Ano;
            sda.SelectCommand.Parameters.Add("@Filial", SqlDbType.Int).Value = Filial;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("F", "FILIAL", 23, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nro NF", "NUMERO", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissão", "DATA", 62, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc", "DOCUMENTO", 28, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fornecedor", "FORNECEDOR", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Codigo ", "CODIGO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 290, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Complemento", "COMPLEMENTO", 190, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Total Despesa", "TOTALNOTA", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("IDFLUXO", "IDFLUXO", 0, false, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["TOTALNOTA"].DefaultCellStyle.Format = "N2";
        }
    }
}
