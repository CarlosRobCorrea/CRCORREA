using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsPedido2Info
    {
        private Int32 _id;
        private Int32 _idordemservico;
        private Int32 _idpedido;
        private Int32 _idpedido1;
        private DateTime _entrega;
        private Decimal _qtde;
        private Decimal _qtdeentregue;
        private Decimal _qtdedefeito;
        private Decimal _qtdebaixada;
        private Decimal _qtdesucata;
        private Decimal _qtdeosaux;
        private Decimal _qtdesaldo;
        private String _divergencia;
        private String _motivo01;
        private String _motivo02;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int32 idordemservico
        {
            get { return _idordemservico; }
            set { _idordemservico = value; }
        }

        public Int32 idpedido
        {
            get { return _idpedido; }
            set { _idpedido = value; }
        }

        public Int32 idpedido1
        {
            get { return _idpedido1; }
            set { _idpedido1 = value; }
        }

        public DateTime entrega
        {
            get { return _entrega; }
            set { _entrega = value; }
        }

        public Decimal qtde
        {
            get { return _qtde; }
            set { _qtde = value; }
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

        public Decimal qtdebaixada
        {
            get { return _qtdebaixada; }
            set { _qtdebaixada = value; }
        }

        public Decimal qtdesucata
        {
            get { return _qtdesucata; }
            set { _qtdesucata = value; }
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

        public String divergencia
        {
            get { return _divergencia; }
            set { _divergencia = value; }
        }

        public String motivo01
        {
            get { return _motivo01; }
            set { _motivo01 = value; }
        }

        public String motivo02
        {
            get { return _motivo02; }
            set { _motivo02 = value; }
        }

    }
}
