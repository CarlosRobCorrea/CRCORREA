using System;

namespace CRCorreaInfo
{
    public class clsRecebidaInfo
    {
        private Int32 _id;
        private Int32 _filial;
        private Int32 _duplicata;
        private Int32 _posicao;
        private Int32 _posicaofim;
        private DateTime _emissao;
        private Int32 _iddocumento;
        private String _setor;
        private Int32 _idcliente;
        private DateTime _datalanca;
        private String _emitente;
        private Int32 _idhistorico;
        private Int32 _idcentrocusto;
        private Int32 _idcodigoctabil;
        private Int32 _idnotafiscal;
        private Int32 _idrecebernfv;
        private Int32 _idreceber;
        private String _observa;
        private Int32 _idformapagto;
        private Int32 _idbanco;
        private Int32 _idbancoint;
        private String _boleto;
        private Decimal _boletonro;
        private String _dv;
        private String _baixa;
        private DateTime _vencimento;
        private Decimal _valor;
        private Decimal _valordesconto;
        private String _atevencimento;
        private Decimal _valorliquido;
        private Decimal _valorjuros;
        private Decimal _valormulta;
        private Int32 _idsitbanco;
        private String _contrato;
        private Decimal _comissao;
        private Decimal _valorpago;
        private Decimal _valorbaixando;
        private Decimal _valordevolvido;
        private Int32 _idcodigo01;
        private Int32 _idcodigo02;
        private Int32 _idcodigo03;
        private Int32 _idcodigo04;
        private String _imprimir;
        private String _transfebanco;
        private Int32 _transfenumero;
        private Int32 _idcoordenador;
        private Int32 _idsupervisor;
        private Int32 _idvendedor;
        private Decimal _valorcomissao;
        private Decimal _valorcomissaoger;
        private Decimal _valorcomissaosup;

        private String _xxxapagarabaixoxxx;
        private Int32 _cheque;
        private Decimal _diferenca;
        private Decimal _juros;
        private Decimal _cambio;
        private String _pagoem;
        private Decimal _cambiopg;
        private Int32 _garbanco;
        private Int32 _garcheque;
        private Decimal _garvalor;
        private String _moeda;
        private DateTime _datapago;
        private String _tipofornecedor;
        private Int32 _sitbanco;
        private String _sitdocumento;
        private String _recibo;
        private String _posicaoold;
        private String _vendedor;
        private String _documento;
        private String _cognome;
        private Int32 _nota;
        private Int32 _idnotadoc;
        private String _notadoc;
        private Decimal _valornota;
        private Int32 _parcelas;
        private String _despesa;
        private String _centrocusto;
        private String _codigo01;
        private String _codigo02;
        private String _codigo03;
        private String _codigo04;
        private Int32 _banco;
        private String _banconom;
        private Int32 _bancoint;
        private String _banconomint;
        private String _pagaraem;
        private Int32 _situacao;

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

        public Int32 duplicata
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

        public Int32 idcliente
        {
            get { return _idcliente; }
            set { _idcliente = value; }
        }
        public Int32 idcoordenador
        {
            get { return _idcoordenador; }
            set { _idcoordenador = value; }
        }
        public Int32 idsupervisor
        {
            get { return _idsupervisor; }
            set { _idsupervisor = value; }
        }
        public Int32 idvendedor
        {
            get { return _idvendedor; }
            set { _idvendedor = value; }
        }
        public Decimal valorcomissao
        {
            get { return _valorcomissao; }
            set { _valorcomissao = value; }
        }

        public Decimal valorcomissaoger
        {
            get { return _valorcomissaoger; }
            set { _valorcomissaoger = value; }
        }

        public Decimal valorcomissaosup
        {
            get { return _valorcomissaosup; }
            set { _valorcomissaosup = value; }
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

        public Int32 idrecebernfv
        {
            get { return _idrecebernfv; }
            set { _idrecebernfv = value; }
        }

        public Int32 idreceber
        {
            get { return _idreceber; }
            set { _idreceber = value; }
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

        public String contrato
        {
            get { return _contrato; }
            set { _contrato = value; }
        }


        public Decimal comissao
        {
            get { return _comissao; }
            set { _comissao = value; }
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

        public Int32 idcodigo01
        {
            get { return _idcodigo01; }
            set { _idcodigo01 = value; }
        }

        public Int32 idcodigo02
        {
            get { return _idcodigo02; }
            set { _idcodigo02 = value; }
        }

        public Int32 idcodigo03
        {
            get { return _idcodigo03; }
            set { _idcodigo03 = value; }
        }

        public Int32 idcodigo04
        {
            get { return _idcodigo04; }
            set { _idcodigo04 = value; }
        }

        public String imprimir
        {
            get { return _imprimir; }
            set { _imprimir = value; }
        }

        public String transfebanco
        {
            get { return _transfebanco; }
            set { _transfebanco = value; }
        }

        public Int32 transfenumero
        {
            get { return _transfenumero; }
            set { _transfenumero = value; }
        }

        public String xxxapagarabaixoxxx
        {
            get { return _xxxapagarabaixoxxx; }
            set { _xxxapagarabaixoxxx = value; }
        }

        public Int32 cheque
        {
            get { return _cheque; }
            set { _cheque = value; }
        }

        public Decimal diferenca
        {
            get { return _diferenca; }
            set { _diferenca = value; }
        }

        public Decimal juros
        {
            get { return _juros; }
            set { _juros = value; }
        }

        public Decimal cambio
        {
            get { return _cambio; }
            set { _cambio = value; }
        }

        public String pagoem
        {
            get { return _pagoem; }
            set { _pagoem = value; }
        }

        public Decimal cambiopg
        {
            get { return _cambiopg; }
            set { _cambiopg = value; }
        }

        public Int32 garbanco
        {
            get { return _garbanco; }
            set { _garbanco = value; }
        }

        public Int32 garcheque
        {
            get { return _garcheque; }
            set { _garcheque = value; }
        }

        public Decimal garvalor
        {
            get { return _garvalor; }
            set { _garvalor = value; }
        }

        public String moeda
        {
            get { return _moeda; }
            set { _moeda = value; }
        }

        public DateTime datapago
        {
            get { return _datapago; }
            set { _datapago = value; }
        }

        public String tipofornecedor
        {
            get { return _tipofornecedor; }
            set { _tipofornecedor = value; }
        }

        public Int32 sitbanco
        {
            get { return _sitbanco; }
            set { _sitbanco = value; }
        }

        public String sitdocumento
        {
            get { return _sitdocumento; }
            set { _sitdocumento = value; }
        }

        public String recibo
        {
            get { return _recibo; }
            set { _recibo = value; }
        }

        public String posicaoold
        {
            get { return _posicaoold; }
            set { _posicaoold = value; }
        }

        public String vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; }
        }

        public String documento
        {
            get { return _documento; }
            set { _documento = value; }
        }

        public String cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
        }

        public Int32 nota
        {
            get { return _nota; }
            set { _nota = value; }
        }

        public Int32 idnotadoc
        {
            get { return _idnotadoc; }
            set { _idnotadoc = value; }
        }

        public String notadoc
        {
            get { return _notadoc; }
            set { _notadoc = value; }
        }

        public Decimal valornota
        {
            get { return _valornota; }
            set { _valornota = value; }
        }

        public Int32 parcelas
        {
            get { return _parcelas; }
            set { _parcelas = value; }
        }

        public String despesa
        {
            get { return _despesa; }
            set { _despesa = value; }
        }

        public String centrocusto
        {
            get { return _centrocusto; }
            set { _centrocusto = value; }
        }

        public String codigo01
        {
            get { return _codigo01; }
            set { _codigo01 = value; }
        }

        public String codigo02
        {
            get { return _codigo02; }
            set { _codigo02 = value; }
        }

        public String codigo03
        {
            get { return _codigo03; }
            set { _codigo03 = value; }
        }

        public String codigo04
        {
            get { return _codigo04; }
            set { _codigo04 = value; }
        }

        public Int32 banco
        {
            get { return _banco; }
            set { _banco = value; }
        }

        public String banconom
        {
            get { return _banconom; }
            set { _banconom = value; }
        }

        public Int32 bancoint
        {
            get { return _bancoint; }
            set { _bancoint = value; }
        }

        public String banconomint
        {
            get { return _banconomint; }
            set { _banconomint = value; }
        }

        public String pagaraem
        {
            get { return _pagaraem; }
            set { _pagaraem = value; }
        }

        public Int32 situacao
        {
            get { return _situacao; }
            set { _situacao = value; }
        }

    }
}
