using System;

namespace CRCorreaInfo
{
    public class clsMaqctInfo
    {
        private Int32 _id;
        private String _codigo;
        private String _nome;
        private String _ativo;

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

        public String ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

    }
}
