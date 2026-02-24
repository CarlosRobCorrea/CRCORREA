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
    public class clsSolicitacaoFornecedorBLL  : SQLFactory<clsSolicitacaoFornecedorInfo>
    {
        public static DataTable GridDados(Int32 id, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "select solicitacaofornecedor.id, " +
                 " solicitacaofornecedor.idsolicitacao, " +
                " solicitacaofornecedor.idfornecedor, " +
                " clienteprospeccao.cognome as [fornecedor], " +
                " solicitacaofornecedor.preferencia " +               
                 "from solicitacaofornecedor " +
                "inner join solicitacao on solicitacao.id = solicitacaofornecedor.idsolicitacao " +
                "left join clienteprospeccao on clienteprospeccao.id = solicitacaofornecedor.idfornecedor " +
                " WHERE SOLICITACAOFORNECEDOR.IDSOLICITACAO = @ID ";
            filtro = filtro + " ORDER BY clienteprospeccao.cognome ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("idSolicitacao", "idsolicitacao", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("idFornecedor", "idfornecedor", 0, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fornecedor", "fornecedor", 110, true, DataGridViewContentAlignment.MiddleLeft),
             new GridColuna("Preferência", "preferencia", 60, true, DataGridViewContentAlignment.MiddleLeft)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "fornecedor");
        }
    }
}
