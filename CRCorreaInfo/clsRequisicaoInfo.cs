using System;

namespace CRCorreaInfo
{
    public class clsRequisicaoInfo
    {
        private Int32 _id;
        private Int32 _ano;
        private DateTime _data;
        private String _emitente;
        private Int32 _filial;
        private Int32 _numero;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int32 numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public Int32 ano
        {
            get { return _ano; }
            set { _ano = value; }
        }
        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }
        public String emitente
        {
            get { return _emitente; }
            set { _emitente = value; }
        }
        public Int32 filial
        {
            get { return _filial; }
            set { _filial = value; }
        }

    }
}
