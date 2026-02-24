using System;
using System.Data;
using System.Data.SqlClient;

namespace CRCorreaFuncoes
{
    public class clsSelectInsertUpdateDAL
    {       
        //Stored Procedures        
        private const String _select = "_select";
        private const String _insert = "_insert";
        private const String _update = "_update";
        private const String _delete = "_delete";

        public DataTable Select(String _conexao, String _tabela, String _campos_tabela, String _where, String _ordem)
        {   
            SqlConnection select_conn;
            DataTable tabela = new DataTable();
            SqlCommand select_command = new SqlCommand(_select, select_conn = new SqlConnection(_conexao));
            select_command.Parameters.Add("@TABELA", SqlDbType.NVarChar).Value = _tabela.Trim();
            select_command.Parameters.Add("@CAMPOS_TABELA", SqlDbType.NVarChar).Value = _campos_tabela.Trim();
            select_command.Parameters.Add("@WHERE", SqlDbType.NVarChar).Value = _where.Trim();
            select_command.Parameters.Add("@ORDEM", SqlDbType.NVarChar).Value = _ordem.Trim();
            select_command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(select_command);            
            adapter.SelectCommand.CommandTimeout = 2000;
            adapter.Fill(tabela);
            return tabela;
        }

        public DataRow SelectCollectionFields(String _conexao, String _campos_tabela, String _tabela, String _where, String _ordem)
        {
            DataRow resultado;
            try
            {
                resultado = Select(_conexao, _tabela, _campos_tabela, _where, _ordem).Rows[0];
            }
            catch
            {
                resultado = null;
            }
            return resultado;
        }

        public String SelectOnlyField(String _conexao, String _campo_tabela, String _tabela, String _where, String _ordem)
        {
            String resultado;
            try
            {
                resultado = Select(_conexao, _tabela, _campo_tabela, _where, _ordem).Rows[0][0].ToString();                
            }
            catch
            {
                resultado = "";
            }
            return resultado;
        }

        public Int32 Insert(String _conexao, String _tabela, String _campos_tabela, String _campos_programa)
        {
            Int32 _id;
            SqlDataReader dr;
            SqlConnection insert_conn;
            SqlCommand insert_command = new SqlCommand(_insert, insert_conn = new SqlConnection(_conexao));
            insert_command.Parameters.Add("@TABELA", SqlDbType.NVarChar).Value = _tabela.Trim();
            insert_command.Parameters.Add("@CAMPOS_TABELA", SqlDbType.NVarChar).Value = _campos_tabela.Trim();
            insert_command.Parameters.Add("@CAMPOS_PROGRAMA", SqlDbType.NVarChar).Value = _campos_programa.Trim();
            insert_command.CommandType = CommandType.StoredProcedure;
            insert_command.Connection.Open();
            insert_command.CommandTimeout = 2000;
            dr = insert_command.ExecuteReader();
            if (dr.Read())
            {
                _id = Int32.Parse(dr[0].ToString());
            }
            else
            {
                _id = 0;
            }
            insert_command.Connection.Close();
            return _id;
        }

        public Int32 Update(String _conexao, String _tabela, String _campos_tabela_programa, String _where)
        {
            Int32 _id;
            SqlDataReader dr;
            SqlConnection update_conn;
            SqlCommand update_command = new SqlCommand(_update, update_conn = new SqlConnection(_conexao));
            update_command.Parameters.Add("@TABELA", SqlDbType.NVarChar).Value = _tabela.Trim();
            update_command.Parameters.Add("@CAMPOS_TABELA_PROGRAMA", SqlDbType.NVarChar).Value = _campos_tabela_programa.Trim();
            update_command.Parameters.Add("@WHERE", SqlDbType.NVarChar).Value = _where.Trim();
            update_command.CommandType = CommandType.StoredProcedure;
            update_command.Connection.Open();
            update_command.CommandTimeout = 2000;
            //update_command.ExecuteNonQuery();
            dr = update_command.ExecuteReader();
            if (dr.Read())
            {
                _id = Int32.Parse(dr[0].ToString());
            }
            else
            {
                _id = 0;
            }
            update_command.Connection.Close();
            return _id;
        }

        public void Delete(String _conexao, String _tabela, String _where)
        {
            SqlConnection delete_conn;
            SqlCommand delete_command = new SqlCommand(_delete, delete_conn = new SqlConnection(_conexao));
            delete_command.Parameters.Add("@TABELA", SqlDbType.NVarChar).Value = _tabela.Trim();
            delete_command.Parameters.Add("@WHERE", SqlDbType.NVarChar).Value = _where.Trim();
            delete_command.CommandType = CommandType.StoredProcedure;
            delete_command.Connection.Open();
            delete_command.CommandTimeout = 2000;
            delete_command.ExecuteNonQuery();
            delete_command.Connection.Close();
        }
    }
}
