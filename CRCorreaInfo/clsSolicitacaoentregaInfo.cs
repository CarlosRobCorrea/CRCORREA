using System;

namespace CRCorreaInfo
{
    public class clsSolicitacaoentregaInfo
    {
        private Int32 _id;
        private Int32 _idsolicitacao;
        private Decimal _qtdeentrega;
        private DateTime _dataentrega;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idsolicitacao
        {
            get { return _idsolicitacao; }
            set { _idsolicitacao = value; }
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
