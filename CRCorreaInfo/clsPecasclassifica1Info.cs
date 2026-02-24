using System;

namespace CRCorreaInfo
{
    public class clsPecasclassifica1Info
    {
        private Int32 _id;
        private Int32 _idclassifica;
        private String _codigo;
        private String _nome;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idclassifica
        {
            get { return _idclassifica; }
            set { _idclassifica = value; }
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

    }
}
