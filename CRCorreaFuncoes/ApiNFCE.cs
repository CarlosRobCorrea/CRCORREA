using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using Newtonsoft.Json;


namespace CRCorreaFuncoes
{
    public class Produto
    {
        public String numero_item { get; set; }
        public String codigo_ncm { get; set; }
        public String quantidade_comercial { get; set; }
        public String quantidade_tributavel { get; set; }
        public String cfop { get; set; }
        public String valor_unitario_tributavel { get; set; }
        public String valor_unitario_comercial { get; set; }
        public String valor_desconto { get; set; }
        public String descricao { get; set; }
        public String codigo_produto { get; set; }
        public String icms_origem { get; set; }
        public String icms_situacao_tributaria { get; set; }
        public String unidade_comercial { get; set; }
        public String unidade_tributavel { get; set; }
        public String valor_total_tributos { get; set; }

    }
    public class Formas_Pagamentos
    {
        public String forma_pagamento { get; set; }
        public String valor_pagamento { get; set; }
        public String nome_credenciadora { get; set; }
        public String bandeira_operadora { get; set; }
        public String numero_autorizacao { get; set; }

    }


    public class Relatorio
    {
        public String cnpj_emitente { get; set; }
        public String data_emissao { get; set; }
        public String indicador_inscricao_estadual_destinatario { get; set; }
        public String modalidade_frete { get; set; }
        public String local_destino { get; set; }
        public String presenca_comprador { get; set; }
        public String natureza_operacao { get; set; }

        public List<Produto> items { get; set; }
        public List<Formas_Pagamentos> formas_pagamento { get; set; }
    }

}
