using System;

namespace CRCorreaInfo
{
    public class clsPecasFamiliaInfo
    {
        private Int32 _id;
        private String _codigo;
        private String _nome;
        private Int32 _idunidade;
        private String _tipo;

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
        public Int32 idunidade
        {
            get { return _idunidade; }
            set { _idunidade = value; }
        }
        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

    }
}
