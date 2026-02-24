using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsRptPecasAcumulaInfo
    {
        public int id { get; set; }
        public String codigo { get; set; }
        public String nome { get; set; }
        public Decimal qtde { get; set; }
        public Decimal valorapurado { get; set; }
        public Decimal valorcusto { get; set; }
        public Decimal valorvenda { get; set; }
        public Decimal participacao { get; set; }
    }
}
