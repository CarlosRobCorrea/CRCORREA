using CRCorreaFuncoes;
using CRCorreaInfo;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaBLL
{
    public class clsConfiguracaoBLL : SQLFactory<clsConfiguracaoInfo>
    {
        String conexao;
        public  clsConfiguracaoInfo Carregar(String _conexao)
        {
            conexao = _conexao;
            var id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(conexao, "select id from configuracao"));
            var info = this.Carregar(id, conexao);

            return info;
        }

    }
}
