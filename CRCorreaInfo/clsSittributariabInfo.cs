using System;

namespace CRCorreaInfo
{
    public class clsSittributariabInfo
    {
        private Int32 _id;
        private String _codigo;
        private String _nome;
        private Int32 _idreferencia;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public String nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public Int32 idreferencia
        {
            get { return _idreferencia; }
            set { _idreferencia = value; }
        }
    }
}
