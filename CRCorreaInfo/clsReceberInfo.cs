using System;

namespace CRCorreaInfo
{
    public class clsReceberInfo
    {

        private Int32 _id;
        private String _atevencimento;
        private String _baixa;
        private String _boleto;
        private Decimal _boletonro;
        private String _chegou;
        private String _contrato;
        private String _despesapublica;
        private DateTime _datalanca;
        private Int64 _duplicata;
        private String _dv;
        private DateTime _emissao;
        private String _emitente;
        private Int32 _filial;
        private Int32 _idbanco;
        private Int32 _idbancoint;
        private Int32 _idcentrocusto;
        private Int32 _idcliente;
        private Int32 _idcodigo01;
        private Int32 _idcodigo02;
        private Int32 _idcodigo03;
        private Int32 _idcodigo04;
        private Int32 _idcodigoctabil;
        private Int32 _idcoordenador;
        private Int32 _iddespesa;
        private Int32 _iddocumento;
        private Int32 _idformapagto;
        private Int32 _idhistorico;
        private String _imprimir;
        private Int32 _idnotafiscal;
        private Int32 _idrecebernfv;
        private Int32 _idsitbanco;
        private Int32 _idsupervisor;
        private Int32 _idvendedor;

        private String _observa;
        private Int32 _posicao;
        private Int32 _posicaofim;
        private String _setor;
        private String _transfebanco;
        private Int32 _transfenumero;
        private Decimal _valor;
        private Decimal _valorbaixando;
        private Decimal _valorcomissao;
        private Decimal _valorcomissaoger;
        private Decimal _valorcomissaosup;
        private Decimal _valordesconto;
        private Decimal _valordevolvido;
        private Decimal _valorjuros;
        private Decimal _valorliquido;
        private Decimal _valormulta;
        private Decimal _valorpago;
        private DateTime _vencimento;
        private DateTime _vencimentoprev;

        private string _html;
        private int _numerocontroleboleto;
        private string _arquivogerado;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String atevencimento
        {
            get { return _atevencimento; }
            set { _atevencimento = value; }
        }
        public String baixa
        {
            get { return _baixa; }
            set { _baixa = value; }
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
        public String chegou
        {
            get { return _chegou; }
            set { _chegou = value; }
        }
        public String contrato
        {
            get { return _contrato; }
            set { _contrato = value; }
        }
        public DateTime datalanca
        {
            get { return _datalanca; }
            set { _datalanca = value; }
        }
        public String despesapublica
        {
            get { return _despesapublica; }
            set { _despesapublica = value; }
        }
        public Int64 duplicata
        {
            get { return _duplicata; }
            set { _duplicata = value; }
        }
        public String dv
        {
            get { return _dv; }
            set { _dv = value; }
        }
        public DateTime emissao
        {
            get { return _emissao; }
            set { _emissao = value; }
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
        public Int32 idcentrocusto
        {
            get { return _idcentrocusto; }
            set { _idcentrocusto = value; }
        }
        public Int32 idcliente
        {
            get { return _idcliente; }
            set { _idcliente = value; }
        }
        public Int32 idcodigoctabil
        {
            get { return _idcodigoctabil; }
            set { _idcodigoctabil = value; }
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
        public Int32 idcoordenador
        {
            get { return _idcoordenador; }
            set { _idcoordenador = value; }
        }

        public Int32 iddespesa
        {
            get { return _iddespesa; }
            set { _iddespesa = value; }
        }
        public Int32 iddocumento
        {
            get { return _iddocumento; }
            set { _iddocumento = value; }
        }
        public Int32 idformapagto
        {
            get { return _idformapagto; }
            set { _idformapagto = value; }
        }
        public Int32 idhistorico
        {
            get { return _idhistorico; }
            set { _idhistorico = value; }
        }
        public String imprimir
        {
            get { return _imprimir; }
            set { _imprimir = value; }
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
        public Int32 idsitbanco
        {
            get { return _idsitbanco; }
            set { _idsitbanco = value; }
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
        public String observa
        {
            get { return _observa; }
            set { _observa = value; }
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
        public String setor
        {
            get { return _setor; }
            set { _setor = value; }
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

        public Decimal valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        public Decimal valorbaixando
        {
            get { return _valorbaixando; }
            set { _valorbaixando = value; }
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
        public Decimal valordesconto
        {
            get { return _valordesconto; }
            set { _valordesconto = value; }
        }
        public Decimal valordevolvido
        {
            get { return _valordevolvido; }
            set { _valordevolvido = value; }
        }
        public Decimal valorjuros
        {
            get { return _valorjuros; }
            set { _valorjuros = value; }
        }
        public Decimal valorliquido
        {
            get { return _valorliquido; }
            set { _valorliquido = value; }
        }
        public Decimal valormulta
        {
            get { return _valormulta; }
            set { _valormulta = value; }
        }
        public Decimal valorpago
        {
            get { return _valorpago; }
            set { _valorpago = value; }
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

        public string Html
        {
            get { return _html; }
            set { _html = value; }
        }

        public int numerocontroleboleto
        {
            get { return _numerocontroleboleto; }
            set { _numerocontroleboleto = value; }
        }

        public string arquivogerado
        {
            get { return _arquivogerado; }
            set { _arquivogerado = value; }
        }
    }
}
