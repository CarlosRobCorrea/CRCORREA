using System;

namespace CRCorreaInfo
{
    public class clsEstadosInfo
    {
        private Int32 _id;
        private String _estado;
        private String _zonafranca;
        private Decimal _aliquota;
        private String _nomeext;
        private String _capital;
        private String _inicep;
        private String _fimcep;
        private String _regiao;
        private String _ibge;
        private String _iest;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public String zonafranca
        {
            get { return _zonafranca; }
            set { _zonafranca = value; }
        }

        public Decimal aliquota
        {
            get { return _aliquota; }
            set { _aliquota = value; }
        }

        public String nomeext
        {
            get { return _nomeext; }
            set { _nomeext = value; }
        }

        public String capital
        {
            get { return _capital; }
            set { _capital = value; }
        }

        public String inicep
        {
            get { return _inicep; }
            set { _inicep = value; }
        }

        public String fimcep
        {
            get { return _fimcep; }
            set { _fimcep = value; }
        }

        public String regiao
        {
            get { return _regiao; }
            set { _regiao = value; }
        }

        public String ibge
        {
            get { return _ibge; }
            set { _ibge = value; }
        }

        public String iest
        {
            get { return _iest; }
            set { _iest = value; }
        }


    }
}
