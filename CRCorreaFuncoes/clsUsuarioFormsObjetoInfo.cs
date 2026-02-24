using System;

namespace CRCorreaFuncoes
{
    public class clsUsuarioFormsObjetoInfo
    {
        private Int32 _id;
        private Int32 _idusuario;
        private Int32 _idformsobjeto;
        private String _ativo;
        private String _visivel;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idusuario
        {
            get { return _idusuario; }
            set { _idusuario = value; }
        }

        public Int32 idformsobjeto
        {
            get { return _idformsobjeto; }
            set { _idformsobjeto = value; }
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
    }
}
