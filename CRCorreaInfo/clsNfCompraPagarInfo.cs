using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsNfCompraPagarInfo
    {
        private Int32 _id;
        private Int32 _idnota;
        private Int32 _posicao;
        private Int32 _posicaofim;
        private DateTime _data;
        private String _pagou;
        private Decimal _valor;
        private Int32 _idtipopaga;        
        private Decimal _boletonro;
        private String _dv;        

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idnota
        {
            get { return _idnota; }
            set { _idnota = value; }
        }

        public Int32 posicao
        {
            get { return _posicao; }
            set { _posicao = value; }
        }

        public Int32 posicaofim
        {
            get { return _posicaofim; }
            set { _posicaofim = value; }
        }

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }

        public String pagou
        {
            get { return _pagou; }
            set { _pagou = value; }
        }

        public Decimal valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public Int32 idtipopaga
        {
            get { return _idtipopaga; }
            set { _idtipopaga = value; }
        }        

        public Decimal boletonro
        {
            get { return _boletonro; }
            set { _boletonro = value; }
        }

        public String dv
        {
            get { return _dv; }
            set { _dv = value; }
        }
    }
}
