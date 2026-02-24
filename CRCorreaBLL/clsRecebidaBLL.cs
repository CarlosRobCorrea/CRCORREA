using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsRecebidaBLL : SQLFactory<clsRecebidaInfo>
    {
        public static DataTable GridDados(DateTime Datade, Int32 Filial, String TipoLista, String TipoData)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT RECEBIDA.ID AS [ID], RECEBIDA.FILIAL, RECEBIDA.DUPLICATA, RECEBIDA.POSICAO , RECEBIDA.POSICAOFIM, RECEBIDA.EMISSAO, " +
                "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, RECEBIDA.VENCIMENTO, RECEBIDA.VENCIMENTOPREV, " +
                "RECEBIDA.VALORLIQUIDO - (RECEBIDA.VALORPAGO + RECEBIDA.VALORDEVOLVIDO) AS [VALORRECEBIDA], " +
                "SITUACAOTIPOTITULO.CODIGO AS [TITULO], RECEBIDA.OBSERVA AS [OBSERVA], RECEBIDA.CHEGOU, TAB_BANCOS.COGNOME AS [BANCO], RECEBIDA.IDNOTAFISCAL " +
                "FROM RECEBIDA " +
                "LEFT JOIN CLIENTE ON CLIENTE.ID = RECEBIDA.IDCLIENTE  " +
                "LEFT JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBIDA.IDDOCUMENTO  " +
                "LEFT JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBIDA.IDFORMAPAGTO  " +
                "LEFT JOIN TAB_BANCOS ON TAB_BANCOS.ID = RECEBIDA.IDBANCO  ";
            //FILIAL
            if (Filial > 0)
            { // filial especifica
                // filtro = "RECEBIDA.FILIAL = @FILIAL ";
            }
            if (TipoData == "VENC")
            { // DT VENCIMENTO
                if (TipoLista == "VENC")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = " AND ";
                    }
                    filtro = "RECEBIDA.VENCIMENTO >= @DATADE ";
                }
                else if (TipoLista == "ATRASO")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = " AND ";
                    }
                    filtro = "RECEBIDA.VENCIMENTO < @DATADE ";
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
                    filtro = "RECEBIDA.VENCIMENTOPREV >= @DATADE ";
                }
                else if (TipoLista == "ATRASO")
                {
                    if (filtro.Length > 2)
                    {
                        filtro = " AND ";
                    }
                    filtro = "RECEBIDA.VENCIMENTOPREV < @DATADE ";
                }
            }
            if (filtro.Length > 2)
            {
                filtro = "WHERE " + filtro;
            }
            if (TipoData != "VENC")
            {
                filtro = " ORDER BY RECEBIDA.VENCIMENTO, RECEBIDA.DUPLICATA ";
            }
            else
            {
                filtro = " ORDER BY RECEBIDA.VENCIMENTOPREV, RECEBIDA.DUPLICATA ";
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
            new GridColuna("Vencimento", "VENCIMENTO", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Venc. Prev  ", "VENCIMENTOPREV", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Valor", "VALORRECEBIDA", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tipo", "TITULO", 40, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Observa", "OBSERVA", 90, true, DataGridViewContentAlignment.MiddleCenter),
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
            dgv.Columns["VALORARECEBIDA"].DefaultCellStyle.Format = "N2";
        }

        //POR DATA PAGAMENTO
        public static DataTable GridDadosRecebidasPorPagamento(String periodo, String data_de, String data_ate)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            query = "select RECEBIDA.ID, RECEBIDA.DUPLICATA, RECEBIDA.POSICAO, " +
                            "RECEBIDA.POSICAOFIM , RECEBIDA.EMISSAO, " +
                            "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME,RECEBIDA.VENCIMENTO, RECEBIDA.VALOR, " +
                            "RECEBIDA01.DATAENVIO, RECEBIDA01.VALOR AS [VALORPAGTO] " +
                            "FROM RECEBIDA " +
                            "left JOIN RECEBIDA01 ON RECEBIDA.ID = RECEBIDA01.IDDUPLICATA " +
                            "left JOIN CLIENTE ON CLIENTE.ID = RECEBIDA.IDCLIENTE  " +
                            "left JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBIDA.IDDOCUMENTO  " +
                            "left JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBIDA.IDFORMAPAGTO "; // +
                            //"WHERE "; //RECEBIDA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";

            if (periodo == "P")
            {
                query = query + " where RECEBIDA01.DATAENVIO >= " + clsParser.SqlDateTimeFormat(data_de.ToString() + " 00:00", true) + " AND RECEBIDA01.DATAENVIO <= " + clsParser.SqlDateTimeFormat(data_ate.ToString() + " 23:59", true);
            }

            //if (periodo == "P")
            //{
            //    query = query + " AND RECEBIDA01.DATAOK >= " + clsParser.SqlDateTimeFormat(data_de.ToString() + " 00:00", true) + " AND RECEBIDA01.DATAOK <= " + clsParser.SqlDateTimeFormat(data_ate.ToString() + " 23:59", true);
            //}

            query += " order by RECEBIDA.VENCIMENTO ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);

            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunasRecebidasPorPagamento = new GridColuna[]
        {
            new GridColuna("Id.", "id", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Duplicata", "DUPLICATA",65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Pos", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Fim","POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissão", "EMISSAO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc", "DOCUMENTO", 35, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Cognome",  "COGNOME", 250, true, DataGridViewContentAlignment.MiddleLeft),           
            new GridColuna("Vencimento", "VENCIMENTO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("A Receber", "VALOR", 100, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Pagou Em", "DATAENVIO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Valor Pago", "VALORPAGTO", 100, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMontaRecebidasPorPagamento(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasRecebidasPorPagamento, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DUPLICATA");
            dgv.AllowUserToResizeColumns = true;
            dgv.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";    
            dgv.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["DATAENVIO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VALOR"].DefaultCellStyle.Format = "N2";
            dgv.Columns["VALORPAGTO"].DefaultCellStyle.Format = "N2";
        }

        //POR DATA VENCIMETO
        public static DataTable GridDadosRecebidasPorVencimento(String periodo, String data_de, String data_ate)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            query = "select RECEBIDA.ID, RECEBIDA.DUPLICATA, RECEBIDA.POSICAO, " +
                           "RECEBIDA.POSICAOFIM , RECEBIDA.EMISSAO, " +
                           "DOCFISCAL.COGNOME AS [DOCUMENTO], CLIENTE.COGNOME, RECEBIDA.VENCIMENTO, RECEBIDA.VALOR, " +
                           "RECEBIDA01.DATAENVIO, RECEBIDA01.VALOR AS [VALORPAGTO] " +
                           "FROM RECEBIDA " +
                           "left JOIN CLIENTE ON CLIENTE.ID = RECEBIDA.IDCLIENTE  " +
                           "left JOIN RECEBIDA01 ON RECEBIDA.ID = RECEBIDA01.IDDUPLICATA " +
                           "left JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBIDA.IDDOCUMENTO  " +
                           "left JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBIDA.IDFORMAPAGTO "; // +
                           //"WHERE "; //RECEBIDA.FILIAL = " + clsParser.Int32Parse(clsInfo.zfilial.ToString()) + " ";

            if (periodo == "P")
            {
                query = query + " where RECEBIDA.VENCIMENTO >= " + clsParser.SqlDateTimeFormat(data_de.ToString() + " 00:00", true) + " AND RECEBIDA.VENCIMENTO <= " + clsParser.SqlDateTimeFormat(data_ate.ToString() + " 23:59", true);
            }

            query += " order by RECEBIDA.VENCIMENTO ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);

            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunasRecebidasPorVencimento = new GridColuna[]
        {
            new GridColuna("Id.", "id", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Duplicata", "DUPLICATA",65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Pos", "POSICAO", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Fim","POSICAOFIM", 25, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissão", "EMISSAO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc", "DOCUMENTO", 35, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Cognome",  "COGNOME", 250, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Vencimento", "VENCIMENTO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("A Receber", "VALOR", 100, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Pagou Em", "DATAENVIO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Valor Pago", "VALORPAGTO", 100, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMontaRecebidasPorVencimento(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasRecebidasPorVencimento, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DUPLICATA");
            dgv.AllowUserToResizeColumns = true;
            dgv.Columns["EMISSAO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VENCIMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["DATAENVIO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["VALOR"].DefaultCellStyle.Format = "N2";
            dgv.Columns["VALORPAGTO"].DefaultCellStyle.Format = "N2";
        }
    }
}
