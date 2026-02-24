using System;

namespace CRCorreaInfo
{ 
    public class clsFeriadosInfo
    {
        private Int32 _id;
        private DateTime _data;
        private String _descricao;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }

        public String descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
    }
}
