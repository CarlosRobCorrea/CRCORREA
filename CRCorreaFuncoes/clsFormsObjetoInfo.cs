using System;

namespace CRCorreaFuncoes
{
    public class clsFormsObjetoInfo
    {
        private Int32 _id;
        private Int32 _idforms;
        private Int32 _idformsobjeto;
        private Nullable<DateTime> _datac;
        private String _nome;
        private String _nomeobjeto;
        private String _tipo;
        private String _ativo;
        private String _visivel;
        private String _lista;
        private Int32 _posleft;
        private Int32 _postop;
        private Int32 _posdif;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idforms
        {
            get { return _idforms; }
            set { _idforms = value; }
        }

        public Int32 idformsobjeto
        {
            get { return _idformsobjeto; }
            set { _idformsobjeto = value; }
        }

        public Nullable<DateTime> datac
        {
            get { return _datac; }
            set { _datac = value; }
        }

        public String nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public String nomeobjeto
        {
            get { return _nomeobjeto; }
            set { _nomeobjeto = value; }
        }

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public String ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public String visivel
        {
            get { return _visivel; }
            set { _visivel = value; }
        }

        public String lista
        {
            get { return _lista; }
            set { _lista = value; }
        }

        public Int32 posleft
        {
            get { return _posleft; }
            set { _posleft = value; }
        }

        public Int32 postop
        {
            get { return _postop; }
            set { _postop = value; }
        }

        public Int32 posdif
        {
            get { return _posdif; }
            set { _posdif = value; }
        }
    }
}
