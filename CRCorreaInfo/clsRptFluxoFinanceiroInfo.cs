using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsRptFluxoFinanceiroInfo
    {
          public String TABELA { get; set; }
          public Decimal CREDITO { get; set; }
          public Decimal DEBITO { get; set; }
          public Decimal SALDO { get; set; }
          public Int32 ID { get; set; }
          public Int32 FILIAL { get; set; }
          public Int32 DUPLICATA { get; set; }
          public Int32 POSICAO { get; set; }
          public Int32 POSICAOFIM { get; set; }
          public DateTime EMISSAO { get; set; }
          public Int32 IDDOCUMENTO { get; set; }
          public String DOCUMENTO { get; set; }
          public String SETOR { get; set; }
          public Int32 IDCLIENTE { get; set; }
          public String CLIENTE { get; set; }
          public DateTime DATALANCA { get; set; }
          public String EMITENTE { get; set; }
          public Int32 IDHISTORICO { get; set; }
          public String HISTORICO { get; set; }
          public String HISTORICONOM { get; set; }
          public Int32 IDCENTROCUSTO { get; set; }
          public String CENTROCUSTO { get; set; }
          public String CENTROCUSTONOM { get; set; }
          public Int32 IDCODIGOCTABIL { get; set; }
          public String CTACONTABIL { get; set; }
          public String CTACONTABILNOM { get; set; }
          public Int32 IDNOTAFISCAL { get; set; }
          public String NOTAFISCAL { get; set; }
          public Int32 IDNOTAPRINCIPAL { get; set; }
          public Int32 IDFORMAPAGTO { get; set; }
          public String FORMAPAGTO { get; set; }
          public String FORMAPAGTONOM { get; set; }
          public String OBSERVA { get; set; }
          public Int32 IDBANCO { get; set; }
          public String BANCO { get; set; }
          public String BANCONOM { get; set; }
          public Int32 IDBANCOINT { get; set; }
          public String CONTA { get; set; }
          public String NOME { get; set; }
          public String CHEGOU { get; set; }
          public String DESPESAPUBLICA { get; set; }
          public String BOLETO { get; set; }
          public Int32 BOLETONRO { get; set; }
          public Int32 DV { get; set; }
          public String BAIXA { get; set; }
          public DateTime VENCIMENTO { get; set; }
          public DateTime VENCIMENTOPREV { get; set; }
          public Decimal VALOR { get; set; }
          public Decimal VALORDESCONTO { get; set; }
          public String ATEVENCIMENTO { get; set; }
          public Decimal VALORLIQUIDO { get; set; }
          public Decimal VALORJUROS { get; set; }
          public Decimal VALORMULTA { get; set; }
          public Int32 IDSITBANCO { get; set; }
          public String SITBANCOCOD { get; set; }
          public Decimal VALORARECEBER { get; set; }
          public String SITUACAOCODIGO { get; set; }
          public String SITUACAONOME { get; set; }
          public String IMPRIMIR { get; set; }
          public Decimal VALORPAGO { get; set; }
          public Decimal VALORBAIXANDO { get; set; }
          public Decimal VALORDEVOLVIDO { get; set; }
          public Int32 IDVENDEDOR { get; set; }
          public String VENDEDOR { get; set; }
          public Decimal VALORCOMISSAO { get; set; }
          public Int32 IDSUPERVISOR { get; set; }
          public String SUPERVISOR { get; set; }
          public Decimal VALORCOMISSAOSUP { get; set; }
          public Int32 IDCOORDENADOR { get; set; }
          public String COORDENADOR { get; set; }
          public Decimal VALORCOMISSAOGER { get; set; }
    }
}
