using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsMovPecasInfo
    {
        private Int32 _id;
        private Int32 _idtipodocumento;
        private Int32 _iddocumento;
        private Int32 _iddocumentoitem;
        private Int32 _iddocumentoitemsub;
        private Int32 _idcodigo;
        private Int32 _idcondpagto;
        private DateTime _data;
        private Int64 _numero;
        private String _documento;
        private String _cognome;
        private String _operacao;
        private Decimal _qtde;
        private Decimal _qtdesaida;
        private Decimal _qtdeanterior;
        private Decimal _saldo;
        private Decimal _valor;
        private Decimal _valortotal;
        private Decimal _valoracumulado;
        private Decimal _customedio;
        private String _usuario;
        private String _setor;
        private String _motivo;
        private DateTime _dataretorno;
        private String _verificado;
        private DateTime _hora;
        //private Int64 _controle;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idtipodocumento
        {
            get { return _idtipodocumento; }
            set { _idtipodocumento = value; }
        }

        public Int32 iddocumento
        {
            get { return _iddocumento; }
            set { _iddocumento = value; }
        }

        public Int32 iddocumentoitem
        {
            get { return _iddocumentoitem; }
            set { _iddocumentoitem = value; }
        }
        public Int32 iddocumentoitemsub
        {
            get { return _iddocumentoitemsub; }
            set { _iddocumentoitemsub = value; }
        }

        public Int32 idcodigo
        {
            get { return _idcodigo; }
            set { _idcodigo = value; }
        }

        public Int32 idcondpagto
        {
            get { return _idcondpagto; }
            set { _idcondpagto = value; }
        }

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }
        public DateTime hora
        {
            get { return _hora; }
            set { _hora = value; }
        }

        public Int64 numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public String documento
        {
            get { return _documento; }
            set { _documento = value; }
        }

        public String cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
        }

        public String operacao
        {
            get { return _operacao; }
            set { _operacao = value; }
        }

        public Decimal qtde
        {
            get { return _qtde; }
            set { _qtde = value; }
        }

        public Decimal qtdesaida
        {
            get { return _qtdesaida; }
            set { _qtdesaida = value; }
        }

        public Decimal qtdeanterior
        {
            get { return _qtdeanterior; }
            set { _qtdeanterior = value; }
        }

        public Decimal saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }

        public Decimal valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public Decimal valortotal
        {
            get { return _valortotal; }
            set { _valortotal = value; }
        }

        public Decimal valoracumulado
        {
            get { return _valoracumulado; }
            set { _valoracumulado = value; }
        }

        public Decimal customedio
        {
            get { return _customedio; }
            set { _customedio = value; }
        }

        public String usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public String setor
        {
            get { return _setor; }
            set { _setor = value; }
        }

        public String motivo
        {
            get { return _motivo; }
            set { _motivo = value; }
        }

        public DateTime dataretorno
        {
            get { return _dataretorno; }
            set { _dataretorno = value; }
        }
        public String verificado
        {
            get { return _verificado; }
            set { _verificado = value; }
        }

    }
}
