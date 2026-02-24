using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace CRCorreaInfo
{
    public class clsNFCEInfo
    {
        public String natureza_operacao { get; set; }
        public Int32 serie { get; set; }
        public Int32 numero { get; set; }
        public DateTime data_emissao { get; set; }
        public DateTime data_entrada_saida { get; set; }
        public DateTime data_previsao_entrega_bem { get; set; }
        public String tipo_documento { get; set; }
        public Int32 local_destin { get; set; }
        public Int32 municipio { get; set; }
        public Int32 ibs_cbs_municipio { get; set; }
        public Int32 finalidade_emissao { get; set; }
        public String tipo_nota_debito { get; set; }
        public String tipo_nota_credito { get; set; }
        public String consumidor_final { get; set; }
        public String presenca_comprador { get; set; }
        public String indicador_intermediario { get; set; }
        public String notas_referenciadas { get; set; }
        public String tipo_compra_governamental { get; set; }
        public Decimal perc_reducao_aliquota_compra_governamental { get; set; }

        public Int32 tipo_operacao_governamental { get; set; }
        public String chaves_nfe_antecipacao_pagamento { get; set; }
        public Int32 cnpj_emitente { get; set; }
        public Int32 cpf_emitente { get; set; }

        public String nome_emitente { get; set; }
        public String nome_fantasia_emitente { get; set; }
        public String logradouro_emitente { get; set; }
        public String numero_emitente { get; set; }
        public String complemento_emitente { get; set; }
        public String bairro_emitente { get; set; }
        public Int32 codigo_municipio_emitente { get; set; }

        public String municipio_emitente { get; set; }
        public String uf_emitente { get; set; }
        public Int32 cep_emitente { get; set; }
        public Int32 telefone_emitente { get; set; }
        public String inscricao_estadual_emitente { get; set; }
        public String inscricao_estadual_st_emitente { get; set; }
        public String inscricao_municipal_emitente { get; set; }
        public Int32 cnae_fiscal_emitente { get; set; }
        public String regime_tributario_emitente { get; set; }

        public Int32 cnpj_destinatario { get; set; }
        public Int32 cpf_destinatario { get; set; }
        public String id_estrangeiro_destinatario { get; set; }
        public String nome_destinatario { get; set; }
        public String logradouro_destinatario { get; set; }
        public String numero_destinatario { get; set; }
        public String complemento_destinatario { get; set; }
        public String bairro_destinatario { get; set; }
        public Int32 codigo_municipio_destinatario { get; set; }
        public String municipio_destinatario { get; set; }
        public String uf_destinatario { get; set; }
        public String cep_destinatario { get; set; }
        public String codigo_pais_destinatario { get; set; }
        public String pais_destinatario { get; set; }
        public String telefone_destinatario { get; set; }
        public String indicador_inscricao_estadual_destinatario { get; set; }
        public String inscricao_estadual_destinatario { get; set; }
        public String inscricao_suframa_destinatario { get; set; }
        public String inscricao_municipal_destinatario { get; set; }
        public String email_destinatario { get; set; }
        public String cnpj_retirada { get; set; }
        public String cpf_retirada { get; set; }
 
        public String nome_retirada { get; set; }
        public String logradouro_retirada { get; set; }
        public String numero_retirada { get; set; }
        public String complemento_retirada { get; set; }
        public String bairro_retirada { get; set; }
        public String codigo_municipio_retirada { get; set; }
        public String municipio_retirada { get; set; }
        public String uf_retirada { get; set; }
        public String cep_retirada { get; set; }
        public String codigo_pais_retirada { get; set; }
        public String pais_retirada { get; set; }
        public String telefone_retirada { get; set; }
        public String email_retirada { get; set; }
        public String inscricao_estadual_retirada { get; set; }
        public String cnpj_entrega { get; set; }
        public String cpf_entrega { get; set; }
        public String nome_entrega { get; set; }
        public String logradouro_entrega { get; set; }
    public String numero_entrega { get; set; }
public String complemento_entrega { get; set; }
public String bairro_entrega { get; set; }
public String codigo_municipio_entrega { get; set; }
public String municipio_entrega { get; set; }
public String uf_entrega { get; set; }
public String cep_entrega { get; set; }
public String codigo_pais_entrega { get; set; }
public String pais_entrega { get; set; }
public String inscricao_estadual_entrega { get; set; }

/// <summary>
/// //  itens
/// </summary>
public String icms_base_calculo { get; set; }
public String icms_valor_total { get; set; }
public String icms_valor_total_desonerado { get; set; }
public String fcp_valor_total_uf_destino { get; set; }
public String icms_valor_total_uf_destino { get; set; }
public String icms_valor_total_uf_remetente { get; set; }
public String fcp_valor_total { get; set; }
public String icms_base_calculo_st { get; set; }

public String icms_valor_total_st { get; set; }
public String fcp_valor_total_st { get; set; }
public String fcp_valor_total_retido_st { get; set; }
        public String icms_base_calculo_mono { get; set; }
        public String icms_valor_mono { get; set; }
        public String icms_base_calculo_mono_retencao { get; set; }
        public String icms_valor_mono_retencao { get; set; }
        public String icms_base_calculo_mono_retido { get; set; }
        public String icms_valor_mono_retido { get; set; }
        public String valor_produtos { get; set; }
        public String valor_frete { get; set; }
        public String valor_seguro { get; set; }
        public String valor_desconto { get; set; }
        public String valor_total_ii { get; set; }
        public String valor_ipi { get; set; }
        public String valor_ipi_devolvido { get; set; }
        public String valor_pis { get; set; }
        public String valor_cofins { get; set; }
        public String valor_outras_despesas { get; set; }
        public String valor_total { get; set; }
        public String valor_total_tributos { get; set; }
        public String valor_total_servicos { get; set; }
        public String issqn_base_calculo { get; set; }

        public String issqn_valor_total { get; set; }
        public String valor_pis_servicos { get; set; }
        public String valor_cofins_servicos { get; set; }
        public String data_prestacao_servico { get; set; }
        public String issqn_valor_total_deducao { get; set; }
        public String issqn_valor_total_outras_retencoes { get; set; }
        public String issqn_valor_total_desconto_incondicionado { get; set; }
        public String issqn_valor_total_desconto_condicionado { get; set; }


        public String issqn_valor_total_retencao { get; set; }
        public String codigo_regime_especial_tributacao { get; set; }
        public String pis_valor_retido { get; set; }
        public String cofins_valor_retido { get; set; }
        public String csll_valor_retido { get; set; }
        public String irrf_base_calculo { get; set; }
        public String irrf_valor_retido { get; set; }
        public String prev_social_base_calculo { get; set; }
        public String prev_social_valor_retido { get; set; }
        public String is_valor_total { get; set; }

        public String ibs_cbs_base_calculo { get; set; }
        public String ibs_uf_valor_total_diferimento { get; set; }
        public String ibs_uf_valor_total_devolucao { get; set; }
        public String ibs_uf_valor_total { get; set; }
        public String ibs_mun_valor_total_diferimento { get; set; }
        public String ibs_mun_valor_total_devolucao { get; set; }
        public String ibs_mun_valor_total { get; set; }
        public String ibs_valor_total { get; set; }
        public String ibs_valor_total_credito_presumido { get; set; }
        public String ibs_valor_total_condicao_suspensiva { get; set; }
        public String cbs_valor_total_diferimento { get; set; }
        public String cbs_valor_total_devolucao { get; set; }
        public String cbs_valor_total { get; set; }
        public String cbs_valor_total_credito_presumido { get; set; }
        public String cbs_valor_total_condicao_suspensiva { get; set; }
        public String ibs_valor_total_monofasico { get; set; }
        public String cbs_valor_total_monofasico { get; set; }
        public String ibs_valor_total_monofasico_retencao { get; set; }
        public String cbs_valor_total_monofasico_retencao { get; set; }
        public String ibs_valor_total_monofasico_retido { get; set; }
        public String cbs_valor_total_monofasico_retido { get; set; }
        public String ibs_valor_total_credito_estornado { get; set; }
        public String cbs_valor_total_credito_estornado { get; set; }
        public String ibs_cbs_is_valor_total { get; set; }
        public String modalidade_frete { get; set; }
        public String cnpj_transportador { get; set; }
        public String cpf_transportador { get; set; }
        public String nome_transportador { get; set; }
        public String inscricao_estadual_transportador { get; set; }
        public String endereco_transportador { get; set; }
        public String municipio_transportador { get; set; }
        public String uf_transportador { get; set; }
        public String transporte_icms_servico { get; set; }
        public String transporte_icms_base_calculo { get; set; }
        public String transporte_icms_aliquota { get; set; }
        public String transporte_icms_valor { get; set; }
        public String transporte_icms_cfop { get; set; }
        public String transporte_icms_codigo_municipio { get; set; }
        public String veiculo_placa { get; set; }
        public String veiculo_uf { get; set; }
        public String veiculo_rntc { get; set; }
        public String reboques { get; set; }
        public String veiculo_identificacao_vagao { get; set; }
        public String veiculo_identificacao_balsa { get; set; }
        public String volumes { get; set; }
        public String numero_fatura { get; set; }
        public String valor_original_fatura { get; set; }
        public String valor_desconto_fatura { get; set; }
        public String valor_liquido_fatura { get; set; }
        public String duplicatas { get; set; }
        public String formas_pagamento { get; set; }
        public String valor_troco { get; set; }
        public String cnpj_intermediario { get; set; }
        public String id_intermediario { get; set; }
        public String informacoes_adicionais_fisco { get; set; }

        public String informacoes_adicionais_contribuinte { get; set; }
        public String observacoes_contribuinte { get; set; }
        public String observacoes_fisco { get; set; }
        public String processos_referenciados { get; set; }
        public String uf_local_embarque { get; set; }
        public String local_embarque { get; set; }
        public String local_despacho { get; set; }
        public String N/A { get; set; }
        public String nota_empenho_compra { get; set; }
        public String pedido_compra { get; set; }
        public String contrato_compra { get; set; }

        public String cnpj_responsavel_tecnico { get; set; }
        public String contato_responsavel_tecnico { get; set; }
        public String email_responsavel_tecnico { get; set; }
        public String telefone_responsavel_tecnico { get; set; }
        public String identificador_csrt { get; set; }
        public String hash_csrt { get; set; }
        public String agropecuario_defensivo { get; set; }
        public String agropecuario_tipo_guia { get; set; }
        public String agropecuario_uf_guia { get; set; }
        public String agropecuario_serie_guia { get; set; }
        public String agropecuario_numero_guia { get; set; }
    }
}
