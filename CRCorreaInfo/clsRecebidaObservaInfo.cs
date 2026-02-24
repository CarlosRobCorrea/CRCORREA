using System;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsRecebidaObservaInfo
    {
        private Int32 _id;
        private Int32 _idduplicata;
        private SqlDateTime _data;
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

        public SqlDateTime data
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
