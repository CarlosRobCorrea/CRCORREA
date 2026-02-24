using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsXmlNFCompraInfo
    {
        public Int32 id { get; set; }
        public DateTime data { get; set; }
        public String fornecedor { get; set; } // nome do fornecedor
        public Int32 idfornecedor { get; set; }
        public Int32 numero { get; set; }
        public Int32 status { get; set; }
        public Decimal totalnotafiscal { get; set; }


    }
}
