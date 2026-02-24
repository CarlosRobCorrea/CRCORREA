using System;

namespace CRCorreaInfo
{
    public class clsCadClienteEnderecoInfo
    {
        public String cidade { get; set; }

        Int32 _id;
        Int32 _idcliente;
        Int32 _idzona;
        String _tipoendnome;
        String _cobnome;
        String _endereco;
        String _bairro;
        String _cidade;
        String _uf;
        public Int32 idcidade { get; set; }
        Int32 _idestado;
        String _cep;
        String _postal;
        String _email;
        String _dddi;
        String _ddd;
        String _telefone;
        String _contato;
        String _setor;
        Int32 _ramal;
        String _dddfax;
        String _telefax;
        String _observa;
        String _ie;
        String _cgc;
        String _ibge;

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
        public Int32 idzona
        {
            get { return _idzona; }
            set { _idzona = value; }
        }
        public String tipoendnome
        {
            get { return _tipoendnome; }
            set { _tipoendnome = value; }
        }

        public String cobnome
        {
            get { return _cobnome; }
            set { _cobnome = value; }
        }

        public String endereco
        {
            get { return _endereco; }
            set { _endereco = value; }
        }

        public String bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }


        public String uf
        {
            get { return _uf; }
            set { _uf = value; }
        }

        public Int32 idestado
        {
            get { return _idestado; }
            set { _idestado = value; }
        }

        public String cep
        {
            get { return _cep; }
            set { _cep = value; }
        }

        public String postal
        {
            get { return _postal; }
            set { _postal = value; }
        } 

        public String email
        {
            get { return _email; }
            set { _email = value; }
        }

        public String dddi
        {
            get { return _dddi; }
            set { _dddi = value; }
        }

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

        public Int32 ramal
        {
            get { return _ramal; }
            set { _ramal = value; }
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

        public String observa
        {
            get { return _observa; }
            set { _observa = value; }
        }

        public String ie
        {
            get { return _ie; }
            set { _ie = value; }
        }

        public String cgc
        {
            get { return _cgc; }
            set { _cgc = value; }
        }

        public String ibge
        {
            get { return _ibge; }
            set { _ibge = value; }
        }
    }
}
