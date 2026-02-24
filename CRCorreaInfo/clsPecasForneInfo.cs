using System;

namespace CRCorreaInfo
{
    public class clsPecasForneInfo
    {
        private Int32 _id;
        private Int32 _idcodigo;
        private Int32 _idfornecedor;
        private String _codigofornecedor;
        private String _descricaofornecedor;
        private Decimal _precouni;
        private Decimal _icms;
        private Int32 _idunidade;
        private Decimal _fatorconv;
        private DateTime _database1;
        private Int32 _dias;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
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
        public String codigofornecedor
        {
            get { return _codigofornecedor; }
            set { _codigofornecedor = value; }
        }

        public String descricaofornecedor
        {
            get { return _descricaofornecedor; }
            set { _descricaofornecedor = value; }
        }

        public Decimal precouni
        {
            get { return _precouni; }
            set { _precouni = value; }
        }
        public Decimal icms
        {
            get { return _icms; }
            set { _icms = value; }
        }

        public Int32 idunidade
        {
            get { return _idunidade; }
            set { _idunidade = value; }
        }
        public Decimal fatorconv
        {
            get { return _fatorconv; }
            set { _fatorconv = value; }
        }
        public DateTime database1
        {
            get { return _database1; }
            set { _database1 = value; }
        }
        public Int32 dias
        {
            get { return _dias; }
            set { _dias = value; }
        }


    }
}
