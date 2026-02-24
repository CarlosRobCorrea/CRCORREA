using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsOrcamentoInfo
    {
        public Int32 id { get; set; }

        public String atencao { get; set; }
        //public String cognome { get; set; }
        public DateTime data { get; set; }
        public String ddd { get; set; }
        public String email { get; set; }
        public String emitente { get; set; }
        public Int32 filial { get; set; }
        public Int32 idcliente { get; set; }
        public Int32 idcondpag { get; set; }
        public Int32 idresponsavel { get; set; }
        public Int32 idvendedor { get; set; }
        public String motivo { get; set; }
        public Int32 numero { get; set; }
        public String observa { get; set; }
        public String observaa { get; set; }
        public String observa1 { get; set; }
        public String observa2 { get; set; }
        public String observa3 { get; set; }
        public String observa4 { get; set; }
        public Decimal pordesc { get; set; }
        public Decimal qtdeconfirmada { get; set; }
        public Decimal qtdeorcada { get; set; }
        public Decimal qtdeorcadanao { get; set; }
        public String referencia { get; set; }
        public Decimal sedex { get; set; }
        public String setor { get; set; }
        public String situacao { get; set; }
        public String telefone { get; set; }
        public Decimal totalconfirmado { get; set; }
        public Decimal totaldesconto { get; set; }
        public Decimal totalfrete { get; set; }
        public Decimal totalorcamentobruto { get; set; }
        public Decimal totalorcamentoliquido { get; set; }
        public Decimal totalpeso { get; set; }
        public String validade { get; set; }
    }
}
