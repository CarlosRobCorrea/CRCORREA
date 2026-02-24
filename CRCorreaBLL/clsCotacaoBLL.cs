using CRCorreaInfo;
using CRCorreaFuncoes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsCotacaoBLL : SQLFactory<clsCotacaoInfo>
    {
        public override int Incluir(clsCotacaoInfo info, string cnn)
        {
            info.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                "SELECT MAX(NUMERO + 1) FROM COTACAO where  YEAR(DATAMONTAGEM) = YEAR(GETDATE()) AND FILIAL = " + clsInfo.zfilial));

            if (info.numero == 0)
            {
                info.numero = 1;
            }

            return base.Incluir(info, cnn);
        }

        public static DataTable GridDados(Int32 Filial, String TipoLista)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT " +
                            "id, " +
                            "filial, " +
                            "numero, " + 
                            "datamontagem, " +
                            "datafechamento, " +
                            "tudofechadoem, " +
                            "comprador, " +
                            "termino, " +
                            "totalprevisto, " +
                            "observar, " +
                            "motivoreprovado, " +
                            "respostacomprador, " +
                            "ano, " +
                            "idautorizante " +
                    "from " +
                            "cotacao ";
            filtro = "WHERE " +
                            "COTACAO.NUMERO > @NUMERO ";
            //FILIAL
            if (Filial > 0)
            { // filial especifica
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "COTACAO.FILIAL = @FILIAL ";
            }
            if (TipoLista == "A")  // Em Aprovação
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "COTACAO.TERMINO = @TERMINO ";
            }
            else if (TipoLista == "S") // Fechado com pedido de compra
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "COTACAO.TERMINO = @TERMINO ";

            }
            else if (TipoLista == "N") // Cotação em Aberto/Andamento
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "COTACAO.TERMINO = @TERMINO ";

            }
            else if (TipoLista == "R") // Cotação Reprovadas
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "COTACAO.TERMINO = @TERMINO ";

            }
            else if (TipoLista == "O") // Cotação Ok - Aguardando Emitir Pedido de Compra
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "COTACAO.TERMINO = @TERMINO ";

            }
            else if (TipoLista == "F") // Cotação Finalizada - Encerradas com Pedido de Compra Emitido
            {
                if (filtro.Length > 2)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "COTACAO.TERMINO = @TERMINO ";

            }
            else
            {  // Todas as Cotações
            }

            filtro = filtro + " ORDER BY COTACAO.DATAMONTAGEM DESC, COTACAO.NUMERO DESC  ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("NUMERO", SqlDbType.Int).Value = 0;
            sda.SelectCommand.Parameters.Add("TERMINO", SqlDbType.NVarChar).Value = TipoLista;
            sda.SelectCommand.Parameters.Add("FILIAL", SqlDbType.Int).Value = Filial;  

            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 10, false, DataGridViewContentAlignment.MiddleRight), 
            new GridColuna("F", "FILIAL", 15, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("NºCot", "NUMERO", 40, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("D.Monta", "DATAMONTAGEM", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("D.Fecha", "DATAFECHAMENTO", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("D.Compr", "TUDOFECHADOEM", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Comprador", "COMPRADOR", 110, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("T", "TERMINO", 15, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Total Prev.", "TOTALPREVISTO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Obs.", "OBSERVAR", 10, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Motivo", "MOTIVOREPROVADO", 10, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Resposta", "RESPOSTACOMPRADOR", 10, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Ano", "ANO", 10, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Id. Autorizante", "IDAUTORIZANTE", 10, false, DataGridViewContentAlignment.MiddleRight)

        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["NUMERO"].DefaultCellStyle.Format = "N0";
            dgv.Columns["DATAMONTAGEM"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["DATAFECHAMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["TUDOFECHADOEM"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["TOTALPREVISTO"].DefaultCellStyle.Format = "N2";
        }
    }
}
