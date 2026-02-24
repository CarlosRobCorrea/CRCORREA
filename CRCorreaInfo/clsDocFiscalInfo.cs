using System;

namespace CRCorreaInfo
{
    public class clsDocFiscalInfo
    {
        private Int32 _id;
        private String _codigo;
        private String _cognome;
        private Int32 _modelo;
        private String _nome;
        private String _serie;
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

        public String serie
        {
            get { return _serie; }
            set { _serie = value; }
        }

        public String ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public Int32 modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }

    }
}
