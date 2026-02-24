using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaFuncoes
{
    public class clsUsuarioFormsBLL : SQLFactory<clsUsuarioFormsInfo>
    {
        public clsUsuarioFormsInfo Carregar(Int32 idusuario, Int32 idforms, String cnn)
        {
            clsUsuarioFormsInfo result = null;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("select id from UsuarioForms where idforms=@idforms and idusuario=@idusuario", scn);
            scd.Parameters.Add("@idforms", SqlDbType.Int).Value = idforms;
            scd.Parameters.Add("@idusuario", SqlDbType.Int).Value = idusuario;

            scn.Open();

            sdr = scd.ExecuteReader();
            if (sdr.Read() == true)
            {
                result = Carregar(clsParser.Int32Parse(sdr["id"].ToString()), cnn);
            }

            scn.Close();

            return result;
        }

        public Boolean JaExiste(Int32 idusuario, Int32 idforms, String cnn)
        {
            Boolean result = false;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("select id from UsuarioForms where idforms=@idforms and idusuario=@idusuario", scn);
            scd.Parameters.Add("@idforms", SqlDbType.Int).Value = idforms;
            scd.Parameters.Add("@idusuario", SqlDbType.Int).Value = idusuario;

            scn.Open();

            sdr = scd.ExecuteReader();
            if (sdr.Read() == true)
            {
                result = true;
            }

            scn.Close();

            return result;
        }

        public static Boolean UsuarioPermitido(Int32 idusuario, String projeto, String nomeform, String cnn, Form frm)
        {
            Boolean result = false;

            if (projeto.IndexOf(',') != -1)
            {
                projeto = projeto.Substring(0, projeto.IndexOf(','));
            }

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("select top 1 " +
                                    "forms.lista, " +
                                    "usuario.gruposystem, " +
                                    "UsuarioForms.permitido " +
                                "from " +
                                    "usuario " +
                                        "inner join UsuarioForms on UsuarioForms.idusuario = usuario.id " +
                                        "inner join forms on forms.id = UsuarioForms.idforms " +
                                "where " +
                                    "forms.projeto=@projeto and " +
                                    "forms.nomeform=@nomeform and " +
                                    "UsuarioForms.idusuario=@idusuario", scn);
            scd.Parameters.Add("@projeto", SqlDbType.NVarChar).Value = projeto;
            scd.Parameters.Add("@nomeform", SqlDbType.NVarChar).Value = nomeform;
            scd.Parameters.Add("@idusuario", SqlDbType.Int).Value = idusuario;

            scn.Open();

            sdr = scd.ExecuteReader();

            if (sdr.Read() == true)
            {
                if (sdr["lista"].ToString() == "N")
                {
                    result = true;
                }
                else if (sdr["gruposystem"].ToString() == "S")
                {
                    result = true;
                }
                else if (sdr["permitido"].ToString() == "S")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                scn.Close();

                // ############################################
                // Registra o Formúlário e seus controles
                clsFormsInfo FormsInfo = new clsFormsInfo();
                clsFormsBLL FormsBLL = new clsFormsBLL();

                if (FormsBLL.JaExiste(frm.Name, projeto, cnn) == true)
                {
                    FormsInfo = FormsBLL.Carregar(frm.Name, projeto, cnn);
                }
                else
                {
//                    FormsInfo = new clsFormsInfo();
                    FormsInfo.datac = DateTime.Now;
                    FormsInfo.nomeform = frm.Name;
                    FormsInfo.projeto = projeto;
                    FormsInfo.lista = "S";
                }

                if (frm.Text == null)
                {
                    throw new Exception("Formulário " + frm.Name + " não possue o campo Text preenchido.");
                }
                else
                {
                    FormsInfo.nome = frm.Text;
                }

                if (frm.AccessibleDescription == null)
                {
                    FormsInfo.descricao = "";
                }
                else
                {
                    FormsInfo.descricao = frm.AccessibleDescription;
                }

                if (FormsInfo.id == 0)
                {
                    FormsInfo.id = FormsBLL.Incluir(FormsInfo, cnn);
                }
                else
                {
                    FormsBLL.Alterar(FormsInfo, cnn);
                }

                foreach (Control ctl in frm.Controls)
                {
                    clsFormHelper.CheckControl(ctl, 0, FormsInfo.id, cnn);
                }

                result = true;
            }

            scn.Close();

            return result;
        }

        public static DataTable GridDados(Int32 idusuario, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT  " +
                        "UsuarioForms.*, " +
                        "FORMS.NOME, " +
                        "FORMS.NOMEFORM, " +
                        "FORMS.DESCRICAO " +
                    "FROM " +
                        "UsuarioForms " +
                            "INNER JOIN FORMS ON FORMS.ID = UsuarioForms.IDFORMS " +
                    "WHERE " +
                        "FORMS.LISTA = 'S' AND " +
                        "UsuarioForms.IDUSUARIO = @IDUSUARIO " +
                    "ORDER BY " +
                        "FORMS.NOME ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = idusuario;

            sda.Fill(dt);

            return dt;
        }

        public static DataTable GridDados2(Int32 idusuario, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "(SELECT TOP 1 ID FROM UsuarioForms WHERE IDFORMS = FORMS.ID AND IDUSUARIO = @IDUSUARIO) AS ID, " +
                        "FORMS.ID as FORMSID, " +
                        "FORMS.PROJETO, " +
                        "FORMS.NOME, " +
                        "FORMS.NOMEFORM, " +
                        "FORMS.DESCRICAO, " +
                        "(SELECT TOP 1 PERMITIDO FROM UsuarioForms WHERE IDFORMS = FORMS.ID AND IDUSUARIO = @IDUSUARIO) AS PERMITIDO " +
                    "FROM " +
                        "FORMS " +
                    "WHERE " +
                        "FORMS.LISTA = 'S' " +
                    "ORDER BY " +
                        "FORMS.NOME ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = idusuario;

            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (row["PERMITIDO"] == null ||
                    row["PERMITIDO"].ToString() == string.Empty)
                {
                    row["PERMITIDO"] = "S";
                }
            }

            dt.AcceptChanges();

            return dt;
        }

        private static GridColuna[] dtContratoColunas = new GridColuna[]
        {
            new GridColuna("Nome", "NOME", 220, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome Objeto", "NOMEFORM", 220, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("P.", "PERMITIDO", 30, true, DataGridViewContentAlignment.MiddleCenter)
        };

        private static GridColuna[] dtUusarioForms2Colunas = new GridColuna[]
        {
            new GridColuna("Projeto", "PROJETO", 110, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 110, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome Objeto", "NOMEFORM", 220, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("P.", "PERMITIDO", 30, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt)
        {
            dgv.DataSource = dt;


            clsGridHelper.MontaGrid2(dgv, dtContratoColunas, true);
        }

        public static void GridMonta2(DataGridView dgv, DataTable dt)
        {
            dgv.DataSource = dt;


            clsGridHelper.MontaGrid2(dgv, dtUusarioForms2Colunas, true);
        }
    }
}
