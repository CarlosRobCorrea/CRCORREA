using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsFechamentoCaixaInfo
    {
        public Int32 id { get; set; }
        public Decimal apurado { get; set; }
        public DateTime data { get; set; }
        public Decimal pix { get; set; }
        public Decimal dinheiro { get; set; }
        public Decimal cartaodebito { get; set; }
        public Decimal cartaocredito { get; set; }
        public Decimal parcelado { get; set; }
        public Decimal totalcusto { get; set; }
        public Decimal totaldia { get; set; }

    }
}
