using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsComprasInfo
    {
        private Int32 _id;
        private Int32 _ano;
        private String _contato;
        private Decimal _cofins;
        private Decimal _cofins1;
        private Decimal _csll;
        private DateTime _data;
        private Int32 _filial;
        private String _frete;
        private String _fretepaga;
        private Int32 _idautorizante;
        private Int32 _idcomprador;
        private Int32 _idcondpagto;
        private Int32 _idcontato;
        private Int32 _idemitente;
        private Int32 _idformapagto;
        private Int32 _idfornecedor;
        private Int32 _idtransportadora;
        private Decimal _inss;
        private Decimal _irrf;
        private Decimal _iss;
        private Int32 _numero;
        private String _observa;
        private Decimal _pis;
        private Decimal _pispasep;
        private Decimal _piscofinscsll;
        private Decimal _qtdeentregue;
        private Decimal _qtdedefeito;
        private Decimal _qtdesucata;
        private Decimal _qtdebaixada;
        private Decimal _qtdeosaux;
        private Decimal _qtdesaldo;
        private String _setor;
        private Decimal _setorfator;
        private String _situacao;
        private String _termino;
        private String _tipofrete;
        private Decimal _totalbaseicm;
        private Decimal _totalbaseicmsubst;
        private Decimal _totalfrete;
        private Decimal _totalicm;
        private Decimal _totalicmsubst;
        private Decimal _totalipi;
        private Decimal _totalmercadoria;
        private Decimal _totaloutras;
        private Decimal _totalpeca;
        private Decimal _totalpecaentra;
        private Decimal _totalpecatransfe;
        private Decimal _totalpedido;
        private Decimal _totalpeso;
        private Decimal _totalprevisto;
        private Decimal _totalseguro;
        private String _transporte;



        public Int32 id 
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int32 ano
        {
            get { return _ano; }
            set { _ano = value; }
        }
        public Decimal cofins
        {
            get { return _cofins; }
            set { _cofins = value; }
        }
        public Decimal cofins1
        {
            get { return _cofins1; }
            set { _cofins1 = value; }
        }
        public String contato
        {
            get { return _contato; }
            set { _contato = value; }
        }

        public Decimal csll
        {
            get { return _csll; }
            set { _csll = value; }
        }
        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }
        public Int32 filial
        {
            get { return _filial; }
            set { _filial = value; }
        }
        public String frete
        {
            get { return _frete; }
            set { _frete = value; }
        }
        public String fretepaga
        {
            get { return _fretepaga; }
            set { _fretepaga = value; }
        }
        public Int32 idautorizante
        {
            get { return _idautorizante; }
            set { _idautorizante = value; }
        }
        public Int32 idcomprador
        {
            get { return _idcomprador; }
            set { _idcomprador = value; }
        }
        public Int32 idcondpagto
        {
            get { return _idcondpagto; }
            set { _idcondpagto = value; }
        }
        public Int32 idcontato
        {
            get { return _idcontato; }
            set { _idcontato = value; }
        }

        public Int32 idemitente
        {
            get { return _idemitente; }
            set { _idemitente = value; }
        }
        public Int32 idformapagto
        {
            get { return _idformapagto; }
            set { _idformapagto = value; }
        }

        public Int32 idfornecedor
        {
            get { return _idfornecedor; }
            set { _idfornecedor = value; }
        }
        public Int32 idtransportadora
        {
            get { return _idtransportadora; }
            set { _idtransportadora = value; }
        }
        public Decimal inss
        {
            get { return _inss; }
            set { _inss = value; }
        }
        public Decimal irrf
        {
            get { return _irrf; }
            set { _irrf = value; }
        }
        public Decimal iss
        {
            get { return _iss; }
            set { _iss = value; }
        }

        public Int32 numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public String observa
        {
            get { return _observa; }
            set { _observa = value; }
        }

        public Decimal pis
        {
            get { return _pis; }
            set { _pis = value; }
        }
        public Decimal pispasep
        {
            get { return _pispasep; }
            set { _pispasep = value; }
        }

        public Decimal piscofinscsll
        {
            get { return _piscofinscsll; }
            set { _piscofinscsll = value; }
        }
        public Decimal qtdebaixada
        {
            get { return _qtdebaixada; }
            set { _qtdebaixada = value; }
        }
        public Decimal qtdedefeito
        {
            get { return _qtdedefeito; }
            set { _qtdedefeito = value; }
        }
        public Decimal qtdeentregue
        {
            get { return _qtdeentregue; }
            set { _qtdeentregue = value; }
        }
        public Decimal qtdeosaux
        {
            get { return _qtdeosaux; }
            set { _qtdeosaux = value; }
        }
        public Decimal qtdesaldo
        {
            get { return _qtdesaldo; }
            set { _qtdesaldo = value; }
        }

        public Decimal qtdesucata
        {
            get { return _qtdesucata; }
            set { _qtdesucata = value; }
        }

        public String setor
        {
            get { return _setor; }
            set { _setor = value; }
        }

        public Decimal setorfator
        {
            get { return _setorfator; }
            set { _setorfator = value; }
        }
        public String situacao
        {
            get { return _situacao; }
            set { _situacao = value; }
        }
        public String termino
        {
            get { return _termino; }
            set { _termino = value; }
        }
        public String tipofrete
        {
            get { return _tipofrete; }
            set { _tipofrete = value; }
        }
        public Decimal totalbaseicm
        {
            get { return _totalbaseicm; }
            set { _totalbaseicm = value; }
        }
        public Decimal totalbaseicmsubst
        {
            get { return _totalbaseicmsubst; }
            set { _totalbaseicmsubst = value; }
        }
        public Decimal totalfrete
        {
            get { return _totalfrete; }
            set { _totalfrete = value; }
        }
        public Decimal totalicm
        {
            get { return _totalicm; }
            set { _totalicm = value; }
        }
        public Decimal totalicmsubst
        {
            get { return _totalicmsubst; }
            set { _totalicmsubst = value; }
        }
        public Decimal totalipi
        {
            get { return _totalipi; }
            set { _totalipi = value; }
        }
        public Decimal totalmercadoria
        {
            get { return _totalmercadoria; }
            set { _totalmercadoria = value; }
        }
        public Decimal totaloutras
        {
            get { return _totaloutras; }
            set { _totaloutras = value; }
        }
        public Decimal totalpeca
        {
            get { return _totalpeca; }
            set { _totalpeca = value; }
        }
        public Decimal totalpecaentra
        {
            get { return _totalpecaentra; }
            set { _totalpecaentra = value; }
        }
        public Decimal totalpecatransfe
        {
            get { return _totalpecatransfe; }
            set { _totalpecatransfe = value; }
        }
        public Decimal totalpedido
        {
            get { return _totalpedido; }
            set { _totalpedido = value; }
        }
        public Decimal totalpeso
        {
            get { return _totalpeso; }
            set { _totalpeso = value; }
        }
        public Decimal totalprevisto
        {
            get { return _totalprevisto; }
            set { _totalprevisto = value; }
        }
        public Decimal totalseguro
        {
            get { return _totalseguro; }
            set { _totalseguro = value; }
        }
        public String transporte
        {
            get { return _transporte; }
            set { _transporte = value; }
        }




    }
}
