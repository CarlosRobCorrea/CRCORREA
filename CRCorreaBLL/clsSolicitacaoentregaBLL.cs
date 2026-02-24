using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsSolicitacaoentregaBLL : SQLFactory<clsSolicitacaoentregaInfo>
    {
        public void VerificaInfo(clsSolicitacaoentregaInfo info)
        {
            if (info.qtdeentrega == 0)
            {
                throw new Exception("Falta a quantidade de entrega");
            }

            if (info.dataentrega.ToString() == "")
            {
                throw new Exception("Falta inserir data");
            }

            if (info.idsolicitacao <= 0)
            {
                throw new Exception("Falta referenciar a solicitação");
            }
        }

        public static DataTable GridDados(Int32 id, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT SOLICITACAOENTREGA.ID, SOLICITACAOENTREGA.IDSOLICITACAO, SOLICITACAOENTREGA.DATAENTREGA, SOLICITACAOENTREGA.QTDEENTREGA  " +
                    "FROM SOLICITACAOENTREGA " +
                    "LEFT JOIN SOLICITACAO ON SOLICITACAO.ID = SOLICITACAOENTREGA.IDSOLICITACAO ";
            filtro = "WHERE SOLICITACAOENTREGA.IDSOLICITACAO = @ID ";
            filtro = filtro + " ORDER BY SOLICITACAOENTREGA.DATAENTREGA ";
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
            new GridColuna("id Solicitacao", "IDSOLICITACAO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Data Entrega", "DATAENTREGA", 120, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Entrega", "QTDEENTREGA", 110, true, DataGridViewContentAlignment.MiddleRight)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DATAENTREGA");
            dgv.Columns["DATAENTREGA"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
    }
}
