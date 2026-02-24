using System;

namespace CRCorreaInfo
{
    public class clsSolicitacaoFornecedorInfo
    {
        private Int32 _id;
        private Int32 _idsolicitacao;
        private Int32 _idfornecedor;
        private String _preferencia;
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
        public Int32 idfornecedor
        {
            get { return _idfornecedor; }
            set { _idfornecedor = value; }
        }
        public String preferencia
        {
            get { return _preferencia; }
            set { _preferencia = value; }
        }

    }
}
