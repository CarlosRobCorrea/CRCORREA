using System;

namespace CRCorreaInfo
{
    public class clsIpiEstadosicmsInfo
    {
        private Int32 _id;
        private Int32 _idipi;
        private Int32 _idestado;
        private Int32 _idestadodestino;
        private Decimal _aliquota;
        private Decimal _reducao;
        private Decimal _iva;
        private Decimal _reducaoiva;
        private Int32 _iddizeresnfv;
       
        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idipi
        {
            get { return _idipi; }
            set { _idipi = value; }
        }       

        public Int32 idestado
        {
            get { return _idestado; }
            set { _idestado = value; }
        }

        public Int32 idestadodestino
        {
            get { return _idestadodestino; }
            set { _idestadodestino = value; }
        }

        public Decimal aliquota
        {
            get { return _aliquota; }
            set { _aliquota = value; }
        }

        public Decimal reducao
        {
            get { return _reducao; }
            set { _reducao = value; }
        }  

        public Decimal iva
        {
            get { return _iva; }
            set { _iva = value; }
        }

        public Decimal reducaoiva
        {
            get { return _reducaoiva; }
            set { _reducaoiva = value; }
        }

        public Int32 iddizeresnfv
        {
            get { return _iddizeresnfv; }
            set { _iddizeresnfv = value; }
        }
    }
}
