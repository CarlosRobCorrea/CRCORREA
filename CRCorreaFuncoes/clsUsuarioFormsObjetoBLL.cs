using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCorreaFuncoes
{
    public class clsUsuarioFormsObjetoBLL : SQLFactory<clsUsuarioFormsObjetoInfo>
    {
        public override int Incluir(clsUsuarioFormsObjetoInfo info, string cnn)
        {
            if (!DataValid(info))
            {
                return 0;
            }

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsUsuarioFormsObjetoInfo info, string cnn)
        {
            if (!DataValid(info))
            {
                if (info.id > 0)
                {
                    Excluir(info.id, cnn);
                }
            }

            return base.Alterar(info, cnn);
        }

        public bool DataValid(clsUsuarioFormsObjetoInfo info)
        {
            if (info == null)
                return false;

            if (info.visivel == null ||
                info.ativo == null)
                return false;

            // registro ativo e visível é o valor default
            // para não gerar registros desnecessários devem
            // ser desconsiderados.
            if (info.ativo == "S" &&
                info.visivel == "S")
                return false;

            return true;
        }

        public clsUsuarioFormsObjetoInfo Carregar(Int32 idusuario, Int32 idformsobjeto, String cnn)
        {
            clsUsuarioFormsObjetoInfo result = null;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("select id from USUARIOFORMSOBJETO where idformsobjeto=@idformsobjeto and idusuario=@idusuario", scn);
            scd.Parameters.Add("@idformsobjeto", SqlDbType.Int).Value = idformsobjeto;
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

        public List<clsUsuarioFormsObjetoInfo> Lista(Int32 idusuario, Int32 idforms, String cnn)
        {
            clsUsuarioFormsObjetoInfo infoAtual = null;
            List<clsUsuarioFormsObjetoInfo> result = new List<clsUsuarioFormsObjetoInfo>();

            String gruposystem = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select gruposystem from usuario where id=" + idusuario);
            if (gruposystem != null && gruposystem == "S")
            {
                return result;
            }

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("select " +
                                        "USUARIOFORMSOBJETO.id " +
                                 "from " +
                                        "USUARIOFORMSOBJETO " +
                                            "inner join formsobjeto on formsobjeto.id = USUARIOFORMSOBJETO.idformsobjeto " +
                                "where " +
                                    "formsobjeto.idforms=@idforms and " +
                                    "USUARIOFORMSOBJETO.idusuario=@idusuario and " +
                                    "(USUARIOFORMSOBJETO.ativo='N' or USUARIOFORMSOBJETO.visivel='N')", scn);
            scd.Parameters.Add("@idforms", SqlDbType.Int).Value = idforms;
            scd.Parameters.Add("@idusuario", SqlDbType.Int).Value = idusuario;

            scn.Open();

            sdr = scd.ExecuteReader();
            while (sdr.Read() == true)
            {
                infoAtual = Carregar(clsParser.Int32Parse(sdr["id"].ToString()), cnn);
                result.Add(infoAtual);
            }

            scn.Close();

            return result;
        }

        public Boolean JaExiste(Int32 idusuario, Int32 idformsobjeto, String cnn)
        {
            Boolean result = false;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("select id from USUARIOFORMSOBJETO where idformsobjeto=@idformsobjeto and idusuario=@idusuario", scn);
            scd.Parameters.Add("@idformsobjeto", SqlDbType.Int).Value = idformsobjeto;
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

        public static DataTable GridDados(Int32 idusuario, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "USUARIOFORMSOBJETO.*, " +
                        "FORMSOBJETO.IDFORMS, " +
                        "FORMSOBJETO.NOME, " +
                        "FORMSOBJETO.NOMEOBJETO, " +
                        "CASE REVERSE(SUBSTRING(REVERSE(FORMSOBJETO.TIPO), 0, CHARINDEX('.', REVERSE(FORMSOBJETO.TIPO)))) " +
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
                            "else REVERSE(SUBSTRING(REVERSE(FORMSOBJETO.TIPO), 0, CHARINDEX('.', REVERSE(FORMSOBJETO.TIPO)))) " +
                        "END AS TIPO " +
                    "FROM " +
                        "USUARIOFORMSOBJETO " +
                        "INNER JOIN FORMSOBJETO ON FORMSOBJETO.ID = USUARIOFORMSOBJETO.IDFORMSOBJETO " +
                    "WHERE " +
                        "USUARIOFORMSOBJETO.IDUSUARIO = @IDUSUARIO AND " +
                        "FORMSOBJETO.LISTA = 'S' " +
                    "ORDER BY " +
                        "FORMSOBJETO.IDFORMS, FORMSOBJETO.IDFORMSOBJETO, FORMSOBJETO.POSDIF ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);

            sda.SelectCommand.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = idusuario;

            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtContratoColunas = new GridColuna[]
        {
            new GridColuna("Nome", "NOME", 139, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome Objeto", "NOMEOBJETO", 139, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tipo", "TIPO", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("A.", "ATIVO", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("V.", "VISIVEL", 30, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt)
        {
            dgv.DataSource = dt;


            clsGridHelper.MontaGrid2(dgv, dtContratoColunas, true);
        }

        public static DataTable GridDados2(int idusuario, string cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "(SELECT TOP 1 ID FROM USUARIOFORMSOBJETO WHERE IDFORMSOBJETO = FORMSOBJETO.ID AND IDUSUARIO = @IDUSUARIO) AS ID, " +
                        "ID AS IDFORMSOBJETO, " +
                        "IDFORMS, " +
                        "NOME, " +
                        "NOMEOBJETO, " +
                        "TIPO, " +
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
                        "(SELECT TOP 1 ATIVO FROM USUARIOFORMSOBJETO WHERE IDFORMSOBJETO = FORMSOBJETO.ID AND IDUSUARIO = @IDUSUARIO) AS ATIVO, " +
                        "(SELECT TOP 1 VISIVEL FROM USUARIOFORMSOBJETO WHERE IDFORMSOBJETO = FORMSOBJETO.ID AND IDUSUARIO = @IDUSUARIO) AS VISIVEL " +
                    "FROM " +
                        "FORMSOBJETO ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);

            sda.SelectCommand.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = idusuario;

            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (row["ATIVO"] == null ||
                    row["ATIVO"].ToString() == string.Empty)
                {
                    row["ATIVO"] = "S";
                }

                if (row["VISIVEL"] == null ||
                    row["VISIVEL"].ToString() == string.Empty)
                {
                    row["VISIVEL"] = "S";
                }
            }

            dt.AcceptChanges();

            return dt;
        }

        private static GridColuna[] dtUsuariosFormsObjetoColunas = new GridColuna[]
        {
            new GridColuna("Nome", "NOME", 139, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome Objeto", "NOMEOBJETO", 139, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tipo", "TIPO", 100, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("A.", "ATIVO", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("V.", "VISIVEL", 30, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMonta2(DataGridView dgv, DataTable dt)
        {
            dgv.DataSource = dt;


            clsGridHelper.MontaGrid2(dgv, dtUsuariosFormsObjetoColunas, true);
        }
    }
}
