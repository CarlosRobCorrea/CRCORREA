using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsPecasCadastroInfo
    {
        public String aplicacao { get; set; }
        public String ativo { get; set; }
        public DateTime cadastrado { get; set; }
        public String codigo { get; set; }
        public String codigobarra { get; set; }
        public String codigofornecedor { get; set; }
        public String compraautomatica { get; set; }
        public Decimal comprarqtde { get; set; }
        public String comprarvia { get; set; }
        public String consumo { get; set; }
        public DateTime datacompra { get; set; }
        public DateTime datapreco { get; set; }
        public Int32 diasentrega { get; set; }
        public Int32 diasestoque { get; set; }
        public Decimal estoquemin { get; set; }
        public String estoqueminaut { get; set; }
        public Decimal fatorconv { get; set; }
        public Image foto { get; set; }
        public Int32 id { get; set; }
        public Int32 idclassifica { get; set; }
        public Int32 idclassifica1 { get; set; }
        public Int32 idclassifica2 { get; set; }
        public Int32 idcliente { get; set; }
        public Int32 idhistoricobco { get; set; }
        public Int32 idipi { get; set; }
        public Int32 idmarca { get; set; }
        public Int32 idsittriba { get; set; }
        public Int32 idsittribvenda { get; set; }
        public Int32 idunidade { get; set; }
        public Int32 idunidadecom { get; set; }
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

    }
}
