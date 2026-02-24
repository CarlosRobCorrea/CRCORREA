using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace CRCorreaInfo
{
    public class clsXmlNFCompra1Info
    {
        public Int32 id { get; set; }
        public Int32 numero { get; set; }
        public String aplicacao { get; set; }
        public String codigo { get; set; }
        public String codigobarra { get; set; }
        public String codigofornecedor { get; set; }
        public String descricao { get; set; }
        public String descricaocadastro { get; set; }
        public Decimal fator { get; set; }
        public String fatura { get; set; }
        public Image foto { get; set; }
        public Int32 idclassifica { get; set; }
        public Int32 idcodigo { get; set; }
        public Int32 idipi { get; set; }
        public Int32 idmarca { get; set; }
        public Int32 idunid { get; set; }
        public String unid { get; set; }
        public String ncm { get; set; }
        public Decimal qtde { get; set; }
        public Decimal preco { get; set; }
        public Int32 status { get; set; }
        public Decimal totalmercado { get; set; }
        public Decimal totalnfitem { get; set; }
        public Decimal valoricm { get; set; }
        public Decimal valoripi { get; set; }
        public Decimal precovenda { get; set; }
        public Decimal precovendaanterior { get; set; }
    }
}
