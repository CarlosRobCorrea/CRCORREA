using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaFuncoes
{
    public class clsSql
    {
        private static SqlConnection _cn;

        public SqlCommand CriarCommand(string conexao, string query, CommandType cte)
        {
            SqlConnection cn = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.CommandType = cte;
            return cmd;
        }

        // modficado por Williams 08/05/2009 - não estava fechando a conexao do banco de dados
        // add a varivel (cn) com public para acessar o dados dela
        // Ultima Revisao - 
        public SqlDataReader CriarDataReader(string conexao, string query, CommandType cte)
        {
            cn = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader sdr;

            cmd.CommandType = cte;
            cmd.Connection.Open();
            cmd.CommandTimeout = 2000; // Williams temporario
            sdr = cmd.ExecuteReader();
            return sdr;
        }

        public SqlDataReader CriarDataReader(string _conexao, string _query, SqlParameter _spr)
        {
            cn = new SqlConnection(_conexao);
            SqlCommand cmd = new SqlCommand(_query, cn);
            SqlDataReader sdr;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(_spr);
            cmd.Connection.Open();
            cmd.CommandTimeout = 2000; // Williams temporario
            sdr = cmd.ExecuteReader();
            return sdr;
        }

        public SqlDataReader CriarDataReader(string _conexao, string _query, SqlParameter[] _spr)
        {
            cn = new SqlConnection(_conexao);
            SqlCommand cmd = new SqlCommand(_query, cn);
            SqlDataReader sdr;

            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter spr in _spr)
            {
                cmd.Parameters.Add(spr);
            }
            cmd.Connection.Open();
            cmd.CommandTimeout = 2000; // Williams temporario
            sdr = cmd.ExecuteReader();
            return sdr;
        }
        //
        // fim        

        public DataTable CriarDataTable(string _conexao, string _query, CommandType _cte)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(_query, _conexao))
            {
                DataTable tabela = new DataTable();
                da.SelectCommand.CommandTimeout = 2000;
                da.SelectCommand.CommandType = _cte;
                da.Fill(tabela);
                return tabela;
            }
        }

        public DataTable CriarDataTable(string _conexao, string _query, SqlParameter _spr)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(_query, _conexao))
            {
                DataTable tabela = new DataTable();
                da.SelectCommand.CommandTimeout = 2000;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add(_spr);
                da.Fill(tabela);
                return tabela;
            }
        }

        public DataTable CriarDataTable(string _conexao, string _query, SqlParameter[] _spr, CommandType _cte)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(_query, _conexao))
            {
                DataTable tabela = new DataTable();
                da.SelectCommand.CommandTimeout = 2000; // Williams temporario
                da.SelectCommand.CommandType = _cte;
                foreach (SqlParameter spr in _spr)
                {
                    da.SelectCommand.Parameters.Add(spr);
                }
                da.Fill(tabela);
                return tabela;
            }
        }

        // **************************************************************
        // Novas versões do DataTable (criam ou alteram a Stored Procedure)
        public DataTable CriarDataTable(String _conexao,
                                        CommandType _cte,
                                        String _query,
                                        String _query_procedure)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(_query, _conexao))
            {
                DataTable tabela = new DataTable();
                da.SelectCommand.CommandTimeout = 2000; // Williams temporario
                da.SelectCommand.CommandType = _cte;
                try
                {
                    da.Fill(tabela);
                }
                catch (Exception ex)
                {
                    if (_cte == CommandType.StoredProcedure)
                    {
                        VerificaProcedure(_conexao, ex, _query_procedure);
                        da.Fill(tabela);
                    }
                    else
                        throw new Exception(ex.Message, ex);
                }
                return tabela;
            }
        }

        public DataTable CriarDataTable(String _conexao,
                                        CommandType _cte,
                                        String _query,
                                        String _query_procedure,
                                        SqlParameter _spr)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(_query, _conexao))
            {
                DataTable tabela = new DataTable();
                da.SelectCommand.CommandTimeout = 2000; // Williams temporario
                da.SelectCommand.CommandType = _cte;
                da.SelectCommand.Parameters.Add(_spr);
                try
                {
                    da.Fill(tabela);
                }
                catch (Exception ex)
                {
                    if (_cte == CommandType.StoredProcedure)
                    {
                        VerificaProcedure(_conexao, ex, _query_procedure);
                        da.Fill(tabela);
                    }
                    else
                        throw new Exception(ex.Message, ex);
                }
                return tabela;
            }
        }

        public DataTable CriarDataTable(String _conexao,
                                        CommandType _cte,
                                        String _query,
                                        String _query_procedure,
                                        SqlParameter[] _spr)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(_query, _conexao))
            {
                DataTable tabela = new DataTable();
                da.SelectCommand.CommandTimeout = 2000; // Williams temporario
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter spr in _spr)
                {
                    da.SelectCommand.Parameters.Add(spr);
                }

                try
                {
                    da.Fill(tabela);
                }
                catch (Exception ex)
                {
                    if (_cte == CommandType.StoredProcedure)
                    {
                        VerificaProcedure(_conexao, ex, _query_procedure);
                        da.Fill(tabela);
                    }
                    else
                        throw new Exception(ex.Message, ex);
                }
                return tabela;
            }
        }
        // **************************************************************

        public Int32 ExecuteQuery(SqlCommand _scd)
        {
            SqlDataReader dr;
            Int32 registroId;
            try
            {
                _scd.Connection.Open();
                _scd.CommandTimeout = 2000; // Williams temporario
                dr = _scd.ExecuteReader();
                if (dr.Read())
                {
                    registroId = Int32.Parse(dr[0].ToString());
                }
                else
                {
                    registroId = 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro " + ex.Message);
            }
            finally
            {
                _scd.Connection.Close();
            }
            return registroId;
        }

        public Int32 ExecuteNonQuery(SqlCommand _scd)
        {
            Int32 resultado;
            try
            {
                _scd.Connection.Open();
                _scd.CommandTimeout = 2000; // Williams temporario
                resultado = Int32.Parse(_scd.ExecuteNonQuery().ToString());
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _scd.Connection.Close();
            }
            return resultado;
        }

        public string ExecuteScalar(SqlCommand _scd)
        {
            SqlDataReader sdr;
            string resultado = "";
            try
            {
                _scd.Connection.Open();
                _scd.CommandTimeout = 2000; // Williams temporario
                sdr = _scd.ExecuteReader();
                if (sdr.Read())
                {
                    resultado = sdr[0].ToString();
                }
                else
                {
                    resultado = "";
                }
            }
            catch (SqlException ex)
            {
                resultado = "";
                throw new Exception("Erro no servidor: " + ex.Message);
            }
            finally
            {
                _scd.Connection.Close();
            }
            return resultado;
        }

        public void VerificaProcedure(String _conexao, Exception ex, String _query_procedure)
        {
            SqlConnection scn = new SqlConnection(_conexao);
            SqlCommand scd;
            // Stored Procedure não existe - cria
            if (ex.Message.IndexOf("Could not find stored procedure") != -1)
            {
                scd = new SqlCommand("CREATE " + _query_procedure, scn);
                scn.Open();
                scd.ExecuteNonQuery();
                scn.Close();
            }
            else if (ex.Message == "")
            {
                scd = new SqlCommand("CREATE " + _query_procedure, scn);
                scn.Open();
                try
                {
                    scd.ExecuteNonQuery();
                }
                catch
                {
                    scn.Close();

                    scd = new SqlCommand("ALTER " + _query_procedure, scn);
                    scn.Open();
                    scd.ExecuteNonQuery();
                }
                scn.Close();
            }
            else
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static SqlConnection cn
        {
            get { return _cn; }
            set { _cn = value; }
        }
    }
}
