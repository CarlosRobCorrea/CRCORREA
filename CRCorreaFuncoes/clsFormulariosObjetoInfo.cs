using System;

namespace CRCorreaFuncoes
{
    public class clsFormulariosObjetoInfo
    {
        private Int32 _id;
        private Int32 _idformulario;
        private String _nomeobjeto;
        private String _nome;
        private String _ativo;
        private String _liberado;
        private String _aparecelista;
        private String _verificarform;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idformulario
        {
            get { return _idformulario; }
            set { _idformulario = value; }
        }

        public String nomeobjeto
        {
            get { return _nomeobjeto; }
            set { _nomeobjeto = value; }
        }

        public String nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public String ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public String liberado
        {
            get { return _liberado; }
            set { _liberado = value; }
        }

        public String aparecelista
        {
            get { return _aparecelista; }
            set { _aparecelista = value; }
        }

        public String verificarform
        {
            get { return _verificarform; }
            set { _verificarform = value; }
        }

    }
}
