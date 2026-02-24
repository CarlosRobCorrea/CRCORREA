using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsNFVendaInfo
    {
        public String cdv { get; set; }
        public String chnfe { get; set; }
        public Int32 cnf { get; set; }
        public Decimal cofins1 { get; set; }  // total cofins
        public String cognome { get; set; } // string 20
        public String cpf { get; set; } // string 18
        public Int32 cstat { get; set; }
        public DateTime data { get; set; }
        public DateTime datalanca { get; set; }
        public DateTime datasaida { get; set; }
        public DateTime dhcont { get; set; }
        public String embespecie { get; set; }
        public String embmarca { get; set; }
        public String embqtde { get; set; }
        public String emb_nvol { get; set; }
        public String emitente { get; set; }
        public Int32 filial { get; set; }
        public Int32 finnfe { get; set; }
        public String fretepaga { get; set; }
        public String grupocompra_xcont { get; set; }
        public String grupocompra_xnemp { get; set; }
        public String grupocompra_xped { get; set; }
        public Int32 id { get; set; }
        public Int32 id_indproc { get; set; }
        public Int32 id_procemi { get; set; }
        public Int32 idcliente { get; set; }
        public Int32 idcondpagto { get; set; }
        public Int32 iddest { get; set; }
        public Int32 iddocumento { get; set; }
        public Int32 identrega { get; set; }
        public Int32 idretira { get; set; }
        public Int32 idtransportadora { get; set; }
        public Int32 indfinal { get; set; }
        public String indpag { get; set; }
        public Int32 indpres { get; set; }
        public String infadfisco { get; set; }
        public String infcpl { get; set; }
        public String natop { get; set; }
        public String nproc { get; set; }
        public Int32 numero { get; set; }
        public String observa { get; set; }
        public Decimal pispasep { get; set; }  // total pispasep
        public String relnfe { get; set; }
        public String serie { get; set; }
        public String situacao { get; set; }
        public String telefone { get; set; } //String 12
        public String tipoentrada { get; set; }
        public Decimal totalbaseicm { get; set; }
        public Decimal totalbaseicmsubst { get; set; }
        public Decimal totaldesconto { get; set; }
        public Decimal totalfrete { get; set; }
        public Decimal totalicm { get; set; }
        public Decimal totalicmsdeson { get; set; }
        public Decimal totalicmsubst { get; set; }
        public Decimal totalipi { get; set; }
        public Decimal totalmercadoria { get; set; }
        public Decimal totalnotafiscal { get; set; }
        public Decimal totaloutras { get; set; }
        public Decimal totalpeso { get; set; }
        public Decimal totalpesobruto { get; set; }
        public Decimal totalseguro { get; set; }
        public String total_issq_cregtrib { get; set; }
        public DateTime total_issq_dcompet { get; set; }
        public Decimal total_issq_vbc { get; set; }
        public Decimal total_issq_vcofins { get; set; }
        public Decimal total_issq_vdeducao { get; set; }
        public Decimal total_issq_vdesccond { get; set; }
        public Decimal total_issq_vdescincond { get; set; }
        public Decimal total_issq_viss { get; set; }
        public Decimal total_issq_vissret { get; set; }
        public Decimal total_issq_voutro { get; set; }
        public Decimal total_issq_vpis { get; set; }
        public Decimal total_issq_vserv { get; set; }
        public Decimal total_nfe_retencao_vbcirrf { get; set; }
        public Decimal total_nfe_retencao_vbcretprev { get; set; }
        public Decimal total_nfe_retencao_virrf { get; set; }
        public Decimal total_nfe_retencao_vretcofins { get; set; }
        public Decimal total_nfe_retencao_vretcsll { get; set; }
        public Decimal total_nfe_retencao_vretpis { get; set; }
        public Decimal total_nfe_retencao_vretprev { get; set; }
        public Decimal total_tottrib { get; set; }
        public Decimal total_transporte_picmsret { get; set; }
        public Decimal total_transporte_vbcret { get; set; }
        public Decimal total_transporte_vicmsret { get; set; }
        public Decimal total_transporte_vserv { get; set; }
        public Decimal total_vll { get; set; }
        public String tpamb { get; set; }
        public String tpemis { get; set; }
        public String tpimp { get; set; }
        public Int32 tpnf { get; set; }
        public String tpag { get; set; }
        public String transporte_cfop { get; set; }
        public String transporte_cmunfg { get; set; }
        public String transporte_placa { get; set; }
        public String transporte_placa_reboque { get; set; }
        public String transporte_rntc { get; set; }
        public String transporte_rntc_reboque { get; set; }
        public String transporte_uf { get; set; }
        public String transporte_uf_reboque { get; set; }
        public String verproc { get; set; }
        public String versao { get; set; }
        public String xcampo { get; set; }
        public String xjust { get; set; }
        public String xtexto { get; set; }

    }
}
