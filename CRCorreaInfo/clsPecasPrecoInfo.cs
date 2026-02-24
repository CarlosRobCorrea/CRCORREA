using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsPecasPrecoInfo
    {
        private DateTime _data;
        private Int32 _id;
        private Int32 _idcodigo;
        private Decimal _valoravista;
        private Decimal _valoraprazo;
//        private Decimal _valormanutencao;

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }
        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int32 idcodigo
        {
            get { return _idcodigo; }
            set { _idcodigo = value; }
        }
        public Decimal valoravista
        {
            get { return _valoravista; }
            set { _valoravista = value; }
        }
        public Decimal valoraprazo
        {
            get { return _valoraprazo; }
            set { _valoraprazo = value; }
        }
        //public Decimal valormanutencao
        //{
        //    get { return _valormanutencao; }
        //    set { _valormanutencao = value; }
        //}

    }
}
