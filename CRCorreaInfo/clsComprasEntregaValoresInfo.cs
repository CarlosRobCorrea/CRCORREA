using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsComprasEntregaValoresInfo
    {
        private Int32 _id;
        private Int32 _idcompras;
        private Int32 _idcompras1;
        private Int32 _idcomprasentrega;
        private DateTime _datavencimento;
        private Decimal _valorvencimento;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idcompras
        {
            get { return _idcompras; }
            set { _idcompras = value; }
        }

        public Int32 idcompras1
        {
            get { return _idcompras1; }
            set { _idcompras1 = value; }
        }

        public Int32 idcomprasentrega
        {
            get { return _idcomprasentrega; }
            set { _idcomprasentrega = value; }
        }

        public DateTime datavencimento
        {
            get { return _datavencimento; }
            set { _datavencimento = value; }
        }

        public Decimal valorvencimento
        {
            get { return _valorvencimento; }
            set { _valorvencimento = value; }
        }
    }
}
