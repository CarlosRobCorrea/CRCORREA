using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CRCorreaInfo
{
    public class clsRptPecasInfo
    {
        public String ativo { get; set; }
        public DateTime cadastrado { get; set; }
        public String codigo { get; set; }
        public String codigobarra { get; set; }
        public String codigoipi { get; set; }
        public String compraautomatica { get; set; }
        public Decimal comprarqtde { get; set; }
        public String comprarvia { get; set; }
        //public String consumo { get; set; }
        public DateTime datacompra { get; set; }
        public DateTime datapreco { get; set; }
        public Int32 diasentrega { get; set; }
        public Int32 diasestoque { get; set; }
        public Decimal estoquemin { get; set; }
        public String estoqueminaut { get; set; }
        public Decimal fatorconv { get; set; }
        public Image foto { get; set; }
        public Int32 id { get; set; }
        public String grupo { get; set; }
        public String subgrupo1 { get; set; }
        public String subgrupo2 { get; set; }
        public String fabricante { get; set; }
        public String unidade { get; set; }
        public String locacao { get; set; }
        public String nome { get; set; }
        public Decimal pesobruto { get; set; }
        public Decimal pesounit { get; set; }
        public Decimal precocompra { get; set; }
        public Decimal precovenda { get; set; }
        public Decimal qtdeentra { get; set; }
        public Decimal qtdeinicio { get; set; }
        public Decimal qtdesaida { get; set; }
        public Decimal qtdesaldo { get; set; }
        public String tipoproduto { get; set; }

    }
}
