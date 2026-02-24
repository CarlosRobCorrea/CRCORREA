using System;

namespace CRCorreaInfo
{
    public class clsClienteContatoInfo
    {
        Int32 _id;
        Int32 _idcliente;
        String _tipocontato;
        String _contato;
        String _setor;
        String _email;
        String _ddd;
        String _telefone;
        Int32 _ramal;
        Int32 _ramalfax;
        String _dddfax;
        String _telefax;
        String _dddcelular;
        String _celular;
        String _observa;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 idcliente
        {
            get { return _idcliente; }
            set { _idcliente = value; }
        }

        public String tipocontato
        {
            get { return _tipocontato; }
            set { _tipocontato = value; }
        }

        public String contato
        {
            get { return _contato; }
            set { _contato = value; }
        }

        public String setor
        {
            get { return _setor; }
            set { _setor = value; }
        }

        public String email
        {
            get { return _email; }
            set { _email = value; }
        }

     //   public String dddi
     //   {
     //       get { return _dddi; }
      //      set { _dddi = value; }
      //  }

        public String ddd
        {
            get { return _ddd; }
            set { _ddd = value; }
        }

        public String telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

        public Int32 ramal
        {
            get { return _ramal; }
            set { _ramal = value; }
        }
        public Int32 ramalfax
        {
            get { return _ramalfax; }
            set { _ramalfax = value; }
        }

        public String dddfax
        {
            get { return _dddfax; }
            set { _dddfax = value; }
        }

        public String telefax
        {
            get { return _telefax; }
            set { _telefax = value; }
        }

        public String dddcelular
        {
            get { return _dddcelular; }
            set { _dddcelular = value; }
        }

        public String celular
        {
            get { return _celular; }
            set { _celular = value; }
        }

        public String observa
        {
            get { return _observa; }
            set { _observa = value; }
        }

    }
}
