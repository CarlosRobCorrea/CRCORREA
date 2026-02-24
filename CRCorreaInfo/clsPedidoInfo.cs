using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsPedidoInfo
    {
        public Int32 id { get; set; }
        public Int32 ano { get; set; }
        public Decimal comissaorepresentante { get; set; }
        public DateTime data { get; set; }
        public String emitente { get; set; }
        public Int32 filial { get; set; }
        public String frete { get; set; }
        public Decimal fretebaseicms { get; set; }
        public Decimal freteicmaliq { get; set; }
        public String fretepaga { get; set; }
        public Decimal freteprecouni { get; set; }
        public Decimal freteqtde { get; set; }
        public String fretetipo { get; set; }
        public Decimal fretetotal { get; set; }
        public Decimal fretevaloricms { get; set; }
        public String freteunid { get; set; }
        public Int32 idcliente { get; set; }
        public Int32 idcondpagto { get; set; }
        public Int32 idformapagto { get; set; }
        public Int32 idredespacho { get; set; }
        public Int32 idtransportadora { get; set; }
        public Int32 idvendedor { get; set; }
        public Int32 numero { get; set; }
        public String observa { get; set; }
        public String pago_ctareceber { get; set; }
        public Decimal qtdebaixada { get; set; }
        public Decimal qtdedefeito { get; set; }
        public Decimal qtdeentregue { get; set; }
        public Decimal qtdeosaux { get; set; }
        public Decimal qtdepedido { get; set; }
        public Decimal qtdesaldo { get; set; }
        public Decimal qtdesucata { get; set; }
        public String situacao { get; set; }
        public String setor { get; set; }
        public Decimal setorfator { get; set; }
        public String transporte { get; set; }
        public String tipofrete { get; set; }
        public String tipodesconto { get; set; }
        public Decimal totalbaseicm { get; set; }
        public Decimal totalbaseicmsubst { get; set; }
        public Decimal totalcofins1 { get; set; }
        public Decimal totalcofins1retido { get; set; }
        public Decimal totalcusto { get; set; }
        public Decimal totaldesconto { get; set; }
        public Decimal totalfrete { get; set; }
        public Decimal totalicm { get; set; }
        public Decimal totalicmsubst { get; set; }
        public Decimal totalipi { get; set; }
        public Decimal totalmercadoria { get; set; }
        public Decimal totaloutras { get; set; }
        public Decimal totalpedido { get; set; }
        public Decimal totalpeso { get; set; }
        public Decimal totalpispasep { get; set; }
        public Decimal totalpispasepretido { get; set; }
        public Decimal totalprevisto { get; set; }
        public Decimal totalseguro { get; set; }    }
}
