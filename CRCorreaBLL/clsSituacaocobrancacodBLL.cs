using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using CRCorreaInfo;
using CRCorreaFuncoes;

namespace CRCorreaFuncoes
{ 
    public class clsSituacaocobrancacodBLL : SQLFactory<clsSituacaocobrancacodInfo>
    {
        public DataTable GridDados(String cnn)
        {
            String query;
            DataTable dte = new DataTable();
            query = "SELECT ID, CODIGO, NOME FROM SITUACAOCOBRANCACOD ORDER BY CODIGO";

            SqlDataAdapter sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dte);

            return dte;
        }
    }
}
