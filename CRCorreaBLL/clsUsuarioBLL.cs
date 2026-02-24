using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsUsuarioBLL : SQLFactory<clsUsuarioInfo>
    {
        public override int Incluir(clsUsuarioInfo info, string cnn)
        {
            VerificaInfo(info, cnn);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsUsuarioInfo info, string cnn)
        {
            VerificaInfo(info, cnn);

            return base.Alterar(info, cnn);
        }

        public override bool Excluir(int id, string cnn)
        {
            SqlConnection scn = new SqlConnection(cnn);
            SqlCommand scd;

            scn.Open();
            scd = new SqlCommand("delete UsuariosForms where idusuario=@idusuario", scn);
            scd.Parameters.Add("@idusuario", SqlDbType.Int).Value = id;
            scd.ExecuteNonQuery();

            scd = new SqlCommand("delete USUARIOFORMSOBJETO where idusuario=@idusuario", scn);
            scd.Parameters.Add("@idusuario", SqlDbType.Int).Value = id;
            scd.ExecuteNonQuery();
            scn.Close();

            return base.Excluir(id, cnn);
        }

        // Carregar2 
        public clsUsuarioInfo Carregar(String usuario, String cnn)
        {
            
            List<clsSqlFactoryValue> parametros = new List<clsSqlFactoryValue>();
            clsSqlFactoryValue parametroUSUARIO = new clsSqlFactoryValue();
            parametroUSUARIO.Nome = "USUARIO";
            parametroUSUARIO.Valor = usuario;


            parametros.Add(parametroUSUARIO);

            return Carregar(parametros, cnn);
        }

        // primeiro método
        public static DataTable GridDados(String ativo, String cnn)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            bool queryWhere = false;

            query = "select " +
                       "id, " +
                       "usuario, " +
                       "senha, " +
                       "CONVERT(nvarchar(10), dataval, 103) as dataval, " +
                       "CONVERT(nvarchar(10), datault, 103) as datault, " +
                       "case " +
                           "when ativo = 1 then  'S' " +
                           "when ativo = 0 then  'N' " +
                       "end as ativo, " +
                       "CONVERT(nvarchar(5), horaini, 108) as horaini, " +
                       "CONVERT(nvarchar(5), horafim, 108) as horafim, " +
                       "case " +
                           "when weekend = 1 then 'S' " +
                           "when weekend = 0 then 'N' " +
                       "end as weekend, " +
                       "CONVERT(nvarchar(10), trocar, 103) as trocar, " +
                       "trocasenha, " +
                       "email, " +
                       "nivelusuario, " +
                       "case " +
                           "when grupo = 'C' then '' " +
                           "when grupo = 'D' then 'Diretor' " +
                           "when grupo = 'G' then 'Gerente' " +
                           "when grupo = 'V' then 'Vendedor' " +
                           "else 'Normal' " +
                       "end as grupo_usuario, " +
                       "emailsenha " +
                   "From " +
                       "usuario ";

            if (clsInfo.zusuario.ToUpper() != "SUPERVISOR")
            {
                queryWhere = true;
                query += "where USUARIO != 'SUPERVISOR' ";
            }

            if (ativo == "S" || ativo == "N")
            {
                if (!queryWhere)
                {
                    query += "where ";
                }
                else
                {
                    queryWhere = true;
                    query += "and ";
                }

                query += "ativo = @ativo ";
            }

            query += "order by " +
                        "usuario.dataval desc";

            sda = new SqlDataAdapter(query, cnn);

            if (ativo == "S")
            {
                sda.SelectCommand.Parameters.Add("@ativo", SqlDbType.Bit).Value = true;
            }
            else if (ativo == "N")
            {
                sda.SelectCommand.Parameters.Add("@ativo", SqlDbType.Bit).Value = false;
            }

            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtUsuarioColunas = new GridColuna[]
        {
            new GridColuna("Usuário", "USUARIO", 150, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("A.", "ATIVO", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("FS", "WEEKEND", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Início", "HORAINI", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fim", "HORAFIM", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Validade", "DATAVAL", 90, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Último Acesso", "DATAULT", 90, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Nível", "NIVELUSUARIO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Grupo", "GRUPO_USUARIO", 90, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Email", "email", 200, true, DataGridViewContentAlignment.MiddleLeft)
        };  // Objeto

        // segundo método
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {


            dgv.DataSource = dt;

            clsGridHelper.MontaGrid2(dgv, dtUsuarioColunas, true);
        }

        public void VerificaInfo(clsUsuarioInfo info, String cnn)
        {
            if (info.Usuario == null || info.Usuario.Length < 2)
            {
                throw new Exception("Usuário não pode ter menos que duas letras");
            }

            if (Procedure.PesquisaoPrimeiro(cnn, "select usuario from usuario where usuario='" + info.Usuario + "'"

                + " and id <>" + info.Id + " and id > 0 ", "").ToString().Length > 1)
            {
                throw new Exception("Usuário já existe");
            }

        }

    }
}
