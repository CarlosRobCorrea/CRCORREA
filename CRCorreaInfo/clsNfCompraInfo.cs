using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsNfCompraInfo
    {
        private String _chnfe;
        private String _caminhonfe_xml;
        private String _caminhonfe_pdf;
        private Decimal _cofins;
        private Decimal _cofins1;
        private Decimal _csll;
        private DateTime _data;
        private DateTime _datalanca;
        private DateTime _datarecebimento;
        private String _emitente;
        private String _frete;
        private String _fretepaga;
        private Int32 _filial;
        private Int32 _id;
        private Int32 _idcondpagto;
        private Int32 _iddocumento;
        private Int32 _idformapagto;
        private Int32 _idfornecedor;
        private Int32 _idfornecedororigem;
        private Int32 _idpedido;
        private Int32 _idtransportadora;
        private Decimal _inss;
        private Decimal _irrf;
        private Decimal _iss;
        
        private Int64 _numero;
        private String _observa;
        private Decimal _pis;
        private Decimal _piscofinscsll;
        private Decimal _pispasep;
        private String _serie;
        private String _setor;
        private Decimal _setorfator;
        private String _situacao;
        private String _tipoentrada;
        private String _transporte;
        private String _tipofrete;
        private Decimal _totalbaseicm;
        private Decimal _totalicm;
        private Decimal _totalbaseicmsubst;
        private Decimal _totalicmsubst;
        private Decimal _totalpeso;
        private Decimal _totalpesobruto;
        private Decimal _totalmercadoria;
        private Decimal _totalipi;
        private Decimal _totalfrete;
        private Decimal _totalseguro;
        private Decimal _totaloutras;
        private Decimal _totalnotafiscal;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }


        public String chnfe
        {
            get { return _chnfe; }
            set { _chnfe = value; }
        }

        public String caminhonfe_xml
        {
            get { return _caminhonfe_xml; }
            set { _caminhonfe_xml = value; }
        }

        public String caminhonfe_pdf
        {
            get { return _caminhonfe_pdf; }
            set { _caminhonfe_pdf = value; }
        }

        public Int32 filial
        {
            get { return _filial; }
            set { _filial = value; }
        }

        public Int64 numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }

        public DateTime datarecebimento
        {
            get { return _datarecebimento; }
            set { _datarecebimento = value; }
        }
        public Int32 iddocumento
        {
            get { return _iddocumento; }
            set { _iddocumento = value; }
        }
        public String serie
        {
            get { return _serie; }
            set { _serie = value; }
        }

        public String tipoentrada
        {
            get { return _tipoentrada; }
            set { _tipoentrada = value; }
        }

        public Int32 idpedido
        {
            get { return _idpedido; }
            set { _idpedido = value; }
        }

        public Int32 idfornecedor
        {
            get { return _idfornecedor; }
            set { _idfornecedor = value; }
        }

        public Int32 idfornecedororigem
        {
            get { return _idfornecedororigem; }
            set { _idfornecedororigem = value; }
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

        public String situacao
        {
            get { return _situacao; }
            set { _situacao = value; }
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


        public Int32 idcondpagto
        {
            get { return _idcondpagto; }
            set { _idcondpagto = value; }
        }

        public Int32 idformapagto
        {
            get { return _idformapagto; }
            set { _idformapagto = value; }
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

        public String transporte
        {
            get { return _transporte; }
            set { _transporte = value; }
        }

        public Int32 idtransportadora
        {
            get { return _idtransportadora; }
            set { _idtransportadora = value; }
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

        public Decimal totalicm
        {
            get { return _totalicm; }
            set { _totalicm = value; }
        }

        public Decimal totalbaseicmsubst
        {
            get { return _totalbaseicmsubst; }
            set { _totalbaseicmsubst = value; }
        }

        public Decimal totalicmsubst
        {
            get { return _totalicmsubst; }
            set { _totalicmsubst = value; }
        }

        public Decimal totalpeso
        {
            get { return _totalpeso; }
            set { _totalpeso = value; }
        }

        public Decimal totalpesobruto
        {
            get { return _totalpesobruto; }
            set { _totalpesobruto = value; }
        }

        public Decimal totalmercadoria
        {
            get { return _totalmercadoria; }
            set { _totalmercadoria = value; }
        }

        public Decimal totalipi
        {
            get { return _totalipi; }
            set { _totalipi = value; }
        }

        public Decimal totalfrete
        {
            get { return _totalfrete; }
            set { _totalfrete = value; }
        }

        public Decimal totalseguro
        {
            get { return _totalseguro; }
            set { _totalseguro = value; }
        }

        public Decimal totaloutras
        {
            get { return _totaloutras; }
            set { _totaloutras = value; }
        }

        public Decimal totalnotafiscal
        {
            get { return _totalnotafiscal; }
            set { _totalnotafiscal = value; }
        }

        public Decimal pispasep
        {
            get { return _pispasep; }
            set { _pispasep = value; }
        }

        public Decimal cofins1
        {
            get { return _cofins1; }
            set { _cofins1 = value; }
        }


        public Decimal irrf
        {
            get { return _irrf; }
            set { _irrf = value; }
        }

        public Decimal inss
        {
            get { return _inss; }
            set { _inss = value; }
        }

        public Decimal piscofinscsll
        {
            get { return _piscofinscsll; }
            set { _piscofinscsll = value; }
        }

        public Decimal pis
        {
            get { return _pis; }
            set { _pis = value; }
        }
        public Decimal cofins
        {
            get { return _cofins; }
            set { _cofins = value; }
        }
        public Decimal csll
        {
            get { return _csll; }
            set { _csll = value; }
        }
        public Decimal iss
        {
            get { return _iss; }
            set { _iss = value; }
        }

        public String observa
        {
            get { return _observa; }
            set { _observa = value; }
        }
    }
}
