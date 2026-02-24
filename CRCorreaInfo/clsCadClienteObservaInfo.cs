using System;

namespace CRCorreaInfo
{
    public class clsCadClienteObservaInfo
    {
        Int32 _id;
        Int32 _idcliente;
        Int32 _idreferencia;
        String _ligacao;
        DateTime _data;
        String _observar;
        String _emitente;
        String _fechada;
        Int32 _idvendedor;
        DateTime _dataagenda;
        String _observaragenda;
        String _contatado;
        String _contatoagenda;
        String _contatofim;
        DateTime _datafim;
        Int32 _idemitente;

        public String documento { get; set; }

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idcliente
        {
            get { return _idcliente; }
            set { _idcliente = value; }
        }
        public Int32 idreferencia
        {
            get { return _idreferencia; }
            set { _idreferencia = value; }
        }
        public String ligacao
        {
            get { return _ligacao; }
            set { _ligacao = value; }
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
        public String fechada
        {
            get { return _fechada; }
            set { _fechada = value; }
        }
        public Int32 idvendedor
        {
            get { return _idvendedor; }
            set { _idvendedor = value; }
        }
        public DateTime dataagenda
        {
            get { return _dataagenda; }
            set { _dataagenda = value; }
        }
        public String observaragenda
        {
            get { return _observaragenda; }
            set { _observaragenda = value; }
        }
        public String contatado
        {
            get { return _contatado; }
            set { _contatado = value; }
        }
        public String contatoagenda
        {
            get { return _contatoagenda; }
            set { _contatoagenda = value; }
        }
        public String contatofim
        {
            get { return _contatofim; }
            set { _contatofim = value; }
        }
        public DateTime datafim
        {
            get { return _datafim; }
            set { _datafim = value; }
        }
        public Int32 idemitente
        {
            get { return _idemitente; }
            set { _idemitente = value; }
        }
    }
}
