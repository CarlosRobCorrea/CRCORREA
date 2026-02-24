using System;
using System.Data;
using System.Data.SqlClient;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsCentrocustosBLL : SQLFactory<clsCentrocustosInfo>
    {
        public DataTable GridDados(String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "select " +
                        "ID, " +
                        "CODIGO, " +
		                "NOME, " +
		                "case ATIVO " +
			                "when 'S' then 'Ativa' " +
			                "when 'N' then 'Inativa' " +
		                "end	as ATIVO " +
                    "from " +
                        "centrocustos";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDados(String ativo, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "select " +
                        "ID, " +
                        "CODIGO, " +
                        "NOME, " +
                        "case ATIVO " +
                            "when 'S' then 'Ativa' " +
                            "when 'N' then 'Inativa' " +
                        "end	as ATIVO " +
                    "from " +
                        "centrocustos";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@ativo", SqlDbType.NVarChar).Value = ativo;
            sda.Fill(dt);

            return dt;
        }
    }
}
