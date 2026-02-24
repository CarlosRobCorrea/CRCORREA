using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsPecasObservaBLL : SQLFactory<clsPecasObservaInfo>
    {
        public override int Incluir(clsPecasObservaInfo info, string cnn)
        {
            String query = "";

            query = "SELECT top 1 NUMERO + 1 FROM PECASOBSERVA where idpeca = " + info.idpeca + 
                "  ORDER BY data desc,  numero DESC";

            info.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, query));

            if (info.numero == 0)
            {
                info.numero = 1;
            }

            return base.Incluir(info, cnn);
        }


        public static DataTable GridDados(Int32 idpeca, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;

            query = "SELECT PECASOBSERVA.ID, PECASOBSERVA.IDPECA, PECASOBSERVA.NUMERO, PECASOBSERVA.DATA, PECASOBSERVA.IDEMITENTE, PECASOBSERVA.EMITENTE, PECASOBSERVA.OBSERVA " +
                    "FROM PECASOBSERVA " +
                    "WHERE IDPECA = " + idpeca + " ";
            query = query + " " + filtro + " ORDER BY PECASOBSERVA.DATA DESC";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("id", "IDPECA", 0, false, DataGridViewContentAlignment.MiddleLeft),
             new GridColuna("Número", "NUMERO", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Data", "DATA", 90, true, DataGridViewContentAlignment.MiddleCenter),
             new GridColuna("idemitente", "IDEMITENTE", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Emitente", "EMITENTE", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descrição", "OBSERVA", 700, true, DataGridViewContentAlignment.MiddleLeft),
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DATA");
        }

    }
}
