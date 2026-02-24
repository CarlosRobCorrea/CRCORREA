using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System.Data;
using System.Data.SqlClient;

namespace CRCorreaBLL
{
    public class clsFeriadosBLL : SQLFactory<clsFeriadosInfo>
    {

        public DataTable GridCarrega(String cnn)
        {
            String query;
            SqlDataAdapter sda;
            DataTable result;

            query = "select ID, DATA ,DESCRICAO from feriados order by DATA DESC ";

            sda = new SqlDataAdapter(query, cnn);

            result = new DataTable();
            sda.Fill(result);

            return result;
        }

        public DataTable GridCarrega1(String cnn, DateTime dataini, DateTime datafim)
        {
            String query;
            SqlDataAdapter sda;
            DataTable result;
            
            query = "select * from feriados  where " +
                    "data >= @DATAINI and data <= @DATAFIM";          

            sda = new SqlDataAdapter(query, cnn);

            result = new DataTable();
            sda.SelectCommand.Parameters.Add("@DATAINI", SqlDbType.DateTime).Value = dataini;
            sda.SelectCommand.Parameters.Add("@DATAFIM", SqlDbType.DateTime).Value = datafim;
            sda.Fill(result);

            return result;
        }
    }
}
