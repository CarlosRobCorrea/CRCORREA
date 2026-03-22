using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CRCorreaFuncoes
{
    public class Produto
    {
        public String numero_item { get; set; }
        public String codigo_produto { get; set; }
        public String descricao { get; set; }
        public Int32 cfop { get; set; }
        public String unidade_comercial { get; set; }
        public Decimal quantidade_comercial { get; set; }
        public Decimal valor_unitario_comercial { get; set; }
        public String codigo_ncm { get; set; }
        public Decimal valor_total { get; set; }
        public Decimal valor_total_sem_desconto { get; set; }
        public Int32 icms_csosn { get; set; }
        public String pis_situacao_tributaria { get; set; }
        public String cofins_situacao_tributaria { get; set; }
        public String ipi_situacao_tributaria { get; set; }

    }

    public class Nota_NFCE
    {
        public String ApiKey { get; set; }
        public String Cnpj { get; set; }
        public Dados_NFCE Dados { get; set; }


    }
    public class Dados_NFCE
    {
        public String tipo_operacao { get; set; }
        public String natureza_operacao { get; set; }
        public Int32 forma_pagamento { get; set; }
        public String meio_pagamento { get; set; }
        public String pagamento_cnpj { get; set; }
        public String pagamento_tband { get; set; }
        public String pagamento_caut { get; set; }
        public String data_emissao { get; set; }
        public String data_saida_entrada { get; set; }
        public String hora_saida_entrada { get; set; }
        public String finalidade_emissao { get; set; }
        public Decimal valor_total { get; set; }
        public Decimal valor_total_sem_desconto { get; set; }
        public Decimal valor_ipi { get; set; }
        public Int32 modalidade_frete { get; set; }
        public List<Produto> items { get; set; }

    }


}
