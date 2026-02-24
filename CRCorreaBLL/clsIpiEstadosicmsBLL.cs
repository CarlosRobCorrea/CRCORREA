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
    public class clsIpiEstadosicmsBLL : SQLFactory<clsIpiEstadosicmsInfo>
    {
        public DataTable GridDados(Int32 idestado, Int32 idipi, String cnn)
        {
            DataTable dt;
            String query;
            SqlDataAdapter sda;
            query = "SELECT IPIESTADOSICMS.ID,  " +
                    "IPIESTADOSICMS.IDESTADO, " +
                    "IPIESTADOSICMS.IDESTADODESTINO, " + 
                    "ESTADOSORIGEM.ESTADO AS ORIGEM, " + 
                    "ESTADOSDESTINO.ESTADO AS DESTINO, " +
                    "IPIESTADOSICMS.ALIQUOTA,  " +
                    "IPIESTADOSICMS.REDUCAO,  " +
                    "IPIESTADOSICMS.IVA,  " +
                    "IPIESTADOSICMS.REDUCAOIVA,  " +
                    "IPIESTADOSICMS.IDDIZERESNFV, " +
                    "DIZERESNFV.CODIGO, " +
                    "DIZERESNFV.NOME " + 
                    "FROM IPIESTADOSICMS " +
                    "INNER JOIN ESTADOS AS ESTADOSORIGEM ON ESTADOSORIGEM.ID = IPIESTADOSICMS.IDESTADO " +
                    "INNER JOIN ESTADOS AS ESTADOSDESTINO ON ESTADOSDESTINO.ID = IPIESTADOSICMS.IDESTADODESTINO " +
                    "LEFT JOIN DIZERESNFV ON IPIESTADOSICMS.IDDIZERESNFV = DIZERESNFV.ID " +
                    "WHERE IPIESTADOSICMS.IDESTADO = @IDESTADO " +
                    " AND IPIESTADOSICMS.IDIPI =  @IDIPI ORDER BY ESTADOSDESTINO.ESTADO ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDESTADO", SqlDbType.Int).Value = idestado;
            sda.SelectCommand.Parameters.Add("@IDIPI", SqlDbType.Int).Value = idipi;
            sda.Fill(dt);

            return dt;
        }
        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("idESTADO", "IDESTADO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("idESTADODESTINO", "IDESTADODESTINO", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("UF Origem", "ORIGEM",60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("UF Destino", "DESTINO",60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("% ICMS ", "ALIQUOTA", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("% Redução ICMS", "REDUCAO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("% IVA ", "IVA", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("% Redução IVA ", "REDUCAOIVA", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("IDDIZERESNFV ", "IDDIZERESNFV", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Tipo Nota", "CODIGO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Dizeres", "NOME", 440, true, DataGridViewContentAlignment.MiddleLeft)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "DESTINO");            
        }
    }
}
