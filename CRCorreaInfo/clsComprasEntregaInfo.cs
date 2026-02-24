using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsComprasEntregaInfo
    {
        private Int32 _id;
        private Int32 _idcompras;
        private Int32 _idcompras1;
        private Int32 _idos;
        private Decimal _qtdeentrega;
        private DateTime _dataentrega;
        private Decimal _qtdeentregue;
        private Decimal _qtdedefeito;
        private Decimal _qtdesucata;
        private Decimal _qtdebaixada;
        private Decimal _qtdeosaux;
        private Decimal _qtdesaldo;

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

        public Int32 idos
        {
            get { return _idos; }
            set { _idos = value; }
        }

        public Decimal qtdeentrega
        {
            get { return _qtdeentrega; }
            set { _qtdeentrega = value; }
        }

        public DateTime dataentrega
        {
            get { return _dataentrega; }
            set { _dataentrega = value; }
        }

        public Decimal qtdeentregue
        {
            get { return _qtdeentregue; }
            set { _qtdeentregue = value; }
        }

        public Decimal qtdedefeito
        {
            get { return _qtdedefeito; }
            set { _qtdedefeito = value; }
        }

        public Decimal qtdesucata
        {
            get { return _qtdesucata; }
            set { _qtdesucata = value; }
        }

        public Decimal qtdebaixada
        {
            get { return _qtdebaixada; }
            set { _qtdebaixada = value; }
        }

        public Decimal qtdeosaux
        {
            get { return _qtdeosaux; }
            set { _qtdeosaux = value; }
        }

        public Decimal qtdesaldo
        {
            get { return _qtdesaldo; }
            set { _qtdesaldo = value; }
        }

    }
}
