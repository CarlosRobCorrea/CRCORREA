using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsMovPecasMesInfo
    {
        private Int32 _id;
        private Int32 _idcodigo;
        private Int32 _anomes;
        private Decimal _qtdeinicio;
        private Decimal _qtdeentra;
        private Decimal _qtdesaida;
        private Decimal _qtdesaldo;
        private Decimal _customediomes;
        private Decimal _valoracumulado;

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

        public Int32 anomes
        {
            get { return _anomes; }
            set { _anomes = value; }
        }

        public Decimal qtdeinicio
        {
            get { return _qtdeinicio; }
            set { _qtdeinicio = value; }
        }

        public Decimal qtdeentra
        {
            get { return _qtdeentra; }
            set { _qtdeentra = value; }
        }

        public Decimal qtdesaida
        {
            get { return _qtdesaida; }
            set { _qtdesaida = value; }
        }

        public Decimal qtdesaldo
        {
            get { return _qtdesaldo; }
            set { _qtdesaldo = value; }
        }

        public Decimal customediomes
        {
            get { return _customediomes; }
            set { _customediomes = value; }
        }

        public Decimal valoracumulado
        {
            get { return _valoracumulado; }
            set { _valoracumulado = value; }
        }

    }
}
