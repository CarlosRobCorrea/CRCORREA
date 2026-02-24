using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCorreaFuncoes
{
    public class clsInfo
    {
        public static String arquivos { get; set; }
        public static String caminhorelatorios { get; set; }
        public static String conexaorpt { get; set; }
        public static String conexaosqlbanco { get; set; }
        public static String conexaosqldados { get; set; }
        public static String conexaosqlfolha { get; set; }
        public static String imagens { get; set; }
        public static String saidaxml { get; set; }
        public static CrystalDecisions.CrystalReports.Engine.ReportDocument rdtDocumento { get; set; }
        public static DataTable sqlLanguage { get; set; }
        public static Int32 zano { get; set; }
        public static String zarquivoreport { get; set; }
        public static String zbaixarcomhistorico { get; set; }
        public static String zbaixatemnotafiscal { get; set; }
        public static Int32 zbanco { get; set; }
        public static Int32 zbancoint { get; set; }
        public static Int32 zcentrocustos { get; set; }
        public static Int32 zcertificado { get; set; }
        public static Int32 zcfop { get; set; }
        public static Int32 zclassificacao { get; set; }
        public static Int32 zclassificacao1 { get; set; }
        public static Int32 zclassificacao2 { get; set; }
        public static Int32 zcompras { get; set; }
        public static Int32 zcompras1 { get; set; }
        public static Int32 zcomprasentrega { get; set; }
        public static Int32 znfcompravales {  get; set; }
        public static Int32 zcondpagto { get; set; }
        public static Int32 zcontacontabil { get; set; }
        public static Int32 zcotacao { get; set; }
        public static Int32 zcotacao1 { get; set; }
        public static Int32 zcotacao2 { get; set; }
        public static Int32 zdizeresnf { get; set; }
        public static Int32 zdocumento { get; set; }
        public static String zempresa_cnpj { get; set; }
        public static Int32 zempresa_ufid { get; set; }
        public static Int32 zempresaid { get; set; }
        public static Int32 zempresa_cidadeibge { get; set; }
        public static Int32 zempresaclienteid { get; set; }
        public static String zempresacliente_cognome { get; set; } // cognome da empresa como cliente
        public static String zempresa_uf { get; set; }
        public static Int32 zfilial { get; set; }
        public static Int32 zfiscalcofins { get; set; }
        public static Int32 zfiscalcofinsid { get; set; }
        public static Int32 zfiscalinss { get; set; }
        public static Int32 zfiscalinssid { get; set; }
        public static Int32 zfiscalcsll { get; set; }
        public static Int32 zfiscalcsllid { get; set; }
        public static Int32 zfiscalirrf { get; set; }
        public static Int32 zfiscalirrfid { get; set; }
        public static Int32 zfiscalisid { get; set; }
        public static Int32 zfiscaliss { get; set; }
        public static Int32 zfiscalissid { get; set; }
        public static Int32 zfiscalpis { get; set; }
        public static Int32 zfiscalpisid { get; set; }
        public static Int32 zfiscalpiscofins { get; set; }
        public static Int32 zfiscalpiscofinsid { get; set; }
        public static Int32 zformapagto { get; set; }
        public static Int32 zfuncionarioid { get; set; }
        public static Int32 zleicofins { get; set; }
        public static Int32 zhistoricos { get; set; }
        public static Int32 zidincluir { get; set; }
        public static Int32 zipi { get; set; }
        public static Int32 zlastid { get; set; }
        public static Int32 zmes { get; set; }
        public static Int32 zmarca { get; set; }
        public static Int32 zmodulo { get; set; }
        public static Int32 znfcompra { get; set; }
        public static Int32 znfcompra1 { get; set; }
        public static Int32 znfcomprapagar { get; set; }
        public static Int32 znfvenda { get; set; }
        public static Int32 znfvenda1 { get; set; }
        public static String znomegrid { get; set; }
        public static Int32 zoperacoes { get; set; }
        public static Int32 zorcamento { get; set; }
        public static Int32 zorcamento1 { get; set; }
        public static Int32 zordemservico { get; set; }
        public static Int32 zpecas { get; set; }
        public static Int32 zpecasfamilia { get; set; }
        public static Int32 zpedido { get; set; }
        public static Int32 zpedido1 { get; set; }
        public static Int32 zpedido2 { get; set; }

        public static Int32 zramo { get; set; }
        public static Int32 zregimeapuracao { get; set; }
        public static DataGridViewRow zrow { get; set; }
        public static Int32 zrowid { get; set; }
        public static Int32 zsituacaocobrancacod { get; set; }
        public static Int32 zsituacaotitulo { get; set; }
        public static Int32 zsituacaotriba { get; set; }
        public static Int32 zsituacaotribb { get; set; }
        public static Int32 zsittribcofins { get; set; }
        public static Int32 zsittribipi { get; set; }
        public static Int32 zsittribpis { get; set; }
        public static Int32 zsolicitacao { get; set; }
        public static Int32 ztiponota { get; set; }
        public static Int32 ztolerancia { get; set; }
        public static Int32 zunidade { get; set; }
        public static String zusuario { get; set; }
        public static Int32 zusuarioid { get; set; }
        public static Int32 zvendedorid { get; set; }
        public static Int32 zzona { get; set; }
    }
}
