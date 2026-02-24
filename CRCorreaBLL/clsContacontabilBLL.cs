using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CRCorreaInfo;
using CRCorreaFuncoes;

namespace CRCorreaBLL
{
    public class clsContacontabilBLL : SQLFactory<clsContacontabilInfo>
    {
        public DataTable GridDados(String ativo, String cnn)
        {
            DataTable dt;
            String query;
            SqlDataAdapter sda;

            query = "select " +
                        "ID, " +
                        "ATIVO, " +
                        "REDUZIDO, " +
                        "TIPO, " +
                        "CODIGO, " +
                        "NOME " +
		            "FROM " +
                        "CONTACONTABIL ";

            if (ativo.ToUpper() == "S" ||
                ativo.ToUpper() == "N")
            {
                query += " WHERE ATIVO = @ATIVO ";
            }

            query += " ORDER BY CODIGO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);

            if (ativo.ToUpper() == "S" ||
                ativo.ToUpper() == "N")
            {
                sda.SelectCommand.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = ativo;
            }

            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDados(String cnn)
        {
            DataTable dt;
            String query;
            SqlDataAdapter sda;

            query = "select " +
                        "ID, " +
                        "ATIVO, " +
                        "REDUZIDO, " +
                        "TIPO, " +
                        "CODIGO, " +
                        "NOME " +
                    "FROM " +
                        "CONTACONTABIL ";

            query += " ORDER BY CODIGO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);

            sda.Fill(dt);

            return dt;
        }
    }
}
