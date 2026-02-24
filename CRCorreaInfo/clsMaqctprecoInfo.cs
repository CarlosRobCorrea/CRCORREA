using System;

namespace CRCorreaInfo
{
    public class clsMaqctprecoInfo
    {
        private Int32 _id;
        private Int32 _idmaqct;
        private Int32 _idindice;
        private DateTime _data;
        private Decimal _valor;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idmaqct
        {
            get { return _idmaqct; }
            set { _idmaqct = value; }
        }

        public Int32 idindice
        {
            get { return _idindice; }
            set { _idindice = value; }
        }

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }

        public Decimal valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

    }
}
