using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CRCorreaFuncoes
{
    public class TransmiteNotaItem
    {
        public Int32 numero_item { get; set; }
        public String codigo_produto { get; set; }
        public String descricao { get; set; }
        public String cfop { get; set; }
        public String unidade_comercial { get; set; }
        public Decimal quantidade_comercial { get; set; }
        public String valor_unitario_comercial { get; set; }
        public String codigo_ncm { get; set; }
        public String valor_total { get; set; }
        public String valor_total_sem_desconto { get; set; }
        public String icms_csosn { get; set; }
        public String pis_situacao_tributaria { get; set; }
        public String cofins_situacao_tributaria { get; set; }
        public String ipi_situacao_tributaria { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String informacao_adicional { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String cest { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String gtin { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String gtin_tributavel { get; set; }
    }

    public class TransmiteNotaDados
    {
        public Int32 tipo_operacao { get; set; }
        public String natureza_operacao { get; set; }
        public Int32 forma_pagamento { get; set; }
        public String meio_pagamento { get; set; }
        public String pagamento_cnpj { get; set; }
        public String pagamento_tband { get; set; }
        public String pagamento_caut { get; set; }
        public String data_emissao { get; set; }
        public String data_saida_entrada { get; set; }
        public String hora_saida_entrada { get; set; }
        public Int32 finalidade_emissao { get; set; }
        public String valor_total { get; set; }
        public String valor_total_sem_desconto { get; set; }
        public String valor_ipi { get; set; }
        public Int32 modalidade_frete { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String nome_destinatario { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String cnpj_destinatario { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String informacoes_adicionais_contribuinte { get; set; }

        public List<TransmiteNotaItem> Itens { get; set; }
    }

    public class TransmiteNotaRequest
    {
        public String ApiKey { get; set; }
        public String Cnpj { get; set; }
        public TransmiteNotaDados Dados { get; set; }
    }
}
