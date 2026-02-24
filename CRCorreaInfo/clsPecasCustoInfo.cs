using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsPecasCustoInfo
    {
        public Int32 id { get; set; }
        public Int32 idpeca { get; set; }
        public Int32 ordem { get; set; }
        public Int32 idcodigo { get; set; }
        public String tipo { get; set; }
        public Decimal qtde { get; set; }
        public Int32 idunidade { get; set; }
        public Decimal participacao { get; set; }
        public Decimal precounitario { get; set; }
        public Decimal precototal { get; set; }
    }
}
