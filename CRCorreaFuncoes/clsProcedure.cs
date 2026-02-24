using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaFuncoes
{
    public static class Procedure
    {
        private const string _Criar_Zero = "_Criar_Zero";
        private const string _pesquisa_ultimo = "_pesquisa_ultimo";
        private const string _pesquisa_ultimo2 = "_pesquisa_ultimo2";
        private const string _pesquisa_valor = "_pesquisa_valor";
        private const string _pesquisa_valor2 = "_pesquisa_valor2";
        private const string _pesquisa_valor3 = "_pesquisa_valor3";

        private const string _atualizasalariofamilia_id = "_atualizasalariofamilia_id";

        public static String PesquisaValor(String _conexao,
                                    String _tabela,
                                    String _campo,
                                    String _campowhere,
                                    String _campowherevalor)
        {
            SqlCommand scd;
            clsSql clsSql = new clsSql();
            scd = clsSql.CriarCommand(_conexao, _pesquisa_valor, CommandType.StoredProcedure);
            scd.Parameters.AddWithValue("@TABELA", _tabela);
            scd.Parameters.AddWithValue("@CAMPO", _campo);
            scd.Parameters.AddWithValue("@CAMPO2", _campowhere);
            scd.Parameters.AddWithValue("@VALORCAMPO2", _campowherevalor);
            return clsSql.ExecuteScalar(scd).ToString();
        }

        public static String PesquisaoPrimeiro(String _conexao,
                                        String _query)
        {
            String result = null;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(_conexao);
            scd = new SqlCommand(_query, scn);
            scd.CommandTimeout = 100;
            scn.Open();
            sdr = scd.ExecuteReader();
            if (sdr.Read() == true)
            {
                if (sdr[0] != null)
                {
                    result = sdr[0].ToString();
                }
            }
            scn.Close();

            return result;
        }

        public static String PesquisaoPrimeiro(String _conexao,
                                        String _query, String _retorno)
        {
            String result = _retorno;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(_conexao);
            scd = new SqlCommand(_query, scn);
            scd.CommandTimeout = 100;
            scn.Open();
            sdr = scd.ExecuteReader();
            if (sdr.Read() == true)
            {
                if (sdr[0] != null)
                {
                    result = sdr[0].ToString();
                }
            }
            scn.Close();

            return result;
        }

        public static DataTable RetornaDataTable(String _conexao, String _query)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;

            query = _query;


            sda = new SqlDataAdapter(_query, _conexao);

            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static String PesquisaValor(String _conexao, String _tabela, String _campo, String _campowhere, Int32 _campowherevalor)
        {
            SqlCommand scd;
            clsSql clsSql = new clsSql();
            scd = clsSql.CriarCommand(_conexao, _pesquisa_valor2, CommandType.StoredProcedure);
            scd.Parameters.AddWithValue("@TABELA", _tabela);
            scd.Parameters.AddWithValue("@CAMPO", _campo);
            scd.Parameters.AddWithValue("@CAMPO2", _campowhere);
            scd.Parameters.AddWithValue("@VALORCAMPO2", _campowherevalor);
            return clsSql.ExecuteScalar(scd).ToString();
        }

        public static String PesquisaValor3(String _conexao, String _tabela, String _campo, String _campowhere, String _campowherevalor,
                                     String _campowhere2, String _campowherevalor2)
        {
            SqlCommand scd;
            clsSql clsSql = new clsSql();
            scd = clsSql.CriarCommand(_conexao, _pesquisa_valor3, CommandType.StoredProcedure);
            scd.Parameters.AddWithValue("@TABELA", _tabela);
            scd.Parameters.AddWithValue("@CAMPO", _campo);
            scd.Parameters.AddWithValue("@CAMPO2", _campowhere);
            scd.Parameters.AddWithValue("@VALORCAMPO2", _campowherevalor);
            scd.Parameters.AddWithValue("@CAMPO3", _campowhere2);
            scd.Parameters.AddWithValue("@VALORCAMPO3", _campowherevalor2);
            return clsSql.ExecuteScalar(scd).ToString();
        }

        public static void ExecutarQuery(String _conexao, String _query)
        {
            using (SqlConnection connection = new SqlConnection(_conexao))
            {
                SqlTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;

                    if (_conexao.Contains("logix")) //Se for logix executar dirty read antes
                    {
                        command = new SqlCommand("SET ISOLATION TO DIRTY READ", connection, transaction);
                        command.ExecuteNonQuery();
                    }
                    command.CommandText = _query;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);

                }
            }
        }

    }
}
