using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsPedidoReceberInfo
    {
        public Decimal boletonro { get; set; }
        public String caixa { get; set; }
        public DateTime data { get; set; }
        public String dv { get; set; }
        public Int32 id { get; set; }
        public Int32 idnota { get; set; }
        public Int32 idtipopaga { get; set; }
        public String pagou { get; set; }
        public Int32 posicao { get; set; }
        public Int32 posicaofim { get; set; }
        public String tipopaga { get; set; }
        public Decimal valor { get; set; }
        public Decimal valorcomissao { get; set; }
        public Decimal valorcomissaoger { get; set; }
        public Decimal valorcomissaosup { get; set; }
        

    }
}
