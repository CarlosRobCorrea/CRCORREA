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
    public class clsEstadosicmsBLL : SQLFactory<clsEstadosicmsInfo>
    {
        public DataTable GridDados(Int32 idestado, String cnn)
        {
            DataTable dt;
            String query;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
                        "(SELECT " +
                            "ESTADO " +
                          "FROM " +
                            "ESTADOS " +
                          "WHERE " +
                            "ID = ESTADOSICMS.IDESTADODESTINO) AS DESTINO, " +
                        "ALIQUOTA " +
                    "FROM " +
                        "ESTADOSICMS " +
                    "WHERE " +
                        "IDESTADO = @IDESTADO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDESTADO", SqlDbType.Int).Value = idestado;

            sda.Fill(dt);

            return dt;
        }



        public DataTable GridDadosDestino(Int32 idestado, String cnn)
        {
            DataTable dt;
            String query;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
                        "IDESTADODESTINO, " +
                        "ALIQUOTA " +
                    "FROM " +
                        "ESTADOSICMS " +
                    "WHERE " +
                        "IDESTADO = @IDESTADO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDESTADO", SqlDbType.Int).Value = idestado;

            sda.Fill(dt);

            return dt;
        }
    }
}
