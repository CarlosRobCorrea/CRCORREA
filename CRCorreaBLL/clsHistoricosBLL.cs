using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsHistoricosBLL : SQLFactory<clsHistoricosInfo>
    {
        public DataTable GridDados(String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
			            "CODIGO, " +
			            "NOME, " +
			            "case ATIVO " +
				        "    when 'S' then 'Ativa' " +
				        "    when 'N' then 'Inativa' " +
			            "end	as ATIVO, " +
			            "case TIPO " +
				        "    when 'D' then 'Debito' " +
				        "    when 'C' then 'Credito' " +
			            "end	as TIPO " +
	                "FROM " +
                        "HISTORICOS " +
	                "ORDER BY " +
                        "CODIGO ";

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

            query = "SELECT " +
                        "ID, " +
                        "CODIGO, " +
                        "NOME, " +
                        "case ATIVO " +
                        "    when 'S' then 'Ativa' " +
                        "    when 'N' then 'Inativa' " +
                        "end	as ATIVO, " +
                        "case TIPO " +
                        "    when 'D' then 'Debito' " +
                        "    when 'C' then 'Credito' " +
                        "end	as TIPO " +
                    "FROM " +
                        "HISTORICOS ";

            if (ativo == "S" || ativo == "N")
            {
                query += "WHERE " +
                            "ATIVO = @ATIVO ";
            }

            query += "ORDER BY " +
                        "CODIGO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);

            if (ativo == "S" || ativo == "N")
            {
                sda.SelectCommand.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = ativo;
            }

            sda.Fill(dt);

            return dt;
        }
    }
}
