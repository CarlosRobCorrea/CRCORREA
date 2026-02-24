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
    public class clsTab_PaisesBLL : SQLFactory<clsTab_PaisesInfo>
    {
        public DataTable GridDados(String cnn)
        {
            String query;
            DataTable dte = new DataTable();

            query = "SELECT " +
                        "ID, " +
                        "UF, " +
                        "NOME, " +
                        "DDD, " +
                        "DDDDIRETO, " +
                        "ISO3166A, " +
                        "ISO3166B, " +
                        "ISO3166C, " +
                        "NOMEINT, " +
                        "BACEN, " +
                        "AREA, " +
                        "PERIMETRO, " +
                        "ANOFUNDACAO, " +
                        "CONTINENTE, " +
                        "COMEX " +
                    "FROM " +
                        "TAB_PAISES ";

            SqlDataAdapter sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dte);

            return dte;
        }
    }
}
