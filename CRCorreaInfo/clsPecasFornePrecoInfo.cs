using System;

namespace CRCorreaInfo
{
    public class clsPecasFornePrecoInfo
    {
        private Int32 _id;
        private Decimal _baseicms;
        private Int32 _idcodigo;
        private Int32 _idfornecedor;
        private Int32 _idunidade;
        private DateTime _data;
        private Decimal _valorcusto;
        private Decimal _valorvenda;
        private Int32 _sequencia;
        private Decimal _icms;
        private Decimal _piscofins;
        private String _tipo;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Decimal baseicms
        {
            get { return _baseicms; }
            set { _baseicms = value; }
        }

        public Int32 idcodigo
        {
            get { return _idcodigo; }
            set { _idcodigo = value; }
        }
        public Int32 idfornecedor
        {
            get { return _idfornecedor; }
            set { _idfornecedor = value; }
        }
        public Int32 idunidade
        {
            get { return _idunidade; }
            set { _idunidade = value; }
        }

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }
        public Decimal valorcusto
        {
            get { return _valorcusto; }
            set { _valorcusto = value; }
        }
        public Decimal valorvenda
        {
            get { return _valorvenda; }
            set { _valorvenda = value; }
        }
        public Int32 sequencia
        {
            get { return _sequencia; }
            set { _sequencia = value; }
        }
        public Decimal icms
        {
            get { return _icms; }
            set { _icms = value; }
        }
        public Decimal piscofins
        {
            get { return _piscofins; }
            set { _piscofins = value; }
        }
        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

    }
}
