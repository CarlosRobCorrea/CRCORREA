using System;
using System.Data;
using System.Data.SqlClient;

namespace CRCorreaFuncoes
{
    public class clsFormulariosObjetoDAL
    {
        //Stored Procedures
        // - Select
        private const String formulariosobjeto_select_where_id = "formulariosobjeto_select_where_id";
        private const String formulariosobjeto_select_where_name = "formulariosobjeto_select_where_name";

        // - Insert/Update
        private const String formulariosobjeto_insert_update_where_id = "formulariosobjeto_insert_update_where_id";

        // - Delete
        private const String formulariosobjeto_delete_where_id = "formulariosobjeto_delete_where_id";

        // - Grid
        private const String formulariosobjeto_grid = "formulariosobjeto_grid";

        
        // - Functions
        public clsFormulariosObjetoInfo Carregar(String _conexao, Int32 _id)
        {
            SqlDataReader sdr;
            clsSql clsSql = new clsSql();

            sdr = clsSql.CriarDataReader(_conexao, formulariosobjeto_select_where_id, new SqlParameter("@ID", _id));
            if (sdr.Read())
            {
                clsFormulariosObjetoInfo clsFormulariosobjetoInfo;
                clsFormulariosobjetoInfo = CarregaInfo(sdr);

                clsSql.cn.Close();
                clsSql.cn.Dispose();

                return clsFormulariosobjetoInfo;
            }
            else
            {
                clsSql.cn.Close();
                clsSql.cn.Dispose();
                return null;
            }
        }
        public clsFormulariosObjetoInfo CarregarName(String _conexao, String _name, Int32 _idFormulario)
        {
            SqlDataReader sdr;
            clsSql clsSql = new clsSql();

            sdr = clsSql.CriarDataReader(_conexao, formulariosobjeto_select_where_name,
                                                new SqlParameter[] 
                                                {
                                                new SqlParameter("@IDFORMULARIO", _idFormulario), 
                                                new SqlParameter("@NAME", _name)
                                                });
            if (sdr.Read())
            {
                clsFormulariosObjetoInfo clsFormulariosobjetoInfo;
                clsFormulariosobjetoInfo = CarregaInfo(sdr);

                clsSql.cn.Close();
                clsSql.cn.Dispose();
                return clsFormulariosobjetoInfo;
            }
            else
            {
                clsSql.cn.Close();
                clsSql.cn.Dispose();
                return null;
            }
        }

        public DataTable CarregaGrid(String _conexao, Int32 _idFormulario)
        {
            clsSql clsSql = new clsSql();
            return clsSql.CriarDataTable(_conexao, formulariosobjeto_grid, 
                    new SqlParameter("@IDFORMULARIO", _idFormulario));

        }

        public Int32 Incluir(String _conexao, clsFormulariosObjetoInfo _info)
        {
            SqlCommand scd;
            clsSql clsSql = new clsSql();

            scd = clsSql.CriarCommand(_conexao, formulariosobjeto_insert_update_where_id, CommandType.StoredProcedure);
            scd.Parameters.AddWithValue("@ID", 0);
            CarregaParameter(scd, _info);
            return clsSql.ExecuteQuery(scd);
        }

        public Int32 Alterar(String _conexao, clsFormulariosObjetoInfo _info)
        {
            SqlCommand scd;
            clsSql clsSql = new clsSql();

            scd = clsSql.CriarCommand(_conexao, formulariosobjeto_insert_update_where_id, CommandType.StoredProcedure);
            scd.Parameters.AddWithValue("@ID", _info.id);
            CarregaParameter(scd, _info);
            return clsSql.ExecuteQuery(scd);
        }

        public void Excluir(String _conexao, Int32 _id)
        {
            SqlCommand scd;
            clsSql clsSql = new clsSql();
            clsFormulariosObjetoInfo clsFormulariosobjetoInfo = new clsFormulariosObjetoInfo();

            scd = clsSql.CriarCommand(_conexao, formulariosobjeto_delete_where_id, CommandType.StoredProcedure);
            scd.Parameters.AddWithValue("@ID", _id);
            clsSql.ExecuteNonQuery(scd);
        }

        public clsFormulariosObjetoInfo CarregaInfo(SqlDataReader _sdr)
        {
            
            clsFormulariosObjetoInfo clsFormulariosobjetoInfo = new clsFormulariosObjetoInfo();
            clsFormulariosobjetoInfo.id = clsParser.Int32Parse(_sdr["ID"].ToString());
            clsFormulariosobjetoInfo.idformulario = clsParser.Int32Parse(_sdr["IDFORMULARIO"].ToString());
            clsFormulariosobjetoInfo.nomeobjeto = _sdr["NOMEOBJETO"].ToString();
            clsFormulariosobjetoInfo.nome = _sdr["NOME"].ToString();
            clsFormulariosobjetoInfo.ativo = _sdr["ATIVO"].ToString();
            clsFormulariosobjetoInfo.liberado = _sdr["LIBERADO"].ToString();
            clsFormulariosobjetoInfo.aparecelista = _sdr["APARECELISTA"].ToString();
            clsFormulariosobjetoInfo.verificarform = _sdr["VERIFICARFORM"].ToString();

            return clsFormulariosobjetoInfo;
        }

        public void CarregaParameter(SqlCommand _scd, clsFormulariosObjetoInfo _info)
        {
            
            _scd.Parameters.Add("@IDFORMULARIO", SqlDbType.Int).Value = _info.idformulario;
            _scd.Parameters.Add("@NOMEOBJETO", SqlDbType.NVarChar).Value = _info.nomeobjeto;
            _scd.Parameters.Add("@NOME", SqlDbType.NVarChar).Value = _info.nome;
            _scd.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = _info.ativo;
            _scd.Parameters.Add("@LIBERADO", SqlDbType.NVarChar).Value = _info.liberado;
            _scd.Parameters.Add("@APARECELISTA", SqlDbType.NVarChar).Value = _info.aparecelista;
            _scd.Parameters.Add("@VERIFICARFORM", SqlDbType.NVarChar).Value = _info.verificarform;
        }

    }
}
