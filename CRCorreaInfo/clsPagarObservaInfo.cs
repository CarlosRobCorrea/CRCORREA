using System;

namespace CRCorreaInfo
{
    public class clsPagarObservaInfo
    {
        private Int32 _id;
        private Int32 _idduplicata;
        private DateTime _data;
        private String _observar;
        private String _emitente;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idduplicata
        {
            get { return _idduplicata; }
            set { _idduplicata = value; }
        }

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }

        public String observar
        {
            get { return _observar; }
            set { _observar = value; }
        }

        public String emitente
        {
            get { return _emitente; }
            set { _emitente = value; }
        }

    }
}
