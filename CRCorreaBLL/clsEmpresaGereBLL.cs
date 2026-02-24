using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CRCorreaBLL
{
    public class clsEmpresaGereBLL : SQLFactory<clsEmpresaGereInfo>
    {
        public clsEmpresaGereInfo CarregarEmpresa(Int32 empresas_id, String cnn)
        {
            Int32 id;
            clsEmpresaGereInfo info;

            id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from EMPRESAGERE where empresa=" + empresas_id));
            info = Carregar(id, cnn);

            return info;
        }

        public override int Incluir(clsEmpresaGereInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsEmpresaGereInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public bool ExcluirEmpresa(Int32 empresas_id, String cnn)
        {
            Boolean result = true;

            String query;
            DataTable dtItens;
            SqlDataAdapter sda;

            query = "select id from EMPRESAGERE where empresa=@empresa";

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

        public void VerificaInfo(clsEmpresaGereInfo _info)
        {
            if (_info.empresa == 0)
                throw new Exception("falta id da empresa");
        }
    }
}
