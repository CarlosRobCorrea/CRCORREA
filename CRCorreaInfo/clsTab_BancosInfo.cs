using System;

namespace CRCorreaInfo
{
    public class clsTab_BancosInfo
    {
        private Int32 _id;
        private Int32 _codigo;
        private String _cognome;
        private String _nome;
        private String _ativo;
        private String _homepage;
        private String _cogcnab;

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

        public String cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
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
        public String homepage
        {
            get { return _homepage; }
            set { _homepage = value; }
        }

        public String cogcnab
        {
            get { return _cogcnab; }
            set { _cogcnab = value; }
        }

    }
}
