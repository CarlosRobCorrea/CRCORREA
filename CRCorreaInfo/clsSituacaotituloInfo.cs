using System;

namespace CRCorreaInfo
{ 
    public class clsSituacaotituloInfo
    {
        private Int32 _id;
        private Int32 _codigo;
        private String _nome;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public String nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

    }
}
