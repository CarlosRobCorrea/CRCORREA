using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsOrcamento1Info
    {
        public Int32 id { get; set; }
        public String descricao1 { get; set; }
        public String descricao2 { get; set; }
        public String descricao3 { get; set; }
        public String descricao4 { get; set; }
        public Decimal desconto { get; set; }
        public Image foto { get; set; }
        public Int32 idcodigo { get; set; }
        public Int32 idorcamento { get; set; }
        public Int32 idordemservico { get; set; }
        public Int32 idordemservicoitem { get; set; }
        public Int32 idpedido { get; set; }
        public Int32 idpedidoitem { get; set; }
        public Int32 item { get; set; }
        public String motivo { get; set; }
        public Decimal peso { get; set; }
        public Decimal preco { get; set; }
        public Decimal precoconfirmado { get; set; }
        public Decimal precoliquido { get; set; }
        public Decimal qtde { get; set; }
        public Decimal qtdeconfirmadaitem { get; set; }
        public String referencia1 { get; set; }
        public String referencia2 { get; set; }
        public String situacao { get; set; }
        public String unid { get; set; }
        public Decimal totalconfirmadoitem { get; set; }
        public Decimal totalmercadoria { get; set; }
        public Decimal totalpeso { get; set; }
        public Decimal valordesconto { get; set; }
    }
}
