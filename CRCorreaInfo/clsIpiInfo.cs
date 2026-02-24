using System;

namespace CRCorreaInfo
{
    public class clsIpiInfo
    {
        private Int32 _id;
        private String _codigo;
        private Decimal _aliquota;
        private String _tipo;
        private Decimal _aliqipi;
        private Decimal _aliqicm;
        private Decimal _aliqii;
        private Decimal _aliqmer;
//        private String _unidade;
        private String _pispasep;
        private String _cofins;
        private String _nome;
        private String _ativo;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public Decimal aliquota
        {
            get { return _aliquota; }
            set { _aliquota = value; }
        }

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public Decimal aliqipi
        {
            get { return _aliqipi; }
            set { _aliqipi = value; }
        }

        public Decimal aliqicm
        {
            get { return _aliqicm; }
            set { _aliqicm = value; }
        }

        public Decimal aliqii
        {
            get { return _aliqii; }
            set { _aliqii = value; }
        }

        public Decimal aliqmer
        {
            get { return _aliqmer; }
            set { _aliqmer = value; }
        }

/*        public String unidade
        {
            get { return _unidade; }
            set { _unidade = value; }
        }
*/
        public String pispasep
        {
            get { return _pispasep; }
            set { _pispasep = value; }
        }

        public String cofins
        {
            get { return _cofins; }
            set { _cofins = value; }
        }

        public String nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public String ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
    }
}
