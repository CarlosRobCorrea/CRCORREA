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
    public class clsCotacao1BLL  : SQLFactory<clsCotacao1Info>
    {
        public static DataTable GridDados(Int32 id, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT COTACAO1.ID, COTACAO1.IDCOTACAO, COTACAO1.IDCODIGO, COTACAO1.COMPLEMENTO, COTACAO1.COMPLEMENTO1, COTACAO1.DESCRICAOESP, " +
            "COTACAO1.IDSITTRIBA, COTACAO1.IDSITTRIBB, " +
            "COTACAO1.MSG, COTACAO1.CONSUMO, COTACAO1.IDHISTORICO, COTACAO1.IDCENTROCUSTO, COTACAO1.IDCODIGOCTABIL, COTACAO1.TIPOPRODUTO, COTACAO1.TIPOENTRADA, " +
            "COTACAO1.IDORDEMSERVICO, COTACAO1.IDOSITEM, COTACAO1.TIPODESTINO, COTACAO1.IDDESTINO, COTACAO1.QTDEAUTORIZADA, COTACAO1.QTDEOK, COTACAO1.IDUNIDADE, " +
            "COTACAO1.IDSOLICITACAO, COTACAO1.IDPEDIDOCOMPRA, COTACAO1.IDPEDIDOCOMPRAITEM, COTACAO1.IDFORNECEDORGANHOU, COTACAO1.OBSERVAR, COTACAO1.IDCOTACAO2GANHOU, " +
            "PECAS.CODIGO, CASE PECAS.CODIGO WHEN '0' THEN COTACAO1.COMPLEMENTO ELSE PECAS.NOME END AS NOME, " +
            "COTACAO1.TOTALPREVISTO, " +
            "SOLICITACAO.NUMERO AS [NROSOL], ORDEMSERVICO.NUMERO AS [NROOS], COMPRAS.NUMERO AS [PEDCOMPRANRO], UNIDADE.CODIGO AS [UNID] " +
            ", CLIENTEPROSPECCAO.COGNOME AS FORNECEDOR " +
            "FROM COTACAO1 " +
            "LEFT JOIN PECAS ON COTACAO1.IDCODIGO = PECAS.ID " +
            "LEFT JOIN SOLICITACAO ON SOLICITACAO.ID = COTACAO1.IDSOLICITACAO " +
            "LEFT JOIN UNIDADE ON COTACAO1.IDUNIDADE = UNIDADE.ID " +
            "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = COTACAO1.IDORDEMSERVICO " +
            "LEFT JOIN COMPRAS ON COMPRAS.ID = COTACAO1.IDPEDIDOCOMPRA " +
            "LEFT JOIN CLIENTEPROSPECCAO ON CLIENTEPROSPECCAO.ID = COTACAO1.IDFORNECEDORGANHOU " +
            "WHERE COTACAO1.IDCOTACAO= @IDCOTACAO ";
            filtro = filtro + " ORDER BY PECAS.CODIGO ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDCOTACAO", SqlDbType.Int).Value = id;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id.", "ID", 10, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Id. Cotacao", "IDCOTACAO", 10, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt Aut", "QTDEAUTORIZADA", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Un.", "UNID", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Código", "CODIGO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 380, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Total Previsto", "TOTALPREVISTO", 70, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("id Fornecedor", "IDFORNECEDORGANHOU", 10, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("??", "IDCOTACAO2GANHOU", 10, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nº O.S.", "NROOS", 40, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Nº Sol.", "NROSOL", 40, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Nº P.C.", "PEDCOMPRANRO", 40, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Fornecedor", "FORNECEDOR", 120, true, DataGridViewContentAlignment.MiddleLeft)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
/*            dgv.Columns["DATAMONTAGEM"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["DATAFECHAMENTO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["TUDOFECHADOEM"].DefaultCellStyle.Format = "dd/MM/yyyy";*/
            dgv.Columns["TOTALPREVISTO"].DefaultCellStyle.Format = "N2";
            
        }

        public override Boolean Excluir(Int32 id, String cnn)
        {
            clsCotacaoentregaBLL clsCotacaoentregaBLL = new clsCotacaoentregaBLL();
            clsCotacaoentregaBLL.ExcluirEntregas_cotacao1(id, cnn);

            clsCotacao2BLL clsCotacao2BLL = new clsCotacao2BLL();
            clsCotacao2BLL.ExcluirCotacao2_cotacao1(id, cnn);

            return base.Excluir(id, cnn);
        }

    }
}
