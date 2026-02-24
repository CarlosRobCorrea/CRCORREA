using System;

namespace CRCorreaInfo
{
    public class clsFormsInfo
    {
        private Int32 _id;
        private Nullable<DateTime> _datac;
        private String _projeto;
        private String _nome;
        private String _nomeform;
        private String _descricao;
        private String _lista;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Nullable<DateTime> datac
        {
            get { return _datac; }
            set { _datac = value; }
        }

        public String projeto
        {
            get { return _projeto; }
            set { _projeto = value; }
        }

        public String nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public String nomeform
        {
            get { return _nomeform; }
            set { _nomeform = value; }
        }

        public String descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public String lista
        {
            get { return _lista; }
            set { _lista = value; }
        }
    }
}
