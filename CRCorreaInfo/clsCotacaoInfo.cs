using System;

namespace CRCorreaInfo
{
    public class clsCotacaoInfo
    {
        private Int32 _id;
        private Int32 _filial;
        private Int32 _numero;
        private Int32 _ano;
        private DateTime _datamontagem;
        private DateTime _datafechamento;
        private DateTime _tudofechadoem;
        private String _comprador;
        private String _termino;
        private String _observar;
        private String _motivoreprovado;
        private String _respostacomprador;
        private Decimal _totalprevisto;
        private Int32 _idautorizante;

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

        public DateTime datamontagem
        {
            get { return _datamontagem; }
            set { _datamontagem = value; }
        }

        public DateTime datafechamento
        {
            get { return _datafechamento; }
            set { _datafechamento = value; }
        }

        public DateTime tudofechadoem
        {
            get { return _tudofechadoem; }
            set { _tudofechadoem = value; }
        }

        public String comprador
        {
            get { return _comprador; }
            set { _comprador = value; }
        }

        public String termino
        {
            get { return _termino; }
            set { _termino = value; }
        }

        public String observar
        {
            get { return _observar; }
            set { _observar = value; }
        }

        public String motivoreprovado
        {
            get { return _motivoreprovado; }
            set { _motivoreprovado = value; }
        }

        public String respostacomprador
        {
            get { return _respostacomprador; }
            set { _respostacomprador = value; }
        }

        public Decimal totalprevisto
        {
            get { return _totalprevisto; }
            set { _totalprevisto = value; }
        }
        public Int32 idautorizante
        {
            get { return _idautorizante; }
            set { _idautorizante = value; }
        }

    }
}
