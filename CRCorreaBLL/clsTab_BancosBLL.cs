using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsTab_BancosBLL : SQLFactory<clsTab_BancosInfo>
    {
        public override int Incluir(clsTab_BancosInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsTab_BancosInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public void VerificaInfo(clsTab_BancosInfo _info)
        {
            if (_info.codigo.ToString().Trim().Length == 0)
            {
                throw new Exception("Código inválido.");
            }
            
            if (_info.cognome.ToString().Trim().Length == 0)
            {
                throw new Exception("CogNome inválido.");
            }

            if (_info.nome.ToString().Trim().Length == 0)
            {
                throw new Exception("Nome inválido.");
            }
        }

        public DataTable GridDados(String ativo, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
                        "CODIGO, " +
                        "COGNOME, " +
                        "NOME, " +
                        "ATIVO, " +
                        "HOMEPAGE " +
                    "FROM " +
                        "TAB_BANCOS " +
                    " WHERE TAB_BANCOS.CODIGO > 0 ";
            if (ativo == "S" || ativo == "N")
            {
                query += " AND ATIVO = @ATIVO ";
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
