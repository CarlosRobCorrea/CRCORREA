using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CRCorreaBLL
{
    public class clsEmpresaFolhaBLL : SQLFactory<clsEmpresaFolhaInfo>
    {
        public clsEmpresaFolhaInfo CarregarEmpresa(Int32 empresas_id, String cnn)
        {
            Int32 id;
            clsEmpresaFolhaInfo info;

            id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from EMPRESAGERE where empresa=" + empresas_id));
            info = Carregar(id, cnn);

            return info;
        }

        public bool ExcluirEmpresa(Int32 empresas_id, String cnn)
        {
            Boolean result = true;

            String query;
            DataTable dtItens;
            SqlDataAdapter sda;

            query = "select id from empresasfolha where empresa=@empresa";

            dtItens = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@empresa", SqlDbType.Int).Value = empresas_id;
            sda.Fill(dtItens);

            foreach (DataRow row in dtItens.Rows)
            {
                if (result == false)
                {
                    Excluir(clsParser.Int32Parse(row[0].ToString()), cnn);
                }
                else
                {
                    result = Excluir(clsParser.Int32Parse(row[0].ToString()), cnn);
                }
            }

            return result;
        }
    }
}
