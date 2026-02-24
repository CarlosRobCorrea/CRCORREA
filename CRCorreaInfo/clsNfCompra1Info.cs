using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsNfCompra1Info
    {
        private Int32 _id;
        private Decimal _aliqcofins1;
        private Decimal _aliqpispasep;
        private Decimal _basemp;
        private Decimal _baseicm;
        private Decimal _baseicmsubst;
        private Decimal _bcipi;
        private Decimal _bccofins1;
        private Decimal _bcpispasep;
        private String _calculoautomatico;
        private String _codigoemp01;
        private String _codigoemp02;
        private String _codigoemp03;
        private String _codigoemp04;
        private Decimal _cofins;
        private Decimal _cofins1;
        private Decimal _cofinsporc;
        private String _complemento;
        private String _complemento1;
        private String _creditaricm;
        private Decimal _csll;
        private Decimal _csllporc;
        private String _consumo;
        private Decimal _custoicm;
        private Decimal _custoipi;
        public DateTime datatabela { get; set; }
        private String _descricaoesp;
        private Decimal _fatorconv;
        private String _fatura;
        private String _faturando;
        private Decimal _icm;
        private Decimal _icminterno;
        private Decimal _icmssubstreducao;
        private Decimal _icmsubst;
        private Int32 _idcodigo1;
        private Int32 _iddestino;
        private Int32 _idipi;
        private Int32 _idcentrocusto;
        private Int32 _idcfop;
        private Int32 _idcfopfis;
        private Int32 _idcodigoctabil;
        private Int32 _idhistorico;
        private Int32 _idcodigo;
        private Int32 _idcotacao;
        private Int32 _iditemcotacao;
        private Int32 _iditempedido;
        private Int32 _iditempedidoentrega;
        private Int32 _idnotafiscal;
        private Int32 _idnfvendaitem;
        private Int32 _idnfevales;
        private Int32 _idpedido;
        private Int32 _idpedidovenda;
        private Int32 _idpedidovendaitem;
        private Int32 _idordemservico;        

        private Int32 _idsittriba;
        private Int32 _idsittribb;
        private Int32 _idsittribcofins1;
        private Int32 _idsittribipi;
        private Int32 _idsittribpis;

        private Int32 _idsolicitacao;
        private Int32 _idtiponota;
        private Int32 _idtiponotasaida;
        private Int32 _idunid;
        private Int32 _idunidfiscal;

        private Decimal _ipi;
        private Decimal _iss;
        private Decimal _issporc;
        public Decimal margemlucro { get; set; }
        private Decimal _peso;
        private Decimal _pesototal;
        private Decimal _piscofinscsll;
        private Decimal _piscofinscsllporc;
        private Decimal _pis;
        private Decimal _pisporc;
        private Decimal _pispasep;
        private Decimal _preco;
        private Decimal _qtde;
        private Decimal _qtdefiscal;
        private Decimal _qtdedevsaldo;
        private Decimal _qtdedev;
        private Decimal _qtdedevbaixa;
        private Decimal _qtdeticket;

        private Decimal _inss;
        private Decimal _inssporc;
        private Decimal _irrf;
        private Decimal _irrfporc;
        private String _msg;
        private Int32 _numero;
        private Decimal _reducao;
        private String _situacao;
        private String _somaproduto;
        private String _tipodestino;
        public String tipoproduto { get; set; }

        private Decimal _totalmercado;
        private Decimal _totalnota;
        private Decimal _valorfrete;
        private String _valorfreteicms;
        private Decimal _valorseguro;
        private String _valorseguroicms;
        private Decimal _valoroutras;
        private String _valoroutrasicms;
        private Int32 _vidautil;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        
        public Decimal aliqcofins1
        {
            get { return _aliqcofins1; }
            set { _aliqcofins1 = value; }
        }        
        public Decimal aliqpispasep
        {
            get { return _aliqpispasep; }
            set { _aliqpispasep = value; }
        }        
        
        public Decimal bcipi
        {
            get { return _bcipi; }
            set { _bcipi = value; }
        }        
        public Decimal bccofins1
        {
            get { return _bccofins1; }
            set { _bccofins1 = value; }
        }        

        public Decimal bcpispasep
        {
            get { return _bcpispasep; }
            set { _bcpispasep = value; }
        }
        public String complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }
        public String complemento1
        {
            get { return _complemento1; }
            set { _complemento1 = value; }
        }
        public Decimal icmssubstreducao
        {
            get { return _icmssubstreducao; }
            set { _icmssubstreducao = value; }
        }        

        public Int32 numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public Decimal qtdedevsaldo
        {
            get { return _qtdedevsaldo; }
            set { _qtdedevsaldo = value; }
        }
        public Decimal qtdeticket
        {
            get { return _qtdeticket; }
            set { _qtdeticket = value; }
        }
        public Int32 idcodigo
        {
            get { return _idcodigo; }
            set { _idcodigo = value; }
        }
        public Int32 idcotacao
        {
            get { return _idcotacao; }
            set { _idcotacao = value; }
        }
        public Int32 iditemcotacao
        {
            get { return _iditemcotacao; }
            set { _iditemcotacao = value; }
        }
        public Int32 iditempedido
        {
            get { return _iditempedido; }
            set { _iditempedido = value; }
        }
        public Int32 iditempedidoentrega
        {
            get { return _iditempedidoentrega; }
            set { _iditempedidoentrega = value; }
        }
        public Int32 idordemservico
        {
            get { return _idordemservico; }
            set { _idordemservico = value; }
        }
        public Int32 idpedido
        {
            get { return _idpedido; }
            set { _idpedido = value; }
        }
        public Int32 idpedidovenda
        {
            get { return _idpedidovenda; }
            set { _idpedidovenda = value; }
        }

        public Int32 idpedidovendaitem
        {
            get { return _idpedidovendaitem; }
            set { _idpedidovendaitem = value; }
        }

        public Int32 idsolicitacao
        {
            get { return _idsolicitacao; }
            set { _idsolicitacao = value; }
        }

        /// <summary>
        /// //////////////////////////
        /// </summary>



        public String descricaoesp
        {
            get { return _descricaoesp; }
            set { _descricaoesp = value; }
        }

        public String msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        public Int32 idsittriba
        {
            get { return _idsittriba; }
            set { _idsittriba = value; }
        }

        public Int32 idsittribb
        {
            get { return _idsittribb; }
            set { _idsittribb = value; }
        }
        public Int32 idsittribcofins1
        {
            get { return _idsittribcofins1; }
            set { _idsittribcofins1 = value; }
        }

        public Int32 idsittribipi
        {
            get { return _idsittribipi; }
            set { _idsittribipi = value; }
        }

        public Int32 idsittribpis
        {
            get { return _idsittribpis; }
            set { _idsittribpis = value; }
        }

        public Int32 idcfop
        {
            get { return _idcfop; }
            set { _idcfop = value; }
        }

        public Int32 idcfopfis
        {
            get { return _idcfopfis; }
            set { _idcfopfis = value; }
        }
        public Int32 idtiponota
        {
            get { return _idtiponota; }
            set { _idtiponota = value; }
        }

        public Int32 idtiponotasaida
        {
            get { return _idtiponotasaida; }
            set { _idtiponotasaida = value; }
        }

        public String consumo
        {
            get { return _consumo; }
            set { _consumo = value; }
        }

        public String situacao
        {
            get { return _situacao; }
            set { _situacao = value; }
        }

        public Decimal qtdefiscal
        {
            get { return _qtdefiscal; }
            set { _qtdefiscal = value; }
        }

        public Int32 idunidfiscal
        {
            get { return _idunidfiscal; }
            set { _idunidfiscal = value; }
        }

        public Decimal qtde
        {
            get { return _qtde; }
            set { _qtde = value; }
        }        

        public Int32 idunid
        {
            get { return _idunid; }
            set { _idunid = value; }
        }

        public Decimal fatorconv
        {
            get { return _fatorconv; }
            set { _fatorconv = value; }
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

        public Decimal preco
        {
            get { return _preco; }
            set { _preco = value; }
        }

        public Decimal totalmercado
        {
            get { return _totalmercado; }
            set { _totalmercado = value; }
        }

        public Int32 idipi
        {
            get { return _idipi; }
            set { _idipi = value; }
        }

        public Decimal ipi
        {
            get { return _ipi; }
            set { _ipi = value; }
        }        

        public Decimal custoipi
        {
            get { return _custoipi; }
            set { _custoipi = value; }
        }

        public Decimal icm
        {
            get { return _icm; }
            set { _icm = value; }
        }

        public Decimal icminterno
        {
            get { return _icminterno; }
            set { _icminterno = value; }
        }

        public Decimal custoicm
        {
            get { return _custoicm; }
            set { _custoicm = value; }
        }        

        public Decimal basemp
        {
            get { return _basemp; }
            set { _basemp = value; }
        }

        public Decimal baseicmsubst
        {
            get { return _baseicmsubst; }
            set { _baseicmsubst = value; }
        }

        public Decimal icmsubst
        {
            get { return _icmsubst; }
            set { _icmsubst = value; }
        }

        public Decimal reducao
        {
            get { return _reducao; }
            set { _reducao = value; }
        }

        public Decimal baseicm
        {
            get { return _baseicm; }
            set { _baseicm = value; }
        }

        public Decimal totalnota
        {
            get { return _totalnota; }
            set { _totalnota = value; }
        }        

        public Decimal peso
        {
            get { return _peso; }
            set { _peso = value; }
        }

        public Decimal pesototal
        {
            get { return _pesototal; }
            set { _pesototal = value; }
        }

        public Int32 vidautil
        {
            get { return _vidautil; }
            set { _vidautil = value; }
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

        public Decimal irrfporc
        {
            get { return _irrfporc; }
            set { _irrfporc = value; }
        }

        public Decimal irrf
        {
            get { return _irrf; }
            set { _irrf = value; }
        }

        public Decimal inssporc
        {
            get { return _inssporc; }
            set { _inssporc = value; }
        }

        public Decimal inss
        {
            get { return _inss; }
            set { _inss = value; }
        }

        public Decimal piscofinscsllporc
        {
            get { return _piscofinscsllporc; }
            set { _piscofinscsllporc = value; }
        }

        public Decimal piscofinscsll
        {
            get { return _piscofinscsll; }
            set { _piscofinscsll = value; }
        }

        public Decimal pisporc
        {
            get { return _pisporc; }
            set { _pisporc = value; }
        }

        public Decimal pis
        {
            get { return _pis; }
            set { _pis = value; }
        }

        public Decimal cofinsporc
        {
            get { return _cofinsporc; }
            set { _cofinsporc = value; }
        }

        public Decimal cofins
        {
            get { return _cofins; }
            set { _cofins = value; }
        }

        public Decimal csllporc
        {
            get { return _csllporc; }
            set { _csllporc = value; }
        }

        public Decimal csll
        {
            get { return _csll; }
            set { _csll = value; }
        }

        public Decimal issporc
        {
            get { return _issporc; }
            set { _issporc = value; }
        }

        public Decimal iss
        {
            get { return _iss; }
            set { _iss = value; }
        }

        public String codigoemp01
        {
            get { return _codigoemp01; }
            set { _codigoemp01 = value; }
        }

        public String codigoemp02
        {
            get { return _codigoemp02; }
            set { _codigoemp02 = value; }
        }

        public String codigoemp03
        {
            get { return _codigoemp03; }
            set { _codigoemp03 = value; }
        }

        public String codigoemp04
        {
            get { return _codigoemp04; }
            set { _codigoemp04 = value; }
        }


        public Decimal qtdedev
        {
            get { return _qtdedev; }
            set { _qtdedev = value; }
        }

        public Decimal qtdedevbaixa
        {
            get { return _qtdedevbaixa; }
            set { _qtdedevbaixa = value; }
        }


        public Int32 idcodigo1
        {
            get { return _idcodigo1; }
            set { _idcodigo1 = value; }
        }

        public Int32 idnotafiscal
        {
            get { return _idnotafiscal; }
            set { _idnotafiscal = value; }
        }

        public Int32 idnfvendaitem
        {
            get { return _idnfvendaitem; }
            set { _idnfvendaitem = value; }
        }

        public Int32 iddestino
        {
            get { return _iddestino; }
            set { _iddestino = value; }
        }

        public String tipodestino
        {
            get { return _tipodestino; }
            set { _tipodestino = value; }
        }

        //public Int32 idnotafiscal
        //{
        //    get { return _idnotafiscal; }
        //    set { _idnotafiscal = value; }
        //}

        //public Int32 idnfvendaitem
        //{
        //    get { return _idnfvendaitem; }
        //    set { _idnfvendaitem = value; }
        //}

        public String faturando
        {
            get { return _faturando; }
            set { _faturando = value; }
        }

        //public Decimal qtdedevolvida
        //{
        //    get { return _qtdedevolvida; }
        //    set { _qtdedevolvida = value; }
        //}

        //public Decimal qtderemessa
        //{
        //    get { return _qtderemessa; }
        //    set { _qtderemessa = value; }
        //}
        public Decimal valorfrete
        {
            get { return _valorfrete; }
            set { _valorfrete = value; }
        }

        public String valorfreteicms
        {
            get { return _valorfreteicms; }
            set { _valorfreteicms = value; }
        }

        public Decimal valorseguro
        {
            get { return _valorseguro; }
            set { _valorseguro = value; }
        }

        public String valorseguroicms
        {
            get { return _valorseguroicms; }
            set { _valorseguroicms = value; }
        }

        public Decimal valoroutras
        {
            get { return _valoroutras; }
            set { _valoroutras = value; }
        }

        public String valoroutrasicms
        {
            get { return _valoroutrasicms; }
            set { _valoroutrasicms = value; }
        }

        public String calculoautomatico
        {
            get { return _calculoautomatico; }
            set { _calculoautomatico = value; }
        }

        public String creditaricm
        {
            get { return _creditaricm; }
            set { _creditaricm = value; }
        }        

        public Int32 idnfevales
        {
            get { return _idnfevales; }
            set { _idnfevales = value; }
        }


        public String somaproduto
        {
            get { return _somaproduto; }
            set { _somaproduto = value; }
        }
        public String fatura
        {
            get { return _fatura; }
            set { _fatura = value; }
        }

    }
}
