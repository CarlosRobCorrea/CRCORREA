using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace CRCorreaBLL
{
    public class clsSolicitacaoBLL : SQLFactory<clsSolicitacaoInfo>
    {
        public void VerificaInfo(clsSolicitacaoInfo info)
        {
            if (info.area.ToString() == "")
            {
                throw new Exception("Falta um departamento.");
            }

            if (info.idsolicitante <= 0)
            {
                throw new Exception("Falta um solicitante.");
            }
        }

        public static DataTable GridDados(DateTime Datade, DateTime Dataate, Int32 Filial, String TipoLista)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT SOLICITACAO.ID, SOLICITACAO.FILIAL, SOLICITACAO.NUMERO AS [NROSOLICITA],SOLICITACAO.DATA, PECAS.CODIGO, " +
                    "CASE PECAS.CODIGO WHEN '0' THEN SOLICITACAO.COMPLEMENTO ELSE PECAS.NOME END as DESCRICAO, " +
                    "SOLICITACAO.QTDESOL, UNIDADE.CODIGO AS [UNID], SOLICITACAO.APROVADOFAB, SOLICITACAO.APROVADOGER, " +
                    "ORDEMSERVICO.NUMERO AS [NROOS], PECASDESTINO.CODIGO AS [CODIGODESTINO], SOLICITACAO.EMITENTE, " +
                    "COTACAO.NUMERO AS [COTANUMERO], COMPRAS.NUMERO AS [PEDCOMPRA] " +
                    "FROM SOLICITACAO " +
                    "LEFT JOIN PECAS ON SOLICITACAO.IDCODIGO = PECAS.ID " +
                    "LEFT JOIN PECAS PECASDESTINO ON SOLICITACAO.IDDESTINO = PECASDESTINO.ID " +
                    "LEFT JOIN UNIDADE ON UNIDADE.ID= SOLICITACAO.IDUNID " +
                    "LEFT JOIN COTACAO on COTACAO.ID = SOLICITACAO.IDCOTACAO " +
                    "LEFT JOIN COMPRAS on COMPRAS.ID = SOLICITACAO.IDPEDIDOCOMPRA " +
                    "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = SOLICITACAO.IDOS " +
                    " WHERE SOLICITACAO.NUMERO > 0 ";
            //FILIAL
            if (Filial > 0)
            { // filial especifica
                filtro = filtro + " AND SOLICITACAO.FILIAL = @FILIAL ";
            }
            if (TipoLista == "A")  // em aberto sem pedido de compra
            {
                filtro = filtro + " AND COMPRAS.NUMERO = @PEDCOMPRA AND COTACAO.NUMERO = @NUMERO ";
            }
            else if (TipoLista == "F") // fechado com pedido de compra
            {
                filtro = filtro + " AND COMPRAS.NUMERO > @PEDCOMPRA ";
            }
            else if (TipoLista == "C") // NA COTAÇÃO ELE MOSTRA O QUE TEM COTAÇÃO
            {
                filtro = filtro + " AND COTACAO.NUMERO > @NUMERO  AND COMPRAS.NUMERO = @PEDCOMPRA ";
            }

            if (Datade != DateTime.MinValue && Dataate != DateTime.MaxValue)
            {
                filtro += " and solicitacao.data >= @DATAI and solicitacao.data <= @DATAF ";
            }

            if (TipoLista == "C") // NA COTAÇÃO ELE MOSTRA O QUE NÃO TEM COTAÇÃO
            {
                filtro = filtro + " ORDER BY PECAS.CODIGO ";
            }
            else
            {
                filtro = filtro + " ORDER BY SOLICITACAO.NUMERO ";
            }
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@PEDCOMPRA", SqlDbType.Int).Value = 0;
            sda.SelectCommand.Parameters.Add("@NUMERO", SqlDbType.Int).Value = 0;

            if (Datade != DateTime.MinValue && Dataate != DateTime.MaxValue)
            {
                sda.SelectCommand.Parameters.Add("@DATAI", SqlDbType.DateTime).Value = Datade;
                sda.SelectCommand.Parameters.Add("@DATAF", SqlDbType.DateTime).Value = Dataate;
            }

            if (Filial > 0)
            {
                sda.SelectCommand.Parameters.Add("@FILIAL", SqlDbType.Int).Value = Filial;
            }

            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Filial", "FILIAL", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nr. Sol", "NROSOLICITA", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissão", "DATA", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Codigo", "CODIGO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descrição", "DESCRICAO", 200, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qtde Sol", "QTDESOL", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNID", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Ap.Fab", "APROVADOFAB", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Ap.Ger", "APROVADOGER", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("O.S.", "NROOS", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Cod Destino", "CODIGODESTINO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Emitente", "EMITENTE", 70, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Cotação", "COTANUMERO", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("P.Comp", "PEDCOMPRA", 50, true, DataGridViewContentAlignment.MiddleRight)

        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NROSOLICITA");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        public override Int32 Incluir(clsSolicitacaoInfo info, String cnn)
        {
            VerificaInfo(info);

            SqlConnection scn = new SqlConnection(cnn);
            SqlCommand scd = new SqlCommand("SELECT TOP 1 NUMERO FROM SOLICITACAO WHERE YEAR(@DATA) = YEAR(GETDATE()) ORDER BY NUMERO DESC", scn);
            SqlDataReader sdr;

            scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = info.data;

            scn.Open();
            sdr = scd.ExecuteReader();

            if (sdr.Read() == true)
            {
                info.numero = clsParser.Int32Parse(sdr[0].ToString()) + 1;
            }
            else
            {
                info.numero = 1;
            }
            scn.Close();

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsSolicitacaoInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }
    }
}
