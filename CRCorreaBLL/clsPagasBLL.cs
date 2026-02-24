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
    public class clsPagasBLL : SQLFactory<clsPagasInfo>
    {
        public static DataTable GridDados(DateTime Datade, DateTime Dataate, Int32 Filial, String TipoLista, String TipoData)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PAGAS.ID, PAGAS.DUPLICATA, PAGAS.POSICAO , PAGAS.POSICAOFIM , PAGAS.EMISSAO, " +
                        "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, PAGAS.VENCIMENTO, " +
                        "PAGAS.VALOR, SITUACAOCOBRANCACOD.CODIGO AS [COB], PAGAS01.DATAOK, PAGAS01.VALOR AS [VALORPAGTO] " +
                        "FROM PAGAS " +
                        "LEFT JOIN PAGAS01 ON PAGAS.ID = PAGAS01.IDDUPLICATA " +
                        "LEFT JOIN CLIENTE ON CLIENTE.ID = PAGAS.IDFORNECEDOR " +
                        "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAS.IDDOCUMENTO " +
                        "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAS.IDFORMAPAGTO " +
                        "LEFT JOIN SITUACAOCOBRANCACOD ON SITUACAOCOBRANCACOD.ID = PAGAS01.IDCOBRANCACOD ";

            //FILIAL
            if (Filial > 0)
            { // filial especifica
                filtro = "PAGAS.FILIAL = @FILIAL ";
            }
            if (TipoData == "VENC")
            { // DT VENCIMENTO
                if (TipoLista == "VENC")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = " AND ";
                    }
                    filtro = "PAGAS.VENCIMENTO >= @DATADE ";
                }
                else if (TipoLista == "ATRASO")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = " AND ";
                    }
                    filtro = "PAGAS.VENCIMENTO < @DATADE ";
                }
            }
            else
            { // DT PREVISTA
                if (TipoLista == "VENC")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = " AND ";
                    }
                    filtro = "PAGAS.VENCIMENTOPREV >= @DATADE ";
                }
                else if (TipoLista == "ATRASO")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = " AND ";
                    }
                    filtro = "PAGAS.VENCIMENTOPREV < @DATADE ";
                }
            }
            if (filtro.Length > 2)
            {
                filtro = "WHERE " + filtro;
            }
            if (TipoData != "VENC")
            {
                filtro = " ORDER BY PAGAS.VENCIMENTO, PAGAS.DUPLICATA ";
            }
            else
            {
                filtro = " ORDER BY PAGAS.VENCIMENTOPREV, PAGAS.DUPLICATA ";
            }
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@DataDe", SqlDbType.DateTime).Value = Datade;
            sda.SelectCommand.Parameters.Add("@Filial", SqlDbType.Int).Value = Filial;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("F", "FILIAL", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Nro Dup", "DUPLICATA", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("P.", "POSICAO", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("P.Fim", "POSICAOFIM", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissão", "EMISSAO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc", "DOCUMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cliente", "COGNOME", 160, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Vencimento", "VENCIMENTO", 80, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Venc. Prev  ", "VENCIMENTOPREV", 80, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Valor Titulo", "VALORPAGAR", 100, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tipo", "TITULO", 40, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Observa", "OBSERVA", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tot Bruto", "TOTALORCAMENTOBRUTO", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Desconto", "TOTALDESCONTO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Frete", "TOTALFRETE", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tot.geral", "TOTALORCAMENTO", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Enviou", "CHEGOU", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Banco", "BANCO", 50, true, DataGridViewContentAlignment.MiddleCenter),

        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VALORAPAGAR"].DefaultCellStyle.Format = "N2";
        }



    }
}
