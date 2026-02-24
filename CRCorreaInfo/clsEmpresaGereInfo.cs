using System;

namespace CRCorreaInfo
{
    public class clsEmpresaGereInfo
    {
        public Decimal icmsacreditar { get; set; }  // total pispasep

        Int32 _id;
        Int32 _empresa;
        String _cta991;
        String _cta995;
        Decimal	_jurosmes;
        Int32 _protestar;
        public Decimal multa { get; set; }
        String _boletolinha01;
        String _boletolinha02;
        String _boletolinha03;
        String _boletolinha04;
        String _boletolinha05;
        Int32 _sequencia;
        Int32 _registro;
        String _aceitanrozero;
        String _aceitavalorzero;
        Int32 _aceitaqtosdias;
        Int32 _grupodespesa;
        String _despesaconduta;
        String _despesaprocedure;
        Int32 _centrocusto;
        Int32 _setorcentrocusto;
        Decimal _basemp;
        String _custopordentro;
        Decimal _lucro;
        Decimal _comissao;
        Decimal _rejeicao;
        Decimal _adm;
        Decimal _financeira;
        Decimal _bola;
        Decimal _pis;
        Decimal _cofins;
        Decimal _irpj;
        Decimal _contsocial;
        Decimal _icm;
        Decimal _ipi;
        Decimal _markup;
        Decimal _qtdemais;
        Decimal _qtdemenos;
        Decimal _valormais;
        Decimal _valormenos;
        Decimal _totalmais;
        Decimal _totalmenos;
        Decimal _vencemais;
        Decimal _vencemenos;
        String _somapesonfv;
        String _listaprefat;
        Decimal _fiscalirrf;
        Decimal _fiscalinss;
        Decimal _fiscalpis;
        Decimal _fiscalcofins;
        Decimal _fiscalcsll;
        Decimal _fiscalpiscofins;
        Decimal _fiscaliss;
        Decimal _limitepiscofins;
        Int32 _idcodirrf;
        Int32 _idcodinss;
        Int32 _idcodpis;
        Int32 _idcodcofins;
        Int32 _idcodcsll;
        Int32 _idcodpiscofins;
        Int32 _idcodiss;
        String _darfirrf;
        String _darfinss;
        String _darfpis;
        String _darfcofins;
        String _darfcsll;
        String _darfpiscofins;
        String _darfiss;
        String _ctabilcontavinculada;
        String _gerautorizasolicitacao;
        String _gerautorizacompra;
        String _gercotacaopreco;
        String _gerdivergenciacompra;
        String _gerautorizapedido;
        String _gerautorizafaturar;
        Decimal _autorizacomprador;
        Decimal _autorizagerente;
        Decimal _autorizanormal;
        String _pedidofrete;
        String _bcoplanocontas;
        Int32 _bcoarquivomortodias;
        DateTime _bcoarquivodata;
        String _bcochequeunico;
        Int32 _bcoctachepreemi;
        Int32 _bcoctacheprerec;
        Int32	 _bcoctachedev;
        Int32 _bcoctachedevemi;
        String _comissaovaria;
        String _transfbcohist;
        String _transfbcocusto;
        Int32 _idregimeapuracao;
        Int32 _idregimetributario;
        Decimal _leipispasep;
        Decimal _leicofins;
        Decimal _leipispasepretido;
        Decimal _leicofinsretido;
        Decimal _qtdemaisun;
        Decimal _qtdemenosun;
        Decimal _qtdepedcun;
        Decimal _qtdepedckg;
        String _temprospeccao;
        Int32 _metodoautsolic;
        Int32 _checarsolic;
        Int32 _idforirrf;
        Int32 _idpagirrf;
        Int32 _idforinss;
        Int32 _idpaginss;
        Int32 _idforpis;
        Int32 _idpagpis;
        Int32 _idforcofins;
        Int32 _idpagcofins;
        Int32 _idforcsll;
        Int32 _idpagcsll;
        Int32 _idforpiscofins;
        Int32 _idpagpiscofins;
        Int32 _idforiss;
        Int32 _idpagiss;
        Decimal _peddescvis;
        Decimal _peddescgd;
        Decimal _peddescpzo;
        String _baixarcomhistorico;
        String _baixatemnotafiscal;
        String _representantelegal;
        String _cpfrepresentante;
        String _cargorepresentante;


        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String cta991
        {
            get { return _cta991; }
            set { _cta991 = value; }
        }

        public String cta995
        {
            get { return _cta995; }
            set { _cta995 = value; }
        }

        public Decimal jurosmes
        {
            get { return _jurosmes; }
            set { _jurosmes = value; }
        }
        public Int32 protestar
        {
            get { return _protestar; }
            set { _protestar = value; }
        }

        public String boletolinha01
        {
            get { return _boletolinha01; }
            set { _boletolinha01 = value; }
        }

        public String boletolinha02
        {
            get { return _boletolinha02; }
            set { _boletolinha02 = value; }
        }

        public String boletolinha03
        {
            get { return _boletolinha03; }
            set { _boletolinha03 = value; }
        }

        public String boletolinha04
        {
            get { return _boletolinha04; }
            set { _boletolinha04 = value; }
        }

        public String boletolinha05
        {
            get { return _boletolinha05; }
            set { _boletolinha05 = value; }
        }

        public Int32 sequencia
        {
            get { return _sequencia; }
            set { _sequencia = value; }
        }

        public Int32 registro
        {
            get { return _registro; }
            set { _registro = value; }
        }

        public String aceitanrozero
        {
            get { return _aceitanrozero; }
            set { _aceitanrozero = value; }
        }

        public String aceitavalorzero
        {
            get { return _aceitavalorzero; }
            set { _aceitavalorzero = value; }
        }

        public Int32 aceitaqtosdias
        {
            get { return _aceitaqtosdias; }
            set { _aceitaqtosdias = value; }
        }

        public Int32 grupodespesa
        {
            get { return _grupodespesa; }
            set { _grupodespesa = value; }
        }

        public String despesaconduta
        {
            get { return _despesaconduta; }
            set { _despesaconduta = value; }
        }

        public String despesaprocedure
        {
            get { return _despesaprocedure; }
            set { _despesaprocedure = value; }
        }

        public Int32 centrocusto
        {
            get { return _centrocusto; }
            set { _centrocusto = value; }
        }
        
        public Int32 setorcentrocusto
        {
            get { return _setorcentrocusto; }
            set { _setorcentrocusto = value; }
        }

        public Decimal basemp
        {
            get { return _basemp; }
            set { _basemp = value; }
        }
        
        public String custopordentro
        {
            get { return _custopordentro; }
            set { _custopordentro = value; }
        }

        public Decimal lucro
        {
            get { return _lucro; }
            set { _lucro = value; }
        }

        public Decimal comissao
        {
            get { return _comissao; }
            set { _comissao = value; }
        }

        public Decimal rejeicao
        {
            get { return _rejeicao; }
            set { _rejeicao = value; }
        }

        public Decimal adm
        {
            get { return _adm; }
            set { _adm = value; }
        }

        public Decimal financeira
        {
            get { return _financeira; }
            set { _financeira = value; }
        }

        public Decimal bola
        {
            get { return _bola; }
            set { _bola = value; }
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

        public Decimal irpj
        {
            get { return _irpj; }
            set { _irpj = value; }
        }

        public Decimal contsocial
        {
            get { return _contsocial; }
            set { _contsocial = value; }
        }

        public Decimal icm
        {
            get { return _icm; }
            set { _icm = value; }
        }

        public Decimal ipi
        {
            get { return _ipi; }
            set { _ipi = value; }
        }

        public Decimal markup
        {
            get { return _markup; }
            set { _markup = value; }
        }

        public Decimal qtdemais
        {
            get { return _qtdemais; }
            set { _qtdemais = value; }
        }

        public Decimal qtdemenos
        {
            get { return _qtdemenos; }
            set { _qtdemenos = value; }
        }

        public Decimal valormais
        {
            get { return _valormais; }
            set { _valormais = value; }
        }

        public Decimal valormenos
        {
            get { return _valormenos; }
            set { _valormenos = value; }
        }

        public Decimal totalmais
        {
            get { return _totalmais; }
            set { _totalmais = value; }
        }

        public Decimal totalmenos
        {
            get { return _totalmenos; }
            set { _totalmenos = value; }
        }

        public Decimal vencemais
        {
            get { return _vencemais; }
            set { _vencemais = value; }
        }

        public Decimal vencemenos
        {
            get { return _vencemenos; }
            set { _vencemenos = value; }
        }

        public String somapesonfv
        {
            get { return _somapesonfv; }
            set { _somapesonfv = value; }
        }

        public String listaprefat
        {
            get { return _listaprefat; }
            set { _listaprefat = value; }
        }

        public Decimal fiscalirrf
        {
            get { return _fiscalirrf; }
            set { _fiscalirrf = value; }
        }

        public Decimal fiscalinss
        {
            get { return _fiscalinss; }
            set { _fiscalinss = value; }
        }

        public Decimal fiscalpis
        {
            get { return _fiscalpis; }
            set { _fiscalpis = value; }
        }

        public Decimal fiscalcofins
        {
            get { return _fiscalcofins; }
            set { _fiscalcofins = value; }
        }

        public Decimal fiscalcsll
        {
            get { return _fiscalcsll; }
            set { _fiscalcsll = value; }
        }

        public Decimal fiscalpiscofins
        {
            get { return _fiscalpiscofins; }
            set { _fiscalpiscofins = value; }
        }

        public Decimal fiscaliss
        {
            get { return _fiscaliss; }
            set { _fiscaliss = value; }
        }

        public Decimal limitepiscofins
        {
            get { return _limitepiscofins; }
            set { _limitepiscofins = value; }
        }

        public Int32 idcodirrf
        {
            get { return _idcodirrf; }
            set { _idcodirrf = value; }
        }

        public Int32 idcodinss
        {
            get { return _idcodinss; }
            set { _idcodinss = value; }
        }

        public Int32 idcodpis
        {
            get { return _idcodpis; }
            set { _idcodpis = value; }
        }

        public Int32 idcodcofins
        {
            get { return _idcodcofins; }
            set { _idcodcofins = value; }
        }

        public Int32 idcodcsll
        {
            get { return _idcodcsll; }
            set { _idcodcsll = value; }
        }

        public Int32 idcodpiscofins
        {
            get { return _idcodpiscofins; }
            set { _idcodpiscofins = value; }
        }

        public Int32 idcodiss
        {
            get { return _idcodiss; }
            set { _idcodiss = value; }
        }

        public String darfirrf
        {
            get { return _darfirrf; }
            set { _darfirrf = value; }
        }

        public String darfinss
        {
            get { return _darfinss; }
            set { _darfinss = value; }
        }

        public String darfpis
        {
            get { return _darfpis; }
            set { _darfpis = value; }
        }

        public String darfcofins
        {
            get { return _darfcofins; }
            set { _darfcofins = value; }
        }

        public String darfcsll
        {
            get { return _darfcsll; }
            set { _darfcsll = value; }
        }

        public String darfpiscofins
        {
            get { return _darfpiscofins; }
            set { _darfpiscofins = value; }
        }

        public String darfiss
        {
            get { return _darfiss; }
            set { _darfiss = value; }
        }

        public String ctabilcontavinculada
        {
            get { return _ctabilcontavinculada; }
            set { _ctabilcontavinculada = value; }
        }

        public String gerautorizasolicitacao
        {
            get { return _gerautorizasolicitacao; }
            set { _gerautorizasolicitacao = value; }
        }

        public String gerautorizacompra
        {
            get { return _gerautorizacompra; }
            set { _gerautorizacompra = value; }
        }

        public String gercotacaopreco
        {
            get { return _gercotacaopreco; }
            set { _gercotacaopreco = value; }
        }

        public String gerdivergenciacompra
        {
            get { return _gerdivergenciacompra; }
            set { _gerdivergenciacompra = value; }
        }

        public String gerautorizapedido
        {
            get { return _gerautorizapedido; }
            set { _gerautorizapedido = value; }
        }

        public String gerautorizafaturar
        {
            get { return _gerautorizafaturar; }
            set { _gerautorizafaturar = value; }
        }

        public Decimal autorizacomprador
        {
            get { return _autorizacomprador; }
            set { _autorizacomprador = value; }
        }

        public Decimal autorizagerente
        {
            get { return _autorizagerente; }
            set { _autorizagerente = value; }
        }
        public Decimal autorizanormal
        {
            get { return _autorizanormal; }
            set { _autorizanormal = value; }
        }

        public String pedidofrete
        {
            get { return _pedidofrete; }
            set { _pedidofrete = value; }
        }

        public String bcoplanocontas
        {
            get { return _bcoplanocontas; }
            set { _bcoplanocontas = value; }
        }

        public Int32 bcoarquivomortodias
        {
            get { return _bcoarquivomortodias; }
            set { _bcoarquivomortodias = value; }
        }

        public DateTime bcoarquivodata
        {
            get { return _bcoarquivodata; }
            set { _bcoarquivodata = value; }
        }

        public String bcochequeunico
        {
            get { return _bcochequeunico; }
            set { _bcochequeunico = value; }
        }

        public Int32 bcoctachepreemi
        {
            get { return _bcoctachepreemi; }
            set { _bcoctachepreemi = value; }
        }

        public Int32 bcoctacheprerec
        {
            get { return _bcoctacheprerec; }
            set { _bcoctacheprerec = value; }
        }

        public Int32 bcoctachedev
        {
            get { return _bcoctachedev; }
            set { _bcoctachedev = value; }
        }

        public Int32 bcoctachedevemi
        {
            get { return _bcoctachedevemi; }
            set { _bcoctachedevemi = value; }
        }

        public String comissaovaria
        {
            get { return _comissaovaria; }
            set { _comissaovaria = value; }
        }

        public String transfbcohist
        {
            get { return _transfbcohist; }
            set { _transfbcohist = value; }
        }

        public String transfbcocusto
        {
            get { return _transfbcocusto; }
            set { _transfbcocusto = value; }
        }

        public Int32 idregimeapuracao
        {
            get { return _idregimeapuracao; }
            set { _idregimeapuracao = value; }
        }

        public Int32 idregimetributario
        {
            get { return _idregimetributario; }
            set { _idregimetributario = value; }
        }

        public Decimal leipispasep
        {
            get { return _leipispasep; }
            set { _leipispasep = value; }
        }

        public Decimal leicofins
        {
            get { return _leicofins; }
            set { _leicofins = value; }
        }

        public Decimal leipispasepretido
        {
            get { return _leipispasepretido; }
            set { _leipispasepretido = value; }
        }

        public Decimal leicofinsretido
        {
            get { return _leicofinsretido; }
            set { _leicofinsretido = value; }
        }

        public Decimal qtdemaisun
        {
            get { return _qtdemaisun; }
            set { _qtdemaisun = value; }
        }

        public Decimal qtdemenosun
        {
            get { return _qtdemenosun; }
            set { _qtdemenosun = value; }
        }

        public Decimal qtdepedcun
        {
            get { return _qtdepedcun; }
            set { _qtdepedcun = value; }
        }

        public Decimal qtdepedckg
        {
            get { return _qtdepedckg; }
            set { _qtdepedckg = value; }
        }

        public String temprospeccao
        {
            get { return _temprospeccao; }
            set { _temprospeccao = value; }
        }

        public Int32 metodoautsolic
        {
            get { return _metodoautsolic; }
            set { _metodoautsolic = value; }
        }

        public Int32 checarsolic
        {
            get { return _checarsolic; }
            set { _checarsolic = value; }
        }

        public Int32 idforirrf
        {
            get { return _idforirrf; }
            set { _idforirrf = value; }
        }

        public Int32 idpagirrf
        {
            get { return _idpagirrf; }
            set { _idpagirrf = value; }
        }

        public Int32 idforinss
        {
            get { return _idforinss; }
            set { _idforinss = value; }
        }

        public Int32 idpaginss
        {
            get { return _idpaginss; }
            set { _idpaginss = value; }
        }

        public Int32 idforpis
        {
            get { return _idforpis; }
            set { _idforpis = value; }
        }

        public Int32 idpagpis
        {
            get { return _idpagpis; }
            set { _idpagpis = value; }
        }

        public Int32 idforcofins
        {
            get { return _idforcofins; }
            set { _idforcofins = value; }
        }

        public Int32 idpagcofins
        {
            get { return _idpagcofins; }
            set { _idpagcofins = value; }
        }

        public Int32 idforcsll
        {
            get { return _idforcsll; }
            set { _idforcsll = value; }
        }

        public Int32 idpagcsll
        {
            get { return _idpagcsll; }
            set { _idpagcsll = value; }
        }

        public Int32 idforpiscofins
        {
            get { return _idforpiscofins; }
            set { _idforpiscofins = value; }
        }

        public Int32 idpagpiscofins
        {
            get { return _idpagpiscofins; }
            set { _idpagpiscofins = value; }
        }

        public Int32 idforiss
        {
            get { return _idforiss; }
            set { _idforiss = value; }
        }

        public Int32 idpagiss
        {
            get { return _idpagiss; }
            set { _idpagiss = value; }
        }

        public Decimal peddescpzo
        {
            get { return _peddescpzo; }
            set { _peddescpzo = value; }
        }

        public Decimal peddescvis
        {
            get { return _peddescvis; }
            set { _peddescvis = value; }
        }

        public Decimal peddescgd
        {
            get { return _peddescgd; }
            set { _peddescgd = value; }
        }
        public String baixarcomhistorico
        {
            get { return _baixarcomhistorico; }
            set { _baixarcomhistorico = value; }
        }
        public String baixatemnotafiscal
        {
            get { return _baixatemnotafiscal; }
            set { _baixatemnotafiscal = value; }
        }

        public String representantelegal
        {
            get { return _representantelegal; }
            set { _representantelegal = value; }
        }

        public String cpfrepresentante
        {
            get { return _cpfrepresentante; }
            set { _cpfrepresentante = value; }
        }

        public String cargorepresentante
        {
            get { return _cargorepresentante; }
            set { _cargorepresentante = value; }
        }
        
    }
}
