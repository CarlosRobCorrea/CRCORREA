using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorreaFuncoes
{
    public class clsFormsObjetoBLL : SQLFactory<clsFormsObjetoInfo>
    {
        public override int Incluir(clsFormsObjetoInfo info, string cnn)
        {
            Int32 id;

            id = base.Incluir(info, cnn);

            SqlDataAdapter sda;
            DataTable dtUsuario = new DataTable();

            clsUsuarioFormsObjetoBLL UsuariosFormsObjetoBLL = new clsUsuarioFormsObjetoBLL();
            clsUsuarioFormsObjetoInfo UsuariosFormsObjetoInfo;

            sda = new SqlDataAdapter("select id from usuario", cnn);
            sda.Fill(dtUsuario);

            foreach (DataRow row in dtUsuario.Rows)
            {
                UsuariosFormsObjetoInfo = new clsUsuarioFormsObjetoInfo();
                UsuariosFormsObjetoInfo.idformsobjeto = id;
                UsuariosFormsObjetoInfo.idusuario = clsParser.Int32Parse(row["id"].ToString());
                UsuariosFormsObjetoInfo.ativo = "S";
                UsuariosFormsObjetoInfo.visivel = "S";

                UsuariosFormsObjetoBLL.Incluir(UsuariosFormsObjetoInfo, cnn);
            }

            return id;
        }

        public override int Alterar(clsFormsObjetoInfo info, string cnn)
        {
            Int32 result;

            result = base.Alterar(info, cnn);

            SqlDataAdapter sda;
            DataTable dtUsuario = new DataTable();

            clsUsuarioFormsObjetoBLL UsuariosFormsObjetoBLL = new clsUsuarioFormsObjetoBLL();
            clsUsuarioFormsObjetoInfo UsuariosFormsObjetoInfo;

            sda = new SqlDataAdapter("select id from usuario", cnn);
            sda.Fill(dtUsuario);

            foreach (DataRow row in dtUsuario.Rows)
            {
                if (UsuariosFormsObjetoBLL.JaExiste(clsParser.Int32Parse(row["id"].ToString()), info.id, cnn) == false)
                {
                    UsuariosFormsObjetoInfo = new clsUsuarioFormsObjetoInfo();
                    UsuariosFormsObjetoInfo.idformsobjeto = info.id;
                    UsuariosFormsObjetoInfo.idusuario = clsParser.Int32Parse(row["id"].ToString());
                    UsuariosFormsObjetoInfo.ativo = "S";
                    UsuariosFormsObjetoInfo.visivel = "S";

                    UsuariosFormsObjetoBLL.Incluir(UsuariosFormsObjetoInfo, cnn);
                }
            }

            return result;
        }

        public static void ExcluirByForms(int idForms, string cnn)
        {
            SqlConnection scn = new SqlConnection(cnn);
            SqlCommand scd = new SqlCommand("DELETE FORMSOBJETO WHERE IDFORMS = @IDFORMS", scn);

            scd.Parameters.Add("@IDFORMS", SqlDbType.Int).Value = idForms;

            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();
        }

        public Boolean JaExiste(String nomeobjeto, Int32 idformulario, String cnn)
        {
            Boolean result = false;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("select id from formsobjeto where nomeobjeto=@nomeobjeto and idforms=@idforms", scn);
            scd.Parameters.Add("@nomeobjeto", SqlDbType.NVarChar).Value = nomeobjeto;
            scd.Parameters.Add("@idforms", SqlDbType.Int).Value = idformulario;

            scn.Open();

            sdr = scd.ExecuteReader();
            if (sdr.Read() == true)
            {
                result = true;
            }

            scn.Close();

            return result;
        }

        public clsFormsObjetoInfo Carregar(String nomeobjeto, Int32 idformulario, String cnn)
        {
            clsFormsObjetoInfo result = null;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("select id from formsobjeto where nomeobjeto=@nomeobjeto and idforms=@idforms", scn);
            scd.Parameters.Add("@nomeobjeto", SqlDbType.NVarChar).Value = nomeobjeto;
            scd.Parameters.Add("@idforms", SqlDbType.Int).Value = idformulario;

            scn.Open();

            sdr = scd.ExecuteReader();
            if (sdr.Read() == true)
            {
                result = Carregar(clsParser.Int32Parse(sdr["id"].ToString()), cnn);
            }

            scn.Close();

            return result;
        }

        public static DataTable GridDados(String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
                        "IDFORMS, " +
                        "IDFORMSOBJETO, " +
                        "NOME, " +
                        "NOMEOBJETO, " +
                        "CASE REVERSE(SUBSTRING(REVERSE(TIPO), 0, CHARINDEX('.', REVERSE(TIPO)))) " +
                            "WHEN 'TextBox' THEN 'Caixa de Texto' " +
                            "WHEN 'Label' THEN 'Etiqueta' " +
                            "WHEN 'GroupBox' THEN 'Agrupamento' " +
                            "WHEN 'RadioButton' THEN 'Opção' " +
                            "WHEN 'Button' THEN 'Botão' " +
                            "WHEN 'ToolStripButton' THEN 'Botão' " +
                            "WHEN 'MaskedTextBox' THEN 'Caixa de Texto' " +
                            "WHEN 'ListBox' THEN 'Lista de Opções' " +
                            "WHEN 'ComboBox' THEN 'Lista de Opções' " +
                            "WHEN 'CheckBox' THEN 'Caixa de Seleção' " +
                            "WHEN 'Panel' THEN 'Área de Controle' " +
                            "WHEN 'ToolStrip' THEN 'Barra de Opções' " +
                            "WHEN 'DataGridView' THEN 'Grade de Dados' " +
                            "WHEN 'TabPage' THEN 'Guia/Página' " +
                            "WHEN 'TabControl' THEN 'Coleção de Guias/Páginas' " +
                            "WHEN 'PictureBox' THEN 'Caixa de Imagem' " +
                            "WHEN 'StatusStrip' THEN 'Barra de Opções' " +
                            "WHEN 'ProgressBar' THEN 'Barra de Progresso' " +
                            "WHEN 'ToolStripMenuItem' THEN 'Item do Menu' " +
                            "WHEN 'MenuStrip' THEN 'Barra Principal Menu' " +
		                    "else REVERSE(SUBSTRING(REVERSE(TIPO), 0, CHARINDEX('.', REVERSE(TIPO)))) " +
		                "END AS TIPO, " +
                        "ATIVO, " +
                        "VISIVEL, " +
                        "LISTA, " +
                        "POSDIF " +
                    "FROM " +
                        "FORMSOBJETO " +
                    "ORDER BY " +
                        "IDFORMS, IDFORMSOBJETO, POSDIF ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);

            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtContratoColunas = new GridColuna[]
        {
            new GridColuna("Nome", "NOME", 139, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome Objeto", "NOMEOBJETO", 139, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tipo", "TIPO", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("A.", "ATIVO", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("V.", "VISIVEL", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("L.", "LISTA", 30, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt)
        {
            dgv.DataSource = dt;

           
            clsGridHelper.MontaGrid2(dgv, dtContratoColunas, true);
        }
    }
}
