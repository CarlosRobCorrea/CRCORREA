using System;

namespace CRCorreaInfo
{
    public class clsCotacaoentregaInfo
    {
        private Int32 _id;
        private Int32 _idcotacao;
        private Int32 _idcotacao1;
        private Int32 _idcotacao2;
        private Decimal _qtdeentrega;
        private DateTime _dataentrega;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idcotacao
        {
            get { return _idcotacao; }
            set { _idcotacao = value; }
        }

        public Int32 idcotacao1
        {
            get { return _idcotacao1; }
            set { _idcotacao1 = value; }
        }

        public Int32 idcotacao2
        {
            get { return _idcotacao2; }
            set { _idcotacao2 = value; }
        }

        public Decimal qtdeentrega
        {
            get { return _qtdeentrega; }
            set { _qtdeentrega = value; }
        }

        public DateTime dataentrega
        {
            get { return _dataentrega; }
            set { _dataentrega = value; }
        }

    }
}
