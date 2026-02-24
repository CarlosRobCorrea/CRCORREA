using System;

namespace CRCorreaInfo
{
    public class clsPecasObservaInfo
    {
        private Int32 _id;
        private DateTime _data;
        private Int32  _numero;
        private Int32 _idemitente;
        private String _emitente;
        private Int32 _idpeca;
        private String _observa;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }

        public Int32 numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public Int32 idemitente
        {
            get { return _idemitente; }
            set { _idemitente = value; }
        }

        public String emitente
        {
            get { return _emitente; }
            set { _emitente = value; }
        }
        public Int32 idpeca
        {
            get { return _idpeca; }
            set { _idpeca = value; }
        }
        public String observa
        {
            get { return _observa; }
            set { _observa = value; }
        }
    }
}
