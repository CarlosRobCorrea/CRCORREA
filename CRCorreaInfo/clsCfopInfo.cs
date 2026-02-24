using System;

namespace CRCorreaInfo
{
    public class clsCfopInfo
    {
        private Int32 _id;
        private String _cfop;
        private String _nomenota;
        private String _dizer;
        private String _tipo;
        private String _ativo;
        private String _cfopunico;
        private Int32 _idcontadeb;
        private Int32 _idcontacre;
        private String _combustivel;

        public String combustivel
        {
            get { return _combustivel; }
            set { _combustivel = value; }
        }

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String cfop
        {
            get { return _cfop; }
            set { _cfop = value; }
        }

        public String nomenota
        {
            get { return _nomenota; }
            set { _nomenota = value; }
        }

        public String dizer
        {
            get { return _dizer; }
            set { _dizer = value; }
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

        public String cfopunico
        {
            get { return _cfopunico; }
            set { _cfopunico = value; }
        }

        public Int32 idcontadeb
        {
            get { return _idcontadeb; }
            set { _idcontadeb = value; }
        }

        public Int32 idcontacre
        {
            get { return _idcontacre; }
            set { _idcontacre = value; }
        }
    }
}
