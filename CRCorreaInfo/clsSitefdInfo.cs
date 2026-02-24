using System;

namespace  CRCorreaInfo
{
    public class clsSitefdInfo
    {
        private Int32 _id;
        private String _codigo;
        private String _nome;
        private String _natureza;
        private String _detalhamento;
        private String _sobre;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public String nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public String natureza
        {
            get { return _natureza; }
            set { _natureza = value; }
        }
        public String detalhamento
        {
            get { return _detalhamento; }
            set { _detalhamento = value; }
        }
        public String sobre
        {
            get { return _sobre; }
            set { _sobre = value; }
        }
    }
}
