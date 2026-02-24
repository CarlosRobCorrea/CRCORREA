using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsSittributariabBLL : SQLFactory<clsSittributariabInfo>
    {
        //Incluindo Veiculo
        public override int Incluir(clsSittributariabInfo info, string cnn)
        {
            VerificaInfo(info);
            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsSittributariabInfo info, String cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public void VerificaInfo(clsSittributariabInfo info)
        {
            

            if (info.codigo.ToString().Length > 3)
            {
                throw new Exception("Codigo inválido.");
            }
            else if (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select codigo from Sittributariab " +                
                " where codigo = '" + info.codigo + "'" + 
                " and id <> " + info.id, "0") != "0")
            {
                throw new Exception("Codigo já Cadastrado");
            }
            else if (info.nome.ToString().Length > 100)
            {
                throw new Exception("Nome excedeu 100 caractéres");
            }
            else if (info.idreferencia <= 0)
            {
                throw new Exception("Referencia invalida");
            }
        }

        public DataTable GridDados(String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT Sittributariab.ID as id, Sittributariab.CODIGO as codigo, Sittributariab.NOME as nome, " +
                    " SITTRIBUTARIAB.idreferencia as idreferencia, regimetributario.codigo + '-' + regimetributario.nome as codreferencia  " +
                    " FROM SITTRIBUTARIAB inner join regimetributario on SITTRIBUTARIAB.idreferencia = regimetributario.id  " +
                    " ORDER BY CODIGO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDados(String campo, String filtro, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            if (filtro == null)
            {
                filtro = "";
            }

            if (filtro == "")
            {
                query = "SELECT Sittributariab.ID as id, Sittributariab.CODIGO as codigo, Sittributariab.NOME as nome, " +
                    " SITTRIBUTARIAB.idreferencia as idreferencia, regimetributario.codigo + '-' + regimetributario.nome as codreferencia  " +
                    " FROM SITTRIBUTARIAB inner join regimetributario on SITTRIBUTARIAB.idreferencia = regimetributario.id  " +
                    " ORDER BY CODIGO ";
            }
            else
            {
                query = "SELECT Sittributariab.ID as id, Sittributariab.CODIGO as codigo, Sittributariab.NOME as nome, " +
                    " SITTRIBUTARIAB.idreferencia as idreferencia, regimetributario.codigo + '-' + regimetributario.nome as codreferencia  " +
                    " FROM SITTRIBUTARIAB inner join regimetributario on SITTRIBUTARIAB.idreferencia = regimetributario.id  " +
                    " WHERE " + campo + " = '" + filtro + "'";
            }

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);

            if (filtro != "")
            {
                sda.SelectCommand.Parameters.Add("@" + campo, SqlDbType.NVarChar).Value = filtro;
            }

            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna ("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna ("Codigo" , "codigo", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna ("Nome", "nome", 200, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna ("idReferencia", "idreferencia", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna ("Referencia", "codreferencia", 420, true, DataGridViewContentAlignment.MiddleLeft)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);

            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);

            clsGridHelper.SelecionaLinha(id, dgv, "codigo"); 
        }

    }
}

