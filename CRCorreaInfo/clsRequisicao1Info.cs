using System;

namespace CRCorreaInfo
{
    public class clsRequisicao1Info
    {
        private Int32 _id;

        private DateTime _dataentrega;
        private DateTime _dataretorno;
        private String _funcionario;
        private Int32 _idcodigo;
        private Int32 _idfuncionario;
        private Int32 _idmaquina;
        private Int32 _idordemservico;
        private Int32 _idordemservicoop;
        private Int32 _idunidade;
        private String _motivo;
        private Int32 _numero;
        private String _observar;
        private Decimal _qtde;
        private Decimal _qtdedev;
        private Decimal _qtdeentrega;
        private Decimal _qtdesaldo;
        private String _tipo;
        private Decimal _valor;

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

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public String motivo
        {
            get { return _motivo; }
            set { _motivo = value; }
        }

        public Int32 idcodigo
        {
            get { return _idcodigo; }
            set { _idcodigo = value; }
        }

        public Int32 idmaquina
        {
            get { return _idmaquina; }
            set { _idmaquina = value; }
        }

        public Int32 idfuncionario
        {
            get { return _idfuncionario; }
            set { _idfuncionario = value; }
        }

        public Int32 idordemservico
        {
            get { return _idordemservico; }
            set { _idordemservico = value; }
        }
        public Int32 idordemservicoop
        {
            get { return _idordemservicoop; }
            set { _idordemservicoop = value; }
        }
        


        public Int32 idunidade
        {
            get { return _idunidade; }
            set { _idunidade = value; }
        }
        public Decimal qtde
        {
            get { return _qtde; }
            set { _qtde = value; }
        }

        public Decimal qtdedev
        {
            get { return _qtdedev; }
            set { _qtdedev = value; }
        }
        public Decimal qtdeentrega
        {
            get { return _qtdeentrega; }
            set { _qtdeentrega = value; }
        }

        public Decimal qtdesaldo
        {
            get { return _qtdesaldo; }
            set { _qtdesaldo = value; }
        }
        public Decimal valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public String observar
        {
            get { return _observar; }
            set { _observar = value; }
        }

        public DateTime dataentrega
        {
            get { return _dataentrega; }
            set { _dataentrega = value; }
        }

        public DateTime dataretorno
        {
            get { return _dataretorno; }
            set { _dataretorno = value; }
        }
        public String funcionario
        {
            get { return _funcionario; }
            set { _funcionario = value; }
        }

    }
}
