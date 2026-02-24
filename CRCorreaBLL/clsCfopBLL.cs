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
    public class clsCfopBLL : SQLFactory<clsCfopInfo>
    {
        public DataTable GridDados(String ativo, String cnn)
        {
            DataTable dt;
            String query;
            SqlDataAdapter sda;

            query = "select " +
                        "CFOP.ID, " +
                        "CFOP.CFOP, " +
                        "CFOP.NOMENOTA, " +
                        "CFOP.DIZER, " +
                        "CONTACONTABIL.CODIGO AS [DEBITO], " +
                        "CONTABIL1.CODIGO AS [CREDITO], " +
                        "CFOP.IDCONTADEB, " +
                        "CFOP.IDCONTACRE, " +
                        "CFOP.COMBUSTIVEL " +
                    "FROM CFOP " +
                        "inner JOIN CONTACONTABIL ON CONTACONTABIL.ID=IDCONTADEB " +
                        "inner JOIN CONTACONTABIL AS [CONTABIL1] ON CONTABIL1.ID=CFOP.IDCONTACRE ";

            if (ativo.ToUpper() != "T")
            {
                query += " WHERE CFOP.ATIVO = @ATIVO ";
            }

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);

            if (ativo.ToUpper() != "T")
            {
                sda.SelectCommand.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = ativo;
            }

            sda.Fill(dt);

            return dt;
        }
    }
}
