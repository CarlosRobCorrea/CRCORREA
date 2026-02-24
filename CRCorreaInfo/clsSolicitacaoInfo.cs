using System;

namespace CRCorreaInfo
{
    public class clsSolicitacaoInfo
    {
        private Int32 _id;
        private Int32 _numero;
        private DateTime _data;
        private Int32 _ano;
        private String _emitente;
        private Int32 _filial;
        private Int32 _idsolicitante;
        private String _solicitante;
        private Int32 _idarea;
        private String _area;
        private Int32 _iddepto;
        private Int32 _idpedidocompra;
        private Int32 _idpedidocompraitem;
        private Int32 _idcotacao;
        private Int32 _idcotacaoitem;
        private Int32 _idcodigo;
        private String _complemento;
        private String _complemento1;     
        private String _consumo;
        private Int32 _idhistorico;
        private Int32 _idcentrocusto;
        private Int32 _idcodigoctabil;
        private String _tipoproduto;
        private String _tipoentrada;
        private Int32 _idos;
        private String _tipodestino;
        private Int32 _iddestino;
        private Decimal _qtdesol;
        private Decimal _qtde;
        private Int32 _idunid;
        private Decimal _qtdetotal;        
        private String _prioridade;
        private String _motivojustificado;
        private String _motivoreprovado;
        private Int32 _idautorizantesol;
        private Int32 _idautorizantealmox;
        private Int32 _idautorizanteger;
        private String _aprovadosol;
        private String _aprovadofab;
        private String _aprovadoger;
        private Int32 _fornecedorganhou;

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

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }

        public Int32 ano
        {
            get { return _ano; }
            set { _ano = value; }
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

        public Int32 idsolicitante
        {
            get { return _idsolicitante; }
            set { _idsolicitante = value; }
        }
        public String solicitante
        {
            get { return _solicitante; }
            set { _solicitante = value; }
        }
        public Int32 idarea
        {
            get { return _idarea; }
            set { _idarea = value; }
        }
        public String area
        {
            get { return _area; }
            set { _area = value; }
        }
        public Int32 iddepto
        {
            get { return _iddepto; }
            set { _iddepto = value; }
        }

        
        public Int32 idpedidocompra
        {
            get { return _idpedidocompra; }
            set { _idpedidocompra = value; }
        }

        public Int32 idpedidocompraitem
        {
            get { return _idpedidocompraitem; }
            set { _idpedidocompraitem = value; }
        }

        public Int32 idcotacao
        {
            get { return _idcotacao; }
            set { _idcotacao = value; }
        }

        public Int32 idcotacaoitem
        {
            get { return _idcotacaoitem; }
            set { _idcotacaoitem = value; }
        }

        public Int32 idcodigo
        {
            get { return _idcodigo; }
            set { _idcodigo = value; }
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
        /*
        public String descricaoesp
        {
            get { return _descricaoesp; }
            set { _descricaoesp = value; }
        }

        public String msg
        {
            get { return _msg; }
            set { _msg = value; }
        }*/

        public String consumo
        {
            get { return _consumo; }
            set { _consumo = value; }
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

        public String tipoproduto
        {
            get { return _tipoproduto; }
            set { _tipoproduto = value; }
        }

        public String tipoentrada
        {
            get { return _tipoentrada; }
            set { _tipoentrada = value; }
        }

        public Int32 idos
        {
            get { return _idos; }
            set { _idos = value; }
        }

        public String tipodestino
        {
            get { return _tipodestino; }
            set { _tipodestino = value; }
        }

        public Int32 iddestino
        {
            get { return _iddestino; }
            set { _iddestino = value; }
        }

        public Decimal qtdesol
        {
            get { return _qtdesol; }
            set { _qtdesol = value; }
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

        public Decimal qtdetotal
        {
            get { return _qtdetotal; }
            set { _qtdetotal = value; }
        }
        /*
        public String tipopedido
        {
            get { return _tipopedido; }
            set { _tipopedido = value; }
        }
        */
        public String prioridade
        {
            get { return _prioridade; }
            set { _prioridade = value; }
        }

        public String motivojustificado
        {
            get { return _motivojustificado; }
            set { _motivojustificado = value; }
        }

        public String motivoreprovado
        {
            get { return _motivoreprovado; }
            set { _motivoreprovado = value; }
        }

        public Int32 idautorizantesol
        {
            get { return _idautorizantesol; }
            set { _idautorizantesol = value; }
        }

        public Int32 idautorizantealmox
        {
            get { return _idautorizantealmox; }
            set { _idautorizantealmox = value; }
        }

        public Int32 idautorizanteger
        {
            get { return _idautorizanteger; }
            set { _idautorizanteger = value; }
        }

        public String aprovadosol
        {
            get { return _aprovadosol; }
            set { _aprovadosol = value; }
        }

        public String aprovadofab
        {
            get { return _aprovadofab; }
            set { _aprovadofab = value; }
        }

        public String aprovadoger
        {
            get { return _aprovadoger; }
            set { _aprovadoger = value; }
        }

        public Int32 fornecedorganhou
        {
            get { return _fornecedorganhou; }
            set { _fornecedorganhou = value; }
        }

    }
}
