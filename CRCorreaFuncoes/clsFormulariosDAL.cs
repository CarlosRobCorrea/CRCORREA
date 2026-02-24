using System;
using System.Data;
using System.Data.SqlClient;

namespace CRCorreaFuncoes
{
    public class clsFormulariosDAL
    {
        //Stored Procedures
        // - Select
        private const String formularios_select_where_id = "formularios_select_where_id";
        private const String formularios_select_where_nroform = "formularios_select_where_nroform";

        // - Insert/Update
        private const String formularios_insert_update_where_id = "formularios_insert_update_where_id";

        // - Delete
        private const String formularios_delete_where_id = "formularios_delete_where_id";

        // - Grid
        private const String formularios_select_grid = "formularios_select_grid";
        private const String formularios_select_gridmodulos = "formularios_select_gridmodulos";

        // - Functions
        public clsFormulariosInfo Carregar(String _conexao, Int32 _id)
        {
            SqlDataReader sdr;
            clsSql clsSql = new clsSql();

            sdr = clsSql.CriarDataReader(_conexao, formularios_select_where_id, new SqlParameter("@ID", _id));
            if (sdr.Read())
            {
                clsFormulariosInfo clsFormulariosInfo;
                clsFormulariosInfo = CarregaInfo(sdr);
                clsSql.cn.Close();
                clsSql.cn.Dispose();

                return clsFormulariosInfo;
            }
            else
            {
                clsSql.cn.Close();
                clsSql.cn.Dispose();
                return null;
            }
        }

        public clsFormulariosInfo CarregarForm(String _conexao, Int32 _nroform,  Int32 _modulo )
        {
            SqlDataReader sdr;
            clsSql clsSql = new clsSql();

            sdr = clsSql.CriarDataReader(_conexao, formularios_select_where_nroform, 
                                                                new SqlParameter[] 
                                                {
                                                new SqlParameter("@NROFORM", _nroform), 
                                                new SqlParameter("@MODULO", _modulo)
                                                });
            if (sdr.Read())
            {
                clsFormulariosInfo clsFormulariosInfo;
                clsFormulariosInfo = CarregaInfo(sdr);

                clsSql.cn.Close();
                clsSql.cn.Dispose();
                return clsFormulariosInfo;
            }
            else
            {
                clsSql.cn.Close();
                clsSql.cn.Dispose();
                return null;
            }
        }


        public DataTable CarregaGrid(String _conexao)
        {
            clsSql clsSql = new clsSql();
            return clsSql.CriarDataTable(_conexao, formularios_select_grid, CommandType.StoredProcedure);
        }

        public DataTable CarregaGridMod(String _conexao)
        {
            clsSql clsSql = new clsSql();
            return clsSql.CriarDataTable(_conexao, formularios_select_gridmodulos, CommandType.StoredProcedure);
        }
        
        public Int32 Incluir(String _conexao, clsFormulariosInfo _info)
        {
            SqlCommand scd;
            clsSql clsSql = new clsSql();

            scd = clsSql.CriarCommand(_conexao, formularios_insert_update_where_id, CommandType.StoredProcedure);
            scd.Parameters.AddWithValue("@ID", 0);
            CarregaParameter(scd, _info);
            return clsSql.ExecuteQuery(scd);
        }

        public Int32 Alterar(String _conexao, clsFormulariosInfo _info)
        {
            SqlCommand scd;
            clsSql clsSql = new clsSql();

            scd = clsSql.CriarCommand(_conexao, formularios_insert_update_where_id, CommandType.StoredProcedure);
            scd.Parameters.AddWithValue("@ID", _info.id);
            CarregaParameter(scd, _info);
            return clsSql.ExecuteQuery(scd);
        }

        public void Excluir(String _conexao, Int32 _id)
        {
            SqlCommand scd;
            clsSql clsSql = new clsSql();
            clsFormulariosInfo clsFormulariosInfo = new clsFormulariosInfo();

            scd = clsSql.CriarCommand(_conexao, formularios_delete_where_id, CommandType.StoredProcedure);
            scd.Parameters.AddWithValue("@ID", _id);
            clsSql.ExecuteNonQuery(scd);
        }

        public clsFormulariosInfo CarregaInfo(SqlDataReader _sdr)
        {
            
            clsFormulariosInfo clsFormulariosInfo = new clsFormulariosInfo();
            clsFormulariosInfo.id = clsParser.Int32Parse(_sdr["ID"].ToString());
            clsFormulariosInfo.numero = clsParser.Int32Parse(_sdr["NUMERO"].ToString());
            clsFormulariosInfo.modulo = clsParser.Int32Parse(_sdr["MODULO"].ToString());
            clsFormulariosInfo.nome = _sdr["NOME"].ToString();
            clsFormulariosInfo.nomeform = _sdr["NOMEFORM"].ToString();
            clsFormulariosInfo.ativo = _sdr["ATIVO"].ToString();
            clsFormulariosInfo.liberado = _sdr["LIBERADO"].ToString();
            clsFormulariosInfo.aparecelista = _sdr["APARECELISTA"].ToString();
            clsFormulariosInfo.verificarform = _sdr["VERIFICARFORM"].ToString();

            return clsFormulariosInfo;
        }

        public void CarregaParameter(SqlCommand _scd, clsFormulariosInfo _info)
        {
            _scd.Parameters.Add("@NUMERO", SqlDbType.Int).Value = _info.numero;
            _scd.Parameters.Add("@MODULO", SqlDbType.Int).Value = _info.modulo;
            _scd.Parameters.Add("@NOMEFORM", SqlDbType.NVarChar).Value = _info.nomeform;
            _scd.Parameters.Add("@NOME", SqlDbType.NVarChar).Value = _info.nome;
            _scd.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = _info.ativo;
            _scd.Parameters.Add("@LIBERADO", SqlDbType.NVarChar).Value = _info.liberado;
            _scd.Parameters.Add("@APARECELISTA", SqlDbType.NVarChar).Value = _info.aparecelista;
            _scd.Parameters.Add("@VERIFICARFORM", SqlDbType.NVarChar).Value = _info.verificarform;
        }
    }
}
