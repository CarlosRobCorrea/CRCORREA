using System;

namespace CRCorreaInfo
{
    public class clsNFCompraResumidaInfo
    {
        private Int32 _id;
        private Int32 _filial;
        private Int32 _numero;
        private DateTime _data;
        private DateTime _datalanca;
        private String _emitente;
        private Int32 _iddocumento;
        private Int32 _idfornecedor;
        private Int32 _idcodigo;
        private String _complemento;
        private Decimal _totalnota;
        private Int32 _idbancoint;
        private Int32 _idhistorico;
        private Int32 _idcentrocusto;
        private Int32 _idcodigoctabil;
        private Int32 _idfluxo;
        private Int32 _idfluxo01;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int32 filial
        {
            get { return _filial; }
            set { _filial = value; }
        }
        public Int32 numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }
        public DateTime datalanca
        {
            get { return _datalanca; }
            set { _datalanca = value; }
        }
        public String emitente
        {
            get { return _emitente; }
            set { _emitente = value; }
        }
        public Int32 iddocumento
        {
            get { return _iddocumento; }
            set { _iddocumento = value; }
        }
        public Int32 idfornecedor
        {
            get { return _idfornecedor; }
            set { _idfornecedor = value; }
        }
        public Int32 idcodigo
        {
            get { return _idcodigo; }
            set { _idcodigo = value; }
        }
        public String complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }
        public Decimal totalnota
        {
            get { return _totalnota; }
            set { _totalnota = value; }
        }
        public Int32 idbancoint
        {
            get { return _idbancoint; }
            set { _idbancoint = value; }
        }
        public Int32 idhistorico
        {
            get { return _idhistorico; }
            set { _idhistorico = value; }
        }
        public Int32 idcentrocusto
        {
            get { return _idcentrocusto; }
            set { _idcentrocusto = value; }
        }
        public Int32 idcodigoctabil
        {
            get { return _idcodigoctabil; }
            set { _idcodigoctabil = value; }
        }
        public Int32 idfluxo
        {
            get { return _idfluxo; }
            set { _idfluxo = value; }
        }
        public Int32 idfluxo01
        {
            get { return _idfluxo01; }
            set { _idfluxo01 = value; }
        }
    }
}
