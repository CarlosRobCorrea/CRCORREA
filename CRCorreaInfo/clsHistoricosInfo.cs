using System;


namespace CRCorreaInfo
{
    public class clsHistoricosInfo
    {
        private Int32 _id;
        private String _codigo;
        //private String _descricao;
        private Int32 _idcontabil;
        private String _contabil;
        private String _nivel;
        private String _tipo;
        private Int32 _idcobranca;
        private String _cobranca;
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

        public Int32 idcontabil
        {
            get { return _idcontabil; }
            set { _idcontabil = value; }
        }

        public String contabil
        {
            get { return _contabil; }
            set { _contabil = value; }
        }

        public String nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public Int32 idcobranca
        {
            get { return _idcobranca; }
            set { _idcobranca = value; }
        }

        public String cobranca
        {
            get { return _cobranca; }
            set { _cobranca = value; }
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
