using System;

namespace CRCorreaInfo
{
    public class clsContacontabilInfo
    {
        private Int32 _id;
        private String _codigo;
        private String _nome;
        private String _tipo;
        private Int32 _reduzido;
        private Int32 _nivel;
        private String _verfabrica;
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

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public Int32 reduzido
        {
            get { return _reduzido; }
            set { _reduzido = value; }
        }

        public Int32 nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        public String verfabrica
        {
            get { return _verfabrica; }
            set { _verfabrica = value; }
        }

        public String ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

    }
}
