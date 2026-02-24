using System;

namespace CRCorreaInfo
{
    public class clsPagarInfo
    {
        private Int32 _id;
        private String _atevencimento;
        private String _baixa;
        private String _boleto;
        private Decimal _boletonro;
        private String _chegou;
        private DateTime _datalanca;
        private String _despesapublica;
        private Int64 _duplicata;
        private String _dv;
        private DateTime _emissao;
        private String _emitente;
        private Int32 _filial;
        private Int32 _idbanco;
        private Int32 _idbancoint;
        private Int32 _idcentrocusto;
        private Int32 _idcodigoctabil;
        private Int32 _iddocumento;
        private Int32 _idformapagto;
        private Int32 _idfornecedor;
        private Int32 _idhistorico;
        private Int32 _idnotafiscal;
        private Int32 _idpagarnfe;
        private Int32 _idsitbanco;
        private String _imprimir;
        private String _observa;
        private Int32 _posicao;
        private Int32 _posicaofim;
        private String _setor;
        private Decimal _valor;
        private Decimal _valorbaixando;
        private Decimal _valordesconto;
        private Decimal _valordevolvido;
        private Decimal _valorliquido;
        private Decimal _valorjuros;
        private Decimal _valormulta;
        private Decimal _valorpago;
        private DateTime _vencimento;
        private DateTime _vencimentoprev;
        
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

        public Int64 duplicata
        {
            get { return _duplicata; }
            set { _duplicata = value; }
        }

        public Int32 posicao
        {
            get { return _posicao; }
            set { _posicao = value; }
        }

        public Int32 posicaofim
        {
            get { return _posicaofim; }
            set { _posicaofim = value; }
        }

        public DateTime emissao
        {
            get { return _emissao; }
            set { _emissao = value; }
        }

        public Int32 iddocumento
        {
            get { return _iddocumento; }
            set { _iddocumento = value; }
        }

        public String setor
        {
            get { return _setor; }
            set { _setor = value; }
        }

        public Int32 idfornecedor
        {
            get { return _idfornecedor; }
            set { _idfornecedor = value; }
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

        public Int32 idnotafiscal
        {
            get { return _idnotafiscal; }
            set { _idnotafiscal = value; }
        }

        public Int32 idpagarnfe
        {
            get { return _idpagarnfe; }
            set { _idpagarnfe = value; }
        }

        public String observa
        {
            get { return _observa; }
            set { _observa = value; }
        }

        public Int32 idformapagto
        {
            get { return _idformapagto; }
            set { _idformapagto = value; }
        }

        public Int32 idbanco
        {
            get { return _idbanco; }
            set { _idbanco = value; }
        }

        public Int32 idbancoint
        {
            get { return _idbancoint; }
            set { _idbancoint = value; }
        }

        public String chegou
        {
            get { return _chegou; }
            set { _chegou = value; }
        }

        public String despesapublica
        {
            get { return _despesapublica; }
            set { _despesapublica = value; }
        }

        public String boleto
        {
            get { return _boleto; }
            set { _boleto = value; }
        }

        public Decimal boletonro
        {
            get { return _boletonro; }
            set { _boletonro = value; }
        }

        public String dv
        {
            get { return _dv; }
            set { _dv = value; }
        }

        public String baixa
        {
            get { return _baixa; }
            set { _baixa = value; }
        }

        public DateTime vencimento
        {
            get { return _vencimento; }
            set { _vencimento = value; }
        }

        public DateTime vencimentoprev
        {
            get { return _vencimentoprev; }
            set { _vencimentoprev = value; }
        }

        public Decimal valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public Decimal valordesconto
        {
            get { return _valordesconto; }
            set { _valordesconto = value; }
        }

        public String atevencimento
        {
            get { return _atevencimento; }
            set { _atevencimento = value; }
        }

        public Decimal valorliquido
        {
            get { return _valorliquido; }
            set { _valorliquido = value; }
        }

        public Decimal valorjuros
        {
            get { return _valorjuros; }
            set { _valorjuros = value; }
        }

        public Decimal valormulta
        {
            get { return _valormulta; }
            set { _valormulta = value; }
        }

        public Int32 idsitbanco
        {
            get { return _idsitbanco; }
            set { _idsitbanco = value; }
        }

        public Decimal valorpago
        {
            get { return _valorpago; }
            set { _valorpago = value; }
        }

        public Decimal valorbaixando
        {
            get { return _valorbaixando; }
            set { _valorbaixando = value; }
        }

        public Decimal valordevolvido
        {
            get { return _valordevolvido; }
            set { _valordevolvido = value; }
        }

        public String imprimir
        {
            get { return _imprimir; }
            set { _imprimir = value; }
        }        
    }
}
