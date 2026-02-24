using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace CRCorreaInfo
{
    public class clsDanfeInfo
    {
        /*
        public String cnpj_emitente { get; set; }
        public String data_emissao { get; set; }
        public String indicador_inscricao_estadual_destinatario { get; set; }
        public String modalidade_frete { get; set; }
        public String local_destino { get; set; }
        public String presenca_comprador { get; set; }
        public String natureza_operacao { get; set; }
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
        */



        /*
        // Identificacao da Nota Fiscal (ide)
        public Int32 cUF { get; set; }
        public Int32 cNF { get; set; }
        public String natOp { get; set; }
        public String mod { get; set; }
        public Int32 serie { get; set; }
        public Int32 nNF { get; set; }
        public DateTime dhEmi { get; set; }
        public Int32 idDest { get; set; }
        public Int32 cMunFG { get; set; }
        public Int32 tpImp { get; set; }
        public Int32 tpEmis { get; set; }
        public Int32 cDV { get; set; }
        public Int32 tpAmb { get; set; }
        public Int32 finNFe { get; set; }
        public Int32 procEmi { get; set; }
        public String verProc { get; set; }
        public Int32 indFinal { get; set; }
        public Int32 indPres { get; set; }
        public Int32 indIntermed { get; set; }
        // Identificacao da Emitente (emit)
        public String xNome { get; set; }
        public String xFant { get; set; }
        public String IE { get; set; }
        public String IM { get; set; }
        public Int32 CNAE { get; set; }
        public Int32 CRT { get; set; }
        public Int32 CNPJ { get; set; }
        public Int32 CPF { get; set; }
        // Endereço do Emitente (enderEmit)
        public String xLgr { get; set; }
        public String nro { get; set; }
        public String xBairro { get; set; }
        public Int32 cMun { get; set; }
        public String xMun { get; set; }
        public String UF { get; set; }
        public Int32 CEP { get; set; }
        public Int32 cPais { get; set; }
        public String xPais { get; set; }
        public Int32 fone { get; set; }
        // Identificacao da Emitente (dest)
        public String dest_xNome { get; set; }
        public Int32 dest_IE { get; set; }
        public String dest_email { get; set; }
        public Int32 dest_indIEDest { get; set; }
        public Int32 dest_CNPJ { get; set; }
        public Int32 dest_CPF { get; set; }
        // Endereço do Emitente (enderDest)
        public String dest_xLgr { get; set; }
        public String dest_nro { get; set; }
        public String dest_xBairro { get; set; }
        public Int32 dest_cMun { get; set; }
        public String dest_xMun { get; set; }
        public String dest_UF { get; set; }
        public Int32 dest_CEP { get; set; }
        public Int32 dest_cPais { get; set; }
        public String dest_xPais { get; set; }
        public Int32 dest_fone { get; set; }
        // Detalhamento de Produtos e Serviços da NF-e (det)
        public Int32 nItem { get; set; }
        // Detalhamento de Produtos e Serviços da NF-e (prod)
        public String cProd { get; set; }
        public Int32 cEAN { get; set; }
        public String xProd { get; set; }
        public Int32 NCM { get; set; }
        public Int32 CFOP { get; set; }
        public String uCom { get; set; }
        public Decimal qCom { get; set; }
        public Decimal vUnCom { get; set; }
        public Decimal vProd { get; set; }
        public Int32 cEANTrib { get; set; }
        public String uTrib { get; set; }
        public Decimal qTrib { get; set; }
        public Decimal vUnTrib { get; set; }
        public Int32 indTot { get; set; }
        // Total Nota (total)
        // Total Nota Referente ao ICMS (ICMSTot)
        public Decimal vBC { get; set; }
        public Decimal vICMS { get; set; }
        public Decimal vBCST { get; set; }
        public Decimal vST { get; set; }
        public Decimal icmstot_vProd { get; set; }
        public Decimal vFrete { get; set; }
        public Decimal vSeg { get; set; }
        public Decimal vDesc { get; set; }
        public Decimal vII { get; set; }
        public Decimal vIPI { get; set; }
        public Decimal vPIS { get; set; }
        public Decimal vCOFINS { get; set; }
        public Decimal vOutro { get; set; }
        public Decimal vNF { get; set; }
        public Decimal vTotTrib { get; set; }
        public Decimal vICMSDeson { get; set; }
        public Decimal vFCPUFDest { get; set; }
        public Decimal vICMSUFDest { get; set; }
        public Decimal vICMSUFRemet { get; set; }
        public Decimal vFCP { get; set; }
        public Decimal vvFCPSTBC { get; set; }
        public Decimal vFCPSTRet { get; set; }
        public Decimal vIPIDevol { get; set; }
        // Transporte (transp)
        public Int32 modFrete { get; set; }
        // Cobranca cobr (fat)
        public String nFat { get; set; }
        public Decimal vOrig { get; set; }
        public Decimal fat_vDesc { get; set; }
        public Decimal vLiq { get; set; }
        // Pagamento pag (detPag)
        public Int32 indPag { get; set; }
        public Int32 tPag { get; set; }
        public Decimal vPag { get; set; }
        // Informação Adicional (infAdic)
        public String infCpl { get; set; }
        // Informações Responsavel Tecnico (infRespTec)
        public Int32 infresptec_CNPJ { get; set; }
        public String infresptec_xContato { get; set; }
        public String infresptec_email { get; set; }
        public Int32 infresptec_fone { get; set; }
        */

    }
}
