using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsPedido1Info
    {
        public Decimal aliqcofins1 { get; set; }
        public Decimal aliqcofins1_pc { get; set; }
        public Decimal aliqpispasep { get; set; }
        public Decimal aliqpispasep_st { get; set; }
        public Decimal baseicm { get; set; }
        public Decimal baseicmsubst { get; set; }
        public Decimal basemp { get; set; }
        public Decimal bccofins1 { get; set; }
        public Decimal bccofins1_st { get; set; }
        public Decimal bcipi { get; set; }
        public Decimal bcpispasep { get; set; }
        public Decimal bcpispasep_st { get; set; }
        public String calculoautomatico { get; set; }
        public String cean { get; set; }
        public String cenq { get; set; }
        public String cienq { get; set; }
        public String cnpjprod { get; set; }
        public String codigoemp01 { get; set; }
        public String codigoemp02 { get; set; }
        public String codigoemp03 { get; set; }
        public String codigoemp04 { get; set; }
        public Decimal cofins_qbcprod { get; set; }
        public Decimal cofins_qbcprod_st { get; set; }
        public Decimal cofins_valiqprod { get; set; }
        public Decimal cofins_valiqprod_st { get; set; }
        public Decimal cofins1 { get; set; }
        public Decimal cofins1_st { get; set; }
        public String complemento { get; set; }
        public String consumo { get; set; }
        public String cprod { get; set; }
        public String cselo { get; set; }
        public Decimal custoicm { get; set; }
        public Decimal custoipi { get; set; }
        public String extipi { get; set; }
        public Decimal icm { get; set; }
        public Decimal icmsdif { get; set; }
        public Decimal icminterno { get; set; }
        public Decimal icmsubst { get; set; }
        public Decimal icmssubstreducao { get; set; }
        public Int32 id { get; set; }
        public Int32 idcentrocusto { get; set; }
        public Int32 idcfop { get; set; }
        public Int32 idcodigo { get; set; }
        public Int32 idcontacontabil { get; set; }
        public Int32 iddizeres { get; set; }
        public Int32 idhistorico { get; set; }
        public Int32 idicmsdes { get; set; }
        public Int32 idicms_modbc { get; set; }
        public Int32 idicms_modbcst { get; set; }
        public Int32 idipi { get; set; }
        public Int32 idipicst { get; set; }
        public Int32 idissqn_indISS { get; set; }
        public Int32 idnfcompra { get; set; }
        public Int32 iditemnfcompra { get; set; }
        public Int32 idorcamento { get; set; }
        public Int32 idorcamentoitem { get; set; }
        public Int32 idordemservico { get; set; }
        public Int32 idpedido { get; set; }
        public Int32 idpedido2 { get; set; }
        public Int32 idsittriba { get; set; }
        public Int32 idsittribb { get; set; }
        public Int32 idsittribcofins { get; set; }
        public Int32 idsittribipi { get; set; }
        public Int32 idsittribpis { get; set; }
        public Int32 idsittribpispasep { get; set; }
        public Int32 idtiponota { get; set; }
        public Int32 idunidade { get; set; }
        public Decimal ipi { get; set; }
        public Decimal ipi_qunid { get; set; }
        public Decimal ipi_vunid { get; set; }
        public Decimal issqn_cListServ { get; set; }
        public Decimal issqn_cMun { get; set; }
        public Decimal issqn_cMunFG { get; set; }
        public Decimal issqn_cPais { get; set; }
        public Decimal issqn_cServico { get; set; }
        public Decimal issqn_vAliq { get; set; }
        public Decimal issqn_vBC { get; set; }
        public Decimal issqn_vDeducao { get; set; }
        public Decimal issqn_vDescCond { get; set; }
        public Decimal issqn_vDescIncond { get; set; }
        //public Decimal issqn_indISS { get; set; }
        public Decimal issqn_indincentivo { get; set; }
        public Decimal issqn_nprocesso { get; set; }
        public Decimal issqn_vISSQN { get; set; }
        public Decimal issqn_vISSRet { get; set; }
        public Decimal issqn_vOutro { get; set; }
        public String modbc { get; set; }
        public String modbcst { get; set; }
        public String nve { get; set; }
        public Int32 nitem { get; set; }
        public Decimal qtde { get; set; }
        public Decimal qtdebaixada { get; set; }
        public Decimal qtdedefeito { get; set; }
        public Decimal qtdeentregue { get; set; }
        public Decimal qtdeosaux { get; set; }
        public Decimal qtdesaldo { get; set; }
        public Decimal qtdesucata { get; set; }
        public Decimal peso { get; set; }
        public Decimal pesototal { get; set; }
        public Decimal pispasep { get; set; }
        public Decimal pispasep_st { get; set; }
        public Decimal pis_qbcprod { get; set; }
        public Decimal pis_qbcprod_st { get; set; }
        public Decimal pis_valiqprod { get; set; }
        public Decimal pis_valiqprod_st { get; set; }
        public Decimal pmvast { get; set; }
        public Decimal preco { get; set; }
        public Decimal precocusto { get; set; }
        public Decimal precodesconto { get; set; }
        public Decimal precotabela { get; set; }
        public String qselo { get; set; }
        public String tipoitem { get; set; }
        public Decimal totalcustoitem { get; set; }
        public Decimal totalfaturar { get; set; }
        public Decimal totalmercado { get; set; }
        public Decimal totalnota { get; set; }
        public Decimal totalpeso { get; set; }
        public Decimal totalprevisto { get; set; }
        public Decimal tribdev_impostodevol { get; set; }
        public String tribdev_infadprod { get; set; }
        public Decimal tribdev_ipi { get; set; }
        public Decimal tribdev_pdevol { get; set; }
        public Decimal tribdev_vipidevol { get; set; }
        public Decimal tributo_previsto { get; set; }
        public Decimal valorfrete { get; set; }
        public Decimal valorfreteicms { get; set; }
        public Decimal valoroutras { get; set; }
        public Decimal valoroutrasicms { get; set; }
        public Decimal valorseguro { get; set; }
        public Decimal valorseguroicms { get; set; }

    }
}
