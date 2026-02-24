using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CRCorreaInfo;

namespace CRCorreaFuncoes
{
    public class clsFormsBLL : SQLFactory<clsFormsInfo>
    {
        public override bool Excluir(int id, string cnn)
        {
            SqlConnection scn = new SqlConnection(cnn);
            SqlCommand scd;
            //primeiro
            scn.Open();
            scd = new SqlCommand("Delete USUARIOFORMSOBJETO where idformsobjeto in (select id from formsobjeto where idforms= @id)", scn);
            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            scd.ExecuteNonQuery();
            scn.Close();
            //segundo
            scn.Open();
            scd = new SqlCommand("delete UsuariosForms where idforms= @id", scn);
            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            scd.ExecuteNonQuery();
            scn.Close();
            //terceiro
            scn.Open();
            scd = new SqlCommand("delete formsobjeto where idforms= @id", scn);
            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            scd.ExecuteNonQuery();
            scn.Close();
            //quarto
            scn.Open();
            scd = new SqlCommand("delete forms where id= @id", scn);
            scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            scd.ExecuteNonQuery();
            scn.Close();

            return base.Excluir(id, cnn);
        }
        
        public override int Incluir(clsFormsInfo info, string cnn)
        {
            Int32 id;

            id = base.Incluir(info, cnn);

            SqlDataAdapter sda;
            DataTable dtUsuario = new DataTable();

            clsUsuarioFormsBLL UsuariosFormsBLL = new clsUsuarioFormsBLL();
            clsUsuarioFormsInfo UsuariosFormsInfo;

            sda = new SqlDataAdapter("select id from usuario", cnn);
            sda.Fill(dtUsuario);

            foreach (DataRow row in dtUsuario.Rows)
            {   
                UsuariosFormsInfo = new clsUsuarioFormsInfo();
                UsuariosFormsInfo.idforms = id;
                UsuariosFormsInfo.idusuario = clsParser.Int32Parse(row["id"].ToString());
                UsuariosFormsInfo.permitido = "S";

                UsuariosFormsBLL.Incluir(UsuariosFormsInfo, cnn);
            }

            return id;
        }

        public override int Alterar(clsFormsInfo info, string cnn)
        {
            Int32 result;

            result = base.Alterar(info, cnn);

            SqlDataAdapter sda;
            DataTable dtUsuario = new DataTable();

            clsUsuarioFormsBLL UsuariosFormsBLL = new clsUsuarioFormsBLL();
            clsUsuarioFormsInfo UsuariosFormsInfo;

            sda = new SqlDataAdapter("select id from usuario", cnn);
            sda.Fill(dtUsuario);

            foreach (DataRow row in dtUsuario.Rows)
            {
                if (UsuariosFormsBLL.JaExiste(clsParser.Int32Parse(row["id"].ToString()), info.id, cnn) == false)
                {
                    UsuariosFormsInfo = new clsUsuarioFormsInfo();
                    UsuariosFormsInfo.idforms = info.id;
                    UsuariosFormsInfo.idusuario = clsParser.Int32Parse(row["id"].ToString());
                    UsuariosFormsInfo.permitido = "S";

                    UsuariosFormsBLL.Incluir(UsuariosFormsInfo, cnn);   
                }
            }

            return result;
        }

        public Boolean JaExiste(String nomeform, String projeto, String cnn)
        {
            Boolean result = false;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("select id from forms where projeto=@projeto and nomeform=@nomeform", scn);
            scd.Parameters.Add("@projeto", SqlDbType.NVarChar).Value = projeto;
            scd.Parameters.Add("@nomeform", SqlDbType.NVarChar).Value = nomeform;

            scn.Open();

            sdr = scd.ExecuteReader();
            if (sdr.Read() == true)
            {
                result = true;
            }

            scn.Close();

            return result;
        }

        public clsFormsInfo Carregar(String nomeform, String projeto, String cnn)
        {
            clsFormsInfo FormsInfo = null;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);

            if (projeto.IndexOf(".exe") != -1 && projeto.IndexOf(".dll") != -1)
            {
                scd = new SqlCommand("select id from forms where (projeto=@projeto or projeto=@projeto2) and nomeform=@nomeform", scn);
                scd.Parameters.Add("@projeto", SqlDbType.NVarChar).Value = projeto + ".exe";
                scd.Parameters.Add("@projeto2", SqlDbType.NVarChar).Value = projeto + ".dll";
            }
            else
            {
                scd = new SqlCommand("select id from forms where projeto=@projeto and nomeform=@nomeform", scn);
                scd.Parameters.Add("@projeto", SqlDbType.NVarChar).Value = projeto;
            }

            scd.Parameters.Add("@nomeform", SqlDbType.NVarChar).Value = nomeform;

            scn.Open();

            sdr = scd.ExecuteReader();
            if (sdr.Read() == true)
            {
                FormsInfo = Carregar(clsParser.Int32Parse(sdr["id"].ToString()), cnn);
            }

            scn.Close();

            return FormsInfo;
        }

        public static void ChecaFormsNaoExistem(List<string> forms, String cnn)
        {
            List<int> formsExcluir = new List<int>();

            clsFormsBLL formsBLL = new clsFormsBLL();

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("SELECT ID, PROJETO + '.' + NOMEFORM FROM FORMS", scn);

            scn.Open();

            sdr = scd.ExecuteReader();

            while (sdr.Read())
            {
                if (forms.IndexOf(sdr[1].ToString()) == -1)
                {
                    formsExcluir.Add(sdr.GetInt32(0));
                }
            }

            scn.Close();

            foreach (int idForm in formsExcluir)
            {
                // Apaga os objetos do formulário
                clsFormsObjetoBLL.ExcluirByForms(idForm, cnn);

                // Apaga o formulário
                formsBLL.Excluir(idForm, cnn);
            }
        }

        public static DataTable GridDados(String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT  " +
                        "ID, " +
                        "DATAC, " +
                        "PROJETO, " +
                        "NOMEFORM, " +
                        "NOME, " +                       
                        "DESCRICAO, " +
                        "LISTA " +
                    "FROM " +
                        "FORMS " +
                    "ORDER BY " +
                        "NOME ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);

            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtContratoColunas = new GridColuna[]
        {
            new GridColuna("Data Cadastro", "DATAC", 110, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Projeto", "PROJETO", 140, true, DataGridViewContentAlignment.MiddleLeft),
             new GridColuna("Nome Objeto", "NOMEFORM", 200, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Título", "NOME", 270, true, DataGridViewContentAlignment.MiddleLeft),           
            new GridColuna("Descrição", "DESCRICAO", 195, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Listar", "LISTA", 60, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt)
        {
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtContratoColunas, true);
            dgv.Columns["DATAC"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }
    }
}
