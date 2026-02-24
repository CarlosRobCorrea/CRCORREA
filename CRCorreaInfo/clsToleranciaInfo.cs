using System;

namespace CRCorreaInfo
{
    public class clsToleranciaInfo
    {
        private Int32 _id;
        private String _codigo;
        private String _nome;
        private String _norma;
        private String _ativo;
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
        public String norma
        {
            get { return _norma; }
            set { _norma = value; }
        }

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public String ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

    }
}
