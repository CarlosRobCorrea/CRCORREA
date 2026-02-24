using System;

namespace CRCorreaInfo
{
    public class clsEstadosicmsInfo
    {
        private Int32 _id;
        private Int32 _idestado;
        private Int32 _idestadodestino;
        private Decimal _aliquota;
        private Decimal _iva;
        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idestado
        {
            get { return _idestado; }
            set { _idestado = value; }
        }

        public Int32 idestadodestino
        {
            get { return _idestadodestino; }
            set { _idestadodestino = value; }
        }

        public Decimal aliquota
        {
            get { return _aliquota; }
            set { _aliquota = value; }
        }
        public Decimal iva
        {
            get { return _iva; }
            set { _iva = value; }
        }

    }
}
